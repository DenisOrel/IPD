
// Type: Intermech.Controls.OleContainer.REOBJECT
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Drawing;
using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[StructLayout(LayoutKind.Sequential)]
public class REOBJECT
{
  public int cbStruct = Marshal.SizeOf(typeof (REOBJECT));
  public int cp;
  public Guid clsid;
  public IntPtr poleobj;
  public IStorage pstg;
  public IOleClientSite polesite;
  public Size sizel;
  public uint dvAspect;
  public uint dwFlags;
  public uint dwUser;
}
