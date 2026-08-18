// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.ProjectDataGridView
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Bars;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Metadata;
using Intermech.Project.Controls.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

/// <summary>Грид в левой части окна редактора проекта, с деревом задач и их параметрами</summary>
[Designer("System.Windows.Forms.Design.ControlDesigner, System.Design")]
public class ProjectDataGridView : 
  DataGridView,
  ISupportInitialize,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IComponent,
  IDisposable
{
  private ProjectGridTextBoxColumn _imagesColumn;
  private EnhDataGridViewTextBoxColumn _nameDataGridViewColumn;
  private DurationColumn _durationDataGridViewColumn;
  private DurationColumn _workDataGridViewColumn;
  private DataGridViewDateTimeTextBoxColumn _startDataGridViewColumn;
  private DataGridViewDateTimeTextBoxColumn _finishDataGridViewColumn;
  private EnhDataGridViewTextBoxColumn _dependenciesDataGridViewColumn;
  private EnhDataGridViewTextBoxColumn _assignmentsDataGridViewColumn;
  private EnhDataGridViewTextBoxColumn _priorityDataGridViewColumn;
  private EnhDataGridViewTextBoxColumn _percentCompletedDataGridViewColumn;
  private EnhDataGridViewTextBoxColumn _statusDataGridViewColumn;
  private EnhDataGridViewTextBoxColumn _wbsCodeDataGridViewColumn;
  private EnhDataGridViewTextBoxColumn _completedWorkDataGridViewColumn;
  private EnhDataGridViewTextBoxColumn _remainingWorkDataGridViewColumn;
  private DataGridViewDateTimeTextBoxColumn _startConstraintDataGridViewColumn;
  private DataGridViewDateTimeTextBoxColumn _finishConstraintDataGridViewColumn;
  private EnhDataGridViewTextBoxColumn _notesDataGridViewColumn;
  private bool _duringUpdate;
  private Control _editControl;
  private const int IndentLevelSize = 10;
  [NotNull]
  private ColumnLayoutInformation[] _initColumnsLayoutInformation;
  private ClientProject _project;
  [NotNull]
  [ItemNotNull]
  private readonly HashSet<DataGridViewColumn> _dateColumns = new HashSet<DataGridViewColumn>();
  private const int EditButtonWidth = 17;
  private readonly bool _skipPaintCellBackground;
  private Point _lastMousePos = Point.Empty;
  [CanBeNull]
  private ContextMenuBarItem _headerMenu;
  private Dictionary<string, ColumnInfo> _possibleAdditionalColumns;
  [NotNull]
  private readonly List<DataGridViewRow> _hiddenByFilter = new List<DataGridViewRow>();
  [NotNull]
  private readonly List<string> _trackedProps = new List<string>((IEnumerable<string>) new string[8]
  {
    "IndentLevel",
    "HasSubTasks",
    "StartConstraint",
    "FinishConstraint",
    "Notes",
    "Status",
    "PlanningConflict",
    "PendingSiteID"
  });
  private int _lockUpdateTasksCounter;
  private bool _multiline;
  private Rectangle _dragBoxFromMouseDown;
  private int _rowIndexFromMouseDown = -1;
  private int _rowIndexOfItemUnderMouseToDrop;
  private DataGridView.HitTestInfo _lastHitTest;
  internal bool _IsMouseDown;
  private bool _forcedEdit;
  internal GanttChart _GanttChart;

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal ProjectGridTextBoxColumn ImagesColumn
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._imagesColumn.CheckInitializedIn<ProjectGridTextBoxColumn>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal EnhDataGridViewTextBoxColumn NameDataGridViewColumn
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._nameDataGridViewColumn.CheckInitializedIn<EnhDataGridViewTextBoxColumn>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal DurationColumn DurationDataGridViewColumn
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._durationDataGridViewColumn.CheckInitializedIn<DurationColumn>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal DurationColumn WorkDataGridViewColumn
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._workDataGridViewColumn.CheckInitializedIn<DurationColumn>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal DataGridViewDateTimeTextBoxColumn StartDataGridViewColumn
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._startDataGridViewColumn.CheckInitializedIn<DataGridViewDateTimeTextBoxColumn>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal DataGridViewDateTimeTextBoxColumn FinishDataGridViewColumn
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._finishDataGridViewColumn.CheckInitializedIn<DataGridViewDateTimeTextBoxColumn>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal EnhDataGridViewTextBoxColumn DependenciesDataGridViewColumn
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._dependenciesDataGridViewColumn.CheckInitializedIn<EnhDataGridViewTextBoxColumn>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal EnhDataGridViewTextBoxColumn AssignmentsDataGridViewColumn
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._assignmentsDataGridViewColumn.CheckInitializedIn<EnhDataGridViewTextBoxColumn>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal EnhDataGridViewTextBoxColumn PriorityDataGridViewColumn
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._priorityDataGridViewColumn.CheckInitializedIn<EnhDataGridViewTextBoxColumn>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal EnhDataGridViewTextBoxColumn PercentCompletedDataGridViewColumn
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._percentCompletedDataGridViewColumn.CheckInitializedIn<EnhDataGridViewTextBoxColumn>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal EnhDataGridViewTextBoxColumn StatusDataGridViewColumn
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._statusDataGridViewColumn.CheckInitializedIn<EnhDataGridViewTextBoxColumn>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal EnhDataGridViewTextBoxColumn WbsCodeDataGridViewColumn
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._wbsCodeDataGridViewColumn.CheckInitializedIn<EnhDataGridViewTextBoxColumn>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal EnhDataGridViewTextBoxColumn CompletedWorkDataGridViewColumn
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._completedWorkDataGridViewColumn.CheckInitializedIn<EnhDataGridViewTextBoxColumn>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal EnhDataGridViewTextBoxColumn RemainingWorkDataGridViewColumn
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._remainingWorkDataGridViewColumn.CheckInitializedIn<EnhDataGridViewTextBoxColumn>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal DataGridViewDateTimeTextBoxColumn StartConstraintDataGridViewColumn
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._startConstraintDataGridViewColumn.CheckInitializedIn<DataGridViewDateTimeTextBoxColumn>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal DataGridViewDateTimeTextBoxColumn FinishConstraintDataGridViewColumn
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._finishConstraintDataGridViewColumn.CheckInitializedIn<DataGridViewDateTimeTextBoxColumn>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal EnhDataGridViewTextBoxColumn NotesDataGridViewColumn
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._notesDataGridViewColumn.CheckInitializedIn<EnhDataGridViewTextBoxColumn>((object) this);
    }
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ProjectDataGridView));
    DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle3 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle4 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle5 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle6 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle7 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle8 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle9 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle10 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle11 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle12 = new DataGridViewCellStyle();
    gridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle12.BackColor = SystemColors.Window;
    gridViewCellStyle12.Font = new Font("Microsoft Sans Serif", 7.2f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle12.ForeColor = SystemColors.ControlText;
    gridViewCellStyle12.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle12.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle12.WrapMode = DataGridViewTriState.False;
    this._imagesColumn = new ProjectGridTextBoxColumn();
    this._nameDataGridViewColumn = new EnhDataGridViewTextBoxColumn();
    this._durationDataGridViewColumn = new DurationColumn();
    this._workDataGridViewColumn = new DurationColumn();
    this._startDataGridViewColumn = new DataGridViewDateTimeTextBoxColumn();
    this._finishDataGridViewColumn = new DataGridViewDateTimeTextBoxColumn();
    this._dependenciesDataGridViewColumn = new EnhDataGridViewTextBoxColumn();
    this._assignmentsDataGridViewColumn = new EnhDataGridViewTextBoxColumn();
    this._priorityDataGridViewColumn = new EnhDataGridViewTextBoxColumn();
    this._percentCompletedDataGridViewColumn = new EnhDataGridViewTextBoxColumn();
    this._statusDataGridViewColumn = new EnhDataGridViewTextBoxColumn();
    this._wbsCodeDataGridViewColumn = new EnhDataGridViewTextBoxColumn();
    this._completedWorkDataGridViewColumn = new EnhDataGridViewTextBoxColumn();
    this._remainingWorkDataGridViewColumn = new EnhDataGridViewTextBoxColumn();
    this._startConstraintDataGridViewColumn = new DataGridViewDateTimeTextBoxColumn();
    this._finishConstraintDataGridViewColumn = new DataGridViewDateTimeTextBoxColumn();
    this._notesDataGridViewColumn = new EnhDataGridViewTextBoxColumn();
    ((ISupportInitialize) this).BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._imagesColumn, "ImagesColumn");
    this._imagesColumn.Name = "ImagesColumn";
    this._imagesColumn.ReadOnly = true;
    this._imagesColumn.Resizable = DataGridViewTriState.True;
    this._imagesColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
    this._nameDataGridViewColumn.DataPropertyName = "Name";
    gridViewCellStyle1.Padding = new Padding(16 /*0x10*/, 0, 0, 0);
    this._nameDataGridViewColumn.DefaultCellStyle = gridViewCellStyle1;
    this._nameDataGridViewColumn.FillWeight = 144f;
    componentResourceManager.ApplyResources((object) this._nameDataGridViewColumn, "nameDataGridViewColumn");
    this._nameDataGridViewColumn.Name = "nameDataGridViewColumn";
    this._durationDataGridViewColumn.DataPropertyName = "DurationString";
    gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
    this._durationDataGridViewColumn.DefaultCellStyle = gridViewCellStyle2;
    componentResourceManager.ApplyResources((object) this._durationDataGridViewColumn, "durationDataGridViewColumn");
    this._durationDataGridViewColumn.Name = "durationDataGridViewColumn";
    this._workDataGridViewColumn.DataPropertyName = "WorkString";
    gridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
    this._workDataGridViewColumn.DefaultCellStyle = gridViewCellStyle3;
    componentResourceManager.ApplyResources((object) this._workDataGridViewColumn, "workDataGridViewColumn");
    this._workDataGridViewColumn.Name = "workDataGridViewColumn";
    this._startDataGridViewColumn.DataPropertyName = "StartString";
    gridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight;
    gridViewCellStyle4.Format = "d";
    this._startDataGridViewColumn.DefaultCellStyle = gridViewCellStyle4;
    componentResourceManager.ApplyResources((object) this._startDataGridViewColumn, "startDataGridViewColumn");
    this._startDataGridViewColumn.Name = "startDataGridViewColumn";
    this._finishDataGridViewColumn.DataPropertyName = "FinishString";
    gridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleRight;
    gridViewCellStyle5.Format = "d";
    this._finishDataGridViewColumn.DefaultCellStyle = gridViewCellStyle5;
    this._finishDataGridViewColumn.FillWeight = 105f;
    componentResourceManager.ApplyResources((object) this._finishDataGridViewColumn, "finishDataGridViewColumn");
    this._finishDataGridViewColumn.Name = "finishDataGridViewColumn";
    this._dependenciesDataGridViewColumn.DataPropertyName = "DependenciesString";
    this._dependenciesDataGridViewColumn.FillWeight = 90f;
    componentResourceManager.ApplyResources((object) this._dependenciesDataGridViewColumn, "dependenciesDataGridViewColumn");
    this._dependenciesDataGridViewColumn.Name = "dependenciesDataGridViewColumn";
    this._assignmentsDataGridViewColumn.DataPropertyName = "AssignmentsString";
    this._assignmentsDataGridViewColumn.FillWeight = 120f;
    componentResourceManager.ApplyResources((object) this._assignmentsDataGridViewColumn, "assignmentsDataGridViewColumn");
    this._assignmentsDataGridViewColumn.Name = "assignmentsDataGridViewColumn";
    this._priorityDataGridViewColumn.DataPropertyName = "PriorityString";
    gridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleRight;
    this._priorityDataGridViewColumn.DefaultCellStyle = gridViewCellStyle6;
    this._priorityDataGridViewColumn.FillWeight = 75f;
    componentResourceManager.ApplyResources((object) this._priorityDataGridViewColumn, "priorityDataGridViewColumn");
    this._priorityDataGridViewColumn.Name = "priorityDataGridViewColumn";
    this._percentCompletedDataGridViewColumn.DataPropertyName = "PercentCompletedString";
    gridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleRight;
    this._percentCompletedDataGridViewColumn.DefaultCellStyle = gridViewCellStyle7;
    this._percentCompletedDataGridViewColumn.FillWeight = 75f;
    componentResourceManager.ApplyResources((object) this._percentCompletedDataGridViewColumn, "percentCompletedDataGridViewColumn");
    this._percentCompletedDataGridViewColumn.Name = "percentCompletedDataGridViewColumn";
    this._statusDataGridViewColumn.DataPropertyName = "StatusString";
    this._statusDataGridViewColumn.FillWeight = 75f;
    componentResourceManager.ApplyResources((object) this._statusDataGridViewColumn, "statusDataGridViewColumn");
    this._statusDataGridViewColumn.Name = "statusDataGridViewColumn";
    this._statusDataGridViewColumn.Resizable = DataGridViewTriState.True;
    this._wbsCodeDataGridViewColumn.DataPropertyName = "WbsCode";
    this._wbsCodeDataGridViewColumn.FillWeight = 75f;
    componentResourceManager.ApplyResources((object) this._wbsCodeDataGridViewColumn, "wbsCodeDataGridViewColumn");
    this._wbsCodeDataGridViewColumn.Name = "wbsCodeDataGridViewColumn";
    this._completedWorkDataGridViewColumn.DataPropertyName = "CompletedWorkString";
    gridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleRight;
    this._completedWorkDataGridViewColumn.DefaultCellStyle = gridViewCellStyle8;
    this._completedWorkDataGridViewColumn.FillWeight = 75f;
    componentResourceManager.ApplyResources((object) this._completedWorkDataGridViewColumn, "completedWorkDataGridViewColumn");
    this._completedWorkDataGridViewColumn.Name = "completedWorkDataGridViewColumn";
    this._remainingWorkDataGridViewColumn.DataPropertyName = "RemainingWorkString";
    gridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleRight;
    this._remainingWorkDataGridViewColumn.DefaultCellStyle = gridViewCellStyle9;
    this._remainingWorkDataGridViewColumn.FillWeight = 75f;
    componentResourceManager.ApplyResources((object) this._remainingWorkDataGridViewColumn, "remainingWorkDataGridViewColumn");
    this._remainingWorkDataGridViewColumn.Name = "remainingWorkDataGridViewColumn";
    this._startConstraintDataGridViewColumn.DataPropertyName = "StartConstraintString";
    gridViewCellStyle10.Format = "d";
    this._startConstraintDataGridViewColumn.DefaultCellStyle = gridViewCellStyle10;
    componentResourceManager.ApplyResources((object) this._startConstraintDataGridViewColumn, "startConstraintDataGridViewColumn");
    this._startConstraintDataGridViewColumn.Name = "startConstraintDataGridViewColumn";
    this._finishConstraintDataGridViewColumn.DataPropertyName = "FinishConstraintString";
    gridViewCellStyle11.Format = "d";
    this._finishConstraintDataGridViewColumn.DefaultCellStyle = gridViewCellStyle11;
    componentResourceManager.ApplyResources((object) this._finishConstraintDataGridViewColumn, "finishConstraintDataGridViewColumn");
    this._finishConstraintDataGridViewColumn.Name = "finishConstraintDataGridViewColumn";
    this._notesDataGridViewColumn.DataPropertyName = "NotesString";
    this._notesDataGridViewColumn.FillWeight = 200f;
    componentResourceManager.ApplyResources((object) this._notesDataGridViewColumn, "notesDataGridViewColumn");
    this._notesDataGridViewColumn.Name = "notesDataGridViewColumn";
    this.AllowDrop = true;
    this.AllowUserToOrderColumns = true;
    this.AllowUserToResizeRows = false;
    this.BackgroundColor = SystemColors.Window;
    this.BorderStyle = BorderStyle.None;
    this.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
    this.Columns.AddRange((DataGridViewColumn) this._imagesColumn, (DataGridViewColumn) this._nameDataGridViewColumn, (DataGridViewColumn) this._durationDataGridViewColumn, (DataGridViewColumn) this._workDataGridViewColumn, (DataGridViewColumn) this._startDataGridViewColumn, (DataGridViewColumn) this._finishDataGridViewColumn, (DataGridViewColumn) this._dependenciesDataGridViewColumn, (DataGridViewColumn) this._assignmentsDataGridViewColumn, (DataGridViewColumn) this._priorityDataGridViewColumn, (DataGridViewColumn) this._percentCompletedDataGridViewColumn, (DataGridViewColumn) this._statusDataGridViewColumn, (DataGridViewColumn) this._wbsCodeDataGridViewColumn, (DataGridViewColumn) this._completedWorkDataGridViewColumn, (DataGridViewColumn) this._remainingWorkDataGridViewColumn, (DataGridViewColumn) this._startConstraintDataGridViewColumn, (DataGridViewColumn) this._finishConstraintDataGridViewColumn, (DataGridViewColumn) this._notesDataGridViewColumn);
    this.DefaultCellStyle = gridViewCellStyle12;
    this.EnableHeadersVisualStyles = false;
    this.GridColor = Color.Silver;
    this.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
    this.RowTemplate.Height = 21;
    this.ShowEditingIcon = false;
    ((ISupportInitialize) this).EndInit();
    this.ResumeLayout(false);
  }

  public event EventHandler<TaskExpandedEventArgs> TaskExpanded;

  public ProjectDataGridView()
  {
    this.InitializeComponent();
    this.SetStyle(ControlStyles.ResizeRedraw, true);
    this.AutoGenerateColumns = false;
    this._initColumnsLayoutInformation = this.GetColumnsLayoutInformation();
    DataGridViewCellStyle style = this.NameDataGridViewColumn.HeaderCell.Style;
    DataGridViewCellStyle gridViewCellStyle = style;
    Padding padding1 = style.Padding;
    int left1 = padding1.Left;
    padding1 = this.NameDataGridViewColumn.DefaultCellStyle.Padding;
    int left2 = padding1.Left;
    int left3 = left1 + left2;
    padding1 = style.Padding;
    int top = padding1.Top;
    padding1 = style.Padding;
    int right = padding1.Right;
    padding1 = style.Padding;
    int bottom = padding1.Bottom;
    Padding padding2 = new Padding(left3, top, right, bottom);
    gridViewCellStyle.Padding = padding2;
    this.NameDataGridViewColumn.HeaderCell.Style = style;
    this.DoubleBuffered = true;
    this.Multiline = true;
    this.NameDataGridViewColumn.MaxWidth = 850;
    this.RowTemplate.MinimumHeight = this.RowTemplate.Height;
    this._dateColumns.Add((DataGridViewColumn) this._startDataGridViewColumn);
    this._dateColumns.Add((DataGridViewColumn) this._finishDataGridViewColumn);
    this.NotesDataGridViewColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
  }

  /// <summary>Немного более точная проверка в DesignMode мы или нет. Работает и в конструкторе (обычный DesignMode - не работает)</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected bool InDesignMode
  {
    [DebuggerHidden] get
    {
      return this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime;
    }
  }

  private void b_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (!(this._editControl is Panel))
      return;
    this.BeginEdit(true);
    if (!(this.EditingControl is IPopupFormEditingControl editingControl))
      return;
    editingControl.ShowForm();
  }

  private void BeginEditControl()
  {
    this.EndEditControl();
    if (this.CurrentRow != null && this.CurrentRow.Index == this.NewRowIndex)
    {
      DataGridViewCellStyle dataGridViewCellStyle = this.CurrentRow.Index > 0 ? this.Rows[this.CurrentRow.Index - 1].Cells[this.NameDataGridViewColumn.Index].Style : this.NameDataGridViewColumn.DefaultCellStyle;
      this.CurrentRow.Cells[this.NameDataGridViewColumn.Index].Style = new DataGridViewCellStyle(dataGridViewCellStyle)
      {
        Font = new Font(dataGridViewCellStyle.Font ?? this.Font, FontStyle.Regular)
      };
    }
    Control parent;
    try
    {
      parent = this.Parent;
    }
    catch (SecurityException ex)
    {
      return;
    }
    this.EndEditControl();
    if (this.ReadOnly || this.CurrentCell == null || this.CurrentCell.RowIndex == this.NewRowIndex || this.Rows[this.CurrentCell.RowIndex].DataBoundItem is Task dataBoundItem && dataBoundItem.EditingLocked)
      return;
    DataGridViewColumn owningColumn1 = this.CurrentCell.OwningColumn;
    if (owningColumn1.ReadOnly)
      return;
    bool flag1 = owningColumn1 is DataGridViewButtonTextBoxColumn;
    bool flag2 = owningColumn1 is DataGridViewUpDownColumn;
    if (flag1 | flag2)
    {
      if (dataBoundItem != null)
      {
        bool flag3 = dataBoundItem.HasSubTasks;
        if (dataBoundItem is Intermech.Project.Project project && project.ManualPlanning && this._dateColumns.Contains(owningColumn1))
          flag3 = false;
        if (flag3)
          return;
      }
      Rectangle displayRectangle = this.GetCellDisplayRectangle(this.CurrentCell.ColumnIndex, this.CurrentCell.RowIndex, false);
      if (this.GetCellDisplayRectangle(this.CurrentCell.ColumnIndex, this.CurrentCell.RowIndex, true) == displayRectangle)
      {
        int width = displayRectangle.Width;
        DataGridViewColumn owningColumn2 = this.CurrentCell.OwningColumn;
        int right = owningColumn2 != null ? owningColumn2.DefaultCellStyle.Padding.Right : 0;
        if (width >= right)
        {
          Panel panel = new Panel();
          panel.Size = new Size(17, displayRectangle.Height - 1);
          panel.Location = new Point(displayRectangle.Left + displayRectangle.Width - panel.Width, displayRectangle.Top);
          if (flag1)
          {
            Button button = new Button();
            button.Name = "b";
            panel.Controls.Add((Control) button);
            button.FlatStyle = FlatStyle.Popup;
            button.Size = panel.Size;
            button.Location = Point.Empty;
            button.Text = "…";
            button.UseCompatibleTextRendering = true;
            button.Click += new EventHandler(this.b_Click);
          }
          if (flag2)
          {
            NumericUpDown numericUpDown = new NumericUpDown();
            numericUpDown.BorderStyle = BorderStyle.None;
            panel.Controls.Add((Control) numericUpDown);
            numericUpDown.Bounds = new Rectangle(0, (panel.Height - 16 /*0x10*/) / 2, 16 /*0x10*/, 16 /*0x10*/);
            numericUpDown.UpDownAlign = LeftRightAlignment.Left;
            numericUpDown.Tag = (object) this.CurrentCell;
            numericUpDown.Maximum = Decimal.MaxValue;
            double num = 0.0;
            if (owningColumn1 == this._durationDataGridViewColumn)
              num = dataBoundItem.Duration;
            else if (owningColumn1 == this._workDataGridViewColumn)
              num = dataBoundItem.Work;
            numericUpDown.Value = (Decimal) num;
            numericUpDown.ValueChanged += new EventHandler(this.Updown_ValueChanged);
          }
          if (flag1 | flag2 && this.CurrentCell.Style != null && (this.CurrentCell.Style.Alignment == DataGridViewContentAlignment.MiddleRight || this.CurrentCell.Style.Alignment == DataGridViewContentAlignment.NotSet))
          {
            this.CurrentCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            panel.VisibleChanged += new EventHandler(ProjectDataGridView.Panel_VisibleChanged);
            panel.Tag = (object) this.CurrentCell;
          }
          this._editControl = (Control) panel;
        }
      }
    }
    if (this._editControl == null)
      return;
    parent?.Controls.Add(this._editControl);
    this._editControl.BringToFront();
  }

  private void Updown_ValueChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (!(sender is NumericUpDown numericUpDown))
      return;
    double num = (double) numericUpDown.Value;
    if (!(numericUpDown.Tag is DataGridViewCell tag) || !(this.Rows[tag.RowIndex].DataBoundItem is Task dataBoundItem))
      return;
    DataGridViewColumn owningColumn = tag.OwningColumn;
    if (owningColumn == this._durationDataGridViewColumn)
    {
      dataBoundItem.Duration = num;
    }
    else
    {
      if (owningColumn != this._workDataGridViewColumn)
        return;
      dataBoundItem.Work = num;
    }
  }

  private static void Panel_VisibleChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (!(sender is Control control) || control.Visible || !(control.Tag is DataGridViewCell tag))
      return;
    tag.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
  }

  private void EndEditControl()
  {
    if (this._editControl != null)
    {
      this._editControl.Hide();
      if (this.Parent != null)
        this.Parent.Controls.Remove(this._editControl);
      this._editControl.Dispose();
      this._editControl = (Control) null;
    }
    this._forcedEdit = false;
  }

  [NotNull]
  private ColumnLayoutInformation[] GetColumnsLayoutInformation()
  {
    ColumnLayoutInformation[] layoutInformation = new ColumnLayoutInformation[base.Columns.Count];
    List<DataGridViewColumn> list = this.Columns.Cast<DataGridViewColumn>().ToList<DataGridViewColumn>();
    list.Sort((Comparison<DataGridViewColumn>) ((a, b) => a.DisplayIndex - b.DisplayIndex));
    int index = 0;
    foreach (DataGridViewColumn dataGridViewColumn in list)
    {
      layoutInformation[index] = new ColumnLayoutInformation(dataGridViewColumn.Name != string.Empty ? dataGridViewColumn.Name : dataGridViewColumn.DataPropertyName ?? string.Empty, dataGridViewColumn.HeaderText, dataGridViewColumn.DisplayIndex, dataGridViewColumn.Visible, dataGridViewColumn.Width);
      ++index;
    }
    return layoutInformation;
  }

  private void InitSpecialPropertiesEventHandlers()
  {
    if (this.Project == null)
      return;
    foreach (Task task in (System.Collections.ObjectModel.Collection<Task>) this.Project.Tasks)
    {
      task.PropertyChanged -= new PropertyChangedEventHandler(this.Task_PropertyChanged);
      task.PropertyChanged += new PropertyChangedEventHandler(this.Task_PropertyChanged);
    }
  }

  [NotNull]
  [Editor]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public new DataGridViewColumnCollection Columns => base.Columns;

  public bool IsExpanded([NotNull] Task task) => !task.HasSubTasks || !task.Minimized;

  internal bool IsMouseOverPlusMinus([NotNull] DataGridViewCellEventArgs e)
  {
    if (e.RowIndex > -1)
    {
      DataGridViewCell cell = this.Rows[e.RowIndex].Cells[this.NameDataGridViewColumn.Index];
      Point location = this.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location;
      Point client = this.PointToClient(Cursor.Position);
      client.Offset(-location.X, -location.Y);
      int x1 = client.X;
      Padding padding = cell.Style.Padding;
      int left1 = padding.Left;
      if (x1 < left1)
      {
        this.EndEdit();
        int x2 = client.X;
        padding = cell.Style.Padding;
        int num = padding.Left - 10;
        if (x2 >= num)
        {
          int x3 = client.X;
          padding = cell.Style.Padding;
          int left2 = padding.Left;
          if (x3 < left2 && this.Rows[e.RowIndex].DataBoundItem is Task dataBoundItem && dataBoundItem.HasSubTasks)
            return true;
        }
      }
    }
    return false;
  }

  protected override void OnCellMouseUp(DataGridViewCellMouseEventArgs e)
  {
    base.OnCellMouseUp(e);
    Point location = this.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location;
    location.Offset(e.Location);
    DataGridView.HitTestInfo hitTestInfo = this.HitTest(location.X, location.Y);
    if (hitTestInfo.Type != DataGridViewHitTestType.Cell || hitTestInfo.ColumnIndex != this.NameDataGridViewColumn.Index)
      return;
    DataGridViewCell cell = this.Rows[e.RowIndex].Cells[this.NameDataGridViewColumn.Index];
    int x1 = e.X;
    Padding padding = cell.Style.Padding;
    int left1 = padding.Left;
    if (x1 >= left1)
      return;
    this.EndEdit();
    int x2 = e.X;
    padding = cell.Style.Padding;
    int num = padding.Left - 10;
    if (x2 < num)
      return;
    int x3 = e.X;
    padding = cell.Style.Padding;
    int left2 = padding.Left;
    if (x3 >= left2 || !(this.Rows[e.RowIndex].DataBoundItem is Task dataBoundItem) || !dataBoundItem.HasSubTasks)
      return;
    this.SetExpanded(dataBoundItem, !this.IsExpanded(dataBoundItem));
  }

  [CanBeNull]
  public Task GetTask([NotNull] DataGridViewRow row)
  {
    try
    {
      return row.DataBoundItem is Task dataBoundItem ? dataBoundItem : (Task) null;
    }
    catch
    {
      return (Task) null;
    }
  }

  public void PaintCellOnGraphics([NotNull] DataGridViewCellPaintingEventArgs e)
  {
  }

  protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
  {
    if (!this.InDesignMode)
    {
      if (e.RowIndex == -1)
      {
        if (e.ColumnIndex == this.ImagesColumn.Index)
        {
          if (!this._skipPaintCellBackground)
            e.PaintBackground(e.ClipBounds, true);
          Point location = e.CellBounds.Location;
          Bitmap infoImage = Images.InfoImage;
          ref Point local = ref location;
          Rectangle cellBounds = e.CellBounds;
          int dx = cellBounds.Width / 2 - infoImage.Width / 2;
          cellBounds = e.CellBounds;
          int dy = cellBounds.Height / 2 - infoImage.Height / 2;
          local.Offset(dx, dy);
          e.Graphics.DrawImage((Image) infoImage, location);
          e.Handled = true;
          return;
        }
      }
      else
      {
        Task task = this.GetTask(this.Rows[e.RowIndex]);
        if (task != null)
        {
          if (e.ColumnIndex == -1)
          {
            if (!this._skipPaintCellBackground)
              e.PaintBackground(e.ClipBounds, true);
            e.Graphics.DrawString(task.IndexString, this.Font, Brushes.Black, (RectangleF) e.CellBounds, new StringFormat()
            {
              Alignment = StringAlignment.Center
            });
            e.Handled = true;
            return;
          }
          if (e.ColumnIndex == this.ImagesColumn.Index)
          {
            int num1 = 2;
            if (!this._skipPaintCellBackground)
              e.PaintBackground(e.ClipBounds, (e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected);
            DataGridViewCell cell = this.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (!(cell.Tag is ProjectDataGridView.ImageInfos imageInfos))
            {
              imageInfos = new ProjectDataGridView.ImageInfos();
              cell.Tag = (object) imageInfos;
              if (task != null)
              {
                Image statusImage = Images.GetStatusImage(task.Status, true);
                if (statusImage != null)
                  imageInfos.Add(statusImage, $"{Localization.GetString("TaskParamStatus")}: {SimpleFuncs.GetEnumDescription((Enum) task.Status)}");
                if (task is Intermech.Project.Project)
                  imageInfos.Add(Images.ProjectImage, Localization.GetString("Project"));
                if (task.PlanningConflict)
                  imageInfos.Add(Images.ExclamationImage, Localization.GetString("PlanningConflict"));
                if (task.ConstraintDate != DateTime.MinValue)
                  imageInfos.Add((Image) Images.ConstraintImage, string.Format(Localization.GetString("TaskHasConstraint"), (object) SimpleFuncs.GetEnumDescription((Enum) task.ConstraintType), (object) task.ConstraintDate));
                if (task.Notes != string.Empty)
                  imageInfos.Add((Image) Images.NotesImage, $"{Localization.GetString("TaskParamNotes")}: '{task.Notes.Truncate(200)}'");
                if (task is Intermech.Project.Project project && project.SyncPending)
                  imageInfos.Add(Images.SyncPendingImage, Localization.GetString("SyncPending"));
              }
              string empty = string.Empty;
              foreach (ProjectDataGridView.ImageInfo imageInfo in (List<ProjectDataGridView.ImageInfo>) imageInfos)
              {
                if (empty != string.Empty)
                  empty += "\r\n";
                empty += StringFuncs.WordWrap(imageInfo._Text, 30);
              }
              cell.ToolTipText = empty;
            }
            int num2 = 0;
            int num3 = 0;
            Point location = e.CellBounds.Location;
            location.Offset(num1, num1);
            foreach (ProjectDataGridView.ImageInfo imageInfo in (List<ProjectDataGridView.ImageInfo>) imageInfos)
            {
              Image image = imageInfo._Image;
              if (this.Multiline)
              {
                int num4 = location.X + image.Width;
                Rectangle cellBounds = e.CellBounds;
                int right = cellBounds.Right;
                if (num4 > right)
                {
                  int num5 = num2 + num3 + image.Height;
                  cellBounds = e.CellBounds;
                  int bottom = cellBounds.Bottom;
                  if (num5 < bottom)
                  {
                    ref Point local = ref location;
                    cellBounds = e.CellBounds;
                    int x = cellBounds.Location.X;
                    local.X = x;
                    num2 += num3;
                    num3 = 0;
                    location.Y = num2 + num1;
                  }
                }
              }
              imageInfo._Bounds = new Rectangle(location, image.Size);
              e.Graphics.DrawImage(image, location);
              if (image.Height > num3)
                num3 = image.Height;
              location.X += image.Width + num1;
            }
            e.Handled = true;
            return;
          }
          if (task.Grayed && e.CellStyle != null)
            e.CellStyle.ForeColor = SystemColors.GrayText;
        }
      }
    }
    base.OnCellPainting(e);
  }

  protected override void OnRowPostPaint([NotNull] DataGridViewRowPostPaintEventArgs e)
  {
    if (this.InDesignMode)
      return;
    Task task = e.RowIndex < 0 || e.RowIndex >= this.Rows.Count ? (Task) null : this.GetTask(this.Rows[e.RowIndex]);
    if (task == null || !task.HasSubTasks)
      return;
    Rectangle displayRectangle1 = this.GetCellDisplayRectangle(this.NameDataGridViewColumn.Index, e.RowIndex, true);
    int width = this.NameDataGridViewColumn.Width;
    Rectangle displayRectangle2 = this.GetCellDisplayRectangle(this.NameDataGridViewColumn.Index, e.RowIndex, false);
    int right = this.ClientRectangle.Right;
    if (this.VerticalScrollBar.Visible)
      right -= this.VerticalScrollBar.Width;
    if (displayRectangle2.Right > right)
      width -= displayRectangle2.Right - right;
    int num1 = width - displayRectangle1.Width;
    e.Graphics.SetClip(displayRectangle1);
    Icon icon = this.IsExpanded(task) ? Icons.Minus : Icons.Plus;
    int num2 = Math.Max(0, displayRectangle1.Y + displayRectangle1.Height / 2 - icon.Height / 2);
    e.Graphics.DrawIcon(icon, displayRectangle1.X + task.RealIndentLevel * 10 - num1, num2 - 3);
    e.Graphics.ResetClip();
  }

  protected override void OnCellBeginEdit(DataGridViewCellCancelEventArgs e)
  {
    base.OnCellBeginEdit(e);
    DataGridViewCell currentCell = this.CurrentCell;
    if (this.GetCellDisplayRectangle(currentCell.ColumnIndex, currentCell.RowIndex, false) != this.GetCellDisplayRectangle(currentCell.ColumnIndex, currentCell.RowIndex, true))
    {
      if (!currentCell.OwningColumn.Frozen)
      {
        try
        {
          this.FirstDisplayedScrollingColumnIndex = currentCell.ColumnIndex;
        }
        catch (InvalidOperationException ex)
        {
        }
      }
    }
    if (e.RowIndex == this.NewRowIndex)
      this.FirstDisplayedScrollingRowIndex = e.RowIndex;
    if (!(this.Rows[e.RowIndex].DataBoundItem is Task dataBoundItem))
      return;
    bool flag1 = this.ReadOnly || dataBoundItem.EditingLocked;
    DataGridViewColumn column = this.Columns[e.ColumnIndex];
    bool flag2 = !flag1 && dataBoundItem is Intermech.Project.Project project && project.ManualPlanning && this._dateColumns.Contains(column);
    if (!flag1 && dataBoundItem.HasSubTasks && column != this.NameDataGridViewColumn && column != this.NotesDataGridViewColumn && column != this._dependenciesDataGridViewColumn && !flag2)
      flag1 = true;
    if (!flag1)
      return;
    e.Cancel = true;
  }

  protected override void OnCurrentCellChanged([NotNull] EventArgs e)
  {
    if (this.Disposing || this.IsDisposed)
      return;
    base.OnCurrentCellChanged(e);
    this.BeginEditControl();
  }

  protected override void OnDataError(
    bool displayErrorDialogIfNoHandler,
    [NotNull] DataGridViewDataErrorEventArgs e)
  {
    base.OnDataError(displayErrorDialogIfNoHandler, e);
    e.ThrowException = e.Exception is ISimpleMessageException;
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  private ContextMenuBarItem HeaderMenu
  {
    get
    {
      if (this._headerMenu == null)
      {
        this._headerMenu = new ContextMenuBarItem();
        MenuButtonItem menuButtonItem1 = new MenuButtonItem(Resources.AddColumn, new EventHandler(this.addColumn_Click));
        menuButtonItem1.BeginGroup = true;
        this._headerMenu.Items.Add((ToolbarItemBase) menuButtonItem1);
        this._headerMenu.Items.Add((ToolbarItemBase) new MenuButtonItem(Resources.DeleteColumn, new EventHandler(this.deleteColumn_Click)));
        MenuButtonItem menuButtonItem2 = new MenuButtonItem(Resources.ResetColumns, new EventHandler(this.resetColumns_Click));
        menuButtonItem2.BeginGroup = true;
        this._headerMenu.Items.Add((ToolbarItemBase) menuButtonItem2);
      }
      return this._headerMenu;
    }
  }

  private void deleteColumn_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this._lastHitTest == null)
      return;
    DataGridViewColumn column = this.Columns[this._lastHitTest.ColumnIndex];
    if (column == null)
      return;
    if (column.Name != string.Empty)
      column.Visible = false;
    else
      this.Columns.Remove(column);
  }

  /// <summary>Ключ совпадает с ColumnInfo.Name для быстрого поиска</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  private Dictionary<string, ColumnInfo> PossibleAdditionalColumns
  {
    get
    {
      if (this._possibleAdditionalColumns == null)
      {
        HashSet<int> intSet = new HashSet<int>();
        intSet.Add((int) (IpsMetadataEntityBase<int>) Intermech.Metadata.Attributes.Modified);
        intSet.Add((int) (IpsMetadataEntityBase<int>) Intermech.Project.Attributes.Flags);
        this._possibleAdditionalColumns = new Dictionary<string, ColumnInfo>();
        ColumnInfo columnInfo1 = new ColumnInfo("FactStart", Localization.GetString("TaskParamFactStart"), FieldTypes.ftDateTime);
        this._possibleAdditionalColumns.Add(columnInfo1.Name, columnInfo1);
        ColumnInfo columnInfo2 = new ColumnInfo("FactFinish", Localization.GetString("TaskParamFactFinish"), FieldTypes.ftDateTime);
        this._possibleAdditionalColumns.Add(columnInfo2.Name, columnInfo2);
        ColumnInfo columnInfo3 = new ColumnInfo("ChiefString", Localization.GetString("TaskParamChiefString"), FieldTypes.ftString);
        this._possibleAdditionalColumns.Add(columnInfo3.Name, columnInfo3);
        List<IMSAttribute4ObjectType> attribute4ObjectTypeList = MetaDataHelperService.Instance.GetAttribute4ObjectTypeList((int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.Task);
        HashSet<FieldTypes> fieldTypesSet = new HashSet<FieldTypes>((IEnumerable<FieldTypes>) new FieldTypes[5]
        {
          FieldTypes.ftDateTime,
          FieldTypes.ftDouble,
          FieldTypes.ftInteger,
          FieldTypes.ftMemo,
          FieldTypes.ftString
        });
        foreach (IMSAttribute4ObjectType attribute4ObjectType in attribute4ObjectTypeList)
        {
          if (!intSet.Contains(attribute4ObjectType.AttributeID) && attribute4ObjectType.Required == RequiredModes.AutoRequired && fieldTypesSet.Contains(attribute4ObjectType.FieldType))
          {
            string attributeTypeName = MetaDataHelperService.Instance.GetAttributeTypeName(attribute4ObjectType.AttributeID);
            string str = "." + (object) attribute4ObjectType.AttributeID;
            this._possibleAdditionalColumns.Add(str, new ColumnInfo(str, attributeTypeName ?? string.Empty, attribute4ObjectType.FieldType));
          }
        }
      }
      return this._possibleAdditionalColumns;
    }
  }

  [NotNull]
  private static DataGridViewColumn CreateCustomColumn([NotNull] ColumnInfo columnInfo, [CanBeNull] string text)
  {
    DataGridViewColumn customColumn = columnInfo.Type != FieldTypes.ftDateTime ? (DataGridViewColumn) new EnhDataGridViewTextBoxColumn() : (DataGridViewColumn) new DataGridViewDateTimeTextBoxColumn();
    customColumn.Width = 100;
    customColumn.HeaderText = text != string.Empty ? text ?? string.Empty : columnInfo.Text;
    customColumn.DataPropertyName = columnInfo.Name;
    return customColumn;
  }

  private void addColumn_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    using (AddColumnForm addColumnForm = new AddColumnForm())
    {
      HashSet<string> colTexts = new HashSet<string>();
      foreach (DataGridViewColumn column in (BaseCollection) this.Columns)
      {
        if (column != null)
        {
          if (!column.Visible && column.Name != string.Empty)
            addColumnForm.Columns.Add(new ColumnInfo(column.Name, column.HeaderText, FieldTypes.ftUnknown));
          colTexts.Add(column.HeaderText);
        }
      }
      addColumnForm.Columns.AddRange(this.PossibleAdditionalColumns.Values.Where<ColumnInfo>((Func<ColumnInfo, bool>) (ci => !colTexts.Contains(ci.Text))));
      if (addColumnForm.ShowDialog() != DialogResult.OK)
        return;
      ColumnInfo selectedColumnInfo = addColumnForm.SelectedColumnInfo;
      if (selectedColumnInfo == null || selectedColumnInfo.Name == null || this._lastHitTest == null)
        return;
      DataGridViewColumn dataGridViewColumn = (DataGridViewColumn) null;
      if (selectedColumnInfo.Type == FieldTypes.ftUnknown)
        dataGridViewColumn = this.Columns[selectedColumnInfo.Name];
      int num = this._lastHitTest.ColumnIndex;
      if (num < this.Columns.Count)
        num = this.Columns[num].DisplayIndex;
      if (dataGridViewColumn == null)
      {
        DataGridViewColumn customColumn = ProjectDataGridView.CreateCustomColumn(selectedColumnInfo, addColumnForm.ColumnText);
        this.Columns.Insert(num, customColumn);
        customColumn.DisplayIndex = num;
      }
      else
      {
        dataGridViewColumn.DisplayIndex = num;
        dataGridViewColumn.Visible = true;
      }
    }
  }

  private void RefreshVisibility([NotNull] DataGridViewRow row)
  {
    if (!(row.DataBoundItem is Task dataBoundItem))
      return;
    bool isHidden = dataBoundItem.IsHidden;
    if (isHidden && dataBoundItem.HiddenByFilter && !this._hiddenByFilter.Contains(row))
      this._hiddenByFilter.Add(row);
    if (isHidden != row.Visible)
      return;
    if (this.CurrentRow != null && this.CurrentRow == row)
      this.CurrentCell = (DataGridViewCell) null;
    row.Visible = !isHidden;
  }

  public void RefreshVisibility()
  {
    this._hiddenByFilter.Clear();
    for (int index = 0; index < this.Rows.Count; ++index)
      this.RefreshVisibility(this.Rows[index]);
    this.BeginEditControl();
  }

  protected override void OnRowsAdded([NotNull] DataGridViewRowsAddedEventArgs e)
  {
    base.OnRowsAdded(e);
    int num = e.RowIndex + e.RowCount;
    for (int rowIndex = e.RowIndex; rowIndex < num; ++rowIndex)
    {
      DataGridViewRow row = this.Rows[rowIndex];
      if (row.DataBoundItem is Task dataBoundItem)
      {
        this.RefreshHasSubTasks(row, dataBoundItem);
        this.RefreshIndentLevel(row, dataBoundItem.RealIndentLevel);
      }
    }
    if (e.RowIndex <= 0 || e.RowIndex != this.NewRowIndex || !(this.Rows[e.RowIndex - 1].DataBoundItem is Task) || !this.IsCurrentCellInEditMode)
      return;
    this.BeginEdit(false);
  }

  protected override void OnSizeChanged([NotNull] EventArgs e)
  {
    base.OnSizeChanged(e);
    this.BeginEditControl();
  }

  protected override void OnScroll([NotNull] ScrollEventArgs e)
  {
    base.OnScroll(e);
    this.EndEditControl();
    if (this.CurrentRow != null)
      this.Invalidate(this.GetRowDisplayRectangle(this.CurrentRow.Index, true));
    this.BeginEditControl();
  }

  private void RefreshHasSubTasks([NotNull] DataGridViewRow row, [NotNull] Task task)
  {
    foreach (DataGridViewColumn column in (BaseCollection) base.Columns)
    {
      DataGridViewCellStyle style = row.Cells[column.Name].Style;
      Font font = style.Font ?? this.Font;
      FontStyle fontStyle = task.HasSubTasks ? FontStyle.Bold : FontStyle.Regular;
      if (font.Style != fontStyle)
        style.Font = fontStyle == FontStyle.Bold ? style.Font : (Font) null;
      if (task.ErrorString != string.Empty)
      {
        row.Cells[column.Name].ToolTipText = task.ErrorString;
        style.ForeColor = Color.Red;
      }
    }
    this.InvalidateRow(row.Index);
  }

  private void RefreshIndentLevel([NotNull] DataGridViewRow r, int indentLevel)
  {
    int num = this.NameDataGridViewColumn.DefaultCellStyle.Padding.Left + 10 * indentLevel;
    DataGridViewCellStyle style = r.Cells[this.NameDataGridViewColumn.Index].Style;
    if (style.Padding.Left == num)
      return;
    DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle(style);
    DataGridViewCellStyle gridViewCellStyle2 = gridViewCellStyle1;
    int left = num;
    int top = style.Padding.Top;
    Padding padding1 = style.Padding;
    int right = padding1.Right;
    padding1 = style.Padding;
    int bottom = padding1.Bottom;
    Padding padding2 = new Padding(left, top, right, bottom);
    gridViewCellStyle2.Padding = padding2;
    r.Cells[this.NameDataGridViewColumn.Index].Style = gridViewCellStyle1;
    if (!(r.DataBoundItem is Task dataBoundItem))
      return;
    dataBoundItem.RowHeight = 0;
  }

  internal void RefreshSpecialProperties()
  {
    foreach (Task task in (System.Collections.ObjectModel.Collection<Task>) this.Project.Tasks)
    {
      this.Task_PropertyChanged((object) task, new PropertyChangedEventArgs("IndentLevel"));
      this.Task_PropertyChanged((object) task, new PropertyChangedEventArgs("HasSubTasks"));
    }
  }

  private void resetColumns_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (MessageBox.Show(Resources.ResetColumnsConfirm, string.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
      return;
    this.EndEditControl();
    for (int index = this.Columns.Count - 1; index >= 0; --index)
    {
      if (this.Columns[index].Name == string.Empty)
        this.Columns.RemoveAt(index);
    }
    this.SetColumnsLayoutInformation(this._initColumnsLayoutInformation);
    this.BeginEditControl();
  }

  public void SaveLayout([NotNull] string name)
  {
    if (this.DesignMode)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBConfigurations configurations = sessionKeeper.Session.Configurations;
      using (MemoryStream s = new MemoryStream())
      {
        this.SaveLayout((Stream) s);
        BlobInformation config_info = new BlobInformation(s.Length, s.Length, DateTime.Now, name, ArcMethods.NotPacked, "b");
        configurations.WriteConfigData(config_info, s.ToArray());
      }
    }
  }

  public void SaveLayout([NotNull] Stream s)
  {
    ColumnLayoutInformation[] layoutInformation = this.GetColumnsLayoutInformation();
    new BinaryFormatter().Serialize(s, (object) layoutInformation);
  }

  public bool LoadLayout([NotNull] string name)
  {
    if (this.DesignMode)
      return false;
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        byte[] config_file;
        sessionKeeper.Session.Configurations.LoadConfigData(name, out BlobInformation _, out config_file);
        if (config_file.Length != 0)
        {
          using (MemoryStream s = new MemoryStream(config_file))
          {
            s.Position = 0L;
            this.LoadLayout((Stream) s);
          }
          return true;
        }
      }
    }
    catch
    {
    }
    return false;
  }

  public void LoadLayout([NotNull] Stream s)
  {
    this.SetColumnsLayoutInformation((ColumnLayoutInformation[]) new BinaryFormatter().Deserialize(s));
  }

  private void SetColumnsLayoutInformation([NotNull] ColumnLayoutInformation[] cc)
  {
    foreach (ColumnLayoutInformation layoutInformation in cc)
    {
      string columnName = layoutInformation.ColumnName;
      DataGridViewColumn dataGridViewColumn = this.Columns[columnName];
      ColumnInfo columnInfo;
      if (dataGridViewColumn == null && layoutInformation.Visible && this.PossibleAdditionalColumns.TryGetValue(columnName, out columnInfo))
      {
        dataGridViewColumn = ProjectDataGridView.CreateCustomColumn(columnInfo, layoutInformation.Text);
        this.Columns.Add(dataGridViewColumn);
      }
      if (dataGridViewColumn != null)
      {
        dataGridViewColumn.Visible = layoutInformation.Visible;
        try
        {
          dataGridViewColumn.DisplayIndex = layoutInformation.DisplayIndex;
          if (layoutInformation.Width != 0)
            dataGridViewColumn.Width = layoutInformation.Width;
        }
        catch (InvalidOperationException ex)
        {
        }
        catch (ArgumentOutOfRangeException ex)
        {
        }
      }
    }
  }

  public void EnsureExpanded(Task task)
  {
    List<Task> taskList = new List<Task>();
    for (; task != null; task = task.Parent)
      taskList.Insert(0, task);
    foreach (Task task1 in taskList)
    {
      if (!this.IsExpanded(task1))
        this.SetExpanded(task1, true);
    }
  }

  public void SetExpanded([CanBeNull] Task task, bool expanded)
  {
    if (task == null || !task.HasSubTasks || expanded == this.IsExpanded(task) || !task.LoadSubTasks())
      return;
    foreach (DataGridViewRow row in (IEnumerable) this.Rows)
    {
      if (row.DataBoundItem as Task == task)
      {
        DataGridViewCell cell = row.Cells[this.NameDataGridViewColumn.Index];
        for (int index = row.Index + 1; index < (this.NewRowIndex >= 0 ? this.NewRowIndex : this.Rows.Count); ++index)
        {
          try
          {
            if (this.Rows[index].DataBoundItem is Task dataBoundItem)
            {
              if (dataBoundItem.IndentLevel == task.IndentLevel + 1)
              {
                bool visible = expanded;
                if (visible && dataBoundItem.HiddenByFilter)
                  visible = false;
                if (!visible && this.CurrentCell != null && this.CurrentCell.RowIndex == index)
                  this.CurrentCell = cell;
                this.Rows[index].Visible = visible;
                if (dataBoundItem.HasSubTasks)
                  this.SetVisible(dataBoundItem, visible);
              }
              if (dataBoundItem.IndentLevel <= task.IndentLevel)
                break;
            }
            else
              break;
          }
          catch (IndexOutOfRangeException ex)
          {
            break;
          }
        }
        this.InvalidateCell(cell);
        task.Minimized = !expanded;
        EventHandler<TaskExpandedEventArgs> taskExpanded = this.TaskExpanded;
        if (taskExpanded == null)
          break;
        taskExpanded((object) this, new TaskExpandedEventArgs(task, expanded));
        break;
      }
    }
  }

  private void SetVisible([CanBeNull] Task task, bool visible, bool recursive = true)
  {
    if (task == null)
      return;
    if (task.HiddenByFilter)
      visible = false;
    foreach (DataGridViewRow row in (IEnumerable) this.Rows)
    {
      if (row.DataBoundItem as Task == task)
      {
        if (visible && !row.Visible)
          row.Visible = true;
        if (!task.HasSubTasks || visible && !this.IsExpanded(task))
          break;
        bool flag = this.IsCurrentCellInEditMode;
        for (int index1 = row.Index + 1; index1 < (this.NewRowIndex >= 0 ? this.NewRowIndex : this.Rows.Count); ++index1)
        {
          try
          {
            if (this.Rows[index1].DataBoundItem is Task dataBoundItem)
            {
              if (dataBoundItem.IndentLevel == task.IndentLevel + 1)
              {
                bool visible1 = visible;
                if (visible1 && dataBoundItem.HiddenByFilter)
                  visible1 = false;
                if (!visible1 && this.CurrentCell != null && this.CurrentCell.RowIndex == index1)
                {
                  int index2 = index1 + 1;
                  while (!this.Rows[index2].Visible)
                    ++index2;
                  this.CurrentCell = this.Rows[index2].Cells[this.NameDataGridViewColumn.Index];
                  flag = false;
                }
                this.Rows[index1].Visible = visible1;
                if (recursive)
                  this.SetVisible(dataBoundItem, visible1);
              }
              if (dataBoundItem.IndentLevel <= task.IndentLevel)
                break;
            }
            else
              break;
          }
          catch (IndexOutOfRangeException ex)
          {
            break;
          }
        }
        if (!flag)
          break;
        this.BeginEdit(false);
        break;
      }
    }
  }

  private void Task_PropertyChanged([CanBeNull] object sender, [NotNull] PropertyChangedEventArgs e)
  {
    if (this.DuringUpdate)
      return;
    string propertyName = e.PropertyName;
    if (!this._trackedProps.Contains(propertyName) || !(sender is Task task))
      return;
    foreach (DataGridViewRow row in (IEnumerable) this.Rows)
    {
      if (this.GetTask(row) == task)
      {
        switch (propertyName)
        {
          case "HasSubTasks":
            if (task.HasNotLoadedSubTasks && !task.Minimized)
              task.Minimized = true;
            this.RefreshHasSubTasks(row, task);
            return;
          case "IndentLevel":
            this.RefreshIndentLevel(row, task.RealIndentLevel);
            return;
          default:
            DataGridViewCell cell = row.Cells[this.ImagesColumn.Index];
            if (cell == null)
              return;
            cell.Tag = (object) null;
            this.InvalidateCell(cell);
            return;
        }
      }
    }
  }

  private void Tasks_ListChanged([CanBeNull] object sender, [NotNull] ListChangedEventArgs e)
  {
    if (e.ListChangedType == ListChangedType.ItemAdded)
    {
      Task task = this.Project.Tasks[e.NewIndex];
      this.Task_PropertyChanged((object) task, new PropertyChangedEventArgs("IndentLevel"));
      this.Task_PropertyChanged((object) task, new PropertyChangedEventArgs("HasSubTasks"));
      task.PropertyChanged += new PropertyChangedEventHandler(this.Task_PropertyChanged);
    }
    else
    {
      if (e.ListChangedType != ListChangedType.ItemDeleted && e.ListChangedType != ListChangedType.ItemMoved && e.ListChangedType != ListChangedType.Reset)
        return;
      if (e.ListChangedType == ListChangedType.Reset)
        this.InitSpecialPropertiesEventHandlers();
      if (e.ListChangedType == ListChangedType.ItemDeleted)
      {
        if (e.NewIndex <= 0)
        {
          foreach (object task in (System.Collections.ObjectModel.Collection<Task>) this.Project.Tasks)
            this.Task_PropertyChanged(task, new PropertyChangedEventArgs("IndentLevel"));
          foreach (Task subTask in (IEnumerable<Task>) this.Project.SubTasks)
            this.SetVisible(subTask, true, false);
          EventHandler<TaskExpandedEventArgs> taskExpanded = this.TaskExpanded;
          if (taskExpanded == null)
            return;
          taskExpanded((object) this, (TaskExpandedEventArgs) null);
        }
        else
        {
          if (this.Project == null || e.NewIndex > this.Project.Tasks.Count)
            return;
          Task task = this.Project.Tasks[e.NewIndex - 1];
          this.Task_PropertyChanged((object) task, new PropertyChangedEventArgs("HasSubTasks"));
          foreach (object allSubTask in (IEnumerable<Task>) task.AllSubTasks)
            this.Task_PropertyChanged(allSubTask, new PropertyChangedEventArgs("IndentLevel"));
        }
      }
      else
        this.RefreshSpecialProperties();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  internal bool DuringUpdate
  {
    get => this._duringUpdate;
    set
    {
      if (value == this.DuringUpdate)
        return;
      this._duringUpdate = value;
      if (this.Project == null)
        return;
      this.Project.RaisePropertyChangedEvents = !this.DuringUpdate;
      this.Project.Tasks.RaiseListChangedEvents = !this.DuringUpdate;
      if (this.DuringUpdate)
        return;
      this.Project.ResetBindings();
      this.Project.Tasks.ResetBindings();
    }
  }

  public int LockUpdateTasksEvents()
  {
    if (this._lockUpdateTasksCounter == 0 && this.Project?.Tasks != null)
    {
      this.Project.Tasks.ListChanged -= new ListChangedEventHandler(this.Tasks_ListChanged);
      this.DataSource = (object) null;
    }
    return ++this._lockUpdateTasksCounter;
  }

  public int UnlockUpdateTasksEvents()
  {
    if (this._lockUpdateTasksCounter <= 0)
      throw new Exception();
    --this._lockUpdateTasksCounter;
    if (this._lockUpdateTasksCounter == 0 && this.Project?.Tasks != null)
    {
      this.Project.Tasks.ListChanged += new ListChangedEventHandler(this.Tasks_ListChanged);
      this.DataSource = (object) this.Project?.Tasks;
    }
    return this._lockUpdateTasksCounter;
  }

  [DefaultValue(null)]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [CanBeNull]
  public ClientProject Project
  {
    get => this._project;
    set
    {
      if (value == this.Project)
        return;
      if (this.Project != null)
        this.Project.Tasks.ListChanged -= new ListChangedEventHandler(this.Tasks_ListChanged);
      this._project = value;
      this.DataSource = (object) this.Project?.Tasks;
      this.InitSpecialPropertiesEventHandlers();
      this.RefreshSpecialProperties();
      if (this.Project != null)
        this.Project.Tasks.ListChanged += new ListChangedEventHandler(this.Tasks_ListChanged);
      ClientProject project1 = this._project;
      this.AllowUserToAddRows = project1 != null && project1.EditingMode.HasComposition();
      ClientProject project2 = this._project;
      this.AllowUserToDeleteRows = project2 != null && project2.EditingMode.HasComposition();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool Multiline
  {
    get => this._multiline;
    set
    {
      if (this._multiline == value)
        return;
      this._multiline = value;
      this.NameDataGridViewColumn.DefaultCellStyle.WrapMode = value ? DataGridViewTriState.True : DataGridViewTriState.False;
    }
  }

  private int CalcRowHeight([NotNull] DataGridViewRow row, bool force = false)
  {
    int h = 0;
    if (row.Cells[this.NameDataGridViewColumn.Index] is EnhDataGridViewTextBoxCell cell)
    {
      Task task = (Task) null;
      if (this.Project != null && this.Project.Tasks.Count > row.Index)
        task = row.DataBoundItem as Task;
      if (task != null)
      {
        h = task.RowHeight;
        if (!force && h == 0)
          force = cell.Value != null;
        if (force)
        {
          h = cell.GetPreferredHeight();
          task.RowHeight = h;
        }
        if (this._project != null && this._GanttChart != null)
          this._project.ExtendRowHeightForCaptions(ref h, this._GanttChart.FullTaskHeight);
      }
    }
    return h;
  }

  protected override void OnRowHeightInfoNeeded([NotNull] DataGridViewRowHeightInfoNeededEventArgs e)
  {
    base.OnRowHeightInfoNeeded(e);
    if (this.InDesignMode || !this.Multiline)
      return;
    int num = this.CalcRowHeight(this.Rows[e.RowIndex]);
    if (num == 0)
      return;
    e.Height = num;
  }

  protected override void OnRowHeightChanged(DataGridViewRowEventArgs e)
  {
    base.OnRowHeightChanged(e);
    if (this.InDesignMode || !(e.Row.DataBoundItem is Task dataBoundItem))
      return;
    dataBoundItem.RowHeight = e.Row.Height;
  }

  protected override void OnCellValueChanged(DataGridViewCellEventArgs e)
  {
    base.OnCellValueChanged(e);
    if (this.InDesignMode || !this.Multiline || e.ColumnIndex != this.NameDataGridViewColumn.Index || !(this.Rows[e.RowIndex].DataBoundItem is Task dataBoundItem))
      return;
    dataBoundItem.RowHeight = 0;
    this.InvalidateWithGantt();
  }

  [DefaultValue(false)]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool UseColumnHeaderContextMenu { get; set; }

  public override bool BeginEdit(bool selectAll)
  {
    if (!this.InDesignMode && selectAll)
      this._forcedEdit = true;
    return base.BeginEdit(false);
  }

  protected override void OnEnter([NotNull] EventArgs e) => base.OnEnter(e);

  protected override void OnEditingControlShowing([NotNull] DataGridViewEditingControlShowingEventArgs e)
  {
    if (!this.InDesignMode)
    {
      if (this._lastMousePos.X != 0)
      {
        this._lastMousePos = this.PointToScreen(this._lastMousePos);
        this._lastMousePos = e.Control.PointToClient(this._lastMousePos);
        if (e.Control is TextBox control)
        {
          int indexFromPosition = control.GetCharIndexFromPosition(this._lastMousePos);
          control.SelectionStart = indexFromPosition;
          this._lastMousePos = Point.Empty;
        }
      }
      if (this.CurrentCell != null && this.CurrentCell.OwningColumn == this._assignmentsDataGridViewColumn && e.Control is EnhDataGridViewTextBoxEditingControl control1)
      {
        control1.AutoCompleteSource = AutoCompleteSource.CustomSource;
        AutoCompleteStringCollection stringCollection = new AutoCompleteStringCollection();
        if (this.Project != null)
        {
          foreach (Resource allResource in this.Project.AllResources)
            stringCollection.Add(allResource.Name);
        }
        control1.AutoCompleteCustomSource = stringCollection;
        control1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
      }
    }
    base.OnEditingControlShowing(e);
  }

  protected override void OnMouseWheel([NotNull] MouseEventArgs e)
  {
    if (!this.Bounds.Contains(e.Location))
      return;
    base.OnMouseWheel(e);
  }

  protected override void OnMouseMove([NotNull] MouseEventArgs e)
  {
    base.OnMouseMove(e);
    if (this.InDesignMode || this.ReadOnly || (e.Button & MouseButtons.Left) != MouseButtons.Left || !(this.Cursor != Cursors.SizeNS) || this._rowIndexFromMouseDown == -1 || !(this._dragBoxFromMouseDown != Rectangle.Empty) || this._dragBoxFromMouseDown.Contains(e.X, e.Y))
      return;
    if (this._rowIndexFromMouseDown >= this.RowCount)
    {
      this._rowIndexFromMouseDown = -1;
    }
    else
    {
      DataGridViewRow row = this.Rows[this._rowIndexFromMouseDown];
      if (row.IsNewRow)
        return;
      if (this._project != null)
      {
        Task task = this._project.Tasks[this._rowIndexFromMouseDown];
        if (task.ReadOnly)
          return;
        this.EnsureExpanded(task);
      }
      int num = (int) this.DoDragDrop((object) row, DragDropEffects.Move);
    }
  }

  protected override void OnMouseDown([NotNull] MouseEventArgs e)
  {
    if (this.InDesignMode)
      return;
    this._IsMouseDown = true;
    DataGridView.HitTestInfo hitTestInfo = this.HitTest(e.X, e.Y);
    this._rowIndexFromMouseDown = hitTestInfo.RowIndex;
    DataGridViewCell dataGridViewCell1 = (DataGridViewCell) null;
    if (this._rowIndexFromMouseDown != -1 && hitTestInfo.ColumnIndex != -1)
      dataGridViewCell1 = this.Rows[this._rowIndexFromMouseDown].Cells[hitTestInfo.ColumnIndex];
    DataGridViewCell[] array = (DataGridViewCell[]) null;
    if (dataGridViewCell1 == this.CurrentCell)
    {
      array = new DataGridViewCell[this.SelectedCells.Count];
      this.SelectedCells.CopyTo(array, 0);
    }
    try
    {
      base.OnMouseDown(e);
      if (e.Button == MouseButtons.Left)
        this._lastMousePos = new Point(e.X, e.Y);
      this._lastHitTest = this.HitTest(e.X, e.Y);
      if (this._lastHitTest.Type == DataGridViewHitTestType.ColumnHeader)
      {
        if (e.Button == MouseButtons.Right)
        {
          if (this.UseColumnHeaderContextMenu)
          {
            if (this.IsCurrentCellInEditMode)
            {
              if (!this.EndEdit())
                goto label_27;
            }
            this.Focus();
            this.HeaderMenu.Show(Intermech.Client.Services.PopupHost, (Control) this, new Point(e.X, e.Y));
          }
        }
        else if (e.Button == MouseButtons.Left)
        {
          if (this.Cursor == Cursors.Default)
          {
            this.ClearSelection();
            for (int rowIndex = 0; rowIndex < this.NewRowIndex; ++rowIndex)
              this.SetSelectedCellCore(this._lastHitTest.ColumnIndex, rowIndex, true);
            if (this.NewRowIndex > 0)
              this.SetCurrentCellAddressCore(this._lastHitTest.ColumnIndex, this.NewRowIndex - 1, true, true, false);
          }
        }
      }
    }
    finally
    {
      if (array != null)
      {
        foreach (DataGridViewCell dataGridViewCell2 in array)
          this.SetSelectedCellCore(dataGridViewCell2.ColumnIndex, dataGridViewCell2.RowIndex, true);
      }
    }
label_27:
    if (this._rowIndexFromMouseDown != -1)
    {
      Size dragSize = SystemInformation.DragSize;
      this._dragBoxFromMouseDown = new Rectangle(new Point(e.X - dragSize.Width / 2, e.Y - dragSize.Height / 2), dragSize);
    }
    else
      this._dragBoxFromMouseDown = Rectangle.Empty;
  }

  protected override void OnDragOver(DragEventArgs e)
  {
    base.OnDragOver(e);
    if (this.InDesignMode || this._project == null || e.AllowedEffect != DragDropEffects.Move)
      return;
    Task task1 = this._project.Tasks[this._rowIndexFromMouseDown];
    Point client = this.PointToClient(new Point(e.X, e.Y));
    int rowIndex = this.HitTest(client.X, client.Y).RowIndex;
    Task task2 = rowIndex < 0 || rowIndex >= this._project.Tasks.Count ? (Task) null : this._project.Tasks[rowIndex];
    if (task2 == null || task2.ReadOnly || task2.IsChildOf(task1) || task1.IsChildOf(task2))
      e.Effect = DragDropEffects.None;
    else
      e.Effect = DragDropEffects.Move;
  }

  protected override void OnDragDrop(DragEventArgs e)
  {
    base.OnDragDrop(e);
    if (this.InDesignMode || this._project == null)
      return;
    Point client = this.PointToClient(new Point(e.X, e.Y));
    this._rowIndexOfItemUnderMouseToDrop = this.HitTest(client.X, client.Y).RowIndex;
    if (e.Effect != DragDropEffects.Move)
      return;
    this._project.Tasks[this._rowIndexFromMouseDown].Index = this._rowIndexOfItemUnderMouseToDrop;
    foreach (DataGridViewRow row in (IEnumerable) this.Rows)
    {
      DataGridViewCell cell = row.Cells[this.ImagesColumn.Index];
      cell.Tag = (object) null;
      this.InvalidateCell(cell);
    }
    this.Invalidate();
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public new bool ReadOnly => this._project == null || this._project.EditingMode.ReadOnly();

  public void SetVisibleColumns([NotNull] List<string> propertyNames)
  {
    foreach (DataGridViewColumn column in (BaseCollection) this.Columns)
    {
      if (column != null)
        column.Visible = propertyNames.Contains(column.DataPropertyName);
    }
    this._initColumnsLayoutInformation = this.GetColumnsLayoutInformation();
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int VisibleColumnsWidth
  {
    get
    {
      return this.Columns.Cast<DataGridViewColumn>().Where<DataGridViewColumn>((Func<DataGridViewColumn, bool>) (column => column.Visible)).Sum<DataGridViewColumn>((Func<DataGridViewColumn, int>) (column => column.Width));
    }
  }

  public int GetColumnsWidth([NotNull] List<string> propertyNames)
  {
    return this.Columns.Cast<DataGridViewColumn>().Where<DataGridViewColumn>((Func<DataGridViewColumn, bool>) (column => propertyNames.Contains(column.DataPropertyName))).Sum<DataGridViewColumn>((Func<DataGridViewColumn, int>) (column => column.Width));
  }

  protected override void OnMouseUp([NotNull] MouseEventArgs e)
  {
    this._IsMouseDown = false;
    base.OnMouseUp(e);
  }

  protected override void OnCurrentCellDirtyStateChanged([NotNull] EventArgs e)
  {
    if (this.CurrentRow != null && this.IsCurrentCellDirty && this.NewRowIndex == this.CurrentCellAddress.Y && !this.InDesignMode)
    {
      int index = this.CurrentRow.Index;
      base.OnCurrentCellDirtyStateChanged(e);
      if (!(this.Rows[index].DataBoundItem is Task dataBoundItem))
        return;
      dataBoundItem.Uncommitted = false;
    }
    else
      base.OnCurrentCellDirtyStateChanged(e);
  }

  protected override bool SetCurrentCellAddressCore(
    int columnIndex,
    int rowIndex,
    bool setAnchorCellAddress,
    bool validateCurrentCell,
    bool throughMouseClick)
  {
    if (!this.InDesignMode && rowIndex >= 0 && rowIndex < this.Rows.Count)
    {
      DataGridViewRow row = this.Rows[rowIndex];
      if (this._hiddenByFilter.Contains(row))
      {
        if (row.Visible)
          row.Visible = false;
        columnIndex = -1;
        rowIndex = -1;
      }
    }
    return base.SetCurrentCellAddressCore(columnIndex, rowIndex, setAnchorCellAddress, validateCurrentCell, throughMouseClick);
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  private IEnumerable<DataGridViewRow> ExtSelectedRows
  {
    get
    {
      DataGridViewSelectedRowCollection selectedRows = this.SelectedRows;
      DataGridViewSelectedCellCollection selectedCells = this.SelectedCells;
      return selectedRows.Cast<DataGridViewRow>().Concat<DataGridViewRow>(selectedCells.Cast<DataGridViewCell>().Select<DataGridViewCell, DataGridViewRow>((Func<DataGridViewCell, DataGridViewRow>) (cell => cell.OwningRow))).Distinct<DataGridViewRow>();
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IReadOnlyList<Task> SelectedTasks
  {
    get
    {
      Task[] array = this.ExtSelectedRows.SelectNotNull<DataGridViewRow, Task>((Func<DataGridViewRow, Task>) (row => row.DataBoundItem as Task)).Distinct<Task>().ToArray<Task>();
      if (array.Length > 1 && this.Project != null)
        Array.Sort<int, Task>(((IEnumerable<Task>) array).Select<Task, int>((Func<Task, int>) (task => this.Project.Tasks.IndexOf(task))).ToArray<int>(array.Length), array);
      return (IReadOnlyList<Task>) array;
    }
    set
    {
      bool flag = true;
      foreach (DataGridViewRow row in (IEnumerable) this.Rows)
      {
        row.Selected = value.Contains<Task>(row.DataBoundItem as Task);
        if (flag && row.Selected)
        {
          if (row.Visible)
            this.CurrentCell = row.Cells[this.NameDataGridViewColumn.Index];
          flag = false;
          if (value.Count == 1)
            break;
        }
      }
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  internal IReadOnlyList<Task> SelectedWithSubTasks
  {
    get
    {
      return (IReadOnlyList<Task>) new List<Task>(this.SelectedTasks.SelectMany<Task, Task>((Func<Task, IEnumerable<Task>>) (task => (IEnumerable<Task>) task.AllTasks)).Distinct<Task>());
    }
  }

  protected override bool ProcessDataGridViewKey(KeyEventArgs e)
  {
    if (!this.InDesignMode)
    {
      if (this.IsCurrentCellInEditMode && this._forcedEdit && (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Home || e.KeyCode == Keys.End))
        return false;
      if (e.KeyCode == Keys.F2)
        this._forcedEdit = true;
    }
    try
    {
      return base.ProcessDataGridViewKey(e);
    }
    catch (ArgumentOutOfRangeException ex)
    {
    }
    return false;
  }

  protected override void OnRowStateChanged(int rowIndex, DataGridViewRowStateChangedEventArgs e)
  {
    base.OnRowStateChanged(rowIndex, e);
    if (this.InDesignMode || !e.StateChanged.HasFlag((Enum) DataGridViewElementStates.Visible) && !e.StateChanged.HasFlag((Enum) DataGridViewElementStates.Displayed) || this.Project == null)
      return;
    this.Project.ClearVisibleTaskIndexes();
  }

  internal void RecalcRowHeights()
  {
    this.SuspendLayout();
    try
    {
      foreach (Task task in this.Rows.Cast<DataGridViewRow>().Select<DataGridViewRow, object>((Func<DataGridViewRow, object>) (row => row.DataBoundItem)).OfType<Task>())
        task.RowHeight = 0;
      foreach (DataGridViewRow row in (IEnumerable) this.Rows)
      {
        if (row != this.CurrentRow && row.Visible)
        {
          if (row.IsNewRow)
            break;
          row.Visible = false;
          row.Visible = true;
          break;
        }
      }
    }
    finally
    {
      this.ResumeLayout();
    }
  }

  protected override void OnColumnWidthChanged(DataGridViewColumnEventArgs e)
  {
    if (!this.InDesignMode && this.Multiline && e.Column == this.NameDataGridViewColumn)
      this.RecalcRowHeights();
    try
    {
      base.OnColumnWidthChanged(e);
    }
    catch (ArgumentOutOfRangeException ex)
    {
    }
    this.BeginEditControl();
    if (this.InDesignMode)
      return;
    this.InvalidateWithGantt();
  }

  internal void CurrentTaskNameChanged()
  {
    if (this.CurrentRow == null)
      return;
    this.OnCellValueChanged(new DataGridViewCellEventArgs(this.NameDataGridViewColumn.Index, this.CurrentRow.Index));
  }

  private void InvalidateWithGantt()
  {
    this.Invalidate();
    if (this.InvalidateGanttChart == null)
      return;
    this.InvalidateGanttChart((object) this, (EventArgs) null);
  }

  internal event EventHandler InvalidateGanttChart;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public string ErrorMessage { get; internal set; }

  protected override void OnCellFormatting(DataGridViewCellFormattingEventArgs e)
  {
    base.OnCellFormatting(e);
    if (this.InDesignMode)
      return;
    DataGridViewRow row = this.Rows[e.RowIndex];
    DataGridViewColumn column = this.Columns[e.ColumnIndex];
    if (row.DataBoundItem == null)
      return;
    string dataPropertyName = column.DataPropertyName;
    if ((dataPropertyName != null ? (dataPropertyName.StartsWith(".") ? 1 : 0) : 0) == 0)
      return;
    int int32 = Convert.ToInt32(column.DataPropertyName.Remove(0, 1));
    if (int32 == 0)
      return;
    Task task = this.GetTask(row);
    if (task == null)
      return;
    e.Value = task.GetAttributeValue(int32);
  }

  protected override void OnCellParsing(DataGridViewCellParsingEventArgs e)
  {
    if (!this.InDesignMode)
    {
      DataGridViewRow row = this.Rows[e.RowIndex];
      DataGridViewColumn column = this.Columns[e.ColumnIndex];
      if (row.DataBoundItem != null)
      {
        string dataPropertyName = column.DataPropertyName;
        if ((dataPropertyName != null ? (dataPropertyName.StartsWith(".") ? 1 : 0) : 0) != 0)
        {
          int int32 = Convert.ToInt32(column.DataPropertyName.Remove(0, 1));
          if (int32 != 0)
            this.GetTask(row)?.SetAttributeValue(int32, e.Value);
        }
      }
    }
    base.OnCellParsing(e);
  }

  public class ImageInfo
  {
    public readonly Image _Image;
    public Rectangle _Bounds;
    public readonly string _Text;

    public ImageInfo([NotNull] Image image, [NotNull] string text)
    {
      this._Image = image;
      this._Text = text;
    }
  }

  public class ImageInfos : List<ProjectDataGridView.ImageInfo>
  {
    public void Add([NotNull] Image image, [NotNull] string text)
    {
      this.Add(new ProjectDataGridView.ImageInfo(image, text));
    }
  }
}
