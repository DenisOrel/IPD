// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Common.ToolSetupType
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Common;

[TypeConverter(typeof (EnumDescConverter))]
[CustomDescription("TechCard.Document.Attributes_055")]
[CustomCategory("TechCard.Document.Attributes_052")]
public enum ToolSetupType
{
  [CustomDescription("TechCard.Document.Attributes_027")] InLine,
  [CustomDescription("TechCard.Document.Attributes_028")] OnToolType,
  [CustomDescription("TechCard.Document.Attributes_029")] SolidText,
}
