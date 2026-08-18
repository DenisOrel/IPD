// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.DataGridViewDurationTextBoxCell
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

internal class DataGridViewDurationTextBoxCell : EnhDataGridViewTextBoxCell
{
  public override void InitializeEditingControl(
    int rowIndex,
    [CanBeNull] object initialFormattedValue,
    DataGridViewCellStyle dataGridViewCellStyle)
  {
    base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
    DataGridViewDurationTextBoxEditingControl editingControl = this.DataGridView.EditingControl as DataGridViewDurationTextBoxEditingControl;
    if (!(this.OwningColumn is DurationColumn))
      return;
    editingControl.Text = (string) this.Value ?? string.Empty;
  }

  [NotNull]
  public override System.Type EditType
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return typeof (DataGridViewDurationTextBoxEditingControl);
    }
  }
}
