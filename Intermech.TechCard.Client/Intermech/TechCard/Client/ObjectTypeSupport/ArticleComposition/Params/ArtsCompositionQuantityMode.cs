// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Params.ArtsCompositionQuantityMode
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Localization;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Params;

/// <summary>
/// Режимы подсчета "конструкторского" количества для контекстной сборочной единицы
/// </summary>
internal enum ArtsCompositionQuantityMode
{
  /// <summary>Подсчет количества по развернутому составу</summary>
  [CustomDescription("Attribute.TechCard.ArtsCompositionQuantityMode_FullExpanded")] FullExpanded,
  /// <summary>Подсчет количества только по составу первого уровня</summary>
  [CustomDescription("Attribute.TechCard.ArtsCompositionQuantityMode_FirstLevelOnly")] FirstLevelOnly,
}
