
// Type: Intermech.Controls.OleContainer.STARTUPINFO
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class STARTUPINFO
{
  public int cb;
  public string lpReserved;
  public string lpDesktop;
  public string lpTitle;
  public int dwX;
  public int dwY;
  public int dwXSize;
  public int dwYSize;
  public int dwXCountChars;
  public int dwYCountChars;
  public int dwFillAttribute;
  public int dwFlags;
  public short wShowWindow;
  public short cbReserved2;
  public IntPtr lpReserved2;
  public IntPtr hStdInput;
  public IntPtr hStdOutput;
  public IntPtr hStdError;
}
