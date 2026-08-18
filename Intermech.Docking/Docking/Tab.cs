
// Type: Intermech.Docking.Tab
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.Docking;

[ToolboxItem(false)]
public class Tab : Component
{
  internal bool _textTrimmed;
  internal bool _visible;
  internal bool _enabled;
  private string _text;
  internal double _tabWidth;
  private Image _image;
  private Image _workImage;
  private int _imageIndex;
  internal Rectangle _tabBounds;
  private int _maximumTabWidth;
  internal TabControl _parent;
  private Intermech.Docking.Rendering.BorderStyle _borderStyle;

  public Tab()
  {
    this._maximumTabWidth = 0;
    this._textTrimmed = false;
    this._visible = true;
    this._enabled = true;
    this._imageIndex = -1;
  }

  public Tab(string text)
    : this()
  {
    this.Text = text;
  }

  protected void UpdateParent()
  {
    if (this._parent == null)
      return;
    this._parent.ApplyLayout();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this._parent != null)
    {
      this._parent.Tabs.Remove(this);
      this._parent = (TabControl) null;
    }
    base.Dispose(disposing);
  }

  [DefaultValue(true)]
  public bool Enabled
  {
    get => this._enabled;
    set => this._enabled = value;
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
  [DefaultValue(null)]
  public Image TabImage
  {
    get
    {
      if (this._imageIndex == -1)
        return this._image;
      if (this._workImage == null)
      {
        ImageList imageList = this.ImageList;
        if (imageList != null && this._imageIndex >= 0 && this._imageIndex < imageList.Images.Count)
          this._workImage = imageList.Images[this._imageIndex];
      }
      return this._workImage;
    }
    set
    {
      if (value != null && (value.Width != 16 /*0x10*/ || value.Height != 16 /*0x10*/))
        throw new ArgumentException("Image must be 16x16 pixels.");
      this._image = value;
      this._workImage = (Image) null;
      this._imageIndex = -1;
      this.UpdateParent();
    }
  }

  private bool ShouldSerializeTabImage() => this._image != null;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual ImageList ImageList
  {
    get => this._parent != null ? this._parent.ImageList : (ImageList) null;
  }

  [DefaultValue(-1)]
  [Category("Image")]
  [TypeConverter(typeof (ImageIndexConverter))]
  [Description("Gets or sets the index value of the image assigned to the control.")]
  [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design", typeof (UITypeEditor))]
  public int ImageIndex
  {
    get => this._imageIndex;
    set
    {
      if (this._imageIndex == value)
        return;
      this._imageIndex = value;
      this._workImage = (Image) null;
      this.UpdateParent();
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

  [Description("Specifies whether the tab object appears in its TabControl.")]
  [Category("Appearance")]
  [DefaultValue(true)]
  public bool Visible
  {
    get => this._visible;
    set
    {
      this._visible = value;
      this.UpdateParent();
    }
  }

  [Localizable(true)]
  public string Text
  {
    get => this._text;
    set
    {
      if (!(this._text != value))
        return;
      this._text = value;
      this.UpdateParent();
    }
  }

  [Description("Determines order in parent's list.")]
  [Category("Appearance")]
  public int Index
  {
    get => this._parent != null ? this._parent.Tabs.IndexOf(this) : -1;
    set
    {
      if (this._parent == null)
        return;
      this._parent.Tabs.MoveTo(this, value);
    }
  }
}
