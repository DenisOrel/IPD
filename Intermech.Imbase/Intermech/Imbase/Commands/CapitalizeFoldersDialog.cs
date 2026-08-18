// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Commands.CapitalizeFoldersDialog
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Commands;

public class CapitalizeFoldersDialog : Form
{
  private IContainer components;
  private Button btOk;
  private Button btCancel;
  private GroupBox groupBox1;
  private RadioButton _upperCaseRB;
  private RadioButton _capitalizeRB;

  public CapitalizeFoldersDialog() => this.InitializeComponent();

  public bool UpperCase => this._upperCaseRB.Checked;

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CapitalizeFoldersDialog));
    this.btOk = new Button();
    this.btCancel = new Button();
    this.groupBox1 = new GroupBox();
    this._upperCaseRB = new RadioButton();
    this._capitalizeRB = new RadioButton();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.btOk, "btOk");
    this.btOk.DialogResult = DialogResult.OK;
    this.btOk.Name = "btOk";
    this.btOk.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.btCancel, "btCancel");
    this.btCancel.DialogResult = DialogResult.Cancel;
    this.btCancel.Name = "btCancel";
    this.btCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.Controls.Add((Control) this._upperCaseRB);
    this.groupBox1.Controls.Add((Control) this._capitalizeRB);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this._upperCaseRB, "_upperCaseRB");
    this._upperCaseRB.Name = "_upperCaseRB";
    this._upperCaseRB.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._capitalizeRB, "_capitalizeRB");
    this._capitalizeRB.Checked = true;
    this._capitalizeRB.Name = "_capitalizeRB";
    this._capitalizeRB.TabStop = true;
    this._capitalizeRB.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this.btOk;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btCancel;
    this.Controls.Add((Control) this.groupBox1);
    this.Controls.Add((Control) this.btCancel);
    this.Controls.Add((Control) this.btOk);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.Name = nameof (CapitalizeFoldersDialog);
    this.ShowInTaskbar = false;
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.ResumeLayout(false);
  }
}
