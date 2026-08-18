// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.ImbaseIndexSecurity
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase.Params;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Localization;

#nullable disable
namespace Intermech.Imbase.Server;

public class ImbaseIndexSecurity : 
  DBSessionable,
  IDBSecurityCollection,
  IDBSecurity,
  IDBNamedSecurityCollection
{
  private long _catalogId;
  private string _catalogName;
  private int _attributeId;
  private string _collectionName;

  public ImbaseIndexSecurity(UserSession uSession, long catalogId, int attributeId)
    : base(uSession)
  {
    this._catalogId = catalogId;
    this._attributeId = attributeId;
    this._catalogName = uSession.GetObjectInfo(catalogId).Caption;
    this.InitSecurityOptions(30, ImbaseHelper.CreateCategoryId(this._catalogId, (long) this._attributeId));
  }

  public override ActionCategory GetActionCategory(ActionType actionType)
  {
    switch (actionType)
    {
      case ActionType.ShowNonApplicabilityImbaseRecords:
      case ActionType.ShowNonVisibleColumnImbaseRecords:
      case ActionType.ShowNonVisibleRowImbaseRecords:
        return ActionCategory.Read;
      default:
        return base.GetActionCategory(actionType);
    }
  }

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    base.InitSecurityOptions(aCategoryType, aCategoryID);
    this.AccessActions.Add(ActionType.ShowNonApplicabilityImbaseRecords, true);
    if (!ServiceUtils.GetService<IImbaseParamsService>((object) ServerServices.ServiceContainer, true).CommonParams.UseExtendedSecurityCheckForIndexes)
      return;
    this.AccessActions.Add(ActionType.ShowNonVisibleColumnImbaseRecords, true);
    this.AccessActions.Add(ActionType.ShowNonVisibleRowImbaseRecords, true);
    this.AccessActions.Add(ActionType.ShowNonUseImbaseRecords, true);
  }

  public override string ObjectName
  {
    get
    {
      return string.Format(LocalizationHolder.rm.GetString("Imbase.Server_37"), (object) MetaDataHelper.GetAttributeTypeName(this._attributeId), (object) this._catalogName);
    }
  }

  public void PurgeSecurity() => this.PurgeAccess();

  public void SetCollectionName(string name) => this._collectionName = name;

  protected override IDBSecurity GetSecurityByID(long categoryId)
  {
    long objectId;
    int id;
    ImbaseHelper.GetObjectAndId(categoryId, out objectId, out id);
    return (IDBSecurity) new ImbaseIndexSecurity(this.Session as UserSession, objectId, id);
  }

  public override string SecurityCollectionName
  {
    get
    {
      return string.IsNullOrEmpty(this._collectionName) ? string.Format(LocalizationHolder.rm.GetString("Imbase.Server_37"), (object) MetaDataHelper.GetAttributeTypeName(this._attributeId), (object) this._catalogName) : this._collectionName;
    }
  }

  public override bool IsCompatibleElements(long[] categoryID) => true;
}
