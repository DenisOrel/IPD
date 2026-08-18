// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.ProjectView
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Common;
using Intermech.Controls;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces.Client;
using Intermech.Metadata;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.Project.Controls.Properties;
using Intermech.Windows.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

/// <summary>Вьюшка диаграммы Гантта деревом задач и сплиттером </summary>
public class ProjectView : 
  IpsBaseUserControl,
  IComponent,
  IDisposable,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IContainerControl,
  IDesignModeControlsContainer,
  IArrowKeysNavigationSupported,
  ILastFocusedControlTracker,
  IContextAware,
  ISupportSaveLocks,
  INamedContext,
  ICanBeReadOnly,
  ICanBeReadOnly2,
  IClientProjectContext
{
  private bool _allowEditForm;
  private bool _autoLevelResources;
  private int _days;
  private bool _duringAutoLevelResources;
  private bool _duringShowEditPanel;
  [CanBeNull]
  private FindStringForm _findStringForm;
  [CanBeNull]
  private Thread _levelResourcesThread;
  private int _printCurrentPage;
  private DateTime _printGanttChartCurrentDate;
  private int _printGanttChartCurrentDays;
  private bool _printInitialized;
  [ItemNotNull]
  [CanBeNull]
  private List<List<string>> _printPagesColumns;
  [CanBeNull]
  private List<int> _printPagesFirstRows;
  [CanBeNull]
  private List<int> _printWidths;
  [CanBeNull]
  private ClientProject _project;
  [NotNull]
  private readonly Dictionary<Task, Brush> _taskBrushes = new Dictionary<Task, Brush>();
  [NotNull]
  private readonly Dictionary<Task, Pen> _taskPens = new Dictionary<Task, Pen>();
  private bool _useDataGridViewRowHeaderContextMenu;
  private const string ClipboardFormat = "ImProject.Tasks";
  /// <summary>
  /// Аккумулятор серии событий "перерисовка диаграммы Гантта".
  /// Первый вызов <see cref="M:Intermech.Project.Controls.ProjectView.RefreshGanttView" /> вызывает немедленную перерисовку диаграммы,
  /// следующая перерисовка в серии вызовов будет вызвана только если по её (серии вызовов )окончании.
  /// Серией считается последовательность вызовов, между которыми прошло менее <see cref="F:Intermech.Project.Controls.ProjectView.ConstRefreshGanttViewEventsBatchDelay" /> миллисекунд.
  /// 
  /// Позволяет избежать множества промежуточных перерисовок (тормозит), при серии событий их вызывающих
  /// например BB 1583073 - при скороле колесом мыши приходила масса событий OnScroll, каждое из которых вызывало перерисовку, в результате всё тормозило
  /// </summary>
  [NotNull]
  private readonly EventsAccumulator _refreshGanttViewEventsAccumulator;
  private const int ConstRefreshGanttViewEventsBatchDelay = 300;
  private UserSummaryTask _selectedUserTask;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ToolStripContainer _toolStripContainer;
  private SplitContainer _mainSplitContainer;
  private Label _labelIcon;
  private VScrollBar _vScrollBar;
  private SplitContainer _splitContainer;
  private ProjectDataGridView _gridView;
  private GanttChart _ganttChart;
  private Panel _editPanel;
  private Panel _editTitlePanel;
  private Panel _vScrollBarPanel;
  private Panel _vScrollBarPanelBottom;
  private Label _editPanelTitleLabel;
  private Button _closeEditPanelButton;
  private Label _editPanelTitleIconLabel;
  private PrintDocument _printDocument;
  private PrintDialog _printDialog;
  private PageSetupDialog _pageSetupDialog;
  private System.Windows.Forms.Timer _timerAutoLevelResources;
  private PrintPreviewDialog _printPreviewDialog;

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected ToolStripContainer ToolStripContainer
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._toolStripContainer.CheckInitializedIn<ToolStripContainer>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected SplitContainer MainSplitContainer
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._mainSplitContainer.CheckInitializedIn<SplitContainer>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected Label LabelIcon
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelIcon.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected VScrollBar VerticalScrollBar
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._vScrollBar.CheckInitializedIn<VScrollBar>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public SplitContainer SplitContainer
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._splitContainer.CheckInitializedIn<SplitContainer>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ProjectDataGridView GridView
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._gridView.CheckInitializedIn<ProjectDataGridView>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public GanttChart GanttChart
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._ganttChart.CheckInitializedIn<GanttChart>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected Panel EditPanel
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._editPanel.CheckInitializedIn<Panel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected Panel EditTitlePanel
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._editTitlePanel.CheckInitializedIn<Panel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected Panel VerticalScrollBarPanel
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._vScrollBarPanel.CheckInitializedIn<Panel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected Panel VerticalScrollBarPanelBottom
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._vScrollBarPanelBottom.CheckInitializedIn<Panel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected Label EditPanelTitleLabel
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._editPanelTitleLabel.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected Button CloseEditPanelButton
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._closeEditPanelButton.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected Label EditPanelTitleIconLabel
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._editPanelTitleIconLabel.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected PrintDocument PrintDocument
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._printDocument.CheckInitializedIn<PrintDocument>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected PrintDialog PrintDialog
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._printDialog.CheckInitializedIn<PrintDialog>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected PageSetupDialog PageSetupDialog
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._pageSetupDialog.CheckInitializedIn<PageSetupDialog>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected System.Windows.Forms.Timer TimerAutoLevelResources
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._timerAutoLevelResources.CheckInitializedIn<System.Windows.Forms.Timer>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected PrintPreviewDialog PrintPreviewDialog
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._printPreviewDialog.CheckInitializedIn<PrintPreviewDialog>((object) this);
    }
  }

  [CanBeNull]
  [Category("Appearance")]
  [Description("Occurs after the Gantt Chart area is painted")]
  public event PaintEventHandler GanttChartPaint;

  [CanBeNull]
  [Description("Occurs when an operation was completed")]
  [Category("Behavior")]
  public event EventHandler<OperationCompletedEventArgs> OperationCompleted;

  [CanBeNull]
  [Category("Behavior")]
  [Description("Occurs when an operation is starting")]
  public event EventHandler<OperationStartedEventArgs> OperationStarted;

  [CanBeNull]
  [Description("Occurs when a property of the control is changed")]
  [Category("Behavior")]
  public event PropertyChangedEventHandler PropertyChanged;

  [CanBeNull]
  [Description("Occurs when the selected tasks are changed")]
  [Category("Behavior")]
  public event EventHandler SelectionChanged;

  [CanBeNull]
  [Description("Occurs when a task is expanded or minimized")]
  [Category("Behavior")]
  public event EventHandler<TaskExpandedChangedEventArgs> TaskExpandedChanged;

  [Description("Occurs when the context menu invoked")]
  [Category("Behavior")]
  public event MouseEventHandler ContextMenuRequested;

  public ProjectView()
  {
    this.InitializeComponent();
    this._refreshGanttViewEventsAccumulator = new EventsAccumulator(new Action(this._refreshGanttView), 300);
    if (this._project != null)
    {
      this.AddService<ClientProject>(this._project);
      this.AddService<Intermech.Project.Project>((Intermech.Project.Project) this._project);
    }
    this.GanttChart.GridView = this.GridView;
    this.AddService<ProjectView>(this);
    this.AddService<GanttChart>(this.GanttChart);
    this.AddService<ProjectDataGridView>(this.GridView);
    this.PrintDocument.DocumentName = Resources.ProjectPrintDocumentName;
    this.GanttChart.TaskPens = this._taskPens;
    this.GanttChart.TaskBrushes = this._taskBrushes;
    this.Load += new EventHandler(this.ProjectView_Load);
    this.PropertyChanged += new PropertyChangedEventHandler(this.ProjectView_PropertyChanged);
    this.GridView.InvalidateGanttChart += new EventHandler(this.GridView_InvalidateGanttChart);
    this.GridView._GanttChart = this.GanttChart;
    this.SplitContainer.Panel1MinSize = 100;
    this.SplitContainer.Panel2MinSize = 30;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this._refreshGanttViewEventsAccumulator.Dispose();
      this.RemoveService<ProjectDataGridView>();
      this.RemoveService<GanttChart>();
      this.RemoveService<ProjectView>();
      this.RemoveService<Intermech.Project.Project>();
      this.RemoveService<ClientProject>();
      if (this.components != null)
        this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  private void GridView_InvalidateGanttChart([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.GanttChart.Invalidate();
  }

  public void BeginLevelResources()
  {
    while (this._levelResourcesThread != null)
      Thread.Sleep(100);
    this._levelResourcesThread = new Thread(new ParameterizedThreadStart(this.LevelResources));
    this._levelResourcesThread.Start();
  }

  private void BeginPrint()
  {
    this._printPagesColumns = new List<List<string>>();
    this._printPagesFirstRows = new List<int>();
    this._printCurrentPage = 0;
    this._printInitialized = false;
  }

  private void ClearEditPanel()
  {
    if (this.EditPanel.Controls.Count <= 1)
      return;
    Control control = this.EditPanel.Controls.Cast<Control>().FirstOrDefault<Control>();
    if (control == null)
      return;
    this.EditPanel.Controls.RemoveAt(0);
    control.Dispose();
  }

  public static DialogResult SayError([NotNull] string s, MessageBoxButtons buttons)
  {
    return MessageBox.Show(s, Resources.Error, buttons, MessageBoxIcon.Hand);
  }

  private void closeEditPanelButton_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.HideEditPanel();
  }

  private void CompleteLevelResources([NotNull] object state)
  {
    List<object> objectList = (List<object>) state;
    int index1 = (int) objectList[0];
    int index2 = (int) objectList[1];
    bool flag = (bool) objectList[3];
    this.GridView.DuringUpdate = false;
    this.GanttChart.AllowDrag = flag;
    if (index1 >= 0 && index2 >= 0)
      this.GridView.CurrentCell = this.GridView.Rows[index1].Cells[index2];
    EventHandler<OperationCompletedEventArgs> operationCompleted = this.OperationCompleted;
    if (operationCompleted == null)
      return;
    operationCompleted((object) this, new OperationCompletedEventArgs("LevelResources"));
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool HasSelected
  {
    get
    {
      return this.DataGridView.SelectedCells.Count > 0 && (this.DataGridView.NewRowIndex == -1 || this.DataGridView.SelectedCells[0].RowIndex < this.DataGridView.NewRowIndex);
    }
  }

  public void Copy()
  {
    if (this.DataGridView.SelectedRows.Count == 0)
    {
      try
      {
        Clipboard.SetDataObject((object) this.DataGridView.GetClipboardContent(), true, 5, 10);
      }
      catch
      {
      }
    }
    else
    {
      DataGridViewCell currentCell = this.GridView.CurrentCell;
      DataGridViewCell dataGridViewCell1 = currentCell;
      int index1 = dataGridViewCell1 != null ? dataGridViewCell1.RowIndex : -1;
      DataGridViewCell dataGridViewCell2 = currentCell;
      int index2 = dataGridViewCell2 != null ? dataGridViewCell2.ColumnIndex : -1;
      this.Validate();
      EventHandler<OperationStartedEventArgs> operationStarted = this.OperationStarted;
      if (operationStarted != null)
        operationStarted((object) this, new OperationStartedEventArgs(nameof (Copy)));
      using (MemoryStream serializationStream = new MemoryStream())
      {
        try
        {
          BinaryFormatter binaryFormatter = new BinaryFormatter();
          binaryFormatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
          this.SuspendDrawing();
          Task[] array;
          try
          {
            array = this.GetSelectedTasksWithAllSubtasks(true, (ProjectView.BeforeSubProjectLoadingDelegate) (subProject =>
            {
              currentCell = (DataGridViewCell) null;
              return (object) this.GridView.IsExpanded((Task) subProject);
            }), (ProjectView.AfterSubProjectLoadingDelegate) ((subProject, loadResult, expandToRestore) =>
            {
              if (expandToRestore == null)
                return;
              bool expanded = (bool) expandToRestore;
              if (!loadResult || this.GridView.IsExpanded((Task) subProject) == expanded)
                return;
              this.GridView.SetExpanded((Task) subProject, expanded);
            })).ToArray<Task>();
          }
          finally
          {
            this.ResumeDrawing();
          }
          binaryFormatter.Serialize((Stream) serializationStream, (object) array);
          serializationStream.Seek(0L, SeekOrigin.Begin);
          try
          {
            Clipboard.SetData("ImProject.Tasks", (object) serializationStream.ToArray());
          }
          catch
          {
          }
        }
        catch (SecurityException ex)
        {
          EventHandler<OperationCompletedEventArgs> operationCompleted = this.OperationCompleted;
          if (operationCompleted == null)
            return;
          operationCompleted((object) this, new OperationCompletedEventArgs(nameof (Copy), false));
          return;
        }
      }
      try
      {
        if (index1 >= 0)
        {
          if (index1 < this.GridView.RowCount)
          {
            if (index2 >= 0)
            {
              if (index2 < this.GridView.ColumnCount)
              {
                DataGridViewRow row = this.GridView.Rows[index1];
                if (row.Visible)
                {
                  if (this.GridView.Columns[index2].Visible)
                    this.GridView.CurrentCell = row.Cells[index2];
                }
              }
            }
          }
        }
      }
      catch
      {
      }
      this.GridView.Invalidate();
      EventHandler<OperationCompletedEventArgs> operationCompleted1 = this.OperationCompleted;
      if (operationCompleted1 == null)
        return;
      operationCompleted1((object) this, new OperationCompletedEventArgs(nameof (Copy)));
    }
  }

  public void Cut()
  {
    this.Validate();
    EventHandler<OperationStartedEventArgs> operationStarted = this.OperationStarted;
    if (operationStarted != null)
      operationStarted((object) this, new OperationStartedEventArgs(nameof (Cut)));
    this.Copy();
    this.Delete();
    this.GridView.ClearSelection();
    EventHandler<OperationCompletedEventArgs> operationCompleted = this.OperationCompleted;
    if (operationCompleted == null)
      return;
    operationCompleted((object) this, new OperationCompletedEventArgs(nameof (Cut)));
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool CanDecreaseIndent
  {
    get
    {
      return this.Project != null && this.EditingMode.Any() && this.SelectedTasks.Any<Task>((Func<Task, bool>) (task => task.IndentLevel > 0));
    }
  }

  public void DecreaseIndent()
  {
    this.Validate();
    EventHandler<OperationStartedEventArgs> operationStarted = this.OperationStarted;
    if (operationStarted != null)
      operationStarted((object) this, new OperationStartedEventArgs(nameof (DecreaseIndent)));
    bool success = true;
    List<Task> list = this.SelectedTasks.Where<Task>((Func<Task, bool>) (task => task.IndentLevel > 0)).ToList<Task>();
    try
    {
      this.Project.ChangeIndent((IEnumerable<Task>) list, -1);
    }
    catch (InvalidOperationException ex)
    {
      success = false;
    }
    EventHandler<OperationCompletedEventArgs> operationCompleted = this.OperationCompleted;
    if (operationCompleted == null)
      return;
    operationCompleted((object) this, new OperationCompletedEventArgs(nameof (DecreaseIndent), success));
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool CanDelete
  {
    get => this.Project != null && this.EditingMode.HasComposition() && this.HasSelected;
  }

  public void Delete()
  {
    if (this.DataGridView.SelectedRows.Count == 0)
    {
      foreach (DataGridViewCell selectedCell in (BaseCollection) this.DataGridView.SelectedCells)
        selectedCell.Value = (object) string.Empty;
    }
    else
    {
      this.Validate();
      EventHandler<OperationStartedEventArgs> operationStarted = this.OperationStarted;
      if (operationStarted != null)
        operationStarted((object) this, new OperationStartedEventArgs(nameof (Delete)));
      this.Project.RemoveTasks((IEnumerable<Task>) this.GridView.SelectedWithSubTasks);
      EventHandler<OperationCompletedEventArgs> operationCompleted = this.OperationCompleted;
      if (operationCompleted == null)
        return;
      operationCompleted((object) this, new OperationCompletedEventArgs(nameof (Delete)));
    }
  }

  public bool Edit([CanBeNull] string initialParamName = null)
  {
    Task selectedTask = this.SelectedTask;
    if (selectedTask != null)
    {
      this.Validate();
      using (EditTaskForm editTaskForm = new EditTaskForm(initialParamName))
      {
        string name = selectedTask.Name;
        if (!(selectedTask is Intermech.Project.Project p) || (Control.ModifierKeys & Keys.Control) != Keys.None ? editTaskForm.EditTask(selectedTask, !selectedTask.EditingMode.HasProperties()) : ProjectPropsForm.Show(p))
        {
          if (name != selectedTask.Name)
            this.GridView.CurrentTaskNameChanged();
          this.GridView.InvalidateRow(this.GridView.CurrentRow.Index);
          this.GanttChart.Invalidate();
        }
      }
    }
    return false;
  }

  public void ShowProjectProperties()
  {
    ClientProject project = this.Project;
    if (project == null)
      return;
    this.Validate();
    ProjectPropsForm.Show((Intermech.Project.Project) project);
  }

  private void EndPrint()
  {
    this._printPagesColumns = (List<List<string>>) null;
    this._printPagesFirstRows = (List<int>) null;
    this._printCurrentPage = 0;
    this._printInitialized = false;
  }

  public void Expand([CanBeNull] Task task) => this.GridView.SetExpanded(task, true);

  public void ExpandAll()
  {
    if (this.Project == null)
      return;
    foreach (Task task in this.Project.Tasks.Where<Task>((Func<Task, bool>) (task => task.HasSubTasks)))
      this.Expand(task);
  }

  public void Find()
  {
    if (this.Project == null)
      return;
    this.Validate();
    Form parentForm = this.ParentForm;
    if (this._findStringForm == null)
    {
      this._findStringForm = new FindStringForm("Tasks");
      Task selectedTask = this.SelectedTask;
      this._findStringForm.CurrentIndex = selectedTask != null ? selectedTask.Index : 0;
      this._findStringForm.Show((IWin32Window) parentForm);
      this._findStringForm.FormClosed += new FormClosedEventHandler(this.findStringForm_FormClosed);
      this._findStringForm.Find += new FindStringForm.FindEventHandler(this.findStringForm_Find);
    }
    else
      this._findStringForm.Activate();
  }

  private void findStringForm_Find([NotNull] object sender, [NotNull] FindStringForm.FindEventArgs e)
  {
    this.Validate();
    EventHandler<OperationStartedEventArgs> operationStarted = this.OperationStarted;
    if (operationStarted != null)
      operationStarted((object) this, new OperationStartedEventArgs("Find"));
    FindStringForm findStringForm = sender as FindStringForm;
    List<Task> taskList = new List<Task>();
    TaskCollection tasks = this.Project.Tasks;
    while (findStringForm.DirectionDown && findStringForm.CurrentIndex < tasks.Count || !findStringForm.DirectionDown && findStringForm.CurrentIndex >= 0)
    {
      Task task = (Task) null;
      if (findStringForm.CurrentIndex >= 0 && findStringForm.CurrentIndex < tasks.Count)
        task = tasks[findStringForm.CurrentIndex];
      if (findStringForm.DirectionDown)
        ++findStringForm.CurrentIndex;
      else
        --findStringForm.CurrentIndex;
      if (task != null && findStringForm.FindInString($"{task.Name}\r{task.AssignmentsString}"))
      {
        taskList.Add(task);
        break;
      }
    }
    this.SelectedTasks = (IReadOnlyList<Task>) taskList;
    if (taskList.Count == 0)
    {
      int num = (int) MessageBox.Show(string.Format(Strings.CannotFind, (object) findStringForm.FindString), string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      findStringForm.Activate();
      EventHandler<OperationCompletedEventArgs> operationCompleted = this.OperationCompleted;
      if (operationCompleted == null)
        return;
      operationCompleted((object) this, new OperationCompletedEventArgs("Find", false));
    }
    else
    {
      EventHandler<OperationCompletedEventArgs> operationCompleted = this.OperationCompleted;
      if (operationCompleted == null)
        return;
      operationCompleted((object) this, new OperationCompletedEventArgs("Find"));
    }
  }

  private void findStringForm_FormClosed([CanBeNull] object sender, [NotNull] FormClosedEventArgs e)
  {
    this._findStringForm = (FindStringForm) null;
  }

  private int GetIndex(int visibleIndex)
  {
    int index = 0;
    foreach (DataGridViewRow row in (IEnumerable) this.GridView.Rows)
    {
      if (row.Index == visibleIndex)
        return index;
      if (row.Visible)
        ++index;
    }
    return 0;
  }

  private int GetVisibleIndex(int index)
  {
    int visibleIndex = 0;
    foreach (DataGridViewBand row in (IEnumerable) this.GridView.Rows)
    {
      if (row.Visible)
      {
        if (index == 0)
          return visibleIndex;
        --index;
      }
      ++visibleIndex;
    }
    return 0;
  }

  public void HideEditPanel()
  {
    this.Validate();
    this.GridView.ClearSelection();
    this.EditPanelTitle = (string) null;
    this._duringShowEditPanel = true;
    this.MainSplitContainer.Panel2Collapsed = true;
    this.ClearEditPanel();
    this._duringShowEditPanel = false;
  }

  private bool IncreaseCurrentPage()
  {
    ++this._printCurrentPage;
    return this._printCurrentPage < this.PrintPagesCount;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool CanIncreaseIndent
  {
    get
    {
      return this.Project != null && !this.EditingMode.ReadOnly() && this.SelectedTasks.Any<Task>((Func<Task, bool>) (task => task.MaxPossibleIndentLevel > task.IndentLevel));
    }
  }

  public void IncreaseIndent()
  {
    this.Validate();
    EventHandler<OperationStartedEventArgs> operationStarted = this.OperationStarted;
    if (operationStarted != null)
      operationStarted((object) this, new OperationStartedEventArgs(nameof (IncreaseIndent)));
    bool success = true;
    try
    {
      this.Project.ChangeIndent((IEnumerable<Task>) this.SelectedTasks, 1);
    }
    catch (InvalidOperationException ex)
    {
      success = false;
    }
    EventHandler<OperationCompletedEventArgs> operationCompleted = this.OperationCompleted;
    if (operationCompleted == null)
      return;
    operationCompleted((object) this, new OperationCompletedEventArgs(nameof (IncreaseIndent), success));
  }

  private void InitializePrint([NotNull] Graphics graphics, int graphicsWidth, int graphicsHeight)
  {
    if (this._printInitialized)
      return;
    int length = 0;
    List<DataGridViewColumn> dataGridViewColumnList = new List<DataGridViewColumn>();
    foreach (DataGridViewColumn column in (BaseCollection) this.GridView.Columns)
    {
      if (column != null && column.Visible)
      {
        dataGridViewColumnList.Add(column);
        ++length;
      }
    }
    int[] collection = new int[length];
    for (int index = 0; index < length; ++index)
      collection[index] = 0;
    this._printWidths = new List<int>((IEnumerable<int>) collection);
    int index1 = 0;
    foreach (DataGridViewColumn dataGridViewColumn in dataGridViewColumnList)
    {
      string headerText = dataGridViewColumn.HeaderText;
      this._printWidths[index1] = (int) Math.Ceiling((double) graphics.MeasureString(headerText, new Font(this.Font, FontStyle.Bold), graphicsWidth).Width);
      ++index1;
    }
    for (int index2 = 0; index2 < length; ++index2)
    {
      List<int> printWidths;
      int index3;
      (printWidths = this._printWidths)[index3 = index2] = printWidths[index3] + 4;
    }
    foreach (DataGridViewRow row in (IEnumerable) this.GridView.Rows)
    {
      int index4 = 0;
      foreach (DataGridViewColumn dataGridViewColumn in dataGridViewColumnList)
      {
        object obj1 = row.Cells[dataGridViewColumn.Index].FormattedValue ?? (object) string.Empty;
        object obj2;
        if ((obj2 = obj1) is bool)
          obj1 = (bool) obj2 ? (object) Resources.True : (object) Resources.False;
        int num1 = (int) Math.Ceiling((double) graphics.MeasureString(obj1.ToString(), row.Cells[dataGridViewColumn.Index].Style.Font ?? this.Font, graphicsWidth).Width);
        Padding padding = row.Cells[dataGridViewColumn.Index].Style.Padding;
        int left = padding.Left;
        int num2 = num1 + left;
        padding = row.Cells[dataGridViewColumn.Index].Style.Padding;
        int right = padding.Right;
        int num3 = num2 + right;
        if (num3 > this._printWidths[index4])
          this._printWidths[index4] = num3;
        ++index4;
      }
    }
    int num4 = 0;
    int num5 = 0;
    foreach (int printWidth in this._printWidths)
    {
      num4 += printWidth;
      if (num4 > graphicsWidth)
      {
        ++num5;
        num4 = printWidth;
      }
    }
    int num6 = num4 + num5 * graphicsWidth;
    DateTime dateTime = this.Project.Start;
    int num7 = 7;
    do
    {
      DayOfWeek firstDayOfWeek = Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
      while ((double) num7 >= 7.0 && dateTime.DayOfWeek != firstDayOfWeek)
        dateTime = dateTime.AddDays(-1.0);
      int num8 = graphicsWidth - num6;
      if ((double) num8 < (double) this.GanttChart.DayWidth * 7.0)
        num8 = graphicsWidth;
      this._printWidths.Add(num8);
      num7 = (int) ((double) num8 / (double) this.GanttChart.DayWidth);
      if (num7 < 1)
        num7 = 1;
      dateTime = dateTime.AddDays((double) num7);
      num6 = 0;
    }
    while (dateTime <= this.Project.Finish);
    List<string> stringList = new List<string>();
    int num9 = 0;
    for (int index5 = 0; index5 < this._printWidths.Count; ++index5)
    {
      int printWidth = this._printWidths[index5];
      num9 += printWidth;
      if (num9 > graphicsWidth)
      {
        this._printPagesColumns?.Add(stringList);
        stringList = new List<string>();
        num9 = printWidth;
      }
      stringList.Add(index5 < length ? dataGridViewColumnList[index5]?.Index.ToString() ?? string.Empty : $"Gantt Chart {index5 - length}");
    }
    this._printPagesColumns?.Add(stringList);
    this._printPagesFirstRows = new List<int>((IEnumerable<int>) new int[1]
    {
      1
    });
    int columnHeadersHeight = this.GridView.ColumnHeadersHeight;
    int num10 = 1;
    foreach (DataGridViewRow row in (IEnumerable) this.GridView.Rows)
    {
      ++num10;
      columnHeadersHeight += row.Height;
      if (columnHeadersHeight > graphicsHeight)
      {
        this._printPagesFirstRows.Add(num10);
        columnHeadersHeight = this.GridView.ColumnHeadersHeight;
      }
    }
    int index6 = this._printPagesFirstRows.Count - 1;
    if (index6 > 0 && this._printPagesFirstRows[index6] >= this.GridView.Rows.Count)
      this._printPagesFirstRows.RemoveAt(index6);
    this._printInitialized = true;
  }

  private void InitLevelResources([NotNull] object state)
  {
    List<object> objectList = (List<object>) state;
    int num1 = -1;
    int num2 = -1;
    if (this.GridView.CurrentCell != null)
    {
      num1 = this.GridView.CurrentCell.RowIndex;
      num2 = this.GridView.CurrentCell.ColumnIndex;
    }
    bool allowDrag = this.GanttChart.AllowDrag;
    objectList.Add((object) num1);
    objectList.Add((object) num2);
    objectList.Add((object) allowDrag);
    this.Validate();
    this.GridView.ClearSelection();
    EventHandler<OperationStartedEventArgs> operationStarted = this.OperationStarted;
    if (operationStarted != null)
      operationStarted((object) this, new OperationStartedEventArgs("LevelResources"));
    this.GridView.DuringUpdate = true;
  }

  public void InsertNew()
  {
    int index = Math.Max(0, this.SelectedIndex);
    this.Validate();
    EventHandler<OperationStartedEventArgs> operationStarted = this.OperationStarted;
    if (operationStarted != null)
      operationStarted((object) this, new OperationStartedEventArgs(nameof (InsertNew)));
    Task task = this.Project.Tasks.NewElement();
    this.Project.Tasks.Insert(index, task);
    this.GridView.RefreshSpecialProperties();
    this.SelectedTask = task;
    if (this.GridView.CurrentRow != null)
      this.GridView.CurrentRow.Selected = false;
    if (this.GridView.CurrentCell != null)
      this.GridView.CurrentCell.Selected = true;
    EventHandler<OperationCompletedEventArgs> operationCompleted = this.OperationCompleted;
    if (operationCompleted == null)
      return;
    operationCompleted((object) this, new OperationCompletedEventArgs(nameof (InsertNew)));
  }

  public void InsertProject()
  {
    long[] numArray = SelectionWindow.SelectObjects(Localization.GetString("ChooseProject"), string.Empty, (int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.Project, SelectionOptions.SelectObjects | SelectionOptions.ForceRebuildNavTree);
    if (numArray == null || numArray.Length == 0)
      return;
    long objectID = numArray[0];
    int index = Math.Max(0, this.SelectedIndex);
    this.Validate();
    this.Project.InsertProject(objectID, index).RowHeight = 0;
    this.GridView.RefreshSpecialProperties();
    this.GanttChart.Refresh();
  }

  public bool IsExpanded([NotNull] Task task) => this.GridView.IsExpanded(task);

  private void LevelResources([CanBeNull] object state = null)
  {
    List<object> objectList = new List<object>();
    this.Invoke((Delegate) new WaitCallback(this.InitLevelResources), (object) objectList);
    this.Project.LevelResources();
    this.BeginInvoke((Delegate) new WaitCallback(this.CompleteLevelResources), (object) objectList);
    this._levelResourcesThread = (Thread) null;
  }

  public void Minimize([CanBeNull] Task task) => this.GridView.SetExpanded(task, false);

  public void MinimizeAll()
  {
    if (this.Project == null)
      return;
    foreach (Task task in this.Project.Tasks.Where<Task>((Func<Task, bool>) (task => task.HasSubTasks)))
      this.Minimize(task);
  }

  public void PageSetup()
  {
    this.Validate();
    this.GridView.ClearSelection();
    Form parentForm = this.ParentForm;
    try
    {
      int num = (int) this.PageSetupDialog.ShowDialog((IWin32Window) parentForm);
    }
    catch (InvalidPrinterException ex)
    {
      int num = (int) MessageBox.Show((IWin32Window) parentForm, Resources.NoPrinter, parentForm != null ? parentForm.Text : "Project", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool CanPaste
  {
    get
    {
      if (this.Project == null || !this.EditingMode.HasComposition())
        return false;
      return Clipboard.ContainsData("ImProject.Tasks") || Clipboard.ContainsText();
    }
  }

  public void Paste()
  {
    int pos = Math.Max(0, this.SelectedIndex);
    EventHandler<OperationStartedEventArgs> operationStarted = this.OperationStarted;
    if (operationStarted != null)
      operationStarted((object) this, new OperationStartedEventArgs(nameof (Paste)));
    bool success = true;
    bool flag = true;
    try
    {
      if (Clipboard.ContainsData("ImProject.Tasks"))
      {
        using (MemoryStream serializationStream = new MemoryStream())
        {
          if (Clipboard.GetData("ImProject.Tasks") is byte[] data)
            serializationStream.Write(data, 0, data.Length);
          serializationStream.Seek(0L, SeekOrigin.Begin);
          BinaryFormatter binaryFormatter = new BinaryFormatter();
          binaryFormatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
          try
          {
            this.Validate();
            if (binaryFormatter.Deserialize((Stream) serializationStream) is Task[] tasks)
            {
              List<long> longList = (List<long>) null;
              Dictionary<Intermech.Project.Project, Task> dictionary = (Dictionary<Intermech.Project.Project, Task>) null;
              for (int index = 0; index < tasks.Length; ++index)
              {
                if (tasks[index] is Intermech.Project.Project key && key.ObjectID != 0L)
                {
                  if (longList == null)
                  {
                    IReadOnlyCollection<Intermech.Project.Project> subProjects = this.Project.SubProjects;
                    longList = new List<long>(subProjects.Count + 10);
                    foreach (Intermech.Project.Project project in (IEnumerable<Intermech.Project.Project>) subProjects)
                      longList.Add(Math.Abs(project.ObjectID));
                  }
                  long num = Math.Abs(key.ObjectID);
                  if (longList.Contains(num))
                  {
                    Task copyAsTask = key.GetCopyAsTask();
                    copyAsTask._Project = (Intermech.Project.Project) null;
                    copyAsTask.AssignProperties((Task) this.Project);
                    copyAsTask.HasNotLoadedSubTasks = false;
                    tasks[index] = copyAsTask;
                    if (dictionary == null)
                      dictionary = new Dictionary<Intermech.Project.Project, Task>();
                    dictionary.Add(key, copyAsTask);
                  }
                  else
                    longList.Add(num);
                }
              }
              ((IEnumerable<Task>) tasks).ResolveDependencies();
              foreach (Task task in tasks)
              {
                if (task is Intermech.Project.Project project && project._SessionProvider == null)
                  project._SessionProvider = (ISessionProvider) this.Project;
              }
              this.SuspendDrawing();
              try
              {
                this.Project.InsertTasks(pos, tasks);
                if (dictionary != null)
                {
                  foreach (KeyValuePair<Intermech.Project.Project, Task> keyValuePair in dictionary)
                  {
                    Intermech.Project.Project key;
                    Task task1;
                    keyValuePair.Deconstruct<Intermech.Project.Project, Task>(out key, out task1);
                    Intermech.Project.Project project = key;
                    Task task2 = task1;
                    string durationString = project.DurationString;
                    if (task2.CanSetProperty("DurationString", (object) durationString, true) && task2.CanSetProperty("Estimation", (object) durationString, true))
                      task2.DurationString = durationString;
                  }
                }
                this.Project.Tasks.RecalcIndexes();
                for (int index = tasks.Length - 1; index >= 0; --index)
                {
                  Task task = tasks[index];
                  task.UseCache = false;
                  task.ClearCache();
                  if (task.HasSubTasks && !this.GridView.IsExpanded(task))
                  {
                    this.GridView.SetExpanded(task, true);
                    this.GridView.SetExpanded(task, false);
                  }
                }
                this.SelectedTasks = (IReadOnlyList<Task>) tasks;
                this.GridView.CurrentCell = this.GridView.CurrentRow?.Cells[this.NameDataGridViewColumn.Index];
              }
              finally
              {
                this.ResumeDrawing();
              }
            }
            else
              success = false;
          }
          catch (Exception ex)
          {
          }
        }
      }
      else
      {
        if (!Clipboard.ContainsText() || this.DataGridView.SelectedCells.Count != 1)
          return;
        flag = false;
        bool currentCellInEditMode = this.DataGridView.IsCurrentCellInEditMode;
        if (!currentCellInEditMode)
        {
          this.DataGridView.Focus();
          this.DataGridView.BeginEdit(true);
        }
        if (this.DataGridView.EditingControl is TextBox editingControl)
        {
          if (!currentCellInEditMode)
            editingControl.Clear();
          editingControl.Paste();
        }
        if (currentCellInEditMode)
          return;
        this.DataGridView.EndEdit();
      }
    }
    finally
    {
      if (flag)
        this.GridView.ClearSelection();
      EventHandler<OperationCompletedEventArgs> operationCompleted = this.OperationCompleted;
      if (operationCompleted != null)
        operationCompleted((object) this, new OperationCompletedEventArgs(nameof (Paste), success));
    }
  }

  public void Print() => this.Print(true);

  public void Print(bool askSetup)
  {
    if (this.Project == null)
      return;
    this.Validate();
    this.GridView.ClearSelection();
    DialogResult dialogResult = DialogResult.OK;
    if (askSetup)
      dialogResult = this.PrintDialog.ShowDialog((IWin32Window) this.ParentForm);
    if (dialogResult != DialogResult.OK)
      return;
    this.PrintDocument.Print();
  }

  private void printDocument_BeginPrint([CanBeNull] object sender, [NotNull] PrintEventArgs e)
  {
    this.BeginPrint();
  }

  private void printDocument_EndPrint([CanBeNull] object sender, [NotNull] PrintEventArgs e)
  {
    this.EndPrint();
  }

  private void printDocument_PrintPage([CanBeNull] object sender, [NotNull] PrintPageEventArgs e)
  {
    Graphics graphics = e.Graphics;
    Rectangle marginBounds = e.MarginBounds;
    int width = marginBounds.Width;
    marginBounds = e.MarginBounds;
    int height = marginBounds.Height;
    this.InitializePrint(graphics, width, height);
    this.PrintPage(e.Graphics);
    e.HasMorePages = this.IncreaseCurrentPage();
  }

  private void PrintPage([NotNull] Graphics graphics)
  {
    int index1 = this._printCurrentPage % this._printPagesColumns.Count;
    int index2 = this._printCurrentPage / this._printPagesColumns.Count;
    int num1 = 0;
    Color color = Color.FromArgb(242, 240 /*0xF0*/, 222);
    Brush brush = (Brush) new SolidBrush(color);
    Pen controlLightLightPen = new Pen(brush);
    StringFormat stringFormat1 = new StringFormat();
    stringFormat1.Alignment = StringAlignment.Near;
    stringFormat1.LineAlignment = StringAlignment.Center;
    StringFormat stringFormat2 = new StringFormat();
    stringFormat2.Alignment = StringAlignment.Center;
    stringFormat2.LineAlignment = StringAlignment.Center;
    StringFormat stringFormat3 = new StringFormat();
    stringFormat3.Alignment = StringAlignment.Far;
    stringFormat3.LineAlignment = StringAlignment.Center;
    int index3 = 0;
    for (int index4 = 0; index4 < index1; ++index4)
    {
      int num2 = index3;
      List<string> printPagesColumn = this._printPagesColumns[index4];
      // ISSUE: explicit non-virtual call
      int count = printPagesColumn != null ? __nonvirtual (printPagesColumn.Count) : 0;
      index3 = num2 + count;
    }
    foreach (string s in this._printPagesColumns[index1])
    {
      if (!s.StartsWith("Gantt Chart"))
      {
        int index5 = int.Parse(s);
        this._printGanttChartCurrentDate = this.Project.Start;
        this._printGanttChartCurrentDays = 7;
        StringFormat stringFormat4;
        switch (this.GridView.Columns[index5] != null ? this.GridView.Columns[index5].DefaultCellStyle.Alignment : DataGridViewContentAlignment.NotSet)
        {
          case DataGridViewContentAlignment.NotSet:
          case DataGridViewContentAlignment.TopLeft:
          case DataGridViewContentAlignment.MiddleLeft:
          case DataGridViewContentAlignment.BottomLeft:
            stringFormat4 = stringFormat1;
            break;
          case DataGridViewContentAlignment.TopRight:
          case DataGridViewContentAlignment.MiddleRight:
          case DataGridViewContentAlignment.BottomRight:
            stringFormat4 = stringFormat3;
            break;
          default:
            stringFormat4 = stringFormat2;
            break;
        }
        StringFormat format = stringFormat4;
        int y = 0;
        int num3 = 0;
        while (num3 < (index2 < this._printPagesFirstRows.Count - 1 ? this._printPagesFirstRows[index2 + 1] : this.GridView.Rows.Count))
        {
          if (num3 == 0)
          {
            graphics.FillRectangle(brush, num1, y, this._printWidths[index3], this.GridView.ColumnHeadersHeight);
            graphics.DrawRectangle(Pens.Black, num1, y, this._printWidths[index3], this.GridView.ColumnHeadersHeight);
            object headerText = (object) this.GridView.Columns[index5].HeaderText;
            int num4 = this._printWidths[index3] / 2;
            switch (format.Alignment)
            {
              case StringAlignment.Near:
                num4 = 2;
                break;
              case StringAlignment.Center:
                num4 = this._printWidths[index3] / 2;
                break;
              case StringAlignment.Far:
                num4 = this._printWidths[index3] - 2;
                break;
            }
            graphics.DrawString(headerText.ToString(), new Font(this.Font, FontStyle.Bold), Brushes.Black, (float) (num1 + num4), (float) (y + this.GridView.ColumnHeadersHeight / 2), format);
            y += this.GridView.ColumnHeadersHeight;
          }
          else
          {
            graphics.FillRectangle(Brushes.White, num1, y, this._printWidths[index3], this.GridView.Rows[num3 - 1].Height);
            graphics.DrawRectangle(Pens.Black, num1, y, this._printWidths[index3], this.GridView.Rows[num3 - 1].Height);
            object obj1 = this.GridView.Rows[num3 - 1].Cells[index5].FormattedValue ?? (object) string.Empty;
            object obj2;
            if ((obj2 = obj1) is bool)
              obj1 = (bool) obj2 ? (object) Resources.True : (object) Resources.False;
            int num5 = this._printWidths[index3] / 2;
            switch (format.Alignment)
            {
              case StringAlignment.Near:
                num5 = 2 + this.GridView.Rows[num3 - 1].Cells[index5].Style.Padding.Left;
                break;
              case StringAlignment.Center:
                num5 = this._printWidths[index3] / 2;
                break;
              case StringAlignment.Far:
                num5 = this._printWidths[index3] - 2 - this.GridView.Rows[num3 - 1].Cells[index5].Style.Padding.Right;
                break;
            }
            GraphicsState gstate = graphics.Save();
            try
            {
              graphics.SetClip(new Rectangle(num1, y, this._printWidths[index3], this.GridView.Rows[num3 - 1].Height));
              graphics.DrawString(obj1.ToString(), this.GridView.Rows[num3 - 1].Cells[index5].Style.Font ?? this.Font, Brushes.Black, (float) (num1 + num5), (float) (y + this.GridView.Rows[num3 - 1].Height / 2), format);
            }
            finally
            {
              graphics.Restore(gstate);
            }
            y += this.GridView.Rows[num3 - 1].Height;
          }
          if (num3 > 0)
            ++num3;
          else
            num3 = this._printPagesFirstRows[index2];
        }
      }
      else
      {
        int visibleTaskIndex = this._printPagesFirstRows[index2] - 1;
        int visibleTaskCount = (index2 < this._printPagesFirstRows.Count - 1 ? this._printPagesFirstRows[index2 + 1] : this.GridView.Rows.Count) - visibleTaskIndex - 1;
        DateTime currentDate = this._printGanttChartCurrentDate;
        DayOfWeek firstDayOfWeek = Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
        while (currentDate.DayOfWeek != firstDayOfWeek && this._printGanttChartCurrentDays >= 7)
          currentDate = currentDate.AddDays(-1.0);
        int rowHeight = this.GanttChart.RowHeight;
        this._days = (int) ((double) this._printWidths[index3] / (double) this.GanttChart.DayWidth);
        float width = this.GanttChart.DayWidth * (float) this._days;
        int height = this.GridView.ColumnHeadersHeight + rowHeight * visibleTaskCount;
        GraphicsState gstate = graphics.Save();
        graphics.SetClip(new RectangleF((float) num1, 0.0f, width, (float) height));
        this.GanttChart.Print(graphics, num1, 1, this.Project, visibleTaskIndex, visibleTaskCount, currentDate, this._days, this.GridView.ColumnHeadersHeight, rowHeight, this.GanttChart.DayWidth, height, this.Font, brush, Brushes.Black, Pens.Black, controlLightLightPen, color, Color.White, Color.Black, (Brush) new HatchBrush(HatchStyle.Percent50, Color.Blue, Color.Transparent), (Brush) new HatchBrush(HatchStyle.Percent50, Color.Red, Color.Transparent), Brushes.Black, Brushes.Black, Brushes.Black, (Brush) new HatchBrush(HatchStyle.Percent75, Color.Red, Color.Transparent), Pens.Blue, Pens.Red, Pens.Black, Pens.Black, Pens.Green, new Pen(Color.Red, 3f), this.ScaleType, this.HighlightCriticalTasks, this.UseNumericScaleValues, this.NumericScaleType, (Brush) new HatchBrush(HatchStyle.Percent25, Color.Gray, SystemColors.Window), new Pen((Brush) new HatchBrush(HatchStyle.Percent50, SystemColors.ControlDark, SystemColors.Window)), new Pen((Brush) new HatchBrush(HatchStyle.Percent50, SystemColors.ControlDarkDark, SystemColors.Window)), (Dictionary<Task, Pen>) null, (Dictionary<Task, Brush>) null, this.RectangleRoundnessPercent, this.RectangleHeightPercent);
        graphics.Restore(gstate);
        graphics.DrawRectangle(Pens.Black, (float) num1, 0.0f, width, (float) height);
        if (this._days < 1)
          this._days = 1;
        this._printGanttChartCurrentDate = currentDate.AddDays((double) this._days);
        this._printGanttChartCurrentDays = this._days;
      }
      num1 += this._printWidths[index3++];
    }
  }

  public void PrintPreview()
  {
    if (this.Project == null)
      return;
    this.Validate();
    this.GridView.ClearSelection();
    Form parentForm = this.ParentForm;
    PrintPreviewUtilities.ApplyResources(this.PrintPreviewDialog);
    try
    {
      int num = (int) this.PrintPreviewDialog.ShowDialog((IWin32Window) this.ParentForm);
    }
    catch (InvalidPrinterException ex)
    {
      int num = (int) MessageBox.Show((IWin32Window) parentForm, Resources.NoPrinter, parentForm != null ? parentForm.Text : "Project", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
  }

  private void Project_PropertyChanged([CanBeNull] object sender, [NotNull] PropertyChangedEventArgs e)
  {
    this.HasChanges = true;
    if (this.AutoLevelResources && !this._duringAutoLevelResources)
    {
      this.TimerAutoLevelResources.Stop();
      this.TimerAutoLevelResources.Start();
    }
    if (!(e.PropertyName == "Start") || this.Project.PlanningType != PlanningType.FromStart)
      return;
    this.InitialDate = this.Project.Start;
  }

  private void projectDataGridView_CellBeginEdit([CanBeNull] object sender, [NotNull] DataGridViewCellCancelEventArgs e)
  {
    EventHandler<OperationStartedEventArgs> operationStarted = this.OperationStarted;
    if (operationStarted == null)
      return;
    operationStarted((object) this, new OperationStartedEventArgs("DataGridUpdate"));
  }

  private void projectDataGridView_CellEndEdit([CanBeNull] object sender, [NotNull] DataGridViewCellEventArgs e)
  {
    EventHandler<OperationCompletedEventArgs> operationCompleted = this.OperationCompleted;
    if (operationCompleted == null)
      return;
    operationCompleted((object) this, new OperationCompletedEventArgs("DataGridUpdate", true));
  }

  private void projectDataGridView_CurrentCellChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    EventHandler selectionChanged = this.SelectionChanged;
    if (selectionChanged == null)
      return;
    selectionChanged((object) this, EventArgs.Empty);
  }

  private void projectDataGridView_DataError([CanBeNull] object sender, [NotNull] DataGridViewDataErrorEventArgs e)
  {
    EventHandler<OperationCompletedEventArgs> operationCompleted = this.OperationCompleted;
    if (operationCompleted == null)
      return;
    operationCompleted((object) this, new OperationCompletedEventArgs("DataGridUpdate", false, e.Exception));
  }

  private void projectDataGridView_KeyDown([CanBeNull] object sender, [NotNull] KeyEventArgs e)
  {
    if (e.KeyCode == Keys.Apps)
    {
      if (this.ContextMenuRequested == null)
        return;
      Point currentCellAddress = this.GridView.CurrentCellAddress;
      Rectangle displayRectangle = this.GridView.GetCellDisplayRectangle(currentCellAddress.X, currentCellAddress.Y, true);
      this.ContextMenuRequested((object) this, new MouseEventArgs(MouseButtons.None, 0, displayRectangle.Right - 10, displayRectangle.Bottom - 10, 0));
    }
    else
    {
      if (e.KeyCode != Keys.Delete || !this.CanDelete)
        return;
      this.Delete();
    }
  }

  private void projectDataGridView_MouseUp([CanBeNull] object sender, [NotNull] MouseEventArgs e)
  {
    if (!this.UseDataGridViewRowHeaderContextMenu)
      return;
    System.Windows.Forms.DataGridView.HitTestInfo hitTestInfo = this.GridView.HitTest(e.X, e.Y);
    if (e.Button != MouseButtons.Right || hitTestInfo.Type != DataGridViewHitTestType.RowHeader && hitTestInfo.Type != DataGridViewHitTestType.Cell)
      return;
    if (hitTestInfo.Type == DataGridViewHitTestType.Cell)
      this.GridView.CurrentCell = this.GridView.Rows[hitTestInfo.RowIndex].Cells[hitTestInfo.ColumnIndex];
    if (this.GridView.IsCurrentCellInEditMode)
      return;
    MouseEventHandler contextMenuRequested = this.ContextMenuRequested;
    if (contextMenuRequested == null)
      return;
    contextMenuRequested((object) this, e);
  }

  private void projectDataGridView_Resize([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.RefreshGanttView();
  }

  private void GridView_CellDoubleClick([CanBeNull] object sender, [NotNull] DataGridViewCellEventArgs e)
  {
    if (e.RowIndex == -1 && Cursor.Current == Cursors.SizeWE)
    {
      if (e.ColumnIndex < 0 || e.ColumnIndex >= this.GridView.ColumnCount || this.GridView.Columns[e.ColumnIndex] == null)
        return;
      this.GridView.AutoResizeColumn(e.ColumnIndex);
    }
    else if (e.ColumnIndex == -1 && Cursor.Current == Cursors.SizeNS)
    {
      this.GridView.AutoResizeRow(e.RowIndex, DataGridViewAutoSizeRowMode.AllCells);
    }
    else
    {
      if (this.GridView.IsMouseOverPlusMinus(e) || !this.AllowEditForm || e.RowIndex < 0 || e.ColumnIndex < 0)
        return;
      if (this.GridView.CurrentCell != null && this.GridView.CurrentCell.ColumnIndex == e.ColumnIndex && this.GridView.CurrentCell.RowIndex == e.RowIndex)
      {
        string initialParamName = (string) null;
        if (e.ColumnIndex >= 0 && e.ColumnIndex < this.GridView.ColumnCount)
        {
          DataGridViewColumn column = this.GridView.Columns[e.ColumnIndex];
          if (column != null)
            initialParamName = column.DataPropertyName;
        }
        this.Edit(initialParamName);
      }
      else
        this.GridView.ClearSelection();
    }
  }

  private void projectDataGridView_RowsAdded([CanBeNull] object sender, [NotNull] DataGridViewRowsAddedEventArgs e)
  {
    this.RefreshGanttView();
  }

  private void projectDataGridView_RowsRemoved([CanBeNull] object sender, [NotNull] DataGridViewRowsRemovedEventArgs e)
  {
    this.RefreshGanttView();
  }

  private void projectDataGridView_Scroll([CanBeNull] object sender, [NotNull] ScrollEventArgs e)
  {
    this.RefreshGanttView();
  }

  private void projectDataGridView_SelectionChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this.SelectionChanged == null)
      return;
    this.SelectionChanged((object) this, EventArgs.Empty);
  }

  private void projectDataGridView_TaskExpanded([CanBeNull] object sender, [CanBeNull] TaskExpandedEventArgs e)
  {
    this.RefreshGanttView();
    if (this.TaskExpandedChanged == null || e == null)
      return;
    this.TaskExpandedChanged((object) this, new TaskExpandedChangedEventArgs(e.Task, e.Expanded));
  }

  private void projectGanttChartView_DragDropCompleted([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    EventHandler<OperationCompletedEventArgs> operationCompleted1 = this.OperationCompleted;
    if (operationCompleted1 != null)
      operationCompleted1((object) this, new OperationCompletedEventArgs("GanttChartDrag", true));
    EventHandler<OperationCompletedEventArgs> operationCompleted2 = this.OperationCompleted;
    if (operationCompleted2 == null)
      return;
    operationCompleted2((object) this, new OperationCompletedEventArgs("DataGridUpdate", true));
  }

  private void projectGanttChartView_DragDropStarted([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.Validate();
    EventHandler<OperationStartedEventArgs> operationStarted1 = this.OperationStarted;
    if (operationStarted1 != null)
      operationStarted1((object) this, new OperationStartedEventArgs("DataGridUpdate"));
    EventHandler<OperationStartedEventArgs> operationStarted2 = this.OperationStarted;
    if (operationStarted2 != null)
      operationStarted2((object) this, new OperationStartedEventArgs("GanttChartDrag"));
    this.SelectedTask = this.GanttChart.Task;
  }

  private void projectGanttChartView_GanttChartPaint([CanBeNull] object sender, [NotNull] PaintEventArgs e)
  {
    PaintEventHandler ganttChartPaint = this.GanttChartPaint;
    if (ganttChartPaint == null)
      return;
    ganttChartPaint((object) this, e);
  }

  private void ProjectView_Load([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    DataGridViewRow rowTemplate = this.DataGridView.RowTemplate;
    this.RowHeight = rowTemplate != null ? rowTemplate.Height : 20;
    int num = this.VerticalScrollBar.Width - 17;
    this.VerticalScrollBar.Left -= num;
    this.VerticalScrollBar.Height -= num;
    this.SplitContainer.Width -= num;
  }

  private void ProjectView_PropertyChanged([CanBeNull] object sender, [NotNull] PropertyChangedEventArgs e)
  {
    if (!(e.PropertyName == "TaskToolStripButtonsEnabled"))
      return;
    EventHandler<OperationCompletedEventArgs> operationCompleted = this.OperationCompleted;
    if (operationCompleted == null)
      return;
    operationCompleted((object) this, new OperationCompletedEventArgs("DataGridViewEditEnabledChanged"));
  }

  private void RefreshData()
  {
    this.GridView.Project = this.Project;
    this.GridView.Invalidate();
    this.GanttChart.Project = this.Project;
    this.RefreshGanttView();
  }

  public void RefreshGanttView()
  {
    if (this.GridView.Project != this.GanttChart.Project)
      return;
    this._refreshGanttViewEventsAccumulator.Event();
  }

  private void _refreshGanttView()
  {
    if (this.GridView.Project != this.GanttChart.Project)
      return;
    int num = this.GridView.DisplayedRowCount(false);
    int visibleIndex = Math.Max(0, this.GridView.FirstDisplayedScrollingRowIndex);
    if (this.GanttChart.DisplayedRowCount == num)
      this.GanttChart.Invalidate();
    this.GanttChart.DisplayedRowCount = num;
    this.GanttChart.FirstDisplayedScrollingRowIndex = visibleIndex;
    int rowHeight = this.GanttChart.RowHeight;
    int height = visibleIndex + num <= this.GridView.NewRowIndex ? this.GridView.GetRowDisplayRectangle(visibleIndex + num, true).Height : 0;
    this.VerticalScrollBar.Minimum = 0;
    this.VerticalScrollBar.Maximum = Math.Max(0, this.VisibleRowCount * rowHeight);
    this.VerticalScrollBar.SmallChange = rowHeight;
    this.VerticalScrollBar.LargeChange = num * rowHeight + height;
    this.VerticalScrollBar.Value = this.GetIndex(visibleIndex) * rowHeight;
    this.VerticalScrollBar.Enabled = this.VerticalScrollBar.Maximum > num * rowHeight;
    this.GridView.RecalcRowHeights();
  }

  public void SetExpanded([CanBeNull] Task task, bool expanded)
  {
    this.GridView.SetExpanded(task, expanded);
  }

  public void SetTaskDrawingSettings([NotNull] Task task, [CanBeNull] Pen pen, [CanBeNull] Brush brush)
  {
    if (pen != null)
    {
      if (this._taskPens.ContainsKey(task))
        this._taskPens[task] = pen;
      else
        this._taskPens.Add(task, pen);
    }
    else
      this._taskPens.Remove(task);
    if (brush != null)
    {
      if (this._taskBrushes.ContainsKey(task))
        this._taskBrushes[task] = brush;
      else
        this._taskBrushes.Add(task, brush);
    }
    else
      this._taskBrushes.Remove(task);
    this.GanttChart.Invalidate();
  }

  public void ShowCustomEditPanel([NotNull] Control control, [NotNull] string title)
  {
    this.ShowCustomEditPanel(control, title, this.LabelIcon.Image);
  }

  public void ShowCustomEditPanel([NotNull] Control control, [NotNull] string title, [CanBeNull] Image icon)
  {
    this.Validate();
    this.GridView.ClearSelection();
    bool duringShowEditPanel = this._duringShowEditPanel;
    this._duringShowEditPanel = true;
    this.ClearEditPanel();
    this.EditPanel.Controls.Add(control);
    this.EditPanel.Controls.SetChildIndex(control, 0);
    this.EditPanelTitle = title;
    control.Dock = DockStyle.Fill;
    this.EditPanelTitleIconLabel.Image = icon;
    this.EditPanelTitleLabel.Text = title;
    this.MainSplitContainer.Panel2Collapsed = false;
    this._duringShowEditPanel = duringShowEditPanel;
  }

  public void SplitRemainingWork()
  {
    this.Validate();
    EventHandler<OperationStartedEventArgs> operationStarted = this.OperationStarted;
    if (operationStarted != null)
      operationStarted((object) this, new OperationStartedEventArgs(nameof (SplitRemainingWork)));
    bool success = true;
    foreach (Task selectedTask in (IEnumerable<Task>) this.SelectedTasks)
    {
      try
      {
        selectedTask.SplitRemainingWork();
      }
      catch (InvalidOperationException ex)
      {
        success = false;
      }
    }
    EventHandler<OperationCompletedEventArgs> operationCompleted = this.OperationCompleted;
    if (operationCompleted == null)
      return;
    operationCompleted((object) this, new OperationCompletedEventArgs(nameof (SplitRemainingWork), success));
  }

  private void timerAutoLevelResources_Tick([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this.Project == null || this.GridView.IsCurrentCellInEditMode || this.GridView.CurrentRow != null && this.GridView.CurrentRow.Index == this.GridView.NewRowIndex)
      return;
    this.TimerAutoLevelResources.Stop();
    this._duringAutoLevelResources = true;
    this.Project.LevelResources();
    this._duringAutoLevelResources = false;
  }

  private void vScrollBar_ValueChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    int rowHeight = this.GanttChart.RowHeight;
    if (this.VerticalScrollBar.Value >= this.VisibleRowCount * rowHeight)
      return;
    try
    {
      this.GridView.FirstDisplayedScrollingRowIndex = this.GetVisibleIndex((int) Math.Round((double) this.VerticalScrollBar.Value / (double) rowHeight));
    }
    catch (InvalidOperationException ex)
    {
    }
    catch (ArgumentOutOfRangeException ex)
    {
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public EditingMode EditingMode
  {
    get
    {
      ClientProject project = this._project;
      return project == null ? EditingMode.None : project.EditingMode;
    }
  }

  [Description("Indicates whether or not to allow form-based edit action in the grid")]
  [Category("Behavior")]
  [DefaultValue(false)]
  public bool AllowEditForm
  {
    get => this._allowEditForm;
    set
    {
      if (value == this.AllowEditForm)
        return;
      this._allowEditForm = value;
    }
  }

  [Description("Indicates whether or not to allow drag and drop interactive actions in the Gantt chart")]
  [DefaultValue(false)]
  [Category("Behavior")]
  public bool AllowGanttChartTaskDrag
  {
    get => this.GanttChart.AllowDrag;
    set
    {
      if (value == this.AllowGanttChartTaskDrag)
        return;
      this.GanttChart.AllowDrag = value;
    }
  }

  [Browsable(false)]
  [DefaultValue(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool AutoLevelResources
  {
    get => this._autoLevelResources;
    set
    {
      if (value == this.AutoLevelResources)
        return;
      this._autoLevelResources = value;
      this.GanttChart.AllowDragStart = !this.AutoLevelResources;
      if (!this.AutoLevelResources)
        return;
      this.TimerAutoLevelResources.Start();
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Brush CriticalTaskBrush
  {
    get => this.GanttChart.CriticalTaskBrush;
    set
    {
      if (value == this.CriticalTaskBrush)
        return;
      this.GanttChart.CriticalTaskBrush = value;
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Pen CriticalTaskPen
  {
    get => this.GanttChart.CriticalTaskPen;
    set
    {
      if (value == this.CriticalTaskPen)
        return;
      this.GanttChart.CriticalTaskPen = value;
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public DateTime CurrentDate
  {
    get => this.GanttChart.CurrentDate;
    set
    {
      if (!(value != this.CurrentDate))
        return;
      this.GanttChart.CurrentDate = value;
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public HScrollBar CurrentDateScrollBar => this.GanttChart.CurrentDateScrollBar;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int CurrentDateScrollMaximumValue
  {
    get => this.GanttChart.CurrentDateScrollMaximumValue;
    set
    {
      if (value == this.CurrentDateScrollMaximumValue)
        return;
      this.GanttChart.CurrentDateScrollMaximumValue = value;
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ProjectDataGridView DataGridView => this.GridView;

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public DataGridViewColumnCollection DataGridViewColumnCollection => this.GridView.Columns;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public float DayWidth
  {
    get => this.GanttChart.DayWidth;
    set
    {
      if ((double) value == (double) this.DayWidth)
        return;
      this.GanttChart.DayWidth = (double) value >= 0.5 && (double) value <= 160.0 ? value : throw new ArgumentOutOfRangeException(nameof (DayWidth), "DayWidth must be between 0.5 and 160");
    }
  }

  [Browsable(false)]
  [DefaultValue(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool EditPanelFocused => ControlUtilities.ContainsFocus((Control) this.EditPanel);

  [Browsable(false)]
  [DefaultValue(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public string EditPanelTitle { get; private set; }

  [Browsable(false)]
  [DefaultValue(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool EditPanelVisible => this.EditPanel.Visible;

  [DefaultValue(null)]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public string FileName { get; set; }

  [DefaultValue(false)]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool HasChanges { get; set; }

  [Description("Specifies whether to highlight critical tasks in the Gantt chart. A task is critical if it would generate delays in the project in case that it wouldn't be finished on time.")]
  [DefaultValue(false)]
  [Category("Appearance")]
  public bool HighlightCriticalTasks
  {
    get => this.GanttChart.HighlightCriticalTasks;
    set
    {
      if (value == this.HighlightCriticalTasks)
        return;
      this.GanttChart.HighlightCriticalTasks = value;
    }
  }

  [Description("Отображать сетку на диаграмме")]
  [DefaultValue(false)]
  [Category("Appearance")]
  public bool ShowGrid
  {
    get => this.GanttChart.ShowGrid;
    set
    {
      if (this.ShowGrid == value)
        return;
      this.GanttChart.ShowGrid = value;
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public DateTime InitialDate
  {
    get => this.GanttChart.InitialDate;
    set
    {
      if (!(value != this.InitialDate))
        return;
      this.GanttChart.InitialDate = value;
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Pen MetConstraintPen
  {
    get => this.GanttChart.MetConstraintPen;
    set
    {
      if (value == this.MetConstraintPen)
        return;
      this.GanttChart.MetConstraintPen = value;
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Brush MilestoneTaskBrush
  {
    get => this.GanttChart.MilestoneTaskBrush;
    set
    {
      if (value == this.MilestoneTaskBrush)
        return;
      this.GanttChart.MilestoneTaskBrush = value;
    }
  }

  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public DataGridViewColumn NameDataGridViewColumn
  {
    get => this.DataGridViewColumnCollection["nameDataGridViewColumn"];
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Brush NonWorkingDayBrush
  {
    get => this.GanttChart.NonWorkingDayBrush;
    set
    {
      if (value == this.NonWorkingDayBrush)
        return;
      this.GanttChart.NonWorkingDayBrush = value;
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Pen NotMetConstraintPen
  {
    get => this.GanttChart.NotMetConstraintPen;
    set
    {
      if (value == this.NotMetConstraintPen)
        return;
      this.GanttChart.NotMetConstraintPen = value;
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public NumericScaleType NumericScaleType
  {
    get => this.GanttChart.NumericScaleType;
    set
    {
      if (value == this.NumericScaleType)
        return;
      this.GanttChart.NumericScaleType = value;
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Brush ParentTaskBrush
  {
    get => this.GanttChart.ParentTaskBrush;
    set
    {
      if (value == this.ParentTaskBrush)
        return;
      this.GanttChart.ParentTaskBrush = value;
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Pen ParentTaskPen
  {
    get => this.GanttChart.ParentTaskPen;
    set
    {
      if (value == this.ParentTaskPen)
        return;
      this.GanttChart.ParentTaskPen = value;
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Brush PercentCompletedBrush
  {
    get => this.GanttChart.PercentCompletedBrush;
    set
    {
      if (value == this.PercentCompletedBrush)
        return;
      this.GanttChart.PercentCompletedBrush = value;
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Brush PercentNotCompletedBrush
  {
    get => this.GanttChart.PercentNotCompletedBrush;
    set
    {
      if (value == this.PercentNotCompletedBrush)
        return;
      this.GanttChart.PercentNotCompletedBrush = value;
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Pen PeriodLinePen
  {
    get => this.GanttChart.PeriodLinePen;
    set
    {
      if (value == this.PeriodLinePen)
        return;
      this.GanttChart.PeriodLinePen = value;
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  private int PrintPagesCount
  {
    get
    {
      List<int> printPagesFirstRows = this._printPagesFirstRows;
      int count1 = printPagesFirstRows != null ? __nonvirtual (printPagesFirstRows.Count) : 0;
      List<List<string>> printPagesColumns = this._printPagesColumns;
      int count2 = printPagesColumns != null ? __nonvirtual (printPagesColumns.Count) : 0;
      return count1 * count2;
    }
  }

  [CanBeNull]
  [Browsable(false)]
  [DefaultValue(null)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ClientProject Project
  {
    get => this._project;
    set
    {
      if (this.Project != null)
      {
        this.Project.PropertyChanged -= new PropertyChangedEventHandler(this.Project_PropertyChanged);
        this.Project.OnRequestExpand -= new Intermech.Project.Project.TaskRequestHandler(this.Expand);
        if (this.ServiceContainer != null)
        {
          this.RemoveService<ClientProject>();
          this.RemoveService<Intermech.Project.Project>();
        }
      }
      this._project = value;
      if (value != null && this.ServiceContainer != null)
      {
        this.AddService<ClientProject>(value);
        this.AddService<Intermech.Project.Project>((Intermech.Project.Project) value);
      }
      if (this._project != null)
        this._project.DisplayOptions.View = this;
      TimeSpan timeSpan = value.Finish.Subtract(value.Start);
      int num1 = 0;
      int num2 = 0;
      if (value.PlanningType == PlanningType.FromEnd)
        num1 -= 92;
      this.InitialDate = value.Start.AddDays((double) num1);
      this.CurrentDateScrollMaximumValue = timeSpan.Days + 92;
      this.CurrentDate = value.Start.AddDays((double) num2);
      this.RefreshData();
      this.HasChanges = false;
      if (this.Project != null)
      {
        this.Project.PropertyChanged += new PropertyChangedEventHandler(this.Project_PropertyChanged);
        this.Project.OnRequestExpand += new Intermech.Project.Project.TaskRequestHandler(this.Expand);
      }
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged((object) this, new PropertyChangedEventArgs(nameof (Project)));
    }
  }

  [Category("Appearance")]
  [DefaultValue(0.5f)]
  public float RectangleHeightPercent
  {
    get => this.GanttChart.RectangleHeightPercent;
    set
    {
      if ((double) value == (double) this.RectangleHeightPercent)
        return;
      this.GanttChart.RectangleHeightPercent = value;
    }
  }

  [Category("Appearance")]
  [DefaultValue(0.0f)]
  public float RectangleRoundnessPercent
  {
    get => this.GanttChart.RectangleRoundnessPercent;
    set
    {
      if ((double) value == (double) this.RectangleRoundnessPercent)
        return;
      this.GanttChart.RectangleRoundnessPercent = value;
    }
  }

  [Category("Appearance")]
  [Description("Specifies the data grid view row height")]
  [DefaultValue(22)]
  public int RowHeight
  {
    get
    {
      DataGridViewRow rowTemplate = this.GridView.RowTemplate;
      return Math.Max(rowTemplate != null ? rowTemplate.Height : 0, this.GanttChart.RowHeight);
    }
    set
    {
      if (this.GridView.RowTemplate == null)
        return;
      this.GridView.RowTemplate.Height = this.GanttChart.RowHeight = value;
    }
  }

  [Description("Specifies the scale type of the Gantt chart, such as weeks, months, quarters, or years")]
  [Category("Appearance")]
  [DefaultValue(1)]
  public ScaleType ScaleType
  {
    get => this.GanttChart.ScaleType;
    set
    {
      if (value == this.ScaleType)
        return;
      this.GanttChart.ScaleType = value >= ScaleType.Days && value <= ScaleType.Years ? value : throw new ArgumentOutOfRangeException(nameof (ScaleType), "ScaleType must be between Days and Years");
      if (this.DesignMode)
        return;
      if (this.ScaleType == ScaleType.Days)
      {
        if (!(this.CurrentDate < DateTime.Today))
          return;
        this.CurrentDate = DateTime.Today;
      }
      else
      {
        if (!(this.CurrentDate < this.GanttChart.InitialDate))
          return;
        this.CurrentDate = this.GanttChart.InitialDate;
      }
    }
  }

  [Browsable(false)]
  [DefaultValue(null)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int SelectedIndex
  {
    get => this.GridView.CurrentRow == null ? -1 : this.GridView.CurrentRow.Index;
  }

  [CanBeNull]
  [Browsable(false)]
  [DefaultValue(null)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Task SelectedTask
  {
    get => this.SelectedTasks.FirstOrDefault<Task>();
    set
    {
      Task[] taskArray;
      if (value == null)
        taskArray = Array.Empty<Task>();
      else
        taskArray = new Task[1]{ value };
      this.SelectedTasks = (IReadOnlyList<Task>) taskArray;
    }
  }

  [NotNull]
  [ItemNotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IReadOnlyList<Task> SelectedTasks
  {
    get => this.GridView.SelectedTasks;
    set => this.GridView.SelectedTasks = value;
  }

  [NotNull]
  private IReadOnlyCollection<Task> GetSelectedTasksWithAllSubtasks(
    bool autoLoadSubProjects = false,
    [CanBeNull] ProjectView.BeforeSubProjectLoadingDelegate beforeSubProjectLoading = null,
    [CanBeNull] ProjectView.AfterSubProjectLoadingDelegate afterSubProjectLoading = null)
  {
    IReadOnlyList<Task> selectedTasks = this.GridView.SelectedTasks;
    if (selectedTasks.Count <= 0)
      return (IReadOnlyCollection<Task>) Task.EmptyTasksArray;
    HashSet<Task> source = new HashSet<Task>();
    foreach (Task task in (IEnumerable<Task>) selectedTasks)
    {
      source.Add(task);
      if (autoLoadSubProjects && task is Intermech.Project.Project subProject && subProject.HasNotLoadedSubTasks)
      {
        bool loadSubTasksResult = false;
        object savedState = beforeSubProjectLoading != null ? beforeSubProjectLoading(subProject) : (object) null;
        try
        {
          loadSubTasksResult = subProject.LoadSubTasks(recursive: true);
        }
        finally
        {
          if (afterSubProjectLoading != null)
            afterSubProjectLoading(subProject, loadSubTasksResult, savedState);
        }
      }
      foreach (Task allSubTask in (IEnumerable<Task>) task.AllSubTasks)
        source.Add(allSubTask);
    }
    return source.GetAsReadOnlyCollection<Task>();
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IReadOnlyCollection<Task> SelectedTasksWithAllSubtasks
  {
    get => this.GetSelectedTasksWithAllSubtasks();
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Brush StandardTaskBrush
  {
    get => this.GanttChart.StandardTaskBrush;
    set
    {
      if (value == this.StandardTaskBrush)
        return;
      this.GanttChart.StandardTaskBrush = value;
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Pen StandardTaskPen
  {
    get => this.GanttChart.StandardTaskPen;
    set
    {
      if (value == this.StandardTaskPen)
        return;
      this.GanttChart.StandardTaskPen = value;
    }
  }

  [DefaultValue(false)]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool TaskToolStripButtonsEnabled
  {
    get
    {
      return this.SelectedIndex < this.GridView.NewRowIndex || this.SelectedTasks.GetEnumerator().MoveNext();
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Pen TodayLinePen
  {
    get => this.GanttChart.TodayLinePen;
    set
    {
      if (value == this.TodayLinePen)
        return;
      this.GanttChart.TodayLinePen = value;
    }
  }

  [DefaultValue(false)]
  [Category("Behavior")]
  [Description("Indicates whether to use the grid view column header context menu")]
  public bool UseDataGridViewColumnHeaderContextMenu
  {
    get => this.GridView.UseColumnHeaderContextMenu;
    set
    {
      if (value == this.UseDataGridViewColumnHeaderContextMenu)
        return;
      this.GridView.UseColumnHeaderContextMenu = value;
    }
  }

  [Description("Indicates whether to use the grid view row header context menu")]
  [DefaultValue(false)]
  [Category("Behavior")]
  public bool UseDataGridViewRowHeaderContextMenu
  {
    get => this._useDataGridViewRowHeaderContextMenu;
    set
    {
      if (value == this.UseDataGridViewRowHeaderContextMenu)
        return;
      this._useDataGridViewRowHeaderContextMenu = value;
    }
  }

  [DefaultValue(false)]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool UseNumericScaleValues
  {
    get => this.GanttChart.UseNumericScaleValues;
    set
    {
      if (value == this.UseNumericScaleValues)
        return;
      this.GanttChart.UseNumericScaleValues = value;
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  private int VisibleRowCount
  {
    get
    {
      return this.GridView.Rows.Cast<DataGridViewRow>().Count<DataGridViewRow>((Func<DataGridViewRow, bool>) (row => row != null && row.Visible));
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public BaseMainWindow EditorForm { get; set; }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public double DataGridPercentWidth
  {
    get
    {
      return (double) this.SplitContainer.SplitterDistance * 100.0 / (double) this.SplitContainer.Width;
    }
    set
    {
      try
      {
        this.SplitContainer.SplitterDistance = (int) Math.Round((double) this.SplitContainer.Width * value / 100.0);
      }
      catch
      {
      }
    }
  }

  protected override void OnMouseWheel(MouseEventArgs e)
  {
    base.OnMouseWheel(e);
    this.GanttChart.HandleParentMouseWheel((Control) this, e);
  }

  private void ganttChart_TaskDoubleClicked([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.Edit();
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public VScrollBar VScrollBar
  {
    [DebuggerHidden] get => this.VerticalScrollBar;
  }

  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public UserSummaryTask SelectedUserTask
  {
    get => this._selectedUserTask;
    set
    {
      if (this.SelectedUserTask == value)
        return;
      foreach (DataGridViewRow row in (IEnumerable) this.GridView.Rows)
      {
        if (row.DataBoundItem is UserSummaryTask dataBoundItem && dataBoundItem.Equals((object) value))
        {
          foreach (DataGridViewBand selectedRow in (BaseCollection) this.GridView.SelectedRows)
            selectedRow.Selected = false;
          try
          {
            this.GridView.CurrentCell = this.GridView.CurrentCell != null ? row.Cells[this.GridView.CurrentCell.ColumnIndex] : (DataGridViewCell) null;
            break;
          }
          catch
          {
            break;
          }
        }
      }
    }
  }

  public event EventHandler SelectedUserTaskChanged;

  private void GridView_RowEnter([CanBeNull] object sender, [NotNull] DataGridViewCellEventArgs e)
  {
    if (this.SelectedUserTaskChanged != null)
    {
      int rowIndex = e.RowIndex;
      Task dataBoundItem;
      do
      {
        dataBoundItem = this.GridView.Rows[rowIndex].DataBoundItem as Task;
        if (!(dataBoundItem is UserSummaryTask))
          --rowIndex;
        else
          break;
      }
      while (dataBoundItem != null && rowIndex >= 0);
      if (dataBoundItem is UserSummaryTask userSummaryTask && this._selectedUserTask != userSummaryTask)
      {
        this._selectedUserTask = userSummaryTask;
        this.SelectedUserTaskChanged((object) this, (EventArgs) null);
      }
    }
    this.GanttChart.Invalidate();
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    DataGridViewCellStyle gridViewCellStyle = new DataGridViewCellStyle();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ProjectView));
    this._toolStripContainer = new ToolStripContainer();
    this._mainSplitContainer = new SplitContainer();
    this._splitContainer = new SplitContainer();
    this._gridView = new ProjectDataGridView();
    this._ganttChart = new GanttChart();
    this._vScrollBar = new VScrollBar();
    this._labelIcon = new Label();
    this._editPanel = new Panel();
    this._vScrollBarPanel = new Panel();
    this._vScrollBarPanelBottom = new Panel();
    this._editTitlePanel = new Panel();
    this._editPanelTitleLabel = new Label();
    this._closeEditPanelButton = new Button();
    this._editPanelTitleIconLabel = new Label();
    this._printDocument = new PrintDocument();
    this._printPreviewDialog = new PrintPreviewDialog();
    this._printDialog = new PrintDialog();
    this._pageSetupDialog = new PageSetupDialog();
    this._timerAutoLevelResources = new System.Windows.Forms.Timer(this.components);
    this._toolStripContainer.ContentPanel.SuspendLayout();
    this._toolStripContainer.SuspendLayout();
    this._mainSplitContainer.BeginInit();
    this._mainSplitContainer.Panel1.SuspendLayout();
    this._mainSplitContainer.Panel2.SuspendLayout();
    this._mainSplitContainer.SuspendLayout();
    this._splitContainer.BeginInit();
    this._splitContainer.Panel1.SuspendLayout();
    this._splitContainer.Panel2.SuspendLayout();
    this._splitContainer.SuspendLayout();
    ((ISupportInitialize) this._gridView).BeginInit();
    this._editPanel.SuspendLayout();
    this._vScrollBarPanel.SuspendLayout();
    this._editTitlePanel.SuspendLayout();
    this.SuspendLayout();
    this._toolStripContainer.ContentPanel.Controls.Add((Control) this._mainSplitContainer);
    this._toolStripContainer.ContentPanel.Size = new Size(699, 269);
    this._toolStripContainer.Dock = DockStyle.Fill;
    this._toolStripContainer.Location = new Point(0, 0);
    this._toolStripContainer.Name = "_toolStripContainer";
    this._toolStripContainer.Size = new Size(699, 294);
    this._toolStripContainer.TabIndex = 0;
    this._mainSplitContainer.Dock = DockStyle.Fill;
    this._mainSplitContainer.Location = new Point(0, 0);
    this._mainSplitContainer.Name = "_mainSplitContainer";
    this._mainSplitContainer.Panel1.Controls.Add((Control) this._splitContainer);
    this._mainSplitContainer.Panel1.Controls.Add((Control) this._vScrollBarPanel);
    this._mainSplitContainer.Panel1.Controls.Add((Control) this._labelIcon);
    this._mainSplitContainer.Panel1MinSize = 0;
    this._mainSplitContainer.Panel2.Controls.Add((Control) this._editPanel);
    this._mainSplitContainer.Panel2Collapsed = true;
    this._mainSplitContainer.Panel2MinSize = 0;
    this._mainSplitContainer.Size = new Size(699, 269);
    this._mainSplitContainer.SplitterDistance = 480;
    this._mainSplitContainer.TabIndex = 0;
    this._splitContainer.Dock = DockStyle.Fill;
    this._splitContainer.Location = new Point(0, 0);
    this._splitContainer.Name = "_splitContainer";
    this._splitContainer.Panel1.Controls.Add((Control) this._gridView);
    this._splitContainer.Panel1MinSize = 0;
    this._splitContainer.Panel2.Controls.Add((Control) this._ganttChart);
    this._splitContainer.Panel2MinSize = 0;
    this._splitContainer.Size = new Size(681, 269);
    this._splitContainer.SplitterDistance = 429;
    this._splitContainer.TabIndex = 0;
    this._gridView.AllowDrop = true;
    this._gridView.AllowUserToOrderColumns = true;
    this._gridView.AllowUserToResizeRows = false;
    this._gridView.AutoGenerateColumns = false;
    this._gridView.BackgroundColor = SystemColors.Window;
    this._gridView.BorderStyle = BorderStyle.None;
    this._gridView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
    this._gridView.ColumnHeadersHeight = 40;
    this._gridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
    gridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle.BackColor = SystemColors.Window;
    gridViewCellStyle.Font = new Font("Microsoft Sans Serif", 7.2f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle.ForeColor = SystemColors.ControlText;
    gridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle.WrapMode = DataGridViewTriState.False;
    this._gridView.DefaultCellStyle = gridViewCellStyle;
    this._gridView.Dock = DockStyle.Fill;
    this._gridView.EnableHeadersVisualStyles = false;
    this._gridView.GridColor = Color.Silver;
    this._gridView.Location = new Point(0, 0);
    this._gridView.Name = "_gridView";
    this._gridView.RowHeadersWidth = 45;
    this._gridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
    this._gridView.RowTemplate.Height = 21;
    this._gridView.ShowEditingIcon = false;
    this._gridView.Size = new Size(429, 269);
    this._gridView.TabIndex = 0;
    this._gridView.TaskExpanded += new EventHandler<TaskExpandedEventArgs>(this.projectDataGridView_TaskExpanded);
    this._gridView.CellBeginEdit += new DataGridViewCellCancelEventHandler(this.projectDataGridView_CellBeginEdit);
    this._gridView.CellDoubleClick += new DataGridViewCellEventHandler(this.GridView_CellDoubleClick);
    this._gridView.CellEndEdit += new DataGridViewCellEventHandler(this.projectDataGridView_CellEndEdit);
    this._gridView.CurrentCellChanged += new EventHandler(this.projectDataGridView_CurrentCellChanged);
    this._gridView.DataError += new DataGridViewDataErrorEventHandler(this.projectDataGridView_DataError);
    this._gridView.RowEnter += new DataGridViewCellEventHandler(this.GridView_RowEnter);
    this._gridView.RowsAdded += new DataGridViewRowsAddedEventHandler(this.projectDataGridView_RowsAdded);
    this._gridView.RowsRemoved += new DataGridViewRowsRemovedEventHandler(this.projectDataGridView_RowsRemoved);
    this._gridView.Scroll += new ScrollEventHandler(this.projectDataGridView_Scroll);
    this._gridView.SelectionChanged += new EventHandler(this.projectDataGridView_SelectionChanged);
    this._gridView.KeyDown += new KeyEventHandler(this.projectDataGridView_KeyDown);
    this._gridView.MouseUp += new MouseEventHandler(this.projectDataGridView_MouseUp);
    this._gridView.Resize += new EventHandler(this.projectDataGridView_Resize);
    this._ganttChart.BackColor = SystemColors.Window;
    this._ganttChart.BarWidth = -1f;
    this._ganttChart.DayWidth = 20f;
    this._ganttChart.Dock = DockStyle.Fill;
    this._ganttChart.Location = new Point(0, 0);
    this._ganttChart.Name = "_ganttChart";
    this._ganttChart.Size = new Size(248, 269);
    this._ganttChart.TabIndex = 0;
    this._ganttChart.DragStarted += new EventHandler(this.projectGanttChartView_DragDropStarted);
    this._ganttChart.GanttChartPaint += new PaintEventHandler(this.projectGanttChartView_GanttChartPaint);
    this._ganttChart.TaskDoubleClicked += new EventHandler(this.ganttChart_TaskDoubleClicked);
    this._ganttChart.DragDrop += new DragEventHandler(this.projectGanttChartView_DragDropCompleted);
    this._vScrollBarPanel.Controls.Add((Control) this._vScrollBar);
    this._vScrollBarPanel.Controls.Add((Control) this._vScrollBarPanelBottom);
    this._vScrollBarPanel.Dock = DockStyle.Right;
    this._vScrollBarPanel.Name = "_vScrollBarPanel";
    this._vScrollBarPanel.Size = new Size(17, 270);
    this._vScrollBarPanel.TabIndex = 2;
    this._vScrollBarPanelBottom.Dock = DockStyle.Bottom;
    this._vScrollBarPanelBottom.Name = "_vScrollBarPanelBottom";
    this._vScrollBarPanelBottom.Size = new Size(17, 17);
    this._vScrollBarPanelBottom.TabIndex = 1;
    this._vScrollBarPanelBottom.TabStop = false;
    this._vScrollBar.Dock = DockStyle.Fill;
    this._vScrollBar.Name = "_vScrollBar";
    this._vScrollBar.TabIndex = 0;
    this._vScrollBar.TabStop = true;
    this._vScrollBar.ValueChanged += new EventHandler(this.vScrollBar_ValueChanged);
    this._labelIcon.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this._labelIcon.Image = (Image) componentResourceManager.GetObject("_labelIcon.Image");
    this._labelIcon.Location = new Point(682, 0);
    this._labelIcon.Name = "_labelIcon";
    this._labelIcon.Size = new Size(17, 40);
    this._labelIcon.TabIndex = 1;
    this._editPanel.Controls.Add((Control) this._editTitlePanel);
    this._editPanel.Dock = DockStyle.Fill;
    this._editPanel.Location = new Point(0, 0);
    this._editPanel.Name = "_editPanel";
    this._editPanel.Size = new Size(96 /*0x60*/, 100);
    this._editPanel.TabIndex = 0;
    this._editTitlePanel.BackColor = SystemColors.ActiveCaption;
    this._editTitlePanel.Controls.Add((Control) this._editPanelTitleLabel);
    this._editTitlePanel.Controls.Add((Control) this._closeEditPanelButton);
    this._editTitlePanel.Controls.Add((Control) this._editPanelTitleIconLabel);
    this._editTitlePanel.Dock = DockStyle.Fill;
    this._editTitlePanel.ForeColor = SystemColors.ActiveCaptionText;
    this._editTitlePanel.Location = new Point(0, 0);
    this._editTitlePanel.Name = "_editTitlePanel";
    this._editTitlePanel.Padding = new Padding(2);
    this._editTitlePanel.Size = new Size(96 /*0x60*/, 100);
    this._editTitlePanel.TabIndex = 1;
    this._editPanelTitleLabel.Dock = DockStyle.Fill;
    this._editPanelTitleLabel.Location = new Point(19, 2);
    this._editPanelTitleLabel.Name = "_editPanelTitleLabel";
    this._editPanelTitleLabel.Size = new Size(0, 96 /*0x60*/);
    this._editPanelTitleLabel.TabIndex = 1;
    this._editPanelTitleLabel.TextAlign = ContentAlignment.MiddleLeft;
    this._closeEditPanelButton.Dock = DockStyle.Right;
    this._closeEditPanelButton.FlatAppearance.BorderSize = 0;
    this._closeEditPanelButton.FlatStyle = FlatStyle.Flat;
    this._closeEditPanelButton.Location = new Point(19, 2);
    this._closeEditPanelButton.Name = "_closeEditPanelButton";
    this._closeEditPanelButton.Size = new Size(75, 96 /*0x60*/);
    this._closeEditPanelButton.TabIndex = 2;
    this._closeEditPanelButton.UseVisualStyleBackColor = true;
    this._closeEditPanelButton.Click += new EventHandler(this.closeEditPanelButton_Click);
    this._editPanelTitleIconLabel.Dock = DockStyle.Left;
    this._editPanelTitleIconLabel.Location = new Point(2, 2);
    this._editPanelTitleIconLabel.Name = "_editPanelTitleIconLabel";
    this._editPanelTitleIconLabel.Size = new Size(17, 96 /*0x60*/);
    this._editPanelTitleIconLabel.TabIndex = 0;
    this._printDocument.OriginAtMargins = true;
    this._printDocument.BeginPrint += new PrintEventHandler(this.printDocument_BeginPrint);
    this._printDocument.EndPrint += new PrintEventHandler(this.printDocument_EndPrint);
    this._printDocument.PrintPage += new PrintPageEventHandler(this.printDocument_PrintPage);
    this._printPreviewDialog.AutoScrollMargin = new Size(0, 0);
    this._printPreviewDialog.AutoScrollMinSize = new Size(0, 0);
    this._printPreviewDialog.ClientSize = new Size(632, 446);
    this._printPreviewDialog.Document = this._printDocument;
    this._printPreviewDialog.Enabled = true;
    this._printPreviewDialog.Icon = (Icon) componentResourceManager.GetObject("_printPreviewDialog.Icon");
    this._printPreviewDialog.Name = "_printPreviewDialog";
    this._printPreviewDialog.ShowIcon = false;
    this._printPreviewDialog.Visible = false;
    this._printDialog.Document = this._printDocument;
    this._printDialog.UseEXDialog = true;
    this._pageSetupDialog.Document = this._printDocument;
    this._pageSetupDialog.EnableMetric = true;
    this._timerAutoLevelResources.Tick += new EventHandler(this.timerAutoLevelResources_Tick);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._toolStripContainer);
    this.Name = nameof (ProjectView);
    this.Size = new Size(699, 294);
    this._toolStripContainer.ContentPanel.ResumeLayout(false);
    this._toolStripContainer.ResumeLayout(false);
    this._toolStripContainer.PerformLayout();
    this._mainSplitContainer.Panel1.ResumeLayout(false);
    this._mainSplitContainer.Panel2.ResumeLayout(false);
    this._mainSplitContainer.EndInit();
    this._mainSplitContainer.ResumeLayout(false);
    this._splitContainer.Panel1.ResumeLayout(false);
    this._splitContainer.Panel2.ResumeLayout(false);
    this._splitContainer.EndInit();
    this._splitContainer.ResumeLayout(false);
    ((ISupportInitialize) this._gridView).EndInit();
    this._vScrollBarPanel.ResumeLayout(false);
    this._editPanel.ResumeLayout(false);
    this._editTitlePanel.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  [CanBeNull]
  private delegate object BeforeSubProjectLoadingDelegate([NotNull] Intermech.Project.Project subProject);

  private delegate void AfterSubProjectLoadingDelegate(
    [NotNull] Intermech.Project.Project subProject,
    bool loadSubTasksResult,
    [CanBeNull] object savedState);
}
