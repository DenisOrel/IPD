// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.FileImport.ImportManyDialog
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.FileImport;

internal class ImportManyDialog : Form
{
  private const string IMPORT_CATEGORY = "Импорт таблиц";
  private Exception _exception;
  private string _folderHead;
  private int _processedFolders;
  private int _processedTables;
  private string[] _subFolders;
  private string _tableHead;
  private IContainer components;
  private Button cancelButton;
  private BackgroundWorker backgroundWorker;
  private ProgressBar progressBar;
  private Label lbFolder;
  private Label lbTable;
  private Label lbActionName;

  public ImportManyDialog()
  {
    this.InitializeComponent();
    this._tableHead = this.lbTable.Text;
    this._folderHead = this.lbFolder.Text;
  }

  public ImportManyDialog(string[] subFolders)
    : this()
  {
    this._subFolders = subFolders;
  }

  internal void ProcessSubfolders(string path)
  {
    if (this.backgroundWorker.CancellationPending)
      return;
    Tuple<string, string> userState = new Tuple<string, string>(path, string.Empty);
    this._exception = (Exception) null;
    ++this._processedFolders;
    if (ImbaseTableImporter.CheckNeedFiles(path))
    {
      try
      {
        string str = ImbaseTableImporter.ProcessForTable(path, false);
        userState = new Tuple<string, string>(path, str);
        ++this._processedTables;
      }
      catch (Exception ex)
      {
        this._exception = ex;
      }
    }
    this.backgroundWorker.ReportProgress(1, (object) userState);
    string[] directories = Directory.GetDirectories(path);
    if (directories == null || directories.Length == 0)
      return;
    foreach (string path1 in directories)
    {
      if (this.backgroundWorker.CancellationPending)
        break;
      this.ProcessSubfolders(path1);
    }
  }

  private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
  {
    try
    {
      foreach (string subFolder in this._subFolders)
        this.ProcessSubfolders(subFolder);
    }
    catch (AbortException ex)
    {
    }
  }

  private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
  {
    this.progressBar.PerformStep();
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    if (e.UserState is Tuple<string, string> userState)
    {
      empty1 = userState.Item1;
      empty2 = userState.Item2;
    }
    if (e.ProgressPercentage == 1)
    {
      this.lbFolder.Text = this._folderHead + empty1;
      this.lbTable.Text = this._tableHead + empty2;
    }
    if (!(ServicesManager.GetService(typeof (IOutputView)) is IOutputView service))
      return;
    service.WriteString("Импорт таблиц", $"{this.lbFolder.Text}.{this.lbTable.Text}");
    if (this._exception == null)
      return;
    service.WriteString("Импорт таблиц", this._exception.Message);
    service.WriteString("Импорт таблиц", this._exception.StackTrace);
  }

  private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
  {
    this.Close();
    int num = (int) MessageBox.Show($"Обработано папок : {this._processedFolders}.{Environment.NewLine} Обработано таблиц : {this._processedTables}.", "Импорт таблиц", MessageBoxButtons.OK);
  }

  private int CalcSubFolders(string[] parentFolders)
  {
    int length = parentFolders.Length;
    foreach (string parentFolder in parentFolders)
    {
      string[] directories = Directory.GetDirectories(parentFolder);
      if (directories.Length != 0)
        length += this.CalcSubFolders(directories);
    }
    return length;
  }

  private void CancelButton_Click(object sender, EventArgs e)
  {
    this.backgroundWorker.CancelAsync();
  }

  private void ImportManyDialog_Shown(object sender, EventArgs e)
  {
    this.lbActionName.Text = "Подсчет общего количества таблиц...";
    Application.DoEvents();
    int num = this.CalcSubFolders(this._subFolders);
    this.lbActionName.Text = $"Общее количество подпапок :{num}.";
    this.progressBar.Maximum = num;
    this.progressBar.Step = 1;
    Application.DoEvents();
    this.backgroundWorker.RunWorkerAsync();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.cancelButton = new Button();
    this.backgroundWorker = new BackgroundWorker();
    this.progressBar = new ProgressBar();
    this.lbFolder = new Label();
    this.lbTable = new Label();
    this.lbActionName = new Label();
    this.SuspendLayout();
    this.cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.cancelButton.DialogResult = DialogResult.Cancel;
    this.cancelButton.Location = new Point(361, 104);
    this.cancelButton.Name = "cancelButton";
    this.cancelButton.Size = new Size(119, 23);
    this.cancelButton.TabIndex = 0;
    this.cancelButton.Text = "Остановить импорт";
    this.cancelButton.UseVisualStyleBackColor = true;
    this.cancelButton.Click += new EventHandler(this.CancelButton_Click);
    this.backgroundWorker.WorkerReportsProgress = true;
    this.backgroundWorker.WorkerSupportsCancellation = true;
    this.backgroundWorker.DoWork += new DoWorkEventHandler(this.BackgroundWorker_DoWork);
    this.backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(this.BackgroundWorker_ProgressChanged);
    this.backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.BackgroundWorker_RunWorkerCompleted);
    this.progressBar.Location = new Point(12, 59);
    this.progressBar.Name = "progressBar1";
    this.progressBar.Size = new Size(468, 18);
    this.progressBar.TabIndex = 1;
    this.lbFolder.AutoEllipsis = true;
    this.lbFolder.Location = new Point(9, 9);
    this.lbFolder.Name = "lbFolder";
    this.lbFolder.Size = new Size(471, 24);
    this.lbFolder.TabIndex = 2;
    this.lbFolder.Text = "Папка :";
    this.lbFolder.TextAlign = ContentAlignment.MiddleLeft;
    this.lbTable.AutoEllipsis = true;
    this.lbTable.Location = new Point(9, 33);
    this.lbTable.Name = "lbTable";
    this.lbTable.Size = new Size(471, 24);
    this.lbTable.TabIndex = 3;
    this.lbTable.Text = "Таблица :";
    this.lbTable.TextAlign = ContentAlignment.MiddleLeft;
    this.lbActionName.AutoSize = true;
    this.lbActionName.Location = new Point(12, 89);
    this.lbActionName.Name = "lbActionName";
    this.lbActionName.Size = new Size(0, 13);
    this.lbActionName.TabIndex = 4;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.cancelButton;
    this.ClientSize = new Size(492, 139);
    this.Controls.Add((Control) this.lbActionName);
    this.Controls.Add((Control) this.lbTable);
    this.Controls.Add((Control) this.lbFolder);
    this.Controls.Add((Control) this.progressBar);
    this.Controls.Add((Control) this.cancelButton);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ImportManyDialog);
    this.Padding = new Padding(9);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Импор таблиц";
    this.Shown += new EventHandler(this.ImportManyDialog_Shown);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
