
// Type: Intermech.Controls.OleContainer.TYMED
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;


namespace Intermech.Controls.OleContainer;

[Flags]
public enum TYMED
{
  TYMED_ENHMF = 64, // 0x00000040
  TYMED_FILE = 2,
  TYMED_GDI = 16, // 0x00000010
  TYMED_HGLOBAL = 1,
  TYMED_ISTORAGE = 8,
  TYMED_ISTREAM = 4,
  TYMED_MFPICT = 32, // 0x00000020
  TYMED_NULL = 0,
  TYMED_NONE = -1, // 0xFFFFFFFF
}
