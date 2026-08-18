
// Type: Intermech.Navigator.Conditions.AttributeConditionControls.EditControlDialog
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.Conditions.AttributeConditionControls;

internal class EditControlDialog : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel pControl;
  private Button bOK;
  private Button bCancel;

  public EditControlDialog() => this.InitializeComponent();

  public void SetControl(Control control)
  {
    this.pControl.Controls.Add(control);
    control.Dock = DockStyle.Fill;
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
    this.pControl = new Panel();
    this.bOK = new Button();
    this.bCancel = new Button();
    this.SuspendLayout();
    this.pControl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.pControl.Location = new Point(42, 29);
    this.pControl.Name = "pControl";
    this.pControl.Size = new Size(248, 23);
    this.pControl.TabIndex = 2;
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Location = new Point(43, 67);
    this.bOK.Name = "bOK";
    this.bOK.Size = new Size(121, 27);
    this.bOK.TabIndex = 3;
    this.bOK.Text = "ОК";
    this.bOK.UseVisualStyleBackColor = true;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(170, 67);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 4;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this.bOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(332, 118);
    this.Controls.Add((Control) this.bCancel);
    this.Controls.Add((Control) this.bOK);
    this.Controls.Add((Control) this.pControl);
    this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    this.Name = nameof (EditControlDialog);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Новое значение";
    this.ResumeLayout(false);
  }
}
