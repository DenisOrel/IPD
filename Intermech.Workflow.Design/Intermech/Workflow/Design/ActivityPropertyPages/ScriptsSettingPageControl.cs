// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ActivityPropertyPages.ScriptsSettingPageControl
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using ImSSP;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Workflow.Design.ScriptPad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design.ActivityPropertyPages;

public class ScriptsSettingPageControl : UserControl
{
  private ActivitySettings _settings;
  private bool _readOnly;
  private bool _checkLocalScriptNotDelete = true;
  private bool _checkLocalScriptNotCreate;
  public List<long> LocalScriptsToDeleted = new List<long>();
  public Dictionary<int, long> NewScripts = new Dictionary<int, long>();
  public bool AddedNewScriptToDelete;
  private bool _loading;
  private bool _additionalParticipantsModified;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private GroupBox AfterScriptGroupBox;
  private ButtonEdit afterScriptEdit;
  private System.Windows.Forms.ComboBox AfterScriptExecCombo;
  private System.Windows.Forms.ComboBox afterScriptType;
  private Label label6;
  private Label label17;
  private CheckBox AfterScriptCheckBox;
  private Panel panel13;
  private GroupBox BeforeScriptGroupBox;
  private ButtonEdit beforeScriptEdit;
  private System.Windows.Forms.ComboBox BeforeScriptExecCombo;
  private System.Windows.Forms.ComboBox beforeScriptType;
  private Label label4;
  private Label label16;
  private CheckBox BeforeScriptCheckBox;
  private GroupBox ScriptGroupBox;
  private ButtonEdit scriptEdit;
  private GroupBox ScriptUserGroupBox;
  private ButtonEdit ScriptUserEdit;
  private System.Windows.Forms.ComboBox scriptType;
  private System.Windows.Forms.ComboBox ScriptExecCombo;
  private Label label14;
  private Label label10;

  public ScriptsSettingPageControl() => this.InitializeComponent();

  public bool ReadOnly
  {
    get => this._readOnly;
    set
    {
      this._readOnly = value;
      if (!value)
        return;
      ControlFuncs.SetControlsReadOnly((Control) this, (value ? 1 : 0) != 0, new List<Control>((IEnumerable<Control>) new Control[3]
      {
        (Control) this.beforeScriptEdit,
        (Control) this.afterScriptEdit,
        (Control) this.scriptEdit
      }));
    }
  }

  public bool LoadScriptsSettingPageControl(
    ActivitySettings settings,
    IDBObject activityObject,
    bool participantsVisible,
    IUserSession activitySession)
  {
    try
    {
      this._loading = true;
      bool flag = false;
      this._settings = settings;
      if (settings.ActivityType == wfConsts.SchemesTypeID || settings.ActivityType == wfConsts.ProcessesTypeID)
      {
        flag = true;
      }
      else
      {
        if (!participantsVisible && settings.ActivityType != wfConsts.StartTypeID)
        {
          this.BeforeScriptExecCombo.Tag = (object) 1;
          this.BeforeScriptExecCombo.SelectedIndex = 0;
          this.AfterScriptExecCombo.Tag = (object) 1;
          this.AfterScriptExecCombo.SelectedIndex = 0;
        }
        IDBRelationCollection relationCollection = activitySession.GetRelationCollection(wfConsts.ScriptRelationTypeID);
        relationCollection.LocalTypesMode = true;
        DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[6]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID,
          (object) wfConsts.AttrScriptKindID,
          (object) ObligatoryObjectAttributes.F_PRJLINK_ID,
          (object) wfConsts.AttrScriptExecSideID,
          (object) ObligatoryObjectAttributes.F_OBJECT_TYPE,
          (object) ObligatoryObjectAttributes.CAPTION
        });
        foreach (DataRow row in (InternalDataCollectionBase) relationCollection.ConsistFrom(paramSet, settings.ActivityObjectID).Rows)
        {
          int index = 0;
          if (!row[1].Equals((object) DBNull.Value))
            index = Convert.ToInt32(row[1]);
          settings.ScriptInfos[index].ScriptID = Convert.ToInt64(row[0]);
          settings.ScriptInfos[index].OldScriptID = settings.ScriptInfos[index].ScriptID;
          settings.ScriptInfos[index].ScriptLinkID = Convert.ToInt64(row[2]);
          if (!row[3].Equals((object) DBNull.Value))
            settings.ScriptInfos[index].ExecSide = (ScriptExecSide) Convert.ToInt64(row[3]);
          settings.ScriptInfos[index].ScriptType = Convert.ToInt32(row[4]) == wfConsts.WorkflowCommonScript ? WorkflowScriptType.Common : WorkflowScriptType.Local;
          settings.ScriptInfos[index].ScriptCaption = row[5].ToString();
        }
        if (settings.ActivityType == wfConsts.ScriptTypeID)
        {
          this.BeforeScriptGroupBox.Visible = false;
          this.AfterScriptGroupBox.Visible = false;
          this.ScriptGroupBox.Visible = true;
          this.ScriptExecCombo.SelectedIndex = (int) settings.ScriptInfos[0].ExecSide;
          this.scriptType.SelectedIndex = (int) settings.ScriptInfos[0].ScriptType;
          if (settings.Participants == null)
            settings.Participants = new ParticipantList(activitySession);
          this.ScriptUserEdit.Text = settings.Participants.ToUserString();
          this.scriptEdit.Text = settings.ScriptInfos[0].ScriptCaption;
          this.ScriptExecCombo.Enabled = true;
          this.scriptType.Enabled = settings.ScriptInfos[0].ScriptID == 0L;
        }
        else if (settings.ActivityType == wfConsts.StartTypeID || settings.ActivityType == wfConsts.TaskTypeID || settings.ActivityType == wfConsts.ApproveTypeID)
        {
          this.BeforeScriptCheckBox.Checked = settings.ScriptInfos[0].ScriptID != 0L;
          this.BeforeScriptExecCombo.SelectedIndex = (int) settings.ScriptInfos[0].ExecSide;
          this.beforeScriptType.SelectedIndex = (int) settings.ScriptInfos[0].ScriptType;
          this.beforeScriptEdit.Text = settings.ScriptInfos[0].ScriptCaption;
          this.BeforeScriptExecCombo.Enabled = true;
          this.beforeScriptType.Enabled = settings.ScriptInfos[0].ScriptID == 0L;
          this.BeforeScriptExecCombo.Tag = (object) 0;
          this.AfterScriptCheckBox.Checked = settings.ScriptInfos[1].ScriptID != 0L;
          this.AfterScriptExecCombo.SelectedIndex = (int) settings.ScriptInfos[1].ExecSide;
          this.afterScriptType.SelectedIndex = (int) settings.ScriptInfos[1].ScriptType;
          this.afterScriptEdit.Text = settings.ScriptInfos[1].ScriptCaption;
          this.AfterScriptExecCombo.Enabled = true;
          this.afterScriptType.Enabled = settings.ScriptInfos[1].ScriptID == 0L;
          this.AfterScriptExecCombo.Tag = (object) 0;
          this.BeforeScriptCheckBox_CheckedChanged((object) null, (EventArgs) null);
        }
        else
        {
          this.BeforeScriptExecCombo.Tag = (object) 1;
          this.AfterScriptExecCombo.Tag = (object) 1;
          this.BeforeScriptCheckBox.Checked = settings.ScriptInfos[0].ScriptID != 0L;
          this.BeforeScriptExecCombo.SelectedIndex = 0;
          this.beforeScriptType.SelectedIndex = (int) settings.ScriptInfos[0].ScriptType;
          this.beforeScriptEdit.Text = settings.ScriptInfos[0].ScriptCaption;
          this.BeforeScriptExecCombo.Enabled = false;
          this.beforeScriptType.Enabled = settings.ScriptInfos[0].ScriptID == 0L;
          this.AfterScriptCheckBox.Checked = settings.ScriptInfos[1].ScriptID != 0L;
          this.AfterScriptExecCombo.SelectedIndex = 0;
          this.afterScriptType.SelectedIndex = (int) settings.ScriptInfos[1].ScriptType;
          this.afterScriptEdit.Text = settings.ScriptInfos[1].ScriptCaption;
          this.AfterScriptExecCombo.Enabled = false;
          this.afterScriptType.Enabled = settings.ScriptInfos[1].ScriptID == 0L;
          this.BeforeScriptCheckBox_CheckedChanged((object) null, (EventArgs) null);
        }
      }
      return flag;
    }
    finally
    {
      this._loading = false;
    }
  }

  private void EditScriptButton_Click(object sender, ButtonPressedEventArgs e)
  {
    int int32 = Convert.ToInt32((sender as Control).Tag);
    ScriptInfo scriptInfo = this._settings.ScriptInfos[int32];
    string workflowLocalName = string.Empty;
    string str1 = string.Empty;
    if (this._settings.ProcessID != 0L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        ObjectSystemPropertiesEx systemPropertiesEx = sessionKeeper.Session.GetObjectSystemPropertiesEx(this._settings.ProcessID, false);
        if (systemPropertiesEx != null)
          str1 = CaptionTransform.GetCaption(systemPropertiesEx.Caption, (long) systemPropertiesEx.VersionID) + ".";
      }
    }
    string str2 = string.Empty;
    WorkflowScriptType workflowScriptType;
    ScriptExecSide workflowExecSide;
    if (this._settings.ActivityType == wfConsts.ScriptTypeID)
    {
      workflowScriptType = (WorkflowScriptType) this.scriptType.SelectedIndex;
      workflowExecSide = (ScriptExecSide) this.ScriptExecCombo.SelectedIndex;
    }
    else
    {
      workflowScriptType = sender == this.beforeScriptEdit ? (WorkflowScriptType) this.beforeScriptType.SelectedIndex : (WorkflowScriptType) this.afterScriptType.SelectedIndex;
      workflowExecSide = sender == this.beforeScriptEdit ? (ScriptExecSide) this.BeforeScriptExecCombo.SelectedIndex : (ScriptExecSide) this.AfterScriptExecCombo.SelectedIndex;
      str2 = int32 == 0 ? "[Перед] " : "[После] ";
    }
    ScriptTypes scriptType = workflowScriptType == WorkflowScriptType.Common ? ScriptTypes.WorkflowCommon : ScriptTypes.WorkflowLocal;
    if (scriptType == ScriptTypes.WorkflowLocal)
      workflowLocalName = $"{str2}{str1} {this._settings.ActivityName}";
    long objectID = Math.Abs(new WorkflowScriptPadHelper(scriptType, this.ParentForm).EditScript(scriptInfo.ScriptID, workflowLocalName, workflowExecSide, !Holder.IsAdmin && this.ReadOnly, this._settings.ActivityObjectID));
    if (objectID != 0L && objectID != scriptInfo.ScriptID)
    {
      scriptInfo.ScriptID = objectID;
      scriptInfo.ExecSide = workflowExecSide;
      scriptInfo.ScriptType = workflowScriptType;
      ButtonEdit buttonEdit = sender as ButtonEdit;
      if (scriptType == ScriptTypes.WorkflowLocal)
      {
        buttonEdit.Text = workflowLocalName;
        scriptInfo.ScriptCaption = workflowLocalName;
        if (this.NewScripts.ContainsKey(int32))
          this.NewScripts[int32] = objectID;
        else
          this.NewScripts.Add(int32, objectID);
      }
      else
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(objectID);
          if (!objectInfo.Empty)
          {
            buttonEdit.Text = objectInfo.Caption;
            scriptInfo.ScriptCaption = objectInfo.Caption;
          }
        }
      }
    }
    if (sender == this.scriptEdit)
      this.scriptType.Enabled = scriptInfo.ScriptID == 0L;
    else if (sender == this.beforeScriptEdit)
    {
      this.beforeScriptType.Enabled = scriptInfo.ScriptID == 0L;
    }
    else
    {
      if (sender != this.afterScriptEdit)
        return;
      this.afterScriptType.Enabled = scriptInfo.ScriptID == 0L;
    }
  }

  private bool SaveScriptInfo(
    IDBObject activity,
    ScriptInfo si,
    ScriptKind sk,
    CheckBox checkBox,
    System.Windows.Forms.ComboBox execCombo,
    bool modified)
  {
    if (si.OldScriptID != si.ScriptID || si.ExecSide != (ScriptExecSide) execCombo.SelectedIndex)
    {
      if (si.ScriptLinkID != 0L)
      {
        IDBRelation relation = activity.Session.GetRelation(si.ScriptLinkID, false);
        if (relation != null)
        {
          relation.Delete(0L);
          si.ScriptLinkID = 0L;
          modified = true;
        }
      }
      if ((checkBox == null || checkBox.Checked) && si.ScriptID != 0L)
      {
        si.OldScriptID = si.ScriptID;
        IDBRelation relation;
        if (si.ScriptLinkID == 0L)
        {
          relation = activity.Session.GetRelationCollection(wfConsts.ScriptRelationTypeID).Create(activity.ObjectID, si.ScriptID);
          si.ScriptLinkID = relation.RelationID;
          IDBAttribute attributeById = relation.GetAttributeByID(wfConsts.AttrScriptKindID);
          if (attributeById != null)
            attributeById.AsInteger = (long) sk;
          modified = true;
        }
        else
          relation = activity.Session.GetRelation(si.ScriptLinkID, false);
        if (relation != null)
        {
          IDBAttribute attributeById = relation.GetAttributeByID(wfConsts.AttrScriptExecSideID);
          if (attributeById != null)
          {
            attributeById.AsInteger = (long) execCombo.SelectedIndex;
            modified = true;
          }
        }
      }
    }
    return modified;
  }

  private void BeforeScriptCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this.beforeScriptEdit.Enabled = this.BeforeScriptCheckBox.Checked;
    this.beforeScriptType.Enabled = !this.BeforeScriptCheckBox.Checked;
    this.BeforeScriptExecCombo.Enabled = !1.Equals(this.BeforeScriptExecCombo.Tag);
    this.afterScriptEdit.Enabled = this.AfterScriptCheckBox.Checked;
    this.afterScriptType.Enabled = !this.AfterScriptCheckBox.Checked;
    this.AfterScriptExecCombo.Enabled = !1.Equals(this.AfterScriptExecCombo.Tag);
    if (this._loading || !(sender is CheckBox) || !this._checkLocalScriptNotDelete)
      return;
    ButtonEdit sender1 = this.afterScriptEdit;
    if (sender == this.BeforeScriptCheckBox)
      sender1 = this.beforeScriptEdit;
    if (((CheckBox) sender).Checked)
    {
      this.EditScriptButton_Click((object) sender1, (ButtonPressedEventArgs) null);
      if (this._settings.ScriptInfos[Convert.ToInt32(sender1.Tag)].ScriptID != 0L)
        return;
      this._checkLocalScriptNotCreate = true;
      (sender as CheckBox).Checked = false;
      this._checkLocalScriptNotCreate = false;
    }
    else
    {
      ScriptInfo si = this._settings.ScriptInfos[Convert.ToInt32(sender1.Tag)];
      DialogResult dialogResult = DialogResult.Yes;
      if (si.ScriptType == WorkflowScriptType.Local && !this._checkLocalScriptNotCreate)
        dialogResult = MessageBox.Show("Данное действие приведёт к удалению локального сценария. Хотите продолжить?", "Внимание", MessageBoxButtons.YesNo);
      switch (dialogResult)
      {
        case DialogResult.Yes:
          if (si.ScriptID > 0L)
          {
            if (si.ScriptType == WorkflowScriptType.Local)
            {
              if (this.NewScripts.ContainsValue(si.ScriptID))
                this.NewScripts.Remove(this.NewScripts.FirstOrDefault<KeyValuePair<int, long>>((System.Func<KeyValuePair<int, long>, bool>) (x => x.Value == si.ScriptID)).Key);
              if (!this.LocalScriptsToDeleted.Contains(si.ScriptID))
              {
                this.LocalScriptsToDeleted.Add(si.ScriptID);
                this.AddedNewScriptToDelete = true;
              }
            }
            si.OldScriptID = si.ScriptID;
            si.ScriptID = 0L;
          }
          sender1.Text = string.Empty;
          break;
        case DialogResult.No:
          this._checkLocalScriptNotDelete = false;
          ((CheckBox) sender).Checked = true;
          this._checkLocalScriptNotDelete = true;
          break;
      }
    }
  }

  private void ScriptExecCombo_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.ScriptUserGroupBox.Enabled = this.ScriptExecCombo.SelectedIndex == sc_21974.ssp_workflow_21975(437076009);
  }

  private void ScriptUserEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    if (!wfFunx.BrowseForUsers(this._settings.Participants, this._settings.ProcessID))
      return;
    this.ScriptUserEdit.Text = this._settings.Participants.ToUserString();
    this._additionalParticipantsModified = true;
  }

  public bool AdditionalParticipantsModified => this._additionalParticipantsModified;

  public bool Save(IDBObject activityToSave, bool modified)
  {
    if (this._settings.ActivityType == wfConsts.ScriptTypeID)
    {
      modified = this.SaveScriptInfo(activityToSave, this._settings.ScriptInfos[0], ScriptKind.BeforeExec, (CheckBox) null, this.ScriptExecCombo, modified);
    }
    else
    {
      modified = this.SaveScriptInfo(activityToSave, this._settings.ScriptInfos[0], ScriptKind.BeforeExec, this.BeforeScriptCheckBox, this.BeforeScriptExecCombo, modified);
      modified = this.SaveScriptInfo(activityToSave, this._settings.ScriptInfos[1], ScriptKind.AfterExec, this.AfterScriptCheckBox, this.AfterScriptExecCombo, modified);
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
    this.AfterScriptGroupBox = new GroupBox();
    this.afterScriptEdit = new ButtonEdit();
    this.AfterScriptExecCombo = new System.Windows.Forms.ComboBox();
    this.afterScriptType = new System.Windows.Forms.ComboBox();
    this.label6 = new Label();
    this.label17 = new Label();
    this.AfterScriptCheckBox = new CheckBox();
    this.panel13 = new Panel();
    this.BeforeScriptGroupBox = new GroupBox();
    this.beforeScriptEdit = new ButtonEdit();
    this.BeforeScriptExecCombo = new System.Windows.Forms.ComboBox();
    this.beforeScriptType = new System.Windows.Forms.ComboBox();
    this.label4 = new Label();
    this.label16 = new Label();
    this.BeforeScriptCheckBox = new CheckBox();
    this.ScriptGroupBox = new GroupBox();
    this.scriptEdit = new ButtonEdit();
    this.ScriptUserGroupBox = new GroupBox();
    this.ScriptUserEdit = new ButtonEdit();
    this.scriptType = new System.Windows.Forms.ComboBox();
    this.ScriptExecCombo = new System.Windows.Forms.ComboBox();
    this.label14 = new Label();
    this.label10 = new Label();
    this.AfterScriptGroupBox.SuspendLayout();
    this.afterScriptEdit.Properties.BeginInit();
    this.BeforeScriptGroupBox.SuspendLayout();
    this.beforeScriptEdit.Properties.BeginInit();
    this.ScriptGroupBox.SuspendLayout();
    this.scriptEdit.Properties.BeginInit();
    this.ScriptUserGroupBox.SuspendLayout();
    this.ScriptUserEdit.Properties.BeginInit();
    this.SuspendLayout();
    this.AfterScriptGroupBox.Controls.Add((Control) this.afterScriptEdit);
    this.AfterScriptGroupBox.Controls.Add((Control) this.AfterScriptExecCombo);
    this.AfterScriptGroupBox.Controls.Add((Control) this.afterScriptType);
    this.AfterScriptGroupBox.Controls.Add((Control) this.label6);
    this.AfterScriptGroupBox.Controls.Add((Control) this.label17);
    this.AfterScriptGroupBox.Controls.Add((Control) this.AfterScriptCheckBox);
    this.AfterScriptGroupBox.Dock = DockStyle.Top;
    this.AfterScriptGroupBox.Location = new Point(0, 272);
    this.AfterScriptGroupBox.Name = "AfterScriptGroupBox";
    this.AfterScriptGroupBox.Size = new Size(802, 100);
    this.AfterScriptGroupBox.TabIndex = 8;
    this.AfterScriptGroupBox.TabStop = false;
    this.AfterScriptGroupBox.Text = "После выполнения действия";
    this.afterScriptEdit.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.afterScriptEdit.EditValue = (object) "";
    this.afterScriptEdit.Location = new Point(178, 28);
    this.afterScriptEdit.Name = "afterScriptEdit";
    this.afterScriptEdit.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.afterScriptEdit.Properties.ReadOnly = true;
    this.afterScriptEdit.Size = new Size(614, 22);
    this.afterScriptEdit.TabIndex = 6;
    this.afterScriptEdit.Tag = (object) "1";
    this.afterScriptEdit.ButtonClick += new ButtonPressedEventHandler(this.EditScriptButton_Click);
    this.AfterScriptExecCombo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.AfterScriptExecCombo.DropDownStyle = ComboBoxStyle.DropDownList;
    this.AfterScriptExecCombo.FormattingEnabled = true;
    this.AfterScriptExecCombo.Items.AddRange(new object[2]
    {
      (object) "На сервере",
      (object) "На клиенте"
    });
    this.AfterScriptExecCombo.Location = new Point(611, 63 /*0x3F*/);
    this.AfterScriptExecCombo.Name = "AfterScriptExecCombo";
    this.AfterScriptExecCombo.Size = new Size(181, 24);
    this.AfterScriptExecCombo.TabIndex = 9;
    this.afterScriptType.DropDownStyle = ComboBoxStyle.DropDownList;
    this.afterScriptType.FormattingEnabled = true;
    this.afterScriptType.Items.AddRange(new object[2]
    {
      (object) "Локальный",
      (object) "Общий"
    });
    this.afterScriptType.Location = new Point(112 /*0x70*/, 63 /*0x3F*/);
    this.afterScriptType.Name = "afterScriptType";
    this.afterScriptType.Size = new Size(164, 24);
    this.afterScriptType.TabIndex = 10;
    this.label6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.label6.AutoSize = true;
    this.label6.ImeMode = ImeMode.NoControl;
    this.label6.Location = new Point(400, 66);
    this.label6.Name = "label6";
    this.label6.Size = new Size(205, 17);
    this.label6.TabIndex = 8;
    this.label6.Text = "Сценарий будет выполняться";
    this.label17.AutoSize = true;
    this.label17.ImeMode = ImeMode.NoControl;
    this.label17.Location = new Point(6, 67);
    this.label17.Name = "label17";
    this.label17.Size = new Size(100, 17);
    this.label17.TabIndex = 6;
    this.label17.Text = "Тип сценария";
    this.AfterScriptCheckBox.AutoSize = true;
    this.AfterScriptCheckBox.ImeMode = ImeMode.NoControl;
    this.AfterScriptCheckBox.Location = new Point(9, 28);
    this.AfterScriptCheckBox.Name = "AfterScriptCheckBox";
    this.AfterScriptCheckBox.Size = new Size(162, 21);
    this.AfterScriptCheckBox.TabIndex = 2;
    this.AfterScriptCheckBox.Text = "Сценарий назначен";
    this.AfterScriptCheckBox.UseVisualStyleBackColor = true;
    this.AfterScriptCheckBox.CheckedChanged += new EventHandler(this.BeforeScriptCheckBox_CheckedChanged);
    this.panel13.Dock = DockStyle.Top;
    this.panel13.Location = new Point(0, 260);
    this.panel13.Name = "panel13";
    this.panel13.Size = new Size(802, 12);
    this.panel13.TabIndex = 11;
    this.BeforeScriptGroupBox.Controls.Add((Control) this.beforeScriptEdit);
    this.BeforeScriptGroupBox.Controls.Add((Control) this.BeforeScriptExecCombo);
    this.BeforeScriptGroupBox.Controls.Add((Control) this.beforeScriptType);
    this.BeforeScriptGroupBox.Controls.Add((Control) this.label4);
    this.BeforeScriptGroupBox.Controls.Add((Control) this.label16);
    this.BeforeScriptGroupBox.Controls.Add((Control) this.BeforeScriptCheckBox);
    this.BeforeScriptGroupBox.Dock = DockStyle.Top;
    this.BeforeScriptGroupBox.Location = new Point(0, 160 /*0xA0*/);
    this.BeforeScriptGroupBox.Name = "BeforeScriptGroupBox";
    this.BeforeScriptGroupBox.Size = new Size(802, 100);
    this.BeforeScriptGroupBox.TabIndex = 9;
    this.BeforeScriptGroupBox.TabStop = false;
    this.BeforeScriptGroupBox.Text = "Перед выполнением действия";
    this.beforeScriptEdit.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.beforeScriptEdit.EditValue = (object) "";
    this.beforeScriptEdit.Location = new Point(178, 28);
    this.beforeScriptEdit.Name = "beforeScriptEdit";
    this.beforeScriptEdit.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.beforeScriptEdit.Properties.ReadOnly = true;
    this.beforeScriptEdit.Size = new Size(614, 22);
    this.beforeScriptEdit.TabIndex = 6;
    this.beforeScriptEdit.Tag = (object) "0";
    this.beforeScriptEdit.ButtonClick += new ButtonPressedEventHandler(this.EditScriptButton_Click);
    this.BeforeScriptExecCombo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.BeforeScriptExecCombo.DropDownStyle = ComboBoxStyle.DropDownList;
    this.BeforeScriptExecCombo.FormattingEnabled = true;
    this.BeforeScriptExecCombo.Items.AddRange(new object[2]
    {
      (object) "На сервере",
      (object) "На клиенте"
    });
    this.BeforeScriptExecCombo.Location = new Point(611, 63 /*0x3F*/);
    this.BeforeScriptExecCombo.Name = "BeforeScriptExecCombo";
    this.BeforeScriptExecCombo.Size = new Size(181, 24);
    this.BeforeScriptExecCombo.TabIndex = 7;
    this.beforeScriptType.DropDownStyle = ComboBoxStyle.DropDownList;
    this.beforeScriptType.FormattingEnabled = true;
    this.beforeScriptType.Items.AddRange(new object[2]
    {
      (object) "Локальный",
      (object) "Общий"
    });
    this.beforeScriptType.Location = new Point(113, 63 /*0x3F*/);
    this.beforeScriptType.Name = "beforeScriptType";
    this.beforeScriptType.Size = new Size(164, 24);
    this.beforeScriptType.TabIndex = 8;
    this.label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.label4.AutoSize = true;
    this.label4.ImeMode = ImeMode.NoControl;
    this.label4.Location = new Point(400, 66);
    this.label4.Name = "label4";
    this.label4.Size = new Size(205, 17);
    this.label4.TabIndex = 6;
    this.label4.Text = "Сценарий будет выполняться";
    this.label16.AutoSize = true;
    this.label16.ImeMode = ImeMode.NoControl;
    this.label16.Location = new Point(7, 67);
    this.label16.Name = "label16";
    this.label16.Size = new Size(100, 17);
    this.label16.TabIndex = 6;
    this.label16.Text = "Тип сценария";
    this.BeforeScriptCheckBox.AutoSize = true;
    this.BeforeScriptCheckBox.ImeMode = ImeMode.NoControl;
    this.BeforeScriptCheckBox.Location = new Point(10, 28);
    this.BeforeScriptCheckBox.Name = "BeforeScriptCheckBox";
    this.BeforeScriptCheckBox.Size = new Size(162, 21);
    this.BeforeScriptCheckBox.TabIndex = 3;
    this.BeforeScriptCheckBox.Text = "Сценарий назначен";
    this.BeforeScriptCheckBox.UseVisualStyleBackColor = true;
    this.BeforeScriptCheckBox.CheckedChanged += new EventHandler(this.BeforeScriptCheckBox_CheckedChanged);
    this.ScriptGroupBox.Controls.Add((Control) this.scriptEdit);
    this.ScriptGroupBox.Controls.Add((Control) this.ScriptUserGroupBox);
    this.ScriptGroupBox.Controls.Add((Control) this.scriptType);
    this.ScriptGroupBox.Controls.Add((Control) this.ScriptExecCombo);
    this.ScriptGroupBox.Controls.Add((Control) this.label14);
    this.ScriptGroupBox.Controls.Add((Control) this.label10);
    this.ScriptGroupBox.Dock = DockStyle.Top;
    this.ScriptGroupBox.Location = new Point(0, 0);
    this.ScriptGroupBox.Name = "ScriptGroupBox";
    this.ScriptGroupBox.Size = new Size(802, 160 /*0xA0*/);
    this.ScriptGroupBox.TabIndex = 10;
    this.ScriptGroupBox.TabStop = false;
    this.ScriptGroupBox.Text = "Настройка сценария";
    this.ScriptGroupBox.Visible = false;
    this.scriptEdit.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.scriptEdit.EditValue = (object) "";
    this.scriptEdit.Location = new Point(10, 22);
    this.scriptEdit.Name = "scriptEdit";
    this.scriptEdit.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.scriptEdit.Properties.ReadOnly = true;
    this.scriptEdit.Size = new Size(782, 22);
    this.scriptEdit.TabIndex = 6;
    this.scriptEdit.Tag = (object) "0";
    this.scriptEdit.ButtonClick += new ButtonPressedEventHandler(this.EditScriptButton_Click);
    this.ScriptUserGroupBox.AutoSize = true;
    this.ScriptUserGroupBox.Controls.Add((Control) this.ScriptUserEdit);
    this.ScriptUserGroupBox.Dock = DockStyle.Bottom;
    this.ScriptUserGroupBox.Location = new Point(3, 100);
    this.ScriptUserGroupBox.Name = "ScriptUserGroupBox";
    this.ScriptUserGroupBox.Padding = new Padding(10);
    this.ScriptUserGroupBox.Size = new Size(796, 57);
    this.ScriptUserGroupBox.TabIndex = 8;
    this.ScriptUserGroupBox.TabStop = false;
    this.ScriptUserGroupBox.Text = "Исполнители сценария";
    this.ScriptUserEdit.Dock = DockStyle.Top;
    this.ScriptUserEdit.EditValue = (object) "";
    this.ScriptUserEdit.Location = new Point(10, 25);
    this.ScriptUserEdit.Name = "ScriptUserEdit";
    this.ScriptUserEdit.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.ScriptUserEdit.Properties.ReadOnly = true;
    this.ScriptUserEdit.Size = new Size(776, 22);
    this.ScriptUserEdit.TabIndex = 5;
    this.ScriptUserEdit.ButtonClick += new ButtonPressedEventHandler(this.ScriptUserEdit_ButtonClick);
    this.scriptType.DropDownStyle = ComboBoxStyle.DropDownList;
    this.scriptType.FormattingEnabled = true;
    this.scriptType.Items.AddRange(new object[2]
    {
      (object) "Локальный",
      (object) "Общий"
    });
    this.scriptType.Location = new Point(112 /*0x70*/, 64 /*0x40*/);
    this.scriptType.Name = "scriptType";
    this.scriptType.Size = new Size(164, 24);
    this.scriptType.TabIndex = 9;
    this.ScriptExecCombo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.ScriptExecCombo.DropDownStyle = ComboBoxStyle.DropDownList;
    this.ScriptExecCombo.FormattingEnabled = true;
    this.ScriptExecCombo.Items.AddRange(new object[2]
    {
      (object) "На сервере",
      (object) "На клиенте"
    });
    this.ScriptExecCombo.Location = new Point(611, 63 /*0x3F*/);
    this.ScriptExecCombo.Name = "ScriptExecCombo";
    this.ScriptExecCombo.Size = new Size(181, 24);
    this.ScriptExecCombo.TabIndex = 7;
    this.ScriptExecCombo.SelectedIndexChanged += new EventHandler(this.ScriptExecCombo_SelectedIndexChanged);
    this.label14.AutoSize = true;
    this.label14.ImeMode = ImeMode.NoControl;
    this.label14.Location = new Point(6, 67);
    this.label14.Name = "label14";
    this.label14.Size = new Size(100, 17);
    this.label14.TabIndex = 6;
    this.label14.Text = "Тип сценария";
    this.label10.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.label10.AutoSize = true;
    this.label10.ImeMode = ImeMode.NoControl;
    this.label10.Location = new Point(400, 66);
    this.label10.Name = "label10";
    this.label10.Size = new Size(205, 17);
    this.label10.TabIndex = 6;
    this.label10.Text = "Сценарий будет выполняться";
    this.AutoScaleDimensions = new SizeF(120f, 120f);
    this.AutoScaleMode = AutoScaleMode.Dpi;
    this.BackColor = SystemColors.ControlLightLight;
    this.Controls.Add((Control) this.AfterScriptGroupBox);
    this.Controls.Add((Control) this.panel13);
    this.Controls.Add((Control) this.BeforeScriptGroupBox);
    this.Controls.Add((Control) this.ScriptGroupBox);
    this.Name = nameof (ScriptsSettingPageControl);
    this.Size = new Size(802, 390);
    this.AfterScriptGroupBox.ResumeLayout(false);
    this.AfterScriptGroupBox.PerformLayout();
    this.afterScriptEdit.Properties.EndInit();
    this.BeforeScriptGroupBox.ResumeLayout(false);
    this.BeforeScriptGroupBox.PerformLayout();
    this.beforeScriptEdit.Properties.EndInit();
    this.ScriptGroupBox.ResumeLayout(false);
    this.ScriptGroupBox.PerformLayout();
    this.scriptEdit.Properties.EndInit();
    this.ScriptUserGroupBox.ResumeLayout(false);
    this.ScriptUserEdit.Properties.EndInit();
    this.ResumeLayout(false);
  }
}
