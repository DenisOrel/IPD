// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.TechNumerationObjectModes
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using Intermech.Localization;
using System;

#nullable disable
namespace Intermech.Interfaces.TechCard;

/// <summary>Начальный объект для нумерации</summary>
[Serializable]
public enum TechNumerationObjectModes
{
  /// <summary>Нумерация с первого элемента</summary>
  [CustomDescription("Attribute.Interfaces.TechCard_12")] FirstObj,
  /// <summary>Нумерация с текущего элемента</summary>
  [CustomDescription("Attribute.Interfaces.TechCard_13")] CurrentObj,
}
