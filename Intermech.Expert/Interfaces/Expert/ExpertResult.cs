// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Expert.ExpertResult
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Expert;

/// <summary>Expert System result codes</summary>
[TypeConverter(typeof (EnumDescConverter))]
[CustomDescription("Attribute.Interfaces_21")]
[Category("Expert System")]
public enum ExpertResult
{
  [CustomDescription("Attribute.Interfaces_22")] Unknown,
  [CustomDescription("Attribute.Interfaces_23")] OK,
  [CustomDescription("Attribute.Interfaces_24")] WrongTaskId,
  [CustomDescription("Attribute.Interfaces_25")] TaskBusy,
  [CustomDescription("Attribute.Interfaces_26")] NoContext,
  [CustomDescription("Attribute.Interfaces_27")] NoSuitableObjects,
  [CustomDescription("Attribute.Interfaces_28")] NoCondParms,
  [CustomDescription("Attribute.Interfaces_29")] NoCalcParms,
  [CustomDescription("Attribute.Interfaces_30")] ObjectNotFound,
  [CustomDescription("Attribute.Interfaces_31")] RuleNotFound,
  [CustomDescription("Attribute.Interfaces_32")] AmbiguousValue,
  [CustomDescription("Attribute.Interfaces_33")] CircularReference,
  [CustomDescription("Attribute.Interfaces_34")] EmptyTableCell,
  [CustomDescription("Attribute.Interfaces_157")] Aborted,
}
