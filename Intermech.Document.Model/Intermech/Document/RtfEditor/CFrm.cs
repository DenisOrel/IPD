// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.CFrm
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class CFrm : COp
{
  internal CFrm(ImRtfEditor ImRtfEditor)
    : base(ImRtfEditor)
  {
  }

  internal new bool AnchorParaFound(int line, int CurFID)
  {
    if (this.e.text[line].len == 0)
      return false;
    int len = this.e.text[line].len;
    char chr = this.e.text[line].txt[len - 1];
    return ((int) chr == (int) this.e.ParaChar || this.lstrchr(this.e.BreakChars, chr)) && (!this.True(this.e.text[line].cid) || !this.False(this.e.text[line].tabw) && (this.e.text[line].tabw.type & 32 /*0x20*/) != 0) && (line + 1 >= this.e.TotalLines || (this.e.text[line + 1].flags & 1966080 /*0x1E0000*/) == 0) && line < this.e.TotalLines - 1 && (this.e.text[line + 1].fid == 0 || this.e.text[line + 1].fid != CurFID);
  }

  internal new bool AnchorPictFrame(int pict, int LineNo, int col)
  {
    int pLineNo = 0;
    int pCol = 0;
    if (!this.e.TerLocateFontId(pict, ref pLineNo, ref pCol))
      return false;
    this.MoveLineData(pLineNo, pCol, 1, 'D');
    this.MoveLineData(LineNo, col, 1, 'B');
    char[] txt = this.e.text[LineNo].txt;
    ushort[] numArray = this.OpenCfmt(LineNo);
    int index = col;
    txt[index] = '\u0018';
    numArray[col] = (ushort) pict;
    this.CloseCfmt(LineNo);
    this.ReleaseUndo();
    return true;
  }

  internal int BlankHdrFrameHeight(int FirstLine, int LastLine, int sect)
  {
    return (int) ((double) this.e.TerSect[sect].HdrMargin * (double) this.e.UnitResY) - this.e.TerSect1[sect].HiddenY;
  }

  internal new bool CalcFrameSpace(
    int line,
    COp.RECT rect,
    out int FrameX,
    out int FrameWidth,
    out int FrameHt,
    int sect,
    bool GetRowSpace,
    bool GetLineSpace,
    bool GetRowIndent,
    int PageNo)
  {
    int num1 = 0;
    int num2 = 0;
    int num3 = 0;
    int num4 = 0;
    int num5 = 0;
    int num6 = 0;
    int num7 = 0;
    int num8 = 0;
    int num9 = 1;
    int x1 = 0;
    int num10 = line;
    int num11;
    FrameHt = num11 = 0;
    int num12;
    FrameWidth = num12 = num11;
    FrameX = num12;
    if (!this.e.TerArg.PageMode || GetRowIndent & GetRowSpace)
      return false;
    if (line >= this.e.TotalLines)
      line = this.e.TotalLines - 1;
    if (line >= 0 && this.e.text[line].fid > 0)
      return false;
    bool flag1 = line >= 0 && this.e.text[line].cid > 0;
    if (flag1)
      num5 = this.e.text[line].cid;
    if (GetRowSpace | GetRowIndent && !this.e.repaginating)
      return false;
    if (GetRowSpace | GetRowIndent)
      flag1 = false;
    int num13;
    int x2;
    bool flag2;
    if (line >= 0)
    {
      if ((this.e.text[line].flags & 1966080 /*0x1E0000*/) != 0)
        return false;
      num13 = this.e.text[line].y;
      if (num13 == 0 && this.e.ViewPageHdrFtr)
        return false;
      if (PageNo < 0)
      {
        int num14 = 0;
        PageNo = this.e.text[line].page;
        if ((this.e.PfmtId[this.e.text[line].pfmt].flags & 12288 /*0x3000*/) != 0)
        {
          if (this.e.FullRenderMode)
            num14 = this.sec.GetHdrFtrFlag(line);
          if ((num14 & 1572864 /*0x180000*/) != 0)
          {
            for (int firstLine = this.e.PageInfo[PageNo].FirstLine; firstLine <= this.e.PageInfo[PageNo].LastLine && firstLine < this.e.TotalLines && (this.e.PfmtId[this.e.text[firstLine].pfmt].flags & 12288 /*0x3000*/) != 0; ++firstLine)
            {
              if ((num14 & 524288 /*0x080000*/) != 0 && (this.e.text[firstLine].flags & 131072 /*0x020000*/) != 0 || (num14 & 1048576 /*0x100000*/) != 0 && (this.e.text[firstLine].flags & 262144 /*0x040000*/) != 0)
              {
                ++PageNo;
                break;
              }
            }
          }
        }
      }
      x2 = this.e.text[line].height;
      if (num10 > line)
        num13 += x2;
      if (this.e.FullRenderMode)
        x1 = this.sec.GetHdrFtrFlag(line);
      flag2 = (this.e.text[line].flags2 & 32 /*0x20*/) != 0;
    }
    else
    {
      num13 = rect.top == 0 ? 0 : this.MulDiv(rect.top, this.e.UnitResY, this.e.ScrResY);
      if (PageNo < 0)
        PageNo = this.e.CurPage;
      x2 = rect.bottom - rect.top;
      if (x2 != 0)
        x2 = this.MulDiv(x2, this.e.UnitResY, this.e.ScrResY);
      flag2 = false;
    }
    if (x2 == 0)
      return false;
    if (GetRowSpace | GetRowIndent)
    {
      int row = this.e.cell[this.e.text[line].cid].row;
      int index = this.e.TableRow[row].FirstCell;
      if (GetRowSpace)
      {
        num6 = this.e.text[line].x + (this.e.TableRow[row].indent != 0 ? this.MulDiv(this.e.TableRow[row].indent, this.e.UnitResX, 1440) : 0);
        if (this.e.BorderShowing)
          num6 -= this.GetBorderLeftSpace(PageNo);
      }
      else
        num6 = this.e.text[line].x;
      num7 = num6;
      for (; index > 0; index = this.e.cell[index].NextCell)
        num7 += this.MulDiv(this.e.cell[index].width, this.e.UnitResX, 1440);
      num8 = num7 - num6;
    }
    int num15;
    int num16;
    int num17;
    if (line >= 0)
    {
      if (sect < 0)
        sect = this.GetSection(line);
      if (GetRowSpace | GetRowIndent)
      {
        num15 = this.ScrToUnitX(this.wrp.TerWrapWidth2(-1, sect, true));
        if (this.e.TerSect[sect].columns > 1 && (this.e.PfmtId[this.e.text[line].pfmt].flags & 12288 /*0x3000*/) == 0)
          num15 = (num15 - (int) ((double) this.e.UnitResX * ((double) this.e.TerSect[sect].ColumnSpace * (double) (this.e.TerSect[sect].columns - 1)))) / this.e.TerSect[sect].columns;
      }
      else
        num15 = this.ScrToUnitX(this.wrp.TerWrapWidth2(line, sect, true));
      num16 = this.e.text[line].x;
      num17 = !this.True(this.e.text[line].cid) ? this.TwipsToUnitX(this.e.FrameDistFromMargin) : this.TwipsToUnitX(this.e.FrameDistFromMargin / 4);
      num9 = this.e.TerSect[sect].columns;
    }
    else
    {
      num15 = this.ScrToUnitX(rect.right - rect.left);
      num16 = this.ScrToUnitX(rect.left);
      num17 = 0;
    }
    int num18 = num17 / 4;
    if (this.e.BorderShowing)
      num16 -= this.GetBorderLeftSpace(PageNo);
    int num19 = 0;
    bool flag3;
    int num20;
    while (true)
    {
      bool flag4 = true;
      flag3 = false;
      int num21 = num20 = num13;
      int num22 = num13 + x2;
      int num23 = this.TwipsToUnitY(180) * 3 / 4;
      int num24 = 5;
      if ((this.e.TerFlags & 1048576 /*0x100000*/) != 0 || !this.e.PrinterAvailable)
        num24 = 1;
      for (int index = 1; index < this.e.TotalParaFrames; ++index)
      {
        this.e.ParaFrame[index].flags = tc.ResetUintFlag(ref this.e.ParaFrame[index].flags, 4096 /*0x1000*/);
        if (this.e.ParaFrame[index].InUse && (this.e.ParaFrame[index].flags & 4194304 /*0x400000*/) == 0 && this.e.ParaFrame[index].ShapeType != 20 && (this.e.ParaFrame[index].flags & 256 /*0x0100*/) == 0 && (this.e.ParaFrame[index].flags & 32768 /*0x8000*/) != 0)
        {
          int textLine = this.e.ParaFrame[index].TextLine;
          int hdrFtrFlag = textLine < 0 || textLine >= this.e.TotalLines ? 0 : this.sec.GetHdrFtrFlag(textLine);
          if (this.e.ParaFrame[index].PageNo == PageNo && (hdrFtrFlag & 1572864 /*0x180000*/) != 0)
          {
            int topSect = this.e.PageInfo[PageNo].TopSect;
            if ((hdrFtrFlag & 524288 /*0x080000*/) != 0 && this.e.TerSect1[topSect].fhdr.FirstLine >= 0 || (hdrFtrFlag & 1048576 /*0x100000*/) != 0 && this.e.TerSect1[topSect].fftr.FirstLine >= 0)
              continue;
          }
          if (this.e.ParaFrame[index].PageNo != PageNo)
          {
            int pageNo = this.e.ParaFrame[index].PageNo;
            if (!this.True(hdrFtrFlag) || pageNo < 0 || (hdrFtrFlag & 524288 /*0x080000*/) != 0 && this.e.PageInfo[pageNo].TopSect != this.e.PageInfo[PageNo].HdrSect || (hdrFtrFlag & 1048576 /*0x100000*/) != 0 && this.e.PageInfo[pageNo].TopSect != this.e.PageInfo[PageNo].FtrSect || (hdrFtrFlag & 131072 /*0x020000*/) != 0 || (hdrFtrFlag & 262144 /*0x040000*/) != 0)
              continue;
          }
          if ((this.e.ParaFrame[index].flags & 16384 /*0x4000*/) == 0)
          {
            int num25 = 0;
            if (this.e.ParaFrame[index].pict > 0)
              num25 = this.e.ParaFrame[index].CellId;
            if ((!flag1 || num5 == num25) && (flag1 || num25 <= 0) && (textLine < 0 || textLine >= this.e.TotalLines || (hdrFtrFlag == x1 || ((hdrFtrFlag & 655360 /*0x0A0000*/) == 0 || (x1 & 1310720 /*0x140000*/) == 0) && ((hdrFtrFlag & 1310720 /*0x140000*/) == 0 || (x1 & 655360 /*0x0A0000*/) == 0)) && (!this.True(hdrFtrFlag) || x1 != 0 || this.e.ParaFrame[index].ShapeType == 75) && (!this.True(x1) || (hdrFtrFlag & 1966080 /*0x1E0000*/) == 0) && ((x1 & 1048576 /*0x100000*/) == 0 || (hdrFtrFlag & 524288 /*0x080000*/) == 0) && (!this.True(hdrFtrFlag) || hdrFtrFlag != x1 || this.e.ParaFrame[index].ShapeType == 0 && (this.e.ParaFrame[index].flags & 896) == 0)))
            {
              int unitY1 = this.TwipsToUnitY(this.e.ParaFrame[index].y);
              int unitY2 = this.TwipsToUnitY(this.e.ParaFrame[index].y + this.e.ParaFrame[index].height);
              if (num22 > unitY1 + num24 && num21 < unitY2 - num24)
              {
                bool flag5 = false;
                if (GetRowSpace || GetLineSpace && num19 > 0)
                {
                  if (unitY1 - num23 < num21)
                  {
                    num21 = unitY1 - 2 * num23;
                    flag5 = true;
                  }
                  if (unitY2 + num23 > num22)
                  {
                    num22 = unitY2 + 2 * num23;
                    flag5 = true;
                  }
                }
                if (flag5)
                {
                  index = 0;
                }
                else
                {
                  int num26 = this.TwipsToUnitX(this.e.ParaFrame[index].x - this.e.ParaFrame[index].DistFromText);
                  int num27 = this.TwipsToUnitX(this.e.ParaFrame[index].x + this.e.ParaFrame[index].width + this.e.ParaFrame[index].DistFromText);
                  if ((this.e.ParaFrame[index].flags & 8192 /*0x2000*/) != 0)
                  {
                    if (num26 > num16)
                      num26 = num16;
                    if (num27 < num16 + num15)
                      num27 = num16 + num15;
                  }
                  if ((!GetRowSpace || num27 >= num6 && num26 <= num7) && (num9 <= 1 || num26 <= num16 + num15 && num27 >= num16))
                  {
                    if (flag2)
                    {
                      int num28 = num16 + num15 - num27;
                      int num29 = num27 - num26;
                      num26 = num16 + num28;
                      num27 = num26 + num29;
                    }
                    if (num26 - num17 <= num16 && num27 + num18 >= num16 + num15)
                    {
                      flag3 = true;
                      if (unitY2 > num20)
                        num20 = unitY2;
                    }
                    if (flag4)
                    {
                      num1 = num26;
                      num2 = num27;
                      num3 = unitY1;
                      num4 = unitY2;
                    }
                    else
                    {
                      if (num26 < num1)
                        num1 = num26;
                      if (num27 > num2)
                        num2 = num27;
                      if (unitY1 < num3)
                        num3 = unitY1;
                      if (unitY2 > num4)
                        num4 = unitY2;
                    }
                    flag4 = false;
                    this.e.ParaFrame[index].flags |= 4096 /*0x1000*/;
                    if ((this.e.ParaFrame[index].flags & 8192 /*0x2000*/) != 0)
                      flag3 = true;
                  }
                }
              }
            }
          }
        }
      }
      if (!flag4)
      {
        if (num19 == 0 & GetLineSpace && num2 + num18 > num15)
          ++num19;
        else
          goto label_109;
      }
      else
        break;
    }
    return false;
label_109:
    if (GetRowSpace && num2 + num8 + num18 / 4 < num15)
      return false;
    int num30 = num1 - num16;
    if (num30 < 0)
      num30 = 0;
    int num31 = num2 - num16;
    if (num31 < num30)
      num31 = num30;
    if (num31 > num15)
      num31 = num15;
    if (num30 < num17)
      num30 = 0;
    if (num31 + num18 > num15)
      num31 = num15;
    if (line >= 0 && num30 == 0 && num31 + this.e.TerFont[0].CharWidth[87] >= num15)
      num31 = num15;
    FrameX = num30;
    FrameWidth = num31 - num30;
    if (num30 == 0 && num31 >= num15 || flag3 | GetRowSpace)
    {
      if (flag3)
        num4 = num20;
      if (num4 > num13)
      {
        int x3 = x2 / 3;
        if (x3 > 100)
          x3 = 100;
        if (!GetRowSpace)
          x3 /= 2;
        FrameHt = num4 - num13 + this.TwipsToUnitY(x3);
      }
    }
    return true;
  }

  internal new int CalcFrmIndentBefRow(int line, int sect)
  {
    int FrameX;
    int FrameWidth;
    int FrameHt;
    return line >= 0 && this.CalcFrameSpace(line, new COp.RECT(), out FrameX, out FrameWidth, out FrameHt, sect, false, false, true, -1) && FrameHt == 0 && FrameX == 0 ? FrameWidth : 0;
  }

  internal new int CalcFrmSpcBef(int line, int sect, bool set, int PageNo)
  {
    int num = 0;
    if (line < 0)
      return 0;
    int FrameHt;
    if (this.CalcFrameSpace(line, new COp.RECT(), out int _, out int _, out FrameHt, sect, false, true, false, PageNo))
      num = FrameHt;
    if (set)
    {
      if (num > 0)
      {
        if (this.False(this.e.text[line].tabw) && (this.e.text[line].tabw = new tc.ClsTabw()) != null)
          this.e.text[line].tabw.section = sect;
        if (this.True(this.e.text[line].tabw))
        {
          this.e.text[line].tabw.type |= 8192 /*0x2000*/;
          this.e.text[line].tabw.height = num;
        }
        return num;
      }
      if (this.True(this.e.text[line].tabw))
      {
        this.e.text[line].tabw.type = tc.ResetUintFlag(ref this.e.text[line].tabw.type, 8192 /*0x2000*/);
        this.e.text[line].tabw.height = 0;
      }
    }
    return num;
  }

  internal new int CalcFrmSpcBefRow(int line, int sect)
  {
    int num = 0;
    if (line < 0)
      return 0;
    int FrameHt;
    if (this.CalcFrameSpace(line, new COp.RECT(), out int _, out int _, out FrameHt, sect, true, false, false, -1))
      num = FrameHt;
    return num;
  }

  internal bool CreateCellFrame(
    int level,
    ref int FrameNo,
    int y,
    int ScrY,
    ref int FrameHt,
    ref int ScrFrameHt,
    int CurColHeight,
    int ScrCurColHeight,
    ref int FirstCellFrame,
    ref int TableRowHeight,
    ref int ScrTableRowHeight,
    ref int CellX,
    int TableRowIndent,
    int PageNo,
    int TopLeftMargin,
    int ColumnNo,
    int sect,
    int PrevCell,
    ref int CellFramed,
    int FrameFirstLine,
    int FrameLastLine,
    int PassFlags,
    bool RowBreak,
    ref int CellWidth,
    int TextWidth,
    int MaxColumns,
    int ColumnSpace,
    int PageWdth,
    int ColumnX,
    int ColumnWidth,
    int BoxFrame,
    int ParaFrameId)
  {
    bool flag = false;
    int row1 = this.e.cell[PrevCell].row;
    Color[] pColor = new Color[4];
    if (level > 0)
      this.TwipsToUnitX(this.e.cell[this.e.cell[PrevCell].ParentCell].margin);
    int frmSpcBef1 = this.e.TableRow[this.e.cell[PrevCell].row].FrmSpcBef;
    if (frmSpcBef1 > 0)
    {
      y += frmSpcBef1;
      ScrY += this.UnitToScrY(frmSpcBef1);
    }
    if (FirstCellFrame == -1)
    {
      FirstCellFrame = FrameNo;
      int x = this.e.TableRow[row1].MinHeight;
      if ((this.e.TableRow[row1].flags & 16 /*0x10*/) != 0 && x > 0)
        x = 0;
      if (x < 0)
        x = -x;
      if (this.e.TableRow[row1].MinPictHeight > x)
        x = this.e.TableRow[row1].MinPictHeight;
      TableRowHeight = this.TwipsToUnitY(x);
      ScrTableRowHeight = this.TwipsToScrY(x);
      flag = true;
    }
    int pLeftWidth;
    int pRightWidth;
    int pTopWidth;
    int pBotWidth;
    int cellFrameBorder = this.GetCellFrameBorder(PrevCell, out pLeftWidth, out pRightWidth, out pTopWidth, out pBotWidth, PageNo, pColor);
    int num1 = (this.e.CellAux[PrevCell].flags & 2) == 0 ? this.TwipsToUnitY(pTopWidth) : this.TwipsToUnitY(this.e.cell[PrevCell].margin);
    int unitY = this.TwipsToUnitY(pBotWidth);
    FrameHt += num1 + unitY;
    ScrFrameHt += this.UnitToScrY(num1 + unitY);
    int num2 = 0;
    if (this.IsLastSpannedCell(PrevCell))
      num2 = this.GetLastSpannedCellHeight(PrevCell, out tc.SkipInt, PageNo);
    else if ((this.e.cell[PrevCell].RowSpan == 1 || this.IsPageLastRow(this.e.cell[PrevCell].row, PageNo)) && (this.e.cell[PrevCell].flags & 16 /*0x10*/) == 0)
    {
      num2 = FrameHt;
      if (this.e.cell[PrevCell].TextAngle != 0)
        num2 = this.TwipsToUnitY(720);
      if ((this.e.cell[PrevCell].flags & 65536 /*0x010000*/) != 0)
        num2 += this.e.CellAux[PrevCell].SpaceBefore;
    }
    if (this.e.TableRow[row1].MinHeight >= 0 && num2 > TableRowHeight)
      TableRowHeight = num2;
    ScrTableRowHeight = this.UnitToScrY(TableRowHeight);
    if (RowBreak)
    {
      this.e.TableRow[this.e.cell[PrevCell].row].height = TableRowHeight;
      for (int index = FirstCellFrame; index <= FrameNo; ++index)
      {
        if (this.e.frame[index].level == level)
        {
          this.e.frame[index].height = TableRowHeight;
          this.e.frame[index].ScrHeight = ScrTableRowHeight;
        }
      }
      FirstCellFrame = -1;
    }
    if (flag)
    {
      int row2 = this.e.cell[PrevCell].row;
      if (this.e.TableRow[row2].FrmSpcBef > 0)
      {
        int frmSpcBef2 = this.e.TableRow[row2].FrmSpcBef;
        this.e.frame[FrameNo].empty = true;
        this.e.frame[FrameNo].level = level;
        this.e.frame[FrameNo].sect = sect;
        this.e.frame[FrameNo].x = CellX;
        this.e.frame[FrameNo].y = y + CurColHeight - frmSpcBef2;
        this.e.frame[FrameNo].ScrY = ScrY + ScrCurColHeight - this.UnitToScrY(frmSpcBef2);
        this.e.frame[FrameNo].width = TextWidth;
        this.e.frame[FrameNo].BoxFrame = BoxFrame;
        this.e.frame[FrameNo].ParaFrameId = ParaFrameId;
        if (ParaFrameId > 0)
          this.e.frame[FrameNo].ZOrder = this.e.ParaFrame[ParaFrameId].ZOrder;
        if (!this.e.BorderShowing && level == 0)
          this.e.frame[FrameNo].flags |= 1;
        this.e.frame[FrameNo].height = frmSpcBef2;
        this.e.frame[FrameNo].ScrHeight = this.UnitToScrY(frmSpcBef2);
        ++FrameNo;
        this.InitFrame(FrameNo);
        if (FirstCellFrame >= 0)
          FirstCellFrame = FrameNo;
      }
      this.e.TableRow[row1].FirstFrame = FrameNo;
      this.e.frame[FrameNo].empty = true;
      this.e.frame[FrameNo].level = level;
      this.e.frame[FrameNo].sect = sect;
      this.e.frame[FrameNo].y = y + CurColHeight;
      this.e.frame[FrameNo].ScrY = ScrY + ScrCurColHeight;
      this.e.frame[FrameNo].BoxFrame = BoxFrame;
      this.e.frame[FrameNo].ParaFrameId = ParaFrameId;
      if (ParaFrameId > 0)
        this.e.frame[FrameNo].ZOrder = this.e.ParaFrame[ParaFrameId].ZOrder;
      this.e.frame[FrameNo].CellId = PrevCell;
      this.e.frame[FrameNo].width = this.e.TableRow[row1].CurIndent;
      this.e.frame[FrameNo].x = CellX;
      this.e.frame[FrameNo].border = 0;
      this.e.frame[FrameNo].RowId = row1;
      int num3;
      FrameHt = num3 = TableRowHeight;
      this.e.frame[FrameNo].height = num3;
      int num4;
      ScrFrameHt = num4 = ScrTableRowHeight;
      this.e.frame[FrameNo].ScrHeight = num4;
      this.e.frame[FrameNo].flags |= 524288 /*0x080000*/;
      CellX += this.e.frame[FrameNo].width;
      ++FrameNo;
      this.InitFrame(FrameNo);
    }
    this.FrameEmptyCells(row1, ref CellFramed, PrevCell, ref CellX, y + CurColHeight, TableRowHeight, ref FrameNo, sect, PageNo);
    int unitX = this.TwipsToUnitX(this.e.cell[PrevCell].margin);
    CellWidth = this.TwipsToUnitX(this.e.cell[PrevCell].width);
    this.e.frame[FrameNo].empty = false;
    this.e.frame[FrameNo].level = level;
    this.e.frame[FrameNo].sect = sect;
    this.e.frame[FrameNo].PageFirstLine = FrameFirstLine;
    this.e.frame[FrameNo].PageLastLine = FrameLastLine;
    this.e.frame[FrameNo].y = y + CurColHeight;
    this.e.frame[FrameNo].ScrY = ScrY + ScrCurColHeight;
    this.e.frame[FrameNo].BoxFrame = BoxFrame;
    this.e.frame[FrameNo].ParaFrameId = ParaFrameId;
    if (ParaFrameId > 0)
      this.e.frame[FrameNo].ZOrder = this.e.ParaFrame[ParaFrameId].ZOrder;
    this.e.frame[FrameNo].shading = this.e.cell[PrevCell].shading;
    this.e.frame[FrameNo].BackColor = this.e.cell[PrevCell].BackColor;
    if (this.e.cell[PrevCell].BackColor == tc.CLR_WHITE && this.e.cell[PrevCell].ParentCell > 0)
    {
      int parentCell = this.e.cell[PrevCell].ParentCell;
      while (parentCell > 0 && !(this.e.cell[parentCell].BackColor != tc.CLR_WHITE))
        parentCell = this.e.cell[parentCell].ParentCell;
      if (parentCell > 0)
        this.e.frame[FrameNo].BackColor = Color.FromArgb(253, 253, 253);
    }
    this.e.frame[FrameNo].flags |= PassFlags;
    if ((this.e.cell[PrevCell].flags & 16384 /*0x4000*/) != 0)
      this.e.frame[FrameNo].flags |= 1024 /*0x0400*/;
    this.e.frame[FrameNo].x = CellX;
    this.e.frame[FrameNo].width = CellWidth;
    this.e.frame[FrameNo].SpaceLeft = unitX;
    if (this.e.HtmlMode & flag && (cellFrameBorder & 4) != 0)
      this.e.frame[FrameNo].SpaceLeft += this.ScrToUnitX(3);
    this.e.frame[FrameNo].SpaceRight = unitX;
    if (flag)
      this.e.TableAux[row1].FrmBegX = this.e.frame[FrameNo].x;
    this.e.TableAux[row1].FrmEndX = this.e.frame[FrameNo].x + this.e.frame[FrameNo].width;
    this.e.frame[FrameNo].border = cellFrameBorder;
    this.e.frame[FrameNo].BorderWidth[2] = pLeftWidth;
    this.e.frame[FrameNo].BorderWidth[3] = pRightWidth;
    this.e.frame[FrameNo].BorderWidth[0] = pTopWidth;
    this.e.frame[FrameNo].BorderWidth[1] = pBotWidth;
    for (int index = 0; index < 4; ++index)
      this.e.frame[FrameNo].BorderColor[index] = pColor[index];
    this.e.frame[FrameNo].RowId = row1;
    if ((this.e.cell[PrevCell].flags & 16 /*0x10*/) != 0)
    {
      int num5;
      this.e.frame[FrameNo].ScrHeight = num5 = 0;
      int num6;
      this.e.frame[FrameNo].height = num6 = num5;
      this.e.frame[FrameNo].TextHeight = num6;
    }
    else
    {
      this.e.frame[FrameNo].TextHeight = ScrFrameHt - this.UnitToScrY(unitY);
      this.e.frame[FrameNo].height = TableRowHeight;
      this.e.frame[FrameNo].ScrHeight = ScrTableRowHeight;
    }
    FrameHt = TableRowHeight;
    ScrFrameHt = ScrTableRowHeight;
    this.e.frame[FrameNo].SpaceTop = num1;
    this.e.frame[FrameNo].SpaceBot = unitY;
    this.e.frame[FrameNo].CellId = PrevCell;
    CellFramed = PrevCell;
    ++FrameNo;
    this.InitFrame(FrameNo);
    int pX = this.e.frame[FrameNo - 1].x + this.e.frame[FrameNo - 1].width;
    if (RowBreak)
    {
      this.FrameEmptyCells(row1, ref CellFramed, 0, ref pX, this.e.frame[FrameNo - 1].y, this.e.frame[FrameNo - 1].height, ref FrameNo, sect, PageNo);
      this.e.TableRow[row1].LastFrame = FrameNo;
      this.e.frame[FrameNo].empty = true;
      this.e.frame[FrameNo].level = level;
      this.e.frame[FrameNo].y = this.e.frame[FrameNo - 1].y;
      this.e.frame[FrameNo].ScrY = this.e.frame[FrameNo - 1].ScrY;
      this.e.frame[FrameNo].x = pX;
      this.e.frame[FrameNo].BoxFrame = BoxFrame;
      this.e.frame[FrameNo].ParaFrameId = ParaFrameId;
      if (ParaFrameId > 0)
        this.e.frame[FrameNo].ZOrder = this.e.ParaFrame[ParaFrameId].ZOrder;
      this.e.frame[FrameNo].width = ColumnX + ColumnWidth + ColumnSpace - pX;
      if (level == 0)
      {
        if (ParaFrameId > 0)
        {
          int num7 = this.e.frame[BoxFrame].x + this.e.frame[BoxFrame].width - this.e.frame[BoxFrame].BorderWidth[3];
          this.e.frame[FrameNo].width = num7 - this.e.frame[FrameNo].x;
        }
        else if (this.e.BorderShowing)
        {
          if (MaxColumns == 1 || ColumnNo + 1 == MaxColumns)
          {
            int sect1 = this.e.frame[FrameNo - 1].sect;
            this.e.frame[FrameNo].width = PageWdth - this.e.LeftBorderWidth - (int) ((double) this.e.TerSect[sect1].RightMargin * (double) this.e.UnitResX) - pX;
          }
        }
        else
          this.e.frame[FrameNo].flags |= 1;
      }
      if (this.e.frame[FrameNo].width < 0)
        this.e.frame[FrameNo].width = 0;
      this.e.frame[FrameNo].height = this.e.frame[FrameNo - 1].height;
      this.e.frame[FrameNo].ScrHeight = this.e.frame[FrameNo - 1].ScrHeight;
      this.e.frame[FrameNo].sect = this.e.frame[FrameNo - 1].sect;
      this.e.frame[FrameNo].RowId = this.e.frame[FrameNo - 1].RowId;
      this.e.frame[FrameNo].CellId = this.e.frame[FrameNo - 1].CellId;
      this.e.frame[FrameNo].flags |= PassFlags | 1048576 /*0x100000*/;
      CellFramed = 0;
      ++FrameNo;
      this.InitFrame(FrameNo);
    }
    FrameHt += frmSpcBef1;
    ScrFrameHt += this.UnitToScrY(frmSpcBef1);
    return true;
  }

  internal bool CreateFnoteFrame(
    int PageNo,
    ref int pY,
    ref int pScrY,
    int TextWidth,
    int TopLeftMargin,
    int TopRightMargin)
  {
    int num1 = pY;
    int num2 = pScrY;
    int totalFrames = this.e.TotalFrames;
    int section = this.GetSection(this.e.PageInfo[PageNo].FirstLine);
    this.e.frame[totalFrames].empty = true;
    this.e.frame[totalFrames].y = num1;
    this.e.frame[totalFrames].ScrY = num2;
    this.e.frame[totalFrames].PageFirstLine = this.e.PageInfo[PageNo].FirstLine;
    this.e.frame[totalFrames].PageLastLine = this.e.PageInfo[PageNo].LastLine;
    this.e.frame[totalFrames].width = TextWidth;
    this.e.frame[totalFrames].height = this.e.PageInfo[PageNo].FnoteHt;
    int scrY;
    this.e.frame[totalFrames].TextHeight = scrY = this.UnitToScrY(this.e.frame[totalFrames].height);
    this.e.frame[totalFrames].ScrHeight = scrY;
    this.e.frame[totalFrames].sect = section;
    this.e.frame[totalFrames].flags |= 8192 /*0x2000*/;
    if (this.e.BorderShowing)
    {
      this.e.frame[totalFrames].x = this.e.LeftBorderWidth;
      this.e.frame[totalFrames].width += TopLeftMargin + TopRightMargin;
      this.e.frame[totalFrames].SpaceLeft = TopLeftMargin;
    }
    else
    {
      this.e.frame[totalFrames].x = 0;
      this.e.frame[totalFrames].flags |= 1;
    }
    int num3 = num1 + this.e.frame[totalFrames].height;
    int num4 = num2 + this.e.frame[totalFrames].ScrHeight;
    ++this.e.TotalFrames;
    this.InitFrame(totalFrames + 1);
    pY = num3;
    pScrY = num4;
    return true;
  }

  internal new void CreateFrames(bool printer, int PageNo, int LastPage)
  {
    if (this.e.TotalLines == 0)
      return;
    int num1 = -1;
    int TextWidth = 0;
    int num2 = 0;
    int num3 = 0;
    int PageWdth = 0;
    int num4 = 0;
    int num5 = 0;
    int num6 = 0;
    int num7 = 0;
    int pLeftFrame = 0;
    int pRightFrame = 0;
    int CellWidth = 0;
    int pTableRowIndent = 0;
    int BoxFrame = 0;
    int FirstLine = 0;
    int[] FrameColX = new int[20];
    int[] FrameColWidth = new int[20];
    int[] FrameColFirst = new int[20];
    if (tc.DebugMode)
      this.misc.dm(nameof (CreateFrames));
    this.e.TotalFrames = 0;
    this.e.ContainsParaFrames = false;
    this.e.PageHasControls = false;
    this.e.HasOverlayingFrames = false;
    int num8;
    this.e.FirstPageHeight = num8 = 0;
    if (PageNo < 0)
      PageNo = 0;
    this.e.FirstFramePage = PageNo;
    this.e.BorderShowing = this.e.ShowPageBorder && !this.e.TerArg.FittedView;
    CFrm.StrFrameSet p = new CFrm.StrFrameSet();
    CFrm.StrFrameSet s = new CFrm.StrFrameSet();
    CFrm.StrFrameSet pSavePrt = new CFrm.StrFrameSet();
    CFrm.StrFrameSet pSaveScr = new CFrm.StrFrameSet();
    for (int index = 0; index < this.e.TotalTableRows; ++index)
    {
      this.e.TableRow[index].LastFrame = -1;
      this.e.TableRow[index].FirstFrame = -1;
    }
    do
    {
      int totalFrames1;
      int index1 = totalFrames1 = this.e.TotalFrames;
      if (num1 < index1)
      {
        this.InitFrame(index1);
        num1 = index1;
      }
      this.e.CurPageWidth = 0;
      p.FtrHeight = 0;
      p.HdrHeight = 0;
      bool pHasPictFrames = false;
      this.e.LastFramePage = PageNo;
      p.HdrMargin = -1;
      bool flag1 = false;
      int pass = 0;
      int index2 = -1;
      if (PageNo == this.e.CurPage)
      {
        this.e.FtrLastPageLine = 0;
        this.e.FtrFirstPageLine = 0;
        this.e.HdrLastPageLine = 0;
        this.e.HdrFirstPageLine = 0;
      }
      int frameCount = this.e.PageInfo[PageNo].FrameCount;
      this.e.PageInfo[PageNo].FrameCount = 0;
      this.e.PageInfo[PageNo].flags = tc.ResetFlag(this.e.PageInfo[PageNo].flags, 4);
      int index3 = this.e.PageInfo[PageNo].FirstLine;
      if (index3 >= this.e.TotalLines)
        index3 = this.e.TotalLines - 1;
      if (this.e.text[index3].cid != 0)
      {
        while (index3 >= 0 && this.e.text[index3].page == PageNo && this.e.text[index3].cid != 0)
          --index3;
        int num9 = index3 + 1;
        this.e.PageInfo[PageNo].FirstLine = num9;
      }
      int firstLine1 = this.e.PageInfo[PageNo].FirstLine;
      if (PageNo > 0)
        p.TopSect = this.sec.GetSection(firstLine1);
      if ((double) this.e.TerSect[p.TopSect].LeftMargin != 0.0)
        p.TopLeftMargin = (int) ((double) this.e.UnitResX * (double) this.e.TerSect[p.TopSect].LeftMargin);
      if (PageNo == this.e.FirstFramePage)
        num6 = p.TopLeftMargin;
      else
        num7 = p.TopLeftMargin;
      int TopRightMargin = 0;
      if ((double) this.e.TerSect[p.TopSect].RightMargin != 0.0)
        TopRightMargin = (int) ((double) this.e.UnitResX * (double) this.e.TerSect[p.TopSect].RightMargin);
      int num10 = 0;
      int y1 = p.y;
      this.e.BotBorderHeight = 0;
      this.e.TopBorderHeight = 0;
      this.e.LeftBorderWidth = 0;
      if (this.e.BorderShowing)
        this.CreatePageBorderFrames(PageNo, ref p.y, ref s.y, ref index1, out pLeftFrame, out pRightFrame);
      int PageTopX = num10 + this.e.LeftBorderWidth;
      int PageTopY = y1 + this.e.TopBorderHeight;
      p.HiddenY = this.e.TerSect1[p.TopSect].HiddenY;
      tc.StrHdrFtr hdr;
      tc.StrHdrFtr ftr;
      if (p.HdrMargin == -1)
      {
        if (this.e.FullRenderMode)
        {
          p.sect = this.PageHdrSect(PageNo, out hdr);
          if (p.sect < 0)
            p.sect = p.TopSect;
          p.HdrHeight = this.PageHdrHeight2(PageNo, false, true);
          p.HdrMargin = p.HdrHeight <= 0 ? (int) ((double) this.e.UnitResY * (double) this.e.TerSect[p.sect].TopMargin) : (int) ((double) this.e.UnitResY * (double) this.e.TerSect[p.sect].HdrMargin);
          p.sect = this.PageFtrSect(PageNo, out ftr);
          if (p.sect < 0)
            p.sect = p.TopSect;
          p.FtrHeight = this.PageFtrHeight(PageNo, false);
          p.FtrMargin = p.FtrHeight <= 0 ? (int) ((double) this.e.UnitResY * (double) this.e.TerSect[p.sect].BotMargin) : (int) ((double) this.e.UnitResY * (double) this.e.TerSect[p.sect].FtrMargin);
        }
        else
        {
          p.sect = 0;
          p.HdrHeight = 0;
          p.HdrMargin = 0;
          p.FtrHeight = 0;
          p.FtrMargin = 0;
        }
        p.sect = p.TopSect;
        if (this.e.BorderShowing)
        {
          num3 = (int) ((double) this.e.TerSect1[p.sect].PgHeight * (double) this.e.UnitResY) + this.e.TopBorderHeight + this.e.BotBorderHeight;
          PageWdth = (int) ((double) this.e.TerSect1[p.sect].PgWidth * (double) this.e.UnitResX) + 2 * this.e.LeftBorderWidth;
        }
        else
        {
          if (num3 == 0 || p.sect != 0)
          {
            num3 = (int) ((double) this.e.TerSect1[p.sect].PgHeight * (double) this.e.UnitResY);
            if (this.e.ViewPageHdrFtr)
            {
              if (p.HiddenY != 0)
                num3 -= 2 * p.HiddenY;
            }
            else if (this.e.FullRenderMode)
              num3 -= p.HdrMargin + p.FtrMargin + p.HdrHeight + p.FtrHeight;
          }
          if (PageWdth == 0 || p.sect != 0)
            PageWdth = (int) ((double) this.e.TerSect1[p.sect].PgWidth * (double) this.e.UnitResX) - p.TopLeftMargin - TopRightMargin;
        }
      }
      do
      {
        int InHdrFtr;
        do
        {
          ++pass;
          int PassFlags = 0;
          int y2 = p.y;
          int num11 = index1;
          int index4 = this.e.PageInfo[PageNo].FirstLine;
          if (index4 >= this.e.TotalLines)
            index4 = this.e.TotalLines - 1;
          int index5 = index4;
          index1 = this.e.TotalFrames;
          if (num1 < index1)
          {
            this.InitFrame(index1);
            num1 = index1;
          }
          switch (pass)
          {
            case 1:
              InHdrFtr = 4096 /*0x1000*/;
              if (this.e.ViewPageHdrFtr)
              {
                p.sect = this.PageHdrSect(PageNo, out hdr);
                if (p.sect < 0)
                {
                  p.sect = p.TopSect;
                  goto label_284;
                }
                index4 = hdr.FirstLine + 1;
                index5 = hdr.LastLine - 1;
                if (!this.e.EditPageHdrFtr)
                  PassFlags = 4096 /*0x1000*/;
                if (PageNo == this.e.CurPage)
                {
                  this.e.HdrFirstPageLine = index4;
                  this.e.HdrLastPageLine = index5;
                }
                if (index5 >= index4)
                {
                  int firstLine2 = this.e.PageInfo[PageNo].FirstLine;
                  p.sect = firstLine2 == 0 ? 0 : this.GetSection(firstLine2);
                  int x;
                  if (!this.e.BorderShowing && (x = this.BlankHdrFrameHeight(index4, index5, p.sect)) > 0)
                  {
                    this.e.frame[index1].empty = true;
                    this.e.frame[index1].y = p.y;
                    this.e.frame[index1].ScrY = s.y;
                    this.e.frame[index1].x = 0;
                    this.e.frame[index1].width = (int) ((double) this.e.UnitResX * ((double) this.e.TerSect1[p.sect].PgWidth - (double) this.e.TerSect[p.sect].LeftMargin - (double) this.e.TerSect[p.sect].RightMargin));
                    this.e.frame[index1].flags = 1;
                    this.e.frame[index1].height = x;
                    this.e.frame[index1].ScrHeight = this.UnitToScrY(x);
                    p.y += this.e.frame[index1].height;
                    s.y += this.e.frame[index1].ScrHeight;
                    ++this.e.TotalFrames;
                    ++index1;
                    this.InitFrame(index1);
                    num1 = index1;
                    break;
                  }
                  break;
                }
                continue;
              }
              continue;
            case 2:
              if (this.e.PageInfo[PageNo].TblHdrHt != 0)
              {
                index4 = this.e.PageInfo[PageNo].TblHdrFirstLine;
                index5 = this.e.PageInfo[PageNo].TblHdrLastLine;
                if (index4 < this.e.TotalLines && index5 < this.e.TotalLines)
                {
                  InHdrFtr = 0;
                  PassFlags = 20480 /*0x5000*/;
                  p.sect = index4 == 0 ? 0 : this.GetSection(index4);
                  for (int index6 = index4; index6 <= index5; ++index6)
                  {
                    int row = this.e.cell[this.e.text[index6].cid].row;
                    if (this.e.text[index6].cid == 0 || (this.e.TableRow[row].flags & 4) == 0)
                      goto label_48;
                  }
                  if (this.LineInfo(index4, 32 /*0x20*/) || !this.LineInfo(index5, 32 /*0x20*/))
                    continue;
                  break;
                }
                continue;
              }
              continue;
            case 4:
              InHdrFtr = 8192 /*0x2000*/;
              if (this.e.ViewPageHdrFtr)
              {
                int sect = p.sect;
                p.sect = this.PageFtrSect(PageNo, out ftr);
                if (p.sect < 0)
                {
                  p.sect = p.TopSect;
                  goto label_284;
                }
                index4 = ftr.FirstLine + 1;
                index5 = ftr.LimitFtrLine != 0 ? ftr.LimitFtrLine : ftr.LastLine - 1;
                if (PageNo == this.e.CurPage)
                {
                  this.e.FtrFirstPageLine = index4;
                  this.e.FtrLastPageLine = index5;
                }
                if (index5 >= index4)
                {
                  if (this.e.ViewPageHdrFtr && ftr.height > ftr.TextHeight)
                  {
                    this.e.frame[index1].empty = true;
                    this.e.frame[index1].y = p.y;
                    this.e.frame[index1].ScrY = s.y;
                    this.e.frame[index1].x = this.e.LeftBorderWidth;
                    this.e.frame[index1].width = (int) ((double) this.e.UnitResX * (double) this.e.TerSect1[p.sect].PgWidth);
                    if (!this.e.BorderShowing)
                    {
                      this.e.frame[index1].width -= (int) ((double) this.e.UnitResX * ((double) this.e.TerSect[p.sect].LeftMargin + (double) this.e.TerSect[p.sect].RightMargin));
                      this.e.frame[index1].flags = 1;
                    }
                    this.e.frame[index1].height = ftr.height - ftr.TextHeight;
                    this.e.frame[index1].ScrHeight = this.UnitToScrY(this.e.frame[index1].height);
                    p.y += this.e.frame[index1].height;
                    s.y += this.e.frame[index1].ScrHeight;
                    ++this.e.TotalFrames;
                    ++index1;
                    this.InitFrame(index1);
                    num1 = index1;
                  }
                  p.sect = sect;
                  break;
                }
                goto label_344;
              }
              goto label_344;
            default:
              InHdrFtr = 0;
              p.sect = index4 == 0 ? 0 : this.GetSection(index4);
              if (PageNo == this.e.TotalPages - 1)
              {
                index5 = this.e.TotalLines - 1;
              }
              else
              {
                index5 = this.e.PageInfo[PageNo].LastLine;
                if (index5 >= this.e.TotalLines)
                {
                  index5 = this.e.TotalLines - 1;
                  if (this.e.RepageBeginLine > index5 - 1)
                    this.e.RepageBeginLine = index5 - 1;
                  if (this.e.RepageBeginLine < 0)
                    this.e.RepageBeginLine = 0;
                }
                int cid = this.e.text[index5].cid;
                int row = cid > 0 ? this.e.cell[cid].row : 0;
                bool flag2 = (this.e.TableRow[row].flags & 16 /*0x10*/) != 0;
                if (!this.e.repaginating && row > 0 && !flag2 && !this.LineInfo(index5, 32 /*0x20*/))
                {
                  int index7 = index5;
                  while (index7 > index4 && this.e.text[index7].cid != 0 && (this.e.text[index7].tabw == null || (this.e.text[index7].tabw.type & 32 /*0x20*/) == 0))
                    --index7;
                  if (index7 == index4)
                  {
                    index7 = index5;
                    while (index7 + 1 < this.e.TotalLines && this.e.text[index7].cid != 0 && (this.e.text[index7].tabw == null || (this.e.text[index7].tabw.type & 32 /*0x20*/) == 0))
                      ++index7;
                  }
                  index5 = index7;
                  this.e.PageInfo[PageNo].LastLine = index5;
                  int index8 = this.LevelRow(0, this.e.cell[this.e.text[index5].cid].row);
                  if (index5 + 1 >= this.e.TotalLines || (this.e.TableRow[index8].flags & 16 /*0x10*/) == 0)
                    this.e.PageInfo[PageNo + 1].FirstLine = index5 + 1;
                  tc.ResetUintFlag(ref this.e.PageInfo[PageNo + 1].flags, 1);
                }
              }
              if (index5 < index4)
                index5 = index4;
              if (index5 >= this.e.TotalLines)
              {
                index5 = this.e.TotalLines - 1;
                break;
              }
              break;
          }
          if (index5 >= this.e.TotalLines)
            index5 = this.e.TotalLines - 1;
          if (index4 > index5)
            index4 = index5;
          if ((pass == 3 || pass == 2) && index2 == -1 && (this.e.TerSect[p.TopSect].flags & 384) != 0)
          {
            this.e.frame[index1].empty = true;
            this.e.frame[index1].y = p.y;
            this.e.frame[index1].ScrY = s.y;
            this.e.frame[index1].x = 0;
            if (p.sect == 0)
              this.e.frame[index1].width = PageWdth;
            else
              this.e.frame[index1].width = (int) ((double) this.e.UnitResX * ((double) this.e.TerSect1[p.sect].PgWidth - (double) this.e.TerSect[p.sect].LeftMargin - (double) this.e.TerSect[p.sect].RightMargin));
            this.e.frame[index1].flags = 1;
            this.e.frame[index1].height = 0;
            this.e.frame[index1].ScrHeight = 0;
            index2 = index1;
            ++this.e.TotalFrames;
            ++index1;
            this.InitFrame(index1);
            num1 = index1;
          }
          FirstLine = index4;
          while (true)
          {
            if (this.e.WmParaFID > 0 && !flag1)
            {
              this.CreateWatermarkFrame(PageNo, p.y, ref index1, p.TopSect, p.TopLeftMargin, p.HiddenY, p.HdrMargin, p.HdrHeight);
              flag1 = true;
            }
            int index9 = index4;
            while (index9 < index5 && (this.e.text[index9].tabw == null || (this.e.text[index9].tabw.type & 2) == 0))
              ++index9;
            int num12 = index9;
            if (num12 >= this.e.TotalLines)
              num12 = this.e.TotalLines - 1;
            if (num12 < 0)
              num12 = 0;
            if (InHdrFtr == 0)
            {
              if (!this.e.ViewPageHdrFtr && PageNo == this.e.CurPage)
                this.e.HdrFirstPageLine = index4;
              while ((this.e.PfmtId[this.e.text[index4].pfmt].flags & 12288 /*0x3000*/) != 0)
              {
                ++index4;
                if (index4 > num12)
                  break;
              }
              if (!this.e.ViewPageHdrFtr && PageNo == this.e.CurPage)
                this.e.HdrLastPageLine = index4 - 1;
              if (index4 <= num12)
                p.sect = index4 == 0 ? 0 : this.GetSection(index4);
              else
                break;
            }
            if (p.sect == 0)
            {
              TextWidth = PageWdth;
            }
            else
            {
              int num13 = 0;
              int num14 = 0;
              if ((double) this.e.TerSect[p.sect].LeftMargin != 0.0)
                num13 = (int) ((double) this.e.UnitResX * (double) this.e.TerSect[p.sect].LeftMargin);
              if ((double) this.e.TerSect[p.sect].RightMargin != 0.0)
                num14 = (int) ((double) this.e.UnitResX * (double) this.e.TerSect[p.sect].RightMargin);
              TextWidth = (int) ((double) this.e.UnitResX * (double) this.e.TerSect1[p.sect].PgWidth) - num13 - num14;
            }
            if (TextWidth > this.e.CurPageWidth)
              this.e.CurPageWidth = TextWidth;
            if (this.e.TerArg.FittedView)
              TextWidth = this.ScrToUnitX(this.e.TerWinWidth);
            int MaxColumns = this.e.TerSect[p.sect].columns;
            if (InHdrFtr != 0)
              MaxColumns = 1;
            if (MaxColumns == 1)
            {
              p.ColumnSpace = 0;
              p.ColumnWidth = TextWidth;
            }
            else
            {
              p.ColumnSpace = (int) ((double) this.e.UnitResX * (double) this.e.TerSect[p.sect].ColumnSpace);
              p.ColumnWidth = (TextWidth - (MaxColumns - 1) * p.ColumnSpace) / MaxColumns;
            }
            int num15 = (InHdrFtr & 4096 /*0x1000*/) == 0 ? ((InHdrFtr & 8192 /*0x2000*/) == 0 ? (this.e.TerSect[p.sect].LastPage != PageNo || this.e.TerSect1[p.sect].LastPageHeight <= 0 ? num3 - (p.y - this.e.FirstPageHeight) : this.e.TerSect1[p.sect].LastPageHeight) : this.PageFtrHeight(PageNo, false) + this.TwipsToUnitY(40)) : this.PageHdrHeight2(PageNo, false, true) + this.TwipsToUnitY(40);
            int num16 = index1;
            if (num1 < index1)
            {
              this.InitFrame(index1);
              num1 = index1;
            }
            p.FrameHt = 0;
            s.FrameHt = 0;
            p.CurColHeight = 0;
            s.CurColHeight = 0;
            p.MaxColHeight = 0;
            s.MaxColHeight = 0;
            p.CellX = 0;
            p.ColumnX = 0;
            if (this.e.BorderShowing)
              p.ColumnX = p.CellX = this.e.LeftBorderWidth + p.TopLeftMargin;
            FrameColX[0] = p.ColumnX;
            FrameColFirst[0] = index1;
            FrameColWidth[0] = p.ColumnWidth;
            int ColumnNo;
            int index10 = ColumnNo = 0;
            int FirstCellFrame = -1;
            int TableRowHeight = 0;
            int ScrTableRowHeight = 0;
            bool flag3 = true;
            int index11 = 0;
            int FrameFirstLine = -1;
            int CellFramed = 0;
            int pTableHt = 0;
            this.e.CurParaFrame = 0;
            int index12 = index4;
            bool flag4;
            int curParaFrame;
            while (true)
            {
              if (index12 >= this.e.TotalLines || pass != 3 || this.e.text[index12].page == PageNo || this.e.text[index12].cid == 0)
              {
                int CurLineHt = 0;
                if (index12 < this.e.TotalLines)
                  CurLineHt = this.e.text[index12].height;
                if (index12 < this.e.TotalLines)
                  this.e.text[index12].frame = -1;
                if (FrameFirstLine < 0)
                  FrameFirstLine = num2 = index12;
                int FrameLastLine = num2;
                num2 = index12;
                int num17;
                bool flag5 = (num17 = 0) != 0;
                bool flag6 = num17 != 0;
                bool flag7 = num17 != 0;
                bool RowBreak = num17 != 0;
                flag4 = num17 != 0;
                bool flag8;
                bool flag9 = flag8 = false;
                if (InHdrFtr == 0 && index12 < this.e.TotalLines && (this.e.PfmtId[this.e.text[index12].pfmt].flags & 12288 /*0x3000*/) != 0)
                  num12 = index12 - 1;
                int PrevCell = index11;
                index11 = index12 >= this.e.TotalLines || this.e.text[index12].cid == 0 ? 0 : this.LevelCell(0, index12);
                int row1 = this.e.cell[index11].row;
                int row2 = this.e.cell[PrevCell].row;
                bool flag10 = row2 > 0 && this.IsPartRow(true, row2, PageNo);
                bool flag11 = row2 > 0 && this.IsPartRow(false, row2, PageNo);
                curParaFrame = this.e.CurParaFrame;
                if (index12 < this.e.TotalLines)
                  this.e.CurParaFrame = this.e.text[index12].fid;
                else
                  this.e.CurParaFrame = 0;
                if (PrevCell > 0)
                  flag6 = true;
                else if (curParaFrame > 0)
                  flag9 = true;
                else
                  flag5 = true;
                if (index11 != PrevCell && index12 > index4)
                  flag7 = true;
                int num18 = row2;
                if (row1 != num18 && row2 != 0 && index12 > index4)
                  RowBreak = true;
                if (this.e.CurParaFrame != curParaFrame && index12 > index4)
                  flag8 = true;
                if (index12 > index4 && this.e.text[index12 - 1].tabw != null && (this.e.text[index12 - 1].tabw.type & 8) != 0 && index10 + 1 < MaxColumns)
                  flag4 = true;
                if (index12 > num12)
                {
                  if (flag10 | flag11)
                    RowBreak = true;
                  flag4 = true;
                  flag3 = false;
                }
                if (((index12 >= this.e.TotalLines ? 0 : ((this.e.text[index12].flags & 32 /*0x20*/) != 0 ? 1 : 0)) & (flag3 ? 1 : 0)) != 0)
                  flag4 = true;
                if (flag6 && !RowBreak)
                  flag4 = false;
                if (this.e.CurParaFrame > 0)
                  flag4 = false;
                if (flag4)
                  ++index10;
                if (flag4 | flag7 || RowBreak | flag8)
                {
                  if (flag6)
                  {
                    this.CreateCellFrame(0, ref index1, p.y, s.y, ref p.FrameHt, ref s.FrameHt, p.CurColHeight, s.CurColHeight, ref FirstCellFrame, ref TableRowHeight, ref ScrTableRowHeight, ref p.CellX, pTableRowIndent, PageNo, p.TopLeftMargin, ColumnNo, p.sect, PrevCell, ref CellFramed, FrameFirstLine, FrameLastLine, PassFlags, RowBreak, ref CellWidth, TextWidth, MaxColumns, p.ColumnSpace, PageWdth, p.ColumnX, p.ColumnWidth, BoxFrame, curParaFrame);
                    if (curParaFrame != this.e.CurParaFrame && curParaFrame > 0)
                      this.SetBoxFrameHt(BoxFrame, curParaFrame, p.CurColHeight + p.FrameHt, FrameLastLine);
                  }
                  else if (flag9)
                  {
                    bool flag12 = this.e.CurParaFrame != curParaFrame;
                    int num19 = this.e.AllTextAngle > 0 ? this.e.AllTextAngle : this.e.ParaFrame[curParaFrame].TextAngle;
                    if ((this.e.frame[BoxFrame].flags & 8) == 0)
                    {
                      this.e.frame[index1] = this.e.frame[BoxFrame].Copy();
                      this.e.frame[index1].empty = false;
                      this.e.ParaFrame[curParaFrame].TextLine = FrameFirstLine;
                      this.e.frame[index1].PageFirstLine = FrameFirstLine;
                      this.e.frame[index1].PageLastLine = FrameLastLine;
                      this.e.frame[index1].y = p.y + p.CurColHeight;
                      this.e.frame[index1].ScrY = s.y + s.CurColHeight;
                      this.e.frame[index1].x = p.ColumnX;
                      this.e.frame[index1].height = p.FrameHt;
                      this.e.frame[index1].SpaceTop = 0;
                      this.e.frame[index1].SpaceBot = 0;
                      if (num19 > 0)
                        this.e.frame[index1].width = this.e.frame[BoxFrame].height;
                      this.e.frame[index1].width -= this.e.frame[index1].SpaceLeft + this.e.frame[index1].SpaceRight;
                      this.e.frame[index1].ScrHeight = this.UnitToScrY(this.e.frame[index1].height);
                      this.e.frame[index1].ScrWidth = this.UnitToScrX(this.e.frame[index1].width);
                      for (int index13 = 0; index13 < 4; ++index13)
                        this.e.frame[index1].BorderWidth[index13] = 0;
                      this.e.frame[index1].border = 0;
                      this.e.frame[index1].SpaceRight = 0;
                      this.e.frame[index1].SpaceLeft = 0;
                      tc.ResetUintFlag(ref this.e.frame[index1].flags1, 2);
                      if (flag12)
                        this.SetBoxFrameHt(BoxFrame, curParaFrame, p.CurColHeight + p.FrameHt, FrameLastLine);
                      num4 = this.e.frame[index1].y + this.e.frame[index1].height;
                      num5 = this.e.frame[index1].ScrY + this.e.frame[index1].ScrHeight;
                      ++index1;
                      if (num1 < index1)
                      {
                        this.InitFrame(index1);
                        num1 = index1;
                      }
                    }
                  }
                  else
                  {
                    this.e.frame[index1].empty = false;
                    this.e.frame[index1].sect = p.sect;
                    this.e.frame[index1].PageFirstLine = FrameFirstLine;
                    this.e.frame[index1].PageLastLine = FrameLastLine;
                    this.e.frame[index1].y = p.y + p.CurColHeight;
                    this.e.frame[index1].ScrY = s.y + s.CurColHeight;
                    this.e.frame[index1].x = p.ColumnX;
                    this.e.frame[index1].width = p.ColumnWidth;
                    if (MaxColumns == 1 || ColumnNo + 1 == MaxColumns)
                    {
                      if (this.e.BorderShowing)
                      {
                        this.e.frame[index1].width = PageWdth - this.e.LeftBorderWidth - this.e.frame[index1].x;
                        this.e.frame[index1].SpaceRight = TopRightMargin;
                      }
                      else
                        this.e.frame[index1].flags |= 1;
                      if (ColumnNo + 1 == MaxColumns)
                        this.e.frame[index1].flags |= 512 /*0x0200*/;
                    }
                    else
                      this.e.frame[index1].width += p.ColumnSpace;
                    this.e.frame[index1].height = p.FrameHt;
                    this.e.frame[index1].ScrHeight = s.FrameHt;
                    this.e.frame[index1].TextHeight = s.FrameHt;
                    if (flag4)
                      this.e.frame[index1].LastColumnFrame = true;
                    ++index1;
                    if (num1 < index1)
                    {
                      this.InitFrame(index1);
                      num1 = index1;
                    }
                  }
                  FrameFirstLine = num2;
                  if (flag6 && !RowBreak)
                  {
                    int num20;
                    s.FrameHt = num20 = 0;
                    p.FrameHt = num20;
                  }
                  if (InHdrFtr == 0 && index12 < this.e.TotalLines && p.CurColHeight + p.FrameHt + CurLineHt > num15 && (p.CurColHeight > 0 || p.FrameHt > 0) && num12 == index5 && index12 <= num12 && index10 >= MaxColumns && !this.e.repaginating && (this.e.PageInfo[PageNo + 1].flags & 1) == 0)
                  {
                    this.e.PageInfo[PageNo].LastLine = index12 - 1;
                    this.e.PageInfo[PageNo + 1].FirstLine = index12;
                    tc.ResetUintFlag(ref this.e.PageInfo[PageNo + 1].flags, 1);
                    num12 = index5 = index12 - 1;
                  }
                  p.CurColHeight += p.FrameHt;
                  s.CurColHeight += s.FrameHt;
                  if (p.CurColHeight > p.MaxColHeight)
                    p.MaxColHeight = p.CurColHeight;
                  if (s.CurColHeight > s.MaxColHeight)
                    s.MaxColHeight = s.CurColHeight;
                  if (flag4)
                  {
                    s.CurColHeight = 0;
                    p.CurColHeight = 0;
                  }
                  s.FrameHt = 0;
                  p.FrameHt = 0;
                  if (flag4)
                    p.ColumnX = p.CellX = p.ColumnX + p.ColumnWidth + p.ColumnSpace;
                  if (flag6)
                    p.CellX += CellWidth;
                  if (index11 == 0)
                    p.CellX = p.ColumnX;
                  if (RowBreak)
                    p.CellX = p.ColumnX;
                  if (flag4 && !flag5)
                  {
                    int index14 = index1 - 1;
                    while (index14 >= num16 && (this.e.frame[index14].ParaFrameId != 0 || this.e.frame[index14].CellId != 0))
                      --index14;
                    if (index14 >= num16)
                    {
                      this.e.frame[index1].empty = true;
                      this.e.frame[index1].LastColumnFrame = true;
                      this.e.frame[index1].y = this.e.frame[index14].y + this.e.frame[index14].height;
                      this.e.frame[index1].ScrY = this.e.frame[index14].ScrY + this.e.frame[index14].ScrHeight;
                      this.e.frame[index1].x = this.e.frame[index14].x;
                      this.e.frame[index1].width = this.e.frame[index14].width;
                      this.e.frame[index1].height = 0;
                      this.e.frame[index1].ScrHeight = 0;
                      this.e.frame[index1].sect = this.e.frame[index1 - 1].sect;
                      if (!this.e.BorderShowing && (MaxColumns == 1 || index10 == MaxColumns))
                        this.e.frame[index1].flags |= 1;
                      ++index1;
                      if (num1 < index1)
                      {
                        this.InitFrame(index1);
                        num1 = index1;
                      }
                    }
                  }
                  ColumnNo = index10;
                  if (flag4)
                  {
                    FrameColFirst[ColumnNo] = index1;
                    FrameColX[ColumnNo] = p.ColumnX;
                    FrameColWidth[ColumnNo] = p.ColumnWidth;
                  }
                  if (index12 > num12)
                    break;
                }
                if (this.e.CurParaFrame != curParaFrame)
                {
                  this.SwitchParaFrames(curParaFrame, ref p, ref s, ref pSavePrt, ref pSaveScr, ref index1, index12, PassFlags, PageNo);
                  BoxFrame = (this.e.CurParaFrame > 0 ? 1 : 0) != 0 ? index1 - 1 : 0;
                }
                if (index12 < this.e.TotalLines && this.TableLevel(index12) > 0)
                {
                  int index15 = this.LevelCell(0, index12);
                  int row3 = this.e.cell[index15].row;
                  int ColumnX = p.ColumnX + this.TwipsToUnitX(this.e.TableRow[row3].indent);
                  for (int index16 = this.e.TableRow[row3].FirstCell; index16 > 0 && index16 != index15; index16 = this.e.cell[index16].NextCell)
                    ColumnX += this.TwipsToUnitX(this.e.cell[index16].width);
                  index12 = this.CreateSubTableFrames(index12, 0, ref index1, ref pTableHt, ColumnX, PageNo, p.sect, p.y + p.CurColHeight + p.FrameHt, s.y + s.CurColHeight + s.FrameHt, p.HiddenY, p.HdrMargin, p.HdrHeight, p.TopSect, p.TopLeftMargin, ColumnNo, ref pHasPictFrames, pass, BoxFrame, this.e.text[index12].fid);
                  p.FrameHt += pTableHt;
                  s.FrameHt += this.UnitToScrY(pTableHt);
                }
                else if (index12 < this.e.TotalLines)
                  this.SetFrameLineInfo(index12, p.y, s.y, ref p.FrameHt, ref s.FrameHt, p.CurColHeight, p.CellX, ref pTableRowIndent, PageNo, p.HiddenY, p.HdrMargin, p.HdrHeight, p.TopSect, p.TopLeftMargin, ColumnNo, ref pHasPictFrames, p.sect, CurLineHt);
              }
              ++index12;
            }
            if (!flag4)
            {
              ++index10;
              if (!this.e.BorderShowing)
                this.e.frame[index1 - 1].flags |= 1;
            }
            if (curParaFrame > 0)
              this.SwitchParaFrames(curParaFrame, ref p, ref s, ref pSavePrt, ref pSaveScr, ref index1, index12, PassFlags, PageNo);
            this.e.TotalFrames = index1;
            int num21;
            int num22 = num21 = 0;
            for (int index17 = num16; index17 < this.e.TotalFrames; ++index17)
            {
              if (this.e.frame[index17].LastColumnFrame && this.e.frame[index17].y + this.e.frame[index17].height > num22)
                num22 = this.e.frame[index17].y + this.e.frame[index17].height;
              if (this.e.frame[index17].LastColumnFrame && this.e.frame[index17].ScrY + this.e.frame[index17].ScrHeight > num21)
                num21 = this.e.frame[index17].ScrY + this.e.frame[index17].ScrHeight;
            }
            if (pass == 3 && index10 >= 2)
            {
              int num23 = !this.e.BorderShowing ? (!this.e.ViewPageHdrFtr ? num3 : num3 - p.FtrMargin) : (int) ((double) this.e.UnitResY * (double) this.e.TerSect1[p.TopSect].PgHeight) + this.e.TopBorderHeight - p.FtrMargin - p.FtrHeight;
              if (num22 > this.e.FirstPageHeight + num23)
                num22 = this.e.FirstPageHeight + num23;
            }
            for (int index18 = num16; index18 < this.e.TotalFrames; ++index18)
            {
              if (this.e.frame[index18].LastColumnFrame)
              {
                this.e.frame[index18].height = num22 - this.e.frame[index18].y;
                this.e.frame[index18].ScrHeight = num21 - this.e.frame[index18].ScrY;
              }
            }
            if (index10 < MaxColumns)
            {
              this.e.frame[index1].empty = true;
              this.e.frame[index1].y = p.y;
              this.e.frame[index1].ScrY = s.y;
              this.e.frame[index1].x = p.ColumnX;
              this.e.frame[index1].width = PageWdth - this.e.LeftBorderWidth - p.ColumnX;
              this.e.frame[index1].height = p.MaxColHeight;
              this.e.frame[index1].ScrHeight = s.MaxColHeight;
              this.e.frame[index1].sect = p.sect;
              this.e.frame[index1].LastColumnFrame = true;
              if (!this.e.BorderShowing)
                this.e.frame[index1].flags |= 1;
              p.ColumnX += this.e.frame[index1].width;
              FrameColWidth[index10] = p.ColumnWidth * (MaxColumns - index10) + p.ColumnSpace * (MaxColumns - index10 - 1);
              MaxColumns = index10 + 1;
              ++index1;
              this.InitFrame(index1);
              num1 = index1;
              this.e.TotalFrames = index1;
            }
            if (MaxColumns > 1 && this.IsParaRtl(0, 0, this.e.TerSect[p.sect].flow, this.e.DocTextFlow))
              this.MapRtlCol(FrameColFirst, FrameColX, FrameColWidth, p.ColumnSpace, index1, MaxColumns);
            p.y += p.MaxColHeight;
            s.y += s.MaxColHeight;
            if (num12 < index5)
              index4 = num12 + 1;
            else
              break;
          }
label_284:
          p.sect = p.TopSect;
          if (pHasPictFrames)
            this.CreatePictFrames(PageNo, p.HiddenY, p.HdrMargin, p.TopLeftMargin, p.HdrHeight, p.TopSect, FirstLine, index5, InHdrFtr);
          if (pass == 3)
          {
            int num24 = y2;
            for (int index19 = num11; index19 < this.e.TotalFrames; ++index19)
            {
              if (!this.e.frame[index19].empty && this.e.frame[index19].height != 0)
              {
                int index20 = this.e.frame[index19].ParaFrameId == 0 ? index19 : (this.e.ParaFrame[this.e.frame[index19].ParaFrameId].pict != 0 ? index19 : this.e.frame[index19].BoxFrame);
                int num25 = this.e.frame[index20].y + this.e.frame[index20].height;
                if (num25 > num24)
                  num24 = num25;
              }
            }
            this.e.PageInfo[PageNo].BodyTextHt = num24 - y2;
          }
        }
        while (pass == 2);
        if (num1 < this.e.TotalFrames)
        {
          this.InitFrame(this.e.TotalFrames);
          int totalFrames2 = this.e.TotalFrames;
        }
        this.e.frame[this.e.TotalFrames].empty = true;
        this.e.frame[this.e.TotalFrames].y = p.y;
        this.e.frame[this.e.TotalFrames].ScrY = s.y;
        this.e.frame[this.e.TotalFrames].width = TextWidth;
        this.e.frame[this.e.TotalFrames].x = 0;
        if (this.e.TotalFrames == 0)
          this.e.frame[this.e.TotalFrames].sect = 0;
        else
          this.e.frame[this.e.TotalFrames].sect = this.e.frame[this.e.TotalFrames - 1].sect;
        int x1;
        if (this.e.BorderShowing)
        {
          this.e.frame[this.e.TotalFrames].x = this.e.LeftBorderWidth + p.TopLeftMargin;
          this.e.frame[this.e.TotalFrames].width += TopRightMargin;
          if ((InHdrFtr & 4096 /*0x1000*/) != 0)
            x1 = this.e.TopBorderHeight + p.HdrMargin + p.HdrHeight;
          else if ((InHdrFtr & 8192 /*0x2000*/) != 0)
          {
            x1 = (int) ((double) this.e.UnitResY * (double) this.e.TerSect1[p.sect].PgHeight) + this.e.TopBorderHeight - p.FtrMargin;
          }
          else
          {
            x1 = (int) ((double) this.e.UnitResY * (double) this.e.TerSect1[p.sect].PgHeight) + this.e.TopBorderHeight - p.FtrMargin - p.FtrHeight;
            if (!this.e.ViewPageHdrFtr)
              x1 += p.FtrHeight;
          }
          if ((InHdrFtr & 4096 /*0x1000*/) != 0)
            this.e.frame[this.e.TotalFrames].flags |= 2;
          if (this.e.ViewPageHdrFtr && InHdrFtr == 0)
            this.e.frame[this.e.TotalFrames].flags |= 2;
        }
        else
        {
          x1 = (InHdrFtr & 4096 /*0x1000*/) == 0 ? ((InHdrFtr & 8192 /*0x2000*/) == 0 ? (!this.e.ViewPageHdrFtr ? num3 : num3 + p.HiddenY - p.FtrMargin - p.FtrHeight) : num3) : p.HdrMargin - p.HiddenY + p.HdrHeight;
          this.e.frame[this.e.TotalFrames].flags |= 3;
          if (this.e.PageInfo[PageNo].FnoteHt > 0 && InHdrFtr == 0)
            tc.ResetUintFlag(ref this.e.frame[this.e.TotalFrames].flags, 2);
        }
        if ((this.e.TerFlags2 & 8192 /*0x2000*/) != 0 && !this.e.InPrinting || this.e.TerArg.FittedView && (PageNo >= this.e.TotalPages - 1 || (this.e.PageInfo[PageNo + 1].flags & 1) == 0))
        {
          this.e.frame[this.e.TotalFrames].ScrHeight = 0;
          this.e.frame[this.e.TotalFrames].height = 0;
          if (p.y < num4)
            this.e.frame[this.e.TotalFrames].height = num4 - p.y;
          if (s.y < num5)
            this.e.frame[this.e.TotalFrames].ScrHeight = num5 - s.y;
        }
        else
        {
          this.e.frame[this.e.TotalFrames].height = x1 - (p.y - this.e.FirstPageHeight);
          this.e.frame[this.e.TotalFrames].ScrHeight = this.UnitToScrY(x1) - (s.y - num8);
          if (InHdrFtr == 0 && this.e.PageInfo[PageNo].FnoteHt > 0)
          {
            this.e.frame[this.e.TotalFrames].height -= this.e.PageInfo[PageNo].FnoteHt;
            this.e.frame[this.e.TotalFrames].ScrHeight -= this.UnitToScrY(this.e.PageInfo[PageNo].FnoteHt);
          }
          if (this.e.frame[this.e.TotalFrames].height < this.TwipsToUnitY(40))
            this.e.frame[this.e.TotalFrames].height = this.TwipsToUnitY(40);
          if (this.e.frame[this.e.TotalFrames].ScrHeight < this.TwipsToScrY(40))
            this.e.frame[this.e.TotalFrames].ScrHeight = this.TwipsToScrY(40);
        }
        p.y += this.e.frame[this.e.TotalFrames].height;
        s.y += this.e.frame[this.e.TotalFrames].ScrHeight;
        if (pass == 3 && index2 >= 0 && this.e.frame[this.e.TotalFrames].height > 0)
        {
          int height = this.e.frame[this.e.TotalFrames].height;
          if ((this.e.TerSect[p.TopSect].flags & 128 /*0x80*/) != 0)
            height /= 2;
          this.e.frame[index2].height = height;
          this.e.frame[index2].ScrHeight = this.UnitToScrY(height);
          for (int index21 = index2 + 1; index21 <= this.e.TotalFrames; ++index21)
          {
            this.e.frame[index21].y += height;
            this.e.frame[index21].ScrY += this.UnitToScrY(height);
          }
          this.e.frame[this.e.TotalFrames].height -= height;
          this.e.frame[this.e.TotalFrames].ScrHeight -= this.UnitToScrY(height);
        }
        ++this.e.TotalFrames;
        this.InitFrame(this.e.TotalFrames);
        num1 = this.e.TotalFrames;
        if (InHdrFtr == 0 && this.e.PageInfo[PageNo].FnoteHt > 0)
          this.CreateFnoteFrame(PageNo, ref p.y, ref s.y, TextWidth, p.TopLeftMargin, TopRightMargin);
        tc.ResetUintFlag(ref this.e.PageInfo[PageNo].flags, 8);
        if ((this.e.BorderShowing || this.e.InPrinting) && (!this.e.ViewPageHdrFtr || (InHdrFtr & 8192 /*0x2000*/) != 0))
          this.CreatePageBox(PageNo, PageTopX, PageTopY, p.TopSect, pLeftFrame);
        if (this.e.BorderShowing && (!this.e.ViewPageHdrFtr || (InHdrFtr & 8192 /*0x2000*/) != 0))
          this.CreatePageBorderBot(PageNo, ref p.y, ref s.y, pLeftFrame, pRightFrame);
        if (pass == 1 && (this.e.TerSect[p.TopSect].flags & 8) != 0)
        {
          int x2 = this.PageHdrHeight2(PageNo, true, true) - this.PageHdrHeight2(PageNo, false, true);
          p.y -= x2;
          s.y -= this.UnitToScrY(x2);
          if (x2 != 0)
            this.e.HasOverlayingFrames = true;
        }
        if (pass == 3 && (this.e.TerSect[p.TopSect].flags & 16 /*0x10*/) != 0)
        {
          int x3 = this.PageFtrHeight(PageNo, true) - this.PageFtrHeight(PageNo, false);
          p.y -= x3;
          s.y -= this.UnitToScrY(x3);
          if (x3 != 0)
            this.e.HasOverlayingFrames = true;
        }
        continue;
label_48:;
      }
      while (pass < 4);
label_344:
      if (PageNo < LastPage || this.e.FirstFramePage == LastPage)
      {
        this.e.FirstPage2Frame = this.e.TotalFrames;
        if (this.e.TotalFrames > 0)
        {
          this.e.FirstPageHeight = this.e.frame[this.e.TotalFrames - 1].y + this.e.frame[this.e.TotalFrames - 1].height;
          num8 = this.e.frame[this.e.TotalFrames - 1].ScrY + this.e.frame[this.e.TotalFrames - 1].ScrHeight;
        }
        else
          this.e.FirstPageHeight = num8 = 0;
      }
      if (PageNo == this.e.FirstFramePage || this.e.TotalFrames == 0)
        this.e.PageInfo[PageNo].ScrHt = num8;
      else
        this.e.PageInfo[PageNo].ScrHt = this.e.frame[this.e.TotalFrames - 1].ScrY + this.e.frame[this.e.TotalFrames - 1].ScrHeight - num8;
      this.e.PageInfo[PageNo].TextHt = 0;
      for (int index22 = totalFrames1; index22 < this.e.TotalFrames; ++index22)
      {
        bool flag13 = !this.e.frame[index22].empty && (this.e.frame[index22].flags & 4096 /*0x1000*/) == 0 && this.e.frame[index22].ScrY + this.e.frame[index22].TextHeight > this.e.PageInfo[PageNo].TextHt;
        if ((this.e.frame[index22].flags & 8192 /*0x2000*/) != 0)
          flag13 = true;
        if (this.e.frame[index22].ParaFrameId != 0)
          this.e.frame[index22].TextHeight = this.e.frame[index22].ScrHeight;
        if (flag13)
          this.e.PageInfo[PageNo].TextHt = this.e.frame[index22].ScrY + this.e.frame[index22].TextHeight;
      }
      if (totalFrames1 >= this.e.FirstPage2Frame)
        this.e.PageInfo[PageNo].TextHt -= this.UnitToScrY(this.e.FirstPageHeight);
      if (frameCount != this.e.PageInfo[PageNo].FrameCount)
        this.e.PageInfo[PageNo].flags |= 4;
      ++PageNo;
    }
    while (this.e.FullRenderMode && PageNo <= LastPage && PageNo < this.e.TotalPages);
    for (int index = 0; index < this.e.TotalFrames; ++index)
    {
      if (this.e.BorderShowing)
      {
        int num26 = (int) ((double) this.e.UnitResX * (double) this.e.TerSect1[p.TopSect].PgWidth);
        int unitResX = this.e.UnitResX;
        ref tc.StrSect local = ref this.e.TerSect[this.e.frame[index].sect];
        p.TopLeftMargin = index < this.e.FirstPage2Frame ? num6 : num7;
        if (this.e.frame[index].ParaFrameId == 0 && this.e.frame[index].level == 0 && this.e.frame[index].x == this.e.LeftBorderWidth + p.TopLeftMargin && (this.e.frame[index].CellId == 0 || this.e.frame[index].empty))
        {
          this.e.frame[index].x = this.e.LeftBorderWidth;
          this.e.frame[index].SpaceLeft += p.TopLeftMargin;
          this.e.frame[index].width += p.TopLeftMargin;
          if (this.e.frame[index].width > num26)
            this.e.frame[index].width = num26;
        }
      }
      if (this.e.frame[index].CellId > 0 && !this.e.frame[index].empty)
      {
        int cellId = this.e.frame[index].CellId;
        ref tc.StrCell local = ref this.e.cell[cellId];
        int PageNo1 = index >= this.e.FirstPage2Frame ? this.e.FirstFramePage + 1 : this.e.FirstFramePage;
        if (this.e.cell[cellId].RowSpan > 1 || (this.e.cell[cellId].flags & 16 /*0x10*/) != 0)
        {
          int spannedRowHeight = this.GetSpannedRowHeight(cellId, out tc.SkipInt, PageNo1);
          this.e.frame[index].height += spannedRowHeight;
          this.e.frame[index].ScrHeight += this.UnitToScrY(spannedRowHeight);
        }
        if ((this.e.cell[cellId].flags & 16 /*0x10*/) != 0)
        {
          int spanningCell = this.e.CellAux[cellId].SpanningCell;
          if (spanningCell > 0 && spanningCell < this.e.TotalCells && this.e.CellAux[spanningCell].LastPage < PageNo1)
            this.e.frame[index].flags |= 32768 /*0x8000*/;
        }
        if (this.e.cell[cellId].TextAngle != 0)
        {
          this.e.frame[index].SpaceLeft = this.e.frame[index].SpaceTop;
          this.e.frame[index].SpaceRight = this.e.frame[index].SpaceBot;
        }
        if ((this.e.cell[cellId].flags & 77824 /*0x013000*/) != 0)
        {
          int num27 = 0;
          bool flag = false;
          for (int pageFirstLine = this.e.frame[index].PageFirstLine; pageFirstLine <= this.e.frame[index].PageLastLine; ++pageFirstLine)
          {
            num27 += this.e.text[pageFirstLine].height;
            if (this.TableLevel(pageFirstLine) > this.e.frame[index].level)
              flag = true;
          }
          if (!flag)
          {
            if ((this.e.cell[cellId].flags & 65536 /*0x010000*/) != 0)
            {
              this.e.frame[index].SpaceTop += this.e.CellAux[cellId].SpaceBefore;
            }
            else
            {
              int num28 = this.e.cell[cellId].TextAngle != 0 ? this.e.frame[index].width - num27 - this.e.frame[index].SpaceLeft - this.e.frame[index].SpaceRight : this.e.frame[index].height - num27 - this.e.frame[index].SpaceTop - this.e.frame[index].SpaceBot;
              if (num28 > 0)
              {
                if ((this.e.cell[cellId].flags & 4096 /*0x1000*/) != 0)
                  this.e.frame[index].SpaceTop += num28 / 2;
                else
                  this.e.frame[index].SpaceTop += num28;
              }
            }
          }
        }
        this.e.CellAux[cellId].FrameId = index;
      }
      if (this.e.TerArg.FittedView && this.e.frame[index].CellId == 0 && !this.e.frame[index].empty && !this.e.InPrinting)
      {
        for (int pageFirstLine = this.e.frame[index].PageFirstLine; pageFirstLine <= this.e.frame[index].PageLastLine; ++pageFirstLine)
        {
          int curCfmt = this.GetCurCfmt(pageFirstLine, 0);
          if ((this.e.TerFlags & 2048 /*0x0800*/) != 0 || (this.e.PfmtId[this.e.text[pageFirstLine].pfmt].pflags & 16 /*0x10*/) != 0)
          {
            int lineWidth = this.GetLineWidth(pageFirstLine, false, false);
            if (this.e.frame[index].width < lineWidth)
              this.e.frame[index].width = lineWidth;
          }
          else if (curCfmt >= 0 && curCfmt < this.e.TotalFonts && (this.e.TerFont[curCfmt].style & 128 /*0x80*/) != 0)
          {
            int unitX = this.TwipsToUnitX(this.e.TerFont[curCfmt].PictWidth);
            if (this.e.frame[index].width < unitX)
              this.e.frame[index].width = unitX;
          }
        }
      }
      if (!this.e.frame[index].empty && (this.e.frame[index].flags & 2048 /*0x0800*/) == 0)
      {
        for (int pageFirstLine = this.e.frame[index].PageFirstLine; pageFirstLine <= this.e.frame[index].PageLastLine; ++pageFirstLine)
        {
          if (this.e.frame[index].CellId == this.e.text[pageFirstLine].cid)
            this.e.text[pageFirstLine].frame = index;
        }
      }
      if (!this.e.frame[index].empty && this.e.ViewPageHdrFtr && !this.e.EditPageHdrFtr && (this.e.PfmtId[this.e.text[this.e.frame[index].PageFirstLine].pfmt].flags & 12288 /*0x3000*/) != 0)
        this.e.frame[index].flags |= 4096 /*0x1000*/;
      if ((this.e.TerFlags3 & 2097152 /*0x200000*/) != 0)
        this.e.frame[index].border = 15;
      this.e.frame[index].OrigX = this.e.frame[index].x;
    }
    for (int frm = 0; frm < this.e.TotalFrames; ++frm)
      this.MapRtlCell(frm);
    for (int index = 0; index < this.e.TotalFrames; ++index)
    {
      this.e.frame[index].ScrFirstLine = -1;
      this.e.frame[index].ScrLastLine = -1;
      this.e.frame[index].RowOffset = 0;
      this.e.frame[index].ScrWidth = 0;
      if (!this.e.frame[index].empty)
        this.e.text[this.e.frame[index].PageFirstLine].flags |= 4096 /*0x1000*/;
    }
    this.e.RowHeight[0] = 0;
    this.e.RowY[0] = 0;
    this.e.RowX[0] = 0;
    this.e.CurPageHeight = 0;
    for (int index = 0; index < this.e.TotalFrames; ++index)
    {
      int num29 = this.e.frame[index].y + this.e.frame[index].height;
      if (num29 > this.e.CurPageHeight)
        this.e.CurPageHeight = num29;
    }
    this.SortFrames();
    if (!printer)
      this.CreateFramesScr();
    if (!this.e.TerArg.PageMode)
    {
      this.e.TotalFrames = 1;
      this.InitFrame(0);
      this.e.frame[0].ScrLastLine = this.e.BeginLine;
      this.e.frame[0].ScrFirstLine = this.e.BeginLine;
      this.e.frame[0].RowOffset = 0;
      this.e.frame[0].PageFirstLine = 0;
      this.e.frame[0].PageLastLine = this.e.TotalLines - 1;
      this.e.frame[0].ScrLastLine = this.e.BeginLine;
      this.e.frame[0].ScrFirstLine = this.e.BeginLine;
    }
    if (!tc.DebugMode)
      return;
    this.misc.dm("CreateFrames - End");
  }

  internal void CreateFramesScr()
  {
    int num1 = 0;
    int firstFramePage = this.e.FirstFramePage;
    int num2 = 0;
    bool flag1 = true;
    this.e.CurPageHeight = 0;
    this.e.CurTextHeight = 0;
    for (int index = 0; index < this.e.TotalFrames; ++index)
    {
      if (this.e.FullRenderMode || !this.e.frame[index].empty && this.e.frame[index].height != 0)
      {
        int x1 = this.e.frame[index].x;
        if (this.e.frame[index].y != 0)
          this.e.frame[index].ScrY = this.MulDiv(this.e.frame[index].y, this.e.ScrResY, this.e.UnitResY);
        else
          this.e.frame[index].ScrY = 0;
        int x2 = this.e.frame[index].y + this.e.frame[index].height;
        if (x2 != 0)
          this.e.frame[index].ScrHeight = this.MulDiv(x2, this.e.ScrResY, this.e.UnitResY) - this.e.frame[index].ScrY;
        else
          this.e.frame[index].ScrHeight = -this.e.frame[index].ScrY;
        if (this.e.frame[index].level > 0 && this.e.frame[index].CellId > 0 && !this.e.frame[index].empty)
        {
          int cellId = this.e.frame[index].CellId;
          int row = this.e.cell[cellId].row;
          if (this.e.cell[cellId].RowSpan == 1 && (this.e.cell[cellId].flags & 16 /*0x10*/) == 0 && this.e.TableRow[row].NextRow <= 0)
            this.e.TableRow[row].height = this.ScrToUnitY(this.e.frame[index].ScrHeight);
        }
        if (!this.e.frame[index].empty && (this.e.frame[index].flags & 2048 /*0x0800*/) == 0 && this.e.frame[index].PageFirstLine >= 0)
        {
          int x3 = this.e.frame[index].y + this.e.frame[index].SpaceTop;
          for (int pageFirstLine = this.e.frame[index].PageFirstLine; pageFirstLine <= this.e.frame[index].PageLastLine; ++pageFirstLine)
          {
            if (this.TableLevel(pageFirstLine) == this.e.frame[index].level)
            {
              if (this.e.UnitResY == this.e.ScrResY)
              {
                this.e.text[pageFirstLine].ScrHt = this.e.text[pageFirstLine].height;
              }
              else
              {
                int x4 = x3 + this.e.text[pageFirstLine].height;
                this.e.text[pageFirstLine].ScrHt = this.MulDiv(x4, this.e.ScrResY, this.e.UnitResY) - this.MulDiv(x3, this.e.ScrResY, this.e.UnitResY);
                x3 = x4;
              }
            }
          }
        }
        if (this.e.frame[index].RowId > 0 && (this.e.frame[index].flags & 131072 /*0x020000*/) != 0)
        {
          this.e.frame[index].width = this.MulDiv(this.e.frame[index].width, this.e.ScrResX, this.e.UnitResX);
          if ((this.e.frame[index].flags & 524288 /*0x080000*/) != 0)
          {
            if (this.e.frame[index].x != 0)
              this.e.frame[index].x = this.MulDiv(this.e.frame[index].x, this.e.ScrResX, this.e.UnitResX);
          }
          else
            this.e.frame[index].x = this.e.frame[index - 1].x - this.e.frame[index].width;
        }
        else
        {
          if (index > 0 && this.e.frame[index].RowId > 0 && this.e.frame[index - 1].RowId == this.e.frame[index].RowId)
            this.e.frame[index].x = this.e.frame[index - 1].x + this.e.frame[index - 1].width;
          else if (this.e.frame[index].x != 0)
            this.e.frame[index].x = this.MulDiv(this.e.frame[index].x, this.e.ScrResX, this.e.UnitResX);
          this.e.frame[index].width = this.MulDiv(x1 + this.e.frame[index].width, this.e.ScrResX, this.e.UnitResX) - this.e.frame[index].x;
        }
        this.e.frame[index].ScrX = this.e.frame[index].x;
        this.e.frame[index].ScrWidth = this.e.frame[index].width;
        this.e.frame[index].y = this.e.frame[index].ScrY;
        this.e.frame[index].height = this.e.frame[index].ScrHeight;
        this.e.frame[index].ScrX = this.e.frame[index].x;
        this.e.frame[index].ScrWidth = this.e.frame[index].width;
        this.e.frame[index].y = this.e.frame[index].ScrY;
        this.e.frame[index].height = this.e.frame[index].ScrHeight;
        if (this.e.frame[index].SpaceTop != 0)
          this.e.frame[index].SpaceTop = this.MulDiv(this.e.frame[index].SpaceTop, this.e.ScrResY, this.e.UnitResY);
        if (this.e.frame[index].SpaceBot != 0)
          this.e.frame[index].SpaceBot = this.MulDiv(this.e.frame[index].SpaceBot, this.e.ScrResY, this.e.UnitResY);
        if (this.e.frame[index].SpaceLeft != 0)
          this.e.frame[index].SpaceLeft = this.MulDiv(this.e.frame[index].SpaceLeft, this.e.ScrResX, this.e.UnitResX);
        if (this.e.frame[index].SpaceRight != 0)
          this.e.frame[index].SpaceRight = this.MulDiv(this.e.frame[index].SpaceRight, this.e.ScrResX, this.e.UnitResX);
        if ((!this.e.frame[index].empty || (this.e.frame[index].flags & 8192 /*0x2000*/) != 0) && this.e.frame[index].y + this.e.frame[index].TextHeight > this.e.CurTextHeight)
          this.e.CurTextHeight = this.e.frame[index].y + this.e.frame[index].TextHeight;
        if (this.e.frame[index].y + this.e.frame[index].height > this.e.CurPageHeight)
          this.e.CurPageHeight = this.e.frame[index].y + this.e.frame[index].height;
        if (index < this.e.FirstPage2Frame)
          this.e.FirstPageHeight = this.e.frame[index].y + this.e.frame[index].height;
        if (((num2 != 0 ? 0 : (index >= this.e.FirstPage2Frame ? 1 : 0)) & (flag1 ? 1 : 0)) != 0)
        {
          this.e.PageInfo[firstFramePage].TextHt = num2 = num1;
          this.e.PageInfo[firstFramePage].ScrHt = this.e.PageInfo[firstFramePage].TextHt;
          ++firstFramePage;
          num1 = 0;
          flag1 = false;
        }
        bool flag2 = !this.e.frame[index].empty && (this.e.frame[index].flags & 4096 /*0x1000*/) == 0 && this.e.frame[index].ScrY + this.e.frame[index].TextHeight > num1;
        if ((this.e.frame[index].flags & 8192 /*0x2000*/) != 0)
          flag2 = true;
        if (flag2)
          num1 = this.e.frame[index].ScrY + this.e.frame[index].TextHeight;
      }
    }
    this.e.PageInfo[firstFramePage].TextHt = num1;
    if (firstFramePage > this.e.FirstFramePage)
      this.e.PageInfo[firstFramePage].TextHt -= num2;
    this.e.PageInfo[firstFramePage].ScrHt = this.e.PageInfo[firstFramePage].TextHt;
  }

  internal bool CreatePageBorderBot(
    int PageNo,
    ref int pY,
    ref int pScrY,
    int LeftFrame,
    int RightFrame)
  {
    int num1 = pY;
    int num2 = pScrY;
    int totalFrames = this.e.TotalFrames;
    int section = this.GetSection(this.e.PageInfo[PageNo].FirstLine);
    this.e.frame[totalFrames].empty = true;
    this.e.frame[totalFrames].y = num1;
    this.e.frame[totalFrames].ScrY = num2;
    this.e.frame[totalFrames].x = this.e.frame[LeftFrame].x + this.e.frame[LeftFrame].width;
    this.e.frame[totalFrames].width = this.e.frame[RightFrame].x - this.e.frame[totalFrames].x;
    this.e.frame[totalFrames].height = (int) ((double) this.e.UnitResY * (double) this.e.TerSect[section].FtrMargin);
    this.e.frame[totalFrames].ScrHeight = this.UnitToScrY(this.e.frame[totalFrames].height);
    int num3 = num1 + this.e.frame[totalFrames].height;
    int num4 = num2 + this.e.frame[totalFrames].ScrHeight;
    ++this.e.TotalFrames;
    int FrameNo = totalFrames + 1;
    this.InitFrame(FrameNo);
    int num5;
    this.e.frame[RightFrame].height = num5 = num3 - this.e.frame[LeftFrame].y;
    this.e.frame[LeftFrame].height = num5;
    int num6;
    this.e.frame[RightFrame].ScrHeight = num6 = num4 - this.e.frame[LeftFrame].ScrY;
    this.e.frame[LeftFrame].ScrHeight = num6;
    this.e.frame[FrameNo].empty = true;
    this.e.frame[FrameNo].y = num3;
    this.e.frame[FrameNo].ScrY = num4;
    this.e.frame[FrameNo].x = 0;
    this.e.frame[FrameNo].width = (int) ((double) this.e.UnitResX * (double) this.e.TerSect1[section].PgWidth) + 2 * this.e.LeftBorderWidth;
    this.e.frame[FrameNo].LeftBorderWdth = this.e.LeftBorderWidth;
    this.e.frame[FrameNo].height = PageNo + 1 >= this.e.TotalPages ? this.e.TopBorderHeight : this.e.BotBorderHeight;
    this.e.frame[FrameNo].ScrHeight = this.UnitToScrY(this.e.frame[FrameNo].height);
    int num7 = num3 + this.e.frame[FrameNo].height;
    int num8 = num4 + this.e.frame[FrameNo].ScrHeight;
    this.e.frame[FrameNo].flags = 259;
    ++this.e.TotalFrames;
    this.InitFrame(FrameNo + 1);
    pY = num7;
    pScrY = num8;
    return true;
  }

  internal bool CreatePageBorderFrames(
    int PageNo,
    ref int pY,
    ref int pScrY,
    ref int pFrameNo,
    out int pLeftFrame,
    out int pRightFrame)
  {
    int num1 = pY;
    int num2 = pScrY;
    int index1 = pFrameNo;
    int section = this.GetSection(this.e.PageInfo[PageNo].FirstLine);
    this.e.LeftBorderWidth = this.TwipsToUnitX(this.e.PageBorderWidth);
    int num3 = (int) ((double) this.e.UnitResX * (double) this.e.TerSect1[section].PgWidth);
    if (num3 + 2 * this.e.LeftBorderWidth < this.ScrToUnitX(this.e.TerWinWidth))
      this.e.LeftBorderWidth = (this.ScrToUnitX(this.e.TerWinWidth) - num3) / 2;
    this.e.frame[index1].empty = true;
    this.e.frame[index1].y = num1;
    this.e.frame[index1].ScrY = num2;
    this.e.frame[index1].x = 0;
    this.e.frame[index1].width = num3 + 2 * this.e.LeftBorderWidth;
    int unitY;
    this.e.TopBorderHeight = unitY = this.TwipsToUnitY(this.e.PageBorderWidth);
    this.e.frame[index1].height = unitY;
    this.e.BotBorderHeight = this.e.TopBorderHeight / 4;
    if (this.e.BotBorderHeight < 90)
      this.e.BotBorderHeight = 90;
    this.e.frame[index1].ScrHeight = this.UnitToScrY(this.e.TopBorderHeight);
    this.e.frame[index1].LeftBorderWdth = this.e.LeftBorderWidth;
    int num4 = num1 + this.e.frame[index1].height;
    int num5 = num2 + this.e.frame[index1].ScrHeight;
    this.e.frame[index1].flags = 129;
    ++this.e.TotalFrames;
    int FrameNo1 = index1 + 1;
    this.InitFrame(FrameNo1);
    int num6 = num4;
    int num7 = num5;
    int index2 = FrameNo1;
    this.e.frame[FrameNo1].empty = true;
    this.e.frame[FrameNo1].y = num6;
    this.e.frame[FrameNo1].ScrY = num7;
    this.e.frame[FrameNo1].x = 0;
    this.e.frame[FrameNo1].width = this.e.LeftBorderWidth;
    this.e.frame[FrameNo1].height = (int) ((double) this.e.UnitResY * (double) this.e.TerSect1[section].PgHeight);
    this.e.frame[FrameNo1].ScrHeight = this.UnitToScrY(this.e.frame[FrameNo1].height);
    this.e.frame[FrameNo1].flags = 32 /*0x20*/;
    ++this.e.TotalFrames;
    int FrameNo2 = FrameNo1 + 1;
    this.InitFrame(FrameNo2);
    int index3 = FrameNo2;
    this.e.frame[FrameNo2].empty = true;
    this.e.frame[FrameNo2].y = num6;
    this.e.frame[FrameNo2].ScrY = num7;
    this.e.frame[FrameNo2].x = this.e.LeftBorderWidth + num3;
    this.e.frame[FrameNo2].width = this.e.LeftBorderWidth;
    this.e.frame[FrameNo2].height = (int) ((double) this.e.UnitResY * (double) this.e.TerSect1[section].PgHeight);
    this.e.frame[FrameNo2].ScrHeight = this.UnitToScrY(this.e.frame[FrameNo2].height);
    this.e.frame[FrameNo2].flags = 65;
    ++this.e.TotalFrames;
    int FrameNo3 = FrameNo2 + 1;
    this.InitFrame(FrameNo3);
    this.e.frame[FrameNo3].empty = true;
    this.e.frame[FrameNo3].y = num6;
    this.e.frame[FrameNo3].ScrY = num7;
    this.e.frame[FrameNo3].x = this.e.frame[index2].x + this.e.frame[index2].width;
    this.e.frame[FrameNo3].width = this.e.frame[index3].x - this.e.frame[FrameNo3].x;
    if (this.e.ViewPageHdrFtr)
    {
      int num8 = this.PageHdrHeight2(PageNo, false, true) <= 0 ? (int) ((double) this.e.UnitResY * (double) this.e.TerSect[section].TopMargin) : (int) ((double) this.e.UnitResY * (double) this.e.TerSect[section].HdrMargin);
      this.e.frame[FrameNo3].height = num8;
    }
    else
      this.e.frame[FrameNo3].height = (int) ((double) this.e.UnitResY * (double) this.e.TerSect[section].TopMargin);
    this.e.frame[FrameNo3].ScrHeight = this.UnitToScrY(this.e.frame[FrameNo3].height);
    int num9 = num6 + this.e.frame[FrameNo3].height;
    int num10 = num7 + this.e.frame[FrameNo3].ScrHeight;
    ++this.e.TotalFrames;
    int FrameNo4 = FrameNo3 + 1;
    this.InitFrame(FrameNo4);
    pY = num9;
    pScrY = num10;
    pFrameNo = FrameNo4;
    pLeftFrame = index2;
    pRightFrame = index3;
    return true;
  }

  internal bool CreatePageBox(int PageNo, int PageTopX, int PageTopY, int TopSect, int LeftFrame)
  {
    int num1 = PageTopY;
    int num2 = PageTopX;
    int totalFrames = this.e.TotalFrames;
    int index = TopSect;
    if (!this.False(this.e.TerSect[index].border) && (this.e.InPrinting || this.e.BorderShowing))
    {
      this.e.PageInfo[PageNo].flags |= 8;
      int x = num1 + this.TwipsToUnitY(this.e.TerSect[index].BorderSpace[0]);
      int num3 = num2 + this.TwipsToUnitX(this.e.TerSect[index].BorderSpace[2]);
      int num4 = (int) ((double) this.e.UnitResY * (double) this.e.TerSect1[index].PgHeight) - (this.e.TerSect[index].BorderSpace[0] + this.e.TerSect[index].BorderSpace[1]);
      int num5 = (int) ((double) this.e.UnitResX * (double) this.e.TerSect1[index].PgWidth) - this.TwipsToUnitX(this.e.TerSect[index].BorderSpace[2] + this.e.TerSect[index].BorderSpace[3]);
      this.e.frame[totalFrames].empty = true;
      this.e.frame[totalFrames].y = x;
      this.e.frame[totalFrames].ScrY = this.UnitToScrY(x);
      this.e.frame[totalFrames].x = num3;
      this.e.frame[totalFrames].width = num5;
      this.e.frame[totalFrames].height = num4;
      this.e.frame[totalFrames].ScrHeight = this.UnitToScrY(this.e.frame[totalFrames].height);
      this.e.frame[totalFrames].sect = index;
      this.e.frame[totalFrames].flags = 65537 /*0x010001*/;
      ++this.e.TotalFrames;
      this.InitFrame(totalFrames + 1);
    }
    return true;
  }

  internal bool CreatePictFrames(
    int PageNo,
    int HiddenY,
    int HdrMargin,
    int TopLeftMargin,
    int HdrHeight,
    int TopSect,
    int FirstLine,
    int LastLine,
    int InHdrFtr)
  {
    if (FirstLine >= this.e.TotalLines)
      FirstLine = this.e.TotalLines - 1;
    if (LastLine >= this.e.TotalLines)
      LastLine = this.e.TotalLines - 1;
    for (int index1 = FirstLine; index1 <= LastLine; ++index1)
    {
      if ((this.e.text[index1].flags & 16384 /*0x4000*/) != 0 && (this.e.text[index1].page == PageNo || InHdrFtr != 0) && (this.e.PfmtId[this.e.text[index1].pfmt].flags & 12288 /*0x3000*/) == InHdrFtr)
      {
        int fid = this.e.text[index1].fid;
        if (!this.True(fid) || (this.e.ParaFrame[fid].flags & 32768 /*0x8000*/) != 0)
        {
          ushort[] numArray = this.OpenCfmt(index1);
          for (int index2 = 0; index2 < this.e.text[index1].len; ++index2)
          {
            int index3 = (int) numArray[index2];
            if (!this.False(this.e.TerFont[index3].InUse) && (this.e.TerFont[index3].style & 128 /*0x80*/) != 0 && this.e.TerFont[index3].FrameType != 0 && this.e.TerFont[index3].ParaFID != 0)
            {
              int paraFid = this.e.TerFont[index3].ParaFID;
              int totalFrames = this.e.TotalFrames;
              this.e.frame[totalFrames].empty = false;
              this.e.frame[totalFrames].sect = this.GetSection(index1);
              this.e.frame[totalFrames].PageFirstLine = index1;
              this.e.frame[totalFrames].PageLastLine = index1;
              this.e.frame[totalFrames].ParaFrameId = paraFid;
              this.e.frame[totalFrames].flags |= 2048 /*0x0800*/;
              if (this.e.TerFont[index3].PictType == 11)
                this.e.frame[totalFrames].flags |= 8;
              this.e.frame[totalFrames].ZOrder = this.e.ParaFrame[paraFid].ZOrder;
              this.e.frame[totalFrames].width = this.TwipsToUnitX(this.e.ParaFrame[paraFid].width);
              this.e.frame[totalFrames].SpaceLeft = this.TwipsToUnitX(this.e.ParaFrame[paraFid].margin);
              this.e.frame[totalFrames].SpaceRight = this.TwipsToUnitX(this.e.ParaFrame[paraFid].margin);
              this.e.frame[totalFrames].SpaceTop = this.TwipsToUnitY(this.e.ParaFrame[paraFid].margin);
              this.e.frame[totalFrames].SpaceBot = this.TwipsToUnitY(this.e.ParaFrame[paraFid].margin);
              this.e.frame[totalFrames].height = this.TwipsToUnitY(this.e.ParaFrame[paraFid].height);
              this.e.frame[totalFrames].ScrHeight = this.TwipsToScrY(this.e.ParaFrame[paraFid].height);
              this.e.frame[totalFrames].TextHeight = this.e.frame[totalFrames].ScrHeight;
              this.e.frame[totalFrames].x = this.TwipsToUnitX(this.e.ParaFrame[paraFid].x);
              if (this.e.BorderShowing)
                this.e.frame[totalFrames].x += this.e.LeftBorderWidth + TopLeftMargin;
              this.SetParaFrameY(paraFid, totalFrames, this.e.text[index1].y, HiddenY, HdrMargin, HdrHeight, TopSect, index1);
              this.e.ContainsParaFrames = true;
              ++this.e.TotalFrames;
              this.InitFrame(this.e.TotalFrames);
            }
          }
          this.CloseCfmt(index1);
        }
      }
    }
    return true;
  }

  internal int CreateSubTableFrames(
    int FirstLine,
    int ParentLevel,
    ref int pFrameNo,
    ref int pTableHt,
    int ColumnX,
    int PageNo,
    int sect,
    int y,
    int ScrY,
    int HiddenY,
    int HdrMargin,
    int HdrHeight,
    int TopSect,
    int TopLeftMargin,
    int ColumnNo,
    ref bool pHasPictFrames,
    int pass,
    int BoxFrame,
    int ParaFrameId)
  {
    int FrameNo = pFrameNo;
    int num1 = ParentLevel + 1;
    int CellWidth = 0;
    bool flag1 = false;
    int num2 = 0;
    int PassFlags = 0;
    Color[] pColor = new Color[4];
    if (tc.DebugMode)
      this.misc.dm(nameof (CreateSubTableFrames));
    pTableHt = 0;
    if (this.e.text[FirstLine].cid == 0)
      return FirstLine;
    int index1 = 0;
    for (int LineNo = FirstLine; LineNo < this.e.TotalLines; ++LineNo)
    {
      if (this.TableLevel(LineNo) == ParentLevel)
        return LineNo - 1;
      if (this.TableLevel(LineNo) == ParentLevel + 1)
      {
        index1 = this.e.text[LineNo].cid;
        break;
      }
    }
    if (index1 == 0)
      return this.e.TotalLines - 1;
    int parentCell = this.e.cell[index1].ParentCell;
    int row1 = this.e.cell[parentCell].row;
    int unitX = this.TwipsToUnitX(this.e.cell[parentCell].margin);
    int num3 = this.TwipsToUnitX(this.e.cell[parentCell].width) - 2 * unitX;
    ColumnX += unitX;
    if (this.e.cell[parentCell].PrevCell <= 0 && this.e.cell[parentCell].level > 0)
    {
      int row2 = this.e.cell[parentCell].row;
      ColumnX += this.e.TableRow[row2].CurIndent;
    }
    int pTopWidth;
    this.GetCellFrameBorder(parentCell, out int _, out int _, out pTopWidth, out int _, PageNo, pColor);
    if ((this.e.CellAux[parentCell].flags & 2) != 0)
      pTopWidth = this.e.cell[parentCell].margin;
    int unitY = this.TwipsToUnitY(pTopWidth);
    int scrY = this.UnitToScrY(unitY);
    this.InitFrame(FrameNo);
    int num4 = 0;
    int num5 = 0;
    int CellX = ColumnX;
    int FirstCellFrame = -1;
    int TableRowHeight = 0;
    int ScrTableRowHeight = 0;
    int index2 = 0;
    int FrameFirstLine = -1;
    int pTableHt1 = 0;
    int pTableRowIndent = 0;
    int CellFramed = 0;
    int index3 = FirstLine;
    while (true)
    {
      if (this.TableLevel(index3) < num1)
        flag1 = true;
      if (((index3 >= this.e.TotalLines || pass != 3 ? 1 : (this.e.text[index3].page == PageNo ? 1 : (!this.True(this.e.text[index3].cid) ? 1 : 0))) | (flag1 ? 1 : 0)) != 0)
      {
        if (FrameFirstLine < 0)
          FrameFirstLine = num2 = index3;
        int FrameLastLine = num2;
        num2 = index3;
        int CurLineHt = 0;
        if (index3 < this.e.TotalLines)
          CurLineHt = this.e.text[index3].height;
        bool flag2;
        bool RowBreak = flag2 = false;
        int PrevCell = index2;
        index2 = index3 >= this.e.TotalLines || flag1 ? 0 : this.LevelCell(num1, index3);
        if (!flag1 || PrevCell != 0 || index2 != 0)
        {
          int row3 = this.e.cell[index2].row;
          int row4 = this.e.cell[PrevCell].row;
          if (index2 != PrevCell && PrevCell != 0)
            flag2 = true;
          int num6 = row4;
          if (row3 != num6 && row4 != 0 && PrevCell != 0)
            RowBreak = true;
          if (flag1)
            flag2 = RowBreak = true;
          if (flag2 | RowBreak)
          {
            this.CreateCellFrame(num1, ref FrameNo, y, ScrY, ref num4, ref num5, unitY, scrY, ref FirstCellFrame, ref TableRowHeight, ref ScrTableRowHeight, ref CellX, pTableRowIndent, PageNo, TopLeftMargin, ColumnNo, sect, PrevCell, ref CellFramed, FrameFirstLine, FrameLastLine, PassFlags, RowBreak, ref CellWidth, num3, 1, 0, num3, ColumnX, num3, BoxFrame, ParaFrameId);
            FrameFirstLine = num2;
            if (!RowBreak)
              num4 = num5 = 0;
            unitY += num4;
            scrY += num5;
            num4 = num5 = 0;
            CellX += CellWidth;
            if (RowBreak)
              CellX = ColumnX;
          }
          if (!flag1)
          {
            if (index3 < this.e.TotalLines && this.TableLevel(index3) > num1)
            {
              int ColumnX1 = CellX;
              int index4 = 0;
              if (FirstLine < this.e.TotalLines)
                index4 = this.LevelCell(num1, FirstLine);
              if (index4 > 0 && this.e.cell[index2].PrevCell > 0)
              {
                for (int index5 = this.e.TableRow[this.e.cell[index4].row].FirstCell; index5 > 0 && index5 != index4; index5 = this.e.cell[index5].NextCell)
                  ColumnX1 += this.TwipsToUnitX(this.e.cell[index5].width);
              }
              index3 = this.CreateSubTableFrames(index3, num1, ref FrameNo, ref pTableHt1, ColumnX1, PageNo, sect, y + unitY + num4, ScrY + scrY + num5, HiddenY, HdrMargin, HdrHeight, TopSect, TopLeftMargin, ColumnNo, ref pHasPictFrames, pass, BoxFrame, ParaFrameId);
              num4 += pTableHt1;
              num5 += this.UnitToScrY(pTableHt1);
            }
            else if (index3 < this.e.TotalLines)
              this.SetFrameLineInfo(index3, y, ScrY, ref num4, ref num5, unitY, CellX, ref pTableRowIndent, PageNo, HiddenY, HdrMargin, HdrHeight, TopSect, TopLeftMargin, ColumnNo, ref pHasPictFrames, sect, CurLineHt);
          }
          else
            break;
        }
        else
          break;
      }
      ++index3;
    }
    pFrameNo = FrameNo;
    pTableHt = unitY;
    return index3 - 1;
  }

  internal bool CreateWatermarkFrame(
    int PageNo,
    int y,
    ref int FrameNo,
    int TopSect,
    int TopLeftMargin,
    int HiddenY,
    int HdrMargin,
    int HdrHeight)
  {
    if (this.e.WmParaFID > 0)
    {
      int wmParaFid = this.e.WmParaFID;
      if (!this.e.repaginating)
        this.PosWatermarkFrame(PageNo);
      this.e.frame[FrameNo].empty = true;
      this.SetParaFrameY(wmParaFid, FrameNo, y, HiddenY, HdrMargin, HdrHeight, TopSect, -1);
      this.e.frame[FrameNo].x = this.TwipsToUnitX(this.e.ParaFrame[wmParaFid].x);
      if (this.e.BorderShowing)
        this.e.frame[FrameNo].x += this.e.LeftBorderWidth + TopLeftMargin;
      this.e.frame[FrameNo].width = this.TwipsToUnitY(this.e.ParaFrame[wmParaFid].width);
      this.e.frame[FrameNo].height = this.TwipsToUnitY(this.e.ParaFrame[wmParaFid].height);
      this.e.frame[FrameNo].ScrHeight = this.UnitToScrY(this.e.frame[FrameNo].height);
      this.e.frame[FrameNo].BoxFrame = FrameNo;
      this.e.frame[FrameNo].flags = 2097152 /*0x200000*/;
      this.e.frame[FrameNo].ParaFrameId = wmParaFid;
      ++this.e.TotalFrames;
      ++FrameNo;
      this.InitFrame(FrameNo);
    }
    return true;
  }

  internal new bool DeleteFrame()
  {
    if (this.e.FrameClicked)
    {
      if ((this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].flags & 12288 /*0x3000*/) != 0 && !this.e.EditPageHdrFtr)
      {
        this.MessageBeep(0);
        return true;
      }
      int fid = this.e.text[this.e.CurLine].fid;
      if (this.False(fid) || DialogResult.No == this.ShowMessage((this.e.ParaFrame[fid].flags & 896) == 0 ? this.e.MsgString[153] : this.e.MsgString[152], this.e.MsgString[154], MessageBoxButtons.YesNo))
        return true;
      int index1 = this.e.CurLine - 1;
      while (index1 >= 0 && this.e.text[index1].fid == fid)
        --index1;
      this.e.HilightBegRow = index1 + 1;
      this.e.HilightBegCol = 0;
      int index2 = this.e.CurLine + 1;
      while (index2 < this.e.TotalLines && this.e.text[index2].fid == fid)
        ++index2;
      this.e.HilightEndRow = index2 - 1;
      this.e.HilightEndCol = this.e.text[this.e.HilightEndRow].len;
      this.e.HilightType = 1;
      this.DeleteLineBlock(false);
      this.e.HilightType = 0;
      this.e.ParaFrame[fid].InUse = false;
      this.Repaginate(false, true, 0, true);
    }
    return true;
  }

  internal new bool FitPictureInFrame(int line, bool ResizePicture)
  {
    int num = 0;
    int x1 = 0;
    int x2 = 0;
    if (!this.e.TerArg.PageMode)
      return false;
    int fid = this.e.text[line].fid;
    int frame;
    if (fid == 0 || (frame = this.GetFrame(line)) == -1 || this.e.frame[frame].ParaFrameId != fid)
      return false;
    for (int pageFirstLine = this.e.frame[frame].PageFirstLine; pageFirstLine <= this.e.frame[frame].PageLastLine; ++pageFirstLine)
    {
      num += this.e.text[pageFirstLine].len;
      if (num > 2)
        return false;
    }
    int pageFirstLine1 = this.e.frame[frame].PageFirstLine;
    if (this.e.text[pageFirstLine1].len == 0)
      return false;
    int index = (int) this.OpenCfmt(pageFirstLine1)[0];
    this.CloseCfmt(pageFirstLine1);
    if ((this.e.TerFont[index].style & 128 /*0x80*/) == 0)
      return false;
    if (ResizePicture)
    {
      int z = 0;
      int x3 = this.e.ParaFrame[fid].width - 2 * this.e.ParaFrame[fid].margin - this.ScrToTwipsX(x1);
      if (this.e.ParaFrame[fid].MinHeight > 0)
        z = this.e.ParaFrame[fid].MinHeight - 2 * this.e.ParaFrame[fid].margin - this.ScrToTwipsY(x2);
      if ((this.e.TerFlags & 131072 /*0x020000*/) != 0 && x3 > 0 && z > 0)
      {
        int pictWidth = this.e.TerFont[index].PictWidth;
        int pictHeight = this.e.TerFont[index].PictHeight;
        if (pictWidth >= this.MulDiv(x3, pictHeight, z))
        {
          this.e.TerFont[index].PictWidth = x3;
          this.e.TerFont[index].PictHeight = this.MulDiv(pictHeight, this.e.TerFont[index].PictWidth, pictWidth);
        }
        else
        {
          this.e.TerFont[index].PictHeight = z;
          this.e.TerFont[index].PictWidth = this.MulDiv(pictWidth, this.e.TerFont[index].PictHeight, pictHeight);
        }
      }
      else
      {
        if (x3 > 0)
          this.e.TerFont[index].PictWidth = x3;
        if (z > 0)
          this.e.TerFont[index].PictHeight = z;
      }
      this.SetPictSize(index, this.TwipsToScrY(this.e.TerFont[index].PictHeight), this.TwipsToScrX(this.e.TerFont[index].PictWidth), true);
      this.XlateSizeForPrt(index);
    }
    else
    {
      this.e.ParaFrame[fid].width = this.e.TerFont[index].PictWidth + 2 * this.e.ParaFrame[fid].margin;
      this.e.ParaFrame[fid].MinHeight = 0;
      if (this.True(this.e.BkPictId))
        this.DeleteTextMap(true);
      if (line < this.e.RepageBeginLine)
        this.e.RepageBeginLine = line;
    }
    return true;
  }

  internal bool FrameEmptyCells(
    int row,
    ref int pCellFramed,
    int NextCell,
    ref int pX,
    int y,
    int height,
    ref int pFrameNo,
    int sect,
    int PageNo)
  {
    Color[] pColor = new Color[4];
    if ((this.e.TableRow[row].flags & 16 /*0x10*/) != 0 && PageNo >= this.e.TableAux[row].FirstPage && PageNo <= this.e.TableAux[row].LastPage)
    {
      int x = pCellFramed;
      int FrameNo = pFrameNo;
      int num = pX;
      int CurCell = !this.True(x) ? this.e.TableRow[row].FirstCell : this.e.cell[x].NextCell;
      while (CurCell != NextCell && CurCell > 0)
      {
        this.e.frame[FrameNo].empty = true;
        this.e.frame[FrameNo].y = y;
        this.e.frame[FrameNo].ScrY = this.UnitToScrY(y);
        this.e.frame[FrameNo].x = num;
        this.e.frame[FrameNo].width = this.TwipsToUnitX(this.e.cell[CurCell].width);
        this.e.frame[FrameNo].height = height;
        this.e.frame[FrameNo].ScrHeight = this.UnitToScrY(y + height) - this.UnitToScrY(y);
        this.e.frame[FrameNo].sect = sect;
        this.e.frame[FrameNo].RowId = row;
        this.e.frame[FrameNo].CellId = CurCell;
        this.e.frame[FrameNo].shading = this.e.cell[CurCell].shading;
        this.e.frame[FrameNo].BackColor = this.e.cell[CurCell].BackColor;
        this.e.frame[FrameNo].level = this.e.cell[CurCell].level;
        if ((this.e.cell[CurCell].flags & 16384 /*0x4000*/) != 0)
          this.e.frame[FrameNo].flags |= 1024 /*0x0400*/;
        this.e.frame[FrameNo].flags1 |= 1;
        int pLeftWidth;
        int pRightWidth;
        int pTopWidth;
        int pBotWidth;
        this.e.frame[FrameNo].border = this.GetCellFrameBorder(CurCell, out pLeftWidth, out pRightWidth, out pTopWidth, out pBotWidth, PageNo, pColor);
        this.e.frame[FrameNo].BorderWidth[2] = pLeftWidth;
        this.e.frame[FrameNo].BorderWidth[3] = pRightWidth;
        this.e.frame[FrameNo].BorderWidth[0] = pTopWidth;
        this.e.frame[FrameNo].BorderWidth[1] = pBotWidth;
        for (int index = 0; index < 4; ++index)
          this.e.frame[FrameNo].BorderColor[index] = pColor[index];
        x = CurCell;
        num += this.e.frame[FrameNo].width;
        CurCell = this.e.cell[CurCell].NextCell;
        ++FrameNo;
        this.InitFrame(FrameNo);
      }
      pFrameNo = FrameNo;
      pCellFramed = x;
      pX = num;
    }
    return true;
  }

  internal new int FrameToMargX(int x)
  {
    if (this.e.BorderShowing)
    {
      x -= this.UnitToTwipsY(this.e.LeftBorderWidth);
      int pageSect = this.e.TerGetPageSect(this.e.CurPage);
      x -= (int) ((double) this.e.TerSect[pageSect].LeftMargin * 1440.0);
    }
    return x;
  }

  internal new int FrameToPageY(int y)
  {
    if (y > this.ScrToTwipsY(this.e.FirstPageHeight))
      y -= this.ScrToTwipsY(this.e.FirstPageHeight);
    if (this.e.BorderShowing)
    {
      y -= this.UnitToTwipsY(this.e.TopBorderHeight);
      return y;
    }
    if (!this.e.ViewPageHdrFtr)
    {
      int pageSect = this.e.TerGetPageSect(this.e.CurPage);
      y += (int) ((double) this.e.TerSect[pageSect].TopMargin * 1440.0);
    }
    return y;
  }

  internal new int GetAnchorY(int AnchorLine)
  {
    if (AnchorLine < 0)
    {
      int num = -AnchorLine;
      int index = 0;
      while (index < this.e.TotalLines && this.e.text[index].fid != num)
        ++index;
      if (index == this.e.TotalLines)
        return 0;
      AnchorLine = index;
    }
    while (AnchorLine < this.e.TotalLines && this.e.text[AnchorLine].fid != 0)
      ++AnchorLine;
    if (AnchorLine == this.e.TotalLines)
      AnchorLine = this.e.TotalLines - 1;
    int units = this.LineToUnits(AnchorLine);
    if (this.LineInfo(AnchorLine, 8192 /*0x2000*/))
      units -= this.UnitToScrY(this.e.text[AnchorLine].tabw.height);
    if (this.True(this.e.text[AnchorLine].cid))
    {
      int row = this.e.cell[this.e.text[AnchorLine].cid].row;
      units -= this.UnitToScrY(this.e.TableRow[row].FrmSpcBef);
    }
    return units;
  }

  internal new int GetBorderCell(int CellId, int PageNo, bool next)
  {
    int CellId1 = this.GetSameColumnCell(CellId, next);
    if (CellId1 <= 0)
    {
      if (next && this.e.cell[CellId].RowSpan > 1)
      {
        int index1 = this.e.cell[CellId].row;
        for (int index2 = 1; index2 < this.e.cell[CellId].RowSpan; ++index2)
        {
          index1 = this.e.TableRow[index1].NextRow;
          if (index1 <= 0)
            return 0;
        }
        CellId = this.e.TableRow[index1].FirstCell;
      }
      CellId1 = this.UniformRowBorderCell(CellId, next);
      if (CellId1 > 0 && this.UniformRowBorderCell(CellId1, !next) <= 0)
        CellId1 = 0;
      if (CellId1 <= 0)
      {
        int x1 = this.e.cell[CellId].x;
        int num1 = x1 + this.e.cell[CellId].width;
        int row = this.e.cell[CellId].row;
        int index3 = next ? this.e.TableRow[row].NextRow : this.e.TableRow[row].PrevRow;
        if (index3 > 0)
        {
          int num2 = -1;
          int num3 = 0;
          int num4 = 0;
          int num5 = 60;
          for (int index4 = this.e.TableRow[index3].FirstCell; index4 > 0; index4 = this.e.cell[index4].NextCell)
          {
            int x2 = this.e.cell[index4].x;
            int num6 = x2 + this.e.cell[index4].width;
            if (num6 > x1 + num5)
            {
              if (x2 < num1 - num5 && (x2 >= x1 - num5 || num6 <= x1 + num5 || num6 >= num1 - num5) && (x2 <= x1 + num5 || x2 >= num1 - num5 || num6 <= num1 + num5))
              {
                int num7 = 0;
                if (next && (this.e.cell[index4].border & 1) != 0)
                  num7 = this.e.cell[index4].BorderWidth[0];
                if (!next && (this.e.cell[index4].border & 2) != 0)
                  num7 = this.e.cell[index4].BorderWidth[1];
                if (num2 != -1)
                {
                  if (num7 != num2)
                    break;
                }
                if (num3 <= 0)
                  num3 = index4;
                num4 = this.e.cell[index4].x + this.e.cell[index4].width;
              }
              else
                break;
            }
          }
          if (num4 >= num1 - num5 && num2 > 0)
            CellId1 = num3;
        }
      }
    }
    if (CellId1 > 0)
    {
      int row = this.e.cell[CellId].row;
      int prevRow = this.e.TableRow[row].PrevRow;
      int nextRow = this.e.TableRow[row].NextRow;
      if (next)
      {
        if (this.IsPageLastRow(row, PageNo))
          CellId1 = 0;
        if (nextRow > 0 && (this.e.TableRow[nextRow].flags & 4) == 0 && (this.e.TableRow[row].flags & 4) != 0)
          CellId1 = 0;
      }
      if (!next)
      {
        if (this.IsPartRow(true, this.e.cell[CellId].row, PageNo))
          CellId1 = 0;
        if (this.e.cell[CellId].row == this.e.PageInfo[PageNo].FirstRow)
          CellId1 = 0;
        if (prevRow > 0 && (this.e.TableRow[prevRow].flags & 4) != 0 && (this.e.TableRow[row].flags & 4) == 0)
          CellId1 = 0;
      }
    }
    return CellId1;
  }

  internal new int GetBorderLeftSpace(int PageNo)
  {
    if (!this.e.BorderShowing)
      return 0;
    if (PageNo < 0)
      PageNo = this.GetCurPage(-PageNo);
    return this.e.LeftBorderWidth + (int) ((double) this.e.UnitResX * (double) this.e.TerSect[this.e.TerGetPageSect(PageNo)].LeftMargin);
  }

  internal int GetCellFrameBorder(
    int CurCell,
    out int pLeftWidth,
    out int pRightWidth,
    out int pTopWidth,
    out int pBotWidth,
    int PageNo,
    Color[] pColor)
  {
    int borders = this.e.cell[CurCell].border;
    Color[] colorArray = new Color[4];
    int num1;
    pBotWidth = num1 = 0;
    int num2;
    pTopWidth = num2 = num1;
    int num3;
    pRightWidth = num3 = num2;
    pLeftWidth = num3;
    int cellFrameLeftWidth = this.GetCellFrameLeftWidth(CurCell, ref borders, out colorArray[2]);
    int cellFrameRightWidth = this.GetCellFrameRightWidth(CurCell, ref borders, out colorArray[3]);
    int cellFrameTopWidth = this.GetCellFrameTopWidth(CurCell, ref borders, PageNo, out colorArray[0]);
    int cellFrameBotWidth = this.GetCellFrameBotWidth(CurCell, ref borders, PageNo, out colorArray[1]);
    if (pColor != null)
    {
      for (int index = 0; index < 4; ++index)
        pColor[index] = colorArray[index];
    }
    if (this.e.ShowTableGridLines)
    {
      ref tc.StrCell local = ref this.e.cell[CurCell];
      int num4 = borders | 2 | 8;
      if (this.GetPrevCellInColumn(CurCell, false, false) == -1 && this.UniformRowBorderCell(CurCell, false) == 0)
        num4 |= 1;
      borders = num4 | 1;
      if (this.e.cell[CurCell].PrevCell == -1)
        borders |= 4;
    }
    pLeftWidth = cellFrameLeftWidth;
    pRightWidth = cellFrameRightWidth;
    pTopWidth = cellFrameTopWidth;
    pBotWidth = cellFrameBotWidth;
    return borders;
  }

  internal new int GetCellFrameBotWidth(int CellId, ref int borders, int PageNo, out Color pColor)
  {
    int cellFrameBotWidth = 0;
    Color color = this.e.cell[CellId].BorderColor[1];
    if (this.e.HtmlMode && (this.e.TerFlags3 & 8) == 0 && (this.e.cell[CellId].flags & 131072 /*0x020000*/) != 0)
    {
      cellFrameBotWidth = this.e.cell[CellId].BorderWidth[1];
      if (cellFrameBotWidth > 0)
      {
        cellFrameBotWidth += cellFrameBotWidth;
        int index = this.e.cell[CellId].row;
        for (int rowSpan = this.e.cell[CellId].RowSpan; rowSpan > 1; --rowSpan)
        {
          if (index > 0)
            index = this.e.TableRow[index].NextRow;
        }
        if (index > 0)
          index = this.e.TableRow[index].NextRow;
        if (index <= 0)
          cellFrameBotWidth += cellFrameBotWidth;
      }
      if (this.e.cell[CellId].margin != this.e.DefCellMargin)
        cellFrameBotWidth += this.e.cell[CellId].margin;
    }
    else
    {
      if ((borders & 2) != 0)
        cellFrameBotWidth = this.e.cell[CellId].BorderWidth[1];
      int borderCell = this.GetBorderCell(CellId, PageNo, true);
      if (borderCell > 0)
      {
        cellFrameBotWidth /= 2;
        int num = (this.e.cell[borderCell].border & 1) == 0 ? 0 : this.e.cell[borderCell].BorderWidth[0] / 2;
        if ((this.e.TerFlags4 & 67108864 /*0x04000000*/) == 0)
        {
          if (cellFrameBotWidth == 0 && num > 0)
            color = this.e.cell[borderCell].BorderColor[0];
          if (num > cellFrameBotWidth)
            cellFrameBotWidth = num;
        }
        else if (num > 0)
          cellFrameBotWidth = 0;
        else
          cellFrameBotWidth *= 2;
        if (cellFrameBotWidth > 0)
          borders |= 2;
      }
      if (cellFrameBotWidth == 0)
        tc.ResetUintFlag(ref borders, 2);
    }
    pColor = color;
    return cellFrameBotWidth;
  }

  internal new int GetCellFrameLeftWidth(int CellId, ref int borders, out Color pColor)
  {
    int cellFrameLeftWidth = 0;
    Color color = this.e.cell[CellId].BorderColor[2];
    pColor = color;
    if (this.e.HtmlMode && (this.e.TerFlags3 & 8) == 0 && (this.e.cell[CellId].flags & 131072 /*0x020000*/) != 0)
    {
      cellFrameLeftWidth = this.e.cell[CellId].BorderWidth[0];
      if (cellFrameLeftWidth > 0 && this.e.cell[CellId].PrevCell <= 0)
        cellFrameLeftWidth *= 3;
    }
    else
    {
      if ((borders & 4) != 0)
        cellFrameLeftWidth = this.e.cell[CellId].BorderWidth[2];
      int prevCell = this.e.cell[CellId].PrevCell;
      if (prevCell > 0)
      {
        cellFrameLeftWidth = (cellFrameLeftWidth + 1) / 2;
        int num = (this.e.cell[prevCell].border & 8) == 0 ? 0 : this.e.cell[prevCell].BorderWidth[3] / 2;
        if ((this.e.TerFlags4 & 67108864 /*0x04000000*/) == 0)
        {
          if (cellFrameLeftWidth == 0 && num > 0)
            color = this.e.cell[prevCell].BorderColor[3];
          if (num > cellFrameLeftWidth)
            cellFrameLeftWidth = num;
        }
        else
          cellFrameLeftWidth *= 2;
        if (cellFrameLeftWidth > 0)
          borders |= 4;
      }
    }
    if (cellFrameLeftWidth > this.e.cell[CellId].margin)
      cellFrameLeftWidth = this.e.cell[CellId].margin;
    pColor = color;
    return cellFrameLeftWidth;
  }

  internal new int GetCellFrameRightWidth(int CellId, ref int borders, out Color pColor)
  {
    int cellFrameRightWidth = 0;
    Color color = this.e.cell[CellId].BorderColor[3];
    if (this.e.HtmlMode && (this.e.TerFlags3 & 8) == 0 && (this.e.cell[CellId].flags & 131072 /*0x020000*/) != 0)
    {
      cellFrameRightWidth = this.e.cell[CellId].BorderWidth[0];
      if (cellFrameRightWidth > 0 && this.e.cell[CellId].NextCell <= 0)
        cellFrameRightWidth *= 3;
    }
    else
    {
      if ((borders & 8) != 0)
        cellFrameRightWidth = this.e.cell[CellId].BorderWidth[3];
      int nextCell = this.e.cell[CellId].NextCell;
      if (nextCell > 0)
      {
        cellFrameRightWidth /= 2;
        int num = (this.e.cell[nextCell].border & 4) == 0 ? 0 : this.e.cell[nextCell].BorderWidth[2] / 2;
        if ((this.e.TerFlags4 & 67108864 /*0x04000000*/) == 0)
        {
          if (cellFrameRightWidth == 0 && num > 0)
            color = this.e.cell[nextCell].BorderColor[2];
          if (num > cellFrameRightWidth)
            cellFrameRightWidth = num;
        }
        else if (num > 0)
          cellFrameRightWidth = 0;
        else
          cellFrameRightWidth *= 2;
        if (cellFrameRightWidth > 0)
          borders |= 8;
      }
      if (cellFrameRightWidth == 0)
        tc.ResetUintFlag(ref borders, 8);
    }
    if (cellFrameRightWidth > this.e.cell[CellId].margin)
      cellFrameRightWidth = this.e.cell[CellId].margin;
    pColor = color;
    return cellFrameRightWidth;
  }

  internal new int GetCellFrameTopWidth(int CellId, ref int borders, int PageNo, out Color pColor)
  {
    int cellFrameTopWidth = 0;
    Color color = this.e.cell[CellId].BorderColor[0];
    if (this.e.HtmlMode && (this.e.TerFlags3 & 8) == 0 && (this.e.cell[CellId].flags & 131072 /*0x020000*/) != 0)
    {
      cellFrameTopWidth = this.e.cell[CellId].BorderWidth[0];
      if (cellFrameTopWidth > 0 && this.e.TableRow[this.e.cell[CellId].row].PrevRow <= 0)
        cellFrameTopWidth *= 3;
      if (this.e.cell[CellId].margin != this.e.DefCellMargin)
        cellFrameTopWidth += this.e.cell[CellId].margin;
    }
    else
    {
      if ((borders & 1) != 0)
        cellFrameTopWidth = this.e.cell[CellId].BorderWidth[0];
      int borderCell = this.GetBorderCell(CellId, PageNo, false);
      if (borderCell > 0)
      {
        cellFrameTopWidth = (cellFrameTopWidth + 1) / 2;
        int num = (this.e.cell[borderCell].border & 2) == 0 ? 0 : this.e.cell[borderCell].BorderWidth[1] / 2;
        if ((this.e.TerFlags4 & 67108864 /*0x04000000*/) == 0)
        {
          if (cellFrameTopWidth == 0 && num > 0)
            color = this.e.cell[borderCell].BorderColor[1];
          if (num > cellFrameTopWidth)
            cellFrameTopWidth = num;
        }
        else
          cellFrameTopWidth *= 2;
        if (cellFrameTopWidth > 0)
          borders |= 1;
      }
    }
    pColor = color;
    return cellFrameTopWidth;
  }

  internal new int GetFrame(int lin)
  {
    int frame1 = -1;
    bool flag1 = false;
    if (!this.e.TerArg.PageMode)
      return 0;
    bool flag2 = lin >= 0 && lin < this.e.TotalLines;
    if (flag2 && (this.e.PfmtId[this.e.text[lin].pfmt].flags & 12288 /*0x3000*/) != 0)
      flag1 = true;
    if (!flag1 & flag2)
    {
      int frame2 = this.e.text[lin].frame;
      if (frame2 == -1)
        return frame1;
      if (!this.e.frame[frame2].empty && frame2 >= 0 && frame2 < this.e.TotalFrames && (this.e.frame[frame2].flags & 4096 /*0x1000*/) == 0 && (this.e.frame[frame2].flags & 2048 /*0x0800*/) == 0 && lin >= this.e.frame[frame2].PageFirstLine && lin <= this.e.frame[frame2].PageLastLine)
        return frame2;
    }
    for (int frame3 = 0; frame3 < this.e.TotalFrames; ++frame3)
    {
      if (!this.e.frame[frame3].empty && (this.e.frame[frame3].flags & 2048 /*0x0800*/) == 0 && lin >= this.e.frame[frame3].PageFirstLine && lin <= this.e.frame[frame3].PageLastLine)
      {
        if (!flag1)
          return frame3;
        frame1 = frame3;
        if (this.e.CurPage == this.e.FirstFramePage && this.e.frame[frame3].y < this.e.FirstPageHeight)
          return frame3;
      }
    }
    return frame1;
  }

  internal new bool GetFrameSpace(
    int line,
    COp.RECT rect,
    out int FrameX,
    out int FrameWidth,
    out int FrameHt)
  {
    if (this.CalcFrameSpace(line, rect, out FrameX, out FrameWidth, out FrameHt, -1, false, false, false, -1) && FrameHt > 0)
    {
      FrameWidth = 0;
      FrameX = 0;
      FrameHt = 0;
    }
    return true;
  }

  internal new int GetFrmSpcBef(int line, bool InScrUnits)
  {
    int x = 0;
    if (!this.e.TerArg.PageMode || line < 0 || line >= this.e.TotalLines || this.e.text[line].fid != 0)
      return 0;
    if (this.e.text[line].tabw != null && (this.e.text[line].tabw.type & 8192 /*0x2000*/) != 0)
      x = this.e.text[line].tabw.height;
    if (InScrUnits && x != 0)
      x = this.MulDiv(x, this.e.ScrResY, this.e.UnitResY);
    return x;
  }

  internal new bool GetLinePoints(int FrameNo, out int x1, out int y1, out int x2, out int y2)
  {
    int paraFrameId = this.e.frame[FrameNo].ParaFrameId;
    int num1;
    y2 = num1 = 0;
    int num2;
    y1 = num2 = num1;
    int num3;
    x2 = num3 = num2;
    x1 = num3;
    if (!this.e.frame[FrameNo].empty && (this.e.ParaFrame[paraFrameId].flags & 256 /*0x0100*/) != 0)
    {
      x1 = this.e.frame[FrameNo].x;
      x2 = this.e.frame[FrameNo].x + this.e.frame[FrameNo].width;
      y1 = this.e.frame[FrameNo].y;
      y2 = this.e.frame[FrameNo].y + this.e.frame[FrameNo].height;
      if (this.e.ParaFrame[paraFrameId].LineType == 2)
        this.SwapInts(ref y1, ref y2);
      if (this.e.ParaFrame[paraFrameId].LineType == 0)
        y2 = y1;
      if (this.e.ParaFrame[paraFrameId].LineType == 1)
        x2 = x1;
    }
    return true;
  }

  internal int GetObjectColAdj(int ParaFID, int line, int PageNo)
  {
    int FrameX = 0;
    int FrameWidth = 0;
    if ((this.e.ParaFrame[ParaFID].flags & 128 /*0x80*/) != 0)
    {
      while (line < this.e.TotalLines && this.e.text[line].fid != 0)
        ++line;
      if (line > this.e.TotalLines)
        return 0;
    }
    int FrameHt;
    if (this.CalcFrameSpace(line, tc.SkipRect, out FrameX, out FrameWidth, out FrameHt, -1, false, false, false, PageNo))
    {
      if (FrameHt > 0)
        FrameX = FrameWidth = 0;
      if (FrameX == 0)
        return FrameWidth;
    }
    return 0;
  }

  internal new int GetObjSpcBef(int line, bool InScrUnits)
  {
    return this.GetFrmSpcBef(line, InScrUnits) + this.GetTblSpcBef(line, InScrUnits);
  }

  internal new int GetParaFrameLine(int StartLine)
  {
    int fid = this.e.text[StartLine].fid;
    int num1 = 0;
    int x = this.e.PfmtId[this.e.text[StartLine].pfmt].flags & 12288 /*0x3000*/;
    int firstLine = this.e.PageInfo[this.e.CurPage].FirstLine;
    int num2 = this.e.CurPage + 1 != this.e.TotalPages ? this.e.PageInfo[this.e.CurPage].LastLine : this.e.TotalLines - 1;
    if (firstLine < this.e.RepageBeginLine)
      this.e.RepageBeginLine = firstLine;
    bool flag = this.e.text[StartLine].fid != 0;
    for (; num1 < 2; ++num1)
    {
      if (flag)
      {
        if (this.True(x))
        {
          for (int line = StartLine; line < this.e.TotalLines && (this.e.PfmtId[this.e.text[line].pfmt].flags & 12288 /*0x3000*/) == x; ++line)
          {
            if (this.e.text[line].page == this.e.CurPage && this.AnchorParaFound(line, fid) && line + 1 < this.e.TotalLines && (this.e.PfmtId[this.e.text[line + 1].pfmt].flags & 12288 /*0x3000*/) == x)
              return line + 1;
          }
        }
        else
        {
          for (int line = StartLine; line <= num2; ++line)
          {
            if (this.e.text[line].page == this.e.CurPage && (this.e.PfmtId[this.e.text[line].pfmt].flags & 12288 /*0x3000*/) == 0 && this.AnchorParaFound(line, fid) && line + 1 <= num2)
              return line + 1;
          }
        }
      }
      else if (this.True(x))
      {
        for (int line = StartLine - 1; line >= 0 && (this.e.PfmtId[this.e.text[line].pfmt].flags & 12288 /*0x3000*/) == x; --line)
        {
          if (this.e.text[line].page == this.e.CurPage && this.AnchorParaFound(line, fid) && line + 1 < this.e.TotalLines && (this.e.PfmtId[this.e.text[line + 1].pfmt].flags & 12288 /*0x3000*/) == x)
            return line + 1;
        }
      }
      else
      {
        if (StartLine == 0 && this.e.text[StartLine].fid == 0)
          return StartLine;
        for (int line = StartLine - 1; line >= firstLine - 1 && line >= 0; --line)
        {
          if (this.e.text[line].page == this.e.CurPage && this.AnchorParaFound(line, fid) && line + 1 <= num2)
            return line + 1;
        }
      }
      flag = !flag;
    }
    if (StartLine != 0 || this.e.text[StartLine].fid != 0)
    {
      for (int paraFrameLine = StartLine - 1; paraFrameLine >= firstLine - 1 && paraFrameLine >= 0; --paraFrameLine)
      {
        if (this.e.text[paraFrameLine].page == this.e.CurPage && (this.e.text[paraFrameLine].flags & 4) != 0 && (!this.True(this.e.text[paraFrameLine].cid) || !this.False(this.e.text[paraFrameLine].tabw) && (this.e.text[paraFrameLine].tabw.type & 32 /*0x20*/) != 0) && (!this.True(this.e.text[paraFrameLine].fid) || paraFrameLine <= 0 || this.e.text[paraFrameLine - 1].fid != this.e.text[paraFrameLine].fid))
          return paraFrameLine;
      }
      if (StartLine <= 0 || (this.e.text[StartLine - 1].flags & 1966080 /*0x1E0000*/) != 0)
        return StartLine;
      int index1 = StartLine - 1;
      int len = this.e.text[index1].len;
      char[] txt1 = this.e.text[index1].txt;
      char chr = len <= 0 ? char.MinValue : txt1[len - 1];
      if ((int) chr != (int) this.e.ParaChar && (int) chr != (int) this.e.CellChar && !this.lstrchr(this.e.BreakChars, chr))
      {
        this.LineAlloc(index1, len, len + 1);
        char[] txt2 = this.e.text[index1].txt;
        ushort[] numArray = this.OpenCfmt(index1);
        txt2[len] = this.e.ParaChar;
        int index2 = len;
        numArray[index2] = (ushort) 0;
        this.CloseCfmt(index1);
      }
    }
    return StartLine;
  }

  internal new int GetParaFrameSlot()
  {
    if (!this.e.InRtfRead)
    {
      for (int index = 1; index < this.e.TotalParaFrames; ++index)
        this.e.ParaFrame[index].InUse = false;
      for (int index = 0; index < this.e.TotalLines; ++index)
        this.e.ParaFrame[this.e.text[index].fid].InUse = true;
      for (int index = 0; index < this.e.TotalFonts; ++index)
      {
        if (this.e.TerFont[index].InUse && (this.e.TerFont[index].style & 128 /*0x80*/) != 0 && this.e.TerFont[index].FrameType != 0)
          this.e.ParaFrame[this.e.TerFont[index].ParaFID].InUse = true;
      }
      for (int paraFrameSlot = 1; paraFrameSlot < this.e.TotalParaFrames; ++paraFrameSlot)
      {
        if (!this.e.ParaFrame[paraFrameSlot].InUse)
        {
          this.e.ParaFrame[paraFrameSlot] = new tc.StrParaFrame();
          return paraFrameSlot;
        }
      }
    }
    if (this.e.TotalParaFrames >= this.e.MaxParaFrames)
    {
      int count = this.e.MaxParaFrames + this.e.MaxParaFrames / 2;
      this.e.ParaFrame = this.ReAlloc(this.e.ParaFrame, count);
      this.e.MaxParaFrames = count;
    }
    if (this.e.TotalParaFrames < this.e.MaxParaFrames)
    {
      ++this.e.TotalParaFrames;
      this.e.ParaFrame[this.e.TotalParaFrames - 1] = new tc.StrParaFrame();
      return this.e.TotalParaFrames - 1;
    }
    this.PrintError(110, nameof (GetParaFrameSlot));
    return -1;
  }

  internal new bool InitFrame(int FrameNo)
  {
    int num1 = 10;
    if (FrameNo + num1 >= this.e.MaxFrames)
    {
      int maxFrames = this.e.MaxFrames;
      while (FrameNo >= this.e.MaxFrames)
      {
        if (this.e.MaxFrames + num1 < 100)
          this.e.MaxFrames += 20;
        else
          this.e.MaxFrames += 50;
      }
      tc.StrFrame[] strFrameArray = new tc.StrFrame[this.e.MaxFrames];
      for (int index = 0; index < maxFrames; ++index)
        strFrameArray[index] = this.e.frame[index];
      this.e.frame = strFrameArray;
    }
    this.e.frame[FrameNo] = new tc.StrFrame();
    int num2;
    this.e.frame[FrameNo].ScrWidth = num2 = 9999;
    this.e.frame[FrameNo].width = num2;
    int terWinHeight;
    this.e.frame[FrameNo].ScrHeight = terWinHeight = this.e.TerWinHeight;
    this.e.frame[FrameNo].height = terWinHeight;
    this.e.frame[FrameNo].BackColor = tc.CLR_WHITE;
    this.e.frame[FrameNo].BorderWidth = new int[4];
    this.e.frame[FrameNo].BorderColor = new Color[4]
    {
      tc.CLR_AUTO,
      tc.CLR_AUTO,
      tc.CLR_AUTO,
      tc.CLR_AUTO
    };
    return true;
  }

  internal int InsertParaFrame(
    int x,
    int y,
    int width,
    int height,
    bool boxed,
    int InitFlags,
    bool insert,
    bool rotatedFrame = false)
  {
    if (!this.e.TerArg.PrintView && !this.e.RotatedFrame)
      return 0;
    if (!this.CheckLineLimit(this.e.TotalLines + 1))
    {
      this.PrintError(88, this.e.MsgString[20]);
      return 0;
    }
    this.ReleaseUndo();
    int paraFrameSlot;
    if ((paraFrameSlot = this.GetParaFrameSlot()) < 0)
      return 0;
    if (x == -1)
      x = this.e.NewFrameX;
    if (y == -1)
      y = this.e.NewFrameY;
    if (x == -1)
    {
      x = this.ColToUnits(this.e.CurCol, this.e.CurLine, 1024 /*0x0400*/);
      x = this.ScrToTwipsX(x);
      if (this.e.BorderShowing)
        x -= this.UnitToTwipsX(this.GetBorderLeftSpace(this.e.CurPage));
    }
    if (width < 0)
      width = this.e.NewFrameWidth;
    if (height < 0)
      height = this.e.NewFrameHeight;
    this.e.ParaFrame[paraFrameSlot] = new tc.StrParaFrame();
    this.e.ParaFrame[paraFrameSlot].InUse = true;
    this.e.ParaFrame[paraFrameSlot].x = x;
    this.e.ParaFrame[paraFrameSlot].width = width;
    this.e.ParaFrame[paraFrameSlot].height = height;
    this.e.ParaFrame[paraFrameSlot].margin = 0;
    this.e.ParaFrame[paraFrameSlot].MinHeight = height;
    this.e.ParaFrame[paraFrameSlot].DistFromText = 180;
    this.e.ParaFrame[paraFrameSlot].PageNo = this.e.CurPage;
    this.e.ParaFrame[paraFrameSlot].flags = InitFlags;
    this.e.ParaFrame[paraFrameSlot].ZOrder = 1;
    this.e.ParaFrame[paraFrameSlot].rotatedFrame = rotatedFrame;
    if (this.e.NewFrameVPage)
      this.e.ParaFrame[paraFrameSlot].flags |= 32 /*0x20*/;
    int paraFrameLine = this.GetParaFrameLine(this.e.CurLine);
    int line = paraFrameLine;
    while (line < this.e.TotalLines && this.e.text[line].fid != 0)
      ++line;
    if (line == this.e.TotalLines)
      --line;
    int twipsY1 = this.ScrToTwipsY(this.LineToUnits(line));
    int twipsY2 = this.ScrToTwipsY(this.LineToUnits(this.e.CurLine));
    if ((this.e.ParaFrame[paraFrameSlot].flags & 32 /*0x20*/) != 0)
    {
      if (y == -1)
        y = this.FrameToPageY(twipsY2);
      int num;
      this.e.ParaFrame[paraFrameSlot].y = num = y;
      this.e.ParaFrame[paraFrameSlot].ParaY = num;
    }
    else
    {
      if (y == -1)
        y = twipsY2 - twipsY1;
      this.e.ParaFrame[paraFrameSlot].ParaY = y;
      this.e.ParaFrame[paraFrameSlot].y = this.e.ParaFrame[paraFrameSlot].ParaY + twipsY1;
    }
    if (insert)
    {
      int flags = this.e.PfmtId[this.e.text[paraFrameLine].pfmt].flags;
      tc.ResetUintFlag(ref flags, 1016);
      if (boxed)
        flags |= 65776 /*0x0100F0*/;
      this.MoveLineArrays(paraFrameLine, 1, 'B');
      this.e.text[paraFrameLine].fid = paraFrameSlot;
      this.e.text[paraFrameLine].cid = 0;
      this.e.text[paraFrameLine].pfmt = this.NewParaId(this.e.text[paraFrameLine].pfmt, this.e.PfmtId[this.e.text[paraFrameLine].pfmt].LeftIndentTwips, this.e.PfmtId[this.e.text[paraFrameLine].pfmt].RightIndentTwips, this.e.PfmtId[this.e.text[paraFrameLine].pfmt].FirstIndentTwips, this.e.PfmtId[this.e.text[paraFrameLine].pfmt].TabId, 0, this.e.PfmtId[this.e.text[paraFrameLine].pfmt].AuxId, this.e.PfmtId[this.e.text[paraFrameLine].pfmt].Aux1Id, this.e.PfmtId[this.e.text[paraFrameLine].pfmt].StyId, this.e.PfmtId[this.e.text[paraFrameLine].pfmt].shading, this.e.PfmtId[this.e.text[paraFrameLine].pfmt].pflags | 1, this.e.PfmtId[this.e.text[paraFrameLine].pfmt].SpaceBefore, this.e.PfmtId[this.e.text[paraFrameLine].pfmt].SpaceAfter, this.e.PfmtId[this.e.text[paraFrameLine].pfmt].SpaceBetween, this.e.PfmtId[this.e.text[paraFrameLine].pfmt].LineSpacing, this.e.PfmtId[this.e.text[paraFrameLine].pfmt].BkColor, this.e.PfmtId[this.e.text[paraFrameLine].pfmt].BorderSpace, this.e.PfmtId[this.e.text[paraFrameLine].pfmt].flow, flags);
      this.LineAlloc(paraFrameLine, 0, 1);
      char[] txt = this.e.text[paraFrameLine].txt;
      ushort[] numArray = this.OpenCfmt(paraFrameLine);
      int paraChar = (int) this.e.ParaChar;
      txt[0] = (char) paraChar;
      numArray[0] = (ushort) 0;
      this.CloseCfmt(paraFrameLine);
      this.e.CurLine = paraFrameLine;
      this.e.CurCol = 0;
      this.Repaginate(false, true, 0, true);
    }
    return paraFrameSlot;
  }

  internal new bool IsFramePict(int pict)
  {
    if (pict >= 0 && pict < this.e.TotalFonts && this.e.TerFont[pict].InUse && (this.e.TerFont[pict].style & 128 /*0x80*/) != 0 && this.e.TerFont[pict].FrameType != 0)
    {
      int paraFid = this.e.TerFont[pict].ParaFID;
      if (paraFid >= 0 && paraFid < this.e.TotalParaFrames)
        return true;
    }
    return false;
  }

  internal new bool LinePointsToRect(int ParaFID, int x1, int y1, int x2, int y2)
  {
    if ((this.e.ParaFrame[ParaFID].flags & 256 /*0x0100*/) != 0)
    {
      if (x2 < x1)
      {
        this.SwapInts(ref x1, ref x2);
        this.SwapInts(ref y1, ref y2);
      }
      this.e.ParaFrame[ParaFID].x = x1;
      this.e.ParaFrame[ParaFID].width = x2 - x1;
      if (y2 > y1)
      {
        this.e.ParaFrame[ParaFID].y = y1;
        this.e.ParaFrame[ParaFID].MinHeight = y2 - y1;
        this.e.ParaFrame[ParaFID].LineType = 3;
      }
      else
      {
        this.e.ParaFrame[ParaFID].y = y2;
        this.e.ParaFrame[ParaFID].MinHeight = y1 - y2;
        this.e.ParaFrame[ParaFID].LineType = 2;
      }
      if (y1 == y2)
        this.e.ParaFrame[ParaFID].LineType = 0;
      if (x1 == x2)
        this.e.ParaFrame[ParaFID].LineType = 1;
      this.e.ParaFrame[ParaFID].height = this.e.ParaFrame[ParaFID].MinHeight;
    }
    return true;
  }

  internal new bool LineRectToPoints(int ParaFID, out int x1, out int y1, out int x2, out int y2)
  {
    int num1;
    y2 = num1 = 0;
    int num2;
    x2 = num2 = num1;
    int num3;
    y1 = num3 = num2;
    x1 = num3;
    if ((this.e.ParaFrame[ParaFID].flags & 256 /*0x0100*/) != 0)
    {
      x1 = this.e.ParaFrame[ParaFID].x;
      x2 = this.e.ParaFrame[ParaFID].x + this.e.ParaFrame[ParaFID].width;
      y1 = this.e.ParaFrame[ParaFID].y;
      y2 = this.e.ParaFrame[ParaFID].y + this.e.ParaFrame[ParaFID].MinHeight;
      if (this.e.ParaFrame[ParaFID].LineType == 2)
        this.SwapInts(ref y1, ref y2);
      if (this.e.ParaFrame[ParaFID].LineType == 0)
        y2 = y1;
      if (this.e.ParaFrame[ParaFID].LineType == 1)
        x2 = x1;
    }
    return true;
  }

  internal new int LineTextAngle(int LineNo)
  {
    int num = 0;
    if (LineNo < 0)
      return this.GetFrameTextAngle(-LineNo);
    if (LineNo >= this.e.TotalLines)
      return 0;
    int fid = this.e.text[LineNo].fid;
    int cid = this.e.text[LineNo].cid;
    if (this.e.AllTextAngle > 0)
      num = this.e.AllTextAngle;
    else if (fid != 0)
      num = this.e.ParaFrame[fid].TextAngle;
    if (num == 0 && cid > 0)
      num = this.e.cell[cid].TextAngle;
    return num;
  }

  internal int LineTextAngle2(int LineNo)
  {
    return this.e.AllTextAngle2 != 0 ? this.e.AllTextAngle2 : this.LineTextAngle(LineNo);
  }

  internal bool MapRtlCell(int frm)
  {
    if (this.e.frame[frm].CellId != 0 && (this.e.frame[frm].flags & 262144 /*0x040000*/) == 0)
    {
      this.e.frame[frm].flags |= 262144 /*0x040000*/;
      int cellId = this.e.frame[frm].CellId;
      int row = this.e.cell[cellId].row;
      if (this.IsParaRtl(0, this.e.TableRow[row].flow, this.e.TerSect[this.e.frame[frm].sect].flow, this.e.DocTextFlow))
      {
        int firstFrame = this.e.TableRow[row].FirstFrame;
        int origX = this.e.frame[firstFrame].OrigX;
        int lastFrame = this.e.TableRow[row].LastFrame;
        int num1 = this.e.frame[lastFrame].OrigX + this.e.frame[lastFrame].width - origX;
        if (this.e.cell[cellId].level == 0 && this.e.BorderShowing)
        {
          int sect = this.e.frame[frm].sect;
          num1 += (int) ((double) this.e.TerSect[sect].RightMargin * (double) this.e.UnitResX);
        }
        int num2 = this.e.frame[frm].OrigX - origX;
        int num3 = num1 - num2 - this.e.frame[frm].width;
        if (this.e.cell[cellId].level > 0)
        {
          int frameId = this.e.CellAux[this.e.cell[cellId].ParentCell].FrameId;
          if ((this.e.frame[frameId].flags & 262144 /*0x040000*/) == 0)
            this.MapRtlCell(frameId);
          this.e.frame[frm].x = this.e.frame[frameId].x + this.e.frame[frameId].SpaceLeft + num3;
        }
        else
          this.e.frame[frm].x = this.e.frame[firstFrame].OrigX + num3;
        this.e.frame[frm].flags |= 131072 /*0x020000*/;
        this.e.TableRow[row].flags |= 65536 /*0x010000*/;
      }
      else
        tc.ResetUintFlag(ref this.e.TableRow[row].flags, 65536 /*0x010000*/);
    }
    return true;
  }

  internal bool MapRtlCol(
    int[] FrameColFirst,
    int[] FrameColX,
    int[] FrameColWidth,
    int SectColSpace,
    int NextFrame,
    int MaxColumns)
  {
    int num1 = 0;
    for (int index1 = 0; index1 < MaxColumns; ++index1)
    {
      if (index1 + 1 < MaxColumns)
      {
        int[] numArray;
        IntPtr index2;
        (numArray = FrameColWidth)[(int) (index2 = (IntPtr) index1)] = numArray[(int) index2] + SectColSpace;
      }
      num1 += FrameColWidth[index1];
    }
    int num2 = num1;
    for (int index3 = 0; index3 < MaxColumns; ++index3)
    {
      int num3 = index3 + 1 >= MaxColumns ? NextFrame - 1 : FrameColFirst[index3 + 1] - 1;
      int num4 = FrameColWidth[index3];
      num2 -= FrameColWidth[index3];
      int num5 = FrameColX[index3];
      int num6 = FrameColX[0];
      int num7 = num2 + FrameColX[0];
      for (int index4 = FrameColFirst[index3]; index4 <= num3; ++index4)
      {
        if (this.e.frame[index4].ParaFrameId <= 0)
        {
          int num8 = this.e.frame[index4].x - FrameColX[index3];
          this.e.frame[index4].x = num7 + num8;
          if (this.e.frame[index4].CellId == 0)
          {
            this.e.frame[index4].width = num4;
            int num9;
            this.e.frame[index4].SpaceRight = num9 = 0;
            this.e.frame[index4].SpaceLeft = num9;
            if (index3 + 1 < MaxColumns)
              this.e.frame[index4].SpaceLeft = SectColSpace;
          }
          else if (index3 != 0)
          {
            this.e.frame[index4].x -= SectColSpace;
            if (this.e.BorderShowing)
              this.e.frame[index4].x -= SectColSpace;
          }
        }
      }
    }
    return true;
  }

  internal new int PageToFrameY(int y)
  {
    if (this.e.BorderShowing)
      y += this.UnitToTwipsY(this.e.TopBorderHeight);
    else if (!this.e.ViewPageHdrFtr)
    {
      int pageSect = this.e.TerGetPageSect(this.e.CurPage);
      y -= (int) ((double) this.e.TerSect[pageSect].TopMargin * 1440.0);
    }
    if (this.e.CurPage > this.e.FirstFramePage)
      y += this.ScrToTwipsY(this.e.FirstPageHeight);
    return y;
  }

  internal new int ParaIdForFrame(int CurPara, int HdrFtr)
  {
    int flags1 = this.e.PfmtId[CurPara].flags;
    tc.ResetUintFlag(ref flags1, 12288 /*0x3000*/);
    int flags2 = flags1 | HdrFtr;
    return this.NewParaId(CurPara, this.e.PfmtId[CurPara].LeftIndentTwips, this.e.PfmtId[CurPara].RightIndentTwips, this.e.PfmtId[CurPara].FirstIndentTwips, this.e.PfmtId[CurPara].TabId, 0, this.e.PfmtId[CurPara].AuxId, this.e.PfmtId[CurPara].Aux1Id, this.e.PfmtId[CurPara].StyId, this.e.PfmtId[CurPara].shading, this.e.PfmtId[CurPara].pflags, this.e.PfmtId[CurPara].SpaceBefore, this.e.PfmtId[CurPara].SpaceAfter, this.e.PfmtId[CurPara].SpaceBetween, this.e.PfmtId[CurPara].LineSpacing, this.e.PfmtId[CurPara].BkColor, this.e.PfmtId[CurPara].BorderSpace, this.e.PfmtId[CurPara].flow, flags2);
  }

  internal bool PosPictFrames(
    int LineNo,
    int CurX,
    int CurY,
    int HiddenY,
    int HdrMargin,
    int HdrHeight,
    int TopSect,
    int PageNo)
  {
    bool flag1 = false;
    int num1 = 0;
    int num2 = 0;
    bool flag2 = false;
    if ((this.e.text[LineNo].flags & 16384 /*0x4000*/) == 0)
      return false;
    ushort[] numArray = this.OpenCfmt(LineNo);
    int fid = this.e.text[LineNo].fid;
    if (this.True(fid) && (this.e.ParaFrame[fid].flags & 32768 /*0x8000*/) == 0)
      return false;
    for (int index1 = 0; index1 < this.e.text[LineNo].len; ++index1)
    {
      int index2 = (int) numArray[index1];
      if (!this.False(this.e.TerFont[index2].InUse) && (this.e.TerFont[index2].style & 128 /*0x80*/) != 0 && this.e.TerFont[index2].FrameType != 0 && this.e.TerFont[index2].ParaFID != 0)
        tc.ResetUintFlag(ref this.e.ParaFrame[this.e.TerFont[index2].ParaFID].flags, 32768 /*0x8000*/);
    }
    for (int index3 = 0; index3 < this.e.text[LineNo].len; ++index3)
    {
      int index4 = (int) numArray[index3];
      if (this.False(this.e.TerFont[index4].InUse) || (this.e.TerFont[index4].style & 128 /*0x80*/) == 0 || this.e.TerFont[index4].FrameType == 0 || this.e.TerFont[index4].ParaFID == 0)
      {
        flag2 = true;
      }
      else
      {
        int paraFid = this.e.TerFont[index4].ParaFID;
        int num3 = this.e.TerFont[index4].FrameType != 3 ? 1 : ((this.e.ParaFrame[paraFid].flags & 12) != 0 ? 1 : 0);
        if (this.e.TerFont[index4].PictType != 11)
        {
          this.e.ParaFrame[paraFid].width = this.e.TerFont[index4].PictWidth + 2 * this.e.ParaFrame[paraFid].margin;
          this.e.ParaFrame[paraFid].height = this.e.TerFont[index4].PictHeight + 2 * this.e.ParaFrame[paraFid].margin;
          this.e.ParaFrame[paraFid].MinHeight = this.e.ParaFrame[paraFid].height;
        }
        this.e.ParaFrame[paraFid].TextLine = LineNo;
        this.e.ParaFrame[paraFid].CellId = this.e.text[LineNo].cid;
        int CurY1 = CurY;
        if (this.e.HtmlMode & flag2)
          CurY1 += this.e.text[LineNo].height + this.TwipsToUnitY(50);
        if (num3 != 0)
        {
          int twipsX1 = this.UnitToTwipsX(CurX);
          if (this.e.text[LineNo].cid > 0)
          {
            int cid = this.e.text[LineNo].cid;
            int border = this.e.cell[cid].border;
            twipsX1 += this.e.cell[cid].margin;
            CurY1 += this.TwipsToUnitY(this.GetCellFrameTopWidth(cid, ref border, PageNo, out tc.SkipColor));
          }
          int num4;
          if (this.e.TerFont[index4].FrameType == 2 || (this.e.ParaFrame[paraFid].flags & 4) != 0)
          {
            int twipsX2 = this.ScrToTwipsX(this.wrp.TerWrapWidth2(LineNo, -1, true));
            num4 = twipsX1 + twipsX2 - this.e.ParaFrame[paraFid].width - num2;
          }
          else if (this.e.TerFont[index4].FrameType == 2 || (this.e.ParaFrame[paraFid].flags & 8) != 0)
          {
            int twipsX3 = this.ScrToTwipsX(this.wrp.TerWrapWidth2(LineNo, -1, true));
            num4 = twipsX1 + (twipsX3 - this.e.ParaFrame[paraFid].width) / 2 - num2;
          }
          else
            num4 = twipsX1 + num1;
          this.e.ParaFrame[paraFid].x = num4;
          this.e.ParaFrame[paraFid].ParaY = 0;
          if ((this.e.text[LineNo].flags & 4) == 0)
            this.e.ParaFrame[paraFid].ParaY = this.UnitToTwipsY(this.e.text[LineNo].height);
          if (this.e.TerFont[index4].FrameType == 2)
            num2 += this.e.ParaFrame[paraFid].width;
          else
            num1 += this.e.ParaFrame[paraFid].width;
        }
        else
        {
          int unitX = this.TwipsToUnitX(this.e.ParaFrame[paraFid].OrgX);
          if (this.e.text[LineNo].cid > 0 && (this.e.ParaFrame[paraFid].flags & 16777216 /*0x01000000*/) != 0)
            unitX += CurX;
          else if ((this.e.ParaFrame[paraFid].flags & 1610612736 /*0x60000000*/) != 0)
            unitX += CurX;
          if ((this.e.ParaFrame[paraFid].flags & 1) != 0)
            unitX -= (int) ((double) this.e.UnitResX * (double) this.e.TerSect[TopSect].LeftMargin);
          if ((this.e.ParaFrame[paraFid].flags & 1610612736 /*0x60000000*/) != 0 && this.e.text[LineNo].tabw != null && this.e.text[LineNo].tabw.FrameCharPos == 0 && this.e.text[LineNo].tabw.FrameSpaceWidth > 0)
          {
            int FrameWidth;
            int FrameHt;
            this.GetFrameSpace(LineNo, tc.SkipRect, out int _, out FrameWidth, out FrameHt);
            if (FrameHt == 0)
              unitX += FrameWidth;
          }
          this.e.ParaFrame[paraFid].x = this.UnitToTwipsX(unitX);
        }
        tc.ResetUintFlag(ref this.e.ParaFrame[paraFid].flags, 32768 /*0x8000*/);
        this.SetParaFrameY(paraFid, -1, CurY1, HiddenY, HdrMargin, HdrHeight, TopSect, LineNo);
        ++this.e.PageInfo[PageNo].FrameCount;
      }
    }
    this.CloseCfmt(LineNo);
    return flag1;
  }

  internal new bool RefreshFrames(bool ForceRefresh)
  {
    bool flag = this.e.TotalLines != this.e.FrameRefreshLineCount | ForceRefresh || this.e.TotalFrames == 1 || (this.e.TerOpFlags & 128 /*0x80*/) != 0;
    if (this.e.FrameRefreshEnabled)
    {
      int curPage1 = this.e.CurPage;
      int LastPage = this.e.CurPage + 1;
      if (this.e.CurPage >= this.e.FirstFramePage && this.e.CurPage <= this.e.LastFramePage)
      {
        if (this.e.FirstFramePage == this.e.LastFramePage)
        {
          this.CreateFrames(false, this.e.CurPage, this.e.CurPage + 1);
        }
        else
        {
          int num1 = this.e.TerWinOrgY + this.e.TerWinHeight / 2;
          if (this.e.CurPage == this.e.FirstFramePage)
          {
            if (num1 < this.e.FirstPageHeight / 2 && this.e.CurPage > 0)
            {
              int PageNo = this.e.CurPage - 1;
              int curPage2 = this.e.CurPage;
              if (flag || PageNo != this.e.FirstFramePage || curPage2 != this.e.LastFramePage)
                this.CreateFrames(false, PageNo, curPage2);
              this.e.TerWinOrgY += this.e.FirstPageHeight;
              this.SetTerWindowOrg();
            }
            else if (flag || curPage1 != this.e.FirstFramePage || LastPage != this.e.LastFramePage)
              this.CreateFrames(false, curPage1, LastPage);
          }
          else
          {
            int num2 = (this.e.CurPageHeight - this.e.FirstPageHeight) / 2 + this.e.FirstPageHeight;
            if (num1 > num2 && this.e.CurPage + 1 < this.e.TotalPages)
            {
              this.e.TerWinOrgY -= this.e.FirstPageHeight;
              if (this.e.TerWinOrgY < 0)
                this.e.TerWinOrgY = 0;
              if (flag || curPage1 != this.e.FirstFramePage || LastPage != this.e.LastFramePage)
                this.CreateFrames(false, curPage1, LastPage);
              this.SetTerWindowOrg();
            }
            else
            {
              int PageNo = this.e.CurPage - 1;
              int curPage3 = this.e.CurPage;
              if (flag || PageNo != this.e.FirstFramePage || curPage3 != this.e.LastFramePage)
                this.CreateFrames(false, PageNo, curPage3);
            }
          }
        }
      }
      else
        this.CreateFrames(false, this.e.CurPage, this.e.CurPage + 1);
      this.e.FrameRefreshLineCount = this.e.TotalLines;
    }
    return true;
  }

  internal new bool ReposPictFrames()
  {
    for (int index1 = 0; index1 < this.e.TotalFonts; ++index1)
    {
      if (this.e.TerFont[index1].InUse && (this.e.TerFont[index1].style & 128 /*0x80*/) != 0 && this.e.TerFont[index1].FrameType == 3)
      {
        ref tc.StrFont local = ref this.e.TerFont[index1];
        int pCol;
        int pLineNo = pCol = 0;
        if (this.e.TerLocateFontId(index1, ref pLineNo, ref pCol))
        {
          int curCfmt1 = this.GetCurCfmt(pLineNo, 0);
          int curCfmt2 = this.GetCurCfmt(pLineNo, pCol);
          if (this.e.TerFont[curCfmt1].FieldId == 0 && this.e.TerFont[curCfmt2].FieldId == 0)
          {
            int page = this.e.text[pLineNo].page;
            int index2 = pLineNo;
            while (index2 >= 0 && (this.e.text[index2].flags & 4) == 0 && index2 != this.e.PageInfo[page].FirstLine)
              --index2;
            if (index2 != this.e.PageInfo[page].FirstLine)
            {
              if (index2 < 0)
                index2 = 0;
              if (pLineNo != index2 || pCol != 0)
              {
                int abs1 = this.RowColToAbs(pLineNo, pCol);
                int abs2 = this.RowColToAbs(index2, 0);
                int abs3 = this.RowColToAbs(this.e.CurLine, this.e.CurCol);
                this.AnchorPictFrame(index1, index2, 0);
                if (abs3 >= abs2 && abs3 <= abs1)
                {
                  ++this.e.CurCol;
                  if (this.e.CurCol >= this.e.text[this.e.CurLine].len && this.e.text[this.e.CurLine].len > 0)
                    this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
                }
              }
            }
          }
        }
      }
    }
    this.e.TerOpFlags2 &= -5;
    return true;
  }

  internal bool SetBoxFrameHt(int BoxFrame, int PrevParaFrame, int BoxHeight, int FrameLastLine)
  {
    int textAngle = this.e.ParaFrame[PrevParaFrame].TextAngle;
    tc.StrFrame strFrame = this.e.frame[BoxFrame];
    int spaceTop = strFrame.SpaceTop;
    int spaceBot = strFrame.SpaceBot;
    strFrame.PageLastLine = FrameLastLine;
    if (textAngle == 0 && (this.e.ParaFrame[PrevParaFrame].flags & 67108864 /*0x04000000*/) == 0)
    {
      int unitY = this.TwipsToUnitY(this.e.ParaFrame[PrevParaFrame].MinHeight);
      if (unitY == 0 || (this.e.ParaFrame[PrevParaFrame].flags & 128 /*0x80*/) == 0)
      {
        int num = BoxHeight + spaceTop + spaceBot;
        if (num > unitY)
          strFrame.height = num;
        if (strFrame.height != this.TwipsToUnitY(this.e.ParaFrame[PrevParaFrame].height))
        {
          this.e.PaintFlag = 6;
          this.e.ParaFrame[PrevParaFrame].height = this.UnitToTwipsY(strFrame.height);
        }
      }
    }
    strFrame.ScrHeight = this.UnitToScrY(strFrame.height);
    strFrame.TextHeight = 0;
    this.e.frame[BoxFrame] = strFrame;
    return true;
  }

  internal bool SetFrameLineInfo(
    int l,
    int y,
    int ScrY,
    ref int pFrameHt,
    ref int pScrFrameHt,
    int CurColHeight,
    int CellX,
    ref int pTableRowIndent,
    int PageNo,
    int HiddenY,
    int HdrMargin,
    int HdrHeight,
    int TopSect,
    int TopLeftMargin,
    int ColumnNo,
    ref bool pHasPictFrames,
    int sect,
    int CurLineHt)
  {
    int x1 = pFrameHt;
    int num1 = pScrFrameHt;
    int fid1 = this.e.text[l].fid;
    if (fid1 != 0 && (this.e.ParaFrame[fid1].flags & 32768 /*0x8000*/) != 0)
      this.e.text[l].y = this.MulDiv(this.e.ParaFrame[fid1].y, this.e.UnitResY, 1440) + x1;
    else
      this.e.text[l].y = y + CurColHeight + x1 - this.e.FirstPageHeight;
    this.e.text[l].x = CellX;
    if ((this.e.text[l].flags & 8) != 0)
      this.e.PageHasControls = true;
    this.e.text[l].flags &= -4097;
    if (this.e.text[l].cid != 0 && this.e.cell[this.e.text[l].cid].PrevCell <= 0 && (l == 0 || this.e.text[l].cid != this.e.text[l - 1].cid))
    {
      int row = this.e.cell[this.e.text[l].cid].row;
      int frmSpcBef = this.e.TableRow[row].FrmSpcBef;
      this.e.text[l].y += frmSpcBef;
      y += frmSpcBef;
      ScrY += this.MulDiv(frmSpcBef, this.e.ScrResY, this.e.UnitResY);
      pTableRowIndent = this.e.TableRow[row].CurIndent;
    }
    if (this.e.text[l].cid != 0 && this.e.cell[this.e.text[l].cid].PrevCell <= 0)
      this.e.text[l].x += pTableRowIndent;
    if ((this.e.text[l].flags & 16384 /*0x4000*/) != 0)
    {
      int fid2 = this.e.text[l].fid;
      if (this.e.repaginating && l <= this.e.PageInfo[PageNo].LastLine)
      {
        int x2 = this.e.text[l].x;
        if (this.e.BorderShowing)
          x2 -= this.e.LeftBorderWidth + TopLeftMargin;
        this.PosPictFrames(l, x2, this.e.text[l].y, HiddenY, HdrMargin, HdrHeight, TopSect, PageNo);
      }
      pHasPictFrames = true;
    }
    if (this.e.text[l].tabw == null || (this.e.text[l].tabw.type & 32 /*0x20*/) == 0)
    {
      int num2 = 0;
      if (this.e.text[l].fid == 0)
        num2 = this.CalcFrmSpcBef(l, sect, true, PageNo);
      x1 = x1 + CurLineHt + num2;
      num1 = this.MulDiv(x1, this.e.ScrResY, this.e.UnitResY);
      this.e.text[l].y += num2;
    }
    if (this.e.text[l].tabw != null && (this.e.text[l].tabw.type & 16384 /*0x4000*/) != 0)
    {
      int repageBeginLine = this.e.RepageBeginLine;
      this.fld.UpdateDynField(l, PageNo);
      this.e.RepageBeginLine = repageBeginLine;
    }
    this.e.text[l].JustAdjX = 0;
    if ((this.e.text[l].flags & 536870912 /*0x20000000*/) != 0)
    {
      if (this.e.text[l].tabw != null && this.e.text[l].tabw.count > 0)
        this.e.text[l].JustAdjX = this.e.text[l].tabw.width[0];
    }
    else if ((this.e.PfmtId[this.e.text[l].pfmt].flags & 3) != 0 && !this.LineInfo(l, 1024 /*0x0400*/))
    {
      int pfmt = this.e.text[l].pfmt;
      int sect1 = this.e.TerSect[TopSect].columns == this.e.TerSect[sect].columns ? TopSect : sect;
      int x3 = this.wrp.TerWrapWidth2(l, sect1, true) - this.e.PfmtId[pfmt].LeftIndent - ((this.e.text[l].flags & 4) != 0 ? this.e.PfmtId[pfmt].FirstIndent : 0) - this.e.PfmtId[pfmt].RightIndent - this.GetLineWidth(l, false, true);
      if (x3 > 0)
      {
        this.e.text[l].JustAdjX = this.MulDiv(x3, this.e.UnitResX, this.e.ScrResX);
        if ((this.e.PfmtId[pfmt].flags & 1) != 0)
          this.e.text[l].JustAdjX /= 2;
      }
    }
    pFrameHt = x1;
    pScrFrameHt = num1;
    return true;
  }

  internal bool SetParaFrameY(
    int ParaFID,
    int FrameNo,
    int CurY,
    int HiddenY,
    int HdrMargin,
    int HdrHeight,
    int TopSect,
    int line)
  {
    int x1 = 0;
    if ((this.e.ParaFrame[ParaFID].flags & 33554432 /*0x02000000*/) != 0)
    {
      float x2 = this.e.TerSect[TopSect].IsPortrait ? this.e.TerSect[TopSect].PprHeight : this.e.TerSect[TopSect].PprWidth;
      if ((this.e.ParaFrame[ParaFID].flags & 64 /*0x40*/) != 0)
        x2 = x2 - this.e.TerSect[TopSect].TopMargin - this.e.TerSect[TopSect].BotMargin;
      else
        this.e.ParaFrame[ParaFID].flags |= 32 /*0x20*/;
      int twips = (int) this.InchesToTwips((double) x2);
      this.e.ParaFrame[ParaFID].ParaY = (twips - this.e.ParaFrame[ParaFID].height) / 2;
    }
    if ((this.e.ParaFrame[ParaFID].flags & 96 /*0x60*/) != 0)
    {
      bool flag = false;
      if ((this.e.ParaFrame[ParaFID].flags & 16777216 /*0x01000000*/) != 0 && line >= 0 && line < this.e.TotalLines && this.e.text[line].cid > 0)
      {
        int cid = this.e.text[line].cid;
        for (int index = 0; index < this.e.TotalFrames; ++index)
        {
          if (index != FrameNo && this.e.frame[index].CellId == cid)
          {
            x1 = this.e.frame[index].y + this.TwipsToUnitY(this.e.ParaFrame[ParaFID].ParaY);
            flag = true;
            break;
          }
        }
      }
      if (!flag)
      {
        int unitY = this.TwipsToUnitY(this.e.ParaFrame[ParaFID].ParaY);
        if ((this.e.ParaFrame[ParaFID].flags & 64 /*0x40*/) != 0)
          unitY += (int) ((double) this.e.UnitResY * (double) this.e.TerSect[TopSect].TopMargin);
        x1 = !this.e.BorderShowing ? (!this.e.ViewPageHdrFtr ? unitY - HdrMargin - HdrHeight : unitY - HiddenY) : unitY + this.e.TopBorderHeight;
        if (FrameNo >= 0 && FrameNo >= this.e.FirstPage2Frame)
          x1 += this.e.FirstPageHeight;
      }
      if (FrameNo >= 0)
      {
        this.e.frame[FrameNo].y = x1;
        this.e.frame[FrameNo].ScrY = this.UnitToScrY(x1);
      }
      this.e.ParaFrame[ParaFID].y = this.UnitToTwipsY(x1 - this.e.FirstPageHeight);
      if (this.e.repaginating)
        this.e.ParaFrame[ParaFID].flags |= 32768 /*0x8000*/;
      return true;
    }
    if ((this.e.ParaFrame[ParaFID].flags & 32768 /*0x8000*/) == 0 || this.e.repaginating && this.e.ParaFrame[ParaFID].ParaY > 0)
    {
      this.e.ParaFrame[ParaFID].PageY = this.UnitToTwipsY(CurY) + this.e.ParaFrame[ParaFID].ParaY - this.UnitToTwipsY(this.e.FirstPageHeight);
      if (this.e.BorderShowing)
        this.e.ParaFrame[ParaFID].PageY -= this.UnitToTwipsY(this.e.TopBorderHeight);
      else if (this.e.ViewPageHdrFtr)
        this.e.ParaFrame[ParaFID].PageY += this.UnitToTwipsY(HiddenY);
      else
        this.e.ParaFrame[ParaFID].PageY += this.UnitToTwipsY(HdrMargin + HdrHeight);
      if (this.e.repaginating)
        this.e.ParaFrame[ParaFID].flags |= 32768 /*0x8000*/;
    }
    int x3 = !this.e.BorderShowing ? (!this.e.ViewPageHdrFtr ? this.TwipsToUnitY(this.e.ParaFrame[ParaFID].PageY) - HdrMargin - HdrHeight : this.TwipsToUnitY(this.e.ParaFrame[ParaFID].PageY) - HiddenY) : this.TwipsToUnitY(this.e.ParaFrame[ParaFID].PageY) + this.e.TopBorderHeight;
    this.e.ParaFrame[ParaFID].y = this.UnitToTwipsY(x3);
    if (FrameNo >= 0)
    {
      this.e.frame[FrameNo].y = x3;
      if (FrameNo >= this.e.FirstPage2Frame)
        this.e.frame[FrameNo].y += this.e.FirstPageHeight;
      this.e.frame[FrameNo].ScrY = this.UnitToScrY(this.e.frame[FrameNo].y);
    }
    return true;
  }

  internal bool SortFrames()
  {
    bool flag1 = false;
    this.e.FramesSorted = false;
    for (int index = 0; index < this.e.TotalFrames; ++index)
    {
      this.e.frame[index].DispFrame = index;
      if (this.e.frame[index].ZOrder != 0 || this.e.frame[index].level > 0)
        flag1 = true;
    }
    if (flag1)
    {
      CFrm.StrZOrder[] strZorderArray = new CFrm.StrZOrder[this.e.TotalFrames];
      for (int index = 0; index < this.e.TotalFrames; ++index)
      {
        if (this.e.frame[index].ParaFrameId > 0 && this.e.frame[index].ZOrder == 0 && this.e.ParaFrame[this.e.frame[index].ParaFrameId].pict > 0)
          this.e.frame[index].ZOrder = 1;
        strZorderArray[index].ZOrder = (double) this.e.frame[index].ZOrder;
        strZorderArray[index].level = this.e.frame[index].level;
        strZorderArray[index].FrameId = index;
        strZorderArray[index].line = this.e.frame[index].PageFirstLine;
        strZorderArray[index].ParaFID = this.e.frame[index].ParaFrameId;
        if (!this.e.frame[index].empty)
        {
          int pfmt = this.e.text[this.e.frame[index].PageFirstLine].pfmt;
          int paraFrameId = this.e.frame[index].ParaFrameId;
          if ((this.e.PfmtId[pfmt].flags & 4096 /*0x1000*/) != 0 && (this.e.ParaFrame[paraFrameId].flags & 768 /*0x0300*/) != 0)
          {
            strZorderArray[index].ZOrder -= 16384.0;
            if (strZorderArray[index].ZOrder >= 0.0)
              strZorderArray[index].ZOrder = -1.0;
          }
        }
        if ((this.e.frame[index].flags & 480) != 0)
          strZorderArray[index].ZOrder = 30000.0;
        if (this.e.frame[index].ParaFrameId > 0 && this.e.frame[index].BoxFrame == index && (this.e.frame[index].flags & 8) == 0 && this.e.frame[index].empty)
          strZorderArray[index].ZOrder -= 0.1;
      }
      for (int index1 = 0; index1 < this.e.TotalFrames; ++index1)
      {
        for (int index2 = index1 + 1; index2 < this.e.TotalFrames; ++index2)
        {
          int frameId1 = strZorderArray[index1].FrameId;
          int paraFid1 = strZorderArray[index1].ParaFID;
          int frameId2 = strZorderArray[index2].FrameId;
          int paraFid2 = strZorderArray[index2].ParaFID;
          bool flag2;
          if (this.e.frame[frameId1].ParaFrameId > 0 && this.e.frame[frameId1].ParaFrameId == this.e.frame[frameId2].ParaFrameId && this.e.frame[frameId1].BoxFrame == frameId2)
            flag2 = true;
          else if (paraFid2 > 0 && paraFid1 == 0 && (this.e.ParaFrame[paraFid2].flags & 134217728 /*0x08000000*/) != 0)
            flag2 = true;
          else if (paraFid1 > 0 && paraFid2 == 0 && (this.e.ParaFrame[paraFid1].flags & 134217728 /*0x08000000*/) == 0 && this.e.ParaFrame[paraFid1].ZOrder >= 0)
          {
            flag2 = true;
          }
          else
          {
            flag2 = false;
            double num1 = strZorderArray[index2].ZOrder - strZorderArray[index1].ZOrder;
            if (num1 < 0.0)
              flag2 = true;
            else if (num1 == 0.0)
            {
              double num2 = (double) (strZorderArray[index2].level - strZorderArray[index1].level);
              if (num2 < 0.0)
                flag2 = true;
              else if (num2 == 0.0 && (double) (strZorderArray[index2].line - strZorderArray[index1].line) < 0.0 && (strZorderArray[index2].ParaFID > 0 || strZorderArray[index1].ParaFID > 0))
                flag2 = true;
            }
          }
          if (flag2)
          {
            this.SwapDbls(ref strZorderArray[index1].ZOrder, ref strZorderArray[index2].ZOrder);
            this.SwapInts(ref strZorderArray[index1].level, ref strZorderArray[index2].level);
            this.SwapInts(ref strZorderArray[index1].FrameId, ref strZorderArray[index2].FrameId);
            this.SwapInts(ref strZorderArray[index1].line, ref strZorderArray[index2].line);
            this.SwapInts(ref strZorderArray[index1].ParaFID, ref strZorderArray[index2].ParaFID);
          }
        }
      }
      for (int index = 0; index < this.e.TotalFrames; ++index)
        this.e.frame[index].DispFrame = strZorderArray[index].FrameId;
      this.e.FramesSorted = true;
    }
    return true;
  }

  internal bool SwitchParaFrames(
    int PrevParaFrame,
    ref CFrm.StrFrameSet p,
    ref CFrm.StrFrameSet s,
    ref CFrm.StrFrameSet pSavePrt,
    ref CFrm.StrFrameSet pSaveScr,
    ref int pFrameNo,
    int l,
    int PassFlags,
    int PageNo)
  {
    int FrameNo = pFrameNo;
    if (this.e.CurParaFrame > 0)
    {
      if (PrevParaFrame == 0)
      {
        pSavePrt = p;
        pSaveScr = s;
      }
      else
      {
        p = pSavePrt;
        s = pSaveScr;
      }
      this.e.ContainsParaFrames = true;
      ++this.e.PageInfo[PageNo].FrameCount;
      this.e.frame[FrameNo].empty = true;
      this.e.frame[FrameNo].sect = p.sect;
      int num1;
      this.e.ParaFrame[PrevParaFrame].TextLine = num1 = l;
      this.e.frame[FrameNo].PageFirstLine = num1;
      this.e.frame[FrameNo].PageLastLine = l;
      this.e.frame[FrameNo].ParaFrameId = this.e.CurParaFrame;
      this.e.frame[FrameNo].ZOrder = this.e.ParaFrame[this.e.CurParaFrame].ZOrder;
      this.e.frame[FrameNo].flags |= PassFlags;
      this.e.frame[FrameNo].flags1 |= 2;
      this.e.frame[FrameNo].BoxFrame = FrameNo;
      if ((this.e.ParaFrame[this.e.CurParaFrame].flags & 768 /*0x0300*/) != 0)
      {
        this.e.frame[FrameNo].flags |= 8;
        this.e.frame[FrameNo].empty = false;
      }
      if ((this.e.ParaFrame[this.e.CurParaFrame].flags & 896) != 0 && this.e.ParaFrame[this.e.CurParaFrame].FillPattern == 0)
        this.e.frame[FrameNo].flags |= 16 /*0x10*/;
      int num2;
      if ((this.e.ParaFrame[this.e.CurParaFrame].flags & 896) != 0)
      {
        num2 = (this.e.ParaFrame[this.e.CurParaFrame].flags & 1024 /*0x0400*/) == 0 ? 0 : 65776 /*0x0100F0*/;
      }
      else
      {
        int pfmt = this.e.text[this.e.frame[FrameNo].PageLastLine].pfmt;
        num2 = this.e.PfmtId[pfmt].flags;
        this.e.frame[FrameNo].shading = this.e.PfmtId[pfmt].shading / 100;
        this.e.frame[FrameNo].BackColor = this.e.PfmtId[pfmt].BkColor;
      }
      for (int index = 0; index < 4; ++index)
        this.e.frame[FrameNo].BorderWidth[index] = 0;
      if ((num2 & 65776 /*0x0100F0*/) != 0)
      {
        int num3 = 1;
        if ((num2 & 64 /*0x40*/) != 0)
          this.e.frame[FrameNo].border |= 4;
        if ((num2 & 128 /*0x80*/) != 0)
          this.e.frame[FrameNo].border |= 8;
        if ((num2 & 16 /*0x10*/) != 0)
          this.e.frame[FrameNo].border |= 1;
        if ((num2 & 32 /*0x20*/) != 0)
          this.e.frame[FrameNo].border |= 2;
        if ((this.e.ParaFrame[this.e.CurParaFrame].flags & 640) != 0)
        {
          int num4 = this.e.ParaFrame[this.e.CurParaFrame].LineWdth;
          if (num4 < 1)
            num4 = 1;
          this.e.frame[FrameNo].BorderWidth[0] = num4;
          this.e.frame[FrameNo].BorderWidth[1] = num4;
          this.e.frame[FrameNo].BorderWidth[2] = num4;
          this.e.frame[FrameNo].BorderWidth[3] = num4;
        }
        else
        {
          if ((num2 & 768 /*0x0300*/) != 0)
            num3 = 2;
          if (!this.e.ParaFrame[this.e.CurParaFrame].rotatedFrame)
          {
            if ((num2 & 16 /*0x10*/) != 0)
              this.e.frame[FrameNo].BorderWidth[0] = 15 * num3;
            if ((num2 & 32 /*0x20*/) != 0)
              this.e.frame[FrameNo].BorderWidth[1] = 15 * num3;
            if ((num2 & 64 /*0x40*/) != 0)
              this.e.frame[FrameNo].BorderWidth[2] = 15 * num3;
            if ((num2 & 128 /*0x80*/) != 0)
              this.e.frame[FrameNo].BorderWidth[3] = 15 * num3;
          }
        }
      }
      this.e.frame[FrameNo].SpaceTop = this.TwipsToUnitY(this.e.ParaFrame[this.e.CurParaFrame].margin + this.e.frame[FrameNo].BorderWidth[0]);
      this.e.frame[FrameNo].SpaceBot = this.TwipsToUnitY(this.e.ParaFrame[this.e.CurParaFrame].margin + this.e.frame[FrameNo].BorderWidth[1]);
      this.e.frame[FrameNo].height = this.TwipsToUnitY(this.e.ParaFrame[this.e.CurParaFrame].MinHeight);
      this.e.frame[FrameNo].ScrHeight = this.TwipsToScrY(this.e.ParaFrame[this.e.CurParaFrame].MinHeight);
      this.SetParaFrameY(this.e.CurParaFrame, FrameNo, p.y + p.CurColHeight, p.HiddenY, p.HdrMargin, p.HdrHeight, p.TopSect, l);
      this.e.frame[FrameNo].y = this.e.frame[FrameNo].y;
      this.e.frame[FrameNo].ScrY = this.e.frame[FrameNo].ScrY;
      if (this.e.repaginating)
      {
        this.e.ParaFrame[this.e.CurParaFrame].flags |= 32768 /*0x8000*/;
        this.e.ParaFrame[this.e.CurParaFrame].InUse = true;
      }
      this.e.ParaFrame[this.e.CurParaFrame].y = this.UnitToTwipsY(this.e.frame[FrameNo].y - this.e.FirstPageHeight);
      this.e.frame[FrameNo].x = this.TwipsToUnitX(this.e.ParaFrame[this.e.CurParaFrame].x);
      if (this.e.BorderShowing)
        this.e.frame[FrameNo].x += this.e.LeftBorderWidth + p.TopLeftMargin;
      if ((this.e.ParaFrame[this.e.CurParaFrame].flags & 128 /*0x80*/) != 0 && (this.e.ParaFrame[this.e.CurParaFrame].flags & 1073741824 /*0x40000000*/) != 0)
        this.e.frame[FrameNo].x += this.GetObjectColAdj(this.e.CurParaFrame, l, PageNo);
      this.e.frame[FrameNo].width = this.TwipsToUnitX(this.e.ParaFrame[this.e.CurParaFrame].width);
      this.e.frame[FrameNo].SpaceLeft = this.TwipsToUnitX(this.e.ParaFrame[this.e.CurParaFrame].margin + this.e.frame[FrameNo].BorderWidth[2]);
      this.e.frame[FrameNo].SpaceRight = this.TwipsToUnitX(this.e.ParaFrame[this.e.CurParaFrame].margin + this.e.frame[FrameNo].BorderWidth[3]);
      if ((this.e.ParaFrame[this.e.CurParaFrame].flags & 16 /*0x10*/) != 0)
      {
        int x = 0;
        if (this.e.text[l].cid > 0)
        {
          this.e.ParaFrame[this.e.CurParaFrame].width = this.GetRowWidth(this.e.cell[this.e.text[l].cid].row) + 2 * this.e.ParaFrame[this.e.CurParaFrame].margin;
        }
        else
        {
          for (int lin = l; lin < this.e.TotalLines && this.e.text[lin].fid == this.e.text[l].fid; ++lin)
            x += this.GetLineWidth(lin, true, false);
          int pfmt = this.e.text[l].pfmt;
          int num5 = this.UnitToTwipsX(x) + 2 * this.e.ParaFrame[this.e.CurParaFrame].margin + 40 + this.e.PfmtId[pfmt].LeftIndentTwips + this.e.PfmtId[pfmt].FirstIndentTwips + this.e.PfmtId[pfmt].RightIndentTwips;
          if (num5 < this.e.ParaFrame[this.e.CurParaFrame].width)
            this.e.ParaFrame[this.e.CurParaFrame].width = num5;
        }
        this.e.ParaFrame[this.e.CurParaFrame].flags = tc.ResetUintFlag(ref this.e.ParaFrame[this.e.CurParaFrame].flags, 16 /*0x10*/);
        if ((this.e.ParaFrame[this.e.CurParaFrame].flags & 12) != 0)
        {
          this.e.ParaFrame[this.e.CurParaFrame].x = this.UnitToTwipsX(p.ColumnX + p.ColumnWidth) - this.e.ParaFrame[this.e.CurParaFrame].width;
          if ((this.e.ParaFrame[this.e.CurParaFrame].flags & 8) != 0)
            this.e.ParaFrame[this.e.CurParaFrame].x /= 2;
          this.e.frame[FrameNo].x = this.TwipsToUnitX(this.e.ParaFrame[this.e.CurParaFrame].x);
          if (this.e.BorderShowing)
            this.e.frame[FrameNo].x += this.e.LeftBorderWidth + p.TopLeftMargin;
        }
      }
      p.y = this.e.frame[FrameNo].y + this.e.frame[FrameNo].SpaceTop;
      s.y = this.UnitToScrY(p.y);
      int num6;
      p.ColumnX = num6 = this.e.frame[FrameNo].x + this.e.frame[FrameNo].SpaceLeft;
      p.CellX = num6;
      p.ColumnWidth = this.e.frame[FrameNo].width - this.e.frame[FrameNo].SpaceLeft - this.e.frame[FrameNo].SpaceRight;
      p.ColumnSpace = 0;
      int num7;
      s.FrameHt = num7 = 0;
      p.FrameHt = num7;
      int num8;
      s.CurColHeight = num8 = 0;
      p.CurColHeight = num8;
      int num9;
      s.MaxColHeight = num9 = 0;
      p.MaxColHeight = num9;
      ++FrameNo;
      this.InitFrame(FrameNo);
    }
    else
    {
      p = pSavePrt;
      s = pSaveScr;
    }
    pFrameNo = FrameNo;
    return true;
  }

  internal int TerCreateParaFrameId(int x, int y, int width, int height)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    return this.InsertParaFrame(x, y, width, height, false, 0, false);
  }

  internal bool TerGetDrawObjectInfo(
    int FrameId,
    out int width,
    out int height,
    out int LineWdth,
    out Color LineColor,
    out Color BackColor,
    out int flags)
  {
    int num1;
    LineWdth = num1 = 0;
    int num2;
    height = num2 = num1;
    width = num2;
    BackColor = new Color();
    LineColor = BackColor;
    flags = 0;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (FrameId < 0 || FrameId >= this.e.TotalParaFrames || !this.e.ParaFrame[FrameId].InUse)
      return false;
    width = this.e.ParaFrame[FrameId].width;
    height = this.e.ParaFrame[FrameId].height;
    LineWdth = this.e.ParaFrame[FrameId].LineWdth;
    LineColor = this.e.ParaFrame[FrameId].LineColor;
    BackColor = this.e.ParaFrame[FrameId].BackColor;
    flags = this.e.ParaFrame[FrameId].flags;
    return true;
  }

  internal int TerGetFrameParam(int FrameId, int type)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (FrameId >= 0 && FrameId < this.e.TotalParaFrames && this.e.ParaFrame[FrameId].InUse)
    {
      switch (type)
      {
        case 1:
          switch (this.e.AllTextAngle > 0 ? this.e.AllTextAngle : this.e.ParaFrame[FrameId].TextAngle)
          {
            case 90:
              return 2;
            case 270:
              return 1;
            default:
              return 0;
          }
        case 2:
          int flags1 = this.e.ParaFrame[FrameId].flags;
          if ((flags1 & 8192 /*0x2000*/) != 0)
            return 1;
          return (flags1 & 16384 /*0x4000*/) != 0 ? 5 : 2;
        case 3:
          int flags2 = this.e.ParaFrame[FrameId].flags;
          if ((flags2 & 32 /*0x20*/) != 0)
            return 0;
          return (flags2 & 64 /*0x40*/) != 0 ? 1 : 2;
        case 4:
          return this.e.ParaFrame[FrameId].FillPattern;
        case 5:
          return this.e.ParaFrame[FrameId].DistFromText;
      }
    }
    return 9999;
  }

  internal bool TerGetFrameSize(
    int ParaFID,
    out int pX,
    out int pY,
    out int pWidth,
    out int pHeight)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int num1;
    pHeight = num1 = 0;
    int num2;
    pWidth = num2 = num1;
    int num3;
    pY = num3 = num2;
    pX = num3;
    if (!this.e.TerArg.PageMode || ParaFID > 0 && !this.e.ParaFrame[ParaFID].InUse)
      return false;
    if (ParaFID <= 0)
    {
      ParaFID = this.e.text[this.e.CurLine].fid;
      if (ParaFID <= 0)
        return false;
    }
    int x = this.e.ParaFrame[ParaFID].x;
    int y = this.e.ParaFrame[ParaFID].y;
    int num4 = (int) ((double) this.e.TerSect[this.e.TerGetPageSect(this.e.CurPage)].LeftMargin * 1440.0);
    int num5 = x + num4;
    int twipsY = this.TwipsToScrY(y) - this.e.TerWinOrgY + this.e.TerWinRect.top;
    this.e.TerScrToTwipsY(twipsY, out twipsY);
    pX = num5;
    pY = twipsY;
    pWidth = this.e.ParaFrame[ParaFID].width;
    pHeight = this.e.ParaFrame[ParaFID].height;
    return true;
  }

  internal bool TerGetPageBorderDim(out int pWidth, out int pHeight)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    pWidth = this.UnitToTwipsX(this.e.LeftBorderWidth);
    pHeight = this.UnitToTwipsX(this.e.TopBorderHeight);
    return true;
  }

  internal int TerInsertDrawObject(int type, int x, int y, int width, int height)
  {
    int num1 = 0;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.PrintView)
      return 0;
    if (type <= 0)
    {
      if (!this.CallDialogBox((Form) new terdlg_draw_object(this.e)))
        return 0;
      type = this.e.DlgResult;
    }
    int InitFlags = num1 | 16384 /*0x4000*/;
    if (type == 1)
      InitFlags |= 128 /*0x80*/;
    else if (type == 2)
      InitFlags |= 512 /*0x0200*/;
    else if (type == 3)
      InitFlags |= 256 /*0x0100*/;
    int index;
    if ((index = this.InsertParaFrame(x, y, width, height, false, InitFlags, true)) == 0)
      return 0;
    this.e.ParaFrame[index].BackColor = Color.White;
    this.e.ParaFrame[index].LineColor = Color.Black;
    this.e.ParaFrame[index].FillPattern = 1;
    this.e.ParaFrame[index].margin = 40;
    if (type == 2 || type == 3)
      this.e.ParaFrame[index].margin = 0;
    if (type == 3 && y == -1)
    {
      this.e.ParaFrame[index].ParaY = this.UnitToTwipsX(this.e.text[this.e.CurLine].height);
      this.e.TerRepaginate(true);
    }
    this.e.ParaFrame[index].LineWdth = 20;
    this.e.ParaFrame[index].flags |= 1024 /*0x0400*/;
    switch (type)
    {
      case 2:
        this.e.ParaFrame[index].MinHeight = this.e.ParaFrame[index].height;
        break;
      case 3:
        int num2;
        this.e.ParaFrame[index].height = num2 = 0;
        this.e.ParaFrame[index].MinHeight = num2;
        break;
    }
    this.PaintTer();
    return index;
  }

  internal int TerInsertLineObject(int x1, int y1, int x2, int y2)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.PrintView)
      return 0;
    int InitFlags = 16640;
    int width = Math.Abs(x1 - x2);
    int height = Math.Abs(y1 - y2);
    int x = x1 < x2 ? x1 : x2;
    int y = y1 < y2 ? y1 : y2;
    int ParaFID;
    if ((ParaFID = this.InsertParaFrame(x, y, width, height, false, InitFlags, true)) == 0)
      return 0;
    this.e.ParaFrame[ParaFID].BackColor = Color.White;
    this.e.ParaFrame[ParaFID].LineColor = Color.Black;
    this.e.ParaFrame[ParaFID].FillPattern = 1;
    this.e.ParaFrame[ParaFID].margin = 40;
    this.e.ParaFrame[ParaFID].margin = 0;
    this.e.ParaFrame[ParaFID].LineWdth = 20;
    this.e.ParaFrame[ParaFID].flags |= 1024 /*0x0400*/;
    this.LinePointsToRect(ParaFID, x1, y1, x2, y2);
    this.PaintTer();
    return ParaFID;
  }

  internal int TerInsertParaFrame(
    int x,
    int y,
    int width,
    int height,
    bool boxed,
    bool rotatedFrame = false)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    return this.InsertParaFrame(x, y, width, height, boxed, 0, true, rotatedFrame);
  }

  internal bool TerMoveParaFrame(
    int ParaFID,
    int FrameX,
    int FrameY,
    int FrmWidth,
    int FrmHeight)
  {
    return this.e.TerMoveParaFrame2(ParaFID, FrameX, FrameY, FrmWidth, FrmHeight, -1);
  }

  internal bool TerMoveParaFrame2(
    int ParaFID,
    int FrameX,
    int FrameY,
    int FrmWidth,
    int FrmHeight,
    int page)
  {
    int num1 = 0;
    Point point = new Point();
    bool flag1 = true;
    bool flag2 = false;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.PageMode || !this.e.ParaFrame[ParaFID].InUse)
      return false;
    if (this.e.BorderShowing)
      FrameY += this.UnitToTwipsY(this.e.TopBorderHeight);
    int scrX = this.TwipsToScrX(FrameX);
    int scrY = this.TwipsToScrY(FrameY);
    int index1 = 0;
    while (index1 < this.e.TotalFrames && (this.e.frame[index1].empty || this.e.frame[index1].ParaFrameId != ParaFID))
      ++index1;
    if (index1 < this.e.TotalFrames)
    {
      this.e.frame[index1].empty = true;
      int pageFirstLine = this.e.frame[index1].PageFirstLine;
      if (page < 0)
        page = this.e.text[pageFirstLine].page;
      if (page == this.e.FirstFramePage + 1)
        scrY += this.e.FirstPageHeight;
    }
    int line = this.UnitsToLine(scrX, scrY);
    this.SetPageFromY(scrY);
    if (index1 < this.e.TotalFrames)
      this.e.frame[index1].empty = false;
    if ((this.e.ParaFrame[ParaFID].flags & 96 /*0x60*/) != 0)
    {
      int LineNo = 0;
      while (LineNo < this.e.TotalLines && this.e.text[LineNo].fid != ParaFID)
        ++LineNo;
      if (LineNo < this.e.TotalLines && ((this.e.PfmtId[this.e.text[LineNo].pfmt].flags & 12288 /*0x3000*/) != 0 || this.PageFromLine(LineNo, -1) == this.e.CurPage))
        flag1 = false;
      num1 = LineNo;
    }
    int paraFrameLine = this.GetParaFrameLine(line);
    int anchorY = this.GetAnchorY(paraFrameLine);
    this.RecreateSections();
    int num2;
    if ((this.e.ParaFrame[ParaFID].flags & 96 /*0x60*/) != 0)
    {
      int section = this.GetSection(flag1 ? paraFrameLine : num1);
      int twips = (int) this.InchesToTwips((double) this.e.TerSect[section].TopMargin);
      point.Y = this.UnitToTwipsY(this.e.TerSect1[section].HiddenY);
      num2 = this.e.CurPage <= this.e.FirstFramePage ? FrameY : FrameY - this.ScrToTwipsY(this.e.FirstPageHeight);
      if ((this.e.ParaFrame[ParaFID].flags & 32 /*0x20*/) != 0)
      {
        if (this.e.BorderShowing)
          num2 -= this.UnitToTwipsY(this.e.TopBorderHeight);
        else if (this.e.ViewPageHdrFtr)
          num2 += point.Y;
        else
          num2 += twips;
      }
      else if (this.e.BorderShowing)
        num2 -= this.UnitToTwipsY(this.e.TopBorderHeight) + twips;
      else if (this.e.ViewPageHdrFtr)
        num2 -= twips - point.Y;
    }
    else
      num2 = this.ScrToTwipsY(scrY - anchorY);
    int index2 = this.e.CurLine - 1;
    while (index2 >= 0 && this.e.text[index2].fid == ParaFID)
      --index2;
    int StartLine = index2 + 1;
    int index3 = this.e.CurLine + 1;
    while (index3 < this.e.TotalLines && this.e.text[index3].fid == ParaFID)
      ++index3;
    int num3 = index3 - 1;
    if (!flag1 || paraFrameLine >= StartLine && paraFrameLine <= num3 + 1)
    {
      this.SaveUndo(ParaFID, 0, 0, 0, '2');
    }
    else
    {
      if (this.e.TotalLines <= 5000)
        this.SaveUndo(ParaFID, 0, 0, 0, '1');
      else
        flag2 = this.ReleaseUndo();
      int HdrFtr = this.e.PfmtId[this.e.text[paraFrameLine].pfmt].flags & 12288 /*0x3000*/;
      int count = num3 - StartLine + 1;
      if (!this.CheckLineLimit(this.e.TotalLines + count))
        return true;
      this.MoveLineArrays(paraFrameLine, count, 'B');
      if (StartLine > paraFrameLine)
      {
        StartLine += count;
        int num4 = num3 + count;
      }
      for (int index4 = 0; index4 < count; ++index4)
      {
        this.FreeLine(paraFrameLine + index4);
        this.e.text[paraFrameLine + index4] = this.e.text[StartLine + index4];
        this.e.text[StartLine + index4] = (tc.ClsLinePtr) null;
        this.e.text[paraFrameLine + index4].pfmt = this.ParaIdForFrame(this.e.text[paraFrameLine + index4].pfmt, HdrFtr);
      }
      this.MoveLineArrays(StartLine, count, 'D');
      if (StartLine < paraFrameLine)
        paraFrameLine -= count;
      this.e.CurLine = paraFrameLine;
      this.e.CurCol = 0;
    }
    this.e.ParaFrame[ParaFID].x = FrameX;
    this.e.ParaFrame[ParaFID].y = FrameY;
    this.e.ParaFrame[ParaFID].ParaY = num2;
    if (FrmWidth > 0)
      this.e.ParaFrame[ParaFID].width = FrmWidth;
    if (FrmHeight >= 0)
      this.e.ParaFrame[ParaFID].MinHeight = FrmHeight;
    if ((this.e.ParaFrame[ParaFID].flags & 896) != 0)
      this.e.ParaFrame[ParaFID].height = this.e.ParaFrame[ParaFID].MinHeight;
    if (this.e.ParaFrame[ParaFID].ShapeType == 75 && (FrmWidth > 0 || FrmHeight > 0))
    {
      int pict = this.e.ParaFrame[ParaFID].pict;
      if (pict > 0)
      {
        if (FrmWidth > 0)
          this.e.TerFont[pict].PictWidth = FrmWidth;
        if (FrmHeight > 0)
          this.e.TerFont[pict].PictHeight = FrmHeight;
        this.SetPictSize(pict, this.TwipsToScrY(this.e.TerFont[pict].PictHeight), this.TwipsToScrX(this.e.TerFont[pict].PictWidth), false);
        this.XlateSizeForPrt(pict);
      }
    }
    if (this.True(this.e.BkPictId))
      this.DeleteTextMap(true);
    if (flag2)
      this.ReleaseUndo();
    this.Repaginate(false, true, 0, true);
    return true;
  }

  internal bool TerMovePictFrame(int pict, int FrameX, int FrameY)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.PageMode || pict < 0 || pict >= this.e.TotalFonts || !this.e.TerFont[pict].InUse || (this.e.TerFont[pict].style & 128 /*0x80*/) == 0 || this.e.TerFont[pict].FrameType == 0)
      return false;
    int paraFid = this.e.TerFont[pict].ParaFID;
    if (!this.e.ParaFrame[paraFid].InUse)
      return false;
    this.SaveUndo(paraFid, 0, 0, 0, '2');
    if (FrameX != -31234)
    {
      int num = FrameX - this.e.ParaFrame[paraFid].x;
      this.e.ParaFrame[paraFid].x += num;
      this.e.ParaFrame[paraFid].OrgX += num;
    }
    if (FrameY != -31234)
      this.e.ParaFrame[paraFid].ParaY = FrameY;
    ++this.e.TerArg.modified;
    this.Repaginate(false, true, 0, true);
    return true;
  }

  internal bool TerMovePictFrame2(int pict, int DeltaX, int DeltaY)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.PageMode || pict < 0 || pict >= this.e.TotalFonts || !this.e.TerFont[pict].InUse || (this.e.TerFont[pict].style & 128 /*0x80*/) == 0 || this.e.TerFont[pict].FrameType == 0)
      return false;
    int paraFid = this.e.TerFont[pict].ParaFID;
    if (!this.e.ParaFrame[paraFid].InUse)
      return false;
    int num1 = this.TwipsToScrY(this.e.ParaFrame[paraFid].y + DeltaY);
    if (this.e.ParaFrame[paraFid].PageNo > this.e.FirstFramePage)
      num1 += this.e.FirstPageHeight;
    int num2 = num1 + this.TwipsToScrY(this.e.ParaFrame[paraFid].height);
    int num3 = 0;
    int num4 = this.e.FirstPageHeight;
    if (this.e.ParaFrame[paraFid].PageNo > this.e.FirstFramePage)
    {
      num3 = num4;
      num4 = this.e.CurPageHeight;
    }
    bool flag = num1 >= num3 && num2 <= num4;
    if (!flag && num1 >= num3 && this.e.ParaFrame[paraFid].PageNo + 1 == this.e.TotalPages)
      flag = true;
    if (flag)
      return this.TerMovePictFrame(pict, this.e.ParaFrame[paraFid].x + DeltaX, this.e.ParaFrame[paraFid].ParaY + DeltaY);
    if (this.e.TotalLines <= 5000)
      this.SaveUndo(paraFid, 0, 0, 0, '1');
    else
      this.ReleaseUndo();
    this.e.ParaFrame[paraFid].x += DeltaX;
    this.e.ParaFrame[paraFid].OrgX += DeltaX;
    int scrX = this.TwipsToScrX(this.e.ParaFrame[paraFid].x);
    int index = 0;
    while (index < this.e.TotalFrames && (this.e.frame[index].empty || this.e.frame[index].ParaFrameId != paraFid))
      ++index;
    if (index < this.e.TotalFrames)
      this.e.frame[index].empty = true;
    int line = this.UnitsToLine(scrX, num1);
    this.SetPageFromY(num1);
    if (index < this.e.TotalFrames)
      this.e.frame[index].empty = false;
    int paraFrameLine = this.GetParaFrameLine(line);
    int anchorY = this.GetAnchorY(paraFrameLine);
    this.AnchorPictFrame(pict, paraFrameLine, 0);
    if (this.e.CurPage == 0 && num1 < 0)
      num1 = 0;
    int num5 = this.FrameToPageY(this.ScrToTwipsY(num1));
    if (num5 - this.e.ParaFrame[paraFid].height < 0)
      num5 = this.e.ParaFrame[paraFid].height;
    if ((this.e.ParaFrame[paraFid].flags & 96 /*0x60*/) != 0)
    {
      this.e.ParaFrame[paraFid].ParaY = num5;
      if ((this.e.ParaFrame[paraFid].flags & 64 /*0x40*/) != 0)
      {
        int section = this.GetSection(paraFrameLine);
        this.e.ParaFrame[paraFid].ParaY -= (int) this.InchesToTwips((double) this.e.TerSect[section].TopMargin);
      }
    }
    else
      this.e.ParaFrame[paraFid].ParaY = num5 - this.ScrToTwipsY(anchorY);
    this.e.ParaFrame[paraFid].PageNo = this.e.text[paraFrameLine].page;
    ++this.e.TerArg.modified;
    this.e.CurLine = paraFrameLine;
    this.e.CurCol = 0;
    this.e.TerRepaginate(true);
    return true;
  }

  internal bool TerPosFrame(int FrameNo, int pos, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.e.TerArg.PageMode && (pos == 0 || pos == 1) && FrameNo >= 1 && FrameNo < this.e.TotalParaFrames)
    {
      int NewLine1 = 0;
      while (NewLine1 < this.e.TotalLines && this.e.text[NewLine1].fid != FrameNo)
        ++NewLine1;
      if (NewLine1 == this.e.TotalLines)
        return false;
      if (pos == 0)
      {
        this.e.SetTerCursorPos(NewLine1, 0, repaint);
        return true;
      }
      while (NewLine1 < this.e.TotalLines && this.e.text[NewLine1].fid == FrameNo)
        ++NewLine1;
      int NewLine2 = NewLine1 - 1;
      if (NewLine2 >= 0)
      {
        int NewCol = this.e.text[NewLine2].len - 1;
        if (NewCol < 0)
          NewCol = 0;
        this.e.SetTerCursorPos(NewLine2, NewCol, repaint);
        return true;
      }
    }
    return false;
  }

  internal bool TerRotateFrameText(bool dialog, int LineNo, int direction, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (LineNo >= this.e.TotalLines)
      return false;
    int index;
    if (LineNo >= 0)
    {
      if (this.True(this.e.text[LineNo].cid))
        return false;
      index = this.e.text[LineNo].fid;
    }
    else
      index = -LineNo;
    if (index < 0 || index >= this.e.TotalParaFrames || (this.e.ParaFrame[index].flags & 768 /*0x0300*/) != 0 || this.e.ParaFrame[index].pict > 0)
      return false;
    int num;
    if (dialog)
    {
      this.e.DlgInt1 = this.e.ParaFrame[index].TextAngle;
      if (!this.CallDialogBox((Form) new terdlg_text_rotation(this.e)))
        return false;
      num = this.e.DlgInt1;
    }
    else
    {
      num = 0;
      switch (direction)
      {
        case 1:
          num = 270;
          break;
        case 2:
          num = 90;
          break;
      }
    }
    this.e.ParaFrame[index].TextAngle = num;
    this.e.ParaFrame[index].MinHeight = this.e.ParaFrame[index].height;
    ++this.e.TerArg.modified;
    if (this.e.CurLine < this.e.RepageBeginLine)
      this.e.RepageBeginLine = this.e.CurLine;
    if (repaint)
      this.PaintTer();
    return true;
  }

  internal bool TerSetFrameMarginDist(int dist)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    this.e.FrameDistFromMargin = dist;
    return true;
  }

  internal bool TerSetFrameTextDist(int ParaFID, int dist)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (ParaFID <= 0)
      ParaFID = this.e.text[this.e.CurLine].fid;
    if (ParaFID <= 0 || ParaFID >= this.e.TotalParaFrames || !this.e.ParaFrame[ParaFID].InUse)
      return false;
    this.e.ParaFrame[ParaFID].DistFromText = dist;
    this.RequestPagination(true);
    return true;
  }

  internal bool TerSetFrameYBase(int FrameId, int yBase)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.PageMode)
      return false;
    int num1;
    if (FrameId < 0)
    {
      FrameId = this.e.text[this.e.CurLine].fid;
      if (FrameId <= 0)
      {
        int curCfmt = this.GetCurCfmt(this.e.CurLine, this.e.CurCol);
        if ((this.e.TerFont[curCfmt].style & 128 /*0x80*/) == 0 || this.e.TerFont[curCfmt].FrameType == 0)
          return false;
        FrameId = this.e.TerFont[curCfmt].ParaFID;
      }
      this.e.DlgInt1 = FrameId;
      if (!this.CallDialogBox((Form) new terdlg_object_pos(this.e)))
        return false;
      yBase = this.e.DlgInt2;
      num1 = this.e.CurLine;
    }
    else
    {
      if (FrameId <= 0 || FrameId >= this.e.TotalParaFrames || this.False(this.e.ParaFrame[FrameId].InUse))
        return false;
      int index = 0;
      while (index < this.e.TotalLines && this.e.text[index].fid != FrameId)
        ++index;
      if (index == this.e.TotalLines)
        return false;
      num1 = index;
    }
    if (yBase != 0 && yBase != 1 && yBase != 2)
      return false;
    int flags = this.e.ParaFrame[FrameId].flags;
    if (((flags & 32 /*0x20*/) == 0 || yBase != 0) && ((flags & 64 /*0x40*/) == 0 || yBase != 1) && ((flags & 96 /*0x60*/) != 0 || yBase != 2))
    {
      ++this.e.TerArg.modified;
      int section = this.GetSection(num1);
      int PageNo = this.PageFromLine(num1, -1);
      int hiddenY = this.e.TerSect1[section].HiddenY;
      if (yBase == 0 || yBase == 1)
      {
        int num2 = !this.e.BorderShowing ? (!this.e.ViewPageHdrFtr ? this.e.ParaFrame[FrameId].y + (int) this.InchesToTwips((double) this.e.TerSect[section].HdrMargin) + this.UnitToTwipsY(this.PageHdrHeight2(PageNo, false, true)) : this.e.ParaFrame[FrameId].y + this.UnitToTwipsY(hiddenY)) : this.e.ParaFrame[FrameId].y - this.UnitToTwipsY(this.e.TopBorderHeight);
        if (yBase == 0)
          this.e.ParaFrame[FrameId].ParaY = num2;
        else
          this.e.ParaFrame[FrameId].ParaY = num2 - (int) this.InchesToTwips((double) this.e.TerSect[section].TopMargin);
      }
      else
      {
        int anchorY = this.GetAnchorY(-FrameId);
        int frame;
        if ((frame = this.GetFrame(num1)) < 0)
          return false;
        this.e.ParaFrame[FrameId].ParaY = this.ScrToTwipsY(this.e.frame[frame].y - anchorY);
      }
      tc.ResetUintFlag(ref this.e.ParaFrame[FrameId].flags, 96 /*0x60*/);
      switch (yBase)
      {
        case 0:
          this.e.ParaFrame[FrameId].flags |= 32 /*0x20*/;
          break;
        case 1:
          this.e.ParaFrame[FrameId].flags |= 64 /*0x40*/;
          break;
      }
      this.Repaginate(false, true, 0, true);
    }
    return true;
  }

  internal bool TerSetNewFrameDim(int x, int y, int width, int height, bool PageTop)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    this.e.NewFrameX = x;
    this.e.NewFrameY = y;
    if (width > 0)
      this.e.NewFrameWidth = width;
    if (height > 0)
      this.e.NewFrameHeight = height;
    this.e.NewFrameVPage = PageTop;
    return true;
  }

  internal bool TerSetObjectAttrib(
    int FrameId,
    int LineType,
    int LineThickness,
    Color LineColor,
    bool FillSolid,
    Color FillColor)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    return this.TerSetObjectAttribEx(FrameId, LineType, LineThickness, LineColor, FillSolid, FillColor, -9999);
  }

  internal bool TerSetObjectAttribEx(
    int FrameId,
    int LineType,
    int LineThickness,
    Color LineColor,
    bool FillSolid,
    Color FillColor,
    int ZOrder)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.PageMode)
      return false;
    int num;
    if (FrameId < 0)
    {
      FrameId = this.e.text[this.e.CurLine].fid;
      if (FrameId <= 0)
      {
        int curCfmt = this.GetCurCfmt(this.e.CurLine, this.e.CurCol);
        if ((this.e.TerFont[curCfmt].style & 128 /*0x80*/) == 0 || this.e.TerFont[curCfmt].FrameType == 0)
          return false;
        FrameId = this.e.TerFont[curCfmt].ParaFID;
      }
      if ((this.e.ParaFrame[FrameId].flags & 896) == 0)
        return false;
      this.e.DlgInt1 = FrameId;
      if (!this.CallDialogBox((Form) new terdlg_object_attrib(this.e)))
        return false;
      LineThickness = this.e.DlgInt1;
      LineType = this.e.DlgInt2;
      LineColor = this.e.DlgColor1;
      FillSolid = this.e.DlgBool1;
      FillColor = this.e.DlgColor2;
      ZOrder = this.e.DlgInt4;
      num = this.e.DlgInt5;
    }
    else
    {
      if (FrameId <= 0 || FrameId >= this.e.TotalParaFrames || this.False(this.e.ParaFrame[FrameId].InUse) || (this.e.ParaFrame[FrameId].flags & 896) == 0)
        return false;
      num = this.e.ParaFrame[FrameId].flags & 24576 /*0x6000*/;
    }
    if (LineThickness < 0 || LineType != 0 && LineType != 1 && LineType != 2)
      return false;
    this.e.ParaFrame[FrameId].LineWdth = LineThickness;
    this.e.ParaFrame[FrameId].flags = tc.ResetUintFlag(ref this.e.ParaFrame[FrameId].flags, 3072 /*0x0C00*/);
    switch (LineType)
    {
      case 1:
        this.e.ParaFrame[FrameId].flags |= 1024 /*0x0400*/;
        break;
      case 2:
        this.e.ParaFrame[FrameId].flags |= 3072 /*0x0C00*/;
        break;
    }
    this.e.ParaFrame[FrameId].LineColor = LineColor;
    if (FillSolid)
      this.e.ParaFrame[FrameId].FillPattern = 1;
    else
      this.e.ParaFrame[FrameId].FillPattern = 0;
    this.e.ParaFrame[FrameId].BackColor = FillColor;
    if (ZOrder != -9999)
      this.e.ParaFrame[FrameId].ZOrder = ZOrder;
    if ((this.e.ParaFrame[FrameId].flags & 256 /*0x0100*/) == 0)
    {
      if ((this.e.ParaFrame[FrameId].flags & 24576 /*0x6000*/) != num)
        this.RequestPagination(true);
      tc.ResetUintFlag(ref this.e.ParaFrame[FrameId].flags, 24576 /*0x6000*/);
      this.e.ParaFrame[FrameId].flags |= num;
    }
    this.e.PaintFlag = 6;
    this.PaintTer();
    return true;
  }

  internal bool TerSetObjectWrapStyle(int FrameId, int WrapStyle)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.PageMode || WrapStyle != 1 && WrapStyle != 2 && WrapStyle != 5)
      return false;
    if (FrameId < 0)
      FrameId = this.e.text[this.e.CurLine].fid;
    if (FrameId == 0 || FrameId >= this.e.TotalParaFrames || !this.e.ParaFrame[FrameId].InUse || (this.e.ParaFrame[FrameId].flags & 896) == 0 && this.e.ParaFrame[FrameId].pict == 0 || (this.e.ParaFrame[FrameId].flags & 256 /*0x0100*/) != 0)
      return false;
    int num = 0;
    if (WrapStyle == 1)
      num = 8192 /*0x2000*/;
    if (WrapStyle == 5)
      num = 16384 /*0x4000*/;
    if ((this.e.ParaFrame[FrameId].flags & 24576 /*0x6000*/) != num)
      this.RequestPagination(true);
    tc.ResetUintFlag(ref this.e.ParaFrame[FrameId].flags, 24576 /*0x6000*/);
    this.e.ParaFrame[FrameId].flags |= num;
    this.e.PaintFlag = 6;
    this.PaintTer();
    return true;
  }

  internal struct StrFrameSet
  {
    internal int y;
    internal int CurColHeight;
    internal int MaxColHeight;
    internal int ColumnX;
    internal int ColumnWidth;
    internal int ColumnSpace;
    internal int CellX;
    internal int FrameHt;
    internal int TopSect;
    internal int sect;
    internal int TopLeftMargin;
    internal int HiddenY;
    internal int HdrMargin;
    internal int HdrHeight;
    internal int FtrMargin;
    internal int FtrHeight;
  }

  internal struct StrZOrder
  {
    internal double ZOrder;
    internal int level;
    internal int FrameId;
    internal int line;
    internal int ParaFID;
  }
}
