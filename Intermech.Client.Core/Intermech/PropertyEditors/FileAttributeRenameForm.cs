
// Type: Intermech.PropertyEditors.FileAttributeRenameForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

public class FileAttributeRenameForm : Form
{
  private string conflictFullName = string.Empty;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btnCancel;
  private Button btnOK;
  private Label label1;
  private Label label2;
  private TextBox tbConflictName;
  private Label label3;
  private TextBox tbNewName;
  private Button btnRandom;
  private Button btnAuto;

  public FileAttributeRenameForm() => this.InitializeComponent();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="conflictFullName"></param>
  /// <param name="newName"></param>
  /// <returns>Yes - автоматически сейчас и далее, OK-да, Cancel - Отмена</returns>
  public DialogResult ShowDialog(string conflictFullName, out string newName)
  {
    this.conflictFullName = conflictFullName;
    this.tbConflictName.Text = conflictFullName;
    newName = Path.GetFileName(conflictFullName);
    this.tbNewName.Text = newName;
    int num = (int) this.ShowDialog();
    if (num != 1)
      return (DialogResult) num;
    newName = this.tbNewName.Text;
    return (DialogResult) num;
  }

  private void btnRandom_Click(object sender, EventArgs e)
  {
    this.tbNewName.Text = FileAttributeEditForm.AutoRename(this.conflictFullName);
  }

  private void btnAuto_Click(object sender, EventArgs e)
  {
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FileAttributeRenameForm));
    this.btnCancel = new Button();
    this.btnOK = new Button();
    this.label1 = new Label();
    this.label2 = new Label();
    this.tbConflictName = new TextBox();
    this.label3 = new Label();
    this.tbNewName = new TextBox();
    this.btnRandom = new Button();
    this.btnAuto = new Button();
    this.SuspendLayout();
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(356, 179);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(75, 23);
    this.btnCancel.TabIndex = 1;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Location = new Point(275, 179);
    this.btnOK.Name = "btnOK";
    this.btnOK.Size = new Size(75, 23);
    this.btnOK.TabIndex = 2;
    this.btnOK.Text = "OK";
    this.btnOK.UseVisualStyleBackColor = true;
    this.label1.AutoSize = true;
    this.label1.Location = new Point(15, 9);
    this.label1.Name = "label1";
    this.label1.Size = new Size(358, 52);
    this.label1.TabIndex = 3;
    this.label1.Text = componentResourceManager.GetString("label1.Text");
    this.label2.AutoSize = true;
    this.label2.Location = new Point(15, 74);
    this.label2.Name = "label2";
    this.label2.Size = new Size(211, 13);
    this.label2.TabIndex = 4;
    this.label2.Text = "Полное имя с конфликтом именования:";
    this.tbConflictName.Location = new Point(18, 90);
    this.tbConflictName.Name = "tbConflictName";
    this.tbConflictName.ReadOnly = true;
    this.tbConflictName.Size = new Size(412, 20);
    this.tbConflictName.TabIndex = 5;
    this.label3.AutoSize = true;
    this.label3.Location = new Point(15, 122);
    this.label3.Name = "label3";
    this.label3.Size = new Size((int) sbyte.MaxValue, 13);
    this.label3.TabIndex = 6;
    this.label3.Text = "Новое уникальное имя:";
    this.tbNewName.Location = new Point(18, 138);
    this.tbNewName.Name = "tbNewName";
    this.tbNewName.Size = new Size(331, 20);
    this.tbNewName.TabIndex = 7;
    this.btnRandom.Location = new Point(355, 136);
    this.btnRandom.Name = "btnRandom";
    this.btnRandom.Size = new Size(75, 23);
    this.btnRandom.TabIndex = 8;
    this.btnRandom.Text = "Случайное";
    this.btnRandom.UseVisualStyleBackColor = true;
    this.btnRandom.Click += new EventHandler(this.btnRandom_Click);
    this.btnAuto.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnAuto.DialogResult = DialogResult.Yes;
    this.btnAuto.Location = new Point(19, 179);
    this.btnAuto.Name = "btnAuto";
    this.btnAuto.Size = new Size(250, 23);
    this.btnAuto.TabIndex = 9;
    this.btnAuto.Text = "Автоматически сейчас и далее";
    this.btnAuto.UseVisualStyleBackColor = true;
    this.btnAuto.Click += new EventHandler(this.btnAuto_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(443, 214);
    this.Controls.Add((Control) this.btnAuto);
    this.Controls.Add((Control) this.btnRandom);
    this.Controls.Add((Control) this.tbNewName);
    this.Controls.Add((Control) this.label3);
    this.Controls.Add((Control) this.tbConflictName);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.btnOK);
    this.Controls.Add((Control) this.btnCancel);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.Name = nameof (FileAttributeRenameForm);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Конфликт именования";
    this.TopMost = true;
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
