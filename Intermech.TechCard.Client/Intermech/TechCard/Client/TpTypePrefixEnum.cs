// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TpTypePrefixEnum
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Localization;
using System;

#nullable disable
namespace Intermech.TechCard.Client;

/// <summary>Типы префиксы для обозначений ТП</summary>
[Obsolete("Use short object type name instead. Will be removed in IPS 8", true)]
public enum TpTypePrefixEnum
{
  /// <summary>Групповой ТП</summary>
  [CustomDescription("Attribute.TechCard.Client_16")] TechProcGroup,
  /// <summary>Единичный ТП</summary>
  [CustomDescription("Attribute.TechCard.Client_17")] TechProcEdin,
  /// <summary>Типовой ТП</summary>
  [CustomDescription("Attribute.TechCard.Client_18")] TechProcTipov,
  /// <summary>Типовой элемент ТП</summary>
  [CustomDescription("Attribute.TechCard.Client_19")] TechProcElemBase,
}
