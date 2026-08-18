// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.RelationLinks
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll


namespace Intermech.Kernel.Briefcase;

internal sealed class RelationLinks : IDLink
{
  public long RelationID;

  public RelationLinks(
    long relationID,
    int attributeID,
    int inListID,
    long oldObjectID,
    string caption,
    int type,
    bool isID)
    : base(attributeID, inListID, oldObjectID, caption, type, isID)
  {
    this.RelationID = relationID;
  }
}
