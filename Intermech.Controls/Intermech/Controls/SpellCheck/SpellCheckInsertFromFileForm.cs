
// Type: Intermech.Controls.SpellCheck.SpellCheckInsertFromFileForm
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace Intermech.Controls.SpellCheck;

public class SpellCheckInsertFromFileForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button bSelectFile;
  private RichTextBox richTextBox1;
  private Button bAdd;
  private Button bCancel;

  public SpellCheckInsertFromFileForm() => this.InitializeComponent();

  private void bSelectFile_Click(object sender, EventArgs e)
  {
    OpenFileDialog openFileDialog = new OpenFileDialog();
    openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
    if (openFileDialog.ShowDialog() != DialogResult.OK)
      return;
    this.richTextBox1.Lines = File.ReadAllLines(openFileDialog.FileName);
  }

  public string[] Lines => this.richTextBox1.Lines;

  public string FileText => this.richTextBox1.Text;

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
    this.bSelectFile = new Button();
    this.richTextBox1 = new RichTextBox();
    this.bAdd = new Button();
    this.bCancel = new Button();
    this.SuspendLayout();
    this.bSelectFile.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.bSelectFile.Location = new Point(17, 7);
    this.bSelectFile.Name = "bSelectFile";
    this.bSelectFile.Size = new Size(501, 23);
    this.bSelectFile.TabIndex = 0;
    this.bSelectFile.Text = "Выбрать файл";
    this.bSelectFile.UseVisualStyleBackColor = true;
    this.bSelectFile.Click += new EventHandler(this.bSelectFile_Click);
    this.richTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.richTextBox1.Location = new Point(17, 39);
    this.richTextBox1.Name = "richTextBox1";
    this.richTextBox1.Size = new Size(501, 425);
    this.richTextBox1.TabIndex = 1;
    this.richTextBox1.Text = "";
    this.bAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bAdd.DialogResult = DialogResult.OK;
    this.bAdd.Location = new Point(362, 473);
    this.bAdd.Name = "bAdd";
    this.bAdd.Size = new Size(75, 23);
    this.bAdd.TabIndex = 3;
    this.bAdd.Text = "Добавить";
    this.bAdd.UseVisualStyleBackColor = true;
    this.bCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(443, 473);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(75, 23);
    this.bCancel.TabIndex = 4;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(530, 509);
    this.Controls.Add((Control) this.bCancel);
    this.Controls.Add((Control) this.bAdd);
    this.Controls.Add((Control) this.richTextBox1);
    this.Controls.Add((Control) this.bSelectFile);
    this.MinimumSize = new Size(546, 548);
    this.Name = nameof (SpellCheckInsertFromFileForm);
    this.Text = "Добавление данных из файла";
    this.ResumeLayout(false);
  }
}
