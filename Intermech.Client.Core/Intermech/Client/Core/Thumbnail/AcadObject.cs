
// Type: Intermech.Client.Core.Thumbnail.AcadObject
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Drawing;


namespace Intermech.Client.Core.Thumbnail;

internal abstract class AcadObject
{
  internal Point PatchPoint(Point pt, int dx, int dy, int offset)
  {
    Point point = new Point()
    {
      X = pt.X - dx,
      Y = pt.Y - dy
    };
    point.Y = offset - point.Y;
    return point;
  }

  internal abstract void Draw(Graphics g, Brush b, Pen p);

  internal abstract void Patch(int dx, int dy, int offset);
}
