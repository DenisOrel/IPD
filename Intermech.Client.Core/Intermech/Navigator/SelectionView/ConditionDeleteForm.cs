
// Type: Intermech.Navigator.SelectionView.ConditionDeleteForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Navigator.SelectionView;

public class ConditionDeleteForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button bCancel;
  private Button bOK;
  private RadioButton rbNodeOnly;
  private RadioButton rbNodeWithChild;

  internal DeleteNodeType DeleteType
  {
    get => !this.rbNodeOnly.Checked ? DeleteNodeType.NodeWithChild : DeleteNodeType.NodeOnly;
  }

  public ConditionDeleteForm() => this.InitializeComponent();

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ConditionDeleteForm));
    this.bCancel = new Button();
    this.bOK = new Button();
    this.rbNodeOnly = new RadioButton();
    this.rbNodeWithChild = new RadioButton();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.bCancel, "bCancel");
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Name = "bCancel";
    this.bCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.bOK, "bOK");
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Name = "bOK";
    this.bOK.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.rbNodeOnly, "rbNodeOnly");
    this.rbNodeOnly.Checked = true;
    this.rbNodeOnly.Name = "rbNodeOnly";
    this.rbNodeOnly.TabStop = true;
    this.rbNodeOnly.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.rbNodeWithChild, "rbNodeWithChild");
    this.rbNodeWithChild.Name = "rbNodeWithChild";
    this.rbNodeWithChild.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this.bOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.Controls.Add((Control) this.rbNodeWithChild);
    this.Controls.Add((Control) this.rbNodeOnly);
    this.Controls.Add((Control) this.bOK);
    this.Controls.Add((Control) this.bCancel);
    this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    this.Name = nameof (ConditionDeleteForm);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
