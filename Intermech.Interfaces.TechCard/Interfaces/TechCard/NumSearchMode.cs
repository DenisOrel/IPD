// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.NumSearchMode
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

#nullable disable
namespace Intermech.Interfaces.TechCard;

/// <summary>Режимы поиска правил нумерации</summary>
public enum NumSearchMode
{
  /// <summary>Условия на все параметры</summary>
  /// <remarks>Поиск правил для объекта с известной входимостью</remarks>
  nrsmAllParams,
  /// <summary>Условия на все не пустрые параметры</summary>
  /// <remarks>Поиск правил для объекта по всех входимостям или для родительского типа объекта (наприм. ТП)</remarks>
  nrsmNotNull,
}
