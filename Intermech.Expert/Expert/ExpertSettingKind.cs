// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.ExpertSettingKind
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Expert;

[TypeConverter(typeof (EnumDescConverter))]
[CustomDescription("Attribute.Expert_117")]
[Category("Expert System")]
public enum ExpertSettingKind
{
  [CustomDescription("Attribute.Expert_118")] setKindValue,
  [CustomDescription("Attribute.Expert_119")] setKindByTable,
  [CustomDescription("Attribute.Expert_120")] setKindSum,
  [CustomDescription("Attribute.Expert_121")] setKindAverage,
  [CustomDescription("Attribute.Expert_122")] setKindNumber,
  [CustomDescription("Attribute.Expert_123")] setKindMinimum,
  [CustomDescription("Attribute.Expert_124")] setKindMaximum,
  [CustomDescription("Attribute.Expert_125")] setKindList,
}
