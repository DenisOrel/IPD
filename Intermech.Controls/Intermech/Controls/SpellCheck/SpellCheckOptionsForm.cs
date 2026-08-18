
// Type: Intermech.Controls.SpellCheck.SpellCheckOptionsForm
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Controls.SpellCheck;

public class SpellCheckOptionsForm : Form
{
  private Hashtable Hash = new Hashtable();
  private List<string> itemsToRemove = new List<string>();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panel1;
  private Button bCancel;
  private Button bOk;
  private Panel panel2;
  private Button bAppendFromFile;
  private RichTextBox richTextBox1;

  public SpellCheckOptionsForm(Hashtable hash)
  {
    this.Hash = hash;
    this.InitializeComponent();
    this.richTextBox1.Clear();
    List<string> stringList = new List<string>();
    foreach (object key in (IEnumerable) this.Hash.Keys)
    {
      string str = Convert.ToString(key);
      if (!string.IsNullOrEmpty(str))
        stringList.Add(str);
    }
    stringList.Sort();
    this.richTextBox1.Text = string.Join(Environment.NewLine, stringList.ToArray());
  }

  private void bAdd_Click(object sender, EventArgs e)
  {
  }

  private void bRemove_Click(object sender, EventArgs e)
  {
  }

  protected override void OnClosed(EventArgs e)
  {
    if (this.DialogResult == DialogResult.OK)
    {
      this.Hash.Clear();
      foreach (string line in this.richTextBox1.Lines)
      {
        string lower = line.ToLower();
        if (!this.Hash.ContainsKey((object) lower))
          this.Hash.Add((object) lower, (object) lower);
      }
    }
    base.OnClosed(e);
  }

  private void bAppendFromFile_Click(object sender, EventArgs e)
  {
    SpellCheckInsertFromFileForm insertFromFileForm = new SpellCheckInsertFromFileForm();
    if (insertFromFileForm.ShowDialog() != DialogResult.OK)
      return;
    this.richTextBox1.AppendText(insertFromFileForm.FileText);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SpellCheckOptionsForm));
    this.panel1 = new Panel();
    this.bCancel = new Button();
    this.bOk = new Button();
    this.panel2 = new Panel();
    this.bAppendFromFile = new Button();
    this.richTextBox1 = new RichTextBox();
    this.panel1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.bCancel);
    this.panel1.Controls.Add((Control) this.bOk);
    this.panel1.Dock = DockStyle.Bottom;
    this.panel1.Location = new Point(0, 302);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(332, 39);
    this.panel1.TabIndex = 0;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(248, 8);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(75, 23);
    this.bCancel.TabIndex = 1;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.bOk.DialogResult = DialogResult.OK;
    this.bOk.Location = new Point(167, 8);
    this.bOk.Name = "bOk";
    this.bOk.Size = new Size(75, 23);
    this.bOk.TabIndex = 0;
    this.bOk.Text = "ОК";
    this.bOk.UseVisualStyleBackColor = true;
    this.panel2.Controls.Add((Control) this.bAppendFromFile);
    this.panel2.Dock = DockStyle.Right;
    this.panel2.Location = new Point(284, 0);
    this.panel2.Name = "panel2";
    this.panel2.Size = new Size(48 /*0x30*/, 302);
    this.panel2.TabIndex = 2;
    this.bAppendFromFile.Image = (Image) componentResourceManager.GetObject("bAppendFromFile.Image");
    this.bAppendFromFile.Location = new Point(8, 4);
    this.bAppendFromFile.Name = "bAppendFromFile";
    this.bAppendFromFile.Size = new Size(32 /*0x20*/, 32 /*0x20*/);
    this.bAppendFromFile.TabIndex = 0;
    this.bAppendFromFile.UseVisualStyleBackColor = true;
    this.bAppendFromFile.Click += new EventHandler(this.bAppendFromFile_Click);
    this.richTextBox1.Dock = DockStyle.Fill;
    this.richTextBox1.Location = new Point(0, 0);
    this.richTextBox1.Name = "richTextBox1";
    this.richTextBox1.Size = new Size(284, 302);
    this.richTextBox1.TabIndex = 3;
    this.richTextBox1.Text = "";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(332, 341);
    this.Controls.Add((Control) this.richTextBox1);
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.panel1);
    this.MaximumSize = new Size(500, 1000);
    this.MinimumSize = new Size(348, 379);
    this.Name = nameof (SpellCheckOptionsForm);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Пользовательский словарь";
    this.panel1.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
