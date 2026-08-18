// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.TabControlWrapper
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
internal class TabControlWrapper
{
  private IMTabControl _tabCtrl = new IMTabControl();
  private PropertyDescriptorCollection _pdc = new PropertyDescriptorCollection((PropertyDescriptor[]) null);

  /// <summary>Шрифт текста, отображаемый элементом управления.</summary>
  [CustomDisplayName("Attribute.FormDesigner_111")]
  [CustomCategory("Attribute.FormDesigner_8")]
  [CustomDescription("Attribute.FormDesigner.Font.Description")]
  [TypeConverter(typeof (FontConverter))]
  [Editor(typeof (FontEditor), typeof (UITypeEditor))]
  public Font Font
  {
    get => this._tabCtrl.Font;
    set => this.SetValue(this._pdc[nameof (Font)], (object) value);
  }

  /// <summary>Возвращает или задает место расположения закладок.</summary>
  [DefaultValue(TabAlignment.Top)]
  [CustomDisplayName("Attribute.FormDesigner_25")]
  [CustomCategory("Attribute.FormDesigner_5")]
  [CustomDescription("Attribute.FormDesigner.TabAlignment.Description")]
  [TypeConverter(typeof (TabAlignmentConverter))]
  [Editor(typeof (TabAlignmentEditor), typeof (UITypeEditor))]
  public TabAlignment Alignment
  {
    get => this._tabCtrl.Alignment;
    set => this.SetValue(this._pdc[nameof (Alignment)], (object) value);
  }

  /// <summary>Возвращает или задает внешний вид закладок.</summary>
  [DefaultValue(TabAppearance.Normal)]
  [CustomDisplayName("Attribute.FormDesigner_8")]
  [CustomCategory("Attribute.FormDesigner_5")]
  [CustomDescription("Attribute.FormDesigner.TabAppearance.Description")]
  [TypeConverter(typeof (TabAppearanceConverter))]
  [Editor(typeof (TabAppearanceEditor), typeof (UITypeEditor))]
  public TabAppearance Appearance
  {
    get => this._tabCtrl.Appearance;
    set => this.SetValue(this._pdc[nameof (Appearance)], (object) value);
  }

  /// <summary>
  /// Возвращает или задает возможность быстрого переключения между закладками.
  /// </summary>
  [DefaultValue(false)]
  [CustomDisplayName("Attribute.FormDesigner_206")]
  [CustomCategory("Attribute.FormDesigner_5")]
  [CustomDescription("Attribute.FormDesigner.HotTrack.Description")]
  [TypeConverter(typeof (BooleanConverter))]
  [Editor(typeof (BooleanEditor), typeof (UITypeEditor))]
  public bool HotTrack
  {
    get => this._tabCtrl.HotTrack;
    set => this.SetValue(this._pdc[nameof (HotTrack)], (object) value);
  }

  /// <summary>Возвращает или задает размер закладок.</summary>
  [CustomDisplayName("Attribute.FormDesigner_208")]
  [CustomCategory("Attribute.FormDesigner_5")]
  [CustomDescription("Attribute.FormDesigner.ItemSize.Description")]
  [TypeConverter(typeof (SizeConverter))]
  public Size ItemSize
  {
    get => this._tabCtrl.ItemSize;
    set => this.SetValue(this._pdc[nameof (ItemSize)], (object) value);
  }

  /// <summary>
  /// Возвращает или задает значение, которое показывает можно ли располагать закладки в несколько строк.
  /// </summary>
  [DefaultValue(false)]
  [CustomDisplayName("Attribute.FormDesigner_210")]
  [CustomCategory("Attribute.FormDesigner_5")]
  [CustomDescription("Attribute.FormDesigner.TabMultiline.Description")]
  [TypeConverter(typeof (BooleanConverter))]
  [Editor(typeof (BooleanEditor), typeof (UITypeEditor))]
  public bool Multiline
  {
    get => this._tabCtrl.Multiline;
    set => this.SetValue(this._pdc[nameof (Multiline)], (object) value);
  }

  /// <summary>
  /// Возвращает или задает величину отступа вокруг текста на закладке элемента управления.
  /// </summary>
  [CustomDisplayName("Attribute.FormDesigner_50")]
  [CustomCategory("Attribute.FormDesigner_5")]
  [CustomDescription("Attribute.FormDesigner.Padding.Description")]
  [TypeConverter(typeof (PointConverter))]
  public Point Padding
  {
    get => this._tabCtrl.Padding;
    set => this.SetValue(this._pdc[nameof (Padding)], (object) value);
  }

  /// <summary>
  /// Возвращает или задает значение, которое позволяет отображать подсказку, при наведении курсора на закладку.
  /// </summary>
  [DefaultValue(false)]
  [CustomDisplayName("Attribute.FormDesigner_214")]
  [CustomCategory("Attribute.FormDesigner_5")]
  [CustomDescription("Attribute.FormDesigner.ShowToolTips.Description")]
  [TypeConverter(typeof (BooleanConverter))]
  [Editor(typeof (BooleanEditor), typeof (UITypeEditor))]
  public bool ShowToolTips
  {
    get => this._tabCtrl.ShowToolTips;
    set => this.SetValue(this._pdc[nameof (ShowToolTips)], (object) value);
  }

  /// <summary>
  /// Возвращает или задает способ установки размера закладки.
  /// </summary>
  [DefaultValue(TabSizeMode.Normal)]
  [CustomDisplayName("Attribute.FormDesigner_186")]
  [CustomCategory("Attribute.FormDesigner_5")]
  [CustomDescription("Attribute.FormDesigner.TabSizeMode.Description")]
  [TypeConverter(typeof (TabSizeModeConverter))]
  [Editor(typeof (TabSizeModeEditor), typeof (UITypeEditor))]
  public TabSizeMode SizeMode
  {
    get => this._tabCtrl.SizeMode;
    set => this.SetValue(this._pdc[nameof (SizeMode)], (object) value);
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
    get => this._tabCtrl.TabIndex;
    set => this.SetValue(this._pdc[nameof (TabIndex)], (object) value);
  }

  /// <summary>Возвращает коллекцию закладок.</summary>
  [CustomDisplayName("Attribute.FormDesigner_218")]
  [CustomCategory("Attribute.FormDesigner_5")]
  [CustomDescription("Attribute.FormDesigner.TabPages.Description")]
  [Editor(typeof (TabPageCollectionEditor), typeof (UITypeEditor))]
  public TabControl.TabPageCollection TabPages => this._tabCtrl.TabPages;

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
    get => this._tabCtrl.TabStop;
    set => this.SetValue(this._pdc[nameof (TabStop)], (object) value);
  }

  /// <summary>Поле для заметок.</summary>
  [DefaultValue("")]
  [CustomDisplayName("FormDesigner_PropName_Tag")]
  [CustomCategory("Attribute.FormDesigner_11")]
  [CustomDescription("FormDesigner_PropDescr_Tag")]
  public string Tag
  {
    get => Convert.ToString(this._tabCtrl.Tag);
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
    get => this._tabCtrl.Name;
    set
    {
      this._tabCtrl.Name = value;
      if (this._tabCtrl.Site == null)
        return;
      this._tabCtrl.Site.Name = value;
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
    get => this._tabCtrl.Anchor;
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
    get => this._tabCtrl.Dock;
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
    get => this._tabCtrl.Location;
    set => this.SetValue(this._pdc[nameof (Location)], (object) value);
  }

  /// <summary>Расстояние между элементами управления.</summary>
  [CustomDisplayName("Attribute.FormDesigner_121")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.Margin.Description")]
  [TypeConverter(typeof (MarginPaddingConverter))]
  public System.Windows.Forms.Padding Margin
  {
    get => this._tabCtrl.Margin;
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
    get => this._tabCtrl.MaximumSize;
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
    get => this._tabCtrl.MinimumSize;
    set => this.SetValue(this._pdc[nameof (MinimumSize)], (object) value);
  }

  /// <summary>Размеры элемента управления.</summary>
  [CustomDisplayName("Attribute.FormDesigner_75")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.Size.Description")]
  [TypeConverter(typeof (SizeConverter))]
  public Size Size
  {
    get => this._tabCtrl.Size;
    set => this.SetValue(this._pdc[nameof (Size)], (object) value);
  }

  /// <summary>Конструктор.</summary>
  /// <param name="parent"></param>
  public TabControlWrapper(IMTabControl parent)
  {
    this._tabCtrl = parent;
    this._pdc = TypeDescriptor.GetProperties((object) this._tabCtrl, true);
  }

  /// <summary>Метод установки значения через дескриптор.</summary>
  /// <param name="prop">Дескриптор свойства</param>
  /// <param name="value">Новое значение</param>
  private void SetValue(PropertyDescriptor prop, object value)
  {
    if (prop == null)
      throw new Exception(LocalizationHolder.rm.GetString(sc_7230.ssp_imclient_7231()));
    prop.SetValue((object) this._tabCtrl, value);
  }
}
