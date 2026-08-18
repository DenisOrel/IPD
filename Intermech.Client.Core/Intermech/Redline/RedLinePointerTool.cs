
// Type: Intermech.Redline.RedLinePointerTool
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Map;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Redline;

[Serializable]
public class RedLinePointerTool : MapTool
{
  private Redliner _redliner;
  private bool _isActive;
  private PointF _point;

  internal RedLinePointerTool(Redliner redliner)
    : base(redliner.View)
  {
    this._redliner = redliner;
  }

  /// <summary>сменить курсор перед работой</summary>
  public override void Start()
  {
    this.View.Cursor = Cursors.Arrow;
    this.View.InitFocus();
  }

  /// <summary>уничтожить создаваемый объект и востановить курсор</summary>
  public override void Stop() => this.View.Cursor = this.View.DefaultCursor;

  /// <summary>действия когда клавиша мыши нажата</summary>
  public override void DoMouseDown()
  {
    if (this.LastInput.Buttons != MouseButtons.Left)
      return;
    this._isActive = true;
    this._point = this.LastInput.DocPoint;
  }

  /// <summary>действия когда клавиша мыши отпущена</summary>
  public override void DoMouseUp()
  {
    if (this.LastInput.Buttons != MouseButtons.Left)
      return;
    this.DoSelect(this.LastInput);
    this.DoClick(this.LastInput);
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
