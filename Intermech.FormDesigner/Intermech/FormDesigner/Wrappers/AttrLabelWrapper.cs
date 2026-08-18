// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.AttrLabelWrapper
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
internal class AttrLabelWrapper : IImageFromLibrary, IWrapper
{
  private AttrLabel _attrLb = new AttrLabel();
  private PropertyDescriptorCollection _pdc = new PropertyDescriptorCollection((PropertyDescriptor[]) null);

  /// <summary>Цвет фона элемента управления.</summary>
  [CustomDisplayName("Attribute.FormDesigner_37")]
  [CustomCategory("Attribute.FormDesigner_8")]
  [CustomDescription("Attribute.FormDesigner.BackColor.Description")]
  public Color BackColor
  {
    get => this._attrLb.BackColor;
    set => this.SetValue(this._pdc[nameof (BackColor)], (object) value);
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
    get => this._attrLb.BorderStyle;
    set => this.SetValue(this._pdc[nameof (BorderStyle)], (object) value);
  }

  /// <summary>Шрифт текста, отображаемый элементом управления.</summary>
  [CustomDisplayName("Attribute.FormDesigner_111")]
  [CustomCategory("Attribute.FormDesigner_8")]
  [CustomDescription("Attribute.FormDesigner.Font.Description")]
  [TypeConverter(typeof (FontConverter))]
  [Editor(typeof (FontEditor), typeof (UITypeEditor))]
  public Font Font
  {
    get => this._attrLb.Font;
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
    get => this._attrLb.ForeColor;
    set => this.SetValue(this._pdc[nameof (ForeColor)], (object) value);
  }

  /// <summary>Изображение, отображаемое в элементе управления.</summary>
  [DefaultValue(null)]
  [CustomDisplayName("Attribute.FormDesigner_183")]
  [CustomCategory("Attribute.FormDesigner_8")]
  [CustomDescription("Attribute.FormDesigner.Image.Description")]
  [TypeConverter(typeof (ImageConverter))]
  [RefreshProperties(RefreshProperties.All)]
  public Image Image
  {
    get => this._attrLb.Image;
    set => this.SetValue(this._pdc[nameof (Image)], (object) value);
  }

  /// <summary>
  /// Выравнивание изображения, отображаемого в элементе управления.
  /// </summary>
  [DefaultValue(ContentAlignment.MiddleCenter)]
  [CustomDisplayName("Attribute.FormDesigner_32")]
  [CustomCategory("Attribute.FormDesigner_8")]
  [CustomDescription("Attribute.FormDesigner.ImageAlign.Description")]
  [TypeConverter(typeof (ContentAlignmentConverter))]
  public ContentAlignment ImageAlign
  {
    get => this._attrLb.ImageAlign;
    set => this.SetValue(this._pdc[nameof (ImageAlign)], (object) value);
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
    get => this._attrLb.ImageFromLibrary;
    set => this.SetValue(this._pdc[nameof (ImageFromLibrary)], (object) value);
  }

  /// <summary>ID объекта "библиотечное изображение".</summary>
  [Browsable(false)]
  public long ImageFromLibraryID => this._attrLb.ImageFromLibraryID;

  /// <summary>Наименование изображения.</summary>
  [Browsable(false)]
  public string ImageFromLibraryName => this._attrLb.ImageFromLibraryName;

  /// <summary>Выравнивание текста в элементе управления.</summary>
  [DefaultValue(ContentAlignment.TopLeft)]
  [CustomDisplayName("Attribute.FormDesigner_35")]
  [CustomCategory("Attribute.FormDesigner_8")]
  [CustomDescription("Attribute.FormDesigner.TextAlign.Description")]
  [TypeConverter(typeof (ContentAlignmentConverter))]
  public ContentAlignment TextAlign
  {
    get => this._attrLb.TextAlign;
    set => this.SetValue(this._pdc[nameof (TextAlign)], (object) value);
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
    get => this._attrLb.TabIndex;
    set => this.SetValue(this._pdc[nameof (TabIndex)], (object) value);
  }

  /// <summary>Информация об атрибуте.</summary>
  [DefaultValue(null)]
  [CustomDisplayName("Attribute.FormDesigner_14")]
  [CustomCategory("Attribute.FormDesigner_11")]
  [TypeConverter(typeof (AttributeInfo2TypeNamesConverter))]
  [Editor(typeof (AttributeEditor), typeof (UITypeEditor))]
  [FieldTypes(new FieldTypes[] {FieldTypes.ftBoolean, FieldTypes.ftDateTime, FieldTypes.ftDouble, FieldTypes.ftGuid, FieldTypes.ftInteger, FieldTypes.ftMeasured, FieldTypes.ftMemo, FieldTypes.ftObjectLink, FieldTypes.ftObjectLinkByID, FieldTypes.ftString, FieldTypes.ftSystem, FieldTypes.ftAutoInc})]
  [MultiValueModes(new MultiValueModes[] {MultiValueModes.SingleValue, MultiValueModes.SingleValueFromList, MultiValueModes.MultiValues, MultiValueModes.MultiValuesFromList})]
  public AttributeInfo AttributeInfo
  {
    get => this._attrLb.AttributeInfo;
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
    get => this._attrLb.ParentPoint;
    set => this.SetValue(this._pdc[nameof (ParentPoint)], (object) value);
  }

  /// <summary>Поле для заметок.</summary>
  [DefaultValue("")]
  [CustomDisplayName("FormDesigner_PropName_Tag")]
  [CustomCategory("Attribute.FormDesigner_11")]
  [CustomDescription("FormDesigner_PropDescr_Tag")]
  public string Tag
  {
    get => Convert.ToString(this._attrLb.Tag);
    set
    {
      this.SetValue(this._pdc[nameof (Tag)], value != string.Empty ? (object) value : (object) (string) null);
    }
  }

  /// <summary>
  /// Возможность использовать атрибут в экспертной системе.
  /// </summary>
  [DefaultValue(false)]
  [CustomDisplayName("Attribute.FormDesigner_17")]
  [CustomCategory("Attribute.FormDesigner_11")]
  [TypeConverter(typeof (BooleanConverter))]
  [Editor(typeof (BooleanEditor), typeof (UITypeEditor))]
  public bool UseInExpertSystem
  {
    get => this._attrLb.UseInExpertSystem;
    set => this.SetValue(this._pdc[nameof (UseInExpertSystem)], (object) value);
  }

  /// <summary>Наименование элемента управления.</summary>
  [CustomDisplayName("Attribute.FormDesigner_102")]
  [CustomCategory("Attribute.FormDesigner_103")]
  [CustomDescription("Attribute.FormDesigner.Name.Description")]
  public string Name
  {
    get => this._attrLb.Name;
    set
    {
      this._attrLb.Name = value;
      if (this._attrLb.Site == null)
        return;
      this._attrLb.Site.Name = value;
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
    get => this._attrLb.Anchor;
    set => this.SetValue(this._pdc[nameof (Anchor)], (object) value);
  }

  /// <summary>Авторазмер элемента управления.</summary>
  [DefaultValue(false)]
  [CustomDisplayName("Attribute.FormDesigner_23")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.AutoSize.Description")]
  [TypeConverter(typeof (BooleanConverter))]
  [Editor(typeof (BooleanEditor), typeof (UITypeEditor))]
  [RefreshProperties(RefreshProperties.All)]
  public bool AutoSize
  {
    get => this._attrLb.AutoSize;
    set => this.SetValue(this._pdc[nameof (AutoSize)], (object) value);
  }

  /// <summary>Область размещения элемент управления.</summary>
  [DefaultValue(DockStyle.None)]
  [CustomDisplayName("Attribute.FormDesigner_98")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.Dock.Description")]
  [TypeConverter(typeof (DockStyleConverter))]
  public DockStyle Dock
  {
    get => this._attrLb.Dock;
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
    get => this._attrLb.Location;
    set => this.SetValue(this._pdc[nameof (Location)], (object) value);
  }

  /// <summary>Расстояние между элементами управления.</summary>
  [CustomDisplayName("Attribute.FormDesigner_121")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.Margin.Description")]
  [TypeConverter(typeof (MarginPaddingConverter))]
  public Padding Margin
  {
    get => this._attrLb.Margin;
    set => this.SetValue(this._pdc[nameof (Margin)], (object) value);
  }

  /// <summary>Максимальный размер элемента управления.</summary>
  [CustomDisplayName("Attribute.FormDesigner_119")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.MaximumSize.Description")]
  [TypeConverter(typeof (SizeConverter))]
  [RefreshProperties(RefreshProperties.All)]
  public Size MaximumSize
  {
    get => this._attrLb.MaximumSize;
    set => this.SetValue(this._pdc[nameof (MaximumSize)], (object) value);
  }

  /// <summary>Минимальный размер элемента управления.</summary>
  [CustomDisplayName("Attribute.FormDesigner_117")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.MinimumSize.Description")]
  [TypeConverter(typeof (SizeConverter))]
  [RefreshProperties(RefreshProperties.All)]
  public Size MinimumSize
  {
    get => this._attrLb.MinimumSize;
    set => this.SetValue(this._pdc[nameof (MinimumSize)], (object) value);
  }

  /// <summary>Отступы от краев в элементе управления.</summary>
  [CustomDisplayName("Attribute.FormDesigner_50")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.Margin.Description")]
  [TypeConverter(typeof (MarginPaddingConverter))]
  [RefreshProperties(RefreshProperties.All)]
  public Padding Padding
  {
    get => this._attrLb.Padding;
    set => this.SetValue(this._pdc[nameof (Padding)], (object) value);
  }

  /// <summary>Размеры элемента управления.</summary>
  [CustomDisplayName("Attribute.FormDesigner_75")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.Size.Description")]
  [TypeConverter(typeof (SizeConverter))]
  public Size Size
  {
    get => this._attrLb.Size;
    set => this.SetValue(this._pdc[nameof (Size)], (object) value);
  }

  /// <summary>Конструктор.</summary>
  /// <param name="parent">Контрол</param>
  public AttrLabelWrapper(AttrLabel parent)
  {
    this._attrLb = parent;
    this._pdc = TypeDescriptor.GetProperties((object) this._attrLb, true);
  }

  /// <summary>Метод установки значения через дескриптор.</summary>
  /// <param name="prop">Дескриптор свойства</param>
  /// <param name="value">Новое значение</param>
  private void SetValue(PropertyDescriptor prop, object value)
  {
    if (prop == null)
      throw new Exception(LocalizationHolder.rm.GetString(sc_7197.ssp_imclient_7198()));
    prop.SetValue((object) this._attrLb, value);
  }

  /// <summary>
  /// 
  /// </summary>
  [Browsable(false)]
  public object BaseClass => (object) this._attrLb;
}
