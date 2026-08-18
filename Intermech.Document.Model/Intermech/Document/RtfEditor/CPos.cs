// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.CPos
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class CPos : COp
{
  internal CPos(ImRtfEditor ImRtfEditor)
    : base(ImRtfEditor)
  {
    this.e = ImRtfEditor;
  }

  internal new void AbsToRowCol(int abs, char dest)
  {
    int row;
    int col;
    this.AbsToRowCol(abs, out row, out col, true, false);
    switch (dest)
    {
      case 'B':
        this.e.HilightBegRow = row;
        this.e.HilightBegCol = col;
        break;
      case 'C':
        this.e.CurLine = row;
        this.e.CurCol = col;
        break;
      case 'E':
        this.e.HilightEndRow = row;
        this.e.HilightEndCol = col;
        break;
      default:
        int num = (int) this.ShowMessage(this.e.MsgString[200], nameof (AbsToRowCol), MessageBoxButtons.OK);
        break;
    }
  }

  /// <summary>Преобразовать абсолютную (сквозную) позицию в строке текста в строку и столбец текста</summary>
  /// <param name="abs">Абсолютная позиция в строке текста</param>
  /// <param name="row">Строка</param>
  /// <param name="col">Столбец</param>
  /// <param name="internalPos">Преобразовать в позицию внутреннего представления строки с учётом замен.
  /// Иначе в позицию в оригинальной строке до замен формул и спецсимволов</param>
  /// <param name="scanAllChars">Принудительно проверять весь текст,
  /// для поиска переносов строки, которые находятся не в конце строки</param>
  internal new void AbsToRowCol(
    int abs,
    out int row,
    out int col,
    bool internalPos = true,
    bool scanAllChars = false)
  {
    int num1 = 0;
    bool flag1 = (this.e.TerFlags4 & 1) != 0;
    if (!this.e.TerArg.WordWrap)
      num1 = 2;
    int num2;
    col = num2 = 0;
    row = num2;
    while (abs >= 0)
    {
      int len = this.e.text[row].len;
      if ((this.e.text[row].flags2 & 512 /*0x0200*/) != 0 && len > 0 && this.e.text[row].txt[len - 1] == '\u0006')
        --len;
      int num3 = 0;
      int num4 = 0;
      bool flag2 = false;
      if (!internalPos && this.e.text[row].tag != null && this.e.text[row].tag.Length != 0)
      {
        for (int index1 = 0; index1 < this.e.text[row].len; ++index1)
        {
          int index2 = (int) this.e.text[row].tag[index1];
          if (index2 != 0 && (this.e.CharTag[index2].type == 78 || this.e.CharTag[index2].type == 79 || this.e.CharTag[index2].type == 80 /*0x50*/))
          {
            flag2 = index1 == this.e.text[row].len - 1;
            string auxText = this.e.CharTag[index2].AuxText;
            if (auxText != null)
              num3 += auxText.Length - 1;
            else
              --num3;
            if (flag1 && this.e.text[row].txt[index1] == '\u0015')
              --num3;
          }
          else if (flag1 && index1 < this.e.text[row].len - 1 && this.e.text[row].txt[index1] == '\u0015')
            ++num4;
        }
      }
      else if (scanAllChars & flag1)
      {
        for (int index = 0; index < this.e.text[row].len - 1; ++index)
        {
          if (this.e.text[row].txt[index] == '\u0015')
            ++num4;
        }
      }
      int num5 = len + num1 + num3 + num4;
      if (flag1 && !flag2 && (len > 0 && this.e.text[row].txt[len - 1] == '\u0015' || (this.e.text[row].flags & 1966209) != 0 || this.e.text[row].len == 1 && this.LineInfo(row, 32 /*0x20*/)))
        ++num5;
      if (abs >= num5)
      {
        abs -= num5;
        if (row >= this.e.TotalLines - 1)
        {
          col = this.e.text[this.e.TotalLines - 1].len - 1 + num1;
          break;
        }
        ++row;
      }
      else
      {
        col = abs;
        break;
      }
    }
    if (col >= 0)
      return;
    col = 0;
  }

  internal new void AdjustHiddenPos()
  {
    int num1 = 0;
    bool flag1 = false;
    if ((this.e.TerFlags2 & 268435456 /*0x10000000*/) != 0 || (this.e.TerOpFlags & 65536 /*0x010000*/) != 0 || !this.e.CaretEngaged)
      return;
    if (this.e.TerArg.PageMode && (this.e.TerFlags3 & 32 /*0x20*/) != 0 && this.e.CursorCell > 0 && (this.e.text[this.e.CurLine].cid != this.e.CursorCell || this.LineInfo(this.e.CurLine, 32 /*0x20*/)))
    {
      int LineNo = 0;
      while (LineNo < this.e.TotalLines && this.e.text[LineNo].cid != this.e.CursorCell)
        ++LineNo;
      if (LineNo == this.e.TotalLines)
      {
        this.e.CursorCell = 0;
        return;
      }
      if (this.e.CurLine < LineNo)
      {
        this.e.CurLine = LineNo;
        this.e.CurCol = 0;
        this.e.CursDirection = 1;
      }
      else
      {
        while (LineNo < this.e.TotalLines && this.e.text[LineNo].cid == this.e.CursorCell && !this.LineInfo(LineNo, 32 /*0x20*/))
          ++LineNo;
        int num2 = LineNo - 1;
        if (this.e.CurLine > num2)
        {
          this.e.CurLine = num2;
          this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
          this.e.CursDirection = 2;
        }
      }
    }
    if (this.e.TerArg.PageMode && !this.e.ViewPageHdrFtr)
      flag1 = true;
    if ((!this.e.TerArg.PageMode || this.e.text[this.e.CurLine].tabw == null || (this.e.text[this.e.CurLine].tabw.type & 32 /*0x20*/) == 0) && (this.e.text[this.e.CurLine].flags & 1966080 /*0x1E0000*/) == 0 && (!flag1 || (this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].flags & 12288 /*0x3000*/) == 0) && !this.MoveCursor(this.e.CurLine, this.e.CurCol))
      return;
    bool flag2 = this.GetCursDirection();
    int curLine1;
    while (true)
    {
      curLine1 = this.e.CurLine;
      if (!flag2)
      {
        if (this.e.TerArg.PageMode && this.e.ViewPageHdrFtr && !this.e.EditPageHdrFtr)
        {
          if ((this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].flags & 8192 /*0x2000*/) != 0)
            this.e.CurLine = this.e.PageInfo[this.e.CurPage].LastLine;
          if ((this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].flags & 4096 /*0x1000*/) != 0 && this.e.CurPage > 0)
            this.e.CurLine = this.e.PageInfo[this.e.CurPage - 1].LastLine;
        }
        int curLine2;
        for (curLine2 = this.e.CurLine; curLine2 >= 0; --curLine2)
        {
          int len = this.e.text[curLine2].len;
          if (len != 0 && (this.e.text[curLine2].flags & 1966080 /*0x1E0000*/) == 0 && (this.e.text[curLine2].tabw == null || (this.e.text[curLine2].tabw.type & 32 /*0x20*/) == 0) && (!flag1 || (this.e.PfmtId[this.e.text[curLine2].pfmt].flags & 12288 /*0x3000*/) == 0))
          {
            this.OpenCfmt(curLine2);
            if ((this.e.CommandId == 602 || this.e.CommandId == 600 || this.e.CursDirection == 3) && curLine2 < this.e.PrevCursLine)
            {
              int col1 = this.UnitsToCol(this.e.CursHorzPos, curLine2);
              int col2 = col1;
              while (col2 < len && this.MoveCursor(curLine2, col2))
                ++col2;
              if (col2 >= len)
              {
                col2 = col1;
                while (col2 >= 0 && this.MoveCursor(curLine2, col2))
                  --col2;
              }
              if (col2 >= 0)
              {
                this.e.CurLine = curLine2;
                this.e.CurCol = col2;
                this.CloseCfmt(curLine2);
                goto label_78;
              }
            }
            else
            {
              for (int col = curLine2 != this.e.CurLine ? len - 1 : this.e.CurCol - 1; col >= 0; --col)
              {
                if (!this.MoveCursor(curLine2, col))
                {
                  this.e.CurLine = curLine2;
                  this.e.CurCol = col;
                  this.CloseCfmt(curLine2);
                  goto label_78;
                }
              }
              this.CloseCfmt(curLine2);
            }
          }
        }
        ++num1;
        if (curLine2 < 0 || curLine2 == this.e.TotalLines)
        {
          if (num1 >= 2)
          {
            if (flag1)
            {
              this.e.ViewPageHdrFtr = true;
              flag1 = false;
            }
            else
              goto label_17;
          }
          flag2 = !flag2;
        }
        else
          goto label_78;
      }
      else
        break;
    }
    bool flag3 = false;
    if (this.e.TerArg.PageMode && this.e.ViewPageHdrFtr && !this.e.EditPageHdrFtr)
    {
      if ((this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].flags & 4096 /*0x1000*/) != 0)
      {
        this.e.CurLine = this.e.PageInfo[this.e.CurPage].FirstLine;
        while (this.e.CurLine + 1 < this.e.TotalLines && (this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].flags & 12288 /*0x3000*/) != 0)
          ++this.e.CurLine;
        this.e.CurCol = 0;
      }
      if ((this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].flags & 8192 /*0x2000*/) != 0 && this.e.CurPage + 1 < this.e.TotalPages)
      {
        this.e.CurLine = this.e.PageInfo[this.e.CurPage + 1].FirstLine;
        flag3 = true;
      }
    }
    for (int curLine3 = this.e.CurLine; curLine3 < this.e.TotalLines; ++curLine3)
    {
      int len = this.e.text[curLine3].len;
      if (len != 0 && (this.e.text[curLine3].flags & 1966080 /*0x1E0000*/) == 0 && (this.e.text[curLine3].tabw == null || (this.e.text[curLine3].tabw.type & 32 /*0x20*/) == 0) && (!flag1 || (this.e.PfmtId[this.e.text[curLine3].pfmt].flags & 12288 /*0x3000*/) == 0))
      {
        this.OpenCfmt(curLine3);
        int num3 = curLine3 != this.e.CurLine ? 0 : this.e.CurCol + 1;
        if (flag3)
          num3 = 0;
        for (int col3 = num3; col3 < len; ++col3)
        {
          if (!this.MoveCursor(curLine3, col3))
          {
            if ((this.e.CommandId == 603 || this.e.CommandId == 601 || this.e.CursDirection == 4) && curLine3 > this.e.PrevCursLine && this.e.TerArg.PageMode && this.e.text[curLine3].x != this.e.text[this.e.CurLine].x)
            {
              int col4 = this.UnitsToCol(this.e.CursHorzPos, curLine3);
              if (col4 >= col3)
              {
                col3 = col4;
                if (this.MoveCursor(curLine3, col3))
                  continue;
              }
              else
                continue;
            }
            this.e.CurLine = curLine3;
            this.e.CurCol = col3;
            if (this.e.CommandId == 611 && this.e.text[this.e.CurLine].len > 0)
              this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
            this.CloseCfmt(curLine3);
            goto label_78;
          }
        }
        this.CloseCfmt(curLine3);
      }
    }
    this.e.CurLine = this.e.TotalLines - 1;
    this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
    if (this.e.CurCol < 0)
    {
      this.e.CurCol = 0;
      goto label_78;
    }
    goto label_78;
label_17:
    return;
label_78:
    if (curLine1 != this.e.BeginLine)
      return;
    this.e.BeginLine = this.e.CurLine;
    this.e.CurRow = 0;
  }

  internal new int ColToUnits(int col, int LineNo, int CursPos)
  {
    int font = 0;
    ushort[] numArray = (ushort[]) null;
    if (LineNo >= this.e.TotalLines)
      return 0;
    ushort[] lineCharWidth = this.GetLineCharWidth(LineNo);
    if (lineCharWidth == null)
      return 0;
    if (this.e.text[LineNo].fmt != null)
      numArray = this.e.text[LineNo].fmt;
    int len = this.e.text[LineNo].len;
    if (this.e.text[LineNo].fmt != null)
      numArray = this.e.text[LineNo].fmt;
    int rowX = this.GetRowX(LineNo);
    for (int index = 0; index <= col; ++index)
    {
      int num1;
      int num2 = num1 = 0;
      if (this.e.text[LineNo].tabw != null && (this.e.text[LineNo].tabw.type & 1024 /*0x0400*/) != 0 && index == this.e.text[LineNo].tabw.FrameCharPos)
        rowX += this.e.text[LineNo].tabw.FrameScrWidth;
      if (this.e.text[LineNo].fmt == null)
        font = (int) this.e.text[LineNo].UniFmt;
      else if (index < len)
        font = (int) numArray[index];
      int num3;
      if (index < len)
      {
        num3 = (int) lineCharWidth[index];
        if (this.e.text[LineNo].tabw != null && this.e.text[LineNo].tabw.CharFlags != null && index < this.e.text[LineNo].tabw.CharFlagsLen)
        {
          if (((int) this.e.text[LineNo].tabw.CharFlags[index] & 1) != 0)
            num2 = this.e.ExtraSpaceScrX;
          if (((int) this.e.text[LineNo].tabw.CharFlags[index] & 2) != 0)
            num1 = this.e.ExtraSpaceScrX;
        }
      }
      else
        num3 = this.fnt.LwrCharWidth(font, true, ' ');
      if (index == col)
      {
        if (CursPos == 1)
          rowX += num2 + (num3 - num2 - num1) / 2;
        if (CursPos == 2)
          rowX += num2 + (num3 - num2 - num1);
        else
          rowX += num2;
      }
      else
        rowX += num3;
    }
    if (this.e.TerArg.PageMode)
    {
      int frame = this.frm.GetFrame(LineNo);
      if (frame >= 0)
        rowX += this.e.frame[frame].ScrX;
    }
    return rowX;
  }

  internal new bool CursorOnFirstWord()
  {
    char[] txt = this.e.text[this.e.CurLine].txt;
    int index = this.e.CurCol;
    if (index >= this.e.text[this.e.CurLine].len)
      index = this.e.text[this.e.CurLine].len - 1;
    if (index < 0)
      index = 0;
    if (txt[index] == ' ')
      --index;
    for (; index >= 0; --index)
    {
      if (txt[index] == ' ')
        return false;
    }
    return true;
  }

  internal new bool DisengageCaret()
  {
    if (this.e.CaretEngaged)
    {
      this.e.CaretPos = this.RowColToAbs(this.e.CurLine, this.e.CurCol, true, false);
      this.e.CaretPage = this.e.CurPage;
      this.e.CaretEngaged = false;
    }
    return true;
  }

  internal new bool DocFitsInWindow()
  {
    int num = 0;
    for (int lin = 0; lin < this.e.TotalLines; ++lin)
    {
      num += this.ScrLineHeight(lin, false);
      if (num > this.e.TerWinHeight)
        return false;
    }
    return true;
  }

  internal new bool EngageCaret(int cmd)
  {
    if (!this.e.CaretEngaged && cmd != 674 && cmd != 676 && cmd != 722 && cmd != 681 && cmd != 680 && cmd != 682 && cmd != 686 && cmd != 692 && cmd != 710 && cmd != 643 && cmd != 717)
    {
      this.e.CaretEngaged = true;
      if (cmd == 600 || cmd == 601 || cmd == 718 || cmd == -1)
        return true;
      int col = this.e.CurCol;
      int row;
      this.AbsToRowCol(this.e.CaretPos, out row, out col, true, false);
      this.e.CurCol = col;
      if (this.e.TerArg.PageMode)
      {
        this.e.CurPage = this.e.CaretPage;
        this.e.CurLine = row;
        this.PaintTer();
      }
      else
        this.TerPosLine(row + 1);
    }
    return true;
  }

  internal new bool FrameNoRotateDC(Graphics gr)
  {
    gr.Transform = new Matrix();
    return true;
  }

  internal new bool FrameRotateDC(Graphics gr, int FrameNo)
  {
    int boxFrame = this.e.frame[FrameNo].BoxFrame;
    int paraFrameId = this.e.frame[FrameNo].ParaFrameId;
    if (this.e.AllTextAngle != 0 || this.e.ParaFrame[paraFrameId].TextAngle != 0)
    {
      int num1 = this.e.frame[boxFrame].x + this.e.frame[boxFrame].SpaceLeft + (this.e.InPrinting ? this.e.PrtLeftMarg : 0);
      int num2 = this.e.frame[boxFrame].y + this.e.frame[boxFrame].SpaceTop + (this.e.InPrinting ? this.e.PrtTopMarg : 0);
      int num3 = this.e.frame[boxFrame].height - this.e.frame[boxFrame].SpaceTop - this.e.frame[boxFrame].SpaceBot;
      int num4 = this.e.frame[boxFrame].width - this.e.frame[boxFrame].SpaceLeft - this.e.frame[boxFrame].SpaceRight;
      float m11 = 0.0f;
      float m22 = 0.0f;
      float m21;
      float dx;
      float m12;
      float dy;
      if (this.e.ScrFrameAngle == 90)
      {
        m21 = 1f;
        dx = (float) (num1 - num2);
        m12 = -1f;
        dy = (float) (num1 + num2 + num3);
      }
      else
      {
        m21 = -1f;
        dx = (float) (num1 + num2 + num4);
        m12 = 1f;
        dy = (float) (num2 - num1);
      }
      gr.Transform = new Matrix(m11, m12, m21, m22, dx, dy);
    }
    return true;
  }

  internal new bool FrameRotateRect(ref COp.RECT rect, int FrameNo)
  {
    ref tc.StrFrame local = ref this.e.frame[FrameNo];
    int num1 = this.FrameRotateX(rect.left, rect.top, FrameNo);
    int num2 = this.FrameRotateY(rect.left, rect.top, FrameNo);
    int num3 = rect.right - rect.left;
    int num4 = rect.bottom - rect.top;
    rect.left = num1;
    rect.bottom = num2;
    if (this.GetFrameTextAngle(FrameNo) == 90)
    {
      rect.right = rect.left + num4;
      rect.top = rect.bottom - num3;
    }
    else
    {
      rect.right = rect.left - num4;
      rect.top = rect.bottom + num3;
    }
    return true;
  }

  internal new int FrameRotateX(int x, int y, int FrameNo)
  {
    int index = this.e.frame[FrameNo].BoxFrame;
    int paraFrameId = this.e.frame[FrameNo].ParaFrameId;
    if (paraFrameId == 0)
      index = FrameNo;
    int frameTextAngle = this.GetFrameTextAngle(FrameNo);
    if (frameTextAngle == 0)
      return x;
    int num1;
    int num2;
    int num3;
    if (paraFrameId == 0)
    {
      num1 = this.e.frame[index].x + (this.e.InPrinting ? this.e.PrtLeftMarg : 0);
      num2 = this.e.frame[index].y + (this.e.InPrinting ? this.e.PrtTopMarg : 0);
      num3 = this.e.frame[index].width;
    }
    else
    {
      num1 = this.e.frame[index].x + this.e.frame[index].SpaceLeft + (this.e.InPrinting ? this.e.PrtLeftMarg : 0);
      num2 = this.e.frame[index].y + this.e.frame[index].SpaceTop + (this.e.InPrinting ? this.e.PrtTopMarg : 0);
      num3 = this.e.frame[index].width - this.e.frame[index].SpaceLeft - this.e.frame[index].SpaceRight;
    }
    return frameTextAngle != 90 ? num1 + num2 + num3 - y : num1 + y - num2;
  }

  internal new int FrameRotateY(int x, int y, int FrameNo)
  {
    int index = this.e.frame[FrameNo].BoxFrame;
    int paraFrameId = this.e.frame[FrameNo].ParaFrameId;
    if (paraFrameId == 0)
      index = FrameNo;
    int frameTextAngle = this.GetFrameTextAngle(FrameNo);
    if (frameTextAngle == 0)
      return y;
    int num1;
    int num2;
    int num3;
    if (paraFrameId == 0)
    {
      num1 = this.e.frame[index].x + (this.e.InPrinting ? this.e.PrtLeftMarg : 0);
      num2 = this.e.frame[index].y + (this.e.InPrinting ? this.e.PrtTopMarg : 0);
      num3 = this.e.frame[index].height;
    }
    else
    {
      num1 = this.e.frame[index].x + this.e.frame[index].SpaceLeft + (this.e.InPrinting ? this.e.PrtLeftMarg : 0);
      num2 = this.e.frame[index].y + this.e.frame[index].SpaceTop + (this.e.InPrinting ? this.e.PrtTopMarg : 0);
      num3 = this.e.frame[index].height - this.e.frame[index].SpaceTop - this.e.frame[index].SpaceBot;
    }
    return frameTextAngle != 90 ? -num1 + x + num2 : num1 + num2 + num3 - x;
  }

  internal new bool GetCaretXY(int CaretLine, int CaretCol, int y, out int pX, out int pY)
  {
    int num1 = y;
    int num2;
    int x = num2 = this.ColToUnits(CaretCol, CaretLine, 1024 /*0x0400*/);
    if ((this.e.text[CaretLine].flags2 & 32 /*0x20*/) != 0)
    {
      int pTotalLineSeg = this.e.TotalScrSeg;
      this.e.pScrSeg = this.GetLineSeg(CaretLine, (ushort[]) null, out pTotalLineSeg);
      this.e.TotalScrSeg = pTotalLineSeg;
      if (this.e.TotalScrSeg > 0)
      {
        int frame;
        if ((frame = this.frm.GetFrame(CaretLine)) >= 0)
        {
          this.e.CurScrSeg = this.GetCharSeg(CaretLine, CaretCol, this.e.TotalScrSeg, this.e.pScrSeg);
          if (CaretCol == 0)
          {
            num2 = x = this.RtlX(x, 0, frame, this.e.pScrSeg[this.e.CurScrSeg]);
            if (num2 >= this.e.TerWinWidth)
              --num2;
          }
          else
          {
            int col = CaretCol - 1;
            this.e.CurScrSeg = this.GetCharSeg(CaretLine, col, this.e.TotalScrSeg, this.e.pScrSeg);
            num2 = x = this.RtlX(this.ColToUnits(col, CaretLine, 2), 0, frame, this.e.pScrSeg[this.e.CurScrSeg]);
          }
        }
        this.e.TotalScrSeg = 0;
      }
    }
    if (this.e.TerArg.PageMode)
    {
      int num3 = 0;
      int fid = this.e.text[CaretLine].fid;
      int cid = this.e.text[CaretLine].cid;
      if (this.e.AllTextAngle2 != 0)
        num3 = -this.e.AllTextAngle2;
      else if (fid != 0)
        num3 = this.e.ParaFrame[fid].TextAngle;
      if (num3 == 0 && cid > 0)
        num3 = this.e.cell[cid].TextAngle;
      int frame;
      if (num3 > 0 && (frame = this.frm.GetFrame(CaretLine)) >= 0)
      {
        num2 = this.FrameRotateX(x, y, frame);
        num1 = this.FrameRotateY(x, y, frame);
        if (num3 == 270)
          num2 -= this.e.CaretHeight;
      }
    }
    pX = num2;
    pY = num1;
    return true;
  }

  internal new int GetCharSeg(int LineNo, int col, int SegCount, tc.StrLineSeg[] pSegIn)
  {
    tc.StrLineSeg[] strLineSegArray = pSegIn != null ? pSegIn : this.GetLineSeg(LineNo, (ushort[]) null, out SegCount);
    if (strLineSegArray == null || SegCount == 0)
      return 0;
    int charSeg = SegCount - 1;
    while (charSeg >= 0 && col < strLineSegArray[charSeg].col)
      --charSeg;
    return charSeg;
  }

  internal new int GetFlatX(int x, int y, int line)
  {
    int pVal1_1 = 0;
    bool flag = false;
    if (line < 0)
    {
      line = this.UnitsToLine(x, y);
      if (line >= this.e.TotalLines)
        line = this.e.TotalLines - 1;
    }
    if (this.e.cell[this.e.text[line].cid].TextAngle != 0)
      flag = true;
    if (this.e.TerArg.PageMode && this.e.ContainsParaFrames | flag)
    {
      int index1 = 0;
      while (index1 < this.e.TotalFrames && (this.e.frame[index1].empty || this.e.frame[index1].ParaFrameId == 0 || (this.e.frame[index1].flags & 2048 /*0x0800*/) == 0 || (this.e.frame[index1].flags & 4096 /*0x1000*/) != 0 && (this.e.TerOpFlags2 & 512 /*0x0200*/) == 0 || x < this.e.frame[index1].x || x >= this.e.frame[index1].x + this.e.frame[index1].width || y < this.e.frame[index1].y || y >= this.e.frame[index1].y + this.e.frame[index1].height))
        ++index1;
      if (index1 < this.e.TotalFrames)
      {
        int pict = this.e.ParaFrame[this.e.frame[index1].ParaFrameId].pict;
        if (pict < this.e.TotalFonts && (this.e.TerFont[pict].style & 128 /*0x80*/) != 0 && this.e.TerFont[pict].FrameType != 0)
        {
          ushort[] numArray = this.OpenCfmt(line);
          int col = 0;
          while (col < this.e.text[line].len && (int) numArray[col] != pict)
            ++col;
          this.CloseCfmt(line);
          if (col < this.e.text[line].len)
            return this.ColToUnits(col, line, 0);
        }
      }
      int FrameNo1 = 0;
      while (FrameNo1 < this.e.TotalFrames && (this.e.frame[FrameNo1].empty || this.e.frame[FrameNo1].ParaFrameId == 0 && this.e.frame[FrameNo1].CellId == 0 || (this.e.frame[FrameNo1].flags & 4096 /*0x1000*/) != 0 && (this.e.TerOpFlags2 & 512 /*0x0200*/) == 0 || this.GetFrameTextAngle(FrameNo1) == 0 || !this.InRotatedFrame(x, y, FrameNo1)))
        ++FrameNo1;
      int FrameNo2 = FrameNo1;
      if (FrameNo2 < this.e.TotalFrames)
      {
        int paraFrameId = this.e.frame[FrameNo2].ParaFrameId;
        int cellId = this.e.frame[FrameNo2].CellId;
        int index2 = paraFrameId == 0 ? FrameNo2 : this.e.frame[FrameNo2].BoxFrame;
        int num1;
        int num2;
        int num3;
        if (paraFrameId == 0 && cellId > 0 && this.e.cell[cellId].TextAngle != 0)
        {
          num1 = this.e.frame[index2].x;
          num2 = this.e.frame[index2].y;
          num3 = this.e.frame[index2].height;
        }
        else
        {
          num1 = this.e.frame[index2].x + this.e.frame[index2].SpaceLeft;
          num2 = this.e.frame[index2].y + this.e.frame[index2].SpaceTop;
          num3 = this.e.frame[index2].height - this.e.frame[index2].SpaceTop - this.e.frame[index2].SpaceBot;
        }
        int frameTextAngle = this.GetFrameTextAngle(FrameNo2);
        if (frameTextAngle > 0)
          x = frameTextAngle != 90 ? num1 + (y - num2) : num1 + num3 - (y - num2);
      }
    }
    if ((this.e.text[line].flags2 & 32 /*0x20*/) != 0)
    {
      int FrameNo3 = -1;
      if (this.e.TerArg.PageMode)
      {
        int FrameNo4;
        for (FrameNo4 = 0; FrameNo4 < this.e.TotalFrames; ++FrameNo4)
        {
          if (!this.e.frame[FrameNo4].empty && ((this.e.frame[FrameNo4].flags & 4096 /*0x1000*/) == 0 || (this.e.TerOpFlags2 & 512 /*0x0200*/) != 0))
          {
            int paraFrameId = this.e.frame[FrameNo4].ParaFrameId;
            pVal1_1 = this.e.frame[FrameNo4].width;
            int height = this.e.frame[FrameNo4].height;
            if (this.GetFrameTextAngle(FrameNo4) != 0)
              this.SwapInts(ref pVal1_1, ref height);
            if (x >= this.e.frame[FrameNo4].x && x < this.e.frame[FrameNo4].x + pVal1_1 && y >= this.e.frame[FrameNo4].y && y < this.e.frame[FrameNo4].y + height)
              break;
          }
        }
        if (FrameNo4 < this.e.TotalFrames)
          FrameNo3 = FrameNo4;
      }
      else
      {
        FrameNo3 = 0;
        pVal1_1 = this.e.frame[FrameNo3].width;
      }
      if (FrameNo3 >= 0 && FrameNo3 < this.e.TotalFrames)
      {
        int x1 = this.e.frame[FrameNo3].x;
        int pTotalLineSeg = this.e.TotalScrSeg;
        this.e.pScrSeg = this.GetLineSeg(line, (ushort[]) null, out pTotalLineSeg);
        this.e.TotalScrSeg = pTotalLineSeg;
        int num4 = -1;
        for (int index = 0; index < this.e.TotalScrSeg; ++index)
        {
          int pVal1_2 = this.e.pScrSeg[index].x + x1;
          pVal1_2 = this.RtlX(pVal1_2, 0, FrameNo3, this.e.pScrSeg[index]);
          int pVal2 = this.RtlX(this.e.pScrSeg[index].x + this.e.pScrSeg[index].width + x1, 0, FrameNo3, this.e.pScrSeg[index]);
          if (pVal1_2 > pVal2)
            this.SwapInts(ref pVal1_2, ref pVal2);
          if (index == 0 && (this.e.pScrSeg[index].ParaRtl && x >= pVal2 || !this.e.pScrSeg[index].ParaRtl && x < pVal1_2))
          {
            num4 = x1;
            break;
          }
          if (x >= pVal1_2 && x < pVal2)
          {
            int num5 = this.e.pScrSeg[index].x + x1;
            num4 = !this.e.pScrSeg[index].rtl ? num5 + (x - pVal1_2) : num5 + (pVal2 - x);
            break;
          }
          if (index == this.e.TotalScrSeg - 1 && (this.e.pScrSeg[index].ParaRtl && x < pVal1_2 || !this.e.pScrSeg[index].ParaRtl && x >= pVal2))
          {
            num4 = x1 + (pVal1_1 - 1);
            break;
          }
        }
        if (num4 != -1)
          x = num4;
        this.e.pScrSeg = (tc.StrLineSeg[]) null;
        this.e.TotalScrSeg = this.e.CurScrSeg = 0;
      }
    }
    return x;
  }

  internal new int GetFrameTextAngle(int FrameNo)
  {
    int frameTextAngle = 0;
    int paraFrameId = this.e.frame[FrameNo].ParaFrameId;
    int cellId = this.e.frame[FrameNo].CellId;
    if (this.e.AllTextAngle2 != 0)
      frameTextAngle = -this.e.AllTextAngle2;
    else if (paraFrameId > 0)
      frameTextAngle = this.e.ParaFrame[paraFrameId].TextAngle;
    if (frameTextAngle == 0 && cellId > 0)
      frameTextAngle = this.e.cell[cellId].TextAngle;
    return frameTextAngle;
  }

  internal int GetFrameTextAngle2(int FrameNo)
  {
    return this.e.AllTextAngle2 != 0 ? this.e.AllTextAngle2 : this.GetFrameTextAngle(FrameNo);
  }

  internal new bool GetHotSpotHit(Point pt, out int obj, out int HotSpot)
  {
    int num;
    HotSpot = num = -1;
    obj = num;
    for (int index1 = 0; index1 < this.e.TotalDragObjs; ++index1)
    {
      if (this.e.DragObj[index1].InUse)
      {
        if (this.e.DragObj[index1].IsHotPolygon)
        {
          Point[] points = new Point[this.e.DragObj[index1].HotRectCount];
          for (int index2 = 0; index2 < this.e.DragObj[index1].HotRectCount; ++index2)
          {
            points[index2].X = this.e.DragObj[index1].HotRect[index2].left;
            points[index2].Y = this.e.DragObj[index1].HotRect[index2].top;
          }
          using (GraphicsPath graphicsPath = new GraphicsPath())
          {
            graphicsPath.AddPolygon(points);
            if (!graphicsPath.IsVisible(pt))
              continue;
          }
          obj = index1;
          HotSpot = 0;
          return true;
        }
        for (int index3 = 0; index3 < this.e.DragObj[index1].HotRectCount; ++index3)
        {
          if (this.ToRectangle(this.e.DragObj[index1].HotRect[index3]).Contains(pt))
          {
            obj = index1;
            HotSpot = index3;
            return true;
          }
        }
      }
    }
    return false;
  }

  internal ushort[] GetLineCharWidth(int LineNo, bool originalWidth)
  {
    if (LineNo >= this.e.TotalLines || LineNo < 0)
      return (ushort[]) null;
    int TabNo = 0;
    int SpaceNo = 0;
    ushort[] numArray1 = (ushort[]) null;
    bool screen = !this.e.TerArg.WordWrap || !this.e.TerArg.PrintView || this.e.TerArg.FittedView;
    ushort[] lineCharWidth = new ushort[this.e.text[LineNo].len + 1];
    ushort[] numArray2 = new ushort[this.e.text[LineNo].len + 1];
    char[] txt = this.e.text[LineNo].txt;
    int len = this.e.text[LineNo].len;
    if (this.e.text[LineNo].fmt != null)
      numArray1 = this.e.text[LineNo].fmt;
    int num1;
    if (this.e.InPrinting)
    {
      this.e.DevResX = this.e.UnitResX;
      num1 = 0;
    }
    else
    {
      this.e.DevResX = this.e.ScrResX;
      num1 = this.GetRowX(LineNo);
      if (!screen)
      {
        int pfmt = this.e.text[LineNo].pfmt;
        num1 = this.e.PfmtId[pfmt].LeftIndent != num1 ? this.MulDiv(num1, 1440, this.e.ScrResX) : this.e.PfmtId[pfmt].LeftIndentTwips;
      }
    }
    if ((this.e.text[LineNo].flags & 536870912 /*0x20000000*/) != 0)
      ++TabNo;
    for (int col = 0; col < len; ++col)
    {
      int x1 = 0;
      char ch = txt[col];
      int index = this.e.text[LineNo].fmt != null ? (int) numArray1[col] : (int) this.e.text[LineNo].UniFmt;
      if ((this.e.TerFont[index].style & 196608 /*0x030000*/) != 0 && this.IsLcChar(ch))
        ch -= '2';
      int x2 = 0;
      bool flag = true;
      switch (ch)
      {
        case '\u0004':
          x1 = 0;
          flag = false;
          break;
        case '\u0006':
          if (col == len - 1 && !this.e.ShowParaMark)
          {
            x1 = this.fnt.LwrCharWidth(index, screen, '-');
            flag = false;
            break;
          }
          break;
        case '\t':
          if (!this.edit.HiddenText(index))
          {
            x1 = this.GetTabWidth(LineNo, TabNo, num1);
            ++TabNo;
            flag = false;
            break;
          }
          break;
        case ' ':
          if (this.e.text[LineNo].tabw != null && (this.e.text[LineNo].tabw.type & 128 /*0x80*/) != 0 && this.JustifySpace(index))
          {
            x1 = this.fnt.LwrCharWidth(index, screen, ch) + this.GetSpaceAdj(LineNo, SpaceNo);
            ++SpaceNo;
            flag = false;
            break;
          }
          break;
      }
      if (flag)
      {
        if (this.e.ShowParaMark && !screen && (this.e.TerFont[index].style & 128 /*0x80*/) != 0 && this.e.TerFont[index].FrameType != 0)
        {
          x2 = this.e.TerFont[index].CharWidth[24];
          x1 = screen ? x2 : this.MulDiv(x2, 1440, this.e.DevResX);
        }
        else if ((this.e.TerFont[index].style & 128 /*0x80*/) != 0 && this.edit.HiddenText(index))
        {
          x2 = x1 = 0;
        }
        else
        {
          if (this.e.text[LineNo].cwidth != null && (this.fnt.GetCharWidth(LineNo, col) & 16384 /*0x4000*/) != 0)
          {
            if (screen)
            {
              x1 = x2 = this.fnt.GetCharWidth(LineNo, col) & 16383 /*0x3FFF*/;
            }
            else
            {
              x1 = this.fnt.GetCharWidth(LineNo, col) & 16383 /*0x3FFF*/;
              x2 = this.MulDiv(x1, this.e.DevResX, 1440);
            }
            if ((this.e.TerFont[index].flags & 128 /*0x80*/) == 0)
            {
              if (!this.e.EditFootnoteText && this.IsFootnoteStyle(this.e.TerFont[index].style))
                x1 = x2 = 0;
              if (!this.e.EditEndnoteText && (this.e.TerFont[index].style & 32768 /*0x8000*/) != 0)
                x1 = x2 = 0;
            }
            if (this.e.TerFont[index].CharWidth[65] == 0)
              x1 = x2 = 0;
          }
          else
          {
            x2 = this.fnt.LwrCharWidth(index, true, ch);
            x1 = !screen ? this.fnt.LwrCharWidth(index, screen, ch) : x2;
          }
          if (this.e.text[LineNo].tabw != null && this.e.text[LineNo].tabw.CharFlags != null && col < this.e.text[LineNo].tabw.CharFlagsLen && ((int) this.e.text[LineNo].tabw.CharFlags[col] & 3) != 0)
          {
            int extraSpaceScrX = this.e.ExtraSpaceScrX;
            x2 += this.e.InPrinting ? this.e.ExtraSpacePrtX : extraSpaceScrX;
            x1 += screen ? extraSpaceScrX : this.e.ExtraSpacePrtX;
          }
        }
      }
      lineCharWidth[col] = (ushort) x1;
      numArray2[col] = (ushort) x2;
    }
    if (!screen && !this.e.InPrinting)
    {
      int num2 = 0;
      if (this.e.TerArg.PageMode)
      {
        int frame = this.frm.GetFrame(LineNo);
        if (frame >= 0)
          num2 = this.e.frame[frame].x;
      }
      int x3 = num1 + num2;
      int num3 = this.MulDiv(x3, this.e.DevResX, 1440);
      int num4 = 0;
      int num5 = 9999;
      if (!originalWidth)
      {
        for (int index1 = 0; index1 < len; ++index1)
        {
          if (num5 == 9999 || txt[index1] == ' ')
          {
            int index2 = index1 + 1;
            while (index2 < len && txt[index2] != ' ')
              ++index2;
            num5 = index2 >= len ? -1 : index2;
          }
          int x4 = x3 + (int) lineCharWidth[index1];
          int num6 = this.MulDiv(x4, this.e.DevResX, 1440);
          int num7 = (int) lineCharWidth[index1];
          if (num6 >= num3)
          {
            lineCharWidth[index1] = (ushort) (num6 - num3);
          }
          else
          {
            int num8 = -(num6 - num3);
            if (index1 > 0)
              lineCharWidth[index1 - 1] = (int) lineCharWidth[index1 - 1] < num8 ? (ushort) 0 : (ushort) ((uint) lineCharWidth[index1 - 1] - (uint) num8);
            lineCharWidth[index1] = (ushort) 0;
          }
          if ((int) lineCharWidth[index1] < (int) numArray2[index1])
          {
            ++lineCharWidth[index1];
            ++num6;
          }
          if ((int) lineCharWidth[index1] > (int) numArray2[index1] && numArray2[index1] != (ushort) 0 && lineCharWidth[index1] > (ushort) 0 && txt[index1] != ' ')
          {
            int num9 = (int) lineCharWidth[index1] - (int) numArray2[index1] - 1;
            lineCharWidth[index1] = (ushort) ((uint) numArray2[index1] + 1U);
            num6 -= num9;
            if (index1 > num5 && num9 > 2)
            {
              ++lineCharWidth[index1];
              ++num6;
            }
          }
          if (num7 == 0 && lineCharWidth[index1] > (ushort) 0)
          {
            num6 -= (int) lineCharWidth[index1];
            lineCharWidth[index1] = (ushort) 0;
          }
          x3 = x4;
          num3 = num6;
          num4 += (int) lineCharWidth[index1];
        }
      }
    }
    return lineCharWidth;
  }

  internal new int GetLineHeight(int lin, out int pBaseHeight, out int pExtLead)
  {
    int num1 = 0;
    int num2 = 0;
    int num3 = 0;
    int num4 = 9999;
    int num5 = num4;
    int num6 = num4;
    pBaseHeight = 0;
    pExtLead = 0;
    int lineHeight;
    if (lin >= this.e.TotalLines)
      lineHeight = this.e.TerFont[0].height;
    else if (this.e.text[lin].fmt == null)
    {
      ushort uniFmt = this.e.text[lin].UniFmt;
      int num7 = this.e.TerFont[(int) uniFmt].height - this.e.TerFont[(int) uniFmt].BaseHeight;
      if (this.e.TerFont[(int) uniFmt].OffsetVal > 0)
        num7 -= this.e.TerFont[(int) uniFmt].OffsetVal;
      int offsetVal = this.e.TerFont[(int) uniFmt].OffsetVal;
      int num8;
      int num9 = num8 = 0;
      if (offsetVal > 0)
        num9 = offsetVal;
      else
        num8 = -offsetVal;
      num3 = this.e.TerFont[(int) uniFmt].BaseHeight;
      lineHeight = num3 + num7 - num8;
      if (num8 != 0)
        num3 -= num8;
      if (num9 != 0)
        num3 += num9;
      num2 = this.e.TerFont[(int) uniFmt].ExtLead;
    }
    else if (this.e.text[lin].len > 0)
    {
      ushort[] fmt = this.e.text[lin].fmt;
      char[] txt = this.e.text[lin].txt;
      ushort index1 = fmt[0];
      int num10;
      num2 = num10 = 0;
      int num11 = num10;
      num1 = num10;
      num3 = 0;
      int len = this.e.text[lin].len;
      int num12;
      bool flag1 = (num12 = 0) != 0;
      bool flag2 = num12 != 0;
      bool flag3 = num12 != 0;
      int index2 = 0;
      if (len > 1 && (this.e.text[lin].flags & 129) != 0)
      {
        index2 = (int) fmt[len - 1];
        --len;
        flag1 = true;
      }
      for (int index3 = 0; index3 < len; ++index3)
      {
        if (!flag2 && (txt[index3] == ' ' || txt[index3] == '\t') && this.e.TerFont[(int) fmt[index3]].height > 0)
          flag2 = true;
        if ((txt[index3] != ' ' && txt[index3] != '\t' || index3 >= len - 1 && !flag3) && (!flag3 || (int) fmt[index3] != (int) index1))
        {
          index1 = fmt[index3];
          if (this.e.TerFont[(int) index1].BaseHeight > num3)
            num3 = this.e.TerFont[(int) index1].BaseHeight;
          int num13 = this.e.TerFont[(int) index1].height - this.e.TerFont[(int) index1].BaseHeight;
          if (this.e.TerFont[(int) index1].OffsetVal > 0)
            num13 -= this.e.TerFont[(int) index1].OffsetVal;
          if (num13 > num11)
            num11 = num13;
          if (this.e.TerFont[(int) index1].ExtLead > num2)
            num2 = this.e.TerFont[(int) index1].ExtLead;
          int offsetVal = this.e.TerFont[(int) index1].OffsetVal;
          if (offsetVal == 0)
            num6 = num5 = 0;
          else if (offsetVal > 0 && offsetVal < num5)
            num5 = offsetVal;
          else if (offsetVal < 0 && -offsetVal < num6)
            num6 = -offsetVal;
          if (this.e.TerFont[(int) index1].height > 0)
            flag3 = true;
        }
      }
      if (num5 == num4)
        num5 = 0;
      if (num6 == num4)
        num6 = 0;
      if (num5 != 0 && num6 != 0)
        num5 = num6 = 0;
      lineHeight = num3 + num11 - num6 - num5;
      if (num6 != 0)
        num3 -= num6;
      if (lineHeight == 0 && flag2 | flag1)
      {
        lineHeight = this.e.TerFont[index2].height;
        num3 = this.e.TerFont[index2].BaseHeight;
        num2 = this.e.TerFont[index2].ExtLead;
      }
    }
    else
    {
      lineHeight = this.e.TerFont[0].height;
      num3 = this.e.TerFont[0].BaseHeight;
      num2 = this.e.TerFont[0].ExtLead;
    }
    pExtLead = num2;
    pBaseHeight = num3;
    return lineHeight;
  }

  internal new tc.StrLineSeg[] GetLineSeg(int LineNo, ushort[] pWidthParam, out int pTotalLineSeg)
  {
    return this.GetLineSeg2(LineNo, pWidthParam, out pTotalLineSeg, 0, 0, (char[]) null, (ushort[]) null);
  }

  internal new tc.StrLineSeg[] GetLineSeg2(
    int LineNo,
    ushort[] pWidthParam,
    out int pTotalLineSeg,
    int len,
    int CurX,
    char[] ptr,
    ushort[] fmt)
  {
    int index1 = 0;
    int index2 = 0;
    int num1 = 0;
    int num2 = 0;
    tc.StrLineSeg[] OldObj = (tc.StrLineSeg[]) null;
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    bool flag4 = false;
    bool flag5 = false;
    bool flag6 = false;
    ushort[] numArray1;
    ushort[] numArray2;
    int num3;
    if (LineNo >= 0)
    {
      numArray1 = pWidthParam != null ? pWidthParam : this.GetLineCharWidth(LineNo);
      numArray2 = (this.e.text[LineNo].flags2 & 32 /*0x20*/) != 0 ? this.e.text[LineNo].cwidth : (ushort[]) null;
      num3 = !this.LineInfo(LineNo, 1024 /*0x0400*/) ? -1 : this.e.text[LineNo].tabw.FrameCharPos;
      CurX = !this.e.InPrinting ? this.GetRowX(LineNo) : 0;
      len = this.e.text[LineNo].len;
      if (len != 0)
      {
        fmt = this.OpenCfmt(LineNo);
        ptr = this.e.text[LineNo].txt;
      }
      else
        goto label_35;
    }
    else
    {
      numArray1 = pWidthParam;
      numArray2 = (ushort[]) null;
      num3 = -1;
    }
    for (int index3 = 0; index3 <= len; ++index3)
    {
      if (index3 < len)
      {
        int index4 = (int) fmt[index3];
        flag1 = numArray2 != null && ((uint) numArray2[index3] & 32768U /*0x8000*/) > 0U;
        flag3 = this.e.TerFont[index4].rtl;
        flag5 = (this.e.TerFont[index4].style & 39936) != 0;
        if (flag3 && !flag1)
        {
          int fieldId = this.e.TerFont[index4].FieldId;
          flag1 = fieldId == 8 || fieldId == 10;
        }
      }
      if (index3 == 0)
      {
        flag2 = flag1;
        flag4 = flag3;
        flag6 = flag5;
        index2 = num1 = 0;
        num2 = CurX;
      }
      else
      {
        bool flag7 = false;
        if (index3 == len || numArray2 != null && flag1 != flag2 || index3 == num3 || flag6 != flag5 || flag3 != flag4 || ptr[index3] < ' ' && ptr[index3] != '\u0018' || ptr[index3 - 1] < ' ' && ptr[index3 - 1] != '\u0018')
          flag7 = true;
        if (!flag7)
        {
          bool flag8 = ptr[index3] >= '0' && ptr[index3] <= '9';
          bool flag9 = ptr[index3 - 1] >= '0' && ptr[index3 - 1] <= '9';
          if (flag4 && flag8 != flag9)
            flag7 = true;
        }
        if (flag7)
        {
          OldObj = index1 != 0 ? this.ReAlloc(OldObj, index1 + 1) : new tc.StrLineSeg[1];
          if (index2 == num3)
          {
            if (this.e.InPrinting)
              num2 += this.e.text[LineNo].tabw.FrameSpaceWidth;
            else
              num2 += this.UnitToScrX(this.e.text[LineNo].tabw.FrameSpaceWidth);
          }
          OldObj[index1].col = index2;
          OldObj[index1].count = index3 - index2;
          OldObj[index1].x = num2;
          OldObj[index1].width = num1;
          OldObj[index1].rtl = numArray2 != null ? flag2 : flag4;
          if (ptr[index2] < ' ' && index1 > 0)
            OldObj[index1].rtl = true;
          if (index3 == index2 + 1 & flag4)
            OldObj[index1].rtl = true;
          if (LineNo >= 0 && (this.e.text[LineNo].flags2 & 32 /*0x20*/) != 0 && (ptr[index2] < '0' || ptr[index2] > '9') && len == 1 | flag4)
            OldObj[index1].rtl = true;
          ++index1;
          if (index3 != len)
          {
            flag2 = flag1;
            flag4 = flag3;
            flag6 = flag5;
            index2 = index3;
            num2 += num1;
            num1 = 0;
          }
          else
            break;
        }
      }
      num1 += (int) numArray1[index3];
    }
    if (LineNo >= 0)
      this.CloseCfmt(LineNo);
    for (int index5 = 0; index5 < index1; ++index5)
    {
      OldObj[index5].pFirstSeg = OldObj;
      OldObj[index5].idx = index5;
      OldObj[index5].TotalLineSeg = index1;
      OldObj[index5].ParaRtl = LineNo < 0 || (this.e.text[LineNo].flags2 & 256 /*0x0100*/) != 0;
    }
label_35:
    pTotalLineSeg = index1;
    return OldObj;
  }

  internal new bool GetLineSpacing(
    int lin,
    int TextHeight,
    out int SpcBef,
    out int SpcAft,
    bool screen)
  {
    return this.GetLineSpacing2(lin, TextHeight, out SpcBef, out SpcAft, out tc.SkipInt, out tc.SkipInt, screen);
  }

  internal new bool GetLineSpacing2(
    int lin,
    int TextHeight,
    out int SpcBef,
    out int SpcAft,
    out int pParaSpcBef,
    out int pParaSpcAft,
    bool screen)
  {
    SpcBef = 0;
    SpcAft = 0;
    pParaSpcBef = 0;
    pParaSpcAft = 0;
    if (TextHeight == 0)
    {
      TextHeight = this.GetLineHeight(lin, out int _, out tc.SkipInt);
      if (!screen)
        TextHeight = this.ScrToUnitY(TextHeight);
    }
    return TextHeight == 0 || this.GetLineSpacingAlt(lin, TextHeight, out SpcBef, out SpcAft, out pParaSpcBef, out pParaSpcAft, screen);
  }

  internal new bool GetLineSpacingAlt(
    int lin,
    int TextHeight,
    out int SpcBef,
    out int SpcAft,
    out int pParaSpcBef,
    out int pParaSpcAft,
    bool screen)
  {
    int num1 = 0;
    int num2 = 0;
    int num3 = 0;
    SpcBef = 0;
    SpcAft = 0;
    pParaSpcBef = 0;
    pParaSpcAft = 0;
    if (TextHeight != 0)
    {
      if ((this.e.PfmtId[this.e.text[lin].pfmt].flags & 12288 /*0x3000*/) == 0)
      {
        for (int index = 0; index < this.e.TotalSects; ++index)
        {
          if (this.e.TerSect[index].LineSpace != 0 && (this.e.TerSect[index].flags & 1024 /*0x0400*/) != 0)
          {
            int section = this.GetSection(lin);
            if (section >= 0 && (this.e.TerSect[index].flags & 1024 /*0x0400*/) != 0)
            {
              num3 = !screen ? this.TwipsToUnitY(this.e.TerSect[section].LineSpace) : this.TwipsToScrY(this.e.TerSect[section].LineSpace);
              break;
            }
            break;
          }
        }
      }
      int pfmt1 = this.e.text[lin].pfmt;
      if (this.e.PfmtId[pfmt1].SpaceBetween != 0)
      {
        int num4 = !screen ? this.TwipsToUnitY(this.e.PfmtId[pfmt1].SpaceBetween) : this.TwipsToScrY(this.e.PfmtId[pfmt1].SpaceBetween);
        if (num4 > 0)
        {
          if (num4 > TextHeight)
            num1 += num4 - TextHeight;
        }
        else
        {
          int num5 = -num4 - TextHeight;
          if (num5 < 0)
            num5 = 0;
          int num6 = num5 / 2;
          int num7 = num5 - num6;
          num1 += num6;
          num2 += num7;
        }
      }
      else if (this.e.PfmtId[pfmt1].LineSpacing != 0)
        num2 += this.MulDiv(TextHeight, this.e.PfmtId[pfmt1].LineSpacing, 100);
      else if ((this.e.PfmtId[pfmt1].flags & 4) != 0)
        num2 += TextHeight + 2 * num3;
      else if (num3 != 0)
      {
        int num8 = num3;
        if (num8 > TextHeight)
        {
          int num9 = num8 - TextHeight;
          int num10 = num9 / 2;
          int num11 = num9 - num10;
          num1 += num10;
          num2 += num11;
        }
      }
      int num12 = this.LineSpaceAfter(lin);
      int num13 = this.e.PfmtId[pfmt1].SpaceBefore;
      if ((this.e.PfmtId[pfmt1].flags & 131072 /*0x020000*/) != 0 && (this.e.text[lin].flags & 4) != 0)
      {
        int num14 = 270;
        if (lin == 0)
        {
          num13 = 0;
        }
        else
        {
          int num15 = this.LineSpaceAfter(lin - 1);
          int pfmt2 = this.e.text[lin - 1].pfmt;
          if (num14 > 0 && this.e.text[lin].cid > 0 && this.e.text[lin].cid != this.e.text[lin - 1].cid)
            num14 = 0;
          if (num14 > 0 && this.e.text[lin].fid > 0 && this.e.text[lin].fid != this.e.text[lin - 1].fid)
            num14 = 0;
          if (num14 > 0 && this.e.PfmtId[pfmt1].BltId > 0 && this.e.PfmtId[pfmt1].BltId == this.e.PfmtId[pfmt2].BltId)
            num14 = 0;
          num13 = num14 <= num15 ? 0 : num14 - num15;
        }
      }
      int num16 = (this.e.text[lin].flags & 512 /*0x0200*/) == 0 ? 0 : this.e.PfmtId[pfmt1].BorderSpace + 30;
      if ((this.e.PfmtId[pfmt1].flags & 256 /*0x0100*/) != 0 && num16 > 0)
        num16 += 15;
      int x1 = num13 + num16;
      if (x1 > 0 && (this.e.text[lin].flags & 4) != 0)
      {
        int num17 = !screen ? this.TwipsToUnitY(x1) : this.TwipsToScrY(x1);
        num1 += num17;
        if ((this.e.text[lin].flags & 4194816 /*0x400200*/) != 0)
          pParaSpcBef = num17;
      }
      int num18 = (this.e.text[lin].flags & 1024 /*0x0400*/) != 0 || (this.e.text[lin].flags2 & 16 /*0x10*/) != 0 ? this.e.PfmtId[pfmt1].BorderSpace + 30 : 0;
      if ((this.e.PfmtId[pfmt1].flags & 256 /*0x0100*/) != 0 && num18 > 0)
        num18 += 15;
      int num19 = num18;
      int x2 = num12 + num19;
      if (x2 > 0 && (this.e.text[lin].flags & 1) != 0)
      {
        int num20 = !screen ? this.TwipsToUnitY(x2) : this.TwipsToScrY(x2);
        num2 += num20;
        if ((this.e.text[lin].flags & 8389632 /*0x800400*/) != 0)
          pParaSpcAft = num20;
      }
      if (lin > 0 && this.e.text[lin].len == 1 && this.LineInfo(lin, 16 /*0x10*/) && this.TableLevel(lin - 1) > this.TableLevel(lin))
      {
        int cid = this.e.text[lin].cid;
        int num21 = (!screen ? this.TwipsToUnitY(this.e.cell[cid].margin) : this.TwipsToScrY(this.e.cell[cid].margin)) - TextHeight;
        int num22 = num21 / 2;
        int num23 = num21 - num22;
        num1 = num22;
        num2 = num23;
      }
      SpcBef = num1;
      SpcAft = num2;
    }
    return true;
  }

  internal int GetLineWidth(int lin, bool IncludeDelim, bool screen, bool originalWidth)
  {
    int x = 0;
    int TabNo = 0;
    int SpaceNo = 0;
    ushort[] numArray = (ushort[]) null;
    if (lin >= this.e.TotalLines || this.e.text[lin].len == 0)
      return 0;
    if (!this.e.TerArg.PrintView)
      screen = true;
    char[] txt = this.e.text[lin].txt;
    int len = this.e.text[lin].len;
    if (len > 0 && !IncludeDelim && txt[len - 1] == '\u0014')
      --len;
    if (len > 0 && !IncludeDelim && ((int) txt[len - 1] == (int) this.e.ParaChar || (int) txt[len - 1] == (int) this.e.CellChar || txt[len - 1] == '\u000F'))
      --len;
    if (!IncludeDelim)
    {
      while (len > 0 && txt[len - 1] == ' ' && this.e.TerFont[this.e.text[lin].fmt != null ? (int) this.e.text[lin].fmt[len - 1] : (int) this.e.text[lin].UniFmt].FieldId == 0)
        --len;
    }
    if (len == 0 || screen && (numArray = this.GetLineCharWidth(lin, originalWidth)) == null)
      return 0;
    if ((this.e.text[lin].flags & 536870912 /*0x20000000*/) != 0)
      ++TabNo;
    if (this.e.text[lin].fmt == null)
    {
      ushort uniFmt = this.e.text[lin].UniFmt;
      for (int index = 0; index < len; ++index)
      {
        if (screen)
          x += (int) numArray[index];
        else
          x += this.fnt.LwrCharWidth((int) uniFmt, screen, txt[index]);
      }
      return x;
    }
    ushort[] fmt = this.e.text[lin].fmt;
    ushort num = fmt[0];
    for (int index = 0; index < len; ++index)
    {
      if (screen)
      {
        x += (int) numArray[index];
      }
      else
      {
        if ((int) fmt[index] != (int) num)
          num = fmt[index];
        if (txt[index] == '\t')
        {
          int tabWidth = this.GetTabWidth(lin, TabNo, this.UnitToScrX(x));
          x += tabWidth;
          ++TabNo;
        }
        else if (txt[index] == ' ' && this.e.text[lin].tabw != null && (this.e.text[lin].tabw.type & 128 /*0x80*/) != 0 && this.JustifySpace((int) num))
        {
          x = x + this.fnt.LwrCharWidth((int) num, screen, txt[index]) + this.GetSpaceAdj(lin, SpaceNo);
          ++SpaceNo;
        }
        else
          x += this.fnt.LwrCharWidth((int) num, screen, txt[index]);
      }
    }
    return x;
  }

  internal new int GetRowHeight(int lin)
  {
    int frame = this.frm.GetFrame(lin);
    return frame < 0 || lin < this.e.frame[frame].ScrFirstLine || lin > this.e.frame[frame].ScrLastLine ? 0 : this.e.RowHeight[this.e.frame[frame].RowOffset + lin - this.e.frame[frame].ScrFirstLine];
  }

  internal new int GetRowX(int lin)
  {
    int frame = this.frm.GetFrame(lin);
    if (frame < 0)
      return this.MulDiv(this.e.text[lin].JustAdjX, this.e.ScrResX, this.e.UnitResX);
    if (lin >= this.e.frame[frame].ScrFirstLine && lin <= this.e.frame[frame].ScrLastLine)
      return this.e.RowX[this.e.frame[frame].RowOffset + lin - this.e.frame[frame].ScrFirstLine];
    int num = 0;
    if (this.e.TerArg.PageMode && this.e.BorderShowing)
      num = this.e.frame[frame].SpaceLeft;
    return num + this.MulDiv(this.e.text[lin].JustAdjX, this.e.ScrResX, this.e.UnitResX);
  }

  internal new int GetRowY(int lin)
  {
    int line = lin;
    if (lin < 0 || lin >= this.e.TotalLines)
      return 0;
    if (this.e.TerArg.PageMode)
    {
      if ((this.e.text[lin].flags & 655360 /*0x0A0000*/) != 0)
      {
        if (lin + 1 < this.e.TotalLines && (this.e.PfmtId[this.e.text[lin + 1].pfmt].flags & 4096 /*0x1000*/) != 0)
          ++lin;
        else if (lin > 0)
          --lin;
      }
      if ((this.e.text[lin].flags & 1310720 /*0x140000*/) != 0)
      {
        if (lin + 1 < this.e.TotalLines && (this.e.PfmtId[this.e.text[lin + 1].pfmt].flags & 8192 /*0x2000*/) != 0)
          ++lin;
        else if (lin > 0)
          --lin;
      }
      int frame = this.frm.GetFrame(lin);
      if (frame < 0)
        return 0;
      int num = this.e.frame[frame].y + this.e.frame[frame].SpaceTop;
      for (int pageFirstLine = this.e.frame[frame].PageFirstLine; pageFirstLine < line; ++pageFirstLine)
      {
        if (this.TableLevel(pageFirstLine) == this.e.frame[frame].level)
          num += this.ScrLineHeight(pageFirstLine, true);
      }
      return num + this.GetObjSpcBef(line, true);
    }
    int index = 0;
    if (lin < this.e.frame[index].ScrFirstLine)
      lin = this.e.frame[index].ScrFirstLine;
    if (lin > this.e.frame[index].ScrLastLine)
      lin = this.e.frame[index].ScrLastLine;
    return this.e.RowY[this.e.frame[index].RowOffset + lin - this.e.frame[index].ScrFirstLine];
  }

  internal new int GetSpaceAdj(int line, int SpaceNo)
  {
    int spaceAdj = 0;
    if (this.e.text[line].tabw != null && (this.e.text[line].tabw.type & 128 /*0x80*/) != 0 && SpaceNo >= this.e.text[line].tabw.JustSpaceIgnore && SpaceNo < this.e.text[line].tabw.JustSpaceCount)
    {
      spaceAdj = this.e.text[line].tabw.JustAdj;
      if (SpaceNo - this.e.text[line].tabw.JustSpaceIgnore < this.e.text[line].tabw.JustCount)
        ++spaceAdj;
    }
    return spaceAdj;
  }

  internal new bool GetTabPos(
    int ParaId,
    tc.StrTab tab,
    int CurPos,
    out int pTabPos,
    out int pTabType,
    out byte pFlags,
    bool screen)
  {
    bool flag1 = false;
    bool flag2 = !this.e.NoTabIndent;
    byte num1 = 0;
    int num2;
    pTabType = num2 = 0;
    pTabPos = num2;
    pFlags = (byte) 0;
    if (screen)
      CurPos = this.ScrToTwipsX(CurPos + 1);
    int count = tab.count;
    int num3 = this.e.PfmtId[ParaId].LeftIndentTwips;
    int index;
    for (index = 0; index < count; ++index)
    {
      if (flag2 && num3 < tab.pos[index] && num3 > CurPos)
      {
        flag1 = true;
        break;
      }
      if (tab.pos[index] > CurPos)
        break;
    }
    int x;
    int num4;
    if (flag1)
    {
      x = num3;
      num4 = 0;
    }
    else if (index < count)
    {
      x = tab.pos[index];
      num4 = tab.type[index];
      num1 = tab.flags[index];
    }
    else
    {
      x = 0;
      if (this.e.NoTabIndent)
        num3 = -99999;
      while (true)
      {
        if (x >= num3)
        {
          if (num3 <= CurPos)
          {
            if (x > CurPos)
              goto label_19;
          }
          else
            break;
        }
        x += this.e.DefTabWidth;
      }
      x = num3;
label_19:
      num4 = 0;
    }
    if (screen)
      x = this.TwipsToScrX(x);
    pTabPos = x;
    pTabType = num4;
    pFlags = num1;
    return true;
  }

  internal new int GetTabWidth(int line, int TabNo, int CurX)
  {
    return this.e.text[line].tabw != null && (this.e.text[line].tabw.type & 1) != 0 && TabNo < this.e.text[line].tabw.count ? this.e.text[line].tabw.width[TabNo] : this.TwipsToScrX((this.ScrToTwipsX(CurX + 1) / this.e.DefTabWidth + 1) * this.e.DefTabWidth) - CurX;
  }

  internal bool GetTerCursorPos(out int CursLine, ref int CursCol)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (CursCol == -1)
    {
      CursLine = this.RowColToAbs(this.e.CurLine, this.e.CurCol, true, false);
    }
    else
    {
      CursLine = this.e.CurLine;
      CursCol = this.e.CurCol;
    }
    return true;
  }

  internal new int[] GetTextCharWidth(int FontId, char[] ptr, int len, ushort[] pWidth)
  {
    int[] textCharWidth = new int[len];
    for (int index = 0; index < len; ++index)
      textCharWidth[index] = pWidth == null ? this.fnt.LwrCharWidth(FontId, true, ptr[index]) : (int) pWidth[index];
    return textCharWidth;
  }

  internal new int GetTextHeight(
    char[] ptr,
    ushort[] fmt,
    int len,
    bool screen,
    out int pBaseHeight,
    out int pExtLead)
  {
    int num1 = 0;
    int num2 = 0;
    int num3 = 0;
    ushort index1 = 0;
    pBaseHeight = 0;
    pExtLead = 0;
    for (int index2 = 0; index2 < len; ++index2)
    {
      if ((index2 <= 0 || (int) ptr[index2] != (int) this.e.ParaChar && ptr[index2] != '\u000F') && ((int) fmt[index2] != (int) index1 || index2 == 0))
      {
        index1 = fmt[index2];
        if (screen)
        {
          if (this.e.TerFont[(int) index1].BaseHeight > num3)
            num3 = this.e.TerFont[(int) index1].BaseHeight;
          if (this.e.TerFont[(int) index1].height - this.e.TerFont[(int) index1].BaseHeight > num1)
            num1 = this.e.TerFont[(int) index1].height - this.e.TerFont[(int) index1].BaseHeight;
          if (this.e.TerFont[(int) index1].ExtLead > num2)
            num2 = this.e.TerFont[(int) index1].ExtLead;
        }
        else
        {
          if (this.e.PrtFont[(int) index1].BaseHeight > num3)
            num3 = this.e.PrtFont[(int) index1].BaseHeight;
          if (this.e.PrtFont[(int) index1].height - this.e.PrtFont[(int) index1].BaseHeight > num1)
            num1 = this.e.PrtFont[(int) index1].height - this.e.PrtFont[(int) index1].BaseHeight;
          if (this.e.PrtFont[(int) index1].ExtLead > num2)
            num2 = this.e.PrtFont[(int) index1].ExtLead;
        }
      }
    }
    int textHeight = num3 + num1;
    pBaseHeight = num3;
    pExtLead = num2;
    return textHeight;
  }

  internal new void HorScrollCheck()
  {
    int pY = 0;
    int num1 = 15;
    int terWinOrgX = this.e.TerWinOrgX;
    if (this.e.TerArg.WordWrap && this.e.TerWinOrgX == 0 && !this.HScrollAllowed() && (!this.e.TerArg.PageMode ? this.TerWrapWidth(this.e.CurLine, -1) : this.PageTextWidth()) <= this.e.TerWinWidth || !this.UseCaret() || !this.e.CaretEngaged || (this.e.TerFlags3 & 128 /*0x80*/) != 0 && this.e.TerWinOrgX == 0)
      return;
    int pX;
    if ((this.e.text[this.e.CurLine].flags2 & 32 /*0x20*/) != 0)
      this.GetCaretXY(this.e.CurLine, this.e.CurCol, pY, out pX, out pY);
    else
      pX = this.ColToUnits(this.e.CurCol, this.e.CurLine, 1024 /*0x0400*/);
    if (pX >= this.e.TerWinOrgX && pX <= this.e.TerWinOrgX + this.e.TerWinWidth)
      return;
    int num2 = this.e.TerWinWidth > 0 ? pX * 2 / this.e.TerWinWidth : 1;
    if (num2 == 0)
      num2 = 1;
    this.e.TerWinOrgX = (num2 - 1) * (this.e.TerWinWidth / 2);
    if (terWinOrgX == 0 && (this.e.text[this.e.CurLine].flags2 & 32 /*0x20*/) != 0 && (pX > this.e.TerWinWidth - num1 || this.e.CurCol <= 1) && this.e.TerArg.WordWrap && this.e.TerArg.PageMode)
    {
      int num3 = this.PageTextWidth() - this.e.TerWinWidth;
      if (num3 < 0)
        num3 = 0;
      if (pX >= num3 && pX <= num3 + this.e.TerWinWidth)
        this.e.TerWinOrgX = num3;
      ++this.e.TerWinOrgX;
    }
    this.SetTerWindowOrg();
    this.DeleteTextMap(true);
  }

  internal new bool HScrollAllowed()
  {
    if ((this.e.TerFlags & 2048 /*0x0800*/) != 0)
      return true;
    for (int index = 0; index < this.e.TotalPfmts; ++index)
    {
      if ((this.e.PfmtId[index].pflags & 16 /*0x10*/) != 0)
        return true;
    }
    return false;
  }

  internal new bool InitCaret()
  {
    if (this.e.UseWin)
    {
      if (!this.UseCaret() || this.e.InPrintPreview || !this.e.Focused)
      {
        if (this.e.CaretEnabled)
          this.TerDestroyCaret();
        this.e.CaretEnabled = false;
        this.e.CaretHidden = true;
        this.e.CaretHeight = 0;
        return true;
      }
      int row;
      int col;
      if (this.e.CaretEngaged)
      {
        row = this.e.CurLine;
        col = this.e.CurCol;
      }
      else
        this.AbsToRowCol(this.e.CaretPos, out row, out col, true, false);
      int units = this.LineToUnits(row);
      int TextHeight = this.GetRowHeight(row);
      int SpcBef;
      this.GetLineSpacing(row, TextHeight, out SpcBef, out tc.SkipInt, true);
      int y = units + SpcBef;
      if (y < this.e.TerWinOrgY)
      {
        TextHeight -= this.e.TerWinOrgY - y;
        y = this.e.TerWinOrgY;
      }
      if (TextHeight + y - this.e.TerWinOrgY > this.e.TerWinHeight)
        TextHeight = this.e.TerWinHeight - y + this.e.TerWinOrgY;
      if (TextHeight <= 0)
        TextHeight = this.e.TerFont[0].height;
      int num = this.frm.LineTextAngle2(row);
      bool flag = num == 0 || num == 180;
      if ((TextHeight != this.e.CaretHeight || this.e.CaretVert != flag) && !this.e.CaretHidden)
        this.TerDestroyCaret();
      this.e.CaretHeight = TextHeight;
      this.e.CaretVert = flag;
      this.e.CaretEnabled = true;
      if (this.IsCaretVisible(row, col))
      {
        int pX;
        int pY;
        this.GetCaretXY(row, col, y, out pX, out pY);
        if (pX <= this.e.TerRect.right)
          this.SetCaretPos(this.e.TerWinRect.left + pX - this.e.TerWinOrgX, this.e.TerWinRect.top + pY - this.e.TerWinOrgY);
      }
    }
    return true;
  }

  internal new bool InRotatedFrame(int x, int y, int FrameNo)
  {
    bool flag = false;
    int x1 = this.e.frame[FrameNo].x;
    int y1 = this.e.frame[FrameNo].y;
    int num1 = this.e.frame[FrameNo].width;
    int num2 = this.e.frame[FrameNo].height;
    int paraFrameId = this.e.frame[FrameNo].ParaFrameId;
    if (paraFrameId > 0 && (this.e.AllTextAngle != 0 || this.e.ParaFrame[paraFrameId].TextAngle != 0))
      flag = true;
    if (flag && this.GetFrameTextAngle(FrameNo) > 0 && (this.e.frame[FrameNo].flags1 & 2) == 0)
    {
      COp.RECT OurRect;
      this.SetRect(out OurRect, x1, y1, x1 + num1, y1 + num2);
      this.FrameRotateRect(ref OurRect, FrameNo);
      this.NormalizeRect(ref OurRect);
      x1 = OurRect.left;
      y1 = OurRect.top;
      num1 = OurRect.right - OurRect.left;
      num2 = OurRect.bottom - OurRect.top;
    }
    int scrY = (this.e.ParaFrame[paraFrameId].flags & 256 /*0x0100*/) == 0 || (this.e.TerOpFlags & 16384 /*0x4000*/) != 0 ? 0 : this.TwipsToScrY(60);
    return x >= x1 - scrY && x < x1 + num1 + scrY && y >= y1 - scrY && y < y1 + num2 + scrY;
  }

  internal new bool IsCaretVisible(int CaretLine, int CaretCol)
  {
    bool flag = true;
    if (!this.e.UseWin)
      return false;
    if (!this.e.CaretEngaged)
    {
      if (CaretLine < 0)
        this.AbsToRowCol(this.e.CaretPos, out CaretLine, out CaretCol, true, false);
      flag = false;
      int frame = this.frm.GetFrame(CaretLine);
      if (frame >= 0 && CaretLine >= this.e.frame[frame].ScrFirstLine && CaretLine <= this.e.frame[frame].ScrLastLine)
      {
        int units = this.ColToUnits(CaretCol, CaretLine, 1024 /*0x0400*/);
        if (units >= this.e.TerWinOrgX && units < this.e.TerWinOrgX + this.e.TerWinWidth)
          flag = true;
        if (this.e.ScrollBM != null)
        {
          int rowY = this.GetRowY(CaretLine);
          if (rowY < this.e.TerWinOrgY || rowY >= this.e.TerWinOrgY + this.e.TerWinHeight)
            flag = false;
        }
      }
    }
    if (!this.e.ShowProtectCaret & flag && (this.e.TerFont[this.GetEffectiveCfmt()].style & 512 /*0x0200*/) != 0)
      flag = false;
    if (flag)
    {
      if ((this.e.ParaFrame[this.e.text[CaretLine].fid].flags & 768 /*0x0300*/) != 0)
        flag = false;
      if (this.e.CurSID >= 0)
        flag = false;
    }
    if (this.e.HilightType != 0 && !this.e.DraggingText && (this.e.HilightBegRow != this.e.HilightEndRow || this.e.HilightBegCol != this.e.HilightEndCol))
      flag = false;
    if (flag && this.e.CaretHidden)
    {
      if (this.e.CaretVert)
        this.CreateCaret(this.e.hTerWnd, IntPtr.Zero, this.e.TerTextMet.tmAveCharWidth / 4, this.e.CaretHeight);
      else
        this.CreateCaret(this.e.hTerWnd, IntPtr.Zero, this.e.CaretHeight, this.e.TerTextMet.tmAveCharWidth / 4);
      this.ShowCaret(this.e.hTerWnd);
      this.e.CaretHidden = false;
    }
    if (!flag && !this.e.CaretHidden)
    {
      this.DestroyCaret();
      this.e.CaretHidden = true;
    }
    return flag;
  }

  internal new bool IsLineVisible(int lin)
  {
    int frame = this.frm.GetFrame(lin);
    return frame >= 0 && lin >= this.e.frame[frame].ScrFirstLine && lin <= this.e.frame[frame].ScrLastLine;
  }

  internal new bool IsTextPosVisible(int line, int col)
  {
    if (!this.e.TerArg.PageMode)
      return line >= this.e.BeginLine && line <= this.e.BeginLine + this.e.WinHeight;
    int frame;
    if ((frame = this.frm.GetFrame(line)) == -1 || line < this.e.frame[frame].ScrFirstLine || line > this.e.frame[frame].ScrLastLine)
      return false;
    int units = this.ColToUnits(col, line, 0);
    return units >= this.e.TerWinOrgX && units <= this.e.TerWinOrgX + this.e.TerWinWidth;
  }

  internal new bool JustifySpace(int CurFont) => !this.edit.HiddenText(CurFont);

  internal int LineSpaceAfter(int lin)
  {
    if ((this.e.text[lin].flags & 1) == 0)
      return 0;
    int pfmt1 = this.e.text[lin].pfmt;
    int num = this.e.PfmtId[pfmt1].SpaceAfter;
    if ((this.e.PfmtId[pfmt1].flags & 262144 /*0x040000*/) != 0)
    {
      num = 270;
      if (lin >= this.e.TotalLines - 1)
      {
        num = 0;
      }
      else
      {
        int pfmt2 = this.e.text[lin + 1].pfmt;
        if (num > 0 && this.e.text[lin].cid > 0 && this.LineInfo(lin, 48 /*0x30*/))
          num = 0;
        if (num > 0 && this.e.text[lin].fid > 0 && this.e.text[lin].fid != this.e.text[lin + 1].fid)
          num = 0;
        if (num > 0 && this.e.PfmtId[pfmt1].BltId > 0 && this.e.PfmtId[pfmt1].BltId == this.e.PfmtId[pfmt2].BltId)
          num = 0;
      }
    }
    return num;
  }

  internal new int LineToUnits(int line)
  {
    if (line != -1)
      return this.GetRowY(line);
    return this.e.TerArg.BorderMargin ? this.e.TerWinOrgY + this.e.TerWinRect.bottom - this.e.TerWinRect.top + this.TwipsToOrigScrY(75) : this.e.TerWinOrgY + this.e.TerWinRect.bottom - this.e.TerWinRect.top;
  }

  internal bool NormalizeRect(ref COp.RECT rect)
  {
    if (rect.right < rect.left)
      this.SwapInts(ref rect.left, ref rect.right);
    if (rect.bottom < rect.top)
      this.SwapInts(ref rect.bottom, ref rect.top);
    return true;
  }

  internal new bool OurSetCaretPos()
  {
    int col = 0;
    int row = -1;
    if (this.e.UseWin)
    {
      int curCfmt;
      if (this.e.CaretEnabled)
      {
        this.e.CaretPositioned = true;
        if (this.e.CaretEngaged)
        {
          row = this.e.CurLine;
          col = this.e.CurCol;
        }
        else
          this.AbsToRowCol(this.e.CaretPos, out row, out col, true, false);
        int units = this.LineToUnits(row);
        int TextHeight = this.GetRowHeight(row);
        int SpcBef;
        this.GetLineSpacing(row, TextHeight, out SpcBef, out tc.SkipInt, true);
        int y = units + SpcBef;
        if (y < this.e.TerWinOrgY)
        {
          TextHeight -= this.e.TerWinOrgY - y;
          y = this.e.TerWinOrgY;
        }
        if (TextHeight + y - this.e.TerWinOrgY > this.e.TerWinHeight)
          TextHeight = this.e.TerWinHeight - y + this.e.TerWinOrgY;
        bool flag = this.LineTextAngle(row) == 0;
        if (this.e.CaretHeight != TextHeight || this.e.CaretVert != flag)
          this.InitCaret();
        if (this.IsCaretVisible(row, col))
        {
          int pX;
          int pY;
          this.GetCaretXY(row, col, y, out pX, out pY);
          this.SetCaretPos(this.e.TerWinRect.left + pX - this.e.TerWinOrgX, this.e.TerWinRect.top + pY - this.e.TerWinOrgY);
        }
        curCfmt = this.GetCurCfmt(row, col);
        if ((this.e.TerFont[curCfmt].style & 128 /*0x80*/) != 0 && this.e.PictureClicked)
        {
          int paraFid = this.e.TerFont[curCfmt].ParaFID;
          if (paraFid > 0 && (this.e.ParaFrame[paraFid].flags & 256 /*0x0100*/) != 0)
          {
            int dispFrame = this.e.TerFont[curCfmt].DispFrame;
            if (dispFrame >= 0)
              this.ShowFrameDragObjects(dispFrame, paraFid);
          }
          else if ((this.e.TerFlags & 16777216 /*0x01000000*/) == 0)
          {
            if (this.e.HilightType == 2)
              this.ShowPictureDragObjects(curCfmt);
            else
              this.e.PictureClicked = false;
          }
        }
        if (this.e.FrameClicked)
        {
          int frame = this.frm.GetFrame(row);
          if (frame >= 0 && this.e.frame[frame].ParaFrameId > 0)
            this.ShowFrameDragObjects(this.e.frame[frame].BoxFrame, -1);
        }
      }
      else
      {
        curCfmt = this.GetCurCfmt(this.e.CurLine, this.e.CurCol);
        if ((this.e.TerFont[curCfmt].style & 128 /*0x80*/) != 0 && this.e.PictureClicked && (this.e.TerFlags & 16777216 /*0x01000000*/) == 0)
          this.ShowPictureDragObjects(curCfmt);
      }
      this.e.InFootnote = false;
      if (row == -1)
      {
        if (this.e.CaretEngaged)
        {
          row = this.e.CurLine;
          col = this.e.CurCol;
        }
        else
          this.AbsToRowCol(this.e.CaretPos, out row, out col, true, false);
      }
      if (curCfmt == -1)
        curCfmt = this.GetCurCfmt(row, col);
      if ((this.e.TerFont[curCfmt].style & 6144) != 0)
        this.e.InFootnote = true;
    }
    return true;
  }

  internal new bool PosAfterHiddenText()
  {
    while ((this.e.TerFont[this.GetCurCfmt(this.e.CurLine, this.e.CurCol)].style & 64 /*0x40*/) != 0)
    {
      this.e.CursDirection = 2;
      if (this.e.CurCol + 1 < this.e.text[this.e.CurLine].len)
        ++this.e.CurCol;
      else if (this.e.CurLine + 1 < this.e.TotalLines)
      {
        ++this.e.CurLine;
        this.e.CurCol = 0;
      }
      else
        break;
    }
    return true;
  }

  internal new int PosToCol(int x, int y, int line)
  {
    x = this.GetFlatX(x, y, line);
    return this.UnitsToCol(x, line);
  }

  /// <summary>Преобразовать строку столбец в абсолютные координаты в строке</summary>
  /// <param name="row">Строка</param>
  /// <param name="col">Столбец</param>
  /// <param name="internalPos">Преобразовать в позицию внутреннего представления строки с учётом замен.
  /// Иначе в позицию в оригинальной строке до замен формул и спецсимволов</param>
  /// <param name="scanAllChars">Принудительно проверять весь текст,
  /// для поиска переносов строки, которые находятся не в конце строки</param>
  /// <returns></returns>
  internal int RowColToAbs(int row, int col, bool internalPos = true, bool scanAllChars = false)
  {
    if (this.e.TotalLines == 0)
      return 0;
    int num1 = 0;
    bool flag1 = (this.e.TerFlags4 & 1) != 0;
    int num2 = 0;
    if (!this.e.TerArg.WordWrap)
      num2 = 2;
    if (row < 0)
      row = 0;
    if (row >= this.e.TotalLines)
      row = this.e.TotalLines - 1;
    if (row < 0)
      row = 0;
    for (int LineNo = 0; LineNo < row; ++LineNo)
    {
      if (this.e.text[LineNo] != null)
      {
        int len = this.e.text[LineNo].len;
        if ((this.e.text[LineNo].flags2 & 512 /*0x0200*/) != 0 && len > 0 && this.e.text[LineNo].txt[len - 1] == '\u0006')
          --len;
        int num3 = len;
        int num4 = 0;
        int num5 = 0;
        bool flag2 = false;
        if (!internalPos && this.e.HasTextReplaces && this.e.text[LineNo].tag != null && this.e.text[LineNo].tag.Length != 0)
        {
          for (int index1 = 0; index1 < this.e.text[LineNo].len; ++index1)
          {
            int index2 = (int) this.e.text[LineNo].tag[index1];
            if (index2 != 0 && (this.e.CharTag[index2].type == 78 || this.e.CharTag[index2].type == 79 || this.e.CharTag[index2].type == 80 /*0x50*/))
            {
              flag2 = index1 == this.e.text[LineNo].len - 1;
              string auxText = this.e.CharTag[index2].AuxText;
              if (auxText != null)
                num4 += auxText.Length - 1;
              else
                --num4;
              if (flag1 && this.e.text[LineNo].txt[index1] == '\u0015')
                --num4;
            }
            else if (flag1 && index1 < this.e.text[LineNo].len - 1 && this.e.text[LineNo].txt[index1] == '\u0015')
              ++num5;
          }
        }
        else if (scanAllChars & flag1)
        {
          for (int index = 0; index < this.e.text[LineNo].len - 1; ++index)
          {
            if (this.e.text[LineNo].txt[index] == '\u0015')
              ++num5;
          }
        }
        num1 += num3 + num4 + num2 + num5;
        if (flag1 && !flag2 && (len > 0 && this.e.text[LineNo].txt[len - 1] == '\u0015' || (this.e.text[LineNo].flags & 1966209) != 0 || this.e.text[LineNo].len == 1 && this.LineInfo(LineNo, 32 /*0x20*/)))
          ++num1;
      }
    }
    if (this.e.text[row] != null && col > this.e.text[row].len && col > 0)
      col = this.e.text[row].len - 1 + num2;
    if (col < 0)
      col = 0;
    return num1 + col;
  }

  internal new bool RtlRect(ref COp.RECT pRect, int FrameNo, tc.StrLineSeg pSeg)
  {
    ref tc.StrFrame local1 = ref this.e.frame[FrameNo];
    int frameTextAngle = this.GetFrameTextAngle(FrameNo);
    ref tc.StrFrame local2 = ref this.e.frame[FrameNo];
    ref tc.StrFrame local3 = ref this.e.frame[FrameNo];
    if (this.e.InPrinting)
    {
      int prtLeftMarg = this.e.PrtLeftMarg;
    }
    ref tc.StrFrame local4 = ref this.e.frame[FrameNo];
    ref tc.StrFrame local5 = ref this.e.frame[FrameNo];
    ref tc.StrFrame local6 = ref this.e.frame[FrameNo];
    if (frameTextAngle != 0)
    {
      ref tc.StrFrame local7 = ref this.e.frame[FrameNo];
      ref tc.StrFrame local8 = ref this.e.frame[FrameNo];
      ref tc.StrFrame local9 = ref this.e.frame[FrameNo];
    }
    int pVal1 = this.RtlX(pRect.left, 0, FrameNo, pSeg);
    int pVal2 = this.RtlX(pRect.right, 0, FrameNo, pSeg);
    if (pVal2 < pVal1)
      this.SwapInts(ref pVal1, ref pVal2);
    pRect.left = pVal1;
    pRect.right = pVal2;
    return true;
  }

  internal new int RtlX(int x, int TextRectWidth, int FrameNo, tc.StrLineSeg pSeg)
  {
    if (FrameNo < 0 || FrameNo >= this.e.TotalFrames)
      return x;
    int boxFrame = this.e.frame[FrameNo].BoxFrame;
    int paraFrameId = this.e.frame[FrameNo].ParaFrameId;
    int num1 = this.e.frame[FrameNo].x + (this.e.InPrinting ? this.e.PrtLeftMarg : 0);
    int num2 = num1 + this.e.frame[FrameNo].SpaceLeft;
    int num3 = this.e.frame[FrameNo].width;
    if (num3 == 9999)
      num3 = this.e.TerWinWidth;
    int num4 = num3 - this.e.frame[FrameNo].SpaceLeft - this.e.frame[FrameNo].SpaceRight;
    if (this.e.TerArg.FittedView && num4 >= this.e.TerWinWidth)
      num4 = this.e.TerWinWidth - 1;
    if (this.e.AllTextAngle != 0 || this.e.ParaFrame[paraFrameId].TextAngle != 0)
      num4 = this.e.frame[boxFrame].height - this.e.frame[boxFrame].SpaceTop - this.e.frame[boxFrame].SpaceBot;
    if (pSeg.count != 0 && !pSeg.ParaRtl)
    {
      if (!pSeg.rtl)
        return x;
      tc.StrLineSeg[] pFirstSeg = pSeg.pFirstSeg;
      int idx = pSeg.idx;
      int totalLineSeg = pSeg.TotalLineSeg;
      int index1 = idx;
      while (index1 >= 0 && pFirstSeg[index1].rtl)
        --index1;
      int index2 = index1 + 1;
      int index3 = idx;
      while (index3 < totalLineSeg && pFirstSeg[index3].rtl)
        ++index3;
      int index4 = index3 - 1;
      int x1 = pFirstSeg[index2].x;
      int num5 = pFirstSeg[index4].x + pFirstSeg[index4].width - x1;
      int num6 = x1 + num1;
      int num7 = x - num6;
      return num6 + num5 - num7 - TextRectWidth;
    }
    if (pSeg.count == 0 || pSeg.rtl)
      return num2 + (num4 - (x - num2)) - TextRectWidth;
    int num8 = pSeg.x + num1;
    int num9 = x - num8;
    return num2 + (num4 - (num8 - num2)) - pSeg.width + num9;
  }

  internal new int ScrLineHeight(int lin, bool AddSpcBef)
  {
    if (lin >= this.e.TotalLines)
      lin = this.e.TotalLines - 1;
    if (this.e.TerArg.PageMode)
    {
      int scrHt = this.e.text[lin].ScrHt;
      if (AddSpcBef)
        scrHt += this.frm.GetFrmSpcBef(lin, true) + this.tbl.GetTblSpcBef(lin, true);
      return scrHt;
    }
    int lineHeight = this.GetLineHeight(lin, out int _, out tc.SkipInt);
    int SpcBef;
    int SpcAft;
    this.GetLineSpacing(lin, lineHeight, out SpcBef, out SpcAft, true);
    return lineHeight + (SpcBef + SpcAft);
  }

  internal new bool SetDragCaret(int lParam)
  {
    this.TerMousePos(lParam, false);
    if (!this.CanInsert(this.e.MouseLine, this.e.MouseCol))
    {
      this.MessageBeep(0);
      return true;
    }
    this.e.CurLine = this.e.MouseLine;
    this.e.CurCol = this.e.MouseCol;
    if (!this.e.CaretEnabled)
      this.InitCaret();
    this.OurSetCaretPos();
    return true;
  }

  internal new bool SetScrollBars()
  {
    if (tc.DebugMode)
      this.misc.dm(nameof (SetScrollBars));
    if (this.e.UseWin)
    {
      int num1;
      if (this.e.TerArg.ShowVerBar)
      {
        num1 = 0;
        int num2;
        int num3;
        if (this.e.TerArg.PageMode)
        {
          int x = this.SumPageScrHeight(0, this.e.FirstFramePage) + this.e.TerWinOrgY;
          int z1;
          int num4 = z1 = this.SumPageScrHeight(0, this.e.TotalPages);
          int z2 = num4 - this.e.TerWinHeight;
          if (z1 < this.e.TerWinHeight && this.e.TerWinOrgY > 0)
            this.PostMessage(this.e.hTerWnd, 277, 2, 0);
          if (z1 == 0)
          {
            num2 = 1000;
          }
          else
          {
            num2 = this.MulDiv(this.e.TerWinHeight, 1000, z1);
            if (num2 < this.e.MinThumbHt)
              num2 = this.e.MinThumbHt;
            if (num2 > 1000)
              num2 = 1000;
          }
          if (num2 == 1000 && this.e.TerWinOrgY != 0)
            --num2;
          int y = 1000 - num2;
          num3 = z2 > 0 ? (x <= 0 || x + this.e.TerWinHeight < num4 ? this.MulDiv(x, y, z2) : y) : 0;
        }
        else
        {
          int num5 = 0;
          int num6 = 0;
          int num7 = this.LastScrollBeginLine();
          for (int lin = 0; lin < num7; ++lin)
          {
            int num8 = this.ScrLineHeight(lin, false);
            num6 += num8;
            if (lin < this.e.BeginLine)
              num5 += num8;
          }
          if (this.e.WinYOffsetLine != -1)
            num5 += this.e.WinYOffset;
          if (this.DocFitsInWindow() && this.e.BeginLine > 0)
          {
            this.e.BeginLine = 0;
            this.e.CurRow = this.e.CurLine;
            this.e.WrapFlag = 0;
            this.PaintTer();
          }
          if (num6 == 0)
          {
            num2 = 1000;
          }
          else
          {
            num2 = this.e.TerWinHeight * 1000 / (num6 + this.e.TerWinHeight);
            if (num2 < this.e.MinThumbHt)
              num2 = this.e.MinThumbHt;
            if (num2 > 1000)
              num2 = 1000;
          }
          int num9 = 1000 - num2;
          num3 = num6 == 0 || this.e.BeginLine == 0 ? 0 : num5 * num9 / num6;
          if (num3 > num9)
            num3 = num9;
        }
        if (num3 != this.e.VerScrollPos || num2 != this.e.VerThumbSize)
        {
          COp.SCROLLINFO lpsi = new COp.SCROLLINFO();
          if (num3 > 1000 - num2)
            num3 = 1000 - num2;
          lpsi.cbSize = 28;
          lpsi.fMask = 12;
          if (num2 > 0)
            lpsi.fMask |= 2;
          lpsi.nPage = num2 + 1;
          int num10;
          this.e.VerScrollPos = num10 = num3;
          lpsi.nPos = num10;
          this.SetScrollInfo(this.e.hTerWnd, 1, ref lpsi, true);
          this.e.VerThumbSize = num2;
        }
      }
      if (this.e.TerArg.ShowHorBar)
      {
        num1 = 0;
        int num11;
        int num12;
        if (this.e.TerArg.WordWrap && (this.e.TerArg.PageMode || !this.HScrollAllowed()))
        {
          num11 = !this.e.TerArg.PageMode ? this.TerWrapWidth(this.e.CurLine, -1) : this.PageTextWidth();
          if (num11 == 0)
          {
            num12 = 1000;
          }
          else
          {
            if (num11 <= this.e.TerWinWidth && this.e.TerWinOrgX > 0 && this.ColToUnits(this.e.CurCol, this.e.CurLine, 1024 /*0x0400*/) < this.e.TerWinWidth && this.LineTextAngle(this.e.CurLine) == 0)
            {
              this.e.TerWinOrgX = 0;
              this.SetTerWindowOrg();
              this.e.WrapFlag = 0;
              this.PaintTer();
            }
            num12 = this.e.TerWinWidth * 1000 / num11;
            if (num12 < this.e.MinThumbHt)
              num12 = this.e.MinThumbHt;
            if (num12 > 1000)
              num12 = 1000;
          }
        }
        else
        {
          num11 = 0;
          for (int beginLine = this.e.BeginLine; beginLine <= this.e.frame[0].ScrLastLine; ++beginLine)
          {
            int lineWidth = this.GetLineWidth(beginLine, true, true);
            if (lineWidth > num11)
              num11 = lineWidth;
          }
          if (num11 <= this.e.TerWinWidth && this.e.TerWinOrgX > 0)
          {
            this.e.TerWinOrgX = 0;
            this.SetTerWindowOrg();
            this.e.WrapFlag = 0;
            this.PaintTer();
          }
          if (num11 == 0)
          {
            num12 = 1000;
          }
          else
          {
            num12 = this.e.TerWinWidth * 1000 / num11;
            if (num12 < this.e.MinThumbHt)
              num12 = this.e.MinThumbHt;
            if (num12 > 1000)
              num12 = 1000;
          }
        }
        int num13 = 1000 - num12;
        int num14 = num11 - this.e.TerWinWidth;
        if (num14 <= 0)
          num14 = 1;
        int num15 = this.e.TerWinOrgX * num13 / num14;
        if (this.e.TerWinOrgX >= num11 - this.e.TerWinWidth)
          num15 = num13;
        if (this.e.TerWinOrgX == 0)
          num15 = 0;
        if (num15 != this.e.HorScrollPos || num12 != this.e.HorThumbSize)
        {
          COp.SCROLLINFO lpsi = new COp.SCROLLINFO();
          lpsi.fMask = 12;
          if (num12 > 0)
            lpsi.fMask |= 2;
          lpsi.nPage = num12 + 1;
          int num16;
          this.e.HorScrollPos = num16 = num15;
          lpsi.nPos = num16;
          this.SetScrollInfo(this.e.hTerWnd, 0, ref lpsi, true);
          this.e.HorThumbSize = num12;
        }
      }
    }
    return true;
  }

  internal bool SetTerCursorPos(int NewLine, int NewCol, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int curLine = this.e.CurLine;
    int curCol = this.e.CurCol;
    if (this.e.TotalLines == 0)
    {
      this.e.CurLine = 0;
      this.e.CurCol = 0;
      if (curLine != this.e.CurLine || curCol != this.e.CurCol)
        this.e.OnCursorPosChanged();
      return true;
    }
    if (NewCol < 0)
      this.AbsToRowCol(NewLine, out NewLine, out NewCol, true, false);
    if (NewLine >= this.e.TotalLines || NewLine < 0)
    {
      if (this.e.TotalLines > 0)
      {
        NewLine = this.e.TotalLines - 1;
        NewCol = this.e.text[NewLine].len;
      }
      else
        NewLine = 0;
      if (this.e.TerArg.WordWrap)
        --NewCol;
      if (NewCol < 0)
        NewCol = 0;
    }
    if (this.e.text[NewLine] != null && NewCol > this.e.text[NewLine].len)
      NewCol = this.e.text[NewLine].len;
    if (NewCol < 0)
      NewCol = 0;
    if (!this.e.CaretEngaged)
      this.EngageCaret(-1);
    this.e.CurLine = NewLine;
    this.e.CurCol = NewCol;
    int num = this.e.TerWinHeight / this.e.TerFont[0].height;
    if (this.e.CurLine - this.e.BeginLine >= num || this.e.CurLine - this.e.BeginLine < 0)
    {
      this.e.BeginLine = this.e.CurLine - num / 2;
      if (this.e.BeginLine < 0)
        this.e.BeginLine = 0;
    }
    this.e.CurRow = this.e.CurLine - this.e.BeginLine;
    if (repaint)
    {
      this.PaintTer();
      if (this.e.UseWin)
        this.PostMessage(this.e.hTerWnd, 1034, 0, 0);
    }
    if (curLine != this.e.CurLine || curCol != this.e.CurCol)
      this.e.OnCursorPosChanged();
    return true;
  }

  /// <summary>Преобразовать абсолютную (сквозную) позицию в строке текста в строку и столбец текста</summary>
  /// <param name="abs">Абсолютная позиция в строке текста</param>
  /// <param name="row">Строка</param>
  /// <param name="col">Столбец</param>
  /// <param name="internalPos">Преобразовать в позицию внутреннего представления строки с учётом замен.
  /// Иначе в позицию в оригинальной строке до замен формул и спецсимволов</param>
  /// <param name="scanAllChars">Принудительно проверять весь текст,
  /// для поиска переносов строки, которые находятся не в конце строки</param>
  internal void TerAbsToRowCol(
    int abs,
    out int row,
    out int col,
    bool internalPos = true,
    bool scanAllChars = false)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    row = 0;
    col = 0;
    this.AbsToRowCol(abs, out row, out col, internalPos, scanAllChars);
  }

  internal new bool TerDestroyCaret()
  {
    if (this.e.UseWin)
    {
      if (!this.e.CaretHidden)
        this.DestroyCaret();
      this.e.CaretEnabled = false;
      this.e.CaretHidden = true;
    }
    return true;
  }

  internal bool TerEngageCaret(bool AtCursorLoc)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.CaretEngaged)
      return this.EngageCaret(AtCursorLoc ? -1 : 0);
    if (!this.e.CaretEnabled && this.UseCaret())
      this.InitCaret();
    return true;
  }

  internal int TerGetCaretPos()
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    return this.e.CaretEngaged ? this.RowColToAbs(this.e.CurLine, this.e.CurCol, true, false) : this.e.CaretPos;
  }

  internal int TerGetLineWidth(int LineNo)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    return this.GetLineWidth(LineNo, false, false);
  }

  internal int TerGetVisibleCol(int line, int col)
  {
    int num = -1;
    bool flag = false;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (line < 0)
      line = this.e.CurLine;
    if (col < 0)
      col = this.e.CurCol;
    if (line >= this.e.TotalLines)
      return -1;
    ushort[] numArray = this.OpenCfmt(line);
    int len = this.e.text[line].len;
    if (col >= len)
      col = len - 1;
    int visibleCol = 0;
    for (int index = 0; index < col; ++index)
    {
      if ((int) numArray[index] != num)
        flag = !this.edit.HiddenText((int) numArray[index]);
      num = (int) numArray[index];
      if (flag)
        ++visibleCol;
    }
    this.CloseCfmt(line);
    return visibleCol;
  }

  internal new bool TerJump()
  {
    if (!this.CallDialogBox((Form) new terdlg_jump(this.e)))
      return false;
    int dlgResult = this.e.DlgResult;
    if (dlgResult < 0)
      return false;
    this.TerPosLine(dlgResult);
    return true;
  }

  internal new bool TerMousePos(int lParam, bool SetPage)
  {
    Point pt = new Point();
    this.e.MouseOverShoot = ' ';
    this.e.MouseOverShootDist = 0;
    this.e.MouseLine = this.e.CurLine;
    this.e.MouseCol = this.e.CurCol;
    this.e.RulerClicked = false;
    this.e.CurDragObj = -1;
    this.e.TerOpFlags &= -3;
    this.e.TerOpFlags &= -134217729;
    int num1 = this.e.MouseX = (int) (short) COp.LOWORD(lParam);
    int num2 = this.e.MouseY = (int) (short) COp.HIWORD(lParam);
    if (this.e.TotalLines == 0)
      return true;
    if (!this.e.StretchHilight && !this.e.TerArg.ReadOnly)
    {
      if (this.e.TerArg.ruler && num1 >= this.e.RulerRect.left && num1 <= this.e.RulerRect.right && num2 >= this.e.RulerRect.top && num2 <= this.e.RulerRect.bottom)
        this.e.RulerClicked = true;
      pt.X = num1 - this.e.TerWinRect.left + this.e.TerWinOrgX;
      pt.Y = num2 - this.e.TerWinRect.top + this.e.TerWinOrgY;
      int num3;
      int HotSpot;
      this.GetHotSpotHit(pt, out num3, out HotSpot);
      this.e.CurDragObj = num3;
      this.e.CurHotSpot = HotSpot;
    }
    if (num1 < this.e.TerWinRect.left)
    {
      this.e.MouseOverShoot = 'L';
      this.e.MouseOverShootDist = this.e.TerWinRect.left - num1;
    }
    if (num1 > this.e.TerWinRect.right)
    {
      this.e.MouseOverShoot = 'R';
      this.e.MouseOverShootDist = num1 - this.e.TerWinRect.right;
    }
    if (num2 < this.e.TerWinRect.top)
    {
      this.e.MouseOverShoot = 'T';
      this.e.MouseOverShootDist = this.e.TerWinRect.top - num2;
    }
    if (num2 > this.e.TerWinRect.bottom)
    {
      this.e.MouseOverShoot = 'B';
      this.e.MouseOverShootDist = num2 - this.e.TerWinRect.bottom;
    }
    if (num1 < this.e.TerWinRect.left)
      num1 = this.e.TerWinRect.left;
    int x = num1 - this.e.TerWinRect.left + this.e.TerWinOrgX;
    int y = num2 - this.e.TerWinRect.top + this.e.TerWinOrgY;
    this.e.TerOpFlags |= 4194304 /*0x400000*/;
    this.e.MouseLine = this.UnitsToLine(x, y);
    this.e.TerOpFlags &= -4194305;
    if (this.e.MouseLine >= this.e.TotalLines)
      this.e.MouseLine = this.e.TotalLines - 1;
    int frame1;
    if (this.e.MouseOverShoot == ' ' && this.e.TerArg.PageMode && this.e.text[this.e.MouseLine].cid > 0 && (this.e.MouseLine == 0 || this.e.text[this.e.MouseLine - 1].cid != this.e.text[this.e.MouseLine].cid && this.IsFirstTableRow(this.e.cell[this.e.text[this.e.MouseLine].cid].row)) && (frame1 = this.frm.GetFrame(this.e.MouseLine)) > 0 && x >= this.e.frame[frame1].x && x <= this.e.frame[frame1].x + this.e.frame[frame1].width && Math.Abs(this.e.frame[frame1].y - y) < this.TwipsToScrX(60))
    {
      int cid = this.e.text[this.e.MouseLine].cid;
      if (!this.e.HtmlMode || (this.e.cell[cid].border & 1) != 0)
        this.e.TerOpFlags |= 2;
    }
    if (SetPage && this.e.TerArg.PageMode)
    {
      if (this.e.CurPage == this.e.FirstFramePage && y >= this.e.FirstPageHeight && this.e.CurPage + 1 < this.e.TotalPages)
        ++this.e.CurPage;
      else if (this.e.CurPage == this.e.FirstFramePage + 1 && y < this.e.FirstPageHeight && this.e.CurPage > 0)
        --this.e.CurPage;
    }
    int frame2;
    if ((frame2 = this.frm.GetFrame(this.e.MouseLine)) >= 0 && this.e.frame[frame2].ScrLastLine >= 0 && this.e.MouseLine > this.e.frame[frame2].ScrLastLine && this.e.MouseOverShoot == ' ')
      this.e.MouseOverShoot = 'B';
    int flatX = this.GetFlatX(x, y, this.e.MouseLine);
    if (this.e.MouseLine >= this.e.BeginLine && this.e.MouseLine - this.e.BeginLine < this.e.WinHeight && flatX < this.GetRowX(this.e.MouseLine))
    {
      this.e.MouseCol = -1;
      this.e.TerOpFlags |= 134217728 /*0x08000000*/;
    }
    else if (this.e.MousePictFrame >= 0 && this.e.MousePictFrame < this.e.TotalFrames && this.e.frame[this.e.MousePictFrame].ParaFrameId > 0)
    {
      int pict = this.e.ParaFrame[this.e.frame[this.e.MousePictFrame].ParaFrameId].pict;
      ushort[] numArray = this.OpenCfmt(this.e.MouseLine);
      int len = this.e.text[this.e.MouseLine].len;
      int index = 0;
      while (index < len && (int) numArray[index] != pict)
        ++index;
      if (index < len)
        this.e.MouseCol = index;
      else
        this.e.MouseCol = 0;
    }
    else
      this.e.MouseCol = this.UnitsToCol(flatX, this.e.MouseLine);
    if (this.e.MouseCol >= 0)
    {
      int curCfmt = this.GetCurCfmt(this.e.MouseLine, this.e.MouseCol);
      if ((this.e.TerFont[curCfmt].style & 128 /*0x80*/) == 0 && !this.IsHypertext(curCfmt))
      {
        int units = this.ColToUnits(this.e.MouseCol, this.e.MouseLine, 1);
        if (flatX > units)
          ++this.e.MouseCol;
      }
    }
    if (this.e.MouseCol >= this.e.text[this.e.MouseLine].len)
    {
      this.e.MouseCol = this.e.text[this.e.MouseLine].len;
      if (this.e.TerArg.WordWrap)
        --this.e.MouseCol;
    }
    if (this.e.MouseCol < 0)
      this.e.MouseCol = 0;
    return true;
  }

  internal bool TerPixToTextPos(int rel, int x, int y, out int pLine, ref int pCol)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int num1;
    pCol = num1 = 0;
    pLine = num1;
    if (rel != 0 && rel != 1 && rel != 2)
      return false;
    if (rel == 0)
    {
      Point client = this.e.PointToClient(new Point(x, y));
      x = client.X;
      y = client.Y;
    }
    if (rel == 1 || rel == 0)
    {
      x -= this.e.TerWinRect.left;
      y -= this.e.TerWinRect.top;
    }
    x += this.e.TerWinOrgX;
    y += this.e.TerWinOrgY;
    int terOpFlags2 = this.e.TerOpFlags2;
    this.e.TerOpFlags2 |= 512 /*0x0200*/;
    int num2 = this.UnitsToLine(x, y);
    this.e.TerOpFlags2 = terOpFlags2;
    int col = this.PosToCol(x, y, num2);
    if (pCol == -1)
    {
      num2 = this.RowColToAbs(num2, col, true, false);
      col = -1;
    }
    pLine = num2;
    pCol = col;
    return true;
  }

  internal bool TerPosBodyText(int sect, int pos, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (pos == 0 || pos == 1)
    {
      int num = 0;
      int NewLine = 0;
      while (num < sect)
      {
        for (; NewLine < this.e.TotalLines; ++NewLine)
        {
          if (this.e.text[NewLine].tabw != null && (this.e.text[NewLine].tabw.type & 2) != 0)
          {
            ++num;
            ++NewLine;
            break;
          }
        }
        if (NewLine == this.e.TotalLines)
          break;
      }
      if (NewLine != this.e.TotalLines)
      {
        while (NewLine < this.e.TotalLines && (this.e.PfmtId[this.e.text[NewLine].pfmt].flags & 12288 /*0x3000*/) != 0)
          ++NewLine;
        if (NewLine == this.e.TotalLines)
          return false;
        if (pos == 0)
        {
          this.SetTerCursorPos(NewLine, 0, repaint);
          return true;
        }
        while (NewLine < this.e.TotalLines && (this.e.text[NewLine].tabw == null || (this.e.text[NewLine].tabw.type & 2) == 0))
          ++NewLine;
        if (NewLine == this.e.TotalLines)
          --NewLine;
        if (NewLine >= 0)
        {
          int NewCol = this.e.text[NewLine].len - 1;
          if (NewCol < 0)
            NewCol = 0;
          this.SetTerCursorPos(NewLine, NewCol, repaint);
          return true;
        }
      }
    }
    return false;
  }

  internal new bool TerPosLine(int GotoLine)
  {
    if (GotoLine > this.e.TotalLines)
      GotoLine = this.e.TotalLines;
    if (GotoLine < 1)
      GotoLine = 1;
    this.e.CurLine = GotoLine - 1;
    if (this.e.CurLine - this.e.BeginLine >= this.e.WinHeight || this.e.CurLine - this.e.BeginLine < 0)
    {
      this.e.BeginLine = this.e.CurLine - this.e.WinHeight / 2;
      if (this.e.BeginLine < 0)
        this.e.BeginLine = 0;
    }
    this.e.CurRow = this.e.CurLine - this.e.BeginLine;
    if (this.e.TerArg.PageMode)
    {
      int curPage1 = this.e.CurPage;
      this.e.CurPage = this.GetCurPage(this.e.CurLine);
      int curPage2 = this.e.CurPage;
      if (curPage1 != curPage2)
        this.RefreshFrames(true);
      if (this.e.CurCol >= this.e.text[this.e.CurLine].len)
        this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
      if (this.e.CurCol < 0)
        this.e.CurCol = 0;
      int units = this.LineToUnits(this.e.CurLine);
      int terWinOrgY = this.e.TerWinOrgY;
      if (units < this.e.TerWinOrgY || units >= this.e.TerWinOrgY + this.e.TerWinHeight)
        this.e.TerWinOrgY = units;
      if (this.e.TerWinOrgY + this.e.TerWinHeight > this.e.CurPageHeight)
        this.e.TerWinOrgY = this.e.CurPageHeight - this.e.TerWinHeight;
      if (this.e.TerWinOrgY < 0)
        this.e.TerWinOrgY = 0;
      if (this.e.TerWinOrgY != terWinOrgY)
        this.SetTerWindowOrg();
    }
    this.PaintTer();
    if (this.e.TerArg.ShowHorBar || this.e.TerArg.ShowVerBar)
      this.SetScrollBars();
    this.InitCaret();
    return true;
  }

  internal bool TerPosLineAtTop(int line, bool top)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    this.e.CurLine = this.e.BeginLine = line;
    this.e.CurRow = 0;
    this.e.CurCol = 0;
    if (!this.e.TerArg.PageMode)
    {
      if (!top)
      {
        this.e.CurRow = this.e.WinHeight / 2;
        this.e.BeginLine = this.e.CurLine - this.e.CurRow;
        if (this.e.BeginLine < 0)
          this.e.BeginLine = 0;
      }
      this.PaintTer();
      return true;
    }
    this.e.CurPage = this.GetCurPage(this.e.CurLine);
    this.RefreshFrames(true);
    int num = this.LineToUnits(this.e.CurLine);
    if (!top)
    {
      num -= this.e.TerWinHeight / 2;
      if (num < 0)
        num = 0;
    }
    int firstPageHeight = this.e.CurPage != this.e.FirstFramePage + 1 ? 0 : this.e.FirstPageHeight;
    if (this.e.HtmlMode & top && this.e.CurPage == this.e.TotalPages - 1 && num - firstPageHeight + this.e.TerWinHeight > this.GetScrPageHt(this.e.CurPage))
    {
      num = this.GetScrPageHt(this.e.CurPage) - this.e.TerWinHeight + firstPageHeight;
      if (num < 0)
        num = 0;
    }
    if (num != this.e.TerWinOrgY)
    {
      this.e.TerWinOrgY = num;
      this.SetTerWindowOrg();
    }
    this.PaintTer();
    return true;
  }

  /// <summary>Преобразовать строку столбец в абсолютные координаты в строке</summary>
  /// <param name="row">Строка</param>
  /// <param name="col">Столбец</param>
  /// <param name="internalPos">Преобразовать в позицию внутреннего представления строки с учётом замен.
  /// Иначе в позицию в оригинальной строке до замен формул и спецсимволов</param>
  /// <param name="scanAllChars">Принудительно проверять весь текст,
  /// для поиска переносов строки, которые находятся не в конце строки</param>
  /// <returns></returns>
  internal int TerRowColToAbs(int row, int col, bool internalPos = true, bool scanAllChars = false)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    return this.RowColToAbs(row, col, internalPos, scanAllChars);
  }

  internal int TerScrLineHeight(int line)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    return line < 0 || line >= this.e.TotalLines ? 0 : this.ScrLineHeight(line, true);
  }

  internal bool TerScrToTwipsX(int scrX, out int twipsX)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    scrX -= this.e.TerWinRect.left;
    scrX += this.e.TerWinOrgX;
    int x = this.ScrToTwipsX(scrX);
    if (this.e.TerArg.PageMode)
      x = this.FrameToMargX(x);
    twipsX = x;
    return true;
  }

  internal bool TerScrToTwipsY(int scrY, out int twipsY)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    scrY -= this.e.TerWinRect.top;
    scrY += this.e.TerWinOrgY;
    int y = this.ScrToTwipsY(scrY);
    if (this.e.TerArg.PageMode)
      y = this.FrameToPageY(y);
    twipsY = y;
    return true;
  }

  internal bool TerSetCaretPos(int NewPos)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.e.CaretEngaged)
      this.SetTerCursorPos(NewPos, -1, false);
    else
      this.e.CaretPos = NewPos;
    return true;
  }

  internal new bool TerSetCursorShape(int lParam, bool normal)
  {
    int index = -1;
    int HotSpot = 0;
    Point pt = new Point();
    Cursor cursor = (Cursor) null;
    if ((this.e.TerFlags2 & 256 /*0x0100*/) == 0 && (normal || !this.e.TblSelCursShowing || !this.e.StretchHilight))
    {
      int num1 = (int) (short) COp.LOWORD(lParam);
      int num2 = (int) (short) COp.HIWORD(lParam);
      pt.X = num1 - this.e.TerWinRect.left + this.e.TerWinOrgX;
      pt.Y = num2 - this.e.TerWinRect.top + this.e.TerWinOrgY;
      if (!normal && !this.e.TerArg.ReadOnly)
        this.GetHotSpotHit(pt, out index, out HotSpot);
      if (index >= 0)
      {
        if (this.e.DragObj[index].type == 9 || this.e.DragObj[index].type == 10)
          cursor = tc.Table1Cur;
        else if (this.e.DragObj[index].type == 11)
          cursor = tc.Table3Cur;
        if (this.e.DragObj[index].type >= 5 && this.e.DragObj[index].type <= 8)
          cursor = this.e.DragObj[index].type != 8 ? Cursors.SizeWE : tc.Tab1Cur;
        if (this.e.DragObj[index].type == 1 || this.e.DragObj[index].type == 2)
        {
          if (this.e.DragObj[index].type == 2 && (this.e.ParaFrame[this.e.DragObj[index].id1].flags & 256 /*0x0100*/) != 0)
          {
            cursor = Cursors.Cross;
          }
          else
          {
            switch (HotSpot)
            {
              case 0:
              case 1:
                cursor = Cursors.SizeWE;
                break;
              case 2:
              case 3:
                cursor = Cursors.SizeNS;
                break;
              case 4:
              case 7:
                cursor = Cursors.SizeNWSE;
                break;
              default:
                cursor = Cursors.SizeNESW;
                break;
            }
          }
        }
        if (this.e.DragObj[index].type == 3)
          cursor = tc.PlusCur;
      }
      else
      {
        this.TerMousePos(lParam, false);
        int frame;
        if (-1 == (frame = this.frm.GetFrame(this.e.MouseLine)))
          return true;
        if (this.e.TerArg.PageMode && this.e.MouseLine >= 0 && this.e.MouseLine < this.e.TotalLines)
        {
          ref tc.StrCell local = ref this.e.cell[this.e.text[this.e.MouseLine].cid];
        }
        if (this.e.WheelShowing && !normal && (Cursor) null != this.e.WheelCur)
        {
          cursor = this.e.WheelCur;
          this.e.CurDragObj = -1;
          this.e.RulerClicked = false;
        }
        else if (this.e.DraggingText && !this.e.InOleDrag && !normal)
        {
          cursor = this.e.MouseOverShoot == ' ' || this.e.MouseOverShootDist <= this.e.TerFont[0].height * 3 / 2 ? (((int) this.GetKeyState(17) & 32768 /*0x8000*/) != 0 ? tc.DragInCopyCur : tc.DragInCur) : tc.DragOutCur;
          this.e.RulerClicked = false;
          this.e.CurDragObj = -1;
        }
        else if (!normal && (this.e.TerOpFlags & 2) != 0)
          cursor = tc.Table2Cur;
        else if (this.e.ShowHyperlinkCursor && !normal && (this.e.TerOpFlags & 134217728 /*0x08000000*/) == 0)
        {
          if (!this.e.RulerClicked && this.e.CurDragObj < 0 && this.IsHypertext3(this.GetCurCfmt(this.e.MouseLine, this.e.MouseCol), false, true))
            cursor = tc.HyperlinkCur;
          this.e.RulerClicked = false;
          this.e.CurDragObj = -1;
        }
        else if (((int) this.GetKeyState(17) & 32768 /*0x8000*/) != 0 && this.e.CurDragObj == -1 && !this.e.RulerClicked && !normal && (this.e.TerOpFlags & 134217728 /*0x08000000*/) == 0 && this.InvokeTextLink(false, this.e.MouseLine, this.e.MouseCol))
          cursor = tc.HyperlinkCur;
        if ((Cursor) null == cursor)
        {
          if (pt.Y < this.e.TerWinOrgY || pt.Y > this.e.TerWinOrgY + this.e.TerWinHeight)
            cursor = Cursors.Arrow;
          else if (pt.X < this.e.frame[frame].x + this.e.PfmtId[this.e.text[this.e.MouseLine].pfmt].LeftIndent)
            cursor = Cursors.Arrow;
          else if (pt.X < this.e.frame[frame].x + this.e.frame[frame].width - this.e.PfmtId[this.e.text[this.e.MouseLine].pfmt].RightIndent)
          {
            if (this.CanDragText())
            {
              cursor = Cursors.Arrow;
            }
            else
            {
              int curCfmt = this.GetCurCfmt(this.e.MouseLine, this.e.MouseCol);
              if ((this.e.TerFont[curCfmt].style & 512 /*0x0200*/) != 0)
                cursor = Cursors.Arrow;
              else if ((this.e.TerFont[curCfmt].style & 128 /*0x80*/) == 0)
                cursor = this.GetFrameTextAngle(frame) > 0 || this.e.AllTextAngle2 == 90 || this.e.AllTextAngle2 == -90 || this.e.AllTextAngle2 == 270 ? tc.HBeamCur : Cursors.IBeam;
            }
          }
        }
      }
      if (index < 0 && !this.e.MouseOnTextLine && cursor != this.e.WheelCur)
        cursor = (Cursor) null;
      if ((Cursor) null != cursor)
        this.e.Cursor = cursor;
      else
        this.e.Cursor = Cursors.Arrow;
      this.e.TblSelCursShowing = cursor == tc.Table2Cur;
      this.e.LinkCursShowing = cursor == tc.HyperlinkCur;
    }
    return true;
  }

  internal bool TerTextPosToPix(int rel, int line, int col, out int pX, out int pY)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int num;
    pY = num = 0;
    pX = num;
    if (rel != 0 && rel != 1 && rel != 2)
      return false;
    if (line < 0)
    {
      line = this.e.CurLine;
      col = this.e.CurCol;
    }
    if (col < 0)
      this.AbsToRowCol(line, out line, out col, true, false);
    int x = this.ColToUnits(col, line, 1024 /*0x0400*/) - this.e.TerWinOrgX;
    int y = this.LineToUnits(line) - this.e.TerWinOrgY;
    if (rel == 1 || rel == 0)
    {
      x += this.e.TerWinRect.left;
      y += this.e.TerWinRect.top;
    }
    if (rel == 0)
    {
      Point screen = this.e.PointToScreen(new Point(x, y));
      x = screen.X;
      y = screen.Y;
    }
    pX = x;
    pY = y;
    return true;
  }

  internal new int UnitsToCol(int HorUnits, int line)
  {
    int col = 0;
    int font = 0;
    ushort[] numArray = (ushort[]) null;
    this.e.TerOpFlags &= -524289;
    this.e.TerOpFlags &= -134217729;
    if (line >= this.e.TotalLines || line < 0)
      return 0;
    ushort[] lineCharWidth = this.GetLineCharWidth(line);
    if (lineCharWidth == null)
      return 0;
    int len = this.e.text[line].len;
    if (this.e.text[line].fmt != null)
      numArray = this.e.text[line].fmt;
    HorUnits -= this.GetRowX(line);
    if (this.e.TerArg.PageMode)
    {
      int frame = this.frm.GetFrame(line);
      if (frame >= 0)
        HorUnits -= this.e.frame[frame].ScrX;
    }
    if (HorUnits < 0)
      this.e.TerOpFlags |= 134217728 /*0x08000000*/;
    while (true)
    {
      int frameScrWidth = this.e.text[line].tabw == null || (this.e.text[line].tabw.type & 1024 /*0x0400*/) == 0 || col != this.e.text[line].tabw.FrameCharPos ? 0 : this.e.text[line].tabw.FrameScrWidth;
      if (this.e.text[line].fmt == null)
        font = (int) this.e.text[line].UniFmt;
      else if (col < len)
        font = (int) numArray[col];
      int num;
      if (col < len)
        num = frameScrWidth + (int) lineCharWidth[col];
      else if (!this.e.TerArg.WordWrap)
        num = frameScrWidth + this.fnt.LwrCharWidth(font, true, ' ');
      else
        break;
      HorUnits -= num;
      if (HorUnits >= 0)
        ++col;
      else
        goto label_22;
    }
    this.e.TerOpFlags |= 524288 /*0x080000*/;
label_22:
    if (this.e.TerArg.WordWrap)
    {
      if (col >= this.e.text[line].len && this.e.text[line].len > 0)
        col = this.e.text[line].len - 1;
    }
    else if (col > this.e.text[line].len && this.e.text[line].len > 0)
      col = this.e.text[line].len;
    return col;
  }

  internal new int UnitsToLine(int x, int y) => this.UnitsToLine2(x, y, -1);

  internal new int UnitsToLine2(int x, int y, int frm)
  {
    int index1 = -1;
    int index2 = -1;
    int index3 = -1;
    int num1 = 0;
    int num2 = 0;
    int num3 = 0;
    int num4 = 0;
    int index4 = -1;
    int num5 = -1;
    int num6 = 0;
    bool flag1 = (this.e.TerOpFlags & 4194304 /*0x400000*/) != 0;
    this.e.MouseOnTextLine = true;
    this.e.MousePictFrame = -1;
    if (!this.e.TerArg.PageMode)
    {
      int index5 = 0;
      int rowOffset = this.e.frame[index5].RowOffset;
      int line2;
      for (line2 = this.e.frame[index5].ScrFirstLine; line2 <= this.e.frame[index5].ScrLastLine; ++line2)
      {
        int num7 = this.e.RowY[rowOffset + line2 - this.e.frame[index5].ScrFirstLine];
        int num8 = this.e.RowY[rowOffset + line2 + 1 - this.e.frame[index5].ScrFirstLine];
        if (y >= num7 && y < num8)
          break;
      }
      if (y < this.e.RowY[rowOffset])
        line2 = this.e.frame[index5].ScrFirstLine;
      if (line2 > this.e.frame[index5].ScrLastLine)
      {
        line2 = this.e.frame[index5].ScrLastLine;
        if (flag1)
          this.e.MouseOnTextLine = false;
      }
      return line2;
    }
    if ((this.e.TerFlags3 & 32 /*0x20*/) != 0 && this.e.text[this.e.CurLine].cid > 0)
      num6 = this.e.text[this.e.CurLine].cid;
    int FrameNo1;
    if (frm >= 0)
    {
      FrameNo1 = frm;
    }
    else
    {
      int FrameNo2;
      if (this.e.ContainsParaFrames && num6 == 0)
      {
        bool flag2 = false;
        for (FrameNo2 = 0; FrameNo2 < this.e.TotalFrames; ++FrameNo2)
        {
          if ((!this.e.frame[FrameNo2].empty || (this.e.frame[FrameNo2].flags1 & 2) != 0) && this.e.frame[FrameNo2].ParaFrameId != 0 && ((this.e.frame[FrameNo2].flags & 4096 /*0x1000*/) == 0 || (this.e.TerOpFlags2 & 512 /*0x0200*/) != 0))
          {
            int paraFrameId = this.e.frame[FrameNo2].ParaFrameId;
            if ((this.e.ParaFrame[paraFrameId].flags & 512 /*0x0200*/) != 0 && (this.e.ParaFrame[paraFrameId].ShapeType == 0 || this.e.ParaFrame[paraFrameId].ShapeType == 1))
            {
              int num9 = this.TwipsToScrY(60) / 2;
              if ((this.e.TerOpFlags & 16384 /*0x4000*/) != 0)
                num9 = 0;
              int x1 = this.e.frame[FrameNo2].x;
              int y1 = this.e.frame[FrameNo2].y;
              int num10 = x1 + this.e.frame[FrameNo2].width;
              int num11 = y1 + this.e.frame[FrameNo2].height;
              if ((y < y1 - num9 || y > num11 + num9 || (x < x1 - num9 || x > x1 + num9) && (x < num10 - num9 || x > num10 + num9)) && (x < x1 - num9 || x > num10 + num9 || (y < y1 - num9 || y > y1 + num9) && (y < num11 - num9 || y > num11 + num9)))
                continue;
            }
            else if (!this.InRotatedFrame(x, y, FrameNo2))
              continue;
            if (!flag2)
            {
              num4 = FrameNo2;
              flag2 = true;
            }
            else if (this.e.frame[FrameNo2].x > num2 || this.e.frame[FrameNo2].y > num3)
              num4 = FrameNo2;
            if ((this.e.frame[FrameNo2].flags1 & 2) != 0)
              index4 = FrameNo2;
            else
              num5 = FrameNo2;
            num2 = this.e.frame[FrameNo2].x;
            num3 = this.e.frame[FrameNo2].y;
          }
        }
        if (flag2)
          FrameNo2 = num4;
        if (FrameNo2 < this.e.TotalFrames && (this.e.frame[FrameNo2].flags & 2048 /*0x0800*/) != 0)
        {
          this.e.MousePictFrame = FrameNo2;
          return this.e.frame[FrameNo2].PageFirstLine;
        }
      }
      else
        FrameNo2 = this.e.TotalFrames;
      if (FrameNo2 == index4 && (this.e.frame[FrameNo2].flags & 4096 /*0x1000*/) == 0)
      {
        if (num5 != -1)
        {
          FrameNo2 = num5;
        }
        else
        {
          int pageLastLine = this.e.frame[index4].PageLastLine;
          if (this.LineInfo(pageLastLine, 32 /*0x20*/) && pageLastLine > 0)
            --pageLastLine;
          return pageLastLine;
        }
      }
      if (FrameNo2 == this.e.TotalFrames)
      {
        FrameNo2 = 0;
        while (FrameNo2 < this.e.TotalFrames && (this.e.frame[FrameNo2].empty || this.e.frame[FrameNo2].ParaFrameId > 0 || (this.e.frame[FrameNo2].flags & 4096 /*0x1000*/) != 0 && (this.e.TerOpFlags2 & 512 /*0x0200*/) == 0 || num6 != 0 && this.e.frame[FrameNo2].CellId != num6 || x < this.e.frame[FrameNo2].x || x >= this.e.frame[FrameNo2].x + this.e.frame[FrameNo2].width || y < this.e.frame[FrameNo2].y || y >= this.e.frame[FrameNo2].y + this.e.frame[FrameNo2].height))
          ++FrameNo2;
      }
      if (FrameNo2 == this.e.TotalFrames)
      {
        if (flag1)
          this.e.MouseOnTextLine = false;
        for (FrameNo2 = 0; FrameNo2 < this.e.TotalFrames; ++FrameNo2)
        {
          if (!this.e.frame[FrameNo2].empty && this.e.frame[FrameNo2].ParaFrameId <= 0 && ((this.e.frame[FrameNo2].flags & 4096 /*0x1000*/) == 0 || (this.e.TerOpFlags2 & 512 /*0x0200*/) != 0) && (num6 == 0 || this.e.frame[FrameNo2].CellId == num6))
          {
            if (y < this.e.frame[FrameNo2].y || y >= this.e.frame[FrameNo2].y + this.e.frame[FrameNo2].height)
            {
              if (y < this.e.FirstPageHeight && FrameNo2 < this.e.FirstPage2Frame)
              {
                if (index1 == -1)
                  index1 = FrameNo2;
                index2 = FrameNo2;
              }
              else if (y >= this.e.FirstPageHeight && FrameNo2 >= this.e.FirstPage2Frame)
              {
                if (index1 == -1)
                  index1 = FrameNo2;
                index2 = FrameNo2;
              }
              int num12 = Math.Abs(y - this.e.frame[FrameNo2].y);
              if (index3 < 0)
              {
                index3 = FrameNo2;
                num1 = num12;
              }
              if (num12 < num1)
              {
                index3 = FrameNo2;
                num1 = num12;
              }
              int num13 = Math.Abs(this.e.frame[FrameNo2].y + this.e.frame[FrameNo2].height - y);
              if (num13 < num1)
              {
                index3 = FrameNo2;
                num1 = num13;
              }
            }
            else
              break;
          }
        }
      }
      if (FrameNo2 == this.e.TotalFrames)
      {
        if (flag1)
          this.e.MouseOnTextLine = false;
        if (num6 != 0)
        {
          int index6 = 0;
          while (index6 < this.e.TotalFrames && this.e.frame[index6].CellId != num6)
            ++index6;
          if (index6 < this.e.TotalFrames)
            index3 = index6;
        }
        if (index3 >= 0)
        {
          int LineNo = Math.Abs(this.e.frame[index3].y - y) >= Math.Abs(this.e.frame[index3].y + this.e.frame[index3].height - y) ? this.e.frame[index3].PageLastLine : this.e.frame[index3].PageFirstLine;
          if (this.LineInfo(LineNo, 32 /*0x20*/))
            --LineNo;
          return LineNo;
        }
        if (index1 >= 0 && y < this.e.frame[index1].y)
          return this.e.frame[index1].PageFirstLine;
        if (index2 >= 0 && y >= this.e.frame[index2].y + this.e.frame[index2].height)
          return this.e.frame[index2].PageLastLine;
        int num14 = this.e.CurPage < this.e.TotalPages - 1 ? this.e.PageInfo[this.e.CurPage + 1].FirstLine : this.e.TotalLines - 1;
        return (this.e.CurLine < this.e.PageInfo[this.e.CurPage].FirstLine || this.e.CurLine > num14) && index2 >= 0 ? this.e.frame[index2].PageFirstLine : this.e.CurLine;
      }
      FrameNo1 = FrameNo2;
    }
    int paraFrameId1 = this.e.frame[FrameNo1].ParaFrameId;
    int num15 = this.e.frame[FrameNo1].y + this.e.frame[FrameNo1].SpaceTop;
    ref tc.StrFrame local1 = ref this.e.frame[FrameNo1];
    ref tc.StrFrame local2 = ref this.e.frame[FrameNo1];
    int frameTextAngle = this.GetFrameTextAngle(FrameNo1);
    if (frameTextAngle == 0 || frm > 0)
      y -= num15;
    else if (frameTextAngle == 90)
    {
      int num16 = this.FrameRotateX(this.e.frame[FrameNo1].x, this.e.frame[FrameNo1].y, FrameNo1);
      y = x - num16;
    }
    else
    {
      int height = this.e.frame[FrameNo1].height;
      int num17 = this.FrameRotateX(this.e.frame[FrameNo1].x, this.e.frame[FrameNo1].y, FrameNo1) - height;
      y = height - (x - num17);
      y -= this.e.frame[FrameNo1].SpaceTop;
    }
    int pageLastLine1 = this.e.frame[FrameNo1].PageLastLine;
    if (pageLastLine1 > 0 && pageLastLine1 < this.e.TotalLines && this.e.text[pageLastLine1].tabw != null && (this.e.text[pageLastLine1].tabw.type & 32 /*0x20*/) != 0)
      --pageLastLine1;
    int num18 = 0;
    int pageFirstLine;
    for (pageFirstLine = this.e.frame[FrameNo1].PageFirstLine; pageFirstLine < pageLastLine1; ++pageFirstLine)
    {
      if (this.TableLevel(pageFirstLine) == this.e.frame[FrameNo1].level)
      {
        int num19 = this.ScrLineHeight(pageFirstLine, true);
        if (num18 + num19 <= y)
          num18 += num19;
        else
          break;
      }
    }
    return pageFirstLine;
  }

  internal new bool UseCaret()
  {
    return (!this.e.TerArg.ReadOnly || (this.e.TerFlags & 128 /*0x80*/) != 0 || this.e.ProtectForm) && (this.e.TerFlags2 & 64 /*0x40*/) == 0;
  }
}
