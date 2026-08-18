// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.AttrTextBtnCompWrapper
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

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
internal class AttrTextBtnCompWrapper : IWrapper
{
  private AttrTextBtnComp _attrTxtBtnComp = new AttrTextBtnComp();
  private PropertyDescriptorCollection _pdc = new PropertyDescriptorCollection((PropertyDescriptor[]) null);

  /// <summary>Цвет фона элемента управления.</summary>
  [CustomDisplayName("Attribute.FormDesigner_37")]
  [CustomCategory("Attribute.FormDesigner_8")]
  [CustomDescription("Attribute.FormDesigner.BackColor.Description")]
  public Color BackColor
  {
    get => this._attrTxtBtnComp.BackColor;
    set => this.SetValue(this._pdc[nameof (BackColor)], (object) value);
  }

  /// <summary>Вид обрамления для элемента управления.</summary>
  [DefaultValue(BorderStyle.Fixed3D)]
  [CustomDisplayName("Attribute.FormDesigner_134")]
  [CustomCategory("Attribute.FormDesigner_8")]
  [CustomDescription("Attribute.FormDesigner.BorderStyle.Description")]
  [TypeConverter(typeof (BorderStyleConverter))]
  [Editor(typeof (BorderStyleEditor), typeof (UITypeEditor))]
  public BorderStyle BorderStyle
  {
    get => this._attrTxtBtnComp.BorderStyle;
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
    get => this._attrTxtBtnComp.Font;
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
    get => this._attrTxtBtnComp.ForeColor;
    set => this.SetValue(this._pdc[nameof (ForeColor)], (object) value);
  }

  /// <summary>Текстовая подсказка.</summary>
  [DefaultValue("")]
  [CustomDisplayName("Attribute.FormDesigner.ToolTip")]
  [CustomCategory("Attribute.FormDesigner_8")]
  [CustomDescription("Attribute.FormDesigner.ToolTip.Description")]
  public string Hint
  {
    get => this._attrTxtBtnComp.Hint;
    set => this.SetValue(this._pdc[nameof (Hint)], (object) value);
  }

  /// <summary>Текст, связанный с элементом управления.</summary>
  [DefaultValue("")]
  [CustomDisplayName("Attribute.FormDesigner.Text")]
  [CustomCategory("Attribute.FormDesigner_8")]
  [CustomDescription("Attribute.FormDesigner.Text.Description")]
  public string Text
  {
    get => this._attrTxtBtnComp.Text;
    set => this.SetValue(this._pdc[nameof (Text)], (object) value);
  }

  /// <summary>Выравнивание текста в элементе управления.</summary>
  [DefaultValue(HorizontalAlignment.Left)]
  [CustomDisplayName("Attribute.FormDesigner_35")]
  [CustomCategory("Attribute.FormDesigner_8")]
  [CustomDescription("Attribute.FormDesigner.TextAlign.Description")]
  [TypeConverter(typeof (HorizontalAlignmentConverter))]
  [Editor(typeof (HorizontalAlignmentEditor), typeof (UITypeEditor))]
  public HorizontalAlignment TextAlign
  {
    get => this._attrTxtBtnComp.TextAlign;
    set => this.SetValue(this._pdc[nameof (TextAlign)], (object) value);
  }

  /// <summary>Достпность контрола.</summary>
  [DefaultValue(false)]
  [CustomDisplayName("Attribute_FormDesigner_DisabledProp_Name")]
  [CustomCategory("Attribute.FormDesigner_5")]
  [CustomDescription("Attribute_FormDesigner_DisabledProp_Description")]
  [TypeConverter(typeof (YesNoConverter))]
  [Editor(typeof (YesNoEditor), typeof (UITypeEditor))]
  public bool DisabledInDesign
  {
    get => this._attrTxtBtnComp.DisabledInDesign;
    set => this.SetValue(this._pdc[nameof (DisabledInDesign)], (object) value);
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
    get => this._attrTxtBtnComp.TabIndex;
    set => this.SetValue(this._pdc[nameof (TabIndex)], (object) value);
  }

  /// <summary>
  /// Возможность передачи фокуса данному элементу управления при помощи клавиши TAB.
  /// </summary>
  [DefaultValue(true)]
  [CustomDisplayName("Attribute.FormDesigner_107")]
  [CustomCategory("Attribute.FormDesigner_5")]
  [CustomDescription("Attribute.FormDesigner.TabStop.Description")]
  [TypeConverter(typeof (BooleanConverter))]
  [Editor(typeof (BooleanEditor), typeof (UITypeEditor))]
  public bool TabStop
  {
    get => this._attrTxtBtnComp.TabStop;
    set => this.SetValue(this._pdc[nameof (TabStop)], (object) value);
  }

  /// <summary>Информация об атрибуте.</summary>
  [DefaultValue(null)]
  [CustomDisplayName("Attribute.FormDesigner_14")]
  [CustomCategory("Attribute.FormDesigner_11")]
  [TypeConverter(typeof (AttributeInfo2TypeNamesConverter))]
  [Editor(typeof (AttributeEditor), typeof (UITypeEditor))]
  [FieldTypes(new FieldTypes[] {FieldTypes.ftObjectLink})]
  [MultiValueModes(new MultiValueModes[] {MultiValueModes.SingleValue})]
  [RefreshProperties(RefreshProperties.All)]
  public AttributeInfo AttributeInfo
  {
    get => this._attrTxtBtnComp.AttributeInfo;
    set => this.SetValue(this._pdc[nameof (AttributeInfo)], (object) value);
  }

  /// <summary>Возможность добавлять атрибут.</summary>
  [DefaultValue(true)]
  [CustomDisplayName("Attribute.FormDesigner_15")]
  [CustomCategory("Attribute.FormDesigner_11")]
  [CustomDescription("Attribute.FormDesigner_16")]
  [TypeConverter(typeof (BooleanConverter))]
  [Editor(typeof (BooleanEditor), typeof (UITypeEditor))]
  public bool CanAddAttribute
  {
    get => this._attrTxtBtnComp.CanAddAttribute;
    set => this.SetValue(this._pdc[nameof (CanAddAttribute)], (object) value);
  }

  /// <summary>Для чего нужен контрол.</summary>
  [DefaultValue(AttributeDestinationPoint.Default)]
  [CustomDisplayName("Attribute.FormDesigner_18")]
  [CustomCategory("Attribute.FormDesigner_11")]
  [TypeConverter(typeof (ParentPointConverter))]
  [Editor(typeof (ParentPointEditor), typeof (UITypeEditor))]
  public AttributeDestinationPoint ParentPoint
  {
    get => this._attrTxtBtnComp.ParentPoint;
    set => this.SetValue(this._pdc[nameof (ParentPoint)], (object) value);
  }

  /// <summary>Поле для заметок.</summary>
  [DefaultValue("")]
  [CustomDisplayName("FormDesigner_PropName_Tag")]
  [CustomCategory("Attribute.FormDesigner_11")]
  [CustomDescription("FormDesigner_PropDescr_Tag")]
  public string Tag
  {
    get => Convert.ToString(this._attrTxtBtnComp.Tag);
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
    get => this._attrTxtBtnComp.Name;
    set
    {
      this._attrTxtBtnComp.Name = value;
      if (this._attrTxtBtnComp.Site == null)
        return;
      this._attrTxtBtnComp.Site.Name = value;
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
    get => this._attrTxtBtnComp.Anchor;
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
    get => this._attrTxtBtnComp.Dock;
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
    get => this._attrTxtBtnComp.Location;
    set => this.SetValue(this._pdc[nameof (Location)], (object) value);
  }

  /// <summary>Расстояние между элементами управления.</summary>
  [CustomDisplayName("Attribute.FormDesigner_121")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.Margin.Description")]
  [TypeConverter(typeof (MarginPaddingConverter))]
  public Padding Margin
  {
    get => this._attrTxtBtnComp.Margin;
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
    get => this._attrTxtBtnComp.MaximumSize;
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
    get => this._attrTxtBtnComp.MinimumSize;
    set => this.SetValue(this._pdc[nameof (MinimumSize)], (object) value);
  }

  /// <summary>Размеры элемента управления.</summary>
  [CustomDisplayName("Attribute.FormDesigner_75")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.Size.Description")]
  [TypeConverter(typeof (SizeConverter))]
  public Size Size
  {
    get => this._attrTxtBtnComp.Size;
    set => this.SetValue(this._pdc[nameof (Size)], (object) value);
  }

  /// <summary>Конструктор.</summary>
  /// <param name="parent">Контрол</param>
  public AttrTextBtnCompWrapper(AttrTextBtnComp parent)
  {
    this._attrTxtBtnComp = parent;
    this._pdc = TypeDescriptor.GetProperties((object) this._attrTxtBtnComp, true);
  }

  /// <summary>Метод установки значения через дескриптор.</summary>
  /// <param name="prop">Дескриптор свойства</param>
  /// <param name="value">Новое значение</param>
  private void SetValue(PropertyDescriptor prop, object value)
  {
    if (prop == null)
      throw new Exception(LocalizationHolder.rm.GetString("FormDesigner_106"));
    prop.SetValue((object) this._attrTxtBtnComp, value);
  }

  /// <summary>
  /// 
  /// </summary>
  [Browsable(false)]
  public object BaseClass => (object) this._attrTxtBtnComp;
}
