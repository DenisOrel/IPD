
// Type: Intermech.Diagnostics.Modifier
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Diagnostics
{
    /// <summary>Модификатор аргумента метода</summary>
    [Flags]
    public enum Modifier
    {
      None = 0,
      In = 1,
      Ref = 2,
      Out = 4,
    }
}
