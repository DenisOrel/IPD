
// Type: Intermech.Controls.OleContainer.CHARFORMAT2A
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[StructLayout(LayoutKind.Sequential, Pack = 4)]
public class CHARFORMAT2A
{
  public int cbSize;
  public int dwMask;
  public int dwEffects;
  public int yHeight;
  public int yOffset;
  public int crTextColor;
  public byte bCharSet;
  public byte bPitchAndFamily;
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32 /*0x20*/)]
  public byte[] szFaceName;
  public short wWeight;
  public short sSpacing;
  public int crBackColor;
  public int lcid;
  public int dwReserved;
  public short sStyle;
  public short wKerning;
  public byte bUnderlineType;
  public byte bAnimation;
  public byte bRevAuthor;

  public CHARFORMAT2A()
  {
    this.cbSize = Marshal.SizeOf(typeof (CHARFORMAT2A));
    this.szFaceName = new byte[32 /*0x20*/];
  }
}
