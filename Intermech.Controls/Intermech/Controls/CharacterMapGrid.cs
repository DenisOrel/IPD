
// Type: Intermech.Controls.CharacterMapGrid
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Controls;

public class CharacterMapGrid : Control
{
  public Color CellBackGroundColor;
  private SolidBrush CellBGround;
  public Color CellBorderColor;
  public int CellBorderWidth;
  private Pen CellPen;
  private int CellsInRow;
  public int CellSpacing;
  public int CellWidth;
  public int CellWidthHalf;
  public Font CharFont;
  private int FocusedCell;
  private int FocusedIterator;
  private Graphics g;
  public Color GridFontColor;
  private Rectangle GridRect;
  private int GridRowsCount;
  public NativeWindowMethods.GLYPHSET gs;
  private bool IsPreview;
  public Color PreviewBackGroundColor;
  private Brush PreviewbkBrush;
  public int PreviewCellWidth;
  public int PreviewCellWidthHalf;
  public Color PreviewFontColor;
  private StringFormat PreviewStringFormat;
  private SolidBrush PreviewSymbolBrush;
  public Font PreviewSymbolFont;
  private Rectangle[] Rectangles;
  private VScrollBar Scroll;
  private StringFormat stringFormat;
  private RichTextBox SymbolBox;
  private SolidBrush SymbolBrush;
  private int VisibleCellsCount;

  public event CharacterMapGrid.CharSelectedEventHandler OnCharSelected;

  public CharacterMapGrid()
  {
    this.CellWidth = 18;
    this.CellWidthHalf = 9;
    this.PreviewCellWidth = 48 /*0x30*/;
    this.PreviewCellWidthHalf = 24;
    this.CellBorderWidth = 0;
    this.CellSpacing = 6;
    this.CharFont = (Font) null;
    this.gs = (NativeWindowMethods.GLYPHSET) null;
    this.CellBackGroundColor = Color.Gainsboro;
    this.GridFontColor = Color.DimGray;
    this.PreviewFontColor = Color.Black;
    this.PreviewBackGroundColor = Color.LightGray;
    this.CellBorderColor = SystemColors.ControlDarkDark;
    this.stringFormat = new StringFormat();
    this.PreviewStringFormat = new StringFormat();
    this.Scroll = (VScrollBar) null;
    this.Rectangles = (Rectangle[]) null;
    this.FocusedIterator = -1;
    this.SymbolBox = new RichTextBox();
    this.g = (Graphics) null;
    this.CellPen = (Pen) null;
    this.SymbolBrush = (SolidBrush) null;
    this.PreviewbkBrush = (Brush) null;
    this.PreviewSymbolFont = (Font) null;
    this.PreviewSymbolBrush = (SolidBrush) null;
    this.CellBGround = (SolidBrush) null;
    this.FocusedCell = -1;
    this.IsPreview = false;
    this.CellsInRow = 0;
    this.GridRowsCount = 0;
    this.VisibleCellsCount = 0;
    this.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.DoubleBuffer, true);
    this.Controls.AddRange(new Control[1]
    {
      (Control) this.SymbolBox
    });
    this.stringFormat.Alignment = StringAlignment.Center;
    this.stringFormat.LineAlignment = StringAlignment.Center;
    this.stringFormat.FormatFlags = StringFormatFlags.FitBlackBox | StringFormatFlags.NoFontFallback;
    this.PreviewStringFormat.Alignment = StringAlignment.Center;
    this.PreviewStringFormat.LineAlignment = StringAlignment.Center;
    this.PreviewStringFormat.FormatFlags = StringFormatFlags.FitBlackBox | StringFormatFlags.NoFontFallback;
  }

  private int CellsCountInRow()
  {
    return (this.GridRect.Width - this.Scroll.Width) / (this.CellWidth + this.CellSpacing);
  }

  private void DrawGridCell(Graphics g, Rectangle rect)
  {
    if (this.gs == null)
      return;
    CharacterMapGrid.FocusedSymbol FSymbol = new CharacterMapGrid.FocusedSymbol();
    this.GetFocusedSymbol(0, ref FSymbol);
    int range = FSymbol.Range;
    int c = (int) this.gs.ranges[FSymbol.Range].wcLow + FSymbol.Symbol;
    for (int index = 0; index < this.Rectangles.Length && (range != this.gs.header.cRanges - 1 || this.gs.header.cbThis == 0 || c < (int) this.gs.ranges[range].wcLow + (int) this.gs.ranges[range].cGlyphs - 1); ++index)
    {
      if (c >= (int) this.gs.ranges[range].cGlyphs + (int) this.gs.ranges[range].wcLow)
      {
        ++range;
        if (range == this.gs.header.cRanges)
        {
          int num = range - 1;
          break;
        }
        c = (int) this.gs.ranges[range].wcLow;
      }
      string s = new string((char) c, 1);
      g.FillRectangle((Brush) this.CellBGround, this.Rectangles[index]);
      Rectangle layoutRectangle = new Rectangle(this.Rectangles[index].Left, this.Rectangles[index].Top, this.CellWidth, this.CellWidth);
      g.DrawString(s, this.CharFont, (Brush) this.SymbolBrush, (RectangleF) layoutRectangle, this.stringFormat);
      ++c;
    }
  }

  private void DrawPreviewCell(int CellNumber)
  {
    this.Refresh();
    CharacterMapGrid.FocusedSymbol FSymbol = new CharacterMapGrid.FocusedSymbol();
    this.GetFocusedSymbol(CellNumber, ref FSymbol);
    if (FSymbol.Range == -1)
      return;
    int num = (int) this.gs.ranges[FSymbol.Range].wcLow + FSymbol.Symbol;
    string s = new string((char) num, 1);
    Rectangle rect = new Rectangle(this.Rectangles[CellNumber].X + this.CellWidthHalf - this.PreviewCellWidthHalf, this.Rectangles[CellNumber].Y + this.CellWidthHalf - this.PreviewCellWidthHalf, this.PreviewCellWidth, this.PreviewCellWidth);
    Rectangle layoutRectangle = new Rectangle(this.Rectangles[CellNumber].X + this.CellWidthHalf - this.PreviewCellWidthHalf, this.Rectangles[CellNumber].Y + this.CellWidthHalf - this.PreviewCellWidthHalf, this.PreviewCellWidth, this.PreviewCellWidth);
    this.g.DrawRectangle(this.CellPen, rect);
    this.g.FillRectangle(this.PreviewbkBrush, rect);
    this.g.DrawString(s, this.PreviewSymbolFont, (Brush) this.PreviewSymbolBrush, (RectangleF) layoutRectangle, this.PreviewStringFormat);
    this.SetStatusText(num, false);
  }

  private int GetFirstVisibleUnicodeRange(int ScrollValue)
  {
    int num = ScrollValue * this.CellsInRow;
    for (int visibleUnicodeRange = 0; visibleUnicodeRange < this.gs.header.cRanges; ++visibleUnicodeRange)
    {
      if (num <= (int) this.gs.ranges[visibleUnicodeRange].cGlyphs)
        return visibleUnicodeRange;
      num -= (int) this.gs.ranges[visibleUnicodeRange].cGlyphs;
    }
    return 0;
  }

  private void GetFocusedSymbol(int Symbol, ref CharacterMapGrid.FocusedSymbol FSymbol)
  {
    int num = Symbol + this.Scroll.Value * this.CellsInRow;
    if (num >= this.gs.header.cGlyphsSupported)
    {
      FSymbol.Range = -1;
    }
    else
    {
      for (int index = 0; index < this.gs.header.cRanges; ++index)
      {
        if (num >= (int) this.gs.ranges[index].cGlyphs)
        {
          num -= (int) this.gs.ranges[index].cGlyphs;
        }
        else
        {
          FSymbol.Symbol = num;
          FSymbol.Range = index;
          break;
        }
      }
    }
  }

  private int GetRowsCount(int CellsCount)
  {
    int rowsCount = (int) ((double) CellsCount / ((double) (this.GridRect.Width - this.Scroll.Width) / (double) (this.CellWidth + this.CellSpacing)));
    if (rowsCount <= 0)
      rowsCount = 1;
    return rowsCount;
  }

  private int GetVisibleCellNumber()
  {
    return this.GridRect.Bottom / (this.CellWidth + this.CellSpacing) * ((this.GridRect.Width - this.Scroll.Width - 3) / (this.CellWidth + this.CellSpacing));
  }

  private void InitScrollBar()
  {
    this.Scroll = new VScrollBar();
    this.Scroll.Dock = DockStyle.None;
    this.Controls.Add((Control) this.Scroll);
    this.Scroll.Scroll += new ScrollEventHandler(this.OnScroll);
    this.Scroll.Value = 0;
  }

  public void InvalidateGrid(bool RecalculateGrid)
  {
    if (!RecalculateGrid)
    {
      this.CellBGround = new SolidBrush(this.CellBackGroundColor);
      this.CellPen = new Pen(this.CellBorderColor, (float) this.CellBorderWidth);
      this.SymbolBrush = new SolidBrush(this.GridFontColor);
      this.PreviewSymbolBrush = new SolidBrush(this.PreviewFontColor);
      this.PreviewbkBrush = (Brush) new SolidBrush(this.PreviewBackGroundColor);
    }
    else if (!this.GridRect.IsEmpty && this.VisibleCellsCount > 0)
    {
      this.Rectangles = new Rectangle[this.VisibleCellsCount];
      int num1 = 0;
      int num2 = 0;
      for (int index = 0; index < this.VisibleCellsCount; ++index)
      {
        if (index > 0 && this.Rectangles[index - 1].Right + this.CellWidth > this.GridRect.Right - this.CellSpacing - this.Scroll.Width - 2)
        {
          num1 = 0;
          num2 += this.CellWidth + this.CellSpacing;
        }
        this.Rectangles[index] = new Rectangle(this.GridRect.Left + this.CellSpacing + num1, this.GridRect.Top + this.CellSpacing + num2, this.CellWidth, this.CellWidth);
        num1 += this.CellWidth + this.CellSpacing;
      }
    }
    this.Invalidate();
  }

  protected override void OnClick(EventArgs e)
  {
    CharacterMapGrid.FocusedSymbol FSymbol = new CharacterMapGrid.FocusedSymbol();
    this.GetFocusedSymbol(this.FocusedCell, ref FSymbol);
    if (FSymbol.Range < 0)
      return;
    string _selectedChar = new string((char) ((uint) this.gs.ranges[FSymbol.Range].wcLow + (uint) FSymbol.Symbol), 1);
    if (_selectedChar != null)
    {
      CharacterMap.CharacterMapEventArgs e1 = new CharacterMap.CharacterMapEventArgs(_selectedChar, this.CharFont);
      if (this.OnCharSelected != null)
        this.OnCharSelected((object) this, e1);
    }
    base.OnClick(e);
  }

  protected override void OnGotFocus(EventArgs e)
  {
    if (this.FocusedCell < 0)
    {
      this.FocusedCell = 0;
      this.SetStatusText(this.FocusedCell, true);
      this.Invalidate();
    }
    else
    {
      this.ResetPreviewCell();
      this.SetStatusText(this.FocusedCell, true);
      this.Invalidate();
    }
    base.OnGotFocus(e);
  }

  protected override void OnLeave(EventArgs e)
  {
    this.Invalidate();
    base.OnLeave(e);
  }

  protected override void OnMouseEnter(EventArgs e) => base.OnMouseEnter(e);

  protected override void OnMouseMove(MouseEventArgs e)
  {
    base.OnMouseMove(e);
    for (int CellNumber = 0; CellNumber < this.Rectangles.Length; ++CellNumber)
    {
      if (e.X >= this.Rectangles[CellNumber].Left && e.X <= this.Rectangles[CellNumber].Right && e.Y >= this.Rectangles[CellNumber].Top && e.Y <= this.Rectangles[CellNumber].Bottom)
      {
        if (this.FocusedIterator != CellNumber)
        {
          this.FocusedCell = CellNumber;
          this.DrawPreviewCell(CellNumber);
          this.ResetPreviewCell();
          this.FocusedIterator = CellNumber;
          break;
        }
      }
      else if (this.FocusedIterator == CellNumber && this.FocusedIterator != -1)
      {
        this.FocusedIterator = -1;
        this.Refresh();
        break;
      }
    }
  }

  protected override void OnMouseWheel(MouseEventArgs e)
  {
    base.OnMouseWheel(e);
    if (e.Delta < 0 && this.Scroll.Visible)
    {
      if (this.Scroll.Value >= this.Scroll.Maximum - (this.Scroll.LargeChange - 1))
        return;
      this.OnScroll((object) this, new ScrollEventArgs(ScrollEventType.SmallIncrement, this.Scroll.Value += this.Scroll.SmallChange));
    }
    else
    {
      if (this.Scroll.Value <= this.Scroll.Minimum)
        return;
      this.OnScroll((object) this, new ScrollEventArgs(ScrollEventType.SmallDecrement, this.Scroll.Value -= this.Scroll.SmallChange));
    }
  }

  protected override void OnPaint(PaintEventArgs e)
  {
    ControlPaint.DrawBorder3D(e.Graphics, this.GridRect.Left, this.GridRect.Top, this.GridRect.Width - this.Scroll.Width, this.GridRect.Height, Border3DStyle.Etched);
    this.DrawGridCell(e.Graphics, this.GridRect);
    if (this.FocusedCell < this.Rectangles.Length && this.FocusedCell >= 0 && this.Focused)
    {
      Rectangle rectangle = new Rectangle(this.Rectangles[this.FocusedCell].Left - 3, this.Rectangles[this.FocusedCell].Top - 3, this.CellWidth + 6, this.CellWidth + 6);
      ControlPaint.DrawFocusRectangle(e.Graphics, rectangle, this.BackColor, this.CellBorderColor);
    }
    base.OnPaint(e);
  }

  protected override void OnPaintBackground(PaintEventArgs pevent)
  {
    pevent.Graphics.FillRectangle((Brush) new SolidBrush(this.BackColor), this.GridRect.Left, this.GridRect.Top, this.GridRect.Width - this.Scroll.Width, this.GridRect.Height);
    base.OnPaintBackground(pevent);
  }

  protected override void OnResize(EventArgs e)
  {
    if (this.GridRect.IsEmpty)
    {
      this.GridRect = new Rectangle(0, 0, this.Width - 1, this.Height - 24);
      if (this.Scroll == null || !this.Scroll.Created)
        this.InitScrollBar();
      this.SetScrollMaximum();
      this.SymbolBox.Location = new Point(this.ClientSize.Width, this.GridRect.Bottom + 1);
      this.SymbolBox.Size = new Size(this.ClientSize.Width / 2, this.CellWidth);
      this.SymbolBox.BackColor = SystemColors.Window;
      this.SymbolBox.BorderStyle = BorderStyle.Fixed3D;
      this.SymbolBox.Multiline = false;
      this.SymbolBox.Visible = false;
      this.InvalidateGrid(false);
    }
    else
    {
      this.SuspendLayout();
      this.GridRect = new Rectangle(0, 0, this.Width - 1, this.Height - 24);
      this.Scroll.Size = new Size(this.Scroll.Width, this.GridRect.Height - 8);
      this.Scroll.Location = new Point(this.GridRect.Right - this.Scroll.Width, this.GridRect.Top);
      this.SetScrollMaximum();
      this.ResumeLayout();
    }
    if (this.g != null)
      this.g.Dispose();
    this.g = this.CreateGraphics();
    this.InvalidateGrid(true);
    base.OnResize(e);
  }

  private void OnScroll(object Sender, ScrollEventArgs e)
  {
    if (e.Type != ScrollEventType.LargeIncrement && e.Type != ScrollEventType.SmallIncrement && e.Type != ScrollEventType.SmallDecrement && e.Type != ScrollEventType.LargeDecrement && e.Type != ScrollEventType.ThumbTrack)
      return;
    this.Invalidate();
  }

  protected override bool ProcessDialogKey(Keys keyData)
  {
    switch (keyData)
    {
      case Keys.Tab:
        this.ResetPreviewCell();
        this.Parent.Parent.SelectNextControl(this.Parent.Parent, false, false, false, false);
        break;
      case Keys.Return:
        this.OnClick((EventArgs) null);
        break;
      case Keys.Prior:
        if (this.Scroll.Value - this.Scroll.LargeChange <= this.Scroll.Minimum || !this.Scroll.Visible)
        {
          if (this.Scroll.Value != this.Scroll.Minimum && this.Scroll.Visible)
          {
            this.Scroll.Value = this.Scroll.Minimum;
            this.Invalidate();
          }
        }
        else
        {
          this.Scroll.Value -= this.Scroll.LargeChange;
          this.Invalidate();
        }
        this.ResetPreviewCell();
        this.SetStatusText(this.FocusedCell, true);
        break;
      case Keys.Next:
        if (this.Scroll.Value + this.Scroll.LargeChange >= this.Scroll.Maximum - (this.Scroll.LargeChange - 1) || !this.Scroll.Visible)
        {
          if (this.Scroll.Value != this.Scroll.Maximum - (this.Scroll.LargeChange - 1) && this.Scroll.Visible)
          {
            this.Scroll.Value = this.Scroll.Maximum - (this.Scroll.LargeChange - 1);
            this.Invalidate();
          }
        }
        else
        {
          this.Scroll.Value += this.Scroll.LargeChange;
          this.Invalidate();
        }
        this.ResetPreviewCell();
        this.SetStatusText(this.FocusedCell, true);
        break;
      case Keys.Left:
        if (this.FocusedCell - 1 < 0)
        {
          if (this.Scroll.Value > this.Scroll.Minimum && this.Scroll.Visible)
          {
            --this.Scroll.Value;
            this.FocusedCell = this.CellsInRow - 1;
          }
        }
        else
          --this.FocusedCell;
        this.ResetPreviewCell();
        this.SetStatusText(this.FocusedCell, true);
        this.Invalidate();
        break;
      case Keys.Up:
        if (this.FocusedCell - this.CellsInRow < this.Scroll.Minimum)
        {
          if (this.FocusedCell - this.CellsInRow < this.Rectangles.Length && this.Scroll.Value > this.Scroll.Minimum && this.Scroll.Visible)
            --this.Scroll.Value;
        }
        else
          this.FocusedCell -= this.CellsInRow;
        this.ResetPreviewCell();
        this.SetStatusText(this.FocusedCell, true);
        this.Invalidate();
        break;
      case Keys.Right:
        if (this.FocusedCell + 1 < this.Rectangles.Length || !this.Scroll.Visible || this.Scroll.Value >= this.Scroll.Maximum - (this.Scroll.LargeChange - 1))
        {
          if (this.FocusedCell + 1 < this.Rectangles.Length)
            ++this.FocusedCell;
        }
        else
        {
          ++this.Scroll.Value;
          this.FocusedCell = this.Rectangles.Length - this.CellsInRow;
        }
        this.ResetPreviewCell();
        this.SetStatusText(this.FocusedCell, true);
        this.Invalidate();
        break;
      case Keys.Down:
        if (this.FocusedCell + this.CellsInRow < this.Rectangles.Length || this.Scroll.Value >= this.Scroll.Maximum - (this.Scroll.LargeChange - 1) || !this.Scroll.Visible)
        {
          if (this.FocusedCell + this.CellsInRow < this.Rectangles.Length)
            this.FocusedCell += this.CellsInRow;
        }
        else
          ++this.Scroll.Value;
        this.ResetPreviewCell();
        this.SetStatusText(this.FocusedCell, true);
        this.Invalidate();
        break;
      case Keys.Add:
        if (!this.IsPreview)
        {
          this.DrawPreviewCell(this.FocusedCell);
          this.IsPreview = true;
          break;
        }
        break;
      case Keys.Subtract:
        this.ResetPreviewCell();
        this.Invalidate();
        break;
      default:
        this.Invalidate();
        return false;
    }
    return false;
  }

  private void ResetPreviewCell()
  {
    if (!this.IsPreview)
      return;
    this.IsPreview = false;
  }

  public void SetScrollMaximum()
  {
    this.CellsInRow = this.CellsCountInRow();
    try
    {
      this.GridRowsCount = this.GetRowsCount(this.gs.header.cGlyphsSupported);
    }
    catch
    {
      return;
    }
    this.VisibleCellsCount = this.GetVisibleCellNumber();
    this.FocusedCell = -1;
    if (this.VisibleCellsCount > this.gs.header.cGlyphsSupported)
    {
      this.Scroll.Value = 0;
      this.Scroll.Maximum = this.GridRowsCount;
      this.Scroll.Visible = false;
    }
    else
    {
      this.Scroll.Value = 0;
      this.Scroll.SuspendLayout();
      this.Scroll.Size = new Size(this.Scroll.Width, this.GridRect.Height);
      this.Scroll.Location = new Point(this.GridRect.Right - this.Scroll.Width, this.GridRect.Top);
      this.Scroll.SendToBack();
      this.Scroll.ResumeLayout();
      this.Scroll.SmallChange = 1;
      if (this.VisibleCellsCount > 0)
        this.Scroll.LargeChange = this.GetRowsCount(this.VisibleCellsCount);
      else
        this.Scroll.LargeChange = 0;
      this.Scroll.Maximum = this.GridRowsCount;
      this.Scroll.Visible = true;
    }
  }

  private void SetStatusText(int Symbol, bool FindSymbol)
  {
  }

  public delegate void CharSelectedEventHandler(object source, CharacterMap.CharacterMapEventArgs e);

  private struct FocusedSymbol
  {
    public int Range;
    public int Symbol;
  }
}
