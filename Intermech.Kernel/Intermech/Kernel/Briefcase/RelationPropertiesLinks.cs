// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.RelationPropertiesLinks
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll


namespace Intermech.Kernel.Briefcase;

internal sealed class RelationPropertiesLinks
{
  public long PrjLinkID;
  public long OldCreatorID;
  public int RelationType;

  public RelationPropertiesLinks(long prjLinkID, long oldCreatorID, int relationType)
  {
    this.PrjLinkID = prjLinkID;
    this.OldCreatorID = oldCreatorID;
    this.RelationType = relationType;
  }
}
