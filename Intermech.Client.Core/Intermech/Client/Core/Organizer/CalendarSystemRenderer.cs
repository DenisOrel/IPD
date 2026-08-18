
// Type: Intermech.Client.Core.Organizer.CalendarSystemRenderer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// CalendarRenderer that renders low-intensity calendar for slow computers
/// </summary>
public class CalendarSystemRenderer : CalendarRenderer
{
  /// <summary>
  /// Gets or sets the <see cref="T:Intermech.Client.Core.Organizer.SchedulerColorTable" /> for this renderer.
  /// </summary>
  public SchedulerColorTable ColorTable { get; set; }

  /// <summary>
  /// Gets or sets the size of the border of selected items.
  /// </summary>
  public float SelectedItemBorder { get; set; }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="calendar"></param>
  public CalendarSystemRenderer(Scheduler calendar)
    : base(calendar)
  {
    this.ColorTable = new SchedulerColorTable();
    this.SelectedItemBorder = 1f;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  public override void OnDrawBackground(CalendarRendererEventArgs e)
  {
    e.Graphics.Clear(this.ColorTable.Background);
    using (Pen pen = new Pen(this.ColorTable.TimeScaleLine))
      e.Graphics.DrawRectangle(pen, 0, 0, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  public override void OnDrawCaption(CalendarRendererEventArgs e)
  {
    base.OnDrawCaption(e);
    using (Font font = new Font(e.Calendar.Font.FontFamily, 15f))
    {
      Size size = TextRenderer.MeasureText(e.Calendar.Caption, font);
      int num = 7;
      Rectangle bounds = new Rectangle(76, e.Calendar.Header.Height + num, size.Width, size.Height);
      TextRenderer.DrawText((IDeviceContext) e.Graphics, e.Calendar.Caption, font, bounds, this.ColorTable.DayHeaderText);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  public override void OnDrawDay(CalendarRendererDayEventArgs e)
  {
    Rectangle bounds = e.Day.Bounds;
    if (bounds.Width <= 0 || bounds.Height <= 0)
      return;
    Color color = this.ColorTable.DayBackgroundOdd;
    if (e.Day.Selected)
      color = this.ColorTable.DayBackgroundSelected;
    else if (e.Day.Date.Month % 2 == 0)
      color = this.ColorTable.DayBackgroundEven;
    using (Brush brush = (Brush) new SolidBrush(color))
      e.Graphics.FillRectangle(brush, bounds);
    base.OnDrawDay(e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  public override void OnDrawDayBorder(CalendarRendererDayEventArgs e)
  {
    base.OnDrawDayBorder(e);
    Rectangle bounds = e.Day.Bounds;
    DateTime date = e.Day.Date;
    date = date.Date;
    bool flag = date.Equals(DateTime.Today.Date);
    using (Pen pen = new Pen(flag ? this.ColorTable.TodayBorder : this.ColorTable.DayBorder, flag ? 2f : 1f))
    {
      if (e.Calendar.DaysMode == CalendarDaysMode.Short)
      {
        e.Graphics.DrawLine(pen, bounds.Right, bounds.Top, bounds.Right, bounds.Bottom);
        e.Graphics.DrawLine(pen, bounds.Left, bounds.Bottom, bounds.Right, bounds.Bottom);
        if (!(e.Day.Date.DayOfWeek == e.Calendar.FirstDayOfWeek | flag))
          return;
        e.Graphics.DrawLine(pen, bounds.Left, bounds.Top, bounds.Left, bounds.Bottom);
      }
      else
        e.Graphics.DrawRectangle(pen, bounds);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  public override void OnDrawDayTop(CalendarRendererDayEventArgs e)
  {
    bool selected = e.Day.DayTop.Selected;
    using (Brush brush = (Brush) new SolidBrush(selected ? this.ColorTable.DayTopSelectedBackground : this.ColorTable.DayTopBackground))
      e.Graphics.FillRectangle(brush, e.Day.DayTop.Bounds);
    using (Pen pen = new Pen(selected ? this.ColorTable.DayTopSelectedBorder : this.ColorTable.DayTopBorder))
      e.Graphics.DrawRectangle(pen, e.Day.DayTop.Bounds);
    base.OnDrawDayTop(e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  public override void OnDrawDayHeaderBackground(CalendarRendererDayEventArgs e)
  {
    DateTime date = e.Day.Date;
    date = date.Date;
    using (Brush brush = (Brush) new SolidBrush(date.Equals(DateTime.Today.Date) ? this.ColorTable.TodayTopBackground : this.ColorTable.DayHeaderBackground))
      e.Graphics.FillRectangle(brush, e.Day.HeaderBounds);
    base.OnDrawDayHeaderBackground(e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  public override void OnDrawWeekHeader(CalendarRendererBoxEventArgs e)
  {
    using (Brush brush = (Brush) new SolidBrush(this.ColorTable.WeekHeaderBackground))
      e.Graphics.FillRectangle(brush, e.Bounds);
    using (Pen pen = new Pen(this.ColorTable.WeekHeaderBorder))
      e.Graphics.DrawRectangle(pen, e.Bounds);
    e.TextColor = this.ColorTable.WeekHeaderText;
    base.OnDrawWeekHeader(e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  public override void OnDrawDayTimeUnit(CalendarRendererTimeUnitEventArgs e)
  {
    base.OnDrawDayTimeUnit(e);
    using (SolidBrush solidBrush = new SolidBrush(this.ColorTable.TimeUnitBackground))
    {
      if (e.Unit.Selected)
        solidBrush.Color = this.ColorTable.TimeUnitSelectedBackground;
      else if (e.Unit.Highlighted)
        solidBrush.Color = this.ColorTable.TimeUnitHighlightedBackground;
      e.Graphics.FillRectangle((Brush) solidBrush, e.Unit.Bounds);
    }
    using (Pen pen1 = new Pen(e.Unit.Minutes == 0 ? this.ColorTable.TimeUnitBorderDark : this.ColorTable.TimeUnitBorderLight))
    {
      Graphics graphics = e.Graphics;
      Pen pen2 = pen1;
      Rectangle bounds = e.Unit.Bounds;
      Point location = bounds.Location;
      bounds = e.Unit.Bounds;
      int right = bounds.Right;
      bounds = e.Unit.Bounds;
      int top = bounds.Top;
      Point pt2 = new Point(right, top);
      graphics.DrawLine(pen2, location, pt2);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  public override void OnDrawTimeScale(CalendarRendererEventArgs e)
  {
    int num1 = 5;
    int x1_1 = this.TimeScaleBounds.Left + num1;
    int x2_1 = this.TimeScaleBounds.Right - num1;
    Rectangle timeScaleBounds1 = this.TimeScaleBounds;
    int left1 = timeScaleBounds1.Left;
    timeScaleBounds1 = this.TimeScaleBounds;
    int num2 = timeScaleBounds1.Width / 2;
    int x1_2 = left1 + num2;
    int x2_2 = x2_1;
    Pen pen1 = new Pen(this.ColorTable.TimeScaleLine);
    for (int index = 0; index < e.Calendar.Days[0].TimeUnits.Length; ++index)
    {
      SchedulerTimeScaleUnit timeUnit = e.Calendar.Days[0].TimeUnits[index];
      if (timeUnit.Visible)
      {
        int top = timeUnit.Bounds.Top;
        if (timeUnit.Minutes == 0)
          e.Graphics.DrawLine(pen1, x1_1, top, x2_1, top);
        else
          e.Graphics.DrawLine(pen1, x1_2, top, x2_2, top);
      }
    }
    if (e.Calendar.DaysMode == CalendarDaysMode.Expanded)
    {
      CalendarDay[] days = e.Calendar.Days;
      if (days != null && days.Length != 0)
      {
        CalendarDay calendarDay = days[0];
        if (calendarDay.TimeUnits != null && calendarDay.TimeUnits.Length != 0)
        {
          int top = calendarDay.BodyBounds.Top;
          Graphics graphics = e.Graphics;
          Pen pen2 = pen1;
          Rectangle timeScaleBounds2 = this.TimeScaleBounds;
          int left2 = timeScaleBounds2.Left;
          int y1 = top;
          timeScaleBounds2 = this.TimeScaleBounds;
          int right = timeScaleBounds2.Right;
          int y2 = top;
          graphics.DrawLine(pen2, left2, y1, right, y2);
        }
      }
    }
    pen1.Dispose();
    base.OnDrawTimeScale(e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  public override void OnDrawTimeScaleHour(CalendarRendererBoxEventArgs e)
  {
    e.TextColor = this.ColorTable.TimeScaleHours;
    base.OnDrawTimeScaleHour(e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  public override void OnDrawTimeScaleMinutes(CalendarRendererBoxEventArgs e)
  {
    e.TextColor = this.ColorTable.TimeScaleMinutes;
    base.OnDrawTimeScaleMinutes(e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  public override void OnDrawItemBackground(CalendarRendererItemBoundsEventArgs e)
  {
    base.OnDrawItemBackground(e);
    int alpha = e.Item.IsDragging ? 120 : (e.Calendar.DaysMode == CalendarDaysMode.Short ? 200 : (int) byte.MaxValue);
    Color baseColor1 = !e.Item.BackgroundColorLighter.IsEmpty ? e.Item.BackgroundColorLighter : Color.White;
    Color baseColor2 = e.Item.Selected ? this.ColorTable.ItemSelectedBackground : this.ColorTable.ItemBackground;
    if (!e.Item.BackgroundColor.IsEmpty)
      baseColor2 = e.Item.BackgroundColor;
    this.ItemFill(e, e.Bounds, Color.FromArgb(alpha, baseColor1), Color.FromArgb(alpha, baseColor2));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  public override void OnDrawItemShadow(CalendarRendererItemBoundsEventArgs e)
  {
    base.OnDrawItemShadow(e);
    if (e.Item.IsOnDayTop || e.Calendar.DaysMode == CalendarDaysMode.Short || e.Item.IsDragging)
      return;
    Rectangle bounds = e.Bounds;
    bounds.Offset(this.ItemShadowPadding, this.ItemShadowPadding);
    using (new SolidBrush(this.ColorTable.ItemShadow))
      this.ItemFill(e, bounds, this.ColorTable.ItemShadow, this.ColorTable.ItemShadow);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  public override void OnDrawItemBorder(CalendarRendererItemBoundsEventArgs e)
  {
    base.OnDrawItemBorder(e);
    Color color1 = e.Item.BorderColor.IsEmpty ? this.ColorTable.ItemBorder : e.Item.BorderColor;
    Color baseColor = !e.Item.Selected || e.Item.IsDragging ? color1 : this.ColorTable.ItemSelectedBorder;
    Color color2 = Color.FromArgb(e.Item.IsDragging ? 120 : (int) byte.MaxValue, baseColor);
    this.ItemBorder(e, e.Bounds, color2, !e.Item.Selected || e.Item.IsDragging ? 1f : this.SelectedItemBorder);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  public override void OnDrawItemStartTime(CalendarRendererBoxEventArgs e)
  {
    if (e.TextColor.IsEmpty)
      e.TextColor = this.ColorTable.ItemSecondaryText;
    base.OnDrawItemStartTime(e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  public override void OnDrawItemEndTime(CalendarRendererBoxEventArgs e)
  {
    if (e.TextColor.IsEmpty)
      e.TextColor = this.ColorTable.ItemSecondaryText;
    base.OnDrawItemEndTime(e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  public override void OnDrawItemText(CalendarRendererBoxEventArgs e)
  {
    if (e.Tag is CalendarItem tag && tag.IsDragging)
      e.TextColor = Color.FromArgb(120, e.TextColor);
    base.OnDrawItemText(e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  public override void OnDrawWeekHeaders(CalendarRendererEventArgs e) => base.OnDrawWeekHeaders(e);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  public override void OnDrawDayNameHeader(CalendarRendererBoxEventArgs e)
  {
    e.TextColor = this.ColorTable.WeekDayName;
    base.OnDrawDayNameHeader(e);
    using (Pen pen = new Pen(this.ColorTable.WeekDayName))
      e.Graphics.DrawLine(pen, e.Bounds.Right, e.Bounds.Top, e.Bounds.Right, e.Bounds.Bottom);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  public override void OnDrawDayOverflowEnd(CalendarRendererDayEventArgs e)
  {
    using (GraphicsPath path = new GraphicsPath())
    {
      CalendarDay day = e.Day;
      Rectangle overflowEndBounds = day.OverflowEndBounds;
      int y = overflowEndBounds.Top + overflowEndBounds.Height / 2;
      path.AddPolygon(new Point[3]
      {
        new Point(overflowEndBounds.Left, y),
        new Point(overflowEndBounds.Right, y),
        new Point(overflowEndBounds.Left + overflowEndBounds.Width / 2, overflowEndBounds.Bottom)
      });
      using (Brush brush = (Brush) new SolidBrush(day.OverflowEndSelected ? this.ColorTable.DayOverflowSelectedBackground : this.ColorTable.DayOverflowBackground))
        e.Graphics.FillPath(brush, path);
      using (Pen pen = new Pen(this.ColorTable.DayOverflowBorder))
        e.Graphics.DrawPath(pen, path);
    }
  }
}
