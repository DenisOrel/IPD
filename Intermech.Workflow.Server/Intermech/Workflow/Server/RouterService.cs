// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.RouterService
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Signs.Interfaces;
using Intermech.Workflow.Server.Activities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

#nullable disable
namespace Intermech.Workflow.Server;

internal class RouterService : LongLifeObject, IRouterService
{
  public static void Register()
  {
    (ApplicationServices.Container.GetService(typeof (ICustomServices)) as ICustomServices).AddService(typeof (IRouterService), (object) new RouterService());
  }

  public IDBObject CreateMessage(
    Guid SessionGuid,
    int TypeID,
    long ToUserID,
    string Subject,
    string Text,
    long FromUserID)
  {
    IUserSession sessionById = UserSession.GetSessionByID(SessionGuid);
    return sessionById != null ? ServerFunx.CreateMessage(sessionById, TypeID, ToUserID, Subject, Text, 0L, 0L, FromUserID) : (IDBObject) null;
  }

  public IDBObject[] CreateMessage(
    Guid SessionGuid,
    long[] ToUserIDs,
    string Subject,
    string Text,
    long FromUserID)
  {
    IUserSession sessionById = UserSession.GetSessionByID(SessionGuid);
    if (sessionById == null)
      return (IDBObject[]) null;
    List<IDBObject> dbObjectList = new List<IDBObject>();
    foreach (long toUserId in ToUserIDs)
      dbObjectList.Add(ServerFunx.CreateMessage(sessionById, toUserId, Subject, Text, 0L, 0L, FromUserID));
    return dbObjectList.ToArray();
  }

  public IDBObject CreateMessage(
    Guid SessionGuid,
    long ToUserID,
    string Subject,
    string Text,
    long FromUserID)
  {
    return this.CreateMessage(SessionGuid, wfConsts.MessageTypeID, ToUserID, Subject, Text, FromUserID);
  }

  public IProcess CreateProcess(Guid SessionGuid, long SchemeID)
  {
    IProcess process = (IProcess) null;
    IUserSession sessionById = UserSession.GetSessionByID(SessionGuid);
    if (sessionById != null)
    {
      IDBObject objectBaseVersionById = sessionById.GetObjectBaseVersionByID(SchemeID, false);
      if (objectBaseVersionById != null)
        SchemeID = objectBaseVersionById.ObjectID;
      process = sessionById.GetObjectCollection(wfConsts.ProcessesTypeID).Create(SchemeID) as IProcess;
      process.CommitCreation(false);
    }
    return process;
  }

  public void ReloadSettings(SettingsGroup Group)
  {
    if (!(ApplicationServices.Container.GetService(typeof (IDBTimedEvents)) is IDBTimedEvents service))
      return;
    IUserSession sessionTemporaryClone = service.GetSystemSessionTemporaryClone("workflow.ReloadSettings");
    try
    {
      (sessionTemporaryClone as UserSession).ReloadConfigurations();
      if (Group == SettingsGroup.Base && GlobalMailSettings.Cfg != null)
      {
        GlobalMailSettings.Cfg.Load(sessionTemporaryClone);
      }
      else
      {
        if (Group != SettingsGroup.AutoLaunch)
          return;
        AutoLaunchSettings.All.Load(sessionTemporaryClone);
      }
    }
    finally
    {
      sessionTemporaryClone?.Logout("workflow.ReloadSettings");
    }
  }

  public GraphsSet GetGraphsToSign(Guid SessionGuid, long[] ObjectIDs, int[] objectsType)
  {
    GraphsCollection graphsCollection = (GraphsCollection) null;
    IUserSession sessionById = UserSession.GetSessionByID(SessionGuid);
    if (sessionById != null)
    {
      ColumnDescriptor[] columns = new ColumnDescriptor[2]
      {
        new ColumnDescriptor((object) -2),
        new ColumnDescriptor((object) wfConsts.AttrRequiredSignsID, AttributeSourceTypes.Object, ColumnContents.String, ColumnNameMapping.Default, SortOrders.NONE, 0)
      };
      ConditionStructure[] conds = new ConditionStructure[2]
      {
        new ConditionStructure(wfConsts.AttrActivityStatusID, RelationalOperators.Equal, (object) 4, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Object),
        new ConditionStructure(wfConsts.AttrRecipID, RelationalOperators.Equal, (object) sessionById.UserID, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Object)
      };
      DataTable attachmentsUsage = AttachmentFuncs.GetAttachmentsUsage(sessionById, ObjectIDs, conds, columns, new List<int>((IEnumerable<int>) new int[1]
      {
        wfConsts.ApproveTypeID
      }), true);
      if (attachmentsUsage != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) attachmentsUsage.Rows)
        {
          long int64 = Convert.ToInt64(row[0]);
          string xml = DBNull.Value.Equals(row[1]) ? string.Empty : row[1].ToString();
          if (sessionById.GetObject(int64, false) is Approve approve)
          {
            if (approve.GraphForType)
            {
              if (approve.IndividualSettingForTypes != null)
              {
                foreach (int objectTypeID in objectsType)
                {
                  SignsDataItem signsDataItem = approve.IndividualSettingForTypes.GetSignsDataItem(objectTypeID);
                  if (signsDataItem != null)
                  {
                    if (graphsCollection == null)
                      graphsCollection = new GraphsCollection();
                    foreach (SignsGroup group in (Collection<SignsGroup>) signsDataItem.Groups)
                    {
                      foreach (SignsDataItemChildren child in (Collection<SignsDataItemChildren>) group.Children)
                      {
                        GraphClass graphClass = new GraphClass(child.GraphForType, child.StrongControl, false);
                        if (!graphsCollection.Contains(graphClass))
                          graphsCollection.Add(graphClass);
                      }
                    }
                  }
                }
              }
            }
            else
            {
              if (xml.Length == wfConsts.MaxStoredTextLength)
              {
                IDBAttribute attributeById = approve.GetAttributeByID(wfConsts.AttrRequiredSignsID);
                if (attributeById != null)
                  xml = attributeById.Value.ToString();
              }
              if (!string.IsNullOrEmpty(xml))
              {
                RequiredSigns requiredSigns = new RequiredSigns(xml);
                if (graphsCollection == null)
                  graphsCollection = new GraphsCollection();
                foreach (string graphs in requiredSigns.GraphsSet)
                {
                  foreach (GraphClass graphClass in requiredSigns.GraphsSet[graphs])
                  {
                    if (!graphsCollection.Contains(graphClass))
                      graphsCollection.Add(graphClass);
                  }
                }
              }
            }
          }
        }
      }
    }
    if (graphsCollection == null)
      return (GraphsSet) null;
    GraphsSet graphsToSign = new GraphsSet();
    graphsToSign.Add("0", graphsCollection);
    return graphsToSign;
  }

  public DateTime CalcPeriod(Guid SessionGuid, DateTime fromTime, TimeUnits units, int unitsCount)
  {
    IUserSession sessionById = UserSession.GetSessionByID(SessionGuid);
    if (sessionById != null)
      fromTime = new PeriodInformation(sessionById)
      {
        Units = units,
        UnitsCount = unitsCount
      }.GetExecTime(fromTime);
    return fromTime;
  }
}
