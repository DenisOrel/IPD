// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.DrawingTypeEditor
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Cadmech.Integrator.Properties;
using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal class DrawingTypeEditor : Form
{
  private DrawingTypeSettings dwgType;
  private IContainer components;
  private Button btOK;
  private Button btCancel;
  private Label lbXRefMode;
  private ComboBox cbXRefMode;
  private Label lbStmEditor;
  private TextBox tbStmName;
  private GroupBox gbXRefMode;
  private GroupBox gbStm;
  private PictureBox pbXRefMode;
  private PictureBox pbStm;

  public DrawingTypeEditor() => this.InitializeComponent();

  private void DrawingTypeEditor_Shown(object sender, EventArgs e)
  {
    if (this.dwgType != null)
    {
      foreach (XRefMode id in Enum.GetValues(typeof (XRefMode)))
        this.cbXRefMode.Items.Add((object) new LocalId<XRefMode>(id, EnumTypeHelper.GetCaption((Enum) id)));
      this.Text = this.dwgType.DocumentType.Name;
      this.cbXRefMode.SelectedIndex = (int) this.dwgType.XRefMode;
      this.tbStmName.Text = this.dwgType.StmName;
      this.btOK.Enabled = true;
    }
    else
      this.btOK.Enabled = false;
  }

  private void btOK_Click(object sender, EventArgs e)
  {
    this.dwgType.XRefMode = ((LocalId<XRefMode>) this.cbXRefMode.SelectedItem).Id;
    this.dwgType.StmName = this.tbStmName.Text.Trim();
  }

  public DrawingTypeSettings DrawingType
  {
    get => this.dwgType;
    set => this.dwgType = value;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DrawingTypeEditor));
    this.btOK = new Button();
    this.btCancel = new Button();
    this.lbXRefMode = new Label();
    this.cbXRefMode = new ComboBox();
    this.lbStmEditor = new Label();
    this.tbStmName = new TextBox();
    this.gbXRefMode = new GroupBox();
    this.pbXRefMode = new PictureBox();
    this.gbStm = new GroupBox();
    this.pbStm = new PictureBox();
    this.gbXRefMode.SuspendLayout();
    ((ISupportInitialize) this.pbXRefMode).BeginInit();
    this.gbStm.SuspendLayout();
    ((ISupportInitialize) this.pbStm).BeginInit();
    this.SuspendLayout();
    this.btOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btOK.DialogResult = DialogResult.OK;
    this.btOK.Location = new Point(316, 307);
    this.btOK.Name = "btOK";
    this.btOK.Size = new Size(75, 23);
    this.btOK.TabIndex = 2;
    this.btOK.Text = "OK";
    this.btOK.UseVisualStyleBackColor = true;
    this.btOK.Click += new EventHandler(this.btOK_Click);
    this.btCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btCancel.DialogResult = DialogResult.Cancel;
    this.btCancel.Location = new Point(397, 307);
    this.btCancel.Name = "btCancel";
    this.btCancel.Size = new Size(75, 23);
    this.btCancel.TabIndex = 3;
    this.btCancel.Text = "Отмена";
    this.btCancel.UseVisualStyleBackColor = true;
    this.lbXRefMode.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.lbXRefMode.Location = new Point(44, 25);
    this.lbXRefMode.Name = "lbXRefMode";
    this.lbXRefMode.Size = new Size(410, 39);
    this.lbXRefMode.TabIndex = 0;
    this.lbXRefMode.Text = "Укажите режим регистрации в базе IPS внешних ссылок из чертежей данного типа. Поддерживаются ссылки на DWG, PDF и растровые изображения.\r\n";
    this.cbXRefMode.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.cbXRefMode.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbXRefMode.FormattingEnabled = true;
    this.cbXRefMode.Location = new Point(47, 76);
    this.cbXRefMode.Name = "cbXRefMode";
    this.cbXRefMode.Size = new Size(407, 21);
    this.cbXRefMode.TabIndex = 1;
    this.lbStmEditor.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.lbStmEditor.Location = new Point(44, 25);
    this.lbStmEditor.Name = "lbStmEditor";
    this.lbStmEditor.Size = new Size(410, 89);
    this.lbStmEditor.TabIndex = 0;
    this.lbStmEditor.Text = componentResourceManager.GetString("lbStmEditor.Text");
    this.tbStmName.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.tbStmName.Location = new Point(47, 124);
    this.tbStmName.Name = "tbStmName";
    this.tbStmName.Size = new Size(407, 20);
    this.tbStmName.TabIndex = 1;
    this.gbXRefMode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.gbXRefMode.Controls.Add((Control) this.pbXRefMode);
    this.gbXRefMode.Controls.Add((Control) this.cbXRefMode);
    this.gbXRefMode.Controls.Add((Control) this.lbXRefMode);
    this.gbXRefMode.Location = new Point(12, 17);
    this.gbXRefMode.Margin = new Padding(3, 8, 3, 8);
    this.gbXRefMode.Name = "gbXRefMode";
    this.gbXRefMode.Padding = new Padding(3, 12, 3, 8);
    this.gbXRefMode.Size = new Size(460, 108);
    this.gbXRefMode.TabIndex = 0;
    this.gbXRefMode.TabStop = false;
    this.gbXRefMode.Text = "Внешние ссылки";
    this.pbXRefMode.Image = (Image) Resources.IR_XRef_32x32;
    this.pbXRefMode.Location = new Point(6, 25);
    this.pbXRefMode.Name = "pbXRefMode";
    this.pbXRefMode.Size = new Size(32 /*0x20*/, 32 /*0x20*/);
    this.pbXRefMode.TabIndex = 2;
    this.pbXRefMode.TabStop = false;
    this.gbStm.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.gbStm.Controls.Add((Control) this.pbStm);
    this.gbStm.Controls.Add((Control) this.tbStmName);
    this.gbStm.Controls.Add((Control) this.lbStmEditor);
    this.gbStm.Location = new Point(12, 141);
    this.gbStm.Margin = new Padding(3, 8, 3, 8);
    this.gbStm.Name = "gbStm";
    this.gbStm.Padding = new Padding(3, 12, 3, 8);
    this.gbStm.Size = new Size(460, 155);
    this.gbStm.TabIndex = 1;
    this.gbStm.TabStop = false;
    this.gbStm.Text = "Сканирование штампа чертежа";
    this.pbStm.Image = (Image) Resources.IR_Config_32x32;
    this.pbStm.Location = new Point(6, 25);
    this.pbStm.Name = "pbStm";
    this.pbStm.Size = new Size(32 /*0x20*/, 32 /*0x20*/);
    this.pbStm.TabIndex = 4;
    this.pbStm.TabStop = false;
    this.AcceptButton = (IButtonControl) this.btCancel;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackColor = SystemColors.Control;
    this.CancelButton = (IButtonControl) this.btCancel;
    this.ClientSize = new Size(484, 342);
    this.Controls.Add((Control) this.gbStm);
    this.Controls.Add((Control) this.gbXRefMode);
    this.Controls.Add((Control) this.btCancel);
    this.Controls.Add((Control) this.btOK);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MinimumSize = new Size(490, 370);
    this.Name = nameof (DrawingTypeEditor);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Shown += new EventHandler(this.DrawingTypeEditor_Shown);
    this.gbXRefMode.ResumeLayout(false);
    ((ISupportInitialize) this.pbXRefMode).EndInit();
    this.gbStm.ResumeLayout(false);
    this.gbStm.PerformLayout();
    ((ISupportInitialize) this.pbStm).EndInit();
    this.ResumeLayout(false);
  }
}
