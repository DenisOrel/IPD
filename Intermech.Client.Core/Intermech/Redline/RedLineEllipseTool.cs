
// Type: Intermech.Redline.RedLineEllipseTool
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Map;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Intermech.Redline;

[Serializable]
public class RedLineEllipseTool : MapTool
{
  private PointF _pointLast;
  private PointF _pointFirst;
  private MapRedEllipse _ellipse;
  private Redliner _redliner;
  private IMapRelative _relative;

  internal RedLineEllipseTool(Redliner redliner)
    : base(redliner.View)
  {
    this._redliner = redliner;
    this._relative = this._redliner.Relative;
  }

  /// <summary>сменить курсор перед работой</summary>
  public override void Start()
  {
    this.View.Cursor = Cursors.Cross;
    this.View.InitFocus();
  }

  /// <summary>уничтожить создаваемый объект и востановить курсор</summary>
  public override void Stop()
  {
    if (this._ellipse != null)
    {
      this._ellipse.Remove();
      this._ellipse = (MapRedEllipse) null;
    }
    this.View.Cursor = this.View.DefaultCursor;
  }

  /// <summary>действия когда клавиша мыши нажата</summary>
  public override void DoMouseDown()
  {
    if (this.LastInput.Buttons != MouseButtons.Left || this._ellipse != null)
      return;
    this._pointFirst = this.LastInput.DocPoint;
    this._ellipse = new MapRedEllipse();
    this._ellipse.Pen = new Pen(this._redliner.PenColorAlpha, this._redliner.PenWidthInDrawingUnits);
    this._ellipse.Pen.DashStyle = DashStyle.Solid;
    this._ellipse.Bounds = new RectangleF(this._pointFirst, new SizeF(0.0f, 0.0f));
    this.View.Layers.Default.Add((MapObject) this._ellipse);
  }

  /// <summary>действия когда мышь двигают</summary>
  public override void DoMouseMove()
  {
    if (this._ellipse == null || this._pointLast == this.LastInput.DocPoint)
      return;
    this._pointLast = this.LastInput.DocPoint;
    float x = Math.Min(this._pointFirst.X, this._pointLast.X);
    float y = Math.Min(this._pointFirst.Y, this._pointLast.Y);
    float width = Math.Max(this._pointFirst.X, this._pointLast.X) - x;
    float height = Math.Max(this._pointFirst.Y, this._pointLast.Y) - y;
    this._ellipse.Bounds = new RectangleF(x, y, width, height);
  }

  /// <summary>действия когда клавиша мыши отпущена</summary>
  public override void DoMouseUp()
  {
    if (this.LastInput.Buttons != MouseButtons.Left)
      return;
    if (this._ellipse != null)
    {
      this.StartTransaction();
      if (this._relative != null)
      {
        this._ellipse.Relative = this._relative;
        this._ellipse.RelativeId = this._relative.GetId(this._ellipse.Center);
      }
      this._redliner.AddNewObject((MapObject) this._ellipse);
      this._ellipse = (MapRedEllipse) null;
      this.TransactionResult = "New RedEllipse";
      this.StopTransaction();
      this._redliner.SetDirty(true);
      this._redliner.OnChanged();
      this.TransactionResult = (string) null;
    }
    else
      this.DoCancelMouse();
  }

  /// <summary>действия когда клавиша клавиатуры нажата</summary>
  public override void DoKeyDown()
  {
    if (this.LastInput.Key == Keys.Escape)
    {
      this.DoCancelMouse();
      this._redliner.OnChanged();
    }
    else
      base.DoKeyDown();
  }

  public override void DoMouseWheel() => this.View.DoWheel(this.LastInput);
}
