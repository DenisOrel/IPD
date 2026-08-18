// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Client.ProjectEditorForm
// Assembly: Intermech.Project.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D968BDD9-29F0-4E24-8F57-6E851EE47258
// Assembly location: D:\IPS\Client\Intermech.Project.Client.dll

using Intermech.Bars;
using Intermech.Diagnostics;
using Intermech.Docking;
using Intermech.Document.Client;
using Intermech.Document.Model;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Document;
using Intermech.Metadata;
using Intermech.Navigator.Interfaces;
using Intermech.Project.Controls;
using Intermech.Project.Print;
using Intermech.Workflow;
using Intermech.Workflow.Design;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Client;

public class ProjectEditorForm : BaseMainWindow, ICommandTarget, IContextAware
{
  [CanBeNull]
  private ClientProject _project;
  internal static Guid DockGuid = new Guid("{CB41F29B-23BA-45fa-94FC-611C8CB3174E}");
  [NotNull]
  private readonly WorkshopRouteProcessingCommand _workshopRouteProcessingCommand;
  private DataGridView.HitTestInfo _projectViewContextMenuHitTest;
  [NotNull]
  private static readonly ConcurrentDictionary<long, ProjectEditorForm> _openedProjectForms = new ConcurrentDictionary<long, ProjectEditorForm>();
  [CanBeNull]
  private ContextMenuBarItem _contextMenu;
  private TaskFilter _prevSelectedTaskFilter;
  private System.Threading.Timer _paintFilterTimer;
  private bool _saved;
  private ProjectEditorMode _mode;
  private ContextMenuBarItem _ganttHeaderContextMenu;
  private bool _forceClose;
  private IContainer components;
  private Intermech.Bars.ToolBar _toolBar;
  private ProjectView _projectView;
  private ResourcesSummaryView _resourcesSummaryView;
  private Splitter _horizontalSplitter;
  private ComboBoxItem _filtersComboBoxItem;

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected Intermech.Bars.ToolBar ToolBar
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._toolBar.CheckInitializedIn<Intermech.Bars.ToolBar>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ProjectView ProjectView
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._projectView.CheckInitializedIn<ProjectView>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public GanttChart GanttChart
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._projectView.CheckInitializedIn<ProjectView>((object) this).GanttChart;
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ResourcesSummaryView ResourcesSummaryView
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._resourcesSummaryView.CheckInitializedIn<ResourcesSummaryView>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected Splitter HorizontalSplitter
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._horizontalSplitter.CheckInitializedIn<Splitter>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected ComboBoxItem FiltersComboBoxItem
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._filtersComboBoxItem.CheckInitializedIn<ComboBoxItem>((object) this);
    }
  }

  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  internal IToolBarRenderer Renderer
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.ToolBar.Renderer;
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this.ToolBar.Renderer = value;
    }
  }

  public ProjectEditorForm()
  {
    this.InitializeComponent();
    this.Closing += new CancelEventHandler(this.EditorFormClosing);
    this.Closed += new EventHandler(this.EditorFormClosed);
    this.AddService<ProjectEditorForm>(this);
    this.Tag = (object) this;
    this.AddService<ProjectView>(this.ProjectView);
    this.AddService<GanttChart>(this.ProjectView.GanttChart);
    this.ProjectView.ContextMenuRequested += new MouseEventHandler(this.ProjectView_ContextMenuRequested);
    this.ProjectView.DataGridView.SelectionChanged += new EventHandler(this.DataGridView_SelectionChanged);
    this.ProjectView.EditorForm = (BaseMainWindow) this;
    this._workshopRouteProcessingCommand = new WorkshopRouteProcessingCommand(this);
    Intermech.Client.Services.NotificationService.Subscribe("ObjectsRemoved", new NotificationEventHandler(this.NotificationObjectsRemoved));
    Intermech.Client.Services.NotificationService.Subscribe("ObjectsChanged", new NotificationEventHandler(this.NotificationObjectsChanged));
    this.GanttChart.HeaderClick += new MouseEventHandler(this.GanttChart_HeaderClick);
    this.Guid = ProjectEditorForm.DockGuid;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      Intermech.Client.Services.NotificationService.Unsubscribe("ObjectsChanged", new NotificationEventHandler(this.NotificationObjectsChanged));
      Intermech.Client.Services.NotificationService.Unsubscribe("ObjectsRemoved", new NotificationEventHandler(this.NotificationObjectsRemoved));
      this.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      this.RemoveService<ClientProject>();
      this.RemoveService<Intermech.Project.Project>();
      this.RemoveService<ISessionProvider>();
      this.RemoveService<GanttChart>();
      this.RemoveService<ProjectView>();
      this.RemoveService<ProjectEditorForm>();
    }
    if (this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  [CanBeNull]
  public DockControl DockControl => this.Parent as DockControl;

  private void DataGridView_SelectionChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (!this.IsHandleCreated)
      return;
    this.BeginInvoke((Delegate) new ThreadStart(this.UpdateCommands));
  }

  private void ProjectView_ContextMenuRequested([CanBeNull] object sender, [NotNull] MouseEventArgs e)
  {
    this._projectViewContextMenuHitTest = this.ProjectView.DataGridView.HitTest(e.X, e.Y);
    try
    {
      this.ContextMenu.Show(Intermech.Client.Services.PopupHost, sender as Control, new Point(e.X, e.Y));
    }
    finally
    {
      this._projectViewContextMenuHitTest = (DataGridView.HitTestInfo) null;
    }
  }

  private void _project_Saving([CanBeNull] object sender, [NotNull] CancelEventArgs e)
  {
    if (this._project == null)
      return;
    if (this._project.ObjectID == 0L && !SaveProjectForm.Show((Intermech.Project.Project) this._project))
    {
      e.Cancel = true;
    }
    else
    {
      ProjectDisplayOptions displayOptions = this._project.DisplayOptions;
      ProjectView view = this._project.DisplayOptions.View;
      DateTime dateTime = view != null ? view.CurrentDate : DateTime.Now;
      displayOptions.CurrentDate = dateTime;
      this.Refresh();
    }
  }

  private void _project_Saved([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this._project == null)
      return;
    Editors.RegisterEditor((Control) this, this._project.ObjectID, this._project.EditingMode.Any());
  }

  [NotNull]
  protected string ConfigSection
  {
    get
    {
      return this.Mode != ProjectEditorMode.Project ? "ResourcesUsageForm.Layout" : "ProjectEditorForm.Layout";
    }
  }

  [NotNull]
  protected string DataGridConfigName
  {
    get => this.Mode != ProjectEditorMode.Project ? "RProjectDataGridView" : "ProjectDataGridView";
  }

  private double SummaryViewPercentHeight
  {
    get => (double) this.ResourcesSummaryView.Height * 100.0 / (double) this.Height;
    set
    {
      try
      {
        this.ResourcesSummaryView.Height = (int) Math.Round((double) this.Height * value / 100.0);
        this.ResourcesSummaryView.Refresh();
      }
      catch
      {
      }
    }
  }

  internal void LoadState()
  {
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBConfigurations configurations = sessionKeeper.Session.Configurations;
        if (this.Mode == ProjectEditorMode.Resources)
          this.ProjectView.DataGridView.SetVisibleColumns(new List<string>((IEnumerable<string>) new string[4]
          {
            "Name",
            "WorkString",
            "StartString",
            "FinishString"
          }));
        if (!this.ProjectView.DataGridView.LoadLayout(this.DataGridConfigName + ".Layout") && this.Mode == ProjectEditorMode.Resources)
          this.ProjectView.SplitContainer.SplitterDistance = this.ProjectView.DataGridView.RowHeadersWidth + this.ProjectView.DataGridView.GetColumnsWidth(new List<string>((IEnumerable<string>) new string[2]
          {
            "Name",
            "WorkString"
          })) + 1;
        else
          this.ProjectView.DataGridPercentWidth = configurations.ReadDouble("Редактор проектов", this.ConfigSection, "DataGridWidth", 40.0, DBConfigMode.UserOnly);
        this.ResourcesSummaryView.SplitContainer.SplitterDistance = this.ProjectView.SplitContainer.SplitterDistance;
        if (this.Mode != ProjectEditorMode.Resources)
          return;
        this._project.DisplayOptions.ScaleType = configurations.ReadEnum<ScaleType>("Редактор проектов", this.ConfigSection, "ScaleType", ScaleType.Weeks, DBConfigMode.UserOnly);
        this.SummaryViewPercentHeight = configurations.ReadDouble("Редактор проектов", this.ConfigSection, "SummaryViewHeight", this.SummaryViewPercentHeight, DBConfigMode.UserOnly);
        this.ResourcesSummaryView.ChartView.CalcMode = configurations.ReadEnum<ResourcesCalcMode>("Редактор проектов", this.ConfigSection, "CalcMode", this.ResourcesSummaryView.ChartView.CalcMode, DBConfigMode.UserOnly);
      }
    }
    catch
    {
    }
  }

  internal void SaveState()
  {
    this.InitPersistString();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBConfigurations configurations = sessionKeeper.Session.Configurations;
      this.ProjectView.DataGridView.SaveLayout(this.DataGridConfigName + ".Layout");
      configurations.WriteDouble("Редактор проектов", this.ConfigSection, "DataGridWidth", this.ProjectView.DataGridPercentWidth);
      if (this.Mode != ProjectEditorMode.Resources)
        return;
      configurations.WriteInteger("Редактор проектов", this.ConfigSection, "ScaleType", (long) this.ProjectView.GanttChart.ScaleType);
      configurations.WriteDouble("Редактор проектов", this.ConfigSection, "SummaryViewHeight", this.SummaryViewPercentHeight);
      configurations.WriteInteger("Редактор проектов", this.ConfigSection, "CalcMode", (long) this.ResourcesSummaryView.ChartView.CalcMode);
    }
  }

  private void RefreshFilters()
  {
    int val1 = this.FiltersComboBoxItem.ComboBox.SelectedIndex;
    this.FiltersComboBoxItem.ComboBox.BeginUpdate();
    try
    {
      this.FiltersComboBoxItem.Items.Clear();
      this.FiltersComboBoxItem.Items.AddRange(TaskFilters.All.Select(false).Select<TaskFilter, object>((Func<TaskFilter, object>) (taskFilter => (object) taskFilter)).ToArray<object>());
      if (this.Project != null)
        this.FiltersComboBoxItem.Items.AddRange(this.Project.DisplayOptions.Filters.Select(false).Select<TaskFilter, object>((Func<TaskFilter, object>) (taskFilter => (object) taskFilter)).ToArray<object>());
      ArrayList.Adapter((IList) this.FiltersComboBoxItem.Items).Sort();
    }
    finally
    {
      this.FiltersComboBoxItem.ComboBox.EndUpdate();
    }
    if (val1 == -1)
      val1 = 0;
    this.FiltersComboBoxItem.ComboBox.SelectedIndex = Math.Min(val1, this.FiltersComboBoxItem.ComboBox.Items.Count - 1);
    this.RefreshPaintFilters();
  }

  private void RefreshPaintFilters()
  {
    List<TaskFilter> taskFilterList = TaskFilters.All.Select(true);
    if (this.Project != null)
      taskFilterList.AddRange((IEnumerable<TaskFilter>) this.Project.DisplayOptions.Filters.Select(true));
    Dictionary<Task, Brush> brushes = new Dictionary<Task, Brush>();
    Dictionary<Task, Pen> dictionary = new Dictionary<Task, Pen>();
    if (this.Project != null)
    {
      foreach (TaskFilter filter in taskFilterList)
      {
        if (!string.IsNullOrEmpty(filter.BrushStr))
        {
          Brush brush = (Brush) null;
          Pen pen = (Pen) null;
          foreach (Task task in this.Project.Tasks.Where<Task>((Func<Task, bool>) (t => !brushes.ContainsKey(t))))
          {
            bool flag = false;
            try
            {
              flag = Intermech.Project.Evaluator.Evaluator.Eval(task, filter);
            }
            catch
            {
            }
            if (flag)
            {
              brushes.Add(task, brush ?? (brush = GraphicFuncs.StringToBrush(filter.BrushStr)));
              if (!string.IsNullOrEmpty(filter.PenStr))
                dictionary.Add(task, pen ?? (pen = GraphicFuncs.StringToPen(filter.PenStr)));
            }
          }
        }
      }
    }
    this.ProjectView.GanttChart.TaskBrushes = brushes;
    this.ProjectView.GanttChart.TaskPens = dictionary;
  }

  private void ProjectEditorForm_Load([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    ProjectClientPlugin.CommandList.AddToToolbar(this.ToolBar, (IReadOnlyCollection<string>) new string[8]
    {
      "DecreaseIndent",
      "IncreaseIndent",
      "Cut",
      "Copy",
      "Paste",
      "Delete",
      "Properties",
      "Sync"
    });
    this.FiltersComboBoxItem.Index = this.ToolBar.Items.Count - 1;
    this.FiltersComboBoxItem.ComboBox.SelectedValueChanged += new EventHandler(this.FiltersComboBox_SelectedValueChanged);
    this.FiltersComboBoxItem.ComboBox.DropDownClosed += new EventHandler(this.ComboBox_DropDownClosed);
  }

  [CanBeNull]
  public static ProjectEditorForm GetProjectForm([NotEmpty] long projectID)
  {
    ProjectEditorForm projectEditorForm;
    return !ProjectEditorForm._openedProjectForms.TryGetValue(Math.Abs(projectID), out projectEditorForm) ? (ProjectEditorForm) null : projectEditorForm;
  }

  [NotNull]
  private ContextMenuBarItem ContextMenu
  {
    get
    {
      if (this._contextMenu == null)
      {
        this._contextMenu = new ContextMenuBarItem();
        CommandList commandList = ProjectClientPlugin.CommandList;
        ContextMenuBarItem contextMenu = this._contextMenu;
        string[] items;
        if (this.Mode != ProjectEditorMode.Project)
          items = new string[3]
          {
            "ViewProject",
            "EditProject",
            "Properties"
          };
        else
          items = new string[15]
          {
            "InsertNew",
            "InsertProject",
            "DecreaseIndent",
            "IncreaseIndent",
            "CheckOut",
            "CheckIn",
            "Cut",
            "Copy",
            "Paste",
            "ConvertToProject",
            "ConvertToTask",
            "Delete",
            "ViewProject",
            "EditProject",
            "Properties"
          };
        commandList.AddToMenu((MenuItemBase) contextMenu, (IReadOnlyCollection<string>) items);
        this.UpdateCommands();
      }
      ProjectEditorForm.HideDisabledMenuItems(this._contextMenu);
      return this._contextMenu;
    }
  }

  private static void HideDisabledMenuItems([NotNull] ContextMenuBarItem contextMenu)
  {
    foreach (MenuButtonItem menuButtonItem in (CollectionBase) contextMenu.Items)
    {
      menuButtonItem.Visible = menuButtonItem.Enabled;
      if (menuButtonItem.Visible && menuButtonItem.HasChildren && menuButtonItem.Items != null)
        ProjectEditorForm.HideDisabledMenuItems(contextMenu);
    }
  }

  internal void UpdateCommands()
  {
    foreach (KeyValuePair<string, CommandList.CommandInfo> command1 in (Dictionary<string, CommandList.CommandInfo>) ProjectClientPlugin.CommandList)
    {
      ICommandState command2 = this.CommandManager.FindCommand(command1.Value.CommandName);
      if (command2 != null)
        this.QueryStatus(command2);
    }
  }

  internal void EditorFormClosing([CanBeNull] object sender, [NotNull] CancelEventArgs e)
  {
    if (this._forceClose)
      return;
    DialogResult dialogResult = DialogResult.No;
    if (this.Project == null)
      return;
    if (this.Project.EditingMode.Any())
    {
      if (!this.ProjectView.Validate())
      {
        e.Cancel = true;
        return;
      }
      e.Cancel = false;
      if (this.Project.Modified)
        dialogResult = MessageFuncs.Ask(Intermech.Project.Localization.GetString("SavePrompt", (object) this.Project.Name), MessageBoxButtons.YesNoCancel);
      switch (dialogResult)
      {
        case DialogResult.Cancel:
          e.Cancel = true;
          break;
        case DialogResult.Yes:
          e.Cancel = !this.Save();
          break;
      }
      if (!e.Cancel)
      {
        Editors.UnregisterEditor((Control) this);
        this.Project.EndEdit(dialogResult == DialogResult.No);
      }
    }
    if (e.Cancel)
      return;
    this.SaveState();
  }

  internal void EditorFormClosed([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this.Project != null && this.Project.ID != 0L)
      ProjectEditorForm._openedProjectForms.TryRemove(Math.Abs(this.Project.ID), out ProjectEditorForm _);
    Intermech.Client.Services.NotificationService.Unsubscribe("ObjectsChanged", new NotificationEventHandler(this.NotificationObjectsChanged));
    Intermech.Client.Services.NotificationService.Unsubscribe("ObjectsRemoved", new NotificationEventHandler(this.NotificationObjectsRemoved));
    if (!(this.Parent is DockControl parent))
      return;
    parent.Closing -= new CancelEventHandler(this.EditorFormClosing);
    parent.Closed -= new EventHandler(this.EditorFormClosed);
  }

  private void PrintHandler(bool previewOnly)
  {
    if (this.Mode == ProjectEditorMode.Project)
    {
      this.Validate();
      if (this.Project.Modified)
      {
        if (MessageFuncs.Confirm(Intermech.Project.Localization.GetString("ReportRequiresSaving")) != DialogResult.OK)
          return;
        this.Save();
      }
      wfFunx.PerformCommand(this.ProjectView.Project.ObjectID, "RunScenarioDoc");
    }
    else
      this.GenerateReport(previewOnly);
  }

  [NotNull]
  public static DocumentTreeNode CreateNode(
    [NotNull] ImDocumentData document,
    [NotNull] DocumentTreeNode workArea,
    [NotNull, NotWhitespace] string nodeID)
  {
    DocumentTreeNode child = document.Template.FindNode(nodeID).CloneFromTemplate(true, true);
    workArea.AddChildNode(child, false, false);
    return child;
  }

  public static void WriteNodeRow([NotNull] DocumentTreeNode node, [NotNull] params string[] values)
  {
    if (values.Length == 0)
      return;
    TextData textData = (TextData) null;
    for (int index = 0; index < values.Length; ++index)
    {
      if (index % 2 == 0)
        textData = node.FindFirstNodeFromTemplate_Recursive(values[index]) as TextData;
      else
        textData?.AssignText(values[index], false, false, false);
    }
  }

  private static void Write([NotNull] DocumentTreeNode parent, [NotNull] string tplId, [NotNull] string text)
  {
    if (!(parent.FindFirstNodeFromTemplate_Recursive(tplId) is TextData templateRecursive))
      return;
    templateRecursive.AssignText(text, false, false, false);
  }

  private static void WriteMacros(
    [NotNull] DocumentTreeNode parent,
    [NotNull] string tplId,
    [NotNull] Dictionary<string, string> macros)
  {
    if (!(parent.FindFirstNodeFromTemplate_Recursive(tplId) is TextData templateRecursive))
      return;
    string str = templateRecursive.Text ?? string.Empty;
    foreach (KeyValuePair<string, string> macro in macros)
      str = str.Replace($"%{macro.Key}%", macro.Value);
    templateRecursive.AssignText(str, false, false, false);
  }

  private bool _generateReport2([NotNull] ImDocument tplDoc, [NotNull] ImDocument doc)
  {
    if (!(this.Project is ResourceAssignmentsProject project))
      return false;
    DateTime start;
    DateTime finish;
    using (ReportPeriodForm reportPeriodForm = new ReportPeriodForm())
    {
      if (reportPeriodForm.ShowDialog() != DialogResult.OK)
        return false;
      start = reportPeriodForm.Start;
      finish = reportPeriodForm.Finish;
    }
    doc.Name = project.UserNames;
    ProjectEditorForm.WriteMacros((DocumentTreeNode) doc, "title", new Dictionary<string, string>()
    {
      {
        "name",
        project.UserNames
      }
    });
    ProjectEditorForm.WriteMacros((DocumentTreeNode) doc, "date", new Dictionary<string, string>()
    {
      {
        "start",
        start.ToString("d")
      },
      {
        "finish",
        finish.ToString("d")
      }
    });
    DocumentTreeNode node1 = doc.FindNode("table");
    int num1 = 0;
    List<long> longList = new List<long>();
    double num2 = 0.0;
    double num3 = 0.0;
    double num4 = 0.0;
    if (node1 != null)
    {
      DocumentTreeNode template = doc.Template;
      DocumentTreeNode node2 = template.FindNode("row");
      DocumentTreeNode node3 = template.FindNode("prow");
      DocumentTreeNode node4 = template.FindNode("trow");
      DocumentTreeNode documentTreeNode1 = (DocumentTreeNode) null;
      DocumentTreeNode documentTreeNode2 = (DocumentTreeNode) null;
      DocumentTreeNode documentTreeNode3 = (DocumentTreeNode) null;
      DocumentTreeNode documentTreeNode4 = (DocumentTreeNode) null;
      if (node2 != null && node3 != null && node4 != null)
      {
        foreach (Task task in (System.Collections.ObjectModel.Collection<Task>) this.Project.Tasks)
        {
          if (task is UserSummaryTask currentUserTask)
          {
            ++num1;
            documentTreeNode1 = node2.CloneFromTemplate();
            node1.AddChildNode(documentTreeNode1, false, false);
            documentTreeNode3 = documentTreeNode1.FindFirstNodeFromTemplate_Recursive("pcol");
            ProjectEditorForm.Write(documentTreeNode1, "user", currentUserTask.Name);
            UsageData usageData = this.ResourcesSummaryView.ChartView.GetUsageData(currentUserTask, start, finish);
            if (usageData != null)
            {
              num2 += usageData.Load;
              num3 += usageData._PossibleWork;
              num4 += usageData._Work;
              double num5 = usageData._PossibleWork - usageData._Work;
              if (num5 < 0.0)
                num5 = 0.0;
              ProjectEditorForm.Write(documentTreeNode1, "freeres", num5.ToString("0.#"));
              ProjectEditorForm.Write(documentTreeNode1, "load", $"{usageData.Load * 100.0:0.#}%");
            }
            ProjectEditorForm.Write(documentTreeNode1, "percent", $"{currentUserTask.PercentCompleted:0.#}%");
          }
          else if (!task.HiddenByFilter && (task.Start >= start && task.Start <= finish || task.Finish >= start && task.Finish <= finish || task.Start < start && task.Finish > finish))
          {
            if (task is ResourceAssignmentsSubProject && documentTreeNode1 != null && documentTreeNode3 != null)
            {
              if (!longList.Contains(task.ObjectID))
                longList.Add(task.ObjectID);
              documentTreeNode2 = node3.CloneFromTemplate();
              documentTreeNode3.AddChildNode(documentTreeNode2, false, false);
              ProjectEditorForm.Write(documentTreeNode2, "project", task.Name);
              ProjectEditorForm.Write(documentTreeNode2, "percent", $"{task.PercentCompleted:0.#}%");
              documentTreeNode4 = documentTreeNode2.FindFirstNodeFromTemplate_Recursive("tcol");
            }
            else if (documentTreeNode1 != null && documentTreeNode2 != null && documentTreeNode4 != null)
            {
              DocumentTreeNode documentTreeNode5 = node4.CloneFromTemplate();
              documentTreeNode4.AddChildNode(documentTreeNode5, false, false);
              ProjectEditorForm.Write(documentTreeNode5, "name", task.Name);
              ProjectEditorForm.Write(documentTreeNode5, "start", task.StartString);
              ProjectEditorForm.Write(documentTreeNode5, "finish", task.FinishString);
              ProjectEditorForm.Write(documentTreeNode5, "percent", $"{task.PercentCompleted:0.#}%");
            }
          }
        }
      }
    }
    Dictionary<string, string> macros1 = new Dictionary<string, string>()
    {
      {
        "sname",
        project.UserNames.Truncate(120)
      },
      {
        "start",
        start.ToString("d")
      },
      {
        "finish",
        finish.ToString("d")
      },
      {
        "projectcount",
        longList.Count.ToString()
      },
      {
        "rescount",
        num1.ToString()
      }
    };
    ProjectEditorForm.WriteMacros((DocumentTreeNode) doc, "summary1", macros1);
    double num6 = num2 / (double) num1;
    double num7 = num3 - num4;
    if (num7 < 0.0)
      num7 = 0.0;
    Dictionary<string, string> macros2 = new Dictionary<string, string>()
    {
      {
        "load",
        num6.ToString("0.#%")
      },
      {
        "possiblework",
        num3.ToString("0.#")
      },
      {
        "work",
        num4.ToString("0.#")
      },
      {
        "remainingwork",
        num7.ToString("0.#")
      }
    };
    ProjectEditorForm.WriteMacros((DocumentTreeNode) doc, "summary2", macros2);
    return true;
  }

  private void GenerateReport(bool previewOnly)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      long reportTemplate2Id = (long) (IpsMetadataEntityBase<long>) Intermech.Project.SystemObject.AssignmentsReportTemplate2ID;
      IDBAttribute attributeById = (session.GetObjectActualCopy(reportTemplate2Id, false) ?? throw new Exception(Intermech.Project.Localization.GetString("ErrNoReportTemplate"))).GetAttributeByID(session.IdentHelper.FileAttributeID);
      if (attributeById == null)
        return;
      using (MemoryStream stream = StreamHelper.BlobReaderToStream(attributeById as IBlobReader))
      {
        ImDocument imDocument1 = ImDocument.LoadFromXml((Stream) stream, false, false);
        ImDocument imDocument2 = new ImDocument(imDocument1, true, true);
        imDocument2.Name = this.Project.Name;
        if (!this._generateReport2(imDocument1, imDocument2))
          return;
        imDocument2.UpdateLayout(0, true, false);
        if (DocumentEditorPlugin.Instance == null)
          return;
        if (previewOnly)
        {
          DocumentEditorPlugin.Instance.OpenDocument((DocumentTreeNode) imDocument2, false, true);
        }
        else
        {
          using (PrintDialog printDialog = new PrintDialog())
          {
            PrintDocument printDocument = imDocument2.PrintDocument;
            printDialog.Document = printDocument;
            if (printDialog.ShowDialog() != DialogResult.OK)
              return;
            printDocument.Print();
          }
        }
      }
    }
  }

  private long ResourcesSubProjectID
  {
    get
    {
      Task task = this.ProjectView.SelectedTask;
      while (true)
      {
        switch (task)
        {
          case null:
          case ResourceAssignmentsSubProject _:
            goto label_3;
          default:
            task = task.Parent;
            continue;
        }
      }
label_3:
      ResourceAssignmentsSubProject assignmentsSubProject = (ResourceAssignmentsSubProject) task;
      return assignmentsSubProject == null ? 0L : assignmentsSubProject.ObjectID;
    }
  }

  private void ReloadProject()
  {
    Point point = new Point();
    DataGridViewCell currentCell = this.ProjectView.GridView.CurrentCell;
    if (currentCell != null)
    {
      point.X = currentCell.ColumnIndex;
      point.Y = currentCell.RowIndex;
    }
    int num = this.ProjectView.VScrollBar.Value;
    DateTime currentDate = this.ProjectView.GanttChart.CurrentDate;
    Intermech.Project.Project.TasksStateSet set = new Intermech.Project.Project.TasksStateSet();
    this.Project.SaveMinimizedTasks(set);
    ClientProject clientProject = IMProject.LoadProject(this.Project.ObjectID, this.Project.EditingMode.Any());
    if (clientProject == null)
    {
      this.ForceClose();
    }
    else
    {
      clientProject.RestoreMinimizedTasks(set);
      clientProject.DisplayOptions = this.Project.DisplayOptions;
      this.Project = clientProject;
      try
      {
        if (this.ProjectView.GridView.Rows.Count > point.X)
        {
          if (point.Y < this.ProjectView.GridView.Rows.Count)
          {
            if (point.X < this.ProjectView.GridView.Rows[point.Y].Cells.Count)
              this.ProjectView.GridView.CurrentCell = this.ProjectView.GridView.Rows[point.Y].Cells[point.X];
          }
        }
      }
      catch
      {
      }
      this.ProjectView.VScrollBar.Value = num;
      this.ProjectView.GanttChart.CurrentDate = currentDate;
    }
  }

  public bool Execute([NotNull] ICommandState commandState)
  {
    bool flag = true;
    if (!this.CommandManager.IsCommandEnabled((ICommandTarget) this, commandState.CommandName))
      return false;
    try
    {
      ClientProject project1 = this.Project;
      Intermech.Project.Project selectedTask1 = this.ProjectView.SelectedTask as Intermech.Project.Project;
      string commandName = commandState.CommandName;
      // ISSUE: reference to a compiler-generated method
      switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(commandName))
      {
        case 135637716:
          if (commandName == "Refresh")
          {
            if (this.Project is ResourceAssignmentsProject project2)
            {
              project2.Reload();
              this.ResourcesSummaryView.SetProject(project2.SummaryProject);
              this.ResourcesSummaryView.ChartView.CurrentDate = this.ProjectView.GanttChart.CurrentDate;
              goto label_133;
            }
            if (this.Project.Modified)
              throw new NotificationException(Intermech.Project.Localization.GetString("ErrCantRefreshUnsaved"));
            this.ReloadProject();
            goto label_133;
          }
          goto default;
        case 188393196:
          if (commandName == "Filters")
          {
            if (project1 != null)
            {
              if (FiltersForm.Show(project1))
              {
                this.RefreshFilters();
                goto label_133;
              }
              goto label_133;
            }
            goto label_133;
          }
          goto default;
        case 575318136:
          if (commandName == "ValidateProject")
          {
            string s = this.Project.Validate(false);
            if (s != string.Empty)
            {
              int num = (int) MessageFuncs.SayError(s);
              goto label_133;
            }
            int num1 = (int) MessageFuncs.SayOK(Intermech.Project.Localization.GetString("NoErrors"));
            goto label_133;
          }
          goto default;
        case 1042076026:
          if (commandName == "Find")
          {
            this.ProjectView.Find();
            goto label_133;
          }
          goto default;
        case 1079084467:
          if (commandName == "CreateReport")
          {
            this.PrintHandler(false);
            goto label_133;
          }
          goto default;
        case 1186794275:
          if (commandName == "ImportObject")
          {
            using (ImportObjectsFormAdv importObjectsFormAdv = new ImportObjectsFormAdv(this.Services, "ImportObjectToProject"))
            {
              if (importObjectsFormAdv.ShowDialog() == DialogResult.OK)
              {
                importObjectsFormAdv.ImportTasks();
                goto label_133;
              }
              goto label_133;
            }
          }
          goto default;
        case 1294818664:
          if (commandName == "Save")
          {
            this.Save();
            goto label_133;
          }
          goto default;
        case 1331691168:
          if (commandName == "ConvertToTask")
          {
            Task selectedTask2 = this.ProjectView.SelectedTask;
            if (selectedTask2 != null)
            {
              if (MessageFuncs.Ask(Intermech.Project.Localization.GetString("ConvertToTaskPrompt", (object) selectedTask2.NameInMessages)) == DialogResult.Yes)
              {
                if (selectedTask2.ConvertTo<Task>())
                {
                  Intermech.Client.Services.NotificationService.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsRemoved", selectedTask2.ObjectID));
                  Intermech.Client.Services.NotificationService.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", selectedTask2.ObjectID));
                  this.ReloadProject();
                  goto label_133;
                }
                goto label_133;
              }
              goto label_133;
            }
            goto label_133;
          }
          goto default;
        case 1463683828:
          if (commandName == "Import")
          {
            this.LoadFromMsProjectXml();
            goto label_133;
          }
          goto default;
        case 1469573738:
          if (commandName == "Delete")
          {
            this.ProjectView.Delete();
            goto label_133;
          }
          goto default;
        case 1472122989:
          if (commandName == "ViewProject")
          {
            if (this.Mode == ProjectEditorMode.Resources)
            {
              long resourcesSubProjectId = this.ResourcesSubProjectID;
              if (resourcesSubProjectId != 0L)
              {
                IMProject.ViewProject(resourcesSubProjectId);
                goto label_133;
              }
              goto label_133;
            }
            if (this.Mode == ProjectEditorMode.Project)
            {
              IMProject.ViewProject(selectedTask1.ObjectID);
              goto label_133;
            }
            goto label_133;
          }
          goto default;
        case 1478911948:
          if (commandName == "CheckIn")
          {
            if (selectedTask1 != null)
            {
              this.ProjectView.Validate();
              if (selectedTask1.Modified)
              {
                if (MessageFuncs.Ask(Intermech.Project.Localization.GetString("SavePrompt", (object) selectedTask1.Name), MessageBoxButtons.OKCancel) != DialogResult.Cancel)
                {
                  if (!selectedTask1.Save())
                    goto label_133;
                }
                else
                  goto label_133;
              }
              selectedTask1.CheckIn();
              goto label_133;
            }
            goto label_133;
          }
          goto default;
        case 1491228771:
          if (commandName == "Cut")
          {
            this.ProjectView.Cut();
            goto label_133;
          }
          goto default;
        case 1703884388:
          if (commandName == "Copy")
          {
            this.ProjectView.Copy();
            goto label_133;
          }
          goto default;
        case 1876427442:
          if (commandName == "WorkshopRouteProcessing")
          {
            this._workshopRouteProcessingCommand.Execute();
            goto label_133;
          }
          goto default;
        case 1957421594:
          if (commandName == "ConvertToProject")
          {
            Task selectedTask3 = this.ProjectView.SelectedTask;
            if (selectedTask3 != null)
            {
              if (MessageFuncs.Ask(Intermech.Project.Localization.GetString("ConvertToProjectPrompt", (object) selectedTask3.NameInMessages)) == DialogResult.Yes)
              {
                if (selectedTask3.ConvertTo<Intermech.Project.Project>())
                {
                  Intermech.Client.Services.NotificationService.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", selectedTask3.ObjectID));
                  this.ReloadProject();
                  goto label_133;
                }
                goto label_133;
              }
              goto label_133;
            }
            goto label_133;
          }
          goto default;
        case 2029981677:
          if (commandName == "SyncWithImportedObjectComposition")
          {
            if (this.Project.ImportedObjects.Count == 0)
            {
              if (MessageFuncs.Ask(Intermech.Project.Localization.GetString("NoImportedObjectsInProject")) == DialogResult.Yes)
              {
                ICommandState commandState1 = this.CommandManager.GetCommandState((ICommandTarget) this, ProjectClientPlugin.CmdImportObject.CommandName);
                if (commandState1.Visible && commandState1.Enabled)
                {
                  this.CommandManager.Execute(commandState1);
                  goto label_133;
                }
                this.CommandManager.QueryStatus();
                goto label_133;
              }
              goto label_133;
            }
            using (SelectImportedObjectForm importedObjectForm = new SelectImportedObjectForm(this.Services, "SyncWithImportedObjectComposition"))
            {
              DialogResult dialogResult = importedObjectForm.ShowDialog();
              if (dialogResult == DialogResult.OK || importedObjectForm.SaveChanges)
              {
                foreach (long importedObjectId in (IEnumerable<long>) importedObjectForm.DeleteImportedObjectIDs)
                  this.Project.RemoveConnectionWithImportedObject(importedObjectId, importedObjectForm.DeleteTasksImportedObjectIDs.Contains<long>(importedObjectId));
              }
              if (dialogResult == DialogResult.OK)
              {
                if (importedObjectForm.FocusedObjectVersionID != 0L)
                {
                  using (SyncWithCompositionForm withCompositionForm = new SyncWithCompositionForm(this.Services, "SyncWithImportedObjectComposition", this.Project.GetImportedObjectDescriptor(importedObjectForm.FocusedObjectVersionID)))
                  {
                    if (withCompositionForm.ShowDialog() == DialogResult.OK)
                    {
                      withCompositionForm.SyncWithProject();
                      goto label_133;
                    }
                    goto label_133;
                  }
                }
                goto label_133;
              }
              goto label_133;
            }
          }
          goto default;
        case 2158594675:
          if (commandName == "IncreaseIndent")
          {
            this.ProjectView.IncreaseIndent();
            goto label_133;
          }
          goto default;
        case 2177370620:
          if (commandName == "Properties")
          {
            string initialParamName = (string) null;
            if (this._projectViewContextMenuHitTest != null && this._projectViewContextMenuHitTest.ColumnIndex >= 0 && this._projectViewContextMenuHitTest.ColumnIndex < this.ProjectView.DataGridView.ColumnCount)
            {
              DataGridViewColumn column = this.ProjectView.DataGridView.Columns[this._projectViewContextMenuHitTest.ColumnIndex];
              if (column != null)
                initialParamName = column.DataPropertyName;
            }
            this.ProjectView.Edit(initialParamName);
            goto label_133;
          }
          goto default;
        case 2309430202:
          if (commandName == "Sync")
          {
            this.Project.Sync();
            goto label_133;
          }
          goto default;
        case 2338211422:
          if (commandName == "SpecialCommands")
            goto label_133;
          goto default;
        case 2971234699:
          if (commandName == "ProjectProperties")
          {
            this.ProjectView.ShowProjectProperties();
            goto label_133;
          }
          goto default;
        case 3007971976:
          if (commandName == "Paste")
          {
            this.ProjectView.Paste();
            goto label_133;
          }
          goto default;
        case 3061131130:
          if (commandName == "EditProject")
          {
            if (this.Mode == ProjectEditorMode.Resources)
            {
              long resourcesSubProjectId = this.ResourcesSubProjectID;
              if (resourcesSubProjectId != 0L)
              {
                IMProject.EditProject(resourcesSubProjectId);
                goto label_133;
              }
              goto label_133;
            }
            if (this.Mode == ProjectEditorMode.Project)
            {
              IMProject.EditProject(selectedTask1.ObjectID);
              goto label_133;
            }
            goto label_133;
          }
          goto default;
        case 3201178149:
          if (commandName == "LineStyleSetup")
            goto label_133;
          goto default;
        case 3298834406:
          if (commandName == "InsertNew")
          {
            this.ProjectView.InsertNew();
            goto label_133;
          }
          goto default;
        case 3406190625:
          if (commandName == "PrintDocument")
            break;
          goto default;
        case 3590187823:
          if (commandName == "DecreaseIndent")
          {
            this.ProjectView.DecreaseIndent();
            goto label_133;
          }
          goto default;
        case 3799286124:
          if (commandName == "PrintPreview")
            break;
          goto default;
        case 3841988537:
          if (commandName == "CheckOut")
          {
            if (selectedTask1 != null)
            {
              selectedTask1.CheckOut();
              goto label_133;
            }
            goto label_133;
          }
          goto default;
        case 3895594280:
          if (commandName == "Print")
            break;
          goto default;
        case 3898821075:
          if (commandName == "Export")
          {
            this.SaveToSimpleXml();
            goto label_133;
          }
          goto default;
        case 3903265834:
          if (commandName == "LevelResources")
          {
            this.Project.LevelResources(true);
            goto label_133;
          }
          goto default;
        case 3911021853:
          if (commandName == "InsertProject")
          {
            this.ProjectView.InsertProject();
            goto label_133;
          }
          goto default;
        default:
          flag = false;
          goto label_133;
      }
      using (PrintPreviewForm printPreviewForm = new PrintPreviewForm(this.Services, this.Mode == ProjectEditorMode.Project ? "PrintProjectPreview" : "PrintResourcesPreview"))
      {
        int num = (int) printPreviewForm.ShowDialog();
      }
    }
    catch (Exception ex)
    {
      if (!ex.TryProcessExceptionOnClient((IWin32Window) this))
        throw;
    }
label_133:
    return flag;
  }

  public bool QueryStatus(ICommandState commandState)
  {
    if (this._project == null)
      return false;
    bool flag1 = true;
    bool flag2 = false;
    bool? nullable = new bool?();
    try
    {
      switch (commandState.CommandName)
      {
        case "CheckIn":
          flag2 = !this.EditingMode.ReadOnly();
          if (flag2)
            flag2 = this.ProjectView.SelectedTask is Intermech.Project.Project selectedTask1 && selectedTask1.CheckInPossible;
          if (flag2)
          {
            nullable = new bool?(true);
            break;
          }
          break;
        case "CheckOut":
          flag2 = !this.EditingMode.ReadOnly();
          if (flag2)
            flag2 = this.ProjectView.SelectedTask is Intermech.Project.Project selectedTask2 && selectedTask2.CheckOutPossible;
          if (flag2)
          {
            nullable = new bool?(true);
            break;
          }
          break;
        case "ConvertToProject":
          Task selectedTask3 = this.ProjectView.SelectedTask;
          flag2 = this.EditingMode.HasComposition() && selectedTask3 != null && !selectedTask3.Milestone && !(selectedTask3 is Intermech.Project.Project);
          if (flag2)
          {
            nullable = new bool?(true);
            break;
          }
          break;
        case "ConvertToTask":
          flag2 = this.EditingMode.HasComposition() && this.ProjectView.SelectedTask is Intermech.Project.Project && this.ProjectView.SelectedTask != this.Project;
          if (flag2)
          {
            nullable = new bool?(true);
            break;
          }
          break;
        case "Copy":
          flag2 = this.ProjectView.HasSelected;
          break;
        case "CreateReport":
        case "Export":
        case "Filters":
        case "Import":
        case "ImportObject":
        case "LineStyleSetup":
        case "PreviewReport":
        case "Print":
        case "PrintDocument":
        case "PrintPreview":
        case "ValidateProject":
          flag2 = true;
          break;
        case "Cut":
        case "Delete":
          flag2 = this.ProjectView.CanDelete;
          break;
        case "DecreaseIndent":
          flag2 = this.ProjectView.CanDecreaseIndent;
          break;
        case "EditProject":
        case "ViewProject":
          if (this.Mode == ProjectEditorMode.Resources)
          {
            flag2 = this.ProjectView.SelectedTask is ResourceAssignmentsSubProject;
            if (flag2)
            {
              nullable = new bool?(true);
              break;
            }
            break;
          }
          if (this.Mode == ProjectEditorMode.Project)
          {
            flag2 = this.EditingMode.HasComposition() && this.ProjectView.SelectedTask is Intermech.Project.Project && this.ProjectView.SelectedTask != this.Project;
            if (flag2)
            {
              nullable = new bool?(true);
              break;
            }
            break;
          }
          break;
        case "Find":
        case "LevelResources":
          flag2 = true;
          break;
        case "IncreaseIndent":
          flag2 = this.ProjectView.CanIncreaseIndent;
          break;
        case "InsertNew":
        case "InsertProject":
          flag2 = this.EditingMode.HasComposition();
          break;
        case "Paste":
          flag2 = this.ProjectView.CanPaste;
          break;
        case "ProjectProperties":
          flag2 = true;
          break;
        case "Properties":
          flag2 = this.ProjectView.HasSelected;
          break;
        case "Refresh":
          flag2 = true;
          break;
        case "Save":
          flag2 = this._project.Modified;
          break;
        case "SpecialCommands":
          flag2 = SpecialCommands.AnyCommandVisible;
          if (flag2)
          {
            nullable = new bool?(true);
            break;
          }
          break;
        case "Sync":
          flag2 = SiteID.PortalEnabled;
          if (flag2)
          {
            nullable = new bool?(true);
            break;
          }
          break;
        case "SyncWithImportedObjectComposition":
          flag2 = true;
          nullable = new bool?(true);
          break;
        case "WorkshopRouteProcessing":
          flag2 = this._workshopRouteProcessingCommand.Enabled;
          if (flag2)
          {
            nullable = new bool?(true);
            break;
          }
          break;
        default:
          flag1 = false;
          break;
      }
    }
    catch
    {
    }
    if (flag1)
    {
      if (nullable.HasValue)
        commandState.Visible = nullable.Value;
      commandState.Enabled = flag2;
    }
    return flag1;
  }

  [CanBeNull]
  public ClientProject Project
  {
    get => this._project;
    set
    {
      if (this._project == value)
        return;
      if (this._project != null)
      {
        if (this._project.ID != 0L)
          ProjectEditorForm._openedProjectForms.TryRemove(Math.Abs(this._project.ID), out ProjectEditorForm _);
        this.RemoveService<ClientProject>();
        this.RemoveService<Intermech.Project.Project>();
        this.RemoveService<ISessionProvider>();
      }
      this._project = value;
      if (this._project != null)
      {
        this.AddService<ClientProject>(this._project);
        this.AddService<Intermech.Project.Project>((Intermech.Project.Project) this._project);
        this.AddService<ISessionProvider>((ISessionProvider) this._project);
      }
      if (this.ProjectView.Project == null)
        this.LoadState();
      if (this._project != null)
      {
        this._project.ModifiedChanged += new EventHandler(this.Project_ModifiedChanged);
        this._project.Saving += new CancelEventHandler(this._project_Saving);
        this._project.Saved += new EventHandler(this._project_Saved);
        this._project.PropertyChanged += new PropertyChangedEventHandler(this.Project_PropertyChanged);
      }
      this.Refresh();
      this.ProjectView.Project = this._project;
      this.UpdateTitle();
      ProjectSettings.Apply(this.ProjectView);
      this.RefreshFilters();
      ProjectEditorForm.CurrentProject = this._project;
      if (this._project.ObjectID != 0L)
        Editors.RegisterEditor((Control) this, this._project.ObjectID, this.EditingMode.Any());
      if (this._project.ID == 0L)
        return;
      ProjectEditorForm._openedProjectForms.AddOrUpdate(Math.Abs(this._project.ID), this, (Func<long, ProjectEditorForm, ProjectEditorForm>) ((_, __) => this));
    }
  }

  private void ComboBox_DropDownClosed([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    bool allowUserToAddRows = this.ProjectView.GridView.AllowUserToAddRows;
    if (allowUserToAddRows)
      this.ProjectView.GridView.AllowUserToAddRows = false;
    try
    {
      this.ProjectView.Focus();
    }
    finally
    {
      if (allowUserToAddRows)
        this.ProjectView.GridView.AllowUserToAddRows = true;
    }
  }

  private void FiltersComboBox_SelectedValueChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    bool flag = false;
    if (this._project != null)
    {
      TaskFilter taskFilter = this.FiltersComboBoxItem.ComboBox.SelectedItem as TaskFilter;
      if (taskFilter.RequiresInput)
      {
        taskFilter = new TaskFilter(taskFilter);
        if (!FilterDialogForm.Query(taskFilter, this.Project))
          flag = true;
      }
      if (!flag)
      {
        try
        {
          if (taskFilter.AllTasks)
            taskFilter = (TaskFilter) null;
          this._project.Filter = taskFilter;
          this.GanttChart.VisibleTaskIndex = 0;
          this.Project?.ClearVisibleTaskIndexes();
          this.ProjectView.GridView.RefreshVisibility();
          this.ProjectView.RefreshGanttView();
          if (this._project._FilterError != string.Empty)
            throw new ArgumentException(this._project._FilterError);
        }
        catch (Exception ex)
        {
          int num = (int) MessageBox.Show(ex.Message, (string) null, MessageBoxButtons.OK, MessageBoxIcon.Hand);
          flag = true;
        }
      }
    }
    if (flag)
    {
      if (this._prevSelectedTaskFilter != null && !this._project.Filter.Name.Equals(this._prevSelectedTaskFilter.Name))
        this.FiltersComboBoxItem.ComboBox.SelectedItem = (object) this._prevSelectedTaskFilter;
      else
        this.FiltersComboBoxItem.ComboBox.SelectedIndex = 0;
    }
    this._prevSelectedTaskFilter = this.FiltersComboBoxItem.ComboBox.SelectedItem as TaskFilter;
  }

  private void Project_PropertyChanged([CanBeNull] object sender, [NotNull] PropertyChangedEventArgs e)
  {
    if (e.PropertyName == "Name")
      this.UpdateTitle();
    if (this._paintFilterTimer == null)
      this._paintFilterTimer = new System.Threading.Timer(new TimerCallback(this.RefreshPaintFilters), (object) null, 100, -1);
    else
      this._paintFilterTimer.Change(100, -1);
  }

  public void RefreshPaintFilters([CanBeNull] object stateInfo) => this.RefreshPaintFilters();

  public bool Save()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.ProjectView.Validate();
      this._saved = this._project.Save(sessionKeeper.Session);
      return this._saved;
    }
  }

  public EditingMode EditingMode => this.ProjectView.EditingMode;

  protected void UpdateTitle()
  {
    if (this._project == null)
      return;
    string str = this._project.Name;
    if (this._project.Status != TaskStatus.NotStarted)
      str = $"{str} ({this._project.StatusString})";
    if (this.EditingMode.ReadOnly() && !(this._project is ResourceAssignmentsProject))
      str = $"{str} [{Intermech.Project.Localization.GetString("ReadOnly")}]";
    if (this.EditingMode.HasProperties() != this.EditingMode.HasComposition())
      str = !this.EditingMode.HasProperties() ? str + " [Только состав]" : str + " [Только свойства]";
    if (this.EditingMode.Any() && this._project.Modified)
      str += "*";
    this.Text = str;
    if (!(this.Parent is DockControl parent))
      return;
    parent.Text = this.Text;
  }

  private void Project_ModifiedChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.UpdateTitle();
    this.UpdateCommands();
  }

  private void LoadFromMsProjectXml()
  {
    DialogResult dialogResult = DialogResult.Yes;
    if (this.Project.Tasks.Count > 0)
      dialogResult = MessageFuncs.Ask(Intermech.Project.Localization.GetString("ImportPrompt", (object) this.Project.Name));
    if (dialogResult != DialogResult.Yes)
      return;
    OpenFileDialog openFileDialog = new OpenFileDialog();
    openFileDialog.Filter = "Microsoft Project File (Saved as XML)|*.xml";
    openFileDialog.RestoreDirectory = true;
    if (openFileDialog.ShowDialog() != DialogResult.OK)
      return;
    this.ProjectView.Refresh();
    try
    {
      this.Project.ImportFromMsProjectXml(openFileDialog.FileName);
    }
    finally
    {
      this.ProjectView.Project = this.Project;
    }
  }

  [NotNull]
  private static string TaskNamesString([NotNull] Intermech.Project.Project p)
  {
    string str1 = string.Empty;
    string empty = string.Empty;
    foreach (Task task in (System.Collections.ObjectModel.Collection<Task>) p.Tasks)
    {
      if (!task.IsProjectSummaryTask)
      {
        str1 = !(str1 != string.Empty) ? $"Project: {p.Name} [{p.Tasks.Count}]: " : str1 + ", ";
        str1 += task.NameInMessages;
        if (task is Intermech.Project.Project p1)
        {
          string str2 = ProjectEditorForm.TaskNamesString(p1);
          if (str2 != string.Empty)
          {
            if (empty != string.Empty)
              empty += "\r\n\r\n";
            empty += str2;
          }
        }
      }
    }
    if (empty != string.Empty)
      str1 += "\r\n\r\n";
    return str1 + empty;
  }

  protected override void OnKeyDown(KeyEventArgs e)
  {
    base.OnKeyDown(e);
    if (!e.Shift || !e.Control || !e.Alt)
      return;
    switch (e.KeyCode)
    {
      case Keys.C:
        this._project.DebugClearCache();
        break;
      case Keys.E:
        string text1 = this._project.DebugGraphErrors();
        if (!(text1 != string.Empty))
          break;
        int num1 = (int) MessageBox.Show(text1);
        break;
      case Keys.F:
        Intermech.Project.Evaluator.Evaluator.InDebug = true;
        try
        {
          this._project.Filter = TaskFilters.All[1];
          foreach (Task task in (System.Collections.ObjectModel.Collection<Task>) this.Project.Tasks)
          {
            int num2 = task.HiddenByFilter ? 1 : 0;
          }
        }
        finally
        {
          Intermech.Project.Evaluator.Evaluator.InDebug = false;
        }
        this.ProjectView.GridView.RefreshVisibility();
        this.ProjectView.RefreshGanttView();
        break;
      case Keys.G:
        int num3 = (int) MessageBox.Show(this._project.DebugPrintGraph());
        break;
      case Keys.I:
        string text2 = string.Empty;
        Task selectedTask = this.ProjectView.SelectedTask;
        if (selectedTask != null)
        {
          string str1 = $"{$"{$"{$"{$"{$"{$"{$"{$"{$"{$"{$"{$"{$"{$"{$"{text2}Task: {selectedTask.Name}"}\r\nFull name: {selectedTask.NameInMessages}"}\r\nObjectID: {(object) selectedTask.ObjectID}"}\r\nType: {selectedTask.GetType().Name}"}\r\nProject: {selectedTask.Project?.Name ?? "{null}"}"}\r\nProjectID: {selectedTask.Project?.ObjectID.ToString() ?? "{null}"}"}\r\nParent: {selectedTask.Parent?.Name ?? "{null}"}"}\r\nWork: {(object) selectedTask.Work}"}\r\nRealWork: {(object) selectedTask.RealWork}"}\r\nIndentLevel: {(object) selectedTask.IndentLevel}"}\r\nRealIndentLevel: {(object) selectedTask.RealIndentLevel}"}\r\nIndex: {(object) selectedTask.Index}"}\r\nRealIndex: {(object) selectedTask.RealIndex}"}\r\nDependencies: {(object) selectedTask.Dependencies.Count}"}\r\nBackDependencies: {(object) selectedTask.BackDependencies.Count}"}\r\nHasSubTasks: {selectedTask.HasSubTasks.ToString()}";
          bool flag = selectedTask.HasNotLoadedSubTasks;
          string str2 = flag.ToString();
          string str3 = $"{$"{$"{$"{str1}\r\nHasNotLoadedSubTasks: {str2}"}\r\nConstraintType: {(object) selectedTask.ConstraintType}"}\r\nConstraintDate: {(object) selectedTask.ConstraintDate}"}\r\nPlannedPercentCompleted: {(object) selectedTask.PlannedPercentCompleted}";
          flag = selectedTask.Uncommitted;
          string str4 = flag.ToString();
          text2 = $"{str3}\r\nUncommitted: {str4}";
        }
        if (this.ProjectView.DataGridView.CurrentCell != null)
        {
          string str = Regex.Replace(this.ProjectView.DataGridView.CurrentCell.Style?.ToString() ?? string.Empty, "[\\[\\]\\{\\}],?", "\r").Replace("=\r", "=\r  ");
          text2 = $"{text2}\r\n----\r\nStyle: {str.Trim()}";
        }
        if (selectedTask is Intermech.Project.Project project)
        {
          string enumDescription = SimpleFuncs.GetEnumDescription((Enum) project.RemoteStatus);
          text2 = $"{text2}\r\n----\r\nRemote process status: {enumDescription}";
        }
        int num4 = (int) MessageBox.Show(text2);
        break;
      case Keys.M:
        this.GanttChart.ShowConstraintMarkers = !this.GanttChart.ShowConstraintMarkers;
        break;
      case Keys.T:
        int num5 = (int) MessageBox.Show(ProjectEditorForm.TaskNamesString((Intermech.Project.Project) this.Project));
        break;
      case Keys.Multiply:
        Task._IndicateModifiedTasks = !Task._IndicateModifiedTasks;
        this.Refresh();
        break;
    }
  }

  private void SaveToSimpleXml()
  {
    SaveFileDialog saveFileDialog = new SaveFileDialog();
    saveFileDialog.Filter = "XML|*.xml";
    saveFileDialog.DefaultExt = "xml";
    saveFileDialog.FileName = this.Project.Name;
    saveFileDialog.RestoreDirectory = true;
    if (saveFileDialog.ShowDialog() != DialogResult.OK)
      return;
    this.Project.SaveToSimpleXml(saveFileDialog.FileName);
  }

  internal ProjectEditorMode Mode
  {
    get => this._mode;
    set
    {
      this._mode = value;
      this.HorizontalSplitter.Visible = value == ProjectEditorMode.Resources;
      this.ResourcesSummaryView.Visible = value == ProjectEditorMode.Resources;
      if (value != ProjectEditorMode.Resources)
        return;
      this.ProjectView.GanttChart.BarWidth = 60f;
      this.ResourcesSummaryView.ChartView.BarWidth = 60f;
      SplitterCancelEventHandler cancelEventHandler = new SplitterCancelEventHandler(this.SplitContainer_SplitterMoving);
      this.ProjectView.SplitContainer.SplitterDistance = this.ResourcesSummaryView.SplitContainer.SplitterDistance;
      this.ProjectView.SplitContainer.SplitterMoving += cancelEventHandler;
      this.ResourcesSummaryView.SplitContainer.SplitterMoving += cancelEventHandler;
      ScrollEventHandler scrollEventHandler = new ScrollEventHandler(this.GanttChartView_Scroll);
      this.ProjectView.GanttChart.CurrentDateScrollBar.Scroll += scrollEventHandler;
      this.ResourcesSummaryView.ChartView.CurrentDateScrollBar.Scroll += scrollEventHandler;
      EventHandler eventHandler1 = new EventHandler(this.GanttChartView_ScaleTypeChanged);
      this.ProjectView.GanttChart.ScaleTypeChanged += eventHandler1;
      this.ResourcesSummaryView.ChartView.ScaleTypeChanged += eventHandler1;
      EventHandler eventHandler2 = new EventHandler(this.ProjectView_SelectedUserTaskChanged);
      this.ProjectView.SelectedUserTaskChanged += eventHandler2;
      this.ResourcesSummaryView.SelectedUserTaskChanged += eventHandler2;
      this.ResourcesSummaryView.ChartView.CurrentDate = this.ProjectView.GanttChart.CurrentDate;
    }
  }

  private void ProjectView_SelectedUserTaskChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (sender == this.ProjectView)
      this.ResourcesSummaryView.SelectedUserTask = this.ProjectView.SelectedUserTask;
    else
      this.ProjectView.SelectedUserTask = this.ResourcesSummaryView.SelectedUserTask;
  }

  private void GanttChartView_ScaleTypeChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (!(sender is GanttChart ganttChart))
      return;
    if (ganttChart == this.ProjectView.GanttChart)
      this.ResourcesSummaryView.ChartView.ScaleType = this.ProjectView.GanttChart.ScaleType;
    else
      this.ProjectView.GanttChart.ScaleType = this.ResourcesSummaryView.ChartView.ScaleType;
  }

  private void GanttChartView_Scroll([CanBeNull] object sender, [NotNull] ScrollEventArgs e)
  {
    if (!(sender is HScrollBar hscrollBar) || !(hscrollBar.Parent is GanttChart parent))
      return;
    if (parent == this.ProjectView.GanttChart)
      this.ResourcesSummaryView.ChartView.CurrentDate = this.ProjectView.GanttChart.CurrentDate;
    else
      this.ProjectView.GanttChart.CurrentDate = this.ResourcesSummaryView.ChartView.CurrentDate;
  }

  private void SplitContainer_SplitterMoving([CanBeNull] object sender, [NotNull] SplitterCancelEventArgs e)
  {
    if ((SplitContainer) sender == this.ProjectView.SplitContainer)
      this.ResourcesSummaryView.SplitContainer.SplitterDistance = e.SplitX;
    else
      this.ProjectView.SplitContainer.SplitterDistance = e.SplitX;
  }

  protected override void OnMouseWheel(MouseEventArgs e)
  {
    base.OnMouseWheel(e);
    if (this.ProjectView.GanttChart.HandleParentMouseWheel((Control) this, e) || this.ResourcesSummaryView.ChartView.HandleParentMouseWheel((Control) this, e))
      return;
    Point screen = this.PointToScreen(e.Location);
    WinApi.SendMessage(this.ProjectView.VScrollBar.Handle, 522, e.Delta << 16 /*0x10*/, screen.Y << 16 /*0x10*/ + screen.X);
  }

  private void InitPersistString()
  {
    if (this.DockControl == null || this.Project == null)
      return;
    List<string> stringList = new List<string>();
    stringList.Add(Convert.ToInt32((object) this.Mode).ToString());
    stringList.Add(Convert.ToInt32((object) this.Project.EditingMode).ToString());
    if (this.Mode == ProjectEditorMode.Project)
    {
      if (this.Project.ObjectID == 0L)
        return;
      stringList.Add(this.Project.ObjectID.ToString());
    }
    else if (this.Project is ResourceAssignmentsProject project)
    {
      string empty = string.Empty;
      foreach (long objectId in project.ObjectIDs)
      {
        if (empty != string.Empty)
          empty += ",";
        empty += objectId.ToString();
      }
      stringList.Add(empty);
    }
    this.DockControl.PersistString = string.Join("|", stringList.ToArray());
  }

  internal static bool ParsePersistString(
    [NotNull] string persistString,
    ref ProjectEditorMode mode,
    ref bool editingMode,
    [CanBeNull] ref List<long> ids)
  {
    string[] strArray = persistString.Split('|');
    if (strArray.Length != 3)
      return false;
    mode = (ProjectEditorMode) Convert.ToInt32(strArray[0]);
    editingMode = Convert.ToBoolean(Convert.ToInt32(strArray[1]));
    string[] source = strArray[2].Split(',');
    ids = ((IEnumerable<string>) source).Select<string, long>((Func<string, long>) (s => Convert.ToInt64(s))).ToList<long>();
    return true;
  }

  [NotNull]
  private ContextMenuBarItem GanttHeaderContextMenu
  {
    get
    {
      if (this._ganttHeaderContextMenu == null)
      {
        this._ganttHeaderContextMenu = new ContextMenuBarItem();
        this._ganttHeaderContextMenu.Items.Add(Intermech.Project.Localization.GetString("CmdScale"), new EventHandler(this.ScaleMiClick));
        this._ganttHeaderContextMenu.Items.Add(Intermech.Project.Localization.GetString("CmdCalendars"), new EventHandler(ProjectEditorForm.OpenCalendarsMiClick));
        this._ganttHeaderContextMenu.Items[this._ganttHeaderContextMenu.Items.Count - 1].BeginGroup = true;
      }
      return this._ganttHeaderContextMenu;
    }
  }

  private void ScaleMiClick([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    using (ScaleSettingsForm scaleSettingsForm = new ScaleSettingsForm())
    {
      scaleSettingsForm.Options = this.Project.DisplayOptions;
      int num = (int) scaleSettingsForm.ShowDialog();
    }
  }

  private static void OpenCalendarsMiClick([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    Intermech.Navigator.Utils.OpenNewWindow((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor((int) (IpsMetadataEntityBase<int>) Intermech.Metadata.ObjectTypes.Calendar), (System.IServiceProvider) null);
  }

  private void GanttChart_HeaderClick([CanBeNull] object sender, [NotNull] MouseEventArgs e)
  {
    if (e.Button != MouseButtons.Right)
      return;
    this.GanttHeaderContextMenu.Show(Intermech.Client.Services.PopupHost, sender as Control, new Point(e.X, e.Y));
  }

  public static ClientProject CurrentProject { get; internal set; }

  private void NotificationObjectsRemoved(
    [CanBeNull] object sender,
    [NotNull] NotificationEventArgs notificationEventArgs)
  {
    DBObjectsEventArgs objectsEventArgs = notificationEventArgs as DBObjectsEventArgs;
    ResourceAssignmentsProject project = this.Project as ResourceAssignmentsProject;
    if (objectsEventArgs?.ObjectIDs == null || this._project == null || project != null || this._project.ObjectID == 0L || !objectsEventArgs.ObjectIDs.Contains(this._project.ObjectID))
      return;
    this.ForceClose();
  }

  public void ForceClose()
  {
    this._forceClose = true;
    if (this.Parent is DockControl parent)
      parent.Close();
    else
      this.Close();
  }

  private void NotificationObjectsChanged(
    [CanBeNull] object sender,
    [NotNull] NotificationEventArgs notificationEventArgs)
  {
    DBObjectsEventArgs objectsEventArgs = notificationEventArgs as DBObjectsEventArgs;
    ResourceAssignmentsProject project = this.Project as ResourceAssignmentsProject;
    if (objectsEventArgs?.ObjectIDs == null || this._project == null || project != null || this._project.ObjectID == 0L || this._forceClose || !objectsEventArgs.ObjectIDs.Contains(this._project.ObjectID) || this.Project.Modified)
      return;
    this.ReloadProject();
  }

  [NotNull]
  public ICommandManager CommandManager
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Intermech.Client.Services.CommandManager;
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ProjectEditorForm));
    this._toolBar = new Intermech.Bars.ToolBar();
    this._filtersComboBoxItem = new ComboBoxItem();
    this._horizontalSplitter = new Splitter();
    this._projectView = new ProjectView();
    this._resourcesSummaryView = new ResourcesSummaryView();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._toolBar, "_toolBar");
    this._toolBar.FullMenus = true;
    this._toolBar.Guid = new Guid("493e306f-2a46-4710-a2eb-59cdd2af402a");
    this._toolBar.Hidden = false;
    this._toolBar.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this._filtersComboBoxItem
    });
    this._toolBar.Name = "_toolBar";
    this._filtersComboBoxItem.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._filtersComboBoxItem, "_filtersComboBoxItem");
    this._filtersComboBoxItem.DropDownStyle = ComboBoxStyle.DropDownList;
    this._filtersComboBoxItem.MinimumControlWidth = 200;
    this._filtersComboBoxItem.Padding.Bottom = 0;
    this._filtersComboBoxItem.Padding.Left = 1;
    this._filtersComboBoxItem.Padding.Right = 1;
    this._filtersComboBoxItem.Padding.Top = 0;
    componentResourceManager.ApplyResources((object) this._horizontalSplitter, "_horizontalSplitter");
    this._horizontalSplitter.Name = "_horizontalSplitter";
    this._horizontalSplitter.TabStop = false;
    componentResourceManager.ApplyResources((object) this._projectView, "_projectView");
    this._projectView.AllowEditForm = true;
    this._projectView.AllowGanttChartTaskDrag = true;
    this._projectView.Name = "_projectView";
    this._projectView.RowHeight = 21;
    this._projectView.ScaleType = ScaleType.Weeks;
    this._projectView.UseDataGridViewColumnHeaderContextMenu = true;
    this._projectView.UseDataGridViewRowHeaderContextMenu = true;
    componentResourceManager.ApplyResources((object) this._resourcesSummaryView, "_resourcesSummaryView");
    this._resourcesSummaryView.Name = "_resourcesSummaryView";
    this._resourcesSummaryView.SelectedUserTask = (UserSummaryTask) null;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._projectView);
    this.Controls.Add((Control) this._horizontalSplitter);
    this.Controls.Add((Control) this._resourcesSummaryView);
    this.Controls.Add((Control) this._toolBar);
    this.Name = nameof (ProjectEditorForm);
    this.Load += new EventHandler(this.ProjectEditorForm_Load);
    this.ResumeLayout(false);
  }
}
