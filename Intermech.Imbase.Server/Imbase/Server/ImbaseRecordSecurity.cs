// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.ImbaseRecordSecurity
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Kernel;

#nullable disable
namespace Intermech.Imbase.Server;

internal class ImbaseRecordSecurity : 
  DBSessionable,
  IDBSecurityCollection,
  IDBSecurity,
  IDBNamedSecurityCollection
{
  private DBObject _table;
  private long _recordId;
  private string _collectionName;

  public ImbaseRecordSecurity(UserSession session, DBObject table, long recordId)
    : base(session)
  {
    this._table = table;
    this._recordId = recordId;
    this.InitSecurityOptions(25, ImbaseHelper.CreateCategoryId(this._table.ObjectID, this._recordId));
  }

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    base.InitSecurityOptions(aCategoryType, aCategoryID);
    this.AccessActions.Add(ActionType.Edit, true);
    this.AccessActions.Add(ActionType.View, true);
    this.AccessActions.Add(ActionType.Print, true);
    this.AccessActions.Add(ActionType.Delete, true);
    this.AccessActions.Add(ActionType.Remove, true);
    this.AccessActions.Add(ActionType.ChangeAccessLevel, false);
    this.AccessActions.Add(ActionType.Use, true);
  }

  public override ActionCategory GetActionCategory(ActionType actionType)
  {
    return actionType == ActionType.Use ? ActionCategory.Read : base.GetActionCategory(actionType);
  }

  public override bool CheckAccess(
    ActionType anAction,
    bool aDefaultAccess,
    CheckAccessFlags flags)
  {
    return base.CheckAccess(anAction, aDefaultAccess, flags);
  }

  public override string ObjectName
  {
    get => $"Права доступа к записи [{this._recordId}] таблицы  '{this._table.ObjectID}'";
  }

  public override long AccessOwnerID
  {
    get => this._table != null ? this._table.OwnerID : base.AccessOwnerID;
  }

  protected override IDBSecurity GetSecurityByID(long categoryId)
  {
    long objectId;
    int id;
    ImbaseHelper.GetObjectAndId(categoryId, out objectId, out id);
    return (IDBSecurity) new ImbaseRecordSecurity(this.Session as UserSession, this.Session.GetObject(objectId) as DBObject, (long) id);
  }

  public override string SecurityCollectionName
  {
    get
    {
      return !string.IsNullOrEmpty(this._collectionName) ? this._collectionName : $"Права доступа к записям таблицы '{this._table.ObjectID}'";
    }
  }

  public override bool IsCompatibleElements(long[] categoryID) => true;

  public void SetCollectionName(string name) => this._collectionName = name;

  internal int LoadCache(ActionType actionType)
  {
    return this.LoadCacheTable(actionType, ImbaseHelper.MinCategoryId(this._table.ObjectID), ImbaseHelper.MaxCategoryId(this._table.ObjectID));
  }

  internal void SetRecordId(long recordId)
  {
    this._recordId = recordId;
    this._CategoryID = ImbaseHelper.CreateCategoryId(this._table.ObjectID, this._recordId);
  }

  internal void SetCategoryId(long categoryId) => this._CategoryID = categoryId;

  internal void PurgeAllAccessData()
  {
    this.PurgeAccess(this.CategoryType, ImbaseHelper.MinCategoryId(this._table.ObjectID), ImbaseHelper.MaxCategoryId(this._table.ObjectID));
  }
}
