// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.UserTaskView
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Bars;
using Intermech.Client.Core.Organizer;
using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Metadata;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Project.Controls.Properties;
using Intermech.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

[ViewDescriptionProvider(typeof (UserTaskView.UserTaskViewDescriptionProvider))]
public class UserTaskView : SimpleBaseUserControl, IView
{
  private bool _shown;
  [NotNull]
  private readonly string _userPrefix;
  [NotNull]
  private readonly string _chiefPrefix;
  [CanBeNull]
  private (int TypeID, long ObjectID)? _selectedObject;
  private long _taskID;
  private long _messageID;
  [CanBeNull]
  private string _managerAnswer;
  [NotNull]
  private string _origPercent = string.Empty;
  private TaskStatus _origTaskStatus;
  [NotNull]
  private readonly ISessionProvider _sessionProvider = ClientSessionProvider2.Provider;
  [CanBeNull]
  private Task _task;
  [NotNull]
  [ItemNotNull]
  private readonly List<Control> _roControls;
  private int _initialDescriptionHeight = -1;
  private bool _readOnly;
  private bool _modified;
  [CanBeNull]
  private System.IServiceProvider _services;
  [NotNull]
  private string _prevPercent = string.Empty;
  [CanBeNull]
  private readonly EventHandler _completedCheckBoxCheckedChanged;
  [NotNull]
  private string _prevCorrectPercent = string.Empty;
  private bool _disableRbEvents;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Intermech.Bars.ToolBar _toolBar;
  private ButtonItem _propertiesItem;
  private ButtonItem _viewProjectItem;
  private Panel _containerPanel;
  private Panel _panel2;
  private Button _cancelButton;
  private Button _saveButton;
  private GroupBox _chiefGroupBox;
  private Label _label1;
  private GroupBox _userGroupBox;
  private MaskedTextBox _percentTextBox;
  private CheckBox _completedCheckBox;
  private Label _label2;
  private GroupBox _propsGroupBox;
  private Panel _descriptionPanel;
  private TextBox _descriptionTextBox;
  private Label _descriptionLabel;
  private Panel _projectNamePanel;
  private TextBox _projectTextBox;
  private Label _projectLabel;
  private Panel _panel3;
  private Label _statusLabel;
  private Label _statusCapLabel;
  private TextBox _finishTextBox;
  private TextBox _startTextBox;
  private Label _labelFinish;
  private Label _labelStart;
  private Panel _panel5;
  private TextBox _nameTextBox;
  private Label _capLabel;
  private RadioButton _rejectExecRB;
  private RadioButton _acceptExecRB;
  private Button _startExecButton;
  private Panel _startExecPanel;
  private Panel _execPanel;
  private Panel _panel1;
  private TextBox _textMessageToExecutors;
  private Label _labelMessageToExecutors;

  [NotNull]
  public Intermech.Bars.ToolBar ToolBar
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._toolBar.CheckInitializedIn<Intermech.Bars.ToolBar>((object) this);
    }
  }

  [NotNull]
  public ButtonItem PropertiesItem
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._propertiesItem.CheckInitializedIn<ButtonItem>((object) this);
    }
  }

  [NotNull]
  public ButtonItem ViewProjectItem
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._viewProjectItem.CheckInitializedIn<ButtonItem>((object) this);
    }
  }

  [NotNull]
  public Panel ContainerPanel
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._containerPanel.CheckInitializedIn<Panel>((object) this);
    }
  }

  [NotNull]
  protected Panel Panel2
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._panel2.CheckInitializedIn<Panel>((object) this);
    }
  }

  [NotNull]
  public Button CancelButton
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._cancelButton.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  public Button SaveButton
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._saveButton.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  public GroupBox ChiefGroupBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._chiefGroupBox.CheckInitializedIn<GroupBox>((object) this);
    }
  }

  [NotNull]
  protected Label Label1
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._label1.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  public GroupBox UserGroupBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._userGroupBox.CheckInitializedIn<GroupBox>((object) this);
    }
  }

  [NotNull]
  public MaskedTextBox PercentTextBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._percentTextBox.CheckInitializedIn<MaskedTextBox>((object) this);
    }
  }

  [NotNull]
  public CheckBox CompletedCheckBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._completedCheckBox.CheckInitializedIn<CheckBox>((object) this);
    }
  }

  [NotNull]
  protected Label Label2
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._label2.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  public GroupBox PropsGroupBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._propsGroupBox.CheckInitializedIn<GroupBox>((object) this);
    }
  }

  [NotNull]
  public Panel DescriptionPanel
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._descriptionPanel.CheckInitializedIn<Panel>((object) this);
    }
  }

  [NotNull]
  public TextBox DescriptionTextBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._descriptionTextBox.CheckInitializedIn<TextBox>((object) this);
    }
  }

  [NotNull]
  public Label DescriptionLabel
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._descriptionLabel.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  public Panel ProjectNamePanel
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._projectNamePanel.CheckInitializedIn<Panel>((object) this);
    }
  }

  [NotNull]
  public TextBox ProjectTextBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._projectTextBox.CheckInitializedIn<TextBox>((object) this);
    }
  }

  [NotNull]
  public Label ProjectLabel
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._projectLabel.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  protected Panel Panel3
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._panel3.CheckInitializedIn<Panel>((object) this);
    }
  }

  [NotNull]
  public Label StatusLabel
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._statusLabel.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  public Label StatusCapLabel
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._statusCapLabel.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  public TextBox FinishTextBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._finishTextBox.CheckInitializedIn<TextBox>((object) this);
    }
  }

  [NotNull]
  public TextBox StartTextBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._startTextBox.CheckInitializedIn<TextBox>((object) this);
    }
  }

  [NotNull]
  public Label LabelFinish
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelFinish.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  public Label LabelStart
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelStart.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  protected Panel Panel5
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._panel5.CheckInitializedIn<Panel>((object) this);
    }
  }

  [NotNull]
  public TextBox NameTextBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._nameTextBox.CheckInitializedIn<TextBox>((object) this);
    }
  }

  [NotNull]
  public Label CapLabel
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._capLabel.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  public RadioButton RejectExecRB
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._rejectExecRB.CheckInitializedIn<RadioButton>((object) this);
    }
  }

  [NotNull]
  public RadioButton AcceptExecRB
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._acceptExecRB.CheckInitializedIn<RadioButton>((object) this);
    }
  }

  [NotNull]
  public Button StartExecButton
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._startExecButton.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  public Panel StartExecPanel
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._startExecPanel.CheckInitializedIn<Panel>((object) this);
    }
  }

  [NotNull]
  public Panel ExecPanel
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._execPanel.CheckInitializedIn<Panel>((object) this);
    }
  }

  [NotNull]
  public Panel Panel1
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._panel1.CheckInitializedIn<Panel>((object) this);
    }
  }

  [NotNull]
  public TextBox TextMessageToExecutors
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._textMessageToExecutors.CheckInitializedIn<TextBox>((object) this);
    }
  }

  [NotNull]
  public Label LabelMessageToExecutors
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelMessageToExecutors.CheckInitializedIn<Label>((object) this);
    }
  }

  public UserTaskView()
  {
    this.InitializeComponent();
    this.ToolBar.ImageList = Intermech.Client.Services.NamedList.ImageList;
    this.PropertiesItem.ImageIndex = Intermech.Client.Services.NamedList.ImageIndex("imgProp");
    this.ViewProjectItem.ImageIndex = Intermech.Client.Services.NamedList.ImageIndex("imgView");
    this._completedCheckBoxCheckedChanged = new EventHandler(this.CompletedCheckBox_CheckedChanged);
    BarManager service = ApplicationServices.Container.GetService<BarManager>(false);
    if (service != null)
      this.ToolBar.Renderer = service.Renderer;
    this._roControls = new List<Control>((IEnumerable<Control>) new Control[4]
    {
      (Control) this.NameTextBox,
      (Control) this.ProjectTextBox,
      (Control) this.StartTextBox,
      (Control) this.FinishTextBox
    });
    foreach (TextBoxBase textBoxBase in this._roControls.OfType<TextBox>())
      textBoxBase.ReadOnly = true;
    this._roControls.Add((Control) this.ToolBar);
    this._userPrefix = this.UserGroupBox.Text;
    this._chiefPrefix = this.ChiefGroupBox.Text;
    this.DescriptionTextBox.HandleCreated += new EventHandler(this.DescriptionTextBox_HandleCreated);
    this.PropsGroupBox.MaximumSize = new Size(this.PropsGroupBox.Width, 0);
    this.UserGroupBox.MaximumSize = new Size(this.UserGroupBox.Width, 0);
    this.ChiefGroupBox.MaximumSize = new Size(this.ChiefGroupBox.Width, 0);
  }

  protected override void OnVisibleChanged([NotNull] EventArgs e)
  {
    base.OnVisibleChanged(e);
    if (!this.Visible)
      return;
    if (!this._shown)
      this.CalcDescScrollBars();
    this._shown = true;
  }

  public long ProjectObjectID
  {
    get
    {
      long projectObjectId = 0;
      if (this._task?.Project != null)
        projectObjectId = this._task.Project.ObjectID;
      if (projectObjectId == 0L && this._task is Intermech.Project.Project)
        projectObjectId = this._task.ObjectID;
      return projectObjectId;
    }
  }

  [NotNull]
  private static string FormatDt(DateTime dt) => $"{dt.ToString("d")} {dt.ToString("t")}";

  private void LoadPercentCompleted()
  {
    this._origPercent = "0";
    this._origPercent = this._task.PercentCompletedString.Trim('%');
    this.PercentTextBox.Text = this._origPercent;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this._toolBar != null)
        this._toolBar.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      this.components?.Dispose();
    }
    base.Dispose(disposing);
  }

  private void UpdateView()
  {
    this.CheckNotDisposed();
    this._modified = false;
    this._task = (Task) null;
    this._managerAnswer = (string) null;
    this.ViewProjectItem.Visible = UserTaskView.ViewProject != null;
    this._taskID = 0L;
    this._messageID = 0L;
    if (this._selectedObject.HasValue)
    {
      int typeId = this._selectedObject.Value.TypeID;
      if (typeId == (int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.ProjectMessage || MetaDataHelperService.Instance.IsObjectTypeChildOf(typeId, (int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.ProjectMessage))
      {
        this._messageID = this._selectedObject.Value.ObjectID;
        this._taskID = Session.Invoke<long>((Session.SessionHandler<long>) (session =>
        {
          IDBProjectMessage projectMessage = session.GetProjectMessage(this._messageID, false);
          return projectMessage == null ? 0L : projectMessage.TaskID;
        }));
      }
      else if (typeId == (int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.Task || MetaDataHelperService.Instance.IsObjectTypeChildOf(typeId, (int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.Task))
        this._taskID = this._selectedObject.Value.ObjectID;
    }
    if (this._taskID != 0L)
    {
      IUserSession session = this._sessionProvider.GetSession();
      try
      {
        IDBProjectTask result;
        if (!session.TryGetTask(this._taskID, out result))
        {
          this._taskID = 0L;
          this.UpdateView();
          return;
        }
        this._managerAnswer = result.ManagerAnswer;
        this._task = StandaloneTask.Get(this._sessionProvider, this._taskID);
        this._task.Load((IDBObject) result, new bool?(false));
        this._origTaskStatus = this._task.Status;
        this.NameTextBox.Text = this._task.Name;
        this._task.ProjectNeeded();
        if (this._task.Project != null)
          this.ProjectTextBox.Text = this._task.Project.Name;
        this.ProjectNamePanel.Visible = this._task.Project != null;
        DateTime? planStartDateTime = result.PlanStartDateTime;
        this.StartTextBox.Text = planStartDateTime.HasValue ? planStartDateTime.ToString() : string.Empty;
        DateTime? dueDateId = result.DueDateID;
        if (dueDateId.HasValue)
        {
          this.FinishTextBox.Text = dueDateId.Value.ToString((IFormatProvider) CultureInfo.CurrentCulture);
          if (this._task.Status == TaskStatus.Executed && DateTime.Now > dueDateId.Value)
            this.FinishTextBox.ForeColor = Color.Red;
          else
            this.FinishTextBox.ForeColor = SystemColors.WindowText;
        }
        else
        {
          this.FinishTextBox.Text = string.Empty;
          this.FinishTextBox.ForeColor = SystemColors.WindowText;
        }
        this.StatusLabel.Text = this._task.StatusString;
        this.LoadPercentCompleted();
        string description = result.Description;
        this.DescriptionTextBox.Visible = !string.IsNullOrEmpty(description);
        this.DescriptionTextBox.Text = description;
        if (this.DescriptionTextBox.Visible && this.IsHandleCreated)
          this.UpdateDescriptionHeight();
        this.ReadOnly = !this._task.Assignments.UserIDs.Contains(session.UserID);
        if (!this.ReadOnly)
          this.ReadOnly = this._task.Status == TaskStatus.Completed;
        if (this._task.Status != TaskStatus.NotStarted)
        {
          this.CompletedCheckBox.Visible = !this.ReadOnly;
          this.CompletedCheckBox.CheckedChanged -= this._completedCheckBoxCheckedChanged;
          this.CompletedCheckBox.Checked = this._origPercent == "100";
          this.CompletedCheckBox.CheckedChanged += this._completedCheckBoxCheckedChanged;
          string userNamesString = this._task.Assignments.UserNamesString;
          this.UserGroupBox.Text = userNamesString != string.Empty ? $"{this._userPrefix} ({userNamesString})" : Localization.GetString("AddData");
          this.StartExecPanel.Visible = this._task.Status == TaskStatus.Sent && userNamesString != string.Empty;
          this.ExecPanel.Enabled = this._task.Status != TaskStatus.Sent;
          this.UserGroupBox.Visible = true;
        }
        else
          this.UserGroupBox.Visible = false;
        this.ChiefMode = this._task.Status == TaskStatus.Pending && session.UserID == this._task.ChiefID;
        if (this.ChiefMode)
        {
          this.ChiefGroupBox.Visible = true;
          this.ChiefGroupBox.Text = $"{this._chiefPrefix} ({this._task.ChiefName})";
          this._disableRbEvents = true;
          this.AcceptExecRB.Checked = this._task.Status == TaskStatus.Completed;
          this.RejectExecRB.Checked = false;
          this._disableRbEvents = false;
          this.AcceptExecRB.Enabled = true;
          this.RejectExecRB.Enabled = this.AcceptExecRB.Enabled;
          this.TextMessageToExecutors.Text = this._managerAnswer ?? string.Empty;
        }
        else
        {
          this.ChiefGroupBox.Visible = false;
          this.TextMessageToExecutors.Clear();
        }
      }
      finally
      {
        this._sessionProvider.ReleaseSession();
      }
    }
    else
    {
      this.NameTextBox.Clear();
      this.ProjectTextBox.Clear();
      this.PercentTextBox.Clear();
      this.StartTextBox.Clear();
      this.FinishTextBox.Clear();
      this.TextMessageToExecutors.Clear();
      this._disableRbEvents = true;
      this.AcceptExecRB.Checked = false;
      this.RejectExecRB.Checked = false;
      this.CompletedCheckBox.Checked = false;
      this._disableRbEvents = false;
      this.ReadOnly = true;
    }
    this.UpdateEnabled();
  }

  private void UpdateDescriptionHeight()
  {
    Size clientSize;
    if (this._initialDescriptionHeight == -1)
    {
      clientSize = this.DescriptionTextBox.ClientSize;
      this._initialDescriptionHeight = clientSize.Height;
    }
    int num1 = (int) Math.Ceiling((double) ControlFuncs.MeasureTextBoxHeight(this.DescriptionTextBox));
    int num2 = num1;
    clientSize = this.DescriptionTextBox.ClientSize;
    int height1 = clientSize.Height;
    if (num2 < height1)
    {
      TextBox descriptionTextBox = this.DescriptionTextBox;
      clientSize = this.DescriptionTextBox.ClientSize;
      Size size = new Size(clientSize.Width, num1);
      descriptionTextBox.ClientSize = size;
    }
    else
    {
      int num3 = num1;
      clientSize = this.DescriptionTextBox.ClientSize;
      int height2 = clientSize.Height;
      if (num3 <= height2)
        return;
      TextBox descriptionTextBox = this.DescriptionTextBox;
      clientSize = this.DescriptionTextBox.ClientSize;
      Size size = new Size(clientSize.Width, Math.Min(num1, this._initialDescriptionHeight));
      descriptionTextBox.ClientSize = size;
    }
  }

  private void DescriptionTextBox_HandleCreated([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.UpdateDescriptionHeight();
  }

  public bool ReadOnly
  {
    get => this._readOnly;
    set
    {
      if (value == this._readOnly)
        return;
      ControlFuncs.SetControlsReadOnly((Control) this, value, this._roControls);
      this._readOnly = value;
    }
  }

  private void Save()
  {
    Task task = this._task;
    using (task?.Project?.LockRefreshMailBlock())
    {
      this.SaveButton.Enabled = false;
      this._origPercent = this.PercentTextBox.Text;
      if (task != null)
      {
        int result1;
        double result2;
        if (int.TryParse(this._origPercent, out result1))
          result2 = (double) result1;
        else
          double.TryParse(this._origPercent, out result2);
        if (result2 > 100.0)
          result2 = 100.0;
        try
        {
          task.PercentCompleted = result2;
        }
        catch
        {
          this.LoadPercentCompleted();
          throw;
        }
        if (this.ChiefMode)
        {
          if (this.AcceptExecRB.Checked)
            task.Status = TaskStatus.Completed;
          if (this.RejectExecRB.Checked)
          {
            if (this._taskID != 0L)
            {
              IUserSession session = this._sessionProvider.GetSession();
              try
              {
                IDBProjectTask result3;
                if (session.TryGetTask(this._taskID, out result3))
                  result3.ManagerAnswer = this.TextMessageToExecutors.Text.Trim();
              }
              finally
              {
                this._sessionProvider.ReleaseSession();
              }
            }
            task.Status = TaskStatus.Executed;
            if (task.PercentCompleted == 100.0)
              task.PercentCompleted = 50.0;
          }
        }
        this._origTaskStatus = task.Status;
      }
      this.UpdateEnabled();
      if (this._origTaskStatus != TaskStatus.Completed)
        return;
      this.RefreshOrganizer();
    }
  }

  public void Initialize([NotNull] ISelectedItems items, [CanBeNull] System.IServiceProvider provider)
  {
    this._taskID = 0L;
    this._messageID = 0L;
    if (items.Count == 0)
    {
      this._selectedObject = new (int, long)?();
    }
    else
    {
      IDBTypedObjectID itemData = items.GetItemData<IDBTypedObjectID>(0);
      this._selectedObject = new (int, long)?((itemData.ObjectType, itemData.Value));
    }
    this._services = provider;
  }

  public void Activate([CanBeNull] IView previousView) => this.UpdateView();

  public void Deactivate([CanBeNull] IView nextView)
  {
    if (!this.SaveButton.Enabled || this._task == null || MessageFuncs.Ask(string.Format(Resources.SaveUserTaskQuestion, (object) this._task.NameInMessages)) != DialogResult.Yes)
      return;
    this.Save();
  }

  [NotNull]
  public string Caption => Localization.GetString("TaskProps");

  public int ImageIndex => Images.TaskImageIndex;

  public int OrderID => 0;

  [CanBeNull]
  public static event UserTaskView.ViewProjectDelegate ViewProject;

  private void ViewProjectItem_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    UserTaskView.ViewProjectDelegate viewProject = UserTaskView.ViewProject;
    if (viewProject == null)
      return;
    viewProject(this.ProjectObjectID);
  }

  private void PropertiesItem_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this._task == null)
      return;
    using (EditTaskForm editTaskForm = new EditTaskForm())
      editTaskForm.EditTask(this._task, true);
  }

  private void SetMaskedTextBoxSelectAll([NotNull] MaskedTextBox txtBox)
  {
    this.PercentTextBox.SelectAll();
  }

  private void PercentTextBox_Enter([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.BeginInvoke((Delegate) new UserTaskView.SetMaskedTextBoxSelectAllDelegate(this.SetMaskedTextBoxSelectAll), (object) (MaskedTextBox) sender);
  }

  private void CompletedCheckBox_CheckedChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this.CompletedCheckBox.Checked)
    {
      this._prevPercent = this.PercentTextBox.Text;
      this.PercentTextBox.Text = "100";
    }
    else
    {
      if (this._prevPercent == string.Empty || this._prevPercent == "100")
        this._prevPercent = "50";
      this.PercentTextBox.Text = this._prevPercent;
    }
  }

  private void SaveButton_Click([CanBeNull] object sender, [NotNull] EventArgs e) => this.Save();

  private void CancelButton_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.UpdateView();
  }

  private void UpdateEnabled()
  {
    this._modified = this._origPercent != this.PercentTextBox.Text;
    if (!this._modified && this._task != null && this.ChiefMode)
    {
      TaskStatus taskStatus = this._origTaskStatus;
      if (this.AcceptExecRB.Checked)
        taskStatus = TaskStatus.Completed;
      if (this.RejectExecRB.Checked)
        taskStatus = TaskStatus.Executed;
      this._modified = this._origTaskStatus != taskStatus;
    }
    this.SaveButton.Enabled = this._modified;
    this.CancelButton.Enabled = this._modified;
    this.MessageToExecutorsEnabled = this.RejectExecRB.Checked;
  }

  private void PercentTextBox_TextChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.UpdateEnabled();
    if (this.IsPercentValid(this.PercentTextBox.Text))
      this._prevCorrectPercent = this.PercentTextBox.Text;
    else if (this._prevCorrectPercent != string.Empty)
      this.PercentTextBox.Text = this._prevCorrectPercent;
    this.CompletedCheckBox.CheckedChanged -= this._completedCheckBoxCheckedChanged;
    this.CompletedCheckBox.Checked = this.PercentTextBox.Text == "100";
    this.CompletedCheckBox.CheckedChanged += this._completedCheckBoxCheckedChanged;
  }

  private bool IsPercentValid([NotNull] string text)
  {
    int result1;
    if (int.TryParse(text, out result1))
    {
      if (result1 > 100)
        this.PercentTextBox.Text = "100";
      return true;
    }
    double result2;
    if (!double.TryParse(text, out result2))
      return false;
    if (result2 > 100.0)
      this.PercentTextBox.Text = "100";
    return true;
  }

  private void PercentTextBox_Validating([CanBeNull] object sender, [NotNull] CancelEventArgs e)
  {
    e.Cancel = !this.IsPercentValid(this.PercentTextBox.Text);
  }

  private void PercentTextBox_KeyPress([CanBeNull] object sender, [NotNull] KeyPressEventArgs e)
  {
  }

  private void AcceptExecRB_CheckedChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this._disableRbEvents)
      return;
    this.UpdateEnabled();
    if (sender == this.RejectExecRB && this.RejectExecRB.Checked)
    {
      this.PercentTextBox.Text = "50";
    }
    else
    {
      if (sender != this.AcceptExecRB || !this.AcceptExecRB.Checked)
        return;
      this.PercentTextBox.Text = "100";
    }
  }

  private bool MessageToExecutorsEnabled
  {
    get => this.TextMessageToExecutors.Enabled;
    set
    {
      if (value == this.MessageToExecutorsEnabled)
        return;
      if (value)
      {
        this.TextMessageToExecutors.Enabled = true;
        this.TextMessageToExecutors.ReadOnly = false;
        this.TextMessageToExecutors.BackColor = SystemColors.Window;
        this.TextMessageToExecutors.ForeColor = SystemColors.WindowText;
        this.LabelMessageToExecutors.ForeColor = SystemColors.ControlText;
      }
      else
      {
        this.TextMessageToExecutors.Enabled = false;
        this.TextMessageToExecutors.ReadOnly = true;
        this.TextMessageToExecutors.BackColor = SystemColors.Control;
        this.TextMessageToExecutors.ForeColor = SystemColors.GrayText;
        this.LabelMessageToExecutors.ForeColor = SystemColors.GrayText;
      }
    }
  }

  private void StartExecButton_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this._task.Status = TaskStatus.Executed;
    if (this.IsDisposed)
      return;
    this.UpdateView();
  }

  protected bool ChiefMode { get; private set; }

  public void RefreshOrganizer()
  {
    OrganizerCalendarView service1 = this._services.GetService<OrganizerCalendarView>(false);
    if (service1 != null)
    {
      service1.Refresh();
    }
    else
    {
      PageViewsManager service2 = this._services.GetService<PageViewsManager>(false);
      if (service2 == null)
        return;
      IViewPage activeViewPage = service2.ActiveViewPage;
      if (activeViewPage == null || !(activeViewPage.Control is OrganizerChildrenView control))
        return;
      int? count = new int?();
      control.ReloadItems(count);
    }
  }

  private void DescriptionTextBox_TextChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.CalcDescScrollBars();
  }

  private void CalcDescScrollBars()
  {
    ControlFuncs.CalcScrollBarsNeeded(this.DescriptionTextBox);
    this.DescriptionTextBox.Width = this.NameTextBox.Width;
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (UserTaskView));
    this._toolBar = new Intermech.Bars.ToolBar();
    this._propertiesItem = new ButtonItem();
    this._viewProjectItem = new ButtonItem();
    this._containerPanel = new Panel();
    this._panel2 = new Panel();
    this._cancelButton = new Button();
    this._saveButton = new Button();
    this._chiefGroupBox = new GroupBox();
    this._textMessageToExecutors = new TextBox();
    this._labelMessageToExecutors = new Label();
    this._panel1 = new Panel();
    this._acceptExecRB = new RadioButton();
    this._rejectExecRB = new RadioButton();
    this._label1 = new Label();
    this._userGroupBox = new GroupBox();
    this._execPanel = new Panel();
    this._percentTextBox = new MaskedTextBox();
    this._label2 = new Label();
    this._completedCheckBox = new CheckBox();
    this._startExecPanel = new Panel();
    this._startExecButton = new Button();
    this._propsGroupBox = new GroupBox();
    this._descriptionPanel = new Panel();
    this._descriptionTextBox = new TextBox();
    this._descriptionLabel = new Label();
    this._panel3 = new Panel();
    this._statusLabel = new Label();
    this._statusCapLabel = new Label();
    this._finishTextBox = new TextBox();
    this._startTextBox = new TextBox();
    this._labelFinish = new Label();
    this._labelStart = new Label();
    this._projectNamePanel = new Panel();
    this._projectTextBox = new TextBox();
    this._projectLabel = new Label();
    this._panel5 = new Panel();
    this._nameTextBox = new TextBox();
    this._capLabel = new Label();
    this._containerPanel.SuspendLayout();
    this._panel2.SuspendLayout();
    this._chiefGroupBox.SuspendLayout();
    this._panel1.SuspendLayout();
    this._userGroupBox.SuspendLayout();
    this._execPanel.SuspendLayout();
    this._startExecPanel.SuspendLayout();
    this._propsGroupBox.SuspendLayout();
    this._descriptionPanel.SuspendLayout();
    this._panel3.SuspendLayout();
    this._projectNamePanel.SuspendLayout();
    this._panel5.SuspendLayout();
    this.SuspendLayout();
    this._toolBar.FullMenus = true;
    this._toolBar.Guid = new Guid("83cc7b24-0888-422e-969d-e1869b0fc075");
    this._toolBar.Hidden = false;
    this._toolBar.Items.AddRange(new ToolbarItemBase[2]
    {
      (ToolbarItemBase) this._propertiesItem,
      (ToolbarItemBase) this._viewProjectItem
    });
    componentResourceManager.ApplyResources((object) this._toolBar, "_toolBar");
    this._toolBar.Name = "_toolBar";
    componentResourceManager.ApplyResources((object) this._propertiesItem, "_propertiesItem");
    this._propertiesItem.Click += new EventHandler(this.PropertiesItem_Click);
    componentResourceManager.ApplyResources((object) this._viewProjectItem, "_viewProjectItem");
    this._viewProjectItem.Click += new EventHandler(this.ViewProjectItem_Click);
    componentResourceManager.ApplyResources((object) this._containerPanel, "_containerPanel");
    this._containerPanel.Controls.Add((Control) this._panel2);
    this._containerPanel.Controls.Add((Control) this._chiefGroupBox);
    this._containerPanel.Controls.Add((Control) this._userGroupBox);
    this._containerPanel.Controls.Add((Control) this._propsGroupBox);
    this._containerPanel.Name = "_containerPanel";
    this._panel2.Controls.Add((Control) this._cancelButton);
    this._panel2.Controls.Add((Control) this._saveButton);
    componentResourceManager.ApplyResources((object) this._panel2, "_panel2");
    this._panel2.Name = "_panel2";
    componentResourceManager.ApplyResources((object) this._cancelButton, "_cancelButton");
    this._cancelButton.Name = "_cancelButton";
    this._cancelButton.UseVisualStyleBackColor = true;
    this._cancelButton.Click += new EventHandler(this.CancelButton_Click);
    componentResourceManager.ApplyResources((object) this._saveButton, "_saveButton");
    this._saveButton.Name = "_saveButton";
    this._saveButton.UseVisualStyleBackColor = true;
    this._saveButton.Click += new EventHandler(this.SaveButton_Click);
    componentResourceManager.ApplyResources((object) this._chiefGroupBox, "_chiefGroupBox");
    this._chiefGroupBox.BackColor = SystemColors.Control;
    this._chiefGroupBox.Controls.Add((Control) this._textMessageToExecutors);
    this._chiefGroupBox.Controls.Add((Control) this._labelMessageToExecutors);
    this._chiefGroupBox.Controls.Add((Control) this._panel1);
    this._chiefGroupBox.Controls.Add((Control) this._label1);
    this._chiefGroupBox.Name = "_chiefGroupBox";
    this._chiefGroupBox.TabStop = false;
    this._textMessageToExecutors.BackColor = SystemColors.Control;
    componentResourceManager.ApplyResources((object) this._textMessageToExecutors, "_textMessageToExecutors");
    this._textMessageToExecutors.ForeColor = SystemColors.GrayText;
    this._textMessageToExecutors.Name = "_textMessageToExecutors";
    this._textMessageToExecutors.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this._labelMessageToExecutors, "_labelMessageToExecutors");
    this._labelMessageToExecutors.ForeColor = SystemColors.GrayText;
    this._labelMessageToExecutors.Name = "_labelMessageToExecutors";
    this._panel1.Controls.Add((Control) this._acceptExecRB);
    this._panel1.Controls.Add((Control) this._rejectExecRB);
    componentResourceManager.ApplyResources((object) this._panel1, "_panel1");
    this._panel1.Name = "_panel1";
    componentResourceManager.ApplyResources((object) this._acceptExecRB, "_acceptExecRB");
    this._acceptExecRB.Name = "_acceptExecRB";
    this._acceptExecRB.TabStop = true;
    this._acceptExecRB.UseVisualStyleBackColor = true;
    this._acceptExecRB.CheckedChanged += new EventHandler(this.AcceptExecRB_CheckedChanged);
    componentResourceManager.ApplyResources((object) this._rejectExecRB, "_rejectExecRB");
    this._rejectExecRB.Name = "_rejectExecRB";
    this._rejectExecRB.TabStop = true;
    this._rejectExecRB.UseVisualStyleBackColor = true;
    this._rejectExecRB.CheckedChanged += new EventHandler(this.AcceptExecRB_CheckedChanged);
    componentResourceManager.ApplyResources((object) this._label1, "_label1");
    this._label1.Name = "_label1";
    componentResourceManager.ApplyResources((object) this._userGroupBox, "_userGroupBox");
    this._userGroupBox.Controls.Add((Control) this._execPanel);
    this._userGroupBox.Controls.Add((Control) this._startExecPanel);
    this._userGroupBox.Name = "_userGroupBox";
    this._userGroupBox.TabStop = false;
    this._execPanel.Controls.Add((Control) this._percentTextBox);
    this._execPanel.Controls.Add((Control) this._label2);
    this._execPanel.Controls.Add((Control) this._completedCheckBox);
    componentResourceManager.ApplyResources((object) this._execPanel, "_execPanel");
    this._execPanel.Name = "_execPanel";
    this._percentTextBox.BeepOnError = true;
    this._percentTextBox.CutCopyMaskFormat = MaskFormat.ExcludePromptAndLiterals;
    componentResourceManager.ApplyResources((object) this._percentTextBox, "_percentTextBox");
    this._percentTextBox.Name = "_percentTextBox";
    this._percentTextBox.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
    this._percentTextBox.TextChanged += new EventHandler(this.PercentTextBox_TextChanged);
    this._percentTextBox.Enter += new EventHandler(this.PercentTextBox_Enter);
    this._percentTextBox.KeyPress += new KeyPressEventHandler(this.PercentTextBox_KeyPress);
    this._percentTextBox.Validating += new CancelEventHandler(this.PercentTextBox_Validating);
    componentResourceManager.ApplyResources((object) this._label2, "_label2");
    this._label2.Name = "_label2";
    componentResourceManager.ApplyResources((object) this._completedCheckBox, "_completedCheckBox");
    this._completedCheckBox.Name = "_completedCheckBox";
    this._completedCheckBox.UseVisualStyleBackColor = true;
    this._completedCheckBox.CheckedChanged += new EventHandler(this.CompletedCheckBox_CheckedChanged);
    this._startExecPanel.Controls.Add((Control) this._startExecButton);
    componentResourceManager.ApplyResources((object) this._startExecPanel, "_startExecPanel");
    this._startExecPanel.Name = "_startExecPanel";
    componentResourceManager.ApplyResources((object) this._startExecButton, "_startExecButton");
    this._startExecButton.Name = "_startExecButton";
    this._startExecButton.UseVisualStyleBackColor = true;
    this._startExecButton.Click += new EventHandler(this.StartExecButton_Click);
    componentResourceManager.ApplyResources((object) this._propsGroupBox, "_propsGroupBox");
    this._propsGroupBox.Controls.Add((Control) this._descriptionPanel);
    this._propsGroupBox.Controls.Add((Control) this._panel3);
    this._propsGroupBox.Controls.Add((Control) this._projectNamePanel);
    this._propsGroupBox.Controls.Add((Control) this._panel5);
    this._propsGroupBox.Name = "_propsGroupBox";
    this._propsGroupBox.TabStop = false;
    componentResourceManager.ApplyResources((object) this._descriptionPanel, "_descriptionPanel");
    this._descriptionPanel.Controls.Add((Control) this._descriptionTextBox);
    this._descriptionPanel.Controls.Add((Control) this._descriptionLabel);
    this._descriptionPanel.Name = "_descriptionPanel";
    this._descriptionTextBox.BackColor = SystemColors.Control;
    componentResourceManager.ApplyResources((object) this._descriptionTextBox, "_descriptionTextBox");
    this._descriptionTextBox.Name = "_descriptionTextBox";
    this._descriptionTextBox.ReadOnly = true;
    this._descriptionTextBox.TextChanged += new EventHandler(this.DescriptionTextBox_TextChanged);
    componentResourceManager.ApplyResources((object) this._descriptionLabel, "_descriptionLabel");
    this._descriptionLabel.Name = "_descriptionLabel";
    this._panel3.Controls.Add((Control) this._statusLabel);
    this._panel3.Controls.Add((Control) this._statusCapLabel);
    this._panel3.Controls.Add((Control) this._finishTextBox);
    this._panel3.Controls.Add((Control) this._startTextBox);
    this._panel3.Controls.Add((Control) this._labelFinish);
    this._panel3.Controls.Add((Control) this._labelStart);
    componentResourceManager.ApplyResources((object) this._panel3, "_panel3");
    this._panel3.Name = "_panel3";
    componentResourceManager.ApplyResources((object) this._statusLabel, "_statusLabel");
    this._statusLabel.Name = "_statusLabel";
    componentResourceManager.ApplyResources((object) this._statusCapLabel, "_statusCapLabel");
    this._statusCapLabel.Name = "_statusCapLabel";
    this._finishTextBox.BackColor = SystemColors.Control;
    componentResourceManager.ApplyResources((object) this._finishTextBox, "_finishTextBox");
    this._finishTextBox.Name = "_finishTextBox";
    this._finishTextBox.ReadOnly = true;
    this._startTextBox.BackColor = SystemColors.Control;
    componentResourceManager.ApplyResources((object) this._startTextBox, "_startTextBox");
    this._startTextBox.Name = "_startTextBox";
    this._startTextBox.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this._labelFinish, "_labelFinish");
    this._labelFinish.Name = "_labelFinish";
    componentResourceManager.ApplyResources((object) this._labelStart, "_labelStart");
    this._labelStart.Name = "_labelStart";
    this._projectNamePanel.Controls.Add((Control) this._projectTextBox);
    this._projectNamePanel.Controls.Add((Control) this._projectLabel);
    componentResourceManager.ApplyResources((object) this._projectNamePanel, "_projectNamePanel");
    this._projectNamePanel.Name = "_projectNamePanel";
    this._projectTextBox.BackColor = SystemColors.Control;
    componentResourceManager.ApplyResources((object) this._projectTextBox, "_projectTextBox");
    this._projectTextBox.Name = "_projectTextBox";
    this._projectTextBox.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this._projectLabel, "_projectLabel");
    this._projectLabel.Name = "_projectLabel";
    this._panel5.Controls.Add((Control) this._nameTextBox);
    this._panel5.Controls.Add((Control) this._capLabel);
    componentResourceManager.ApplyResources((object) this._panel5, "_panel5");
    this._panel5.Name = "_panel5";
    componentResourceManager.ApplyResources((object) this._nameTextBox, "_nameTextBox");
    this._nameTextBox.Name = "_nameTextBox";
    this._nameTextBox.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this._capLabel, "_capLabel");
    this._capLabel.Name = "_capLabel";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._containerPanel);
    this.Controls.Add((Control) this._toolBar);
    this.Name = nameof (UserTaskView);
    this._containerPanel.ResumeLayout(false);
    this._containerPanel.PerformLayout();
    this._panel2.ResumeLayout(false);
    this._chiefGroupBox.ResumeLayout(false);
    this._chiefGroupBox.PerformLayout();
    this._panel1.ResumeLayout(false);
    this._panel1.PerformLayout();
    this._userGroupBox.ResumeLayout(false);
    this._execPanel.ResumeLayout(false);
    this._execPanel.PerformLayout();
    this._startExecPanel.ResumeLayout(false);
    this._propsGroupBox.ResumeLayout(false);
    this._propsGroupBox.PerformLayout();
    this._descriptionPanel.ResumeLayout(false);
    this._descriptionPanel.PerformLayout();
    this._panel3.ResumeLayout(false);
    this._panel3.PerformLayout();
    this._projectNamePanel.ResumeLayout(false);
    this._projectNamePanel.PerformLayout();
    this._panel5.ResumeLayout(false);
    this._panel5.PerformLayout();
    this.ResumeLayout(false);
  }

  public delegate void ViewProjectDelegate(long projectID);

  private delegate void SetMaskedTextBoxSelectAllDelegate([NotNull] MaskedTextBox txtBox);

  private sealed class UserTaskViewDescriptionProvider : BaseViewDescriptionProvider
  {
    [NotNull]
    public override ViewDescription DoGetViewDescription(
      [CanBeNull] ISelectedItems selectedItems,
      [CanBeNull] System.IServiceProvider serviceProvider)
    {
      return new ViewDescription()
      {
        Caption = Localization.GetString("TaskProps"),
        ImageIndex = Images.TaskImageIndex,
        OrderID = 0
      };
    }
  }
}
