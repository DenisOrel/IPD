// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.BoundsHelper
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System.Drawing;

#nullable disable
namespace Intermech.Interfaces.Document;

internal class BoundsHelper
{
  internal static RectangleF SetLocation(RectangleF rect, PointF loc)
  {
    return new RectangleF(loc, rect.Size);
  }

  internal static RectangleF SetSize(RectangleF rect, SizeF size)
  {
    return new RectangleF(rect.Location, size);
  }

  internal static RectangleF SetWidth(RectangleF rect, float w)
  {
    return new RectangleF(rect.X, rect.Y, w, rect.Height);
  }

  internal static RectangleF SetHeight(RectangleF rect, float h)
  {
    return new RectangleF(rect.X, rect.Y, rect.Width, h);
  }

  internal static RectangleF SetX(RectangleF rect, float x)
  {
    return new RectangleF(x, rect.Y, rect.Width, rect.Height);
  }

  internal static RectangleF SetY(RectangleF rect, float y)
  {
    return new RectangleF(rect.X, y, rect.Width, rect.Height);
  }
}
