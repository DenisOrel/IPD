// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.Reports.TableColumnSettings
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Interfaces.Document;

#nullable disable
namespace Intermech.Document.Client.Reports;

public class TableColumnSettings
{
  public string Caption;
  public float Width;
  public HorzAlignment TextAlignment;

  public TableColumnSettings(string caption, float width, HorzAlignment textAlignment)
  {
    this.Caption = caption;
    this.Width = width;
    this.TextAlignment = textAlignment;
  }
}
