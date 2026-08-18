// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.LinksBase
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll


namespace Intermech.Kernel.Briefcase;

internal abstract class LinksBase
{
  public int Type;
  public int AttributeID;
  public int InListID;
  public long OldLinkID;
  public string Caption;

  public LinksBase(int attributeID, int inListID, long oldLinkID, string caption, int type)
  {
    this.AttributeID = attributeID;
    this.OldLinkID = oldLinkID;
    this.InListID = inListID;
    this.Caption = caption;
    this.Type = type;
  }
}
