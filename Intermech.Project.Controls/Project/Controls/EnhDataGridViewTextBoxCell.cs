// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.EnhDataGridViewTextBoxCell
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

public class EnhDataGridViewTextBoxCell : DataGridViewTextBoxCell, ICloneable, IDisposable
{
  private static bool _inBulkSet;

  public override void InitializeEditingControl(
    int rowIndex,
    [CanBeNull] object initialFormattedValue,
    DataGridViewCellStyle dataGridViewCellStyle)
  {
    base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
  }

  [NotNull]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public override System.Type EditType => typeof (EnhDataGridViewTextBoxEditingControl);

  [CanBeNull]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public ProjectDataGridView DataGridView => base.DataGridView as ProjectDataGridView;

  protected override bool SetValue(int rowIndex, [CanBeNull] object value)
  {
    try
    {
      bool flag = base.SetValue(rowIndex, value);
      if (!EnhDataGridViewTextBoxCell._inBulkSet && this.DataGridView != null)
      {
        EnhDataGridViewTextBoxCell._inBulkSet = true;
        try
        {
          DataGridViewSelectedCellCollection selectedCells = this.DataGridView.SelectedCells;
          if (selectedCells.Count > 1)
          {
            foreach (DataGridViewCell dataGridViewCell in (BaseCollection) selectedCells)
            {
              if (dataGridViewCell != null && dataGridViewCell != this && dataGridViewCell.RowIndex != rowIndex && dataGridViewCell.OwningColumn == this.OwningColumn && this.DataGridView.Rows[dataGridViewCell.RowIndex].DataBoundItem is Task dataBoundItem && !dataBoundItem.ReadOnly && !dataBoundItem.HasSubTasks)
                dataGridViewCell.Value = value;
            }
          }
        }
        finally
        {
          EnhDataGridViewTextBoxCell._inBulkSet = false;
        }
      }
      if (flag && this.DataGridView != null)
        this.DataGridView.ErrorMessage = string.Empty;
      return flag;
    }
    catch (Exception ex)
    {
      if (ex is ICancelEditException)
      {
        if (this.DataGridView != null)
        {
          this.DataGridView.ErrorMessage = string.Empty;
          this.DataGridView.CancelEdit();
        }
        throw;
      }
      if (this.DataGridView != null)
        this.DataGridView.ErrorMessage = ex.Message;
      int num = (int) MessageBox.Show(ex.Message, (string) null, MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return false;
    }
  }

  public override bool KeyEntersEditMode(KeyEventArgs e)
  {
    return e.Shift && e.KeyCode == Keys.Insert || base.KeyEntersEditMode(e);
  }

  public int GetPreferredHeight()
  {
    ProjectDataGridView dataGridView = this.DataGridView;
    if ((dataGridView != null ? (!dataGridView.IsHandleCreated ? 1 : 0) : 1) != 0)
      return -1;
    DataGridViewCellStyle inheritedStyle = this.GetInheritedStyle((DataGridViewCellStyle) null, this.RowIndex, false);
    int height;
    using (Graphics graphics1 = Graphics.FromHwnd(this.DataGridView.Handle))
    {
      Graphics graphics2 = graphics1;
      DataGridViewCellStyle cellStyle = inheritedStyle;
      int rowIndex = this.RowIndex;
      DataGridViewColumn owningColumn = this.OwningColumn;
      Size constraintSize = new Size(owningColumn != null ? owningColumn.Width : 0, 0);
      height = this.GetPreferredSize(graphics2, cellStyle, rowIndex, constraintSize).Height;
      if (height % 2 == 0)
        ++height;
    }
    return height;
  }
}
