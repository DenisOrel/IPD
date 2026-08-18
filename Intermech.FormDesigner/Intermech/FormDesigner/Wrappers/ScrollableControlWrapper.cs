// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.ScrollableControlWrapper
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

internal class ScrollableControlWrapper : ControlWrapper
{
  private ScrollableControl _parent = (ScrollableControl) new Panel();

  public ScrollableControlWrapper()
  {
  }

  public ScrollableControlWrapper(ScrollableControl parent)
    : base((Control) parent)
  {
    this._parent = parent;
  }

  /// <summary>
  /// Задает или получает значение, указывающее, будет ли контейнер давать возможность пользователю выполнять прокрутку любых элементов
  /// управления, помещенных вне его отображаемых границ.
  /// </summary>
  [CustomDisplayName("Attribute.FormDesigner_188")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.AutoScroll.Description")]
  [TypeConverter(typeof (BooleanConverter))]
  [Editor(typeof (BooleanEditor), typeof (UITypeEditor))]
  [DefaultValue(true)]
  public virtual bool AutoScroll
  {
    get => this._parent.AutoScroll;
    set => this.SetValue(this._pdc[nameof (AutoScroll)], (object) value);
  }

  /// <summary>
  /// Возвращает или задает размер поля автоматической прокрутки.
  /// </summary>
  [CustomDisplayName("Attribute.FormDesigner_190")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.AutoScrollMargin.Description")]
  [TypeConverter(typeof (SizeConverter))]
  public virtual Size AutoScrollMargin
  {
    get => this._parent.AutoScrollMargin;
    set => this.SetValue(this._pdc[nameof (AutoScrollMargin)], (object) value);
  }

  /// <summary>
  /// Возвращает или задает минимальный размер для автоматической прокрутки.
  /// </summary>
  [CustomDisplayName("Attribute.FormDesigner_192")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.AutoScrollMinSize.Description")]
  [TypeConverter(typeof (SizeConverter))]
  public virtual Size AutoScrollMinSize
  {
    get => this._parent.AutoScrollMinSize;
    set => this.SetValue(this._pdc[nameof (AutoScrollMinSize)], (object) value);
  }

  /// <summary>
  /// Возвращает параметры для заполнения стыковки на всех краях элемента управления.
  /// </summary>
  [CustomDisplayName("Attribute.FormDesigner_196")]
  [CustomCategory("Attribute.FormDesigner_51")]
  [CustomDescription("Attribute.FormDesigner.DockPadding.Description")]
  [TypeConverter(typeof (DockPaddingEdgesConverter))]
  [RefreshProperties(RefreshProperties.All)]
  public virtual ScrollableControl.DockPaddingEdges DockPadding => this._parent.DockPadding;
}
