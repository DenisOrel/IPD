// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.StringListForm
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

public class StringListForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panel1;
  private Button button1;
  private ListBox lb;
  private Button btnSave;
  private SaveFileDialog sfd;

  public StringListForm() => this.InitializeComponent();

  public void Execute(List<string> sList)
  {
    this.lb.Items.Clear();
    foreach (object s in sList)
      this.lb.Items.Add(s);
    int num = (int) this.ShowDialog();
  }

  private void btnSave_Click(object sender, EventArgs e)
  {
    if (this.sfd.ShowDialog() != DialogResult.OK)
      return;
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < this.lb.Items.Count; ++index)
      stringBuilder.AppendLine(this.lb.Items[index].ToString());
    using (StreamWriter streamWriter = new StreamWriter(this.sfd.FileName))
    {
      streamWriter.Write(stringBuilder.ToString());
      streamWriter.Flush();
      streamWriter.Close();
    }
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
    this.panel1 = new Panel();
    this.button1 = new Button();
    this.lb = new ListBox();
    this.btnSave = new Button();
    this.sfd = new SaveFileDialog();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.btnSave);
    this.panel1.Controls.Add((Control) this.button1);
    this.panel1.Dock = DockStyle.Bottom;
    this.panel1.Location = new Point(0, 262);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(820, 35);
    this.panel1.TabIndex = 0;
    this.button1.DialogResult = DialogResult.OK;
    this.button1.Location = new Point(733, 6);
    this.button1.Name = "button1";
    this.button1.Size = new Size(75, 23);
    this.button1.TabIndex = 0;
    this.button1.Text = "Да";
    this.button1.UseVisualStyleBackColor = true;
    this.lb.Dock = DockStyle.Fill;
    this.lb.FormattingEnabled = true;
    this.lb.IntegralHeight = false;
    this.lb.Location = new Point(0, 0);
    this.lb.Name = "lb";
    this.lb.Size = new Size(820, 262);
    this.lb.TabIndex = 1;
    this.btnSave.Location = new Point(12, 6);
    this.btnSave.Name = "btnSave";
    this.btnSave.Size = new Size(159, 23);
    this.btnSave.TabIndex = 1;
    this.btnSave.Text = "Сохранить в файл...";
    this.btnSave.UseVisualStyleBackColor = true;
    this.btnSave.Click += new EventHandler(this.btnSave_Click);
    this.sfd.DefaultExt = "TXT";
    this.sfd.Filter = "Текстовые файлы|*.txt";
    this.sfd.Title = "Сохранить отчет в файл";
    this.sfd.RestoreDirectory = true;
    this.AcceptButton = (IButtonControl) this.button1;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(820, 297);
    this.Controls.Add((Control) this.lb);
    this.Controls.Add((Control) this.panel1);
    this.MinimizeBox = false;
    this.Name = nameof (StringListForm);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Отчет";
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
