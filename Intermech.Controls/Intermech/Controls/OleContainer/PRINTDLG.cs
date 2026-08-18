
// Type: Intermech.Controls.OleContainer.PRINTDLG
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Auto)]
public class PRINTDLG
{
  public int lStructSize;
  public IntPtr hwndOwner;
  public IntPtr hDevMode;
  public IntPtr hDevNames;
  public IntPtr hDC;
  public int Flags;
  public short nFromPage;
  public short nToPage;
  public short nMinPage;
  public short nMaxPage;
  public short nCopies;
  public IntPtr hInstance;
  public IntPtr lCustData;
  public WndProc lpfnPrintHook;
  public WndProc lpfnSetupHook;
  public string lpPrintTemplateName;
  public string lpSetupTemplateName;
  public IntPtr hPrintTemplate;
  public IntPtr hSetupTemplate;
}
