// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.ProductNumberDlg
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

public class ProductNumberDlg : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btnCancel;
  private Button btnOK;
  private Label label1;
  private TextBox tbProductNumber;

  public ProductNumberDlg() => this.InitializeComponent();

  public static DialogResult Execute(ref string productNumber)
  {
    ProductNumberDlg productNumberDlg = new ProductNumberDlg();
    productNumberDlg.tbProductNumber.Text = productNumber;
    int num = (int) productNumberDlg.ShowDialog();
    if (num != 1)
      return (DialogResult) num;
    productNumber = productNumberDlg.tbProductNumber.Text;
    return (DialogResult) num;
  }

  protected void UpdateControls()
  {
    this.btnOK.Enabled = this.tbProductNumber.Text != "" && this.tbProductNumber.Text != null;
  }

  private void tbProductNumber_TextChanged(object sender, EventArgs e) => this.UpdateControls();

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
    this.label1 = new Label();
    this.tbProductNumber = new TextBox();
    this.SuspendLayout();
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.FlatStyle = FlatStyle.System;
    this.btnCancel.ImeMode = ImeMode.NoControl;
    this.btnCancel.Location = new Point(138, 53);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(121, 27);
    this.btnCancel.TabIndex = 3;
    this.btnCancel.Text = "О&тмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.FlatStyle = FlatStyle.System;
    this.btnOK.ImeMode = ImeMode.NoControl;
    this.btnOK.Location = new Point(13, 53);
    this.btnOK.Name = "btnOK";
    this.btnOK.Size = new Size(121, 27);
    this.btnOK.TabIndex = 2;
    this.btnOK.Text = "&ОК";
    this.btnOK.UseVisualStyleBackColor = true;
    this.label1.AutoSize = true;
    this.label1.Location = new Point(10, 8);
    this.label1.Name = "label1";
    this.label1.Size = new Size(185, 13);
    this.label1.TabIndex = 0;
    this.label1.Text = "Наименование графы исполнения:";
    this.tbProductNumber.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbProductNumber.Location = new Point(12, 24);
    this.tbProductNumber.Name = "tbProductNumber";
    this.tbProductNumber.Size = new Size(247, 20);
    this.tbProductNumber.TabIndex = 1;
    this.tbProductNumber.TextChanged += new EventHandler(this.tbProductNumber_TextChanged);
    this.AcceptButton = (IButtonControl) this.btnOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(271, 92);
    this.Controls.Add((Control) this.tbProductNumber);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOK);
    this.MaximizeBox = false;
    this.MaximumSize = new Size(600, 130);
    this.MinimizeBox = false;
    this.MinimumSize = new Size(220, 130);
    this.Name = nameof (ProductNumberDlg);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Номер исполнения";
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
