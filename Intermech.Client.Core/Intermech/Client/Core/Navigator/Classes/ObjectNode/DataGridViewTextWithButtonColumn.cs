
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.DataGridViewTextWithButtonColumn
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Windows.Forms;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode;

public class DataGridViewTextWithButtonColumn : DataGridViewColumn
{
  private bool _textReadOnly;

  /// <summary>Конструктор.</summary>
  public DataGridViewTextWithButtonColumn()
    : base((DataGridViewCell) new DataGridViewTextWithButtonCell())
  {
  }

  /// <summary>
  /// 
  /// </summary>
  public override DataGridViewCell CellTemplate
  {
    get => base.CellTemplate;
    set
    {
      base.CellTemplate = value == null || value.GetType().IsAssignableFrom(typeof (DataGridViewTextWithButtonCell)) ? value : throw new InvalidCastException("Must be a TextWithButtonCell");
    }
  }

  /// <summary>Установка текста в значение "Только для чтения".</summary>
  public bool TextReadOnly
  {
    get => this._textReadOnly;
    set => this._textReadOnly = value;
  }

  /// <summary>
  /// 
  /// </summary>
  public event EventHandler ButtonClick;

  public event EventHandler KeyDown;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="cell"></param>
  internal void OnButtonClick(object cell)
  {
    if (this.ButtonClick == null)
      return;
    this.ButtonClick(cell, EventArgs.Empty);
  }

  internal void OnKeyDown(object cell)
  {
    if (this.KeyDown == null)
      return;
    this.KeyDown(cell, EventArgs.Empty);
  }
}
