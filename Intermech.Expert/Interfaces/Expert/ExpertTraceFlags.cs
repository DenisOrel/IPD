// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Expert.ExpertTraceFlags
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Localization;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Expert;

/// <summary>
/// These Trace Flags can be set to obtain needed trace info
/// </summary>
[TypeConverter(typeof (EnumDescConverter))]
[CustomDescription("Attribute.Interfaces_35")]
[Category("Expert System")]
[Flags]
public enum ExpertTraceFlags
{
  [CustomDescription("Attribute.Interfaces_36")] None = 0,
  [CustomDescription("Attribute.Interfaces_37")] ShowExpertObjects = 1,
  [CustomDescription("Attribute.Interfaces_38")] ShowOtherObjects = 2,
  [CustomDescription("Attribute.Interfaces_39")] ShowObjConds = 4,
  [CustomDescription("Attribute.Interfaces_40")] TraceAttribSearch = 8,
  [CustomDescription("Attribute.Interfaces_41")] TraceTables = 16, // 0x00000010
  [CustomDescription("Attribute.Interfaces_42")] TraceScripts = 32, // 0x00000020
  [CustomDescription("Attribute.Interfaces_43")] ShowContext = 64, // 0x00000040
  [CustomDescription("Attribute.Interfaces_44")] ShowAttrChanges = 128, // 0x00000080
  [CustomDescription("Attribute.Interfaces_45")] ShowObjResults = 256, // 0x00000100
  [CustomDescription("Attribute.Interfaces_46")] ShowScriptConds = 512, // 0x00000200
  [CustomDescription("Attribute.Interfaces_47")] ShowFillDocs = 1024, // 0x00000400
  [CustomDescription("Attribute.Interfaces_48")] TraceObjectSearch = 2048, // 0x00000800
  [CustomDescription("Attribute.Interfaces_49")] ShowSettings = 4096, // 0x00001000
  [CustomDescription("Attribute.Interfaces_50")] ShowGlobalTables = 8192, // 0x00002000
}
