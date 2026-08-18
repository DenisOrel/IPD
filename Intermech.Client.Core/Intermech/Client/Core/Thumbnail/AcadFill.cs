
// Type: Intermech.Client.Core.Thumbnail.AcadFill
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Drawing;


namespace Intermech.Client.Core.Thumbnail;

internal class AcadFill : AcadObject
{
  internal Point[] _points;

  internal AcadFill(Point[] points) => this._points = points;

  internal override void Patch(int dx, int dy, int offset)
  {
    int length = this._points.Length;
    for (int index = 0; index < length; ++index)
      this._points[index] = this.PatchPoint(this._points[index], dx, dy, offset);
  }

  internal override void Draw(Graphics g, Brush b, Pen p) => g.FillPolygon(b, this._points);
}
