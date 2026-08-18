
// Type: Intermech.Mvp.Components.IWindowCollection`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Mvp.Components
{
    public interface IWindowCollection<TWindow>
    {
      TWindow AddWindow();

      void RemoveWindow(TWindow window);

      TWindow ActiveWindow { get; set; }

      event EventHandler ActiveWindowChanged;
    }
}
