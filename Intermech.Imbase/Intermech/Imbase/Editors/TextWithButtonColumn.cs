// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.TextWithButtonColumn
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Editors;

public class TextWithButtonColumn : DataGridViewColumn
{
  private bool _textReadOnly;

  public TextWithButtonColumn()
    : base((DataGridViewCell) new TextWithButtonCell())
  {
  }

  public override DataGridViewCell CellTemplate
  {
    get => base.CellTemplate;
    set
    {
      base.CellTemplate = value == null || value.GetType().IsAssignableFrom(typeof (TextWithButtonCell)) ? value : throw new InvalidCastException("Must be a TextWithButtonCell");
    }
  }

  public bool TextReadOnly
  {
    get => this._textReadOnly;
    set => this._textReadOnly = value;
  }

  public event EventHandler ButtonClick;

  internal void OnButtonClick(object cell)
  {
    EventHandler buttonClick = this.ButtonClick;
    if (buttonClick == null)
      return;
    buttonClick(cell, EventArgs.Empty);
  }
}
