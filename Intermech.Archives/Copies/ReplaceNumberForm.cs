// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Copies.ReplaceNumberForm
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Archives.Copies;

/// <summary>
/// форма для запроса пользователю на замену старого инвентарного номера
/// </summary>
public class ReplaceNumberForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btnYes;
  private Button btnYesForAll;
  private Button btnNo;
  private Button btnNoForAll;
  private Button btnCancel;
  private PictureBox pictureBox1;
  private TextBox textBox1;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectName"></param>
  /// <param name="oldNumber"></param>
  public ReplaceNumberForm(string objectName, string oldNumber)
  {
    this.InitializeComponent();
    this.textBox1.Text = string.Format(ServiceHolder.rm.GetString("Archives_133"), (object) objectName, (object) oldNumber);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ReplaceNumberForm));
    this.btnYes = new Button();
    this.btnYesForAll = new Button();
    this.btnNo = new Button();
    this.btnNoForAll = new Button();
    this.btnCancel = new Button();
    this.pictureBox1 = new PictureBox();
    this.textBox1 = new TextBox();
    ((ISupportInitialize) this.pictureBox1).BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.btnYes, "btnYes");
    this.btnYes.DialogResult = DialogResult.Yes;
    this.btnYes.Name = "btnYes";
    this.btnYes.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.btnYesForAll, "btnYesForAll");
    this.btnYesForAll.DialogResult = DialogResult.OK;
    this.btnYesForAll.Name = "btnYesForAll";
    this.btnYesForAll.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.btnNo, "btnNo");
    this.btnNo.DialogResult = DialogResult.No;
    this.btnNo.Name = "btnNo";
    this.btnNo.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.btnNoForAll, "btnNoForAll");
    this.btnNoForAll.DialogResult = DialogResult.Ignore;
    this.btnNoForAll.Name = "btnNoForAll";
    this.btnNoForAll.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.pictureBox1, "pictureBox1");
    this.pictureBox1.Name = "pictureBox1";
    this.pictureBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.textBox1, "textBox1");
    this.textBox1.Name = "textBox1";
    this.textBox1.ReadOnly = true;
    this.textBox1.TabStop = false;
    this.AcceptButton = (IButtonControl) this.btnYesForAll;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.textBox1);
    this.Controls.Add((Control) this.pictureBox1);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnNoForAll);
    this.Controls.Add((Control) this.btnNo);
    this.Controls.Add((Control) this.btnYesForAll);
    this.Controls.Add((Control) this.btnYes);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ReplaceNumberForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    ((ISupportInitialize) this.pictureBox1).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
