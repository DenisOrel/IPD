// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.Report.DatePrintFormats
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
[CustomDescription("Attribute.Document.Client_11")]
[Category("Misc")]
[Flags]
public enum DatePrintFormats
{
  [CustomDescription("Attribute.Document.Client_12"), DatePrintFormatValue] None = 0,
  [CustomDescription("Attribute.Document.Client_13"), DatePrintFormatValue("dd MMMM yyyy")] FullDate = 1,
  [CustomDescription("Attribute.Document.Client_14"), DatePrintFormatValue("dd.MM.yyyy")] Date = 2,
  [CustomDescription("Attribute.Document.Client_15"), DatePrintFormatValue("HH:mm:ss")] Time = Date | FullDate, // 0x00000003
  [CustomDescription("Attribute.Document.Client_16"), DatePrintFormatValue("dd MMMM yyyy HH:mm:ss")] FullDateAndTime = 4,
  [CustomDescription("Attribute.Document.Client_17"), DatePrintFormatValue("dd.MM.yyyy HH:mm:ss")] DateTime = FullDateAndTime | FullDate, // 0x00000005
}
