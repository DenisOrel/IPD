
// Type: Intermech.Controls.OleContainer.MMCKINFO
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class MMCKINFO
{
  public int ckID;
  public int cksize;
  public int fccType;
  public int dwDataOffset;
  public int dwFlags;
}
