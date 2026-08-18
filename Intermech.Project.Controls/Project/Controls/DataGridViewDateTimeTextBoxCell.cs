// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.DataGridViewDateTimeTextBoxCell
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

internal class DataGridViewDateTimeTextBoxCell : EnhDataGridViewTextBoxCell, ICloneable, IDisposable
{
  public override void InitializeEditingControl(
    int rowIndex,
    [CanBeNull] object initialFormattedValue,
    DataGridViewCellStyle dataGridViewCellStyle)
  {
    base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
    ProjectDataGridView dataGridView = this.DataGridView;
    if (!(this.OwningColumn is DataGridViewDateTimeTextBoxColumn) || !(dataGridView.EditingControl is DataGridViewDateTimeTextBoxEditingControl editingControl))
      return;
    string str = (string) this.Value ?? string.Empty;
    editingControl.Text = str;
  }

  [NotNull]
  public override System.Type EditType => typeof (DataGridViewDateTimeTextBoxEditingControl);
}
