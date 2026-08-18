// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.AVSDocumentForm
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

using Intermech.ComponentModel;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary>Форма спецификации</summary>
[TypeConverter(typeof (EnumCustomConverter))]
[Serializable]
public enum AVSDocumentForm
{
  /// <summary>Единичная</summary>
  [Description("Единичная")] Single,
  /// <summary>Групповая А</summary>
  [Description("Групповая А")] A,
  /// <summary>Групповая Б</summary>
  [Description("Групповая Б")] B,
  /// <summary>Зеркальная</summary>
  [Description("Зеркальная")] Mirror,
  /// <summary>Групповая В</summary>
  [Description("Групповая В")] V,
  /// <summary>Групповая Г</summary>
  [Description("Групповая Г")] G,
}
