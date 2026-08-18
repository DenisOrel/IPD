// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.PurposeDocument
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>Назначение конструкторского документа</summary>
[TypeConverter(typeof (EnumDescConverter))]
[CustomDescription("Attribute.Interfaces.Pdm_14")]
[Category("Misc")]
public enum PurposeDocument
{
  /// <summary>Главная модель</summary>
  [CustomDescription("Attribute.Interfaces.Pdm_15")] MainModel,
  /// <summary>Главный чертеж</summary>
  [CustomDescription("Attribute.Interfaces.Pdm_16")] MainDrawing,
  /// <summary>Главная модель и чертеж</summary>
  [CustomDescription("Attribute.Interfaces.Pdm_17")] MainModelAndDrawing,
}
