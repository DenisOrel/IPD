// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Commands.Replace.ReplaceCommandDialog
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.Commands.Replace;

/// <summary>
/// 
/// </summary>
internal class ReplaceCommandDialog : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel pnlButtons;
  private Button btnCancel;
  private Button btnApply;
  private Label lblInfo;
  private CheckBox chbDraftMode;

  /// <summary>
  /// 
  /// </summary>
  private ReplaceCommandDialog() => this.InitializeComponent();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectId"></param>
  /// <param name="draftMode"></param>
  /// <returns></returns>
  public static DialogResult Show(long objectId, out bool draftMode)
  {
    ReplaceCommandDialog replaceCommandDialog = new ReplaceCommandDialog();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      replaceCommandDialog.lblInfo.Text = string.Format(LocalizationHolder.rm.GetString(sc_19300.ssp_techcard_19301()), (object) TechCardConsts.Utils.GetObjectString(objectId, sessionKeeper.Session));
    int num = (int) replaceCommandDialog.ShowDialog();
    draftMode = replaceCommandDialog.chbDraftMode.Checked;
    return (DialogResult) num;
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
    this.pnlButtons = new Panel();
    this.btnCancel = new Button();
    this.btnApply = new Button();
    this.lblInfo = new Label();
    this.chbDraftMode = new CheckBox();
    this.pnlButtons.SuspendLayout();
    this.SuspendLayout();
    this.pnlButtons.BackColor = SystemColors.Control;
    this.pnlButtons.Controls.Add((Control) this.btnCancel);
    this.pnlButtons.Controls.Add((Control) this.btnApply);
    this.pnlButtons.Dock = DockStyle.Bottom;
    this.pnlButtons.Location = new Point(0, 74);
    this.pnlButtons.Name = "pnlButtons";
    this.pnlButtons.Size = new Size(418, 39);
    this.pnlButtons.TabIndex = 4;
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.ImeMode = ImeMode.NoControl;
    this.btnCancel.Location = new Point(334, 7);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(75, 23);
    this.btnCancel.TabIndex = 1;
    this.btnCancel.Text = "Отмена";
    this.btnApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnApply.DialogResult = DialogResult.OK;
    this.btnApply.ImeMode = ImeMode.NoControl;
    this.btnApply.Location = new Point(254, 7);
    this.btnApply.Name = "btnApply";
    this.btnApply.Size = new Size(75, 23);
    this.btnApply.TabIndex = 0;
    this.btnApply.Text = "ОК";
    this.lblInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.lblInfo.Location = new Point(12, 9);
    this.lblInfo.Name = "lblInfo";
    this.lblInfo.Size = new Size(394, 43);
    this.lblInfo.TabIndex = 5;
    this.lblInfo.Text = "Заменить объект";
    this.lblInfo.TextAlign = ContentAlignment.MiddleCenter;
    this.chbDraftMode.AutoSize = true;
    this.chbDraftMode.Location = new Point(12, 52);
    this.chbDraftMode.Name = "chbDraftMode";
    this.chbDraftMode.Size = new Size(111, 17);
    this.chbDraftMode.TabIndex = 6;
    this.chbDraftMode.Text = "Включая эскизы";
    this.chbDraftMode.UseVisualStyleBackColor = true;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackColor = SystemColors.ControlLightLight;
    this.ClientSize = new Size(418, 113);
    this.Controls.Add((Control) this.chbDraftMode);
    this.Controls.Add((Control) this.lblInfo);
    this.Controls.Add((Control) this.pnlButtons);
    this.Name = "TechReplaceItemsDialog";
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Замена";
    this.pnlButtons.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
