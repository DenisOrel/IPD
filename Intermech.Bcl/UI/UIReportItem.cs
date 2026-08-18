
// Type: Intermech.UI.UIReportItem
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;


namespace Intermech.UI
{
    public sealed class UIReportItem : ICloneable
    {
      private TraceLevel traceLevel;
      private int id;
      private int indentLevel;
      private string header;
      private string text;
      private object[] context;
      private object[] data;
      private static readonly object[] emptyData = new object[0];

      public UIReportItem()
      {
        this.traceLevel = TraceLevel.Off;
        this.id = 0;
        this.indentLevel = 0;
        this.header = string.Empty;
        this.text = string.Empty;
        this.context = UIReportItem.emptyData;
        this.data = UIReportItem.emptyData;
      }

      object ICloneable.Clone() => (object) this.Clone();

      public UIReportItem Clone()
      {
        return new UIReportItem()
        {
          TraceLevel = this.TraceLevel,
          Id = this.Id,
          IndentLevel = this.IndentLevel,
          Header = this.Header,
          Text = this.Text,
          Context = UIReportItem.CloneArray(this.Context),
          Data = UIReportItem.CloneArray(this.Data)
        };
      }

      private static object[] CloneArray(object[] source)
      {
        if (source.Length == 0)
          return source;
        object[] objArray = new object[source.Length];
        source.CopyTo((Array) objArray, 0);
        return objArray;
      }

      public bool IsDataOnly => string.IsNullOrEmpty(this.text) && this.data.Length != 0;

      public TraceLevel TraceLevel
      {
        get => this.traceLevel;
        set => this.traceLevel = value;
      }

      public int Id
      {
        get => this.id;
        set => this.id = value >= 0 ? value : throw new ArgumentOutOfRangeException();
      }

      public int IndentLevel
      {
        get => this.indentLevel;
        set => this.indentLevel = value >= 0 ? value : throw new ArgumentOutOfRangeException();
      }

      public string Header
      {
        get => this.header;
        set => this.header = value != null ? value : throw new ArgumentNullException(nameof (Header));
      }

      public string Text
      {
        get => this.text;
        set => this.text = value != null ? value : throw new ArgumentNullException(nameof (Text));
      }

      public object[] Context
      {
        get => this.context;
        set => this.context = value != null ? value : throw new ArgumentNullException(nameof (Context));
      }

      public object[] Data
      {
        get => this.data;
        set => this.data = value != null ? value : throw new ArgumentNullException(nameof (Data));
      }
    }
}
