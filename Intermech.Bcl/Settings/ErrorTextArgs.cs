
// Type: Intermech.Settings.ErrorTextArgs
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Settings
{
    public sealed class ErrorTextArgs : EventArgs
    {
      private string text;

      public ErrorTextArgs(string text)
      {
        this.text = text != null ? text : throw new ArgumentNullException(nameof (text));
      }

      public string Text
      {
        get => this.text;
        set
        {
          if (value == null)
            throw new ArgumentNullException(nameof (Text));
          if (string.Equals(this.text, value))
            return;
          this.text = value;
        }
      }
    }
}
