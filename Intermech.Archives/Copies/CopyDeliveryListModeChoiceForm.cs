// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Copies.CopyDeliveryListModeChoiceForm
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Archives.Copies;

/// <summary>
/// Форма выбора режима копирования листа рассылки у документа.
/// </summary>
public class CopyDeliveryListModeChoiceForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private GroupBox groupBox1;
  private RadioButton rbSubstSubscribers;
  private RadioButton rbAddSubscribers;
  private Button btnOk;
  private Button btnCancel;

  /// <summary>Конструктор.</summary>
  public CopyDeliveryListModeChoiceForm() => this.InitializeComponent();

  /// <summary>Кнопка ОК.</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  private void btnOk_Click(object sender, EventArgs e)
  {
    if (this.rbAddSubscribers.Checked)
      this.DialogResult = DialogResult.Yes;
    else
      this.DialogResult = DialogResult.No;
  }

  /// <summary>Кнопка Отмена.</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  private void btnCancel_Click(object sender, EventArgs e)
  {
    this.DialogResult = DialogResult.Cancel;
    this.Close();
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
    this.groupBox1 = new GroupBox();
    this.rbSubstSubscribers = new RadioButton();
    this.rbAddSubscribers = new RadioButton();
    this.btnOk = new Button();
    this.btnCancel = new Button();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    this.groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.groupBox1.Controls.Add((Control) this.rbSubstSubscribers);
    this.groupBox1.Controls.Add((Control) this.rbAddSubscribers);
    this.groupBox1.Location = new Point(12, 3);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(442, 75);
    this.groupBox1.TabIndex = 0;
    this.groupBox1.TabStop = false;
    this.rbSubstSubscribers.AutoSize = true;
    this.rbSubstSubscribers.Location = new Point(7, 44);
    this.rbSubstSubscribers.Name = "rbSubstSubscribers";
    this.rbSubstSubscribers.Size = new Size(228, 17);
    this.rbSubstSubscribers.TabIndex = 1;
    this.rbSubstSubscribers.Text = "Заменить абонентов в листе рассылки.";
    this.rbSubstSubscribers.UseVisualStyleBackColor = true;
    this.rbAddSubscribers.AutoSize = true;
    this.rbAddSubscribers.Checked = true;
    this.rbAddSubscribers.Location = new Point(7, 20);
    this.rbAddSubscribers.Name = "rbAddSubscribers";
    this.rbAddSubscribers.Size = new Size(222, 17);
    this.rbAddSubscribers.TabIndex = 0;
    this.rbAddSubscribers.TabStop = true;
    this.rbAddSubscribers.Text = "Добавить абонентов в лист рассылки.";
    this.rbAddSubscribers.UseVisualStyleBackColor = true;
    this.btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOk.ImeMode = ImeMode.NoControl;
    this.btnOk.Location = new Point(269, 90);
    this.btnOk.Name = "btnOk";
    this.btnOk.Size = new Size(90, 27);
    this.btnOk.TabIndex = 3;
    this.btnOk.Text = "ОК";
    this.btnOk.UseVisualStyleBackColor = true;
    this.btnOk.Click += new EventHandler(this.btnOk_Click);
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.ImeMode = ImeMode.NoControl;
    this.btnCancel.Location = new Point(365, 90);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(90, 27);
    this.btnCancel.TabIndex = 2;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    this.AcceptButton = (IButtonControl) this.btnOk;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(466, 126);
    this.Controls.Add((Control) this.btnOk);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.groupBox1);
    this.MinimumSize = new Size(482, 153);
    this.Name = nameof (CopyDeliveryListModeChoiceForm);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Выбор режима копирования листа рассылки у документа.";
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.ResumeLayout(false);
  }
}
