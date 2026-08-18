// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.Report.ShowReport
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Document.Client.Report;

/// <summary>
/// 
/// </summary>
[TypeConverter(typeof (EnumDescConverter))]
[CustomDescription("Attribute.Document.Client_1")]
internal enum ShowReport
{
  [CustomDescription("Attribute.Document.Client_2")] InDoc,
  [CustomDescription("Attribute.Document.Client_3")] InPreviewWindow,
  [CustomDescription("Attribute.Document.Client_4")] Print,
}
