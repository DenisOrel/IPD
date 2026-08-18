// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.ComplectTemplateDlg
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

/// <summary>Summary description for UserPrompt.</summary>
public class ComplectTemplateDlg : Form
{
  private Label label1;
  private TextBox edName;
  private Button button1;
  private Button button2;
  private ButtonEdit beObjectType;
  private Label label2;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;
  public int objTypeId = -1;
  public string name = "";

  public ComplectTemplateDlg()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1334);
  }

  public bool Execute() => this.ShowDialog() == DialogResult.OK;

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ComplectTemplateDlg));
    this.label1 = new Label();
    this.edName = new TextBox();
    this.button1 = new Button();
    this.button2 = new Button();
    this.beObjectType = new ButtonEdit();
    this.label2 = new Label();
    this.beObjectType.Properties.BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.edName, "edName");
    this.edName.Name = "edName";
    this.button1.DialogResult = DialogResult.OK;
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.Name = "button1";
    this.button1.Click += new EventHandler(this.button1_Click);
    this.button2.DialogResult = DialogResult.Cancel;
    componentResourceManager.ApplyResources((object) this.button2, "button2");
    this.button2.Name = "button2";
    componentResourceManager.ApplyResources((object) this.beObjectType, "beObjectType");
    this.beObjectType.Name = "beObjectType";
    this.beObjectType.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.beObjectType.Properties.ReadOnly = true;
    this.beObjectType.Properties.ButtonClick += new ButtonPressedEventHandler(this.beObjectType_Properties_ButtonClick);
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.beObjectType);
    this.Controls.Add((Control) this.button2);
    this.Controls.Add((Control) this.button1);
    this.Controls.Add((Control) this.edName);
    this.Controls.Add((Control) this.label1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ComplectTemplateDlg);
    this.ShowInTaskbar = false;
    this.Tag = (object) "";
    this.beObjectType.Properties.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private void beObjectType_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("Expert.Editor_555"), typeof (ObjectTypeFolder), false);
    new ArrayList() { (object) this.objTypeId };
    if (selectorForm.ShowDialog() != DialogResult.OK)
      return;
    this.objTypeId = Convert.ToInt32(selectorForm.IDList[0]);
    this.beObjectType.Text = Convert.ToString(selectorForm.NameList[0]);
  }

  private void button1_Click(object sender, EventArgs e)
  {
    this.name = this.edName.Text;
    if (this.name == "")
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_556"), LocalizationHolder.rm.GetString("Expert.Editor_107"), MessageBoxButtons.OK);
      this.DialogResult = DialogResult.None;
    }
    if (this.objTypeId != -1)
      return;
    int num1 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_557"), LocalizationHolder.rm.GetString("Expert.Editor_107"), MessageBoxButtons.OK);
    this.DialogResult = DialogResult.None;
  }
}
