
// Type: Intermech.Controls.OleContainer.EDITSTREAM64
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[StructLayout(LayoutKind.Sequential)]
public class EDITSTREAM64
{
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
  public byte[] contents;

  public EDITSTREAM64() => this.contents = new byte[20];
}
