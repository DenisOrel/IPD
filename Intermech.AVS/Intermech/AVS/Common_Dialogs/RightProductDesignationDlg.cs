// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.RightProductDesignationDlg
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Common_Dialogs;

public class RightProductDesignationDlg : Form
{
  /// <summary>Обозначение исполнения</summary>
  public string LeftProductDesignation;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btnCancel;
  private Button btnOK;
  private TextBox tbRightProductDesignation;
  private Label label1;

  public RightProductDesignationDlg() => this.InitializeComponent();

  /// <summary>Обозначение исполнения</summary>
  public string RightProductDesignation
  {
    set => this.tbRightProductDesignation.Text = value;
    get => this.tbRightProductDesignation.Text;
  }

  /// <summary>Вызвать диалог для обозначения правого исполнения</summary>
  /// <param name="leftProductDesignation">Обозначение левого исполнения</param>
  /// <param name="rightProductDesignation">Обозначение правого исполнения</param>
  /// <returns></returns>
  public static DialogResult Execute(
    string leftProductDesignation,
    ref string rightProductDesignation)
  {
    RightProductDesignationDlg productDesignationDlg = new RightProductDesignationDlg();
    productDesignationDlg.LeftProductDesignation = leftProductDesignation;
    productDesignationDlg.RightProductDesignation = rightProductDesignation;
    int num = (int) productDesignationDlg.ShowDialog();
    if (num != 1)
      return (DialogResult) num;
    rightProductDesignation = productDesignationDlg.tbRightProductDesignation.Text;
    return (DialogResult) num;
  }

  protected void UpdateControls()
  {
    this.btnOK.Enabled = this.tbRightProductDesignation.Text != "" && this.tbRightProductDesignation.Text != null;
  }

  private void tbProductDesignation_TextChanged(object sender, EventArgs e)
  {
    this.UpdateControls();
  }

  private void btnOK_Click(object sender, EventArgs e)
  {
    if (this.tbRightProductDesignation.Text == this.LeftProductDesignation)
    {
      int num = (int) MessageBox.Show("Обозначения правого и левого исполнений не должны совпадать!", "Ошибка");
      this.DialogResult = DialogResult.None;
    }
    else
    {
      this.DialogResult = DialogResult.OK;
      this.Close();
      this.DialogResult = DialogResult.OK;
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
    this.btnCancel = new Button();
    this.btnOK = new Button();
    this.tbRightProductDesignation = new TextBox();
    this.label1 = new Label();
    this.SuspendLayout();
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.FlatStyle = FlatStyle.System;
    this.btnCancel.ImeMode = ImeMode.NoControl;
    this.btnCancel.Location = new Point(292, 57);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(121, 27);
    this.btnCancel.TabIndex = 5;
    this.btnCancel.Text = "О&тмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.FlatStyle = FlatStyle.System;
    this.btnOK.ImeMode = ImeMode.NoControl;
    this.btnOK.Location = new Point(165, 57);
    this.btnOK.Name = "btnOK";
    this.btnOK.Size = new Size(121, 27);
    this.btnOK.TabIndex = 4;
    this.btnOK.Text = "&ОК";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnOK.Click += new EventHandler(this.btnOK_Click);
    this.tbRightProductDesignation.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbRightProductDesignation.Location = new Point(12, 30);
    this.tbRightProductDesignation.Name = "tbRightProductDesignation";
    this.tbRightProductDesignation.Size = new Size(400, 20);
    this.tbRightProductDesignation.TabIndex = 10;
    this.tbRightProductDesignation.TextChanged += new EventHandler(this.tbProductDesignation_TextChanged);
    this.label1.AutoSize = true;
    this.label1.ImeMode = ImeMode.NoControl;
    this.label1.Location = new Point(9, 14);
    this.label1.Name = "label1";
    this.label1.Size = new Size(184, 13);
    this.label1.TabIndex = 11;
    this.label1.Text = "&Обозначение правого исполнения:";
    this.AcceptButton = (IButtonControl) this.btnOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(424, 96 /*0x60*/);
    this.Controls.Add((Control) this.tbRightProductDesignation);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOK);
    this.MaximizeBox = false;
    this.MaximumSize = new Size(990, 134);
    this.MinimizeBox = false;
    this.MinimumSize = new Size(220, 134);
    this.Name = nameof (RightProductDesignationDlg);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Обозначение исполнения";
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
