// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.TestESForm
// Assembly: Intermech.Expert.Test, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 494A2DB2-0ED6-480D-BF40-DFD41733278B
// Assembly location: D:\IPS\Client\Intermech.Expert.Test.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Expert;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

public class TestESForm : Form
{
  private List<string> errList;
  private IContainer components;
  private ListBox lbErrors;
  private Button btnOK;
  private Button btnSave;
  private SaveFileDialog saveFileDialog1;

  public TestESForm() => this.InitializeComponent();

  public bool Execute()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.errList = (sessionKeeper.Session.GetCustomService(typeof (IExpertServer)) as IExpertServer).CheckExpertObjects();
      foreach (object err in this.errList)
        this.lbErrors.Items.Add(err);
      int num = (int) this.ShowDialog();
      return this.errList.Count > 0;
    }
  }

  private void btnSave_Click(object sender, EventArgs e)
  {
    if (this.errList == null || this.saveFileDialog1.ShowDialog() != DialogResult.OK)
      return;
    FileStream fileStream = (FileStream) this.saveFileDialog1.OpenFile();
    StreamWriter streamWriter = new StreamWriter((Stream) fileStream);
    if (fileStream == null)
      return;
    foreach (string err in this.errList)
      streamWriter.WriteLine(err);
    streamWriter.Flush();
    fileStream.Close();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.lbErrors = new ListBox();
    this.btnOK = new Button();
    this.btnSave = new Button();
    this.saveFileDialog1 = new SaveFileDialog();
    this.SuspendLayout();
    this.lbErrors.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.lbErrors.FormattingEnabled = true;
    this.lbErrors.Location = new Point(12, 12);
    this.lbErrors.Name = "lbErrors";
    this.lbErrors.Size = new Size(739, 212);
    this.lbErrors.TabIndex = 0;
    this.btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Location = new Point(676, 230);
    this.btnOK.Name = "btnOK";
    this.btnOK.Size = new Size(75, 23);
    this.btnOK.TabIndex = 1;
    this.btnOK.Text = "Закрыть";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnSave.Location = new Point(577, 230);
    this.btnSave.Name = "btnSave";
    this.btnSave.Size = new Size(93, 23);
    this.btnSave.TabIndex = 2;
    this.btnSave.Text = "Сохранить...";
    this.btnSave.UseVisualStyleBackColor = true;
    this.btnSave.Click += new EventHandler(this.btnSave_Click);
    this.saveFileDialog1.DefaultExt = "TXT";
    this.saveFileDialog1.Filter = "Текстовые файлы|*.txt|Все файлы|*.*";
    this.saveFileDialog1.Title = "Выберите имя файла для сохранения отчета";
    this.saveFileDialog1.RestoreDirectory = true;
    this.AcceptButton = (IButtonControl) this.btnOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(763, 262);
    this.Controls.Add((Control) this.btnSave);
    this.Controls.Add((Control) this.btnOK);
    this.Controls.Add((Control) this.lbErrors);
    this.MinimizeBox = false;
    this.Name = nameof (TestESForm);
    this.Text = "Тест объектов экспертной системы";
    this.ResumeLayout(false);
  }
}
