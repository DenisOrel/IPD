
// Type: Intermech.Controls.OleContainer.STGMEDIUM
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

public struct STGMEDIUM
{
  public TYMED tymed;
  public IntPtr unionmember;
  [MarshalAs(UnmanagedType.IUnknown)]
  public object pUnkForRelease;
}
