// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.AddKeywordReplacementDlg
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Common_Dialogs;

public class AddKeywordReplacementDlg : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label lblKeyword;
  private Button btnCancel;
  private Button btnOk;
  private TextBox tbKeyword;
  private Label lblReplacement;
  private TextBox tbReplacement;

  public AddKeywordReplacementDlg() => this.InitializeComponent();

  public string Keyword
  {
    get => this.tbKeyword.Text;
    set => this.tbKeyword.Text = value;
  }

  public string Replacement
  {
    get => this.tbReplacement.Text;
    set => this.tbReplacement.Text = value;
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
    this.lblKeyword = new Label();
    this.btnCancel = new Button();
    this.btnOk = new Button();
    this.tbKeyword = new TextBox();
    this.lblReplacement = new Label();
    this.tbReplacement = new TextBox();
    this.SuspendLayout();
    this.lblKeyword.AutoSize = true;
    this.lblKeyword.Location = new Point(6, 8);
    this.lblKeyword.Name = "lblKeyword";
    this.lblKeyword.Size = new Size(90, 13);
    this.lblKeyword.TabIndex = 12;
    this.lblKeyword.Text = "Ключевое слово";
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point((int) byte.MaxValue, 120);
    this.btnCancel.MaximumSize = new Size(120, 27);
    this.btnCancel.MinimumSize = new Size(120, 27);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(120, 27);
    this.btnCancel.TabIndex = 11;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOk.DialogResult = DialogResult.OK;
    this.btnOk.Location = new Point(130, 120);
    this.btnOk.MaximumSize = new Size(120, 27);
    this.btnOk.MinimumSize = new Size(120, 27);
    this.btnOk.Name = "btnOk";
    this.btnOk.Size = new Size(120, 27);
    this.btnOk.TabIndex = 10;
    this.btnOk.Text = "OK";
    this.btnOk.UseVisualStyleBackColor = true;
    this.tbKeyword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbKeyword.Location = new Point(6, 24);
    this.tbKeyword.Name = "tbKeyword";
    this.tbKeyword.Size = new Size(369, 20);
    this.tbKeyword.TabIndex = 8;
    this.lblReplacement.AutoSize = true;
    this.lblReplacement.Location = new Point(6, 57);
    this.lblReplacement.Name = "lblReplacement";
    this.lblReplacement.Size = new Size(69, 13);
    this.lblReplacement.TabIndex = 13;
    this.lblReplacement.Text = "Заменитель";
    this.tbReplacement.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbReplacement.Location = new Point(6, 73);
    this.tbReplacement.Name = "tbReplacement";
    this.tbReplacement.Size = new Size(369, 20);
    this.tbReplacement.TabIndex = 9;
    this.AcceptButton = (IButtonControl) this.btnOk;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(382, 155);
    this.ControlBox = false;
    this.Controls.Add((Control) this.tbReplacement);
    this.Controls.Add((Control) this.lblReplacement);
    this.Controls.Add((Control) this.lblKeyword);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOk);
    this.Controls.Add((Control) this.tbKeyword);
    this.MaximumSize = new Size(700, 220);
    this.MinimumSize = new Size(300, 120);
    this.Name = nameof (AddKeywordReplacementDlg);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Добавить замену";
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
