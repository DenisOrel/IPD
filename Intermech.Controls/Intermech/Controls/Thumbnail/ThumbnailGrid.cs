
// Type: Intermech.Controls.Thumbnail.ThumbnailGrid
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Intermech.Controls.Thumbnail;

/// <summary>Summary description for ThumbnailGrid.</summary>
public class ThumbnailGrid : Control
{
  private VScrollBar _scrollBar;
  private const int WM_KEYDOWN = 256 /*0x0100*/;
  private const int WM_KEYUP = 257;
  private int _colCount;
  private int _visibleRows;
  private int _totalRows;
  private int _rowCount;
  private bool _fixedSize;
  private int _itemIndex;
  private int _oldIndex;
  private int _popupIndex;
  private Rectangle _oldBounds;
  private Rectangle _selectedBounds;
  private int _count;
  private int _panelHeight;
  private int _panelWidth;
  private int _panelSpacing;
  private IThumbnailRenderer _renderer;
  private bool _resizing;
  private bool _dragging;
  private StringFormat _sf;
  private Rectangle _resizeMarkBounds;
  private IContainer components;
  private ToolTip _resizeToolTip;
  private ToolTip _itemTtoolTip;
  private static Bitmap _resizeMark = new Bitmap(typeof (ThumbnailGrid).Assembly.GetManifestResourceStream("Intermech.Controls.Thumbnail.resizemark.bmp"));
  private Bitmap _shadow;
  private int _shadowSize;
  private bool _showShadow;

  public event ThumbnailEventHandler ItemDoubleClick;

  public event ThumbnailEventHandler ShowContextMenu;

  public event GetToolTipTextEventHandler GetToolTipText;

  public event SelectionChangedEventHandler SelectionChanged;

  public event EventHandler StopResize;

  public event EventHandler StartResize;

  /// <summary>
  /// 
  /// </summary>
  [DefaultValue(0)]
  public int Count
  {
    get => this._count;
    set
    {
      this._count = value;
      this.ItemIndex = this._count != 0 ? 0 : -1;
      this.UpdateLayout();
      this.Invalidate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [DefaultValue(true)]
  public bool FixedSize
  {
    get => this._fixedSize;
    set
    {
      if (this._fixedSize == value)
        return;
      this._fixedSize = value;
      this.UpdateLayout();
      this.Invalidate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public int ItemIndex
  {
    get => this._itemIndex;
    set
    {
      if (this._count == 0)
      {
        this._itemIndex = 0;
      }
      else
      {
        if (value < 0)
          value = 0;
        if (value > this._count - 1)
          value = this._count - 1;
        if (value == this.ItemIndex)
          return;
        this.OnSelectionChanged(this._itemIndex, value);
        this._itemIndex = value;
        if (this._scrollBar.Enabled)
        {
          this.EnsureVisible(this._itemIndex);
          this.Invalidate();
          this.Update();
        }
        else
        {
          this.Invalidate(this._selectedBounds);
          this.Invalidate(this.GetPanelBounds(this._itemIndex));
          this.Update();
        }
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [Browsable(false)]
  internal int ItemOffset => !this._scrollBar.Enabled ? 0 : this._scrollBar.Value * this._colCount;

  /// <summary>
  /// 
  /// </summary>
  [DefaultValue(120)]
  public int PanelHeight
  {
    get => this._panelHeight;
    set
    {
      if (this._panelHeight == value)
        return;
      this._panelHeight = value;
      this.UpdateLayout();
      this.Invalidate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [DefaultValue(8)]
  public int PanelSpacing
  {
    get => this._panelSpacing;
    set
    {
      if (this._panelSpacing == value)
        return;
      this._panelSpacing = value;
      this.UpdateLayout();
      this.Invalidate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [Browsable(false)]
  public Size PanelSize
  {
    get => new Size(this._panelWidth, this._panelHeight);
    set
    {
      bool flag = false;
      if (this._panelWidth != value.Width)
      {
        this._panelWidth = value.Width;
        flag = true;
      }
      if (this._panelHeight != value.Height)
      {
        this._panelHeight = value.Height;
        flag = true;
      }
      if (!flag)
        return;
      this.UpdateLayout();
      this.Invalidate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [DefaultValue(150)]
  public int PanelWidth
  {
    get => this._panelWidth;
    set
    {
      if (this._panelWidth == value)
        return;
      this._panelWidth = value;
      this.UpdateLayout();
      this.Invalidate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [RefreshProperties(RefreshProperties.Repaint)]
  public IThumbnailRenderer Renderer
  {
    get => this._renderer;
    set
    {
      if (this._renderer == value)
        return;
      if (this._renderer != null)
        this._renderer.RedrawRequired -= new RedrawEventHandler(this.Renderer_RedrawRequired);
      this._renderer = value;
      if (this._renderer != null)
        this._renderer.RedrawRequired += new RedrawEventHandler(this.Renderer_RedrawRequired);
      this.Invalidate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private bool Resizing
  {
    get => this._resizing;
    set
    {
      if (this._resizing == value)
        return;
      this._resizing = value;
      Rectangle resizeMarkBounds = this._resizeMarkBounds;
      resizeMarkBounds.Inflate(4, 4);
      this.Invalidate(resizeMarkBounds);
      this._resizeToolTip.Active = this._resizing;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private Rectangle RowsBounds
  {
    get
    {
      Rectangle clientRectangle = this.ClientRectangle;
      clientRectangle.Width -= this._scrollBar.Width;
      clientRectangle.Inflate(-1, -1);
      return clientRectangle;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [DefaultValue(6)]
  public int ShadowSize
  {
    get => this._shadowSize;
    set
    {
      if (this._shadowSize == value)
        return;
      this._shadowSize = value;
      this.CreateShadow();
      if (!this._showShadow)
        return;
      this.Invalidate();
      this.Update();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [DefaultValue(true)]
  public bool ShowShadow
  {
    get => this._showShadow;
    set
    {
      if (this._showShadow == value)
        return;
      this._showShadow = value;
      this.Invalidate();
      this.Update();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [Browsable(false)]
  internal int VisibleItemsCount => this._colCount * this._rowCount;

  /// <summary>
  /// 
  /// </summary>
  public ThumbnailGrid()
  {
    this._panelHeight = 120;
    this._panelWidth = 150;
    this._panelSpacing = 8;
    this._showShadow = true;
    this._shadowSize = 6;
    this._count = 0;
    this._itemIndex = 0;
    this._oldIndex = -1;
    this._resizing = false;
    this._fixedSize = true;
    this._resizeMarkBounds = Rectangle.Empty;
    this._sf = new StringFormat();
    this._sf.Alignment = StringAlignment.Center;
    this._sf.LineAlignment = StringAlignment.Center;
    this.InitializeComponent();
    this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
    this.SetStyle(ControlStyles.Selectable, true);
    this._resizeToolTip.SetToolTip((Control) this, "Перетащите этот значок для изменения размера панели.");
    this.UpdateLayout();
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this.components != null)
        this.components.Dispose();
      if (this._sf != null)
      {
        this._sf.Dispose();
        this._sf = (StringFormat) null;
      }
    }
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this._scrollBar = new VScrollBar();
    this._resizeToolTip = new ToolTip(this.components);
    this._itemTtoolTip = new ToolTip(this.components);
    this.SuspendLayout();
    this._scrollBar.Dock = DockStyle.Right;
    this._scrollBar.Location = new Point(280, 0);
    this._scrollBar.Name = "_scrollBar";
    this._scrollBar.Size = new Size(16 /*0x10*/, 288);
    this._scrollBar.TabIndex = 0;
    this._scrollBar.Scroll += new ScrollEventHandler(this.OnScroll);
    this._resizeToolTip.Active = false;
    this._resizeToolTip.AutoPopDelay = 2000;
    this._resizeToolTip.InitialDelay = 500;
    this._resizeToolTip.ReshowDelay = 100;
    this._itemTtoolTip.AutoPopDelay = 2000;
    this._itemTtoolTip.InitialDelay = 500;
    this._itemTtoolTip.ReshowDelay = 100;
    this.Controls.Add((Control) this._scrollBar);
    this.Size = new Size(296, 288);
    this.ResumeLayout(false);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnScroll(object sender, ScrollEventArgs e) => this.Invalidate();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void Renderer_RedrawRequired(object sender, BoundsEventArgs e)
  {
    this.Invalidate(e.Bounds);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseHover(EventArgs e) => base.OnMouseHover(e);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnResize(EventArgs e)
  {
    base.OnResize(e);
    this.UpdateLayout();
    this.EnsureVisible(this._itemIndex);
    this.Invalidate();
    this.Update();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnDoubleClick(EventArgs e)
  {
    base.OnDoubleClick(e);
    if (this.IsDisposed)
      return;
    Point client = this.PointToClient(Control.MousePosition);
    int indexAtPos = this.GetIndexAtPos(client.X, client.Y);
    if (indexAtPos == -1)
      return;
    this.OnPanelDoubleClick(indexAtPos + this.ItemOffset);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseDown(MouseEventArgs e)
  {
    if (!this.Focused)
      this.Focus();
    base.OnMouseDown(e);
    if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right)
      return;
    if (this._resizing)
    {
      if (!this._resizeMarkBounds.Contains(new Point(e.X, e.Y)))
        return;
      this.OnStartResize();
      this._dragging = true;
    }
    else
    {
      int indexAtPos = this.GetIndexAtPos(e.X, e.Y);
      this._popupIndex = indexAtPos;
      if (indexAtPos == -1)
        return;
      bool flag = false;
      if (indexAtPos >= this._colCount * this._visibleRows)
      {
        indexAtPos -= this._colCount;
        ++this._scrollBar.Value;
        flag = true;
      }
      int num = indexAtPos + this.ItemOffset;
      if (num == this._itemIndex || num >= this._count)
        return;
      this._oldIndex = this._itemIndex;
      this._oldBounds = this._selectedBounds;
      this._itemIndex = num;
      if (!flag)
      {
        this.Invalidate(this._selectedBounds);
        this.Invalidate(this.GetPanelBounds(indexAtPos));
        this.Update();
      }
      else
        this.Invalidate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseUp(MouseEventArgs e)
  {
    base.OnMouseUp(e);
    if (this.IsDisposed)
      return;
    if (this._dragging)
    {
      this.OnStopResize();
      this._dragging = false;
    }
    if (e.Button == MouseButtons.Right)
    {
      Point client = this.PointToClient(Control.MousePosition);
      if (this._itemIndex == -1)
        return;
      this.OnShowContextMenu(this._itemIndex, client);
      if (this._itemIndex == this._oldIndex)
        return;
      this._itemIndex = this._oldIndex;
      this.Invalidate(this._selectedBounds);
      this.Invalidate(this._oldBounds);
      this.Update();
    }
    else
    {
      if (e.Button != MouseButtons.Left || this._oldIndex == this._itemIndex)
        return;
      this.OnSelectionChanged(this._oldIndex, this._itemIndex);
      this._oldIndex = this._itemIndex;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseMove(MouseEventArgs e)
  {
    base.OnMouseMove(e);
    if (this._dragging && this._resizing)
    {
      int num1 = this._panelSpacing / 2;
      int num2 = e.X - num1;
      int num3 = e.Y - num1;
      if (this._renderer != null)
      {
        Size minimumSize = this._renderer.MinimumSize;
        if (minimumSize.Width > num2)
          num2 = minimumSize.Width;
        if (minimumSize.Height > num3)
          num3 = minimumSize.Height;
        Size maximumSize = this._renderer.MaximumSize;
        if (maximumSize.Width < num2)
          num2 = maximumSize.Width;
        if (maximumSize.Height < num3)
          num3 = maximumSize.Height;
      }
      if (this._panelWidth == num2 && this._panelHeight == num3)
        return;
      this._panelHeight = num3;
      this._panelWidth = num2;
      this.UpdateLayout();
      this.Invalidate();
      this.Update();
    }
    else
    {
      if (this._scrollBar.Enabled && this._scrollBar.Value != 0)
        return;
      this.Resizing = this._resizeMarkBounds.Contains(e.X, e.Y);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseWheel(MouseEventArgs e)
  {
    base.OnMouseWheel(e);
    int itemIndex = this._itemIndex;
    int num;
    if (e.Delta > 0)
    {
      num = itemIndex - 1;
    }
    else
    {
      if (e.Delta >= 0)
        return;
      num = itemIndex + 1;
    }
    this.ItemIndex = num;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="msg"></param>
  /// <returns></returns>
  public override bool PreProcessMessage(ref Message msg)
  {
    if (msg.Msg != 256 /*0x0100*/)
      return base.PreProcessMessage(ref msg);
    Keys wparam = (Keys) (int) msg.WParam;
    int itemIndex = this._itemIndex;
    int num1 = this._itemIndex - this.ItemOffset;
    int num2 = this._visibleRows * this._colCount;
    int num3;
    switch (wparam)
    {
      case Keys.Prior:
        num3 = num1 - num2;
        break;
      case Keys.Next:
        num3 = num1 + num2;
        break;
      case Keys.End:
        if (num1 != this._count - 1)
        {
          this._itemIndex = this._count - 1;
          if (this._scrollBar.Enabled)
            this._scrollBar.Value = this._count / this._colCount - this._visibleRows + 1;
          this.Invalidate();
        }
        return true;
      case Keys.Home:
        if (num1 != 0)
        {
          this._itemIndex = 0;
          this._scrollBar.Value = 0;
          this.Invalidate();
        }
        return true;
      case Keys.Left:
        num3 = num1 - 1;
        break;
      case Keys.Up:
        num3 = num1 - this._colCount;
        break;
      case Keys.Right:
        num3 = num1 + 1;
        break;
      case Keys.Down:
        num3 = num1 + this._colCount;
        break;
      default:
        return base.PreProcessMessage(ref msg);
    }
    if (this._scrollBar.Enabled)
    {
      int val2 = this._scrollBar.Value;
      if (num3 < 0)
      {
        for (; num3 < 0 && val2 > 0; num3 += this._colCount)
          --val2;
      }
      else if (num3 >= num2)
      {
        for (; num3 >= num2; num3 -= this._colCount)
          ++val2;
      }
      if (val2 < 0)
        val2 = 0;
      this._scrollBar.Value = Math.Min(this._scrollBar.Maximum, val2);
    }
    int num4 = num3 + this.ItemOffset;
    if (num4 > this._count - 1)
      num4 = this._count - 1;
    if (num4 < 0)
      num4 = 0;
    if (itemIndex != num4)
    {
      this.ItemIndex = num4;
      this.Invalidate();
    }
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnPaint(PaintEventArgs e)
  {
    Graphics graphics = e.Graphics;
    using (SolidBrush solidBrush = new SolidBrush(this.BackColor))
      graphics.FillRectangle((Brush) solidBrush, this.Bounds);
    int num1 = this._panelSpacing / 2;
    int itemOffset = this.ItemOffset;
    int num2 = this._colCount * this._rowCount;
    if (this._count > 0)
    {
      for (int index = 0; index < num2; ++index)
      {
        int panelIndex = index + itemOffset;
        if (panelIndex < this._count)
        {
          Rectangle panelBounds = this.GetPanelBounds(index);
          if (this._showShadow)
            graphics.DrawImage((Image) this._shadow, panelBounds.X, panelBounds.Y);
          if (this._itemIndex == panelIndex)
            this._selectedBounds = panelBounds;
          panelBounds.Inflate(-num1, -num1);
          if (this._renderer != null)
          {
            this._renderer.DrawPanel(panelIndex, graphics, panelBounds, this._itemIndex == panelIndex, this.Focused);
          }
          else
          {
            graphics.DrawRectangle(SystemPens.ControlDark, panelBounds);
            graphics.DrawString(panelIndex.ToString(), this.Font, SystemBrushes.ControlText, (float) (panelBounds.Left + 10), (float) (panelBounds.Top + 10));
            if (this._itemIndex == panelIndex)
            {
              panelBounds.Inflate(num1, num1);
              graphics.DrawRectangle(SystemPens.ControlLightLight, panelBounds);
            }
          }
        }
        else
          break;
      }
    }
    else
    {
      Rectangle rowsBounds = this.RowsBounds;
      graphics.DrawString("Нет данных для представления в этом виде.", this.Font, SystemBrushes.ControlText, (RectangleF) rowsBounds, this._sf);
    }
    if (!this._resizing)
      return;
    graphics.DrawImage((Image) ThumbnailGrid._resizeMark, this._resizeMarkBounds);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="bounds"></param>
  /// <param name="size"></param>
  /// <returns></returns>
  private GraphicsPath GetPath(Rectangle bounds, int size)
  {
    GraphicsPath path = new GraphicsPath();
    int num = size * 2;
    path.AddLine(bounds.Left, bounds.Bottom, bounds.Right, bounds.Bottom);
    path.AddLine(bounds.Right, bounds.Bottom, bounds.Right, bounds.Top);
    path.AddArc(bounds.Right, bounds.Top, size, size, 270f, 90f);
    path.AddLine(bounds.Right + size, bounds.Top + size, bounds.Right + size, bounds.Bottom);
    path.AddArc(bounds.Right, bounds.Bottom, size, size, 0.0f, 90f);
    path.AddLine(bounds.Right, bounds.Bottom + size, bounds.Left + size, bounds.Bottom + size);
    path.AddArc(bounds.Left, bounds.Bottom - size, num, num, 90f, 90f);
    return path;
  }

  /// <summary>
  /// 
  /// </summary>
  private void CreateShadow()
  {
    int panelSpacing = this._panelSpacing;
    this._shadow = new Bitmap(this._panelWidth + panelSpacing * 2, this._panelHeight + panelSpacing * 2);
    using (Graphics graphics = Graphics.FromImage((Image) this._shadow))
    {
      graphics.FillRectangle(SystemBrushes.Control, 0, 0, this._shadow.Width, this._shadow.Height);
      using (GraphicsPath path = this.GetPath(new Rectangle(panelSpacing / 2, panelSpacing / 2, this._panelWidth - 1, this._panelHeight - 1), this._shadowSize))
      {
        float num = (float) (1.0 - (double) this._shadowSize * 2.0 / (double) this._panelWidth);
        graphics.FillPath((Brush) new PathGradientBrush(path)
        {
          CenterPoint = (PointF) new Point(0, 0),
          CenterColor = SystemColors.ControlDarkDark,
          FocusScales = new PointF(num, num),
          SurroundColors = new Color[1]{ Color.Transparent }
        }, path);
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void UpdateLayout()
  {
    this.CreateShadow();
    Rectangle rowsBounds = this.RowsBounds;
    if (this._fixedSize)
    {
      int num1 = rowsBounds.Width / (this._panelWidth + this._panelSpacing);
      if (num1 < 1)
        num1 = 1;
      this._colCount = num1;
      int num2 = rowsBounds.Height / (this._panelHeight + this._panelSpacing);
      if (num2 < 1)
        num2 = 1;
      this._rowCount = num2 + 1;
      this._visibleRows = num2;
      this._totalRows = this._count / this._colCount + 1;
      if (this._count > 0)
      {
        Size size = ThumbnailGrid._resizeMark.Size;
        int num3 = this._panelSpacing / 2;
        this._resizeMarkBounds = new Rectangle(this._panelWidth - size.Width / 2 + num3, this._panelHeight - size.Height / 2 + num3, size.Width, size.Height);
      }
      else
        this._resizeMarkBounds = Rectangle.Empty;
    }
    else
      this._resizeMarkBounds = Rectangle.Empty;
    this.UpdateScrollBar();
  }

  /// <summary>
  /// 
  /// </summary>
  private void UpdateScrollBar()
  {
    this._scrollBar.Enabled = this._visibleRows < this._totalRows;
    if (!this._scrollBar.Enabled)
      return;
    this._scrollBar.Value = 0;
    this._scrollBar.SmallChange = 1;
    this._scrollBar.LargeChange = this._visibleRows;
    this._scrollBar.Maximum = this._totalRows - 1;
    this._scrollBar.Minimum = 0;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="aItemIndex"></param>
  private void EnsureVisible(int aItemIndex)
  {
    if (this._count <= 0 || this._colCount * this._visibleRows <= 0 || !this._scrollBar.Enabled)
      return;
    int num1 = aItemIndex - this.ItemOffset;
    int num2 = this._scrollBar.Value;
    int num3 = this._colCount * this._visibleRows;
    if (num1 < 0)
    {
      for (; num1 < 0 && num2 > 0; num1 += this._colCount)
        --num2;
    }
    else if (num1 >= num3)
    {
      for (; num1 >= num3; num1 -= this._colCount)
        ++num2;
    }
    if (this._scrollBar.Value == num2)
      return;
    this._scrollBar.Value = num2;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="index"></param>
  /// <returns></returns>
  private Rectangle GetPanelBounds(int index)
  {
    int num1 = index % this._colCount;
    int num2 = index / this._colCount;
    int num3 = this._panelWidth + this._panelSpacing;
    int num4 = this._panelHeight + this._panelSpacing;
    return new Rectangle(num3 * num1, num4 * num2, num3 - 1, num4 - 1);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="x"></param>
  /// <param name="y"></param>
  /// <returns></returns>
  private int GetIndexAtPos(int x, int y)
  {
    for (int index = 0; index < this._colCount * this._rowCount && index + this.ItemOffset < this._count; ++index)
    {
      if (this.GetPanelBounds(index).Contains(x, y))
        return index;
    }
    return -1;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="index"></param>
  private void OnPanelDoubleClick(int index)
  {
    if (this.ItemDoubleClick == null)
      return;
    this.ItemDoubleClick((object) this, new ThumbnailEventArgs(index));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="index"></param>
  /// <param name="pos"></param>
  private void OnShowContextMenu(int index, Point pos)
  {
    if (this.ShowContextMenu == null)
      return;
    this.ShowContextMenu((object) this, new ThumbnailEventArgs(index, pos));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="index"></param>
  /// <returns></returns>
  private string OnGetTooltipText(int index)
  {
    return this.GetToolTipText != null ? this.GetToolTipText((object) this, index) : (string) null;
  }

  /// <summary>
  /// 
  /// </summary>
  private void OnStopResize()
  {
    if (this.StopResize == null)
      return;
    this.StopResize((object) this, EventArgs.Empty);
  }

  /// <summary>
  /// 
  /// </summary>
  private void OnStartResize()
  {
    if (this.StartResize == null)
      return;
    this.StartResize((object) this, EventArgs.Empty);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="oldIndex"></param>
  /// <param name="newIndex"></param>
  private void OnSelectionChanged(int oldIndex, int newIndex)
  {
    if (this.SelectionChanged == null)
      return;
    this.SelectionChanged((object) this, oldIndex, newIndex);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="value"></param>
  /// <returns></returns>
  public bool IsItemVisible(int value)
  {
    if (this._count <= value)
      return false;
    int num = value - this.ItemOffset;
    return num >= 0 && num <= this._colCount * this._visibleRows;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="itemIndex"></param>
  public void RepaintItem(int itemIndex)
  {
    if (!this.IsItemVisible(itemIndex))
      return;
    this.Invalidate(this.GetPanelBounds(itemIndex - this.ItemOffset));
    this.Update();
  }
}
