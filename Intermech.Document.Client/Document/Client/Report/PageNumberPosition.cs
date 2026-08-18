// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.Report.PageNumberPosition
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Localization;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Document.Client.Report;

[TypeConverter(typeof (EnumDescConverter))]
[CustomDescription("Attribute.Document.Client_18")]
[Category("Misc")]
[Flags]
public enum PageNumberPosition
{
  [CustomDescription("Attribute.Document.Client_19")] None = 0,
  [CustomDescription("Attribute.Document.Client_20")] DownLeft = 1,
  [CustomDescription("Attribute.Document.Client_21")] DownCenter = 2,
  [CustomDescription("Attribute.Document.Client_22")] DownRight = DownCenter | DownLeft, // 0x00000003
}
