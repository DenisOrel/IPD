
// Type: Intermech.PropertyEditors.NewPasswordForm
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
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for NewPasswordEditor.</summary>
public class NewPasswordForm : Form
{
  private PictureBox pictureBox1;
  private Label label1;
  private Label label2;
  private Label label3;
  private Button btCancel;
  private Button btOk;
  private TextBox edOld;
  private TextBox edNew;
  private TextBox edNew2;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;

  public NewPasswordForm()
  {
    this.InitializeComponent();
    this.edNew.PasswordChar = ClientConsts.PasswordChar;
    this.edNew2.PasswordChar = ClientConsts.PasswordChar;
    this.edOld.PasswordChar = ClientConsts.PasswordChar;
    this.edOld.MaxLength = Consts.MaxPasswordSize;
    this.edNew.MaxLength = Consts.MaxPasswordSize;
    this.edNew2.MaxLength = Consts.MaxPasswordSize;
  }

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (NewPasswordForm));
    this.pictureBox1 = new PictureBox();
    this.label1 = new Label();
    this.edOld = new TextBox();
    this.edNew = new TextBox();
    this.label2 = new Label();
    this.edNew2 = new TextBox();
    this.label3 = new Label();
    this.btCancel = new Button();
    this.btOk = new Button();
    ((ISupportInitialize) this.pictureBox1).BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.pictureBox1, "pictureBox1");
    this.pictureBox1.Name = "pictureBox1";
    this.pictureBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.edOld, "edOld");
    this.edOld.Name = "edOld";
    componentResourceManager.ApplyResources((object) this.edNew, "edNew");
    this.edNew.Name = "edNew";
    this.edNew.TextChanged += new EventHandler(this.edNew_TextChanged);
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.edNew2, "edNew2");
    this.edNew2.Name = "edNew2";
    this.edNew2.TextChanged += new EventHandler(this.edNew_TextChanged);
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    this.btCancel.DialogResult = DialogResult.Cancel;
    componentResourceManager.ApplyResources((object) this.btCancel, "btCancel");
    this.btCancel.Name = "btCancel";
    componentResourceManager.ApplyResources((object) this.btOk, "btOk");
    this.btOk.Name = "btOk";
    this.btOk.Click += new EventHandler(this.btOk_Click);
    this.AcceptButton = (IButtonControl) this.btOk;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.btCancel;
    this.Controls.Add((Control) this.btOk);
    this.Controls.Add((Control) this.btCancel);
    this.Controls.Add((Control) this.edNew2);
    this.Controls.Add((Control) this.label3);
    this.Controls.Add((Control) this.edNew);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.edOld);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.pictureBox1);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (NewPasswordForm);
    this.ShowInTaskbar = false;
    ((ISupportInitialize) this.pictureBox1).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private void edNew_TextChanged(object sender, EventArgs e)
  {
    this.btOk.Enabled = this.edNew.Text == this.edNew2.Text;
  }

  private void btOk_Click(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      if (!CryptHelper.IsPasswordEqual(this.edOld.Text, session.GetObject(session.UserID).GetAttributeByGuid(new Guid("cad00019-306c-11d8-b4e9-00304f19f545")).AsString))
      {
        if (this.edOld.Text.Length == 0)
        {
          int num1 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_963"), LocalizationHolder.rm.GetString("Client.Core_82"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
        else
        {
          int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_964"), LocalizationHolder.rm.GetString("Client.Core_82"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
      }
      else if (this.edNew.Text != this.edNew2.Text)
      {
        int num3 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_965"), LocalizationHolder.rm.GetString("Client.Core_82"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else
      {
        EncryptedAttributeHelper.ValidateComplexPassword(sessionKeeper.Session, this.edNew.Text);
        CryptHelper.ValidatePswRules(sessionKeeper.Session, this.edNew.Text, EncryptedAttributeHelper.GetPasswordHash(sessionKeeper.Session, this.edNew.Text), sessionKeeper.Session.UserID);
        this.DialogResult = DialogResult.OK;
      }
    }
  }

  public string NewPassword => this.edNew.Text;
}
