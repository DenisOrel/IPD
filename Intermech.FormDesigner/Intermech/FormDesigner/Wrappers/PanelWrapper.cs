// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.PanelWrapper
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

internal class PanelWrapper : ScrollableControlWrapper
{
  private Panel _parent = new Panel();

  public PanelWrapper()
  {
  }

  public PanelWrapper(Panel parent)
    : base((ScrollableControl) parent)
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

  /// <summary>Возвращает или задает стиль границ для закладки.</summary>
  [CustomDisplayName("Attribute.FormDesigner_134")]
  [CustomCategory("Attribute.FormDesigner_8")]
  [CustomDescription("Attribute.FormDesigner.BorderStyle.Description")]
  [TypeConverter(typeof (BorderStyleConverter))]
  [Editor(typeof (BorderStyleEditor), typeof (UITypeEditor))]
  [DefaultValue(BorderStyle.None)]
  public BorderStyle BorderStyle
  {
    get => this._parent.BorderStyle;
    set => this.SetValue(this._pdc[nameof (BorderStyle)], (object) value);
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
    set => base.Text = value;
  }

  [Browsable(false)]
  public override ScrollableControl.DockPaddingEdges DockPadding => base.DockPadding;

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

  /// <summary>
  /// Возвращает или задает режим установки автоматического размера группы.
  /// </summary>
  [CustomDisplayName("Attribute.FormDesigner.AutoSizeMode.Name")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.AutoSizeMode.Description")]
  [TypeConverter(typeof (AutoSizeModeConverter))]
  [Editor(typeof (AutoSizeModeEditor), typeof (UITypeEditor))]
  [RefreshProperties(RefreshProperties.All)]
  public virtual AutoSizeMode AutoSizeMode
  {
    get => this._parent.AutoSizeMode;
    set => this.SetValue(this._pdc[nameof (AutoSizeMode)], (object) value);
  }
}
