
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.DataGridViewCalendarEditingControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Windows.Forms;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode;

internal class DataGridViewCalendarEditingControl : DateTimePicker, IDataGridViewEditingControl
{
  private DataGridView dataGridView;
  private bool valueChanged;
  private int rowIndex;

  /// <summary>Конструктор.</summary>
  public DataGridViewCalendarEditingControl()
  {
    this.Format = DateTimePickerFormat.Custom;
    this.CustomFormat = "MMMM dd, yyyy";
    this.CloseUp += new EventHandler(this.Close_Up);
  }

  /// <summary>
  /// Implements the IDataGridViewEditingControl.EditingControlDataGridView property.
  /// </summary>
  public DataGridView EditingControlDataGridView
  {
    get => this.dataGridView;
    set => this.dataGridView = value;
  }

  /// <summary>
  /// Implements the IDataGridViewEditingControl.EditingControlFormattedValue property.
  /// </summary>
  public object EditingControlFormattedValue
  {
    get
    {
      DateTime date = this.Value;
      date = date.Date;
      return (object) date.ToString();
    }
    set
    {
      if (!(value is string))
        return;
      try
      {
        this.Value = DateTime.Parse((string) value).Date;
      }
      catch
      {
        this.Value = DateTime.Now;
      }
    }
  }

  /// <summary>
  /// Implements the IDataGridViewEditingControl.EditingControlRowIndex property.
  /// </summary>
  public int EditingControlRowIndex
  {
    get => this.rowIndex;
    set => this.rowIndex = value;
  }

  /// <summary>
  /// Implements the IDataGridViewEditingControl.EditingControlValueChanged property.
  /// </summary>
  public bool EditingControlValueChanged
  {
    get => this.valueChanged;
    set => this.valueChanged = value;
  }

  /// <summary>
  /// Implements the IDataGridViewEditingControl.EditingPanelCursor property.
  /// </summary>
  public Cursor EditingPanelCursor => this.Cursor;

  /// <summary>
  /// Implements the IDataGridViewEditingControl.RepositionEditingControlOnValueChange property.
  /// </summary>
  public bool RepositionEditingControlOnValueChange => false;

  /// <summary>
  /// Implements the IDataGridViewEditingControl.ApplyCellStyleToEditingControl method.
  /// </summary>
  /// <param name="dataGridViewCellStyle"></param>
  public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
  {
    this.Font = dataGridViewCellStyle.Font;
    this.CalendarForeColor = dataGridViewCellStyle.ForeColor;
    this.CalendarMonthBackground = dataGridViewCellStyle.BackColor;
  }

  /// <summary>
  /// Implements the IDataGridViewEditingControl.EditingControlWantsInputKey method.
  /// </summary>
  /// <param name="key"></param>
  /// <param name="dataGridViewWantsInputKey"></param>
  /// <returns></returns>
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

  /// <summary>
  /// Implements the IDataGridViewEditingControl.GetEditingControlFormattedValue method.
  /// </summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
  {
    return this.EditingControlFormattedValue;
  }

  public void SetEditingControlFormattedValue(DateTime dateTime)
  {
    this.EditingControlFormattedValue = (object) dateTime;
  }

  /// <summary>
  /// Implements the IDataGridViewEditingControl.PrepareEditingControlForEdit method.
  /// </summary>
  /// <param name="selectAll"></param>
  public void PrepareEditingControlForEdit(bool selectAll)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="eventargs"></param>
  protected override void OnValueChanged(EventArgs eventargs)
  {
    this.valueChanged = true;
    this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
    base.OnValueChanged(eventargs);
  }

  private void Close_Up(object sender, EventArgs e)
  {
    if (this.EditingControlDataGridView == null || !(this.EditingControlDataGridView.CurrentCell is DataGridViewCalendarCell currentCell))
      return;
    (currentCell.OwningColumn as DataGridViewCalendarColumn).OnClouseUp((object) currentCell);
  }
}
