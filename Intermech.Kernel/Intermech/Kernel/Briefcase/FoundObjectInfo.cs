// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.FoundObjectInfo
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll


namespace Intermech.Kernel.Briefcase;

public sealed class FoundObjectInfo
{
  public long BriefcaseObjectID { get; }

  public long DBObjectID { get; set; }

  public FoundObjectInfo(long briefcaseObjectID, long dbObjectID)
  {
    this.BriefcaseObjectID = briefcaseObjectID;
    this.DBObjectID = dbObjectID;
  }
}
