
// Type: Intermech.Client.Core.CompareSettingsEditForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Navigator.Controls.Windows;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>Форма редактирования настроек сравнения файлов</summary>
public class CompareSettingsEditForm : Form
{
  private FilesComparisonSettings _settings;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btnOk;
  private Button btDir;
  private TextBox tbArgs;
  private Label labelArgs;
  private Label label1;
  private Button btnCancel;
  private Label labelPath;
  private AutoCompleteTextBox tbExeFile;
  private Label labelMasks;
  private TextBox tbName;
  private TextBox tbExtensions;
  private ToolTip toolTip1;

  public FilesComparisonSettings Settings => this._settings;

  public CompareSettingsEditForm()
  {
    this.InitializeComponent();
    this.InitializeToolTip();
  }

  private void InitializeToolTip()
  {
    this.toolTip1.SetToolTip((Control) this.tbExtensions, "Используйте ; как разделитель для расширений.");
    this.toolTip1.SetToolTip((Control) this.tbArgs, $"Используйте {"%file1"} и {"%file2"} в качестве подстановки для путей сравниваемых файлов.");
  }

  /// <summary>Инициализация настройками</summary>
  /// <param name="settings"></param>
  public void Init(FilesComparisonSettings settings)
  {
    this.tbName.Text = settings.Name;
    this.tbExeFile.Text = settings.ProgramExePath;
    this.tbArgs.Text = settings.Arguments;
    this.tbExtensions.Text = settings.ExtensionsAsString;
  }

  private bool SettingsIsValid()
  {
    return this.CheckName() && this.CheckExecutiveFile() && this.CheckArgs() && this.CheckExtensions();
  }

  private bool CheckExtensions()
  {
    if (!string.IsNullOrWhiteSpace(this.tbExtensions.Text))
      return true;
    int num = (int) MessageBox.Show("Необходимо указать расширения сравниваемых файлов.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    return false;
  }

  private bool CheckArgs()
  {
    int num1 = this.tbArgs.Text.IndexOf("%file1", StringComparison.Ordinal);
    int num2 = this.tbArgs.Text.IndexOf("%file2", StringComparison.Ordinal);
    if (num1 >= 0 && num2 >= 0)
      return true;
    int num3 = (int) MessageBox.Show("В аргументах должны фигурировать две подстановки для сравниваемых файлов: %file1 и %file2.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    return false;
  }

  private bool CheckExecutiveFile()
  {
    if (!string.IsNullOrWhiteSpace(this.tbExeFile.Text))
      return true;
    int num = (int) MessageBox.Show("Необходимо указать программу сравнения файлов.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    return false;
  }

  private bool CheckName()
  {
    if (!string.IsNullOrWhiteSpace(this.tbName.Text))
      return true;
    int num = (int) MessageBox.Show("Наименование не может быть пустым.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    return false;
  }

  private void btnOk_Click(object sender, EventArgs e)
  {
    if (!this.SettingsIsValid())
      return;
    this._settings = new FilesComparisonSettings(this.tbName.Text, this.tbExeFile.Text, this.tbArgs.Text, this.tbExtensions.Text);
    this.DialogResult = DialogResult.OK;
    this.Close();
  }

  private void btnCancel_Click(object sender, EventArgs e) => this.Close();

  private void btDir_Click(object sender, EventArgs e)
  {
    using (OpenFileDialog openFileDialog = new OpenFileDialog())
    {
      openFileDialog.RestoreDirectory = true;
      openFileDialog.Filter = " файлы exe|*.exe";
      if (openFileDialog.ShowDialog() != DialogResult.OK)
        return;
      this.tbExeFile.Text = openFileDialog.FileName;
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
    this.btnOk = new Button();
    this.btDir = new Button();
    this.tbArgs = new TextBox();
    this.labelArgs = new Label();
    this.label1 = new Label();
    this.btnCancel = new Button();
    this.labelPath = new Label();
    this.labelMasks = new Label();
    this.tbName = new TextBox();
    this.tbExtensions = new TextBox();
    this.toolTip1 = new ToolTip(this.components);
    this.tbExeFile = new AutoCompleteTextBox();
    this.SuspendLayout();
    this.btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOk.Location = new Point(171, 186);
    this.btnOk.Name = "btnOk";
    this.btnOk.Size = new Size(121, 27);
    this.btnOk.TabIndex = 18;
    this.btnOk.Text = "OK";
    this.btnOk.UseVisualStyleBackColor = true;
    this.btnOk.Click += new EventHandler(this.btnOk_Click);
    this.btDir.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btDir.Location = new Point(387, 68);
    this.btDir.Name = "btDir";
    this.btDir.Size = new Size(33, 22);
    this.btDir.TabIndex = 23;
    this.btDir.Text = "...";
    this.btDir.UseVisualStyleBackColor = true;
    this.btDir.Click += new EventHandler(this.btDir_Click);
    this.tbArgs.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbArgs.Location = new Point(6, 113);
    this.tbArgs.Name = "tbArgs";
    this.tbArgs.Size = new Size(414, 20);
    this.tbArgs.TabIndex = 22;
    this.labelArgs.AutoSize = true;
    this.labelArgs.Location = new Point(6, 96 /*0x60*/);
    this.labelArgs.Name = "labelArgs";
    this.labelArgs.Size = new Size(160 /*0xA0*/, 13);
    this.labelArgs.TabIndex = 21;
    this.labelArgs.Text = "Аргументы командной строки";
    this.label1.AutoSize = true;
    this.label1.Location = new Point(3, 9);
    this.label1.Name = "label1";
    this.label1.Size = new Size(83, 13);
    this.label1.TabIndex = 13;
    this.label1.Text = "Наименование";
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(299, 186);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(121, 27);
    this.btnCancel.TabIndex = 19;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    this.labelPath.AutoSize = true;
    this.labelPath.Location = new Point(3, 53);
    this.labelPath.Name = "labelPath";
    this.labelPath.Size = new Size(164, 13);
    this.labelPath.TabIndex = 15;
    this.labelPath.Text = "Программа сравнения файлов";
    this.labelMasks.AutoSize = true;
    this.labelMasks.Location = new Point(3, 143);
    this.labelMasks.Name = "labelMasks";
    this.labelMasks.Size = new Size(146, 13);
    this.labelMasks.TabIndex = 17;
    this.labelMasks.Text = "Маски расширений файлов";
    this.tbName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbName.Location = new Point(6, 27);
    this.tbName.Name = "tbName";
    this.tbName.Size = new Size(414, 20);
    this.tbName.TabIndex = 12;
    this.tbExtensions.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbExtensions.Location = new Point(6, 159);
    this.tbExtensions.Name = "tbExtensions";
    this.tbExtensions.Size = new Size(414, 20);
    this.tbExtensions.TabIndex = 16 /*0x10*/;
    this.tbExeFile.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbExeFile.Location = new Point(6, 69);
    this.tbExeFile.Name = "tbExeFile";
    this.tbExeFile.Separator = ';';
    this.tbExeFile.Size = new Size(381, 20);
    this.tbExeFile.TabIndex = 14;
    this.tbExeFile.Values = (string[]) null;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.AutoScroll = true;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(426, 221);
    this.Controls.Add((Control) this.btnOk);
    this.Controls.Add((Control) this.btDir);
    this.Controls.Add((Control) this.tbArgs);
    this.Controls.Add((Control) this.labelArgs);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.labelPath);
    this.Controls.Add((Control) this.tbExeFile);
    this.Controls.Add((Control) this.labelMasks);
    this.Controls.Add((Control) this.tbName);
    this.Controls.Add((Control) this.tbExtensions);
    this.MaximizeBox = false;
    this.MinimumSize = new Size(442, 260);
    this.Name = nameof (CompareSettingsEditForm);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Настройка сравнения файлов";
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
