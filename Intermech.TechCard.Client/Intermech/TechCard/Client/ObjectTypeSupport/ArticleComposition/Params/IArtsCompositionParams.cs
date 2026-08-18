// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Params.IArtsCompositionParams
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Params;

/// <summary>Настройки контекстных сборочных единиц</summary>
internal interface IArtsCompositionParams
{
  /// <summary>
  /// Отображение оставшегося количества при добавлении комплектующих
  /// </summary>
  bool ShowRemainQty { get; set; }

  /// <summary>
  /// Режим подсчета "конструкторского" количества для контекстной сборочной единицы
  /// </summary>
  ArtsCompositionQuantityMode DesignQuantityMode { get; set; }

  /// <summary>Параметры для статусов позиций</summary>
  IReadOnlyList<IArtsCompositionStatusParams> StatusParams { get; }
}
