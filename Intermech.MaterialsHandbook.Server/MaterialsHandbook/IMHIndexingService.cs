// Decompiled with JetBrains decompiler
// Type: Intermech.MaterialsHandbook.IMHIndexingService
// Assembly: Intermech.MaterialsHandbook.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 415584AC-BDF0-4945-B0B3-EBEC9DE4A5E1
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.MaterialsHandbook.Server.dll

using Intermech.Imbase;
using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.MaterialsHandbook;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;

#nullable disable
namespace Intermech.MaterialsHandbook;

public class IMHIndexingService : LongLifeObject, IIMHIndexingService
{
  private const string COL_CLASS = "ClassName";
  private int _completed;
  private IMHIndexingService.IndexAction _currentAction;
  private string _msg = string.Empty;
  private string _currentUserName = string.Empty;
  private Queue<IMHIndexingService.QueueIndexData> _queue = new Queue<IMHIndexingService.QueueIndexData>();
  private Thread _thread;
  private long _currentTableID;
  private bool _isBreaked;

  public IMHIndexingService()
  {
    if (!(ServerServices.GetService(typeof (IEventLogHelper)) is IEventLogHelper service))
      return;
    service.AddAttributeWriteHandler((object) Intermech.Imbase.Consts.ImbaseTableDataAttID, new WriteAttributeValueHandler(this.WritePathAttributeValueHandler));
  }

  private void WritePathAttributeValueHandler(IDBAttribute attribute, AttributeValueEventArgs args)
  {
    if (!(attribute is DBAttribute dbAttribute))
      return;
    QuickObjectInfo objectInfo = dbAttribute.Session.GetObjectInfo(attribute.DBObjectID);
    if (objectInfo.Empty || objectInfo.ObjectTypeID != Intermech.Imbase.Consts.ImbaseTableTypeID)
      return;
    long dbObjectId = attribute.DBObjectID;
    if (dbObjectId == 0L)
      return;
    string userName = dbAttribute.Session.UserName;
    IMHIndexingService.QueueIndexData queueIndexData = new IMHIndexingService.QueueIndexData(dbObjectId, userName);
    if (this._queue.Contains(queueIndexData))
      return;
    this._isBreaked = dbObjectId == this._currentTableID;
    this._queue.Enqueue(queueIndexData);
    if (this._currentAction != IMHIndexingService.IndexAction.None)
      return;
    this._thread = new Thread(new ThreadStart(this.ExecuteIndexing))
    {
      Priority = ThreadPriority.Lowest,
      Name = "IMHIndexing",
      IsBackground = true
    };
    this._thread.Start();
  }

  public int Completed => this._completed;

  public bool IsBusy => this._currentAction != 0;

  public string Msg
  {
    get
    {
      this.SetMessageForBusyProcess();
      return this._msg;
    }
  }

  public void Add(Guid sessionGuid, long sourceID, Dictionary<string, List<Guid>> attrs)
  {
    if (attrs == null || attrs.Count <= 0)
      return;
    UserSession session = this.GetSession(sessionGuid);
    IDbManager dataManager = session.DataManager;
    if (dataManager == null)
      throw new Exception(IMHIndexingService.ExceptionMessages.NullDBManager);
    string classifKeyByObjId = ImbaseHelper.GetClassifKeyByObjID((IUserSession) session, sourceID);
    if (string.IsNullOrEmpty(classifKeyByObjId))
      return;
    string[] strArray = new string[attrs.Count];
    attrs.Keys.CopyTo(strArray, 0);
    DataTable linkIds = this.GetLinkIDs((IUserSession) session, classifKeyByObjId, strArray);
    if (linkIds == null)
      throw new Exception(IMHIndexingService.ExceptionMessages.NullCatalogsTables);
    if (!this.IsBusy)
    {
      try
      {
        this._currentAction = IMHIndexingService.IndexAction.Add;
        this.RemoveIndexesAndData(session, dataManager, sourceID, attrs);
        this.AddIndexes(session, dataManager, sourceID, classifKeyByObjId, attrs);
        int num = 0;
        foreach (DataRow row in (InternalDataCollectionBase) linkIds.Rows)
        {
          if (!this.IsBusy)
            break;
          long int64_1 = Convert.ToInt64(row["F_LINK_ID"]);
          long int64_2 = Convert.ToInt64(row["F_TABLE_ID"]);
          string str = row["ClassName"].ToString();
          if (attrs.ContainsKey(str))
          {
            this.AddIndexesData(session, dataManager, sourceID, int64_1, int64_2, str, attrs[str]);
            this._completed = Convert.ToInt32(Math.Floor((double) ++num / Convert.ToDouble(linkIds.Rows.Count) * 100.0));
          }
        }
      }
      finally
      {
        this.MarkAsFree();
      }
    }
    else
      this.SetMessageForBusyProcess();
  }

  public void IndexingMaterial(Guid sessionGuid, long sourceID)
  {
    if (sourceID == 0L)
      return;
    UserSession session = this.GetSession(sessionGuid);
    IDbManager dataManager = session.DataManager;
    if (dataManager == null)
      throw new Exception(IMHIndexingService.ExceptionMessages.NullDBManager);
    string classifKeyByObjId = ImbaseHelper.GetClassifKeyByObjID((IUserSession) session, sourceID);
    if (string.IsNullOrEmpty(classifKeyByObjId))
      return;
    DataTable linkIds = this.GetLinkIDs((IUserSession) session, classifKeyByObjId, (string[]) null);
    if (linkIds == null)
      throw new Exception(IMHIndexingService.ExceptionMessages.NullCatalogsTables);
    if (!this.IsBusy)
    {
      try
      {
        this._currentAction = IMHIndexingService.IndexAction.Add;
        Guid attrGuid = new Guid("cad00020-306c-11d8-b4e9-00304f19f545");
        this.RemoveIndexAndData(session, dataManager, sourceID, attrGuid);
        this.AddIndex(session, dataManager, sourceID, classifKeyByObjId, attrGuid);
        int num = 0;
        foreach (DataRow row in (InternalDataCollectionBase) linkIds.Rows)
        {
          if (!this.IsBusy)
            break;
          long int64 = Convert.ToInt64(row["F_LINK_ID"]);
          this.AddMaterialsIndexesData(session, dataManager, sourceID, int64, attrGuid);
          this._completed = Convert.ToInt32(Math.Floor((double) ++num / Convert.ToDouble(linkIds.Rows.Count) * 100.0));
        }
      }
      finally
      {
        this.MarkAsFree();
      }
    }
    else
      this.SetMessageForBusyProcess();
  }

  public void MarkAsFree()
  {
    this._msg = string.Empty;
    this._currentAction = IMHIndexingService.IndexAction.None;
    this._currentTableID = 0L;
    this._currentUserName = string.Empty;
    this._isBreaked = false;
  }

  public void RemoveIndexes(Guid sessionGuid, long sourceID, Dictionary<string, List<Guid>> attrs)
  {
    if (attrs == null || attrs.Count <= 0)
      return;
    UserSession session = this.GetSession(sessionGuid);
    IDbManager dataManager = session.DataManager;
    if (dataManager == null)
      throw new Exception(IMHIndexingService.ExceptionMessages.NullDBManager);
    if (!this.IsBusy)
    {
      this._currentAction = IMHIndexingService.IndexAction.Del;
      string str = $"DELETE FROM IMS_IMH_INDEX  WHERE {IndexesField.F_SOURCE_ID}=:sourceID AND {IndexesField.F_CLASS_NAME}=:className AND {IndexesField.F_ATTRIBUTE_ID} IN ";
      session.StartTransaction();
      try
      {
        foreach (KeyValuePair<string, List<Guid>> attr in attrs)
        {
          if (attr.Value != null && attr.Value.Count != 0)
          {
            List<IDbDataParameter> pars = new List<IDbDataParameter>((IEnumerable<IDbDataParameter>) new IDbDataParameter[2]
            {
              dataManager.Parameter(":sourceID", (object) sourceID),
              dataManager.Parameter(":className", (object) attr.Key)
            });
            string paramsRange = this.CreateParamsRange(dataManager, attr.Value, pars);
            dataManager.ExecuteNonQuery($"{str} {paramsRange}", pars.ToArray());
          }
        }
        if (this.IsBusy)
          session.Commit();
        else
          session.Rollback();
      }
      catch (Exception ex)
      {
        session.Rollback();
        throw;
      }
      finally
      {
        this.MarkAsFree();
      }
    }
    else
      this.SetMessageForBusyProcess();
  }

  public void RemoveObject(Guid sessionGuid, long sourceID, bool isTable)
  {
    UserSession session = this.GetSession(sessionGuid);
    IDbManager dataManager = session.DataManager;
    if (dataManager == null)
      throw new Exception(IMHIndexingService.ExceptionMessages.NullDBManager);
    if (!this.IsBusy)
    {
      this._currentAction = IMHIndexingService.IndexAction.Del;
      if (isTable)
      {
        List<long> linkIds = this.GetLinkIDs((IUserSession) session, sourceID);
        this.RemoveDataForTable(session, dataManager, linkIds);
      }
      else
      {
        session.StartTransaction();
        try
        {
          dataManager.ExecuteNonQuery($"DELETE FROM IMS_IMH_INDEX WHERE {IndexesField.F_LINK_ID}=:sourceID", dataManager.Parameter(":sourceID", (object) sourceID));
          if (this.IsBusy)
            session.Commit();
          else
            session.Rollback();
        }
        catch (Exception ex)
        {
          session.Rollback();
          throw;
        }
        finally
        {
          this.MarkAsFree();
        }
      }
    }
    else
      this.SetMessageForBusyProcess();
  }

  public DataTable Search(
    Guid sessionGuid,
    long sourceID,
    Guid attrGuid,
    string[] colsNames,
    string request,
    SearchesAccuracy sa)
  {
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID(attrGuid);
    List<long> catalogIDs = new List<long>();
    if (sourceID != 0L)
      catalogIDs.Add(sourceID);
    return ServerServices.GetService(typeof (IImbaseIndexingService)) is IImbaseIndexingService service ? service.Search(sessionGuid, catalogIDs, attributeTypeId, colsNames, request, sa) : (DataTable) null;
  }

  public List<long> SearchAssortmentData(
    Guid sessionGuid,
    long sourceID,
    string className,
    List<ConditionClass> conditions)
  {
    List<long> longList = new List<long>();
    if (sourceID != 0L && !string.IsNullOrEmpty(className) && conditions != null && conditions.Count > 0)
    {
      UserSession session = this.GetSession(sessionGuid);
      IDbManager dataManager = session.DataManager;
      if (dataManager == null)
        throw new Exception(IMHIndexingService.ExceptionMessages.NullDBManager);
      List<IMHIndexingService.InfoClass> idsForConditions = this.GetAttrIDsForConditions(session, className, ref conditions);
      List<string> tableAliases = new List<string>(idsForConditions.Count);
      for (int index = 0; index < idsForConditions.Count; ++index)
        tableAliases.Add($"tbl{index}");
      bool flag;
      do
      {
        foreach (IMHIndexingService.InfoClass infoClass in idsForConditions)
          infoClass.CC.Alias = infoClass.AttrIDs[infoClass.Index].ToString();
        DataTable dataTable = this.SearchAssortment(dataManager, sourceID, className, tableAliases, conditions);
        if (dataTable != null && dataTable.Rows.Count > 0)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            long int64 = Convert.ToInt64(row[0]);
            if (!longList.Contains(int64))
              longList.Add(int64);
          }
        }
        flag = true;
        foreach (IMHIndexingService.InfoClass infoClass in idsForConditions)
        {
          ++infoClass.Index;
          if (infoClass.Index < infoClass.AttrIDs.Count)
          {
            flag = false;
            break;
          }
          infoClass.Index = 0;
        }
      }
      while (!flag);
    }
    return longList;
  }

  public bool UpdateDataByTableID(Guid sessionGuid, long tableID)
  {
    bool flag = true;
    if (tableID != 0L)
    {
      UserSession session = this.GetSession(sessionGuid);
      this._currentAction = IMHIndexingService.IndexAction.Update;
      this._currentTableID = tableID;
      this._currentUserName = session.UserName;
      flag = this.UpdateDataByTableID(session, tableID);
    }
    return flag;
  }

  public void UpdateIndexes(
    Guid sessionGuid,
    long sourceID,
    Dictionary<string, List<Guid>> addedAttrs,
    Dictionary<string, List<Guid>> deletedAttrs)
  {
    if (deletedAttrs != null && deletedAttrs.Count > 0)
      this.RemoveIndexes(sessionGuid, sourceID, deletedAttrs);
    if (addedAttrs == null || addedAttrs.Count <= 0)
      return;
    this.Add(sessionGuid, sourceID, addedAttrs);
  }

  private bool AddDoubleData(
    UserSession session,
    IDbManager iDBManager,
    string strAttrID,
    DataTable dtData,
    string sql,
    IDbDataParameter[] pars)
  {
    bool flag = true;
    if (dtData.Columns.Contains(strAttrID))
    {
      session.StartTransaction();
      try
      {
        foreach (DataRow row in (InternalDataCollectionBase) dtData.Rows)
        {
          if (this._isBreaked)
          {
            flag = false;
            break;
          }
          object obj = row[strAttrID];
          string str = obj == null || obj == DBNull.Value ? string.Empty : obj.ToString();
          string indexedString = session.StringNormalizer.GetIndexedString(str);
          pars[5] = iDBManager.Parameter(":tabKey", row["-2"]);
          pars[6] = iDBManager.Parameter(":text", (object) str);
          pars[7] = iDBManager.Parameter(":hashText", (object) indexedString);
          pars[8] = iDBManager.Parameter(":integerValue", (object) DBNull.Value);
          double result;
          pars[9] = !double.TryParse(str, out result) ? iDBManager.Parameter(":doubleValue", (object) DBNull.Value) : iDBManager.Parameter(":doubleValue", (object) result);
          iDBManager.ExecuteNonQuery(sql, pars);
        }
        if (this.IsBusy & flag)
          session.Commit();
        else
          session.Rollback();
      }
      catch (Exception ex)
      {
        session.Rollback();
        throw;
      }
    }
    return flag;
  }

  private void AddIndex(
    UserSession session,
    IDbManager mngr,
    long sourceID,
    string classifKey,
    Guid attrGuid)
  {
    string commandText = $"INSERT INTO IMS_IMH_INDEX ({IndexesField.F_ATTRIBUTE_ID}, {IndexesField.F_SOURCE_ID}, {IndexesField.F_TABKEY}, {IndexesField.F_LINK_ID}, F_CLASSIVKEY) " + " VALUES (:a_ID, :sourceID, :tabKey, :l_ID, :classifKey)";
    IDbDataParameter[] dbDataParameterArray = new IDbDataParameter[5]
    {
      mngr.Parameter(":a_ID", (object) MetaDataHelper.GetAttributeTypeID(attrGuid)),
      mngr.Parameter(":sourceID", (object) sourceID),
      mngr.Parameter(":tabKey", (object) -1),
      mngr.Parameter(":l_ID", (object) -1),
      mngr.Parameter(":classifKey", (object) classifKey)
    };
    session.StartTransaction();
    try
    {
      mngr.ExecuteNonQuery(commandText, dbDataParameterArray);
      if (this.IsBusy)
        session.Commit();
      else
        session.Rollback();
    }
    catch (Exception ex)
    {
      session.Rollback();
      throw;
    }
  }

  private void AddIndexes(
    UserSession session,
    IDbManager mngr,
    long sourceID,
    string classifKey,
    Dictionary<string, List<Guid>> attrs)
  {
    string commandText = $"INSERT INTO IMS_IMH_INDEX ({IndexesField.F_ATTRIBUTE_ID}, {IndexesField.F_SOURCE_ID}, {IndexesField.F_CLASS_NAME}, {IndexesField.F_TABKEY}, {IndexesField.F_LINK_ID}, F_CLASSIVKEY) " + " VALUES (:a_ID, :sourceID, :className, :tabKey, :l_ID, :classifKey)";
    IDbDataParameter[] dbDataParameterArray = new IDbDataParameter[6]
    {
      null,
      mngr.Parameter(":sourceID", (object) sourceID),
      null,
      mngr.Parameter(":tabKey", (object) -1),
      mngr.Parameter(":l_ID", (object) -1),
      mngr.Parameter(":classifKey", (object) classifKey)
    };
    foreach (KeyValuePair<string, List<Guid>> attr in attrs)
    {
      if (attr.Value != null && attr.Value.Count != 0)
      {
        dbDataParameterArray[2] = mngr.Parameter(":className", (object) attr.Key);
        session.StartTransaction();
        try
        {
          foreach (Guid attrTypeGuid in attr.Value)
          {
            dbDataParameterArray[0] = mngr.Parameter(":a_ID", (object) MetaDataHelper.GetAttributeTypeID(attrTypeGuid));
            mngr.ExecuteNonQuery(commandText, dbDataParameterArray);
          }
          if (this.IsBusy)
          {
            session.Commit();
          }
          else
          {
            session.Rollback();
            break;
          }
        }
        catch (Exception ex)
        {
          session.Rollback();
          throw;
        }
      }
    }
  }

  private bool AddIndexesData(
    UserSession session,
    IDbManager mngr,
    long sourceID,
    long l_ID,
    long t_ID,
    string className,
    List<Guid> a_Guids)
  {
    bool flag = true;
    if (a_Guids != null && a_Guids.Count > 0)
    {
      DataTable recordsTable = new DataTable();
      if (session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService)
        customService.LoadRecords(session.SessionGUID, t_ID, string.Empty, Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator, out recordsTable, out AttributeTypeProperties[] _, out ImbaseKeyInfo _);
      if (recordsTable != null && recordsTable.Rows.Count > 0 && recordsTable.DataSet.Tables.Contains("IMS_ATTR_TYPES"))
      {
        string sql = $"INSERT INTO IMS_IMH_INDEX ({IndexesField.F_SOURCE_ID}, {IndexesField.F_CLASS_NAME}, F_CLASSIVKEY, {IndexesField.F_LINK_ID}, {IndexesField.F_ATTRIBUTE_ID}, {IndexesField.F_TABKEY}, {IndexesField.F_TEXT}, {IndexesField.F_HASHTEXT}, {IndexesField.F_INTEGER_VALUE}, {IndexesField.F_DOUBLE_VALUE}) " + " VALUES (:sourceID, :className, :classifKey, :l_ID, :a_ID, :tabKey, :text, :hashText, :integerValue, :doubleValue)";
        IDbDataParameter[] pars = new IDbDataParameter[10];
        pars[0] = mngr.Parameter(":sourceID", (object) sourceID);
        pars[1] = mngr.Parameter(":className", (object) className);
        pars[2] = mngr.Parameter(":classifKey", (object) ImbaseHelper.GetClassifKeyByObjID((IUserSession) session, l_ID));
        pars[3] = mngr.Parameter(":l_ID", (object) l_ID);
        foreach (Guid aGuid in a_Guids)
        {
          if (!this._isBreaked)
          {
            int attributeTypeId = MetaDataHelper.GetAttributeTypeID(aGuid);
            pars[4] = mngr.Parameter(":a_ID", (object) attributeTypeId);
            FieldTypes fieldTypes = FieldTypes.ftUnknown;
            IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(aGuid);
            if (attributeType != null)
              fieldTypes = attributeType.FieldType;
            switch (fieldTypes)
            {
              case FieldTypes.ftInteger:
                flag = this.AddIntegerData(session, mngr, attributeTypeId.ToString(), recordsTable, sql, pars);
                break;
              case FieldTypes.ftDouble:
              case FieldTypes.ftMeasured:
                flag = this.AddDoubleData(session, mngr, attributeTypeId.ToString(), recordsTable, sql, pars);
                break;
              default:
                flag = this.AddOtherData(session, mngr, attributeTypeId.ToString(), recordsTable, sql, pars);
                break;
            }
            if (!flag)
              break;
          }
          else
            break;
        }
      }
    }
    return flag;
  }

  private bool AddIntegerData(
    UserSession session,
    IDbManager iDBManager,
    string strAttrID,
    DataTable dtData,
    string sql,
    IDbDataParameter[] pars)
  {
    bool flag = true;
    if (dtData.Columns.Contains(strAttrID))
    {
      session.StartTransaction();
      try
      {
        foreach (DataRow row in (InternalDataCollectionBase) dtData.Rows)
        {
          if (this._isBreaked)
          {
            flag = false;
            break;
          }
          object obj = row[strAttrID];
          string str = obj == null || obj == DBNull.Value ? string.Empty : obj.ToString();
          string indexedString = session.StringNormalizer.GetIndexedString(str);
          pars[5] = iDBManager.Parameter(":tabKey", row["-2"]);
          pars[6] = iDBManager.Parameter(":text", (object) str);
          pars[7] = iDBManager.Parameter(":hashText", (object) indexedString);
          long result;
          pars[8] = !long.TryParse(str, out result) ? iDBManager.Parameter(":integerValue", (object) DBNull.Value) : iDBManager.Parameter(":integerValue", (object) result);
          pars[9] = iDBManager.Parameter(":doubleValue", (object) DBNull.Value);
          iDBManager.ExecuteNonQuery(sql, pars);
        }
        if (this.IsBusy & flag)
          session.Commit();
        else
          session.Rollback();
      }
      catch (Exception ex)
      {
        session.Rollback();
        throw;
      }
    }
    return flag;
  }

  private void AddMaterialsIndexesData(
    UserSession session,
    IDbManager mngr,
    long sourceID,
    long l_ID,
    Guid attrGuid)
  {
    DataTable recordsTable = new DataTable();
    if (session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService)
      customService.LoadRecords(session.SessionGUID, l_ID, string.Empty, Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator, out recordsTable, out AttributeTypeProperties[] _, out ImbaseKeyInfo _);
    if (recordsTable == null || recordsTable.Rows.Count <= 0)
      return;
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID(attrGuid);
    string sql = $"INSERT INTO IMS_IMH_INDEX ({IndexesField.F_SOURCE_ID}, {IndexesField.F_CLASS_NAME}, F_CLASSIVKEY, {IndexesField.F_LINK_ID}, {IndexesField.F_ATTRIBUTE_ID}, {IndexesField.F_TABKEY}, {IndexesField.F_TEXT}, {IndexesField.F_HASHTEXT}, {IndexesField.F_INTEGER_VALUE}, {IndexesField.F_DOUBLE_VALUE}) " + " VALUES (:sourceID, :className, :classifKey, :l_ID, :a_ID, :tabKey, :text, :hashText, :integerValue, :doubleValue)";
    IDbDataParameter[] pars = new IDbDataParameter[10]
    {
      mngr.Parameter(":sourceID", (object) sourceID),
      mngr.Parameter(":className", (object) string.Empty),
      mngr.Parameter(":classifKey", (object) ImbaseHelper.GetClassifKeyByObjID((IUserSession) session, l_ID)),
      mngr.Parameter(":l_ID", (object) l_ID),
      mngr.Parameter(":a_ID", (object) attributeTypeId),
      null,
      null,
      null,
      null,
      null
    };
    this.AddOtherData(session, mngr, attributeTypeId.ToString(), recordsTable, sql, pars);
  }

  private bool AddOtherData(
    UserSession session,
    IDbManager iDBManager,
    string strAttrID,
    DataTable dtData,
    string sql,
    IDbDataParameter[] pars)
  {
    bool flag = true;
    if (dtData.Columns.Contains(strAttrID))
    {
      session.StartTransaction();
      try
      {
        foreach (DataRow row in (InternalDataCollectionBase) dtData.Rows)
        {
          if (this._isBreaked)
          {
            flag = false;
            break;
          }
          object obj = row[strAttrID];
          string str_to_index = obj == null || obj == DBNull.Value ? string.Empty : obj.ToString();
          string indexedString = session.StringNormalizer.GetIndexedString(str_to_index);
          pars[5] = iDBManager.Parameter(":tabKey", row["-2"]);
          pars[6] = iDBManager.Parameter(":text", (object) str_to_index);
          pars[7] = iDBManager.Parameter(":hashText", (object) indexedString);
          pars[8] = iDBManager.Parameter(":integerValue", (object) DBNull.Value);
          pars[9] = iDBManager.Parameter(":doubleValue", (object) DBNull.Value);
          iDBManager.ExecuteNonQuery(sql, pars);
        }
        if (this.IsBusy & flag)
          session.Commit();
        else
          session.Rollback();
      }
      catch (Exception ex)
      {
        session.Rollback();
        throw;
      }
    }
    return flag;
  }

  private List<int> ConvertAttrGuidsToAttrIDs(List<string> attrGuids)
  {
    List<int> attrIds = (List<int>) null;
    if (attrGuids != null)
    {
      attrIds = new List<int>(attrGuids.Count);
      foreach (string attrGuid in attrGuids)
      {
        if (GuidHelper.IsGuid(attrGuid))
        {
          int attributeTypeId = MetaDataHelper.GetAttributeTypeID(attrGuid);
          if (!attrIds.Contains(attributeTypeId))
            attrIds.Add(attributeTypeId);
        }
      }
    }
    return attrIds;
  }

  private string CreateParamsRange(
    IDbManager manager,
    List<Guid> attrs,
    List<IDbDataParameter> pars)
  {
    string format = "({0})";
    for (int index = 0; index < attrs.Count; ++index)
    {
      int attributeTypeId = MetaDataHelper.GetAttributeTypeID(attrs[index]);
      string parameterName = $":par{index}";
      pars.Add(manager.Parameter(parameterName, (object) attributeTypeId));
      format = string.Format(format, (object) (parameterName + " {0}"));
    }
    return format.Replace(" :", ", :").Replace(" {0}", "");
  }

  private string CreateParamsRange(IDbManager manager, List<long> IDs, List<IDbDataParameter> pars)
  {
    string format = "({0})";
    for (int index = 0; index < IDs.Count; ++index)
    {
      string parameterName = $":par{index}";
      pars.Add(manager.Parameter(parameterName, (object) IDs[index]));
      format = string.Format(format, (object) (parameterName + " {0}"));
    }
    return format.Replace(" :", ", :").Replace(" {0}", "");
  }

  private string CreateParamsRange(
    IDbManager manager,
    List<int> attrs,
    List<IDbDataParameter> pars)
  {
    string format = "({0})";
    for (int index = 0; index < attrs.Count; ++index)
    {
      string parameterName = $":par{index}";
      pars.Add(manager.Parameter(parameterName, (object) attrs[index]));
      format = string.Format(format, (object) (parameterName + " {0}"));
    }
    return format.Replace(" :", ", :").Replace(" {0}", "");
  }

  private void ExecuteIndexing()
  {
    session = (UserSession) null;
    try
    {
      if (!((ServerServices.GetService(typeof (IDBTimedEvents)) is IDBTimedEvents service ? service.GetSystemSessionTemporaryClone("IMH.ExecuteIndexing") : (IUserSession) null) is UserSession session))
        return;
      try
      {
        this._currentAction = IMHIndexingService.IndexAction.Update;
        while (this._queue.Count > 0)
        {
          IMHIndexingService.QueueIndexData queueIndexData = this._queue.Dequeue();
          this._currentTableID = queueIndexData.TableID;
          this._currentUserName = queueIndexData.UserName;
          this.UpdateDataByTableID(session, this._currentTableID);
          this._isBreaked = false;
        }
      }
      catch (Exception ex)
      {
      }
      finally
      {
        this.MarkAsFree();
      }
    }
    finally
    {
      session?.Logout("IMH.ExecuteIndexing");
    }
  }

  private List<IMHIndexingService.InfoClass> GetAttrIDsForConditions(
    UserSession session,
    string className,
    ref List<ConditionClass> conditions)
  {
    List<IMHIndexingService.InfoClass> idsForConditions = (List<IMHIndexingService.InfoClass>) null;
    IMHAssortmentClass classSettings = this.GetClassSettings(session, className);
    if (classSettings != null)
    {
      idsForConditions = new List<IMHIndexingService.InfoClass>(conditions.Count);
      List<ConditionClass> conditionClassList = new List<ConditionClass>(conditions.Count);
      foreach (ConditionClass cc in conditions)
      {
        if (classSettings.Parameters.ContainsKey(cc.Alias))
        {
          List<int> attrIds = this.ConvertAttrGuidsToAttrIDs(classSettings.Parameters[cc.Alias]);
          if (attrIds != null && attrIds.Count != 0)
          {
            idsForConditions.Add(new IMHIndexingService.InfoClass(cc, attrIds));
            conditionClassList.Add(cc);
          }
        }
      }
      conditions = conditionClassList;
    }
    return idsForConditions;
  }

  private IMHAssortmentClass GetClassSettings(UserSession session, string className)
  {
    IMHAssortmentClass classSettings = (IMHAssortmentClass) null;
    if (session.GetCustomService(typeof (IIMHSystemSettingsService)) is IIMHSystemSettingsService customService)
    {
      List<IMHAssortmentClass> assortmentSearchSettings = customService.GetSystemSettings()?.AssortmentSearchSettings;
      if (assortmentSearchSettings == null)
        return (IMHAssortmentClass) null;
      foreach (IMHAssortmentClass imhAssortmentClass in assortmentSearchSettings)
      {
        if (!(imhAssortmentClass.Name != className))
        {
          classSettings = imhAssortmentClass;
          break;
        }
      }
    }
    return classSettings;
  }

  private DataTable GetIndexedLinkIDs(IDbManager mngr, List<long> linkIDs)
  {
    DataTable indexedLinkIds = (DataTable) null;
    if (linkIDs.Count > 0)
    {
      List<IDbDataParameter> pars = new List<IDbDataParameter>();
      string strFromArr = this.GetStrFromArr(new string[3]
      {
        IndexesField.F_LINK_ID,
        IndexesField.F_SOURCE_ID,
        IndexesField.F_CLASS_NAME
      }, "*");
      string paramsRange = this.CreateParamsRange(mngr, linkIDs, pars);
      string commandText = $"SELECT {strFromArr} FROM IMS_IMH_INDEX WHERE {IndexesField.F_LINK_ID} IN {paramsRange} GROUP BY {strFromArr}";
      indexedLinkIds = mngr.ExecuteDataTable(commandText, pars.ToArray());
    }
    return indexedLinkIds;
  }

  private List<Guid> GetIndexes(
    IDbManager mngr,
    long sourceID,
    string className,
    List<int> attrsIDs)
  {
    List<Guid> indexes = (List<Guid>) null;
    List<IDbDataParameter> pars = new List<IDbDataParameter>((IEnumerable<IDbDataParameter>) new IDbDataParameter[3]
    {
      mngr.Parameter(":sourceID", (object) sourceID),
      mngr.Parameter(":className", (object) className),
      mngr.Parameter(":l_ID", (object) -1)
    });
    string str = $"{$"SELECT {IndexesField.F_ATTRIBUTE_ID} FROM IMS_IMH_INDEX "} WHERE {IndexesField.F_SOURCE_ID}=:sourceID AND {IndexesField.F_CLASS_NAME}=:className AND {IndexesField.F_LINK_ID}=:l_ID AND {IndexesField.F_ATTRIBUTE_ID} IN ";
    string paramsRange = this.CreateParamsRange(mngr, attrsIDs, pars);
    DataTable dataTable = mngr.ExecuteDataTable($"{str} {paramsRange}", pars.ToArray());
    if (dataTable != null && dataTable.Rows.Count > 0)
    {
      indexes = new List<Guid>(dataTable.Rows.Count);
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        Guid attributeTypeGuid = MetaDataHelper.GetAttributeTypeGuid(Convert.ToInt32(row[IndexesField.F_ATTRIBUTE_ID]));
        if (!(attributeTypeGuid == Guid.Empty) && !indexes.Contains(attributeTypeGuid))
          indexes.Add(attributeTypeGuid);
      }
    }
    return indexes;
  }

  private DataTable GetLinkIDs(IUserSession session, string classifKey, string[] classes)
  {
    IDBObjectCollection objectCollection = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableRefTypeID);
    List<ConditionStructure> conditionStructureList = new List<ConditionStructure>(3)
    {
      new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.StartString, (object) classifKey, LogicalOperators.AND, 0, true)
    };
    List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>(3)
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) Intermech.Imbase.Consts.ImbaseTableRefAttID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0)
    };
    if (classes != null && classes.Length != 0)
    {
      conditionStructureList.Add(new ConditionStructure(Intermech.Imbase.Consts.ImbaseTableRefAttID, RelationalOperators.NotEmpty, (object) null, LogicalOperators.AND, 0, true));
      conditionStructureList.Add(new ConditionStructure(Intermech.Imbase.Consts.ClassAttrID, RelationalOperators.In, (object) classes, LogicalOperators.NONE, 0, true));
      columnDescriptorList.Add(new ColumnDescriptor((object) Intermech.Imbase.Consts.ClassAttrID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0));
    }
    else
      conditionStructureList.Add(new ConditionStructure(Intermech.Imbase.Consts.ImbaseTableRefAttID, RelationalOperators.NotEmpty, (object) null, LogicalOperators.NONE, 0, true));
    DBRecordSetParams paramSet = new DBRecordSetParams(conditionStructureList.ToArray(), columnDescriptorList.ToArray());
    DataTable linkIds = objectCollection.Select(paramSet);
    if (linkIds != null)
    {
      if (linkIds.Rows.Count > 0)
      {
        linkIds.Columns[-2.ToString()].ColumnName = "F_LINK_ID";
        linkIds.Columns[Intermech.Imbase.Consts.ImbaseTableRefAttID.ToString()].ColumnName = "F_TABLE_ID";
        if (classes != null)
          linkIds.Columns[Intermech.Imbase.Consts.ClassAttrID.ToString()].ColumnName = "ClassName";
      }
      else
        linkIds = (DataTable) null;
    }
    return linkIds;
  }

  private List<long> GetLinkIDs(IUserSession session, long tableID)
  {
    List<long> linkIds = (List<long>) null;
    DataTable dataTable = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableRefTypeID).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(Intermech.Imbase.Consts.ImbaseTableRefAttID, RelationalOperators.Equal, (object) tableID, (object) null, LogicalOperators.NONE, 0, true, AttributeSourceTypes.Auto, ColumnContents.ID)
    }, new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
    }));
    if (dataTable != null && dataTable.Rows.Count > 0)
    {
      linkIds = new List<long>(dataTable.Rows.Count);
      string columnName = -2.ToString();
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        long int64 = Convert.ToInt64(row[columnName]);
        if (!linkIds.Contains(int64))
          linkIds.Add(int64);
      }
    }
    return linkIds;
  }

  private UserSession GetSession(Guid sessionGuid)
  {
    session = (UserSession) null;
    if (sessionGuid != Guid.Empty)
      this._currentUserName = UserSession.GetSessionByID(sessionGuid) is UserSession session ? session.UserName : (string) null;
    if (session == null)
    {
      this._currentUserName = string.Empty;
      throw new ArgumentException(string.Format(LocalizationHolder.rm.GetString("IMH_WrongSessionID"), (object) sessionGuid.ToString()), nameof (sessionGuid));
    }
    return session;
  }

  private string GetStrFromArr(string[] arr, string ifEmpty)
  {
    string strFromArr = ifEmpty;
    if (arr != null && arr.Length != 0)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (string str in arr)
      {
        stringBuilder.Append(str);
        stringBuilder.Append(", ");
      }
      stringBuilder.Remove(stringBuilder.Length - 2, 2);
      strFromArr = stringBuilder.ToString();
    }
    return strFromArr;
  }

  private string GetStrRelOperator(RelationalOperators relOperator)
  {
    string strRelOperator = string.Empty;
    switch (relOperator)
    {
      case RelationalOperators.Equal:
        strRelOperator = "=";
        break;
      case RelationalOperators.NotEqual:
        strRelOperator = "<>";
        break;
      case RelationalOperators.Greater:
        strRelOperator = ">";
        break;
      case RelationalOperators.GreaterOrEqual:
        strRelOperator = ">=";
        break;
      case RelationalOperators.Less:
        strRelOperator = "<";
        break;
      case RelationalOperators.LessOrEqual:
        strRelOperator = "<=";
        break;
    }
    return strRelOperator;
  }

  private List<int> GetTableAttrCollection(UserSession session, long tableID)
  {
    List<int> tableAttrCollection = (List<int>) null;
    if (tableID != 0L)
    {
      DataSet tables = TableLoadHelper.GetTables((IUserSession) session, tableID, false);
      if (tables != null && tables.Tables.Contains("IMS_ATTR_TYPES"))
      {
        DataTable table = tables.Tables["IMS_ATTR_TYPES"];
        tableAttrCollection = new List<int>(table.Rows.Count);
        foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
        {
          int attributeTypeId = MetaDataHelper.GetAttributeTypeID(row["F_ATTRIBUTE_GUID"].ToString());
          if (!tableAttrCollection.Contains(attributeTypeId))
            tableAttrCollection.Add(attributeTypeId);
        }
      }
    }
    return tableAttrCollection;
  }

  private void RemoveDataForTable(UserSession session, IDbManager mngr, List<long> linkIDs)
  {
    if (linkIDs == null || linkIDs.Count <= 0)
      return;
    session.StartTransaction();
    try
    {
      List<IDbDataParameter> pars = new List<IDbDataParameter>();
      string paramsRange = this.CreateParamsRange(mngr, linkIDs, pars);
      mngr.ExecuteNonQuery($"DELETE FROM IMS_IMH_INDEX WHERE {IndexesField.F_LINK_ID} IN {paramsRange}", pars.ToArray());
      if (this.IsBusy)
        session.Commit();
      else
        session.Rollback();
    }
    catch (Exception ex)
    {
      session.Rollback();
      throw;
    }
  }

  private void RemoveIndexAndData(
    UserSession session,
    IDbManager mngr,
    long sourceID,
    Guid attrGuid)
  {
    string commandText = $"DELETE FROM IMS_IMH_INDEX  WHERE {IndexesField.F_SOURCE_ID}=:sourceID AND {IndexesField.F_ATTRIBUTE_ID}=:a_ID ";
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID(attrGuid);
    List<IDbDataParameter> dbDataParameterList = new List<IDbDataParameter>((IEnumerable<IDbDataParameter>) new IDbDataParameter[2]
    {
      mngr.Parameter(":sourceID", (object) sourceID),
      mngr.Parameter(":a_ID", (object) attributeTypeId)
    });
    session.StartTransaction();
    try
    {
      mngr.ExecuteNonQuery(commandText, dbDataParameterList.ToArray());
      if (this.IsBusy)
        session.Commit();
      else
        session.Rollback();
    }
    catch (Exception ex)
    {
      session.Rollback();
      throw;
    }
  }

  private void RemoveIndexesAndData(
    UserSession session,
    IDbManager mngr,
    long sourceID,
    Dictionary<string, List<Guid>> attrs)
  {
    string str = $"DELETE FROM IMS_IMH_INDEX  WHERE {IndexesField.F_SOURCE_ID}=:sourceID AND {IndexesField.F_CLASS_NAME}=:className AND {IndexesField.F_ATTRIBUTE_ID} IN ";
    foreach (KeyValuePair<string, List<Guid>> attr in attrs)
    {
      if (attr.Value != null && attr.Value.Count != 0)
      {
        List<IDbDataParameter> pars = new List<IDbDataParameter>((IEnumerable<IDbDataParameter>) new IDbDataParameter[2]
        {
          mngr.Parameter(":sourceID", (object) sourceID),
          mngr.Parameter(":className", (object) attr.Key)
        });
        string paramsRange = this.CreateParamsRange(mngr, attr.Value, pars);
        session.StartTransaction();
        try
        {
          mngr.ExecuteNonQuery($"{str} {paramsRange}", pars.ToArray());
          if (this.IsBusy)
          {
            session.Commit();
          }
          else
          {
            session.Rollback();
            break;
          }
        }
        catch (Exception ex)
        {
          session.Rollback();
          throw;
        }
      }
    }
  }

  private DataTable SearchAssortment(
    IDbManager mngr,
    long sourceID,
    string className,
    List<string> tableAliases,
    List<ConditionClass> conditions)
  {
    string str1 = $"SELECT {tableAliases[0]}.{IndexesField.F_LINK_ID} ";
    StringBuilder stringBuilder1 = new StringBuilder("FROM ");
    foreach (string tableAlias in tableAliases)
    {
      stringBuilder1.Append("IMS_IMH_INDEX ");
      stringBuilder1.Append(tableAlias);
      stringBuilder1.Append(", ");
    }
    stringBuilder1.Remove(stringBuilder1.Length - 2, 1);
    List<IDbDataParameter> dbDataParameterList = new List<IDbDataParameter>();
    StringBuilder stringBuilder2 = new StringBuilder("WHERE ");
    double num = 1E-10;
    int index1 = 0;
    foreach (ConditionClass condition in conditions)
    {
      if (condition.RelOperator != RelationalOperators.Empty)
      {
        int int32 = Convert.ToInt32(condition.Alias);
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(int32);
        if (attributeType != null)
        {
          string tableAlias = tableAliases[index1];
          string parameterName1 = $":a_ID{index1}";
          string strRelOperator = this.GetStrRelOperator(condition.RelOperator);
          switch (attributeType.FieldType)
          {
            case FieldTypes.ftInteger:
              long result1;
              if (condition.Value != null && long.TryParse(condition.Value.ToString(), out result1))
              {
                string parameterName2 = $":intValue{index1}";
                stringBuilder2.Append(string.Format("{0}.{1}={2} AND {0}.{3}{4}{5} AND ", (object) tableAlias, (object) IndexesField.F_ATTRIBUTE_ID, (object) parameterName1, (object) IndexesField.F_INTEGER_VALUE, (object) strRelOperator, (object) parameterName2));
                dbDataParameterList.Add(mngr.Parameter(parameterName1, (object) int32));
                dbDataParameterList.Add(mngr.Parameter(parameterName2, (object) result1));
                break;
              }
              continue;
            case FieldTypes.ftDouble:
            case FieldTypes.ftMeasured:
              double result2;
              if (condition.Value != null && double.TryParse(condition.Value.ToString(), out result2))
              {
                dbDataParameterList.Add(mngr.Parameter(parameterName1, (object) int32));
                if (condition.RelOperator != RelationalOperators.Equal)
                {
                  string parameterName3 = $":doubleValue{index1}";
                  stringBuilder2.Append(string.Format("{0}.{1}={2} AND {0}.{3}{4}{5} AND ", (object) tableAlias, (object) IndexesField.F_ATTRIBUTE_ID, (object) parameterName1, (object) IndexesField.F_DOUBLE_VALUE, (object) strRelOperator, (object) parameterName3));
                  dbDataParameterList.Add(mngr.Parameter(parameterName3, (object) result2));
                  break;
                }
                string parameterName4 = $":val1{index1}";
                string parameterName5 = $":val2{index1}";
                stringBuilder2.Append(string.Format("{0}.{1}={2} AND {0}.{3} BETWEEN {4} AND {5} AND ", (object) tableAlias, (object) IndexesField.F_ATTRIBUTE_ID, (object) parameterName1, (object) IndexesField.F_DOUBLE_VALUE, (object) parameterName4, (object) parameterName5));
                dbDataParameterList.Add(mngr.Parameter(parameterName4, (object) (result2 - num)));
                dbDataParameterList.Add(mngr.Parameter(parameterName5, (object) (result2 + num)));
                break;
              }
              continue;
            default:
              string str2 = condition.Value?.ToString() ?? string.Empty;
              string parameterName6 = $":hashText{index1}";
              stringBuilder2.Append(string.Format("{0}.{1}={2} AND {0}.{3}{4}{5} AND ", (object) tableAlias, (object) IndexesField.F_ATTRIBUTE_ID, (object) parameterName1, (object) IndexesField.F_HASHTEXT, (object) strRelOperator, (object) parameterName6));
              dbDataParameterList.Add(mngr.Parameter(parameterName1, (object) int32));
              dbDataParameterList.Add(mngr.Parameter(parameterName6, (object) str2));
              break;
          }
          ++index1;
        }
      }
    }
    stringBuilder2.Append(string.Format("{0}.{1}=:sourceID AND {0}.{2}=:className ", (object) tableAliases[0], (object) IndexesField.F_SOURCE_ID, (object) IndexesField.F_CLASS_NAME));
    dbDataParameterList.Add(mngr.Parameter(":sourceID", (object) sourceID));
    dbDataParameterList.Add(mngr.Parameter(":className", (object) className));
    int index2 = 0;
    for (int index3 = 1; index3 < index1; ++index3)
      stringBuilder2.Append(string.Format("AND {0}.{1}={2}.{1} AND {0}.{3}={2}.{3} ", (object) tableAliases[index2], (object) IndexesField.F_LINK_ID, (object) tableAliases[index3], (object) IndexesField.F_TABKEY));
    string str3 = $"GROUP BY {tableAliases[0]}.{IndexesField.F_LINK_ID}";
    string commandText = $"{str1}{stringBuilder1}{stringBuilder2}{str3}";
    return mngr.ExecuteDataTable(commandText, dbDataParameterList.ToArray());
  }

  private void SetMessageForBusyProcess()
  {
    if (this._currentAction == IMHIndexingService.IndexAction.Add)
      this._msg = string.Format(LocalizationHolder.rm.GetString("IMHIndexingService_RunIndexProcess"), (object) this._currentUserName, (object) this.Completed);
    else if (this._currentAction == IMHIndexingService.IndexAction.Del)
    {
      this._msg = string.Format(LocalizationHolder.rm.GetString("IMHIndexingService_DelIndexProcess"), (object) this._currentUserName, (object) this.Completed);
    }
    else
    {
      if (this._currentAction != IMHIndexingService.IndexAction.Update)
        return;
      this._msg = string.Format(LocalizationHolder.rm.GetString("IMHIndexingService_UpdateIndexProcess"), (object) this._currentUserName);
    }
  }

  private bool UpdateDataByTableID(UserSession session, long tableID)
  {
    bool flag = true;
    IDbManager dataManager = session.DataManager;
    if (dataManager == null)
      throw new Exception(IMHIndexingService.ExceptionMessages.NullDBManager);
    try
    {
      List<int> tableAttrCollection = this.GetTableAttrCollection(session, tableID);
      if (tableAttrCollection != null)
      {
        if (tableAttrCollection.Count > 0)
        {
          List<long> linkIds = this.GetLinkIDs((IUserSession) session, tableID);
          if (linkIds != null)
          {
            DataTable indexedLinkIds = this.GetIndexedLinkIDs(dataManager, linkIds);
            if (indexedLinkIds != null)
            {
              if (indexedLinkIds.Rows.Count > 0)
              {
                if (!this._isBreaked)
                {
                  this.RemoveDataForTable(session, dataManager, linkIds);
                  List<Guid> a_Guids = (List<Guid>) null;
                  long sourceID = 0;
                  foreach (DataRow row in (InternalDataCollectionBase) indexedLinkIds.Rows)
                  {
                    if (this._isBreaked)
                    {
                      flag = false;
                      break;
                    }
                    long int64 = Convert.ToInt64(row[IndexesField.F_SOURCE_ID]);
                    string className = row[IndexesField.F_CLASS_NAME].ToString();
                    if (int64 != sourceID)
                    {
                      sourceID = int64;
                      a_Guids = this.GetIndexes(dataManager, sourceID, className, tableAttrCollection);
                      if (a_Guids == null)
                        continue;
                    }
                    flag = this.AddIndexesData(session, dataManager, int64, Convert.ToInt64(row[IndexesField.F_LINK_ID]), tableID, className, a_Guids);
                    if (!flag)
                      break;
                  }
                }
              }
            }
          }
        }
      }
    }
    catch
    {
      flag = false;
    }
    return flag;
  }

  private class ExceptionMessages
  {
    internal static readonly string NullCatalogsTables = LocalizationHolder.rm.GetString("IMHIndexingService_NullCatalogsTables");
    internal static readonly string NullDBManager = LocalizationHolder.rm.GetString("IMHIndexingService_NullDBManager");
  }

  private enum IndexAction
  {
    None,
    Add,
    Del,
    Update,
  }

  private class QueueIndexData
  {
    internal long TableID;
    internal string UserName;

    public QueueIndexData(long tableID, string userName)
    {
      this.TableID = tableID;
      this.UserName = userName;
    }

    public override bool Equals(object obj)
    {
      bool flag = false;
      if (obj != null && obj.GetType() == this.GetType() && obj is IMHIndexingService.QueueIndexData queueIndexData)
        flag = queueIndexData.TableID == this.TableID;
      return flag;
    }

    public override int GetHashCode() => this.TableID.GetHashCode();
  }

  private class InfoClass
  {
    internal ConditionClass CC;
    internal List<int> AttrIDs;
    internal int Index;

    public InfoClass(ConditionClass cc, List<int> attrIDs)
    {
      this.CC = cc;
      this.AttrIDs = attrIDs;
    }
  }
}
