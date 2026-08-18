// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.ProgressForm
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Views;

public class ProgressForm : Form
{
  private string _processed = string.Empty;
  private ILoadDataAsync _source;
  private readonly SynchronizationContext _synchronizationContext;
  private IContainer components;
  private Button _btnCancel;
  private ProgressBar _progress;

  public object Data { get; set; }

  public ProgressForm(ILoadDataAsync source)
  {
    this.InitializeComponent();
    this._processed = LocalizationHolder.rm.GetString("Imbase.ProgressForm.Processed");
    this._btnCancel.Location = new Point(0, -10);
    this._synchronizationContext = SynchronizationContext.Current;
    this._source = source;
    this._source.SetProgress += new Action<int, int>(this.SetProgress);
  }

  protected override void OnShown(EventArgs e)
  {
    base.OnShown(e);
    this.LoadSourceAsync();
  }

  protected override void OnClosing(CancelEventArgs e)
  {
    this._source.SetProgress -= new Action<int, int>(this.SetProgress);
    base.OnClosing(e);
  }

  private async void LoadSourceAsync()
  {
    await Task.Run((Action) (() => this.LoadSource()));
    this.Close();
    this.DialogResult = DialogResult.OK;
  }

  private void LoadSource()
  {
    try
    {
      this.Data = this._source.LoadData();
    }
    catch (OperationCanceledException ex)
    {
    }
  }

  private void SetProgress(int count, int current)
  {
    this._synchronizationContext.Post(new SendOrPostCallback(this.SetProgress), (object) new int[2]
    {
      count,
      current
    });
  }

  private void SetProgress(object obj)
  {
    if (!(obj is int[] numArray))
      return;
    this._progress.Minimum = 0;
    this._progress.Maximum = numArray[0];
    this._progress.Value = numArray[1];
    this._progress.Refresh();
    string processed = this._processed;
    int maximum = this._progress.Value;
    string str1 = maximum.ToString();
    maximum = this._progress.Maximum;
    string str2 = maximum.ToString();
    this.Text = string.Format(processed, (object) str1, (object) str2);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ProgressForm));
    this._btnCancel = new Button();
    this._progress = new ProgressBar();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._progress, "_progress");
    this._progress.Name = "_progress";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._btnCancel;
    this.Controls.Add((Control) this._progress);
    this.Controls.Add((Control) this._btnCancel);
    this.DoubleBuffered = true;
    this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ProgressForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.ResumeLayout(false);
  }
}
