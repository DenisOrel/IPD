// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.ECOorCJForm
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client;

public class ECOorCJForm : Form
{
  private bool _includeToECO;
  private bool _includeToCJ;
  private bool require;
  private IContainer components;
  private Panel panel1;
  private Button btnOK;
  private Button btnCancel;
  private RadioButton rbECO;
  private RadioButton rbRecord;
  private RadioButton rbNone;
  private Label lblNoVersion;

  public bool IncludeToECO => this._includeToECO;

  public bool IncludeToCJ => this._includeToCJ;

  public ECOorCJForm() => this.InitializeComponent();

  public bool Execute(bool req)
  {
    this.require = req;
    return this.ShowDialog() == DialogResult.OK;
  }

  private void rbECO_CheckedChanged(object sender, EventArgs e)
  {
    if (!this.require)
      return;
    this.lblNoVersion.Visible = sender == this.rbNone && (sender as RadioButton).Checked;
  }

  private void btnOK_Click(object sender, EventArgs e)
  {
    this._includeToECO = this.rbECO.Checked;
    this._includeToCJ = this.rbRecord.Checked;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.panel1 = new Panel();
    this.lblNoVersion = new Label();
    this.btnOK = new Button();
    this.btnCancel = new Button();
    this.rbECO = new RadioButton();
    this.rbRecord = new RadioButton();
    this.rbNone = new RadioButton();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.lblNoVersion);
    this.panel1.Controls.Add((Control) this.btnOK);
    this.panel1.Controls.Add((Control) this.btnCancel);
    this.panel1.Dock = DockStyle.Bottom;
    this.panel1.Location = new Point(0, 85);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(500, 32 /*0x20*/);
    this.panel1.TabIndex = 0;
    this.lblNoVersion.AutoSize = true;
    this.lblNoVersion.Location = new Point(76, 10);
    this.lblNoVersion.Name = "lblNoVersion";
    this.lblNoVersion.Size = new Size(135, 13);
    this.lblNoVersion.TabIndex = 2;
    this.lblNoVersion.Text = "Версия не будет создана";
    this.lblNoVersion.Visible = false;
    this.btnOK.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Location = new Point(337, 5);
    this.btnOK.Name = "btnOK";
    this.btnOK.Size = new Size(75, 23);
    this.btnOK.TabIndex = 1;
    this.btnOK.Text = "Да";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnOK.Click += new EventHandler(this.btnOK_Click);
    this.btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(418, 5);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(75, 23);
    this.btnCancel.TabIndex = 0;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.rbECO.AutoSize = true;
    this.rbECO.Checked = true;
    this.rbECO.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.rbECO.Location = new Point(12, 12);
    this.rbECO.Name = "rbECO";
    this.rbECO.Size = new Size(163, 17);
    this.rbECO.TabIndex = 1;
    this.rbECO.TabStop = true;
    this.rbECO.Text = "Включить в извещение";
    this.rbECO.UseVisualStyleBackColor = true;
    this.rbECO.CheckedChanged += new EventHandler(this.rbECO_CheckedChanged);
    this.rbRecord.AutoSize = true;
    this.rbRecord.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.rbRecord.Location = new Point(12, 35);
    this.rbRecord.Name = "rbRecord";
    this.rbRecord.Size = new Size(209, 17);
    this.rbRecord.TabIndex = 2;
    this.rbRecord.Text = "Включить в журнал изменений";
    this.rbRecord.UseVisualStyleBackColor = true;
    this.rbRecord.CheckedChanged += new EventHandler(this.rbECO_CheckedChanged);
    this.rbNone.AutoSize = true;
    this.rbNone.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.rbNone.Location = new Point(12, 58);
    this.rbNone.Name = "rbNone";
    this.rbNone.Size = new Size(280, 17);
    this.rbNone.TabIndex = 3;
    this.rbNone.Text = "Не включать в контексты редактирования";
    this.rbNone.UseVisualStyleBackColor = true;
    this.rbNone.CheckedChanged += new EventHandler(this.rbECO_CheckedChanged);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(500, 117);
    this.Controls.Add((Control) this.rbNone);
    this.Controls.Add((Control) this.rbRecord);
    this.Controls.Add((Control) this.rbECO);
    this.Controls.Add((Control) this.panel1);
    this.FormBorderStyle = FormBorderStyle.FixedSingle;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ECOorCJForm);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Включить новую версию в контекст  редактирования?";
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
