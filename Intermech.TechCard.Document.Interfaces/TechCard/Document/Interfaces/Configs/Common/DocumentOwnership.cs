// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Common.DocumentOwnership
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Common;

[TypeConverter(typeof (EnumDescConverter))]
[CustomDescription("TechCard.Document.Attributes_051")]
[CustomCategory("TechCard.Document.Attributes_052")]
public enum DocumentOwnership
{
  [CustomDescription("TechCard.Document.Attributes_014")] Complect,
  [CustomDescription("TechCard.Document.Attributes_015")] Album,
  [CustomDescription("TechCard.Document.Attributes_016")] Article,
  [CustomDescription("TechCard.Document.Attributes_017")] Process,
  [CustomDescription("TechCard.Document.Attributes_018")] OperGroup,
  [CustomDescription("TechCard.Document.Attributes_019")] Operation,
  [CustomDescription("TechCard.Document.Attributes_020")] InstrumentPosition,
  [CustomDescription("TechCard.Document.Attributes_077")] Report,
  [CustomDescription("TechCard.Document.Attributes_078")] Notice,
}
