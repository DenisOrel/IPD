// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.UpdateModeInReadOnlyEnum
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.AVS;

/// <summary>Режимы вида спецификации</summary>
[TypeConverter(typeof (EnumDescConverter))]
[Serializable]
public enum UpdateModeInReadOnlyEnum
{
  /// <summary>Не обновлять</summary>
  [Description("Не обновлять")] No,
  /// <summary>Обновлять литеры</summary>
  [Description("Обновлять литеры")] Part,
}
