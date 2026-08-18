
// Type: Intermech.WindowsDll.FormatMessageFlags
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.WindowsDll
{
    [Flags]
    public enum FormatMessageFlags
    {
      AllocateBuffer = 256, // 0x00000100
      ArgumentArray = 8192, // 0x00002000
      FromHModule = 2048, // 0x00000800
      FromString = 1024, // 0x00000400
      FromSystem = 4096, // 0x00001000
      IgnoreInserts = 512, // 0x00000200
    }
}
