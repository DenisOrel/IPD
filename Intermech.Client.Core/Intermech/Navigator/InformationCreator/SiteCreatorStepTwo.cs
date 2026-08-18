
// Type: Intermech.Navigator.InformationCreator.SiteCreatorStepTwo
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Client.Core.ObjectCreator;
using Intermech.Client.Core.ObjectCreator.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.WebPortal;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.InformationCreator;

/// <summary>
/// второй диалог в мастере создания Узел информационной системы
/// </summary>
public class SiteCreatorStepTwo : ObjectCreatorControl
{
  private long _userID1;
  private long _userID2;
  private bool _first = true;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panel1;
  private Label label1;
  private PictureBox pictureBox1;
  private ToolTip toolTip1;
  private Label labelName;
  private Label labelRealName;
  private Panel panel2;
  private GroupBox groupBox2;
  private Label label2;
  private Button buttonPassword2;
  private Label label3;
  private TextBox textBoxPassword2;
  private Label label4;
  private TextBox textBoxFullName2;
  private TextBox textBoxLogin2;
  private GroupBox groupBox1;
  private Label labelFullName;
  private Button buttonPassword1;
  private Label labelPassword;
  private TextBox textBoxPassword1;
  private Label labelLogin;
  private TextBox textBoxFullName1;
  private TextBox textBoxLogin1;

  /// <summary>
  /// содержит текущие идентификаторы выбранных пользователей, кроме только что созданного
  /// </summary>
  public SiteCreatorStepTwo(CreatedObjectItem createdObject)
    : base(createdObject)
  {
    this.InitializeComponent();
    this.pictureBox1.Image = this.CreatedObject.ObjectTypeImage;
    this.label1.Text = this.CreatedObject.ObjectTypeCaption;
    this.textBoxPassword1.PasswordChar = this.textBoxPassword2.PasswordChar = ClientConsts.PasswordChar;
    this.textBoxPassword1.MaxLength = this.textBoxPassword2.MaxLength = Intermech.Consts.MaxPasswordSize;
    this.textBoxFullName1.Text = LocalizationHolder.rm.GetString("Client.Core_1532");
    this.textBoxFullName2.Text = LocalizationHolder.rm.GetString("Client.Core_1533");
  }

  public override bool Save(PageSaveArgs args)
  {
    if (args != null && args.NextPageIndex == 0)
      return true;
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBTransactions customService = (IDBTransactions) sessionKeeper.Session.GetCustomService(typeof (IDBTransactions));
        customService.StartTransaction();
        try
        {
          sessionKeeper.Session.GetObject(this.CreatedObject.ObjectID);
          char siteCode = ((ISitesCacheService) sessionKeeper.Session.GetCustomService(typeof (ISitesCacheService))).NextCode();
          IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(sessionKeeper.Session.IdentHelper.UsersTypeID);
          IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(MetaDataHelper.GetRelationTypeID("cad00022-306c-11d8-b4e9-00304f19f545"));
          IDBObject dbObject1 = objectCollection.Create();
          dbObject1.Attributes.AddAttribute(sessionKeeper.Session.GetAttributeType(new Guid("cad0001d-306c-11d8-b4e9-00304f19f545")).AttributeID, false).AsString = this.textBoxFullName1.Text;
          dbObject1.Attributes.AddAttribute(sessionKeeper.Session.GetAttributeType(new Guid("cad00018-306c-11d8-b4e9-00304f19f545")).AttributeID, false).AsString = PortalConsts.GlobalLoginName(siteCode, this.textBoxLogin1.Text);
          dbObject1.Attributes.AddAttribute(sessionKeeper.Session.GetAttributeType(new Guid("cad00019-306c-11d8-b4e9-00304f19f545")).AttributeID, false).AsString = this.textBoxPassword1.Text;
          IDBObject dbObject2 = sessionKeeper.Session.GetObject(PortalConsts.objectReplicatorRole, true);
          relationCollection.Create(dbObject2.ObjectID, dbObject1.ObjectID);
          relationCollection.Create(this.CreatedObject.ObjectID, dbObject1.ObjectID);
          dbObject1.CommitCreation(true);
          this._userID1 = dbObject1.ObjectID;
          IDBObject dbObject3 = objectCollection.Create();
          dbObject3.Attributes.AddAttribute(sessionKeeper.Session.GetAttributeType(new Guid("cad0001d-306c-11d8-b4e9-00304f19f545")).AttributeID, false).AsString = this.textBoxFullName2.Text;
          dbObject3.Attributes.AddAttribute(sessionKeeper.Session.GetAttributeType(new Guid("cad00018-306c-11d8-b4e9-00304f19f545")).AttributeID, false).AsString = PortalConsts.GlobalLoginName(siteCode, this.textBoxLogin2.Text);
          dbObject3.Attributes.AddAttribute(sessionKeeper.Session.GetAttributeType(new Guid("cad00019-306c-11d8-b4e9-00304f19f545")).AttributeID, false).AsString = this.textBoxPassword2.Text;
          relationCollection.Create(sessionKeeper.Session.IdentHelper.AdminRoleID, dbObject3.ObjectID);
          relationCollection.Create(this.CreatedObject.ObjectID, dbObject3.ObjectID);
          dbObject3.CommitCreation(true);
          this._userID2 = dbObject3.ObjectID;
          customService.Commit();
        }
        catch
        {
          customService.Rollback();
          throw;
        }
      }
      return true;
    }
    catch (Exception ex)
    {
      args.Error = ex;
      return false;
    }
  }

  internal void DeleteUsers()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBTransactions customService = (IDBTransactions) sessionKeeper.Session.GetCustomService(typeof (IDBTransactions));
      customService.StartTransaction();
      try
      {
        if (this._userID1 != 0L)
          sessionKeeper.Session.GetObject(this._userID1, false)?.Delete(0L);
        if (this._userID2 != 0L)
          sessionKeeper.Session.GetObject(this._userID2, false)?.Delete(0L);
        customService.Commit();
      }
      catch
      {
        customService.Rollback();
        throw;
      }
    }
  }

  public override bool Refresh(PageRefreshArgs args)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.labelRealName.Text = sessionKeeper.Session.GetObject(this.CreatedObject.ObjectID, true).Attributes.AddAttribute(sessionKeeper.Session.GetAttributeType(new Guid("cad00020-306c-11d8-b4e9-00304f19f545")).AttributeID, false).AsString;
      if (this._first)
      {
        if (this.labelRealName.Text != string.Empty)
        {
          this.textBoxFullName1.Text = $"{this.textBoxFullName1.Text} {this.labelRealName.Text}";
          this.textBoxFullName2.Text = $"{this.textBoxFullName2.Text} {this.labelRealName.Text}";
        }
        this._first = false;
      }
    }
    return base.Refresh(args);
  }

  private void buttonPassword1_Click(object sender, EventArgs e)
  {
    using (PasswordDlg passwordDlg = new PasswordDlg(new Guid("cad00019-306c-11d8-b4e9-00304f19f545")))
    {
      if (passwordDlg.ShowDialog() != DialogResult.OK)
        return;
      this.textBoxPassword1.Text = passwordDlg.Password;
    }
  }

  private void buttonPassword2_Click(object sender, EventArgs e)
  {
    using (PasswordDlg passwordDlg = new PasswordDlg(new Guid("cad00019-306c-11d8-b4e9-00304f19f545")))
    {
      if (passwordDlg.ShowDialog() != DialogResult.OK)
        return;
      this.textBoxPassword2.Text = passwordDlg.Password;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SiteCreatorStepTwo));
    this.panel1 = new Panel();
    this.label1 = new Label();
    this.pictureBox1 = new PictureBox();
    this.toolTip1 = new ToolTip(this.components);
    this.labelName = new Label();
    this.labelRealName = new Label();
    this.panel2 = new Panel();
    this.groupBox2 = new GroupBox();
    this.label2 = new Label();
    this.buttonPassword2 = new Button();
    this.label3 = new Label();
    this.textBoxPassword2 = new TextBox();
    this.label4 = new Label();
    this.textBoxFullName2 = new TextBox();
    this.textBoxLogin2 = new TextBox();
    this.groupBox1 = new GroupBox();
    this.labelFullName = new Label();
    this.buttonPassword1 = new Button();
    this.labelPassword = new Label();
    this.textBoxPassword1 = new TextBox();
    this.labelLogin = new Label();
    this.textBoxFullName1 = new TextBox();
    this.textBoxLogin1 = new TextBox();
    this.panel1.SuspendLayout();
    ((ISupportInitialize) this.pictureBox1).BeginInit();
    this.panel2.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.BackColor = SystemColors.Control;
    this.panel1.Controls.Add((Control) this.label1);
    this.panel1.Controls.Add((Control) this.pictureBox1);
    this.panel1.Name = "panel1";
    this.toolTip1.SetToolTip((Control) this.panel1, componentResourceManager.GetString("panel1.ToolTip"));
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.ForeColor = SystemColors.GrayText;
    this.label1.Name = "label1";
    this.toolTip1.SetToolTip((Control) this.label1, componentResourceManager.GetString("label1.ToolTip"));
    componentResourceManager.ApplyResources((object) this.pictureBox1, "pictureBox1");
    this.pictureBox1.Name = "pictureBox1";
    this.pictureBox1.TabStop = false;
    this.toolTip1.SetToolTip((Control) this.pictureBox1, componentResourceManager.GetString("pictureBox1.ToolTip"));
    componentResourceManager.ApplyResources((object) this.labelName, "labelName");
    this.labelName.Name = "labelName";
    this.toolTip1.SetToolTip((Control) this.labelName, componentResourceManager.GetString("labelName.ToolTip"));
    componentResourceManager.ApplyResources((object) this.labelRealName, "labelRealName");
    this.labelRealName.Name = "labelRealName";
    this.toolTip1.SetToolTip((Control) this.labelRealName, componentResourceManager.GetString("labelRealName.ToolTip"));
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.BackColor = SystemColors.ControlLight;
    this.panel2.Controls.Add((Control) this.groupBox2);
    this.panel2.Controls.Add((Control) this.groupBox1);
    this.panel2.Controls.Add((Control) this.labelRealName);
    this.panel2.Controls.Add((Control) this.labelName);
    this.panel2.Name = "panel2";
    this.toolTip1.SetToolTip((Control) this.panel2, componentResourceManager.GetString("panel2.ToolTip"));
    componentResourceManager.ApplyResources((object) this.groupBox2, "groupBox2");
    this.groupBox2.Controls.Add((Control) this.label2);
    this.groupBox2.Controls.Add((Control) this.buttonPassword2);
    this.groupBox2.Controls.Add((Control) this.label3);
    this.groupBox2.Controls.Add((Control) this.textBoxPassword2);
    this.groupBox2.Controls.Add((Control) this.label4);
    this.groupBox2.Controls.Add((Control) this.textBoxFullName2);
    this.groupBox2.Controls.Add((Control) this.textBoxLogin2);
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.TabStop = false;
    this.toolTip1.SetToolTip((Control) this.groupBox2, componentResourceManager.GetString("groupBox2.ToolTip"));
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    this.toolTip1.SetToolTip((Control) this.label2, componentResourceManager.GetString("label2.ToolTip"));
    componentResourceManager.ApplyResources((object) this.buttonPassword2, "buttonPassword2");
    this.buttonPassword2.Name = "buttonPassword2";
    this.toolTip1.SetToolTip((Control) this.buttonPassword2, componentResourceManager.GetString("buttonPassword2.ToolTip"));
    this.buttonPassword2.UseVisualStyleBackColor = true;
    this.buttonPassword2.Click += new EventHandler(this.buttonPassword2_Click);
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    this.toolTip1.SetToolTip((Control) this.label3, componentResourceManager.GetString("label3.ToolTip"));
    componentResourceManager.ApplyResources((object) this.textBoxPassword2, "textBoxPassword2");
    this.textBoxPassword2.BackColor = SystemColors.Window;
    this.textBoxPassword2.Name = "textBoxPassword2";
    this.textBoxPassword2.ReadOnly = true;
    this.toolTip1.SetToolTip((Control) this.textBoxPassword2, componentResourceManager.GetString("textBoxPassword2.ToolTip"));
    componentResourceManager.ApplyResources((object) this.label4, "label4");
    this.label4.Name = "label4";
    this.toolTip1.SetToolTip((Control) this.label4, componentResourceManager.GetString("label4.ToolTip"));
    componentResourceManager.ApplyResources((object) this.textBoxFullName2, "textBoxFullName2");
    this.textBoxFullName2.Name = "textBoxFullName2";
    this.toolTip1.SetToolTip((Control) this.textBoxFullName2, componentResourceManager.GetString("textBoxFullName2.ToolTip"));
    componentResourceManager.ApplyResources((object) this.textBoxLogin2, "textBoxLogin2");
    this.textBoxLogin2.Name = "textBoxLogin2";
    this.toolTip1.SetToolTip((Control) this.textBoxLogin2, componentResourceManager.GetString("textBoxLogin2.ToolTip"));
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.Controls.Add((Control) this.labelFullName);
    this.groupBox1.Controls.Add((Control) this.buttonPassword1);
    this.groupBox1.Controls.Add((Control) this.labelPassword);
    this.groupBox1.Controls.Add((Control) this.textBoxPassword1);
    this.groupBox1.Controls.Add((Control) this.labelLogin);
    this.groupBox1.Controls.Add((Control) this.textBoxFullName1);
    this.groupBox1.Controls.Add((Control) this.textBoxLogin1);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    this.toolTip1.SetToolTip((Control) this.groupBox1, componentResourceManager.GetString("groupBox1.ToolTip"));
    componentResourceManager.ApplyResources((object) this.labelFullName, "labelFullName");
    this.labelFullName.Name = "labelFullName";
    this.toolTip1.SetToolTip((Control) this.labelFullName, componentResourceManager.GetString("labelFullName.ToolTip"));
    componentResourceManager.ApplyResources((object) this.buttonPassword1, "buttonPassword1");
    this.buttonPassword1.Name = "buttonPassword1";
    this.toolTip1.SetToolTip((Control) this.buttonPassword1, componentResourceManager.GetString("buttonPassword1.ToolTip"));
    this.buttonPassword1.UseVisualStyleBackColor = true;
    this.buttonPassword1.Click += new EventHandler(this.buttonPassword1_Click);
    componentResourceManager.ApplyResources((object) this.labelPassword, "labelPassword");
    this.labelPassword.Name = "labelPassword";
    this.toolTip1.SetToolTip((Control) this.labelPassword, componentResourceManager.GetString("labelPassword.ToolTip"));
    componentResourceManager.ApplyResources((object) this.textBoxPassword1, "textBoxPassword1");
    this.textBoxPassword1.BackColor = SystemColors.Window;
    this.textBoxPassword1.Name = "textBoxPassword1";
    this.textBoxPassword1.ReadOnly = true;
    this.toolTip1.SetToolTip((Control) this.textBoxPassword1, componentResourceManager.GetString("textBoxPassword1.ToolTip"));
    componentResourceManager.ApplyResources((object) this.labelLogin, "labelLogin");
    this.labelLogin.Name = "labelLogin";
    this.toolTip1.SetToolTip((Control) this.labelLogin, componentResourceManager.GetString("labelLogin.ToolTip"));
    componentResourceManager.ApplyResources((object) this.textBoxFullName1, "textBoxFullName1");
    this.textBoxFullName1.Name = "textBoxFullName1";
    this.toolTip1.SetToolTip((Control) this.textBoxFullName1, componentResourceManager.GetString("textBoxFullName1.ToolTip"));
    componentResourceManager.ApplyResources((object) this.textBoxLogin1, "textBoxLogin1");
    this.textBoxLogin1.Name = "textBoxLogin1";
    this.toolTip1.SetToolTip((Control) this.textBoxLogin1, componentResourceManager.GetString("textBoxLogin1.ToolTip"));
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.panel1);
    this.Name = nameof (SiteCreatorStepTwo);
    this.toolTip1.SetToolTip((Control) this, componentResourceManager.GetString("$this.ToolTip"));
    this.panel1.ResumeLayout(false);
    ((ISupportInitialize) this.pictureBox1).EndInit();
    this.panel2.ResumeLayout(false);
    this.panel2.PerformLayout();
    this.groupBox2.ResumeLayout(false);
    this.groupBox2.PerformLayout();
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.ResumeLayout(false);
  }
}
