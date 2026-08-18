// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.ExpertFormulaType
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Expert;

[TypeConverter(typeof (EnumDescConverter))]
[CustomDescription("Attribute.Expert_78")]
[Category("Expert System")]
public enum ExpertFormulaType
{
  [CustomDescription("Attribute.Expert_79")] Unknown,
  [CustomDescription("Attribute.Expert_80")] CommonFormula,
  [CustomDescription("Attribute.Expert_81")] Cond,
  [CustomDescription("Attribute.Expert_82")] SimpleFormula,
  [CustomDescription("Attribute.Expert_216")] ESFolder,
}
