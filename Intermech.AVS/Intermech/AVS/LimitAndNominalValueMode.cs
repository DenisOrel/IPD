// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.LimitAndNominalValueMode
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.AVS;

/// <summary>Режим вывода Предельных значений и Значений номинала</summary>
[TypeConverter(typeof (EnumDescConverter))]
[Serializable]
public enum LimitAndNominalValueMode
{
  /// <summary>Диапазон значений</summary>
  [Description("Диапазон значений")] Range,
  /// <summary>Список всех значений</summary>
  [Description("Список всех значений")] List,
  /// <summary>Использовать только значения атрибута "Предельные значения"</summary>
  [Description("Использовать только атрибут \"Предельные значения\"")] UseLimitValuesOnly,
}
