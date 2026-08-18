
// Type: Intermech.Client.Core.Organizer.NavigationCollapseButton
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.Organizer;

[ToolboxItem(false)]
public class NavigationCollapseButton : Control
{
  private CollapseButtonRenderer _renderer = new CollapseButtonRenderer();
  private InputState _input;
  private bool _isCollapsed;

  /// <summary>
  /// 
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool IsCollapsed
  {
    get => this._isCollapsed;
    set
    {
      this._isCollapsed = value;
      this.Invalidate();
    }
  }

  /// <summary>Конструктор.</summary>
  public NavigationCollapseButton()
  {
    this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
    this.SetStyle(ControlStyles.UserPaint, true);
    this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
    this.SetStyle(ControlStyles.ResizeRedraw, true);
    this.SuspendLayout();
    this.Size = new Size(18, 18);
    this.ResumeLayout();
    this.Name = "navigationCollapseButton";
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseDown(MouseEventArgs e)
  {
    base.OnMouseDown(e);
    this._input = InputState.Clicked;
    this.Invalidate();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseEnter(EventArgs e)
  {
    base.OnMouseEnter(e);
    this._input = InputState.Hovered;
    this.Invalidate();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseLeave(EventArgs e)
  {
    base.OnMouseLeave(e);
    this._input = InputState.Normal;
    this.Invalidate();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseUp(MouseEventArgs e)
  {
    base.OnMouseUp(e);
    this._input = InputState.Normal;
    this.Invalidate();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnPaint(PaintEventArgs e)
  {
    base.OnPaint(e);
    this._renderer.DrawBackground(e.Graphics, this.ClientRectangle, this._input);
    Rectangle clientRectangle = this.ClientRectangle;
    clientRectangle.X += 10;
    clientRectangle.Width -= 10;
    this._renderer.Draw(e.Graphics, this.ClientRectangle, this._input, this._isCollapsed);
  }
}
