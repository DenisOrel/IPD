// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.InformationRequest.DesktopLayout
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;
using System.Drawing;

#nullable disable
namespace Intermech.Interfaces.Client.InformationRequest;

internal sealed class DesktopLayout
{
  private Rectangle _bounds;
  private int _count;
  private Rectangle[] _displays;
  private bool _workingAreaOnly;

  public DesktopLayout() => this.Initialize();

  public Rectangle Bounds => this._bounds;

  public int Count => this._count;

  public int Height => this._bounds.Height;

  public int Width => this._bounds.Width;

  public bool WorkingAreaOnly
  {
    get => this._workingAreaOnly;
    set
    {
      if (this._workingAreaOnly == value)
        return;
      this._workingAreaOnly = value;
      this.Initialize();
    }
  }

  public Rectangle GetDisplayBounds(int index) => this._displays[index];

  public Rectangle GetNormalizedDisplayBounds(int index)
  {
    return this.GetNormalizedDisplayBounds(this._displays[index]);
  }

  public Rectangle GetNormalizedDisplayBounds(Rectangle bounds)
  {
    return new Rectangle(bounds.X - this._bounds.X, bounds.Y - this._bounds.Y, bounds.Width, bounds.Height);
  }

  private void Initialize()
  {
    List<DisplayInfo> displays = ScreenshotCapture.GetDisplays();
    int num1 = 0;
    int num2 = 0;
    int val1_1 = 0;
    int val1_2 = 0;
    this._count = displays.Count;
    this._displays = new Rectangle[this._count];
    for (int index = 0; index < this._count; ++index)
    {
      DisplayInfo displayInfo = displays[index];
      Rectangle rectangle = (Rectangle) (this._workingAreaOnly ? displayInfo.WorkArea : displayInfo.MonitorArea);
      this._displays[index] = rectangle;
      num1 = Math.Min(num1, rectangle.X);
      num2 = Math.Min(num2, rectangle.Y);
      val1_1 = Math.Max(val1_1, rectangle.Right);
      val1_2 = Math.Max(val1_2, rectangle.Bottom);
    }
    this._bounds = new Rectangle(num1, num2, val1_1 - num1, val1_2 - num2);
  }
}
