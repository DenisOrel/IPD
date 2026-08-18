// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ActivityPropertyPages.SubProcessSettingPageControl
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Navigator.DBObjects;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design.ActivityPropertyPages;

public class SubProcessSettingPageControl : UserControl
{
  private bool _readOnly;
  private ActivitySettings _settings;
  private ParticipantList _customParticipant;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private GroupBox GroupBox5;
  private CheckBox useActualVersionSchemeCheckBox;
  private ButtonEdit SchemeEdit;
  private Label Label3;
  private Label SubNameLabel;
  private CheckBox WaitCheckBox;
  private ButtonEdit SubNameEdit;
  private GroupBox SubProcessUserGroupBox;
  private ButtonEdit SubProcessUserEdit;
  private CheckBox useCustomParticipantCheckBox;

  public SubProcessSettingPageControl() => this.InitializeComponent();

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

  public bool LoadSubProcessSettingPageControl(ActivitySettings settings, IDBObject activityObject)
  {
    this._settings = settings;
    bool flag = false;
    if (settings.ActivityType == wfConsts.SubProcessTypeID)
    {
      IDBAttribute byId1 = activityObject.Attributes.FindByID(wfConsts.AttrSubprocessSchemeID);
      if (byId1 != null)
        this.LocalSubprocessID = byId1.AsInteger;
      IDBAttribute byId2 = activityObject.Attributes.FindByID(wfConsts.AttrSubprocFormatID);
      if (byId2 != null)
        this.SubNameEdit.Text = byId2.AsString;
      IDBAttribute byId3 = activityObject.Attributes.FindByID(wfConsts.AttrWaitForCompletionID);
      if (byId3 != null)
        this.WaitCheckBox.Checked = byId3.AsBoolean;
      this.useActualVersionSchemeCheckBox.Checked = settings.ExtProperties.ReadBool("UseActualSchemeVersion");
      this.useCustomParticipantCheckBox.Checked = settings.ExtProperties.ReadBool("UseCustomParticipant");
      this.SubProcessUserGroupBox.Visible = this.useCustomParticipantCheckBox.Checked;
      string str = this._settings.ExtProperties.Ini.ReadString("Props", "CustomParticipant", new ParticipantList(activityObject.Session).AsString);
      this._customParticipant = new ParticipantList()
      {
        AsString = str
      };
      this.SubProcessUserEdit.Text = this._customParticipant.ToUserString();
    }
    else
      flag = true;
    return flag;
  }

  private void SchemeEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    long num = wfFunx.BrowseForScheme();
    if (num == -1L)
      return;
    this.LocalSubprocessID = num;
  }

  private void SubNameEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Workflow.Design_11") + LocalizationHolder.rm.GetString("Workflow.Design_12") + LocalizationHolder.rm.GetString("Workflow.Design_13") + LocalizationHolder.rm.GetString("Workflow.Design_14") + LocalizationHolder.rm.GetString("Workflow.Design_15"), LocalizationHolder.rm.GetString("Workflow.Design_16"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }

  private long LocalSubprocessID
  {
    get => Convert.ToInt64(this.SchemeEdit.Tag);
    set
    {
      this.SchemeEdit.Tag = (object) value;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        ObjectSystemPropertiesEx systemPropertiesEx = sessionKeeper.Session.GetObjectSystemPropertiesEx(value, false);
        if (systemPropertiesEx == null)
          return;
        this.SchemeEdit.Text = CaptionTransform.GetCaption(systemPropertiesEx.Caption, (long) systemPropertiesEx.VersionID);
      }
    }
  }

  public bool Save(IDBObject activityToSave, bool modified)
  {
    IDBAttribute byId1 = activityToSave.Attributes.FindByID(wfConsts.AttrSubprocessSchemeID);
    if (byId1 != null && byId1.AsInteger != Convert.ToInt64(this.SchemeEdit.Tag))
    {
      modified = true;
      byId1.AsInteger = Convert.ToInt64(this.SchemeEdit.Tag);
    }
    IDBAttribute byId2 = activityToSave.Attributes.FindByID(wfConsts.AttrSubprocFormatID);
    if (byId2 != null && byId2.AsString != this.SubNameEdit.Text)
    {
      modified = true;
      byId2.AsString = this.SubNameEdit.Text;
    }
    IDBAttribute byId3 = activityToSave.Attributes.FindByID(wfConsts.AttrWaitForCompletionID);
    if (byId3 != null && byId3.AsBoolean != this.WaitCheckBox.Checked)
    {
      modified = true;
      byId3.AsBoolean = this.WaitCheckBox.Checked;
    }
    this._settings.ExtProperties.WriteBool("UseActualSchemeVersion", this.useActualVersionSchemeCheckBox.Checked, ExtPropertiesFlag.SubProcess);
    this._settings.ExtProperties.WriteBool("UseCustomParticipant", this.useCustomParticipantCheckBox.Checked, ExtPropertiesFlag.SubProcess);
    this._settings.ExtProperties.Write("CustomParticipant", this._customParticipant.AsString, ExtPropertiesFlag.SubProcess, new ParticipantList(activityToSave.Session).AsString);
    return modified;
  }

  private void SubProcessUserEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    if (!wfFunx.BrowseForUsers(this._customParticipant, this._settings.ProcessID))
      return;
    this.SubProcessUserEdit.Text = this._customParticipant.ToUserString();
  }

  private void useCustomParticipant_CheckedChanged(object sender, EventArgs e)
  {
    this.SubProcessUserGroupBox.Visible = this.useCustomParticipantCheckBox.Checked;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SubProcessSettingPageControl));
    this.GroupBox5 = new GroupBox();
    this.useActualVersionSchemeCheckBox = new CheckBox();
    this.SchemeEdit = new ButtonEdit();
    this.Label3 = new Label();
    this.SubNameLabel = new Label();
    this.WaitCheckBox = new CheckBox();
    this.SubNameEdit = new ButtonEdit();
    this.SubProcessUserGroupBox = new GroupBox();
    this.SubProcessUserEdit = new ButtonEdit();
    this.useCustomParticipantCheckBox = new CheckBox();
    this.GroupBox5.SuspendLayout();
    this.SchemeEdit.Properties.BeginInit();
    this.SubNameEdit.Properties.BeginInit();
    this.SubProcessUserGroupBox.SuspendLayout();
    this.SubProcessUserEdit.Properties.BeginInit();
    this.SuspendLayout();
    this.GroupBox5.Controls.Add((Control) this.useCustomParticipantCheckBox);
    this.GroupBox5.Controls.Add((Control) this.useActualVersionSchemeCheckBox);
    this.GroupBox5.Controls.Add((Control) this.SchemeEdit);
    this.GroupBox5.Controls.Add((Control) this.Label3);
    this.GroupBox5.Controls.Add((Control) this.SubNameLabel);
    this.GroupBox5.Controls.Add((Control) this.WaitCheckBox);
    this.GroupBox5.Controls.Add((Control) this.SubNameEdit);
    this.GroupBox5.Dock = DockStyle.Top;
    this.GroupBox5.Location = new Point(0, 0);
    this.GroupBox5.Name = "GroupBox5";
    this.GroupBox5.Size = new Size(630, 221);
    this.GroupBox5.TabIndex = 1;
    this.GroupBox5.TabStop = false;
    this.useActualVersionSchemeCheckBox.AutoSize = true;
    this.useActualVersionSchemeCheckBox.Checked = true;
    this.useActualVersionSchemeCheckBox.CheckState = CheckState.Checked;
    this.useActualVersionSchemeCheckBox.ImeMode = ImeMode.NoControl;
    this.useActualVersionSchemeCheckBox.Location = new Point(12, 166);
    this.useActualVersionSchemeCheckBox.Name = "useActualVersionSchemeCheckBox";
    this.useActualVersionSchemeCheckBox.Size = new Size(296, 21);
    this.useActualVersionSchemeCheckBox.TabIndex = 7;
    this.useActualVersionSchemeCheckBox.Text = "Использовать базовую версию шаблона";
    this.useActualVersionSchemeCheckBox.UseVisualStyleBackColor = true;
    this.SchemeEdit.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.SchemeEdit.EditValue = (object) "";
    this.SchemeEdit.Location = new Point(12, 40);
    this.SchemeEdit.Name = "SchemeEdit";
    this.SchemeEdit.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, false, HorzAlignment.Center, (Image) componentResourceManager.GetObject("SchemeEdit.Properties.Buttons"))
    });
    this.SchemeEdit.Properties.ReadOnly = true;
    this.SchemeEdit.Size = new Size(606, 24);
    this.SchemeEdit.TabIndex = 5;
    this.SchemeEdit.ButtonClick += new ButtonPressedEventHandler(this.SchemeEdit_ButtonClick);
    this.Label3.AutoSize = true;
    this.Label3.ImeMode = ImeMode.NoControl;
    this.Label3.Location = new Point(8, 18);
    this.Label3.Name = "Label3";
    this.Label3.Size = new Size(216, 17);
    this.Label3.TabIndex = 0;
    this.Label3.Text = "Запустить процесс по шаблону";
    this.SubNameLabel.AutoSize = true;
    this.SubNameLabel.ImeMode = ImeMode.NoControl;
    this.SubNameLabel.Location = new Point(8, 77);
    this.SubNameLabel.Name = "SubNameLabel";
    this.SubNameLabel.Size = new Size(200, 17);
    this.SubNameLabel.TabIndex = 1;
    this.SubNameLabel.Text = "Формат имени подпроцесса:";
    this.WaitCheckBox.AutoSize = true;
    this.WaitCheckBox.ImeMode = ImeMode.NoControl;
    this.WaitCheckBox.Location = new Point(12, 140);
    this.WaitCheckBox.Name = "WaitCheckBox";
    this.WaitCheckBox.Size = new Size(158, 21);
    this.WaitCheckBox.TabIndex = 3;
    this.WaitCheckBox.Text = "Ждать завершения";
    this.SubNameEdit.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.SubNameEdit.EditValue = (object) "%1% - %2%";
    this.SubNameEdit.Location = new Point(12, 98);
    this.SubNameEdit.Name = "SubNameEdit";
    this.SubNameEdit.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Glyph, "?", 16 /*0x10*/, true, true, false, HorzAlignment.Center, (Image) componentResourceManager.GetObject("SubNameEdit.Properties.Buttons"))
    });
    this.SubNameEdit.Size = new Size(606, 24);
    this.SubNameEdit.TabIndex = 6;
    this.SubNameEdit.ButtonClick += new ButtonPressedEventHandler(this.SubNameEdit_ButtonClick);
    this.SubProcessUserGroupBox.AutoSize = true;
    this.SubProcessUserGroupBox.Controls.Add((Control) this.SubProcessUserEdit);
    this.SubProcessUserGroupBox.Dock = DockStyle.Top;
    this.SubProcessUserGroupBox.Location = new Point(0, 221);
    this.SubProcessUserGroupBox.Name = "SubProcessUserGroupBox";
    this.SubProcessUserGroupBox.Padding = new Padding(10);
    this.SubProcessUserGroupBox.Size = new Size(630, 57);
    this.SubProcessUserGroupBox.TabIndex = 9;
    this.SubProcessUserGroupBox.TabStop = false;
    this.SubProcessUserGroupBox.Text = "Инициатор подпроцесса";
    this.SubProcessUserEdit.Dock = DockStyle.Top;
    this.SubProcessUserEdit.EditValue = (object) "";
    this.SubProcessUserEdit.Location = new Point(10, 25);
    this.SubProcessUserEdit.Name = "SubProcessUserEdit";
    this.SubProcessUserEdit.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.SubProcessUserEdit.Properties.ReadOnly = true;
    this.SubProcessUserEdit.Size = new Size(610, 22);
    this.SubProcessUserEdit.TabIndex = 5;
    this.SubProcessUserEdit.ButtonClick += new ButtonPressedEventHandler(this.SubProcessUserEdit_ButtonClick);
    this.useCustomParticipantCheckBox.AutoSize = true;
    this.useCustomParticipantCheckBox.Location = new Point(12, 193);
    this.useCustomParticipantCheckBox.Name = "useCustomParticipantCheckBox";
    this.useCustomParticipantCheckBox.Size = new Size(284, 21);
    this.useCustomParticipantCheckBox.TabIndex = 8;
    this.useCustomParticipantCheckBox.Text = "Использовать назначенного инициатора подпроцесса";
    this.useCustomParticipantCheckBox.UseVisualStyleBackColor = true;
    this.useCustomParticipantCheckBox.CheckedChanged += new EventHandler(this.useCustomParticipant_CheckedChanged);
    this.AutoScaleDimensions = new SizeF(120f, 120f);
    this.AutoScaleMode = AutoScaleMode.Dpi;
    this.BackColor = SystemColors.ControlLightLight;
    this.Controls.Add((Control) this.SubProcessUserGroupBox);
    this.Controls.Add((Control) this.GroupBox5);
    this.Name = nameof (SubProcessSettingPageControl);
    this.Size = new Size(630, 287);
    this.GroupBox5.ResumeLayout(false);
    this.GroupBox5.PerformLayout();
    this.SchemeEdit.Properties.EndInit();
    this.SubNameEdit.Properties.EndInit();
    this.SubProcessUserGroupBox.ResumeLayout(false);
    this.SubProcessUserEdit.Properties.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
