// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Projects.ProjectDBSecurity
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Localization;


namespace Intermech.Kernel.Projects;

internal class ProjectDBSecurity : DBSessionable, IDBSecurity
{
  private DBProjectObject _Project;
  private DBObject _ObjectInProject;
  private bool _ProjectAccessMode;

  public ProjectDBSecurity(
    UserSession session,
    DBProjectObject project,
    DBObject objectInProject,
    bool projectAccessMode)
    : base(session)
  {
    this._Project = project;
    this._ObjectInProject = objectInProject;
    this._ProjectAccessMode = projectAccessMode;
    this.InitSecurityOptions(18, project.ObjectID);
  }

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    base.InitSecurityOptions(aCategoryType, aCategoryID);
    this.AccessActions.Add(ActionType.Create, true);
    this.AccessActions.Add(ActionType.Edit, true);
    this.AccessActions.Add(ActionType.View, true);
    this.AccessActions.Add(ActionType.Delete, true);
    this.AccessActions.Add(ActionType.Remove, true);
    this.AccessActions.Add(ActionType.Purge, true);
    this.AccessActions.Add(ActionType.NextLCStep, true);
    this.AccessActions.Add(ActionType.TakeOwnership, false);
    this.AccessActions.Add(ActionType.ChangeBaseVersion, false);
    this.AccessActions.Add(ActionType.ChangeAccessLevel, false);
  }

  public override bool CheckAccess(
    ActionType anAction,
    bool aDefaultAccess,
    CheckAccessFlags flags)
  {
    return this._ProjectAccessMode && (anAction == ActionType.GetAccess || anAction == ActionType.SetAccess) || base.CheckAccess(anAction, aDefaultAccess, flags);
  }

  public override string ObjectName
  {
    get
    {
      return string.Format(LocalizationHolder.rm.GetString("Kernel_492"), (object) this._Project.Caption);
    }
  }

  public override long AccessOwnerID
  {
    get => this._ObjectInProject != null ? this._ObjectInProject.OwnerID : base.AccessOwnerID;
  }

  protected override string GetExtendedAccessSQL()
  {
    string extendedAccessSql = base.GetExtendedAccessSQL();
    if (this._ObjectInProject != null)
      extendedAccessSql = this._ObjectInProject.GetObjectExtendedAccessSQL(extendedAccessSql);
    return extendedAccessSql;
  }
}
