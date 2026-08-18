// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Victor.AddVariable
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Victor;

public class AddVariable : Form
{
  public string _variable_template = "";
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panelForButtons;
  internal Button bCancel;
  internal Button bOK;
  private Label label1;
  public ListBox listBox1;

  public AddVariable() => this.InitializeComponent();

  private void AddVariable_Load(object sender, EventArgs e) => this.listBox1.SelectedIndex = 0;

  private void bOK_Click(object sender, EventArgs e)
  {
    if (this.listBox1.SelectedIndex <= 0)
      return;
    this._variable_template = this.listBox1.Items[this.listBox1.SelectedIndex].ToString();
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
    this.panelForButtons = new Panel();
    this.bCancel = new Button();
    this.bOK = new Button();
    this.label1 = new Label();
    this.listBox1 = new ListBox();
    this.panelForButtons.SuspendLayout();
    this.SuspendLayout();
    this.panelForButtons.BorderStyle = BorderStyle.Fixed3D;
    this.panelForButtons.Controls.Add((Control) this.bCancel);
    this.panelForButtons.Controls.Add((Control) this.bOK);
    this.panelForButtons.Dock = DockStyle.Bottom;
    this.panelForButtons.Location = new Point(0, 319);
    this.panelForButtons.Name = "panelForButtons";
    this.panelForButtons.Size = new Size(444, 42);
    this.panelForButtons.TabIndex = 12;
    this.bCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(310, 5);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 2;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.bOK.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Location = new Point(160 /*0xA0*/, 5);
    this.bOK.Name = "bOK";
    this.bOK.Size = new Size(121, 27);
    this.bOK.TabIndex = 1;
    this.bOK.Text = "OK";
    this.bOK.UseVisualStyleBackColor = true;
    this.bOK.Click += new EventHandler(this.bOK_Click);
    this.label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.label1.BackColor = SystemColors.Info;
    this.label1.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.label1.Location = new Point(-3, -1);
    this.label1.Name = "label1";
    this.label1.Size = new Size(447, 43);
    this.label1.TabIndex = 13;
    this.label1.Text = "Выберите исполнение, которое выступит прототипом для создаваемого";
    this.label1.TextAlign = ContentAlignment.TopCenter;
    this.listBox1.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.listBox1.FormattingEnabled = true;
    this.listBox1.ItemHeight = 16 /*0x10*/;
    this.listBox1.Items.AddRange(new object[1]
    {
      (object) "[Без прототипа]"
    });
    this.listBox1.Location = new Point(0, 69);
    this.listBox1.Name = "listBox1";
    this.listBox1.Size = new Size(444, 244);
    this.listBox1.TabIndex = 14;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(444, 361);
    this.Controls.Add((Control) this.listBox1);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.panelForButtons);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (AddVariable);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Выбор исполнения";
    this.Load += new EventHandler(this.AddVariable_Load);
    this.panelForButtons.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
