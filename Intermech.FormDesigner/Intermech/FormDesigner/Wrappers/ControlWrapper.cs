// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.ControlWrapper
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using ImSSP;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>
/// Класс wrapper для отображения в PropertyGrid на русском языке.
/// </summary>
public class ControlWrapper
{
  private Control _parent = new Control();
  /// <summary>Коллекция дескрипторов</summary>
  protected PropertyDescriptorCollection _pdc = new PropertyDescriptorCollection((PropertyDescriptor[]) null);

  /// <summary>Базовый конструктор</summary>
  public ControlWrapper()
  {
  }

  /// <summary>Конструктор с переданным исходным объеком</summary>
  /// <param name="parent">исходный объект</param>
  public ControlWrapper(Control parent)
  {
    this._parent = parent;
    this._pdc = TypeDescriptor.GetProperties((object) this._parent);
  }

  /// <summary>Возвращает или задает цвет фона элемента управления.</summary>
  [CustomDisplayName("Attribute.FormDesigner_37")]
  [CustomCategory("Attribute.FormDesigner_8")]
  [CustomDescription("Attribute.FormDesigner.BackColor.Description")]
  [DefaultValue(typeof (Color), "Control")]
  public virtual Color BackColor
  {
    get => this._parent.BackColor;
    set => this.SetValue(this._pdc[nameof (BackColor)], (object) value);
  }

  /// <summary>
  /// Возвращает или задает шрифт текста, отображаемого элементом управления.
  /// </summary>
  [CustomDisplayName("Attribute.FormDesigner_111")]
  [CustomCategory("Attribute.FormDesigner_8")]
  [CustomDescription("Attribute.FormDesigner.Font.Description")]
  [TypeConverter(typeof (FontConverter))]
  [Editor(typeof (FontEditor), typeof (UITypeEditor))]
  public virtual Font Font
  {
    get => this._parent.Font;
    set => this.SetValue(this._pdc[nameof (Font)], (object) value);
  }

  /// <summary>
  /// Возвращает или задает основной цвет элемента управления, который используется для отображаемого текста.
  /// </summary>
  [CustomDisplayName("Attribute.FormDesigner_113")]
  [CustomCategory("Attribute.FormDesigner_8")]
  [CustomDescription("Attribute.FormDesigner.ForeColor.Description")]
  [DefaultValue(typeof (Color), "ControlText")]
  public virtual Color ForeColor
  {
    get => this._parent.ForeColor;
    set => this.SetValue(this._pdc[nameof (ForeColor)], (object) value);
  }

  /// <summary>
  /// Получает или задает текст, связанный с этим элементом управления.
  /// </summary>
  [CustomDisplayName("Attribute.FormDesigner.Text")]
  [CustomCategory("Attribute.FormDesigner_8")]
  [CustomDescription("Attribute.FormDesigner.Text.Description")]
  [DefaultValue("")]
  public virtual string Text
  {
    get => this._parent.Text;
    set => this.SetValue(this._pdc[nameof (Text)], (object) value);
  }

  /// <summary>
  /// Возвращает или задает объект, содержащий данные элемента управления.
  /// </summary>
  [DefaultValue("")]
  [CustomDisplayName("FormDesigner_PropName_Tag")]
  [CustomCategory("Attribute.FormDesigner_11")]
  [CustomDescription("FormDesigner_PropDescr_Tag")]
  public virtual string Tag
  {
    get => this._parent.Tag == null ? string.Empty : this._parent.Tag.ToString();
    set
    {
      this.SetValue(this._pdc[nameof (Tag)], value != string.Empty ? (object) value : (object) (string) null);
    }
  }

  /// <summary>
  /// Возвращает или задает последовательность перехода элемента управления внутри контейнера.
  /// </summary>
  [CustomDisplayName("Attribute.FormDesigner_109")]
  [CustomCategory("Attribute.FormDesigner_5")]
  [CustomDescription("Attribute.FormDesigner.TabIndex.Description")]
  public virtual int TabIndex
  {
    get => this._parent.TabIndex;
    set => this.SetValue(this._pdc[nameof (TabIndex)], (object) value);
  }

  /// <summary>
  /// Возвращает или задает значение, показывающее, можно ли передать фокус данному элементу управления при помощи клавиши TAB.
  /// </summary>
  [CustomDisplayName("Attribute.FormDesigner_107")]
  [CustomCategory("Attribute.FormDesigner_5")]
  [CustomDescription("Attribute.FormDesigner.TabStop.Description")]
  [TypeConverter(typeof (BooleanConverter))]
  [Editor(typeof (BooleanEditor), typeof (UITypeEditor))]
  public virtual bool TabStop
  {
    get => this._parent.TabStop;
    set => this.SetValue(this._pdc[nameof (TabStop)], (object) value);
  }

  /// <summary>Возвращает или задает имя элемента управления.</summary>
  [CustomDisplayName("Attribute.FormDesigner_102")]
  [CustomCategory("Attribute.FormDesigner_103")]
  [CustomDescription("Attribute.FormDesigner.Name.Description")]
  public virtual string Name
  {
    get => this._parent.Name;
    set
    {
      if (this._parent.Site != null)
        this._parent.Site.Name = value;
      this._parent.Name = value;
    }
  }

  /// <summary>
  /// Возвращает или задает значение, указывающее, какие края элемента управления будут привязаны к краям контейнера.
  /// </summary>
  [CustomDisplayName("Attribute.FormDesigner_100")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.Anchor.Description")]
  [TypeConverter(typeof (AnchorStylesConverter))]
  public virtual AnchorStyles Anchor
  {
    get => this._parent.Anchor;
    set => this.SetValue(this._pdc[nameof (Anchor)], (object) value);
  }

  /// <summary>
  /// Возвращает или задает значение, показывающее, изменяет ли элемент управления размеры автоматически для отображения всего содержимого.
  /// </summary>
  [CustomDisplayName("Attribute.FormDesigner_23")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.AutoSize.Description")]
  [TypeConverter(typeof (BooleanConverter))]
  [Editor(typeof (BooleanEditor), typeof (UITypeEditor))]
  [RefreshProperties(RefreshProperties.All)]
  public virtual bool AutoSize
  {
    get
    {
      return this._pdc[nameof (AutoSize)] != null && Convert.ToBoolean(this._pdc[nameof (AutoSize)].GetValue((object) this._parent));
    }
    set => this.SetValue(this._pdc[nameof (AutoSize)], (object) value);
  }

  /// <summary>
  /// Возвращает или задает край родительского контейнера, к которому прикрепляется элемент управления.
  /// </summary>
  [CustomDisplayName("Attribute.FormDesigner_98")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.Dock.Description")]
  [TypeConverter(typeof (DockStyleConverter))]
  public virtual DockStyle Dock
  {
    get => this._parent.Dock;
    set => this.SetValue(this._pdc[nameof (Dock)], (object) value);
  }

  /// <summary>
  /// Возвращает или задает координаты левого верхнего угла элемента управления относительно левого верхнего угла контейнера.
  /// </summary>
  [CustomDisplayName("Attribute.FormDesigner_95")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.Location.Description")]
  [TypeConverter(typeof (PointConverter))]
  public virtual Point Location
  {
    get => this._parent.Location;
    set => this.SetValue(this._pdc[nameof (Location)], (object) value);
  }

  /// <summary>
  /// Возвращает или задает расстояние между элементами управления.
  /// </summary>
  [CustomDisplayName("Attribute.FormDesigner_121")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.Margin.Description")]
  [TypeConverter(typeof (MarginPaddingConverter))]
  public virtual Padding Margin
  {
    get => this._parent.Margin;
    set => this.SetValue(this._pdc[nameof (Margin)], (object) value);
  }

  /// <summary>
  /// Возвращает или задает максимальный размер элемента управления.
  /// </summary>
  [CustomDisplayName("Attribute.FormDesigner_119")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.MaximumSize.Description")]
  [TypeConverter(typeof (SizeConverter))]
  public virtual Size MaximumSize
  {
    get => this._parent.MaximumSize;
    set => this.SetValue(this._pdc[nameof (MaximumSize)], (object) value);
  }

  /// <summary>
  /// Возвращает или задает минимальный размер элемента управления.
  /// </summary>
  [CustomDisplayName("Attribute.FormDesigner_117")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.MinimumSize.Description")]
  [TypeConverter(typeof (SizeConverter))]
  public virtual Size MinimumSize
  {
    get => this._parent.MinimumSize;
    set => this.SetValue(this._pdc[nameof (MinimumSize)], (object) value);
  }

  /// <summary>
  /// Возвращает или задает высоту и ширину элемента управления.
  /// </summary>
  [CustomDisplayName("Attribute.FormDesigner_75")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.Size.Description")]
  [TypeConverter(typeof (SizeConverter))]
  public virtual Size Size
  {
    get => this._parent.Size;
    set => this.SetValue(this._pdc[nameof (Size)], (object) value);
  }

  /// <summary>
  /// 
  /// </summary>
  public void Prepare()
  {
    Dictionary<string, lPropertyTemplate> dictionary1 = new Dictionary<string, lPropertyTemplate>();
    Dictionary<string, lPropertyTemplate> dictionary2 = new Dictionary<string, lPropertyTemplate>();
    dictionary1.Add("Location", new lPropertyTemplate("Location", LocalizationHolder.rm.GetString("FormDesigner_206"), new Attribute[3]
    {
      (Attribute) new CustomCategory("Attribute.FormDesigner_51"),
      (Attribute) new CustomDescription("Attribute.FormDesigner.Location.Description"),
      (Attribute) new TypeConverterAttribute(typeof (PointConverter))
    }));
    dictionary2.Add("BackgroundImage", new lPropertyTemplate("BackgroundImage", LocalizationHolder.rm.GetString("FormDesigner_193"), new Attribute[4]
    {
      (Attribute) new CustomCategory("Attribute.FormDesigner.Category.Appearance"),
      (Attribute) new CustomDescription("Attribute.FormDesigner.BackgroundImage.Description"),
      (Attribute) new TypeConverterAttribute(typeof (ImageConverter)),
      (Attribute) new RefreshPropertiesAttribute(RefreshProperties.All)
    }));
    dictionary2.Add("BackgroundImageLayout", new lPropertyTemplate("BackgroundImageLayout", LocalizationHolder.rm.GetString("FormDesigner.BackgroundImageLayout.Name"), new Attribute[6]
    {
      (Attribute) new CustomCategory("Attribute.FormDesigner.Category.Appearance"),
      (Attribute) new CustomDescription("Attribute.FormDesigner.BackgroundImageLayout.Description"),
      (Attribute) new TypeConverterAttribute(typeof (BackgroundImageLayoutConverter)),
      (Attribute) new EditorAttribute(typeof (BackgroundImageLayoutEditor), typeof (UITypeEditor)),
      (Attribute) new DefaultValueAttribute((object) ImageLayout.Tile),
      (Attribute) new RefreshPropertiesAttribute(RefreshProperties.All)
    }));
    dictionary2.Add("Font", new lPropertyTemplate("Font", LocalizationHolder.rm.GetString("FormDesigner_215"), new Attribute[4]
    {
      (Attribute) new CustomCategory("Attribute.FormDesigner.Category.Appearance"),
      (Attribute) new CustomDescription("Attribute.FormDesigner.Font.Description"),
      (Attribute) new TypeConverterAttribute(typeof (FontConverter)),
      (Attribute) new EditorAttribute(typeof (FontEditor), typeof (UITypeEditor))
    }));
    dictionary2.Add("ForeColor", new lPropertyTemplate("ForeColor", LocalizationHolder.rm.GetString("FormDesigner_216"), new Attribute[2]
    {
      (Attribute) new CustomCategory("Attribute.FormDesigner.Category.Appearance"),
      (Attribute) new CustomDescription("Attribute.FormDesigner.ForeColor.Description")
    }));
    dictionary2.Add("Tag", new lPropertyTemplate("Tag", LocalizationHolder.rm.GetString("FormDesigner_211"), new Attribute[3]
    {
      (Attribute) new CustomCategory("Attribute.FormDesigner_11"),
      (Attribute) new CustomDescription("Attribute.FormDesigner.Tag.Description"),
      (Attribute) new DefaultValueAttribute((string) null)
    }));
    dictionary2.Add("TabIndex", new lPropertyTemplate("TabIndex", LocalizationHolder.rm.GetString("FormDesigner_213"), new Attribute[2]
    {
      (Attribute) new CustomCategory("Attribute.FormDesigner_5"),
      (Attribute) new CustomDescription("Attribute.FormDesigner.TabIndex.Description")
    }));
    dictionary2.Add("TabStop", new lPropertyTemplate("TabStop", LocalizationHolder.rm.GetString("FormDesigner_212"), new Attribute[5]
    {
      (Attribute) new CustomCategory("Attribute.FormDesigner_5"),
      (Attribute) new CustomDescription("Attribute.FormDesigner.TabStop.Description"),
      (Attribute) new TypeConverterAttribute(typeof (BooleanConverter)),
      (Attribute) new EditorAttribute(typeof (BooleanEditor), typeof (UITypeEditor)),
      (Attribute) new DefaultValueAttribute(true)
    }));
    dictionary2.Add("Anchor", new lPropertyTemplate("Anchor", LocalizationHolder.rm.GetString("FormDesigner_210"), new Attribute[3]
    {
      (Attribute) new CustomCategory("Attribute.FormDesigner_51"),
      (Attribute) new CustomDescription("Attribute.FormDesigner.Anchor.Description"),
      (Attribute) new TypeConverterAttribute(typeof (AnchorStylesConverter))
    }));
    dictionary2.Add("Dock", new lPropertyTemplate("Dock", LocalizationHolder.rm.GetString("FormDesigner_209"), new Attribute[3]
    {
      (Attribute) new CustomCategory("Attribute.FormDesigner_51"),
      (Attribute) new CustomDescription("Attribute.FormDesigner.Dock.Description"),
      (Attribute) new TypeConverterAttribute(typeof (DockStyleConverter))
    }));
    dictionary2.Add("Margin", new lPropertyTemplate("Margin", LocalizationHolder.rm.GetString("FormDesigner_220"), new Attribute[3]
    {
      (Attribute) new CustomCategory("Attribute.FormDesigner_51"),
      (Attribute) new CustomDescription("Attribute.FormDesigner.Margin.Description"),
      (Attribute) new TypeConverterAttribute(typeof (MarginPaddingConverter))
    }));
    dictionary2.Add("MaximumSize", new lPropertyTemplate("MaximumSize", LocalizationHolder.rm.GetString("FormDesigner_219"), new Attribute[3]
    {
      (Attribute) new CustomCategory("Attribute.FormDesigner_51"),
      (Attribute) new CustomDescription("Attribute.FormDesigner.MaximumSize.Description"),
      (Attribute) new TypeConverterAttribute(typeof (SizeConverter))
    }));
    dictionary2.Add("MinimumSize", new lPropertyTemplate("MinimumSize", LocalizationHolder.rm.GetString("FormDesigner_218"), new Attribute[3]
    {
      (Attribute) new CustomCategory("Attribute.FormDesigner_51"),
      (Attribute) new CustomDescription("Attribute.FormDesigner.MinimumSize.Description"),
      (Attribute) new TypeConverterAttribute(typeof (SizeConverter))
    }));
    dictionary2.Add("Padding", new lPropertyTemplate("Padding", LocalizationHolder.rm.GetString("FormDesigner_221"), new Attribute[3]
    {
      (Attribute) new CustomCategory("Attribute.FormDesigner_51"),
      (Attribute) new CustomDescription("Attribute.FormDesigner.Padding.Description"),
      (Attribute) new TypeConverterAttribute(typeof (MarginPaddingConverter))
    }));
    dictionary2.Add("Size", new lPropertyTemplate("Size", LocalizationHolder.rm.GetString("FormDesigner_208"), new Attribute[3]
    {
      (Attribute) new CustomCategory("Attribute.FormDesigner_51"),
      (Attribute) new CustomDescription("Attribute.FormDesigner.Size.Description"),
      (Attribute) new TypeConverterAttribute(typeof (SizeConverter))
    }));
    lPropertyTranslate.PropertyTranslate[typeof (ControlDesigner)] = dictionary1;
    lPropertyTranslate.PropertyTranslate[typeof (Control)] = dictionary2;
    lPropertyTranslate.PropertyTranslate.AddCommonTemplate(new lPropertyTemplate("Name", LocalizationHolder.rm.GetString("FormDesigner_222"), new Attribute[2]
    {
      (Attribute) new CustomCategory("Attribute.FormDesigner_103"),
      (Attribute) new CustomDescription("Attribute.FormDesigner.Name.Description")
    }));
  }

  /// <summary>Метод установки значения через дескриптор</summary>
  /// <param name="prop">дескриптор свойства</param>
  /// <param name="value">новое значение</param>
  protected void SetValue(PropertyDescriptor prop, object value)
  {
    if (prop == null)
      throw new Exception(LocalizationHolder.rm.GetString(sc_7210.ssp_imclient_7211()));
    prop.SetValue((object) this._parent, value);
  }
}
