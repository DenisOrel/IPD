
// Type: Intermech.Mvp.Components.MultiCommandEventArgs
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Mvp.Components
{
    public class MultiCommandEventArgs : EventArgs
    {
      private readonly MultiCommandItem item;

      public MultiCommandEventArgs(MultiCommandItem item)
      {
        this.item = item != null ? item : throw new ArgumentNullException(nameof (item));
      }

      public MultiCommandItem Item => this.item;
    }
}
