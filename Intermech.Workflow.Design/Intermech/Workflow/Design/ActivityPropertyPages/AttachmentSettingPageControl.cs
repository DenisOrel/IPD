// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ActivityPropertyPages.AttachmentSettingPageControl
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design.ActivityPropertyPages;

public class AttachmentSettingPageControl : UserControl
{
  private AttachmentsView _attView;
  private bool _readOnly;
  private ActivitySettings _settings;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel AttachsPanel;
  private GroupBox TempRightsGroupBox;
  private CheckBox TempRightsGroupingCheckBox;
  private CheckBox TempRightsAdminCheckBox;
  private CheckBox TempRightsEditCheckBox;
  private CheckBox TempRightsViewCheckBox;
  private RadioButton NoTempRightsButton;
  private Panel ContentOptionsPanel;
  private CheckBox AllowAddAttachsCheckBox;
  private CheckBox AllowDelAttachsCheckBox;
  private CheckBox allowAdminAttachCheckBox;
  private CheckBox allowSystemAttachCheckBox;

  public AttachmentSettingPageControl() => this.InitializeComponent();

  public bool ReadOnly
  {
    get => this._readOnly;
    set
    {
      this._readOnly = value;
      if (!this._readOnly)
        return;
      ControlFuncs.SetControlsReadOnly((Control) this, value);
    }
  }

  public bool LoadAttachmentSettingControl(
    ActivitySettings settings,
    IDBObject activityObject,
    bool participantVisible)
  {
    this._settings = settings;
    bool flag1 = false;
    if (settings.ActivityType == wfConsts.SchemesTypeID || settings.ActivityType == wfConsts.ProcessesTypeID)
    {
      flag1 = true;
    }
    else
    {
      this.ContentOptionsPanel.Visible = participantVisible;
      if (participantVisible)
      {
        this.AllowAddAttachsCheckBox.Checked = !settings.ActivityFlags.HasFlag((Enum) ActivityFlags.DenyAttach);
        this.AllowDelAttachsCheckBox.Checked = !settings.ActivityFlags.HasFlag((Enum) ActivityFlags.DenyDetach);
        this.allowSystemAttachCheckBox.Checked = settings.ActivityFlags.HasFlag((Enum) ActivityFlags.AllowSystemAttach);
        this.allowAdminAttachCheckBox.Checked = settings.ActivityFlags.HasFlag((Enum) ActivityFlags.AllowAdminAttach);
      }
      bool flag2 = settings.Participants != null;
      this.TempRightsGroupBox.Visible = flag2;
      if (flag2)
      {
        IDBAttribute attributeById = activityObject.GetAttributeByID(wfConsts.AttrTempRightsID);
        TemporaryRights temporaryRights = attributeById != null ? (TemporaryRights) attributeById.AsInteger : TemporaryRights.None;
        this.NoTempRightsButton.Checked = temporaryRights == TemporaryRights.None;
        this.TempRightsViewCheckBox.Checked = (temporaryRights & TemporaryRights.View) != 0;
        this.TempRightsEditCheckBox.Checked = (temporaryRights & TemporaryRights.Edit) != 0;
        this.TempRightsAdminCheckBox.Checked = (temporaryRights & TemporaryRights.Admin) != 0;
        this.TempRightsGroupingCheckBox.Checked = (temporaryRights & TemporaryRights.HandleGrouped) != 0;
        this.TempRightsGroupingCheckBox.Enabled = temporaryRights != 0;
      }
    }
    return flag1;
  }

  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    this.InitAttachments();
  }

  private void InitAttachments()
  {
    if (this._attView != null || this._settings == null)
      return;
    AttachmentsView attachmentsView = new AttachmentsView();
    attachmentsView.BackColor = SystemColors.Window;
    attachmentsView.Dock = DockStyle.Fill;
    this._attView = attachmentsView;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this._settings.ActivityObjectID);
      if (dbObject != null)
        this._attView.Load(dbObject);
    }
    this._attView.ReadOnly = this.ReadOnly;
    this.AttachsPanel.Controls.Add((Control) this._attView);
    this._attView.BringToFront();
  }

  private void TempRightsViewCheckBox_Click(object sender, EventArgs e)
  {
    bool flag = !this.TempRightsViewCheckBox.Checked && !this.TempRightsEditCheckBox.Checked && !this.TempRightsAdminCheckBox.Checked;
    if (sender is RadioButton)
      flag = this.NoTempRightsButton.Checked;
    else
      this.NoTempRightsButton.Checked = flag;
    if (flag)
    {
      this.TempRightsViewCheckBox.Checked = false;
      this.TempRightsEditCheckBox.Checked = false;
      this.TempRightsAdminCheckBox.Checked = false;
      this.TempRightsGroupingCheckBox.Checked = false;
    }
    else
    {
      if (this.TempRightsAdminCheckBox.Checked)
        this.TempRightsEditCheckBox.Checked = true;
      this.TempRightsEditCheckBox.Enabled = !this.TempRightsAdminCheckBox.Checked;
      if (this.TempRightsEditCheckBox.Checked)
        this.TempRightsViewCheckBox.Checked = true;
      this.TempRightsViewCheckBox.Enabled = !this.TempRightsEditCheckBox.Checked;
    }
    this.TempRightsGroupingCheckBox.Enabled = !flag;
  }

  public bool Save(
    IDBObject activityToSave,
    bool modified,
    bool attachmentVisible,
    bool participantVisible)
  {
    if (this._attView != null && this._attView.Modified)
    {
      this._attView.Save(activityToSave);
      modified = true;
    }
    if (attachmentVisible & participantVisible)
    {
      if (!this.AllowAddAttachsCheckBox.Checked)
        this._settings.ActivityFlags |= ActivityFlags.DenyAttach;
      if (!this.AllowDelAttachsCheckBox.Checked)
        this._settings.ActivityFlags |= ActivityFlags.DenyDetach;
      if (this.allowAdminAttachCheckBox.Checked)
        this._settings.ActivityFlags |= ActivityFlags.AllowAdminAttach;
      if (this.allowSystemAttachCheckBox.Checked)
        this._settings.ActivityFlags |= ActivityFlags.AllowSystemAttach;
    }
    if (attachmentVisible && this._settings.Participants != null)
    {
      IDBAttribute attributeById = activityToSave.GetAttributeByID(wfConsts.AttrTempRightsID);
      TemporaryRights temporaryRights1 = attributeById != null ? (TemporaryRights) attributeById.AsInteger : TemporaryRights.None;
      TemporaryRights temporaryRights2 = TemporaryRights.None;
      if (this.TempRightsViewCheckBox.Checked)
        temporaryRights2 |= TemporaryRights.View;
      if (this.TempRightsEditCheckBox.Checked)
        temporaryRights2 |= TemporaryRights.Edit;
      if (this.TempRightsAdminCheckBox.Checked)
        temporaryRights2 |= TemporaryRights.Admin;
      if (this.TempRightsGroupingCheckBox.Checked)
        temporaryRights2 |= TemporaryRights.HandleGrouped;
      if (temporaryRights2 != temporaryRights1)
      {
        if (attributeById == null)
          activityToSave.Attributes.AddAttribute(wfConsts.AttrTempRightsID, false, new object[1]
          {
            (object) (long) temporaryRights2
          });
        else
          attributeById.AsInteger = (long) temporaryRights2;
        modified = true;
      }
    }
    return modified;
  }

  private void allowAdminAttachCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    if (this.allowAdminAttachCheckBox.Checked)
    {
      this.allowSystemAttachCheckBox.Checked = true;
      this.allowSystemAttachCheckBox.Enabled = false;
    }
    else
      this.allowSystemAttachCheckBox.Enabled = true;
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
    this.AttachsPanel = new Panel();
    this.ContentOptionsPanel = new Panel();
    this.AllowAddAttachsCheckBox = new CheckBox();
    this.AllowDelAttachsCheckBox = new CheckBox();
    this.allowAdminAttachCheckBox = new CheckBox();
    this.allowSystemAttachCheckBox = new CheckBox();
    this.TempRightsGroupBox = new GroupBox();
    this.TempRightsGroupingCheckBox = new CheckBox();
    this.TempRightsAdminCheckBox = new CheckBox();
    this.TempRightsEditCheckBox = new CheckBox();
    this.TempRightsViewCheckBox = new CheckBox();
    this.NoTempRightsButton = new RadioButton();
    this.AttachsPanel.SuspendLayout();
    this.ContentOptionsPanel.SuspendLayout();
    this.TempRightsGroupBox.SuspendLayout();
    this.SuspendLayout();
    this.AttachsPanel.Controls.Add((Control) this.ContentOptionsPanel);
    this.AttachsPanel.Dock = DockStyle.Fill;
    this.AttachsPanel.Location = new Point(0, 0);
    this.AttachsPanel.Name = "AttachsPanel";
    this.AttachsPanel.Size = new Size(651, 354);
    this.AttachsPanel.TabIndex = 4;
    this.ContentOptionsPanel.AutoSize = true;
    this.ContentOptionsPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.ContentOptionsPanel.Controls.Add((Control) this.AllowAddAttachsCheckBox);
    this.ContentOptionsPanel.Controls.Add((Control) this.AllowDelAttachsCheckBox);
    this.ContentOptionsPanel.Controls.Add((Control) this.allowAdminAttachCheckBox);
    this.ContentOptionsPanel.Controls.Add((Control) this.allowSystemAttachCheckBox);
    this.ContentOptionsPanel.Dock = DockStyle.Bottom;
    this.ContentOptionsPanel.Location = new Point(0, 260);
    this.ContentOptionsPanel.Name = "ContentOptionsPanel";
    this.ContentOptionsPanel.Padding = new Padding(5);
    this.ContentOptionsPanel.Size = new Size(651, 94);
    this.ContentOptionsPanel.TabIndex = 1;
    this.AllowAddAttachsCheckBox.AutoSize = true;
    this.AllowAddAttachsCheckBox.Dock = DockStyle.Bottom;
    this.AllowAddAttachsCheckBox.ImeMode = ImeMode.NoControl;
    this.AllowAddAttachsCheckBox.Location = new Point(5, 5);
    this.AllowAddAttachsCheckBox.Name = "AllowAddAttachsCheckBox";
    this.AllowAddAttachsCheckBox.Size = new Size(641, 21);
    this.AllowAddAttachsCheckBox.TabIndex = 0;
    this.AllowAddAttachsCheckBox.Text = "Разрешить получателю прикреплять вложения";
    this.AllowDelAttachsCheckBox.AutoSize = true;
    this.AllowDelAttachsCheckBox.Dock = DockStyle.Bottom;
    this.AllowDelAttachsCheckBox.ImeMode = ImeMode.NoControl;
    this.AllowDelAttachsCheckBox.Location = new Point(5, 26);
    this.AllowDelAttachsCheckBox.Name = "AllowDelAttachsCheckBox";
    this.AllowDelAttachsCheckBox.Size = new Size(641, 21);
    this.AllowDelAttachsCheckBox.TabIndex = 1;
    this.AllowDelAttachsCheckBox.Text = "Разрешить получателю откреплять вложения";
    this.allowAdminAttachCheckBox.AutoSize = true;
    this.allowAdminAttachCheckBox.Dock = DockStyle.Bottom;
    this.allowAdminAttachCheckBox.ImeMode = ImeMode.NoControl;
    this.allowAdminAttachCheckBox.Location = new Point(5, 47);
    this.allowAdminAttachCheckBox.Name = "allowAdminAttachCheckBox";
    this.allowAdminAttachCheckBox.Size = new Size(641, 21);
    this.allowAdminAttachCheckBox.TabIndex = 3;
    this.allowAdminAttachCheckBox.Text = "Не контролировать для административной сессии";
    this.allowAdminAttachCheckBox.CheckedChanged += new EventHandler(this.allowAdminAttachCheckBox_CheckedChanged);
    this.allowSystemAttachCheckBox.AutoSize = true;
    this.allowSystemAttachCheckBox.Dock = DockStyle.Bottom;
    this.allowSystemAttachCheckBox.ImeMode = ImeMode.NoControl;
    this.allowSystemAttachCheckBox.Location = new Point(5, 68);
    this.allowSystemAttachCheckBox.Name = "allowSystemAttachCheckBox";
    this.allowSystemAttachCheckBox.Size = new Size(641, 21);
    this.allowSystemAttachCheckBox.TabIndex = 2;
    this.allowSystemAttachCheckBox.Text = "Не контролировать для системной сессии";
    this.TempRightsGroupBox.Controls.Add((Control) this.TempRightsGroupingCheckBox);
    this.TempRightsGroupBox.Controls.Add((Control) this.TempRightsAdminCheckBox);
    this.TempRightsGroupBox.Controls.Add((Control) this.TempRightsEditCheckBox);
    this.TempRightsGroupBox.Controls.Add((Control) this.TempRightsViewCheckBox);
    this.TempRightsGroupBox.Controls.Add((Control) this.NoTempRightsButton);
    this.TempRightsGroupBox.Dock = DockStyle.Bottom;
    this.TempRightsGroupBox.Location = new Point(0, 354);
    this.TempRightsGroupBox.Name = "TempRightsGroupBox";
    this.TempRightsGroupBox.Size = new Size(651, 134);
    this.TempRightsGroupBox.TabIndex = 6;
    this.TempRightsGroupBox.TabStop = false;
    this.TempRightsGroupBox.Text = "Временные права доступа";
    this.TempRightsGroupingCheckBox.AutoSize = true;
    this.TempRightsGroupingCheckBox.ImeMode = ImeMode.NoControl;
    this.TempRightsGroupingCheckBox.Location = new Point(17, 103);
    this.TempRightsGroupingCheckBox.Name = "TempRightsGroupingCheckBox";
    this.TempRightsGroupingCheckBox.Size = new Size(438, 21);
    this.TempRightsGroupingCheckBox.TabIndex = 4;
    this.TempRightsGroupingCheckBox.Text = "Назначать также на объекты внутри группирующих объектов";
    this.TempRightsGroupingCheckBox.UseVisualStyleBackColor = true;
    this.TempRightsAdminCheckBox.AutoSize = true;
    this.TempRightsAdminCheckBox.ImeMode = ImeMode.NoControl;
    this.TempRightsAdminCheckBox.Location = new Point(274, 75);
    this.TempRightsAdminCheckBox.Name = "TempRightsAdminCheckBox";
    this.TempRightsAdminCheckBox.Size = new Size(165, 21);
    this.TempRightsAdminCheckBox.TabIndex = 3;
    this.TempRightsAdminCheckBox.Text = "Администрирование";
    this.TempRightsAdminCheckBox.UseVisualStyleBackColor = true;
    this.TempRightsAdminCheckBox.Click += new EventHandler(this.TempRightsViewCheckBox_Click);
    this.TempRightsEditCheckBox.AutoSize = true;
    this.TempRightsEditCheckBox.ImeMode = ImeMode.NoControl;
    this.TempRightsEditCheckBox.Location = new Point(274, 48 /*0x30*/);
    this.TempRightsEditCheckBox.Name = "TempRightsEditCheckBox";
    this.TempRightsEditCheckBox.Size = new Size(140, 21);
    this.TempRightsEditCheckBox.TabIndex = 2;
    this.TempRightsEditCheckBox.Text = "Редактирование";
    this.TempRightsEditCheckBox.UseVisualStyleBackColor = true;
    this.TempRightsEditCheckBox.Click += new EventHandler(this.TempRightsViewCheckBox_Click);
    this.TempRightsViewCheckBox.AutoSize = true;
    this.TempRightsViewCheckBox.ImeMode = ImeMode.NoControl;
    this.TempRightsViewCheckBox.Location = new Point(274, 22);
    this.TempRightsViewCheckBox.Name = "TempRightsViewCheckBox";
    this.TempRightsViewCheckBox.Size = new Size(95, 21);
    this.TempRightsViewCheckBox.TabIndex = 1;
    this.TempRightsViewCheckBox.Text = "Просмотр";
    this.TempRightsViewCheckBox.UseVisualStyleBackColor = true;
    this.TempRightsViewCheckBox.Click += new EventHandler(this.TempRightsViewCheckBox_Click);
    this.NoTempRightsButton.AutoSize = true;
    this.NoTempRightsButton.ImeMode = ImeMode.NoControl;
    this.NoTempRightsButton.Location = new Point(17, 47);
    this.NoTempRightsButton.Name = "NoTempRightsButton";
    this.NoTempRightsButton.Size = new Size(120, 21);
    this.NoTempRightsButton.TabIndex = 0;
    this.NoTempRightsButton.TabStop = true;
    this.NoTempRightsButton.Text = "Не назначать";
    this.NoTempRightsButton.UseVisualStyleBackColor = true;
    this.NoTempRightsButton.Click += new EventHandler(this.TempRightsViewCheckBox_Click);
    this.AutoScaleDimensions = new SizeF(120f, 120f);
    this.AutoScaleMode = AutoScaleMode.Dpi;
    this.BackColor = SystemColors.ControlLightLight;
    this.Controls.Add((Control) this.AttachsPanel);
    this.Controls.Add((Control) this.TempRightsGroupBox);
    this.Name = nameof (AttachmentSettingPageControl);
    this.Size = new Size(651, 488);
    this.AttachsPanel.ResumeLayout(false);
    this.AttachsPanel.PerformLayout();
    this.ContentOptionsPanel.ResumeLayout(false);
    this.ContentOptionsPanel.PerformLayout();
    this.TempRightsGroupBox.ResumeLayout(false);
    this.TempRightsGroupBox.PerformLayout();
    this.ResumeLayout(false);
  }
}
