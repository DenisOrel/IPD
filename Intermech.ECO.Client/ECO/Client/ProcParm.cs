// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.ProcParm
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client;

public class ProcParm : Form
{
  public long processId;
  public string theme = "";
  public string message = "";
  private IContainer components;
  private Panel panel1;
  private Button btnOK;
  private Button btnCancel;
  private ButtonEdit beProcTemplate;
  private Label label1;
  private Label label2;
  private TextBox tbTopic;
  private TextBox tbText;
  private Label label3;

  public ProcParm() => this.InitializeComponent();

  public bool Execute()
  {
    if (this.ShowDialog() != DialogResult.OK)
      return false;
    this.theme = this.tbTopic.Text;
    this.message = this.tbText.Text;
    return true;
  }

  private void beProcTemplate_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    long[] numArray = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("ECO.Client_356"), LocalizationHolder.rm.GetString("ECO.Client_357"), RevHelper.idObjProcTemplate, SelectionOptions.Default);
    if (numArray == null || numArray.Length == 0)
      return;
    this.processId = numArray[0];
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(this.processId);
      if (!objectInfo.Empty)
        this.beProcTemplate.Text = objectInfo.Caption;
      else
        this.processId = 0L;
    }
  }

  private void btnOK_Click(object sender, EventArgs e)
  {
    if (this.processId != 0L)
      return;
    int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_358"), LocalizationHolder.rm.GetString("ECO.Client_176"), MessageBoxButtons.OK);
    this.DialogResult = DialogResult.None;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.panel1 = new Panel();
    this.btnOK = new Button();
    this.btnCancel = new Button();
    this.beProcTemplate = new ButtonEdit();
    this.label1 = new Label();
    this.label2 = new Label();
    this.tbTopic = new TextBox();
    this.tbText = new TextBox();
    this.label3 = new Label();
    this.panel1.SuspendLayout();
    this.beProcTemplate.Properties.BeginInit();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.btnOK);
    this.panel1.Controls.Add((Control) this.btnCancel);
    this.panel1.Dock = DockStyle.Bottom;
    this.panel1.Location = new Point(0, 221);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(598, 32 /*0x20*/);
    this.panel1.TabIndex = 0;
    this.btnOK.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Location = new Point(430, 4);
    this.btnOK.Name = "btnOK";
    this.btnOK.Size = new Size(75, 23);
    this.btnOK.TabIndex = 1;
    this.btnOK.Text = "OK";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnOK.Click += new EventHandler(this.btnOK_Click);
    this.btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(511 /*0x01FF*/, 4);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(75, 23);
    this.btnCancel.TabIndex = 0;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.beProcTemplate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.beProcTemplate.EditValue = (object) "(Не выбран)";
    this.beProcTemplate.Location = new Point(12, 24);
    this.beProcTemplate.Name = "beProcTemplate";
    this.beProcTemplate.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.beProcTemplate.Properties.ReadOnly = true;
    this.beProcTemplate.Properties.ButtonClick += new ButtonPressedEventHandler(this.beProcTemplate_Properties_ButtonClick);
    this.beProcTemplate.Size = new Size(574, 20);
    this.beProcTemplate.TabIndex = 1;
    this.label1.AutoSize = true;
    this.label1.Location = new Point(9, 8);
    this.label1.Name = "label1";
    this.label1.Size = new Size(97, 13);
    this.label1.TabIndex = 2;
    this.label1.Text = "Шаблон процесса";
    this.label2.AutoSize = true;
    this.label2.Location = new Point(9, 56);
    this.label2.Name = "label2";
    this.label2.Size = new Size(94, 13);
    this.label2.TabIndex = 3;
    this.label2.Text = "Тема сообщения";
    this.tbTopic.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbTopic.Location = new Point(12, 72);
    this.tbTopic.Name = "tbTopic";
    this.tbTopic.Size = new Size(574, 20);
    this.tbTopic.TabIndex = 4;
    this.tbText.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.tbText.Location = new Point(12, 112 /*0x70*/);
    this.tbText.Multiline = true;
    this.tbText.Name = "tbText";
    this.tbText.Size = new Size(574, 94);
    this.tbText.TabIndex = 5;
    this.label3.AutoSize = true;
    this.label3.Location = new Point(9, 96 /*0x60*/);
    this.label3.Name = "label3";
    this.label3.Size = new Size(97, 13);
    this.label3.TabIndex = 6;
    this.label3.Text = "Текст сообщения";
    this.AcceptButton = (IButtonControl) this.btnOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(598, 253);
    this.Controls.Add((Control) this.label3);
    this.Controls.Add((Control) this.tbText);
    this.Controls.Add((Control) this.tbTopic);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.beProcTemplate);
    this.Controls.Add((Control) this.panel1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ProcParm);
    this.Text = "Запуск процесса для записей ЖИ";
    this.panel1.ResumeLayout(false);
    this.beProcTemplate.Properties.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
