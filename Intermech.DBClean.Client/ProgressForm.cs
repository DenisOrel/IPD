// Decompiled with JetBrains decompiler
// Type: Intermech.DBClean.Client.ProgressForm
// Assembly: Intermech.DBClean.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 973F13FD-72F3-4555-9BF9-74AC5C606885
// Assembly location: D:\IPS\Client\Intermech.DBClean.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.DBClean.Client.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.DBClean.Client;

/// <summary>Simple progress form.</summary>
public class ProgressForm : Form
{
  private BackgroundWorker worker;
  private int lastPercent;
  private string lastStatus;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label labelStatus;
  private ProgressBar progressBar;
  private Button buttonCancel;

  /// <summary>
  /// Gets the progress bar so it is possible to customize it
  /// before displaying the form.
  /// Do not use it directly from the background worker function!
  /// </summary>
  public ProgressBar ProgressBar => this.progressBar;

  /// <summary>Will be passed to the background worker.</summary>
  public object Argument { get; set; }

  /// <summary>
  /// Background worker's result.
  /// You may also check ShowDialog return value
  /// to know how the background worker finished.
  /// </summary>
  public RunWorkerCompletedEventArgs Result { get; private set; }

  /// <summary>
  /// True if the user clicked the Cancel button
  /// and the background worker is still running.
  /// </summary>
  public bool CancellationPending => this.worker.CancellationPending;

  /// <summary>Text displayed once the Cancel button is clicked.</summary>
  public string CancellingText { get; set; }

  /// <summary>Default status text.</summary>
  public string DefaultStatusText { get; set; }

  /// <summary>Occurs when the background worker starts.</summary>
  public event ProgressForm.DoWorkEventHandler DoWork;

  /// <summary>Constructor.</summary>
  public ProgressForm()
  {
    this.InitializeComponent();
    this.worker = new BackgroundWorker();
    this.worker.WorkerReportsProgress = true;
    this.worker.WorkerSupportsCancellation = true;
    this.worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.worker_DoWork);
    this.worker.ProgressChanged += new ProgressChangedEventHandler(this.worker_ProgressChanged);
    this.worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.worker_RunWorkerCompleted);
  }

  /// <summary>Changes the status text only.</summary>
  /// <param name="status">New status text.</param>
  public void SetProgress(string status)
  {
    if (!(status != this.lastStatus) || this.worker.CancellationPending)
      return;
    this.lastStatus = status;
    this.worker.ReportProgress(this.progressBar.Minimum - 1, (object) status);
  }

  /// <summary>Changes the progress bar value only.</summary>
  /// <param name="percent">New value for the progress bar.</param>
  public void SetProgress(int percent)
  {
    if (percent == this.lastPercent)
      return;
    this.lastPercent = percent;
    this.worker.ReportProgress(percent);
  }

  /// <summary>Changes both progress bar value and status text.</summary>
  /// <param name="percent">New value for the progress bar.</param>
  /// <param name="status">New status text.</param>
  public void SetProgress(int percent, string status)
  {
    if (percent == this.lastPercent && (!(status != this.lastStatus) || this.worker.CancellationPending))
      return;
    this.lastPercent = percent;
    this.lastStatus = status;
    this.worker.ReportProgress(percent, (object) status);
  }

  private void ProgressForm_Load(object sender, EventArgs e)
  {
    this.Result = (RunWorkerCompletedEventArgs) null;
    this.buttonCancel.Enabled = true;
    this.progressBar.Value = this.progressBar.Minimum;
    this.labelStatus.Text = this.DefaultStatusText;
    this.lastStatus = this.DefaultStatusText;
    this.lastPercent = this.progressBar.Minimum;
    this.worker.RunWorkerAsync(this.Argument);
  }

  private void buttonCancel_Click(object sender, EventArgs e)
  {
    this.worker.CancelAsync();
    this.buttonCancel.Enabled = false;
    this.labelStatus.Text = this.CancellingText;
  }

  private void worker_DoWork(object sender, DoWorkEventArgs e)
  {
    if (this.DoWork == null)
      return;
    this.DoWork(this, e);
  }

  private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
  {
    if (e.ProgressPercentage >= this.progressBar.Minimum && e.ProgressPercentage <= this.progressBar.Maximum)
      this.progressBar.Value = e.ProgressPercentage;
    if (e.UserState == null || this.worker.CancellationPending)
      return;
    this.labelStatus.Text = e.UserState.ToString();
  }

  private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
  {
    this.Result = e;
    if (e.Error != null)
      this.DialogResult = DialogResult.Abort;
    else if (e.Cancelled)
      this.DialogResult = DialogResult.Cancel;
    else
      this.DialogResult = DialogResult.OK;
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
    this.labelStatus = new Label();
    this.progressBar = new ProgressBar();
    this.buttonCancel = new Button();
    this.SuspendLayout();
    this.labelStatus.AutoSize = true;
    this.labelStatus.Location = new Point(12, 9);
    this.labelStatus.Name = "labelStatus";
    this.labelStatus.Size = new Size(130, 13);
    this.labelStatus.TabIndex = 0;
    this.labelStatus.Text = "Пожалуйста подождите:";
    this.progressBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.progressBar.Location = new Point(15, 32 /*0x20*/);
    this.progressBar.Name = "progressBar";
    this.progressBar.Size = new Size(385, 23);
    this.progressBar.TabIndex = 1;
    this.buttonCancel.Anchor = AnchorStyles.Bottom;
    this.buttonCancel.Location = new Point(169, 61);
    this.buttonCancel.Name = "buttonCancel";
    this.buttonCancel.Size = new Size(75, 23);
    this.buttonCancel.TabIndex = 3;
    this.buttonCancel.Text = "Отмена";
    this.buttonCancel.UseVisualStyleBackColor = true;
    this.buttonCancel.Visible = false;
    this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(412, 96 /*0x60*/);
    this.ControlBox = false;
    this.Controls.Add((Control) this.buttonCancel);
    this.Controls.Add((Control) this.progressBar);
    this.Controls.Add((Control) this.labelStatus);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.Name = nameof (ProgressForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Выполняется операция";
    this.Load += new EventHandler(this.ProgressForm_Load);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  /// <summary>Delegate for the DoWork event.</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">Contains the event data.</param>
  public delegate void DoWorkEventHandler(ProgressForm sender, DoWorkEventArgs e);
}
