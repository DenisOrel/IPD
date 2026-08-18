// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Setup.ToolSecurityContext
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Security;
using System;

#nullable disable
namespace Intermech.Tools.Setup;

internal sealed class ToolSecurityContext
{
  private ToolSecurityGroup securityGroup;
  private ToolSecurityRights securityRights;
  private TargetDescriptor activeTarget;
  private bool canEditPublicSettings;
  private bool canOverrideTarget;
  private bool canEditTargetSettings;

  public ToolSecurityContext()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IToolSecurity service = ServiceUtils.GetService<IToolSecurity>((object) sessionKeeper.Session, true);
      this.securityGroup = service.GetUserGroup();
      this.securityRights = service.GetUserRights();
      this.canEditPublicSettings = (this.securityRights & ToolSecurityRights.EditPublicSettings) != 0;
      this.canOverrideTarget = (this.securityRights & ToolSecurityRights.OverridePersonalSettings) != 0;
      this.canEditTargetSettings = (this.securityRights & ToolSecurityRights.EditPersonalSettings) != 0;
    }
    this.activeTarget = this.securityGroup == ToolSecurityGroup.Administrator ? TargetDescriptor.PublicSettings : TargetDescriptor.CurrentUser;
  }

  public TargetDescriptor ActiveTarget
  {
    get => this.activeTarget;
    set
    {
      if (value == null)
        throw new ArgumentNullException();
      if (value.Target.Equals((object) this.activeTarget.Target))
        return;
      if (this.canOverrideTarget)
      {
        this.activeTarget = value;
      }
      else
      {
        IPSPrincipal currentPrincipal = IPSPrincipal.CurrentPrincipal;
        throw new InvalidOperationException(string.Format(LocalizationHolder.rm.GetString("Tools.Client_178"), (object) currentPrincipal.Identity.UserName));
      }
    }
  }

  public bool CanEditPublicSettings => this.canEditPublicSettings;

  public bool CanOverrideTarget => this.canOverrideTarget;

  public bool CanEditTargetSettings => this.canEditTargetSettings;
}
