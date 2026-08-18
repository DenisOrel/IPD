
// Type: Intermech.Controls.OleContainer.PRINTDLGEX
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class PRINTDLGEX
{
  public int lStructSize;
  public IntPtr hwndOwner;
  public IntPtr hDevMode;
  public IntPtr hDevNames;
  public IntPtr hDC;
  public int Flags;
  public int Flags2;
  public int ExclusionFlags;
  public int nPageRanges;
  public int nMaxPageRanges;
  public IntPtr pageRanges;
  public int nMinPage;
  public int nMaxPage;
  public int nCopies;
  public IntPtr hInstance;
  public string lpPrintTemplateName;
  public WndProc lpCallback;
  public int nPropertyPages;
  public IntPtr lphPropertyPages;
  public int nStartPage;
  public int dwResultAction;
}
