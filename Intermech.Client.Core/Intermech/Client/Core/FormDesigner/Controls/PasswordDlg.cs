
// Type: Intermech.Client.Core.FormDesigner.Controls.PasswordDlg
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Protection;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// 
/// </summary>
internal class PasswordDlg : Form
{
  private long _ID = -1;
  private AttributableElements _kind;
  private Guid _attribute = Guid.Empty;
  private bool _isNewObject;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button _btnOk;
  private Button _btnCancel;
  private TextBox _txtNew2;
  private TextBox _txtNew;
  private TextBox _txtOld;
  private Label label3;
  private Label label2;
  private Label label1;
  private PictureBox pictureBox1;
  private Panel panel1;
  private Panel panel2;
  private Panel panel3;
  private Panel panel4;

  /// <summary>
  /// 
  /// </summary>
  public string Password => this._txtNew.Text;

  /// <summary>Конструктор.</summary>
  public PasswordDlg()
  {
    this.InitializeComponent();
    this.Text = LocalizationHolder.rm.GetString("Client.Core_1146");
    this._txtOld.PasswordChar = ClientConsts.PasswordChar;
    this._txtNew.PasswordChar = ClientConsts.PasswordChar;
    this._txtNew2.PasswordChar = ClientConsts.PasswordChar;
    this._txtOld.MaxLength = Consts.MaxPasswordSize;
    this._txtNew.MaxLength = Consts.MaxPasswordSize;
    this._txtNew2.MaxLength = Consts.MaxPasswordSize;
  }

  /// <summary>Конструктор.</summary>
  /// <param name="ID"></param>
  /// <param name="kind"></param>
  /// <param name="attribute"></param>
  public PasswordDlg(long ID, AttributableElements kind, Guid attribute)
    : this()
  {
    this._ID = ID;
    this._kind = kind;
    this._attribute = attribute;
    if (!(ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole).IsAdmin)
      return;
    this.Text = LocalizationHolder.rm.GetString("Client.Core_1147");
    this.panel2.Visible = false;
    Size size = new Size(this.Width, this.Height - this.panel2.Height);
    this.MinimumSize = size;
    this.Size = size;
  }

  /// <summary>Конструктор.</summary>
  /// <param name="attribute"></param>
  public PasswordDlg(Guid attribute)
    : this()
  {
    this._attribute = attribute;
    this._isNewObject = true;
    this.panel2.Visible = false;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txtNew_TextChanged(object sender, EventArgs e)
  {
    this._btnOk.Enabled = string.Equals(this._txtNew.Text, this._txtNew2.Text);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnOk_Click(object sender, EventArgs e)
  {
    if (this._txtNew.Text != this._txtNew2.Text)
    {
      int num1 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1151"), LocalizationHolder.rm.GetString("Client.Core_1149"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else if (!this._isNewObject)
    {
      if (this._ID == Convert.ToInt64(-1) || this._kind == AttributableElements.None)
      {
        int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1148"), LocalizationHolder.rm.GetString("Client.Core_1149"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
        this.DialogResult = DialogResult.Cancel;
      }
      else
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBAttributable dbAttributable = (IDBAttributable) null;
          switch (this._kind)
          {
            case AttributableElements.Object:
              dbAttributable = (IDBAttributable) sessionKeeper.Session.GetObject(this._ID);
              break;
            case AttributableElements.Relation:
              dbAttributable = (IDBAttributable) sessionKeeper.Session.GetRelation(this._ID);
              break;
          }
          IDBEncryptedAttribute attributeByGuid = dbAttributable.GetAttributeByGuid(new Guid("cad00019-306c-11d8-b4e9-00304f19f545")) as IDBEncryptedAttribute;
          if (this.panel2.Visible && attributeByGuid != null && !attributeByGuid.ValidateCurrent(this._txtOld.Text))
          {
            int num3 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1150"), LocalizationHolder.rm.GetString("Client.Core_1149"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
            return;
          }
          if (attributeByGuid != null)
          {
            EncryptedAttributeHelper.ValidateComplexPassword(sessionKeeper.Session, this._txtNew.Text);
            CryptHelper.ValidatePswRules(sessionKeeper.Session, this._txtNew.Text, EncryptedAttributeHelper.GetPasswordHash(sessionKeeper.Session, this._txtNew.Text), sessionKeeper.Session.UserID);
          }
        }
      }
    }
    this.DialogResult = DialogResult.OK;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PasswordDlg));
    this._btnOk = new Button();
    this._btnCancel = new Button();
    this._txtNew2 = new TextBox();
    this._txtNew = new TextBox();
    this._txtOld = new TextBox();
    this.label3 = new Label();
    this.label2 = new Label();
    this.label1 = new Label();
    this.pictureBox1 = new PictureBox();
    this.panel1 = new Panel();
    this.panel2 = new Panel();
    this.panel3 = new Panel();
    this.panel4 = new Panel();
    ((ISupportInitialize) this.pictureBox1).BeginInit();
    this.panel1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.panel3.SuspendLayout();
    this.panel4.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._btnOk, "_btnOk");
    this._btnOk.Name = "_btnOk";
    this._btnOk.Click += new EventHandler(this.On_btnOk_Click);
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Name = "_btnCancel";
    componentResourceManager.ApplyResources((object) this._txtNew2, "_txtNew2");
    this._txtNew2.Name = "_txtNew2";
    this._txtNew2.TextChanged += new EventHandler(this.On_txtNew_TextChanged);
    componentResourceManager.ApplyResources((object) this._txtNew, "_txtNew");
    this._txtNew.Name = "_txtNew";
    this._txtNew.TextChanged += new EventHandler(this.On_txtNew_TextChanged);
    componentResourceManager.ApplyResources((object) this._txtOld, "_txtOld");
    this._txtOld.Name = "_txtOld";
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.pictureBox1, "pictureBox1");
    this.pictureBox1.Name = "pictureBox1";
    this.pictureBox1.TabStop = false;
    this.panel1.Controls.Add((Control) this._btnOk);
    this.panel1.Controls.Add((Control) this._btnCancel);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    this.panel2.Controls.Add((Control) this._txtOld);
    this.panel2.Controls.Add((Control) this.label1);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    this.panel3.Controls.Add((Control) this._txtNew);
    this.panel3.Controls.Add((Control) this.label2);
    componentResourceManager.ApplyResources((object) this.panel3, "panel3");
    this.panel3.Name = "panel3";
    this.panel4.Controls.Add((Control) this._txtNew2);
    this.panel4.Controls.Add((Control) this.label3);
    componentResourceManager.ApplyResources((object) this.panel4, "panel4");
    this.panel4.Name = "panel4";
    this.AcceptButton = (IButtonControl) this._btnOk;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this._btnCancel;
    this.Controls.Add((Control) this.panel4);
    this.Controls.Add((Control) this.panel3);
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.pictureBox1);
    this.DoubleBuffered = true;
    this.FormBorderStyle = FormBorderStyle.FixedSingle;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (PasswordDlg);
    this.ShowIcon = false;
    this.Tag = (object) " ";
    ((ISupportInitialize) this.pictureBox1).EndInit();
    this.panel1.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.panel2.PerformLayout();
    this.panel3.ResumeLayout(false);
    this.panel3.PerformLayout();
    this.panel4.ResumeLayout(false);
    this.panel4.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
