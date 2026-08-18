// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.TabPageWrapper
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

internal class TabPageWrapper : PanelWrapper
{
  private TabPage _parent = new TabPage();

  public TabPageWrapper()
  {
  }

  public TabPageWrapper(TabPage parent)
    : base((Panel) parent)
  {
    this._parent = parent;
  }

  [Browsable(true)]
  public override string Text
  {
    get => base.Text;
    set => base.Text = value;
  }

  /// <summary>
  /// Возвращает или задает значение, которое указывает, можно ли использовать цвет фона для текущего стиля.
  /// </summary>
  [CustomDisplayName("Attribute.FormDesigner.UseVisualStyleBackColor.Name")]
  [CustomCategory("Attribute.FormDesigner_8")]
  [CustomDescription("Attribute.FormDesigner.UseVisualStyleBackColor.Description")]
  [TypeConverter(typeof (BooleanConverter))]
  [Editor(typeof (BooleanEditor), typeof (UITypeEditor))]
  public bool UseVisualStyleBackColor
  {
    get => this._parent.UseVisualStyleBackColor;
    set => this.SetValue(this._pdc[nameof (UseVisualStyleBackColor)], (object) value);
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
  public override AutoSizeMode AutoSizeMode
  {
    get => base.AutoSizeMode;
    set => base.AutoSizeMode = value;
  }

  [Browsable(false)]
  public override DockStyle Dock
  {
    get => base.Dock;
    set => base.Dock = value;
  }

  [Browsable(false)]
  public override Point Location
  {
    get => base.Location;
    set => base.Location = value;
  }

  [Browsable(false)]
  public override Size MaximumSize
  {
    get => base.MaximumSize;
    set => base.MaximumSize = value;
  }

  [Browsable(false)]
  public override Size MinimumSize
  {
    get => base.MinimumSize;
    set => base.MinimumSize = value;
  }

  /// <summary>
  /// Возвращает или задает тект подсказки, которая появляется при наведении курсора на закладку.
  /// </summary>
  [CustomDisplayName("Attribute.FormDesigner_221")]
  [CustomCategory("Attribute.FormDesigner_92")]
  [CustomDescription("Attribute.FormDesigner_222")]
  public string ToolTipText
  {
    get => this._parent.ToolTipText;
    set => this.SetValue(this._pdc[nameof (ToolTipText)], (object) value);
  }
}
