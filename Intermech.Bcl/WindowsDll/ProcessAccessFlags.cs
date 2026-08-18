
// Type: Intermech.WindowsDll.ProcessAccessFlags
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.WindowsDll
{
    [Flags]
    public enum ProcessAccessFlags
    {
      All = 2035711, // 0x001F0FFF
      Terminate = 1,
      CreateThread = 2,
      VirtualMemoryOperation = 8,
      VirtualMemoryRead = 16, // 0x00000010
      VirtualMemoryWrite = 32, // 0x00000020
      DuplicateHandle = 64, // 0x00000040
      CreateProcess = 128, // 0x00000080
      SetQuota = 256, // 0x00000100
      SetInformation = 512, // 0x00000200
      QueryInformation = 1024, // 0x00000400
      QueryLimitedInformation = 4096, // 0x00001000
      Synchronize = 1048576, // 0x00100000
    }
}
