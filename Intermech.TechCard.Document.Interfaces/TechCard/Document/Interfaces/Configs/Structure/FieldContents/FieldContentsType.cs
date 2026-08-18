// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Structure.FieldContents.FieldContentsType
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Structure.FieldContents;

[TypeConverter(typeof (EnumDescConverter))]
[CustomDescription("TechCard.Document.Attributes_060")]
[CustomCategory("TechCard.Document.Attributes_052")]
public enum FieldContentsType
{
  [CustomDescription("TechCard.Document.Attributes_061")] Attribute,
  [CustomDescription("TechCard.Document.Attributes_062")] Template,
  [CustomDescription("TechCard.Document.Attributes_063")] Formula,
  [CustomDescription("TechCard.Document.Attributes_072")] Custom,
}
