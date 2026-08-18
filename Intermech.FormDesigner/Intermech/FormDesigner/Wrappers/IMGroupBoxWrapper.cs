// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.IMGroupBoxWrapper
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

/// <summary>Дескриптор для контрола "Группа".</summary>
internal class IMGroupBoxWrapper : IImageFromLibrary
{
  private IMGroupBox _grb = new IMGroupBox();
  private PropertyDescriptorCollection _pdc = new PropertyDescriptorCollection((PropertyDescriptor[]) null);

  /// <summary>Цвет фона элемента управления.</summary>
  [CustomDisplayName("Attribute.FormDesigner_37")]
  [CustomCategory("Attribute.FormDesigner_8")]
  [CustomDescription("Attribute.FormDesigner.BackColor.Description")]
  public Color BackColor
  {
    get => this._grb.BackColor;
    set => this.SetValue(this._pdc[nameof (BackColor)], (object) value);
  }

  /// <summary>
  /// Фоновое изображение, выводимое на элементе управления.
  /// </summary>
  [DefaultValue(null)]
  [CustomDisplayName("Attribute.FormDesigner_183")]
  [CustomCategory("Attribute.FormDesigner_8")]
  [CustomDescription("Attribute.FormDesigner.BackgroundImage.Description")]
  [TypeConverter(typeof (ImageConverter))]
  [RefreshProperties(RefreshProperties.All)]
  public Image BackgroundImage
  {
    get => this._grb.BackgroundImage;
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
    get => this._grb.BackgroundImageLayout;
    set => this.SetValue(this._pdc[nameof (BackgroundImageLayout)], (object) value);
  }

  /// <summary>Шрифт текста, отображаемый элементом управления.</summary>
  [CustomDisplayName("Attribute.FormDesigner_111")]
  [CustomCategory("Attribute.FormDesigner_8")]
  [CustomDescription("Attribute.FormDesigner.Font.Description")]
  [TypeConverter(typeof (FontConverter))]
  [Editor(typeof (FontEditor), typeof (UITypeEditor))]
  public Font Font
  {
    get => this._grb.Font;
    set => this.SetValue(this._pdc[nameof (Font)], (object) value);
  }

  /// <summary>
  /// Основной цвет элемента управления, который используется для отображаемого текста.
  /// </summary>
  [CustomDisplayName("Attribute.FormDesigner_113")]
  [CustomCategory("Attribute.FormDesigner_8")]
  [CustomDescription("Attribute.FormDesigner.ForeColor.Description")]
  public Color ForeColor
  {
    get => this._grb.ForeColor;
    set => this.SetValue(this._pdc[nameof (ForeColor)], (object) value);
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
    get => this._grb.ImageFromLibrary;
    set => this.SetValue(this._pdc[nameof (ImageFromLibrary)], (object) value);
  }

  /// <summary>ID объекта "библиотечное изображение".</summary>
  [Browsable(false)]
  public long ImageFromLibraryID => this._grb.ImageFromLibraryID;

  /// <summary>Наименование изображения.</summary>
  [Browsable(false)]
  public string ImageFromLibraryName => this._grb.ImageFromLibraryName;

  /// <summary>Текст, связанный с элементом управления.</summary>
  [DefaultValue("")]
  [CustomDisplayName("Attribute.FormDesigner.Text")]
  [CustomCategory("Attribute.FormDesigner_8")]
  [CustomDescription("Attribute.FormDesigner.Text.Description")]
  public string Text
  {
    get => this._grb.Text;
    set => this.SetValue(this._pdc[nameof (Text)], (object) value);
  }

  /// <summary>
  /// Последовательность перехода элемента управления внутри контейнера.
  /// </summary>
  [DefaultValue(-1)]
  [CustomDisplayName("Attribute.FormDesigner_109")]
  [CustomCategory("Attribute.FormDesigner_5")]
  [CustomDescription("Attribute.FormDesigner.TabIndex.Description")]
  public int TabIndex
  {
    get => this._grb.TabIndex;
    set => this.SetValue(this._pdc[nameof (TabIndex)], (object) value);
  }

  /// <summary>Поле для заметок.</summary>
  [DefaultValue("")]
  [CustomDisplayName("FormDesigner_PropName_Tag")]
  [CustomCategory("Attribute.FormDesigner_11")]
  [CustomDescription("FormDesigner_PropDescr_Tag")]
  public string Tag
  {
    get => Convert.ToString(this._grb.Tag);
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
    get => this._grb.Name;
    set
    {
      this._grb.Name = value;
      if (this._grb.Site == null)
        return;
      this._grb.Site.Name = value;
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
    get => this._grb.Anchor;
    set => this.SetValue(this._pdc[nameof (Anchor)], (object) value);
  }

  /// <summary>Авторазмер элемента управления.</summary>
  [DefaultValue(false)]
  [CustomDisplayName("Attribute.FormDesigner_23")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.AutoSize.Description")]
  [TypeConverter(typeof (BooleanConverter))]
  [Editor(typeof (BooleanEditor), typeof (UITypeEditor))]
  public bool AutoSize
  {
    get => this._grb.AutoSize;
    set => this.SetValue(this._pdc[nameof (AutoSize)], (object) value);
  }

  /// <summary>Режим установки автоматического размера группы.</summary>
  [CustomDisplayName("Attribute.FormDesigner.AutoSizeMode.Name")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.AutoSizeMode.Description")]
  [TypeConverter(typeof (AutoSizeModeConverter))]
  [Editor(typeof (AutoSizeModeEditor), typeof (UITypeEditor))]
  [RefreshProperties(RefreshProperties.All)]
  public AutoSizeMode AutoSizeMode
  {
    get => this._grb.AutoSizeMode;
    set => this.SetValue(this._pdc[nameof (AutoSizeMode)], (object) value);
  }

  /// <summary>Область размещения элемент управления.</summary>
  [DefaultValue(DockStyle.None)]
  [CustomDisplayName("Attribute.FormDesigner_98")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.Dock.Description")]
  [TypeConverter(typeof (DockStyleConverter))]
  public DockStyle Dock
  {
    get => this._grb.Dock;
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
    get => this._grb.Location;
    set => this.SetValue(this._pdc[nameof (Location)], (object) value);
  }

  /// <summary>Расстояние между элементами управления.</summary>
  [CustomDisplayName("Attribute.FormDesigner_121")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.Margin.Description")]
  [TypeConverter(typeof (MarginPaddingConverter))]
  public Padding Margin
  {
    get => this._grb.Margin;
    set => this.SetValue(this._pdc[nameof (Margin)], (object) value);
  }

  /// <summary>Максимальный размер элемента управления.</summary>
  [CustomDisplayName("Attribute.FormDesigner_119")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.MaximumSize.Description")]
  [TypeConverter(typeof (SizeConverter))]
  public Size MaximumSize
  {
    get => this._grb.MaximumSize;
    set => this.SetValue(this._pdc[nameof (MaximumSize)], (object) value);
  }

  /// <summary>Минимальный размер элемента управления.</summary>
  [CustomDisplayName("Attribute.FormDesigner_117")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.MinimumSize.Description")]
  [TypeConverter(typeof (SizeConverter))]
  public Size MinimumSize
  {
    get => this._grb.MinimumSize;
    set => this.SetValue(this._pdc[nameof (MinimumSize)], (object) value);
  }

  /// <summary>Размеры элемента управления.</summary>
  [CustomDisplayName("Attribute.FormDesigner_75")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.Size.Description")]
  [TypeConverter(typeof (SizeConverter))]
  public Size Size
  {
    get => this._grb.Size;
    set => this.SetValue(this._pdc[nameof (Size)], (object) value);
  }

  /// <summary>Конструктор.</summary>
  /// <param name="parent">Контрол</param>
  public IMGroupBoxWrapper(IMGroupBox parent)
  {
    this._grb = parent;
    this._pdc = TypeDescriptor.GetProperties((object) this._grb, true);
  }

  /// <summary>Метод установки значения через дескриптор.</summary>
  /// <param name="prop">Дескриптор свойства</param>
  /// <param name="value">Новое значение</param>
  private void SetValue(PropertyDescriptor prop, object value)
  {
    if (prop == null)
      throw new Exception(LocalizationHolder.rm.GetString(sc_7214.ssp_imclient_7215()));
    prop.SetValue((object) this._grb, value);
  }
}
