// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.CTbl
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class CTbl : COp
{
  internal CTbl(ImRtfEditor ImRtfEditor)
    : base(ImRtfEditor)
  {
    this.e = ImRtfEditor;
  }

  internal new bool AdjustBlockForTable(bool AdjustCurPos)
  {
    int hilightBegRow = this.e.HilightBegRow;
    int hilightBegCol = this.e.HilightBegCol;
    int hilightEndRow = this.e.HilightEndRow;
    int hilightEndCol = this.e.HilightEndCol;
    int num = this.AdjustBlockForTable(ref hilightBegRow, ref hilightBegCol, ref hilightEndRow, ref hilightEndCol, AdjustCurPos) ? 1 : 0;
    this.e.HilightBegRow = hilightBegRow;
    this.e.HilightBegCol = hilightBegCol;
    this.e.HilightEndRow = hilightEndRow;
    this.e.HilightEndCol = hilightEndCol;
    return num != 0;
  }

  internal new bool AdjustBlockForTable(
    ref int pBegRow,
    ref int pBegCol,
    ref int pEndRow,
    ref int pEndCol,
    bool AdjustCurPos)
  {
    int index1 = pBegRow;
    int index2 = pEndRow;
    int col1 = pBegCol;
    int col2 = pEndCol;
    bool flag1 = true;
    bool flag2 = true;
    int num1 = 0;
    int num2 = 0;
    if (this.e.HilightType != 2)
      return false;
    if (this.True(this.e.text[index1].tabw) && (this.e.text[index1].tabw.type & 32 /*0x20*/) != 0 && index1 > 0)
    {
      --index1;
      col1 = this.e.text[index1].len;
    }
    if (this.True(this.e.text[index2].tabw) && (this.e.text[index2].tabw.type & 32 /*0x20*/) != 0 && index2 > 0)
    {
      --index2;
      col2 = this.e.text[index2].len;
    }
    if ((this.e.TerFlags & 67108864 /*0x04000000*/) != 0)
    {
      int curCfmt1 = this.GetCurCfmt(index1, col1);
      int curCfmt2 = this.GetCurCfmt(index2, col2);
      bool flag3 = this.IsHypertext(curCfmt1);
      bool flag4 = this.IsHypertext(curCfmt2);
      if (this.e.CursDirection == 1)
      {
        if (flag3)
          this.GetHypertextStart(ref index1, ref col1);
        if (flag4)
        {
          this.GetHypertextEnd(ref index2, ref col2);
          this.e.CurLine = index2;
          this.e.CurCol = col2;
        }
      }
      else if (this.e.CursDirection == 2)
      {
        if (flag3)
          this.GetHypertextEnd(ref index1, ref col1);
        if (flag4)
        {
          this.GetHypertextStart(ref index2, ref col2);
          this.e.CurLine = index2;
          this.e.CurCol = col2;
        }
      }
    }
    if (index1 > index2)
    {
      this.SwapInts(ref index1, ref index2);
      this.SwapInts(ref col1, ref col2);
      flag2 = false;
    }
    if (index1 == index2 && col1 > col2)
    {
      this.SwapInts(ref col1, ref col2);
      flag2 = false;
    }
    if (this.e.TerArg.PageMode)
    {
      int level = this.MinTableLevel(index1, index2);
      int index3 = this.LevelCell(level, index1);
      int index4 = this.LevelCell(level, index2);
      if (index3 == index4 && index3 != 0)
      {
        if (this.TableLevel(index2) == level && this.LineInfo(index2, 16 /*0x10*/) && col2 == this.e.text[index2].len)
        {
          while (this.LevelCell(level, index1) == index3 && index1 >= 0)
            --index1;
          ++index1;
          col1 = 0;
        }
      }
      else
      {
        flag1 = this.InSameTable(index3, index4);
        if (flag1)
        {
          int cellColumn1 = this.GetCellColumn(index3, true);
          int num3 = cellColumn1 + this.e.cell[index3].ColSpan - 1;
          int cellColumn2 = this.GetCellColumn(index4, true);
          int num4 = cellColumn2 + this.e.cell[index4].ColSpan - 1;
          num1 = cellColumn1 < cellColumn2 ? cellColumn1 : cellColumn2;
          num2 = num3 > num4 ? num3 : num4;
        }
        if (index3 > 0)
        {
          int num5 = !flag1 ? 0 : num1;
          int cellColumn = this.GetCellColumn(index3, true);
          while (cellColumn > num5 && index1 > 0 && this.e.text[index1 - 1].cid != 0 && (!this.LineInfo(index1 - 1, 32 /*0x20*/) || level != this.TableLevel(index1 - 1)))
          {
            --index1;
            if (this.LineInfo(index1, 16 /*0x10*/) && level == this.TableLevel(index1))
              cellColumn -= this.e.cell[this.e.text[index1].cid].ColSpan;
          }
          int num6 = this.LevelCell(level, index1);
          while (index1 > 0 && this.LevelCell(level, index1 - 1) == num6)
            --index1;
          col1 = 0;
        }
        if (index4 > 0)
        {
          int num7 = this.GetCellColumn(index4, true) + this.e.cell[index4].ColSpan - 1;
          int num8;
          if (flag1)
          {
            num8 = num2;
          }
          else
          {
            num8 = 9999;
            if (this.e.HilightWithColCursor)
            {
              while (index2 + 1 < this.e.TotalLines && this.e.TableRow[this.e.cell[this.LevelCell(level, index2)].row].NextRow > 0)
                ++index2;
              num7 = 0;
            }
          }
          for (; num7 < num8 && index2 + 1 < this.e.TotalLines && this.e.text[index2 + 1].cid != 0 && (!this.LineInfo(index2 + 1, 32 /*0x20*/) || this.TableLevel(index2 + 1) != level); ++index2)
          {
            if (this.LineInfo(index2, 16 /*0x10*/) && this.TableLevel(index2) == level)
              num7 += this.e.cell[this.e.text[index2].cid].ColSpan;
          }
          int num9 = this.LevelCell(level, index2);
          while (index2 + 1 < this.e.TotalLines && (!this.LineInfo(index2, 16 /*0x10*/) || this.TableLevel(index2) != level) && this.LevelCell(level, index2 + 1) == num9)
            ++index2;
          col2 = this.e.text[index2].len;
          if (col2 < 0)
            col2 = 0;
        }
      }
    }
    if (!flag1 && this.e.text[index2].cid > 0 && index2 + 1 < this.e.TotalLines && this.LineInfo(index2 + 1, 32 /*0x20*/))
    {
      ++index2;
      col2 = 1;
    }
    if (!flag2)
    {
      this.SwapInts(ref index1, ref index2);
      this.SwapInts(ref col1, ref col2);
    }
    int num10 = pBegRow != index1 || pBegCol != col1 || pEndRow != index2 ? 1 : (pEndCol != col2 ? 1 : 0);
    pBegRow = index1;
    pBegCol = col1;
    pEndRow = index2;
    pEndCol = col2;
    if (!(this.e.StretchHilight & AdjustCurPos))
      return num10 != 0;
    this.e.CurLine = pEndRow;
    this.e.CurCol = pEndCol;
    if (this.e.CurCol >= this.e.text[this.e.CurLine].len && this.e.CurLine + 1 < this.e.TotalLines)
    {
      ++this.e.CurLine;
      this.e.CurCol = 0;
    }
    this.e.CurRow = this.e.CurLine - this.e.BeginLine;
    if (this.e.CurRow < 0)
      this.e.CurRow = 0;
    this.e.BeginLine = this.e.CurLine - this.e.CurRow;
    return num10 != 0;
  }

  internal new bool AdjustTableRowWidth(int row)
  {
    if (tc.DebugMode)
      this.misc.dm(nameof (AdjustTableRowWidth));
    int firstCell = this.e.TableRow[row].FirstCell;
    int parentCell = this.e.cell[firstCell].ParentCell;
    if (parentCell > 0)
    {
      int y = this.e.cell[parentCell].width - 2 * this.e.cell[parentCell].margin;
      int num;
      int z = num = 0;
      for (int index = firstCell; index > 0; index = this.e.cell[index].NextCell)
        z += this.e.cell[index].width;
      if ((this.e.TableRow[row].flags & 3) == 0)
        num = this.e.TableRow[row].indent;
      if (z + num > y)
        num = y - z;
      if (num >= 0)
      {
        this.e.TableRow[row].indent = num;
        return true;
      }
      this.e.TableRow[row].indent = 0;
      for (int index = firstCell; index > 0; index = this.e.cell[index].NextCell)
      {
        this.e.cell[index].width = this.MulDiv(this.e.cell[index].width, y, z);
        if (this.e.cell[index].width <= 2 * this.e.cell[index].margin)
          this.e.cell[index].width = 2 * this.e.cell[index].margin + 10;
      }
    }
    return true;
  }

  internal new bool CanInsertTable(int line, int col)
  {
    return (!this.True(this.e.text[line].fid) || (this.e.ParaFrame[this.e.text[line].fid].flags & 768 /*0x0300*/) == 0) && (!this.True(this.e.text[line].cid) || (this.e.TerFlags3 & 8192 /*0x2000*/) != 0) && this.CanInsertTextObject(line, col);
  }

  internal new bool CopyCell(int src, int dest)
  {
    this.e.cell[dest] = this.e.cell[src].Copy();
    this.e.CellAux[dest] = this.e.CellAux[src].Copy();
    return true;
  }

  internal new bool DelCell(int CurCell)
  {
    this.e.cell[CurCell].InUse = false;
    if (this.e.FirstFreeCellId == 0)
      this.e.FirstFreeCellId = CurCell;
    else if (CurCell < this.e.FirstFreeCellId)
      this.e.FirstFreeCellId = CurCell;
    return true;
  }

  internal new int GetCellColumn(int CurCell, bool UseColSpan)
  {
    int cellColumn = 0;
    if (CurCell >= 0 && CurCell < this.e.TotalCells && !this.False(this.e.cell[CurCell].InUse))
    {
      for (int index = this.e.TableRow[this.e.cell[CurCell].row].FirstCell; index != -1 && index != 0; index = this.e.cell[index].NextCell)
      {
        if (index == CurCell)
          return cellColumn;
        if (UseColSpan)
          cellColumn += this.e.cell[index].ColSpan;
        else
          ++cellColumn;
      }
    }
    return 0;
  }

  internal new bool GetCellMinMaxWidth(int cl, out int MinWidth, out int MaxWidth, int TblWidth)
  {
    int num1 = 0;
    int num2 = 0;
    int index1 = 0;
    int num3 = 0;
    bool flag1 = false;
    char minValue = char.MinValue;
    int num4;
    int num5 = num4 = 0;
    int level = this.e.cell[cl].level;
    for (int firstLine = this.e.cell[cl].FirstLine; firstLine <= this.e.cell[cl].LastLine; ++firstLine)
    {
      if (this.e.cell[this.e.text[firstLine].cid].level > level)
      {
        int num6 = 0;
        for (; firstLine < this.e.cell[cl].LastLine; ++firstLine)
        {
          int cid = this.e.text[firstLine].cid;
          if (cid != cl)
          {
            if (this.True(num6) && this.e.cell[cid].level == level + 1 && num6 != this.e.cell[cid].row)
            {
              int row = this.e.cell[cid].row;
              if (row == 0 || this.IsFirstTableRow(row))
                break;
            }
            if (num6 == 0 && this.e.cell[cid].level == level + 1)
              num6 = this.e.cell[cid].row;
          }
          else
            break;
        }
        if (num6 > 0)
        {
          int TblMinWidth;
          int TblMaxWidth;
          if (this.GetTableMinMaxWidths(num6, out TblMinWidth, out TblMaxWidth, out tc.SkipInt, out tc.SkipBool, out tc.SkipIntArray, out tc.SkipIntArray, 0))
          {
            if (TblMinWidth > num5)
              num5 = TblMinWidth;
            if (TblMaxWidth > num4)
              num4 = TblMaxWidth;
          }
          --firstLine;
          continue;
        }
      }
      int len = this.e.text[firstLine].len;
      num3 += len;
      char[] txt = this.e.text[firstLine].txt;
      ushort[] numArray = this.OpenCfmt(firstLine);
      bool flag2 = (this.e.PfmtId[this.e.text[firstLine].pfmt].pflags & 16 /*0x10*/) != 0;
      for (int index2 = 0; index2 <= len; ++index2)
      {
        if (!flag2 && index2 < len && (minValue == ' ' && txt[index2] != ' ' || (this.e.TerFont[index1].style & 128 /*0x80*/) != 0))
        {
          if (num1 > num5)
            num5 = num1;
          num1 = 0;
        }
        if (index2 == len & flag2)
        {
          if (num1 > num5)
            num5 = num1;
          num1 = 0;
        }
        if (index2 == len && (this.e.text[firstLine].flags & 129) != 0)
        {
          if (num2 > num4)
            num4 = num2;
          if (num1 > num5)
            num5 = num1;
          num2 = num1 = 0;
        }
        if (index2 != len)
        {
          int font = (int) numArray[index2];
          if ((this.e.TerFont[font].style & 128 /*0x80*/) != 0)
            flag1 = true;
          int num7 = !flag1 || this.e.TerFont[font].FrameType == 0 ? ((int) txt[index2] != (int) this.e.CellChar || !this.e.TerArg.ReadOnly || !flag1 || num3 != 2 ? this.fnt.LwrCharWidth(font, false, txt[index2]) : 0) : this.e.TerFont[font].PictWidth + 720;
          num1 += num7;
          num2 += num7;
          minValue = txt[index2];
          index1 = (int) numArray[index2];
        }
        else
          break;
      }
      this.CloseCfmt(firstLine);
    }
    int num8 = num4 + this.TwipsToUnitX(30);
    int num9 = num5 + this.TwipsToUnitX(30);
    if (num8 > TblWidth)
      num8 = TblWidth;
    if (num8 < num9)
      num8 = num9;
    MinWidth = num9;
    MaxWidth = num8;
    return true;
  }

  internal new int GetCellRightX(int CurCell)
  {
    if (CurCell < 0 || CurCell >= this.e.TotalCells || this.False(this.e.cell[CurCell].InUse))
      return 0;
    int row = this.e.cell[CurCell].row;
    int indent = this.e.TableRow[row].indent;
    for (int index = this.e.TableRow[row].FirstCell; index != -1 && index != 0; index = this.e.cell[index].NextCell)
    {
      indent += this.e.cell[index].width;
      if (index == CurCell)
        break;
    }
    return indent;
  }

  internal new int GetCellSlot(bool recover)
  {
    if ((this.e.TerFlags & 64 /*0x40*/) != 0)
    {
      this.ResetTerFlag(64 /*0x40*/);
      recover = false;
    }
    else
    {
      for (int CurCell = recover || this.e.FirstFreeCellId <= 0 ? 1 : this.e.FirstFreeCellId; CurCell < this.e.TotalCells; ++CurCell)
      {
        if (!this.e.cell[CurCell].InUse)
        {
          this.InitCell(CurCell);
          this.e.FirstFreeCellId = CurCell;
          return CurCell;
        }
      }
    }
    if (recover)
    {
      this.RecoverCellSlots();
      for (int CurCell = 1; CurCell < this.e.TotalCells; ++CurCell)
      {
        if (!this.e.cell[CurCell].InUse)
        {
          this.InitCell(CurCell);
          this.e.FirstFreeCellId = CurCell;
          return CurCell;
        }
      }
    }
    if (this.e.TotalCells >= this.e.MaxCells)
    {
      int count = this.e.MaxCells + this.e.MaxCells / 2;
      this.e.cell = this.ReAlloc(this.e.cell, count);
      this.e.CellAux = this.ReAlloc(this.e.CellAux, count);
      this.e.MaxCells = count;
    }
    ++this.e.TotalCells;
    this.InitCell(this.e.TotalCells - 1);
    this.e.FirstFreeCellId = this.e.TotalCells - 1;
    return this.e.TotalCells - 1;
  }

  internal new int GetColumnCell(int row, int col, bool UseColSpan)
  {
    if (row < 0 || row >= this.e.TotalTableRows || !this.e.TableRow[row].InUse)
      return 0;
    int columnCell;
    for (columnCell = this.e.TableRow[row].FirstCell; col > 0 && this.e.cell[columnCell].NextCell > 0; columnCell = this.e.cell[columnCell].NextCell)
    {
      if (UseColSpan)
        col -= this.e.cell[columnCell].ColSpan;
      else
        --col;
      if (col < 0)
        break;
    }
    return columnCell;
  }

  internal new int GetLastSpannedCellHeight(int CurCell, out int pScrHeight, int PageNo)
  {
    int num = 0;
    pScrHeight = 0;
    if ((this.e.cell[CurCell].flags & 16 /*0x10*/) == 0)
      return 0;
    int spanningCell = this.e.CellAux[CurCell].SpanningCell;
    int spannedCellHeight;
    if (PageNo >= 0)
    {
      int firstPage = this.e.CellAux[spanningCell].FirstPage;
      int lastPage = this.e.CellAux[spanningCell].LastPage;
      spannedCellHeight = PageNo != firstPage ? (PageNo != lastPage ? (firstPage == lastPage ? this.e.CellAux[spanningCell].height : this.e.PageInfo[PageNo].BodyHt) : this.e.CellAux[spanningCell].LastPageHt) : this.e.CellAux[spanningCell].FirstPageHt;
    }
    else
      spannedCellHeight = this.e.CellAux[spanningCell].height;
    int index = this.e.cell[CurCell].row;
    int row = this.e.cell[spanningCell].row;
    int rowSpan = this.e.cell[spanningCell].RowSpan;
    if (PageNo >= 0)
      num = this.LevelRow(this.e.cell[CurCell].level, this.e.PageInfo[PageNo].FirstRow);
    while (rowSpan > 1)
    {
      index = this.e.TableRow[index].PrevRow;
      if (index > 0)
      {
        spannedCellHeight -= this.e.TableRow[index].height;
        --rowSpan;
        if (index != row && rowSpan != 1)
        {
          if (PageNo >= 0 && index == num && index != row)
            return 0;
        }
        else
          break;
      }
      else
        break;
    }
    return spannedCellHeight;
  }

  internal new int GetNextCellInColumn(int CurCell, bool exact)
  {
    if (CurCell != -1)
    {
      int cellColumn = this.GetCellColumn(CurCell, false);
      int nextRow = this.e.TableRow[this.e.cell[CurCell].row].NextRow;
      if (nextRow == -1)
        return -1;
      int nextCellInColumn = this.e.TableRow[nextRow].FirstCell;
      int num1;
      for (int index = 0; index < cellColumn && this.e.cell[nextCellInColumn].NextCell != -1; index = num1 + 1)
      {
        num1 = index + (this.e.cell[nextCellInColumn].ColSpan - 1);
        nextCellInColumn = this.e.cell[nextCellInColumn].NextCell;
      }
      if (!exact)
        return nextCellInColumn;
      int num2 = Math.Abs(this.e.cell[nextCellInColumn].x - this.e.cell[CurCell].x);
      int num3 = Math.Abs(this.e.cell[nextCellInColumn].width - this.e.cell[CurCell].width);
      if (num2 < 60 && num3 < 60)
        return nextCellInColumn;
    }
    return -1;
  }

  internal new int GetNextCellInColumnPos(int CurCell)
  {
    if (CurCell == -1)
      return -1;
    int cellRightX = this.GetCellRightX(CurCell);
    int index = this.e.cell[CurCell].row;
    bool flag;
    int nextCellInColumnPos;
    do
    {
      index = this.e.TableRow[index].NextRow;
      if (index == -1)
        return -1;
      int CurCell1 = this.e.TableRow[index].FirstCell;
      flag = false;
      int num1 = nextCellInColumnPos = 0;
      while (true)
      {
        int num2 = Math.Abs(this.GetCellRightX(CurCell1) - cellRightX);
        if (num2 < 60 && (!flag || num2 < num1))
        {
          flag = true;
          nextCellInColumnPos = CurCell1;
          num1 = num2;
        }
        if (this.e.cell[CurCell1].NextCell != -1)
          CurCell1 = this.e.cell[CurCell1].NextCell;
        else
          break;
      }
    }
    while (!flag);
    return nextCellInColumnPos;
  }

  internal new int GetPrevCellInColumn(int CurCell, bool exact, bool UseIndex)
  {
    if (CurCell != -1)
    {
      int cellColumn = this.GetCellColumn(CurCell, true);
      int prevRow = this.e.TableRow[this.e.cell[CurCell].row].PrevRow;
      if (prevRow == -1)
        return -1;
      int prevCellInColumn = this.e.TableRow[prevRow].FirstCell;
      for (int index = this.e.cell[prevCellInColumn].ColSpan - 1; index < cellColumn && this.e.cell[prevCellInColumn].NextCell > 0; index = index + (this.e.cell[prevCellInColumn].ColSpan - 1) + 1)
        prevCellInColumn = this.e.cell[prevCellInColumn].NextCell;
      if (UseIndex)
        return prevCellInColumn;
      if (!exact)
        return this.e.cell[prevCellInColumn].x <= this.e.cell[CurCell].x && this.e.cell[prevCellInColumn].x + this.e.cell[prevCellInColumn].width >= this.e.cell[CurCell].x + this.e.cell[CurCell].width ? prevCellInColumn : -1;
      int num1 = Math.Abs(this.e.cell[prevCellInColumn].x - this.e.cell[CurCell].x);
      int num2 = Math.Abs(this.e.cell[prevCellInColumn].width - this.e.cell[CurCell].width);
      if (num1 < 60 && num2 < 60)
        return prevCellInColumn;
    }
    return -1;
  }

  internal new int GetPrevCellInColumnPos(int CurCell, bool exact)
  {
    if (CurCell == -1)
      return -1;
    int cellRightX = this.GetCellRightX(CurCell);
    int index = this.e.cell[CurCell].row;
    bool flag;
    int prevCellInColumnPos;
    do
    {
      index = this.e.TableRow[index].PrevRow;
      if (index == -1)
        return -1;
      int CurCell1 = this.e.TableRow[index].FirstCell;
      flag = false;
      int num1 = prevCellInColumnPos = 0;
      while (true)
      {
        int num2 = Math.Abs(this.GetCellRightX(CurCell1) - cellRightX);
        if (num2 < 60 && (!flag || num2 < num1))
        {
          flag = true;
          prevCellInColumnPos = CurCell1;
          num1 = num2;
        }
        if (this.e.cell[CurCell1].NextCell != -1)
          CurCell1 = this.e.cell[CurCell1].NextCell;
        else
          break;
      }
    }
    while (!flag);
    return prevCellInColumnPos;
  }

  internal new int GetRemainingCellSpans(int CurCell)
  {
    int row = this.e.cell[CurCell].row;
    int spanningCell = this.e.CellAux[CurCell].SpanningCell;
    int remainingCellSpans = this.e.cell[spanningCell].RowSpan;
    for (int index = this.e.cell[spanningCell].row; index != row && index > 0; index = this.e.TableRow[index].NextRow)
      --remainingCellSpans;
    if (remainingCellSpans < 1)
      remainingCellSpans = 1;
    return remainingCellSpans;
  }

  internal new int GetRowCell(int row, int col)
  {
    if (row < 0 || row >= this.e.TotalTableRows || this.False(this.e.TableRow[row].InUse))
      return -1;
    int rowCell = this.e.TableRow[row].FirstCell;
    for (; col > 0; --col)
    {
      rowCell = this.e.cell[rowCell].NextCell;
      if (rowCell == -1)
        break;
    }
    return rowCell;
  }

  internal new int GetRowWidth(int row)
  {
    int rowWidth = 0;
    if (row <= 0 || row >= this.e.TotalTableRows)
      return 0;
    for (int index = this.e.TableRow[row].FirstCell; index > 0; index = this.e.cell[index].NextCell)
      rowWidth += this.e.cell[index].width;
    return rowWidth;
  }

  internal new int GetSameColumnCell(int CellId, bool next)
  {
    int index1 = CellId;
    if (this.e.RepageBeginLine >= this.e.TotalLines)
    {
      int sameColumnCell = next ? this.e.CellAux[CellId].NextColCell : this.e.CellAux[CellId].PrevColCell;
      if (sameColumnCell > 0 && sameColumnCell < this.e.TotalCells && this.e.cell[sameColumnCell].InUse)
        return sameColumnCell;
    }
label_3:
    int row = this.e.cell[CellId].row;
    int index2 = !next ? this.e.TableRow[row].PrevRow : this.e.TableRow[row].NextRow;
    if (index2 > 0)
    {
      int x1 = this.e.cell[CellId].x;
      int num1 = x1 + this.e.cell[CellId].width;
      for (int sameColumnCell = this.e.TableRow[index2].FirstCell; sameColumnCell > 0; sameColumnCell = this.e.cell[sameColumnCell].NextCell)
      {
        int x2 = this.e.cell[sameColumnCell].x;
        int num2 = x2 + this.e.cell[sameColumnCell].width;
        if (Math.Abs(x1 - x2) <= 60 && Math.Abs(num1 - num2) <= 60)
        {
          if (next && (this.e.cell[sameColumnCell].flags & 16 /*0x10*/) != 0)
          {
            CellId = sameColumnCell;
            goto label_3;
          }
          if (next)
          {
            this.e.CellAux[index1].NextColCell = sameColumnCell;
            return sameColumnCell;
          }
          this.e.CellAux[index1].PrevColCell = sameColumnCell;
          return sameColumnCell;
        }
      }
    }
    return 0;
  }

  internal new int GetSpannedRowHeight(int CurCell, out int pScrHeight, int PageNo)
  {
    int spannedRowHeight = 0;
    int num1 = 0;
    int num2 = 0;
    int num3 = 0;
    pScrHeight = 0;
    int row = this.e.cell[CurCell].row;
    if (PageNo >= 0)
    {
      num2 = this.LevelRow(this.e.cell[CurCell].level, this.e.PageInfo[PageNo].FirstRow);
      num3 = this.LevelRow(this.e.cell[CurCell].level, this.e.PageInfo[PageNo].LastRow);
    }
    int num4;
    if ((this.e.cell[CurCell].flags & 16 /*0x10*/) != 0)
    {
      if (PageNo < 0 || row != num2)
        return 0;
      num4 = this.GetRemainingCellSpans(CurCell);
    }
    else
      num4 = this.e.cell[CurCell].RowSpan;
    if (PageNo < 0 || num3 != row)
    {
      for (int nextRow = this.e.TableRow[row].NextRow; nextRow > 0 && num4 > 1; --num4)
      {
        spannedRowHeight += this.e.TableRow[nextRow].height;
        num1 += this.UnitToScrY(this.e.TableRow[nextRow].height);
        if (PageNo < 0 || num3 != nextRow)
          nextRow = this.e.TableRow[nextRow].NextRow;
        else
          break;
      }
    }
    pScrHeight = num1;
    return spannedRowHeight;
  }

  internal new bool GetTableMinMaxWidths(
    int FirstRow,
    out int TblMinWidth,
    out int TblMaxWidth,
    out int TblWidth,
    out bool ExactWidth,
    out int[] pColMinWidth,
    out int[] pColMaxWidth,
    int EmbTblWidth)
  {
    int num1;
    TblMaxWidth = num1 = 0;
    TblMinWidth = num1;
    TblWidth = 0;
    ExactWidth = false;
    int[] numArray1;
    pColMaxWidth = numArray1 = (int[]) null;
    pColMinWidth = numArray1;
    int firstCell = this.e.TableRow[FirstRow].FirstCell;
    if (this.False(this.e.cell[firstCell].InUse))
      return false;
    int[] numArray2 = new int[300];
    int[] numArray3 = new int[300];
    bool[] flagArray1 = new bool[300];
    bool[] flagArray2 = new bool[300];
    ref tc.StrCell local = ref this.e.cell[firstCell];
    TblWidth = this.e.InPrinting || !this.e.TerArg.FittedView ? (int) (((double) this.e.TerSect1[0].PgWidth - (double) this.e.TerSect[0].LeftMargin - (double) this.e.TerSect[0].RightMargin) * 1440.0) : this.ScrToTwipsX(this.e.TerWinWidth);
    if (TblWidth < 0)
      TblWidth = 10080;
    if (EmbTblWidth > 0 && TblWidth > EmbTblWidth)
      TblWidth = EmbTblWidth;
    ExactWidth = false;
    if (this.True(this.e.TableRow[FirstRow].FixWidth))
    {
      TblWidth = this.e.TableRow[FirstRow].FixWidth >= 0 ? this.e.TableRow[FirstRow].FixWidth : this.MulDiv(TblWidth, -this.e.TableRow[FirstRow].FixWidth, 100);
      ExactWidth = true;
    }
    for (int index = 0; index < 300; ++index)
    {
      int num2;
      numArray3[index] = num2 = 0;
      numArray2[index] = num2;
      flagArray1[index] = false;
      flagArray2[index] = false;
    }
    int num3 = 0;
    int row = FirstRow;
    while (row > 0)
    {
      int index1 = 0;
      for (int cl = this.e.TableRow[row].FirstCell; cl > 0; cl = this.e.cell[cl].NextCell)
      {
        if (this.e.cell[cl].FixWidth == 0)
          this.e.cell[cl].flags |= 8;
        int MinWidth1;
        int MaxWidth1;
        bool flag;
        if ((this.e.cell[cl].flags & 8) != 0 || !ExactWidth && this.e.cell[cl].FixWidth < 0)
        {
          this.GetCellMinMaxWidth(cl, out MinWidth1, out MaxWidth1, TblWidth);
          MinWidth1 += 2 * this.e.cell[cl].margin;
          MaxWidth1 += 2 * this.e.cell[cl].margin;
          flag = false;
        }
        else
        {
          if (this.e.cell[cl].FixWidth < 0)
          {
            MinWidth1 = this.MulDiv(TblWidth, -this.e.cell[cl].FixWidth, 100);
            if (this.e.cell[cl].level > 0 && EmbTblWidth == 0)
              MinWidth1 = 2 * this.e.cell[cl].margin;
          }
          else
            MinWidth1 = this.e.cell[cl].FixWidth;
          MaxWidth1 = MinWidth1;
          flag = true;
          int MinWidth2;
          int MaxWidth2;
          this.GetCellMinMaxWidth(cl, out MinWidth2, out MaxWidth2, TblWidth);
          if (this.e.cell[cl].FixWidth > 0 && this.e.cell[cl].FixWidth > MinWidth2)
            MinWidth2 = MaxWidth2 = MinWidth1;
          else if (MinWidth2 > MinWidth1 && (this.e.TerFlags3 & 64 /*0x40*/) == 0)
            MinWidth1 = MaxWidth1 = MinWidth2;
          else
            flag = true;
        }
        if (this.e.cell[cl].FirstLine != this.e.cell[cl].LastLine || this.e.text[this.e.cell[cl].FirstLine].len > 1)
          flagArray2[index1] = true;
        if (this.e.cell[cl].ColSpan < 1)
          this.e.cell[cl].ColSpan = 1;
        if (this.e.cell[cl].ColSpan == 1)
        {
          if (!flagArray1[index1] | flag)
          {
            if (MinWidth1 > numArray2[index1] | flag)
              numArray2[index1] = MinWidth1;
            if (MaxWidth1 > numArray3[index1] | flag)
              numArray3[index1] = MaxWidth1;
          }
          if (flag)
            flagArray1[index1] = true;
          ++index1;
        }
        else
        {
          int num4;
          int num5 = num4 = 0;
          int colSpan = this.e.cell[cl].ColSpan;
          for (int index2 = 0; index2 < colSpan; ++index2)
          {
            num5 += numArray2[index1 + index2];
            num4 += numArray3[index1 + index2];
          }
          if (!flagArray1[index1] | flag)
          {
            if (MinWidth1 > num5)
            {
              int num6 = (MinWidth1 - num5) / colSpan;
              for (int index3 = 0; index3 < colSpan; ++index3)
              {
                int[] numArray4;
                IntPtr index4;
                (numArray4 = numArray2)[(int) (index4 = (IntPtr) (index1 + index3))] = numArray4[(int) index4] + num6;
              }
            }
            if (MaxWidth1 > num5)
            {
              int num7 = (MaxWidth1 - num5) / colSpan;
              for (int index5 = 0; index5 < colSpan; ++index5)
              {
                int[] numArray5;
                IntPtr index6;
                (numArray5 = numArray3)[(int) (index6 = (IntPtr) (index1 + index5))] = numArray5[(int) index6] + num7;
              }
            }
          }
          if (flag)
            flagArray1[index1] = true;
          if (flagArray2[index1])
          {
            for (int index7 = 1; index7 < colSpan; ++index7)
              flagArray2[index1 + index7] = true;
          }
          index1 += colSpan;
        }
      }
      if (index1 > num3)
        num3 = index1;
      row = this.e.TableRow[row].NextRow;
      if (row > 0 && this.IsFirstTableRow(row))
        row = 0;
    }
    TblMinWidth = 0;
    TblMaxWidth = 0;
    for (int index = 0; index < num3; ++index)
    {
      if (numArray2[index] > numArray3[index])
        numArray3[index] = numArray2[index];
      if (!flagArray2[index] && !flagArray1[index])
      {
        int num8;
        numArray3[index] = num8 = 100;
        numArray2[index] = num8;
      }
      TblMinWidth += numArray2[index];
      TblMaxWidth += numArray3[index];
    }
    pColMinWidth = numArray2;
    pColMaxWidth = numArray3;
    return true;
  }

  internal new int GetTableRowSlot()
  {
    for (int tableRowSlot = 1; tableRowSlot < this.e.TotalTableRows; ++tableRowSlot)
    {
      if (!this.e.TableRow[tableRowSlot].InUse)
      {
        tc.StrTableRow strTableRow = new tc.StrTableRow();
        this.e.TableRow[tableRowSlot] = strTableRow.init();
        this.e.TableAux[tableRowSlot] = new tc.StrTableAux();
        return tableRowSlot;
      }
    }
    if (!this.e.InRtfRead && (this.e.TerFlags3 & 65536 /*0x010000*/) == 0 && (this.e.TerOpFlags & 16777216 /*0x01000000*/) == 0)
    {
      this.RecoverCellSlots();
      this.RecoverTableRowSlots();
    }
    for (int tableRowSlot = 1; tableRowSlot < this.e.TotalTableRows; ++tableRowSlot)
    {
      if (!this.e.TableRow[tableRowSlot].InUse)
      {
        tc.StrTableRow strTableRow = new tc.StrTableRow();
        this.e.TableRow[tableRowSlot] = strTableRow.init();
        this.e.TableAux[tableRowSlot] = new tc.StrTableAux();
        return tableRowSlot;
      }
    }
    if (this.e.TotalTableRows >= this.e.MaxTableRows)
    {
      int count = this.e.MaxTableRows + this.e.MaxTableRows / 2;
      this.e.TableRow = this.ReAlloc(this.e.TableRow, count);
      this.e.TableAux = this.ReAlloc(this.e.TableAux, count);
      this.e.MaxTableRows = count;
    }
    ++this.e.TotalTableRows;
    this.e.TableRow[this.e.TotalTableRows - 1] = new tc.StrTableRow().init();
    this.e.TableAux[this.e.TotalTableRows - 1] = new tc.StrTableAux();
    return this.e.TotalTableRows - 1;
  }

  internal bool GetTableSelRange(out int FirstLine, out int LastLine)
  {
    int num;
    LastLine = num = -1;
    FirstLine = num;
    for (int index = 0; index < this.e.TotalCells; ++index)
    {
      if (!this.False(this.e.cell[index].InUse) && (this.e.cell[index].flags & 3) != 0)
      {
        if (FirstLine == -1)
          FirstLine = this.e.cell[index].FirstLine;
        if (this.e.cell[index].FirstLine < FirstLine)
          FirstLine = this.e.cell[index].FirstLine;
        if (this.e.cell[index].LastLine > LastLine)
          LastLine = this.e.cell[index].LastLine;
      }
    }
    return true;
  }

  internal new int GetTblSpcBef(int LineNo, bool screen)
  {
    int tblSpcBef = 0;
    int page = this.e.text[LineNo].page;
    int num1 = 0;
    int LineNo1 = LineNo;
    if (!this.e.TerArg.PageMode)
      return 0;
    int LineNo2;
    for (; LineNo != 0 && this.TableLevel(LineNo - 1) > this.TableLevel(LineNo1); LineNo = LineNo2 + 1)
    {
      int cid = this.e.text[LineNo - 1].cid;
      if (cid <= 0)
        return tblSpcBef;
      for (int index = this.e.cell[cid].row; index > 0; index = this.e.TableRow[index].PrevRow)
      {
        num1 = index;
        if (page > this.e.TableAux[index].LastPage)
          return tblSpcBef;
        int x = (this.e.TableRow[index].flags & 16 /*0x10*/) == 0 ? this.e.TableRow[index].height : this.e.TableAux[index].TopRowHt;
        if (screen)
          tblSpcBef += this.UnitToScrY(x);
        else
          tblSpcBef += x;
        if ((this.e.TableRow[index].flags & 16 /*0x10*/) != 0)
          return tblSpcBef;
      }
      if (!this.e.HtmlMode)
        return tblSpcBef;
      int num2 = this.TableLevel(LineNo - 1);
      LineNo2 = LineNo - 1;
      if (this.e.cell[this.e.text[LineNo2].cid].row != num1)
      {
        while (LineNo2 >= 0 && this.e.cell[this.e.text[LineNo2].cid].row != num1)
          --LineNo2;
      }
      while (LineNo2 >= 0 && (this.TableLevel(LineNo2) > num2 || this.e.cell[this.e.text[LineNo2].cid].row == num1))
        --LineNo2;
    }
    return tblSpcBef;
  }

  internal new bool HilightTableCol(int LineNo, bool IsNew, bool repaint)
  {
    int num = 20;
    if (LineNo >= 0 && LineNo < this.e.TotalLines && this.e.text[LineNo].cid != 0)
    {
      int hilightType = this.e.HilightType;
      int hilightBegRow = this.e.HilightBegRow;
      int hilightEndRow = this.e.HilightEndRow;
      int hilightBegCol = this.e.HilightBegCol;
      int hilightEndCol = this.e.HilightEndCol;
      int CurCell = this.e.text[LineNo].cid;
      int row1;
      int row2 = row1 = this.e.cell[CurCell].row;
      while (!this.IsFirstTableRow(row2))
        row2 = this.e.TableRow[row2].PrevRow;
      int cellColumn = this.GetCellColumn(CurCell, true);
      if (this.e.cell[CurCell].row != row2)
        CurCell = this.GetColumnCell(row2, cellColumn, true);
      while (!this.IsLastTableRow(row1))
        row1 = this.e.TableRow[row1].NextRow;
      int index1 = this.GetColumnCell(row1, cellColumn, true);
      if (Math.Abs(this.e.cell[index1].x - this.e.cell[CurCell].x) > num)
      {
        while ((row1 = this.e.TableRow[row1].PrevRow) > 0)
        {
          index1 = this.GetColumnCell(row1, cellColumn, true);
          if (Math.Abs(this.e.cell[index1].x - this.e.cell[CurCell].x) < num)
            break;
        }
        if (row1 <= 0)
          index1 = CurCell;
      }
      if (this.e.cell[CurCell].ColSpan > 1)
      {
        for (int colSpan = this.e.cell[CurCell].ColSpan; colSpan > 1 && this.e.cell[index1].NextCell > 0; index1 = this.e.cell[index1].NextCell)
        {
          colSpan -= this.e.cell[index1].ColSpan;
          if (colSpan > 0)
          {
            if (Math.Abs(this.e.cell[CurCell].x + this.e.cell[CurCell].width - (this.e.cell[index1].x + this.e.cell[index1].width)) < num)
            {
              this.e.cell[CurCell].ColSpan -= colSpan;
              if (this.e.cell[CurCell].ColSpan < 1)
              {
                this.e.cell[CurCell].ColSpan = 1;
                break;
              }
              break;
            }
          }
          else
            break;
        }
      }
      if (this.e.HilightType != 2 | IsNew)
      {
        int index2 = LineNo;
        while (index2 >= 0 && this.e.text[index2].cid != CurCell)
          --index2;
        while (index2 >= 0 && this.e.text[index2].cid == CurCell)
          --index2;
        this.e.HilightBegRow = index2 + 1;
        this.e.HilightBegCol = 0;
        this.e.HilightType = 2;
      }
      int index3 = LineNo;
      while (index3 < this.e.TotalLines && this.e.text[index3].cid > 0 && this.e.text[index3].cid != index1)
        ++index3;
      if (index3 < this.e.TotalLines)
      {
        while (index3 < this.e.TotalLines && this.e.text[index3].cid == index1)
          ++index3;
      }
      int index4 = index3 - 1;
      if (index4 >= this.e.TotalLines)
        --index4;
      if (index4 < 0)
        index4 = 0;
      if (this.True(this.e.text[index4].tabw) && (this.e.text[index4].tabw.type & 32 /*0x20*/) != 0)
        --index4;
      this.e.HilightEndRow = index4;
      this.e.HilightEndCol = this.e.text[index4].len - 1;
      if (this.e.HilightBegRow == this.e.HilightEndRow && this.e.HilightBegCol == this.e.HilightEndCol)
        ++this.e.HilightEndCol;
      if (this.e.HilightEndCol < 0)
        this.e.HilightEndCol = 0;
      this.e.StretchHilight = true;
      this.e.HilightWithColCursor = this.e.TblSelCursShowing;
      if (repaint && (this.e.HilightType != hilightType || this.e.HilightBegRow != hilightBegRow || this.e.HilightBegCol != hilightBegCol || this.e.HilightEndRow != hilightEndRow || this.e.HilightEndCol != hilightEndCol))
        this.PaintTer();
    }
    return true;
  }

  internal new bool InitCell(int CurCell)
  {
    this.e.cell[CurCell] = new tc.StrCell();
    this.e.CellAux[CurCell] = new tc.StrCellAux();
    this.e.cell[CurCell].BackColor = tc.CLR_WHITE;
    int num;
    this.e.cell[CurCell].ColSpan = num = 1;
    this.e.cell[CurCell].RowSpan = num;
    this.e.cell[CurCell].BorderWidth = new int[4];
    this.e.cell[CurCell].BorderColor = new Color[4];
    for (int index = 0; index < 4; ++index)
      this.e.cell[CurCell].BorderColor[index] = tc.CLR_AUTO;
    return true;
  }

  internal new bool InOuterLevels(int level, int LineNo)
  {
    int index = LineNo < 0 ? -LineNo : this.e.text[LineNo].cid;
    return index == 0 || this.e.cell[index].level < level;
  }

  internal new bool InSameTable(int cell1, int cell2)
  {
    if (cell1 == 0 || cell2 == 0)
      return false;
    int row1 = this.e.cell[cell1].row;
    while (!this.IsFirstTableRow(row1))
      row1 = this.e.TableRow[row1].PrevRow;
    int row2 = this.e.cell[cell2].row;
    while (!this.IsFirstTableRow(row2))
      row2 = this.e.TableRow[row2].PrevRow;
    return row1 == row2;
  }

  internal new bool InsertCell(int NewCell, int CurCell, int CurRowId, char type)
  {
    if (this.e.TableRow[CurRowId].FirstCell <= 0 || this.e.TableRow[CurRowId].LastCell <= 0)
    {
      int num;
      this.e.TableRow[CurRowId].LastCell = num = NewCell;
      this.e.TableRow[CurRowId].FirstCell = num;
    }
    else
    {
      switch (type)
      {
        case 'A':
          if (CurCell == this.e.TableRow[CurRowId].LastCell)
          {
            int lastCell = this.e.TableRow[CurRowId].LastCell;
            this.e.TableRow[CurRowId].LastCell = NewCell;
            this.e.cell[lastCell].NextCell = NewCell;
            this.e.cell[NewCell].PrevCell = lastCell;
            this.e.cell[NewCell].NextCell = -1;
            break;
          }
          if (CurCell <= 0)
          {
            int firstCell = this.e.TableRow[CurRowId].FirstCell;
            this.e.TableRow[CurRowId].FirstCell = NewCell;
            this.e.cell[NewCell].PrevCell = -1;
            this.e.cell[NewCell].NextCell = firstCell;
            this.e.cell[firstCell].PrevCell = NewCell;
            break;
          }
          int nextCell = this.e.cell[CurCell].NextCell;
          this.e.cell[NewCell].NextCell = nextCell;
          if (nextCell > 0)
            this.e.cell[nextCell].PrevCell = NewCell;
          this.e.cell[NewCell].PrevCell = CurCell;
          this.e.cell[CurCell].NextCell = NewCell;
          break;
        case 'B':
          if (CurCell == this.e.TableRow[CurRowId].FirstCell)
          {
            this.e.TableRow[CurRowId].FirstCell = NewCell;
            this.e.cell[NewCell].PrevCell = -1;
            this.e.cell[NewCell].NextCell = CurCell;
            this.e.cell[CurCell].PrevCell = NewCell;
            break;
          }
          if (CurCell <= 0)
          {
            int lastCell = this.e.TableRow[CurRowId].LastCell;
            this.e.TableRow[CurRowId].LastCell = NewCell;
            this.e.cell[lastCell].NextCell = NewCell;
            this.e.cell[NewCell].PrevCell = lastCell;
            this.e.cell[NewCell].NextCell = -1;
            break;
          }
          int prevCell = this.e.cell[CurCell].PrevCell;
          this.e.cell[NewCell].PrevCell = prevCell;
          if (prevCell > 0)
            this.e.cell[prevCell].NextCell = NewCell;
          this.e.cell[NewCell].NextCell = CurCell;
          this.e.cell[CurCell].PrevCell = NewCell;
          break;
      }
    }
    this.e.cell[NewCell].InUse = true;
    this.e.cell[NewCell].row = CurRowId;
    return true;
  }

  internal new bool IsBaselineAlignedCellLine(int line)
  {
    int cid = this.e.text[line].cid;
    return cid > 0 && (this.e.cell[cid].flags & 65536 /*0x010000*/) != 0;
  }

  internal new bool IsFirstTableRow(int row)
  {
    return this.e.TableRow[row].PrevRow <= 0 || this.e.HtmlMode && (this.e.TableRow[row].flags & 32768 /*0x8000*/) != 0;
  }

  internal new bool IsLastSpannedCell(int cl)
  {
    return (this.e.cell[cl].flags & 16 /*0x10*/) != 0 && this.GetRemainingCellSpans(cl) <= 1;
  }

  internal new bool IsLastTableRow(int row)
  {
    if (this.e.TableRow[row].NextRow <= 0)
      return true;
    if (this.e.HtmlMode)
    {
      int nextRow = this.e.TableRow[row].NextRow;
      if (nextRow > 0 && (this.e.TableRow[nextRow].flags & 32768 /*0x8000*/) != 0)
        return true;
    }
    return false;
  }

  internal new bool IsPartRow(bool top, int row, int PageNo)
  {
    if ((this.e.TableRow[row].flags & 16 /*0x10*/) == 0)
      return false;
    if (!top)
      return PageNo == this.e.TableAux[row].FirstPage;
    return PageNo > this.e.TableAux[row].FirstPage && PageNo <= this.e.TableAux[row].LastPage;
  }

  internal new bool IsSpannedRow(int row)
  {
    if (row < 0)
      row = this.e.cell[-row].row;
    for (int index = this.e.TableRow[row].FirstCell; index > 0; index = this.e.cell[index].NextCell)
    {
      if ((this.e.cell[index].flags & 16 /*0x10*/) != 0)
        return true;
    }
    return false;
  }

  internal new bool IsSpanningRow(int row)
  {
    if (row < 0)
      row = this.e.cell[-row].row;
    for (int index = this.e.TableRow[row].FirstCell; index > 0; index = this.e.cell[index].NextCell)
    {
      if (this.e.cell[index].RowSpan > 1)
        return true;
    }
    return false;
  }

  internal new int LevelCell(int level, int LineNo)
  {
    int index = LineNo < 0 ? -LineNo : this.e.text[LineNo].cid;
    if (index != 0 && this.e.cell[index].level != 0 && this.e.cell[index].level != level)
    {
      while ((index = this.e.cell[index].ParentCell) > 0 && this.e.cell[index].level != level)
        ;
    }
    return index;
  }

  internal new int LevelRow(int level, int row)
  {
    int firstCell = this.e.TableRow[row].FirstCell;
    return this.e.cell[firstCell].level == 0 ? row : this.e.cell[this.LevelCell(level, -firstCell)].row;
  }

  internal new bool MarkCells(int select)
  {
    for (int index = 0; index < this.e.TotalCells; ++index)
      this.e.cell[index].flags = tc.ResetUintFlag(ref this.e.cell[index].flags, 3);
    if (select == 942)
    {
      int cid = this.e.text[this.e.CurLine].cid;
      if (cid != 0)
      {
        int index1 = this.e.cell[cid].row;
        while (this.e.TableRow[index1].PrevRow > 0)
          index1 = this.e.TableRow[index1].PrevRow;
        for (; index1 > 0; index1 = this.e.TableRow[index1].NextRow)
        {
          for (int index2 = this.e.TableRow[index1].FirstCell; index2 > 0; index2 = this.e.cell[index2].NextCell)
            this.e.cell[index2].flags |= 1;
        }
      }
      return true;
    }
    if (this.e.HilightType == 0)
    {
      int cid = this.e.text[this.e.CurLine].cid;
      if (cid > 0)
        this.e.cell[cid].flags |= 1;
    }
    else
    {
      if (!this.NormalizeBlock())
        return false;
      int num = this.MinTableLevel(this.e.HilightBegRow, this.e.HilightEndRow);
      int index = 0;
      for (int hilightBegRow = this.e.HilightBegRow; hilightBegRow <= this.e.HilightEndRow; ++hilightBegRow)
      {
        if (this.LineSelected(hilightBegRow))
        {
          int cid = this.e.text[hilightBegRow].cid;
          if (this.e.cell[cid].level <= num && cid != index)
          {
            index = cid;
            if (index > 0)
              this.e.cell[index].flags |= 1;
          }
        }
      }
    }
    if (select == 887)
    {
      for (int index = 0; index < this.e.TotalCells; ++index)
      {
        if (!this.False(this.e.cell[index].InUse) && (this.e.cell[index].flags & 1) != 0)
        {
          int CurCell1 = index;
          while ((CurCell1 = this.GetNextCellInColumnPos(CurCell1)) > 0)
            this.e.cell[CurCell1].flags |= 2;
          int CurCell2 = index;
          while ((CurCell2 = this.GetPrevCellInColumnPos(CurCell2, true)) > 0)
            this.e.cell[CurCell2].flags |= 2;
        }
      }
    }
    if (select == 888)
    {
      for (int index3 = 0; index3 < this.e.TotalCells; ++index3)
      {
        if (!this.False(this.e.cell[index3].InUse) && (this.e.cell[index3].flags & 1) != 0)
        {
          for (int index4 = this.e.TableRow[this.e.cell[index3].row].FirstCell; index4 > 0; index4 = this.e.cell[index4].NextCell)
            this.e.cell[index4].flags |= 2;
        }
      }
    }
    for (int index = 0; index < this.e.TotalCells; ++index)
    {
      if (!this.False(this.e.cell[index].InUse) && (this.e.cell[index].flags & 3) == 0)
      {
        int spanningCell = this.e.CellAux[index].SpanningCell;
        if (spanningCell != 0 && spanningCell >= 0 && spanningCell < this.e.TotalCells && (this.e.cell[spanningCell].flags & 3) != 0)
          this.e.cell[index].flags |= 2;
      }
    }
    return true;
  }

  internal new int MinTableLevel(int FromLine, int ToLine)
  {
    int num1 = this.TableLevel(FromLine);
    if (num1 == 0 && this.TableLevel(ToLine) == 0)
      return 0;
    if (FromLine > ToLine)
      this.SwapInts(ref FromLine, ref ToLine);
    int num2 = num1;
    for (int LineNo = FromLine + 1; LineNo <= ToLine; ++LineNo)
    {
      int num3 = this.TableLevel(LineNo);
      if (num3 < num2)
        num2 = num3;
    }
    return num2;
  }

  internal new bool RecoverCellSlots()
  {
    for (int index = 1; index < this.e.TotalCells; ++index)
      this.e.cell[index].InUse = false;
    for (int index = 0; index < this.e.TotalLines; ++index)
      this.e.cell[this.e.text[index].cid].InUse = true;
    this.e.cell[0].InUse = true;
    return true;
  }

  internal new bool RecoverTableRowSlots()
  {
    for (int index = 1; index < this.e.TotalTableRows; ++index)
      this.e.TableRow[index].InUse = false;
    for (int index = 0; index < this.e.TotalCells; ++index)
    {
      if (this.e.cell[index].InUse)
        this.e.TableRow[this.e.cell[index].row].InUse = true;
    }
    this.e.TableRow[0].InUse = true;
    return true;
  }

  internal new bool RemoveCell(int CurCell)
  {
    int row = this.e.cell[CurCell].row;
    if (CurCell == this.e.TableRow[row].FirstCell && CurCell == this.e.TableRow[row].LastCell)
    {
      int num;
      this.e.TableRow[row].LastCell = num = -1;
      this.e.TableRow[row].FirstCell = num;
    }
    else if (CurCell == this.e.TableRow[row].FirstCell)
    {
      int nextCell = this.e.cell[CurCell].NextCell;
      this.e.TableRow[row].FirstCell = nextCell;
      if (nextCell > 0)
        this.e.cell[nextCell].PrevCell = -1;
    }
    else if (CurCell == this.e.TableRow[row].LastCell)
    {
      int prevCell = this.e.cell[CurCell].PrevCell;
      this.e.TableRow[row].LastCell = prevCell;
      if (prevCell > 0)
        this.e.cell[prevCell].NextCell = -1;
    }
    else
    {
      int prevCell = this.e.cell[CurCell].PrevCell;
      int nextCell = this.e.cell[CurCell].NextCell;
      if (prevCell > 0)
        this.e.cell[prevCell].NextCell = nextCell;
      if (nextCell > 0)
        this.e.cell[nextCell].PrevCell = prevCell;
    }
    this.e.cell[CurCell].InUse = false;
    if (this.e.FirstFreeCellId == 0)
      this.e.FirstFreeCellId = CurCell;
    else if (CurCell < this.e.FirstFreeCellId)
      this.e.FirstFreeCellId = CurCell;
    return true;
  }

  internal new int RepairOneTable(int FirstLine, int level)
  {
    int num1;
    int index1 = num1 = -1;
    int index2;
    for (index2 = FirstLine; index2 < this.e.TotalLines; ++index2)
    {
      int cid1 = this.e.text[index2].cid;
      if (this.e.cell[cid1].level < 0)
        this.e.cell[cid1].level = 0;
      if (this.TableLevel(index2) > level)
      {
        index2 = this.RepairOneTable(index2, level + 1);
      }
      else
      {
        int cid2 = this.e.text[index2].cid;
        if (cid2 == 0 || this.e.cell[cid2].level < level)
          return index2 - 1;
        if (this.e.cell[cid2].width <= 2 * this.e.cell[cid2].margin && !this.e.HtmlMode)
        {
          this.MoveLineArrays(index2, 1, 'D');
          this.e.cell[cid2].InUse = false;
        }
        else
        {
          int index3 = cid2;
          this.e.cell[index3].PrevCell = -1;
          this.e.cell[index3].NextCell = -1;
          this.e.cell[index3].x = 0;
          int index4 = index3;
          int id = this.e.TableRow[this.e.cell[index3].row].id;
          int num2 = -1;
          for (; index2 < this.e.TotalLines; ++index2)
          {
            if (this.TableLevel(index2) > level)
            {
              index2 = this.RepairOneTable(index2, level + 1);
            }
            else
            {
              int cid3 = this.e.text[index2].cid;
              if (this.True(this.e.text[index2].tabw) && (this.e.text[index2].tabw.type & 32 /*0x20*/) != 0)
              {
                if (cid3 > 0 && !this.e.cell[cid3].InUse)
                  this.e.text[index2].cid = cid2;
                if (this.e.InRtfRead && cid3 != cid2)
                {
                  this.e.text[index2].cid = cid2;
                  break;
                }
                break;
              }
              if (cid3 > 0 && this.e.cell[cid3].width <= 2 * this.e.cell[cid3].margin && !this.e.HtmlMode)
              {
                int num3 = this.e.cell[cid3].width / 4;
                if (num3 < 10)
                {
                  this.MoveLineArrays(index2, 1, 'D');
                  this.e.cell[cid3].InUse = false;
                  --index2;
                  continue;
                }
                this.e.cell[cid3].margin = num3;
              }
              if (this.True(this.e.text[index2].tabw) && (this.e.text[index2].tabw.type & 16 /*0x10*/) != 0)
                num2 = index2;
              cid2 = this.e.text[index2].cid;
              if (cid2 != 0)
              {
                int num4;
                this.e.CellAux[cid2].NextColCell = num4 = 0;
                this.e.CellAux[cid2].PrevColCell = num4;
                if (cid2 != index4)
                {
                  this.e.cell[index4].NextCell = cid2;
                  this.e.cell[cid2].PrevCell = index4;
                  this.e.cell[cid2].NextCell = -1;
                  this.e.cell[cid2].x = this.e.cell[index4].x + this.e.cell[index4].width;
                  index4 = cid2;
                }
              }
              else
                break;
            }
          }
          if ((index2 == this.e.TotalLines || cid2 == 0 || this.False(this.e.text[index2].tabw) || (this.e.text[index2].tabw.type & 32 /*0x20*/) == 0) && this.CheckLineLimit(this.e.TotalLines + 1))
          {
            this.InsertMarkerLine(index2, '\u0012', 0, 0, 32 /*0x20*/, this.e.text[index2 - 1].cid);
            if (index2 < this.e.RepageBeginLine)
              this.e.RepageBeginLine = index2;
          }
          if (num2 >= 0 && num2 + 1 < index2)
          {
            this.MoveLineArrays(num2 + 1, index2 - num2 - 1, 'D');
            index2 = num2 + 1;
          }
          if ((this.e.text[index2].tabw.type & 32 /*0x20*/) != 0 && index2 > 0 && this.LevelCell(level, index2) != this.LevelCell(level, index2 - 1) && this.CheckLineLimit(this.e.TotalLines + 1))
          {
            this.InsertMarkerLine(index2, this.e.CellChar, 0, -1, 16 /*0x10*/, -1);
            this.e.text[index2].pfmt = this.e.text[index2 + 1].pfmt;
            this.e.text[index2].cid = this.e.text[index2 + 1].cid;
            if (index2 < this.e.RepageBeginLine)
              this.e.RepageBeginLine = index2;
            ++index2;
          }
          int row = this.e.cell[index4].row;
          if ((this.e.TableRow[row].flags & 32768 /*0x8000*/) != 0 && this.e.HtmlMode)
            index1 = -1;
          this.e.TableRow[row].FirstCell = index3;
          this.e.TableRow[row].LastCell = 0;
          this.e.TableRow[row].PrevRow = index1;
          this.e.TableRow[row].NextRow = -1;
          this.e.TableRow[row].id = id;
          if (index1 >= 0)
            this.e.TableRow[index1].NextRow = row;
          index1 = row;
          for (int index5 = index3; index5 > 0; index5 = this.e.cell[index5].NextCell)
          {
            this.e.cell[index5].row = row;
            this.e.TableRow[row].LastCell = index5;
          }
        }
      }
    }
    return index2 - 1;
  }

  internal new bool RepairTable()
  {
    if (this.e.TotalTableRows != 1 || this.e.TotalCells != 1)
    {
      int index1 = 0;
      for (int index2 = 0; index2 < this.e.TotalTableRows; ++index2)
        tc.ResetUintFlag(ref this.e.TableRow[index2].flags, 8);
      bool flag1 = false;
      for (int LineNo = this.e.TotalLines - 1; LineNo >= 0; --LineNo)
      {
        if (this.TableLevel(LineNo) > 0)
          this.e.TableRow[this.e.cell[this.e.cell[this.e.text[LineNo].cid].ParentCell].row].flags |= 8;
        if (flag1)
        {
          if (this.e.text[LineNo].cid == 0)
            flag1 = false;
          else if (this.True(this.e.text[LineNo].tabw) && (this.e.text[LineNo].tabw.type & 48 /*0x30*/) != 0)
            index1 = this.e.text[LineNo].cid;
          else if (this.e.cell[this.e.text[LineNo].cid].level < this.e.cell[index1].level)
            index1 = this.e.text[LineNo].cid;
          else if (this.e.text[LineNo].cid != index1 && this.e.InRtfRead)
          {
            this.e.text[LineNo].cid = index1;
            if (LineNo < this.e.RepageBeginLine)
              this.e.RepageBeginLine = LineNo;
          }
        }
        else if (this.True(this.e.text[LineNo].tabw) && (this.e.text[LineNo].tabw.type & 48 /*0x30*/) != 0)
        {
          flag1 = true;
          index1 = this.e.text[LineNo].cid;
        }
        else if (this.e.text[LineNo].cid > 0)
        {
          this.e.text[LineNo].cid = 0;
          if (LineNo < this.e.RepageBeginLine)
            this.e.RepageBeginLine = LineNo;
        }
      }
      for (int FirstLine = 0; FirstLine < this.e.TotalLines; ++FirstLine)
      {
        if (this.e.text[FirstLine].cid > 0)
          FirstLine = this.RepairOneTable(FirstLine, 0);
      }
      for (int index3 = 0; index3 < this.e.TotalCells; ++index3)
      {
        if (this.e.cell[index3].InUse)
          this.e.cell[index3].RowSpan = 1;
      }
      for (int index4 = 0; index4 < this.e.TotalTableRows; ++index4)
      {
        if (this.e.TableRow[index4].InUse && this.e.TableRow[index4].FirstCell > 0)
        {
          for (int index5 = this.e.TableRow[index4].FirstCell; index5 > 0; index5 = this.e.cell[index5].NextCell)
          {
            this.e.CellAux[index5].SpanningCell = 0;
            if ((this.e.cell[index5].flags & 16 /*0x10*/) != 0)
            {
              bool flag2 = true;
              int num = 2;
              int row = this.e.cell[index5].row;
              for (int prevRow = this.e.TableRow[row].PrevRow; prevRow > 0 & flag2; prevRow = this.e.TableRow[prevRow].PrevRow)
              {
                if ((this.e.TableRow[row].flags & 3075) != (this.e.TableRow[prevRow].flags & 3075))
                  flag2 = false;
                if (this.e.TableRow[row].indent != this.e.TableRow[prevRow].indent)
                  flag2 = false;
              }
              int CurCell = index5;
              if (flag2)
              {
                while ((CurCell = this.GetPrevCellInColumnPos(CurCell, true)) > 0 && (this.e.cell[CurCell].flags & 16 /*0x10*/) != 0)
                  ++num;
              }
              else
              {
                while ((CurCell = this.GetPrevCellInColumn(CurCell, false, true)) > 0 && (this.e.cell[CurCell].flags & 16 /*0x10*/) != 0)
                  ++num;
              }
              if (CurCell > 0)
              {
                if (num > this.e.cell[CurCell].RowSpan)
                  this.e.cell[CurCell].RowSpan = num;
                this.e.CellAux[index5].SpanningCell = CurCell;
              }
            }
          }
        }
      }
    }
    return true;
  }

  internal new bool SetCellLines()
  {
    for (int index = 0; index < this.e.TotalTableRows; ++index)
      this.e.TableRow[index].InUse = false;
    for (int index = 0; index < this.e.TotalCells; ++index)
    {
      int num;
      this.e.cell[index].LastLine = num = -1;
      this.e.cell[index].FirstLine = num;
      this.e.cell[index].InUse = false;
    }
    for (int index1 = 0; index1 < this.e.TotalLines; ++index1)
    {
      int cid = this.e.text[index1].cid;
      if (this.False(this.e.text[index1].tabw) || (this.e.text[index1].tabw.type & 32 /*0x20*/) == 0)
      {
        if (this.e.cell[cid].FirstLine == -1)
        {
          this.e.cell[cid].FirstLine = index1;
          for (int parentCell = this.e.cell[cid].ParentCell; parentCell > 0; parentCell = this.e.cell[parentCell].ParentCell)
          {
            if (this.e.cell[parentCell].FirstLine == -1)
            {
              int num;
              this.e.cell[parentCell].LastLine = num = index1;
              this.e.cell[parentCell].FirstLine = num;
            }
          }
        }
        this.e.cell[cid].LastLine = index1;
        this.e.cell[cid].InUse = true;
        this.e.TableRow[this.e.cell[cid].row].InUse = true;
        if (this.e.cell[cid].level > 0)
        {
          for (int index2 = cid; index2 > 0; index2 = this.e.cell[index2].ParentCell)
          {
            this.e.cell[index2].InUse = true;
            this.e.TableRow[this.e.cell[index2].row].InUse = true;
          }
        }
      }
    }
    return true;
  }

  internal new int SetRowIndent(int LineNo, int row, int sect, int ColumnWidth)
  {
    if ((this.e.TableRow[row].flags & 3) != 0)
    {
      int num = 0;
      for (int index = this.e.TableRow[row].FirstCell; index > 0; index = this.e.cell[index].NextCell)
        num += this.e.cell[index].width;
      this.e.TableRow[row].indent = this.UnitToTwipsX(ColumnWidth) - num;
      if ((this.e.TableRow[row].flags & 1) != 0)
        this.e.TableRow[row].indent /= 2;
    }
    int num1 = this.TwipsToUnitX(this.e.TableRow[row].indent);
    int num2 = this.CalcFrmIndentBefRow(LineNo, sect);
    if (num2 > num1 && num2 > 0)
      num1 = num2;
    this.e.TableRow[row].CurIndent = num1;
    return num1;
  }

  internal new bool SetSubtableCellWidths(int cl, int width)
  {
    int level = this.e.cell[cl].level;
    for (int firstLine = this.e.cell[cl].FirstLine; firstLine <= this.e.cell[cl].LastLine; ++firstLine)
    {
      if (this.e.cell[this.e.text[firstLine].cid].level > level)
      {
        int num = 0;
        for (; firstLine < this.e.cell[cl].LastLine; ++firstLine)
        {
          int cid = this.e.text[firstLine].cid;
          if (cid != cl)
          {
            if (this.True(num) && this.e.cell[cid].level == level + 1 && num != this.e.cell[cid].row)
            {
              int row = this.e.cell[cid].row;
              if (row == 0 || this.IsFirstTableRow(row))
                break;
            }
            if (num == 0 && this.e.cell[cid].level == level + 1)
              num = this.e.cell[cid].row;
          }
          else
            break;
        }
        if (num > 0)
        {
          int TblMinWidth;
          int TblMaxWidth;
          bool ExactWidth;
          int[] pColMinWidth;
          int[] pColMaxWidth;
          if (this.GetTableMinMaxWidths(num, out TblMinWidth, out TblMaxWidth, out int _, out ExactWidth, out pColMinWidth, out pColMaxWidth, width))
            this.SetTableCellWidths(num, TblMinWidth, TblMaxWidth, width, ExactWidth, pColMinWidth, pColMaxWidth);
          --firstLine;
        }
      }
    }
    return true;
  }

  internal new bool SetTableCellWidths(
    int FirstRow,
    int TblMinWidth,
    int TblMaxWidth,
    int TblWidth,
    bool ExactWidth,
    int[] ColMinWidth,
    int[] ColMaxWidth)
  {
    int row = FirstRow;
    while (row > 0)
    {
      int num1 = 0;
      int index1 = 0;
      for (int index2 = this.e.TableRow[row].FirstCell; index2 > 0; index2 = this.e.cell[index2].NextCell)
      {
        int num2 = ColMinWidth[index1] * this.e.cell[index2].ColSpan;
        if (this.e.cell[index2].FixWidth > 0 && this.e.cell[index2].FixWidth > num2)
          num1 += this.e.cell[index2].FixWidth;
        index1 += this.e.cell[index2].ColSpan;
      }
      int num3 = TblWidth - num1;
      int num4 = TblMaxWidth - num1;
      int num5 = TblMinWidth - num1;
      if (num4 == 0)
        num4 = 1;
      int index3 = 0;
      int cl = this.e.TableRow[row].FirstCell;
      int num6 = 0;
      for (; cl > 0; cl = this.e.cell[cl].NextCell)
      {
        this.e.cell[cl].x = num6;
        this.e.cell[cl].width = 0;
        for (int index4 = 0; index4 < this.e.cell[cl].ColSpan; ++index4)
        {
          if (this.e.cell[cl].FixWidth > 0 && this.e.cell[cl].FixWidth >= ColMinWidth[index3])
          {
            this.e.cell[cl].width = this.e.cell[cl].FixWidth;
            ++index3;
          }
          else
          {
            if (num5 >= num3)
              this.e.cell[cl].width += ColMinWidth[index3];
            else if (num4 <= num3)
            {
              if (ExactWidth)
                this.e.cell[cl].width += ColMaxWidth[index3] * num3 / num4;
              else
                this.e.cell[cl].width += ColMaxWidth[index3];
            }
            else
            {
              int num7 = num3 - num5;
              int num8 = num4 - num5;
              int num9 = ColMaxWidth[index3] - ColMinWidth[index3];
              if (num8 == 0)
                num8 = 1;
              if (!ExactWidth)
                num7 = num7 * 9 / 10;
              if (num7 < 0)
                num7 = 0;
              this.e.cell[cl].width += ColMinWidth[index3] + num9 * num7 / num8;
            }
            ++index3;
          }
        }
        num6 = this.e.cell[cl].x + this.e.cell[cl].width;
        this.SetSubtableCellWidths(cl, this.e.cell[cl].width - 2 * this.e.cell[cl].margin);
      }
      if (!this.e.HasNestedTables)
      {
        int z = 0;
        for (int index5 = this.e.TableRow[row].FirstCell; index5 > 0; index5 = this.e.cell[index5].NextCell)
          z += this.e.cell[index5].width;
        if (z > TblWidth && TblWidth > 0)
        {
          for (int index6 = this.e.TableRow[row].FirstCell; index6 > 0; index6 = this.e.cell[index6].NextCell)
            this.e.cell[index6].width = this.MulDiv(this.e.cell[index6].width, TblWidth, z);
        }
      }
      row = this.e.TableRow[row].NextRow;
      if (row > 0 && this.IsFirstTableRow(row))
        row = 0;
    }
    return true;
  }

  internal new bool TableHilighted()
  {
    if (!this.e.TerArg.PageMode)
      return false;
    int level = this.MinTableLevel(this.e.HilightBegRow, this.e.HilightEndRow);
    int cell1 = this.LevelCell(level, this.e.HilightBegRow);
    int cell2 = this.LevelCell(level, this.e.HilightEndRow);
    if (cell1 == 0 && cell2 == 0)
      return false;
    if (cell1 != cell2)
      return this.InSameTable(cell1, cell2);
    if ((this.e.TerOpFlags & 8388608 /*0x800000*/) != 0 && this.e.HilightEndCol == this.e.text[this.e.HilightEndRow].len && this.e.HilightEndCol > 0)
      --this.e.HilightEndCol;
    int num = this.e.text[this.e.HilightEndRow].len - 1;
    if ((this.e.TerFlags & 1073741824 /*0x40000000*/) != 0)
      ++num;
    int cid = this.e.text[this.e.HilightEndRow].cid;
    return (this.e.HilightBegRow == 0 || this.LevelCell(level, this.e.HilightBegRow - 1) != cell1) && this.e.HilightBegCol == 0 && this.LineInfo(this.e.HilightEndRow, 48 /*0x30*/) && this.e.cell[cid].level == level && this.e.HilightEndCol >= num;
  }

  internal new int TableLevel(int LineNo)
  {
    if (LineNo < 0 || LineNo >= this.e.TotalLines)
      return 0;
    int cid = this.e.text[LineNo].cid;
    return cid == 0 ? 0 : this.e.cell[cid].level;
  }

  internal new bool TblHilightLeft()
  {
    if (this.e.CurLine != 0 || this.e.CurCol != 0)
    {
      int cid1 = this.e.text[this.e.HilightBegRow].cid;
      int cid2 = this.e.text[this.e.HilightEndRow].cid;
      if (this.e.CurLine != this.e.HilightEndRow)
      {
        this.e.CurLine = this.e.HilightEndRow;
        this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
      }
      this.e.WrapFlag = 0;
      this.e.PaintFlag = 1;
      this.e.HilightAtCurPos = false;
      if (cid1 == cid2)
      {
        if (this.e.CurCol > 0)
        {
          --this.e.CurCol;
          this.PaintTer();
          return true;
        }
        if (this.e.CurLine > 0 && this.e.text[this.e.CurLine].cid == this.e.text[this.e.CurLine - 1].cid)
        {
          --this.e.CurLine;
          this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
          if (this.e.CurCol < 0)
            this.e.CurCol = 0;
          this.PaintTer();
          return true;
        }
      }
      if (this.e.CurLine > 0)
      {
        if (this.InSameTable(cid1, cid2))
        {
          if (this.e.cell[cid2].PrevCell > 0)
          {
            --this.e.CurLine;
            while (this.e.CurLine > 0 && (!this.True(this.e.text[this.e.CurLine].tabw) || (this.e.text[this.e.CurLine].tabw.type & 16 /*0x10*/) == 0))
              --this.e.CurLine;
          }
          else
          {
            int prevRow = this.e.TableRow[this.e.cell[cid2].row].PrevRow;
            int firstCell = prevRow <= 0 ? 0 : this.e.TableRow[prevRow].FirstCell;
            --this.e.CurLine;
            while (this.e.CurLine > 0 && this.e.text[this.e.CurLine].cid != 0 && (this.e.text[this.e.CurLine].cid != firstCell || !this.True(this.e.text[this.e.CurLine].tabw) || (this.e.text[this.e.CurLine].tabw.type & 16 /*0x10*/) == 0))
              --this.e.CurLine;
          }
        }
        else
        {
          for (--this.e.CurLine; this.e.CurLine > 0 && this.e.text[this.e.CurLine].cid != 0; --this.e.CurLine)
          {
            if (this.True(this.e.text[this.e.CurLine].tabw) && (this.e.text[this.e.CurLine].tabw.type & 32 /*0x20*/) != 0 && this.True(this.e.text[this.e.CurLine - 1].tabw) && (this.e.text[this.e.CurLine - 1].tabw.type & 16 /*0x10*/) != 0)
            {
              --this.e.CurLine;
              break;
            }
          }
        }
      }
      this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
      if (this.e.CurCol < 0)
        this.e.CurCol = 0;
      this.PaintTer();
    }
    return true;
  }

  internal new bool TblHilightRight(bool HilightBegins)
  {
    int cid1 = this.e.text[this.e.HilightBegRow].cid;
    int cid2 = this.e.text[this.e.HilightEndRow].cid;
    this.e.WrapFlag = 0;
    this.e.PaintFlag = 1;
    this.e.HilightAtCurPos = false;
    if (cid1 == cid2)
    {
      if (this.e.CurCol + 1 < this.e.text[this.e.CurLine].len)
        ++this.e.CurCol;
      else if (this.e.CurLine + 1 < this.e.TotalLines)
      {
        ++this.e.CurLine;
        this.e.CurCol = 0;
      }
      if (this.e.cell[this.e.text[this.e.CurLine].cid].row == this.e.cell[cid1].row)
      {
        this.PaintTer();
        return true;
      }
    }
    this.e.CurLine = this.e.HilightEndRow;
    this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
    if (this.e.CurCol < 0)
      this.e.CurCol = 0;
    if (this.e.CurLine + 1 < this.e.TotalLines)
    {
      if (this.InSameTable(cid1, cid2))
      {
        if (this.e.cell[cid1].row == this.e.cell[cid2].row && this.e.cell[cid2].NextCell > 0)
        {
          ++this.e.CurLine;
          while (this.e.CurLine + 1 < this.e.TotalLines && (!this.True(this.e.text[this.e.CurLine].tabw) || (this.e.text[this.e.CurLine].tabw.type & 16 /*0x10*/) == 0))
            ++this.e.CurLine;
        }
        else
        {
          ++this.e.CurLine;
          if (this.True(this.e.text[this.e.CurLine].tabw) && (this.e.text[this.e.CurLine].tabw.type & 32 /*0x20*/) != 0)
            ++this.e.CurLine;
          bool flag = this.True(this.e.text[this.e.CurLine - 1].tabw) && (this.e.text[this.e.CurLine - 1].tabw.type & 32 /*0x20*/) != 0;
          if (this.e.text[this.e.CurLine].cid == 0)
          {
            this.e.CurCol = 0;
            this.PaintTer();
            return true;
          }
          for (; this.e.CurLine + 1 < this.e.TotalLines; ++this.e.CurLine)
          {
            if (this.True(this.e.text[this.e.CurLine].tabw) && (this.e.text[this.e.CurLine].tabw.type & 16 /*0x10*/) != 0)
            {
              if (flag)
              {
                if (this.True(this.e.text[this.e.CurLine + 1].tabw) && (this.e.text[this.e.CurLine + 1].tabw.type & 32 /*0x20*/) != 0)
                {
                  ++this.e.CurLine;
                  break;
                }
              }
              else
                break;
            }
          }
        }
      }
      else
      {
        ++this.e.CurLine;
        if (this.True(this.e.text[this.e.CurLine].tabw) && (this.e.text[this.e.CurLine].tabw.type & 32 /*0x20*/) != 0)
          ++this.e.CurLine;
        if (this.e.text[this.e.CurLine].cid == 0)
        {
          this.e.CurCol = 0;
          this.PaintTer();
          return true;
        }
        while (this.e.CurLine + 1 < this.e.TotalLines && (!this.True(this.e.text[this.e.CurLine].tabw) || (this.e.text[this.e.CurLine].tabw.type & 16 /*0x10*/) == 0 || !this.True(this.e.text[this.e.CurLine + 1].tabw) || (this.e.text[this.e.CurLine + 1].tabw.type & 32 /*0x20*/) == 0))
          ++this.e.CurLine;
        ++this.e.CurLine;
      }
    }
    this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
    if (this.e.CurCol < 0)
      this.e.CurCol = 0;
    this.PaintTer();
    return true;
  }

  internal bool TerAdjustHtmlTable()
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    this.e.HasNestedTables = false;
    for (int index = 0; index < this.e.TotalCells; ++index)
    {
      if (this.e.cell[index].InUse && this.e.cell[index].ParentCell != 0)
        this.e.HasNestedTables = true;
    }
    this.SetCellLines();
    for (int FirstRow = 1; FirstRow < this.e.TotalTableRows; ++FirstRow)
    {
      int TblMinWidth;
      int TblMaxWidth;
      int TblWidth;
      bool ExactWidth;
      int[] pColMinWidth;
      int[] pColMaxWidth;
      if (this.e.TableRow[FirstRow].InUse && this.e.TableRow[FirstRow].PrevRow <= 0 && this.e.cell[this.e.TableRow[FirstRow].FirstCell].level <= 0 && this.GetTableMinMaxWidths(FirstRow, out TblMinWidth, out TblMaxWidth, out TblWidth, out ExactWidth, out pColMinWidth, out pColMaxWidth, 0))
        this.SetTableCellWidths(FirstRow, TblMinWidth, TblMaxWidth, TblWidth, ExactWidth, pColMinWidth, pColMaxWidth);
    }
    this.e.RepageBeginLine = 0;
    return true;
  }

  internal new bool TerBackTabCell()
  {
    if ((this.e.TerFlags3 & 32 /*0x20*/) == 0)
    {
      int cid1 = this.e.text[this.e.CurLine].cid;
      int index1 = this.e.CurLine - 1;
      while (index1 >= 0 && this.e.text[index1].cid != 0 && (this.e.text[index1].cid == cid1 || !this.True(this.e.text[index1].tabw) || (this.e.text[index1].tabw.type & 16 /*0x10*/) == 0 || (this.e.cell[this.e.text[index1].cid].flags & 16 /*0x10*/) != 0))
        --index1;
      if (index1 < 0)
        return true;
      int cid2 = this.e.text[index1].cid;
      if (cid2 == 0)
        return true;
      int index2 = index1;
      int LineNo = index2 - 1;
      while (LineNo >= 0 && (this.TableLevel(LineNo) != this.TableLevel(this.e.CurLine) || this.e.text[LineNo].cid == cid2))
        --LineNo;
      int num = LineNo + 1;
      this.e.CurLine = index2;
      this.e.CurCol = this.e.text[index2].len - 1;
      if (this.e.CurCol < 0)
        this.e.CurCol = 0;
      this.e.HilightType = 0;
      this.e.HilightBegRow = num;
      this.e.HilightBegCol = 0;
      this.e.HilightEndRow = this.e.CurLine;
      this.e.HilightEndCol = this.e.CurCol;
      if (this.e.HilightBegRow != this.e.HilightEndRow || this.e.HilightBegCol != this.e.HilightEndCol)
        this.e.HilightType = 2;
      this.PaintTer();
    }
    return true;
  }

  internal bool TerCellBorder(
    int select,
    int TopWidth,
    int BotWidth,
    int LeftWidth,
    int RightWidth,
    bool repaint)
  {
    return this.TerCellBorder2(select, TopWidth, BotWidth, LeftWidth, RightWidth, false, repaint);
  }

  internal bool TerCellBorder2(
    int select,
    int TopWidth,
    int BotWidth,
    int LeftWidth,
    int RightWidth,
    bool outline,
    bool repaint)
  {
    int[] numArray1 = new int[4];
    int[] numArray2 = new int[4];
    bool flag1 = false;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.e.HilightType == 2 && (this.e.text[this.e.HilightBegRow].cid > 0 || this.e.text[this.e.HilightEndRow].cid > 0))
      flag1 = true;
    int cid = this.e.text[this.e.CurLine].cid;
    if (this.e.TerArg.PrintView && cid != 0 | flag1 && (cid != 0 || select != 4))
    {
      for (int index = 0; index < 4; ++index)
        numArray1[index] = this.e.cell[cid].BorderWidth[index];
      int margin1 = this.e.cell[cid].margin;
      if (select <= 0)
      {
        if (!this.CallDialogBox((Form) new terdlg_cell_border(this.e)))
          return true;
        select = this.e.DlgResult;
        outline = this.e.DlgBool1;
      }
      else
      {
        select = select != 1 ? (select != 2 ? (select != 3 ? (select != 4 ? 0 : 942) : 888) : 887) : 889;
        if (select > 0)
        {
          this.e.cell[cid].BorderWidth[0] = TopWidth;
          this.e.cell[cid].BorderWidth[1] = BotWidth;
          this.e.cell[cid].BorderWidth[2] = LeftWidth;
          this.e.cell[cid].BorderWidth[3] = RightWidth;
        }
      }
      for (int index = 0; index < 4; ++index)
      {
        numArray2[index] = this.e.cell[cid].BorderWidth[index];
        this.e.cell[cid].BorderWidth[index] = numArray1[index];
      }
      int margin2 = this.e.cell[cid].margin;
      this.e.cell[cid].margin = margin1;
      this.e.TableRow[this.e.cell[cid].row].CellMargin = margin1;
      if (select != 0)
      {
        if (!this.MarkCells(select))
          return false;
        this.SaveUndo(0, 0, 0, 0, '4');
        for (int CurCell = 0; CurCell < this.e.TotalCells; ++CurCell)
        {
          if (!this.False(this.e.cell[CurCell].InUse) && (this.e.cell[CurCell].flags & 3) != 0)
          {
            this.e.cell[CurCell].margin = margin2;
            this.e.TableRow[this.e.cell[CurCell].row].CellMargin = margin2;
            if (numArray2[0] >= 0)
            {
              if (numArray2[0] > 0)
              {
                bool flag2 = true;
                if (outline)
                {
                  int prevCellInColumnPos = this.GetPrevCellInColumnPos(CurCell, false);
                  if (prevCellInColumnPos > 0 && (this.e.cell[prevCellInColumnPos].flags & 3) != 0)
                    flag2 = false;
                }
                if (flag2)
                {
                  this.e.cell[CurCell].border |= 1;
                  this.e.cell[CurCell].BorderWidth[0] = Math.Min(margin2, numArray2[0]);
                }
              }
              else
                this.e.cell[CurCell].border = tc.ResetUintFlag(ref this.e.cell[CurCell].border, 1);
            }
            if (numArray2[1] >= 0)
            {
              if (numArray2[1] > 0)
              {
                bool flag3 = true;
                if (outline)
                {
                  int nextCellInColumnPos = this.GetNextCellInColumnPos(CurCell);
                  if (nextCellInColumnPos > 0 && (this.e.cell[nextCellInColumnPos].flags & 3) != 0)
                    flag3 = false;
                }
                if (flag3)
                {
                  this.e.cell[CurCell].border |= 2;
                  this.e.cell[CurCell].BorderWidth[1] = Math.Min(margin2, numArray2[1]);
                }
              }
              else
                this.e.cell[CurCell].border = tc.ResetUintFlag(ref this.e.cell[CurCell].border, 2);
            }
            if (numArray2[2] >= 0)
            {
              if (numArray2[2] > 0)
              {
                bool flag4 = true;
                if (outline)
                {
                  int prevCell = this.e.cell[CurCell].PrevCell;
                  if (prevCell > 0 && (this.e.cell[prevCell].flags & 3) != 0)
                    flag4 = false;
                }
                if (flag4)
                {
                  this.e.cell[CurCell].border |= 4;
                  this.e.cell[CurCell].BorderWidth[2] = Math.Min(margin2, numArray2[2]);
                }
              }
              else
                this.e.cell[CurCell].border = tc.ResetUintFlag(ref this.e.cell[CurCell].border, 4);
            }
            if (numArray2[3] >= 0)
            {
              if (numArray2[3] > 0)
              {
                bool flag5 = true;
                if (outline)
                {
                  int nextCell = this.e.cell[CurCell].NextCell;
                  if (nextCell > 0 && (this.e.cell[nextCell].flags & 3) != 0)
                    flag5 = false;
                }
                if (flag5)
                {
                  this.e.cell[CurCell].border |= 8;
                  this.e.cell[CurCell].BorderWidth[3] = Math.Min(margin2, numArray2[3]);
                }
              }
              else
                this.e.cell[CurCell].border = tc.ResetUintFlag(ref this.e.cell[CurCell].border, 8);
            }
          }
        }
        this.DeleteTextMap(true);
        ++this.e.TerArg.modified;
        if (repaint)
          this.PaintTer();
      }
    }
    return true;
  }

  internal bool TerCellBorderColor(
    int select,
    Color TopColor,
    Color BotColor,
    Color LeftColor,
    Color RightColor,
    bool repaint)
  {
    Color[] colorArray1 = new Color[4];
    Color[] colorArray2 = new Color[4];
    bool flag = false;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.e.HilightType == 2 && (this.e.text[this.e.HilightBegRow].cid > 0 || this.e.text[this.e.HilightEndRow].cid > 0))
      flag = true;
    int cid = this.e.text[this.e.CurLine].cid;
    if (this.e.TerArg.PrintView && cid != 0 | flag && (cid != 0 || select != 4))
    {
      for (int index = 0; index < 4; ++index)
        colorArray1[index] = this.e.cell[cid].BorderColor[index];
      if (select <= 0)
      {
        if (!this.CallDialogBox((Form) new terdlg_cell_border_color(this.e)))
          return true;
        select = this.e.DlgResult;
      }
      else
      {
        select = select != 1 ? (select != 2 ? (select != 3 ? (select != 4 ? 0 : 942) : 888) : 887) : 889;
        if (select > 0)
        {
          this.e.cell[cid].BorderColor[0] = TopColor;
          this.e.cell[cid].BorderColor[1] = BotColor;
          this.e.cell[cid].BorderColor[2] = LeftColor;
          this.e.cell[cid].BorderColor[3] = RightColor;
        }
      }
      for (int index = 0; index < 4; ++index)
      {
        colorArray2[index] = this.e.cell[cid].BorderColor[index];
        this.e.cell[cid].BorderColor[index] = colorArray1[index];
      }
      if (select != 0)
      {
        if (!this.MarkCells(select))
          return false;
        this.SaveUndo(0, 0, 0, 0, '4');
        for (int index = 0; index < this.e.TotalCells; ++index)
        {
          if (!this.False(this.e.cell[index].InUse) && (this.e.cell[index].flags & 3) != 0)
          {
            this.e.cell[index].BorderColor[0] = colorArray2[0];
            this.e.cell[index].BorderColor[1] = colorArray2[1];
            this.e.cell[index].BorderColor[2] = colorArray2[2];
            this.e.cell[index].BorderColor[3] = colorArray2[3];
          }
        }
        this.DeleteTextMap(true);
        ++this.e.TerArg.modified;
        if (repaint)
          this.PaintTer();
      }
    }
    return true;
  }

  internal bool TerCellColor(int select, Color color, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int cid = this.e.text[this.e.CurLine].cid;
    if (this.e.TerArg.PrintView && (cid != 0 || this.e.HilightType == 2))
    {
      if (select <= 0)
      {
        Color backColor = this.e.cell[cid].BackColor;
        if (!this.CallDialogBox((Form) new terdlg_cell_color(this.e)))
          return true;
        select = this.e.DlgResult;
        color = this.e.cell[cid].BackColor;
        this.e.cell[cid].BackColor = backColor;
      }
      else
        select = select != 1 ? (select != 2 ? (select != 3 ? (select != 4 ? 0 : 942) : 888) : 887) : 889;
      if (select != 0)
      {
        if (!this.MarkCells(select))
          return false;
        this.SaveUndo(0, 0, 0, 0, '4');
        this.e.HilightType = 0;
        for (int index = 0; index < this.e.TotalCells; ++index)
        {
          if (!this.False(this.e.cell[index].InUse) && (this.e.cell[index].flags & 3) != 0)
            this.e.cell[index].BackColor = color;
        }
        this.DeleteTextMap(true);
        ++this.e.TerArg.modified;
        if (repaint)
          this.PaintTer();
      }
    }
    return true;
  }

  internal bool TerCellRotateText(int select, int direction, bool repaint)
  {
    bool flag = false;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.e.HilightType == 2 && (this.e.text[this.e.HilightBegRow].cid > 0 || this.e.text[this.e.HilightEndRow].cid > 0))
      flag = true;
    int cid = this.e.text[this.e.CurLine].cid;
    if (this.e.TerArg.PrintView && cid != 0 | flag)
    {
      int num;
      if (select <= 0)
      {
        if (!this.CallDialogBox((Form) new terdlg_cell_rotation(this.e)))
          return true;
        select = this.e.DlgResult;
        num = this.e.cell[cid].TextAngle;
      }
      else
      {
        num = 0;
        if (direction == 1)
          num = 270;
        if (direction == 2)
          num = 90;
        switch (select)
        {
          case 1:
            select = 889;
            break;
          case 2:
            select = 887;
            break;
          case 3:
            select = 888;
            break;
          case 4:
            select = 942;
            break;
          default:
            select = 0;
            break;
        }
      }
      if (select != 0)
      {
        if (!this.MarkCells(select))
          return false;
        this.SaveUndo(0, 0, 0, 0, '4');
        this.e.HilightType = 0;
        for (int index = 0; index < this.e.TotalCells; ++index)
        {
          if (!this.False(this.e.cell[index].InUse) && (this.e.cell[index].flags & 3) != 0)
            this.e.cell[index].TextAngle = num;
        }
        this.DeleteTextMap(true);
        ++this.e.TerArg.modified;
        if (this.e.RepageBeginLine > this.e.CurLine)
          this.e.RepageBeginLine = this.e.CurLine;
        this.RequestPagination(false);
        if (repaint)
          this.PaintTer();
      }
    }
    return true;
  }

  internal bool TerCellShading(int select, int shading, bool repaint)
  {
    bool flag = false;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.e.HilightType == 2 && (this.e.text[this.e.HilightBegRow].cid > 0 || this.e.text[this.e.HilightEndRow].cid > 0))
      flag = true;
    int cid = this.e.text[this.e.CurLine].cid;
    if (this.e.TerArg.PrintView && cid != 0 | flag)
    {
      if (select <= 0)
      {
        int shading1 = this.e.cell[cid].shading;
        if (!this.CallDialogBox((Form) new terdlg_cell_shading(this.e)))
          return true;
        select = this.e.DlgResult;
        shading = this.e.cell[cid].shading;
        this.e.cell[cid].shading = shading1;
      }
      else
      {
        if (shading < 0)
          shading = 0;
        if (shading > 100)
          shading = 100;
        switch (select)
        {
          case 1:
            select = 889;
            break;
          case 2:
            select = 887;
            break;
          case 3:
            select = 888;
            break;
          case 4:
            select = 942;
            break;
          default:
            select = 0;
            break;
        }
      }
      if (select != 0)
      {
        if (!this.MarkCells(select))
          return false;
        this.SaveUndo(0, 0, 0, 0, '4');
        this.e.HilightType = 0;
        for (int index = 0; index < this.e.TotalCells; ++index)
        {
          if (!this.False(this.e.cell[index].InUse) && (this.e.cell[index].flags & 3) != 0)
            this.e.cell[index].shading = shading;
        }
        this.DeleteTextMap(true);
        ++this.e.TerArg.modified;
        if (repaint)
          this.PaintTer();
      }
    }
    return true;
  }

  internal bool TerCellVertAlign(int select, int align, bool repaint)
  {
    bool flag1 = false;
    int flag2 = 77824 /*0x013000*/;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.e.HilightType == 2 && (this.e.text[this.e.HilightBegRow].cid > 0 || this.e.text[this.e.HilightEndRow].cid > 0))
      flag1 = true;
    int cid = this.e.text[this.e.CurLine].cid;
    if (this.e.TerArg.PrintView && cid != 0 | flag1)
    {
      if (select <= 0)
      {
        int num = this.e.cell[cid].flags & flag2;
        if (!this.CallDialogBox((Form) new terdlg_cell_vert_align(this.e)))
          return true;
        select = this.e.DlgResult;
        align = this.e.cell[cid].flags & flag2;
        this.e.cell[cid].flags = tc.ResetUintFlag(ref this.e.cell[cid].flags, flag2);
        this.e.cell[cid].flags |= num;
      }
      else
      {
        if (align != 4096 /*0x1000*/ && align != 8192 /*0x2000*/ && align != 65536 /*0x010000*/)
          align = 0;
        switch (select)
        {
          case 1:
            select = 889;
            break;
          case 2:
            select = 887;
            break;
          case 3:
            select = 888;
            break;
          case 4:
            select = 942;
            break;
          default:
            select = 0;
            break;
        }
      }
      if (select != 0)
      {
        if (!this.MarkCells(select))
          return false;
        this.SaveUndo(0, 0, 0, 0, '4');
        this.e.HilightType = 0;
        for (int index = 0; index < this.e.TotalCells; ++index)
        {
          if (!this.False(this.e.cell[index].InUse) && (this.e.cell[index].flags & 3) != 0)
          {
            this.e.cell[index].flags = tc.ResetUintFlag(ref this.e.cell[index].flags, flag2);
            this.e.cell[index].flags |= align;
          }
        }
        this.DeleteTextMap(true);
        ++this.e.TerArg.modified;
        if (align == 65536 /*0x010000*/)
        {
          if (this.e.RepageBeginLine > this.e.CurLine)
            this.e.RepageBeginLine = this.e.CurLine;
          this.RequestPagination(false);
        }
        if (repaint)
          this.PaintTer();
      }
    }
    return true;
  }

  internal bool TerCellWidth(int select, int width, int margin, bool repaint)
  {
    bool flag = false;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.e.HilightType == 2 && (this.e.text[this.e.HilightBegRow].cid > 0 || this.e.text[this.e.HilightEndRow].cid > 0))
      flag = true;
    int cid = this.e.text[this.e.CurLine].cid;
    if (this.e.TerArg.PrintView && cid != 0 | flag && (cid != 0 || select != 4))
    {
      if (select <= 0)
      {
        if (!this.CallDialogBox((Form) new terdlg_cell_width(this.e)))
          return true;
        select = this.e.DlgResult;
        width = this.e.DlgInt1;
        margin = this.e.DlgInt2;
      }
      else
        select = select != 1 ? (select != 2 ? (select != 3 ? (select != 4 ? 0 : 942) : 888) : 887) : 889;
      if (select != 0)
      {
        if (!this.MarkCells(select))
          return false;
        this.SaveUndo(0, 0, 0, 0, '4');
        for (int index = 0; index < this.e.TotalCells; ++index)
        {
          if (!this.False(this.e.cell[index].InUse) && (this.e.cell[index].flags & 3) != 0)
          {
            if (width > 0)
            {
              int num;
              this.e.cell[index].FixWidth = num = width;
              this.e.cell[index].width = num;
            }
            if (margin > 0)
            {
              this.e.cell[index].margin = margin;
              this.e.TableRow[this.e.cell[index].row].CellMargin = margin;
            }
            if (this.e.cell[index].width < 3 * this.e.cell[index].margin)
            {
              int num;
              this.e.cell[index].width = num = 3 * this.e.cell[index].margin;
              this.e.cell[index].FixWidth = num;
            }
            if (margin > 0)
            {
              if ((this.e.cell[index].border & 4) != 0 && this.e.cell[index].BorderWidth[2] > margin)
                this.e.cell[index].BorderWidth[2] = margin;
              if ((this.e.cell[index].border & 8) != 0 && this.e.cell[index].BorderWidth[3] > margin)
                this.e.cell[index].BorderWidth[3] = margin;
              if ((this.e.cell[index].border & 1) != 0 && this.e.cell[index].BorderWidth[0] > margin)
                this.e.cell[index].BorderWidth[0] = margin;
              if ((this.e.cell[index].border & 2) != 0 && this.e.cell[index].BorderWidth[1] > margin)
                this.e.cell[index].BorderWidth[1] = margin;
            }
          }
        }
        this.DeleteTextMap(true);
        ++this.e.TerArg.modified;
        if (repaint)
          this.PaintTer();
      }
    }
    return true;
  }

  internal int TerCreateCellId(
    bool NewRow,
    int PrevCell,
    int RowAlign,
    int RowPos,
    int RowMinHeight,
    int CellWidth,
    int shading,
    int LeftWidth,
    int RightWidth,
    int TopWidth,
    int BotWidth,
    int RowSpan,
    int ColSpan,
    int CellFlags)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.PageMode)
      return 0;
    int cellSlot;
    if (NewRow)
    {
      int index;
      if (PrevCell > 0)
      {
        if (PrevCell >= this.e.TotalCells || this.False(this.e.cell[PrevCell].InUse))
          return 0;
        index = this.e.cell[PrevCell].row;
        if (this.e.TableRow[index].NextRow != -1)
          return 0;
      }
      else
        index = 0;
      int terOpFlags = this.e.TerOpFlags;
      this.e.TerOpFlags |= 16777216 /*0x01000000*/;
      int tableRowSlot;
      if ((tableRowSlot = this.GetTableRowSlot()) == -1)
        return 0;
      this.e.TerOpFlags = terOpFlags;
      this.e.TableRow[tableRowSlot].InUse = true;
      if (index > 0)
      {
        this.e.TableRow[index].NextRow = tableRowSlot;
        this.e.TableRow[tableRowSlot].PrevRow = index;
      }
      else
      {
        this.e.TableRow[tableRowSlot].PrevRow = -1;
        this.e.TableRow[tableRowSlot].flags |= 32768 /*0x8000*/;
      }
      this.e.TableRow[tableRowSlot].NextRow = -1;
      this.e.TableRow[tableRowSlot].FirstCell = 0;
      this.e.TableRow[tableRowSlot].LastCell = 0;
      this.e.TableRow[tableRowSlot].CellMargin = this.e.DefCellMargin;
      switch (RowAlign)
      {
        case 1:
          this.e.TableRow[tableRowSlot].flags |= 1;
          break;
        case 2:
          this.e.TableRow[tableRowSlot].flags |= 2;
          break;
      }
      this.e.TableRow[tableRowSlot].MinHeight = RowMinHeight;
      if ((cellSlot = this.GetCellSlot(false)) == -1)
        return 0;
      this.e.cell[cellSlot].InUse = true;
      this.e.cell[cellSlot].row = tableRowSlot;
      this.e.cell[cellSlot].margin = this.e.DefCellMargin;
      this.e.cell[cellSlot].PrevCell = -1;
      this.e.cell[cellSlot].NextCell = -1;
      this.e.cell[cellSlot].x = RowPos;
      int num;
      this.e.TableRow[tableRowSlot].LastCell = num = cellSlot;
      this.e.TableRow[tableRowSlot].FirstCell = num;
      this.e.TableRow[tableRowSlot].indent = RowPos;
    }
    else
    {
      if (PrevCell <= 0 || PrevCell >= this.e.TotalCells || this.False(this.e.cell[PrevCell].InUse) || this.e.cell[PrevCell].NextCell != -1 || (cellSlot = this.GetCellSlot(false)) == -1)
        return 0;
      this.CopyCell(PrevCell, cellSlot);
      this.e.cell[cellSlot].x = this.e.cell[PrevCell].x + this.e.cell[PrevCell].width;
      this.e.cell[PrevCell].NextCell = cellSlot;
      this.e.cell[cellSlot].PrevCell = PrevCell;
      this.e.cell[cellSlot].NextCell = -1;
      int row = this.e.cell[PrevCell].row;
      this.e.TableRow[row].LastCell = cellSlot;
      if (this.True(RowMinHeight))
        this.e.TableRow[row].MinHeight = RowMinHeight;
    }
    if (CellWidth > 0)
    {
      int num;
      this.e.cell[cellSlot].FixWidth = num = CellWidth;
      this.e.cell[cellSlot].width = num;
    }
    else
    {
      this.e.cell[cellSlot].width = 1000;
      this.e.cell[cellSlot].FixWidth = CellWidth;
    }
    this.e.cell[cellSlot].shading = shading;
    this.e.cell[cellSlot].border = 0;
    if (LeftWidth > 0)
      this.e.cell[cellSlot].border |= 4;
    if (RightWidth > 0)
      this.e.cell[cellSlot].border |= 8;
    if (TopWidth > 0)
      this.e.cell[cellSlot].border |= 1;
    if (BotWidth > 0)
      this.e.cell[cellSlot].border |= 2;
    this.e.cell[cellSlot].BorderWidth[2] = LeftWidth;
    this.e.cell[cellSlot].BorderWidth[3] = RightWidth;
    this.e.cell[cellSlot].BorderWidth[0] = TopWidth;
    this.e.cell[cellSlot].BorderWidth[1] = BotWidth;
    this.e.cell[cellSlot].flags = 0;
    if (RowSpan < 1)
      this.e.cell[cellSlot].flags |= 16 /*0x10*/;
    if (ColSpan < 1)
      this.e.cell[cellSlot].flags |= 64 /*0x40*/;
    if (RowSpan < 1)
      RowSpan = 1;
    if (ColSpan < 1)
      ColSpan = 1;
    if (CellWidth == 0)
      this.e.cell[cellSlot].flags |= 8;
    if (CellWidth < 0)
      this.e.cell[cellSlot].flags |= 512 /*0x0200*/;
    if (CellWidth > 0)
      this.e.cell[cellSlot].flags |= 256 /*0x0100*/;
    this.e.cell[cellSlot].flags |= CellFlags;
    this.e.cell[cellSlot].RowSpan = RowSpan;
    this.e.cell[cellSlot].ColSpan = ColSpan;
    return cellSlot;
  }

  internal bool TerCreateTable(int row, int col, bool refresh)
  {
    int index1 = -1;
    int EndLine = 0;
    bool recover = true;
    Cursor x = (Cursor) null;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.e.text[this.e.CurLine].cid > 0 && (this.e.TerFlags3 & 8192 /*0x2000*/) == 0)
      return false;
    if (row < 0)
    {
      this.e.TableRows = 3;
      this.e.TableCols = 2;
      if (!this.CallDialogBox((Form) new terdlg_table(this.e)))
        return false;
    }
    else
    {
      this.e.TableRows = row;
      this.e.TableCols = col;
    }
    int undoRef = this.e.UndoRef;
    if (!this.PrepForObject())
      return false;
    int curLine = this.e.CurLine;
    int num1;
    int num2;
    int num3;
    if (this.e.text[this.e.CurLine].cid == 0)
    {
      num2 = num1 = 0;
      num3 = this.ScrToTwipsX(this.TerWrapWidth(this.e.CurLine, -1));
    }
    else
    {
      int cid = this.e.text[this.e.CurLine].cid;
      num2 = this.e.cell[cid].level + 1;
      num3 = this.e.cell[cid].width - 2 * this.e.cell[cid].margin;
      num1 = cid;
    }
    int num4 = num3 / this.e.TableCols;
    if (this.CheckLineLimit(this.e.TotalLines + (this.e.TableRows * this.e.TableCols + this.e.TableRows)))
    {
      ushort CurFont = (ushort) this.GetEffectiveCfmt();
      if (this.e.TerFont[(int) CurFont].FieldId > 0)
        CurFont = (ushort) 0;
      int fid = this.e.text[this.e.CurLine].fid;
      if ((this.e.TerFlags & 512 /*0x0200*/) == 0)
      {
        x = this.e.Cursor;
        this.e.Cursor = Cursors.WaitCursor;
      }
      if (num1 > 0)
      {
        this.e.UndoRef = undoRef;
        this.SaveUndo(this.e.CurLine, 0, this.e.CurLine, 0, 'T');
      }
      for (int index2 = 0; index2 < this.e.TableRows; ++index2)
      {
        int tableRowSlot;
        if ((tableRowSlot = this.GetTableRowSlot()) != -1)
        {
          this.e.TableRow[tableRowSlot].InUse = true;
          this.e.TableRow[tableRowSlot].PrevRow = index1;
          this.e.TableRow[tableRowSlot].NextRow = -1;
          this.e.TableRow[tableRowSlot].FirstCell = 0;
          this.e.TableRow[tableRowSlot].LastCell = 0;
          this.e.TableRow[tableRowSlot].CellMargin = this.e.DefCellMargin;
          if (this.e.HtmlMode && index2 == 0)
            this.e.TableRow[tableRowSlot].flags |= 32768 /*0x8000*/;
          if (index1 > 0)
            this.e.TableRow[index1].NextRow = tableRowSlot;
          index1 = tableRowSlot;
          int index3 = -1;
          for (int index4 = 0; index4 < this.e.TableCols; ++index4)
          {
            if (!recover)
              this.e.TerFlags |= 64 /*0x40*/;
            int cellSlot;
            if ((cellSlot = this.GetCellSlot(recover)) != -1)
            {
              if (cellSlot == this.e.TotalCells - 1)
                recover = false;
              this.e.cell[cellSlot].InUse = true;
              this.e.cell[cellSlot].row = tableRowSlot;
              this.e.cell[cellSlot].width = num4;
              this.e.cell[cellSlot].margin = this.e.DefCellMargin;
              this.e.cell[cellSlot].border = 0;
              this.e.cell[cellSlot].level = num2;
              this.e.cell[cellSlot].ParentCell = num1;
              if (this.e.HtmlMode)
              {
                this.e.cell[cellSlot].border = 15;
                this.e.cell[cellSlot].BorderWidth[2] = 15;
                this.e.cell[cellSlot].BorderWidth[3] = 15;
                this.e.cell[cellSlot].BorderWidth[0] = 15;
                this.e.cell[cellSlot].BorderWidth[1] = 15;
                this.e.cell[cellSlot].flags |= 256 /*0x0100*/;
                this.e.cell[cellSlot].FixWidth = this.e.cell[cellSlot].width;
              }
              this.e.cell[cellSlot].PrevCell = index3;
              this.e.cell[cellSlot].NextCell = -1;
              if (index3 > 0)
                this.e.cell[cellSlot].x = this.e.cell[index3].x + this.e.cell[index3].width;
              else
                this.e.cell[cellSlot].x = 0;
              if (index3 > 0)
                this.e.cell[index3].NextCell = cellSlot;
              index3 = cellSlot;
              if (this.e.TableRow[tableRowSlot].FirstCell == 0)
                this.e.TableRow[tableRowSlot].FirstCell = cellSlot;
              this.e.TableRow[tableRowSlot].LastCell = cellSlot;
              this.InsertMarkerLine(this.e.CurLine, this.e.CellChar, (int) CurFont, this.e.text[this.e.CurLine].pfmt, 16 /*0x10*/, cellSlot);
              this.e.text[this.e.CurLine].fid = fid;
              ++this.e.CurLine;
            }
          }
          this.InsertMarkerLine(this.e.CurLine, '\u0012', (int) CurFont, this.e.text[this.e.CurLine].pfmt, 32 /*0x20*/, this.e.text[this.e.CurLine - 1].cid);
          this.e.text[this.e.CurLine].fid = fid;
          EndLine = this.e.CurLine;
          ++this.e.CurLine;
        }
      }
      if (this.True(x))
        this.e.Cursor = x;
      this.e.CurLine = curLine;
      this.e.CurCol = 0;
      this.RequestPagination(false);
      this.e.UndoRef = undoRef;
      this.SaveUndo(this.e.CurLine, this.e.CurCol, EndLine, 0, 'S');
      if (this.e.TableRows * this.e.TableCols > 300)
        this.e.TerRepaginate(refresh);
      else if (refresh)
        this.PaintTer();
    }
    return true;
  }

  internal bool TerDeleteCells(int select, bool repaint)
  {
    int index1 = 0;
    int index2 = 0;
    bool flag = false;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.e.HilightType == 2 && (this.e.text[this.e.HilightBegRow].cid > 0 || this.e.text[this.e.HilightEndRow].cid > 0))
      flag = true;
    int num1 = !flag ? this.e.text[this.e.CurLine].cid : this.LevelCell(this.MinTableLevel(this.e.HilightBegRow, this.e.HilightEndRow), this.e.CurLine);
    if (!this.e.TerArg.PrintView || num1 == 0 && !flag)
      return true;
    if (this.True(select))
    {
      switch (select)
      {
        case 1:
          select = 889;
          break;
        case 2:
          select = 887;
          break;
        case 3:
          select = 888;
          break;
        default:
          return false;
      }
    }
    else
    {
      select = 0;
      if (this.CallDialogBox((Form) new terdlg_del_cell(this.e)))
        select = this.e.DlgResult;
      if (select == 0)
        return true;
    }
    if (!this.MarkCells(select))
      return false;
    this.e.HilightType = 0;
    this.SetCellLines();
    if (!this.e.InUndo)
    {
      int FirstLine;
      int LastLine;
      this.GetTableSelRange(out FirstLine, out LastLine);
      int undoRef = this.e.UndoRef;
      if (FirstLine != -1)
      {
        this.SaveUndo(FirstLine, 0, LastLine, 0, 'T');
        index1 = this.e.cell[this.LevelCell(0, FirstLine)].row;
        index2 = this.e.cell[this.LevelCell(0, LastLine)].row;
      }
    }
    for (int index3 = 0; index3 < this.e.TotalCells; ++index3)
    {
      if (!this.False(this.e.cell[index3].InUse) && (this.e.cell[index3].flags & 3) != 0 && this.e.cell[index3].FirstLine != -1 && this.e.cell[index3].LastLine != -1)
      {
        int row = this.e.cell[index3].row;
        int prevRow = this.e.TableRow[row].PrevRow;
        int nextRow = this.e.TableRow[row].NextRow;
        int prevCell = this.e.cell[index3].PrevCell;
        int nextCell = this.e.cell[index3].NextCell;
        int lastLine = this.e.cell[index3].LastLine;
        if (index3 == this.e.TableRow[row].FirstCell && index3 == this.e.TableRow[row].LastCell)
        {
          int num2 = lastLine + 1;
        }
        if (prevCell > 0)
          this.e.cell[prevCell].NextCell = nextCell;
        else if (nextCell <= 0)
        {
          if (prevRow > 0)
            this.e.TableRow[prevRow].NextRow = nextRow;
          if (nextRow > 0)
            this.e.TableRow[nextRow].PrevRow = prevRow;
          ++this.e.cell[index3].LastLine;
          this.e.TableRow[row].InUse = false;
        }
        else
          this.e.TableRow[row].FirstCell = nextCell;
        if (nextCell > 0)
        {
          this.e.cell[nextCell].PrevCell = prevCell;
        }
        else
        {
          this.e.TableRow[row].LastCell = prevCell;
          int index4 = this.e.cell[index3].LastLine + 1;
          if (this.True(this.e.text[index4].tabw) && (this.e.text[index4].tabw.type & 32 /*0x20*/) != 0)
            this.e.text[index4].cid = prevCell;
        }
        int count = this.e.cell[index3].LastLine - this.e.cell[index3].FirstLine + 1;
        this.MoveLineArrays(this.e.cell[index3].FirstLine, count, 'D');
        for (int index5 = 0; index5 < this.e.TotalCells; ++index5)
        {
          if (this.e.cell[index5].InUse && this.e.cell[index5].FirstLine > this.e.cell[index3].LastLine)
          {
            this.e.cell[index5].FirstLine -= count;
            this.e.cell[index5].LastLine -= count;
          }
        }
        this.e.cell[index3].InUse = false;
      }
    }
    this.e.TerOpFlags &= -2;
    if (this.e.CurLine >= this.e.TotalLines)
      this.e.CurLine = this.e.TotalLines - 1;
    if (this.e.CurCol >= this.e.text[this.e.CurLine].len)
      this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
    if (this.e.CurCol < 0)
      this.e.CurCol = 0;
    if (!this.e.InUndo)
    {
      int firstCell = this.e.TableRow[index1].FirstCell;
      int lastCell = this.e.TableRow[index2].LastCell;
      if (this.e.cell[firstCell].InUse && this.e.cell[lastCell].InUse)
      {
        int undoRef = this.e.UndoRef;
        this.SaveUndo(this.e.cell[firstCell].FirstLine, 0, this.e.cell[lastCell].LastLine, 0, 'S');
      }
    }
    ++this.e.TerArg.modified;
    this.PaintTer();
    return true;
  }

  internal bool TerDeleteCellText(int select, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.e.HilightType == 2)
    {
      if (!this.TableHilighted())
        return false;
    }
    else if (this.e.text[this.e.CurLine].cid == 0)
      return false;
    int level = this.MinTableLevel(this.e.HilightBegRow, this.e.HilightEndRow);
    this.LevelCell(level, this.e.CurLine);
    if (this.True(select))
    {
      switch (select)
      {
        case 1:
          select = 889;
          break;
        case 2:
          select = 887;
          break;
        case 3:
          select = 888;
          break;
        default:
          return false;
      }
    }
    else
    {
      select = 0;
      if (this.CallDialogBox((Form) new terdlg_del_cell(this.e)))
        select = this.e.DlgResult;
      if (select == 0)
        return true;
    }
    int undoRef = this.e.UndoRef;
    if (!this.MarkCells(select))
      return false;
    this.e.HilightType = 0;
    this.SetCellLines();
    for (int index1 = 0; index1 < this.e.TotalCells; ++index1)
    {
      if (!this.False(this.e.cell[index1].InUse) && (this.e.cell[index1].flags & 3) != 0 && this.e.cell[index1].level == level)
      {
        int firstLine = this.e.cell[index1].FirstLine;
        int lastLine = this.e.cell[index1].LastLine;
        if (firstLine != -1 && lastLine != -1 && (firstLine != lastLine || this.e.text[firstLine].len != 1))
        {
          int EndCol = this.e.text[lastLine].len - 2;
          bool flag;
          if (EndCol < 0)
          {
            --lastLine;
            EndCol = this.e.text[lastLine].len - 1;
            if (lastLine >= firstLine)
              flag = true;
            else
              continue;
          }
          else
            flag = false;
          this.e.UndoRef = undoRef;
          this.SaveUndo(firstLine, 0, lastLine, EndCol, 'D');
          int count = lastLine - firstLine + 1;
          if (flag)
          {
            this.MoveLineArrays(firstLine, count, 'D');
          }
          else
          {
            --count;
            if (count > 0)
              this.MoveLineArrays(firstLine, count, 'D');
            this.MoveLineData(lastLine - count, 0, EndCol + 1, 'D');
          }
          for (int index2 = 0; index2 < this.e.TotalCells; ++index2)
          {
            if (this.e.cell[index2].InUse && this.e.cell[index2].FirstLine > this.e.cell[index1].LastLine)
            {
              this.e.cell[index2].FirstLine -= count;
              this.e.cell[index2].LastLine -= count;
            }
          }
        }
      }
    }
    this.e.CurLine = this.e.HilightBegRow;
    this.e.CurCol = 0;
    if (this.e.CurLine >= this.e.TotalLines)
      this.e.CurLine = this.e.TotalLines - 1;
    if (this.e.CurCol >= this.e.text[this.e.CurLine].len)
      this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
    if (this.e.CurCol < 0)
      this.e.CurCol = 0;
    ++this.e.TerArg.modified;
    this.PaintTer();
    return true;
  }

  internal bool TerDifTableRows(int row1, int row2)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if ((this.e.TerFlags3 & 512 /*0x0200*/) == 0)
    {
      int index1 = this.e.TableRow[row1].FirstCell;
      int index2 = this.e.TableRow[row2].FirstCell;
      int colSpan1 = this.e.cell[index1].ColSpan;
      int colSpan2 = this.e.cell[index2].ColSpan;
      int num1 = this.e.cell[index1].width;
      int num2 = this.e.cell[index2].width;
      while (index1 > 0 || index2 > 0)
      {
        if (index1 <= 0 && index2 > 0 || index2 <= 0 && index1 > 0)
          return true;
        if (colSpan1 <= 0)
        {
          colSpan1 = this.e.cell[index1].ColSpan;
          num1 += this.e.cell[index1].width;
        }
        if (colSpan2 <= 0)
        {
          colSpan2 = this.e.cell[index2].ColSpan;
          num2 += this.e.cell[index2].width;
        }
        if (colSpan1 == colSpan2 && Math.Abs(num1 - num2) > 60)
          return true;
        --colSpan1;
        --colSpan2;
        if (colSpan1 == 0 && colSpan2 == 0)
          num1 = num2 = 0;
        if (colSpan1 <= 0)
          index1 = this.e.cell[index1].NextCell;
        if (colSpan2 <= 0)
          index2 = this.e.cell[index2].NextCell;
      }
    }
    return false;
  }

  internal bool TerEquateHtmlTable(int CellId, int TotalTableCols)
  {
    int num = 0;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (CellId < 0 || CellId >= this.e.TotalCells || !this.e.cell[CellId].InUse)
      return false;
    int[] numArray1 = new int[TotalTableCols + 1];
    for (int index = 0; index < TotalTableCols + 1; ++index)
      numArray1[index] = 0;
    int index1 = this.e.cell[CellId].row;
    while (this.e.TableRow[index1].PrevRow > 0)
      index1 = this.e.TableRow[index1].PrevRow;
    for (; index1 > 0; index1 = this.e.TableRow[index1].NextRow)
    {
      int index2 = 0;
      int src = this.e.TableRow[index1].FirstCell;
      while (true)
      {
        int colSpan = this.e.cell[src].ColSpan;
        for (int index3 = index2; index3 < index2 + colSpan; ++index3)
        {
          if ((this.e.cell[src].flags & 16 /*0x10*/) != 0)
          {
            int[] numArray2;
            IntPtr index4;
            (numArray2 = numArray1)[(int) (index4 = (IntPtr) index3)] = numArray2[(int) index4] - 1;
          }
          else
            numArray1[index3] = this.e.cell[src].RowSpan - 1;
        }
        index2 += colSpan;
        if (this.e.cell[src].NextCell > 0)
          src = this.e.cell[src].NextCell;
        else
          break;
      }
      if (index2 < TotalTableCols)
      {
        for (; index2 < TotalTableCols; ++index2)
        {
          this.SetCellLines();
          int cellSlot;
          if ((cellSlot = this.GetCellSlot(false)) == -1)
            return false;
          this.CopyCell(src, cellSlot);
          this.e.cell[cellSlot].RowSpan = 1;
          this.e.cell[cellSlot].ColSpan = 1;
          this.e.cell[cellSlot].FixWidth = 0;
          if (numArray1[index2] > 0)
          {
            this.e.cell[cellSlot].flags = 16 /*0x10*/;
            int[] numArray3;
            IntPtr index5;
            (numArray3 = numArray1)[(int) (index5 = (IntPtr) index2)] = numArray3[(int) index5] - 1;
          }
          else
            this.e.cell[cellSlot].flags = tc.ResetUintFlag(ref this.e.cell[cellSlot].flags, 16 /*0x10*/);
          this.e.cell[cellSlot].width = this.e.cell[src].width / this.e.cell[src].ColSpan;
          this.e.cell[src].NextCell = cellSlot;
          this.e.cell[cellSlot].PrevCell = src;
          this.e.TableRow[index1].LastCell = cellSlot;
          num = this.e.cell[src].LastLine;
          this.InsertMarkerLine(num + 1, this.e.CellChar, 0, this.e.text[num + 1].pfmt, 16 /*0x10*/, cellSlot);
          src = cellSlot;
        }
        if (num + 2 < this.e.TotalLines)
          this.e.text[num + 2].cid = this.e.text[num + 1].cid;
      }
    }
    return true;
  }

  internal bool TerGetCellBorderColor(
    int CellId,
    out Color pLeft,
    out Color pRight,
    out Color pTop,
    out Color pBot)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    Color clrBlack;
    pBot = clrBlack = tc.CLR_BLACK;
    Color color1;
    pTop = color1 = clrBlack;
    Color color2;
    pRight = color2 = color1;
    pLeft = color2;
    if (!this.e.TerArg.PageMode)
      return false;
    if (CellId <= 0)
      CellId = this.e.text[this.e.CurLine].cid;
    if (CellId <= 0 || CellId >= this.e.TotalCells || !this.e.cell[CellId].InUse)
      return false;
    pLeft = this.e.cell[CellId].BorderColor[2];
    pRight = this.e.cell[CellId].BorderColor[3];
    pTop = this.e.cell[CellId].BorderColor[0];
    pBot = this.e.cell[CellId].BorderColor[1];
    return true;
  }

  internal bool TerGetCellBorderWidth(
    int CellId,
    out int pLeft,
    out int pRight,
    out int pTop,
    out int pBot)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int num1;
    pBot = num1 = 0;
    int num2;
    pTop = num2 = num1;
    int num3;
    pRight = num3 = num2;
    pLeft = num3;
    if (!this.e.TerArg.PageMode)
      return false;
    if (CellId <= 0)
      CellId = this.e.text[this.e.CurLine].cid;
    if (CellId <= 0 || CellId >= this.e.TotalCells || !this.e.cell[CellId].InUse)
      return false;
    if ((this.e.cell[CellId].border & 4) != 0)
      pLeft = this.e.cell[CellId].BorderWidth[2];
    if ((this.e.cell[CellId].border & 8) != 0)
      pRight = this.e.cell[CellId].BorderWidth[3];
    if ((this.e.cell[CellId].border & 1) != 0)
      pTop = this.e.cell[CellId].BorderWidth[0];
    if ((this.e.cell[CellId].border & 2) != 0)
      pBot = this.e.cell[CellId].BorderWidth[1];
    return true;
  }

  internal bool TerGetCellInfo(
    int CellId,
    out int row,
    out int PrevCell,
    out int NextCell,
    out int width,
    out int border,
    out int shading,
    out int RowSpan,
    out int ColSpan,
    out int CellFlags)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int num1;
    ColSpan = num1 = 0;
    int num2;
    RowSpan = num2 = num1;
    int num3;
    shading = num3 = num2;
    int num4;
    border = num4 = num3;
    int num5;
    width = num5 = num4;
    int num6;
    NextCell = num6 = num5;
    int num7;
    PrevCell = num7 = num6;
    row = num7;
    CellFlags = 0;
    if (!this.e.TerArg.PageMode || CellId < 0 || CellId >= this.e.TotalCells)
      return false;
    row = this.e.cell[CellId].row;
    PrevCell = this.e.cell[CellId].PrevCell;
    NextCell = this.e.cell[CellId].NextCell;
    width = this.e.cell[CellId].width;
    border = this.e.cell[CellId].border;
    shading = this.e.cell[CellId].shading;
    RowSpan = this.e.cell[CellId].RowSpan;
    ColSpan = this.e.cell[CellId].ColSpan;
    CellFlags = this.e.cell[CellId].flags;
    return true;
  }

  internal bool TerGetCellInfo2(int CellId, out Color BackColor, out int margin)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    BackColor = tc.CLR_WHITE;
    margin = 0;
    if (!this.e.TerArg.PageMode || CellId < 0 || CellId >= this.e.TotalCells || !this.e.cell[CellId].InUse)
      return false;
    BackColor = this.e.cell[CellId].BackColor;
    margin = this.e.cell[CellId].margin;
    return true;
  }

  internal bool TerGetCellParam(int type, int CellId, out int val)
  {
    bool cellParam = true;
    val = 0;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.e.TerArg.PageMode)
    {
      if (CellId < 0)
        CellId = this.e.text[this.e.CurLine].cid;
      if (CellId <= 0 || CellId >= this.e.TotalCells || !this.e.cell[CellId].InUse)
        return false;
      switch (type)
      {
        case 0:
          val = this.e.cell[CellId].FixWidth;
          return cellParam;
        case 1:
          val = this.e.cell[CellId].ParentCell;
          return cellParam;
        case 2:
          val = this.e.cell[CellId].level;
          return cellParam;
        case 3:
          switch (this.e.cell[CellId].TextAngle)
          {
            case 90:
              val = 2;
              return cellParam;
            case 270:
              val = 1;
              return cellParam;
            default:
              val = 0;
              return cellParam;
          }
      }
    }
    return false;
  }

  internal int TerGetLevelCell(int level, int LineNo)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    return this.LevelCell(level, LineNo);
  }

  internal int TerGetRowCellCount(bool GetRowCount)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.PageMode)
      return 0;
    int cid;
    int index1 = cid = this.e.text[this.e.CurLine].cid;
    if (index1 <= 0)
      return 0;
    if (GetRowCount)
    {
      int row1 = this.e.cell[index1].row;
      int rowCellCount = 1;
      while (!this.IsFirstTableRow(row1))
      {
        row1 = this.e.TableRow[row1].PrevRow;
        ++rowCellCount;
      }
      int row2 = this.e.cell[index1].row;
      while (!this.IsLastTableRow(row2))
      {
        row2 = this.e.TableRow[row2].NextRow;
        ++rowCellCount;
      }
      return rowCellCount;
    }
    int rowCellCount1 = 1;
    while (this.e.cell[index1].PrevCell > 0)
    {
      index1 = this.e.cell[index1].PrevCell;
      ++rowCellCount1;
    }
    int index2 = cid;
    while (this.e.cell[index2].NextCell > 0)
    {
      index2 = this.e.cell[index2].NextCell;
      ++rowCellCount1;
    }
    return rowCellCount1;
  }

  internal bool TerGetRowInfo(
    int RowId,
    out int height,
    out int MinHeight,
    out int FixWidth,
    out int PrevRow,
    out int NextRow,
    out int indent,
    out int flags,
    out int border,
    out int CurWidth)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int num1;
    CurWidth = num1 = 0;
    int num2;
    border = num2 = num1;
    int num3;
    flags = num3 = num2;
    int num4;
    indent = num4 = num3;
    int num5;
    NextRow = num5 = num4;
    int num6;
    PrevRow = num6 = num5;
    int num7;
    FixWidth = num7 = num6;
    int num8;
    MinHeight = num8 = num7;
    height = num8;
    if (RowId < 0)
    {
      int index = -RowId;
      if (index >= this.e.TotalCells || !this.e.cell[index].InUse)
        return false;
      RowId = this.e.cell[index].row;
    }
    if (RowId >= this.e.TotalTableRows || !this.e.TableRow[RowId].InUse)
      return false;
    height = this.e.TableRow[RowId].height;
    MinHeight = this.e.TableRow[RowId].MinHeight;
    FixWidth = this.e.TableRow[RowId].FixWidth;
    PrevRow = this.e.TableRow[RowId].PrevRow;
    NextRow = this.e.TableRow[RowId].NextRow;
    indent = this.e.TableRow[RowId].indent;
    flags = this.e.TableRow[RowId].flags;
    border = this.e.TableRow[RowId].border;
    int num9 = 0;
    for (int index = this.e.TableRow[RowId].FirstCell; index > 0; index = this.e.cell[index].NextCell)
      num9 += this.e.cell[index].width;
    CurWidth = num9;
    return true;
  }

  internal int TerGetTableId(int row)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (row < 0)
    {
      int cid = this.e.text[this.e.CurLine].cid;
      if (cid == 0)
        return -1;
      row = this.e.cell[cid].row;
    }
    if (!this.e.TableRow[row].InUse)
      return -1;
    if (this.e.TableRow[row].id >= 0)
    {
      while (!this.IsFirstTableRow(row))
        row = this.e.TableRow[row].PrevRow;
    }
    return this.e.TableRow[row].id;
  }

  internal int TerGetTableLevel(int LineNo)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (LineNo >= 0)
      return this.TableLevel(LineNo);
    int index = -LineNo;
    return index >= this.e.TotalCells || !this.e.cell[index].InUse ? 0 : this.e.cell[index].level;
  }

  internal bool TerGetTablePos(out int pTableNo, out int pRowNo, out int pColNo)
  {
    return this.TerGetTablePos2(out pTableNo, out pRowNo, out pColNo, 0);
  }

  internal bool TerGetTablePos2(out int pTableNo, out int pRowNo, out int pColNo, int ParentCell)
  {
    int num1 = 0;
    bool flag = false;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int num2;
    pColNo = num2 = 0;
    int num3;
    pRowNo = num3 = num2;
    pTableNo = num3;
    if (!this.e.TerArg.PageMode)
      return false;
    int num4;
    int level1;
    if (ParentCell > 0)
    {
      if (ParentCell > this.e.TotalCells || this.False(this.e.cell[ParentCell].InUse))
        return false;
      int level2 = this.e.cell[ParentCell].level;
      num4 = -1;
      for (int LineNo = 0; LineNo < this.e.TotalLines; ++LineNo)
      {
        if (num4 < 0)
        {
          if (!this.InOuterLevels(level2, LineNo) && this.LevelCell(level2, LineNo) == ParentCell)
          {
            int num5;
            num4 = num5 = LineNo;
          }
          else
            continue;
        }
        else if (this.InOuterLevels(level2, LineNo) || this.LevelCell(level2, LineNo) != ParentCell)
          break;
        num1 = LineNo;
      }
      if (num4 < 0)
        return false;
      level1 = level2 + 1;
    }
    else
    {
      num4 = 0;
      num1 = this.e.TotalLines - 1;
      int num6 = level1 = 0;
    }
    if (this.InOuterLevels(level1, this.e.CurLine) || this.e.CurLine < num4 || this.e.CurLine > num1)
      return false;
    int LineNo1 = num4;
    int num7 = -1;
    for (; LineNo1 <= this.e.CurLine; ++LineNo1)
    {
      if (!flag && !this.InOuterLevels(level1, LineNo1))
      {
        ++num7;
        flag = true;
      }
      if (flag && this.InOuterLevels(level1, LineNo1))
        flag = false;
    }
    pTableNo = num7;
    int num8 = 0;
    for (int curLine = this.e.CurLine; curLine >= num4 && !this.InOuterLevels(level1, curLine); --curLine)
    {
      if (this.True(this.e.text[curLine].tabw) && (this.e.text[curLine].tabw.type & 32 /*0x20*/) != 0 && this.TableLevel(curLine) == level1)
        ++num8;
    }
    pRowNo = num8;
    int num9 = 0;
    for (int LineNo2 = this.e.CurLine - 1; LineNo2 >= 0; --LineNo2)
    {
      int cid = this.e.text[LineNo2].cid;
      if (!this.InOuterLevels(level1, LineNo2) && (!this.LineInfo(LineNo2, 32 /*0x20*/) || this.TableLevel(LineNo2) != level1))
      {
        if (this.LineInfo(LineNo2, 16 /*0x10*/) && this.TableLevel(LineNo2) == level1)
          ++num9;
      }
      else
        break;
    }
    pColNo = num9;
    return true;
  }

  internal bool TerHtmlCellWidthFlag(int select, int flag, bool repaint)
  {
    bool flag1 = false;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.e.HilightType == 2 && (this.e.text[this.e.HilightBegRow].cid > 0 || this.e.text[this.e.HilightEndRow].cid > 0))
      flag1 = true;
    int cid = this.e.text[this.e.CurLine].cid;
    if (this.e.TerArg.PrintView && cid != 0 | flag1)
    {
      if (select <= 0)
      {
        this.e.DlgInt1 = this.e.cell[cid].flags;
        if (!this.CallDialogBox((Form) new terdlg_cell_width_flag(this.e)))
          return false;
        select = this.e.DlgResult;
        flag = this.e.DlgInt1;
      }
      else
        select = select != 1 ? (select != 2 ? (select != 3 ? (select != 4 ? 0 : 942) : 888) : 887) : 889;
      if (select != 0)
      {
        if (flag != 0 && flag != 256 /*0x0100*/ && flag != 512 /*0x0200*/ || !this.MarkCells(select))
          return false;
        this.e.HilightType = 0;
        for (int index = 0; index < this.e.TotalCells; ++index)
        {
          if (!this.False(this.e.cell[index].InUse) && (this.e.cell[index].flags & 3) != 0)
          {
            this.e.cell[index].flags = tc.ResetUintFlag(ref this.e.cell[index].flags, 776);
            this.e.cell[index].flags |= flag;
            if ((flag & 512 /*0x0200*/) != 0)
              this.e.cell[index].flags |= 8;
            if ((this.e.cell[index].flags & 256 /*0x0100*/) != 0)
              this.e.cell[index].FixWidth = this.e.cell[index].width;
            else
              this.e.cell[index].FixWidth = 0;
          }
        }
        this.DeleteTextMap(true);
        ++this.e.TerArg.modified;
        if (repaint)
          this.PaintTer();
      }
    }
    return true;
  }

  internal bool TerInsertTableCol(bool insert, bool AllRows, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int cid = this.e.text[this.e.CurLine].cid;
    if (cid == 0)
      return false;
    this.SetCellLines();
    this.e.HilightType = 0;
    if (!insert)
    {
      int lastCell = this.e.TableRow[this.e.cell[cid].row].LastCell;
      this.e.HilightType = 2;
      this.e.HilightBegRow = this.e.cell[lastCell].FirstLine;
      this.e.HilightEndRow = this.e.cell[lastCell].LastLine;
      this.e.HilightBegCol = 0;
      this.e.HilightEndCol = this.e.text[this.e.HilightEndRow].len;
    }
    if (!this.MarkCells(AllRows ? 887 : 0))
      return false;
    this.e.HilightType = 0;
    int FirstLine;
    int LastLine;
    this.GetTableSelRange(out FirstLine, out LastLine);
    int undoRef = this.e.UndoRef;
    if (FirstLine != -1)
      this.SaveUndo(FirstLine, 0, LastLine, 0, 'T');
    for (int src = 0; src < this.e.TotalCells; ++src)
    {
      if (!this.False(this.e.cell[src].InUse) && (this.e.cell[src].flags & 3) != 0)
      {
        if (!this.CheckLineLimit(this.e.TotalLines + 1))
          return true;
        int cellSlot;
        if ((cellSlot = this.GetCellSlot(true)) != -1)
        {
          this.CopyCell(src, cellSlot);
          this.e.cell[cellSlot].flags = tc.ResetUintFlag(ref this.e.cell[cellSlot].flags, 3);
          int LineNo;
          int pfmt;
          int line;
          if (insert)
          {
            if (this.e.cell[src].PrevCell < 0)
            {
              this.e.TableRow[this.e.cell[src].row].FirstCell = cellSlot;
              this.e.cell[cellSlot].PrevCell = -1;
              this.e.cell[cellSlot].NextCell = src;
              this.e.cell[src].PrevCell = cellSlot;
            }
            else
            {
              int prevCell = this.e.cell[src].PrevCell;
              this.e.cell[prevCell].NextCell = cellSlot;
              this.e.cell[cellSlot].PrevCell = prevCell;
              this.e.cell[src].PrevCell = cellSlot;
              this.e.cell[cellSlot].NextCell = src;
            }
            LineNo = this.e.cell[src].FirstLine;
            pfmt = this.e.text[LineNo].pfmt;
            line = LineNo;
          }
          else
          {
            this.e.TableRow[this.e.cell[src].row].LastCell = cellSlot;
            this.e.cell[cellSlot].NextCell = -1;
            this.e.cell[cellSlot].PrevCell = src;
            this.e.cell[src].NextCell = cellSlot;
            LineNo = this.e.cell[src].LastLine + 1;
            pfmt = this.e.text[this.e.cell[src].LastLine].pfmt;
            line = this.e.cell[src].LastLine;
            this.e.text[LineNo].cid = cellSlot;
          }
          int textFont = this.GetTextFont(this.e.InputFontId < 0 ? this.e.TerGetCurFont(line, 0) : this.e.InputFontId);
          this.InsertMarkerLine(LineNo, this.e.CellChar, textFont, pfmt, 16 /*0x10*/, cellSlot);
          if (this.e.CurLine >= LineNo)
            ++this.e.CurLine;
          for (int index = src; index < this.e.TotalCells; ++index)
          {
            if (!this.False(this.e.cell[index].InUse) && (this.e.cell[index].flags & 3) != 0)
            {
              if (this.e.cell[index].FirstLine >= LineNo)
                ++this.e.cell[index].FirstLine;
              if (this.e.cell[index].LastLine >= LineNo)
                ++this.e.cell[index].LastLine;
            }
          }
        }
      }
    }
    this.SetCellLines();
    this.GetTableSelRange(out FirstLine, out LastLine);
    this.e.UndoRef = undoRef;
    if (FirstLine != -1)
      this.SaveUndo(FirstLine, 0, LastLine, 0, 'S');
    ++this.e.TerArg.modified;
    if (repaint)
      this.PaintTer();
    return true;
  }

  internal bool TerInsertTableRow(bool insert, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int cid = this.e.text[this.e.CurLine].cid;
    if (this.e.TerArg.PrintView && cid != 0)
    {
      int undoRef = this.e.UndoRef;
      if (this.e.cell[cid].level > 0)
      {
        this.e.UndoRef = undoRef;
        this.SaveUndo(this.e.CurLine, 0, this.e.CurLine, 0, 'T');
      }
      int index1 = this.e.cell[cid].row;
      if (!insert)
      {
        while (this.e.TableRow[index1].NextRow >= 0)
          index1 = this.e.TableRow[index1].NextRow;
      }
      int num = 0;
      for (int index2 = this.e.TableRow[index1].FirstCell; index2 > 0; index2 = this.e.cell[index2].NextCell)
        ++num;
      this.SetCellLines();
      if (!this.CheckLineLimit(this.e.TotalLines + (num + 1)))
        return true;
      ushort CurFont = (ushort) this.GetEffectiveCfmt();
      if (this.e.TerFont[(int) CurFont].FieldId > 0)
        CurFont = (ushort) this.SetFontFieldId((int) CurFont, 0, (string) null);
      int tableRowSlot;
      if ((tableRowSlot = this.GetTableRowSlot()) == -1)
        return true;
      this.e.TableRow[tableRowSlot] = this.e.TableRow[index1].Copy();
      this.e.TableAux[tableRowSlot] = this.e.TableAux[index1].Copy();
      this.e.TableRow[tableRowSlot].FirstCell = 0;
      this.e.TableRow[tableRowSlot].LastCell = 0;
      if (insert)
      {
        int prevRow = this.e.TableRow[index1].PrevRow;
        this.e.TableRow[tableRowSlot].PrevRow = prevRow;
        if (prevRow > 0)
          this.e.TableRow[prevRow].NextRow = tableRowSlot;
        this.e.TableRow[tableRowSlot].NextRow = index1;
        this.e.TableRow[index1].PrevRow = tableRowSlot;
        tc.ResetUintFlag(ref this.e.TableRow[index1].flags, 32768 /*0x8000*/);
      }
      else
      {
        this.e.TableRow[tableRowSlot].PrevRow = index1;
        this.e.TableRow[index1].NextRow = tableRowSlot;
        this.e.TableRow[tableRowSlot].NextRow = -1;
      }
      int firstCell = this.e.TableRow[index1].FirstCell;
      if (insert)
      {
        this.e.CurLine = this.e.cell[firstCell].FirstLine;
      }
      else
      {
        this.e.CurLine = this.e.cell[this.e.TableRow[index1].LastCell].LastLine;
        this.e.CurLine += 2;
      }
      int curLine = this.e.CurLine;
      for (int index3 = firstCell; index3 > 0; index3 = this.e.cell[index3].NextCell)
        this.e.CellAux[index3].TempPfmt = this.e.text[this.e.cell[index3].LastLine].pfmt;
      this.e.CurCol = 0;
      int src = this.e.TableRow[index1].FirstCell;
      int index4 = -1;
      for (int index5 = 0; index5 < num; ++index5)
      {
        int cellSlot;
        if ((cellSlot = this.GetCellSlot(true)) != -1)
        {
          this.CopyCell(src, cellSlot);
          ref tc.StrCell local = ref this.e.cell[src];
          int tempPfmt = this.e.CellAux[src].TempPfmt;
          src = this.e.cell[src].NextCell;
          this.e.cell[cellSlot].InUse = true;
          this.e.cell[cellSlot].row = tableRowSlot;
          this.e.cell[cellSlot].PrevCell = index4;
          this.e.cell[cellSlot].NextCell = -1;
          if (index4 > 0)
            this.e.cell[index4].NextCell = cellSlot;
          index4 = cellSlot;
          if (this.e.TableRow[tableRowSlot].FirstCell == 0)
            this.e.TableRow[tableRowSlot].FirstCell = cellSlot;
          this.e.TableRow[tableRowSlot].LastCell = cellSlot;
          this.InsertMarkerLine(this.e.CurLine, this.e.CellChar, (int) CurFont, tempPfmt, 16 /*0x10*/, cellSlot);
          ++this.e.CurLine;
        }
      }
      this.InsertMarkerLine(this.e.CurLine, '\u0012', (int) CurFont, this.e.text[this.e.CurLine].pfmt, 32 /*0x20*/, this.e.text[this.e.CurLine - 1].cid);
      ++this.e.CurLine;
      this.e.UndoRef = undoRef;
      this.SaveUndo(this.e.CurLine - 1, 0, this.e.CurLine - 1, 0, 'S');
      if (!insert)
      {
        this.e.CurLine = curLine;
        this.e.CurCol = 0;
      }
      if (repaint)
        this.PaintTer();
    }
    return true;
  }

  internal bool TerIsTableSelected()
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.e.HilightType != 0 && this.NormalizeBlock())
    {
      for (int hilightBegRow = this.e.HilightBegRow; hilightBegRow <= this.e.HilightEndRow; ++hilightBegRow)
      {
        if (this.e.text[hilightBegRow].cid != 0)
        {
          int hilightBegCol = hilightBegRow == this.e.HilightBegRow ? this.e.HilightBegCol : 0;
          int num = hilightBegRow == this.e.HilightEndRow ? this.e.HilightEndCol : this.e.text[hilightBegRow].len;
          char[] txt = this.e.text[hilightBegRow].txt;
          for (int index = hilightBegCol; index < num; ++index)
          {
            if ((int) txt[index] == (int) this.e.CellChar || txt[index] == '\u0012')
              return true;
          }
        }
      }
    }
    return false;
  }

  internal new bool TerMergeCells()
  {
    int index1 = 0;
    if (this.e.HilightType != 0)
    {
      if (!this.NormalizeBlock())
        return false;
      int hilightEndRow1 = this.e.HilightEndRow;
      int hilightBegRow1 = this.e.HilightBegRow;
      while (hilightBegRow1 <= hilightEndRow1 && this.e.text[hilightBegRow1].cid != 0)
        ++hilightBegRow1;
      if (hilightBegRow1 <= hilightEndRow1)
        return false;
      this.MarkCells(0);
      int FirstLine;
      int LastLine;
      this.GetTableSelRange(out FirstLine, out LastLine);
      int undoRef = this.e.UndoRef;
      if (FirstLine != -1)
        this.SaveUndo(FirstLine, 0, LastLine, 0, 'T');
      int cid1 = this.e.text[hilightEndRow1].cid;
      int row = this.e.cell[cid1].row;
      bool flag1 = true;
      if (this.e.TableRow[row].NextRow > 0)
      {
        int cellColumn = this.GetCellColumn(cid1, true);
        int columnCell = this.GetColumnCell(this.e.TableRow[row].NextRow, cellColumn, true);
        if (this.e.cell[columnCell].ColSpan == 1)
        {
          if (Math.Abs(this.e.cell[cid1].x - this.e.cell[columnCell].x) > 60)
            flag1 = false;
          if (Math.Abs(this.e.cell[cid1].width - this.e.cell[columnCell].width) > 60)
            flag1 = false;
        }
      }
      if (this.e.TableRow[row].PrevRow > 0)
      {
        int cellColumn = this.GetCellColumn(cid1, true);
        int columnCell = this.GetColumnCell(this.e.TableRow[row].PrevRow, cellColumn, true);
        if (this.e.cell[columnCell].ColSpan == 1)
        {
          if (Math.Abs(this.e.cell[cid1].x - this.e.cell[columnCell].x) > 60)
            flag1 = false;
          if (Math.Abs(this.e.cell[cid1].width - this.e.cell[columnCell].width) > 60)
            flag1 = false;
        }
      }
      bool flag2 = false;
      for (int StartLine = hilightEndRow1; StartLine >= this.e.HilightBegRow; --StartLine)
      {
        if (this.True(this.e.text[StartLine].tabw) && (this.e.text[StartLine].tabw.type & 32 /*0x20*/) != 0)
        {
          cid1 = this.e.text[StartLine].cid;
          row = this.e.cell[cid1].row;
        }
        else if ((this.e.cell[this.e.text[StartLine].cid].flags & 1) == 0)
          flag2 = true;
        else if (flag2)
        {
          cid1 = this.e.text[StartLine].cid;
          flag2 = false;
        }
        else
        {
          index1 = this.e.text[StartLine].cid;
          if (index1 != cid1)
          {
            if (this.True(this.e.text[StartLine].tabw) && (this.e.text[StartLine].tabw.type & 16 /*0x10*/) != 0)
            {
              if (this.e.text[StartLine].len == 1)
              {
                this.MoveLineArrays(StartLine, 1, 'D');
                --this.e.HilightEndRow;
              }
              else
              {
                char[] txt = this.e.text[StartLine].txt;
                int len = this.e.text[StartLine].len;
                if (len > 0 && (int) txt[len - 1] == (int) this.e.CellChar)
                  txt[len - 1] = this.e.ParaChar;
                this.e.text[StartLine].tabw.type = tc.ResetUintFlag(ref this.e.text[StartLine].tabw.type, 16 /*0x10*/);
              }
              this.e.cell[cid1].x = this.e.cell[index1].x;
              this.e.cell[cid1].width += this.e.cell[index1].width;
              if (flag1)
                ++this.e.cell[cid1].ColSpan;
              int prevCell = this.e.cell[index1].PrevCell;
              if (prevCell > 0)
              {
                this.e.cell[prevCell].NextCell = cid1;
                this.e.cell[cid1].PrevCell = prevCell;
              }
              else
              {
                this.e.cell[cid1].PrevCell = -1;
                this.e.TableRow[row].FirstCell = cid1;
              }
            }
            this.e.text[StartLine].cid = cid1;
            this.e.CurLine = this.e.HilightBegRow;
          }
        }
      }
      if (index1 != cid1 && index1 != 0)
      {
        for (int index2 = this.e.HilightBegRow - 1; index2 >= 0 && this.e.text[index2].cid == index1; --index2)
          this.e.text[index2].cid = cid1;
      }
      int hilightEndRow2 = this.e.HilightEndRow;
      int cid2 = this.e.text[this.e.HilightBegRow].cid;
      int rowSpan = this.e.cell[cid2].RowSpan;
      int hilightBegRow2 = this.e.HilightBegRow;
      while (hilightBegRow2 <= hilightEndRow2 && (!this.True(this.e.text[hilightBegRow2].tabw) || (this.e.text[hilightBegRow2].tabw.type & 16 /*0x10*/) == 0))
        ++hilightBegRow2;
      int index3 = hilightBegRow2;
      for (int index4 = index3 + 1; index4 <= hilightEndRow2; ++index4)
      {
        if (!this.True(this.e.text[index4].tabw) || (this.e.text[index4].tabw.type & 32 /*0x20*/) == 0)
        {
          int cid3 = this.e.text[index4].cid;
          if ((this.e.cell[cid3].flags & 1) != 0)
          {
            if (this.True(this.e.text[index4].tabw) && (this.e.text[index4].tabw.type & 16 /*0x10*/) != 0)
            {
              if ((this.e.cell[cid3].flags & 16 /*0x10*/) == 0)
                rowSpan += this.e.cell[cid3].RowSpan;
              this.e.cell[cid3].flags |= 16 /*0x10*/;
              this.e.cell[cid3].RowSpan = 1;
              this.e.CellAux[cid3].SpanningCell = cid2;
            }
            int len1 = this.e.text[index3].len;
            char[] txt1 = this.e.text[index3].txt;
            if (len1 > 0 && (int) txt1[len1 - 1] == (int) this.e.CellChar)
            {
              if (len1 == 1)
              {
                this.LineAlloc(index3, this.e.text[index3].len, 0);
              }
              else
              {
                txt1[len1 - 1] = this.e.ParaChar;
                if (this.True(this.e.text[index3].tabw))
                  this.e.text[index3].tabw.type = tc.ResetUintFlag(ref this.e.text[index3].tabw.type, 16 /*0x10*/);
              }
            }
            this.MoveLineArrays(index3, 1, 'A');
            ++index3;
            ++index4;
            ++hilightEndRow2;
            int len2 = this.e.text[index4].len;
            this.LineAlloc(index3, this.e.text[index3].len, len2);
            this.MoveCharInfo(index4, 0, index3, 0, len2);
            this.e.text[index3].pfmt = this.e.text[index4].pfmt;
            this.e.text[index3].cid = cid2;
            this.e.text[index3].fid = this.e.text[index3 - 1].fid;
            char[] txt2 = this.e.text[index4].txt;
            ushort[] numArray = this.OpenCfmt(index4);
            if ((int) txt2[len2 - 1] == (int) this.e.CellChar)
            {
              txt2[0] = txt2[len2 - 1];
              numArray[0] = numArray[len2 - 1];
              this.LineAlloc(index4, this.e.text[index4].len, 1);
              if (this.False(this.e.text[index3].tabw))
                this.AllocTabw(index3);
              if (this.True(this.e.text[index3].tabw))
                this.e.text[index3].tabw.type |= 16 /*0x10*/;
            }
            else
              this.LineAlloc(index4, this.e.text[index4].len, 0);
            this.CloseCfmt(index4);
            this.CloseCfmt(index3);
          }
        }
      }
      this.e.cell[cid2].RowSpan = rowSpan;
      ++this.e.TerArg.modified;
      this.e.CurLine = this.e.HilightBegRow;
      this.e.CurCol = 0;
      this.e.UndoRef = undoRef;
      if (FirstLine != -1)
        this.SaveUndo(FirstLine, 0, hilightEndRow2, 0, 'S');
      this.e.HilightType = 0;
      this.PaintTer();
      this.e.TerRepaginate(true);
    }
    return true;
  }

  internal bool TerPosAfterTable(bool OuterMost, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.PageMode)
      return false;
    int cid1 = this.e.text[this.e.CurLine].cid;
    if (cid1 == 0)
      return false;
    int level = this.e.cell[cid1].level;
    int NewLine;
    for (NewLine = this.e.CurLine + 1; NewLine < this.e.TotalLines; ++NewLine)
    {
      int cid2 = this.e.text[NewLine].cid;
      if (cid2 == 0 || !OuterMost && this.e.cell[cid2].level < level)
        break;
    }
    this.e.SetTerCursorPos(NewLine, 0, repaint);
    return true;
  }

  internal bool TerPosTable(int TableNo, int RowNo, int ColNo, int pos, bool repaint)
  {
    return this.TerPosTable2(TableNo, RowNo, ColNo, pos, 0, repaint);
  }

  internal bool TerPosTable2(
    int TableNo,
    int RowNo,
    int ColNo,
    int pos,
    int ParentCell,
    bool repaint)
  {
    return this.TerPosTable4(0, TableNo, RowNo, ColNo, pos, ParentCell, repaint);
  }

  internal bool TerPosTable3(int TableId, int RowNo, int ColNo, int pos, bool repaint)
  {
    return this.TerPosTable4(TableId, 0, RowNo, ColNo, pos, 0, repaint);
  }

  internal bool TerPosTable4(
    int TableId,
    int TableNo,
    int RowNo,
    int ColNo,
    int pos,
    int ParentCell,
    bool repaint)
  {
    int num1 = 0;
    int index1 = 0;
    bool flag = false;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.PageMode || pos != 0 && pos != 1)
      return false;
    int num2;
    int level1;
    if (TableId != 0)
    {
      int index2;
      for (index2 = 0; index2 < this.e.TotalLines; ++index2)
      {
        index1 = this.e.text[index2].cid;
        if (index1 != 0 && this.e.TableRow[this.e.cell[index1].row].id == TableId)
          break;
      }
      if (index2 == this.e.TotalLines)
        return false;
      num2 = index2;
      num1 = this.e.TotalLines - 1;
      level1 = this.e.cell[index1].level;
    }
    else if (ParentCell > 0)
    {
      if (ParentCell > this.e.TotalCells || this.False(this.e.cell[ParentCell].InUse))
        return false;
      int level2 = this.e.cell[ParentCell].level;
      num2 = -1;
      for (int LineNo = 0; LineNo < this.e.TotalLines; ++LineNo)
      {
        if (num2 < 0)
        {
          if (!this.InOuterLevels(level2, LineNo) && this.LevelCell(level2, LineNo) == ParentCell)
          {
            int num3;
            num2 = num3 = LineNo;
          }
          else
            continue;
        }
        else if (this.InOuterLevels(level2, LineNo) || this.LevelCell(level2, LineNo) != ParentCell)
          break;
        num1 = LineNo;
      }
      if (num2 < 0)
        return false;
      level1 = level2 + 1;
    }
    else
    {
      num2 = 0;
      num1 = this.e.TotalLines - 1;
      level1 = 0;
    }
    int index3;
    if (TableId != 0)
      index3 = num2;
    else if (TableNo >= 0)
    {
      index3 = num2;
      int num4 = -1;
      for (; index3 <= num1; ++index3)
      {
        if (!flag && !this.InOuterLevels(level1, index3))
        {
          ++num4;
          if (num4 != TableNo)
            flag = true;
          else
            break;
        }
        if (flag && this.InOuterLevels(level1, index3))
          flag = false;
      }
      if (index3 >= num1)
        return false;
    }
    else
    {
      if (this.e.text[this.e.CurLine].cid == 0 || ParentCell > 0 && this.InOuterLevels(level1, this.e.CurLine))
        return false;
      int row = this.e.cell[this.LevelCell(level1, this.e.CurLine)].row;
      while (!this.IsFirstTableRow(row))
        row = this.e.TableRow[row].PrevRow;
      int firstCell = this.e.TableRow[row].FirstCell;
      int curLine = this.e.CurLine;
      while (curLine >= 0 && !this.InOuterLevels(level1, curLine) && this.LevelCell(level1, curLine) != firstCell)
        --curLine;
      if (curLine < 0 || this.InOuterLevels(level1, curLine))
        return false;
      while (curLine >= 0 && !this.InOuterLevels(level1, curLine) && this.LevelCell(level1, curLine) == firstCell)
        --curLine;
      index3 = curLine + 1;
    }
    if (RowNo > 0)
    {
      int num5 = 0;
      for (; index3 <= num1; ++index3)
      {
        if (this.InOuterLevels(level1, index3))
          return false;
        if (this.True(this.e.text[index3].tabw) && (this.e.text[index3].tabw.type & 32 /*0x20*/) != 0 && this.TableLevel(index3) == level1)
        {
          ++num5;
          if (num5 == RowNo)
          {
            ++index3;
            break;
          }
        }
      }
    }
    if (this.InOuterLevels(level1, index3))
      return false;
    if (pos == 1)
      ++ColNo;
    if (ColNo > 0)
    {
      int num6 = 0;
      for (; index3 <= num1; ++index3)
      {
        if (this.True(this.e.text[index3].tabw) && (this.e.text[index3].tabw.type & 32 /*0x20*/) != 0 && this.TableLevel(index3) == level1)
          return false;
        if (this.True(this.e.text[index3].tabw) && (this.e.text[index3].tabw.type & 16 /*0x10*/) != 0 && this.TableLevel(index3) == level1)
        {
          ++num6;
          if (num6 == ColNo)
          {
            ++index3;
            if (pos == 0 && index3 < this.e.TotalLines && this.LineInfo(index3, 32 /*0x20*/) && this.TableLevel(index3) == level1)
              return false;
            break;
          }
        }
      }
    }
    if (pos == 0)
    {
      this.e.SetTerCursorPos(index3, 0, repaint);
    }
    else
    {
      int NewCol = this.e.text[index3 - 1].len - 1;
      if (NewCol < 0)
        NewCol = 0;
      this.e.SetTerCursorPos(index3 - 1, NewCol, repaint);
    }
    return true;
  }

  internal bool TerRowHeight(int MinHeight, bool AllRows, bool refresh)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int cid1 = this.e.text[this.e.CurLine].cid;
    if (this.e.TerArg.PrintView && cid1 != 0)
    {
      if (MinHeight == -1)
      {
        if (!this.CallDialogBox((Form) new terdlg_row_height(this.e)))
          return false;
        switch (this.e.DlgResult)
        {
          case 0:
            return true;
          case 891:
            AllRows = true;
            break;
          default:
            AllRows = false;
            break;
        }
        MinHeight = this.e.DlgInt1;
      }
      ++this.e.TerArg.modified;
      if (AllRows)
      {
        int index = this.e.cell[this.e.text[this.e.CurLine].cid].row;
        while (this.e.TableRow[index].PrevRow > 0)
          index = this.e.TableRow[index].PrevRow;
        this.SaveUndo(-1, this.e.TableRow[index].FirstCell, -1, 0, '4');
        for (; index > 0; index = this.e.TableRow[index].NextRow)
          this.e.TableRow[index].MinHeight = MinHeight;
        if (refresh)
          this.PaintTer();
        return true;
      }
      for (int index = 0; index < this.e.TotalCells; ++index)
        this.e.cell[index].flags = tc.ResetUintFlag(ref this.e.cell[index].flags, 3);
      if (this.e.HilightType == 0)
      {
        int cid2 = this.e.text[this.e.CurLine].cid;
        this.e.cell[cid2].flags |= 1;
        this.SaveUndo(-1, cid2, -1, cid2, '4');
      }
      else
      {
        if (!this.NormalizeBlock())
          return false;
        this.SaveUndo(this.e.HilightBegRow, 0, this.e.HilightEndRow, 0, '4');
        int index = this.e.text[this.e.HilightBegRow].cid;
        this.e.cell[index].flags |= 1;
        for (int LineNo = this.e.HilightBegRow + 1; LineNo <= this.e.HilightEndRow; ++LineNo)
        {
          if (this.LineSelected(LineNo))
          {
            int cid3 = this.e.text[LineNo].cid;
            if (cid3 != index)
            {
              index = cid3;
              this.e.cell[index].flags |= 1;
            }
          }
        }
      }
      for (int index = 0; index < this.e.TotalCells; ++index)
      {
        if (!this.False(this.e.cell[index].InUse) && (this.e.cell[index].flags & 1) != 0)
          this.e.TableRow[this.e.cell[index].row].MinHeight = MinHeight;
      }
      if (refresh)
        this.PaintTer();
    }
    return true;
  }

  internal bool TerRowPosition(int JustFlag, bool AllRows, bool refresh)
  {
    return this.TerRowPositionEx(JustFlag, 0, AllRows, refresh);
  }

  internal bool TerRowPositionEx(int JustFlag, int indent, bool AllRows, bool refresh)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int cid1 = this.e.text[this.e.CurLine].cid;
    if (this.e.TerArg.PrintView && cid1 != 0)
    {
      if (JustFlag == 0 && indent == 0)
      {
        this.e.DlgResult = 0;
        if (!this.CallDialogBox((Form) new terdlg_row_position(this.e)))
          return true;
        switch (this.e.DlgResult)
        {
          case 0:
            return true;
          case 891:
            AllRows = true;
            break;
          default:
            AllRows = false;
            break;
        }
        JustFlag = this.e.DlgInt1;
        if (JustFlag == 0)
          JustFlag = 1024 /*0x0400*/;
      }
      if (this.True(indent))
        JustFlag = 1024 /*0x0400*/;
      ++this.e.TerArg.modified;
      if (this.e.CurLine < this.e.RepageBeginLine)
        this.e.RepageBeginLine = this.e.CurLine;
      if (AllRows)
      {
        int index = this.e.cell[this.e.text[this.e.CurLine].cid].row;
        while (this.e.TableRow[index].PrevRow > 0)
          index = this.e.TableRow[index].PrevRow;
        this.SaveUndo(-1, this.e.TableRow[index].FirstCell, -1, 0, '4');
        for (; index > 0; index = this.e.TableRow[index].NextRow)
        {
          this.e.TableRow[index].flags = tc.ResetUintFlag(ref this.e.TableRow[index].flags, 3);
          this.e.TableRow[index].flags |= JustFlag;
          this.e.TableRow[index].indent = indent;
        }
        if (refresh)
          this.Repaginate(false, true, 0, true);
        return true;
      }
      for (int index = 0; index < this.e.TotalCells; ++index)
        this.e.cell[index].flags = tc.ResetUintFlag(ref this.e.cell[index].flags, 3);
      if (this.e.HilightType == 0)
      {
        int cid2 = this.e.text[this.e.CurLine].cid;
        this.e.cell[cid2].flags |= 1;
        this.SaveUndo(-1, cid2, -1, cid2, '4');
      }
      else
      {
        if (!this.NormalizeBlock())
          return false;
        this.SaveUndo(this.e.HilightBegRow, 0, this.e.HilightEndRow, 0, '4');
        int index = this.e.text[this.e.HilightBegRow].cid;
        this.e.cell[index].flags |= 1;
        for (int LineNo = this.e.HilightBegRow + 1; LineNo <= this.e.HilightEndRow; ++LineNo)
        {
          if (this.LineSelected(LineNo))
          {
            int cid3 = this.e.text[LineNo].cid;
            if (cid3 != index)
            {
              index = cid3;
              this.e.cell[index].flags |= 1;
            }
          }
        }
      }
      for (int index = 0; index < this.e.TotalCells; ++index)
      {
        if (!this.False(this.e.cell[index].InUse) && (this.e.cell[index].flags & 1) != 0)
        {
          int row = this.e.cell[index].row;
          this.e.TableRow[row].flags = tc.ResetUintFlag(ref this.e.TableRow[row].flags, 3);
          this.e.TableRow[row].flags |= JustFlag;
          this.e.TableRow[row].indent = indent;
        }
      }
      if (refresh)
        this.Repaginate(false, true, 0, true);
    }
    return true;
  }

  internal bool TerSelectCellText(bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int cid = this.e.text[this.e.CurLine].cid;
    if (cid == 0)
      return false;
    int curLine1 = this.e.CurLine;
    while (curLine1 > 0 && this.e.text[curLine1 - 1].cid == cid)
      --curLine1;
    this.e.HilightBegRow = curLine1;
    this.e.HilightBegCol = 0;
    int curLine2 = this.e.CurLine;
    while (curLine2 < this.e.TotalLines - 1 && this.e.text[curLine2 + 1].cid == cid)
      ++curLine2;
    if (this.LineInfo(curLine2, 32 /*0x20*/))
      --curLine2;
    this.e.HilightEndRow = curLine2;
    this.e.HilightEndCol = this.e.text[curLine2].len - 1;
    if (this.e.HilightEndCol < 0)
      this.e.HilightEndCol = 0;
    this.e.StretchHilight = false;
    this.e.HilightType = 2;
    if (repaint)
      this.PaintTer();
    return true;
  }

  internal bool TerSelectCol(bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.e.text[this.e.CurLine].cid == 0)
      return false;
    this.HilightTableCol(this.e.CurLine, true, false);
    this.e.StretchHilight = false;
    this.e.HilightWithColCursor = false;
    if (repaint)
      this.PaintTer();
    return true;
  }

  internal bool TerSelectRow(bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int cid = this.e.text[this.e.CurLine].cid;
    if (cid == 0)
      return false;
    int row = this.e.cell[cid].row;
    int level = this.e.cell[cid].level;
    int curLine1;
    for (curLine1 = this.e.CurLine; curLine1 > 0; --curLine1)
    {
      int index = this.LevelCell(level, curLine1 - 1);
      if (index == 0 || row != this.e.cell[index].row)
        break;
    }
    this.e.HilightBegRow = curLine1;
    this.e.HilightBegCol = 0;
    int curLine2;
    for (curLine2 = this.e.CurLine; curLine2 + 1 < this.e.TotalLines; ++curLine2)
    {
      int index = this.LevelCell(level, curLine2 + 1);
      if (index == 0 || row != this.e.cell[index].row)
        break;
    }
    this.e.HilightEndRow = curLine2;
    this.e.HilightEndCol = this.e.text[this.e.HilightEndRow].len;
    this.e.HilightType = 2;
    this.e.StretchHilight = false;
    this.e.HilightWithColCursor = false;
    if (repaint)
      this.PaintTer();
    return true;
  }

  internal bool TerSelectTable(int level, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int cid = this.e.text[this.e.CurLine].cid;
    if (cid == 0)
      return false;
    if (level < 0)
      level = this.e.cell[cid].level;
    if (level > this.e.cell[cid].level)
      return false;
    int curLine1 = this.e.CurLine;
    while (curLine1 > 0 && this.e.text[curLine1 - 1].cid != 0 && this.e.cell[this.e.text[curLine1 - 1].cid].level >= level)
      --curLine1;
    this.e.HilightBegRow = curLine1;
    this.e.HilightBegCol = 0;
    int curLine2 = this.e.CurLine;
    while (curLine2 < this.e.TotalLines - 1 && this.e.text[curLine2 + 1].cid != 0 && this.e.cell[this.e.text[curLine2 + 1].cid].level >= level)
      ++curLine2;
    if (this.LineInfo(curLine2, 32 /*0x20*/))
      --curLine2;
    this.e.HilightEndRow = curLine2;
    this.e.HilightEndCol = this.e.text[curLine2].len;
    this.e.StretchHilight = false;
    this.e.HilightType = 2;
    if (repaint)
      this.PaintTer();
    return true;
  }

  internal bool TerSetCellBorderColor(int CellId, Color top, Color bot, Color left, Color right)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.PageMode || CellId <= 0 || CellId >= this.e.TotalCells || !this.e.cell[CellId].InUse)
      return false;
    this.e.cell[CellId].BorderColor[0] = top;
    this.e.cell[CellId].BorderColor[1] = bot;
    this.e.cell[CellId].BorderColor[2] = left;
    this.e.cell[CellId].BorderColor[3] = right;
    return true;
  }

  internal bool TerSetCellInfo(int CellId, Color BackColor, int margin)
  {
    return this.TerSetCellInfo2(CellId, BackColor, margin, 0);
  }

  internal bool TerSetCellInfo2(int CellId, Color BackColor, int margin, int ParentCell)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.PageMode || CellId <= 0 || CellId >= this.e.TotalCells || this.False(this.e.cell[CellId].InUse))
      return false;
    this.e.cell[CellId].BackColor = BackColor;
    if (margin >= 0)
    {
      this.e.cell[CellId].margin = margin;
      this.e.TableRow[this.e.cell[CellId].row].CellMargin = margin;
    }
    if (ParentCell > 0)
    {
      if (ParentCell >= this.e.TotalCells || this.False(this.e.cell[ParentCell].InUse))
        return false;
      this.e.cell[CellId].ParentCell = ParentCell;
      this.e.cell[CellId].level = this.e.cell[ParentCell].level + 1;
    }
    return true;
  }

  internal bool TerSetHdrRow(int CurCell, bool set, bool refresh)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (CurCell <= 0)
      CurCell = this.e.text[this.e.CurLine].cid;
    if (this.e.TerArg.PageMode && CurCell != 0)
    {
      int row = this.e.cell[CurCell].row;
      if (set)
        this.e.TableRow[row].flags |= 4;
      else
        tc.ResetUintFlag(ref this.e.TableRow[row].flags, 4);
      ++this.e.TerArg.modified;
      if (this.e.CurLine < this.e.RepageBeginLine)
        this.e.RepageBeginLine = this.e.CurLine;
      if (refresh)
        this.Repaginate(false, true, 0, true);
    }
    return true;
  }

  internal bool TerSetHtmlTblWidth(int CellId, int width)
  {
    bool flag = false;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (CellId <= 0)
    {
      CellId = this.e.text[this.e.CurLine].cid;
      flag = true;
    }
    if (CellId <= 0 || CellId >= this.e.TotalCells || !this.e.cell[CellId].InUse)
      return false;
    int row = this.e.cell[CellId].row;
    if (flag)
    {
      while (!this.IsFirstTableRow(row))
        row = this.e.TableRow[row].PrevRow;
      for (; row > 0; row = this.e.TableRow[row].NextRow)
        this.e.TableRow[row].FixWidth = width;
    }
    else
      this.e.TableRow[row].FixWidth = width;
    return true;
  }

  internal bool TerSetRowKeep(int CurCell, bool set, bool refresh)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.e.TerArg.PageMode)
    {
      if (this.e.HilightType == 0)
      {
        if (CurCell <= 0)
          CurCell = this.e.text[this.e.CurLine].cid;
        if (CurCell == 0)
          return true;
        int row = this.e.cell[CurCell].row;
        if (set)
          this.e.TableRow[row].flags |= 8192 /*0x2000*/;
        else
          tc.ResetUintFlag(ref this.e.TableRow[row].flags, 8192 /*0x2000*/);
      }
      else
      {
        if (!this.NormalizeBlock())
          return false;
        for (int hilightBegRow = this.e.HilightBegRow; hilightBegRow <= this.e.HilightEndRow; ++hilightBegRow)
        {
          if (this.e.text[hilightBegRow].cid != 0)
          {
            CurCell = this.e.text[hilightBegRow].cid;
            int row = this.e.cell[CurCell].row;
            if (set)
              this.e.TableRow[row].flags |= 8192 /*0x2000*/;
            else
              tc.ResetUintFlag(ref this.e.TableRow[row].flags, 8192 /*0x2000*/);
          }
        }
      }
      ++this.e.TerArg.modified;
      if (this.e.CurLine < this.e.RepageBeginLine)
        this.e.RepageBeginLine = this.e.CurLine;
      if (refresh)
        this.Repaginate(false, true, 0, true);
    }
    return true;
  }

  internal bool TerSetRowTextFlow(bool dialog, bool AllRows, int flow, bool refresh)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (flow != 0 && flow != 2 && flow != 1)
      return false;
    int cid1 = this.e.text[this.e.CurLine].cid;
    if (this.e.TerArg.PrintView && cid1 != 0)
    {
      int row = this.e.cell[cid1].row;
      if (dialog)
      {
        this.e.DlgInt1 = this.e.TableRow[row].flow;
        if (!this.CallDialogBox((Form) new terdlg_row_text_flow(this.e)))
          return true;
        switch (this.e.DlgResult)
        {
          case 0:
            return true;
          case 891:
            AllRows = true;
            break;
          default:
            AllRows = false;
            break;
        }
        flow = this.e.DlgInt1;
      }
      ++this.e.TerArg.modified;
      if (AllRows)
      {
        int index = this.e.cell[this.e.text[this.e.CurLine].cid].row;
        while (this.e.TableRow[index].PrevRow > 0)
          index = this.e.TableRow[index].PrevRow;
        this.SaveUndo(-1, this.e.TableRow[index].FirstCell, -1, 0, '4');
        for (; index > 0; index = this.e.TableRow[index].NextRow)
          this.e.TableRow[index].flow = flow;
        if (refresh)
          this.e.TerRepaginate(true);
        else
          this.RequestPagination(true);
        return true;
      }
      for (int index = 0; index < this.e.TotalCells; ++index)
        this.e.cell[index].flags = tc.ResetUintFlag(ref this.e.cell[index].flags, 3);
      if (this.e.HilightType == 0)
      {
        int cid2 = this.e.text[this.e.CurLine].cid;
        this.e.cell[cid2].flags |= 1;
        this.SaveUndo(-1, cid2, -1, cid2, '4');
      }
      else
      {
        if (!this.NormalizeBlock())
          return false;
        this.SaveUndo(this.e.HilightBegRow, 0, this.e.HilightEndRow, 0, '4');
        int index = this.e.text[this.e.HilightBegRow].cid;
        this.e.cell[index].flags |= 1;
        for (int LineNo = this.e.HilightBegRow + 1; LineNo <= this.e.HilightEndRow; ++LineNo)
        {
          if (this.LineSelected(LineNo))
          {
            int cid3 = this.e.text[LineNo].cid;
            if (cid3 != index)
            {
              index = cid3;
              this.e.cell[index].flags |= 1;
            }
          }
        }
      }
      for (int index = 0; index < this.e.TotalCells; ++index)
      {
        if (!this.False(this.e.cell[index].InUse) && (this.e.cell[index].flags & 1) != 0)
          this.e.TableRow[this.e.cell[index].row].flow = flow;
      }
      if (refresh)
        this.e.TerRepaginate(true);
      else
        this.RequestPagination(true);
    }
    return true;
  }

  internal bool TerSetTableColWidth(int width, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.e.text[this.e.CurLine].cid == 0 || width < 0)
      return false;
    this.e.HilightType = 0;
    if (!this.MarkCells(887))
      return false;
    for (int index = 0; index < this.e.TotalCells; ++index)
    {
      if (!this.False(this.e.cell[index].InUse) && (this.e.cell[index].flags & 3) != 0)
        this.e.cell[index].width = width;
    }
    ++this.e.TerArg.modified;
    if (repaint)
      this.PaintTer();
    return true;
  }

  internal bool TerSetTableId(int row, int id)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (row < 0)
    {
      int cid = this.e.text[this.e.CurLine].cid;
      if (cid == 0)
        return false;
      row = this.e.cell[cid].row;
    }
    if (!this.e.TableRow[row].InUse)
      return false;
    if (id < 0)
    {
      this.e.TableRow[row].id = id;
      return true;
    }
    while (!this.IsFirstTableRow(row))
      row = this.e.TableRow[row].PrevRow;
    while (true)
    {
      this.e.TableRow[row].id = id;
      if (!this.IsLastTableRow(row))
        row = this.e.TableRow[row].NextRow;
      else
        break;
    }
    return true;
  }

  internal new bool TerSplitCell()
  {
    if (this.e.HilightType == 0 && this.e.TerArg.PrintView)
    {
      int undoRef = this.e.UndoRef;
      int curLine1 = this.e.CurLine;
      this.SaveUndo(curLine1, 0, curLine1, 0, 'T');
      int index;
      int CurCell = index = this.e.text[this.e.CurLine].cid;
      int rowSpan = this.e.cell[CurCell].RowSpan;
      int cellColumn = this.GetCellColumn(CurCell, true);
      for (; CurCell != 0 && (!this.True(this.e.text[this.e.CurLine].tabw) || (this.e.text[this.e.CurLine].tabw.type & 32 /*0x20*/) == 0); CurCell = this.e.text[this.e.CurLine].cid)
      {
        if (this.e.cell[CurCell].width <= 180)
        {
          this.MessageBeep(0);
          break;
        }
        if (this.CheckLineLimit(this.e.TotalLines + 1))
        {
          ushort effectiveCfmt = (ushort) this.GetEffectiveCfmt();
          int src = CurCell;
          int nextCell = this.e.cell[CurCell].NextCell;
          int row = this.e.cell[CurCell].row;
          int curLine2;
          for (curLine2 = this.e.CurLine; curLine2 < this.e.TotalLines && (!this.True(this.e.text[curLine2].tabw) || (this.e.text[curLine2].tabw.type & 32 /*0x20*/) == 0) && (nextCell <= 0 || this.e.text[curLine2].cid != nextCell); ++curLine2)
          {
            if (this.e.text[curLine2].cid == 0 || curLine2 == this.e.TotalLines - 1)
              return this.PrintError(90, nameof (TerSplitCell));
          }
          this.e.CurLine = curLine2;
          this.e.CurCol = 0;
          int cellSlot;
          if ((cellSlot = this.GetCellSlot(true)) == -1)
            return false;
          this.CopyCell(src, cellSlot);
          this.e.cell[cellSlot].InUse = true;
          this.e.cell[cellSlot].PrevCell = src;
          if (src > 0)
            this.e.cell[src].NextCell = cellSlot;
          this.e.cell[cellSlot].NextCell = nextCell;
          if (nextCell > 0)
            this.e.cell[nextCell].PrevCell = cellSlot;
          int num;
          this.e.cell[src].width = num = this.e.cell[src].width / 2;
          this.e.cell[cellSlot].width = num;
          this.e.cell[cellSlot].x = this.e.cell[src].x + this.e.cell[src].width;
          if (this.e.cell[src].ColSpan > 1)
            --this.e.cell[src].ColSpan;
          this.e.cell[cellSlot].ColSpan = 1;
          if (this.e.TableRow[row].LastCell == src)
            this.e.TableRow[row].LastCell = cellSlot;
          this.InsertMarkerLine(this.e.CurLine, this.e.CellChar, (int) effectiveCfmt, this.e.text[this.e.CurLine - 1].pfmt, 16 /*0x10*/, cellSlot);
          if (this.e.CurLine < this.e.TotalLines - 1 && this.True(this.e.text[this.e.CurLine + 1].tabw) && (this.e.text[this.e.CurLine + 1].tabw.type & 32 /*0x20*/) != 0)
            this.e.text[this.e.CurLine + 1].cid = this.e.text[this.e.CurLine].cid;
          --rowSpan;
          if (rowSpan >= 1)
          {
            int nextRow = this.e.TableRow[this.e.cell[index].row].NextRow;
            if (nextRow > 0)
            {
              index = this.GetColumnCell(nextRow, cellColumn, true);
              while (this.e.CurLine + 1 < this.e.TotalLines && this.e.text[this.e.CurLine].cid != index)
                ++this.e.CurLine;
              this.e.CurCol = 0;
            }
            else
              break;
          }
          else
            break;
        }
        else
          break;
      }
      this.e.UndoRef = undoRef;
      this.SaveUndo(curLine1, 0, curLine1, 0, 'S');
      this.PaintTer();
    }
    return true;
  }

  internal new bool TerTabCell()
  {
    int index1 = 0;
    if ((this.e.TerFlags3 & 32 /*0x20*/) == 0)
    {
      int cid1 = this.e.text[this.e.CurLine].cid;
      int index2 = this.e.CurLine + 1;
      while (index2 < this.e.TotalLines && this.e.text[index2].cid != 0 && (this.e.text[index2].cid == cid1 || (this.e.cell[this.e.text[index2].cid].flags & 16 /*0x10*/) != 0))
        ++index2;
      if (index2 >= this.e.TotalLines)
        return true;
      int cid2 = this.e.text[index2].cid;
      if (cid2 == 0)
        return true;
      int num = index2;
      for (int LineNo = num + 1; LineNo < this.e.TotalLines; ++LineNo)
      {
        if (this.TableLevel(LineNo) == this.TableLevel(this.e.CurLine) && (this.e.text[LineNo].cid != cid2 || this.True(this.e.text[LineNo].tabw) && (this.e.text[LineNo].tabw.type & 32 /*0x20*/) != 0))
        {
          index1 = LineNo - 1;
          break;
        }
      }
      if (index1 >= this.e.TotalLines)
        index1 = this.e.TotalLines - 1;
      this.e.CurLine = index1;
      this.e.CurCol = this.e.text[index1].len - 1;
      if (this.e.CurCol < 0)
        this.e.CurCol = 0;
      this.e.HilightType = 0;
      this.e.HilightBegRow = num;
      this.e.HilightBegCol = 0;
      this.e.HilightEndRow = this.e.CurLine;
      this.e.HilightEndCol = this.e.CurCol;
      if (this.e.HilightBegRow != this.e.HilightEndRow || this.e.HilightBegCol != this.e.HilightEndCol)
        this.e.HilightType = 2;
      this.PaintTer();
    }
    return true;
  }

  internal bool TerTableOutlineBorder(int CurCell, int width, Color color, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (CurCell <= 0 || CurCell >= this.e.TotalCells)
      return false;
    int row = this.e.cell[CurCell].row;
    while (row > 0 && !this.IsFirstTableRow(row) && this.e.TableRow[row].PrevRow > 0)
      row = this.e.TableRow[row].PrevRow;
    int index1 = row;
    while (row > 0 && !this.IsLastTableRow(row) && this.e.TableRow[row].NextRow > 0)
      row = this.e.TableRow[row].NextRow;
    int index2 = row;
    for (int index3 = this.e.TableRow[index1].FirstCell; index3 > 0; index3 = this.e.cell[index3].NextCell)
    {
      this.e.cell[index3].BorderWidth[0] = Math.Min(this.e.cell[index3].margin, width);
      this.e.cell[index3].BorderColor[0] = color;
      this.e.cell[index3].border |= 1;
    }
    for (int index4 = this.e.TableRow[index2].FirstCell; index4 > 0; index4 = this.e.cell[index4].NextCell)
    {
      this.e.cell[index4].BorderWidth[1] = Math.Min(this.e.cell[index4].margin, width);
      this.e.cell[index4].BorderColor[1] = color;
      this.e.cell[index4].border |= 2;
    }
    for (int index5 = index1; index5 > 0; index5 = this.e.TableRow[index5].NextRow)
    {
      int index6 = this.e.TableRow[index5].FirstCell;
      this.e.cell[index6].BorderWidth[2] = Math.Min(this.e.cell[index6].margin, width);
      this.e.cell[index6].BorderColor[2] = color;
      this.e.cell[index6].border |= 4;
      for (; index6 > 0; index6 = this.e.cell[index6].NextCell)
      {
        if (this.e.cell[index6].NextCell <= 0)
        {
          this.e.cell[index6].BorderWidth[3] = Math.Min(this.e.cell[index6].margin, width);
          this.e.cell[index6].BorderColor[3] = color;
          this.e.cell[index6].border |= 8;
          break;
        }
      }
    }
    ++this.e.TerArg.modified;
    if (repaint)
      this.PaintTer();
    return true;
  }

  internal new bool TerToggleTableGrid()
  {
    if (!this.False(this.e.TerArg.PageMode))
    {
      this.e.ShowTableGridLines = !this.e.ShowTableGridLines;
      this.DeleteTextMap(true);
      this.PaintTer();
    }
    return true;
  }

  internal new int UniformRowBorderCell(int CellId, bool next)
  {
    int num1 = -1;
    int row1 = this.e.cell[CellId].row;
    int row2;
    if (next)
    {
      row2 = this.e.TableRow[row1].NextRow;
    }
    else
    {
      if ((row2 = this.e.TableRow[row1].PrevRow) <= 0)
        return 0;
      for (int firstCell = this.e.TableRow[row2].FirstCell; row2 > 0 && (this.e.cell[firstCell].flags & 16 /*0x10*/) != 0; firstCell = this.e.TableRow[row2].FirstCell)
      {
        if ((row2 = this.e.TableRow[row2].PrevRow) <= 0)
          return 0;
      }
    }
    if (row2 <= 0 || this.e.TableRow[row1].indent != this.e.TableRow[row2].indent || (this.e.TableRow[row1].flags & 3) != 0 != ((this.e.TableRow[row2].flags & 3) != 0) || Math.Abs(this.GetRowWidth(row1) - this.GetRowWidth(row2)) > 60)
      return 0;
    for (int index = this.e.TableRow[row2].FirstCell; index > 0; index = this.e.cell[index].NextCell)
    {
      int num2 = 0;
      if (next && (this.e.cell[index].border & 1) != 0)
        num2 = this.e.cell[index].BorderWidth[0];
      if (!next && (this.e.cell[index].border & 2) != 0)
        num2 = this.e.cell[index].BorderWidth[1];
      if (num1 == -1)
        num1 = num2;
      if (num1 != num2)
        return 0;
    }
    return this.e.TableRow[row2].FirstCell;
  }
}
