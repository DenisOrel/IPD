// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Server.ECOServer
// Assembly: Intermech.ECO.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E6459663-BB12-41FD-949A-3849B46AE118
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ECO.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.ECO;
using Intermech.Interfaces.LifeCycles;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Server.DelayedNotifications;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;

#nullable disable
namespace Intermech.ECO.Server;

public class ECOServer : LongLifeObject, IECOServer
{
  public static readonly ECOServer ecos;
  public static readonly string ECO_Guid = "cad00348-306c-11d8-b4e9-00304f19f545";
  public static readonly string ECO_II = "cad00349-306c-11d8-b4e9-00304f19f545";
  public static readonly string ECO_PI = "cad0034a-306c-11d8-b4e9-00304f19f545";
  public static readonly string ECO_PR = "cad0034b-306c-11d8-b4e9-00304f19f545";
  public static readonly string ECO_SN = "cadd9bc7-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidObj_DI = "cadd955e-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidObj_DPI = "cadd9560-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidObj_CJ = "cadd9584-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidObjCJRecord = "cadd9588-306c-11d8-b4e9-00304f19f545";
  public static int idII = 0;
  public static int idPR = 0;
  public static int idPI = 0;
  public static int idDI = 0;
  public static int idDPI = 0;
  public static readonly string attrChangeDate = "cad007a0-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidattrChangeDateEnd = "cadd9562-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrLCFailed = "cad01484-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrFutureLC = "cad01483-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrIncludeGoal = "cad007a3-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrAuxLinks = "cadd93b7-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrNotifDate = "cadd9732-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrReasonObject = "cad00697-306c-11d8-b4e9-00304f19f545";
  public static readonly string attrHidingType = "cadd98a3-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrDelWhenExcluded = "cad00073-306c-11d8-b4e9-00304f19f545";
  public int attrChangeDateId = -1;
  public int attrChangeDateEndId = -1;
  public int attrLCFailedId = -1;
  public int attrFutureLCId = -1;
  public int attrIncludeGoalId = -1;
  public int attrAuxLinksId = -1;
  public int attrChangeNo = -1;
  public int attrLCStep = -1;
  public int attrNotifDate = -1;
  public int attrReasonObj = -1;
  public int attrHidingId = -1;
  public int attrDelWhenExcluded = -1;
  public static readonly string lcActualize = "cad003cc-306c-11d8-b4e9-00304f19f545";
  public static readonly string lcWaiting = "cad00824-306c-11d8-b4e9-00304f19f545";
  public static readonly string lcDeleting = "cad003c9-306c-11d8-b4e9-00304f19f545";
  public static readonly string levelKeeping = "cad009de-306c-11d8-b4e9-00304f19f545";
  public static readonly string levelProduction = "cad00011-306c-11d8-b4e9-00304f19f545";
  public static readonly string levelAnnuled = "cad00012-306c-11d8-b4e9-00304f19f545";
  public static readonly string levelWaitingForII = "cadd9593-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidLink_FromDI = "cadd955f-306c-11d8-b4e9-00304f19f545";
  private Dictionary<int, System.Action<IUserSession, long, long, long>> _includedIntoECO = new Dictionary<int, System.Action<IUserSession, long, long, long>>();
  public static readonly string guidAttrDopIzv = "cadd9561-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrDopDesign = "cadd9563-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrLinkToAnnuledPI = "cadd96bf-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidModelDetal = "cad0078f-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidModelSborEd = "cad00768-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidModelStandEd = "cad015cb-306c-11d8-b4e9-00304f19f545";
  internal HashSet<int> modelTypes = new HashSet<int>();
  internal IServiceProvider _serviceProvider;
  internal IEventLogHelper _iLogH;
  internal static IDBTimedEvents _idbTE;
  internal EcoPropHolder ep;
  internal int lcActualizeId = -1;
  internal int lcWaitId = -1;
  internal int idLinkRevision = -1;
  internal int idAttrVerId = -1;
  internal int levKeepingId = -1;
  internal bool lockDoNextLCStep;
  internal int lcWaitingForII = -1;
  internal int levDeletedId = -1;
  internal static int relTypeDI = 0;
  internal static int idAttrDopIzv = 0;
  internal static int idAttrDopDesign = 0;
  internal static int idAttrLinkToAnnuledPI = 0;
  internal static int idLevelKeeping = 0;
  internal static int idLevelAnnuled = 0;
  internal static int idLevelProduction = 0;
  internal ConcurrentDictionary<long, ECOServer.DeletingPackage> delayedModels = new ConcurrentDictionary<long, ECOServer.DeletingPackage>();
  internal ConcurrentDictionary<Guid, ECOServer.DeletingPackage> sessionModels = new ConcurrentDictionary<Guid, ECOServer.DeletingPackage>();
  private readonly HashSet<long> NotDeleted = new HashSet<long>()
  {
    -1L
  };
  private object _syncRoot = new object();
  private Thread _thread;
  private bool _inEvent;
  private ConcurrentDictionary<long, bool> _lockedRevList = new ConcurrentDictionary<long, bool>(1, 100);
  private HashSet<long> _verIdentsToDelete = new HashSet<long>();
  public static readonly string GuidAttrLitera = "cad0038b-306c-11d8-b4e9-00304f19f545";
  public static readonly Guid LiteraGuid = new Guid(ECOServer.GuidAttrLitera);
  public static List<string> LiteraList = (List<string>) null;
  internal Dictionary<long, List<long>> _startedLinkCreation = new Dictionary<long, List<long>>();
  internal HashSet<long> _startedLinkDeletion = new HashSet<long>();
  internal HashSet<long> _startedECODeletion = new HashSet<long>();
  public static readonly string guidInvNoOTD = "cadd935b-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidDateOTD = "cadd941c-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidRegOTD = "cadd941d-306c-11d8-b4e9-00304f19f545";
  private readonly HashSet<long> _disabledAddContextECOs = new HashSet<long>();
  public int idLinkDoc = -1;
  public int idLinkProject = -1;
  internal static HashSet<long> deletingObjects = new HashSet<long>();

  public Dictionary<int, System.Action<IUserSession, long, long, long>> IncludedIntoECO
  {
    get => this._includedIntoECO;
  }

  public bool SubscribeToIncludeIntoECO(int objType, System.Action<IUserSession, long, long, long> code)
  {
    if (this._includedIntoECO.ContainsKey(objType))
      return false;
    this._includedIntoECO.Add(objType, code);
    return true;
  }

  static ECOServer() => ECOServer.ecos = new ECOServer();

  private ECOServer()
  {
  }

  public void Init()
  {
    IServerSession sessionPermanentClone = ECOServer._idbTE.GetSystemSessionPermanentClone("ECOServer.Init") as IServerSession;
    try
    {
      ECOServer.idII = sessionPermanentClone.GetObjectType(new Guid(ECOServer.ECO_II)).ObjectType;
      ECOServer.idPR = sessionPermanentClone.GetObjectType(new Guid(ECOServer.ECO_PR)).ObjectType;
      ECOServer.idPI = sessionPermanentClone.GetObjectType(new Guid(ECOServer.ECO_PI)).ObjectType;
      ECOServer.idDI = sessionPermanentClone.GetObjectType(new Guid(ECOServer.guidObj_DI)).ObjectType;
      ECOServer.idDPI = sessionPermanentClone.GetObjectType(new Guid(ECOServer.guidObj_DPI)).ObjectType;
      this.attrChangeDateId = sessionPermanentClone.GetAttributeType(new Guid(ECOServer.attrChangeDate)).AttributeID;
      this.attrChangeDateEndId = sessionPermanentClone.GetAttributeType(new Guid(ECOServer.guidattrChangeDateEnd)).AttributeID;
      this.attrLCFailedId = sessionPermanentClone.GetAttributeType(new Guid(ECOServer.attrLCFailed)).AttributeID;
      this.attrFutureLCId = sessionPermanentClone.GetAttributeType(new Guid(ECOServer.attrFutureLC)).AttributeID;
      this.attrIncludeGoalId = sessionPermanentClone.GetAttributeType(new Guid(ECOServer.attrIncludeGoal)).AttributeID;
      this.attrAuxLinksId = sessionPermanentClone.GetAttributeType(new Guid(ECOServer.attrAuxLinks)).AttributeID;
      ECOServer.idAttrDopIzv = sessionPermanentClone.GetAttributeType(new Guid(ECOServer.guidAttrDopIzv)).AttributeID;
      ECOServer.idAttrDopDesign = sessionPermanentClone.GetAttributeType(new Guid(ECOServer.guidAttrDopDesign)).AttributeID;
      ECOServer.idAttrLinkToAnnuledPI = sessionPermanentClone.GetAttributeType(new Guid(ECOServer.guidAttrLinkToAnnuledPI)).AttributeID;
      this.attrChangeNo = sessionPermanentClone.GetAttributeType(new Guid("cad00770-306c-11d8-b4e9-00304f19f545")).AttributeID;
      this.attrLCStep = sessionPermanentClone.GetAttributeType(new Guid("cad0002b-306c-11d8-b4e9-00304f19f545")).AttributeID;
      this.attrNotifDate = sessionPermanentClone.GetAttributeType(new Guid(ECOServer.guidAttrNotifDate)).AttributeID;
      this.attrReasonObj = sessionPermanentClone.GetAttributeType(new Guid(ECOServer.attrReasonObject)).AttributeID;
      this.attrHidingId = sessionPermanentClone.GetAttributeType(new Guid(ECOServer.attrHidingType)).AttributeID;
      this.attrDelWhenExcluded = sessionPermanentClone.GetAttributeType(new Guid(ECOServer.guidAttrDelWhenExcluded)).AttributeID;
      ECOServer.idLevelKeeping = MetaDataHelper.GetLCLevelID(new Guid("cad009de-306c-11d8-b4e9-00304f19f545"));
      ECOServer.idLevelAnnuled = MetaDataHelper.GetLCLevelID(new Guid("cad00012-306c-11d8-b4e9-00304f19f545"));
      ECOServer.idLevelProduction = MetaDataHelper.GetLCLevelID(new Guid("cad00011-306c-11d8-b4e9-00304f19f545"));
      this.idLinkProject = sessionPermanentClone.GetRelationType(new Guid("cad00023-306c-11d8-b4e9-00304f19f545")).RelationType;
      this.idLinkDoc = sessionPermanentClone.GetRelationType(new Guid("cad00154-306c-11d8-b4e9-00304f19f545")).RelationType;
      ECOServer.relTypeDI = sessionPermanentClone.GetRelationType(new Guid(ECOServer.guidLink_FromDI)).RelationType;
      IDBLifecycleStep lifecycleStep1 = sessionPermanentClone.GetLifecycleStep(new Guid(ECOServer.lcActualize));
      if (lifecycleStep1 != null)
        this.lcActualizeId = lifecycleStep1.LCStep;
      IDBLifecycleStep lifecycleStep2 = sessionPermanentClone.GetLifecycleStep(new Guid(ECOServer.lcWaiting));
      if (lifecycleStep2 != null)
        this.lcWaitId = lifecycleStep2.LCStep;
      IDBLifecycleLevelType lifecycleLevel1 = sessionPermanentClone.GetLifecycleLevel(new Guid(ECOServer.levelWaitingForII));
      if (lifecycleLevel1 != null)
        this.lcWaitingForII = lifecycleLevel1.LevelID;
      this.ep = new EcoPropHolder();
      this.ep.LoadFromBase((IUserSession) sessionPermanentClone);
      this.idLinkRevision = sessionPermanentClone.GetRelationType(new Guid("cad0036b-306c-11d8-b4e9-00304f19f545")).RelationType;
      this.idAttrVerId = sessionPermanentClone.GetAttributeType(new Guid("cad001c2-306c-11d8-b4e9-00304f19f545")).AttributeID;
      IDBLifecycleLevelType lifecycleLevel2 = sessionPermanentClone.GetLifecycleLevel(new Guid(ECOServer.levelKeeping));
      if (lifecycleLevel2 != null)
        this.levKeepingId = lifecycleLevel2.LevelID;
      IDBLifecycleLevelType lifecycleLevel3 = sessionPermanentClone.GetLifecycleLevel(new Guid("cad0000e-306c-11d8-b4e9-00304f19f545"));
      if (lifecycleLevel3 != null)
        this.levDeletedId = lifecycleLevel3.LevelID;
      ECOHolder.DeliveryListParametersInit((IUserSession) sessionPermanentClone);
      if (ServerServices.GetService(typeof (IDelayedUpdaterService)) is IDelayedUpdaterService service)
        service.DelayedNotificationEvent += new DelayedNotificationHandler(this.ProcessNotification);
    }
    finally
    {
      sessionPermanentClone.Logout("ECOServer.Init");
    }
    this.LoadModelTypes();
    (ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper).AfterNextLCStepEvent += new NextLCStepHandler(this.ehelper_AfterNextLCStepEvent);
  }

  internal void ehelper_AfterNextLCStepEvent(
    IDBObject sender,
    IDBLifecycleStep nextstep,
    IUserSession session)
  {
    if (nextstep.LevelID != this.levDeletedId)
      return;
    ECOServer.DeletingPackage deletingPackage = (ECOServer.DeletingPackage) null;
    long key = Math.Abs(sender.ObjectID);
    try
    {
      if (this.delayedModels.ContainsKey(key))
      {
        deletingPackage = this.delayedModels[key];
        if (!deletingPackage.NotEmpty())
          deletingPackage = (ECOServer.DeletingPackage) null;
        if (deletingPackage != null)
        {
          foreach (long model in deletingPackage.models)
          {
            IDBObject dBObject = session.GetObject(model, false);
            if (LinkIzvObject.CanDeleteObject(dBObject))
              ECOServer.DeleteObject(dBObject, deletingPackage.DeleteMode);
          }
        }
      }
    }
    finally
    {
      if (deletingPackage != null)
      {
        lock (deletingPackage)
          deletingPackage.wasDeleted = true;
      }
      if (this.delayedModels.ContainsKey(key))
        this.delayedModels.TryRemove(key, out deletingPackage);
    }
    Guid sessionGuid = session.SessionGUID;
    try
    {
      if (!this.sessionModels.ContainsKey(sessionGuid))
        return;
      deletingPackage = this.sessionModels[sessionGuid];
      if (!deletingPackage.NotEmpty())
        deletingPackage = (ECOServer.DeletingPackage) null;
      if (deletingPackage == null)
        return;
      foreach (long model in deletingPackage.models)
      {
        IDBObject dBObject = session.GetObject(model, false);
        if (LinkIzvObject.CanDeleteObject(dBObject))
          ECOServer.DeleteObject(dBObject, deletingPackage.DeleteMode);
      }
    }
    finally
    {
      if (deletingPackage != null)
      {
        lock (deletingPackage)
          deletingPackage.wasDeleted = true;
      }
      if (this.sessionModels.ContainsKey(sessionGuid))
        this.sessionModels.TryRemove(sessionGuid, out deletingPackage);
    }
  }

  internal void LoadModelTypes()
  {
    List<int> childrenIdRecursive1 = MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid(ECOServer.guidModelDetal));
    if (childrenIdRecursive1 != null)
      this.modelTypes.UnionWith((IEnumerable<int>) childrenIdRecursive1);
    List<int> childrenIdRecursive2 = MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid(ECOServer.guidModelSborEd));
    if (childrenIdRecursive2 != null)
      this.modelTypes.UnionWith((IEnumerable<int>) childrenIdRecursive2);
    List<int> childrenIdRecursive3 = MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid(ECOServer.guidModelStandEd));
    if (childrenIdRecursive3 == null)
      return;
    this.modelTypes.UnionWith((IEnumerable<int>) childrenIdRecursive3);
  }

  public bool IsModelType(int objType) => this.modelTypes.Contains(objType);

  internal void AddDeletingPackage(long baseObjId, ECOServer.DeletingPackage pack)
  {
    if (this.delayedModels.ContainsKey(baseObjId))
      this.delayedModels[baseObjId].models.UnionWith((IEnumerable<long>) pack.models);
    else
      this.delayedModels.GetOrAdd(baseObjId, pack);
  }

  internal void AddSessionPackage(Guid sessionGuid, ECOServer.DeletingPackage pack)
  {
    if (this.sessionModels.ContainsKey(sessionGuid))
    {
      ECOServer.DeletingPackage sessionModel = this.sessionModels[sessionGuid];
      lock (sessionModel)
        sessionModel.models.UnionWith((IEnumerable<long>) pack.models);
    }
    else
    {
      lock (pack)
        this.sessionModels.GetOrAdd(sessionGuid, pack);
    }
  }

  public HashSet<long> GetDeletedObjects(Guid sessionGuid)
  {
    if (!this.sessionModels.ContainsKey(sessionGuid))
      return (HashSet<long>) null;
    ECOServer.DeletingPackage sessionModel = this.sessionModels[sessionGuid];
    lock (sessionModel)
    {
      if (!sessionModel.wasDeleted)
        return this.NotDeleted;
      this.sessionModels.TryRemove(sessionGuid, out ECOServer.DeletingPackage _);
      return sessionModel.models;
    }
  }

  internal List<ColumnDescriptor> _GetColDescList(bool needNotifDate = false)
  {
    List<ColumnDescriptor> colDescList = new List<ColumnDescriptor>()
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1),
      new ColumnDescriptor((object) this.attrChangeDateId, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1),
      new ColumnDescriptor((object) this.attrChangeDateEndId, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1),
      new ColumnDescriptor((object) ECOServer.idAttrDopIzv, AttributeSourceTypes.Auto, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, 1),
      new ColumnDescriptor((object) this.attrLCFailedId, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1),
      new ColumnDescriptor((object) -7, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1)
    };
    if (needNotifDate)
    {
      colDescList.Add(new ColumnDescriptor((object) -8, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1));
      colDescList.Add(new ColumnDescriptor((object) this.attrNotifDate, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1));
    }
    return colDescList;
  }

  internal ColumnDescriptor[] GetColumnDescs() => this._GetColDescList().ToArray();

  internal ColumnDescriptor[] GetColumnDescs2() => this._GetColDescList(true).ToArray();

  internal DataTable GetECOsByType(
    IUserSession ius,
    ConditionStructure[] conds,
    int objTypeId,
    ColumnDescriptor[] descs)
  {
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(objTypeId);
    DataTable ecOsByType = new DataTable();
    foreach (int objectType in childrenIdRecursive)
    {
      IDBObjectCollection objectCollection = ius.GetObjectCollection(objectType);
      ConditionStructure[] conditions = new ConditionStructure[conds.Length];
      for (int index = 0; index < conds.Length; ++index)
        conditions[index] = conds[index];
      DBRecordSetParams paramSet = new DBRecordSetParams(conditions, descs);
      DataTable table = objectCollection.Select(paramSet);
      if (table != null && table.Rows.Count > 0)
        ecOsByType.Merge(table);
    }
    return ecOsByType;
  }

  private void CopyRow(DataTable dst, DataRow row)
  {
    DataRow row1 = dst.NewRow();
    row1.ItemArray = row.ItemArray;
    dst.Rows.Add(row1);
  }

  private void CopyColumns(DataTable dst, DataTable src, ColumnDescriptor[] descs)
  {
    if (src == null)
      return;
    DataColumn[] instance = (DataColumn[]) Array.CreateInstance(typeof (DataColumn), src.Columns.Count);
    for (int index = 0; index < src.Columns.Count; ++index)
    {
      DataColumn column = src.Columns[index];
      instance[index] = new DataColumn(column.ColumnName, column.DataType, column.Expression, column.ColumnMapping);
      if (descs != null && index < descs.Length && descs[index].AttributeID is Guid)
        instance[index].Caption = Convert.ToString(descs[index].AttributeID);
    }
    dst.Columns.AddRange(instance);
  }

  private DataTable CreateDTWithColumns(IUserSession ius, ColumnDescriptor[] descs)
  {
    return ius.GetObjectCollection(ECOServer.idII).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-2, RelationalOperators.Empty, (object) 0, LogicalOperators.NONE, 0, false)
    }, descs, recordCount: -2));
  }

  internal ConditionStructure[] GetConds(bool waitStep)
  {
    return new ConditionStructure[2]
    {
      new ConditionStructure(this.attrLCStep, RelationalOperators.Equal, (object) (waitStep ? this.lcWaitId : this.lcActualizeId), LogicalOperators.AND, 0, false),
      new ConditionStructure(waitStep ? this.attrChangeDateId : this.attrChangeDateEndId, RelationalOperators.LessOrEqual, (object) DateTime.Now, LogicalOperators.NONE, 0, false)
    };
  }

  internal ConditionStructure[] GetAdditionalDIConds()
  {
    return new ConditionStructure[2]
    {
      new ConditionStructure(this.attrLCStep, RelationalOperators.Equal, (object) this.lcActualizeId, LogicalOperators.AND, 0, false),
      new ConditionStructure(this.attrChangeDateId, RelationalOperators.LessOrEqual, (object) DateTime.Now, LogicalOperators.NONE, 0, false)
    };
  }

  internal DataTable GetAllECOs(IUserSession ius)
  {
    ColumnDescriptor[] columnDescs = this.GetColumnDescs();
    DataTable dtWithColumns = this.CreateDTWithColumns(ius, columnDescs);
    ConditionStructure[] conds1 = this.GetConds(true);
    foreach (DataRow row in (InternalDataCollectionBase) this.GetECOsByType(ius, conds1, ECOServer.idII, columnDescs).Rows)
    {
      if (!Convert.ToBoolean(row[4]))
        this.CopyRow(dtWithColumns, row);
    }
    ConditionStructure[] conds2 = this.GetConds(true);
    foreach (DataRow row in (InternalDataCollectionBase) this.GetECOsByType(ius, conds2, ECOServer.idPI, columnDescs).Rows)
    {
      if (!Convert.ToBoolean(row[4]))
        this.CopyRow(dtWithColumns, row);
    }
    ConditionStructure[] conds3 = this.GetConds(false);
    foreach (DataRow row in (InternalDataCollectionBase) this.GetECOsByType(ius, conds3, ECOServer.idII, columnDescs).Rows)
      this.CopyRow(dtWithColumns, row);
    ConditionStructure[] conds4 = this.GetConds(false);
    foreach (DataRow row in (InternalDataCollectionBase) this.GetECOsByType(ius, conds4, ECOServer.idPI, columnDescs).Rows)
      this.CopyRow(dtWithColumns, row);
    ConditionStructure[] conds5 = this.GetConds(false);
    foreach (DataRow row in (InternalDataCollectionBase) this.GetECOsByType(ius, conds5, ECOServer.idDI, columnDescs).Rows)
    {
      if (!Convert.ToBoolean(row[4]))
        this.CopyRow(dtWithColumns, row);
    }
    ConditionStructure[] conds6 = this.GetConds(false);
    foreach (DataRow row in (InternalDataCollectionBase) this.GetECOsByType(ius, conds6, ECOServer.idDPI, columnDescs).Rows)
    {
      if (!Convert.ToBoolean(row[4]))
        this.CopyRow(dtWithColumns, row);
    }
    ConditionStructure[] additionalDiConds = this.GetAdditionalDIConds();
    foreach (DataRow row in (InternalDataCollectionBase) this.GetECOsByType(ius, additionalDiConds, ECOServer.idDPI, columnDescs).Rows)
    {
      if (!Convert.ToBoolean(row[4]))
        this.CopyRow(dtWithColumns, row);
    }
    return dtWithColumns;
  }

  internal DataTable GetAllRelations(IUserSession ius, long ecoID)
  {
    IDBRelationCollection relationCollection = ius.GetRelationCollection(this.idLinkRevision);
    DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PRJLINK_ID)
    });
    relationCollection.LocalTypesMode = true;
    return relationCollection.ConsistFrom(paramSet, ecoID);
  }

  internal DataTable _GetECOsWithinDates(
    IUserSession ius,
    int objTypeId,
    ConditionStructure[] conds,
    ColumnDescriptor[] descs,
    int daysFromCurrent)
  {
    DataTable ecOsWithinDates = ius.GetObjectCollection(objTypeId).Select(new DBRecordSetParams(conds, descs, recordCount: -2));
    int columnIndex = ecOsWithinDates.Columns.Count - 1;
    for (int index = ecOsWithinDates.Rows.Count - 1; index >= 0; --index)
    {
      DataRow row = ecOsWithinDates.Rows[index];
      DateTime dateTime1 = DateTime.Now;
      if (row[3] != null && row[3] != DBNull.Value)
      {
        long int64 = Convert.ToInt64(row[3]);
        IDBObject dbObject = ius.GetObject(int64, false);
        if (dbObject == null)
        {
          ecOsWithinDates.Rows.RemoveAt(index);
          continue;
        }
        IDBAttribute attributeById = dbObject.GetAttributeByID(this.attrChangeDateEndId);
        if (attributeById != null)
        {
          dateTime1 = Convert.ToDateTime(attributeById.Value);
          row[2] = (object) dateTime1;
        }
      }
      else
        dateTime1 = Convert.ToDateTime(row[2]);
      if (row[columnIndex] != null && row[columnIndex] != DBNull.Value)
      {
        DateTime dateTime2 = Convert.ToDateTime(row[columnIndex]);
        TimeSpan timeSpan = dateTime1.Subtract(dateTime2);
        if (timeSpan.Days > 0 && timeSpan.Days <= daysFromCurrent)
          ecOsWithinDates.Rows.RemoveAt(index);
      }
    }
    return ecOsWithinDates;
  }

  internal DataTable GetECOsWithinDates(IUserSession ius, int daysFromCurrent)
  {
    HashSet<int> intSet = new HashSet<int>((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(ECOServer.idII));
    foreach (int num in MetaDataHelper.GetObjectTypeChildrenIDRecursive(ECOServer.idPI))
    {
      if (!intSet.Contains(num))
        intSet.Add(num);
    }
    ColumnDescriptor[] columnDescs2 = this.GetColumnDescs2();
    DataTable ecOsWithinDates1 = new DataTable();
    DateTime conditionValue2 = DateTime.Now.AddDays((double) daysFromCurrent);
    ConditionStructure[] conditionStructureArray = new ConditionStructure[2]
    {
      new ConditionStructure(ECOServer.idAttrDopIzv, RelationalOperators.NotEmpty, (object) 0, LogicalOperators.OR, 0, false),
      new ConditionStructure(this.attrChangeDateEndId, RelationalOperators.Between, (object) DateTime.Now, (object) conditionValue2, LogicalOperators.NONE, 0, false)
    };
    foreach (int objTypeId in intSet)
    {
      ConditionStructure[] conds = new ConditionStructure[conditionStructureArray.Length];
      for (int index = 0; index < conditionStructureArray.Length; ++index)
        conds[index] = conditionStructureArray[index];
      DataTable ecOsWithinDates2 = this._GetECOsWithinDates(ius, objTypeId, conds, columnDescs2, daysFromCurrent);
      if (ecOsWithinDates2 != null && ecOsWithinDates2.Rows.Count > 0)
        ecOsWithinDates1.Merge(ecOsWithinDates2);
    }
    return ecOsWithinDates1;
  }

  internal void NotifyEndingECOs(int daysFromCurrent)
  {
    if (!((ServerServices.GetService(typeof (ICustomServices)) as ICustomServices).GetService(typeof (IRouterService)) is IRouterService service))
      return;
    IUserSession sessionTemporaryClone = ECOServer._idbTE.GetSystemSessionTemporaryClone("ECOServer.NotifyEndingECOs");
    try
    {
      DataTable ecOsWithinDates = this.GetECOsWithinDates(sessionTemporaryClone, daysFromCurrent);
      foreach (DataRow row in (InternalDataCollectionBase) ecOsWithinDates.Rows)
      {
        long int64_1 = Convert.ToInt64(row[ecOsWithinDates.Columns.Count - 2]);
        if (row[2] != null && row[2] != DBNull.Value)
        {
          TimeSpan timeSpan = Convert.ToDateTime(row[2]).Subtract(DateTime.Now);
          long int64_2 = Convert.ToInt64(row[0]);
          IDBObject dbObject1 = sessionTemporaryClone.GetObject(int64_2, false);
          string str = dbObject1 != null ? $"<a href =\"#object={dbObject1.ObjectGUID}\">{dbObject1.NameInMessages}</a>" : Convert.ToString(int64_2);
          service.CreateMessage(sessionTemporaryClone.SessionGUID, int64_1, Intermech.Localization.LocalizationHolder.rm.GetString("ECO_Server20"), string.Format(Intermech.Localization.LocalizationHolder.rm.GetString("ECO_Server21"), (object) str, (object) timeSpan.Days), sessionTemporaryClone.UserID);
          IDBObject dbObject2 = sessionTemporaryClone.GetObject(int64_2, false);
          if (dbObject2 != null)
          {
            IDBAttribute dbAttribute = dbObject2.Attributes.AddAttribute(this.attrNotifDate, false);
            if (dbAttribute != null)
              dbAttribute.Value = (object) DateTime.Now;
          }
        }
      }
    }
    finally
    {
      sessionTemporaryClone?.Logout("ECOServer.NotifyEndingECOs");
    }
  }

  public bool AutoMoveObjects => this.ep.AutoMoveObjects;

  public bool WarnOnMove => this.ep.WarnOnMove;

  public bool LiteraFullSostav => this.ep.SetLiteraForFullSostav;

  internal bool InEvent
  {
    get
    {
      lock (this._syncRoot)
        return this._inEvent;
    }
    set
    {
      lock (this._syncRoot)
        this._inEvent = value;
    }
  }

  public ConcurrentDictionary<long, bool> lockedRevList => this._lockedRevList;

  public void LockRevision(long revId)
  {
    long key = Math.Abs(revId);
    if (this._lockedRevList.ContainsKey(key))
      this._lockedRevList[key] = true;
    else
      this._lockedRevList.TryAdd(key, true);
  }

  public void UnlockRevision(long revId)
  {
    long key = Math.Abs(revId);
    if (!this._lockedRevList.ContainsKey(key))
      return;
    this._lockedRevList[key] = false;
  }

  public bool IsRevLocked(long revId)
  {
    long key = Math.Abs(revId);
    return this._lockedRevList.ContainsKey(key) && this._lockedRevList[key];
  }

  public bool AddVersionToDelete(long verId)
  {
    lock (this._verIdentsToDelete)
    {
      if (this._verIdentsToDelete.Contains(verId) || this._verIdentsToDelete.Contains(-verId))
        return false;
      this._verIdentsToDelete.Add(verId);
      return true;
    }
  }

  public bool RemoveVersionToDelete(long verId)
  {
    lock (this._verIdentsToDelete)
    {
      if (this._verIdentsToDelete.Contains(verId))
      {
        this._verIdentsToDelete.Remove(verId);
        return true;
      }
      if (!this._verIdentsToDelete.Contains(-verId))
        return false;
      this._verIdentsToDelete.Remove(-verId);
      return true;
    }
  }

  public bool HasVersionToDelete(long verId)
  {
    lock (this._verIdentsToDelete)
      return this._verIdentsToDelete.Contains(verId) || this._verIdentsToDelete.Contains(-verId);
  }

  private void ProcessNotification(DelayedNotification notification)
  {
    if (!(notification is RelationDelayedNotification delayedNotification) || delayedNotification.NotificationType != ActionType.DeleteLink)
      return;
    long num = Math.Abs(delayedNotification.PartObjectID);
    if (!this.HasVersionToDelete(num))
      return;
    IUserSession sessionTemporaryClone = ECOServer._idbTE.GetSystemSessionTemporaryClone("ECOServer.ProcessNotification");
    try
    {
      if (sessionTemporaryClone == null)
        return;
      IDBObject dBObject = sessionTemporaryClone.GetObject(num, false);
      if (LinkIzvObject.CanDeleteObject(dBObject))
        ECOServer.DeleteObject(dBObject, 0L);
    }
    finally
    {
      sessionTemporaryClone?.Logout("ECOServer.ProcessNotification");
    }
    this.RemoveVersionToDelete(delayedNotification.PartObjectID);
  }

  public void SaveProps(
    Guid sessionGuid,
    bool AutoMoveObjects,
    bool WarnOnMove,
    bool writeComplect,
    string kiTemplate,
    bool DesOnReplace,
    bool leaveOTD,
    bool autoCO,
    int daysBefore,
    bool placeInvNum,
    string invNumAttr,
    bool hideHidden,
    bool alwaysOrigSize,
    bool createLiteraVersion,
    bool setLiteraFullSostav,
    bool moveAuthFiles,
    int maxDocsAllowed,
    bool replaceEmptyDesign,
    bool hideOnCreation,
    bool prohibitCustomReason,
    bool askOnNewOrgs,
    bool checkObjCreation,
    bool noSlashInDPIDesign)
  {
    this.ep.AutoMoveObjects = AutoMoveObjects;
    this.ep.WarnOnMove = WarnOnMove;
    this.ep.WriteComplect = writeComplect;
    this.ep.KIInventoryNumberTemplate = kiTemplate;
    this.ep.WriteDesOnReplace = DesOnReplace;
    this.ep.LeaveOTDNumberForChange = leaveOTD;
    this.ep.AutoCheckOut = autoCO;
    this.ep.DaysBeforeEndTermWarning = daysBefore;
    this.ep.PlaceInvNum = placeInvNum;
    this.ep.InvNumAttr = invNumAttr;
    this.ep.HideHiddenObjects = hideHidden;
    this.ep.AutoOriginalSize = alwaysOrigSize;
    this.ep.CreateLiteraVersion = createLiteraVersion;
    this.ep.SetLiteraForFullSostav = setLiteraFullSostav;
    this.ep.MoveAuthenticFiles = moveAuthFiles;
    this.ep.MaxDocNum = maxDocsAllowed;
    this.ep.ReplaceEmptyDesByTemplate = replaceEmptyDesign;
    this.ep.HideOnCreation = hideOnCreation;
    this.ep.ProhibitCustomReason = prohibitCustomReason;
    this.ep.AskOnNewOrganizations = askOnNewOrgs;
    this.ep.CheckObjectCreation = checkObjCreation;
    this.ep.NoSlashInDPIDesign = noSlashInDPIDesign;
    this.ep.SaveToBase(UserSession.GetSessionByID(sessionGuid));
  }

  public void LoadProps(
    Guid sessionGuid,
    out bool AutoMove,
    out bool WarnOnMove,
    out bool WriteComplect,
    out string kiTemplate,
    out bool DesOnReplace,
    out bool leaveOTD,
    out bool autoCO,
    out int daysBefore,
    out bool placeInvNum,
    out string invNumAttr,
    out bool HideHidden,
    out bool AutoOrigSize,
    out bool createLiteraVersion,
    out bool setLiteraFullSostav,
    out bool moveAuthFiles,
    out int maxDocsAllowed,
    out bool replaceEmptyDes,
    out bool hideOnCreation,
    out bool prohibitCustomReason,
    out bool askOnNewOrgs,
    out bool checkObjectCreation,
    out bool noSlashInDPI)
  {
    this.ep.LoadFromBase(UserSession.GetSessionByID(sessionGuid));
    kiTemplate = this.ep.KIInventoryNumberTemplate;
    AutoMove = this.ep.AutoMoveObjects;
    WarnOnMove = this.ep.WarnOnMove;
    WriteComplect = this.ep.WriteComplect;
    DesOnReplace = this.ep.WriteDesOnReplace;
    leaveOTD = this.ep.LeaveOTDNumberForChange;
    autoCO = this.ep.AutoCheckOut;
    daysBefore = this.ep.DaysBeforeEndTermWarning;
    placeInvNum = this.ep.PlaceInvNum;
    invNumAttr = this.ep.InvNumAttr;
    HideHidden = this.ep.HideHiddenObjects;
    AutoOrigSize = this.ep.AutoOriginalSize;
    createLiteraVersion = this.ep.CreateLiteraVersion;
    setLiteraFullSostav = this.ep.SetLiteraForFullSostav;
    moveAuthFiles = this.ep.MoveAuthenticFiles;
    maxDocsAllowed = this.ep.MaxDocNum;
    replaceEmptyDes = this.ep.ReplaceEmptyDesByTemplate;
    hideOnCreation = this.ep.HideOnCreation;
    prohibitCustomReason = this.ep.ProhibitCustomReason;
    askOnNewOrgs = this.ep.AskOnNewOrganizations;
    checkObjectCreation = this.ep.CheckObjectCreation;
    noSlashInDPI = this.ep.NoSlashInDPIDesign;
  }

  public void SetLitera(Guid sessionGuid, long objId, string Litera)
  {
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID(ECOServer.LiteraGuid);
    IDBObject dbObject1 = UserSession.GetSessionByID(sessionGuid).GetObject(objId, false);
    if (dbObject1 == null)
      return;
    if (objId > 0L)
    {
      IUserSession sessionTemporaryClone = ECOServer._idbTE.GetSystemSessionTemporaryClone("ECOServer.SetLitera");
      try
      {
        if (sessionTemporaryClone == null)
          return;
        IDBObject dbObject2 = sessionTemporaryClone.GetObject(objId, false);
        if (dbObject2 == null)
          return;
        ECOServer.SetObjectLitera(dbObject2, attributeTypeId, Litera);
      }
      finally
      {
        sessionTemporaryClone?.Logout("ECOServer.SetLitera");
      }
    }
    else
      ECOServer.SetObjectLitera(dbObject1, attributeTypeId, Litera);
  }

  public static void SetObjectLitera(IDBObject dbObject, int literaAttrId, string Litera)
  {
    if (ECOServer.LiteraList == null)
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(ECOServer.LiteraGuid);
      ECOServer.LiteraList = new List<string>();
      attributeType.PossibleValues.ForEach((Action<object>) (o => ECOServer.LiteraList.Add(o.ToString())));
    }
    IDBAttribute dbAttribute = dbObject.GetAttributeByGuid(ECOServer.LiteraGuid) ?? dbObject.Attributes.AddAttribute(literaAttrId, false);
    if (dbAttribute.Value != null && dbAttribute.Value != DBNull.Value)
    {
      string str = Convert.ToString(dbAttribute.Value);
      if (ECOServer.LiteraList.IndexOf(str) >= ECOServer.LiteraList.IndexOf(Litera))
        return;
    }
    dbAttribute.AsString = Litera;
  }

  public long GetNewChangeNo(long Id, long objId)
  {
    long newChangeNo = 1;
    IUserSession sessionTemporaryClone = ECOServer._idbTE != null ? ECOServer._idbTE.GetSystemSessionTemporaryClone("ECOServer.GetNewChangeNo") : (IUserSession) null;
    if (sessionTemporaryClone == null)
      throw new Exception(Intermech.Localization.LocalizationHolder.rm.GetString("ECO_Server23"));
    try
    {
      List<long> objectVersions = sessionTemporaryClone.GetObjectVersions(Id);
      if (objectVersions == null)
        throw new Exception(string.Format(Intermech.Localization.LocalizationHolder.rm.GetString("ECO_Server22"), (object) Id));
      foreach (long objectID in objectVersions)
      {
        if (Math.Abs(objectID) != Math.Abs(objId))
        {
          IDBObject dbObject = sessionTemporaryClone.GetObject(objectID, false);
          if (dbObject != null)
          {
            IDBAttribute attributeById = dbObject.GetAttributeByID(this.attrChangeNo);
            if (attributeById != null && attributeById.Value != DBNull.Value)
            {
              string str1 = Convert.ToString(attributeById.Value);
              try
              {
                long int64 = Convert.ToInt64(str1);
                if (int64 >= newChangeNo)
                  newChangeNo = int64 + 1L;
              }
              catch (Exception ex1)
              {
                switch (ex1)
                {
                  case FormatException _:
                  case OverflowException _:
                    int num = 0;
                    while (num < str1.Length && char.IsDigit(str1[num]))
                      ++num;
                    if (num > 0)
                    {
                      if (num < str1.Length)
                      {
                        string str2 = str1.Substring(0, num);
                        try
                        {
                          long int64 = Convert.ToInt64(str2);
                          if (int64 >= newChangeNo)
                          {
                            newChangeNo = int64 + 1L;
                            continue;
                          }
                          continue;
                        }
                        catch (Exception ex2)
                        {
                          switch (ex2)
                          {
                            case FormatException _:
                            case OverflowException _:
                              continue;
                            default:
                              throw;
                          }
                        }
                      }
                      else
                        continue;
                    }
                    else
                      continue;
                  default:
                    throw;
                }
              }
            }
          }
        }
      }
    }
    finally
    {
      sessionTemporaryClone?.Logout("ECOServer.GetNewChangeNo");
    }
    return newChangeNo;
  }

  public bool IsChangeNumUnique(long objId, string sNum)
  {
    IUserSession sessionTemporaryClone = ECOServer._idbTE.GetSystemSessionTemporaryClone("ECOServer.IsChangeNumUnique");
    try
    {
      List<long> objectIdVersions = sessionTemporaryClone.GetObjectIDVersions(objId);
      if (objectIdVersions == null)
        return true;
      foreach (long objectID in objectIdVersions)
      {
        if (Math.Abs(objectID) != Math.Abs(objId))
        {
          IDBObject dbObject = sessionTemporaryClone.GetObject(objectID, false);
          if (dbObject != null)
          {
            IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad00770-306c-11d8-b4e9-00304f19f545"));
            if (attributeByGuid != null && attributeByGuid.Value != DBNull.Value && Convert.ToString(attributeByGuid.Value).Trim() == sNum)
              return false;
          }
        }
      }
    }
    finally
    {
      sessionTemporaryClone?.Logout("ECOServer.IsChangeNumUnique");
    }
    return true;
  }

  public void SaveDeliveryListParams(Guid sessionGuid)
  {
    ECOHolder.DeliveryListParametersInit(UserSession.GetSessionByID(sessionGuid));
  }

  public bool GetDeliveryListParam() => ECOHolder.CopyDeliveryListToDoc;

  public Dictionary<long, long> GetDocsIDsInfoFromECOComposition(long ecoObjectID, Guid sessionGuid)
  {
    Dictionary<long, long> fromEcoComposition = new Dictionary<long, long>();
    IDBRelationCollection relationCollection = UserSession.GetSessionByID(sessionGuid).GetRelationCollection(MetaDataHelper.GetRelationTypeID("cad0036b-306c-11d8-b4e9-00304f19f545"));
    relationCollection.LocalTypesMode = true;
    DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[3]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0)
    });
    DataTable dataTable = relationCollection.ConsistFrom(paramSet, ecoObjectID);
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      long int64_1 = Convert.ToInt64(dataTable.Rows[index][-3.ToString()]);
      long int64_2 = Convert.ToInt64(dataTable.Rows[index][-2.ToString()]);
      if (MetaDataHelper.IsObjectTypeChildOf(Convert.ToInt32(dataTable.Rows[index][-7.ToString()]), MetaDataHelper.GetObjectTypeID(new Guid("cad00070-306c-11d8-b4e9-00304f19f545"))))
        fromEcoComposition.Add(int64_2, int64_1);
    }
    return fromEcoComposition;
  }

  public List<string> AssignChangeNumbers(List<IdLinkPair> objRevList)
  {
    List<string> stringList = new List<string>();
    IUserSession sessionTemporaryClone = ECOServer._idbTE.GetSystemSessionTemporaryClone("ECOServer.AssignChangeNumbers");
    try
    {
      for (int index = 0; index < objRevList.Count; ++index)
      {
        IdLinkPair objRev = objRevList[index];
        if (objRev.Goal == ECOGoal.Creation)
        {
          stringList.Add("");
        }
        else
        {
          string str = Convert.ToString(this.GetNewChangeNo(objRev.ObjID, sessionTemporaryClone));
          IDBObject dbObject = sessionTemporaryClone.GetObject(objRev.ObjID, false);
          IDBRelation relation = sessionTemporaryClone.GetRelation(objRev.RelID, false);
          List<long> longList = new List<long>();
          if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout && dbObject.ObjectID > 0L)
          {
            sessionTemporaryClone.StartLogHistory();
            try
            {
              dbObject = dbObject.CheckOut();
            }
            finally
            {
              sessionTemporaryClone.StopLogHistory();
            }
            foreach (CategoryValue modificationsHistory in sessionTemporaryClone.GetModificationsHistoryList())
            {
              if (modificationsHistory.ActionID == ActionType.CheckOut)
                longList.Add(modificationsHistory.CategoryID);
            }
          }
          IDBAttribute dbAttribute = dbObject.Attributes.AddAttribute(this.attrChangeNo, false);
          if (dbAttribute != null)
            dbAttribute.AsString = str;
          if (longList.Count > 0)
          {
            foreach (long objectID in longList)
            {
              IDBObject objectActualCopy = sessionTemporaryClone.GetObjectActualCopy(objectID, false);
              if (objectActualCopy != null && objectActualCopy.ObjectID < 0L)
                objectActualCopy.CheckIn();
            }
          }
          IDBAttribute attributeById = relation.GetAttributeByID(this.attrChangeNo);
          if (attributeById != null)
            attributeById.AsString = str;
          stringList.Add(str);
        }
      }
    }
    finally
    {
      sessionTemporaryClone?.Logout("ECOServer.AssignChangeNumbers");
    }
    return stringList;
  }

  public void ClearChangeNumbers(List<IdLinkPair> objRevList)
  {
    IUserSession sessionTemporaryClone = ECOServer._idbTE.GetSystemSessionTemporaryClone("ECOServer.ClearChangeNumbers");
    try
    {
      for (int index = 0; index < objRevList.Count; ++index)
      {
        IdLinkPair objRev = objRevList[index];
        IDBObject dbObject = sessionTemporaryClone.GetObject(objRev.ObjID, false);
        IDBRelation relation = sessionTemporaryClone.GetRelation(objRev.RelID, false);
        List<long> longList = new List<long>();
        if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout && dbObject.ObjectID > 0L)
        {
          sessionTemporaryClone.StartLogHistory();
          try
          {
            dbObject = dbObject.CheckOut();
          }
          finally
          {
            sessionTemporaryClone.StopLogHistory();
          }
          foreach (CategoryValue modificationsHistory in sessionTemporaryClone.GetModificationsHistoryList())
          {
            if (modificationsHistory.ActionID == ActionType.CheckOut)
              longList.Add(modificationsHistory.CategoryID);
          }
        }
        IDBAttribute byId = dbObject.Attributes.FindByID(this.attrChangeNo);
        if (byId != null)
          byId.AsString = "";
        if (longList.Count > 0)
        {
          foreach (long objectID in longList)
          {
            IDBObject objectActualCopy = sessionTemporaryClone.GetObjectActualCopy(objectID, false);
            if (objectActualCopy != null && objectActualCopy.ObjectID < 0L)
              objectActualCopy.CheckIn();
          }
        }
        IDBAttribute attributeById = relation.GetAttributeByID(this.attrChangeNo);
        if (attributeById != null)
          attributeById.AsString = "";
      }
    }
    finally
    {
      sessionTemporaryClone?.Logout("ECOServer.ClearChangeNumbers");
    }
  }

  private long GetNewChangeNo(long objId, IUserSession ius)
  {
    long newChangeNo = 1;
    foreach (long objectIdVersion in ius.GetObjectIDVersions(objId))
    {
      if (Math.Abs(objectIdVersion) != Math.Abs(objId))
      {
        IDBObject dbObject = ius.GetObject(objectIdVersion, false);
        if (dbObject != null)
        {
          IDBAttribute attributeById = dbObject.GetAttributeByID(this.attrChangeNo);
          if (attributeById != null && attributeById.Value != DBNull.Value)
          {
            string str1 = Convert.ToString(attributeById.Value);
            try
            {
              long int64 = Convert.ToInt64(str1);
              if (int64 >= newChangeNo)
                newChangeNo = int64 + 1L;
            }
            catch (Exception ex1)
            {
              switch (ex1)
              {
                case FormatException _:
                case OverflowException _:
                  int num = 0;
                  while (num < str1.Length && char.IsDigit(str1[num]))
                    ++num;
                  if (num > 0)
                  {
                    if (num < str1.Length)
                    {
                      string str2 = str1.Substring(0, num);
                      try
                      {
                        long int64 = Convert.ToInt64(str2);
                        if (int64 >= newChangeNo)
                        {
                          newChangeNo = int64 + 1L;
                          continue;
                        }
                        continue;
                      }
                      catch (Exception ex2)
                      {
                        switch (ex2)
                        {
                          case FormatException _:
                          case OverflowException _:
                            continue;
                          default:
                            throw;
                        }
                      }
                    }
                    else
                      continue;
                  }
                  else
                    continue;
                default:
                  throw;
              }
            }
          }
        }
      }
    }
    return newChangeNo;
  }

  public bool SetStartDate(long objId, DateTime date) => this.SetDate(objId, date, true);

  public bool SetEndDate(long objId, DateTime date) => this.SetDate(objId, date, false);

  private bool SetDate(long objId, DateTime date, bool Start)
  {
    bool flag = true;
    IUserSession sessionTemporaryClone = ECOServer._idbTE.GetSystemSessionTemporaryClone("ECOServer.SetDate");
    try
    {
      IDBObject idbO1 = sessionTemporaryClone.GetObject(objId, false);
      if (idbO1 != null)
        flag = this._SetDate(idbO1, date, Start);
      if (objId > 0L)
      {
        IDBObject idbO2 = sessionTemporaryClone.GetObject(-objId, false);
        if (idbO2 != null)
          flag = this._SetDate(idbO2, date, Start) & flag;
      }
    }
    finally
    {
      sessionTemporaryClone?.Logout("ECOServer.SetDate");
    }
    return flag;
  }

  private bool _SetDate(IDBObject idbO, DateTime date, bool Start)
  {
    IDBAttribute attributeById = idbO.GetAttributeByID(Start ? this.attrChangeDateId : this.attrChangeDateEndId);
    if (attributeById == null)
      return false;
    try
    {
      if (date.Equals(DateTime.MinValue))
      {
        if (attributeById.Value != DBNull.Value)
          attributeById.Value = (object) DBNull.Value;
      }
      else if (attributeById.AsDateTime != date)
        attributeById.AsDateTime = date;
    }
    catch
    {
      return false;
    }
    return true;
  }

  public void RemoveChangeNums(Guid sessionGuid, long relId, long objId)
  {
    UserSession sessionById = (UserSession) UserSession.GetSessionByID(sessionGuid);
    if (relId != 0L)
    {
      IDBRelation relation = sessionById.GetRelation(relId, false);
      if (relation != null)
      {
        IDBAttribute attributeById1 = relation.GetAttributeByID(this.attrChangeNo);
        if (attributeById1 != null)
          attributeById1.Value = (object) DBNull.Value;
        if (objId == 0L)
        {
          IDBAttribute attributeById2 = relation.GetAttributeByID(this.idAttrVerId);
          if (attributeById2 != null && attributeById2.Value != DBNull.Value)
            objId = Convert.ToInt64(attributeById2.Value);
        }
      }
    }
    if (objId == 0L)
      return;
    IDBObject objectActualCopy = sessionById.GetObjectActualCopy(objId, false);
    if (objectActualCopy == null)
      return;
    IDBAttribute attributeById = objectActualCopy.GetAttributeByID(this.attrChangeNo);
    if (attributeById == null)
      return;
    attributeById.Value = (object) DBNull.Value;
  }

  private void DeleteStartEndAttrs(IUserSession ius, long objId)
  {
    IDBObject dbObject = ius.GetObject(objId, false);
    if (dbObject == null)
      return;
    if (((DBSessionable) dbObject).Deleted)
      return;
    try
    {
      dbObject.GetAttributeByID(this.attrChangeDateId)?.Delete(0L);
      dbObject.GetAttributeByID(this.attrChangeDateEndId)?.Delete(0L);
    }
    catch
    {
    }
  }

  public void DeleteStartEndAttrs(List<long> objIDs)
  {
    IUserSession sessionTemporaryClone = ECOServer._idbTE.GetSystemSessionTemporaryClone("ECOServer.DeleteStartEndAttrs");
    try
    {
      foreach (long objId in objIDs)
        this.DeleteStartEndAttrs(sessionTemporaryClone, objId);
    }
    finally
    {
      sessionTemporaryClone?.Logout("ECOServer.DeleteStartEndAttrs");
    }
  }

  public void LinkRevisionsToOther(
    Guid sessionGuid,
    IEnumerable<long> revList,
    long newLinkedContextNumber)
  {
    UserSession sessionById = (UserSession) UserSession.GetSessionByID(sessionGuid);
    List<IDBEditingContextsObject> editingContextsObjectList = new List<IDBEditingContextsObject>();
    foreach (long rev in revList)
    {
      if (sessionById.GetObject(rev, false) is IDBEditingContextsObject editingContextsObject)
      {
        if (sessionById.UserID != editingContextsObject.OwnerID)
          (editingContextsObject as IDBSecurity).CheckAccess(ActionType.Edit, false, true);
        editingContextsObjectList.Add(editingContextsObject);
      }
    }
    sessionById.StartTransaction();
    try
    {
      foreach (IDBEditingContextsObject editingContextsObject in editingContextsObjectList)
        editingContextsObject.LinkedContextNumber = newLinkedContextNumber;
      foreach (IDBEditingContextsObject idbEC in editingContextsObjectList)
        this._RecordLinkMessage(sessionById, idbEC, newLinkedContextNumber);
    }
    catch
    {
      sessionById.Rollback();
      throw;
    }
    finally
    {
      if (sessionById.InTransaction)
        sessionById.Commit();
    }
  }

  public void RecordLinkMessage(Guid sessionGuid, long revId, long newContext)
  {
    UserSession sessionById = (UserSession) UserSession.GetSessionByID(sessionGuid);
    if (sessionById == null)
      return;
    IDBEditingContextsObject idbEC = (IDBEditingContextsObject) sessionById.GetObject(revId, false);
    if (idbEC == null)
      return;
    this._RecordLinkMessage(sessionById, idbEC, newContext);
  }

  private void _RecordLinkMessage(UserSession ius, IDBEditingContextsObject idbEC, long newContext)
  {
    IEventLogHelper eventLogHelper = ius.EventLogHelper;
    long objectId = idbEC.ObjectID;
    string str1 = $"{Intermech.Localization.LocalizationHolder.rm.GetString("ECO_Server17")} '{idbEC.Caption}'";
    string str2 = $"{Intermech.Localization.LocalizationHolder.rm.GetString("ECO_Server18")} [{Convert.ToString(newContext)}]";
    long ObjectID = objectId;
    long CategoryID = objectId;
    string ObjectName = str1;
    string Note = str2;
    long userId = ius.UserID;
    string computerName = ius.ComputerName;
    UserSession aSession = ius;
    eventLogHelper.AddEvent(ObjectID, 0L, 2, CategoryID, ObjectName, Note, ActionType.LinkECO_ToContext, EventlogRecordType.Information, userId, computerName, (IUserSession) aSession);
  }

  public void UnlinkToOther(Guid sessionGuid, long revId)
  {
    UserSession sessionById = (UserSession) UserSession.GetSessionByID(sessionGuid);
    IDBObject dbObject = sessionById.GetObject(revId, false);
    IDBEditingContextsObject editingContextsObject1 = dbObject as IDBEditingContextsObject;
    if (sessionById.UserID != dbObject.OwnerID)
      (dbObject as IDBSecurity).CheckAccess(ActionType.Edit, false, true);
    long linkedContextNumber = editingContextsObject1.LinkedContextNumber;
    editingContextsObject1.LinkedContextNumber = Math.Abs(revId);
    if (linkedContextNumber == editingContextsObject1.LinkedContextNumber)
    {
      IDBObjectCollection objectCollection = sessionById.GetObjectCollection(-1);
      List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(MetaDataHelper.GetObjectTypeID(new Guid("cad00348-306c-11d8-b4e9-00304f19f545")));
      int attributeTypeId = MetaDataHelper.GetAttributeTypeID(new Guid("cad014ff-306c-11d8-b4e9-00304f19f545"));
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[2]
      {
        new ConditionStructure(-7, RelationalOperators.In, (object) childrenIdRecursive.ToArray(), LogicalOperators.AND, 0, false),
        new ConditionStructure(attributeTypeId, RelationalOperators.Equal, (object) linkedContextNumber, LogicalOperators.NONE, 0, false)
      }, new object[2]
      {
        (object) -2,
        (object) attributeTypeId
      }, 0L, (object) null, -1);
      DataTable dataTable = objectCollection.Select(paramSet);
      if (dataTable != null && dataTable.Rows.Count > 0)
      {
        long num1 = -1;
        List<long> longList = new List<long>();
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          long int64 = Convert.ToInt64(row[0]);
          long num2 = Math.Abs(int64);
          if (num2 != linkedContextNumber)
          {
            longList.Add(int64);
            if (num1 == -1L)
              num1 = num2;
          }
        }
        if (num1 != -1L)
        {
          foreach (long objectID in longList)
          {
            if (sessionById.GetObject(objectID, false) is IDBEditingContextsObject editingContextsObject2)
              editingContextsObject2.LinkedContextNumber = num1;
          }
        }
      }
    }
    IEventLogHelper eventLogHelper = sessionById.EventLogHelper;
    string str1 = $"{Intermech.Localization.LocalizationHolder.rm.GetString("ECO_Server17")} '{editingContextsObject1.Caption}'";
    string str2 = $"{Intermech.Localization.LocalizationHolder.rm.GetString("ECO_Server19")} [{Convert.ToString(linkedContextNumber)}]";
    long ObjectID = revId;
    long CategoryID = revId;
    string ObjectName = str1;
    string Note = str2;
    long userId = sessionById.UserID;
    string computerName = sessionById.ComputerName;
    UserSession aSession = sessionById;
    eventLogHelper.AddEvent(ObjectID, 0L, 2, CategoryID, ObjectName, Note, ActionType.UnlinkECO_FromContext, EventlogRecordType.Information, userId, computerName, (IUserSession) aSession);
  }

  public bool ObjectHasID(Guid sessionGuid, long objId)
  {
    UserSession sessionById = (UserSession) UserSession.GetSessionByID(sessionGuid);
    if (sessionById == null)
      return false;
    IDbManager dataManager = sessionById.DataManager;
    object obj = dataManager.ExecuteScalar("SELECT F_ID FROM IMS_OBJECTS WHERE F_OBJECT_ID = :oid", dataManager.Parameter("oid", (object) objId));
    return obj != null && obj != DBNull.Value;
  }

  public void StartLinkCreation(long rootId, long childId)
  {
    rootId = Math.Abs(rootId);
    lock (this._startedLinkCreation)
    {
      if (!this._startedLinkCreation.ContainsKey(rootId))
        this._startedLinkCreation.Add(rootId, new List<long>());
      List<long> longList = this._startedLinkCreation[rootId];
      if (longList.Contains(childId))
        return;
      longList.Add(childId);
    }
  }

  public void EndLinkCreation(long rootId, long childId)
  {
    rootId = Math.Abs(rootId);
    lock (this._startedLinkCreation)
    {
      if (!this._startedLinkCreation.ContainsKey(rootId))
        return;
      List<long> longList = this._startedLinkCreation[rootId];
      if (!longList.Contains(childId))
        return;
      longList.Remove(childId);
    }
  }

  public bool LinkCreationAllowed(long rootId, long childId)
  {
    rootId = Math.Abs(rootId);
    lock (this._startedLinkCreation)
      return this._startedLinkCreation.ContainsKey(rootId) && this._startedLinkCreation[rootId].Contains(childId);
  }

  public void StartLinkDeletion(long relId)
  {
    lock (this._startedLinkDeletion)
    {
      if (this._startedLinkDeletion.Contains(relId))
        return;
      this._startedLinkDeletion.Add(relId);
    }
  }

  public void EndLinkDeletion(long relId)
  {
    lock (this._startedLinkDeletion)
    {
      if (!this._startedLinkDeletion.Contains(relId))
        return;
      this._startedLinkDeletion.Remove(relId);
    }
  }

  public void DoDeleteRelation(IDBRelation rel)
  {
    long relationId = rel.RelationID;
    this.StartLinkDeletion(relationId);
    try
    {
      rel.Delete(0L);
    }
    finally
    {
      this.EndLinkDeletion(relationId);
    }
  }

  public void StartECODeletion(long ecoId)
  {
    lock (this._startedECODeletion)
    {
      if (this._startedECODeletion.Contains(ecoId))
        return;
      this._startedECODeletion.Add(ecoId);
    }
  }

  public void EndECODeletion(long ecoId)
  {
    lock (this._startedECODeletion)
    {
      if (!this._startedECODeletion.Contains(ecoId))
        return;
      this._startedECODeletion.Remove(ecoId);
    }
  }

  public bool LinkDeletionAllowed(long relId)
  {
    lock (this._startedLinkDeletion)
      return this._startedLinkDeletion.Contains(relId);
  }

  public bool ECODeletionAllowed(long ecoId)
  {
    lock (this._startedECODeletion)
      return this._startedECODeletion.Contains(ecoId);
  }

  public void ClearOTDAttrs(long objId)
  {
    IUserSession sessionTemporaryClone = ECOServer._idbTE.GetSystemSessionTemporaryClone(nameof (ECOServer));
    try
    {
      IDBObject dbObject = sessionTemporaryClone.GetObject(objId, false);
      IDBAttribute attributeByGuid1 = dbObject.GetAttributeByGuid(new Guid(ECOServer.guidInvNoOTD));
      if (attributeByGuid1 != null)
        attributeByGuid1.Value = (object) DBNull.Value;
      IDBAttribute attributeByGuid2 = dbObject.GetAttributeByGuid(new Guid(ECOServer.guidRegOTD));
      if (attributeByGuid2 != null)
        attributeByGuid2.Value = (object) DBNull.Value;
      IDBAttribute attributeByGuid3 = dbObject.GetAttributeByGuid(new Guid(ECOServer.guidDateOTD));
      if (attributeByGuid3 == null)
        return;
      attributeByGuid3.Value = (object) DBNull.Value;
    }
    catch (KernelExceptionID ex)
    {
    }
    finally
    {
      sessionTemporaryClone?.Logout(nameof (ECOServer));
    }
  }

  public void StartDisableAddContext(long ecoObjId)
  {
    this._disabledAddContextECOs.Add(Math.Abs(ecoObjId));
  }

  public void StopDisableAddContext(long ecoObjId)
  {
    this._disabledAddContextECOs.Remove(Math.Abs(ecoObjId));
  }

  public bool IsAddContextDisabled(long ecoObjId)
  {
    return this._disabledAddContextECOs.Contains(Math.Abs(ecoObjId));
  }

  public static IDBLifecycleStep GetStepForLevel(
    IUserSession systemSession,
    long verId,
    int levelId)
  {
    IDBObject dbObject = systemSession.GetObject(verId, false);
    if (dbObject == null)
      return (IDBLifecycleStep) null;
    IDBObjectType objectType = systemSession.GetObjectType(dbObject.ObjectType);
    if (objectType == null)
      return (IDBLifecycleStep) null;
    DataTable table = systemSession.GetLCSchema(objectType.SchemaID).GetStepsCollection().GetSchema().Tables["IMS_LC_STEPS"];
    if (table == null)
      return (IDBLifecycleStep) null;
    foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
    {
      if (Convert.ToInt32(row["F_LEVEL_ID"]) == levelId)
      {
        int int32 = Convert.ToInt32(row["F_LC_STEP"]);
        return systemSession.GetLifecycleStep(int32);
      }
    }
    return (IDBLifecycleStep) null;
  }

  internal static void SendMessage(
    IUserSession session,
    long toUserID,
    List<long> objects,
    long revId,
    bool autoMove,
    bool failed)
  {
    if (!((ServerServices.GetService(typeof (ICustomServices)) as ICustomServices).GetService(typeof (IRouterService)) is IRouterService service))
      return;
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2 = new StringBuilder();
    for (int index = 0; index < objects.Count; ++index)
    {
      IDBObject dbObject = session.GetObject(objects[index], false);
      string str = dbObject != null ? $"<a href =\"#object={dbObject.ObjectGUID}\">{dbObject.NameInMessages}</a>" : Convert.ToString(objects[index]);
      stringBuilder1.Append(str);
      stringBuilder2.Append(Convert.ToString(objects[index]));
      if (index < objects.Count - 1)
      {
        stringBuilder1.Append(", ");
        stringBuilder2.Append(",");
      }
    }
    stringBuilder2.ToString();
    string Subject = string.Format(Intermech.Localization.LocalizationHolder.rm.GetString("ECO_Server10"), (object) revId);
    string format;
    if (failed)
      format = Intermech.Localization.LocalizationHolder.rm.GetString("ECO_Server2");
    else if (autoMove)
    {
      format = Intermech.Localization.LocalizationHolder.rm.GetString("ECO_Server4");
      Subject = Intermech.Localization.LocalizationHolder.rm.GetString("ECO_Server11");
    }
    else
    {
      format = Intermech.Localization.LocalizationHolder.rm.GetString("ECO_Server3");
      Subject = Intermech.Localization.LocalizationHolder.rm.GetString("ECO_Server12");
    }
    string Text = string.Format(format, (object) stringBuilder1.ToString());
    service.CreateMessage(session.SessionGUID, toUserID, Subject, Text, session.UserID);
  }

  internal static void SendExceptionMessage(
    IUserSession session,
    long toUserID,
    List<long> objects,
    long revId,
    Exception e)
  {
    if (!((ServerServices.GetService(typeof (ICustomServices)) as ICustomServices).GetService(typeof (IRouterService)) is IRouterService service))
      return;
    string Subject = string.Format(Intermech.Localization.LocalizationHolder.rm.GetString("ECO_Server10"), (object) revId);
    service.CreateMessage(session.SessionGUID, toUserID, Subject, e.Message, session.UserID);
  }

  public static void GetEntersInObjects(UserSession ius, long verId, ref List<long> verIDs)
  {
    IDBObject dbObject = ius.GetObject(verId, false);
    if (dbObject == null)
      return;
    IDBRelationCollection relationCollection = ius.GetRelationCollection(-1, "cad001e0-306c-11d8-b4e9-00304f19f545");
    relationCollection.LocalTypesMode = true;
    foreach (DataRow row in (InternalDataCollectionBase) relationCollection.EntersIn(new DBRecordSetParams((ConditionStructure[]) null, new object[1]
    {
      (object) -2
    }), dbObject.ID).Rows)
    {
      long int64 = Convert.ToInt64(row[0]);
      if (verIDs.IndexOf(int64) < 0)
        verIDs.Add(int64);
    }
  }

  public static bool IsSynchroMove(int parentObjType, int childObjType, int relType)
  {
    return (MetaDataHelper.GetApplicability(parentObjType, childObjType, relType).Options & ApplicabilityOptions.ChangeLCStep) != 0;
  }

  internal static bool HasMultipleRevLinks(IUserSession ius, long verId)
  {
    List<long> parentRevisions = ECOServer.GetParentRevisions(ius, verId);
    return parentRevisions != null && parentRevisions.Count > 1;
  }

  internal static List<long> GetParentRevisions(IUserSession ius, long verId)
  {
    if (verId == 0L)
      return (List<long>) null;
    DataTable dataTable = (DataTable) null;
    IDBRelationCollection relationCollection = ius.GetRelationCollection(ECOServer.ecos.idLinkRevision);
    try
    {
      dataTable = relationCollection.EntersInVersion(new DBRecordSetParams((ConditionStructure[]) null, new object[1]
      {
        (object) -2
      }), verId);
    }
    catch (ObjectNotFoundException ex)
    {
    }
    if (dataTable == null || dataTable.Rows.Count <= 0)
      return (List<long>) null;
    List<long> parentRevisions = new List<long>();
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      if (row[0] != DBNull.Value)
        parentRevisions.Add(Convert.ToInt64(row[0]));
    }
    return parentRevisions;
  }

  public void _treadStart()
  {
    this._thread = new Thread(new ThreadStart(this.MainThreadMethod));
    this._thread.Name = "ECOServer service thread";
    this._thread.IsBackground = true;
    this._thread.Priority = ThreadPriority.BelowNormal;
    this._thread.Start();
  }

  public void _treadStop()
  {
    if (this._thread != null && this._thread.IsAlive)
      this._thread.Abort();
    this._thread = (Thread) null;
  }

  private void MainThreadMethod()
  {
    Thread.Sleep(TimeSpan.FromMinutes(5.0));
    while (true)
    {
      try
      {
        this.performTime();
      }
      catch (Exception ex)
      {
        if (ServerServices.GetService(typeof (IOutputView)) is IOutputView service)
        {
          service.WriteString(nameof (ECOServer), $"Exception source:\n{ex.Source}");
          service.WriteString(nameof (ECOServer), $"Exception message:\n{ex.Message}");
          service.WriteString(nameof (ECOServer), $"Exception stack:\n{ex.StackTrace}");
          if (ex.InnerException != null)
          {
            service.WriteString(nameof (ECOServer), $"Inner exception source:\n{ex.InnerException.Source}");
            service.WriteString(nameof (ECOServer), $"Inner exception message:\n{ex.InnerException.Message}");
            service.WriteString(nameof (ECOServer), $"Inner exception stack:\n{ex.InnerException.StackTrace}");
          }
        }
      }
      Thread.Sleep(new TimeSpan(1, 0, 0, 0));
    }
  }

  public string GetLitera(long objId, IUserSession ius)
  {
    IDBObject dbObject = ius.GetObject(objId, false);
    string litera = "";
    if (dbObject != null)
    {
      Guid attributeGuid = new Guid(ECOServer.GuidAttrLitera);
      IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(attributeGuid);
      if (attributeByGuid != null)
        litera = Convert.ToString(attributeByGuid.Value);
    }
    return litera;
  }

  internal void performTime()
  {
    IUserSession sessionTemporaryClone = ECOServer._idbTE.GetSystemSessionTemporaryClone("ECOServer.performTime");
    try
    {
      this.ep.LoadFromBase(sessionTemporaryClone);
      if (!this.ep.AutoMoveObjects)
      {
        if (!this.ep.WarnOnMove)
          goto label_83;
      }
      foreach (DataRow row in (InternalDataCollectionBase) this.GetAllECOs(sessionTemporaryClone).Rows)
      {
        long int64 = Convert.ToInt64(row[0]);
        if (int64 >= 0L)
        {
          int int32 = Convert.ToInt32(row[5]);
          int num = int32 == ECOServer.idDI ? 1 : (int32 == ECOServer.idDPI ? 1 : 0);
          DateTime result1 = new DateTime(3000, 12, 12);
          if (row[1] != null && row[1] != DBNull.Value)
            DateTime.TryParse(Convert.ToString(row[1]), out result1);
          DateTime result2 = new DateTime(3000, 12, 12);
          if (row[2] != null && row[2] != DBNull.Value)
            DateTime.TryParse(Convert.ToString(row[2]), out result2);
          if (num != 0)
          {
            DataTable dataTable = sessionTemporaryClone.GetRelationCollection(ECOServer.relTypeDI).ConsistFrom(new DBRecordSetParams((ConditionStructure[]) null, new object[4]
            {
              (object) -26,
              (object) -22,
              (object) -2,
              (object) -21
            }), int64);
            if (dataTable != null && dataTable.Rows.Count > 0)
              int64 = Convert.ToInt64(dataTable.Rows[0][2]);
            else
              continue;
          }
          else
          {
            long objectID = 0;
            if (row[3] != null && row[3] != DBNull.Value)
              objectID = Convert.ToInt64(row[3]);
            if (objectID != 0L)
            {
              IDBObject dbObject = sessionTemporaryClone.GetObject(objectID, false);
              if (dbObject != null)
              {
                IDBAttribute attributeById1 = dbObject.GetAttributeByID(this.attrChangeDateId);
                if (attributeById1 != null && attributeById1.Value != null && attributeById1.Value != DBNull.Value)
                  DateTime.TryParse(Convert.ToString(attributeById1.Value), out result1);
                IDBAttribute attributeById2 = dbObject.GetAttributeByID(this.attrChangeDateEndId);
                if (attributeById2 != null && attributeById2.Value != null && attributeById2.Value != DBNull.Value)
                  DateTime.TryParse(Convert.ToString(attributeById2.Value), out result2);
              }
            }
          }
          IDBObject revObj = sessionTemporaryClone.GetObject(int64);
          if (DateTime.Now > result1 && revObj.LCStep == this.lcWaitId)
          {
            List<ECOServer.IncludedObjInfo> objectsSteps = this.GetObjectsSteps(sessionTemporaryClone, int64);
            if (objectsSteps.Any<ECOServer.IncludedObjInfo>((System.Func<ECOServer.IncludedObjInfo, bool>) (ioi => ioi.Goal == ECOServer.EcoGoal.Litera)))
            {
              string litera = ECOServer.ecos.GetLitera(int64, sessionTemporaryClone);
              if (litera != "")
              {
                foreach (ECOServer.IncludedObjInfo includedObjInfo in objectsSteps)
                {
                  if (includedObjInfo.Goal == ECOServer.EcoGoal.Litera)
                    this.PerformSetLitera(sessionTemporaryClone, includedObjInfo.ObjId, includedObjInfo.AuxObjects, litera);
                }
              }
            }
            bool flag1 = false;
            bool failed = false;
            IDBAttribute byId = revObj.Attributes.FindByID(this.attrLCFailedId);
            if (byId != null && byId.AsBoolean)
              flag1 = true;
            if (!flag1)
            {
              if (ECOServer.ecos.AutoMoveObjects)
              {
                try
                {
                  if (revObj.ObjectType == ECOServer.idII)
                    this.DoSetEndTerms((UserSession) sessionTemporaryClone, objectsSteps, result1);
                  failed = !this.MoveObjects((UserSession) sessionTemporaryClone, objectsSteps);
                  if (!failed)
                    this.MoveAnnuledPI(revObj, sessionTemporaryClone);
                }
                catch (Exception ex)
                {
                  ((IEventLogHelper) this._serviceProvider.GetService(typeof (IEventLogHelper)))?.AddToTrace($"{string.Format(Intermech.Localization.LocalizationHolder.rm.GetString("ECO_Server10"), (object) int64)} : {ex.Message}", 0, "");
                  failed = true;
                }
                try
                {
                  if (failed)
                    throw new Exception("###");
                  this.lockDoNextLCStep = true;
                  revObj.LCStep = this.lcActualizeId;
                }
                catch (Exception ex)
                {
                  if (ex.Message != "###")
                    ((IEventLogHelper) this._serviceProvider.GetService(typeof (IEventLogHelper)))?.AddToTrace($"{string.Format(Intermech.Localization.LocalizationHolder.rm.GetString("ECO_Server10"), (object) int64)} : {ex.Message}", 0, "");
                  if (revObj.CheckoutBy == 0L)
                  {
                    if (revObj.ObjectModifyMode != ObjectModifyModes.CantModify)
                    {
                      bool flag2 = false;
                      if (revObj.ObjectModifyMode == ObjectModifyModes.Checkout)
                      {
                        revObj = revObj.CheckOut();
                        flag2 = true;
                      }
                      try
                      {
                        IDBAttribute dbAttribute = revObj.Attributes.AddAttribute(this.attrLCFailedId, false);
                        if (dbAttribute != null)
                          dbAttribute.AsBoolean = true;
                      }
                      finally
                      {
                        if (flag2)
                          revObj.CheckIn();
                      }
                    }
                  }
                }
                finally
                {
                  this.lockDoNextLCStep = false;
                }
              }
            }
            if (this.ep.WarnOnMove | failed)
            {
              List<long> objects = new List<long>(objectsSteps.Select<ECOServer.IncludedObjInfo, long>((System.Func<ECOServer.IncludedObjInfo, long>) (ioi => ioi.ObjId)));
              ECOServer.SendMessage(sessionTemporaryClone, revObj.OwnerID, objects, int64, this.ep.AutoMoveObjects, failed);
            }
          }
          if (DateTime.Now > result2 && revObj.LCStep == this.lcActualizeId)
          {
            List<ECOServer.IncludedObjInfo> objectsSteps = this.GetObjectsSteps(sessionTemporaryClone, int64);
            if (ECOServer.ecos.AutoMoveObjects)
            {
              try
              {
                this.MoveObjects((UserSession) sessionTemporaryClone, objectsSteps);
                foreach (ECOServer.IncludedObjInfo includedObjInfo in objectsSteps)
                {
                  IDBObject dbObject = sessionTemporaryClone.GetObject(includedObjInfo.ObjId, false);
                  if (dbObject != null && dbObject.LCStep == includedObjInfo.FutureStepId)
                    ((DBObject) dbObject).RemoveFromStep();
                }
              }
              catch (Exception ex)
              {
                ((IEventLogHelper) this._serviceProvider.GetService(typeof (IEventLogHelper)))?.AddToTrace($"{string.Format(Intermech.Localization.LocalizationHolder.rm.GetString("ECO_Server10"), (object) int64)} : {ex.Message}", 0, "");
              }
              try
              {
                this.lockDoNextLCStep = true;
                revObj.LCStep = this.levKeepingId;
              }
              catch (Exception ex)
              {
              }
              finally
              {
                this.lockDoNextLCStep = false;
              }
            }
          }
        }
      }
    }
    finally
    {
      sessionTemporaryClone?.Logout("ECOServer.performTime");
    }
label_83:
    if (this.ep.DaysBeforeEndTermWarning <= 0)
      return;
    this.NotifyEndingECOs(this.ep.DaysBeforeEndTermWarning);
  }

  internal List<ECOServer.IncludedObjInfo> GetObjectsSteps(IUserSession session, long ecoId)
  {
    List<ECOServer.IncludedObjInfo> objectsSteps = new List<ECOServer.IncludedObjInfo>();
    foreach (DataRow row in (InternalDataCollectionBase) this.GetAllRelations(session, ecoId).Rows)
    {
      long int64 = Convert.ToInt64(row[0]);
      IDBRelation relation = session.GetRelation(int64, false);
      if (relation != null)
      {
        IDBAttribute attributeById1 = relation.GetAttributeByID(this.idAttrVerId);
        long objId = attributeById1 != null ? attributeById1.AsInteger : -1L;
        IDBAttribute attributeById2 = relation.GetAttributeByID(this.attrFutureLCId);
        int futureStep = attributeById2 != null ? Convert.ToInt32(attributeById2.Value) : -1;
        IDBAttribute attributeById3 = relation.GetAttributeByID(this.attrIncludeGoalId);
        int goal = attributeById3 != null ? Convert.ToInt32(attributeById3.Value) : -1;
        List<long> auxObjects = (List<long>) null;
        IDBAttribute attributeById4 = relation.GetAttributeByID(this.attrAuxLinksId);
        if (attributeById4 != null && attributeById4.Values.Length != 0 && attributeById4.Values[0] != DBNull.Value)
        {
          auxObjects = new List<long>();
          foreach (object obj in attributeById4.Values)
            auxObjects.Add(Convert.ToInt64(obj));
        }
        objectsSteps.Add(new ECOServer.IncludedObjInfo(objId, futureStep, goal, auxObjects));
      }
    }
    return objectsSteps;
  }

  internal int GetAnnulStep(IUserSession ius, IDBObject annulPI)
  {
    IMSObjectType objectType = MetaDataHelper.GetObjectType(annulPI.ObjectType);
    IDBLCSchema lcSchema = ius.GetLCSchema(objectType.SchemaID, false);
    if (lcSchema != null)
    {
      DataTable table = lcSchema.GetStepsCollection().GetSchema().Tables["IMS_LC_STEPS"];
      DataRow[] dataRowArray1 = table.Select("F_LEVEL_ID = " + Convert.ToString(ECOServer.idLevelAnnuled));
      if (dataRowArray1 != null && dataRowArray1.Length != 0)
        return Convert.ToInt32(dataRowArray1[0]["F_LC_STEP"]);
      DataRow[] dataRowArray2 = table.Select("F_LEVEL_ID = " + Convert.ToString(ECOServer.idLevelKeeping));
      if (dataRowArray2 != null && dataRowArray2.Length != 0)
        return Convert.ToInt32(dataRowArray2[0]["F_LC_STEP"]);
    }
    return -1;
  }

  internal bool MoveObjects(UserSession systemSession, List<ECOServer.IncludedObjInfo> objInfoList)
  {
    systemSession.StartTransaction();
    try
    {
      List<ECOServer.IncludedObjInfo> source = (List<ECOServer.IncludedObjInfo>) null;
      foreach (ECOServer.IncludedObjInfo objInfo in objInfoList)
      {
        if (objInfo.ObjId != -1L && objInfo.FutureStepId != -1)
        {
          IDBObject dbObject = systemSession.GetObject(objInfo.ObjId, false);
          if (dbObject != null)
          {
            if (!this.IsAnnulStep(systemSession, objInfo.FutureStepId))
            {
              dbObject.LCStep = objInfo.FutureStepId;
            }
            else
            {
              if (source == null)
                source = new List<ECOServer.IncludedObjInfo>();
              source.Add(objInfo);
            }
          }
        }
      }
      if (source != null)
      {
        List<long> objs = new List<long>(source.Select<ECOServer.IncludedObjInfo, long>((System.Func<ECOServer.IncludedObjInfo, long>) (ioi => ioi.ObjId)));
        List<int> stepIds = new List<int>(source.Select<ECOServer.IncludedObjInfo, int>((System.Func<ECOServer.IncludedObjInfo, int>) (ioi => ioi.FutureStepId)));
        this.PerformAnnul(systemSession, objs, stepIds);
      }
      systemSession.Commit();
      return true;
    }
    catch
    {
      systemSession.Rollback();
      throw;
    }
  }

  internal bool SetEndTerm4PrevVersions(
    UserSession systemSession,
    List<long> objs,
    DateTime revStartDate)
  {
    systemSession.StartTransaction();
    try
    {
      foreach (long id in objs)
      {
        DataTable allObjectVersions = systemSession.GetAllObjectVersions(id, false, false, false, Array.Empty<string>());
        if (allObjectVersions != null && allObjectVersions.Rows.Count > 0)
        {
          foreach (DataRow row in (InternalDataCollectionBase) allObjectVersions.Rows)
          {
            if (Convert.ToInt32(row["F_LEVEL_ID"]) == ECOServer.idLevelProduction)
            {
              long int64 = Convert.ToInt64(row["F_OBJECT_ID"]);
              IDBObject dbObject = systemSession.GetObject(int64, false);
              if (dbObject != null)
              {
                IDBAttribute attributeById = dbObject.GetAttributeByID(this.attrChangeDateEndId);
                if (attributeById != null)
                {
                  attributeById.AsDateTime = revStartDate;
                  break;
                }
                break;
              }
              break;
            }
          }
        }
      }
      systemSession.Commit();
      return true;
    }
    catch
    {
      systemSession.Rollback();
      return false;
    }
  }

  internal bool DoSetEndTerms(
    UserSession systemSession,
    List<ECOServer.IncludedObjInfo> incList,
    DateTime revStartDate)
  {
    if (revStartDate.Year == 3000)
      return false;
    List<long> objs = new List<long>();
    foreach (ECOServer.IncludedObjInfo inc in incList)
    {
      if (inc.Goal == ECOServer.EcoGoal.Change || inc.Goal == ECOServer.EcoGoal.Replace)
        objs.Add(inc.ObjId);
    }
    return objs.Count <= 0 || this.SetEndTerm4PrevVersions(systemSession, objs, revStartDate);
  }

  public bool MoveAnnuledPI(IDBObject revObj, IUserSession session)
  {
    IDBAttribute attributeById = revObj.GetAttributeByID(ECOServer.idAttrLinkToAnnuledPI);
    if (attributeById != null && attributeById.Value != null)
    {
      long int64 = Convert.ToInt64(attributeById.Value);
      if (int64 != 0L)
      {
        IDBObject annulPI = session.GetObject(int64, false);
        if (annulPI != null)
        {
          int annulStep = this.GetAnnulStep(session, annulPI);
          if (annulStep != -1)
          {
            try
            {
              annulPI.LCStep = annulStep;
            }
            catch
            {
              List<long> objects = new List<long>()
              {
                int64
              };
              ECOServer.SendMessage(session, revObj.OwnerID, objects, revObj.ObjectID, this.ep.AutoMoveObjects, true);
            }
          }
        }
        return true;
      }
    }
    return false;
  }

  internal bool IsAnnulStep(UserSession ius, int stepId)
  {
    IDBLifecycleStep lifecycleStep = ius.GetLifecycleStep(stepId);
    if (lifecycleStep == null)
      return false;
    IDBLifecycleLevelType lifecycleLevel = ius.GetLifecycleLevel(lifecycleStep.LevelID);
    return lifecycleLevel != null && lifecycleLevel.LevelID == ius.IdentHelper.AnnulmentLevelID;
  }

  internal void PerformAnnul(UserSession ius, List<long> objs, List<int> stepIds)
  {
    List<List<long>> longListList = new List<List<long>>();
    foreach (long verId in objs)
    {
      List<long> verIDs = new List<long>();
      ECOServer.GetEntersInObjects(ius, verId, ref verIDs);
      longListList.Add(verIDs);
    }
    List<int> intList = new List<int>();
    for (int index = 0; index < objs.Count; ++index)
      intList.Add(index);
    for (int index1 = objs.Count - 2; index1 >= 0; --index1)
    {
      for (int index2 = index1; index2 < objs.Count - 1; ++index2)
      {
        Math.Abs(objs[intList[index2]]);
        long num1 = Math.Abs(objs[intList[index2 + 1]]);
        if (longListList[intList[index2]].IndexOf(num1) >= 0)
        {
          int num2 = intList[index2];
          intList[index2] = intList[index2 + 1];
          intList[index2 + 1] = num2;
        }
      }
    }
    foreach (int index in intList)
    {
      long objectID = objs[index];
      int stepId = stepIds[index];
      IDBObject dbObject = ius.GetObject(objectID, false);
      if (dbObject != null)
        dbObject.LCStep = stepId;
    }
  }

  public void PerformSetLitera(
    IUserSession session,
    long objId,
    List<long> auxObjects,
    string litera)
  {
    this._SetLitera(session, objId, litera);
    if (auxObjects == null)
      return;
    foreach (long auxObject in auxObjects)
      this._SetLitera(session, auxObject, litera);
  }

  protected void _SetLitera(IUserSession session, long verId, string litera)
  {
    ECOServer.ecos.SetLitera(session.SessionGUID, verId, litera);
    IDBRelationCollection relationCollection1 = session.GetRelationCollection(this.idLinkProject);
    DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    });
    relationCollection1.LocalTypesMode = true;
    DataTable dataTable1 = relationCollection1.ConsistFrom(paramSet, verId);
    if (dataTable1 != null)
    {
      foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
      {
        long int64 = Convert.ToInt64(row[0]);
        ECOServer.ecos.SetLitera(session.SessionGUID, int64, litera);
      }
    }
    IDBRelationCollection relationCollection2 = session.GetRelationCollection(this.idLinkDoc);
    paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    });
    relationCollection2.LocalTypesMode = true;
    DataTable dataTable2 = relationCollection2.ConsistFrom(paramSet, verId);
    if (dataTable2 == null)
      return;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
    {
      long int64 = Convert.ToInt64(row[0]);
      ECOServer.ecos.SetLitera(session.SessionGUID, int64, litera);
    }
  }

  internal List<ResultEcoDocumentsInformation> notifSS_GetEcoDocumentsListEvent(
    EcoDocumentsInAttachments attachmentsDoc)
  {
    DataTable dataTable = (DataTable) null;
    IUserSession sessionTemporaryClone = ECOServer._idbTE.GetSystemSessionTemporaryClone("ECOServer.notifSS");
    try
    {
      IDBRelationCollection relationCollection = sessionTemporaryClone.GetRelationCollection(LinkIzvObject.relTypeECO);
      relationCollection.LocalTypesMode = true;
      dataTable = relationCollection.ConsistFrom(new DBRecordSetParams((ConditionStructure[]) null, new object[8]
      {
        (object) -20,
        (object) -22,
        (object) -2,
        (object) -21,
        (object) LinkIzvObject.attrVerId,
        (object) this.attrIncludeGoalId,
        (object) -6,
        (object) -7
      }), attachmentsDoc.EcoObjectID);
    }
    finally
    {
      sessionTemporaryClone?.Logout("ECOServer.notifSS");
    }
    bool createLiteraVersion = ECOServer.ecos.ep.CreateLiteraVersion;
    List<ResultEcoDocumentsInformation> documentsListEvent = new List<ResultEcoDocumentsInformation>();
    if (dataTable != null && dataTable.Rows.Count > 0)
    {
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        if (row[5] != null && row[5] != DBNull.Value)
        {
          switch (Convert.ToInt32(row[5]))
          {
            case 1:
              continue;
            case 2:
              if (!createLiteraVersion)
                continue;
              break;
          }
          ResultEcoDocumentsInformation documentsInformation = new ResultEcoDocumentsInformation();
          if (row[1] != null && row[1] != DBNull.Value)
            documentsInformation.ID = Convert.ToInt64(row[1]);
          if (row[4] != null && row[4] != DBNull.Value)
            documentsInformation.ObjectID = Convert.ToInt64(row[4]);
          if (row[6] != null && row[6] != DBNull.Value)
            documentsInformation.CheckOutBy = Convert.ToInt64(row[6]);
          if (row[7] != null && row[7] != DBNull.Value)
            documentsInformation.ObjectType = Convert.ToInt32(row[7]);
          documentsListEvent.Add(documentsInformation);
        }
      }
    }
    return documentsListEvent;
  }

  public static void DeleteObject(IDBObject dBObject, long deleteMode)
  {
    long objectId = dBObject.ObjectID;
    if (ECOServer.deletingObjects.Contains(objectId))
      return;
    ECOServer.deletingObjects.Add(objectId);
    try
    {
      dBObject.Delete(deleteMode);
    }
    finally
    {
      ECOServer.deletingObjects.Remove(objectId);
    }
  }

  public IDBRelation CreateRevLink(
    IUserSession session,
    long revId,
    long objVerId,
    bool delOnExclude = true,
    long futureStepId = 0,
    string changeNum = "",
    ECOGoal goal = ECOGoal.Change,
    HidingType hType = HidingType.CanBeHidden,
    IEnumerable<long> auxObjects = null)
  {
    IDBRelationCollection relationCollection = session.GetRelationCollection(this.idLinkRevision);
    List<AttributeValues> attributeValuesList = new List<AttributeValues>();
    attributeValuesList.Add(new AttributeValues(this.attrChangeNo, (object) changeNum));
    attributeValuesList.Add(new AttributeValues(this.attrIncludeGoalId, (object) (int) goal));
    attributeValuesList.Add(new AttributeValues(this.attrHidingId, (object) (int) hType));
    attributeValuesList.Add(new AttributeValues(this.attrFutureLCId, (object) futureStepId));
    attributeValuesList.Add(new AttributeValues(this.attrDelWhenExcluded, (object) delOnExclude));
    if (auxObjects != null)
    {
      object[] array = auxObjects.ToArray<long>().Cast<object>().ToArray<object>();
      attributeValuesList.Add(new AttributeValues(this.attrAuxLinksId, FieldTypes.ftObjectLink, MultiValueModes.MultiValues, array));
    }
    AttributeValues[] array1 = attributeValuesList.ToArray();
    NewRelationProperties properties = new NewRelationProperties(-1L, revId, 0L, DateTime.Now, DateTime.MaxValue, objVerId, array1);
    return relationCollection.Create(properties);
  }

  internal class DeletingPackage
  {
    public bool wasDeleted;
    public HashSet<long> models;

    public long DeleteMode { get; set; }

    public DeletingPackage(long delMode)
    {
      this.models = new HashSet<long>();
      this.DeleteMode = delMode;
    }

    public DeletingPackage(long delMode, long modelId)
    {
      this.models = new HashSet<long>();
      this.models.Add(modelId);
      this.DeleteMode = delMode;
    }

    public void AddModelId(long modelId)
    {
      if (this.models.Contains(modelId))
        return;
      this.models.Add(modelId);
    }

    public bool NotEmpty() => this.models != null && this.models.Count > 0;
  }

  public enum EcoGoal
  {
    NoGoal = -1, // 0xFFFFFFFF
    Change = 0,
    Annul = 1,
    Litera = 2,
    Replace = 3,
    Creation = 4,
  }

  public class IncludedObjInfo
  {
    public long ObjId { get; set; }

    public int FutureStepId { get; set; }

    public ECOServer.EcoGoal Goal { get; set; }

    public List<long> AuxObjects { get; }

    public string PrevLitera { get; set; }

    public IncludedObjInfo(long objId, int futureStep, int goal, List<long> auxObjects)
    {
      this.ObjId = objId;
      this.FutureStepId = futureStep;
      this.Goal = (ECOServer.EcoGoal) goal;
      this.AuxObjects = auxObjects;
      this.PrevLitera = "";
    }
  }
}
