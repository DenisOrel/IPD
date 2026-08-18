
// Type: Intermech.Controls.OleContainer.PAGESETUPDLG
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[StructLayout(LayoutKind.Sequential)]
public class PAGESETUPDLG
{
  public int lStructSize;
  public IntPtr hwndOwner;
  public IntPtr hDevMode;
  public IntPtr hDevNames;
  public int Flags;
  public int paperSizeX;
  public int paperSizeY;
  public int minMarginLeft;
  public int minMarginTop;
  public int minMarginRight;
  public int minMarginBottom;
  public int marginLeft;
  public int marginTop;
  public int marginRight;
  public int marginBottom;
  public IntPtr hInstance;
  public IntPtr lCustData;
  public WndProc lpfnPageSetupHook;
  public WndProc lpfnPagePaintHook;
  public string lpPageSetupTemplateName;
  public IntPtr hPageSetupTemplate;
}
