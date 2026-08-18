
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.DataGridViewTextWithButtonEditingControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Windows.Forms;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode;

public class DataGridViewTextWithButtonEditingControl : UserControl, IDataGridViewEditingControl
{
  private DataGridView _dataGridView;
  private bool _valueChanged;
  private int _rowIndex;
  private TextBox _txt = new TextBox();
  private Button _button = new Button();

  /// <summary>Конструктор.</summary>
  public DataGridViewTextWithButtonEditingControl()
  {
    this._txt.Dock = DockStyle.Fill;
    this._txt.Name = nameof (_txt);
    this._txt.TabIndex = 0;
    this._txt.ReadOnly = true;
    this._txt.TextChanged += new EventHandler(this.On_txt_TextChanged);
    this._txt.KeyDown += new KeyEventHandler(this.On_txt_KeyDown);
    this.Controls.Add((Control) this._txt);
    this._button.Dock = DockStyle.Right;
    this._button.Name = nameof (_button);
    this._button.TabIndex = 1;
    this._button.Text = "...";
    this._button.Width = 27;
    this._button.Click += new EventHandler(this.On_btn_Click);
    this.Controls.Add((Control) this._button);
  }

  /// <summary>Текст лэйбы.</summary>
  public string Value
  {
    get => this._txt.Text;
    set => this._txt.Text = value;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btn_Click(object sender, EventArgs e)
  {
    if (this.EditingControlDataGridView == null || !(this.EditingControlDataGridView.CurrentCell is DataGridViewTextWithButtonCell currentCell))
      return;
    (currentCell.OwningColumn as DataGridViewTextWithButtonColumn).OnButtonClick((object) currentCell);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_KeyDown(object sender, KeyEventArgs e)
  {
    if (this.EditingControlDataGridView == null || !(this.EditingControlDataGridView.CurrentCell is DataGridViewTextWithButtonCell currentCell) || e.KeyCode != Keys.Delete && e.KeyCode != Keys.Back)
      return;
    this._txt.Text = string.Empty;
    (currentCell.OwningColumn as DataGridViewTextWithButtonColumn).OnKeyDown((object) currentCell);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_TextChanged(object sender, EventArgs e)
  {
    this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
  }

  /// <summary>
  /// 
  /// </summary>
  public object EditingControlFormattedValue
  {
    get => (object) this._txt.Text;
    set
    {
      if (!(value is string str))
        return;
      this._txt.Text = str.ToString();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
  {
    return this.EditingControlFormattedValue;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="dataGridViewCellStyle"></param>
  public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
  {
    this._txt.Font = dataGridViewCellStyle.Font;
    this._txt.ForeColor = dataGridViewCellStyle.ForeColor;
    this._txt.BackColor = dataGridViewCellStyle.BackColor;
  }

  /// <summary>
  /// 
  /// </summary>
  public int EditingControlRowIndex
  {
    get => this._rowIndex;
    set => this._rowIndex = value;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="key"></param>
  /// <param name="dataGridViewWantsInputKey"></param>
  /// <returns></returns>
  public bool EditingControlWantsInputKey(Keys key, bool dataGridViewWantsInputKey)
  {
    switch (key & Keys.KeyCode)
    {
      case Keys.Prior:
      case Keys.Next:
      case Keys.End:
      case Keys.Home:
      case Keys.Left:
      case Keys.Up:
      case Keys.Right:
      case Keys.Down:
        return true;
      default:
        return !dataGridViewWantsInputKey;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="selectAll"></param>
  public void PrepareEditingControlForEdit(bool selectAll)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  public bool RepositionEditingControlOnValueChange => false;

  /// <summary>
  /// 
  /// </summary>
  public DataGridView EditingControlDataGridView
  {
    get => this._dataGridView;
    set
    {
      this._dataGridView = value;
      this.ReadParentSettings();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public bool EditingControlValueChanged
  {
    get => this._valueChanged;
    set => this._valueChanged = value;
  }

  /// <summary>
  /// 
  /// </summary>
  public Cursor EditingPanelCursor => this.Cursor;

  /// <summary>Инициализация текстового контрола.</summary>
  private void ReadParentSettings()
  {
    if (this.EditingControlDataGridView == null || !(this.EditingControlDataGridView.CurrentCell is DataGridViewTextWithButtonCell) || !(this.EditingControlDataGridView.CurrentCell is DataGridViewTextWithButtonCell currentCell) || !(currentCell.OwningColumn is DataGridViewTextWithButtonColumn))
      return;
    this._txt.ReadOnly = (currentCell.OwningColumn as DataGridViewTextWithButtonColumn).TextReadOnly;
    if (!this._txt.ReadOnly)
      return;
    this._txt.KeyDown += new KeyEventHandler(this.On_txt_KeyDown);
  }
}
