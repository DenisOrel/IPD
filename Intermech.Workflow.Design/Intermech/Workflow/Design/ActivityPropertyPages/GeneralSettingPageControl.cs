// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ActivityPropertyPages.GeneralSettingPageControl
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

public class GeneralSettingPageControl : UserControl
{
  private bool _readOnly;
  private ActivitySettings _settings;
  public bool NameModified;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel ActRuntimePanel;
  private Label label8;
  private Label txtCompletedLabel;
  private Label txtStartedLabel;
  private Label txtStatusLabel;
  private Label CompletedLabel;
  private Label StartedLabel;
  private Label StatusLabel;
  private Panel panel1;
  private CheckBox showFormWhereActivityBack;
  private CheckBox CollectorCheckBox;
  private Label Label1;
  private TextBox NameEdit;
  private Label Label2;
  private PictureBox ActImage;
  private TextBox DescriptionMemo;
  private Label activityIDLbl;

  public GeneralSettingPageControl() => this.InitializeComponent();

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

  public void LoadGeneralPropertyControl(ActivitySettings settings, IDBObject activityObject)
  {
    this._settings = settings;
    this.ActImage.Image = settings.ActivityIcon;
    this.activityIDLbl.Text = settings.ActivityObjectID.ToString();
    if (settings.ActivityType == wfConsts.SchemesTypeID)
    {
      IDBAttribute attributeById = activityObject.GetAttributeByID(wfConsts.AttrShowFormWithActivityBackID);
      this.showFormWhereActivityBack.Visible = true;
      this.showFormWhereActivityBack.Enabled = true;
      if (attributeById != null)
        this.showFormWhereActivityBack.Checked = attributeById.AsBoolean;
    }
    else
    {
      this.showFormWhereActivityBack.Visible = false;
      this.showFormWhereActivityBack.Enabled = false;
    }
    if (settings.ActivityName == null)
      this.NameEdit.Enabled = false;
    else
      this.NameEdit.Text = settings.ActivityName;
    if (settings.ActivityDescription == null)
      this.DescriptionMemo.Enabled = false;
    else
      this.DescriptionMemo.Text = settings.ActivityDescription;
    IDBAttribute attributeById1 = activityObject.GetAttributeByID(wfConsts.AttrCollectorID);
    if (attributeById1 != null && settings.ActivityType != wfConsts.StartTypeID && settings.ActivityType != wfConsts.StopTypeID)
    {
      this.CollectorCheckBox.Checked = attributeById1.AsBoolean;
    }
    else
    {
      this.CollectorCheckBox.Enabled = false;
      this.CollectorCheckBox.Visible = false;
    }
    settings.ActivityStatus = ActivityStatus.OnApproach;
    IDBAttribute attributeById2 = activityObject.GetAttributeByID(wfConsts.AttrActivityStatusID);
    if (attributeById2 != null)
    {
      this.ActRuntimePanel.Visible = true;
      this.StatusLabel.Visible = true;
      this.txtStatusLabel.Visible = true;
      if (settings.ActivityType == wfConsts.SchemesTypeID)
      {
        this.StatusLabel.Text = attributeById2.AsInteger < 0L ? SimpleFuncs.GetEnumDescription((Enum) (SchemeStatus) attributeById2.AsInteger) : "";
      }
      else
      {
        settings.ActivityStatus = (ActivityStatus) attributeById2.AsInteger;
        this.StatusLabel.Text = SimpleFuncs.GetEnumDescription((Enum) settings.ActivityStatus);
        if (settings.ActivityStatus != ActivityStatus.OnApproach && settings.ActivityType == wfConsts.RemoteSubProcessTypeID)
        {
          IDBAttribute attributeById3 = activityObject.GetAttributeByID(wfConsts.AttrRemoteProcessStatusID);
          if (attributeById3 != null)
          {
            string enumDescription = SimpleFuncs.GetEnumDescription((Enum) (RemoteProcessStatus) attributeById3.AsInteger);
            if (enumDescription != "")
              this.StatusLabel.Text = $"{this.StatusLabel.Text} ({enumDescription})";
          }
        }
      }
    }
    settings.ObjectIDwithVars = settings.ActivityStatus == ActivityStatus.OnApproach ? settings.ProcessID : settings.ActivityObjectID;
    bool flag1 = false;
    bool flag2 = false;
    IDBAttribute byId1 = activityObject.Attributes.FindByID(wfConsts.AttrStartedID);
    if (byId1 != null)
    {
      this.ActRuntimePanel.Visible = true;
      this.StartedLabel.Visible = true;
      this.txtStartedLabel.Visible = true;
      this.StartedLabel.Text = byId1.AsString;
      flag1 = true;
    }
    IDBAttribute byId2 = activityObject.Attributes.FindByID(wfConsts.AttrCompletedID);
    if (byId2 != null)
    {
      this.ActRuntimePanel.Visible = true;
      this.CompletedLabel.Visible = true;
      this.txtCompletedLabel.Visible = true;
      this.CompletedLabel.Text = byId2.AsString;
      flag2 = true;
    }
    if (!flag1)
      this.ActRuntimePanel.Height -= this.StartedLabel.Height;
    if (flag2)
      return;
    this.ActRuntimePanel.Height -= this.CompletedLabel.Height;
  }

  public bool Save(IDBObject activityObject)
  {
    bool flag = false;
    if (this.NameEdit.Enabled && this.NameEdit.Modified)
    {
      IDBAttribute byId = activityObject.Attributes.FindByID(wfConsts.AttrNameID);
      if (byId.AsString != this.NameEdit.Text)
      {
        this.NameModified = true;
        flag = true;
        byId.AsString = this.NameEdit.Text;
      }
    }
    if (this.DescriptionMemo.Enabled && this.DescriptionMemo.Modified)
    {
      IDBAttribute byId = activityObject.Attributes.FindByID(wfConsts.AttrDescriptionID);
      if (byId.AsString != this.DescriptionMemo.Text)
      {
        flag = true;
        byId.AsString = this.DescriptionMemo.Text;
      }
    }
    if (this.showFormWhereActivityBack.Enabled)
    {
      activityObject.Attributes.AddAttribute(wfConsts.AttrShowFormWithActivityBackID, false, new object[1]
      {
        (object) this.showFormWhereActivityBack.Checked
      });
      flag = true;
    }
    if (this.CollectorCheckBox.Enabled)
    {
      IDBAttribute byId = activityObject.Attributes.FindByID(wfConsts.AttrCollectorID);
      if (byId != null && this.CollectorCheckBox.Checked != byId.AsBoolean)
      {
        byId.AsBoolean = this.CollectorCheckBox.Checked;
        flag = true;
      }
    }
    return flag;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (GeneralSettingPageControl));
    this.ActRuntimePanel = new Panel();
    this.label8 = new Label();
    this.txtCompletedLabel = new Label();
    this.txtStartedLabel = new Label();
    this.txtStatusLabel = new Label();
    this.CompletedLabel = new Label();
    this.StartedLabel = new Label();
    this.StatusLabel = new Label();
    this.panel1 = new Panel();
    this.activityIDLbl = new Label();
    this.showFormWhereActivityBack = new CheckBox();
    this.CollectorCheckBox = new CheckBox();
    this.Label1 = new Label();
    this.NameEdit = new TextBox();
    this.Label2 = new Label();
    this.ActImage = new PictureBox();
    this.DescriptionMemo = new TextBox();
    this.ActRuntimePanel.SuspendLayout();
    this.panel1.SuspendLayout();
    ((ISupportInitialize) this.ActImage).BeginInit();
    this.SuspendLayout();
    this.ActRuntimePanel.BackColor = Color.Transparent;
    this.ActRuntimePanel.Controls.Add((Control) this.label8);
    this.ActRuntimePanel.Controls.Add((Control) this.txtCompletedLabel);
    this.ActRuntimePanel.Controls.Add((Control) this.txtStartedLabel);
    this.ActRuntimePanel.Controls.Add((Control) this.txtStatusLabel);
    this.ActRuntimePanel.Controls.Add((Control) this.CompletedLabel);
    this.ActRuntimePanel.Controls.Add((Control) this.StartedLabel);
    this.ActRuntimePanel.Controls.Add((Control) this.StatusLabel);
    this.ActRuntimePanel.Dock = DockStyle.Bottom;
    this.ActRuntimePanel.Location = new Point(0, 437);
    this.ActRuntimePanel.Name = "ActRuntimePanel";
    this.ActRuntimePanel.Size = new Size(679, 67);
    this.ActRuntimePanel.TabIndex = 4;
    this.ActRuntimePanel.Visible = false;
    this.label8.BorderStyle = BorderStyle.Fixed3D;
    this.label8.Dock = DockStyle.Top;
    this.label8.ImeMode = ImeMode.NoControl;
    this.label8.Location = new Point(0, 0);
    this.label8.Name = "label8";
    this.label8.Size = new Size(679, 2);
    this.label8.TabIndex = 7;
    this.txtCompletedLabel.AutoSize = true;
    this.txtCompletedLabel.ImeMode = ImeMode.NoControl;
    this.txtCompletedLabel.Location = new Point(6, 38);
    this.txtCompletedLabel.Name = "txtCompletedLabel";
    this.txtCompletedLabel.Size = new Size(91, 17);
    this.txtCompletedLabel.TabIndex = 0;
    this.txtCompletedLabel.Text = "Выполнено :";
    this.txtCompletedLabel.Visible = false;
    this.txtStartedLabel.AutoSize = true;
    this.txtStartedLabel.ImeMode = ImeMode.NoControl;
    this.txtStartedLabel.Location = new Point(6, 22);
    this.txtStartedLabel.Name = "txtStartedLabel";
    this.txtStartedLabel.Size = new Size(65, 17);
    this.txtStartedLabel.TabIndex = 3;
    this.txtStartedLabel.Text = "Начато :";
    this.txtStartedLabel.Visible = false;
    this.txtStatusLabel.AutoSize = true;
    this.txtStatusLabel.Font = new Font("Microsoft Sans Serif", 8.25f);
    this.txtStatusLabel.ImeMode = ImeMode.NoControl;
    this.txtStatusLabel.Location = new Point(6, 5);
    this.txtStatusLabel.Name = "txtStatusLabel";
    this.txtStatusLabel.Size = new Size(61, 17);
    this.txtStatusLabel.TabIndex = 4;
    this.txtStatusLabel.Text = "Статус :";
    this.txtStatusLabel.Visible = false;
    this.CompletedLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.CompletedLabel.ImeMode = ImeMode.NoControl;
    this.CompletedLabel.Location = new Point(88, 38);
    this.CompletedLabel.Name = "CompletedLabel";
    this.CompletedLabel.Size = new Size(580, 16 /*0x10*/);
    this.CompletedLabel.TabIndex = 1;
    this.CompletedLabel.Text = "***";
    this.CompletedLabel.TextAlign = ContentAlignment.TopRight;
    this.CompletedLabel.Visible = false;
    this.StartedLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.StartedLabel.ImeMode = ImeMode.NoControl;
    this.StartedLabel.Location = new Point(88, 21);
    this.StartedLabel.Name = "StartedLabel";
    this.StartedLabel.Size = new Size(580, 16 /*0x10*/);
    this.StartedLabel.TabIndex = 2;
    this.StartedLabel.Text = "***";
    this.StartedLabel.TextAlign = ContentAlignment.TopRight;
    this.StartedLabel.Visible = false;
    this.StatusLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.StatusLabel.ImeMode = ImeMode.NoControl;
    this.StatusLabel.Location = new Point(88, 5);
    this.StatusLabel.Name = "StatusLabel";
    this.StatusLabel.Size = new Size(580, 16 /*0x10*/);
    this.StatusLabel.TabIndex = 5;
    this.StatusLabel.Text = "***";
    this.StatusLabel.TextAlign = ContentAlignment.TopRight;
    this.StatusLabel.Visible = false;
    this.panel1.BackColor = Color.Transparent;
    this.panel1.Controls.Add((Control) this.activityIDLbl);
    this.panel1.Controls.Add((Control) this.showFormWhereActivityBack);
    this.panel1.Controls.Add((Control) this.CollectorCheckBox);
    this.panel1.Controls.Add((Control) this.Label1);
    this.panel1.Controls.Add((Control) this.NameEdit);
    this.panel1.Controls.Add((Control) this.Label2);
    this.panel1.Controls.Add((Control) this.ActImage);
    this.panel1.Controls.Add((Control) this.DescriptionMemo);
    this.panel1.Dock = DockStyle.Fill;
    this.panel1.Location = new Point(0, 0);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(679, 437);
    this.panel1.TabIndex = 5;
    this.activityIDLbl.AutoSize = true;
    this.activityIDLbl.Location = new Point(15, 172);
    this.activityIDLbl.Name = "activityIDLbl";
    this.activityIDLbl.Size = new Size(69, 17);
    this.activityIDLbl.TabIndex = 18;
    this.activityIDLbl.Text = "Activity ID";
    this.activityIDLbl.Visible = false;
    this.showFormWhereActivityBack.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.showFormWhereActivityBack.AutoSize = true;
    this.showFormWhereActivityBack.ImeMode = ImeMode.NoControl;
    this.showFormWhereActivityBack.Location = new Point(96 /*0x60*/, 407);
    this.showFormWhereActivityBack.Name = "showFormWhereActivityBack";
    this.showFormWhereActivityBack.Size = new Size(357, 21);
    this.showFormWhereActivityBack.TabIndex = 0;
    this.showFormWhereActivityBack.Text = "Показывать форму при отправке действия назад";
    this.showFormWhereActivityBack.UseVisualStyleBackColor = true;
    this.CollectorCheckBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.CollectorCheckBox.AutoSize = true;
    this.CollectorCheckBox.ImeMode = ImeMode.NoControl;
    this.CollectorCheckBox.Location = new Point(96 /*0x60*/, 407);
    this.CollectorCheckBox.Name = "CollectorCheckBox";
    this.CollectorCheckBox.Size = new Size(101, 21);
    this.CollectorCheckBox.TabIndex = 17;
    this.CollectorCheckBox.Text = "Коллектор";
    this.Label1.AutoSize = true;
    this.Label1.ImeMode = ImeMode.NoControl;
    this.Label1.Location = new Point(10, 10);
    this.Label1.Name = "Label1";
    this.Label1.Size = new Size(76, 17);
    this.Label1.TabIndex = 16 /*0x10*/;
    this.Label1.Text = "Название:";
    this.Label1.TextAlign = ContentAlignment.MiddleLeft;
    this.NameEdit.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.NameEdit.Location = new Point(96 /*0x60*/, 10);
    this.NameEdit.MaxLength = 240 /*0xF0*/;
    this.NameEdit.Name = "NameEdit";
    this.NameEdit.Size = new Size(572, 22);
    this.NameEdit.TabIndex = 15;
    this.Label2.AutoSize = true;
    this.Label2.ImeMode = ImeMode.NoControl;
    this.Label2.Location = new Point(10, 42);
    this.Label2.Name = "Label2";
    this.Label2.Size = new Size(78, 17);
    this.Label2.TabIndex = 14;
    this.Label2.Text = "Описание:";
    this.ActImage.Image = (Image) componentResourceManager.GetObject("ActImage.Image");
    this.ActImage.ImeMode = ImeMode.NoControl;
    this.ActImage.Location = new Point(28, 103);
    this.ActImage.Name = "ActImage";
    this.ActImage.Size = new Size(38, 50);
    this.ActImage.TabIndex = 13;
    this.ActImage.TabStop = false;
    this.DescriptionMemo.AcceptsReturn = true;
    this.DescriptionMemo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.DescriptionMemo.Location = new Point(96 /*0x60*/, 38);
    this.DescriptionMemo.MaxLength = 1000;
    this.DescriptionMemo.Multiline = true;
    this.DescriptionMemo.Name = "DescriptionMemo";
    this.DescriptionMemo.Size = new Size(572, 360);
    this.DescriptionMemo.TabIndex = 12;
    this.AutoScaleDimensions = new SizeF(120f, 120f);
    this.AutoScaleMode = AutoScaleMode.Dpi;
    this.BackColor = SystemColors.ControlLightLight;
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.ActRuntimePanel);
    this.Name = nameof (GeneralSettingPageControl);
    this.Size = new Size(679, 504);
    this.ActRuntimePanel.ResumeLayout(false);
    this.ActRuntimePanel.PerformLayout();
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    ((ISupportInitialize) this.ActImage).EndInit();
    this.ResumeLayout(false);
  }
}
