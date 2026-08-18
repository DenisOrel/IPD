
// Type: Intermech.Docking.TabPage
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using Intermech.Docking.Designers;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.Docking;

[ToolboxItem(false)]
[Designer(typeof (TabPageDesigner))]
public class TabPage : ScrollableControl
{
  internal bool _textTrimmed;
  internal bool _tabVisible;
  internal double _tabWidth;
  private Image _image;
  private Image _workingImage;
  private int _imageIndex;
  internal Rectangle _tabBounds;
  private int _maximumTabWidth;
  private Intermech.Docking.Rendering.BorderStyle _borderStyle;

  public TabPage()
  {
    this._maximumTabWidth = 0;
    this._textTrimmed = false;
    this._tabVisible = true;
    this._imageIndex = -1;
    this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
    this._borderStyle = Intermech.Docking.Rendering.BorderStyle.None;
  }

  protected override void Dispose(bool disposing)
  {
    base.Dispose(disposing);
    if (!disposing)
      return;
    this.ClearWorkingImage();
  }

  private void ClearWorkingImage()
  {
    if (this._workingImage == null)
      return;
    this._workingImage.Dispose();
    this._workingImage = (Image) null;
  }

  public TabPage(string text)
    : this()
  {
    this.Text = text;
  }

  protected void UpdateParent()
  {
    if (!(this.Parent is PageControl parent))
      return;
    if (!this._tabVisible && parent.SelectedPage == this)
    {
      bool flag = false;
      foreach (TabPage tabPage in parent.TabPages)
      {
        if (tabPage.TabVisible)
        {
          parent.SelectedPage = tabPage;
          parent.Invalidate();
          flag = true;
          break;
        }
      }
      if (!flag)
      {
        this.Visible = false;
        parent.SetSelectedPage((TabPage) null);
        parent.Invalidate();
      }
    }
    else if (this._tabVisible && parent.SelectedPage == null)
    {
      parent.SetSelectedPage(this);
      parent.Invalidate();
    }
    parent.ApplyLayout();
  }

  protected override void OnPaintBackground(PaintEventArgs pevent)
  {
    if (this.Parent is PageControl && ((PageControl) this.Parent).Renderer.ShouldDrawTabControlBackground)
      ((PageControl) this.Parent).Renderer.DrawTabControlBackground(pevent.Graphics, this.ClientRectangle, this.BackColor, true);
    else
      base.OnPaintBackground(pevent);
  }

  protected override void OnPaint(PaintEventArgs e)
  {
    base.OnPaint(e);
    DockControl.PaintBorder((Control) this, e.Graphics, this._borderStyle);
  }

  private PageControl GetContainer() => this.Parent as PageControl;

  [Browsable(false)]
  [EditorBrowsable(EditorBrowsableState.Never)]
  public override AnchorStyles Anchor
  {
    get => base.Anchor;
    set => base.Anchor = value;
  }

  public override Color BackColor
  {
    get => base.BackColor;
    set
    {
      base.BackColor = value;
      this.UpdateParent();
    }
  }

  [Browsable(false)]
  [EditorBrowsable(EditorBrowsableState.Never)]
  public override DockStyle Dock
  {
    get => base.Dock;
    set => base.Dock = value;
  }

  [Browsable(false)]
  [EditorBrowsable(EditorBrowsableState.Never)]
  public new bool Enabled
  {
    get => base.Enabled;
    set => base.Enabled = value;
  }

  [DefaultValue(0)]
  [Description("Indicates the maximum width of the tab.")]
  [Category("Layout")]
  public int MaximumTabWidth
  {
    get => this._maximumTabWidth;
    set
    {
      this._maximumTabWidth = value >= 0 ? value : throw new ArgumentException("Value must be greater than or equal to zero.");
      this.UpdateParent();
    }
  }

  [Browsable(false)]
  public Rectangle TabBounds => this._tabBounds;

  [Description("The image displayed next to the text on the tab.")]
  [AmbientValue(typeof (Image), null)]
  [Category("Appearance")]
  [DefaultValue(typeof (Image), null)]
  public Image TabImage
  {
    get
    {
      if (this._imageIndex == -1)
        return this._image;
      if (this._workingImage == null)
      {
        ImageList imageList = this.ImageList;
        if (imageList != null && this._imageIndex >= 0 && this._imageIndex < imageList.Images.Count)
          this._workingImage = imageList.Images[this._imageIndex];
      }
      return this._workingImage;
    }
    set
    {
      if (value != null && (value.Width != 16 /*0x10*/ || value.Height != 16 /*0x10*/))
        throw new ArgumentException("Image must be 16x16 pixels.");
      this._image = value;
      this._imageIndex = -1;
      this.ClearWorkingImage();
      this.UpdateParent();
    }
  }

  private bool ShouldSerializeTabImage() => this._image != null;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual ImageList ImageList
  {
    get => this.Parent is PageControl parent ? parent.ImageList : (ImageList) null;
  }

  [DefaultValue(-1)]
  [Category("Image")]
  [TypeConverter(typeof (ImageIndexConverter))]
  [Description("Gets or sets the index value of the image assigned to the control.")]
  [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design", typeof (UITypeEditor))]
  public int TabImageIndex
  {
    get => this._imageIndex;
    set
    {
      if (this._imageIndex == value)
        return;
      this._imageIndex = value;
      this.ClearWorkingImage();
      this.UpdateParent();
    }
  }

  [Browsable(false)]
  [EditorBrowsable(EditorBrowsableState.Never)]
  public new int TabIndex
  {
    get => base.TabIndex;
    set => base.TabIndex = value;
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  [Browsable(false)]
  public new bool TabStop
  {
    get => base.TabStop;
    set => base.TabStop = value;
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Obsolete]
  [Browsable(false)]
  public string TabText
  {
    get => "";
    set
    {
    }
  }

  [Description("The type of border to be drawn around the control.")]
  [Category("Appearance")]
  [DefaultValue(typeof (Intermech.Docking.Rendering.BorderStyle), "None")]
  public Intermech.Docking.Rendering.BorderStyle BorderStyle
  {
    get => this._borderStyle;
    set
    {
      this._borderStyle = value;
      this.UpdateParent();
    }
  }

  public override Rectangle DisplayRectangle
  {
    get
    {
      Rectangle displayRectangle = base.DisplayRectangle;
      switch (this._borderStyle)
      {
        case Intermech.Docking.Rendering.BorderStyle.Flat:
        case Intermech.Docking.Rendering.BorderStyle.RaisedThin:
        case Intermech.Docking.Rendering.BorderStyle.SunkenThin:
          displayRectangle.Inflate(-1, -1);
          return displayRectangle;
        case Intermech.Docking.Rendering.BorderStyle.RaisedThick:
        case Intermech.Docking.Rendering.BorderStyle.SunkenThick:
          displayRectangle.Inflate(-2, -2);
          return displayRectangle;
        default:
          return displayRectangle;
      }
    }
  }

  [Description("Specifies whether the tab object appears in its TabControl.")]
  [Category("Appearance")]
  [DefaultValue(true)]
  public bool TabVisible
  {
    get => this._tabVisible;
    set
    {
      this._tabVisible = value;
      this.UpdateParent();
    }
  }

  [Browsable(true)]
  public override string Text
  {
    get => base.Text;
    set
    {
      base.Text = value;
      this.UpdateParent();
    }
  }

  [Browsable(false)]
  [EditorBrowsable(EditorBrowsableState.Never)]
  public new bool Visible
  {
    get => base.Visible;
    set => base.Visible = value;
  }

  [Description("Determines order in parent's list.")]
  [Category("Appearance")]
  public int Index
  {
    get
    {
      PageControl container = this.GetContainer();
      return container != null ? container.Controls.IndexOf((Control) this) : -1;
    }
    set
    {
      if (value <= -1)
        return;
      Control container = (Control) this.GetContainer();
      if (container == null)
        return;
      if (value > container.Controls.Count)
        value = container.Controls.Count;
      try
      {
        container.SuspendLayout();
        container.Controls.SetChildIndex((Control) this, value);
      }
      finally
      {
        container.ResumeLayout();
        this.UpdateParent();
      }
    }
  }
}
