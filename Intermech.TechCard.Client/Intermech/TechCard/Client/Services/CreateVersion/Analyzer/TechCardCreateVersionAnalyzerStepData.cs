// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Services.CreateVersion.Analyzer.TechCardCreateVersionAnalyzerStepData
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Diagnostics;
using Intermech.Interfaces.Compositions;
using Intermech.Navigator;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.Services.CreateVersion.Analyzer;

internal class TechCardCreateVersionAnalyzerStepData
{
  /// <summary>
  /// 
  /// </summary>
  public TechCardCreateVersionAnalyzerStepData(
    [NotNull] IEnumerable<RelObjInfoItem> relObjInfoItem,
    [NotNull] IEnumerable<RelObjInfoItem> compositionItems)
  {
    this.RelObjInfoItems = (IList<RelObjInfoItem>) new List<RelObjInfoItem>(relObjInfoItem);
    this.CompositionItems = (IList<RelObjInfoItem>) new List<RelObjInfoItem>(compositionItems);
  }

  /// <summary>
  /// Описание объектов (вместе со связями), для которых требуется анализ выпуска версии
  /// </summary>
  public IList<RelObjInfoItem> RelObjInfoItems { get; }

  /// <summary>
  /// Список описаний объектов (вместе со связями) применяемости для RelObjInfoItems
  /// </summary>
  public IList<RelObjInfoItem> CompositionItems { get; }

  /// <summary>
  /// 
  /// </summary>
  public IDictionary<RelObjInfoItem, ObjInfoItem> RelObjInfo2SignedObjCache { get; set; }

  /// <summary>
  /// Признак вызова обработчика по-умолчанию для созданий версий объектов
  /// </summary>
  public bool DefaultCreateVersionHandler { get; set; }

  /// <summary>
  /// Список дескриптов для отображения списка объектов, по которым есть проблемы и нужно получить добро от пользователя
  /// для продолжения
  /// </summary>
  public DescriptorCollection ErrorDescriptors { get; } = new DescriptorCollection();
}
