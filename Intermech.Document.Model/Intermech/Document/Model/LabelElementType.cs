// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.LabelElementType
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.ComponentModel;
using Intermech.Localization;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>Выравнивание элементов страницы</summary>
[TypeConverter(typeof (EnumCustomConverter))]
[Serializable]
public enum LabelElementType
{
  /// <summary>Нет</summary>
  [CustomDescription("Attribute.Document.Model_283")] Text,
  /// <summary>Штрих-код CODE128</summary>
  [CustomDescription("Attribute.Document.Model_284")] BarCode_CODE128,
  /// <summary>Штрих-код EAN13</summary>
  [CustomDescription("Attribute.Document.Model_294")] BarCode_EAN13,
}
