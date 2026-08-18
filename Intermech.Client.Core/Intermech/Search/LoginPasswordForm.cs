
// Type: Intermech.Search.LoginPasswordForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search;

public class LoginPasswordForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private PictureBox pictureBox1;
  private TableLayoutPanel tableLayoutPanel1;
  private FlowLayoutPanel flowLayoutPanel1;
  private Button _cancelButton;
  private Button _acceptButton;
  private TableLayoutPanel tableLayoutPanel2;
  private Label label1;
  private Label label2;
  private TextBox _loginTextBox;
  private TextBox _passwordTextBox;

  public LoginPasswordForm() => this.InitializeComponent();

  public string Login
  {
    get => this._loginTextBox.Text;
    set => this._loginTextBox.Text = value;
  }

  public string Password
  {
    get => this._passwordTextBox.Text;
    set => this._passwordTextBox.Text = value;
  }

  private void LoginPasswordForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void LoginPasswordForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (LoginPasswordForm));
    this.pictureBox1 = new PictureBox();
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this.flowLayoutPanel1 = new FlowLayoutPanel();
    this._cancelButton = new Button();
    this._acceptButton = new Button();
    this.tableLayoutPanel2 = new TableLayoutPanel();
    this.label1 = new Label();
    this.label2 = new Label();
    this._loginTextBox = new TextBox();
    this._passwordTextBox = new TextBox();
    ((ISupportInitialize) this.pictureBox1).BeginInit();
    this.tableLayoutPanel1.SuspendLayout();
    this.flowLayoutPanel1.SuspendLayout();
    this.tableLayoutPanel2.SuspendLayout();
    this.SuspendLayout();
    this.pictureBox1.Dock = DockStyle.Top;
    this.pictureBox1.Image = (Image) componentResourceManager.GetObject("pictureBox1.Image");
    this.pictureBox1.ImeMode = ImeMode.NoControl;
    this.pictureBox1.Location = new Point(0, 0);
    this.pictureBox1.Name = "pictureBox1";
    this.pictureBox1.Size = new Size(313, 60);
    this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
    this.pictureBox1.TabIndex = 1;
    this.pictureBox1.TabStop = false;
    this.tableLayoutPanel1.ColumnCount = 1;
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.Controls.Add((Control) this.flowLayoutPanel1, 0, 1);
    this.tableLayoutPanel1.Controls.Add((Control) this.tableLayoutPanel2, 0, 0);
    this.tableLayoutPanel1.Dock = DockStyle.Fill;
    this.tableLayoutPanel1.Location = new Point(0, 60);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.tableLayoutPanel1.RowCount = 2;
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40f));
    this.tableLayoutPanel1.Size = new Size(313, (int) sbyte.MaxValue);
    this.tableLayoutPanel1.TabIndex = 2;
    this.flowLayoutPanel1.Controls.Add((Control) this._cancelButton);
    this.flowLayoutPanel1.Controls.Add((Control) this._acceptButton);
    this.flowLayoutPanel1.Dock = DockStyle.Fill;
    this.flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
    this.flowLayoutPanel1.Location = new Point(3, 90);
    this.flowLayoutPanel1.Name = "flowLayoutPanel1";
    this.flowLayoutPanel1.Size = new Size(307, 34);
    this.flowLayoutPanel1.TabIndex = 0;
    this._cancelButton.DialogResult = DialogResult.Cancel;
    this._cancelButton.Location = new Point(229, 3);
    this._cancelButton.Name = "_cancelButton";
    this._cancelButton.Size = new Size(75, 23);
    this._cancelButton.TabIndex = 0;
    this._cancelButton.Text = "Отмена";
    this._cancelButton.UseVisualStyleBackColor = true;
    this._acceptButton.DialogResult = DialogResult.OK;
    this._acceptButton.Location = new Point(148, 3);
    this._acceptButton.Name = "_acceptButton";
    this._acceptButton.Size = new Size(75, 23);
    this._acceptButton.TabIndex = 0;
    this._acceptButton.Text = "OK";
    this._acceptButton.UseVisualStyleBackColor = true;
    this.tableLayoutPanel2.ColumnCount = 2;
    this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30f));
    this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70f));
    this.tableLayoutPanel2.Controls.Add((Control) this.label1, 0, 0);
    this.tableLayoutPanel2.Controls.Add((Control) this.label2, 0, 1);
    this.tableLayoutPanel2.Controls.Add((Control) this._loginTextBox, 1, 0);
    this.tableLayoutPanel2.Controls.Add((Control) this._passwordTextBox, 1, 1);
    this.tableLayoutPanel2.Dock = DockStyle.Fill;
    this.tableLayoutPanel2.Location = new Point(10, 10);
    this.tableLayoutPanel2.Margin = new Padding(10);
    this.tableLayoutPanel2.Name = "tableLayoutPanel2";
    this.tableLayoutPanel2.RowCount = 2;
    this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
    this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
    this.tableLayoutPanel2.Size = new Size(293, 67);
    this.tableLayoutPanel2.TabIndex = 1;
    this.label1.AutoSize = true;
    this.label1.Dock = DockStyle.Fill;
    this.label1.Location = new Point(3, 0);
    this.label1.Name = "label1";
    this.label1.Size = new Size(81, 33);
    this.label1.TabIndex = 0;
    this.label1.Text = "Пользователь:";
    this.label1.TextAlign = ContentAlignment.TopRight;
    this.label2.AutoSize = true;
    this.label2.Dock = DockStyle.Fill;
    this.label2.Location = new Point(3, 33);
    this.label2.Name = "label2";
    this.label2.Size = new Size(81, 34);
    this.label2.TabIndex = 1;
    this.label2.Text = "Пароль:";
    this.label2.TextAlign = ContentAlignment.TopRight;
    this._loginTextBox.Dock = DockStyle.Fill;
    this._loginTextBox.Location = new Point(90, 3);
    this._loginTextBox.Name = "_loginTextBox";
    this._loginTextBox.Size = new Size(200, 20);
    this._loginTextBox.TabIndex = 2;
    this._passwordTextBox.Dock = DockStyle.Fill;
    this._passwordTextBox.Location = new Point(90, 36);
    this._passwordTextBox.Name = "_passwordTextBox";
    this._passwordTextBox.Size = new Size(200, 20);
    this._passwordTextBox.TabIndex = 2;
    this._passwordTextBox.UseSystemPasswordChar = true;
    this.AcceptButton = (IButtonControl) this._acceptButton;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._cancelButton;
    this.ClientSize = new Size(313, 187);
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.Controls.Add((Control) this.pictureBox1);
    this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
    this.MaximizeBox = false;
    this.Name = nameof (LoginPasswordForm);
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.Text = "Введите логин и пароль";
    this.FormClosing += new FormClosingEventHandler(this.LoginPasswordForm_FormClosing);
    this.Load += new EventHandler(this.LoginPasswordForm_Load);
    ((ISupportInitialize) this.pictureBox1).EndInit();
    this.tableLayoutPanel1.ResumeLayout(false);
    this.flowLayoutPanel1.ResumeLayout(false);
    this.tableLayoutPanel2.ResumeLayout(false);
    this.tableLayoutPanel2.PerformLayout();
    this.ResumeLayout(false);
  }
}
