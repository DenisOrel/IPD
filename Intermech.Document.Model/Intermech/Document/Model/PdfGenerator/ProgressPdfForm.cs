// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.PdfGenerator.ProgressPdfForm
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Model.PdfGenerator;

public class ProgressPdfForm : Form
{
  private BackgroundWorker worker;
  private object argument;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ProgressBar progressBar1;
  private Button bCancel;
  private Label label1;

  public ProgressPdfForm(BackgroundWorker worker, int totalpages, object argument)
  {
    this.InitializeComponent();
    this.worker = worker;
    worker.ProgressChanged += new ProgressChangedEventHandler(this.worker_ProgressChanged);
    worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.worker_RunWorkerCompleted);
    this.progressBar1.Maximum = totalpages;
    this.progressBar1.Value = 0;
    this.label1.Text = $"{0} из {this.progressBar1.Maximum}";
    this.argument = argument;
  }

  private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
  {
    this.Close();
  }

  private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
  {
    this.progressBar1.Value = e.ProgressPercentage;
    this.label1.Text = $"{e.ProgressPercentage} из {this.progressBar1.Maximum}";
  }

  protected override void OnShown(EventArgs e)
  {
    base.OnShown(e);
    this.worker.RunWorkerAsync(this.argument);
  }

  private void bCancel_Click(object sender, EventArgs e) => this.worker.CancelAsync();

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
    this.progressBar1 = new ProgressBar();
    this.bCancel = new Button();
    this.label1 = new Label();
    this.SuspendLayout();
    this.progressBar1.Location = new Point(12, 29);
    this.progressBar1.Name = "progressBar1";
    this.progressBar1.Size = new Size(299, 23);
    this.progressBar1.TabIndex = 0;
    this.progressBar1.Value = 20;
    this.bCancel.Location = new Point(119, 61);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(75, 23);
    this.bCancel.TabIndex = 1;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.bCancel.Click += new EventHandler(this.bCancel_Click);
    this.label1.AutoSize = true;
    this.label1.Location = new Point(12, 9);
    this.label1.Name = "label1";
    this.label1.Size = new Size(35, 13);
    this.label1.TabIndex = 2;
    this.label1.Text = "label1";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(323, 92);
    this.ControlBox = false;
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.bCancel);
    this.Controls.Add((Control) this.progressBar1);
    this.Name = nameof (ProgressPdfForm);
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Сохранение в pdf";
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
