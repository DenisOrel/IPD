// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Common.TablePropertiesFlags
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Localization;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Common;

[TypeConverter(typeof (EnumDescConverter))]
[CustomDescription("TechCard.Document.Attributes_069")]
[CustomCategory("TechCard.Document.Attributes_052")]
[Flags]
public enum TablePropertiesFlags
{
  [CustomDescription("TechCard.Document.Attributes_070")] None = 0,
  [CustomDescription("TechCard.Document.Attributes_065")] NotRepeated = 1,
  [CustomDescription("TechCard.Document.Attributes_066")] OnDetail = 2,
  [CustomDescription("TechCard.Document.Attributes_067")] SketchField = 4,
  [CustomDescription("TechCard.Document.Attributes_068")] CalcOnFill = 8,
}
