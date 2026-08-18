// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.DataGridViewCalendarCell
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Editors;

public class DataGridViewCalendarCell : DataGridViewTextBoxCell
{
  public override object DefaultNewRowValue => (object) DateTime.Now;

  public override System.Type EditType => typeof (DataGridViewCalendarEditingControl);

  public override System.Type ValueType => typeof (DateTime);

  public override void InitializeEditingControl(
    int rowIndex,
    object initialFormattedValue,
    DataGridViewCellStyle dataGridViewCellStyle)
  {
    base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
    DataGridViewCalendarEditingControl editingControl = this.DataGridView.EditingControl as DataGridViewCalendarEditingControl;
    if (this.Value == DBNull.Value)
      editingControl.Value = DateTime.Now;
    else
      editingControl.Value = Convert.ToDateTime(this.Value);
  }

  protected override void OnKeyDown(KeyEventArgs e, int rowIndex)
  {
    base.OnKeyDown(e, rowIndex);
    if (e.KeyCode != Keys.Delete && e.KeyCode != Keys.Back)
      return;
    this.Value = (object) DBNull.Value;
  }
}
