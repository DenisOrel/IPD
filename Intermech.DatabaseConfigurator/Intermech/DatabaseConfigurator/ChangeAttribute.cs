// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.ChangeAttribute
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.DatabaseConfigurator;

public class ChangeAttribute : Form
{
  public int ObjectType = -1;
  public int SourceAttribute;
  public int DestignationAttribute;
  private IContainer components;
  private ButtonEdit buttonEdit1;
  private CheckBox checkBox1;
  private GroupBox groupBox1;
  private GroupBox groupBox2;
  private Button button2;
  private Button button1;
  private Label label2;
  private Label label1;
  private ButtonEdit buttonEdit3;
  private ButtonEdit buttonEdit2;

  public bool Recursive => this.checkBox1.Checked;

  public ChangeAttribute() => this.InitializeComponent();

  private void buttonEdit1_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("DatabaseConfigurator_147"), typeof (ObjectTypeFolder), false);
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count == 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectType objectType = sessionKeeper.Session.GetObjectType(Convert.ToInt32(selectorForm.IDList[0]));
      this.buttonEdit1.Text = objectType.ObjectTypeName;
      this.ObjectType = objectType.ObjectType;
    }
  }

  private int attr_ButtonClick(object sender, ButtonPressedEventArgs e, ButtonEdit button)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (AttributesFolder), LocalizationHolder.rm.GetString("DatabaseConfigurator_148"), typeof (AttributeFolder), false);
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count == 0)
      return 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(Convert.ToInt32(selectorForm.IDList[0]));
      button.Text = attributeType.Name;
      return attributeType.AttributeID;
    }
  }

  private void buttonEdit2_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    int num = this.attr_ButtonClick(sender, e, this.buttonEdit2);
    if (num == 0)
      return;
    this.SourceAttribute = num;
  }

  private void buttonEdit3_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    int num = this.attr_ButtonClick(sender, e, this.buttonEdit3);
    if (num == 0)
      return;
    this.DestignationAttribute = num;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ChangeAttribute));
    this.buttonEdit1 = new ButtonEdit();
    this.checkBox1 = new CheckBox();
    this.groupBox1 = new GroupBox();
    this.groupBox2 = new GroupBox();
    this.button2 = new Button();
    this.button1 = new Button();
    this.label2 = new Label();
    this.label1 = new Label();
    this.buttonEdit3 = new ButtonEdit();
    this.buttonEdit2 = new ButtonEdit();
    this.buttonEdit1.Properties.BeginInit();
    this.groupBox1.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.buttonEdit3.Properties.BeginInit();
    this.buttonEdit2.Properties.BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.buttonEdit1, "buttonEdit1");
    this.buttonEdit1.Name = "buttonEdit1";
    this.buttonEdit1.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.buttonEdit1.Properties.ReadOnly = true;
    this.buttonEdit1.ButtonClick += new ButtonPressedEventHandler(this.buttonEdit1_ButtonClick);
    componentResourceManager.ApplyResources((object) this.checkBox1, "checkBox1");
    this.checkBox1.Name = "checkBox1";
    this.checkBox1.UseVisualStyleBackColor = true;
    this.groupBox1.Controls.Add((Control) this.buttonEdit1);
    this.groupBox1.Controls.Add((Control) this.checkBox1);
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    this.groupBox2.Controls.Add((Control) this.button2);
    this.groupBox2.Controls.Add((Control) this.button1);
    this.groupBox2.Controls.Add((Control) this.label2);
    this.groupBox2.Controls.Add((Control) this.label1);
    this.groupBox2.Controls.Add((Control) this.buttonEdit3);
    this.groupBox2.Controls.Add((Control) this.buttonEdit2);
    componentResourceManager.ApplyResources((object) this.groupBox2, "groupBox2");
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.TabStop = false;
    this.button2.DialogResult = DialogResult.Cancel;
    componentResourceManager.ApplyResources((object) this.button2, "button2");
    this.button2.Name = "button2";
    this.button2.UseVisualStyleBackColor = true;
    this.button1.DialogResult = DialogResult.OK;
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.Name = "button1";
    this.button1.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.buttonEdit3, "buttonEdit3");
    this.buttonEdit3.Name = "buttonEdit3";
    this.buttonEdit3.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.buttonEdit3.Properties.ReadOnly = true;
    this.buttonEdit3.ButtonClick += new ButtonPressedEventHandler(this.buttonEdit3_ButtonClick);
    componentResourceManager.ApplyResources((object) this.buttonEdit2, "buttonEdit2");
    this.buttonEdit2.Name = "buttonEdit2";
    this.buttonEdit2.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.buttonEdit2.Properties.ReadOnly = true;
    this.buttonEdit2.ButtonClick += new ButtonPressedEventHandler(this.buttonEdit2_ButtonClick);
    this.AcceptButton = (IButtonControl) this.button1;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.button2;
    this.Controls.Add((Control) this.groupBox2);
    this.Controls.Add((Control) this.groupBox1);
    this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    this.Name = nameof (ChangeAttribute);
    this.ShowInTaskbar = false;
    this.Tag = (object) " ";
    this.buttonEdit1.Properties.EndInit();
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.groupBox2.ResumeLayout(false);
    this.groupBox2.PerformLayout();
    this.buttonEdit3.Properties.EndInit();
    this.buttonEdit2.Properties.EndInit();
    this.ResumeLayout(false);
  }
}
