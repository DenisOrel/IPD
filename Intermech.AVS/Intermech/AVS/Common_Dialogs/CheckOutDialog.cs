// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.CheckOutDialog
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Common_Dialogs;

public class CheckOutDialog : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button _BtnCancel;
  private Button _BtnOK;
  private GroupBox groupBox1;
  private RadioButton rbWithoutCheck;
  private RadioButton rbWithCheck;
  private Label label1;

  public CheckOutDialog() => this.InitializeComponent();

  public ChekOutType Type
  {
    get
    {
      if (this.DialogResult == DialogResult.Cancel)
        return ChekOutType.None;
      if (this.rbWithCheck.Checked)
        return ChekOutType.CheckOutWithCheckIn;
      return this.rbWithoutCheck.Checked ? ChekOutType.CheckOutWithoutCheckIn : ChekOutType.None;
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
    this._BtnCancel = new Button();
    this._BtnOK = new Button();
    this.groupBox1 = new GroupBox();
    this.rbWithoutCheck = new RadioButton();
    this.rbWithCheck = new RadioButton();
    this.label1 = new Label();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    this._BtnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._BtnCancel.DialogResult = DialogResult.Cancel;
    this._BtnCancel.FlatStyle = FlatStyle.System;
    this._BtnCancel.ImeMode = ImeMode.NoControl;
    this._BtnCancel.Location = new Point(159, 133);
    this._BtnCancel.Name = "_BtnCancel";
    this._BtnCancel.Size = new Size(121, 27);
    this._BtnCancel.TabIndex = 6;
    this._BtnCancel.Text = "Отмена";
    this._BtnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._BtnOK.DialogResult = DialogResult.OK;
    this._BtnOK.FlatStyle = FlatStyle.System;
    this._BtnOK.ImeMode = ImeMode.NoControl;
    this._BtnOK.Location = new Point(19, 133);
    this._BtnOK.Name = "_BtnOK";
    this._BtnOK.Size = new Size(121, 27);
    this._BtnOK.TabIndex = 5;
    this._BtnOK.Text = "ОК";
    this.groupBox1.Controls.Add((Control) this.rbWithoutCheck);
    this.groupBox1.Controls.Add((Control) this.rbWithCheck);
    this.groupBox1.Location = new Point(19, 47);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(261, 73);
    this.groupBox1.TabIndex = 7;
    this.groupBox1.TabStop = false;
    this.rbWithoutCheck.AutoSize = true;
    this.rbWithoutCheck.Location = new Point(13, 42);
    this.rbWithoutCheck.Name = "rbWithoutCheck";
    this.rbWithoutCheck.Size = new Size(242, 17);
    this.rbWithoutCheck.TabIndex = 0;
    this.rbWithoutCheck.Text = "Взять на редактирование без завершения";
    this.rbWithoutCheck.UseVisualStyleBackColor = true;
    this.rbWithCheck.AutoSize = true;
    this.rbWithCheck.Checked = true;
    this.rbWithCheck.Location = new Point(13, 19);
    this.rbWithCheck.Name = "rbWithCheck";
    this.rbWithCheck.Size = new Size(238, 17);
    this.rbWithCheck.TabIndex = 0;
    this.rbWithCheck.TabStop = true;
    this.rbWithCheck.Text = "Взять на редактирование с завершением";
    this.rbWithCheck.UseVisualStyleBackColor = true;
    this.label1.AutoSize = true;
    this.label1.Location = new Point(21, 18);
    this.label1.Name = "label1";
    this.label1.Size = new Size(196, 26);
    this.label1.TabIndex = 8;
    this.label1.Text = "Для изменения объекта необходимо\r\nвзять его на редактирование.";
    this.AcceptButton = (IButtonControl) this._BtnOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._BtnCancel;
    this.ClientSize = new Size(292, 168);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.groupBox1);
    this.Controls.Add((Control) this._BtnCancel);
    this.Controls.Add((Control) this._BtnOK);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (CheckOutDialog);
    this.Text = "Взять на изменение";
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
