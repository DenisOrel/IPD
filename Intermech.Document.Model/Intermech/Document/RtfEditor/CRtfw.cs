// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.CRtfw
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class CRtfw : COp
{
  internal CRtfw(ImRtfEditor ImRtfEditor)
    : base(ImRtfEditor)
  {
    this.e = ImRtfEditor;
  }

  internal string AppendRtfHlink(tc.ClsRtfOut rtf, string code)
  {
    string str = "";
    int num1 = code.ToUpper().IndexOf("HREF=");
    if (!this.e.HtmlMode || num1 < 0)
      return str + code;
    int index1 = num1 + 5;
    int length = code.Length;
    while (code[index1] == ' ' && index1 < length)
      ++index1;
    if (index1 >= length)
      return str + code;
    char ch = code[index1];
    int startIndex = index1;
    int index2;
    for (index2 = startIndex + 1; index2 < length; ++index2)
    {
      if (ch == '\'' || ch == '"')
      {
        if ((int) code[index2] == (int) ch)
        {
          ++index2;
          break;
        }
      }
      else if (code[index2] == ' ')
        break;
    }
    int num2 = index2;
    return str + code.Substring(startIndex, num2 - startIndex);
  }

  internal bool BeginRtfFieldName(tc.ClsRtfOut rtf, int PrevFieldId, int CurFont)
  {
    if ((PrevFieldId == 7 || PrevFieldId == 6) && (!this.EndRtfGroup(rtf) || !this.EndRtfGroup(rtf)) || !this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "field", 0, 0.0) || !this.WriteRtfControl(rtf, "fldlock", 0, 0.0) || !this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "*", 0, 0.0) || !this.WriteRtfControl(rtf, "fldinst", 0, 0.0))
      return false;
    string text = "MERGEFIELD ";
    this.WriteRtfText(rtf, text, text.Length);
    rtf.flags |= 512 /*0x0200*/;
    if (CurFont >= 0)
    {
      tc.StrRtfOutGroup strRtfOutGroup = rtf.group[rtf.GroupLevel] with
      {
        style = this.e.TerFont[CurFont].style,
        FieldId = this.e.TerFont[CurFont].FieldId,
        FieldCode = this.e.TerFont[CurFont].FieldCode
      };
      rtf.group[rtf.GroupLevel] = strRtfOutGroup;
    }
    return true;
  }

  internal bool BeginRtfGroup(tc.ClsRtfOut rtf)
  {
    rtf.SpacePending = false;
    if (rtf.GroupLevel + 1 < 50)
    {
      ++rtf.GroupLevel;
      rtf.group[rtf.GroupLevel] = rtf.group[rtf.GroupLevel - 1];
    }
    return this.PutRtfChar(rtf, '{');
  }

  internal bool CellCharIncluded(int line1, int col1, int line2, int col2, int level)
  {
    if (this.e.HilightType == 0)
      return true;
    for (int index = line1; index <= line2; ++index)
    {
      int num = index != line2 ? this.e.text[index].len : col2;
      if (num > 0 && (int) this.e.text[index].txt[num - 1] == (int) this.e.CellChar && (level < 0 || this.e.cell[this.e.text[index].cid].level == level))
        return true;
    }
    return false;
  }

  internal bool EndInterParaGroups(tc.ClsRtfOut rtf, int NewFont)
  {
    int fieldId = rtf.group[rtf.GroupLevel].FieldId;
    if ((rtf.flags & 4) != 0)
    {
      if (!this.EndRtfGroup(rtf))
        return false;
      rtf.flags = tc.ResetUintFlag(ref rtf.flags, 4);
    }
    if ((rtf.flags & 8) != 0)
    {
      if (!this.EndRtfGroup(rtf))
        return false;
      rtf.flags = tc.ResetUintFlag(ref rtf.flags, 8);
    }
    if ((rtf.flags & 512 /*0x0200*/) != 0)
    {
      if (!this.EndRtfGroup(rtf) || !this.EndRtfGroup(rtf))
        return false;
      tc.ResetUintFlag(ref rtf.flags, 512 /*0x0200*/);
    }
    return fieldId != 7 || this.e.TerFont[NewFont].FieldId == 7 || this.EndRtfGroup(rtf) && this.EndRtfGroup(rtf);
  }

  internal bool EndRtfGroup(tc.ClsRtfOut rtf)
  {
    rtf.SpacePending = false;
    if (rtf.GroupLevel > 0)
      --rtf.GroupLevel;
    return this.PutRtfChar(rtf, '}');
  }

  internal bool FlushRtfLine(tc.ClsRtfOut rtf)
  {
    if (rtf.TextLen != 0)
    {
      if ((rtf.flags & 2048 /*0x0800*/) == 0)
      {
        rtf.text[rtf.TextLen] = '\r';
        ++rtf.TextLen;
        rtf.text[rtf.TextLen] = '\n';
        ++rtf.TextLen;
      }
      if (rtf.oFile != null)
      {
        try
        {
          rtf.oFile.Write(rtf.text, 0, rtf.TextLen);
        }
        catch (Exception ex)
        {
          return this.PrintError(31 /*0x1F*/, nameof (FlushRtfLine));
        }
      }
      else
      {
        if (rtf.BufIndex + rtf.TextLen + 1 > rtf.BufLen)
        {
          rtf.BufLen += rtf.BufLen / 4;
          if (rtf.BufLen < rtf.BufIndex + rtf.TextLen + 1)
            rtf.BufLen = rtf.BufIndex + rtf.TextLen + 1;
          rtf.buf = this.ReAlloc(rtf.buf, rtf.BufLen);
        }
        for (int index = 0; index < rtf.TextLen; ++index)
          rtf.buf[rtf.BufIndex + index] = rtf.text[index];
        rtf.BufIndex += rtf.TextLen;
      }
      rtf.TextLen = 0;
      rtf.SpacePending = false;
    }
    return true;
  }

  internal uint GetRtfTrackingTime(tc.ClsDateTime dt)
  {
    if (dt == null)
    {
      dt = new tc.ClsDateTime();
      dt.dt = DateTime.Now;
    }
    return (uint) (dt.dt.Minute | dt.dt.Hour << 6 | dt.dt.Day << 11 | dt.dt.Month << 16 /*0x10*/ | dt.dt.Year - 1900 << 20 | (int) dt.dt.DayOfWeek << 29);
  }

  internal bool OldWriteRtfDoInfo(tc.ClsRtfOut rtf, int CurParaFID, int ParaFlags, int NewFID)
  {
    if ((this.e.ParaFrame[CurParaFID].flags & 768 /*0x0300*/) != 0 && (ParaFlags & 32768 /*0x8000*/) != 0)
      this.WriteRtfControl(rtf, "keepn", 0, 0.0);
    this.FlushRtfLine(rtf);
    if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "do", 0, 0.0) || (this.e.ParaFrame[CurParaFID].flags & 1) == 0 && !this.WriteRtfControl(rtf, "dobxmargin", 0, 0.0) || (this.e.ParaFrame[CurParaFID].flags & 1) != 0 && !this.WriteRtfControl(rtf, "dobxpage", 0, 0.0) || (this.e.ParaFrame[CurParaFID].flags & 96 /*0x60*/) == 0 && !this.WriteRtfControl(rtf, "dobypara", 0, 0.0) || (this.e.ParaFrame[CurParaFID].flags & 64 /*0x40*/) != 0 && !this.WriteRtfControl(rtf, "dobymargin", 0, 0.0) || (this.e.ParaFrame[CurParaFID].flags & 32 /*0x20*/) != 0 && !this.WriteRtfControl(rtf, "dobypage", 0, 0.0))
      return false;
    int val = this.e.ParaFrame[CurParaFID].ZOrder;
    if (val < 0)
      val = 0;
    if (!this.WriteRtfControl(rtf, "dodhgt", 1, (double) val))
      return false;
    if ((this.e.ParaFrame[CurParaFID].flags & 128 /*0x80*/) != 0)
    {
      if (!this.WriteRtfControl(rtf, "dptxbx", 0, 0.0) || !this.WriteRtfControl(rtf, "dptxbxmar", 1, (double) this.e.ParaFrame[CurParaFID].margin) || this.e.ParaFrame[CurParaFID].TextAngle == 90 && !this.WriteRtfControl(rtf, "dptxbtlr", 0, 0.0) || this.e.ParaFrame[CurParaFID].TextAngle == 270 && !this.WriteRtfControl(rtf, "dptxtbrl", 0, 0.0) || !this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "dptxbxtext", 0, 0.0))
        return false;
    }
    else if ((this.e.ParaFrame[CurParaFID].flags & 256 /*0x0100*/) != 0)
    {
      if (!this.WriteRtfControl(rtf, "dpline", 0, 0.0))
        return false;
      this.WritePfObjectTail(rtf, NewFID);
    }
    else if ((this.e.ParaFrame[CurParaFID].flags & 512 /*0x0200*/) != 0)
    {
      if (!this.WriteRtfControl(rtf, "dprect", 0, 0.0))
        return false;
      this.WritePfObjectTail(rtf, NewFID);
    }
    return true;
  }

  internal bool ParaCharIncluded(int line1, int col1, int line2, int col2)
  {
    if (this.e.HilightType == 0)
      return true;
    for (int index = line1; index <= line2; ++index)
    {
      int num = index != line2 ? this.e.text[index].len : col2;
      if (num > 0 && (int) this.e.text[index].txt[num - 1] == (int) this.e.ParaChar)
        return true;
    }
    return false;
  }

  internal bool PutRtfChar(tc.ClsRtfOut rtf, char CurChar)
  {
    bool flag = (rtf.flags & 2) != 0;
    if (rtf.SpacePending)
    {
      rtf.text[rtf.TextLen] = ' ';
      ++rtf.TextLen;
      rtf.SpacePending = false;
    }
    if (!rtf.WritingControl && (rtf.flags & 32 /*0x20*/) == 0 && (rtf.TextLen > 250 && rtf.text[rtf.TextLen - 1] == ' ' && !this.FlushRtfLine(rtf) || rtf.TextLen > 333 && !this.FlushRtfLine(rtf)) || CurChar == '\\' && !flag && rtf.TextLen > 500 && !this.FlushRtfLine(rtf) || rtf.TextLen + 2 > 1000 && !this.FlushRtfLine(rtf))
      return false;
    if (CurChar >= char.MinValue && CurChar <= 'À')
    {
      if (CurChar < '\u0080')
      {
        rtf.text[rtf.TextLen] = CurChar;
        ++rtf.TextLen;
      }
      else
        this.PutRtfSpecChar(rtf, CurChar);
    }
    else if (!this.WriteRtfControl(rtf, "u", 1, (double) CurChar) || !this.e.BlockQuestionMarkAfterUnicode && !this.WriteRtfText(rtf, "?", 1))
      return false;
    return true;
  }

  internal bool PutRtfHexChar(tc.ClsRtfOut rtf, char CurChar)
  {
    char ch1 = (char) (((int) CurChar & 240 /*0xF0*/) >> 4);
    char ch2 = (char) ((uint) CurChar & 15U);
    char CurChar1 = ch1 > '\t' ? (char) (97 + (int) ch1 - 10) : (char) (48U /*0x30*/ + (uint) ch1);
    if (!this.PutRtfChar(rtf, CurChar1))
      return false;
    char CurChar2 = ch2 > '\t' ? (char) (97 + (int) ch2 - 10) : (char) (48U /*0x30*/ + (uint) ch2);
    bool writingControl = rtf.WritingControl;
    rtf.WritingControl = true;
    if (!this.PutRtfChar(rtf, CurChar2))
      return false;
    rtf.WritingControl = writingControl;
    return true;
  }

  internal bool PutRtfSpecChar(tc.ClsRtfOut rtf, char CurChar)
  {
    if (!this.PutRtfChar(rtf, '\\'))
      return false;
    rtf.flags |= 32 /*0x20*/;
    if (!this.PutRtfChar(rtf, '\'') || !this.PutRtfHexChar(rtf, CurChar))
      return false;
    rtf.flags = tc.ResetUintFlag(ref rtf.flags, 32 /*0x20*/);
    return true;
  }

  internal bool ResetRtfFont(tc.ClsRtfOut rtf)
  {
    tc.StrRtfOutGroup strRtfOutGroup = rtf.group[rtf.GroupLevel];
    if (!this.WriteRtfControl(rtf, "plain", 0, 0.0) || !this.WriteRtfControl(rtf, "f", 1, (double) this.e.TerFont[0].RtfIndex) || !this.WriteRtfControl(rtf, "fs", 1, (double) (this.e.TerFont[0].TwipsSize / 10)))
      return false;
    strRtfOutGroup.FontId = 0;
    rtf.group[rtf.GroupLevel] = strRtfOutGroup;
    return true;
  }

  internal new bool RtfWrite(int output, string OutFile, out string OutData)
  {
    StreamWriter streamWriter = (StreamWriter) null;
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    Cursor x = (Cursor) null;
    bool flag4 = false;
    bool flag5 = false;
    bool flag6 = (this.e.TerFlags5 & 268435456 /*0x10000000*/) != 0;
    int num1 = -1;
    OutData = (string) null;
    this.e.RtfInHdrFtr = 0;
    this.e.RtfInTable = false;
    this.e.RtfClipData = (char[]) null;
    this.e.ClipInfo = (tc.ClsClipInfo) null;
    if (output == 0)
    {
      OutFile = OutFile.Trim();
      if (OutFile.Length == 0)
        return false;
      if (File.Exists(OutFile))
        File.Delete(OutFile);
      try
      {
        streamWriter = new StreamWriter(OutFile, false, Encoding.ASCII);
      }
      catch (Exception ex)
      {
        return false;
      }
    }
    int index1;
    int index2;
    int col1;
    int col2;
    if (output >= 2)
    {
      index1 = this.e.HilightBegRow;
      index2 = this.e.HilightEndRow;
      col1 = 0;
      col2 = this.e.text[index2].len;
      if (this.e.HilightType == 2)
      {
        col1 = this.e.HilightBegCol;
        col2 = this.e.HilightEndCol;
      }
      if ((this.e.TerFlags3 & 256 /*0x0100*/) != 0 && this.e.text[index1].cid > 0 && this.e.text[index1].cid == this.e.text[index2].cid && this.LineInfo(index2, 16 /*0x10*/) && col2 >= this.e.text[index2].len && col2 > 0)
      {
        flag5 = true;
        --col2;
        flag4 = true;
      }
    }
    else
    {
      if (this.e.TotalLines > 0 && (this.e.text[this.e.TotalLines - 1].fid > 0 || this.e.text[this.e.TotalLines - 1].cid > 0) && !this.e.RotatedFrame)
      {
        ++this.e.TotalLines;
        int index3 = this.e.TotalLines - 1;
        this.InitLine(index3);
        this.LineAlloc(index3, 0, 1);
        this.e.text[index3].txt[0] = this.e.ParaChar;
        this.OpenCfmt(index3)[0] = (ushort) 0;
        this.CloseCfmt(index3);
        this.e.text[index3].pfmt = this.e.text[index3 - 1].pfmt;
        this.e.text[index3].fid = 0;
        this.e.text[index3].cid = 0;
      }
      index1 = 0;
      col1 = 0;
      index2 = this.e.TotalLines - 1;
      col2 = this.e.text[index2].len;
    }
    tc.ClsRtfOut rtf = new tc.ClsRtfOut();
    rtf.text = new char[1001];
    rtf.group = new tc.StrRtfOutGroup[51];
    rtf.XlateLs = new int[5000];
    rtf.output = output;
    if (rtf.output == 0)
    {
      rtf.oFile = streamWriter;
    }
    else
    {
      rtf.BufLen = 0;
      for (int index4 = index1; index4 <= index2; ++index4)
        rtf.BufLen = rtf.BufLen + this.e.text[index4].len + 2;
      rtf.BufLen = rtf.BufLen * 3 / 2;
      if (rtf.BufLen <= 0)
        rtf.BufLen = 1;
      rtf.buf = new char[rtf.BufLen];
      rtf.BufIndex = 0;
      rtf.oFile = (StreamWriter) null;
      if (rtf.output >= 2 && this.e.HilightType == 2 && !flag4)
        rtf.TblHilight = this.TableHilighted();
    }
    this.e.ClipTblLevel = 1;
    this.e.ClipEmbTable = false;
    if (output >= 2)
    {
      if (!rtf.TblHilight || this.e.HilightType != 2)
      {
        this.e.ClipTblLevel = 0;
        this.e.ClipEmbTable = true;
      }
      else
      {
        int cid1 = this.e.text[this.e.HilightBegRow].cid;
        int cid2 = this.e.text[this.e.HilightEndRow].cid;
        int level = this.e.cell[cid1].level < this.e.cell[cid2].level ? this.e.cell[cid1].level : this.e.cell[cid2].level;
        bool flag7 = false;
        int num2 = this.LevelCell(level, -cid1);
        int num3 = this.LevelCell(level, -cid2);
        if (num2 != num3)
          this.e.ClipTblLevel = level + 1;
        else if (this.CellCharIncluded(index1, col1, index2, col2, level))
          this.e.ClipTblLevel = level + 1;
        if (this.e.text[this.e.HilightBegRow].cid > 0 && this.e.text[this.e.HilightBegRow].cid == this.e.text[this.e.HilightEndRow].cid && (flag5 || this.LineInfo(this.e.HilightEndRow, 16 /*0x10*/)) && (this.e.HilightBegRow == 0 || this.e.text[this.e.HilightBegRow - 1].cid != this.e.text[this.e.HilightBegRow].cid))
          flag7 = true;
        if (!flag7 && num2 == num3)
        {
          bool flag8 = false;
          int num4 = 99;
          int num5 = 0;
          for (int index5 = index1; index5 <= index2; ++index5)
          {
            int cid3;
            if ((cid3 = this.e.text[index5].cid) == 0)
            {
              flag8 = true;
            }
            else
            {
              if (this.e.cell[cid3].level < num4)
                num4 = this.e.cell[cid3].level;
              if (this.e.cell[cid3].level > num5)
                num5 = this.e.cell[cid3].level;
              if (num4 < num5 | flag8)
              {
                this.e.ClipEmbTable = true;
                break;
              }
            }
          }
        }
      }
    }
    if (this.e.HilightType != 0)
    {
      for (int LineNo = index1; LineNo <= index2; ++LineNo)
      {
        if (this.LineSelected(LineNo))
          this.e.text[LineNo].flags |= 268435456 /*0x10000000*/;
        else
          this.e.text[LineNo].flags &= -268435457 /*0xEFFFFFFF*/;
      }
    }
    tc.StrRtfColor[] strRtfColorArray = rtf.color = new tc.StrRtfColor[this.e.MaxRtfColors + 1];
    for (int index6 = 0; index6 < this.e.MaxRtfColors; ++index6)
      strRtfColorArray[index6].color = Color.Black;
    if ((this.e.TerFlags & 512 /*0x0200*/) == 0)
    {
      x = this.e.Cursor;
      this.e.Cursor = Cursors.WaitCursor;
    }
    if (rtf.TblHilight)
    {
      this.SetCellLines();
      for (int index7 = 0; index7 < this.e.TotalCells; ++index7)
        this.e.cell[index7].flags = tc.ResetFlag(this.e.cell[index7].flags, 2048 /*0x0800*/);
      for (int index8 = index1; index8 <= index2; ++index8)
      {
        if ((this.e.text[index8].flags & 268435456 /*0x10000000*/) != 0)
          this.e.cell[this.e.text[index8].cid].flags |= 2048 /*0x0800*/;
      }
      int cid4 = this.e.text[index1].cid;
      if (cid4 > 0 && this.e.cell[cid4].PrevCell <= 0)
      {
        int cid5 = this.e.text[index2].cid;
        if (cid5 > 0 && this.e.cell[cid5].NextCell <= 0 && this.LineInfo(index2, 48 /*0x30*/) && col2 >= this.e.text[index2].len - 1)
          flag3 = true;
      }
    }
    List<bool> realUsedFonts = new List<bool>((IEnumerable<bool>) new bool[this.e.TotalFonts]);
    List<bool> realUsedStyles = new List<bool>((IEnumerable<bool>) new bool[this.e.TotalSID]);
    List<bool> realUsedParaFormats = new List<bool>((IEnumerable<bool>) new bool[this.e.TotalPfmts]);
    if (this.e.ShortRtf)
    {
      for (int index9 = 0; index9 < this.e.TotalLines; ++index9)
      {
        for (int index10 = 0; index10 < this.e.text[index9].len; ++index10)
        {
          int index11 = this.e.text[index9].fmt != null ? (int) this.e.text[index9].fmt[index10] : (int) this.e.text[index9].UniFmt;
          if (index11 >= 0 && index11 < realUsedFonts.Count && (!this.fnt.FontsIsEqual(this.e.TerFont[index11], this.e.TerFont[0]) || this.e.TerFont[index11].TypeFace == "Symbol" || this.e.TerFont[index11].TypeFace == "Wingdings"))
          {
            realUsedFonts[index11] = true;
            int style = this.e.TerFont[index11].style;
            if (style >= 0 && style < realUsedStyles.Count)
              realUsedStyles[style] = true;
          }
        }
        int pfmt = this.e.text[index9].pfmt;
        if (pfmt >= 0 && pfmt < realUsedParaFormats.Count)
        {
          realUsedParaFormats[pfmt] = true;
          int styId = this.e.PfmtId[pfmt].StyId;
          if (styId >= 0 && styId < realUsedStyles.Count)
            realUsedStyles[styId] = true;
        }
      }
    }
    if (this.BeginRtfGroup(rtf) && this.WriteRtfControl(rtf, "rtf", 1, 1.0) && (this.e.ShortRtf || this.WriteRtfControl(rtf, "ansi", 0, 0.0)) && (this.e.ShortRtf || this.WriteRtfControl(rtf, "deflang", 1, (double) this.e.DefLang)) && (this.e.ShortRtf || this.WriteRtfControl(rtf, "ftnbj", 0, 0.0)) && (this.e.ShortRtf || this.WriteRtfControl(rtf, "uc", 1, 1.0)) && this.WriteRtfFont(rtf, realUsedFonts, realUsedStyles) && this.WriteRtfColor(rtf, realUsedFonts, realUsedStyles, realUsedParaFormats) && this.WriteRtfStylesheet(rtf, realUsedStyles) && (this.e.ShortRtf || this.WriteRtfRev(rtf)) && this.WriteRtfList(rtf))
    {
      Color BkColor = (this.e.TerFlags6 & 16384 /*0x4000*/) != 0 ? this.e.TextDefBkColor : this.e.PageBkColor;
      if ((BkColor == tc.CLR_WHITE || this.e.HtmlMode && BkColor == this.ToColor(12632256 /*0xC0C0C0*/) || this.WriteRtfBackground(rtf, BkColor)) && (this.e.ShortRtf || this.WriteRtfMargin(rtf)) && ((this.e.TerOpFlags & 1) == 0 || this.WriteRtfControl(rtf, "ssdelcell", 0, 0.0)) && (!flag3 || this.WriteRtfControl(rtf, "sswholetablerows", 0, 0.0)))
      {
        int section1 = this.GetSection(index1);
        if (this.e.ShortRtf && this.e.TotalSects == 1 || this.WriteRtfSection(rtf, section1))
        {
          int CurFont1 = 0;
          rtf.group[rtf.GroupLevel].FontId = -1;
          if (!this.e.ShortRtf && !this.WriteRtfControl(rtf, "plain", 0, 0.0))
            return false;
          if (this.WriteRtfCharFmt(rtf, CurFont1, false))
          {
            int PrevPfmt = this.e.text[index1].pfmt;
            int PrevCell = this.e.text[index1].cid;
            int PrevFID = this.e.text[index1].fid;
            if (this.e.text[index1].cid > 0)
            {
              PrevPfmt = 0;
              PrevCell = 0;
              if (rtf.TblHilight || this.CellCharIncluded(index1, col1, index2, col2, -1))
                rtf.flags |= 16 /*0x10*/;
            }
            else if (this.ParaCharIncluded(index1, col1, index2, col2))
              PrevPfmt = 0;
            if (PrevFID > 0)
            {
              int index12 = index2;
              if (col2 == this.e.text[index2].len && index12 + 1 < this.e.TotalLines)
                ++index12;
              if (flag6 || this.e.text[index1].fid != this.e.text[index12].fid)
                PrevFID = 0;
            }
            if (this.e.HilightType == 0)
            {
              this.e.RtfInitLevel = 0;
            }
            else
            {
              this.e.RtfInitLevel = this.MinTableLevel(index1, index2);
              if (!this.CellCharIncluded(index1, col1, index2, col2, this.e.RtfInitLevel))
                ++this.e.RtfInitLevel;
            }
            this.e.RtfCurLevel = this.e.RtfInitLevel;
            int index13;
            for (index13 = index1; index13 <= index2; ++index13)
            {
              rtf.line = index13;
              if ((this.e.PfmtId[this.e.text[index13].pfmt].pflags & 128 /*0x80*/) == 0 && (!rtf.TblHilight || (this.e.text[index13].flags & 268435456 /*0x10000000*/) != 0 || this.LineInfo(index13, 32 /*0x20*/) && (!this.True(this.e.text[index13].cid) || this.TableLevel(index13) <= this.e.RtfInitLevel)))
              {
                this.e.RtfPrevLevel = this.e.RtfCurLevel;
                this.e.RtfCurLevel = this.TableLevel(index13);
                if (this.e.text[index13].len == 1 && (this.e.text[index13].flags & 1966080 /*0x1E0000*/) != 0)
                {
                  if (this.False(this.e.RtfInHdrFtr))
                  {
                    bool flag9 = index13 + 2 < this.e.TotalLines && (this.e.text[index13 + 2].flags & 1966080 /*0x1E0000*/) != 0 && this.e.text[index13 + 1].len == 1;
                    if (flag9 && this.e.text[index13 + 1].tag != null)
                      flag9 = false;
                    if (flag9)
                    {
                      index13 += 2;
                    }
                    else
                    {
                      this.FlushRtfLine(rtf);
                      if (this.BeginRtfGroup(rtf))
                      {
                        this.e.RtfInHdrFtr = 12288 /*0x3000*/;
                        if ((this.e.text[index13].flags & 524288 /*0x080000*/) != 0)
                        {
                          if (!this.WriteRtfControl(rtf, "header", 0, 0.0) || this.e.WmParaFID > 0 && !rtf.WatermarkWritten && output < 2 && !this.WriteRtfWatermark(rtf, false))
                            goto label_206;
                        }
                        else if ((this.e.text[index13].flags & 1048576 /*0x100000*/) != 0 && !this.WriteRtfControl(rtf, "footer", 0, 0.0) || (this.e.text[index13].flags & 131072 /*0x020000*/) != 0 && !this.WriteRtfControl(rtf, "headerf", 0, 0.0) || (this.e.text[index13].flags & 262144 /*0x040000*/) != 0 && !this.WriteRtfControl(rtf, "footerf", 0, 0.0))
                          goto label_206;
                        rtf.flags |= 16384 /*0x4000*/;
                      }
                      else
                        goto label_206;
                    }
                  }
                  else
                  {
                    int CurFont2 = 0;
                    if (this.WriteRtfCharFmt(rtf, CurFont2, false) && this.EndRtfGroup(rtf))
                    {
                      this.FlushRtfLine(rtf);
                      this.e.RtfInHdrFtr = 0;
                      PrevPfmt = -1;
                    }
                    else
                      goto label_206;
                  }
                }
                else if (this.e.RtfInHdrFtr != 0 || this.e.WmParaFID <= 0 || rtf.WatermarkWritten || output >= 2 || this.WriteRtfWatermark(rtf, true))
                {
                  int index14 = 0;
                  int num6 = this.e.text[index13].len;
                  if (index13 == index1)
                    index14 = col1;
                  if (index13 == index2)
                    num6 = col2;
                  int fid = flag6 ? 0 : this.e.text[index13].fid;
                  if (this.e.text[index13].pfmt != PrevPfmt || this.e.text[index13].cid != PrevCell || fid != PrevFID || index13 == num1 + 1)
                  {
                    this.EndInterParaGroups(rtf, this.GetCurCfmt(index13, 0));
                    if (this.WriteRtfParaFmt(rtf, this.e.text[index13].pfmt, PrevPfmt, this.e.text[index13].cid, PrevCell, fid, PrevFID, index13))
                    {
                      PrevPfmt = this.e.text[index13].pfmt;
                      PrevCell = this.e.text[index13].cid;
                      PrevFID = fid;
                      if (rtf.group[rtf.GroupLevel].FieldId == 0 && !this.WriteRtfCharFmt(rtf, 0, false))
                        goto label_206;
                    }
                    else
                      goto label_206;
                  }
                  if (index14 < num6)
                  {
                    char[] txt = this.e.text[index13].txt;
                    ushort[] numArray = this.OpenCfmt(index13);
                    int index15 = (int) numArray[index14];
                    int TextLen = 0;
                    if ((this.e.text[index13].flags2 & 512 /*0x0200*/) != 0 && num6 > 0 && num6 - 1 > index14 && txt[num6 - 1] == '\u0006')
                      --num6;
                    for (int index16 = index14; index16 <= num6; ++index16)
                    {
                      int index17 = this.e.text[index13].tag == null || index16 >= num6 ? 0 : (int) this.e.text[index13].tag[index16];
                      if (index17 > 0 && index17 < this.e.TotalCharTags && this.e.CharTag[index17].InUse)
                      {
                        ref tc.StrCharTag local = ref this.e.CharTag[index17];
                      }
                      if (index17 != 0 && index16 == index14)
                        this.WriteRtfTag(rtf, index17);
                      bool flag10 = index16 < num6 && this.e.TerFont[index15].FieldId == 6 && this.e.TerFont[(int) numArray[index16]].FieldId == 6 && (this.e.TerFont[(int) numArray[index16]].style & 512 /*0x0200*/) != 0 && txt[index16] == '{';
                      if (index16 == num6 || (int) numArray[index16] != (int) (ushort) index15 || (int) txt[index16] == (int) this.e.ParaChar || (int) txt[index16] == (int) this.e.CellChar || txt[index16] == '\u000F' || txt[index16] == '\u0012' || txt[index16] == '\u0014' || (this.e.TerFont[(int) numArray[index16]].style & 128 /*0x80*/) != 0 || ((!this.True(index17) ? 0 : (index16 > index14 ? 1 : 0)) | (flag10 ? 1 : 0)) != 0)
                      {
                        int idx = index16 - TextLen;
                        if (this.e.TerFont[index15].FieldId == 6 && (this.e.TerFont[index15].style & 512 /*0x0200*/) != 0)
                        {
                          if (txt[idx] == '{')
                          {
                            --TextLen;
                            ++idx;
                          }
                          if (index16 > 0 && txt[index16 - 1] == '}')
                            --TextLen;
                        }
                        if (flag10 && (rtf.flags & 512 /*0x0200*/) != 0)
                          this.BeginRtfFieldName(rtf, 6, index15);
                        if (TextLen > 0)
                        {
                          if (this.WriteRtfCharFmt(rtf, index15, false) && ((rtf.flags & 256 /*0x0100*/) != 0 || rtf.DelRevCount > 0 || this.WriteRtfText(rtf, this.CopyArray(txt, idx), TextLen)))
                            tc.ResetUintFlag(ref rtf.flags, 1024 /*0x0400*/);
                          else
                            goto label_206;
                        }
                        if (index16 != num6)
                          TextLen = 0;
                        else
                          break;
                      }
                      if (this.True(index17) && index16 > index14)
                        this.WriteRtfTag(rtf, index17);
                      index15 = (int) numArray[index16];
                      if ((this.e.TerFont[index15].style & 128 /*0x80*/) != 0)
                      {
                        if (this.e.TerFont[index15].FieldId == 2)
                        {
                          int fontId = this.e.TerFont[index15].form.FontId;
                          Color textBkColor = this.e.TerFont[fontId].TextBkColor;
                          this.e.TerFont[fontId].TextBkColor = tc.CLR_WHITE;
                          if (this.WriteRtfCharFmt(rtf, fontId, false))
                            this.e.TerFont[fontId].TextBkColor = textBkColor;
                          else
                            goto label_206;
                        }
                        else if (!this.WriteRtfCharFmt(rtf, index15, false))
                          goto label_206;
                        flag1 = true;
                        if (!(output != 4 ? (this.e.TerFont[index15].ParaFID <= 0 ? (this.e.TerFont[index15].ObjectType == 0 || this.e.TerFont[index15].ObjectType == 3 ? this.WriteRtfPicture(rtf, index15) : this.WriteRtfObject(rtf, index15)) : this.WriteRtfShape(rtf, index15)) : this.WriteRtfControl(rtf, "subpictid", 1, (double) index15)))
                          goto label_206;
                      }
                      else if ((int) txt[index16] == (int) this.e.ParaChar)
                      {
                        int index18 = PrevFID;
                        if (this.WriteRtfCharFmt(rtf, index15, false))
                        {
                          if ((index18 == 0 || (this.e.ParaFrame[index18].flags & 768 /*0x0300*/) == 0) && (rtf.flags & 256 /*0x0100*/) == 0)
                          {
                            if (!rtf.ParaFmtOnParaEnd || this.WriteRtfParaFmt(rtf, this.e.text[index13].pfmt, 0, this.e.text[index13].cid, PrevCell, fid, PrevFID, index13))
                            {
                              rtf.ParaFmtOnParaEnd = false;
                              if (!this.WriteRtfControl(rtf, "par", 0, 0.0))
                                goto label_206;
                            }
                            else
                              goto label_206;
                          }
                          rtf.FieldHasPara = true;
                        }
                        else
                          goto label_206;
                      }
                      else if ((int) txt[index16] == (int) this.e.CellChar || txt[index16] == '\u000F')
                      {
                        if (this.WriteRtfCharFmt(rtf, index15, false))
                        {
                          if ((int) txt[index16] == (int) this.e.CellChar)
                          {
                            int colSpan = this.e.cell[this.e.text[index13].cid].ColSpan;
                            int level = this.e.cell[this.e.text[index13].cid].level;
                            if (!rtf.ParaFmtOnParaEnd || this.WriteRtfParaFmt(rtf, this.e.text[index13].pfmt, 0, this.e.text[index13].cid, PrevCell, fid, PrevFID, index13))
                            {
                              rtf.ParaFmtOnParaEnd = false;
                              string control = level == this.e.RtfInitLevel ? "cell" : "nestcell";
                              if (this.WriteRtfControl(rtf, control, 0, 0.0))
                              {
                                if (level == this.e.RtfInitLevel)
                                {
                                  for (; colSpan > 1; --colSpan)
                                  {
                                    if (this.WriteRtfControl(rtf, control, 0, 0.0))
                                    {
                                      if (level > this.e.RtfInitLevel)
                                        this.WriteRtfNoNestGroup(rtf);
                                    }
                                    else
                                      goto label_206;
                                  }
                                }
                                else
                                  this.WriteRtfNoNestGroup(rtf);
                                rtf.flags |= 1024 /*0x0400*/;
                                this.FlushRtfLine(rtf);
                              }
                              else
                                goto label_206;
                            }
                            else
                              goto label_206;
                          }
                          if (txt[index16] == '\u000F' && this.e.TerFont[index15].CharId > 0 && !this.WriteRtfControl(rtf, "lbr", 1, (double) this.e.TerFont[index15].CharId) || txt[index16] == '\u000F' && !this.WriteRtfControl(rtf, "line", 0, 0.0))
                            goto label_206;
                        }
                        else
                          goto label_206;
                      }
                      else if (txt[index16] == '\u0012')
                      {
                        int num7 = this.TableLevel(index13);
                        if (this.WriteRtfCharFmt(rtf, index15, false) && this.WriteRtfControl(rtf, "intbl", 0, 0.0))
                        {
                          if (num7 == this.e.RtfInitLevel)
                          {
                            if (this.WriteRtfControl(rtf, "row", 0, 0.0))
                            {
                              this.FlushRtfLine(rtf);
                              this.e.RtfInTable = false;
                            }
                            else
                              goto label_206;
                          }
                          else if (!this.WriteRtfControl(rtf, "itap", 1, (double) (num7 - this.e.RtfInitLevel + 1)))
                            goto label_206;
                          tc.ResetUintFlag(ref rtf.flags, 1024 /*0x0400*/);
                        }
                        else
                          goto label_206;
                      }
                      else if (txt[index16] == '\u0014')
                      {
                        if (this.True(this.e.text[index13].tabw) && (this.e.text[index13].tabw.type & 2) != 0)
                        {
                          if (this.WriteRtfCharFmt(rtf, index15, true) && (index13 <= index1 || this.e.text[index13 - 1].cid <= 0 || this.WriteRtfControl(rtf, "pard", 0, 0.0)) && this.WriteRtfControl(rtf, "sect", 0, 0.0))
                          {
                            int section2 = this.GetSection(index13 + 1);
                            if (this.WriteRtfSection(rtf, section2))
                              num1 = index13;
                            else
                              goto label_206;
                          }
                          else
                            goto label_206;
                        }
                      }
                      else
                        ++TextLen;
                    }
                    this.CloseCfmt(index13);
                  }
                  if (!this.e.TerArg.WordWrap && index13 < index2 && !this.WriteRtfControl(rtf, "par", 0, 0.0))
                    goto label_206;
                }
                else
                  goto label_206;
              }
            }
            if (this.e.RtfInTable)
            {
              if ((this.e.TerOpFlags & 1) == 0 || this.WriteRtfControl(rtf, "sstblend", 0, 0.0))
              {
                this.EndInterParaGroups(rtf, this.GetCurCfmt(index13, 0));
                for (int rtfCurLevel = this.e.RtfCurLevel; rtfCurLevel > this.e.RtfInitLevel; --rtfCurLevel)
                {
                  if (this.WriteRtfControl(rtf, "intbl", 0, 0.0) && this.WriteRtfControl(rtf, "itap", 1, (double) (rtfCurLevel - this.e.RtfInitLevel + 1)) && ((rtf.flags & 1024 /*0x0400*/) != 0 || this.WriteRtfControl(rtf, "nestcell", 0, 0.0)) && this.WriteRtfRow(rtf, 0, this.LevelCell(rtfCurLevel, index13), rtfCurLevel))
                    tc.ResetUintFlag(ref rtf.flags, 1024 /*0x0400*/);
                  else
                    goto label_206;
                }
                if (!this.WriteRtfControl(rtf, "intbl", 0, 0.0) || (rtf.flags & 1024 /*0x0400*/) == 0 && !this.WriteRtfControl(rtf, "e.cell", 0, 0.0) || !this.WriteRtfControl(rtf, "row", 0, 0.0))
                  goto label_206;
              }
              else
                goto label_206;
            }
            for (int groupLevel = rtf.GroupLevel; groupLevel > 0; --groupLevel)
            {
              if (!this.EndRtfGroup(rtf))
                goto label_206;
            }
            flag2 = true;
          }
        }
      }
    }
label_206:
    this.FlushRtfLine(rtf);
    switch (output)
    {
      case 0:
        streamWriter.Close();
        if ((this.e.TerFlags5 & 2) != 0 && this.e.Parent != null)
        {
          this.e.Parent.Text = OutFile;
          break;
        }
        break;
      case 1:
        this.e.TerArg.hBuffer = new string(rtf.buf, 0, rtf.BufIndex);
        this.e.TerArg.BufferLen = rtf.BufIndex;
        break;
      default:
        rtf.buf[rtf.BufIndex] = char.MinValue;
        ++rtf.BufIndex;
        rtf.buf = this.ReAlloc(rtf.buf, rtf.BufIndex);
        if (output == 2)
        {
          this.e.RtfClipData = rtf.buf;
          this.e.ClipInfo = new tc.ClsClipInfo();
          this.e.ClipInfo.size = 12;
          this.e.ClipInfo.TblLevel = this.e.ClipTblLevel;
          this.e.ClipInfo.EmbTable = this.e.ClipEmbTable;
          this.e.ClipTblLevel = 1;
          this.e.ClipEmbTable = true;
          break;
        }
        OutData = new string(rtf.buf);
        this.e.DlgInt1 = rtf.BufIndex;
        break;
    }
    if (this.True(x))
      this.e.Cursor = x;
    if (output == 0 || output == 1)
      this.e.TerArg.modified = this.e.PageModifyCount = 0;
    return flag2;
  }

  internal string TerGetRtfSel()
  {
    string OutData = (string) null;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.e.HilightType != 0)
    {
      if (!this.NormalizeBlock())
        return (string) null;
      if (this.RtfWrite(3, "", out OutData))
        return OutData;
    }
    return (string) null;
  }

  internal bool WriteOtherShapeProps(tc.ClsRtfOut rtf, int ShapeType, ref tc.StrParaFrame pFrame)
  {
    if (ShapeType == 20)
    {
      if ((pFrame.flags & 262144 /*0x040000*/) != 0 && !this.WriteRtfShapeProp(rtf, "fLine", "0") || pFrame.LineType == 2 && !this.WriteRtfShapeProp(rtf, "fFlipV", "1"))
        return false;
    }
    else
    {
      string str = ((pFrame.flags & 1024 /*0x0400*/) == 0 || pFrame.LineWdth <= 0 ? 0 : 1).ToString();
      if (!this.WriteRtfShapeProp(rtf, "fLine", str))
        return false;
    }
    if (ShapeType == 20 || (pFrame.flags & 1024 /*0x0400*/) != 0)
    {
      string str1 = this.TwipsToEmu(pFrame.LineWdth).ToString();
      if (!this.WriteRtfShapeProp(rtf, "lineWidth", str1))
        return false;
      string str2 = this.ToColorRef(pFrame.LineColor).ToString();
      if (!this.WriteRtfShapeProp(rtf, "lineColor", str2))
        return false;
      if ((pFrame.flags & 2048 /*0x0800*/) != 0)
      {
        string str3 = 6.ToString();
        if (!this.WriteRtfShapeProp(rtf, "lineDashing", str3))
          return false;
      }
    }
    if (ShapeType == 1)
    {
      string str4 = (pFrame.FillPattern == 0 ? 0 : 1).ToString();
      if (!this.WriteRtfShapeProp(rtf, "fFilled", str4))
        return false;
      if (pFrame.FillPattern > 0)
      {
        string str5 = this.ToColorRef(pFrame.BackColor).ToString();
        if (!this.WriteRtfShapeProp(rtf, "fillColor", str5))
          return false;
      }
    }
    if ((pFrame.flags & 16777216 /*0x01000000*/) != 0)
    {
      string str = 1.ToString();
      if (!this.WriteRtfShapeProp(rtf, "fLayoutInCell", str))
        return false;
    }
    if ((pFrame.flags & 4) != 0)
    {
      if (!this.WriteRtfShapeProp(rtf, "posh", "3"))
        return false;
    }
    else if ((pFrame.flags & 8) != 0 && !this.WriteRtfShapeProp(rtf, "posh", "2"))
      return false;
    return true;
  }

  internal bool WritePfObjectTail(tc.ClsRtfOut rtf, int ParaFID)
  {
    if (ParaFID > 0)
    {
      if ((this.e.TerFlags5 & 32 /*0x20*/) == 0)
      {
        if ((this.e.ParaFrame[ParaFID].flags & 128 /*0x80*/) != 0 && !this.EndRtfGroup(rtf) || !this.EndRtfGroup(rtf) || !this.EndRtfGroup(rtf))
          return false;
        this.FlushRtfLine(rtf);
        return true;
      }
      if ((this.e.ParaFrame[ParaFID].flags & 128 /*0x80*/) != 0 && !this.EndRtfGroup(rtf))
        return false;
      if ((this.e.ParaFrame[ParaFID].flags & 256 /*0x0100*/) != 0)
      {
        int val1 = 0;
        int val2 = 0;
        int val3 = this.e.ParaFrame[ParaFID].width;
        int val4 = this.e.ParaFrame[ParaFID].height;
        if (this.e.ParaFrame[ParaFID].LineType == 0)
          val4 = val2;
        else if (this.e.ParaFrame[ParaFID].LineType == 1)
          val3 = val1;
        else if (this.e.ParaFrame[ParaFID].LineType == 2)
        {
          val2 = this.e.ParaFrame[ParaFID].height;
          val4 = 0;
        }
        if (!this.WriteRtfControl(rtf, "dpptx", 1, (double) val1) || !this.WriteRtfControl(rtf, "dppty", 1, (double) val2) || !this.WriteRtfControl(rtf, "dpptx", 1, (double) val3) || !this.WriteRtfControl(rtf, "dppty", 1, (double) val4))
          return false;
      }
      if (!this.WriteRtfControl(rtf, "dpx", 1, (double) this.e.ParaFrame[ParaFID].x) || !this.WriteRtfControl(rtf, "dpy", 1, (double) this.e.ParaFrame[ParaFID].ParaY) || !this.WriteRtfControl(rtf, "dpxsize", 1, (double) this.e.ParaFrame[ParaFID].width))
        return false;
      int val = this.e.ParaFrame[ParaFID].MinHeight;
      if (val == 0)
        val = this.e.ParaFrame[ParaFID].height;
      if (!this.WriteRtfControl(rtf, "dpysize", 1, (double) val))
        return false;
      if ((this.e.ParaFrame[ParaFID].flags & 1024 /*0x0400*/) != 0)
      {
        string control = (this.e.ParaFrame[ParaFID].flags & 2048 /*0x0800*/) == 0 ? "dplinesolid" : "dplinedot";
        if (!this.WriteRtfControl(rtf, control, 0, 0.0))
          return false;
      }
      else if (!this.WriteRtfControl(rtf, "dplinehollow", 0, 0.0))
        return false;
      if (!this.WriteRtfControl(rtf, "dplinecor", 1, (double) this.e.ParaFrame[ParaFID].LineColor.R) || !this.WriteRtfControl(rtf, "dplinecog", 1, (double) this.e.ParaFrame[ParaFID].LineColor.G) || !this.WriteRtfControl(rtf, "dplinecob", 1, (double) this.e.ParaFrame[ParaFID].LineColor.B) || !this.WriteRtfControl(rtf, "dplinew", 1, (double) this.e.ParaFrame[ParaFID].LineWdth) || !this.WriteRtfControl(rtf, "dpfillbgcr", 1, (double) this.e.ParaFrame[ParaFID].BackColor.R) || !this.WriteRtfControl(rtf, "dpfillbgcg", 1, (double) this.e.ParaFrame[ParaFID].BackColor.G) || !this.WriteRtfControl(rtf, "dpfillbgcb", 1, (double) this.e.ParaFrame[ParaFID].BackColor.B) || !this.WriteRtfControl(rtf, "dpfillpat", 1, (double) this.e.ParaFrame[ParaFID].FillPattern) || !this.EndRtfGroup(rtf))
        return false;
      this.FlushRtfLine(rtf);
    }
    return true;
  }

  internal bool WriteRtfAnimSeq(tc.ClsRtfOut rtf, int pict)
  {
    rtf.flags |= 64 /*0x40*/;
    int pict1 = pict;
    do
    {
      tc.ClsAnim anim = this.e.TerFont[pict1].anim;
      if (anim != null)
        pict1 = anim.NextPict;
      else
        break;
    }
    while (pict1 != 0 && this.e.TerFont[pict1].anim != null && this.WriteRtfPicture(rtf, pict1));
    tc.ResetUintFlag(ref rtf.flags, 64 /*0x40*/);
    return true;
  }

  internal bool WriteRtfBackground(tc.ClsRtfOut rtf, Color BkColor)
  {
    if ((this.e.TerFlags3 & 131072 /*0x020000*/) == 0)
    {
      if (!this.WriteRtfControl(rtf, "viewkind", 1, 5.0) || !this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "*", 0, 0.0) || !this.WriteRtfControl(rtf, "background", 0, 0.0) || !this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "shp", 0, 0.0) || !this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "*", 0, 0.0) || !this.WriteRtfControl(rtf, "shpinst", 0, 0.0) || !this.WriteRtfControl(rtf, "shpleft", 1, 0.0) || !this.WriteRtfControl(rtf, "shpright", 1, 0.0) || !this.WriteRtfControl(rtf, "shptop", 1, 0.0) || !this.WriteRtfControl(rtf, "shpbottom", 1, 0.0) || !this.WriteRtfControl(rtf, "shpbymargin", 0, 0.0) || !this.WriteRtfControl(rtf, "shpwr", 1, 1.0) || !this.WriteRtfControl(rtf, "shpwrk", 1, 0.0))
        return false;
      string str1 = 1.ToString();
      if (!this.WriteRtfShapeProp(rtf, "shapeType", str1))
        return false;
      string str2 = this.ToColorRef(BkColor).ToString();
      if (!this.WriteRtfShapeProp(rtf, "fillColor", str2) || !this.EndRtfGroup(rtf) || !this.EndRtfGroup(rtf) || !this.EndRtfGroup(rtf))
        return false;
    }
    return true;
  }

  internal bool WriteRtfBullet(tc.ClsRtfOut rtf, int BltId)
  {
    string str = "";
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = this.e.TerBlt[BltId].IsBullet;
    if (!flag3 && this.e.TerBlt[BltId].ls > 0)
    {
      tc.StrListLevel pLevel = new tc.StrListLevel();
      this.GetListLevelPtr(this.e.TerBlt[BltId].ls, this.e.TerBlt[BltId].lvl, out pLevel);
      flag3 = pLevel.NumType == 23;
    }
    if (this.e.TerBlt[BltId].IsBullet && (rtf.flags & 65536 /*0x010000*/) == 0 && (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "pntext", 0, 0.0) || !this.WriteRtfControl(rtf, "pard", 0, 0.0) || !this.WriteRtfControl(rtf, "plain", 0, 0.0) || !this.WriteRtfControl(rtf, "f", 1, 1.0) || !this.WriteRtfControl(rtf, "fs", 1, 20.0) || !this.PutRtfChar(rtf, '\\') || !this.WriteRtfText(rtf, "'b7", 3) || !this.WriteRtfControl(rtf, "tab", 0, 20.0) || !this.EndRtfGroup(rtf)) || !this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "*", 0, 0.0) || !this.WriteRtfControl(rtf, "pn", 0, 0.0))
      return false;
    if ((this.e.TerBlt[BltId].flags & 1) != 0)
    {
      if (!this.WriteRtfControl(rtf, "pnlvlcont", 0, 0.0))
        return false;
      flag1 = true;
    }
    if (flag3)
    {
      if (!flag1 && !this.WriteRtfControl(rtf, "pnlvlblt", 0, 0.0))
        return false;
    }
    else
    {
      if (this.e.TerBlt[BltId].level == 0 || this.e.TerBlt[BltId].level == 10)
      {
        if (!flag1 && !this.WriteRtfControl(rtf, "pnlvlbody", 0, 0.0))
          return false;
      }
      else if (!flag1 && !this.WriteRtfControl(rtf, "pnlvl", 1, (double) this.e.TerBlt[BltId].level))
        return false;
      if (!this.WriteRtfControl(rtf, "pnstart", 1, this.e.TerBlt[BltId].start == 0 ? 1.0 : (double) this.e.TerBlt[BltId].start) || this.e.TerBlt[BltId].NumberType == 1 && !this.WriteRtfControl(rtf, "pnucltr", 0, 0.0) || this.e.TerBlt[BltId].NumberType == 2 && !this.WriteRtfControl(rtf, "pnlcltr", 0, 0.0) || this.e.TerBlt[BltId].NumberType == 3 && !this.WriteRtfControl(rtf, "pnucrm", 0, 0.0) || this.e.TerBlt[BltId].NumberType == 4 && !this.WriteRtfControl(rtf, "pnlcrm", 0, 0.0) || this.e.TerBlt[BltId].NumberType == 0 && !this.WriteRtfControl(rtf, "pndec", 0, 0.0))
        return false;
      if (this.e.TotalListOr > 1)
      {
        int ls = this.e.TerBlt[BltId].ls;
        if (!this.WriteRtfControl(rtf, "ls", 1, (double) rtf.XlateLs[ls]) || !this.WriteRtfControl(rtf, "ilvl", 1, (double) this.e.TerBlt[BltId].lvl))
          return false;
        flag2 = true;
      }
    }
    if (this.e.TotalListOr > 1)
    {
      int ls = this.e.TerBlt[BltId].ls;
      if (!this.WriteRtfControl(rtf, "ls", 1, (double) rtf.XlateLs[ls]) || !this.WriteRtfControl(rtf, "ilvl", 1, (double) this.e.TerBlt[BltId].lvl))
        return false;
      flag2 = true;
    }
    if (!this.WriteRtfControl(rtf, "pnhang", 0, 0.0))
      return false;
    if (flag3)
    {
      int index1 = 0;
      if (this.e.TerBlt[BltId].font == 1)
        str = "Symbol";
      else if (this.e.TerBlt[BltId].font == 2)
        str = "Wingdings";
      int index2 = 0;
      while (index2 < this.e.TotalFonts && (!this.e.TerFont[index2].InUse || (this.e.TerFont[index2].style & 128 /*0x80*/) != 0 || !(this.e.TerFont[index2].TypeFace == str)))
        ++index2;
      if (index2 < this.e.TotalFonts)
        index1 = index2;
      if (!this.WriteRtfControl(rtf, "pnf", 1, (double) this.e.TerFont[index1].RtfIndex) || !this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "pntxtb", 0, 0.0))
        return false;
      char CurChar = this.e.TerBlt[BltId].BulletChar == char.MinValue ? '·' : this.e.TerBlt[BltId].BulletChar;
      if (!this.PutRtfSpecChar(rtf, CurChar) || !this.EndRtfGroup(rtf))
        return false;
    }
    else
    {
      if (this.e.TerBlt[BltId].BefText.Length > 0)
      {
        if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "pntxtb", 0, 0.0))
          return false;
        rtf.WritingControl = true;
        this.WriteRtfText(rtf, this.e.TerBlt[BltId].BefText, this.e.TerBlt[BltId].BefText.Length);
        rtf.WritingControl = false;
        if (!this.EndRtfGroup(rtf))
          return false;
      }
      char CurChar = this.e.TerBlt[BltId].AftChar;
      if (CurChar == char.MinValue)
        CurChar = '.';
      if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "pntxta", 0, 0.0) || !this.PutRtfSpecChar(rtf, CurChar) || !this.EndRtfGroup(rtf))
        return false;
    }
    if (!this.EndRtfGroup(rtf))
      return false;
    if (flag2)
    {
      int ls = this.e.TerBlt[BltId].ls;
      if (!this.WriteRtfControl(rtf, "ls", 1, (double) rtf.XlateLs[ls]) || !this.WriteRtfControl(rtf, "ilvl", 1, (double) this.e.TerBlt[BltId].lvl))
        return false;
    }
    return true;
  }

  internal bool WriteRtfCell(tc.ClsRtfOut rtf, int CurCellId)
  {
    int row = this.e.cell[CurCellId].row;
    int flags = this.e.cell[CurCellId].flags;
    this.FlushRtfLine(rtf);
    if ((flags & 77824 /*0x013000*/) == 0 && !this.WriteRtfControl(rtf, "clvertalt", 0, 0.0) || (flags & 4096 /*0x1000*/) != 0 && !this.WriteRtfControl(rtf, "clvertalc", 0, 0.0) || (flags & 8192 /*0x2000*/) != 0 && !this.WriteRtfControl(rtf, "clvertalb", 0, 0.0) || (flags & 65536 /*0x010000*/) != 0 && !this.WriteRtfControl(rtf, "ssclvertalbs", 0, 0.0) || this.e.cell[CurCellId].RowSpan > 1 && (this.e.cell[CurCellId].flags & 16 /*0x10*/) == 0 && !this.WriteRtfControl(rtf, "clvmgf", 0, 0.0))
      return false;
    if ((this.e.cell[CurCellId].flags & 16 /*0x10*/) != 0)
    {
      int spanningCell = this.e.CellAux[CurCellId].SpanningCell;
      if (spanningCell > 0 && this.e.cell[spanningCell].RowSpan > 1 && !this.WriteRtfControl(rtf, "clvmrg", 0, 0.0))
        return false;
    }
    if (this.e.cell[CurCellId].ColSpan > 1 && (this.e.cell[CurCellId].level == this.e.RtfInitLevel && !this.WriteRtfControl(rtf, "clmgf", 0, 0.0) || this.e.cell[CurCellId].level != this.e.RtfInitLevel && !this.WriteRtfControl(rtf, "sscolspan", 1, (double) this.e.cell[CurCellId].ColSpan)) || !this.WriteRtfCellBorder(rtf, CurCellId) || this.e.cell[CurCellId].shading > 0 && !this.WriteRtfControl(rtf, "clshdng", 1, (double) (this.e.cell[CurCellId].shading * 100)))
      return false;
    if (this.e.cell[CurCellId].BackColor != tc.CLR_WHITE || this.e.cell[CurCellId].ParentCell > 0 || (this.e.cell[CurCellId].flags & 16384 /*0x4000*/) != 0)
    {
      int val = 0;
      while (val < rtf.TotalColors && !(rtf.color[val].color == this.e.cell[CurCellId].BackColor))
        ++val;
      if (val == rtf.TotalColors)
        val = 0;
      if (!this.WriteRtfControl(rtf, "clcbpat", 1, (double) val))
        return false;
    }
    if ((this.e.cell[CurCellId].flags & 32768 /*0x8000*/) != 0 && (!this.WriteRtfControl(rtf, "clpadt", 1, (double) this.e.cell[CurCellId].margin) || !this.WriteRtfControl(rtf, "clpadr", 1, (double) this.e.cell[CurCellId].margin) || !this.WriteRtfControl(rtf, "clpadft", 1, 3.0) || !this.WriteRtfControl(rtf, "clpadfr", 1, 3.0)) || this.e.cell[CurCellId].TextAngle == 90 && !this.WriteRtfControl(rtf, "cltxbtlr", 0, 0.0) || this.e.cell[CurCellId].TextAngle == 270 && !this.WriteRtfControl(rtf, "cltxtbrl", 0, 0.0))
      return false;
    int val1 = this.e.cell[CurCellId].x + this.e.cell[CurCellId].width - this.e.TableRow[row].AddedIndent;
    int colSpan = this.e.cell[CurCellId].ColSpan;
    if (colSpan > 1 && this.e.cell[CurCellId].level == this.e.RtfInitLevel)
      val1 -= 2 * this.e.TableRow[row].CellMargin * (colSpan - 1);
    if (!this.WriteRtfControl(rtf, "cellx", 1, (double) val1))
      return false;
    this.FlushRtfLine(rtf);
    if (this.e.cell[CurCellId].level == this.e.RtfInitLevel)
    {
      for (; colSpan > 1; --colSpan)
      {
        if (!this.WriteRtfControl(rtf, "clmrg", 0, 0.0) || !this.WriteRtfCellBorder(rtf, CurCellId))
          return false;
        val1 += 2 * this.e.TableRow[row].CellMargin;
        if (!this.WriteRtfControl(rtf, "cellx", 1, (double) val1))
          return false;
      }
    }
    this.FlushRtfLine(rtf);
    return true;
  }

  internal bool WriteRtfCellBorder(tc.ClsRtfOut rtf, int CellId)
  {
    Color color1 = tc.CLR_AUTO;
    int num = -1;
    for (int index1 = 0; index1 < 4; ++index1)
    {
      if (this.e.cell[CellId].BorderWidth[index1] != 0 || this.e.cell[CellId].BorderColor[index1] != tc.CLR_AUTO)
      {
        if (index1 == 0 && !this.WriteRtfControl(rtf, "clbrdrt", 0, 0.0) || index1 == 1 && !this.WriteRtfControl(rtf, "clbrdrb", 0, 0.0) || index1 == 2 && !this.WriteRtfControl(rtf, "clbrdrl", 0, 0.0) || index1 == 3 && !this.WriteRtfControl(rtf, "clbrdrr", 0, 0.0) || this.e.cell[CellId].BorderWidth[index1] > 0 && (!this.WriteRtfControl(rtf, "brdrs", 0, 0.0) || !this.WriteRtfControl(rtf, "brdrw", 1, (double) this.e.cell[CellId].BorderWidth[index1])))
          return false;
        Color color2;
        if ((color2 = this.e.cell[CellId].BorderColor[index1]) != tc.CLR_AUTO)
        {
          int val;
          if (color2 == color1)
          {
            val = num;
          }
          else
          {
            int index2 = 0;
            while (index2 < rtf.TotalColors && !(rtf.color[index2].color == color2))
              ++index2;
            if (index2 == rtf.TotalColors)
              index2 = 0;
            color1 = color2;
            num = val = index2;
          }
          if (!this.WriteRtfControl(rtf, "brdrcf", 1, (double) val))
            return false;
        }
      }
    }
    return true;
  }

  internal bool WriteRtfCharFmt(tc.ClsRtfOut rtf, int CurFont, bool BeforeBreak)
  {
    int flag = 39936;
    if ((this.e.TerFlags3 & 4096 /*0x1000*/) != 0)
      flag |= 8192 /*0x2000*/;
    if (CurFont < this.e.TotalFonts && CurFont >= 0)
    {
      tc.StrRtfOutGroup strRtfOutGroup1 = rtf.group[rtf.GroupLevel];
      int index = strRtfOutGroup1.FontId;
      if (index < 0)
        index = 0;
      int style1 = this.e.TerFont[index].style;
      tc.ResetUintFlag(ref style1, flag);
      string fieldCode = strRtfOutGroup1.FieldCode;
      int fieldId = strRtfOutGroup1.FieldId;
      style1 |= strRtfOutGroup1.style & flag;
      int style2 = this.e.TerFont[CurFont].style;
      string str1;
      int FieldId;
      if (BeforeBreak)
      {
        str1 = "";
        FieldId = 0;
      }
      else
      {
        str1 = this.e.TerFont[CurFont].FieldCode;
        FieldId = this.e.TerFont[CurFont].FieldId;
      }
      if ((style2 & 16 /*0x10*/) != 0 != ((style1 & 16 /*0x10*/) != 0) && (style2 & 16 /*0x10*/) == 0)
      {
        if (!this.EndRtfGroup(rtf))
          return false;
        rtf.flags = tc.ResetUintFlag(ref rtf.flags, 4);
      }
      if ((style2 & 32 /*0x20*/) != 0 != ((style1 & 32 /*0x20*/) != 0) && (style2 & 32 /*0x20*/) == 0)
      {
        if (!this.EndRtfGroup(rtf))
          return false;
        rtf.flags = tc.ResetUintFlag(ref rtf.flags, 8);
      }
      if ((rtf.flags & 4) != 0 && !this.EndRtfGroup(rtf) || (rtf.flags & 8) != 0 && !this.EndRtfGroup(rtf))
        return false;
      if (FieldId != 6 && FieldId != 7 && (fieldId == 6 || fieldId == 7))
      {
        if (!this.EndRtfGroup(rtf))
          return false;
        if (fieldId == 6)
        {
          if (!this.BeginRtfGroup(rtf))
            return false;
          this.WriteRtfControl(rtf, "fldrslt", 0, 0.0);
          if (!this.EndRtfGroup(rtf))
            return false;
        }
        if (!this.EndRtfGroup(rtf))
          return false;
        tc.ResetUintFlag(ref rtf.flags, 512 /*0x0200*/);
        if (rtf.FieldHasPara)
          rtf.ParaFmtOnParaEnd = true;
      }
      if (FieldId == 7 && fieldId == 6)
      {
        if (!this.EndRtfGroup(rtf) || !this.BeginRtfGroup(rtf))
          return false;
        this.WriteRtfControl(rtf, "fldrslt", 0, 0.0);
        tc.ResetUintFlag(ref rtf.flags, 512 /*0x0200*/);
      }
      if (FieldId == 6 && (fieldId == 0 || fieldId == 7))
        this.BeginRtfFieldName(rtf, fieldId, -1);
      if ((this.IsDynField(fieldId) || fieldId == 9 || fieldId == 14 || fieldId == 2) && (fieldId != FieldId || !this.IsSameFieldCode(fieldCode, str1)))
      {
        if (!this.EndRtfGroup(rtf) || !this.EndRtfGroup(rtf))
          return false;
        tc.ResetUintFlag(ref rtf.flags, 256 /*0x0100*/);
        if (rtf.FieldHasPara)
          rtf.ParaFmtOnParaEnd = true;
      }
      if (fieldId == 13 && fieldId != FieldId)
      {
        if (!this.EndRtfGroup(rtf))
          return false;
        tc.ResetUintFlag(ref rtf.flags, 4096 /*0x1000*/);
        tc.ResetUintFlag(ref rtf.flags, 256 /*0x0100*/);
      }
      if (fieldId == 15 && fieldId != FieldId)
      {
        if (!this.EndRtfGroup(rtf))
          return false;
        tc.ResetUintFlag(ref rtf.flags, 8192 /*0x2000*/);
      }
      if ((this.e.TerFlags3 & 4096 /*0x1000*/) != 0 && (style1 & 8192 /*0x2000*/) != 0 != ((style2 & 8192 /*0x2000*/) != 0))
      {
        if ((style1 & 8192 /*0x2000*/) != 0)
        {
          this.WriteRtfText(rtf, ")", 1);
          if (!this.EndRtfGroup(rtf) || !this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "fldrslt", 0, 0.0) || !this.EndRtfGroup(rtf) || !this.EndRtfGroup(rtf))
            return false;
        }
        else if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "field", 0, 0.0) || !this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "fldinst", 0, 0.0) || !this.WriteRtfText(rtf, "EQ \\X(", 6))
          return false;
      }
      if ((style1 & 2048 /*0x0800*/) != 0 && (style2 & 4096 /*0x1000*/) != 0 && !this.EndRtfGroup(rtf) || (style1 & 4096 /*0x1000*/) != 0 && (style2 & 6144) == 0 && !this.EndRtfGroup(rtf) || (style1 & 1024 /*0x0400*/) != 0 && (style2 & 7168) == 0 && !this.EndRtfGroup(rtf) || (style1 & 2048 /*0x0800*/) != 0 && (style2 & 6144) == 0 && (!this.EndRtfGroup(rtf) || !this.EndRtfGroup(rtf)) || (style1 & 1024 /*0x0400*/) == 0 && (style2 & 1024 /*0x0400*/) != 0 && !this.BeginRtfGroup(rtf))
        return false;
      if ((style1 & 1024 /*0x0400*/) != 0 && (style2 & 2048 /*0x0800*/) != 0)
      {
        if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "footnote", 0, 0.0) || (style2 & 32768 /*0x8000*/) != 0 && !this.WriteRtfControl(rtf, "ftnalt", 0, 0.0))
          return false;
        rtf.flags |= 16384 /*0x4000*/;
        rtf.group[rtf.GroupLevel].flags |= 1;
      }
      if ((this.IsDynField(FieldId) || FieldId == 9 || FieldId == 14 || FieldId == 2) && (FieldId != fieldId || !this.IsSameFieldCode(str1, fieldCode)))
      {
        if (!this.WriteRtfFontAttrib(rtf, CurFont) || !this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "field", 0, 0.0) || !this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "fldinst", 0, 0.0) || (FieldId == 1 || FieldId == 5 || FieldId == 17 || FieldId == 8 || FieldId == 10) && !this.WriteRtfFontAttrib(rtf, CurFont) || FieldId == 1 && !this.WriteRtfText(rtf, "PAGE", 4) || FieldId == 5 && !this.WriteRtfText(rtf, "NUMPAGES", 8) || FieldId == 17 && !this.WriteRtfText(rtf, "SECTIONPAGES", 12))
          return false;
        if (FieldId == 8 || FieldId == 10)
        {
          string str2 = FieldId == 8 ? "TIME" : "PRINTDATE";
          if (this.True(str1) && str1 != "")
          {
            string str3 = str2 + " \\@ ";
            this.AddChar(ref str3, '"');
            str2 = str3 + str1;
            this.AddChar(ref str2, '"');
          }
          if (!this.WriteRtfText(rtf, str2, str2.Length))
            return false;
        }
        if (FieldId == 9)
        {
          string text = "TOC ";
          if (this.True(str1))
          {
            if (!this.WriteRtfText(rtf, text, text.Length) || !this.WriteRtfText(rtf, str1, str1.Length))
              return false;
            tc.ResetLongFlag(ref rtf.flags, 2);
          }
          else
          {
            string str4 = text + "\\o ";
            this.AddChar(ref str4, '"');
            string str5 = str4 + "1-9";
            this.AddChar(ref str5, '"');
            if (!this.WriteRtfText(rtf, str5, str5.Length))
              return false;
          }
        }
        if (FieldId == 11 || FieldId == 12 || FieldId == 14 || FieldId == 16 /*0x10*/)
        {
          string text = "";
          if (FieldId == 11)
            text = "LISTNUM";
          if (FieldId == 12)
            text = "AUTONUMLGL";
          if (FieldId == 14)
            text = "HYPERLINK";
          if (FieldId == 16 /*0x10*/)
            text = "PAGEREF";
          if (this.True(str1))
          {
            string str6 = text + " ";
            if (FieldId == 14)
            {
              text = str6 + this.AppendRtfHlink(rtf, str1);
            }
            else
            {
              text = str6 + str1;
              if (FieldId == 16 /*0x10*/)
                text += " ";
            }
          }
          if (!this.WriteRtfText(rtf, text, text.Length))
            return false;
        }
        if (FieldId == 2 && !this.WriteRtfFormTextField(rtf, str1) || !this.EndRtfGroup(rtf) || !this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "fldrslt", 0, 0.0))
          return false;
      }
      if (FieldId == 13 && fieldId != 13 && (rtf.flags & 4096 /*0x1000*/) == 0)
      {
        if (!this.BeginRtfGroup(rtf))
          return false;
        if (!this.WriteRtfControl(rtf, "tc", 0, 0.0))
          return true;
        if (str1 != null && str1.Length > 0)
        {
          rtf.flags |= 4096 /*0x1000*/;
          this.WriteRtfText(rtf, str1, str1.Length);
          tc.ResetUintFlag(ref rtf.flags, 4096 /*0x1000*/);
        }
      }
      if (FieldId == 15 && fieldId != 15 && (rtf.flags & 8192 /*0x2000*/) == 0)
      {
        rtf.flags |= 8192 /*0x2000*/;
        if (!this.BeginRtfGroup(rtf))
          return false;
        if (!this.WriteRtfControl(rtf, "xe", 0, 0.0))
          return true;
      }
      if ((style2 & 16 /*0x10*/) != 0 != ((style1 & 16 /*0x10*/) != 0) && (style2 & 16 /*0x10*/) != 0)
        rtf.flags |= 4;
      if ((rtf.flags & 4) != 0)
      {
        if (!this.BeginRtfGroup(rtf))
          return false;
        if (!this.WriteRtfControl(rtf, "super", 0, 0.0))
          return true;
      }
      if ((style2 & 32 /*0x20*/) != 0 != ((style1 & 32 /*0x20*/) != 0) && (style2 & 32 /*0x20*/) != 0)
        rtf.flags |= 8;
      if ((rtf.flags & 8) != 0)
      {
        if (!this.BeginRtfGroup(rtf))
          return false;
        if (!this.WriteRtfControl(rtf, "sub", 0, 0.0))
          return true;
      }
      tc.StrRtfOutGroup strRtfOutGroup2 = rtf.group[rtf.GroupLevel];
      if (BeforeBreak)
      {
        strRtfOutGroup2.FieldId = 0;
        strRtfOutGroup2.FieldCode = (string) null;
      }
      else
      {
        strRtfOutGroup2.FieldId = this.e.TerFont[CurFont].FieldId;
        strRtfOutGroup2.FieldCode = this.e.TerFont[CurFont].FieldCode;
      }
      rtf.group[rtf.GroupLevel] = strRtfOutGroup2;
      if (!this.WriteRtfFontAttrib(rtf, CurFont))
        return false;
    }
    return true;
  }

  internal bool WriteRtfCharStyle(tc.ClsRtfOut rtf, int id)
  {
    int style = this.e.StyleId[id].style;
    if (this.e.StyleId[id].TypeFace.Length > 0 && !this.WriteRtfControl(rtf, "f", 1, (double) this.e.StyleId[id].RtfIndex) || this.e.StyleId[id].TwipsSize > 0 && !this.WriteRtfControl(rtf, "fs", 1, (double) (this.e.StyleId[id].TwipsSize / 10)) || (style & 2) != 0 && !this.WriteRtfControl(rtf, "b", 0, 0.0) || (style & 1) != 0 && !this.WriteRtfControl(rtf, "ul", 0, 0.0) || (style & 256 /*0x0100*/) != 0 && !this.WriteRtfControl(rtf, "uldb", 0, 0.0) || (style & 4) != 0 && !this.WriteRtfControl(rtf, "i", 0, 0.0) || (style & 64 /*0x40*/) != 0 && !this.WriteRtfControl(rtf, "v", 0, 0.0) || (style & 8) != 0 && !this.WriteRtfControl(rtf, "strike", 0, 0.0) || (style & 512 /*0x0200*/) != 0 && !this.WriteRtfControl(rtf, "protect", 0, 0.0) || (style & 16 /*0x10*/) != 0 && !this.WriteRtfControl(rtf, "super", 0, 0.0) || (style & 32 /*0x20*/) != 0 && !this.WriteRtfControl(rtf, "sub", 0, 0.0) || (style & 16384 /*0x4000*/) != 0 && !this.WriteRtfControl(rtf, "sshlink", 0, 0.0) || (style & 65536 /*0x010000*/) != 0 && !this.WriteRtfControl(rtf, "caps", 0, 0.0) || (style & 131072 /*0x020000*/) != 0 && !this.WriteRtfControl(rtf, "scaps", 0, 0.0))
      return false;
    int val1 = 0;
    while (val1 < rtf.TotalColors && !(rtf.color[val1].color == this.e.StyleId[id].TextColor))
      ++val1;
    if (val1 == rtf.TotalColors)
      val1 = 0;
    if (!this.WriteRtfControl(rtf, "cf", 1, (double) val1))
      return false;
    int val2 = 0;
    while (val2 < rtf.TotalColors && !(rtf.color[val2].color == this.e.StyleId[id].TextBkColor))
      ++val2;
    if (val2 == rtf.TotalColors)
      val2 = 0;
    if (!this.WriteRtfControl(rtf, "cb", 1, (double) val2))
      return false;
    int val3 = 0;
    while (val3 < rtf.TotalColors && !(rtf.color[val3].color == this.e.StyleId[id].UlineColor))
      ++val3;
    if (val3 == rtf.TotalColors)
      val3 = 0;
    if (!this.WriteRtfControl(rtf, "ulc", 1, (double) val3) || this.True(this.e.StyleId[id].expand) && (!this.WriteRtfControl(rtf, "expnd", 1, (double) (this.e.StyleId[id].expand * 4 / 20)) || !this.WriteRtfControl(rtf, "expndtw", 1, (double) this.e.StyleId[id].expand)))
      return false;
    if (this.True(this.e.StyleId[id].expand))
    {
      int offset = this.e.StyleId[id].offset;
      if (!this.WriteRtfControl(rtf, offset > 0 ? "up" : "dn", 1, (double) this.TwipsToPoints(Math.Abs(offset) * 2)))
        return false;
    }
    return true;
  }

  internal bool WriteRtfColor(
    tc.ClsRtfOut rtf,
    List<bool> realUsedFonts,
    List<bool> realUsedStyles,
    List<bool> realUsedParaFormats)
  {
    this.FlushRtfLine(rtf);
    tc.StrRtfColor[] color = rtf.color;
    color[0].color = this.e.TerFont[0].TextColor;
    int index1 = 1;
    if (this.e.PageBkColor != tc.CLR_WHITE && color[0].color != this.e.PageBkColor)
    {
      color[index1].color = this.e.PageBkColor;
      ++index1;
    }
    int index2 = 0;
    while (index2 < this.e.TotalFonts)
    {
      if (!this.e.TerFont[index2].InUse || this.e.ShortRtf && !realUsedFonts[index2] && !(this.e.TerFont[index2].TypeFace == "Symbol") && !(this.e.TerFont[index2].TypeFace == "Wingdings"))
      {
        ++index2;
      }
      else
      {
        int index3 = 0;
        while (index3 < index1 && !(this.e.TerFont[index2].TextColor == color[index3].color))
          ++index3;
        if (index3 == index1 && index1 < this.e.MaxRtfColors && (!this.e.ShortRtf || !this.IsSameColor(this.e.TerFont[index2].TextColor, tc.CLR_BLACK)))
        {
          color[index1].color = this.e.TerFont[index2].TextColor;
          ++index1;
        }
        int index4 = 0;
        while (index4 < index1 && !(this.e.TerFont[index2].TextBkColor == color[index4].color))
          ++index4;
        if (index4 == index1 && index1 < this.e.MaxRtfColors && (!this.e.ShortRtf || !this.IsSameColor(this.e.TerFont[index2].TextBkColor, tc.CLR_WHITE)))
        {
          color[index1].color = this.e.TerFont[index2].TextBkColor;
          ++index1;
        }
        int index5 = 0;
        while (index5 < index1 && !(this.e.TerFont[index2].UlineColor == color[index5].color))
          ++index5;
        if (index5 == index1 && index1 < this.e.MaxRtfColors && (!this.e.ShortRtf || !this.IsSameColor(this.e.TerFont[index2].UlineColor, tc.CLR_BLACK)))
        {
          color[index1].color = this.e.TerFont[index2].UlineColor;
          ++index1;
        }
        ++index2;
      }
    }
    int index6 = 0;
    while (index6 < this.e.TotalSID)
    {
      if (!this.e.StyleId[index6].InUse || this.e.ShortRtf && !realUsedStyles[index6])
      {
        ++index6;
      }
      else
      {
        int index7 = 0;
        while (index7 < index1 && !(this.e.StyleId[index6].TextColor == color[index7].color))
          ++index7;
        if (index7 == index1 && index1 < this.e.MaxRtfColors && (!this.e.ShortRtf || !this.IsSameColor(this.e.StyleId[index6].TextColor, tc.CLR_BLACK)))
        {
          color[index1].color = this.e.StyleId[index6].TextColor;
          ++index1;
        }
        int index8 = 0;
        while (index8 < index1 && !(this.e.StyleId[index6].TextBkColor == color[index8].color))
          ++index8;
        if (index8 == index1 && index1 < this.e.MaxRtfColors && (!this.e.ShortRtf || !this.IsSameColor(this.e.StyleId[index6].TextBkColor, tc.CLR_WHITE)))
        {
          color[index1].color = this.e.StyleId[index6].TextBkColor;
          ++index1;
        }
        int index9 = 0;
        while (index9 < index1 && !(this.e.StyleId[index6].UlineColor == color[index9].color))
          ++index9;
        if (index9 == index1 && index1 < this.e.MaxRtfColors && (!this.e.ShortRtf || !this.IsSameColor(this.e.StyleId[index6].UlineColor, tc.CLR_BLACK)))
        {
          color[index1].color = this.e.StyleId[index6].UlineColor;
          ++index1;
        }
        int index10 = 0;
        while (index10 < index1 && !(this.e.StyleId[index6].ParaBkColor == color[index10].color))
          ++index10;
        if (index10 == index1 && index1 < this.e.MaxRtfColors && (!this.e.ShortRtf || !this.IsSameColor(this.e.StyleId[index6].ParaBkColor, tc.CLR_WHITE)))
        {
          color[index1].color = this.e.StyleId[index6].ParaBkColor;
          ++index1;
        }
        if (!this.IsSameColor(this.e.StyleId[index6].ParaBorderColor, tc.CLR_AUTO))
        {
          int index11 = 0;
          while (index11 < index1 && !(this.e.StyleId[index6].ParaBorderColor == color[index11].color))
            ++index11;
          if (index11 == index1 && index1 < this.e.MaxRtfColors && (!this.e.ShortRtf || !this.IsSameColor(this.e.StyleId[index6].ParaBorderColor, tc.CLR_BLACK)))
          {
            color[index1].color = this.e.StyleId[index6].ParaBorderColor;
            ++index1;
          }
        }
        ++index6;
      }
    }
    for (int index12 = 0; index12 < this.e.TotalSects; ++index12)
    {
      if (!this.False(this.e.TerSect[index12].border))
      {
        int index13 = 0;
        while (index13 < index1 && !(this.e.TerSect[index12].BorderColor == color[index13].color))
          ++index13;
        if (index13 == index1 && index1 < this.e.MaxRtfColors)
        {
          color[index1].color = this.e.TerSect[index12].BorderColor;
          ++index1;
        }
      }
    }
    for (int index14 = 0; index14 < this.e.TotalCells; ++index14)
    {
      if (this.e.cell[index14].InUse)
      {
        int index15 = 0;
        while (index15 < index1 && !(this.e.cell[index14].BackColor == color[index15].color))
          ++index15;
        if (index15 == index1 && index1 < this.e.MaxRtfColors && (!this.e.ShortRtf || !this.e.cell[index14].BackColor.IsEmpty && !this.IsSameColor(this.e.cell[index14].BackColor, tc.CLR_WHITE)))
        {
          color[index1].color = this.e.cell[index14].BackColor;
          ++index1;
        }
        for (int index16 = 0; index16 < 4; ++index16)
        {
          if (this.e.cell[index14].BorderWidth[index16] > 0 && this.e.cell[index14].BorderColor[index16] != tc.CLR_AUTO)
          {
            int index17 = 0;
            while (index17 < index1 && !(this.e.cell[index14].BorderColor[index16] == color[index17].color))
              ++index17;
            if (index17 == index1 && index1 < this.e.MaxRtfColors && (!this.e.ShortRtf || !this.IsSameColor(this.e.cell[index14].BorderColor[index16], tc.CLR_BLACK)))
            {
              color[index1].color = this.e.cell[index14].BorderColor[index16];
              ++index1;
            }
          }
        }
      }
    }
    int index18 = 0;
    while (index18 < this.e.TotalPfmts)
    {
      if (this.e.ShortRtf && !realUsedParaFormats[index18])
      {
        ++index18;
      }
      else
      {
        int index19 = 0;
        while (index19 < index1 && !(this.e.PfmtId[index18].BkColor == color[index19].color))
          ++index19;
        if (index19 == index1 && index1 < this.e.MaxRtfColors && (!this.e.ShortRtf || !this.IsSameColor(this.e.PfmtId[index18].BkColor, tc.CLR_WHITE)))
        {
          color[index1].color = this.e.PfmtId[index18].BkColor;
          ++index1;
        }
        if (!this.IsSameColor(this.e.PfmtId[index18].BorderColor, tc.CLR_AUTO))
        {
          int index20 = 0;
          while (index20 < index1 && !(this.e.PfmtId[index18].BorderColor == color[index20].color))
            ++index20;
          if (index20 == index1 && index1 < this.e.MaxRtfColors && (!this.e.ShortRtf || !this.IsSameColor(this.e.PfmtId[index18].BorderColor, tc.CLR_BLACK)))
          {
            color[index1].color = this.e.PfmtId[index18].BorderColor;
            ++index1;
          }
        }
        ++index18;
      }
    }
    rtf.TotalColors = index1;
    if (index1 > 1)
    {
      if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "colortbl", 0, 0.0))
        return false;
      for (int index21 = 0; index21 < index1; ++index21)
      {
        if (index21 > 0 || rtf.output >= 2 && (this.e.TerFlags6 & 4) == 0 || (this.e.TerFlags2 & 524288 /*0x080000*/) != 0)
        {
          byte val1 = color[index21].color.R;
          byte val2 = color[index21].color.G;
          byte val3 = color[index21].color.B;
          if (color[index21].color == tc.CLR_AUTO)
          {
            int num;
            val3 = (byte) (num = 0);
            val2 = (byte) num;
            val1 = (byte) num;
          }
          if (!this.WriteRtfControl(rtf, "red", 1, (double) val1) || !this.WriteRtfControl(rtf, "green", 1, (double) val2) || !this.WriteRtfControl(rtf, "blue", 1, (double) val3))
            return false;
        }
        if (!this.WriteRtfText(rtf, ";", 1))
          return false;
      }
      if (!this.EndRtfGroup(rtf))
        return false;
    }
    this.FlushRtfLine(rtf);
    return true;
  }

  internal bool WriteRtfControl(tc.ClsRtfOut rtf, string control, int type, double val)
  {
    rtf.SpacePending = false;
    rtf.WritingControl = true;
    if (!this.PutRtfChar(rtf, '\\') || !this.WriteRtfText(rtf, control, control.Length))
      return false;
    switch (type)
    {
      case 1:
        string text1 = ((int) val).ToString();
        if (!this.WriteRtfText(rtf, text1, text1.Length))
          return false;
        break;
      case 2:
        string text2 = val.ToString();
        if (!this.WriteRtfText(rtf, text2, text2.Length))
          return false;
        break;
    }
    rtf.SpacePending = true;
    rtf.WritingControl = false;
    return true;
  }

  internal bool WriteRtfCtl(tc.ClsRtfOut rtf, int pict)
  {
    bool flag = true;
    byte[] pictData = this.e.TerFont[pict].PictData;
    int pictHeight = this.e.TerFont[pict].PictHeight;
    int pictWidth = this.e.TerFont[pict].PictWidth;
    if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "*", 0, 0.0) || !this.WriteRtfControl(rtf, "sscontrol", 0, 0.0) || !this.WriteRtfControl(rtf, "ssctl", 0, 0.0) || !this.WriteRtfControl(rtf, "picw", 1, (double) this.e.TerFont[pict].bmWidth) || !this.WriteRtfControl(rtf, "pich", 1, (double) this.e.TerFont[pict].bmHeight) || !this.WriteRtfControl(rtf, "picwgoal", 1, (double) pictWidth) || !this.WriteRtfControl(rtf, "pichgoal", 1, (double) pictHeight) || !this.WriteRtfControl(rtf, "sspicalign", 1, (double) this.e.TerFont[pict].PictAlign))
      return false;
    if ((this.e.TerFlags4 & 512 /*0x0200*/) != 0)
    {
      if (!this.WriteRtfControl(rtf, "bin", 1, (double) pictData.Length))
        return false;
      rtf.flags |= 2048 /*0x0800*/;
      rtf.SpacePending = false;
    }
    if (pictData != null)
    {
      for (int index = 0; index < pictData.Length; ++index)
      {
        flag = (this.e.TerFlags4 & 512 /*0x0200*/) == 0 ? this.PutRtfHexChar(rtf, (char) pictData[index]) : this.PutRtfChar(rtf, (char) pictData[index]);
        if (!flag)
          goto label_13;
      }
    }
    if ((this.e.TerFlags4 & 512 /*0x0200*/) != 0)
      tc.ResetUintFlag(ref rtf.flags, 2048 /*0x0800*/);
label_13:
    return this.EndRtfGroup(rtf) && flag;
  }

  internal bool WriteRtfDIB(tc.ClsRtfOut rtf, int pict, byte[] pMem, bool WriteHeader)
  {
    bool flag = false;
    this.FlushRtfLine(rtf);
    int pictHeight = this.e.TerFont[pict].PictHeight;
    int pictWidth = this.e.TerFont[pict].PictWidth;
    if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, nameof (pict), 0, 0.0) || !this.WriteRtfControl(rtf, "dibitmap", 1, 0.0))
      return false;
    int val1 = pictWidth;
    if (this.e.TerFont[pict].OrigPictWidth != 0)
      val1 = this.e.TerFont[pict].OrigPictWidth;
    int val2 = this.MulDiv(this.e.TerFont[pict].PictWidth, 100, val1 - this.e.TerFont[pict].CropLeft - this.e.TerFont[pict].CropRight);
    int val3 = pictHeight;
    if (this.e.TerFont[pict].OrigPictHeight != 0)
      val3 = this.e.TerFont[pict].OrigPictHeight;
    int val4 = this.MulDiv(this.e.TerFont[pict].PictHeight, 100, val3 - this.e.TerFont[pict].CropTop - this.e.TerFont[pict].CropBot);
    if (this.e.TerFont[pict].CropLeft != 0 && !this.WriteRtfControl(rtf, "piccropl", 1, (double) this.e.TerFont[pict].CropLeft) || this.e.TerFont[pict].CropRight != 0 && !this.WriteRtfControl(rtf, "piccropr", 1, (double) this.e.TerFont[pict].CropRight) || this.e.TerFont[pict].CropTop != 0 && !this.WriteRtfControl(rtf, "piccropt", 1, (double) this.e.TerFont[pict].CropTop) || this.e.TerFont[pict].CropBot != 0 && !this.WriteRtfControl(rtf, "piccropb", 1, (double) this.e.TerFont[pict].CropBot) || !this.WriteRtfControl(rtf, "picw", 1, (double) this.e.TerFont[pict].bmWidth) || !this.WriteRtfControl(rtf, "pich", 1, (double) this.e.TerFont[pict].bmHeight) || !this.WriteRtfControl(rtf, "picwgoal", 1, (double) val1) || !this.WriteRtfControl(rtf, "pichgoal", 1, (double) val3) || !this.WriteRtfControl(rtf, "picscalex", 1, (double) val2) || !this.WriteRtfControl(rtf, "picscaley", 1, (double) val4) || !this.WriteRtfControl(rtf, "sspicalign", 1, (double) this.e.TerFont[pict].PictAlign))
      return false;
    if ((this.e.TerFlags4 & 512 /*0x0200*/) != 0)
    {
      if (!this.WriteRtfControl(rtf, "bin", 1, (double) pMem.Length))
        return false;
      rtf.flags |= 2048 /*0x0800*/;
      rtf.SpacePending = false;
    }
    for (int index = WriteHeader ? 0 : 14; index < pMem.Length; ++index)
    {
      flag = (this.e.TerFlags4 & 512 /*0x0200*/) == 0 ? this.PutRtfHexChar(rtf, (char) pMem[index]) : this.PutRtfChar(rtf, (char) pMem[index]);
      if (!flag)
        goto label_18;
    }
    if ((this.e.TerFlags4 & 512 /*0x0200*/) != 0)
      tc.ResetUintFlag(ref rtf.flags, 2048 /*0x0800*/);
label_18:
    if (!this.EndRtfGroup(rtf))
      return false;
    this.FlushRtfLine(rtf);
    return flag;
  }

  internal bool WriteRtfDoInfo(tc.ClsRtfOut rtf, int CurParaFID, int ParaFlags, int NewFID)
  {
    if ((this.e.ParaFrame[CurParaFID].flags & 768 /*0x0300*/) != 0 && (ParaFlags & 32768 /*0x8000*/) != 0)
      this.WriteRtfControl(rtf, "keepn", 0, 0.0);
    this.FlushRtfLine(rtf);
    if ((this.e.TerFlags5 & 32 /*0x20*/) != 0)
    {
      if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "do", 0, 0.0) || (this.e.ParaFrame[CurParaFID].flags & 1) == 0 && !this.WriteRtfControl(rtf, "dobxmargin", 0, 0.0) || (this.e.ParaFrame[CurParaFID].flags & 1) != 0 && !this.WriteRtfControl(rtf, "dobxpage", 0, 0.0) || (this.e.ParaFrame[CurParaFID].flags & 96 /*0x60*/) == 0 && !this.WriteRtfControl(rtf, "dobypara", 0, 0.0) || (this.e.ParaFrame[CurParaFID].flags & 64 /*0x40*/) != 0 && !this.WriteRtfControl(rtf, "dobymargin", 0, 0.0) || (this.e.ParaFrame[CurParaFID].flags & 32 /*0x20*/) != 0 && !this.WriteRtfControl(rtf, "dobypage", 0, 0.0))
        return false;
      int val = this.e.ParaFrame[CurParaFID].ZOrder;
      if (val < 0)
        val = 0;
      if (!this.WriteRtfControl(rtf, "dodhgt", 1, (double) val))
        return false;
      if ((this.e.ParaFrame[CurParaFID].flags & 128 /*0x80*/) != 0)
      {
        if (!this.WriteRtfControl(rtf, "dptxbx", 0, 0.0) || !this.WriteRtfControl(rtf, "dptxbxmar", 1, (double) this.e.ParaFrame[CurParaFID].margin) || this.e.ParaFrame[CurParaFID].TextAngle == 90 && !this.WriteRtfControl(rtf, "dptxbtlr", 0, 0.0) || this.e.ParaFrame[CurParaFID].TextAngle == 270 && !this.WriteRtfControl(rtf, "dptxtbrl", 0, 0.0) || !this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "dptxbxtext", 0, 0.0))
          return false;
      }
      else if ((this.e.ParaFrame[CurParaFID].flags & 256 /*0x0100*/) != 0)
      {
        if (!this.WriteRtfControl(rtf, "dpline", 0, 0.0))
          return false;
        this.WritePfObjectTail(rtf, NewFID);
      }
      else if ((this.e.ParaFrame[CurParaFID].flags & 512 /*0x0200*/) != 0)
      {
        if (!this.WriteRtfControl(rtf, "dprect", 0, 0.0))
          return false;
        this.WritePfObjectTail(rtf, NewFID);
      }
    }
    else
    {
      tc.StrParaFrame strParaFrame = this.e.ParaFrame[CurParaFID];
      this.WriteRtfControl(rtf, "pard", 0, 0.0);
      this.FlushRtfLine(rtf);
      if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "shp", 0, 0.0) || !this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "*", 0, 0.0) || !this.WriteRtfControl(rtf, "shpinst", 0, 0.0) || !this.WriteRtfControl(rtf, "shpleft", 1, (double) strParaFrame.x) || !this.WriteRtfControl(rtf, "shpright", 1, (double) (strParaFrame.x + strParaFrame.width)) || !this.WriteRtfControl(rtf, "shptop", 1, (double) strParaFrame.ParaY) || !this.WriteRtfControl(rtf, "shpbottom", 1, (double) (strParaFrame.ParaY + strParaFrame.height)) || !this.WriteRtfControl(rtf, "shpbxmarg", 0, 0.0) || (strParaFrame.flags & 64 /*0x40*/) != 0 && !this.WriteRtfControl(rtf, "shpbymargin", 0, 0.0) || (strParaFrame.flags & 32 /*0x20*/) != 0 && !this.WriteRtfControl(rtf, "shpbypage", 0, 0.0) || (strParaFrame.flags & 96 /*0x60*/) == 0 && !this.WriteRtfControl(rtf, "shpbypara", 0, 0.0) || (strParaFrame.flags & 8192 /*0x2000*/) != 0 && !this.WriteRtfControl(rtf, "shpwr", 1, 1.0) || (strParaFrame.flags & 16384 /*0x4000*/) != 0 && !this.WriteRtfControl(rtf, "shpwr", 1, 3.0) || (strParaFrame.flags & 24576 /*0x6000*/) == 0 && (!this.WriteRtfControl(rtf, "shpwr", 1, 2.0) || !this.WriteRtfControl(rtf, "shpwrk", 1, 0.0)) || strParaFrame.ZOrder != 0 && !this.WriteRtfControl(rtf, "shpz", 1, (double) strParaFrame.ZOrder) || (strParaFrame.flags & 134217728 /*0x08000000*/) != 0 && !this.WriteRtfShapeProp(rtf, "fBehindDocument", "1"))
        return false;
      int num = (strParaFrame.flags & 256 /*0x0100*/) == 0 ? ((strParaFrame.flags & 512 /*0x0200*/) == 0 ? 202 : 1) : 20;
      string str1 = num.ToString();
      if (!this.WriteRtfShapeProp(rtf, "shapeType", str1))
        return false;
      if (num == 20)
      {
        if ((strParaFrame.flags & 262144 /*0x040000*/) != 0 && !this.WriteRtfShapeProp(rtf, "fLine", "0") || strParaFrame.LineType == 2 && !this.WriteRtfShapeProp(rtf, "fFlipV", "1"))
          return false;
      }
      else
      {
        string str2 = ((strParaFrame.flags & 1024 /*0x0400*/) == 0 || strParaFrame.LineWdth <= 0 ? 0 : 1).ToString();
        if (!this.WriteRtfShapeProp(rtf, "fLine", str2))
          return false;
      }
      if (num == 20 || (strParaFrame.flags & 1024 /*0x0400*/) != 0)
      {
        string str3 = this.TwipsToEmu(strParaFrame.LineWdth).ToString();
        if (!this.WriteRtfShapeProp(rtf, "lineWidth", str3))
          return false;
        string str4 = this.ToColorRef(strParaFrame.LineColor).ToString();
        if (!this.WriteRtfShapeProp(rtf, "lineColor", str4))
          return false;
        if ((strParaFrame.flags & 2048 /*0x0800*/) != 0)
        {
          string str5 = 6.ToString();
          if (!this.WriteRtfShapeProp(rtf, "lineDashing", str5))
            return false;
        }
      }
      if (num == 1 || num == 202)
      {
        string str6 = (this.e.ParaFrame[CurParaFID].FillPattern == 0 ? 0 : 1).ToString();
        if (!this.WriteRtfShapeProp(rtf, "fFilled", str6))
          return false;
        if (this.e.ParaFrame[CurParaFID].FillPattern > 0)
        {
          string str7 = this.ToColorRef(strParaFrame.BackColor).ToString();
          if (!this.WriteRtfShapeProp(rtf, "fillColor", str7))
            return false;
        }
      }
      if (num == 202)
      {
        if (this.e.ParaFrame[CurParaFID].TextAngle != 0)
        {
          string str8 = (this.e.ParaFrame[CurParaFID].TextAngle == 90 ? 2 : 1).ToString();
          if (!this.WriteRtfShapeProp(rtf, "txflTextFlow", str8))
            return false;
        }
        string str9 = this.TwipsToEmu(strParaFrame.margin).ToString();
        if (!this.WriteRtfShapeProp(rtf, "dxTextLeft", str9) || !this.WriteRtfShapeProp(rtf, "dxTextRight", str9) || !this.WriteRtfShapeProp(rtf, "dyTextTop", str9) || !this.WriteRtfShapeProp(rtf, "dyTextBottom", str9) || !this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "shptxt", 0, 0.0))
          return false;
        rtf.flags |= 16384 /*0x4000*/;
      }
      else
        this.WritePfObjectTail(rtf, NewFID);
    }
    return true;
  }

  [DllImport("Gdi32.dll")]
  internal static extern uint GetEnhMetaFileBits(IntPtr hemf, uint cbBuffer, byte[] lpbBuffer);

  [DllImport("Gdi32.dll")]
  internal static extern bool DeleteEnhMetaFile(IntPtr hemf);

  internal static void SaveImageToStream(Image image, Stream stream)
  {
    if (image is Metafile metafile1)
    {
      Metafile metafile = (Metafile) metafile1.Clone();
      IntPtr henhmetafile = metafile.GetHenhmetafile();
      uint enhMetaFileBits1 = CRtfw.GetEnhMetaFileBits(henhmetafile, 0U, (byte[]) null);
      byte[] numArray = new byte[(int) enhMetaFileBits1];
      int enhMetaFileBits2 = (int) CRtfw.GetEnhMetaFileBits(henhmetafile, enhMetaFileBits1, numArray);
      CRtfw.DeleteEnhMetaFile(henhmetafile);
      metafile.Dispose();
      stream.Write(numArray, 0, numArray.Length);
    }
    else
    {
      ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
      ImageFormat rawFormat = image.RawFormat;
      ImageCodecInfo encoder = (ImageCodecInfo) null;
      Guid guid1 = ImageFormat.Png.Guid;
      Guid guid2 = rawFormat.Guid;
      foreach (ImageCodecInfo imageCodecInfo in imageEncoders)
      {
        Guid formatId = imageCodecInfo.FormatID;
        if (formatId.Equals(guid2))
        {
          encoder = imageCodecInfo;
          break;
        }
        if (encoder == null)
        {
          formatId = imageCodecInfo.FormatID;
          if (formatId.Equals(guid1))
            encoder = imageCodecInfo;
        }
      }
      image.Save(stream, encoder, (EncoderParameters) null);
    }
  }

  internal bool WriteRtfEnhMetafile(tc.ClsRtfOut rtf, int pict)
  {
    bool flag = true;
    this.FlushRtfLine(rtf);
    byte[] numArray = this.e.TerFont[pict].PictData;
    if (numArray == null)
    {
      try
      {
        MemoryStream memoryStream = new MemoryStream();
        CRtfw.SaveImageToStream(this.e.TerFont[pict].image, (Stream) memoryStream);
        if (memoryStream.Length == 0L)
          return true;
        numArray = memoryStream.GetBuffer();
        memoryStream.Close();
      }
      catch (Exception ex)
      {
        return false;
      }
    }
    if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "*", 0, 0.0) || !this.WriteRtfControl(rtf, "shppict", 0, 0.0) || !this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, nameof (pict), 0, 0.0) || !this.WriteRtfControl(rtf, "emfblip", 0, 0.0))
      return false;
    int x1 = this.e.TerFont[pict].bmHeight;
    int x2 = this.e.TerFont[pict].bmWidth;
    if (x1 == 0)
      x1 = this.e.TerFont[pict].PictHeight;
    if (x2 == 0)
      x2 = this.e.TerFont[pict].PictWidth;
    int val1 = x2;
    if (this.e.TerFont[pict].OrigPictWidth != 0)
      val1 = this.e.TerFont[pict].OrigPictWidth;
    int val2 = this.MulDiv(this.e.TerFont[pict].PictWidth, 100, val1 - this.e.TerFont[pict].CropLeft - this.e.TerFont[pict].CropRight);
    int val3 = x1;
    if (this.e.TerFont[pict].OrigPictHeight != 0)
      val3 = this.e.TerFont[pict].OrigPictHeight;
    int val4 = this.MulDiv(this.e.TerFont[pict].PictHeight, 100, val3 - this.e.TerFont[pict].CropTop - this.e.TerFont[pict].CropBot);
    if (this.e.TerFont[pict].CropLeft != 0 && !this.WriteRtfControl(rtf, "piccropl", 1, (double) this.e.TerFont[pict].CropLeft) || this.e.TerFont[pict].CropRight != 0 && !this.WriteRtfControl(rtf, "piccropr", 1, (double) this.e.TerFont[pict].CropRight) || this.e.TerFont[pict].CropTop != 0 && !this.WriteRtfControl(rtf, "piccropt", 1, (double) this.e.TerFont[pict].CropTop) || this.e.TerFont[pict].CropBot != 0 && !this.WriteRtfControl(rtf, "piccropb", 1, (double) this.e.TerFont[pict].CropBot))
      return false;
    int val5 = this.MulDiv(x2, 2540, 1440);
    if (!this.WriteRtfControl(rtf, "picw", 1, (double) val5))
      return false;
    int val6 = this.MulDiv(x1, 2540, 1440);
    if (!this.WriteRtfControl(rtf, "pich", 1, (double) val6) || !this.WriteRtfControl(rtf, "picwgoal", 1, (double) val1) || !this.WriteRtfControl(rtf, "pichgoal", 1, (double) val3) || !this.WriteRtfControl(rtf, "picscalex", 1, (double) val2) || !this.WriteRtfControl(rtf, "picscaley", 1, (double) val4) || !this.WriteRtfControl(rtf, "sspicalign", 1, (double) this.e.TerFont[pict].PictAlign))
      return false;
    if ((this.e.TerFlags4 & 512 /*0x0200*/) != 0)
    {
      if (!this.WriteRtfControl(rtf, "bin", 1, (double) numArray.Length))
        return false;
      rtf.flags |= 2048 /*0x0800*/;
      rtf.SpacePending = false;
    }
    for (int index = 0; index < numArray.Length; ++index)
    {
      flag = (this.e.TerFlags4 & 512 /*0x0200*/) == 0 ? this.PutRtfHexChar(rtf, (char) numArray[index]) : this.PutRtfChar(rtf, (char) numArray[index]);
      if (!flag)
        break;
    }
    if ((this.e.TerFlags4 & 512 /*0x0200*/) != 0)
      tc.ResetUintFlag(ref rtf.flags, 2048 /*0x0800*/);
    if (!this.EndRtfGroup(rtf) || !this.EndRtfGroup(rtf))
      return false;
    this.FlushRtfLine(rtf);
    return flag;
  }

  internal bool WriteRtfFont(tc.ClsRtfOut rtf, List<bool> realUsedFonts, List<bool> realUsedStyles)
  {
    string name = "Times New Roman";
    int num = 16 /*0x10*/;
    int CharSet = 0;
    if (!this.e.ShortRtf && !this.WriteRtfControl(rtf, "deff", 1, this.e.TerFont[0].TypeFace == name ? 0.0 : 1.0))
      return false;
    this.FlushRtfLine(rtf);
    int index1 = 0;
    while (index1 < this.e.TotalPfmts && (this.e.PfmtId[index1].flags & 8) == 0)
      ++index1;
    if (index1 < this.e.TotalPfmts)
    {
      int index2 = 0;
      while (index2 < this.e.TotalFonts && (!this.e.TerFont[index2].InUse || !(this.e.TerFont[index2].TypeFace == "Symbol")))
        ++index2;
      if (index2 == this.e.TotalFonts)
      {
        this.GetNewFont(this.e.TerGr, 0, "Symbol", 12, 0, tc.CLR_BLACK, tc.CLR_WHITE, tc.CLR_AUTO, 0, 0, 0, 1, 0, 0, 0, 0, (string) null, 0, (byte) 1, 0, 0);
        if (realUsedFonts.Count < this.e.TotalFonts)
          realUsedFonts.Add(true);
      }
      int index3 = 0;
      while (index3 < this.e.TotalFonts && (!this.e.TerFont[index3].InUse || !(this.e.TerFont[index3].TypeFace == "Wingdings")))
        ++index3;
      if (index3 == this.e.TotalFonts)
      {
        this.GetNewFont(this.e.TerGr, 0, "Wingdings", 12, 0, tc.CLR_BLACK, tc.CLR_WHITE, tc.CLR_AUTO, 0, 0, 0, 1, 0, 0, 0, 0, (string) null, 0, (byte) 1, 0, 0);
        if (realUsedFonts.Count < this.e.TotalFonts)
          realUsedFonts.Add(true);
      }
    }
    bool flag = true;
    int RtfIndex1 = 0;
    if (!this.e.ShortRtf)
    {
      flag = false;
      if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "fonttbl", 0, 0.0))
        return false;
      this.WriteRtfOneFont(rtf, RtfIndex1, name, "roman", CharSet);
    }
    int RtfIndex2 = RtfIndex1 + 1;
    for (int index4 = 0; index4 < this.e.TotalFonts; ++index4)
    {
      if (!this.e.TerFont[index4].InUse || this.e.ShortRtf && !realUsedFonts[index4] && !(this.e.TerFont[index4].TypeFace == "Symbol") && !(this.e.TerFont[index4].TypeFace == "Wingdings") || (this.e.TerFont[index4].style & 128 /*0x80*/) != 0)
      {
        this.e.TerFont[index4].RtfIndex = 0;
      }
      else
      {
        int index5 = 0;
        while (index5 < index4 && (!this.e.TerFont[index5].InUse || this.e.ShortRtf && !realUsedFonts[index5] && !(this.e.TerFont[index4].TypeFace == "Symbol") && !(this.e.TerFont[index4].TypeFace == "Wingdings") || !(this.e.TerFont[index5].TypeFace == this.e.TerFont[index4].TypeFace) || (int) this.e.TerFont[index5].FontFamily != (int) this.e.TerFont[index4].FontFamily || (int) this.e.TerFont[index5].CharSet != (int) this.e.TerFont[index4].CharSet))
          ++index5;
        if (index5 < index4)
          this.e.TerFont[index4].RtfIndex = this.e.TerFont[index5].RtfIndex;
        else if (this.e.TerFont[index4].TypeFace == name && (int) this.e.TerFont[index4].FontFamily == num && (int) this.e.TerFont[index4].CharSet == CharSet)
        {
          this.e.TerFont[index4].RtfIndex = 0;
        }
        else
        {
          string typeFace = this.e.TerFont[index4].TypeFace;
          string family;
          switch (this.e.TerFont[index4].FontFamily)
          {
            case 16 /*0x10*/:
              family = "roman";
              break;
            case 32 /*0x20*/:
              family = "swiss";
              break;
            case 48 /*0x30*/:
              family = "modern";
              break;
            case 64 /*0x40*/:
              family = "script";
              break;
            case 80 /*0x50*/:
              family = "decor";
              break;
            default:
              family = "nil";
              break;
          }
          if (flag)
          {
            flag = false;
            if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "fonttbl", 0, 0.0))
              return false;
          }
          this.WriteRtfOneFont(rtf, RtfIndex2, typeFace, family, (int) this.e.TerFont[index4].CharSet);
          this.e.TerFont[index4].RtfIndex = RtfIndex2;
          ++RtfIndex2;
          rtf.WritingControl = false;
        }
      }
    }
    for (int index6 = 0; index6 < this.e.TotalSID; ++index6)
    {
      this.e.StyleId[index6].RtfIndex = 0;
      if (this.e.StyleId[index6].InUse && (!this.e.ShortRtf || realUsedStyles[index6]) && this.e.StyleId[index6].TypeFace.Length != 0)
      {
        int index7 = 0;
        while (index7 < this.e.TotalFonts && (!this.e.TerFont[index7].InUse || this.e.ShortRtf && !realUsedFonts[index7] && !(this.e.TerFont[index7].TypeFace == "Symbol") && !(this.e.TerFont[index7].TypeFace == "Wingdings") || !(this.e.TerFont[index7].TypeFace == this.e.StyleId[index6].TypeFace) || (int) this.e.TerFont[index7].FontFamily != (int) this.e.StyleId[index6].FontFamily && this.e.StyleId[index6].FontFamily != (byte) 0))
          ++index7;
        if (index7 < this.e.TotalFonts)
        {
          this.e.StyleId[index6].RtfIndex = this.e.TerFont[index7].RtfIndex;
        }
        else
        {
          int index8 = 0;
          while (index8 < index6 && (!this.e.StyleId[index8].InUse || !(this.e.StyleId[index8].TypeFace == this.e.StyleId[index6].TypeFace) || (int) this.e.StyleId[index8].FontFamily != (int) this.e.StyleId[index6].FontFamily))
            ++index8;
          if (index8 < index6)
          {
            this.e.StyleId[index6].RtfIndex = this.e.StyleId[index8].RtfIndex;
          }
          else
          {
            string typeFace = this.e.StyleId[index6].TypeFace;
            string family;
            switch (this.e.StyleId[index6].FontFamily)
            {
              case 16 /*0x10*/:
                family = "roman";
                break;
              case 32 /*0x20*/:
                family = "swiss";
                break;
              case 48 /*0x30*/:
                family = "modern";
                break;
              case 64 /*0x40*/:
                family = "script";
                break;
              case 80 /*0x50*/:
                family = "decor";
                break;
              default:
                family = "nil";
                break;
            }
            if (flag)
            {
              flag = false;
              if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "fonttbl", 0, 0.0))
                return false;
            }
            this.WriteRtfOneFont(rtf, RtfIndex2, typeFace, family, 1);
            this.e.StyleId[index6].RtfIndex = RtfIndex2;
            ++RtfIndex2;
            rtf.WritingControl = false;
          }
        }
      }
    }
    if (!flag && !this.EndRtfGroup(rtf))
      return false;
    this.FlushRtfLine(rtf);
    return true;
  }

  internal bool WriteRtfFontAttrib(tc.ClsRtfOut rtf, int CurFont)
  {
    int flag1 = 39936;
    bool flag2 = false;
    if ((this.e.TerFlags3 & 4096 /*0x1000*/) != 0)
      flag1 |= 8192 /*0x2000*/;
    if (CurFont < this.e.TotalFonts && CurFont >= 0)
    {
      string typeFace = this.e.TerFont[CurFont].TypeFace;
      byte fontFamily = this.e.TerFont[CurFont].FontFamily;
      int style1 = this.e.TerFont[CurFont].style;
      Color textColor = this.e.TerFont[CurFont].TextColor;
      Color textBkColor = this.e.TerFont[CurFont].TextBkColor;
      Color ulineColor = this.e.TerFont[CurFont].UlineColor;
      int charStyId = this.e.TerFont[CurFont].CharStyId;
      int num1 = this.e.TerFont[CurFont].TwipsSize;
      if (num1 == 0 && (this.e.TerFont[CurFont].style & 128 /*0x80*/) != 0)
        num1 = this.e.TerArg.PointSize * 20;
      int auxId = this.e.TerFont[CurFont].AuxId;
      int expand = this.e.TerFont[CurFont].expand;
      int lang = this.e.TerFont[CurFont].lang;
      int charSet = (int) this.e.TerFont[CurFont].CharSet;
      int offset = this.e.TerFont[CurFont].offset;
      int insRev = this.e.TerFont[CurFont].InsRev;
      int delRev = this.e.TerFont[CurFont].DelRev;
      tc.ClsDateTime insTime = this.e.TerFont[CurFont].InsTime;
      tc.ClsDateTime delTime = this.e.TerFont[CurFont].DelTime;
      if ((this.e.TerFlags2 & 32768 /*0x8000*/) != 0)
      {
        if ((style1 & 64 /*0x40*/) != 0)
          rtf.flags |= 256 /*0x0100*/;
        else
          rtf.flags &= -257;
      }
      tc.StrRtfOutGroup strRtfOutGroup = rtf.group[rtf.GroupLevel];
      if (strRtfOutGroup.FontId < 0 || (rtf.flags & 16384 /*0x4000*/) != 0 || (rtf.group[rtf.GroupLevel].flags & 1) != 0)
      {
        strRtfOutGroup.FontId = 0;
        flag2 = true;
        rtf.flags &= -16385;
      }
      int PrevStyle = strRtfOutGroup.style & flag1;
      int fontId = strRtfOutGroup.FontId;
      string strB;
      byte num2;
      Color color1;
      Color color2;
      Color color3;
      int num3;
      int num4;
      int num5;
      int num6;
      int num7;
      int num8;
      int num9;
      int num10;
      int num11;
      if (flag2)
      {
        strB = "";
        num2 = (byte) 0;
        color1 = tc.CLR_AUTO;
        color2 = tc.CLR_WHITE;
        color3 = tc.CLR_AUTO;
        num3 = 1;
        num4 = 0;
        num5 = 0;
        num6 = 0;
        num7 = 0;
        num8 = 1;
        num9 = 0;
        num10 = 0;
        num11 = 0;
      }
      else
      {
        strB = this.e.TerFont[fontId].TypeFace;
        num2 = this.e.TerFont[fontId].FontFamily;
        int style2 = this.e.TerFont[fontId].style;
        tc.ResetUintFlag(ref style2, flag1);
        PrevStyle |= style2;
        color1 = this.e.TerFont[fontId].TextColor;
        color2 = this.e.TerFont[fontId].TextBkColor;
        color3 = this.e.TerFont[fontId].UlineColor;
        num3 = this.e.TerFont[fontId].CharStyId;
        num4 = this.e.TerFont[fontId].TwipsSize;
        num5 = this.e.TerFont[fontId].AuxId;
        num6 = this.e.TerFont[fontId].expand;
        num7 = this.e.TerFont[fontId].lang;
        num8 = (int) this.e.TerFont[fontId].CharSet;
        num9 = this.e.TerFont[fontId].offset;
        num10 = this.e.TerFont[fontId].InsRev;
        num11 = this.e.TerFont[fontId].DelRev;
        ref tc.StrFont local1 = ref this.e.TerFont[fontId];
        ref tc.StrFont local2 = ref this.e.TerFont[fontId];
      }
      if (!flag2 && strRtfOutGroup.FontId == CurFont && strRtfOutGroup.style == (this.e.TerFont[CurFont].style & flag1))
        return true;
      strRtfOutGroup.FontId = CurFont;
      strRtfOutGroup.style = this.e.TerFont[CurFont].style & flag1;
      rtf.group[rtf.GroupLevel] = strRtfOutGroup;
      if (typeFace == this.e.TerArg.FontTypeFace && num1 == this.e.TerArg.PointSize * 20 && (textColor == Color.Black || textColor == tc.CLR_AUTO) && textBkColor == tc.CLR_WHITE && ulineColor == tc.CLR_AUTO && (style1 & ~flag1) == 0 && charStyId == 1 && lang == 0 && offset == 0 && auxId == 0 && expand == 0 && insRev == 0 && delRev == 0)
      {
        if ((!this.e.ShortRtf || CurFont != 0) && (!this.WriteRtfControl(rtf, "plain", 0, 0.0) || CurFont >= 0 && !this.WriteRtfControl(rtf, "f", 1, (double) this.e.TerFont[CurFont].RtfIndex) || !this.WriteRtfControl(rtf, "fs", 1, (double) (num1 / 10))))
          return false;
        rtf.flags &= -257;
        return true;
      }
      if (charStyId != num3 && !this.WriteRtfControl(rtf, "cs", 1, (double) charStyId) || lang != num7 && (!this.e.ShortRtf || this.e.TerFont[CurFont].lang != 0 && this.e.DefLang != this.e.TerFont[CurFont].lang) && !this.WriteRtfControl(rtf, "lang", 1, this.e.TerFont[CurFont].lang != 0 ? (double) this.e.TerFont[CurFont].lang : (double) this.e.DefLang) || (!this.e.ShortRtf || CurFont != 0) && (string.Compare(typeFace, strB, true) != 0 || (int) fontFamily != (int) num2 || lang != num7 || charSet != num8) && !this.WriteRtfControl(rtf, "f", 1, (double) this.e.TerFont[CurFont].RtfIndex))
        return false;
      if (!(this.e.ShortRtf & flag2) && textColor != color1)
      {
        int val = 0;
        while (val < rtf.TotalColors && !(rtf.color[val].color == textColor))
          ++val;
        if (val == rtf.TotalColors)
          val = 0;
        if (!this.WriteRtfControl(rtf, "cf", 1, (double) val))
          return false;
      }
      if (textBkColor != color2)
      {
        int val = 0;
        while (val < rtf.TotalColors && !(rtf.color[val].color == textBkColor))
          ++val;
        if (val == rtf.TotalColors)
          val = 0;
        if (!this.WriteRtfControl(rtf, "highlight", 1, (double) val))
          return false;
      }
      if (ulineColor != color3)
      {
        int val = 0;
        while (val < rtf.TotalColors && !(rtf.color[val].color == ulineColor))
          ++val;
        if (val == rtf.TotalColors)
          val = 0;
        if (!this.WriteRtfControl(rtf, "ulc", 1, (double) val))
          return false;
      }
      if (!(this.e.ShortRtf & flag2) && num1 != num4 && !this.WriteRtfControl(rtf, "fs", 1, (double) (num1 / 10)) || auxId != num5 && !this.e.HtmlMode && !this.WriteRtfControl(rtf, "sscharaux", 1, (double) auxId) || expand != num6 && (!this.WriteRtfControl(rtf, "expnd", 1, (double) (expand * 4 / 20)) || !this.WriteRtfControl(rtf, "expndtw", 1, (double) expand)))
        return false;
      if (offset != num9)
      {
        if (offset == 0)
          this.WriteRtfControl(rtf, num9 > 0 ? "up" : "dn", 1, 0.0);
        else
          this.WriteRtfControl(rtf, offset > 0 ? "up" : "dn", 1, (double) this.TwipsToPoints(Math.Abs(offset) * 2));
      }
      if (insRev != num10 && (rtf.output < 2 || !this.e.TrackChanges))
      {
        if (insRev != 0)
        {
          if (!this.WriteRtfControl(rtf, "revised", 0, 0.0) || !this.WriteRtfControl(rtf, "revauth", 1, (double) insRev) || !this.WriteRtfControl(rtf, "revdttm", 1, (double) this.GetRtfTrackingTime(insTime)))
            return false;
        }
        else if (!this.WriteRtfControl(rtf, "revised", 1, 0.0))
          return false;
      }
      if (delRev != num11)
      {
        if (rtf.output < 2 || !this.e.TrackChanges)
        {
          if (delRev != 0)
          {
            if (!this.WriteRtfControl(rtf, "deleted", 0, 0.0) || !this.WriteRtfControl(rtf, "revauthdel", 1, (double) delRev) || !this.WriteRtfControl(rtf, "revdttmdel", 1, (double) this.GetRtfTrackingTime(delTime)))
              return false;
          }
          else if (!this.WriteRtfControl(rtf, "deleted", 1, 0.0))
            return false;
        }
        else if (delRev != 0)
          ++rtf.DelRevCount;
        else
          --rtf.DelRevCount;
      }
      if (!this.WriteRtfFontStyle(rtf, style1, PrevStyle))
        return false;
    }
    return true;
  }

  internal bool WriteRtfFontStyle(tc.ClsRtfOut rtf, int CurStyle, int PrevStyle)
  {
    if ((CurStyle & 2) != 0 != ((PrevStyle & 2) != 0) && !((CurStyle & 2) == 0 ? this.WriteRtfControl(rtf, "b", 1, 0.0) : this.WriteRtfControl(rtf, "b", 0, 0.0)) || (CurStyle & 16384 /*0x4000*/) != 0 != ((PrevStyle & 16384 /*0x4000*/) != 0) && !((CurStyle & 16384 /*0x4000*/) == 0 ? this.WriteRtfControl(rtf, "sshlink", 1, 0.0) : this.WriteRtfControl(rtf, "sshlink", 0, 0.0)) || (CurStyle & 1) != 0 != ((PrevStyle & 1) != 0) && !((CurStyle & 1) == 0 ? this.WriteRtfControl(rtf, "ul", 1, 0.0) : this.WriteRtfControl(rtf, "ul", 0, 0.0)) || (CurStyle & 256 /*0x0100*/) != 0 != ((PrevStyle & 256 /*0x0100*/) != 0) && !((CurStyle & 256 /*0x0100*/) == 0 ? this.WriteRtfControl(rtf, "uldb", 1, 0.0) : this.WriteRtfControl(rtf, "uldb", 0, 0.0)) || (CurStyle & 4) != 0 != ((PrevStyle & 4) != 0) && !((CurStyle & 4) == 0 ? this.WriteRtfControl(rtf, "i", 1, 0.0) : this.WriteRtfControl(rtf, "i", 0, 0.0)))
      return false;
    if ((this.e.TerFlags2 & 32768 /*0x8000*/) != 0)
    {
      if ((CurStyle & 64 /*0x40*/) != 0)
        rtf.flags |= 256 /*0x0100*/;
      else
        tc.ResetUintFlag(ref rtf.flags, 256 /*0x0100*/);
    }
    else if ((CurStyle & 64 /*0x40*/) != 0 != ((PrevStyle & 64 /*0x40*/) != 0) && !((CurStyle & 64 /*0x40*/) == 0 ? this.WriteRtfControl(rtf, "v", 1, 0.0) : this.WriteRtfControl(rtf, "v", 0, 0.0)))
      return false;
    if ((CurStyle & 8) != 0 != ((PrevStyle & 8) != 0) && !((CurStyle & 8) == 0 ? this.WriteRtfControl(rtf, "strike", 1, 0.0) : this.WriteRtfControl(rtf, "strike", 0, 0.0)) || (CurStyle & 524288 /*0x080000*/) != 0 != ((PrevStyle & 524288 /*0x080000*/) != 0) && !((CurStyle & 524288 /*0x080000*/) == 0 ? this.WriteRtfControl(rtf, "striked", 1, 0.0) : this.WriteRtfControl(rtf, "striked", 0, 0.0)) || (CurStyle & 65536 /*0x010000*/) != 0 != ((PrevStyle & 65536 /*0x010000*/) != 0) && !((CurStyle & 65536 /*0x010000*/) == 0 ? this.WriteRtfControl(rtf, "caps", 1, 0.0) : this.WriteRtfControl(rtf, "caps", 0, 0.0)) || (CurStyle & 131072 /*0x020000*/) != 0 != ((PrevStyle & 131072 /*0x020000*/) != 0) && !((CurStyle & 131072 /*0x020000*/) == 0 ? this.WriteRtfControl(rtf, "scaps", 1, 0.0) : this.WriteRtfControl(rtf, "scaps", 0, 0.0)) || (CurStyle & 512 /*0x0200*/) != 0 != ((PrevStyle & 512 /*0x0200*/) != 0) && !((CurStyle & 512 /*0x0200*/) == 0 ? this.WriteRtfControl(rtf, "protect", 1, 0.0) : this.WriteRtfControl(rtf, "protect", 0, 0.0)))
      return false;
    if ((CurStyle & 8192 /*0x2000*/) != 0 != ((PrevStyle & 8192 /*0x2000*/) != 0))
    {
      if ((CurStyle & 8192 /*0x2000*/) != 0)
      {
        if (!this.WriteRtfControl(rtf, "chbrdr", 0, 0.0) || !this.WriteRtfControl(rtf, "brdrs", 0, 0.0) || !this.WriteRtfControl(rtf, "brdrw", 1, 10.0))
          return false;
      }
      else if (!this.WriteRtfControl(rtf, "chbrdr", 1, 0.0))
        return false;
    }
    return true;
  }

  internal bool WriteRtfForm(tc.ClsRtfOut rtf, int pict)
  {
    string text = "";
    tc.ClsForm form = this.e.TerFont[pict].form;
    if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "field", 0, 0.0) || !this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "*", 0, 0.0) || !this.WriteRtfControl(rtf, "fldinst", 0, 0.0))
      return false;
    if (this.e.TerFont[pict].FieldId == 3)
      text = "FORMCHECKBOX";
    if (this.e.TerFont[pict].FieldId == 4)
      text = "FORMDROPDOWN";
    if (text.Length > 0 && (!this.BeginRtfGroup(rtf) || !this.WriteRtfText(rtf, text, text.Length) || !this.EndRtfGroup(rtf)) || !this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "*", 0, 0.0) || !this.WriteRtfControl(rtf, "formfield", 0, 0.0))
      return false;
    int val = 0;
    if (this.e.TerFont[pict].FieldId == 3)
      val = 1;
    if (this.e.TerFont[pict].FieldId == 4)
      val = 2;
    if (val == 0 || !this.WriteRtfControl(rtf, "fftype", 1, (double) val))
      return false;
    if (this.e.TerFont[pict].FieldId == 3 && this.e.TerFont[pict].ctl != null)
    {
      CheckBox ctl = (CheckBox) this.e.TerFont[pict].ctl;
      if (!this.WriteRtfControl(rtf, "ffres", 1, ctl.Checked ? 1.0 : 0.0) || !this.WriteRtfControl(rtf, "ffhps", 1, (double) (2 * this.e.TerFont[pict].PictHeight / 20)))
        return false;
    }
    return this.BeginRtfGroup(rtf) && this.WriteRtfControl(rtf, "*", 0, 0.0) && this.WriteRtfControl(rtf, "ffname", 0, 0.0) && this.WriteRtfText(rtf, form.name, form.name.Length) && this.EndRtfGroup(rtf) && this.EndRtfGroup(rtf) && this.EndRtfGroup(rtf) && this.BeginRtfGroup(rtf) && this.WriteRtfControl(rtf, "fldrslt", 0, 0.0) && this.EndRtfGroup(rtf) && this.EndRtfGroup(rtf);
  }

  internal bool WriteRtfFormTextField(tc.ClsRtfOut rtf, string FieldCode)
  {
    if (FieldCode == null)
      return false;
    string text = "FORMTEXT";
    if (!this.BeginRtfGroup(rtf) || !this.WriteRtfText(rtf, text, text.Length) || !this.EndRtfGroup(rtf) || !this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "*", 0, 0.0) || !this.WriteRtfControl(rtf, "formfield", 0, 0.0))
      return false;
    int val1 = 0;
    if (!this.WriteRtfControl(rtf, "fftype", 1, (double) val1))
      return false;
    int val2 = this.ToInt(this.GetStringField(FieldCode, 0, '|'));
    if (val2 > 0 && !this.WriteRtfControl(rtf, "ffmaxlen", 1, (double) val2) || !this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "*", 0, 0.0) || !this.WriteRtfControl(rtf, "ffdeftext", 0, 0.0) || !this.WriteRtfText(rtf, " ", 1) || !this.EndRtfGroup(rtf) || !this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "*", 0, 0.0) || !this.WriteRtfControl(rtf, "ffname", 0, 0.0))
      return false;
    string stringField = this.GetStringField(FieldCode, 1, '|');
    return this.WriteRtfText(rtf, stringField, stringField.Length) && this.EndRtfGroup(rtf) && this.EndRtfGroup(rtf);
  }

  internal bool WriteRtfFrameInfo(tc.ClsRtfOut rtf, int CurParaFID)
  {
    if (CurParaFID != 0)
    {
      if ((this.e.ParaFrame[CurParaFID].flags & 32 /*0x20*/) != 0 && !this.WriteRtfControl(rtf, "pvpg", 0, 0.0) || (this.e.ParaFrame[CurParaFID].flags & 64 /*0x40*/) != 0 && !this.WriteRtfControl(rtf, "pvmrg", 0, 0.0) || (this.e.ParaFrame[CurParaFID].flags & 96 /*0x60*/) == 0 && !this.WriteRtfControl(rtf, "pvpara", 0, 0.0) || (this.e.ParaFrame[CurParaFID].flags & 1) == 0 && !this.WriteRtfControl(rtf, "phmrg", 0, 0.0) || !this.WriteRtfControl(rtf, "posx", 1, (double) this.e.ParaFrame[CurParaFID].x) || !this.WriteRtfControl(rtf, "posy", 1, (double) this.e.ParaFrame[CurParaFID].ParaY))
        return false;
      int val = this.e.ParaFrame[CurParaFID].MinHeight;
      if ((this.e.ParaFrame[CurParaFID].flags & 67108864 /*0x04000000*/) != 0)
        val = -val;
      if (!this.WriteRtfControl(rtf, "absh", 1, (double) val) || !this.WriteRtfControl(rtf, "absw", 1, (double) this.e.ParaFrame[CurParaFID].width) || !this.WriteRtfControl(rtf, "dxfrtext", 1, (double) this.e.ParaFrame[CurParaFID].DistFromText) || !this.WriteRtfControl(rtf, "dfrmtxtx", 1, (double) this.e.ParaFrame[CurParaFID].DistFromText) || !this.WriteRtfControl(rtf, "dfrmtxty", 1, 0.0) || (this.e.ParaFrame[CurParaFID].flags & 8192 /*0x2000*/) != 0 && !this.WriteRtfControl(rtf, "nowrap", 0, 0.0) || this.e.ParaFrame[CurParaFID].TextAngle == 90 && !this.WriteRtfControl(rtf, "frmtxbtlr", 0, 0.0) || this.e.ParaFrame[CurParaFID].TextAngle == 270 && !this.WriteRtfControl(rtf, "frmtxtblr", 0, 0.0))
        return false;
    }
    return true;
  }

  internal bool WriteRtfLinkedPicture(tc.ClsRtfOut rtf, int pict)
  {
    if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "sslinkpictw", 1, (double) this.e.TerFont[pict].PictWidth) || !this.WriteRtfControl(rtf, "sslinkpicth", 1, (double) this.e.TerFont[pict].PictHeight) || !this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "field", 0, 0.0) || !this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "fldinst", 0, 0.0))
      return false;
    this.e.TempString = "INCLUDEPICTURE ";
    if (!this.WriteRtfText(rtf, this.e.TempString, this.e.TempString.Length))
      return false;
    string InStr = this.e.TerFont[pict].LinkFile;
    if ((this.e.TerFlags6 & 16 /*0x10*/) != 0)
    {
      int length = InStr.Length;
      if (length > 0)
      {
        int index = length - 1;
        while (index >= 0 && InStr[index] != '\\' && InStr[index] != ':')
          --index;
        if (index >= 0)
          InStr = InStr.Substring(index + 1);
      }
    }
    string OutStr;
    this.AddSlashes(InStr, out OutStr, 4);
    if (OutStr.IndexOf(" ") >= 0)
      this.StrQuote(ref OutStr);
    string text = OutStr + " \\\\d";
    rtf.flags |= 2;
    if (!this.WriteRtfText(rtf, text, text.Length))
      return false;
    rtf.flags = tc.ResetUintFlag(ref rtf.flags, 2);
    return this.EndRtfGroup(rtf) && this.BeginRtfGroup(rtf) && this.WriteRtfControl(rtf, "fldrslt", 0, 0.0) && this.EndRtfGroup(rtf) && this.EndRtfGroup(rtf) && this.EndRtfGroup(rtf);
  }

  internal bool WriteRtfList(tc.ClsRtfOut rtf)
  {
    rtf.XlateLs[0] = 0;
    if (this.e.TotalLists != 1 || this.e.TotalListOr != 1)
    {
      this.FlushRtfLine(rtf);
      if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "*", 0, 0.0) || !this.WriteRtfControl(rtf, "listtable", 0, 0.0))
        return false;
      for (int index1 = 1; index1 < this.e.TotalLists; ++index1)
      {
        if (this.e.list[index1].InUse)
        {
          this.FlushRtfLine(rtf);
          if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "list", 0, 0.0) || !this.WriteRtfControl(rtf, "listtemplateid", 1, (double) this.e.list[index1].TmplId) || this.e.list[index1].LevelCount == 1 && !this.WriteRtfControl(rtf, "listsimple", 0, 0.0) || (this.e.list[index1].flags & 1) != 0 && !this.WriteRtfControl(rtf, "listrestarthdn", 0, 0.0))
            return false;
          for (int index2 = 0; index2 < this.e.list[index1].LevelCount; ++index2)
          {
            if (!this.WriteRtfListLevel(rtf, this.e.list[index1].level[index2]))
              return false;
          }
          if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "listname", 0, 0.0))
            return false;
          string text = (!this.True(this.e.list[index1].name) ? "" : this.e.list[index1].name) + ";";
          if (!this.WriteRtfText(rtf, text, text.Length) || !this.EndRtfGroup(rtf) || !this.WriteRtfControl(rtf, "listid", 1, (double) this.e.list[index1].id))
            return false;
          this.FlushRtfLine(rtf);
          if (!this.EndRtfGroup(rtf))
            return false;
          this.FlushRtfLine(rtf);
        }
      }
      if (!this.EndRtfGroup(rtf))
        return false;
      this.FlushRtfLine(rtf);
      this.FlushRtfLine(rtf);
      if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "*", 0, 0.0) || !this.WriteRtfControl(rtf, "listoverridetable", 0, 0.0))
        return false;
      int val = 1;
      for (int index3 = 1; index3 < this.e.TotalListOr; ++index3)
      {
        if (this.e.ListOr[index3].InUse)
        {
          int index4 = 0;
          while (index4 < this.e.TotalBlts && this.e.TerBlt[index4].ls != index3)
            ++index4;
          if (index4 < this.e.TotalBlts)
          {
            this.FlushRtfLine(rtf);
            if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "listoverride", 0, 0.0))
              return false;
            int id = this.e.list[this.e.ListOr[index3].ListIdx].id;
            if (!this.WriteRtfControl(rtf, "listid", 1, (double) id) || !this.WriteRtfControl(rtf, "listoverridecount", 1, (double) this.e.ListOr[index3].LevelCount))
              return false;
            for (int index5 = 0; index5 < this.e.ListOr[index3].LevelCount; ++index5)
            {
              this.FlushRtfLine(rtf);
              if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "lfolevel", 0, 0.0) || (this.e.ListOr[index3].level[index5].flags & 16 /*0x10*/) != 0 && !this.WriteRtfControl(rtf, "listoverrideformat", 0, 0.0) || (this.e.ListOr[index3].level[index5].flags & 1) != 0 && !this.WriteRtfControl(rtf, "listoverridestartat", 0, 0.0) || !this.WriteRtfListLevel(rtf, this.e.ListOr[index3].level[index5]) || !this.EndRtfGroup(rtf))
                return false;
              this.FlushRtfLine(rtf);
            }
            if (!this.WriteRtfControl(rtf, "ls", 1, (double) val) || !this.EndRtfGroup(rtf))
              return false;
            this.FlushRtfLine(rtf);
            rtf.XlateLs[index3] = val;
            ++val;
          }
        }
      }
      if (!this.EndRtfGroup(rtf))
        return false;
      this.FlushRtfLine(rtf);
    }
    return true;
  }

  internal bool WriteRtfListLevel(tc.ClsRtfOut rtf, tc.StrListLevel pLevel)
  {
    this.FlushRtfLine(rtf);
    if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "listlevel", 0, 0.0) || !this.WriteRtfControl(rtf, "levelnfc", 1, (double) pLevel.NumType) || !this.WriteRtfControl(rtf, "levelfollow", 1, (double) pLevel.CharAft) || !this.WriteRtfControl(rtf, "levelstartat", 1, (double) pLevel.start) || (pLevel.flags & 32 /*0x20*/) != 0 && !this.WriteRtfControl(rtf, "levelnorestart", 0, 0.0) || pLevel.MinIndent > 0 && !this.WriteRtfControl(rtf, "levelindent", 1, (double) pLevel.MinIndent) || (pLevel.flags & 2) != 0 && !this.WriteRtfControl(rtf, "levelold", 0, 0.0) || (pLevel.flags & 8) != 0 && !this.WriteRtfControl(rtf, "levellegal", 0, 0.0) || !this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "leveltext", 0, 0.0))
      return false;
    int num = (int) pLevel.text[0] + 1;
    for (int idx = 0; idx < num; ++idx)
    {
      bool flag = idx == 0 || pLevel.text[idx] < '\t';
      rtf.flags |= 32 /*0x20*/;
      if (flag && !this.PutRtfSpecChar(rtf, pLevel.text[idx]) || !flag && !this.WriteRtfText(rtf, this.CopyArray(pLevel.text, idx), 1))
        return false;
    }
    tc.ResetUintFlag(ref rtf.flags, 32 /*0x20*/);
    if (!this.EndRtfGroup(rtf) || !this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "levelnumbers", 0, 0.0))
      return false;
    for (int CurChar = 0; CurChar < num; ++CurChar)
    {
      if (CurChar != 0 && pLevel.text[CurChar] < '\t' && !this.PutRtfSpecChar(rtf, (char) CurChar))
        return false;
    }
    if (!this.EndRtfGroup(rtf))
      return false;
    int fontId = pLevel.FontId;
    if (fontId > 0)
    {
      if (!this.WriteRtfControl(rtf, "f", 1, (double) this.e.TerFont[fontId].RtfIndex) || !this.WriteRtfControl(rtf, "fs", 1, (double) (this.e.TerFont[fontId].TwipsSize / 10)) || !this.WriteRtfFontStyle(rtf, this.e.TerFont[fontId].style, 0))
        return false;
      Color textColor = this.e.TerFont[fontId].TextColor;
      if (!this.IsSameColor(textColor, tc.CLR_AUTO) && !this.IsSameColor(textColor, tc.CLR_BLACK))
      {
        int val = 0;
        while (val < rtf.TotalColors && !(rtf.color[val].color == textColor))
          ++val;
        if (val == rtf.TotalColors)
          val = 0;
        if (!this.WriteRtfControl(rtf, "cf", 1, (double) val))
          return false;
      }
    }
    if (pLevel.FirstIndent > 0 && !this.WriteRtfControl(rtf, "fi", 1, (double) pLevel.FirstIndent) || pLevel.LeftIndent > 0 && !this.WriteRtfControl(rtf, "li", 1, (double) pLevel.LeftIndent) || pLevel.RightIndent > 0 && !this.WriteRtfControl(rtf, "ri", 1, (double) pLevel.RightIndent) || !this.EndRtfGroup(rtf))
      return false;
    this.FlushRtfLine(rtf);
    return true;
  }

  internal bool WriteRtfMargin(tc.ClsRtfOut rtf)
  {
    this.FlushRtfLine(rtf);
    if (rtf.output < 2)
    {
      int index1 = 0;
      while (index1 < 11 && !this.True(this.e.pRtfInfo[index1]))
        ++index1;
      if (index1 < 11)
      {
        if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "info", 0, 0.0))
          return false;
        for (int index2 = 0; index2 < 11; ++index2)
        {
          if (!this.False(this.e.pRtfInfo[index2]))
          {
            if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, this.e.RtfInfo[index2], 0, 0.0))
              return false;
            this.WriteRtfText(rtf, this.e.pRtfInfo[index2], this.e.pRtfInfo[index2].Length);
            if (!this.EndRtfGroup(rtf))
              return false;
          }
        }
        if (!this.EndRtfGroup(rtf))
          return false;
      }
    }
    float x1 = 8.5f;
    float x2 = 11f;
    if (!this.WriteRtfControl(rtf, "paperw", 1, (double) this.InchesToTwips((double) x1)) || !this.WriteRtfControl(rtf, "paperh", 1, (double) this.InchesToTwips((double) x2)) || !this.WriteRtfControl(rtf, "margl", 1, (double) this.InchesToTwips((double) this.e.TerSect[0].LeftMargin)) || !this.WriteRtfControl(rtf, "margr", 1, (double) this.InchesToTwips((double) this.e.TerSect[0].RightMargin)) || !this.WriteRtfControl(rtf, "margt", 1, (double) this.InchesToTwips((double) this.e.TerSect[0].TopMargin * ((this.e.TerSect[0].flags & 8) != 0 ? -1.0 : 1.0))) || !this.WriteRtfControl(rtf, "margb", 1, (double) this.InchesToTwips((double) this.e.TerSect[0].BotMargin * ((this.e.TerSect[0].flags & 16 /*0x10*/) != 0 ? -1.0 : 1.0))) || !this.WriteRtfControl(rtf, "headery", 1, (double) this.InchesToTwips((double) this.e.TerSect[0].HdrMargin)) || !this.WriteRtfControl(rtf, "footery", 1, (double) this.InchesToTwips((double) this.e.TerSect[0].FtrMargin)) || !this.WriteRtfControl(rtf, "deftab", 1, (double) this.e.DefTabWidth) || !this.WriteRtfControl(rtf, "formshade", 0, 0.0) || this.e.ProtectForm && !this.WriteRtfControl(rtf, "formprot", 0, 0.0))
      return false;
    if (this.True(this.e.FootnoteNumFmt))
    {
      string control = "";
      if (this.e.FootnoteNumFmt == 3)
        control = "ftnnruc";
      else if (this.e.FootnoteNumFmt == 4)
        control = "ftnnrlc";
      else if (this.e.FootnoteNumFmt == 1)
        control = "ftnnauc";
      else if (this.e.FootnoteNumFmt == 2)
        control = "ftnnalc";
      if (control.Length > 0 && !this.WriteRtfControl(rtf, control, 0, 0.0))
        return false;
    }
    if (!this.WriteRtfControl(rtf, this.e.EndnoteAtSect ? "aendnotes" : "aenddoc", 0, 0.0))
      return false;
    if (this.True(this.e.EndnoteNumFmt))
    {
      string control = "";
      if (this.e.EndnoteNumFmt == 3)
        control = "aftnnruc";
      else if (this.e.EndnoteNumFmt == 4)
        control = "aftnnrlc";
      else if (this.e.EndnoteNumFmt == 1)
        control = "aftnnauc";
      else if (this.e.EndnoteNumFmt == 2)
        control = "aftnnalc";
      if (control.Length > 0 && !this.WriteRtfControl(rtf, control, 0, 0.0))
        return false;
    }
    if (!this.WriteRtfControl(rtf, "pgbrdrhead", 0, 0.0) || !this.WriteRtfControl(rtf, "pgbrdrfoot", 0, 0.0) || this.e.NoTabIndent && !this.WriteRtfControl(rtf, "notabind", 0, 0.0) || this.e.DocTextFlow == 2 && !this.WriteRtfControl(rtf, "rtldoc", 0, 0.0) || this.e.DocTextFlow == 1 && !this.WriteRtfControl(rtf, "ltrdoc", 0, 0.0) || this.e.TrackChanges && !this.WriteRtfControl(rtf, "revisions", 0, 0.0))
      return false;
    this.FlushRtfLine(rtf);
    return true;
  }

  internal bool WriteRtfMetafile(tc.ClsRtfOut rtf, int pict)
  {
    bool flag = true;
    this.FlushRtfLine(rtf);
    if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, nameof (pict), 0, 0.0) || !this.WriteRtfControl(rtf, "wmetafile", 1, 8.0))
      return false;
    int bmHeight = this.e.TerFont[pict].bmHeight;
    int bmWidth = this.e.TerFont[pict].bmWidth;
    int x1;
    int x2;
    if (this.e.TerFont[pict].OrigPictWidth > 0)
    {
      x1 = this.e.TerFont[pict].OrigPictHeight;
      x2 = this.e.TerFont[pict].OrigPictWidth;
    }
    else
    {
      x1 = this.e.TerFont[pict].bmHeight;
      x2 = this.e.TerFont[pict].bmWidth;
    }
    int num = this.MulDiv(32000, 1440, 2540);
    if (x2 > num)
      x2 = num;
    if (x1 > num)
      x1 = num;
    if (!this.WriteRtfControl(rtf, "picw", 1, (double) this.MulDiv(x2, 2540, 1440)) || !this.WriteRtfControl(rtf, "pich", 1, (double) this.MulDiv(x1, 2540, 1440)))
      return false;
    int val1 = x2;
    int val2 = this.MulDiv(this.e.TerFont[pict].PictWidth, 100, val1 - this.e.TerFont[pict].CropLeft - this.e.TerFont[pict].CropRight);
    int val3 = x1;
    int val4 = this.MulDiv(this.e.TerFont[pict].PictHeight, 100, val3 - this.e.TerFont[pict].CropTop - this.e.TerFont[pict].CropBot);
    if (this.e.TerFont[pict].CropLeft != 0 && !this.WriteRtfControl(rtf, "piccropl", 1, (double) this.e.TerFont[pict].CropLeft) || this.e.TerFont[pict].CropRight != 0 && !this.WriteRtfControl(rtf, "piccropr", 1, (double) this.e.TerFont[pict].CropRight) || this.e.TerFont[pict].CropTop != 0 && !this.WriteRtfControl(rtf, "piccropt", 1, (double) this.e.TerFont[pict].CropTop) || this.e.TerFont[pict].CropBot != 0 && !this.WriteRtfControl(rtf, "piccropb", 1, (double) this.e.TerFont[pict].CropBot))
      return false;
    if (x2 > 0)
    {
      if (!this.WriteRtfControl(rtf, "picwgoal", 1, (double) val1) || !this.WriteRtfControl(rtf, "picscalex", 1, (double) val2))
        return false;
    }
    else if (!this.WriteRtfControl(rtf, "picwgoal", 1, (double) val1) || !this.WriteRtfControl(rtf, "picscalex", 1, (double) val2))
      return false;
    if (x1 > 0)
    {
      if (!this.WriteRtfControl(rtf, "pichgoal", 1, (double) val3) || !this.WriteRtfControl(rtf, "picscaley", 1, (double) val4))
        return false;
    }
    else if (!this.WriteRtfControl(rtf, "pichgoal", 1, (double) val3) || !this.WriteRtfControl(rtf, "picscaley", 1, (double) val4))
      return false;
    if (!this.WriteRtfControl(rtf, "sspicalign", 1, (double) this.e.TerFont[pict].PictAlign))
      return false;
    byte[] numArray = this.e.TerFont[pict].PictData;
    if (numArray == null)
    {
      MemoryStream memoryStream = new MemoryStream();
      this.e.TerFont[pict].image.Save((Stream) memoryStream, ImageFormat.Wmf);
      numArray = memoryStream.GetBuffer();
      memoryStream.Close();
    }
    if ((this.e.TerFlags4 & 512 /*0x0200*/) != 0)
    {
      if (!this.WriteRtfControl(rtf, "bin", 1, (double) numArray.Length))
        return false;
      rtf.flags |= 2048 /*0x0800*/;
      rtf.SpacePending = false;
    }
    for (int index = 0; index < numArray.Length; ++index)
    {
      flag = (this.e.TerFlags4 & 512 /*0x0200*/) == 0 ? this.PutRtfHexChar(rtf, (char) numArray[index]) : this.PutRtfChar(rtf, (char) numArray[index]);
      if (!flag)
        break;
    }
    if ((this.e.TerFlags4 & 512 /*0x0200*/) != 0)
      tc.ResetUintFlag(ref rtf.flags, 2048 /*0x0800*/);
    if (!this.EndRtfGroup(rtf))
      return false;
    this.FlushRtfLine(rtf);
    return flag;
  }

  internal bool WriteRtfNoNestGroup(tc.ClsRtfOut rtf)
  {
    return this.BeginRtfGroup(rtf) && this.WriteRtfControl(rtf, "nonesttables", 0, 0.0) && this.WriteRtfControl(rtf, "par", 0, 0.0) && this.EndRtfGroup(rtf);
  }

  internal bool WriteRtfObject(tc.ClsRtfOut rtf, int obj) => true;

  internal bool WriteRtfOneFont(
    tc.ClsRtfOut rtf,
    int RtfIndex,
    string name,
    string family,
    int CharSet)
  {
    if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "f", 1, (double) RtfIndex))
      return false;
    rtf.WritingControl = true;
    return this.PutRtfChar(rtf, '\\') && this.PutRtfChar(rtf, 'f') && this.WriteRtfText(rtf, family, family.Length) && this.WriteRtfText(rtf, " ", 1) && (!this.e.mbcs && CharSet == 1 || this.WriteRtfControl(rtf, "fcharset", 1, (double) CharSet)) && this.WriteRtfText(rtf, name, name.Length) && this.WriteRtfText(rtf, ";", 1) && this.EndRtfGroup(rtf);
  }

  internal bool WriteRtfOrigImage(tc.ClsRtfOut rtf, int pict, Guid ImageType, byte[] pImage)
  {
    bool flag = true;
    if (ImageType == ImageFormat.Bmp.Guid || pImage == null)
    {
      MemoryStream memoryStream = new MemoryStream();
      this.e.TerFont[pict].image.Save((Stream) memoryStream, ImageFormat.Png);
      pImage = memoryStream.GetBuffer();
      memoryStream.Close();
      ImageType = ImageFormat.Png.Guid;
    }
    string control;
    if (ImageType == ImageFormat.Jpeg.Guid)
      control = "jpegblip";
    else if (ImageType == ImageFormat.Png.Guid)
    {
      control = "pngblip";
    }
    else
    {
      if (this.e.TerFont[pict].image == null)
        return false;
      try
      {
        MemoryStream memoryStream = new MemoryStream();
        this.e.TerFont[pict].image.Save((Stream) memoryStream, ImageFormat.Png);
        pImage = memoryStream.GetBuffer();
      }
      catch (Exception ex)
      {
        return false;
      }
      control = "pngblip";
    }
    int length = pImage.Length;
    this.FlushRtfLine(rtf);
    ref tc.StrFont local1 = ref this.e.TerFont[pict];
    ref tc.StrFont local2 = ref this.e.TerFont[pict];
    int twipsX = this.ScrToTwipsX(this.e.TerFont[pict].bmWidth);
    int twipsY = this.ScrToTwipsY(this.e.TerFont[pict].bmHeight);
    if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, nameof (pict), 0, 0.0) || !this.WriteRtfControl(rtf, control, 1, 0.0))
      return false;
    int val1 = twipsX;
    if (this.e.TerFont[pict].OrigPictWidth != 0)
      val1 = this.e.TerFont[pict].OrigPictWidth;
    if (val1 >= 32768 /*0x8000*/)
      val1 = 20000;
    int val2 = this.MulDiv(this.e.TerFont[pict].PictWidth, 100, val1 - this.e.TerFont[pict].CropLeft - this.e.TerFont[pict].CropRight);
    int val3 = twipsY;
    if (this.e.TerFont[pict].OrigPictHeight != 0)
      val3 = this.e.TerFont[pict].OrigPictHeight;
    if (val3 >= 32768 /*0x8000*/)
      val3 = 20000;
    int val4 = this.MulDiv(this.e.TerFont[pict].PictHeight, 100, val3 - this.e.TerFont[pict].CropTop - this.e.TerFont[pict].CropBot);
    if (this.e.TerFont[pict].CropLeft != 0 && !this.WriteRtfControl(rtf, "piccropl", 1, (double) this.e.TerFont[pict].CropLeft) || this.e.TerFont[pict].CropRight != 0 && !this.WriteRtfControl(rtf, "piccropr", 1, (double) this.e.TerFont[pict].CropRight) || this.e.TerFont[pict].CropTop != 0 && !this.WriteRtfControl(rtf, "piccropt", 1, (double) this.e.TerFont[pict].CropTop) || this.e.TerFont[pict].CropBot != 0 && !this.WriteRtfControl(rtf, "piccropb", 1, (double) this.e.TerFont[pict].CropBot) || !this.WriteRtfControl(rtf, "picw", 1, (double) this.e.TerFont[pict].bmWidth) || !this.WriteRtfControl(rtf, "pich", 1, (double) this.e.TerFont[pict].bmHeight) || !this.WriteRtfControl(rtf, "picwgoal", 1, (double) val1) || !this.WriteRtfControl(rtf, "pichgoal", 1, (double) val3) || !this.WriteRtfControl(rtf, "picscalex", 1, (double) val2) || !this.WriteRtfControl(rtf, "picscaley", 1, (double) val4) || !this.WriteRtfControl(rtf, "sspicalign", 1, (double) this.e.TerFont[pict].PictAlign))
      return false;
    if ((this.e.TerFlags4 & 512 /*0x0200*/) != 0)
    {
      if (!this.WriteRtfControl(rtf, "bin", 1, (double) length))
        return false;
      rtf.flags |= 2048 /*0x0800*/;
      rtf.SpacePending = false;
    }
    for (int index = 0; index < length; ++index)
    {
      flag = (this.e.TerFlags4 & 512 /*0x0200*/) == 0 ? this.PutRtfHexChar(rtf, (char) pImage[index]) : this.PutRtfChar(rtf, (char) pImage[index]);
      if (!flag)
        goto label_33;
    }
    if ((this.e.TerFlags4 & 512 /*0x0200*/) != 0)
      tc.ResetUintFlag(ref rtf.flags, 2048 /*0x0800*/);
label_33:
    if (!this.EndRtfGroup(rtf))
      return false;
    this.FlushRtfLine(rtf);
    return flag;
  }

  internal bool WriteRtfParaBorder(
    tc.ClsRtfOut rtf,
    int CurFlags,
    Color BorderColor,
    int CurParaFID)
  {
    if ((CurFlags & 65776 /*0x0100F0*/) == 65776 /*0x0100F0*/)
    {
      if (!this.WriteRtfControl(rtf, "box", 0, 0.0) || (CurFlags & 512 /*0x0200*/) != 0 && !this.WriteRtfControl(rtf, "brdrw", 1, 30.0) || (CurFlags & 256 /*0x0100*/) != 0 && !this.WriteRtfControl(rtf, "brdrdb", 0, 0.0) || (CurFlags & 256 /*0x0100*/) == 0 && !this.WriteRtfControl(rtf, "brdrs", 0, 0.0))
        return false;
      if (!this.IsSameColor(BorderColor, tc.CLR_AUTO))
        this.WriteRtfParaBorderColor(rtf, BorderColor);
    }
    else
    {
      if ((CurFlags & 16 /*0x10*/) != 0)
      {
        if (!this.WriteRtfControl(rtf, "brdrt", 0, 0.0) || (CurFlags & 512 /*0x0200*/) != 0 && !this.WriteRtfControl(rtf, "brdrw", 1, 30.0) || (CurFlags & 256 /*0x0100*/) != 0 && !this.WriteRtfControl(rtf, "brdrdb", 0, 0.0) || (CurFlags & 256 /*0x0100*/) == 0 && !this.WriteRtfControl(rtf, "brdrs", 0, 0.0))
          return false;
        if (!this.IsSameColor(BorderColor, tc.CLR_AUTO))
          this.WriteRtfParaBorderColor(rtf, BorderColor);
      }
      if ((CurFlags & 32 /*0x20*/) != 0)
      {
        if (!this.WriteRtfControl(rtf, "brdrb", 0, 0.0) || (CurFlags & 512 /*0x0200*/) != 0 && !this.WriteRtfControl(rtf, "brdrw", 1, 30.0) || (CurFlags & 256 /*0x0100*/) != 0 && !this.WriteRtfControl(rtf, "brdrdb", 0, 0.0) || (CurFlags & 256 /*0x0100*/) == 0 && !this.WriteRtfControl(rtf, "brdrs", 0, 0.0))
          return false;
        if (!this.IsSameColor(BorderColor, tc.CLR_AUTO))
          this.WriteRtfParaBorderColor(rtf, BorderColor);
      }
      if ((CurFlags & 65536 /*0x010000*/) != 0)
      {
        if (!this.WriteRtfControl(rtf, "brdrbtw", 0, 0.0) || (CurFlags & 512 /*0x0200*/) != 0 && !this.WriteRtfControl(rtf, "brdrw", 1, 30.0) || (CurFlags & 256 /*0x0100*/) != 0 && !this.WriteRtfControl(rtf, "brdrdb", 0, 0.0) || (CurFlags & 256 /*0x0100*/) == 0 && !this.WriteRtfControl(rtf, "brdrs", 0, 0.0))
          return false;
        if (!this.IsSameColor(BorderColor, tc.CLR_AUTO))
          this.WriteRtfParaBorderColor(rtf, BorderColor);
      }
      if ((CurFlags & 64 /*0x40*/) != 0)
      {
        if (!this.WriteRtfControl(rtf, "brdrl", 0, 0.0) || (CurFlags & 512 /*0x0200*/) != 0 && !this.WriteRtfControl(rtf, "brdrw", 1, 30.0) || (CurFlags & 256 /*0x0100*/) != 0 && !this.WriteRtfControl(rtf, "brdrdb", 0, 0.0) || (CurFlags & 256 /*0x0100*/) == 0 && !this.WriteRtfControl(rtf, "brdrs", 0, 0.0))
          return false;
        if (!this.IsSameColor(BorderColor, tc.CLR_AUTO))
          this.WriteRtfParaBorderColor(rtf, BorderColor);
      }
      if ((CurFlags & 128 /*0x80*/) != 0)
      {
        if (!this.WriteRtfControl(rtf, "brdrr", 0, 0.0) || (CurFlags & 512 /*0x0200*/) != 0 && !this.WriteRtfControl(rtf, "brdrw", 1, 30.0) || (CurFlags & 256 /*0x0100*/) != 0 && !this.WriteRtfControl(rtf, "brdrdb", 0, 0.0) || (CurFlags & 256 /*0x0100*/) == 0 && !this.WriteRtfControl(rtf, "brdrs", 0, 0.0))
          return false;
        if (!this.IsSameColor(BorderColor, tc.CLR_AUTO))
          this.WriteRtfParaBorderColor(rtf, BorderColor);
      }
    }
    if ((CurFlags & 65776 /*0x0100F0*/) != 0 && CurParaFID > 0 && (this.e.ParaFrame[CurParaFID].flags & 896) == 0)
    {
      int val = this.e.ParaFrame[CurParaFID].margin;
      if (val < 0)
        val = 0;
      if (!this.WriteRtfControl(rtf, "brsp", 1, (double) val))
        return false;
    }
    return true;
  }

  internal bool WriteRtfParaBorderColor(tc.ClsRtfOut rtf, Color BorderColor)
  {
    if (!this.IsSameColor(BorderColor, tc.CLR_AUTO))
    {
      int val = 0;
      while (val < rtf.TotalColors && !this.IsSameColor(rtf.color[val].color, BorderColor))
        ++val;
      if (val == rtf.TotalColors)
        val = 0;
      if (!this.WriteRtfControl(rtf, "brdrcf", 1, (double) val))
        return false;
    }
    return true;
  }

  internal bool WriteRtfParaFmt(
    tc.ClsRtfOut rtf,
    int NewPfmt,
    int PrevPfmt,
    int NewCell,
    int PrevCell,
    int NewFID,
    int PrevFID,
    int line)
  {
    int num1 = 0;
    int num2 = 0;
    int num3 = 0;
    int num4 = 0;
    int num5 = 0;
    int flags1 = 0;
    int num6 = 0;
    int num7 = 0;
    int num8 = 0;
    int num9 = 0;
    int num10 = 0;
    int num11 = 0;
    int num12 = 0;
    int num13 = 0;
    Color color1 = tc.CLR_WHITE;
    Color clr2 = tc.CLR_AUTO;
    bool flag1 = false;
    bool flag2 = false;
    if (PrevPfmt < this.e.TotalPfmts && NewPfmt < this.e.TotalPfmts && NewPfmt >= 0)
    {
      if (PrevPfmt > 0)
      {
        num1 = this.e.PfmtId[PrevPfmt].LeftIndentTwips;
        num2 = this.e.PfmtId[PrevPfmt].RightIndentTwips;
        num3 = this.e.PfmtId[PrevPfmt].FirstIndentTwips;
        flags1 = this.e.PfmtId[PrevPfmt].flags;
        if (this.True(PrevFID) && this.True(PrevCell))
          tc.ResetUintFlag(ref flags1, 65776 /*0x0100F0*/);
        tc.ResetUintFlag(ref flags1, 12288 /*0x3000*/);
        num4 = this.e.PfmtId[PrevPfmt].TabId;
        num5 = this.e.PfmtId[PrevPfmt].BltId;
        num6 = this.e.PfmtId[PrevPfmt].shading;
        num11 = this.e.PfmtId[PrevPfmt].StyId;
        num7 = this.e.PfmtId[PrevPfmt].SpaceBefore;
        num8 = this.e.PfmtId[PrevPfmt].SpaceAfter;
        num9 = this.e.PfmtId[PrevPfmt].SpaceBetween;
        num10 = this.e.PfmtId[PrevPfmt].LineSpacing;
        color1 = this.e.PfmtId[PrevPfmt].BkColor;
        clr2 = this.e.PfmtId[PrevPfmt].BorderColor;
        num13 = this.e.PfmtId[PrevPfmt].flow;
        num12 = this.e.PfmtId[PrevPfmt].pflags & 65520;
      }
      int index1 = PrevCell;
      int row1 = this.e.cell[index1].row;
      int level1 = this.e.cell[PrevCell].level;
      int index2 = PrevFID;
      int leftIndentTwips = this.e.PfmtId[NewPfmt].LeftIndentTwips;
      int rightIndentTwips = this.e.PfmtId[NewPfmt].RightIndentTwips;
      int firstIndentTwips = this.e.PfmtId[NewPfmt].FirstIndentTwips;
      int flags2 = this.e.PfmtId[NewPfmt].flags;
      if (this.True(NewFID) && this.True(NewCell))
        tc.ResetUintFlag(ref flags2, 65776 /*0x0100F0*/);
      tc.ResetUintFlag(ref flags2, 12288 /*0x3000*/);
      int tabId = this.e.PfmtId[NewPfmt].TabId;
      int bltId = this.e.PfmtId[NewPfmt].BltId;
      int index3 = NewCell;
      int row2 = this.e.cell[index3].row;
      int shading = this.e.PfmtId[NewPfmt].shading;
      int CurParaFID = NewFID;
      int styId = this.e.PfmtId[NewPfmt].StyId;
      int spaceBefore = this.e.PfmtId[NewPfmt].SpaceBefore;
      int spaceAfter = this.e.PfmtId[NewPfmt].SpaceAfter;
      int spaceBetween = this.e.PfmtId[NewPfmt].SpaceBetween;
      int lineSpacing = this.e.PfmtId[NewPfmt].LineSpacing;
      Color bkColor = this.e.PfmtId[NewPfmt].BkColor;
      Color color2 = this.e.PfmtId[NewPfmt].BorderColor;
      int flow = this.e.PfmtId[NewPfmt].flow;
      int num14 = this.e.PfmtId[NewPfmt].pflags & 65520;
      int level2 = this.e.cell[NewCell].level;
      if (level2 != level1)
        this.FlushRtfLine(rtf);
      if (index2 > 0 && index2 != CurParaFID && (this.e.ParaFrame[index2].flags & 128 /*0x80*/) != 0 && !this.WritePfObjectTail(rtf, PrevFID))
        return false;
      if (CurParaFID != index2 && CurParaFID > 0)
      {
        if ((this.e.ParaFrame[CurParaFID].flags & 896) != 0)
          this.WriteRtfDoInfo(rtf, CurParaFID, flags2, NewFID);
        else
          this.WriteRtfFrameInfo(rtf, CurParaFID);
        int num15;
        num5 = num15 = 0;
        num4 = num15;
        num3 = num15;
        num2 = num15;
        num1 = num15;
        int num16;
        num10 = num16 = 0;
        num9 = num16;
        num8 = num16;
        num7 = num16;
        flags1 = 0;
        num12 = 0;
        num11 = 0;
        color1 = Color.White;
        clr2 = tc.CLR_AUTO;
        num13 = 0;
        flag1 = true;
      }
      if (row2 != row1)
      {
        int row3 = this.e.cell[this.e.cell[NewCell].ParentCell].row;
        if (level1 > this.e.RtfInitLevel && row1 != row3 && line > 0 && this.LineInfo(line - 1, 32 /*0x20*/))
          this.WriteRtfRow(rtf, 0, PrevCell, level1);
        if (level2 == this.e.RtfInitLevel && row2 > 0)
        {
          this.WriteRtfRow(rtf, NewCell, 0, level1);
          this.e.RtfInTable = true;
        }
      }
      if (leftIndentTwips == 0 && rightIndentTwips == 0 && firstIndentTwips == 0 && flags2 == 0 && tabId == 0 && index3 == 0 && shading == 0 && CurParaFID == 0 && index2 == 0 && spaceBefore == 0 && spaceAfter == 0 && spaceBetween == 0 && lineSpacing == 0 && styId == 0 && flow == 0 && num14 == 0)
        return this.WriteRtfControl(rtf, "pard", 0, 0.0);
      int num17 = 50168;
      int num18 = 112 /*0x70*/;
      bool flag3 = line > 0 && line < this.e.TotalLines && this.True(this.e.text[line - 1].tabw) && (this.e.text[line - 1].tabw.type & 2) != 0;
      int num19;
      if (index3 != index1 || tabId != num4 || bltId != num5 || CurParaFID != index2 || styId != num11 || num13 != 0 && flow == 0 || (((flags2 & num17) != 0 != ((flags1 & num17) != 0) ? 1 : ((num14 & num18) != (num12 & num18) ? 1 : 0)) | (flag3 ? 1 : 0)) != 0)
      {
        this.WriteRtfControl(rtf, "pard", 0, 0.0);
        if ((this.e.ParaFrame[CurParaFID].flags & 896) == 0)
          this.WriteRtfFrameInfo(rtf, CurParaFID);
        if (index3 > 0 && this.e.cell[index3].level >= this.e.RtfInitLevel)
        {
          this.WriteRtfControl(rtf, "intbl", 0, 0.0);
          if (level2 > this.e.RtfInitLevel)
            this.WriteRtfControl(rtf, "itap", 1, (double) (level2 - this.e.RtfInitLevel + 1));
        }
        int num20;
        num19 = num20 = 0;
        num4 = num20;
        num3 = num20;
        num2 = num20;
        num1 = num20;
        int num21;
        num10 = num21 = 0;
        num9 = num21;
        num8 = num21;
        num7 = num21;
        flags1 = 0;
        num12 = 0;
        num11 = num6 = 0;
        num13 = 0;
        color1 = tc.CLR_WHITE;
        clr2 = tc.CLR_AUTO;
        flag1 = true;
      }
      if (styId != num11 | flag1)
      {
        bool flag4 = true;
        if ((this.e.TerFlags2 & 1) != 0 && index3 > 0)
          flag4 = false;
        if (flag4 && !this.WriteRtfControl(rtf, "s", 1, (double) styId))
          return false;
      }
      if (tabId != num4 | flag1)
      {
        if (!this.WriteRtfTab(rtf, tabId))
          return false;
        int num22;
        num19 = num22 = 0;
        num2 = num22;
        num1 = num22;
        num3 = num22;
        int num23;
        num10 = num23 = 0;
        num9 = num23;
        num8 = num23;
        num7 = num23;
        int num24;
        flags1 = num24 = 0;
        num13 = 0;
        color1 = tc.CLR_WHITE;
        clr2 = tc.CLR_AUTO;
      }
      if ((flags2 & 8) != 0 && (flag1 || (flags1 & 8) == 0) && !this.WriteRtfBullet(rtf, bltId))
        return false;
      if ((flags2 & 66544) == 0)
        color2 = tc.CLR_AUTO;
      if ((flags1 & 66544) == 0)
        clr2 = tc.CLR_AUTO;
      if (flag1 || (flags2 & 66544) != 0 != ((flags1 & 66544) != 0) || !this.IsSameColor(color2, clr2))
      {
        if ((this.e.TerFlags5 & 268435456 /*0x10000000*/) == 0 && !this.WriteRtfParaBorder(rtf, flags2, color2, CurParaFID))
          return false;
        int num25;
        num2 = num25 = 0;
        num1 = num25;
        num3 = num25;
        flags1 = 0;
        num12 = 0;
        num13 = 0;
      }
      if (firstIndentTwips != num3 && !this.WriteRtfControl(rtf, "fi", 1, (double) firstIndentTwips) || leftIndentTwips != num1 && !this.WriteRtfControl(rtf, "li", 1, (double) leftIndentTwips) || rightIndentTwips != num2 && !this.WriteRtfControl(rtf, "ri", 1, (double) rightIndentTwips) || spaceBefore != num7 && !this.WriteRtfControl(rtf, "sb", 1, (double) spaceBefore) || spaceAfter != num8 && !this.WriteRtfControl(rtf, "sa", 1, (double) spaceAfter))
        return false;
      if (lineSpacing != num10)
      {
        if (!this.WriteRtfControl(rtf, "sl", 1, (double) (this.MulDiv(lineSpacing, 240 /*0xF0*/, 100) + 240 /*0xF0*/)) || !this.WriteRtfControl(rtf, "slmult", 1, 1.0))
          return false;
      }
      else if (spaceBetween != num9 && !this.WriteRtfControl(rtf, "sl", 1, (double) spaceBetween))
        return false;
      if (!this.e.ShortRtf || PrevPfmt != 0 || NewPfmt != 0)
      {
        if ((flags2 & 2) != 0)
        {
          if ((flags1 & 2) == 0 && !this.WriteRtfControl(rtf, "qr", 0, 0.0))
            return false;
        }
        else if ((flags2 & 1) != 0)
        {
          if ((flags1 & 1) == 0 && !this.WriteRtfControl(rtf, "qc", 0, 0.0))
            return false;
        }
        else if ((flags2 & 2048 /*0x0800*/) != 0)
        {
          if ((flags1 & 2048 /*0x0800*/) == 0 && !this.WriteRtfControl(rtf, "qj", 0, 0.0))
            return false;
        }
        else if (((flags2 & 1024 /*0x0400*/) != 0 || (flags2 & 2051) == 0) && ((flags1 & 1024 /*0x0400*/) == 0 || (flags1 & 2051) != 0) && !this.WriteRtfControl(rtf, "ql", 0, 0.0))
          return false;
      }
      bool flag5;
      if ((flags2 & 4) != 0 != ((flags1 & 4) != 0) && spaceBetween == num9 && lineSpacing == num10)
      {
        bool flag6;
        if ((flags2 & 4) != 0)
        {
          flag5 = this.WriteRtfControl(rtf, "sl", 1, 480.0);
          flag6 = this.WriteRtfControl(rtf, "slmult", 1, 1.0);
        }
        else
          flag6 = this.WriteRtfControl(rtf, "sl", 1, 0.0);
        if (!flag6)
          return false;
      }
      if ((flags2 & 16384 /*0x4000*/) != 0 && (flags1 & 16384 /*0x4000*/) == 0)
        flag5 = this.WriteRtfControl(rtf, "keep", 0, 0.0);
      if ((flags2 & 32768 /*0x8000*/) != 0 && (flags1 & 32768 /*0x8000*/) == 0)
        flag5 = this.WriteRtfControl(rtf, "keepn", 0, 0.0);
      if ((flags2 & 131072 /*0x020000*/) != 0 && (flags1 & 131072 /*0x020000*/) == 0)
        flag5 = this.WriteRtfControl(rtf, "sbauto", 1, (flags2 & 131072 /*0x020000*/) != 0 ? 1.0 : 0.0);
      if ((flags2 & 262144 /*0x040000*/) != 0 && (flags1 & 262144 /*0x040000*/) == 0)
        flag5 = this.WriteRtfControl(rtf, "saauto", 1, (flags2 & 262144 /*0x040000*/) != 0 ? 1.0 : 0.0);
      if ((num14 & 32 /*0x20*/) != 0 && (num12 & 32 /*0x20*/) == 0)
        flag5 = this.WriteRtfControl(rtf, "widctlpar", 0, 0.0);
      if ((num14 & 16 /*0x10*/) != 0 && (num12 & 16 /*0x10*/) == 0)
        flag5 = this.WriteRtfControl(rtf, "ssparnw", 0, 0.0);
      if ((num14 & 64 /*0x40*/) != 0 && (num12 & 64 /*0x40*/) == 0)
        flag5 = this.WriteRtfControl(rtf, "pagebb", 0, 0.0);
      if (shading != num6)
      {
        if (!this.WriteRtfControl(rtf, "shading", 1, (double) shading))
          return false;
        flag2 = true;
      }
      if (bkColor != color1)
      {
        int val = 0;
        while (val < rtf.TotalColors && !(rtf.color[val].color == bkColor))
          ++val;
        if (val == rtf.TotalColors)
          val = 0;
        if (!this.WriteRtfControl(rtf, "cbpat", 1, (double) val))
          return false;
      }
      if (flow != num13 && (flow == 2 && !this.WriteRtfControl(rtf, "rtlpar", 0, 0.0) || flow == 1 && !this.WriteRtfControl(rtf, "ltrpar", 0, 0.0)))
        return false;
      if (flag2)
        this.ResetRtfFont(rtf);
    }
    return true;
  }

  internal bool WriteRtfParaStyle(tc.ClsRtfOut rtf, int id)
  {
    int paraFlags = this.e.StyleId[id].ParaFlags;
    tc.StrStyleId strStyleId = this.e.StyleId[id];
    if (strStyleId.TabId > 0 && !this.WriteRtfTab(rtf, strStyleId.TabId) || (paraFlags & 66544) != 0 && !this.WriteRtfParaBorder(rtf, paraFlags, strStyleId.ParaBorderColor, 0) || strStyleId.FirstIndentTwips != 0 && !this.WriteRtfControl(rtf, "fi", 1, (double) strStyleId.FirstIndentTwips) || strStyleId.LeftIndentTwips > 0 && !this.WriteRtfControl(rtf, "li", 1, (double) strStyleId.LeftIndentTwips) || strStyleId.RightIndentTwips > 0 && !this.WriteRtfControl(rtf, "ri", 1, (double) strStyleId.RightIndentTwips) || strStyleId.SpaceBefore > 0 && !this.WriteRtfControl(rtf, "sb", 1, (double) strStyleId.SpaceBefore) || strStyleId.SpaceAfter > 0 && !this.WriteRtfControl(rtf, "sa", 1, (double) strStyleId.SpaceAfter))
      return false;
    if (strStyleId.LineSpacing != 0)
    {
      if (!this.WriteRtfControl(rtf, "sl", 1, (double) (this.MulDiv(strStyleId.LineSpacing, 240 /*0xF0*/, 100) + 240 /*0xF0*/)) || !this.WriteRtfControl(rtf, "slmult", 1, 1.0))
        return false;
    }
    else if (strStyleId.SpaceBetween != 0 && !this.WriteRtfControl(rtf, "sl", 1, (double) strStyleId.SpaceBetween))
      return false;
    if (strStyleId.shading > 0 && !this.WriteRtfControl(rtf, "shading", 1, (double) strStyleId.shading) || strStyleId.OutlineLevel >= 0 && !this.WriteRtfControl(rtf, "outlinelevel", 1, (double) strStyleId.OutlineLevel) || (paraFlags & 8) != 0 && !this.WriteRtfBullet(rtf, strStyleId.BltId) || (paraFlags & 2) != 0 && !this.WriteRtfControl(rtf, "qr", 0, 0.0) || (paraFlags & 1) != 0 && !this.WriteRtfControl(rtf, "qc", 0, 0.0) || (paraFlags & 2048 /*0x0800*/) != 0 && !this.WriteRtfControl(rtf, "qj", 0, 0.0) || (paraFlags & 16384 /*0x4000*/) != 0 && !this.WriteRtfControl(rtf, "keep", 0, 0.0) || (paraFlags & 32768 /*0x8000*/) != 0 && !this.WriteRtfControl(rtf, "keepn", 0, 0.0) || (paraFlags & 4) != 0 && (!this.WriteRtfControl(rtf, "sl", 1, 480.0) || !this.WriteRtfControl(rtf, "slmult", 1, 1.0)))
      return false;
    if (strStyleId.ParaBkColor != tc.CLR_WHITE && strStyleId.ParaBkColor != tc.CLR_AUTO)
    {
      int val = 0;
      while (val < rtf.TotalColors && !this.IsSameColor(rtf.color[val].color, strStyleId.ParaBkColor))
        ++val;
      if (val == rtf.TotalColors)
        val = 0;
      if (!this.WriteRtfControl(rtf, "cbpat", 1, (double) val))
        return false;
    }
    return strStyleId.shading == 0 || this.WriteRtfControl(rtf, "shading", 1, (double) strStyleId.shading);
  }

  internal bool WriteRtfPicture(tc.ClsRtfOut rtf, int pict)
  {
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = true;
    tc.ClsAnim anim = this.e.TerFont[pict].anim;
    if (anim != null)
    {
      if ((rtf.flags & 64 /*0x40*/) == 0 && (this.e.TerFont[pict].anim.FirstPict == 0 || this.e.TerFont[pict].anim.FirstPict == pict))
        flag1 = true;
      else
        flag2 = true;
    }
    if (this.True(this.e.TerFont[pict].LinkFile) && this.e.TerFont[pict].LinkFile.IndexOf(".GIF") >= 0 && flag1 | flag2)
      flag1 = flag2 = false;
    if (flag1 | flag2 && (!this.BeginRtfGroup(rtf) || flag2 && (!this.WriteRtfControl(rtf, "*", 0, 0.0) || !this.WriteRtfControl(rtf, "ssanimseq", 0, 0.0)) || !this.WriteRtfControl(rtf, "ssanimdelay", 1, (double) anim.delay) || flag1 && !this.WriteRtfControl(rtf, "ssanimloops", 1, (double) anim.OrigLoopCount)))
      return false;
    if (this.True(this.e.TerFont[pict].LinkFile) && (this.e.TerFlags & 16 /*0x10*/) == 0)
      flag3 = this.WriteRtfLinkedPicture(rtf, pict);
    else if (this.e.TerFont[pict].PictType == 2)
      flag3 = this.WriteRtfCtl(rtf, pict);
    else if (this.e.TerFont[pict].PictType == 6)
      flag3 = this.WriteRtfForm(rtf, pict);
    else if (this.e.TerFont[pict].PictType == 0)
    {
      flag3 = false;
      if ((this.e.TerFlags6 & 64 /*0x40*/) != 0)
      {
        byte[] pMem = (byte[]) null;
        try
        {
          MemoryStream memoryStream = new MemoryStream();
          this.e.TerFont[pict].image.Save((Stream) memoryStream, ImageFormat.Bmp);
          pMem = memoryStream.GetBuffer();
          memoryStream.Close();
        }
        catch (Exception ex)
        {
        }
        if (pMem != null)
          flag3 = this.WriteRtfDIB(rtf, pict, pMem, false);
      }
      else if (this.e.TerFont[pict].ImageType == ImageFormat.Wmf.Guid)
      {
        if (this.e.TerFont[pict].PictData == null & this.True(this.e.TerFont[pict].LinkFile))
          this.e.TerFont[pict].PictData = this.FileToByteArray(this.e.TerFont[pict].LinkFile, out tc.SkipInt);
        flag3 = this.WriteRtfMetafile(rtf, pict);
      }
      if (this.e.TerFont[pict].ImageType == ImageFormat.Emf.Guid)
      {
        if (this.e.TerFont[pict].PictData == null & this.True(this.e.TerFont[pict].LinkFile))
          this.e.TerFont[pict].PictData = this.FileToByteArray(this.e.TerFont[pict].LinkFile, out tc.SkipInt);
        flag3 = this.WriteRtfEnhMetafile(rtf, pict);
      }
      if (!flag3)
        flag3 = this.WriteRtfOrigImage(rtf, pict, this.e.TerFont[pict].ImageType, this.e.TerFont[pict].PictData);
    }
    if (flag1 | flag2 && !this.EndRtfGroup(rtf))
      return false;
    if (flag1 && (rtf.flags & 128 /*0x80*/) == 0)
      this.WriteRtfAnimSeq(rtf, pict);
    return flag3;
  }

  private bool WriteRtfRev(tc.ClsRtfOut rtf)
  {
    if (rtf.output < 2 || !this.e.TrackChanges)
    {
      this.FlushRtfLine(rtf);
      if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "*", 0, 0.0) || !this.WriteRtfControl(rtf, "revtbl", 0, 0.0))
        return false;
      for (int index = 0; index < this.e.TotalReviewers; ++index)
      {
        if (!this.BeginRtfGroup(rtf))
          return false;
        string text = this.e.reviewer[index].name == null || this.e.reviewer[index].name.Length == 0 ? "Unknown" : this.e.reviewer[index].name;
        if (!this.WriteRtfText(rtf, text, text.Length) || !this.WriteRtfText(rtf, ";", 1) || !this.EndRtfGroup(rtf))
          return false;
      }
      if (!this.EndRtfGroup(rtf))
        return false;
      this.FlushRtfLine(rtf);
    }
    return true;
  }

  internal bool WriteRtfRow(tc.ClsRtfOut rtf, int NewCell, int PrevCell, int PrevLevel)
  {
    bool flag1 = false;
    bool flag2 = false;
    int row = this.e.cell[NewCell].row;
    if (NewCell == 0)
      flag2 = true;
    if (flag2)
    {
      this.FlushRtfLine(rtf);
      if (!this.BeginRtfGroup(rtf) || !this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "*", 0, 0.0) || !this.WriteRtfControl(rtf, "nesttableprops", 0, 0.0))
        return false;
    }
    else
    {
      int prevRow = this.e.TableRow[row].PrevRow;
      if (prevRow >= 0 && this.e.cell[PrevCell].level <= this.e.RtfInitLevel && (rtf.flags & 16 /*0x10*/) == 0 && !rtf.TblHilight && this.e.TableRow[prevRow].flags == this.e.TableRow[row].flags && this.e.TableRow[prevRow].CellMargin == this.e.TableRow[row].CellMargin && this.e.TableRow[prevRow].indent == this.e.TableRow[row].indent && this.e.TableRow[prevRow].AddedIndent == this.e.TableRow[row].AddedIndent && this.e.TableRow[prevRow].MinHeight == this.e.TableRow[row].MinHeight && this.e.TableRow[prevRow].FixWidth == this.e.TableRow[row].FixWidth && this.e.TableRow[prevRow].border == this.e.TableRow[row].border && this.e.TableRow[prevRow].flow == this.e.TableRow[row].flow && this.e.TableRow[prevRow].id == this.e.TableRow[row].id)
      {
        int index1 = this.e.TableRow[row].FirstCell;
        int index2 = this.e.TableRow[prevRow].FirstCell;
        int num = 12304;
        while (index1 > 0)
        {
          if (index2 >= 0 && this.e.cell[index2].x == this.e.cell[index1].x && this.e.cell[index2].width == this.e.cell[index1].width && this.e.cell[index2].border == this.e.cell[index1].border && this.e.cell[index2].shading == this.e.cell[index1].shading && !(this.e.cell[index2].BackColor != this.e.cell[index1].BackColor) && this.e.cell[index2].RowSpan == this.e.cell[index1].RowSpan && this.e.cell[index2].ColSpan == this.e.cell[index1].ColSpan && this.e.cell[index2].TextAngle == this.e.cell[index1].TextAngle && (this.e.cell[index2].flags & num) != 0 == ((this.e.cell[index1].flags & num) != 0))
          {
            if (this.True(this.e.cell[index1].border))
            {
              for (int index3 = 0; index3 < 4; ++index3)
              {
                if (this.e.cell[index2].BorderWidth[index3] != this.e.cell[index1].BorderWidth[index3] || this.e.cell[index2].BorderColor[index3] != this.e.cell[index1].BorderColor[index3])
                  goto label_16;
              }
            }
            index1 = this.e.cell[index1].NextCell;
            index2 = this.e.cell[index2].NextCell;
          }
          else
            goto label_16;
        }
        if (index2 <= 0)
          goto label_49;
      }
    }
label_16:
    int index = !flag2 ? row : this.e.cell[PrevCell].row;
    if (!flag2)
      this.FlushRtfLine(rtf);
    if (!this.WriteRtfControl(rtf, "trowd", 0, 0.0))
      return false;
    bool flag3 = true;
    if ((this.e.TableRow[index].flags & 1) != 0)
      flag3 = this.WriteRtfControl(rtf, "trqc", 0, 0.0);
    else if ((this.e.TableRow[index].flags & 2) != 0)
      flag3 = this.WriteRtfControl(rtf, "trqr", 0, 0.0);
    if (!flag3)
      return flag3;
    if (!this.WriteRtfControl(rtf, "trgaph", 1, (double) this.e.TableRow[index].CellMargin) || this.e.cell[this.e.TableRow[index].FirstCell].level > this.e.RtfInitLevel && (!this.WriteRtfControl(rtf, "trpaddl", 1, (double) this.e.TableRow[index].CellMargin) || !this.WriteRtfControl(rtf, "trpaddr", 1, (double) this.e.TableRow[index].CellMargin) || !this.WriteRtfControl(rtf, "trpaddfl", 1, 3.0) || !this.WriteRtfControl(rtf, "trpaddfr", 1, 3.0)) || !this.WriteRtfControl(rtf, "trleft", 1, (double) (this.e.TableRow[index].indent - this.e.TableRow[index].AddedIndent)) || this.e.TableRow[index].MinHeight != 0 && !this.WriteRtfControl(rtf, "trrh", 1, (double) this.e.TableRow[index].MinHeight) || (this.e.TableRow[index].flags & 4) != 0 && !this.WriteRtfControl(rtf, "trhdr", 0, 0.0) || (this.e.TableRow[index].flags & 8192 /*0x2000*/) != 0 && !this.WriteRtfControl(rtf, "trkeep", 0, 0.0) || this.e.TableRow[index].id != 0 && !this.WriteRtfControl(rtf, "sstrid", 1, (double) this.e.TableRow[index].id) || (this.e.TableRow[index].border & 1) != 0 && (!this.WriteRtfControl(rtf, "trbrdrt", 0, 0.0) || !this.WriteRtfControl(rtf, "brdrs", 0, 0.0) || !this.WriteRtfControl(rtf, "brdrw", 1, (double) this.e.TableRow[index].BorderWidth[0])) || (this.e.TableRow[index].border & 2) != 0 && (!this.WriteRtfControl(rtf, "trbrdrb", 0, 0.0) || !this.WriteRtfControl(rtf, "brdrs", 0, 0.0) || !this.WriteRtfControl(rtf, "brdrw", 1, (double) this.e.TableRow[index].BorderWidth[1])) || (this.e.TableRow[index].border & 4) != 0 && (!this.WriteRtfControl(rtf, "trbrdrl", 0, 0.0) || !this.WriteRtfControl(rtf, "brdrs", 0, 0.0) || !this.WriteRtfControl(rtf, "brdrw", 1, (double) this.e.TableRow[index].BorderWidth[2])) || (this.e.TableRow[index].border & 8) != 0 && (!this.WriteRtfControl(rtf, "trbrdrr", 0, 0.0) || !this.WriteRtfControl(rtf, "brdrs", 0, 0.0) || !this.WriteRtfControl(rtf, "brdrw", 1, (double) this.e.TableRow[index].BorderWidth[3])) || this.e.TableRow[index].flow == 2 && !this.WriteRtfControl(rtf, "rtlrow", 0, 0.0) || this.e.TableRow[index].flow == 1 && !this.WriteRtfControl(rtf, "ltrrow", 0, 0.0))
      return false;
    int num1 = 0;
    if (rtf.output >= 2 && this.e.HilightType != 0)
    {
      int hilightEndRow = this.e.HilightEndRow;
      while (hilightEndRow > this.e.HilightBegRow && ((this.e.text[hilightEndRow].flags & 268435456 /*0x10000000*/) == 0 || this.e.cell[this.e.text[hilightEndRow].cid].row != index))
        --hilightEndRow;
      num1 = this.e.text[hilightEndRow].cid;
    }
    int CurCellId = this.e.TableRow[index].FirstCell;
    int indent = this.e.TableRow[index].indent;
    for (; CurCellId > 0; CurCellId = this.e.cell[CurCellId].NextCell)
    {
      if (!flag1)
        flag1 = !rtf.TblHilight || (this.e.cell[CurCellId].flags & 2048 /*0x0800*/) != 0;
      if (flag1)
      {
        if (!rtf.TblHilight || (this.e.cell[CurCellId].flags & 2048 /*0x0800*/) != 0)
        {
          int x = this.e.cell[CurCellId].x;
          this.e.cell[CurCellId].x = indent;
          indent += this.e.cell[CurCellId].width;
          this.WriteRtfCell(rtf, CurCellId);
          this.e.cell[CurCellId].x = x;
        }
        else
          break;
      }
      if (CurCellId == num1)
        break;
    }
    if (flag2)
    {
      if (!this.WriteRtfControl(rtf, "nestrow", 0, 0.0) || !this.EndRtfGroup(rtf))
        return false;
      this.WriteRtfNoNestGroup(rtf);
      if (!this.EndRtfGroup(rtf))
        return false;
      this.FlushRtfLine(rtf);
    }
    rtf.flags = tc.ResetUintFlag(ref rtf.flags, 16 /*0x10*/);
label_49:
    if (PrevLevel == this.e.RtfInitLevel)
      this.e.RtfInTable = true;
    return true;
  }

  internal bool WriteRtfSection(tc.ClsRtfOut rtf, int sect)
  {
    this.FlushRtfLine(rtf);
    if (!this.WriteRtfControl(rtf, "sectd", 0, 0.0))
      return false;
    if (rtf.output < 2 && this.True(this.e.TerSect[sect].border))
    {
      if (this.e.TerSect[sect].BorderOpts > 0 && !this.WriteRtfControl(rtf, "pgbrdropt", 1, (double) this.e.TerSect[sect].BorderOpts))
        return false;
      for (int val = 0; val < 4; ++val)
      {
        if (val == 0 && !this.WriteRtfControl(rtf, "pgbrdrt", 0, 0.0) || val == 1 && !this.WriteRtfControl(rtf, "pgbrdrb", 0, 0.0) || val == 2 && !this.WriteRtfControl(rtf, "pgbrdrl", 0, 0.0) || val == 3 && !this.WriteRtfControl(rtf, "pgbrdrr", 0, 0.0) || this.e.TerSect[sect].BorderType == 8 && !this.WriteRtfControl(rtf, "brdrnone", 0, 0.0) || this.e.TerSect[sect].BorderType == 0 && !this.WriteRtfControl(rtf, "brdrs", 0, 0.0) || this.e.TerSect[sect].BorderType == 1 && !this.WriteRtfControl(rtf, "brdrdb", 0, 0.0) || this.e.TerSect[sect].BorderType == 2 && !this.WriteRtfControl(rtf, "brdrtriple", 0, 0.0) || this.e.TerSect[sect].BorderType == 3 && !this.WriteRtfControl(rtf, "brdrsh", 0, 0.0) || this.e.TerSect[sect].BorderType == 4 && !this.WriteRtfControl(rtf, "brdrthtnmg", 0, 0.0) || this.e.TerSect[sect].BorderType == 5 && !this.WriteRtfControl(rtf, "brdrthtnthmg", 0, 0.0) || this.e.TerSect[sect].BorderType == 6 && !this.WriteRtfControl(rtf, "brdrtnthmg", 0, 0.0) || this.e.TerSect[sect].BorderType == 7 && !this.WriteRtfControl(rtf, "brdrtnthtnmg", 0, 0.0) || !this.WriteRtfControl(rtf, "brdrw", 1, (double) this.e.TerSect[sect].BorderWidth[val]) || !this.WriteRtfControl(rtf, "brsp", 1, (double) this.e.TerSect[sect].BorderSpace[val]))
          return false;
        if (this.e.TerSect[sect].BorderColor != tc.CLR_AUTO)
        {
          int index = 0;
          while (index < rtf.TotalColors && !(rtf.color[index].color == this.e.TerSect[sect].BorderColor))
            ++index;
          if (index == rtf.TotalColors)
            ;
          if (!this.WriteRtfControl(rtf, "brdrcf", 1, (double) val))
            return false;
        }
      }
    }
    float x1 = 8.5f;
    float x2 = 11f;
    PaperKind pprKind = this.e.TerSect[sect].PprKind;
    if (!this.e.TerSect[sect].IsPortrait && !this.WriteRtfControl(rtf, "lndscpsxn", 0, 0.0))
      return false;
    switch (pprKind)
    {
      case PaperKind.Letter:
        if (!this.e.TerSect[sect].IsPortrait)
        {
          double num = (double) x1;
          x1 = x2;
          x2 = (float) num;
        }
        if (!this.WriteRtfControl(rtf, "pgwsxn", 1, (double) this.InchesToTwips((double) x1)) || !this.WriteRtfControl(rtf, "pghsxn", 1, (double) this.InchesToTwips((double) x2)))
          return false;
        PaperSourceKind firstPageBin = this.e.TerSect[sect].FirstPageBin;
        switch (firstPageBin)
        {
          case (PaperSourceKind) 0:
          case PaperSourceKind.AutomaticFeed:
            PaperSourceKind bin = this.e.TerSect[sect].bin;
            switch (bin)
            {
              case (PaperSourceKind) 0:
              case PaperSourceKind.AutomaticFeed:
                if ((this.e.TerSect1[sect].fhdr.FirstLine >= 0 || this.e.TerSect1[sect].fftr.FirstLine >= 0 || (this.e.TerSect[sect].flags & 4) != 0) && !this.WriteRtfControl(rtf, "titlepg", 0, 0.0) || !this.WriteRtfControl(rtf, "marglsxn", 1, (double) this.InchesToTwips((double) this.e.TerSect[sect].LeftMargin)) || !this.WriteRtfControl(rtf, "margrsxn", 1, (double) this.InchesToTwips((double) this.e.TerSect[sect].RightMargin)) || !this.WriteRtfControl(rtf, "margtsxn", 1, (double) this.InchesToTwips((double) this.e.TerSect[sect].TopMargin * ((this.e.TerSect[sect].flags & 8) != 0 ? -1.0 : 1.0))) || !this.WriteRtfControl(rtf, "margbsxn", 1, (double) this.InchesToTwips((double) this.e.TerSect[sect].BotMargin * ((this.e.TerSect[sect].flags & 16 /*0x10*/) != 0 ? -1.0 : 1.0))) || !this.WriteRtfControl(rtf, "headery", 1, (double) this.InchesToTwips((double) this.e.TerSect[sect].HdrMargin)) || !this.WriteRtfControl(rtf, "footery", 1, (double) this.InchesToTwips((double) this.e.TerSect[sect].FtrMargin)) || (this.e.TerSect[sect].flags & 1) == 0 && !this.WriteRtfControl(rtf, "sbknone", 0, 0.0) || (this.e.TerSect[sect].flags & 1) != 0 && !this.WriteRtfControl(rtf, "sbkpage", 0, 0.0))
                  return false;
                if ((this.e.TerSect[sect].flags & 2) != 0)
                {
                  if (!this.WriteRtfControl(rtf, "pgnstarts", 1, (double) this.e.TerSect[sect].FirstPageNo) || !this.WriteRtfControl(rtf, "pgnrestart", 0, 0.0))
                    return false;
                }
                else if (!this.WriteRtfControl(rtf, "pgncont", 0, 0.0))
                  return false;
                if ((this.e.TerSect[sect].flags & 128 /*0x80*/) != 0 && !this.WriteRtfControl(rtf, "vertalc", 0, 0.0) || (this.e.TerSect[sect].flags & 256 /*0x0100*/) != 0 && !this.WriteRtfControl(rtf, "vertal", 0, 0.0) || this.e.TerSect[sect].columns > 1 && (!this.WriteRtfControl(rtf, "cols", 1, (double) this.e.TerSect[sect].columns) || !this.WriteRtfControl(rtf, "colsx", 1, (double) this.InchesToTwips((double) this.e.TerSect[sect].ColumnSpace))))
                  return false;
                string control = this.e.TerSect[sect].PageNumFmt != 3 ? (this.e.TerSect[sect].PageNumFmt != 4 ? (this.e.TerSect[sect].PageNumFmt != 1 ? (this.e.TerSect[sect].PageNumFmt != 2 ? "pgndec" : "pgnlcltr") : "pgnucltr") : "pgnlcrm") : "pgnucrm";
                if (!this.WriteRtfControl(rtf, control, 0, 0.0) || this.e.TerSect[sect].flow == 2 && !this.WriteRtfControl(rtf, "rtlsect", 0, 0.0) || this.e.TerSect[sect].flow == 1 && !this.WriteRtfControl(rtf, "ltrsect", 0, 0.0))
                  return false;
                if ((this.e.TerSect[sect].flags & 512 /*0x0200*/) != 0)
                {
                  int val = this.e.TerSect[sect].LineStep;
                  if (val < 1)
                    val = 1;
                  if (!this.WriteRtfControl(rtf, "linemod", 1, (double) val))
                    return false;
                }
                if (this.True(this.e.TerSect[sect].LineSpace != 0) && (this.e.TerSect[sect].flags & 1024 /*0x0400*/) != 0 && (!this.WriteRtfControl(rtf, "sectlinegrid", 1, (double) this.e.TerSect[sect].LineSpace) || !this.WriteRtfControl(rtf, "sectspecifyl", 0, 0.0)))
                  return false;
                rtf.sect = sect;
                this.FlushRtfLine(rtf);
                return true;
              default:
                if (!this.WriteRtfControl(rtf, "binsxn", 1, (double) bin))
                  return false;
                goto case (PaperSourceKind) 0;
            }
          default:
            if (!this.WriteRtfControl(rtf, "binfsxn", 1, (double) firstPageBin))
              return false;
            goto case (PaperSourceKind) 0;
        }
      case PaperKind.Legal:
        x2 = 14f;
        goto case PaperKind.Letter;
      case PaperKind.A4:
        x1 = 8.27f;
        x2 = 11.69f;
        goto case PaperKind.Letter;
      case PaperKind.Number10Envelope:
        x2 = 9.5f;
        x1 = 4.125f;
        goto case PaperKind.Letter;
      default:
        x2 = this.e.TerSect[sect].PprHeight;
        x1 = this.e.TerSect[sect].PprWidth;
        goto case PaperKind.Letter;
    }
  }

  internal bool WriteRtfShape(tc.ClsRtfOut rtf, int pict)
  {
    int paraFid = this.e.TerFont[pict].ParaFID;
    tc.StrParaFrame pFrame = this.e.ParaFrame[paraFid];
    bool flag1 = false;
    int flags = pFrame.flags;
    tc.ClsAnim anim = this.e.TerFont[pict].anim;
    if (anim != null && (this.e.TerFont[pict].anim.FirstPict == 0 || this.e.TerFont[pict].anim.FirstPict == pict))
      flag1 = true;
    if (((!this.True(this.e.TerFont[pict].LinkFile) ? 0 : (this.e.TerFont[pict].LinkFile.IndexOf(".GIF") >= 0 ? 1 : 0)) & (flag1 ? 1 : 0)) != 0)
      flag1 = false;
    if (flag1 && (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "ssanimdelay", 1, (double) anim.delay) || !this.WriteRtfControl(rtf, "ssanimloops", 1, (double) anim.OrigLoopCount)))
      return false;
    this.FlushRtfLine(rtf);
    bool flag2 = (this.e.ParaFrame[paraFid].flags & 2097152 /*0x200000*/) != 0;
    if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, flag2 ? "shpgrp" : "shp", 0, 0.0) || !this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "*", 0, 0.0) || !this.WriteRtfControl(rtf, "shpinst", 0, 0.0))
      return false;
    int val1;
    if ((pFrame.flags & 524288 /*0x080000*/) != 0)
    {
      val1 = pFrame.OrgX;
      if ((pFrame.flags & 1) != 0)
      {
        int section = this.GetSection(rtf.line);
        val1 -= (int) ((double) this.e.TerSect[section].LeftMargin * 1440.0);
      }
    }
    else
      val1 = pFrame.x;
    if (!this.WriteRtfControl(rtf, "shpleft", 1, (double) val1) || !this.WriteRtfControl(rtf, "shpright", 1, (double) (val1 + pFrame.width)))
      return false;
    int val2 = (pFrame.flags & 1048576 /*0x100000*/) != 0 ? pFrame.OrgY : pFrame.ParaY;
    if (!this.WriteRtfControl(rtf, "shptop", 1, (double) val2) || !this.WriteRtfControl(rtf, "shpbottom", 1, (double) (val2 + pFrame.height)))
      return false;
    tc.ResetUintFlag(ref flags, 1);
    if ((flags & 1) != 0)
    {
      if (!this.WriteRtfControl(rtf, "shpbxpage", 0, 0.0))
        return false;
    }
    else if ((flags & 1073741824 /*0x40000000*/) != 0)
    {
      if (!this.WriteRtfControl(rtf, "shpbxcolumn", 0, 0.0))
        return false;
    }
    else if (!this.WriteRtfControl(rtf, "shpbxmarg", 0, 0.0))
      return false;
    if ((pFrame.flags & 64 /*0x40*/) != 0 && !this.WriteRtfControl(rtf, "shpbymargin", 0, 0.0) || (pFrame.flags & 32 /*0x20*/) != 0 && !this.WriteRtfControl(rtf, "shpbypage", 0, 0.0) || (pFrame.flags & 96 /*0x60*/) == 0 && !this.WriteRtfControl(rtf, "shpbypara", 0, 0.0) || (pFrame.flags & 524288 /*0x080000*/) != 0 && !this.WriteRtfControl(rtf, "shpbxignore", 0, 0.0) || (pFrame.flags & 1048576 /*0x100000*/) != 0 && !this.WriteRtfControl(rtf, "shpbyignore", 0, 0.0) || (pFrame.flags & 8192 /*0x2000*/) != 0 && !this.WriteRtfControl(rtf, "shpwr", 1, 1.0) || (pFrame.flags & 16384 /*0x4000*/) != 0 && !this.WriteRtfControl(rtf, "shpwr", 1, 3.0) || (pFrame.flags & 24576 /*0x6000*/) == 0 && (!this.WriteRtfControl(rtf, "shpwr", 1, 2.0) || !this.WriteRtfControl(rtf, "shpwrk", 1, 0.0)) || pFrame.ZOrder != 0 && !this.WriteRtfControl(rtf, "shpz", 1, (double) pFrame.ZOrder) || ((pFrame.flags & 134217728 /*0x08000000*/) != 0 || paraFid == this.e.WmParaFID) && !this.WriteRtfShapeProp(rtf, "fBehindDocument", "1") || this.e.TerFont[pict].FrameType == 1 && !this.WriteRtfControl(rtf, "ssshpalignleft", 0, 0.0) || this.e.TerFont[pict].FrameType == 2 && !this.WriteRtfControl(rtf, "ssshpalignright", 0, 0.0))
      return false;
    int ShapeType = this.e.ParaFrame[paraFid].ShapeType;
    string OutStr;
    if (flag2)
    {
      if (!this.WriteRtfShapeProp(rtf, "groupLeft", "0") || !this.WriteRtfShapeProp(rtf, "groupTop", "0") || !this.WriteRtfShapeProp(rtf, "groupRight", pFrame.width.ToString()) || !this.WriteRtfShapeProp(rtf, "groupBottom", pFrame.height.ToString()))
        return false;
    }
    else
    {
      if (this.e.TerFont[pict].ObjectType == 5)
        ShapeType = 201;
      OutStr = ShapeType.ToString();
      if (!this.WriteRtfShapeProp(rtf, "shapeType", OutStr))
        return false;
      this.WriteOtherShapeProps(rtf, ShapeType, ref pFrame);
    }
    if ((pFrame.flags & 524288 /*0x080000*/) != 0)
    {
      if (!this.WriteRtfShapeProp2(rtf, "posh", 0))
        return false;
      if ((flags & 1) != 0)
      {
        if (!this.WriteRtfShapeProp2(rtf, "posrelh", 1))
          return false;
      }
      else if ((flags & 1073741824 /*0x40000000*/) != 0)
      {
        if (!this.WriteRtfShapeProp2(rtf, "posrelh", 2))
          return false;
      }
      else if ((flags & 536870912 /*0x20000000*/) != 0)
      {
        if (!this.WriteRtfShapeProp2(rtf, "posrelh", 3))
          return false;
      }
      else if (!this.WriteRtfShapeProp2(rtf, "posrelh", 0))
        return false;
    }
    if ((pFrame.flags & 1048576 /*0x100000*/) != 0)
    {
      if (!this.WriteRtfShapeProp2(rtf, "posv", 0))
        return false;
      if ((pFrame.flags & 32 /*0x20*/) != 0)
      {
        if (!this.WriteRtfShapeProp2(rtf, "posrelv", 1))
          return false;
      }
      else if ((pFrame.flags & 64 /*0x40*/) != 0)
      {
        if (!this.WriteRtfShapeProp2(rtf, "posrelv", 0))
          return false;
      }
      else if ((pFrame.flags & 268435456 /*0x10000000*/) != 0)
      {
        if (!this.WriteRtfShapeProp2(rtf, "posrelv", 3))
          return false;
      }
      else if (!this.WriteRtfShapeProp2(rtf, "posrelv", 2))
        return false;
    }
    if (flag2)
    {
      this.FlushRtfLine(rtf);
      if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "shp", 0, 0.0) || !this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "*", 0, 0.0) || !this.WriteRtfControl(rtf, "shpinst", 0, 0.0))
        return false;
      int num1 = 0;
      int num2 = 0;
      if (!this.WriteRtfShapeProp(rtf, "relLeft", num1.ToString()) || !this.WriteRtfShapeProp(rtf, "relTop", num2.ToString()))
        return false;
      num1 += pFrame.width;
      int num3 = num2 + pFrame.height;
      if (!this.WriteRtfShapeProp(rtf, "relRight", num1.ToString()) || !this.WriteRtfShapeProp(rtf, "relBottom", num3.ToString()))
        return false;
      int shapeType = this.e.ParaFrame[paraFid].ShapeType;
      OutStr = shapeType.ToString();
      if (!this.WriteRtfShapeProp(rtf, "shapeType", OutStr))
        return false;
      this.WriteOtherShapeProps(rtf, shapeType, ref pFrame);
    }
    if (this.True(this.e.TerFont[pict].LinkFile) && this.e.TerFont[pict].ObjectType == 0 && (this.e.TerFlags & 16 /*0x10*/) == 0)
    {
      this.AddSlashes(this.e.TerFont[pict].LinkFile, out OutStr, 2);
      rtf.flags |= 2;
      if (!this.WriteRtfShapeProp(rtf, "pibName", OutStr))
        return false;
      tc.ResetUintFlag(ref rtf.flags, 2);
      if (!this.WriteRtfShapeProp(rtf, "pibFlags", "14"))
        return false;
    }
    else if (this.e.TerFont[pict].PictType != 11)
    {
      if (!this.WriteRtfShapeProp(rtf, "pib", (string) null))
        return false;
      rtf.flags |= 128 /*0x80*/;
      if (!this.WriteRtfPicture(rtf, pict))
        return false;
      tc.ResetUintFlag(ref rtf.flags, 128 /*0x80*/);
      if (!this.EndRtfGroup(rtf) || !this.EndRtfGroup(rtf))
        return false;
    }
    if (this.e.TerFont[pict].ObjectType != 0 && this.e.TerFont[pict].ObjectType != 3)
    {
      if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "shptxt", 0, 0.0))
        return false;
      this.WriteRtfObject(rtf, pict);
      if (!this.EndRtfGroup(rtf))
        return false;
    }
    if (flag2 && (!this.EndRtfGroup(rtf) || !this.EndRtfGroup(rtf)) || paraFid == this.e.WmParaFID && (this.e.WmImageAttr != null && (!this.WriteRtfShapeProp(rtf, "pictureContrast", "19661") || !this.WriteRtfShapeProp(rtf, "pictureBrightness", "22938")) || !this.WriteRtfShapeProp(rtf, "wzName", "WordPictureWatermark3") || !this.WriteRtfShapeProp(rtf, "posh", "2") || !this.WriteRtfShapeProp(rtf, "posrelh", "0") || !this.WriteRtfShapeProp(rtf, "posv", "2") || !this.WriteRtfShapeProp(rtf, "posrelv", "0")) || !this.EndRtfGroup(rtf) || !flag2 && this.e.TerFont[pict].PictType != 11 && (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "shprslt", 0, 0.0) || !(this.e.TerFont[pict].ObjectType == 0 || this.e.TerFont[pict].ObjectType == 3 ? this.WriteRtfPicture(rtf, pict) : this.WriteRtfObject(rtf, pict)) || !this.EndRtfGroup(rtf)) || !this.EndRtfGroup(rtf))
      return false;
    if (flag1)
    {
      if (!this.EndRtfGroup(rtf))
        return false;
      this.WriteRtfAnimSeq(rtf, pict);
    }
    return true;
  }

  internal bool WriteRtfShapeProp(tc.ClsRtfOut rtf, string name, string value)
  {
    return this.BeginRtfGroup(rtf) && this.WriteRtfControl(rtf, "sp", 0, 0.0) && this.BeginRtfGroup(rtf) && this.WriteRtfControl(rtf, "sn", 0, 0.0) && this.WriteRtfText(rtf, name, name.Length) && this.EndRtfGroup(rtf) && this.BeginRtfGroup(rtf) && this.WriteRtfControl(rtf, "sv", 0, 0.0) && (value == null || this.WriteRtfText(rtf, value, value.Length) && this.EndRtfGroup(rtf) && this.EndRtfGroup(rtf));
  }

  internal bool WriteRtfShapeProp2(tc.ClsRtfOut rtf, string name, int value)
  {
    return this.WriteRtfShapeProp(rtf, name, value.ToString());
  }

  internal bool WriteRtfStylesheet(tc.ClsRtfOut rtf, List<bool> realUsedStyles)
  {
    this.FlushRtfLine(rtf);
    bool flag = true;
    rtf.flags |= 65536 /*0x010000*/;
    for (int index = 0; index < this.e.TotalSID; ++index)
    {
      if (this.e.StyleId[index].InUse && (!this.e.ShortRtf || realUsedStyles[index]))
      {
        if (flag)
        {
          flag = false;
          if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "stylesheet", 0, 0.0))
            return false;
        }
        if (!this.BeginRtfGroup(rtf))
          return false;
        if (this.e.StyleId[index].type == 1)
        {
          if (!this.WriteRtfControl(rtf, "cs", 1, (double) index) || !this.WriteRtfCharStyle(rtf, index))
            return false;
        }
        else if (index > 0 && !this.WriteRtfControl(rtf, "s", 1, (double) index) || index > 0 && !this.WriteRtfControl(rtf, "snext", 1, (double) this.e.StyleId[index].next) || !this.WriteRtfCharStyle(rtf, index) || !this.WriteRtfParaStyle(rtf, index))
          return false;
        if (!this.WriteRtfText(rtf, this.e.StyleId[index].name, this.e.StyleId[index].name.Length) || !this.WriteRtfText(rtf, ";", 1) || !this.EndRtfGroup(rtf))
          return false;
      }
    }
    rtf.flags &= -65537;
    if (!flag && !this.EndRtfGroup(rtf))
      return false;
    this.FlushRtfLine(rtf);
    return true;
  }

  internal bool WriteRtfTab(tc.ClsRtfOut rtf, int CurTabId)
  {
    int num = this.e.TerTab[CurTabId].count;
    if (num > 20)
      num = 0;
    for (int index = 0; index < num; ++index)
    {
      if (this.e.TerTab[CurTabId].type[index] == 1 && !this.WriteRtfControl(rtf, "tqr", 0, 0.0) || this.e.TerTab[CurTabId].type[index] == 3 && !this.WriteRtfControl(rtf, "tqdec", 0, 0.0) || this.e.TerTab[CurTabId].type[index] == 2 && !this.WriteRtfControl(rtf, "tqc", 0, 0.0) || this.e.TerTab[CurTabId].flags[index] == (byte) 1 && !this.WriteRtfControl(rtf, "tldot", 0, 0.0) || this.e.TerTab[CurTabId].flags[index] == (byte) 2 && !this.WriteRtfControl(rtf, "tlhyph", 0, 0.0) || this.e.TerTab[CurTabId].flags[index] == (byte) 4 && !this.WriteRtfControl(rtf, "tlul", 0, 0.0) || !this.WriteRtfControl(rtf, "tx", 1, (double) this.e.TerTab[CurTabId].pos[index]))
        return false;
    }
    return true;
  }

  internal bool WriteRtfTag(tc.ClsRtfOut rtf, int CurTag)
  {
    if (CurTag > 0 && CurTag < this.e.TotalCharTags && this.e.CharTag[CurTag].InUse)
    {
      for (; this.True(CurTag) && CurTag > 0 && CurTag < this.e.TotalCharTags && this.e.CharTag[CurTag].InUse; CurTag = this.e.CharTag[CurTag].next)
      {
        if (this.e.CharTag[CurTag].type == 1)
        {
          if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "*", 0, 0.0) || !this.WriteRtfControl(rtf, "bkmkstart", 0, 0.0) || !this.WriteRtfText(rtf, this.e.CharTag[CurTag].name, this.e.CharTag[CurTag].name.Length) || !this.EndRtfGroup(rtf) || !this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "*", 0, 0.0) || !this.WriteRtfControl(rtf, "bkmkend", 0, 0.0) || !this.WriteRtfText(rtf, this.e.CharTag[CurTag].name, this.e.CharTag[CurTag].name.Length) || !this.EndRtfGroup(rtf))
            return false;
        }
        else if (this.e.CharTag[CurTag].type == 0)
        {
          if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "*", 0, 0.0) || !this.WriteRtfControl(rtf, "sstag", 0, 0.0) || !this.WriteRtfText(rtf, this.e.CharTag[CurTag].name, this.e.CharTag[CurTag].name.Length) || !this.WriteRtfControl(rtf, "sstagint", 1, (double) this.e.CharTag[CurTag].AuxInt))
            return false;
          string auxText = this.e.CharTag[CurTag].AuxText;
          if (auxText != null && auxText.Length > 0 && !this.WriteRtfText(rtf, auxText, auxText.Length) || !this.EndRtfGroup(rtf))
            return false;
        }
        else if (this.e.CharTag[CurTag].type == 78)
        {
          this.e.BlockQuestionMarkAfterUnicode = true;
          try
          {
            if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "*", 0, 0.0) || !this.WriteRtfControl(rtf, "replchartag", 0, 0.0))
              return false;
            string text = this.e.CharTag[CurTag].name;
            if (text == null || text == "")
              text = "REPLACEDCHAR";
            if (!this.WriteRtfText(rtf, text, text.Length) || !this.WriteRtfControl(rtf, "replchartagint", 1, (double) this.e.CharTag[CurTag].AuxInt))
              return false;
            string auxText = this.e.CharTag[CurTag].AuxText;
            if (auxText != null && auxText.Length > 0 && !this.WriteRtfText(rtf, auxText, auxText.Length))
              return false;
            if (!this.EndRtfGroup(rtf))
              return false;
          }
          finally
          {
            this.e.BlockQuestionMarkAfterUnicode = false;
          }
        }
        else if (this.e.CharTag[CurTag].type == 79)
        {
          this.e.BlockQuestionMarkAfterUnicode = true;
          try
          {
            if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "*", 0, 0.0) || !this.WriteRtfControl(rtf, "areplchartag", 0, 0.0))
              return false;
            string text = this.e.CharTag[CurTag].name;
            if (text == null || text == "")
              text = "AUTOREPLACEDCHAR";
            if (!this.WriteRtfText(rtf, text, text.Length) || !this.WriteRtfControl(rtf, "areplchartagint", 1, (double) this.e.CharTag[CurTag].AuxInt))
              return false;
            string auxText = this.e.CharTag[CurTag].AuxText;
            if (auxText != null && auxText.Length > 0 && !this.WriteRtfText(rtf, auxText, auxText.Length))
              return false;
            if (!this.EndRtfGroup(rtf))
              return false;
          }
          finally
          {
            this.e.BlockQuestionMarkAfterUnicode = false;
          }
        }
        else if (this.e.CharTag[CurTag].type == 80 /*0x50*/)
        {
          this.e.BlockQuestionMarkAfterUnicode = true;
          try
          {
            if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "*", 0, 0.0) || !this.WriteRtfControl(rtf, "mreplchartag", 0, 0.0))
              return false;
            string text = this.e.CharTag[CurTag].name;
            if (text == null || text == "")
              text = "MANUALREPLACEDCHAR";
            if (!this.WriteRtfText(rtf, text, text.Length) || !this.WriteRtfControl(rtf, "mreplchartagint", 1, (double) this.e.CharTag[CurTag].AuxInt))
              return false;
            string auxText = this.e.CharTag[CurTag].AuxText;
            if (auxText != null && auxText.Length > 0 && !this.WriteRtfText(rtf, auxText, auxText.Length))
              return false;
            if (!this.EndRtfGroup(rtf))
              return false;
          }
          finally
          {
            this.e.BlockQuestionMarkAfterUnicode = false;
          }
        }
        else if (this.e.CharTag[CurTag].type == 81)
        {
          this.e.BlockQuestionMarkAfterUnicode = true;
          try
          {
            if (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "*", 0, 0.0) || !this.WriteRtfControl(rtf, "chrfmttag", 0, 0.0))
              return false;
            string text = this.e.CharTag[CurTag].name;
            if (text == null || text == "")
              text = "FORMULAFORMAT";
            if (!this.WriteRtfText(rtf, text, text.Length) || !this.WriteRtfControl(rtf, "chrfmttagint", 1, (double) this.e.CharTag[CurTag].AuxInt))
              return false;
            string auxText = this.e.CharTag[CurTag].AuxText;
            if (auxText != null && auxText.Length > 0 && !this.WriteRtfText(rtf, auxText, auxText.Length))
              return false;
            if (!this.EndRtfGroup(rtf))
              return false;
          }
          finally
          {
            this.e.BlockQuestionMarkAfterUnicode = false;
          }
        }
      }
    }
    return true;
  }

  internal bool WriteRtfText(tc.ClsRtfOut rtf, string text, int TextLen)
  {
    return this.WriteRtfText(rtf, text.ToCharArray(), TextLen);
  }

  internal bool WriteRtfText(tc.ClsRtfOut rtf, char[] text, int TextLen)
  {
    char ch = '"';
    bool flag = (rtf.flags & 2) != 0;
    if ((rtf.flags & 4096 /*0x1000*/) != 0 && this.False(rtf.WritingControl))
      flag = true;
    if (TextLen != 0)
    {
      for (int index = 0; index < TextLen; ++index)
      {
        char CurChar = text[index];
        switch (CurChar)
        {
          case '\n':
          case '\r':
          case '\u001C':
            if (!this.PutRtfSpecChar(rtf, CurChar))
              return false;
            break;
          default:
            if (!tc.InIE || (int) CurChar != (int) ch)
            {
              if (CurChar == '\\' && !flag || CurChar == '{' || CurChar == '}' || CurChar == '\u000E' || CurChar == '\u0017' || CurChar == '\u0006')
              {
                if (!this.PutRtfChar(rtf, '\\'))
                  return false;
                rtf.flags |= 32 /*0x20*/;
              }
              switch (CurChar)
              {
                case '\u0004':
                  if (!this.WriteRtfControl(rtf, "zwnj", 0, 0.0))
                    return false;
                  break;
                case '\u0005':
                  if (!this.WriteRtfControl(rtf, "par", 0, 0.0))
                    return false;
                  break;
                case '\u0006':
                  if (!this.PutRtfChar(rtf, '-'))
                    return false;
                  break;
                case '\t':
                  if (!this.WriteRtfControl(rtf, "tab", 0, 0.0))
                    return false;
                  break;
                case '\f':
                  if (!this.WriteRtfControl(rtf, "page", 0, 0.0))
                    return false;
                  break;
                case '\u000E':
                  if (!this.PutRtfChar(rtf, '~'))
                    return false;
                  break;
                case '\u0016':
                  if (!this.WriteRtfControl(rtf, "column", 0, 0.0))
                    return false;
                  break;
                case '\u0017':
                  if (!this.PutRtfChar(rtf, '_'))
                    return false;
                  break;
                default:
                  if (!this.PutRtfChar(rtf, CurChar))
                    return false;
                  break;
              }
              rtf.flags = tc.ResetUintFlag(ref rtf.flags, 32 /*0x20*/);
              break;
            }
            goto case '\n';
        }
      }
    }
    return true;
  }

  internal bool WriteRtfWatermark(tc.ClsRtfOut rtf, bool WriteHeader)
  {
    rtf.WatermarkWritten = true;
    int pict = this.e.ParaFrame[this.e.WmParaFID].pict;
    if (WriteHeader && (!this.BeginRtfGroup(rtf) || !this.WriteRtfControl(rtf, "header", 0, 0.0)))
      return false;
    this.WriteRtfShape(rtf, pict);
    return !WriteHeader || this.EndRtfGroup(rtf);
  }
}
