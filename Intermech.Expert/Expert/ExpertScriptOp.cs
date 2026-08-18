// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.ExpertScriptOp
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Expert;

[TypeConverter(typeof (EnumDescConverter))]
[CustomDescription("Attribute.Expert_94")]
[Category("Expert System")]
public enum ExpertScriptOp
{
  [CustomDescription("Attribute.Expert_95")] opUnknown = -1, // 0xFFFFFFFF
  [CustomDescription("Attribute.Expert_96")] opObjParents = 9,
  [CustomDescription("Attribute.Expert_97")] opObjChildren = 10, // 0x0000000A
  [CustomDescription("Attribute.Expert_98")] opObjSiblings = 11, // 0x0000000B
  [CustomDescription("Attribute.Expert_99")] opObjLinked = 12, // 0x0000000C
  [CustomDescription("Attribute.Expert_100")] opObjAncestors = 13, // 0x0000000D
  [CustomDescription("Attribute.Expert_101")] opObjDescendants = 14, // 0x0000000E
  [CustomDescription("Attribute.Expert_102")] opExit = 15, // 0x0000000F
  [CustomDescription("Attribute.Expert_103")] opFolder = 16, // 0x00000010
  [CustomDescription("Attribute.Expert_104")] opSelFolder = 17, // 0x00000011
  [CustomDescription("Attribute.Expert_105")] opSetting = 18, // 0x00000012
  [CustomDescription("Attribute.Expert_106")] opDocFillText = 19, // 0x00000013
  [CustomDescription("Attribute.Expert_107")] opDocNewElem = 20, // 0x00000014
  [CustomDescription("Attribute.Expert_108")] opDocSelectElem = 21, // 0x00000015
  [CustomDescription("Attribute.Expert_109")] opObjType = 24, // 0x00000018
  [CustomDescription("Attribute.Expert_110")] opByFormula = 25, // 0x00000019
  [CustomDescription("Attribute.Expert_111")] opByTable = 26, // 0x0000001A
  [CustomDescription("Attribute.Expert_112")] opByScript = 27, // 0x0000001B
  [CustomDescription("Attribute.Expert_177")] opDocControl = 32, // 0x00000020
  [CustomDescription("Attribute.Expert_113")] opReturnObject = 39, // 0x00000027
  [CustomDescription("Attribute.Expert_114")] opRecalc = 40, // 0x00000028
  [CustomDescription("Attribute.Expert_115")] opUserProc = 43, // 0x0000002B
  [CustomDescription("Attribute.Expert_116")] opVersionRule = 44, // 0x0000002C
  [CustomDescription("Attribute.Expert_145")] opCreateDocument = 49, // 0x00000031
  [CustomDescription("Attribute.Expert_146")] opCreateComplect = 50, // 0x00000032
  [CustomDescription("Attribute.Expert_165")] opGlobRoot = 53, // 0x00000035
  [CustomDescription("Attribute.Expert_166")] opGlobForObject = 54, // 0x00000036
  [CustomDescription("Attribute.Expert_221")] opVisPreview = 63, // 0x0000003F
  [CustomDescription("Attribute.Expert_222")] opVisCommon = 64, // 0x00000040
  [CustomDescription("Attribute.Expert_223")] opVisRelation = 65, // 0x00000041
  [CustomDescription("Attribute.Expert_224")] opDocCopy = 66, // 0x00000042
  [CustomDescription("Attribute.Expert_231")] opSetInBase = 67, // 0x00000043
}
