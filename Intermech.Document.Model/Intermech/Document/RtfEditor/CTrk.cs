// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.CTrk
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class CTrk : COp
{
  internal CTrk(ImRtfEditor ImRtfEditor)
    : base(ImRtfEditor)
  {
    this.e = ImRtfEditor;
  }

  internal bool AcceptChange(int line, int col)
  {
    int curCfmt = this.GetCurCfmt(line, col);
    if (!this.IsTrackChangeFont(curCfmt))
      return false;
    int StartLine;
    int pBeginLine = StartLine = line;
    int StartCol;
    int pBeginCol = StartCol = col;
    if (!this.GetChangeBeginPos(ref pBeginLine, ref pBeginCol) || !this.TerLocateChangedChar(this.e.TerFont[curCfmt].InsRev, this.e.TerFont[curCfmt].DelRev, false, ref StartLine, ref StartCol, true))
      return false;
    if (this.e.TerFont[curCfmt].DelRev > 0)
    {
      int num = (this.e.TerFlags & 256 /*0x0100*/) != 0 ? 1 : 0;
      this.e.HilightBegRow = pBeginLine;
      this.e.HilightBegCol = pBeginCol;
      this.e.HilightEndRow = StartLine;
      this.e.HilightEndCol = StartCol;
      this.e.StretchHilight = false;
      this.e.HilightType = 2;
      this.e.TerFlags |= 256 /*0x0100*/;
      this.e.TerDeleteBlock(false);
      if (num == 0)
        this.ResetTerFlag(256 /*0x0100*/);
      return true;
    }
    this.SaveUndo(pBeginLine, pBeginCol, StartLine, StartCol, 'F');
    for (int line1 = pBeginLine; line1 <= StartLine; ++line1)
    {
      int num1 = line1 == pBeginLine ? pBeginCol : 0;
      int num2 = line1 == StartLine ? StartCol : this.e.text[line1].len;
      ushort[] numArray = this.OpenCfmt(line1);
      for (int col1 = num1; col1 < num2; ++col1)
      {
        int CurFont = (int) numArray[col1];
        int insRev = this.e.TerFont[CurFont].InsRev;
        int index = (int) this.SetTrackingFont(CurFont, 0);
        if (this.e.reviewer[insRev].InsColor == this.e.TerFont[index].TextColor)
          index = (int) this.GetNewColor((ushort) index, this.ToColorRef(tc.CLR_AUTO), 0, "", line1, col1);
        if ((this.e.reviewer[insRev].InsStyle & this.e.TerFont[index].style) != 0)
          index = this.SetFontStyle(index, this.e.reviewer[insRev].InsStyle, false);
        numArray[col1] = (ushort) index;
      }
      this.CloseCfmt(line1);
    }
    ++this.e.TerArg.modified;
    return true;
  }

  internal new bool FreeReviewer(int idx) => idx >= 0 && idx < this.e.TotalReviewers;

  internal bool GetChangeBeginPos(ref int pBeginLine, ref int pBeginCol)
  {
    int line = pBeginLine;
    int col = pBeginCol;
    int curCfmt1 = this.GetCurCfmt(line, col);
    if (this.TerLocateChangedChar(this.e.TerFont[curCfmt1].InsRev, this.e.TerFont[curCfmt1].DelRev, false, ref line, ref col, false))
    {
      this.NextTextPos(ref line, ref col);
    }
    else
    {
      int curCfmt2 = this.GetCurCfmt(0, 0);
      if (this.e.TerFont[curCfmt2].InsRev != this.e.TerFont[curCfmt1].InsRev || this.e.TerFont[curCfmt2].DelRev != this.e.TerFont[curCfmt1].DelRev)
        return false;
      line = 0;
      col = 0;
    }
    pBeginLine = line;
    pBeginCol = col;
    return true;
  }

  internal int GetReviewerSlot()
  {
    if (this.e.TotalReviewers >= this.e.MaxReviewers)
    {
      int count = this.e.MaxReviewers + this.e.MaxReviewers / 2;
      this.e.reviewer = this.ReAlloc(this.e.reviewer, count);
      this.e.MaxReviewers = count;
    }
    this.e.reviewer[this.e.TotalReviewers] = new tc.StrReviewer();
    ++this.e.TotalReviewers;
    int index = this.e.TotalReviewers - 1;
    this.e.reviewer[index].name = "";
    this.e.reviewer[index].InsStyle = 0;
    this.e.reviewer[index].DelStyle = 8;
    Color color1 = tc.CLR_AUTO;
    switch (index)
    {
      case 1:
        color1 = Color.FromArgb((int) byte.MaxValue, 0, 0);
        break;
      case 2:
        color1 = Color.FromArgb(0, 0, (int) byte.MaxValue);
        break;
      case 3:
        color1 = Color.FromArgb(0, (int) byte.MaxValue, 0);
        break;
      case 4:
        color1 = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 0);
        break;
      case 5:
        color1 = Color.FromArgb((int) byte.MaxValue, 0, (int) byte.MaxValue);
        break;
      case 6:
        color1 = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue);
        break;
      case 7:
        color1 = Color.FromArgb(128 /*0x80*/, 0, 0);
        break;
      case 8:
        color1 = Color.FromArgb(0, 128 /*0x80*/, 0);
        break;
      case 9:
        color1 = Color.FromArgb(0, 0, 128 /*0x80*/);
        break;
      case 10:
        color1 = Color.FromArgb(128 /*0x80*/, 128 /*0x80*/, 0);
        break;
      case 11:
        color1 = Color.FromArgb(128 /*0x80*/, 0, 128 /*0x80*/);
        break;
      case 12:
        color1 = Color.FromArgb(0, 128 /*0x80*/, 128 /*0x80*/);
        break;
    }
    Color color2;
    this.e.reviewer[index].DelColor = color2 = color1;
    this.e.reviewer[index].InsColor = color2;
    return this.e.TotalReviewers - 1;
  }

  internal new bool IsTrackChangeFont(int CurCfmt)
  {
    return this.e.TerFont[CurCfmt].InsRev > 0 || this.e.TerFont[CurCfmt].DelRev > 0;
  }

  internal bool LocateRevMatched(
    bool present,
    int InsRev,
    int DelRev,
    int CurInsRev,
    int CurDelRev)
  {
    if (present)
    {
      if (InsRev == CurInsRev && DelRev == CurDelRev || InsRev == CurInsRev && DelRev == -1 || InsRev == -1 && DelRev == CurDelRev || InsRev < 0 && DelRev < 0 && (CurInsRev > 0 || CurDelRev > 0))
        return true;
    }
    else if (InsRev >= 0 && DelRev >= 0 && (InsRev != CurInsRev || DelRev != CurDelRev) || InsRev < 0 && DelRev >= 0 && DelRev != CurDelRev || DelRev < 0 && InsRev >= 0 && InsRev != CurInsRev || InsRev < 0 && DelRev < 0 && CurInsRev == 0 && CurDelRev == 0)
      return true;
    return false;
  }

  internal new ushort SetTrackingFont(int CurFont, int type)
  {
    if ((this.e.TerFont[CurFont].style & 128 /*0x80*/) != 0)
    {
      this.e.TerFont[CurFont].InsRev = 0;
      this.e.TerFont[CurFont].DelRev = 0;
      this.e.TerFont[CurFont].InsTime = (tc.ClsDateTime) null;
      this.e.TerFont[CurFont].DelTime = (tc.ClsDateTime) null;
      switch (type)
      {
        case 1:
          this.e.TerFont[CurFont].InsRev = this.e.TrackRev;
          this.e.TerFont[CurFont].InsTime = this.e.TrackTime.Copy();
          this.e.TerFont[CurFont].DelRev = 0;
          this.e.TerFont[CurFont].DelTime = (tc.ClsDateTime) null;
          break;
        case 2:
          this.e.TerFont[CurFont].DelRev = this.e.TrackRev;
          this.e.TerFont[CurFont].DelTime = this.e.TrackTime.Copy();
          break;
        default:
          this.e.TerFont[CurFont].InsRev = 0;
          this.e.TerFont[CurFont].DelRev = 0;
          this.e.TerFont[CurFont].InsTime = (tc.ClsDateTime) null;
          this.e.TerFont[CurFont].DelTime = (tc.ClsDateTime) null;
          break;
      }
      this.SetPictSize(CurFont, this.TwipsToScrY(this.e.TerFont[CurFont].PictHeight), this.TwipsToScrX(this.e.TerFont[CurFont].PictWidth), true);
      this.XlateSizeForPrt(CurFont);
      return (ushort) CurFont;
    }
    tc.StrFont font = this.e.TerFont[CurFont];
    switch (type)
    {
      case 1:
        font.InsRev = this.e.TrackRev;
        font.InsTime = this.e.TrackTime.Copy();
        font.DelRev = 0;
        font.DelTime = (tc.ClsDateTime) null;
        break;
      case 2:
        font.DelRev = this.e.TrackRev;
        font.DelTime = this.e.TrackTime.Copy();
        break;
      default:
        font.InsRev = 0;
        font.DelRev = 0;
        font.InsTime = (tc.ClsDateTime) null;
        font.DelTime = (tc.ClsDateTime) null;
        break;
    }
    ushort newFont2;
    return (newFont2 = (ushort) this.GetNewFont2(this.e.TerGr, CurFont, font)) != (ushort) 0 ? newFont2 : (ushort) CurFont;
  }

  internal int TerAcceptChanges(bool all, bool msg, bool repaint)
  {
    int num1 = 0;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.e.TrackChanges)
      return -1;
    if (!all)
    {
      if (!this.AcceptChange(this.e.CurLine, this.e.CurCol))
        return 0;
      if (repaint)
        this.PaintTer();
      return 1;
    }
    if (msg && DialogResult.No == this.ShowMessage(this.e.MsgString[225], "", MessageBoxButtons.YesNo))
      return -1;
    int undoRef = this.e.UndoRef;
    int curLine = this.e.CurLine;
    int curCol = this.e.CurCol;
    this.e.CurLine = 0;
    this.e.CurCol = 0;
    while (this.TerFindNextChange(true, false))
    {
      this.e.UndoRef = undoRef;
      if (this.AcceptChange(this.e.CurLine, this.e.CurCol))
      {
        if (this.IsTrackChangeFont(this.GetCurCfmt(this.e.CurLine, this.e.CurCol)))
          this.PrevTextPos();
        ++num1;
      }
      else
        break;
    }
    if (num1 == 0)
    {
      this.e.CurLine = curLine;
      this.e.CurCol = curCol;
    }
    else if (repaint)
      this.PaintTer();
    if (msg)
    {
      int num2 = (int) this.ShowMessage($"{this.e.MsgString[226]} {num1.ToString()}", "", MessageBoxButtons.OK);
    }
    return num1;
  }

  internal bool TerEnableTracking(
    bool enable,
    string UName,
    bool UseDefaultClrStyle,
    int InsStyle,
    Color InsColor,
    int DelStyle,
    Color DelColor)
  {
    bool flag = false;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.WordWrap)
      return false;
    if (this.e.TrackChanges != enable)
    {
      this.e.TrackChanges = enable;
      if (enable)
      {
        string strB = !this.True(UName) || UName.Length <= 0 ? Environment.UserName : UName;
        int index = 1;
        while (index < this.e.TotalReviewers && string.Compare(this.e.reviewer[index].name, strB, true) != 0)
          ++index;
        if (index == this.e.TotalReviewers)
        {
          index = this.GetReviewerSlot();
          this.e.reviewer[index].name = strB;
          flag = true;
          if (UseDefaultClrStyle)
          {
            this.e.reviewer[index].InsStyle = 0;
            this.e.reviewer[index].DelStyle = 8;
            Color color1 = tc.CLR_AUTO;
            switch (index)
            {
              case 1:
                color1 = Color.FromArgb((int) byte.MaxValue, 0, 0);
                break;
              case 2:
                color1 = Color.FromArgb(0, (int) byte.MaxValue, 0);
                break;
              case 3:
                color1 = Color.FromArgb(0, 0, (int) byte.MaxValue);
                break;
              case 4:
                color1 = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 0);
                break;
              case 5:
                color1 = Color.FromArgb((int) byte.MaxValue, 0, (int) byte.MaxValue);
                break;
              case 6:
                color1 = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue);
                break;
              case 7:
                color1 = Color.FromArgb(128 /*0x80*/, 0, 0);
                break;
              case 8:
                color1 = Color.FromArgb(0, 128 /*0x80*/, 0);
                break;
              case 9:
                color1 = Color.FromArgb(0, 0, 128 /*0x80*/);
                break;
              case 10:
                color1 = Color.FromArgb(128 /*0x80*/, 128 /*0x80*/, 0);
                break;
              case 11:
                color1 = Color.FromArgb(128 /*0x80*/, 0, 128 /*0x80*/);
                break;
              case 12:
                color1 = Color.FromArgb(0, 128 /*0x80*/, 128 /*0x80*/);
                break;
            }
            Color color2;
            this.e.reviewer[index].DelColor = color2 = color1;
            this.e.reviewer[index].InsColor = color2;
          }
        }
        if (!UseDefaultClrStyle)
        {
          this.e.reviewer[index].InsStyle = InsStyle;
          this.e.reviewer[index].DelStyle = DelStyle;
          this.e.reviewer[index].InsColor = InsColor;
          this.e.reviewer[index].DelColor = DelColor;
        }
        this.e.TrackRev = index;
        this.e.TrackTime = new tc.ClsDateTime();
        this.e.TrackTime.dt = DateTime.Now;
        this.e.InsertMode = true;
        if (!flag)
        {
          this.RecreateFonts(this.e.TerGr);
          this.RequestPagination(true);
          this.PaintTer();
        }
      }
      else
      {
        this.e.TrackRev = 0;
        this.e.TrackTime = (tc.ClsDateTime) null;
      }
    }
    return true;
  }

  internal bool TerFindNextChange(bool forward, bool repaint)
  {
    bool nextChange = false;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int curLine = this.e.CurLine;
    int curCol = this.e.CurCol;
    int curCfmt = this.GetCurCfmt(curLine, curCol);
    if (this.e.TerFont[curCfmt].DelRev > 0)
    {
      if (!this.TerLocateChangedChar(-1, this.e.TerFont[curCfmt].DelRev, false, ref curLine, ref curCol, forward))
        goto label_7;
    }
    else if (this.e.TerFont[curCfmt].InsRev > 0 && !this.TerLocateChangedChar(this.e.TerFont[curCfmt].InsRev, 0, false, ref curLine, ref curCol, forward))
      goto label_7;
    nextChange = this.TerLocateChangedChar(-1, -1, true, ref curLine, ref curCol, forward);
    if (nextChange && !forward)
      this.GetChangeBeginPos(ref curLine, ref curCol);
label_7:
    if (nextChange)
    {
      this.e.CurCol = curCol;
      if (repaint)
      {
        this.TerPosLine(curLine + 1);
        return nextChange;
      }
      this.e.CurLine = curLine;
      return nextChange;
    }
    if (repaint)
    {
      int num = (int) this.ShowMessage(this.e.MsgString[224 /*0xE0*/], "", MessageBoxButtons.OK);
    }
    return nextChange;
  }

  internal bool TerLocateChangedChar(
    int InsRev,
    int DelRev,
    bool present,
    ref int StartLine,
    ref int StartCol,
    bool forward)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    for (int index = 0; index < this.e.TotalFonts; ++index)
      tc.ResetUintFlag(ref this.e.TerFont[index].flags, 8192 /*0x2000*/);
    if (forward)
    {
      for (int line = StartLine; line < this.e.TotalLines; ++line)
      {
        int num = line != StartLine ? 0 : StartCol;
        if (num < this.e.text[line].len && this.e.text[line].len != 0)
        {
          if (this.e.text[line].fmt == null)
          {
            int uniFmt = (int) this.e.text[line].UniFmt;
            if ((this.e.TerFont[uniFmt].flags & 8192 /*0x2000*/) == 0)
            {
              this.e.TerFont[uniFmt].flags |= 8192 /*0x2000*/;
              int insRev = this.e.TerFont[uniFmt].InsRev;
              int delRev = this.e.TerFont[uniFmt].DelRev;
              if (this.LocateRevMatched(present, InsRev, DelRev, insRev, delRev))
              {
                StartLine = line;
                StartCol = num;
                return true;
              }
            }
          }
          else
          {
            ushort[] numArray = this.OpenCfmt(line);
            for (int index1 = num; index1 < this.e.text[line].len; ++index1)
            {
              int index2 = (int) numArray[index1];
              if ((this.e.TerFont[index2].flags & 8192 /*0x2000*/) == 0)
              {
                this.e.TerFont[index2].flags |= 8192 /*0x2000*/;
                int insRev = this.e.TerFont[index2].InsRev;
                int delRev = this.e.TerFont[index2].DelRev;
                if (this.LocateRevMatched(present, InsRev, DelRev, insRev, delRev))
                {
                  StartLine = line;
                  StartCol = index1;
                  this.CloseCfmt(line);
                  return true;
                }
              }
            }
            this.CloseCfmt(line);
          }
        }
      }
    }
    else
    {
      for (int line = StartLine; line >= 0; --line)
      {
        int num = line != StartLine ? this.e.text[line].len - 1 : StartCol;
        if (num >= 0 && this.e.text[line].len != 0)
        {
          if (this.e.text[line].fmt == null)
          {
            int uniFmt = (int) this.e.text[line].UniFmt;
            if ((this.e.TerFont[uniFmt].flags & 8192 /*0x2000*/) == 0)
            {
              this.e.TerFont[uniFmt].flags |= 8192 /*0x2000*/;
              int insRev = this.e.TerFont[uniFmt].InsRev;
              int delRev = this.e.TerFont[uniFmt].DelRev;
              if (this.LocateRevMatched(present, InsRev, DelRev, insRev, delRev))
              {
                StartLine = line;
                StartCol = num;
                return true;
              }
            }
          }
          else
          {
            ushort[] numArray = this.OpenCfmt(line);
            for (int index3 = num; index3 >= 0; --index3)
            {
              int index4 = (int) numArray[index3];
              if ((this.e.TerFont[index4].flags & 8192 /*0x2000*/) == 0)
              {
                this.e.TerFont[index4].flags |= 8192 /*0x2000*/;
                int insRev = this.e.TerFont[index4].InsRev;
                int delRev = this.e.TerFont[index4].DelRev;
                if (this.LocateRevMatched(present, InsRev, DelRev, insRev, delRev))
                {
                  StartLine = line;
                  StartCol = index3;
                  this.CloseCfmt(line);
                  return true;
                }
              }
            }
            this.CloseCfmt(line);
          }
        }
      }
    }
    return false;
  }

  internal new bool TrackDel(int line, int col, bool forward)
  {
    int curLine = this.e.CurLine;
    if (!this.e.TrackChanges)
      return false;
    int curCfmt = this.GetCurCfmt(line, col);
    if (this.e.TerFont[curCfmt].InsRev == this.e.TrackRev)
      return false;
    if (!this.True(this.e.TerFont[curCfmt].DelRev))
    {
      this.SaveUndo(line, col, line, col, 'F');
      ushort[] numArray = this.OpenCfmt(line);
      numArray[col] = this.SetTrackingFont((int) numArray[col], 2);
      this.CloseCfmt(line);
    }
    if (forward)
      this.NextTextPos();
    return true;
  }

  internal new bool TrackDelBlock(
    int BegLine,
    int BegCol,
    int EndLine,
    int EndCol,
    bool ResetHilight,
    bool repaint)
  {
    int num1 = 0;
    if (!this.e.TrackChanges)
      return false;
    bool flag1 = false;
    for (int line = BegLine; line <= EndLine; ++line)
    {
      if ((this.e.text[line].flags2 & 128 /*0x80*/) == 0)
      {
        flag1 = true;
        break;
      }
      int num2 = line == BegLine ? BegCol : 0;
      int num3 = line == EndLine ? EndCol : this.e.text[line].len;
      ushort[] numArray = this.OpenCfmt(line);
      for (int index = num2; index < num3; ++index)
      {
        if (this.e.TerFont[(int) numArray[index]].InsRev != this.e.TrackRev)
        {
          flag1 = true;
          break;
        }
      }
      this.CloseCfmt(line);
      if (flag1)
        break;
    }
    if (!flag1)
      return false;
    int abs = this.RowColToAbs(EndLine, EndCol);
    int undoRef = this.e.UndoRef;
    this.SaveUndo(BegLine, BegCol, EndLine, EndCol - 1, 'D');
    for (int index1 = BegLine; index1 <= EndLine; ++index1)
    {
      int num4 = index1 == BegLine ? BegCol : 0;
      int num5 = index1 == EndLine ? EndCol : this.e.text[index1].len;
      if ((this.e.text[index1].flags2 & 128 /*0x80*/) == 0)
      {
        ushort[] numArray = this.OpenCfmt(index1);
        for (int index2 = num4; index2 < num5; ++index2)
        {
          if (this.e.TerFont[(int) numArray[index2]].DelRev != this.e.TrackRev)
            numArray[index2] = this.SetTrackingFont((int) numArray[index2], 2);
        }
        this.CloseCfmt(index1);
      }
      else
      {
        bool flag2 = true;
        ushort[] numArray1 = this.OpenCfmt(index1);
        for (int index3 = num4; index3 < num5; ++index3)
        {
          int index4 = (int) numArray1[index3];
          if (this.e.TerFont[index4].InsRev != this.e.TrackRev || this.True(this.e.TerFont[index4].DelRev))
          {
            flag2 = false;
            break;
          }
        }
        this.CloseCfmt(index1);
        if (flag2 && num4 == 0 && num5 == this.e.text[index1].len)
        {
          num1 += this.e.text[index1].len;
          if (index1 < this.e.CurLine)
            --this.e.CurLine;
          if (index1 == this.e.CurLine)
          {
            --this.e.CurLine;
            this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
          }
          this.MoveLineArrays(index1, 1, 'D');
          --EndLine;
        }
        else
        {
          ushort[] numArray2 = this.OpenCfmt(index1);
          for (int StartPos = num4; StartPos < num5; ++StartPos)
          {
            int index5 = (int) numArray2[StartPos];
            if (!this.True(this.e.TerFont[index5].DelRev))
            {
              if (this.e.TerFont[index5].InsRev == this.e.TrackRev)
              {
                this.MoveLineData(index1, StartPos, 1, 'D');
                numArray2 = this.OpenCfmt(index1);
                --StartPos;
                --num5;
                ++num1;
                if (index1 == this.e.CurLine && StartPos < this.e.CurCol)
                  --this.e.CurCol;
              }
              else if (this.e.TerFont[index5].DelRev != this.e.TrackRev)
                numArray2[StartPos] = this.SetTrackingFont((int) numArray2[StartPos], 2);
            }
          }
          this.CloseCfmt(index1);
        }
      }
    }
    this.e.UndoRef = undoRef;
    this.AbsToRowCol(abs - num1, out EndLine, out EndCol);
    this.SaveUndo(BegLine, BegCol, EndLine, EndCol - 1, 'I');
    if (this.e.CurLine >= this.e.TotalLines)
      this.e.CurLine = this.e.TotalLines - 1;
    if (this.e.CurCol >= this.e.text[this.e.CurLine].len)
      this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
    if (this.e.CurCol < 0)
      this.e.CurCol = 0;
    if (ResetHilight)
      this.e.HilightType = 0;
    if (repaint)
      this.PaintTer();
    return true;
  }

  internal new bool TrackingComment(int line, int col, out string pMsg)
  {
    int curCfmt = this.GetCurCfmt(line, col);
    pMsg = "";
    if ((this.e.TerFlags6 & 262144 /*0x040000*/) != 0 || this.False(this.e.TerFont[curCfmt].InsRev) && this.False(this.e.TerFont[curCfmt].DelRev))
      return false;
    DateTime dateTime;
    if (this.True(this.e.TerFont[curCfmt].InsRev))
    {
      int insRev = this.e.TerFont[curCfmt].InsRev;
      dateTime = this.e.TerFont[curCfmt].InsTime == null ? DateTime.Now : this.e.TerFont[curCfmt].InsTime.dt;
      string str = pMsg;
      pMsg = $"{str}{this.e.MsgString[222]}{this.e.reviewer[insRev].name}\n    @ {dateTime.ToString()}";
    }
    if (this.True(this.e.TerFont[curCfmt].DelRev))
    {
      int delRev = this.e.TerFont[curCfmt].DelRev;
      dateTime = this.e.TerFont[curCfmt].DelTime == null ? DateTime.Now : this.e.TerFont[curCfmt].DelTime.dt;
      if (pMsg.Length > 0)
        pMsg += "\n";
      string str = pMsg;
      pMsg = $"{str}{this.e.MsgString[223]}{this.e.reviewer[delRev].name}\n    @ {dateTime.ToString()}";
    }
    return pMsg.Length > 0;
  }
}
