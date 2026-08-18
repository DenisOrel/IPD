// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.GanttChart
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Controls;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Project.Controls.Properties;
using Intermech.Project.Evaluator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

/// <summary>Компонент для отображения диаграммы Гантта</summary>
public class GanttChart : 
  BaseControl,
  IComponent,
  IDisposable,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IContextAware,
  IControlServiceContainer,
  IAdvancedServiceContainer,
  IServiceContainer,
  System.IServiceProvider,
  IClientProjectContext
{
  private bool _allowDrag;
  private bool _allowDragStart = true;
  [NotNull]
  private Brush _criticalTaskBrush = (Brush) new HatchBrush(HatchStyle.Percent50, Color.Red, Color.Transparent);
  [NotNull]
  private Pen _criticalTaskPen = Pens.Red;
  private DateTime _currentDate;
  private float _dayWidth = 20f;
  private Dependency _dragDropDependency;
  internal DragDropOperation _DragDropOperation;
  private Point _dragDropOrigin = Point.Empty;
  private double _dragDropOriginalCompletedWork;
  private DateTime _dragDropOriginalTaskFinish;
  private DateTime _dragDropOriginalTaskStart;
  private double _dragDropOriginalTaskDuration;
  private ConstraintType _dragDropOriginalConstraintType;
  [NotNull]
  private static Dictionary<Task, double> _finishBeforeDictionary = new Dictionary<Task, double>();
  private int _headerHeight = 40;
  private bool _highlightCriticalTasks;
  [NotNull]
  private Dictionary<RectangleF, DragDropOperation> _lastPrintedRectangles = new Dictionary<RectangleF, DragDropOperation>();
  [NotNull]
  private Pen _metConstraintPen = Pens.Green;
  [NotNull]
  private Brush _milestoneTaskBrush = Brushes.Black;
  [NotNull]
  private Pen _milestoneTaskPen = Pens.Transparent;
  [NotNull]
  private Brush _nonWorkingDayBrush = (Brush) new SolidBrush(SystemColors.Control);
  [NotNull]
  private Pen _notMetConstraintPen = new Pen(Color.Red, 3f);
  private NumericScaleType _numericScaleType;
  [NotNull]
  private Brush _parentTaskBrush = Brushes.Black;
  [NotNull]
  private Pen _parentTaskPen = Pens.Black;
  [NotNull]
  private Brush _percentCompletedBrush = Brushes.Black;
  [NotNull]
  private Brush _percentNotCompletedBrush = (Brush) new HatchBrush(HatchStyle.Percent75, Color.Red, Color.Transparent);
  [NotNull]
  private Pen _periodLinePen = new Pen((Brush) new HatchBrush(HatchStyle.Percent50, SystemColors.ControlDark, SystemColors.Window));
  [NotNull]
  private Dictionary<RectangleF, DragDropOperation> _printedRectangles = new Dictionary<RectangleF, DragDropOperation>();
  private ClientProject _project;
  private float _rectangleHeightPercent = 0.5f;
  private int _taskRectangleHeight = 10;
  private float _rectangleRoundnessPercent;
  private int _rowHeight = 22;
  private ScaleType _scaleType = ScaleType.Weeks;
  [NotNull]
  private Brush _standardTaskBrush = IMProject.DefaultTaskBrush;
  [NotNull]
  private Pen _standardTaskPen = IMProject.DefaultTaskPen;
  [NotNull]
  private static Dictionary<Task, double> _startDelays = new Dictionary<Task, double>();
  [NotNull]
  private Dictionary<Task, Brush> _taskBrushes = new Dictionary<Task, Brush>();
  [NotNull]
  private Dictionary<Task, Pen> _taskPens = new Dictionary<Task, Pen>();
  [NotNull]
  private Pen _todayLinePen;
  private bool _useNumericScaleValues;
  private int _visibleTaskCount;
  private int _visibleTaskIndex;
  private int _displayedRowCount;
  private int _firstDisplayedScrollingRowIndex;
  private DateTime _initialDate;
  private int _scrollPos;
  private int _scrollPosDec;
  private int _scrollPosInc;
  [NotNull]
  private readonly List<GanttChart.ToolTipRectangle> _toolTipRectangles = new List<GanttChart.ToolTipRectangle>(100);
  private const int InvisibleTaskIndex = -9999999;
  private ToolTip _tooltip;
  [CanBeNull]
  private string _hint = string.Empty;
  private Point _mousePos;
  [CanBeNull]
  private System.Windows.Forms.Timer _hintTimer;
  public int _SpacerDaysWidth = 30;
  internal int _CurrentXOffset;
  [NotNull]
  protected ScalePoints _BottomScalePoints = new ScalePoints();
  private bool _showConstraintMarkers;
  protected Brush _FactTermsBrush = (Brush) new SolidBrush(Color.Gray);
  private const int ArrowMethod = 1;
  private Pen _minimizedSummaryLinePen;
  private bool _showGrid;
  private float _barWidth = -1f;
  private HScrollBar _hScrollBar;

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected HScrollBar HorizontalScrollBar
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._hScrollBar.CheckInitializedIn<HScrollBar>((object) this);
    }
  }

  [CanBeNull]
  public ProjectDataGridView GridView { get; protected internal set; }

  public event EventHandler DragStarted;

  public event PaintEventHandler GanttChartPaint;

  public event EventHandler TaskDoubleClicked;

  public GanttChart()
  {
    this._currentDate = GanttChart.AlignToFirstDayOfWeek(DateTime.Today);
    this.BackColor = SystemColors.Window;
    this.SetStyle(ControlStyles.UserPaint, true);
    this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
    this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
    this.SetStyle(ControlStyles.ResizeRedraw, true);
    this._todayLinePen = new Pen(Color.Green);
    this._todayLinePen.DashStyle = DashStyle.Dash;
    if (!this.InDesignMode)
    {
      try
      {
        this.AllowDrop = true;
      }
      catch (SecurityException ex)
      {
      }
    }
    this.InitializeComponent();
    this.AddService<GanttChart>(this);
    this.AddService<IClientProjectContext>((IClientProjectContext) this);
    if (this.Project != null)
    {
      this.AddService<ClientProject>(this.Project);
      this.AddService<Intermech.Project.Project>((Intermech.Project.Project) this.Project);
    }
    this._initialDate = this.CurrentDate;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this._taskBrushes != null)
      {
        foreach (Brush brush in this._taskBrushes.Values.NotNull<Brush>().Distinct<Brush>())
          brush.Dispose();
      }
      if (this._taskPens != null)
      {
        foreach (Pen pen in this._taskPens.Values.NotNull<Pen>().Distinct<Pen>())
          pen.Dispose();
      }
      this.RemoveService<Intermech.Project.Project>();
      this.RemoveService<ClientProject>();
      this.RemoveService<IClientProjectContext>();
      this.RemoveService<GanttChart>();
    }
    base.Dispose(disposing);
  }

  private void hScrollBar_Scroll([CanBeNull] object sender, [NotNull] ScrollEventArgs e)
  {
    if (e.Type == ScrollEventType.SmallIncrement)
    {
      this._scrollPos += this.HorizontalScrollBar.SmallChange;
      if (e.OldValue == e.NewValue)
        this._scrollPosInc += this.HorizontalScrollBar.SmallChange;
      else if (this._scrollPosDec > 0)
      {
        this._scrollPosDec -= this.HorizontalScrollBar.SmallChange;
        e.NewValue = e.OldValue;
      }
    }
    else if (e.Type == ScrollEventType.SmallDecrement)
    {
      this._scrollPos -= this.HorizontalScrollBar.SmallChange;
      if (e.OldValue == e.NewValue)
        this._scrollPosDec += this.HorizontalScrollBar.SmallChange;
      else if (this._scrollPosInc > 0)
      {
        this._scrollPosInc -= this.HorizontalScrollBar.SmallChange;
        e.NewValue = e.OldValue;
      }
    }
    else if (e.Type != ScrollEventType.EndScroll)
    {
      this._scrollPos = e.NewValue;
      this._scrollPosInc = 0;
      this._scrollPosDec = 0;
    }
    this.CurrentDate = this._initialDate.AddDays((double) this._scrollPos);
  }

  public static DateTime AlignToFirstDayOfWeek(DateTime today)
  {
    DayOfWeek firstDayOfWeek = Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
    while (today.DayOfWeek != firstDayOfWeek)
      today = today.AddDays(-1.0);
    return today;
  }

  private static int GetIndex([NotNull] ClientProject project, [NotNull] Task task, int visibleTaskIndex)
  {
    int num1;
    project.VisibleTaskIndexes.TryGetValue(project.Tasks[visibleTaskIndex], out num1);
    int num2;
    return project.VisibleTaskIndexes.TryGetValue(task, out num2) ? num2 - num1 : -9999999;
  }

  [CanBeNull]
  private static GraphicsPath GetRoundedPath([NotNull] PointF[] points, float radius)
  {
    if (points.Length == 0)
      return (GraphicsPath) null;
    PointF point1 = points[0];
    PointF point2 = points[points.Length - 1];
    GraphicsPath roundedPath = new GraphicsPath();
    roundedPath.AddLine(point1, point1);
    PointF pointF = point1;
    for (int index = 1; index < points.Length - 1; ++index)
    {
      PointF point3 = points[index];
      PointF point4 = points[index + 1];
      float val1 = Math.Max(Math.Abs(point3.X - pointF.X), Math.Abs(point3.X - point4.X));
      float val2 = Math.Max(Math.Abs(point3.Y - pointF.Y), Math.Abs(point3.Y - point4.Y));
      float num = Math.Min(radius, Math.Min(val1, val2)) * 2f;
      if ((double) point3.Y == (double) pointF.Y && (double) point3.X == (double) point4.X)
      {
        if ((double) point3.X >= (double) pointF.X)
        {
          if ((double) point3.Y <= (double) point4.Y)
            roundedPath.AddArc(point3.X - num, point3.Y, num, num, 270f, 90f);
          else
            roundedPath.AddArc(point3.X - num, point3.Y - num, num, num, 90f, -90f);
        }
        else if ((double) point3.Y <= (double) point4.Y)
          roundedPath.AddArc(point3.X, point3.Y, num, num, 270f, -90f);
        else
          roundedPath.AddArc(point3.X, point3.Y - num, num, num, 90f, 90f);
      }
      else if ((double) point3.X == (double) pointF.X && (double) point3.Y == (double) point4.Y)
      {
        if ((double) point3.X <= (double) point4.X)
        {
          if ((double) point3.Y >= (double) pointF.Y)
            roundedPath.AddArc(point3.X, point3.Y - num, num, num, 180f, -90f);
          else
            roundedPath.AddArc(point3.X, point3.Y, num, num, 180f, 90f);
        }
        else if ((double) point3.Y >= (double) pointF.Y)
          roundedPath.AddArc(point3.X - num, point3.Y - num, num, num, 0.0f, 90f);
        else
          roundedPath.AddArc(point3.X - num, point3.Y, num, num, 0.0f, -90f);
      }
      pointF = point3;
    }
    roundedPath.AddLine(point2, point2);
    return roundedPath;
  }

  [NotNull]
  private static GraphicsPath GetRoundedRectanglePath(
    float x,
    float y,
    float width,
    float height,
    float radius)
  {
    float num = Math.Min(radius * 2f, Math.Max(width, height));
    RectangleF rect = new RectangleF(x, y, num, num);
    GraphicsPath roundedRectanglePath = new GraphicsPath();
    roundedRectanglePath.AddArc(rect, 180f, 90f);
    rect.X = x + width - num;
    roundedRectanglePath.AddArc(rect, 270f, 90f);
    rect.Y = y + height - num;
    roundedRectanglePath.AddArc(rect, 0.0f, 90f);
    rect.X = x;
    roundedRectanglePath.AddArc(rect, 90f, 90f);
    roundedRectanglePath.CloseFigure();
    return roundedRectanglePath;
  }

  [NotNull]
  private static IEnumerable<Task> GetTasksPage([CanBeNull] Intermech.Project.Project project, int firstIndex, int visibleCount)
  {
    List<Task> tasksPage = new List<Task>();
    if (project != null)
    {
      TaskCollection tasks = project.Tasks;
      for (int index = firstIndex; index < tasks.Count && tasksPage.Count < visibleCount; ++index)
      {
        Task task = tasks[index];
        if (!task.IsHidden)
          tasksPage.Add(task);
      }
    }
    return (IEnumerable<Task>) tasksPage;
  }

  private static bool IsVisible([NotNull] Task t, DateTime cd, DateTime ed)
  {
    return !(t.Start > ed.AddDays(1.0)) || t.Finish >= cd.AddDays(-1.0);
  }

  protected override void OnDragOver(DragEventArgs e)
  {
    base.OnDragOver(e);
    if (!this.AllowDrag)
      return;
    e.Effect = DragDropEffects.None;
    if (this._DragDropOperation == null)
      return;
    Point p = new Point(e.X, e.Y);
    p = this.PointToClient(p);
    if ((this._dragDropOrigin.Y - this.HeaderHeight) / this.RowHeight != (p.Y - this.HeaderHeight) / this.RowHeight)
    {
      DragDropOperation dragDropOperation = this._DragDropOperation;
      int num;
      if (dragDropOperation == null)
      {
        num = 0;
      }
      else
      {
        bool? allowDragDrop = dragDropOperation.Task?.AllowDragDrop;
        bool flag = true;
        num = allowDragDrop.GetValueOrDefault() == flag & allowDragDrop.HasValue ? 1 : 0;
      }
      if (num != 0)
      {
        if (this._DragDropOperation.Task.PlanningType == PlanningType.FromStart)
          this._DragDropOperation.Task.Start = this._dragDropOriginalTaskStart;
        else
          this._DragDropOperation.Task.Finish = this._dragDropOriginalTaskFinish;
        this._DragDropOperation.Task.Duration = this._dragDropOriginalTaskDuration;
        this._DragDropOperation.Task.ConstraintType = this._dragDropOriginalConstraintType;
        if (!this._DragDropOperation.Task.Milestone)
          this._DragDropOperation.Task.CompletedWork = this._dragDropOriginalCompletedWork;
      }
      Task task1 = (Task) null;
      using (IEnumerator<DragDropOperation> enumerator = this._printedRectangles.Keys.Where<RectangleF>((Func<RectangleF, bool>) (ef => ef.Contains((PointF) p))).Select<RectangleF, DragDropOperation>((Func<RectangleF, DragDropOperation>) (ef => this._printedRectangles[ef])).Where<DragDropOperation>((Func<DragDropOperation, bool>) (operation => operation.Type == DragDropOperationType.Standard)).GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          DragDropOperation current = enumerator.Current;
          try
          {
            Task task2 = current.Task;
            if ((task2 != null ? (task2.ReadOnly ? 1 : 0) : 1) == 0)
            {
              task1 = current.Task;
              if (this._dragDropDependency != null)
                this._dragDropDependency.Task = task1;
              e.Effect = DragDropEffects.Move;
              Cursor.Current = Cursors.UpArrow;
              return;
            }
          }
          catch (ArgumentException ex)
          {
          }
        }
      }
      if (task1 == null && this._dragDropDependency != null)
        this._dragDropDependency.Task = (Task) null;
      Cursor.Current = Cursors.No;
    }
    else
    {
      if (this._dragDropDependency != null)
        this._dragDropDependency.Task = (Task) null;
      DragDropOperation dragDropOperation = this._DragDropOperation;
      int num1;
      if (dragDropOperation == null)
      {
        num1 = 1;
      }
      else
      {
        bool? allowDragDrop = dragDropOperation.Task?.AllowDragDrop;
        bool flag = true;
        num1 = !(allowDragDrop.GetValueOrDefault() == flag & allowDragDrop.HasValue) ? 1 : 0;
      }
      if (num1 != 0)
      {
        Cursor.Current = Cursors.No;
      }
      else
      {
        float num2 = (float) this._dragDropOrigin.X / this.DayWidth;
        float num3 = (float) p.X / this.DayWidth;
        switch (this._DragDropOperation.Type)
        {
          case DragDropOperationType.Standard:
            if (!this.AllowDragStart)
            {
              Cursor.Current = Cursors.No;
              break;
            }
            double num4 = Math.Round((double) num3 - (double) num2);
            if (num4 != 0.0)
            {
              if (this._DragDropOperation.Task.PlanningType == PlanningType.FromStart)
              {
                this._DragDropOperation.Task.DurationLock = "Start";
                this._DragDropOperation.Task.Start = this._dragDropOriginalTaskStart.AddDays(num4);
              }
              else
              {
                this._DragDropOperation.Task.DurationLock = "Finish";
                this._DragDropOperation.Task.Finish = this._dragDropOriginalTaskFinish.AddDays(num4);
              }
            }
            e.Effect = DragDropEffects.Move;
            Cursor.Current = Cursors.SizeAll;
            break;
          case DragDropOperationType.Duration:
            this._DragDropOperation.Task.Duration = this._DragDropOperation.Task.CalcDuration(this._dragDropOriginalTaskFinish.AddDays(Math.Round((double) num3 - (double) num2)));
            e.Effect = DragDropEffects.Move;
            Cursor.Current = Cursors.SizeWE;
            break;
          case DragDropOperationType.PercentCompleted:
            Task task = this._DragDropOperation.Task;
            task.CompletedWork = task.GetWorkTime(task.Start, this.CurrentDate.AddDays((double) (p.X - this._CurrentXOffset) / (double) this.DayWidth)).Work;
            e.Effect = DragDropEffects.Move;
            Cursor.Current = Cursors.SizeWE;
            break;
          default:
            Cursor.Current = Cursors.No;
            break;
        }
      }
    }
  }

  protected override void OnGiveFeedback(GiveFeedbackEventArgs e)
  {
    base.OnGiveFeedback(e);
    if (!this.AllowDrag)
      return;
    e.UseDefaultCursors = false;
  }

  protected override void OnMouseDown([NotNull] MouseEventArgs e)
  {
    base.OnMouseDown(e);
    if (e.Button != MouseButtons.Left)
      return;
    using (IEnumerator<RectangleF> enumerator = this._printedRectangles.Keys.Where<RectangleF>((Func<RectangleF, bool>) (ef => ef.Contains((float) e.X, (float) e.Y))).GetEnumerator())
    {
      if (!enumerator.MoveNext())
        return;
      RectangleF current = enumerator.Current;
      if (e.Clicks > 1 && this.TaskDoubleClicked != null)
      {
        this.TaskDoubleClicked((object) this, (EventArgs) e);
      }
      else
      {
        this._DragDropOperation = this._printedRectangles[current];
        this._dragDropOrigin = new Point(e.X, e.Y);
        if (this.DragStarted != null)
          this.DragStarted((object) this, EventArgs.Empty);
        if (!this.AllowDrag)
          return;
        DragDropOperation dragDropOperation = this._DragDropOperation;
        int num1;
        if (dragDropOperation == null)
        {
          num1 = 0;
        }
        else
        {
          bool? allowDragDrop = dragDropOperation.Task?.AllowDragDrop;
          bool flag = true;
          num1 = allowDragDrop.GetValueOrDefault() == flag & allowDragDrop.HasValue ? 1 : 0;
        }
        if (num1 != 0)
        {
          this._dragDropOriginalTaskStart = this._DragDropOperation.Task.Start;
          this._dragDropOriginalTaskFinish = this._DragDropOperation.Task.Finish;
          this._dragDropOriginalTaskDuration = this._DragDropOperation.Task.Duration;
          this._dragDropOriginalConstraintType = this._DragDropOperation.Task.ConstraintType;
          if (!this._DragDropOperation.Task.Milestone)
            this._dragDropOriginalCompletedWork = this._DragDropOperation.Task.CompletedWork;
        }
        if (this._DragDropOperation?.Task == null)
          return;
        this._dragDropDependency = new Dependency(this._DragDropOperation.Task);
        int num2 = (int) this.DoDragDrop((object) this._DragDropOperation, DragDropEffects.Move);
        this._DragDropOperation = (DragDropOperation) null;
        this._dragDropOrigin = Point.Empty;
        this._printedRectangles = this._lastPrintedRectangles;
        this.Invalidate();
      }
    }
  }

  private void InitTooltip()
  {
    if (this._tooltip != null)
      return;
    this._tooltip = new ToolTip();
    this._tooltip.AutoPopDelay = 1000;
  }

  private void CancelHint()
  {
    if (this._tooltip != null)
      this._tooltip.Hide((IWin32Window) this);
    if (this._hintTimer == null)
      return;
    this._hintTimer.Stop();
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [CanBeNull]
  protected string Hint
  {
    get => this._hint;
    set
    {
      if (!(this._hint != value))
        return;
      this._hint = value;
      if (!(this._hint == string.Empty))
        return;
      this.CancelHint();
    }
  }

  private void HintTimer_Tick([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this._hintTimer == null)
      return;
    this._hintTimer.Stop();
    this.InitTooltip();
    if (this._tooltip == null)
      return;
    ToolTip tooltip = this._tooltip;
    string hint = this.Hint;
    int x = this._mousePos.X;
    int y1 = this._mousePos.Y;
    Cursor cursor = this.Cursor;
    int num = ((object) cursor != null ? cursor.Size.Height : 0) / 2;
    int y2 = y1 + num;
    tooltip.Show(hint, (IWin32Window) this, x, y2);
  }

  protected override void OnMouseLeave([NotNull] EventArgs e)
  {
    base.OnMouseLeave(e);
    this.CancelHint();
  }

  protected override void OnMouseMove([NotNull] MouseEventArgs e)
  {
    base.OnMouseMove(e);
    DragDropOperation dragDropOperation = (DragDropOperation) null;
    if (this._printedRectangles != null)
    {
      this.Cursor = Cursors.Default;
      using (IEnumerator<RectangleF> enumerator = this._printedRectangles.Keys.Where<RectangleF>((Func<RectangleF, bool>) (ef => ef.Contains((float) e.X, (float) e.Y))).GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          dragDropOperation = this._printedRectangles[enumerator.Current];
          if (dragDropOperation != null)
          {
            switch (dragDropOperation.Type)
            {
              case DragDropOperationType.Standard:
                this.Cursor = Cursors.SizeAll;
                break;
              case DragDropOperationType.Duration:
              case DragDropOperationType.PercentCompleted:
                this.Cursor = Cursors.SizeWE;
                break;
            }
          }
        }
      }
    }
    this._mousePos = e.Location;
    string h = string.Empty;
    if (e.Y < this.HeaderHeight)
      h = this._BottomScalePoints.SeekDate((float) (this._mousePos.X - this._SpacerDaysWidth) / this.DayWidth);
    else if (dragDropOperation != null)
      h = dragDropOperation.Task?.Name ?? string.Empty;
    else if (this._toolTipRectangles.Count > 0)
      this._toolTipRectangles.InvokeForFirst<GanttChart.ToolTipRectangle>((Predicate<GanttChart.ToolTipRectangle>) (toolTipRectangle => toolTipRectangle._Rect.Contains((PointF) this._mousePos)), (Action<GanttChart.ToolTipRectangle>) (toolTipRectangle => h = toolTipRectangle._Text));
    this.Hint = h;
    if (string.IsNullOrWhiteSpace(this.Hint))
      return;
    if (this._hintTimer == null)
    {
      this._hintTimer = new System.Windows.Forms.Timer();
      this._hintTimer.Interval = 1000;
      this._hintTimer.Tick += new EventHandler(this.HintTimer_Tick);
    }
    this._hintTimer.Stop();
    this._hintTimer.Start();
  }

  protected override void OnPaint(PaintEventArgs pe)
  {
    base.OnPaint(pe);
    Graphics graphics = pe.Graphics;
    int days = (int) Math.Round((double) this.ClientRectangle.Width / (double) this.DayWidth) + 1;
    int visibleTaskCount = this.VisibleTaskCount;
    if (this.Project != null && this.Project.HasState(TaskState.Loading))
      visibleTaskCount = 0;
    this._lastPrintedRectangles = this.Draw(graphics, 0, 0, this.Project, this.VisibleTaskIndex, visibleTaskCount, this.CurrentDate, days, this.HeaderHeight, this.RowHeight, this.DayWidth, this.ClientSize.Height, this.Font, SystemBrushes.Control, SystemBrushes.ControlText, SystemPens.ControlDark, SystemPens.ControlLightLight, SystemColors.Control, SystemColors.Window, SystemColors.ControlDark, this.StandardTaskBrush, this.CriticalTaskBrush, this.ParentTaskBrush, this.MilestoneTaskBrush, this.PercentCompletedBrush, this.PercentNotCompletedBrush, this.StandardTaskPen, this.CriticalTaskPen, this.ParentTaskPen, this.MilestoneTaskPen, this.MetConstraintPen, this.NotMetConstraintPen, this.HighlightCriticalTasks, this.AllowDrag, this.ScaleType, this.UseNumericScaleValues, this.NumericScaleType, this.NonWorkingDayBrush, this.PeriodLinePen, this.TodayLinePen, this._taskPens, this._taskBrushes, this.RectangleRoundnessPercent, this.RectangleHeightPercent);
    if (this._DragDropOperation == null)
    {
      this._printedRectangles = this._lastPrintedRectangles;
    }
    else
    {
      foreach (RectangleF key in this._printedRectangles.Keys)
      {
        DragDropOperation printedRectangle = this._printedRectangles[key];
        if (printedRectangle?.Task != null && printedRectangle.Type == DragDropOperationType.Standard)
        {
          Dictionary<RectangleF, DragDropOperation> printedRectangles = this._lastPrintedRectangles;
          // ISSUE: explicit non-virtual call
          if ((printedRectangles != null ? (!__nonvirtual (printedRectangles.ContainsKey(key)) ? 1 : 0) : 1) != 0)
          {
            bool flag = !this.HighlightCriticalTasks || !printedRectangle.Task.IsCritical;
            Pen pen = (Pen) (!printedRectangle.Task.HasSubTasks ? (flag ? this.StandardTaskPen : this.CriticalTaskPen) : this.ParentTaskPen).Clone();
            pen.DashStyle = DashStyle.Dot;
            graphics.DrawLine(pen, key.X + 1f, (float) ((double) key.Y + (double) key.Height / 2.0 - 1.0), (float) ((double) key.X + (double) key.Width - 1.0), (float) ((double) key.Y + (double) key.Height / 2.0 - 1.0));
            graphics.DrawLine(pen, key.X + 1f, (float) ((double) key.Y + (double) key.Height / 2.0 + 1.0), (float) ((double) key.X + (double) key.Width - 1.0), (float) ((double) key.Y + (double) key.Height / 2.0 + 1.0));
          }
        }
      }
    }
    PaintEventHandler ganttChartPaint = this.GanttChartPaint;
    if (ganttChartPaint == null)
      return;
    ganttChartPaint((object) this, pe);
  }

  [Browsable(false)]
  protected virtual bool DrawNonWorkingTime => this._scaleType <= ScaleType.Weeks;

  [NotNull]
  public virtual Dictionary<RectangleF, DragDropOperation> Draw(
    Graphics g,
    int w,
    int py,
    ClientProject project,
    int visibleTaskIndex,
    int visibleTaskCount,
    DateTime currentDate,
    int days,
    int headerHeight,
    int rowHeight,
    float dayWidth,
    int height,
    Font font,
    Brush controlBrush,
    Brush controlTextBrush,
    Pen controlDarkPen,
    Pen controlLightLightPen,
    Color controlColor,
    Color windowColor,
    Color controlDarkColor,
    Brush standardTaskBrush,
    Brush criticalTaskBrush,
    Brush parentTaskBrush,
    Brush milestoneTaskBrush,
    Brush percentCompletedBrush,
    Brush percentNotCompletedBrush,
    Pen standardTaskPen,
    Pen criticalTaskPen,
    Pen parentTaskPen,
    Pen milestoneTaskPen,
    Pen metConstraintPen,
    Pen notMetConstraintPen,
    bool highlightCriticalTasks,
    bool allowDragDrop,
    ScaleType scaleType,
    bool useNumericScaleValues,
    NumericScaleType numericScaleType,
    Brush nonWorkingDayBrush,
    Pen periodLinePen,
    Pen todayLinePen,
    Dictionary<Task, Pen> taskPens,
    Dictionary<Task, Brush> taskBrushes,
    float rectangleRoundnessPercent,
    float rectangleHeightPercent)
  {
    return this.Draw(g, w, py, project, visibleTaskIndex, visibleTaskCount, currentDate, days, headerHeight, (GanttChart.GetRowTopYDelegate) null, dayWidth, height, font, controlBrush, controlTextBrush, controlDarkPen, controlLightLightPen, controlColor, windowColor, controlDarkColor, standardTaskBrush, criticalTaskBrush, parentTaskBrush, milestoneTaskBrush, percentCompletedBrush, percentNotCompletedBrush, standardTaskPen, criticalTaskPen, parentTaskPen, milestoneTaskPen, metConstraintPen, notMetConstraintPen, highlightCriticalTasks, allowDragDrop, scaleType, useNumericScaleValues, numericScaleType, nonWorkingDayBrush, periodLinePen, todayLinePen, taskPens, taskBrushes, rectangleRoundnessPercent, rectangleHeightPercent);
  }

  [NotNull]
  public virtual Dictionary<RectangleF, DragDropOperation> Draw(
    Graphics g,
    int w,
    int py,
    ClientProject project,
    int visibleTaskIndex,
    int visibleTaskCount,
    DateTime currentDate,
    int days,
    int headerHeight,
    GanttChart.GetRowTopYDelegate getRowTopY,
    float dayWidth,
    int height,
    Font font,
    Brush controlBrush,
    Brush controlTextBrush,
    Pen controlDarkPen,
    Pen controlLightLightPen,
    Color controlColor,
    Color windowColor,
    Color controlDarkColor,
    Brush standardTaskBrush,
    Brush criticalTaskBrush,
    Brush parentTaskBrush,
    Brush milestoneTaskBrush,
    Brush percentCompletedBrush,
    Brush percentNotCompletedBrush,
    Pen standardTaskPen,
    Pen criticalTaskPen,
    Pen parentTaskPen,
    Pen milestoneTaskPen,
    Pen metConstraintPen,
    Pen notMetConstraintPen,
    bool highlightCriticalTasks,
    bool allowDragDrop,
    ScaleType scaleType,
    bool useNumericScaleValues,
    NumericScaleType numericScaleType,
    Brush nonWorkingDayBrush,
    Pen periodLinePen,
    Pen todayLinePen,
    Dictionary<Task, Pen> taskPens,
    Dictionary<Task, Brush> taskBrushes,
    float rectangleRoundnessPercent,
    float rectangleHeightPercent)
  {
    GanttChart._startDelays = new Dictionary<Task, double>();
    GanttChart._finishBeforeDictionary = new Dictionary<Task, double>();
    int num1 = 24;
    DayOfWeek firstDayOfWeek = Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
    w += this._SpacerDaysWidth;
    int num2 = Convert.ToInt32((float) this._SpacerDaysWidth / this.DayWidth) + 1;
    for (int index = -num2; index <= days; ++index)
    {
      DateTime time = currentDate.AddDays((double) index);
      bool flag1 = project != null && (project.PlanningType == PlanningType.FromStart && time < project.Start.Date || project.PlanningType == PlanningType.FromEnd && time > project.Finish.Date);
      if (this.DrawNonWorkingTime && !flag1)
        flag1 = project != null && project.Schedule.IsNonWorkingTime(time);
      if (flag1)
        g.FillRectangle(nonWorkingDayBrush, (float) w + (float) index * dayWidth, (float) headerHeight, dayWidth, (float) (height - headerHeight));
      bool flag2 = false;
      if (!this.DrawNonWorkingTime)
      {
        switch (scaleType)
        {
          case ScaleType.Days:
            if (time.Date != DateTime.Today)
            {
              flag2 = true;
              break;
            }
            break;
          case ScaleType.Weeks:
            if (time.DayOfWeek == firstDayOfWeek)
            {
              flag2 = true;
              break;
            }
            break;
          case ScaleType.Months:
            if (time.Day == 1)
            {
              flag2 = true;
              break;
            }
            break;
          case ScaleType.Quarters:
            if (time.Day == 1 && time.Month % 3 == 1)
            {
              flag2 = true;
              break;
            }
            break;
          case ScaleType.Years:
            if (time.Day == 1 && time.Month == 1)
            {
              flag2 = true;
              break;
            }
            break;
        }
      }
      if (flag2)
        g.DrawLine(periodLinePen, (float) w + (float) index * dayWidth, (float) headerHeight, (float) w + (float) index * dayWidth, (float) height);
    }
    float totalDays1 = (float) DateTime.Now.Subtract(currentDate).TotalDays;
    if ((double) totalDays1 > 0.0 && (double) totalDays1 <= (double) days)
      g.DrawLine(todayLinePen, (float) w + totalDays1 * dayWidth, (float) headerHeight, (float) w + totalDays1 * dayWidth, (float) height);
    Dictionary<RectangleF, DragDropOperation> dictionary = project == null || this.DesignMode ? (Dictionary<RectangleF, DragDropOperation>) null : this.DrawTasks(g, w, project, visibleTaskIndex, visibleTaskCount, currentDate, days, headerHeight, getRowTopY, dayWidth, font, standardTaskBrush, criticalTaskBrush, parentTaskBrush, milestoneTaskBrush, percentCompletedBrush, percentNotCompletedBrush, standardTaskPen, criticalTaskPen, parentTaskPen, milestoneTaskPen, metConstraintPen, notMetConstraintPen, SystemBrushes.Window, SystemBrushes.WindowText, highlightCriticalTasks, allowDragDrop, windowColor, controlDarkColor, taskPens, taskBrushes, rectangleRoundnessPercent, rectangleHeightPercent);
    g.FillRectangle(controlBrush, (float) w - (float) this._SpacerDaysWidth, 0.0f, (float) (days + 1) * dayWidth, (float) (headerHeight - 1 + py));
    g.DrawRectangle(controlLightLightPen, (float) (w + 1) - (float) this._SpacerDaysWidth, 1f, (float) (days + 1) * dayWidth, (float) (headerHeight - 2 + py));
    g.DrawRectangle(controlDarkPen, (float) w - (float) this._SpacerDaysWidth, 0.0f, (float) (days + 1) * dayWidth, (float) (headerHeight - 1 + py));
    this._BottomScalePoints.Clear();
    for (int x1 = -2 * num2; x1 <= days; ++x1)
    {
      DateTime date1 = currentDate.AddDays((double) x1);
      DateTime dateTime;
      switch (scaleType)
      {
        case ScaleType.Days:
          g.DrawRectangle(controlLightLightPen, (float) ((double) w + (double) x1 * (double) dayWidth + 1.0), 1f, dayWidth, (float) (headerHeight / 2));
          g.DrawRectangle(controlDarkPen, (float) w + (float) x1 * dayWidth, 0.0f, dayWidth, (float) (headerHeight / 2));
          int num3 = num1 / 3;
          for (int index = 0; index < num3; ++index)
            this._BottomScalePoints.Add((float) x1 + (float) index / (float) num3, 1f / (float) num3, date1);
          num1 = num3 * 3;
          break;
        case ScaleType.Weeks:
          if (date1.DayOfWeek == firstDayOfWeek)
          {
            g.DrawRectangle(controlLightLightPen, (float) ((double) w + (double) x1 * (double) dayWidth + 1.0), 1f, 7f * dayWidth, (float) (headerHeight / 2));
            g.DrawRectangle(controlDarkPen, (float) w + (float) x1 * dayWidth, 0.0f, 7f * dayWidth, (float) (headerHeight / 2));
          }
          this._BottomScalePoints.Add((float) x1, 1f, date1);
          break;
        case ScaleType.Months:
          if (date1.Day == 1)
          {
            g.DrawRectangle(controlLightLightPen, (float) ((double) w + (double) x1 * (double) dayWidth + 1.0), 1f, (float) DateTime.DaysInMonth(date1.Year, date1.Month) * dayWidth, (float) (headerHeight / 2));
            g.DrawRectangle(controlDarkPen, (float) w + (float) x1 * dayWidth, 0.0f, (float) DateTime.DaysInMonth(date1.Year, date1.Month) * dayWidth, (float) (headerHeight / 2));
          }
          if (date1.DayOfWeek == firstDayOfWeek)
          {
            this._BottomScalePoints.Add((float) x1, 7f, date1);
            break;
          }
          break;
        case ScaleType.Quarters:
          if (date1.Day == 1 && date1.Month % 3 == 1)
          {
            Graphics graphics1 = g;
            Pen pen1 = controlLightLightPen;
            double x2 = (double) w + (double) x1 * (double) dayWidth + 1.0;
            double num4 = (double) dayWidth;
            int num5 = DateTime.DaysInMonth(date1.Year, date1.Month);
            dateTime = date1.AddMonths(1);
            int year1 = dateTime.Year;
            dateTime = date1.AddMonths(1);
            int month1 = dateTime.Month;
            int num6 = DateTime.DaysInMonth(year1, month1);
            int num7 = num5 + num6;
            dateTime = date1.AddMonths(2);
            int year2 = dateTime.Year;
            dateTime = date1.AddMonths(2);
            int month2 = dateTime.Month;
            int num8 = DateTime.DaysInMonth(year2, month2);
            double num9 = (double) (num7 + num8);
            double width1 = num4 * num9;
            double height1 = (double) (headerHeight / 2);
            graphics1.DrawRectangle(pen1, (float) x2, 1f, (float) width1, (float) height1);
            Graphics graphics2 = g;
            Pen pen2 = controlDarkPen;
            double x3 = (double) w + (double) x1 * (double) dayWidth;
            double num10 = (double) dayWidth;
            int num11 = DateTime.DaysInMonth(date1.Year, date1.Month);
            dateTime = date1.AddMonths(1);
            int year3 = dateTime.Year;
            dateTime = date1.AddMonths(1);
            int month3 = dateTime.Month;
            int num12 = DateTime.DaysInMonth(year3, month3);
            int num13 = num11 + num12;
            dateTime = date1.AddMonths(2);
            int year4 = dateTime.Year;
            dateTime = date1.AddMonths(2);
            int month4 = dateTime.Month;
            int num14 = DateTime.DaysInMonth(year4, month4);
            double num15 = (double) (num13 + num14);
            double width2 = num10 * num15;
            double height2 = (double) (headerHeight / 2);
            graphics2.DrawRectangle(pen2, (float) x3, 0.0f, (float) width2, (float) height2);
          }
          if (date1.Day == 1)
          {
            this._BottomScalePoints.Add((float) x1, (float) DateTime.DaysInMonth(date1.Year, date1.Month), date1);
            break;
          }
          break;
        case ScaleType.Years:
          if (date1.Day == 1 && date1.Month == 1)
          {
            g.DrawRectangle(controlLightLightPen, (float) ((double) w + (double) x1 * (double) dayWidth + 1.0), 1f, dayWidth * (DateTime.IsLeapYear(date1.Year) ? 366f : 365f), (float) (headerHeight / 2));
            g.DrawRectangle(controlDarkPen, (float) w + (float) x1 * dayWidth, 0.0f, dayWidth * (DateTime.IsLeapYear(date1.Year) ? 366f : 365f), (float) (headerHeight / 2));
          }
          if (date1.Day == 1 && date1.Month % 3 == 1)
          {
            ScalePoints bottomScalePoints = this._BottomScalePoints;
            double x4 = (double) x1;
            int num16 = DateTime.DaysInMonth(date1.Year, date1.Month);
            dateTime = date1.AddMonths(1);
            int year5 = dateTime.Year;
            dateTime = date1.AddMonths(1);
            int month5 = dateTime.Month;
            int num17 = DateTime.DaysInMonth(year5, month5);
            int num18 = num16 + num17;
            dateTime = date1.AddMonths(2);
            int year6 = dateTime.Year;
            dateTime = date1.AddMonths(2);
            int month6 = dateTime.Month;
            int num19 = DateTime.DaysInMonth(year6, month6);
            double width = (double) (num18 + num19);
            DateTime date2 = date1;
            bottomScalePoints.Add((float) x4, (float) width, date2);
            break;
          }
          break;
      }
    }
    foreach (ScalePoint bottomScalePoint in (List<ScalePoint>) this._BottomScalePoints)
    {
      g.DrawRectangle(controlLightLightPen, (float) ((double) w + (double) bottomScalePoint._X * (double) dayWidth + 1.0), (float) (headerHeight / 2 + 1), bottomScalePoint._Y * dayWidth, (float) (headerHeight / 2 - 2 + py));
      g.DrawRectangle(controlDarkPen, (float) w + bottomScalePoint._X * dayWidth, (float) (headerHeight / 2), bottomScalePoint._Y * dayWidth, (float) (headerHeight / 2 - 1 + py));
    }
    g.DrawLine(Pens.Silver, 0, 0, 0, this.Height);
    int num20;
    switch (numericScaleType)
    {
      case NumericScaleType.Units:
        num20 = 1;
        break;
      case NumericScaleType.Tens:
        num20 = 10;
        break;
      case NumericScaleType.Hundreds:
        num20 = 100;
        break;
      default:
        num20 = 1;
        break;
    }
    int num21 = num20;
    string str1;
    switch (numericScaleType)
    {
      case NumericScaleType.Units:
        str1 = string.Empty;
        break;
      case NumericScaleType.Tens:
        str1 = Resources.TenAbr;
        break;
      case NumericScaleType.Hundreds:
        str1 = Resources.HundredAbr;
        break;
      default:
        str1 = string.Empty;
        break;
    }
    string str2 = str1;
    for (int index1 = -2 * num2; index1 <= days; ++index1)
    {
      DateTime dt = currentDate.AddDays((double) index1);
      TimeSpan timeSpan;
      DateTime start;
      switch (scaleType)
      {
        case ScaleType.Days:
          if (useNumericScaleValues)
          {
            timeSpan = dt - project.Start;
            int totalDays2 = (int) timeSpan.TotalDays;
            if (totalDays2 >= 0 && totalDays2 % num21 == 0)
            {
              int num22 = 1 + totalDays2 / num21;
              g.DrawString(str2 + (object) num22, font, controlTextBrush, (float) ((double) w + (double) index1 * (double) dayWidth + 1.0), (float) ((headerHeight / 2 - font.Height) / 2));
            }
          }
          else
            g.DrawString(GanttChart.FormatDateTime(dt, true, scaleType, project), font, controlTextBrush, (float) ((double) w + (double) index1 * (double) dayWidth + 1.0), (float) ((headerHeight / 2 - font.Height) / 2));
          for (int index2 = 0; index2 < num1; index2 += 3)
          {
            string str3 = index2.ToString();
            SizeF sizeF = g.MeasureString(str3, font);
            g.DrawString(str3, font, controlTextBrush, (float) ((double) w + (double) index1 * (double) dayWidth + 1.0 + (double) index2 * (double) dayWidth / (double) num1 + (double) dayWidth / (double) num1 * 3.0 / 2.0 - (double) sizeF.Width / 2.0), (float) (headerHeight / 2 + (headerHeight / 2 - font.Height) / 2));
          }
          break;
        case ScaleType.Weeks:
          if (dt.DayOfWeek == DayOfWeek.Monday)
          {
            if (!useNumericScaleValues)
            {
              g.DrawString(GanttChart.FormatDateTime(dt, true, scaleType, project), font, controlTextBrush, (float) ((double) w + (double) index1 * (double) dayWidth + 1.0), (float) ((headerHeight / 2 - font.Height) / 2));
            }
            else
            {
              DateTime dateTime1 = dt;
              start = project.Start;
              DateTime dateTime2 = start.AddDays((double) (-(int) project.Start.DayOfWeek + 1));
              timeSpan = dateTime1 - dateTime2;
              int totalDays3 = (int) timeSpan.TotalDays;
              if (totalDays3 >= 0)
              {
                int num23 = 1 + totalDays3 / 7;
                g.DrawString(Resources.WeekAbr + (object) num23, font, controlTextBrush, (float) ((double) w + (double) index1 * (double) dayWidth + 1.0), (float) ((headerHeight / 2 - font.Height) / 2));
              }
            }
          }
          if (!useNumericScaleValues)
          {
            g.DrawString(dt.ToString("ddd").Substring(0, 1), font, controlTextBrush, (float) ((double) w + (double) index1 * (double) dayWidth + 1.0), (float) (headerHeight / 2 + (headerHeight / 2 - font.Height) / 2));
            break;
          }
          timeSpan = dt - project.Start;
          int totalDays4 = (int) timeSpan.TotalDays;
          if (totalDays4 >= 0 && totalDays4 % num21 == 0)
          {
            int num24 = 1 + totalDays4 / num21;
            g.DrawString(str2 + (object) num24, font, controlTextBrush, (float) ((double) w + (double) index1 * (double) dayWidth + 1.0), (float) (headerHeight / 2 + (headerHeight / 2 - font.Height) / 2));
            break;
          }
          break;
        case ScaleType.Months:
          if (dt.Day == 1)
          {
            if (useNumericScaleValues)
            {
              int year7 = dt.Year;
              start = project.Start;
              int year8 = start.Year;
              int num25 = (year7 - year8) * 12;
              int month7 = dt.Month;
              start = project.Start;
              int month8 = start.Month;
              int num26 = month7 - month8;
              int num27 = num25 + num26;
              if (num27 >= 0)
              {
                int num28 = 1 + num27;
                g.DrawString(Resources.MonthAbr + (object) num28, font, controlTextBrush, (float) ((double) w + (double) index1 * (double) dayWidth + 1.0), (float) ((headerHeight / 2 - font.Height) / 2));
              }
            }
            else
              g.DrawString(GanttChart.FormatDateTime(dt, true, scaleType, project), font, controlTextBrush, (float) ((double) w + (double) index1 * (double) dayWidth + 1.0), (float) ((headerHeight / 2 - font.Height) / 2));
          }
          if (dt.DayOfWeek == DayOfWeek.Monday)
          {
            if (!useNumericScaleValues)
            {
              g.DrawString(dt.Day.ToString(), font, controlTextBrush, (float) ((double) w + (double) index1 * (double) dayWidth + 1.0), (float) (headerHeight / 2 + (headerHeight / 2 - font.Height) / 2));
              break;
            }
            DateTime dateTime3 = dt;
            start = project.Start;
            DateTime dateTime4 = start.AddDays((double) (-(int) project.Start.DayOfWeek + 1));
            timeSpan = dateTime3 - dateTime4;
            int totalDays5 = (int) timeSpan.TotalDays;
            if (totalDays5 >= 0)
            {
              int num29 = 1 + totalDays5 / 7;
              g.DrawString(Resources.WeekAbrShort + (object) num29, font, controlTextBrush, (float) ((double) w + (double) index1 * (double) dayWidth + 1.0), (float) (headerHeight / 2 + (headerHeight / 2 - font.Height) / 2));
              break;
            }
            break;
          }
          break;
        case ScaleType.Quarters:
          if (dt.Day == 1 && dt.Month % 3 == 1)
          {
            if (useNumericScaleValues)
            {
              int year9 = dt.Year;
              start = project.Start;
              int year10 = start.Year;
              int num30 = (year9 - year10) * 4;
              int num31 = dt.Month / 3;
              start = project.Start;
              int num32 = start.Month / 3;
              int num33 = num31 - num32;
              int num34 = num30 + num33;
              if (num34 >= 0)
              {
                int num35 = 1 + num34;
                g.DrawString(Resources.QuarterAbr + (object) num35, font, controlTextBrush, (float) ((double) w + (double) index1 * (double) dayWidth + 1.0), (float) ((headerHeight / 2 - font.Height) / 2));
              }
            }
            else
              g.DrawString(string.Format(dt.Month == 1 ? Resources.QuarterFirst : Resources.Quarter, (object) (dt.Month / 3 + 1), (object) dt.Year), font, controlTextBrush, (float) ((double) w + (double) index1 * (double) dayWidth + 1.0), (float) ((headerHeight / 2 - font.Height) / 2));
          }
          if (dt.Day == 1)
          {
            if (!useNumericScaleValues)
            {
              g.DrawString(StringFuncs.UCFirst(dt.ToString("MMM")), font, controlTextBrush, (float) ((double) w + (double) index1 * (double) dayWidth + 1.0), (float) (headerHeight / 2 + (headerHeight / 2 - font.Height) / 2));
              break;
            }
            int year11 = dt.Year;
            start = project.Start;
            int year12 = start.Year;
            int num36 = (year11 - year12) * 12;
            int month9 = dt.Month;
            start = project.Start;
            int month10 = start.Month;
            int num37 = month9 - month10;
            int num38 = num36 + num37;
            if (num38 >= 0)
            {
              int num39 = 1 + num38;
              g.DrawString(Resources.MonthAbrShort + (object) num39, font, controlTextBrush, (float) ((double) w + (double) index1 * (double) dayWidth + 1.0), (float) (headerHeight / 2 + (headerHeight / 2 - font.Height) / 2));
              break;
            }
            break;
          }
          break;
        case ScaleType.Years:
          if (dt.Day == 1 && dt.Month == 1)
          {
            if (useNumericScaleValues)
            {
              int year13 = dt.Year;
              start = project.Start;
              int year14 = start.Year;
              int num40 = year13 - year14;
              if (num40 >= 0)
              {
                int num41 = 1 + num40;
                g.DrawString(Resources.YearAbr + (object) num41, font, controlTextBrush, (float) ((double) w + (double) index1 * (double) dayWidth + 1.0), (float) ((headerHeight / 2 - font.Height) / 2));
              }
            }
            else
              g.DrawString(dt.ToString("yyyy"), font, controlTextBrush, (float) ((double) w + (double) index1 * (double) dayWidth + 1.0), (float) ((headerHeight / 2 - font.Height) / 2));
          }
          if (dt.Day == 1 && dt.Month % 3 == 1)
          {
            if (!useNumericScaleValues)
            {
              g.DrawString(string.Format(Resources.QuarterShort, (object) (dt.Month / 3 + 1)), font, controlTextBrush, (float) ((double) w + (double) index1 * (double) dayWidth + 1.0), (float) (headerHeight / 2 + (headerHeight / 2 - font.Height) / 2));
              break;
            }
            int year15 = dt.Year;
            start = project.Start;
            int year16 = start.Year;
            int num42 = (year15 - year16) * 4;
            int num43 = dt.Month / 3;
            start = project.Start;
            int num44 = start.Month / 3;
            int num45 = num43 - num44;
            int num46 = num42 + num45;
            if (num46 >= 0)
            {
              g.DrawString(Resources.QuarterAbrShort + (object) (1 + num46), font, controlTextBrush, (float) ((double) w + (double) index1 * (double) dayWidth + 1.0), (float) (headerHeight / 2 + (headerHeight / 2 - font.Height) / 2));
              break;
            }
            break;
          }
          break;
      }
    }
    return dictionary;
  }

  private float CalculatePos([CanBeNull] Task task, DateTime start, DateTime currentDate, double startDelay)
  {
    double totalDays = (start - currentDate).TotalDays;
    float pos;
    if (this.ScaleType == ScaleType.Days)
    {
      pos = (float) startDelay + (float) totalDays;
    }
    else
    {
      float num1 = (float) startDelay + (float) (int) totalDays;
      if (totalDays < 0.0)
        --num1;
      float num2 = (float) (totalDays - Math.Floor(totalDays));
      DayTimeIntervalCollection dayTimeIntervals = task.CurrentSchedule.GetDayTimeIntervals(start);
      double start1 = dayTimeIntervals.Start;
      double num3 = dayTimeIntervals.Finish;
      if (num3 == 0.0)
        num3 = 24.0;
      float num4 = (float) (((double) num2 - start1 / 24.0) * 24.0 / (num3 - start1));
      pos = num1 + num4;
    }
    return pos;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool ShowConstraintMarkers
  {
    get => this._showConstraintMarkers;
    set
    {
      if (this._showConstraintMarkers == value)
        return;
      this._showConstraintMarkers = value;
      this.Refresh();
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  private static Image PlanningConflictImage => Images.ExclamationImage;

  /// <summary>
  /// Полная высота прямоугольника задачи, вместе с отступами
  /// </summary>
  [Browsable(false)]
  internal int FullTaskHeight => this.TaskRectangleHeight * 2;

  [CanBeNull]
  protected virtual Dictionary<RectangleF, DragDropOperation> DrawTasks(
    Graphics g,
    int w,
    ClientProject project,
    int visibleTaskIndex,
    int visibleTaskCount,
    DateTime currentDate,
    int days,
    int headerHeight,
    GanttChart.GetRowTopYDelegate getRowTopY,
    float dayWidth,
    Font font,
    Brush standardTaskBrush,
    Brush criticalTaskBrush,
    Brush parentTaskBrush,
    Brush milestoneTaskBrush,
    Brush percentCompletedBrush,
    Brush percentNotCompletedBrush,
    Pen standardTaskPen,
    Pen criticalTaskPen,
    Pen parentTaskPen,
    Pen milestoneTaskPen,
    Pen metConstraintPen,
    Pen notMetConstraintPen,
    Brush windowBrush,
    Brush controlTextBrush,
    bool highlightCriticalTasks,
    bool allowDragDrop,
    Color windowColor,
    Color controlDarkColor,
    Dictionary<Task, Pen> taskPens,
    Dictionary<Task, Brush> taskBrushes,
    float rectangleRoundnessPercent,
    float rectangleHeightPercent)
  {
    if (this.DesignMode)
      return (Dictionary<RectangleF, DragDropOperation>) null;
    Dictionary<RectangleF, DragDropOperation> dictionary1 = new Dictionary<RectangleF, DragDropOperation>();
    Pen pen1 = (Pen) standardTaskPen.Clone();
    Pen pen2 = (Pen) criticalTaskPen.Clone();
    Pen pen3 = (Pen) parentTaskPen.Clone();
    this._toolTipRectangles.Clear();
    List<Task> taskList = new List<Task>(GanttChart.GetTasksPage((Intermech.Project.Project) project, visibleTaskIndex, visibleTaskCount));
    DateTime ed = currentDate.AddDays((double) days);
    int taskRectangleHeight = this._taskRectangleHeight;
    float num1 = (float) taskRectangleHeight / 2f;
    float num2 = (float) taskRectangleHeight;
    Dictionary<Task, RectangleF> dictionary2 = new Dictionary<Task, RectangleF>();
    float num3 = (float) headerHeight;
    float num4 = -1f;
    double x1 = (double) g.ClipBounds.X;
    RectangleF clipBounds = g.ClipBounds;
    double width1 = (double) clipBounds.Width;
    double num5 = x1 + width1;
    clipBounds = g.ClipBounds;
    double y1 = (double) clipBounds.Y;
    clipBounds = g.ClipBounds;
    double height1 = (double) clipBounds.Height;
    float num6 = (float) (y1 + height1);
    float num7 = num6 + 10f;
    int num8 = int.MaxValue;
    int num9 = -1;
    using (Bitmap bitmap = new Bitmap(Convert.ToInt32((float) num5), Convert.ToInt32(num6), PixelFormat.Format32bppArgb))
    {
      Graphics graphics = g;
      using (Graphics.FromImage((Image) bitmap))
      {
        Color.FromArgb(SystemColors.Window.ToArgb() - 1);
        foreach (Task task in taskList)
        {
          if (!task.IsHidden && GanttChart.IsVisible(task, currentDate, ed))
          {
            int index1 = GanttChart.GetIndex(project, task, visibleTaskIndex);
            double startDelay = GanttChart._startDelays.ContainsKey(task) ? GanttChart._startDelays[task] : 0.0;
            double num10 = GanttChart._finishBeforeDictionary.ContainsKey(task) ? GanttChart._finishBeforeDictionary[task] : 0.0;
            float num11 = this.CalculatePos(task, task.Start, currentDate, startDelay);
            float pos1 = this.CalculatePos(task, task.Finish, currentDate, -num10);
            int rowHeight = task.RowHeight;
            if (rowHeight == 0)
              rowHeight = this._rowHeight;
            if (getRowTopY != null)
              num3 = (float) (getRowTopY(index1) + headerHeight);
            ProjectDisplayOptions displayOptions = this.Project.DisplayOptions;
            this.Project.ExtendRowHeightForCaptions(ref rowHeight, this.FullTaskHeight);
            num3 += (float) displayOptions.TaskCaptions.Padding.Top;
            rowHeight -= displayOptions.TaskCaptions.Padding.Top;
            float y2 = num3 + num1;
            float y3 = num3 + (float) taskRectangleHeight;
            float x2 = (float) w + num11 * dayWidth;
            float x3 = (float) w + pos1 * dayWidth;
            this._CurrentXOffset = w;
            RectangleF key1 = new RectangleF(x2, y2, (pos1 - num11) * dayWidth, (float) taskRectangleHeight);
            int index2 = task.Index;
            if (index2 < num8)
              num8 = index2;
            if (index2 > num9)
              num9 = index2;
            if (!task.Milestone)
            {
              if (!task.HasSubTasks)
              {
                bool flag = !highlightCriticalTasks || !task.IsCritical;
                g.TranslateTransform(key1.X, key1.Y);
                // ISSUE: explicit non-virtual call
                Pen taskPen = taskPens == null || !__nonvirtual (taskPens.ContainsKey(task)) ? (Pen) null : taskPens[task];
                // ISSUE: explicit non-virtual call
                Brush taskBrush = taskBrushes == null || !__nonvirtual (taskBrushes.ContainsKey(task)) ? (Brush) null : taskBrushes[task];
                if ((double) rectangleRoundnessPercent == 0.0)
                {
                  Color? taskColor;
                  HatchBrush hatchBrush1;
                  if (!task.TaskColor.HasValue)
                  {
                    hatchBrush1 = (HatchBrush) null;
                  }
                  else
                  {
                    taskColor = task.TaskColor;
                    hatchBrush1 = new HatchBrush(HatchStyle.Percent50, taskColor.Value, SystemColors.Window);
                  }
                  using (HatchBrush hatchBrush2 = hatchBrush1)
                  {
                    taskColor = task.TaskColor;
                    Pen pen4;
                    if (!taskColor.HasValue)
                    {
                      pen4 = (Pen) null;
                    }
                    else
                    {
                      taskColor = task.TaskColor;
                      pen4 = new Pen(taskColor.Value);
                    }
                    using (Pen pen5 = pen4)
                    {
                      Brush brush1 = (Brush) hatchBrush2 ?? (flag ? taskBrush ?? standardTaskBrush : criticalTaskBrush);
                      g.FillRectangle(brush1, 0.0f, 0.0f, key1.Width, key1.Height);
                      g.DrawRectangle(pen5 ?? (flag ? taskPen ?? standardTaskPen : criticalTaskPen), 0.0f, 0.0f, key1.Width, key1.Height);
                      if (this.ShowConstraintMarkers)
                      {
                        taskColor = task.TaskColor;
                        Color black;
                        if (!taskColor.HasValue)
                        {
                          taskColor = task.TaskColor;
                          black = taskColor.Value;
                        }
                        else
                          black = Color.Black;
                        Color color = black;
                        if (brush1 is HatchBrush hatchBrush3)
                          color = hatchBrush3.ForegroundColor;
                        Brush brush2 = (Brush) new SolidBrush(color);
                        float height2 = key1.Height;
                        float num12 = height2 / 2f;
                        PointF[] points;
                        if (task.LeftToRight)
                        {
                          points = new PointF[3]
                          {
                            new PointF(0.0f, height2),
                            new PointF(0.0f, num12),
                            new PointF(num12, height2)
                          };
                        }
                        else
                        {
                          float width2 = key1.Width;
                          points = new PointF[3]
                          {
                            new PointF(width2, height2),
                            new PointF(width2, num12),
                            new PointF((float) ((double) width2 - (double) num12 - 1.0), height2)
                          };
                        }
                        g.FillPolygon(brush2, points);
                        brush2.Dispose();
                      }
                    }
                  }
                }
                else
                {
                  GraphicsPath path = (GraphicsPath) null;
                  try
                  {
                    float radius = (float) ((double) rectangleRoundnessPercent * (double) key1.Height / 2.0);
                    path = GanttChart.GetRoundedRectanglePath(0.0f, 0.0f, key1.Width, key1.Height, radius);
                  }
                  catch (ArgumentException ex)
                  {
                  }
                  if (path != null)
                  {
                    g.FillPath(flag ? taskBrush ?? standardTaskBrush : criticalTaskBrush, path);
                    g.DrawPath(flag ? taskPen ?? standardTaskPen : criticalTaskPen, path);
                  }
                  else
                  {
                    g.FillRectangle(flag ? taskBrush ?? standardTaskBrush : criticalTaskBrush, 0.0f, 0.0f, key1.Width, key1.Height);
                    g.DrawRectangle(flag ? taskPen ?? standardTaskPen : criticalTaskPen, 0.0f, 0.0f, key1.Width, key1.Height);
                  }
                }
                g.TranslateTransform(-key1.X, -key1.Y);
                RectangleF key2 = new RectangleF(x3 - (float) ((double) dayWidth / 2.0 / 2.0), y2, dayWidth / 2f, (float) taskRectangleHeight);
                if (allowDragDrop && !task.ReadOnly)
                  dictionary1[key2] = new DragDropOperation(task, DragDropOperationType.Duration);
                float y4 = num3 + num1 + num1 - (float) (taskRectangleHeight / 6);
                float height3 = (float) (taskRectangleHeight / 3) - 1f;
                if (task.PercentCompleted > 0.0)
                {
                  DateTime start = DateTime.MinValue;
                  DateScheduleList workTime = task.GetWorkTime(task.Start, task.CompletedWork);
                  DateSchedule dateSchedule = (DateSchedule) null;
                  if (workTime.Count > 0)
                    dateSchedule = workTime[workTime.Count - 1];
                  if (dateSchedule != null)
                    start = dateSchedule.FinishTime;
                  float pos2 = this.CalculatePos(task, start, currentDate, startDelay);
                  RectangleF rect = new RectangleF(x2, y4, (pos2 - num11) * dayWidth, height3);
                  g.FillRectangle(percentCompletedBrush, rect);
                  num11 = pos2;
                }
                if (allowDragDrop && !task.ReadOnly)
                {
                  RectangleF key3 = new RectangleF((float) w + num11 * dayWidth, y2, 10f, (float) taskRectangleHeight);
                  dictionary1[key3] = new DragDropOperation(task, DragDropOperationType.PercentCompleted);
                }
                float pos3 = this.CalculatePos(task, DateTime.Now, currentDate, 0.0);
                if ((double) pos3 > (double) num11)
                {
                  RectangleF rect = new RectangleF((float) w + num11 * dayWidth, y4, (Math.Min(pos1, pos3) - num11) * dayWidth, height3);
                  g.FillRectangle(percentNotCompletedBrush, rect);
                }
              }
              else
              {
                float num13 = (float) taskRectangleHeight / 8f;
                PointF[] array = new List<PointF>()
                {
                  new PointF(x2, num13 + y2 + (float) taskRectangleHeight),
                  new PointF(x2 + num2 / 2f, num13 + y3),
                  new PointF(x3 - num2 / 2f, num13 + y3),
                  new PointF(x3, num13 + y2 + (float) taskRectangleHeight),
                  new PointF(x3 + num2 / 2f, num13 + y3),
                  new PointF(x3 + num2 / 2f, num13 + y2),
                  new PointF(x2 - num2 / 2f, num13 + y2),
                  new PointF(x2 - num2 / 2f, num13 + y3)
                }.ToArray();
                g.FillPolygon(parentTaskBrush, array);
                g.DrawPolygon(parentTaskPen, array);
              }
              Image statusImage = Images.GetStatusImage(task.Status);
              if (statusImage != null)
              {
                g.TranslateTransform(key1.X, key1.Y);
                float y5 = key1.Height / 2f - (float) statusImage.Height / 2f;
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                int x4 = -statusImage.Width - 2;
                if (task.HasSubTasks)
                  x4 -= 5;
                g.DrawImage(statusImage, (float) x4, y5);
                g.TranslateTransform(-key1.X, -key1.Y);
              }
              if (task.PlanningConflict && GanttChart.PlanningConflictImage != null)
              {
                g.TranslateTransform(key1.X, key1.Y);
                x3 += (float) (GanttChart.PlanningConflictImage.Width / 2);
                g.DrawImage(GanttChart.PlanningConflictImage, key1.Width + 5f, key1.Height / 2f - (float) (GanttChart.PlanningConflictImage.Height / 2));
                g.TranslateTransform(-key1.X, -key1.Y);
              }
              if (this.Project != null && this.Project.DisplayOptions.ShowFactDurations > FactDurationsDisplayMode.None && task.FactStart > DateTime.MinValue)
              {
                DateTime start = task.FactFinish;
                if (start == DateTime.MinValue && (this.Project.DisplayOptions.ShowFactDurations & FactDurationsDisplayMode.Executed) != FactDurationsDisplayMode.None || start != DateTime.MinValue && (this.Project.DisplayOptions.ShowFactDurations & FactDurationsDisplayMode.Completed) != FactDurationsDisplayMode.None)
                {
                  if (start == DateTime.MinValue && (this.Project.DisplayOptions.ShowFactDurations & FactDurationsDisplayMode.Executed) != FactDurationsDisplayMode.None)
                    start = DateTime.Now;
                  float pos4 = this.CalculatePos(task, task.FactStart, currentDate, startDelay);
                  float pos5 = this.CalculatePos(task, start, currentDate, -num10);
                  float num14 = (float) taskRectangleHeight + 2f;
                  key1 = new RectangleF((float) w + pos4 * dayWidth, y2 + num14, (pos5 - pos4) * dayWidth, 3f);
                  if ((double) key1.Width < 5.0)
                    key1.Width = 5f;
                  g.TranslateTransform(key1.X, key1.Y);
                  g.FillRectangle(this._FactTermsBrush, 0.0f, 0.0f, key1.Width, key1.Height);
                  g.TranslateTransform(-key1.X, -key1.Y);
                }
              }
            }
            else
            {
              List<PointF> pointFList = new List<PointF>();
              float y6 = y2 + num1;
              pointFList.Add(new PointF(x2, y6));
              pointFList.Add(new PointF(x2 + num1, y6 + num1));
              pointFList.Add(new PointF(x2 + (float) taskRectangleHeight, y6));
              pointFList.Add(new PointF(x2 + num1, y6 - num1));
              PointF[] array = pointFList.ToArray();
              g.FillPolygon(milestoneTaskBrush, array);
              g.DrawPolygon(milestoneTaskPen, array);
            }
            if ((double) key1.Width == 0.0)
              key1.Width = (float) taskRectangleHeight;
            dictionary1[key1] = !allowDragDrop || task.ReadOnly ? new DragDropOperation(task, DragDropOperationType.None) : new DragDropOperation(task, DragDropOperationType.Standard);
            if (task._VisualProps == null)
              task._VisualProps = new TaskVisualProps();
            task._VisualProps.Rect = key1;
            dictionary2[task] = key1;
            if (task.StartConstraint >= currentDate.AddDays(-1.0) && task.StartConstraint <= ed.AddDays(1.0))
            {
              float num15 = (float) taskRectangleHeight;
              float pos6 = this.CalculatePos(task, task.StartConstraint, currentDate, 0.0);
              PointF[] array = new List<PointF>()
              {
                new PointF((float) ((double) w + (double) pos6 * (double) dayWidth - (double) num15 / 2.0), (float) ((double) y3 - (double) num15 / 2.0 + 1.0)),
                new PointF((float) ((double) w + (double) pos6 * (double) dayWidth - (double) num15 / 2.0), (float) ((double) y3 + (double) num15 / 2.0 - 1.0)),
                new PointF((float) ((double) w + (double) pos6 * (double) dayWidth - 1.0), y3)
              }.ToArray();
              g.FillPolygon(windowBrush, array);
              g.DrawPolygon(task.ConstraintMet ? metConstraintPen : notMetConstraintPen, array);
            }
            if (task.FinishConstraint >= currentDate.AddDays(-1.0) && task.FinishConstraint <= ed.AddDays(1.0))
            {
              float num16 = (float) taskRectangleHeight;
              float pos7 = this.CalculatePos(task, task.FinishConstraint, currentDate, 0.0);
              PointF[] array = new List<PointF>()
              {
                new PointF((float) ((double) w + (double) pos7 * (double) dayWidth + (double) num16 / 2.0), (float) ((double) y3 - (double) num16 / 2.0 + 1.0)),
                new PointF((float) ((double) w + (double) pos7 * (double) dayWidth + (double) num16 / 2.0), (float) ((double) y3 + (double) num16 / 2.0 - 1.0)),
                new PointF((float) ((double) w + (double) pos7 * (double) dayWidth + 1.0), y3)
              }.ToArray();
              g.FillPolygon(windowBrush, array);
              g.DrawPolygon(task.ConstraintMet ? metConstraintPen : notMetConstraintPen, array);
            }
            PropInfo taskCaption1 = displayOptions.TaskCaptions[DockStyle.Left];
            if (taskCaption1 != null)
            {
              string propString = task.GetPropString(taskCaption1);
              if (!string.IsNullOrWhiteSpace(propString))
              {
                SizeF size = g.MeasureString(propString, font);
                StringFormat format = new StringFormat();
                format.LineAlignment = StringAlignment.Center;
                GanttChart.ToolTipRectangle toolTipRectangle = new GanttChart.ToolTipRectangle(x2 - 10f - size.Width, y3 - size.Height / 2f, size, propString);
                this._toolTipRectangles.Add(toolTipRectangle);
                g.DrawString(propString, font, controlTextBrush, toolTipRectangle._Rect.X, y3, format);
              }
            }
            PropInfo taskCaption2 = displayOptions.TaskCaptions[DockStyle.Right];
            if (taskCaption2 != null)
            {
              string propString = task.GetPropString(taskCaption2);
              if (!string.IsNullOrWhiteSpace(propString))
              {
                SizeF size = g.MeasureString(propString, font);
                StringFormat format = new StringFormat();
                format.LineAlignment = StringAlignment.Center;
                GanttChart.ToolTipRectangle toolTipRectangle = new GanttChart.ToolTipRectangle(x3 + 10f, y3 - size.Height / 2f, size, propString);
                this._toolTipRectangles.Add(toolTipRectangle);
                g.DrawString(propString, font, controlTextBrush, toolTipRectangle._Rect.X, y3, format);
              }
            }
            PropInfo taskCaption3 = displayOptions.TaskCaptions[DockStyle.Top];
            if (taskCaption3 != null)
            {
              string propString = task.GetPropString(taskCaption3);
              if (!string.IsNullOrWhiteSpace(propString))
              {
                SizeF size = g.MeasureString(propString, font);
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                GanttChart.ToolTipRectangle toolTipRectangle = new GanttChart.ToolTipRectangle((float) (((double) x2 + (double) x3) / 2.0), y2 - (float) font.Height, size, propString);
                this._toolTipRectangles.Add(toolTipRectangle);
                g.DrawString(propString, font, controlTextBrush, toolTipRectangle._Rect.X, toolTipRectangle._Rect.Y, format);
                toolTipRectangle._Rect.X -= size.Width / 2f;
              }
            }
            PropInfo taskCaption4 = displayOptions.TaskCaptions[DockStyle.Bottom];
            if (taskCaption4 != null)
            {
              string propString = task.GetPropString(taskCaption4);
              if (!string.IsNullOrWhiteSpace(propString))
              {
                SizeF size = g.MeasureString(propString, font);
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                GanttChart.ToolTipRectangle toolTipRectangle = new GanttChart.ToolTipRectangle((float) (((double) x2 + (double) x3) / 2.0), y2 + (float) taskRectangleHeight, size, propString);
                this._toolTipRectangles.Add(toolTipRectangle);
                g.DrawString(propString, font, controlTextBrush, toolTipRectangle._Rect.X, toolTipRectangle._Rect.Y, format);
                toolTipRectangle._Rect.X -= size.Width / 2f;
              }
            }
            if (getRowTopY == null)
              num3 += (float) rowHeight;
            Pen pen6 = (Pen) null;
            if (task != null && task.HasSubTasks && task.Minimized)
            {
              if (this._minimizedSummaryLinePen == null)
                this._minimizedSummaryLinePen = new Pen((Brush) new HatchBrush(HatchStyle.Percent25, controlDarkColor, windowColor));
              pen6 = this._minimizedSummaryLinePen;
            }
            else if (this.ShowGrid)
              pen6 = this._periodLinePen;
            if (pen6 != null)
              g.DrawLine(pen6, 0.0f, num3 - 1f, (float) (days + 1) * dayWidth, num3 - 1f);
          }
        }
        if (this.ShowGrid && project != null && project.EditingMode.HasComposition())
          g.DrawLine(this._periodLinePen, 0.0f, num3 - 1f + (float) this._rowHeight, (float) (days + 1) * dayWidth, num3 - 1f + (float) this._rowHeight);
        g = graphics;
        CustomLineCap customLineCap = (CustomLineCap) new AdjustableArrowCap(5f, 3f);
        if (customLineCap != null)
          pen1.CustomEndCap = pen2.CustomEndCap = pen3.CustomEndCap = customLineCap;
        int num17 = num8 - visibleTaskIndex;
        int num18 = num9 - visibleTaskIndex;
        foreach (Task task in (System.Collections.ObjectModel.Collection<Task>) project.Tasks)
        {
          foreach (Dependency dependency in (System.Collections.ObjectModel.Collection<Dependency>) task.Dependencies)
          {
            if (dependency.Resolved)
            {
              Task dependentOfTask = dependency.DependentOfTask;
              if (!task.IsHidden && (GanttChart.IsVisible(task, currentDate, ed) || GanttChart.IsVisible(dependentOfTask, currentDate, ed)))
              {
                int index3 = GanttChart.GetIndex(project, dependentOfTask, visibleTaskIndex);
                int index4 = GanttChart.GetIndex(project, task, visibleTaskIndex);
                if ((index3 >= num17 || index4 >= num17) && (index3 <= num18 || index4 <= num18))
                {
                  float num19 = 10f;
                  int num20 = !task.HasSubTasks ? 1 : 0;
                  bool flag1 = !dependentOfTask.HasSubTasks;
                  float val1 = num20 != 0 ? 0.0f : num2 / 2f;
                  float val2 = flag1 ? 0.0f : num2 / 2f;
                  List<PointF> pointFList = new List<PointF>();
                  RectangleF rectangleF1;
                  dictionary2.TryGetValue(dependentOfTask, out rectangleF1);
                  RectangleF rectangleF2;
                  dictionary2.TryGetValue(task, out rectangleF2);
                  if (!rectangleF1.IsEmpty || !rectangleF2.IsEmpty)
                  {
                    if (rectangleF1.IsEmpty)
                    {
                      float pos8 = this.CalculatePos(dependentOfTask, dependentOfTask.Start, currentDate, 0.0);
                      float pos9 = this.CalculatePos(dependentOfTask, dependentOfTask.Finish, currentDate, 0.0);
                      float x5 = (float) w + pos8 * dayWidth;
                      float num21 = (float) w + pos9 * dayWidth;
                      float y7 = index3 < index4 ? num4 : num7;
                      rectangleF1 = new RectangleF(x5, y7, num21 - x5, 10f);
                    }
                    if (rectangleF2.IsEmpty)
                    {
                      float pos10 = this.CalculatePos(task, task.Start, currentDate, 0.0);
                      float pos11 = this.CalculatePos(task, task.Finish, currentDate, 0.0);
                      float x6 = (float) w + pos10 * dayWidth;
                      float num22 = (float) w + pos11 * dayWidth;
                      float y8 = index3 < index4 ? num7 : num4;
                      rectangleF2 = new RectangleF(x6, y8, num22 - x6, 10f);
                    }
                    if (index3 == -9999999 || dependentOfTask.IsHidden)
                    {
                      pointFList.Add(new PointF(rectangleF2.Left - dayWidth - val1, rectangleF2.Top + num1));
                      pointFList.Add(new PointF(rectangleF2.Left - val1, rectangleF2.Top + num1));
                    }
                    else
                    {
                      switch (dependency.DependencyType)
                      {
                        case DependencyType.FinishFinish:
                        case DependencyType.FinishStart:
                          pointFList.Add(new PointF(rectangleF1.Right, rectangleF1.Top + num1));
                          break;
                        case DependencyType.StartFinish:
                        case DependencyType.StartStart:
                          pointFList.Add(new PointF(rectangleF1.Left, rectangleF1.Top + num1));
                          break;
                      }
                      switch (dependency.DependencyType)
                      {
                        case DependencyType.FinishFinish:
                          float num23 = num19 + Math.Max(val1, val2);
                          pointFList.Add(new PointF(rectangleF2.Right + num23, rectangleF1.Top + num1));
                          pointFList.Add(new PointF(rectangleF2.Right + num23, rectangleF2.Top + num1));
                          pointFList.Add(new PointF(rectangleF2.Right + val1, rectangleF2.Top + num1));
                          break;
                        case DependencyType.FinishStart:
                          float x7 = rectangleF2.Left + num1 + val2;
                          pointFList.Add(new PointF(x7, rectangleF1.Top + num1));
                          pointFList.Add(new PointF(x7, index4 >= index3 ? rectangleF2.Top : rectangleF2.Bottom));
                          break;
                        case DependencyType.StartFinish:
                          pointFList.Add(new PointF(rectangleF1.Left - num19 / 2f, rectangleF1.Top + num1));
                          float y9 = (float) ((double) rectangleF1.Top + (double) num1 + (index4 >= index3 ? (double) num19 : -(double) num19));
                          pointFList.Add(new PointF(rectangleF1.Left - num19 / 2f, y9));
                          pointFList.Add(new PointF(rectangleF2.Right + val1 + num19, y9));
                          pointFList.Add(new PointF(rectangleF2.Right + val1 + num19, rectangleF2.Top + num1));
                          pointFList.Add(new PointF(rectangleF2.Right + val1, rectangleF2.Top + num1));
                          break;
                        case DependencyType.StartStart:
                          float num24 = num19 + Math.Max(val1, val2);
                          pointFList.Add(new PointF(rectangleF2.Left - num24, rectangleF1.Top + num1));
                          pointFList.Add(new PointF(rectangleF2.Left - num24, rectangleF2.Top + num1));
                          pointFList.Add(new PointF(rectangleF2.Left - val1, rectangleF2.Top + num1));
                          break;
                      }
                    }
                    bool flag2 = !highlightCriticalTasks || !task.IsCritical || !dependentOfTask.IsCritical;
                    Pen pen7 = flag1 ? (flag2 ? pen1 : pen2) : pen3;
                    if ((double) rectangleRoundnessPercent == 0.0)
                    {
                      g.DrawLines(pen7, pointFList.ToArray());
                    }
                    else
                    {
                      float radius = (float) ((double) rectangleRoundnessPercent * (double) this._rowHeight / 2.0);
                      GraphicsPath path = (GraphicsPath) null;
                      try
                      {
                        path = GanttChart.GetRoundedPath(pointFList.ToArray(), radius);
                      }
                      catch (ArgumentException ex)
                      {
                      }
                      if (path != null)
                        g.DrawPath(pen7, path);
                      else
                        g.DrawLines(pen7, pointFList.ToArray());
                    }
                  }
                }
              }
            }
          }
        }
      }
    }
    return dictionary1;
  }

  private void Project_PropertyChanged([CanBeNull] object sender, [NotNull] PropertyChangedEventArgs e)
  {
    this.Invalidate();
  }

  [DefaultValue(false)]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool AllowDrag
  {
    get => this._allowDrag;
    set
    {
      if (value == this.AllowDrag)
        return;
      this._allowDrag = value;
      this.Invalidate();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DefaultValue(true)]
  public bool AllowDragStart
  {
    get => this._allowDragStart;
    set
    {
      if (value == this.AllowDragStart)
        return;
      this._allowDragStart = value;
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Brush CriticalTaskBrush
  {
    get => this._criticalTaskBrush;
    set
    {
      this._criticalTaskBrush = value;
      this.Invalidate();
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Pen CriticalTaskPen
  {
    get => this._criticalTaskPen;
    set
    {
      this._criticalTaskPen = value;
      this.Invalidate();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public DateTime CurrentDate
  {
    get => this._currentDate;
    set
    {
      if (!(value != this.CurrentDate))
        return;
      value = value.Date;
      int totalDays = (int) value.Subtract(this._initialDate).TotalDays;
      this.HorizontalScrollBar.Value = totalDays < this.HorizontalScrollBar.Minimum || totalDays > this.HorizontalScrollBar.Maximum ? (totalDays < this.HorizontalScrollBar.Minimum ? this.HorizontalScrollBar.Minimum : this.HorizontalScrollBar.Maximum) : totalDays;
      this._scrollPos = totalDays;
      this._scrollPosInc = Math.Max(0, totalDays - this.HorizontalScrollBar.Maximum);
      this._scrollPosDec = Math.Max(0, this.HorizontalScrollBar.Minimum - totalDays);
      this._currentDate = value;
      this.Invalidate();
    }
  }

  [DefaultValue(20)]
  public virtual float DayWidth
  {
    get => this._dayWidth;
    set
    {
      if ((double) this._dayWidth == (double) value)
        return;
      this._dayWidth = value;
      this.HorizontalScrollBar.SmallChange = Math.Max(1, (int) Math.Round(20.0 / (double) this.DayWidth));
      this.Invalidate();
    }
  }

  [DefaultValue(40)]
  public int HeaderHeight
  {
    get => this._headerHeight;
    set
    {
      this._headerHeight = value;
      this.Invalidate();
    }
  }

  [Browsable(false)]
  [DefaultValue(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool HighlightCriticalTasks
  {
    get => this._highlightCriticalTasks;
    set
    {
      if (value == this.HighlightCriticalTasks)
        return;
      this._highlightCriticalTasks = value;
      this.Invalidate();
    }
  }

  [Browsable(false)]
  [DefaultValue(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool ShowGrid
  {
    get => this._showGrid;
    set
    {
      if (this._showGrid == value)
        return;
      this._showGrid = value;
      this.Invalidate();
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Pen MetConstraintPen
  {
    get => this._metConstraintPen;
    set
    {
      this._metConstraintPen = value;
      this.Invalidate();
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Brush MilestoneTaskBrush
  {
    get => this._milestoneTaskBrush;
    set
    {
      this._milestoneTaskBrush = value;
      this.Invalidate();
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Pen MilestoneTaskPen
  {
    get => this._milestoneTaskPen;
    set
    {
      this._milestoneTaskPen = value;
      this.Invalidate();
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Brush NonWorkingDayBrush
  {
    get => this._nonWorkingDayBrush;
    set
    {
      this._nonWorkingDayBrush = value;
      this.Invalidate();
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Pen NotMetConstraintPen
  {
    get => this._notMetConstraintPen;
    set
    {
      this._notMetConstraintPen = value;
      this.Invalidate();
    }
  }

  [DefaultValue(0)]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public NumericScaleType NumericScaleType
  {
    get => this._numericScaleType;
    set
    {
      this._numericScaleType = value;
      this.Invalidate();
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Brush ParentTaskBrush
  {
    get => this._parentTaskBrush;
    set
    {
      this._parentTaskBrush = value;
      this.Invalidate();
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Pen ParentTaskPen
  {
    get => this._parentTaskPen;
    set
    {
      this._parentTaskPen = value;
      this.Invalidate();
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Brush PercentCompletedBrush
  {
    get => this._percentCompletedBrush;
    set
    {
      this._percentCompletedBrush = value;
      this.Invalidate();
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Brush PercentNotCompletedBrush
  {
    get => this._percentNotCompletedBrush;
    set
    {
      this._percentNotCompletedBrush = value;
      this.Invalidate();
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Pen PeriodLinePen
  {
    get => this._periodLinePen;
    set
    {
      this._periodLinePen = value;
      this.Invalidate();
    }
  }

  [Browsable(false)]
  [DefaultValue(null)]
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
      {
        this.Project.PropertyChanged -= new PropertyChangedEventHandler(this.Project_PropertyChanged);
        if (this.ServiceContainer != null)
        {
          this.ServiceContainer.RemoveService<ClientProject>();
          this.ServiceContainer.RemoveService<Intermech.Project.Project>();
        }
      }
      this._project = value;
      if (this.Project != null)
      {
        this.Project.PropertyChanged += new PropertyChangedEventHandler(this.Project_PropertyChanged);
        if (this.ServiceContainer != null)
        {
          this.ServiceContainer.AddService<ClientProject>(this.Project);
          this.ServiceContainer.AddService<Intermech.Project.Project>((Intermech.Project.Project) this.Project);
        }
      }
      this.Invalidate();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DefaultValue(0.5f)]
  public float RectangleHeightPercent
  {
    get => this._rectangleHeightPercent;
    set
    {
      if ((double) value == (double) this.RectangleHeightPercent)
        return;
      this._rectangleHeightPercent = (double) value >= 0.0 && (double) value <= 1.0 ? value : throw new ArgumentOutOfRangeException(nameof (RectangleHeightPercent));
      this.Invalidate();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DefaultValue(10)]
  public int TaskRectangleHeight
  {
    get => this._taskRectangleHeight;
    set
    {
      if (value == this._taskRectangleHeight)
        return;
      this._taskRectangleHeight = value;
      this.Invalidate();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DefaultValue(0.0f)]
  public float RectangleRoundnessPercent
  {
    get => this._rectangleRoundnessPercent;
    set
    {
      if ((double) value == (double) this.RectangleRoundnessPercent)
        return;
      this._rectangleRoundnessPercent = (double) value >= 0.0 && (double) value <= 1.0 ? value : throw new ArgumentOutOfRangeException(nameof (RectangleRoundnessPercent));
      this.Invalidate();
    }
  }

  [DefaultValue(22)]
  public int RowHeight
  {
    get => this._rowHeight;
    set
    {
      this._rowHeight = value;
      this.Invalidate();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DefaultValue(typeof (ScaleType), "Weeks")]
  public ScaleType ScaleType
  {
    get => this._scaleType;
    set
    {
      if (value == this._scaleType)
        return;
      this._scaleType = value;
      this.Invalidate();
      if ((double) this.BarWidth == -1.0)
      {
        switch (this.ScaleType)
        {
          case ScaleType.Days:
            this.DayWidth = 160f;
            break;
          case ScaleType.Weeks:
            this.UpdateDayWidth();
            break;
          case ScaleType.Months:
            this.DayWidth = 4f;
            break;
          case ScaleType.Quarters:
            this.DayWidth = 2f;
            break;
          case ScaleType.Years:
            this.DayWidth = 0.5f;
            break;
        }
      }
      else
        this.BarWidth = this.BarWidth;
      if (this.ScaleTypeChanged == null)
        return;
      this.ScaleTypeChanged((object) this, (EventArgs) null);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Brush StandardTaskBrush
  {
    get => this._standardTaskBrush;
    set
    {
      this._standardTaskBrush = value;
      this.Invalidate();
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Pen StandardTaskPen
  {
    get => this._standardTaskPen;
    set
    {
      this._standardTaskPen = value;
      this.Invalidate();
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Dictionary<Task, Brush> TaskBrushes
  {
    get => this._taskBrushes;
    set
    {
      this._taskBrushes = value;
      this.Invalidate();
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Dictionary<Task, Pen> TaskPens
  {
    get => this._taskPens;
    set
    {
      this._taskPens = value;
      this.Invalidate();
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Pen TodayLinePen
  {
    get => this._todayLinePen;
    set
    {
      this._todayLinePen = value;
      this.Invalidate();
    }
  }

  [DefaultValue(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool UseNumericScaleValues
  {
    get => this._useNumericScaleValues;
    set
    {
      if (value == this.UseNumericScaleValues)
        return;
      this._useNumericScaleValues = value;
      this.Invalidate();
    }
  }

  [DefaultValue(0)]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int VisibleTaskCount
  {
    get => this._visibleTaskCount;
    set
    {
      this._visibleTaskCount = value;
      this.Invalidate();
    }
  }

  [Browsable(false)]
  [DefaultValue(0)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int VisibleTaskIndex
  {
    get => this._visibleTaskIndex;
    set
    {
      this._visibleTaskIndex = value;
      this.Invalidate();
    }
  }

  internal virtual void UpdateDayWidth()
  {
    if ((double) this.BarWidth != -1.0 || this.ScaleType != ScaleType.Weeks)
      return;
    using (Graphics graphics = Graphics.FromHwnd(this.Handle))
    {
      this.DayWidth = (float) Math.Ceiling((double) graphics.MeasureString("M", this.Font).Width);
      if ((double) this.DayWidth / 2.0 == (double) (int) ((double) this.DayWidth / 2.0))
        return;
      ++this.DayWidth;
    }
  }

  protected override void OnFontChanged([NotNull] EventArgs e)
  {
    base.OnFontChanged(e);
    this.UpdateDayWidth();
  }

  [NotNull]
  private static string FormatDateTime(
    DateTime dt,
    bool isTopLevel,
    ScaleType scaleType,
    [CanBeNull] ClientProject p)
  {
    string format = string.Empty;
    Dictionary<ScaleType, string> dictionary = (Dictionary<ScaleType, string>) null;
    if (p?.DisplayOptions != null)
      dictionary = isTopLevel ? p.DisplayOptions.TopLevelFormat : p.DisplayOptions.BottomLevelFormat;
    else
      format = "d";
    dictionary?.TryGetValue(scaleType, out format);
    return StringFuncs.UCFirst(dt.ToString(format));
  }

  public event MouseEventHandler HeaderClick;

  protected override void OnMouseUp([NotNull] MouseEventArgs e)
  {
    base.OnMouseUp(e);
    if (e.Y >= this._headerHeight)
      return;
    this.OnHeaderClick(e);
    MouseEventHandler headerClick = this.HeaderClick;
    if (headerClick == null)
      return;
    headerClick((object) this, e);
  }

  protected virtual void OnHeaderClick([NotNull] MouseEventArgs e)
  {
  }

  internal void HandleMouseWheel([NotNull] MouseEventArgs e)
  {
    if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
    {
      ProjectDisplayOptions displayOptions = this.Project.DisplayOptions;
      ScaleType scaleType = displayOptions.ScaleType;
      if (e.Delta < 0)
      {
        if (scaleType != ScaleType.Years)
          ++scaleType;
      }
      else if (scaleType != ScaleType.Days)
        --scaleType;
      displayOptions.ScaleType = scaleType;
      displayOptions.UpdateControls();
    }
    if (!(e is HandledMouseEventArgs handledMouseEventArgs))
      return;
    handledMouseEventArgs.Handled = true;
  }

  protected override void OnMouseWheel([NotNull] MouseEventArgs e)
  {
    base.OnMouseWheel(e);
    this.HandleMouseWheel(e);
  }

  public new void Invalidate() => base.Invalidate();

  public new void Refresh() => base.Refresh();

  public void Print(
    Graphics g,
    int w,
    int py,
    ClientProject project,
    int visibleTaskIndex,
    int visibleTaskCount,
    DateTime currentDate,
    int days,
    int headerHeight,
    int rowHeight,
    float dayWidth,
    int height,
    Font font,
    Brush controlBrush,
    Brush controlTextBrush,
    Pen controlDarkPen,
    Pen controlLightLightPen,
    Color controlColor,
    Color windowColor,
    Color controlDarkColor,
    Brush standardTaskBrush,
    Brush criticalTaskBrush,
    Brush parentTaskBrush,
    Brush milestoneTaskBrush,
    Brush percentCompletedBrush,
    Brush percentNotCompletedBrush,
    Pen standardTaskPen,
    Pen criticalTaskPen,
    Pen parentTaskPen,
    Pen milestoneTaskPen,
    Pen metConstraintPen,
    Pen notMetConstraintPen,
    ScaleType scaleType,
    bool highlightCriticalTasks,
    bool useNumericScaleValues,
    NumericScaleType numericScaleType,
    Brush nonWorkingDayBrush,
    Pen periodLinePen,
    Pen todayLinePen,
    Dictionary<Task, Pen> taskPens,
    Dictionary<Task, Brush> taskBrushes,
    float rectangleRoundnessPercent,
    float rectangleHeightPercent)
  {
    this.Draw(g, w, py, project, visibleTaskIndex, visibleTaskCount, currentDate, days, headerHeight, rowHeight, dayWidth, height, font, controlBrush, controlTextBrush, controlDarkPen, controlLightLightPen, controlColor, windowColor, controlDarkColor, standardTaskBrush, criticalTaskBrush, parentTaskBrush, milestoneTaskBrush, percentCompletedBrush, percentNotCompletedBrush, standardTaskPen, criticalTaskPen, parentTaskPen, milestoneTaskPen, metConstraintPen, notMetConstraintPen, highlightCriticalTasks, false, scaleType, useNumericScaleValues, numericScaleType, nonWorkingDayBrush, periodLinePen, todayLinePen, taskPens, taskBrushes, rectangleRoundnessPercent, rectangleHeightPercent);
  }

  [Browsable(false)]
  [NotNull]
  public HScrollBar CurrentDateScrollBar => this.HorizontalScrollBar;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int CurrentDateScrollMaximumValue
  {
    get => this.HorizontalScrollBar.Maximum;
    set
    {
      if (value == this.CurrentDateScrollMaximumValue)
        return;
      this.HorizontalScrollBar.Maximum = value;
      this.hScrollBar_Scroll((object) this.HorizontalScrollBar, new ScrollEventArgs(ScrollEventType.ThumbPosition, this.HorizontalScrollBar.Value));
    }
  }

  [DefaultValue(0)]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int DisplayedRowCount
  {
    get => this._displayedRowCount;
    set
    {
      if (value == this.DisplayedRowCount)
        return;
      this._displayedRowCount = value;
      this.VisibleTaskCount = this.DisplayedRowCount + 1;
    }
  }

  [DefaultValue(0)]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int FirstDisplayedScrollingRowIndex
  {
    get => this._firstDisplayedScrollingRowIndex;
    set
    {
      if (value == this.FirstDisplayedScrollingRowIndex)
        return;
      this._firstDisplayedScrollingRowIndex = value;
      this.VisibleTaskIndex = this.FirstDisplayedScrollingRowIndex;
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  internal DateTime InitialDate
  {
    get => this._initialDate;
    set
    {
      if (!(value != this.InitialDate))
        return;
      this._initialDate = value.Date;
      this.CurrentDate = this._initialDate.AddDays((double) this._scrollPos);
    }
  }

  [CanBeNull]
  public Task Task => this._DragDropOperation?.Task;

  public event EventHandler ScaleTypeChanged;

  public bool HandleParentMouseWheel([CanBeNull] Control parent, [NotNull] MouseEventArgs e)
  {
    if (parent == null || (Control.ModifierKeys & Keys.Control) != Keys.Control || this.Focused || !this.Bounds.Contains(this.PointToClient(parent.PointToScreen(e.Location))))
      return false;
    this.HandleMouseWheel(e);
    return true;
  }

  [DefaultValue(-1)]
  public float BarWidth
  {
    get => this._barWidth;
    set
    {
      this._barWidth = value;
      switch (this.ScaleType)
      {
        case ScaleType.Days:
          this.DayWidth = this._barWidth * 8f;
          break;
        case ScaleType.Weeks:
          this.DayWidth = this._barWidth;
          break;
        case ScaleType.Months:
          this.DayWidth = this._barWidth / 7f;
          break;
        case ScaleType.Quarters:
          this.DayWidth = this._barWidth / 30f;
          break;
        case ScaleType.Years:
          this.DayWidth = this._barWidth / 91f;
          break;
      }
    }
  }

  protected override void WndProc(ref Message m)
  {
    if (this.GridView != null && m.Msg == 522)
      Intermech.WindowsDll.User32.PostMessage(this.GridView.Handle, m.Msg, m.WParam, m.LParam);
    base.WndProc(ref m);
  }

  /// <summary>Initializes the component</summary>
  private void InitializeComponent()
  {
    this._hScrollBar = new HScrollBar();
    this.SuspendLayout();
    this._hScrollBar.Dock = DockStyle.Bottom;
    this._hScrollBar.Location = new Point(0, 461);
    this._hScrollBar.Name = "_hScrollBar";
    this._hScrollBar.Size = new Size(686, 17);
    this._hScrollBar.TabIndex = 1;
    this._hScrollBar.Location = new Point(0, 457);
    this._hScrollBar.Scroll += new ScrollEventHandler(this.hScrollBar_Scroll);
    this.Controls.Add((Control) this._hScrollBar);
    this.Name = "ProjectGanttChart";
    this.Size = new Size(686, 478);
    this.ResumeLayout(false);
  }

  private struct ToolTipRectangle
  {
    public RectangleF _Rect;
    public readonly string _Text;

    public ToolTipRectangle(float x, float y, SizeF size, [CanBeNull] string text)
    {
      this._Rect = new RectangleF(x, y, size.Width, size.Height);
      this._Text = text;
    }
  }

  public delegate int GetRowTopYDelegate(int rowNum);
}
