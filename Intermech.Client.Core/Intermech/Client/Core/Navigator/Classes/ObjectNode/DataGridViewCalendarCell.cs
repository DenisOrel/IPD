
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.DataGridViewCalendarCell
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Windows.Forms;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode;

public class DataGridViewCalendarCell : DataGridViewTextBoxCell
{
  /// <summary>
  /// 
  /// </summary>
  public override object DefaultNewRowValue => (object) DateTime.Now;

  /// <summary>
  /// 
  /// </summary>
  public override System.Type EditType => typeof (DataGridViewCalendarEditingControl);

  /// <summary>
  /// 
  /// </summary>
  public override System.Type ValueType => typeof (DateTime);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="rowIndex"></param>
  /// <param name="initialFormattedValue"></param>
  /// <param name="dataGridViewCellStyle"></param>
  public override void InitializeEditingControl(
    int rowIndex,
    object initialFormattedValue,
    DataGridViewCellStyle dataGridViewCellStyle)
  {
    base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
    DataGridViewCalendarEditingControl editingControl = this.DataGridView.EditingControl as DataGridViewCalendarEditingControl;
    if (this.Value == null || this.Value.ToString() == DateTime.MinValue.ToString())
      editingControl.Value = (DateTime) this.DefaultNewRowValue;
    else
      editingControl.Value = (DateTime) this.Value;
  }
}
