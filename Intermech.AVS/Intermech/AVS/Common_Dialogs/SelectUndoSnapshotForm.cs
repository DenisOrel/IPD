// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.SelectUndoSnapshotForm
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Common_Dialogs;

public class SelectUndoSnapshotForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ListBox listBox;
  private Panel panel1;
  private Button button2;
  private Button button1;

  public SelectUndoSnapshotForm() => this.InitializeComponent();

  internal static AvsMenuHelper.SnapshotInfo Execute(List<AvsMenuHelper.SnapshotInfo> infos)
  {
    SelectUndoSnapshotForm undoSnapshotForm = new SelectUndoSnapshotForm();
    foreach (AvsMenuHelper.SnapshotInfo info in infos)
      undoSnapshotForm.listBox.Items.Add((object) info);
    undoSnapshotForm.listBox.SelectedIndex = 0;
    return undoSnapshotForm.ShowDialog() == DialogResult.OK ? undoSnapshotForm.listBox.SelectedItem as AvsMenuHelper.SnapshotInfo : (AvsMenuHelper.SnapshotInfo) null;
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
    this.listBox = new ListBox();
    this.panel1 = new Panel();
    this.button2 = new Button();
    this.button1 = new Button();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.listBox.Dock = DockStyle.Fill;
    this.listBox.FormattingEnabled = true;
    this.listBox.Location = new Point(0, 0);
    this.listBox.Name = "listBox";
    this.listBox.Size = new Size(431, 212);
    this.listBox.TabIndex = 0;
    this.panel1.Controls.Add((Control) this.button2);
    this.panel1.Controls.Add((Control) this.button1);
    this.panel1.Dock = DockStyle.Bottom;
    this.panel1.Location = new Point(0, 222);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(431, 40);
    this.panel1.TabIndex = 1;
    this.button2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.button2.DialogResult = DialogResult.Cancel;
    this.button2.Location = new Point(348, 10);
    this.button2.Name = "button2";
    this.button2.Size = new Size(75, 23);
    this.button2.TabIndex = 1;
    this.button2.Text = "Отмена";
    this.button2.UseVisualStyleBackColor = true;
    this.button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.button1.DialogResult = DialogResult.OK;
    this.button1.Location = new Point(267, 10);
    this.button1.Name = "button1";
    this.button1.Size = new Size(75, 23);
    this.button1.TabIndex = 0;
    this.button1.Text = "OK";
    this.button1.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this.button1;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.button2;
    this.ClientSize = new Size(431, 262);
    this.Controls.Add((Control) this.listBox);
    this.Controls.Add((Control) this.panel1);
    this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    this.MinimumSize = new Size(300, 296);
    this.Name = nameof (SelectUndoSnapshotForm);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Выбор изменения для отката";
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
