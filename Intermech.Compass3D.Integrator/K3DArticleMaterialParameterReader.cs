// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.K3DArticleMaterialParameterReader
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.CADInterface.Proxies;
using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class K3DArticleMaterialParameterReader : IArticleMaterialParameterReader
{
  private static readonly DecodeAttributesOptions emptyDecodeOptions = new DecodeAttributesOptions();
  private IIntegrator integrator;
  private SectionEntity articleEntity;
  private IList<StringKey> parametersToCache;
  private ContainerValues cachedParameters;

  public K3DArticleMaterialParameterReader(
    IIntegrator integrator,
    SectionEntity articleEntity,
    IList<StringKey> parametersToCache)
  {
    this.integrator = integrator;
    this.articleEntity = articleEntity;
    this.parametersToCache = parametersToCache;
  }

  public ValueRecord TryReadParameter(StringKey parameterName)
  {
    if (this.cachedParameters == null)
      this.cachedParameters = this.ReadParametersToCache();
    return this.cachedParameters.Bag.Find((Predicate<ValueRecord>) (item => item.Key == parameterName))?.Clone();
  }

  private ContainerValues ReadParametersToCache()
  {
    CIArticleData ciArticleData = this.articleEntity.Sections.Get<CIArticleData>();
    return new DBObjectAttributesCodec((IValueBagFormatter) new CADInterfaceFormatter(CADInterfaceFormatterMode.UncheckedRead)).ReadAttributes(ServiceUtils.GetService<ICADInterfaceService>((object) this.integrator, true).GetArticleAttributeContainer(ciArticleData.Configuration), (ICollection<StringKey>) this.parametersToCache, K3DArticleMaterialParameterReader.emptyDecodeOptions);
  }
}
