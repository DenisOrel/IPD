// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.FindDialog
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Docking;
using Intermech.Interfaces.Client;
using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Editors;

public class FindDialog : Form
{
  private IFindTarget _target;
  private IContainer components;
  private Button btFind;
  private Button btCancel;
  private GroupBox groupBox1;
  private RadioButton rbBack;
  private RadioButton rbForward;
  private CheckBox cbWord;
  private CheckBox cbCase;
  private GroupBox groupBox2;
  private RadioButton rbFirst;
  private RadioButton rbCurrent;
  private Button btReplace;
  private Button btReplaceAll;
  private GroupBox groupBox3;
  private RadioButton rbSelected;
  private RadioButton rbAllrecords;
  private Label lbReplace;
  private TextBox tbReplace;
  private TextBox tbFind;
  private Label lbFind;
  private Tab tabReplace;
  private Tab tabFind;
  private Intermech.Docking.TabControl _tabs;

  public FindDialog()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 896);
  }

  private void OnSelectedTabChanged(object sender, EventArgs e)
  {
    bool flag = this._tabs.SelectedTab == this.tabReplace;
    this.lbReplace.Visible = this.tbReplace.Visible = flag;
    this.btReplace.Visible = flag;
    this.btReplaceAll.Visible = flag;
  }

  internal void ShowTab(bool replace)
  {
    this._tabs.SelectedTab = !replace ? this.tabFind : this.tabReplace;
    this.Show();
  }

  private void OnCancel_Click(object sender, EventArgs e) => this.CloseDialog();

  private void FindDialog_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (e.CloseReason != CloseReason.UserClosing)
      return;
    e.Cancel = true;
    this.CloseDialog();
  }

  private void CloseDialog()
  {
    EventHandler dialogClosed = this.DialogClosed;
    if (dialogClosed != null)
      dialogClosed((object) this, EventArgs.Empty);
    this.Hide();
  }

  internal IFindTarget Target
  {
    set => this._target = value;
  }

  private void OnFind_Click(object sender, EventArgs e) => this.PerformFind();

  private void OnReplace_Click(object sender, EventArgs e)
  {
    this.PerformReplace(FindReplaceOptions.None);
  }

  private void OnReplaceAll_Click(object sender, EventArgs e)
  {
    this.PerformReplace(FindReplaceOptions.RelaceAll);
  }

  private void PerformFind()
  {
    if (this._target == null)
      return;
    this._target.Find(new FindReplaceData()
    {
      _options = this.CollectOptions(),
      _findText = this.tbFind.Text,
      _replaceText = string.Empty
    });
  }

  private void PerformReplace(FindReplaceOptions options)
  {
    if (this._target == null)
      return;
    this._target.Replace(new FindReplaceData()
    {
      _options = this.CollectOptions() | options,
      _findText = this.tbFind.Text,
      _replaceText = this.tbReplace.Text
    });
  }

  private FindReplaceOptions CollectOptions()
  {
    FindReplaceOptions findReplaceOptions = FindReplaceOptions.None;
    if (this.cbCase.Checked)
      findReplaceOptions |= FindReplaceOptions.MatchCase;
    if (this.cbWord.Checked)
      findReplaceOptions |= FindReplaceOptions.WholeWord;
    if (this.rbSelected.Checked)
      findReplaceOptions |= FindReplaceOptions.Selected;
    if (this.rbBack.Checked)
      findReplaceOptions |= FindReplaceOptions.SearchUp;
    if (this.rbCurrent.Checked)
      findReplaceOptions |= FindReplaceOptions.FromCurrent;
    return findReplaceOptions;
  }

  internal event EventHandler DialogClosed;

  private void Options_CheckedChanged(object sender, EventArgs e)
  {
    if (this._target == null)
      return;
    this._target.ResetFindPos();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FindDialog));
    this.btFind = new Button();
    this.btCancel = new Button();
    this.groupBox1 = new GroupBox();
    this.rbBack = new RadioButton();
    this.rbForward = new RadioButton();
    this.cbWord = new CheckBox();
    this.cbCase = new CheckBox();
    this.groupBox2 = new GroupBox();
    this.rbFirst = new RadioButton();
    this.rbCurrent = new RadioButton();
    this.btReplace = new Button();
    this.btReplaceAll = new Button();
    this.lbFind = new Label();
    this.lbReplace = new Label();
    this.tbReplace = new TextBox();
    this.tbFind = new TextBox();
    this.groupBox3 = new GroupBox();
    this.rbSelected = new RadioButton();
    this.rbAllrecords = new RadioButton();
    this.tabReplace = new Tab();
    this.tabFind = new Tab();
    this._tabs = new Intermech.Docking.TabControl();
    this.groupBox1.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.groupBox3.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.btFind, "btFind");
    this.btFind.Name = "btFind";
    this.btFind.UseVisualStyleBackColor = true;
    this.btFind.Click += new EventHandler(this.OnFind_Click);
    this.btCancel.DialogResult = DialogResult.Cancel;
    componentResourceManager.ApplyResources((object) this.btCancel, "btCancel");
    this.btCancel.Name = "btCancel";
    this.btCancel.UseVisualStyleBackColor = true;
    this.btCancel.Click += new EventHandler(this.OnCancel_Click);
    this.groupBox1.Controls.Add((Control) this.rbBack);
    this.groupBox1.Controls.Add((Control) this.rbForward);
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.rbBack, "rbBack");
    this.rbBack.Name = "rbBack";
    this.rbBack.UseVisualStyleBackColor = true;
    this.rbBack.CheckedChanged += new EventHandler(this.Options_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbForward, "rbForward");
    this.rbForward.Checked = true;
    this.rbForward.Name = "rbForward";
    this.rbForward.TabStop = true;
    this.rbForward.UseVisualStyleBackColor = true;
    this.rbForward.CheckedChanged += new EventHandler(this.Options_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbWord, "cbWord");
    this.cbWord.Name = "cbWord";
    this.cbWord.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.cbCase, "cbCase");
    this.cbCase.Name = "cbCase";
    this.cbCase.UseVisualStyleBackColor = true;
    this.groupBox2.Controls.Add((Control) this.rbFirst);
    this.groupBox2.Controls.Add((Control) this.rbCurrent);
    componentResourceManager.ApplyResources((object) this.groupBox2, "groupBox2");
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.TabStop = false;
    componentResourceManager.ApplyResources((object) this.rbFirst, "rbFirst");
    this.rbFirst.Checked = true;
    this.rbFirst.Name = "rbFirst";
    this.rbFirst.TabStop = true;
    this.rbFirst.UseVisualStyleBackColor = true;
    this.rbFirst.CheckedChanged += new EventHandler(this.Options_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbCurrent, "rbCurrent");
    this.rbCurrent.Name = "rbCurrent";
    this.rbCurrent.TabStop = true;
    this.rbCurrent.UseVisualStyleBackColor = true;
    this.rbCurrent.CheckedChanged += new EventHandler(this.Options_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.btReplace, "btReplace");
    this.btReplace.Name = "btReplace";
    this.btReplace.UseVisualStyleBackColor = true;
    this.btReplace.Click += new EventHandler(this.OnReplace_Click);
    componentResourceManager.ApplyResources((object) this.btReplaceAll, "btReplaceAll");
    this.btReplaceAll.Name = "btReplaceAll";
    this.btReplaceAll.UseVisualStyleBackColor = true;
    this.btReplaceAll.Click += new EventHandler(this.OnReplaceAll_Click);
    componentResourceManager.ApplyResources((object) this.lbFind, "lbFind");
    this.lbFind.Name = "lbFind";
    componentResourceManager.ApplyResources((object) this.lbReplace, "lbReplace");
    this.lbReplace.Name = "lbReplace";
    componentResourceManager.ApplyResources((object) this.tbReplace, "tbReplace");
    this.tbReplace.Name = "tbReplace";
    componentResourceManager.ApplyResources((object) this.tbFind, "tbFind");
    this.tbFind.Name = "tbFind";
    this.groupBox3.Controls.Add((Control) this.rbSelected);
    this.groupBox3.Controls.Add((Control) this.rbAllrecords);
    componentResourceManager.ApplyResources((object) this.groupBox3, "groupBox3");
    this.groupBox3.Name = "groupBox3";
    this.groupBox3.TabStop = false;
    componentResourceManager.ApplyResources((object) this.rbSelected, "rbSelected");
    this.rbSelected.Name = "rbSelected";
    this.rbSelected.TabStop = true;
    this.rbSelected.UseVisualStyleBackColor = true;
    this.rbSelected.CheckedChanged += new EventHandler(this.Options_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbAllrecords, "rbAllrecords");
    this.rbAllrecords.Checked = true;
    this.rbAllrecords.Name = "rbAllrecords";
    this.rbAllrecords.TabStop = true;
    this.rbAllrecords.UseVisualStyleBackColor = true;
    this.rbAllrecords.CheckedChanged += new EventHandler(this.Options_CheckedChanged);
    this.tabReplace.Index = 1;
    componentResourceManager.ApplyResources((object) this.tabReplace, "tabReplace");
    this.tabFind.Index = 0;
    componentResourceManager.ApplyResources((object) this.tabFind, "tabFind");
    this._tabs.BorderStyle = Intermech.Docking.Rendering.BorderStyle.None;
    componentResourceManager.ApplyResources((object) this._tabs, "_tabs");
    this._tabs.Name = "_tabs";
    this._tabs.TabLayout = TabLayout.SingleLineFixed;
    this._tabs.Tabs.AddRange(new Tab[2]
    {
      this.tabFind,
      this.tabReplace
    });
    this._tabs.SelectedTabChanged += new EventHandler(this.OnSelectedTabChanged);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btCancel;
    this.Controls.Add((Control) this.lbFind);
    this.Controls.Add((Control) this.lbReplace);
    this.Controls.Add((Control) this._tabs);
    this.Controls.Add((Control) this.btReplace);
    this.Controls.Add((Control) this.groupBox3);
    this.Controls.Add((Control) this.groupBox1);
    this.Controls.Add((Control) this.cbWord);
    this.Controls.Add((Control) this.tbReplace);
    this.Controls.Add((Control) this.groupBox2);
    this.Controls.Add((Control) this.btReplaceAll);
    this.Controls.Add((Control) this.btCancel);
    this.Controls.Add((Control) this.cbCase);
    this.Controls.Add((Control) this.tbFind);
    this.Controls.Add((Control) this.btFind);
    this.DoubleBuffered = true;
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (FindDialog);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.FormClosing += new FormClosingEventHandler(this.FindDialog_FormClosing);
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.groupBox2.ResumeLayout(false);
    this.groupBox2.PerformLayout();
    this.groupBox3.ResumeLayout(false);
    this.groupBox3.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
