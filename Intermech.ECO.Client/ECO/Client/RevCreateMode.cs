// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.RevCreateMode
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.ECO.Client;

[TypeConverter(typeof (EnumDescConverter))]
[CustomDescription("Attribute.ECO.Client_8")]
[Category("Revision")]
public enum RevCreateMode
{
  [CustomDescription("Attribute.ECO.Client_9")] ByUser,
  [CustomDescription("Attribute.ECO.Client_10")] ByVerSuggest,
  [CustomDescription("Attribute.ECO.Client_11")] ByVerForce,
}
