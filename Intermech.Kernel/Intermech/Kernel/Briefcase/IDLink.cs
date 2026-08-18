// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.IDLink
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll


namespace Intermech.Kernel.Briefcase;

internal abstract class IDLink : LinksBase
{
  public bool IsID;

  public IDLink(
    int attributeID,
    int inListID,
    long oldLinkID,
    string caption,
    int type,
    bool isID)
    : base(attributeID, inListID, oldLinkID, caption, type)
  {
    this.IsID = isID;
  }
}
