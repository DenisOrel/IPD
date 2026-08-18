// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.FileStorage.FileStorageFilterForm
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Controls;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.DatabaseConfigurator.FileStorage;

internal class FileStorageFilterForm : Form
{
  public ConditionStructure[] ConditionStructures;
  private bool _sizeFilterEnabled;
  private bool _dateFilterEnabled;
  private IContainer components;
  private GroupBox groupBox1;
  private TextBox tbNote;
  private Label label2;
  private TextBox tbFileName;
  private Label label1;
  private GroupBox groupBox2;
  private GroupBox groupBox3;
  private CheckBox cbDateEnable;
  private Label label4;
  private Label label3;
  private Label label5;
  private Label label6;
  private DateTimePicker dtpDateDo;
  private DateTimePicker dtpDateTo;
  private CalcEdit ceSizeDo;
  private CalcEdit ceSizeTo;
  private System.Windows.Forms.ComboBox cbSizeMode;
  private CheckBox cbSizeEnable;
  private Button buttonOK;
  private Button buttonCancel;
  private Button buttonClear;

  public FileStorageFilterForm()
  {
    this.InitializeComponent();
    this.cbSizeMode.SelectedIndex = 0;
    this.cbSizeEnable.Checked = this._sizeFilterEnabled;
    this.ceSizeDo.Enabled = this._sizeFilterEnabled;
    this.ceSizeTo.Enabled = this._sizeFilterEnabled;
    this.cbSizeMode.Enabled = this._sizeFilterEnabled;
    this.cbDateEnable.Checked = this._dateFilterEnabled;
    this.dtpDateDo.Enabled = this._dateFilterEnabled;
    this.dtpDateTo.Enabled = this._dateFilterEnabled;
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1125);
  }

  private void buttonOK_Click(object sender, EventArgs e)
  {
    if (this.cbSizeEnable.Checked && (this.ceSizeTo.Value < 0M || this.ceSizeDo.Value < 0M))
    {
      int num = (int) IMMessageBox.Show(LocalizationHolder.rm.GetString("DatabaseConfigurator_221"), LocalizationHolder.rm.GetString("DatabaseConfigurator_222"), MessageBoxButtons.OK, IMMessageBoxImage.Error);
    }
    else
    {
      List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
      if (this.tbFileName.Text.Trim() != string.Empty)
        conditionStructureList.Add(new ConditionStructure(-71, RelationalOperators.Substring, (object) this.tbFileName.Text, LogicalOperators.AND, 0, false));
      if (this.tbNote.Text.Trim() != string.Empty)
        conditionStructureList.Add(new ConditionStructure(-38, RelationalOperators.Substring, (object) this.tbNote.Text, LogicalOperators.AND, 0, false));
      if (this.cbSizeEnable.Checked)
      {
        if (this.ceSizeDo.Value == 0M && this.ceSizeTo.Value > 0M)
          conditionStructureList.Add(new ConditionStructure(this.cbSizeMode.SelectedIndex == 0 ? -74 : -72, RelationalOperators.GreaterOrEqual, (object) this.ceSizeTo.Value, LogicalOperators.AND, 0, false));
        else if (this.ceSizeDo.Value == 0M && this.ceSizeTo.Value == 0M)
          conditionStructureList.Add(new ConditionStructure(this.cbSizeMode.SelectedIndex == 0 ? -74 : -72, RelationalOperators.Equal, (object) 0, LogicalOperators.AND, 0, false));
        else
          conditionStructureList.Add(new ConditionStructure(this.cbSizeMode.SelectedIndex == 0 ? -74 : -72, RelationalOperators.Between, (object) this.ceSizeTo.Value, (object) this.ceSizeDo.Value, LogicalOperators.AND, 0, false));
      }
      if (this.cbDateEnable.Checked)
      {
        DateTime dateTime1 = this.dtpDateTo.Value;
        DateTime dateTime2 = this.dtpDateDo.Value;
        conditionStructureList.Add(new ConditionStructure(-73, RelationalOperators.Between, (object) this.dtpDateTo.Value, (object) this.dtpDateDo.Value, LogicalOperators.AND, 0, false));
      }
      this.ConditionStructures = conditionStructureList.Count > 0 ? conditionStructureList.ToArray() : (ConditionStructure[]) null;
      this.DialogResult = DialogResult.OK;
      this.Close();
    }
  }

  private void buttonClear_Click(object sender, EventArgs e)
  {
    this.tbFileName.Text = string.Empty;
    this.tbNote.Text = string.Empty;
    this.ceSizeDo.Text = string.Empty;
    this.ceSizeTo.Text = string.Empty;
    this.cbSizeMode.SelectedIndex = 0;
    this.dtpDateDo.Value = DateTime.Now;
    this.dtpDateTo.Value = DateTime.Now;
    this.cbSizeEnable.Checked = false;
    this.cbDateEnable.Checked = false;
  }

  private void cbSizeEnable_CheckedChanged(object sender, EventArgs e)
  {
    this.ceSizeDo.Enabled = this.cbSizeEnable.Checked;
    this.ceSizeTo.Enabled = this.cbSizeEnable.Checked;
    this.cbSizeMode.Enabled = this.cbSizeEnable.Checked;
  }

  private void cbDateEnable_CheckedChanged(object sender, EventArgs e)
  {
    this.dtpDateDo.Enabled = this.cbDateEnable.Checked;
    this.dtpDateTo.Enabled = this.cbDateEnable.Checked;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FileStorageFilterForm));
    this.groupBox1 = new GroupBox();
    this.tbNote = new TextBox();
    this.label2 = new Label();
    this.tbFileName = new TextBox();
    this.label1 = new Label();
    this.groupBox2 = new GroupBox();
    this.cbDateEnable = new CheckBox();
    this.label4 = new Label();
    this.label3 = new Label();
    this.dtpDateDo = new DateTimePicker();
    this.dtpDateTo = new DateTimePicker();
    this.groupBox3 = new GroupBox();
    this.cbSizeMode = new System.Windows.Forms.ComboBox();
    this.cbSizeEnable = new CheckBox();
    this.ceSizeDo = new CalcEdit();
    this.ceSizeTo = new CalcEdit();
    this.label5 = new Label();
    this.label6 = new Label();
    this.buttonOK = new Button();
    this.buttonCancel = new Button();
    this.buttonClear = new Button();
    this.groupBox1.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.groupBox3.SuspendLayout();
    this.ceSizeDo.Properties.BeginInit();
    this.ceSizeTo.Properties.BeginInit();
    this.SuspendLayout();
    this.groupBox1.Controls.Add((Control) this.tbNote);
    this.groupBox1.Controls.Add((Control) this.label2);
    this.groupBox1.Controls.Add((Control) this.tbFileName);
    this.groupBox1.Controls.Add((Control) this.label1);
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.tbNote, "tbNote");
    this.tbNote.Name = "tbNote";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.tbFileName, "tbFileName");
    this.tbFileName.Name = "tbFileName";
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    this.groupBox2.Controls.Add((Control) this.cbDateEnable);
    this.groupBox2.Controls.Add((Control) this.label4);
    this.groupBox2.Controls.Add((Control) this.label3);
    this.groupBox2.Controls.Add((Control) this.dtpDateDo);
    this.groupBox2.Controls.Add((Control) this.dtpDateTo);
    componentResourceManager.ApplyResources((object) this.groupBox2, "groupBox2");
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.TabStop = false;
    componentResourceManager.ApplyResources((object) this.cbDateEnable, "cbDateEnable");
    this.cbDateEnable.Name = "cbDateEnable";
    this.cbDateEnable.UseVisualStyleBackColor = true;
    this.cbDateEnable.CheckedChanged += new EventHandler(this.cbDateEnable_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.label4, "label4");
    this.label4.Name = "label4";
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    componentResourceManager.ApplyResources((object) this.dtpDateDo, "dtpDateDo");
    this.dtpDateDo.Format = DateTimePickerFormat.Custom;
    this.dtpDateDo.Name = "dtpDateDo";
    componentResourceManager.ApplyResources((object) this.dtpDateTo, "dtpDateTo");
    this.dtpDateTo.Format = DateTimePickerFormat.Custom;
    this.dtpDateTo.Name = "dtpDateTo";
    this.groupBox3.Controls.Add((Control) this.cbSizeMode);
    this.groupBox3.Controls.Add((Control) this.cbSizeEnable);
    this.groupBox3.Controls.Add((Control) this.ceSizeDo);
    this.groupBox3.Controls.Add((Control) this.ceSizeTo);
    this.groupBox3.Controls.Add((Control) this.label5);
    this.groupBox3.Controls.Add((Control) this.label6);
    componentResourceManager.ApplyResources((object) this.groupBox3, "groupBox3");
    this.groupBox3.Name = "groupBox3";
    this.groupBox3.TabStop = false;
    this.cbSizeMode.DropDownStyle = ComboBoxStyle.DropDownList;
    componentResourceManager.ApplyResources((object) this.cbSizeMode, "cbSizeMode");
    this.cbSizeMode.FormattingEnabled = true;
    this.cbSizeMode.Items.AddRange(new object[2]
    {
      (object) componentResourceManager.GetString("cbSizeMode.Items"),
      (object) componentResourceManager.GetString("cbSizeMode.Items1")
    });
    this.cbSizeMode.Name = "cbSizeMode";
    componentResourceManager.ApplyResources((object) this.cbSizeEnable, "cbSizeEnable");
    this.cbSizeEnable.Name = "cbSizeEnable";
    this.cbSizeEnable.UseVisualStyleBackColor = true;
    this.cbSizeEnable.CheckedChanged += new EventHandler(this.cbSizeEnable_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.ceSizeDo, "ceSizeDo");
    this.ceSizeDo.Name = "ceSizeDo";
    this.ceSizeDo.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    componentResourceManager.ApplyResources((object) this.ceSizeTo, "ceSizeTo");
    this.ceSizeTo.Name = "ceSizeTo";
    this.ceSizeTo.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    componentResourceManager.ApplyResources((object) this.label5, "label5");
    this.label5.Name = "label5";
    componentResourceManager.ApplyResources((object) this.label6, "label6");
    this.label6.Name = "label6";
    componentResourceManager.ApplyResources((object) this.buttonOK, "buttonOK");
    this.buttonOK.Name = "buttonOK";
    this.buttonOK.UseVisualStyleBackColor = true;
    this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
    this.buttonCancel.DialogResult = DialogResult.Cancel;
    componentResourceManager.ApplyResources((object) this.buttonCancel, "buttonCancel");
    this.buttonCancel.Name = "buttonCancel";
    this.buttonCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.buttonClear, "buttonClear");
    this.buttonClear.Name = "buttonClear";
    this.buttonClear.UseVisualStyleBackColor = true;
    this.buttonClear.Click += new EventHandler(this.buttonClear_Click);
    this.AcceptButton = (IButtonControl) this.buttonOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.buttonCancel;
    this.Controls.Add((Control) this.buttonClear);
    this.Controls.Add((Control) this.buttonCancel);
    this.Controls.Add((Control) this.buttonOK);
    this.Controls.Add((Control) this.groupBox3);
    this.Controls.Add((Control) this.groupBox2);
    this.Controls.Add((Control) this.groupBox1);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (FileStorageFilterForm);
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.groupBox2.ResumeLayout(false);
    this.groupBox2.PerformLayout();
    this.groupBox3.ResumeLayout(false);
    this.groupBox3.PerformLayout();
    this.ceSizeDo.Properties.EndInit();
    this.ceSizeTo.Properties.EndInit();
    this.ResumeLayout(false);
  }
}
