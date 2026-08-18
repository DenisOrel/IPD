// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Cadmech_3D.IgTexWithButtonCellManager
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.TechCard.Client.Cadmech_3D;

/// <summary>
/// 
/// </summary>
internal class IgTexWithButtonCellManager
{
  /// <summary>Спец. так для идентификации типа ячейки</summary>
  public const string CELL_TEXTWITHBUTTON_TAG = "TextWithButton";
  /// <summary>
  /// 
  /// </summary>
  private iGrid _grid;
  /// <summary>
  /// 
  /// </summary>
  private bool _buttonMouseDown;
  /// <summary>
  /// 
  /// </summary>
  private bool _buttonMouseUp;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="colIndex"></param>
  /// <returns></returns>
  private bool IsTextWithButtonColumn(int colIndex)
  {
    return (string) this._grid.Cols[colIndex].Tag == "TextWithButton";
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="rowIndex"></param>
  /// <param name="colIndex"></param>
  /// <returns></returns>
  private bool IsCellButtonEnabled(int rowIndex, int colIndex)
  {
    return this.IsTextWithButtonColumn(colIndex) && this._grid.Cells[rowIndex, colIndex].Enabled != iGBool.False;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="rect"></param>
  /// <param name="pos"></param>
  /// <returns></returns>
  private bool IsCellPointInButtonBounds(Rectangle rect, Point pos)
  {
    return pos.X >= rect.X + rect.Width - rect.Height;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="grid"></param>
  public void AttachTo(iGrid grid)
  {
    this._grid = grid ?? throw new ArgumentNullException(nameof (grid));
    this._grid.CellClick += new iGCellClickEventHandler(this.grid_CellClick);
    this._grid.CellMouseDown += new iGCellMouseDownEventHandler(this.grid_CellMouseDown);
    this._grid.CellMouseUp += new iGCellMouseUpEventHandler(this.grid_CellMouseUp);
    this._grid.CellMouseEnter += new iGCellMouseEnterLeaveEventHandler(this.grid_CellMouseEnter);
    this._grid.CellMouseLeave += new iGCellMouseEnterLeaveEventHandler(this.grid_CellMouseLeave);
    this._grid.CustomDrawCellForeground += new iGCustomDrawCellEventHandler(this.grid_CustomDrawCellForeground);
    this._grid.CustomDrawCellGetHeight += new iGCustomDrawCellGetHeightEventHandler(this.grid_CustomDrawCellGetHeight);
    this._grid.CustomDrawCellGetWidth += new iGCustomDrawCellGetWidthEventHandler(this.grid_CustomDrawCellGetWidth);
    this._grid.RequestEdit += new iGRequestEditEventHandler(this.grid_RequestEdit);
    this._grid.KeyPress += new KeyPressEventHandler(this.grid_KeyPress);
    foreach (iGCol col in (IEnumerable) this._grid.Cols)
    {
      if (this.IsTextWithButtonColumn(col.Index))
        col.CellStyle.CustomDrawFlags = iGCustomDrawFlags.Foreground;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public event IgTexWithButtonCellManager.CellButtonClickedDelegate CellButtonClicked;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="rowIndex"></param>
  /// <param name="colIndex"></param>
  private void RaiseCellButtonClickEvent(int rowIndex, int colIndex)
  {
    IgTexWithButtonCellManager.CellButtonClickedDelegate cellButtonClicked = this.CellButtonClicked;
    if (cellButtonClicked == null)
      return;
    cellButtonClicked(rowIndex, colIndex);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void grid_CellClick(object sender, iGCellClickEventArgs e)
  {
    if (!this.IsCellButtonEnabled(e.RowIndex, e.ColIndex) || !this._buttonMouseDown || !this._buttonMouseUp)
      return;
    this.RaiseCellButtonClickEvent(e.RowIndex, e.ColIndex);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void grid_CellMouseDown(object sender, iGCellMouseDownEventArgs e)
  {
    if (!this.IsCellButtonEnabled(e.RowIndex, e.ColIndex))
      return;
    this._buttonMouseDown = this.IsCellPointInButtonBounds(e.Bounds, e.MousePos);
    this._grid?.Invalidate(e.Bounds);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void grid_CellMouseEnter(object sender, iGCellMouseEnterLeaveEventArgs e)
  {
    if (!this.IsCellButtonEnabled(e.RowIndex, e.ColIndex))
      return;
    this._grid?.Invalidate(e.Bounds);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void grid_CellMouseLeave(object sender, iGCellMouseEnterLeaveEventArgs e)
  {
    if (!this.IsCellButtonEnabled(e.RowIndex, e.ColIndex))
      return;
    this._grid?.Invalidate(e.Bounds);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void grid_CellMouseUp(object sender, iGCellMouseUpEventArgs e)
  {
    if (!this.IsCellButtonEnabled(e.RowIndex, e.ColIndex))
      return;
    this._buttonMouseUp = this.IsCellPointInButtonBounds(e.Bounds, e.MousePos);
    this._grid?.Invalidate(e.Bounds);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void grid_CustomDrawCellForeground(object sender, iGCustomDrawCellEventArgs e)
  {
    if (!this.IsTextWithButtonColumn(e.ColIndex))
      return;
    iGCell cell = this._grid.Cells[e.RowIndex, e.ColIndex];
    if (!e.Selected)
    {
      TextRenderer.DrawText((IDeviceContext) e.Graphics, cell.Text, cell.EffectiveFont, e.Bounds, cell.EffectiveForeColor, TextFormatFlags.EndEllipsis | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter);
    }
    else
    {
      PushButtonState pushButtonState;
      switch (e.State)
      {
        case iGControlState.Normal:
          pushButtonState = PushButtonState.Normal;
          break;
        case iGControlState.Hot:
          pushButtonState = PushButtonState.Hot;
          break;
        case iGControlState.Pressed:
          pushButtonState = this._buttonMouseDown ? PushButtonState.Pressed : PushButtonState.Normal;
          break;
        default:
          pushButtonState = PushButtonState.Disabled;
          break;
      }
      Graphics graphics1 = e.Graphics;
      string text = cell.Text;
      Font effectiveFont1 = cell.EffectiveFont;
      Rectangle bounds1 = e.Bounds;
      int x1 = bounds1.X;
      bounds1 = e.Bounds;
      int y1 = bounds1.Y;
      bounds1 = e.Bounds;
      int width1 = bounds1.Width;
      bounds1 = e.Bounds;
      int height1 = bounds1.Height;
      int width2 = width1 - height1;
      bounds1 = e.Bounds;
      int height2 = bounds1.Height;
      Rectangle bounds2 = new Rectangle(x1, y1, width2, height2);
      Color foreColor = this._grid.Focused ? this._grid.FocusRectColor2 : this._grid.FocusRectColor1;
      TextRenderer.DrawText((IDeviceContext) graphics1, text, effectiveFont1, bounds2, foreColor, TextFormatFlags.EndEllipsis | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter);
      Graphics graphics2 = e.Graphics;
      bounds1 = e.Bounds;
      int x2 = bounds1.X;
      bounds1 = e.Bounds;
      int width3 = bounds1.Width;
      int num = x2 + width3;
      bounds1 = e.Bounds;
      int height3 = bounds1.Height;
      int x3 = num - height3;
      bounds1 = e.Bounds;
      int y2 = bounds1.Y;
      bounds1 = e.Bounds;
      int height4 = bounds1.Height;
      bounds1 = e.Bounds;
      int height5 = bounds1.Height;
      Rectangle bounds3 = new Rectangle(x3, y2, height4, height5);
      Font effectiveFont2 = cell.EffectiveFont;
      int state = (int) pushButtonState;
      ButtonRenderer.DrawButton(graphics2, bounds3, "...", effectiveFont2, TextFormatFlags.HorizontalCenter | TextFormatFlags.NoPadding, false, (PushButtonState) state);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void grid_CustomDrawCellGetHeight(object sender, iGCustomDrawCellGetHeightEventArgs e)
  {
    Font font = (Font) null;
    if (e.RowIndex < 0)
      font = this._grid.Font;
    else if (this.IsTextWithButtonColumn(e.ColIndex))
      font = this._grid.Cells[e.RowIndex, e.ColIndex].EffectiveFont;
    if (font == null)
      return;
    e.Height = font.Height + 8;
  }

  private void grid_CustomDrawCellGetWidth(object sender, iGCustomDrawCellGetWidthEventArgs e)
  {
    if (!this.IsTextWithButtonColumn(e.ColIndex))
      return;
    iGCell cell = this._grid.Cells[e.RowIndex, e.ColIndex];
    using (Graphics graphics = this._grid.CreateGraphics())
      e.Width = (int) graphics.MeasureString(cell.Text, cell.EffectiveFont).Width + 8;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void grid_RequestEdit(object sender, iGRequestEditEventArgs e)
  {
    if (!this.IsTextWithButtonColumn(e.ColIndex))
      return;
    e.DoDefault = false;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void grid_KeyPress(object sender, KeyPressEventArgs e)
  {
    iGCell curCell = this._grid.CurCell;
    if (e.KeyChar != ' ' || curCell == null || !this.IsCellButtonEnabled(curCell.RowIndex, curCell.ColIndex))
      return;
    this.RaiseCellButtonClickEvent(curCell.RowIndex, curCell.ColIndex);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="rowIndex"></param>
  /// <param name="colIndex"></param>
  public delegate void CellButtonClickedDelegate(int rowIndex, int colIndex);
}
