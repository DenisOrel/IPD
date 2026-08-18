// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.ExpCommandType
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Expert;

/// <summary>Inner commands for formula calculation</summary>
[TypeConverter(typeof (EnumDescConverter))]
[CustomDescription("Attribute.Expert_39")]
[Category("Expert System")]
public enum ExpCommandType
{
  [CustomDescription("Attribute.Expert_40")] JumpTrue,
  [CustomDescription("Attribute.Expert_41")] JumpFalse,
  [CustomDescription("Attribute.Expert_42")] FormPackage,
  [CustomDescription("Attribute.Expert_218")] GetArray,
}
