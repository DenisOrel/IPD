// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Victor.EditGroupForm
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

public class EditGroupForm : Form
{
  public string _formaGroup_Doc;
  public int _nGroupForm;
  public bool _isModifiedGroup;
  public string _change = "";
  private int groupForm_Old;
  private int groupForm_New;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  public RadioButton rbGroupB;
  public RadioButton rbGroupA;
  public RadioButton rbSingle;
  private Button bOk;
  private Button bCancel;

  public EditGroupForm() => this.InitializeComponent();

  private void EditGroupForm_Load(object sender, EventArgs e)
  {
    switch (this._nGroupForm)
    {
      case 12:
        this.rbSingle.Visible = true;
        this.rbGroupA.Visible = true;
        this.rbGroupB.Visible = false;
        break;
      case 23:
        this.rbSingle.Visible = false;
        this.rbGroupA.Visible = true;
        this.rbGroupB.Visible = true;
        break;
      case 123:
        this.rbSingle.Visible = true;
        this.rbGroupA.Visible = true;
        this.rbGroupB.Visible = true;
        break;
      default:
        this.rbSingle.Visible = true;
        this.rbGroupA.Visible = true;
        this.rbGroupB.Visible = true;
        break;
    }
    if (this._formaGroup_Doc == "" || this._formaGroup_Doc == "Ed")
    {
      this.rbSingle.Checked = true;
      this.groupForm_Old = 0;
      this._change = "E";
    }
    if (this._formaGroup_Doc == "A")
    {
      this.rbGroupA.Checked = true;
      this.groupForm_Old = 1;
      this._change = "A";
    }
    if (!(this._formaGroup_Doc == "B"))
      return;
    this.rbGroupB.Checked = true;
    this.groupForm_Old = 2;
    this._change = "B";
    this.rbGroupA.Enabled = false;
    this.rbSingle.Enabled = false;
  }

  private void bOk_Click(object sender, EventArgs e)
  {
    if (this.rbSingle.Checked)
    {
      this.groupForm_New = 0;
      this._change += "E";
    }
    if (this.rbGroupA.Checked)
    {
      this.groupForm_New = 1;
      this._change += "A";
    }
    if (this.rbGroupB.Checked)
    {
      this.groupForm_New = 2;
      this._change += "B";
    }
    if (this.groupForm_New == this.groupForm_Old)
      return;
    this._isModifiedGroup = true;
    if (this.rbSingle.Checked)
      this._formaGroup_Doc = "Ed";
    if (this.rbGroupA.Checked)
      this._formaGroup_Doc = "A";
    if (!this.rbGroupB.Checked)
      return;
    this._formaGroup_Doc = "B";
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
    this.rbGroupB = new RadioButton();
    this.rbGroupA = new RadioButton();
    this.rbSingle = new RadioButton();
    this.bOk = new Button();
    this.bCancel = new Button();
    this.SuspendLayout();
    this.rbGroupB.AutoSize = true;
    this.rbGroupB.ImeMode = ImeMode.NoControl;
    this.rbGroupB.Location = new Point(83, 77);
    this.rbGroupB.Name = "rbGroupB";
    this.rbGroupB.Size = new Size(88, 17);
    this.rbGroupB.TabIndex = 7;
    this.rbGroupB.Tag = (object) "2";
    this.rbGroupB.Text = "Групповая Б";
    this.rbGroupB.UseVisualStyleBackColor = true;
    this.rbGroupA.AutoSize = true;
    this.rbGroupA.ImeMode = ImeMode.NoControl;
    this.rbGroupA.Location = new Point(83, 44);
    this.rbGroupA.Name = "rbGroupA";
    this.rbGroupA.Size = new Size(88, 17);
    this.rbGroupA.TabIndex = 6;
    this.rbGroupA.Tag = (object) "1";
    this.rbGroupA.Text = "Групповая А";
    this.rbGroupA.UseVisualStyleBackColor = true;
    this.rbSingle.AutoSize = true;
    this.rbSingle.Checked = true;
    this.rbSingle.ImeMode = ImeMode.NoControl;
    this.rbSingle.Location = new Point(83, 11);
    this.rbSingle.Name = "rbSingle";
    this.rbSingle.Size = new Size(79, 17);
    this.rbSingle.TabIndex = 5;
    this.rbSingle.TabStop = true;
    this.rbSingle.Tag = (object) "0";
    this.rbSingle.Text = "Единичная";
    this.rbSingle.UseVisualStyleBackColor = true;
    this.bOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bOk.DialogResult = DialogResult.OK;
    this.bOk.ImeMode = ImeMode.NoControl;
    this.bOk.Location = new Point(14, 108);
    this.bOk.Name = "bOk";
    this.bOk.Size = new Size(121, 27);
    this.bOk.TabIndex = 8;
    this.bOk.Text = "&OK";
    this.bOk.UseVisualStyleBackColor = true;
    this.bOk.Click += new EventHandler(this.bOk_Click);
    this.bCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.ImeMode = ImeMode.NoControl;
    this.bCancel.Location = new Point(147, 108);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 9;
    this.bCancel.Text = "О&тмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(280, 147);
    this.Controls.Add((Control) this.bOk);
    this.Controls.Add((Control) this.bCancel);
    this.Controls.Add((Control) this.rbGroupB);
    this.Controls.Add((Control) this.rbGroupA);
    this.Controls.Add((Control) this.rbSingle);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (EditGroupForm);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Выбор формы документа";
    this.Load += new EventHandler(this.EditGroupForm_Load);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
