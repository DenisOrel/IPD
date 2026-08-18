// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.CEdit
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class CEdit : COp
{
  internal CEdit(ImRtfEditor ImRtfEditor)
    : base(ImRtfEditor)
  {
  }

  internal bool AdjustInputPos(char CurChar)
  {
    if (CurChar == ' ' && !this.e.ShowFieldNames && this.e.CurCol != 0)
    {
      int curCfmt = this.GetCurCfmt(this.e.CurLine, this.e.CurCol);
      if (this.e.TerFont[curCfmt].FieldId == 7 && this.e.TerFont[this.GetCurCfmt(this.e.CurLine, this.e.CurCol - 1)].FieldId == 6)
      {
        ushort[] numArray = this.OpenCfmt(this.e.CurLine);
        int index = this.e.CurCol - 1;
        while (index >= 0 && this.e.TerFont[(int) numArray[index]].FieldId == 6)
          --index;
        this.CloseCfmt(this.e.CurLine);
        this.e.CurCol = index + 1;
        if (this.e.InputFontId >= 0)
          this.e.InputFontId = this.SetFontFieldId(this.e.InputFontId, 0, "");
        else
          this.e.InputFontId = this.SetFontFieldId(curCfmt, 0, "");
        if ((this.e.TerFont[this.e.InputFontId].style & 512 /*0x0200*/) != 0)
          this.e.InputFontId = this.SetFontStyle(this.e.InputFontId, 512 /*0x0200*/, false);
        this.e.CursDirection = 1;
      }
    }
    if (this.e.CurCol == 0 && this.e.InputFontId > 0 && this.e.TerFont[this.e.InputFontId].FieldId == 14)
    {
      if (this.e.CurLine == 0)
      {
        this.e.InputFontId = 0;
      }
      else
      {
        int prevCfmt = this.GetPrevCfmt(this.e.CurLine, this.e.CurCol);
        if (this.e.TerFont[prevCfmt].FieldId != 14)
          this.e.InputFontId = prevCfmt;
      }
    }
    return true;
  }

  internal new bool CanDragText()
  {
    if (this.e.HilightType == 2)
    {
      int abs1 = this.RowColToAbs(this.e.HilightBegRow, this.e.HilightBegCol);
      int abs2 = this.RowColToAbs(this.e.HilightEndRow, this.e.HilightEndCol);
      int abs3 = this.RowColToAbs(this.e.MouseLine, this.e.MouseCol);
      if (!this.e.IsProtectedZone(abs1, true) && !this.e.IsProtectedZone(abs2, true) && !this.e.IsProtectedZone(abs3, true))
        return false;
      if (abs1 > abs2)
        this.SwapInts(ref abs1, ref abs2);
      if ((!this.e.TerArg.ReadOnly || (this.e.TerFlags6 & 524288 /*0x080000*/) != 0) && (this.e.TerFlags & 2097152 /*0x200000*/) == 0 && abs3 >= abs1 && abs3 < abs2 && this.e.text[this.e.HilightBegRow].cid == this.e.text[this.e.HilightEndRow].cid && this.e.text[this.e.HilightBegRow].fid == this.e.text[this.e.HilightEndRow].fid)
        return !this.e.PictureHilighted || this.e.TerFont[this.GetCurCfmt(this.e.HilightBegRow, this.e.HilightBegCol)].FrameType != 3;
    }
    return false;
  }

  internal new bool CanInsert(int line, int col)
  {
    if (line >= this.e.TotalLines || this.e.IsProtectedZone(this.pos.TerRowColToAbs(line, col), false))
      return false;
    if (col < this.e.text[line].len || this.e.TerArg.WordWrap)
    {
      if (col >= this.e.text[line].len || this.e.TerArg.PageMode && !this.e.EditPageHdrFtr && (this.e.PfmtId[this.e.text[line].pfmt].flags & 12288 /*0x3000*/) != 0 || (this.e.text[line].flags & 1966080 /*0x1E0000*/) != 0)
        return false;
      int curCfmt = this.GetCurCfmt(line, col);
      if ((this.e.TerFont[curCfmt].style & 512 /*0x0200*/) != 0)
      {
        int prevCfmt = this.GetPrevCfmt(line, col);
        if ((this.e.TerFont[prevCfmt].style & 512 /*0x0200*/) != 0)
        {
          bool flag = false;
          if (this.e.TerFont[curCfmt].FieldId == 6 && this.e.TerFont[prevCfmt].FieldId == 6 && this.e.text[line].txt[col] == '{' && (this.e.TerFont[this.GetNextCfmt(line, col)].style & 512 /*0x0200*/) == 0)
            flag = true;
          if (!flag)
            return false;
        }
      }
      if (this.True(this.e.text[line].tabw) && (this.e.text[line].tabw.type & 32 /*0x20*/) != 0 || this.IsDynField(this.e.TerFont[curCfmt].FieldId) && curCfmt == this.GetPrevCfmt(line, col))
        return false;
    }
    return true;
  }

  internal new bool CanInsertBreakChar(int line, int col)
  {
    return !this.e.IsProtectedZone(this.pos.TerRowColToAbs(line, col), false) && (this.e.PfmtId[this.e.text[line].pfmt].flags & 12288 /*0x3000*/) == 0 && this.CanInsertObject(line, col);
  }

  internal new bool CanInsertInInputField(int pict, int line, int col)
  {
    if (this.e.IsProtectedZone(this.pos.TerRowColToAbs(line, col), false))
      return false;
    if (this.e.TerFont[pict].FieldCode == null)
      return true;
    int num1 = this.ToInt(this.GetStringField(this.e.TerFont[pict].FieldCode, 0, '|'));
    if (num1 == 0)
      return true;
    int StartLine1 = line;
    int StartCol1 = col;
    if (!this.e.TerLocateFieldChar(2, (string) null, false, ref StartLine1, ref StartCol1, false))
    {
      StartLine1 = 0;
      StartCol1 = 0;
    }
    int abs = this.RowColToAbs(StartLine1, StartCol1);
    int StartLine2 = line;
    int StartCol2 = col;
    if (!this.e.TerLocateFieldChar(2, (string) null, false, ref StartLine2, ref StartCol2, true))
      return true;
    int num2 = this.RowColToAbs(StartLine2, StartCol2) - abs - 1;
    return num1 > num2;
  }

  internal new bool CanInsertObject(int line, int col)
  {
    return !this.e.IsProtectedZone(this.pos.TerRowColToAbs(line, col), false) && !this.True(this.e.text[line].fid) && !this.True(this.e.text[line].cid) && this.CanInsertTextObject(line, col);
  }

  internal new bool CanInsertTextObject(int line, int col)
  {
    if (this.e.IsProtectedZone(this.pos.TerRowColToAbs(line, col), false) || (this.e.text[line].flags & 1966080 /*0x1E0000*/) != 0 || this.LineInfo(line, 32 /*0x20*/))
      return false;
    int curCfmt = this.GetCurCfmt(line, col);
    int prevCfmt = this.GetPrevCfmt(line, col);
    int fieldId1 = this.e.TerFont[curCfmt].FieldId;
    int fieldId2 = this.e.TerFont[prevCfmt].FieldId;
    return (fieldId1 != 7 || fieldId2 != 6) && (fieldId1 != 7 || fieldId2 != 7) && (fieldId1 != 6 || fieldId2 != 6) && (this.e.TerFont[curCfmt].style & 6144) == 0;
  }

  internal new void CopyLineData(int SrcLine, int DestLine)
  {
    int len = this.e.text[SrcLine].len;
    this.LineAlloc(DestLine, 0, len);
    if (len == 0)
      return;
    char[] ptr = new char[len + 1];
    ushort[] fmt = new ushort[len];
    ushort[] ct = new ushort[len];
    this.GetLineData(SrcLine, 0, len, ptr, fmt, ct);
    this.SetLineData(DestLine, 0, len, ptr, fmt, ct);
    this.e.text[DestLine].pfmt = this.e.text[SrcLine].pfmt;
    this.e.text[DestLine].cid = this.e.text[SrcLine].cid;
    this.e.text[DestLine].fid = this.e.text[SrcLine].fid;
    this.e.text[DestLine].flags = this.e.text[SrcLine].flags;
  }

  internal bool DoAutoComp(char CurChar)
  {
    if (!this.e.InAutoComp)
    {
      this.e.AutoCompPos = this.RowColToAbs(this.e.CurLine, this.e.CurCol);
      if (CurChar >= ' ')
        --this.e.AutoCompPos;
      this.e.InAutoComp = true;
      return true;
    }
    if (this.e.CurCol != 0)
    {
      if (CurChar != ' ' && CurChar != ',')
        return true;
      int row;
      int col;
      this.AbsToRowCol(this.e.AutoCompPos, out row, out col);
      if (row == this.e.CurLine)
      {
        char[] txt = this.e.text[row].txt;
        int index1 = col;
        while (index1 < this.e.CurCol && (txt[index1] == ' ' || txt[index1] == ',' || txt[index1] == ';' || txt[index1] < ' '))
          ++index1;
        if (index1 != this.e.CurCol)
        {
          col = index1;
          if (col > 0)
          {
            switch (txt[col - 1])
            {
              case '\t':
              case ' ':
              case ',':
              case ';':
                break;
              default:
                goto label_23;
            }
          }
          int index2 = col;
          while (index2 < this.e.CurCol && txt[index2] != ' ' && txt[index2] != ',' && txt[index2] != ';' && txt[index2] >= ' ')
            ++index2;
          if (col != index2)
          {
            int num1 = index2 - col;
            string str = new string(txt, col, num1);
            int index3 = 0;
            while (index3 < this.e.TotalAutoComps && !(str == this.e.AutoCompWord[index3]))
              ++index3;
            if (index3 != this.e.TotalAutoComps)
            {
              int num2 = row;
              int BegCol = col;
              int undoRef = this.e.UndoRef;
              this.SaveUndo(num2, BegCol, num2, BegCol + num1 - 1, 'D');
              this.ReplaceTextInPlace(ref row, ref col, num1, this.e.AutoCompPhrase[index3]);
              this.e.UndoRef = undoRef;
              this.SaveUndo(num2, BegCol, num2, BegCol + this.e.AutoCompPhrase[index3].Length - 1, 'I');
              this.e.CurCol += this.e.AutoCompPhrase[index3].Length - num1;
            }
          }
        }
      }
    }
label_23:
    this.e.AutoCompPos = this.RowColToAbs(this.e.CurLine, this.e.CurCol);
    return true;
  }

  internal new bool FixPos()
  {
    int curLine = this.e.CurLine;
    int curCol = this.e.CurCol;
    int num = this.FixPos(ref curLine, ref curCol) ? 1 : 0;
    this.e.CurLine = curLine;
    this.e.CurCol = curCol;
    return num != 0;
  }

  internal new bool FixPos(ref int pLine, ref int pCol)
  {
    int index = pLine;
    int num = pCol;
    if (index < 0)
    {
      index = 0;
      num = 0;
    }
    if (index >= this.e.TotalLines)
    {
      index = this.e.TotalLines - 1;
      num = this.e.text[index].len - 1;
    }
    if (this.e.TerArg.WordWrap)
    {
      if (num < 0)
      {
        --index;
        while (index >= 0 && this.e.text[index].len == 0)
          --index;
        if (index < 0)
        {
          index = 0;
          num = 0;
        }
        else
          num = this.e.text[index].len - 1;
      }
      else if (num >= this.e.text[index].len)
      {
        ++index;
        while (index < this.e.TotalLines && this.e.text[index].len == 0)
          ++index;
        if (index >= this.e.TotalLines)
        {
          index = this.e.TotalLines - 1;
          num = this.e.text[index].len - 1;
          if (num < 0)
            num = 0;
        }
        else
          num = 0;
      }
      pLine = index;
      pCol = num;
    }
    return true;
  }

  internal new char GetCurChar(int line, int col)
  {
    return line >= this.e.TotalLines || col < 0 || col >= this.e.text[line].len ? char.MinValue : this.e.text[line].txt[col];
  }

  internal new bool GetCursDirection()
  {
    if (this.e.CursDirection == 1 || this.e.CursDirection == 4)
      return true;
    if (this.e.CursDirection == 2 || this.e.CursDirection == 3)
      return false;
    bool cursDirection = false;
    if (this.e.TerArg.PageMode)
      return this.e.CurPage > this.e.PrevCursPage || this.e.CurPage == this.e.PrevCursPage && this.e.CurLineY + this.e.TerWinOrgY > this.e.PrevCursLineY || this.e.CurLine == this.e.PrevCursLine && this.e.CurCol >= this.e.PrevCursCol || cursDirection;
    if (this.e.CurLine > this.e.PrevCursLine)
      return true;
    if (this.e.CurLine == this.e.PrevCursLine && this.e.CurCol >= this.e.PrevCursCol)
      cursDirection = true;
    return cursDirection;
  }

  internal new void GetLineData(
    int SrcLine,
    int SrcCol,
    int count,
    char[] ptr,
    ushort[] fmt,
    ushort[] ct)
  {
    char[] txt = this.e.text[SrcLine].txt;
    ushort[] src1 = this.OpenCfmt(SrcLine);
    ushort[] src2 = this.OpenCtid(SrcLine);
    if (this.True(ptr))
    {
      this.FarMoveOl(txt, SrcCol, ptr, 0, count);
      ptr[count] = char.MinValue;
    }
    if (this.True(fmt))
      this.FarMoveOl(src1, SrcCol, fmt, 0, count);
    if (this.True(ct))
      this.FarMoveOl(src2, SrcCol, ct, 0, count);
    this.CloseCharInfo(SrcLine);
  }

  internal new bool HiddenText(int CurFont)
  {
    bool flag = false;
    int style = this.e.TerFont[CurFont].style;
    if (!this.e.ShowHiddenText && (style & 64 /*0x40*/) != 0)
      flag = true;
    if (!this.e.EditFootnoteText && (style & 2048 /*0x0800*/) != 0 && (style & 32768 /*0x8000*/) == 0 && (this.e.TerOpFlags & 1024 /*0x0400*/) == 0)
      flag = true;
    if (!this.e.EditEndnoteText && (style & 32768 /*0x8000*/) != 0)
      flag = true;
    if (!this.e.ShowFieldNames && this.e.TerFont[CurFont].FieldId == 6)
      flag = true;
    if (this.e.ShowFieldNames && this.e.TerFont[CurFont].FieldId == 7)
      flag = true;
    return flag;
  }

  internal new bool InsertMarkerLine(
    int LineNo,
    char BreakChar,
    int CurFont,
    int CurParaId,
    int TabwType,
    int CurCellId)
  {
    if (!this.CheckLineLimit(this.e.TotalLines + 1))
    {
      this.PrintError(88, nameof (InsertMarkerLine));
      return false;
    }
    int fid = this.e.text[LineNo].fid;
    this.MoveLineArrays(LineNo, 1, 'B');
    this.LineAlloc(LineNo, 0, 1);
    this.e.text[LineNo].fid = fid;
    char[] txt = this.e.text[LineNo].txt;
    ushort[] numArray = this.OpenCfmt(LineNo);
    int num = (int) BreakChar;
    txt[0] = (char) num;
    numArray[0] = (ushort) CurFont;
    this.CloseCfmt(LineNo);
    if (CurParaId >= 0)
      this.e.text[LineNo].pfmt = CurParaId;
    else if ((this.e.PfmtId[this.e.text[LineNo].pfmt].flags & 12288 /*0x3000*/) != 0 != ((this.e.PfmtId[this.e.text[LineNo + 1].pfmt].flags & 12288 /*0x3000*/) != 0))
      this.e.text[LineNo].pfmt = this.e.text[LineNo + 1].pfmt;
    if (CurCellId >= 0)
      this.e.text[LineNo].cid = CurCellId;
    if ((int) BreakChar == (int) this.e.ParaChar || (int) BreakChar == (int) this.e.CellChar)
      this.e.text[LineNo].flags |= 1;
    if (this.True(TabwType))
    {
      this.AllocTabw(LineNo);
      this.e.text[LineNo].tabw.type = TabwType;
    }
    return true;
  }

  internal new bool IsHiddenLine(int LineNo)
  {
    int num = -1;
    if (this.e.text[LineNo].fmt == null)
      return this.HiddenText((int) this.e.text[LineNo].UniFmt);
    ushort[] fmt = this.e.text[LineNo].fmt;
    int len = this.e.text[LineNo].len;
    int index;
    for (index = 0; index < len; ++index)
    {
      int CurFont = (int) fmt[index];
      if (CurFont != num)
      {
        if (this.HiddenText(CurFont))
          num = CurFont;
        else
          break;
      }
    }
    return index >= len;
  }

  internal new bool IsProtectedChar(int line, int col)
  {
    if (this.e.IsProtectedZone(this.pos.TerRowColToAbs(line, col), true) || this.e.CheckTextTag(line, col, 77))
      return true;
    if (line < this.e.TotalLines && col < this.e.text[line].len)
    {
      if (this.e.TerArg.PageMode && !this.e.EditPageHdrFtr && (this.e.PfmtId[this.e.text[line].pfmt].flags & 12288 /*0x3000*/) != 0 || (this.e.text[line].flags & 1966080 /*0x1E0000*/) != 0 || line > 0 && this.e.text[line].len == 1 && (this.e.PfmtId[this.e.text[line - 1].pfmt].flags & 12288 /*0x3000*/) != 0 && (this.e.PfmtId[this.e.text[line].pfmt].flags & 12288 /*0x3000*/) == 0 && (line >= this.e.TotalLines - 1 || this.True(this.e.text[line + 1].tabw) && (this.e.text[line + 1].tabw.type & 2) != 0))
        return true;
      int curCfmt = this.GetCurCfmt(line, col);
      if ((this.e.TerFont[curCfmt].style & 512 /*0x0200*/) != 0)
        return true;
      int index1;
      int index2 = index1 = 0;
      if (col > 0)
      {
        index2 = this.GetCurCfmt(line, col - 1);
      }
      else
      {
        int line1 = line - 1;
        while (line1 >= 0 && this.e.text[line1].len <= 0)
          --line1;
        if (line1 >= 0)
          index2 = this.GetCurCfmt(line1, this.e.text[line1].len - 1);
      }
      if (col + 1 < this.e.text[line].len)
      {
        index1 = this.GetCurCfmt(line, col + 1);
      }
      else
      {
        int line2 = line + 1;
        while (line2 < this.e.TotalLines && this.e.text[line2].len <= 0)
          ++line2;
        if (line2 < this.e.TotalLines)
          index1 = this.GetCurCfmt(line2, 0);
      }
      if ((this.e.TerFlags2 & 8) == 0 && (this.e.TerFont[index2].style & 512 /*0x0200*/) != 0 && (this.e.TerFont[index1].style & 512 /*0x0200*/) != 0 || (this.e.TerFont[curCfmt].style & 5120) != 0 || (this.e.TerFont[curCfmt].style & 2048 /*0x0800*/) != 0 && (this.e.TerFont[index2].style & 1024 /*0x0400*/) != 0 && (this.e.TerFont[index1].style & 2048 /*0x0800*/) == 0 || (this.e.TerFlags5 & 536870912 /*0x20000000*/) != 0 && (this.e.TerFont[curCfmt].FieldId == 6 || this.e.TerFont[curCfmt].FieldId == 7) || !this.e.ShowParaMark && !this.e.PictureClicked && (this.e.TerFont[curCfmt].style & 128 /*0x80*/) != 0 && this.e.TerFont[curCfmt].FrameType != 0)
        return true;
      char ch = this.e.text[line].txt[col];
      if ((int) ch == (int) this.e.CellChar || ch == '\u0012' || this.e.text[line].fid > 0 && (line == this.e.TotalLines - 1 || this.e.text[line + 1].fid == 0) && (int) ch == (int) this.e.ParaChar || col + 1 == this.e.text[line].len && (this.e.text[line].flags & 1) != 0 && line + 1 < this.e.TotalLines && this.e.text[line].fid != this.e.text[line + 1].fid || this.e.HtmlMode && col > 0 && col + 1 == this.e.text[line].len && (this.e.text[line].flags & 1) != 0 && (line + 1 >= this.e.TotalLines || this.e.PfmtId[this.e.text[line].pfmt].AuxId != this.e.PfmtId[this.e.text[line + 1].pfmt].AuxId) || col + 1 == this.e.text[line].len && (this.e.text[line].flags & 1) != 0 && !this.LineInfo(line, 2) && line + 1 < this.e.TotalLines && (this.e.text[line + 1].flags & 1966080 /*0x1E0000*/) != 0 || (this.e.TerFlags4 & 8388608 /*0x800000*/) != 0 && this.e.text[line].cid == 0 && col + 1 == this.e.text[line].len && (this.e.text[line].flags & 1) != 0 && !this.LineInfo(line, 2) && line + 1 < this.e.TotalLines && this.e.text[line + 1].cid > 0)
        return true;
    }
    return false;
  }

  internal new int LastScrollBeginLine()
  {
    int num = 0;
    int lin;
    for (lin = this.e.TotalLines - 1; lin >= 0; --lin)
    {
      num += this.GetLineHeight(lin, out int _, out tc.SkipInt);
      if (num > this.e.TerWinHeight)
        break;
    }
    return lin + 1;
  }

  internal new void LineAlloc(int LineNo, int OldSize, int NewSize)
  {
    ++this.e.TerArg.modified;
    if (this.e.text[LineNo].fmt != null)
      this.FmtAlloc(LineNo, OldSize, NewSize);
    if (this.e.text[LineNo].tag != null)
      this.CtidAlloc(LineNo, OldSize, NewSize);
    if (this.e.text[LineNo].cwidth != null)
      this.CharWidthAlloc(LineNo, OldSize, NewSize);
    if (OldSize == 0 && NewSize == 0)
    {
      this.e.text[LineNo].txt = (char[]) null;
      this.e.text[LineNo].len = 0;
      this.e.text[LineNo].tabw = (tc.ClsTabw) null;
    }
    else if (OldSize == NewSize)
      this.e.text[LineNo].len = NewSize;
    else if (NewSize == 0)
    {
      this.e.text[LineNo].txt = (char[]) null;
      this.e.text[LineNo].len = 0;
      this.e.text[LineNo].tabw = (tc.ClsTabw) null;
    }
    else if (OldSize != 0)
    {
      this.e.text[LineNo].txt = this.ReAlloc(this.e.text[LineNo].txt, NewSize + 1);
      this.e.text[LineNo].len = NewSize;
    }
    else
    {
      this.e.text[LineNo].txt = new char[NewSize + 1];
      this.e.text[LineNo].len = NewSize;
    }
  }

  internal new bool LineSelected(int LineNo)
  {
    if (this.e.HilightType != 2 || LineNo < 0 || LineNo >= this.e.TotalLines)
      return true;
    int level = this.MinTableLevel(this.e.HilightBegRow, this.e.HilightEndRow);
    int CurCell = this.LevelCell(level, LineNo);
    if (CurCell == 0)
      return true;
    int index1 = this.LevelCell(level, this.e.HilightBegRow);
    int index2 = this.LevelCell(level, this.e.HilightEndRow);
    if (index1 == 0 || index2 == 0 || !this.InSameTable(index1, index2) || this.IsFirstTableRow(this.e.cell[index1].row) && this.IsLastTableRow(this.e.cell[index2].row) && this.e.cell[index1].PrevCell <= 0 && this.e.cell[index2].NextCell <= 0)
      return true;
    if (this.e.cell[index1].PrevCell <= 0 && this.e.cell[index2].NextCell <= 0)
    {
      int row1 = this.e.cell[index1].row;
      int row2 = this.e.cell[index2].row;
      int row3 = this.e.cell[CurCell].row;
      if (row3 == row1 || row3 == row2)
        return true;
      for (int index3 = row1; index3 > 0; index3 = this.e.TableRow[index3].NextRow)
      {
        if (index3 == row3)
          return true;
        if (index3 == row2)
          break;
      }
    }
    int cellColumn1 = this.GetCellColumn(index1, true);
    int num1 = cellColumn1 + this.e.cell[index1].ColSpan - 1;
    int cellColumn2 = this.GetCellColumn(index2, true);
    int num2 = cellColumn2 + this.e.cell[index2].ColSpan - 1;
    int num3 = cellColumn1 < cellColumn2 ? cellColumn1 : cellColumn2;
    int num4 = num1 > num2 ? num1 : num2;
    int cellColumn3 = this.GetCellColumn(CurCell, true);
    return cellColumn3 >= num3 && cellColumn3 <= num4;
  }

  internal new void MoveCharInfo(int SrcLine, int SrcCol, int DestLine, int DestCol, int count)
  {
    char[] txt1 = this.e.text[SrcLine].txt;
    ushort[] numArray1 = this.OpenCfmt(SrcLine);
    ushort[] numArray2 = this.OpenCtid(SrcLine);
    if (SrcLine == DestLine)
    {
      this.FarMoveOl(txt1, SrcCol, DestCol, count);
      this.FarMoveOl(numArray1, SrcCol, DestCol, count);
      this.FarMoveOl(numArray2, SrcCol, DestCol, count);
    }
    else
    {
      char[] txt2 = this.e.text[DestLine].txt;
      ushort[] dest1 = this.OpenCfmt(DestLine);
      ushort[] dest2 = this.OpenCtid(DestLine);
      this.FarMove(txt1, SrcCol, txt2, DestCol, count);
      this.FarMove(numArray1, SrcCol, dest1, DestCol, count);
      this.FarMove(numArray2, SrcCol, dest2, DestCol, count);
      this.CloseCharInfo(DestLine);
    }
    this.CloseCharInfo(SrcLine);
  }

  internal new bool MoveCursor(int LineNo, int col)
  {
    int curCfmt1 = this.GetCurCfmt(LineNo, col);
    bool flag1 = (this.e.TerFlags2 & 536870912 /*0x20000000*/) != 0;
    bool flag2 = (this.e.TerFlags5 & 2048 /*0x0800*/) != 0;
    bool flag3 = (this.e.TerFlags2 & 1073741824 /*0x40000000*/) != 0;
    bool flag4 = (this.e.TerFlags4 & 2) != 0;
    if (this.e.HtmlMode)
    {
      if (!this.e.TerArg.ReadOnly)
      {
        int prevCfmt = this.GetPrevCfmt(LineNo, col);
        bool flag5 = this.HiddenText(curCfmt1);
        bool flag6 = this.HiddenText(prevCfmt);
        if (flag5 && !flag6)
          return false;
        if (!this.e.ShowHiddenText & flag6 && !flag5 && this.IsHypertext(curCfmt1))
          return true;
      }
      if (this.HiddenText(curCfmt1))
        return true;
    }
    else
    {
      if (flag1)
      {
        if (this.HiddenText(this.GetPrevCfmt(LineNo, col)))
        {
          if (!flag3)
            return true;
          int line = LineNo;
          int col1 = col - 1;
          this.FixPos(ref line, ref col1);
          if (!this.e.TerLocateStyleChar(64 /*0x40*/, false, ref line, ref col1, false))
            return true;
          int curCfmt2 = this.GetCurCfmt(line, col1);
          int nextCfmt = this.GetNextCfmt(line, col1);
          if ((this.e.TerFont[curCfmt2].style & 512 /*0x0200*/) == 0 || (this.e.TerFont[nextCfmt].style & 512 /*0x0200*/) == 0)
            return true;
        }
      }
      else if (this.HiddenText(curCfmt1) && (!flag2 || this.HiddenText(this.GetPrevCfmt(LineNo, col))))
        return true;
      if (flag3 && (this.e.TerFont[this.GetPrevCfmt(LineNo, col)].style & 512 /*0x0200*/) != 0 && (this.e.TerFont[curCfmt1].style & 512 /*0x0200*/) != 0 || flag4 && (this.e.TerFont[curCfmt1].style & 512 /*0x0200*/) != 0)
        return true;
      int fieldId = this.e.TerFont[curCfmt1].FieldId;
      if (fieldId > 0 && fieldId != 6 && fieldId != 7 && fieldId != 9 && fieldId != 14 && fieldId != 2 && this.e.TerFont[this.GetPrevCfmt(LineNo, col)].FieldId > 0 || this.e.ViewPageHdrFtr && this.False(this.e.EditPageHdrFtr) && (this.e.PfmtId[this.e.text[LineNo].pfmt].flags & 12288 /*0x3000*/) != 0 || (this.e.cell[this.e.text[LineNo].cid].flags & 16 /*0x10*/) != 0)
        return true;
    }
    return false;
  }

  internal new void MoveLineArrays(int StartLine, int count, char InsertDel)
  {
    switch (InsertDel)
    {
      case 'A':
        if (this.e.TotalLines == 1 && (this.e.text[0] == null || this.e.text[0].len == 0) && this.e.text[0] == null)
          this.InitLine(0);
        if (!this.CheckLineLimit(this.e.TotalLines + count))
          break;
        if (this.e.TotalLines - StartLine - 1 > 0)
          this.FarMoveOl(this.e.text, StartLine + 1, StartLine + count + 1, this.e.TotalLines - StartLine - 1);
        for (int line = StartLine + 1; line <= StartLine + count; ++line)
          this.InitLine(line);
        this.e.TotalLines += count;
        this.AdjustSections(StartLine, count);
        if (this.e.RepageBeginLine <= StartLine + 1)
          break;
        this.e.RepageBeginLine = StartLine + 1;
        break;
      case 'B':
        if (this.e.TotalLines == 1 && (this.e.text[0] == null || this.e.text[0].len == 0))
        {
          if (this.e.text[0] != null)
            break;
          this.InitLine(0);
          break;
        }
        if (!this.CheckLineLimit(this.e.TotalLines + count))
          break;
        this.FarMoveOl(this.e.text, StartLine, StartLine + count, this.e.TotalLines - StartLine);
        for (int line = StartLine; line < StartLine + count; ++line)
          this.InitLine(line);
        this.e.TotalLines += count;
        this.AdjustSections(StartLine - 1, count);
        if (this.e.RepageBeginLine <= StartLine)
          break;
        this.e.RepageBeginLine = StartLine;
        break;
      default:
        for (int line = StartLine; line < StartLine + count && line < this.e.TotalLines; ++line)
        {
          if (this.True(this.e.text[line]) && (this.e.text[line].flags2 & 2) != 0)
            this.e.TerOpFlags |= 1073741824 /*0x40000000*/;
          this.FreeLine(line);
        }
        if (this.e.TotalLines - StartLine - count > 0)
          this.FarMoveOl(this.e.text, StartLine + count, StartLine, this.e.TotalLines - StartLine - count);
        this.e.TotalLines -= count;
        if (this.e.TotalLines == 0)
        {
          this.e.TotalLines = 1;
          this.InitLine(0);
        }
        this.AdjustSections(StartLine, -count);
        if (this.e.RepageBeginLine <= StartLine - 1)
          break;
        this.e.RepageBeginLine = StartLine - 1;
        break;
    }
  }

  internal new void MoveLineData(int line, int StartPos, int count, char InsertDel)
  {
    int repageBeginLine = this.e.RepageBeginLine;
    int len = this.e.text[line].len;
    bool flag = this.e.text[line].fmt == null;
    switch (InsertDel)
    {
      case 'A':
        this.LineAlloc(line, len, len + count);
        if (StartPos + 1 < len)
        {
          this.FarMoveOl(this.e.text[line].txt, StartPos + 1, StartPos + 1 + count, len - StartPos - 1);
          if (!flag)
          {
            ushort[] fmt = this.e.text[line].fmt;
            this.FarMoveOl(fmt, StartPos + 1, StartPos + 1 + count, len - StartPos - 1);
            ushort num = fmt[StartPos + 1 + count];
            for (int index = 0; index < count; ++index)
              fmt[StartPos + 1 + index] = num;
          }
          if (this.True(this.e.text[line].tag))
          {
            ushort[] tag = this.e.text[line].tag;
            this.FarMoveOl(tag, StartPos + 1, StartPos + 1 + count, len - StartPos - 1);
            for (int index = 0; index < count; ++index)
              tag[StartPos + 1 + index] = (ushort) 0;
          }
          if (this.True(this.e.text[line].cwidth))
          {
            this.FarMoveOl(this.e.text[line].cwidth, StartPos + 1, StartPos + 1 + count, len - StartPos - 1);
            for (int index = 0; index < count; ++index)
              this.SetCharWidth(line, StartPos + 1 + index, 0);
            break;
          }
          break;
        }
        break;
      case 'B':
        this.LineAlloc(line, len, len + count);
        if (StartPos < len)
        {
          this.FarMoveOl(this.e.text[line].txt, StartPos, StartPos + count, len - StartPos);
          if (!flag)
          {
            ushort[] fmt = this.e.text[line].fmt;
            this.FarMoveOl(fmt, StartPos, StartPos + count, len - StartPos);
            ushort num = fmt[StartPos + count];
            for (int index = 0; index < count; ++index)
              fmt[StartPos + index] = num;
          }
          if (this.True(this.e.text[line].tag))
          {
            ushort[] tag = this.e.text[line].tag;
            this.FarMoveOl(tag, StartPos, StartPos + count, len - StartPos);
            for (int index = 0; index < count; ++index)
              tag[StartPos + index] = (ushort) 0;
          }
          if (this.True(this.e.text[line].cwidth))
          {
            this.FarMoveOl(this.e.text[line].cwidth, StartPos, StartPos + count, len - StartPos);
            for (int index = 0; index < count; ++index)
              this.SetCharWidth(line, StartPos + index, 0);
            break;
          }
          break;
        }
        break;
      default:
        if (count <= len)
        {
          char[] txt = this.e.text[line].txt;
          int index1 = StartPos;
          for (int index2 = count + StartPos; index1 < index2; ++index1)
          {
            if (txt[index1] == '\u0014')
            {
              this.e.PosPageHdrFtr = true;
              this.e.SectModified = true;
            }
          }
          if (StartPos + count < len)
          {
            this.FarMoveOl(this.e.text[line].txt, StartPos + count, StartPos, len - StartPos - count);
            if (!flag)
              this.FarMoveOl(this.e.text[line].fmt, StartPos + count, StartPos, len - StartPos - count);
            if (this.True(this.e.text[line].tag))
              this.FarMoveOl(this.e.text[line].tag, StartPos + count, StartPos, len - StartPos - count);
            if (this.True(this.e.text[line].cwidth))
              this.FarMoveOl(this.e.text[line].cwidth, StartPos + count, StartPos, len - StartPos - count);
          }
          this.LineAlloc(line, len, len - count);
          break;
        }
        break;
    }
    if ((this.e.TerOpFlags & 4) != 0)
      this.e.RepageBeginLine = repageBeginLine;
    else if (this.e.RepageBeginLine > line)
      this.e.RepageBeginLine = line;
    tc.ResetLongFlag(ref this.e.text[line].flags2, 1);
  }

  internal new bool NextTextPos()
  {
    int curLine = this.e.CurLine;
    int curCol = this.e.CurCol;
    int num = this.NextTextPos(ref curLine, ref curCol) ? 1 : 0;
    this.e.CurLine = curLine;
    this.e.CurCol = curCol;
    return num != 0;
  }

  internal new bool NextTextPos(ref int pLineNo, ref int pCol)
  {
    int index = pLineNo;
    int num1 = pCol;
    if (index == this.e.TotalLines - 1 && num1 == this.e.text[index].len - 1)
      return false;
    int num2 = num1 + 1;
    if (num2 >= this.e.text[index].len)
    {
      ++index;
      while (index < this.e.TotalLines && this.e.text[index].len == 0)
        ++index;
      if (index >= this.e.TotalLines)
      {
        index = this.e.TotalLines - 1;
        num2 = this.e.text[index].len - 1;
        if (num2 < 0)
          num2 = 0;
      }
      else
        num2 = 0;
    }
    pLineNo = index;
    pCol = num2;
    return true;
  }

  internal new bool PrevTextPos()
  {
    int curLine = this.e.CurLine;
    int curCol = this.e.CurCol;
    int num = this.PrevTextPos(ref curLine, ref curCol) ? 1 : 0;
    this.e.CurLine = curLine;
    this.e.CurCol = curCol;
    return num != 0;
  }

  internal new bool PrevTextPos(ref int pLineNo, ref int pCol)
  {
    int index = pLineNo;
    int num1 = pCol;
    if (index == 0 && num1 == 0)
      return false;
    int num2 = num1 - 1;
    if (num2 < 0)
    {
      --index;
      while (index >= 0 && this.e.text[index].len == 0)
        --index;
      if (index < 0)
      {
        index = 0;
        num2 = 0;
      }
      else
        num2 = this.e.text[index].len - 1;
    }
    pLineNo = index;
    pCol = num2;
    return true;
  }

  internal new bool ReplaceTextInPlace(ref int pLine, ref int pCol, int len, string txt)
  {
    int num = pCol;
    int index1 = pLine;
    int length;
    int count1 = length = txt.Length;
    while (len > 0 || count1 > 0)
    {
      if (count1 == 0)
      {
        while (len > 0)
        {
          int count2 = len;
          if (num + count2 > this.e.text[index1].len)
            count2 = this.e.text[index1].len - num;
          this.MoveLineData(index1, num, count2, 'D');
          len -= count2;
          num += count2;
          if (num >= this.e.text[index1].len)
          {
            ++index1;
            if (index1 < this.e.TotalLines)
              num = 0;
            else
              goto label_20;
          }
        }
      }
      else if (len == 0)
      {
        int prevCfmt = this.GetPrevCfmt(index1, num);
        this.MoveLineData(index1, num, count1, 'B');
        this.SetLineData(index1, num, count1, txt.Substring(length - count1).ToCharArray(), (ushort[]) null, (ushort[]) null);
        ushort[] numArray = this.OpenCfmt(index1);
        for (int index2 = 0; index2 < count1; ++index2)
          numArray[num + index2] = (ushort) prevCfmt;
        this.CloseCfmt(index1);
        num += count1;
        break;
      }
      int count3 = len;
      if (count1 < count3)
        count3 = count1;
      if (this.e.text[index1].len - num < count3)
        count3 = this.e.text[index1].len - num;
      this.SetLineData(index1, num, count3, txt.Substring(length - count1).ToCharArray(), (ushort[]) null, (ushort[]) null);
      count1 -= count3;
      len -= count3;
      num += count3;
      if (num >= this.e.text[index1].len)
      {
        ++index1;
        num = 0;
      }
    }
label_20:
    pLine = index1;
    pCol = num;
    return true;
  }

  internal new bool ScrollText()
  {
    if (this.e.MouseOverShoot == ' ' || this.e.DraggingText && this.e.MouseOverShootDist > this.e.TerFont[0].height * 3 / 2)
      return false;
    do
    {
      if (this.e.MouseOverShoot == 'L')
        this.TerWinLeft();
      else if (this.e.MouseOverShoot == 'R')
        this.TerWinRight();
      else if (this.e.MouseOverShoot == 'T')
      {
        bool flag = this.e.TerWinRect.top - this.e.MouseY >= 2 * this.e.TerFont[0].height;
        if (this.e.TerArg.PageMode)
        {
          if ((this.e.TerFlags4 & 32 /*0x20*/) != 0 && !flag)
            this.TerWinUp();
          else
            this.PgmUp();
        }
        else if (flag)
          this.TerPageUp(false);
        else
          this.TerWinUp();
      }
      else if (this.e.MouseOverShoot == 'B')
      {
        bool flag = this.e.MouseY - this.e.TerWinRect.bottom >= 2 * this.e.TerFont[0].height;
        if (this.e.TerArg.PageMode)
        {
          this.e.CommandId = 603;
          if ((this.e.TerFlags4 & 32 /*0x20*/) != 0 && !flag)
          {
            this.TerWinDown();
            int units = this.ColToUnits(this.e.CurCol, this.e.CurLine, 1);
            int line = this.UnitsToLine(units, this.e.TerWinOrgY + this.e.TerWinHeight);
            if (line != this.e.CurLine)
            {
              this.e.CurLine = line;
              this.e.CurCol = this.UnitsToCol(units, this.e.CurLine);
            }
          }
          else
            this.PgmDown();
        }
        else if (flag)
          this.TerPageDn(false);
        else
          this.TerWinDown();
      }
    }
    while (this.e.DraggingText && !this.PeekMessage(out COp.MSG _, IntPtr.Zero, 512 /*0x0200*/, 522, 2));
    return true;
  }

  internal new void SetLineData(
    int DestLine,
    int DestCol,
    int count,
    char[] ptr,
    ushort[] fmt,
    ushort[] ct)
  {
    char[] txt = this.e.text[DestLine].txt;
    ushort[] dest1 = this.OpenCfmt(DestLine);
    ushort[] dest2 = this.OpenCtid(DestLine);
    if (this.True(ptr))
      this.FarMoveOl(ptr, 0, txt, DestCol, count);
    if (this.True(fmt))
      this.FarMoveOl(fmt, 0, dest1, DestCol, count);
    if (this.True(ct))
      this.FarMoveOl(ct, 0, dest2, DestCol, count);
    this.CloseCharInfo(DestLine);
  }

  internal new bool SetLineText(string str, int line, int col)
  {
    char[] charArray = str.ToCharArray();
    int length = charArray.Length;
    if (col + length > this.e.text[line].len)
      this.LineAlloc(line, this.e.text[line].len, col + length);
    char[] txt = this.e.text[line].txt;
    for (int index = 0; index < length; ++index)
      txt[col + index] = charArray[index];
    return true;
  }

  internal new bool SplitLine(int line, int col, int extra)
  {
    ushort[] fmt = new ushort[1];
    ushort[] ct = new ushort[1];
    char[] ptr = new char[1];
    this.MoveLineArrays(line, 1, 'A');
    if (col >= this.e.text[line].len)
      return false;
    this.LineAlloc(line + 1, this.e.text[line + 1].len, extra + this.e.text[line].len - col);
    fmt[0] = col >= this.e.text[line].len ? (ushort) 0 : (ushort) this.GetCurCfmt(line, col);
    if ((this.e.TerFont[(int) fmt[0]].style & 128 /*0x80*/) != 0)
      fmt[0] = (ushort) 0;
    ct[0] = (ushort) 0;
    ptr[0] = ' ';
    for (int DestCol = 0; DestCol < extra; ++DestCol)
      this.SetLineData(line + 1, DestCol, 1, ptr, fmt, ct);
    this.MoveCharInfo(line, col, line + 1, extra, this.e.text[line].len - col);
    this.LineAlloc(line, this.e.text[line].len, col);
    if (this.True(this.e.text[line].tabw))
    {
      this.CopyTabw(line, line + 1);
      this.e.text[line].tabw.type &= 129;
    }
    if (this.LineInfo(line, 2))
    {
      this.e.text[line + 1].tabw.type |= 2;
      this.e.text[line + 1].tabw.section = this.e.text[line].tabw.section;
      tc.ResetUintFlag(ref this.e.text[line].tabw.type, 2);
    }
    return true;
  }

  internal new bool TerAscii(char AscCode)
  {
    int num1 = 0;
    bool flag1 = false;
    bool flag2 = false;
    if (this.e.IsProtectedZone(this.pos.TerRowColToAbs(this.e.CurLine, this.e.CurCol), false))
      return false;
    if (!tc.expired && !this.lstrchr(this.e.BreakChars, AscCode))
    {
      if (Control.ModifierKeys == (Keys.Shift | Keys.Control) && AscCode == ' ')
        AscCode = '\u000E';
      if (AscCode == '\u001B')
      {
        this.MessageBeep(0);
        return false;
      }
      this.AdjustInputPos(AscCode);
      if (this.e.TerArg.PageMode && !this.e.EditPageHdrFtr && (this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].flags & 12288 /*0x3000*/) != 0 || (this.e.text[this.e.CurLine].flags & 1966080 /*0x1E0000*/) != 0)
      {
        this.MessageBeep(0);
        return false;
      }
      bool flag3 = this.e.InsertMode;
      if (!flag3 && ((int) AscCode == (int) this.e.ParaChar || AscCode == '\u000F' || AscCode == '\f' || AscCode == '\t'))
        flag3 = true;
      if (!flag3 && this.e.text[this.e.CurLine].len > 0)
      {
        int uniFmt;
        if (this.e.text[this.e.CurLine].fmt == null)
        {
          uniFmt = (int) this.e.text[this.e.CurLine].UniFmt;
        }
        else
        {
          ushort[] numArray = this.OpenCfmt(this.e.CurLine);
          uniFmt = this.e.CurCol >= this.e.text[this.e.CurLine].len ? 0 : (int) numArray[this.e.CurCol];
          this.CloseCfmt(this.e.CurLine);
        }
        if ((this.e.TerFont[uniFmt].style & 512 /*0x0200*/) != 0)
        {
          this.MessageBeep(0);
          return false;
        }
        char[] txt = this.e.text[this.e.CurLine].txt;
        if (this.e.CurCol < this.e.text[this.e.CurLine].len && ((int) txt[this.e.CurCol] == (int) this.e.ParaChar || txt[this.e.CurCol] == '\u0013'))
          flag3 = true;
      }
      int index1;
      if (this.e.InputFontId >= 0)
      {
        index1 = this.e.InputFontId;
      }
      else
      {
        int hilightType = this.e.HilightType;
        this.e.HilightType = 0;
        index1 = this.GetEffectiveCfmt();
        this.e.HilightType = hilightType;
      }
      if (flag3 && this.e.TerFont[index1].FieldId == 2 && !this.CanInsertInInputField(index1, this.e.CurLine, this.e.CurCol))
      {
        this.MessageBeep(0);
        return false;
      }
      bool flag4 = (this.e.TerFlags5 & 536870912 /*0x20000000*/) != 0 && (this.e.TerFont[index1].FieldId == 6 || this.e.TerFont[index1].FieldId == 7);
      if ((((this.e.TerFont[index1].style & 512 /*0x0200*/) != 0 ? 1 : (!this.e.InFootnote ? 0 : ((this.e.TerFont[index1].style & 2048 /*0x0800*/) == 0 ? 1 : 0))) | (flag4 ? 1 : 0)) != 0)
      {
        index1 = this.GetCurCfmt(this.e.CurLine, this.e.CurCol);
        if (flag4 || (this.e.TerFont[index1].style & 5632) != 0)
        {
          this.MessageBeep(0);
          return false;
        }
      }
      if (this.e.text[this.e.CurLine].fid > 0 && (this.e.ParaFrame[this.e.text[this.e.CurLine].fid].flags & 768 /*0x0300*/) != 0)
      {
        this.MessageBeep(0);
        return false;
      }
      if ((this.e.text[this.e.CurLine].flags & 2) != 0 && (this.e.text[this.e.CurLine].flags & 2049) == 0 && (int) AscCode != (int) this.e.ParaChar || (this.e.text[this.e.CurLine].flags & 256 /*0x0100*/) != 0)
      {
        this.MessageBeep(0);
        return false;
      }
      if ((this.e.TerFont[index1].style & 128 /*0x80*/) != 0)
        index1 = 0;
      if (this.e.ImeEnabled && (this.e.TerFont[index1].TempStyle & 1) == 0)
        index1 = (int) this.GetNewTempStyle((ushort) index1, 1, 1, this.e.CurLine, this.e.CurCol);
      int TextAngle = this.e.AllTextAngle <= 0 ? (this.True(this.e.text[this.e.CurLine].fid) ? this.e.ParaFrame[this.e.text[this.e.CurLine].fid].TextAngle : 0) : this.e.AllTextAngle;
      if (TextAngle == 0)
        TextAngle = this.True(this.e.text[this.e.CurLine].cid) ? this.e.cell[this.e.text[this.e.CurLine].cid].TextAngle : 0;
      if (this.e.TerFont[index1].TextAngle != TextAngle)
        index1 = this.SetFontTextAngle(index1, TextAngle);
      int CurFont = this.SetCurLangFont(index1);
      if (this.e.TrackChanges)
        CurFont = (int) this.SetTrackingFont(CurFont, 1);
      else if (this.True(this.e.TerFont[CurFont].InsRev) || this.True(this.e.TerFont[CurFont].DelRev))
        CurFont = (int) this.SetTrackingFont(CurFont, 0);
      if ((int) AscCode != (int) this.e.ParaChar && AscCode != '\u000F')
        this.e.TerOpFlags |= 4;
      if ((this.e.TerFlags3 & 16384 /*0x4000*/) != 0)
        this.e.TerOpFlags |= 32768 /*0x8000*/;
      if (this.e.HilightType == 2)
      {
        int terFlags = this.e.TerFlags;
        if (this.IsProtected(true, true))
          return false;
        this.e.TerFlags |= 1073741824 /*0x40000000*/;
        this.e.TerOpFlags |= 8388608 /*0x800000*/;
        this.e.TerOpFlags2 |= 2048 /*0x0800*/;
        this.e.TerDeleteBlock(false);
        this.e.TerFlags = terFlags;
        this.e.TerOpFlags &= -8388609;
        this.e.TerOpFlags2 &= -2049;
        this.e.CurLine = this.e.HilightBegRow;
        this.e.CurCol = this.e.HilightBegCol;
        this.e.HilightType = 0;
        this.e.PaintFlag = 4;
        flag1 = true;
      }
      if (!flag3 && this.IsProtectedChar(this.e.CurLine, this.e.CurCol))
        return false;
      int len1 = this.e.text[this.e.CurLine].len;
      char[] txt1;
      ushort[] numArray1;
      if (flag3 && this.e.CurCol < this.e.text[this.e.CurLine].len)
      {
        this.MoveLineData(this.e.CurLine, this.e.CurCol, 1, 'B');
        txt1 = this.e.text[this.e.CurLine].txt;
        numArray1 = this.OpenCfmt(this.e.CurLine);
      }
      else
      {
        int len2 = this.e.text[this.e.CurLine].len;
        if (this.IsProtectedChar(this.e.CurLine, this.e.CurCol))
        {
          this.MessageBeep(0);
          return false;
        }
        if (this.e.CurCol + 1 > this.e.text[this.e.CurLine].len)
          this.LineAlloc(this.e.CurLine, this.e.text[this.e.CurLine].len, this.e.CurCol + 1);
        txt1 = this.e.text[this.e.CurLine].txt;
        numArray1 = this.OpenCfmt(this.e.CurLine);
        if (this.e.text[this.e.CurLine].len > len2)
        {
          ushort num2 = len2 != 0 ? numArray1[len2 - 1] : (ushort) 0;
          for (int index2 = len2; index2 < this.e.text[this.e.CurLine].len; ++index2)
            numArray1[index2] = num2;
        }
      }
      for (int index3 = 0; index3 < this.e.text[this.e.CurLine].len; ++index3)
      {
        if (this.e.TerFont[(int) numArray1[index3]].height > num1)
          num1 = this.e.TerFont[(int) numArray1[index3]].height;
      }
      for (int index4 = len1; index4 <= this.e.CurCol; ++index4)
        txt1[index4] = ' ';
      int index5 = 0;
      if (len1 > 0)
        index5 = (int) numArray1[len1 - 1];
      if ((this.e.TerFont[index5].style & 640) != 0)
        index5 = 0;
      for (int index6 = len1; index6 <= this.e.CurCol; ++index6)
        numArray1[index6] = (ushort) index5;
      if ((int) AscCode == (int) this.e.ParaChar || AscCode == '\u000F')
        this.e.TerOpFlags |= 1048576 /*0x100000*/;
      if (!this.e.ImeEnabled || !this.e.InsertMode)
      {
        this.SaveUndo(this.e.CurLine, this.e.CurCol, this.e.CurLine, this.e.CurCol, flag3 ? 'I' : 'R');
        numArray1 = this.OpenCfmt(this.e.CurLine);
      }
      txt1[this.e.CurCol] = AscCode;
      numArray1[this.e.CurCol] = (ushort) CurFont;
      if ((this.e.text[this.e.CurLine].flags2 & 32 /*0x20*/) == 0 && this.e.TerFont[CurFont].rtl)
        flag2 = true;
      this.CloseCfmt(this.e.CurLine);
      bool flag5 = this.e.CurLine > 0 && this.e.text[this.e.CurLine].len == 2 && this.True(this.e.text[this.e.CurLine].cid) && this.LineInfo(this.e.CurLine, 16 /*0x10*/) && this.TableLevel(this.e.CurLine - 1) > this.TableLevel(this.e.CurLine);
      if ((int) AscCode != (int) this.e.ParaChar && AscCode != '\u000F' && AscCode != '\t' && AscCode != '\u000E' && AscCode != '\u0017' && !flag1 && this.e.InputFontId == -1 && this.e.TerFont[CurFont].height <= num1 && !flag5 && !flag2 && (this.e.text[this.e.CurLine].flags2 & 32 /*0x20*/) == 0 && AscCode < 'Ā' && (this.e.TerFont[CurFont].style & 165888) == 0)
        this.e.WrapFlag = 1;
      if (this.e.CurCol < this.e.LineWidth - 1 || this.e.CurCol + 1 < this.e.text[this.e.CurLine].len)
        ++this.e.CurCol;
      if (this.e.CurCol >= this.e.text[this.e.CurLine].len && this.e.TerArg.WordWrap && this.e.CurLine + 1 < this.e.TotalLines)
      {
        ++this.e.CurLine;
        ++this.e.CurRow;
        this.e.CurCol = 0;
        this.e.PaintFlag = 4;
        this.e.WrapFlag = 3;
      }
      this.e.FrameClicked = false;
      tc.ResetLongFlag(ref this.e.text[this.e.CurLine].flags2, 1);
      this.e.EditLine = this.e.CurLine;
      this.e.EditCol = this.e.CurCol;
      if ((this.e.text[this.e.CurLine].flags & 16384 /*0x4000*/) != 0)
        this.e.TerOpFlags2 |= 4;
      this.PaintTer();
      ++this.e.TerArg.modified;
      this.e.InputFontId = -1;
      this.e.EnterHit = false;
      this.e.TerOpFlags &= -1081413;
      if (this.e.EditEndnoteText && (this.e.TerFont[CurFont].style & 32768 /*0x8000*/) != 0)
        this.e.TerOpFlags |= 1073741824 /*0x40000000*/;
      if ((this.e.TerOpFlags & 1073741824 /*0x40000000*/) != 0 && this.e.CurLine < this.e.RepageBeginLine)
        this.e.RepageBeginLine = this.e.CurLine;
      if (this.e.TotalAutoComps > 0)
        this.DoAutoComp(AscCode);
      if ((int) AscCode == (int) this.e.ParaChar)
        this.SetNextStyle();
    }
    return true;
  }

  internal new bool TerBackSpace()
  {
    if (this.e.HilightType != 0)
    {
      int terFlags = this.e.TerFlags;
      this.e.TerFlags |= 1073741824 /*0x40000000*/;
      this.e.TerOpFlags2 |= 2048 /*0x0800*/;
      this.e.TerDeleteBlock(true);
      this.e.TerFlags = terFlags;
      this.e.TerOpFlags2 &= -2049;
      return true;
    }
    int curLine = this.e.CurLine;
    int curCol = this.e.CurCol;
    int curCfmt1;
    while (true)
    {
      if (this.e.CurCol == 0)
      {
        if (this.e.CurLine != 0 && (!this.True(this.e.text[this.e.CurLine].cid) || !this.False(this.e.text[this.e.CurLine - 1].cid)))
        {
          if (this.e.TerArg.WordWrap)
          {
            --this.e.CurLine;
            this.e.CurCol = this.e.text[this.e.CurLine].len;
          }
          else
            break;
        }
        else
          goto label_9;
      }
      if (this.e.CurCol <= this.e.text[this.e.CurLine].len)
      {
        if (this.e.text[this.e.CurLine].len > 0)
        {
          if (this.IsProtectedChar(this.e.CurLine, this.e.CurCol - 1))
          {
            curCfmt1 = this.GetCurCfmt(this.e.CurLine, this.e.CurCol - 1);
            if ((this.e.TerFont[curCfmt1].style & 128 /*0x80*/) != 0 && this.e.TerFont[curCfmt1].ParaFID != 0 && !this.e.ShowParaMark)
              --this.e.CurCol;
            else
              goto label_16;
          }
          else
            goto label_19;
        }
        else
          goto label_29;
      }
      else
        goto label_11;
    }
    if (this.e.JoinLines)
    {
      --this.e.CurLine;
      --this.e.CurRow;
      this.e.CurCol = this.e.text[this.e.CurLine].len;
      this.TerJoinLine();
      return true;
    }
label_9:
    return true;
label_11:
    --this.e.CurCol;
    this.e.PaintFlag = 1;
    this.PaintTer();
    return true;
label_16:
    int textTag = this.e.GetTextTag(this.e.CurLine, this.e.CurCol - 1, (IList<int>) tc.ReplacedCharTags);
    bool forceDel = textTag != -1 && (this.e.CharTag[textTag].AuxText == null || this.e.CharTag[textTag].AuxText == "");
    if (forceDel || this.e.TerFont[curCfmt1].FieldId == 6 && this.HiddenText(curCfmt1))
    {
      --this.e.CurCol;
      this.e.CursDirection = 2;
      this.AdjustHiddenPos();
      this.e.PrevCursLine = this.e.CurLine;
      this.e.PrevCursCol = this.e.CurCol;
      this.e.CursDirection = 2;
      return this.TerDel(forceDel);
    }
    this.MessageBeep(0);
    this.e.CurLine = curLine;
    this.e.CurCol = curCol;
    return false;
label_19:
    if (this.IsLoneHypertextChar(this.e.CurLine, this.e.CurCol - 1))
      return this.e.TerDeleteHypertext(this.e.CurLine, this.e.CurCol - 1, true);
    int curCfmt2 = this.GetCurCfmt(this.e.CurLine, this.e.CurCol - 1);
    if (this.HiddenText(curCfmt2))
    {
      --this.e.CurCol;
      this.e.CursDirection = 2;
      this.AdjustHiddenPos();
      this.e.PrevCursLine = this.e.CurLine;
      this.e.PrevCursCol = this.e.CurCol;
      this.e.CursDirection = 1;
      return this.TerDel();
    }
    if (!this.e.TrackChanges || !this.TrackDel(this.e.CurLine, this.e.CurCol - 1, false))
    {
      this.TransferTags(this.e.CurLine, this.e.CurCol - 1);
      this.e.InputFontId = curCfmt2;
      int curChar = (int) this.GetCurChar(this.e.CurLine, this.e.CurCol - 1);
      this.SaveUndo(this.e.CurLine, this.e.CurCol - 1, this.e.CurLine, this.e.CurCol - 1, 'D');
      this.MoveLineData(this.e.CurLine, this.e.CurCol - 1, 1, 'D');
      int paraChar = (int) this.e.ParaChar;
      if (curChar == paraChar && this.e.text[this.e.CurLine].len > 0 && this.e.CurCol > this.e.text[this.e.CurLine].len)
      {
        for (int index = this.e.CurLine + 1; index < this.e.TotalLines; ++index)
        {
          this.e.text[index].pfmt = this.e.text[this.e.CurLine].pfmt;
          if ((this.e.text[index].flags & 3) != 0)
            break;
        }
      }
    }
label_29:
    --this.e.CurCol;
    if ((this.e.text[this.e.CurLine].flags & 1) != 0 && this.e.CurCol < this.e.text[this.e.CurLine].len - 1 && !this.CursorOnFirstWord())
      this.e.WrapFlag = 1;
    else
      this.e.WrapFlag = 2;
    if ((this.e.text[this.e.CurLine].flags & 65536 /*0x010000*/) != 0 || (this.e.text[this.e.CurLine].flags2 & 2) != 0)
    {
      this.e.WrapFlag = 2;
      this.e.TerOpFlags |= 1073741824 /*0x40000000*/;
    }
    if (this.e.WrapFlag == 1)
      this.e.PaintFlag = 2;
    else
      this.e.PaintFlag = 4;
    this.e.FrameClicked = false;
    this.PaintTer();
    return true;
  }

  internal new bool TerBackTab()
  {
    int curCfmt = this.GetCurCfmt(this.e.CurLine, this.e.CurCol);
    if (this.e.TerArg.PrintView && this.e.text[this.e.CurLine].cid > 0 && this.e.TerFont[curCfmt].FieldId != 2)
      return this.TerBackTabCell();
    if (this.e.text[this.e.CurLine].len != 0)
    {
      if (this.e.CurCol == 0 && (this.e.text[this.e.CurLine].flags & 4) != 0)
      {
        int bltId = this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].BltId;
        if ((this.e.TerFlags6 & 32 /*0x20*/) == 0 && bltId != 0 && this.e.TerBlt[bltId].ls != 0 && this.e.TerBlt[bltId].lvl > 0)
          this.e.TerSetListLevel(-1, -1, true);
        return true;
      }
      if (this.e.TerFont[curCfmt].FieldId == 2)
        return this.TabOnControl(true);
      if (this.e.CurCol >= this.e.text[this.e.CurLine].len)
        this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
      char[] txt = this.e.text[this.e.CurLine].txt;
      if (this.e.CurCol > 0 && txt[this.e.CurCol] == '\t')
        --this.e.CurCol;
      while (this.e.CurCol > 0 && txt[this.e.CurCol] != '\t')
        --this.e.CurCol;
      this.e.PaintFlag = 1;
      this.PaintTer();
    }
    return true;
  }

  internal new bool TerBeginFile()
  {
    this.TerSetCharHilight();
    this.e.CurCol = 0;
    if (this.e.TerArg.PageMode)
    {
      if (this.e.EditPageHdrFtr)
      {
        this.e.CurPage = 0;
        this.RefreshFrames(true);
        int num1;
        int num2 = num1 = -1;
        int index;
        for (index = 0; index < this.e.TotalLines && (this.e.PfmtId[this.e.text[index].pfmt].flags & 12288 /*0x3000*/) != 0; ++index)
        {
          if ((this.e.text[index].flags & 524288 /*0x080000*/) != 0 && num2 == -1)
            num2 = index;
          if ((this.e.text[index].flags & 131072 /*0x020000*/) != 0 && num1 == -1)
            num1 = index;
        }
        if (num1 >= 0)
          this.TerPosLine(num1 + 2);
        else if (num2 >= 0)
          this.TerPosLine(num2 + 2);
        else
          this.TerPosLine(index + 1);
      }
      else
      {
        int index = 0;
        while (index < this.e.TotalLines && (this.e.PfmtId[this.e.text[index].pfmt].flags & 12288 /*0x3000*/) != 0)
          ++index;
        if (index == this.e.TotalLines)
          ++index;
        this.TerPosLine(index + 1);
      }
    }
    else
      this.TerPosLine(1);
    return true;
  }

  internal new bool TerBeginLine()
  {
    this.TerSetCharHilight();
    this.e.CurCol = 0;
    this.e.PrevCursCol = 0;
    this.e.PaintFlag = 1;
    this.PaintTer();
    return true;
  }

  internal new bool TerCtrlDown()
  {
    if (this.e.CurCol > 0 && this.e.CurLine < this.e.TotalLines - 1)
      this.e.CurCol = 0;
    return this.e.CurCol == 0 && this.e.CurLine < this.e.TotalLines - 1 ? this.TerDown() : this.TerEndLine();
  }

  internal new bool TerCtrlUp()
  {
    if (this.e.CurCol == 0)
      return this.TerUp();
    this.TerSetCharHilight();
    this.e.CurCol = 0;
    this.e.PaintFlag = 4;
    this.PaintTer();
    return true;
  }

  internal new bool TerDel() => this.TerDel(false);

  internal bool TerDel(bool forceDel)
  {
    bool flag = false;
    if (this.e.HilightType != 0)
    {
      int terFlags = this.e.TerFlags;
      this.e.TerFlags |= 1073741824 /*0x40000000*/;
      this.e.TerOpFlags2 |= 2048 /*0x0800*/;
      this.e.blk.TerDeleteBlock(true, forceDel);
      this.e.TerOpFlags2 &= -2049;
      this.e.TerFlags = terFlags;
      return true;
    }
    if (this.e.FrameClicked && !this.e.RotatedFrame)
      return this.DeleteFrame();
    if (this.e.CurCol >= this.e.text[this.e.CurLine].len && !this.e.TerArg.WordWrap)
    {
      if (!this.e.JoinLines)
        return true;
      this.TerJoinLine();
      return true;
    }
    if (!this.e.TerArg.WordWrap || this.e.CurCol + 1 <= this.e.text[this.e.CurLine].len)
    {
      if (this.e.HtmlMode && !this.e.ShowHiddenText)
        this.PosAfterHiddenText();
      if (!forceDel && this.IsProtectedChar(this.e.CurLine, this.e.CurCol))
      {
        this.MessageBeep(0);
        return true;
      }
      if (this.e.TerArg.WordWrap && this.e.CurLine == this.e.TotalLines - 1 && this.e.CurCol == this.e.text[this.e.CurLine].len - 1)
        return true;
      if (!this.e.TrackChanges || !this.TrackDel(this.e.CurLine, this.e.CurCol, true))
      {
        if (this.IsLoneHypertextChar(this.e.CurLine, this.e.CurCol))
          return this.e.TerDeleteHypertext(this.e.CurLine, this.e.CurCol, true);
        if (this.e.text[this.e.CurLine].len > 0)
        {
          int curChar = (int) this.GetCurChar(this.e.CurLine, this.e.CurCol);
          if ((this.e.text[this.e.CurLine].flags & 65536 /*0x010000*/) != 0 || (this.e.text[this.e.CurLine].flags2 & 2) != 0)
          {
            if ((this.e.TerFont[this.GetCurCfmt(this.e.CurLine, this.e.CurCol)].style & 34816) != 0)
              flag = true;
            this.e.TerOpFlags |= 1073741824 /*0x40000000*/;
          }
          this.TransferTags(this.e.CurLine, this.e.CurCol);
          this.SaveUndo(this.e.CurLine, this.e.CurCol, this.e.CurLine, this.e.CurCol, 'D');
          this.MoveLineData(this.e.CurLine, this.e.CurCol, 1, 'D');
          int paraChar = (int) this.e.ParaChar;
          if (curChar == paraChar && this.e.text[this.e.CurLine].len > 0 && this.e.CurCol == this.e.text[this.e.CurLine].len)
          {
            for (int index = this.e.CurLine + 1; index < this.e.TotalLines; ++index)
            {
              this.e.text[index].pfmt = this.e.text[this.e.CurLine].pfmt;
              if ((this.e.text[index].flags & 3) != 0)
                break;
            }
          }
        }
      }
      if ((this.e.text[this.e.CurLine].flags & 1) != 0 && this.e.CurCol < this.e.text[this.e.CurLine].len - 1 && !this.CursorOnFirstWord() && !flag)
        this.e.WrapFlag = 1;
      else
        this.e.WrapFlag = 2;
      if (this.e.WrapFlag == 1)
        this.e.PaintFlag = 2;
      else
        this.e.PaintFlag = 4;
      this.e.FrameClicked = false;
      this.PaintTer();
    }
    return true;
  }

  internal new bool TerDeleteLine()
  {
    if (this.e.TerArg.WordWrap && this.e.TotalLines == 1)
    {
      if (this.e.text[this.e.CurLine].len <= 1)
        return true;
      this.e.HilightBegRow = this.e.HilightEndRow = this.e.CurLine;
      this.e.HilightBegCol = 0;
      this.e.HilightEndCol = this.e.text[this.e.CurLine].len - 1;
      this.e.HilightType = 2;
      return this.DeleteCharBlock(true, true);
    }
    ++this.e.TerArg.modified;
    this.SaveUndo(this.e.CurLine, 0, this.e.CurLine, this.e.text[this.e.CurLine].len, 'D');
    this.MoveLineArrays(this.e.CurLine, 1, 'D');
    if (this.e.CurLine == this.e.TotalLines && this.e.CurLine > 0)
    {
      --this.e.CurLine;
      --this.e.CurRow;
      this.e.CurCol = 0;
    }
    if (this.e.CurLine >= this.e.HilightBegRow && this.e.CurLine <= this.e.HilightEndRow)
      this.e.HilightType = 0;
    if (this.e.CurLine >= this.e.HilightEndRow && this.e.CurLine <= this.e.HilightBegRow)
      this.e.HilightType = 0;
    if (this.e.CurLine <= this.e.HilightBegRow)
      --this.e.HilightBegRow;
    if (this.e.CurLine <= this.e.HilightEndRow)
      --this.e.HilightEndRow;
    if (this.e.HilightBegRow < 0 || this.e.HilightEndRow < 0)
      this.e.HilightType = 0;
    if (this.e.CurRow < 0)
    {
      this.e.CurRow = 0;
      this.e.BeginLine = this.e.CurLine;
    }
    if (this.e.TerArg.WordWrap && this.e.CurCol >= this.e.text[this.e.CurLine].len)
      this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
    if (this.e.CurCol < 0)
      this.e.CurCol = 0;
    this.e.PaintFlag = 5;
    this.PaintTer();
    return true;
  }

  internal new bool TerDelPrevWord()
  {
    int curLine = this.e.CurLine;
    int curCol = this.e.CurCol;
    if (this.e.CurCol == 0 && this.e.CurLine > 0 && this.LineInfo(this.e.CurLine - 1, 48 /*0x30*/) || !this.TerPrevWord(false))
      return false;
    this.e.HilightBegRow = this.e.CurLine;
    this.e.HilightBegCol = this.e.CurCol;
    this.e.HilightEndRow = curLine;
    this.e.HilightEndCol = curCol;
    this.e.HilightType = 2;
    this.e.TerDeleteBlock(true);
    return true;
  }

  internal new bool TerDown()
  {
    this.TerSetCharHilight();
    if (this.e.TerArg.PageMode)
      return this.PgmDown();
    if (this.e.CurLine + 1 >= this.e.TotalLines)
    {
      if (this.e.HilightType == 2 && this.e.StretchHilight && this.e.CurCol + 1 < this.e.text[this.e.CurLine].len)
      {
        this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
        if (this.e.CurCol < 0)
          this.e.CurCol = 0;
        this.e.WrapFlag = 0;
        this.e.PaintFlag = 1;
        this.PaintTer();
      }
      return true;
    }
    if (this.e.CursHorzPos < 0)
      this.e.CursHorzPos = this.ColToUnits(this.e.CurCol, this.e.CurLine, 1);
    if (this.e.CurLine < this.e.frame[0].ScrLastLine)
    {
      ++this.e.CurRow;
      ++this.e.CurLine;
      this.e.CurCol = this.UnitsToCol(this.e.CursHorzPos, this.e.CurLine);
      this.e.WrapFlag = 0;
      if (this.e.TerArg.WordWrap)
      {
        if (this.e.CurCol >= this.e.text[this.e.CurLine].len)
          this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
        if (this.e.CurCol < 0)
          this.e.CurCol = 0;
      }
      this.e.PaintFlag = 1;
      this.PaintTer();
      return true;
    }
    ++this.e.CurLine;
    this.e.CurCol = this.UnitsToCol(this.e.CursHorzPos, this.e.CurLine);
    if (this.e.TerArg.WordWrap)
    {
      this.e.CurRow = this.e.WinHeight - 1;
      ++this.e.BeginLine;
    }
    else
    {
      this.e.CurRow = this.e.WinHeight;
      this.e.BeginLine = this.e.CurLine - this.e.CurRow;
    }
    if (this.e.BeginLine < 0)
    {
      this.e.BeginLine = 0;
      this.e.CurRow = this.e.CurLine - this.e.BeginLine;
    }
    if (this.e.TerArg.WordWrap)
    {
      if (this.e.CurCol >= this.e.text[this.e.CurLine].len)
        this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
      if (this.e.CurCol < 0)
        this.e.CurCol = 0;
    }
    this.e.UseTextMap = false;
    this.e.PaintFlag = 4;
    this.PaintTer();
    return true;
  }

  internal new bool TerEndFile()
  {
    this.TerSetCharHilight();
    this.e.CurCol = this.e.text[this.e.TotalLines - 1].len;
    if (this.e.TerArg.WordWrap)
      --this.e.CurCol;
    if (this.e.CurCol < 0)
      this.e.CurCol = 0;
    if (this.e.TerArg.PageMode && this.e.ViewPageHdrFtr)
    {
      this.e.CurPage = this.e.TotalPages - 1;
      if (this.e.CurPage < 0)
        this.e.CurPage = 0;
      this.RefreshFrames(true);
    }
    this.e.HilightAtCurPos = true;
    if (this.e.TerArg.PageMode)
    {
      this.TerPosLine(this.e.TotalLines);
    }
    else
    {
      this.e.BeginLine = this.LastScrollBeginLine();
      this.e.CurLine = this.e.TotalLines - 1;
      this.e.CurRow = this.e.CurLine - this.e.BeginLine;
      this.PaintTer();
    }
    return true;
  }

  internal new bool TerEndLine()
  {
    this.TerSetCharHilight();
    if (this.e.text[this.e.CurLine].len != 0)
    {
      this.e.CurCol = this.e.text[this.e.CurLine].len;
      if (this.e.TerArg.WordWrap && this.e.CurCol > 0)
        --this.e.CurCol;
      this.e.PaintFlag = 1;
      this.PaintTer();
    }
    return true;
  }

  internal new bool TerInsertTab()
  {
    int inputFontId = this.e.InputFontId;
    this.TerSetCharHilight();
    this.e.InputFontId = inputFontId;
    if (this.e.TerArg.WordWrap)
    {
      if (this.e.CurCol == 0 && (this.e.text[this.e.CurLine].flags & 4) != 0)
      {
        int bltId = this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].BltId;
        if ((this.e.TerFlags6 & 32 /*0x20*/) == 0 && bltId != 0 && this.e.TerBlt[bltId].ls != 0 && this.e.TerBlt[bltId].lvl < 8)
        {
          this.e.TerSetListLevel(-1, 1, true);
          return true;
        }
      }
      int curCfmt = this.GetCurCfmt(this.e.CurLine, this.e.CurCol);
      if (this.e.TerArg.ReadOnly && this.e.TerFont[curCfmt].FieldId != 2 && this.e.TerFont[this.GetPrevCfmt(this.e.CurLine, this.e.CurCol)].FieldId == 2)
      {
        this.PrevTextPos();
        curCfmt = this.GetCurCfmt(this.e.CurLine, this.e.CurCol);
      }
      if (this.e.TerArg.ReadOnly && this.e.TerFont[curCfmt].FieldId == 2)
        return this.TabOnControl(false);
      if (this.e.TerArg.PrintView && this.e.text[this.e.CurLine].cid > 0 && this.e.CommandId != 614)
        return this.TerTabCell();
      if (this.e.TerArg.ReadOnly)
        return true;
      if (this.e.HtmlMode)
        return this.e.InsertTerText("    ", true);
      if (this.False(this.e.text[this.e.CurLine].tabw))
        this.AllocTabw(this.e.CurLine);
      if ((this.e.text[this.e.CurLine].tabw.type & 1) == 0)
      {
        this.e.text[this.e.CurLine].tabw.type |= 1;
        this.e.text[this.e.CurLine].tabw.count = 0;
      }
    }
    return this.e.TerArg.ReadOnly || this.TerAscii('\t');
  }

  internal new bool TerJoinLine()
  {
    int index1 = 0;
    if (this.e.TotalLines != this.e.CurLine + 1)
    {
      ++this.e.TerArg.modified;
      char[] txt1 = this.e.text[this.e.CurLine + 1].txt;
      while (index1 < this.e.text[this.e.CurLine + 1].len && txt1[index1] == ' ')
        ++index1;
      int len = this.e.text[this.e.CurLine].len;
      this.LineAlloc(this.e.CurLine, this.e.text[this.e.CurLine].len, this.e.text[this.e.CurLine].len + this.e.text[this.e.CurLine + 1].len - index1);
      char[] txt2 = this.e.text[this.e.CurLine].txt;
      char[] txt3 = this.e.text[this.e.CurLine + 1].txt;
      ushort[] fmt1;
      ushort[] cmi1;
      this.OpenCharInfo(this.e.CurLine, out fmt1, out cmi1);
      ushort[] fmt2;
      ushort[] cmi2;
      this.OpenCharInfo(this.e.CurLine + 1, out fmt2, out cmi2);
      for (int index2 = 0; index2 < this.e.text[this.e.CurLine + 1].len - index1; ++index2)
      {
        txt2[len + index2] = txt3[index1 + index2];
        fmt1[len + index2] = fmt2[index1 + index2];
        cmi1[len + index2] = cmi2[index1 + index2];
      }
      this.CloseCharInfo(this.e.CurLine);
      this.CloseCharInfo(this.e.CurLine + 1);
      int curLine = this.e.CurLine;
      int curRow = this.e.CurRow;
      int curCol = this.e.CurCol;
      ++this.e.CurLine;
      ++this.e.CurRow;
      if (this.e.CurRow >= this.e.WinHeight)
        this.e.CurRow = this.e.WinHeight - 1;
      this.e.BeginLine = this.e.CurLine - this.e.CurRow;
      this.TerDeleteLine();
      this.e.CurLine = curLine;
      this.e.CurRow = curRow;
      this.e.CurCol = curCol;
      this.e.BeginLine = this.e.CurLine - this.e.CurRow;
      this.PaintTer();
    }
    return true;
  }

  internal new bool TerLeft()
  {
    int hilightType = this.e.HilightType;
    this.TerSetCharHilight();
    if (hilightType == 0 || this.e.HilightType != 0)
    {
      if (this.e.TerArg.PageMode)
        return this.PgmLeft();
      if (this.e.CurCol > 0)
      {
        --this.e.CurCol;
        this.e.WrapFlag = 0;
        this.e.PaintFlag = 1;
        this.PaintTer();
        return true;
      }
      if (this.e.CurLine != 0)
      {
        if (this.e.CurRow > 0)
        {
          --this.e.CurRow;
          --this.e.CurLine;
          this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
          this.e.PaintFlag = 1;
          this.e.WrapFlag = 1;
          this.PaintTer();
          return true;
        }
        --this.e.CurLine;
        this.e.CurRow = 0;
        if (this.e.CurLine < this.e.CurRow)
          this.e.CurRow = this.e.CurLine;
        this.e.BeginLine = this.e.CurLine - this.e.CurRow;
        this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
        this.e.PaintFlag = 4;
        this.PaintTer();
        return true;
      }
    }
    return true;
  }

  internal bool TerLineSelected(int LineNo)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    return this.LineSelected(LineNo);
  }

  internal new bool TerNextWord()
  {
    bool flag = false;
    this.TerSetCharHilight();
    this.e.CursDirection = 1;
    int num;
    int index1 = num = 0;
    while (true)
    {
      char[] txt = this.e.text[this.e.CurLine].txt;
      ushort[] numArray = this.OpenCfmt(this.e.CurLine);
      for (int curCol = this.e.CurCol; curCol < this.e.text[this.e.CurLine].len; ++curCol)
      {
        int index2 = index1;
        index1 = (int) numArray[curCol];
        if (flag && txt[curCol] != ' ' && txt[curCol] != '\t' || curCol > this.e.CurCol && this.IsBreakChar(txt[curCol]) || curCol > this.e.CurCol && this.e.TerFont[index1].FieldId != this.e.TerFont[index2].FieldId)
        {
          this.e.CurCol = curCol;
          goto label_10;
        }
        if (txt[curCol] == ' ' || txt[curCol] == '\t')
          flag = true;
      }
      this.CloseCfmt(this.e.CurLine);
      if (this.e.CurLine + 1 < this.e.TotalLines)
      {
        this.e.CurCol = 0;
        ++this.e.CurLine;
        ++this.e.CurRow;
        flag = true;
      }
      else
        break;
    }
label_10:
    this.TerPosLine(this.e.CurLine + 1);
    return true;
  }

  internal new bool TerPageDn(bool keyboard)
  {
    if (this.e.InPrintPreview)
      return this.PreviewDown(true);
    if (keyboard)
      this.TerSetCharHilight();
    if (this.e.TerArg.PageMode)
      return this.PgmPageDn();
    if (this.e.CursHorzPos < 0)
      this.e.CursHorzPos = this.ColToUnits(this.e.CurCol, this.e.CurLine, 1);
    if (this.e.frame[0].ScrLastLine >= this.e.TotalLines - 1)
    {
      this.TerWinDown();
      return true;
    }
    int num1 = this.e.frame[0].ScrLastLine - this.e.BeginLine + 1 - this.e.PagingMargin;
    if (num1 <= 0)
      num1 = 1;
    int num2 = this.LastScrollBeginLine();
    this.e.BeginLine += num1;
    if (this.e.BeginLine >= num2)
      this.e.BeginLine = num2;
    this.e.CurLine += num1;
    if (this.e.CurLine >= this.e.TotalLines - 1)
      this.e.CurLine = this.e.TotalLines - 1;
    if (this.e.CurLine < this.e.BeginLine)
      this.e.CurLine = this.e.BeginLine;
    this.e.CurRow = this.e.CurLine - this.e.BeginLine;
    this.e.CurCol = this.UnitsToCol(this.e.CursHorzPos, this.e.CurLine);
    this.e.UseTextMap = false;
    this.e.PaintFlag = 4;
    this.PaintTer();
    return true;
  }

  internal new bool TerPageHorz(char type, int pos)
  {
    if (this.e.TerArg.PageMode)
      return this.PgmPageHorz(pos);
    if (this.e.TerArg.WordWrap && !this.HScrollAllowed())
    {
      if (this.TerWrapWidth(this.e.CurLine, -1) < this.e.TerWinWidth)
        return true;
      this.e.TerWinOrgX = (this.TerWrapWidth(this.e.CurLine, -1) - this.e.TerWinWidth) * pos / 1000;
      if (this.e.TerWinOrgX > this.TerWrapWidth(this.e.CurLine, -1) - this.e.TerWinWidth)
        this.e.TerWinOrgX = this.TerWrapWidth(this.e.CurLine, -1) - this.e.TerWinWidth;
      this.SetTerWindowOrg();
    }
    else
    {
      int num = 0;
      for (int beginLine = this.e.BeginLine; beginLine <= this.e.frame[0].ScrLastLine; ++beginLine)
      {
        int lineWidth = this.GetLineWidth(beginLine, true, true);
        if (lineWidth > num)
          num = lineWidth;
      }
      switch (type)
      {
        case 'L':
          this.e.TerWinOrgX -= this.e.TerWinWidth / 2;
          break;
        case 'R':
          this.e.TerWinOrgX += this.e.TerWinWidth / 2;
          break;
        case 'l':
          this.e.TerWinOrgX -= this.fnt.LwrCharWidth(0, true, 'M');
          break;
        case 'r':
          this.e.TerWinOrgX += this.fnt.LwrCharWidth(0, true, 'M');
          break;
        default:
          this.e.TerWinOrgX = (num - this.e.TerWinWidth) * pos / 1000;
          break;
      }
      if (this.e.TerWinOrgX > num - this.e.TerWinWidth)
        this.e.TerWinOrgX = num - this.e.TerWinWidth;
      if (this.e.TerWinOrgX < 0)
        this.e.TerWinOrgX = 0;
      this.SetTerWindowOrg();
    }
    this.e.UseTextMap = false;
    this.e.PaintFlag = 4;
    this.PaintTer();
    return true;
  }

  internal new bool TerPageLeft(bool page)
  {
    if (this.e.InPrintPreview)
      return this.PreviewLeft(page);
    if (this.e.TerArg.PageMode)
      return this.PgmPageLeft(page);
    if (this.e.TerArg.WordWrap && !this.HScrollAllowed())
    {
      if (page)
        this.e.TerWinOrgX -= this.e.TerWinWidth / 2;
      else
        this.e.TerWinOrgX -= this.e.TerWinWidth / 6;
      if (this.e.TerWinOrgX < 0)
        this.e.TerWinOrgX = 0;
      this.SetTerWindowOrg();
      this.e.UseTextMap = false;
      this.e.PaintFlag = 4;
      this.PaintTer();
      return true;
    }
    return page ? this.TerPageHorz('L', 0) : this.TerPageHorz('l', 0);
  }

  internal new bool TerPageRight(bool page)
  {
    if (this.e.InPrintPreview)
      return this.PreviewRight(page);
    if (this.e.TerArg.PageMode)
      return this.PgmPageRight(page);
    if (this.e.TerArg.WordWrap && !this.HScrollAllowed())
    {
      if (this.TerWrapWidth(this.e.CurLine, -1) < this.e.TerWinWidth)
        return true;
      if (page)
        this.e.TerWinOrgX += this.e.TerWinWidth / 2;
      else
        this.e.TerWinOrgX += this.e.TerWinWidth / 6;
      if (this.e.TerWinOrgX > this.TerWrapWidth(this.e.CurLine, -1) - this.e.TerWinWidth)
        this.e.TerWinOrgX = this.TerWrapWidth(this.e.CurLine, -1) - this.e.TerWinWidth;
      this.SetTerWindowOrg();
      this.e.UseTextMap = false;
      this.e.PaintFlag = 4;
      this.PaintTer();
      return true;
    }
    return page ? this.TerPageHorz('R', 0) : this.TerPageHorz('r', 0);
  }

  internal new bool TerPageUp(bool keyboard)
  {
    if (this.e.InPrintPreview)
      return this.PreviewUp(true);
    if (keyboard)
      this.TerSetCharHilight();
    if (this.e.TerArg.PageMode)
      return this.PgmPageUp();
    if (this.e.CurLine == 0)
      return this.TerWinUp();
    if (this.e.CursHorzPos < 0)
      this.e.CursHorzPos = this.ColToUnits(this.e.CurCol, this.e.CurLine, 1);
    if (this.e.BeginLine == 0)
    {
      this.e.CurLine = this.e.CurRow = 0;
      this.e.CurCol = this.UnitsToCol(this.e.CursHorzPos, this.e.CurLine);
      this.e.PaintFlag = 1;
      this.e.WrapFlag = 0;
      this.PaintTer();
      return true;
    }
    int num = this.e.WinHeight - this.e.PagingMargin;
    if (num <= 0)
      num = 1;
    this.e.CurLine -= num;
    this.e.BeginLine -= num;
    if (this.e.BeginLine < 0)
    {
      this.e.CurLine = this.e.CurRow;
      this.e.BeginLine = 0;
    }
    this.e.CurCol = this.UnitsToCol(this.e.CursHorzPos, this.e.CurLine);
    this.e.UseTextMap = false;
    this.e.PaintFlag = 4;
    this.PaintTer();
    return true;
  }

  internal new bool TerPrevWord(bool pos)
  {
    bool flag = false;
    this.TerSetCharHilight();
    this.e.CursDirection = 2;
    int num1 = this.e.CurCol < this.e.text[this.e.CurLine].len ? this.e.CurCol - 1 : this.e.text[this.e.CurLine].len - 2;
    int num2;
    int index1 = num2 = 0;
    while (true)
    {
      char[] txt = this.e.text[this.e.CurLine].txt;
      ushort[] numArray = this.OpenCfmt(this.e.CurLine);
      for (int index2 = num1; index2 >= 0; --index2)
      {
        int index3 = index1;
        index1 = (int) numArray[index2];
        if (flag && (txt[index2] == ' ' || txt[index2] == '\t') || index2 < num1 && this.e.TerFont[index1].FieldId != this.e.TerFont[index3].FieldId)
        {
          this.e.CurCol = index2 + 1;
          goto label_13;
        }
        if (txt[index2] != ' ' && txt[index2] != '\t')
          flag = true;
      }
      this.CloseCfmt(this.e.CurLine);
      if (!(this.e.CurLine <= 0 | flag))
      {
        --this.e.CurLine;
        --this.e.CurRow;
        if ((this.e.text[this.e.CurLine].flags & 3) == 0 || this.e.text[this.e.CurLine].len <= 0)
          num1 = this.e.text[this.e.CurLine].len - 2;
        else
          break;
      }
      else
        goto label_12;
    }
    this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
    goto label_13;
label_12:
    this.e.CurCol = 0;
label_13:
    if (pos)
      this.TerPosLine(this.e.CurLine + 1);
    return true;
  }

  internal new bool TerReturn()
  {
    bool flag1 = ((uint) this.GetKeyState(16 /*0x10*/) & 32768U /*0x8000*/) > 0U;
    if ((this.e.TerFlags6 & 67108864 /*0x04000000*/) != 0)
      flag1 = !flag1;
    if (this.e.TerArg.LineLimit > 0 && this.e.TotalLines >= this.e.TerArg.LineLimit)
      return this.PrintError(88, "Carriage Return");
    if (this.e.TerArg.WordWrap)
    {
      if (this.e.text[this.e.CurLine].len == 1 && (this.e.text[this.e.CurLine].flags & 4) != 0)
      {
        int bltId = this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].BltId;
        if ((this.e.TerFlags6 & 32 /*0x20*/) == 0 && bltId != 0 && this.e.TerBlt[bltId].ls != 0)
          this.e.TerSetListBullet(false, 0, 0, 0, "", "", false);
      }
      else if (this.e.CurLine > 0 && this.e.text[this.e.CurLine - 1].len == 1 && (this.e.text[this.e.CurLine - 1].flags & 4) != 0 && (this.e.text[this.e.CurLine - 1].flags & 2) == 0)
      {
        int bltId = this.e.PfmtId[this.e.text[this.e.CurLine - 1].pfmt].BltId;
        if ((this.e.TerFlags6 & 32 /*0x20*/) == 0 && bltId != 0 && this.e.TerBlt[bltId].ls != 0)
        {
          --this.e.CurLine;
          this.e.TerSetListBullet(false, 0, 0, 0, "", "", false);
        }
      }
      if (this.e.CurCol == 0 && this.e.text[this.e.CurLine].cid > 0 && !flag1 && (this.e.TerFlags3 & 32 /*0x20*/) == 0)
      {
        int cid = this.e.text[this.e.CurLine].cid;
        int row = this.e.cell[cid].row;
        bool flag2 = this.e.HtmlMode && this.e.cell[cid].PrevCell <= 0 && (this.e.TableRow[row].flags & 32768 /*0x8000*/) != 0;
        if (this.e.CurLine == 0 || flag2 && this.e.text[this.e.CurLine - 1].cid > 0 && this.e.text[this.e.CurLine - 1].cid != this.e.text[this.e.CurLine].cid || (this.e.text[this.e.CurLine - 1].flags & 1966080 /*0x1E0000*/) != 0 || this.True(this.e.text[this.e.CurLine - 1].tabw) && (this.e.text[this.e.CurLine - 1].tabw.type & 14) != 0 && this.e.text[this.e.CurLine - 1].cid == 0)
        {
          this.InsertMarkerLine(this.e.CurLine, this.e.ParaChar, 0, this.e.text[this.e.CurLine].pfmt, 0, 0);
          this.PaintTer();
          return true;
        }
      }
      if (flag1)
        this.TerAscii('\u000F');
      else
        this.TerAscii(this.e.ParaChar);
      this.e.EnterHit = true;
      return true;
    }
    int StartCol = 0;
    if (this.e.text[this.e.CurLine].len > 0)
    {
      char[] txt = this.e.text[this.e.CurLine].txt;
      int index = 0;
      while (index < this.e.text[this.e.CurLine].len && txt[index] == ' ')
        ++index;
      if (txt[index] != ' ')
        StartCol = index;
      if (this.e.CurCol < StartCol)
        StartCol = this.e.CurCol;
    }
    if (this.e.CrSplitLine)
    {
      this.TerSplitLine(StartCol, true, true);
      return true;
    }
    if (this.e.CrNewLine && this.e.text[this.e.CurLine].len <= this.e.CurCol)
    {
      this.TerSplitLine(StartCol, true, true);
      return true;
    }
    if (this.e.CurLine + 1 < this.e.TotalLines)
    {
      this.e.CurCol = 0;
      ++this.e.CurRow;
      ++this.e.CurLine;
      if (this.e.HilightType != 0)
      {
        this.e.HilightType = 0;
        this.e.PaintFlag = 4;
      }
      this.PaintTer();
    }
    return true;
  }

  internal new bool TerRight()
  {
    char minValue = char.MinValue;
    int hilightType = this.e.HilightType;
    this.TerSetCharHilight();
    if (hilightType == 0 || this.e.HilightType != 0)
    {
      bool HilightBegins = hilightType == 0 && this.e.HilightType == 2;
      if (this.e.TerArg.PageMode)
        return this.PgmRight(HilightBegins);
      if (this.e.TerArg.WordWrap && this.e.CurLine == this.e.TotalLines - 1 && this.e.CurCol + 1 == this.e.text[this.e.CurLine].len)
        return true;
      ++this.e.CurCol;
      if (this.e.text[this.e.CurLine].len > 0)
        minValue = this.e.text[this.e.CurLine].txt[this.e.text[this.e.CurLine].len - 1];
      if (this.e.TerArg.WordWrap && this.e.CurCol >= this.e.text[this.e.CurLine].len && (this.e.CurLine + 1 < this.e.TotalLines || (int) minValue == (int) this.e.ParaChar))
      {
        this.e.CurCol = 0;
        if (this.e.CurLine + 1 < this.e.TotalLines)
          ++this.e.CurLine;
      }
      else
      {
        this.e.WrapFlag = 0;
        this.e.PaintFlag = 1;
      }
      this.PaintTer();
    }
    return true;
  }

  internal new bool TerSplitLine(int StartCol, bool AlignTab, bool repaint)
  {
    if (!this.CheckLineLimit(this.e.TotalLines + 1))
      return this.PrintError(88, nameof (TerSplitLine));
    this.SplitLine(this.e.CurLine, this.e.CurCol, StartCol);
    this.e.text[this.e.CurLine + 1].pfmt = this.e.text[this.e.CurLine].pfmt;
    if (this.e.CurLine < this.e.HilightBegRow)
      ++this.e.HilightBegRow;
    if (this.e.CurLine < this.e.HilightEndRow)
      ++this.e.HilightEndRow;
    this.e.CurCol = 0;
    if (AlignTab && this.e.text[this.e.CurLine + 1].len == 0)
    {
      int curLine = this.e.CurLine;
      while (this.e.text[curLine].len == 0 && curLine > 0)
        --curLine;
      int index = 0;
      char[] txt = this.e.text[curLine].txt;
      while (index < this.e.text[curLine].len && txt[index] == ' ')
        ++index;
      this.e.CurCol = index;
    }
    this.e.PaintFlag = 5;
    ++this.e.CurLine;
    ++this.e.CurRow;
    if (this.e.CurRow >= this.e.WinHeight)
    {
      this.e.CurRow = this.e.WinHeight - 1;
      this.e.PaintFlag = 4;
    }
    this.e.BeginLine = this.e.CurLine - this.e.CurRow;
    ++this.e.TerArg.modified;
    if (repaint)
      this.PaintTer();
    return true;
  }

  internal new bool TerUp()
  {
    this.TerSetCharHilight();
    if (this.e.TerArg.PageMode)
      return this.PgmUp();
    if (this.e.CurLine + 1 == this.e.TotalLines && this.e.HilightType == 2 && this.e.StretchHilight && this.e.CurCol + 1 == this.e.text[this.e.CurLine].len && this.e.CurCol > 0)
    {
      this.e.CurCol = 0;
      this.e.WrapFlag = 0;
      this.e.PaintFlag = 1;
      this.PaintTer();
      return true;
    }
    if (this.e.CurLine > 0)
    {
      if (this.e.CursHorzPos < 0)
        this.e.CursHorzPos = this.ColToUnits(this.e.CurCol, this.e.CurLine, 1);
      if (this.e.CurRow > 0)
      {
        --this.e.CurRow;
        --this.e.CurLine;
        this.e.CurCol = this.UnitsToCol(this.e.CursHorzPos, this.e.CurLine);
        if (this.e.TerArg.WordWrap)
        {
          if (this.e.CurCol >= this.e.text[this.e.CurLine].len)
            this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
          if (this.e.CurCol < 0)
            this.e.CurCol = 0;
        }
        this.e.WrapFlag = 0;
        this.e.PaintFlag = 1;
        this.PaintTer();
        return true;
      }
      --this.e.CurLine;
      --this.e.BeginLine;
      --this.e.CurRow;
      this.e.CurCol = this.UnitsToCol(this.e.CursHorzPos, this.e.CurLine);
      if (this.e.TerArg.WordWrap)
      {
        this.e.CurRow = 0;
        if (this.e.CurCol >= this.e.text[this.e.CurLine].len)
          this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
        if (this.e.CurCol < 0)
          this.e.CurCol = 0;
        this.e.WrapFlag = 2;
      }
      this.e.PaintFlag = 4;
      this.e.UseTextMap = false;
      this.PaintTer();
    }
    return true;
  }

  internal new bool TerWinDown()
  {
    this.e.CursDirection = 4;
    if (this.e.InPrintPreview)
      return this.PreviewDown(false);
    if (this.e.TerArg.PageMode)
      return this.PgmWinDown();
    if (this.e.TotalLines <= this.e.WinHeight && this.DocFitsInWindow() || this.e.BeginLine > this.LastScrollBeginLine())
      return true;
    int lineHeight = this.GetLineHeight(this.e.BeginLine, out int _, out tc.SkipInt);
    if (lineHeight > this.e.TerWinHeight)
    {
      if (this.e.WinYOffsetLine == -1)
        this.e.WinYOffset = 0;
      this.e.WinYOffset += this.e.TerWinHeight / 6;
      if (this.e.WinYOffset < lineHeight)
      {
        this.e.WinYOffsetLine = this.e.BeginLine;
        goto label_27;
      }
      if (this.e.WinYOffset > lineHeight)
        this.e.WinYOffset = lineHeight;
    }
    else
      this.e.WinYOffsetLine = -1;
    if (this.e.CurLine + 1 >= this.e.TotalLines && this.e.BeginLine + 1 >= this.e.TotalLines || this.e.HilightType != 0 && this.e.StretchHilight && this.e.TotalLines - this.e.BeginLine < this.e.WinHeight - 1)
      return true;
    if (this.e.CurLine + 1 < this.e.TotalLines)
      ++this.e.CurLine;
    if (this.e.BeginLine + 1 < this.e.TotalLines)
      ++this.e.BeginLine;
    if (this.e.BeginLine > this.e.CurLine)
      this.e.BeginLine = this.e.CurLine;
    this.e.CurRow = this.e.CurLine - this.e.BeginLine;
    if (this.e.TerArg.WordWrap)
    {
      if (this.e.CurCol >= this.e.text[this.e.CurLine].len)
        this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
      if (this.e.CurCol < 0)
        this.e.CurCol = 0;
    }
label_27:
    this.e.UseTextMap = false;
    this.e.PaintFlag = 4;
    this.PaintTer();
    return true;
  }

  internal new bool TerWinLeft()
  {
    if (this.e.TerArg.PageMode)
      return this.PgmWinLeft();
    if (this.e.CurCol != 0)
    {
      --this.e.CurCol;
      if (this.e.CurCol + 1 < this.e.text[this.e.CurLine].len)
      {
        char[] txt = this.e.text[this.e.CurLine].txt;
        this.e.TerWinOrgX -= this.fnt.LwrCharWidth(this.GetCurCfmt(this.e.CurLine, this.e.CurCol + 1), true, txt[this.e.CurCol + 1]);
      }
      else
        this.e.TerWinOrgX -= this.fnt.LwrCharWidth(0, true, ' ');
      if (this.e.TerWinOrgX < 0)
        this.e.TerWinOrgX = 0;
      this.SetTerWindowOrg();
      if (this.e.TerArg.WordWrap)
      {
        if (this.e.CurCol >= this.e.text[this.e.CurLine].len)
          this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
        if (this.e.CurCol < 0)
          this.e.CurCol = 0;
        this.e.WrapFlag = 0;
      }
      this.e.UseTextMap = false;
      this.e.PaintFlag = 4;
      this.PaintTer();
    }
    return true;
  }

  internal new bool TerWinRight()
  {
    if (this.e.TerArg.PageMode)
      return this.PgmWinRight();
    ++this.e.CurCol;
    if (this.e.CurCol < this.e.text[this.e.CurLine].len)
    {
      char[] txt = this.e.text[this.e.CurLine].txt;
      this.e.TerWinOrgX += this.fnt.LwrCharWidth(this.GetCurCfmt(this.e.CurLine, this.e.CurCol - 1), true, txt[this.e.CurCol - 1]);
    }
    else if (this.e.TerArg.WordWrap)
    {
      if (this.e.CurCol >= this.e.text[this.e.CurLine].len)
        this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
      if (this.e.CurCol < 0)
        this.e.CurCol = 0;
      this.e.WrapFlag = 0;
    }
    else
      this.e.TerWinOrgX += this.fnt.LwrCharWidth(0, true, ' ');
    this.SetTerWindowOrg();
    this.e.UseTextMap = false;
    this.PaintTer();
    return true;
  }

  internal new bool TerWinUp()
  {
    bool flag = false;
    this.e.CursDirection = 3;
    if (this.e.InPrintPreview)
      return this.PreviewUp(false);
    if (this.e.TerArg.PageMode)
      return this.PgmWinUp();
    int lineHeight;
    while (true)
    {
      lineHeight = this.GetLineHeight(this.e.BeginLine, out int _, out tc.SkipInt);
      if (lineHeight <= this.e.TerWinHeight || !(this.e.WinYOffsetLine >= 0 | flag))
      {
        this.e.WinYOffsetLine = -1;
        if (!flag)
        {
          if (this.e.CurLine > 0 || this.e.BeginLine > 0)
          {
            if (this.e.CurLine > 0)
              --this.e.CurLine;
            if (this.e.BeginLine > 0)
              --this.e.BeginLine;
            if (this.e.BeginLine > this.e.CurLine)
              this.e.BeginLine = this.e.CurLine;
            this.e.CurRow = this.e.CurLine - this.e.BeginLine;
            if (this.e.TerArg.WordWrap)
            {
              if (this.e.CurCol >= this.e.text[this.e.CurLine].len)
                this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
              if (this.e.CurCol < 0)
                this.e.CurCol = 0;
            }
            flag = true;
          }
          else
            goto label_13;
        }
        else
          goto label_26;
      }
      else
        break;
    }
    if (this.e.WinYOffsetLine == -1 & flag)
      this.e.WinYOffset = lineHeight - this.e.TerWinHeight;
    else
      this.e.WinYOffset -= this.e.TerWinHeight / 6;
    if (this.e.WinYOffset > 0)
    {
      this.e.WinYOffsetLine = this.e.BeginLine;
      goto label_26;
    }
    this.e.WinYOffsetLine = -1;
    goto label_26;
label_13:
    return true;
label_26:
    this.e.UseTextMap = false;
    this.e.PaintFlag = 4;
    this.PaintTer();
    return true;
  }
}
