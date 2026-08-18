// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.ResourcesGanttChart
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Bars;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

/// <summary>Диаграмма загрузки ресурсов</summary>
public class ResourcesGanttChart : 
  GanttChart,
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
  [NotNull]
  private readonly StringFormat _bottomCaptionFormat;
  [NotNull]
  private readonly StringFormat _titleCaptionFormat;
  private bool _legacyPaint;
  private ResourcesCalcMode _calcMode = ResourcesCalcMode.PeakLoad;
  private string _calModeString;
  [NotNull]
  private readonly Dictionary<UserSummaryTask, DateScheduleList> _overallWorkSchedules = new Dictionary<UserSummaryTask, DateScheduleList>();
  private Pen _gridPen;
  private Pen _thickPen;
  [NotNull]
  private readonly Dictionary<float, string> _hScale = new Dictionary<float, string>();
  private int _prevScale;
  private float _minTextHeight;
  private ContextMenuBarItem _contextMenu;
  [CanBeNull]
  private UserSummaryTask _currentUserTask;
  [CanBeNull]
  private Panel _scalePanel;

  public ResourcesGanttChart()
  {
    this._bottomCaptionFormat = new StringFormat();
    this._bottomCaptionFormat.Alignment = StringAlignment.Center;
    this._titleCaptionFormat = new StringFormat();
    this._titleCaptionFormat.Alignment = StringAlignment.Far;
    this._titleCaptionFormat.LineAlignment = StringAlignment.Center;
  }

  protected override Dictionary<RectangleF, DragDropOperation> DrawTasks(
    Graphics g,
    int w,
    ClientProject project,
    int visibleTaskIndex,
    int visibleTaskCount,
    DateTime currentDate,
    int days,
    int headerHeight,
    GanttChart.GetRowTopYDelegate getRowHeight,
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
    return !this._legacyPaint ? new Dictionary<RectangleF, DragDropOperation>() : base.DrawTasks(g, w, project, visibleTaskIndex, visibleTaskCount, currentDate, days, headerHeight, getRowHeight, dayWidth, font, standardTaskBrush, criticalTaskBrush, parentTaskBrush, milestoneTaskBrush, percentCompletedBrush, percentNotCompletedBrush, standardTaskPen, criticalTaskPen, parentTaskPen, milestoneTaskPen, metConstraintPen, notMetConstraintPen, windowBrush, controlTextBrush, highlightCriticalTasks, allowDragDrop, windowColor, controlDarkColor, taskPens, taskBrushes, rectangleRoundnessPercent, rectangleHeightPercent);
  }

  internal override void UpdateDayWidth()
  {
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ResourcesCalcMode CalcMode
  {
    get => this._calcMode;
    set
    {
      if (this._calcMode == value)
        return;
      this._calcMode = value;
      this._calModeString = (string) null;
      this.Invalidate();
    }
  }

  [NotNull]
  [Browsable(false)]
  public string CalcModeString
  {
    get
    {
      this._calModeString = this._calModeString ?? SimpleFuncs.GetEnumDescription((Enum) this.CalcMode);
      return this._calModeString;
    }
  }

  public static int IntervalsSorter([NotNull] TimeInterval ti1, [NotNull] TimeInterval ti2)
  {
    double num = ti1.Start - ti2.Start;
    if (num < 0.0)
      return -1;
    return num <= 0.0 ? 0 : 1;
  }

  [NotNull]
  protected DateScheduleList GetOverallWorkSchedules([NotNull] UserSummaryTask currentUserTask)
  {
    DateScheduleList overallWorkSchedules;
    this._overallWorkSchedules.TryGetValue(currentUserTask, out overallWorkSchedules);
    if (overallWorkSchedules == null)
    {
      overallWorkSchedules = new DateScheduleList();
      this._overallWorkSchedules.Add(currentUserTask, overallWorkSchedules);
      foreach (Task subTask in (IEnumerable<Task>) currentUserTask.SubTasks)
      {
        foreach (DateSchedule dateSchedule1 in (List<DateSchedule>) subTask.GetWorkTime(currentUserTask.ObjectID, subTask.Start, subTask.Finish))
        {
          DateSchedule byDate = overallWorkSchedules.GetByDate(dateSchedule1.Date);
          if (byDate != null)
          {
            byDate.TimeIntervalCollection.Merge(dateSchedule1.TimeIntervalCollection);
          }
          else
          {
            DateSchedule dateSchedule2 = dateSchedule1.Clone();
            overallWorkSchedules.Add(dateSchedule2);
          }
        }
      }
    }
    return overallWorkSchedules;
  }

  /// <summary>Возвращает процент загрузки для текущего юзера на задаче t</summary>
  private double GetMaxUnits([NotNull] Task t)
  {
    return ResourcesGanttChart.GetMaxUnits(t, this.CurrentUserTask);
  }

  private static double GetMaxUnits([NotNull] Task t, [CanBeNull] UserSummaryTask currentUserTask)
  {
    double maxUnits = 1.0;
    if (currentUserTask != null)
    {
      foreach (Assignment assignment in (System.Collections.ObjectModel.Collection<Assignment>) t.Assignments)
      {
        if (assignment.Resource != null && assignment.Resource.ObjectID == currentUserTask.ObjectID)
        {
          maxUnits = assignment.MaxUnits;
          break;
        }
      }
    }
    return maxUnits;
  }

  private double GetUsage(DateTime start, DateTime finish)
  {
    return this.GetUsage(this.CurrentUserTask, this.CalcMode, start, finish);
  }

  /// <summary>Возвращает коэффициенты загрузки</summary>
  [CanBeNull]
  public UsageData GetUsageData([CanBeNull] UserSummaryTask currentUserTask, DateTime start, DateTime finish)
  {
    if (currentUserTask == null)
      return (UsageData) null;
    double peakLoad = 0.0;
    double work = 0.0;
    DateTime date = start;
    do
    {
      DateSchedule byDate = this.GetOverallWorkSchedules(currentUserTask).GetByDate(date);
      if (byDate != null)
      {
        foreach (TimeInterval timeInterval in (System.Collections.ObjectModel.Collection<TimeInterval>) byDate.TimeIntervalCollection)
        {
          double num1 = timeInterval.Start;
          if (start.Date == date.Date)
          {
            double num2 = (double) start.Hour + (double) start.Minute / 60.0;
            if (num2 > num1)
              num1 = num2;
          }
          double num3 = timeInterval.Finish;
          if (finish.Date == date.Date)
          {
            double num4 = (double) finish.Hour + (double) finish.Minute / 60.0;
            if (num4 < num3)
              num3 = num4;
          }
          double num5 = num3 - num1;
          if (num5 >= 0.0)
          {
            double num6 = num5 * timeInterval.Ratio;
            work += num6;
            if (timeInterval.Ratio > peakLoad)
              peakLoad = timeInterval.Ratio;
          }
        }
      }
      date = date.AddDays(1.0);
    }
    while (date <= finish);
    DateScheduleList workTime = currentUserTask.UserSchedule?.GetWorkTime(start, finish);
    return new UsageData(workTime != null ? workTime.Work : 0.0, work, peakLoad);
  }

  public double GetUsage(
    [CanBeNull] UserSummaryTask currentUserTask,
    ResourcesCalcMode calcMode,
    DateTime start,
    DateTime finish)
  {
    if (this.DesignMode)
      return 0.0;
    UsageData usageData = this.GetUsageData(currentUserTask, start, finish);
    if (usageData == null)
      return 0.0;
    return calcMode != ResourcesCalcMode.Load ? usageData._PeakLoad : usageData.Load;
  }

  [NotNull]
  [Browsable(false)]
  protected Pen GridPen
  {
    get
    {
      if (this._gridPen == null)
      {
        this._gridPen = new Pen(Color.Gray);
        this._gridPen.DashStyle = DashStyle.Dot;
      }
      return this._gridPen;
    }
  }

  [NotNull]
  [Browsable(false)]
  protected Pen ThickPen
  {
    get
    {
      if (this._thickPen == null)
      {
        this._thickPen = new Pen(Color.Black);
        this._thickPen.Width = 2f;
      }
      return this._thickPen;
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IReadOnlyDictionary<float, string> HScale
  {
    get => (IReadOnlyDictionary<float, string>) this._hScale;
  }

  [Browsable(false)]
  private float MinTextHeight
  {
    get
    {
      if ((double) this._minTextHeight == 0.0)
      {
        using (Graphics graphics = Graphics.FromHwnd(this.Handle))
          this._minTextHeight = graphics.MeasureString("M", this.Font).Height;
      }
      return this._minTextHeight;
    }
  }

  public override Dictionary<RectangleF, DragDropOperation> Draw(
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
    Dictionary<RectangleF, DragDropOperation> dictionary = base.Draw(g, w, py, project, visibleTaskIndex, visibleTaskCount, currentDate, days, headerHeight, rowHeight, dayWidth, height, font, controlBrush, controlTextBrush, controlDarkPen, controlLightLightPen, controlColor, windowColor, controlDarkColor, standardTaskBrush, criticalTaskBrush, parentTaskBrush, milestoneTaskBrush, percentCompletedBrush, percentNotCompletedBrush, standardTaskPen, criticalTaskPen, parentTaskPen, milestoneTaskPen, metConstraintPen, notMetConstraintPen, highlightCriticalTasks, allowDragDrop, scaleType, useNumericScaleValues, numericScaleType, nonWorkingDayBrush, periodLinePen, todayLinePen, taskPens, taskBrushes, rectangleRoundnessPercent, rectangleHeightPercent);
    w += this._SpacerDaysWidth;
    if (!this._legacyPaint)
    {
      List<ResourcesGanttChart.UsageInfo> usageInfoList = new List<ResourcesGanttChart.UsageInfo>();
      double a = 0.0;
      int num1 = 2;
      float height1 = 17f;
      float height2 = (float) (15 + this.HorizontalScrollBar.Height);
      float num2 = (float) height - height2;
      float num3 = num2 - (float) headerHeight - height1;
      foreach (ScalePoint bottomScalePoint in (List<ScalePoint>) this._BottomScalePoints)
      {
        double usage = this.GetUsage(currentDate.AddDays((double) bottomScalePoint._X), currentDate.AddDays((double) bottomScalePoint._X + (double) bottomScalePoint._Y));
        if (usage > 0.0)
        {
          usageInfoList.Add(new ResourcesGanttChart.UsageInfo(bottomScalePoint._X, bottomScalePoint._Y, usage));
          if (usage > a)
            a = usage;
        }
        g.DrawLine(Pens.Black, (float) w + bottomScalePoint._X * dayWidth, num2, (float) w + bottomScalePoint._X * dayWidth, (float) height);
      }
      if (a == 0.0)
        a = 1.0;
      int int32 = Convert.ToInt32(Math.Ceiling(a));
      float num4 = num3 / (float) int32;
      int num5 = int32;
      while (num5 > 10)
        num5 /= 2;
      if (num5 < 4)
        num5 = 4;
      while (num5 > 1 && (double) num3 / (double) num5 < (double) this.MinTextHeight * 1.5)
        num5 /= 2;
      float num6 = (float) int32 / (float) num5;
      this._hScale.Clear();
      for (int index = 1; index <= num5; ++index)
      {
        float num7 = num2 - (float) index * num6 * num4;
        Pen pen = this.GridPen;
        if ((double) index * (double) num6 == 1.0)
        {
          pen = this.ThickPen;
          num7 += 0.5f;
        }
        g.DrawLine(pen, (float) w, num7, (float) w + (float) days * dayWidth, num7);
        this._hScale.Add(num7, Convert.ToInt32((float) ((double) index * (double) num6 * 100.0)).ToString() + "%");
      }
      if (int32 != this._prevScale && this.ScalePanel != null)
      {
        this._prevScale = int32;
        this.ScalePanel.Invalidate();
      }
      RectangleF rectangleF;
      foreach (ResourcesGanttChart.UsageInfo usageInfo in usageInfoList)
      {
        double num8 = usageInfo._Usage;
        float height3 = (float) num8 * num4;
        float x1 = (float) w + usageInfo._Start * dayWidth + (float) num1;
        float width = usageInfo._Width * dayWidth - (float) (2 * num1);
        rectangleF = new RectangleF(x1, (float) headerHeight + height1 + num3 - height3, width, height3);
        g.FillRectangle(SystemBrushes.Window, rectangleF);
        if (num8 > 1.0)
          num8 = 1.0;
        float height4 = (float) num8 * num4;
        float x2 = (float) w + usageInfo._Start * dayWidth + (float) num1;
        rectangleF = new RectangleF(x2, (float) headerHeight + height1 + num3 - height4, width, height4);
        g.FillRectangle(standardTaskBrush, rectangleF);
        g.DrawRectangle(standardTaskPen, rectangleF.X, rectangleF.Y, rectangleF.Width, rectangleF.Height);
        double usage = usageInfo._Usage;
        if (usage > 1.0)
        {
          float num9 = (float) usage * num4;
          rectangleF = new RectangleF(x2, (float) headerHeight + height1 + num3 - num9, width, num9 - num4);
          g.FillRectangle(criticalTaskBrush, rectangleF);
          g.DrawRectangle(criticalTaskPen, rectangleF.X, rectangleF.Y, rectangleF.Width, rectangleF.Height);
        }
        rectangleF = new RectangleF(x2, num2, width, height2);
        g.DrawString($"{Convert.ToInt32(usage * 100.0)}%", font, Brushes.Black, rectangleF, this._bottomCaptionFormat);
      }
      g.DrawLine(Pens.Black, (float) w, num2, (float) w + (float) days * dayWidth, num2);
      rectangleF = new RectangleF(0.0f, (float) headerHeight, (float) (this.Width - 2), height1);
      g.DrawString(this.CalcModeString, font, Brushes.Silver, rectangleF, this._titleCaptionFormat);
    }
    return dictionary;
  }

  protected override void OnKeyDown(KeyEventArgs e)
  {
    base.OnKeyDown(e);
    if (!e.Control || !e.Shift || e.KeyCode != Keys.V)
      return;
    this._legacyPaint = !this._legacyPaint;
    this.Refresh();
  }

  [Browsable(false)]
  protected override bool DrawNonWorkingTime => false;

  [NotNull]
  [Browsable(false)]
  private ContextMenuBarItem ContextMenu
  {
    get
    {
      if (this._contextMenu == null)
      {
        this._contextMenu = new ContextMenuBarItem();
        foreach (ResourcesCalcMode resourcesCalcMode in Enum.GetValues(typeof (ResourcesCalcMode)))
        {
          string enumDescription = SimpleFuncs.GetEnumDescription((Enum) resourcesCalcMode);
          MenuButtonItem menuButtonItem = new MenuButtonItem();
          menuButtonItem.Text = enumDescription;
          menuButtonItem.ToolTipText = enumDescription;
          menuButtonItem.Tag = (object) resourcesCalcMode;
          menuButtonItem.Click += new EventHandler(this.CalcModeMI_Click);
          this._contextMenu.Items.Add((ToolbarItemBase) menuButtonItem);
        }
      }
      return this._contextMenu;
    }
  }

  private void CalcModeMI_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    object tag;
    if (!(sender is MenuButtonItem menuButtonItem) || !((tag = menuButtonItem.Tag) is ResourcesCalcMode))
      return;
    this.CalcMode = (ResourcesCalcMode) tag;
  }

  private void InvokeContextMenu([CanBeNull] object sender, [NotNull] MouseEventArgs e)
  {
    foreach (MenuItemBase menuItemBase in (CollectionBase) this.ContextMenu.Items)
    {
      object tag;
      if ((tag = menuItemBase.Tag) is ResourcesCalcMode)
      {
        ResourcesCalcMode resourcesCalcMode = (ResourcesCalcMode) tag;
        menuItemBase.Checked = resourcesCalcMode == this.CalcMode;
      }
    }
    this.ContextMenu.Show(BaseHolder.PopupHost, sender as Control, new Point(e.X, e.Y));
  }

  protected override void OnMouseDown([NotNull] MouseEventArgs e)
  {
    base.OnMouseDown(e);
    if (e.Button != MouseButtons.Right)
      return;
    this.BeginInvoke((Delegate) new MouseEventHandler(this.InvokeContextMenu), (object) this, (object) e);
  }

  internal void ResetCaches() => this._overallWorkSchedules.Clear();

  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public UserSummaryTask CurrentUserTask
  {
    get => this._currentUserTask;
    set
    {
      if (this._currentUserTask == value)
        return;
      this._currentUserTask = value;
      this.Refresh();
    }
  }

  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DefaultValue(null)]
  public ResourcesSummaryProject Project
  {
    get => (ResourcesSummaryProject) base.Project;
    set
    {
      this.Project = (ClientProject) value;
      if (value == null)
        return;
      int num = this.ClientSize.Height / this.RowHeight;
      if (num > value.Tasks.Count)
        num = value.Tasks.Count;
      this.DisplayedRowCount = num;
      this.FirstDisplayedScrollingRowIndex = 0;
      TimeSpan timeSpan = value.Finish.Subtract(value.Start);
      this.InitialDate = value.Start.AddDays(0.0);
      this.CurrentDateScrollMaximumValue = timeSpan.Days + 92;
      this.CurrentDate = this.InitialDate;
      value.DisplayOptions.GanttChart = (GanttChart) this;
      if (value.InnerProject == null)
        return;
      value.InnerProject.OnLoaded += new EventHandler(this.project_OnLoaded);
    }
  }

  private void project_OnLoaded([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.ResetCaches();
  }

  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Panel ScalePanel
  {
    get => this._scalePanel;
    set
    {
      if (this._scalePanel != null)
      {
        this._scalePanel.Resize -= new EventHandler(this.ScalePanel_Resize);
        this._scalePanel.Paint -= new PaintEventHandler(this.ScalePanel_Paint);
      }
      this._scalePanel = value;
      if (this._scalePanel == null)
        return;
      this._scalePanel.Resize += new EventHandler(this.ScalePanel_Resize);
      this._scalePanel.Paint += new PaintEventHandler(this.ScalePanel_Paint);
    }
  }

  private void ScalePanel_Resize([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this._scalePanel?.Invalidate();
  }

  private void ScalePanel_Paint([CanBeNull] object sender, [NotNull] PaintEventArgs e)
  {
    if (this._scalePanel == null)
      return;
    e.Graphics.FillRectangle(SystemBrushes.Window, this._scalePanel.ClientRectangle);
    foreach (KeyValuePair<float, string> keyValuePair in (IEnumerable<KeyValuePair<float, string>>) this.HScale)
    {
      float num = keyValuePair.Key - 2f;
      e.Graphics.DrawLine(Pens.Black, (float) (this._scalePanel.Width - 10), num, (float) this._scalePanel.Width, num);
      StringFormat format = new StringFormat();
      RectangleF layoutRectangle = new RectangleF((float) (this._scalePanel.Width - 60), num - 10f, 50f, 20f);
      format.Alignment = StringAlignment.Far;
      format.LineAlignment = StringAlignment.Center;
      Font font = new Font(this.Font, this.Font.Style);
      while ((double) font.Size > 3.0 && (double) e.Graphics.MeasureString(keyValuePair.Value, font).Width > (double) layoutRectangle.Width)
        font = new Font(font.FontFamily, font.Size - 1f);
      e.Graphics.DrawString(keyValuePair.Value, font, Brushes.Black, layoutRectangle, format);
    }
  }

  private class UsageInfo
  {
    public readonly float _Start;
    public readonly float _Width;
    public readonly double _Usage;

    public UsageInfo(float start, float width, double usage)
    {
      this._Start = start;
      this._Width = width;
      this._Usage = usage;
    }
  }
}
