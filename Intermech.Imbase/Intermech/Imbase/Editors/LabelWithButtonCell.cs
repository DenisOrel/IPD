// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.LabelWithButtonCell
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Editors;

public class LabelWithButtonCell : DataGridViewTextBoxCell
{
  private LabelWithButtonEditingControl ctrl;

  public string Value
  {
    get => this.ctrl.Value;
    set => this.ctrl.Value = value;
  }

  public override void InitializeEditingControl(
    int rowIndex,
    object initialFormattedValue,
    DataGridViewCellStyle dataGridViewCellStyle)
  {
    base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
    this.ctrl = this.DataGridView.EditingControl as LabelWithButtonEditingControl;
    this.ctrl.Value = base.Value != null ? base.Value.ToString() : string.Empty;
  }

  public override System.Type EditType => typeof (LabelWithButtonEditingControl);

  public override System.Type ValueType => typeof (string);

  public override object DefaultNewRowValue => (object) string.Empty;
}
