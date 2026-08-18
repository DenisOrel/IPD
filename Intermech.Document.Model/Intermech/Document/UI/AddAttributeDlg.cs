// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.AddAttributeDlg
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Диалог добавления/удаления дополнительных атрибутов</summary>
public class AddAttributeDlg : Form
{
  private DocumentTreeNode node;
  private Button okButton;
  private Button cancelButton;
  private Label label1;
  private TextBox nameTextBox;
  private TextBox valueTextBox;
  private Label label2;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;
  private bool closeBlocked;

  /// <summary>Создать и вызвать диалог</summary>
  /// <param name="node">Узел владелец атрибутов</param>
  /// <param name="attributeName">Имя атрибута</param>
  /// <param name="attributeValue">Значение атрибута</param>
  /// <returns>true, если OK</returns>
  public static bool ExecuteDialog(
    DocumentTreeNode node,
    out string attributeName,
    out string attributeValue)
  {
    return new AddAttributeDlg().Execute(node, out attributeName, out attributeValue);
  }

  /// <summary>Вызвать диалог</summary>
  /// <param name="node">Узел владелец атрибутов</param>
  /// <param name="attributeName">Имя атрибута</param>
  /// <param name="attributeValue">Значение атрибута</param>
  /// <returns>true, если OK</returns>
  public bool Execute(DocumentTreeNode node, out string attributeName, out string attributeValue)
  {
    attributeName = (string) null;
    attributeValue = (string) null;
    this.node = node;
    if (this.ShowDialog() != DialogResult.OK)
      return false;
    attributeName = this.nameTextBox.Text;
    attributeValue = this.valueTextBox.Text;
    return true;
  }

  /// <summary>Конструктор</summary>
  public AddAttributeDlg() => this.InitializeComponent();

  /// <summary>Clean up any resources being used.</summary>
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AddAttributeDlg));
    this.okButton = new Button();
    this.cancelButton = new Button();
    this.label1 = new Label();
    this.nameTextBox = new TextBox();
    this.valueTextBox = new TextBox();
    this.label2 = new Label();
    this.SuspendLayout();
    this.okButton.DialogResult = DialogResult.OK;
    componentResourceManager.ApplyResources((object) this.okButton, "okButton");
    this.okButton.Name = "okButton";
    this.okButton.Click += new EventHandler(this.okButton_Click);
    this.cancelButton.DialogResult = DialogResult.Cancel;
    componentResourceManager.ApplyResources((object) this.cancelButton, "cancelButton");
    this.cancelButton.Name = "cancelButton";
    this.cancelButton.Click += new EventHandler(this.cancelButton_Click);
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.nameTextBox, "nameTextBox");
    this.nameTextBox.Name = "nameTextBox";
    this.nameTextBox.TextChanged += new EventHandler(this.nameTextBox_TextChanged);
    componentResourceManager.ApplyResources((object) this.valueTextBox, "valueTextBox");
    this.valueTextBox.Name = "valueTextBox";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    this.AcceptButton = (IButtonControl) this.okButton;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Dpi;
    this.CancelButton = (IButtonControl) this.cancelButton;
    this.Controls.Add((Control) this.valueTextBox);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.nameTextBox);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.cancelButton);
    this.Controls.Add((Control) this.okButton);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.Name = nameof (AddAttributeDlg);
    this.ShowInTaskbar = false;
    this.Tag = (object) " ";
    this.Closing += new CancelEventHandler(this.AddAttributeDlg_Closing);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private void okButton_Click(object sender, EventArgs e)
  {
    this.closeBlocked = false;
    if (this.nameTextBox.Text == "" || this.nameTextBox.Text == null)
    {
      this.closeBlocked = true;
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Document.Model_1"));
    }
    else
    {
      if (!this.node.ContainsAttribute(this.nameTextBox.Text))
        return;
      this.closeBlocked = true;
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Document.Model_2"));
    }
  }

  private void AddAttributeDlg_Closing(object sender, CancelEventArgs e)
  {
    if (!this.closeBlocked)
      return;
    e.Cancel = true;
  }

  private void cancelButton_Click(object sender, EventArgs e) => this.closeBlocked = false;

  private void nameTextBox_TextChanged(object sender, EventArgs e)
  {
    this.okButton.Enabled = this.nameTextBox.Text != "" && this.nameTextBox.Text != null;
  }
}
