// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.ExpertScriptMod
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Expert;

[TypeConverter(typeof (EnumDescConverter))]
[CustomDescription("Attribute.Expert_83")]
[Category("Expert System")]
public enum ExpertScriptMod
{
  [CustomDescription("Attribute.Expert_84")] modUnknown = -1, // 0xFFFFFFFF
  [CustomDescription("Attribute.Expert_85")] modForEach = 0,
  [CustomDescription("Attribute.Expert_86")] modForFirst = 1,
  [CustomDescription("Attribute.Expert_87")] modForMin = 2,
  [CustomDescription("Attribute.Expert_88")] modForMax = 3,
  [CustomDescription("Attribute.Expert_89")] modIfExists = 4,
  [CustomDescription("Attribute.Expert_90")] modIfAll = 5,
  [CustomDescription("Attribute.Expert_91")] modLoop = 6,
  [CustomDescription("Attribute.Expert_92")] modLoopSort = 7,
  [CustomDescription("Attribute.Expert_93")] modLoopGroup = 8,
  [CustomDescription("Attribute.Expert_229")] modVersions = 68, // 0x00000044
}
