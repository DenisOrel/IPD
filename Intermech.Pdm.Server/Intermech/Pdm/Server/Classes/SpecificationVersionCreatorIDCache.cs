// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.Classes.SpecificationVersionCreatorIDCache
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces.Data.Metadata;
using System;

#nullable disable
namespace Intermech.Pdm.Server.Classes;

internal sealed class SpecificationVersionCreatorIDCache
{
  public SpecificationVersionCreatorIDCache(MetadataResolverFactory metadataResolvers)
  {
    this.Specifications = metadataResolvers.ObjectTypeResolver(new Guid("cad00133-306c-11d8-b4e9-00304f19f545"));
    this.ArticleToDocuments = metadataResolvers.RelationTypeResolver(new Guid("CAD00154-306C-11D8-B4E9-00304F19F545"));
    this.FixedRelation = metadataResolvers.AttributeTypeResolver(new Guid("CAD001C2-306C-11D8-B4E9-00304F19F545"));
    this.InstanceGroupId = metadataResolvers.AttributeTypeResolver(new Guid("CAD001F9-306C-11D8-B4E9-00304F19F545"));
  }

  public ObjectTypeResolver Specifications { get; private set; }

  public RelationTypeResolver ArticleToDocuments { get; private set; }

  public AttributeTypeResolver FixedRelation { get; private set; }

  public AttributeTypeResolver InstanceGroupId { get; private set; }
}
