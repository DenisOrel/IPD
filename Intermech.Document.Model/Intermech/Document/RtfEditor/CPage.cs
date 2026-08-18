// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.CPage
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class CPage : COp
{
  internal CPage(ImRtfEditor ImRtfEditor)
    : base(ImRtfEditor)
  {
  }

  internal int AdjustForParaKeep(int BreakLine, int FirstColLine, bool PageTop)
  {
    int index1 = BreakLine;
    int index2 = 0;
    int num1 = 0;
    bool flag1 = true;
    bool flag2 = false;
    int pfmt = this.e.text[index1].pfmt;
    if (FirstColLine < 0)
      FirstColLine = -FirstColLine;
    while ((this.e.PfmtId[this.e.text[FirstColLine].pfmt].flags & 12288 /*0x3000*/) != 0 && FirstColLine + 1 < index1)
      ++FirstColLine;
    for (; index1 > FirstColLine; --index1)
    {
      int fid = this.e.text[index1 - 1].fid;
      if (fid == 0 || (this.e.ParaFrame[fid].flags & 96 /*0x60*/) != 0)
        break;
    }
    if (index1 > 0 && this.e.text[index1 - 1].cid > 0 && (this.e.TableRow[this.e.cell[this.e.text[index1 - 1].cid].row].flags & 4) != 0)
      flag2 = true;
    int num2;
    do
    {
      num2 = index1;
      int pFirstLine;
      int index3;
      int num3;
      if (index1 > 0 && this.e.text[index1 - 1].cid > 0 && this.IsKeepNextRow(index1 - 1, out pFirstLine))
      {
        bool flag3 = true;
        if (pFirstLine > 0 && this.e.text[pFirstLine - 1].cid > 0 && !flag2 && (this.e.TableRow[this.e.cell[this.e.text[pFirstLine - 1].cid].row].flags & 4) != 0)
          flag3 = false;
        if (flag3)
        {
          if (pFirstLine < index1 & flag1)
          {
            for (int index4 = index1; index4 > pFirstLine; --index4)
            {
              if ((this.e.text[index4].flags & 4) != 0)
              {
                flag1 = false;
                break;
              }
            }
          }
          index1 = pFirstLine;
          index3 = this.e.text[index1].pfmt;
          num3 = this.e.PfmtId[index3].flags;
          goto label_29;
        }
      }
      index3 = this.e.text[index1].pfmt;
      num3 = this.e.PfmtId[index3].flags;
      if (index1 > 0)
      {
        index2 = this.e.text[index1 - 1].pfmt;
        num1 = this.e.PfmtId[index2].flags;
      }
      if ((this.e.text[index1].flags & 4) != 0)
        flag1 = false;
      while (index1 > FirstColLine && (num1 & 32768 /*0x8000*/) != 0 && (this.e.text[index1].flags & 4) != 0)
      {
        if (this.e.text[index1].tabw != null && (this.e.text[index1].tabw.type & 14) != 0 || (this.e.text[index1].flags & 1966080 /*0x1E0000*/) != 0)
          return index1;
        --index1;
        index3 = index2;
        num3 = num1;
        if (index1 > 0)
        {
          index2 = this.e.text[index1 - 1].pfmt;
          num1 = this.e.PfmtId[index2].flags;
        }
      }
label_29:
      if (index1 <= FirstColLine)
        return PageTop ? BreakLine : FirstColLine;
      while (index1 > FirstColLine && this.e.text[index1].fid > 0 && this.e.text[index1].fid == this.e.text[index1 - 1].fid)
      {
        --index1;
        index3 = this.e.text[index1].pfmt;
        num3 = this.e.PfmtId[index3].flags;
      }
      if (index1 <= FirstColLine)
        return PageTop ? BreakLine : FirstColLine;
      for (bool flag4 = (num3 & 16384 /*0x4000*/) != 0; this.e.text[index1].cid == 0 & flag4 && index1 > FirstColLine && (this.e.text[index1].flags & 4) == 0; flag4 = (this.e.PfmtId[index3].flags & 16384 /*0x4000*/) != 0)
      {
        --index1;
        index3 = this.e.text[index1].pfmt;
      }
      if (this.e.text[index1].cid == 0 && (this.e.PfmtId[index3].pflags & 32 /*0x20*/) != 0 && index1 > FirstColLine && (this.e.text[index1].flags & 4) == 0)
      {
        int num4 = index1;
        if ((this.e.text[index1].flags & 1) != 0)
          --index1;
        else if ((this.e.text[index1 - 1].flags & 4) != 0)
          --index1;
        if (index1 != num4)
        {
          int flags = this.e.PfmtId[this.e.text[index1].pfmt].flags;
        }
      }
      if (index1 <= FirstColLine)
        return PageTop ? BreakLine : FirstColLine;
      if (flag1)
        BreakLine = index1;
    }
    while (index1 != num2);
    return index1;
  }

  internal new int AdjustPageNbr(int PageNo, int LineNo)
  {
    if (LineNo >= 0 && LineNo < 0)
    {
      if (PageNo < 0 || PageNo >= this.e.TotalPages)
        return 0;
      if ((this.e.PfmtId[this.e.text[LineNo].pfmt].flags & 12288 /*0x3000*/) == 0 || PageNo + 1 >= this.e.TotalPages)
        return PageNo;
      int topSect = this.e.PageInfo[PageNo].TopSect;
      if (topSect < 0 || topSect >= this.e.TotalSects)
        return PageNo;
      if (LineNo >= this.e.TerSect1[topSect].hdr.FirstLine && LineNo <= this.e.TerSect1[topSect].hdr.LastLine && this.e.TerSect1[topSect].fhdr.FirstLine >= 0)
        ++PageNo;
      if (LineNo >= this.e.TerSect1[topSect].ftr.FirstLine && LineNo <= this.e.TerSect1[topSect].ftr.LastLine && this.e.TerSect1[topSect].fftr.FirstLine >= 0)
        ++PageNo;
    }
    return PageNo;
  }

  internal bool AdjustRowHeightForBaseAlign(int row)
  {
    int firstCell = this.e.TableRow[row].FirstCell;
    int index1 = firstCell;
    while (index1 > 0 && (this.e.cell[index1].flags & 65536 /*0x010000*/) == 0)
      index1 = this.e.cell[index1].NextCell;
    if (index1 > 0)
    {
      int num1 = 0;
      for (int index2 = firstCell; index2 > 0; index2 = this.e.cell[index2].NextCell)
      {
        if ((this.e.cell[index2].flags & 65536 /*0x010000*/) != 0 && this.e.CellAux[index2].BaseHeight > num1)
          num1 = this.e.CellAux[index2].BaseHeight;
      }
      for (int index3 = firstCell; index3 > 0; index3 = this.e.cell[index3].NextCell)
      {
        this.e.CellAux[index3].SpaceBefore = num1 - this.e.CellAux[index3].BaseHeight;
        this.e.CellAux[index3].height += this.e.CellAux[index3].SpaceBefore;
      }
      if (this.e.TableRow[row].MinHeight < 0)
        return true;
      int num2 = 0;
      for (int index4 = firstCell; index4 > 0; index4 = this.e.cell[index4].NextCell)
      {
        if (this.e.CellAux[index4].height > num2)
          num2 = this.e.CellAux[index4].height;
      }
      if (num2 > this.e.TableRow[row].height)
        this.e.TableRow[row].height = num2;
    }
    return true;
  }

  internal int AdjustSectColHeight(
    int MaxColumns,
    int ColHeight,
    int FirstLine,
    ref int LastLine,
    int MaxColHeight,
    bool PageTop,
    int sect)
  {
    int num1 = 0;
    int num2 = 0;
    int num3 = 0;
    bool flag1 = false;
    for (int LineNo = FirstLine; LineNo <= LastLine; ++LineNo)
    {
      if (this.LineInfo(LineNo, 1024 /*0x0400*/))
        return MaxColHeight;
    }
    if (LastLine + 1 > this.e.TotalLines)
      return MaxColHeight;
    int index1 = this.e.TerSect1[sect].NextSect;
    if (index1 < 0)
    {
      this.RecreateSections();
      index1 = this.GetSection(LastLine + 1);
    }
    if (index1 < 0 || (this.e.TerSect[index1].flags & 1) != 0)
      return MaxColHeight;
    for (int index2 = FirstLine; index2 <= LastLine; ++index2)
      this.e.text[index2].flags &= -33;
    int FirstColLine = FirstLine;
    for (int index3 = FirstLine; index3 <= LastLine; ++index3)
    {
      if ((this.e.PfmtId[this.e.text[index3].pfmt].flags & 12288 /*0x3000*/) == 0)
      {
        if (this.e.text[index3].fid == 0)
        {
          int cid = this.e.text[index3].cid;
          int num4;
          if (cid == 0)
            num4 = this.e.text[index3].height + this.GetFrmSpcBef(index3, false);
          else if (this.e.text[index3].tabw != null && (this.e.text[index3].tabw.type & 32 /*0x20*/) != 0 && this.e.cell[this.e.text[index3].cid].level == 0)
            num4 = this.e.TableRow[this.e.cell[cid].row].height;
          else
            continue;
          bool flag2 = index3 > FirstLine && this.e.text[index3 - 1].tabw != null && (this.e.text[index3 - 1].tabw.type & 8) != 0;
          if (flag1)
            flag2 = false;
          flag1 = false;
          if (index3 > FirstLine && num3 + num4 > ColHeight | flag2)
          {
            if (num1 + 1 < MaxColumns)
            {
              if (num3 > num2)
                num2 = num3;
              if (!flag2)
              {
                index3 = this.AdjustForParaKeep(index3, FirstColLine, PageTop);
                if (this.LineInfo(index3, 32 /*0x20*/))
                {
                  int LineNo = index3 - 1;
                  while (LineNo >= FirstColLine && this.e.text[LineNo].cid != 0 && (!this.LineInfo(LineNo, 32 /*0x20*/) || this.TableLevel(LineNo) != 0))
                    --LineNo;
                  index3 = LineNo + 1;
                }
                this.e.text[index3].flags |= 32 /*0x20*/;
              }
              num3 = 0;
              FirstColLine = index3;
              ++num1;
              flag1 = true;
              --index3;
              continue;
            }
            if (flag2 || num3 + num4 > MaxColHeight)
            {
              int num5 = this.AdjustForParaKeep(index3, FirstColLine, PageTop);
              LastLine = num5 - 1;
              return num2;
            }
          }
          num3 += num4;
        }
      }
    }
    if (num3 > num2)
      num2 = num3;
    return num2;
  }

  internal int BeginEndnote(bool LastEndnote, ref int BegLine, int EndnotePara)
  {
    int num1 = 0;
    int NewSize = 25;
    if (this.e.text[BegLine].len > 1)
    {
      if (LastEndnote)
      {
        if (!this.CheckLineLimit(this.e.TotalLines + 1))
          return 0;
        this.MoveLineArrays(BegLine, 1, 'A');
        ++num1;
        ++BegLine;
        this.LineAlloc(BegLine, this.e.text[BegLine].len, 1);
        this.e.text[BegLine].txt[0] = this.e.ParaChar;
        this.e.text[BegLine].flags |= 1;
      }
      else
      {
        int OldSize = this.e.text[BegLine].len - 1;
        this.LineAlloc(BegLine, OldSize, OldSize - 1);
        this.e.text[BegLine].flags &= -2049;
        if (this.e.text[BegLine].tabw != null)
          this.e.text[BegLine].tabw.type &= -3;
        char[] txt1 = this.e.text[BegLine].txt;
        int len = this.e.text[BegLine].len;
        if (len == 0 || len > 0 && (int) txt1[len - 1] != (int) this.e.ParaChar)
        {
          this.LineAlloc(BegLine, len, len + 1);
          this.e.text[BegLine].txt[len] = this.e.ParaChar;
          this.e.text[BegLine].flags |= 1;
        }
        if (!this.CheckLineLimit(this.e.TotalLines + 1))
          return 0;
        this.MoveLineArrays(BegLine, 1, 'A');
        ++num1;
        ++BegLine;
        this.LineAlloc(BegLine, this.e.text[BegLine].len, 2);
        char[] txt2 = this.e.text[BegLine].txt;
        txt2[0] = this.e.ParaChar;
        txt2[1] = '\u0014';
        this.e.text[BegLine].flags |= 2049;
      }
    }
    if (!this.CheckLineLimit(this.e.TotalLines + 1))
      return 0;
    this.MoveLineArrays(BegLine, 1, 'B');
    int num2 = num1 + 1;
    this.e.text[BegLine].pfmt = EndnotePara;
    this.LineAlloc(BegLine, this.e.text[BegLine].len, 1);
    this.e.text[BegLine].txt[0] = this.e.ParaChar;
    this.e.text[BegLine].flags = 1;
    this.e.text[BegLine].flags2 = 0;
    ++BegLine;
    if (!this.CheckLineLimit(this.e.TotalLines + 1))
      return 0;
    this.MoveLineArrays(BegLine, 1, 'B');
    int num3 = num2 + 1;
    this.e.text[BegLine].pfmt = EndnotePara;
    if (this.IsDefLangRtl())
    {
      int pfmt = this.e.text[BegLine].pfmt;
      if (this.e.PfmtId[pfmt].flow != 2)
        this.e.text[BegLine].pfmt = this.SetParaParam(pfmt, 16 /*0x10*/, 2);
    }
    this.LineAlloc(BegLine, this.e.text[BegLine].len, NewSize);
    char[] txt = this.e.text[BegLine].txt;
    for (int index = 0; index < NewSize - 1; ++index)
      txt[index] = '_';
    txt[NewSize - 1] = this.e.ParaChar;
    this.e.text[BegLine].flags = 1;
    this.e.text[BegLine].flags2 = 0;
    ushort[] numArray = this.OpenCfmt(BegLine);
    for (int index = 0; index < NewSize; ++index)
      numArray[index] = (ushort) this.SetFontStyle(0, 512 /*0x0200*/, true);
    this.CloseCfmt(BegLine);
    ++BegLine;
    return num3;
  }

  internal bool BreakBeforeSect(int sect)
  {
    int firstLine = this.e.TerSect[sect].FirstLine;
    if ((this.e.TerSect[sect].flags & 1) != 0)
      return true;
    int index = firstLine;
    while (index < this.e.TotalLines && (this.e.PfmtId[this.e.text[index].pfmt].flags & 12288 /*0x3000*/) != 0)
      ++index;
    return index < this.e.TotalLines && (this.e.PfmtId[this.e.text[index].pfmt].pflags & 64 /*0x40*/) != 0;
  }

  internal bool CalcTblHdrHt(int PageNo)
  {
    int num1 = 0;
    this.e.PageInfo[PageNo].TblHdrHt = 0;
    this.e.PageInfo[PageNo].TblHdrLastLine = -1;
    this.e.PageInfo[PageNo].TblHdrFirstLine = -1;
    int num2;
    int num3 = num2 = -1;
    int index = this.e.PageInfo[PageNo].FirstLine - 1;
    if (PageNo != 0 && index >= 0 && !this.e.HtmlMode && !this.e.TerArg.FittedView && this.e.text[this.e.PageInfo[PageNo].FirstLine].cid != 0)
    {
      int cid1 = this.e.text[index].cid;
      int row1 = this.e.cell[cid1].row;
      if (cid1 == 0)
        return true;
      for (; index >= 0; --index)
      {
        int cid2 = this.e.text[index].cid;
        if (cid2 != 0)
        {
          if (this.e.cell[cid2].level == 0)
          {
            int row2 = this.e.cell[cid2].row;
            if ((this.e.TableRow[row2].flags & 4) != 0)
            {
              if (num2 == -1)
                num2 = index;
              num3 = index;
              if (row2 != num1)
              {
                this.e.PageInfo[PageNo].TblHdrHt += this.e.TableRow[row2].height;
                num1 = row2;
              }
            }
            else if (num3 != -1)
              break;
          }
        }
        else
          break;
      }
      this.e.PageInfo[PageNo].TblHdrFirstLine = num3;
      this.e.PageInfo[PageNo].TblHdrLastLine = num2;
    }
    return true;
  }

  internal bool CheckPageSpace(int LastPage)
  {
    int maxPages = this.e.MaxPages;
    if (LastPage + 1 >= this.e.MaxPages)
    {
      int count = this.e.MaxPages + 5;
      if (count <= LastPage)
        count = LastPage + 10;
      if (count <= this.e.MaxPages)
        return true;
      this.e.PageInfo = this.ReAlloc(this.e.PageInfo, count);
      this.e.MaxPages = count;
      for (int index = maxPages; index < this.e.MaxPages; ++index)
        this.e.PageInfo[index] = new tc.StrPage();
    }
    return true;
  }

  internal new bool CreateEndnote()
  {
    int num1 = 0;
    int EndnotePara = -1;
    int modified = this.e.TerArg.modified;
    ushort CurFont = 0;
    if (tc.DebugMode)
      this.misc.dm(nameof (CreateEndnote));
    int index1 = 0;
    while (index1 < this.e.TotalFonts && (!this.e.TerFont[index1].InUse || (this.e.TerFont[index1].style & 32768 /*0x8000*/) == 0))
      ++index1;
    if (index1 == this.e.TotalFonts)
      return true;
    int num2 = -1;
    for (int StartLine = 0; StartLine < this.e.TotalLines; ++StartLine)
    {
      int pfmt = this.e.text[StartLine].pfmt;
      if (pfmt != num2)
      {
        if ((this.e.PfmtId[pfmt].pflags & 128 /*0x80*/) != 0)
        {
          this.MoveLineArrays(StartLine, 1, 'D');
          if (this.e.CurLine > StartLine)
            --this.e.CurLine;
          if (this.e.HilightType != 0 && this.e.HilightBegRow > StartLine)
            --this.e.HilightBegRow;
          if (this.e.HilightType != 0 && this.e.HilightEndRow > StartLine)
            --this.e.HilightEndRow;
          --StartLine;
        }
        else
          num2 = pfmt;
      }
    }
    int num3 = 0;
    if (!this.e.EndnoteAtSect)
      num3 = this.e.TotalLines - 1;
    int num4 = 0;
    bool LastEndnote = false;
    while (true)
    {
      int index2 = num3;
      while (index2 < this.e.TotalLines && (this.e.text[index2].flags & 2048 /*0x0800*/) == 0)
        ++index2;
      if (index2 == this.e.TotalLines)
      {
        LastEndnote = true;
        --index2;
      }
      int BegLine = index2;
      int num5 = BegLine;
      bool flag1 = true;
      bool flag2 = this.e.CurLine > BegLine;
      bool flag3 = this.e.HilightType != 0 && this.e.HilightBegRow > BegLine;
      bool flag4 = this.e.HilightType != 0 && this.e.HilightEndRow > BegLine;
      for (int index3 = num4; index3 <= num5; ++index3)
      {
        if ((this.e.text[index3].flags2 & 2) == 0)
        {
          if ((this.e.TerOpFlags2 & 16 /*0x10*/) != 0)
          {
            ushort[] numArray = this.OpenCfmt(index3);
            for (int index4 = 0; index4 < this.e.text[index3].len; ++index4)
            {
              if ((this.e.TerFont[(int) numArray[index4]].style & 32768 /*0x8000*/) != 0)
              {
                this.e.text[index3].flags2 |= 2;
                break;
              }
            }
            this.CloseCfmt(index3);
          }
          if ((this.e.text[index3].flags2 & 2) == 0)
            continue;
        }
        if (this.e.text[index3].len != 0)
        {
          int num6 = 0;
          if (flag1)
          {
            if (EndnotePara < 0)
              EndnotePara = this.e.TerCreateParaId(-1, true, 360, 0, -360, 0, 0, 0, 0, 128 /*0x80*/, 0, 0, 0, 0);
            num6 += this.BeginEndnote(LastEndnote, ref BegLine, EndnotePara);
            flag1 = false;
          }
          int num7 = 0;
          while (num7 < this.e.text[index3].len)
          {
            ushort[] numArray1 = this.OpenCfmt(index3);
            int SrcCol = -1;
            int index5;
            for (index5 = num7; index5 < this.e.text[index3].len; ++index5)
            {
              if (SrcCol == -1 && (this.e.TerFont[(int) numArray1[index5]].style & 32768 /*0x8000*/) != 0)
                SrcCol = index5;
              if (SrcCol < 0 || (this.e.TerFont[(int) numArray1[index5]].style & 32768 /*0x8000*/) != 0)
                num1 = index5;
              else
                break;
            }
            num7 = index5;
            this.CloseCfmt(index3);
            if (SrcCol >= 0)
            {
              if (!this.CheckLineLimit(this.e.TotalLines + 1))
                return true;
              this.MoveLineArrays(BegLine, 1, 'B');
              ++num6;
              if (flag2)
                this.e.CurLine += num6;
              if (flag3)
                this.e.HilightBegRow += num6;
              if (flag4)
                this.e.HilightEndRow += num6;
              bool flag5 = true;
              if (num1 + 1 == this.e.text[index3].len && index3 + 1 < this.e.TotalLines && (this.e.TerFont[this.GetCurCfmt(index3 + 1, 0)].style & 32768 /*0x8000*/) != 0)
                flag5 = false;
              int NewSize = num1 - SrcCol + 1;
              if (flag5)
                ++NewSize;
              this.LineAlloc(BegLine, 0, NewSize);
              this.MoveCharInfo(index3, SrcCol, BegLine, 0, num1 - SrcCol + 1);
              this.e.text[BegLine].pfmt = EndnotePara;
              this.e.text[BegLine].cid = 0;
              this.e.text[BegLine].fid = 0;
              int len1 = this.e.text[BegLine].len;
              this.e.text[BegLine].flags = 0;
              this.e.text[BegLine].flags2 = 0;
              if (flag5 && len1 > 1)
              {
                char[] txt = this.e.text[BegLine].txt;
                ushort[] numArray2 = this.OpenCfmt(BegLine);
                txt[len1 - 1] = this.e.ParaChar;
                numArray2[len1 - 1] = numArray2[len1 - 2];
              }
              int len2 = this.e.text[BegLine].len;
              if (len2 > 0 && (this.e.text[index3].flags & 3) != 0)
              {
                char[] txt = this.e.text[BegLine].txt;
                char chr = txt[len2 - 1];
                if ((int) chr == (int) this.e.CellChar || this.lstrchr(this.e.BreakChars, chr))
                  txt[len2 - 1] = this.e.ParaChar;
              }
              char[] txt1 = this.e.text[BegLine].txt;
              int len3 = this.e.text[BegLine].len;
              for (int index6 = 0; index6 < len3; ++index6)
              {
                if (txt1[index6] == '\t')
                  txt1[index6] = ' ';
              }
              for (int StartPos = 1; StartPos < this.e.text[BegLine].len; ++StartPos)
              {
                if (txt1[StartPos - 1] == ' ' && txt1[StartPos] == ' ')
                {
                  this.MoveLineData(BegLine, StartPos, 1, 'D');
                  txt1 = this.e.text[BegLine].txt;
                  --StartPos;
                }
              }
              int len4 = this.e.text[BegLine].len;
              for (int index7 = 0; index7 < this.e.text[BegLine].len; ++index7)
              {
                int curCfmt = this.GetCurCfmt(BegLine, index7);
                if ((this.e.TerFont[curCfmt].style & 64 /*0x40*/) != 0 || this.e.TerFont[curCfmt].FieldId == 6)
                {
                  this.MoveLineData(BegLine, index7, 1, 'D');
                  --index7;
                }
              }
              if (this.e.text[BegLine].tag != null)
              {
                ushort[] tag = this.e.text[BegLine].tag;
                for (int index8 = 0; index8 < len4; ++index8)
                  tag[index8] = (ushort) 0;
                this.CloseCtid(BegLine);
              }
              ushort[] numArray3 = this.OpenCfmt(BegLine);
              ushort maxValue = ushort.MaxValue;
              for (int index9 = 0; index9 < len4; ++index9)
              {
                if ((int) numArray3[index9] != (int) maxValue)
                {
                  maxValue = numArray3[index9];
                  CurFont = (ushort) this.SetFontStyle((int) (ushort) this.SetFontStyle((int) maxValue, 39936, false), 512 /*0x0200*/, true);
                  if (this.e.TerFont[(int) CurFont].ParaStyId != 0 || this.e.TerFont[(int) CurFont].CharStyId != 1)
                    CurFont = (ushort) this.SetFontStyleId((int) CurFont, 1, 0);
                }
                numArray3[index9] = CurFont;
              }
              char[] txt2 = this.e.text[BegLine].txt;
              int len5 = this.e.text[BegLine].len;
              if (len5 > 0 && (int) txt2[len5 - 1] == (int) this.e.ParaChar)
                this.e.text[BegLine].flags |= 1;
              ++BegLine;
            }
          }
        }
      }
      if (!LastEndnote)
        num3 = num4 = BegLine + 1;
      else
        break;
    }
    if (this.e.CurLine >= this.e.TotalLines)
      this.e.CurLine = this.e.TotalLines - 1;
    if (this.e.CurCol >= this.e.text[this.e.CurLine].len)
      this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
    if (this.e.CurCol < 0)
      this.e.CurCol = 0;
    if (this.e.HilightType != 0)
    {
      if (this.e.HilightBegRow >= this.e.TotalLines)
        this.e.HilightBegRow = this.e.TotalLines - 1;
      if (this.e.HilightBegCol >= this.e.text[this.e.HilightBegRow].len)
        this.e.HilightBegCol = this.e.text[this.e.HilightBegRow].len - 1;
      if (this.e.HilightBegCol < 0)
        this.e.HilightBegCol = 0;
      if (this.e.HilightEndRow >= this.e.TotalLines)
        this.e.HilightEndRow = this.e.TotalLines - 1;
      if (this.e.HilightEndCol > this.e.text[this.e.HilightEndRow].len)
        this.e.HilightEndCol = this.e.text[this.e.HilightEndRow].len;
    }
    this.e.TerArg.modified = modified;
    return true;
  }

  internal bool CreateOnePage(int PageNo, bool FullRepage)
  {
    if (this.e.TotalLines == 0)
      return true;
    int num1 = -1;
    bool flag1 = false;
    bool RowSpanned = false;
    int num2 = 0;
    int num3 = 0;
    bool flag2 = false;
    int num4 = 0;
    int num5 = 0;
    bool EndPage = false;
    bool EndCell = false;
    int[] numArray1 = new int[20];
    int[] numArray2 = new int[20];
    int[] CurCell = new int[20];
    int[] CurRowId = new int[20];
    int[] CellHeight = new int[20];
    int[] numArray3 = new int[20];
    int[] SkipCell = new int[20];
    int[] PrevCellsWidth = new int[20];
    int[] numArray4 = new int[20];
    int[] numArray5 = new int[20];
    bool[] flagArray = new bool[20];
    bool[] InPartBotRow = new bool[20];
    bool[] InPartTopRow = new bool[20];
    int[] TableX = new int[20];
    if (tc.DebugMode)
      this.misc.dm("CreateOnePage: " + PageNo.ToString());
    if (this.e.FullRenderMode)
      this.CheckPageSpace(PageNo);
    this.e.LastPageCreated = false;
    int abs;
    int row1 = abs = 0;
    int firstLine = this.e.PageInfo[PageNo].FirstLine;
    int num6 = PageNo + 1 < this.e.TotalPages ? this.e.PageInfo[PageNo].LastLine : this.e.TotalLines - 1;
    if (!FullRepage)
    {
      for (int index = 1; index < this.e.TotalParaFrames; ++index)
      {
        if (this.e.ParaFrame[index].PageNo == PageNo)
          this.e.ParaFrame[index].flags &= -98305;
      }
      this.e.LastWrappedLine = firstLine - 1;
    }
    if (!this.e.IsPlaneText)
    {
      this.ResetLinePictPos(firstLine);
      this.CalcTblHdrHt(PageNo);
    }
    while (true)
    {
      if (num6 + 1 >= this.e.TotalLines)
        num6 = this.e.TotalLines - 1;
      int LastLine;
      for (LastLine = firstLine; LastLine <= num6; ++LastLine)
      {
        int fid = this.e.text[LastLine].fid;
        if (this.e.text[LastLine].page == PageNo)
        {
          this.e.text[LastLine].flags &= -32801;
          if (fid > 0 && fid < this.e.TotalParaFrames)
            this.e.ParaFrame[fid].InUse = true;
        }
      }
      int num7 = firstLine;
      int TopSect = 0;
      if (firstLine != 0)
        TopSect = this.sec.GetSection(firstLine);
      int index1 = TopSect;
      this.e.PageInfo[PageNo].TopSect = TopSect;
      if (PageNo == 0 || this.e.PageInfo[PageNo - 1].TopSect != TopSect && index1 != this.GetSection(firstLine - 1))
        this.e.PageInfo[PageNo].flags |= 2;
      else
        this.e.PageInfo[PageNo].flags &= -3;
      int num8 = 0;
      int num9 = 0;
      if (this.e.FullRenderMode)
      {
        num8 = this.PageHdrHeight2(PageNo, false, true);
        num9 = this.PageFtrHeight(PageNo, false);
      }
      int index2 = 0;
      short num10 = 0;
      int num11 = 0;
      int num12 = 0;
      int num13 = 0;
      int num14 = 0;
      int num15 = 0;
      int num16 = 0;
      int num17 = 0;
      int FirstLine = firstLine;
      int FirstColLine = firstLine;
      bool flag3 = false;
      int num18;
      this.e.PageInfo[PageNo].FnoteHt = num18 = 0;
      this.e.PageInfo[PageNo].LastRow = 0;
      this.e.PageInfo[PageNo].FirstRow = 0;
      int num19 = -1;
      int PrevLevel = this.e.IsPlaneText ? 0 : this.tbl.TableLevel(firstLine);
      int num20 = PrevLevel;
      int SkipLevel = 0;
      this.e.DoExtraPass = false;
      int num21 = 0;
      int num22 = 0;
      int index3 = 0;
      for (int level = 0; level < 20; ++level)
      {
        numArray1[level] = firstLine;
        numArray2[level] = firstLine;
        numArray3[level] = 0;
        PrevCellsWidth[level] = 0;
        SkipCell[level] = 0;
        CurCell[level] = 0;
        CurRowId[level] = 0;
        CellHeight[level] = 0;
        TableX[level] = 0;
        flagArray[level] = false;
        InPartTopRow[level] = false;
        InPartBotRow[level] = false;
        if (level <= num20 && firstLine > 0 && this.e.text[firstLine].cid > 0 && this.e.cell[this.LevelCell(level, firstLine)].row == this.e.cell[this.LevelCell(level, firstLine - 1)].row)
          InPartTopRow[level] = true;
      }
      int num23 = (int) ((double) this.e.TerSect1[index1].PgHeight / 2.0 * (double) this.e.UnitResY);
      float num24 = this.e.TerSect[index1].FtrMargin;
      if (num9 == 0)
        num24 = this.e.TerSect[index1].BotMargin;
      int num25 = (int) ((double) this.e.TerSect1[index1].PgHeight * (double) this.e.UnitResY);
      int num26 = (double) num24 == 0.0 ? num25 : (int) (((double) this.e.TerSect1[index1].PgHeight - (double) num24) * (double) this.e.UnitResY);
      int num27 = num26;
      if (this.e.FullRenderMode)
      {
        if (num8 > 0)
        {
          if ((double) this.e.TerSect[index1].HdrMargin != 0.0)
            num27 -= (int) ((double) this.e.TerSect[index1].HdrMargin * (double) this.e.UnitResY);
        }
        else if ((double) this.e.TerSect[index1].TopMargin != 0.0)
          num27 -= (int) ((double) this.e.TerSect[index1].TopMargin * (double) this.e.UnitResY);
        if (this.e.TerArg.FittedView && !this.e.InPrinting)
          num27 *= this.GetPageMultiple();
      }
      this.e.PageInfo[PageNo].BodyHt = num27 - num9 - num18 - num8;
      int ColumnWidth1;
      int ColumnSpace;
      int TextX;
      int YBefHdr;
      this.sec.GetSectColWidthSpace(index1, index1, out ColumnWidth1, out ColumnSpace, out TextX, out YBefHdr);
      int num28 = 0;
      int PageColHeight = 0;
      int num29 = 0;
      int num30 = this.e.PageInfo[PageNo].TblHdrHt;
      if (num30 >= num27 - num8 - num9 - this.TwipsToUnitY(1440))
        num30 = 0;
      this.e.PageInfo[PageNo].TblHdrHt = num30;
      int num31;
      int CurPgHeight = num31 = num8 + num30;
      if (this.e.FullRenderMode)
        this.PosWatermarkFrame(PageNo);
      for (LastLine = firstLine; LastLine < this.e.TotalLines; ++LastLine)
      {
        if (LastLine > this.e.LastWrappedLine)
          this.WrapMoreLines(index1);
        if (LastLine < this.e.TotalLines)
        {
          if (LastLine > 0)
            PrevLevel = this.tbl.TableLevel(LastLine - 1);
          int level = this.tbl.TableLevel(LastLine);
          if (index3 > 0)
          {
            if (this.e.text[LastLine].cid == index3 && this.LineInfo(LastLine, 16 /*0x10*/))
            {
              index3 = 0;
            }
            else
            {
              if (this.e.text[LastLine].cid == index3 || level > this.e.cell[index3].level)
              {
                if (this.e.text[LastLine].page <= PageNo && LastLine >= num3)
                {
                  this.e.text[LastLine].page = PageNo + 1;
                  continue;
                }
                continue;
              }
              index3 = 0;
            }
          }
          if (this.SkipCellLine(LastLine, level, PrevLevel, InPartBotRow, InPartTopRow, PageNo, SkipCell, ref SkipLevel, CurCell, CurRowId, ref PageColHeight, ref CurPgHeight, TableX, PrevCellsWidth, ref EndPage, CellHeight, ref EndCell))
          {
            if (EndCell)
              this.EndCellOnPage(LastLine, PageNo, InPartTopRow[level], InPartBotRow[level], ref CurCell[level], ref numArray5[level], ref PageColHeight, ref CurPgHeight, CellHeight[level], ref RowSpanned);
            if (InPartBotRow[level] && !this.LineInfo(LastLine, 16 /*0x10*/) && this.e.text[LastLine].page >= PageNo)
            {
              index3 = this.e.text[LastLine].cid;
              num3 = LastLine;
            }
          }
          else
          {
            int height1;
            if (EndPage)
            {
              LastLine = this.AdjustForParaKeep(LastLine, FirstColLine, index1 == TopSect);
              height1 = this.e.text[LastLine].height;
              flag2 = true;
              this.CheckPageSpace(PageNo + 1);
              this.e.PageInfo[PageNo].LastLine = num7;
              this.e.PageInfo[PageNo + 1].FirstLine = LastLine;
              tc.ResetUintFlag(ref this.e.PageInfo[PageNo + 1].flags, 1);
              this.e.PageInfo[PageNo + 1].TopSect = this.e.PageInfo[PageNo].TopSect;
              break;
            }
            int height2 = this.e.text[LastLine].height;
            flag2 = false;
            if ((this.e.text[LastLine].flags & 65536 /*0x010000*/) != 0)
            {
              num18 += this.ExtractFootnote(this.e.TerGr, 0, 0, LastLine, index1, false, false);
              if (this.e.PageInfo[PageNo].FnoteHt == 0 && num18 > 0)
                num18 += 2 * this.TwipsToUnitY(50);
              this.e.PageInfo[PageNo].FnoteHt = num18;
            }
            int index4 = index2;
            int num32 = num22;
            int flags = this.e.PfmtId[this.e.text[LastLine].pfmt].flags;
            index2 = this.e.text[LastLine].fid;
            num22 = 0;
            if ((flags & 12288 /*0x3000*/) != 0)
            {
              num22 = num29;
              if ((this.e.text[LastLine].flags & 524288 /*0x080000*/) != 0)
                num22 = 17;
              if ((this.e.text[LastLine].flags & 1048576 /*0x100000*/) != 0)
                num22 = 16 /*0x10*/;
              if ((this.e.text[LastLine].flags & 131072 /*0x020000*/) != 0)
                num22 = 25;
              if ((this.e.text[LastLine].flags & 262144 /*0x040000*/) != 0)
                num22 = 26;
            }
            bool flag4 = (flags & 12288 /*0x3000*/) == 0 && index2 <= 0;
            int num33 = CurPgHeight;
            if (index2 == 0 && index4 > 0)
            {
              CurPgHeight = num14;
              PageColHeight = num15;
              num31 = num16;
              num28 = num17;
            }
            if (index2 > 0 && index4 > 0 && index2 != index4)
            {
              CurPgHeight = num14;
              PageColHeight = num15;
              num31 = num16;
              num28 = num17;
              this.e.ParaFrame[index2].TextLine = LastLine;
            }
            if (num29 != 0 && num22 == 0)
            {
              if (this.e.TerSect1[index1].PrevSect < 0 || (this.e.TerSect[index1].flags & 1) != 0)
              {
                if (num29 == 17 && num22 != 17)
                {
                  this.e.TerSect1[index1].hdr.height = num33;
                  if ((double) this.UnitToInchesY(this.e.TerSect1[index1].hdr.height) + (double) this.e.TerSect[index1].HdrMargin < (double) this.e.TerSect[index1].TopMargin)
                    this.e.TerSect1[index1].hdr.height = (int) ((double) this.e.UnitResY * ((double) this.e.TerSect[index1].TopMargin - (double) this.e.TerSect[index1].HdrMargin));
                }
                if (num29 == 25 && num22 != 25)
                {
                  this.e.TerSect1[index1].fhdr.height = num33;
                  if ((double) this.UnitToInchesY(this.e.TerSect1[index1].fhdr.height) + (double) this.e.TerSect[index1].HdrMargin < (double) this.e.TerSect[index1].TopMargin)
                    this.e.TerSect1[index1].fhdr.height = (int) ((double) this.e.UnitResY * ((double) this.e.TerSect[index1].TopMargin - (double) this.e.TerSect[index1].HdrMargin));
                }
                num8 = this.PageHdrHeight2(PageNo, false, true);
                if (num8 > 0)
                  num27 = num26 - (int) ((double) this.e.TerSect[index1].HdrMargin * (double) this.e.UnitResY);
                num9 = this.PageFtrHeight(PageNo, false);
                CurPgHeight = num31 = num8 + num30;
                PageColHeight = 0;
              }
              else
              {
                CurPgHeight = num11;
                PageColHeight = num12;
                num31 = num13;
              }
            }
            if (index2 != 0 && index2 != index4 && this.e.ParaFrame[index2].PageNo != PageNo)
              tc.ResetUintFlag(ref this.e.ParaFrame[index2].flags, 32768 /*0x8000*/);
            if (index2 > 0 && index4 == 0)
            {
              num14 = CurPgHeight;
              num15 = PageColHeight;
              num16 = num31;
              num17 = num28;
              this.e.ParaFrame[index2].TextLine = LastLine;
            }
            if (num22 != 0 && num29 == 0)
            {
              num11 = CurPgHeight;
              num12 = PageColHeight;
              num13 = num31;
            }
            if (num29 == 0 && num22 != 0 || num29 == 17 && num22 != 17 || num29 == 16 /*0x10*/ && num22 != 16 /*0x10*/ || num29 == 25 && num22 != 25 || num29 == 26 && num22 != 26 || index2 != index4 && index2 > 0)
            {
              num28 = 0;
              CellHeight[level] = 0;
              CurCell[level] = 0;
              CurRowId[level] = 0;
              if (num29 == 17 && num22 != 17)
              {
                this.e.TerSect1[index1].hdr.height = num33;
                if ((double) this.UnitToInchesY(this.e.TerSect1[index1].hdr.height) + (double) this.e.TerSect[index1].HdrMargin < (double) this.e.TerSect[index1].TopMargin)
                  this.e.TerSect1[index1].hdr.height = (int) ((double) this.e.UnitResY * ((double) this.e.TerSect[index1].TopMargin - (double) this.e.TerSect[index1].HdrMargin));
                num8 = this.PageHdrHeight2(PageNo, false, true);
              }
              if (num29 == 25 && num22 != 25)
              {
                this.e.TerSect1[index1].fhdr.height = num33;
                if ((double) this.UnitToInchesY(this.e.TerSect1[index1].fhdr.height) + (double) this.e.TerSect[index1].HdrMargin < (double) this.e.TerSect[index1].TopMargin)
                  this.e.TerSect1[index1].fhdr.height = (int) ((double) this.e.UnitResY * ((double) this.e.TerSect[index1].TopMargin - (double) this.e.TerSect[index1].HdrMargin));
                num8 = this.PageHdrHeight2(PageNo, false, true);
              }
              if (num29 == 16 /*0x10*/ && num22 != 16 /*0x10*/)
              {
                int num34;
                this.e.TerSect1[index1].ftr.TextHeight = num34 = num5 != 0 ? num5 : num33;
                this.e.TerSect1[index1].ftr.height = num34;
                this.e.TerSect1[index1].ftr.LimitFtrLine = num21;
                if ((double) this.UnitToInchesY(this.e.TerSect1[index1].ftr.height) + (double) this.e.TerSect[index1].FtrMargin < (double) this.e.TerSect[index1].BotMargin)
                  this.e.TerSect1[index1].ftr.height = (int) ((double) this.e.UnitResY * ((double) this.e.TerSect[index1].BotMargin - (double) this.e.TerSect[index1].FtrMargin));
                num9 = this.PageFtrHeight(PageNo, false);
              }
              if (num29 == 26 && num22 != 26)
              {
                int num35;
                this.e.TerSect1[index1].fftr.TextHeight = num35 = num5 != 0 ? num5 : num33;
                this.e.TerSect1[index1].fftr.height = num35;
                this.e.TerSect1[index1].fftr.LimitFtrLine = num21;
                if ((double) this.UnitToInchesY(this.e.TerSect1[index1].fftr.height) + (double) this.e.TerSect[index1].FtrMargin < (double) this.e.TerSect[index1].BotMargin)
                  this.e.TerSect1[index1].fftr.height = (int) ((double) this.e.UnitResY * ((double) this.e.TerSect[index1].BotMargin - (double) this.e.TerSect[index1].FtrMargin));
                num9 = this.PageFtrHeight(PageNo, false);
              }
              if (num8 > 0)
                num27 = num26 - (int) ((double) this.e.TerSect[index1].HdrMargin * (double) this.e.UnitResY);
              if (num22 != 0)
              {
                int num36;
                num5 = num36 = 0;
                num31 = num36;
                PageColHeight = num36;
                CurPgHeight = num36;
                num21 = 0;
              }
            }
            if (num22 == 17)
            {
              if (LastLine != this.e.TerSect1[index1].hdr.LastLine + 1)
                this.e.TerSect1[index1].hdr.FirstLine = -1;
              if (this.e.TerSect1[index1].hdr.FirstLine < 0)
                this.e.TerSect1[index1].hdr.FirstLine = LastLine;
              this.e.TerSect1[index1].hdr.LastLine = LastLine;
              num29 = 17;
            }
            else if (num22 == 25)
            {
              if (LastLine != this.e.TerSect1[index1].fhdr.LastLine + 1)
                this.e.TerSect1[index1].fhdr.FirstLine = -1;
              if (this.e.TerSect1[index1].fhdr.FirstLine < 0)
                this.e.TerSect1[index1].fhdr.FirstLine = LastLine;
              this.e.TerSect1[index1].fhdr.LastLine = LastLine;
              num29 = 25;
            }
            else if (num22 == 16 /*0x10*/)
            {
              if (LastLine != this.e.TerSect1[index1].ftr.LastLine + 1)
                this.e.TerSect1[index1].ftr.FirstLine = -1;
              if (this.e.TerSect1[index1].ftr.FirstLine < 0)
                this.e.TerSect1[index1].ftr.FirstLine = LastLine;
              this.e.TerSect1[index1].ftr.LastLine = LastLine;
              num29 = 16 /*0x10*/;
            }
            else if (num22 == 26)
            {
              if (LastLine != this.e.TerSect1[index1].fftr.LastLine + 1)
                this.e.TerSect1[index1].fftr.FirstLine = -1;
              if (this.e.TerSect1[index1].fftr.FirstLine < 0)
                this.e.TerSect1[index1].fftr.FirstLine = LastLine;
              this.e.TerSect1[index1].fftr.LastLine = LastLine;
              num29 = 26;
            }
            else
              num29 = 0;
            if (index2 > 0 && index2 != index4)
            {
              if ((this.e.ParaFrame[index2].flags & 896) == 0)
              {
                int index5 = LastLine + 1;
                while (index5 < this.e.TotalLines && this.e.text[index5].fid != 0)
                  ++index5;
                if (index5 < this.e.TotalLines && this.e.text[index5].height + CurPgHeight > num27 - num9 - num18 && num29 == 0 && this.e.text[index5].cid == 0 && LastLine > FirstColLine && num28 == this.e.TerSect[index1].columns - 1 && PageNo < 4499)
                {
                  this.e.text[LastLine].page = PageNo + 1;
                  --num7;
                  this.e.PageInfo[PageNo + 1].FirstLine = LastLine;
                  this.e.PageInfo[PageNo].LastLine = num7;
                  tc.ResetUintFlag(ref this.e.PageInfo[PageNo + 1].flags, 1);
                  break;
                }
              }
              this.e.ParaFrame[index2].PageNo = PageNo;
            }
            if (index4 > 0 && index4 != index2 && (this.e.ParaFrame[index4].flags & 16384 /*0x4000*/) == 0)
            {
              if ((this.e.ParaFrame[index4].flags & 32768 /*0x8000*/) == 0 && num32 == 0 && (num22 == 0 || this.e.ViewPageHdrFtr))
                flag3 = true;
              flag1 = true;
            }
            if (LastLine > firstLine && (this.e.text[LastLine].flags & 16384 /*0x4000*/) != 0 && this.IsPictPageBreak(LastLine, PageNo) && (this.e.PfmtId[this.e.text[LastLine].pfmt].flags & 12288 /*0x3000*/) == 0 && this.e.text[LastLine].fid == 0 && this.e.text[LastLine].cid == 0)
            {
              this.e.text[LastLine].page = PageNo + 1;
              num7 = LastLine - 1;
              this.e.PageInfo[PageNo + 1].FirstLine = LastLine;
              this.e.PageInfo[PageNo].LastLine = num7;
              tc.ResetUintFlag(ref this.e.PageInfo[PageNo + 1].flags, 1);
              break;
            }
            if (LastLine > firstLine && (this.e.text[LastLine - 1].flags & 16384 /*0x4000*/) != 0 && this.HasUnpositionedPict(LastLine - 1, PageNo) && ((this.e.PfmtId[this.e.text[LastLine - 1].pfmt].flags & 12288 /*0x3000*/) == 0 || this.e.ViewPageHdrFtr))
            {
              flag3 = true;
              abs = this.RowColToAbs(LastLine - 1, 0);
            }
            if (flag3 && index2 == 0 && num22 == 0 && LastLine > num1)
            {
              this.e.PageInfo[PageNo + 1].FirstLine = LastLine;
              this.e.PageInfo[PageNo].LastLine = num7;
              tc.ResetUintFlag(ref this.e.PageInfo[PageNo + 1].flags, 1);
              if (this.e.TotalPages < PageNo + 2)
                this.e.TotalPages = PageNo + 2;
              this.CreateFrames(false, PageNo, PageNo);
              this.e.PageInfo[PageNo + 1].FirstLine = this.e.TotalLines;
              this.e.PageInfo[PageNo].LastLine = this.e.TotalLines - 1;
              this.e.PageInfo[PageNo + 1].flags &= -2;
              this.e.LastWrappedLine = firstLine - 1;
              flag1 = true;
              num1 = LastLine;
              goto label_15;
            }
            bool flag5 = (((this.e.PfmtId[this.e.text[LastLine].pfmt].pflags & 64 /*0x40*/) == 0 || (this.e.text[LastLine].flags & 4) == 0 ? 0 : (this.e.text[LastLine].cid != 0 ? 0 : (this.e.text[LastLine].fid == 0 ? 1 : 0))) & (flag4 ? 1 : 0)) != 0;
            if (LastLine > 0 && (this.e.text[LastLine - 1].page < PageNo || (this.e.text[LastLine - 1].flags & 1966080 /*0x1E0000*/) != 0))
              flag5 = false;
            if (this.e.text[LastLine].tabw != null && (this.e.text[LastLine].tabw.type & 46) != 0)
              flag5 = false;
            if (LastLine == FirstColLine)
              flag5 = false;
            bool flag6 = false;
            bool flag7 = true;
            if (((this.e.text[LastLine].tabw == null ? 0 : ((this.e.text[LastLine].tabw.type & 46) != 0 ? 1 : 0)) | (flag5 ? 1 : 0)) != 0)
            {
              int num37 = CurPgHeight;
              int frmSpcBef = this.frm.GetFrmSpcBef(LastLine, false);
              CurPgHeight += height2 + frmSpcBef;
              PageColHeight += height2 + frmSpcBef;
              num7 = LastLine;
              this.e.text[LastLine].page = PageNo;
              if (this.LineInfo(LastLine, 2) & flag4 && CurRowId[level] == 0)
              {
                bool flag8 = false;
                int num38 = LastLine;
                int num39 = this.e.UnitResY / 4;
                int num40 = 0;
                int MaxColHeight = num27 - num9 - num31;
                if (this.e.TerSect[index1].columns > 1)
                {
                  PageColHeight = PageColHeight / this.e.TerSect[index1].columns + 1;
                  while (PageColHeight <= MaxColHeight)
                  {
                    LastLine = num38;
                    num40 = this.AdjustSectColHeight(this.e.TerSect[index1].columns, PageColHeight, FirstLine, ref LastLine, MaxColHeight, index1 == TopSect, index1);
                    height2 = this.e.text[LastLine].height;
                    if (LastLine != num38)
                    {
                      if (PageColHeight >= MaxColHeight)
                      {
                        flag2 = true;
                        break;
                      }
                      PageColHeight += num39;
                      if (PageColHeight > MaxColHeight)
                        PageColHeight = MaxColHeight;
                    }
                    else
                      break;
                  }
                  PageColHeight = num40;
                  if (LastLine < num38)
                  {
                    if (LastLine > FirstColLine)
                    {
                      flag8 = true;
                    }
                    else
                    {
                      LastLine = num38;
                      height2 = this.e.text[LastLine].height;
                    }
                  }
                }
                else
                {
                  flag8 = num37 > num27 - num9 - num18 & flag4 && LastLine > FirstColLine;
                  if (flag8)
                  {
                    this.e.text[LastLine].page = PageNo + 1;
                    --num7;
                  }
                }
                if (flag8)
                {
                  this.e.PageInfo[PageNo + 1].FirstLine = LastLine;
                  this.e.PageInfo[PageNo].LastLine = num7;
                  tc.ResetUintFlag(ref this.e.PageInfo[PageNo + 1].flags, 1);
                  break;
                }
                this.e.TerSect[index1].LastPage = PageNo;
                this.e.TerSect1[index1].LastPageHeight = PageColHeight;
                this.e.TerSect[index1].LastLine = LastLine;
                int index6 = index1;
                index1 = this.e.TerSect1[index6].NextSect;
                if (index1 < 0)
                {
                  this.RecreateSections();
                  index1 = this.GetSection(LastLine + 1);
                }
                this.e.TerSect[index1].FirstLine = LastLine + 1;
                this.e.TerSect[index1].LastPage = -1;
                if (LastLine + 1 < this.e.TotalLines && (this.e.PfmtId[this.e.text[LastLine + 1].pfmt].flags & 12288 /*0x3000*/) == 0)
                {
                  this.e.TerSect1[index1].hdr.height = this.e.TerSect1[index6].hdr.height;
                  if (!this.HdrFtrExists(this.e.TerSect1[index6].hdr))
                  {
                    this.ResetHdrFtr(ref this.e.TerSect1[index1].hdr);
                    this.e.TerSect1[index1].hdr.height = (int) (((double) this.e.TerSect[index1].TopMargin - (double) this.e.TerSect[index1].HdrMargin) * (double) this.e.UnitResY);
                  }
                  this.e.TerSect1[index1].ftr.height = this.e.TerSect1[index6].ftr.height;
                  this.e.TerSect1[index1].ftr.TextHeight = this.e.TerSect1[index6].ftr.TextHeight;
                  if (!this.HdrFtrExists(this.e.TerSect1[index6].ftr))
                  {
                    this.ResetHdrFtr(ref this.e.TerSect1[index1].ftr);
                    this.e.TerSect1[index1].ftr.height = (int) (((double) this.e.TerSect[index1].BotMargin - (double) this.e.TerSect[index1].FtrMargin) * (double) this.e.UnitResY);
                  }
                  this.e.TerSect1[index1].fhdr.height = this.e.TerSect1[index6].fhdr.height;
                  if (!this.HdrFtrExists(this.e.TerSect1[index6].fhdr))
                    this.ResetHdrFtr(ref this.e.TerSect1[index1].fhdr);
                  this.e.TerSect1[index1].fftr.height = this.e.TerSect1[index6].fftr.height;
                  this.e.TerSect1[index1].fftr.TextHeight = this.e.TerSect1[index6].fftr.TextHeight;
                  if (!this.HdrFtrExists(this.e.TerSect1[index6].fftr))
                    this.ResetHdrFtr(ref this.e.TerSect1[index1].fftr);
                }
                num10 = (this.e.TerSect[index1].flags & 2) == 0 ? (short) 0 : this.e.TerSect[index1].FirstPageNo;
                CurPgHeight = (num31 += PageColHeight);
                FirstLine = LastLine + 1;
                FirstColLine = LastLine + 1;
                this.GetSectColWidthSpace(TopSect, index1, out ColumnWidth1, out ColumnSpace, out TextX, out YBefHdr);
                num27 = (int) (((double) this.e.TerSect1[TopSect].PgHeight - ((double) this.e.TerSect[TopSect].HdrMargin + (double) this.e.TerSect[index1].FtrMargin)) * (double) this.e.UnitResY);
                if (this.e.TerArg.FittedView && !this.e.InPrinting)
                  num27 *= this.GetPageMultiple();
                PageColHeight = 0;
                num28 = 0;
                CellHeight[level] = 0;
                CurCell[level] = 0;
                CurRowId[level] = 0;
                if (!flag8 && !this.e.TerArg.FittedView && this.BreakBeforeSect(index1))
                  flag6 = true;
                else
                  continue;
              }
              if (this.LineInfo(LastLine, 8) && CurRowId[level] == 0)
              {
                if (num28 + 1 < this.e.TerSect[index1].columns)
                {
                  CurPgHeight = num31;
                  FirstColLine = LastLine + 1;
                  ++num28;
                  CellHeight[level] = 0;
                  CurCell[level] = 0;
                  CurRowId[level] = 0;
                  TextX += ColumnWidth1 + ColumnSpace;
                  continue;
                }
                flag6 = true;
              }
              if (flag6)
                flag5 = false;
              if (flag5 && LastLine > 0)
              {
                this.e.text[LastLine].page = PageNo + 1;
                --LastLine;
                num7 = LastLine;
              }
              if ((flag6 | flag5 || (this.e.text[LastLine].tabw.type & 4) != 0 && !this.e.TerArg.FittedView) && PageNo < 4499 & flag4 && CurRowId[level] == 0)
              {
                this.CheckPageSpace(PageNo + 1);
                this.e.PageInfo[PageNo].LastLine = num7;
                this.e.PageInfo[PageNo + 1].FirstLine = LastLine + 1;
                this.e.PageInfo[PageNo + 1].flags |= 1;
                this.e.PageInfo[PageNo + 1].TopSect = this.e.PageInfo[PageNo].TopSect;
                break;
              }
              if ((this.e.text[LastLine].tabw.type & 32 /*0x20*/) != 0 && CurRowId[level] > 0)
              {
                bool flag9 = true;
                int num41;
                if (InPartTopRow[level])
                  num41 = this.e.TableAux[CurRowId[level]].TopRowHt;
                else if (InPartBotRow[level])
                {
                  num41 = this.e.TableAux[CurRowId[level]].BotRowHt;
                }
                else
                {
                  this.AdjustRowHeightForBaseAlign(CurRowId[level]);
                  num41 = this.e.TableRow[CurRowId[level]].height;
                }
                if (num41 + CurPgHeight > num27 - num9 - num18 && CurPgHeight > num8 && (!this.e.HtmlMode || level == 0 || this.e.InPrinting) && flag4 & flag9 && PageNo < 4499 && !InPartTopRow[level] && !InPartBotRow[level] && numArray2[level] > FirstColLine)
                {
                  int num42 = num19 > FirstColLine ? num19 : FirstColLine;
                  int cid = this.e.text[LastLine].cid;
                  flag6 = true;
                  LastLine = numArray1[level] <= num42 ? numArray2[level] : numArray1[level];
                  height2 = this.e.text[LastLine].height;
                  int parentCell = this.e.cell[cid].ParentCell;
                  for (int index7 = level - 1; index7 >= 0 && parentCell > 0; --index7)
                  {
                    SkipCell[index7] = parentCell;
                    InPartBotRow[index7] = true;
                    this.e.TableRow[CurRowId[index7]].flags |= 16 /*0x10*/;
                    this.e.TableAux[CurRowId[index7]].LastPage = PageNo;
                    parentCell = this.e.cell[parentCell].ParentCell;
                  }
                  CurRowId[level] = 0;
                  CurCell[level] = 0;
                }
                else
                {
                  PageColHeight += num41;
                  CurPgHeight += num41;
                  if (level > 0)
                  {
                    int[] numArray6;
                    IntPtr index8;
                    (numArray6 = CellHeight)[(int) (index8 = (IntPtr) (level - 1))] = numArray6[(int) index8] + num41;
                  }
                  if (InPartBotRow[level])
                  {
                    if (level != 0)
                    {
                      int cid = this.e.text[LastLine + 1].cid;
                      int row2 = this.e.cell[cid].row;
                      if (cid > 0 && this.e.cell[cid].level == level && this.IsSpannedRow(row2))
                      {
                        for (int index9 = numArray1[level]; index9 <= LastLine; ++index9)
                          this.e.text[index9].page = PageNo + 1;
                      }
                      SkipLevel = level;
                    }
                    else
                      break;
                  }
                  CurRowId[level] = 0;
                  CurCell[level] = 0;
                  bool flag10;
                  InPartBotRow[level] = flag10 = false;
                  InPartTopRow[level] = flag10;
                  continue;
                }
              }
            }
            int LineNo;
            int num43;
            int y;
            int num44;
            int num45;
            while (true)
            {
              bool flag11 = height2 + CurPgHeight > num27 - num9 - num18;
              if ((flag6 | flag11) & flag4 && (!this.e.HtmlMode || level == 0 || this.e.InPrinting) && (CurRowId[level] == 0 || !flagArray[level]) && CurPgHeight > num8 && LastLine > FirstColLine && PageNo < 4499)
              {
                if (CurRowId[level] <= 0)
                {
                  int num46 = num19 > FirstColLine ? num19 : FirstColLine;
                  LineNo = LastLine;
                  num43 = level;
                  if (level > 0 && this.e.text[LastLine].cid != 0 && this.IsSpannedRow(-this.e.text[LastLine].cid) && numArray1[level] > num46)
                  {
                    LastLine = numArray1[level];
                    height1 = this.e.text[LastLine].height;
                  }
                  if (level > 0 && this.e.text[LastLine].cid != 0)
                  {
                    for (int index10 = level - 1; index10 >= 0 && this.e.cell[this.e.text[LastLine].cid].ParentCell > 0; --index10)
                    {
                      if (flagArray[index10])
                      {
                        LastLine = numArray1[index10] <= firstLine ? numArray2[index10] : numArray1[index10];
                        height1 = this.e.text[LastLine].height;
                        level = index10;
                      }
                    }
                  }
                  if (level <= 0 || num28 < this.e.TerSect[index1].columns - 1)
                  {
                    if (num28 < this.e.TerSect[index1].columns - 1)
                    {
                      LastLine = this.AdjustForParaKeep(LastLine, FirstColLine, index1 == TopSect);
                      height2 = this.e.text[LastLine].height;
                      ++num28;
                      if (LastLine > FirstColLine)
                      {
                        this.e.text[LastLine].flags |= 32 /*0x20*/;
                        TextX += ColumnWidth1 + ColumnSpace;
                      }
                      FirstColLine = LastLine;
                      CurPgHeight = num31;
                    }
                    else
                      goto label_271;
                  }
                  else
                    goto label_258;
                }
                else
                  break;
              }
              bool flag12 = false;
              int num47 = level;
              if (CurCell[level] == 0 && this.e.text[LastLine].cid > 0)
              {
                int index11 = this.e.text[LastLine].cid;
                flag12 = true;
                int index12;
                for (index12 = level; index12 >= 0 && index11 > 0 && CurCell[index12] == 0; --index12)
                {
                  CurCell[index12] = index11;
                  index11 = this.e.cell[index11].ParentCell;
                }
                num4 = index12 + 1;
                if ((this.e.cell[this.e.text[LastLine].cid].flags & 65536 /*0x010000*/) != 0)
                {
                  this.e.CellAux[this.e.text[LastLine].cid].BaseHeight = this.e.text[LastLine].BaseHt;
                  this.e.CellAux[this.e.text[LastLine].cid].SpaceBefore = 0;
                }
              }
              for (int index13 = num4; index13 <= num47 & flag12; ++index13)
              {
                int CellId = CurCell[index13];
                if (this.e.TableAux[this.e.cell[CellId].row].FirstPage == PageNo)
                  this.e.cell[CellId].FirstLine = LastLine;
                this.e.cell[CellId].LastLine = LastLine;
                tc.ResetUintFlag(ref this.e.CellAux[CellId].flags, 1);
                CellHeight[index13] = 0;
                SkipCell[index13] = 0;
                TableX[index13] = index13 != 0 ? TableX[index13 - 1] + this.e.TableRow[CurRowId[index13 - 1]].CurIndent + PrevCellsWidth[index13 - 1] : 0;
                int num48;
                this.e.CellAux[CellId].NextColCell = num48 = 0;
                this.e.CellAux[CellId].PrevColCell = num48;
                if (CurRowId[index13] == 0)
                  numArray4[index13] = CurPgHeight + YBefHdr - (this.e.ViewPageHdrFtr ? 0 : num8);
                int border = this.e.cell[CellId].border;
                int cellFrameTopWidth = this.GetCellFrameTopWidth(CellId, ref border, PageNo, out tc.SkipColor);
                int cellFrameBotWidth = this.GetCellFrameBotWidth(CellId, ref border, PageNo, out tc.SkipColor);
                tc.ResetUintFlag(ref this.e.CellAux[CellId].flags, 2);
                int unitY = this.TwipsToUnitY(cellFrameTopWidth + cellFrameBotWidth);
                CurPgHeight += unitY;
                PageColHeight += unitY;
                int[] numArray7;
                IntPtr index14;
                (numArray7 = CellHeight)[(int) (index14 = (IntPtr) index13)] = numArray7[(int) index14] + unitY;
                numArray5[index13] = 0;
                if (CurRowId[index13] == 0)
                {
                  int row3 = this.e.cell[CellId].row;
                  CurRowId[index13] = row3;
                  if (index13 > 0)
                    this.AdjustTableRowWidth(row3);
                  if (!InPartTopRow[index13])
                  {
                    tc.ResetUintFlag(ref this.e.TableRow[row3].flags, 16 /*0x10*/);
                    this.e.TableAux[row3].FirstPage = PageNo;
                    this.e.TableAux[row3].BotRowHt = 0;
                  }
                  this.e.TableAux[row3].TopRowHt = 0;
                  this.e.TableAux[row3].LastPage = PageNo;
                  flagArray[index13] = (this.e.TableRow[row3].flags & 8196) != 0 || this.e.TerSect[index1].columns > 1 || num29 != 0 || this.e.TableRow[row3].MinHeight < 0 || this.IsKeepNextRow(LastLine, out tc.SkipInt) || index13 > 0 && this.IsSpannedRow(row3) || index13 > 0 && this.IsSpanningRow(row3) || index13 > 0 && flagArray[index13 - 1];
                  if (LastLine == FirstColLine)
                    flagArray[index13] = false;
                  if (this.e.TableRow[row3].MinHeight < 0)
                    this.e.TableRow[row3].height = this.TwipsToUnitY(-this.e.TableRow[row3].MinHeight);
                  else
                    this.e.TableRow[row3].height = this.TwipsToUnitY(this.e.TableRow[row3].MinHeight);
                  this.e.TableRow[row3].MinPictHeight = 0;
                  if (!this.IsSpannedRow(row3))
                  {
                    numArray3[index13] = row3;
                    numArray1[index13] = LastLine;
                  }
                  numArray2[index13] = LastLine;
                  if ((this.e.TableRow[row3].flags & 4) != 0)
                    num19 = -1;
                  else if (num19 == -1)
                    num19 = LastLine;
                }
                if (this.e.cell[CellId].PrevCell > 0)
                {
                  int prevCell = this.e.cell[CellId].PrevCell;
                  int[] numArray8;
                  IntPtr index15;
                  (numArray8 = PrevCellsWidth)[(int) (index15 = (IntPtr) index13)] = numArray8[(int) index15] + this.TwipsToUnitX(this.e.cell[prevCell].width);
                }
              }
              level = num47;
              y = this.e.text[LastLine].y;
              this.e.text[LastLine].x = TextX;
              int num49 = CurPgHeight + YBefHdr;
              if (!this.e.ViewPageHdrFtr && !this.e.BorderShowing)
                num49 -= num8;
              this.e.text[LastLine].y = num49;
              if (this.e.text[LastLine].cid != 0 && LastLine == numArray1[level])
              {
                if (this.e.text[LastLine].fid == 0)
                {
                  num44 = this.CalcFrmSpcBefRow(LastLine, index1);
                  this.e.text[LastLine].y = num49 + num44;
                  for (int index16 = level; index16 >= 0 && LastLine == numArray1[index16]; --index16)
                  {
                    this.e.TableRow[CurRowId[index16]].FrmSpcBef = num44;
                    int[] numArray9;
                    IntPtr index17;
                    (numArray9 = numArray4)[(int) (index17 = (IntPtr) index16)] = numArray9[(int) index17] + num44;
                    int ColumnWidth2;
                    if (index16 == 0)
                    {
                      ColumnWidth2 = ColumnWidth1;
                    }
                    else
                    {
                      int index18 = CurCell[index16 - 1];
                      ColumnWidth2 = this.TwipsToUnitX(this.e.cell[index18].width - 2 * this.e.cell[index18].margin);
                    }
                    this.SetRowIndent(LastLine, CurRowId[index16], index1, ColumnWidth2);
                    PrevCellsWidth[index16] = 0;
                  }
                }
                else
                {
                  int fid = this.e.text[LastLine].fid;
                  num44 = 0;
                  this.e.text[LastLine].y = num49;
                  for (int index19 = level; index19 >= 0 && LastLine == numArray1[index19]; --index19)
                  {
                    this.e.TableRow[CurRowId[index19]].FrmSpcBef = num44;
                    int unitX;
                    if (index19 == 0)
                    {
                      unitX = this.TwipsToUnitX(this.e.ParaFrame[fid].width);
                    }
                    else
                    {
                      int index20 = CurCell[index19 - 1];
                      unitX = this.TwipsToUnitX(this.e.cell[index20].width - 2 * this.e.cell[index20].margin);
                    }
                    this.SetRowIndent(LastLine, CurRowId[index19], index1, unitX);
                    PrevCellsWidth[index19] = 0;
                  }
                }
              }
              else
              {
                num44 = 0;
                if (this.e.text[LastLine].cid != 0)
                {
                  int index21 = numArray3[level];
                  this.e.TableRow[CurRowId[level]].CurIndent = this.e.TableRow[index21].CurIndent;
                  this.e.TableRow[CurRowId[level]].indent = this.e.TableRow[index21].indent;
                }
              }
              if (this.e.text[LastLine].cid != 0)
                this.e.text[LastLine].x += TableX[level] + this.e.TableRow[CurRowId[level]].CurIndent + PrevCellsWidth[level];
              num45 = this.e.text[LastLine].fid != 0 ? 0 : this.frm.CalcFrmSpcBef(LastLine, index1, false, PageNo);
              if (num45 > 0 & flag7 && height2 + CurPgHeight + num45 > num27 - num9 - num18 & flag4)
              {
                for (; LastLine - 1 > firstLine && this.e.text[LastLine - 1].fid > 0; --LastLine)
                  this.e.ParaFrame[this.e.text[LastLine - 1].fid].flags &= -32769;
                flag6 = true;
                flag7 = false;
              }
              else
                goto label_330;
            }
            int index22 = this.e.text[LastLine].cid;
            for (int index23 = level; index23 >= 0 && index22 > 0; --index23)
            {
              bool flag13 = true;
              if (index23 == level && this.LineInfo(LastLine, 16 /*0x10*/) && !this.LineInfo(LastLine + 1, 32 /*0x20*/))
                flag13 = false;
              SkipCell[index23] = !flag13 ? 0 : index22;
              InPartBotRow[index23] = true;
              this.e.TableRow[CurRowId[index23]].flags |= 16 /*0x10*/;
              this.e.TableAux[CurRowId[index23]].LastPage = PageNo;
              index22 = this.e.cell[index22].ParentCell;
            }
            this.EndCellOnPage(LastLine, PageNo, InPartTopRow[level], InPartBotRow[level], ref CurCell[level], ref numArray5[level], ref PageColHeight, ref CurPgHeight, CellHeight[level], ref RowSpanned);
            index3 = this.e.text[LastLine].cid;
            num3 = LastLine;
            if (this.e.text[LastLine].page <= PageNo)
            {
              this.e.text[LastLine].page = PageNo + 1;
              continue;
            }
            continue;
label_258:
            int cid1 = this.e.text[LineNo].cid;
            int num50 = this.tbl.TableLevel(LineNo);
            SkipLevel = num43;
            int parentCell1 = this.e.cell[cid1].ParentCell;
            for (int index24 = num50 - 1; index24 >= 0 && parentCell1 > 0; --index24)
            {
              SkipCell[index24] = parentCell1;
              InPartBotRow[index24] = true;
              this.e.TableRow[CurRowId[index24]].flags |= 16 /*0x10*/;
              this.e.TableAux[CurRowId[index24]].LastPage = PageNo;
              parentCell1 = this.e.cell[parentCell1].ParentCell;
            }
            if (this.e.text[LastLine].page <= PageNo)
              this.e.text[LastLine].page = PageNo + 1;
            if (LastLine < LineNo)
            {
              for (; LastLine < this.e.TotalLines; ++LastLine)
              {
                if (this.e.text[LastLine].page <= PageNo)
                  this.e.text[LastLine].page = PageNo + 1;
                if (this.tbl.TableLevel(LastLine) == 0 && this.LineInfo(LastLine, 16 /*0x10*/))
                  break;
              }
              SkipLevel = 0;
              continue;
            }
            continue;
label_271:
            LastLine = this.AdjustForParaKeep(LastLine, FirstColLine, index1 == TopSect);
            height1 = this.e.text[LastLine].height;
            flag2 = true;
            this.CheckPageSpace(PageNo + 1);
            this.e.PageInfo[PageNo].LastLine = num7;
            this.e.PageInfo[PageNo + 1].FirstLine = LastLine;
            this.e.PageInfo[PageNo + 1].flags &= -2;
            this.e.PageInfo[PageNo + 1].TopSect = this.e.PageInfo[PageNo].TopSect;
            break;
label_330:
            this.e.text[LastLine].y += num45;
            if (this.e.text[LastLine].fid == 0 & flag1 && this.e.text[LastLine].y != y && LastLine > num1)
            {
              int num51 = 0;
              int num52 = 0;
              int FrameX = 0;
              int FrameWidth = 0;
              if (this.e.text[LastLine].tabw != null)
              {
                num51 = this.e.text[LastLine].tabw.FrameX;
                num52 = this.e.text[LastLine].tabw.FrameWidth;
              }
              COp.RECT rect = new COp.RECT();
              this.frm.GetFrameSpace(LastLine, rect, out FrameX, out FrameWidth, out tc.SkipInt);
              if (FrameX != num51 || FrameWidth != num52)
              {
                this.e.LastWrappedLine = firstLine - 1;
                num1 = LastLine;
                goto label_15;
              }
            }
            if ((flags & 8192 /*0x2000*/) != 0 && num5 == 0 && this.e.text[LastLine].fid == 0 && CurPgHeight + height2 + num45 + num44 > num23)
            {
              num5 = CurPgHeight;
              num21 = LastLine - 1;
            }
            CurPgHeight += height2 + num45 + num44;
            PageColHeight += height2 + num45 + num44;
            int[] numArray10;
            IntPtr index25;
            (numArray10 = CellHeight)[(int) (index25 = (IntPtr) level)] = numArray10[(int) index25] + (height2 + num45);
            if (this.e.text[LastLine].cid != 0 && (this.e.text[LastLine].flags & 16384 /*0x4000*/) != 0)
            {
              int num53 = this.GetPictLastY(LastLine) + this.TwipsToUnitY(60);
              if (num53 - numArray4[level] > numArray5[level])
                numArray5[level] = num53 - numArray4[level];
            }
            if (this.e.text[LastLine].tabw != null && (this.e.text[LastLine].tabw.type & 16 /*0x10*/) != 0 && CurCell[level] > 0 && CurRowId[level] > 0)
              this.EndCellOnPage(LastLine, PageNo, InPartTopRow[level], InPartBotRow[level], ref CurCell[level], ref numArray5[level], ref PageColHeight, ref CurPgHeight, CellHeight[level], ref RowSpanned);
            this.UpdateBookmark(LastLine, PageNo);
            num7 = LastLine;
            this.e.text[LastLine].page = PageNo;
          }
        }
        else
          break;
      }
      this.e.PageInfo[PageNo].BodyHt = num27 - num9 - num18 - num8;
      if (abs == 0)
        row1 = 0;
      else
        this.AbsToRowCol(abs, out row1, out int _);
      if (LastLine <= row1)
      {
        bool flag14 = true;
        for (int LineNo = LastLine; LineNo <= row1; ++LineNo)
        {
          if (this.e.text[LineNo].page == PageNo && (this.e.text[LineNo].flags & 16384 /*0x4000*/) != 0)
          {
            this.ResetLinePictPos(LineNo);
            if (flag14)
              this.SetPictPageBreak(LineNo, PageNo + 1);
            flag14 = false;
          }
        }
      }
      if (flag2 && LastLine <= num7)
      {
        for (int index26 = LastLine; index26 <= num7; ++index26)
        {
          if (this.e.text[index26].page == PageNo)
          {
            this.e.text[index26].page = PageNo + 1;
            int fid = this.e.text[index26].fid;
            if (fid != 0)
            {
              tc.ResetUintFlag(ref this.e.ParaFrame[fid].flags, 32768 /*0x8000*/);
              this.e.ParaFrame[fid].PageNo = PageNo + 1;
            }
          }
        }
        num7 = LastLine - 1;
      }
      if (num7 >= this.e.TotalLines)
        num7 = this.e.TotalLines - 1;
      this.e.PageInfo[PageNo].LastLine = num7;
      int lastLine = this.e.PageInfo[0].LastLine;
      for (int index27 = 1; index27 <= PageNo; ++index27)
      {
        if (this.e.PageInfo[index27].LastLine >= this.e.TotalLines)
          this.e.PageInfo[index27].LastLine = this.e.TotalLines - 1;
        if (this.e.PageInfo[index27].LastLine > lastLine)
          lastLine = this.e.PageInfo[index27].LastLine;
      }
      LastLine = this.e.PageInfo[PageNo].FirstLine + 1;
      while (LastLine <= lastLine && this.e.text[LastLine].page <= PageNo)
        ++LastLine;
      if (LastLine > this.e.TotalLines)
        LastLine = this.e.TotalLines;
      this.e.PageInfo[PageNo + 1].FirstLine = LastLine;
      this.e.LastPageCreated = LastLine == this.e.TotalLines;
      if (this.e.LastPageCreated)
      {
        this.e.TotalPages = PageNo + 1;
        this.e.PageInfo[this.e.TotalPages].LastLine = this.e.TotalLines;
        tc.ResetUintFlag(ref this.e.PageInfo[this.e.TotalPages].flags, 1);
      }
      else
      {
        this.e.PageInfo[PageNo + 1].DispNbr = num10 != (short) 0 ? (int) num10 : this.e.PageInfo[PageNo].DispNbr + 1;
        if (PageNo + 2 > this.e.TotalPages)
          this.e.TotalPages = PageNo + 2;
      }
      LastLine = this.e.PageInfo[PageNo].FirstLine;
      while (LastLine <= this.e.PageInfo[PageNo].LastLine && (this.e.text[LastLine].cid == 0 || this.e.text[LastLine].page != PageNo))
        ++LastLine;
      if (LastLine <= this.e.PageInfo[PageNo].LastLine)
        this.e.PageInfo[PageNo].FirstRow = this.e.cell[this.e.text[LastLine].cid].row;
      LastLine = this.e.PageInfo[PageNo].LastLine;
      while (LastLine >= this.e.PageInfo[PageNo].FirstLine && (this.e.text[LastLine].cid == 0 || this.e.text[LastLine].page != PageNo))
        --LastLine;
      if (LastLine >= this.e.PageInfo[PageNo].FirstLine)
        this.e.PageInfo[PageNo].LastRow = this.e.cell[this.e.text[LastLine].cid].row;
      for (LastLine = this.e.PageInfo[PageNo].FirstLine; LastLine <= this.e.PageInfo[PageNo].LastLine; ++LastLine)
      {
        int cid = this.e.text[LastLine].cid;
        if (cid != 0 && this.e.text[LastLine].page == PageNo)
          this.e.TableRow[this.e.cell[cid].row].PageNo = PageNo;
      }
      if (lastLine + 1 < this.e.TotalLines && (this.e.text[lastLine + 1].flags & 16384 /*0x4000*/) != 0 && this.e.text[lastLine + 1].fid == 0 && this.e.text[lastLine + 1].cid == 0)
      {
        this.SetPictPageBreak(lastLine + 1, PageNo + 1);
        flag3 = true;
      }
      int LineNo1 = !this.e.LastPageCreated ? this.e.PageInfo[PageNo].LastLine : this.e.TotalLines - 1;
      if ((this.e.text[LineNo1].flags & 16384 /*0x4000*/) != 0 && this.HasUnpositionedPict(LineNo1, PageNo))
        flag3 = flag1 = true;
      if (this.e.FullRenderMode && (flag1 || this.e.TerArg.FittedView || flag3 || this.e.LastPageCreated))
        this.CreateFrames(false, PageNo, PageNo);
      if (flag3 && this.e.PageInfo[PageNo].LastLine > num1)
      {
        this.e.LastWrappedLine = firstLine - 1;
        num1 = this.e.PageInfo[PageNo].LastLine;
        continue;
      }
      ++num2;
      if ((flag1 | RowSpanned || this.e.DoExtraPass || this.e.DocHasToc || (this.e.PageInfo[PageNo].flags & 4) != 0) && num2 < 2)
      {
        this.e.LastWrappedLine = firstLine - 1;
        num1 = -1;
        continue;
      }
      break;
label_15:;
    }
    if (flag1 && this.e.LastWrappedLine >= this.e.PageInfo[PageNo + 1].FirstLine)
      this.e.LastWrappedLine = this.e.PageInfo[PageNo + 1].FirstLine - 1;
    return true;
  }

  internal bool EndCellOnPage(
    int l,
    int PageNo,
    bool InPartTopRow,
    bool InPartBotRow,
    ref int CurCell,
    ref int MinCellHeight,
    ref int PageColHeight,
    ref int CurPgHeight,
    int CellHeight,
    ref bool RowSpanned)
  {
    if ((this.e.CellAux[CurCell].flags & 1) == 0)
    {
      int row = this.e.cell[CurCell].row;
      if (this.e.cell[CurCell].TextAngle != 0)
      {
        if (MinCellHeight < this.TwipsToUnitY(720))
          MinCellHeight = this.TwipsToUnitY(720);
        if (CellHeight > MinCellHeight)
          CellHeight = MinCellHeight;
      }
      if (!InPartTopRow)
      {
        this.e.CellAux[CurCell].height = CellHeight;
        this.e.CellAux[CurCell].FirstPageHt = CellHeight;
        this.e.CellAux[CurCell].LastPageHt = CellHeight;
        this.e.CellAux[CurCell].FirstPage = PageNo;
        this.e.CellAux[CurCell].LastPage = PageNo;
      }
      else
      {
        this.e.CellAux[CurCell].LastPageHt = CellHeight;
        this.e.CellAux[CurCell].LastPage = PageNo;
      }
      if (this.e.TableRow[row].MinHeight < 0)
        MinCellHeight = 0;
      int num = MinCellHeight > CellHeight ? MinCellHeight : CellHeight;
      if (this.IsLastSpannedCell(CurCell))
        num = this.GetLastSpannedCellHeight(CurCell, out tc.SkipInt, PageNo);
      else if (this.e.cell[CurCell].RowSpan > 1 || (this.e.cell[CurCell].flags & 16 /*0x10*/) != 0)
        num = 0;
      if (this.e.cell[CurCell].RowSpan > 1)
        RowSpanned = true;
      if (this.UnitToTwipsY(MinCellHeight) > this.e.TableRow[row].MinPictHeight)
        this.e.TableRow[row].MinPictHeight = this.UnitToTwipsY(MinCellHeight);
      if (InPartTopRow && this.e.TableAux[row].TopRowHt < num)
        this.e.TableAux[row].TopRowHt = num;
      if (InPartBotRow && this.e.TableAux[row].BotRowHt < num)
        this.e.TableAux[row].BotRowHt = num;
      if (InPartBotRow | InPartTopRow && this.e.cell[CurCell].PrevCell <= 0 && this.e.TableRow[row].MinHeight > 0)
        this.e.TableRow[row].height = 0;
      if (this.e.TableRow[row].height < num && (this.e.TableRow[row].MinHeight >= 0 || this.e.TableRow[row].MinPictHeight > 0))
        this.e.TableRow[row].height = num;
      this.e.cell[CurCell].LastLine = l;
      this.e.CellAux[CurCell].flags |= 1;
      PageColHeight -= CellHeight;
      CurPgHeight -= CellHeight;
      CurCell = 0;
    }
    return true;
  }

  internal new int ExtractFootnote(
    Graphics gr,
    int x,
    int y,
    int line,
    int sect,
    bool screen,
    bool DoDraw)
  {
    int footnote = 0;
    bool inPrinting = this.e.InPrinting;
    int index1 = line;
    ushort[] numArray = (ushort[]) null;
    if (tc.DebugMode)
      this.misc.dm(nameof (ExtractFootnote));
    if (this.e.TerArg.FittedView || (this.e.PfmtId[this.e.text[index1].pfmt].flags & 12288 /*0x3000*/) != 0 || this.e.WrapCharWidth == null && (this.e.WrapCharWidth = new ushort[this.e.WrapBufferSize]) == null)
      return 0;
    this.SetFnoteFontInfo(true);
    int WrapWidth = (int) (((double) this.e.TerSect1[sect].PgWidth - (double) this.e.TerSect[sect].LeftMargin - (double) this.e.TerSect[sect].RightMargin) * (double) this.e.UnitResX);
    int len = this.e.text[line].len;
    for (int col = 0; col < len && index1 <= line; ++col)
    {
      int style = this.e.TerFont[this.GetCurCfmt(index1, col)].style;
      if (col == 0 && this.IsFootnoteStyle(style))
      {
        if (this.IsFootnoteStyle(this.e.TerFont[this.GetPrevCfmt(index1, col)].style))
        {
          while (col < len && this.IsFootnoteStyle(this.e.TerFont[this.GetCurCfmt(index1, col)].style))
            ++col;
        }
        if (col == len)
          break;
      }
      while (col < len && !this.IsFootnoteStyle(this.e.TerFont[this.GetCurCfmt(index1, col)].style))
        ++col;
      if (col != len)
      {
        int index2 = 0;
        bool flag1 = true;
        while (col < len)
        {
          if (flag1)
          {
            this.e.InPrinting = !screen;
            numArray = this.GetLineCharWidth(index1);
            this.e.InPrinting = inPrinting;
          }
          int curCfmt = this.GetCurCfmt(index1, col);
          char curChar = this.GetCurChar(index1, col);
          bool flag2 = this.IsFootnoteStyle(this.e.TerFont[curCfmt].style);
          int num = (int) numArray[col];
          if (!flag2 || index2 >= this.e.WrapBufferSize)
          {
            int BufLen = index2;
            this.e.wrap[BufLen] = char.MinValue;
            footnote += this.DrawOneFootnote(gr, x, y + footnote, line, BufLen, WrapWidth, screen, DoDraw);
            if (flag2)
              index2 = 0;
            else
              break;
          }
          this.e.wrap[index2] = curChar;
          if (this.e.wrap[index2] == '\u0005')
            this.e.wrap[index2] = this.e.ParaChar;
          this.e.WrapCfmt[index2] = (ushort) curCfmt;
          this.e.WrapCharWidth[index2] = (ushort) num;
          ++index2;
          ++col;
          if (col == len)
          {
            ++index1;
            if (index1 < this.e.TotalLines)
            {
              len = this.e.text[index1].len;
              col = 0;
              numArray = (ushort[]) null;
            }
            else
              break;
          }
          flag1 = true;
        }
      }
      else
        break;
    }
    this.SetFnoteFontInfo(false);
    return footnote;
  }

  internal new int GetCurPage(int LineNo)
  {
    if (LineNo >= this.e.TotalLines)
      return this.e.CurPage = this.e.TotalPages - 1;
    if (this.e.TerArg.PageMode && this.e.ViewPageHdrFtr && (this.e.PfmtId[this.e.text[LineNo].pfmt].flags & 12288 /*0x3000*/) != 0)
    {
      int index = 0;
      while (index < this.e.TotalFrames && (this.e.frame[index].empty || LineNo < this.e.frame[index].PageFirstLine || LineNo > this.e.frame[index].PageLastLine + 1))
        ++index;
      if (index < this.e.TotalFrames)
        return this.e.CurPage;
    }
    this.e.CurPage = this.PageFromLine(LineNo, -1);
    return this.e.CurPage;
  }

  internal int GetPageMultiple() => !this.e.TerArg.FittedView || this.e.InPrinting ? 1 : 2;

  internal int GetPictLastY(int LineNo)
  {
    int x = 0;
    if ((this.e.text[LineNo].flags & 16384 /*0x4000*/) == 0)
      return 0;
    ushort[] numArray = this.OpenCfmt(LineNo);
    for (int index1 = 0; index1 < this.e.text[LineNo].len; ++index1)
    {
      ushort index2 = numArray[index1];
      if (this.e.TerFont[(int) index2].InUse && (this.e.TerFont[(int) index2].style & 128 /*0x80*/) != 0 && this.e.TerFont[(int) index2].FrameType != 0 && this.e.TerFont[(int) index2].ParaFID > 0)
      {
        int paraFid = this.e.TerFont[(int) index2].ParaFID;
        if (this.e.ParaFrame[paraFid].ShapeType == 75 && (this.e.ParaFrame[paraFid].flags & 32768 /*0x8000*/) != 0)
        {
          int num = this.e.ParaFrame[paraFid].y + this.e.ParaFrame[paraFid].height;
          if (num > x)
            x = num;
        }
      }
    }
    this.CloseCfmt(LineNo);
    return this.TwipsToUnitY(x);
  }

  internal new int GetScrPageHt(int PageNo)
  {
    int num1 = 0;
    if (this.e.BorderShowing)
      num1 = this.UnitToScrY(this.e.TopBorderHeight + this.e.BotBorderHeight);
    int num2;
    if (this.e.TerArg.FittedView)
    {
      num2 = this.e.PageInfo[PageNo].ScrHt;
    }
    else
    {
      int section = this.GetSection(this.e.PageInfo[PageNo].FirstLine);
      num2 = (int) ((double) this.e.TerSect1[section].PgHeight * (double) this.e.ScrResY);
      if (!this.e.BorderShowing && !this.e.ViewPageHdrFtr)
        num2 -= (int) (((double) this.e.TerSect[section].TopMargin + (double) this.e.TerSect[section].BotMargin) * (double) this.e.ScrResY);
    }
    return num2 + num1;
  }

  internal bool HasUnpositionedPict(int LineNo, int PageNo)
  {
    bool flag = false;
    if ((this.e.text[LineNo].flags & 16384 /*0x4000*/) == 0)
      return false;
    ushort[] numArray = this.OpenCfmt(LineNo);
    for (int index1 = 0; index1 < this.e.text[LineNo].len; ++index1)
    {
      ushort index2 = numArray[index1];
      if (this.e.TerFont[(int) index2].InUse && (this.e.TerFont[(int) index2].style & 128 /*0x80*/) != 0 && this.e.TerFont[(int) index2].FrameType != 0 && this.e.TerFont[(int) index2].ParaFID > 0)
      {
        int paraFid = this.e.TerFont[(int) index2].ParaFID;
        this.e.ParaFrame[paraFid].InUse = true;
        if (this.e.ParaFrame[paraFid].PageNo > PageNo)
          tc.ResetUintFlag(ref this.e.ParaFrame[paraFid].flags, 32768 /*0x8000*/);
        if ((this.e.ParaFrame[paraFid].flags & 32768 /*0x8000*/) == 0)
          flag = true;
        this.e.ParaFrame[paraFid].PageNo = PageNo;
      }
    }
    this.CloseCfmt(LineNo);
    return flag;
  }

  internal bool IsKeepNextRow(int line, out int pFirstLine)
  {
    pFirstLine = line;
    if (line < 0 || line >= this.e.TotalLines)
      return false;
    int cid1 = this.e.text[line].cid;
    if (cid1 == 0)
      return false;
    int level = this.e.cell[cid1].level;
    int row = this.e.cell[cid1].row;
    int index1;
    for (index1 = line - 1; index1 >= 0; --index1)
    {
      int cid2 = this.e.text[index1].cid;
      if (cid2 == 0 || this.e.cell[cid2].level <= level && row != this.e.cell[cid2].row)
        break;
    }
    int index2 = index1 + 1;
    pFirstLine = index2;
    return (this.e.PfmtId[this.e.text[index2].pfmt].flags & 32768 /*0x8000*/) != 0;
  }

  internal new bool IsPageLastRow(int row, int PageNo)
  {
    int num = this.LevelRow(this.e.cell[row >= 0 ? this.e.TableRow[row].FirstCell : -row].level, this.e.PageInfo[PageNo].LastRow);
    if (row < 0)
    {
      int CurCell = -row;
      row = this.e.cell[CurCell].row;
      for (int index = (this.e.cell[CurCell].flags & 16 /*0x10*/) == 0 ? this.e.cell[CurCell].RowSpan : this.GetRemainingCellSpans(CurCell); index > 1; --index)
      {
        if (row == num)
          return true;
        row = this.e.TableRow[row].NextRow;
        if (row <= 0)
          return false;
      }
    }
    return row == num;
  }

  internal bool IsPictPageBreak(int LineNo, int PageNo)
  {
    if ((this.e.text[LineNo].flags & 16384 /*0x4000*/) != 0)
    {
      ushort[] numArray = this.OpenCfmt(LineNo);
      for (int index1 = 0; index1 < this.e.text[LineNo].len; ++index1)
      {
        ushort index2 = numArray[index1];
        if (this.e.TerFont[(int) index2].InUse && (this.e.TerFont[(int) index2].style & 128 /*0x80*/) != 0 && this.e.TerFont[(int) index2].FrameType != 0 && this.e.TerFont[(int) index2].ParaFID > 0)
        {
          int paraFid = this.e.TerFont[(int) index2].ParaFID;
          if (this.e.ParaFrame[paraFid].PageNo == PageNo + 1 && (this.e.ParaFrame[paraFid].flags & 65536 /*0x010000*/) != 0)
            return true;
        }
      }
      this.CloseCfmt(LineNo);
    }
    return false;
  }

  internal new Color PageColor()
  {
    return !this.IsSameColor(this.e.PageBkColor, tc.CLR_WHITE) ? this.e.PageBkColor : this.e.TextDefBkColor;
  }

  internal new int PageFromLine(int LineNo, int PrevPage)
  {
    if (LineNo < 0)
      LineNo = 0;
    if (LineNo >= this.e.TotalLines)
      LineNo = this.e.TotalLines - 1;
    int PageNo;
    if (PrevPage >= 0 && PrevPage < this.e.TotalPages && (this.e.PfmtId[this.e.text[LineNo].pfmt].flags & 12288 /*0x3000*/) != 0 && this.GetSection(LineNo) == this.GetSection(this.e.PageInfo[PrevPage].FirstLine))
      PageNo = PrevPage;
    else if (this.e.text[LineNo].cid != 0)
    {
      PageNo = this.e.text[LineNo].page;
    }
    else
    {
      PageNo = 0;
      while (PageNo < this.e.TotalPages && this.e.PageInfo[PageNo].FirstLine <= LineNo)
        ++PageNo;
      if (PageNo > 0)
        --PageNo;
    }
    return this.AdjustPageNbr(PageNo, LineNo);
  }

  internal new int PageFtrHeight(int PageNo, bool IncludeOverflow)
  {
    bool flag = PageNo == 0 || (this.e.PageInfo[PageNo].flags & 2) != 0;
    int topSect = this.e.PageInfo[PageNo].TopSect;
    int num1 = 0;
    int index = -1;
    tc.StrHdrFtr ftr;
    if (flag && this.e.TerSect1[topSect].fftr.FirstLine >= 0)
    {
      index = topSect;
      num1 = this.e.TerSect1[topSect].fftr.height;
    }
    else if (flag && (this.e.TerSect[topSect].flags & 4) != 0)
    {
      index = this.PageFtrSect(PageNo, out ftr);
      if (index >= 0)
        num1 = this.e.TerSect1[index].fftr.height;
    }
    if (index < 0)
    {
      index = this.PageFtrSect(PageNo, out ftr);
      if (index < 0)
        index = 0;
      num1 = this.e.TerSect1[index].ftr.height;
    }
    int num2 = num1 > 0 ? 1 : 0;
    if (!IncludeOverflow && (this.e.TerSect[index].flags & 16 /*0x10*/) != 0)
      num1 = (int) (((double) this.e.TerSect[index].BotMargin - (double) this.e.TerSect[index].FtrMargin) * (double) this.e.UnitResY);
    if (num2 != 0 && num1 == 0)
      num1 = 1;
    if (num1 < 0)
      num1 = 0;
    return num1;
  }

  internal new int PageFtrSect(int PageNo, out tc.StrHdrFtr ftr)
  {
    bool flag = (this.e.PageInfo[PageNo].flags & 2) != 0;
    int topSect = this.e.PageInfo[PageNo].TopSect;
    ftr = new tc.StrHdrFtr();
    if (flag && this.e.TerSect1[topSect].fftr.FirstLine >= 0)
    {
      ftr = this.e.TerSect1[topSect].fftr;
      return topSect;
    }
    if (flag && (this.e.TerSect[topSect].flags & 4) != 0)
    {
      for (int index = topSect; index >= 0; index = this.e.TerSect1[index].PrevSect)
      {
        if (index >= 0 && this.e.TerSect1[index].fftr.FirstLine >= 0)
        {
          ftr = this.e.TerSect1[index].ftr;
          return index;
        }
        if ((this.e.TerSect[index].flags & 4) == 0)
          break;
      }
      return -1;
    }
    int index1 = topSect;
    while (index1 >= 0 && (index1 < 0 || this.e.TerSect1[index1].ftr.FirstLine < 0 || !this.e.EditPageHdrFtr && !this.HdrFtrExists(this.e.TerSect1[index1].ftr)))
      index1 = this.e.TerSect1[index1].PrevSect;
    if (index1 >= 0)
      ftr = this.e.TerSect1[index1].ftr;
    if (ftr.FirstLine < 0)
      index1 = -1;
    this.e.PageInfo[PageNo].FtrSect = index1;
    return index1;
  }

  internal new int PageFtrTextHeight(int PageNo)
  {
    int num = PageNo == 0 ? 1 : ((this.e.PageInfo[PageNo].flags & 2) != 0 ? 1 : 0);
    int topSect = this.e.PageInfo[PageNo].TopSect;
    return num != 0 && this.e.TerSect1[topSect].fftr.FirstLine >= 0 && (PageNo == 0 || (this.e.TerSect[topSect].flags & 1) != 0) ? this.e.TerSect1[topSect].fftr.TextHeight : this.e.TerSect1[topSect].ftr.TextHeight;
  }

  internal new int PageHdrHeight(int PageNo, bool IncludeOverflow)
  {
    return this.PageHdrHeight2(PageNo, IncludeOverflow, false);
  }

  internal new int PageHdrHeight2(int PageNo, bool IncludeOverflow, bool inherit)
  {
    bool flag = PageNo == 0 || (this.e.PageInfo[PageNo].flags & 2) != 0;
    int topSect = this.e.PageInfo[PageNo].TopSect;
    int num1 = 0;
    int index = -1;
    tc.StrHdrFtr hdr;
    if (flag && this.e.TerSect1[topSect].fhdr.FirstLine >= 0)
    {
      index = topSect;
      num1 = this.e.TerSect1[topSect].fhdr.height;
    }
    else if (flag && (this.e.TerSect[topSect].flags & 4) != 0)
    {
      index = PageNo != 0 ? this.PageHdrSect(PageNo, out hdr) : 0;
      if (index >= 0)
        num1 = this.e.TerSect1[index].fhdr.height;
    }
    if (index < 0)
    {
      if (PageNo != 0)
      {
        index = inherit ? this.PageHdrSect(PageNo, out hdr) : this.sec.GetSection(this.e.PageInfo[PageNo].FirstLine);
        if (index < 0)
          index = topSect;
      }
      else
        index = 0;
      num1 = this.e.TerSect1[index].hdr.height;
    }
    int num2 = num1 > 0 ? 1 : 0;
    if (!IncludeOverflow && (this.e.TerSect[index].flags & 8) != 0)
      num1 = (int) (((double) this.e.TerSect[index].TopMargin - (double) this.e.TerSect[index].HdrMargin) * (double) this.e.UnitResY);
    if (num2 != 0 && num1 == 0)
      num1 = 1;
    if (num1 < 0)
      num1 = 0;
    return num1;
  }

  internal new int PageHdrSect(int PageNo, out tc.StrHdrFtr hdr)
  {
    bool flag = (this.e.PageInfo[PageNo].flags & 2) != 0;
    int topSect = this.e.PageInfo[PageNo].TopSect;
    hdr = new tc.StrHdrFtr();
    if (flag && this.e.TerSect1[topSect].fhdr.FirstLine >= 0)
    {
      hdr = this.e.TerSect1[topSect].fhdr;
      return topSect;
    }
    if (flag && (this.e.TerSect[topSect].flags & 4) != 0)
    {
      for (int index = topSect; index >= 0; index = this.e.TerSect1[index].PrevSect)
      {
        if (index >= 0 && this.e.TerSect1[index].fhdr.FirstLine >= 0)
        {
          hdr = this.e.TerSect1[index].fhdr;
          return index;
        }
        if ((this.e.TerSect[index].flags & 4) == 0)
          break;
      }
      return -1;
    }
    int index1 = topSect;
    while (index1 >= 0 && (index1 < 0 || this.e.TerSect1[index1].hdr.FirstLine < 0 || !this.e.EditPageHdrFtr && !this.HdrFtrExists(this.e.TerSect1[index1].hdr)))
      index1 = this.e.TerSect1[index1].PrevSect;
    if (index1 >= 0)
      hdr = this.e.TerSect1[index1].hdr;
    if (hdr.FirstLine < 0)
      index1 = -1;
    this.e.PageInfo[PageNo].HdrSect = index1;
    return index1;
  }

  internal bool PageResized()
  {
    if ((this.e.TerFlags5 & 524288 /*0x080000*/) != 0)
    {
      int textHeight = this.e.TerGetTextHeight();
      int topSect = this.e.PageInfo[0].TopSect;
      int twips1 = (int) this.InchesToTwips((double) this.e.TerSect[topSect].TopMargin + (double) this.e.TerSect[topSect].BotMargin);
      int NewPageSize = textHeight + twips1;
      bool flag = this.e.TotalPages > 1;
      if (!flag)
      {
        int twips2 = (int) this.InchesToTwips((double) this.e.TerSect1[topSect].PgHeight);
        if (NewPageSize + this.e.PageHeightAdj + 120 < twips2)
          flag = true;
        if (NewPageSize > twips2)
          flag = true;
      }
      if (flag)
      {
        int num = NewPageSize;
        this.e.FirePageSizeChanging((object) this.e, ref NewPageSize);
        if (NewPageSize != 0)
        {
          if (NewPageSize < num)
            NewPageSize = num;
          this.e.PageHeightAdj = NewPageSize - num;
          return this.e.TerSetSectPageSize(0, PaperKind.Custom, (int) this.InchesToTwips((double) this.e.TerSect1[topSect].PgWidth), NewPageSize + 60, true);
        }
      }
    }
    return false;
  }

  internal new int PageTextWidth()
  {
    int num1 = 0;
    int num2;
    if (!this.e.TerArg.FittedView)
    {
      int pageSect = this.TerGetPageSect(this.e.CurPage);
      num2 = !this.e.BorderShowing ? (int) ((double) this.e.ScrResX * ((double) this.e.TerSect1[pageSect].PgWidth - (double) this.e.TerSect[pageSect].LeftMargin - (double) this.e.TerSect[pageSect].RightMargin)) : (int) ((double) this.e.ScrResX * (double) this.e.TerSect1[pageSect].PgWidth) + 2 * this.UnitToScrX(this.e.LeftBorderWidth);
    }
    else
      num2 = this.e.TerWinWidth;
    for (int index = 0; index < this.e.TotalFrames; ++index)
    {
      if (!this.e.frame[index].empty && this.e.frame[index].y <= this.e.TerWinOrgY + this.e.TerWinHeight && this.e.frame[index].y + this.e.frame[index].height >= this.e.TerWinOrgY)
      {
        int num3 = this.e.frame[index].x + this.e.frame[index].width;
        if (num3 > num1)
          num1 = num3;
      }
    }
    if (num1 < num2)
      num1 = num2;
    return num1;
  }

  internal new bool PgmDown()
  {
    bool flag1 = false;
    bool flag2 = false;
    int num1 = 0;
    if (!this.e.StretchHilight && !this.e.DraggingText)
    {
      if (this.e.HilightType == 2 && this.e.HilightEndRow == this.e.CurLine - 1 && this.e.HilightEndCol == this.e.text[this.e.HilightEndRow].len)
      {
        --this.e.CurLine;
        this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
      }
      this.TerSetCharHilight();
    }
    int frame1;
    while ((frame1 = this.frm.GetFrame(this.e.CurLine)) >= 0)
    {
      this.e.CursDirection = 1;
      this.e.HilightAtCurPos = true;
      if (this.e.CursHorzPos < 0)
        this.e.CursHorzPos = this.ColToUnits(this.e.CurCol, this.e.CurLine, 1024 /*0x0400*/);
      if (this.e.CursHorzPos >= this.e.frame[frame1].x + this.e.frame[frame1].width)
      {
        int num2 = this.e.frame[frame1].x + this.e.frame[frame1].width - 1;
        num1 = this.e.CursHorzPos - num2;
        this.e.CursHorzPos = num2;
      }
      int pageLastLine = this.e.frame[frame1].PageLastLine;
      if (this.e.text[pageLastLine].tabw != null && (this.e.text[pageLastLine].tabw.type & 32 /*0x20*/) != 0)
        --pageLastLine;
      int num3 = this.LineTextAngle(-frame1);
      int y;
      if (this.e.CurLine < pageLastLine)
      {
        y = this.LineToUnits(this.e.CurLine) + this.ScrLineHeight(this.e.CurLine, false) + 1;
        if (num3 > 0)
          goto label_35;
      }
      else
      {
        if (num3 > 0)
          return true;
        y = this.e.frame[frame1].y + this.e.frame[frame1].height + 1;
      }
      bool flag3 = this.e.CurLine >= pageLastLine;
      int num4 = this.e.CurPageHeight;
      for (int index = 0; index < this.e.TotalFrames; ++index)
      {
        if (!this.e.frame[index].empty && (this.e.frame[index].flags & 4096 /*0x1000*/) == 0 && ((this.e.TerFlags3 & 32 /*0x20*/) == 0 || this.e.frame[index].CellId == this.e.text[this.e.CurLine].cid) && (!flag3 || frame1 != index) && this.e.CursHorzPos >= this.e.frame[index].x && this.e.CursHorzPos < this.e.frame[index].x + this.e.frame[index].width)
        {
          if (y >= this.e.frame[index].y && y < this.e.frame[index].y + this.e.frame[index].height)
            flag1 = true;
          if (this.e.frame[index].y > y && this.e.frame[index].y < num4)
            num4 = this.e.frame[index].y;
        }
      }
      if (!flag1 && num4 == this.e.CurPageHeight)
      {
        if (this.e.CurLine + 1 == this.e.TotalLines)
        {
          if (this.e.HilightType == 2)
          {
            this.e.HilightEndCol = this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
            this.PaintTer();
          }
          return true;
        }
        if (this.e.CurPage != this.e.LastFramePage || flag2)
          return this.PgmPageDn();
        this.CreateFrames(false, this.e.CurPage, this.e.CurPage + 1);
        flag2 = true;
        continue;
      }
      if (!flag1)
        y = num4;
label_35:
      this.e.TerOpFlags |= 16384 /*0x4000*/;
      this.e.CurLine = this.UnitsToLine2(this.e.CursHorzPos, y, num3 > 0 ? frame1 : -1);
      this.e.TerOpFlags &= -16385;
      if (this.e.CurPage == this.e.FirstFramePage && y >= this.e.FirstPageHeight)
        ++this.e.CurPage;
      int frame2 = this.frm.GetFrame(this.e.CurLine);
      int scrLastLine = this.e.frame[frame2].ScrLastLine;
      if (this.e.CurLine == scrLastLine && this.GetRowY(this.e.CurLine) + this.ScrLineHeight(this.e.CurLine, true) > this.e.TerWinOrgY + this.e.TerWinHeight)
        --scrLastLine;
      if ((this.e.frame[frame2].flags & 4) != 0 && this.e.CurLine <= scrLastLine && (this.e.TerWinOrgY + this.e.TerWinHeight < this.e.CurPageHeight || this.e.LastFramePage == this.e.TotalPages - 1))
      {
        this.e.PaintFlag = 1;
        this.e.WrapFlag = 0;
      }
      else
      {
        if ((this.e.TerFlags3 & 4194304 /*0x400000*/) != 0)
          this.e.CurLineY = this.e.TerWinHeight - this.e.text[this.e.CurLine].ScrHt;
        else
          this.e.CurLineY = this.e.TerWinHeight / 2;
        this.e.TerWinOrgY = this.LineToUnits(this.e.CurLine) - this.e.CurLineY;
        if (this.e.TerWinOrgY < 0)
          this.e.TerWinOrgY = 0;
        this.SetTerWindowOrg();
        int terWinOrgY = this.e.TerWinOrgY;
        int line = this.UnitsToLine(this.e.CursHorzPos, terWinOrgY);
        if (line == this.e.CurLine - 1)
        {
          int units = this.LineToUnits(line);
          if (units < terWinOrgY)
          {
            int num5 = this.ScrLineHeight(this.e.CurLine, false);
            this.e.CurLineY += terWinOrgY - units;
            if (this.e.CurLineY + num5 >= this.e.TerWinHeight)
              this.e.CurLineY = this.e.TerWinHeight - num5 - 1;
            if (this.e.CurLineY < this.e.TerWinHeight / 2)
              this.e.CurLineY = this.e.TerWinHeight / 2;
          }
        }
        this.e.PaintFlag = 4;
        this.e.UseTextMap = false;
      }
      this.e.CurCol = this.UnitsToCol(this.e.CursHorzPos + num1, this.e.CurLine);
      if (this.e.HilightType == 2 && this.e.StretchHilight && this.e.text[this.e.CurLine].cid != 0)
      {
        while (this.e.CurLine + 1 < this.e.TotalLines && !this.LineInfo(this.e.CurLine, 16 /*0x10*/))
          ++this.e.CurLine;
        this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
      }
      this.PaintTer();
      return true;
    }
    return true;
  }

  internal new bool PgmLeft()
  {
    if (this.e.HilightType == 2 && this.e.text[this.e.HilightEndRow].cid > 0)
      return this.TblHilightLeft();
    this.TerSetCharHilight();
    this.e.CursDirection = 2;
    if (this.e.CurCol > 0)
    {
      --this.e.CurCol;
      this.e.WrapFlag = 0;
      this.e.PaintFlag = 1;
      this.PaintTer();
      return true;
    }
    if (this.e.CurLine != 0 && ((this.e.TerFlags3 & 32 /*0x20*/) == 0 || this.e.text[this.e.CurLine].cid == this.e.text[this.e.CurLine - 1].cid))
    {
      bool flag = (this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].flags & 12288 /*0x3000*/) != 0;
      if (flag && (this.e.text[this.e.CurLine - 1].flags & 1966080 /*0x1E0000*/) != 0)
        return true;
      if (!flag && (this.e.PfmtId[this.e.text[this.e.CurLine - 1].pfmt].flags & 12288 /*0x3000*/) != 0 && !this.e.EditPageHdrFtr)
      {
        int curLine = this.e.CurLine;
        while (curLine > 0 && (this.e.PfmtId[this.e.text[curLine - 1].pfmt].flags & 12288 /*0x3000*/) != 0)
          --curLine;
        if (curLine == 0)
          return true;
        this.e.CurLine = curLine - 1;
      }
      else
        --this.e.CurLine;
      this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
      if (this.e.text[this.e.CurLine].page == this.e.CurPage)
      {
        this.e.WrapFlag = 0;
        this.e.PaintFlag = 2;
      }
      this.PaintTer();
    }
    return true;
  }

  internal new bool PgmPageDn()
  {
    this.e.CursDirection = 1;
    int num1;
    while (true)
    {
      num1 = this.e.TerWinOrgY + this.e.TerWinHeight - 3 * this.e.TerFont[0].height / 2;
      if (num1 < this.e.TerWinOrgY)
        num1 = this.e.TerWinOrgY + this.e.TerWinHeight;
      if (this.e.CursHorzPos < 0)
        this.e.CursHorzPos = this.ColToUnits(this.e.CurCol, this.e.CurLine, 1024 /*0x0400*/);
      if (this.e.TerWinOrgY + this.e.TerWinHeight >= this.e.CurTextHeight)
      {
        if (this.e.CurPage < this.e.TotalPages - 1 && this.e.LastFramePage < this.e.TotalPages - 1)
        {
          this.e.CurPage = this.e.LastFramePage + 1;
          if (this.e.LastFramePage > this.e.FirstFramePage)
            this.e.TerWinOrgY -= this.e.FirstPageHeight;
          this.CreateFrames(false, this.e.CurPage - 1, this.e.CurPage);
        }
        else
          goto label_16;
      }
      else
        break;
    }
    this.e.TerWinOrgY = num1;
    if (this.e.TerWinOrgY + this.e.TerWinHeight >= this.e.CurPageHeight)
      this.e.TerWinOrgY = this.e.CurPageHeight - this.e.TerWinHeight;
    if (this.e.TerWinOrgY < 0)
      this.e.TerWinOrgY = 0;
    if (this.e.CurLineY < 0)
    {
      this.e.CurLineY = 0;
      goto label_35;
    }
    goto label_35;
label_16:
    if (this.e.CommandId != 601 && this.e.CommandId != 603)
    {
      this.e.TerWinOrgY = num1;
      if (this.e.TerWinOrgY + this.e.TerWinHeight >= this.e.CurPageHeight)
        this.e.TerWinOrgY = this.e.CurPageHeight - this.e.TerWinHeight - 1;
      if (this.e.TerWinOrgY < 0)
        this.e.TerWinOrgY = 0;
      this.e.CurLineY = 0;
    }
    else
    {
      if (this.e.CommandId == 601)
      {
        bool flag = false;
        if ((this.e.TerFlags3 & 32 /*0x20*/) != 0 && this.e.text[this.e.CurLine].cid != 0)
        {
          int LineNo = this.e.CurLine + 1;
          while (LineNo < this.e.TotalLines && this.e.text[LineNo].cid == this.e.text[this.e.CurLine].cid && !this.LineInfo(LineNo, 32 /*0x20*/))
            ++LineNo;
          this.e.CurLine = LineNo - 1;
          flag = true;
        }
        else if (this.LineToUnits(this.e.TotalLines - 1) >= this.e.TerWinOrgY)
        {
          this.e.CurLine = this.e.TotalLines - 1;
          flag = true;
        }
        if (flag)
        {
          this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
          if (this.e.CurCol < 0)
            this.e.CurCol = 0;
          this.PaintTer();
        }
      }
      return true;
    }
label_35:
    int y = this.e.TerWinOrgY + this.e.CurLineY;
    int index1 = -1;
    int num2 = 99999;
    int index2;
    for (index2 = 0; index2 < this.e.TotalFrames; ++index2)
    {
      if (!this.e.frame[index2].empty && (this.e.frame[index2].flags & 4096 /*0x1000*/) == 0 && ((this.e.TerFlags3 & 32 /*0x20*/) == 0 || this.e.frame[index2].CellId == this.e.text[this.e.CurLine].cid))
      {
        if (y >= this.e.frame[index2].y && y < this.e.frame[index2].y + this.e.frame[index2].height)
        {
          this.e.CurLine = this.UnitsToLine(this.e.CursHorzPos, y);
          break;
        }
        if (this.e.frame[index2].y >= y && this.e.frame[index2].y - y < num2)
        {
          index1 = index2;
          num2 = this.e.frame[index2].y - y;
        }
      }
    }
    if (index2 == this.e.TotalFrames && index1 >= 0)
      this.e.CurLine = this.e.frame[index1].PageFirstLine;
    if (index2 < this.e.TotalFrames)
    {
      if (index2 < this.e.FirstPage2Frame)
        this.e.CurPage = this.e.FirstFramePage;
      else
        this.e.CurPage = this.e.LastFramePage;
    }
    this.e.CurCol = this.UnitsToCol(this.e.CursHorzPos, this.e.CurLine);
    int units = this.LineToUnits(this.e.CurLine);
    if ((units < this.e.TerWinOrgY || units >= this.e.TerWinOrgY + this.e.TerWinHeight) && this.e.CommandId != 603)
      this.DisengageCaret();
    this.SetTerWindowOrg();
    this.e.UseTextMap = false;
    this.PaintTer();
    return true;
  }

  internal new bool PgmPageHorz(int pos)
  {
    int num = this.PageTextWidth();
    if (num >= this.e.TerWinWidth)
    {
      this.e.TerWinOrgX = (num - this.e.TerWinWidth) * pos / (1000 - this.e.HorThumbSize);
      if (this.e.TerWinOrgX > num - this.e.TerWinWidth)
        this.e.TerWinOrgX = num - this.e.TerWinWidth;
      this.SetTerWindowOrg();
      this.e.UseTextMap = false;
      this.PaintTer();
    }
    return true;
  }

  internal new bool PgmPageLeft(bool page)
  {
    int terWinOrgX = this.e.TerWinOrgX;
    if (page)
      this.e.TerWinOrgX -= this.e.TerWinWidth / 2;
    else
      this.e.TerWinOrgX -= this.e.TerWinWidth / 8;
    if (this.e.TerWinOrgX < 0)
      this.e.TerWinOrgX = 0;
    this.SetTerWindowOrg();
    if (page)
    {
      this.e.UseTextMap = false;
      this.PaintTer();
    }
    else
      this.PgmWinScroll(this.e.TerWinOrgX - terWinOrgX, 0);
    return true;
  }

  internal new bool PgmPageRight(bool page)
  {
    int terWinOrgX = this.e.TerWinOrgX;
    int num = this.PageTextWidth() + 1;
    if (num >= this.e.TerWinWidth)
    {
      if (page)
        this.e.TerWinOrgX += this.e.TerWinWidth / 2;
      else
        this.e.TerWinOrgX += this.e.TerWinWidth / 8;
      if (this.e.TerWinOrgX + this.e.TerWinWidth > num)
        this.e.TerWinOrgX = num - this.e.TerWinWidth;
      if (this.e.TerWinOrgX < 0)
        this.e.TerWinOrgX = 0;
      this.SetTerWindowOrg();
      if (page)
      {
        this.e.UseTextMap = false;
        this.PaintTer();
      }
      else
        this.PgmWinScroll(this.e.TerWinOrgX - terWinOrgX, 0);
    }
    return true;
  }

  internal new bool PgmPageUp()
  {
    int num1 = -1;
    int index1 = -1;
    this.e.CursDirection = 2;
    if (this.e.TerWinOrgY == 0 && this.e.CurPage == 0)
    {
      for (int index2 = this.e.TotalFrames - 1; index2 >= 0; --index2)
      {
        if (!this.e.frame[index2].empty && (this.e.frame[index2].flags & 4096 /*0x1000*/) == 0 && ((this.e.TerFlags3 & 32 /*0x20*/) == 0 || this.e.frame[index2].CellId == this.e.text[this.e.CurLine].cid))
        {
          if (num1 < 0)
            num1 = this.e.frame[index2].y;
          if (this.e.frame[index2].y <= num1)
          {
            num1 = this.e.frame[index2].y;
            index1 = index2;
          }
        }
      }
      if (index1 >= 0 && this.e.CurLine != this.e.frame[index1].PageFirstLine)
      {
        this.e.CurLine = this.e.frame[index1].PageFirstLine;
        this.e.CurCol = 0;
        this.e.CursDirection = 1;
        this.PaintTer();
      }
      return true;
    }
    int num2 = this.e.TerWinOrgY - this.e.TerWinHeight + 3 * this.e.TerFont[0].height / 2;
    if (this.e.CursHorzPos < 0)
      this.e.CursHorzPos = this.ColToUnits(this.e.CurCol, this.e.CurLine, 1024 /*0x0400*/);
    this.e.TerWinOrgY = num2;
    if (this.e.TerWinOrgY < 0)
    {
      if (this.e.FirstFramePage > 0)
      {
        if (this.e.CurPage > 0)
          --this.e.CurPage;
        --this.e.FirstFramePage;
        this.CreateFrames(false, this.e.FirstFramePage, this.e.FirstFramePage + 1);
        this.e.TerWinOrgY = this.e.FirstPageHeight + num2;
        if (this.e.TerWinOrgY < 0)
          this.e.TerWinOrgY = 0;
        if (this.e.CommandId == 602)
          this.e.CurLineY = this.e.TerWinHeight;
        else
          this.e.CurLineY = this.e.TerWinHeight / 2;
      }
      else
      {
        this.e.TerWinOrgY = 0;
        if (this.e.CurLineY < 0)
          this.e.CurLineY = 0;
      }
    }
    if (this.e.CurLineY > (this.e.TerWinHeight - 3 * this.e.TerFont[0].height) / 2)
      this.e.CurLineY = this.e.TerWinHeight - 3 * this.e.TerFont[0].height / 2;
    if (this.e.CurLineY < 0)
      this.e.CurLineY = 0;
    int y = this.e.TerWinOrgY + this.e.CurLineY;
    int index3 = -1;
    int index4;
    for (index4 = this.e.TotalFrames - 1; index4 >= 0; --index4)
    {
      if (!this.e.frame[index4].empty && (this.e.frame[index4].flags & 4096 /*0x1000*/) == 0 && ((this.e.TerFlags3 & 32 /*0x20*/) == 0 || this.e.frame[index4].CellId == this.e.text[this.e.CurLine].cid))
      {
        if (y >= this.e.frame[index4].y && y < this.e.frame[index4].y + this.e.frame[index4].height)
        {
          this.e.CurLine = this.UnitsToLine(this.e.CursHorzPos, y);
          break;
        }
        if (this.e.frame[index4].y <= y)
        {
          if (index3 != -1 && this.e.frame[index3].y < this.e.TerWinOrgY + this.e.TerWinHeight)
            this.e.CurLine = this.e.frame[index3].PageFirstLine;
          else
            this.e.CurLine = this.e.frame[index4].PageLastLine;
        }
        index3 = index4;
      }
    }
    if (index4 < 0 && index3 != -1 && this.e.frame[index3].y < this.e.TerWinOrgY + this.e.TerWinHeight)
      this.e.CurLine = this.e.frame[index3].PageFirstLine;
    this.e.CurCol = this.UnitsToCol(this.e.CursHorzPos, this.e.CurLine);
    int units = this.LineToUnits(this.e.CurLine);
    if (units < this.e.TerWinOrgY || units >= this.e.TerWinOrgY + this.e.TerWinHeight)
      this.DisengageCaret();
    this.SetTerWindowOrg();
    this.e.UseTextMap = false;
    this.PaintTer();
    return true;
  }

  internal new bool PgmPageVert(int pos)
  {
    int curPage = this.e.CurPage;
    bool flag1 = false;
    bool flag2 = (this.e.TerOpFlags & 33554432 /*0x02000000*/) != 0;
    int z = 1000 - this.e.VerThumbSize;
    int x = this.SumPageScrHeight(0, this.e.TotalPages) - this.e.TerWinHeight;
    int num1;
    int num2;
    if (pos >= z - 2)
    {
      num1 = this.e.TotalPages - 1;
      num2 = this.GetScrPageHt(num1) - this.e.TerWinHeight;
      if (num2 < 0)
        num2 = 0;
    }
    else
    {
      int num3 = this.MulDiv(x, pos, z);
      int num4 = 0;
      for (num1 = 0; num1 < this.e.TotalPages - 1; ++num1)
      {
        int scrPageHt = this.GetScrPageHt(num1);
        if (num4 + scrPageHt <= num3)
          num4 += scrPageHt;
        else
          break;
      }
      num2 = num3 - num4;
      if (num2 >= this.SumPageScrHeight(num1, 1))
        num2 = this.SumPageScrHeight(num1, 1) - 1;
    }
    if (num1 != this.e.CurPage && this.e.PagesShowing && !flag2 && (num1 < this.e.TotalPages - 1 || pos < z - 2))
      flag1 = true;
    this.e.CurPage = num1;
    if (curPage != this.e.CurPage)
      this.RefreshFrames(true);
    if (flag1)
    {
      if (this.e.CurPage == this.e.FirstFramePage)
        this.e.TerWinOrgY = 0;
      else
        this.e.TerWinOrgY = this.e.FirstPageHeight;
    }
    else
    {
      if (this.e.CurPage == this.e.FirstFramePage)
        this.e.TerWinOrgY = num2;
      else
        this.e.TerWinOrgY = this.e.FirstPageHeight + num2;
      if (this.e.TerWinOrgY >= this.e.CurPageHeight - this.e.TerWinHeight)
        this.e.TerWinOrgY = this.e.CurPageHeight - this.e.TerWinHeight;
      if (this.e.TerWinOrgY < 0)
        this.e.TerWinOrgY = 0;
    }
    this.SetTerWindowOrg();
    int y = this.e.TerWinOrgY;
    if (y >= this.e.CurTextHeight)
      y = this.e.CurTextHeight - 1;
    this.e.CurLine = this.UnitsToLine(0, y);
    this.e.CurCol = 0;
    this.e.UseTextMap = false;
    this.PaintTer();
    return true;
  }

  internal new bool PgmRight(bool HilightBegins)
  {
    char minValue = char.MinValue;
    this.e.CursDirection = 1;
    if (this.e.HilightType == 2 && this.e.text[this.e.HilightEndRow].cid > 0 && this.e.text[this.e.CurLine].cid > 0)
      return this.TblHilightRight(HilightBegins);
    if (this.e.CurCol != this.e.LineWidth - 1)
    {
      ++this.e.CurCol;
      if (this.e.text[this.e.CurLine].len > 0)
        minValue = this.e.text[this.e.CurLine].txt[this.e.text[this.e.CurLine].len - 1];
      if (this.e.CurCol >= this.e.text[this.e.CurLine].len && (this.e.CurLine + 1 < this.e.TotalLines || (int) minValue == (int) this.e.ParaChar))
      {
        if (this.e.CurLine + 1 == this.e.TotalLines)
        {
          --this.e.CurCol;
          return true;
        }
        if ((this.e.TerFlags3 & 32 /*0x20*/) != 0 && (this.e.text[this.e.CurLine].cid != this.e.text[this.e.CurLine + 1].cid || this.LineInfo(this.e.CurLine + 1, 32 /*0x20*/)))
        {
          --this.e.CurCol;
          return true;
        }
        bool flag = (this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].flags & 12288 /*0x3000*/) != 0;
        if (flag && (this.e.text[this.e.CurLine + 1].flags & 1966080 /*0x1E0000*/) != 0)
        {
          --this.e.CurCol;
          return true;
        }
        if (!flag && (this.e.PfmtId[this.e.text[this.e.CurLine + 1].pfmt].flags & 12288 /*0x3000*/) != 0 && !this.e.EditPageHdrFtr)
        {
          int curLine = this.e.CurLine;
          while (curLine + 1 < this.e.TotalLines && (this.e.PfmtId[this.e.text[curLine + 1].pfmt].flags & 12288 /*0x3000*/) != 0)
            ++curLine;
          if (curLine + 1 >= this.e.TotalLines)
            return true;
          this.e.CurLine = curLine + 1;
        }
        else
          ++this.e.CurLine;
        this.e.CurCol = 0;
        if (this.e.CurPage + 1 < this.e.TotalPages && this.e.text[this.e.CurLine].page == this.e.CurPage)
        {
          this.e.WrapFlag = 0;
          this.e.PaintFlag = 2;
        }
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

  internal new bool PgmUp()
  {
    bool flag1 = false;
    bool flag2 = false;
    int num1 = 0;
    if (this.e.CurLine <= 0)
      return true;
    int frame1;
    while ((frame1 = this.frm.GetFrame(this.e.CurLine)) >= 0)
    {
      this.e.CursDirection = 2;
      this.e.HilightAtCurPos = true;
      if (this.e.CursHorzPos < 0)
        this.e.CursHorzPos = this.ColToUnits(this.e.CurCol, this.e.CurLine, 1024 /*0x0400*/);
      int num2 = this.LineTextAngle(-frame1);
      if (this.e.CursHorzPos >= this.e.frame[frame1].x + this.e.frame[frame1].width)
      {
        int num3 = this.e.frame[frame1].x + this.e.frame[frame1].width - 1;
        num1 = this.e.CursHorzPos - num3;
        this.e.CursHorzPos = num3;
      }
      int y;
      if (this.e.CurLine > this.e.frame[frame1].PageFirstLine)
      {
        y = this.LineToUnits(this.e.CurLine) - 1 - this.GetObjSpcBef(this.e.CurLine, true);
        this.e.CurLineY = y;
        if (num2 > 0)
          goto label_26;
      }
      else
      {
        y = this.e.frame[frame1].y - 1;
        if (num2 > 0)
          return true;
      }
      bool flag3 = this.e.CurLine <= this.e.frame[frame1].PageFirstLine;
      int num4 = -1;
      for (int index = this.e.TotalFrames - 1; index >= 0; --index)
      {
        if (!this.e.frame[index].empty && (this.e.frame[index].flags & 4096 /*0x1000*/) == 0 && ((this.e.TerFlags3 & 32 /*0x20*/) == 0 || this.e.frame[index].CellId == this.e.text[this.e.CurLine].cid) && (!flag3 || frame1 != index) && this.e.CursHorzPos >= this.e.frame[index].x && this.e.CursHorzPos < this.e.frame[index].x + this.e.frame[index].width)
        {
          int num5 = this.e.frame[index].y + this.e.frame[index].height - 1;
          if (y >= this.e.frame[index].y && y <= num5)
            flag1 = true;
          if (num5 <= y && num5 > num4)
            num4 = num5;
        }
      }
      if (!flag1 && num4 < 0)
      {
        if (this.e.CurPage != this.e.FirstFramePage || flag2 || this.e.CurPage <= 0)
          return this.PgmPageUp();
        this.CreateFrames(false, this.e.CurPage - 1, this.e.CurPage);
        flag2 = true;
        continue;
      }
      if (!flag1)
        y = num4;
label_26:
      this.e.TerOpFlags |= 16384 /*0x4000*/;
      this.e.CurLine = this.UnitsToLine2(this.e.CursHorzPos, y, num2 > 0 ? frame1 : -1);
      this.e.TerOpFlags &= -16385;
      if (this.e.CurPage > this.e.FirstFramePage && y < this.e.FirstPageHeight)
        --this.e.CurPage;
      int frame2 = this.frm.GetFrame(this.e.CurLine);
      int scrFirstLine = this.e.frame[frame2].ScrFirstLine;
      if (this.e.frame[frame2].ScrY < this.e.TerWinOrgY)
        ++scrFirstLine;
      if (this.e.text[this.e.CurLine].tabw != null && (this.e.text[this.e.CurLine].tabw.type & 32 /*0x20*/) != 0)
        --this.e.CurLine;
      if ((this.e.frame[frame2].flags & 4) != 0 && this.e.CurLine >= scrFirstLine)
      {
        this.e.PaintFlag = 1;
        this.e.WrapFlag = 0;
      }
      else
      {
        if ((this.e.TerFlags3 & 4194304 /*0x400000*/) != 0)
          this.e.CurLineY = 0;
        else
          this.e.CurLineY = this.e.TerWinHeight / 2;
        this.e.TerWinOrgY = this.LineToUnits(this.e.CurLine) - this.e.CurLineY;
        if (this.e.TerWinOrgY < 0)
          this.e.TerWinOrgY = 0;
        this.SetTerWindowOrg();
        this.e.PaintFlag = 4;
        this.e.UseTextMap = false;
      }
      this.e.CurCol = this.UnitsToCol(this.e.CursHorzPos + num1, this.e.CurLine);
      if (this.e.HilightType == 2 && this.e.StretchHilight && this.e.text[this.e.CurLine].cid != 0)
      {
        int curLine = this.e.CurLine;
        while (this.e.CurLine - 1 >= 0 && !this.LineInfo(this.e.CurLine - 1, 48 /*0x30*/) && this.e.text[this.e.CurLine - 1].cid != 0)
          --this.e.CurLine;
        if (curLine != this.e.CurLine)
          this.e.CurCol = 0;
      }
      this.PaintTer();
      return true;
    }
    return true;
  }

  internal new bool PgmWinDown()
  {
    bool flag = false;
    int terWinOrgY = this.e.TerWinOrgY;
    int height = this.e.TerFont[0].height;
    if (this.e.MessageId == 522)
      height *= 3;
    int units = this.ColToUnits(this.e.CurCol, this.e.CurLine, 1024 /*0x0400*/);
    int firstPageHeight;
    for (; this.e.TerWinOrgY + this.e.TerWinHeight >= this.e.CurPageHeight; this.e.TerWinOrgY -= firstPageHeight)
    {
      if (this.e.LastFramePage >= this.e.TotalPages - 1)
        return true;
      firstPageHeight = this.e.FirstFramePage < this.e.LastFramePage ? this.e.FirstPageHeight : 0;
      this.CreateFrames(false, this.e.LastFramePage, this.e.LastFramePage + 1);
      flag = true;
    }
    this.e.TerWinOrgY += height;
    int curPageHeight = this.e.CurPageHeight;
    if (this.e.TerWinOrgY + this.e.TerWinHeight >= curPageHeight)
      this.e.TerWinOrgY = curPageHeight - this.e.TerWinHeight;
    if (this.e.TerWinOrgY < 0)
      this.e.TerWinOrgY = 0;
    if (this.e.CurLineY < 0)
      this.e.CurLineY = 0;
    if (this.e.TerWinOrgY + this.e.CurLineY > this.e.CurTextHeight)
      this.e.CurLineY = this.e.CurTextHeight - this.e.TerWinOrgY;
    if (this.e.CurLineY < 0)
      this.e.CurLineY = 0;
    this.e.CurLine = this.UnitsToLine(units, this.e.TerWinOrgY + this.e.CurLineY);
    this.e.CurCol = this.UnitsToCol(units, this.e.CurLine);
    if (this.e.TerWinOrgY != terWinOrgY | flag)
    {
      this.SetTerWindowOrg();
      this.PgmWinScroll(0, this.e.TerWinOrgY - terWinOrgY);
    }
    return true;
  }

  internal new bool PgmWinLeft()
  {
    int units1 = this.ColToUnits(this.e.CurCol, this.e.CurLine, 1024 /*0x0400*/);
    int units2 = this.LineToUnits(this.e.CurLine);
    int num1;
    if (this.e.CurCol > 0)
    {
      char[] txt = this.e.text[this.e.CurLine].txt;
      num1 = this.e.TerFont[(int) this.OpenCfmt(this.e.CurLine)[this.e.CurCol - 1]].CharWidth[(int) (byte) txt[this.e.CurCol - 1]];
      this.CloseCfmt(this.e.CurLine);
    }
    else
      num1 = this.fnt.LwrCharWidth(0, true, ' ');
    this.e.TerWinOrgX -= num1;
    if (this.e.TerWinOrgX < 0)
      this.e.TerWinOrgX = 0;
    int num2 = units1 - num1;
    if (num2 < 0)
      num2 = 0;
    this.e.CurLine = this.UnitsToLine(num2, units2);
    this.e.CurCol = this.UnitsToCol(num2, this.e.CurLine);
    this.SetTerWindowOrg();
    this.e.UseTextMap = false;
    this.PaintTer();
    return true;
  }

  internal new bool PgmWinRight()
  {
    int units1 = this.ColToUnits(this.e.CurCol, this.e.CurLine, 1024 /*0x0400*/);
    int units2 = this.LineToUnits(this.e.CurLine);
    int num1;
    if (this.e.CurCol < this.e.text[this.e.CurLine].len)
    {
      char[] txt = this.e.text[this.e.CurLine].txt;
      num1 = this.e.TerFont[(int) this.OpenCfmt(this.e.CurLine)[this.e.CurCol]].CharWidth[(int) (byte) txt[this.e.CurCol]];
      this.CloseCfmt(this.e.CurLine);
    }
    else
      num1 = this.fnt.LwrCharWidth(0, true, ' ');
    this.e.TerWinOrgX += num1;
    int num2 = this.PageTextWidth();
    if (this.e.TerWinOrgX + this.e.TerWinWidth > num2)
      this.e.TerWinOrgX = num2 - this.e.TerWinWidth;
    if (this.e.TerWinOrgX < 0)
      this.e.TerWinOrgX = 0;
    int num3 = units1 + num1;
    if (num3 > this.e.TerWinOrgX + this.e.TerWinWidth)
      num3 = this.e.TerWinOrgX + this.e.TerWinWidth;
    this.e.CurLine = this.UnitsToLine(num3, units2);
    this.e.CurCol = this.UnitsToCol(num3, this.e.CurLine);
    this.SetTerWindowOrg();
    this.e.UseTextMap = false;
    this.PaintTer();
    return true;
  }

  internal new bool PgmWinScroll(int ScrollX, int ScrollY)
  {
    this.e.UseTextMap = false;
    this.e.PictureClicked = this.e.FrameClicked = false;
    this.PaintTer();
    return true;
  }

  internal new bool PgmWinUp()
  {
    int terWinOrgY = this.e.TerWinOrgY;
    int height = this.e.TerFont[0].height;
    if (this.e.MessageId == 522)
      height *= 3;
    int units = this.ColToUnits(this.e.CurCol, this.e.CurLine, 1024 /*0x0400*/);
    if (this.e.TerWinOrgY != 0 || this.e.CurPage != 0)
    {
      if (this.e.FirstFramePage > 0 && height > this.e.TerWinOrgY)
      {
        this.CreateFrames(false, this.e.FirstFramePage - 1, this.e.FirstFramePage);
        this.e.TerWinOrgY += this.e.FirstPageHeight;
        if (this.e.TerWinOrgY + this.e.TerWinHeight - height > this.e.CurTextHeight)
          this.e.TerWinOrgY = this.e.CurTextHeight - this.e.TerWinHeight + height;
      }
      this.e.TerWinOrgY -= height;
      if (this.e.TerWinOrgY < 0)
        this.e.TerWinOrgY = 0;
      if (this.e.CurLineY < 0)
        this.e.CurLineY = 0;
      if (this.e.CurLineY > this.e.TerWinHeight)
        this.e.CurLineY = this.e.TerWinHeight;
      if (this.e.TerWinOrgY + this.e.CurLineY > this.e.CurTextHeight)
        this.e.CurLineY = this.e.CurTextHeight - this.e.TerWinOrgY;
      if (this.e.CurLineY < 0)
        this.e.CurLineY = 0;
      this.e.CurLine = this.UnitsToLine(units, this.e.TerWinOrgY + this.e.CurLineY);
      this.e.CurCol = this.UnitsToCol(units, this.e.CurLine);
      this.SetTerWindowOrg();
      this.PgmWinScroll(0, this.e.TerWinOrgY - terWinOrgY);
    }
    return true;
  }

  internal new bool PosWatermarkFrame(int PageNo)
  {
    int topSect = this.e.PageInfo[PageNo].TopSect;
    int twips1 = (int) this.InchesToTwips((double) this.e.TerSect1[topSect].PgWidth);
    int twips2 = (int) this.InchesToTwips((double) this.e.TerSect1[topSect].PgHeight);
    int wmParaFid = this.e.WmParaFID;
    if (this.e.WmParaFID > 0)
    {
      int pict = this.e.ParaFrame[wmParaFid].pict;
      if (pict > 0)
      {
        int y = 100;
        if (this.e.ParaFrame[wmParaFid].height > twips2)
        {
          int num = this.MulDiv(twips2, 100, this.e.ParaFrame[wmParaFid].height);
          if (num < y)
            y = num;
        }
        if (this.e.ParaFrame[wmParaFid].width > twips1)
        {
          int num = this.MulDiv(twips1, 100, this.e.ParaFrame[wmParaFid].width);
          if (num < y)
            y = num;
        }
        if (y < 100)
        {
          this.e.ParaFrame[wmParaFid].height = this.MulDiv(this.e.ParaFrame[wmParaFid].height, y, 100);
          this.e.ParaFrame[wmParaFid].width = this.MulDiv(this.e.ParaFrame[wmParaFid].width, y, 100);
          this.e.TerFont[pict].PictHeight = this.e.ParaFrame[wmParaFid].height;
          this.e.TerFont[pict].PictWidth = this.e.ParaFrame[wmParaFid].width;
          this.SetPictSize(pict, this.TwipsToScrY(this.e.TerFont[pict].PictHeight), this.TwipsToScrX(this.e.TerFont[pict].PictWidth), true);
          this.XlateSizeForPrt(pict);
        }
      }
      int num1;
      this.e.ParaFrame[wmParaFid].ParaY = num1 = (twips2 - this.e.ParaFrame[wmParaFid].height) / 2;
      this.e.ParaFrame[wmParaFid].y = num1;
      this.e.ParaFrame[wmParaFid].x = (twips1 - this.e.ParaFrame[wmParaFid].width) / 2;
      this.e.ParaFrame[wmParaFid].x -= (int) this.InchesToTwips((double) this.e.TerSect[topSect].LeftMargin);
      tc.ResetUintFlag(ref this.e.ParaFrame[wmParaFid].flags, 64 /*0x40*/);
      this.e.ParaFrame[wmParaFid].flags |= 32 /*0x20*/;
    }
    return true;
  }

  /// <summary>Разбивка по страницам</summary>
  internal new bool Repaginate(bool yield, bool selective, int LastPage, bool repaint)
  {
    if (this.e.TotalLines == 0)
      return true;
    Cursor cursor = (Cursor) null;
    int pHilightBeg = 0;
    int pHilightEnd = 0;
    int curPage = this.e.CurPage;
    bool FullRepage = false;
    bool flag = true;
    bool pBegHilightAtLineEnd = false;
    bool pEndHilightAtLineEnd = false;
    bool pSelectAll = false;
    int totalPages = this.e.TotalPages;
    int abs = 0;
    if (tc.DebugMode)
      this.misc.dm(nameof (Repaginate));
    if (this.e.FullRenderMode)
    {
      if (!this.e.TerArg.PrintView)
        return false;
      if (this.e.TerArg.FittedView && !yield && !selective && LastPage == 0)
        this.draw.GetWinDimension();
      if (!yield && (this.e.TerFlags & 512 /*0x0200*/) == 0)
        cursor = Cursors.WaitCursor;
      if (this.e.RepageBeginLine < 0 || (this.e.TerOpFlags2 & 131072 /*0x020000*/) != 0)
        this.e.RepageBeginLine = 0;
      if (yield && this.e.TerArg.modified == this.e.PageModifyCount || yield && this.e.RepageBeginLine >= this.e.TotalLines)
        return false;
      if (yield && this.MessagePending())
      {
        this.e.RepagePending = true;
        return false;
      }
      if (!yield)
        this.CreateToc();
      if (!yield || (this.e.TerOpFlags & 1073741824 /*0x40000000*/) != 0)
        this.CreateEndnote();
    }
    if (!this.e.IsPlaneText && !yield && !this.e.InRtfRead || (this.e.TerOpFlags2 & 4) != 0)
      this.frm.ReposPictFrames();
    if (this.e.FullRenderMode)
    {
      abs = this.pos.RowColToAbs(this.e.CurLine, this.e.CurCol, true, false);
      this.wrp.SaveWrapHilight(out pHilightBeg, out pHilightEnd, out pBegHilightAtLineEnd, out pEndHilightAtLineEnd, out pSelectAll);
    }
    this.e.PrevTotalPages = this.e.TotalPages;
    int modified = this.e.TerArg.modified;
    if (!yield && !selective && LastPage == 0)
      FullRepage = true;
    if (this.e.TotalLines == 1)
      FullRepage = true;
    if (FullRepage)
      yield = false;
    int index1 = 0;
    if (yield)
    {
      if (this.e.RepageBeginLine >= this.e.TotalLines)
        this.e.RepageBeginLine = this.e.TotalLines - 1;
      if (this.e.RepageBeginLine != 0)
        index1 = this.PageFromLine(this.e.RepageBeginLine, -1);
      int cid = this.e.text[this.e.RepageBeginLine].cid;
      if (index1 > 0 && cid > 0)
        index1 = this.e.TableAux[this.e.cell[cid].row].FirstPage;
      int index2 = 0;
      while (index2 < this.e.TotalPages && this.e.PageInfo[index2].FirstLine < this.e.TotalLines)
        ++index2;
      if (index2 < this.e.TotalPages)
        index1 = 0;
      if (index1 > 0 && this.e.RepageBeginLine == this.e.PageInfo[index1].FirstLine)
        --index1;
    }
    else if (selective)
    {
      int num = this.e.CurLine == 0 ? 0 : this.PageFromLine(this.e.CurLine, -1);
      int index3 = num;
      while (index3 > 0 && (this.e.PageInfo[index3].flags & 1) == 0)
        --index3;
      index1 = index3 - 1;
      if (index1 < 0)
        index1 = 0;
      int index4 = num + 1;
      while (index4 < this.e.TotalPages && (this.e.PageInfo[index4].flags & 1) == 0)
        ++index4;
      LastPage = index4 - 1;
    }
    this.e.repaginating = true;
    this.e.RepagePending = false;
    int firstFramePage = this.e.FirstFramePage;
    int lastFramePage = this.e.LastFramePage;
    if (this.e.FullRenderMode)
      this.sec.SetSectPageSize();
    int index5 = 0;
    if ((this.e.TerSect[index5].flags & 2) != 0)
      this.e.PageInfo[0].DispNbr = (int) this.e.TerSect[index5].FirstPageNo;
    else
      this.e.PageInfo[0].DispNbr = 1;
    if (FullRepage)
      this.e.TerSect1[index5].PrevSect = -1;
    if (index1 == 0)
    {
      for (int index6 = 0; index6 < this.e.TotalSects; ++index6)
      {
        if (FullRepage || index6 == index5)
        {
          if (this.e.FullRenderMode)
          {
            this.e.TerSect1[index6].hdr.height = (int) ((double) this.e.UnitResY * ((double) this.e.TerSect[index6].TopMargin - (double) this.e.TerSect[index6].HdrMargin));
            this.e.TerSect1[index6].ftr.height = (int) ((double) this.e.UnitResY * ((double) this.e.TerSect[index6].BotMargin - (double) this.e.TerSect[index6].FtrMargin));
            if (this.e.TerSect1[index6].hdr.height < 0)
              this.e.TerSect1[index6].hdr.height = 0;
            if (this.e.TerSect1[index6].ftr.height < 0)
              this.e.TerSect1[index6].ftr.height = 0;
          }
          else
          {
            this.e.TerSect1[index6].hdr.height = 0;
            this.e.TerSect1[index6].ftr.height = 0;
          }
          this.e.TerSect1[index6].ftr.TextHeight = 0;
        }
      }
    }
    if (FullRepage)
    {
      for (int index7 = 1; index7 < this.e.TotalParaFrames; ++index7)
      {
        this.e.ParaFrame[index7].InUse = false;
        this.e.ParaFrame[index7].flags &= -98305;
      }
    }
    else
    {
      int index8 = 1;
      while (index8 < this.e.TotalParaFrames && (this.e.ParaFrame[index8].flags & 896) != 0)
        ++index8;
      if (index8 < this.e.TotalParaFrames)
      {
        for (int index9 = 1; index9 < this.e.TotalParaFrames; ++index9)
          this.e.ParaFrame[index9].InUse = false;
        for (int index10 = 0; index10 < this.e.TotalLines; ++index10)
        {
          if (this.e.text[index10].fid != 0)
            this.e.ParaFrame[this.e.text[index10].fid].InUse = true;
          if ((this.e.text[index10].flags & 16384 /*0x4000*/) != 0)
          {
            ushort[] fmt = this.e.text[index10].fmt;
            if (fmt != null)
            {
              for (int index11 = 0; index11 < fmt.Length; ++index11)
              {
                if (this.IsFramePict((int) fmt[index11]))
                {
                  int paraFid = this.e.TerFont[(int) fmt[index11]].ParaFID;
                  this.e.ParaFrame[paraFid].InUse = true;
                  if (this.e.text[index10].page >= index1)
                    tc.ResetUintFlag(ref this.e.ParaFrame[paraFid].flags, 98304 /*0x018000*/);
                }
              }
            }
          }
        }
      }
    }
    this.e.LastWrappedLine = -1;
    int PageNo = index1;
    this.e.LastPageCreated = false;
    while (!this.e.LastPageCreated && (LastPage == 0 || PageNo <= LastPage))
    {
      this.CreateOnePage(PageNo, FullRepage);
      if (!this.e.FullRenderMode)
        this.e.LastPageCreated = true;
      ++PageNo;
      COp.MSG msg;
      if (yield && this.PeekMessage(out msg, this.e.hTerWnd, 512 /*0x0200*/, 512 /*0x0200*/, 1) && this.e.IgnoreMouseMove)
      {
        if (this.e.ShowHyperlinkCursor)
        {
          try
          {
            this.TerSetCursorShape(msg.lParam.ToInt32(), false);
          }
          catch (Exception ex)
          {
          }
        }
      }
      if (yield)
      {
        if (this.e.TotalLines < 5000)
        {
          if (this.MessagePending())
          {
            this.e.RepagePending = true;
            break;
          }
        }
        else
        {
          do
            ;
          while (this.PeekMessage(out msg, this.e.hTerWnd, 275, 275, 1));
          if (this.PeekMessage(out msg, this.e.hTerWnd, 258, 258, 2) || this.PeekMessage(out msg, this.e.hTerWnd, 256 /*0x0100*/, 256 /*0x0100*/, 2) || this.PeekMessage(out msg, this.e.hTerWnd, 132, 132, 2) || this.PeekMessage(out msg, this.e.hTerWnd, 32 /*0x20*/, 32 /*0x20*/, 2) || this.PeekMessage(out msg, this.e.hTerWnd, 160 /*0xA0*/, 160 /*0xA0*/, 2) || this.PeekMessage(out msg, IntPtr.Zero, 512 /*0x0200*/, 522, 2))
          {
            this.e.RepagePending = true;
            flag = false;
            break;
          }
        }
      }
    }
    if (this.e.FullRenderMode)
      this.AbsToRowCol(abs, 'C');
    if (this.e.BeginLine >= this.e.TotalLines)
      this.e.BeginLine = this.e.TotalLines - 1;
    if (this.e.BeginLine > this.e.CurLine)
      this.e.BeginLine = this.e.CurLine;
    this.e.CurRow = this.e.CurLine - this.e.BeginLine;
    if (this.e.CurRow >= this.e.WinHeight)
    {
      this.e.CurRow = this.e.WinHeight - 1;
      this.e.BeginLine = this.e.CurLine - this.e.CurRow;
    }
    if (this.e.FullRenderMode)
    {
      this.wrp.RestoreWrapHilight(pHilightBeg, pHilightEnd, pBegHilightAtLineEnd, pEndHilightAtLineEnd, pSelectAll);
      this.sec.UpdateToc();
    }
    if (!this.e.IsPlaneText)
      this.tbl.RepairTable();
    if (this.e.FullRenderMode)
      this.sec.RecreateSections();
    if (this.e.LastPageCreated)
    {
      this.e.TerArg.modified = modified;
      this.e.PageModifyCount = modified;
      this.e.RepageBeginLine = this.e.TotalLines;
      if ((this.e.TerOpFlags & 1073741824 /*0x40000000*/) != 0)
        this.e.TerOpFlags &= -1073741825 /*0xBFFFFFFF*/;
    }
    else
    {
      this.e.TerArg.modified = modified;
      if (PageNo == this.e.TotalPages)
        this.e.RepageBeginLine = this.e.TotalLines;
      else
        this.e.RepageBeginLine = this.e.PageInfo[PageNo].FirstLine;
    }
    if (cursor != (Cursor) null)
      this.e.Cursor = cursor;
    this.e.CurPage = curPage;
    if (this.e.CurPage >= this.e.TotalPages)
      this.e.CurPage = this.e.TotalPages - 1;
    if (this.e.CurLine != 0 && this.e.FullRenderMode)
      this.e.CurPage = this.PageFromLine(this.e.CurLine, this.e.CurPage);
    else
      this.e.CurPage = 0;
    this.e.PrevTotalPages = this.e.TotalPages;
    this.e.repaginating = false;
    this.e.TerOpFlags2 &= -131073;
    if (this.e.CurPage >= firstFramePage && this.e.CurPage <= lastFramePage)
      this.CreateFrames(false, firstFramePage, lastFramePage);
    else if (curPage == this.e.CurPage - 1)
      this.CreateFrames(false, this.e.CurPage - 1, this.e.CurPage);
    else
      this.CreateFrames(false, this.e.CurPage, this.e.CurPage);
    if (totalPages == 1 && this.PageResized())
      return true;
    if (repaint)
      this.PaintTer();
    if (!this.e.InRtfRead && totalPages != this.e.TotalPages)
      this.e.FirePageCount((object) this.e);
    return flag;
  }

  internal new bool RequestPagination(bool full)
  {
    if (full)
    {
      this.e.RepageBeginLine = 0;
      this.e.TerOpFlags2 |= 131072 /*0x020000*/;
    }
    this.e.PageModifyCount = this.e.TerArg.modified - 1;
    if (this.e.UseWin)
      this.PostMessage(this.e.hTerWnd, 1034, 0, 0);
    return true;
  }

  internal bool ResetLinePictPos(int LineNo)
  {
    bool flag = false;
    if ((this.e.text[LineNo].flags & 16384 /*0x4000*/) == 0)
      return false;
    ushort[] fmt = this.e.text[LineNo].fmt;
    if (fmt != null)
    {
      for (int index1 = 0; index1 < fmt.Length; ++index1)
      {
        ushort index2 = fmt[index1];
        if (this.e.TerFont[(int) index2].InUse && (this.e.TerFont[(int) index2].style & 128 /*0x80*/) != 0 && this.e.TerFont[(int) index2].FrameType != 0 && this.e.TerFont[(int) index2].ParaFID > 0)
          this.e.ParaFrame[this.e.TerFont[(int) index2].ParaFID].flags &= -32769;
      }
    }
    return flag;
  }

  internal new bool SetPageFromY(int y)
  {
    if (this.e.TerArg.PageMode)
    {
      this.e.CurPage = this.e.FirstFramePage;
      if (y > this.e.FirstPageHeight)
        ++this.e.CurPage;
      if (this.e.CurPage >= this.e.TotalPages)
        --this.e.CurPage;
    }
    return true;
  }

  internal bool SetPictPageBreak(int LineNo, int PageNo)
  {
    if ((this.e.text[LineNo].flags & 16384 /*0x4000*/) != 0)
    {
      ushort[] numArray = this.OpenCfmt(LineNo);
      for (int index1 = 0; index1 < this.e.text[LineNo].len; ++index1)
      {
        ushort index2 = numArray[index1];
        if (this.e.TerFont[(int) index2].InUse && (this.e.TerFont[(int) index2].style & 128 /*0x80*/) != 0 && this.e.TerFont[(int) index2].FrameType != 0 && this.e.TerFont[(int) index2].ParaFID > 0)
        {
          int paraFid = this.e.TerFont[(int) index2].ParaFID;
          this.e.ParaFrame[paraFid].PageNo = PageNo;
          this.e.ParaFrame[paraFid].flags |= 65536 /*0x010000*/;
          tc.ResetUintFlag(ref this.e.ParaFrame[paraFid].flags, 32768 /*0x8000*/);
        }
      }
      this.CloseCfmt(LineNo);
    }
    return false;
  }

  internal bool SkipCellLine(
    int l,
    int level,
    int PrevLevel,
    bool[] InPartBotRow,
    bool[] InPartTopRow,
    int PageNo,
    int[] SkipCell,
    ref int SkipLevel,
    int[] CurCell,
    int[] CurRowId,
    ref int PageColHeight,
    ref int CurPgHeight,
    int[] TableX,
    int[] PrevCellsWidth,
    ref bool EndPage,
    int[] CellHeight,
    ref bool EndCell)
  {
    bool flag1;
    EndCell = flag1 = false;
    EndPage = flag1;
    if (InPartTopRow[PrevLevel] && l > 0 && this.TableLevel(l - 1) == PrevLevel && this.LineInfo(l - 1, 32 /*0x20*/))
    {
      if (InPartBotRow[PrevLevel])
      {
        if (PrevLevel == 0)
        {
          EndPage = true;
          return false;
        }
        SkipLevel = PrevLevel;
      }
      int topRowHt = this.e.TableAux[CurRowId[PrevLevel]].TopRowHt;
      this.e.TableAux[CurRowId[PrevLevel]].LastPage = PageNo;
      PageColHeight += topRowHt;
      CurPgHeight += topRowHt;
      if (PrevLevel > 0)
      {
        int[] numArray;
        IntPtr index;
        (numArray = CellHeight)[(int) (index = (IntPtr) (PrevLevel - 1))] = numArray[(int) index] + topRowHt;
      }
      InPartTopRow[PrevLevel] = false;
      CurRowId[PrevLevel] = 0;
      CurCell[PrevLevel] = 0;
    }
    if (SkipLevel > 0 && level >= SkipLevel)
    {
      if (this.e.text[l].page <= PageNo)
        this.e.text[l].page = PageNo + 1;
      return true;
    }
    SkipLevel = 0;
    int cid = this.e.text[l].cid;
    bool flag2 = false;
    if (level > PrevLevel)
    {
      int index1 = cid;
      if (l > 0 && this.e.text[l - 1].cid == this.LevelCell(PrevLevel, l))
      {
        for (int index2 = level; index2 > PrevLevel; --index2)
        {
          SkipCell[index2] = SkipCell[PrevLevel] <= 0 ? 0 : index1;
          CellHeight[index2] = 0;
          PrevCellsWidth[index2] = 0;
          index1 = this.e.cell[index1].ParentCell;
        }
        for (int index3 = PrevLevel + 1; index3 <= level; ++index3)
          TableX[index3] = TableX[index3 - 1] + this.e.TableRow[CurRowId[index3 - 1]].CurIndent + PrevCellsWidth[index3 - 1];
        if (SkipCell[level] != 0)
          flag2 = true;
      }
    }
    else if (level == PrevLevel && SkipCell[level] > 0)
    {
      if (cid == 0 || this.e.cell[SkipCell[level]].row != this.e.cell[cid].row)
      {
        if (level == 0)
        {
          EndPage = true;
          return false;
        }
        SkipLevel = level;
      }
      if (SkipLevel == 0)
      {
        if (cid == SkipCell[level])
          flag2 = true;
        if (cid != SkipCell[level])
          SkipCell[level] = 0;
        else if (this.LineInfo(l, 16 /*0x10*/) && !this.LineInfo(l + 1, 32 /*0x20*/))
          SkipCell[level] = 0;
      }
    }
    else if (level < PrevLevel)
    {
      for (int index = level + 1; index <= PrevLevel; ++index)
        SkipCell[index] = 0;
      if (SkipCell[level] > 0)
        flag2 = EndCell = true;
    }
    if (flag2 || SkipLevel != 0)
    {
      if (this.e.text[l].page <= PageNo)
        this.e.text[l].page = PageNo + 1;
      return true;
    }
    return InPartTopRow[0] && SkipCell[level] == 0 && this.e.text[l].page < PageNo && cid > 0;
  }

  internal new int SumPageScrHeight(int StartPage, int count)
  {
    int num = 0;
    if (StartPage < 0)
      count = 0;
    if (StartPage >= this.e.TotalPages)
      StartPage = this.e.TotalPages - 1;
    for (int PageNo = StartPage; PageNo < StartPage + count; ++PageNo)
      num += this.GetScrPageHt(PageNo);
    return num;
  }

  internal int TerGetDispPageNo(int page)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    return page < 0 || page >= this.e.TotalPages ? 0 : this.e.PageInfo[page].DispNbr;
  }

  internal bool TerGetPageCount(out int pTotalPages, out int pCurPage)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    pTotalPages = 1;
    pCurPage = 0;
    if (!this.e.TerArg.PageMode)
      return false;
    pTotalPages = this.e.TotalPages;
    pCurPage = this.e.CurPage;
    return true;
  }

  internal int TerGetPageFirstLine(int page)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (page < 0)
      page = 0;
    if (page > this.e.TotalPages - 1)
      page = this.e.TotalPages - 1;
    return this.e.PageInfo[page].FirstLine;
  }

  internal bool TerGetPageOffset(
    int PageNo,
    int rel,
    out int x,
    out int y,
    out int width,
    out int height)
  {
    bool pageOffset = true;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int num1;
    height = num1 = 0;
    int num2;
    width = num2 = num1;
    int num3;
    y = num3 = num2;
    x = num3;
    if (!this.e.TerArg.PageMode || rel != 1 && rel != 2)
      return false;
    if (PageNo < 0)
      PageNo = this.e.CurPage;
    if (PageNo < this.e.FirstFramePage || PageNo > this.e.LastFramePage)
      return false;
    y = 0;
    if (PageNo > this.e.FirstFramePage)
      y += this.e.FirstPageHeight;
    if (this.e.BorderShowing)
      y += this.UnitToScrY(this.e.TopBorderHeight);
    y -= this.e.TerWinOrgY;
    if (y > this.e.TerWinHeight)
      pageOffset = false;
    if (y < 0 & pageOffset)
    {
      height = this.e.FirstPageHeight;
      if (this.e.BorderShowing)
        height -= this.UnitToScrY(this.e.TopBorderHeight + this.e.BotBorderHeight);
      if (y + height < 0)
        pageOffset = false;
    }
    if (rel == 1)
      y += this.e.TerWinRect.top;
    x = 0;
    if (this.e.BorderShowing)
      x += this.UnitToScrX(this.e.LeftBorderWidth);
    x -= this.e.TerWinOrgX;
    if (x > this.e.TerWinWidth)
      pageOffset = false;
    if (rel == 1)
      x += this.e.TerWinRect.left;
    height = PageNo != this.e.FirstFramePage ? this.e.frame[this.e.TotalFrames - 1].y + this.e.frame[this.e.TotalFrames - 1].height - this.e.frame[this.e.FirstPage2Frame].y : this.e.FirstPageHeight;
    if (this.e.BorderShowing)
    {
      height -= this.UnitToScrY(this.e.TopBorderHeight);
      height -= PageNo + 1 == this.e.TotalPages ? this.UnitToScrY(this.e.TopBorderHeight) : this.UnitToScrY(this.e.BotBorderHeight);
    }
    int topSect = this.e.PageInfo[PageNo].TopSect;
    width = (int) ((double) this.e.TerSect1[topSect].PgWidth * (double) this.e.ScrResX);
    return pageOffset;
  }

  internal bool TerGetPageOrient(int page)
  {
    return this.TerGetPageOrientEx(page, out tc.SkipInt, out tc.SkipInt);
  }

  internal bool TerGetPageOrient2(
    int page,
    out int pWidth,
    out int pHeight,
    out int pHiddenX,
    out int pHiddenY)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (page < 0)
      page = 0;
    if (page >= this.e.TotalPages)
      page = this.e.TotalPages - 1;
    int pageSect = this.e.TerGetPageSect(page);
    pWidth = (int) ((double) this.e.TerSect1[pageSect].PgWidth * 1440.0);
    pHeight = (int) ((double) this.e.TerSect1[pageSect].PgHeight * 1440.0);
    pHiddenX = this.UnitToTwipsX(this.e.TerSect1[pageSect].HiddenX);
    pHiddenY = this.UnitToTwipsY(this.e.TerSect1[pageSect].HiddenY);
    return this.e.TerSect[pageSect].IsPortrait;
  }

  internal bool TerGetPageOrientEx(int page, out int pWidth, out int pHeight)
  {
    return this.TerGetPageOrient2(page, out pWidth, out pHeight, out tc.SkipInt, out tc.SkipInt);
  }

  internal bool TerGetPagePos(out int pPage, out int pOff)
  {
    pPage = 0;
    pOff = 0;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.PageMode)
      return false;
    int firstFramePage = this.e.FirstFramePage;
    int x = this.e.TerWinOrgY;
    if (this.e.TerWinOrgY > this.e.FirstPageHeight && firstFramePage + 1 < this.e.TotalPages)
    {
      ++firstFramePage;
      x -= this.e.FirstPageHeight;
    }
    if (x < 0)
      x = 0;
    int twipsY = this.ScrToTwipsY(x);
    pPage = firstFramePage;
    pOff = twipsY;
    return true;
  }

  internal int TerGetPageSect(int pg)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    return pg < 0 || pg >= this.e.TotalPages ? 0 : this.GetSection(this.e.PageInfo[pg].FirstLine);
  }

  internal int TerGetTextHeight()
  {
    int x1 = 0;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.e.TerArg.PageMode)
    {
      int x2 = 0;
      for (int index = 0; index < this.e.TotalPages; ++index)
        x2 += this.e.PageInfo[index].BodyTextHt;
      return this.UnitToTwipsY(x2);
    }
    for (int lin = 0; lin < this.e.TotalLines; ++lin)
      x1 += this.ScrLineHeight(lin, false);
    return this.ScrToTwipsY(x1);
  }

  internal int TerGetFirstPageTextHeight()
  {
    int x = 0;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.e.TerArg.PageMode)
      return this.UnitToTwipsY(this.e.PageInfo[0].BodyTextHt);
    for (int lin = 0; lin < this.e.TotalLines; ++lin)
      x += this.ScrLineHeight(lin, false);
    return this.ScrToTwipsY(x);
  }

  internal bool TerInsertPageRef(
    string bookmark,
    bool IsHyperlink,
    bool IsAlphabetic,
    bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.PageMode || !this.CanInsert(this.e.CurLine, this.e.CurCol) || !this.IsValidBookmark(bookmark, true))
      return false;
    string str = bookmark + " ";
    if (IsHyperlink)
      str += "\\h ";
    if (IsAlphabetic)
      str += "\\* alphabetic ";
    string FieldCode = str + "\\* MERGEFORMAT ";
    this.e.InputFontId = this.e.TerGetFieldFont(this.GetEffectiveCfmt(), 16 /*0x10*/, FieldCode);
    this.e.InsertTerText("1", repaint);
    this.e.InputFontId = -1;
    ++this.e.TerArg.modified;
    return true;
  }

  internal bool TerPageBreak(bool repaint)
  {
    bool flag = false;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.CheckLineLimit(this.e.TotalLines + 1))
    {
      if (this.e.text[this.e.CurLine].cid > 0 || (this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].flags & 12288 /*0x3000*/) != 0)
        return false;
      if (this.e.CurCol > 0 && this.SplitLine(this.e.CurLine, this.e.CurCol, 0))
      {
        ++this.e.CurLine;
        this.e.CurCol = 0;
        flag = true;
      }
      int effectiveCfmt = this.GetEffectiveCfmt();
      int curLine = this.e.CurLine;
      if (((this.e.CurLine == 0 ? 1 : ((this.e.text[this.e.CurLine - 1].flags & 1) == 0 ? 1 : 0)) | (flag ? 1 : 0)) != 0 && (this.e.TerOpFlags & 131072 /*0x020000*/) == 0)
      {
        this.InsertMarkerLine(this.e.CurLine, this.e.ParaChar, effectiveCfmt, this.e.text[this.e.CurLine].pfmt, 0, 0);
        ++this.e.CurLine;
      }
      this.InsertMarkerLine(this.e.CurLine, '\f', effectiveCfmt, -1, 0, -1);
      this.SaveUndo(curLine, 0, this.e.CurLine, 0, 'I');
      ++this.e.CurLine;
      if (this.e.CurLine - this.e.BeginLine >= this.e.WinHeight || this.e.CurLine - this.e.BeginLine < 0)
      {
        this.e.BeginLine = this.e.CurLine - this.e.WinHeight / 2;
        if (this.e.BeginLine < 0)
          this.e.BeginLine = 0;
      }
      this.e.CurRow = this.e.CurLine - this.e.BeginLine;
      this.e.HilightType = 0;
      ++this.e.TerArg.modified;
      if (repaint)
      {
        this.Repaginate(false, true, 0, true);
        this.PaintTer();
      }
    }
    return true;
  }

  internal int TerPageFromLine(int LineNo)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    return this.PageFromLine(LineNo, -1);
  }

  internal bool TerPosPage(int NewPage) => this.TerSetPagePos(NewPage, 0);

  internal bool TerRepaginate(bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if ((this.e.TerFlags4 & 16 /*0x10*/) == 0)
    {
      if (this.e.TerArg.PrintView)
        this.Repaginate(false, false, 0, false);
      else
        this.e.TerRewrap();
      if (repaint)
        this.PaintTer();
    }
    return true;
  }

  internal bool TerSetPageBkColor(Color BkColor)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    this.e.PageBkColor = BkColor;
    ++this.e.TerArg.modified;
    return true;
  }

  internal bool TerSetPageBorderWidth(int width, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    this.e.PageBorderWidth = width;
    if (repaint)
    {
      this.RefreshFrames(true);
      this.PaintTer();
    }
    return true;
  }

  internal bool TerSetPagePos(int NewPage, int PageY)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.PageMode)
      return false;
    PageY = this.TwipsToScrY(PageY);
    int curPage1 = this.e.CurPage;
    this.e.CurPage = NewPage;
    int curPage2 = this.e.CurPage;
    if (curPage1 != curPage2 || this.e.CurPage != this.e.FirstFramePage)
      this.CreateFrames(false, this.e.CurPage, this.e.CurPage + 1);
    if (this.e.CurPage == this.e.FirstFramePage)
      this.e.TerWinOrgY = PageY;
    else
      this.e.TerWinOrgY = this.e.FirstPageHeight + PageY;
    if (this.e.TerWinOrgY >= this.e.CurPageHeight - this.e.TerWinHeight)
      this.e.TerWinOrgY = this.e.CurPageHeight - this.e.TerWinHeight;
    this.SetTerWindowOrg();
    int y = this.e.TerWinOrgY;
    if (y >= this.e.CurTextHeight)
      y = this.e.CurTextHeight - 1;
    this.e.CurLine = this.UnitsToLine(0, y);
    this.e.CurCol = 0;
    if (this.e.CaretEngaged)
      this.DisengageCaret();
    this.PaintTer();
    return true;
  }

  internal new bool ToggleFittedView()
  {
    if (!this.e.TerArg.PrintView)
      return false;
    this.e.RepageBeginLine = 0;
    if (this.e.TerArg.FittedView)
    {
      this.e.TerArg.FittedView = false;
    }
    else
    {
      this.e.TerArg.FittedView = true;
      if (!this.e.TerArg.PageMode)
        return this.TogglePageMode();
      if (this.e.ViewPageHdrFtr)
        return this.ToggleViewHdrFtr();
    }
    if (this.e.TerArg.PageMode && !this.e.TerArg.FittedView)
      this.e.PagesShowing = true;
    else
      this.e.PagesShowing = false;
    this.DisplayStatus();
    if (this.e.TerArg.PageMode)
      this.TerRepaginate(true);
    else
      this.PaintTer();
    return true;
  }

  internal new bool TogglePageBorder()
  {
    if (!this.e.TerArg.WordWrap || !this.e.TerArg.PageMode || this.e.TerArg.FittedView)
      return false;
    this.e.ShowPageBorder = !this.e.ShowPageBorder;
    if (!this.e.ShowPageBorder)
      this.e.BorderShowing = false;
    this.e.TerRepaginate(true);
    return true;
  }

  internal new bool TogglePageMode()
  {
    if (!this.e.TerArg.PrintView)
      return false;
    if (this.e.TerArg.PageMode)
    {
      if (this.e.EditPageHdrFtr)
        this.ToggleEditHdrFtr();
      this.e.ViewPageHdrFtr = false;
      int rowY = this.GetRowY(this.e.CurLine);
      this.e.TerArg.PageMode = false;
      this.e.TerArg.FittedView = false;
      this.e.TotalFrames = 1;
      this.InitFrame(0);
      this.e.frame[0].ScrFirstLine = this.e.frame[0].ScrLastLine = this.e.BeginLine;
      this.e.frame[0].RowOffset = 0;
      this.e.frame[0].PageFirstLine = 0;
      this.e.frame[0].PageLastLine = this.e.TotalLines - 1;
      this.e.CurLineY = rowY - this.e.TerWinOrgY;
      if (this.e.CurLineY < 0)
        this.e.CurLineY = 0;
      if (this.e.CurLineY > this.e.TerWinHeight)
        this.e.CurLineY = this.e.TerWinHeight;
      this.e.TerWinOrgY = 0;
      for (this.e.BeginLine = this.e.CurLine; this.e.BeginLine > 0; --this.e.BeginLine)
      {
        int num = this.ScrLineHeight(this.e.BeginLine, true);
        if (num <= this.e.CurLineY)
          this.e.CurLineY -= num;
        else
          break;
      }
      this.e.frame[0].ScrFirstLine = this.e.frame[0].ScrLastLine = this.e.BeginLine;
      this.e.WinHeight = this.e.TerWinHeight / this.e.TerFont[0].height;
      this.e.CurRow = this.e.CurLine - this.e.BeginLine;
      this.e.WinHeight = this.e.CurRow + 1;
      this.e.WinYOffsetLine = -1;
    }
    else
    {
      this.e.CurLineY = this.GetRowY(this.e.CurLine);
      this.e.TerArg.PageMode = true;
      this.CreateFrames(false, this.e.CurPage, this.e.CurPage);
      this.e.TerWinOrgY = this.GetRowY(this.e.CurLine) - this.e.CurLineY;
      if (this.e.TerWinOrgY < 0)
        this.e.TerWinOrgY = 0;
    }
    if (this.e.TerArg.PageMode && !this.e.TerArg.FittedView)
      this.e.PagesShowing = true;
    else
      this.e.PagesShowing = false;
    this.SetTerWindowOrg();
    this.PaintTer();
    return true;
  }

  internal bool UpdateBookmark(int line, int PageNo)
  {
    if (this.e.text[line].tag != null)
    {
      ushort[] tag = this.e.text[line].tag;
      int len = this.e.text[line].len;
      for (int index = 0; index < len; ++index)
      {
        for (int next = (int) tag[index]; next != 0; next = this.e.CharTag[next].next)
          this.e.CharTag[next].line = line;
      }
    }
    return true;
  }

  internal bool WrapMoreLines(int sect)
  {
    int num1 = 30;
    int num2 = this.e.LastWrappedLine + 1;
    if ((this.e.TerOpFlags2 & 1) != 0)
    {
      ++this.e.LastWrappedLine;
      return true;
    }
    this.e.KnownSect = sect;
    this.e.KnownSectBegLine = this.e.LastWrappedLine;
    this.e.KnownSectEndLine = this.e.LastWrappedLine + 30;
    for (int index = 0; index <= num1 && num2 + index < this.e.TotalLines; index = this.e.LastWrappedLine - num2 + 1)
    {
      int lastWrappedLine = this.e.LastWrappedLine;
      int WrapLines = num1 - index + 1;
      if (WrapLines < 20)
        WrapLines = 20;
      this.wrp.WrapMakeBuffer(num2 + index, WrapLines);
      this.wrp.WrapParseBuffer(num2 + index);
      bool flag = false;
      if (this.e.LastBufferedLine > this.e.LastWrappedLine + 25)
        flag = true;
      if (this.e.BufferLength == 0)
        flag = true;
      if (this.e.LastBufferedLine + 1 >= this.e.TotalLines)
        flag = true;
      if (this.e.LastWrappedLine == lastWrappedLine + 1)
        flag = true;
      if (this.e.LastBufferedLine > this.e.LastWrappedLine)
      {
        if (flag)
        {
          this.DisplacePointers(this.e.LastBufferedLine + 1, this.e.LastWrappedLine - this.e.LastBufferedLine);
          this.e.LastBufferedLine = this.e.LastWrappedLine;
        }
        else
        {
          for (int line = this.e.LastWrappedLine + 1; line <= this.e.LastBufferedLine; ++line)
          {
            this.init.FreeLine(line);
            this.InitLine(line);
            this.e.text[line].pfmt = 0;
          }
        }
      }
      if (this.e.LastWrappedLine < 0)
        this.e.LastWrappedLine = 0;
      if ((this.e.text[this.e.LastWrappedLine].flags & 131) == 0)
      {
        if (this.e.LastWrappedLine > num2 && this.e.LastWrappedLine + 1 <= this.e.TotalLines && this.e.LastWrappedLine < num2 + num1 - 1 && this.e.LastWrappedLine > lastWrappedLine + 1)
          --this.e.LastWrappedLine;
        else
          this.e.DoExtraPass = true;
      }
      if ((this.e.text[this.e.LastWrappedLine].flags & 2048 /*0x0800*/) != 0)
        this.e.KnownSect = -1;
    }
    this.DisplacePointers(this.e.LastBufferedLine + 1, this.e.LastWrappedLine - this.e.LastBufferedLine);
    if ((this.e.text[this.e.LastWrappedLine].flags & 131) == 0 && this.e.LastWrappedLine > num2 && this.e.LastWrappedLine + 1 <= this.e.TotalLines && this.e.LastWrappedLine > num2 + 1)
      --this.e.LastWrappedLine;
    this.e.KnownSect = -1;
    return true;
  }
}
