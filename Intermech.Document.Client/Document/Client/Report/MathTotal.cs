// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.Report.MathTotal
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Document.Client.Report;

/// <summary>Итог</summary>
[TypeConverter(typeof (EnumDescConverter))]
[CustomDescription("Attribute.Document.Client_5")]
internal enum MathTotal
{
  [CustomDescription("Attribute.Document.Client_6")] None,
  [CustomDescription("Attribute.Document.Client_7")] Summ,
  [CustomDescription("Attribute.Document.Client_8")] Mean,
  [CustomDescription("Attribute.Document.Client_9")] Min,
  [CustomDescription("Attribute.Document.Client_10")] Max,
}
