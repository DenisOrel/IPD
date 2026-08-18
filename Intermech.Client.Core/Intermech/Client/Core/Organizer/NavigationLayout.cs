
// Type: Intermech.Client.Core.Organizer.NavigationLayout
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.Organizer;

[TypeConverter(typeof (ExpandableObjectConverter))]
[ToolboxItem(false)]
public class NavigationLayout : Component
{
  private NavigationBar _bar;
  private NavigationCollapseButton _collapseBtn = new NavigationCollapseButton();
  private NavigationCollapsedBand _collapsedBand = new NavigationCollapsedBand();
  private BarRenderer _barRenderer = new BarRenderer();
  private SplitterRenderer _splitterRenderer = new SplitterRenderer();
  private Rectangle _headerRect = Rectangle.Empty;
  private Rectangle _headerTextRect = Rectangle.Empty;
  private Rectangle _footerRect = Rectangle.Empty;
  private Rectangle _splitterRect = Rectangle.Empty;
  private Font _headerFont = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
  private int _splitterHeight = 8;
  private int _splitterPosition = 8;
  private int _visibleBtns;
  private int _expandedBarWidth;
  private bool _splitterDragging;

  /// <summary>
  /// 
  /// </summary>
  public NavigationBar Bar
  {
    get => this._bar;
    set
    {
      this._bar = value;
      if (this._collapseBtn != null && !this._bar.Controls.Contains((Control) this._collapseBtn))
        this._bar.Controls.Add((Control) this._collapseBtn);
      if (this._collapsedBand == null || this._bar.Controls.Contains((Control) this._collapsedBand))
        return;
      this._bar.Controls.Add((Control) this._collapsedBand);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public int VisibleButtons => this._visibleBtns;

  /// <summary>Конструктор.</summary>
  public NavigationLayout()
  {
    this._collapseBtn.Click += new EventHandler(this.On_collapseBtn_Click);
  }

  /// <summary>
  /// 
  /// </summary>
  private void On_collapseBtn_Click(object sender, EventArgs e)
  {
    this._bar.MinimumSize = !this._bar.IsCollapsed ? new Size(33, 310) : new Size(200, 310);
    this._bar.IsCollapsed = !this._bar.IsCollapsed;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="disposing"></param>
  protected override void Dispose(bool disposing)
  {
    base.Dispose(disposing);
    if (this._bar.Controls.Contains((Control) this._collapseBtn))
      this._bar.Controls.Remove((Control) this._collapseBtn);
    if (this._bar.Controls.Contains((Control) this._collapseBtn))
      this._bar.Controls.Remove((Control) this._collapseBtn);
    if (this._headerFont == null)
      return;
    this._headerFont.Dispose();
    this._headerFont = (Font) null;
  }

  /// <summary>
  /// 
  /// </summary>
  private void CalcBandsRegions()
  {
    foreach (NavigationBand band in (CollectionBase) this._bar.Bands)
    {
      band.Location = new Point(1, this._bar.HeaderHeight);
      band.Size = new Size(this._bar.Width - 2, this._bar.Height - this._bar.HeaderHeight - 2);
      if (band == this._bar.ActiveBand && !this._bar.IsCollapsed)
        band.BringToFront();
    }
    if (this._bar.IsCollapsed)
    {
      this._collapsedBand.Visible = true;
      this._collapsedBand.BringToFront();
      this._collapsedBand.Text = this._bar.ActiveBand.Text;
    }
    this._collapsedBand.Location = new Point(1, this._bar.HeaderHeight);
    this._collapsedBand.Size = new Size(this._bar.Width - 2, this._bar.Height - this._bar.HeaderHeight - 2);
  }

  /// <summary>
  /// 
  /// </summary>
  private void CalcButtonsRegion()
  {
    int num1 = 0;
    int num2 = this._splitterPosition + this._footerRect.Height + 1 - this._splitterHeight;
    int num3 = (this._visibleBtns - this._bar.LargeButtonsCount) * this._bar.SmallButtonWidth + 1;
    foreach (NavigationBand band in (CollectionBase) this._bar.Bands)
    {
      if (band.Button != null)
      {
        NavigationButton button = band.Button;
        if (button.Visible)
        {
          ++num1;
          if (this._bar.ActiveBand != null && this._bar.ActiveBand.Button == button && !button.Active)
            button.Active = true;
          if (num1 <= this._bar.LargeButtonsCount)
          {
            button.Location = new Point(1, this._bar.Height - num2);
            button.Height = this._bar.ButtonHeight;
            button.Width = this._bar.Width - 2;
            button.IsSmall = false;
            num2 -= this._bar.ButtonHeight;
          }
          else if (!this._bar.IsCollapsed)
          {
            button.IsSmall = true;
            button.Height = this._footerRect.Height;
            button.Width = this._bar.SmallButtonWidth;
            button.Location = new Point(this._bar.Width - num3, this._footerRect.Top);
            num3 -= this._bar.SmallButtonWidth;
          }
          else
          {
            button.Location = new Point(0, 0);
            button.Size = new Size(0, 0);
          }
        }
      }
    }
    this._collapseBtn.Size = new Size(this._bar.HeaderHeight, this._bar.HeaderHeight - 3);
    this._collapseBtn.Location = new Point(this._bar.Width - this._bar.HeaderHeight - 1, 2);
  }

  /// <summary>
  /// 
  /// </summary>
  private void CalcRegions()
  {
    this._headerRect = new Rectangle(new Point(1, 1), new Size(this._bar.Width - 2, this._bar.HeaderHeight - 1));
    this._footerRect = new Rectangle(1, this._bar.Height - this._bar.FooterHeight, this._bar.Width - 2, this._bar.FooterHeight - 1);
    this._headerTextRect = this._headerRect;
    this._headerTextRect.Offset(10, 0);
  }

  /// <summary>
  /// 
  /// </summary>
  private void CalcSplitterRegion()
  {
    this._splitterPosition = this._bar.ButtonHeight * this._bar.LargeButtonsCount + this._splitterHeight;
    this._splitterRect = new Rectangle(1, this._bar.Height - (this._splitterPosition + this._footerRect.Height + 1), this._bar.Width - 2, this._splitterHeight);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="ylocation"></param>
  private void DragSplitter(int ylocation)
  {
    int num = this._bar.Height - ylocation - this._footerRect.Height - this._splitterHeight;
    if (this._bar.LargeButtonsCount > this._visibleBtns)
      this._bar.LargeButtonsCount = this._visibleBtns;
    if (num > 0)
    {
      if (num > this._bar.ButtonHeight + this._bar.LargeButtonsCount * this._bar.ButtonHeight)
      {
        if (this._bar.LargeButtonsCount < this._visibleBtns)
          ++this._bar.LargeButtonsCount;
      }
      else if (num <= this._bar.ButtonHeight * this._bar.LargeButtonsCount - this._bar.ButtonHeight / 2 && this._bar.LargeButtonsCount > 0)
        --this._bar.LargeButtonsCount;
    }
    else
      this._bar.LargeButtonsCount = 0;
    this.Layout((object) this._bar, new LayoutEventArgs((Control) this._bar, "Splitter"));
    this._bar.Invalidate();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  private void HandleMouseDown(MouseEventArgs e)
  {
    if (e == null)
      return;
    this._splitterDragging = e.Button == MouseButtons.Left && e.Clicks == 1 && this.MouseInSplitter(e.X, e.Y);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  private void HandleMouseMove(MouseEventArgs e)
  {
    if (e == null)
      return;
    if (this._splitterDragging)
      this.DragSplitter(e.Y);
    this._bar.Cursor = this.MouseInSplitter(e.X, e.Y) || this._splitterDragging ? Cursors.SizeNS : Cursors.Default;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="x"></param>
  /// <param name="y"></param>
  /// <returns></returns>
  private bool MouseInSplitter(int x, int y)
  {
    return x > this._splitterRect.X && x < this._splitterRect.Right && y > this._splitterRect.Y && y < this._splitterRect.Bottom;
  }

  /// <summary>
  /// 
  /// </summary>
  private void ReCalculateButtonTotals()
  {
    this._visibleBtns = 0;
    foreach (Control button in (CollectionBase) this._bar.Buttons)
    {
      if (button.Visible)
        ++this._visibleBtns;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public void CheckButtonCount()
  {
    if (this._bar.LargeButtonsCount <= this._visibleBtns)
      return;
    this._bar.LargeButtonsCount = this._visibleBtns;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="g"></param>
  public void Draw(Graphics g)
  {
    if (this._bar.ActiveBand == null)
      return;
    this._barRenderer.DrawHeaderText(g, this._headerTextRect, this._bar.ActiveBand.Text, this._headerFont);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="g"></param>
  public void DrawBackground(Graphics g)
  {
    this._barRenderer.DrawBackground(g, this._bar.ClientRectangle);
    this._barRenderer.DrawSmallButtonRegion(g, this._footerRect);
    this._barRenderer.DrawHeader(g, this._headerRect);
    this._splitterRenderer.DrawBackground(g, this._splitterRect);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="container"></param>
  /// <param name="layoutEventArgs"></param>
  /// <returns></returns>
  public bool Layout(object container, LayoutEventArgs layoutEventArgs)
  {
    this.CalcRegions();
    this.ReCalculateButtonTotals();
    if (layoutEventArgs.AffectedProperty == "Band.Visible")
      this.CheckButtonCount();
    this.CalcButtonsRegion();
    this.CalcBandsRegions();
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="id"></param>
  /// <param name="arguments"></param>
  public void Notify(string id, object arguments)
  {
    switch (id)
    {
      case "MouseDown":
        this.HandleMouseDown(arguments as MouseEventArgs);
        break;
      case "MouseMove":
        this.HandleMouseMove(arguments as MouseEventArgs);
        break;
      case "MouseLeave":
        this._bar.Cursor = Cursors.Default;
        break;
      case "MouseUp":
        this._splitterDragging = false;
        break;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public void SwitchCollapsion(bool collapse, bool oldCollapsed)
  {
    if (collapse && !oldCollapsed)
    {
      this._expandedBarWidth = this._bar.Width;
      this._bar.Width = 33;
    }
    if (!collapse)
    {
      if (this._expandedBarWidth < 100)
        this._expandedBarWidth = 100;
      this._bar.Width = this._expandedBarWidth;
    }
    foreach (NavigationButton button in (CollectionBase) this._bar.Buttons)
      button.IsCollapsed = collapse;
    this._collapseBtn.IsCollapsed = collapse;
  }
}
