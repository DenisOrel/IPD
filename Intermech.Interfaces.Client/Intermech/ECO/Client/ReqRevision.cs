// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.ReqRevision
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.ECO.Client;

/// <summary>Способ создания версии (и модификации) объекта</summary>
[TypeConverter(typeof (EnumDescConverter))]
[CustomDescription("Attribute.Interfaces.Client_3")]
[Category("Revision")]
public enum ReqRevision
{
  [CustomDescription("Attribute.Interfaces.Client_4")] NoRevision = 0,
  [CustomDescription("Attribute.Interfaces.Client_5")] SuggestRevision = 1,
  [CustomDescription("Attribute.Interfaces.Client_6")] ForceRevision = 2,
  [CustomDescription("Attribute.Interfaces_555")] Inherited = 3,
  [CustomDescription("Attribute.Interfaces.Client_46")] SuggestCJ = 17, // 0x00000011
  [CustomDescription("Attribute.Interfaces.Client_44")] RequireCJ = 18, // 0x00000012
  [CustomDescription("Attribute.Interfaces.Client_45")] SuggestRevisionOrCJ = 25, // 0x00000019
  [CustomDescription("Attribute.Interfaces.Client_43")] RequireRevisionOrCJ = 26, // 0x0000001A
}
