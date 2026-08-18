// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.ReplaceTemplateForm
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator;
using MWCommon;
using MWControls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client;

public class ReplaceTemplateForm : Form
{
  private IContainer components;
  private Panel panel1;
  private Button btnCancel;
  private Button btnOK;
  private PictureBox pictureBox1;
  private MWLabel mwLabel1;
  private MWLabel mwLabel2;
  private MWLabel mwLabel3;
  private SaveFileDialog revFileDialog;
  private ButtonEdit edNewTemplate;
  private Label label1;
  private ButtonEdit edBackupFile;
  private Label label2;

  public long NewTemplateId { get; set; }

  public string BackupFilePath { get; set; } = string.Empty;

  public ReplaceTemplateForm() => this.InitializeComponent();

  private void btnForAttr_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    long[] numArray = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("ECO.Client_446"), LocalizationHolder.rm.GetString("ECO.Client_447"), RevHelper.idObjTypeRevTemplate, SelectionOptions.SelectObjects | SelectionOptions.DisableMultiselect);
    if (numArray == null || numArray.Length == 0)
      return;
    this.NewTemplateId = numArray[0];
    QuickObjectInfo objectInfo;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      objectInfo = sessionKeeper.Session.GetObjectInfo(this.NewTemplateId);
    if (objectInfo.Empty)
    {
      this.NewTemplateId = 0L;
      this.edNewTemplate.Text = "";
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_448"), LocalizationHolder.rm.GetString("ECO.Client_176"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
      this.edNewTemplate.Text = objectInfo.Caption;
  }

  private void buttonEdit1_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    if (this.revFileDialog.ShowDialog() != DialogResult.OK)
      return;
    this.BackupFilePath = this.revFileDialog.FileName;
    this.edBackupFile.Text = this.BackupFilePath;
  }

  private void btnOK_Click(object sender, EventArgs e)
  {
    if (this.NewTemplateId == 0L)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_449"), LocalizationHolder.rm.GetString("ECO.Client_176"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      this.DialogResult = DialogResult.None;
    }
    else
    {
      if (!(this.BackupFilePath == string.Empty))
        return;
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_450"), LocalizationHolder.rm.GetString("ECO.Client_176"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      this.DialogResult = DialogResult.None;
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ReplaceTemplateForm));
    this.panel1 = new Panel();
    this.btnCancel = new Button();
    this.btnOK = new Button();
    this.pictureBox1 = new PictureBox();
    this.mwLabel1 = new MWLabel();
    this.mwLabel2 = new MWLabel();
    this.mwLabel3 = new MWLabel();
    this.revFileDialog = new SaveFileDialog();
    this.edNewTemplate = new ButtonEdit();
    this.label1 = new Label();
    this.edBackupFile = new ButtonEdit();
    this.label2 = new Label();
    this.panel1.SuspendLayout();
    ((ISupportInitialize) this.pictureBox1).BeginInit();
    this.edNewTemplate.Properties.BeginInit();
    this.edBackupFile.Properties.BeginInit();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.btnCancel);
    this.panel1.Controls.Add((Control) this.btnOK);
    this.panel1.Dock = DockStyle.Bottom;
    this.panel1.Location = new Point(0, 396);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(927, 54);
    this.panel1.TabIndex = 0;
    this.btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(820, 12);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(95, 32 /*0x20*/);
    this.btnCancel.TabIndex = 1;
    this.btnCancel.Text = "&Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnOK.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Location = new Point(716, 12);
    this.btnOK.Name = "btnOK";
    this.btnOK.Size = new Size(95, 32 /*0x20*/);
    this.btnOK.TabIndex = 0;
    this.btnOK.Text = "&Да";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnOK.Click += new EventHandler(this.btnOK_Click);
    this.pictureBox1.Image = (Image) Intermech.ECO.Client.Properties.Resources.Roger;
    this.pictureBox1.Location = new Point(12, 12);
    this.pictureBox1.Name = "pictureBox1";
    this.pictureBox1.Size = new Size(146, 142);
    this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
    this.pictureBox1.TabIndex = 1;
    this.pictureBox1.TabStop = false;
    this.mwLabel1.Anchor = AnchorStyles.Top;
    this.mwLabel1.AutoSize = true;
    this.mwLabel1.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.mwLabel1.ForeColor = Color.Red;
    this.mwLabel1.Location = new Point(322, 9);
    this.mwLabel1.Name = "mwLabel1";
    this.mwLabel1.Size = new Size(418, 25);
    this.mwLabel1.StringFrmt = StringFormatEnum.GenericTypographic;
    this.mwLabel1.TabIndex = 3;
    this.mwLabel1.Text = "ОСТАНОВИТЕСЬ! НЕ ДЕЛАЙТЕ ЭТОГО!";
    this.mwLabel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.mwLabel2.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.mwLabel2.Location = new Point(168, 42);
    this.mwLabel2.Name = "mwLabel2";
    this.mwLabel2.Size = new Size(751, 122);
    this.mwLabel2.StringFrmt = StringFormatEnum.GenericTypographic;
    this.mwLabel2.TabIndex = 4;
    this.mwLabel2.Text = componentResourceManager.GetString("mwLabel2.Text");
    this.mwLabel3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.mwLabel3.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.mwLabel3.ForeColor = Color.Red;
    this.mwLabel3.Location = new Point(27, 319);
    this.mwLabel3.Name = "mwLabel3";
    this.mwLabel3.Size = new Size(874, 59);
    this.mwLabel3.StringFrmt = StringFormatEnum.GenericTypographic;
    this.mwLabel3.TabIndex = 5;
    this.mwLabel3.Text = "Помните, вы делаете замену шаблона НА СВОЙ СТРАХ И РИСК! В случае неудачи никто не вернет вам потерянную информацию и не восстановит испорченное извещение.";
    this.revFileDialog.Filter = "Извещения INTERMECH|*.revx";
    this.revFileDialog.Title = "Сохранение резервной копии файла извещение";
    this.edNewTemplate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.edNewTemplate.Location = new Point(12, 212);
    this.edNewTemplate.Name = "edNewTemplate";
    this.edNewTemplate.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.edNewTemplate.Properties.ButtonClick += new ButtonPressedEventHandler(this.btnForAttr_Properties_ButtonClick);
    this.edNewTemplate.Size = new Size(903, 26);
    this.edNewTemplate.TabIndex = 14;
    this.label1.AutoSize = true;
    this.label1.Location = new Point(14, 185);
    this.label1.Name = "label1";
    this.label1.Size = new Size(210, 20);
    this.label1.TabIndex = 15;
    this.label1.Text = "Новый шаблон извещения";
    this.edBackupFile.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.edBackupFile.EditValue = (object) "";
    this.edBackupFile.Location = new Point(12, 279);
    this.edBackupFile.Name = "edBackupFile";
    this.edBackupFile.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.edBackupFile.Properties.ButtonClick += new ButtonPressedEventHandler(this.buttonEdit1_Properties_ButtonClick);
    this.edBackupFile.Size = new Size(903, 26);
    this.edBackupFile.TabIndex = 16 /*0x10*/;
    this.label2.AutoSize = true;
    this.label2.Location = new Point(14, 256 /*0x0100*/);
    this.label2.Name = "label2";
    this.label2.Size = new Size(419, 20);
    this.label2.TabIndex = 17;
    this.label2.Text = "Резервная копия файла извещения (ОБЯЗАТЕЛЬНО)";
    this.AcceptButton = (IButtonControl) this.btnOK;
    this.AutoScaleDimensions = new SizeF(9f, 20f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(927, 450);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.edBackupFile);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.edNewTemplate);
    this.Controls.Add((Control) this.mwLabel3);
    this.Controls.Add((Control) this.mwLabel2);
    this.Controls.Add((Control) this.mwLabel1);
    this.Controls.Add((Control) this.pictureBox1);
    this.Controls.Add((Control) this.panel1);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ReplaceTemplateForm);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Замена шаблона извещения";
    this.panel1.ResumeLayout(false);
    ((ISupportInitialize) this.pictureBox1).EndInit();
    this.edNewTemplate.Properties.EndInit();
    this.edBackupFile.Properties.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
