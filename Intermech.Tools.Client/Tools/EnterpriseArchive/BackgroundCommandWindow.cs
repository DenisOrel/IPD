// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.BackgroundCommandWindow
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Mvp;
using Intermech.Mvp.Winforms;
using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive;

internal class BackgroundCommandWindow : MvpWindow, IBackgroundCommandView, IView
{
  private bool commandComplete;
  private IContainer components;
  private Button btAction;
  private ProgressBar pbProgressBar;
  private Label lbMessage;
  private TableLayoutPanel tableLayoutPanel1;
  private FlowLayoutPanel flowLayoutPanel1;

  public BackgroundCommandWindow() => this.InitializeComponent();

  private void btAction_Click(object sender, EventArgs e) => this.Close();

  private void ProgressForm_Load(object sender, EventArgs e)
  {
    this.commandComplete = false;
    this.btAction.Enabled = true;
  }

  private void ProgressForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (e.CloseReason != CloseReason.UserClosing || this.commandComplete)
      return;
    e.Cancel = MessageBox.Show("Вы действительно хотите прервать выполнение команды?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes;
  }

  void IBackgroundCommandView.SetCaption(string text) => this.Text = text;

  void IBackgroundCommandView.SetMessage(string text) => this.lbMessage.Text = text;

  void IBackgroundCommandView.EnableProgressBar(bool infinite)
  {
    this.pbProgressBar.Style = infinite ? ProgressBarStyle.Marquee : ProgressBarStyle.Blocks;
    this.pbProgressBar.Value = 0;
  }

  void IBackgroundCommandView.DisableProgressBar()
  {
    this.pbProgressBar.Style = ProgressBarStyle.Blocks;
    this.pbProgressBar.Value = this.pbProgressBar.Maximum;
  }

  void IBackgroundCommandView.SetProgress(double progress)
  {
    if (this.pbProgressBar.Style == ProgressBarStyle.Marquee)
      return;
    int num = (int) Math.Truncate(progress);
    if (num < 0)
      num = 0;
    else if (num > 100)
      num = 100;
    this.pbProgressBar.Value = num;
  }

  void IBackgroundCommandView.Hide()
  {
    if (!this.Visible)
      return;
    this.Hide();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (BackgroundCommandWindow));
    this.btAction = new Button();
    this.pbProgressBar = new ProgressBar();
    this.lbMessage = new Label();
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this.flowLayoutPanel1 = new FlowLayoutPanel();
    this.tableLayoutPanel1.SuspendLayout();
    this.flowLayoutPanel1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.btAction, "btAction");
    this.btAction.DialogResult = DialogResult.Cancel;
    this.btAction.Name = "btAction";
    this.btAction.UseVisualStyleBackColor = true;
    this.btAction.Click += new EventHandler(this.btAction_Click);
    componentResourceManager.ApplyResources((object) this.pbProgressBar, "pbProgressBar");
    this.pbProgressBar.Name = "pbProgressBar";
    this.pbProgressBar.Step = 2;
    componentResourceManager.ApplyResources((object) this.lbMessage, "lbMessage");
    this.lbMessage.Name = "lbMessage";
    componentResourceManager.ApplyResources((object) this.tableLayoutPanel1, "tableLayoutPanel1");
    this.tableLayoutPanel1.Controls.Add((Control) this.flowLayoutPanel1, 0, 2);
    this.tableLayoutPanel1.Controls.Add((Control) this.pbProgressBar, 0, 1);
    this.tableLayoutPanel1.Controls.Add((Control) this.lbMessage, 0, 0);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    componentResourceManager.ApplyResources((object) this.flowLayoutPanel1, "flowLayoutPanel1");
    this.flowLayoutPanel1.Controls.Add((Control) this.btAction);
    this.flowLayoutPanel1.Name = "flowLayoutPanel1";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btAction;
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.FormBorderStyle = FormBorderStyle.FixedSingle;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (BackgroundCommandWindow);
    this.ShowIcon = false;
    this.FormClosing += new FormClosingEventHandler(this.ProgressForm_FormClosing);
    this.Load += new EventHandler(this.ProgressForm_Load);
    this.tableLayoutPanel1.ResumeLayout(false);
    this.tableLayoutPanel1.PerformLayout();
    this.flowLayoutPanel1.ResumeLayout(false);
    this.flowLayoutPanel1.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
