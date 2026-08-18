
// Type: Intermech.Controls.OleContainer.OBJECTDESCRIPTOR
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Drawing;
using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[StructLayout(LayoutKind.Sequential)]
public class OBJECTDESCRIPTOR
{
  public uint cbSize;
  public Guid clsid;
  public DVASPECT dwDrawAspect;
  public Size sizel;
  public Point pointl;
  public int dwStatus;
  public int dwFullUserTypeName;
  public int dwSrcOfCopy;
}
