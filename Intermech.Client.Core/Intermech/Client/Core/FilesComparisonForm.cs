
// Type: Intermech.Client.Core.FilesComparisonForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core;

public class FilesComparisonForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  protected Panel panel1;
  protected Button btnClose;
  protected UCFilesComparison ucFilesComparison;

  public FilesComparisonForm() => this.InitializeComponent();

  public void Init(ObjectFileInfo fileInfo1, ObjectFileInfo fileInfo2)
  {
    this.ucFilesComparison.Init(fileInfo1, fileInfo2);
  }

  private void btnClose_Click(object sender, EventArgs e) => this.Close();

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
    this.ucFilesComparison = new UCFilesComparison();
    this.panel1 = new Panel();
    this.btnClose = new Button();
    this.ucFilesComparison.SuspendLayout();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.ucFilesComparison.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.ucFilesComparison.Location = new Point(0, 0);
    this.ucFilesComparison.Name = "ucFilesComparison";
    this.panel1.BackColor = SystemColors.Control;
    this.panel1.Controls.Add((Control) this.btnClose);
    this.panel1.Dock = DockStyle.Bottom;
    this.panel1.Location = new Point(0, 163);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(694, 43);
    this.panel1.TabIndex = 1;
    this.btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnClose.DialogResult = DialogResult.OK;
    this.btnClose.Location = new Point(571, 6);
    this.btnClose.Name = "btnClose";
    this.btnClose.Size = new Size(116, 30);
    this.btnClose.TabIndex = 0;
    this.btnClose.Text = "Закрыть";
    this.btnClose.UseVisualStyleBackColor = true;
    this.btnClose.Click += new EventHandler(this.btnClose_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackColor = SystemColors.ActiveBorder;
    this.ClientSize = new Size(694, 206);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.ucFilesComparison);
    this.Name = nameof (FilesComparisonForm);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Сравнение файлов";
    this.ucFilesComparison.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
