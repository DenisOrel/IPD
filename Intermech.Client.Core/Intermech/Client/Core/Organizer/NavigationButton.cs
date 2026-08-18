
// Type: Intermech.Client.Core.Organizer.NavigationButton
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
public class NavigationButton : ContainerControl
{
  protected NavButtonRenderer _renderer = new NavButtonRenderer();
  private Image _img;
  private Image _sImg;
  private bool _active;
  private bool _isSmall;
  private bool _isCollapsed;
  protected InputState _input;
  private readonly object _threadLock = new object();

  /// <summary>
  /// 
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool Active
  {
    get => this._active;
    set
    {
      this._active = value;
      this.Invalidate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [DefaultValue(null)]
  [Category("Appearance")]
  public Image Image
  {
    get => this._img;
    set
    {
      this._img = value;
      this.Invalidate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual bool IsCollapsed
  {
    get => this._isCollapsed;
    set
    {
      this._isCollapsed = value;
      this.Invalidate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool IsSmall
  {
    get => this._isSmall;
    set
    {
      this._isSmall = value;
      this.Invalidate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [DefaultValue(null)]
  [Localizable(true)]
  public Image SmallImage
  {
    get => this._sImg;
    set
    {
      this._sImg = value;
      this.Invalidate();
    }
  }

  /// <summary>Конкструктор.</summary>
  public NavigationButton()
  {
    this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
    this.SetStyle(ControlStyles.UserPaint, true);
    this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
    this.SetStyle(ControlStyles.ResizeRedraw, true);
    this.Visible = true;
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
    this._renderer.DrawBackground(e.Graphics, this.ClientRectangle, this._active, this._input);
    if (this._isSmall)
    {
      if (this._sImg == null)
        return;
      Point location = new Point(this.Width / 2 - this._sImg.Width / 2, this.Height / 2 - this._sImg.Height / 2);
      this._renderer.DrawImage(e.Graphics, location, this._sImg);
    }
    else
    {
      Rectangle clientRectangle = this.ClientRectangle;
      if (this._img != null)
      {
        Point location = new Point(this._isCollapsed ? 4 : 10, this.Height / 2 - this._img.Height / 2);
        this._renderer.DrawImage(e.Graphics, location, this._img);
        clientRectangle.X += 32 /*0x20*/;
        clientRectangle.Width -= 32 /*0x20*/;
      }
      clientRectangle.X += 10;
      clientRectangle.Width -= 10;
      if (this._isCollapsed)
        return;
      this._renderer.DrawText(e.Graphics, clientRectangle, this.Font, this.Text);
    }
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
