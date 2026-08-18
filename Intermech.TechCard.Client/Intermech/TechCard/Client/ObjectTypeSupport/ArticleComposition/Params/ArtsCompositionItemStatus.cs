// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Params.ArtsCompositionItemStatus
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Localization;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Params;

/// <summary>
/// Возможные значение поля "Статус позиции" для контекстной сборочной единицы в техпроцессе
/// </summary>
internal enum ArtsCompositionItemStatus
{
  /// <summary>Значение не определено</summary>
  None,
  /// <summary>Изделия не выбиралось в ТП</summary>
  [DefaultColor("#FFFF0000"), CustomDescription("Attribute.TechCard.Client_53")] NotUsed,
  /// <summary>
  /// Изделие выбиралось в ТП, но не всё количество использовано
  /// </summary>
  [DefaultColor("#FF0000FF"), CustomDescription("Attribute.TechCard.Client_54")] NotAllUsed,
  /// <summary>Всё количество изделий выбрано в ТП</summary>
  [DefaultColor("#FF000000"), CustomDescription("Attribute.TechCard.Client_55")] AllUsed,
  /// <summary>
  /// Изделие выбиралось, но не всё количество использовано в ТП. Изделие в конструкторском составе имеет версию, отличающуюся от версии в ТП.
  /// </summary>
  [DefaultColor("#FF008000"), CustomDescription("Attribute.TechCard.Client_56")] VersionNotAllUsed,
  /// <summary>
  /// Всё количество изделий выбрано в ТП. Изделие в конструкторском составе имеет версию, отличающуюся от версии в ТП
  /// </summary>
  [DefaultColor("#FF800080"), CustomDescription("Attribute.TechCard.Client_57")] VersionAllUsed,
  /// <summary>
  /// Количество комплектующих в ТП превышает количество изделий в конструкторском составе.
  /// </summary>
  [DefaultColor("#FFFFFF00"), CustomDescription("Attribute.TechCard.Client_58")] UsedOverLimit,
}
