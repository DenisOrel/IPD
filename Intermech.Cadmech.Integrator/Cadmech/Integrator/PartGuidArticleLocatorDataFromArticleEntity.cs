// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.PartGuidArticleLocatorDataFromArticleEntity
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Data.SectionEntities;
using System;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class PartGuidArticleLocatorDataFromArticleEntity : IPartGuidArticleLocatorData
{
  private SectionEntity articleEntity;

  public PartGuidArticleLocatorDataFromArticleEntity(SectionEntity articleEntity)
  {
    this.articleEntity = articleEntity != null ? articleEntity : throw new ArgumentNullException(nameof (articleEntity));
  }

  public Guid GetPartGuid() => this.articleEntity.Sections.Get<PartData>().PartGuid;
}
