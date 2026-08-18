// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.ProjectPropsForm
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using Intermech.Expressions.Exceptions;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using Intermech.Project.Evaluator;
using Intermech.Workflow;
using Intermech.Workflow.Design;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

public class ProjectPropsForm : Form
{
  private readonly string _planningHintIntro;
  public EditingMode EditingMode;
  [CanBeNull]
  private Intermech.Project.Project _project;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TabControl _tabControl;
  private TabPage _tabPage1;
  private TabPage _displaySettingPage;
  private Panel _panel2;
  private Button _сancButton;
  private Button _okButton;
  private Label _label1;
  private DateTimePicker _startTimePicker;
  private ComboBox _planningComboBox;
  private Label _label6;
  private ComboBox _scheduleComboBox;
  private Label _label5;
  private Label _label3;
  private DateTimePicker _finishTimePicker;
  private Label _label2;
  private DateTimePicker _dateTimePicker2;
  private Label _label7;
  private NumericUpDown _priorityUpDown;
  private CheckBox _showProjectTaskCheckBox;
  private ComboBox _dateFormatBox;
  private Label _label8;
  private CheckBox _showCriticalCheckBox;
  private Label _spacerLabel;
  private CheckBox _requireTaskVerificationCheckBox;
  private CheckBox _enableMailNotificationsCheckBox;
  private TextBox _nameTextBox;
  private Label _nameLabel;
  private Label _label9;
  private ChiefEdit _chiefEdit;
  private Label _label4;
  private DateTimePicker _currentTimePicker;
  private Label _planningHintLabel;
  private CheckBox _showFactCheckBox2;
  private CheckBox _showFactCheckBox1;
  private Label _label10;
  private CheckBox _completeTasksOnProcessCheckBox;
  private EnhDataGridView _stylesView;
  private DataGridViewTextBoxColumn _whereColumn;
  private DataGridViewComboBoxColumn _fieldColumn;
  private GroupBox _groupBox1;
  private ComboBox _planningModeComboBox;
  private TabPage _syncPage;
  private Label _label11;
  private GroupBox _remoteSitesBox;
  private ComboBox _compoSitesCombo;
  private Label _label12;
  private ComboBox _propsSitesCombo;
  private AutoSizeLabel _syncNeededLabel;
  private Label _remoteStatusLabel;
  private CheckBox _showGridCheckBox;

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal TabControl TabControl
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._tabControl.CheckInitializedIn<TabControl>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal TabPage TabPage1
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._tabPage1.CheckInitializedIn<TabPage>((object) this);
    }
  }

  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal TabPage DisplaySettingPage
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._displaySettingPage;
    }
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] private set
    {
      this._displaySettingPage = value;
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Panel Panel2
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._panel2.CheckInitializedIn<Panel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button CancButton
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._сancButton.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button OkButton
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._okButton.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label Label1
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._label1.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal DateTimePicker StartTimePicker
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._startTimePicker.CheckInitializedIn<DateTimePicker>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal ComboBox PlanningComboBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._planningComboBox.CheckInitializedIn<ComboBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label Label6
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._label6.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal ComboBox ScheduleComboBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._scheduleComboBox.CheckInitializedIn<ComboBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label Label5
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._label5.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label Label3
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._label3.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal DateTimePicker FinishTimePicker
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._finishTimePicker.CheckInitializedIn<DateTimePicker>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label Label2
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._label2.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal DateTimePicker DateTimePicker2
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._dateTimePicker2.CheckInitializedIn<DateTimePicker>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label Label7
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._label7.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal NumericUpDown PriorityUpDown
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._priorityUpDown.CheckInitializedIn<NumericUpDown>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal CheckBox ShowProjectTaskCheckBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._showProjectTaskCheckBox.CheckInitializedIn<CheckBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal ComboBox DateFormatBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._dateFormatBox.CheckInitializedIn<ComboBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label Label8
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._label8.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal CheckBox ShowCriticalCheckBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._showCriticalCheckBox.CheckInitializedIn<CheckBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label SpacerLabel
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._spacerLabel.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal CheckBox RequireTaskVerificationCheckBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._requireTaskVerificationCheckBox.CheckInitializedIn<CheckBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal CheckBox EnableMailNotificationsCheckBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._enableMailNotificationsCheckBox.CheckInitializedIn<CheckBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal TextBox NameTextBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._nameTextBox.CheckInitializedIn<TextBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label NameLabel
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._nameLabel.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label Label9
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._label9.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal ChiefEdit ChiefEdit
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._chiefEdit.CheckInitializedIn<ChiefEdit>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label Label4
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._label4.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal DateTimePicker CurrentTimePicker
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._currentTimePicker.CheckInitializedIn<DateTimePicker>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label PlanningHintLabel
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._planningHintLabel.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal CheckBox ShowFactCheckBox2
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._showFactCheckBox2.CheckInitializedIn<CheckBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal CheckBox ShowFactCheckBox1
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._showFactCheckBox1.CheckInitializedIn<CheckBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label Label10
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._label10.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal CheckBox CompleteTasksOnProcessCheckBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._completeTasksOnProcessCheckBox.CheckInitializedIn<CheckBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal EnhDataGridView StylesView
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._stylesView.CheckInitializedIn<EnhDataGridView>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal DataGridViewTextBoxColumn WhereColumn
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._whereColumn.CheckInitializedIn<DataGridViewTextBoxColumn>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal DataGridViewComboBoxColumn FieldColumn
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._fieldColumn.CheckInitializedIn<DataGridViewComboBoxColumn>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal GroupBox GroupBox1
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._groupBox1.CheckInitializedIn<GroupBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal ComboBox PlanningModeComboBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._planningModeComboBox.CheckInitializedIn<ComboBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal TabPage SyncPage
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._syncPage.CheckInitializedIn<TabPage>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label Label11
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._label11.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal GroupBox RemoteSitesBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._remoteSitesBox.CheckInitializedIn<GroupBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal ComboBox CompoSitesCombo
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._compoSitesCombo.CheckInitializedIn<ComboBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label Label12
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._label12.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal ComboBox PropsSitesCombo
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._propsSitesCombo.CheckInitializedIn<ComboBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal AutoSizeLabel SyncNeededLabel
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._syncNeededLabel.CheckInitializedIn<AutoSizeLabel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label RemoteStatusLabel
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._remoteStatusLabel.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal CheckBox ShowGridCheckBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._showGridCheckBox.CheckInitializedIn<CheckBox>((object) this);
    }
  }

  public ProjectPropsForm()
  {
    this.InitializeComponent();
    this._planningHintIntro = this.PlanningHintLabel.Text;
  }

  public bool Edit([NotNull] Intermech.Project.Project p)
  {
    this._project = p;
    this.EditingMode = p.EditingMode;
    Intermech.Project.Project project = p;
    while (project.Project != null)
      project = project.Project;
    if (!(project is ClientProject clientProject))
      throw new InvalidArgumentException();
    this.NameTextBox.Text = p.Name;
    this.StartTimePicker.CustomFormat = clientProject.DisplayOptions.PickerDateFormat;
    this.StartTimePicker.Value = p.Start;
    this.FinishTimePicker.CustomFormat = clientProject.DisplayOptions.PickerDateFormat;
    this.FinishTimePicker.Value = p.Finish;
    ControlFuncs.EnumToCombo(this.PlanningComboBox, (Enum) p.PlanningType);
    this.ManualPlanning = p.ManualPlanning;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.ScheduleComboBox.DataSource = (object) ScheduleList.GetAllSchedules(sessionKeeper.Session);
      this.ScheduleComboBox.DisplayMember = "Value";
      this.ScheduleComboBox.ValueMember = "Key";
    }
    long objectId = p.Schedule.ObjectID;
    KeyValuePair<long, string> keyValuePair1 = new KeyValuePair<long, string>();
    foreach (KeyValuePair<long, string> keyValuePair2 in this.ScheduleComboBox.Items)
    {
      if (keyValuePair2.Key == objectId)
      {
        keyValuePair1 = keyValuePair2;
        break;
      }
    }
    this.ScheduleComboBox.SelectedItem = (object) keyValuePair1;
    this.PriorityUpDown.Value = (Decimal) p.Priority;
    this.ChiefEdit.FromTask((Task) p);
    this.EnableMailNotificationsCheckBox.Checked = p._Properties.EnableMailNotifications;
    this.RequireTaskVerificationCheckBox.Checked = p._Properties.RequireTaskVerification;
    this.CompleteTasksOnProcessCheckBox.Checked = p._Properties.CompleteTasksOnProcess;
    if (p == clientProject)
    {
      this.ShowProjectTaskCheckBox.Checked = clientProject.DisplayOptions.ShowProjectTask;
      this.ShowCriticalCheckBox.Checked = clientProject.DisplayOptions.HighlightCriticalTasks;
      this.ShowGridCheckBox.Checked = clientProject.DisplayOptions.ShowGrid;
      this.ShowFactCheckBox1.Checked = (clientProject.DisplayOptions.ShowFactDurations & FactDurationsDisplayMode.Executed) != 0;
      this.ShowFactCheckBox2.Checked = (clientProject.DisplayOptions.ShowFactDurations & FactDurationsDisplayMode.Completed) != 0;
      if (p.Status == Intermech.Project.TaskStatus.NotStarted)
      {
        this.ShowFactCheckBox1.Enabled = false;
        this.ShowFactCheckBox2.Enabled = false;
      }
      if (clientProject.DisplayOptions.View != null)
        this.CurrentTimePicker.Value = clientProject.DisplayOptions.View.CurrentDate;
      foreach (string dateFormat in DefaultDateFormats.DateFormats)
        this.DateFormatBox.Items.Add((object) new DateFormatItem(dateFormat));
      this.DateFormatBox.SelectedItem = (object) new DateFormatItem(clientProject.DisplayOptions.DateFormat);
    }
    else
    {
      if (this.DisplaySettingPage != null)
        this.TabControl.TabPages.Remove(this.DisplaySettingPage);
      this.DisplaySettingPage = (TabPage) null;
    }
    if (!Portal.Enabled)
      this.TabControl.TabPages.Remove(this.SyncPage);
    if (!this.EditingMode.HasProperties())
      ControlFuncs.SetControlsReadOnly((Control) this, true, ListFactory.Create<Control>((Control) this.DisplaySettingPage, (Control) this.ChiefEdit, (Control) this.SyncPage));
    if (!this.EditingMode.HasComposition())
      ControlFuncs.SetControlsReadOnly((Control) this.ChiefEdit, true);
    bool flag = this.ShowDialog() == DialogResult.OK;
    if (flag)
    {
      if (this.EditingMode.HasProperties())
      {
        p.Name = this.NameTextBox.Text;
        p.PlanningType = this.StartTimePicker.Enabled ? PlanningType.FromStart : PlanningType.FromEnd;
        p.ManualPlanning = this.ManualPlanning;
        if (this.StartTimePicker.Enabled)
          p.Start = this.StartTimePicker.Value;
        if (this.FinishTimePicker.Enabled)
          p.Finish = this.FinishTimePicker.Value;
        p.Priority = (int) this.PriorityUpDown.Value;
        long key = this.ScheduleComboBox.SelectedItem != null ? ((KeyValuePair<long, string>) this.ScheduleComboBox.SelectedItem).Key : 0L;
        if (key != 0L && key != p.Schedule.ObjectID)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
            p.Schedule = ScheduleList.GetSchedule(key, sessionKeeper.Session);
        }
      }
      if (this.EditingMode.HasComposition())
        this.ChiefEdit.ToTask((Task) p);
      p._Properties.EnableMailNotifications = this.EnableMailNotificationsCheckBox.Checked;
      p._Properties.RequireTaskVerification = this.RequireTaskVerificationCheckBox.Checked;
      p._Properties.CompleteTasksOnProcess = this.CompleteTasksOnProcessCheckBox.Checked;
      if (this.DisplaySettingPage != null)
      {
        clientProject.DisplayOptions.ShowProjectTask = this.ShowProjectTaskCheckBox.Checked;
        clientProject.DisplayOptions.HighlightCriticalTasks = this.ShowCriticalCheckBox.Checked;
        clientProject.DisplayOptions.ShowGrid = this.ShowGridCheckBox.Checked;
        FactDurationsDisplayMode durationsDisplayMode = FactDurationsDisplayMode.None;
        if (this.ShowFactCheckBox1.Checked)
          durationsDisplayMode |= FactDurationsDisplayMode.Executed;
        if (this.ShowFactCheckBox2.Checked)
          durationsDisplayMode |= FactDurationsDisplayMode.Completed;
        clientProject.DisplayOptions.ShowFactDurations = durationsDisplayMode;
        if (this.DateFormatBox.SelectedItem is DateFormatItem selectedItem)
          clientProject.DisplayOptions.DateFormat = selectedItem.Format;
        if (clientProject.DisplayOptions.View != null)
          clientProject.DisplayOptions.View.CurrentDate = this.CurrentTimePicker.Value;
        if (this.DisplaySettingPage.Tag != null)
        {
          TaskCaptions taskCaptions = clientProject.DisplayOptions.TaskCaptions;
          taskCaptions.Modified = false;
          taskCaptions[DockStyle.Left] = this.StylesView.Rows[0].Cells[1].Value as PropInfo;
          taskCaptions[DockStyle.Right] = this.StylesView.Rows[1].Cells[1].Value as PropInfo;
          taskCaptions[DockStyle.Top] = this.StylesView.Rows[2].Cells[1].Value as PropInfo;
          taskCaptions[DockStyle.Bottom] = this.StylesView.Rows[3].Cells[1].Value as PropInfo;
        }
      }
      if (this.SyncPage.Tag != null)
      {
        SiteID siteId = new SiteID(p.CurrentSiteID);
        SiteInfo selectedItem1 = this.PropsSitesCombo.SelectedItem as SiteInfo;
        siteId.Owner = selectedItem1.Code;
        SiteInfo selectedItem2 = this.CompoSitesCombo.SelectedItem as SiteInfo;
        siteId.CompositionOwner = selectedItem2.Code;
        p.SiteID = siteId.Value;
      }
      clientProject.DisplayOptions.UpdateControls();
    }
    return flag;
  }

  public static bool Show([NotNull] Intermech.Project.Project p)
  {
    using (ProjectPropsForm projectPropsForm = new ProjectPropsForm())
      return projectPropsForm.Edit(p);
  }

  private void PlanningComboBox_SelectedValueChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (!this.EditingMode.HasProperties() || !(this.PlanningComboBox.SelectedItem is KeyValuePair<Enum, string> selectedItem))
      return;
    this.StartTimePicker.Enabled = this.ManualPlanning || (PlanningType) selectedItem.Key == PlanningType.FromStart;
    this.FinishTimePicker.Enabled = this.ManualPlanning || !this.StartTimePicker.Enabled;
  }

  private void PlanningComboBox_SelectedIndexChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    string s = Localization.GetString(this.PlanningComboBox.SelectedIndex == 0 ? "AsSoonAsPossible" : "AsLateAsPossible");
    if (s.Length > 0)
      s = StringFuncs.ReplaceChar(s, 0, char.ToLower(s[0]));
    this.PlanningHintLabel.Text = $"{this._planningHintIntro} {s}";
  }

  private void AddStyleRow(DockStyle ds, [NotNull] string caption)
  {
    if (!(this._project is ClientProject project))
      return;
    PropInfo taskCaption = project.DisplayOptions.TaskCaptions[ds];
    this.StylesView.Rows.Add((object) caption, (object) taskCaption);
  }

  private void tabControl_SelectedIndexChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this.TabControl.SelectedTab == this.DisplaySettingPage && this.DisplaySettingPage?.Tag == null)
    {
      this.FieldColumn.ValueType = typeof (PropInfo);
      this.FieldColumn.Items.Add((object) string.Empty);
      this.FieldColumn.Items.AddRange((object[]) PropInfos.All.ToArray());
      this.StylesView.ColumnHeadersVisible = false;
      DataGridViewCellStyle defaultCellStyle = this.StylesView.RowHeadersDefaultCellStyle;
      defaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      this.StylesView.Columns[0].DefaultCellStyle = defaultCellStyle;
      this.StylesView.Columns[0].DefaultCellStyle.SelectionBackColor = defaultCellStyle.BackColor;
      this.StylesView.Columns[0].DefaultCellStyle.SelectionForeColor = defaultCellStyle.ForeColor;
      this.AddStyleRow(DockStyle.Left, "Слева");
      this.AddStyleRow(DockStyle.Right, "Справа");
      this.AddStyleRow(DockStyle.Top, "Сверху");
      this.AddStyleRow(DockStyle.Bottom, "Снизу");
      int num = 0;
      foreach (DataGridViewRow row in (IEnumerable) this.StylesView.Rows)
        num += row.Height;
      this.StylesView.Height = num + 3;
      if (this.DisplaySettingPage == null)
        return;
      this.DisplaySettingPage.Tag = (object) 1;
    }
    else
    {
      if (this.TabControl.SelectedTab != this.SyncPage || this.SyncPage.Tag != null || this._project == null)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        ISitesCacheService customService = sessionKeeper.Session.GetCustomService<ISitesCacheService>(false);
        SiteID siteId1 = new SiteID(this._project.CurrentSiteID);
        SiteID siteId2 = new SiteID(this._project.SiteID);
        ProjectPropsForm.FillSites(customService, this.PropsSitesCombo, this.EditingMode.HasProperties() ? siteId1.Owner : siteId2.Owner, this.EditingMode.HasProperties());
        ProjectPropsForm.FillSites(customService, this.CompoSitesCombo, this.EditingMode.HasComposition() ? siteId1.CompositionOwner : siteId2.CompositionOwner, this.EditingMode.HasComposition());
        this.SyncNeededLabel.Visible = this._project.SyncPending;
        if (this._project.SyncPending)
          this.SyncNeededLabel.StatusImage = Images.SyncPendingImage;
        else
          this.RemoteSitesBox.Height = this.SyncNeededLabel.Top;
        string str = string.Empty;
        RemoteProcessStatus remoteStatus = this._project.RemoteStatus;
        if (remoteStatus != RemoteProcessStatus.Published)
          str = SimpleFuncs.GetEnumDescription((Enum) remoteStatus);
        this.RemoteStatusLabel.Visible = str != string.Empty;
        if (str != string.Empty)
          this.RemoteStatusLabel.Text += str;
        if (this._project.SyncPending && str == string.Empty)
          this.RemoteSitesBox.Height = this.RemoteStatusLabel.Top + 10;
        if (!this._project.SyncPending)
        {
          if (str != string.Empty)
          {
            this.RemoteStatusLabel.Top = this.SyncNeededLabel.Top;
            this.RemoteSitesBox.Height = this.RemoteStatusLabel.Bottom + 10;
          }
        }
      }
      this.SyncPage.Tag = (object) 1;
    }
  }

  private static void FillSites(
    [CanBeNull] ISitesCacheService srv,
    [NotNull] ComboBox combo,
    char selectedCode,
    bool enable)
  {
    if (srv?.Info == null)
      return;
    SiteInfo siteInfo1 = (SiteInfo) null;
    SiteInfo siteInfo2 = (SiteInfo) null;
    foreach (SiteInfo site in srv.Sites)
    {
      if (site.ID == srv.Info.ID)
      {
        siteInfo1 = (SiteInfo) new CurrentSiteInfo(site);
        combo.Items.Add((object) siteInfo1);
      }
      else
        combo.Items.Add((object) site);
      if ((int) site.Code == (int) selectedCode)
        siteInfo2 = site;
    }
    if (siteInfo2 != null && siteInfo1 != null && (int) siteInfo2.Code == (int) siteInfo1.Code)
      siteInfo2 = siteInfo1;
    combo.Sorted = true;
    combo.SelectedItem = (object) (siteInfo2 ?? siteInfo1);
    combo.Enabled = enable;
  }

  private void StylesView_ComboSelectedIndexChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.StylesView.CurrentCell.Value = ((ComboBox) sender).SelectedItem;
  }

  private static void StylesView_ComboDropDown([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    ((Control) sender).BackColor = SystemColors.Window;
  }

  private void StylesView_EditingControlShowing(
    [CanBeNull] object sender,
    [NotNull] DataGridViewEditingControlShowingEventArgs e)
  {
    if (!(e.Control is ComboBox control))
      return;
    control.SelectedIndexChanged -= new EventHandler(this.StylesView_ComboSelectedIndexChanged);
    control.SelectedIndexChanged += new EventHandler(this.StylesView_ComboSelectedIndexChanged);
    control.DropDown += new EventHandler(ProjectPropsForm.StylesView_ComboDropDown);
    control.GotFocus += new EventHandler(ProjectPropsForm.StylesView_ComboDropDown);
  }

  private void StylesView_CellParsing([CanBeNull] object sender, [NotNull] DataGridViewCellParsingEventArgs e)
  {
    e.Value = this.StylesView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
    e.ParsingApplied = true;
  }

  private void StylesView_DataError([CanBeNull] object sender, [NotNull] DataGridViewDataErrorEventArgs e)
  {
  }

  private bool ManualPlanning
  {
    get => this.PlanningModeComboBox.SelectedIndex == 1;
    set => this.PlanningModeComboBox.SelectedIndex = value ? 1 : 0;
  }

  private void PlanningModeComboBox_SelectedIndexChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.PlanningComboBox.Enabled = !this.ManualPlanning;
    if (!this.ManualPlanning)
      return;
    this.PlanningComboBox.SelectedIndex = 0;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ProjectPropsForm));
    this._tabControl = new TabControl();
    this._tabPage1 = new TabPage();
    this._planningModeComboBox = new ComboBox();
    this._completeTasksOnProcessCheckBox = new CheckBox();
    this._planningHintLabel = new Label();
    this._chiefEdit = new ChiefEdit();
    this._label9 = new Label();
    this._nameTextBox = new TextBox();
    this._nameLabel = new Label();
    this._spacerLabel = new Label();
    this._requireTaskVerificationCheckBox = new CheckBox();
    this._enableMailNotificationsCheckBox = new CheckBox();
    this._label7 = new Label();
    this._priorityUpDown = new NumericUpDown();
    this._planningComboBox = new ComboBox();
    this._label6 = new Label();
    this._scheduleComboBox = new ComboBox();
    this._label5 = new Label();
    this._label3 = new Label();
    this._finishTimePicker = new DateTimePicker();
    this._label1 = new Label();
    this._startTimePicker = new DateTimePicker();
    this._displaySettingPage = new TabPage();
    this._showGridCheckBox = new CheckBox();
    this._groupBox1 = new GroupBox();
    this._stylesView = new EnhDataGridView();
    this._whereColumn = new DataGridViewTextBoxColumn();
    this._fieldColumn = new DataGridViewComboBoxColumn();
    this._showFactCheckBox2 = new CheckBox();
    this._showFactCheckBox1 = new CheckBox();
    this._label10 = new Label();
    this._label4 = new Label();
    this._currentTimePicker = new DateTimePicker();
    this._showCriticalCheckBox = new CheckBox();
    this._dateFormatBox = new ComboBox();
    this._label8 = new Label();
    this._showProjectTaskCheckBox = new CheckBox();
    this._syncPage = new TabPage();
    this._remoteSitesBox = new GroupBox();
    this._remoteStatusLabel = new Label();
    this._syncNeededLabel = new AutoSizeLabel();
    this._compoSitesCombo = new ComboBox();
    this._label12 = new Label();
    this._propsSitesCombo = new ComboBox();
    this._label11 = new Label();
    this._panel2 = new Panel();
    this._сancButton = new Button();
    this._okButton = new Button();
    this._label2 = new Label();
    this._dateTimePicker2 = new DateTimePicker();
    this._tabControl.SuspendLayout();
    this._tabPage1.SuspendLayout();
    this._priorityUpDown.BeginInit();
    this._displaySettingPage.SuspendLayout();
    this._groupBox1.SuspendLayout();
    ((ISupportInitialize) this._stylesView).BeginInit();
    this._syncPage.SuspendLayout();
    this._remoteSitesBox.SuspendLayout();
    this._panel2.SuspendLayout();
    this.SuspendLayout();
    this._tabControl.Controls.Add((Control) this._tabPage1);
    this._tabControl.Controls.Add((Control) this._displaySettingPage);
    this._tabControl.Controls.Add((Control) this._syncPage);
    componentResourceManager.ApplyResources((object) this._tabControl, "_tabControl");
    this._tabControl.Name = "_tabControl";
    this._tabControl.SelectedIndex = 0;
    this._tabControl.SelectedIndexChanged += new EventHandler(this.tabControl_SelectedIndexChanged);
    this._tabPage1.Controls.Add((Control) this._planningModeComboBox);
    this._tabPage1.Controls.Add((Control) this._completeTasksOnProcessCheckBox);
    this._tabPage1.Controls.Add((Control) this._planningHintLabel);
    this._tabPage1.Controls.Add((Control) this._chiefEdit);
    this._tabPage1.Controls.Add((Control) this._label9);
    this._tabPage1.Controls.Add((Control) this._nameTextBox);
    this._tabPage1.Controls.Add((Control) this._nameLabel);
    this._tabPage1.Controls.Add((Control) this._spacerLabel);
    this._tabPage1.Controls.Add((Control) this._requireTaskVerificationCheckBox);
    this._tabPage1.Controls.Add((Control) this._enableMailNotificationsCheckBox);
    this._tabPage1.Controls.Add((Control) this._label7);
    this._tabPage1.Controls.Add((Control) this._priorityUpDown);
    this._tabPage1.Controls.Add((Control) this._planningComboBox);
    this._tabPage1.Controls.Add((Control) this._label6);
    this._tabPage1.Controls.Add((Control) this._scheduleComboBox);
    this._tabPage1.Controls.Add((Control) this._label5);
    this._tabPage1.Controls.Add((Control) this._label3);
    this._tabPage1.Controls.Add((Control) this._finishTimePicker);
    this._tabPage1.Controls.Add((Control) this._label1);
    this._tabPage1.Controls.Add((Control) this._startTimePicker);
    componentResourceManager.ApplyResources((object) this._tabPage1, "_tabPage1");
    this._tabPage1.Name = "_tabPage1";
    this._tabPage1.UseVisualStyleBackColor = true;
    this._planningModeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._planningModeComboBox.FormattingEnabled = true;
    this._planningModeComboBox.Items.AddRange(new object[2]
    {
      (object) componentResourceManager.GetString("PlanningModeComboBox.Items"),
      (object) componentResourceManager.GetString("PlanningModeComboBox.Items1")
    });
    componentResourceManager.ApplyResources((object) this._planningModeComboBox, "_planningModeComboBox");
    this._planningModeComboBox.Name = "_planningModeComboBox";
    this._planningModeComboBox.SelectedIndexChanged += new EventHandler(this.PlanningModeComboBox_SelectedIndexChanged);
    this._planningModeComboBox.SelectedValueChanged += new EventHandler(this.PlanningComboBox_SelectedValueChanged);
    componentResourceManager.ApplyResources((object) this._completeTasksOnProcessCheckBox, "_completeTasksOnProcessCheckBox");
    this._completeTasksOnProcessCheckBox.Name = "_completeTasksOnProcessCheckBox";
    this._completeTasksOnProcessCheckBox.UseVisualStyleBackColor = true;
    this._planningHintLabel.ForeColor = SystemColors.ControlDarkDark;
    componentResourceManager.ApplyResources((object) this._planningHintLabel, "_planningHintLabel");
    this._planningHintLabel.Name = "_planningHintLabel";
    this._chiefEdit.AllowDel = false;
    this._chiefEdit.Inherited = false;
    componentResourceManager.ApplyResources((object) this._chiefEdit, "_chiefEdit");
    this._chiefEdit.Name = "_chiefEdit";
    this._label9.BorderStyle = BorderStyle.Fixed3D;
    componentResourceManager.ApplyResources((object) this._label9, "_label9");
    this._label9.Name = "_label9";
    componentResourceManager.ApplyResources((object) this._nameTextBox, "_nameTextBox");
    this._nameTextBox.Name = "_nameTextBox";
    componentResourceManager.ApplyResources((object) this._nameLabel, "_nameLabel");
    this._nameLabel.Name = "_nameLabel";
    this._spacerLabel.BorderStyle = BorderStyle.Fixed3D;
    componentResourceManager.ApplyResources((object) this._spacerLabel, "_spacerLabel");
    this._spacerLabel.Name = "_spacerLabel";
    componentResourceManager.ApplyResources((object) this._requireTaskVerificationCheckBox, "_requireTaskVerificationCheckBox");
    this._requireTaskVerificationCheckBox.Name = "_requireTaskVerificationCheckBox";
    this._requireTaskVerificationCheckBox.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._enableMailNotificationsCheckBox, "_enableMailNotificationsCheckBox");
    this._enableMailNotificationsCheckBox.Name = "_enableMailNotificationsCheckBox";
    this._enableMailNotificationsCheckBox.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._label7, "_label7");
    this._label7.Name = "_label7";
    componentResourceManager.ApplyResources((object) this._priorityUpDown, "_priorityUpDown");
    this._priorityUpDown.Maximum = new Decimal(new int[4]
    {
      1000,
      0,
      0,
      0
    });
    this._priorityUpDown.Name = "_priorityUpDown";
    this._planningComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._planningComboBox.FormattingEnabled = true;
    componentResourceManager.ApplyResources((object) this._planningComboBox, "_planningComboBox");
    this._planningComboBox.Name = "_planningComboBox";
    this._planningComboBox.SelectedIndexChanged += new EventHandler(this.PlanningComboBox_SelectedIndexChanged);
    this._planningComboBox.SelectedValueChanged += new EventHandler(this.PlanningComboBox_SelectedValueChanged);
    componentResourceManager.ApplyResources((object) this._label6, "_label6");
    this._label6.Name = "_label6";
    this._scheduleComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._scheduleComboBox.FormattingEnabled = true;
    componentResourceManager.ApplyResources((object) this._scheduleComboBox, "_scheduleComboBox");
    this._scheduleComboBox.Name = "_scheduleComboBox";
    this._scheduleComboBox.Sorted = true;
    componentResourceManager.ApplyResources((object) this._label5, "_label5");
    this._label5.Name = "_label5";
    componentResourceManager.ApplyResources((object) this._label3, "_label3");
    this._label3.Name = "_label3";
    componentResourceManager.ApplyResources((object) this._finishTimePicker, "_finishTimePicker");
    this._finishTimePicker.Format = DateTimePickerFormat.Custom;
    this._finishTimePicker.Name = "_finishTimePicker";
    componentResourceManager.ApplyResources((object) this._label1, "_label1");
    this._label1.Name = "_label1";
    this._startTimePicker.Cursor = Cursors.Default;
    componentResourceManager.ApplyResources((object) this._startTimePicker, "_startTimePicker");
    this._startTimePicker.Format = DateTimePickerFormat.Custom;
    this._startTimePicker.Name = "_startTimePicker";
    this._displaySettingPage.Controls.Add((Control) this._showGridCheckBox);
    this._displaySettingPage.Controls.Add((Control) this._groupBox1);
    this._displaySettingPage.Controls.Add((Control) this._showFactCheckBox2);
    this._displaySettingPage.Controls.Add((Control) this._showFactCheckBox1);
    this._displaySettingPage.Controls.Add((Control) this._label10);
    this._displaySettingPage.Controls.Add((Control) this._label4);
    this._displaySettingPage.Controls.Add((Control) this._currentTimePicker);
    this._displaySettingPage.Controls.Add((Control) this._showCriticalCheckBox);
    this._displaySettingPage.Controls.Add((Control) this._dateFormatBox);
    this._displaySettingPage.Controls.Add((Control) this._label8);
    this._displaySettingPage.Controls.Add((Control) this._showProjectTaskCheckBox);
    componentResourceManager.ApplyResources((object) this._displaySettingPage, "_displaySettingPage");
    this._displaySettingPage.Name = "_displaySettingPage";
    this._displaySettingPage.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._showGridCheckBox, "_showGridCheckBox");
    this._showGridCheckBox.Name = "_showGridCheckBox";
    this._showGridCheckBox.UseVisualStyleBackColor = true;
    this._groupBox1.Controls.Add((Control) this._stylesView);
    componentResourceManager.ApplyResources((object) this._groupBox1, "_groupBox1");
    this._groupBox1.Name = "_groupBox1";
    this._groupBox1.TabStop = false;
    this._stylesView.AllowUserToAddRows = false;
    this._stylesView.AllowUserToDeleteRows = false;
    this._stylesView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
    this._stylesView.BackgroundColor = SystemColors.Window;
    this._stylesView.BorderStyle = BorderStyle.Fixed3D;
    this._stylesView.CellBorderStyle = DataGridViewCellBorderStyle.None;
    this._stylesView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this._stylesView.Columns.AddRange((DataGridViewColumn) this._whereColumn, (DataGridViewColumn) this._fieldColumn);
    componentResourceManager.ApplyResources((object) this._stylesView, "_stylesView");
    this._stylesView.EnableHeadersVisualStyles = false;
    this._stylesView.Name = "_stylesView";
    this._stylesView.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
    this._stylesView.RowHeadersVisible = false;
    this._stylesView.ShowEditingIcon = false;
    this._stylesView.CellParsing += new DataGridViewCellParsingEventHandler(this.StylesView_CellParsing);
    this._stylesView.DataError += new DataGridViewDataErrorEventHandler(this.StylesView_DataError);
    this._stylesView.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(this.StylesView_EditingControlShowing);
    componentResourceManager.ApplyResources((object) this._whereColumn, "_whereColumn");
    this._whereColumn.Name = "_whereColumn";
    this._whereColumn.ReadOnly = true;
    this._fieldColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    this._fieldColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
    this._fieldColumn.DisplayStyleForCurrentCellOnly = true;
    componentResourceManager.ApplyResources((object) this._fieldColumn, "_fieldColumn");
    this._fieldColumn.Name = "_fieldColumn";
    this._fieldColumn.Resizable = DataGridViewTriState.True;
    this._fieldColumn.SortMode = DataGridViewColumnSortMode.Automatic;
    componentResourceManager.ApplyResources((object) this._showFactCheckBox2, "_showFactCheckBox2");
    this._showFactCheckBox2.Name = "_showFactCheckBox2";
    this._showFactCheckBox2.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._showFactCheckBox1, "_showFactCheckBox1");
    this._showFactCheckBox1.Name = "_showFactCheckBox1";
    this._showFactCheckBox1.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._label10, "_label10");
    this._label10.Name = "_label10";
    componentResourceManager.ApplyResources((object) this._label4, "_label4");
    this._label4.Name = "_label4";
    this._currentTimePicker.Format = DateTimePickerFormat.Custom;
    componentResourceManager.ApplyResources((object) this._currentTimePicker, "_currentTimePicker");
    this._currentTimePicker.Name = "_currentTimePicker";
    componentResourceManager.ApplyResources((object) this._showCriticalCheckBox, "_showCriticalCheckBox");
    this._showCriticalCheckBox.Name = "_showCriticalCheckBox";
    this._showCriticalCheckBox.UseVisualStyleBackColor = true;
    this._dateFormatBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._dateFormatBox.FormattingEnabled = true;
    componentResourceManager.ApplyResources((object) this._dateFormatBox, "_dateFormatBox");
    this._dateFormatBox.Name = "_dateFormatBox";
    componentResourceManager.ApplyResources((object) this._label8, "_label8");
    this._label8.Name = "_label8";
    componentResourceManager.ApplyResources((object) this._showProjectTaskCheckBox, "_showProjectTaskCheckBox");
    this._showProjectTaskCheckBox.Name = "_showProjectTaskCheckBox";
    this._showProjectTaskCheckBox.UseVisualStyleBackColor = true;
    this._syncPage.Controls.Add((Control) this._remoteSitesBox);
    componentResourceManager.ApplyResources((object) this._syncPage, "_syncPage");
    this._syncPage.Name = "_syncPage";
    this._syncPage.UseVisualStyleBackColor = true;
    this._remoteSitesBox.Controls.Add((Control) this._remoteStatusLabel);
    this._remoteSitesBox.Controls.Add((Control) this._syncNeededLabel);
    this._remoteSitesBox.Controls.Add((Control) this._compoSitesCombo);
    this._remoteSitesBox.Controls.Add((Control) this._label12);
    this._remoteSitesBox.Controls.Add((Control) this._propsSitesCombo);
    this._remoteSitesBox.Controls.Add((Control) this._label11);
    componentResourceManager.ApplyResources((object) this._remoteSitesBox, "_remoteSitesBox");
    this._remoteSitesBox.Name = "_remoteSitesBox";
    this._remoteSitesBox.TabStop = false;
    componentResourceManager.ApplyResources((object) this._remoteStatusLabel, "_remoteStatusLabel");
    this._remoteStatusLabel.Name = "_remoteStatusLabel";
    componentResourceManager.ApplyResources((object) this._syncNeededLabel, "_syncNeededLabel");
    this._syncNeededLabel.Name = "_syncNeededLabel";
    this._compoSitesCombo.DropDownStyle = ComboBoxStyle.DropDownList;
    this._compoSitesCombo.FormattingEnabled = true;
    componentResourceManager.ApplyResources((object) this._compoSitesCombo, "_compoSitesCombo");
    this._compoSitesCombo.Name = "_compoSitesCombo";
    componentResourceManager.ApplyResources((object) this._label12, "_label12");
    this._label12.Name = "_label12";
    this._propsSitesCombo.DropDownStyle = ComboBoxStyle.DropDownList;
    this._propsSitesCombo.FormattingEnabled = true;
    componentResourceManager.ApplyResources((object) this._propsSitesCombo, "_propsSitesCombo");
    this._propsSitesCombo.Name = "_propsSitesCombo";
    componentResourceManager.ApplyResources((object) this._label11, "_label11");
    this._label11.Name = "_label11";
    this._panel2.BackColor = Color.Transparent;
    this._panel2.Controls.Add((Control) this._сancButton);
    this._panel2.Controls.Add((Control) this._okButton);
    componentResourceManager.ApplyResources((object) this._panel2, "_panel2");
    this._panel2.Name = "_panel2";
    componentResourceManager.ApplyResources((object) this._сancButton, "_сancButton");
    this._сancButton.DialogResult = DialogResult.Cancel;
    this._сancButton.Name = "_сancButton";
    componentResourceManager.ApplyResources((object) this._okButton, "_okButton");
    this._okButton.DialogResult = DialogResult.OK;
    this._okButton.Name = "_okButton";
    componentResourceManager.ApplyResources((object) this._label2, "_label2");
    this._label2.Name = "_label2";
    componentResourceManager.ApplyResources((object) this._dateTimePicker2, "_dateTimePicker2");
    this._dateTimePicker2.Format = DateTimePickerFormat.Custom;
    this._dateTimePicker2.Name = "_dateTimePicker2";
    this.AcceptButton = (IButtonControl) this._okButton;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._сancButton;
    this.Controls.Add((Control) this._tabControl);
    this.Controls.Add((Control) this._panel2);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ProjectPropsForm);
    this.ShowInTaskbar = false;
    this._tabControl.ResumeLayout(false);
    this._tabPage1.ResumeLayout(false);
    this._tabPage1.PerformLayout();
    this._priorityUpDown.EndInit();
    this._displaySettingPage.ResumeLayout(false);
    this._displaySettingPage.PerformLayout();
    this._groupBox1.ResumeLayout(false);
    ((ISupportInitialize) this._stylesView).EndInit();
    this._syncPage.ResumeLayout(false);
    this._remoteSitesBox.ResumeLayout(false);
    this._remoteSitesBox.PerformLayout();
    this._panel2.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
