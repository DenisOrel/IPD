
// Type: Intermech.Controls.OleContainer.OPENFILENAME_I
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class OPENFILENAME_I
{
  public int lStructSize;
  public IntPtr hwndOwner;
  public IntPtr hInstance;
  public string lpstrFilter;
  public IntPtr lpstrCustomFilter;
  public int nMaxCustFilter;
  public int nFilterIndex;
  public IntPtr lpstrFile;
  public int nMaxFile;
  public IntPtr lpstrFileTitle;
  public int nMaxFileTitle;
  public string lpstrInitialDir;
  public string lpstrTitle;
  public int Flags;
  public short nFileOffset;
  public short nFileExtension;
  public string lpstrDefExt;
  public IntPtr lCustData;
  public WndProc lpfnHook;
  public string lpTemplateName;
  public IntPtr pvReserved;
  public int dwReserved;
  public int FlagsEx;

  public OPENFILENAME_I()
  {
    this.lStructSize = Marshal.SizeOf(typeof (OPENFILENAME_I));
    this.nMaxFile = 260;
    this.nMaxFileTitle = 260;
  }
}
