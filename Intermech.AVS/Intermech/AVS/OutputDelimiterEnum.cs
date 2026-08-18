// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.OutputDelimiterEnum
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
public enum OutputDelimiterEnum
{
  /// <summary>пробел</summary>
  [Description("(пробел)")] Space,
  /// <summary>неразрывный пробел</summary>
  [Description("(неразрывный пробел)")] NonBreakSpace,
  /// <summary>без пробела</summary>
  [Description("(без пробела)")] NoSpace,
  /// <summary>принудительный перенос</summary>
  [Description("(принудительный перенос)")] ForceLineBreak,
  /// <summary>точка</summary>
  [Description(". (точка)")] Dot,
  /// <summary>запятая</summary>
  [Description(", (запятая)")] Comma,
  /// <summary>звездочка</summary>
  [Description("* (звездочка)")] Star,
  /// <summary>минус</summary>
  [Description("- (минус)")] Minus,
  /// <summary>неразрывный дефис</summary>
  [Description("(неразрывный дефис)")] Dash,
}
