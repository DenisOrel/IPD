
// Type: Intermech.Mvp.Components.MultiCommandItem
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Mvp.Components
{
    public class MultiCommandItem
    {
      public MultiCommandItem(string text, object tag)
      {
        this.Text = text;
        this.Tag = tag;
      }

      public string Text { get; private set; }

      public object Tag { get; private set; }
    }
}
