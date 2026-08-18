
// Type: Intermech.Client.Core.Organizer.CalendarRenderer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// Base class that renders visual elements of Calendar control.
/// </summary>
public class CalendarRenderer
{
  private Scheduler _calendar;
  private Rectangle[] _dayNameHeaderColumns;
  private Rectangle _timeScaleBounds;
  private Padding _itemTextMargin = new Padding(3, 3, 6, 3);
  private int _allDayItemsPadding = 5;
  private int _standardItemHeight;
  private int _dayTopHeight;
  private int _dayTopMinHeight;
  private int _dayHeaderHeight;
  private int _dayNameHeadersHeight;
  private int _itemInvalidateMargin;
  private int _itemsPadding = 5;
  private int _itemShadowPadding = 4;
  private int _itemRoundness;
  private int _timeScaleUnitHeight;
  private int _timeScaleWidth;
  private int _weekHeaderWidth;

  /// <summary>
  /// Compares both <see cref="T:Intermech.Client.Core.Organizer.CalendarDayTop" /> items by Date.
  /// </summary>
  /// <param name="top1"></param>
  /// <param name="top2"></param>
  /// <returns></returns>
  private static int CompareTops(CalendarDayTop top1, CalendarDayTop top2)
  {
    return top1.Date.CompareTo(top2.Date);
  }

  /// <summary>Comparison delegate to sort units.</summary>
  /// <param name="item1"></param>
  /// <param name="item2"></param>
  /// <returns></returns>
  private static int CompareUnits(SchedulerTimeScaleUnit item1, SchedulerTimeScaleUnit item2)
  {
    return item1.Date.CompareTo(item2.Date);
  }

  /// <summary>Creates a rectangle with rounded corners.</summary>
  /// <param name="r"></param>
  /// <param name="radius"></param>
  /// <returns></returns>
  public static GraphicsPath RoundRectangle(Rectangle r, int radius)
  {
    return CalendarRenderer.RoundRectangle(r, radius, CalendarRenderer.Corners.All);
  }

  /// <summary>
  /// Creates a rectangle with the specified corners rounded.
  /// </summary>
  /// <param name="r"></param>
  /// <param name="radius"></param>
  /// <param name="corners"></param>
  /// <returns></returns>
  public static GraphicsPath RoundRectangle(
    Rectangle r,
    int radius,
    CalendarRenderer.Corners corners)
  {
    GraphicsPath graphicsPath = new GraphicsPath();
    if (r.Width <= 0 || r.Height <= 0)
      return graphicsPath;
    int num1 = radius * 2;
    int num2 = (corners & CalendarRenderer.Corners.NorthWest) == CalendarRenderer.Corners.NorthWest ? num1 : 0;
    int num3 = (corners & CalendarRenderer.Corners.NorthEast) == CalendarRenderer.Corners.NorthEast ? num1 : 0;
    int num4 = (corners & CalendarRenderer.Corners.SouthEast) == CalendarRenderer.Corners.SouthEast ? num1 : 0;
    int num5 = (corners & CalendarRenderer.Corners.SouthWest) == CalendarRenderer.Corners.SouthWest ? num1 : 0;
    graphicsPath.AddLine(r.Left + num2, r.Top, r.Right - num3, r.Top);
    if (num3 > 0)
      graphicsPath.AddArc(Rectangle.FromLTRB(r.Right - num3, r.Top, r.Right, r.Top + num3), -90f, 90f);
    graphicsPath.AddLine(r.Right, r.Top + num3, r.Right, r.Bottom - num4);
    if (num4 > 0)
      graphicsPath.AddArc(Rectangle.FromLTRB(r.Right - num4, r.Bottom - num4, r.Right, r.Bottom), 0.0f, 90f);
    graphicsPath.AddLine(r.Right - num4, r.Bottom, r.Left + num5, r.Bottom);
    if (num5 > 0)
      graphicsPath.AddArc(Rectangle.FromLTRB(r.Left, r.Bottom - num5, r.Left + num5, r.Bottom), 90f, 90f);
    graphicsPath.AddLine(r.Left, r.Bottom - num5, r.Left, r.Top + num2);
    if (num2 > 0)
      graphicsPath.AddArc(Rectangle.FromLTRB(r.Left, r.Top, r.Left + num2, r.Top + num2), 180f, 90f);
    graphicsPath.CloseFigure();
    return graphicsPath;
  }

  /// <summary>
  /// Gets the <see cref="P:Intermech.Client.Core.Organizer.CalendarRenderer.Calendar" /> this renderer belongs to.
  /// </summary>
  public Scheduler Calendar => this._calendar;

  /// <summary>Gets the height of the header of days.</summary>
  public virtual int DayHeaderHeight
  {
    get
    {
      if (this._dayHeaderHeight == 0)
        this._dayHeaderHeight = TextRenderer.MeasureText("Ag", this.Calendar.Font).Height + 6;
      return this._dayHeaderHeight;
    }
  }

  /// <summary>Gets the bounds for day name headers.</summary>
  public Rectangle[] DayNameHeaderColumns => this._dayNameHeaderColumns;

  /// <summary>Gets the height of the day name headers.</summary>
  public virtual int DayNameHeadersHeight
  {
    get
    {
      if (this._dayNameHeadersHeight == 0)
        this._dayNameHeadersHeight = this.DayHeaderHeight;
      return this._dayNameHeadersHeight;
    }
  }

  /// <summary>
  /// Gets a value indicating if the day names headers are visible (e.g. Monday, Tuesday, Wednesday ...).
  /// </summary>
  public bool DayNameHeadersVisible => this.Calendar.DaysMode == CalendarDaysMode.Short;

  /// <summary>Gets the current height of the all day items area.</summary>
  public virtual int DayTopHeight
  {
    get
    {
      if (this._dayTopHeight == 0)
        this._dayTopHeight = this.DayTopMinHeight;
      return this._dayTopHeight;
    }
    set => this._dayTopHeight = value;
  }

  /// <summary>
  /// Gets or sets the padding of the items that goes on the top part of the days,
  /// when in <see cref="F:Intermech.Client.Core.Organizer.CalendarDaysMode.Expanded" />.
  /// </summary>
  public int DayTopItemsPadding
  {
    get => this._allDayItemsPadding;
    set => this._allDayItemsPadding = value;
  }

  /// <summary>Gets the minimum height for day tops.</summary>
  public virtual int DayTopMinHeight
  {
    get
    {
      if (this._dayTopMinHeight == 0)
        this._dayTopMinHeight = TextRenderer.MeasureText("Ag", this.Calendar.Font).Height + 16 /*0x10*/;
      return this._dayTopMinHeight;
    }
  }

  /// <summary>
  /// Gets or sets the extra margin for invalidating and redrawing items.
  /// </summary>
  public int ItemInvalidateMargin
  {
    get => this._itemInvalidateMargin;
    set => this._itemInvalidateMargin = value;
  }

  /// <summary>Gets or sets the roundness of the item.</summary>
  public int ItemRoundness
  {
    get => this._itemRoundness;
    set => this._itemRoundness = value;
  }

  /// <summary>
  /// Gets or sets the amount of pixels that the item's shadow is dropped.
  /// </summary>
  public virtual int ItemShadowPadding
  {
    get => this._itemShadowPadding;
    set => this._itemShadowPadding = value;
  }

  /// <summary>Gets or sets the padding of items on expanded mode.</summary>
  public int ItemsPadding
  {
    get => this._itemsPadding;
    set => this._itemsPadding = value;
  }

  /// <summary>Gets the margin of the text in the items.</summary>
  public virtual Padding ItemTextMargin
  {
    get => this._itemTextMargin;
    set => this._itemTextMargin = value;
  }

  /// <summary>Gets the height of items on day tops.</summary>
  public virtual int StandardItemHeight
  {
    get
    {
      if (this._standardItemHeight == 0)
        this._standardItemHeight = TextRenderer.MeasureText("Ag", this.Calendar.Font).Height;
      return this._standardItemHeight + this.ItemTextMargin.Vertical;
    }
  }

  /// <summary>Gets or sets the bounds of the timescale.</summary>
  public Rectangle TimeScaleBounds
  {
    get => this._timeScaleBounds;
    set => this._timeScaleBounds = value;
  }

  /// <summary>Gets the height of the rows on of the timescale.</summary>
  public virtual int TimeScaleUnitHeight
  {
    get
    {
      if (this._timeScaleUnitHeight == 0)
        this._timeScaleUnitHeight = TextRenderer.MeasureText("Ag", this.Calendar.Font).Height + 10;
      return this._timeScaleUnitHeight;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public bool TimeScaleVisible => this.Calendar.DaysMode == CalendarDaysMode.Expanded;

  /// <summary>Gets the width of the timescale.</summary>
  public virtual int TimeScaleWidth
  {
    get
    {
      if (this._timeScaleWidth == 0)
        this._timeScaleWidth = 60;
      return this._timeScaleWidth;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public virtual int WeekHeaderWidth
  {
    get
    {
      if (this._weekHeaderWidth == 0)
        this._weekHeaderWidth = TextRenderer.MeasureText("Ag", this.Calendar.Font).Height + 4;
      return this._weekHeaderWidth;
    }
  }

  /// <summary>Creates a new renderer for the specified calendar.</summary>
  /// <param name="calendar"></param>
  public CalendarRenderer(Scheduler calendar)
  {
    this._calendar = calendar != null ? calendar : throw new ArgumentNullException(nameof (calendar));
  }

  /// <summary>
  /// Gets the amout of units that can be displayed on the calendar viewport.
  /// </summary>
  internal int GetVisibleTimeUnits()
  {
    if (this.Calendar.DaysMode == CalendarDaysMode.Short)
      return Convert.ToInt32(this.Calendar.ViewEnd.Subtract(this.Calendar.ViewStart).TotalDays / 7.0);
    return this.Calendar.Days != null && this.Calendar.Days.Length != 0 ? Convert.ToInt32(Math.Floor((double) Convert.ToSingle(this.Calendar.Days[0].BodyBounds.Height) / (double) Convert.ToSingle(this.TimeScaleUnitHeight))) : 0;
  }

  /// <summary>
  /// Recursive method that collects items intersecting on time, to graphically represent-them on the layout.
  /// </summary>
  /// <param name="calendarItem"></param>
  /// <param name="items"></param>
  /// <param name="grouped"></param>
  private void CollectIntersectingGroup(
    CalendarItem calendarItem,
    List<CalendarItem> items,
    List<CalendarItem> grouped)
  {
    if (!grouped.Contains(calendarItem))
      grouped.Add(calendarItem);
    foreach (CalendarItem calendarItem1 in items)
    {
      if (!grouped.Contains(calendarItem1))
      {
        CalendarItem calendarItem2 = calendarItem;
        DateTime dateTime = calendarItem1.StartDate;
        TimeSpan timeOfDay1 = dateTime.TimeOfDay;
        dateTime = calendarItem1.EndDate;
        TimeSpan timeOfDay2 = dateTime.TimeOfDay;
        if (calendarItem2.IntersectsWith(timeOfDay1, timeOfDay2))
        {
          grouped.Add(calendarItem1);
          this.CollectIntersectingGroup(calendarItem1, items, grouped);
        }
      }
    }
  }

  /// <summary>
  /// Outs the location of the specified number in the matrix.
  /// </summary>
  /// <param name="m">Matrix to search in</param>
  /// <param name="number">Number to find</param>
  /// <param name="left">Result left</param>
  /// <param name="top">Result top</param>
  private void FindInMatrix(int[,] m, int number, out int left, out int top)
  {
    for (int index1 = 0; index1 < m.GetLength(1); ++index1)
    {
      for (int index2 = 0; index2 < m.GetLength(0); ++index2)
      {
        if (m[index2, index1] == number)
        {
          left = index2;
          top = index1;
          return;
        }
      }
    }
    left = top = -1;
  }

  /// <summary>
  /// Outs the startIndex and the endIndex of units in the group.
  /// </summary>
  /// <param name="group"></param>
  /// <param name="startIndex"></param>
  /// <param name="endIndex"></param>
  private void GetGroupBoundUnits(List<CalendarItem> group, out int startIndex, out int endIndex)
  {
    startIndex = int.MaxValue;
    endIndex = int.MinValue;
    foreach (CalendarItem calendarItem in group)
    {
      foreach (SchedulerTimeScaleUnit schedulerTimeScaleUnit in calendarItem.UnitsPassing)
      {
        startIndex = Math.Min(startIndex, schedulerTimeScaleUnit.Index);
        endIndex = Math.Max(endIndex, schedulerTimeScaleUnit.Index);
      }
    }
  }

  /// <summary>Prints the specified matrix on debug.</summary>
  /// <param name="m"></param>
  private void PrintMatrix(int[,] m)
  {
    Console.WriteLine("--------------------------------");
    for (int index1 = 0; index1 < m.GetLength(1); ++index1)
    {
      for (int index2 = 0; index2 < m.GetLength(0); ++index2)
        Console.Write($" {m[index2, index1]}");
      Console.WriteLine(" ");
    }
    Console.WriteLine("--------------------------------");
  }

  /// <summary>
  /// Draws text using the information of the <see cref="T:Intermech.Client.Core.Organizer.CalendarRendererBoxEventArgs" />.
  /// </summary>
  /// <param name="e"></param>
  protected virtual void DrawStandarBoxText(CalendarRendererBoxEventArgs e)
  {
    TextFormatFlags flags = e.Format | TextFormatFlags.LeftAndRightPadding;
    TextRenderer.DrawText((IDeviceContext) e.Graphics, e.Text, e.Font, e.Bounds, e.TextColor, flags);
  }

  /// <summary>
  /// Sets the value of the <see cref="P:Intermech.Client.Core.Organizer.CalendarRenderer.DayHeaderHeight" /> property.
  /// </summary>
  /// <param name="height">Height of the day header</param>
  protected void SetDayHeaderHeight(int height) => this._dayHeaderHeight = height;

  /// <summary>
  /// Sets the value of the <see cref="P:Intermech.Client.Core.Organizer.CalendarRenderer.DayNameHeadersHeight" /> property.
  /// </summary>
  /// <param name="height">Height of the day name headers</param>
  protected void SetDayNameHeadersHeight(int height) => this._dayNameHeadersHeight = height;

  /// <summary>
  /// Sets the value of the <see cref="P:Intermech.Client.Core.Organizer.CalendarRenderer.DayTopHeight" /> property.
  /// </summary>
  /// <param name="height">Height of all <see cref="T:Intermech.Client.Core.Organizer.CalendarDayTop" /> elements</param>
  protected void SetDayTopHeight(int height) => this._dayTopHeight = height;

  /// <summary>
  /// Sets the value of the <see cref="P:Intermech.Client.Core.Organizer.CalendarRenderer.TimeScaleUnitHeight" /> property.
  /// </summary>
  /// <param name="height">Height of the time scale unit</param>
  protected void SetTimeScaleUnitHeight(int height) => this._timeScaleUnitHeight = height;

  /// <summary>
  /// Sets the value of the <see cref="P:Intermech.Client.Core.Organizer.CalendarRenderer.TimeScaleWidth" /> property.
  /// </summary>
  /// <param name="width">New width for the time scale</param>
  protected void SetTimeScaleWidth(int width) => this._timeScaleWidth = width;

  /// <summary>
  /// Gets the exact Y coordinate that corresponds to the specified time.
  /// This only works when is in <c>Expanded</c> mode.
  /// </summary>
  /// <param name="time">Time of day to get Y coordinate</param>
  /// <returns>Y coordinate corresponding to the specified <para>time</para></returns>
  /// <exception cref="T:System.InvalidOperationException">When calendar is not in <c>Expaned</c> mode.</exception>
  public int GetTimeY(TimeSpan time)
  {
    if (this.Calendar.DaysMode != CalendarDaysMode.Expanded)
      throw new InvalidOperationException("Can't measure Time's Y when calendar isn't in Expanded mode");
    if (this.Calendar.Days == null || this.Calendar.Days.Length == 0)
      return 0;
    double num = Convert.ToDouble(this.Calendar.Days[0].TimeUnits[0].Duration.TotalMinutes);
    double totalMinutes = time.TotalMinutes;
    int int32_1 = Convert.ToInt32(Math.Floor(totalMinutes / num));
    double int32_2 = (double) Convert.ToInt32(Math.Floor(totalMinutes % num));
    SchedulerTimeScaleUnit timeUnit = this.Calendar.Days[0].TimeUnits[int32_1];
    return timeUnit.Bounds.Top + Convert.ToInt32(Convert.ToDouble(timeUnit.Bounds.Height) / num) * Convert.ToInt32(int32_2);
  }

  /// <summary>
  /// Draws the specified rectangle with item border roundness.
  /// </summary>
  /// <param name="e"></param>
  /// <param name="bounds"></param>
  /// <param name="color"></param>
  /// <param name="width"></param>
  /// <returns></returns>
  public void ItemBorder(
    CalendarRendererItemBoundsEventArgs e,
    Rectangle bounds,
    Color color,
    float width)
  {
    using (GraphicsPath path = this.ItemRectangle(e, bounds))
    {
      using (Pen pen = new Pen(color, width))
        e.Graphics.DrawPath(pen, path);
    }
  }

  /// <summary>
  /// Fills the specified rectangle with item border roundness.
  /// </summary>
  /// <param name="e"></param>
  /// <param name="bounds"></param>
  /// <param name="north"></param>
  /// <param name="south"></param>
  /// <returns></returns>
  public void ItemFill(
    CalendarRendererItemBoundsEventArgs e,
    Rectangle bounds,
    Color north,
    Color south)
  {
    if (bounds.Width <= 0 || bounds.Height <= 0)
      return;
    using (GraphicsPath path = this.ItemRectangle(e, bounds))
    {
      using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(bounds, north, south, 90f))
        e.Graphics.FillPath((Brush) linearGradientBrush, path);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  /// <param name="bounds"></param>
  /// <param name="patternColor"></param>
  public void ItemPattern(
    CalendarRendererItemBoundsEventArgs e,
    Rectangle bounds,
    Color patternColor)
  {
    if (bounds.Width <= 0 || bounds.Height <= 0)
      return;
    using (GraphicsPath path = this.ItemRectangle(e, bounds))
    {
      using (Brush brush = (Brush) new HatchBrush(e.Item.Pattern, patternColor, Color.Transparent))
        e.Graphics.FillPath(brush, path);
    }
  }

  /// <summary>Creates a rectangle with item roundess.</summary>
  /// <param name="evtData"></param>
  /// <param name="bounds"></param>
  /// <returns></returns>
  public GraphicsPath ItemRectangle(CalendarRendererItemBoundsEventArgs evtData, Rectangle bounds)
  {
    int num1 = 5;
    if ((evtData.Item.Bounds.Top != evtData.Item.MinuteStartTop || evtData.Item.Bounds.Bottom != evtData.Item.MinuteEndTop) && evtData.Item.MinuteEndTop != 0 && evtData.Item.MinuteStartTop != 0 && !evtData.Item.IsOnDayTop && evtData.Calendar.DaysMode == CalendarDaysMode.Expanded)
    {
      int num2 = this.ItemRoundness * 2;
      Point point1 = new Point(bounds.Left, evtData.Item.MinuteStartTop);
      Point point2 = new Point(point1.X + num1, point1.Y);
      Point point3 = new Point(point2.X, bounds.Top);
      Point point4 = new Point(bounds.Right, point3.Y);
      Point point5 = new Point(point4.X, bounds.Bottom);
      Point point6 = new Point(point2.X, point5.Y);
      Point point7 = new Point(point2.X, evtData.Item.MinuteEndTop);
      Point point8 = new Point(point1.X, point7.Y);
      GraphicsPath graphicsPath = new GraphicsPath();
      graphicsPath.AddLine(point1, point2);
      graphicsPath.AddLine(point2, point3);
      graphicsPath.AddLine(point3, new Point(point4.X - num2, point4.Y));
      graphicsPath.AddArc(new Rectangle(point4.X - num2, point4.Y, num2, num2), -90f, 90f);
      graphicsPath.AddLine(new Point(point4.X, point4.Y + num2), new Point(point4.X, point5.Y - num2));
      graphicsPath.AddArc(new Rectangle(point5.X - num2, point5.Y - num2, num2, num2), 0.0f, 90f);
      graphicsPath.AddLine(new Point(point5.X - num2, point5.Y), point6);
      graphicsPath.AddLine(point6, point7);
      graphicsPath.AddLine(point7, point8);
      graphicsPath.AddLine(point8, point1);
      graphicsPath.CloseFigure();
      return graphicsPath;
    }
    CalendarRenderer.Corners corners = CalendarRenderer.Corners.None;
    if (evtData.IsFirst)
      corners |= CalendarRenderer.Corners.West;
    if (evtData.IsLast)
      corners |= CalendarRenderer.Corners.East;
    return CalendarRenderer.RoundRectangle(bounds, this.ItemRoundness, corners);
  }

  /// <summary>Updates the bounds of CalendarItems.</summary>
  public void PerformItemsLayout()
  {
    if (this.Calendar.Days == null || this.Calendar.Items.Count == 0)
      return;
    bool flag1 = false;
    Math.Abs(this.Calendar.TimeUnitsOffset);
    List<CalendarItem> calendarItemList = new List<CalendarItem>();
    foreach (CalendarDay day in this.Calendar.Days)
    {
      day.ContainedItems.Clear();
      day.DayTop.PassingItems.Clear();
      day.ClearTimeUnits();
    }
    if (this.Calendar.DaysMode == CalendarDaysMode.Expanded)
    {
      int val1 = 0;
      foreach (CalendarItem calendarItem in (List<CalendarItem>) this.Calendar.Items)
      {
        calendarItem.ClearBounds();
        calendarItem.ClearPassings();
        if (calendarItem.IsOnDayTop)
        {
          CalendarDay calendarDay1 = calendarItem.DayStart;
          CalendarDay calendarDay2 = calendarItem.DayEnd;
          if (calendarDay1 == null)
            calendarDay1 = this.Calendar.Days[0];
          if (calendarDay2 == null)
          {
            calendarDay2 = this.Calendar.Days[this.Calendar.Days.Length - 1];
            DateTime dateTime = calendarItem.EndDate;
            DateTime date1 = dateTime.Date;
            dateTime = calendarDay2.Date;
            DateTime date2 = dateTime.Date;
            if (date1 < date2)
            {
              foreach (CalendarDay day in this.Calendar.Days)
              {
                dateTime = calendarItem.EndDate;
                DateTime date3 = dateTime.Date;
                dateTime = day.Date;
                DateTime date4 = dateTime.Date;
                if (!(date3 < date4))
                  calendarDay2 = day;
                else
                  break;
              }
            }
          }
          for (int index = 0; index < this.Calendar.Days.Length; ++index)
          {
            if (!(this.Calendar.Days[index].Date < calendarDay1.Date))
            {
              if (!(this.Calendar.Days[index].Date > calendarDay2.Date))
              {
                calendarItem.AddTopPassing(this.Calendar.Days[index].DayTop);
                this.Calendar.Days[index].DayTop.AddPassingItem(calendarItem);
              }
              else
                break;
            }
          }
          calendarItem.Bounds = Rectangle.FromLTRB(calendarDay1.DayTop.Bounds.Left, 0, calendarDay2.DayTop.Bounds.Right, 1);
        }
        else
        {
          CalendarDay dayStart = calendarItem.DayStart;
          if (dayStart != null)
          {
            double num = Convert.ToDouble((int) this.Calendar.TimeScale);
            DateTime startDate = calendarItem.StartDate;
            DateTime endDate = calendarItem.EndDate;
            TimeSpan timeOfDay = startDate.TimeOfDay;
            int int32_1 = Convert.ToInt32(Math.Floor(timeOfDay.TotalMinutes / num));
            timeOfDay = endDate.TimeOfDay;
            int int32_2 = Convert.ToInt32(Math.Ceiling(timeOfDay.TotalMinutes / num));
            for (int index = 0; index < dayStart.TimeUnits.Length; ++index)
            {
              if (index >= int32_1 && index < int32_2)
              {
                dayStart.TimeUnits[index].AddPassingItem(calendarItem);
                calendarItem.AddUnitPassing(dayStart.TimeUnits[index]);
              }
            }
            calendarItem.Bounds = Rectangle.Empty;
            calendarItemList.Add(calendarItem);
          }
        }
      }
      foreach (CalendarDay day in this.Calendar.Days)
        val1 = Math.Max(val1, day.DayTop.PassingItems.Count);
      int[,] m1 = new int[this.Calendar.Days.Length, val1];
      if (m1.GetLength(1) > 0)
      {
        for (int index1 = 0; index1 < this.Calendar.Items.Count; ++index1)
        {
          CalendarItem calendarItem = this.Calendar.Items[index1];
          if (calendarItem.IsOnDayTop)
          {
            calendarItem.TopsPassing.Sort(new Comparison<CalendarDayTop>(CalendarRenderer.CompareTops));
            int startX = 0;
            for (int index2 = 0; index2 < this.Calendar.Days.Length; ++index2)
            {
              if (calendarItem.TopsPassing[0].Day == this.Calendar.Days[index2])
              {
                startX = index2;
                break;
              }
            }
            int endX = 0;
            for (int index3 = 0; index3 < this.Calendar.Days.Length; ++index3)
            {
              if (calendarItem.TopsPassing[calendarItem.TopsPassing.Count - 1].Day == this.Calendar.Days[index3])
              {
                endX = index3;
                break;
              }
            }
            this.PlaceInMatrix(ref m1, index1 + 1, startX, endX);
          }
        }
        int num = m1.GetLength(1) * this.StandardItemHeight + this.DayTopMinHeight;
        this.Calendar.ScrollBar.Location.Offset(0, m1.GetLength(1) * this.StandardItemHeight);
        if (this.DayTopHeight != num)
        {
          this.DayTopHeight = num;
          flag1 = true;
        }
        int standardItemHeight = this.StandardItemHeight;
        for (int index = 0; index < this.Calendar.Items.Count; ++index)
        {
          CalendarItem calendarItem = this.Calendar.Items[index];
          if (calendarItem.IsOnDayTop)
          {
            int top;
            this.FindInMatrix(m1, index + 1, out int _, out top);
            Rectangle bounds = calendarItem.Bounds with
            {
              Y = this.Calendar.Days[0].DayTop.Bounds.Top + top * standardItemHeight,
              Height = standardItemHeight
            };
            calendarItem.Bounds = bounds;
            calendarItem.FirstAndLastRectangleGapping();
          }
        }
      }
      if (flag1)
        this.PerformLayout(false);
      foreach (CalendarDay day in this.Calendar.Days)
      {
        val1 = Math.Max(val1, day.DayTop.PassingItems.Count);
        List<List<CalendarItem>> calendarItemListList = new List<List<CalendarItem>>();
        List<CalendarItem> items = new List<CalendarItem>((IEnumerable<CalendarItem>) day.ContainedItems);
        while (items.Count > 0)
        {
          List<CalendarItem> grouped = new List<CalendarItem>();
          this.CollectIntersectingGroup(items[0], items, grouped);
          calendarItemListList.Add(grouped);
          foreach (CalendarItem calendarItem in grouped)
            items.Remove(calendarItem);
        }
        foreach (List<CalendarItem> group in calendarItemListList)
        {
          int val2 = 0;
          int startIndex;
          int endIndex;
          this.GetGroupBoundUnits(group, out startIndex, out endIndex);
          for (int index = startIndex; index <= endIndex; ++index)
            val2 = Math.Max(day.TimeUnits[index].PassingItems.Count, val2);
          int[,] m2 = new int[val2, endIndex - startIndex + 1];
          foreach (CalendarItem calendarItem in group)
          {
            int index4 = 0;
            calendarItem.UnitsPassing.Sort(new Comparison<SchedulerTimeScaleUnit>(CalendarRenderer.CompareUnits));
            int num1 = calendarItem.UnitsPassing[0].Index - startIndex;
            int num2 = num1 + calendarItem.UnitsPassing.Count - 1;
            bool flag2 = false;
            while (!flag2)
            {
              flag2 = true;
              for (int index5 = num1; index5 <= num2; ++index5)
              {
                if (m2[index4, index5] != 0)
                {
                  flag2 = false;
                  break;
                }
              }
              if (!flag2)
                ++index4;
            }
            for (int index6 = num1; index6 <= num2; ++index6)
              m2[index4, index6] = group.IndexOf(calendarItem) + 1;
          }
          foreach (CalendarItem calendarItem in group)
          {
            int num3 = group.IndexOf(calendarItem);
            int count = calendarItem.UnitsPassing.Count;
            int num4 = 1;
            int left;
            int top;
            this.FindInMatrix(m2, num3 + 1, out left, out top);
            bool flag3 = left >= 0 && top >= 0;
            while (flag3)
            {
              for (int index = top; index < top + count; ++index)
              {
                if (m2.GetLength(0) <= left + num4 || m2[left + num4, index] != 0)
                {
                  flag3 = false;
                  break;
                }
              }
              if (flag3)
              {
                for (int index = top; index < top + count; ++index)
                  m2[left + num4, index] = num3 + 1;
                ++num4;
              }
            }
          }
          Rectangle bounds = day.Bounds;
          int int32 = Convert.ToInt32(Math.Floor((double) Convert.ToSingle(bounds.Width - this.ItemsPadding) / (double) Convert.ToSingle(m2.GetLength(0))));
          foreach (CalendarItem calendarItem1 in group)
          {
            int num5 = group.IndexOf(calendarItem1);
            int num6 = 1;
            int left1;
            int top1;
            this.FindInMatrix(m2, num5 + 1, out left1, out top1);
            if (left1 >= 0 && top1 >= 0)
            {
              for (int index = left1 + 1; index < m2.GetLength(0) && m2[index, top1] == num5 + 1; ++index)
                ++num6;
            }
            bounds = day.TimeUnits[calendarItem1.UnitsPassing[0].Index].Bounds;
            int top2 = bounds.Top;
            bounds = day.TimeUnits[calendarItem1.UnitsPassing[calendarItem1.UnitsPassing.Count - 1].Index].Bounds;
            int bottom = bounds.Bottom;
            bounds = day.Bounds;
            int left2 = bounds.Left + left1 * int32;
            int right = left2 + int32 * num6;
            calendarItem1.Bounds = Rectangle.FromLTRB(left2, top2, right, bottom);
            CalendarItem calendarItem2 = calendarItem1;
            DateTime dateTime = calendarItem1.StartDate;
            int timeY1 = this.GetTimeY(dateTime.TimeOfDay);
            calendarItem2.MinuteStartTop = timeY1;
            CalendarItem calendarItem3 = calendarItem1;
            dateTime = calendarItem1.EndDate;
            int timeY2 = this.GetTimeY(dateTime.TimeOfDay);
            calendarItem3.MinuteEndTop = timeY2;
          }
        }
      }
    }
    else if (this.Calendar.DaysMode == CalendarDaysMode.Short)
    {
      this.Calendar.Items.Reverse();
      for (int index = 0; index < this.Calendar.Days.Length; ++index)
      {
        this.Calendar.Days[index].ContainedItems.Clear();
        this.Calendar.Days[index].SetOverflowEnd(false);
        this.Calendar.Days[index].SetOverflowStart(false);
      }
      int val1 = 0;
      foreach (CalendarItem calendarItem in (List<CalendarItem>) this.Calendar.Items)
      {
        CalendarDay dayStart = calendarItem.DayStart;
        CalendarDay dayEnd = calendarItem.DayEnd;
        if (dayStart == null || dayEnd == null)
          return;
        calendarItem.ClearBounds();
        for (int index = dayStart.Index; index <= dayEnd.Index; ++index)
        {
          this.Calendar.Days[index].AddContainedItem(calendarItem);
          val1 = Math.Max(val1, this.Calendar.Days[index].ContainedItems.Count);
        }
      }
      int[,] m3 = new int[this.Calendar.Days.Length, val1];
      int num7 = 0;
      foreach (CalendarItem calendarItem in (List<CalendarItem>) this.Calendar.Items)
      {
        CalendarDay dayStart = calendarItem.DayStart;
        CalendarDay dayEnd = calendarItem.DayEnd;
        this.PlaceInMatrix(ref m3, num7 + 1, dayStart.Index, dayEnd.Index);
        ++num7;
      }
      for (int index7 = 0; index7 < this.Calendar.Weeks.Length; ++index7)
      {
        int num8 = index7 * 7;
        int num9 = 0;
        int[,] m4 = new int[7, m3.GetLength(1)];
        CalendarDay day1 = this.Calendar.FindDay(this.Calendar.Weeks[index7].StartDate);
        for (int index8 = 0; index8 < m4.GetLength(1); ++index8)
        {
          for (int index9 = 0; index9 < m4.GetLength(0); ++index9)
            m4[index9, index8] = m3[index9 + num8, index8];
        }
        foreach (CalendarItem calendarItem in (List<CalendarItem>) this.Calendar.Items)
        {
          int num10 = 0;
          int left;
          int top3;
          this.FindInMatrix(m4, ++num9, out left, out top3);
          if (left >= 0 && top3 >= 0)
          {
            for (int index10 = left; index10 < m4.GetLength(0) && m4[index10, top3] == num9; ++index10)
              ++num10;
            CalendarDay day2 = this.Calendar.Days[num8 + left];
            CalendarDay day3 = this.Calendar.Days[num8 + left + num10 - 1];
            Rectangle bounds1 = day2.Bounds;
            Rectangle bounds2 = day3.Bounds;
            int top4 = bounds1.Top + this.DayHeaderHeight + top3 * this.StandardItemHeight;
            Rectangle r = Rectangle.FromLTRB(bounds1.Left, top4, bounds2.Right, top4 + this.StandardItemHeight);
            if (r.Bottom <= day1.Bounds.Bottom)
            {
              calendarItem.AddBounds(r);
            }
            else
            {
              for (int index11 = day2.Index; index11 <= day3.Index; ++index11)
                this.Calendar.Days[index11].SetOverflowEnd(true);
            }
          }
        }
      }
      foreach (CalendarItem calendarItem in (List<CalendarItem>) this.Calendar.Items)
        calendarItem.FirstAndLastRectangleGapping();
      this.Calendar.Items.Reverse();
    }
    this.Calendar.RaiseItemsPositioned();
  }

  /// <summary>Peform layout of elements and items of the calendar.</summary>
  public void PerformLayout() => this.PerformLayout(true);

  /// <summary>
  /// Updates the bounds of graphical elements.
  /// Optionally calls <see cref="M:Intermech.Client.Core.Organizer.CalendarRenderer.PerformItemsLayout" /> to update bounds of items.
  /// </summary>
  /// <remarks>
  /// This method is called every time the <see cref="P:Intermech.Client.Core.Organizer.CalendarRenderer.Calendar" /> control is resized.
  /// </remarks>
  public void PerformLayout(bool performItemsLayout)
  {
    if (this.Calendar.Days == null)
      return;
    int y1 = this.Calendar.Header.Height * 2 + 16 /*0x10*/;
    int width1 = this.Calendar.ScrollBar.Width;
    this.TimeScaleBounds = Rectangle.Empty;
    if (this.Calendar.DaysMode == CalendarDaysMode.Expanded)
    {
      int timeScaleWidth = this.TimeScaleWidth;
      Rectangle rectangle1 = this.Calendar.ClientRectangle;
      int height1 = rectangle1.Height;
      this.TimeScaleBounds = new Rectangle(0, 0, timeScaleWidth, height1);
      rectangle1 = this.TimeScaleBounds;
      int right = rectangle1.Right;
      int height2 = this.Calendar.ClientSize.Height - 1 - y1;
      int width2 = (this.Calendar.ClientSize.Width - 2 - this.TimeScaleBounds.Width - width1) / this.Calendar.Days.Length - 1;
      VScrollBar scrollBar = this.Calendar.ScrollBar;
      Rectangle rectangle2 = this.Calendar.ClientRectangle;
      Point point = new Point(rectangle2.Width - width1 - 1, y1 + this.DayHeaderHeight + this.DayTopHeight);
      scrollBar.Location = point;
      this.Calendar.ScrollBar.Height = height2 - this.DayHeaderHeight - this.DayTopHeight;
      for (int index1 = 0; index1 < this.Calendar.Days.Length; ++index1)
      {
        CalendarDay day = this.Calendar.Days[index1];
        day.Bounds = new Rectangle(right, y1, width2, height2);
        CalendarDayTop dayTop = day.DayTop;
        int x = right;
        rectangle2 = day.HeaderBounds;
        int bottom = rectangle2.Bottom;
        int width3 = width2;
        int dayTopHeight = this.DayTopHeight;
        Rectangle rectangle3 = new Rectangle(x, bottom, width3, dayTopHeight);
        dayTop.Bounds = rectangle3;
        right += width2 + 1;
        rectangle2 = day.BodyBounds;
        int num = rectangle2.Top + this.Calendar.TimeUnitsOffset * this.TimeScaleUnitHeight;
        for (int index2 = 0; index2 < day.TimeUnits.Length; ++index2)
        {
          SchedulerTimeScaleUnit timeUnit = day.TimeUnits[index2];
          timeUnit.Visible = this.Calendar.TimeUnitsOffset * -1 < index2 + 1;
          SchedulerTimeScaleUnit schedulerTimeScaleUnit = timeUnit;
          rectangle2 = day.Bounds;
          int left = rectangle2.Left;
          int y2 = num;
          rectangle2 = day.Bounds;
          int width4 = rectangle2.Width;
          int timeScaleUnitHeight = this.TimeScaleUnitHeight;
          Rectangle rectangle4 = new Rectangle(left, y2, width4, timeScaleUnitHeight);
          schedulerTimeScaleUnit.Bounds = rectangle4;
          num += this.TimeScaleUnitHeight;
        }
      }
      int visibleTimeUnits = this.GetVisibleTimeUnits();
      if (this.Calendar.Days != null && this.Calendar.Days.Length != 0 && this.Calendar.Days[0].TimeUnits != null)
      {
        int num = this.Calendar.Days[0].TimeUnits.Length - visibleTimeUnits;
        if (num > 0)
        {
          this.Calendar.ScrollBar.Maximum = num;
          if (Math.Abs(this.Calendar.TimeUnitsOffset) > this.Calendar.ScrollBar.Maximum)
            this.Calendar.TimeUnitsOffset = 0;
          this.Calendar.ScrollBar.Value = num;
        }
        this.Calendar.ScrollBar.Visible = num > 0;
      }
    }
    else
    {
      int weekHeaderWidth = this.WeekHeaderWidth;
      int x = weekHeaderWidth;
      int y3 = this.DayNameHeadersHeight + y1;
      int height = (this.Calendar.ClientSize.Height - y3) / (this.Calendar.Days.Length / 7) - 1;
      int width5 = (this.Calendar.ClientSize.Width - 2 - weekHeaderWidth - width1) / 7 - 1;
      this.Calendar.ScrollBar.Location = new Point(this.Calendar.ClientRectangle.Width - width1 - 1, y3 - this.DayNameHeadersHeight);
      this.Calendar.ScrollBar.Height = this.Calendar.ClientSize.Height - (y3 - this.DayNameHeadersHeight) - 1;
      this._dayNameHeaderColumns = new Rectangle[7];
      int num1 = 0;
      for (int index = 0; index < this.Calendar.Days.Length; ++index)
      {
        this.Calendar.Days[index].Bounds = new Rectangle(x, y3, width5, height);
        if (index < this._dayNameHeaderColumns.Length)
          this._dayNameHeaderColumns[index] = new Rectangle(x, y3 - this.DayNameHeadersHeight, width5, this.DayNameHeadersHeight);
        x += width5 + 1;
        if (this.Calendar.Days[index].Date.DayOfWeek == this.Calendar.FirstDayOfWeek)
          this.Calendar.Weeks[num1++].SetBounds(new Rectangle(0, y3, this.Calendar.ClientSize.Width, height));
        if ((index + 1) % 7 == 0)
        {
          y3 += height + 1;
          x = weekHeaderWidth;
        }
      }
      int visibleTimeUnits = this.GetVisibleTimeUnits();
      DateTime dateTime1;
      ref DateTime local1 = ref dateTime1;
      DateTime dateTime2 = DateTime.Now;
      dateTime2 = dateTime2.AddYears(-1);
      int year1 = dateTime2.Year;
      local1 = new DateTime(year1, 1, 1);
      DateTime dateTime3;
      ref DateTime local2 = ref dateTime3;
      dateTime2 = DateTime.Now;
      dateTime2 = dateTime2.AddYears(1);
      int year2 = dateTime2.Year;
      local2 = new DateTime(year2, 12, 31 /*0x1F*/);
      this.Calendar.ScrollBar.Maximum = Convert.ToInt32(dateTime3.Subtract(dateTime1).TotalDays / 7.0) - 1 - visibleTimeUnits;
      dateTime2 = this.Calendar.ViewStart;
      int num2 = Convert.ToInt32(dateTime2.Subtract(dateTime1).TotalDays / 7.0) - 1;
      if (num2 > -1 && num2 <= this.Calendar.ScrollBar.Maximum)
        this.Calendar.ScrollBar.Value = num2;
    }
    if (!performItemsLayout)
      return;
    this.PerformItemsLayout();
  }

  /// <summary>
  /// Places the specified item in the matrix for the layout engine purposes.
  /// </summary>
  /// <param name="m"></param>
  /// <param name="index"></param>
  /// <param name="startX"></param>
  /// <param name="endX"></param>
  private void PlaceInMatrix(ref int[,] m, int index, int startX, int endX)
  {
    int index1 = 0;
    bool flag = false;
    while (!flag && index1 < m.GetLength(1))
    {
      flag = true;
      for (int index2 = startX; index2 <= endX; ++index2)
      {
        if (index2 >= 0 && index2 < m.GetLength(0) && m[index2, index1] != 0)
        {
          flag = false;
          break;
        }
      }
      if (!flag)
        ++index1;
    }
    if (index1 >= m.GetLength(1))
      return;
    for (int index3 = startX; index3 <= endX; ++index3)
      m[index3, index1] = index;
  }

  /// <summary>Initializes the Calendar.</summary>
  /// <param name="e"></param>
  public virtual void OnInitialize(CalendarRendererEventArgs e)
  {
  }

  /// <summary>Paints the background of the calendar.</summary>
  /// <param name="e">Paint info</param>
  public virtual void OnDrawBackground(CalendarRendererEventArgs e)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  public virtual void OnDrawCaption(CalendarRendererEventArgs e)
  {
  }

  /// <summary>Paints the specified day on the calendar.</summary>
  /// <param name="e">Paint info</param>
  public virtual void OnDrawDay(CalendarRendererDayEventArgs e)
  {
    CalendarDay day = e.Day;
    CalendarRendererBoxEventArgs e1 = new CalendarRendererBoxEventArgs((CalendarRendererEventArgs) e, day.HeaderBounds, day.Date.Day.ToString(), TextFormatFlags.VerticalCenter);
    e1.Font = new Font(this.Calendar.Font, FontStyle.Regular);
    CalendarRendererBoxEventArgs e2 = new CalendarRendererBoxEventArgs((CalendarRendererEventArgs) e, day.HeaderBounds, day.Date.ToString(e.Format, (IFormatProvider) CultureInfo.CurrentUICulture), TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
    this.OnDrawDayHeaderBackground(e);
    if (this.Calendar.DaysMode == CalendarDaysMode.Short)
    {
      DateTime date;
      if (day.Index != 0)
      {
        date = day.Date;
        if (date.Day != 1)
          goto label_4;
      }
      CalendarRendererBoxEventArgs rendererBoxEventArgs = e1;
      date = day.Date;
      string str = date.ToString("dd MMM", (IFormatProvider) CultureInfo.CurrentUICulture);
      rendererBoxEventArgs.Text = str;
    }
label_4:
    this.OnDrawDayHeaderText(e1);
    if (e2.TextSize.Width < day.HeaderBounds.Width - e1.TextSize.Width * 2 && e.Calendar.DaysMode == CalendarDaysMode.Expanded)
      this.OnDrawDayHeaderText(e2);
    this.OnDrawDayTimeUnits(e);
    this.OnDrawDayTop(e);
    this.OnDrawDayBorder(e);
  }

  /// <summary>Paints the border of the specified day.</summary>
  /// <param name="e"></param>
  public virtual void OnDrawDayBorder(CalendarRendererDayEventArgs e)
  {
  }

  /// <summary>Paints the background of the specified day's header.</summary>
  /// <param name="e"></param>
  public virtual void OnDrawDayHeaderBackground(CalendarRendererDayEventArgs e)
  {
  }

  /// <summary>Paints the header of the specified day.</summary>
  /// <param name="e">Paint info</param>
  public virtual void OnDrawDayHeaderText(CalendarRendererBoxEventArgs e)
  {
    this.DrawStandarBoxText(e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e">Paint info</param>
  public virtual void OnDrawDayNameHeader(CalendarRendererBoxEventArgs e)
  {
    this.DrawStandarBoxText(e);
  }

  /// <summary>Paints the name of the day columns when is.</summary>
  /// <param name="e">Paint Info</param>
  public virtual void OnDrawDayNameHeaders(CalendarRendererEventArgs e)
  {
    DateTime dateTime = DateTime.Now.AddDays((double) (-((int) DateTime.Now.DayOfWeek % 7) + 1));
    int num = 0;
    string format = "dddd";
    for (int index = 0; index < this.DayNameHeaderColumns.Length; ++index)
    {
      Size size = TextRenderer.MeasureText(dateTime.AddDays((double) index).ToString("dddd", (IFormatProvider) CultureInfo.CurrentUICulture), e.Calendar.Font);
      if (num <= size.Width)
        num = size.Width;
    }
    if (this.DayNameHeaderColumns.Length != 0)
      format = this.DayNameHeaderColumns[0].Width < num ? "ddd" : "dddd";
    for (int index = 0; index < this.DayNameHeaderColumns.Length; ++index)
      this.OnDrawDayNameHeader(new CalendarRendererBoxEventArgs(e, this.DayNameHeaderColumns[index], dateTime.AddDays((double) index).ToString(format, (IFormatProvider) CultureInfo.CurrentUICulture), TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter));
  }

  /// <summary>Draws the overflow to end of specified day.</summary>
  /// <param name="e"></param>
  public virtual void OnDrawDayOverflowEnd(CalendarRendererDayEventArgs e)
  {
  }

  /// <summary>Draws the overflow to start of specified day.</summary>
  /// <param name="e">Event data</param>
  public virtual void OnDrawDayOverflowStart(CalendarRendererDayEventArgs e)
  {
  }

  /// <summary>Paints the days on the current calendar view.</summary>
  /// <param name="e">Paint Info</param>
  public virtual void OnDrawDays(CalendarRendererEventArgs e)
  {
    if (e.Calendar.Days.Length == 0)
      return;
    int num1 = 0;
    using (Font font1 = new Font(e.Calendar.Font, FontStyle.Regular))
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      foreach (CalendarDay day in e.Calendar.Days)
      {
        string text1 = day.Date.Day.ToString();
        string text2 = day.Date.ToString("dddd", (IFormatProvider) CultureInfo.CurrentUICulture);
        Size size1 = TextRenderer.MeasureText(text1, font1);
        Font font2 = e.Calendar.Font;
        Size size2 = TextRenderer.MeasureText(text2, font2);
        int num2 = size1.Width * 2 + size2.Width + 1;
        if (num1 <= num2)
          num1 = num2;
      }
    }
    string format = num1 > e.Calendar.Days[0].HeaderBounds.Width ? "ddd" : "dddd";
    for (int index = 0; index < e.Calendar.Days.Length; ++index)
    {
      CalendarDay day = e.Calendar.Days[index];
      if (!e.Calendar.ExcludedDays.ContainsKey(day.Date.Month) || !e.Calendar.ExcludedDays[day.Date.Month].Contains(day.Date.Day))
      {
        e.Tag = (object) day;
        this.OnDrawDay(new CalendarRendererDayEventArgs(e, day, format));
      }
    }
  }

  /// <summary>Draws a time unit of a day.</summary>
  /// <param name="e"></param>
  public virtual void OnDrawDayTimeUnit(CalendarRendererTimeUnitEventArgs e)
  {
  }

  /// <summary>
  /// Paints the body of the day. Usually timeline unit indicator lines or a solid color if.
  /// </summary>
  /// <param name="e"></param>
  public virtual void OnDrawDayTimeUnits(CalendarRendererDayEventArgs e)
  {
    for (int index = 0; index < e.Day.TimeUnits.Length; ++index)
    {
      SchedulerTimeScaleUnit timeUnit = e.Day.TimeUnits[index];
      if (timeUnit.Visible)
        this.OnDrawDayTimeUnit(new CalendarRendererTimeUnitEventArgs((CalendarRendererEventArgs) e, timeUnit));
    }
  }

  /// <summary>Draws the all day items area.</summary>
  /// <param name="e">Paint Info</param>
  public virtual void OnDrawDayTop(CalendarRendererDayEventArgs e)
  {
  }

  /// <summary>Draws an item of the calendar.</summary>
  /// <param name="e">Event Info</param>
  public virtual void OnDrawItem(CalendarRendererItemEventArgs e)
  {
    List<Rectangle> rectangleList = new List<Rectangle>(e.Item.GetAllBounds());
    for (int index = 0; index < rectangleList.Count; ++index)
    {
      CalendarRendererItemBoundsEventArgs e1 = new CalendarRendererItemBoundsEventArgs(e, rectangleList[index], index == 0 && !e.Item.IsOpenStart, index == rectangleList.Count - 1 && !e.Item.IsOpenEnd);
      this.OnDrawItemShadow(e1);
      this.OnDrawItemBackground(e1);
      if (!e1.Item.PatternColor.IsEmpty)
        this.OnDrawItemPattern(e1);
      if (!e.Item.IsEditing)
        this.OnDrawItemContent(e1);
      SmoothingMode smoothingMode = e.Graphics.SmoothingMode;
      e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
      this.OnDrawItemBorder(e1);
      e.Graphics.SmoothingMode = smoothingMode;
    }
  }

  /// <summary>Draws the background of the specified item.</summary>
  /// <param name="e">Event Info</param>
  public virtual void OnDrawItemBackground(CalendarRendererItemBoundsEventArgs e)
  {
  }

  /// <summary>Draws the border of the specified item.</summary>
  /// <param name="e">Event Info</param>
  public virtual void OnDrawItemBorder(CalendarRendererItemBoundsEventArgs e)
  {
  }

  /// <summary>
  /// Draws the strings of an item. Strings inlude StartTime, EndTime and Text.
  /// </summary>
  /// <param name="e">Event Info</param>
  public virtual void OnDrawItemContent(CalendarRendererItemBoundsEventArgs e)
  {
    if (e.Item == e.Calendar.EditModeItem)
      return;
    Color foreColor = e.Item.ForeColor;
    Rectangle bounds1 = e.Bounds;
    if (!e.Item.BaseItem)
    {
      if (bounds1.Width <= 13)
        return;
      int x = bounds1.Right - 11;
      int y = bounds1.Bottom - 10;
      this.OnDrawRepetitionImage(new CalendarRendererItemBoundsEventArgs((CalendarRendererItemEventArgs) e, new Rectangle(x, y, 9, 8), false, false));
    }
    Rectangle empty = Rectangle.Empty;
    Padding itemTextMargin;
    if (e.Item.Image != null)
    {
      int width = bounds1.Width;
      itemTextMargin = this.ItemTextMargin;
      int num = 16 /*0x10*/ + itemTextMargin.Left;
      if (width <= num)
        return;
      ref Rectangle local = ref empty;
      int x1 = bounds1.X;
      itemTextMargin = this.ItemTextMargin;
      int left = itemTextMargin.Left;
      int x2 = x1 + left;
      int y1 = bounds1.Y;
      itemTextMargin = this.ItemTextMargin;
      int top = itemTextMargin.Top;
      int y2 = y1 + top;
      local = new Rectangle(x2, y2, 16 /*0x10*/, 16 /*0x10*/);
      this.OnDrawItemImage(new CalendarRendererItemBoundsEventArgs((CalendarRendererItemEventArgs) e, empty, false, false));
    }
    Point location;
    ref Point local1 = ref location;
    int right1 = empty.Right;
    itemTextMargin = this.ItemTextMargin;
    int left1 = itemTextMargin.Left;
    int x3 = right1 + left1;
    int y3 = empty.Y;
    local1 = new Point(x3, y3);
    Size size1;
    ref Size local2 = ref size1;
    int num1 = bounds1.Right - location.X;
    itemTextMargin = this.ItemTextMargin;
    int right2 = itemTextMargin.Right;
    int width1 = num1 - right2;
    int height1 = bounds1.Height;
    itemTextMargin = this.ItemTextMargin;
    int vertical = itemTextMargin.Vertical;
    int height2 = height1 - vertical;
    local2 = new Size(width1, height2);
    string caption = e.Item.Caption;
    string text1 = caption.Substring(0, caption.Length > 13 ? 13 : caption.Length);
    string text2 = caption.Substring(0, caption.Length > 3 ? 3 : caption.Length);
    Size size2 = TextRenderer.MeasureText(caption, e.Calendar.Font);
    Size size3 = TextRenderer.MeasureText(text1, e.Calendar.Font);
    Font font = e.Calendar.Font;
    if (TextRenderer.MeasureText(text2, font).Width > size1.Width)
      return;
    Rectangle bounds2 = new Rectangle(location, size1);
    Rectangle bounds3 = Rectangle.Empty;
    Rectangle bounds4 = Rectangle.Empty;
    bool flag1 = false;
    bool flag2 = false;
    Size size4 = new Size(0, 0);
    Size size5 = new Size(0, 0);
    if (e.Item.ShowStartTime)
    {
      Size size6 = TextRenderer.MeasureText(e.Item.StartDateText, e.Calendar.Font);
      ref Point local3 = ref location;
      int right3 = empty.Right;
      itemTextMargin = this.ItemTextMargin;
      int left2 = itemTextMargin.Left;
      int x4 = right3 + left2;
      int y4 = bounds2.Y;
      local3 = new Point(x4, y4);
      size1 = new Size(size6.Width, bounds2.Height);
      itemTextMargin = this.ItemTextMargin;
      int num2 = itemTextMargin.Left + size1.Width;
      if (flag1 = bounds2.Width - num2 >= size3.Width)
      {
        bounds3 = new Rectangle(location, size1);
        bounds2.X += num2;
        bounds2.Width -= num2;
      }
    }
    if (e.Item.ShowEndTime)
    {
      Size size7 = TextRenderer.MeasureText(e.Item.EndDateText, e.Calendar.Font);
      ref Point local4 = ref location;
      int right4 = bounds1.Right;
      itemTextMargin = this.ItemTextMargin;
      int right5 = itemTextMargin.Right;
      int x5 = right4 - right5 - size7.Width;
      int y5 = bounds2.Y;
      local4 = new Point(x5, y5);
      size1 = new Size(size7.Width, bounds2.Height);
      itemTextMargin = this.ItemTextMargin;
      int num3 = itemTextMargin.Right + size1.Width;
      flag2 = bounds2.Width - num3 >= size3.Width;
      if (flag2)
      {
        bounds4 = new Rectangle(location, size1);
        bounds2.Width -= num3;
      }
      else if (flag1)
      {
        if (e.Item.IsOpenStart)
        {
          flag1 = false;
          flag2 = true;
          itemTextMargin = this.ItemTextMargin;
          int num4 = itemTextMargin.Left + bounds3.Width;
          bounds2.X -= num4;
          bounds2.Width += num4;
          bounds4 = new Rectangle(location, size1);
          ref Rectangle local5 = ref bounds2;
          int width2 = local5.Width;
          itemTextMargin = this.ItemTextMargin;
          int num5 = itemTextMargin.Right + size1.Width;
          local5.Width = width2 - num5;
        }
        else if (!e.Item.IsOpenEnd)
        {
          flag1 = false;
          itemTextMargin = this.ItemTextMargin;
          int num6 = itemTextMargin.Left + bounds3.Width;
          bounds2.X -= num6;
          bounds2.Width += num6;
        }
      }
    }
    CalendarRendererBoxEventArgs e1 = new CalendarRendererBoxEventArgs((CalendarRendererEventArgs) e, bounds2, e.Item.Caption, TextFormatFlags.Default);
    if ((e.Item.IsOnDayTop || this.Calendar.DaysMode == CalendarDaysMode.Short) && bounds2.Width > size2.Width)
      e1.Format |= TextFormatFlags.HorizontalCenter;
    e1.TextColor = !e.Item.ForeColor.IsEmpty ? e.Item.ForeColor : e1.TextColor;
    e1.Tag = (object) e.Item;
    this.OnDrawItemText(e1);
    if (flag1)
      this.OnDrawItemStartTime(new CalendarRendererBoxEventArgs((CalendarRendererEventArgs) e, bounds3, e.Item.StartDateText, foreColor));
    if (!flag2)
      return;
    this.OnDrawItemEndTime(new CalendarRendererBoxEventArgs((CalendarRendererEventArgs) e, bounds4, e.Item.EndDateText, foreColor));
  }

  /// <summary>Draws the end time of the item if applicable.</summary>
  /// <param name="e">Event data</param>
  public virtual void OnDrawItemEndTime(CalendarRendererBoxEventArgs e)
  {
    this.DrawStandarBoxText(e);
  }

  /// <summary>Draws the image of an item.</summary>
  /// <param name="e"></param>
  public virtual void OnDrawItemImage(CalendarRendererItemBoundsEventArgs e)
  {
    if (e.Item.Image == null)
      return;
    e.Graphics.DrawImage(e.Item.Image, e.Bounds);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  public virtual void OnDrawItemPattern(CalendarRendererItemBoundsEventArgs e)
  {
    foreach (Rectangle allBound in e.Item.GetAllBounds())
      this.ItemPattern(e, allBound, e.Item.PatternColor);
  }

  /// <summary>Draws the items of the calendar.</summary>
  /// <param name="e">Event info</param>
  public virtual void OnDrawItems(CalendarRendererEventArgs e)
  {
    Rectangle daysBodyRectangle = e.Calendar.DaysBodyRectangle;
    daysBodyRectangle.Inflate(-1, -1);
    Region clip = e.Graphics.Clip;
    bool flag1 = e.Calendar.DaysMode == CalendarDaysMode.Expanded;
    bool flag2 = false;
    foreach (CalendarItem calendarItem in (List<CalendarItem>) e.Calendar.Items)
    {
      bool flag3 = false;
      if (flag1 && !calendarItem.IsOnDayTop && calendarItem.Bounds.Top < daysBodyRectangle.Top)
      {
        e.Graphics.SetClip(daysBodyRectangle, CombineMode.Intersect);
        flag3 = true;
      }
      List<Rectangle> rectangleList = new List<Rectangle>(calendarItem.GetAllBounds());
      for (int index = 0; index < rectangleList.Count; ++index)
        this.OnDrawItemShadow(new CalendarRendererItemBoundsEventArgs(new CalendarRendererItemEventArgs(e, calendarItem), rectangleList[index], index == 0 && !calendarItem.IsOpenStart, index == rectangleList.Count - 1 && !calendarItem.IsOpenEnd));
      if (flag3)
        e.Graphics.SetClip(clip, CombineMode.Replace);
    }
    foreach (CalendarItem calendarItem in (List<CalendarItem>) e.Calendar.Items)
    {
      bool flag4 = false;
      if (flag1 && !calendarItem.IsOnDayTop && calendarItem.Bounds.Top < daysBodyRectangle.Top)
      {
        e.Graphics.SetClip(daysBodyRectangle, CombineMode.Intersect);
        flag4 = true;
      }
      this.OnDrawItem(new CalendarRendererItemEventArgs(e, calendarItem));
      if (flag4)
        e.Graphics.SetClip(clip, CombineMode.Replace);
    }
    foreach (CalendarItem calendarItem in (List<CalendarItem>) e.Calendar.Items)
    {
      if (calendarItem.Selected && calendarItem.BaseItem)
      {
        flag2 = false;
        if (flag1 && !calendarItem.IsOnDayTop && calendarItem.Bounds.Top < daysBodyRectangle.Top)
        {
          e.Graphics.SetClip(daysBodyRectangle, CombineMode.Intersect);
          flag2 = true;
        }
        List<Rectangle> rectangleList = new List<Rectangle>(calendarItem.GetAllBounds());
        for (int index = 0; index < rectangleList.Count; ++index)
        {
          CalendarRendererItemBoundsEventArgs e1 = new CalendarRendererItemBoundsEventArgs(new CalendarRendererItemEventArgs(e, calendarItem), rectangleList[index], index == 0 && !calendarItem.IsOpenStart, index == rectangleList.Count - 1 && !calendarItem.IsOpenEnd);
          SmoothingMode smoothingMode = e.Graphics.SmoothingMode;
          e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
          this.OnDrawItemBorder(e1);
          e.Graphics.SmoothingMode = smoothingMode;
        }
      }
    }
  }

  /// <summary>Draws the shadow of the specified item.</summary>
  /// <param name="e"></param>
  public virtual void OnDrawItemShadow(CalendarRendererItemBoundsEventArgs e)
  {
  }

  /// <summary>Draws the starttime of the item if applicable.</summary>
  /// <param name="e">Event data</param>
  public virtual void OnDrawItemStartTime(CalendarRendererBoxEventArgs e)
  {
    this.DrawStandarBoxText(e);
  }

  /// <summary>Draws the text of an item.</summary>
  /// <param name="e"></param>
  public virtual void OnDrawItemText(CalendarRendererBoxEventArgs e) => this.DrawStandarBoxText(e);

  /// <summary>Draws the overflows of days.</summary>
  /// <param name="e"></param>
  public virtual void OnDrawOverflows(CalendarRendererEventArgs e)
  {
    for (int index = 0; index < e.Calendar.Days.Length; ++index)
    {
      CalendarDay day = e.Calendar.Days[index];
      if (day.OverflowStart)
        this.OnDrawDayOverflowStart(new CalendarRendererDayEventArgs(e, day));
      if (day.OverflowEnd)
        this.OnDrawDayOverflowEnd(new CalendarRendererDayEventArgs(e, day));
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  public virtual void OnDrawRepetitionImage(CalendarRendererItemBoundsEventArgs e)
  {
    if (e == null || e.Item.Scheduler.RepetitionIco == null)
      return;
    e.Graphics.DrawImage((Image) e.Item.Scheduler.RepetitionIco.ToBitmap(), e.Bounds);
  }

  /// <summary>Paints the timescale of the calendar.</summary>
  /// <param name="e">Paint info</param>
  public virtual void OnDrawTimeScale(CalendarRendererEventArgs e)
  {
    if (e.Calendar.DaysMode == CalendarDaysMode.Short || e.Calendar.Days == null || e.Calendar.Days.Length == 0 || e.Calendar.Days[0].TimeUnits == null)
      return;
    using (Font font1 = new Font(e.Calendar.Font.FontFamily, e.Calendar.Font.Size * (e.Calendar.TimeScale == CalendarTimeScale.SixtyMinutes ? 1f : 1.5f)))
    {
      Font font2 = e.Calendar.Font;
      Rectangle rectangle = this.TimeScaleBounds;
      int num1 = rectangle.Left + 3;
      rectangle = this.TimeScaleBounds;
      int left = rectangle.Left;
      rectangle = this.TimeScaleBounds;
      int num2 = rectangle.Width / 2;
      int num3 = left + num2 + 7;
      int num4 = num3 - 7;
      int num5 = num3;
      int num6 = 0;
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      for (int index = 0; index < e.Calendar.Days[0].TimeUnits.Length; ++index)
      {
        SchedulerTimeScaleUnit timeUnit = e.Calendar.Days[0].TimeUnits[index];
        if (timeUnit.Visible)
        {
          string str1 = timeUnit.Hours.ToString("00");
          string str2 = timeUnit.Minutes == 0 ? "00" : string.Empty;
          if (!string.IsNullOrEmpty(str2))
          {
            if (str1 == "00")
              str1 = "12";
            CalendarRendererEventArgs original1 = e;
            int x1 = num1;
            rectangle = timeUnit.Bounds;
            int top1 = rectangle.Top;
            int width1 = num3;
            rectangle = timeUnit.Bounds;
            int height1 = rectangle.Height;
            Rectangle bounds1 = new Rectangle(x1, top1, width1, height1);
            string text1 = str1;
            this.OnDrawTimeScaleHour(new CalendarRendererBoxEventArgs(original1, bounds1, text1, TextFormatFlags.Right)
            {
              Font = font1
            });
            if (num6++ == 0 || timeUnit.Hours == 0 || timeUnit.Hours == 12)
              str2 = timeUnit.Date.ToString("tt");
            CalendarRendererEventArgs original2 = e;
            int x2 = num4;
            rectangle = timeUnit.Bounds;
            int top2 = rectangle.Top;
            int width2 = num5;
            rectangle = timeUnit.Bounds;
            int height2 = rectangle.Height;
            Rectangle bounds2 = new Rectangle(x2, top2, width2, height2);
            string text2 = str2;
            this.OnDrawTimeScaleMinutes(new CalendarRendererBoxEventArgs(original2, bounds2, text2, TextFormatFlags.Default)
            {
              Font = font2
            });
          }
        }
      }
    }
  }

  /// <summary>Paints an hour of a timescale unit.</summary>
  /// <param name="e">Paint Info</param>
  public virtual void OnDrawTimeScaleHour(CalendarRendererBoxEventArgs e)
  {
    this.DrawStandarBoxText(e);
  }

  /// <summary>Paints minutes of a timescale unit.</summary>
  /// <param name="e">Paint Info</param>
  public virtual void OnDrawTimeScaleMinutes(CalendarRendererBoxEventArgs e)
  {
    this.DrawStandarBoxText(e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e">Paint info</param>
  public virtual void OnDrawWeekHeader(CalendarRendererBoxEventArgs e)
  {
    StringFormat format = new StringFormat();
    format.FormatFlags = StringFormatFlags.DirectionRightToLeft | StringFormatFlags.DirectionVertical | StringFormatFlags.NoWrap;
    format.LineAlignment = StringAlignment.Center;
    format.Alignment = StringAlignment.Center;
    using (SolidBrush solidBrush = new SolidBrush(e.TextColor))
      e.Graphics.DrawString(e.Text, e.Font, (Brush) solidBrush, (RectangleF) e.Bounds, format);
    e.Graphics.ResetTransform();
    format.Dispose();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  public virtual void OnDrawWeekHeaders(CalendarRendererEventArgs e)
  {
    if (this.Calendar.Weeks == null)
      return;
    for (int index = 0; index < this.Calendar.Weeks.Length; ++index)
    {
      string text = this.Calendar.Weeks[index].ToStringLarge();
      if (TextRenderer.MeasureText(text, e.Calendar.Font).Width > this.Calendar.Weeks[index].HeaderBounds.Height)
        text = this.Calendar.Weeks[index].ToStringShort();
      this.OnDrawWeekHeader(new CalendarRendererBoxEventArgs(e, this.Calendar.Weeks[index].HeaderBounds, text, TextFormatFlags.Default));
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [Flags]
  public enum Corners
  {
    None = 0,
    NorthWest = 2,
    NorthEast = 4,
    SouthEast = 8,
    SouthWest = 16, // 0x00000010
    All = SouthWest | SouthEast | NorthEast | NorthWest, // 0x0000001E
    North = NorthEast | NorthWest, // 0x00000006
    South = SouthWest | SouthEast, // 0x00000018
    East = SouthEast | NorthEast, // 0x0000000C
    West = SouthWest | NorthWest, // 0x00000012
  }
}
