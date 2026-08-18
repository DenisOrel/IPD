// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.CUndo
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class CUndo : COp
{
  internal CUndo(ImRtfEditor ImRtfEditor)
    : base(ImRtfEditor)
  {
    this.e = ImRtfEditor;
  }

  internal bool FreeOneUndo(int idx)
  {
    this.e.undo[idx].txt = (char[]) null;
    this.e.undo[idx].fmt = (ushort[]) null;
    this.e.undo[idx].pfmt = (int[]) null;
    this.e.undo[idx].pFrame = (tc.ClsParaFrame) null;
    if (this.e.undo[idx].LinePtrU != null)
      this.FreeClonedLinePtr(this.e.undo[idx].LinePtrU, this.e.undo[idx].TotalLinesU);
    this.e.undo[idx].LinePtrU = (tc.ClsLinePtr[]) null;
    this.e.undo[idx].RowId = (int[]) null;
    this.e.undo[idx].pRow = (tc.StrTableRow[]) null;
    this.e.undo[idx].CellId = (int[]) null;
    this.e.undo[idx].pCell = (tc.StrCell[]) null;
    this.e.undo[idx].RowCount = 0;
    this.e.undo[idx].CellCount = 0;
    return true;
  }

  private bool GetUndoRowRange(ref int BegLine, ref int EndLine)
  {
    if (this.e.text[BegLine].cid == 0)
    {
      while (BegLine + 1 < this.e.TotalLines && this.e.text[BegLine].cid <= 0)
        ++BegLine;
    }
    if (this.e.text[EndLine].cid == 0)
    {
      while (EndLine > 0 && this.e.text[EndLine].cid <= 0)
        --EndLine;
    }
    if (this.e.text[BegLine].cid == 0)
      return false;
    if (BegLine != 0 && this.e.text[BegLine - 1].cid != 0)
    {
      --BegLine;
      while (BegLine >= 0)
      {
        int cid = this.e.text[BegLine].cid;
        if (cid != 0 && (this.e.cell[cid].level > 0 || !this.LineInfo(BegLine, 32 /*0x20*/)))
          --BegLine;
        else
          break;
      }
      ++BegLine;
      int index1 = this.e.cell[this.e.text[BegLine].cid].row;
      bool flag = false;
      for (; index1 > 0; index1 = this.e.TableRow[index1].PrevRow)
      {
        int index2 = this.e.TableRow[index1].FirstCell;
        while (index2 > 0 && (this.e.cell[index2].flags & 16 /*0x10*/) == 0)
          index2 = this.e.cell[index2].NextCell;
        if (index2 > 0)
        {
          flag = true;
          if (this.e.TableRow[index1].PrevRow <= 0)
            break;
        }
        else
          break;
      }
      if (flag)
      {
        int index3 = this.e.TableRow[index1].PrevRow;
        if (index3 < 0)
          index3 = 0;
        int lastCell = index3 <= 0 ? 0 : this.e.TableRow[index3].LastCell;
        while (BegLine >= 0 && this.e.text[BegLine].cid != lastCell)
          --BegLine;
        ++BegLine;
      }
    }
    int cid1 = this.e.text[EndLine].cid;
    if (cid1 == 0)
      return false;
    if (this.e.cell[cid1].level != 0 || !this.LineInfo(EndLine, 32 /*0x20*/))
    {
      while (EndLine + 1 < this.e.TotalLines)
      {
        int cid2 = this.e.text[EndLine].cid;
        if (cid2 == 0)
          return false;
        if (this.e.cell[cid2].level > 0 || !this.LineInfo(EndLine, 32 /*0x20*/))
          ++EndLine;
        else
          break;
      }
    }
    int index4 = this.e.cell[this.e.text[EndLine].cid].row;
    bool flag1 = false;
    int num = 0;
    for (; index4 > 0; index4 = this.e.TableRow[index4].NextRow)
    {
      int index5 = this.e.TableRow[index4].FirstCell;
      while (index5 > 0 && (this.e.cell[index5].flags & 16 /*0x10*/) == 0 && this.e.cell[index5].RowSpan <= 1)
        index5 = this.e.cell[index5].NextCell;
      if (index5 > 0)
      {
        flag1 = true;
        num = index4;
        if (this.e.TableRow[index4].NextRow <= 0)
          break;
      }
      else
        break;
    }
    if (flag1 && num > 0)
    {
      while (EndLine + 1 < this.e.TotalLines && (this.e.cell[this.e.text[EndLine].cid].row != num || !this.LineInfo(EndLine, 32 /*0x20*/)))
        ++EndLine;
    }
    return true;
  }

  internal new bool ReleaseRedo()
  {
    for (int undoCount = this.e.UndoCount; undoCount < this.e.UndoTblSize; ++undoCount)
      this.FreeOneUndo(undoCount);
    this.e.UndoTblSize = this.e.UndoCount;
    return true;
  }

  internal new bool ReleaseUndo()
  {
    for (int idx = 0; idx < this.e.UndoTblSize; ++idx)
      this.FreeOneUndo(idx);
    this.e.UndoCount = this.e.UndoTblSize = 0;
    return true;
  }

  internal new void SaveUndo(int BegLine, int BegCol, int EndLine, int EndCol, char type)
  {
    bool flag = true;
    if (this.e.InUndo || this.e.UndoRef == this.e.UndoSkipRef || (this.e.TerFlags3 & 262144 /*0x040000*/) != 0 || (this.e.TerOpFlags2 & 8192 /*0x2000*/) != 0)
      return;
    this.ReleaseRedo();
    if (EndCol < 0 && EndLine > BegLine && this.e.text[EndLine].len > 0)
    {
      --EndLine;
      EndCol = this.e.text[EndLine].len - 1;
    }
    int idx = this.e.UndoCount - 1;
    if (idx >= 0 && this.e.undo[idx].type == 'I' && type == 'I' && BegLine == EndLine && BegCol == EndCol && (this.e.TerFlags4 & 1024 /*0x0400*/) == 0 && (this.e.TerOpFlags & 1048576 /*0x100000*/) == 0 && this.RowColToAbs(EndLine, EndCol) == this.e.undo[idx].end + 1 && (this.e.TerFont[this.GetPrevCfmt(BegLine, BegCol)].style & 128 /*0x80*/) == 0)
    {
      ++this.e.undo[idx].end;
      this.FreeOneUndo(idx);
    }
    else
    {
      if (type == 'O')
        type = 'I';
      if (this.e.UndoCount == this.e.MaxUndos)
      {
        this.ScrollUndo();
        if (this.e.UndoSkipRef == this.e.UndoRef)
        {
          this.e.UndoTblSize = this.e.UndoCount;
          return;
        }
      }
      int undoCount = this.e.UndoCount;
      this.e.undo[undoCount] = new tc.StrUndo();
      this.e.undo[undoCount].type = type;
      this.e.undo[undoCount].id = this.e.UndoRef;
      this.e.undo[undoCount].TblLevel = -1;
      this.e.undo[undoCount].EmbTable = false;
      if (this.e.undo[undoCount].type == 'R')
        flag = this.SaveUndoRep(undoCount, BegLine, BegCol, EndLine, EndCol);
      else if (this.e.undo[undoCount].type == 'F')
        flag = this.SaveUndoFont(undoCount, BegLine, BegCol, EndLine, EndCol);
      else if (this.e.undo[undoCount].type == 'D')
        flag = this.SaveUndoDel(undoCount, BegLine, BegCol, EndLine, EndCol);
      else if (this.e.undo[undoCount].type == 'P')
        flag = this.SaveUndoPara(undoCount, BegLine, EndLine);
      else if (this.e.undo[undoCount].type == 'T')
        flag = this.SaveUndoRowDel(undoCount, ref BegLine, ref EndLine);
      else if (this.e.undo[undoCount].type == 'S')
        flag = this.SaveUndoRowIns(undoCount, ref BegLine, ref EndLine);
      else if (this.e.undo[undoCount].type == '1' || this.e.undo[undoCount].type == '2')
        flag = this.SaveUndoFrame(undoCount, ref BegLine, ref EndLine, (int) this.e.undo[undoCount].type);
      else if (this.e.undo[undoCount].type == '3')
        flag = this.SaveUndoPict(undoCount, ref BegLine, ref EndLine);
      else if (this.e.undo[undoCount].type == '4')
        flag = this.SaveUndoTableAttrib(undoCount, BegLine, BegCol, EndLine, EndCol);
      if (flag)
      {
        if (type == 'T' || type == 'S' || type == 'U' || type == 'V')
        {
          BegCol = 0;
          EndCol = this.e.text[EndLine].len - 1;
          if (EndCol < 0)
            EndCol = 0;
        }
        if (this.e.undo[undoCount].type != '1' && this.e.undo[undoCount].type != '2' && this.e.undo[undoCount].type != '4')
        {
          this.e.undo[undoCount].beg = this.RowColToAbs(BegLine, BegCol);
          this.e.undo[undoCount].end = this.RowColToAbs(EndLine, EndCol);
        }
        ++this.e.UndoCount;
        this.e.UndoTblSize = this.e.UndoCount;
      }
      this.e.OnUndoSaved(new EventArgs());
    }
  }

  private bool SaveUndoAlloc(
    char type,
    int idx,
    int BegLine,
    int BegCol,
    int EndLine,
    int EndCol,
    out char[] ppUndo,
    out ushort[] ppUndoCfmt,
    out int[] ppUndoPfmt)
  {
    int length = 0;
    this.FreeOneUndo(idx);
    char[] chArray = (char[]) null;
    ushort[] numArray1 = (ushort[]) null;
    int[] numArray2 = (int[]) null;
    if (type == 'D' || type == 'F')
    {
      if (EndCol >= this.e.text[EndLine].len)
      {
        length += 2;
        EndCol = this.e.text[EndLine].len - 1;
        if (EndCol < 0)
          EndCol = 0;
      }
      for (int index = BegLine; index <= EndLine; ++index)
        length = (BegLine != EndLine ? length + this.e.text[index].len : length + EndCol - BegCol + 1) + 2;
      ++length;
    }
    else if (type == 'P')
    {
      length = 2;
      for (int index = BegLine + 1; index <= EndLine; ++index)
      {
        if ((this.e.text[index].flags & 4) != 0)
          ++length;
      }
    }
    else if (type == 'R')
      length = 2;
    if (type == 'F')
      this.e.undo[idx].fmt = numArray1 = new ushort[length];
    else if (type == 'D' || type == 'R')
    {
      this.e.undo[idx].txt = chArray = new char[length];
      this.e.undo[idx].fmt = numArray1 = new ushort[length];
    }
    else if (type == 'P')
      this.e.undo[idx].pfmt = numArray2 = new int[length];
    ppUndo = chArray;
    ppUndoCfmt = numArray1;
    ppUndoPfmt = numArray2;
    return true;
  }

  private bool SaveUndoDel(int idx, int BegLine, int BegCol, int EndLine, int EndCol)
  {
    int DestIdx = 0;
    bool flag1 = false;
    bool flag2 = false;
    if (BegLine != EndLine || (this.e.text[EndLine].flags & 1) != 0 && EndCol + 1 >= this.e.text[EndLine].len)
      flag1 = true;
    if (flag1)
    {
      int hilightType = this.e.HilightType;
      int hilightBegRow = this.e.HilightBegRow;
      int hilightBegCol = this.e.HilightBegCol;
      int hilightEndRow = this.e.HilightEndRow;
      int hilightEndCol = this.e.HilightEndCol;
      this.e.HilightType = 2;
      this.e.HilightBegRow = BegLine;
      this.e.HilightBegCol = BegCol;
      this.e.HilightEndRow = EndLine;
      this.e.HilightEndCol = EndCol + 1;
      if (this.e.HilightEndCol > this.e.text[this.e.HilightEndRow].len)
        this.e.HilightEndCol = this.e.text[this.e.HilightEndRow].len;
      bool trackChanges = this.e.TrackChanges;
      this.e.TrackChanges = false;
      string OutData;
      this.RtfWrite(4, "", out OutData);
      this.e.TrackChanges = trackChanges;
      this.e.undo[idx].txt = OutData.ToCharArray();
      this.e.undo[idx].fmt = (ushort[]) null;
      this.e.undo[idx].TblLevel = this.e.ClipTblLevel;
      this.e.undo[idx].EmbTable = this.e.ClipEmbTable;
      this.e.ClipTblLevel = 1;
      this.e.ClipEmbTable = true;
      this.e.HilightType = hilightType;
      this.e.HilightBegRow = hilightBegRow;
      this.e.HilightBegCol = hilightBegCol;
      this.e.HilightEndRow = hilightEndRow;
      this.e.HilightEndCol = hilightEndCol;
    }
    else
    {
      if (EndCol >= this.e.text[EndLine].len)
        flag2 = true;
      char[] ppUndo;
      ushort[] ppUndoCfmt;
      if (!this.SaveUndoAlloc('D', idx, BegLine, BegCol, EndLine, EndCol, out ppUndo, out ppUndoCfmt, out tc.SkipInts))
        return false;
      tc.SkipInts = (int[]) null;
      int count = EndCol - BegCol + 1;
      if (count > this.e.text[BegLine].len)
        count = this.e.text[BegLine].len;
      char[] txt = this.e.text[BegLine].txt;
      ushort[] src = this.OpenCfmt(BegLine);
      this.FarMove(txt, BegCol, ppUndo, DestIdx, count);
      this.FarMove(src, BegCol, ppUndoCfmt, DestIdx, count);
      int index = DestIdx + count;
      this.CloseCfmt(BegLine);
      if (flag2)
        index = this.AddCrLf(index, ppUndo, ppUndoCfmt);
      ppUndo[index] = char.MinValue;
      ppUndoCfmt[index] = (ushort) 0;
    }
    return true;
  }

  private bool SaveUndoFont(int idx, int BegLine, int BegCol, int EndLine, int EndCol)
  {
    int index1 = 0;
    ushort[] ppUndoCfmt;
    if (!this.SaveUndoAlloc('F', idx, BegLine, BegCol, EndLine, EndCol, out char[] _, out ppUndoCfmt, out tc.SkipInts))
      return false;
    tc.SkipInts = (int[]) null;
    for (int line = BegLine; line <= EndLine; ++line)
    {
      if (this.e.text[line].len != 0)
      {
        int num1 = line != BegLine ? 0 : BegCol;
        int num2 = line != EndLine ? this.e.text[line].len - 1 : EndCol;
        ushort[] numArray = this.OpenCfmt(line);
        int index2 = num1;
        while (index2 <= num2)
        {
          ppUndoCfmt[index1] = numArray[index2];
          ++index2;
          ++index1;
        }
        this.CloseCfmt(line);
      }
    }
    ppUndoCfmt[index1] = (ushort) 0;
    return true;
  }

  internal bool SaveUndoFrame(int idx, ref int BegLine, ref int EndLine, int type)
  {
    int index = BegLine;
    int num;
    EndLine = num = 0;
    BegLine = num;
    if (index < 0 || index >= this.e.TotalParaFrames)
      return false;
    this.e.undo[idx].pFrame = new tc.ClsParaFrame();
    if ((this.e.TerOpFlags2 & 65536 /*0x010000*/) != 0)
      this.e.undo[idx].pFrame.frm = this.e.UndoParaFrame;
    else
      this.e.undo[idx].pFrame.frm = this.e.ParaFrame[index].Copy();
    this.e.undo[idx].ObjId = index;
    if (type == 49)
    {
      this.e.undo[idx].LinePtrU = this.CloneLinePtr();
      this.e.undo[idx].TotalLinesU = this.e.TotalLines;
      this.e.undo[idx].MaxLinesU = this.e.MaxLines;
      this.e.undo[idx].CursPos = this.RowColToAbs(this.e.CurLine, this.e.CurCol);
    }
    return true;
  }

  internal bool SaveUndoMarkedCells(int idx)
  {
    int num = 0;
    for (int index = 0; index < this.e.TotalCells; ++index)
    {
      if (this.e.cell[index].InUse && (this.e.cell[index].flags & 3) != 0)
        ++num;
    }
    if (num == 0)
      return false;
    this.e.undo[idx].RowId = new int[1];
    this.e.undo[idx].pRow = new tc.StrTableRow[1];
    this.e.undo[idx].CellId = new int[num + 1];
    this.e.undo[idx].pCell = new tc.StrCell[num + 1];
    int index1 = 0;
    int index2 = 0;
    for (int index3 = 0; index3 < this.e.TotalCells; ++index3)
    {
      if (this.e.cell[index3].InUse && (this.e.cell[index3].flags & 3) != 0)
      {
        this.e.undo[idx].CellId[index1] = index3;
        this.e.undo[idx].pCell[index1] = this.e.cell[index3].Copy();
        ++index1;
        int row = this.e.cell[index3].row;
        int index4 = 0;
        while (index4 < index2 && this.e.undo[idx].RowId[index4] != row)
          ++index4;
        if (index4 == index2)
        {
          this.e.undo[idx].RowId = this.ReAlloc(this.e.undo[idx].RowId, index2 + 2);
          this.e.undo[idx].pRow = this.ReAlloc(this.e.undo[idx].pRow, index2 + 2);
          this.e.undo[idx].RowId[index2] = row;
          this.e.undo[idx].pRow[index2] = this.e.TableRow[row].Copy();
          ++index2;
        }
      }
    }
    this.e.undo[idx].RowCount = index2;
    this.e.undo[idx].CellCount = index1;
    return true;
  }

  private bool SaveUndoPara(int idx, int BegLine, int EndLine)
  {
    int num = 0;
    int[] ppUndoPfmt;
    if (!this.SaveUndoAlloc('P', idx, BegLine, 0, EndLine, 0, out tc.SkipChars, out tc.SkipUshortArray, out ppUndoPfmt))
      return false;
    tc.SkipChars = (char[]) null;
    tc.SkipUshortArray = (ushort[]) null;
    int index1 = num + 1;
    for (int index2 = BegLine; index2 <= EndLine; ++index2)
    {
      if ((this.e.text[index2].flags & 4) != 0 || index2 == BegLine)
      {
        ppUndoPfmt[index1] = this.e.text[index2].pfmt;
        ++index1;
      }
    }
    ppUndoPfmt[0] = (int) (ushort) (index1 - 1);
    return true;
  }

  internal bool SaveUndoPict(int idx, ref int BegLine, ref int EndLine)
  {
    int num1 = BegLine;
    int num2;
    EndLine = num2 = 0;
    BegLine = num2;
    if (num1 < 0 || num1 >= this.e.TotalFonts)
      return false;
    this.e.undo[idx].ObjId = num1;
    this.e.undo[idx].width = this.e.UndoInt1;
    this.e.undo[idx].height = this.e.UndoInt2;
    return true;
  }

  private bool SaveUndoRep(int idx, int BegLine, int BegCol, int EndLine, int EndCol)
  {
    char[] ppUndo;
    ushort[] ppUndoCfmt;
    if (!this.SaveUndoAlloc('R', idx, BegLine, BegCol, EndLine, EndCol, out ppUndo, out ppUndoCfmt, out tc.SkipInts))
      return false;
    tc.SkipInts = (int[]) null;
    char[] txt = this.e.text[BegLine].txt;
    ushort[] numArray = this.OpenCfmt(BegLine);
    if (BegCol < this.e.text[BegLine].len)
    {
      ppUndo[0] = txt[BegCol];
      ppUndoCfmt[0] = numArray[BegCol];
    }
    else
    {
      ppUndo[0] = ' ';
      ppUndoCfmt[0] = (ushort) 0;
    }
    ppUndo[1] = char.MinValue;
    ppUndoCfmt[1] = (ushort) 0;
    return true;
  }

  private bool SaveUndoRowDel(int idx, ref int pBegLine, ref int pEndLine)
  {
    if (!this.GetUndoRowRange(ref pBegLine, ref pEndLine) || !this.SaveUndoDel(idx, pBegLine, 0, pEndLine, 0))
      return false;
    this.e.undo[idx].TblLevel = 0;
    this.e.undo[idx].EmbTable = false;
    return true;
  }

  private bool SaveUndoRowIns(int idx, ref int pBegLine, ref int pEndLine)
  {
    return this.GetUndoRowRange(ref pBegLine, ref pEndLine);
  }

  internal bool SaveUndoTableAttrib(int idx, int BegLine, int BegCell, int EndLine, int EndCell)
  {
    this.e.undo[idx].RowCount = 0;
    this.e.undo[idx].RowId = (int[]) null;
    this.e.undo[idx].CellId = (int[]) null;
    this.e.undo[idx].pRow = (tc.StrTableRow[]) null;
    this.e.undo[idx].pCell = (tc.StrCell[]) null;
    if (BegLine == 0 && EndLine == 0 && BegCell == 0 && EndCell == 0)
      return this.SaveUndoMarkedCells(idx);
    if (BegLine < 0)
    {
      int index = 0;
      while (index < this.e.TotalLines && this.e.text[index].cid != BegCell)
        ++index;
      if (index == this.e.TotalLines)
        return false;
      BegLine = index;
    }
    if (EndLine < 0)
    {
      if (EndCell <= 0)
      {
        EndLine = BegLine;
        while (EndLine < this.e.TotalLines && this.e.text[EndLine].cid > 0)
          ++EndLine;
        --EndLine;
      }
      else
      {
        int index = 0;
        while (index < this.e.TotalLines && this.e.text[index].cid != EndCell)
          ++index;
        if (index == this.e.TotalLines)
          return false;
        EndLine = index;
      }
    }
    if (EndLine < BegLine)
    {
      int num = BegLine;
      BegLine = EndLine;
      EndLine = num;
    }
    int num1;
    int num2 = num1 = 0;
    int num3 = 0;
    for (int index1 = BegLine; index1 <= EndLine; ++index1)
    {
      int cid = this.e.text[index1].cid;
      if (cid != 0)
      {
        int row = this.e.cell[cid].row;
        if (row != num3)
        {
          ++num2;
          for (int index2 = this.e.TableRow[row].FirstCell; index2 >= 0; index2 = this.e.cell[index2].NextCell)
            ++num1;
          num3 = row;
        }
      }
    }
    if (num2 == 0 || num1 == 0)
      return false;
    this.e.undo[idx].RowId = new int[num2 + 1];
    this.e.undo[idx].pRow = new tc.StrTableRow[num2 + 1];
    this.e.undo[idx].CellId = new int[num1 + 1];
    this.e.undo[idx].pCell = new tc.StrCell[num1 + 1];
    int index3;
    int index4 = index3 = 0;
    int num4 = 0;
    for (int index5 = BegLine; index5 <= EndLine; ++index5)
    {
      int cid = this.e.text[index5].cid;
      if (cid != 0)
      {
        int row = this.e.cell[cid].row;
        if (row != num4)
        {
          this.e.undo[idx].RowId[index4] = row;
          this.e.undo[idx].pRow[index4] = this.e.TableRow[row].Copy();
          ++index4;
          for (int index6 = this.e.TableRow[row].FirstCell; index6 >= 0; index6 = this.e.cell[index6].NextCell)
          {
            this.e.undo[idx].CellId[index3] = index6;
            this.e.undo[idx].pCell[index3] = this.e.cell[index6].Copy();
            ++index3;
          }
          num4 = row;
        }
      }
    }
    this.e.undo[idx].RowCount = index4;
    this.e.undo[idx].CellCount = index3;
    return true;
  }

  internal new bool ScrollUndo()
  {
    if (this.e.UndoCount > 0)
    {
      if (this.e.MaxUndos < this.e.MaxUndoLimit)
      {
        this.e.MaxUndos += 50;
        if (this.e.MaxUndos > this.e.MaxUndoLimit)
          this.e.MaxUndos = this.e.MaxUndoLimit;
        this.e.undo = this.ReAlloc(this.e.undo, this.e.MaxUndos + 1);
        return true;
      }
      int index1 = this.e.MaxUndoLimit / 4;
      if (index1 < 1)
        index1 = 1;
      if (index1 > 1000)
        index1 = 1000;
      if (this.e.undo[index1 - 1].id == this.e.undo[index1].id)
      {
        int index2 = index1;
        while (index2 < this.e.UndoCount && this.e.undo[index2].id == this.e.undo[index1 - 1].id)
          ++index2;
        index1 = index2;
      }
      this.e.UndoSkipRef = this.e.undo[index1 - 1].id;
      for (int idx = 0; idx < index1; ++idx)
        this.FreeOneUndo(idx);
      for (int index3 = index1; index3 < this.e.UndoCount; ++index3)
        this.e.undo[index3 - index1] = this.e.undo[index3];
      this.e.UndoCount -= index1;
      if (this.e.UndoCount < 0)
        this.e.UndoCount = 0;
    }
    return true;
  }

  internal bool TerFlushUndo()
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    return this.ReleaseUndo();
  }

  internal bool TerSetMaxUndo(int NewMaxUndoLimit)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (NewMaxUndoLimit >= this.e.MaxUndos)
      this.e.MaxUndoLimit = NewMaxUndoLimit;
    return true;
  }

  internal int TerSetUndoRef(int NewRef)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int undoRef = this.e.UndoRef;
    if (NewRef < 0)
      return undoRef;
    this.e.UndoRef = NewRef;
    return undoRef;
  }

  internal new bool TerUndo(bool DoUndo)
  {
    int num1 = -1;
    int row1 = 0;
    int row2 = 0;
    int col1 = 0;
    int col2 = 0;
    if (this.e.UndoCount <= 0 & DoUndo || this.e.UndoTblSize == this.e.UndoCount && !DoUndo)
      return true;
    this.e.InUndo = true;
    if (this.e.UndoTblSize < this.e.UndoCount)
      this.e.UndoTblSize = this.e.UndoCount;
    while (true)
    {
      int idx = !DoUndo ? this.e.UndoCount : this.e.UndoCount - 1;
      if (num1 < 0)
        num1 = this.e.undo[idx].id;
      if (idx >= 0 && idx < this.e.UndoTblSize && this.e.undo[idx].id == num1)
      {
        if (this.e.undo[idx].type != '1' && this.e.undo[idx].type != '2' && this.e.undo[idx].type != '3' && this.e.undo[idx].type != '4')
        {
          this.AbsToRowCol(this.e.undo[idx].beg, out row1, out col1);
          this.AbsToRowCol(this.e.undo[idx].end, out row2, out col2);
          this.e.CurLine = row1;
          this.e.CurCol = col1;
        }
        char ch1 = this.e.CurUndoType = this.e.undo[idx].type;
        switch (this.e.undo[idx].type)
        {
          case '1':
          case '2':
            int objId1 = this.e.undo[idx].ObjId;
            if (objId1 >= 0 && objId1 < this.e.TotalParaFrames)
            {
              if (this.e.undo[idx].type == '1')
              {
                tc.ClsLinePtr[] text = this.e.text;
                int totalLines = this.e.TotalLines;
                int maxLines = this.e.MaxLines;
                int abs = this.RowColToAbs(this.e.CurLine, this.e.CurCol);
                this.e.text = this.e.undo[idx].LinePtrU;
                this.e.TotalLines = this.e.undo[idx].TotalLinesU;
                this.e.MaxLines = this.e.undo[idx].MaxLinesU;
                this.AbsToRowCol(this.e.undo[idx].CursPos, 'C');
                this.e.undo[idx].LinePtrU = text;
                this.e.undo[idx].TotalLinesU = totalLines;
                this.e.undo[idx].MaxLinesU = maxLines;
                this.e.undo[idx].CursPos = abs;
              }
              tc.StrParaFrame strParaFrame = this.e.ParaFrame[objId1].Copy();
              this.e.ParaFrame[objId1] = this.e.undo[idx].pFrame.frm.Copy();
              this.e.undo[idx].pFrame.frm = strParaFrame.Copy();
              if (this.e.ParaFrame[objId1].pict > 0)
              {
                int pict = this.e.ParaFrame[objId1].pict;
                if (pict >= 0 && pict < this.e.TotalFonts)
                {
                  this.e.TerFont[pict].PictWidth = this.e.ParaFrame[objId1].width;
                  this.e.TerFont[pict].PictHeight = this.e.ParaFrame[objId1].height;
                  this.SetPictSize(pict, this.TwipsToScrY(this.e.TerFont[pict].PictHeight), this.TwipsToScrX(this.e.TerFont[pict].PictWidth), this.e.TerFont[pict].FrameType != 0);
                  this.XlateSizeForPrt(pict);
                }
              }
              this.e.TerRepaginate(true);
              break;
            }
            break;
          case '3':
            int objId2 = this.e.undo[idx].ObjId;
            this.SwapInts(ref this.e.TerFont[objId2].PictWidth, ref this.e.undo[idx].width);
            this.SwapInts(ref this.e.TerFont[objId2].PictHeight, ref this.e.undo[idx].height);
            this.SetPictSize(objId2, this.TwipsToScrY(this.e.TerFont[objId2].PictHeight), this.TwipsToScrX(this.e.TerFont[objId2].PictWidth), this.e.TerFont[objId2].FrameType != 0);
            this.XlateSizeForPrt(objId2);
            this.e.TerRepaint(true);
            break;
          case '4':
            this.UndoTableAttrib(idx);
            break;
          case 'D':
            char[] txt1 = this.e.undo[idx].txt;
            ushort[] fmt1 = this.e.undo[idx].fmt;
            bool trackChanges1 = this.e.TrackChanges;
            this.e.TrackChanges = false;
            if (this.e.undo[idx].fmt == null)
            {
              this.e.ClipTblLevel = this.e.undo[idx].TblLevel;
              this.e.ClipEmbTable = this.e.undo[idx].EmbTable;
              this.e.InsertRtfBuf(new string(txt1), this.e.CurLine, this.e.CurCol, false);
              this.e.ClipTblLevel = 1;
              this.e.ClipEmbTable = true;
            }
            else
              this.InsertBuffer(txt1, fmt1, (int[]) null, true);
            this.e.TrackChanges = trackChanges1;
            ch1 = 'I';
            break;
          case 'F':
            ushort[] fmt2 = this.e.undo[idx].fmt;
            this.e.undo[idx].fmt = (ushort[]) null;
            this.SaveUndoFont(idx, row1, col1, row2, col2);
            int index1 = 0;
            for (int line = row1; line <= row2; ++line)
            {
              if (this.e.text[line].len != 0)
              {
                int num2 = line != row1 ? 0 : col1;
                int num3 = line != row2 ? this.e.text[line].len - 1 : col2;
                ushort[] numArray = this.OpenCfmt(line);
                int index2 = num2;
                while (index2 <= num3)
                {
                  numArray[index2] = fmt2[index1];
                  ++index2;
                  ++index1;
                }
                this.CloseCfmt(line);
              }
            }
            break;
          case 'I':
            if (DoUndo)
              this.SaveUndoDel(idx, row1, col1, row2, col2);
            this.e.HilightType = 2;
            this.e.HilightBegRow = row1;
            this.e.HilightEndRow = row2;
            this.e.HilightBegCol = col1;
            this.e.HilightEndCol = col2 + 1;
            if (this.e.HilightEndCol >= this.e.text[this.e.HilightEndRow].len && this.e.HilightEndRow + 1 < this.e.TotalLines && this.e.text[this.e.HilightEndRow].cid == this.e.text[this.e.HilightEndRow + 1].cid)
            {
              ++this.e.HilightEndRow;
              this.e.HilightEndCol = 0;
            }
            if (this.e.HilightBegRow == this.e.HilightEndRow && col1 == col2)
              this.e.TerOpFlags2 |= 2;
            bool trackChanges2 = this.e.TrackChanges;
            this.e.TrackChanges = false;
            this.DeleteCharBlock(false, false);
            this.e.TrackChanges = trackChanges2;
            this.e.TerOpFlags2 &= -3;
            this.e.HilightType = 0;
            ch1 = 'D';
            break;
          case 'P':
            int[] pfmt = this.e.undo[idx].pfmt;
            int num4 = pfmt[0];
            int index3 = 1;
            int num5 = 0;
            for (int index4 = row1; index4 <= row2; ++index4)
            {
              if ((this.e.text[index4].flags & 4) != 0 || index4 == row1)
              {
                num5 = pfmt[index3];
                pfmt[index3] = this.e.text[index4].pfmt;
                if (index3 < num4)
                  ++index3;
              }
              this.e.text[index4].pfmt = num5;
            }
            break;
          case 'R':
            char[] txt2 = this.e.undo[idx].txt;
            ushort[] fmt3 = this.e.undo[idx].fmt;
            char ch2 = txt2[0];
            ushort num6 = fmt3[0];
            this.SaveUndoRep(idx, row1, col1, row2, col2);
            this.e.text[this.e.CurLine].txt[this.e.CurCol] = ch2;
            this.OpenCfmt(this.e.CurLine)[this.e.CurCol] = num6;
            this.CloseCfmt(this.e.CurLine);
            break;
          case 'S':
            if (DoUndo)
              this.SaveUndoRowDel(idx, ref row1, ref row2);
            this.e.HilightType = 2;
            this.e.HilightBegRow = row1;
            this.e.HilightEndRow = row2;
            this.e.HilightBegCol = col1;
            this.e.HilightEndCol = col2 + 1;
            if (this.e.HilightEndCol >= this.e.text[this.e.HilightEndRow].len && this.e.HilightEndRow + 1 < this.e.TotalLines && this.e.text[this.e.HilightEndRow].cid == this.e.text[this.e.HilightEndRow + 1].cid)
            {
              ++this.e.HilightEndRow;
              this.e.HilightEndCol = 0;
            }
            if (this.e.HilightBegRow == this.e.HilightEndRow && col1 == col2)
              this.e.TerOpFlags2 |= 2;
            this.DeleteCharBlock(false, false);
            this.e.TerOpFlags2 = tc.ResetFlag(this.e.TerOpFlags2, 2);
            this.e.HilightType = 0;
            ch1 = 'T';
            break;
          case 'T':
            this.e.ClipTblLevel = this.e.undo[idx].TblLevel;
            this.e.ClipEmbTable = this.e.undo[idx].EmbTable;
            this.e.InsertRtfBuf(new string(this.e.undo[idx].txt), this.e.CurLine, this.e.CurCol, false);
            this.e.ClipTblLevel = 1;
            this.e.ClipEmbTable = true;
            ch1 = 'S';
            break;
        }
        this.e.undo[idx].type = ch1;
        if (DoUndo)
          --this.e.UndoCount;
        else
          ++this.e.UndoCount;
      }
      else
        break;
    }
    this.e.InUndo = false;
    if (this.e.CurLine >= this.e.TotalLines)
      --this.e.CurLine;
    if (this.e.CurLine - this.e.BeginLine >= this.e.WinHeight || this.e.CurLine - this.e.BeginLine < 0)
      this.e.BeginLine = this.e.CurLine - this.e.WinHeight / 2;
    if (this.e.BeginLine < 0)
      this.e.BeginLine = 0;
    this.e.CurRow = this.e.CurLine - this.e.BeginLine;
    this.e.PaintFlag = 4;
    this.PaintTer();
    return true;
  }

  internal bool UndoTableAttrib(int idx)
  {
    if (this.e.undo[idx].RowCount == 0 || this.e.undo[idx].CellCount == 0)
      return false;
    for (int index1 = 0; index1 < this.e.undo[idx].RowCount; ++index1)
    {
      int index2 = this.e.undo[idx].RowId[index1];
      if (this.e.TableRow[index2].PrevRow != this.e.undo[idx].pRow[index1].PrevRow || this.e.TableRow[index2].NextRow != this.e.undo[idx].pRow[index1].NextRow || this.e.TableRow[index2].FirstCell != this.e.undo[idx].pRow[index1].FirstCell)
        return false;
    }
    for (int index3 = 0; index3 < this.e.undo[idx].CellCount; ++index3)
    {
      int index4 = this.e.undo[idx].CellId[index3];
      if (this.e.cell[index4].PrevCell != this.e.undo[idx].pCell[index3].PrevCell || this.e.cell[index4].NextCell != this.e.undo[idx].pCell[index3].NextCell)
        return false;
    }
    int rowCount = this.e.undo[idx].RowCount;
    int cellCount = this.e.undo[idx].CellCount;
    int[] numArray1 = new int[rowCount + 1];
    tc.StrTableRow[] strTableRowArray = new tc.StrTableRow[rowCount + 1];
    int[] numArray2 = new int[cellCount + 1];
    tc.StrCell[] strCellArray = new tc.StrCell[cellCount + 1];
    for (int index5 = 0; index5 < rowCount; ++index5)
    {
      int index6 = numArray1[index5] = this.e.undo[idx].RowId[index5];
      strTableRowArray[index5] = this.e.TableRow[index6].Copy();
    }
    for (int index7 = 0; index7 < cellCount; ++index7)
    {
      int index8 = numArray2[index7] = this.e.undo[idx].CellId[index7];
      strCellArray[index7] = this.e.cell[index8].Copy();
    }
    for (int index = 0; index < this.e.undo[idx].RowCount; ++index)
      this.e.TableRow[this.e.undo[idx].RowId[index]] = this.e.undo[idx].pRow[index].Copy();
    for (int index = 0; index < this.e.undo[idx].CellCount; ++index)
      this.e.cell[this.e.undo[idx].CellId[index]] = this.e.undo[idx].pCell[index].Copy();
    this.e.undo[idx].RowId = numArray1;
    this.e.undo[idx].CellId = numArray2;
    this.e.undo[idx].pRow = strTableRowArray;
    this.e.undo[idx].pCell = strCellArray;
    this.RequestPagination(true);
    this.RefreshFrames(true);
    return true;
  }
}
