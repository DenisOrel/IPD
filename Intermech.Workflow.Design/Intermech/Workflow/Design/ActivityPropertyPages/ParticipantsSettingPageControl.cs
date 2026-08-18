// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ActivityPropertyPages.ParticipantsSettingPageControl
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

public class ParticipantsSettingPageControl : UserControl
{
  private bool _readOnly;
  private ActivitySettings _settings;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private UsersListView ParticipantsView;
  private Panel PartsPanel;
  private GroupBox PartKindGroupBox;
  private RadioButton AnyPartButton;
  private RadioButton AllPartsButton;
  private CheckBox sendWorkOfferLastParticipantCheckBox;
  private Button DelUserButton;
  private Button AddUserButton;
  private CheckBox DenyDelCheck;
  private CheckBox RequireAnswerCheck;
  private CheckBox sendParticipantsEmail;
  private ColumnHeader participantsCaption;

  public ParticipantsSettingPageControl()
  {
    this.InitializeComponent();
    this.ParticipantsView.AddButton = this.AddUserButton;
    this.ParticipantsView.DelButton = this.DelUserButton;
  }

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

  public bool LoadParticipantSettingControl(ActivitySettings settings, IDBObject activityObject)
  {
    this._settings = settings;
    bool flag1 = false;
    this.ParticipantsView.ProcessID = settings.ProcessID;
    IDBAttribute attributeById1 = activityObject.GetAttributeByID(wfConsts.AttrRecipID);
    if (attributeById1 != null && !attributeById1.IsNull)
    {
      settings.Participants = new ParticipantList(activityObject.Session);
      settings.Participants.AddParticipant(ParticipantKind.User, attributeById1.AsInteger);
    }
    else
    {
      IDBAttribute byId = activityObject.Attributes.FindByID(wfConsts.AttrParticipantsID);
      if (byId != null)
      {
        settings.Participants = new ParticipantList(activityObject.Session);
        string addData = byId.Value.ToString();
        if (settings.ActivityStatus == ActivityStatus.ParticipantWaiting)
        {
          addData = ParticipantList.ExtractAddData(addData);
          settings.Participants.XmlSection = "Expanded";
        }
        settings.Participants.AsString = addData;
        this.AnyPartButton.Checked = !settings.Participants.EveryOne;
        if (settings.ActivityType == wfConsts.StartTypeID)
        {
          this.ParticipantsView.Enabled = false;
          this.PartsPanel.Enabled = false;
          this.RequireAnswerCheck.Enabled = false;
        }
        bool flag2 = settings.ExtProperties.Ini.ReadBoolean("Props", "SendWorkOfferLastParticipant", false);
        if (this.AnyPartButton.Checked)
        {
          this.sendWorkOfferLastParticipantCheckBox.Enabled = true;
          this.sendWorkOfferLastParticipantCheckBox.Checked = flag2;
        }
        else
        {
          this.sendWorkOfferLastParticipantCheckBox.Enabled = false;
          this.sendWorkOfferLastParticipantCheckBox.Checked = false;
        }
      }
    }
    if (settings.Participants == null || settings.ActivityType == wfConsts.ScriptTypeID)
    {
      flag1 = true;
    }
    else
    {
      if (settings.ActivityType == wfConsts.RemoteSubProcessTypeID)
      {
        this.AnyPartButton.Checked = true;
        this.AllPartsButton.Visible = false;
        this.sendParticipantsEmail.Checked = true;
        this.sendParticipantsEmail.Visible = false;
      }
      else
        this.sendParticipantsEmail.Checked = settings.ExtProperties.Ini.ReadBoolean("Props", "sendParticipantsEmail", true);
      this.ParticipantsView.Participants = settings.Participants;
    }
    IDBAttribute attributeById2 = activityObject.GetAttributeByID(wfConsts.AttrAddIDID);
    settings.ActivityFlags = attributeById2 != null ? (ActivityFlags) attributeById2.AsInteger : (ActivityFlags) 0;
    this.DenyDelCheck.Checked = settings.ActivityFlags.HasFlag((Enum) ActivityFlags.DenyDeletionFromMail);
    this.RequireAnswerCheck.Checked = settings.ActivityFlags.HasFlag((Enum) ActivityFlags.RequireAnswerText);
    return flag1;
  }

  private void AllPartsButton_CheckedChanged(object sender, EventArgs e)
  {
    this.sendWorkOfferLastParticipantCheckBox.Enabled = false;
  }

  private void AnyPartButton_CheckedChanged(object sender, EventArgs e)
  {
    this.sendWorkOfferLastParticipantCheckBox.Enabled = true;
  }

  public void AddStarterVariable()
  {
    this.ParticipantsView.AddParticipant(new Participant(ParticipantKind.Variable, (long) wfConsts.SysVarStarterID));
  }

  public bool AnyPartChecked => this.AnyPartButton.Checked;

  public bool Save(IDBObject activityToSave, bool additionalParticipantsModified, bool modified)
  {
    bool flag = this.ParticipantsView.Modified;
    if (this._settings.Participants != null && this._settings.Participants.EveryOne != this.AllPartsButton.Checked)
      flag = true;
    if (additionalParticipantsModified)
      flag = true;
    if (flag && this._settings.Participants != null)
    {
      IDBAttribute byId = activityToSave.Attributes.FindByID(wfConsts.AttrParticipantsID);
      if (byId != null)
      {
        this._settings.Participants.EveryOne = this.AllPartsButton.Checked;
        byId.Value = (object) this._settings.Participants.AsString;
      }
      else
        activityToSave.Attributes.AddAttribute(wfConsts.AttrParticipantsID, false, new object[1]
        {
          (object) this._settings.Participants.AsString
        });
    }
    if (flag)
      modified = true;
    if (this._settings.Participants != null)
      this._settings.ExtProperties.Ini.WriteBoolean("Props", "sendParticipantsEmail", this.sendParticipantsEmail.Checked);
    if (this._settings.Participants != null)
      this._settings.ExtProperties.Ini.WriteBoolean("Props", "SendWorkOfferLastParticipant", this.sendWorkOfferLastParticipantCheckBox.Enabled && this.sendWorkOfferLastParticipantCheckBox.Checked);
    if (this.DenyDelCheck.Checked)
      this._settings.ActivityFlags |= ActivityFlags.DenyDeletionFromMail;
    if (this.RequireAnswerCheck.Checked)
      this._settings.ActivityFlags |= ActivityFlags.RequireAnswerText;
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
    this.ParticipantsView = new UsersListView();
    this.PartsPanel = new Panel();
    this.PartKindGroupBox = new GroupBox();
    this.AnyPartButton = new RadioButton();
    this.AllPartsButton = new RadioButton();
    this.sendWorkOfferLastParticipantCheckBox = new CheckBox();
    this.DelUserButton = new Button();
    this.AddUserButton = new Button();
    this.DenyDelCheck = new CheckBox();
    this.RequireAnswerCheck = new CheckBox();
    this.sendParticipantsEmail = new CheckBox();
    this.participantsCaption = new ColumnHeader();
    this.PartsPanel.SuspendLayout();
    this.PartKindGroupBox.SuspendLayout();
    this.SuspendLayout();
    this.ParticipantsView.AddButton = (Button) null;
    this.ParticipantsView.AllowManualSorting = true;
    this.ParticipantsView.Columns.AddRange(new ColumnHeader[1]
    {
      this.participantsCaption
    });
    this.ParticipantsView.DelButton = (Button) null;
    this.ParticipantsView.Dock = DockStyle.Fill;
    this.ParticipantsView.FullRowSelect = true;
    this.ParticipantsView.HideSelection = false;
    this.ParticipantsView.Location = new Point(0, 0);
    this.ParticipantsView.Name = "ParticipantsView";
    this.ParticipantsView.OwnerDraw = true;
    this.ParticipantsView.ProcessID = 0L;
    this.ParticipantsView.RadioGroups = false;
    this.ParticipantsView.ReadOnly = false;
    this.ParticipantsView.Size = new Size(770, 342);
    this.ParticipantsView.SortColumn = 0;
    this.ParticipantsView.Sorting = SortOrder.Ascending;
    this.ParticipantsView.SubitemImages = (ImageList) null;
    this.ParticipantsView.TabIndex = 14;
    this.ParticipantsView.UseCompatibleStateImageBehavior = false;
    this.ParticipantsView.View = View.Details;
    this.PartsPanel.Controls.Add((Control) this.PartKindGroupBox);
    this.PartsPanel.Controls.Add((Control) this.DelUserButton);
    this.PartsPanel.Controls.Add((Control) this.AddUserButton);
    this.PartsPanel.Dock = DockStyle.Bottom;
    this.PartsPanel.Location = new Point(0, 342);
    this.PartsPanel.Name = "PartsPanel";
    this.PartsPanel.Size = new Size(770, 95);
    this.PartsPanel.TabIndex = 15;
    this.PartKindGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.PartKindGroupBox.Controls.Add((Control) this.AnyPartButton);
    this.PartKindGroupBox.Controls.Add((Control) this.AllPartsButton);
    this.PartKindGroupBox.Controls.Add((Control) this.sendWorkOfferLastParticipantCheckBox);
    this.PartKindGroupBox.Location = new Point(0, 8);
    this.PartKindGroupBox.Name = "PartKindGroupBox";
    this.PartKindGroupBox.Size = new Size(674, 75);
    this.PartKindGroupBox.TabIndex = 11;
    this.PartKindGroupBox.TabStop = false;
    this.PartKindGroupBox.Text = "На данном шаге участвуют:";
    this.AnyPartButton.AutoSize = true;
    this.AnyPartButton.ImeMode = ImeMode.NoControl;
    this.AnyPartButton.Location = new Point(10, 46);
    this.AnyPartButton.Name = "AnyPartButton";
    this.AnyPartButton.Size = new Size(236, 21);
    this.AnyPartButton.TabIndex = 8;
    this.AnyPartButton.Text = "Любой пользователь из списка";
    this.AnyPartButton.CheckedChanged += new EventHandler(this.AnyPartButton_CheckedChanged);
    this.AllPartsButton.AutoSize = true;
    this.AllPartsButton.Checked = true;
    this.AllPartsButton.ImeMode = ImeMode.NoControl;
    this.AllPartsButton.Location = new Point(10, 24);
    this.AllPartsButton.Name = "AllPartsButton";
    this.AllPartsButton.Size = new Size(258, 21);
    this.AllPartsButton.TabIndex = 7;
    this.AllPartsButton.TabStop = true;
    this.AllPartsButton.Text = "Все перечисленные пользователи";
    this.AllPartsButton.CheckedChanged += new EventHandler(this.AllPartsButton_CheckedChanged);
    this.sendWorkOfferLastParticipantCheckBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.sendWorkOfferLastParticipantCheckBox.AutoSize = true;
    this.sendWorkOfferLastParticipantCheckBox.Enabled = false;
    this.sendWorkOfferLastParticipantCheckBox.ImeMode = ImeMode.NoControl;
    this.sendWorkOfferLastParticipantCheckBox.Location = new Point(291, 47);
    this.sendWorkOfferLastParticipantCheckBox.Name = "sendWorkOfferLastParticipantCheckBox";
    this.sendWorkOfferLastParticipantCheckBox.Size = new Size(376, 21);
    this.sendWorkOfferLastParticipantCheckBox.TabIndex = 9;
    this.sendWorkOfferLastParticipantCheckBox.Text = "Назначать исполнителем последнего пользователя";
    this.DelUserButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.DelUserButton.BackColor = SystemColors.Control;
    this.DelUserButton.ImeMode = ImeMode.NoControl;
    this.DelUserButton.Location = new Point(680, 52);
    this.DelUserButton.Name = "DelUserButton";
    this.DelUserButton.Size = new Size(90, 26);
    this.DelUserButton.TabIndex = 10;
    this.DelUserButton.Text = "&Удалить";
    this.DelUserButton.UseVisualStyleBackColor = true;
    this.AddUserButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.AddUserButton.BackColor = SystemColors.Control;
    this.AddUserButton.ImeMode = ImeMode.NoControl;
    this.AddUserButton.Location = new Point(680, 18);
    this.AddUserButton.Name = "AddUserButton";
    this.AddUserButton.Size = new Size(90, 27);
    this.AddUserButton.TabIndex = 9;
    this.AddUserButton.Text = "&Добавить";
    this.AddUserButton.UseVisualStyleBackColor = true;
    this.DenyDelCheck.AutoSize = true;
    this.DenyDelCheck.Dock = DockStyle.Bottom;
    this.DenyDelCheck.ImeMode = ImeMode.NoControl;
    this.DenyDelCheck.Location = new Point(0, 437);
    this.DenyDelCheck.Name = "DenyDelCheck";
    this.DenyDelCheck.Size = new Size(770, 21);
    this.DenyDelCheck.TabIndex = 13;
    this.DenyDelCheck.Text = "Запретить удаление действия из почты получателями";
    this.RequireAnswerCheck.AutoSize = true;
    this.RequireAnswerCheck.Dock = DockStyle.Bottom;
    this.RequireAnswerCheck.ImeMode = ImeMode.NoControl;
    this.RequireAnswerCheck.Location = new Point(0, 458);
    this.RequireAnswerCheck.Name = "RequireAnswerCheck";
    this.RequireAnswerCheck.Size = new Size(770, 21);
    this.RequireAnswerCheck.TabIndex = 16 /*0x10*/;
    this.RequireAnswerCheck.Text = "Требовать заполнение ответа при отправке назад";
    this.sendParticipantsEmail.AutoSize = true;
    this.sendParticipantsEmail.Checked = true;
    this.sendParticipantsEmail.CheckState = CheckState.Checked;
    this.sendParticipantsEmail.Dock = DockStyle.Bottom;
    this.sendParticipantsEmail.ImeMode = ImeMode.NoControl;
    this.sendParticipantsEmail.Location = new Point(0, 479);
    this.sendParticipantsEmail.Name = "sendParticipantsEmail";
    this.sendParticipantsEmail.Size = new Size(770, 21);
    this.sendParticipantsEmail.TabIndex = 17;
    this.sendParticipantsEmail.Text = "Выполнять рассылку на внешнюю почту";
    this.participantsCaption.Text = "Исполнитель";
    this.participantsCaption.Width = 309;
    this.AutoScaleDimensions = new SizeF(120f, 120f);
    this.AutoScaleMode = AutoScaleMode.Dpi;
    this.BackColor = SystemColors.ControlLightLight;
    this.Controls.Add((Control) this.ParticipantsView);
    this.Controls.Add((Control) this.PartsPanel);
    this.Controls.Add((Control) this.DenyDelCheck);
    this.Controls.Add((Control) this.RequireAnswerCheck);
    this.Controls.Add((Control) this.sendParticipantsEmail);
    this.MinimumSize = new Size(770, 500);
    this.Name = nameof (ParticipantsSettingPageControl);
    this.Size = new Size(770, 500);
    this.PartsPanel.ResumeLayout(false);
    this.PartKindGroupBox.ResumeLayout(false);
    this.PartKindGroupBox.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
