// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Server.OfficeRegistrationService
// Assembly: Intermech.Office.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 414402D9-801C-4C77-86BA-4C6FCAC834BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Office.Server.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Office.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Threading;

#nullable disable
namespace Intermech.Office.Server;

internal sealed class OfficeRegistrationService : LongLifeObject, IOfficeRegistrationService
{
  [NotNull]
  private ConcurrentDictionary<long, long> _usersUnitCache = new ConcurrentDictionary<long, long>();
  private volatile bool _loading;
  private volatile bool _needReload;
  [NotNull]
  private readonly Timer _timer;
  private const int CacheReloadPeriod = 10;
  [CanBeNull]
  internal OfficeCacheSynchronizer _ServersSynchronizer;

  public OfficeRegistrationService()
  {
    IEventLogHelper service = ApplicationServices.Container.GetService<IEventLogHelper>();
    service.AfterCacheReload += new CacheReloadHandler(this.eventLogHelper_AfterCacheReload);
    service.AfterDeleteRelationEvent += new DeleteRelationHandler(this.EventAfterDeleteRelation);
    service.AfterCreateRelationExEvent += new CreateRelationExHandler(this.EventAfterCreateRelation);
    service.AddAttributeWriteHandler((object) OfficeConsts.AttrSelfOfficeID, new WriteAttributeValueHandler(this.WriteAttrSelfOfficeHandler));
    service.AfterRemoveRelationEvent += new RemoveRelationHandler(this.EventAfterRemoveRelation);
    this._timer = new Timer(new TimerCallback(this.TimerCacheReload), (object) null, TimeSpan.FromMinutes(10.0), TimeSpan.FromMinutes(10.0));
  }

  private void EventAfterRemoveRelation([NotNull] IDBRelation sender, [NotNull] IUserSession session)
  {
    if (sender.RelationType != session.IdentHelper.SimpleRelationTypeID || !this.IsNeedUpdateType(sender.As<DBRelation>().ProjObject.ObjectType, session))
      return;
    this._needReload = true;
  }

  private void WriteAttrSelfOfficeHandler([NotNull] IDBAttribute attribute, [NotNull] AttributeValueEventArgs args)
  {
    this._needReload = true;
  }

  private void eventLogHelper_AfterCacheReload([NotNull] IDbManager db) => this.ReloadCache();

  private bool IsNeedUpdateType(int objectTypeID, [NotNull] IUserSession session)
  {
    return objectTypeID == session.IdentHelper.GroupsTypeID || MetaDataHelper.IsObjectTypeChildOf(objectTypeID, OfficeConsts.ObjtypeOrganizationUnitsID);
  }

  private void EventAfterDeleteRelation([NotNull] IDBRelation sender, long deleteMode, [NotNull] IUserSession session)
  {
    if (sender.RelationType != session.IdentHelper.SimpleRelationTypeID || !this.IsNeedUpdateType(sender.As<DBRelation>().ProjObject.ObjectType, session))
      return;
    this._needReload = true;
  }

  private void EventAfterCreateRelation([NotNull] IDBRelation sender, [NotNull] IUserSession session, int assignMode)
  {
    if (sender.RelationType != session.IdentHelper.SimpleRelationTypeID || !this.IsNeedUpdateType(((DBRelation) sender).ProjObject.ObjectType, session))
      return;
    this._needReload = true;
  }

  public void InitCacheReload() => this._needReload = true;

  private void TimerCacheReload([CanBeNull] object state)
  {
    if (!this._needReload)
      return;
    this.ReloadCache();
  }

  private void ReloadCache()
  {
    IUserSession sessionTemporaryClone = ApplicationServices.Container.GetService<IDBTimedEvents>().GetSystemSessionTemporaryClone("OfficeServer.ReloadCache");
    try
    {
      this.LoadCache(sessionTemporaryClone);
      if (!this._needReload)
        return;
      Intermech.Diagnostics.Check.NotNull<OfficeCacheSynchronizer>(this._ServersSynchronizer, "_ServersSynchronizer");
      this._ServersSynchronizer.AddEvent(string.Empty, ((UserSession) sessionTemporaryClone).DataManager);
    }
    finally
    {
      sessionTemporaryClone.Logout("OfficeServer.ReloadCache");
    }
  }

  public void LoadCache([NotNull] IUserSession session)
  {
    if (this._loading)
      return;
    try
    {
      ConcurrentDictionary<long, long> concurrentDictionary = new ConcurrentDictionary<long, long>();
      DataTable dataTable1 = session.GetObjectCollection(OfficeConsts.ObjtypeOrganizationUnitsID).SelectWithLocalObjects(new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(OfficeConsts.AttrSelfOfficeID, RelationalOperators.Equal, (object) true, LogicalOperators.NONE, 0, false)
      }, new object[2]{ (object) -2, (object) -7 }));
      if (dataTable1.Rows.Count > 0)
      {
        ICompositionLoadService service = ApplicationServices.Container.GetService<ICompositionLoadService>();
        List<ColumnDescriptor> columns = new List<ColumnDescriptor>()
        {
          new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Name, SortOrders.NONE, 0)
        };
        List<int> searchRelationTypes = new List<int>((IEnumerable<int>) new int[1]
        {
          OfficeConsts.ReltypeSimpleID
        });
        List<int> searchObjectTypes = new List<int>((IEnumerable<int>) new int[1]
        {
          session.IdentHelper.UsersTypeID
        });
        for (int index1 = 0; index1 < dataTable1.Rows.Count; ++index1)
        {
          long int64_1 = Convert.ToInt64(dataTable1.Rows[index1][0]);
          DataTable dataTable2 = service.LoadComposition((object) session, int64_1, Convert.ToInt32(dataTable1.Rows[index1][1]), (IEnumerable<int>) searchRelationTypes, (IEnumerable<int>) searchObjectTypes, (IEnumerable<ColumnDescriptor>) columns, true, false, (VersionsRule) null, (IEnumerable<ConditionStructure>) null, string.Empty, (HybridDictionary) null, -1);
          if (dataTable2 != null)
          {
            for (int index2 = 0; index2 < dataTable2.Rows.Count; ++index2)
            {
              long int64_2 = Convert.ToInt64(dataTable2.Rows[index2][0]);
              if (!concurrentDictionary.TryGetValue(int64_2, out long _))
                concurrentDictionary[int64_2] = int64_1;
            }
          }
        }
      }
      this._usersUnitCache = concurrentDictionary;
      this._loading = true;
    }
    finally
    {
      this._loading = false;
      this._needReload = false;
    }
  }

  public long GetUserUnit(long userID)
  {
    long num;
    return !this._usersUnitCache.TryGetValue(userID, out num) ? 0L : num;
  }

  public bool PrivateRegister([NotEmpty] Guid sessionGuid, long unitID, [NotEmpty] long documentID, [NotNull] string regNumber)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    IDBTransactions customService = (IDBTransactions) sessionById.GetCustomService(typeof (IDBTransactions));
    customService.StartTransaction();
    try
    {
      bool flag1 = false;
      IDBObject dbObject = sessionById.GetObject(documentID);
      if (dbObject.CheckoutBy == 0L && dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
      {
        dbObject = dbObject.CheckOut(true);
        flag1 = true;
      }
      bool flag2 = false;
      IDBAttribute dbAttribute = dbObject.GetAttributeByID(OfficeConsts.AttrPrivateRegNumberID);
      if (dbAttribute == null)
      {
        dbAttribute = dbObject.Attributes.AddAttribute(OfficeConsts.AttrPrivateRegNumberID, false);
        flag2 = true;
      }
      else if (dbAttribute.IsNull)
        flag2 = true;
      if (flag2)
        dbAttribute.Value = (object) regNumber;
      else
        dbAttribute.AddValue((object) regNumber);
      (dbObject.GetAttributeByID(OfficeConsts.AttrIsPrivateRegisterID) ?? dbObject.Attributes.AddAttribute(OfficeConsts.AttrIsPrivateRegisterID, false)).AsBoolean = true;
      if (flag1)
        dbObject.CheckIn();
      ApplicationServices.Container.GetService<IFiltrationTableService>().AddOrUpdateValue(((UserSession) sessionById).DataManager, Math.Abs(documentID), Math.Abs(unitID), regNumber);
      customService.Commit();
      return true;
    }
    catch
    {
      customService.Rollback();
      throw;
    }
  }

  public bool IsDocumentPrivateRegister(Guid sessionGuid, long unitID, long documentID)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    return ApplicationServices.Container.GetService<IFiltrationTableService>().GetValue(((UserSession) sessionById).DataManager, Math.Abs(documentID), Math.Abs(unitID)) != string.Empty;
  }

  public bool IsDocumentRegister(Guid sessionGuid, long documentID)
  {
    IDBObject dbObject = UserSession.GetSessionByID(sessionGuid).GetObject(documentID);
    IDBAttribute attributeById1 = dbObject.GetAttributeByID(OfficeConsts.AttrRegNumberID);
    IDBAttribute attributeById2 = dbObject.GetAttributeByID(OfficeConsts.AttrRegistrationDateID);
    return attributeById1 != null && attributeById2 != null && !attributeById2.IsNull;
  }

  public string GetPrivateRegNumber(Guid sessionGuid, long documentID)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    long userUnit = sessionById.GetCustomService<IOfficeRegistrationService>().GetUserUnit(sessionById.UserID);
    return userUnit == 0L ? string.Empty : ApplicationServices.Container.GetService<IFiltrationTableService>().GetValue(((UserSession) sessionById).DataManager, Math.Abs(documentID), Math.Abs(userUnit));
  }

  public void UpdatePrivateRegNumber(Guid sessionGuid, long documentID, string regNumber)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    long userUnit = sessionById.GetCustomService<IOfficeRegistrationService>().GetUserUnit(sessionById.UserID);
    if (userUnit == 0L)
      return;
    ApplicationServices.Container.GetService<IFiltrationTableService>().UpdateValue(((UserSession) sessionById).DataManager, Math.Abs(documentID), Math.Abs(userUnit), regNumber);
  }
}
