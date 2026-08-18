
// Type: Intermech.Controls.OleContainer.MSOCRINFOSTRUCT
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class MSOCRINFOSTRUCT
{
  public int cbSize;
  public int uIdleTimeInterval;
  public int grfcrf;
  public int grfcadvf;

  public MSOCRINFOSTRUCT() => this.cbSize = Marshal.SizeOf(typeof (MSOCRINFOSTRUCT));
}
