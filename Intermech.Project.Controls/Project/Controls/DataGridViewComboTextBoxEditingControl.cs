// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.DataGridViewComboTextBoxEditingControl
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

internal class DataGridViewComboTextBoxEditingControl : 
  TextBox,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IComponent,
  IDisposable,
  IDataGridViewEditingControl,
  IPopupFormEditingControl
{
  [CanBeNull]
  private DataGridViewComboTextBoxEditingControl.ComboForm _comboForm;
  [CanBeNull]
  protected DataGridView _DataGridView;
  private string _displayColumn;
  protected int _RowIndex;
  private DataTable _source;
  protected bool _ValueChanged;
  private string _valueColumn;
  private int _width;

  public void ApplyCellStyleToEditingControl([NotNull] DataGridViewCellStyle dataGridViewCellStyle)
  {
    if (dataGridViewCellStyle.Font != null)
      this.Font = dataGridViewCellStyle.Font;
    this.ForeColor = dataGridViewCellStyle.ForeColor;
    this.BackColor = dataGridViewCellStyle.BackColor;
    this.TextAlign = DataGridViewComboTextBoxEditingControl.TranslateAlignment(dataGridViewCellStyle.Alignment);
  }

  private void dateTimeForm_VisibleChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this._comboForm == null || this._comboForm.Visible)
      return;
    this._comboForm.Close();
    this._comboForm = (DataGridViewComboTextBoxEditingControl.ComboForm) null;
    if (this._DataGridView == null)
      return;
    this._DataGridView.EndEdit();
  }

  public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
  {
    switch (keyData & Keys.KeyCode)
    {
      case Keys.Prior:
      case Keys.Next:
        if (this._ValueChanged)
          return true;
        break;
      case Keys.End:
      case Keys.Home:
        if (this.SelectionLength != this.Text.Length)
          return true;
        break;
      case Keys.Left:
        if (this.SelectionLength != 0 || this.SelectionStart != 0)
          return true;
        break;
      case Keys.Right:
        if (this.SelectionLength != 0 || this.SelectionStart != this.Text.Length)
          return true;
        break;
      case Keys.Delete:
        if (this.SelectionLength > 0 || this.SelectionStart < this.Text.Length)
          return true;
        break;
    }
    return !dataGridViewWantsInputKey;
  }

  [NotNull]
  public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
  {
    return (object) this.Text;
  }

  protected virtual void NotifyDataGridViewOfValueChange()
  {
    this._ValueChanged = true;
    if (this._DataGridView == null)
      return;
    this._DataGridView.NotifyCurrentCellDirty(true);
  }

  protected override void OnDoubleClick([NotNull] EventArgs e)
  {
    base.OnDoubleClick(e);
    this.ShowForm();
  }

  protected override void OnKeyDown(KeyEventArgs e)
  {
    base.OnKeyDown(e);
    if (e.KeyCode != Keys.F4 || e.Shift || e.Control || e.Alt)
      return;
    this.ShowForm();
  }

  protected override void OnTextChanged([NotNull] EventArgs e)
  {
    base.OnTextChanged(e);
    this.NotifyDataGridViewOfValueChange();
  }

  public void PrepareEditingControlForEdit(bool selectAll)
  {
    if (selectAll)
      this.SelectAll();
    else
      this.SelectionStart = this.Text.Length;
  }

  public void SetProperties(int width, [NotNull] DataTable source, [NotNull] string displayColumn, [NotNull] string valueColumn)
  {
    this._width = width;
    this._source = source;
    this._displayColumn = displayColumn;
    this._valueColumn = valueColumn;
  }

  public void ShowForm()
  {
    DataGridViewComboTextBoxEditingControl.ComboForm comboForm = this._comboForm;
    if ((comboForm != null ? (!comboForm.Visible ? 1 : 0) : 1) == 0)
      return;
    if (this._comboForm == null)
    {
      this._comboForm = new DataGridViewComboTextBoxEditingControl.ComboForm(this);
      this._comboForm.VisibleChanged += new EventHandler(this.dateTimeForm_VisibleChanged);
    }
    this._comboForm.Show();
    this._comboForm.Left -= 6;
    --this._comboForm.Left;
    try
    {
      Point location = this._comboForm.Location;
      Point point = location;
      point.Offset(this._comboForm.Width, this._comboForm.Height);
      Rectangle workingArea = Screen.GetWorkingArea((Control) this);
      if (location.X < workingArea.Left)
      {
        location.Offset(workingArea.Left - location.X, 0);
        this._comboForm.Location = location;
      }
      else if (point.X > workingArea.Right)
      {
        location.Offset(workingArea.Right - point.X, 0);
        this._comboForm.Location = location;
      }
      if (location.Y < workingArea.Top)
      {
        location.Offset(0, workingArea.Top - location.Y);
        this._comboForm.Location = location;
      }
      else
      {
        if (point.Y <= workingArea.Bottom)
          return;
        location.Offset(0, workingArea.Bottom - point.Y);
        this._comboForm.Location = location;
      }
    }
    catch (SecurityException ex)
    {
    }
  }

  private static HorizontalAlignment TranslateAlignment(DataGridViewContentAlignment align)
  {
    switch (align)
    {
      case DataGridViewContentAlignment.TopLeft:
      case DataGridViewContentAlignment.MiddleLeft:
      case DataGridViewContentAlignment.BottomLeft:
        return HorizontalAlignment.Left;
      case DataGridViewContentAlignment.TopCenter:
      case DataGridViewContentAlignment.MiddleCenter:
      case DataGridViewContentAlignment.BottomCenter:
        return HorizontalAlignment.Center;
      case DataGridViewContentAlignment.TopRight:
      case DataGridViewContentAlignment.MiddleRight:
      case DataGridViewContentAlignment.BottomRight:
        return HorizontalAlignment.Right;
      default:
        return HorizontalAlignment.Left;
    }
  }

  [CanBeNull]
  public DataGridView EditingControlDataGridView
  {
    get => this._DataGridView;
    set => this._DataGridView = value;
  }

  [NotNull]
  public object EditingControlFormattedValue
  {
    get => (object) this.Text;
    set
    {
      this.Text = value.ToString();
      this.NotifyDataGridViewOfValueChange();
    }
  }

  public int EditingControlRowIndex
  {
    get => this._RowIndex;
    set => this._RowIndex = value;
  }

  public bool EditingControlValueChanged
  {
    get => this._ValueChanged;
    set => this._ValueChanged = value;
  }

  [NotNull]
  public Cursor EditingPanelCursor => Cursors.IBeam;

  public bool RepositionEditingControlOnValueChange => false;

  internal class ComboForm : Form
  {
    private readonly DataGridViewComboTextBoxEditingControl _control;
    private bool _duringSet;
    private readonly ListBox _lb;

    public ComboForm([NotNull] DataGridViewComboTextBoxEditingControl control)
    {
      this._control = control;
      this.ShowInTaskbar = false;
      this.TopMost = true;
      this.StartPosition = FormStartPosition.Manual;
      this.FormBorderStyle = FormBorderStyle.None;
      Point location = control.Location;
      location.Offset(0, control.Height);
      this.Location = control.PointToScreen(location);
      this.BackColor = SystemColors.ControlDark;
      this.Padding = new Padding(1);
      this._lb = new ListBox();
      this._lb.BorderStyle = BorderStyle.None;
      this._lb.Click += new EventHandler(this.lb_Click);
      this._lb.KeyDown += new KeyEventHandler(this.mc_KeyDown);
      this._lb.SelectedIndexChanged += new EventHandler(this.lb_SelectedIndexChanged);
      this.Size = this._lb.PreferredSize;
      this.Set();
      this.Controls.Add((Control) this._lb);
      this._lb.Dock = DockStyle.Fill;
    }

    private void HideForm() => this.Hide();

    private void lb_Click([CanBeNull] object sender, [NotNull] EventArgs e) => this.HideForm();

    private void lb_SelectedIndexChanged([CanBeNull] object sender, [NotNull] EventArgs e)
    {
      if (this._lb?.SelectedValue == null)
        return;
      if (this._duringSet)
        return;
      try
      {
        if (this._control?._valueColumn == null || !(this._lb.SelectedItem is DataRowView selectedItem))
          return;
        this._control.Text = selectedItem[this._control._valueColumn]?.ToString() ?? string.Empty;
      }
      catch (ObjectDisposedException ex)
      {
      }
    }

    private void mc_KeyDown([CanBeNull] object sender, [NotNull] KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Return && e.KeyCode != Keys.Escape && e.KeyCode != Keys.F4)
        return;
      this.HideForm();
    }

    protected override void OnDeactivate([NotNull] EventArgs e)
    {
      base.OnDeactivate(e);
      this.HideForm();
    }

    protected override void OnShown([NotNull] EventArgs e)
    {
      base.OnShown(e);
      if (this._control != null && this._lb != null)
      {
        int width = this._control._width + 1;
        int height1 = this._lb.Height;
        Padding padding = this.Padding;
        int top = padding.Top;
        int num = height1 + top;
        padding = this.Padding;
        int bottom = padding.Bottom;
        int height2 = num + bottom;
        this.Size = new Size(width, height2);
      }
      this.Set();
    }

    private void Set()
    {
      this._duringSet = true;
      if (this._lb != null)
      {
        this._lb.DataSource = (object) this._control?._source;
        this._lb.DisplayMember = this._control?._displayColumn ?? string.Empty;
        this._lb.ValueMember = this._control?._valueColumn ?? string.Empty;
        if (this._control?._valueColumn != null)
        {
          foreach (DataRowView dataRowView in this._lb.Items)
          {
            if (dataRowView[this._control._valueColumn]?.ToString() == this._control.Text)
            {
              this._lb.SelectedItem = (object) dataRowView;
              break;
            }
          }
        }
      }
      this._duringSet = false;
    }
  }
}
