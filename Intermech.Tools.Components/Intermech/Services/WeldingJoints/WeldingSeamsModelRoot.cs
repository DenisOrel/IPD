// Decompiled with JetBrains decompiler
// Type: Intermech.Services.WeldingJoints.WeldingSeamsModelRoot
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using Experimental.Kernel.Entities;
using System;

#nullable disable
namespace Intermech.Services.WeldingJoints;

/// <summary>
/// Основной объект для доменной модели сварных швов.
/// Он предоставляет все необходимые средства для чтения изменения объектов доменной модели.
/// Реализация не является thread safe.
/// </summary>
internal sealed class WeldingSeamsModelRoot : DBModelRoot, IWeldingSeamsModelRoot, IModelRoot
{
  public WeldingSeamsModelRoot(
    WeldingSeamsModelConfiguration modelConfiguration,
    WeldingSeamsIDCache idCache)
    : base((DBModelConfiguration) modelConfiguration)
  {
    this.SpecialQueries = idCache != null ? (IWeldingSeamsSpecialQueries) new WeldingSeamsSpecialQueries((IWeldingSeamsModelRoot) this, idCache) : throw new ArgumentNullException(nameof (idCache));
  }

  public IEntityDataService<MechanicalArticleEntity> Articles { get; private set; }

  public IEntityDataService<MechanicalDocumentEntity> Documents { get; private set; }

  public IEntityDataService<WeldingSeamEntity> WeldingSeams { get; private set; }

  public IWeldingSeamsSpecialQueries SpecialQueries { get; private set; }
}
