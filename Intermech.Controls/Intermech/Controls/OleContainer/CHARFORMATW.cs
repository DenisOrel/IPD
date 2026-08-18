
// Type: Intermech.Controls.OleContainer.CHARFORMATW
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[StructLayout(LayoutKind.Sequential, Pack = 4)]
public class CHARFORMATW
{
  public int cbSize;
  public int dwMask;
  public int dwEffects;
  public int yHeight;
  public int yOffset;
  public int crTextColor;
  public byte bCharSet;
  public byte bPitchAndFamily;
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64 /*0x40*/)]
  public byte[] szFaceName;

  public CHARFORMATW()
  {
    this.cbSize = Marshal.SizeOf(typeof (CHARFORMATW));
    this.szFaceName = new byte[64 /*0x40*/];
  }
}
