// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.Report.DatePrintFormatValue
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using System;

#nullable disable
namespace Intermech.Document.Client.Report;

public class DatePrintFormatValue : Attribute
{
  public string FormatString;

  public DatePrintFormatValue(string formalString) => this.FormatString = formalString;

  public DatePrintFormatValue() => this.FormatString = string.Empty;
}
