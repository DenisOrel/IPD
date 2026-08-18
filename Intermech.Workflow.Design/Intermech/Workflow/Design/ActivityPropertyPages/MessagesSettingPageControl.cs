// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ActivityPropertyPages.MessagesSettingPageControl
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design.ActivityPropertyPages;

public class MessagesSettingPageControl : UserControl
{
  private ActivitySettings _settings;
  private bool _readOnly;
  private bool _loading;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private GroupBox MsgsGroupBox;
  private Panel MsgAbortPanel;
  private CheckBox MsgAbortCheckBox;
  private Button MsgAbortButton;
  private Panel MsgStopPanel;
  private CheckBox MsgStopCheckBox;
  private Button MsgStopButton;
  private Panel MsgBackPanel;
  private CheckBox MsgBackCheckBox;
  private Button MsgBackButton;
  private Panel MsgPeriodPanel;
  private CheckBox MsgPeriodCheckBox;
  private Button MsgPeriodButton;
  private Panel MsgReadPanel;
  private CheckBox MsgReadCheckBox;
  private Button MsgReadButton;
  private Panel MsgStartPanel;
  private CheckBox MsgStartCheckBox;
  private Button MsgStartButton;
  private ImageList TabsIL;
  private EnhToolTip ToolTip;

  public MessagesSettingPageControl()
  {
    this.InitializeComponent();
    this.MsgStartCheckBox.Tag = (object) this.MsgStartButton;
    this.MsgReadCheckBox.Tag = (object) this.MsgReadButton;
    this.MsgPeriodCheckBox.Tag = (object) this.MsgPeriodButton;
    this.MsgStopCheckBox.Tag = (object) this.MsgStopButton;
    this.MsgAbortCheckBox.Tag = (object) this.MsgAbortButton;
    this.MsgBackCheckBox.Tag = (object) this.MsgBackButton;
  }

  public bool ReadOnly
  {
    get => this._readOnly;
    set
    {
      this._readOnly = value;
      if (!value)
        return;
      ControlFuncs.SetControlsReadOnly((Control) this, (value ? 1 : 0) != 0, new List<Control>((IEnumerable<Control>) new Control[6]
      {
        (Control) this.MsgStartButton,
        (Control) this.MsgPeriodButton,
        (Control) this.MsgStopButton,
        (Control) this.MsgAbortButton,
        (Control) this.MsgReadButton,
        (Control) this.MsgBackButton
      }));
    }
  }

  public bool LoadMessagesSettingPageControl(
    ActivitySettings settings,
    IDBObject activityObject,
    bool rollbackVisible,
    IUserSession activitySession)
  {
    try
    {
      this._loading = true;
      bool flag1 = false;
      this._settings = settings;
      if (settings.ActivityType == wfConsts.AbortTypeID)
      {
        this.MsgStartPanel.Visible = false;
        this.MsgPeriodPanel.Visible = false;
      }
      IDBAttribute attributeById = activityObject.GetAttributeByID(wfConsts.AttrNotificationsID);
      settings.Notifications = new Notifications(activitySession);
      if (attributeById != null)
      {
        settings.Notifications.Load(attributeById);
        this.MsgStartCheckBox.Checked = settings.Notifications.StartNotify.Enabled;
        this.MsgPeriodCheckBox.Checked = settings.Notifications.PeriodNotify.Enabled;
        this.MsgStopCheckBox.Checked = settings.Notifications.StopNotify.Enabled;
        this.MsgAbortCheckBox.Checked = settings.Notifications.AbortNotify.Enabled;
        this.MsgReadCheckBox.Checked = settings.Notifications.ReadNotify.Enabled;
        this.MsgBackCheckBox.Checked = settings.Notifications.BackNotify.Enabled;
        bool flag2 = settings.ActivityType == wfConsts.SchemesTypeID || settings.ActivityType == wfConsts.ProcessesTypeID;
        this.MsgAbortPanel.Visible = flag2;
        this.MsgReadPanel.Visible = !flag2 && wfConsts.IsParticipantActivity(ActivityInfos.ActivityTypeToKind(settings.ActivityType));
        this.MsgBackPanel.Visible = rollbackVisible;
      }
      else
        flag1 = true;
      return flag1;
    }
    finally
    {
      this._loading = false;
    }
  }

  private void EditMessageClick(object sender, EventArgs e)
  {
    this.EditMessage(Convert.ToInt32((sender as Control).Tag));
  }

  private bool EditMessage(int index)
  {
    using (ComposeMessageForm composeMessageForm = new ComposeMessageForm())
    {
      composeMessageForm.ProcessID = this._settings.ObjectIDwithVars;
      Notification notification = this.GetNotification(index);
      if (notification == null)
        return false;
      composeMessageForm.Notification = notification;
      composeMessageForm.ReadOnly = this.ReadOnly;
      notification.Enabled = true;
      return composeMessageForm.ShowDialog() == DialogResult.OK;
    }
  }

  private Notification GetNotification(int index)
  {
    return this._settings.Notifications.List.Count >= index ? this._settings.Notifications.List[index - 1] : (Notification) null;
  }

  private void MsgCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    Control tag = (Control) (sender as Control).Tag;
    tag.Enabled = (sender as CheckBox).Checked;
    if (this._loading)
      return;
    int int32 = Convert.ToInt32(tag.Tag);
    if (tag.Enabled)
    {
      if (this.EditMessage(int32))
        return;
      (sender as CheckBox).Checked = false;
    }
    else
    {
      Notification notification = this.GetNotification(int32);
      if (notification == null)
        return;
      notification.Enabled = false;
    }
  }

  public bool Save(IDBObject activityToSave, bool modified)
  {
    if (this._settings.Notifications.Modified)
    {
      modified = true;
      IDBAttribute byId = activityToSave.Attributes.FindByID(wfConsts.AttrNotificationsID);
      if (byId != null)
        this._settings.Notifications.Save(byId);
    }
    return modified;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (MessagesSettingPageControl));
    this.MsgsGroupBox = new GroupBox();
    this.MsgAbortPanel = new Panel();
    this.MsgAbortCheckBox = new CheckBox();
    this.MsgAbortButton = new Button();
    this.TabsIL = new ImageList(this.components);
    this.MsgStopPanel = new Panel();
    this.MsgStopCheckBox = new CheckBox();
    this.MsgStopButton = new Button();
    this.MsgBackPanel = new Panel();
    this.MsgBackCheckBox = new CheckBox();
    this.MsgBackButton = new Button();
    this.MsgPeriodPanel = new Panel();
    this.MsgPeriodCheckBox = new CheckBox();
    this.MsgPeriodButton = new Button();
    this.MsgReadPanel = new Panel();
    this.MsgReadCheckBox = new CheckBox();
    this.MsgReadButton = new Button();
    this.MsgStartPanel = new Panel();
    this.MsgStartCheckBox = new CheckBox();
    this.MsgStartButton = new Button();
    this.ToolTip = new EnhToolTip(this.components);
    this.MsgsGroupBox.SuspendLayout();
    this.MsgAbortPanel.SuspendLayout();
    this.MsgStopPanel.SuspendLayout();
    this.MsgBackPanel.SuspendLayout();
    this.MsgPeriodPanel.SuspendLayout();
    this.MsgReadPanel.SuspendLayout();
    this.MsgStartPanel.SuspendLayout();
    this.SuspendLayout();
    this.MsgsGroupBox.AutoSize = true;
    this.MsgsGroupBox.BackColor = Color.Transparent;
    this.MsgsGroupBox.Controls.Add((Control) this.MsgAbortPanel);
    this.MsgsGroupBox.Controls.Add((Control) this.MsgStopPanel);
    this.MsgsGroupBox.Controls.Add((Control) this.MsgBackPanel);
    this.MsgsGroupBox.Controls.Add((Control) this.MsgPeriodPanel);
    this.MsgsGroupBox.Controls.Add((Control) this.MsgReadPanel);
    this.MsgsGroupBox.Controls.Add((Control) this.MsgStartPanel);
    this.MsgsGroupBox.Dock = DockStyle.Top;
    this.MsgsGroupBox.Location = new Point(0, 0);
    this.MsgsGroupBox.Name = "MsgsGroupBox";
    this.MsgsGroupBox.Padding = new Padding(3, 7, 3, 10);
    this.MsgsGroupBox.Size = new Size(610, 205);
    this.MsgsGroupBox.TabIndex = 1;
    this.MsgsGroupBox.TabStop = false;
    this.MsgsGroupBox.Text = "Отправлять сообщение";
    this.MsgAbortPanel.Controls.Add((Control) this.MsgAbortCheckBox);
    this.MsgAbortPanel.Controls.Add((Control) this.MsgAbortButton);
    this.MsgAbortPanel.Dock = DockStyle.Top;
    this.MsgAbortPanel.Location = new Point(3, 167);
    this.MsgAbortPanel.Name = "MsgAbortPanel";
    this.MsgAbortPanel.Size = new Size(604, 28);
    this.MsgAbortPanel.TabIndex = 9;
    this.MsgAbortCheckBox.AutoSize = true;
    this.MsgAbortCheckBox.ImeMode = ImeMode.NoControl;
    this.MsgAbortCheckBox.Location = new Point(11, 5);
    this.MsgAbortCheckBox.Name = "MsgAbortCheckBox";
    this.MsgAbortCheckBox.Size = new Size(135, 21);
    this.MsgAbortCheckBox.TabIndex = 4;
    this.MsgAbortCheckBox.Tag = (object) "";
    this.MsgAbortCheckBox.Text = "Когда прервано";
    this.MsgAbortCheckBox.CheckedChanged += new EventHandler(this.MsgCheckBox_CheckedChanged);
    this.MsgAbortButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.MsgAbortButton.Enabled = false;
    this.MsgAbortButton.ImageAlign = ContentAlignment.MiddleLeft;
    this.MsgAbortButton.ImageIndex = 14;
    this.MsgAbortButton.ImageList = this.TabsIL;
    this.MsgAbortButton.ImeMode = ImeMode.NoControl;
    this.MsgAbortButton.Location = new Point(459, 0);
    this.MsgAbortButton.Name = "MsgAbortButton";
    this.MsgAbortButton.Size = new Size(135, 29);
    this.MsgAbortButton.TabIndex = 5;
    this.MsgAbortButton.Tag = (object) "4";
    this.MsgAbortButton.Text = "Сообщение...";
    this.MsgAbortButton.TextAlign = ContentAlignment.MiddleRight;
    this.MsgAbortButton.Click += new EventHandler(this.EditMessageClick);
    this.TabsIL.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("TabsIL.ImageStream");
    this.TabsIL.TransparentColor = Color.Fuchsia;
    this.TabsIL.Images.SetKeyName(0, "");
    this.TabsIL.Images.SetKeyName(1, "user_gr.ico");
    this.TabsIL.Images.SetKeyName(2, "addon.ico");
    this.TabsIL.Images.SetKeyName(3, "");
    this.TabsIL.Images.SetKeyName(4, "");
    this.TabsIL.Images.SetKeyName(5, "");
    this.TabsIL.Images.SetKeyName(6, "");
    this.TabsIL.Images.SetKeyName(7, "back.ico");
    this.TabsIL.Images.SetKeyName(8, "sign.ico");
    this.TabsIL.Images.SetKeyName(9, "");
    this.TabsIL.Images.SetKeyName(10, "arc.ico");
    this.TabsIL.Images.SetKeyName(11, "");
    this.TabsIL.Images.SetKeyName(12, "status.png");
    this.TabsIL.Images.SetKeyName(13, "time.ico");
    this.TabsIL.Images.SetKeyName(14, "info_z.ico");
    this.TabsIL.Images.SetKeyName(15, "Подпроцесс2_16.bmp");
    this.MsgStopPanel.Controls.Add((Control) this.MsgStopCheckBox);
    this.MsgStopPanel.Controls.Add((Control) this.MsgStopButton);
    this.MsgStopPanel.Dock = DockStyle.Top;
    this.MsgStopPanel.Location = new Point(3, 138);
    this.MsgStopPanel.Name = "MsgStopPanel";
    this.MsgStopPanel.Size = new Size(604, 29);
    this.MsgStopPanel.TabIndex = 7;
    this.MsgStopCheckBox.AutoSize = true;
    this.MsgStopCheckBox.ImeMode = ImeMode.NoControl;
    this.MsgStopCheckBox.Location = new Point(11, 5);
    this.MsgStopCheckBox.Name = "MsgStopCheckBox";
    this.MsgStopCheckBox.Size = new Size(145, 21);
    this.MsgStopCheckBox.TabIndex = 6;
    this.MsgStopCheckBox.Tag = (object) "";
    this.MsgStopCheckBox.Text = "Когда выполнено";
    this.ToolTip.SetToolTip((Control) this.MsgStopCheckBox, "Сообщение будет отправлено в случае успешного выполнения (действие отправлено дальше)");
    this.MsgStopCheckBox.CheckedChanged += new EventHandler(this.MsgCheckBox_CheckedChanged);
    this.MsgStopButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.MsgStopButton.Enabled = false;
    this.MsgStopButton.ImageAlign = ContentAlignment.MiddleLeft;
    this.MsgStopButton.ImageIndex = 14;
    this.MsgStopButton.ImageList = this.TabsIL;
    this.MsgStopButton.ImeMode = ImeMode.NoControl;
    this.MsgStopButton.Location = new Point(459, 0);
    this.MsgStopButton.Name = "MsgStopButton";
    this.MsgStopButton.Size = new Size(135, 29);
    this.MsgStopButton.TabIndex = 7;
    this.MsgStopButton.Tag = (object) "3";
    this.MsgStopButton.Text = "Сообщение...";
    this.MsgStopButton.TextAlign = ContentAlignment.MiddleRight;
    this.MsgStopButton.Click += new EventHandler(this.EditMessageClick);
    this.MsgBackPanel.Controls.Add((Control) this.MsgBackCheckBox);
    this.MsgBackPanel.Controls.Add((Control) this.MsgBackButton);
    this.MsgBackPanel.Dock = DockStyle.Top;
    this.MsgBackPanel.Location = new Point(3, 109);
    this.MsgBackPanel.Name = "MsgBackPanel";
    this.MsgBackPanel.Size = new Size(604, 29);
    this.MsgBackPanel.TabIndex = 12;
    this.MsgBackCheckBox.AutoSize = true;
    this.MsgBackCheckBox.ImeMode = ImeMode.NoControl;
    this.MsgBackCheckBox.Location = new Point(11, 5);
    this.MsgBackCheckBox.Name = "MsgBackCheckBox";
    this.MsgBackCheckBox.Size = new Size(152, 21);
    this.MsgBackCheckBox.TabIndex = 6;
    this.MsgBackCheckBox.Tag = (object) "";
    this.MsgBackCheckBox.Text = "Когда возвращено";
    this.ToolTip.SetToolTip((Control) this.MsgBackCheckBox, "Сообщение будет отправлено в случае возврата (действие возвращено назад)");
    this.MsgBackCheckBox.CheckedChanged += new EventHandler(this.MsgCheckBox_CheckedChanged);
    this.MsgBackButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.MsgBackButton.Enabled = false;
    this.MsgBackButton.ImageAlign = ContentAlignment.MiddleLeft;
    this.MsgBackButton.ImageIndex = 14;
    this.MsgBackButton.ImageList = this.TabsIL;
    this.MsgBackButton.ImeMode = ImeMode.NoControl;
    this.MsgBackButton.Location = new Point(459, 0);
    this.MsgBackButton.Name = "MsgBackButton";
    this.MsgBackButton.Size = new Size(135, 29);
    this.MsgBackButton.TabIndex = 7;
    this.MsgBackButton.Tag = (object) "6";
    this.MsgBackButton.Text = "Сообщение...";
    this.MsgBackButton.TextAlign = ContentAlignment.MiddleRight;
    this.MsgBackButton.Click += new EventHandler(this.EditMessageClick);
    this.MsgPeriodPanel.Controls.Add((Control) this.MsgPeriodCheckBox);
    this.MsgPeriodPanel.Controls.Add((Control) this.MsgPeriodButton);
    this.MsgPeriodPanel.Dock = DockStyle.Top;
    this.MsgPeriodPanel.Location = new Point(3, 80 /*0x50*/);
    this.MsgPeriodPanel.Name = "MsgPeriodPanel";
    this.MsgPeriodPanel.Size = new Size(604, 29);
    this.MsgPeriodPanel.TabIndex = 8;
    this.MsgPeriodCheckBox.AutoSize = true;
    this.MsgPeriodCheckBox.ImeMode = ImeMode.NoControl;
    this.MsgPeriodCheckBox.Location = new Point(11, 5);
    this.MsgPeriodCheckBox.Name = "MsgPeriodCheckBox";
    this.MsgPeriodCheckBox.Size = new Size(123, 21);
    this.MsgPeriodCheckBox.TabIndex = 4;
    this.MsgPeriodCheckBox.Tag = (object) "";
    this.MsgPeriodCheckBox.Text = "Через период";
    this.MsgPeriodCheckBox.CheckedChanged += new EventHandler(this.MsgCheckBox_CheckedChanged);
    this.MsgPeriodButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.MsgPeriodButton.Enabled = false;
    this.MsgPeriodButton.ImageAlign = ContentAlignment.MiddleLeft;
    this.MsgPeriodButton.ImageIndex = 14;
    this.MsgPeriodButton.ImageList = this.TabsIL;
    this.MsgPeriodButton.ImeMode = ImeMode.NoControl;
    this.MsgPeriodButton.Location = new Point(459, 0);
    this.MsgPeriodButton.Name = "MsgPeriodButton";
    this.MsgPeriodButton.Size = new Size(135, 29);
    this.MsgPeriodButton.TabIndex = 5;
    this.MsgPeriodButton.Tag = (object) "2";
    this.MsgPeriodButton.Text = "Сообщение...";
    this.MsgPeriodButton.TextAlign = ContentAlignment.MiddleRight;
    this.MsgPeriodButton.Click += new EventHandler(this.EditMessageClick);
    this.MsgReadPanel.Controls.Add((Control) this.MsgReadCheckBox);
    this.MsgReadPanel.Controls.Add((Control) this.MsgReadButton);
    this.MsgReadPanel.Dock = DockStyle.Top;
    this.MsgReadPanel.Location = new Point(3, 51);
    this.MsgReadPanel.Name = "MsgReadPanel";
    this.MsgReadPanel.Size = new Size(604, 29);
    this.MsgReadPanel.TabIndex = 11;
    this.MsgReadCheckBox.AutoSize = true;
    this.MsgReadCheckBox.ImeMode = ImeMode.NoControl;
    this.MsgReadCheckBox.Location = new Point(11, 5);
    this.MsgReadCheckBox.Name = "MsgReadCheckBox";
    this.MsgReadCheckBox.Size = new Size(123, 21);
    this.MsgReadCheckBox.TabIndex = 2;
    this.MsgReadCheckBox.Tag = (object) "";
    this.MsgReadCheckBox.Text = "По прочтении";
    this.MsgReadCheckBox.CheckedChanged += new EventHandler(this.MsgCheckBox_CheckedChanged);
    this.MsgReadButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.MsgReadButton.Enabled = false;
    this.MsgReadButton.ImageAlign = ContentAlignment.MiddleLeft;
    this.MsgReadButton.ImageIndex = 14;
    this.MsgReadButton.ImageList = this.TabsIL;
    this.MsgReadButton.ImeMode = ImeMode.NoControl;
    this.MsgReadButton.Location = new Point(459, 0);
    this.MsgReadButton.Name = "MsgReadButton";
    this.MsgReadButton.Size = new Size(135, 29);
    this.MsgReadButton.TabIndex = 5;
    this.MsgReadButton.Tag = (object) "5";
    this.MsgReadButton.Text = "Сообщение...";
    this.MsgReadButton.TextAlign = ContentAlignment.MiddleRight;
    this.MsgReadButton.Click += new EventHandler(this.EditMessageClick);
    this.MsgStartPanel.Controls.Add((Control) this.MsgStartCheckBox);
    this.MsgStartPanel.Controls.Add((Control) this.MsgStartButton);
    this.MsgStartPanel.Dock = DockStyle.Top;
    this.MsgStartPanel.Location = new Point(3, 22);
    this.MsgStartPanel.Name = "MsgStartPanel";
    this.MsgStartPanel.Size = new Size(604, 29);
    this.MsgStartPanel.TabIndex = 6;
    this.MsgStartCheckBox.AutoSize = true;
    this.MsgStartCheckBox.ImeMode = ImeMode.NoControl;
    this.MsgStartCheckBox.Location = new Point(11, 5);
    this.MsgStartCheckBox.Name = "MsgStartCheckBox";
    this.MsgStartCheckBox.Size = new Size(105, 21);
    this.MsgStartCheckBox.TabIndex = 0;
    this.MsgStartCheckBox.Tag = (object) "";
    this.MsgStartCheckBox.Text = "При старте";
    this.MsgStartCheckBox.CheckedChanged += new EventHandler(this.MsgCheckBox_CheckedChanged);
    this.MsgStartButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.MsgStartButton.Enabled = false;
    this.MsgStartButton.ImageAlign = ContentAlignment.MiddleLeft;
    this.MsgStartButton.ImageIndex = 14;
    this.MsgStartButton.ImageList = this.TabsIL;
    this.MsgStartButton.ImeMode = ImeMode.NoControl;
    this.MsgStartButton.Location = new Point(459, 0);
    this.MsgStartButton.Name = "MsgStartButton";
    this.MsgStartButton.Size = new Size(135, 29);
    this.MsgStartButton.TabIndex = 1;
    this.MsgStartButton.Tag = (object) "1";
    this.MsgStartButton.Text = "Сообщение...";
    this.MsgStartButton.TextAlign = ContentAlignment.MiddleRight;
    this.MsgStartButton.Click += new EventHandler(this.EditMessageClick);
    this.ToolTip.AutoPopDelay = 3000;
    this.ToolTip.InitialDelay = 100;
    this.ToolTip.ReshowDelay = 100;
    this.AutoScaleDimensions = new SizeF(120f, 120f);
    this.AutoScaleMode = AutoScaleMode.Dpi;
    this.BackColor = SystemColors.ControlLightLight;
    this.Controls.Add((Control) this.MsgsGroupBox);
    this.Name = nameof (MessagesSettingPageControl);
    this.Size = new Size(610, 237);
    this.MsgsGroupBox.ResumeLayout(false);
    this.MsgAbortPanel.ResumeLayout(false);
    this.MsgAbortPanel.PerformLayout();
    this.MsgStopPanel.ResumeLayout(false);
    this.MsgStopPanel.PerformLayout();
    this.MsgBackPanel.ResumeLayout(false);
    this.MsgBackPanel.PerformLayout();
    this.MsgPeriodPanel.ResumeLayout(false);
    this.MsgPeriodPanel.PerformLayout();
    this.MsgReadPanel.ResumeLayout(false);
    this.MsgReadPanel.PerformLayout();
    this.MsgStartPanel.ResumeLayout(false);
    this.MsgStartPanel.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
