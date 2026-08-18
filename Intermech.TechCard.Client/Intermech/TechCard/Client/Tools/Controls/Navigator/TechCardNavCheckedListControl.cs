// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Tools.Controls.Navigator.TechCardNavCheckedListControl
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Navigator.Controls;
using Intermech.Navigator.Views;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.TechCard.Client.Tools.Controls.Navigator;

/// <summary>
/// Control для отображения списка объектов навигатора с Checked поддержкой
/// </summary>
public class TechCardNavCheckedListControl : TechCardNavObjListControl
{
  /// <summary>Наименование столбца с checkBox</summary>
  private const string CheckBoxColName = "Special_CheckBoxColl";
  /// <summary>Ширина столбца с checkBox</summary>
  private const int CheckBoxColWidth = 30;
  /// <summary>Позиция колонки с CheckBox в списке по умолчанию</summary>
  private const int CheckBoxColOrderDef = 1;
  /// <summary>Признак отображения checkBox</summary>
  private bool _checkBoxes = true;
  /// <summary>Позиция колонки с CheckBox в списке</summary>
  private int _colCheckBoxOrder = 1;
  /// <summary>Стиль для отображения "спец" колонки</summary>
  private iGCellStyle _colCheckBoxStyle;
  /// <summary>"Шаблон" для спец колонки</summary>
  private iGColPattern _colPattern;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  /// <summary>Обновление</summary>
  private void UpdateCheckBoxesMode()
  {
    if (this.Grid?.Cols == null)
      return;
    int colIndex = -1;
    foreach (iGCol col in (IEnumerable) this.Grid.Cols)
    {
      if (col != null && !(col.Key != "Special_CheckBoxColl"))
      {
        colIndex = col.Index;
        break;
      }
    }
    bool checkBoxes = this._checkBoxes;
    if (checkBoxes)
    {
      if (!checkBoxes || !this._dataLoaded || colIndex != -1)
        return;
      iGCol iGcol = this.Grid.Cols.Add(this._colPattern);
      iGcol.CellStyle = this._colCheckBoxStyle;
      Dictionary<iGCol, bool> dictionary = new Dictionary<iGCol, bool>(this.Grid.Cols.Count);
      foreach (iGCol col in (IEnumerable) this.Grid.Cols)
      {
        if (col != null && !col.AllowMoving)
        {
          dictionary.Add(col, true);
          col.AllowMoving = true;
        }
      }
      int num = this._colCheckBoxOrder;
      if (num >= this.Grid.Cols.Count)
        num = 1;
      iGcol.Order = num;
      foreach (iGCol key in dictionary.Keys)
      {
        if (key != null)
          key.AllowMoving = false;
      }
    }
    else
    {
      if (colIndex == -1)
        return;
      this.Grid.Cols.RemoveAt(colIndex);
    }
  }

  /// <summary>Конструктор</summary>
  public TechCardNavCheckedListControl()
  {
    this.InitializeComponent();
    this.InitializeCustomProperties();
  }

  /// <summary>Initialize custom data</summary>
  private void InitializeCustomProperties()
  {
    this._colCheckBoxStyle = new iGCellStyle(true);
    this._colCheckBoxStyle.Flags = iGCellFlags.DisplayImage;
    this._colCheckBoxStyle.ImageAlign = iGContentAlignment.MiddleCenter;
    this._colCheckBoxStyle.SingleClickEdit = iGBool.True;
    this._colCheckBoxStyle.TextAlign = iGContentAlignment.MiddleCenter;
    this._colCheckBoxStyle.Type = iGCellType.Check;
    this._colCheckBoxStyle.ValueType = typeof (int);
    this._colCheckBoxStyle.ReadOnly = iGBool.False;
    this._colCheckBoxStyle.CustomDrawFlags = iGCustomDrawFlags.Background;
    this._colPattern = new iGColPattern(30, true, true, 30, 30, false, false, false, iGSortType.None, iGSortOrder.None, false, (object) null, (object) "", "Special_CheckBoxColl", -1, (object) 0, (object) 0, -1);
  }

  /// <summary>Активировать закладку</summary>
  /// <param name="previousView">Предыдущая закладка</param>
  public override void Activate(IView previousView)
  {
    if (previousView == PageViewsManager.BlackHoleView)
      return;
    base.Activate(previousView);
    this.UpdateCheckBoxesMode();
  }

  /// <summary>Запрос на редактирование</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected override void GridRequestEdit(object sender, iGRequestEditEventArgs e)
  {
  }

  /// <summary>Получение checked статуса для строки</summary>
  /// <param name="rowIdx"></param>
  /// <returns></returns>
  public CheckState GetRowCheckState(int rowIdx) => this.GetRowCheckState(this.Grid.Rows[rowIdx]);

  /// <summary>Получение checked статуса для строки</summary>
  /// <param name="row"></param>
  /// <returns></returns>
  public CheckState GetRowCheckState(iGRow row)
  {
    return (CheckState) row.Cells["Special_CheckBoxColl"].Value;
  }

  /// <summary>Назначение checked статуса для строки</summary>
  /// <param name="rowIdx"></param>
  /// <param name="state"></param>
  public void SetRowCheckState(int rowIdx, CheckState state)
  {
    this.SetRowCheckState(this.Grid.Rows[rowIdx], state);
  }

  /// <summary>Назначение checked статуса для строки</summary>
  /// <param name="row"></param>
  /// <param name="state"></param>
  public void SetRowCheckState(iGRow row, CheckState state)
  {
    row.Cells["Special_CheckBoxColl"].Value = (object) (int) state;
  }

  /// <summary>Получение checked статуса для строк</summary>
  /// <returns></returns>
  public Dictionary<iGRow, CheckState> GetRowsCheckState()
  {
    Dictionary<iGRow, CheckState> rowsCheckState = new Dictionary<iGRow, CheckState>(this.Grid.Rows.Count);
    foreach (iGRow row in (IEnumerable) this.Grid.Rows)
    {
      if (row != null)
        rowsCheckState.Add(row, this.GetRowCheckState(row));
    }
    return rowsCheckState;
  }

  /// <summary>Список</summary>
  public iGRow[] CheckedRows
  {
    get
    {
      List<iGRow> iGrowList = new List<iGRow>(this.Grid.Rows.Count);
      foreach (KeyValuePair<iGRow, CheckState> keyValuePair in this.GetRowsCheckState())
      {
        if (keyValuePair.Value == CheckState.Checked)
          iGrowList.Add(keyValuePair.Key);
      }
      return iGrowList.ToArray();
    }
  }

  /// <summary>Отображение CheckBox</summary>
  public bool CheckBoxes
  {
    get => this._checkBoxes;
    set
    {
      if (this._checkBoxes == value)
        return;
      this._checkBoxes = value;
      this.UpdateCheckBoxesMode();
    }
  }

  /// <summary>Позиция колонки с CheckBox в списке</summary>
  public int CheckBoxColumnOrder
  {
    get => this._colCheckBoxOrder;
    set => this._colCheckBoxOrder = value;
  }

  private void _grid_CellMouseUp(object sender, iGCellMouseUpEventArgs e)
  {
    if (e.ColIndex != 1 || e.Button != MouseButtons.Left)
      return;
    int rowIndex = e.RowIndex;
    int count = this._grid.Rows.Count;
  }

  private void _grid_BeforeCommitEdit(object sender, iGBeforeCommitEditEventArgs e)
  {
  }

  private void _grid_AfterCommitEdit(object sender, iGAfterCommitEditEventArgs e)
  {
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
    {
      this._colCheckBoxStyle?.Dispose();
      this._colCheckBoxStyle = (iGCellStyle) null;
      this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ((ISupportInitialize) this._grid).BeginInit();
    ((ISupportInitialize) this._pictureBox).BeginInit();
    this.SuspendLayout();
    this._toolBar.Hidden = false;
    this._toolBar.Size = new Size(1151, 40);
    this._grid.DefaultAutoGroupRow.Height = 25;
    this._grid.FrozenArea.ColCount = 1;
    this._grid.FrozenArea.SortFrozenRows = true;
    this._grid.GroupBox.BackColor = SystemColors.AppWorkspace;
    this._grid.GroupBox.HintBackColor = SystemColors.AppWorkspace;
    this._grid.GroupBox.HintForeColor = SystemColors.ControlText;
    this._grid.GroupBox.Text = "Перетащите заголовок колонки в эту область для группировки по значениям этой колонки";
    this._grid.Header.AutoHeightFlags = iGHdrAutoHeightFlags.OnAddCol | iGHdrAutoHeightFlags.OnRemoveCol | iGHdrAutoHeightFlags.OnShowCol | iGHdrAutoHeightFlags.OnContentsChange | iGHdrAutoHeightFlags.OnThemeChange | iGHdrAutoHeightFlags.OnResizeCol;
    this._grid.Header.Height = 20;
    this._grid.LayoutObject.Flags = iGLayoutFlags.Grouping | iGLayoutFlags.Sorting | iGLayoutFlags.ColVisibility | iGLayoutFlags.ColWidth | iGLayoutFlags.ColOrder;
    this._grid.Size = new Size(1151, 160 /*0xA0*/);
    this._grid.CellMouseUp += new iGCellMouseUpEventHandler(this._grid_CellMouseUp);
    this._grid.BeforeCommitEdit += new iGBeforeCommitEditEventHandler(this._grid_BeforeCommitEdit);
    this._grid.AfterCommitEdit += new iGAfterCommitEditEventHandler(this._grid_AfterCommitEdit);
    this._filtersComboBoxItem.Padding.Bottom = 0;
    this._filtersComboBoxItem.Padding.Left = 1;
    this._filtersComboBoxItem.Padding.Right = 1;
    this._filtersComboBoxItem.Padding.Top = 0;
    this.buttonHeightSet.Padding.Bottom = 0;
    this.buttonHeightSet.Padding.Left = 0;
    this.buttonHeightSet.Padding.Right = 0;
    this.buttonHeightSet.Padding.Top = 0;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Name = nameof (TechCardNavCheckedListControl);
    ((ISupportInitialize) this._grid).EndInit();
    ((ISupportInitialize) this._pictureBox).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
