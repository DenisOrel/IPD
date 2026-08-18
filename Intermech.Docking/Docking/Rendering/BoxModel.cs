
// Type: Intermech.Docking.Rendering.BoxModel
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using System.Drawing;


namespace Intermech.Docking.Rendering;

public class BoxModel
{
  private BoxEdges a;
  private BoxEdges b;
  private int c;
  private int d;

  public BoxModel()
  {
    this.a = new BoxEdges();
    this.b = new BoxEdges();
  }

  public BoxModel(
    int width,
    int height,
    int paddingLeft,
    int paddingTop,
    int paddingRight,
    int paddingBottom,
    int marginLeft,
    int marginTop,
    int marginRight,
    int marginBottom)
  {
    this.c = width;
    this.d = height;
    this.b = new BoxEdges(paddingLeft, paddingTop, paddingRight, paddingBottom);
    this.a = new BoxEdges(marginLeft, marginTop, marginRight, marginBottom);
  }

  public Rectangle RemoveMargin(Rectangle source)
  {
    source.X += this.a.Left;
    source.Y += this.a.Top;
    source.Width -= this.a.Left + this.a.Right;
    source.Height -= this.a.Top + this.a.Bottom;
    return source;
  }

  public Rectangle RemovePadding(Rectangle source)
  {
    source.X += this.b.Left;
    source.Y += this.b.Top;
    source.Width -= this.b.Left + this.b.Right;
    source.Height -= this.b.Top + this.b.Bottom;
    return source;
  }

  public int ExtraHeight => this.a.Top + this.a.Bottom + this.b.Top + this.b.Bottom;

  public int ExtraWidth => this.a.Left + this.a.Right + this.b.Left + this.b.Right;

  public int Height
  {
    get => this.d;
    set => this.d = value;
  }

  public Size InnerSize
  {
    get => new Size(this.c - this.a.Left - this.a.Right, this.d - this.a.Top - this.a.Bottom);
  }

  public BoxEdges Margin
  {
    get => this.a;
    set => this.a = value;
  }

  public BoxEdges Padding
  {
    get => this.b;
    set => this.b = value;
  }

  public int Width
  {
    get => this.c;
    set => this.c = value;
  }
}
