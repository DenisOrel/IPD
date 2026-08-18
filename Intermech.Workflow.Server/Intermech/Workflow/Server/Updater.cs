// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.Updater
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.ApplicationModel;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.MetadataUpdates;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Kernel.Services;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;

#nullable disable
namespace Intermech.Workflow.Server;

internal class Updater : IExtendedUpdatable, IUpdatable
{
  private const int _currentVersion = 3;
  private const string MyModuleName = "WORKFLOW";
  private int _dbVersion = -1;
  private int _dbRevision = -1;

  public string[] GetUpdateScripts()
  {
    return new string[3]
    {
      "Intermech.Workflow.xml",
      "Intermech.Forums.xml",
      "Intermech.Email.xml"
    };
  }

  public void BeforeExecScript(IUserSession session, string scriptName)
  {
    if (this._dbVersion != -1)
      return;
    UserSession userSession = session as UserSession;
    userSession.GetDBVersionEx("WORKFLOW", ref this._dbVersion, ref this._dbRevision);
    if (this._dbVersion != 0)
      return;
    IDBAttributeType attributeType = userSession.GetAttributeType(wfConsts.AttrBodyGuid, false);
    if (attributeType == null || attributeType.Name.EndsWith("_del"))
      return;
    attributeType.Name += "_del";
  }

  public void AfterExecScript(IUserSession session, string scriptName)
  {
  }

  private string SingleStrToIntStr(string s)
  {
    if (s.Contains(","))
      s = s.Replace(',', '.');
    return ((int) Math.Round((double) Convert.ToSingle(s, (IFormatProvider) CultureInfo.InvariantCulture))).ToString();
  }

  private void WriteGraphData(UserSession session, NameValueCollection nc)
  {
    IDBObject dbObject = (IDBObject) null;
    try
    {
      if (nc["Guid"] != null)
        dbObject = session.GetObject(new Guid(nc["Guid"]), false);
      else if (nc["ObjectID"] != null)
        dbObject = session.GetObject(Convert.ToInt64(nc["ObjectID"]), false);
    }
    catch
    {
    }
    if (dbObject == null)
      return;
    StringList stringList = new StringList();
    if (nc["X"] != null)
      stringList.Values["X"] = this.SingleStrToIntStr(nc["X"]);
    if (nc["Y"] != null)
      stringList.Values["Y"] = this.SingleStrToIntStr(nc["Y"]);
    if (nc["Points"] != null)
    {
      string[] strArray = nc["Points"].Split('|');
      for (int index = 0; index < strArray.Length; ++index)
        strArray[index] = this.SingleStrToIntStr(strArray[index]);
      stringList.Values["P"] = string.Join("|", strArray);
    }
    string commaText = stringList.CommaText;
    if (string.IsNullOrEmpty(commaText) || !(dbObject.Attributes is DBObjectAttributeCollection attributes))
      return;
    int attrGraphDataId = wfConsts.AttrGraphDataID;
    object[] initValues = new object[1]
    {
      (object) commaText
    };
    attributes.AddAttribute(attrGraphDataId, false, false, initValues);
  }

  private void _patchV1(UserSession session, int typeID)
  {
    ColumnDescriptor[] columns = new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, SortOrders.ASC, 0)
    };
    DataTable dataTable = session.GetObjectCollection(typeID).Select(new DBRecordSetParams((ConditionStructure[]) null, columns));
    string message = string.Empty;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      long int64 = Convert.ToInt64(row[0]);
      IDBObject dbObject = session.GetObject(int64);
      OldWorkflowGraph oldWorkflowGraph = new OldWorkflowGraph();
      int attrBodyId = wfConsts.AttrBodyID;
      using (MemoryStream stream = StreamHelper.BlobReaderToStream(dbObject.GetAttributeByID(attrBodyId) as IBlobReader))
      {
        try
        {
          oldWorkflowGraph.Load((Stream) stream, int64, (IUserSession) session);
          foreach (NameValueCollection node in oldWorkflowGraph.Nodes)
            this.WriteGraphData(session, node);
          foreach (NameValueCollection link in oldWorkflowGraph.Links)
            this.WriteGraphData(session, link);
        }
        catch (Exception ex)
        {
          string str = string.Empty;
          stream.Position = 0L;
          using (StreamReader streamReader = new StreamReader((Stream) stream))
            str = streamReader.ReadToEnd();
          if (!string.IsNullOrEmpty(str))
          {
            session.EventLogHelper?.AddToTrace($"Error: {ex.Message}\r\nObjectID: {int64.ToString()}\r\n{str}\r\n", Consts.traceAlways, "wfpatch1.log");
            if (string.IsNullOrEmpty(message))
              message = "Во время патча графических данных произошли ошибки, подробнее см. wfpatch1.log";
          }
        }
      }
    }
    if (!string.IsNullOrEmpty(message))
      throw new Exception(message);
  }

  private void PatchV1(UserSession session)
  {
    this._patchV1(session, wfConsts.SchemesTypeID);
    this._patchV1(session, wfConsts.ProcessesTypeID);
  }

  private bool Patch(UserSession session, int toVersion, Updater.PatchFunc func)
  {
    if (this._dbVersion < toVersion)
    {
      this._dbRevision = Updater.DateTimeToMins(DateTime.UtcNow);
      session.SetDBVersion("WORKFLOW", this._dbVersion, this._dbRevision);
      string s = $"Workflow patch ({this._dbVersion} -> {toVersion})...";
      try
      {
        this.DumpColor(session, s, false, ConsoleColor.Green);
        func(session);
        this._dbVersion = toVersion;
        session.SetDBVersion("WORKFLOW", this._dbVersion, this._dbRevision);
        this.DumpColor(session, "OK", true, ConsoleColor.Green);
      }
      catch (Exception ex)
      {
        string err = "Ошибка: " + ex.Message;
        this.DumpError(session, err);
        return false;
      }
    }
    return true;
  }

  private void PatchV2(UserSession session)
  {
    IDBAttributeType attributeType = session.GetAttributeType(wfConsts.AttrBodyGuid, false);
    if (attributeType == null)
      return;
    bool developerMode = session.DeveloperMode;
    session.DeveloperMode = true;
    try
    {
      AttributeTypeProperties propertiesStructure = attributeType.PropertiesStructure;
      StringBuilder stringBuilder = new StringBuilder(propertiesStructure.AttributeGuid.ToString());
      stringBuilder[0] = 'a';
      propertiesStructure.AttributeGuid = new Guid(stringBuilder.ToString());
      attributeType.PropertiesStructure = propertiesStructure;
      try
      {
        foreach (DataRow dataRow in session.DBCache.GetTable("IMS_ATTR4OBJ_TYPES").Select($"F_ATTRIBUTE_ID={(object) attributeType.AttributeID} and F_PUBLIC in (0,1)"))
        {
          int int32 = Convert.ToInt32(dataRow["F_OBJECT_TYPE"]);
          session.GetObjectType(int32, false)?.Attributes.GetAttributeByID(attributeType.AttributeID, false)?.Delete((long) Consts.DeleteInstances);
        }
        attributeType.Delete((long) (Consts.PurgeMode | Consts.DeleteInstances));
      }
      catch
      {
        stringBuilder[0] = 'c';
        propertiesStructure.AttributeGuid = new Guid(stringBuilder.ToString());
        attributeType.PropertiesStructure = propertiesStructure;
        throw;
      }
    }
    finally
    {
      session.DeveloperMode = developerMode;
    }
  }

  private void PatchV3(UserSession session)
  {
    session.GetRelationType(wfConsts.AttachmentRelationTypeID).Attributes.GetAttributeByGUID(new Guid("cad00202-306c-11d8-b4e9-00304f19f545"))?.Delete((long) Consts.DeleteInstances);
  }

  private void DumpColor(UserSession session, string s, bool newLine, ConsoleColor color)
  {
    session.EventLogHelper?.AddToTrace(s, Consts.traceAlways, string.Empty);
    if (AdminUtilsService.ServerRunMode != ServerRunModes.Console)
      return;
    int foregroundColor = (int) Console.ForegroundColor;
    Console.ForegroundColor = color;
    if (newLine)
      Console.WriteLine(s);
    else
      Console.Write(s);
    Console.ForegroundColor = (ConsoleColor) foregroundColor;
  }

  private void DumpError(UserSession session, string err)
  {
    this.DumpColor(session, err, true, ConsoleColor.Red);
  }

  internal static DateTime MinsToDateTime(int minutes)
  {
    DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
    dateTime = dateTime.AddMinutes((double) minutes);
    return dateTime;
  }

  internal static int DateTimeToMins(DateTime dt)
  {
    DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
    return (int) dt.Subtract(dateTime).TotalMinutes;
  }

  public void AfterExecAllScripts(IUserSession session)
  {
    wfConsts.Init(session);
    UserSession userSession = session as UserSession;
    MetaDataHelper.Locked = false;
    userSession.DBCache.ReloadTables((IUserSession) userSession, userSession.DataManager, "IMS_OBJECT_TYPES");
    MetaDataHelper.SyncMetadata(userSession.CacheDataSet, true);
    DateTime dateTime1;
    if (this._dbVersion < 3)
    {
      int dbRevision = this._dbRevision;
      if (this._dbRevision != 0)
      {
        DateTime dateTime2 = Updater.MinsToDateTime(this._dbRevision);
        dateTime1 = DateTime.UtcNow;
        if (dateTime1.Subtract(dateTime2).TotalMinutes > 15.0)
          this._dbRevision = 0;
      }
      if (this._dbRevision == 0)
      {
        this._dbRevision = Updater.DateTimeToMins(DateTime.UtcNow);
        if (userSession.SetDBVersion("WORKFLOW", this._dbVersion, this._dbRevision, " and F_REVISION_ID=" + dbRevision.ToString()) != 0)
        {
          try
          {
            if (this.Patch(userSession, 1, new Updater.PatchFunc(this.PatchV1)))
            {
              if (this.Patch(userSession, 2, new Updater.PatchFunc(this.PatchV2)))
                this.Patch(userSession, 3, new Updater.PatchFunc(this.PatchV3));
            }
          }
          finally
          {
            if (this._dbRevision != 0)
            {
              this._dbRevision = 0;
              userSession.SetDBVersion("WORKFLOW", this._dbVersion, this._dbRevision);
            }
          }
        }
      }
    }
    if (3 != this._dbVersion)
    {
      string str = string.Empty;
      if (this._dbRevision != 0)
      {
        dateTime1 = Updater.MinsToDateTime(this._dbRevision).ToLocalTime();
        str = dateTime1.ToString();
        if (!string.IsNullOrEmpty(str))
          str = "; patch: " + str;
      }
      string err = string.Format(LocalizationHolder.rm.GetString("ErrDifferentDBVersion"), (object) this._dbVersion, (object) (3.ToString() + str));
      this.DumpError(userSession, err);
    }
    IDBAttributeType attributeType = session.GetAttributeType(wfConsts.AttrActivityStatusGuid);
    DataTable possibleValues = attributeType.GetPossibleValues();
    string description1 = ActivityStatus.ScriptExecuted.GetDescription<ActivityStatus>();
    string description2 = ActivityStatus.LCStepExecuted.GetDescription<ActivityStatus>();
    DataRow[] dataRowArray1 = possibleValues.Select($"{attributeType.PossibleValueFieldName}='{9}'");
    DataRow[] dataRowArray2 = possibleValues.Select($"{attributeType.PossibleValueFieldName}='{10}'");
    bool flag = false;
    if (dataRowArray1.Length == 0)
    {
      possibleValues.Rows.Add((object) possibleValues.Rows.Count, (object) 9, (object) description1);
      flag = true;
    }
    if (dataRowArray2.Length == 0)
    {
      possibleValues.Rows.Add((object) possibleValues.Rows.Count, (object) 10, (object) description2);
      flag = true;
    }
    if (!flag)
      return;
    attributeType.SetNewPossibleValues(possibleValues);
  }

  internal static void CopyCoordsFromSchemesToProcesses(
    IConsoleService console,
    IUserSession session)
  {
    int num1 = 0;
    int num2 = 0;
    int num3 = 0;
    int num4 = 0;
    int num5 = 0;
    int num6 = 0;
    console.Write("Processing", ConsoleColor.White);
    ColumnDescriptor[] columns = new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, SortOrders.ASC, 1),
      new ColumnDescriptor((object) wfConsts.AttrPrototypeID, AttributeSourceTypes.Auto, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, 0)
    };
    DataTable dataTable = session.GetObjectCollection(wfConsts.ProcessesTypeID).Select(new DBRecordSetParams((ConditionStructure[]) null, columns));
    long num7 = 0;
    WorkflowGraph workflowGraph1 = (WorkflowGraph) null;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      long int64_1 = !DBNull.Value.Equals(row[0]) ? Convert.ToInt64(row[0]) : 0L;
      long int64_2 = !DBNull.Value.Equals(row[1]) ? Convert.ToInt64(row[1]) : 0L;
      if (int64_2 == 0L)
        ++num3;
      else if (int64_1 != 0L)
      {
        if (num7 != int64_2 || workflowGraph1 == null)
        {
          workflowGraph1 = new WorkflowGraph(int64_2, session, GraphOptions.SkipParent);
          num7 = int64_2;
          ++num1;
          console.Write(".", ConsoleColor.White);
        }
        if (workflowGraph1.Count != 0)
        {
          WorkflowGraph workflowGraph2 = new WorkflowGraph(int64_1, session, GraphOptions.SkipParent);
          ++num2;
          Dictionary<long, ActivityNode>.ValueCollection.Enumerator enumerator = workflowGraph2.Nodes.GetEnumerator();
          foreach (ActivityNode node in workflowGraph1.Nodes)
          {
            if (enumerator.MoveNext())
            {
              string str = string.Empty;
              IDBObject dbObject1 = session.GetObject(node.ObjectID, false);
              if (dbObject1 != null)
              {
                IDBAttribute attributeById = dbObject1.GetAttributeByID(wfConsts.AttrGraphDataID);
                if (attributeById != null)
                  str = attributeById.AsString;
              }
              if (string.IsNullOrEmpty(str))
                ++num6;
              foreach (long objectId in enumerator.Current.ObjectIDs)
              {
                IDBObject dbObject2 = session.GetObject(objectId, false);
                if (dbObject2 != null)
                {
                  ++num4;
                  IDBAttribute attributeById = dbObject2.GetAttributeByID(wfConsts.AttrGraphDataID);
                  if (attributeById == null || string.IsNullOrEmpty(attributeById.AsString))
                  {
                    dbObject2.Attributes.AddAttribute(wfConsts.AttrGraphDataID, false, new object[1]
                    {
                      (object) str
                    });
                    ++num5;
                  }
                }
              }
            }
            else
              break;
          }
        }
      }
    }
    console.WriteLine("Done", ConsoleColor.White);
    string str1 = string.Empty;
    if (num6 > 0)
      str1 = $" Found {num6} scheme activities without coordinates.";
    console.WriteLine($"Processed {num1} schemes, {num2} processes, {num4} activities, {num5} activities fixed. {num3} processes skipped (parent scheme not found).{str1}", ConsoleColor.White);
  }

  public void BeforeUpdates(IUserSession session)
  {
    WorkflowImporter workflowImporter = new WorkflowImporter();
  }

  private delegate void PatchFunc(UserSession session);
}
