// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.InputForm
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client;

public class InputForm : Form
{
  private IContainer components;
  private Panel panel1;
  private Button btnOK;
  private Button btnCancel;
  private TextBox tbValue;

  public InputForm() => this.InitializeComponent();

  public bool Execute(string FldName, ref string Value)
  {
    this.Text = $"{this.Text}\"{FldName}\"";
    this.tbValue.Text = Value;
    if (this.ShowDialog() != DialogResult.OK)
      return false;
    Value = this.tbValue.Text;
    return true;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.panel1 = new Panel();
    this.tbValue = new TextBox();
    this.btnCancel = new Button();
    this.btnOK = new Button();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.btnOK);
    this.panel1.Controls.Add((Control) this.btnCancel);
    this.panel1.Dock = DockStyle.Bottom;
    this.panel1.Location = new Point(0, 37);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(693, 33);
    this.panel1.TabIndex = 0;
    this.tbValue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbValue.Location = new Point(12, 8);
    this.tbValue.Name = "tbValue";
    this.tbValue.Size = new Size(669, 20);
    this.tbValue.TabIndex = 1;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(606, 3);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(75, 23);
    this.btnCancel.TabIndex = 0;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Location = new Point(525, 3);
    this.btnOK.Name = "btnOK";
    this.btnOK.Size = new Size(75, 23);
    this.btnOK.TabIndex = 1;
    this.btnOK.Text = "Да";
    this.btnOK.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this.btnOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(693, 70);
    this.Controls.Add((Control) this.tbValue);
    this.Controls.Add((Control) this.panel1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (InputForm);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Редактирование графы ";
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
