
// Type: Intermech.PropertyEditors.FileAttributeProgressForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

public class FileAttributeProgressForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ProgressBar pbFile;
  private ProgressBar pbAll;
  private Button btnCancel;
  private TextBox tbFile;

  public FileAttributeProgressForm()
  {
    this.InitializeComponent();
    this.InitControlProperties((Control) this.tbFile);
  }

  private void InitControlProperties(Control control)
  {
    control.BackColor = this.BackColor;
    control.ForeColor = this.ForeColor;
    control.Font = this.Font;
  }

  /// <summary>событие прерывания процесса</summary>
  public event BreakEvent Break;

  private void btnCancel_Click(object sender, EventArgs e)
  {
    if (this.Break == null)
      return;
    this.Break((object) this);
  }

  public int FileCount
  {
    get => this.pbAll.Value;
    set => this.pbAll.Value = value;
  }

  public int FileCountMaximum
  {
    get => this.pbAll.Maximum;
    set => this.pbAll.Maximum = value;
  }

  public string FileName
  {
    get => this.tbFile.Text;
    set => this.tbFile.Text = value;
  }

  public int FileProgress
  {
    get => this.pbFile.Value;
    set => this.pbFile.Value = value;
  }

  /// <summary>Показать форму</summary>
  /// <param name="caption">заголовок окна</param>
  /// <param name="maxFileCount">максимальное количество обрабатываемых файлов</param>
  public void ShowProgress(string caption, int maxFileCount)
  {
    this.Text = "IPS: " + caption;
    this.FileCount = 0;
    this.FileCountMaximum = maxFileCount;
    this.FileName = string.Empty;
    this.FileProgress = 0;
    this.Show();
  }

  /// <summary>Скрыть форму</summary>
  public void HideProgress() => this.Hide();

  public void NewFileProgress(string name)
  {
    this.FileName = name;
    this.FileProgress = 0;
    ++this.FileCount;
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
    this.pbFile = new ProgressBar();
    this.pbAll = new ProgressBar();
    this.btnCancel = new Button();
    this.tbFile = new TextBox();
    this.SuspendLayout();
    this.pbFile.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.pbFile.Location = new Point(12, 26);
    this.pbFile.Name = "pbFile";
    this.pbFile.Size = new Size(496, 23);
    this.pbFile.TabIndex = 1;
    this.pbAll.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.pbAll.Location = new Point(12, 55);
    this.pbAll.Name = "pbAll";
    this.pbAll.Size = new Size(496, 23);
    this.pbAll.TabIndex = 2;
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.Location = new Point(433, 117);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(75, 23);
    this.btnCancel.TabIndex = 3;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    this.tbFile.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbFile.BorderStyle = BorderStyle.None;
    this.tbFile.Location = new Point(12, 84);
    this.tbFile.Multiline = true;
    this.tbFile.Name = "tbFile";
    this.tbFile.ReadOnly = true;
    this.tbFile.Size = new Size(496, 27);
    this.tbFile.TabIndex = 4;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(520, 153);
    this.Controls.Add((Control) this.tbFile);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.pbAll);
    this.Controls.Add((Control) this.pbFile);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (FileAttributeProgressForm);
    this.StartPosition = FormStartPosition.CenterParent;
    this.TopMost = true;
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
