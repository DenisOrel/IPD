
// Type: Intermech.Client.Core.ChangeCurrentUserPasswordForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>
/// Форма, позволяющая выполнять смену пароля текущего пользователя (требуется ввод текущего пароля).
/// </summary>
public class ChangeCurrentUserPasswordForm : Form
{
  /// <summary>Разрешить вводить пустой пароль</summary>
  private bool _enableEmpty = true;
  /// <summary>Старый пароль</summary>
  private string _oldPassword = string.Empty;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private PictureBox pictureBox1;
  private TextBox edOld;
  private Label labelOldPassword;
  private Label labelNewPassword;
  private TextBox edNew;
  private Label labelConfirmation;
  private TextBox edConfirm;
  private Button btnOk;
  private Button btnCancel;
  private Panel panel1;

  /// <summary>Создать экземпляр класса</summary>
  public ChangeCurrentUserPasswordForm() => this.InitializeComponent();

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="oldPasword">Старый пароль</param>
  /// <param name="enalbeEmpty">Разрешить вводить пустой пароль</param>
  /// <param name="createOnly">Открыть форму в режиме нового пароля</param>
  private ChangeCurrentUserPasswordForm(string oldPasword, bool enalbeEmpty)
  {
    this.InitializeComponent();
    this.Init(oldPasword, enalbeEmpty);
  }

  /// <summary>Инициализировать элементы управления</summary>
  /// <param name="oldPasword">Старый пароль</param>
  /// <param name="enableEmpty">Разрешить вводить пустой пароль</param>
  private void Init(string oldPasword, bool enableEmpty)
  {
    this._oldPassword = oldPasword;
    this._enableEmpty = enableEmpty;
    this.edOld.PasswordChar = ClientConsts.PasswordChar;
    this.edOld.MaxLength = Consts.MaxPasswordSize;
    this.edNew.PasswordChar = ClientConsts.PasswordChar;
    this.edNew.MaxLength = Consts.MaxPasswordSize;
    this.edConfirm.PasswordChar = ClientConsts.PasswordChar;
    this.edConfirm.MaxLength = Consts.MaxPasswordSize;
    this.edNew.Focus();
    this.edNew.Text = string.Empty;
    this.edConfirm.Text = string.Empty;
    this.UpdateControls();
  }

  /// <summary>Обновить контролы</summary>
  private void UpdateControls()
  {
    this.btnOk.Enabled = this.edNew.Text == this.edConfirm.Text && this.edNew.Text != this._oldPassword && this.edOld.Text == this._oldPassword;
    if (this._enableEmpty || !(this.edNew.Text == string.Empty))
      return;
    this.btnOk.Enabled = false;
  }

  /// <summary>Обновить контролы</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoUpdateControls(object sender, EventArgs e) => this.UpdateControls();

  /// <summary>Статический метод для вызова формы по смене пароля</summary>
  /// <param name="oldPassword">Старый пароль</param>
  /// <param name="enableEmpty">Разрешить вводить пустой пароль</param>
  /// <param name="newPassword">Новый пароль</param>
  /// <returns>Результат вызова формы</returns>
  public static DialogResult Execute(string oldPassword, bool enableEmpty, out string newPassword)
  {
    newPassword = string.Empty;
    using (ChangeCurrentUserPasswordForm userPasswordForm = new ChangeCurrentUserPasswordForm(oldPassword, enableEmpty))
    {
      if (userPasswordForm.ShowDialog() != DialogResult.OK)
        return DialogResult.Cancel;
      newPassword = userPasswordForm.edNew.Text;
      return DialogResult.OK;
    }
  }

  private void NewPasswordForm_Shown(object sender, EventArgs e)
  {
    this.Activate();
    this.ActiveControl = (Control) this.edOld;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ChangeCurrentUserPasswordForm));
    this.pictureBox1 = new PictureBox();
    this.edNew = new TextBox();
    this.edOld = new TextBox();
    this.labelOldPassword = new Label();
    this.labelNewPassword = new Label();
    this.labelConfirmation = new Label();
    this.edConfirm = new TextBox();
    this.btnOk = new Button();
    this.btnCancel = new Button();
    this.panel1 = new Panel();
    ((ISupportInitialize) this.pictureBox1).BeginInit();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.pictureBox1, "pictureBox1");
    this.pictureBox1.Name = "pictureBox1";
    this.pictureBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.edNew, "edNew");
    this.edNew.Name = "edNew";
    this.edNew.TextChanged += new EventHandler(this.DoUpdateControls);
    componentResourceManager.ApplyResources((object) this.edOld, "edOld");
    this.edOld.Name = "edOld";
    this.edOld.TextChanged += new EventHandler(this.DoUpdateControls);
    componentResourceManager.ApplyResources((object) this.labelOldPassword, "labelOldPassword");
    this.labelOldPassword.Name = "labelOldPassword";
    componentResourceManager.ApplyResources((object) this.labelNewPassword, "labelNewPassword");
    this.labelNewPassword.Name = "labelNewPassword";
    componentResourceManager.ApplyResources((object) this.labelConfirmation, "labelConfirmation");
    this.labelConfirmation.Name = "labelConfirmation";
    componentResourceManager.ApplyResources((object) this.edConfirm, "edConfirm");
    this.edConfirm.Name = "edConfirm";
    this.edConfirm.TextChanged += new EventHandler(this.DoUpdateControls);
    componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
    this.btnOk.DialogResult = DialogResult.OK;
    this.btnOk.Name = "btnOk";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.panel1.Controls.Add((Control) this.labelOldPassword);
    this.panel1.Controls.Add((Control) this.edOld);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    this.AcceptButton = (IButtonControl) this.btnOk;
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.CancelButton = (IButtonControl) this.btnCancel;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOk);
    this.Controls.Add((Control) this.labelConfirmation);
    this.Controls.Add((Control) this.edConfirm);
    this.Controls.Add((Control) this.labelNewPassword);
    this.Controls.Add((Control) this.pictureBox1);
    this.Controls.Add((Control) this.edNew);
    this.DoubleBuffered = true;
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ChangeCurrentUserPasswordForm);
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.Tag = (object) "w";
    this.Shown += new EventHandler(this.NewPasswordForm_Shown);
    ((ISupportInitialize) this.pictureBox1).EndInit();
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
