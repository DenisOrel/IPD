// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.UserControlKeyWords
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary> Описание класса UserControlSetupSkipPositions </summary>
public class UserControlKeyWords : ExtUserControl
{
  private IContainer components;
  private ToolTipController _editModeToolTip;
  public Button _btnReset;
  private Button bAdd;
  private Button bDelete;
  private Intermech.VirtualTreeView.VirtualTreeView tree;
  protected Column column;
  protected CellEditor editSeparator;
  private TextBox textBox;
  private ToolTipController _readModeToolTip;
  public KeyWordsSchema schema;

  public UserControlKeyWords()
  {
    this.InitializeComponent();
    this.Init();
  }

  /// <summary> Инциализация формы </summary>
  protected void Init()
  {
  }

  /// <summary> Очистка использованных ресурсов </summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary> Обязательный метод, требуемый дизайнеру формы - не модиффицируйте данный код </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this._editModeToolTip = new ToolTipController(this.components);
    this._readModeToolTip = new ToolTipController(this.components);
    this._btnReset = new Button();
    this.bAdd = new Button();
    this.bDelete = new Button();
    this.tree = new Intermech.VirtualTreeView.VirtualTreeView();
    this.column = new Column();
    this.editSeparator = new CellEditor();
    this.textBox = new TextBox();
    this.tree.BeginInit();
    this.SuspendLayout();
    this._editModeToolTip.Active = false;
    this._editModeToolTip.Style = new ViewStyle("ToolTip style");
    this._readModeToolTip.Style = new ViewStyle("ToolTip style");
    this._btnReset.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this._btnReset.Enabled = false;
    this._btnReset.FlatStyle = FlatStyle.System;
    this._btnReset.Location = new Point(3, 341);
    this._btnReset.Name = "_btnReset";
    this._btnReset.Size = new Size(121, 27);
    this._btnReset.TabIndex = 18;
    this._btnReset.Text = "По умолчанию";
    this._btnReset.Click += new EventHandler(this._btnReset_Click);
    this.bAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.bAdd.Location = new Point(3, 312);
    this.bAdd.Name = "bAdd";
    this.bAdd.Size = new Size(121, 27);
    this.bAdd.TabIndex = 22;
    this.bAdd.Text = "Добавить";
    this.bAdd.UseVisualStyleBackColor = true;
    this.bAdd.Click += new EventHandler(this.bAdd_Click);
    this.bDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.bDelete.Location = new Point(130, 312);
    this.bDelete.Name = "bDelete";
    this.bDelete.Size = new Size(121, 27);
    this.bDelete.TabIndex = 23;
    this.bDelete.Text = "Удалить";
    this.bDelete.UseVisualStyleBackColor = true;
    this.bDelete.Click += new EventHandler(this.bDelete_Click);
    this.tree.AllowDrop = true;
    this.tree.AllowIndividualRowResize = false;
    this.tree.AllowRowResize = false;
    this.tree.AllowUserPinnedColumns = false;
    this.tree.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.tree.AutoFitColumns = true;
    this.tree.Columns.Add(this.column);
    this.tree.DisableHeaderContextMenu = true;
    this.tree.Editors.Add(this.editSeparator);
    this.tree.ImageList = (ImageList) null;
    this.tree.IndentWidth = 0;
    this.tree.LineStyle = LineStyle.Dot;
    this.tree.Location = new Point(3, 3);
    this.tree.MainColumn = this.column;
    this.tree.MinRowHeight = 21;
    this.tree.Name = "tree";
    this.tree.RowHeight = 21;
    this.tree.RowSelectedUnfocusedStyle.BackColor = SystemColors.Highlight;
    this.tree.RowSelectedUnfocusedStyle.ForeColor = SystemColors.HighlightText;
    this.tree.ShowRootRow = false;
    this.tree.Size = new Size(324, 302);
    this.tree.SortColumn = this.column;
    this.tree.SuppressErrorMessages = true;
    this.tree.TabIndex = 25;
    this.tree.GetCellData += new GetCellDataHandler(this.tree_GetCellData);
    this.tree.SetCellValue += new SetCellValueHandler(this.tree_SetCellValue);
    this.tree.SortColumnChanged += new EventHandler(this.tree_SortColumnChanged);
    this.tree.Click += new EventHandler(this.tree_Click);
    this.column.Caption = "Ключевые слова";
    this.column.CellEditor = this.editSeparator;
    this.column.CellStyle.BackColor = SystemColors.InactiveCaptionText;
    this.column.HeaderStyle.HorzAlignment = StringAlignment.Near;
    this.column.MinWidth = 100;
    this.column.Movable = false;
    this.column.Name = "column";
    this.column.ToolTip = "";
    this.column.Width = 320;
    this.editSeparator.CellAlignment = ContentAlignment.MiddleCenter;
    this.editSeparator.Control = (Control) this.textBox;
    this.textBox.Location = new Point(140, 468);
    this.textBox.Name = "textBox";
    this.textBox.Size = new Size(23, 20);
    this.textBox.TabIndex = 26;
    this.textBox.Visible = false;
    this.Controls.Add((Control) this.textBox);
    this.Controls.Add((Control) this.tree);
    this.Controls.Add((Control) this.bDelete);
    this.Controls.Add((Control) this.bAdd);
    this.Controls.Add((Control) this._btnReset);
    this.MinimumSize = new Size(330, 215);
    this.Name = nameof (UserControlKeyWords);
    this.Size = new Size(330, 373);
    this.tree.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  /// <summary> Схема пропуска строк </summary>
  public KeyWordsSchema KeyWordsSchema
  {
    get => this.schema;
    set
    {
      this.LockControls();
      try
      {
        this.schema = value;
        this.Changed = false;
        this.RefreshReadOnly();
        this.UpdateControls(true);
        this.RaiseOnInitDataEvent((object) this.schema);
      }
      finally
      {
        this.UnlockControls();
      }
    }
  }

  private void UpdateButtons()
  {
    if (this.ReadOnly)
    {
      this.bAdd.Enabled = false;
      this.bDelete.Enabled = false;
    }
    else
    {
      this.bAdd.Enabled = true;
      this.bDelete.Enabled = this.schema != null && this.schema.KeyWords.Count > 0;
    }
  }

  /// <summary> Обновление визуальных контролов </summary>
  protected override void UpdateControls()
  {
    if (this._editModeToolTip != null)
    {
      if (this.ReadOnly)
      {
        if (this._editModeToolTip.Active)
        {
          this._editModeToolTip.Active = false;
          this._readModeToolTip.Active = true;
        }
      }
      else if (this._readModeToolTip.Active)
      {
        this._readModeToolTip.Active = false;
        this._editModeToolTip.Active = true;
      }
    }
    this.UpdateButtons();
    this.RefreshGrid();
    this.tree.Enabled = !this.ReadOnly;
    this.RefreshBoldUpDown((Control) null);
    this._btnReset.Enabled = !this.ReadOnly;
  }

  private void RefreshGrid()
  {
    this.tree.DataSource = this.column.SortDirection == ListSortDirection.Descending ? (object) this.schema?.KeyWords?.RevertList() : (object) this.schema?.KeyWords;
  }

  /// <summary> Проверка, должно ли быть доступно редактирование </summary>
  public override bool GetIsReadOnly() => this.schema == null || this.schema.ReadOnly;

  /// <summary>Обновление параметра Bold у шрифта NumericUpDown</summary>
  /// <param name="numericUpDown">NumericUpDown, у которого надо обновить Bold. Если = null, то обновляется у всех</param>
  public void RefreshBoldUpDown(Control control)
  {
  }

  private void ChangeUpDownFontBold(Control control, bool mustBeBold)
  {
    if (control.Font.Bold == mustBeBold)
      return;
    control.Font = new Font(control.Font.FontFamily, control.Font.SizeInPoints, mustBeBold ? FontStyle.Bold : FontStyle.Regular, control.Font.Unit, control.Font.GdiCharSet, control.Font.GdiVerticalFont);
  }

  private void BeforeChangeUpDown(SpinEdit spinEdit, ChangingEventArgs e)
  {
    bool wasUpdated = false;
    int oldValue = e.OldValue == null || e.OldValue.GetType() != typeof (int) ? 0 : (int) e.OldValue;
    if (this.schema == null || this.ControlsAreUpdating)
      return;
    e.Cancel = !this.CheckCanEdit(ref wasUpdated) || wasUpdated && oldValue != Decimal.ToInt32(spinEdit.Value);
  }

  private bool BeforeUpDownEdit()
  {
    if (this.ReadOnly || this.schema == null || this.ControlsAreUpdating)
      return false;
    bool wasUpdated = false;
    return this.CheckCanEdit(ref wasUpdated);
  }

  private void AfterUpDownEdit() => this.Changed = true;

  private void _btnReset_Click(object sender, EventArgs e)
  {
    bool wasUpdated = false;
    if (this.ReadOnly || !this.CheckCanEdit(ref wasUpdated) || MessageBox.Show("Сбросить изменения в ключевых словах", "Ключевые слова для материалов", MessageBoxButtons.YesNo) != DialogResult.Yes)
      return;
    this.LockControls();
    try
    {
      this.schema.LoadDefaultParams();
      this.UpdateControls(true);
      this.Changed = true;
    }
    finally
    {
      this.UnlockControls();
    }
  }

  private void bAdd_Click(object sender, EventArgs e)
  {
    if (this.schema != null & this.BeforeUpDownEdit() && !this.schema.KeyWords.Contains(""))
    {
      this.schema.AddKeyWord("", false);
      this.RefreshGrid();
      this.AfterUpDownEdit();
      Row row = this.tree.FindRow((this.tree.DataSource as IList)[0]);
      if (row != null)
      {
        this.tree.SelectedRow = row;
        this.tree.FocusRow = row;
        this.tree.EditFirstCellInFocusRow();
      }
    }
    this.UpdateButtons();
  }

  private void bDelete_Click(object sender, EventArgs e)
  {
    if (this.schema == null || !this.BeforeUpDownEdit() || this.tree.SelectedRow == null)
      return;
    int index = this.tree.SelectedRow.RowIndex;
    if (index > 1)
      --index;
    else if (index > this.tree.NumVisibleRows - 1)
      index = -1;
    this.schema.RemoveKeyWord((string) this.tree.SelectedItem);
    this.RefreshGrid();
    if (index != -1)
      this.tree.SelectedRow = this.tree.GetRow(index);
    this.AfterUpDownEdit();
    this.UpdateButtons();
  }

  private void gridKeyWords_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
  {
    if (!this.BeforeUpDownEdit())
      e.Cancel = true;
    else if (this.schema.KeyWords.Contains(e.FormattedValue.ToString()))
    {
      e.Cancel = true;
    }
    else
    {
      e.Cancel = !this.schema.SetKeyWord(this.schema.KeyWords[e.RowIndex], e.FormattedValue.ToString());
      this.RefreshGrid();
    }
  }

  private void gridKeyWords_CellValidated(object sender, DataGridViewCellEventArgs e)
  {
  }

  private void tree_GetChildren(object sender, GetChildrenEventArgs e)
  {
  }

  private void tree_GetParent(object sender, GetParentEventArgs e)
  {
  }

  private void tree_SetCellValue(object sender, SetCellValueEventArgs e)
  {
    if (!this.BeforeUpDownEdit())
      e.Cancel = true;
    else if (this.schema.KeyWords.Contains(e.NewValue.ToString()))
    {
      e.Cancel = true;
    }
    else
    {
      this.schema.SetKeyWord(e.OldValue.ToString(), e.NewValue.ToString());
      this.AfterUpDownEdit();
      this.RefreshGrid();
    }
  }

  private void tree_SizeChanged(object sender, EventArgs e)
  {
  }

  private void tree_Click(object sender, EventArgs e)
  {
  }

  private void tree_GetCellData(object sender, GetCellDataEventArgs e)
  {
    if (e.Column != this.column)
      return;
    StyleDelta delta = new StyleDelta();
    if (this.schema.IsOwnWord((string) e.Row.Item))
      delta.Font = new Font(e.CellData.OddStyle.Font, FontStyle.Bold);
    e.CellData.OddStyle = new Style(e.Row.Tree.RowOddStyle, delta);
    e.CellData.EvenStyle = new Style(e.Row.Tree.RowEvenStyle, delta);
    e.CellData.Value = (object) (e.Row.Item as string);
  }

  private void tree_SortColumnChanged(object sender, EventArgs e) => this.RefreshGrid();
}
