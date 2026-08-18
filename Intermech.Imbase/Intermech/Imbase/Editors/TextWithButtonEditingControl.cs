// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.TextWithButtonEditingControl
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Editors;

public class TextWithButtonEditingControl : UserControl, IDataGridViewEditingControl
{
  private DataGridView _dataGridView;
  private bool _valueChanged;
  private int _rowIndex;
  private TransparentTextBox _txt = new TransparentTextBox();
  private IContainer components;
  private ImageList _imgList;
  private Button _btn;

  public TextWithButtonEditingControl()
  {
    this.InitializeComponent();
    this._txt.Dock = DockStyle.Fill;
    this._txt.Name = nameof (_txt);
    this._txt.TabIndex = 0;
    this._txt.TextChanged += new EventHandler(this.On_txt_TextChanged);
    this.Controls.Add((Control) this._txt);
  }

  public string Value
  {
    get => this._txt.Text;
    set => this._txt.Text = value;
  }

  private void On_btn_Click(object sender, EventArgs e)
  {
    if (this.EditingControlDataGridView == null || !(this.EditingControlDataGridView.CurrentCell is TextWithButtonCell currentCell))
      return;
    (currentCell.OwningColumn as TextWithButtonColumn).OnButtonClick((object) currentCell);
  }

  private void On_txt_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Delete && e.KeyCode != Keys.Back)
      return;
    this._txt.Text = string.Empty;
  }

  private void On_txt_TextChanged(object sender, EventArgs e)
  {
    this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
  }

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

  public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
  {
    return this.EditingControlFormattedValue;
  }

  public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
  {
    this._txt.Font = dataGridViewCellStyle.Font;
    this._txt.ForeColor = dataGridViewCellStyle.ForeColor;
    this._txt.BackColor = dataGridViewCellStyle.BackColor;
  }

  public int EditingControlRowIndex
  {
    get => this._rowIndex;
    set => this._rowIndex = value;
  }

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

  public void PrepareEditingControlForEdit(bool selectAll)
  {
  }

  public bool RepositionEditingControlOnValueChange => false;

  public DataGridView EditingControlDataGridView
  {
    get => this._dataGridView;
    set
    {
      this._dataGridView = value;
      this.ReadParentSettings();
    }
  }

  public bool EditingControlValueChanged
  {
    get => this._valueChanged;
    set => this._valueChanged = value;
  }

  public Cursor EditingPanelCursor => this.Cursor;

  private void ReadParentSettings()
  {
    if (this.EditingControlDataGridView == null || !(this.EditingControlDataGridView.CurrentCell is TextWithButtonCell) || !(this.EditingControlDataGridView.CurrentCell is TextWithButtonCell currentCell) || !(currentCell.OwningColumn is TextWithButtonColumn))
      return;
    this._txt.ReadOnly = (currentCell.OwningColumn as TextWithButtonColumn).TextReadOnly;
    if (!this._txt.ReadOnly)
      return;
    this._txt.KeyDown += new KeyEventHandler(this.On_txt_KeyDown);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TextWithButtonEditingControl));
    this._imgList = new ImageList(this.components);
    this._btn = new Button();
    this.SuspendLayout();
    this._imgList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("_imgList.ImageStream");
    this._imgList.TransparentColor = Color.Magenta;
    this._imgList.Images.SetKeyName(0, "dots.bmp");
    componentResourceManager.ApplyResources((object) this._btn, "_btn");
    this._btn.BackColor = Color.Transparent;
    this._btn.ImageList = this._imgList;
    this._btn.Name = "_btn";
    this._btn.UseVisualStyleBackColor = false;
    this._btn.Click += new EventHandler(this.On_btn_Click);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._btn);
    this.Name = nameof (TextWithButtonEditingControl);
    this.ResumeLayout(false);
  }
}
