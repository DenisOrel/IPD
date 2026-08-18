
// Type: Intermech.Controls.OleContainer.DVASPECT
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;


namespace Intermech.Controls.OleContainer;

[Flags]
public enum DVASPECT
{
  NONE = -1, // 0xFFFFFFFF
  DVASPECT_CONTENT = 1,
  DVASPECT_DOCPRINT = 8,
  DVASPECT_ICON = 4,
  DVASPECT_THUMBNAIL = 2,
}
