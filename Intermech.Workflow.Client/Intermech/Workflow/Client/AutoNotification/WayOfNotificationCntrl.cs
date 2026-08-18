// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.AutoNotification.WayOfNotificationCntrl
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Interfaces.Workflow.AutoNotification;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client.AutoNotification;

public class WayOfNotificationCntrl : UserControl, ICanSaveNotifSettings
{
  private AutoNotificationSettings _notifSettings;
  private string _message;
  private string _formatMessage;
  private WayOfNotificationEnum _wayOfNotification;
  private bool _isChanged;
  private IContainer components;
  private GroupBox gbWayOfNotification;
  private RadioButton rbMixed;
  private RadioButton rbEmail;
  private RadioButton rbInternalMail;
  private GroupBox gbMessageText;
  private RichTextBox tbMessage;
  private ContextMenuStrip contextMenuStrip1;
  private ToolStripMenuItem cmsiCancel;
  private ToolStripSeparator toolStripSeparator1;
  private ToolStripMenuItem cmsiCut;
  private ToolStripMenuItem cmsiCopy;
  private ToolStripMenuItem cmsiPaste;
  private ToolStripMenuItem cmsiDelete;
  private ToolStripSeparator toolStripSeparator2;
  private ToolStripMenuItem cmsiSelectAll;

  public event EventHandler Modified;

  public bool IsChanged
  {
    get => this._isChanged;
    private set
    {
      this._isChanged = value;
      EventHandler modified = this.Modified;
      if (!value || modified == null)
        return;
      modified((object) this, (EventArgs) null);
    }
  }

  public WayOfNotificationCntrl(AutoNotificationSettings notifSettings)
  {
    this.InitializeComponent();
    this.tbMessage.ContextMenuStrip = this.contextMenuStrip1;
    this.cmsiCancel.Click += new EventHandler(this.cancelMenuItem_Click);
    this.cmsiCut.Click += new EventHandler(this.cutMenuItem_Click);
    this.cmsiCopy.Click += new EventHandler(this.copyMenuItem_Click);
    this.cmsiPaste.Click += new EventHandler(this.pasteMenuItem_Click);
    this.cmsiDelete.Click += new EventHandler(this.deleteMenuItem_Click);
    this.cmsiSelectAll.Click += new EventHandler(this.selectAllMenuItem_Click);
    this._notifSettings = notifSettings;
    this._wayOfNotification = this._notifSettings.WayOfNotification;
    this._message = this._notifSettings.Message;
    this.UpdateControl();
  }

  private void selectAllMenuItem_Click(object sender, EventArgs e)
  {
    this.tbMessage.SelectionStart = 0;
    this.tbMessage.SelectionLength = this.tbMessage.Text.Length;
  }

  private void deleteMenuItem_Click(object sender, EventArgs e)
  {
    int selectionStart = this.tbMessage.SelectionStart;
    this.tbMessage.Text = this.tbMessage.Text.Remove(selectionStart, this.tbMessage.SelectionLength);
    this.tbMessage.SelectionStart = selectionStart;
  }

  private void pasteMenuItem_Click(object sender, EventArgs e)
  {
    string text = Clipboard.GetText();
    int selectionStart = this.tbMessage.SelectionStart;
    this.tbMessage.Text = this.tbMessage.SelectionLength > 0 ? this.tbMessage.Text.Replace(this.tbMessage.SelectedText, text) : this.tbMessage.Text.Insert(selectionStart, text);
    this.tbMessage.SelectionStart = selectionStart + text.Length;
  }

  private void copyMenuItem_Click(object sender, EventArgs e)
  {
    Clipboard.SetText(this.tbMessage.SelectedText);
  }

  private void cutMenuItem_Click(object sender, EventArgs e)
  {
    Clipboard.SetText(this.tbMessage.SelectedText);
    int selectionStart = this.tbMessage.SelectionStart;
    this.tbMessage.Text = this.tbMessage.Text.Remove(selectionStart, this.tbMessage.SelectionLength);
    this.tbMessage.SelectionStart = selectionStart;
  }

  private void cancelMenuItem_Click(object sender, EventArgs e)
  {
    this.tbMessage.Text = this._formatMessage;
  }

  private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
  {
    this.tbMessage.Focus();
    this.cmsiCut.Enabled = this.cmsiCopy.Enabled = this.cmsiDelete.Enabled = this.tbMessage.SelectionLength > 0;
    this.cmsiPaste.Enabled = Clipboard.ContainsText();
  }

  private void rbInternalMail_CheckedChanged(object sender, EventArgs e)
  {
    this._wayOfNotification = WayOfNotificationEnum.InternalMail;
    this.IsChanged = true;
  }

  private void rbEmail_CheckedChanged(object sender, EventArgs e)
  {
    this._wayOfNotification = WayOfNotificationEnum.ExternalMail;
    this.IsChanged = true;
  }

  private void rbMixed_CheckedChanged(object sender, EventArgs e)
  {
    this._wayOfNotification = WayOfNotificationEnum.InternalAndExternalMail;
    this.IsChanged = true;
  }

  private void tbMessage_TextChanged(object sender, EventArgs e) => this.IsChanged = true;

  private void UpdateControl()
  {
    switch (this._wayOfNotification)
    {
      case WayOfNotificationEnum.InternalMail:
        this.rbInternalMail.Checked = true;
        break;
      case WayOfNotificationEnum.ExternalMail:
        this.rbEmail.Checked = true;
        break;
      case WayOfNotificationEnum.InternalAndExternalMail:
        this.rbMixed.Checked = true;
        break;
    }
    this._formatMessage = this._message.Replace("<br>", "\n");
    this.tbMessage.Text = this._formatMessage;
  }

  public override void Refresh()
  {
    base.Refresh();
    this._wayOfNotification = this._notifSettings.WayOfNotification;
    this._message = this._notifSettings.Message;
    this.UpdateControl();
  }

  public void SaveSettings()
  {
    this._notifSettings.WayOfNotification = this._wayOfNotification;
    this._notifSettings.Message = this.tbMessage.Text.Replace("\n", "<br>");
    this.IsChanged = false;
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
    this.gbWayOfNotification = new GroupBox();
    this.rbMixed = new RadioButton();
    this.rbEmail = new RadioButton();
    this.rbInternalMail = new RadioButton();
    this.gbMessageText = new GroupBox();
    this.tbMessage = new RichTextBox();
    this.contextMenuStrip1 = new ContextMenuStrip(this.components);
    this.cmsiCancel = new ToolStripMenuItem();
    this.toolStripSeparator1 = new ToolStripSeparator();
    this.cmsiCut = new ToolStripMenuItem();
    this.cmsiCopy = new ToolStripMenuItem();
    this.cmsiPaste = new ToolStripMenuItem();
    this.cmsiDelete = new ToolStripMenuItem();
    this.toolStripSeparator2 = new ToolStripSeparator();
    this.cmsiSelectAll = new ToolStripMenuItem();
    this.gbWayOfNotification.SuspendLayout();
    this.gbMessageText.SuspendLayout();
    this.contextMenuStrip1.SuspendLayout();
    this.SuspendLayout();
    this.gbWayOfNotification.Controls.Add((Control) this.rbMixed);
    this.gbWayOfNotification.Controls.Add((Control) this.rbEmail);
    this.gbWayOfNotification.Controls.Add((Control) this.rbInternalMail);
    this.gbWayOfNotification.Dock = DockStyle.Top;
    this.gbWayOfNotification.Location = new Point(0, 0);
    this.gbWayOfNotification.Name = "gbWayOfNotification";
    this.gbWayOfNotification.Size = new Size(527, 100);
    this.gbWayOfNotification.TabIndex = 0;
    this.gbWayOfNotification.TabStop = false;
    this.gbWayOfNotification.Text = "Способ уведомления";
    this.rbMixed.AutoSize = true;
    this.rbMixed.Location = new Point(7, 68);
    this.rbMixed.Name = "rbMixed";
    this.rbMixed.Size = new Size(86, 17);
    this.rbMixed.TabIndex = 2;
    this.rbMixed.Text = "Смешанный";
    this.rbMixed.UseVisualStyleBackColor = true;
    this.rbMixed.CheckedChanged += new EventHandler(this.rbMixed_CheckedChanged);
    this.rbEmail.AutoSize = true;
    this.rbEmail.Location = new Point(7, 44);
    this.rbEmail.Name = "rbEmail";
    this.rbEmail.Size = new Size(120, 17);
    this.rbEmail.TabIndex = 1;
    this.rbEmail.Text = "На внешнюю почту";
    this.rbEmail.UseVisualStyleBackColor = true;
    this.rbEmail.CheckedChanged += new EventHandler(this.rbEmail_CheckedChanged);
    this.rbInternalMail.AutoSize = true;
    this.rbInternalMail.Checked = true;
    this.rbInternalMail.Location = new Point(7, 20);
    this.rbInternalMail.Name = "rbInternalMail";
    this.rbInternalMail.Size = new Size(134, 17);
    this.rbInternalMail.TabIndex = 0;
    this.rbInternalMail.TabStop = true;
    this.rbInternalMail.Text = "На внутреннюю почту";
    this.rbInternalMail.UseVisualStyleBackColor = true;
    this.rbInternalMail.CheckedChanged += new EventHandler(this.rbInternalMail_CheckedChanged);
    this.gbMessageText.Controls.Add((Control) this.tbMessage);
    this.gbMessageText.Dock = DockStyle.Fill;
    this.gbMessageText.Location = new Point(0, 100);
    this.gbMessageText.Name = "gbMessageText";
    this.gbMessageText.Size = new Size(527, 187);
    this.gbMessageText.TabIndex = 1;
    this.gbMessageText.TabStop = false;
    this.gbMessageText.Text = "Текст уведомления";
    this.tbMessage.Dock = DockStyle.Fill;
    this.tbMessage.Location = new Point(3, 16 /*0x10*/);
    this.tbMessage.Name = "tbMessage";
    this.tbMessage.Size = new Size(521, 168);
    this.tbMessage.TabIndex = 0;
    this.tbMessage.Text = "";
    this.tbMessage.TextChanged += new EventHandler(this.tbMessage_TextChanged);
    this.contextMenuStrip1.Items.AddRange(new ToolStripItem[8]
    {
      (ToolStripItem) this.cmsiCancel,
      (ToolStripItem) this.toolStripSeparator1,
      (ToolStripItem) this.cmsiCut,
      (ToolStripItem) this.cmsiCopy,
      (ToolStripItem) this.cmsiPaste,
      (ToolStripItem) this.cmsiDelete,
      (ToolStripItem) this.toolStripSeparator2,
      (ToolStripItem) this.cmsiSelectAll
    });
    this.contextMenuStrip1.Name = "contextMenuStrip1";
    this.contextMenuStrip1.Size = new Size(149, 148);
    this.contextMenuStrip1.Opening += new CancelEventHandler(this.contextMenuStrip1_Opening);
    this.cmsiCancel.Name = "cmsiCancel";
    this.cmsiCancel.Size = new Size(148, 22);
    this.cmsiCancel.Tag = (object) "0";
    this.cmsiCancel.Text = "Отмена";
    this.toolStripSeparator1.Name = "toolStripSeparator1";
    this.toolStripSeparator1.Size = new Size(145, 6);
    this.cmsiCut.Name = "cmsiCut";
    this.cmsiCut.Size = new Size(148, 22);
    this.cmsiCut.Tag = (object) "1";
    this.cmsiCut.Text = "Вырезать";
    this.cmsiCopy.Name = "cmsiCopy";
    this.cmsiCopy.Size = new Size(148, 22);
    this.cmsiCopy.Tag = (object) "2";
    this.cmsiCopy.Text = "Копировать";
    this.cmsiPaste.Name = "cmsiPaste";
    this.cmsiPaste.Size = new Size(148, 22);
    this.cmsiPaste.Tag = (object) "3";
    this.cmsiPaste.Text = "Вставить";
    this.cmsiDelete.Name = "cmsiDelete";
    this.cmsiDelete.Size = new Size(148, 22);
    this.cmsiDelete.Tag = (object) "4";
    this.cmsiDelete.Text = "Удалить";
    this.toolStripSeparator2.Name = "toolStripSeparator2";
    this.toolStripSeparator2.Size = new Size(145, 6);
    this.cmsiSelectAll.Name = "cmsiSelectAll";
    this.cmsiSelectAll.Size = new Size(148, 22);
    this.cmsiSelectAll.Tag = (object) "5";
    this.cmsiSelectAll.Text = "Выделить все";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackColor = SystemColors.Control;
    this.Controls.Add((Control) this.gbMessageText);
    this.Controls.Add((Control) this.gbWayOfNotification);
    this.Name = nameof (WayOfNotificationCntrl);
    this.Size = new Size(527, 287);
    this.gbWayOfNotification.ResumeLayout(false);
    this.gbWayOfNotification.PerformLayout();
    this.gbMessageText.ResumeLayout(false);
    this.contextMenuStrip1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
