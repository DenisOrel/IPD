// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.DataGridViewCalendarEditingControl
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Editors;

internal class DataGridViewCalendarEditingControl : DateTimePicker, IDataGridViewEditingControl
{
  private DataGridView dataGridView;
  private bool valueChanged;
  private int rowIndex;

  public DataGridViewCalendarEditingControl()
  {
    this.Format = DateTimePickerFormat.Custom;
    this.CustomFormat = "dd.MM.yyyy H:m:s";
  }

  public DataGridView EditingControlDataGridView
  {
    get => this.dataGridView;
    set => this.dataGridView = value;
  }

  public object EditingControlFormattedValue
  {
    get => (object) this.Value.ToString();
    set
    {
      string s = value.ToString();
      if (s == null)
        return;
      this.Value = DateTime.Parse(s);
    }
  }

  public int EditingControlRowIndex
  {
    get => this.rowIndex;
    set => this.rowIndex = value;
  }

  public bool EditingControlValueChanged
  {
    get => this.valueChanged;
    set => this.valueChanged = value;
  }

  public Cursor EditingPanelCursor => this.Cursor;

  public bool RepositionEditingControlOnValueChange => false;

  public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
  {
    this.Font = dataGridViewCellStyle.Font;
    this.CalendarForeColor = dataGridViewCellStyle.ForeColor;
    this.CalendarMonthBackground = dataGridViewCellStyle.BackColor;
  }

  public bool EditingControlWantsInputKey(Keys key, bool dataGridViewWantsInputKey)
  {
    switch (key & Keys.KeyCode)
    {
      case Keys.Prior:
      case Keys.Next:
      case Keys.End:
      case Keys.Home:
      case Keys.Left:
      case Keys.Up:
      case Keys.Right:
      case Keys.Down:
        return true;
      default:
        return false;
    }
  }

  public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
  {
    return this.EditingControlFormattedValue;
  }

  public void PrepareEditingControlForEdit(bool selectAll)
  {
  }

  protected override void OnValueChanged(EventArgs eventargs)
  {
    this.valueChanged = true;
    this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
    base.OnValueChanged(eventargs);
  }
}
