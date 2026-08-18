// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.CFnt
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class CFnt : COp
{
  private static Dictionary<string, byte> charSetCache = new Dictionary<string, byte>();

  internal CFnt(ImRtfEditor ImRtfEditor)
    : base(ImRtfEditor)
  {
  }

  internal new bool AdjustFontHeight(int NewFont, int ResY, bool apply)
  {
    bool flag = true;
    float num1 = 0.0f;
    ref tc.StrFont local = ref this.e.TerFont[NewFont];
    if (this.e.TerFont[NewFont].CharSet == (byte) 134)
      num1 = 0.018f;
    else if (this.strcmpi(this.e.TerFont[NewFont].TypeFace, "Arial") == 0 || this.strcmpi(this.e.TerFont[NewFont].TypeFace, "Times New Roman") == 0)
      num1 = 0.015975f;
    else if (this.strcmpi(this.e.TerFont[NewFont].TypeFace, "Courier New") == 0)
      num1 = 0.01575f;
    else if (this.strcmpi(this.e.TerFont[NewFont].TypeFace, "Courier") == 0)
      num1 = 0.0166f;
    else if (this.strcmpi(this.e.TerFont[NewFont].TypeFace, "SimSun") == 0)
      num1 = 0.018f;
    else
      flag = false;
    if (flag & apply)
    {
      int num2 = (int) ((double) ResY * (double) num1 * (double) this.e.TerFont[NewFont].TwipsSize / 20.0 + 0.5);
      this.e.TerFont[NewFont].height = num2;
    }
    return flag;
  }

  internal bool ChangeLetterCase(
    int line,
    int BegCol,
    int EndCol,
    bool CaseType,
    ref int StartIndex)
  {
    int num1 = StartIndex;
    int num2 = 6208;
    bool flag = false;
    if (this.e.text[line].fmt == null && (this.e.TerFont[(int) this.e.text[line].UniFmt].style & num2) == 0)
      flag = true;
    char[] txt = this.e.text[line].txt;
    int num3;
    if (flag)
    {
      num3 = EndCol - BegCol + 1;
      if (num3 > 0)
      {
        for (int index = BegCol; index <= EndCol && index < this.e.text[line].len; ++index)
          txt[index] = !CaseType ? char.ToLower(txt[index]) : char.ToUpper(txt[index]);
      }
    }
    else
    {
      ushort[] numArray = this.OpenCfmt(line);
      num3 = 0;
      for (int index = BegCol; index <= EndCol && index < this.e.text[line].len; ++index)
      {
        if ((this.e.TerFont[(int) numArray[index]].style & num2) == 0)
        {
          txt[index] = !CaseType ? char.ToLower(txt[index]) : char.ToUpper(txt[index]);
          ++num3;
        }
      }
      this.CloseCfmt(line);
    }
    int num4 = num1 + num3;
    StartIndex = num4;
    return true;
  }

  internal new bool CharFmt(
    tc.DgtGetNewFontId GetNewFontId,
    int data1,
    int data2,
    string str1,
    bool repaint)
  {
    ++this.e.TerArg.modified;
    if (this.e.HilightType == 1)
    {
      this.CharFmtLine(GetNewFontId, data1, data2, str1, repaint);
      return true;
    }
    if (this.e.HilightType == 2)
    {
      this.CharFmtChr(GetNewFontId, data1, data2, str1, repaint);
      return true;
    }
    ushort effectiveCfmt = (ushort) this.GetEffectiveCfmt();
    this.e.InputFontId = (int) GetNewFontId(effectiveCfmt, data1, data2, str1, this.e.CurLine, this.e.CurCol);
    return true;
  }

  internal new bool CharFmtChr(
    tc.DgtGetNewFontId GetNewFontId,
    int data1,
    int data2,
    string str1,
    bool repaint)
  {
    if (this.NormalizeBlock() && ((this.e.TerFlags2 & 16384 /*0x4000*/) == 0 || !this.BlockHasProtectOn(true, false)))
    {
      this.SaveUndo(this.e.HilightBegRow, this.e.HilightBegCol, this.e.HilightEndRow, this.e.HilightEndCol - 1, 'F');
      if (this.e.HilightBegRow == this.e.HilightEndRow)
      {
        ushort[] numArray = this.OpenCfmt(this.e.HilightBegRow);
        for (int hilightBegCol = this.e.HilightBegCol; hilightBegCol < this.e.HilightEndCol && hilightBegCol < this.e.text[this.e.HilightBegRow].len; ++hilightBegCol)
          numArray[hilightBegCol] = GetNewFontId(numArray[hilightBegCol], data1, data2, str1, this.e.HilightBegRow, hilightBegCol);
        this.CloseCfmt(this.e.HilightBegRow);
      }
      else
      {
        ushort[] numArray1 = this.OpenCfmt(this.e.HilightBegRow);
        for (int hilightBegCol = this.e.HilightBegCol; hilightBegCol < this.e.text[this.e.HilightBegRow].len; ++hilightBegCol)
          numArray1[hilightBegCol] = GetNewFontId(numArray1[hilightBegCol], data1, data2, str1, this.e.HilightBegRow, hilightBegCol);
        this.CloseCfmt(this.e.HilightBegRow);
        ushort[] numArray2 = this.OpenCfmt(this.e.HilightEndRow);
        int col;
        for (col = 0; col < this.e.HilightEndCol && col < this.e.text[this.e.HilightEndRow].len; ++col)
          numArray2[col] = GetNewFontId(numArray2[col], data1, data2, str1, this.e.HilightEndRow, col);
        this.CloseCfmt(this.e.HilightEndRow);
        int num1 = this.e.HilightBegRow + 1;
        int num2 = this.e.HilightEndRow - 1;
        for (int index = num1; index <= num2; ++index)
        {
          if (this.LineSelected(index))
          {
            if (this.e.text[index].fmt == null)
            {
              this.e.text[index].UniFmt = GetNewFontId(this.e.text[index].UniFmt, data1, data2, str1, index, col);
            }
            else
            {
              ushort[] numArray3 = this.OpenCfmt(index);
              for (col = 0; col < this.e.text[index].len; ++col)
                numArray3[col] = GetNewFontId(numArray3[col], data1, data2, str1, index, col);
              this.CloseCfmt(index);
            }
          }
        }
      }
      if ((this.e.TerOpFlags & 8192 /*0x2000*/) != 0)
        this.e.HilightType = 0;
      if (repaint)
        this.PaintTer();
    }
    return true;
  }

  internal new bool CharFmtLine(
    tc.DgtGetNewFontId GetNewFontId,
    int data1,
    int data2,
    string str1,
    bool repaint)
  {
    if (this.NormalizeBlock() && ((this.e.TerFlags2 & 16384 /*0x4000*/) == 0 || !this.BlockHasProtectOn(true, false)))
    {
      this.SaveUndo(this.e.HilightBegRow, 0, this.e.HilightEndRow, this.e.text[this.e.HilightEndRow].len - 1, 'F');
      for (int hilightBegRow = this.e.HilightBegRow; hilightBegRow <= this.e.HilightEndRow; ++hilightBegRow)
      {
        if (this.e.text[hilightBegRow].fmt == null)
        {
          this.e.text[hilightBegRow].UniFmt = GetNewFontId(this.e.text[hilightBegRow].UniFmt, data1, data2, str1, hilightBegRow, 0);
        }
        else
        {
          ushort[] numArray = this.OpenCfmt(hilightBegRow);
          for (int col = 0; col < this.e.text[hilightBegRow].len; ++col)
            numArray[col] = GetNewFontId(numArray[col], data1, data2, str1, hilightBegRow, col);
          this.CloseCfmt(hilightBegRow);
        }
      }
      if ((this.e.TerOpFlags & 8192 /*0x2000*/) != 0)
        this.e.HilightType = 0;
      if (repaint)
        this.PaintTer();
    }
    return true;
  }

  internal new bool CharWidthAlloc(int line, int OldSize, int NewSize)
  {
    ++this.e.TerArg.modified;
    if (this.e.text[line].cwidth == null)
    {
      if (NewSize != 0)
      {
        this.e.text[line].cwidth = new ushort[NewSize + 1];
        for (int col = 0; col < NewSize; ++col)
          this.SetCharWidth(line, col, -1);
      }
      return true;
    }
    if (OldSize == 0 && NewSize == 0)
    {
      this.e.text[line].cwidth = (ushort[]) null;
      return true;
    }
    if (OldSize != NewSize)
    {
      if (NewSize == 0)
      {
        this.e.text[line].cwidth = (ushort[]) null;
        return true;
      }
      if (OldSize > 0)
      {
        this.e.text[line].cwidth = this.ReAlloc(this.e.text[line].cwidth, NewSize + 1);
        for (int col = OldSize; col < NewSize; ++col)
          this.SetCharWidth(line, col, -1);
        return true;
      }
    }
    return true;
  }

  internal new void CloseCfmt(int line)
  {
    if (this.e.text[line].fmt == null)
      return;
    this.CompressCfmt(line);
  }

  internal new void CloseCharInfo(int line)
  {
    if (this.e.text[line].fmt != null)
      this.CompressCfmt(line);
    if (this.e.text[line].tag == null)
      return;
    this.CompressCtid(line);
  }

  internal new void CloseCtid(int line) => this.CompressCtid(line);

  internal new void CompressCfmt(int line)
  {
    if (this.e.text[line].fmt == null || (this.e.TerOpFlags2 & 256 /*0x0100*/) != 0)
      return;
    if (this.e.TerArg.WordWrap)
    {
      if (this.e.text[line].len == 0)
      {
        this.e.text[line].fmt = (ushort[]) null;
        this.e.text[line].UniFmt = (ushort) 0;
        return;
      }
      if (this.e.text[line].tabw != null || (this.e.text[line].flags & 2105408) != 0 || (this.e.text[line].flags & 8) != 0)
        return;
    }
    else if (this.e.text[line].len > 0)
    {
      char[] txt = this.e.text[line].txt;
      int index = 0;
      while (index < this.e.text[line].len && txt[index] != '\t')
        ++index;
      if (index < this.e.text[line].len)
        return;
    }
    ushort[] fmt = this.e.text[line].fmt;
    ushort num = this.e.text[line].len <= 0 ? (ushort) 0 : fmt[0];
    for (int index = 0; index < this.e.text[line].len; ++index)
    {
      if ((int) fmt[index] != (int) num)
        return;
    }
    int modified = this.e.TerArg.modified;
    this.FmtAlloc(line, this.e.text[line].len, 0);
    this.e.TerArg.modified = modified;
    this.e.text[line].fmt = (ushort[]) null;
    this.e.text[line].UniFmt = num;
  }

  internal new void CompressCtid(int line)
  {
    if (this.e.text[line].tag == null)
      return;
    for (int col = 0; col < this.e.text[line].len; ++col)
    {
      if (this.GetCtid(line, col) != 0)
        return;
    }
    int modified = this.e.TerArg.modified;
    this.CtidAlloc(line, this.e.text[line].len, 0);
    this.e.TerArg.modified = modified;
  }

  internal bool ConvertibleToTrueType(string RastFont, ref string TtFont, bool IsTrueType)
  {
    string str = "";
    if (this.strcmpi(RastFont, "Courier") == 0)
      str = "Courier New";
    else if (RastFont.IndexOf("Helv") >= 0)
      str = "Arial";
    else if (this.strcmpi(RastFont, "System") == 0)
      str = "Couier New";
    else if (this.strcmpi(RastFont, "MS Sans Serif") == 0)
      str = "Arial";
    else if (this.strcmpi(RastFont, "MS Serif") == 0)
      str = "Times New Roman";
    else if (!IsTrueType)
      str = "Arial";
    if (str.Length <= 0)
      return false;
    if (TtFont != null)
      TtFont = str;
    return true;
  }

  internal new int CreateGlbFont(COp.LOGFONT lFont, Graphics gr)
  {
    if ((this.e.TerFlags5 & 134217728 /*0x08000000*/) != 0 || tc.InServer)
      return -1;
    bool flag1 = this.GetDeviceCaps(gr, 2) == 1;
    IntPtr opDc = this.GetOpDC(gr);
    int glbFont = 0;
    lock (tc.GlbFontLock)
    {
      for (glbFont = 0; glbFont < tc.TotalGlbFonts; ++glbFont)
      {
        if (tc.GlbFont[glbFont].font != null && (tc.GlbFont[glbFont].hDC == opDc || flag1 && tc.GlbFont[glbFont].IsScrDC) && tc.GlbFont[glbFont].lFont.lfHeight == lFont.lfHeight && tc.GlbFont[glbFont].lFont.lfWidth == lFont.lfWidth && tc.GlbFont[glbFont].lFont.lfEscapement == lFont.lfEscapement && tc.GlbFont[glbFont].lFont.lfOrientation == lFont.lfOrientation && tc.GlbFont[glbFont].lFont.lfWeight == lFont.lfWeight && (int) tc.GlbFont[glbFont].lFont.lfItalic == (int) lFont.lfItalic && (int) tc.GlbFont[glbFont].lFont.lfUnderline == (int) lFont.lfUnderline && (int) tc.GlbFont[glbFont].lFont.lfStrikeOut == (int) lFont.lfStrikeOut && (int) tc.GlbFont[glbFont].lFont.lfCharSet == (int) lFont.lfCharSet && (int) tc.GlbFont[glbFont].lFont.lfOutPrecision == (int) lFont.lfOutPrecision && (int) tc.GlbFont[glbFont].lFont.lfClipPrecision == (int) lFont.lfClipPrecision && (int) tc.GlbFont[glbFont].lFont.lfQuality == (int) lFont.lfQuality && (int) tc.GlbFont[glbFont].lFont.lfPitchAndFamily == (int) lFont.lfPitchAndFamily && tc.GlbFont[glbFont].lFont.lfFaceName == lFont.lfFaceName)
        {
          ++tc.GlbFont[glbFont].UseCount;
          return glbFont;
        }
      }
    }
    bool flag2 = false;
    Font font = (Font) null;
    IntPtr hfont = IntPtr.Zero;
    if (lFont.lfEscapement == 0)
    {
      try
      {
        font = Font.FromLogFont((object) lFont);
        flag2 = true;
      }
      catch (Exception ex)
      {
        lFont.lfFaceName = "Arial";
      }
      if (flag2)
        hfont = font.ToHfont();
    }
    if (!flag2)
    {
      hfont = this.CreateFontIndirect(ref lFont);
      if (IntPtr.Zero == hfont)
        return -1;
      try
      {
        font = Font.FromHfont(hfont);
      }
      catch (Exception ex)
      {
        FontStyle style = FontStyle.Regular;
        if (lFont.lfWeight > 400)
          style |= FontStyle.Bold;
        if (lFont.lfItalic != (byte) 0)
          style |= FontStyle.Italic;
        if (lFont.lfUnderline != (byte) 0)
          style |= FontStyle.Underline;
        if (lFont.lfStrikeOut != (byte) 0)
          style |= FontStyle.Strikeout;
        font = new Font("Arial", (float) Math.Abs(lFont.lfHeight), style);
      }
    }
    lock (tc.GlbFontLock)
    {
      glbFont = 0;
      while (glbFont < tc.TotalGlbFonts && tc.GlbFont[glbFont].font != null)
        ++glbFont;
      if (glbFont == tc.TotalGlbFonts)
      {
        if (tc.TotalGlbFonts == 500)
          return -1;
        glbFont = tc.TotalGlbFonts;
        ++tc.TotalGlbFonts;
      }
      tc.GlbFont[glbFont].UseCount = 1;
      tc.GlbFont[glbFont].font = font;
      tc.GlbFont[glbFont].hFont = hfont;
      tc.GlbFont[glbFont].hDC = opDc;
      tc.GlbFont[glbFont].IsScrDC = flag1;
      tc.GlbFont[glbFont].lFont = lFont;
    }
    return glbFont;
  }

  internal new bool CreateOneFont(Graphics gr, int NewFont, bool ScreenFont)
  {
    COp.LOGFONT lfont = new COp.LOGFONT();
    bool exists = false;
    if ((this.e.TerFont[NewFont].style & 128 /*0x80*/) == 0)
    {
      this.GetDeviceCaps(gr, 2);
      if (this.e.TerFont[NewFont].CharWidth == null)
      {
        this.e.TerFont[NewFont].CharWidth = new int[256 /*0x0100*/];
        this.e.PrtFont[NewFont].CharWidth = new int[256 /*0x0100*/];
      }
      this.e.TerFont[NewFont].TextMetric = new COp.TEXTMETRIC?();
      if (this.e.TerFont[NewFont].font != null)
      {
        this.e.TerCurFont = new Font(this.e.TerArg.FontTypeFace, (float) this.e.TerArg.PointSize);
        this.DeleteTerFont(NewFont);
      }
      if (ScreenFont)
      {
        this.FarMove(this.e.PrtFont[NewFont].CharWidth, this.e.TerFont[NewFont].CharWidth, 256 /*0x0100*/);
        this.CreateOneFont(this.e.TerGr, NewFont, false);
        this.e.PrtFont[NewFont].height = this.e.TerFont[NewFont].height;
        this.e.PrtFont[NewFont].BaseHeight = this.e.TerFont[NewFont].BaseHeight;
        this.e.PrtFont[NewFont].BaseHeightAdj = this.e.TerFont[NewFont].BaseHeightAdj;
        this.FarMove(this.e.TerFont[NewFont].CharWidth, this.e.PrtFont[NewFont].CharWidth, 256 /*0x0100*/);
        this.e.PrtFont[NewFont].ExtLead = this.e.TerFont[NewFont].ExtLead;
        this.e.PrtFont[NewFont].OffsetVal = this.e.TerFont[NewFont].OffsetVal;
        if (this.e.TerFont[NewFont].hidden != null)
        {
          this.e.PrtFont[NewFont].hidden = this.e.TerFont[NewFont].hidden;
          this.e.TerFont[NewFont].hidden = (tc.ClsHdnFont) null;
        }
        lock (this.e.PrtFontLock)
        {
          if (this.e.PrtFont[NewFont].GlbFontId >= 0 || this.e.PrtFont[NewFont].font != null)
            this.DeletePrtFont(NewFont);
          this.e.PrtFont[NewFont].gr = this.e.TerFont[NewFont].gr;
          this.e.PrtFont[NewFont].font = this.e.TerFont[NewFont].font;
          this.e.PrtFont[NewFont].hFont = this.e.TerFont[NewFont].hFont;
          this.e.PrtFont[NewFont].GlbFontId = this.e.TerFont[NewFont].GlbFontId;
          if (this.e.TerFont[NewFont].GlbFontId >= 0)
          {
            lock (tc.GlbFontLock)
              ++tc.GlbFont[this.e.TerFont[NewFont].GlbFontId].UseCount;
          }
        }
      }
      int num1;
      int num2;
      if (this.e.InPrinting || !ScreenFont || this.e.ZoomPercent == 100 || this.e.ZoomPercent == 0)
      {
        num1 = ScreenFont ? this.GetDeviceCaps(gr, 90) : 1440;
        num2 = this.MulDiv(this.e.TerFont[NewFont].TwipsSize, num1, 1440);
      }
      else
      {
        num1 = this.e.OrigScrResY;
        num2 = this.e.TerFont[NewFont].TwipsSize * num1 * this.e.ZoomPercent / 144000;
      }
      int style = this.e.TerFont[NewFont].style;
      if (this.e.TerFont[NewFont].InsRev != 0)
        style |= this.e.reviewer[this.e.TerFont[NewFont].InsRev].InsStyle;
      if (this.e.TerFont[NewFont].DelRev != 0)
        style |= this.e.reviewer[this.e.TerFont[NewFont].DelRev].DelStyle;
      byte num3 = this.e.TerFont[NewFont].CharSet;
      byte charSet = this.GetCharSet(gr, this.e.TerFont[NewFont].TypeFace, ref exists);
      if (num3 == (byte) 1 || charSet == (byte) 2)
        num3 = charSet;
      byte num4;
      bool IsTrueType;
      if (ScreenFont && gr == this.e.TerGr && this.e.TerArg.PrintView && !this.e.UsingZoomFonts && this.e.PrinterAvailable && this.e.PrtGr != null)
      {
        num4 = this.e.SavePrtPitchFamily;
        IsTrueType = this.e.SavePrtIsTrueType;
      }
      else
      {
        IsTrueType = exists;
        num4 = (byte) 0;
      }
      if (((int) num4 & 240 /*0xF0*/) == 0)
        num4 = this.e.TerFont[NewFont].FontFamily;
      if (this.e.TerFont[NewFont].FontFamily == (byte) 0 && gr == this.e.TerGr)
        this.e.TerFont[NewFont].FontFamily = (byte) ((uint) num4 & 240U /*0xF0*/);
      if (!ScreenFont)
      {
        this.e.SavePrtPitchFamily = num4;
        this.e.SavePrtIsTrueType = IsTrueType;
      }
      int num5 = num3 == (byte) 134 || num3 == (byte) 136 ? 1 : (num3 == (byte) 128 /*0x80*/ ? 1 : 0);
      int x1;
      if ((this.e.TerFlags3 & 67108864 /*0x04000000*/) == 0 & ScreenFont && gr == this.e.TerGr && this.e.TerArg.PrintView && !this.e.UsingZoomFonts && this.e.PrinterAvailable && this.e.PrtGr != null && this.e.SavePrtFontHeight > 0)
      {
        x1 = this.UnitToScrY(this.e.SavePrtFontHeight) + 1;
      }
      else
      {
        int x2 = num2;
        if ((style & 32 /*0x20*/) != 0 || (style & 16 /*0x10*/) != 0)
        {
          x2 = (x2 + 1) / 2;
          if (this.e.TerFont[NewFont].TwipsSize / 20 <= 14)
            x2 = this.MulDiv(x2, 5, 4);
        }
        else if ((style & 131072 /*0x020000*/) != 0 && (this.e.TerFont[NewFont].flags & 512 /*0x0200*/) != 0)
          x2 = this.MulDiv(x2, 2, 3);
        if (x2 == 0)
          x2 = 1;
        x1 = -x2;
      }
      int num6 = ScreenFont ? 1 : 0;
      if ((num5 & num6) != 0 && !this.e.InPrinting && (this.e.TerFlags3 & 67108864 /*0x04000000*/) == 0)
        x1 = this.MulDiv(x1, 9, 10);
      lfont.lfHeight = x1;
      int num7;
      lfont.lfOrientation = num7 = 0;
      lfont.lfWidth = num7;
      lfont.lfEscapement = tc.OSCanRotate ? 0 : this.e.TerFont[NewFont].TextAngle * 10;
      lfont.lfOrientation = 0;
      lfont.lfWeight = 0;
      lfont.lfItalic = (this.e.TerFont[NewFont].style & 4) != 0 ? (byte) 1 : (byte) 0;
      lfont.lfUnderline = (this.e.TerFont[NewFont].TempStyle & 2) != 0 || (this.e.TerFont[NewFont].style & 1) != 0 && (this.e.TerFont[NewFont].style & 48 /*0x30*/) != 0 ? (byte) 1 : (byte) 0;
      lfont.lfStrikeOut = (style & 8) != 0 ? (byte) 1 : (byte) 0;
      lfont.lfCharSet = num3 == (byte) 77 ? (byte) 0 : num3;
      lfont.lfOutPrecision = this.e.UsingZoomFonts ? (byte) 7 : (byte) 0;
      lfont.lfClipPrecision = (byte) 0;
      lfont.lfQuality = (byte) 0;
      lfont.lfPitchAndFamily = num4;
      lfont.lfFaceName = this.e.TerFont[NewFont].TypeFace;
      if (num3 != (byte) 0 && num3 != (byte) 2 && lfont.lfFaceName == "Times")
        lfont.lfFaceName = "Times New Roman";
      this.ConvertibleToTrueType(this.e.TerFont[NewFont].TypeFace, ref lfont.lfFaceName, IsTrueType);
      int num8 = 0;
      lfont.lfWeight = (style & 2) != 0 ? 700 : 400;
      lfont.lfItalic = (style & 4) != 0 ? (byte) 1 : (byte) 0;
      int glbFont = this.CreateGlbFont(lfont, gr);
      if (glbFont >= 0)
      {
        lock (tc.GlbFontLock)
        {
          this.e.TerFont[NewFont].font = tc.GlbFont[glbFont].font;
          this.e.TerFont[NewFont].hFont = tc.GlbFont[glbFont].hFont;
          this.e.TerFont[NewFont].GlbFontId = glbFont;
        }
      }
      else
      {
        lock (this.e.TerFontLock)
        {
          bool flag = false;
          if (lfont.lfEscapement == 0)
          {
            try
            {
              this.e.TerFont[NewFont].font = Font.FromLogFont((object) lfont);
              flag = true;
            }
            catch (Exception ex)
            {
              lfont.lfFaceName = "Arial";
            }
            if (flag)
              this.e.TerFont[NewFont].hFont = this.e.TerFont[NewFont].font.ToHfont();
          }
          if (!flag)
          {
            IntPtr fontIndirect;
            this.e.TerFont[NewFont].hFont = fontIndirect = this.CreateFontIndirect(ref lfont);
            if (IntPtr.Zero == fontIndirect)
              return false;
            this.e.TerFont[NewFont].font = Font.FromHfont(this.e.TerFont[NewFont].hFont);
          }
          this.e.TerFont[NewFont].GlbFontId = -1;
        }
      }
      if (this.e.TerFont[NewFont].font == null)
        return this.PrintError(42, "CreateOneFont(a)");
      this.e.TerFont[NewFont].gr = gr;
      this.e.TerFont[NewFont].CharSet = num3;
      COp.TEXTMETRIC tm;
      if (!this.GetTextMetrics(gr, this.e.TerFont[NewFont].font, out tm))
        return this.PrintError(42, "CreateOneFont(c)");
      if (ScreenFont && !this.e.FullRenderMode)
        this.e.TerFont[NewFont].TextMetric = new COp.TEXTMETRIC?(tm);
      if (tm.tmAscent == 0)
        return this.PrintError(42, "CreateOneFont(d)");
      this.e.TerFont[NewFont].ExtLead = tm.tmExternalLeading;
      int fontLanguageInfo = this.GetFontLanguageInfo(gr, this.e.TerFont[NewFont].font);
      this.e.TerFont[NewFont].VarWidth = (fontLanguageInfo & 48 /*0x30*/) != 0;
      this.e.TerFont[NewFont].rtl = (fontLanguageInfo & 2) != 0;
      if (this.e.TerFont[NewFont].VarWidth || this.e.TerFont[NewFont].rtl)
        this.e.HasVarWidthFont = true;
      if (!ScreenFont)
      {
        if (IsTrueType)
          this.e.SavePrtFontHeight = 0;
        else
          this.e.SavePrtFontHeight = tm.tmHeight;
      }
      if (this.e.UsingZoomFonts & ScreenFont)
        this.e.TerFont[NewFont].ExtLead = 0;
      if (!this.e.UsingZoomFonts)
      {
        if (!this.TerGetCharWidth(gr, NewFont, ScreenFont, tm.tmOverhang, tm.tmPitchAndFamily))
          return this.PrintError(42, "CreateOneFont(d)");
        if (tm.tmOverhang > 1)
          this.e.TerFont[NewFont].flags |= 1;
        else
          this.e.TerFont[NewFont].flags = tc.ResetUintFlag(ref this.e.TerFont[NewFont].flags, 1);
        this.e.TerFont[NewFont].height = tm.tmHeight + tm.tmExternalLeading;
        this.e.TerFont[NewFont].BaseHeightAdj = 0;
        if (num8 == 0)
          num8 = tm.tmAscent;
        else
          this.e.TerFont[NewFont].BaseHeightAdj = num8 - tm.tmAscent;
        this.e.TerFont[NewFont].BaseHeight = num8;
        if ((!ScreenFont || this.e.InPrinting) && (style & 131120 /*0x020030*/) == 0)
          this.AdjustFontHeight(NewFont, num1, true);
        int num9 = this.e.TerFont[NewFont].offset * num1 / 1440;
        this.e.TerFont[NewFont].OffsetVal = num9;
        if ((style & 16 /*0x10*/) != 0 || (style & 32 /*0x20*/) != 0)
        {
          int height = this.e.TerFont[NewFont].height;
          this.e.TerFont[NewFont].height = num2;
          this.e.TerFont[NewFont].BaseHeight = tm.tmAscent * num2 / height;
          if ((style & 16 /*0x10*/) != 0)
          {
            this.e.TerFont[NewFont].BaseHeightAdj = -(int) ((double) tm.tmAscent / 4.0);
            int num10 = this.e.TerFont[NewFont].BaseHeight - this.e.TerFont[NewFont].height;
            if (num10 < this.e.TerFont[NewFont].BaseHeightAdj)
              this.e.TerFont[NewFont].BaseHeightAdj = num10;
          }
          else
            this.e.TerFont[NewFont].BaseHeightAdj = num2 - height;
        }
        if (this.e.TerFont[NewFont].offset != 0)
        {
          if (num9 > 0)
            this.e.TerFont[NewFont].BaseHeight += num9;
          else
            this.e.TerFont[NewFont].BaseHeightAdj = -num9;
          this.e.TerFont[NewFont].height += Math.Abs(num9);
        }
        if ((this.e.TerFont[NewFont].style & 8192 /*0x2000*/) != 0)
        {
          if (ScreenFont)
            this.e.TerFont[NewFont].BaseHeightAdj = this.TwipsToScrY(40);
          else
            this.e.TerFont[NewFont].BaseHeightAdj = this.TwipsToUnitY(40);
          this.e.TerFont[NewFont].height += 7 * this.e.TerFont[NewFont].BaseHeightAdj / 4;
          this.e.TerFont[NewFont].BaseHeight += this.e.TerFont[NewFont].BaseHeightAdj;
        }
        if (this.edit.HiddenText(NewFont))
        {
          if (this.e.TerFont[NewFont].hidden == null)
            this.e.TerFont[NewFont].hidden = new tc.ClsHdnFont();
          this.e.TerFont[NewFont].hidden.height = this.e.TerFont[NewFont].height;
          this.e.TerFont[NewFont].hidden.BaseHeight = this.e.TerFont[NewFont].BaseHeight;
          this.e.TerFont[NewFont].hidden.ExtLead = this.e.TerFont[NewFont].ExtLead;
          this.e.TerFont[NewFont].hidden.BaseHeightAdj = this.e.TerFont[NewFont].BaseHeightAdj;
          this.e.TerFont[NewFont].hidden.CharWidth = new int[256 /*0x0100*/];
          this.FarMove(this.e.TerFont[NewFont].CharWidth, this.e.TerFont[NewFont].hidden.CharWidth, 256 /*0x0100*/);
          int num11;
          this.e.TerFont[NewFont].BaseHeight = num11 = 0;
          this.e.TerFont[NewFont].height = num11;
          int num12;
          this.e.TerFont[NewFont].BaseHeightAdj = num12 = 0;
          this.e.TerFont[NewFont].ExtLead = num12;
          for (int index = 0; index < 256 /*0x0100*/; ++index)
            this.e.TerFont[NewFont].CharWidth[index] = 0;
        }
        else if (this.e.TerFont[NewFont].hidden != null)
          this.e.TerFont[NewFont].hidden = (tc.ClsHdnFont) null;
      }
    }
    return true;
  }

  internal new bool CtidAlloc(int line, int OldSize, int NewSize)
  {
    ++this.e.TerArg.modified;
    if (this.e.text[line].tag == null)
    {
      if (NewSize != 0)
      {
        this.e.text[line].tag = new ushort[NewSize + 1];
        for (int col = 0; col < NewSize; ++col)
          this.SetCtid(line, col, 0);
      }
      return true;
    }
    if (OldSize == 0 && NewSize == 0)
    {
      this.e.text[line].tag = (ushort[]) null;
      return true;
    }
    if (OldSize != NewSize)
    {
      if (NewSize == 0)
      {
        this.e.text[line].tag = (ushort[]) null;
        return true;
      }
      if (OldSize > 0)
      {
        this.e.text[line].tag = this.ReAlloc(this.e.text[line].tag, NewSize + 1);
        for (int col = OldSize; col < NewSize; ++col)
          this.SetCtid(line, col, 0);
        return true;
      }
    }
    return true;
  }

  internal new int DeleteTag(int line, int col, int type, string name)
  {
    if (line < 0)
    {
      line = this.e.CurLine;
      col = this.e.CurCol;
    }
    if (line < 0 || line >= this.e.TotalLines || col < 0 || col >= this.e.text[line].len)
      return 0;
    ushort[] numArray = this.OpenCtid(line);
    int TagId = (int) numArray[col];
    if (TagId < this.e.TotalCharTags && TagId != 0)
    {
      int tag = 0;
      int num = TagId;
      while (type != -1 && this.e.CharTag[TagId].type != type || name != null && !(this.e.CharTag[TagId].name == name))
      {
        tag = TagId;
        if (this.e.CharTag[TagId].next == num || TagId == this.e.CharTag[TagId].next)
        {
          this.e.CharTag[TagId].next = 0;
          TagId = 0;
          goto label_14;
        }
        TagId = this.e.CharTag[TagId].next;
        if (TagId == 0)
          goto label_14;
      }
      this.FreeTag(TagId);
      int next = this.e.CharTag[TagId].next;
      if (tag != 0)
      {
        this.e.CharTag[tag].next = next;
        if (this.e.CheckEndlessLoopTags(tag))
          this.e.CharTag[tag].next = 0;
      }
      else
        numArray[col] = (ushort) next;
    }
label_14:
    this.CloseCtid(line);
    ++this.e.TerArg.modified;
    return TagId;
  }

  internal new bool DeleteTerObject(int idx)
  {
    if (this.e.TerFont[idx].InUse)
    {
      if ((this.e.TerFont[idx].style & 128 /*0x80*/) != 0)
      {
        if (this.e.TerFont[idx].image != null)
        {
          this.e.TerFont[idx].image.Dispose();
          this.e.TerFont[idx].image = (Image) null;
        }
        if (this.e.TerFont[idx].hMeta != IntPtr.Zero)
        {
          CRtfw.DeleteEnhMetaFile(this.e.TerFont[idx].hMeta);
          COp.Win32.DeleteMetaFile(this.e.TerFont[idx].hMeta);
        }
        this.e.TerFont[idx].hMeta = IntPtr.Zero;
        try
        {
          if (this.e.TerFont[idx].PictFile != null)
          {
            if (this.e.TerFont[idx].PictFile.Length > 0)
              File.Delete(this.e.TerFont[idx].PictFile);
          }
        }
        catch (Exception ex)
        {
        }
        this.e.TerFont[idx].PictFile = (string) null;
        if (this.e.TerFont[idx].PictType == 2 || this.e.TerFont[idx].PictType == 6)
        {
          this.e.TerFont[idx].InUse = false;
          if (this.e.TerFont[idx].ctl != null)
          {
            this.e.TerFont[idx].ctl.Dispose();
            this.e.TerFont[idx].ctl = (Control) null;
          }
          this.e.Validate();
        }
      }
      else
      {
        this.DeleteTerFont(idx);
        this.DeletePrtFont(idx);
      }
      this.InitTerObject(idx);
    }
    return true;
  }

  /// <summary>очистка e.PrtFont[idx]</summary>
  /// <param name="idx">индекс в массиве</param>
  internal void DeletePrtFont(int idx)
  {
    lock (this.e.PrtFontLock)
    {
      if (this.e.PrtFont[idx].font == null)
        return;
      int glbFontId = this.e.PrtFont[idx].GlbFontId;
      if (glbFontId >= 0)
      {
        lock (tc.GlbFontLock)
        {
          if (tc.GlbFont != null)
            --tc.GlbFont[glbFontId].UseCount;
        }
      }
      else
      {
        try
        {
          this.e.PrtFont[idx].font.Dispose();
          this.e.PrtFont[idx].font = (Font) null;
          if (this.e.PrtFont[idx].hFont != IntPtr.Zero)
          {
            this.DeleteObject(this.e.PrtFont[idx].hFont);
            this.e.PrtFont[idx].hFont = IntPtr.Zero;
          }
        }
        catch (Exception ex)
        {
        }
      }
      this.e.PrtFont[idx].font = (Font) null;
      this.e.PrtFont[idx].hFont = IntPtr.Zero;
      this.e.PrtFont[idx].GlbFontId = -1;
    }
  }

  /// <summary>очистка e.PrtFont[idx]</summary>
  /// <param name="idx">индекс в массиве</param>
  internal void DeleteTerFont(int idx)
  {
    lock (this.e.TerFontLock)
    {
      if (this.e.TerFont[idx].font == null)
        return;
      int glbFontId = this.e.TerFont[idx].GlbFontId;
      if (glbFontId >= 0)
      {
        lock (tc.GlbFontLock)
        {
          if (tc.GlbFont != null)
            --tc.GlbFont[glbFontId].UseCount;
        }
      }
      else
      {
        try
        {
          this.e.TerFont[idx].font.Dispose();
          this.e.TerFont[idx].font = (Font) null;
          if (this.e.TerFont[idx].hFont != IntPtr.Zero)
          {
            this.DeleteObject(this.e.TerFont[idx].hFont);
            this.e.TerFont[idx].hFont = IntPtr.Zero;
          }
        }
        catch (Exception ex)
        {
        }
      }
      this.e.TerFont[idx].font = (Font) null;
      this.e.TerFont[idx].hFont = IntPtr.Zero;
      this.e.TerFont[idx].gr = (Graphics) null;
      this.e.TerFont[idx].GlbFontId = -1;
    }
  }

  internal new void ExpandCfmt(int line)
  {
    if (this.e.text[line].fmt != null || this.e.text[line].len == 0)
      return;
    ushort uniFmt = this.e.text[line].UniFmt;
    int modified = this.e.TerArg.modified;
    this.FmtAlloc(line, 0, this.e.text[line].len);
    this.e.TerArg.modified = modified;
    ushort[] fmt = this.e.text[line].fmt;
    for (int index = 0; index < this.e.text[line].len; ++index)
      fmt[index] = uniFmt;
  }

  internal new void ExpandCtid(int line)
  {
    if (this.e.text[line].tag != null || this.e.text[line].len == 0)
      return;
    int modified = this.e.TerArg.modified;
    this.CtidAlloc(line, 0, this.e.text[line].len);
    this.e.TerArg.modified = modified;
  }

  internal new bool ExpandFontTable(int NewMaxFonts)
  {
    int maxFonts = this.e.MaxFonts;
    tc.StrFont[] terFont = this.e.TerFont;
    this.e.TerFont = new tc.StrFont[NewMaxFonts];
    for (int index = 0; index < this.e.MaxFonts; ++index)
      this.e.TerFont[index] = terFont[index];
    tc.StrPrtFont[] prtFont = this.e.PrtFont;
    this.e.PrtFont = new tc.StrPrtFont[NewMaxFonts];
    for (int index = 0; index < this.e.MaxFonts; ++index)
      this.e.PrtFont[index] = prtFont[index];
    this.e.MaxFonts = NewMaxFonts;
    for (int index = maxFonts; index < NewMaxFonts; ++index)
    {
      int[] numArray;
      this.e.PrtFont[index].CharWidth = numArray = (int[]) null;
      this.e.TerFont[index].CharWidth = numArray;
      this.e.TerFont[index].InUse = false;
    }
    return true;
  }

  internal new int FindOpenSlot()
  {
    if (this.e.NextFontId >= 0)
    {
      int nextFontId = this.e.NextFontId;
      this.e.NextFontId = -1;
      this.DeleteTerObject(nextFontId);
      this.InitTerObject(nextFontId);
      return nextFontId;
    }
    if (this.e.ReclaimResources && !this.e.InPrinting && !this.e.InRtfRead)
      this.FreeFontResources(false);
    for (int idx = 0; idx < this.e.TotalFonts; ++idx)
    {
      if (!this.e.TerFont[idx].InUse)
      {
        this.InitTerObject(idx);
        return idx;
      }
    }
    if (this.e.TotalFonts >= this.e.MaxFonts)
      this.ExpandFontTable(this.e.MaxFonts + this.e.MaxFonts / 3 + 1);
    int totalFonts = this.e.TotalFonts;
    ++this.e.TotalFonts;
    this.InitTerObject(totalFonts);
    return totalFonts;
  }

  internal new bool FmtAlloc(int line, int OldSize, int NewSize)
  {
    ++this.e.TerArg.modified;
    if (this.e.text[line].fmt == null && OldSize != 0)
    {
      ushort uniFmt = this.e.text[line].UniFmt;
      this.e.text[line].fmt = new ushort[OldSize + 1];
      ushort[] fmt = this.e.text[line].fmt;
      for (int index = 0; index < OldSize; ++index)
        fmt[index] = uniFmt;
    }
    if (OldSize == 0 && NewSize == 0)
    {
      this.e.text[line].fmt = (ushort[]) null;
      return true;
    }
    if (OldSize != NewSize)
    {
      if (NewSize == 0)
      {
        this.e.text[line].fmt = (ushort[]) null;
        this.e.text[line].UniFmt = (ushort) 0;
        return true;
      }
      if (OldSize > 0)
      {
        this.e.text[line].fmt = this.ReAlloc(this.e.text[line].fmt, NewSize + 1);
        for (int index = OldSize; index < NewSize; ++index)
          this.e.text[line].fmt[index] = (ushort) 0;
        return true;
      }
      if (OldSize == 0)
      {
        this.e.text[line].fmt = new ushort[NewSize + 1];
        for (int index = 0; index < NewSize; ++index)
          this.e.text[line].fmt[index] = (ushort) 0;
        return true;
      }
    }
    return true;
  }

  internal new bool FreeFontResources(bool always)
  {
    bool[] flagArray = new bool[320];
    int[] numArray = new int[320];
    if (!always)
    {
      int num = 0;
      for (int index = 0; index < this.e.TotalFonts; ++index)
      {
        if (this.e.TerFont[index].InUse && (this.e.TerFont[index].style & 128 /*0x80*/) == 0)
          ++num;
      }
      if (num <= 10)
        return false;
    }
    numArray[0] = 0;
    for (int index1 = 1; index1 < this.e.MaxFonts; ++index1)
    {
      numArray[index1] = index1;
      if ((this.e.TerFont[index1].style & 128 /*0x80*/) == 0)
      {
        for (int index2 = 0; index2 <= index1; ++index2)
        {
          if (this.strcmpi(this.e.TerFont[index2].TypeFace, this.e.TerFont[index1].TypeFace) == 0 && this.e.TerFont[index2].TwipsSize == this.e.TerFont[index1].TwipsSize && (this.e.TerFont[index2].style & 128 /*0x80*/) == 0 && this.e.TerFont[index2].style == this.e.TerFont[index1].style && this.e.TerFont[index2].TempStyle == this.e.TerFont[index1].TempStyle && this.e.TerFont[index2].TextColor == this.e.TerFont[index1].TextColor && this.e.TerFont[index2].TextBkColor == this.e.TerFont[index1].TextBkColor && this.e.TerFont[index2].AuxId == this.e.TerFont[index1].AuxId && this.e.TerFont[index2].Aux1Id == this.e.TerFont[index1].Aux1Id && this.e.TerFont[index2].flags == this.e.TerFont[index1].flags && (int) this.e.TerFont[index2].CharSet == (int) this.e.TerFont[index1].CharSet && this.e.TerFont[index2].CharStyId == this.e.TerFont[index1].CharStyId && this.e.TerFont[index2].ParaStyId == this.e.TerFont[index1].ParaStyId && this.e.TerFont[index2].expand == this.e.TerFont[index1].expand && this.e.TerFont[index2].FieldId == this.e.TerFont[index1].FieldId)
          {
            numArray[index1] = index2;
            break;
          }
        }
      }
    }
    for (int index = 0; index < this.e.MaxFonts; ++index)
      flagArray[index] = false;
    for (int index = 0; index < this.e.TotalFonts; ++index)
    {
      if (this.e.TerFont[index].InUse)
      {
        if ((this.e.TerFont[index].style & 128 /*0x80*/) == 0 && (this.e.TerFont[index].flags & 256 /*0x0100*/) != 0)
        {
          flagArray[index] = true;
          numArray[index] = index;
        }
        else if ((this.e.TerFont[index].style & 128 /*0x80*/) != 0 && this.e.TerFont[index].PictType == 6)
        {
          int fontId = this.e.TerFont[index].form.FontId;
          if (fontId >= 0 && fontId < this.e.TotalFonts && this.e.TerFont[fontId].InUse)
          {
            flagArray[fontId] = true;
            numArray[fontId] = fontId;
          }
        }
        else if ((this.e.TerFont[index].style & 128 /*0x80*/) != 0 && this.e.TerFont[index].ObjectType == 6)
        {
          flagArray[index] = true;
          numArray[index] = index;
        }
      }
    }
    for (int index3 = 1; index3 < this.e.TotalLists; ++index3)
    {
      if (this.e.list[index3].InUse)
      {
        for (int index4 = 0; index4 < this.e.list[index3].LevelCount; ++index4)
        {
          int fontId = this.e.list[index3].level[index4].FontId;
          flagArray[fontId] = true;
          numArray[fontId] = fontId;
        }
      }
    }
    for (int index5 = 1; index5 < this.e.TotalListOr; ++index5)
    {
      if (this.e.ListOr[index5].InUse)
      {
        for (int index6 = 0; index6 < this.e.ListOr[index5].LevelCount; ++index6)
        {
          int fontId = this.e.ListOr[index5].level[index6].FontId;
          flagArray[fontId] = true;
          numArray[fontId] = fontId;
        }
      }
    }
    if (this.e.InputFontId >= 0 && this.e.InputFontId < this.e.TotalFonts && this.e.TerFont[this.e.InputFontId].InUse)
    {
      flagArray[this.e.InputFontId] = true;
      numArray[this.e.InputFontId] = this.e.InputFontId;
    }
    for (int index7 = 0; index7 < this.e.TotalLines; ++index7)
    {
      if (this.e.text[index7].fmt == null)
      {
        int uniFmt = (int) this.e.text[index7].UniFmt;
        int index8 = numArray[uniFmt];
        this.e.text[index7].UniFmt = (ushort) index8;
        flagArray[index8] = true;
      }
      else
      {
        ushort[] fmt = this.e.text[index7].fmt;
        for (int index9 = 0; index9 < this.e.text[index7].len; ++index9)
        {
          int index10 = (int) fmt[index9];
          int index11 = numArray[index10];
          fmt[index9] = (ushort) index11;
          flagArray[index11] = true;
        }
      }
    }
    bool flag = false;
    for (int idx = 1; idx < this.e.MaxFonts; ++idx)
    {
      if (!this.e.TerFont[idx].InUse || (this.e.TerFont[idx].style & 128 /*0x80*/) == 0)
      {
        if (this.e.TerFont[idx].InUse && numArray[idx] != idx)
        {
          this.DeleteTerObject(idx);
          flag = true;
        }
        if (this.e.TerFont[idx].InUse && !flagArray[idx])
        {
          this.DeleteTerObject(idx);
          flag = true;
        }
      }
    }
    if (flag)
      this.TerShrinkFontTable();
    return flag;
  }

  internal new bool FreeTag(int TagId)
  {
    if (TagId >= 0 && TagId < this.e.TotalCharTags && this.e.CharTag[TagId].InUse)
    {
      this.e.CharTag[TagId].HtmlInfo = (string) null;
      this.e.CharTag[TagId].AuxText = (string) null;
      this.e.CharTag[TagId].name = (string) null;
      this.e.CharTag[TagId].type = 0;
      this.e.CharTag[TagId].InUse = false;
    }
    return true;
  }

  internal new byte GetCharSet(Graphics gr, string typeface, ref bool exists)
  {
    byte charSet = 1;
    lock (CFnt.charSetCache)
    {
      bool flag = CFnt.charSetCache.TryGetValue(typeface, out charSet);
      if (flag && typeface != "Symbol")
      {
        exists = true;
        return charSet;
      }
      FontFamily[] families = FontFamily.Families;
      exists = false;
      for (int index = 0; index < families.Length; ++index)
      {
        if (this.strcmpi(families[index].Name, typeface) == 0)
        {
          exists = true;
          break;
        }
      }
      if (exists)
      {
        Font font;
        try
        {
          FontFamily family = new FontFamily(typeface);
          FontStyle? nullable = new FontStyle?();
          foreach (FontStyle style in Enum.GetValues(typeof (FontStyle)))
          {
            if (family.IsStyleAvailable(style))
            {
              nullable = new FontStyle?(style);
              break;
            }
          }
          if (!nullable.HasValue)
          {
            CFnt.charSetCache.Add(typeface, charSet);
            return charSet;
          }
          font = new Font(family, 12f, nullable.Value);
        }
        catch (Exception ex)
        {
          CFnt.charSetCache.Add(typeface, charSet);
          return charSet;
        }
        IntPtr hfont = font.ToHfont();
        COp.LOGFONT lf;
        charSet = !(hfont != IntPtr.Zero) || !this.GetLogFont(hfont, out lf) ? font.GdiCharSet : lf.lfCharSet;
        this.DeleteObject(hfont);
        font.Dispose();
      }
      if (!flag)
        CFnt.charSetCache.Add(typeface, charSet);
    }
    return charSet;
  }

  internal new int GetCharWidth(int line, int col) => (int) this.e.text[line].cwidth[col];

  internal new int GetCtid(int line, int col) => (int) this.e.text[line].tag[col];

  internal new int GetCurCfmt(int line, int col)
  {
    if (line < 0 || line >= this.e.TotalLines || col < 0 || col >= this.e.text[line].len)
      return 0;
    if (this.e.text[line].fmt == null)
      return (int) this.e.text[line].UniFmt;
    int curCfmt = (int) this.OpenCfmt(line)[col];
    this.CloseCfmt(line);
    return curCfmt;
  }

  internal new int GetEffectiveCfmt()
  {
    bool flag1 = false;
    int pPrevFont = 0;
    if (this.e.InputFontId >= 0 && (this.e.TerFont[this.e.InputFontId].style & 128 /*0x80*/) != 0)
      this.e.InputFontId = -1;
    if (this.e.InputFontId >= 0)
      return this.e.InputFontId;
    if (this.e.HilightType == 2)
    {
      int line;
      int index;
      if (this.e.HilightEndRow < this.e.HilightBegRow || this.e.HilightEndRow == this.e.HilightBegRow && this.e.HilightEndCol < this.e.HilightBegCol)
      {
        line = this.e.HilightEndRow;
        index = this.e.HilightEndCol;
      }
      else
      {
        line = this.e.HilightBegRow;
        index = this.e.HilightBegCol;
      }
      if (index >= 0 && this.e.text[line].len > index)
      {
        pPrevFont = (int) this.OpenCfmt(line)[index];
        this.CloseCfmt(line);
      }
      if (this.e.HilightBegRow == this.e.HilightEndRow && Math.Abs(this.e.HilightEndCol - this.e.HilightBegCol) == 1 && (this.e.TerFont[pPrevFont].style & 128 /*0x80*/) != 0)
      {
        int style = this.e.TerFont[pPrevFont].style;
        tc.ResetUintFlag(ref style, 128 /*0x80*/);
        pPrevFont = this.e.InputFontId = this.SetFontStyle(0, style, true);
      }
      if ((this.e.TerFont[pPrevFont].style & 512 /*0x0200*/) != 0)
      {
        this.e.InputFontId = pPrevFont;
        return this.e.InputFontId;
      }
    }
    else
    {
      if (this.e.CurLine > 0)
      {
        if (this.e.text[this.e.CurLine].len == 0)
          flag1 = true;
        else if (this.e.CurCol == 0)
        {
          if (this.e.HtmlMode)
          {
            if (this.e.text[this.e.CurLine].pfmt == this.e.text[this.e.CurLine - 1].pfmt && this.e.text[this.e.CurLine].len == 1)
              flag1 = true;
          }
          else
          {
            if (this.e.text[this.e.CurLine].len <= 1)
              flag1 = true;
            else if ((this.e.text[this.e.CurLine - 1].flags & 3) == 0)
              flag1 = true;
            else if (this.e.CurLine + 1 == this.e.TotalLines && (this.e.text[this.e.CurLine].flags & 1) != 0 && this.e.text[this.e.CurLine].len == 1)
              flag1 = true;
            else if (this.e.EnterHit && (this.e.text[this.e.CurLine].flags & 1) != 0 && this.e.text[this.e.CurLine].len == 1)
              flag1 = true;
            if (flag1 && this.e.CurLine > 0 && this.e.text[this.e.CurLine - 1].cid != this.e.text[this.e.CurLine].cid)
              flag1 = false;
            if (flag1 && this.e.CurLine > 0 && this.e.text[this.e.CurLine - 1].fid != this.e.text[this.e.CurLine].fid)
              flag1 = false;
          }
        }
      }
      for (; flag1; flag1 = false)
      {
        int num = 0;
        char minValue = char.MinValue;
        int line;
        for (line = this.e.CurLine - 1; line >= 0; --line)
        {
          if (this.e.text[line].len != 0)
          {
            minValue = this.e.text[line].txt[this.e.text[line].len - 1];
            num = this.e.text[line].len - ((this.e.text[line].flags & 2) != 0 ? 1 : 0);
            if (num > 0)
              break;
          }
        }
        if (line >= 0 && num > 0)
        {
          if (this.IsBreakChar(minValue) && (int) minValue != (int) this.e.ParaChar && minValue != '\u000F')
          {
            pPrevFont = this.GetCurCfmt(this.e.CurLine, 0);
            goto label_63;
          }
          pPrevFont = (int) this.OpenCfmt(line)[num - 1];
          this.CloseCfmt(line);
          goto label_63;
        }
      }
      if (this.e.TotalLines == 1 && this.e.text[0].len == 1)
        pPrevFont = this.GetCurCfmt(0, 0);
      else if (this.e.text[this.e.CurLine].fmt == null)
        pPrevFont = (int) this.e.text[this.e.CurLine].UniFmt;
      else if (this.e.CurCol >= this.e.text[this.e.CurLine].len && this.e.text[this.e.CurLine].len > 0)
      {
        pPrevFont = (int) this.OpenCfmt(this.e.CurLine)[this.e.text[this.e.CurLine].len - 1];
        this.CloseCfmt(this.e.CurLine);
      }
      else
      {
        ushort[] numArray = this.OpenCfmt(this.e.CurLine);
        int index1 = this.e.CurCol <= 0 || this.e.CurCol - 1 >= this.e.text[this.e.CurLine].len ? 0 : (int) numArray[this.e.CurCol - 1];
        if (this.e.HtmlMode && (this.e.TerFont[index1].style & 16384 /*0x4000*/) != 0)
          pPrevFont = (int) numArray[this.e.CurCol];
        else if (this.e.CurCol == 0)
        {
          pPrevFont = (int) numArray[this.e.CurCol];
          if (this.e.HtmlMode && (this.e.TerFont[pPrevFont].style & 64 /*0x40*/) != 0 && !this.e.ShowHiddenText)
          {
            int index2 = this.e.CurCol + 1;
            while (index2 < this.e.text[this.e.CurLine].len && (this.e.TerFont[(int) numArray[index2]].style & 16448) != 0)
              ++index2;
            if (index2 < this.e.text[this.e.CurLine].len)
              pPrevFont = (int) numArray[index2];
          }
          else if ((this.e.TerFlags5 & 2048 /*0x0800*/) != 0 && this.edit.HiddenText(pPrevFont))
            pPrevFont = 0;
        }
        else
          pPrevFont = (int) numArray[this.e.CurCol - 1];
        this.CloseCfmt(this.e.CurLine);
      }
    }
label_63:
    int fieldId = this.e.TerFont[pPrevFont].FieldId;
    if (fieldId > 0 && fieldId != 2 && this.e.HilightType != 2 && (this.e.TerFont[this.GetCurCfmt(this.e.CurLine, this.e.CurCol)].FieldId == 0 || this.e.CurLine == 0 && this.e.CurCol == 0))
    {
      pPrevFont = this.GetTextFont(pPrevFont);
      if (fieldId == 14)
      {
        if (this.IsSameColor(this.e.TerFont[pPrevFont].TextColor, this.e.LinkColor))
        {
          Color textColor = this.e.TerFont[this.GetCurCfmt(this.e.CurLine, this.e.CurCol)].TextColor;
          if (this.e.CurLine == 0 && this.e.CurCol == 0)
            textColor = this.e.TerFont[0].TextColor;
          pPrevFont = (int) this.GetNewColor((ushort) pPrevFont, this.ToColorRef(textColor), 0, "", this.e.CurLine, this.e.CurCol);
        }
        if (this.e.LinkStyle != 0 && (this.e.TerFont[pPrevFont].style & this.e.LinkStyle) == this.e.LinkStyle)
          pPrevFont = (int) this.GetNewStyle((ushort) pPrevFont, this.e.LinkStyle, 0, "", this.e.CurLine, this.e.CurCol);
      }
    }
    int CurFont1 = this.e.HilightType != 0 ? pPrevFont : this.GetCurCfmt(this.e.CurLine, this.e.CurCol);
    if (this.e.TerFont[CurFont1].FieldId == 2 && !this.e.TerArg.ReadOnly && (this.e.TerFont[this.GetPrevCfmt(this.e.CurLine, this.e.CurCol)].FieldId != 2 || this.e.CurLine == 0 && this.e.CurCol == 0))
      pPrevFont = this.SetFontFieldId(CurFont1, 0, (string) null);
    else if (this.e.TerFont[pPrevFont].FieldId == 2 && this.e.TerFont[CurFont1].FieldId != 2)
    {
      if (!this.e.TerArg.ReadOnly)
        pPrevFont = CurFont1;
    }
    else if (this.e.TerFont[pPrevFont].FieldId != 2 && this.e.TerFont[CurFont1].FieldId == 2)
    {
      if (this.e.TerArg.ReadOnly)
        pPrevFont = CurFont1;
    }
    else if (!this.IsValidInputFont(ref pPrevFont, CurFont1, this.e.CurLine, this.e.CurCol))
    {
      if (this.e.TerFont[CurFont1].FieldId > 0)
      {
        pPrevFont = CurFont1;
      }
      else
      {
        int CurFont2 = pPrevFont;
        pPrevFont = 0;
        int pCol = this.e.CurCol - 1;
        int curLine = this.e.CurLine;
        bool flag2 = false;
        while (pCol > 0 && this.PrevTextPos(ref curLine, ref pCol))
        {
          int curCfmt = this.GetCurCfmt(curLine, pCol);
          if (this.IsValidInputFont(ref curCfmt, CurFont2, curLine, pCol + 1))
          {
            pPrevFont = curCfmt;
            flag2 = true;
            break;
          }
          CurFont2 = curCfmt;
        }
        if (!flag2)
        {
          ushort[] numArray = this.OpenCfmt(this.e.CurLine);
          for (int index3 = 0; index3 < this.e.text[this.e.CurLine].len; ++index3)
          {
            int index4 = (int) numArray[index3];
            if ((this.e.TerFont[index4].style & 40128) == 0 && this.e.TerFont[index4].FieldId == 0)
            {
              pPrevFont = index4;
              break;
            }
          }
          this.CloseCfmt(this.e.CurLine);
        }
      }
    }
    if ((this.e.TerFont[pPrevFont].style & 128 /*0x80*/) != 0)
      pPrevFont = 0;
    if (this.e.CurCol == 0 && (this.e.TerFont[pPrevFont].style & 512 /*0x0200*/) != 0)
    {
      bool flag3 = false;
      if (this.e.CurLine == 0)
        flag3 = true;
      else if ((this.e.TerFont[this.GetPrevCfmt(this.e.CurLine, this.e.CurCol)].style & 512 /*0x0200*/) == 0)
        flag3 = true;
      if (flag3)
      {
        ushort[] numArray = this.OpenCfmt(this.e.CurLine);
        int len = this.e.text[this.e.CurLine].len;
        for (int index = 0; index < len; ++index)
        {
          if ((this.e.TerFont[(int) numArray[index]].style & 512 /*0x0200*/) == 0)
          {
            pPrevFont = this.SetFontStyle(pPrevFont, 512 /*0x0200*/, false);
            break;
          }
        }
        this.CloseCfmt(this.e.CurLine);
      }
    }
    int prevCfmt = this.GetPrevCfmt(this.e.CurLine, this.e.CurCol);
    if (this.e.HilightType == 0)
    {
      if (this.e.TerFont[prevCfmt].FieldId == 14 && this.e.TerFont[pPrevFont].FieldId != 14)
      {
        int curCfmt = this.GetCurCfmt(this.e.CurLine, this.e.CurCol);
        pPrevFont = this.SetFontStyle(pPrevFont, this.e.TerFont[prevCfmt].style, false);
        pPrevFont = this.SetFontStyle(pPrevFont, this.e.TerFont[curCfmt].style, true);
        if (!this.IsSameColor(this.e.TerFont[curCfmt].TextColor, this.e.TerFont[pPrevFont].TextColor))
          pPrevFont = (int) this.GetNewColor((ushort) pPrevFont, this.ToColorRef(this.e.TerFont[curCfmt].TextColor), 0, "", this.e.CurLine, this.e.CurCol);
      }
      if (this.e.CurCol == 0 && (this.e.TerFont[pPrevFont].style & 512 /*0x0200*/) == 0 && (this.e.TerFont[pPrevFont].FieldId == 14 || this.IsDynField(this.e.TerFont[pPrevFont].FieldId)))
        pPrevFont = this.e.CurLine != 0 ? prevCfmt : 0;
    }
    return pPrevFont;
  }

  internal bool GetFontInfo(int FontId, out string TypeFace, out int PointSize, out int style)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int TwipsSize;
    int num = this.GetFontInfo2(FontId, out TypeFace, out TwipsSize, out style) ? 1 : 0;
    PointSize = (int) this.TwipsToPoints(TwipsSize);
    return num != 0;
  }

  internal bool GetFontInfo2(int FontId, out string TypeFace, out int TwipsSize, out int style)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    TypeFace = "";
    TwipsSize = 0;
    style = 0;
    if (FontId >= 0)
    {
      if (FontId >= this.e.TotalFonts || !this.e.TerFont[FontId].InUse)
        return false;
      TypeFace = this.e.TerFont[FontId].TypeFace;
      TwipsSize = this.e.TerFont[FontId].TwipsSize;
      style = this.e.TerFont[FontId].style;
    }
    else
    {
      int sid;
      if ((sid = this.ParamIdToSID(FontId)) < 0)
        return false;
      TypeFace = this.e.StyleId[sid].TypeFace;
      TwipsSize = this.e.StyleId[sid].TwipsSize;
      style = this.e.StyleId[sid].style;
    }
    return true;
  }

  private ushort GetNewBkColor(
    ushort OldFmt,
    int data1,
    int data2,
    string str,
    int line,
    int col)
  {
    Color color = this.ToColor(data1);
    int OldFont = (int) OldFmt;
    int newFont;
    return (this.e.TerFont[OldFont].style & 128 /*0x80*/) == 0 && (newFont = this.GetNewFont(this.e.TerGr, OldFont, this.e.TerFont[OldFont].TypeFace, this.e.TerFont[OldFont].TwipsSize, this.e.TerFont[OldFont].style, this.e.TerFont[OldFont].TextColor, color, this.e.TerFont[OldFont].UlineColor, this.e.TerFont[OldFont].FieldId, this.e.TerFont[OldFont].AuxId, this.e.TerFont[OldFont].Aux1Id, this.e.TerFont[OldFont].CharStyId, this.e.TerFont[OldFont].ParaStyId, this.e.TerFont[OldFont].expand, this.e.TerFont[OldFont].TempStyle, this.e.TerFont[OldFont].lang, this.e.TerFont[OldFont].FieldCode, this.e.TerFont[OldFont].offset, this.e.TerFont[OldFont].CharSet, this.e.TerFont[OldFont].flags, this.e.TerFont[OldFont].TextAngle)) >= 0 ? (ushort) newFont : OldFmt;
  }

  private ushort GetNewCharOffset(
    ushort OldFmt,
    int data1,
    int data2,
    string str,
    int line,
    int col)
  {
    int NewOffset = data1;
    int OldFont = (int) OldFmt;
    int newFont;
    return (this.e.TerFont[OldFont].style & 128 /*0x80*/) == 0 && (newFont = this.GetNewFont(this.e.TerGr, OldFont, this.e.TerFont[OldFont].TypeFace, this.e.TerFont[OldFont].TwipsSize, this.e.TerFont[OldFont].style, this.e.TerFont[OldFont].TextColor, this.e.TerFont[OldFont].TextBkColor, this.e.TerFont[OldFont].UlineColor, this.e.TerFont[OldFont].FieldId, this.e.TerFont[OldFont].AuxId, this.e.TerFont[OldFont].Aux1Id, this.e.TerFont[OldFont].CharStyId, this.e.TerFont[OldFont].ParaStyId, this.e.TerFont[OldFont].expand, this.e.TerFont[OldFont].TempStyle, this.e.TerFont[OldFont].lang, this.e.TerFont[OldFont].FieldCode, NewOffset, this.e.TerFont[OldFont].CharSet, this.e.TerFont[OldFont].flags, this.e.TerFont[OldFont].TextAngle)) >= 0 ? (ushort) newFont : OldFmt;
  }

  internal new ushort GetNewColor(
    ushort OldFmt,
    int data1,
    int data2,
    string str,
    int line,
    int col)
  {
    Color color = this.ToColor(data1);
    int OldFont = (int) OldFmt;
    int newFont;
    return (this.e.TerFont[OldFont].style & 128 /*0x80*/) == 0 && (newFont = this.GetNewFont(this.e.TerGr, OldFont, this.e.TerFont[OldFont].TypeFace, this.e.TerFont[OldFont].TwipsSize, this.e.TerFont[OldFont].style, color, this.e.TerFont[OldFont].TextBkColor, this.e.TerFont[OldFont].UlineColor, this.e.TerFont[OldFont].FieldId, this.e.TerFont[OldFont].AuxId, this.e.TerFont[OldFont].Aux1Id, this.e.TerFont[OldFont].CharStyId, this.e.TerFont[OldFont].ParaStyId, this.e.TerFont[OldFont].expand, this.e.TerFont[OldFont].TempStyle, this.e.TerFont[OldFont].lang, this.e.TerFont[OldFont].FieldCode, this.e.TerFont[OldFont].offset, this.e.TerFont[OldFont].CharSet, this.e.TerFont[OldFont].flags, this.e.TerFont[OldFont].TextAngle)) >= 0 ? (ushort) newFont : OldFmt;
  }

  private ushort GetNewExpand(
    ushort OldFmt,
    int data1,
    int data2,
    string str,
    int line,
    int col)
  {
    int NewExpand = data1;
    int OldFont = (int) OldFmt;
    int newFont;
    return (this.e.TerFont[OldFont].style & 128 /*0x80*/) == 0 && (newFont = this.GetNewFont(this.e.TerGr, OldFont, this.e.TerFont[OldFont].TypeFace, this.e.TerFont[OldFont].TwipsSize, this.e.TerFont[OldFont].style, this.e.TerFont[OldFont].TextColor, this.e.TerFont[OldFont].TextBkColor, this.e.TerFont[OldFont].UlineColor, this.e.TerFont[OldFont].FieldId, this.e.TerFont[OldFont].AuxId, this.e.TerFont[OldFont].Aux1Id, this.e.TerFont[OldFont].CharStyId, this.e.TerFont[OldFont].ParaStyId, NewExpand, this.e.TerFont[OldFont].TempStyle, this.e.TerFont[OldFont].lang, this.e.TerFont[OldFont].FieldCode, this.e.TerFont[OldFont].offset, this.e.TerFont[OldFont].CharSet, this.e.TerFont[OldFont].flags, this.e.TerFont[OldFont].TextAngle)) >= 0 ? (ushort) newFont : OldFmt;
  }

  internal new int GetNewFont(
    Graphics gr,
    int OldFont,
    string NewTypeFace,
    int NewTwipsSize,
    int NewStyle,
    Color NewTextColor,
    Color NewTextBkColor,
    Color NewUlineColor,
    int NewFieldId,
    int NewAuxId,
    int NewAux1Id,
    int NewCharStyId,
    int NewParaStyId,
    int NewExpand,
    int NewTempStyle,
    int NewLang,
    string NewFieldCode,
    int NewOffset,
    byte NewCharSet,
    int NewFlags,
    int NewTextAngle)
  {
    tc.StrFont font = new tc.StrFont();
    font.TypeFace = NewTypeFace;
    font.TwipsSize = NewTwipsSize;
    font.TextColor = NewTextColor;
    font.TextBkColor = NewTextBkColor;
    font.UlineColor = NewUlineColor;
    font.style = NewStyle;
    font.FieldId = NewFieldId;
    font.AuxId = NewAuxId;
    font.Aux1Id = NewAux1Id;
    font.CharStyId = NewCharStyId;
    font.ParaStyId = NewParaStyId;
    font.expand = NewExpand;
    font.TempStyle = NewTempStyle;
    font.lang = NewLang;
    font.CharSet = NewCharSet;
    font.offset = NewOffset;
    font.TextAngle = NewTextAngle;
    tc.ResetUintFlag(ref font.flags, 1536 /*0x0600*/);
    font.flags |= NewFlags;
    if (NewFieldCode != null)
      font.FieldCode = NewFieldCode;
    return this.GetNewFont2(gr, OldFont, font);
  }

  internal bool FontsIsEqual(tc.StrFont font1, tc.StrFont font2, bool ignoreAutoSpellFlag = false)
  {
    bool flag = (font1.style & 128 /*0x80*/) == 0 && font1.TypeFace == font2.TypeFace && font1.TwipsSize == font2.TwipsSize && font1.style == font2.style && font1.TextColor == font2.TextColor && font1.TextBkColor == font2.TextBkColor && font1.UlineColor == font2.UlineColor && font1.AuxId == font2.AuxId && font1.CharId == font2.CharId && font1.Aux1Id == font2.Aux1Id && font1.CharStyId == font2.CharStyId && font1.ParaStyId == font2.ParaStyId && font1.expand == font2.expand && font1.TempStyle == font2.TempStyle && font1.lang == font2.lang && ((int) font1.CharSet == (int) font2.CharSet || font2.CharSet == (byte) 1 && (font1.CharSet == (byte) 0 || font1.CharSet == (byte) 2)) && font1.InsRev == font2.InsRev && object.Equals((object) font1.InsTime, (object) font2.InsTime) && font1.DelRev == font2.DelRev && object.Equals((object) font1.DelTime, (object) font2.DelTime) && font1.offset == font2.offset && font1.TextAngle == font2.TextAngle && this.fld.IsSameFieldCode(font1.FieldCode, font2.FieldCode) && font1.FieldId == font2.FieldId;
    int flags1 = font1.flags;
    int flags2 = font2.flags;
    return !ignoreAutoSpellFlag ? flag && (flags1 & 1536 /*0x0600*/) == (flags2 & 1536 /*0x0600*/) : flag && (flags1 & 512 /*0x0200*/) == (flags2 & 512 /*0x0200*/);
  }

  internal new int GetNewFont2(Graphics gr, int OldFont, tc.StrFont font)
  {
    if ((this.e.TerFont[OldFont].style & 128 /*0x80*/) != 0)
      return OldFont;
    font.flags &= 1536 /*0x0600*/;
    if (this.e.MatchIds)
    {
      if ((this.e.TerFont[OldFont].style & 128 /*0x80*/) == 0 && this.e.TerFont[OldFont].TypeFace == font.TypeFace && this.e.TerFont[OldFont].TwipsSize == font.TwipsSize && this.e.TerFont[OldFont].style == font.style && this.e.TerFont[OldFont].TextColor == font.TextColor && this.e.TerFont[OldFont].TextBkColor == font.TextBkColor && this.e.TerFont[OldFont].UlineColor == font.UlineColor && this.e.TerFont[OldFont].AuxId == font.AuxId && this.e.TerFont[OldFont].CharId == font.CharId && this.e.TerFont[OldFont].Aux1Id == font.Aux1Id && this.e.TerFont[OldFont].CharStyId == font.CharStyId && this.e.TerFont[OldFont].ParaStyId == font.ParaStyId && this.e.TerFont[OldFont].expand == font.expand && this.e.TerFont[OldFont].TempStyle == font.TempStyle && this.e.TerFont[OldFont].lang == font.lang && ((int) this.e.TerFont[OldFont].CharSet == (int) font.CharSet || font.CharSet == (byte) 1 && (this.e.TerFont[OldFont].CharSet == (byte) 0 || this.e.TerFont[OldFont].CharSet == (byte) 2)) && this.e.TerFont[OldFont].InsRev == font.InsRev && object.Equals((object) this.e.TerFont[OldFont].InsTime, (object) font.InsTime) && this.e.TerFont[OldFont].DelRev == font.DelRev && object.Equals((object) this.e.TerFont[OldFont].DelTime, (object) font.DelTime) && this.e.TerFont[OldFont].offset == font.offset && this.e.TerFont[OldFont].TextAngle == font.TextAngle && (this.e.TerFont[OldFont].flags & 1536 /*0x0600*/) == font.flags && this.fld.IsSameFieldCode(this.e.TerFont[OldFont].FieldCode, font.FieldCode) && this.e.TerFont[OldFont].FieldId == font.FieldId)
        return OldFont;
      for (int newFont2 = 0; newFont2 < this.e.TotalFonts; ++newFont2)
      {
        if (this.e.TerFont[newFont2].InUse && (this.e.TerFont[newFont2].style & 128 /*0x80*/) == 0 && this.e.TerFont[newFont2].TypeFace == font.TypeFace && this.e.TerFont[newFont2].TwipsSize == font.TwipsSize && this.e.TerFont[newFont2].style == font.style && this.e.TerFont[newFont2].TextColor == font.TextColor && this.e.TerFont[newFont2].TextBkColor == font.TextBkColor && this.e.TerFont[newFont2].UlineColor == font.UlineColor && this.e.TerFont[newFont2].AuxId == font.AuxId && this.e.TerFont[newFont2].Aux1Id == font.Aux1Id && this.e.TerFont[newFont2].CharId == font.CharId && this.e.TerFont[newFont2].CharStyId == font.CharStyId && this.e.TerFont[newFont2].ParaStyId == font.ParaStyId && this.e.TerFont[newFont2].expand == font.expand && this.e.TerFont[newFont2].TempStyle == font.TempStyle && this.e.TerFont[newFont2].lang == font.lang)
        {
          bool flag = (int) this.e.TerFont[newFont2].CharSet == (int) font.CharSet;
          if (!flag && font.CharSet == (byte) 1 && this.e.TerFont[newFont2].CharSet == (byte) 2)
            flag = true;
          if (!flag && font.CharSet == (byte) 1 && this.e.TerFont[newFont2].CharSet == (byte) 0)
            flag = true;
          if (flag && this.e.TerFont[newFont2].InsRev == font.InsRev && object.Equals((object) this.e.TerFont[newFont2].InsTime, (object) font.InsTime) && this.e.TerFont[newFont2].DelRev == font.DelRev && object.Equals((object) this.e.TerFont[newFont2].DelTime, (object) font.DelTime) && this.e.TerFont[newFont2].offset == font.offset && this.e.TerFont[newFont2].TextAngle == font.TextAngle && (this.e.TerFont[newFont2].flags & 1536 /*0x0600*/) == font.flags && this.fld.IsSameFieldCode(this.e.TerFont[newFont2].FieldCode, font.FieldCode) && this.e.TerFont[newFont2].FieldId == font.FieldId)
            return newFont2;
        }
      }
    }
    else
      this.e.MatchIds = true;
    int openSlot;
    if ((openSlot = this.FindOpenSlot()) == -1)
      return OldFont;
    this.e.TerFont[openSlot].InUse = true;
    this.e.TerFont[openSlot].TypeFace = font.TypeFace;
    this.e.TerFont[openSlot].TwipsSize = font.TwipsSize;
    this.e.TerFont[openSlot].TextColor = font.TextColor;
    this.e.TerFont[openSlot].TextBkColor = font.TextBkColor;
    this.e.TerFont[openSlot].UlineColor = font.UlineColor;
    this.e.TerFont[openSlot].style = font.style;
    this.e.TerFont[openSlot].FieldId = font.FieldId;
    this.e.TerFont[openSlot].AuxId = font.AuxId;
    this.e.TerFont[openSlot].Aux1Id = font.Aux1Id;
    this.e.TerFont[openSlot].CharId = font.CharId;
    this.e.TerFont[openSlot].CharStyId = font.CharStyId;
    this.e.TerFont[openSlot].ParaStyId = font.ParaStyId;
    this.e.TerFont[openSlot].expand = font.expand;
    this.e.TerFont[openSlot].TempStyle = font.TempStyle;
    this.e.TerFont[openSlot].lang = font.lang;
    this.e.TerFont[openSlot].CharSet = font.CharSet;
    this.e.TerFont[openSlot].InsRev = font.InsRev;
    this.e.TerFont[openSlot].InsTime = font.InsTime == null ? (tc.ClsDateTime) null : font.InsTime.Copy();
    this.e.TerFont[openSlot].DelRev = font.DelRev;
    this.e.TerFont[openSlot].DelTime = font.DelTime == null ? (tc.ClsDateTime) null : font.DelTime.Copy();
    this.e.TerFont[openSlot].offset = font.offset;
    this.e.TerFont[openSlot].TextAngle = font.TextAngle;
    tc.ResetUintFlag(ref this.e.TerFont[openSlot].flags, 1536 /*0x0600*/);
    this.e.TerFont[openSlot].flags |= font.flags;
    if (font.FieldCode != null)
      this.e.TerFont[openSlot].FieldCode = font.FieldCode;
    if (!this.CreateOneFont(gr, openSlot, true))
      this.PrintError(42, (string) null);
    return openSlot;
  }

  private ushort GetNewLang(ushort OldFmt, int data1, int data2, string str, int line, int col)
  {
    int NewLang = data1;
    int OldFont = (int) OldFmt;
    int newFont;
    return (this.e.TerFont[OldFont].style & 128 /*0x80*/) == 0 && (newFont = this.GetNewFont(this.e.TerGr, OldFont, this.e.TerFont[OldFont].TypeFace, this.e.TerFont[OldFont].TwipsSize, this.e.TerFont[OldFont].style, this.e.TerFont[OldFont].TextColor, this.e.TerFont[OldFont].TextBkColor, this.e.TerFont[OldFont].UlineColor, this.e.TerFont[OldFont].FieldId, this.e.TerFont[OldFont].AuxId, this.e.TerFont[OldFont].Aux1Id, this.e.TerFont[OldFont].CharStyId, this.e.TerFont[OldFont].ParaStyId, this.e.TerFont[OldFont].expand, this.e.TerFont[OldFont].TempStyle, NewLang, this.e.TerFont[OldFont].FieldCode, this.e.TerFont[OldFont].offset, this.e.TerFont[OldFont].CharSet, this.e.TerFont[OldFont].flags, this.e.TerFont[OldFont].TextAngle)) >= 0 ? (ushort) newFont : OldFmt;
  }

  internal new ushort GetNewPointSize(ushort OldFmt, int data1, int data2, int line, int col)
  {
    int NewTwipsSize = data1;
    int OldFont = (int) OldFmt;
    int style = this.e.TerFont[OldFont].style;
    int newFont;
    return (newFont = this.GetNewFont(this.e.TerGr, OldFont, this.e.TerFont[OldFont].TypeFace, NewTwipsSize, style, this.e.TerFont[OldFont].TextColor, this.e.TerFont[OldFont].TextBkColor, this.e.TerFont[OldFont].UlineColor, this.e.TerFont[OldFont].FieldId, this.e.TerFont[OldFont].AuxId, this.e.TerFont[OldFont].Aux1Id, this.e.TerFont[OldFont].CharStyId, this.e.TerFont[OldFont].ParaStyId, this.e.TerFont[OldFont].expand, this.e.TerFont[OldFont].TempStyle, this.e.TerFont[OldFont].lang, this.e.TerFont[OldFont].FieldCode, this.e.TerFont[OldFont].offset, this.e.TerFont[OldFont].CharSet, this.e.TerFont[OldFont].flags, this.e.TerFont[OldFont].TextAngle)) >= 0 ? (ushort) newFont : OldFmt;
  }

  private ushort GetNewPointSize(
    ushort OldFmt,
    int data1,
    int data2,
    string str1,
    int line,
    int col)
  {
    int NewTwipsSize = data1;
    int OldFont = (int) OldFmt;
    int style = this.e.TerFont[OldFont].style;
    int newFont;
    return (newFont = this.GetNewFont(this.e.TerGr, OldFont, this.e.TerFont[OldFont].TypeFace, NewTwipsSize, style, this.e.TerFont[OldFont].TextColor, this.e.TerFont[OldFont].TextBkColor, this.e.TerFont[OldFont].UlineColor, this.e.TerFont[OldFont].FieldId, this.e.TerFont[OldFont].AuxId, this.e.TerFont[OldFont].Aux1Id, this.e.TerFont[OldFont].CharStyId, this.e.TerFont[OldFont].ParaStyId, this.e.TerFont[OldFont].expand, this.e.TerFont[OldFont].TempStyle, this.e.TerFont[OldFont].lang, this.e.TerFont[OldFont].FieldCode, this.e.TerFont[OldFont].offset, this.e.TerFont[OldFont].CharSet, this.e.TerFont[OldFont].flags, this.e.TerFont[OldFont].TextAngle)) >= 0 ? (ushort) newFont : OldFmt;
  }

  internal new ushort GetNewStyle(
    ushort OldFmt,
    int data1,
    int data2,
    string str1,
    int line,
    int col)
  {
    int num1 = data1 & -129;
    int num2 = data2 != 0 ? 1 : 0;
    int index = (int) OldFmt;
    int style = this.e.TerFont[index].style;
    int flags = num2 == 0 ? style & ~num1 : style | num1;
    if (num2 != 0 && (num1 & 16 /*0x10*/) != 0)
      flags &= -33;
    if (num2 != 0 && (num1 & 32 /*0x20*/) != 0)
      flags &= -17;
    if (this.e.TerFont[(int) OldFmt].FieldId == 6 && col >= 0)
    {
      switch (this.GetCurChar(line, col))
      {
        case '{':
        case '}':
          if ((this.e.TerFont[index].style & 512 /*0x0200*/) != 0)
            flags |= 512 /*0x0200*/;
          if ((this.e.TerFont[index].style & 512 /*0x0200*/) == 0)
          {
            tc.ResetUintFlag(ref flags, 512 /*0x0200*/);
            break;
          }
          break;
      }
    }
    if ((flags & 128 /*0x80*/) != 0)
    {
      this.e.TerFont[index].style = flags;
      if ((num1 & 64 /*0x40*/) != 0)
      {
        this.SetPictSize(index, this.TwipsToScrY(this.e.TerFont[index].PictHeight), this.TwipsToScrX(this.e.TerFont[index].PictWidth), true);
        this.XlateSizeForPrt(index);
      }
      return OldFmt;
    }
    int newFont;
    return (newFont = this.GetNewFont(this.e.TerGr, index, this.e.TerFont[index].TypeFace, this.e.TerFont[index].TwipsSize, flags, this.e.TerFont[index].TextColor, this.e.TerFont[index].TextBkColor, this.e.TerFont[index].UlineColor, this.e.TerFont[index].FieldId, this.e.TerFont[index].AuxId, this.e.TerFont[index].Aux1Id, this.e.TerFont[index].CharStyId, this.e.TerFont[index].ParaStyId, this.e.TerFont[index].expand, this.e.TerFont[index].TempStyle, this.e.TerFont[index].lang, this.e.TerFont[index].FieldCode, this.e.TerFont[index].offset, this.e.TerFont[index].CharSet, this.e.TerFont[index].flags, this.e.TerFont[index].TextAngle)) >= 0 ? (ushort) newFont : OldFmt;
  }

  internal new ushort GetNewTempStyle(ushort OldFmt, int data1, int data2, int line, int col)
  {
    int flag1 = data1;
    bool flag2 = data2 != 0;
    int OldFont = (int) OldFmt;
    int style = this.e.TerFont[OldFont].style;
    int tempStyle = this.e.TerFont[OldFont].TempStyle;
    if ((style & 128 /*0x80*/) == 0)
    {
      if (flag2)
        tempStyle |= flag1;
      else
        tc.ResetUintFlag(ref tempStyle, flag1);
      int newFont;
      if ((newFont = this.GetNewFont(this.e.TerGr, OldFont, this.e.TerFont[OldFont].TypeFace, this.e.TerFont[OldFont].TwipsSize, this.e.TerFont[OldFont].style, this.e.TerFont[OldFont].TextColor, this.e.TerFont[OldFont].TextBkColor, this.e.TerFont[OldFont].UlineColor, this.e.TerFont[OldFont].FieldId, this.e.TerFont[OldFont].AuxId, this.e.TerFont[OldFont].Aux1Id, this.e.TerFont[OldFont].CharStyId, this.e.TerFont[OldFont].ParaStyId, this.e.TerFont[OldFont].expand, tempStyle, this.e.TerFont[OldFont].lang, this.e.TerFont[OldFont].FieldCode, this.e.TerFont[OldFont].offset, this.e.TerFont[OldFont].CharSet, this.e.TerFont[OldFont].flags, this.e.TerFont[OldFont].TextAngle)) >= 0)
        return (ushort) newFont;
    }
    return OldFmt;
  }

  private ushort GetNewTypeFace(
    ushort OldFmt,
    int data1,
    int data2,
    string str1,
    int line,
    int col)
  {
    string NewTypeFace = str1;
    int OldFont = (int) OldFmt;
    int style = this.e.TerFont[OldFont].style;
    byte NewCharSet = this.e.TerFont[OldFont].CharSet;
    if (NewCharSet == (byte) 2)
      NewCharSet = (byte) 1;
    int newFont;
    return (newFont = this.GetNewFont(this.e.TerGr, OldFont, NewTypeFace, this.e.TerFont[OldFont].TwipsSize, style, this.e.TerFont[OldFont].TextColor, this.e.TerFont[OldFont].TextBkColor, this.e.TerFont[OldFont].UlineColor, this.e.TerFont[OldFont].FieldId, this.e.TerFont[OldFont].AuxId, this.e.TerFont[OldFont].Aux1Id, this.e.TerFont[OldFont].CharStyId, this.e.TerFont[OldFont].ParaStyId, this.e.TerFont[OldFont].expand, this.e.TerFont[OldFont].TempStyle, this.e.TerFont[OldFont].lang, this.e.TerFont[OldFont].FieldCode, this.e.TerFont[OldFont].offset, NewCharSet, this.e.TerFont[OldFont].flags, this.e.TerFont[OldFont].TextAngle)) >= 0 ? (ushort) newFont : OldFmt;
  }

  private ushort GetNewUlineColor(
    ushort OldFmt,
    int data1,
    int data2,
    string str,
    int line,
    int col)
  {
    Color color = this.ToColor(data1);
    int OldFont = (int) OldFmt;
    int newFont;
    return (this.e.TerFont[OldFont].style & 128 /*0x80*/) == 0 && (newFont = this.GetNewFont(this.e.TerGr, OldFont, this.e.TerFont[OldFont].TypeFace, this.e.TerFont[OldFont].TwipsSize, this.e.TerFont[OldFont].style, this.e.TerFont[OldFont].TextColor, this.e.TerFont[OldFont].TextBkColor, color, this.e.TerFont[OldFont].FieldId, this.e.TerFont[OldFont].AuxId, this.e.TerFont[OldFont].Aux1Id, this.e.TerFont[OldFont].CharStyId, this.e.TerFont[OldFont].ParaStyId, this.e.TerFont[OldFont].expand, this.e.TerFont[OldFont].TempStyle, this.e.TerFont[OldFont].lang, this.e.TerFont[OldFont].FieldCode, this.e.TerFont[OldFont].offset, this.e.TerFont[OldFont].CharSet, this.e.TerFont[OldFont].flags, this.e.TerFont[OldFont].TextAngle)) >= 0 ? (ushort) newFont : OldFmt;
  }

  internal new int GetNextCfmt(int line, int col)
  {
    if (line >= this.e.TotalLines)
      return 0;
    int len = this.e.text[line].len;
    if (col < 0 || col >= len)
      return 0;
    ++col;
    if (col < len)
      return this.GetCurCfmt(line, col);
    if (line + 1 == this.e.TotalLines)
      return 0;
    while (line + 1 < this.e.TotalLines)
    {
      ++line;
      if (this.e.text[line].len > 0)
        break;
    }
    return this.GetCurCfmt(line, 0);
  }

  internal new int GetPrevCfmt(int line, int col)
  {
    if (line >= this.e.TotalLines)
      return 0;
    int len = this.e.text[line].len;
    if (col < 0 || col > len)
      return 0;
    --col;
    if (col < 0)
    {
      if (line == 0)
        return 0;
      while (line > 0)
      {
        --line;
        col = this.e.text[line].len - 1;
        if (col >= 0)
          break;
      }
      if (col < 0)
        return 0;
    }
    return this.GetCurCfmt(line, col);
  }

  internal new int GetTag(
    int line,
    int col,
    int type,
    out string name,
    out string AuxText,
    out int AuxInt)
  {
    return this.GetTag(line, col, type, out name, out AuxText, out AuxInt, out tc.SkipObject);
  }

  internal new int GetTag(
    int line,
    int col,
    int type,
    out string name,
    out string AuxText,
    out int AuxInt,
    out object obj)
  {
    name = "";
    AuxText = "";
    AuxInt = 0;
    obj = (object) null;
    if (line < 0)
    {
      line = this.e.CurLine;
      col = this.e.CurCol;
    }
    int tag;
    if (col >= 0)
    {
      if (line < 0 || line >= this.e.TotalLines || col >= this.e.text[line].len)
        return 0;
      tag = (int) this.OpenCtid(line)[col];
      this.CloseCtid(line);
    }
    else
      tag = line;
    if (tag == 0)
      return tag;
    while (this.e.CharTag[tag].type != type)
    {
      tag = this.e.CharTag[tag].next;
      if (tag == 0)
        return 0;
    }
    if (this.e.CharTag[tag].name != null)
      name = this.e.CharTag[tag].name;
    if (this.e.CharTag[tag].AuxText != null)
      AuxText = this.e.CharTag[tag].AuxText;
    AuxInt = this.e.CharTag[tag].AuxInt;
    obj = this.e.CharTag[tag].obj;
    return tag;
  }

  internal new int GetTagSlot()
  {
    for (int tagSlot = 1; tagSlot < this.e.TotalCharTags; ++tagSlot)
    {
      if (!this.e.CharTag[tagSlot].InUse)
      {
        this.e.CharTag[tagSlot] = new tc.StrCharTag();
        return tagSlot;
      }
    }
    if (this.e.TotalCharTags >= this.e.MaxCharTags)
    {
      int count = this.e.MaxCharTags + this.e.MaxCharTags / 2;
      this.e.CharTag = this.ReAlloc(this.e.CharTag, count);
      this.e.MaxCharTags = count;
    }
    this.e.CharTag[this.e.TotalCharTags] = new tc.StrCharTag();
    ++this.e.TotalCharTags;
    return this.e.TotalCharTags - 1;
  }

  internal new int GetTextFont(int CurFont)
  {
    if ((this.e.TerFont[CurFont].style & 128 /*0x80*/) != 0)
      return 0;
    if (this.e.TerFont[CurFont].FieldId > 0 || this.e.TerFont[CurFont].FieldCode != null)
      CurFont = (int) this.GetNewFieldId((ushort) CurFont, 0, (string) null, 0, 0);
    return CurFont;
  }

  internal new bool InitTerObject(int idx)
  {
    this.e.TerFont[idx] = new tc.StrFont();
    this.e.PrtFont[idx] = new tc.StrPrtFont();
    if (this.e.TerFont[idx].CharWidth == null)
    {
      this.e.TerFont[idx].CharWidth = new int[256 /*0x0100*/];
      this.e.PrtFont[idx].CharWidth = new int[256 /*0x0100*/];
    }
    this.e.TerFont[idx].TextBkColor = tc.CLR_WHITE;
    this.e.TerFont[idx].TextColor = tc.CLR_AUTO;
    this.e.TerFont[idx].UlineColor = tc.CLR_AUTO;
    this.e.TerFont[idx].FontFamily = (byte) 0;
    this.e.TerFont[idx].CharSet = (byte) 1;
    this.e.TerFont[idx].ObjectType = 0;
    this.e.TerFont[idx].PictAlign = 0;
    this.e.TerFont[idx].GlbFontId = -1;
    this.e.TerFont[idx].CharStyId = 1;
    this.e.TerFont[idx].hFont = IntPtr.Zero;
    this.e.TerFont[idx].hMeta = IntPtr.Zero;
    this.e.PrtFont[idx].GlbFontId = -1;
    this.e.PrtFont[idx].hFont = IntPtr.Zero;
    return true;
  }

  internal new bool InsertBookmark()
  {
    if (!this.CallDialogBox((Form) new terdlg_bookmark(this.e)))
      return false;
    string tempString = this.e.TempString;
    if (this.e.DlgInt1 == 0)
      this.TerInsertBookmark(-1, 0, tempString);
    else if (this.e.DlgInt1 == 1)
      this.TerDeleteBookmark(tempString);
    else if (this.e.DlgInt1 == 2)
      this.TerPosBookmark(tempString, true);
    else if (this.e.DlgInt1 == 956)
      this.e.TerInsertPageRef(tempString, true, false, true);
    return true;
  }

  internal new bool IsEnglishChar(char[] ptr, int col, int len)
  {
    char ch1 = ptr[col];
    if (ch1 >= 'a' && ch1 <= 'z' || ch1 >= 'A' && ch1 <= 'Z')
      return true;
    if (ch1 >= '0' && ch1 <= '9')
      return this.e.DefLang == 1033;
    if (ch1 == ' ')
    {
      int index1 = col - 1;
      while (index1 >= 0 && ptr[index1] == ' ')
        --index1;
      if (index1 >= 0)
      {
        char ch2 = ptr[index1];
        if (ch2 >= 'a' && ch2 <= 'z' || ch2 >= 'A' && ch2 <= 'Z')
          return true;
        if (ch2 >= '0' && ch2 <= '9')
          return this.e.DefLang == 1033;
      }
      int index2 = col + 1;
      while (index2 < len && ptr[index2] == ' ')
        ++index2;
      if (index2 < len)
      {
        char ch3 = ptr[index2];
        if (ch3 >= 'a' && ch3 <= 'z' || ch3 >= 'A' && ch3 <= 'Z')
          return true;
        if (ch3 >= '0' && ch3 <= '9')
          return this.e.DefLang == 1033;
      }
    }
    return false;
  }

  internal new bool IsMbcsCharSet(int CharSet, out int pCodePage)
  {
    pCodePage = 0;
    COp.CHARSETINFO lpCs;
    if (!this.TranslateCharsetInfo(CharSet, out lpCs, 1))
      return false;
    pCodePage = lpCs.ciACP;
    COp.CPINFO lpCp = new COp.CPINFO();
    this.GetCPInfo(lpCs.ciACP, out lpCp);
    return lpCp.MaxCharSize > 1;
  }

  internal new bool IsValidBookmark(string name, bool exists)
  {
    int length = name.Length;
    if (length != 0)
    {
      for (int index = 0; index < length; ++index)
      {
        if (name[index] == ' ')
          return false;
      }
      if (!exists)
        return true;
      for (int index = 1; index < this.e.TotalCharTags; ++index)
      {
        if (this.e.CharTag[index].InUse && this.e.CharTag[index].type == 1 && this.e.CharTag[index].name == name)
          return true;
      }
    }
    return false;
  }

  internal new bool IsValidInputFont(ref int pPrevFont, int CurFont, int LineNo, int col)
  {
    int CurCfmt = pPrevFont;
    if (col >= this.e.text[LineNo].len)
      this.FixPos(ref LineNo, ref col);
    int style1;
    int fieldId1;
    int style2;
    int fieldId2;
    while (true)
    {
      style1 = this.e.TerFont[CurCfmt].style;
      fieldId1 = this.e.TerFont[CurCfmt].FieldId;
      style2 = this.e.TerFont[CurFont].style;
      fieldId2 = this.e.TerFont[CurFont].FieldId;
      if ((style1 & 128 /*0x80*/) != 0 && (style2 & 128 /*0x80*/) == 0)
        CurCfmt = CurFont;
      else if ((style1 & 512 /*0x0200*/) != 0 && (style2 & 512 /*0x0200*/) == 0 && this.e.HilightType != 2)
        CurCfmt = CurFont;
      else if ((style1 & 512 /*0x0200*/) == 0 || (style2 & 512 /*0x0200*/) == 0)
      {
        if (fieldId1 > 0 && fieldId2 == 0 && (fieldId1 != 7 || (this.e.TerFlags3 & 16384 /*0x4000*/) == 0 || this.e.ShowFieldNames))
          CurCfmt = CurFont;
        else
          goto label_11;
      }
      else
        break;
    }
    if (this.e.ShowFieldNames && fieldId1 == 6 && fieldId2 == 6 && this.GetCurChar(LineNo, col) == '{')
    {
      CurCfmt = 0;
      goto label_27;
    }
    goto label_27;
label_11:
    if ((style1 & 64 /*0x40*/) != 0)
    {
      if (this.link.IsHypertext(CurFont))
      {
        CurCfmt = CurFont;
        goto label_27;
      }
      if (!this.e.ShowHiddenText)
        return false;
    }
    if (this.e.ShowHyperlinkCursor && this.link.IsHypertext(CurCfmt) && !this.link.IsHypertext(CurFont))
    {
      CurCfmt = CurFont;
    }
    else
    {
      if ((style1 & 2048 /*0x0800*/) != 0 && !this.e.EditFootnoteText || (style1 & 32768 /*0x8000*/) != 0 && !this.e.EditEndnoteText || (style1 & 5120) != 0 || this.e.TerFont[CurCfmt].FieldId > 0 && fieldId1 != 6 && fieldId1 != 7 && fieldId1 != 14 && fieldId1 != 9 || !this.e.ShowFieldNames && fieldId1 == 6)
        return false;
      if (this.e.ShowFieldNames && fieldId1 == 7)
      {
        if (fieldId2 != 6)
          return false;
        CurCfmt = 0;
      }
      if (this.e.ShowFieldNames && fieldId2 == 6 && fieldId1 == 6)
      {
        char ch = this.e.text[LineNo].txt[col];
        if ((style2 & 512 /*0x0200*/) != 0 && ch == '{')
          CurCfmt = 0;
      }
    }
label_27:
    pPrevFont = CurCfmt;
    return true;
  }

  internal new bool JumpToPageRefBookmark(bool repaint)
  {
    int curCfmt = this.GetCurCfmt(this.e.CurLine, this.e.CurCol);
    if (this.e.TerFont[curCfmt].FieldId != 16 /*0x10*/ || this.e.TerFont[curCfmt].FieldCode == null)
      return false;
    int length = this.e.TerFont[curCfmt].FieldCode.Length;
    int num = 0;
    while (num < length && this.e.TerFont[curCfmt].FieldCode[num] != ' ')
      ++num;
    return this.PosTag(0, this.e.TerFont[curCfmt].FieldCode.Substring(0, num), 1, 3, repaint);
  }

  internal new int LwrCharWidth(int font, bool screen, char chr)
  {
    if (!this.e.ShowParaMark && (this.e.TerFont[font].style & 128 /*0x80*/) != 0 && this.e.TerFont[font].FrameType != 0)
      return 1;
    return screen ? this.e.TerFont[font].CharWidth[(int) (byte) chr] : this.e.PrtFont[font].CharWidth[(int) (byte) chr];
  }

  internal new ushort[] OpenCfmt(int line)
  {
    if (this.e.text[line].fmt == null)
      this.ExpandCfmt(line);
    return this.e.text[line].fmt;
  }

  internal new bool OpenCharInfo(int line, out ushort[] fmt, out ushort[] cmi)
  {
    if (this.e.text[line].fmt == null)
      this.ExpandCfmt(line);
    if (this.e.text[line].tag == null)
      this.ExpandCtid(line);
    fmt = this.e.text[line].fmt;
    cmi = this.e.text[line].tag;
    return true;
  }

  internal new ushort[] OpenCtid(int line)
  {
    this.ExpandCtid(line);
    return this.e.text[line].tag;
  }

  internal new bool PosTag(int TagId, string name, int type, int scope, bool repaint)
  {
    bool flag = false;
    int index1 = 0;
    if (scope == 3)
    {
      if (this.PosTagQuick(TagId, name, type, repaint))
        return true;
      scope = 0;
    }
    if (name != null && name.Length > 0)
      flag = true;
    int num1;
    int num2;
    if (scope == 0)
    {
      num1 = 0;
      num2 = 0;
      scope = 1;
    }
    else
    {
      num1 = this.e.CurLine;
      num2 = this.e.CurCol;
    }
    int index2;
    if (scope == 1)
    {
      for (index2 = num1; index2 < this.e.TotalLines; ++index2)
      {
        if (this.e.text[index2].tag != null)
        {
          ushort[] tag = this.e.text[index2].tag;
          int len = this.e.text[index2].len;
          for (index1 = index2 == num1 ? num2 : 0; index1 < len; ++index1)
          {
            int next;
            for (next = (int) tag[index1]; next != 0; next = this.e.CharTag[next].next)
            {
              if (flag)
              {
                if ((type == -1 || this.e.CharTag[next].type == type) && this.e.CharTag[next].name != null && this.e.CharTag[next].name == name)
                  break;
              }
              else if (next == TagId)
                break;
            }
            if (next != 0)
              break;
          }
          if (index1 < len)
            break;
        }
      }
      if (index2 == this.e.TotalLines)
        return false;
    }
    else
    {
      for (index2 = num1; index2 >= 0; --index2)
      {
        if (this.e.text[index2].tag != null)
        {
          ushort[] tag = this.e.text[index2].tag;
          int len = this.e.text[index2].len;
          for (index1 = index2 == num1 ? num2 : len - 1; index1 >= 0; --index1)
          {
            int next;
            for (next = (int) tag[index1]; next != 0; next = this.e.CharTag[next].next)
            {
              if (flag)
              {
                if ((type == -1 || this.e.CharTag[next].type == type) && this.e.CharTag[next].name != null && this.e.CharTag[next].name == name)
                  break;
              }
              else if (next == TagId)
                break;
            }
            if (next != 0)
              break;
          }
          if (index1 >= 0)
            break;
        }
      }
      if (index2 < 0)
        return false;
    }
    this.e.CurCol = index1;
    if (repaint)
      this.TerPosLine(index2 + 1);
    else
      this.e.CurLine = index2;
    return true;
  }

  internal int PosTagLine(int TagId, int line)
  {
    if (line >= 0 && line < this.e.TotalLines && this.e.text[line].tag != null)
    {
      ushort[] tag = this.e.text[line].tag;
      int len = this.e.text[line].len;
      for (int index = 0; index < len; ++index)
      {
        for (int next = (int) tag[index]; next != 0; next = this.e.CharTag[next].next)
        {
          if (next == TagId)
            return index;
        }
      }
    }
    return -1;
  }

  internal bool PosTagQuick(int TagId, string name, int type, bool repaint)
  {
    if (TagId == -1 && name != null && name.Length > 0)
    {
      int index = 0;
      while (index < this.e.TotalCharTags && (this.e.CharTag[index].type != type || this.e.CharTag[index].name == null || !(this.e.CharTag[index].name == name)))
        ++index;
      if (index == this.e.TotalCharTags)
        return false;
      TagId = index;
    }
    if (TagId < 0 || TagId >= this.e.TotalCharTags)
      return false;
    int line1 = this.e.CharTag[TagId].line;
    if (line1 < 0 || line1 >= this.e.TotalLines)
      return false;
    int line2 = line1;
    int num;
    if ((num = this.PosTagLine(TagId, line2)) < 0)
    {
      line2 = line1 - 1;
      if ((num = this.PosTagLine(TagId, line2)) < 0)
      {
        line2 = line1 + 1;
        if ((num = this.PosTagLine(TagId, line2)) < 0)
        {
          line2 = line1 - 2;
          if ((num = this.PosTagLine(TagId, line2)) < 0)
          {
            line2 = line1 + 2;
            if ((num = this.PosTagLine(TagId, line2)) < 0)
              return false;
          }
        }
      }
    }
    this.e.CurCol = num;
    if (repaint)
      this.TerPosLine(line2 + 1);
    else
      this.e.CurLine = line2;
    return true;
  }

  internal new bool RecreateFonts(Graphics gr)
  {
    if (tc.DebugMode)
      this.misc.dm(nameof (RecreateFonts));
    for (int NewFont = 0; NewFont < this.e.TotalFonts; ++NewFont)
    {
      if (this.e.TerFont[NewFont].InUse && (this.e.TerFont[NewFont].style & 128 /*0x80*/) == 0 && !this.CreateOneFont(gr, NewFont, true))
        this.PrintError(42, $"#: {NewFont}, Font: {this.e.TerFont[NewFont].TypeFace}, Twips Size: {this.e.TerFont[NewFont].TwipsSize}");
    }
    for (int index = 0; index < this.e.TotalFonts; ++index)
    {
      if (this.e.TerFont[index].InUse && (this.e.TerFont[index].style & 128 /*0x80*/) != 0)
      {
        if (this.e.TerFont[index].PictType == 6 && this.e.TerFont[index].FieldId == 2)
          this.SetTextInputFieldWnd(index, this.e.TerFont[index].PictWidth);
        if (gr == this.e.TerGr)
        {
          this.SetPictSize(index, this.TwipsToScrY(this.e.TerFont[index].PictHeight), this.TwipsToScrX(this.e.TerFont[index].PictWidth), true);
          this.XlateSizeForPrt(index);
        }
        else
          this.SetPictSize(index, this.TwipsToUnitY(this.e.TerFont[index].PictHeight), this.TwipsToUnitX(this.e.TerFont[index].PictWidth), true);
      }
    }
    this.e.FontsReleased = false;
    if (gr == this.e.TerGr)
      this.e.TerRegFont = this.e.TerFont[0].font;
    return true;
  }

  private ushort SetCharAuxId(
    ushort OldFmt,
    int data1,
    int data2,
    string str,
    int line,
    int col)
  {
    int NewAuxId = data1;
    int OldFont = (int) OldFmt;
    if ((this.e.TerFont[OldFont].style & 128 /*0x80*/) != 0)
    {
      this.e.TerFont[OldFont].AuxId = NewAuxId;
      return (ushort) OldFont;
    }
    int newFont;
    return (newFont = this.GetNewFont(this.e.TerGr, OldFont, this.e.TerFont[OldFont].TypeFace, this.e.TerFont[OldFont].TwipsSize, this.e.TerFont[OldFont].style, this.e.TerFont[OldFont].TextColor, this.e.TerFont[OldFont].TextBkColor, this.e.TerFont[OldFont].UlineColor, this.e.TerFont[OldFont].FieldId, NewAuxId, this.e.TerFont[OldFont].Aux1Id, this.e.TerFont[OldFont].CharStyId, this.e.TerFont[OldFont].ParaStyId, this.e.TerFont[OldFont].expand, this.e.TerFont[OldFont].TempStyle, this.e.TerFont[OldFont].lang, this.e.TerFont[OldFont].FieldCode, this.e.TerFont[OldFont].offset, this.e.TerFont[OldFont].CharSet, this.e.TerFont[OldFont].flags, this.e.TerFont[OldFont].TextAngle)) >= 0 ? (ushort) newFont : OldFmt;
  }

  internal new void SetCharWidth(int line, int col, int width)
  {
    this.e.text[line].cwidth[col] = (ushort) width;
  }

  internal new void SetCtid(int line, int col, int tag)
  {
    this.e.text[line].tag[col] = (ushort) tag;
  }

  internal new bool SetCurLang(InputLanguage lng)
  {
    this.e.CurInpLang = lng;
    this.e.ReqLang = lng.Culture.LCID;
    this.e.ReqCharSet = (byte) 1;
    COp.CHARSETINFO lpCs;
    if (this.TranslateCharsetInfo(lng.Culture.TextInfo.ANSICodePage, out lpCs, 2))
      this.e.ReqCharSet = (byte) lpCs.ciCharset;
    return true;
  }

  internal new int SetCurLangFont(int CurFont)
  {
    return this.SetCurLangFont2(CurFont, (InputLanguage) null);
  }

  internal new int SetCurLangFont2(int CurFont, InputLanguage lng)
  {
    if (!this.e.FullRenderMode)
      return CurFont;
    if (lng == null)
      lng = InputLanguage.CurrentInputLanguage;
    if (lng != this.e.CurInpLang)
      this.SetCurLang(lng);
    if (this.e.TerFont[CurFont].CharSet == (byte) 2 || this.e.DefLang == 1049 && (this.e.ReqLang == 1033 || this.e.ReqLang == 2057 || this.e.ReqLang == 9) && this.e.ReqCharSet == (byte) 0 && (this.e.TerFont[CurFont].CharSet == (byte) 1 || this.e.TerFont[CurFont].CharSet == (byte) 0 || this.e.TerFont[CurFont].CharSet == (byte) 204))
      return CurFont;
    if (this.e.DefLang == 1033 && this.e.ReqLang == 1033 && this.e.ReqCharSet == (byte) 0 && this.e.TerFont[CurFont].CharSet == (byte) 1 && (this.e.TerFont[CurFont].lang == 0 || this.e.TerFont[CurFont].lang == 1033))
      this.e.ReqCharSet = this.e.TerFont[CurFont].CharSet;
    return (int) this.e.TerFont[CurFont].CharSet == (int) this.e.ReqCharSet && this.e.TerFont[CurFont].lang == this.e.ReqLang ? CurFont : this.GetNewFont(this.e.TerGr, CurFont, this.e.TerFont[CurFont].TypeFace, this.e.TerFont[CurFont].TwipsSize, this.e.TerFont[CurFont].style, this.e.TerFont[CurFont].TextColor, this.e.TerFont[CurFont].TextBkColor, this.e.TerFont[CurFont].UlineColor, this.e.TerFont[CurFont].FieldId, this.e.TerFont[CurFont].AuxId, this.e.TerFont[CurFont].Aux1Id, this.e.TerFont[CurFont].CharStyId, this.e.TerFont[CurFont].ParaStyId, this.e.TerFont[CurFont].expand, this.e.TerFont[CurFont].TempStyle, this.e.ReqLang, this.e.TerFont[CurFont].FieldCode, this.e.TerFont[CurFont].offset, this.e.ReqCharSet, this.e.TerFont[CurFont].flags, this.e.TerFont[CurFont].TextAngle);
  }

  internal new bool SetFnoteFontInfo(bool set)
  {
    int[] numArray = new int[256 /*0x0100*/];
    for (int index = 0; index < this.e.TotalFonts; ++index)
    {
      if (this.e.TerFont[index].InUse && (this.e.TerFont[index].style & 128 /*0x80*/) == 0 && this.e.TerFont[index].hidden != null && (this.e.TerFont[index].style & 2048 /*0x0800*/) != 0 && (!set || (this.e.TerFont[index].flags & 128 /*0x80*/) == 0) && (set || (this.e.TerFont[index].flags & 128 /*0x80*/) != 0))
      {
        this.SwapInts(ref this.e.TerFont[index].height, ref this.e.TerFont[index].hidden.height);
        this.SwapInts(ref this.e.TerFont[index].BaseHeight, ref this.e.TerFont[index].hidden.BaseHeight);
        this.SwapInts(ref this.e.TerFont[index].BaseHeightAdj, ref this.e.TerFont[index].hidden.BaseHeightAdj);
        this.SwapInts(ref this.e.TerFont[index].ExtLead, ref this.e.TerFont[index].hidden.ExtLead);
        this.FarMove(this.e.TerFont[index].CharWidth, numArray, 256 /*0x0100*/);
        this.FarMove(this.e.TerFont[index].hidden.CharWidth, this.e.TerFont[index].CharWidth, 256 /*0x0100*/);
        this.FarMove(numArray, this.e.TerFont[index].hidden.CharWidth, 256 /*0x0100*/);
        if (this.e.PrtFont[index].hidden != null)
        {
          this.SwapInts(ref this.e.PrtFont[index].height, ref this.e.PrtFont[index].hidden.height);
          this.SwapInts(ref this.e.PrtFont[index].BaseHeight, ref this.e.PrtFont[index].hidden.BaseHeight);
          this.SwapInts(ref this.e.PrtFont[index].BaseHeightAdj, ref this.e.PrtFont[index].hidden.BaseHeightAdj);
          this.SwapInts(ref this.e.PrtFont[index].ExtLead, ref this.e.PrtFont[index].hidden.ExtLead);
          this.FarMove(this.e.PrtFont[index].CharWidth, numArray, 256 /*0x0100*/);
          this.FarMove(this.e.PrtFont[index].hidden.CharWidth, this.e.PrtFont[index].CharWidth, 256 /*0x0100*/);
          this.FarMove(numArray, this.e.PrtFont[index].hidden.CharWidth, 256 /*0x0100*/);
        }
        if (set)
          this.e.TerFont[index].flags |= 128 /*0x80*/;
        else
          tc.ResetUintFlag(ref this.e.TerFont[index].flags, 128 /*0x80*/);
      }
    }
    return true;
  }

  internal int SetFontCharId(int CurFont, int id)
  {
    if ((this.e.TerFont[CurFont].style & 128 /*0x80*/) != 0)
      return CurFont;
    tc.StrFont font = this.e.TerFont[CurFont] with
    {
      CharId = id
    };
    return this.GetNewFont2(this.e.TerGr, CurFont, font);
  }

  internal new int SetFontFlags(int CurFont, int flags, bool set)
  {
    if ((this.e.TerFont[CurFont].style & 128 /*0x80*/) != 0)
      return CurFont;
    int flags1 = this.e.TerFont[CurFont].flags;
    if (set)
      flags1 |= flags;
    else
      tc.ResetUintFlag(ref flags1, flags);
    return this.GetNewFont(this.e.TerGr, CurFont, this.e.TerFont[CurFont].TypeFace, this.e.TerFont[CurFont].TwipsSize, this.e.TerFont[CurFont].style, this.e.TerFont[CurFont].TextColor, this.e.TerFont[CurFont].TextBkColor, this.e.TerFont[CurFont].UlineColor, this.e.TerFont[CurFont].FieldId, this.e.TerFont[CurFont].AuxId, this.e.TerFont[CurFont].Aux1Id, this.e.TerFont[CurFont].CharStyId, this.e.TerFont[CurFont].ParaStyId, this.e.TerFont[CurFont].expand, this.e.TerFont[CurFont].TempStyle, this.e.TerFont[CurFont].lang, this.e.TerFont[CurFont].FieldCode, this.e.TerFont[CurFont].offset, this.e.TerFont[CurFont].CharSet, flags1, this.e.TerFont[CurFont].TextAngle);
  }

  internal new int SetFontStyle(int CurFont, int style, bool set)
  {
    if ((this.e.TerFont[CurFont].style & 128 /*0x80*/) != 0)
      return CurFont;
    int style1 = this.e.TerFont[CurFont].style;
    if (set)
      style1 |= style;
    else
      tc.ResetUintFlag(ref style1, style);
    return this.GetNewFont(this.e.TerGr, CurFont, this.e.TerFont[CurFont].TypeFace, this.e.TerFont[CurFont].TwipsSize, style1, this.e.TerFont[CurFont].TextColor, this.e.TerFont[CurFont].TextBkColor, this.e.TerFont[CurFont].UlineColor, this.e.TerFont[CurFont].FieldId, this.e.TerFont[CurFont].AuxId, this.e.TerFont[CurFont].Aux1Id, this.e.TerFont[CurFont].CharStyId, this.e.TerFont[CurFont].ParaStyId, this.e.TerFont[CurFont].expand, this.e.TerFont[CurFont].TempStyle, this.e.TerFont[CurFont].lang, this.e.TerFont[CurFont].FieldCode, this.e.TerFont[CurFont].offset, this.e.TerFont[CurFont].CharSet, this.e.TerFont[CurFont].flags, this.e.TerFont[CurFont].TextAngle);
  }

  internal new int SetFontStyleId(int CurFont, int CharStyId, int ParaStyId)
  {
    return (this.e.TerFont[CurFont].style & 128 /*0x80*/) != 0 ? CurFont : this.GetNewFont(this.e.TerGr, CurFont, this.e.TerFont[CurFont].TypeFace, this.e.TerFont[CurFont].TwipsSize, this.e.TerFont[CurFont].style, this.e.TerFont[CurFont].TextColor, this.e.TerFont[CurFont].TextBkColor, this.e.TerFont[CurFont].UlineColor, this.e.TerFont[CurFont].FieldId, this.e.TerFont[CurFont].AuxId, this.e.TerFont[CurFont].Aux1Id, CharStyId, ParaStyId, this.e.TerFont[CurFont].expand, this.e.TerFont[CurFont].TempStyle, this.e.TerFont[CurFont].lang, this.e.TerFont[CurFont].FieldCode, this.e.TerFont[CurFont].offset, this.e.TerFont[CurFont].CharSet, this.e.TerFont[CurFont].flags, this.e.TerFont[CurFont].TextAngle);
  }

  internal new int SetFontTextAngle(int CurFont, int TextAngle)
  {
    if ((this.e.TerFont[CurFont].style & 128 /*0x80*/) != 0)
    {
      this.e.TerFont[CurFont].TextAngle = TextAngle;
      this.SetPictSize(CurFont, this.TwipsToScrY(this.e.TerFont[CurFont].PictHeight), this.TwipsToScrX(this.e.TerFont[CurFont].PictWidth), true);
      this.XlateSizeForPrt(CurFont);
      return CurFont;
    }
    int newFont;
    return (newFont = this.GetNewFont(this.e.TerGr, CurFont, this.e.TerFont[CurFont].TypeFace, this.e.TerFont[CurFont].TwipsSize, this.e.TerFont[CurFont].style, this.e.TerFont[CurFont].TextColor, this.e.TerFont[CurFont].TextBkColor, this.e.TerFont[CurFont].UlineColor, this.e.TerFont[CurFont].FieldId, this.e.TerFont[CurFont].AuxId, this.e.TerFont[CurFont].Aux1Id, this.e.TerFont[CurFont].CharStyId, this.e.TerFont[CurFont].ParaStyId, this.e.TerFont[CurFont].expand, this.e.TerFont[CurFont].TempStyle, this.e.TerFont[CurFont].lang, this.e.TerFont[CurFont].FieldCode, this.e.TerFont[CurFont].offset, this.e.TerFont[CurFont].CharSet, this.e.TerFont[CurFont].flags, TextAngle)) >= 0 ? newFont : CurFont;
  }

  internal new int SetScapFont(int CurFont, bool set)
  {
    if ((this.e.TerFont[CurFont].style & 128 /*0x80*/) != 0)
      return CurFont;
    int flags = this.e.TerFont[CurFont].flags;
    if (set)
      flags |= 512 /*0x0200*/;
    else
      tc.ResetUintFlag(ref flags, 512 /*0x0200*/);
    return this.GetNewFont(this.e.TerGr, CurFont, this.e.TerFont[CurFont].TypeFace, this.e.TerFont[CurFont].TwipsSize, this.e.TerFont[CurFont].style, this.e.TerFont[CurFont].TextColor, this.e.TerFont[CurFont].TextBkColor, this.e.TerFont[CurFont].UlineColor, this.e.TerFont[CurFont].FieldId, this.e.TerFont[CurFont].AuxId, this.e.TerFont[CurFont].Aux1Id, this.e.TerFont[CurFont].CharStyId, this.e.TerFont[CurFont].ParaStyId, this.e.TerFont[CurFont].expand, this.e.TerFont[CurFont].TempStyle, this.e.TerFont[CurFont].lang, this.e.TerFont[CurFont].FieldCode, this.e.TerFont[CurFont].offset, this.e.TerFont[CurFont].CharSet, flags, this.e.TerFont[CurFont].TextAngle);
  }

  internal new int SetTag(int line, int col, int type, string name, string AuxText, int AuxInt)
  {
    if (line < 0)
    {
      line = this.e.CurLine;
      col = this.e.CurCol;
    }
    int index;
    if (line < 0 || line >= this.e.TotalLines || col < 0 || col >= this.e.text[line].len || -1 == (index = this.GetTagSlot()))
      return 0;
    ushort[] numArray = this.OpenCtid(line);
    if (numArray[col] != (ushort) 0)
    {
      int next;
      for (next = (int) numArray[col]; this.e.CharTag[next].type != type || !(this.e.CharTag[next].name == name); next = this.e.CharTag[next].next)
      {
        if (this.e.CharTag[next].next == 0)
        {
          this.e.CharTag[next].next = index;
          if (this.e.CheckEndlessLoopTags(next))
          {
            this.e.CharTag[next].next = 0;
            goto label_13;
          }
          goto label_13;
        }
      }
      index = next;
      this.FreeTag(index);
    }
    else
      numArray[col] = (ushort) index;
label_13:
    this.CloseCtid(line);
    this.e.CharTag[index].InUse = true;
    this.e.CharTag[index].type = type;
    this.e.CharTag[index].name = name;
    this.e.CharTag[index].AuxText = AuxText;
    this.e.CharTag[index].AuxInt = AuxInt;
    if (this.e.CheckEndlessLoopTags(index))
      this.e.CharTag[index].next = 0;
    ++this.e.TerArg.modified;
    return index;
  }

  internal bool SetTerBkColor(Color color, bool repaint)
  {
    tc.DgtGetNewFontId GetNewFontId = new tc.DgtGetNewFontId(this.GetNewBkColor);
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.e.CurSID < 0)
      return this.CharFmt(GetNewFontId, this.ToColorRef(color), 0, (string) null, repaint);
    this.e.StyleId[this.e.CurSID].TextBkColor = color;
    return true;
  }

  internal bool SetTerCharStyle(int FmtType, bool OnOff, bool repaint)
  {
    tc.DgtGetNewFontId GetNewFontId = new tc.DgtGetNewFontId(this.GetNewStyle);
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (OnOff && (FmtType & 16384 /*0x4000*/) != 0)
      this.e.LinkStyle |= 16384 /*0x4000*/;
    if (this.e.CurSID < 0)
      return this.CharFmt(GetNewFontId, FmtType, OnOff ? 1 : 0, (string) null, repaint);
    FmtType = tc.ResetUintFlag(ref FmtType, 8192 /*0x2000*/);
    if (OnOff)
      this.e.StyleId[this.e.CurSID].style |= FmtType;
    else
      this.e.StyleId[this.e.CurSID].style = tc.ResetUintFlag(ref this.e.StyleId[this.e.CurSID].style, FmtType);
    return true;
  }

  internal bool SetTerColor(Color color, bool repaint)
  {
    tc.DgtGetNewFontId GetNewFontId = new tc.DgtGetNewFontId(this.GetNewColor);
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.e.CurSID < 0)
      return this.CharFmt(GetNewFontId, this.ToColorRef(color), 0, (string) null, repaint);
    this.e.StyleId[this.e.CurSID].TextColor = color;
    return true;
  }

  internal bool SetTerDefaultFont(
    string TypeFace,
    int PointSize,
    int style,
    Color TextColor,
    bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    this.ReleaseUndo();
    int num = PointSize < 0 ? -PointSize : PointSize * 20;
    this.DeleteTerObject(0);
    this.InitTerObject(0);
    this.e.TerFont[0].InUse = true;
    this.e.TerFont[0].TypeFace = TypeFace;
    this.e.TerFont[0].TwipsSize = num;
    this.e.TerFont[0].style = style;
    if (!this.CreateOneFont(this.e.TerGr, 0, true))
      return this.PrintError(42, nameof (SetTerDefaultFont));
    this.e.TerRegFont = this.e.TerCurFont = this.e.TerFont[0].font;
    this.e.hTerCurFont = this.e.TerFont[0].hFont;
    this.e.TerFont[0].TextColor = TextColor;
    this.e.TerArg.FontTypeFace = TypeFace;
    this.e.TerArg.PointSize = num / 20;
    this.e.StyleId[0].TypeFace = TypeFace;
    this.e.StyleId[0].TwipsSize = num;
    this.e.StyleId[0].style = style;
    this.e.StyleId[0].TextColor = TextColor;
    COp.TEXTMETRIC tm;
    if (!this.e.FullRenderMode && this.e.TerFont[0].TextMetric.HasValue)
    {
      tm = this.e.TerFont[0].TextMetric.Value;
    }
    else
    {
      this.GetTextMetrics(this.e.TerGr, this.e.TerRegFont, out tm);
      if (!this.e.FullRenderMode)
        this.e.TerFont[0].TextMetric = new COp.TEXTMETRIC?(tm);
    }
    this.e.TerTextMet = tm;
    this.draw.GetWinDimension();
    this.DeleteTextMap(true);
    this.e.ToolBarCfmt = -1;
    if (this.e.TerArg.ShowStatus && !this.e.InRtfRead)
      this.DisplayStatus();
    if (repaint)
      this.PaintTer();
    return true;
  }

  internal bool SetTerFont(string TypeFace, bool repaint)
  {
    tc.DgtGetNewFontId GetNewFontId = new tc.DgtGetNewFontId(this.GetNewTypeFace);
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.e.CurSID < 0)
      return this.CharFmt(GetNewFontId, 0, 0, TypeFace, repaint);
    this.e.StyleId[this.e.CurSID].TypeFace = TypeFace;
    return true;
  }

  internal bool SetTerPointSize(int PointSize, bool repaint)
  {
    tc.DgtGetNewFontId GetNewFontId = new tc.DgtGetNewFontId(this.GetNewPointSize);
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int data1 = PointSize < 0 ? -PointSize : PointSize * 20;
    if (this.e.CurSID >= 0)
    {
      this.e.StyleId[this.e.CurSID].TwipsSize = data1;
      return true;
    }
    bool flag = this.CharFmt(GetNewFontId, data1, 0, (string) null, repaint);
    if (this.e.HilightType != 0)
    {
      for (int hilightBegRow = this.e.HilightBegRow; hilightBegRow <= this.e.HilightEndRow; ++hilightBegRow)
      {
        if (this.IsBaselineAlignedCellLine(hilightBegRow))
        {
          if (hilightBegRow < this.e.RepageBeginLine)
            this.e.RepageBeginLine = hilightBegRow;
          this.RequestPagination(false);
          break;
        }
      }
    }
    return flag;
  }

  internal new bool TerColors(bool foreground)
  {
    ColorDialog colorDialog = new ColorDialog();
    int[] numArray = new int[16 /*0x10*/];
    int index1;
    if (this.e.text[this.e.CurLine].fmt == null)
    {
      index1 = (int) this.e.text[this.e.CurLine].UniFmt;
    }
    else
    {
      ushort[] fmt = this.e.text[this.e.CurLine].fmt;
      index1 = this.e.CurCol >= this.e.text[this.e.CurLine].len ? 0 : (int) fmt[this.e.CurCol];
      if ((this.e.TerFont[index1].style & 128 /*0x80*/) != 0)
        index1 = 0;
    }
    float num1 = (float) byte.MaxValue / 16f;
    for (int index2 = 0; index2 < 16 /*0x10*/; ++index2)
    {
      int num2 = (int) (byte) ((double) (index2 + 1) * (double) num1);
      numArray[index2] = (num2 << 16 /*0x10*/) + (num2 << 8) + num2;
    }
    colorDialog.Color = this.e.CurSID < 0 ? (!foreground ? this.e.TerFont[index1].TextBkColor : this.e.TerFont[index1].TextColor) : (!foreground ? this.e.StyleId[this.e.CurSID].TextBkColor : this.e.StyleId[this.e.CurSID].TextColor);
    if (colorDialog.Color == tc.CLR_AUTO)
      colorDialog.Color = tc.CLR_WHITE;
    colorDialog.CustomColors = numArray;
    int num3 = DialogResult.OK == colorDialog.ShowDialog() ? 1 : 0;
    this.e.Focus();
    if (num3 == 0)
      return false;
    return foreground ? this.e.SetTerColor(colorDialog.Color, true) : this.e.SetTerBkColor(colorDialog.Color, true);
  }

  internal int TerCreateFont(
    int ReuseId,
    bool shared,
    string NewTypeFace,
    int NewPointSize,
    int NewStyle,
    Color NewTextColor,
    Color NewTextBkColor,
    int NewFieldId,
    int NewAuxId)
  {
    return this.TerCreateFont2(ReuseId, shared, NewTypeFace, NewPointSize, NewStyle, NewTextColor, NewTextBkColor, NewFieldId, NewAuxId, 1, 0, 0);
  }

  internal int TerCreateFont2(
    int ReuseId,
    bool shared,
    string NewTypeFace,
    int NewPointSize,
    int NewStyle,
    Color NewTextColor,
    Color NewTextBkColor,
    int NewFieldId,
    int NewAuxId,
    int NewCharStyId,
    int NewParaStyId,
    int NewExpand)
  {
    return this.TerCreateFont3(ReuseId, shared, NewTypeFace, NewPointSize, NewStyle, NewTextColor, NewTextBkColor, NewFieldId, NewAuxId, NewCharStyId, NewParaStyId, NewExpand, 1);
  }

  internal int TerCreateFont3(
    int ReuseId,
    bool shared,
    string NewTypeFace,
    int NewPointSize,
    int NewStyle,
    Color NewTextColor,
    Color NewTextBkColor,
    int NewFieldId,
    int NewAuxId,
    int NewCharStyId,
    int NewParaStyId,
    int NewExpand,
    int NewCharSet)
  {
    return this.TerCreateFont3(ReuseId, shared, NewTypeFace, NewPointSize, NewStyle, NewTextColor, NewTextBkColor, tc.CLR_AUTO, NewFieldId, NewAuxId, NewCharStyId, NewParaStyId, NewExpand, NewCharSet, 0, 0);
  }

  /// <summary>Создать шрифт</summary>
  /// <param name="ReuseId">Использовать существующий id с новой информацией. -1 чтобы создать новый id.</param>
  /// <param name="shared">Если true, редактор пытается найти шрифт с теми же параметрами и вернёт его id, иначе создаст новый. true допустимо только при отрицательном ReuseId</param>
  /// <param name="NewTypeFace">Гарнитура шрифта</param>
  /// <param name="NewPointSize">Размер в поинтах для положительного значения и twips для отрицательного</param>
  /// <param name="NewStyle">Флаги стиль</param>
  /// <param name="NewTextColor">Цвет текста</param>
  /// <param name="NewTextBkColor">Цвет фона</param>
  /// <param name="NewULineColor">Цвет подчёркивания</param>
  /// <param name="NewFieldId">Идентификатор поля. 0 для значения по умолчанию</param>
  /// <param name="NewAuxId">Идентификатор назначенный приложением. 0 для значения по умолчанию</param>
  /// <param name="NewCharStyId">Идентификатор стиля шрифта. 0 для стиля "Обычный". 1 для значения по умолчанию</param>
  /// <param name="NewParaStyId">Идентификатор стиля параграфа. 0 для значения по умолчанию</param>
  /// <param name="NewExpand">Расширение ширины символов в twips. 0 для значения по умолчанию</param>
  /// <param name="NewCharSet">Кодировка символов</param>
  /// <param name="NewLang">Язык</param>
  /// <param name="NewTextAngle">Угол поворота текста</param>
  /// <returns></returns>
  internal int TerCreateFont3(
    int ReuseId,
    bool shared,
    string NewTypeFace,
    int NewPointSize,
    int NewStyle,
    Color NewTextColor,
    Color NewTextBkColor,
    Color NewULineColor,
    int NewFieldId,
    int NewAuxId,
    int NewCharStyId,
    int NewParaStyId,
    int NewExpand,
    int NewCharSet,
    int NewLang,
    int NewTextAngle)
  {
    int OldFont = 0;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (ReuseId >= 320)
      return -1;
    int NewTwipsSize = NewPointSize < 0 ? -NewPointSize : NewPointSize * 20;
    if (ReuseId >= 0)
      shared = false;
    if (ReuseId >= this.e.MaxFonts)
      this.ExpandFontTable(ReuseId + 1);
    if (ReuseId >= this.e.TotalFonts)
    {
      for (int totalFonts = this.e.TotalFonts; totalFonts <= ReuseId; ++totalFonts)
        this.InitTerObject(totalFonts);
      this.e.TotalFonts = ReuseId + 1;
    }
    if (!shared)
      this.e.MatchIds = false;
    if (ReuseId >= 0)
      OldFont = ReuseId;
    if (NewCharStyId < 0 || NewCharStyId >= this.e.TotalSID || !this.e.StyleId[NewCharStyId].InUse || this.e.StyleId[NewCharStyId].type != 1)
      NewCharStyId = 1;
    if (NewParaStyId < 0 || NewParaStyId >= this.e.TotalSID || !this.e.StyleId[NewParaStyId].InUse || this.e.StyleId[NewParaStyId].type != 2)
      NewParaStyId = 0;
    int idx = this.GetNewFont(this.e.TerGr, OldFont, NewTypeFace, NewTwipsSize, NewStyle, NewTextColor, NewTextBkColor, NewULineColor, NewFieldId, NewAuxId, this.e.NextFontAux1Id, NewCharStyId, NewParaStyId, NewExpand, 0, NewLang, (string) null, 0, (byte) NewCharSet, 0, NewTextAngle);
    this.e.NextFontAux1Id = 0;
    if (idx >= 0)
      this.e.TerFont[idx].flags |= 256 /*0x0100*/;
    if (ReuseId >= 0 && idx != ReuseId)
    {
      if (this.e.TerFont[ReuseId].InUse)
        this.DeleteTerObject(ReuseId);
      int[] charWidth1 = this.e.TerFont[ReuseId].CharWidth;
      int[] charWidth2 = this.e.PrtFont[ReuseId].CharWidth;
      this.e.TerFont[ReuseId] = this.e.TerFont[idx];
      this.e.PrtFont[ReuseId] = this.e.PrtFont[idx];
      this.e.TerFont[idx].CharWidth = charWidth1;
      this.e.PrtFont[idx].CharWidth = charWidth2;
      this.InitTerObject(idx);
      idx = ReuseId;
    }
    if (idx == 0)
    {
      COp.TEXTMETRIC tm;
      if (!this.e.FullRenderMode && this.e.TerFont[0].TextMetric.HasValue)
      {
        tm = this.e.TerFont[0].TextMetric.Value;
      }
      else
      {
        this.e.TerRegFont = this.e.TerCurFont = this.e.TerFont[0].font;
        this.e.hTerCurFont = this.e.TerFont[0].hFont;
        this.GetTextMetrics(this.e.TerGr, this.e.TerCurFont, out tm);
        if (!this.e.FullRenderMode)
          this.e.TerFont[0].TextMetric = new COp.TEXTMETRIC?(tm);
      }
      this.e.TerTextMet = tm;
      this.draw.GetWinDimension();
    }
    if (this.e.FullRenderMode)
      this.DeleteTextMap(true);
    if (this.e.TerArg.ShowStatus && idx == 0)
      this.DisplayStatus();
    return idx;
  }

  internal bool TerDeleteBookmark(string name)
  {
    bool flag = false;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int curLine = this.e.CurLine;
    int curCol = this.e.CurCol;
    while (this.PosTag(-1, name, 1, 0, false))
      flag = this.DeleteTag(this.e.CurLine, this.e.CurCol, 1, name) != 0;
    this.e.CurLine = curLine;
    this.e.CurCol = curCol;
    return flag;
  }

  internal int TerDeleteTag(int line, int col, int type, string name)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    return this.DeleteTag(line, col, type, name);
  }

  internal new bool TerFonts()
  {
    if (this.e.HilightType == 1)
    {
      int hilightBegRow1 = this.e.HilightBegRow;
    }
    else if (this.e.HilightType == 2)
    {
      int hilightBegRow2 = this.e.HilightBegRow;
      int hilightBegCol = this.e.HilightBegCol;
    }
    else
    {
      int curLine = this.e.CurLine;
      int curCol = this.e.CurCol;
    }
    int index = this.e.InputFontId < 0 ? this.GetEffectiveCfmt() : this.e.InputFontId;
    this.e.DlgInt1 = index;
    this.e.ReqTypeFace = this.e.TerFont[index].TypeFace;
    this.e.ReqTwipsSize = this.e.TerFont[index].TwipsSize;
    if (!this.CallDialogBox((Form) new terdlg_font(this.e)))
      return false;
    this.e.SetTerFont(this.e.ReqTypeFace, false);
    this.e.SetTerPointSize(-this.e.ReqTwipsSize, true);
    return true;
  }

  internal int TerGetBookmark(int index, out string name)
  {
    int bookmark = 0;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    name = "";
    if (index < 0)
      this.UpdateTagTable();
    for (int index1 = 1; index1 < this.e.TotalCharTags; ++index1)
    {
      if (this.e.CharTag[index1].InUse && this.e.CharTag[index1].type == 1)
        ++bookmark;
    }
    if (bookmark != 0 && index >= 0)
    {
      if (index >= bookmark)
        return 0;
      int num = -1;
      name = "";
      for (int index2 = 1; index2 < this.e.TotalCharTags; ++index2)
      {
        if (this.e.CharTag[index2].InUse && this.e.CharTag[index2].type == 1)
        {
          ++num;
          if (num == index)
          {
            name = this.e.CharTag[index2].name;
            break;
          }
        }
      }
    }
    return bookmark;
  }

  internal new bool TerGetCharWidth(
    Graphics gr,
    int NewFont,
    bool ScreenFont,
    int overhang,
    byte PitchAndFamily)
  {
    Font font = this.e.TerFont[NewFont].font;
    if ((this.GetDeviceCaps(gr, 2) != 1 || ((int) PitchAndFamily & 4) != 0) && this.GetCharWidth(gr, font, 0, (int) byte.MaxValue, this.e.TerFont[NewFont].CharWidth))
    {
      for (int index = 0; index < 256 /*0x0100*/; ++index)
        this.e.TerFont[NewFont].CharWidth[index] -= overhang;
    }
    else
    {
      int charWidthAlt1 = this.TerGetCharWidthAlt(gr, NewFont, 'A', overhang);
      int charWidthAlt2 = this.TerGetCharWidthAlt(gr, NewFont, 'B', overhang);
      int num = this.e.TerFont[NewFont].CharWidth[65];
      if (charWidthAlt1 != num || charWidthAlt2 != this.e.TerFont[NewFont].CharWidth[66])
      {
        this.e.TerFont[NewFont].CharWidth[0] = 0;
        for (int chr = 1; chr < 256 /*0x0100*/; ++chr)
          this.e.TerFont[NewFont].CharWidth[chr] = this.TerGetCharWidthAlt(gr, NewFont, (char) chr, overhang);
      }
    }
    int x = this.e.TerFont[NewFont].expand;
    if (x != 0)
    {
      if (x > 4320)
        x = 4320;
      int num1 = !ScreenFont ? this.TwipsToUnitX(x) : this.TwipsToScrX(x);
      for (int index1 = 0; index1 < 256 /*0x0100*/; ++index1)
      {
        int num2 = num1;
        int num3 = this.e.TerFont[NewFont].CharWidth[index1];
        if (num2 < 0 && -num2 > num3 / 2)
          num2 = -num3 / 2;
        int[] charWidth;
        IntPtr index2;
        (charWidth = this.e.TerFont[NewFont].CharWidth)[(int) (index2 = (IntPtr) index1)] = charWidth[(int) index2] + num2;
      }
    }
    this.e.TerFont[NewFont].CharWidth[0] = 0;
    this.e.TerFont[NewFont].CharWidth[(int) (byte) this.e.ParaChar] = this.e.TerFont[NewFont].CharWidth[(this.e.TerFlags2 & 4194304 /*0x400000*/) != 0 ? 191 : 182];
    this.e.TerFont[NewFont].CharWidth[15] = this.e.TerFont[NewFont].CharWidth[(this.e.TerFlags2 & 8388608 /*0x800000*/) != 0 ? 175 : 171];
    this.e.TerFont[NewFont].CharWidth[(int) (byte) this.e.CellChar] = this.e.TerFont[NewFont].CharWidth[164];
    this.e.TerFont[NewFont].CharWidth[14] = this.e.TerFont[NewFont].CharWidth[32 /*0x20*/];
    this.e.TerFont[NewFont].CharWidth[23] = this.e.TerFont[NewFont].CharWidth[45];
    if (this.e.ShowParaMark && !this.e.InPrinting)
      this.e.TerFont[NewFont].CharWidth[6] = this.e.TerFont[NewFont].CharWidth[172];
    else
      this.e.TerFont[NewFont].CharWidth[6] = 0;
    if (this.e.ShowParaMark && !this.e.InPrinting)
      this.e.TerFont[NewFont].CharWidth[28] = this.e.TerFont[NewFont].CharWidth[149];
    else
      this.e.TerFont[NewFont].CharWidth[28] = 0;
    return true;
  }

  internal new int TerGetCharWidthAlt(Graphics gr, int NewFont, char chr, int overhang)
  {
    int num = 10;
    COp.SIZE size;
    this.GetTextExtentPoint(gr, this.e.TerFont[NewFont].font, new string(chr, num), num, out size);
    size.cx -= overhang;
    int charWidthAlt = size.cx / num;
    if (size.cx - charWidthAlt * num >= num / 2)
      ++charWidthAlt;
    return charWidthAlt;
  }

  internal int TerGetCurFont(int line, int col)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (line < 0)
    {
      line = this.e.CurLine;
      col = this.e.CurCol;
    }
    return this.GetCurCfmt(line, col);
  }

  internal int TerGetEffectiveFont()
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    return this.GetEffectiveCfmt();
  }

  internal new int TerGetFieldFont(int font, int FieldId, string FieldCode)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (font < 0 || font >= this.e.TotalFonts || !this.e.TerFont[font].InUse)
      return -1;
    if (this.e.TerFont[font].FieldId == FieldId && this.IsSameFieldCode(this.e.TerFont[font].FieldCode, FieldCode))
      return font;
    if ((this.e.TerFont[font].style & 128 /*0x80*/) == 0)
      return (int) this.GetNewFieldId((ushort) font, FieldId, FieldCode, 0, 0);
    this.e.TerFont[font].FieldId = FieldId;
    this.e.TerFont[font].FieldCode = FieldCode;
    return font;
  }

  internal int TerGetFontAux1Id(int CurFont)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    return CurFont < 0 || CurFont >= this.e.TotalFonts || !this.e.TerFont[CurFont].InUse ? 0 : this.e.TerFont[CurFont].Aux1Id;
  }

  internal bool TerGetFontFieldCode(int font, out string FieldCode)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    FieldCode = "";
    if (font < 0 || font >= this.e.TotalFonts || !this.e.TerFont[font].InUse)
      return false;
    if (this.e.TerFont[font].FieldCode != null)
      FieldCode = this.e.TerFont[font].FieldCode;
    return true;
  }

  internal int TerGetFontFieldId(int font)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    return font < 0 || font >= this.e.TotalFonts || !this.e.TerFont[font].InUse ? 0 : this.e.TerFont[font].FieldId;
  }

  internal int TerGetFontLang(int font)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (font < 0 || font >= this.e.TotalFonts || !this.e.TerFont[font].InUse || (this.e.TerFont[font].style & 128 /*0x80*/) != 0)
      return 0;
    return this.e.TerFont[font].lang == 0 ? this.e.DefLang : this.e.TerFont[font].lang;
  }

  internal int TerGetFontParam(int FontId, int type)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (FontId >= 0)
    {
      if (FontId >= this.e.TotalFonts || !this.e.TerFont[FontId].InUse)
        return -1;
      if (type == 2)
        return (int) this.e.TerFont[FontId].CharSet;
      if (type == 3)
        return this.e.TerFont[FontId].PictWidth;
      if (type == 4)
        return this.e.TerFont[FontId].PictHeight;
      if (type == 6)
        return this.e.TerFont[FontId].AuxId;
      if (type == 7)
        return this.e.TerFont[FontId].flags;
      if (type == 8)
        return this.e.TerFont[FontId].FrameType;
      if (type == 9)
        return this.e.TerFont[FontId].ParaFID;
      if (type == 9)
        return this.e.TerFont[FontId].ParaFID;
      if (type == 10)
        return this.e.TerFont[FontId].offset;
      if (type == 11)
        return (this.e.TerFont[FontId].style & 128 /*0x80*/) == 0 ? 0 : 1;
      if (type != 12)
        return -1;
      return (this.e.TerFont[FontId].style & 128 /*0x80*/) == 0 || this.e.TerFont[FontId].ctl == null ? 0 : 1;
    }
    int sid;
    return (sid = this.ParamIdToSID(FontId)) < 0 || type != 10 ? -1 : this.e.StyleId[sid].offset;
  }

  internal bool TerGetFontParam(int FontId, int type, out Color color)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    color = Color.Black;
    if (FontId >= 0)
    {
      if (FontId >= this.e.TotalFonts || !this.e.TerFont[FontId].InUse || type != 5)
        return false;
      color = this.e.TerFont[FontId].UlineColor;
      return true;
    }
    int sid;
    if ((sid = this.ParamIdToSID(FontId)) < 0 || type != 5)
      return false;
    color = this.e.StyleId[sid].UlineColor;
    return true;
  }

  internal int TerGetFontSpace(int font)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    return font < 0 || font >= this.e.TotalFonts || !this.e.TerFont[font].InUse || (this.e.TerFont[font].style & 128 /*0x80*/) != 0 ? 0 : this.e.TerFont[font].expand;
  }

  internal int TerGetFontStyleId(int FontId)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    return FontId < 0 || FontId >= this.e.TotalFonts || !this.e.TerFont[FontId].InUse ? -1 : this.e.TerFont[FontId].CharStyId;
  }

  internal int TerGetTag(
    int line,
    int col,
    out string name,
    out string AuxText,
    out int AuxInt,
    out int flags)
  {
    return this.TerGetTagEx(line, col, 0, out name, out AuxText, out AuxInt, out flags);
  }

  internal int TerGetTagEx(
    int line,
    int col,
    int TagType,
    out string name,
    out string AuxText,
    out int AuxInt,
    out int flags)
  {
    return this.TerGetTagEx(line, col, TagType, out name, out AuxText, out AuxInt, out tc.SkipObject, out flags);
  }

  internal int TerGetTagEx(
    int line,
    int col,
    int TagType,
    out string name,
    out string AuxText,
    out int AuxInt,
    out object obj,
    out int flags)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    flags = 0;
    return this.GetTag(line, col, TagType, out name, out AuxText, out AuxInt, out obj);
  }

  /// <summary>CHANGED Добавил ULineColor</summary>
  /// <param name="FontId"></param>
  /// <param name="TextColor"></param>
  /// <param name="TextBackColor"></param>
  /// <param name="ULineColor"></param>
  /// <returns></returns>
  internal bool TerGetTextColor(
    int FontId,
    out Color TextColor,
    out Color TextBackColor,
    out Color ULineColor)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    TextColor = Color.Black;
    TextBackColor = Color.White;
    ULineColor = tc.CLR_AUTO;
    if (FontId >= 0)
    {
      if (FontId >= this.e.TotalFonts || !this.e.TerFont[FontId].InUse)
        return false;
      TextColor = this.e.TerFont[FontId].TextColor;
      TextBackColor = this.e.TerFont[FontId].TextBkColor;
      ULineColor = this.e.TerFont[FontId].UlineColor;
    }
    else
    {
      int sid;
      if ((sid = this.ParamIdToSID(FontId)) < 0)
        return false;
      TextColor = this.e.StyleId[sid].TextColor;
      TextBackColor = this.e.StyleId[sid].TextBkColor;
      ULineColor = this.e.StyleId[sid].UlineColor;
    }
    return true;
  }

  internal int TerInsertBookmark(int line, int col, string name)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.IsValidBookmark(name, false))
      return 0;
    this.TerDeleteBookmark(name);
    return this.SetTag(line, col, 1, name, (string) null, 0);
  }

  internal bool TerLocateAuxIdChar(
    int AuxId,
    bool present,
    ref int StartLine,
    ref int StartCol,
    bool forward)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (forward)
    {
      for (int line = StartLine; line < this.e.TotalLines; ++line)
      {
        int num = line != StartLine ? 0 : StartCol;
        if (num < this.e.text[line].len && this.e.text[line].len != 0)
        {
          if (this.e.text[line].fmt == null)
          {
            int auxId = this.e.TerFont[(int) this.e.text[line].UniFmt].AuxId;
            if (present && auxId == AuxId || !present && auxId != AuxId)
            {
              StartLine = line;
              StartCol = num;
              return true;
            }
          }
          else
          {
            ushort[] numArray = this.OpenCfmt(line);
            for (int index = num; index < this.e.text[line].len; ++index)
            {
              int auxId = this.e.TerFont[(int) numArray[index]].AuxId;
              if (present && auxId == AuxId || !present && auxId != AuxId)
              {
                StartLine = line;
                StartCol = index;
                this.CloseCfmt(line);
                return true;
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
            int auxId = this.e.TerFont[(int) this.e.text[line].UniFmt].AuxId;
            if (present && auxId == AuxId || !present && auxId != AuxId)
            {
              StartLine = line;
              StartCol = num;
              return true;
            }
          }
          else
          {
            ushort[] numArray = this.OpenCfmt(line);
            for (int index = num; index >= 0; --index)
            {
              int auxId = this.e.TerFont[(int) numArray[index]].AuxId;
              if (present && auxId == AuxId || !present && auxId != AuxId)
              {
                StartLine = line;
                StartCol = index;
                this.CloseCfmt(line);
                return true;
              }
            }
            this.CloseCfmt(line);
          }
        }
      }
    }
    return false;
  }

  internal bool TerLocateFontFlags(
    int flags,
    bool present,
    ref int StartLine,
    ref int StartCol,
    bool forward)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (forward)
    {
      for (int line = StartLine; line < this.e.TotalLines; ++line)
      {
        int num = line != StartLine ? 0 : StartCol;
        if (num < this.e.text[line].len && this.e.text[line].len != 0)
        {
          if (this.e.text[line].fmt == null)
          {
            int flags1 = this.e.TerFont[(int) this.e.text[line].UniFmt].flags;
            if (present && (flags1 & flags) != 0 || !present && (flags1 & flags) == 0)
            {
              StartLine = line;
              StartCol = num;
              return true;
            }
          }
          else
          {
            ushort[] numArray = this.OpenCfmt(line);
            for (int index = num; index < this.e.text[line].len; ++index)
            {
              int flags2 = this.e.TerFont[(int) numArray[index]].flags;
              if (present && (flags2 & flags) != 0 || !present && (flags2 & flags) == 0)
              {
                StartLine = line;
                StartCol = index;
                this.CloseCfmt(line);
                return true;
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
            int flags3 = this.e.TerFont[(int) this.e.text[line].UniFmt].flags;
            if (present && (flags3 & flags) != 0 || !present && (flags3 & flags) == 0)
            {
              StartLine = line;
              StartCol = num;
              return true;
            }
          }
          else
          {
            ushort[] numArray = this.OpenCfmt(line);
            for (int index = num; index >= 0; --index)
            {
              int flags4 = this.e.TerFont[(int) numArray[index]].flags;
              if (present && (flags4 & flags) != 0 || !present && (flags4 & flags) == 0)
              {
                StartLine = line;
                StartCol = index;
                this.CloseCfmt(line);
                return true;
              }
            }
            this.CloseCfmt(line);
          }
        }
      }
    }
    return false;
  }

  internal bool TerLocateFontId(int FontId, ref int pLineNo, ref int pCol)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int index1 = pLineNo;
    int index2 = pCol;
    if (index1 < 0 || index1 >= this.e.TotalLines)
    {
      index1 = this.e.CurLine;
      index2 = this.e.CurCol;
    }
    if (index2 >= this.e.text[index1].len)
      index2 = this.e.text[index1].len - 1;
    while (index1 < this.e.TotalLines)
    {
      if (this.e.text[index1].len != 0)
      {
        if (this.e.text[index1].fmt == null)
        {
          if ((int) this.e.text[index1].UniFmt != (int) (ushort) FontId)
            goto label_13;
        }
        else
        {
          ushort[] fmt = this.e.text[index1].fmt;
          for (; index2 < this.e.text[index1].len; ++index2)
          {
            if ((int) fmt[index2] == (int) (ushort) FontId)
              goto label_16;
          }
          goto label_13;
        }
label_16:
        pLineNo = index1;
        pCol = index2;
        return true;
      }
label_13:
      ++index1;
      index2 = 0;
    }
    return false;
  }

  internal bool TerLocateStyle(int style, ref int StartLine, ref int StartCol, out int StringLen)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    StringLen = 0;
    for (int line = StartLine; line < this.e.TotalLines; ++line)
    {
      int num = line != StartLine ? 0 : StartCol;
      if (num < this.e.text[line].len && this.e.text[line].len != 0)
      {
        if (this.e.text[line].fmt == null)
        {
          if ((this.e.TerFont[(int) this.e.text[line].UniFmt].style & style) != 0)
          {
            StartLine = line;
            StartCol = num;
            StringLen = this.e.text[line].len - num;
            return true;
          }
        }
        else
        {
          ushort[] numArray = this.OpenCfmt(line);
          for (int index1 = num; index1 < this.e.text[line].len; ++index1)
          {
            if ((this.e.TerFont[(int) numArray[index1]].style & style) != 0)
            {
              int index2 = index1 + 1;
              while (index2 < this.e.text[line].len && (this.e.TerFont[(int) numArray[index2]].style & style) != 0)
                ++index2;
              StartLine = line;
              StartCol = index1;
              StringLen = index2 - index1;
              this.CloseCfmt(line);
              return true;
            }
          }
          this.CloseCfmt(line);
        }
      }
    }
    return false;
  }

  internal bool TerLocateStyleChar(
    int style,
    bool present,
    ref int StartLine,
    ref int StartCol,
    bool forward)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (forward)
    {
      for (int line = StartLine; line < this.e.TotalLines; ++line)
      {
        int num = line != StartLine ? 0 : StartCol;
        if (num < this.e.text[line].len && this.e.text[line].len != 0)
        {
          if (this.e.text[line].fmt == null)
          {
            int style1 = this.e.TerFont[(int) this.e.text[line].UniFmt].style;
            if (present && (style1 & style) != 0 || !present && (style1 & style) == 0)
            {
              StartLine = line;
              StartCol = num;
              return true;
            }
          }
          else
          {
            ushort[] numArray = this.OpenCfmt(line);
            for (int index = num; index < this.e.text[line].len; ++index)
            {
              int style2 = this.e.TerFont[(int) numArray[index]].style;
              if (present && (style2 & style) != 0 || !present && (style2 & style) == 0)
              {
                StartLine = line;
                StartCol = index;
                this.CloseCfmt(line);
                return true;
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
            int style3 = this.e.TerFont[(int) this.e.text[line].UniFmt].style;
            if (present && (style3 & style) != 0 || !present && (style3 & style) == 0)
            {
              StartLine = line;
              StartCol = num;
              return true;
            }
          }
          else
          {
            ushort[] numArray = this.OpenCfmt(line);
            for (int index = num; index >= 0; --index)
            {
              int style4 = this.e.TerFont[(int) numArray[index]].style;
              if (present && (style4 & style) != 0 || !present && (style4 & style) == 0)
              {
                StartLine = line;
                StartCol = index;
                this.CloseCfmt(line);
                return true;
              }
            }
            this.CloseCfmt(line);
          }
        }
      }
    }
    return false;
  }

  internal bool TerPosBookmark(string name, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int curLine1 = this.e.CurLine;
    int curCol = this.e.CurCol;
    if (!this.PosTag(-1, name, 1, 3, false))
      return false;
    if (repaint)
    {
      int curLine2 = this.e.CurLine;
      this.e.CurLine = curLine1;
      this.TerPosLine(curLine2 + 1);
    }
    return true;
  }

  internal bool TerPosTag(int TagId, string name, int scope, bool repaint)
  {
    return this.TerPosTagEx(0, TagId, name, scope, repaint);
  }

  internal bool TerPosTagEx(int TagType, int TagId, string name, int scope, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    return this.PosTag(TagId, name, TagType, scope, repaint);
  }

  internal bool TerSetBkColor(Color BkColor)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    this.e.TextDefBkColor = this.e.TextBorderColor = BkColor;
    this.e.Invalidate();
    return true;
  }

  internal bool TerSetCharAuxId(int AuxId, bool repaint)
  {
    tc.DgtGetNewFontId GetNewFontId = new tc.DgtGetNewFontId(this.SetCharAuxId);
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    return this.e.CurSID < 0 && this.CharFmt(GetNewFontId, AuxId, 0, (string) null, repaint);
  }

  internal bool TerSetCharLang(int lang, bool repaint)
  {
    tc.DgtGetNewFontId GetNewFontId = new tc.DgtGetNewFontId(this.GetNewLang);
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (lang == this.e.DefLang)
      lang = 0;
    return this.e.CurSID >= 0 || this.CharFmt(GetNewFontId, lang, 0, (string) null, repaint);
  }

  internal bool TerSetCharOffset(int offset, bool repaint)
  {
    tc.DgtGetNewFontId GetNewFontId = new tc.DgtGetNewFontId(this.GetNewCharOffset);
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.e.CurSID < 0)
      return this.CharFmt(GetNewFontId, offset, 0, (string) null, repaint);
    this.e.StyleId[this.e.CurSID].offset = offset;
    return true;
  }

  internal bool TerSetCharSet(byte CharSet)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    this.e.ReqCharSet = CharSet;
    return true;
  }

  internal bool TerSetCharSpace(bool dialog, int expand, bool repaint)
  {
    tc.DgtGetNewFontId GetNewFontId = new tc.DgtGetNewFontId(this.GetNewExpand);
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (dialog)
    {
      if (this.e.CurSID >= 0)
        this.e.DlgInt1 = this.e.StyleId[this.e.CurSID].expand;
      else
        this.e.DlgInt1 = this.e.TerFont[this.GetEffectiveCfmt()].expand;
      if (!this.CallDialogBox((Form) new terdlg_char_space(this.e)))
        return false;
      expand = this.e.DlgInt1;
    }
    if (this.e.CurSID < 0)
      return this.CharFmt(GetNewFontId, expand, 0, (string) null, repaint);
    this.e.StyleId[this.e.CurSID].expand = expand;
    return true;
  }

  internal bool TerSetDefLang(int lang)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    this.e.DefLang = lang;
    return true;
  }

  internal bool TerSetDefTextColor(Color ForeColor, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (ForeColor != this.e.TextDefColor)
    {
      this.e.TextDefColor = ForeColor;
      if (repaint)
      {
        this.DeleteTextMap(true);
        this.PaintTer();
      }
    }
    return true;
  }

  internal bool TerSetEffectiveFont(int NewFont)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (NewFont != -1 && (NewFont < 0 || NewFont >= this.e.TotalFonts || !this.e.TerFont[NewFont].InUse || (this.e.TerFont[NewFont].style & 128 /*0x80*/) != 0))
      NewFont = 0;
    this.e.InputFontId = NewFont;
    return true;
  }

  internal bool TerSetFontField(int font, int FieldId, string FieldCode)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (font < 0 || font >= this.e.TotalFonts || !this.e.TerFont[font].InUse)
      return false;
    if (this.e.TerFont[font].FieldId != FieldId || !this.IsSameFieldCode(this.e.TerFont[font].FieldCode, FieldCode))
    {
      this.e.TerFont[font].FieldId = FieldId;
      this.e.TerFont[font].FieldCode = FieldCode;
      ++this.e.TerArg.modified;
    }
    return true;
  }

  internal bool TerSetFontId(int FontId)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (FontId < 0 || FontId >= this.e.TotalFonts || !this.e.TerFont[FontId].InUse)
      return false;
    this.e.NextFontId = FontId;
    return true;
  }

  internal bool TerSetFontSpace(int font, int expand, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (font < 0 || font >= this.e.TotalFonts || !this.e.TerFont[font].InUse || (this.e.TerFont[font].style & 128 /*0x80*/) != 0)
      return false;
    this.e.TerFont[font].expand = expand;
    if (!this.CreateOneFont(this.e.TerGr, font, true))
      return false;
    this.RequestPagination(true);
    if (repaint)
      this.PaintTer();
    return true;
  }

  internal bool TerSetFontStyleId(int FontId, int CharStyId, int ParaStyId)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (FontId < 0 || FontId >= this.e.TotalFonts || !this.e.TerFont[FontId].InUse)
      return false;
    if (CharStyId >= 0 && CharStyId < this.e.TotalSID && this.e.StyleId[CharStyId].InUse && this.e.StyleId[CharStyId].type == 1)
      this.e.TerFont[FontId].CharStyId = CharStyId;
    if (ParaStyId >= 0 && ParaStyId < this.e.TotalSID && this.e.StyleId[ParaStyId].InUse && this.e.StyleId[ParaStyId].type == 2)
      this.e.TerFont[FontId].ParaStyId = ParaStyId;
    return true;
  }

  internal bool TerSetInitTypeface(string typeface)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    tc.InitFontFace = typeface;
    return true;
  }

  internal bool TerSetNextFontAux1Id(int Aux1Id)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    this.e.NextFontAux1Id = Aux1Id;
    return true;
  }

  internal int TerSetTag(int line, int col, string name, string AuxText, int AuxInt, int flags)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    return this.SetTag(line, col, 0, name, AuxText, AuxInt);
  }

  internal int TerSetTag(
    int line,
    int col,
    string name,
    string AuxText,
    int AuxInt,
    object obj,
    int flags)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int index = this.SetTag(line, col, 0, name, AuxText, AuxInt);
    if (index > 0)
      this.e.CharTag[index].obj = obj;
    return index;
  }

  internal bool TerSetTextCase(bool CaseType, bool repaint)
  {
    int StartIndex = 0;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.e.HilightType == 0 || !this.NormalizeBlock() || !this.NormalizeForFootnote())
      return false;
    if (this.e.HilightType == 1)
    {
      this.e.HilightBegCol = 0;
      this.e.HilightEndCol = this.e.text[this.e.HilightEndRow].len;
      if (this.e.HilightEndCol < 0)
        this.e.HilightEndCol = 0;
    }
    if (this.e.HilightBegRow == this.e.HilightEndRow)
    {
      this.ChangeLetterCase(this.e.HilightBegRow, this.e.HilightBegCol, this.e.HilightEndCol - 1, CaseType, ref StartIndex);
    }
    else
    {
      this.ChangeLetterCase(this.e.HilightBegRow, this.e.HilightBegCol, this.e.text[this.e.HilightBegRow].len - 1, CaseType, ref StartIndex);
      for (int line = this.e.HilightBegRow + 1; line < this.e.HilightEndRow; ++line)
        this.ChangeLetterCase(line, 0, this.e.text[line].len - 1, CaseType, ref StartIndex);
      this.ChangeLetterCase(this.e.HilightEndRow, 0, this.e.HilightEndCol - 1, CaseType, ref StartIndex);
    }
    ++this.e.TerArg.modified;
    if (repaint)
      this.PaintTer();
    return true;
  }

  internal bool TerSetUlineColor(bool dialog, Color color, bool repaint)
  {
    tc.DgtGetNewFontId GetNewFontId = new tc.DgtGetNewFontId(this.GetNewUlineColor);
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (dialog)
    {
      if (this.e.CurSID >= 0)
        color = this.e.StyleId[this.e.CurSID].UlineColor;
      else if (this.e.HilightType != 0)
      {
        this.NormalizeBlock();
        color = this.e.TerFont[this.GetCurCfmt(this.e.HilightBegRow, this.e.HilightBegCol)].UlineColor;
      }
      else
        color = this.e.TerFont[this.GetCurCfmt(this.e.CurLine, this.e.CurCol)].UlineColor;
      color = this.DlgEditColor((Control) this.e, color, false);
      if (this.e.DlgCancel)
        return false;
    }
    if (this.e.CurSID < 0)
      return this.CharFmt(GetNewFontId, this.ToColorRef(color), 0, (string) null, repaint);
    this.e.StyleId[this.e.CurSID].UlineColor = color;
    return true;
  }

  internal bool TerSetWaveUnderline(int LineNo, int StartCol, int EndCol, bool set, bool repaint)
  {
    int num1 = 0;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int len = this.e.text[LineNo].len;
    if (EndCol > len - 1)
      EndCol = len - 1;
    if (len >= 0)
    {
      ushort[] numArray = this.OpenCfmt(LineNo);
      int num2 = -1;
      for (int index = StartCol; index <= EndCol; ++index)
      {
        int CurFont = (int) numArray[index];
        if (CurFont != num2)
        {
          num1 = this.SetFontFlags(CurFont, 1024 /*0x0400*/, set);
          num2 = CurFont;
        }
        numArray[index] = (ushort) num1;
      }
      this.e.TerOpFlags |= 268435456 /*0x10000000*/;
      if (repaint)
        this.PaintTer();
    }
    return true;
  }

  internal bool TerShrinkFontTable()
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    lock (tc.GlbFontLock)
    {
      if (tc.GlbFont != null)
      {
        for (int index = 0; index < tc.TotalGlbFonts; ++index)
        {
          if (tc.GlbFont[index].UseCount == 0)
          {
            if (tc.GlbFont[index].font != null)
            {
              tc.GlbFont[index].font.Dispose();
              tc.GlbFont[index].font = (Font) null;
            }
            if (tc.GlbFont[index].hFont != IntPtr.Zero)
            {
              COp.Win32.DeleteObject(tc.GlbFont[index].hFont);
              tc.GlbFont[index].hFont = IntPtr.Zero;
            }
          }
        }
      }
    }
    return true;
  }

  internal new bool TransferFontId(
    bool IntoTerFont,
    int FontId,
    ref tc.StrFont font,
    ref tc.StrPrtFont pfont)
  {
    if (IntoTerFont)
    {
      this.e.TerFont[FontId].CharWidth = (int[]) null;
      this.e.PrtFont[FontId].CharWidth = (int[]) null;
      this.e.TerFont[FontId].hidden = (tc.ClsHdnFont) null;
      this.e.PrtFont[FontId].hidden = (tc.ClsHdnFont) null;
      this.e.TerFont[FontId] = font;
      this.e.PrtFont[FontId] = pfont;
    }
    else
    {
      font = this.e.TerFont[FontId];
      pfont = this.e.PrtFont[FontId];
      this.e.TerFont[FontId].CharWidth = (int[]) null;
      this.e.PrtFont[FontId].CharWidth = (int[]) null;
      this.e.TerFont[FontId].hidden = (tc.ClsHdnFont) null;
      this.e.PrtFont[FontId].hidden = (tc.ClsHdnFont) null;
      this.e.TerFont[FontId].LinkFile = (string) null;
      this.e.TerFont[FontId].InUse = false;
    }
    return true;
  }

  internal new bool TransferTags(int line, int col)
  {
    ushort[] tag;
    if ((tag = this.e.text[line].tag) != null)
    {
      int index1 = (int) tag[col];
      if (index1 == 0)
        return true;
      int line1 = line;
      int index2 = col + 1;
      if (index2 >= this.e.text[line1].len)
      {
        ++line1;
        index2 = 0;
        if (line1 >= this.e.TotalLines)
          return true;
      }
      if (index1 < this.e.CharTag.Length && (this.e.CharTag[index1].type == 78 || this.e.CharTag[index1].type == 79 || this.e.CharTag[index1].type == 80 /*0x50*/ || this.e.CharTag[index1].type == 81 || this.e.CharTag[index1].type == 77))
        return true;
      ushort[] numArray = this.OpenCtid(line1);
      int next = (int) numArray[index2];
      if (next == 0)
      {
        numArray[index2] = (ushort) index1;
      }
      else
      {
        while (this.e.CharTag[next].next > 0)
          next = this.e.CharTag[next].next;
        this.e.CharTag[next].next = index1;
        if (this.e.CheckEndlessLoopTags(next))
          this.e.CharTag[next].next = 0;
      }
      tag[col] = (ushort) 0;
      this.CloseCtid(line1);
    }
    return true;
  }

  internal new bool UpdateTagTable()
  {
    bool[] flagArray = new bool[this.e.TotalCharTags + 1];
    for (int index = 0; index < this.e.TotalCharTags; ++index)
      flagArray[index] = false;
    for (int index1 = 0; index1 < this.e.TotalLines; ++index1)
    {
      if (this.e.text[index1].tag != null)
      {
        ushort[] tag = this.e.text[index1].tag;
        int len = this.e.text[index1].len;
        for (int index2 = 0; index2 < len; ++index2)
        {
          for (int next = (int) tag[index2]; next != 0; next = this.e.CharTag[next].next)
            flagArray[next] = true;
        }
      }
    }
    for (int TagId = 1; TagId < this.e.TotalCharTags; ++TagId)
    {
      if (!flagArray[TagId] && this.e.CharTag[TagId].InUse)
        this.FreeTag(TagId);
    }
    return true;
  }
}
