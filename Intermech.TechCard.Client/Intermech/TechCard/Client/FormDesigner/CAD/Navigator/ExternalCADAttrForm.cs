// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.FormDesigner.CAD.Navigator.ExternalCADAttrForm
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.FormDesigner.CAD.Navigator;

/// <summary>
/// 
/// </summary>
public class ExternalCADAttrForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TableLayoutPanel tableLayoutPanel1;
  private Button btnOK;
  private RichTextBox richTextBox;

  /// <summary>Constructor</summary>
  /// <param name="data"></param>
  public ExternalCADAttrForm(string[] data)
  {
    this.InitializeComponent();
    StringBuilder stringBuilder = new StringBuilder(data.Length);
    foreach (string str1 in data)
    {
      string str2 = str1.Replace('\a', ' ');
      stringBuilder.Append(str2 + "\n");
    }
    this.richTextBox.Text = stringBuilder.ToString();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ExternalCADAttrForm));
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this.btnOK = new Button();
    this.richTextBox = new RichTextBox();
    this.tableLayoutPanel1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.tableLayoutPanel1, "tableLayoutPanel1");
    this.tableLayoutPanel1.Controls.Add((Control) this.btnOK, 2, 1);
    this.tableLayoutPanel1.Controls.Add((Control) this.richTextBox, 0, 0);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.btnOK.DialogResult = DialogResult.OK;
    componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
    this.btnOK.Name = "btnOK";
    this.btnOK.UseVisualStyleBackColor = true;
    this.tableLayoutPanel1.SetColumnSpan((Control) this.richTextBox, 3);
    componentResourceManager.ApplyResources((object) this.richTextBox, "richTextBox");
    this.richTextBox.Name = "richTextBox";
    this.richTextBox.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.Name = nameof (ExternalCADAttrForm);
    this.ShowInTaskbar = false;
    this.tableLayoutPanel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
