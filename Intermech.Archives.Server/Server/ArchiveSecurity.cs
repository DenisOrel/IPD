// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Server.ArchiveSecurity
// Assembly: Intermech.Archives.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2799C6CB-9B1D-4DB5-A12D-8C5FBFCAD6E5
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Archives.Server.dll

using Intermech.Interfaces;
using Intermech.Kernel;

#nullable disable
namespace Intermech.Archives.Server;

internal class ArchiveSecurity : DBSessionable, IDBSecurityCollection, IDBSecurity
{
  private ArchiveDBObject _Archive;
  private bool _ArchiveAccessMode;

  public ArchiveSecurity(UserSession session, ArchiveDBObject archive, bool archiveAccessMode)
    : base(session)
  {
    this._Archive = archive;
    this._ArchiveAccessMode = archiveAccessMode;
    this.UseAccessCache = false;
    this.InitSecurityOptions(17, archive.ObjectID);
  }

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    base.InitSecurityOptions(aCategoryType, aCategoryID);
    this.AccessActions.Add(ActionType.Create, true);
    this.AccessActions.Add(ActionType.Edit, true);
    this.AccessActions.Add(ActionType.View, true);
    this.AccessActions.Add(ActionType.Print, true);
    this.AccessActions.Add(ActionType.SaveToDisk, true);
    this.AccessActions.Add(ActionType.Delete, true);
    this.AccessActions.Add(ActionType.Remove, true);
    this.AccessActions.Add(ActionType.Purge, true);
    this.AccessActions.Add(ActionType.NextLCStep, true);
    this.AccessActions.Add(ActionType.TakeOwnership, false);
    this.AccessActions.Add(ActionType.ChangeBaseVersion, false);
    this.AccessActions.Add(ActionType.ChangeAccessLevel, false);
    this.AccessActions.Add(ActionType.DocRegistry, false);
  }

  public override bool CheckAccess(
    ActionType anAction,
    bool aDefaultAccess,
    CheckAccessFlags flags)
  {
    return this._ArchiveAccessMode && (anAction == ActionType.GetAccess || anAction == ActionType.SetAccess) || base.CheckAccess(anAction, aDefaultAccess, flags);
  }

  public override string ObjectName
  {
    get
    {
      return string.Format(ArchivesServerHolder.rm.GetString("Archives.Server_1"), (object) this._Archive.Caption);
    }
  }

  public override long AccessOwnerID
  {
    get
    {
      return this._Archive != null && this._Archive._ArchivedObject != null ? this._Archive._ArchivedObject.OwnerID : base.AccessOwnerID;
    }
  }

  protected override string GetExtendedAccessSQL()
  {
    string extendedAccessSql = base.GetExtendedAccessSQL();
    if (this._Archive != null && this._Archive._ArchivedObject != null && this._Archive._ArchivedObject.CreatorID == this.UserSession.UserID)
      extendedAccessSql = !(extendedAccessSql == string.Empty) ? $"{extendedAccessSql},{this.UserSession.IdentHelper.ObjectCreatorGroupID.ToString()}" : this.UserSession.IdentHelper.ObjectCreatorGroupID.ToString();
    return extendedAccessSql;
  }

  public override long ObjectID => this._CategoryID;

  public override bool EnabledConditionAccess => true;

  protected override IDBSecurity GetSecurityByID(long categoryID)
  {
    if (!(this.UserSession.GetObject(categoryID) is ArchiveDBObject archiveDbObject))
      throw new KernelException(string.Format(ArchivesServerHolder.rm.GetString("ObjectNotArchive"), (object) categoryID));
    return archiveDbObject.AccessChecker;
  }

  public override string SecurityCollectionName
  {
    get => ArchivesServerHolder.rm.GetString("DocumentsCollectionName");
  }
}
