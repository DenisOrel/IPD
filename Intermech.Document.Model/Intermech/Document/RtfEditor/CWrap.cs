// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.CWrap
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class CWrap : COp
{
  internal CWrap(ImRtfEditor ImRtfEditor)
    : base(ImRtfEditor)
  {
  }

  internal new bool CopyWrapLineData(int LineBegin, int LineEnd, char CurChar, int lflags2)
  {
    if (this.e.text[this.e.LastWrappedLine].len != LineEnd - LineBegin)
    {
      if (LineEnd - LineBegin > 0)
      {
        this.e.text[this.e.LastWrappedLine].txt = new char[LineEnd - LineBegin + 1];
        this.e.text[this.e.LastWrappedLine].fmt = new ushort[LineEnd - LineBegin + 1];
        if (this.e.TagsWrapped)
          this.e.text[this.e.LastWrappedLine].tag = new ushort[LineEnd - LineBegin + 1];
        if (this.e.CharWidthWrapped)
          this.e.text[this.e.LastWrappedLine].cwidth = new ushort[LineEnd - LineBegin + 1];
      }
      else
      {
        this.e.text[this.e.LastWrappedLine].fmt = (ushort[]) null;
        this.e.text[this.e.LastWrappedLine].UniFmt = this.e.WrapCfmt[LineBegin];
        this.e.text[this.e.LastWrappedLine].tag = (ushort[]) null;
        this.e.text[this.e.LastWrappedLine].cwidth = (ushort[]) null;
      }
      this.e.text[this.e.LastWrappedLine].len = LineEnd - LineBegin;
      if (this.e.PaintFlag != 4 && (int) CurChar != (int) this.e.ParaChar && (int) CurChar != (int) this.e.CellChar)
        this.e.PaintFlag = 4;
    }
    if (this.e.text[this.e.LastWrappedLine].len > 0)
    {
      char[] txt = this.e.text[this.e.LastWrappedLine].txt;
      ushort[] fmt = this.e.text[this.e.LastWrappedLine].fmt;
      this.FarMove(this.e.wrap, LineBegin, txt, 0, this.e.text[this.e.LastWrappedLine].len);
      this.FarMove(this.e.WrapCfmt, LineBegin, fmt, 0, this.e.text[this.e.LastWrappedLine].len);
      if (this.e.TagsWrapped)
      {
        if (this.e.text[this.e.LastWrappedLine].tag == null)
          this.e.text[this.e.LastWrappedLine].tag = new ushort[this.e.text[this.e.LastWrappedLine].len];
        this.FarMove(this.e.WrapCtid, LineBegin, this.e.text[this.e.LastWrappedLine].tag, 0, this.e.text[this.e.LastWrappedLine].len);
        this.CompressCtid(this.e.LastWrappedLine);
      }
      else
        this.e.text[this.e.LastWrappedLine].tag = (ushort[]) null;
      if (this.e.CharWidthWrapped)
      {
        if (this.e.text[this.e.LastWrappedLine].cwidth == null)
          this.e.text[this.e.LastWrappedLine].cwidth = new ushort[this.e.text[this.e.LastWrappedLine].len];
        this.FarMove(this.e.WrapCharWidth, LineBegin, this.e.text[this.e.LastWrappedLine].cwidth, 0, this.e.text[this.e.LastWrappedLine].len);
      }
      else
        this.e.text[this.e.LastWrappedLine].cwidth = (ushort[]) null;
    }
    if ((lflags2 & 512 /*0x0200*/) != 0)
    {
      int len = this.e.text[this.e.LastWrappedLine].len;
      if (len > 0)
      {
        this.MoveLineData(this.e.LastWrappedLine, len - 1, 1, 'A');
        char[] txt = this.e.text[this.e.LastWrappedLine].txt;
        ushort[] numArray = this.OpenCfmt(this.e.LastWrappedLine);
        txt[len] = '\u0006';
        numArray[len] = numArray[len - 1];
        this.CloseCfmt(this.e.LastWrappedLine);
      }
    }
    return true;
  }

  internal new bool DisplacePointers(int StartLine, int count)
  {
    if (count > 0)
    {
      this.e.TerOpFlags |= 256 /*0x0100*/;
      int num = this.CheckLineLimit(this.e.TotalLines + count) ? 1 : 0;
      this.e.TerOpFlags &= -257;
      if (num == 0)
        return false;
      if (this.e.TerArg.LineLimit > 0 && this.e.TotalLines + count > this.e.TerArg.LineLimit)
        this.PrintError(88, "WrapParseBuffer");
    }
    if (count == 0)
      return false;
    this.e.LastBufferedLine += count;
    if (count < 0)
    {
      for (int index = -count; index > 0; --index)
      {
        if (StartLine < this.e.TotalLines && (this.e.text[StartLine - index].flags & 32 /*0x20*/) != 0)
          this.e.text[StartLine].flags |= 32 /*0x20*/;
        if (StartLine < this.e.TotalLines && (this.e.text[StartLine - index].flags & 4096 /*0x1000*/) != 0)
          this.e.text[StartLine].flags |= 4096 /*0x1000*/;
        this.init.FreeLine(StartLine - index);
      }
    }
    this.FarMoveOl(this.e.text, StartLine, StartLine + count, this.e.TotalLines - StartLine);
    this.e.TotalLines += count;
    if (count > 0)
    {
      for (int index = 0; index < count; ++index)
        this.InitLine(StartLine + index);
      if (count == 1 && StartLine + 1 < this.e.TotalLines)
        this.e.text[StartLine].y = this.e.text[StartLine + 1].y;
    }
    if (this.e.TotalLines <= 0)
    {
      this.e.TotalLines = 1;
      this.InitLine(0);
    }
    if (count > 0)
      this.AdjustSections(StartLine - 1, count);
    else
      this.AdjustSections(StartLine + count, count);
    return true;
  }

  internal bool IsAsianCharacter(char UChar, int CurFont)
  {
    if (UChar >= '\u0080' && (this.e.TerFont[CurFont].CharSet == (byte) 136 || this.e.TerFont[CurFont].CharSet == (byte) 134 || this.e.TerFont[CurFont].CharSet == (byte) 129 || this.e.TerFont[CurFont].CharSet == (byte) 128 /*0x80*/) || UChar >= '⸀' && UChar <= '龯' || UChar >= '豈' && UChar < '\uFAFF' || UChar >= '︰' && UChar < '﹏' || UChar >= '\uFF00' && UChar < '\uFFEF' || UChar >= '一' && UChar <= 'ꟿ')
      return true;
    return UChar >= '가' && UChar <= '\uD7AF';
  }

  internal bool IsAccentCharPos(int line, int col)
  {
    char[] txt = this.e.text[line].txt;
    return txt != null && col >= 0 && col < this.e.text[line].len && this.IsAccentChar(txt[col]);
  }

  internal bool IsAccentChar(char UChar) => UChar >= '̀' && UChar <= 'ͯ';

  internal bool IsRtlChar(char InChr)
  {
    ushort num = (ushort) InChr;
    if (num >= (ushort) 1536 /*0x0600*/ && num <= (ushort) 1791 /*0x06FF*/ || num >= (ushort) 1872 && num <= (ushort) 1901 || num >= (ushort) 64336 && num <= (ushort) 65023 || num >= (ushort) 65136 && num <= (ushort) 65279 || num >= (ushort) 1425 && num <= (ushort) 1479 || num >= (ushort) 1488 && num <= (ushort) 1514)
      return true;
    return num >= (ushort) 1520 && num <= (ushort) 1524;
  }

  internal bool IsIndianCharPos(int line, int col)
  {
    char[] txt = this.e.text[line].txt;
    return txt != null && col >= 0 && col < this.e.text[line].len && this.IsIndianChar(txt[col]);
  }

  internal bool IsIndianChar(char UChar)
  {
    if (UChar >= 'ऀ' && UChar <= 'ॿ' || UChar >= '\u0B80' && UChar <= '\u0BFF' || UChar >= 'ಂ' && UChar <= '೯' || UChar >= 'ఁ' && UChar <= '౯' || UChar >= 'ം' && UChar <= 'ඃ' || UChar >= 'ං' && UChar <= '෴' || UChar >= 'ঁ' && UChar <= '৻' || UChar >= 'ઁ' && UChar <= '૱' || UChar >= 'ਁ' && UChar <= 'ੵ' || UChar >= 'ଁ' && UChar <= '\u0B77' || UChar >= 'ก' && UChar <= 'ฺ')
      return true;
    return UChar >= '฿' && UChar <= '๛';
  }

  internal bool IsThaiChar(char UChar)
  {
    if (UChar >= 'ก' && UChar <= 'ฺ')
      return true;
    return UChar >= '฿' && UChar <= '๛';
  }

  private int UniscribeCharacterPlacement(
    Graphics gr,
    char[] text,
    IntPtr hFont,
    int count,
    int[] dx,
    int[] order,
    int[] CaretPos,
    bool UseScreenFonts)
  {
    IntPtr pssa = (IntPtr) 0;
    IntPtr ptr = (IntPtr) 0;
    IntPtr hDC = !UseScreenFonts || !this.e.InPrinting ? this.GetOpDC(gr) : this.GetDC((IntPtr) 0);
    IntPtr handle = this.SelectObject(hDC, hFont);
    if (COp.Win32.ScriptStringAnalyse(hDC, text, count, (int) ((double) count * 1.5 + 16.0), -1, 2784, 0, (IntPtr) 0, (IntPtr) 0, (int[]) null, (IntPtr) 0, (byte[]) null, out pssa) == 0 && COp.Win32.ScriptStringGetLogicalWidths(pssa, dx) == 0 && COp.Win32.ScriptStringGetOrder(pssa, order) == 0)
    {
      for (int icp = 0; icp < count; ++icp)
      {
        if (COp.Win32.ScriptStringCPtoX(pssa, icp, false, out CaretPos[icp]) != 0)
          goto label_6;
      }
      ptr = COp.Win32.ScriptString_pSize(pssa);
    }
label_6:
    COp.SIZE structure = (COp.SIZE) Marshal.PtrToStructure(ptr, typeof (COp.SIZE));
    int num = structure.cy << 16 /*0x10*/ | structure.cx;
    COp.Win32.ScriptStringFree(ref pssa);
    this.SelectObject(hDC, handle);
    if (UseScreenFonts && this.e.InPrinting)
    {
      this.ReleaseDC((IntPtr) 0, hDC);
      return num;
    }
    this.ReleaseOpDC(gr);
    return num;
  }

  internal new bool GetWrapCharWidth()
  {
    int index1 = 0;
    int index2 = -1;
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    bool flag4 = false;
    bool flag5 = false;
    bool flag6 = false;
    bool flag7 = true;
    bool UseScreenFonts = false;
    this.e.CharWidthWrapped = false;
    if (!this.e.HasVarWidthFont && !this.e.WrapHasUniChar || this.e.BufferLength == 0)
      return false;
    bool flag8 = this.e.TerArg.PrintView && !this.e.TerArg.FittedView;
    for (int index3 = 0; index3 <= this.e.BufferLength; ++index3)
    {
      bool flag9 = false;
      bool flag10 = false;
      bool flag11 = false;
      bool flag12 = false;
      bool flag13 = false;
      bool flag14 = false;
      if (index3 < this.e.BufferLength)
      {
        if (this.e.wrap[index3] > '\u007F')
          flag9 = true;
        if (flag9)
        {
          flag10 = this.IsAsianCharacter(this.e.wrap[index3], (int) this.e.WrapCfmt[index3]);
          flag12 = this.IsAccentChar(this.e.wrap[index3]);
          flag13 = this.IsRtlChar(this.e.wrap[index3]);
          flag14 = this.e.wrap[index3] >= '！' && this.e.wrap[index3] <= '～';
        }
        flag11 = this.IsIndianChar(this.e.wrap[index3]) || this.IsThaiChar(this.e.wrap[index3]);
      }
      if (index2 == -1)
      {
        if (this.e.wrap[index3] >= ' ')
        {
          index1 = (int) this.e.WrapCfmt[index3];
          index2 = index3;
          flag1 = flag9;
          flag4 = flag10;
          flag2 = flag11;
          flag3 = flag12;
          flag5 = flag13;
          flag6 = flag14;
        }
      }
      else if (index3 == this.e.BufferLength || index1 != (int) this.e.WrapCfmt[index3] || this.e.wrap[index3] < ' ' || this.e.wrap[index3] == '“' || flag1 != flag9 || flag4 != flag10 || flag2 != flag11 || flag3 != flag12 || flag6 != flag14)
      {
        if (index3 > index2 && ((this.e.TerFont[index1].VarWidth || this.e.TerFont[index1].rtl ? 1 : (flag1 ? 1 : (this.e.WrapTextFlow == 2 ? 1 : 0))) | (flag3 ? 1 : 0)) != 0 && (!this.True(this.e.TerFont[index1].style & 196608 /*0x030000*/) | flag1 || this.e.wrap[index2] == ' '))
        {
          if (this.e.TerFont[index1].rtl && this.e.WrapTextFlow != 2)
            this.e.WrapTextFlow = 2;
          if (this.False(this.e.WrapCharWidth) && (this.e.WrapCharWidth = new ushort[this.e.WrapBufferSize]) == null || this.False(this.e.WrapCharWidthOrder) && (this.e.WrapCharWidthOrder = new int[this.e.WrapBufferSize]) == null || this.False(this.e.WrapCharWidthDX) && (this.e.WrapCharWidthDX = new int[this.e.WrapBufferSize]) == null || this.False(this.e.WrapCharWidthCP) && (this.e.WrapCharWidthCP = new int[this.e.WrapBufferSize]) == null || this.False(this.e.WrapCharWidthText) && (this.e.WrapCharWidthText = new ushort[this.e.WrapBufferSize]) == null || this.False(this.e.WrapCharWidthClass) && (this.e.WrapCharWidthClass = new byte[this.e.WrapBufferSize]) == null)
            return false;
          if (!this.e.CharWidthWrapped)
          {
            for (int index4 = 0; index4 < this.e.BufferLength; ++index4)
              this.e.WrapCharWidth[index4] = (ushort) 0;
          }
          if (flag3)
          {
            for (int index5 = index2; index5 < index3; ++index5)
              this.e.WrapCharWidth[index5] = (ushort) 16384 /*0x4000*/;
            this.e.CharWidthWrapped = true;
          }
          else if (flag6)
          {
            for (int index6 = index2; index6 < index3; ++index6)
            {
              if (flag8)
                this.e.WrapCharWidth[index6] = (ushort) this.TwipsToUnitX(this.e.TerFont[index1].TwipsSize);
              else
                this.e.WrapCharWidth[index6] = (ushort) this.TwipsToScrX(this.e.TerFont[index1].TwipsSize);
              this.e.WrapCharWidth[index6] |= (ushort) 16384 /*0x4000*/;
            }
            this.e.CharWidthWrapped = true;
          }
          else if (flag7 & flag4 && !this.e.TerFont[index1].rtl)
          {
            for (int index7 = index2; index7 < index3; ++index7)
            {
              char ch = this.e.wrap[index7];
              bool flag15 = true;
              int num = 1;
              if (ch >= 'ﺀ')
                num = 2;
              else if (ch == '・')
                flag15 = false;
              if (flag8)
                this.e.WrapCharWidth[index7] = (ushort) this.TwipsToUnitX(this.e.TerFont[index1].TwipsSize / num);
              else
                this.e.WrapCharWidth[index7] = (ushort) this.TwipsToScrX(this.e.TerFont[index1].TwipsSize / num);
              if (flag15)
                this.e.WrapCharWidth[index7] |= (ushort) 16384 /*0x4000*/;
            }
            this.e.CharWidthWrapped = true;
          }
          else
          {
            int num1 = index3 - index2;
            if (flag2)
              UseScreenFonts = true;
            Graphics gr = !flag8 | UseScreenFonts ? this.e.TerFont[index1].gr : this.e.PrtFont[index1].gr;
            IntPtr hFont = !flag8 | UseScreenFonts ? this.e.TerFont[index1].hFont : this.e.PrtFont[index1].hFont;
            Font font = !flag8 | UseScreenFonts ? this.e.TerFont[index1].font : this.e.PrtFont[index1].font;
            COp.GCP_RESULTS lpResults = new COp.GCP_RESULTS();
            lpResults.lStructSize = 36;
            lpResults.lpOutString = IntPtr.Zero;
            lpResults.lpOrder = Marshal.AllocCoTaskMem(num1 * Marshal.SizeOf((object) this.e.WrapCharWidthOrder[0]));
            lpResults.lpDx = Marshal.AllocCoTaskMem(num1 * Marshal.SizeOf((object) this.e.WrapCharWidthDX[0]));
            lpResults.lpCaretPos = Marshal.AllocCoTaskMem(num1 * Marshal.SizeOf((object) this.e.WrapCharWidthCP[0]));
            lpResults.lpClass = Marshal.AllocCoTaskMem(num1 * Marshal.SizeOf((object) this.e.WrapCharWidthClass[0]));
            lpResults.lpGlyphs = IntPtr.Zero;
            lpResults.nGlyphs = num1;
            lpResults.nMaxFit = 0;
            char[] chArray = new char[num1 + 1];
            int index8;
            for (index8 = 0; index8 < num1; ++index8)
            {
              chArray[index8] = this.e.wrap[index2 + index8];
              if (this.True(this.e.TerFont[index1].style & 196608 /*0x030000*/))
                chArray[index8] = char.ToUpper(chArray[index8]);
            }
            chArray[index8] = char.MinValue;
            int val;
            if (flag2)
            {
              val = this.UniscribeCharacterPlacement(gr, chArray, hFont, num1, this.e.WrapCharWidthDX, this.e.WrapCharWidthOrder, this.e.WrapCharWidthCP, UseScreenFonts);
            }
            else
            {
              val = this.GetCharacterPlacement(gr, chArray, hFont, num1, 0, ref lpResults, 18);
              int ofs1 = 0;
              int ofs2 = 0;
              int num2 = Marshal.SizeOf(Type.GetType("System.Int32"));
              for (int index9 = 0; index9 < num1; ++index9)
              {
                this.e.WrapCharWidthOrder[index9] = Marshal.ReadInt32(lpResults.lpOrder, ofs1);
                this.e.WrapCharWidthDX[index9] = Marshal.ReadInt32(lpResults.lpDx, ofs1);
                if (lpResults.lpCaretPos == IntPtr.Zero)
                  this.e.WrapCharWidthCP[index9] = 0;
                else
                  this.e.WrapCharWidthCP[index9] = Marshal.ReadInt32(lpResults.lpCaretPos, ofs1);
                this.e.WrapCharWidthClass[index9] = Marshal.ReadByte(lpResults.lpClass, ofs2);
                ofs1 += num2;
                ++ofs2;
              }
            }
            Marshal.FreeCoTaskMem(lpResults.lpOrder);
            Marshal.FreeCoTaskMem(lpResults.lpDx);
            if (lpResults.lpCaretPos != IntPtr.Zero)
              Marshal.FreeCoTaskMem(lpResults.lpCaretPos);
            Marshal.FreeCoTaskMem(lpResults.lpClass);
            int[] wrapCharWidthOrder = this.e.WrapCharWidthOrder;
            int[] wrapCharWidthDx = this.e.WrapCharWidthDX;
            int[] CaretPos = this.e.WrapCharWidthCP;
            bool flag16 = false;
            if (COp.LOWORD(val) == (ushort) 0)
              val = this.UniscribeCharacterPlacement(gr, chArray, hFont, num1, wrapCharWidthDx, wrapCharWidthOrder, CaretPos, UseScreenFonts);
            COp.SIZE size;
            if (COp.LOWORD(val) == (ushort) 0)
            {
              int num3 = 0;
              char[] str = new char[10];
              flag16 = true;
              for (int index10 = 0; index10 < num1; ++index10)
              {
                str[0] = chArray[index10];
                this.GetTextExtentPoint(gr, hFont, str, 1, out size);
                wrapCharWidthDx[index10] = size.cx;
                num3 += wrapCharWidthDx[index10];
                wrapCharWidthOrder[index10] = index10;
              }
              this.GetTextExtentPoint(gr, hFont, chArray, num1, out size);
              int cx = size.cx;
              if (cx != num3)
              {
                for (int index11 = 0; index11 < num1 - 1; ++index11)
                {
                  str[0] = chArray[index11];
                  str[1] = chArray[index11 + 1];
                  this.GetTextExtentPoint(gr, hFont, str, 2, out size);
                  int num4 = size.cx - wrapCharWidthDx[index11];
                  if (num4 < wrapCharWidthDx[index11 + 1])
                  {
                    wrapCharWidthDx[index11 + 1] = num4;
                    ++index11;
                  }
                }
                int num5 = 0;
                for (int index12 = 0; index12 < num1; ++index12)
                  num5 += wrapCharWidthDx[index12];
                int num6 = cx - num5;
                if (num6 != 0)
                {
                  int num7 = 0;
                  for (int index13 = 0; index13 < num1; ++index13)
                  {
                    wrapCharWidthDx[index13] += num6 / num1;
                    wrapCharWidthDx[index13] += num7;
                    num7 = 0;
                    if (wrapCharWidthDx[index13] < 0)
                    {
                      num7 += wrapCharWidthDx[index13];
                      wrapCharWidthDx[index13] = 0;
                    }
                  }
                  int num8 = num6 % num1 + num7;
                  if (num8 > 0)
                  {
                    for (int index14 = 0; index14 < num8 && index14 < num1; ++index14)
                      ++wrapCharWidthDx[index14];
                  }
                }
              }
              val = cx;
              if (val != 0)
                CaretPos = (int[]) null;
            }
            if (val != 0 && this.True(wrapCharWidthDx))
            {
              int num9 = 0;
              for (int index15 = 0; index15 < num1; ++index15)
                num9 += wrapCharWidthDx[index15];
              int num10 = -1;
              for (int index16 = 0; index16 < num1; ++index16)
              {
                int x;
                if (this.True(wrapCharWidthOrder))
                {
                  if (this.True((int) this.e.WrapCharWidthClass[index16] & 2) | flag5 || CaretPos == null)
                  {
                    int index17 = wrapCharWidthOrder[index16];
                    if (num10 == index17)
                    {
                      x = 0;
                    }
                    else
                    {
                      x = wrapCharWidthDx[index17];
                      if (x < 0)
                        x = 0;
                      if (x > 28000 && !flag16)
                      {
                        this.GetTextExtentPoint(gr, font, new string(chArray[index16], 1), 1, out size);
                        x = size.cx;
                      }
                    }
                    num10 = index17;
                  }
                  else
                  {
                    x = index16 + 1 >= num1 ? num9 - CaretPos[index16] : CaretPos[index16 + 1] - CaretPos[index16];
                    if (x < 0)
                      x = -x;
                  }
                }
                else
                  x = wrapCharWidthDx[index16];
                if (flag8 & UseScreenFonts)
                  x = this.ScrToUnitX(x);
                if (this.True(this.e.TerFont[index1].expand))
                {
                  if (flag8)
                    x += this.TwipsToUnitY(this.e.TerFont[index1].expand);
                  else
                    x += this.TwipsToScrY(this.e.TerFont[index1].expand);
                  if (x < 0)
                    x = 0;
                }
                this.e.WrapCharWidth[index2 + index16] = (ushort) (x | 16384 /*0x4000*/);
                if (((this.True((int) this.e.WrapCharWidthClass[index16] & 2) || this.e.wrap[index2 + index16] >= '\u0600' && this.e.wrap[index2 + index16] <= 'ۿ' ? 1 : (this.e.wrap[index2 + index16] < 'ﭐ' || this.e.wrap[index2 + index16] > '\uFDFF' ? (this.e.wrap[index2 + index16] < 'ﹰ' ? 0 : (this.e.wrap[index2 + index16] <= '\uFEFF' ? 1 : 0)) : 1)) | (flag5 ? 1 : 0)) != 0)
                {
                  this.e.WrapCharWidth[index2 + index16] |= (ushort) 32768 /*0x8000*/;
                  this.e.WrapTextFlow = 2;
                }
                else if (this.e.WrapTextFlow == 2 && (this.e.wrap[index2 + index16] == ' ' || this.e.wrap[index2 + index16] == '[' || this.e.wrap[index2 + index16] == ']' || this.e.wrap[index2 + index16] == '(' || this.e.wrap[index2 + index16] == ')'))
                {
                  bool flag17 = false;
                  if (index2 + index16 == 0)
                  {
                    if (this.e.wrap[index2 + index16 + 1] < '\u0080')
                      flag17 = true;
                  }
                  else
                    flag17 = this.e.wrap[index2 + index16 - 1] < '\u0080' && this.e.wrap[index2 + index16 - 1] != ' ' || this.e.wrap[index2 + index16 + 1] < '\u0080' && this.e.wrap[index2 + index16 + 1] != ' ';
                  if (!flag17)
                    this.e.WrapCharWidth[index2 + index16] |= (ushort) 32768 /*0x8000*/;
                }
              }
              this.e.CharWidthWrapped = true;
            }
          }
        }
        if (index3 != this.e.BufferLength)
        {
          index1 = (int) this.e.WrapCfmt[index3];
          index2 = index3;
          if (this.e.wrap[index3] < ' ')
            ++index2;
          flag1 = flag9;
          flag4 = flag10;
          flag2 = flag11;
          flag3 = flag12;
          flag6 = flag14;
          UseScreenFonts = false;
        }
        else
          break;
      }
    }
    return this.e.CharWidthWrapped;
  }

  internal new bool IsFirstParaLine(int LineNo)
  {
    if (LineNo == 0)
      return true;
    int LineNo1 = LineNo - 1;
    while (LineNo1 >= 0 && this.LineInfo(LineNo1, 12))
      --LineNo1;
    return LineNo1 < 0 || (this.e.text[LineNo1].flags & 3) != 0;
  }

  internal new bool RestoreWrapHilight(
    int HilightBeg,
    int HilightEnd,
    bool BegHilightAtLineEnd,
    bool EndHilightAtLineEnd,
    bool SelectAll)
  {
    if (this.e.HilightType == 2)
    {
      int row;
      int col;
      this.AbsToRowCol(HilightBeg, out row, out col);
      this.e.HilightBegRow = row;
      this.e.HilightBegCol = col;
      if (BegHilightAtLineEnd && this.e.HilightBegRow > 0 && this.e.HilightBegCol == 0)
      {
        --this.e.HilightBegRow;
        this.e.HilightBegCol = this.e.text[this.e.HilightBegRow].len;
      }
      this.AbsToRowCol(HilightEnd, out row, out col);
      this.e.HilightEndRow = row;
      this.e.HilightEndCol = col;
      if (EndHilightAtLineEnd)
      {
        int index = this.e.HilightEndCol == 0 ? this.e.HilightEndRow - 1 : this.e.HilightEndRow;
        int cid = index >= this.e.TotalLines || index < 0 ? 0 : this.e.text[index].cid;
        if (this.e.HilightEndRow == this.e.TotalLines - 1 && this.e.HilightEndCol == this.e.text[this.e.HilightEndRow].len - 1 && cid == 0)
          ++this.e.HilightEndCol;
        else if (this.e.HilightEndRow > 0 && this.e.HilightEndCol == 0)
        {
          --this.e.HilightEndRow;
          this.e.HilightEndCol = this.e.text[this.e.HilightEndRow].len;
        }
      }
      if (SelectAll)
      {
        this.e.HilightBegRow = 0;
        this.e.HilightBegCol = 0;
        this.e.HilightEndRow = this.e.TotalLines - 1;
        this.e.HilightEndCol = this.e.text[this.e.HilightEndRow].len;
      }
    }
    return true;
  }

  internal new bool SaveWrapHilight(
    out int pHilightBeg,
    out int pHilightEnd,
    out bool pBegHilightAtLineEnd,
    out bool pEndHilightAtLineEnd,
    out bool pSelectAll)
  {
    int num1 = 0;
    int num2 = 0;
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    if (this.e.HilightBegRow >= this.e.TotalLines || this.e.HilightEndRow >= this.e.TotalLines)
      this.e.HilightType = 0;
    if (this.e.HilightType == 2)
    {
      flag3 = this.e.HilightBegRow == 0 && this.e.HilightBegCol == 0 && this.e.HilightEndRow == this.e.TotalLines - 1 && this.e.HilightEndCol == this.e.text[this.e.HilightEndRow].len;
      num1 = this.RowColToAbs(this.e.HilightBegRow, this.e.HilightBegCol);
      num2 = this.RowColToAbs(this.e.HilightEndRow, this.e.HilightEndCol);
      flag1 = this.e.TerArg.WordWrap && num1 > num2 && this.e.HilightBegCol == this.e.text[this.e.HilightBegRow].len && this.LineEndsInBreak(this.e.HilightBegRow);
      flag2 = this.e.TerArg.WordWrap && num2 > num1 && this.e.HilightEndCol == this.e.text[this.e.HilightEndRow].len && this.LineEndsInBreak(this.e.HilightEndRow);
    }
    pHilightBeg = num1;
    pHilightEnd = num2;
    pBegHilightAtLineEnd = flag1;
    pEndHilightAtLineEnd = flag2;
    pSelectAll = flag3;
    return true;
  }

  internal bool TerRewrap()
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.e.WordWrapSuspended)
      return true;
    if (!this.e.TerArg.WordWrap)
      return false;
    this.WordWrap(0, this.e.TotalLines);
    return true;
  }

  internal bool TerSetWrapWidth(int WidthChars, int WidthTwips, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.WordWrap || this.e.TerArg.PrintView || WidthChars > 0 && WidthTwips > 0)
      return false;
    this.e.WrapWidthChars = WidthChars;
    this.e.WrapWidthTwips = WidthTwips;
    if (repaint)
      this.PaintTer();
    return true;
  }

  internal new int TerWrapWidth(int lin, int sect) => this.TerWrapWidth2(lin, sect, true);

  internal new int TerWrapWidth2(int lin, int sect, bool screen)
  {
    bool flag = (this.e.TerFlags6 & 8192 /*0x2000*/) != 0;
    if (sect < 0 && lin < 0)
      return !screen ? this.ScrToTwipsX(this.e.TerWinWidth) : this.e.TerWinWidth;
    if (this.e.TerArg.WordWrap && this.e.TerArg.PrintView)
    {
      if (lin >= 0)
      {
        int cid;
        if ((cid = this.e.text[lin].cid) > 0)
        {
          int num = this.e.cell[cid].TextAngle;
          if (num > 0 && this.e.text[lin].fid > 0 && this.e.ParaFrame[this.e.text[lin].fid].TextAngle > 0)
            num = 0;
          int x;
          if (num == 0)
          {
            x = this.e.cell[cid].width - 2 * this.e.cell[cid].margin;
          }
          else
          {
            int row = this.e.cell[cid].row;
            x = this.UnitToTwipsX(this.e.TableRow[row].height);
            if (this.e.TableRow[row].MinHeight < 0)
              x = -this.e.TableRow[row].MinHeight;
            else if (this.e.TableRow[row].MinHeight > x)
              x = this.e.TableRow[row].MinHeight;
          }
          return !screen ? x : this.TwipsToScrX(x);
        }
        if (this.e.text[lin].fid > 0)
        {
          int fid = this.e.text[lin].fid;
          int margin = this.e.ParaFrame[fid].margin;
          if ((this.e.ParaFrame[fid].flags & 128 /*0x80*/) != 0)
            margin += this.e.ParaFrame[fid].LineWdth;
          int x = this.e.AllTextAngle > 0 || this.e.ParaFrame[fid].TextAngle > 0 ? this.e.ParaFrame[fid].height - 2 * margin : this.e.ParaFrame[fid].width - 2 * margin;
          if (this.e.ParaFrame[fid].height < this.UnitToTwipsY(this.e.text[lin].height * 2))
            x += 40;
          return !screen ? x : this.TwipsToScrX(x);
        }
        if ((this.e.PfmtId[this.e.text[lin].pfmt].flags & 12288 /*0x3000*/) != 0)
        {
          if (sect < 0)
            sect = this.sec.GetSection(lin);
          return screen ? (int) ((double) this.e.ScrResX * ((double) this.e.TerSect1[sect].PgWidth - (double) this.e.TerSect[sect].LeftMargin - (double) this.e.TerSect[sect].RightMargin)) : (int) ((double) this.e.UnitResX * ((double) this.e.TerSect1[sect].PgWidth - (double) this.e.TerSect[sect].LeftMargin - (double) this.e.TerSect[sect].RightMargin));
        }
        if (sect < 0)
          sect = this.sec.GetSection(lin);
        int num1 = !(this.e.TerArg.FittedView | flag) || this.e.TerWinWidth <= 0 ? (!screen ? (int) ((double) this.e.UnitResX * ((double) this.e.TerSect1[sect].PgWidth - (double) this.e.TerSect[sect].LeftMargin - (double) this.e.TerSect[sect].RightMargin)) : (int) ((double) this.e.ScrResX * ((double) this.e.TerSect1[sect].PgWidth - (double) this.e.TerSect[sect].LeftMargin - (double) this.e.TerSect[sect].RightMargin))) : (screen ? this.e.TerWinWidth : this.ScrToTwipsX(this.e.TerWinWidth));
        return (!screen ? num1 - (int) ((double) this.e.UnitResX * ((double) this.e.TerSect[sect].ColumnSpace * (double) (this.e.TerSect[sect].columns - 1))) : num1 - (int) ((double) this.e.ScrResX * ((double) this.e.TerSect[sect].ColumnSpace * (double) (this.e.TerSect[sect].columns - 1)))) / this.e.TerSect[sect].columns;
      }
      if (sect < 0)
        sect = 0;
      return this.e.TerArg.FittedView && this.e.TerWinWidth > 0 ? (!screen ? this.ScrToTwipsX(this.e.TerWinWidth) : this.e.TerWinWidth) : (screen ? (int) ((double) this.e.ScrResX * ((double) this.e.TerSect1[sect].PgWidth - (double) this.e.TerSect[sect].LeftMargin - (double) this.e.TerSect[sect].RightMargin)) : (int) ((double) this.e.UnitResX * ((double) this.e.TerSect1[sect].PgWidth - (double) this.e.TerSect[sect].LeftMargin - (double) this.e.TerSect[sect].RightMargin)));
    }
    if (sect < 0 && lin < 0)
      return !screen ? this.ScrToTwipsX(this.e.TerWinWidth) : this.e.TerWinWidth;
    if (this.e.WrapWidthTwips > 0)
    {
      if (sect < 0)
        sect = this.sec.GetSection(lin);
      if (screen)
      {
        int num = this.TwipsToScrX(this.e.WrapWidthTwips) - (int) ((double) this.e.ScrResX * ((double) this.e.TerSect[sect].LeftMargin + (double) this.e.TerSect[sect].RightMargin));
        if (num < this.e.ScrResX)
          num = this.e.ScrResX;
        return num;
      }
      int num2 = this.e.WrapWidthTwips - (int) ((double) this.e.UnitResX * ((double) this.e.TerSect[sect].LeftMargin + (double) this.e.TerSect[sect].RightMargin));
      if (num2 < this.ScrToUnitX(this.e.UnitResX))
        num2 = this.e.UnitResX;
      return num2;
    }
    if (this.e.WrapWidthChars > 0)
      return this.fnt.LwrCharWidth(0, screen, 'M') * this.e.WrapWidthChars;
    int x1 = this.e.TerWinWidth >= this.e.ScrResX ? this.e.TerWinWidth : this.e.ScrResX;
    if (!screen)
      x1 = this.ScrToTwipsX(x1);
    return x1;
  }

  internal new bool WordWrap(int StartLine, int WrapLines)
  {
    if (this.e.WordWrapSuspended)
      return true;
    char minValue = char.MinValue;
    bool flag1 = false;
    int num1 = 0;
    int curLine = this.e.CurLine;
    bool flag2 = false;
    bool pBegHilightAtLineEnd = false;
    bool pEndHilightAtLineEnd = false;
    bool pSelectAll = false;
    bool flag3 = StartLine == 0 && WrapLines == this.e.TotalLines;
    if (tc.DebugMode)
      this.misc.dm(nameof (WordWrap));
    if (!this.e.TerArg.WordWrap)
      return true;
    if (StartLine > this.e.CurLine)
      StartLine = this.e.CurLine;
    if (StartLine < 0)
      StartLine = 0;
    if (this.e.CurLine > StartLine + WrapLines)
      WrapLines = this.e.CurLine - StartLine + this.e.WinHeight;
    if (this.e.WrapPending && this.e.WrapFlag == 2)
      this.e.WrapFlag = 3;
    if (this.e.WrapPending && this.e.WrapFlag == 1)
      this.e.WrapFlag = 2;
    if (this.e.WrapFlag == 4)
      this.e.WrapFlag = 3;
    if (this.e.WrapFlag == 1 && (this.e.text[this.e.CurLine].tabw == null || (this.e.text[this.e.CurLine].tabw.type & 1071) == 0) && (this.e.text[this.e.CurLine].tabw == null || this.e.text[this.e.CurLine].tabw.CharFlags == null) && (this.e.text[this.e.CurLine].flags & 1966080 /*0x1E0000*/) == 0)
    {
      if (this.e.AllTextAngle > 0 || this.e.text[this.e.CurLine].fid != 0 && this.e.ParaFrame[this.e.text[this.e.CurLine].fid].TextAngle > 0)
        this.e.PaintFlag = 4;
      else if (this.e.text[this.e.CurLine].cid != 0 && this.e.cell[this.e.text[this.e.CurLine].cid].TextAngle > 0)
        this.e.PaintFlag = 4;
      else if (((this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].flags & 12288 /*0x3000*/) == 0 || this.e.text[this.e.CurLine].len <= 1 || this.e.text[this.e.CurLine].height != 0) && (this.e.WrapWidthChars <= 0 || this.e.text[this.e.CurLine].len <= this.e.WrapWidthChars) && (!this.e.TerArg.PrintView || this.e.text[this.e.CurLine].height != 0 || this.e.text[this.e.CurLine].len <= 0))
      {
        int num2 = this.TerWrapWidth(this.e.CurLine, -1) - this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].LeftIndent - this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].RightIndent;
        if ((this.e.text[this.e.CurLine].flags & 4) != 0)
          num2 -= this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].FirstIndent;
        int num3 = this.e.CurFrame < 0 || this.e.CurFrame >= this.e.TotalFrames ? num2 + this.e.frame[0].x : num2 + this.e.frame[this.e.CurFrame].x;
        if (this.e.CurLine == 0 || (this.e.text[this.e.CurLine - 1].flags & 3) != 0)
          num3 += this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].FirstIndent;
        if (this.e.text[this.e.CurLine].len > 0)
        {
          if (this.e.text[this.e.CurLine].tabw != null && (this.e.text[this.e.CurLine].tabw.type & 128 /*0x80*/) != 0 && this.e.TerArg.PrintView && this.MessagePending())
            flag2 = true;
          if (flag2)
            this.e.text[this.e.CurLine].tabw.type = tc.ResetUintFlag(ref this.e.text[this.e.CurLine].tabw.type, 128 /*0x80*/);
          int units = this.ColToUnits(this.e.text[this.e.CurLine].len, this.e.CurLine, 1024 /*0x0400*/);
          if (flag2)
            this.e.text[this.e.CurLine].tabw.type |= 128 /*0x80*/;
          int num4 = num3;
          if (units >= num4)
          {
            if ((this.e.TerOpFlags & 4) != 0 && this.e.RepageBeginLine > this.e.CurLine)
            {
              this.e.RepageBeginLine = this.e.CurLine;
              goto label_46;
            }
            goto label_46;
          }
        }
        char[] txt = this.e.text[this.e.CurLine].txt;
        int len = this.e.text[this.e.CurLine].len;
        if (len > 0 && this.lstrchr(this.e.BreakChars, txt[len - 1]))
          flag1 = true;
        if (!flag1)
        {
          if (this.e.HilightType == 0 && (this.e.PaintFlag == 4 || this.e.PaintFlag == 5))
            this.e.PaintFlag = 2;
          if (!this.e.RepagePending)
            this.e.RepageBeginLine = this.e.TotalLines;
          return true;
        }
      }
    }
    int totalLines;
    bool flag4;
    string str1;
    string str2;
    do
    {
      int modified = this.e.TerArg.modified;
      totalLines = this.e.TotalLines;
      flag4 = this.e.EditLine == this.e.CurLine && this.e.EditCol == this.e.CurCol;
      int pHilightBeg;
      int pHilightEnd;
      this.SaveWrapHilight(out pHilightBeg, out pHilightEnd, out pBegHilightAtLineEnd, out pEndHilightAtLineEnd, out pSelectAll);
      if (this.e.WrapFlag == 2)
      {
        StartLine = this.e.CurLine;
        if (this.e.CurLine > 0 && this.e.text[this.e.CurLine - 1].len > 0 && (int) this.e.text[this.e.CurLine - 1].txt[this.e.text[this.e.CurLine - 1].len - 1] != (int) this.e.ParaChar)
          --StartLine;
      }
      int num5 = 0;
      if (this.e.TotalLines > 0)
      {
        for (int index = StartLine; index < this.e.CurLine; ++index)
        {
          int len = this.e.text[index].len;
          if ((this.e.text[index].flags2 & 512 /*0x0200*/) != 0 && len > 0 && this.e.text[index].txt[len - 1] == '\u0006')
            --len;
          num5 += len;
        }
        char[] txt = this.e.text[this.e.CurLine].txt;
        if (this.e.text[this.e.CurLine].len > 0)
          minValue = txt[this.e.text[this.e.CurLine].len - 1];
        if (this.e.text[this.e.CurLine].len > 0 && this.e.CurCol >= this.e.text[this.e.CurLine].len && (int) minValue == (int) this.e.ParaChar)
          num5 += this.e.text[this.e.CurLine].len + 1;
        else
          num5 += this.e.CurCol + 1;
      }
      int pfmt = this.e.TotalLines <= 0 ? 0 : this.e.text[this.e.TotalLines - 1].pfmt;
      int num6 = 0;
      while (num6 <= WrapLines | flag3 && StartLine + num6 < this.e.TotalLines)
      {
        int lastWrappedLine = this.e.LastWrappedLine;
        this.WrapMakeBuffer(StartLine + num6, WrapLines - num6 + 1);
        this.WrapParseBuffer(StartLine + num6);
        if (this.e.LastBufferedLine > this.e.LastWrappedLine + 25 || this.e.BufferLength == 0 || this.e.LastBufferedLine + 1 >= this.e.TotalLines)
        {
          this.DisplacePointers(this.e.LastBufferedLine + 1, this.e.LastWrappedLine - this.e.LastBufferedLine);
          this.e.LastBufferedLine = this.e.LastWrappedLine;
        }
        else if (this.e.LastBufferedLine > this.e.LastWrappedLine)
        {
          for (int line = this.e.LastWrappedLine + 1; line <= this.e.LastBufferedLine; ++line)
          {
            this.FreeLine(line);
            this.InitLine(line);
            this.e.text[line].pfmt = 0;
          }
        }
        if (this.e.LastWrappedLine < 0)
          this.e.LastWrappedLine = 0;
        if ((this.e.text[this.e.LastWrappedLine].flags & 131) == 0 && this.e.LastWrappedLine > StartLine && this.e.LastWrappedLine + 1 <= this.e.TotalLines && this.e.LastWrappedLine < StartLine + WrapLines - 1 && this.e.LastWrappedLine > lastWrappedLine + 1)
          --this.e.LastWrappedLine;
        num6 = this.e.LastWrappedLine - StartLine + 1;
        if (this.e.WrapFlag == 2 && this.e.LastWrappedLine >= this.e.CurLine)
          break;
      }
      this.DisplacePointers(this.e.LastBufferedLine + 1, this.e.LastWrappedLine - this.e.LastBufferedLine);
      if (this.e.TotalLines == 0)
      {
        ++this.e.TotalLines;
        this.e.CurLine = this.e.TotalLines - 1;
        this.InitLine(this.e.CurLine);
      }
      for (this.e.CurLine = StartLine; this.e.CurLine < this.e.TotalLines; ++this.e.CurLine)
      {
        int len = this.e.text[this.e.CurLine].len;
        if ((this.e.text[this.e.CurLine].flags2 & 512 /*0x0200*/) != 0 && len > 0 && this.e.text[this.e.CurLine].txt[len - 1] == '\u0006')
          --len;
        if (num5 > len)
          num5 -= len;
        else
          break;
      }
      this.e.CurCol = num5 - 1;
      if (this.e.CurLine >= this.e.TotalLines)
      {
        this.e.CurLine = this.e.TotalLines - 1;
        this.e.CurCol += this.e.text[this.e.CurLine].len;
      }
      char[] txt1 = this.e.text[this.e.CurLine].txt;
      if (this.e.text[this.e.CurLine].len > 0)
        minValue = txt1[this.e.text[this.e.CurLine].len - 1];
      if (this.e.text[this.e.CurLine].len > 0 && this.e.CurCol >= this.e.text[this.e.CurLine].len && (int) minValue == (int) this.e.ParaChar)
      {
        if (this.e.CurLine + 1 >= this.e.TotalLines)
        {
          this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
          if (this.e.CurCol < 0)
            this.e.CurCol = 0;
        }
        else
        {
          this.e.CurCol -= this.e.text[this.e.CurLine].len;
          ++this.e.CurLine;
        }
      }
      if (this.e.CurLine >= this.e.TotalLines)
      {
        ++this.e.TotalLines;
        this.e.CurLine = this.e.TotalLines - 1;
        this.InitLine(this.e.CurLine);
      }
      if (this.e.CurLine == this.e.TotalLines - 1 && !this.e.RotatedFrame && ((this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].flags & 12288 /*0x3000*/) != 0 || this.e.text[this.e.CurLine].fid > 0 || this.e.text[this.e.CurLine].cid > 0))
      {
        ++this.e.TotalLines;
        this.e.CurLine = this.e.TotalLines - 1;
        this.InitLine(this.e.CurLine);
        this.e.text[this.e.CurLine].fid = 0;
        this.e.text[this.e.CurLine].cid = 0;
      }
      if (this.e.text[this.e.CurLine].len == 0)
      {
        this.LineAlloc(this.e.CurLine, 0, 1);
        this.e.text[this.e.CurLine].txt[0] = this.e.ParaChar;
        this.OpenCfmt(this.e.CurLine)[0] = (ushort) 0;
        this.CloseCfmt(this.e.CurLine);
        this.e.text[this.e.CurLine].pfmt = pfmt;
        if (this.e.CurLine == this.e.TotalLines - 1 && ((this.e.PfmtId[pfmt].flags & 12288 /*0x3000*/) != 0 || this.e.text[this.e.CurLine].fid > 0))
          this.e.text[this.e.CurLine].pfmt = 0;
      }
      if (this.e.CurCol > this.e.text[this.e.CurLine].len)
        this.e.CurCol = this.e.text[this.e.CurLine].len;
      if (this.e.CurCol < 0)
        this.e.CurCol = 0;
      this.e.CurRow = this.e.CurLine - this.e.BeginLine;
      if (this.e.CurRow >= this.e.WinHeight && !this.e.TerArg.PageMode)
      {
        this.e.CurRow = this.e.WinHeight - 1;
        this.e.BeginLine = this.e.CurLine - this.e.CurRow;
      }
      this.e.TerArg.modified = modified;
      this.RestoreWrapHilight(pHilightBeg, pHilightEnd, pBegHilightAtLineEnd, pEndHilightAtLineEnd, pSelectAll);
      if (this.e.HilightType != 0 & pEndHilightAtLineEnd && this.e.CurLine == this.e.HilightEndRow + 1 && this.e.CurCol == 0 && ((this.e.text[this.e.CurLine].flags & 1966080 /*0x1E0000*/) != 0 || this.LineInfo(this.e.CurLine, 32 /*0x20*/)))
        this.PrevTextPos();
      ++num1;
      if (!this.e.TerArg.PageMode && this.e.CurLine > 0 && this.IsListLine(this.e.CurLine) && this.IsListLine(this.e.CurLine - 1) && num1 == 1)
      {
        str1 = "";
        str2 = "";
        if (this.e.text[this.e.CurLine].tabw != null)
          str1 = this.e.text[this.e.CurLine].tabw.ListText;
        if (this.e.text[this.e.CurLine - 1].tabw != null)
          str2 = this.e.text[this.e.CurLine - 1].tabw.ListText;
        continue;
      }
      break;
label_46:;
    }
    while (str1 == null || str1 == str2);
    if (flag4)
    {
      this.e.EditLine = this.e.CurLine;
      this.e.EditCol = this.e.CurCol;
    }
    this.e.WrapPending = false;
    if (this.e.RepageBeginLine < StartLine)
      this.e.RepageBeginLine = StartLine;
    if (this.e.TotalLines != totalLines && this.e.PaintFlag != 4 && this.e.PaintFlag != 6)
      this.e.PaintFlag = 4;
    if (this.LineInfo(this.e.CurLine, 1024 /*0x0400*/) && this.e.PaintFlag != 4 && this.e.PaintFlag != 6)
      this.e.PaintFlag = 4;
    if (this.e.TotalLines != totalLines && (this.e.TerOpFlags & 4) != 0 && this.e.RepageBeginLine > this.e.CurLine - 1)
      this.e.RepageBeginLine = this.e.CurLine > 0 ? this.e.CurLine - 1 : this.e.CurLine;
    return true;
  }

  internal new bool WrapMakeBuffer(int StartLine, int WrapLines)
  {
    this.e.WrapSect = 0;
    this.e.WrapHasUniChar = false;
    this.e.CharWidthWrapped = false;
    this.e.TagsWrapped = false;
    this.e.WrapTextFlow = 0;
    this.e.WrapSpellChecked = this.DoAutoSpellCheck();
    this.e.MaxBufferLength = this.e.WrapBufferSize - this.e.LineWidth;
    this.e.BufferLength = 0;
    this.e.LastBufferedLine = StartLine;
    this.e.CurWrapPfmt = this.e.CurWrapCell = this.e.CurWrapParaFID = this.e.WrapParaFont = 0;
    if (this.e.text[this.e.LastBufferedLine].len + 2000 >= this.e.MaxBufferLength)
      this.AllocWrapBuf(this.e.text[this.e.LastBufferedLine].len + 2000);
    while (this.e.LastBufferedLine < this.e.TotalLines && this.e.BufferLength + this.e.text[this.e.LastBufferedLine].len <= this.e.MaxBufferLength)
    {
      if (this.e.text[this.e.LastBufferedLine].len > 0)
      {
        char[] txt = this.e.text[this.e.LastBufferedLine].txt;
        if (this.e.BufferLength <= 0 || !this.IsHdrFtrChar(txt[0]))
        {
          ushort[] src = this.OpenCfmt(this.e.LastBufferedLine);
          int len = this.e.text[this.e.LastBufferedLine].len;
          if ((int) txt[len - 1] == (int) this.e.ParaChar)
          {
            int index = (int) src[len - 1];
            bool flag = false;
            if (this.e.TerFont[index].FieldId == 7 && this.e.TerFont[index].CharWidth[(int) this.e.ParaChar] == 0)
              flag = true;
            if (!this.e.EditFootnoteText && !this.e.EditEndnoteText && (this.e.TerFont[index].style & 2048 /*0x0800*/) != 0 && this.e.TerFont[index].CharWidth[(int) this.e.ParaChar] == 0)
              flag = true;
            if (flag)
            {
              txt[len - 1] = '\u0005';
              this.SetTag(this.e.LastBufferedLine, len - 1, 6, "HPARA2", "", this.e.text[this.e.LastBufferedLine].pfmt);
              txt = this.e.text[this.e.LastBufferedLine].txt;
              src = this.OpenCfmt(this.e.LastBufferedLine);
              len = this.e.text[this.e.LastBufferedLine].len;
            }
          }
          if ((this.e.text[this.e.LastBufferedLine].flags2 & 512 /*0x0200*/) != 0 && len > 0 && txt[len - 1] == '\u0006')
            --len;
          if (!this.e.WrapHasUniChar && len > 0)
          {
            int index = 0;
            while (index < len && txt[index] <= '\u007F')
              ++index;
            if (index < len)
              this.e.WrapHasUniChar = true;
          }
          if (this.e.BufferLength + len >= this.e.wrap.Length)
          {
            int count = this.e.BufferLength + len + 1000;
            this.e.wrap = this.ReAlloc(this.e.wrap, count);
            this.e.WrapCfmt = this.ReAlloc(this.e.WrapCfmt, count);
            this.e.WrapCtid = this.ReAlloc(this.e.WrapCtid, count);
          }
          this.FarMove(txt, 0, this.e.wrap, this.e.BufferLength, len);
          this.FarMove(src, 0, this.e.WrapCfmt, this.e.BufferLength, len);
          if ((this.e.text[this.e.LastBufferedLine].flags2 & 1) == 0)
            this.e.WrapSpellChecked = false;
          if (this.e.text[this.e.LastBufferedLine].tag != null || this.e.TagsWrapped)
          {
            if (!this.e.TagsWrapped && this.e.BufferLength > 0)
              this.FarMemSet(this.e.WrapCtid, (ushort) 0, 0, this.e.BufferLength);
            this.e.TagsWrapped = true;
            if (this.e.text[this.e.LastBufferedLine].tag != null)
              this.FarMove(this.e.text[this.e.LastBufferedLine].tag, 0, this.e.WrapCtid, this.e.BufferLength, len);
            else
              this.FarMemSet(this.e.WrapCtid, (ushort) 0, this.e.BufferLength, len);
          }
          this.e.BufferLength += len;
          if (len > 1 || txt[0] != '\u0014')
            this.e.CurWrapPfmt = this.e.text[this.e.LastBufferedLine].pfmt;
          this.e.CurWrapCell = this.e.text[this.e.LastBufferedLine].cid;
          this.e.CurWrapParaFID = this.e.text[this.e.LastBufferedLine].fid;
        }
        else
          break;
      }
      ++this.e.LastBufferedLine;
      if (this.e.BufferLength > 0 && ((int) this.e.wrap[this.e.BufferLength - 1] == (int) this.e.ParaChar || (int) this.e.wrap[this.e.BufferLength - 1] == (int) this.e.CellChar || this.e.wrap[this.e.BufferLength - 1] == '\f'))
        this.e.WrapParaFont = (int) this.e.WrapCfmt[this.e.BufferLength - 1];
      if (this.e.BufferLength > 1 && this.e.wrap[this.e.BufferLength - 1] == '\u0014' && (int) this.e.wrap[this.e.BufferLength - 2] == (int) this.e.ParaChar)
        this.e.WrapParaFont = (int) this.e.WrapCfmt[this.e.BufferLength - 2];
      if (this.e.BufferLength <= 0 || (int) this.e.wrap[this.e.BufferLength - 1] != (int) this.e.ParaChar && (int) this.e.wrap[this.e.BufferLength - 1] != (int) this.e.CellChar || this.e.LastBufferedLine < this.e.TotalLines && this.e.text[this.e.LastBufferedLine].len > 0 && WrapLines - 1 > 0 && this.e.text[this.e.LastBufferedLine].txt[0] == '\u0014' && this.e.CurWrapParaFID <= 0 && this.e.CurWrapCell <= 0)
      {
        if (this.e.BufferLength > 0)
        {
          char x = this.e.wrap[this.e.BufferLength - 1];
          switch (x)
          {
            case '\f':
            case '\u0012':
            case '\u0016':
              goto label_55;
            default:
              if (this.IsHdrFtrChar(x))
                goto label_55;
              break;
          }
        }
        if (this.e.BufferLength > 0 && this.e.wrap[this.e.BufferLength - 1] == '\u0014')
        {
          if (this.e.text[this.e.LastBufferedLine - 1].tabw != null && (this.e.text[this.e.LastBufferedLine - 1].tabw.type & 2) != 0)
          {
            this.e.WrapSect = this.e.text[this.e.LastBufferedLine - 1].tabw.section;
            break;
          }
          break;
        }
        --WrapLines;
        if (WrapLines <= 0)
        {
          for (int lastBufferedLine = this.e.LastBufferedLine; lastBufferedLine < this.e.TotalLines; ++lastBufferedLine)
          {
            char[] txt = this.e.text[lastBufferedLine].txt;
            int len = this.e.text[lastBufferedLine].len;
            if ((this.e.text[lastBufferedLine].flags & 2048 /*0x0800*/) == 0)
            {
              int col;
              for (col = 0; col < len; ++col)
              {
                char x = txt[col];
                if ((int) x == (int) this.e.ParaChar || (int) x == (int) this.e.CellChar || x == '\u0012' || x == '\f' || x == '\u0016' || this.IsHdrFtrChar(x))
                {
                  this.e.CurWrapPfmt = this.e.text[lastBufferedLine].pfmt;
                  this.e.WrapParaFont = this.GetCurCfmt(lastBufferedLine, col);
                  break;
                }
              }
              if (col < len)
                break;
            }
            else
              break;
          }
          break;
        }
      }
      else
        break;
    }
label_55:
    if (this.e.LastBufferedLine == this.e.TotalLines)
    {
      if (this.e.BufferLength == 0 || (int) this.e.wrap[this.e.BufferLength - 1] != (int) this.e.ParaChar)
      {
        if (this.e.BufferLength > 0 && this.e.wrap[this.e.BufferLength - 1] == '\u000F')
        {
          this.e.wrap[this.e.BufferLength - 1] = this.e.ParaChar;
        }
        else
        {
          this.e.wrap[this.e.BufferLength] = this.e.ParaChar;
          if (this.e.TagsWrapped)
            this.e.WrapCtid[this.e.BufferLength] = (ushort) 0;
          int index = this.e.BufferLength > 0 ? (int) this.e.WrapCfmt[this.e.BufferLength - 1] : 0;
          if (this.e.BufferLength == 0 || (this.e.TerFont[index].style & 128 /*0x80*/) != 0 || this.e.TerFont[index].FieldId > 0)
            this.e.WrapCfmt[this.e.BufferLength] = (ushort) 0;
          else
            this.e.WrapCfmt[this.e.BufferLength] = this.e.WrapCfmt[this.e.BufferLength - 1];
          ++this.e.BufferLength;
        }
      }
      if ((this.e.TerFlags4 & 4096 /*0x1000*/) != 0 && this.e.BufferLength > 1 && (int) this.e.wrap[this.e.BufferLength - 1] == (int) this.e.ParaChar)
      {
        int index = (int) this.e.WrapCfmt[this.e.BufferLength - 2];
        if ((this.e.TerFont[index].style & 128 /*0x80*/) != 0 || this.e.TerFont[index].FieldId > 0)
          index = (int) this.e.WrapCfmt[this.e.BufferLength - 1];
        this.e.WrapCfmt[this.e.BufferLength - 1] = (ushort) index;
      }
    }
    --this.e.LastBufferedLine;
    this.GetWrapCharWidth();
    for (int index = StartLine; index <= this.e.LastBufferedLine; ++index)
      this.e.text[index].pfmt = this.e.CurWrapPfmt;
    return true;
  }

  /// <summary>Разбор буфера при переносе</summary>
  /// <param name="StartLine"></param>
  /// <returns></returns>
  internal new bool WrapParseBuffer(int StartLine)
  {
    int num1 = 0;
    int num2 = 0;
    int num3 = 0;
    int num4 = 0;
    int num5 = 0;
    char ch1 = ' ';
    tc.StrTab strTab = new tc.StrTab();
    tc.ClsTabw clsTabw1 = new tc.ClsTabw();
    char ch2 = '.';
    ushort index1 = 0;
    ushort CurFont = 0;
    int num6 = 0;
    bool flag1 = (this.e.TerFlags3 & 1) != 0;
    string ListText = (string) null;
    int pFontId = 0;
    int pListNbr = 0;
    bool flag2 = (this.e.TerFlags4 & 4) != 0;
    int num7 = 0;
    int num8 = -1;
    ushort[] WordWidth = new ushort[1000];
    if ((this.e.TerFlags & 8388608 /*0x800000*/) != 0)
      ch2 = ',';
    int TextAngle = this.e.AllTextAngle;
    if (TextAngle == 0)
      TextAngle = this.e.CurWrapParaFID > 0 ? this.e.ParaFrame[this.e.CurWrapParaFID].TextAngle : 0;
    if (TextAngle == 0 && this.e.CurWrapCell > 0 && this.e.cell[this.e.CurWrapCell].TextAngle != 0)
      TextAngle = this.e.cell[this.e.CurWrapCell].TextAngle;
    int row = this.e.cell[this.e.CurWrapCell].row;
    bool flag3;
    bool flag4 = flag3 = this.IsParaRtl(this.e.PfmtId[this.e.CurWrapPfmt].flow, this.e.TableRow[row].flow, this.e.TerSect[this.e.WrapSect].flow, this.e.DocTextFlow);
    if (!flag4 && this.e.WrapTextFlow == 2)
    {
      if (this.e.PfmtId[this.e.CurWrapPfmt].flow == 0 && this.e.WrapCharWidth != null && ((int) this.e.WrapCharWidth[0] & 32768 /*0x8000*/) != 0)
        this.e.CurWrapPfmt = this.par.SetParaTextFlow(this.e.CurWrapPfmt, 2);
      flag4 = true;
      this.e.PaintFlag = 4;
    }
    bool flag5 = this.e.TerArg.PrintView && !this.e.TerArg.FittedView;
    if (!this.e.TerArg.PrintView && this.e.WrapWidthChars > 0)
      num6 = this.e.WrapWidthChars;
    int LineEnd = 0;
    this.e.LastWrappedLine = StartLine - 1;
    int num9 = this.TerWrapWidth2(this.e.LastBufferedLine, -1, !flag5);
    int ParaId = this.e.CurWrapPfmt;
    if (ParaId >= this.e.TotalPfmts)
      ParaId = this.e.TotalPfmts - 1;
    if (ParaId < 0)
      ParaId = 0;
    bool flag6 = (this.e.PfmtId[ParaId].pflags & 16 /*0x10*/) != 0;
    if ((this.e.TerFlags & 2048 /*0x0800*/) != 0 && !this.e.TerArg.PageMode)
      flag6 = true;
    while (LineEnd < this.e.BufferLength)
    {
      if (num8 >= 0)
        ParaId = num8;
      int index2 = this.e.LastWrappedLine;
      if (index2 < 0)
        index2 = 0;
      int pListTextWidth = 0;
      if (this.par.IsListLine(this.e.LastWrappedLine + 1))
        this.par.GetListText(ParaId, this.e.LastWrappedLine + 1, out ListText, out pListTextWidth, out pListNbr, out pFontId, this.e.WrapParaFont, -1, flag5);
      int num10 = this.e.PfmtId[ParaId].RightIndent;
      if (num10 < 0 && (!this.e.TerArg.PageMode || this.e.TerArg.FittedView))
        num10 = 0;
      int num11 = this.e.PfmtId[ParaId].RightIndentTwips;
      if (num11 < 0 && (!this.e.TerArg.PageMode || this.e.TerArg.FittedView))
        num11 = 0;
      int leftIndentTwips = this.e.PfmtId[ParaId].LeftIndentTwips;
      int num12 = this.e.PfmtId[ParaId].FirstIndentTwips;
      if (pListTextWidth > 0)
        num12 += flag5 ? pListTextWidth : this.MulDiv(pListTextWidth, 1440, this.e.ScrResX);
      else if ((this.e.PfmtId[ParaId].flags & 8) != 0 && num12 < 0 && (this.e.TerBlt[this.e.PfmtId[ParaId].BltId].flags & 1) == 0)
        num12 = 0;
      if (this.e.LastWrappedLine == -1)
        leftIndentTwips += num12;
      else if ((this.e.text[index2].flags & 3) != 0)
        leftIndentTwips += num12;
      int num13 = this.MulDiv(leftIndentTwips, this.e.ScrResX, 1440);
      if (flag5)
      {
        num13 = leftIndentTwips;
        num10 = num11;
      }
      tc.StrTab tab = this.e.TerTab[this.e.PfmtId[ParaId].TabId];
      tc.ClsTabw clsTabw2 = new tc.ClsTabw();
      clsTabw2.type = 1;
      clsTabw2.FrameCharPos = -1;
      bool flag7 = false;
      int tabId = this.e.PfmtId[ParaId].TabId;
      if (tabId != 0 && this.e.TerArg.PageMode && (!this.e.TerArg.FittedView || this.e.CurWrapCell > 0) && this.e.CurWrapParaFID == 0)
      {
        for (int index3 = 0; index3 < tab.count; ++index3)
        {
          if (tab.pos[index3] >= num9 - num10 + 60)
          {
            flag7 = true;
            break;
          }
        }
      }
      int LineBegin = LineEnd;
      int idx = 0;
      int num14 = 0;
      int num15 = 0;
      int num16 = 0;
      char ch3 = char.MinValue;
      int num17 = 0;
      int lflags2 = 0;
      bool flag8 = false;
      int num18 = -1;
      int num19 = -1;
      int num20 = LineBegin;
      bool flag9 = false;
      bool flag10 = false;
      char minValue;
      char ch4 = minValue = char.MinValue;
      bool flag11 = false;
      int num21 = 0;
      int num22 = 0;
      bool flag12;
      bool flag13 = flag12 = false;
      bool flag14;
      bool flag15 = flag14 = false;
      bool flag16 = false;
      bool flag17 = false;
      if (this.e.LastWrappedLine + 1 < this.e.TotalLines)
        num21 = this.e.text[this.e.LastWrappedLine + 1].JustAdjX;
      int frmSpcBef = this.frm.GetFrmSpcBef(this.e.LastWrappedLine + 1, false);
      int num23;
      int CurPos = num23 = num13;
      if (this.e.LastWrappedLine + 1 < this.e.TotalLines)
      {
        if (flag4)
          this.e.text[this.e.LastWrappedLine + 1].flags2 |= 32 /*0x20*/;
        else
          this.e.text[this.e.LastWrappedLine + 1].flags2 &= -33;
      }
      int FrameX;
      int FrameWidth;
      this.frm.GetFrameSpace(this.e.LastWrappedLine + 1, tc.SkipRect, out FrameX, out FrameWidth, out int _);
      int num24;
      int num25;
      if (flag5)
      {
        num24 = FrameX;
        num25 = FrameWidth;
        clsTabw2.FrameX = num24;
        clsTabw2.FrameWidth = num25;
        clsTabw2.FrameScrWidth = this.MulDiv(num24 + num25, this.e.ScrResX, 1440);
      }
      else
      {
        num24 = this.MulDiv(FrameX, this.e.ScrResX, 1440);
        num25 = this.MulDiv(FrameWidth, this.e.ScrResX, 1440);
        clsTabw2.FrameX = FrameX;
        clsTabw2.FrameWidth = FrameWidth;
        clsTabw2.FrameScrWidth = num24 + num25;
      }
      int pTabPos;
      int pTabType;
      if (tabId != 0 && this.e.TerArg.PageMode && this.e.CurWrapCell > 0 && tab.count == 1 && tab.type[0] == 3)
      {
        flag17 = true;
        this.pos.GetTabPos(ParaId, tab, CurPos, out pTabPos, out pTabType, out tc.SkipByte, !flag5);
        int num26 = pTabPos - CurPos;
        clsTabw2.width[clsTabw2.count] = num26;
        ++clsTabw2.count;
        num2 = pTabPos;
        num1 = pTabType;
        num3 = 0;
        num4 = 0;
        flag8 = false;
        if (flag7 && pTabPos >= num9 - num10 + 60)
          flag6 = true;
      }
      bool flag18 = true;
      int num27;
      do
      {
        bool flag19;
        bool flag20;
        do
        {
          num27 = CurPos;
          if ((int) this.e.WrapCfmt[LineEnd] != (int) CurFont | flag18)
          {
            CurFont = this.e.WrapCfmt[LineEnd];
            index1 = this.e.TerFont[(int) CurFont].ParaStyId != this.e.PfmtId[ParaId].StyId ? this.par.ApplyParaStyleOnFont((int) CurFont, this.e.PfmtId[ParaId].StyId) : CurFont;
            if (this.e.TerFont[(int) index1].TextAngle != TextAngle)
              index1 = (ushort) this.fnt.SetFontTextAngle((int) index1, TextAngle);
            flag18 = false;
          }
          int style = this.e.TerFont[(int) index1].style;
          if ((style & 131072 /*0x020000*/) != 0)
          {
            if (char.IsLower(this.e.wrap[LineEnd]) && (this.e.TerFont[(int) index1].flags & 512 /*0x0200*/) == 0)
              index1 = (ushort) this.fnt.SetScapFont((int) index1, true);
            if (!char.IsLower(this.e.wrap[LineEnd]) && (this.e.TerFont[(int) index1].flags & 512 /*0x0200*/) != 0)
              index1 = (ushort) this.fnt.SetScapFont((int) index1, false);
            style = this.e.TerFont[(int) index1].style;
          }
          this.e.WrapCfmt[LineEnd] = index1;
          if (this.e.TerFont[(int) index1].InsRev != 0)
            lflags2 |= 128 /*0x80*/;
          if (this.e.TerFont[(int) index1].DelRev != 0)
            lflags2 |= 64 /*0x40*/;
          int num28 = !this.e.CharWidthWrapped || ((int) this.e.WrapCharWidth[LineEnd] & 16384 /*0x4000*/) == 0 ? -1 : (int) this.e.WrapCharWidth[LineEnd] & 16383 /*0x3FFF*/;
          if (num28 != -1)
          {
            if ((this.e.TerFont[(int) index1].flags & 128 /*0x80*/) == 0)
            {
              if (!this.e.EditFootnoteText && (this.e.TerFont[(int) index1].style & 2048 /*0x0800*/) != 0)
                num28 = 0;
              if (!this.e.EditEndnoteText && (this.e.TerFont[(int) index1].style & 32768 /*0x8000*/) != 0)
                num28 = 0;
            }
            if (this.e.TerFont[(int) index1].CharWidth[65] == 0)
              num28 = 0;
          }
          bool flag21 = flag13;
          flag13 = false;
          char ch5 = ch4;
          char upper;
          ch4 = upper = this.e.wrap[LineEnd];
          if ((style & 196608 /*0x030000*/) != 0 && char.IsLower(upper))
            upper = char.ToUpper(upper);
          if (ch4 == '\u0005')
          {
            bool flag22 = flag2 && this.e.ShowHiddenText && (this.e.TerFont[(int) index1].style & 64 /*0x40*/) != 0;
            bool flag23 = this.e.TerFont[(int) index1].FieldId == 7 && this.e.TerFont[(int) index1].CharWidth[(int) this.e.ParaChar] != 0;
            bool flag24 = (this.e.TerFont[(int) index1].style & 2048 /*0x0800*/) != 0 && this.e.TerFont[(int) index1].CharWidth[(int) this.e.ParaChar] != 0;
            int num29 = -1;
            if (this.e.TagsWrapped && this.e.WrapCtid[LineEnd] > (ushort) 0)
            {
              int line = (int) this.e.WrapCtid[LineEnd];
              int AuxInt;
              if (flag22 && this.GetTag(line, -1, 5, out tc.SkipStr, out tc.SkipStr, out AuxInt) > 0)
                num29 = AuxInt;
              if (flag23 && this.GetTag(line, -1, 6, out tc.SkipStr, out tc.SkipStr, out AuxInt) > 0)
                num29 = AuxInt;
              if (flag24 && this.GetTag(line, -1, 6, out tc.SkipStr, out tc.SkipStr, out AuxInt) > 0)
                num29 = AuxInt;
              if (num29 >= 0)
              {
                this.e.wrap[LineEnd] = ch4 = this.e.ParaChar;
                num8 = ParaId;
                ParaId = num29;
              }
            }
          }
          if (ch5 != ' ' && !flag21 && ch4 == ' ')
            num22 = 0;
          bool flag25 = LineEnd + 1 < this.e.BufferLength && this.e.wrap[LineEnd + 1] == '\u0014';
          int fieldId = this.e.TerFont[(int) index1].FieldId;
          if (num18 < 0 && fieldId > 0 && this.IsDynField(fieldId))
            num18 = LineEnd;
          switch (fieldId)
          {
            case 9:
              flag15 = true;
              break;
            case 11:
              flag14 = true;
              break;
            case 12:
              flag16 = true;
              break;
            case 13:
              lflags2 |= 4;
              break;
          }
          if ((this.e.TerFont[(int) index1].style & 128 /*0x80*/) != 0 && (this.e.TerFont[(int) index1].PictType == 2 || this.e.TerFont[(int) index1].PictType == 6 || this.e.TerFont[(int) index1].ObjectType == 5))
            num19 = LineEnd;
          bool flag26 = (this.e.TerFont[(int) index1].style & 128 /*0x80*/) != 0 && this.e.TerFont[(int) index1].ParaFID > 0;
          if (flag26)
            num17 |= 16384 /*0x4000*/;
          if ((this.e.TerFont[(int) index1].style & 7168) != 0)
            num17 |= 16 /*0x10*/;
          if ((this.e.TerFont[(int) index1].style & 32768 /*0x8000*/) != 0)
            lflags2 |= 2;
          else if ((this.e.TerFont[(int) index1].style & 2048 /*0x0800*/) != 0)
            num17 |= 65536 /*0x010000*/;
          if ((int) ch4 == (int) this.e.ParaChar && LineEnd + 1 < this.e.BufferLength && !flag25)
          {
            ++LineEnd;
            goto label_205;
          }
          if ((int) ch4 == (int) this.e.CellChar || ch4 == '\u000F')
          {
            flag11 = flag25;
            if (flag11)
              ++LineEnd;
            ++LineEnd;
            ch3 = ch4;
            goto label_205;
          }
          if (ch4 == '\u0014')
          {
            ++LineEnd;
            ch3 = ch4;
            goto label_205;
          }
          if (ch4 <= '\u001C' && (ch4 == '\u0012' || this.IsHdrFtrChar(ch4) || ch4 == '\f' || ch4 == '\u0016'))
          {
            if (LineBegin == LineEnd)
            {
              ++LineEnd;
              ch3 = ch4;
              goto label_205;
            }
            goto label_205;
          }
          if (ch4 == '\t' && !this.edit.HiddenText((int) index1))
          {
            if (clsTabw2.count > 0 && num1 != 0)
            {
              int num30 = num2 - CurPos;
              switch (num1)
              {
                case 2:
                  num30 += num3 / 2;
                  break;
                case 3:
                  num30 += num3;
                  break;
              }
              if (num30 < 0)
                num30 = 0;
              CurPos += num30;
              clsTabw2.width[clsTabw2.count - 1] = num30;
            }
            this.pos.GetTabPos(ParaId, tab, CurPos, out pTabPos, out pTabType, out tc.SkipByte, !flag5);
            int num31 = pTabPos - CurPos;
            clsTabw2.width[clsTabw2.count] = num31;
            if (clsTabw2.count < 20)
              ++clsTabw2.count;
            if (pTabType == 0)
              CurPos = pTabPos;
            num2 = pTabPos;
            num1 = pTabType;
            num3 = 0;
            num4 = 0;
            flag8 = false;
            if (flag7 && pTabPos >= num9 - num10 + this.MulDiv(60, this.e.UnitResX, 1440))
              flag6 = true;
          }
          else
          {
            if ((int) ch4 != (int) this.e.ParaChar && !flag26)
            {
              int num32 = num28;
              if (num32 == -1)
                num32 = this.fnt.LwrCharWidth((int) index1, !flag5, upper);
              if ((this.e.TerFont[(int) index1].style & 128 /*0x80*/) != 0 && num5 < this.e.TerFont[(int) index1].PictHeight)
                num5 = this.e.TerFont[(int) index1].PictHeight;
              if (upper == '\u0006')
                num32 = 0;
              CurPos += num32;
              if (ch4 == ' ')
                num22 += num32;
              if (num32 == 0)
                flag13 = true;
            }
            if (!flag10 && (this.e.TerFont[(int) index1].style & 8192 /*0x2000*/) != 0 || flag10 && (this.e.TerFont[(int) index1].style & 8192 /*0x2000*/) == 0)
            {
              flag10 = !flag10;
              if (flag10)
                flag9 = true;
              if (flag5)
                CurPos += this.e.ExtraSpacePrtX;
              else
                CurPos += this.e.ExtraSpaceScrX;
            }
            if (num23 + num21 < num24 + num25 && CurPos + num21 >= num24 && num25 > 0 && this.e.CurWrapParaFID == 0)
            {
              int num33 = CurPos - num23;
              int x = 0;
              clsTabw2.FrameCharPos = num20 - LineBegin;
              if (num23 + num21 > num24)
                x = num23 + num21 - num24;
              if (flag5)
              {
                clsTabw2.FrameX = num24 + x;
                clsTabw2.FrameWidth = num25 - x;
                clsTabw2.FrameScrWidth = this.MulDiv(num24 + num25 - num23, this.e.ScrResX, this.e.UnitResX);
                clsTabw2.FrameSpaceWidth = num24 + num25 - num23 - x;
              }
              else
              {
                int num34 = this.MulDiv(x, this.e.UnitResX, this.e.ScrResX);
                clsTabw2.FrameX = FrameX + num34;
                clsTabw2.FrameWidth = FrameWidth - num34;
                clsTabw2.FrameScrWidth = num24 + num25 - num23;
                clsTabw2.FrameSpaceWidth = num24 + num25 - num23 - x;
              }
              num23 = num24 + num25;
              CurPos = num23 + num33;
              if (CurPos > num9 - num10)
              {
                LineEnd = num20;
                CurPos = num23;
                continue;
              }
            }
            if ((int) ch4 == (int) ch2 && !flag13)
              flag8 = true;
            if ((num1 == 2 || num1 == 3 & flag8) && (int) ch4 != (int) this.e.ParaChar)
            {
              int num35 = num28;
              if (num35 == -1)
                num35 = this.fnt.LwrCharWidth((int) index1, !flag5, upper);
              num3 += num35;
              num4 += this.fnt.LwrCharWidth((int) index1, true, ch4);
            }
          }
          switch (ch4)
          {
            case '\u0005':
              num17 |= 67108864 /*0x04000000*/;
              break;
            case '\u0006':
              num17 |= 2097152 /*0x200000*/;
              break;
            case '\u000E':
              num17 |= 64 /*0x40*/;
              break;
            case '\u0017':
              num17 |= 8192 /*0x2000*/;
              break;
            case '\u001C':
              lflags2 |= 8;
              break;
          }
          bool flag27 = flag1;
          if (flag4 && !this.e.TerFont[(int) index1].rtl)
            flag27 = true;
          if (this.e.CurWrapParaFID > 0 & flag5 && LineEnd + 2 == this.e.BufferLength && (int) this.e.wrap[LineEnd + 1] == (int) this.e.ParaChar)
            num7 = -this.e.UnitResX / 30;
          if (LineEnd >= idx && LineEnd - idx < 1000)
            WordWidth[LineEnd - idx] = (ushort) (CurPos - num27);
          if (flag6 | flag13 || CurPos <= num9 - num10 + num7 && num6 == 0 || this.e.TerArg.PrintView && ch4 == ' ' && !flag27 || this.e.TerArg.PrintView && (int) ch4 == (int) this.e.ParaChar && ((ch5 == ' ' ? 1 : (ch5 == '\t' ? 1 : 0)) | (flag21 ? 1 : 0)) != 0 || num6 != 0 && LineEnd - LineBegin < num6)
          {
            ++LineEnd;
            if ((int) ch4 == (int) this.e.ParaChar)
            {
              flag11 = flag25;
              if (flag11)
              {
                ++LineEnd;
                goto label_205;
              }
              goto label_205;
            }
            if (LineEnd < this.e.BufferLength)
            {
              flag19 = ch4 == '-' && this.e.TerFont[(int) index1].CharSet != (byte) 2;
              flag20 = false;
              if (ch4 > '⸀')
              {
                ushort num36 = (ushort) ch4;
                flag20 = num36 >= (ushort) 11776 && num36 <= (ushort) 40879 || num36 >= (ushort) 63744 && num36 < (ushort) 64255 || num36 >= (ushort) 65072 && num36 < (ushort) 65103 || num36 >= (ushort) 65280 && num36 < (ushort) 65519;
                if (!flag20)
                  flag20 = num36 >= (ushort) 19968 && num36 <= (ushort) 43007;
              }
            }
            else
              goto label_205;
          }
          else
            goto label_194;
        }
        while (!(flag20 | flag19) && ch4 != ' ' && ch4 != '\t' && ch4 != '\u0006' && ch4 != '\u001C' || !this.e.ShowHiddenText && (this.e.TerFont[(int) index1].style & 64 /*0x40*/) != 0 || !this.e.ShowFieldNames && this.e.TerFont[(int) index1].FieldId == 6 || this.e.ShowFieldNames && this.e.TerFont[(int) index1].FieldId == 7);
        idx = LineEnd;
        num16 = CurPos;
        if (ch4 == '\t' && (this.e.PfmtId[ParaId].flags & 2048 /*0x0800*/) != 0 && !this.edit.HiddenText((int) index1))
          num15 = num14;
        if (ch4 == ' ' && (this.e.PfmtId[ParaId].flags & 2048 /*0x0800*/) != 0 && !this.edit.HiddenText((int) index1))
          ++num14;
        ch1 = ch4;
        num23 = CurPos;
        num20 = LineEnd;
        continue;
label_194:
        if (idx == 0 && LineEnd == LineBegin || idx == 0 && (int) ch4 == (int) this.e.ParaChar && LineEnd > LineBegin && (int) this.e.wrap[LineEnd - 1] != (int) this.e.ParaChar)
          ++LineEnd;
        else
          goto label_196;
      }
      while (LineEnd < this.e.BufferLength);
      goto label_205;
label_196:
      if (idx != 0)
      {
        if (this.e.DoHyph)
        {
          int PrefixWidth;
          int hyphPrefixLen = this.dsh.GetHyphPrefixLen(this.e.wrap, this.e.WrapCfmt, idx, this.e.BufferLength - idx, LineEnd - idx, WordWidth, num9 - num10 + num7 - num27, out PrefixWidth, flag5);
          if (hyphPrefixLen > 0)
          {
            idx += hyphPrefixLen;
            num16 += PrefixWidth;
            ch1 = this.e.wrap[idx - 1];
            num22 = 0;
            lflags2 |= 512 /*0x0200*/;
            num17 |= 2097152 /*0x200000*/;
          }
        }
        LineEnd = idx;
        CurPos = num16;
        if (ch1 == ' ')
          --num14;
        ch4 = ch1;
      }
      if (LineEnd == LineBegin)
        LineEnd = LineBegin + 1;
label_205:
      if (clsTabw2.count > 0 && num1 != 0)
      {
        int num37 = num2 - CurPos;
        if (num3 < 0)
          num3 = 0;
        switch (num1)
        {
          case 2:
            num37 += num3 / 2;
            break;
          case 3:
            num37 += num3;
            break;
        }
        if (num37 < 0)
          num37 = 0;
        CurPos += num37;
        clsTabw2.width[clsTabw2.count - 1] = num37;
      }
      if ((int) ch4 == (int) this.e.ParaChar || ch3 != char.MinValue)
      {
        CurPos += this.fnt.LwrCharWidth((int) index1, !flag5, ch4);
        if (flag11)
          CurPos += this.fnt.LwrCharWidth((int) index1, !flag5, '\u0014');
      }
      if (ch4 == '\u0006' && (!this.e.ShowParaMark || this.e.InPrinting))
        CurPos += this.fnt.LwrCharWidth((int) index1, !flag5, '-');
      ++this.e.LastWrappedLine;
      if (this.e.LastWrappedLine > this.e.LastBufferedLine && !this.DisplacePointers(this.e.LastBufferedLine + 1, this.e.WrapAddLines))
      {
        --this.e.LastWrappedLine;
        return true;
      }
      this.CopyWrapLineData(LineBegin, LineEnd, ch4, lflags2);
      this.e.text[this.e.LastWrappedLine].pfmt = ParaId;
      this.e.text[this.e.LastWrappedLine].cid = this.e.CurWrapCell;
      this.e.text[this.e.LastWrappedLine].fid = this.e.CurWrapParaFID;
      if (this.e.LastWrappedLine == this.e.TotalLines - 1 && LineEnd == this.e.BufferLength)
        this.e.text[this.e.LastWrappedLine].cid = 0;
      if (this.e.CurWrapCell > 0 && (int) ch3 == (int) this.e.CellChar)
      {
        bool flag28 = this.edit.HiddenText((int) index1);
        for (int lastWrappedLine = this.e.LastWrappedLine; lastWrappedLine >= 0 && this.e.text[lastWrappedLine].cid != 0 && (this.e.cell[this.e.text[lastWrappedLine].cid].level != this.e.cell[this.e.CurWrapCell].level || this.e.text[lastWrappedLine].cid == this.e.CurWrapCell); --lastWrappedLine)
        {
          if (this.e.text[lastWrappedLine].height > 0)
          {
            flag28 = false;
            break;
          }
        }
        if (flag28)
          this.e.CellAux[this.e.CurWrapCell].flags |= 16 /*0x10*/;
        else
          this.e.CellAux[this.e.CurWrapCell].flags &= -17;
      }
      if (this.e.LastWrappedLine - 1 < 0)
        ;
      this.e.text[this.e.LastWrappedLine].flags = num17 | this.e.text[this.e.LastWrappedLine].flags & 4128;
      this.e.text[this.e.LastWrappedLine].flags2 = lflags2;
      if ((int) ch4 == (int) this.e.ParaChar || (int) ch4 == (int) this.e.CellChar)
        this.e.text[this.e.LastWrappedLine].flags |= 1;
      switch (ch3)
      {
        case char.MinValue:
          if (flag11 || ch3 == '\u0014')
            this.e.text[this.e.LastWrappedLine].flags |= 2048 /*0x0800*/;
          if (flag15)
            this.e.text[this.e.LastWrappedLine].flags |= 16777216 /*0x01000000*/;
          if (flag14)
            this.e.text[this.e.LastWrappedLine].flags |= 134217728 /*0x08000000*/;
          if (flag16)
            this.e.text[this.e.LastWrappedLine].flags |= 1073741824 /*0x40000000*/;
          if (this.IsFirstParaLine(this.e.LastWrappedLine))
            this.e.text[this.e.LastWrappedLine].flags |= 4;
          if (pListTextWidth > 0)
            this.e.text[this.e.LastWrappedLine].flags |= 33554432 /*0x02000000*/;
          if (num19 >= 0 && num19 < LineEnd)
            this.e.text[this.e.LastWrappedLine].flags |= 8;
          if (this.e.HtmlMode && this.e.PfmtId[ParaId].AuxId > 0 && this.IsHtmlRule(ParaId))
            this.e.text[this.e.LastWrappedLine].flags |= 256 /*0x0100*/;
          if (flag17)
            this.e.text[this.e.LastWrappedLine].flags |= 536870912 /*0x20000000*/;
          if ((this.e.PfmtId[ParaId].flags & 16 /*0x10*/) != 0 && (this.e.text[this.e.LastWrappedLine].flags & 4) != 0 && this.e.text[this.e.LastWrappedLine].fid == 0 && (this.e.LastWrappedLine == 0 || !this.HasSameParaBorder(this.e.LastWrappedLine - 1, this.e.LastWrappedLine)))
            this.e.text[this.e.LastWrappedLine].flags |= 512 /*0x0200*/;
          if ((this.e.PfmtId[ParaId].flags & 32 /*0x20*/) != 0 && (this.e.text[this.e.LastWrappedLine].flags & 3) != 0 && this.e.text[this.e.LastWrappedLine].fid == 0 && (this.e.LastWrappedLine >= this.e.TotalLines - 1 || !this.HasSameParaBorder(this.e.LastWrappedLine + 1, this.e.LastWrappedLine)))
            this.e.text[this.e.LastWrappedLine].flags |= 1024 /*0x0400*/;
          if ((this.e.PfmtId[ParaId].flags & 65536 /*0x010000*/) != 0 && (this.e.text[this.e.LastWrappedLine].flags & 3) != 0 && this.e.text[this.e.LastWrappedLine].fid == 0 && this.e.LastWrappedLine < this.e.TotalLines - 1 && this.HasSameParaBorder(this.e.LastWrappedLine + 1, this.e.LastWrappedLine) && (this.e.text[this.e.LastWrappedLine].flags & 1024 /*0x0400*/) == 0)
            this.e.text[this.e.LastWrappedLine].flags2 |= 16 /*0x10*/;
          if (this.e.PfmtId[ParaId].shading > 0 && (this.e.text[this.e.LastWrappedLine].flags & 4) != 0 && (this.e.LastWrappedLine == 0 || !this.HasSameParaShading(this.e.LastWrappedLine - 1, this.e.LastWrappedLine)))
            this.e.text[this.e.LastWrappedLine].flags |= 4194304 /*0x400000*/;
          if (this.e.PfmtId[ParaId].shading > 0 && (this.e.text[this.e.LastWrappedLine].flags & 3) != 0 && (this.e.LastWrappedLine >= this.e.TotalLines - 1 || !this.HasSameParaShading(this.e.LastWrappedLine + 1, this.e.LastWrappedLine)))
            this.e.text[this.e.LastWrappedLine].flags |= 8388608 /*0x800000*/;
          if (this.e.WrapSpellChecked)
            this.e.text[this.e.LastWrappedLine].flags2 |= 1;
          if (flag4)
            this.e.text[this.e.LastWrappedLine].flags2 |= 32 /*0x20*/;
          if (flag3)
            this.e.text[this.e.LastWrappedLine].flags2 |= 256 /*0x0100*/;
          int num38 = num9 - num10 - CurPos;
          if (!flag4)
            num38 += num22;
          if (clsTabw2.count > 0 || num25 > 0 || flag9 || ch3 != char.MinValue || flag11 || (this.e.PfmtId[ParaId].flags & 2048 /*0x0800*/) != 0 || num38 > 0 || pListTextWidth > 0 || num18 >= 0 && num18 < LineEnd || frmSpcBef > 0)
          {
            this.e.text[this.e.LastWrappedLine].tabw = clsTabw2;
            this.e.text[this.e.LastWrappedLine].tabw.type &= -25791;
            if (this.e.text[this.e.LastWrappedLine].tabw.count == 0)
              this.e.text[this.e.LastWrappedLine].tabw.type &= -2;
            this.e.text[this.e.LastWrappedLine].tabw.JustCount = this.e.text[this.e.LastWrappedLine].tabw.JustAdj = 0;
            int type = this.e.text[this.e.LastWrappedLine].tabw.type;
            if (ch3 > char.MinValue | flag11)
            {
              if (ch3 == '\u0014' | flag11)
                this.e.text[this.e.LastWrappedLine].tabw.type = type | 2;
              if (ch3 == '\f')
                this.e.text[this.e.LastWrappedLine].tabw.type = type | 4;
              if (ch3 == '\u0016')
                this.e.text[this.e.LastWrappedLine].tabw.type = type | 8;
              if ((int) ch3 == (int) this.e.CellChar)
                this.e.text[this.e.LastWrappedLine].tabw.type = type | 16 /*0x10*/;
              if (ch3 == '\u0012')
                this.e.text[this.e.LastWrappedLine].tabw.type = type | 32 /*0x20*/;
              if ((int) ch3 != (int) this.e.CellChar && ch3 != '\u000F' && !flag11)
                this.e.text[this.e.LastWrappedLine].tabw.count = 0;
              this.e.text[this.e.LastWrappedLine].tabw.section = this.e.WrapSect;
            }
            if (num25 > 0 && this.e.TerArg.PrintView)
              this.e.text[this.e.LastWrappedLine].tabw.type |= 1024 /*0x0400*/;
            if (num18 >= 0 && num18 < LineEnd)
              this.e.text[this.e.LastWrappedLine].tabw.type |= 16384 /*0x4000*/;
            if (frmSpcBef > 0)
            {
              this.e.text[this.e.LastWrappedLine].tabw.type |= 8192 /*0x2000*/;
              this.e.text[this.e.LastWrappedLine].tabw.height = frmSpcBef;
            }
            if (pListTextWidth > 0)
            {
              this.e.text[this.e.LastWrappedLine].tabw.ListText = ListText;
              this.e.text[this.e.LastWrappedLine].tabw.ListTextWidth = pListTextWidth;
              this.e.text[this.e.LastWrappedLine].tabw.ListFontId = pFontId;
              this.e.text[this.e.LastWrappedLine].tabw.ListNbr = pListNbr;
            }
            if (((this.e.PfmtId[ParaId].flags & 2048 /*0x0800*/) != 0 || num38 < 0) && ((this.e.text[this.e.LastWrappedLine].tabw.type & 1024 /*0x0400*/) == 0 || this.e.text[this.e.LastWrappedLine].tabw.FrameX == 0))
            {
              int num39 = num14 - num15;
              if (num38 > 0 && num39 > 0 && (int) ch4 != (int) this.e.ParaChar && ch3 == char.MinValue)
              {
                this.e.text[this.e.LastWrappedLine].tabw.type |= 128 /*0x80*/;
                this.e.text[this.e.LastWrappedLine].tabw.JustAdj = num38 / num39;
                this.e.text[this.e.LastWrappedLine].tabw.JustCount = num38 - this.e.text[this.e.LastWrappedLine].tabw.JustAdj * num39;
                this.e.text[this.e.LastWrappedLine].tabw.JustSpaceCount = num14;
                this.e.text[this.e.LastWrappedLine].tabw.JustSpaceIgnore = num15;
              }
            }
            if (flag9)
            {
              this.AllocTabwCharFlags(this.e.LastWrappedLine);
              bool flag29 = false;
              ushort[] fmt = this.e.text[this.e.LastWrappedLine].fmt;
              char[] txt = this.e.text[this.e.LastWrappedLine].txt;
              int len = this.e.text[this.e.LastWrappedLine].len;
              if ((this.e.text[this.e.LastWrappedLine].flags & 3) != 0)
                --len;
              int index4 = len - 1;
              while (index4 >= 0 && txt[index4] == ' ')
                --index4;
              int num40 = index4 + 1;
              int index5;
              for (index5 = 0; index5 < num40; ++index5)
              {
                if (!flag29 && (this.e.TerFont[(int) fmt[index5]].style & 8192 /*0x2000*/) != 0)
                {
                  this.e.text[this.e.LastWrappedLine].tabw.CharFlags[index5] |= (byte) 1;
                  flag29 = true;
                }
                else if (flag29 && (this.e.TerFont[(int) fmt[index5]].style & 8192 /*0x2000*/) == 0)
                {
                  this.e.text[this.e.LastWrappedLine].tabw.CharFlags[index5 - 1] |= (byte) 2;
                  flag29 = false;
                }
              }
              if (flag29)
                this.e.text[this.e.LastWrappedLine].tabw.CharFlags[index5 - 1] |= (byte) 2;
            }
          }
          else if (this.e.text[this.e.LastWrappedLine].tabw != null)
            this.misc.FreeTabw(this.e.LastWrappedLine);
          if (flag14 || (this.e.text[this.e.LastWrappedLine].flags & 67108864 /*0x04000000*/) != 0)
            this.par.SetListnum(this.e.LastWrappedLine, flag5);
          if (flag16)
            this.par.SetAutoNumLgl(this.e.LastWrappedLine, flag5);
          if (this.e.text[this.e.LastWrappedLine].tabw != null && (this.e.text[this.e.LastWrappedLine].tabw.type & 44) != 0)
          {
            this.e.text[this.e.LastWrappedLine].flags &= -1537;
            this.e.text[this.e.LastWrappedLine].flags2 &= -17;
          }
          if (this.e.CurWrapParaFID > 0 && !this.e.InPrinting && (this.e.ParaFrame[this.e.CurWrapParaFID].flags & 768 /*0x0300*/) == 0)
          {
            int curWrapParaFid = this.e.CurWrapParaFID;
            int len = this.e.text[this.e.LastWrappedLine].len;
            int curCfmt = this.fnt.GetCurCfmt(this.e.LastWrappedLine, 0);
            int num41 = this.MulDiv(this.e.TerFont[curCfmt].CharWidth[24], 1440, this.e.ScrResX);
            switch (len)
            {
              case 1:
                if ((this.e.TerFont[curCfmt].style & 128 /*0x80*/) != 0 && num41 > this.e.ParaFrame[curWrapParaFid].width - 2 * this.e.ParaFrame[curWrapParaFid].margin)
                {
                  this.e.ParaFrame[curWrapParaFid].width = num41 + 2 * this.e.ParaFrame[curWrapParaFid].margin;
                  break;
                }
                break;
              case 2:
                if ((this.e.text[this.e.LastWrappedLine].flags & 3) == 0)
                  break;
                goto case 1;
            }
          }
          this.fnt.CompressCfmt(this.e.LastWrappedLine);
          if (this.e.TerArg.PrintView)
          {
            int num42 = 9999;
            int SpcBef = 0;
            int SpcAft = 0;
            int index6 = 0;
            int num43 = num42;
            int num44 = num42;
            bool flag30 = false;
            bool flag31 = false;
            bool flag32 = false;
            int num45;
            int num46 = num45 = 0;
            int num47 = num45;
            int num48 = num45;
            if (this.e.text[this.e.LastWrappedLine].len == 1 && (int) ch3 == (int) this.e.CellChar)
            {
              if ((this.e.TerFlags3 & 134217728 /*0x08000000*/) != 0)
                flag32 = true;
              else if (this.e.LastWrappedLine > 0)
              {
                int cid1 = this.e.text[this.e.LastWrappedLine].cid;
                int level = this.e.cell[cid1].level;
                int cid2 = this.e.text[this.e.LastWrappedLine - 1].cid;
                if (cid1 != cid2 && cid1 == this.LevelCell(level, -cid2))
                  flag32 = true;
              }
            }
            if (this.e.text[this.e.LastWrappedLine].len == 1 && ch3 == '\u000F' && this.e.TerFont[this.e.text[this.e.LastWrappedLine].fmt != null ? (int) this.e.text[this.e.LastWrappedLine].fmt[0] : (int) this.e.text[this.e.LastWrappedLine].UniFmt].CharId > 0)
              flag32 = true;
            int num49;
            if (this.e.text[this.e.LastWrappedLine].fmt == null)
            {
              int uniFmt = (int) this.e.text[this.e.LastWrappedLine].UniFmt;
              num48 = this.e.PrtFont[uniFmt].BaseHeight;
              num47 = this.e.PrtFont[uniFmt].height - this.e.PrtFont[uniFmt].BaseHeight - this.e.PrtFont[uniFmt].ExtLead;
              if (this.e.PrtFont[uniFmt].OffsetVal > 0)
                num47 -= this.e.PrtFont[uniFmt].OffsetVal;
              num49 = Math.Abs(this.e.PrtFont[uniFmt].OffsetVal);
              num46 = this.e.PrtFont[uniFmt].ExtLead;
            }
            else
            {
              bool flag33 = false;
              ushort[] fmt = this.e.text[this.e.LastWrappedLine].fmt;
              char[] txt = this.e.text[this.e.LastWrappedLine].txt;
              int num50 = this.e.text[this.e.LastWrappedLine].len;
              if (flag32)
              {
                num50 = 0;
                flag31 = true;
              }
              if (num50 > 1 && (this.e.text[this.e.LastWrappedLine].flags & 129) != 0)
              {
                index6 = (int) fmt[num50 - 1];
                --num50;
                flag31 = true;
                if (num50 > 0 && (this.e.text[this.e.LastWrappedLine].flags & 16384 /*0x4000*/) != 0)
                {
                  int index7 = 0;
                  while (index7 < num50 && (this.e.TerFont[(int) fmt[index7]].style & 128 /*0x80*/) != 0 && this.e.TerFont[(int) fmt[index7]].ParaFID != 0)
                    ++index7;
                  if (index7 == num50)
                    ++num50;
                }
              }
              for (int index8 = 0; index8 < num50; ++index8)
              {
                int index9 = (int) fmt[index8];
                if (!flag30 && (txt[index8] == ' ' || txt[index8] == '\t') && this.e.PrtFont[index9].height > 0 && this.e.TerFont[index9].FieldId == 0)
                  flag30 = true;
                if ((txt[index8] != ' ' && txt[index8] != '\t' || index8 >= num50 - 1 && !flag33 || this.e.TerFont[index9].FieldId != 0) && ((this.e.TerFont[index9].style & 128 /*0x80*/) == 0 || this.e.TerFont[index9].ParaFID <= 0))
                {
                  if (this.e.PrtFont[index9].BaseHeight > num48)
                    num48 = this.e.PrtFont[index9].BaseHeight;
                  int num51 = this.e.PrtFont[index9].height - this.e.PrtFont[index9].BaseHeight - this.e.PrtFont[index9].ExtLead;
                  if (this.e.PrtFont[index9].OffsetVal > 0)
                    num51 -= this.e.PrtFont[index9].OffsetVal;
                  if (num51 > num47)
                    num47 = num51;
                  if (this.e.PrtFont[index9].ExtLead > num46)
                    num46 = this.e.PrtFont[index9].ExtLead;
                  int offsetVal = this.e.PrtFont[index9].OffsetVal;
                  if (offsetVal == 0)
                    num43 = num44 = 0;
                  else if (offsetVal > 0 && offsetVal < num44)
                    num44 = offsetVal;
                  else if (offsetVal < 0 && -offsetVal < num43)
                    num43 = -offsetVal;
                  flag33 = true;
                }
              }
              if (num44 == num42)
                num44 = 0;
              if (num43 == num42)
                num43 = 0;
              if (num44 != 0 && num43 != 0)
                num44 = num43 = 0;
              num49 = num44 + num43;
            }
            if (num47 < 0)
              num47 = 0;
            int TextHeight = num48 + num47 + num46 - num49;
            if (TextHeight == 0 && flag30 | flag31)
              TextHeight = this.e.PrtFont[index6].height + this.e.PrtFont[index6].ExtLead;
            int x;
            if (flag32)
            {
              x = this.MulDiv(20, this.e.UnitResY, 1440);
            }
            else
            {
              this.pos.GetLineSpacing2(this.e.LastWrappedLine, TextHeight, out SpcBef, out SpcAft, out tc.SkipInt, out tc.SkipInt, false);
              x = TextHeight + (SpcBef + SpcAft);
              num48 += SpcBef;
            }
            if (this.e.text[this.e.LastWrappedLine].len == 1 && (this.e.PfmtId[ParaId].flags & 12288 /*0x3000*/) != 0 && !this.e.EditPageHdrFtr && this.e.LastWrappedLine + 1 < this.e.TotalLines && (this.e.text[this.e.LastWrappedLine + 1].flags & 1966080 /*0x1E0000*/) != 0 && this.e.LastWrappedLine - 1 >= 0 && (this.e.text[this.e.LastWrappedLine - 1].flags & 1966080 /*0x1E0000*/) != 0)
              x = 0;
            if ((this.e.PfmtId[ParaId].flags & 12288 /*0x3000*/) != 0 && x != this.e.text[this.e.LastWrappedLine].height && x == 0 && this.e.text[this.e.LastWrappedLine].height == 0 && this.e.RepageBeginLine > this.e.LastWrappedLine && !this.e.repaginating)
              this.e.RepageBeginLine = this.e.LastWrappedLine;
            if (ch3 != char.MinValue && ch3 != '\u0016' && ch3 != '\u000F' && (int) ch3 != (int) this.e.CellChar && ch3 != '\f' && ch3 != '\u0014')
              x = 0;
            if (this.e.TerArg.PageMode && (ch3 == '\u0012' || this.IsHdrFtrChar(ch3)))
              x = 0;
            if (this.e.TerArg.PageMode && (ch3 == '\u0016' || ch3 == '\f'))
              x = this.ScrToUnitY(this.e.RulerFontHeight);
            if (this.e.TerArg.PageMode && ch3 == '\u0014' && this.e.text[this.e.LastWrappedLine].len == 1 && this.e.LastWrappedLine + 1 < this.e.TotalLines)
            {
              int index10 = this.e.TerSect1[this.GetSection(this.e.LastWrappedLine)].NextSect;
              if (index10 < 0 || index10 >= this.e.TotalSects)
                index10 = 0;
              if ((this.e.TerSect[index10].flags & 1) == 0)
                x = 0;
            }
            if (x == 0)
              this.e.text[this.e.LastWrappedLine].flags &= -1537;
            if (this.e.text[this.e.LastWrappedLine].height != x && this.e.PaintFlag < 4)
              this.e.PaintFlag = 4;
            this.e.text[this.e.LastWrappedLine].height = x;
            int num52;
            if ((this.e.TerOpFlags & 4) != 0 && this.e.LastWrappedLine == this.e.CurLine && (num52 = this.MulDiv(x, this.e.ScrResY, 1440)) > this.e.text[this.e.LastWrappedLine].ScrHt)
              this.e.text[this.e.LastWrappedLine].ScrHt = num52;
            this.e.text[this.e.LastWrappedLine].BaseHt = num48;
            if (this.e.text[this.e.LastWrappedLine].BaseHt > this.e.text[this.e.LastWrappedLine].height)
              this.e.text[this.e.LastWrappedLine].BaseHt = this.e.text[this.e.LastWrappedLine].height;
            num5 = 0;
            continue;
          }
          continue;
        case '\u000F':
          this.e.text[this.e.LastWrappedLine].flags |= 128 /*0x80*/;
          goto case char.MinValue;
        default:
          this.e.text[this.e.LastWrappedLine].flags |= 2;
          this.sec.SetHdrFtrLineFlags(this.e.LastWrappedLine, ch3);
          goto case char.MinValue;
      }
    }
    return true;
  }
}
