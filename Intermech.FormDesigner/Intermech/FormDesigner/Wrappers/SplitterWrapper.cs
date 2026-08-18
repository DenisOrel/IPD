// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.SplitterWrapper
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

internal class SplitterWrapper : ControlWrapper
{
  private Splitter _parent = new Splitter();

  public SplitterWrapper()
  {
  }

  public SplitterWrapper(Splitter parent)
    : base((Control) parent)
  {
    this._parent = parent;
  }

  [CustomDisplayName("Attribute.FormDesigner_134")]
  [CustomCategory("Attribute.FormDesigner_8")]
  [CustomDescription("Attribute.FormDesigner.BorderStyle.Description")]
  [TypeConverter(typeof (BorderStyleConverter))]
  [Editor(typeof (BorderStyleEditor), typeof (UITypeEditor))]
  public BorderStyle BorderStyle
  {
    get => this._parent.BorderStyle;
    set => this.SetValue(this._pdc[nameof (BorderStyle)], (object) value);
  }

  [Browsable(false)]
  public override Font Font
  {
    get => base.Font;
    set => base.Font = value;
  }

  [Browsable(false)]
  public override Color ForeColor
  {
    get => base.ForeColor;
    set => base.ForeColor = value;
  }

  [Browsable(false)]
  public override string Text
  {
    get => base.Text;
    set => base.Text = value;
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
}
