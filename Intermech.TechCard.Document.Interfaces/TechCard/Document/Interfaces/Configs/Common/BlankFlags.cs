// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Common.BlankFlags
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Localization;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Common;

[TypeConverter(typeof (EnumDescConverter))]
[CustomDescription("TechCard.Document.Attributes_057")]
[CustomCategory("TechCard.Document.Attributes_052")]
[Flags]
public enum BlankFlags
{
  [CustomDescription("TechCard.Document.Attributes_070")] BfNone = 0,
  [CustomDescription("TechCard.Document.Attributes_032")] BfContents = 1,
  [CustomDescription("TechCard.Document.Attributes_033")] BfStatement = 2,
  [CustomDescription("TechCard.Document.Attributes_034")] BfRouteCard = 4,
  [CustomDescription("TechCard.Document.Attributes_035")] BfOperatingCard = 8,
  [CustomDescription("TechCard.Document.Attributes_036")] BfShopToolList = 16, // 0x00000010
  [CustomDescription("TechCard.Document.Attributes_037")] BfOperationalList = 32, // 0x00000020
  [CustomDescription("TechCard.Document.Attributes_038")] BfPickingCard = 64, // 0x00000040
  [CustomDescription("TechCard.Document.Attributes_039")] BfPickingCardStructure = 128, // 0x00000080
  [CustomDescription("TechCard.Document.Attributes_040")] BfEmptyStringBeforeOperation = 256, // 0x00000100
  [CustomDescription("TechCard.Document.Attributes_041")] BfEnterInContents = 512, // 0x00000200
  [CustomDescription("TechCard.Document.Attributes_042")] BfDocumentNotInSet = 1024, // 0x00000400
  [CustomDescription("TechCard.Document.Attributes_043")] BfDoNotNumberPages = 2048, // 0x00000800
  [CustomDescription("TechCard.Document.Attributes_044")] BfForPartDocument = 4096, // 0x00001000
  [CustomDescription("TechCard.Document.Attributes_045")] BfPartGroupDocument = 8192, // 0x00002000
  [CustomDescription("TechCard.Document.Attributes_046")] BfSketchDocument = 16384, // 0x00004000
  [CustomDescription("TechCard.Document.Attributes_047")] BfShowToolType = 32768, // 0x00008000
  [CustomDescription("TechCard.Document.Attributes_048")] BfNoRepeatTool = 65536, // 0x00010000
  [CustomDescription("TechCard.Document.Attributes_049")] BfPlaceToolIntoEmptyFields = 131072, // 0x00020000
}
