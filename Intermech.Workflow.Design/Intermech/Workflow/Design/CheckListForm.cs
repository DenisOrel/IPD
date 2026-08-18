// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.CheckListForm
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Controls;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

public class CheckListForm : FormEx
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel Panel2;
  private Button CancButton;
  private Button OkButton;
  public CheckedListBox ListBox;

  public CheckListForm() => this.InitializeComponent();

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
    this.Panel2 = new Panel();
    this.CancButton = new Button();
    this.OkButton = new Button();
    this.ListBox = new CheckedListBox();
    this.Panel2.SuspendLayout();
    this.SuspendLayout();
    this.Panel2.BackColor = Color.Transparent;
    this.Panel2.Controls.Add((Control) this.CancButton);
    this.Panel2.Controls.Add((Control) this.OkButton);
    this.Panel2.Dock = DockStyle.Bottom;
    this.Panel2.Location = new Point(7, 177);
    this.Panel2.Name = "Panel2";
    this.Panel2.Size = new Size(231, 30);
    this.Panel2.TabIndex = 3;
    this.CancButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.CancButton.DialogResult = DialogResult.Cancel;
    this.CancButton.ImeMode = ImeMode.NoControl;
    this.CancButton.Location = new Point(156, 6);
    this.CancButton.Name = "CancButton";
    this.CancButton.Size = new Size(75, 23);
    this.CancButton.TabIndex = 4;
    this.CancButton.Text = "Отмена";
    this.OkButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.OkButton.DialogResult = DialogResult.OK;
    this.OkButton.ImeMode = ImeMode.NoControl;
    this.OkButton.Location = new Point(75, 6);
    this.OkButton.Name = "OkButton";
    this.OkButton.Size = new Size(75, 23);
    this.OkButton.TabIndex = 3;
    this.OkButton.Text = "OK";
    this.ListBox.CheckOnClick = true;
    this.ListBox.Dock = DockStyle.Fill;
    this.ListBox.FormattingEnabled = true;
    this.ListBox.Location = new Point(7, 7);
    this.ListBox.Name = "ListBox";
    this.ListBox.Size = new Size(231, 170);
    this.ListBox.Sorted = true;
    this.ListBox.TabIndex = 4;
    this.AcceptButton = (IButtonControl) this.OkButton;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.CancButton;
    this.ClientSize = new Size(245, 214);
    this.Controls.Add((Control) this.ListBox);
    this.Controls.Add((Control) this.Panel2);
    this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(200, 200);
    this.Name = nameof (CheckListForm);
    this.Padding = new Padding(7);
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Выбор...";
    this.Panel2.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
