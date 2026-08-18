// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.ProductDesignationAndNumberDlg
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

public class ProductDesignationAndNumberDlg : BaseProductInfoDlg
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btnCancel;
  private Button btnOK;
  private TextBox tbProductDesignation;
  private Label label1;
  private TextBox tbProductNumber;
  private Label label2;

  public ProductDesignationAndNumberDlg() => this.InitializeComponent();

  /// <summary>Обозначение исполнения</summary>
  public override string ProductDesignation
  {
    get => this.tbProductDesignation.Text;
    set
    {
      this.tbProductDesignation.Text = value;
      this.originDesignation = value;
    }
  }

  /// <summary>Номер исполнения</summary>
  public override string ProductNumber
  {
    get => this.tbProductNumber.Text;
    set
    {
      this.tbProductNumber.Text = value;
      this.originNumber = value;
    }
  }

  public override string ProductCaption => this.ProductDesignation;

  public static DialogResult Execute(ref string productDesignation, ref string productNumber)
  {
    ProductDesignationAndNumberDlg designationAndNumberDlg = new ProductDesignationAndNumberDlg();
    designationAndNumberDlg.tbProductDesignation.Text = productDesignation;
    designationAndNumberDlg.tbProductNumber.Text = productNumber;
    int num = (int) designationAndNumberDlg.ShowDialog();
    if (num != 1)
      return (DialogResult) num;
    productDesignation = designationAndNumberDlg.tbProductDesignation.Text;
    productNumber = designationAndNumberDlg.tbProductNumber.Text;
    return (DialogResult) num;
  }

  protected void UpdateControls()
  {
    this.btnOK.Enabled = this.tbProductDesignation.Text != "" && this.tbProductDesignation.Text != null && this.tbProductNumber.Text != "" && this.tbProductNumber.Text != null;
  }

  private void tbProductDesignation_TextChanged(object sender, EventArgs e)
  {
    this.UpdateControls();
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
    this.tbProductDesignation = new TextBox();
    this.label1 = new Label();
    this.tbProductNumber = new TextBox();
    this.label2 = new Label();
    this.SuspendLayout();
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.FlatStyle = FlatStyle.System;
    this.btnCancel.ImeMode = ImeMode.NoControl;
    this.btnCancel.Location = new Point(291, 109);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(121, 27);
    this.btnCancel.TabIndex = 5;
    this.btnCancel.Text = "О&тмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.FlatStyle = FlatStyle.System;
    this.btnOK.ImeMode = ImeMode.NoControl;
    this.btnOK.Location = new Point(164, 109);
    this.btnOK.Name = "btnOK";
    this.btnOK.Size = new Size(121, 27);
    this.btnOK.TabIndex = 4;
    this.btnOK.Text = "&ОК";
    this.btnOK.UseVisualStyleBackColor = true;
    this.tbProductDesignation.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbProductDesignation.Location = new Point(12, 30);
    this.tbProductDesignation.Name = "tbProductDesignation";
    this.tbProductDesignation.Size = new Size(400, 20);
    this.tbProductDesignation.TabIndex = 10;
    this.tbProductDesignation.TextChanged += new EventHandler(this.tbProductDesignation_TextChanged);
    this.label1.AutoSize = true;
    this.label1.ImeMode = ImeMode.NoControl;
    this.label1.Location = new Point(9, 14);
    this.label1.Name = "label1";
    this.label1.Size = new Size(140, 13);
    this.label1.TabIndex = 11;
    this.label1.Text = "&Обозначение исполнения:";
    this.tbProductNumber.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbProductNumber.Location = new Point(12, 78);
    this.tbProductNumber.Name = "tbProductNumber";
    this.tbProductNumber.Size = new Size(400, 20);
    this.tbProductNumber.TabIndex = 12;
    this.tbProductNumber.TextChanged += new EventHandler(this.tbProductDesignation_TextChanged);
    this.label2.AutoSize = true;
    this.label2.ImeMode = ImeMode.NoControl;
    this.label2.Location = new Point(9, 62);
    this.label2.Name = "label2";
    this.label2.Size = new Size(185, 13);
    this.label2.TabIndex = 13;
    this.label2.Text = "&Наименование графы исполнения:";
    this.AcceptButton = (IButtonControl) this.btnOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(424, 148);
    this.Controls.Add((Control) this.tbProductNumber);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.tbProductDesignation);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOK);
    this.MaximizeBox = false;
    this.MaximumSize = new Size(990, 186);
    this.MinimizeBox = false;
    this.MinimumSize = new Size(220, 186);
    this.Name = nameof (ProductDesignationAndNumberDlg);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Обозначение исполнения";
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
