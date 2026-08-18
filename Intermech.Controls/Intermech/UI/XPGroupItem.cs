
// Type: Intermech.UI.XPGroupItem
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Intermech.UI;

/// <summary>Summary description for XPGroupItem.</summary>
public class XPGroupItem : UserControl
{
  private bool _selected;
  private bool _checked;
  private string _text = string.Empty;
  private ToolTip _toolTip;
  private ImageList _imageList;
  private int _imageIndex = -1;
  private Image _image;
  private XPGroupItemRenderer _renderer;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  /// <summary>
  /// 
  /// </summary>
  [DefaultValue("")]
  public string Hint
  {
    get => this._toolTip.GetToolTip((Control) this);
    set
    {
      this._toolTip.RemoveAll();
      this._toolTip.SetToolTip((Control) this, value);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [Category("Appearance")]
  [DefaultValue(-1)]
  [Editor("System.Windows.Forms.Design.ImageIndexEditor", typeof (UITypeEditor))]
  [TypeConverter(typeof (ImageIndexConverter))]
  [RefreshProperties(RefreshProperties.Repaint)]
  [Localizable(true)]
  public int ImageIndex
  {
    get
    {
      return this._imageIndex != -1 && this._imageList != null && this._imageIndex >= this._imageList.Images.Count ? this._imageList.Images.Count - 1 : this._imageIndex;
    }
    set
    {
      if (this._imageIndex == value)
        return;
      if (value != -1)
        this._image = (Image) null;
      this._imageIndex = value;
      this.Invalidate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [DefaultValue(null)]
  [Localizable(true)]
  public ImageList ImageList
  {
    get => this._imageList;
    set
    {
      if (this._imageList == value)
        return;
      if (value != null)
        this._image = (Image) null;
      this._imageList = value;
      this.Invalidate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [Description("Determines order in parent's list.")]
  [Category("Appearance")]
  public int Index
  {
    get
    {
      UserControl container = this.GetContainer();
      return container == null ? -1 : container.Controls.Count - container.Controls.IndexOf((Control) this) - 1;
    }
    set
    {
      if (value <= -1)
        return;
      UserControl container = this.GetContainer();
      if (container == null)
        return;
      if (value > container.Controls.Count)
        value = container.Controls.Count;
      container.Controls.SetChildIndex((Control) this, container.Controls.Count - value - 1);
      container.ResumeLayout();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public Image Image
  {
    [ImmutableObject(true), TypeConverter(typeof (ImageConverter))] get
    {
      return this._image == null && this._imageList != null && this.ImageIndex >= 0 ? this._imageList.Images[this.ImageIndex] : this._image;
    }
    set
    {
      if (this.Image == value)
        return;
      this._image = value;
      if (this._image != null)
      {
        this.ImageIndex = -1;
        this.ImageList = (ImageList) null;
      }
      this.Invalidate();
    }
  }

  [DefaultValue("")]
  [Category("Appearance")]
  [Localizable(true)]
  public override string Text
  {
    get => this._text;
    set => this._text = value;
  }

  /// <summary>Конструктор.</summary>
  public XPGroupItem()
  {
    this.InitializeComponent();
    this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.StandardClick | ControlStyles.Selectable | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
    this._renderer = new XPGroupItemRenderer();
    this._toolTip = new ToolTip();
    this._imageIndex = -1;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnXPGroupItem_MouseDown(object sender, MouseEventArgs e) => this._checked = true;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnXPGroupItem_MouseUp(object sender, MouseEventArgs e) => this._checked = false;

  /// <summary>
  /// 
  /// </summary>
  protected override Size DefaultSize => new Size(150, 23);

  /// <summary>Освобождение элемента от указателя мыши.</summary>
  /// <param name="e"></param>
  protected override void OnMouseLeave(EventArgs e)
  {
    this._selected = this._checked = false;
    this.Invalidate();
    base.OnMouseLeave(e);
  }

  /// <summary>Наведение указателя мыши на элемент.</summary>
  /// <param name="e"></param>
  protected override void OnMouseMove(MouseEventArgs e)
  {
    this._selected = true;
    this.Invalidate();
    base.OnMouseMove(e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnPaint(PaintEventArgs e)
  {
    base.OnPaint(e);
    Rectangle bounds;
    ref Rectangle local = ref bounds;
    Rectangle displayRectangle = this.DisplayRectangle;
    Point location = displayRectangle.Location;
    displayRectangle = this.DisplayRectangle;
    int width1 = displayRectangle.Width - 1;
    displayRectangle = this.DisplayRectangle;
    int height1 = displayRectangle.Height - 1;
    Size size = new Size(width1, height1);
    local = new Rectangle(location, size);
    if (this._checked)
      this._renderer.DrawItemHighlight(e.Graphics, bounds, false, XPGroupItemRenderer.HighlightMode.Pushed);
    else if (this._selected)
      this._renderer.DrawItemHighlight(e.Graphics, bounds, false, XPGroupItemRenderer.HighlightMode.Hot);
    int num = 0;
    if (this.Image != null)
    {
      num = this.Image.Width;
      int height2 = this.Image.Height;
      this.Height = height2 > this.Font.Height ? height2 + 8 : this.Font.Height + 8;
      if (this.Enabled)
        e.Graphics.DrawImage(this.Image, 10, this.Height / 2 - height2 / 2);
      else
        ControlPaint.DrawImageDisabled(e.Graphics, this.Image, 10, this.Height / 2 - height2 / 2, this.BackColor);
    }
    using (SolidBrush solidBrush = new SolidBrush(this.ForeColor))
    {
      int width2 = this.Width - num + 14;
      int height3 = this.Font.Height;
      RectangleF layoutRectangle = new RectangleF((float) (num + 14), (float) (this.Height / 2 - this.Font.Height / 2), (float) width2, (float) height3);
      StringFormat format = new StringFormat();
      format.Trimming = StringTrimming.EllipsisCharacter;
      format.LineAlignment = StringAlignment.Center;
      if (this.Enabled)
      {
        e.Graphics.DrawString(this._text, this.Font, (Brush) solidBrush, layoutRectangle, format);
      }
      else
      {
        Color control = SystemColors.Control;
        ControlPaint.DrawStringDisabled(e.Graphics, this._text, this.Font, control, layoutRectangle, format);
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="pevent"></param>
  protected override void OnPaintBackground(PaintEventArgs pevent)
  {
    Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
    if (this._selected || this._checked)
    {
      using (SolidBrush solidBrush = new SolidBrush(SystemColors.GradientInactiveCaption))
        pevent.Graphics.FillRectangle((Brush) solidBrush, rect);
      using (Pen pen = new Pen(SystemColors.ActiveCaption))
        pevent.Graphics.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
    }
    else
    {
      using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, SystemColors.Control, SystemColors.ControlLightLight, LinearGradientMode.Horizontal))
        pevent.Graphics.FillRectangle((Brush) linearGradientBrush, rect);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnParentChanged(EventArgs e)
  {
    base.OnParentChanged(e);
    if (this.Parent == null)
      return;
    System.Type type = this.Parent.GetType();
    if (type != typeof (XPGroupBox) && type != typeof (XPCollapser))
      return;
    this.Dock = DockStyle.Top;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  private UserControl GetContainer()
  {
    return (UserControl) (this.Parent as XPGroupBox) ?? (UserControl) (this.Parent as XPCollapser);
  }

  /// <summary>
  /// 
  /// </summary>
  private void ResetImage()
  {
    if (this._imageIndex != -1)
      return;
    this._image = (Image) null;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  private bool ShouldSerializeImage() => this._image != null;

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
    {
      this._renderer.Dispose();
      this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.SuspendLayout();
    this.BackgroundImageLayout = ImageLayout.Stretch;
    this.Name = nameof (XPGroupItem);
    this.Size = new Size(150, 23);
    this.MouseDown += new MouseEventHandler(this.OnXPGroupItem_MouseDown);
    this.MouseUp += new MouseEventHandler(this.OnXPGroupItem_MouseUp);
    this.ResumeLayout(false);
  }
}
