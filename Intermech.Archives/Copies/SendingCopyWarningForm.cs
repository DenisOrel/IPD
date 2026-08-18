// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Copies.SendingCopyWarningForm
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Archives.Copies;

public class SendingCopyWarningForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btnCancel;
  private Button btnOK;
  private TextBox lbInfo;

  public string MessageText
  {
    get => this.lbInfo.Text;
    set => this.lbInfo.Text = value;
  }

  public SendingCopyWarningForm() => this.InitializeComponent();

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
    this.lbInfo = new TextBox();
    this.SuspendLayout();
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(353, 121);
    this.btnCancel.MinimumSize = new Size(75, 23);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(90, 27);
    this.btnCancel.TabIndex = 7;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Location = new Point(257, 121);
    this.btnOK.MinimumSize = new Size(75, 23);
    this.btnOK.Name = "btnOK";
    this.btnOK.Size = new Size(90, 27);
    this.btnOK.TabIndex = 6;
    this.btnOK.Text = "Продолжить";
    this.btnOK.UseVisualStyleBackColor = true;
    this.lbInfo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.lbInfo.Location = new Point(2, 4);
    this.lbInfo.Multiline = true;
    this.lbInfo.Name = "lbInfo";
    this.lbInfo.ReadOnly = true;
    this.lbInfo.ScrollBars = ScrollBars.Vertical;
    this.lbInfo.Size = new Size(441, 111);
    this.lbInfo.TabIndex = 17;
    this.lbInfo.TabStop = false;
    this.AcceptButton = (IButtonControl) this.btnOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.AutoScroll = true;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(453, 156);
    this.Controls.Add((Control) this.lbInfo);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOK);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(469, 195);
    this.Name = nameof (SendingCopyWarningForm);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Внимание!";
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
