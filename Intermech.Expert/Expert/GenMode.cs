// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.GenMode
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Expert;

[TypeConverter(typeof (EnumDescConverter))]
[CustomDescription("Attribute.Expert_144")]
[Category("Expert System")]
public enum GenMode
{
  genModeNone = -1, // 0xFFFFFFFF
  [CustomDescription("Attribute.Expert_141")] genModeGenerate = 0,
  [CustomDescription("Attribute.Expert_142")] genModeVersion = 1,
  [CustomDescription("Attribute.Expert_143")] genModeRefresh = 2,
}
