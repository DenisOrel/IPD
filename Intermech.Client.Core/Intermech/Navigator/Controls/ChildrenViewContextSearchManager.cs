
// Type: Intermech.Navigator.Controls.ChildrenViewContextSearchManager
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;


namespace Intermech.Navigator.Controls;

/// <summary>Менеджер контекстного поиска в гриде</summary>
public sealed class ChildrenViewContextSearchManager
{
  private ChildrenView _childrenView;
  private Timer _resetTimer;
  private StringBuilder _stringBuilder = new StringBuilder();

  public ChildrenViewContextSearchManager(ChildrenView childrenView)
  {
    this._childrenView = childrenView != null ? childrenView : throw new ArgumentNullException(nameof (childrenView));
    this._childrenView.Grid.KeyPress += new KeyPressEventHandler(this.ChildrenViewGrid_KeyPress);
    this._childrenView.Grid.KeyDown += new KeyEventHandler(this.ChildrenViewGrid_KeyDown);
    this._childrenView.Grid.Paint += new PaintEventHandler(this.ChildrenViewGrid_Paint);
    this._childrenView.Grid.MouseDown += new MouseEventHandler(this.ChildrenViewGrid_MouseDown);
    this._childrenView.Grid.MouseUp += new MouseEventHandler(this.ChildrenViewGrid_MouseUp);
    this._childrenView.Grid.MouseDoubleClick += new MouseEventHandler(this.ChildrenViewGrid_MouseDoubleClick);
    this._resetTimer = new Timer();
    this._resetTimer.Interval = 10000;
    this._resetTimer.Tick += new EventHandler(this.ResetTimer_Tick);
  }

  public bool InProgress => this._stringBuilder.Length > 0;

  private void ChildrenViewGrid_KeyPress(object sender, KeyPressEventArgs e)
  {
    if (this._childrenView.EditingMode)
      return;
    if (this._resetTimer.Enabled)
      this._resetTimer.Stop();
    this._resetTimer.Start();
    if (e.KeyChar == '\b')
    {
      if (this._stringBuilder.Length > 0)
        this._stringBuilder.Remove(this._stringBuilder.Length - 1, 1);
      this.SetText();
      e.Handled = true;
    }
    else if (e.KeyChar <= '\u001F')
    {
      this.Cancel();
    }
    else
    {
      this._stringBuilder.Append(e.KeyChar);
      this.SetText();
      this.SelectNextCell();
      e.Handled = true;
    }
  }

  private void ChildrenViewGrid_KeyDown(object sender, KeyEventArgs e)
  {
    if (this._childrenView.EditingMode || e.KeyData == Keys.Back || (e.Modifiers == Keys.None || (e.Modifiers & Keys.Shift) != Keys.None || (e.Modifiers & Keys.ShiftKey) != Keys.None || (e.Modifiers & Keys.LShiftKey) != Keys.None || (e.Modifiers & Keys.RShiftKey) != Keys.None) && e.KeyData != Keys.Up && e.KeyData != Keys.Down && e.KeyData != Keys.Escape && e.KeyData != Keys.Return && e.KeyData != Keys.Return && e.KeyData != Keys.Home && e.KeyData != Keys.End && e.KeyData != Keys.Left && e.KeyData != Keys.Right && e.KeyData != Keys.Tab && e.KeyData != Keys.Prior && e.KeyData != Keys.Next && e.KeyData != Keys.BrowserBack && e.KeyData != Keys.BrowserFavorites && e.KeyData != Keys.BrowserForward && e.KeyData != Keys.BrowserHome && e.KeyData != Keys.BrowserStop)
      return;
    this.Cancel();
  }

  private void ChildrenViewGrid_Paint(object sender, PaintEventArgs e)
  {
    if (this._childrenView.EditingMode || this._stringBuilder.Length == 0)
      return;
    Rectangle textAreaBounds = this.GetTextAreaBounds(this._stringBuilder.ToString());
    using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(textAreaBounds, Color.FromArgb(200, 208 /*0xD0*/, 208 /*0xD0*/, (int) byte.MaxValue), Color.FromArgb(200, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), LinearGradientMode.Vertical))
      e.Graphics.FillRectangle((Brush) linearGradientBrush, textAreaBounds);
    e.Graphics.DrawRectangle(Pens.Black, textAreaBounds.X, textAreaBounds.Y, textAreaBounds.Width - 1, textAreaBounds.Height - 1);
    Rectangle layoutRectangle = textAreaBounds;
    layoutRectangle.Inflate(-2, -2);
    StringFormat format = new StringFormat();
    format.LineAlignment = StringAlignment.Center;
    Font font = new Font(this._childrenView.Grid.Font, FontStyle.Bold);
    e.Graphics.DrawString(this._stringBuilder.ToString(), font, Brushes.Black, (RectangleF) layoutRectangle, format);
  }

  private void ChildrenViewGrid_MouseDown(object sender, MouseEventArgs e)
  {
    if (this._childrenView.EditingMode || !this.InProgress)
      return;
    this.Cancel();
  }

  private void ChildrenViewGrid_MouseUp(object sender, MouseEventArgs e)
  {
    if (this._childrenView.EditingMode || !this.InProgress)
      return;
    this.Cancel();
  }

  private void ChildrenViewGrid_MouseDoubleClick(object sender, EventArgs e)
  {
    if (this._childrenView.EditingMode || !this.InProgress)
      return;
    this.Cancel();
  }

  private void ResetTimer_Tick(object sender, EventArgs e) => this.Cancel();

  private void SelectNextCell()
  {
    if (this._stringBuilder.Length == 0 || this._childrenView.Grid.Rows.Count == 0 || this._childrenView.Grid.Cols.Count == 0)
      return;
    if (this._childrenView.Grid.CurCell == null)
      this._childrenView.Grid.CurCell = this._childrenView.Grid.Cells[0, 0];
    int rowIndex = this._childrenView.Grid.CurCell.RowIndex;
    int colIndex = this._childrenView.Grid.CurCell.ColIndex;
    string upper1 = this._stringBuilder.ToString().ToUpper();
    iGCell iGcell1 = (iGCell) null;
    int index = rowIndex;
    int num = 0;
    int count = this._childrenView.Grid.Rows.Count;
    this._childrenView.GridCancelHint();
    while (num < count)
    {
      if (index == this._childrenView.Grid.Rows.Count)
        index = 0;
      iGRow row = this._childrenView.Grid.Rows[index];
      ++index;
      ++num;
      string upper2;
      iGCell iGcell2;
      if (row.Type == iGRowType.Normal)
      {
        upper2 = row.Cells[colIndex] == null || row.Cells[colIndex].Text == null ? (string) null : row.Cells[colIndex].Text.ToUpper();
        iGcell2 = row.Cells[colIndex];
      }
      else
      {
        upper2 = row.RowTextCell == null || row.RowTextCell.Text == null ? (string) null : row.RowTextCell.Text.ToUpper();
        iGcell2 = row.RowTextCell;
      }
      if (upper2 != null)
      {
        if (row.Type == iGRowType.Normal && upper2.StartsWith(upper1) || row.Type != iGRowType.Normal && upper2.Contains(upper1))
        {
          iGcell1 = iGcell2;
          break;
        }
      }
      else if (iGcell1 != null)
      {
        iGcell1 = (iGCell) null;
        break;
      }
    }
    if (iGcell1 == null)
      return;
    this._childrenView.Grid.PerformAction(iGActions.DeselectAll);
    iGcell1.Selected = true;
    this._childrenView.Grid.CurCell = iGcell1;
  }

  private void SetText()
  {
    this._childrenView.Grid.Invalidate(this.GetTextAreaBounds(this._stringBuilder.ToString()));
  }

  private Rectangle GetTextAreaBounds(string value)
  {
    Font font = new Font(this._childrenView.Grid.Font, FontStyle.Bold);
    int width = font.Height * 15 + 6;
    int height = font.Height + 6;
    Rectangle rect = this._childrenView.Grid.CurCell != null ? this._childrenView.Grid.CurCell.Bounds : Rectangle.Empty;
    if (rect.Height == 0)
      rect.Height = height;
    Rectangle cellsAreaBounds = this._childrenView.Grid.CellsAreaBounds;
    if (rect == Rectangle.Empty)
      return new Rectangle(cellsAreaBounds.Right - width, cellsAreaBounds.Bottom - height, width, height);
    rect.Offset(0, rect.Height);
    return !cellsAreaBounds.Contains(rect) ? new Rectangle(cellsAreaBounds.Right - width, cellsAreaBounds.Bottom - height, width, height) : new Rectangle(rect.Left, rect.Top, rect.Width, rect.Height);
  }

  private void Cancel()
  {
    this._stringBuilder.Length = 0;
    this._childrenView.Grid.Invalidate();
  }
}
