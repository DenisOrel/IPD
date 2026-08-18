// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.PictureBoxWrapper
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using ImSSP;
using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>
/// 
/// </summary>
internal class PictureBoxWrapper : IWrapper, IImageFromLibrary
{
  private IMPictureBox _pictBox = new IMPictureBox();
  private PropertyDescriptorCollection _pdc = new PropertyDescriptorCollection((PropertyDescriptor[]) null);

  /// <summary>Цвет фона элемента управления.</summary>
  [CustomDisplayName("Attribute.FormDesigner_37")]
  [CustomCategory("Attribute.FormDesigner_8")]
  [CustomDescription("Attribute.FormDesigner.BackColor.Description")]
  public Color BackColor
  {
    get => this._pictBox.BackColor;
    set => this.SetValue(this._pdc[nameof (BackColor)], (object) value);
  }

  /// <summary>
  /// Фоновое изображение, отображаемое в элементе управления.
  /// </summary>
  [DefaultValue(null)]
  [CustomDisplayName("Attribute.FormDesigner_183")]
  [CustomCategory("Attribute.FormDesigner_8")]
  [CustomDescription("Attribute.FormDesigner.BackgroundImage.Description")]
  [TypeConverter(typeof (ImageConverter))]
  [RefreshProperties(RefreshProperties.All)]
  public Image BackgroundImage
  {
    get => this._pictBox.BackgroundImage;
    set => this.SetValue(this._pdc[nameof (BackgroundImage)], (object) value);
  }

  /// <summary>Способ размещения фонового изображения.</summary>
  [DefaultValue(ImageLayout.Tile)]
  [CustomDisplayName("Attribute.FormDesigner.BackgroundImageLayout.Name")]
  [CustomCategory("Attribute.FormDesigner_8")]
  [CustomDescription("Attribute.FormDesigner.BackgroundImageLayout.Description")]
  [TypeConverter(typeof (BackgroundImageLayoutConverter))]
  [Editor(typeof (BackgroundImageLayoutEditor), typeof (UITypeEditor))]
  public ImageLayout BackgroundImageLayout
  {
    get => this._pictBox.BackgroundImageLayout;
    set => this.SetValue(this._pdc[nameof (BackgroundImageLayout)], (object) value);
  }

  /// <summary>Вид обрамления для элемента управления.</summary>
  [DefaultValue(BorderStyle.None)]
  [CustomDisplayName("Attribute.FormDesigner_134")]
  [CustomCategory("Attribute.FormDesigner_8")]
  [CustomDescription("Attribute.FormDesigner.BorderStyle.Description")]
  [TypeConverter(typeof (BorderStyleConverter))]
  [Editor(typeof (BorderStyleEditor), typeof (UITypeEditor))]
  public BorderStyle BorderStyle
  {
    get => this._pictBox.BorderStyle;
    set => this.SetValue(this._pdc[nameof (BorderStyle)], (object) value);
  }

  /// <summary>Изображение, отображаемое в элементе управления.</summary>
  [DefaultValue(null)]
  [CustomDisplayName("Attribute.FormDesigner_7")]
  [CustomCategory("Attribute.FormDesigner_8")]
  [Description("Attribute.FormDesigner.ImageFromLibrary.Description")]
  [TypeConverter(typeof (ImageFromLibraryConverter))]
  [Editor(typeof (ImageFromLibraryEditor), typeof (UITypeEditor))]
  [RefreshProperties(RefreshProperties.All)]
  public Guid ImageFromLibrary
  {
    get => this._pictBox.ImageFromLibrary;
    set => this.SetValue(this._pdc[nameof (ImageFromLibrary)], (object) value);
  }

  /// <summary>ID объекта "библиотечное изображение".</summary>
  [Browsable(false)]
  public long ImageFromLibraryID => this._pictBox.ImageFromLibraryID;

  /// <summary>Наименование изображения.</summary>
  [Browsable(false)]
  public string ImageFromLibraryName => this._pictBox.ImageFromLibraryName;

  /// <summary>
  /// Режим выбора изображения : фиксированный либо пользовательский/runtime
  /// </summary>
  [DefaultValue(PictureSelectMode.Fixed)]
  [CustomDisplayName("FormDesigner_PropName_PictureSelectMode")]
  [CustomCategory("Attribute.FormDesigner_11")]
  [CustomDescription("FormDesigner_PropDescr_PictureSelectMode")]
  [TypeConverter(typeof (PictureSelectModeConverter))]
  [Editor(typeof (PictureSelectModeEditor), typeof (UITypeEditor))]
  public PictureSelectMode PictureSelectMode
  {
    get => this._pictBox.PictureSelectMode;
    set
    {
      if (this._pictBox.PictureSelectMode != value)
        this.SetValue(this._pdc["AttributeInfo"], (object) null);
      this.SetValue(this._pdc[nameof (PictureSelectMode)], (object) value);
    }
  }

  /// <summary>Информация об атрибуте.</summary>
  [DefaultValue(null)]
  [CustomDisplayName("Attribute.FormDesigner_14")]
  [CustomCategory("Attribute.FormDesigner_11")]
  [TypeConverter(typeof (AttributeInfo2TypeNamesConverter))]
  [Editor(typeof (AttributeEditor), typeof (UITypeEditor))]
  [FieldTypes(new FieldTypes[] {FieldTypes.ftObjectLink, FieldTypes.ftFile})]
  [MultiValueModes(new MultiValueModes[] {MultiValueModes.SingleValue, MultiValueModes.MultiValues})]
  public AttributeInfo AttributeInfo
  {
    get => this._pictBox.AttributeInfo;
    set => this.SetValue(this._pdc[nameof (AttributeInfo)], (object) value);
  }

  /// <summary>Для чего нужен контрол.</summary>
  [DefaultValue(AttributeDestinationPoint.Default)]
  [CustomDisplayName("Attribute.FormDesigner_18")]
  [CustomCategory("Attribute.FormDesigner_11")]
  [TypeConverter(typeof (ParentPointConverter))]
  [Editor(typeof (ParentPointEditor), typeof (UITypeEditor))]
  public AttributeDestinationPoint ParentPoint
  {
    get => this._pictBox.ParentPoint;
    set => this.SetValue(this._pdc[nameof (ParentPoint)], (object) value);
  }

  /// <summary>Поле для заметок.</summary>
  [DefaultValue("")]
  [CustomDisplayName("FormDesigner_PropName_Tag")]
  [CustomCategory("Attribute.FormDesigner_11")]
  [CustomDescription("FormDesigner_PropDescr_Tag")]
  public string Tag
  {
    get => Convert.ToString(this._pictBox.Tag);
    set
    {
      this.SetValue(this._pdc[nameof (Tag)], value != string.Empty ? (object) value : (object) (string) null);
    }
  }

  /// <summary>Наименование элемента управления.</summary>
  [CustomDisplayName("Attribute.FormDesigner_102")]
  [CustomCategory("Attribute.FormDesigner_103")]
  [CustomDescription("Attribute.FormDesigner.Name.Description")]
  public string Name
  {
    get => this._pictBox.Name;
    set
    {
      this._pictBox.Name = value;
      if (this._pictBox.Site == null)
        return;
      this._pictBox.Site.Name = value;
    }
  }

  /// <summary>Привязка элемента управления к краям контейнера.</summary>
  [DefaultValue(AnchorStyles.Top | AnchorStyles.Left)]
  [CustomDisplayName("Attribute.FormDesigner_100")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.Anchor.Description")]
  [TypeConverter(typeof (AnchorStylesConverter))]
  public AnchorStyles Anchor
  {
    get => this._pictBox.Anchor;
    set => this.SetValue(this._pdc[nameof (Anchor)], (object) value);
  }

  /// <summary>Область размещения элемент управления.</summary>
  [DefaultValue(DockStyle.None)]
  [CustomDisplayName("Attribute.FormDesigner_98")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.Dock.Description")]
  [TypeConverter(typeof (DockStyleConverter))]
  public DockStyle Dock
  {
    get => this._pictBox.Dock;
    set => this.SetValue(this._pdc[nameof (Dock)], (object) value);
  }

  /// <summary>
  /// Координаты левого верхнего угла элемента управления относительно левого верхнего угла контейнера.
  /// </summary>
  [CustomDisplayName("Attribute.FormDesigner_95")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.Location.Description")]
  [TypeConverter(typeof (PointConverter))]
  public Point Location
  {
    get => this._pictBox.Location;
    set => this.SetValue(this._pdc[nameof (Location)], (object) value);
  }

  /// <summary>Расстояние между элементами управления.</summary>
  [CustomDisplayName("Attribute.FormDesigner_121")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.Margin.Description")]
  [TypeConverter(typeof (MarginPaddingConverter))]
  public Padding Margin
  {
    get => this._pictBox.Margin;
    set => this.SetValue(this._pdc[nameof (Margin)], (object) value);
  }

  /// <summary>Максимальный размер элемента управления.</summary>
  [CustomDisplayName("Attribute.FormDesigner_119")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.MaximumSize.Description")]
  [TypeConverter(typeof (SizeConverter))]
  public Size MaximumSize
  {
    get => this._pictBox.MaximumSize;
    set => this.SetValue(this._pdc[nameof (MaximumSize)], (object) value);
  }

  /// <summary>Минимальный размер элемента управления.</summary>
  [CustomDisplayName("Attribute.FormDesigner_117")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.MinimumSize.Description")]
  [TypeConverter(typeof (SizeConverter))]
  public Size MinimumSize
  {
    get => this._pictBox.MinimumSize;
    set => this.SetValue(this._pdc[nameof (MinimumSize)], (object) value);
  }

  /// <summary>Размеры элемента управления.</summary>
  [CustomDisplayName("Attribute.FormDesigner_75")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.Size.Description")]
  [TypeConverter(typeof (SizeConverter))]
  public Size Size
  {
    get => this._pictBox.Size;
    set => this.SetValue(this._pdc[nameof (Size)], (object) value);
  }

  /// <summary>Конструктор.</summary>
  /// <param name="parent">IMPictureBox контрол</param>
  public PictureBoxWrapper(IMPictureBox parent)
  {
    this._pictBox = parent;
    this._pdc = TypeDescriptor.GetProperties((object) this._pictBox, true);
  }

  /// <summary>Метод установки значения через дескриптор.</summary>
  /// <param name="prop">Дескриптор свойства</param>
  /// <param name="value">Новое значение</param>
  private void SetValue(PropertyDescriptor prop, object value)
  {
    if (prop == null)
      throw new Exception(LocalizationHolder.rm.GetString(sc_7224.ssp_imclient_7225()));
    prop.SetValue((object) this._pictBox, value);
  }

  /// <summary>
  /// 
  /// </summary>
  [Browsable(false)]
  public object BaseClass => (object) this._pictBox;
}
