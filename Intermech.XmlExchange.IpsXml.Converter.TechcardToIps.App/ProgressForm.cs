// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App.ProgressForm
// Assembly: Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EB4A0A0B-E62B-4D21-A944-3B5D877E45CE
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App;

public class ProgressForm : Form
{
  private IContainer components;
  private TableLayoutPanel tlpMain;
  private Button btnCancel;
  private ProgressBar PB;
  private Label lblCurrentStep;

  private void btnCancel_Click(object sender, EventArgs e) => this.CancellationToken?.Cancel();

  public ProgressForm() => this.InitializeComponent();

  public CancellationTokenSource CancellationToken { get; set; }

  public void InitProgress(int stepCount, string initMessage)
  {
    this.PB.Minimum = 0;
    this.PB.Maximum = stepCount;
    this.PB.Value = 0;
    this.lblCurrentStep.Text = initMessage;
  }

  public void DoProgress(int step, string message)
  {
    if (step > this.PB.Value && step < this.PB.Maximum)
      this.PB.Value = step;
    if (!(message != this.lblCurrentStep.Text))
      return;
    this.lblCurrentStep.Text = message;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.tlpMain = new TableLayoutPanel();
    this.btnCancel = new Button();
    this.PB = new ProgressBar();
    this.lblCurrentStep = new Label();
    this.tlpMain.SuspendLayout();
    this.SuspendLayout();
    this.tlpMain.ColumnCount = 1;
    this.tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tlpMain.Controls.Add((Control) this.btnCancel, 0, 2);
    this.tlpMain.Controls.Add((Control) this.PB, 0, 0);
    this.tlpMain.Controls.Add((Control) this.lblCurrentStep, 0, 1);
    this.tlpMain.Dock = DockStyle.Fill;
    this.tlpMain.Location = new Point(0, 0);
    this.tlpMain.Name = "tlpMain";
    this.tlpMain.RowCount = 3;
    this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
    this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 39f));
    this.tlpMain.Size = new Size(576, 91);
    this.tlpMain.TabIndex = 0;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Dock = DockStyle.Right;
    this.btnCancel.Location = new Point(496, 65);
    this.btnCancel.Margin = new Padding(0, 13, 5, 3);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(75, 23);
    this.btnCancel.TabIndex = 0;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    this.PB.Dock = DockStyle.Fill;
    this.PB.Location = new Point(5, 5);
    this.PB.Margin = new Padding(5);
    this.PB.Name = "PB";
    this.PB.Size = new Size(566, 20);
    this.PB.TabIndex = 1;
    this.lblCurrentStep.AutoSize = true;
    this.lblCurrentStep.Dock = DockStyle.Fill;
    this.lblCurrentStep.Location = new Point(3, 30);
    this.lblCurrentStep.Name = "lblCurrentStep";
    this.lblCurrentStep.Size = new Size(570, 22);
    this.lblCurrentStep.TabIndex = 2;
    this.lblCurrentStep.Text = "Начало конвертации";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(576, 91);
    this.Controls.Add((Control) this.tlpMain);
    this.Name = nameof (ProgressForm);
    this.Text = "Конвертация";
    this.tlpMain.ResumeLayout(false);
    this.tlpMain.PerformLayout();
    this.ResumeLayout(false);
  }
}
