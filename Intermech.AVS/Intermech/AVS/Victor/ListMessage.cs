// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Victor.ListMessage
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Victor;

public class ListMessage : Form
{
  public List<string> _listStr;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panelButtons;
  private Button bCancel;
  public ListBox listBox;
  private Button button1;
  private Button buttonServiceCreateDump;

  public ListMessage() => this.InitializeComponent();

  private void ListMessage_Load(object sender, EventArgs e)
  {
    if (this._listStr == null)
      return;
    for (int index = 0; index < this._listStr.Count; ++index)
      this.listBox.Items.Add((object) this._listStr[index]);
  }

  private void button1_Click(object sender, EventArgs e)
  {
    Processing_Ved_Static.SaveToFile(this._listStr);
  }

  private void buttonServiceCreateDump_Click(object sender, EventArgs e)
  {
    Processing_Ved_Static.Print_Strings(this._listStr);
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
    this.panelButtons = new Panel();
    this.bCancel = new Button();
    this.listBox = new ListBox();
    this.buttonServiceCreateDump = new Button();
    this.button1 = new Button();
    this.panelButtons.SuspendLayout();
    this.SuspendLayout();
    this.panelButtons.BorderStyle = BorderStyle.Fixed3D;
    this.panelButtons.Controls.Add((Control) this.button1);
    this.panelButtons.Controls.Add((Control) this.buttonServiceCreateDump);
    this.panelButtons.Controls.Add((Control) this.bCancel);
    this.panelButtons.Dock = DockStyle.Bottom;
    this.panelButtons.Location = new Point(0, 431);
    this.panelButtons.Name = "panelButtons";
    this.panelButtons.Size = new Size(1013, 42);
    this.panelButtons.TabIndex = 4;
    this.bCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(848, 8);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 3;
    this.bCancel.Text = "Выход";
    this.bCancel.UseVisualStyleBackColor = true;
    this.listBox.Dock = DockStyle.Fill;
    this.listBox.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.listBox.FormattingEnabled = true;
    this.listBox.ItemHeight = 16 /*0x10*/;
    this.listBox.Location = new Point(0, 0);
    this.listBox.Name = "listBox";
    this.listBox.Size = new Size(1013, 431);
    this.listBox.TabIndex = 5;
    this.buttonServiceCreateDump.Location = new Point(10, 8);
    this.buttonServiceCreateDump.Name = "buttonServiceCreateDump";
    this.buttonServiceCreateDump.Size = new Size(168, 27);
    this.buttonServiceCreateDump.TabIndex = 18;
    this.buttonServiceCreateDump.Text = "На принтер";
    this.buttonServiceCreateDump.UseVisualStyleBackColor = true;
    this.buttonServiceCreateDump.Click += new EventHandler(this.buttonServiceCreateDump_Click);
    this.button1.Location = new Point(197, 8);
    this.button1.Name = "button1";
    this.button1.Size = new Size(168, 27);
    this.button1.TabIndex = 19;
    this.button1.Text = "В файл";
    this.button1.UseVisualStyleBackColor = true;
    this.button1.Click += new EventHandler(this.button1_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(1013, 473);
    this.Controls.Add((Control) this.listBox);
    this.Controls.Add((Control) this.panelButtons);
    this.Name = nameof (ListMessage);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Список замечаний";
    this.Load += new EventHandler(this.ListMessage_Load);
    this.panelButtons.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
