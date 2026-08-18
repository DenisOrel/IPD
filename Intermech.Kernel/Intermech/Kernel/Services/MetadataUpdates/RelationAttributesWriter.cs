// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.RelationAttributesWriter
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal sealed class RelationAttributesWriter : AttributesWriter<IDBRelation>
{
  protected override IDBAttributeType4 GetAttributeType4(
    IUserSession session,
    IDBRelation attributable,
    int attributeID)
  {
    return session.GetRelationType(attributable.RelationType).Attributes.GetAttributeByID(attributeID, false);
  }
}
