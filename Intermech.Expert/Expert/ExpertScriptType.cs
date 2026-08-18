// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.ExpertScriptType
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Expert;

[TypeConverter(typeof (EnumDescConverter))]
[CustomDescription("Attribute.Expert_70")]
[Category("Expert System")]
public enum ExpertScriptType
{
  [CustomDescription("Attribute.Expert_71")] Unknown,
  [CustomDescription("Attribute.Expert_72")] CommonCalc,
  [CustomDescription("Attribute.Expert_73")] FunctionScript,
  [CustomDescription("Attribute.Expert_74")] DocScript,
  [CustomDescription("Attribute.Expert_75")] AttribRule,
  [CustomDescription("Attribute.Expert_76")] ObjectRule,
  [CustomDescription("Attribute.Expert_77")] RecalcScript,
  [CustomDescription("Attribute.Expert_140")] ComplectTemplate,
  [CustomDescription("Attribute.Expert_219")] VisDataScheme,
  [CustomDescription("Attribute.Expert_220")] VisStyles,
  [CustomDescription("Attribute.Expert_228")] CommandScript,
}
