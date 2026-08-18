
// Type: Intermech.Client.Core.Organizer.NavigationCollapsedBand
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// 
/// </summary>
[ToolboxItem(false)]
public class NavigationCollapsedBand : ContainerControl
{
  private BandRenderer _renderer = new BandRenderer();
  private Font _headerFont = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
  private InputState _input;
  private readonly object _threadLock = new object();

  /// <summary>Конструктор.</summary>
  public NavigationCollapsedBand()
  {
    this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
    this.SetStyle(ControlStyles.UserPaint, true);
    this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
    this.SetStyle(ControlStyles.ResizeRedraw, true);
    this.Name = "navigationBarBar.CollapsedBand1";
    this.Visible = false;
    this.ResizeRedraw = true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="disposing"></param>
  protected override void Dispose(bool disposing)
  {
    if (this._headerFont != null)
    {
      this._headerFont.Dispose();
      this._headerFont = (Font) null;
    }
    base.Dispose(disposing);
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
    this._input = InputState.Hovered;
    this.Invalidate();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnPaintBackground(PaintEventArgs e)
  {
    base.OnPaintBackground(e);
    this._renderer.DrawCollapsedBand(e.Graphics, this.ClientRectangle, this.Text, this._headerFont, this._input);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnResize(EventArgs e)
  {
    base.OnResize(e);
    this.Invalidate();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnTextChanged(EventArgs e)
  {
    base.OnTextChanged(e);
    this.Invalidate();
  }
}
