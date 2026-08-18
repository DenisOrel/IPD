// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Services.CreateVersion.Analyzer.TechCardObjectCreateVersionAnalyzerParam
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Diagnostics;
using Intermech.Interfaces.Compositions;
using Intermech.TechCard.Client.Services.DataProviders;
using Intermech.TechCard.Client.Services.DataProviders.Composition;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.Services.CreateVersion.Analyzer;

/// <summary>
/// Параметры для анализа создания версии технологических объектов
/// </summary>
internal class TechCardObjectCreateVersionAnalyzerParam
{
  /// <summary>
  /// 
  /// </summary>
  private IEnumerable<RelObjInfoItem> _relObjInfoItems;
  /// <summary>
  /// 
  /// </summary>
  private readonly ITechCardDataEnumerableProvider<RelObjInfoItem> _relObjInfoProvider;
  /// <summary>
  /// 
  /// </summary>
  private ITechCardDataEnumerableProvider<RelObjInfoItem> _compositionProvider;

  /// <param name="relObjInfoItems"></param>
  public TechCardObjectCreateVersionAnalyzerParam(
    [NotNull] ITechCardDataEnumerableProvider<RelObjInfoItem> relObjInfoProvider)
  {
    this._relObjInfoProvider = relObjInfoProvider is TechCardRelObjInfoItemsTypeUpdater<RelObjInfoItem> ? relObjInfoProvider : (ITechCardDataEnumerableProvider<RelObjInfoItem>) new TechCardRelObjInfoItemsTypeUpdater<RelObjInfoItem>(relObjInfoProvider);
  }

  /// <summary>
  /// Описание объектов (вместе со связями), для которых требуется выпуск версии
  /// </summary>
  public IEnumerable<RelObjInfoItem> RelObjInfoItems
  {
    get => this._relObjInfoItems ?? (this._relObjInfoItems = this._relObjInfoProvider.Execute());
  }

  /// <summary>
  /// 
  /// </summary>
  public ITechCardDataEnumerableProvider<RelObjInfoItem> CompositionProvider
  {
    get => this._compositionProvider;
    set
    {
      if (value == null)
        this._compositionProvider = (ITechCardDataEnumerableProvider<RelObjInfoItem>) null;
      else
        this._compositionProvider = value is TechCardRelObjInfoItemsTypeUpdater<RelObjInfoItem> ? value : (ITechCardDataEnumerableProvider<RelObjInfoItem>) new TechCardRelObjInfoItemsTypeUpdater<RelObjInfoItem>(value);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public IEnumerable<TechCardCreateVersionAnalyzerStep> AnalyzerSteps { get; set; }
}
