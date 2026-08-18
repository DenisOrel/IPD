// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.DefaultGroupSpecificationForm
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

using Intermech.ComponentModel;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary>Форма спецификации по умолчанию</summary>
[TypeConverter(typeof (EnumCustomConverter))]
[Serializable]
public enum DefaultGroupSpecificationForm
{
  /// <summary>Групповая А</summary>
  [Description("Групповая А")] A,
  /// <summary>Групповая Б</summary>
  [Description("Групповая Б")] B,
  /// <summary>Групповая Б</summary>
  [Description("Групповая В")] V,
}
