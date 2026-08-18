
// Type: Intermech.Controls.OleContainer.LOGFONT
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class LOGFONT
{
  public int lfHeight;
  public int lfWidth;
  public int lfEscapement;
  public int lfOrientation;
  public int lfWeight;
  public byte lfItalic;
  public byte lfUnderline;
  public byte lfStrikeOut;
  public byte lfCharSet;
  public byte lfOutPrecision;
  public byte lfClipPrecision;
  public byte lfQuality;
  public byte lfPitchAndFamily;
  [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32 /*0x20*/)]
  public string lfFaceName;

  public LOGFONT()
  {
  }

  public LOGFONT(LOGFONT lf)
  {
    this.lfHeight = lf.lfHeight;
    this.lfWidth = lf.lfWidth;
    this.lfEscapement = lf.lfEscapement;
    this.lfOrientation = lf.lfOrientation;
    this.lfWeight = lf.lfWeight;
    this.lfItalic = lf.lfItalic;
    this.lfUnderline = lf.lfUnderline;
    this.lfStrikeOut = lf.lfStrikeOut;
    this.lfCharSet = lf.lfCharSet;
    this.lfOutPrecision = lf.lfOutPrecision;
    this.lfClipPrecision = lf.lfClipPrecision;
    this.lfQuality = lf.lfQuality;
    this.lfPitchAndFamily = lf.lfPitchAndFamily;
    this.lfFaceName = lf.lfFaceName;
  }
}
