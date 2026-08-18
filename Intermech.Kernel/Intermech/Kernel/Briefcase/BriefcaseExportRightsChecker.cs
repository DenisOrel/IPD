// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.BriefcaseExportRightsChecker
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll


namespace Intermech.Kernel.Briefcase;

internal class BriefcaseExportRightsChecker : DBSessionable
{
  public BriefcaseExportRightsChecker(UserSession session)
    : base(session)
  {
    this.InitSecurityOptions(14, 0L);
  }

  public void CheckAccess() => this.CheckAccess(ActionType.Export, false, true);
}
