// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Victor.CheckListDialog
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Victor;

public class CheckListDialog : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panelForButtons;
  internal Button bCancel;
  internal Button bOK;
  public CheckedListBox checkedListBox1;
  public Label label1;

  public CheckListDialog() => this.InitializeComponent();

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
    this.panelForButtons = new Panel();
    this.bCancel = new Button();
    this.bOK = new Button();
    this.checkedListBox1 = new CheckedListBox();
    this.label1 = new Label();
    this.panelForButtons.SuspendLayout();
    this.SuspendLayout();
    this.panelForButtons.BorderStyle = BorderStyle.Fixed3D;
    this.panelForButtons.Controls.Add((Control) this.bCancel);
    this.panelForButtons.Controls.Add((Control) this.bOK);
    this.panelForButtons.Dock = DockStyle.Bottom;
    this.panelForButtons.Location = new Point(0, 312);
    this.panelForButtons.Name = "panelForButtons";
    this.panelForButtons.Size = new Size(480, 42);
    this.panelForButtons.TabIndex = 14;
    this.bCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(345, 8);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 2;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.bOK.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Location = new Point(194, 8);
    this.bOK.Name = "bOK";
    this.bOK.Size = new Size(121, 27);
    this.bOK.TabIndex = 1;
    this.bOK.Text = "OK";
    this.bOK.UseVisualStyleBackColor = true;
    this.checkedListBox1.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.checkedListBox1.FormattingEnabled = true;
    this.checkedListBox1.Location = new Point(12, 113);
    this.checkedListBox1.Name = "checkedListBox1";
    this.checkedListBox1.Size = new Size(456, 193);
    this.checkedListBox1.TabIndex = 15;
    this.label1.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, (byte) 204);
    this.label1.Location = new Point(12, 9);
    this.label1.Name = "label1";
    this.label1.Size = new Size(456, 101);
    this.label1.TabIndex = 16 /*0x10*/;
    this.label1.Text = "label1";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(480, 354);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.checkedListBox1);
    this.Controls.Add((Control) this.panelForButtons);
    this.Name = nameof (CheckListDialog);
    this.Text = nameof (CheckListDialog);
    this.panelForButtons.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
