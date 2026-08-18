
// Type: Intermech.Client.Core.Organizer.CalendarProfessionalRenderer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// 
/// </summary>
public class CalendarProfessionalRenderer : CalendarSystemRenderer
{
  public Color HeaderA = Color.FromArgb(228, 236, 246);
  public Color HeaderB = Color.FromArgb(214, 226, 241);
  public Color HeaderC = Color.FromArgb(194, 212, 235);
  public Color HeaderD = Color.FromArgb(208 /*0xD0*/, 222, 239);
  public Color TodayA = Color.FromArgb(248, 212, 120);
  public Color TodayB = Color.FromArgb(248, 212, 120);
  public Color TodayC = Color.FromArgb(242, 170, 54);
  public Color TodayD = Color.FromArgb(247, 201, 102);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="g"></param>
  /// <param name="bounds"></param>
  /// <param name="a"></param>
  /// <param name="b"></param>
  /// <param name="c"></param>
  /// <param name="d"></param>
  public static void GlossyRect(
    Graphics g,
    Rectangle bounds,
    Color a,
    Color b,
    Color c,
    Color d)
  {
    Rectangle bounds1 = new Rectangle(bounds.Left, bounds.Top, bounds.Width, bounds.Height / 2);
    Rectangle bounds2 = Rectangle.FromLTRB(bounds.Left, bounds1.Bottom, bounds.Right, bounds.Bottom);
    CalendarProfessionalRenderer.GradientRect(g, bounds1, a, b);
    CalendarProfessionalRenderer.GradientRect(g, bounds2, c, d);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="g"></param>
  /// <param name="bounds"></param>
  /// <param name="a"></param>
  /// <param name="b"></param>
  public static void GradientRect(Graphics g, Rectangle bounds, Color a, Color b)
  {
    using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(bounds, b, a, -90f))
      g.FillRectangle((Brush) linearGradientBrush, bounds);
  }

  /// <summary>Конструктор.</summary>
  /// <param name="scheduler"></param>
  public CalendarProfessionalRenderer(Scheduler scheduler)
    : base(scheduler)
  {
    this.SelectedItemBorder = 1f;
    this.ItemRoundness = 5;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  public override void OnDrawDayHeaderBackground(CalendarRendererDayEventArgs e)
  {
    Rectangle headerBounds = e.Day.HeaderBounds;
    if (e.Day.Date == DateTime.Today)
      CalendarProfessionalRenderer.GlossyRect(e.Graphics, e.Day.HeaderBounds, this.TodayA, this.TodayB, this.TodayC, this.TodayD);
    else
      CalendarProfessionalRenderer.GlossyRect(e.Graphics, e.Day.HeaderBounds, this.HeaderA, this.HeaderB, this.HeaderC, this.HeaderD);
    if (e.Calendar.DaysMode != CalendarDaysMode.Short)
      return;
    using (Pen pen = new Pen(this.ColorTable.DayBorder))
    {
      e.Graphics.DrawLine(pen, headerBounds.Left, headerBounds.Top, headerBounds.Right, headerBounds.Top);
      e.Graphics.DrawLine(pen, headerBounds.Left, headerBounds.Bottom, headerBounds.Right, headerBounds.Bottom);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  public override void OnDrawItemBorder(CalendarRendererItemBoundsEventArgs e)
  {
    base.OnDrawItemBorder(e);
    using (Pen pen1 = new Pen(Color.FromArgb(150, Color.White)))
    {
      Graphics graphics = e.Graphics;
      Pen pen2 = pen1;
      int x1 = e.Bounds.Left + this.ItemRoundness;
      Rectangle bounds = e.Bounds;
      int y1 = bounds.Top + 1;
      bounds = e.Bounds;
      int x2 = bounds.Right - this.ItemRoundness;
      bounds = e.Bounds;
      int y2 = bounds.Top + 1;
      graphics.DrawLine(pen2, x1, y1, x2, y2);
    }
    if (!e.Item.Selected || e.Item.ReadOnly || e.Item.IsDragging)
      return;
    Rectangle rect1 = new Rectangle(0, 0, 5, 5);
    Rectangle rect2 = new Rectangle(0, 0, 5, 5);
    int num1 = e.Item.IsOnDayTop ? 1 : 0;
    bool flag = !e.Item.IsOnDayTop && e.Calendar.DaysMode == CalendarDaysMode.Expanded;
    if (num1 != 0)
    {
      rect1.X = e.Bounds.Left - 2;
      ref Rectangle local1 = ref rect2;
      Rectangle bounds = e.Bounds;
      int num2 = bounds.Right - rect1.Width + 2;
      local1.X = num2;
      ref Rectangle local2 = ref rect1;
      bounds = e.Bounds;
      int top = bounds.Top;
      bounds = e.Bounds;
      int num3 = (bounds.Height - rect1.Height) / 2;
      int num4 = top + num3;
      local2.Y = num4;
      rect2.Y = rect1.Y;
    }
    if (flag)
    {
      rect1.Y = e.Bounds.Top - 2;
      ref Rectangle local3 = ref rect2;
      Rectangle bounds = e.Bounds;
      int num5 = bounds.Bottom - rect1.Height + 2;
      local3.Y = num5;
      ref Rectangle local4 = ref rect1;
      bounds = e.Bounds;
      int left = bounds.Left;
      bounds = e.Bounds;
      int num6 = (bounds.Width - rect1.Width) / 2;
      int num7 = left + num6;
      local4.X = num7;
      rect2.X = rect1.X;
    }
    if ((num1 | (flag ? 1 : 0)) == 0 || !this.Calendar.AllowItemResize)
      return;
    if (!e.Item.IsOpenStart && e.IsFirst)
    {
      e.Graphics.FillRectangle(Brushes.White, rect1);
      e.Graphics.DrawRectangle(Pens.Black, rect1);
    }
    if (e.Item.IsOpenEnd || !e.IsLast)
      return;
    e.Graphics.FillRectangle(Brushes.White, rect2);
    e.Graphics.DrawRectangle(Pens.Black, rect2);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  public override void OnInitialize(CalendarRendererEventArgs e) => base.OnInitialize(e);
}
