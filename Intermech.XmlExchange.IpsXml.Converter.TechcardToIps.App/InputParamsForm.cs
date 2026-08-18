// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App.InputParamsForm
// Assembly: Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EB4A0A0B-E62B-4D21-A944-3B5D877E45CE
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App.exe

using Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.ConfigEditor;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App;

internal class InputParamsForm : Form
{
  private IContainer components;
  private TableLayoutPanel tlpMain;
  private Button btnOk;
  private Button btnCancel;
  private Label lblUserName;
  private Label lblWorkDir;
  private Label lblConfigFile;
  private Label lblUserRole;
  private Label lblPassword;
  private TextBox txtbUserName;
  private TextBox txtbUserPassword;
  private TextBox txtbUserRole;
  private OpenFileDialog openFileDialog;
  private Button btnConfigFile;
  private ImageList Images;
  private TextBox txtbConfigFile;
  private Button btnWorkDir;
  private TextBox txtbWorkDir;
  private Button btnRolsynCompiler;
  private TextBox txtbRolsynCompiler;
  private Label lblRolsynCompiler;
  private Button btnInputFile;
  private TextBox txtbInputFile;
  private Label lblInputFile;
  private CheckBox cbxShowProgress;
  private FolderBrowserDialog openFolderDialog;
  private Button button1;

  private void FillFormData()
  {
    this.txtbUserName.Text = this.InputParams.UserName;
    this.txtbUserPassword.Text = this.InputParams.UserPassword;
    this.txtbUserRole.Text = this.InputParams.UserRole;
    this.txtbInputFile.Text = this.InputParams.InputFile;
    this.txtbConfigFile.Text = this.InputParams.ConfigFile;
    this.txtbWorkDir.Text = this.InputParams.WorkDir;
    this.txtbRolsynCompiler.Text = this.InputParams.RolsynScriptCompiler;
    this.cbxShowProgress.Checked = ((int) this.InputParams.ShowProgress ?? 0) != 0;
  }

  private void ReadFormData()
  {
    this.InputParams.UserName = this.txtbUserName.Text;
    this.InputParams.UserPassword = this.txtbUserPassword.Text;
    this.InputParams.UserRole = this.txtbUserRole.Text;
    this.InputParams.InputFile = this.txtbInputFile.Text;
    this.InputParams.ConfigFile = this.txtbConfigFile.Text;
    this.InputParams.WorkDir = this.txtbWorkDir.Text;
    this.InputParams.RolsynScriptCompiler = this.txtbRolsynCompiler.Text;
    this.InputParams.ShowProgress = new bool?(this.cbxShowProgress.Checked);
  }

  private void InputParamsForm_Shown(object sender, EventArgs e) => this.FillFormData();

  private void InputParamsForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (this.DialogResult != DialogResult.OK)
      return;
    this.ReadFormData();
    e.Cancel = !AppConfig.CheckInputParams(this.InputParams, true);
  }

  private void btnConfigFile_Click(object sender, EventArgs e)
  {
    this.openFileDialog.Filter = "Config files(*.config)|*.config";
    this.openFileDialog.FileName = this.txtbConfigFile.Text;
    if (this.openFileDialog.ShowDialog() != DialogResult.OK)
      return;
    this.txtbConfigFile.Text = this.openFileDialog.FileName;
  }

  private void btnRolsynCompiler_Click(object sender, EventArgs e)
  {
    this.openFileDialog.Filter = "exe files(*.exe)|*.exe";
    this.openFileDialog.FileName = this.txtbRolsynCompiler.Text;
    if (this.openFileDialog.ShowDialog() != DialogResult.OK)
      return;
    this.txtbRolsynCompiler.Text = this.openFileDialog.FileName;
  }

  private void btnInputFile_Click(object sender, EventArgs e)
  {
    this.openFileDialog.Filter = "Input xml files(*.xml)|*.xml";
    this.openFileDialog.FileName = this.txtbInputFile.Text;
    if (this.openFileDialog.ShowDialog() != DialogResult.OK)
      return;
    this.txtbInputFile.Text = this.openFileDialog.FileName;
  }

  private void btnWorkDir_Click(object sender, EventArgs e)
  {
    this.openFolderDialog.SelectedPath = this.txtbWorkDir.Text;
    if (this.openFolderDialog.ShowDialog() != DialogResult.OK)
      return;
    this.txtbWorkDir.Text = this.openFolderDialog.SelectedPath;
  }

  public InputParamsForm() => this.InitializeComponent();

  public InputParams InputParams { get; } = new InputParams();

  private void button1_Click(object sender, EventArgs e)
  {
    Form form = new Form();
    ConfigMainEditorView configMainEditorView = new ConfigMainEditorView();
    configMainEditorView.Dock = DockStyle.Fill;
    form.Controls.Add((Control) configMainEditorView);
    int num = (int) form.ShowDialog();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (InputParamsForm));
    this.tlpMain = new TableLayoutPanel();
    this.btnInputFile = new Button();
    this.Images = new ImageList(this.components);
    this.txtbInputFile = new TextBox();
    this.lblInputFile = new Label();
    this.btnRolsynCompiler = new Button();
    this.txtbRolsynCompiler = new TextBox();
    this.btnWorkDir = new Button();
    this.txtbWorkDir = new TextBox();
    this.btnConfigFile = new Button();
    this.lblWorkDir = new Label();
    this.lblConfigFile = new Label();
    this.lblUserRole = new Label();
    this.lblPassword = new Label();
    this.btnOk = new Button();
    this.btnCancel = new Button();
    this.lblUserName = new Label();
    this.txtbUserName = new TextBox();
    this.txtbUserPassword = new TextBox();
    this.txtbUserRole = new TextBox();
    this.txtbConfigFile = new TextBox();
    this.lblRolsynCompiler = new Label();
    this.cbxShowProgress = new CheckBox();
    this.openFileDialog = new OpenFileDialog();
    this.openFolderDialog = new FolderBrowserDialog();
    this.button1 = new Button();
    this.tlpMain.SuspendLayout();
    this.SuspendLayout();
    this.tlpMain.ColumnCount = 3;
    this.tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 49f));
    this.tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30f));
    this.tlpMain.Controls.Add((Control) this.button1, 2, 1);
    this.tlpMain.Controls.Add((Control) this.btnInputFile, 2, 15);
    this.tlpMain.Controls.Add((Control) this.txtbInputFile, 0, 16 /*0x10*/);
    this.tlpMain.Controls.Add((Control) this.lblInputFile, 0, 15);
    this.tlpMain.Controls.Add((Control) this.btnRolsynCompiler, 2, 12);
    this.tlpMain.Controls.Add((Control) this.txtbRolsynCompiler, 0, 13);
    this.tlpMain.Controls.Add((Control) this.btnWorkDir, 2, 9);
    this.tlpMain.Controls.Add((Control) this.txtbWorkDir, 0, 10);
    this.tlpMain.Controls.Add((Control) this.btnConfigFile, 2, 6);
    this.tlpMain.Controls.Add((Control) this.lblWorkDir, 0, 9);
    this.tlpMain.Controls.Add((Control) this.lblConfigFile, 0, 6);
    this.tlpMain.Controls.Add((Control) this.lblUserRole, 0, 4);
    this.tlpMain.Controls.Add((Control) this.lblPassword, 0, 2);
    this.tlpMain.Controls.Add((Control) this.btnOk, 0, 19);
    this.tlpMain.Controls.Add((Control) this.btnCancel, 1, 19);
    this.tlpMain.Controls.Add((Control) this.lblUserName, 0, 0);
    this.tlpMain.Controls.Add((Control) this.txtbUserName, 0, 1);
    this.tlpMain.Controls.Add((Control) this.txtbUserPassword, 0, 3);
    this.tlpMain.Controls.Add((Control) this.txtbUserRole, 0, 5);
    this.tlpMain.Controls.Add((Control) this.txtbConfigFile, 0, 7);
    this.tlpMain.Controls.Add((Control) this.lblRolsynCompiler, 0, 12);
    this.tlpMain.Controls.Add((Control) this.cbxShowProgress, 0, 17);
    this.tlpMain.Dock = DockStyle.Fill;
    this.tlpMain.Location = new Point(0, 0);
    this.tlpMain.Name = "tlpMain";
    this.tlpMain.RowCount = 20;
    this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 8f));
    this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 8f));
    this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 8f));
    this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 28f));
    this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 41f));
    this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpMain.Size = new Size(393, 377);
    this.tlpMain.TabIndex = 0;
    this.btnInputFile.Dock = DockStyle.Fill;
    this.btnInputFile.ForeColor = SystemColors.ButtonFace;
    this.btnInputFile.ImageIndex = 0;
    this.btnInputFile.ImageList = this.Images;
    this.btnInputFile.Location = new Point(363, 280);
    this.btnInputFile.Margin = new Padding(0, 16 /*0x10*/, 0, 0);
    this.btnInputFile.MaximumSize = new Size(28, 28);
    this.btnInputFile.Name = "btnInputFile";
    this.tlpMain.SetRowSpan((Control) this.btnInputFile, 3);
    this.btnInputFile.Size = new Size(28, 28);
    this.btnInputFile.TabIndex = 10;
    this.btnInputFile.UseVisualStyleBackColor = true;
    this.btnInputFile.Click += new EventHandler(this.btnInputFile_Click);
    this.Images.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("Images.ImageStream");
    this.Images.TransparentColor = Color.Fuchsia;
    this.Images.Images.SetKeyName(0, "open.bmp");
    this.Images.Images.SetKeyName(1, "open.bmp");
    this.tlpMain.SetColumnSpan((Control) this.txtbInputFile, 2);
    this.txtbInputFile.Dock = DockStyle.Fill;
    this.txtbInputFile.Location = new Point(3, 284);
    this.txtbInputFile.Margin = new Padding(3, 0, 3, 0);
    this.txtbInputFile.Name = "txtbInputFile";
    this.txtbInputFile.Size = new Size(357, 20);
    this.txtbInputFile.TabIndex = 9;
    this.lblInputFile.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.lblInputFile.AutoSize = true;
    this.lblInputFile.ImageAlign = ContentAlignment.TopLeft;
    this.lblInputFile.Location = new Point(3, 271);
    this.lblInputFile.Name = "lblInputFile";
    this.lblInputFile.Size = new Size(128 /*0x80*/, 13);
    this.lblInputFile.TabIndex = 34;
    this.lblInputFile.Text = "Файл для конвертации:";
    this.lblInputFile.TextAlign = ContentAlignment.MiddleLeft;
    this.btnRolsynCompiler.Dock = DockStyle.Fill;
    this.btnRolsynCompiler.ForeColor = SystemColors.ButtonFace;
    this.btnRolsynCompiler.ImageIndex = 0;
    this.btnRolsynCompiler.ImageList = this.Images;
    this.btnRolsynCompiler.Location = new Point(363, 232);
    this.btnRolsynCompiler.Margin = new Padding(0, 16 /*0x10*/, 0, 0);
    this.btnRolsynCompiler.MaximumSize = new Size(28, 28);
    this.btnRolsynCompiler.Name = "btnRolsynCompiler";
    this.tlpMain.SetRowSpan((Control) this.btnRolsynCompiler, 3);
    this.btnRolsynCompiler.Size = new Size(28, 28);
    this.btnRolsynCompiler.TabIndex = 8;
    this.btnRolsynCompiler.UseVisualStyleBackColor = true;
    this.btnRolsynCompiler.Click += new EventHandler(this.btnRolsynCompiler_Click);
    this.tlpMain.SetColumnSpan((Control) this.txtbRolsynCompiler, 2);
    this.txtbRolsynCompiler.Dock = DockStyle.Fill;
    this.txtbRolsynCompiler.Location = new Point(3, 236);
    this.txtbRolsynCompiler.Margin = new Padding(3, 0, 3, 0);
    this.txtbRolsynCompiler.Name = "txtbRolsynCompiler";
    this.txtbRolsynCompiler.Size = new Size(357, 20);
    this.txtbRolsynCompiler.TabIndex = 7;
    this.btnWorkDir.Dock = DockStyle.Fill;
    this.btnWorkDir.ForeColor = SystemColors.ButtonFace;
    this.btnWorkDir.ImageIndex = 0;
    this.btnWorkDir.ImageList = this.Images;
    this.btnWorkDir.Location = new Point(363, 184);
    this.btnWorkDir.Margin = new Padding(0, 16 /*0x10*/, 0, 0);
    this.btnWorkDir.MaximumSize = new Size(28, 28);
    this.btnWorkDir.Name = "btnWorkDir";
    this.tlpMain.SetRowSpan((Control) this.btnWorkDir, 3);
    this.btnWorkDir.Size = new Size(28, 28);
    this.btnWorkDir.TabIndex = 6;
    this.btnWorkDir.UseVisualStyleBackColor = true;
    this.btnWorkDir.Click += new EventHandler(this.btnWorkDir_Click);
    this.tlpMain.SetColumnSpan((Control) this.txtbWorkDir, 2);
    this.txtbWorkDir.Dock = DockStyle.Fill;
    this.txtbWorkDir.Location = new Point(3, 188);
    this.txtbWorkDir.Margin = new Padding(3, 0, 3, 0);
    this.txtbWorkDir.Name = "txtbWorkDir";
    this.txtbWorkDir.Size = new Size(357, 20);
    this.txtbWorkDir.TabIndex = 5;
    this.btnConfigFile.ForeColor = SystemColors.ButtonFace;
    this.btnConfigFile.ImageIndex = 0;
    this.btnConfigFile.ImageList = this.Images;
    this.btnConfigFile.Location = new Point(363, 136);
    this.btnConfigFile.Margin = new Padding(0, 16 /*0x10*/, 0, 0);
    this.btnConfigFile.MaximumSize = new Size(28, 28);
    this.btnConfigFile.Name = "btnConfigFile";
    this.tlpMain.SetRowSpan((Control) this.btnConfigFile, 3);
    this.btnConfigFile.Size = new Size(28, 28);
    this.btnConfigFile.TabIndex = 4;
    this.btnConfigFile.UseVisualStyleBackColor = true;
    this.btnConfigFile.Click += new EventHandler(this.btnConfigFile_Click);
    this.lblWorkDir.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.lblWorkDir.AutoSize = true;
    this.lblWorkDir.ImageAlign = ContentAlignment.TopLeft;
    this.lblWorkDir.Location = new Point(3, 175);
    this.lblWorkDir.Name = "lblWorkDir";
    this.lblWorkDir.Size = new Size(203, 13);
    this.lblWorkDir.TabIndex = 20;
    this.lblWorkDir.Text = "Рабочая директория для конвертации:";
    this.lblWorkDir.TextAlign = ContentAlignment.MiddleLeft;
    this.lblConfigFile.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.lblConfigFile.AutoSize = true;
    this.lblConfigFile.ImageAlign = ContentAlignment.TopLeft;
    this.lblConfigFile.Location = new Point(3, (int) sbyte.MaxValue);
    this.lblConfigFile.Name = "lblConfigFile";
    this.lblConfigFile.Size = new Size(157, 13);
    this.lblConfigFile.TabIndex = 16 /*0x10*/;
    this.lblConfigFile.Text = "Файл настроек конвертации:";
    this.lblConfigFile.TextAlign = ContentAlignment.MiddleLeft;
    this.lblUserRole.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.lblUserRole.AutoSize = true;
    this.lblUserRole.ImageAlign = ContentAlignment.TopLeft;
    this.lblUserRole.Location = new Point(3, 87);
    this.lblUserRole.Name = "lblUserRole";
    this.lblUserRole.Size = new Size(35, 13);
    this.lblUserRole.TabIndex = 12;
    this.lblUserRole.Text = "Роль:";
    this.lblUserRole.TextAlign = ContentAlignment.MiddleLeft;
    this.lblPassword.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.lblPassword.AutoSize = true;
    this.lblPassword.ImageAlign = ContentAlignment.TopLeft;
    this.lblPassword.Location = new Point(3, 47);
    this.lblPassword.Name = "lblPassword";
    this.lblPassword.Size = new Size(48 /*0x30*/, 13);
    this.lblPassword.TabIndex = 8;
    this.lblPassword.Text = "Пароль:";
    this.lblPassword.TextAlign = ContentAlignment.MiddleLeft;
    this.btnOk.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOk.DialogResult = DialogResult.OK;
    this.btnOk.Location = new Point(229, 349);
    this.btnOk.Margin = new Padding(0, 13, 10, 5);
    this.btnOk.Name = "btnOk";
    this.btnOk.Size = new Size(75, 23);
    this.btnOk.TabIndex = 12;
    this.btnOk.Text = "Применить";
    this.btnOk.UseVisualStyleBackColor = true;
    this.btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
    this.tlpMain.SetColumnSpan((Control) this.btnCancel, 2);
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(315, 349);
    this.btnCancel.Margin = new Padding(0, 13, 3, 5);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(75, 23);
    this.btnCancel.TabIndex = 13;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.lblUserName.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.lblUserName.AutoSize = true;
    this.lblUserName.ImageAlign = ContentAlignment.TopLeft;
    this.lblUserName.Location = new Point(3, 7);
    this.lblUserName.Name = "lblUserName";
    this.lblUserName.Size = new Size(83, 13);
    this.lblUserName.TabIndex = 3;
    this.lblUserName.Text = "Пользователь:";
    this.lblUserName.TextAlign = ContentAlignment.MiddleLeft;
    this.txtbUserName.Location = new Point(3, 20);
    this.txtbUserName.Margin = new Padding(3, 0, 3, 0);
    this.txtbUserName.Name = "txtbUserName";
    this.txtbUserName.Size = new Size(164, 20);
    this.txtbUserName.TabIndex = 0;
    this.txtbUserPassword.Location = new Point(3, 60);
    this.txtbUserPassword.Margin = new Padding(3, 0, 3, 0);
    this.txtbUserPassword.Name = "txtbUserPassword";
    this.txtbUserPassword.PasswordChar = '*';
    this.txtbUserPassword.Size = new Size(164, 20);
    this.txtbUserPassword.TabIndex = 1;
    this.txtbUserPassword.UseSystemPasswordChar = true;
    this.txtbUserRole.Location = new Point(3, 100);
    this.txtbUserRole.Margin = new Padding(3, 0, 3, 0);
    this.txtbUserRole.Name = "txtbUserRole";
    this.txtbUserRole.Size = new Size(164, 20);
    this.txtbUserRole.TabIndex = 2;
    this.tlpMain.SetColumnSpan((Control) this.txtbConfigFile, 2);
    this.txtbConfigFile.Dock = DockStyle.Fill;
    this.txtbConfigFile.Location = new Point(3, 140);
    this.txtbConfigFile.Margin = new Padding(3, 0, 3, 0);
    this.txtbConfigFile.Name = "txtbConfigFile";
    this.txtbConfigFile.Size = new Size(357, 20);
    this.txtbConfigFile.TabIndex = 3;
    this.lblRolsynCompiler.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.lblRolsynCompiler.AutoSize = true;
    this.lblRolsynCompiler.ImageAlign = ContentAlignment.TopLeft;
    this.lblRolsynCompiler.Location = new Point(3, 223);
    this.lblRolsynCompiler.Name = "lblRolsynCompiler";
    this.lblRolsynCompiler.Size = new Size(162, 13);
    this.lblRolsynCompiler.TabIndex = 31 /*0x1F*/;
    this.lblRolsynCompiler.Text = "Путь к компилятору скриптов:";
    this.lblRolsynCompiler.TextAlign = ContentAlignment.MiddleLeft;
    this.cbxShowProgress.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.cbxShowProgress.AutoSize = true;
    this.cbxShowProgress.Location = new Point(3, 309);
    this.cbxShowProgress.Margin = new Padding(3, 5, 3, 3);
    this.cbxShowProgress.Name = "cbxShowProgress";
    this.cbxShowProgress.Size = new Size(308, 20);
    this.cbxShowProgress.TabIndex = 11;
    this.cbxShowProgress.Text = "Отображать прогресс выполнения";
    this.cbxShowProgress.UseVisualStyleBackColor = true;
    this.openFileDialog.DefaultExt = "config";
    this.openFileDialog.Title = "Выбор файла настроек конвертации";
    this.button1.ForeColor = SystemColors.ButtonFace;
    this.button1.ImageIndex = 0;
    this.button1.ImageList = this.Images;
    this.button1.Location = new Point(363, 36);
    this.button1.Margin = new Padding(0, 16 /*0x10*/, 0, 0);
    this.button1.MaximumSize = new Size(28, 28);
    this.button1.Name = "button1";
    this.tlpMain.SetRowSpan((Control) this.button1, 3);
    this.button1.Size = new Size(28, 28);
    this.button1.TabIndex = 35;
    this.button1.UseVisualStyleBackColor = true;
    this.button1.Click += new EventHandler(this.button1_Click);
    this.AcceptButton = (IButtonControl) this.btnOk;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(393, 377);
    this.Controls.Add((Control) this.tlpMain);
    this.Name = nameof (InputParamsForm);
    this.Text = "Параметры конвертации";
    this.FormClosing += new FormClosingEventHandler(this.InputParamsForm_FormClosing);
    this.Shown += new EventHandler(this.InputParamsForm_Shown);
    this.tlpMain.ResumeLayout(false);
    this.tlpMain.PerformLayout();
    this.ResumeLayout(false);
  }
}
