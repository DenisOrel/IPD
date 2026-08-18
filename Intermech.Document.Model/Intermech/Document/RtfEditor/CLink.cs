// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.CLink
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class CLink : COp
{
  internal CLink(ImRtfEditor ImRtfEditor)
    : base(ImRtfEditor)
  {
    this.e = ImRtfEditor;
  }

  internal new bool CheckImageMapHit(int pict)
  {
    this.e.CurMapPict = this.e.CurMapId = this.e.CurMapRect = 0;
    if (this.e.TotalImageMaps != 1 && !this.False(this.e.TerFont[pict].InUse) && (this.e.TerFont[pict].style & 128 /*0x80*/) != 0 && this.e.TerFont[pict].MapId != 0)
    {
      int mouseX = this.e.MouseX;
      int mouseY = this.e.MouseY;
      int num1 = mouseX - this.e.TerWinRect.left;
      int num2 = mouseY - this.e.TerWinRect.top;
      int num3 = num1 - this.e.TerFont[pict].PictX;
      int num4 = num2 - this.e.TerFont[pict].PictY;
      int mapId = this.e.TerFont[pict].MapId;
      if (mapId <= 0 || mapId >= this.e.TotalImageMaps)
        return false;
      tc.StrImageMap image = this.e.ImageMap[mapId];
      for (int index = 0; index < image.TotalRects; ++index)
      {
        tc.StrImageMapRect strImageMapRect = image.pMapRect[index];
        if (num3 >= strImageMapRect.rect.left && num3 <= strImageMapRect.rect.right && num4 >= strImageMapRect.rect.top && num4 <= strImageMapRect.rect.bottom)
        {
          this.e.CurMapPict = pict;
          this.e.CurMapId = mapId;
          this.e.CurMapRect = index;
          return true;
        }
      }
    }
    return false;
  }

  internal new bool FreeImageMapTable()
  {
    for (int index = 1; index < this.e.TotalImageMaps; ++index)
      this.e.ImageMap[index] = new tc.StrImageMap();
    this.e.TotalImageMaps = 1;
    return true;
  }

  internal new bool GetHypertextEnd(ref int pLineNo, ref int pColNo)
  {
    int line1 = pLineNo;
    int col1 = pColNo;
    int col2 = 0;
    bool flag1 = true;
    int curCfmt1 = this.GetCurCfmt(line1, col1);
    bool flag2 = this.e.TerFont[curCfmt1].FieldId == 14;
    int line2;
    for (line2 = line1; line2 < this.e.TotalLines; ++line2)
    {
      int num = line2 != line1 ? 0 : col1;
      int len = this.e.text[line2].len;
      for (col2 = num; col2 < len; ++col2)
      {
        int curCfmt2 = this.GetCurCfmt(line2, col2);
        if (!flag2 || this.IsSameField(curCfmt2, curCfmt1))
        {
          if (flag1 && (this.e.TerFont[curCfmt2].style & 64 /*0x40*/) == 0)
            flag1 = false;
          if (!flag1 && !this.IsHypertext(this.GetCurCfmt(line2, col2)))
            break;
        }
        else
          break;
      }
      if (col2 < len)
        break;
    }
    if (line2 >= this.e.TotalLines)
    {
      line2 = this.e.TotalLines - 1;
      col2 = this.e.text[line2].len;
    }
    pLineNo = line2;
    pColNo = col2;
    return true;
  }

  internal new bool GetHypertextStart(ref int pLineNo, ref int pColNo)
  {
    int line1 = pLineNo;
    int col1 = pColNo;
    int col2 = 0;
    int curCfmt = this.GetCurCfmt(line1, col1);
    bool flag1 = (this.e.TerFont[curCfmt].style & 64 /*0x40*/) != 0;
    bool flag2 = this.e.TerFont[curCfmt].FieldId == 14;
    int line2;
    for (line2 = line1; line2 >= 0; --line2)
    {
      for (col2 = line2 != line1 ? this.e.text[line2].len - 1 : col1; col2 >= 0; --col2)
      {
        int prevCfmt = this.GetPrevCfmt(line2, col2);
        if ((!flag2 || this.IsSameField(prevCfmt, curCfmt)) && (!flag1 || (this.e.TerFont[prevCfmt].style & 64 /*0x40*/) != 0))
        {
          if (!this.IsHypertext(prevCfmt))
            flag1 = true;
        }
        else
          break;
      }
      if (col2 >= 0)
        break;
    }
    if (line2 < 0)
    {
      line2 = 0;
      col2 = 0;
    }
    pLineNo = line2;
    pColNo = col2;
    return true;
  }

  internal new bool InsertHyperlink()
  {
    if (!this.CallDialogBox((Form) new terdlg_hyperlink(this.e)) || this.e.DlgText1.Length == 0 || this.e.DlgText2.Length == 0)
      return false;
    this.TerInsertHyperlink(this.e.DlgText1, this.e.DlgText2, 0, true);
    return true;
  }

  internal new bool InvokeTextLink(bool invoke, int line, int col)
  {
    bool flag = false;
    if ((this.e.TerFlags6 & 131072 /*0x020000*/) == 0 && line >= 0 && line < this.e.TotalLines)
    {
      char[] txt = this.e.text[line].txt;
      int len = this.e.text[line].len;
      if (col < 0 || col >= len || this.e.TerFont[this.GetCurCfmt(line, col)].FieldId != 0)
        return false;
      string strA = new string(txt, 0, len);
      string strB1 = "http://";
      int length1 = strB1.Length;
      int indexA;
      if (length1 < len)
      {
        int num = col;
        if (num + length1 > len)
          num = len - length1;
        for (indexA = num; indexA >= 0; --indexA)
        {
          if (string.Compare(strA, indexA, strB1, 0, length1, true) == 0)
          {
            flag = true;
            break;
          }
          if (txt[indexA] == ' ')
            break;
        }
        if (flag)
          goto label_24;
      }
      string strB2 = "www.";
      int length2 = strB2.Length;
      if (length2 < len)
      {
        int num = col;
        if (num + length2 > len)
          num = len - length2;
        for (indexA = num; indexA >= 0; --indexA)
        {
          if (string.Compare(strA, indexA, strB2, 0, length2, true) == 0)
          {
            flag = true;
            break;
          }
          if (txt[indexA] == ' ')
            break;
        }
        if (!flag)
          goto label_23;
      }
      else
        goto label_23;
label_24:
      if (invoke)
      {
        int num = indexA;
        char[] chArray = new char[len];
        int index;
        for (index = num; index < len && txt[index] != ' ' && txt[index] >= ' ' && txt[index] != '<' && txt[index] != '>'; ++index)
          chArray[index - num] = txt[index];
        string fileName = new string(chArray, 0, index - num);
        try
        {
          Process.Start(fileName);
        }
        catch (Exception ex)
        {
        }
      }
      return true;
    }
label_23:
    return false;
  }

  internal new bool IsAnchorName(string code)
  {
    if (this.False(code))
      return false;
    string upper = code.ToUpper();
    return (upper.IndexOf("NAME =") != -1 || upper.IndexOf("NAME=") != -1) && upper.IndexOf("HREF") < 0;
  }

  internal new bool IsHypertext(int CurCfmt) => this.IsHypertext2(CurCfmt, true);

  internal new bool IsHypertext2(int CurCfmt, bool IncludeAnchorName)
  {
    return this.IsHypertext3(CurCfmt, IncludeAnchorName, false);
  }

  internal new bool IsHypertext3(int CurCfmt, bool IncludeAnchorName, bool IncludePageRef)
  {
    if (this.e.TerFont[CurCfmt].FieldId == 14 && (IncludeAnchorName || !this.IsAnchorName(this.e.TerFont[CurCfmt].FieldCode)) || IncludePageRef && this.e.TerFont[CurCfmt].FieldId == 16 /*0x10*/ && this.e.TerFont[CurCfmt].FieldCode != null && this.e.TerFont[CurCfmt].FieldCode.IndexOf("\\h") >= 0)
      return true;
    if ((this.e.TerFlags5 & 65536 /*0x010000*/) != 0)
    {
      if ((this.e.LinkStyle & 16384 /*0x4000*/) != 0 && (this.e.TerFont[CurCfmt].style & 16384 /*0x4000*/) != 0)
        return true;
      if ((this.e.TerFont[CurCfmt].style & this.e.LinkStyle) == 0 && (this.e.LinkStyle & 128 /*0x80*/) == 0 && this.e.LinkStyle != 0)
        return false;
      if ((this.e.TerFont[CurCfmt].style & 128 /*0x80*/) != 0)
        return (this.e.TerFont[CurCfmt].style & this.e.LinkStyle) == this.e.LinkStyle && this.e.LinkStyle != 0 || this.CheckImageMapHit(CurCfmt);
      Color clr1 = this.e.TerFont[CurCfmt].TextColor;
      if (clr1 == tc.CLR_AUTO)
        clr1 = tc.CLR_BLACK;
      if (this.IsSameColor(clr1, this.e.LinkColor) && (this.e.LinkStyle & 16384 /*0x4000*/) == 0)
        return true;
    }
    return false;
  }

  internal new bool IsLoneHypertextChar(int LineNo, int ColNo)
  {
    return this.IsHypertext(this.GetCurCfmt(LineNo, ColNo)) && (this.e.TerFont[this.GetPrevCfmt(LineNo, ColNo)].style & 64 /*0x40*/) != 0 && !this.link.IsHypertext(this.GetNextCfmt(LineNo, ColNo));
  }

  internal new bool SendLinkMessage(bool DoubleClick, bool RightClick)
  {
    tc.StrHyperlink link = new tc.StrHyperlink();
    link.DoubleClick = DoubleClick;
    link.RightClick = RightClick;
    string str;
    link.text = str = "";
    link.code = str;
    return this.e.TerGetHypertext(out link.text, out link.code) && !this.IsAnchorName(link.code) && this.e.SendLinkMessageToParent(ref link);
  }

  internal bool TerAddImageMapRect(
    int MapId,
    string name,
    string LinkInfo,
    string target,
    int left,
    int top,
    int right,
    int bottom)
  {
    if (MapId <= 0 || MapId >= this.e.TotalImageMaps)
      return false;
    tc.StrImageMap image = this.e.ImageMap[MapId];
    image.pMapRect = image.TotalRects != 0 ? this.ReAlloc(image.pMapRect, image.TotalRects + 1) : new tc.StrImageMapRect[1];
    tc.StrImageMapRect strImageMapRect = image.pMapRect[image.TotalRects] with
    {
      name = name,
      LinkInfo = LinkInfo,
      target = target
    };
    strImageMapRect.rect.left = left;
    strImageMapRect.rect.top = top;
    strImageMapRect.rect.right = right;
    strImageMapRect.rect.bottom = bottom;
    image.pMapRect[image.TotalRects] = strImageMapRect;
    ++image.TotalRects;
    this.e.ImageMap[MapId] = image;
    return true;
  }

  internal bool TerApplyHyperlink(string LinkCode, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.False(LinkCode) || LinkCode.Length == 0 || this.e.HilightType == 0 || !this.NormalizeBlock() || !this.NormalizeForFootnote())
      return false;
    if (this.e.HilightType == 1)
    {
      this.e.HilightBegCol = 0;
      this.e.HilightEndCol = this.e.text[this.e.HilightEndRow].len;
      if (this.e.HilightEndCol < 0)
        this.e.HilightEndCol = 0;
    }
    this.SaveUndo(this.e.HilightBegRow, this.e.HilightBegCol, this.e.HilightEndRow, this.e.HilightEndCol - 1, 'F');
    for (int hilightBegRow = this.e.HilightBegRow; hilightBegRow <= this.e.HilightEndRow; ++hilightBegRow)
    {
      int hilightBegCol = hilightBegRow == this.e.HilightBegRow ? this.e.HilightBegCol : 0;
      int num = hilightBegRow == this.e.HilightEndRow ? this.e.HilightEndCol : this.e.text[hilightBegRow].len;
      ushort[] numArray = this.OpenCfmt(hilightBegRow);
      for (int index = hilightBegCol; index < num; ++index)
        numArray[index] = (ushort) this.TerGetFieldFont((int) numArray[index], 14, LinkCode);
      this.CloseCfmt(hilightBegRow);
    }
    ++this.e.TerArg.modified;
    if (repaint)
      this.PaintTer();
    return true;
  }

  internal int TerCreateImageMap(string name)
  {
    if (this.e.TotalImageMaps >= 50)
    {
      this.PrintError(172, nameof (TerCreateImageMap));
      return 0;
    }
    this.e.ImageMap[this.e.TotalImageMaps] = new tc.StrImageMap()
    {
      name = name
    };
    ++this.e.TotalImageMaps;
    return this.e.TotalImageMaps - 1;
  }

  internal bool TerDeleteHypertext(int LineNo, int ColNo, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (LineNo < 0)
    {
      LineNo = this.e.CurLine;
      ColNo = this.e.CurCol;
    }
    if (LineNo < 0 || LineNo >= this.e.TotalLines || ColNo < 0 || ColNo >= this.e.text[LineNo].len || !this.IsHypertext(this.GetCurCfmt(LineNo, ColNo)))
      return false;
    this.e.HilightType = 0;
    int pLineNo = LineNo;
    int pColNo = ColNo;
    this.GetHypertextStart(ref pLineNo, ref pColNo);
    this.e.HilightBegRow = pLineNo;
    this.e.HilightBegCol = pColNo;
    this.e.HilightEndRow = LineNo;
    this.e.HilightEndCol = ColNo + 1;
    if (this.e.HilightBegCol < this.e.HilightEndCol)
      --this.e.HilightEndCol;
    int hilightEndRow = this.e.HilightEndRow;
    pColNo = this.e.HilightEndCol;
    this.GetHypertextEnd(ref hilightEndRow, ref pColNo);
    this.e.HilightEndRow = hilightEndRow;
    this.e.HilightEndCol = pColNo;
    this.e.HilightType = 2;
    this.e.StretchHilight = false;
    return this.e.TerDeleteBlock(repaint);
  }

  internal bool TerFindHlinkField(
    string CodeString1,
    string CodeString2,
    ref int pLine,
    ref int pCol)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (CodeString1 == null && CodeString2 == null)
      CodeString1 = "";
    while (this.e.TerLocateFieldChar(14, (string) null, true, ref pLine, ref pCol, true))
    {
      int curCfmt = this.GetCurCfmt(pLine, pCol);
      if (this.True(this.e.TerFont[curCfmt].FieldCode) && (this.True(CodeString1) && (CodeString1 == "" || this.e.TerFont[curCfmt].FieldCode.IndexOf(CodeString1) >= 0) || this.True(CodeString2) && (CodeString2 == "" || this.e.TerFont[curCfmt].FieldCode.IndexOf(CodeString2) >= 0)))
        return true;
      if (!this.e.TerLocateFieldChar(14, this.e.TerFont[curCfmt].FieldCode, false, ref pLine, ref pCol, true))
        break;
    }
    return false;
  }

  internal bool TerGetHypertext(out string text, out string code)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    string str;
    code = str = "";
    text = str;
    return !this.e.RulerClicked && this.TerGetHypertextEx(out text, out code, false);
  }

  internal bool TerGetHypertext2(
    int LineNo,
    int ColNo,
    out string text,
    out string code,
    bool select)
  {
    int index1 = 0;
    int col = 0;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    string str1;
    code = str1 = "";
    text = str1;
    if (LineNo < 0)
    {
      LineNo = this.e.CurLine;
      ColNo = this.e.CurCol;
    }
    if (this.e.text[LineNo].len == 0)
      return false;
    bool flag1 = (this.e.TerFlags2 & 536870912 /*0x20000000*/) != 0;
    bool flag2 = (this.e.TerFlags4 & 128 /*0x80*/) != 0;
    int curCfmt = this.GetCurCfmt(LineNo, ColNo);
    if (!this.IsHypertext(curCfmt) && (!flag1 || (this.e.TerFont[curCfmt].style & 64 /*0x40*/) == 0))
      return false;
    if (select)
      this.e.HilightType = 0;
    int pLine1;
    int num1 = pLine1 = LineNo;
    int pCol1;
    int num2 = pCol1 = ColNo;
    if (curCfmt == this.e.CurMapPict && this.e.CurMapId > 0)
    {
      tc.StrImageMap image = this.e.ImageMap[this.e.CurMapId];
      tc.StrImageMapRect strImageMapRect = image.pMapRect[this.e.CurMapRect];
      text = "Image Map: ";
      text += image.name;
      string str2 = new string('"', 1);
      code = "";
      if (strImageMapRect.LinkInfo.Length > 0)
        code = $"href={str2}{strImageMapRect.LinkInfo}{str2}";
      int length = code.Length;
      if (strImageMapRect.target.Length > 0 && strImageMapRect.LinkInfo.Length > 0)
      {
        string str3 = code;
        code = $"{str3} target={str2}{strImageMapRect.target}{str2}";
      }
      if (select)
      {
        this.e.HilightBegRow = this.e.HilightEndRow = pLine1;
        this.e.HilightBegCol = pCol1;
        this.e.HilightEndCol = pCol1 + 1;
        this.e.HilightType = 2;
        this.PaintTer();
      }
      return true;
    }
    if (this.e.TerFont[curCfmt].FieldId == 14)
    {
      if (this.True(this.e.TerFont[curCfmt].FieldCode))
        code += this.e.TerFont[curCfmt].FieldCode;
      this.GetFieldLoc(LineNo, ColNo, true, out pLine1, out pCol1);
      int pLine2;
      int pCol2;
      this.GetFieldLoc(LineNo, ColNo, false, out pLine2, out pCol2);
      int num3 = 0;
      for (int SrcLine = pLine1; SrcLine <= pLine2; ++SrcLine)
      {
        int SrcCol = SrcLine == pLine1 ? pCol1 : 0;
        int num4 = SrcLine == pLine2 ? pCol2 : this.e.text[SrcLine].len;
        int length = num4 - SrcCol;
        char[] ptr = new char[length + 1];
        this.GetLineData(SrcLine, SrcCol, num4 - SrcCol, ptr, (ushort[]) null, (ushort[]) null);
        text += new string(ptr, 0, length);
        num3 += length;
      }
      if (select)
      {
        this.e.HilightBegRow = pLine1;
        this.e.HilightEndRow = pLine2;
        this.e.HilightBegCol = pCol1;
        this.e.HilightEndCol = pCol2;
        this.e.HilightType = 2;
        this.PaintTer();
      }
      return true;
    }
    if (flag1 && (this.e.TerFont[curCfmt].style & 64 /*0x40*/) != 0)
    {
      for (int line = pLine1; line < this.e.TotalLines; ++line)
      {
        int len = this.e.text[line].len;
        if (len != 0)
        {
          ushort[] numArray = this.OpenCfmt(line);
          for (index1 = line != pLine1 ? 0 : pCol1 + 1; index1 < len; ++index1)
          {
            int CurCfmt = (int) numArray[index1];
            if (this.IsHypertext(CurCfmt))
            {
              num1 = line;
              num2 = index1;
              break;
            }
            if ((this.e.TerFont[CurCfmt].style & 64 /*0x40*/) == 0)
              return false;
            pLine1 = line;
            pCol1 = index1;
          }
          this.CloseCfmt(line);
          if (index1 < len)
            break;
        }
      }
    }
    else
    {
      for (int line = pLine1; line >= 0; --line)
      {
        int len = this.e.text[line].len;
        if (len != 0)
        {
          ushort[] numArray = this.OpenCfmt(line);
          for (index1 = line != pLine1 ? len - 1 : pCol1 - 1; index1 >= 0; --index1)
          {
            if (!this.IsHypertext((int) numArray[index1]))
            {
              pLine1 = line;
              pCol1 = index1;
              break;
            }
            num1 = line;
            num2 = index1;
          }
          this.CloseCfmt(line);
          if (index1 >= 0)
            break;
        }
      }
    }
    int line1 = -1;
    int line2;
    for (line2 = num1; line2 < this.e.TotalLines; ++line2)
    {
      int len = this.e.text[line2].len;
      if (len != 0)
      {
        ushort[] numArray = this.OpenCfmt(line2);
        char[] txt = this.e.text[line2].txt;
        for (index1 = line2 != num1 ? 0 : num2; index1 < len; ++index1)
        {
          if (!this.IsHypertext((int) numArray[index1]))
          {
            line1 = line2;
            col = index1;
            break;
          }
          text += new string(txt[index1], 1);
        }
        this.CloseCfmt(line2);
        if (index1 < len)
          break;
      }
    }
    if (select)
    {
      if (flag2)
      {
        this.e.HilightBegRow = num1;
        this.e.HilightBegCol = num2;
      }
      else
      {
        this.e.HilightEndRow = line2;
        this.e.HilightEndCol = index1;
      }
    }
    if (flag2)
    {
      if (line1 < 0 || (this.e.TerFont[this.GetCurCfmt(line1, col)].style & 64 /*0x40*/) == 0)
        return true;
    }
    else
    {
      if (num1 == 0 && num2 == 0 || (this.e.TerFont[this.GetCurCfmt(pLine1, pCol1)].style & 64 /*0x40*/) == 0)
        return true;
      line1 = pLine1;
      col = pCol1;
      for (int line3 = pLine1; line3 >= 0; --line3)
      {
        int len = this.e.text[line3].len;
        if (len != 0)
        {
          ushort[] numArray = this.OpenCfmt(line3);
          int index2;
          for (index2 = line3 != pLine1 ? len - 1 : pCol1 - 1; index2 >= 0 && (this.e.TerFont[(int) numArray[index2]].style & 64 /*0x40*/) != 0; --index2)
          {
            line1 = line3;
            col = index2;
          }
          this.CloseCfmt(line3);
          if (index2 >= 0)
            break;
        }
      }
      if (select)
      {
        this.e.HilightBegRow = line1;
        this.e.HilightBegCol = col;
      }
    }
    for (int line4 = line1; line4 < this.e.TotalLines; ++line4)
    {
      int len = this.e.text[line4].len;
      if (len != 0)
      {
        ushort[] numArray = this.OpenCfmt(line4);
        char[] txt = this.e.text[line4].txt;
        int index3;
        for (index3 = line4 != line1 ? 0 : col; index3 < len; ++index3)
        {
          int CurCfmt = (int) numArray[index3];
          if (this.IsHypertext(CurCfmt) || (this.e.TerFont[CurCfmt].style & 64 /*0x40*/) == 0)
          {
            if (flag2)
            {
              this.e.HilightEndRow = line4;
              this.e.HilightEndCol = index3;
              break;
            }
            break;
          }
          code += new string(txt[index3], 1);
        }
        this.CloseCfmt(line4);
        if (index3 < len)
          break;
      }
    }
    if (select)
    {
      this.e.HilightType = 2;
      this.PaintTer();
    }
    return true;
  }

  internal bool TerGetHypertextEx(out string text, out string code, bool select)
  {
    return this.TerGetHypertext2(-1, 0, out text, out code, select);
  }

  internal bool TerGetImageMapInfo(int MapId, out string name, out int pCount)
  {
    name = "";
    pCount = 0;
    if (MapId < 0 || MapId >= this.e.TotalImageMaps)
      return false;
    name = this.e.ImageMap[MapId].name;
    pCount = this.e.ImageMap[MapId].TotalRects;
    return true;
  }

  internal bool TerGetImageMapRectInfo(
    int MapId,
    int MapRectId,
    out string name,
    out string LinkInfo,
    out string target,
    out int pLeft,
    out int pTop,
    out int pRight,
    out int pBottom)
  {
    string str1;
    target = str1 = "";
    string str2;
    LinkInfo = str2 = str1;
    name = str2;
    int num1;
    pBottom = num1 = 0;
    int num2;
    pTop = num2 = num1;
    int num3;
    pRight = num3 = num2;
    pLeft = num3;
    if (this.e.CurMapId > 0)
    {
      if (MapId < 0)
      {
        MapId = this.e.CurMapId;
        MapRectId = this.e.CurMapRect;
      }
      else if (MapRectId < 0)
        MapRectId = this.e.CurMapRect;
    }
    if (MapId < 0 || MapId >= this.e.TotalImageMaps)
      return false;
    tc.StrImageMap image = this.e.ImageMap[MapId];
    if (MapRectId < 0 || MapRectId >= image.TotalRects)
      return false;
    tc.StrImageMapRect strImageMapRect = image.pMapRect[MapRectId];
    name = strImageMapRect.name;
    LinkInfo = strImageMapRect.LinkInfo;
    target = strImageMapRect.target;
    pLeft = strImageMapRect.rect.left;
    pRight = strImageMapRect.rect.right;
    pTop = strImageMapRect.rect.top;
    pBottom = strImageMapRect.rect.bottom;
    return true;
  }

  internal int TerGetPictMapId(int PictId)
  {
    return PictId < 0 || PictId >= this.e.TotalFonts || !this.e.TerFont[PictId].InUse || (this.e.TerFont[PictId].style & 128 /*0x80*/) == 0 ? 0 : this.e.TerFont[PictId].MapId;
  }

  internal int TerImageMapNameToId(string name)
  {
    for (int id = 1; id < this.e.TotalImageMaps; ++id)
    {
      if (this.True(this.e.ImageMap[id].name) && string.Compare(this.e.ImageMap[id].name, name) == 0)
        return id;
    }
    return 0;
  }

  internal int TerInsertHyperlink(string LinkText, string LinkCode, int PictId, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.False(LinkCode) || LinkCode.Length == 0)
      return -1;
    if (PictId > 0)
    {
      if (PictId >= this.e.TotalFonts || !this.e.TerFont[PictId].InUse || (this.e.TerFont[PictId].style & 128 /*0x80*/) == 0)
        return -1;
      this.TerGetFieldFont(PictId, 14, LinkCode);
      this.e.TerInsertObjectId(PictId, repaint);
      return PictId;
    }
    if (this.False(LinkText) || LinkText.Length == 0)
      return -1;
    int linkStyle = this.e.LinkStyle;
    tc.ResetUintFlag(ref linkStyle, 16384 /*0x4000*/);
    if (this.True(linkStyle))
      this.e.SetTerCharStyle(linkStyle, true, false);
    this.e.SetTerColor(this.e.LinkColor, false);
    int effectiveCfmt = this.GetEffectiveCfmt();
    int fieldFont;
    this.e.InputFontId = fieldFont = this.TerGetFieldFont(effectiveCfmt, 14, LinkCode);
    this.e.InsertTerText(LinkText, repaint);
    this.e.InputFontId = -1;
    ++this.e.TerArg.modified;
    return fieldFont;
  }

  internal bool TerSetLinkDblClick(bool DblClick)
  {
    int num = this.e.LinkDblClick ? 1 : 0;
    this.e.LinkDblClick = DblClick;
    return num != 0;
  }

  internal bool TerSetMapRectInfo(
    int MapId,
    int RectId,
    string name,
    string LinkInfo,
    string target)
  {
    if (this.e.CurMapId > 0)
    {
      if (MapId < 0)
      {
        MapId = this.e.CurMapId;
        RectId = this.e.CurMapRect;
      }
      else if (RectId < 0)
        RectId = this.e.CurMapRect;
    }
    if (MapId <= 0 || MapId >= this.e.TotalImageMaps)
      return false;
    tc.StrImageMap image = this.e.ImageMap[MapId];
    if (RectId < 0 || RectId >= image.TotalRects)
      return false;
    tc.StrImageMapRect strImageMapRect = image.pMapRect[RectId] with
    {
      name = name,
      LinkInfo = LinkInfo,
      target = target
    };
    image.pMapRect[RectId] = strImageMapRect;
    this.e.ImageMap[MapId] = image;
    return true;
  }

  internal bool TerSetPictMapId(int PictId, int MapId)
  {
    if (MapId < 0 || MapId >= this.e.TotalImageMaps || PictId < 0 || PictId >= this.e.TotalFonts || !this.e.TerFont[PictId].InUse || (this.e.TerFont[PictId].style & 128 /*0x80*/) == 0)
      return false;
    this.e.TerFont[PictId].MapId = MapId;
    return true;
  }

  internal bool TerUpdateHyperlinkCode(string NewLinkCode)
  {
    int num1 = 0;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.False(NewLinkCode) || NewLinkCode.Length == 0)
      return false;
    int curCfmt = this.GetCurCfmt(this.e.CurLine, this.e.CurCol);
    if (this.e.TerFont[curCfmt].FieldId != 14)
      return false;
    string fieldCode = !this.True(this.e.TerFont[curCfmt].FieldCode) ? "" : this.e.TerFont[curCfmt].FieldCode;
    int curLine;
    int num2 = curLine = this.e.CurLine;
    int curCol;
    int num3 = curCol = this.e.CurCol;
    if (!this.e.TerLocateFieldChar(14, fieldCode, false, ref num2, ref num3, false))
    {
      num2 = 0;
      num3 = 0;
    }
    else
      this.NextTextPos(ref num2, ref num3);
    this.e.TerLocateFieldChar(14, fieldCode, false, ref curLine, ref curCol, true);
    this.PrevTextPos(ref curLine, ref curCol);
    int num4 = -1;
    for (int line = num2; line <= curLine; ++line)
    {
      int num5 = line == num2 ? num3 : 0;
      int num6 = line == curLine ? curCol : this.e.text[line].len - 1;
      ushort[] numArray = this.OpenCfmt(line);
      for (int index = num5; index <= num6; ++index)
      {
        int font = (int) numArray[index];
        if (font == num4)
          numArray[index] = (ushort) num1;
        else if (this.e.TerFont[font].FieldId == 14)
        {
          num1 = this.TerGetFieldFont(font, 14, NewLinkCode);
          if (num1 >= 0)
          {
            num4 = font;
            numArray[index] = (ushort) num1;
          }
          else
            goto label_20;
        }
        else
          goto label_20;
      }
      this.CloseCfmt(line);
    }
label_20:
    ++this.e.TerArg.modified;
    return true;
  }

  internal bool TerUpdateHyperlinkText(string NewLinkText, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.False(NewLinkText))
      return false;
    int curCfmt = this.GetCurCfmt(this.e.CurLine, this.e.CurCol);
    if (this.e.TerFont[curCfmt].FieldId != 14)
      return false;
    string fieldCode = !this.True(this.e.TerFont[curCfmt].FieldCode) ? "" : this.e.TerFont[curCfmt].FieldCode;
    int curLine;
    int row = curLine = this.e.CurLine;
    int curCol;
    int col = curCol = this.e.CurCol;
    if (this.e.TerLocateFieldChar(14, fieldCode, false, ref row, ref col, false))
      this.NextTextPos(ref row, ref col);
    else
      row = col = 0;
    int abs1 = this.RowColToAbs(row, col);
    this.e.TerLocateFieldChar(14, fieldCode, false, ref curLine, ref curCol, true);
    this.PrevTextPos(ref curLine, ref curCol);
    int abs2 = this.RowColToAbs(curLine, curCol);
    this.ReplaceTextString(NewLinkText, abs1, abs2);
    ++this.e.TerArg.modified;
    if (repaint)
      this.PaintTer();
    return true;
  }
}
