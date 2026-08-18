// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.PropertyPages.CheckAccauntForm
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.DatabaseConfigurator.PropertyPages;

public class CheckAccauntForm : Form
{
  private EmailAccaunt _accaunt;
  private IContainer components;
  private GroupBox groupBox1;
  private TextBox tbSubject;
  private TextBox tbEmail;
  private Label label3;
  private Label label2;
  private Label label1;
  private CheckBox cbCheckOutputMail;
  private CheckBox cbCheckInputMail;
  private Button bCheck;
  private Button bClose;
  private RichTextBox richTextBox1;
  private TextBox lbOutput;

  public CheckAccauntForm(EmailAccaunt accaunt)
  {
    this.InitializeComponent();
    this._accaunt = accaunt;
    this.tbEmail.Text = accaunt.Email;
  }

  private void bCheck_Click(object sender, EventArgs e)
  {
    this.lbOutput.Text = string.Empty;
    this.Refresh();
    try
    {
      this.bCheck.Enabled = false;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IEmailService customService = (IEmailService) sessionKeeper.Session.GetCustomService(typeof (IEmailService));
        if (customService == null)
        {
          this.AddOutput("Отсутствует серверный сервис IEmailService...");
        }
        else
        {
          if (this.cbCheckOutputMail.Checked)
          {
            if (!EmailHelper.IsEmail(this.tbEmail.Text))
            {
              this.AddOutput("Введен неверный e-mail получателя...");
              return;
            }
            try
            {
              this.AddOutput("Отправка тестового сообщения...");
              customService.SendMessage(sessionKeeper.Session.SessionGUID, this._accaunt.Guid, this.tbEmail.Text, this.tbSubject.Text, this.richTextBox1.Text);
              this.AddOutput("Текстовое сообщение успешно отправлено ...");
            }
            catch (Exception ex)
            {
              this.AddOutput($"Ошибка при отправке тестового письма: {ex.Message}");
            }
          }
          if (!this.cbCheckInputMail.Checked)
            return;
          this.AddOutput("Проверка соединения с почтовым сервером входящих сообщений...");
          try
          {
            customService.GetInboxMessages(sessionKeeper.Session.SessionGUID, this._accaunt.Guid, new List<string>(0));
            this.AddOutput("Успешно!");
          }
          catch (Exception ex)
          {
            this.AddOutput($"Ошибка: {ex.Message}");
          }
        }
      }
    }
    finally
    {
      this.SetEnableButton();
    }
  }

  private void AddOutput(string text)
  {
    if (!string.IsNullOrEmpty(this.lbOutput.Text))
      this.lbOutput.AppendText(Environment.NewLine);
    this.lbOutput.AppendText(text);
  }

  private void checkBox1_CheckedChanged(object sender, EventArgs e)
  {
    this.groupBox1.Enabled = this.cbCheckOutputMail.Checked;
    this.SetEnableButton();
  }

  private void checkBox2_CheckedChanged(object sender, EventArgs e) => this.SetEnableButton();

  private void SetEnableButton()
  {
    this.bCheck.Enabled = this.cbCheckOutputMail.Checked || this.cbCheckInputMail.Checked;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CheckAccauntForm));
    this.groupBox1 = new GroupBox();
    this.richTextBox1 = new RichTextBox();
    this.label3 = new Label();
    this.label2 = new Label();
    this.label1 = new Label();
    this.tbSubject = new TextBox();
    this.tbEmail = new TextBox();
    this.cbCheckOutputMail = new CheckBox();
    this.cbCheckInputMail = new CheckBox();
    this.bCheck = new Button();
    this.bClose = new Button();
    this.lbOutput = new TextBox();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    this.groupBox1.Controls.Add((Control) this.richTextBox1);
    this.groupBox1.Controls.Add((Control) this.label3);
    this.groupBox1.Controls.Add((Control) this.label2);
    this.groupBox1.Controls.Add((Control) this.label1);
    this.groupBox1.Controls.Add((Control) this.tbSubject);
    this.groupBox1.Controls.Add((Control) this.tbEmail);
    this.groupBox1.Location = new Point(12, 42);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(682, 142);
    this.groupBox1.TabIndex = 1;
    this.groupBox1.TabStop = false;
    this.groupBox1.Text = "Тестовое письмо ";
    this.richTextBox1.Location = new Point(82, 71);
    this.richTextBox1.Name = "richTextBox1";
    this.richTextBox1.Size = new Size(588, 56);
    this.richTextBox1.TabIndex = 6;
    this.richTextBox1.Text = "Тестовое сообщения для проверки настроек исходящей почты";
    this.label3.AutoSize = true;
    this.label3.Location = new Point(8, 71);
    this.label3.Name = "label3";
    this.label3.Size = new Size(68, 13);
    this.label3.TabIndex = 5;
    this.label3.Text = "Сообщение:";
    this.label2.AutoSize = true;
    this.label2.Location = new Point(39, 45);
    this.label2.Name = "label2";
    this.label2.Size = new Size(37, 13);
    this.label2.TabIndex = 4;
    this.label2.Text = "Тема:";
    this.label1.AutoSize = true;
    this.label1.Location = new Point(39, 26);
    this.label1.Name = "label1";
    this.label1.Size = new Size(36, 13);
    this.label1.TabIndex = 3;
    this.label1.Text = "Кому:";
    this.tbSubject.Location = new Point(82, 45);
    this.tbSubject.Name = "tbSubject";
    this.tbSubject.Size = new Size(588, 20);
    this.tbSubject.TabIndex = 2;
    this.tbSubject.Text = "Проверка настроек исходящей почты";
    this.tbEmail.Location = new Point(82, 19);
    this.tbEmail.Name = "tbEmail";
    this.tbEmail.Size = new Size(588, 20);
    this.tbEmail.TabIndex = 1;
    this.cbCheckOutputMail.AutoSize = true;
    this.cbCheckOutputMail.Checked = true;
    this.cbCheckOutputMail.CheckState = CheckState.Checked;
    this.cbCheckOutputMail.Location = new Point(23, 19);
    this.cbCheckOutputMail.Name = "cbCheckOutputMail";
    this.cbCheckOutputMail.Size = new Size(218, 17);
    this.cbCheckOutputMail.TabIndex = 0;
    this.cbCheckOutputMail.Text = "Проверка настроек исходящей почты";
    this.cbCheckOutputMail.UseVisualStyleBackColor = true;
    this.cbCheckOutputMail.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
    this.cbCheckInputMail.AutoSize = true;
    this.cbCheckInputMail.Checked = true;
    this.cbCheckInputMail.CheckState = CheckState.Checked;
    this.cbCheckInputMail.Location = new Point(21, 190);
    this.cbCheckInputMail.Name = "cbCheckInputMail";
    this.cbCheckInputMail.Size = new Size(212, 17);
    this.cbCheckInputMail.TabIndex = 4;
    this.cbCheckInputMail.Text = "Проверка настроек входящей почты";
    this.cbCheckInputMail.UseVisualStyleBackColor = true;
    this.cbCheckInputMail.CheckedChanged += new EventHandler(this.checkBox2_CheckedChanged);
    this.bCheck.Location = new Point(444, 207);
    this.bCheck.Name = "bCheck";
    this.bCheck.Size = new Size(121, 27);
    this.bCheck.TabIndex = 5;
    this.bCheck.Text = "Проверить";
    this.bCheck.UseVisualStyleBackColor = true;
    this.bCheck.Click += new EventHandler(this.bCheck_Click);
    this.bClose.DialogResult = DialogResult.Cancel;
    this.bClose.Location = new Point(571, 207);
    this.bClose.Name = "bClose";
    this.bClose.Size = new Size(121, 27);
    this.bClose.TabIndex = 6;
    this.bClose.Text = "Закрыть";
    this.bClose.UseVisualStyleBackColor = true;
    this.lbOutput.BackColor = SystemColors.Window;
    this.lbOutput.Location = new Point(12, 244);
    this.lbOutput.Multiline = true;
    this.lbOutput.Name = "lbOutput";
    this.lbOutput.ReadOnly = true;
    this.lbOutput.ScrollBars = ScrollBars.Both;
    this.lbOutput.Size = new Size(680, 228);
    this.lbOutput.TabIndex = 7;
    this.AcceptButton = (IButtonControl) this.bCheck;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bClose;
    this.ClientSize = new Size(705, 484);
    this.Controls.Add((Control) this.lbOutput);
    this.Controls.Add((Control) this.bClose);
    this.Controls.Add((Control) this.bCheck);
    this.Controls.Add((Control) this.cbCheckInputMail);
    this.Controls.Add((Control) this.cbCheckOutputMail);
    this.Controls.Add((Control) this.groupBox1);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (CheckAccauntForm);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Проверка настроек аккаунта";
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
