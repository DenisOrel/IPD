// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.CSec
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class CSec : COp
{
  internal CSec(ImRtfEditor ImRtfEditor)
    : base(ImRtfEditor)
  {
  }

  internal new bool AdjustSections(int AfterLine, int count)
  {
    for (int index = 0; index < this.e.TotalSects; ++index)
    {
      if (this.e.TerSect[index].InUse)
      {
        if (this.e.TerSect[index].FirstLine > AfterLine && this.e.TerSect[index].FirstLine > 0)
          this.e.TerSect[index].FirstLine += count;
        if (this.e.TerSect[index].LastLine > AfterLine)
          this.e.TerSect[index].LastLine += count;
        if (this.e.TerSect1[index].hdr.FirstLine > AfterLine)
          this.e.TerSect1[index].hdr.FirstLine += count;
        if (this.e.TerSect1[index].hdr.LastLine > AfterLine)
          this.e.TerSect1[index].hdr.LastLine += count;
        if (this.e.TerSect1[index].ftr.FirstLine > AfterLine)
          this.e.TerSect1[index].ftr.FirstLine += count;
        if (this.e.TerSect1[index].ftr.LastLine > AfterLine)
          this.e.TerSect1[index].ftr.LastLine += count;
      }
    }
    this.e.TerSect[0].LastLine = this.e.TotalLines - 1;
    this.e.SectModified = true;
    return true;
  }

  internal bool CopyHeaderFooter(int SrcSect, int DestSect, char delim)
  {
    int pFirstLine1;
    if ((pFirstLine1 = this.GetFirstSectLine(SrcSect)) < 0)
      return false;
    int pCount;
    if (this.GetHdrFtrRange(delim, pFirstLine1, out pFirstLine1, out pCount))
    {
      for (int index = pFirstLine1; index < pFirstLine1 + pCount; ++index)
      {
        if (this.True(this.e.text[index].cid) || this.True(this.e.text[index].fid))
          return false;
      }
      int pFirstLine2;
      if ((pFirstLine2 = this.GetFirstSectLine(DestSect)) < 0)
        return false;
      this.MoveLineArrays(pFirstLine2, pCount, 'B');
      if (pFirstLine2 < pFirstLine1)
        pFirstLine1 += pCount;
      if (this.e.HilightType != 0 && pFirstLine2 < this.e.HilightBegRow)
        this.e.HilightBegRow += pCount;
      if (this.e.HilightType != 0 && pFirstLine2 < this.e.HilightEndRow)
        this.e.HilightEndRow += pCount;
      int num = 0;
      while (num < pCount)
      {
        this.CopyLineData(pFirstLine1, pFirstLine2);
        this.e.text[pFirstLine2].cid = 0;
        this.e.text[pFirstLine2].fid = 0;
        ++num;
        ++pFirstLine1;
        ++pFirstLine2;
      }
      if (this.GetHdrFtrRange(delim, pFirstLine2, out pFirstLine2, out pCount))
      {
        this.MoveLineArrays(pFirstLine2, pCount, 'D');
        if (this.e.HilightType != 0 && pFirstLine2 < this.e.HilightBegRow)
          this.e.HilightBegRow += pCount;
        if (this.e.HilightType != 0 && pFirstLine2 < this.e.HilightEndRow)
          this.e.HilightEndRow += pCount;
      }
    }
    return true;
  }

  internal new bool CreatePageHdrFtr(char type, int sect)
  {
    bool flag = type == '\u0011' || type == '\u0019';
    if (!this.CheckLineLimit(this.e.TotalLines + 3))
      return false;
    int section = this.GetSection(this.e.CurLine);
    int firstLine = this.e.TerSect[sect].FirstLine;
    int old = 0;
    int flags1 = this.e.PfmtId[old].flags;
    int flags2 = !flag ? flags1 & -4097 | 8192 /*0x2000*/ : flags1 & -8193 | 4096 /*0x1000*/;
    int num = this.NewParaId(old, this.e.PfmtId[old].LeftIndentTwips, this.e.PfmtId[old].RightIndentTwips, this.e.PfmtId[old].FirstIndentTwips, this.e.PfmtId[old].TabId, this.e.PfmtId[old].BltId, this.e.PfmtId[old].AuxId, this.e.PfmtId[old].Aux1Id, this.e.PfmtId[old].StyId, this.e.PfmtId[old].shading, this.e.PfmtId[old].pflags, this.e.PfmtId[old].SpaceBefore, this.e.PfmtId[old].SpaceAfter, this.e.PfmtId[old].SpaceBetween, this.e.PfmtId[old].LineSpacing, this.e.PfmtId[old].BkColor, this.e.PfmtId[old].BorderSpace, this.e.PfmtId[old].flow, flags2);
    this.MoveLineArrays(firstLine, 3, 'B');
    this.e.TerSect[sect].FirstLine = firstLine;
    for (int index = firstLine; index < firstLine + 3; ++index)
    {
      this.LineAlloc(index, 0, 1);
      char[] txt = this.e.text[index].txt;
      ushort[] numArray = this.OpenCfmt(index);
      txt[0] = type;
      if (index == firstLine + 1)
        txt[0] = this.e.ParaChar;
      numArray[0] = (ushort) 0;
      this.CloseCfmt(index);
      this.e.text[index].pfmt = num;
      if (index == firstLine || index == firstLine + 2)
        this.SetHdrFtrLineFlags(index, type);
    }
    if (type == '\u0011' && sect == section && !this.e.InRtfRead)
      this.e.CurLine = firstLine + 1;
    else if (this.e.CurLine >= firstLine)
      this.e.CurLine += 3;
    if (firstLine < this.e.HilightBegRow)
      this.e.HilightBegRow += 3;
    if (firstLine < this.e.HilightEndRow)
      this.e.HilightEndRow += 3;
    ++this.e.TerArg.modified;
    return true;
  }

  internal new bool CreateToc()
  {
    if (this.e.TotalLines == 0)
      return true;
    int modified = this.e.TerArg.modified;
    int num1 = 0;
    char level = char.MinValue;
    ushort OldFmt1 = 0;
    if (tc.DebugMode)
      this.misc.dm(nameof (CreateToc));
    if ((this.e.TerFlags4 & 16777216 /*0x01000000*/) != 0)
      return true;
    for (int line = 0; line < this.e.TotalLines; ++line)
    {
      if (!this.False(this.e.text[line].tag))
      {
        ushort[] tag = this.e.text[line].tag;
        int len = this.e.text[line].len;
        for (int col = 0; col < len; ++col)
        {
          if (tag[col] != (ushort) 0)
          {
            this.DeleteTag(line, col, 2, (string) null);
            if (this.True(this.e.text[line].tag))
              this.DeleteTag(line, col, 3, (string) null);
            if (this.False(this.e.text[line].tag))
              break;
          }
        }
      }
    }
    this.e.DocHasToc = this.e.MultipleToc = false;
    int num2 = 0;
    while (true)
    {
      int num3 = 0;
      int index1 = num2;
      while (index1 < this.e.TotalLines && (this.e.text[index1].flags & 16777216 /*0x01000000*/) == 0)
        ++index1;
      if (index1 != this.e.TotalLines)
      {
        int index2 = index1;
        string str1 = "";
        char ch1;
        char ch2 = ch1 = '1';
        char ch3;
        char ch4 = ch3 = '9';
        bool flag1;
        bool flag2 = flag1 = false;
        ushort[] numArray1 = this.OpenCfmt(index2);
        for (int index3 = 0; index3 < this.e.text[index2].len; ++index3)
        {
          ushort index4 = numArray1[index3];
          if (this.e.TerFont[(int) index4].FieldId == 9)
          {
            if (this.True(this.e.TerFont[(int) index4].FieldCode))
            {
              string fieldCode = this.e.TerFont[(int) index4].FieldCode;
              for (int index5 = 1; index5 < fieldCode.Length; ++index5)
              {
                if (fieldCode[index5 - 1] == '\\')
                {
                  if (fieldCode[index5] == 'o')
                    flag2 = true;
                  if (fieldCode[index5] == 'f')
                    flag1 = true;
                }
                if (fieldCode[index5] == '-' & flag2)
                {
                  ch2 = fieldCode[index5 - 1];
                  ch4 = fieldCode[index5 + 1];
                }
              }
              if (ch2 < '1')
                ch2 = '1';
              if (ch4 > '9')
                ch4 = '9';
              if ((int) ch2 > (int) ch4)
                ch2 = ch4;
              if (flag2)
              {
                ch1 = ch2;
                ch3 = ch4;
              }
              string lower = this.e.TerFont[(int) index4].FieldCode.ToLower();
              string str2 = "heading ";
              int num4;
              if ((num4 = lower.IndexOf(str2)) >= 0)
              {
                int index6 = num4 + str2.Length;
                ch1 = lower[index6];
                ch3 = lower[index6 + 2];
                if (ch1 < '1')
                  ch1 = '1';
                if (ch3 > '9')
                  ch3 = '9';
                if ((int) ch1 > (int) ch3)
                  ch1 = ch3;
              }
              str1 = this.e.TerFont[(int) index4].FieldCode;
              break;
            }
            break;
          }
        }
        if (!flag1)
          flag2 = true;
        int index7;
        for (index7 = index2; index7 < this.e.TotalLines; ++index7)
        {
          if ((this.e.text[index7].flags & 16777216 /*0x01000000*/) == 0)
          {
            if (this.e.text[index7].len != 0)
              break;
          }
          else if ((this.e.text[index7].flags & 2048 /*0x0800*/) != 0)
            break;
        }
        int num5 = index7 - 1;
        if (num5 < index2)
        {
          num2 = index2 + 1;
        }
        else
        {
          int count = num5 - index2 + 1;
          this.MoveLineArrays(index2, count, 'D');
          bool flag3 = this.e.CurLine > num5;
          bool flag4 = this.e.HilightType != 0 && this.e.HilightBegRow > num5;
          bool flag5 = this.e.HilightType != 0 && this.e.HilightEndRow > num5;
          if (flag3)
            this.e.CurLine -= count;
          if (flag4)
            this.e.HilightBegRow -= count;
          if (flag5)
            this.e.HilightEndRow -= count;
          if (!this.e.DocHasToc)
          {
            this.e.DocHasToc = true;
            this.e.FirstTocPos = this.RowColToAbs(index2, 0);
          }
          else
            this.e.MultipleToc = true;
          bool flag6 = false;
          for (int index8 = 0; index8 < this.e.TotalLines; ++index8)
          {
            if (this.e.text[index8].len != 0 && (this.e.text[index8].len != 1 || (this.e.text[index8].flags & 3) == 0 || (this.e.text[index8].flags & 4) == 0) && (this.e.PfmtId[this.e.text[index8].pfmt].flags & 12288 /*0x3000*/) == 0)
            {
              if (this.e.text[index8].len > 1 && (this.e.text[index8].flags & 3) != 0 && (this.e.text[index8].flags & 4) != 0)
              {
                int num6 = this.e.text[index8].len - 1;
                char[] txt = this.e.text[index8].txt;
                int index9 = 0;
                while (index9 < num6 && (txt[index9] == ' ' || txt[index9] == '\t'))
                  ++index9;
                if (index9 == num6)
                  continue;
              }
              if (flag6)
              {
                if ((this.e.text[index8].flags & 3) != 0)
                  flag6 = false;
              }
              else
              {
                bool flag7 = false;
                if (index8 == 0 || (this.e.text[index8 - 1].flags & 3) != 0)
                {
                  ushort[] numArray2 = this.OpenCfmt(index8);
                  int num7 = 0;
                  for (int index10 = 0; index10 < this.e.text[index8].len; ++index10)
                  {
                    if ((this.e.TerFont[(int) numArray2[index10]].style & 64 /*0x40*/) != 0)
                      ++num7;
                  }
                  this.CloseCfmt(index8);
                  if ((this.e.text[index8].flags & 3) != 0 && num7 > 0)
                    ++num7;
                  if (num7 >= this.e.text[index8].len)
                    flag7 = true;
                }
                if ((this.e.text[index8].flags & 16777216 /*0x01000000*/) == 0)
                {
                  int styId1 = this.e.PfmtId[this.e.text[index8].pfmt].StyId;
                  if (styId1 != 0)
                  {
                    bool flag8;
                    bool flag9 = flag8 = false;
                    if (this.True(styId1) & flag2)
                    {
                      flag9 = true;
                      int length1 = "heading ".Length;
                      string str1_1 = this.e.StyleId[styId1].name;
                      if (str1_1.Length > length1)
                        str1_1 = str1_1.Substring(0, length1);
                      if (this.strcmpi(str1_1, "heading ") != 0)
                        flag9 = false;
                      if (flag9)
                      {
                        int length2 = "heading ".Length;
                        if (this.e.StyleId[styId1].name.Length > length2 + 1)
                        {
                          switch (this.e.StyleId[styId1].name[length2 + 1])
                          {
                            case ' ':
                            case ',':
                              break;
                            default:
                              flag9 = false;
                              break;
                          }
                        }
                        if (flag9)
                          level = this.e.StyleId[styId1].name[length2];
                      }
                      if (!flag9 && this.e.StyleId[styId1].OutlineLevel >= 0)
                      {
                        flag9 = true;
                        level = (char) (this.e.StyleId[styId1].OutlineLevel + 49);
                      }
                      if (flag9 && ((int) level < (int) ch2 || (int) level > (int) ch4) && ((int) level < (int) ch1 || (int) level > (int) ch3))
                        flag9 = false;
                      if (flag9 & flag7)
                        flag9 = false;
                    }
                    if (!flag9 & flag1 && (this.e.text[index8].flags2 & 4) != 0)
                      flag8 = true;
                    if (flag9 | flag8)
                    {
                      if (!this.CheckLineLimit(this.e.TotalLines + 1))
                        return true;
                      this.MoveLineArrays(index2, 1, 'B');
                      if (index8 >= index2)
                        ++index8;
                      if (flag3)
                        ++this.e.CurLine;
                      if (flag4)
                        ++this.e.HilightBegRow;
                      if (flag5)
                        ++this.e.HilightEndRow;
                      int len1;
                      if (flag9)
                      {
                        this.LineAlloc(index2, 0, this.e.text[index8].len);
                        this.MoveCharInfo(index8, 0, index2, 0, this.e.text[index8].len);
                        this.e.text[index2].pfmt = this.e.text[index8].pfmt;
                        this.e.text[index2].cid = this.e.text[index2 + 1].cid;
                        this.e.text[index2].fid = this.e.text[index2 + 1].fid;
                        int len2 = this.e.text[index2].len;
                        if ((this.e.text[index8].flags & 67108864 /*0x04000000*/) != 0)
                        {
                          char[] txt = this.e.text[index8].txt;
                          int index11 = 0;
                          while (index11 < len2 && txt[index11] != '\u0005')
                            ++index11;
                          if (index11 < len2)
                          {
                            this.LineAlloc(index2, this.e.text[index8].len, index11 + 1);
                            int len3 = this.e.text[index2].len;
                            this.e.text[index2].txt[len3 - 1] = this.e.ParaChar;
                            flag6 = true;
                          }
                        }
                        if ((this.e.text[index8].flags & 2048 /*0x0800*/) != 0)
                        {
                          int len4 = this.e.text[index2].len;
                          char[] txt = this.e.text[index2].txt;
                          if (len4 > 0 && txt[len4 - 1] == '\u0014')
                          {
                            if (len4 == 1 || (int) txt[len4 - 2] != (int) this.e.ParaChar)
                              txt[len4 - 1] = this.e.ParaChar;
                            else
                              this.LineAlloc(index2, len4, len4 - 1);
                          }
                        }
                        int len5 = this.e.text[index2].len;
                        if (len5 > 0 && (this.e.text[index8].flags & 3) != 0)
                        {
                          char[] txt = this.e.text[index2].txt;
                          char chr = txt[len5 - 1];
                          if ((int) chr == (int) this.e.CellChar || this.lstrchr(this.e.BreakChars, chr))
                            txt[len5 - 1] = this.e.ParaChar;
                          ushort[] numArray3 = this.OpenCfmt(index2);
                          if ((this.e.TerFont[(int) numArray3[len5 - 1]].style & 64 /*0x40*/) != 0)
                            numArray3[len5 - 1] = len5 < 2 ? (ushort) this.SetFontStyle((int) numArray3[len5 - 1], 64 /*0x40*/, false) : numArray3[len5 - 2];
                          this.CloseCfmt(index2);
                        }
                        ushort[] numArray4 = this.OpenCfmt(index2);
                        int OldFmt2 = -1;
                        for (int index12 = 0; index12 < this.e.text[index2].len; ++index12)
                        {
                          int num8 = OldFmt2;
                          OldFmt2 = (int) numArray4[index12];
                          if (index12 == 0 || num8 != OldFmt2)
                          {
                            int newPointSize = (int) this.GetNewPointSize((ushort) OldFmt2, this.e.TerArg.PointSize * 20, 0, index2, 0);
                            numArray4[index12] = (ushort) newPointSize;
                          }
                          else
                            numArray4[index12] = numArray4[index12 - 1];
                        }
                      }
                      else if (flag8)
                      {
                        int SrcCol = -1;
                        int num9 = -1;
                        ushort[] numArray5 = this.OpenCfmt(index8);
                        char[] txt1 = this.e.text[index8].txt;
                        bool flag10 = flag6 = false;
                        level = '1';
                        for (int index13 = 0; index13 < this.e.text[index8].len; ++index13)
                        {
                          if (SrcCol == -1 && this.e.TerFont[(int) numArray5[index13]].FieldId == 13)
                            SrcCol = index13;
                          if (SrcCol != -1)
                          {
                            if (!flag10 && SrcCol == index13)
                            {
                              string fieldCode = this.e.TerFont[(int) numArray5[index13]].FieldCode;
                              if (fieldCode != null && fieldCode.Length >= 4)
                                flag10 = fieldCode[0] == '\\' && fieldCode[1] == 't' && fieldCode[2] == 'c' && fieldCode[3] == 'l';
                              if (flag10 && fieldCode != null && fieldCode.Length >= 5)
                                level = fieldCode[4];
                            }
                            num9 = index13;
                            if (this.e.TerFont[(int) numArray5[index13]].FieldId != 13)
                            {
                              num9 = index13 - 1;
                              break;
                            }
                          }
                        }
                        if (SrcCol != -1)
                        {
                          if (num9 == -1)
                            num9 = this.e.text[index8].len - 1;
                          this.LineAlloc(index2, 0, num9 - SrcCol + 1);
                          this.MoveCharInfo(index8, SrcCol, index2, 0, this.e.text[index2].len);
                          this.e.text[index2].pfmt = 0;
                          this.e.text[index2].cid = this.e.text[index2 + 1].cid;
                          this.e.text[index2].fid = this.e.text[index2 + 1].fid;
                          len1 = this.e.text[index2].len;
                          ushort[] numArray6 = this.OpenCfmt(index2);
                          int num10 = -1;
                          for (int index14 = 0; index14 < this.e.text[index2].len; ++index14)
                          {
                            int num11 = num10;
                            num10 = (int) numArray6[index14];
                            if (index14 == 0 || num11 != num10)
                            {
                              int CurFont = num10;
                              if ((this.e.TerFont[CurFont].style & 64 /*0x40*/) != 0)
                                CurFont = this.SetFontStyle(CurFont, 64 /*0x40*/, false);
                              int newPointSize = (int) this.GetNewPointSize((ushort) this.SetFontFieldId(CurFont, 0, (string) null), this.e.TerArg.PointSize * 20, 0, index2, 0);
                              numArray6[index14] = (ushort) newPointSize;
                            }
                            else
                              numArray6[index14] = numArray6[index14 - 1];
                          }
                          if (flag6)
                          {
                            int len6 = this.e.text[index2].len;
                            this.LineAlloc(index2, len6, len6 + 1);
                            char[] txt2 = this.e.text[index2].txt;
                            ushort[] numArray7 = this.OpenCfmt(index2);
                            txt2[len6] = this.e.ParaChar;
                            numArray7[len6] = numArray7[len6 - 1];
                            this.e.text[index2].flags |= 1;
                          }
                        }
                        else
                          continue;
                      }
                      char[] txt3 = this.e.text[index2].txt;
                      int len7 = this.e.text[index2].len;
                      for (int index15 = 0; index15 < len7; ++index15)
                      {
                        if (txt3[index15] == '\t')
                          txt3[index15] = ' ';
                      }
                      for (int StartPos = 1; StartPos < this.e.text[index2].len; ++StartPos)
                      {
                        if (txt3[StartPos - 1] == ' ' && txt3[StartPos] == ' ')
                        {
                          this.MoveLineData(index2, StartPos, 1, 'D');
                          txt3 = this.e.text[index2].txt;
                          --StartPos;
                        }
                      }
                      len1 = this.e.text[index2].len;
                      for (int index16 = 0; index16 < this.e.text[index2].len; ++index16)
                      {
                        int curCfmt = this.GetCurCfmt(index2, index16);
                        if ((this.e.TerFont[curCfmt].style & 40000) != 0 || this.e.TerFont[curCfmt].FieldId == 6)
                        {
                          this.MoveLineData(index2, index16, 1, 'D');
                          --index16;
                        }
                      }
                      int len8 = this.e.text[index2].len;
                      if (this.True(this.e.text[index2].tag))
                      {
                        ushort[] tag = this.e.text[index2].tag;
                        for (int index17 = 0; index17 < len8; ++index17)
                          tag[index17] = (ushort) 0;
                        this.CloseCtid(index2);
                      }
                      if (flag9 && this.True(this.e.text[index8].tabw) && this.True(this.e.text[index8].tabw.ListText) && (this.e.text[index8].flags & 33554432 /*0x02000000*/) != 0)
                      {
                        int length = this.e.text[index8].tabw.ListText.Length;
                        string listText = this.e.text[index8].tabw.ListText;
                        if (length > 0 && listText[length - 1] != ' ')
                        {
                          listText += "    ";
                          length = listText.Length;
                        }
                        this.MoveLineData(index2, 0, length, 'B');
                        char[] txt4 = this.e.text[index2].txt;
                        ushort[] numArray8 = this.OpenCfmt(index2);
                        for (int index18 = 0; index18 < length; ++index18)
                        {
                          txt4[index18] = listText[index18];
                          numArray8[index18] = numArray8[length];
                        }
                      }
                      int len9 = this.e.text[index2].len;
                      ushort[] numArray9 = this.OpenCfmt(index2);
                      ushort maxValue = ushort.MaxValue;
                      for (int col = 0; col < len9; ++col)
                      {
                        if ((int) numArray9[col] != (int) maxValue)
                        {
                          maxValue = numArray9[col];
                          if ((this.e.TerFont[(int) maxValue].style & 128 /*0x80*/) != 0)
                          {
                            this.e.TerFont[(int) maxValue].FieldId = 9;
                          }
                          else
                          {
                            OldFmt1 = this.GetNewFieldId(maxValue, 9, str1.Length == 0 ? (string) null : str1, index2, col);
                            if (this.e.TerFont[(int) OldFmt1].CharStyId != 1)
                              OldFmt1 = this.GetNewCharStyle(OldFmt1, 1, 0, "", index2, 0);
                          }
                        }
                        numArray9[col] = OldFmt1;
                      }
                      int tocStyle = this.GetTocStyle(level);
                      int styId2 = !flag9 ? 0 : this.e.PfmtId[this.e.text[index8].pfmt].StyId;
                      tc.StrPfmt para = this.e.PfmtId[this.e.text[index8].pfmt] with
                      {
                        pflags = 0
                      };
                      this.SetParaStyleId(ref para, this.e.StyleId[para.StyId], this.e.StyleId[tocStyle], true);
                      if (para.TabId == 0)
                      {
                        if (num1 == 0)
                        {
                          tc.StrTab pTabRec = new tc.StrTab();
                          pTabRec.SetSize(1);
                          int num12 = this.ScrToTwipsX(this.TerWrapWidth(index2, -1)) - 90;
                          pTabRec.pos[0] = num12;
                          pTabRec.type[0] = this.e.TocTabAlign;
                          pTabRec.flags[0] = (byte) this.e.TocTabLeader;
                          num1 = this.e.TerCreateTabId(pTabRec);
                        }
                        para.TabId = num1;
                      }
                      this.e.text[index2].pfmt = this.NewParaId(this.e.text[index8].pfmt, para.LeftIndentTwips, para.RightIndentTwips, para.FirstIndentTwips, para.TabId, para.BltId, para.AuxId, para.Aux1Id, tocStyle, para.shading, para.pflags, para.SpaceBefore, para.SpaceAfter, para.SpaceBetween, para.LineSpacing, para.BkColor, para.BorderSpace, para.flow, para.flags);
                      this.ApplyLineTextStyle(index2, tocStyle, styId2);
                      this.e.text[index2].flags |= 16777216 /*0x01000000*/;
                      if ((this.e.text[index8].flags & 2049) != 0 | flag6)
                        this.e.text[index2].flags |= 1;
                      if ((this.e.text[index2].flags & 1) != 0 && this.e.text[index8].len > 0 && this.e.TocShowPageNo)
                      {
                        int page = this.e.text[index8].page;
                        int len10 = this.e.text[index8].len;
                        int AuxInt;
                        if (this.e.MultipleToc)
                        {
                          AuxInt = this.GetTag(index8, len10 - 1, 3, out tc.SkipStr, out tc.SkipStr, out tc.SkipInt, out tc.SkipObject);
                        }
                        else
                        {
                          string name = "Heading" + (object) num3;
                          ++num3;
                          AuxInt = this.SetTag(index8, len10 - 1, 3, name, (string) null, page);
                        }
                        int dispNbr = this.e.PageInfo[page].DispNbr;
                        string str3 = new string('\t', 1);
                        int length3 = str3.Length;
                        string str4 = str3 + dispNbr.ToString();
                        int length4 = str4.Length;
                        int StartPos = this.e.text[index2].len - 1;
                        this.MoveLineData(index2, StartPos, length4, 'B');
                        char[] txt5 = this.e.text[index2].txt;
                        for (int index19 = 0; index19 < length4; ++index19)
                          txt5[StartPos + index19] = str4[index19];
                        string name1 = "Toc" + (object) num3;
                        this.SetTag(index2, StartPos + length3, 2, name1, (string) null, AuxInt);
                      }
                      ++index2;
                    }
                  }
                }
              }
            }
          }
          num2 = index2;
        }
      }
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

  internal new bool ExpandSectArray(int NewMax)
  {
    if (NewMax < 0)
      NewMax = this.e.MaxSects * 3 / 2;
    this.e.TerSect = this.ReAlloc(this.e.TerSect, NewMax);
    this.e.TerSect1 = this.ReAlloc(this.e.TerSect1, NewMax);
    this.e.MaxSects = NewMax;
    return true;
  }

  internal bool ForceSectOrient() => (this.e.TerFlags & 8) != 0;

  internal int GetFirstSectLine(int sec)
  {
    if (sec == 0)
      return 0;
    for (int firstSectLine = 0; firstSectLine < this.e.TotalLines; ++firstSectLine)
    {
      int len = this.e.text[firstSectLine].len;
      if (len > 0 && this.e.text[firstSectLine].txt[len - 1] == '\u0014')
        --sec;
      if (sec == 0)
      {
        if (firstSectLine + 1 < this.e.TotalLines)
          ++firstSectLine;
        return firstSectLine;
      }
    }
    return -1;
  }

  internal new int GetHdrFtrFlag(int LineNo)
  {
    if (LineNo >= 0 && LineNo < this.e.TotalLines && (this.e.PfmtId[this.e.text[LineNo].pfmt].flags & 12288 /*0x3000*/) != 0)
    {
      for (int index = LineNo; index < this.e.TotalLines; ++index)
      {
        if ((this.e.text[index].flags & 1966080 /*0x1E0000*/) != 0)
          return this.e.text[index].flags & 1966080 /*0x1E0000*/;
      }
    }
    return 0;
  }

  internal new bool GetHdrFtrRange(char delim, int StartLine, out int pFirstLine, out int pCount)
  {
    int num1 = 0;
    int num2;
    pCount = num2 = 0;
    pFirstLine = num2;
    bool flag = false;
    int num3 = 0;
    int index;
    for (index = StartLine; index < this.e.TotalLines; ++index)
    {
      if ((this.e.PfmtId[this.e.text[index].pfmt].flags & 12288 /*0x3000*/) == 0)
        return false;
      if (flag)
        ++num3;
      if (this.e.text[index].len == 1 && (int) this.e.text[index].txt[0] == (int) delim)
      {
        if (!flag)
        {
          flag = true;
          num1 = index;
          ++num3;
        }
        else
          break;
      }
    }
    if (!flag || index == this.e.TotalLines)
      return false;
    pFirstLine = num1;
    pCount = num3;
    return true;
  }

  internal new bool GetSectColWidthSpace(
    int TopSect,
    int CurSect,
    out int ColumnWidth,
    out int ColumnSpace,
    out int TextX,
    out int YBefHdr)
  {
    YBefHdr = 0;
    TextX = 0;
    int num1 = (int) ((double) this.e.UnitResX * (double) this.e.TerSect[TopSect].LeftMargin);
    int num2 = (int) ((double) this.e.UnitResX * (double) this.e.TerSect[TopSect].RightMargin);
    int num3 = (int) ((double) this.e.UnitResX * (double) this.e.TerSect1[TopSect].PgWidth) - num1 - num2;
    if (this.e.TerArg.FittedView)
      num3 = this.ScrToUnitX(this.e.TerWinWidth);
    int columns = this.e.TerSect[CurSect].columns;
    ColumnSpace = (int) ((double) this.e.UnitResX * (double) this.e.TerSect[CurSect].ColumnSpace);
    if (columns == 1)
      ColumnSpace = 0;
    ColumnWidth = (num3 - (columns - 1) * ColumnSpace) / columns;
    if (this.e.BorderShowing)
      TextX = this.e.LeftBorderWidth + num1;
    if (this.e.BorderShowing)
      YBefHdr = (int) ((double) this.e.UnitResY * (double) this.e.TerSect[TopSect].HdrMargin) + this.e.TopBorderHeight;
    else if (this.e.ViewPageHdrFtr)
      YBefHdr = (int) ((double) this.e.UnitResY * (double) this.e.TerSect[TopSect].HdrMargin) - this.e.TerSect1[TopSect].HiddenY;
    return true;
  }

  internal new int GetSection(int lin)
  {
    int section1 = -1;
    if (this.e.TerArg.WordWrap && this.e.TotalSects != 1)
    {
      if (this.e.KnownSect >= 0 && lin >= this.e.KnownSectBegLine && lin <= this.e.KnownSectEndLine)
        return this.e.KnownSect;
      int index1;
      for (index1 = 0; index1 < this.e.TotalSects; ++index1)
      {
        if (this.e.TerSect[index1].InUse)
        {
          int firstLine = this.e.TerSect[index1].FirstLine;
          int lastLine = this.e.TerSect[index1].LastLine;
          if (firstLine >= 0 && firstLine < this.e.TotalLines && lastLine >= 0 && lastLine < this.e.TotalLines && firstLine <= lastLine)
          {
            int prevSect = this.e.TerSect1[index1].PrevSect;
            int num1 = prevSect < 0 ? -1 : this.e.TerSect[prevSect].LastLine;
            if (firstLine == num1 + 1)
            {
              int nextSect = this.e.TerSect1[index1].NextSect;
              int num2 = nextSect < 0 ? this.e.TotalLines : this.e.TerSect[nextSect].FirstLine;
              if (lastLine == num2 - 1)
              {
                if (lin >= firstLine && lin <= lastLine)
                  section1 = index1;
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
      }
      if (index1 >= this.e.TotalSects && section1 >= 0)
        return section1;
      int num = 0;
      for (int line = 0; line < this.e.TotalLines; ++line)
      {
        int len = this.e.text[line].len;
        int AuxInt;
        if (len > 0 && this.e.text[line].txt[len - 1] == '\u0014' && this.GetTag(line, len - 1, 4, out tc.SkipStr, out tc.SkipStr, out AuxInt, out tc.SkipObject) > 0)
        {
          int index2 = AuxInt;
          this.e.TerSect[index2].LastLine = line;
          int nextSect = this.e.TerSect1[index2].NextSect;
          if (nextSect >= 0)
            this.e.TerSect[nextSect].FirstLine = line + 1;
          if (num == 0)
            this.e.TerSect[index2].FirstLine = 0;
          ++num;
        }
      }
      this.e.TerSect[0].LastLine = this.e.TotalLines - 1;
      for (int section2 = 1; section2 < this.e.TotalSects; ++section2)
      {
        if (this.e.TerSect[section2].InUse && lin >= this.e.TerSect[section2].FirstLine && lin <= this.e.TerSect[section2].LastLine)
          return section2;
      }
    }
    return 0;
  }

  internal int GetTocStyle(char level)
  {
    string str1 = "toc ";
    if (tc.DebugMode)
      this.misc.dm(nameof (GetTocStyle));
    string str2 = str1 + new string(level, 1);
    int tocStyle = 0;
    while (tocStyle < this.e.TotalSID && (!this.e.StyleId[tocStyle].InUse || this.e.StyleId[tocStyle].type != 2 || !(this.e.StyleId[tocStyle].name == str2)))
      ++tocStyle;
    if (tocStyle < this.e.TotalSID)
      return tocStyle;
    int styleIdSlot = this.GetStyleIdSlot();
    if (styleIdSlot < 0)
      return 0;
    this.e.StyleId[styleIdSlot] = new tc.StrStyleId();
    this.e.StyleId[styleIdSlot].InUse = true;
    this.e.StyleId[styleIdSlot].type = 2;
    this.e.StyleId[styleIdSlot].name = str2;
    this.e.StyleId[styleIdSlot].TypeFace = this.e.TerFont[0].TypeFace;
    this.e.StyleId[styleIdSlot].FontFamily = this.e.TerFont[0].FontFamily;
    this.e.StyleId[styleIdSlot].TwipsSize = this.e.TerFont[0].TwipsSize;
    this.e.StyleId[styleIdSlot].style = 0;
    this.e.StyleId[styleIdSlot].TextColor = this.e.TerFont[0].TextColor;
    this.e.StyleId[styleIdSlot].TextBkColor = this.e.TerFont[0].TextBkColor;
    this.e.StyleId[styleIdSlot].LeftIndentTwips = 200 * ((int) level - 49);
    this.e.StyleId[styleIdSlot].ParaBkColor = this.e.TextDefBkColor;
    this.e.StyleId[styleIdSlot].TabId = 0;
    this.e.StyleId[styleIdSlot].flags |= 1;
    return styleIdSlot;
  }

  internal new bool HdrFtrExists(tc.StrHdrFtr hdr)
  {
    return hdr.LastLine - hdr.FirstLine > 2 || hdr.FirstLine >= 0 && hdr.FirstLine + 1 < this.e.TotalLines && this.e.text[hdr.FirstLine + 1].len > 1;
  }

  internal new bool InitSect(int sect)
  {
    this.e.TerSect[sect] = new tc.StrSect();
    this.e.TerSect1[sect] = new tc.StrSect1();
    this.e.TerSect[sect].InUse = true;
    float num1;
    this.e.TerSect[sect].RightMargin = num1 = 1.25f;
    this.e.TerSect[sect].LeftMargin = num1;
    float num2;
    this.e.TerSect[sect].BotMargin = num2 = 1f;
    this.e.TerSect[sect].TopMargin = num2;
    float num3;
    this.e.TerSect[sect].FtrMargin = num3 = 0.5f;
    this.e.TerSect[sect].HdrMargin = num3;
    this.e.TerSect[sect].columns = 1;
    this.e.TerSect[sect].ColumnSpace = 0.5f;
    this.e.TerSect[sect].FirstLine = 0;
    this.e.TerSect[sect].LastLine = this.e.TotalLines - 1;
    this.e.TerSect[sect].flags = 1;
    this.e.TerSect[sect].IsPortrait = this.e.IsPortrait;
    this.e.TerSect[sect].FirstPageNo = (short) 1;
    this.e.TerSect[sect].BorderColor = tc.CLR_AUTO;
    this.e.TerSect[sect].PprKind = this.e.PprKind;
    this.e.TerSect[sect].PprWidth = this.e.PageWidth;
    this.e.TerSect[sect].PprHeight = this.e.PageHeight;
    this.e.TerSect1[sect].hdr.FirstLine = -1;
    this.e.TerSect1[sect].ftr.FirstLine = -1;
    this.e.TerSect1[sect].fhdr.FirstLine = -1;
    this.e.TerSect1[sect].fftr.FirstLine = -1;
    this.e.TerSect1[sect].PgWidth = this.e.PageWidth;
    this.e.TerSect1[sect].PgHeight = this.e.PageHeight;
    int num4;
    this.e.TerSect1[sect].ftr.height = num4 = 0;
    this.e.TerSect1[sect].hdr.height = num4;
    int num5;
    this.e.TerSect1[sect].fftr.height = num5 = 0;
    this.e.TerSect1[sect].fhdr.height = num5;
    this.e.TerSect1[sect].PrevSect = -1;
    return true;
  }

  internal new bool RecreateSections()
  {
    int num1 = 0;
    int num2 = 0;
    int num3 = 0;
    int num4 = 0;
    int index1 = -1;
    bool flag = false;
    if (this.e.TerArg.WordWrap)
    {
      for (int index2 = 0; index2 < this.e.TotalSects; ++index2)
        this.e.TerSect[index2].InUse = false;
      int num5 = 0;
      int pageModifyCount = this.e.PageModifyCount;
      int num6;
      int num7 = num6 = -1;
      int num8;
      int num9 = num8 = -1;
      for (int line = 0; line < this.e.TotalLines; ++line)
      {
        if ((this.e.text[line].flags & 131072 /*0x020000*/) != 0)
        {
          if (num9 == -1)
            num9 = line;
          num3 = line;
        }
        else if ((this.e.text[line].flags & 524288 /*0x080000*/) != 0)
        {
          if (num7 == -1)
            num7 = line;
          num1 = line;
        }
        else if ((this.e.text[line].flags & 262144 /*0x040000*/) != 0)
        {
          if (num8 == -1)
            num8 = line;
          num4 = line;
        }
        else if ((this.e.text[line].flags & 1048576 /*0x100000*/) != 0)
        {
          if (num6 == -1)
            num6 = line;
          num2 = line;
        }
        int num10 = -1;
        int len = this.e.text[line].len;
        char[] txt = this.e.text[line].txt;
        if (len > 0 && txt[len - 1] == '\u0014')
        {
          int AuxInt;
          num10 = this.GetTag(line, len - 1, 4, out tc.SkipStr, out tc.SkipStr, out AuxInt, out tc.SkipObject) <= 0 ? -1 : AuxInt;
        }
        if (num10 == -1)
        {
          if (this.True(this.e.text[line].tabw) && (this.e.text[line].tabw.type & 2) != 0)
            tc.ResetUintFlag(ref this.e.text[line].tabw.type, 2);
        }
        else
        {
          if (this.False(this.e.text[line].tabw))
            this.e.text[line].tabw = new tc.ClsTabw();
          this.e.text[line].tabw.type |= 2;
          this.e.text[line].tabw.section = num10;
          int section = this.e.text[line].tabw.section;
          this.e.TerSect[section].InUse = true;
          if (this.e.TerSect[section].FirstLine != num5 || this.e.TerSect[section].LastLine != line)
            flag = true;
          this.e.TerSect[section].FirstLine = num5;
          this.e.TerSect[section].LastLine = line;
          if (num5 == 0)
            this.e.TerSect[section].flags |= 1;
          this.e.TerSect1[section].PrevSect = index1;
          this.e.TerSect1[section].NextSect = -1;
          if (index1 >= 0)
            this.e.TerSect1[index1].NextSect = section;
          this.e.TerSect1[section].hdr.FirstLine = num7;
          this.e.TerSect1[section].hdr.LastLine = num1;
          this.e.TerSect1[section].ftr.FirstLine = num6;
          this.e.TerSect1[section].ftr.LastLine = num2;
          this.e.TerSect1[section].fhdr.FirstLine = num9;
          this.e.TerSect1[section].fhdr.LastLine = num3;
          this.e.TerSect1[section].fftr.FirstLine = num8;
          this.e.TerSect1[section].fftr.LastLine = num4;
          index1 = section;
          num7 = num6 = -1;
          num9 = num8 = -1;
          num5 = line + 1;
        }
      }
      if (this.e.TerSect[0].FirstLine != num5)
        flag = true;
      this.e.TerSect[0].FirstLine = num5;
      this.e.TerSect[0].LastLine = this.e.TotalLines - 1;
      this.e.TerSect1[0].PrevSect = index1;
      this.e.TerSect1[0].NextSect = -1;
      if (index1 >= 0)
        this.e.TerSect1[index1].NextSect = 0;
      this.e.TerSect1[0].hdr.FirstLine = num7;
      this.e.TerSect1[0].hdr.LastLine = num1;
      this.e.TerSect1[0].ftr.FirstLine = num6;
      this.e.TerSect1[0].ftr.LastLine = num2;
      this.e.TerSect1[0].fhdr.FirstLine = num9;
      this.e.TerSect1[0].fhdr.LastLine = num3;
      this.e.TerSect1[0].fftr.FirstLine = num8;
      this.e.TerSect1[0].fftr.LastLine = num4;
      this.e.TerSect[0].InUse = true;
      this.SetSectPageSize();
      this.e.SectModified = false;
      if (this.e.PageModifyCount != pageModifyCount || flag)
        return true;
    }
    return false;
  }

  internal new bool RepairHdrFtrDelims(int FirstLine, int LastLine)
  {
    char HdrFtrChar = char.MinValue;
    int num = -1;
    for (int index = FirstLine; index <= LastLine; ++index)
    {
      char x = this.e.text[index].len != 1 ? char.MinValue : this.GetCurChar(index, 0);
      if (this.IsHdrFtrChar(x))
      {
        if (num == -1)
        {
          num = index;
          HdrFtrChar = x;
        }
        else
        {
          num = -1;
          if ((int) x != (int) HdrFtrChar)
            this.e.text[index].txt[0] = HdrFtrChar;
        }
        this.SetHdrFtrLineFlags(index, HdrFtrChar);
      }
      else
        this.e.text[index].flags &= -1966081;
    }
    return true;
  }

  internal new bool ReposPageHdrFtr(bool repaint)
  {
    bool flag1 = false;
    if (this.e.TerArg.WordWrap)
    {
      if (this.e.HilightType != 0)
        this.e.HilightType = 0;
      this.RecreateSections();
      int num1;
      bool flag2 = (num1 = 0) != 0;
      bool flag3 = num1 != 0;
      bool flag4 = num1 != 0;
      int LastLine;
      int FirstLine = LastLine = -1;
      int sect = 0;
      for (int index1 = this.e.TotalLines - 1; index1 >= -1; --index1)
      {
        bool flag5 = index1 >= 0 && ((this.e.text[index1].flags & 2048 /*0x0800*/) != 0 || this.LineInfo(index1, 2));
        if (flag5 && index1 >= 0)
        {
          char[] txt = this.e.text[index1].txt;
          int index2 = 0;
          while (index2 < this.e.text[index1].len && txt[index2] != '\u0014')
            ++index2;
          if (index2 == this.e.text[index1].len)
            flag5 = false;
        }
        if (index1 == -1 | flag5)
        {
          if (flag4 && FirstLine > index1 + 1)
          {
            this.RepairHdrFtrDelims(FirstLine, LastLine);
            int count = LastLine - FirstLine + 1;
            if (!this.CheckLineLimit(this.e.TotalLines + count))
              return true;
            this.MoveLineArrays(index1 + 1, count, 'B');
            int StartLine = FirstLine + count;
            int num2 = LastLine + count;
            for (int index3 = 0; index3 < count; ++index3)
            {
              this.FreeLine(index1 + index3 + 1);
              this.e.text[index1 + index3 + 1] = this.e.text[StartLine + index3];
              this.e.text[StartLine + index3] = (tc.ClsLinePtr) null;
            }
            this.MoveLineArrays(StartLine, count, 'D');
            flag1 = true;
          }
          if ((this.e.TerSect[sect].flags & 4) != 0 && this.e.TerSect1[sect].PrevSect < 0)
          {
            if (!flag2 && !this.CreatePageHdrFtr('\u001A', sect) || !flag3 && !this.CreatePageHdrFtr('\u0019', sect))
              return false;
            tc.ResetUintFlag(ref this.e.TerSect[sect].flags, 4);
            flag1 = true;
          }
          if (index1 != -1)
          {
            int num3;
            flag2 = (num3 = 0) != 0;
            flag3 = num3 != 0;
            flag4 = num3 != 0;
            FirstLine = LastLine = -1;
            if (index1 >= 0 && this.LineInfo(index1, 2))
              sect = this.e.text[index1].tabw.section;
          }
          else
            break;
        }
        if ((this.e.PfmtId[this.e.text[index1].pfmt].flags & 12288 /*0x3000*/) != 0)
        {
          if (this.e.text[index1].len == 1 && this.GetCurChar(index1, 0) == '\u0019')
            flag3 = true;
          if (this.e.text[index1].len == 1 && this.GetCurChar(index1, 0) == '\u001A')
            flag2 = true;
          if (flag4)
          {
            this.MoveLineArrays(index1, 1, 'D');
            --FirstLine;
            --LastLine;
            flag1 = true;
          }
          else
          {
            if (LastLine == -1)
              LastLine = index1;
            FirstLine = index1;
          }
        }
        else if (LastLine >= 0)
          flag4 = true;
      }
      if (this.e.TotalLines <= 0)
      {
        this.e.TotalLines = 1;
        this.InitLine(0);
      }
      if (this.e.CurLine >= this.e.TotalLines)
        this.e.CurLine = this.e.TotalLines - 1;
      if (flag1)
      {
        this.RecreateSections();
        this.RepairTable();
        if (this.e.TotalLines < 5000)
          this.Repaginate(false, false, 0, true);
      }
      this.e.SectModified = false;
      this.e.PosPageHdrFtr = false;
      if (repaint)
        this.PaintTer();
    }
    return true;
  }

  internal new bool ResetHdrFtr(ref tc.StrHdrFtr hdr)
  {
    hdr.FirstLine = -1;
    hdr.height = 0;
    hdr.TextHeight = 0;
    return true;
  }

  internal new bool SetHdrFtrLineFlags(int LineNo, char HdrFtrChar)
  {
    this.e.text[LineNo].flags &= -1966081;
    switch (HdrFtrChar)
    {
      case '\u0010':
        this.e.text[LineNo].flags |= 1048576 /*0x100000*/;
        break;
      case '\u0011':
        this.e.text[LineNo].flags |= 524288 /*0x080000*/;
        break;
      case '\u0019':
        this.e.text[LineNo].flags |= 131072 /*0x020000*/;
        break;
      case '\u001A':
        this.e.text[LineNo].flags |= 262144 /*0x040000*/;
        break;
    }
    return true;
  }

  internal bool SetSection(
    int NumCols,
    int ColSpace,
    bool StartPage,
    int FirstPageNo,
    bool SetBins,
    PaperSourceKind FirstPageBin,
    PaperSourceKind NextPageBin,
    int sect,
    PaperSize size,
    bool SetOrient,
    bool IsPortrait)
  {
    if (NumCols != 0)
    {
      this.RecreateSections();
      if (sect < 0 || sect >= this.e.TotalSects)
        sect = this.GetSection(this.e.CurLine);
      if (NumCols > 0)
        this.e.TerSect[sect].columns = NumCols;
      this.e.TerSect[sect].ColumnSpace = this.TwipsToInches(ColSpace);
      if (StartPage)
        this.e.TerSect[sect].flags |= 1;
      else
        this.e.TerSect[sect].flags = tc.ResetUintFlag(ref this.e.TerSect[sect].flags, 1);
      if (FirstPageNo > 0)
      {
        this.e.TerSect[sect].flags |= 2;
        this.e.TerSect[sect].FirstPageNo = (short) FirstPageNo;
      }
      else
        this.e.TerSect[sect].flags = tc.ResetUintFlag(ref this.e.TerSect[sect].flags, 2);
      if (SetBins)
      {
        this.e.TerSect[sect].FirstPageBin = FirstPageBin;
        this.e.TerSect[sect].bin = NextPageBin;
      }
      if (size != null)
      {
        this.e.TerSect[sect].PprKind = size.Kind;
        this.e.TerSect[sect].PprWidth = (float) size.Width / 100f;
        this.e.TerSect[sect].PprHeight = (float) size.Height / 100f;
      }
      if (SetOrient)
        this.e.TerSect[sect].IsPortrait = IsPortrait;
    }
    else if (NumCols == 0)
    {
      if (!this.CallDialogBox((Form) new terdlg_section(this.e)))
        return false;
      sect = this.GetSection(this.e.CurLine);
    }
    ++this.e.TerArg.modified;
    this.e.PageModifyCount = 0;
    int prevSect = this.e.TerSect1[sect].PrevSect;
    if (prevSect >= 0 && this.e.TerSect[sect].IsPortrait != this.e.TerSect[prevSect].IsPortrait)
      this.e.TerSect[sect].flags |= 1;
    this.SetSectPageSize();
    this.e.SectModified = true;
    if (this.e.TerArg.PrintView)
    {
      this.PaintTer();
      if (this.e.RepageBeginLine > this.e.TerSect[sect].FirstLine - 1)
        this.e.RepageBeginLine = this.e.TerSect[sect].FirstLine - 1;
      if (this.e.RepageBeginLine < 0)
        this.e.RepageBeginLine = 0;
      this.PostMessage(this.e.hTerWnd, 1034, 0, 0);
    }
    return true;
  }

  internal new bool SetSectPageSize()
  {
    for (int index = 0; index < this.e.TotalSects; ++index)
    {
      if (this.e.TerSect[index].IsPortrait || (this.e.TerOpFlags & 16 /*0x10*/) != 0 && (this.e.TerFlags4 & 32768 /*0x8000*/) == 0)
      {
        this.e.TerSect1[index].PgWidth = this.e.TerSect[index].PprWidth;
        this.e.TerSect1[index].PgHeight = this.e.TerSect[index].PprHeight;
        this.e.TerSect1[index].HiddenX = this.e.PortraitHX;
        this.e.TerSect1[index].HiddenY = this.e.PortraitHY;
      }
      else
      {
        this.e.TerSect1[index].PgWidth = this.e.TerSect[index].PprHeight;
        this.e.TerSect1[index].PgHeight = this.e.TerSect[index].PprWidth;
        this.e.TerSect1[index].HiddenX = this.e.LandscapeHX;
        this.e.TerSect1[index].HiddenY = this.e.LandscapeHY;
      }
      if ((this.e.TerOpFlags & 8) == 0)
      {
        if ((double) this.e.TerSect[index].LeftMargin + (double) this.e.TerSect[index].RightMargin > (double) this.e.TerSect1[index].PgWidth - 0.00050000002374872565)
        {
          this.e.TerSect[index].RightMargin = this.e.TerSect1[index].PgWidth - 0.0005f - this.e.TerSect[index].LeftMargin;
          if ((double) this.e.TerSect[index].RightMargin < 0.0)
          {
            this.e.TerSect[index].LeftMargin += this.e.TerSect[index].RightMargin;
            this.e.TerSect[index].RightMargin = 0.0f;
          }
        }
        if ((double) this.e.TerSect[index].TopMargin + (double) this.e.TerSect[index].BotMargin > (double) this.e.TerSect1[index].PgHeight - 0.00050000002374872565 && (this.e.TerFlags5 & 524288 /*0x080000*/) == 0)
        {
          this.e.TerSect[index].BotMargin = this.e.TerSect1[index].PgHeight - 0.0005f - this.e.TerSect[index].TopMargin;
          if ((double) this.e.TerSect[index].BotMargin < 0.0)
          {
            this.e.TerSect[index].TopMargin += this.e.TerSect[index].BotMargin;
            this.e.TerSect[index].BotMargin = 0.0f;
          }
        }
      }
    }
    return true;
  }

  internal bool TerColBreak(bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.CheckLineLimit(this.e.TotalLines + 1))
    {
      this.ReleaseUndo();
      if (this.e.text[this.e.CurLine].cid > 0 || (this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].flags & 12288 /*0x3000*/) != 0)
        return false;
      this.MoveLineArrays(this.e.CurLine, 1, 'B');
      this.LineAlloc(this.e.CurLine, 0, 1);
      char[] txt = this.e.text[this.e.CurLine].txt;
      ushort[] numArray = this.OpenCfmt(this.e.CurLine);
      txt[0] = '\u0016';
      numArray[0] = (ushort) 0;
      this.CloseCfmt(this.e.CurLine);
      this.SaveUndo(this.e.CurLine, 0, this.e.CurLine, 0, 'I');
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
        this.PaintTer();
    }
    return true;
  }

  internal bool TerCopyHeadersFooters(
    int SrcSect,
    int DestSect,
    bool CopyHdr,
    bool CopyFtr,
    bool CopyFirstHdr,
    bool CopyFirstFtr,
    bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (SrcSect != DestSect)
    {
      if (this.GetFirstSectLine(SrcSect) < 0 || this.GetFirstSectLine(DestSect) < 0 || CopyFtr && !this.CopyHeaderFooter(SrcSect, DestSect, '\u0010') || CopyFirstFtr && !this.CopyHeaderFooter(SrcSect, DestSect, '\u001A') || CopyHdr && !this.CopyHeaderFooter(SrcSect, DestSect, '\u0011') || CopyFirstHdr && !this.CopyHeaderFooter(SrcSect, DestSect, '\u0019'))
        return false;
      this.e.TerRepaginate(repaint);
    }
    return true;
  }

  internal bool TerCreateFirstHdrFtr(bool HdrFtr)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.WordWrap || !this.e.TerArg.PageMode)
      return false;
    if (!this.e.EditPageHdrFtr)
      this.ToggleEditHdrFtr();
    if (!this.e.EditPageHdrFtr)
      return false;
    this.RecreateSections();
    int section = this.GetSection(this.e.CurLine);
    if (HdrFtr && this.e.TerSect1[section].fhdr.FirstLine >= 0 || !HdrFtr && this.e.TerSect1[section].fftr.FirstLine >= 0)
      return false;
    this.ReleaseUndo();
    this.e.TerSect[section].flags |= 4;
    this.CreatePageHdrFtr(HdrFtr ? '\u0019' : '\u001A', section);
    this.RecreateSections();
    if (HdrFtr)
      this.e.CurLine = this.e.TerSect1[section].fhdr.FirstLine + 1;
    else
      this.e.CurLine = this.e.TerSect1[section].fftr.FirstLine + 1;
    if (this.e.CurLine < 0)
      this.e.CurLine = 0;
    this.e.CurCol = 0;
    if (this.e.TotalLines < 5000)
      this.Repaginate(false, false, 0, true);
    else
      this.PaintTer();
    return true;
  }

  internal bool TerDeleteFirstHdrFtr(bool HdrFtr, bool msg)
  {
    return this.TerDeleteHdrFtr(HdrFtr ? '\u0019' : '\u001A', msg);
  }

  internal bool TerDeleteHdrFtr(char HdrFtr, bool msg)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.WordWrap || !this.e.TerArg.PageMode)
      return false;
    if (this.e.EditPageHdrFtr)
      this.ToggleEditHdrFtr();
    if (this.e.EditPageHdrFtr)
      return false;
    this.RecreateSections();
    int section = this.GetSection(this.e.CurLine);
    int firstLine;
    int lastLine;
    switch (HdrFtr)
    {
      case '\u0010':
        firstLine = this.e.TerSect1[section].ftr.FirstLine;
        lastLine = this.e.TerSect1[section].ftr.LastLine;
        break;
      case '\u0011':
        firstLine = this.e.TerSect1[section].hdr.FirstLine;
        lastLine = this.e.TerSect1[section].hdr.LastLine;
        break;
      case '\u0019':
        firstLine = this.e.TerSect1[section].fhdr.FirstLine;
        lastLine = this.e.TerSect1[section].fhdr.LastLine;
        break;
      default:
        firstLine = this.e.TerSect1[section].fftr.FirstLine;
        lastLine = this.e.TerSect1[section].fftr.LastLine;
        break;
    }
    if (firstLine < 0)
      return false;
    if (msg)
    {
      string msg1;
      switch (HdrFtr)
      {
        case '\u0010':
          msg1 = this.e.MsgString[170];
          break;
        case '\u0011':
          msg1 = this.e.MsgString[169];
          break;
        case '\u0019':
          msg1 = this.e.MsgString[158];
          break;
        default:
          msg1 = this.e.MsgString[159];
          break;
      }
      if (DialogResult.No == this.ShowMessage(msg1, this.e.MsgString[154], MessageBoxButtons.YesNo))
        return false;
    }
    this.ReleaseUndo();
    int num = (this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].flags & 12288 /*0x3000*/) != 0 ? 1 : 0;
    int count = lastLine - firstLine + 1;
    this.MoveLineArrays(firstLine, count, 'D');
    if (HdrFtr == '\u0019' || HdrFtr == '\u001A')
      tc.ResetUintFlag(ref this.e.TerSect[section].flags, 4);
    this.RecreateSections();
    if (num != 0)
    {
      while (this.e.CurLine + 1 < this.e.TotalLines && (this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].flags & 12288 /*0x3000*/) != 0)
        ++this.e.CurLine;
    }
    else if (firstLine < this.e.CurLine)
      this.e.CurLine -= count;
    if (this.e.CurLine < 0)
      this.e.CurLine = 0;
    this.e.CurCol = 0;
    if (firstLine < this.e.HilightBegRow)
      this.e.HilightBegRow -= count;
    if (firstLine < this.e.HilightEndRow)
      this.e.HilightEndRow -= count;
    if (this.e.TotalLines < 5000)
      this.Repaginate(false, false, 0, true);
    else
      this.PaintTer();
    return true;
  }

  internal int TerGetHdrFtrPos(int line)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (line < 0)
      line = this.e.CurLine;
    if (line < 0 || line >= this.e.TotalLines || (this.e.PfmtId[this.e.text[line].pfmt].flags & 12288 /*0x3000*/) == 0)
      return 0;
    while (line < this.e.TotalLines && (this.e.text[line].flags & 1966080 /*0x1E0000*/) == 0)
      ++line;
    return line == this.e.TotalLines ? 0 : this.e.text[line].flags & 1966080 /*0x1E0000*/;
  }

  internal int TerGetMarginEx(
    int sect,
    out int pLeft,
    out int pRight,
    out int pTop,
    out int pBottom,
    out int pHeaderY,
    out int pFooterY)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int num1;
    pFooterY = num1 = 0;
    int num2;
    pHeaderY = num2 = num1;
    int num3;
    pBottom = num3 = num2;
    int num4;
    pTop = num4 = num3;
    int num5;
    pRight = num5 = num4;
    pLeft = num5;
    if (sect == -2)
      sect = this.GetSection(this.e.CurLine);
    if (sect < 0 || sect >= this.e.TotalSects)
      return 0;
    pLeft = (int) this.InchesToTwips((double) this.e.TerSect[sect].LeftMargin);
    pRight = (int) this.InchesToTwips((double) this.e.TerSect[sect].RightMargin);
    pTop = (int) this.InchesToTwips((double) this.e.TerSect[sect].TopMargin);
    if ((this.e.TerSect[sect].flags & 8) != 0)
      pTop = -pTop;
    pBottom = (int) this.InchesToTwips((double) this.e.TerSect[sect].BotMargin);
    if ((this.e.TerSect[sect].flags & 16 /*0x10*/) != 0)
      pBottom = -pBottom;
    pHeaderY = (int) this.InchesToTwips((double) this.e.TerSect[sect].HdrMargin);
    pFooterY = (int) this.InchesToTwips((double) this.e.TerSect[sect].FtrMargin);
    return this.e.TotalSects;
  }

  internal int TerGetPageNumFmt(int sect)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (sect == -2)
    {
      this.RecreateSections();
      sect = this.GetSection(this.e.CurLine);
    }
    return sect < 0 || sect >= this.e.TotalSects ? -1 : this.e.TerSect[sect].PageNumFmt;
  }

  internal int TerGetSectAlign(int sect)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (sect == -2)
    {
      this.RecreateSections();
      sect = this.GetSection(this.e.CurLine);
    }
    return this.e.TerSect[sect].flags & 384;
  }

  internal bool TerGetSectBins(
    int sect,
    out PaperSourceKind FirstPageBin,
    out PaperSourceKind NextPageBin)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    PaperSourceKind paperSourceKind;
    NextPageBin = paperSourceKind = PaperSourceKind.AutomaticFeed;
    FirstPageBin = paperSourceKind;
    if (sect == -2)
    {
      this.RecreateSections();
      sect = this.GetSection(this.e.CurLine);
    }
    FirstPageBin = this.e.TerSect[sect].FirstPageBin;
    NextPageBin = this.e.TerSect[sect].bin;
    return true;
  }

  internal bool TerGetSectBorder(
    int sect,
    out int BorderType,
    out int width,
    out int space,
    out Color color)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int num1;
    space = num1 = 0;
    int num2;
    width = num2 = num1;
    BorderType = num2;
    color = tc.CLR_BLACK;
    if (sect == -2)
      sect = this.GetSection(this.e.CurLine);
    if (sect < 0 || sect >= this.e.TotalSects)
      return false;
    BorderType = this.e.TerSect[sect].BorderType;
    if (BorderType == 0)
      BorderType = 8;
    if (BorderType != 8)
    {
      width = this.e.TerSect[sect].BorderWidth[0];
      space = this.e.TerSect[sect].BorderSpace[0];
      color = this.e.TerSect[sect].BorderColor;
    }
    return true;
  }

  internal bool TerGetSectInfo(
    out int NumCols,
    out int ColSpace,
    out bool StartPage,
    out int FirstPageNo)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int num1;
    FirstPageNo = num1 = 0;
    int num2;
    ColSpace = num2 = num1;
    NumCols = num2;
    StartPage = true;
    this.RecreateSections();
    int section = this.GetSection(this.e.CurLine);
    NumCols = this.e.TerSect[section].columns;
    ColSpace = (int) ((double) this.e.TerSect[section].ColumnSpace * 1440.0);
    StartPage = (this.e.TerSect[section].flags & 1) != 0;
    if ((this.e.TerSect[section].flags & 2) != 0)
      FirstPageNo = (int) this.e.TerSect[section].FirstPageNo;
    return true;
  }

  internal int TerGetSectParam(int sect, int type)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (sect == -2)
    {
      this.RecreateSections();
      sect = this.GetSection(this.e.CurLine);
    }
    if (type == 1)
      return this.e.TerSect[sect].flags;
    return type == 2 ? this.e.TerSect[sect].LineStep : 9999;
  }

  internal int TerGetSeqSect(int sect)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (sect >= this.e.TotalSects)
      return -1;
    this.RecreateSections();
    int seqSect = 0;
    for (; this.e.TerSect1[sect].PrevSect >= 0; sect = this.e.TerSect1[sect].PrevSect)
      ++seqSect;
    return seqSect;
  }

  internal int TerHdrFtrExists(int SectId)
  {
    int num1 = 0;
    int num2 = 0;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int index;
    if (SectId < 0)
    {
      index = this.e.CurLine;
    }
    else
    {
      for (index = 0; index < this.e.TotalLines && num1 != SectId; ++index)
      {
        if ((this.e.text[index].flags & 2048 /*0x0800*/) != 0)
          ++num1;
      }
      if (index == this.e.TotalLines)
        return num2;
    }
    while (index < this.e.TotalLines && (this.e.PfmtId[this.e.text[index].pfmt].flags & 12288 /*0x3000*/) != 0)
      ++index;
    if (index != this.e.TotalLines)
    {
      for (; index >= 0; --index)
      {
        num2 |= this.e.text[index].flags & 1966080 /*0x1E0000*/;
        if (SectId != -1 && (this.e.text[index].flags & 2048 /*0x0800*/) != 0)
          break;
      }
    }
    return num2;
  }

  internal bool TerInsertFootnote(string FnMarker, string FnText, int style, bool repaint)
  {
    return this.TerInsertFootnote2(FnMarker, FnText, style, true, repaint);
  }

  internal bool TerInsertFootnote2(
    string FnMarker,
    string FnText,
    int style,
    bool IsFootnote,
    bool repaint)
  {
    int num1 = 0;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.e.InFootnote)
      return false;
    if (this.False(FnMarker) || this.False(FnText))
    {
      this.e.DlgBool1 = IsFootnote;
      if (!this.CallDialogBox((Form) new terdlg_footnote(this.e)))
        return false;
      string tempString = this.e.TempString;
      string str = tempString + " " + this.e.TempString1;
      FnMarker = tempString;
      FnText = str;
      style = !this.True(this.e.DlgInt1) ? 512 /*0x0200*/ : 528;
    }
    else
    {
      FnText = FnMarker + FnText;
      style = tc.ResetUintFlag(ref style, 39936);
    }
    int length1 = FnMarker.Length;
    int length2 = FnText.Length;
    if (this.e.HilightType != 0)
      this.e.HilightType = 0;
    this.e.InputFontId = this.GetEffectiveCfmt();
    int num2 = 3072 /*0x0C00*/ | style;
    if (!IsFootnote)
      num2 |= 32768 /*0x8000*/;
    if ((this.e.TerFont[this.e.InputFontId].style & (num2 | style)) != 0)
      this.e.SetTerCharStyle(num2 | style, false, false);
    if (this.e.DefLang != 1033)
      this.e.InputFontId = this.SetCurLangFont2(this.e.InputFontId, this.e.DefInpLang);
    int inputFontId1 = this.e.InputFontId;
    this.e.SetTerCharStyle(1024 /*0x0400*/ | style, true, false);
    int inputFontId2 = this.e.InputFontId;
    this.e.InputFontId = inputFontId1;
    int FmtType = IsFootnote ? 2048 /*0x0800*/ : 34816;
    this.e.SetTerCharStyle(FmtType, true, false);
    int inputFontId3 = this.e.InputFontId;
    if (length2 > length1)
    {
      this.e.InputFontId = inputFontId1;
      this.e.SetTerCharStyle(FmtType | style, true, false);
      num1 = this.e.InputFontId;
    }
    this.e.InputFontId = -1;
    this.MoveLineData(this.e.CurLine, this.e.CurCol, length1 + length2, 'B');
    char[] txt = this.e.text[this.e.CurLine].txt;
    ushort[] numArray = this.OpenCfmt(this.e.CurLine);
    int num3 = 0;
    txt[this.e.CurCol] = FnMarker[0];
    numArray[this.e.CurCol] = (ushort) inputFontId2;
    int num4 = num3 + 1;
    for (int index = 1; index < length1; ++index)
    {
      txt[this.e.CurCol + num4] = FnMarker[index];
      numArray[this.e.CurCol + num4] = (ushort) inputFontId2;
      ++num4;
    }
    int num5 = length1;
    for (int index = 0; index < length1; ++index)
    {
      txt[this.e.CurCol + num4] = FnText[index];
      numArray[this.e.CurCol + num4] = (ushort) num1;
      ++num4;
    }
    for (int index = 0; index < length2 - num5; ++index)
    {
      txt[this.e.CurCol + num4] = FnText[num5 + index];
      numArray[this.e.CurCol + num4] = (ushort) inputFontId3;
      if (this.e.DefLang != 1033)
      {
        InputLanguage lng = InputLanguage.FromCulture(new CultureInfo(1033));
        if (this.IsEnglishChar(FnText.ToCharArray(), num5 + index, length2))
          numArray[this.e.CurCol + num4] = (ushort) this.SetCurLangFont2((int) numArray[this.e.CurCol + num4], lng);
      }
      ++num4;
    }
    this.CloseCfmt(this.e.CurLine);
    this.e.CurCol += length1 + length2;
    this.ReleaseUndo();
    if (!IsFootnote)
    {
      this.e.text[this.e.CurLine].flags2 |= 2;
      this.CreateEndnote();
      this.RequestPagination(true);
    }
    if (repaint)
      this.PaintTer();
    else
      this.WordWrap(this.e.CurLine, 30);
    return true;
  }

  internal bool TerInsertToc(bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.PageMode || !this.PrepForObject())
      return false;
    this.e.HilightType = 0;
    int newFieldId = (int) this.GetNewFieldId((ushort) this.GetCurCfmt(this.e.CurLine, this.e.CurCol), 9, (string) null, this.e.CurLine, this.e.CurCol);
    this.e.InputFontId = -1;
    if (newFieldId < 0 || this.e.TerFont[newFieldId].FieldId != 9)
      return false;
    string str = "Table of Contents";
    this.AddChar(ref str, this.e.ParaChar);
    int length = str.Length;
    this.MoveLineArrays(this.e.CurLine, 1, 'B');
    this.LineAlloc(this.e.CurLine, 0, length);
    char[] txt = this.e.text[this.e.CurLine].txt;
    ushort[] numArray = this.OpenCfmt(this.e.CurLine);
    for (int index = 0; index < length; ++index)
    {
      txt[index] = str[index];
      numArray[index] = (ushort) newFieldId;
    }
    this.e.text[this.e.CurLine].flags |= 16777216 /*0x01000000*/;
    this.CloseCfmt(this.e.CurLine);
    this.ReleaseUndo();
    if (repaint)
      this.e.TerRepaginate(true);
    return true;
  }

  internal bool TerPosHdrFtr(int sect, bool header, int pos, bool repaint)
  {
    return this.TerPosHdrFtrEx(sect, header ? '\u0011' : '\u0010', pos, repaint);
  }

  internal bool TerPosHdrFtrEx(int sect, char HdrFtr, int pos, bool repaint)
  {
    bool flag1 = false;
    bool flag2 = HdrFtr == '\u0019' || HdrFtr == '\u001A';
    bool flag3 = false;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.PageMode || pos != 0 && pos != 1)
      return false;
    bool paintEnabled1 = this.e.PaintEnabled;
    this.e.PaintEnabled = repaint;
    if (!this.e.EditPageHdrFtr)
      this.ToggleEditHdrFtr();
    this.e.PaintEnabled = paintEnabled1;
    int lin;
    while (true)
    {
      int num = 0;
      lin = 0;
      while (num < sect)
      {
        for (; lin < this.e.TotalLines; ++lin)
        {
          if (this.True(this.e.text[lin].tabw) && (this.e.text[lin].tabw.type & 2) != 0)
          {
            ++num;
            ++lin;
            break;
          }
        }
        if (lin == this.e.TotalLines)
          break;
      }
      if (lin != this.e.TotalLines)
      {
        if (flag2)
        {
          this.RecreateSections();
          int section = this.GetSection(lin);
          if (HdrFtr == '\u0019' && this.e.TerSect1[section].fhdr.FirstLine < 0 || HdrFtr == '\u001A' && this.e.TerSect1[section].fftr.FirstLine < 0)
          {
            if (!flag3)
            {
              bool paintEnabled2 = this.e.PaintEnabled;
              this.e.PaintEnabled = repaint;
              this.e.CurLine = lin;
              this.e.CurCol = 0;
              this.TerCreateFirstHdrFtr(HdrFtr == '\u0019');
              this.e.PaintEnabled = paintEnabled2;
              flag3 = true;
            }
            else
              break;
          }
          else
            goto label_29;
        }
        else
          goto label_29;
      }
      else
        goto label_30;
    }
    return false;
    while (lin < this.e.TotalLines && (!this.True(this.e.text[lin].tabw) || (this.e.text[lin].tabw.type & 2) == 0))
    {
      if (!flag1 && ((this.e.text[lin].flags & 524288 /*0x080000*/) == 0 ? ((this.e.text[lin].flags & 1048576 /*0x100000*/) == 0 ? ((this.e.text[lin].flags & 131072 /*0x020000*/) == 0 ? ((this.e.text[lin].flags & 262144 /*0x040000*/) == 0 ? (int) char.MinValue : (int) '\u001A') : (int) '\u0019') : (int) '\u0010') : (int) '\u0011') == (int) HdrFtr)
      {
        flag1 = true;
        if (pos == 0)
        {
          this.e.CursDirection = 1;
          this.e.SetTerCursorPos(lin + 1, 0, repaint);
          return true;
        }
        ++lin;
        continue;
      }
      if (flag1 && (this.e.text[lin].flags & 1966080 /*0x1E0000*/) != 0)
      {
        int NewCol = this.e.text[lin - 1].len - 1;
        if (NewCol < 0)
          NewCol = 0;
        this.e.CursDirection = 2;
        this.e.SetTerCursorPos(lin - 1, NewCol, repaint);
        return true;
      }
      ++lin;
      continue;
label_29:;
    }
label_30:
    return false;
  }

  internal bool TerSectBreak(bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.CheckLineLimit(this.e.TotalLines + 1))
    {
      this.ReleaseUndo();
      if (this.e.text[this.e.CurLine].cid > 0 || (this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].flags & 12288 /*0x3000*/) != 0 || this.e.EditPageHdrFtr || !this.CanInsertBreakChar(this.e.CurLine, this.e.CurCol))
        return false;
      if (this.e.CurCol > 0)
        this.TerSplitLine(0, false, false);
      this.MoveLineArrays(this.e.CurLine, 1, 'B');
      int section = this.GetSection(this.e.CurLine);
      int AuxInt = 0;
      while (AuxInt < this.e.TotalSects && this.e.TerSect[AuxInt].InUse)
        ++AuxInt;
      if (AuxInt == this.e.TotalSects)
      {
        if (this.e.TotalSects >= this.e.MaxSects)
        {
          this.ExpandSectArray(-1);
          if (this.e.TotalSects >= this.e.MaxSects)
            return this.PrintError(128 /*0x80*/, nameof (TerSectBreak));
        }
        ++this.e.TotalSects;
      }
      this.e.TerSect[AuxInt] = this.e.TerSect[section];
      this.e.TerSect1[AuxInt] = this.e.TerSect1[section];
      this.e.TerSect[AuxInt].LastLine = this.e.CurLine;
      this.e.TerSect[section].FirstLine = this.e.CurLine + 1;
      this.e.TerSect1[section].hdr.FirstLine = -1;
      this.e.TerSect1[section].ftr.FirstLine = -1;
      this.e.TerSect1[section].PrevSect = AuxInt;
      int prevSect = this.e.TerSect1[AuxInt].PrevSect;
      if (prevSect >= 0)
        this.e.TerSect1[prevSect].NextSect = AuxInt;
      this.LineAlloc(this.e.CurLine, 0, 1);
      char[] txt = this.e.text[this.e.CurLine].txt;
      ushort[] numArray = this.OpenCfmt(this.e.CurLine);
      txt[0] = '\u0014';
      numArray[0] = (ushort) 0;
      this.SetTag(this.e.CurLine, 0, 4, "Section" + AuxInt.ToString(), (string) null, AuxInt);
      if (this.e.CurLine > 0 && (this.e.text[this.e.CurLine - 1].flags & 1) == 0)
        this.e.text[this.e.CurLine].pfmt = this.e.text[this.e.CurLine - 1].pfmt;
      else
        this.e.text[this.e.CurLine].pfmt = 0;
      this.e.text[this.e.CurLine].fid = 0;
      if (this.AllocTabw(this.e.CurLine))
      {
        this.e.text[this.e.CurLine].tabw.type = 2;
        this.e.text[this.e.CurLine].tabw.section = AuxInt;
      }
      this.CloseCfmt(this.e.CurLine);
      ++this.e.CurLine;
      this.e.CurCol = 0;
      if (this.e.CurLine - this.e.BeginLine >= this.e.WinHeight || this.e.CurLine - this.e.BeginLine < 0)
      {
        this.e.BeginLine = this.e.CurLine - this.e.WinHeight / 2;
        if (this.e.BeginLine < 0)
          this.e.BeginLine = 0;
      }
      this.e.CurRow = this.e.CurLine - this.e.BeginLine;
      this.e.HilightType = 0;
      ++this.e.TerArg.modified;
      this.e.SectModified = true;
      if (this.e.CurLine < this.e.RepageBeginLine)
        this.e.RepageBeginLine = this.e.CurLine;
      this.ReleaseUndo();
      if (repaint)
        this.PaintTer();
    }
    return true;
  }

  internal bool TerSetMargin(int left, int right, int top, int bottom, bool refresh)
  {
    return this.TerSetMarginEx(-2, left, right, top, bottom, -1, -1, refresh);
  }

  internal bool TerSetMarginEx(
    int sect,
    int left,
    int right,
    int top,
    int bottom,
    int HeaderY,
    int FooterY,
    bool refresh)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (sect == -2)
    {
      this.RecreateSections();
      sect = this.GetSection(this.e.CurLine);
    }
    if (sect != -1 && (sect < 0 || sect >= this.e.TotalSects))
      return false;
    for (int index = 0; index < this.e.TotalSects; ++index)
    {
      if (sect == -1 || sect == index)
      {
        if (left >= 0)
          this.e.TerSect[index].LeftMargin = this.TwipsToInches(left);
        if (right >= 0)
          this.e.TerSect[index].RightMargin = this.TwipsToInches(right);
        if (top != -1)
        {
          if (top >= 0)
          {
            this.e.TerSect[index].TopMargin = this.TwipsToInches(top);
            tc.ResetUintFlag(ref this.e.TerSect[index].flags, 8);
          }
          else
          {
            this.e.TerSect[index].TopMargin = this.TwipsToInches(-top);
            this.e.TerSect[index].flags |= 8;
          }
        }
        if (bottom != -1)
        {
          if (bottom >= 0)
          {
            this.e.TerSect[index].BotMargin = this.TwipsToInches(bottom);
            tc.ResetUintFlag(ref this.e.TerSect[index].flags, 16 /*0x10*/);
          }
          else
          {
            this.e.TerSect[index].BotMargin = this.TwipsToInches(-bottom);
            this.e.TerSect[index].flags |= 16 /*0x10*/;
          }
        }
        if (HeaderY >= 0)
          this.e.TerSect[index].HdrMargin = this.TwipsToInches(HeaderY);
        if (FooterY >= 0)
          this.e.TerSect[index].FtrMargin = this.TwipsToInches(FooterY);
      }
    }
    ++this.e.TerArg.modified;
    this.RequestPagination(true);
    if (refresh)
      this.PaintTer();
    return true;
  }

  internal bool TerSetPageNumFmt(int sect, int fmt)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (sect == -2)
    {
      this.RecreateSections();
      sect = this.GetSection(this.e.CurLine);
    }
    if (sect != -1 && (sect < 0 || sect >= this.e.TotalSects))
      return false;
    for (int index = 0; index < this.e.TotalSects; ++index)
    {
      if (sect == -1 || sect == index)
        this.e.TerSect[index].PageNumFmt = fmt;
    }
    return true;
  }

  internal bool TerSetPaper(PaperSize size, bool IsPortrait, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int x1 = this.MulDiv(size.Width, 1440, 100);
    int x2 = this.MulDiv(size.Height, 1440, 100);
    this.e.PageWidth = this.TwipsToInches(x1);
    this.e.PageHeight = this.TwipsToInches(x2);
    if (!this.e.InRtfRead)
    {
      this.e.TerSect[this.GetSection(this.e.CurLine)].IsPortrait = this.e.IsPortrait = IsPortrait;
      this.e.PprKind = size.Kind;
    }
    if (this.ForceSectOrient())
      this.ApplyPaperOrient(this.e.IsPortrait);
    this.ApplyPaperSize(size);
    this.SetSectPageSize();
    if (repaint)
    {
      if (this.e.TerArg.PrintView)
      {
        this.Repaginate(false, false, 0, true);
        this.e.PageModifyCount = this.e.TerArg.modified++;
        this.e.RepageBeginLine = 0;
      }
      this.PaintTer();
    }
    return true;
  }

  internal bool TerSetSect(int NumCols, int ColSpace, bool StartPage)
  {
    return this.TerSetSectEx(NumCols, ColSpace, StartPage, 0);
  }

  internal bool TerSetSect2(
    int NumCols,
    int ColSpace,
    bool StartPage,
    int FirstPageNo,
    PaperSourceKind FirstPageBin,
    PaperSourceKind NextPageBin)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    return this.SetSection(NumCols, ColSpace, StartPage, FirstPageNo, true, FirstPageBin, NextPageBin, -1, (PaperSize) null, false, true);
  }

  internal bool TerSetSect3(
    int NumCols,
    int ColSpace,
    bool StartPage,
    int FirstPageNo,
    bool SetBins,
    PaperSourceKind FirstPageBin,
    PaperSourceKind NextPageBin,
    int sect,
    PaperSize size)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    return this.SetSection(NumCols, ColSpace, StartPage, FirstPageNo, true, FirstPageBin, NextPageBin, sect, size, false, true);
  }

  internal bool TerSetSect3(
    int NumCols,
    int ColSpace,
    bool StartPage,
    int FirstPageNo,
    bool SetBins,
    PaperSourceKind FirstPageBin,
    PaperSourceKind NextPageBin,
    int sect,
    PaperSize size,
    bool IsPortrait)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    return this.SetSection(NumCols, ColSpace, StartPage, FirstPageNo, SetBins, FirstPageBin, NextPageBin, sect, size, true, IsPortrait);
  }

  internal bool TerSetSectAlign(int sect, int align, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (align != 0 && align != 128 /*0x80*/ && align != 256 /*0x0100*/)
      return false;
    if (sect == -2)
    {
      this.RecreateSections();
      sect = this.GetSection(this.e.CurLine);
    }
    if (sect != -1 && (sect < 0 || sect >= this.e.TotalSects))
      return false;
    for (int index = 0; index < this.e.TotalSects; ++index)
    {
      if (sect == -1 || sect == index)
      {
        this.e.TerSect[index].flags = tc.ResetFlag(this.e.TerSect[index].flags, 384);
        this.e.TerSect[index].flags |= align;
      }
    }
    ++this.e.TerArg.modified;
    this.RequestPagination(true);
    if (repaint)
      this.PaintTer();
    return true;
  }

  internal bool TerSetSectBorder(
    int sect,
    int BorderType,
    int width,
    int space,
    Color color,
    bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (BorderType < 0 || BorderType > 8)
      return false;
    switch (sect)
    {
      case -2:
        sect = this.GetSection(this.e.CurLine);
        break;
      case -1:
        sect = -1;
        break;
      default:
        if (sect < 0 || sect >= this.e.TotalSects)
          return false;
        break;
    }
    for (int index1 = 0; index1 < this.e.TotalSects; ++index1)
    {
      if (sect < 0 || index1 == sect)
      {
        this.e.TerSect[index1].BorderType = BorderType;
        if (BorderType == 8)
        {
          this.e.TerSect[index1].border = 0;
        }
        else
        {
          this.e.TerSect[index1].border = 15;
          this.e.TerSect[index1].BorderWidth = new int[4];
          this.e.TerSect[index1].BorderSpace = new int[4];
          for (int index2 = 0; index2 < 4; ++index2)
          {
            this.e.TerSect[index1].BorderWidth[index2] = width;
            this.e.TerSect[index1].BorderSpace[index2] = space;
          }
          this.e.TerSect[index1].BorderColor = color;
          this.e.TerSect[index1].BorderOpts = 32 /*0x20*/;
        }
        if (sect >= 0)
          break;
      }
    }
    ++this.e.TerArg.modified;
    if (repaint)
      this.PaintTer();
    return true;
  }

  internal bool TerSetSectEx(int NumCols, int ColSpace, bool StartPage, int FirstPageNo)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    return this.SetSection(NumCols, ColSpace, StartPage, FirstPageNo, false, PaperSourceKind.AutomaticFeed, PaperSourceKind.AutomaticFeed, -1, (PaperSize) null, false, true);
  }

  internal bool TerSetSectLineNbr(int sect, bool set, bool repaint)
  {
    return this.TerSetSectLineNbr2(sect, set, 0, repaint);
  }

  internal bool TerSetSectLineNbr2(int sect, bool set, int step, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    switch (sect)
    {
      case -2:
        sect = this.GetSection(this.e.CurLine);
        break;
      case -1:
        sect = -1;
        break;
      default:
        if (sect < 0 || sect >= this.e.TotalSects)
          return false;
        break;
    }
    for (int index = 0; index < this.e.TotalSects; ++index)
    {
      if (sect < 0 || index == sect)
      {
        if (set)
        {
          this.e.TerSect[index].flags |= 512 /*0x0200*/;
          this.e.TerSect[index].LineStep = step;
        }
        else
          tc.ResetUintFlag(ref this.e.TerSect[index].flags, 512 /*0x0200*/);
        if (sect >= 0)
          break;
      }
    }
    ++this.e.TerArg.modified;
    if (repaint)
      this.PaintTer();
    return true;
  }

  internal bool TerSetSectOrient(bool IsPortrait, bool repaint)
  {
    this.e.TerSect[this.GetSection(this.e.CurLine)].IsPortrait = IsPortrait;
    if (this.ForceSectOrient())
    {
      for (int index = 0; index < this.e.TotalSects; ++index)
        this.e.TerSect[index].IsPortrait = IsPortrait;
    }
    this.SetSectPageSize();
    if (repaint)
    {
      if (this.e.TerArg.PrintView)
      {
        this.Repaginate(false, false, 0, true);
        this.e.PageModifyCount = this.e.TerArg.modified++;
        this.e.RepageBeginLine = 0;
      }
      this.PaintTer();
    }
    return true;
  }

  /// <summary>Установить размер страницы</summary>
  /// <param name="sect">Раздел (-1 - все разделы, -2 - текущий раздел)</param>
  /// <param name="pageWidth">Ширина страницы в дюймах</param>
  /// <param name="pageHeight">Высота страницы в дюймах</param>
  /// <param name="repaint">Перерисовать</param>
  /// <returns>false, если возникли ошибки</returns>
  internal bool TerSetSectPageSize(int sect, float pageWidth, float pageHeight, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    switch (sect)
    {
      case -2:
        sect = this.GetSection(this.e.CurLine);
        break;
      case -1:
        sect = -1;
        break;
      default:
        if (sect < 0 || sect >= this.e.TotalSects)
          return false;
        break;
    }
    for (int index = 0; index < this.e.TotalSects; ++index)
    {
      if (sect < 0 || index == sect)
      {
        this.e.TerSect[index].PprKind = PaperKind.Custom;
        this.e.TerSect[index].PprWidth = pageWidth;
        this.e.TerSect[index].PprHeight = pageHeight;
        if (sect >= 0)
          break;
      }
    }
    this.SetSectPageSize();
    if (repaint)
    {
      if (this.e.TerArg.PrintView)
      {
        this.Repaginate(false, false, 0, true);
        this.e.PageModifyCount = this.e.TerArg.modified++;
        this.e.RepageBeginLine = 0;
      }
      this.PaintTer();
    }
    return true;
  }

  internal bool TerSetSectPageSize(
    int sect,
    PaperKind size,
    int ParamWidth,
    int ParamHeight,
    bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    switch (sect)
    {
      case -2:
        sect = this.GetSection(this.e.CurLine);
        break;
      case -1:
        sect = -1;
        break;
      default:
        if (sect < 0 || sect >= this.e.TotalSects)
          return false;
        break;
    }
    int index1 = 0;
    while (index1 < 11 && tc.DefPaperKind[index1] != size)
      ++index1;
    float num1;
    float num2;
    if (index1 < 11)
    {
      num1 = tc.DefPaperWidth[index1];
      num2 = tc.DefPaperHeight[index1];
    }
    else
    {
      num1 = (float) ParamWidth / 1440f;
      num2 = (float) ParamHeight / 1440f;
    }
    for (int index2 = 0; index2 < this.e.TotalSects; ++index2)
    {
      if (sect < 0 || index2 == sect)
      {
        this.e.TerSect[index2].PprKind = size;
        this.e.TerSect[index2].PprWidth = num1;
        this.e.TerSect[index2].PprHeight = num2;
        if (sect >= 0)
          break;
      }
    }
    this.SetSectPageSize();
    if (repaint)
    {
      if (this.e.TerArg.PrintView)
      {
        this.Repaginate(false, false, 0, true);
        this.e.PageModifyCount = this.e.TerArg.modified++;
        this.e.RepageBeginLine = 0;
      }
      this.PaintTer();
    }
    return true;
  }

  internal bool TerSetSectTextFlow(int sect, int flow, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (flow != 0 && flow != 2 && flow != 1)
      return false;
    switch (sect)
    {
      case -2:
        sect = this.GetSection(this.e.CurLine);
        break;
      case -1:
        sect = -1;
        break;
      default:
        if (sect < 0 || sect >= this.e.TotalSects)
          return false;
        break;
    }
    for (int index = 0; index < this.e.TotalSects; ++index)
    {
      if (sect < 0 || index == sect)
      {
        this.e.TerSect[index].flow = flow;
        if (sect >= 0)
          break;
      }
    }
    ++this.e.TerArg.modified;
    if (repaint)
      this.e.TerRepaginate(true);
    else
      this.RequestPagination(true);
    return true;
  }

  internal bool TerSetSectVertAlign(int sect, int valign, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (valign != 0 && valign != 128 /*0x80*/ && valign != 256 /*0x0100*/)
      return false;
    switch (sect)
    {
      case -2:
        sect = this.GetSection(this.e.CurLine);
        break;
      case -1:
        sect = -1;
        break;
      default:
        if (sect < 0 || sect >= this.e.TotalSects)
          return false;
        break;
    }
    for (int index = 0; index < this.e.TotalSects; ++index)
    {
      if (sect < 0 || index == sect)
      {
        tc.ResetUintFlag(ref this.e.TerSect[index].flags, 384);
        switch (valign)
        {
          case 128 /*0x80*/:
            this.e.TerSect[index].flags |= 128 /*0x80*/;
            break;
          case 256 /*0x0100*/:
            this.e.TerSect[index].flags |= 256 /*0x0100*/;
            break;
        }
        if (sect >= 0)
          break;
      }
    }
    ++this.e.TerArg.modified;
    if (repaint)
      this.e.TerRepaginate(true);
    else
      this.RequestPagination(true);
    return true;
  }

  internal new bool ToggleEditHdrFtr()
  {
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    if (!this.e.TerArg.PageMode)
      return false;
    this.RecreateSections();
    if (this.e.EditPageHdrFtr)
    {
      int index1;
      int index2 = index1 = -1;
      int num1 = 0;
      for (int index3 = 0; index3 <= this.e.TotalLines; ++index3)
      {
        if (index3 < this.e.TotalLines && (this.e.text[index3].flags & 393216 /*0x060000*/) != 0)
          flag3 = !flag3;
        int x = index3 >= this.e.TotalLines || flag3 ? 0 : this.e.PfmtId[this.e.text[index3].pfmt].flags & 12288 /*0x3000*/;
        if ((index3 >= this.e.TotalLines || x != num1) && index2 >= 0)
        {
          int section = this.GetSection(index2);
          int count = index1 - index2 + 1;
          bool flag4 = true;
          if (this.e.TerSect[section].FirstLine == 0)
            flag4 = false;
          if (count != 3)
            flag4 = false;
          if (this.e.text[index2 + 1].len != 1)
            flag4 = false;
          if ((this.e.text[index2].flags & 1572864 /*0x180000*/) == 0)
            flag4 = false;
          if ((this.e.text[index1].flags & 1572864 /*0x180000*/) == 0)
            flag4 = false;
          if (flag4)
          {
            this.MoveLineArrays(index2, count, 'D');
            if (index1 < this.e.HilightBegRow)
              this.e.HilightBegRow -= count;
            if (index1 < this.e.HilightEndRow)
              this.e.HilightEndRow -= count;
            int num2 = 0;
            for (int index4 = 0; index4 < count; ++index4)
            {
              if (index2 + index4 < this.e.CurLine)
                ++num2;
            }
            this.e.CurLine -= num2;
            if (this.e.CurLine < 0)
              this.e.CurLine = 0;
            flag1 = true;
            index3 -= count;
          }
          index2 = -1;
        }
        if (index3 < this.e.TotalLines)
        {
          if (this.True(x) && index2 == -1)
            index2 = index3;
          index1 = index3;
          num1 = x;
        }
        else
          break;
      }
      if (flag1)
      {
        this.RecreateSections();
        if (this.e.CurLine >= this.e.TotalLines)
          this.e.CurLine = this.e.TotalLines - 1;
        if (this.e.TotalLines < 5000)
          this.Repaginate(false, false, 0, true);
      }
      this.e.EditPageHdrFtr = false;
      this.PaintTer();
    }
    else
    {
      for (int sect = 0; sect < this.e.TotalSects; ++sect)
      {
        if (this.e.TerSect[sect].InUse)
        {
          bool flag5;
          bool flag6 = flag5 = false;
          for (int firstLine = this.e.TerSect[sect].FirstLine; firstLine <= this.e.TerSect[sect].LastLine; ++firstLine)
          {
            if ((this.e.text[firstLine].flags & 524288 /*0x080000*/) != 0)
              flag6 = true;
            if ((this.e.text[firstLine].flags & 1048576 /*0x100000*/) != 0)
              flag5 = true;
            if (flag6 & flag5)
              break;
          }
          if (!flag5 && !this.CreatePageHdrFtr('\u0010', sect) || !flag6 && !this.CreatePageHdrFtr('\u0011', sect))
            return false;
          if (!flag6 || !flag5)
            flag2 = true;
        }
      }
      this.e.ViewPageHdrFtr = this.e.EditPageHdrFtr = true;
      if (flag2)
      {
        this.RecreateSections();
        if (this.e.TotalLines < 5000)
          this.Repaginate(false, false, 0, true);
      }
      if (!this.e.TerArg.PageMode)
        this.TogglePageMode();
      else
        this.PaintTer();
    }
    return true;
  }

  internal new bool ToggleFootnoteEdit(bool footnote)
  {
    if (footnote)
      this.e.EditFootnoteText = !this.e.EditFootnoteText;
    else
      this.e.EditEndnoteText = !this.e.EditEndnoteText;
    this.RecreateFonts(this.e.TerGr);
    this.e.PageModifyCount = -1;
    this.e.RepageBeginLine = 0;
    this.PaintTer();
    return true;
  }

  internal new bool ToggleViewHdrFtr()
  {
    bool flag1 = false;
    bool flag2 = false;
    if (!this.e.TerArg.PrintView)
      return false;
    if (this.e.ViewPageHdrFtr)
    {
      if (this.e.EditPageHdrFtr)
        this.ToggleEditHdrFtr();
      this.e.ViewPageHdrFtr = false;
    }
    else
    {
      this.RecreateSections();
      int section = this.GetSection(0);
      if (this.e.TerSect1[section].hdr.FirstLine >= 0)
        flag1 = true;
      if (this.e.TerSect1[section].ftr.FirstLine >= 0)
        flag2 = true;
      if (!flag2 && !this.CreatePageHdrFtr('\u0010', section) || !flag1 && !this.CreatePageHdrFtr('\u0011', section))
        return false;
      this.e.ViewPageHdrFtr = true;
      if (!flag1 || !flag2)
      {
        this.RecreateSections();
        if (this.e.TotalLines < 5000)
          this.Repaginate(false, false, 0, true);
      }
    }
    if (!this.e.TerArg.PageMode)
      this.TogglePageMode();
    else
      this.PaintTer();
    return true;
  }

  internal new bool UpdateToc()
  {
    int modified = this.e.TerArg.modified;
    int index1 = 0;
    if (tc.DebugMode)
      this.misc.dm(nameof (UpdateToc));
    this.e.DocHasHeadings = false;
    for (int line = 0; line < this.e.TotalLines; ++line)
    {
      if ((this.e.text[line].flags & 16777216 /*0x01000000*/) == 0 && (this.e.text[line].flags & 1) != 0)
      {
        int styId = this.e.PfmtId[this.e.text[line].pfmt].StyId;
        if (styId != 0)
        {
          int length = "heading ".Length;
          string str1 = this.e.StyleId[styId].name;
          if (length < str1.Length)
            str1 = str1.Substring(0, length);
          if (this.strcmpi(str1, "heading ") == 0 && this.e.StyleId[styId].name.Length <= length + 1)
          {
            switch (this.e.StyleId[styId].name[length])
            {
              case '1':
              case '2':
              case '3':
              case '4':
              case '5':
              case '6':
              case '7':
              case '8':
              case '9':
                this.e.DocHasHeadings = true;
                if (!this.e.DocHasToc || !this.e.TocShowPageNo)
                  return true;
                int tag = this.GetTag(line, this.e.text[line].len - 1, 3, out tc.SkipStr, out tc.SkipStr, out tc.SkipInt, out tc.SkipObject);
                if (tag > 0 && tag < this.e.TotalCharTags)
                {
                  this.e.CharTag[tag].AuxInt = this.e.text[line].page;
                  continue;
                }
                continue;
              default:
                continue;
            }
          }
        }
      }
    }
    int row;
    this.AbsToRowCol(this.e.FirstTocPos, out row, out int _);
    while (true)
    {
      int index2 = row;
      while (index2 < this.e.TotalLines && (this.e.text[index2].flags & 16777216 /*0x01000000*/) == 0)
        ++index2;
      if (index2 != this.e.TotalLines)
      {
        int num1 = index2;
        while (index2 < this.e.TotalLines && (this.e.text[index2].flags & 16777216 /*0x01000000*/) != 0)
          ++index2;
        int num2 = index2 - 1;
        int pLine;
        for (pLine = num1; pLine <= num2; ++pLine)
        {
          if ((this.e.text[pLine].flags & 1) != 0 && !this.False(this.e.text[pLine].tag))
          {
            int len1 = this.e.text[pLine].len;
            int col = 0;
            while (col < len1 && (index1 = this.GetTag(pLine, col, 2, out tc.SkipStr, out tc.SkipStr, out tc.SkipInt, out tc.SkipObject)) == 0)
              ++col;
            if (col != len1)
            {
              int pCol = col;
              int len2 = len1 - pCol - 1;
              int auxInt = this.e.CharTag[index1].AuxInt;
              if (auxInt > 0 && auxInt < this.e.TotalCharTags)
              {
                string txt = this.e.PageInfo[this.e.CharTag[auxInt].AuxInt].DispNbr.ToString();
                this.ReplaceTextInPlace(ref pLine, ref pCol, len2, txt);
              }
            }
          }
        }
        if (this.e.MultipleToc)
          row = pLine;
        else
          break;
      }
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
}
