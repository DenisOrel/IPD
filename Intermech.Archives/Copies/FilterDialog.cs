// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Copies.FilterDialog
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Archives.Common;
using Intermech.Client.Core;
using Intermech.Kernel.Search;
using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Archives.Copies;

/// <summary>Для фильтрации документов по номеру ОТД</summary>
public class FilterDialog : Form
{
  /// <summary>набор условий для фильтрации</summary>
  public ConditionStructure Condition;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btnOk;
  private Button btnCancel;
  private Label label1;
  private TextBox tbFilter;
  private GroupBox groupBox1;
  private CheckBox cbFull;
  private CheckBox cbRegister;
  private Button btnClear;

  /// <summary>
  /// 
  /// </summary>
  public FilterDialog() => this.InitializeComponent();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="curCS"></param>
  public FilterDialog(ConditionStructure curCS)
  {
    this.InitializeComponent();
    if (!curCS.Equals((object) ConditionStructure.Empty))
    {
      this.tbFilter.Text = curCS.Value.ToString();
      this.cbRegister.Checked = curCS.CaseSensitive;
      this.cbFull.Checked = curCS.RelationalOperator == RelationalOperators.Equal;
    }
    this.btnClear.Enabled = this.tbFilter.Text != string.Empty;
  }

  /// <summary>очистим фильтр</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnClear_Click(object sender, EventArgs e)
  {
    this.tbFilter.Text = string.Empty;
    this.Condition = ConditionStructure.Empty;
  }

  /// <summary>сохраним фильтр</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnOk_Click(object sender, EventArgs e)
  {
    if (!(this.tbFilter.Text != string.Empty))
      return;
    bool caseSensitive = this.cbRegister.Checked;
    RelationalOperators relationalOperator = this.cbFull.Checked ? RelationalOperators.Equal : RelationalOperators.Substring;
    this.Condition = new ConditionStructure(ConstsHolder.InventoryNumberID, relationalOperator, (object) this.tbFilter.Text, LogicalOperators.AND, 0, caseSensitive);
  }

  private void btnCancel_Click(object sender, EventArgs e)
  {
    this.Condition = ConditionStructure.Empty;
  }

  private void tbFilter_TextChanged(object sender, EventArgs e)
  {
    this.cbFull.Enabled = this.cbRegister.Enabled = this.btnClear.Enabled = this.tbFilter.Text != string.Empty;
  }

  /// <summary>Восстановление размеров и положения формы</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void FilterDialog_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  /// <summary>Сохранение размеров и положения формы</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void FilterDialog_FormClosing(object sender, FormClosingEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FilterDialog));
    this.btnOk = new Button();
    this.btnCancel = new Button();
    this.label1 = new Label();
    this.tbFilter = new TextBox();
    this.groupBox1 = new GroupBox();
    this.cbFull = new CheckBox();
    this.cbRegister = new CheckBox();
    this.btnClear = new Button();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
    this.btnOk.DialogResult = DialogResult.OK;
    this.btnOk.Name = "btnOk";
    this.btnOk.UseVisualStyleBackColor = true;
    this.btnOk.Click += new EventHandler(this.btnOk_Click);
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.tbFilter, "tbFilter");
    this.tbFilter.Name = "tbFilter";
    this.tbFilter.TextChanged += new EventHandler(this.tbFilter_TextChanged);
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.Controls.Add((Control) this.cbFull);
    this.groupBox1.Controls.Add((Control) this.cbRegister);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.cbFull, "cbFull");
    this.cbFull.Name = "cbFull";
    this.cbFull.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.cbRegister, "cbRegister");
    this.cbRegister.Name = "cbRegister";
    this.cbRegister.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.btnClear, "btnClear");
    this.btnClear.Name = "btnClear";
    this.btnClear.UseVisualStyleBackColor = true;
    this.btnClear.Click += new EventHandler(this.btnClear_Click);
    this.AcceptButton = (IButtonControl) this.btnOk;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.btnClear);
    this.Controls.Add((Control) this.groupBox1);
    this.Controls.Add((Control) this.tbFilter);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOk);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (FilterDialog);
    this.ShowInTaskbar = false;
    this.FormClosing += new FormClosingEventHandler(this.FilterDialog_FormClosing);
    this.Load += new EventHandler(this.FilterDialog_Load);
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
