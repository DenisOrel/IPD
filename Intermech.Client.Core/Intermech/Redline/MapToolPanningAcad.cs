
// Type: Intermech.Redline.MapToolPanningAcad
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Map;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Redline;

internal class MapToolPanningAcad(MapView v) : MapTool(v)
{
  [NonSerialized]
  private bool myActive;
  private bool myModal;
  [NonSerialized]
  private PointF myOrigin;

  public override bool CanStart()
  {
    MapInputEventArgs lastInput = this.LastInput;
    return !lastInput.Alt && !lastInput.Control && !lastInput.Shift && lastInput.Buttons == MouseButtons.Middle;
  }

  public override void DoKeyDown() => this.StopTool();

  private void DoManualPan()
  {
    PointF origin = this.myOrigin;
    Size s;
    ref Size local = ref s;
    Point viewPoint = this.LastInput.ViewPoint;
    int x1 = viewPoint.X;
    viewPoint = this.FirstInput.ViewPoint;
    int x2 = viewPoint.X;
    int width = x1 - x2;
    viewPoint = this.LastInput.ViewPoint;
    int y1 = viewPoint.Y;
    viewPoint = this.FirstInput.ViewPoint;
    int y2 = viewPoint.Y;
    int height = y1 - y2;
    local = new Size(width, height);
    SizeF doc = this.View.ConvertViewToDoc(s);
    this.View.DocPosition = new PointF(origin.X - doc.Width, origin.Y - doc.Height);
  }

  private void Activate()
  {
    this.View.Cursor = Cursors.NoMove2D;
    this.myOrigin = this.View.DocPosition;
    this.myActive = true;
  }

  public override void DoMouseDown()
  {
    if (!this.CanStart())
      return;
    this.Activate();
  }

  public override void DoMouseMove()
  {
    if (!this.myActive)
    {
      if (this.Modal)
        return;
      this.Activate();
    }
    else
      this.DoManualPan();
  }

  public override void DoMouseUp()
  {
    if (this.myActive)
    {
      this.DoManualPan();
      this.View.OnViewChanging();
    }
    this.Stop();
    this.StopTool();
  }

  public override void DoMouseWheel() => this.StopTool();

  public override void Stop()
  {
    this.myActive = false;
    this.View.Cursor = this.View.DefaultCursor;
  }

  public virtual bool Modal
  {
    get => this.myModal;
    set => this.myModal = value;
  }
}
