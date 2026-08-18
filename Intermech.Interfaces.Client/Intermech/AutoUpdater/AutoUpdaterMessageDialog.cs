// Decompiled with JetBrains decompiler
// Type: Intermech.AutoUpdater.AutoUpdaterMessageDialog
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AutoUpdater;

public class AutoUpdaterMessageDialog : Form
{
  private bool autoCloseMode;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btOK;
  private Label lbDescription;
  private Timer tmAutoClose;
  private ProgressBar pbAutoClose;
  protected PictureBox pbUpdateIcon;

  public AutoUpdaterMessageDialog() => this.InitializeComponent();

  public bool AutoCloseMode
  {
    [DebuggerStepThrough] get => this.autoCloseMode;
    set => this.autoCloseMode = value;
  }

  public string MessageText
  {
    [DebuggerStepThrough] get => this.lbDescription.Text;
    [DebuggerStepThrough] set => this.lbDescription.Text = value;
  }

  private void UpdateAvailableDialog_Shown(object sender, EventArgs e)
  {
    if (!this.AutoCloseMode)
      return;
    this.tmAutoClose.Enabled = true;
  }

  private void UpdateAvailableDialog_FormClosed(object sender, FormClosedEventArgs e)
  {
    if (!this.tmAutoClose.Enabled)
      return;
    this.tmAutoClose.Enabled = false;
  }

  private void tmAutoClose_Tick(object sender, EventArgs e)
  {
    if (this.pbAutoClose.Value < this.pbAutoClose.Maximum)
      ++this.pbAutoClose.Value;
    if (this.pbAutoClose.Value < this.pbAutoClose.Maximum)
      return;
    this.Close();
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
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AutoUpdaterMessageDialog));
    this.btOK = new Button();
    this.lbDescription = new Label();
    this.tmAutoClose = new Timer(this.components);
    this.pbAutoClose = new ProgressBar();
    this.pbUpdateIcon = new PictureBox();
    ((ISupportInitialize) this.pbUpdateIcon).BeginInit();
    this.SuspendLayout();
    this.btOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btOK.DialogResult = DialogResult.OK;
    this.btOK.Location = new Point(215, 149);
    this.btOK.Name = "btOK";
    this.btOK.Size = new Size(95, 25);
    this.btOK.TabIndex = 0;
    this.btOK.Text = "OK";
    this.btOK.UseVisualStyleBackColor = true;
    this.lbDescription.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.lbDescription.Location = new Point(55, 30);
    this.lbDescription.Name = "lbDescription";
    this.lbDescription.Padding = new Padding(0, 2, 0, 0);
    this.lbDescription.Size = new Size(437, 62);
    this.lbDescription.TabIndex = 1;
    this.lbDescription.Text = "Message";
    this.tmAutoClose.Tick += new EventHandler(this.tmAutoClose_Tick);
    this.pbAutoClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.pbAutoClose.Location = new Point(15, 95);
    this.pbAutoClose.Maximum = 70;
    this.pbAutoClose.Name = "pbAutoClose";
    this.pbAutoClose.Size = new Size(477, 23);
    this.pbAutoClose.TabIndex = 2;
    this.pbUpdateIcon.Image = (Image) componentResourceManager.GetObject("pbUpdateIcon.Image");
    this.pbUpdateIcon.Location = new Point(12, 30);
    this.pbUpdateIcon.Margin = new Padding(3, 3, 8, 8);
    this.pbUpdateIcon.Name = "pbUpdateIcon";
    this.pbUpdateIcon.Size = new Size(32 /*0x20*/, 32 /*0x20*/);
    this.pbUpdateIcon.TabIndex = 3;
    this.pbUpdateIcon.TabStop = false;
    this.AcceptButton = (IButtonControl) this.btOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(504, 186);
    this.Controls.Add((Control) this.pbUpdateIcon);
    this.Controls.Add((Control) this.pbAutoClose);
    this.Controls.Add((Control) this.lbDescription);
    this.Controls.Add((Control) this.btOK);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MaximumSize = new Size(520, 225);
    this.MinimizeBox = false;
    this.MinimumSize = new Size(520, 225);
    this.Name = nameof (AutoUpdaterMessageDialog);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Caption";
    this.FormClosed += new FormClosedEventHandler(this.UpdateAvailableDialog_FormClosed);
    this.Shown += new EventHandler(this.UpdateAvailableDialog_Shown);
    ((ISupportInitialize) this.pbUpdateIcon).EndInit();
    this.ResumeLayout(false);
  }
}
