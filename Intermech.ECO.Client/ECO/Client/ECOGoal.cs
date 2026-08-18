// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.ECOGoal
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.ECO.Client;

[TypeConverter(typeof (EnumDescConverter))]
[CustomDescription("Attribute.ECO.Client_1")]
[CustomCategory("Attribute.ECO.Client_2")]
public enum ECOGoal
{
  [CustomDescription("Attribute.ECO.Client_3")] NoGoal = -1, // 0xFFFFFFFF
  [CustomDescription("Attribute.ECO.Client_4")] Change = 0,
  [CustomDescription("Attribute.ECO.Client_5")] Annul = 1,
  [CustomDescription("Attribute.ECO.Client_6")] Litera = 2,
  [CustomDescription("Attribute.ECO.Client_7")] Replace = 3,
  [CustomDescription("Attribute.ECO.Client_18")] Creation = 4,
  VersionCreate = 100, // 0x00000064
  Stamp = 101, // 0x00000065
}
