// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ActivityPropertyPages.ArchiveSettingPageControl
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

#nullable disable
namespace Intermech.Workflow.Design.ActivityPropertyPages;

public class ArchiveSettingPageControl : UserControl
{
  private bool _readOnly;
  private ActivitySettings _settings;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private CheckBox DetachRegisteredCheckBox;
  private GroupBox RegRevGroupBox;
  private ButtonEdit RevArcIDEdit;
  private System.Windows.Forms.ComboBox RevArcVarCombo;
  private Panel RevSpacerPanel;
  private RadioButton RevArchiveVarRButton;
  private RadioButton RevArchiveRButton;
  private RadioButton RecArchiveCurrentRButton;
  private Panel RegRevSpacer;
  private GroupBox RegDocsGroupBox;
  private ButtonEdit DocArcIDEdit;
  private System.Windows.Forms.ComboBox DocArcVarCombo;
  private Panel DocSpacerPanel;
  private RadioButton DocArchiveVarRButton;
  private RadioButton DocArchiveRButton;
  private RadioButton DocArchiveCurrentRButton;
  private Panel panel11;
  private GroupBox RegisterGroupBox;
  private RadioButton ArcModeRadio2;
  private RadioButton ArcModeRadio1;
  private EnhToolTip ToolTip;

  public ArchiveSettingPageControl() => this.InitializeComponent();

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

  public bool LoadArchiveSettingPageControl(
    ActivitySettings settings,
    IDBObject activityObject,
    IUserSession activitySession)
  {
    bool flag = false;
    this._settings = settings;
    if (settings.ActivityType == wfConsts.RegisterTypeID)
    {
      VarList vars = new VarList(activitySession.GetObject(settings.ObjectIDwithVars), false, false);
      if (this.FillArcGroupBox(activitySession, activityObject, wfConsts.AttrDocArchiveID, vars, this.RegDocsGroupBox) != this.FillArcGroupBox(activitySession, activityObject, wfConsts.AttrRevArchiveID, vars, this.RegRevGroupBox))
      {
        this.ArcModeRadio2.Checked = true;
        this.RegRevGroupBox.Visible = true;
        this.RegRevSpacer.Visible = this.RegRevGroupBox.Visible;
      }
      this.DetachRegisteredCheckBox.Checked = settings.ActivityFlags.HasFlag((Enum) ActivityFlags.DetachRegisteredObjects);
    }
    else
      flag = true;
    return flag;
  }

  private void ArcModeRadioCheckedChanged(object sender, EventArgs e)
  {
    this.RegRevGroupBox.Visible = this.ArcModeRadio2.Checked;
    this.RegRevSpacer.Visible = this.RegRevGroupBox.Visible;
    if (!this.RegRevGroupBox.Visible)
      return;
    this.RegRevGroupBox.PerformLayout();
  }

  private void DocArcRadioChanged(object sender, EventArgs e)
  {
    int int32 = Convert.ToInt32(((Control) sender).Tag);
    this.SuspendLayout();
    try
    {
      this.DocArcIDEdit.Visible = int32 == 1;
      this.DocArcVarCombo.Visible = int32 == 2;
      this.DocSpacerPanel.Visible = int32 > 0;
    }
    finally
    {
      this.ResumeLayout();
    }
  }

  private void RevArcRadioChanged(object sender, EventArgs e)
  {
    int int32 = Convert.ToInt32(((Control) sender).Tag);
    this.SuspendLayout();
    try
    {
      this.RevArcIDEdit.Visible = int32 == 1;
      this.RevArcVarCombo.Visible = int32 == 2;
      this.RevSpacerPanel.Visible = int32 > 0;
    }
    finally
    {
      this.ResumeLayout();
    }
  }

  private void DocArcIDEdit_ButtonPressed(object sender, ButtonPressedEventArgs e)
  {
    if (!(sender is ButtonEdit buttonEdit))
      return;
    IDescriptor rootDescriptor = (IDescriptor) null;
    ServiceContainer nodesContext = new ServiceContainer();
    if (ApplicationServices.Container.GetService(typeof (IArchivesDescriptorService)) is IArchivesDescriptorService service)
    {
      rootDescriptor = service.GetDescriptor();
      object viewArchives = service.ViewArchives;
      nodesContext.AddService(viewArchives.GetType(), viewArchives);
    }
    long[] numArray = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Workflow.Design_18"), "", rootDescriptor, (System.IServiceProvider) nodesContext, SelectionOptions.Default);
    if (numArray == null || numArray.Length == 0)
      return;
    buttonEdit.Tag = (object) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ObjectSystemPropertiesEx systemPropertiesEx = sessionKeeper.Session.GetObjectSystemPropertiesEx(numArray[0], false);
      if (systemPropertiesEx != null)
      {
        buttonEdit.Text = systemPropertiesEx.Caption;
        buttonEdit.Tag = (object) systemPropertiesEx.VersionGuid;
      }
      else
        buttonEdit.Text = "???";
    }
  }

  private string FillArcGroupBox(
    IUserSession session,
    IDBObject activity,
    int attrID,
    VarList vars,
    GroupBox gb)
  {
    string g = "";
    System.Windows.Forms.ComboBox comboBox = (System.Windows.Forms.ComboBox) null;
    ButtonEdit buttonEdit = (ButtonEdit) null;
    RadioButton radioButton1 = (RadioButton) null;
    RadioButton radioButton2 = (RadioButton) null;
    RadioButton radioButton3 = (RadioButton) null;
    foreach (Control control in (ArrangedElementCollection) gb.Controls)
    {
      switch (control)
      {
        case RadioButton _:
          switch (Convert.ToInt32(control.Tag))
          {
            case 1:
              radioButton2 = (RadioButton) control;
              continue;
            case 2:
              radioButton3 = (RadioButton) control;
              continue;
            default:
              radioButton1 = (RadioButton) control;
              continue;
          }
        case System.Windows.Forms.ComboBox _:
          comboBox = (System.Windows.Forms.ComboBox) control;
          continue;
        case ButtonEdit _:
          buttonEdit = (ButtonEdit) control;
          continue;
        default:
          continue;
      }
    }
    if (comboBox != null && buttonEdit != null && radioButton3 != null)
    {
      foreach (Variable var in vars)
      {
        if (var.VarType == VarType.Archive)
          comboBox.Items.Add((object) var);
      }
      comboBox.SelectedIndex = 0;
      IDBAttribute attributeById = activity.GetAttributeByID(attrID);
      if (attributeById != null)
      {
        g = attributeById.AsString;
        if (g != "")
        {
          try
          {
            Guid guid = new Guid(g);
            QuickObjectInfo objectInfo = session.GetObjectInfo(guid);
            if (objectInfo.Empty)
            {
              IDBAttributeTypeInfo attributeType = ApplicationServices.Container.GetService(typeof (IClientMetadataCache)) is IClientMetadataCache service ? service.GetAttributeType(guid, false) : (IDBAttributeTypeInfo) null;
              if (attributeType != null)
              {
                int attributeId = attributeType.AttributeID;
                foreach (Variable var in vars)
                {
                  if (var.AttrTypeID == attributeId)
                  {
                    radioButton3.Checked = true;
                    comboBox.SelectedItem = (object) var;
                    break;
                  }
                }
              }
            }
            else
            {
              if (radioButton2 != null)
                radioButton2.Checked = true;
              buttonEdit.Text = objectInfo.Caption;
              buttonEdit.Tag = (object) objectInfo.VersionGuid;
            }
          }
          catch (Exception ex)
          {
            if (ApplicationServices.Container.GetService(typeof (IOutputView)) is IOutputView service)
              service.WriteString("Ошибки", "В процессе загрузки данных по архивам произошла ошибка: " + ex.Message);
          }
          return g;
        }
      }
    }
    if (radioButton2 != null && radioButton3 != null && radioButton1 != null && !radioButton2.Checked && !radioButton3.Checked)
      radioButton1.Checked = true;
    return g;
  }

  private bool SaveArcGroupBox(IDBObject activity, int attrID, GroupBox gb)
  {
    string str = string.Empty;
    IDBAttribute attributeById = activity.GetAttributeByID(attrID);
    if (attributeById != null)
      str = attributeById.AsString;
    if (string.IsNullOrEmpty(str))
      str = Guid.Empty.ToString();
    System.Windows.Forms.ComboBox comboBox = (System.Windows.Forms.ComboBox) null;
    ButtonEdit buttonEdit = (ButtonEdit) null;
    RadioButton radioButton1 = (RadioButton) null;
    RadioButton radioButton2 = (RadioButton) null;
    foreach (Control control in (ArrangedElementCollection) gb.Controls)
    {
      switch (control)
      {
        case RadioButton _:
          switch (Convert.ToInt32(control.Tag))
          {
            case 1:
              radioButton2 = (RadioButton) control;
              continue;
            case 2:
              continue;
            default:
              radioButton1 = (RadioButton) control;
              continue;
          }
        case System.Windows.Forms.ComboBox _:
          comboBox = (System.Windows.Forms.ComboBox) control;
          continue;
        case ButtonEdit _:
          buttonEdit = (ButtonEdit) control;
          continue;
        default:
          continue;
      }
    }
    if (radioButton1.Checked)
    {
      if (!string.IsNullOrEmpty(str))
      {
        if (attributeById != null)
          attributeById.AsString = string.Empty;
        return true;
      }
    }
    else if (radioButton2.Checked)
    {
      Guid guid = Guid.Empty;
      if (buttonEdit.Tag != null)
        guid = (Guid) buttonEdit.Tag;
      if (!str.Equals(guid.ToString()) && attributeById != null)
      {
        attributeById.AsString = guid.Equals(Guid.Empty) ? string.Empty : guid.ToString();
        return true;
      }
    }
    else if (comboBox.SelectedItem is Variable selectedItem)
    {
      if (!str.Equals(selectedItem.AttrTypeID.ToString()) && attributeById != null)
      {
        attributeById.AsString = selectedItem.AttrTypeGuid.ToString();
        return true;
      }
    }
    else if (!string.IsNullOrEmpty(str) && attributeById != null)
    {
      attributeById.AsString = string.Empty;
      return true;
    }
    return false;
  }

  public bool Save(IDBObject activityToSave, bool modified)
  {
    if (this.SaveArcGroupBox(activityToSave, wfConsts.AttrDocArchiveID, this.RegDocsGroupBox))
      modified = true;
    if (this.ArcModeRadio2.Checked)
    {
      if (this.SaveArcGroupBox(activityToSave, wfConsts.AttrRevArchiveID, this.RegRevGroupBox))
        modified = true;
    }
    else
    {
      IDBAttribute attributeById1 = activityToSave.GetAttributeByID(wfConsts.AttrRevArchiveID);
      IDBAttribute attributeById2 = activityToSave.GetAttributeByID(wfConsts.AttrDocArchiveID);
      if (attributeById1 != null && attributeById2 != null && attributeById1.AsString != attributeById2.AsString)
      {
        attributeById1.AsString = attributeById2.AsString;
        modified = true;
      }
    }
    if (this.DetachRegisteredCheckBox.Checked)
      this._settings.ActivityFlags |= ActivityFlags.DetachRegisteredObjects;
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
    this.DetachRegisteredCheckBox = new CheckBox();
    this.RegRevGroupBox = new GroupBox();
    this.RevArcIDEdit = new ButtonEdit();
    this.RevArcVarCombo = new System.Windows.Forms.ComboBox();
    this.RevSpacerPanel = new Panel();
    this.RevArchiveVarRButton = new RadioButton();
    this.RevArchiveRButton = new RadioButton();
    this.RecArchiveCurrentRButton = new RadioButton();
    this.RegRevSpacer = new Panel();
    this.RegDocsGroupBox = new GroupBox();
    this.DocArcIDEdit = new ButtonEdit();
    this.DocArcVarCombo = new System.Windows.Forms.ComboBox();
    this.DocSpacerPanel = new Panel();
    this.DocArchiveVarRButton = new RadioButton();
    this.DocArchiveRButton = new RadioButton();
    this.DocArchiveCurrentRButton = new RadioButton();
    this.panel11 = new Panel();
    this.RegisterGroupBox = new GroupBox();
    this.ArcModeRadio2 = new RadioButton();
    this.ArcModeRadio1 = new RadioButton();
    this.ToolTip = new EnhToolTip(this.components);
    this.RegRevGroupBox.SuspendLayout();
    this.RevArcIDEdit.Properties.BeginInit();
    this.RegDocsGroupBox.SuspendLayout();
    this.DocArcIDEdit.Properties.BeginInit();
    this.RegisterGroupBox.SuspendLayout();
    this.SuspendLayout();
    this.DetachRegisteredCheckBox.AutoSize = true;
    this.DetachRegisteredCheckBox.Dock = DockStyle.Top;
    this.DetachRegisteredCheckBox.ImeMode = ImeMode.NoControl;
    this.DetachRegisteredCheckBox.Location = new Point(0, 384);
    this.DetachRegisteredCheckBox.Name = "DetachRegisteredCheckBox";
    this.DetachRegisteredCheckBox.Padding = new Padding(0, 10, 0, 0);
    this.DetachRegisteredCheckBox.Size = new Size(674, 31 /*0x1F*/);
    this.DetachRegisteredCheckBox.TabIndex = 10;
    this.DetachRegisteredCheckBox.Text = "Откреплять успешно зарегистрированные документы";
    this.DetachRegisteredCheckBox.UseVisualStyleBackColor = true;
    this.RegRevGroupBox.AutoSize = true;
    this.RegRevGroupBox.Controls.Add((Control) this.RevArcIDEdit);
    this.RegRevGroupBox.Controls.Add((Control) this.RevArcVarCombo);
    this.RegRevGroupBox.Controls.Add((Control) this.RevSpacerPanel);
    this.RegRevGroupBox.Controls.Add((Control) this.RevArchiveVarRButton);
    this.RegRevGroupBox.Controls.Add((Control) this.RevArchiveRButton);
    this.RegRevGroupBox.Controls.Add((Control) this.RecArchiveCurrentRButton);
    this.RegRevGroupBox.Dock = DockStyle.Top;
    this.RegRevGroupBox.Location = new Point(0, 235);
    this.RegRevGroupBox.Name = "RegRevGroupBox";
    this.RegRevGroupBox.Padding = new Padding(7, 7, 7, 10);
    this.RegRevGroupBox.Size = new Size(674, 149);
    this.RegRevGroupBox.TabIndex = 9;
    this.RegRevGroupBox.TabStop = false;
    this.RegRevGroupBox.Text = "Зарегистрировать извещения в архиве:";
    this.RegRevGroupBox.Visible = false;
    this.RevArcIDEdit.Dock = DockStyle.Top;
    this.RevArcIDEdit.EditValue = (object) "???";
    this.RevArcIDEdit.Location = new Point(7, 117);
    this.RevArcIDEdit.Name = "RevArcIDEdit";
    this.RevArcIDEdit.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Ellipsis, "", 15, true, true, false, HorzAlignment.Center, (Image) null)
    });
    this.RevArcIDEdit.Properties.ReadOnly = true;
    this.RevArcIDEdit.Size = new Size(660, 22);
    this.RevArcIDEdit.TabIndex = 7;
    this.RevArcIDEdit.ButtonPressed += new ButtonPressedEventHandler(this.DocArcIDEdit_ButtonPressed);
    this.RevArcVarCombo.Dock = DockStyle.Top;
    this.RevArcVarCombo.DropDownStyle = ComboBoxStyle.DropDownList;
    this.RevArcVarCombo.Items.AddRange(new object[1]
    {
      (object) "(нет)"
    });
    this.RevArcVarCombo.Location = new Point(7, 93);
    this.RevArcVarCombo.Name = "RevArcVarCombo";
    this.RevArcVarCombo.Size = new Size(660, 24);
    this.RevArcVarCombo.TabIndex = 6;
    this.ToolTip.SetToolTip((Control) this.RevArcVarCombo, "Идентификатор архива берётся из переменной типа 'Архив'");
    this.RevArcVarCombo.Visible = false;
    this.RevSpacerPanel.Dock = DockStyle.Top;
    this.RevSpacerPanel.Location = new Point(7, 85);
    this.RevSpacerPanel.Name = "RevSpacerPanel";
    this.RevSpacerPanel.Padding = new Padding(0, 7, 0, 0);
    this.RevSpacerPanel.Size = new Size(660, 8);
    this.RevSpacerPanel.TabIndex = 2;
    this.RevArchiveVarRButton.AutoSize = true;
    this.RevArchiveVarRButton.Dock = DockStyle.Top;
    this.RevArchiveVarRButton.ImeMode = ImeMode.NoControl;
    this.RevArchiveVarRButton.Location = new Point(7, 64 /*0x40*/);
    this.RevArchiveVarRButton.Name = "RevArchiveVarRButton";
    this.RevArchiveVarRButton.Size = new Size(660, 21);
    this.RevArchiveVarRButton.TabIndex = 9;
    this.RevArchiveVarRButton.Tag = (object) "2";
    this.RevArchiveVarRButton.Text = "Взять имя архива из переменной...";
    this.RevArchiveVarRButton.CheckedChanged += new EventHandler(this.RevArcRadioChanged);
    this.RevArchiveRButton.AutoSize = true;
    this.RevArchiveRButton.Dock = DockStyle.Top;
    this.RevArchiveRButton.ImeMode = ImeMode.NoControl;
    this.RevArchiveRButton.Location = new Point(7, 43);
    this.RevArchiveRButton.Name = "RevArchiveRButton";
    this.RevArchiveRButton.Size = new Size(660, 21);
    this.RevArchiveRButton.TabIndex = 8;
    this.RevArchiveRButton.Tag = (object) "1";
    this.RevArchiveRButton.Text = "Явно указать имя архива...";
    this.RevArchiveRButton.CheckedChanged += new EventHandler(this.RevArcRadioChanged);
    this.RecArchiveCurrentRButton.AutoSize = true;
    this.RecArchiveCurrentRButton.Dock = DockStyle.Top;
    this.RecArchiveCurrentRButton.ImeMode = ImeMode.NoControl;
    this.RecArchiveCurrentRButton.Location = new Point(7, 22);
    this.RecArchiveCurrentRButton.Name = "RecArchiveCurrentRButton";
    this.RecArchiveCurrentRButton.Size = new Size(660, 21);
    this.RecArchiveCurrentRButton.TabIndex = 12;
    this.RecArchiveCurrentRButton.Tag = (object) "0";
    this.RecArchiveCurrentRButton.Text = "Текущий архив";
    this.RecArchiveCurrentRButton.CheckedChanged += new EventHandler(this.RevArcRadioChanged);
    this.RegRevSpacer.Dock = DockStyle.Top;
    this.RegRevSpacer.Location = new Point(0, 229);
    this.RegRevSpacer.Name = "RegRevSpacer";
    this.RegRevSpacer.Size = new Size(674, 6);
    this.RegRevSpacer.TabIndex = 11;
    this.RegRevSpacer.Visible = false;
    this.RegDocsGroupBox.AutoSize = true;
    this.RegDocsGroupBox.Controls.Add((Control) this.DocArcIDEdit);
    this.RegDocsGroupBox.Controls.Add((Control) this.DocArcVarCombo);
    this.RegDocsGroupBox.Controls.Add((Control) this.DocSpacerPanel);
    this.RegDocsGroupBox.Controls.Add((Control) this.DocArchiveVarRButton);
    this.RegDocsGroupBox.Controls.Add((Control) this.DocArchiveRButton);
    this.RegDocsGroupBox.Controls.Add((Control) this.DocArchiveCurrentRButton);
    this.RegDocsGroupBox.Dock = DockStyle.Top;
    this.RegDocsGroupBox.Location = new Point(0, 80 /*0x50*/);
    this.RegDocsGroupBox.Name = "RegDocsGroupBox";
    this.RegDocsGroupBox.Padding = new Padding(7, 7, 7, 10);
    this.RegDocsGroupBox.Size = new Size(674, 149);
    this.RegDocsGroupBox.TabIndex = 7;
    this.RegDocsGroupBox.TabStop = false;
    this.RegDocsGroupBox.Text = "Зарегистрировать документы в архиве:";
    this.DocArcIDEdit.Dock = DockStyle.Top;
    this.DocArcIDEdit.EditValue = (object) "???";
    this.DocArcIDEdit.Location = new Point(7, 117);
    this.DocArcIDEdit.Name = "DocArcIDEdit";
    this.DocArcIDEdit.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Ellipsis, "", 15, true, true, false, HorzAlignment.Center, (Image) null)
    });
    this.DocArcIDEdit.Properties.ReadOnly = true;
    this.DocArcIDEdit.Size = new Size(660, 22);
    this.DocArcIDEdit.TabIndex = 7;
    this.DocArcIDEdit.ButtonPressed += new ButtonPressedEventHandler(this.DocArcIDEdit_ButtonPressed);
    this.DocArcVarCombo.Dock = DockStyle.Top;
    this.DocArcVarCombo.DropDownStyle = ComboBoxStyle.DropDownList;
    this.DocArcVarCombo.Items.AddRange(new object[1]
    {
      (object) "(нет)"
    });
    this.DocArcVarCombo.Location = new Point(7, 93);
    this.DocArcVarCombo.Name = "DocArcVarCombo";
    this.DocArcVarCombo.Size = new Size(660, 24);
    this.DocArcVarCombo.TabIndex = 6;
    this.ToolTip.SetToolTip((Control) this.DocArcVarCombo, "Идентификатор архива берётся из переменной типа 'Архив'");
    this.DocArcVarCombo.Visible = false;
    this.DocSpacerPanel.Dock = DockStyle.Top;
    this.DocSpacerPanel.Location = new Point(7, 85);
    this.DocSpacerPanel.Name = "DocSpacerPanel";
    this.DocSpacerPanel.Size = new Size(660, 8);
    this.DocSpacerPanel.TabIndex = 10;
    this.DocArchiveVarRButton.AutoSize = true;
    this.DocArchiveVarRButton.Dock = DockStyle.Top;
    this.DocArchiveVarRButton.ImeMode = ImeMode.NoControl;
    this.DocArchiveVarRButton.Location = new Point(7, 64 /*0x40*/);
    this.DocArchiveVarRButton.Name = "DocArchiveVarRButton";
    this.DocArchiveVarRButton.Size = new Size(660, 21);
    this.DocArchiveVarRButton.TabIndex = 9;
    this.DocArchiveVarRButton.Tag = (object) "2";
    this.DocArchiveVarRButton.Text = "Взять имя архива из переменной...";
    this.DocArchiveVarRButton.CheckedChanged += new EventHandler(this.DocArcRadioChanged);
    this.DocArchiveRButton.AutoSize = true;
    this.DocArchiveRButton.Dock = DockStyle.Top;
    this.DocArchiveRButton.ImeMode = ImeMode.NoControl;
    this.DocArchiveRButton.Location = new Point(7, 43);
    this.DocArchiveRButton.Name = "DocArchiveRButton";
    this.DocArchiveRButton.Size = new Size(660, 21);
    this.DocArchiveRButton.TabIndex = 8;
    this.DocArchiveRButton.Tag = (object) "1";
    this.DocArchiveRButton.Text = "Явно указать имя архива...";
    this.DocArchiveRButton.CheckedChanged += new EventHandler(this.DocArcRadioChanged);
    this.DocArchiveCurrentRButton.AutoSize = true;
    this.DocArchiveCurrentRButton.Dock = DockStyle.Top;
    this.DocArchiveCurrentRButton.ImeMode = ImeMode.NoControl;
    this.DocArchiveCurrentRButton.Location = new Point(7, 22);
    this.DocArchiveCurrentRButton.Name = "DocArchiveCurrentRButton";
    this.DocArchiveCurrentRButton.Size = new Size(660, 21);
    this.DocArchiveCurrentRButton.TabIndex = 11;
    this.DocArchiveCurrentRButton.Tag = (object) "0";
    this.DocArchiveCurrentRButton.Text = "Текущий архив";
    this.DocArchiveCurrentRButton.CheckedChanged += new EventHandler(this.DocArcRadioChanged);
    this.panel11.Dock = DockStyle.Top;
    this.panel11.Location = new Point(0, 75);
    this.panel11.Name = "panel11";
    this.panel11.Size = new Size(674, 5);
    this.panel11.TabIndex = 12;
    this.RegisterGroupBox.Controls.Add((Control) this.ArcModeRadio2);
    this.RegisterGroupBox.Controls.Add((Control) this.ArcModeRadio1);
    this.RegisterGroupBox.Dock = DockStyle.Top;
    this.RegisterGroupBox.Location = new Point(0, 0);
    this.RegisterGroupBox.Name = "RegisterGroupBox";
    this.RegisterGroupBox.Padding = new Padding(7);
    this.RegisterGroupBox.Size = new Size(674, 75);
    this.RegisterGroupBox.TabIndex = 8;
    this.RegisterGroupBox.TabStop = false;
    this.RegisterGroupBox.Text = "Порядок регистрации";
    this.ArcModeRadio2.AutoSize = true;
    this.ArcModeRadio2.Dock = DockStyle.Top;
    this.ArcModeRadio2.ImeMode = ImeMode.NoControl;
    this.ArcModeRadio2.Location = new Point(7, 43);
    this.ArcModeRadio2.Name = "ArcModeRadio2";
    this.ArcModeRadio2.Size = new Size(660, 21);
    this.ArcModeRadio2.TabIndex = 3;
    this.ArcModeRadio2.Tag = (object) "2";
    this.ArcModeRadio2.Text = "Разные архивы для документов и извещений";
    this.ArcModeRadio2.CheckedChanged += new EventHandler(this.ArcModeRadioCheckedChanged);
    this.ArcModeRadio1.AutoSize = true;
    this.ArcModeRadio1.Checked = true;
    this.ArcModeRadio1.Dock = DockStyle.Top;
    this.ArcModeRadio1.ImeMode = ImeMode.NoControl;
    this.ArcModeRadio1.Location = new Point(7, 22);
    this.ArcModeRadio1.Name = "ArcModeRadio1";
    this.ArcModeRadio1.Size = new Size(660, 21);
    this.ArcModeRadio1.TabIndex = 2;
    this.ArcModeRadio1.TabStop = true;
    this.ArcModeRadio1.Tag = (object) "1";
    this.ArcModeRadio1.Text = "Один архив для документов и извещений";
    this.ArcModeRadio1.CheckedChanged += new EventHandler(this.ArcModeRadioCheckedChanged);
    this.ToolTip.AutoPopDelay = 3000;
    this.ToolTip.InitialDelay = 100;
    this.ToolTip.ReshowDelay = 100;
    this.AutoScaleDimensions = new SizeF(120f, 120f);
    this.AutoScaleMode = AutoScaleMode.Dpi;
    this.BackColor = SystemColors.ControlLightLight;
    this.Controls.Add((Control) this.DetachRegisteredCheckBox);
    this.Controls.Add((Control) this.RegRevGroupBox);
    this.Controls.Add((Control) this.RegRevSpacer);
    this.Controls.Add((Control) this.RegDocsGroupBox);
    this.Controls.Add((Control) this.panel11);
    this.Controls.Add((Control) this.RegisterGroupBox);
    this.Name = nameof (ArchiveSettingPageControl);
    this.Size = new Size(674, 448);
    this.RegRevGroupBox.ResumeLayout(false);
    this.RegRevGroupBox.PerformLayout();
    this.RevArcIDEdit.Properties.EndInit();
    this.RegDocsGroupBox.ResumeLayout(false);
    this.RegDocsGroupBox.PerformLayout();
    this.DocArcIDEdit.Properties.EndInit();
    this.RegisterGroupBox.ResumeLayout(false);
    this.RegisterGroupBox.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
