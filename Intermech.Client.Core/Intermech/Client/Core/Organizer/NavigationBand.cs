
// Type: Intermech.Client.Core.Organizer.NavigationBand
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.Organizer;

[Designer(typeof (NavigationBandDesigner))]
[ToolboxItem(false)]
public class NavigationBand : ContainerControl
{
  private BandRenderer _renderer = new BandRenderer();
  private NavigationButton _btn;
  private Image _img;
  private Image _sImg;
  private int _order;
  private int _originalOrder;
  private readonly object _threadLock = new object();

  /// <summary>
  /// 
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public NavigationButton Button
  {
    get => this._btn;
    set => this._btn = value;
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
      if (this._btn != null)
        this._btn.Image = value;
      this.Invalidate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public int Order
  {
    get => this._order;
    set => this._order = value;
  }

  /// <summary>
  /// 
  /// </summary>
  public int OriginalOrder
  {
    get => this._originalOrder;
    set => this._originalOrder = value;
  }

  /// <summary>
  /// 
  /// </summary>
  [DefaultValue(null)]
  [Category("Appearance")]
  public Image SmallImage
  {
    get => this._sImg;
    set
    {
      this._sImg = value;
      if (this._btn != null)
        this._btn.SmallImage = value;
      this.Invalidate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public override string Text
  {
    get => base.Text;
    set
    {
      base.Text = value;
      if (this.Parent == null)
        return;
      this.Parent.Invalidate();
    }
  }

  /// <summary>Конструктор.</summary>
  public NavigationBand(IContainer container)
  {
    container.Add((IComponent) this);
    this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
    this.SetStyle(ControlStyles.UserPaint, true);
    this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
    this.SetStyle(ControlStyles.ResizeRedraw, true);
    this.ResizeRedraw = true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnPaint(PaintEventArgs e)
  {
    base.OnPaint(e);
    this._renderer.DrawBackground(e.Graphics, this.ClientRectangle);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnTextChanged(EventArgs e)
  {
    base.OnTextChanged(e);
    if (this._btn == null)
      return;
    this._btn.Text = this.Text;
  }
}
