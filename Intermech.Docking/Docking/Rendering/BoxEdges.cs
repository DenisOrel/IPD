
// Type: Intermech.Docking.Rendering.BoxEdges
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll


namespace Intermech.Docking.Rendering;

public class BoxEdges
{
  private int a;
  private int b;
  private int c;
  private int d;

  public BoxEdges()
  {
  }

  public BoxEdges(int left, int top, int right, int bottom)
  {
    this.a = left;
    this.b = top;
    this.d = right;
    this.c = bottom;
  }

  public int Bottom => this.c;

  public int Left => this.a;

  public int Right => this.d;

  public int Top => this.b;
}
