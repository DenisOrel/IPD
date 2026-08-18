
// Type: Intermech.Redline.RedLinePencilTool
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
public class RedLinePencilTool : MapTool
{
  private Redliner _redliner;
  private IMapRelative _relative;
  private MapRedStroke _pencil;
  private bool _isActive;

  internal RedLinePencilTool(Redliner redliner)
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
    if (this._pencil != null)
    {
      this._pencil.Remove();
      this._pencil = (MapRedStroke) null;
    }
    this.View.Cursor = this.View.DefaultCursor;
  }

  /// <summary>действия когда клавиша мыши нажата</summary>
  public override void DoMouseDown()
  {
    if (this.LastInput.Buttons != MouseButtons.Left)
      return;
    if (this._pencil == null)
    {
      this._pencil = new MapRedStroke();
      this._pencil.Pen = new Pen(this._redliner.PenColorAlpha, this._redliner.PenWidthInDrawingUnits);
      this._pencil.Pen.DashStyle = DashStyle.Solid;
      this.View.Layers.Default.Add((MapObject) this._pencil);
      this._isActive = true;
    }
    this._pencil.AddPoint(this.LastInput.DocPoint);
  }

  /// <summary>действия когда мышь двигают</summary>
  public override void DoMouseMove()
  {
    if (!this._isActive || this._pencil == null || !(this._pencil.GetLastPoint() != this.LastInput.DocPoint))
      return;
    this._pencil.AddPoint(this.LastInput.DocPoint);
  }

  /// <summary>действия когда клавиша мыши отпущена</summary>
  public override void DoMouseUp()
  {
    if (this.LastInput.Buttons != MouseButtons.Left)
      return;
    if (this._pencil != null && this._pencil.PointsCount > 2)
    {
      this.StartTransaction();
      if (this._relative != null)
      {
        this._pencil.Relative = this._relative;
        this._pencil.RelativeId = this._relative.GetId(this._pencil.GetPoint(0));
      }
      this._redliner.AddNewObject((MapObject) this._pencil);
      this._pencil = (MapRedStroke) null;
      this._isActive = false;
      this.TransactionResult = "New Pencil";
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
