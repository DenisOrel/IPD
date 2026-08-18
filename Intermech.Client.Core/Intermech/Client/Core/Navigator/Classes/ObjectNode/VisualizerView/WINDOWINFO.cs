
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.WINDOWINFO
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Drawing;
using System.Runtime.InteropServices;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView;

internal struct WINDOWINFO
{
  public uint Size;
  public Rectangle WindowRect;
  public Rectangle ClientRect;
  public uint Style;
  public uint ExStyle;
  public uint WindowStatus;
  public uint WindowBordersX;
  public uint WindowBordersY;
  public ushort WindowType;
  public ushort CreatorVersion;

  public WINDOWINFO(bool? filler)
    : this()
  {
    this.Size = (uint) Marshal.SizeOf(typeof (WINDOWINFO));
  }
}
