// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.FormWrapper
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Localization;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

internal class FormWrapper : ContainerControlWrapper
{
  private Form _parent = new Form();

  public FormWrapper()
  {
  }

  public FormWrapper(Form parent)
    : base((ContainerControl) parent)
  {
    this._parent = parent;
  }

  /// <summary>
  /// Возвращает или задает фоновое изображение, выводимое на элементе управления.
  /// </summary>
  [CustomDisplayName("Attribute.FormDesigner_183")]
  [CustomCategory("Attribute.FormDesigner_8")]
  [CustomDescription("Attribute.FormDesigner.BackgroundImage.Description")]
  [TypeConverter(typeof (ImageConverter))]
  [RefreshProperties(RefreshProperties.All)]
  [DefaultValue(null)]
  public Image BackgroundImage
  {
    get => this._parent.BackgroundImage;
    set => this.SetValue(this._pdc[nameof (BackgroundImage)], (object) value);
  }

  /// <summary>
  /// Возвращает или задает способ размещения фонового изображения.
  /// </summary>
  [CustomDisplayName("Attribute.FormDesigner.BackgroundImageLayout.Name")]
  [CustomCategory("Attribute.FormDesigner_8")]
  [CustomDescription("Attribute.FormDesigner.BackgroundImageLayout.Description")]
  [TypeConverter(typeof (BackgroundImageLayoutConverter))]
  [Editor(typeof (BackgroundImageLayoutEditor), typeof (UITypeEditor))]
  [DefaultValue(ImageLayout.Tile)]
  public ImageLayout BackgroundImageLayout
  {
    get => this._parent.BackgroundImageLayout;
    set => this.SetValue(this._pdc[nameof (BackgroundImageLayout)], (object) value);
  }

  [CustomDisplayName("Attribute.FormDesigner_136")]
  [CustomCategory("Attribute.FormDesigner_8")]
  [Browsable(true)]
  public Cursor Cursor
  {
    get => this._parent.Cursor;
    set => this.SetValue(this._pdc[nameof (Cursor)], (object) value);
  }

  /// <summary>
  /// Возвращает или задает фоновое изображение, выводимое на элементе управления.
  /// </summary>
  [CustomDisplayName("Attribute.FormDesigner_7")]
  [CustomCategory("Attribute.FormDesigner_8")]
  [CustomDescription("Attribute.FormDesigner.BackgroundImage.Description")]
  [TypeConverter(typeof (ImageConverter))]
  [Editor(typeof (ImagesLibraryEditor), typeof (UITypeEditor))]
  [RefreshProperties(RefreshProperties.All)]
  [DefaultValue(null)]
  public Image Image2
  {
    get => this._parent.BackgroundImage;
    set => this.SetValue(this._pdc["BackgroundImage"], (object) value);
  }

  [Browsable(false)]
  public override string Text
  {
    get => base.Text;
    set => base.Text = string.Empty;
  }

  [Browsable(false)]
  public override int TabIndex
  {
    get => base.TabIndex;
    set => base.TabIndex = value;
  }

  [Browsable(false)]
  public override bool TabStop
  {
    get => base.TabStop;
    set => base.TabStop = value;
  }

  [Browsable(false)]
  public override AnchorStyles Anchor
  {
    get => base.Anchor;
    set => base.Anchor = value;
  }

  [Browsable(false)]
  public override bool AutoSize
  {
    get => base.AutoSize;
    set => base.AutoSize = value;
  }

  [Browsable(false)]
  public override ScrollableControl.DockPaddingEdges DockPadding => this._parent.DockPadding;

  [Browsable(false)]
  public override Point Location
  {
    get => base.Location;
    set => base.Location = value;
  }

  [Browsable(false)]
  public override Padding Margin
  {
    get => base.Margin;
    set => base.Margin = value;
  }
}
