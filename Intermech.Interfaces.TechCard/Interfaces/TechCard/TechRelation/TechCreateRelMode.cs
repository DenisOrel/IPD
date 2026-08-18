// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.TechRelation.TechCreateRelMode
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

#nullable disable
namespace Intermech.Interfaces.TechCard.TechRelation;

/// <summary>Режим создания связи</summary>
public enum TechCreateRelMode
{
  /// <summary>Cоздать связь входит в</summary>
  tcrmEnterIn,
  /// <summary>Создать связь содержит</summary>
  tcrmContains,
  /// <summary>
  /// Cоздать связь входит в, если настройки типа связи позволяют, иначе в обратном напрвлении
  /// (c проверкой связи в обратном напрвлении)
  /// </summary>
  tcrmBothEnterInFirst,
  /// <summary>
  /// Cоздать связь содержит, если настройки типа связи позволяют, иначе в обратном напрвлении
  /// (c проверкой связи в обратном напрвлении)
  /// </summary>
  tcrmBothContainsFirst,
}
