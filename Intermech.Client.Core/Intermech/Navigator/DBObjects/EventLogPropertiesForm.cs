
// Type: Intermech.Navigator.DBObjects.EventLogPropertiesForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.XtraEditors;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;


namespace Intermech.Navigator.DBObjects;

public class EventLogPropertiesForm : Form
{
  public int ParentMode;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panelMain;
  private Panel panel1;
  private GroupBox groupBox1;
  private GroupBox groupBox2;
  private GroupBox groupBox3;
  private Label label2;
  private Label label1;
  private Label eDateEnd;
  private Label eDateBegin;
  private Label eUserName;
  private Label label6;
  private Label eCompName;
  private Label label8;
  private GroupBox groupBox4;
  private MemoEdit eComment;
  private Label eCategory;
  private Label label4;
  private Label eType;
  private Label label7;
  private Label eActionType;
  private Label label5;
  private Label eObjectName;
  private Label label9;
  private Label eObjectID;
  private Label label11;
  private Label eObjectVerID;
  private Label label13;
  private Label eRelationID;
  private Label label15;

  public EventLogPropertiesForm() => this.InitializeComponent();

  public void SetParent(Control aParent)
  {
    if (aParent == null)
    {
      this.AutoScaleMode = AutoScaleMode.None;
      this.TopLevel = true;
      this.Dock = DockStyle.None;
      this.FormBorderStyle = FormBorderStyle.Sizable;
      this.Visible = false;
    }
    else
    {
      this.AutoScaleMode = AutoScaleMode.None;
      this.TopLevel = false;
      this.Dock = DockStyle.Fill;
      this.FormBorderStyle = FormBorderStyle.None;
      this.Visible = true;
    }
    this.Parent = aParent;
  }

  /// <summary>Загрузить данные в форму</summary>
  public void LoadObjectData(DataRow eventRow)
  {
    this.eActionType.Text = eventRow[3].ToString();
    this.eCategory.Text = eventRow[10].ToString();
    this.eComment.Text = eventRow[9].ToString();
    this.eCompName.Text = eventRow[6].ToString();
    this.eDateBegin.Text = eventRow[1].ToString();
    this.eDateEnd.Text = eventRow[12].ToString();
    this.eObjectID.Text = eventRow[11].ToString();
    this.eObjectName.Text = eventRow[4].ToString();
    this.eObjectVerID.Text = eventRow[7].ToString();
    this.eRelationID.Text = eventRow[8].ToString();
    this.eType.Text = eventRow[0].ToString();
    this.eUserName.Text = eventRow[5].ToString();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (EventLogPropertiesForm));
    this.panelMain = new Panel();
    this.panel1 = new Panel();
    this.groupBox4 = new GroupBox();
    this.eComment = new MemoEdit();
    this.groupBox3 = new GroupBox();
    this.eCompName = new Label();
    this.label8 = new Label();
    this.eUserName = new Label();
    this.label6 = new Label();
    this.groupBox2 = new GroupBox();
    this.eDateEnd = new Label();
    this.eDateBegin = new Label();
    this.label2 = new Label();
    this.label1 = new Label();
    this.groupBox1 = new GroupBox();
    this.eRelationID = new Label();
    this.label15 = new Label();
    this.eObjectVerID = new Label();
    this.label13 = new Label();
    this.eObjectID = new Label();
    this.label11 = new Label();
    this.eObjectName = new Label();
    this.label9 = new Label();
    this.eType = new Label();
    this.label7 = new Label();
    this.eActionType = new Label();
    this.label5 = new Label();
    this.eCategory = new Label();
    this.label4 = new Label();
    this.panelMain.SuspendLayout();
    this.panel1.SuspendLayout();
    this.groupBox4.SuspendLayout();
    this.eComment.Properties.BeginInit();
    this.groupBox3.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.panelMain, "panelMain");
    this.panelMain.Controls.Add((Control) this.panel1);
    this.panelMain.Name = "panelMain";
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Controls.Add((Control) this.groupBox4);
    this.panel1.Controls.Add((Control) this.groupBox3);
    this.panel1.Controls.Add((Control) this.groupBox2);
    this.panel1.Controls.Add((Control) this.groupBox1);
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.groupBox4, "groupBox4");
    this.groupBox4.Controls.Add((Control) this.eComment);
    this.groupBox4.Name = "groupBox4";
    this.groupBox4.TabStop = false;
    componentResourceManager.ApplyResources((object) this.eComment, "eComment");
    this.eComment.Name = "eComment";
    this.eComment.Properties.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.groupBox3, "groupBox3");
    this.groupBox3.Controls.Add((Control) this.eCompName);
    this.groupBox3.Controls.Add((Control) this.label8);
    this.groupBox3.Controls.Add((Control) this.eUserName);
    this.groupBox3.Controls.Add((Control) this.label6);
    this.groupBox3.Name = "groupBox3";
    this.groupBox3.TabStop = false;
    componentResourceManager.ApplyResources((object) this.eCompName, "eCompName");
    this.eCompName.Name = "eCompName";
    componentResourceManager.ApplyResources((object) this.label8, "label8");
    this.label8.Name = "label8";
    componentResourceManager.ApplyResources((object) this.eUserName, "eUserName");
    this.eUserName.Name = "eUserName";
    componentResourceManager.ApplyResources((object) this.label6, "label6");
    this.label6.Name = "label6";
    componentResourceManager.ApplyResources((object) this.groupBox2, "groupBox2");
    this.groupBox2.Controls.Add((Control) this.eDateEnd);
    this.groupBox2.Controls.Add((Control) this.eDateBegin);
    this.groupBox2.Controls.Add((Control) this.label2);
    this.groupBox2.Controls.Add((Control) this.label1);
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.TabStop = false;
    componentResourceManager.ApplyResources((object) this.eDateEnd, "eDateEnd");
    this.eDateEnd.Name = "eDateEnd";
    componentResourceManager.ApplyResources((object) this.eDateBegin, "eDateBegin");
    this.eDateBegin.Name = "eDateBegin";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.Controls.Add((Control) this.eRelationID);
    this.groupBox1.Controls.Add((Control) this.label15);
    this.groupBox1.Controls.Add((Control) this.eObjectVerID);
    this.groupBox1.Controls.Add((Control) this.label13);
    this.groupBox1.Controls.Add((Control) this.eObjectID);
    this.groupBox1.Controls.Add((Control) this.label11);
    this.groupBox1.Controls.Add((Control) this.eObjectName);
    this.groupBox1.Controls.Add((Control) this.label9);
    this.groupBox1.Controls.Add((Control) this.eType);
    this.groupBox1.Controls.Add((Control) this.label7);
    this.groupBox1.Controls.Add((Control) this.eActionType);
    this.groupBox1.Controls.Add((Control) this.label5);
    this.groupBox1.Controls.Add((Control) this.eCategory);
    this.groupBox1.Controls.Add((Control) this.label4);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.eRelationID, "eRelationID");
    this.eRelationID.Name = "eRelationID";
    componentResourceManager.ApplyResources((object) this.label15, "label15");
    this.label15.Name = "label15";
    componentResourceManager.ApplyResources((object) this.eObjectVerID, "eObjectVerID");
    this.eObjectVerID.Name = "eObjectVerID";
    componentResourceManager.ApplyResources((object) this.label13, "label13");
    this.label13.Name = "label13";
    componentResourceManager.ApplyResources((object) this.eObjectID, "eObjectID");
    this.eObjectID.Name = "eObjectID";
    componentResourceManager.ApplyResources((object) this.label11, "label11");
    this.label11.Name = "label11";
    componentResourceManager.ApplyResources((object) this.eObjectName, "eObjectName");
    this.eObjectName.Name = "eObjectName";
    componentResourceManager.ApplyResources((object) this.label9, "label9");
    this.label9.Name = "label9";
    componentResourceManager.ApplyResources((object) this.eType, "eType");
    this.eType.Name = "eType";
    componentResourceManager.ApplyResources((object) this.label7, "label7");
    this.label7.Name = "label7";
    componentResourceManager.ApplyResources((object) this.eActionType, "eActionType");
    this.eActionType.Name = "eActionType";
    componentResourceManager.ApplyResources((object) this.label5, "label5");
    this.label5.Name = "label5";
    componentResourceManager.ApplyResources((object) this.eCategory, "eCategory");
    this.eCategory.Name = "eCategory";
    componentResourceManager.ApplyResources((object) this.label4, "label4");
    this.label4.Name = "label4";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.panelMain);
    this.Name = nameof (EventLogPropertiesForm);
    this.panelMain.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.groupBox4.ResumeLayout(false);
    this.eComment.Properties.EndInit();
    this.groupBox3.ResumeLayout(false);
    this.groupBox3.PerformLayout();
    this.groupBox2.ResumeLayout(false);
    this.groupBox2.PerformLayout();
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.ResumeLayout(false);
  }
}
