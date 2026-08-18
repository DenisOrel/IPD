
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.DataGridViewTextWithButtonCell
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Windows.Forms;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode;

public class DataGridViewTextWithButtonCell : DataGridViewTextBoxCell
{
  private DataGridViewTextWithButtonEditingControl ctrl;

  /// <summary>
  /// 
  /// </summary>
  public string Value
  {
    get => this.ctrl.Value;
    set => this.ctrl.Value = value;
  }

  /// <summary>Инициализация контрола редактирования.</summary>
  /// <param name="rowIndex">Номер строки</param>
  /// <param name="initialFormattedValue">Значение ячейки</param>
  /// <param name="dataGridViewCellStyle">Стиль ячейки</param>
  public override void InitializeEditingControl(
    int rowIndex,
    object initialFormattedValue,
    DataGridViewCellStyle dataGridViewCellStyle)
  {
    base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
    this.ctrl = this.DataGridView.EditingControl as DataGridViewTextWithButtonEditingControl;
    this.ctrl.EditingControlRowIndex = rowIndex;
    if (initialFormattedValue != null)
      this.ctrl.Value = initialFormattedValue.ToString();
    else
      this.ctrl.Value = string.Empty;
  }

  /// <summary>
  /// 
  /// </summary>
  public override System.Type EditType => typeof (DataGridViewTextWithButtonEditingControl);

  /// <summary>
  /// 
  /// </summary>
  public override System.Type ValueType => typeof (string);

  /// <summary>
  /// 
  /// </summary>
  public override object DefaultNewRowValue => (object) string.Empty;
}
