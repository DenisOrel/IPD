// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.RectangleDlg
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Document.Model.UI;
using Intermech.Interfaces.Document;
using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.UI;

public class RectangleDlg : Form
{
  private bool isCreateRectangleDlg = true;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button okButton;
  private Button cancelButton;
  private Button applyButton;
  private CheckBox showDlgOnCreate_CheckBox;
  private RectangleEditPanel rectangleEditPanel;

  public bool IsCreateRectangleDlg
  {
    get => this.showDlgOnCreate_CheckBox.Visible;
    set
    {
      if (this.isCreateRectangleDlg == value)
        return;
      this.showDlgOnCreate_CheckBox.Visible = value;
      this.isCreateRectangleDlg = value;
      if (value)
        this.Height = Convert.ToInt32((float) ((double) this.Height * 295.0 / 267.0));
      else
        this.Height = Convert.ToInt32((float) ((double) this.Height * 267.0 / 295.0));
    }
  }

  public RectangleDlg() => this.InitializeComponent();

  public RectangleDlg(RectangleElement element)
  {
    this.InitializeComponent();
    this.rectangleEditPanel.SetRectangleElement(element);
    this.rectangleEditPanel.SetOkApplyEnabledHandler = new SetOkApplyEnabledDelegate(this.SetOkApplyEnabled);
    this.showDlgOnCreate_CheckBox.Checked = ImDocumentEditorConfig.Instance.ShowGeometryDlgOnCreate;
  }

  public void SetOkApplyEnabled(bool okEnabled, bool applyEnabled)
  {
    this.okButton.Enabled = okEnabled;
    this.applyButton.Enabled = applyEnabled;
  }

  private void applyButton_Click(object sender, EventArgs e)
  {
    this.rectangleEditPanel.Apply();
    ImDocumentEditorConfig.Instance.ShowGeometryDlgOnCreate = this.showDlgOnCreate_CheckBox.Checked;
  }

  private void okButton_Click(object sender, EventArgs e) => this.applyButton_Click(sender, e);

  protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
  {
    return base.ProcessCmdKey(ref msg, keyData);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (RectangleDlg));
    this.okButton = new Button();
    this.cancelButton = new Button();
    this.applyButton = new Button();
    this.showDlgOnCreate_CheckBox = new CheckBox();
    this.rectangleEditPanel = new RectangleEditPanel();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.okButton, "okButton");
    this.okButton.DialogResult = DialogResult.OK;
    this.okButton.Name = "okButton";
    this.okButton.UseVisualStyleBackColor = true;
    this.okButton.Click += new EventHandler(this.okButton_Click);
    componentResourceManager.ApplyResources((object) this.cancelButton, "cancelButton");
    this.cancelButton.DialogResult = DialogResult.Cancel;
    this.cancelButton.Name = "cancelButton";
    this.cancelButton.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.applyButton, "applyButton");
    this.applyButton.Name = "applyButton";
    this.applyButton.UseVisualStyleBackColor = true;
    this.applyButton.Click += new EventHandler(this.applyButton_Click);
    componentResourceManager.ApplyResources((object) this.showDlgOnCreate_CheckBox, "showDlgOnCreate_CheckBox");
    this.showDlgOnCreate_CheckBox.Name = "showDlgOnCreate_CheckBox";
    this.showDlgOnCreate_CheckBox.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.rectangleEditPanel, "rectangleEditPanel");
    this.rectangleEditPanel.Name = "rectangleEditPanel";
    this.AcceptButton = (IButtonControl) this.okButton;
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.CancelButton = (IButtonControl) this.cancelButton;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.rectangleEditPanel);
    this.Controls.Add((Control) this.showDlgOnCreate_CheckBox);
    this.Controls.Add((Control) this.applyButton);
    this.Controls.Add((Control) this.cancelButton);
    this.Controls.Add((Control) this.okButton);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (RectangleDlg);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
