// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.LabelWithButtonEditingControl
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Editors;

public class LabelWithButtonEditingControl : UserControl, IDataGridViewEditingControl
{
  private DataGridView _dataGridView;
  private bool _valueChanged;
  private int _rowIndex;
  private IContainer components;
  private ImageList _imgList;
  private Button _btn;
  private Label _lb;

  public LabelWithButtonEditingControl() => this.InitializeComponent();

  public string Value
  {
    get => this._lb.Text;
    set => this._lb.Text = value;
  }

  private void On_btn_Click(object sender, EventArgs e)
  {
    if (this.EditingControlDataGridView == null || !(this.EditingControlDataGridView.CurrentCell is LabelWithButtonCell currentCell))
      return;
    (currentCell.OwningColumn as LabelWithButtonColumn).OnButtonClick((object) currentCell);
  }

  private void On_lb_TextChanged(object sender, EventArgs e)
  {
    this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
  }

  public object EditingControlFormattedValue
  {
    get => (object) this._lb.Text;
    set
    {
      if (!(value is string str))
        return;
      this._lb.Text = str.ToString();
    }
  }

  public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
  {
    return this.EditingControlFormattedValue;
  }

  public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
  {
    this._lb.Font = dataGridViewCellStyle.Font;
    this._lb.ForeColor = dataGridViewCellStyle.ForeColor;
    this._lb.BackColor = dataGridViewCellStyle.BackColor;
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
        return false;
    }
  }

  public void PrepareEditingControlForEdit(bool selectAll)
  {
  }

  public bool RepositionEditingControlOnValueChange => false;

  public DataGridView EditingControlDataGridView
  {
    get => this._dataGridView;
    set => this._dataGridView = value;
  }

  public bool EditingControlValueChanged
  {
    get => this._valueChanged;
    set => this._valueChanged = value;
  }

  public Cursor EditingPanelCursor => this.Cursor;

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (LabelWithButtonEditingControl));
    this._imgList = new ImageList(this.components);
    this._btn = new Button();
    this._lb = new Label();
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
    componentResourceManager.ApplyResources((object) this._lb, "_lb");
    this._lb.Name = "_lb";
    this._lb.TextChanged += new EventHandler(this.On_lb_TextChanged);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._lb);
    this.Controls.Add((Control) this._btn);
    this.Name = nameof (LabelWithButtonEditingControl);
    this.ResumeLayout(false);
  }
}
