// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.TokenType
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Expert;

/// <summary>Тип токена (во входной формуле)</summary>
[TypeConverter(typeof (EnumDescConverter))]
[CustomDescription("Attribute.Expert_24")]
[Category("Expert System")]
public enum TokenType
{
  [CustomDescription("Attribute.Expert_25")] UnaryOper,
  [CustomDescription("Attribute.Expert_26")] BinaryOper,
  [CustomDescription("Attribute.Expert_27")] OpeningBrace,
  [CustomDescription("Attribute.Expert_28")] ClosingBrace,
  [CustomDescription("Attribute.Expert_29")] FuncCall,
  [CustomDescription("Attribute.Expert_30")] Integer,
  [CustomDescription("Attribute.Expert_31")] Float,
  [CustomDescription("Attribute.Expert_32")] String,
  [CustomDescription("Attribute.Expert_33")] Date,
  [CustomDescription("Attribute.Expert_34")] ObjectLink,
  [CustomDescription("Attribute.Expert_35")] Attribute,
  [CustomDescription("Attribute.Expert_36")] Command,
  [CustomDescription("Attribute.Expert_37")] Divider,
  [CustomDescription("Attribute.Expert_38")] Measured,
  [CustomDescription("Attribute.Expert_139")] Boolean,
}
