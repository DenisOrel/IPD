
// Type: Intermech.Controls.OleContainer.ADVF
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;


namespace Intermech.Controls.OleContainer;

[Flags]
public enum ADVF
{
  ADVF_NODATA = 1,
  ADVF_PRIMEFIRST = 2,
  ADVF_ONLYONCE = 4,
  ADVF_DATAONSTOP = 64, // 0x00000040
  ADVFCACHE_NOHANDLER = 8,
  ADVFCACHE_FORCEBUILTIN = 16, // 0x00000010
  ADVFCACHE_ONSAVE = 32, // 0x00000020
}
