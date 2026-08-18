
// Type: Intermech.Client.Core.Thumbnail.AcadLine
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Drawing;


namespace Intermech.Client.Core.Thumbnail;

internal class AcadLine : AcadObject
{
  internal Point _from;
  internal Point _to;

  internal AcadLine(int x1, int y1, int x2, int y2)
  {
    this._from = new Point(x1, y1);
    this._to = new Point(x2, y2);
  }

  internal override void Patch(int dx, int dy, int offset)
  {
    this._from = this.PatchPoint(this._from, dx, dy, offset);
    this._to = this.PatchPoint(this._to, dx, dy, offset);
  }

  internal override void Draw(Graphics g, Brush b, Pen p) => g.DrawLine(p, this._from, this._to);
}
