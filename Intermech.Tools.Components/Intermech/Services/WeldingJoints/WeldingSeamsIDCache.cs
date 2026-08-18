// Decompiled with JetBrains decompiler
// Type: Intermech.Services.WeldingJoints.WeldingSeamsIDCache
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces.Data.Metadata;
using System;

#nullable disable
namespace Intermech.Services.WeldingJoints;

/// <summary>Кэш метаданных для сварных швов.</summary>
internal sealed class WeldingSeamsIDCache
{
  private MetadataResolverFactory metadataResolvers;

  public WeldingSeamsIDCache(MetadataResolverFactory metadataResolvers)
  {
    this.metadataResolvers = metadataResolvers != null ? metadataResolvers : throw new ArgumentNullException(nameof (metadataResolvers));
    this.ExternalKey = metadataResolvers.AttributeTypeResolver(new Guid("CAD00378-306C-11D8-B4E9-00304F19F545"));
    this.BasedOnCADModel = metadataResolvers.AttributeTypeResolver(new Guid("CAD0153E-306C-11D8-B4E9-00304F19F545"));
    this.WeldingSeams = metadataResolvers.ObjectTypeResolver(new Guid("CADD98C1-306C-11D8-B4E9-00304F19F545"));
    this.ArticleDocumentsLink = metadataResolvers.RelationTypeResolver(new Guid("CAD00154-306C-11D8-B4E9-00304F19F545"));
  }

  /// <summary>Атрибут 'Внешний ключ объекта IPS'</summary>
  public AttributeTypeResolver ExternalKey { get; private set; }

  /// <summary>Атрибут 'Создано по CAD-модели'</summary>
  public AttributeTypeResolver BasedOnCADModel { get; private set; }

  /// <summary>Тип объектов 'Сварные швы'</summary>
  public ObjectTypeResolver WeldingSeams { get; private set; }

  /// <summary>Тип связи 'Документация на изделие'</summary>
  public RelationTypeResolver ArticleDocumentsLink { get; private set; }
}
