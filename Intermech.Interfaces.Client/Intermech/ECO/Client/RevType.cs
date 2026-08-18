// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.RevType
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.ECO.Client;

/// <summary>Тип извещения</summary>
[TypeConverter(typeof (EnumDescConverter))]
[CustomDescription("Attribute.Interfaces.Client_7")]
[Category("Revision")]
public enum RevType
{
  Unknown = -1, // 0xFFFFFFFF
  [CustomDescription("Attribute.Interfaces.Client_8")] II = 0,
  [CustomDescription("Attribute.Interfaces.Client_9")] PI = 1,
  [CustomDescription("Attribute.Interfaces.Client_10")] PR = 2,
  [CustomDescription("Attribute.Interfaces.Client_47")] DI = 3,
  [CustomDescription("Attribute.Interfaces.Client_48")] DPI = 4,
  [CustomDescription("Attribute.Interfaces.Client_49")] CJ = 5,
}
