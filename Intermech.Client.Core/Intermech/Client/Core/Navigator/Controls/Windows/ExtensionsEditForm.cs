
// Type: Intermech.Client.Core.Navigator.Controls.Windows.ExtensionsEditForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Client.Core.Navigator.Controls.Windows;

/// <summary>Изменение просмотра файлов</summary>
public class ExtensionsEditForm : Form
{
  /// <summary>использовать для просмотра</summary>
  public bool Used;
  /// <summary>наименование</summary>
  public string NameViewer = string.Empty;
  /// <summary>Программный идентификатор ProgID</summary>
  public string ProgID = string.Empty;
  public string CommandLine = string.Empty;
  /// <summary>Маски для расширений файлов</summary>
  public string Extensions = string.Empty;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btnOk;
  private Label label1;
  private Label label2;
  private Label label3;
  private TextBox tbExtensions;
  private TextBox tbName;
  private AutoCompleteTextBox tbProgID;
  private Button btnCancel;
  private CheckBox checkBoxAllUser;
  private Label labelCommandLine;
  private TextBox tbCommandLine;
  private Button btDir;

  /// <summary>Возвращает true, если это администратор</summary>
  public bool IsAdmin { get; }

  /// <summary>использовать для всех пользователей</summary>
  public bool IsAllUser { get; set; }

  private string[] GetProgIds()
  {
    return ((IEnumerable<string>) Registry.ClassesRoot.GetSubKeyNames()).Where<string>((Func<string, bool>) (item => !item.StartsWith(".") && !item.StartsWith("{"))).ToArray<string>();
  }

  public ExtensionsEditForm()
  {
    this.InitializeComponent();
    this.tbProgID.Values = this.GetProgIds();
    this.IsAdmin = ApplicationServices.Container.GetService<ICurrentUserAndRole>().IsAdmin;
    this.checkBoxAllUser.Visible = this.IsAdmin;
    this.UpdateOk();
  }

  /// <summary>конструктор</summary>
  /// <param name="alluser">использовать для всех пользователей(работает для администратора)</param>
  /// <param name="nameViewer">наименование</param>
  /// <param name="progID">Программный идентификатор ProgID</param>
  /// <param name="extensions">Маски для расширений файлов</param>
  public ExtensionsEditForm(
    bool used,
    bool alluser,
    string nameViewer,
    string progID,
    string extensions,
    string commandLine)
    : this()
  {
    this.Used = used;
    this.checkBoxAllUser.Checked = (this.IsAllUser = alluser) && this.IsAdmin;
    this.tbName.Text = nameViewer;
    this.tbProgID.Text = progID;
    this.tbExtensions.Text = extensions;
    this.tbCommandLine.Text = commandLine;
    this.tbCommandLine.TextChanged += new EventHandler(this.tbCommandLine_TextChanged);
    this.UpdateOk();
  }

  private void tbCommandLine_TextChanged(object sender, EventArgs e) => this.UpdateOk();

  private void UpdateOk()
  {
    bool flag1 = !string.IsNullOrEmpty(this.tbExtensions.Text);
    bool flag2 = !string.IsNullOrEmpty(this.tbProgID.Text);
    string lower = this.tbCommandLine.Text.ToLower();
    bool flag3 = !string.IsNullOrEmpty(lower) && lower.IndexOf(".exe\" ") != -1 && lower.IndexOf("\"%1\"") != -1;
    this.label3.ForeColor = flag1 ? SystemColors.ControlText : System.Drawing.Color.Red;
    this.label2.ForeColor = flag3 | flag2 ? SystemColors.ControlText : System.Drawing.Color.Red;
    this.labelCommandLine.ForeColor = flag3 | flag2 ? SystemColors.ControlText : System.Drawing.Color.Red;
    this.btnOk.Enabled = (flag3 | flag2) & flag1;
  }

  /// <summary></summary>
  /// <param name="sender">Отправитель события</param>
  /// <param name="e">Аргументы события</param>
  private void tbExtensions_TextChanged(object sender, EventArgs e) => this.UpdateOk();

  /// <summary></summary>
  /// <param name="sender">Отправитель события</param>
  /// <param name="e">Аргументы события</param>
  private void tbName_TextChanged(object sender, EventArgs e) => this.UpdateOk();

  /// <summary></summary>
  /// <param name="sender">Отправитель события</param>
  /// <param name="e">Аргументы события</param>
  private void btnOk_Click(object sender, EventArgs e)
  {
    this.IsAllUser = this.checkBoxAllUser.Checked && this.IsAdmin;
    this.NameViewer = this.tbName.Text;
    this.ProgID = this.tbProgID.Text;
    this.Extensions = this.tbExtensions.Text;
    this.CommandLine = this.tbCommandLine.Text.Replace("\"%1\"", "\"%x\"").Replace("%1", "\"%x\"").Replace("\"%x\"", "\"%1\"");
  }

  /// <summary></summary>
  /// <param name="sender">Отправитель события</param>
  /// <param name="e">Аргументы события</param>
  private void tbProgID_TextChanged(object sender, EventArgs e) => this.UpdateOk();

  private void btDir_Click(object sender, EventArgs e)
  {
    using (System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog())
    {
      openFileDialog.RestoreDirectory = true;
      openFileDialog.Filter = "Файлы exe|*.exe";
      if (openFileDialog.ShowDialog() == DialogResult.OK)
        this.tbCommandLine.Text = $"\"{openFileDialog.FileName}\" \"%1\"";
    }
    this.UpdateOk();
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
    this.btnOk = new Button();
    this.label1 = new Label();
    this.label2 = new Label();
    this.label3 = new Label();
    this.tbExtensions = new TextBox();
    this.tbName = new TextBox();
    this.tbProgID = new AutoCompleteTextBox();
    this.btnCancel = new Button();
    this.checkBoxAllUser = new CheckBox();
    this.labelCommandLine = new Label();
    this.tbCommandLine = new TextBox();
    this.btDir = new Button();
    this.SuspendLayout();
    this.btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOk.DialogResult = DialogResult.OK;
    this.btnOk.Enabled = false;
    this.btnOk.Location = new Point(217, 190);
    this.btnOk.Name = "btnOk";
    this.btnOk.Size = new Size(121, 27);
    this.btnOk.TabIndex = 6;
    this.btnOk.Text = "OK";
    this.btnOk.UseVisualStyleBackColor = true;
    this.btnOk.Click += new EventHandler(this.btnOk_Click);
    this.label1.AutoSize = true;
    this.label1.Location = new Point(12, 9);
    this.label1.Name = "label1";
    this.label1.Size = new Size(83, 13);
    this.label1.TabIndex = 1;
    this.label1.Text = "Наименование";
    this.label2.AutoSize = true;
    this.label2.Location = new Point(12, 48 /*0x30*/);
    this.label2.Name = "label2";
    this.label2.Size = new Size(161, 13);
    this.label2.TabIndex = 2;
    this.label2.Text = "Программный идентификатор";
    this.label3.AutoSize = true;
    this.label3.Location = new Point(12, 138);
    this.label3.Name = "label3";
    this.label3.Size = new Size(146, 13);
    this.label3.TabIndex = 3;
    this.label3.Text = "Маски расширений файлов";
    this.tbExtensions.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbExtensions.Location = new Point(15, 154);
    this.tbExtensions.Name = "tbExtensions";
    this.tbExtensions.Size = new Size(450, 20);
    this.tbExtensions.TabIndex = 2;
    this.tbExtensions.TextChanged += new EventHandler(this.tbExtensions_TextChanged);
    this.tbName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbName.Location = new Point(15, 27);
    this.tbName.Name = "tbName";
    this.tbName.Size = new Size(450, 20);
    this.tbName.TabIndex = 0;
    this.tbName.TextChanged += new EventHandler(this.tbName_TextChanged);
    this.tbProgID.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbProgID.Location = new Point(15, 64 /*0x40*/);
    this.tbProgID.Name = "tbProgID";
    this.tbProgID.Separator = ';';
    this.tbProgID.Size = new Size(450, 20);
    this.tbProgID.TabIndex = 1;
    this.tbProgID.Values = (string[]) null;
    this.tbProgID.TextChanged += new EventHandler(this.tbProgID_TextChanged);
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(344, 190);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(121, 27);
    this.btnCancel.TabIndex = 7;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.checkBoxAllUser.AutoSize = true;
    this.checkBoxAllUser.Location = new Point(15, 196);
    this.checkBoxAllUser.Name = "checkBoxAllUser";
    this.checkBoxAllUser.Size = new Size(153, 17);
    this.checkBoxAllUser.TabIndex = 8;
    this.checkBoxAllUser.Text = "Для всех пользователей";
    this.checkBoxAllUser.UseVisualStyleBackColor = true;
    this.labelCommandLine.AutoSize = true;
    this.labelCommandLine.Location = new Point(15, 91);
    this.labelCommandLine.Name = "labelCommandLine";
    this.labelCommandLine.Size = new Size(102, 13);
    this.labelCommandLine.TabIndex = 9;
    this.labelCommandLine.Text = "Командная строка";
    this.tbCommandLine.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbCommandLine.Location = new Point(15, 108);
    this.tbCommandLine.Name = "tbCommandLine";
    this.tbCommandLine.Size = new Size(417, 20);
    this.tbCommandLine.TabIndex = 10;
    this.btDir.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btDir.Location = new Point(432, 107);
    this.btDir.Name = "btDir";
    this.btDir.Size = new Size(33, 22);
    this.btDir.TabIndex = 11;
    this.btDir.Text = "...";
    this.btDir.UseVisualStyleBackColor = true;
    this.btDir.Click += new EventHandler(this.btDir_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(477, 229);
    this.Controls.Add((Control) this.btnOk);
    this.Controls.Add((Control) this.btDir);
    this.Controls.Add((Control) this.tbCommandLine);
    this.Controls.Add((Control) this.labelCommandLine);
    this.Controls.Add((Control) this.checkBoxAllUser);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.tbProgID);
    this.Controls.Add((Control) this.label3);
    this.Controls.Add((Control) this.tbName);
    this.Controls.Add((Control) this.tbExtensions);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ExtensionsEditForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.Text = "Настройка просмотра файлов";
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
