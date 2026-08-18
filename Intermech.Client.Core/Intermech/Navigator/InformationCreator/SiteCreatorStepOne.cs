
// Type: Intermech.Navigator.InformationCreator.SiteCreatorStepOne
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.History;
using Intermech.Client.Core.ObjectCreator;
using Intermech.Client.Core.ObjectCreator.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.InformationCreator;

/// <summary>
/// первый диалог в мастере создания Узел информационной системы
/// </summary>
public class SiteCreatorStepOne : ObjectCreatorControl
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panel1;
  private Label label1;
  private PictureBox pictureBox1;
  private Panel panel2;
  private Label labelName;
  private Label labelDescription;
  private TextBox textBoxDescription;
  private ToolTip toolTip1;
  private Button buttonName;
  private TextBox textBoxName;
  private ComboBox comboBox1;
  private Label label2;

  public SiteCreatorStepOne(CreatedObjectItem createdObject)
    : base(createdObject)
  {
    this.InitializeComponent();
    this.pictureBox1.Image = this.CreatedObject.ObjectTypeImage;
    this.label1.Text = this.CreatedObject.ObjectTypeCaption;
    List<SystemTypes> systemTypesList = new List<SystemTypes>();
    foreach (SystemTypes systemTypes in Enum.GetValues(typeof (SystemTypes)))
    {
      if (systemTypes != SystemTypes.Unknown)
        systemTypesList.Add(systemTypes);
    }
    this.comboBox1.DataSource = (object) systemTypesList;
    this.comboBox1.SelectedItem = (object) SystemTypes.IPS;
  }

  private void buttonName_Click(object sender, EventArgs e)
  {
    using (ObjectsHistory objectsHistory = new ObjectsHistory((object) this.CreatedObject.ObjectID, AttributableElements.Object, (object) MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545")))
    {
      objectsHistory.SelectedValue = (object) this.textBoxName.Text.Trim();
      if (objectsHistory.ShowDialog() != DialogResult.OK)
        return;
      this.textBoxName.Text = (string) objectsHistory.SelectedValue;
    }
  }

  public override bool Save(PageSaveArgs args)
  {
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this.CreatedObject.ObjectID, true);
        if (this.textBoxName.Text != string.Empty)
          dbObject.Attributes.AddAttribute(sessionKeeper.Session.GetAttributeType(new Guid("cad00020-306c-11d8-b4e9-00304f19f545")).AttributeID, false).AsString = this.textBoxName.Text;
        if (this.textBoxDescription.Text != string.Empty)
          dbObject.Attributes.AddAttribute(sessionKeeper.Session.GetAttributeType(new Guid("cad0001c-306c-11d8-b4e9-00304f19f545")).AttributeID, false).AsString = this.textBoxDescription.Text;
        dbObject.Attributes.AddAttribute(sessionKeeper.Session.GetAttributeType(PortalConsts.attributeSystem).AttributeID, false).AsInteger = (long) (int) this.comboBox1.SelectedItem;
        ((ISitesCacheService) sessionKeeper.Session.GetCustomService(typeof (ISitesCacheService))).Reload((object) sessionKeeper.Session.SessionGUID);
      }
      return true;
    }
    catch (NullReferenceException ex)
    {
      args.Error = (Exception) ex;
      return false;
    }
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
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SiteCreatorStepOne));
    this.panel1 = new Panel();
    this.label1 = new Label();
    this.pictureBox1 = new PictureBox();
    this.panel2 = new Panel();
    this.buttonName = new Button();
    this.textBoxName = new TextBox();
    this.textBoxDescription = new TextBox();
    this.labelDescription = new Label();
    this.labelName = new Label();
    this.toolTip1 = new ToolTip(this.components);
    this.label2 = new Label();
    this.comboBox1 = new ComboBox();
    this.panel1.SuspendLayout();
    ((ISupportInitialize) this.pictureBox1).BeginInit();
    this.panel2.SuspendLayout();
    this.SuspendLayout();
    this.panel1.BackColor = SystemColors.Control;
    this.panel1.Controls.Add((Control) this.label1);
    this.panel1.Controls.Add((Control) this.pictureBox1);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.ForeColor = SystemColors.GrayText;
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.pictureBox1, "pictureBox1");
    this.pictureBox1.Name = "pictureBox1";
    this.pictureBox1.TabStop = false;
    this.panel2.BackColor = SystemColors.ControlLight;
    this.panel2.Controls.Add((Control) this.comboBox1);
    this.panel2.Controls.Add((Control) this.label2);
    this.panel2.Controls.Add((Control) this.buttonName);
    this.panel2.Controls.Add((Control) this.textBoxName);
    this.panel2.Controls.Add((Control) this.textBoxDescription);
    this.panel2.Controls.Add((Control) this.labelDescription);
    this.panel2.Controls.Add((Control) this.labelName);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    componentResourceManager.ApplyResources((object) this.buttonName, "buttonName");
    this.buttonName.Name = "buttonName";
    this.buttonName.UseVisualStyleBackColor = true;
    this.buttonName.Click += new EventHandler(this.buttonName_Click);
    componentResourceManager.ApplyResources((object) this.textBoxName, "textBoxName");
    this.textBoxName.Name = "textBoxName";
    componentResourceManager.ApplyResources((object) this.textBoxDescription, "textBoxDescription");
    this.textBoxDescription.BackColor = SystemColors.Window;
    this.textBoxDescription.Name = "textBoxDescription";
    componentResourceManager.ApplyResources((object) this.labelDescription, "labelDescription");
    this.labelDescription.Name = "labelDescription";
    componentResourceManager.ApplyResources((object) this.labelName, "labelName");
    this.labelName.Name = "labelName";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
    this.comboBox1.FormattingEnabled = true;
    componentResourceManager.ApplyResources((object) this.comboBox1, "comboBox1");
    this.comboBox1.Name = "comboBox1";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackColor = SystemColors.Control;
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.panel1);
    this.MinimumSize = new Size(600, 300);
    this.Name = nameof (SiteCreatorStepOne);
    this.panel1.ResumeLayout(false);
    ((ISupportInitialize) this.pictureBox1).EndInit();
    this.panel2.ResumeLayout(false);
    this.panel2.PerformLayout();
    this.ResumeLayout(false);
  }
}
