// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.CIo
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class CIo : COp
{
  internal CIo(ImRtfEditor ImRtfEditor)
    : base(ImRtfEditor)
  {
  }

  internal new bool AddExt(string name, string ext)
  {
    bool flag = false;
    int length = name.Length;
    for (int index = length - 1; index >= 0 && index >= length - 4 && name[index] != ':' && name[index] != '\\'; --index)
    {
      if (name[index] == '.')
      {
        flag = true;
        break;
      }
    }
    if (!flag)
      name += ext;
    return true;
  }

  internal new bool GetFileName(
    bool open,
    ref string file,
    int FilterIndex,
    string filter,
    string ext)
  {
    string str = (string) null;
    int length = filter.Length;
    if (length > 0 && filter[length - 1] == '|')
      filter = filter.Substring(0, length - 1);
    bool fileName;
    if (open)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.DefaultExt = ext;
      openFileDialog.FileName = file;
      openFileDialog.Filter = filter;
      openFileDialog.FilterIndex = FilterIndex;
      openFileDialog.RestoreDirectory = true;
      openFileDialog.ShowHelp = true;
      if (this.e.UserDir.Length > 0)
        openFileDialog.InitialDirectory = this.e.UserDir;
      fileName = openFileDialog.ShowDialog() == DialogResult.OK;
      if (fileName)
        file = openFileDialog.FileName;
    }
    else
    {
      SaveFileDialog saveFileDialog = new SaveFileDialog();
      saveFileDialog.DefaultExt = ext;
      saveFileDialog.FileName = file;
      saveFileDialog.Filter = filter;
      saveFileDialog.FilterIndex = FilterIndex;
      saveFileDialog.RestoreDirectory = true;
      saveFileDialog.ShowHelp = true;
      if (this.e.UserDir.Length > 0)
        saveFileDialog.InitialDirectory = this.e.UserDir;
      int num = this.e.TerArg.SaveFormat != 3 ? this.e.TerArg.SaveFormat : this.e.FileFormat;
      saveFileDialog.FilterIndex = 1;
      if (num == 0)
        saveFileDialog.FilterIndex = 1;
      if (num == 1)
        saveFileDialog.FilterIndex = 2;
      if (num == 2)
        saveFileDialog.FilterIndex = 3;
      if (num == 5)
        saveFileDialog.FilterIndex = 4;
      if (num == 6)
        saveFileDialog.FilterIndex = 5;
      if (num == 7)
        saveFileDialog.FilterIndex = 6;
      if (num == 4)
        saveFileDialog.FilterIndex = 7;
      fileName = saveFileDialog.ShowDialog() == DialogResult.OK;
      if (fileName)
      {
        file = saveFileDialog.FileName;
        int filterIndex = saveFileDialog.FilterIndex;
        if (filterIndex == 1)
          this.e.TerArg.SaveFormat = 0;
        if (filterIndex == 2)
          this.e.TerArg.SaveFormat = 1;
        if (filterIndex == 3)
          this.e.TerArg.SaveFormat = 2;
        if (filterIndex == 4)
          this.e.TerArg.SaveFormat = 5;
        if (filterIndex == 5)
          this.e.TerArg.SaveFormat = 6;
        if (filterIndex == 6)
          this.e.TerArg.SaveFormat = 7;
        if (filterIndex == 7)
          this.e.TerArg.SaveFormat = 4;
        str = (string) null;
        string ext1 = this.e.TerArg.SaveFormat != 2 ? (this.e.TerArg.SaveFormat != 4 ? ".txt" : ".htm") : ".rtf";
        this.AddExt(file, ext1);
      }
    }
    this.e.Focus();
    do
      ;
    while (this.PeekMessage(out COp.MSG _, this.e.hTerWnd, 512 /*0x0200*/, 522, 3));
    return fileName;
  }

  internal string GetTerBuffer()
  {
    int modified = this.e.TerArg.modified;
    bool notified = this.e.Notified;
    try
    {
      string terBuffer = (string) null;
      if (!this.e.IsHandleCreated)
        this.e.TerCreateControl();
      if (this.e.TerArg.SaveFormat == 4)
      {
        string buf = (string) null;
        return !this.LoadHtmlAddOn() || !this.HtsSaveFromTer(false, (string) null, out buf) ? (string) null : buf;
      }
      char inputType = this.e.TerArg.InputType;
      if (this.e.TerArg.InputType != 'B')
      {
        this.e.TerArg.InputType = 'B';
        this.e.TerArg.hBuffer = (string) null;
        this.e.TerArg.BufferLen = 0;
        this.e.TerArg.delim = '\r';
      }
      this.e.TerArg.hBuffer = (string) null;
      if ((!this.True(this.e.TerArg.modified) && this.e.TerArg.hBuffer != null || this.TerSave(this.e.DocName, false)) && this.False(this.e.TerArg.modified) && this.e.TerArg.hBuffer != null)
      {
        terBuffer = this.e.TerArg.hBuffer;
        this.e.TerArg.hBuffer = (string) null;
        this.e.TerArg.BufferLen = 0;
      }
      this.e.TerArg.InputType = inputType;
      return terBuffer;
    }
    finally
    {
      this.e.TerArg.modified = modified;
      this.e.Notified = notified;
    }
  }

  internal bool ReadTerFile(string file)
  {
    Cursor x1 = (Cursor) null;
    bool flag = false;
    tc.StrFont font = new tc.StrFont();
    tc.StrPrtFont pfont = new tc.StrPrtFont();
    int x2 = 0;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if ((this.e.TerFlags4 & 1048576 /*0x100000*/) != 0)
    {
      if (!this.LoadHtmlAddOn())
        return false;
      for (int line = 0; line < this.e.TotalLines; ++line)
        this.init.FreeLine(line);
      this.ResetInitVariables();
      this.e.TerArg.hBuffer = (string) null;
      this.e.TerArg.BufferLen = 0;
      return this.HtsReadFromTer(true, file, (string) null);
    }
    if (file.Length > 0 && (!System.IO.File.Exists(file) || System.IO.File.GetAttributes(file) != FileAttributes.Normal && (System.IO.File.GetAttributes(file) & (FileAttributes.ReadOnly | FileAttributes.Archive)) == (FileAttributes) 0))
      return false;
    char inputType = this.e.TerArg.InputType;
    if (this.e.TerArg.InputType != 'F')
    {
      this.e.TerArg.InputType = 'F';
      this.e.TerArg.hBuffer = (string) null;
      this.e.TerArg.BufferLen = 0;
    }
    if ((this.e.TerFlags & 512 /*0x0200*/) == 0)
      x1 = Cursors.WaitCursor;
    for (int line = 0; line < this.e.TotalLines; ++line)
      this.init.FreeLine(line);
    if (this.True(x1))
      this.e.Cursor = x1;
    if (this.True(this.e.BkPictId))
    {
      this.TransferFontId(false, this.e.BkPictId, ref font, ref pfont);
      x2 = this.e.BkPictId;
      this.e.BkPictId = 0;
    }
    this.ResetInitVariables();
    this.e.DocName = file;
    this.e.TerLastMsg = 0;
    if (this.TerRead(this.e.DocName) && this.e.TerLastMsg == 0)
    {
      if ((this.e.TerFlags3 & 1024 /*0x0400*/) != 0 || this.e.ProtectForm)
        this.SelectFirstFormField();
      if (this.e.TerArg.ShowHorBar || this.e.TerArg.ShowVerBar)
        this.SetScrollBars();
      flag = true;
    }
    this.e.TerArg.InputType = inputType;
    if (this.True(x2))
    {
      if (this.e.TotalFonts >= this.e.MaxFonts)
        this.ExpandFontTable(this.e.MaxFonts + this.e.MaxFonts / 3 + 1);
      this.TransferFontId(true, this.e.TotalFonts, ref font, ref pfont);
      ++this.e.TotalFonts;
      this.e.TerSetBkPictId(this.e.TotalFonts - 1, this.e.BkPictFlag, true);
      return flag;
    }
    this.PaintTer();
    return flag;
  }

  internal bool SaveTerFile(string file)
  {
    bool flag = false;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.e.TerArg.SaveFormat == 4)
      return this.LoadHtmlAddOn() && this.HtsSaveFromTer(true, file, out tc.SkipStr);
    char inputType = this.e.TerArg.InputType;
    if (this.e.TerArg.InputType != 'F')
    {
      this.e.TerArg.InputType = 'F';
      this.e.TerArg.hBuffer = (string) null;
      this.e.TerArg.BufferLen = 0;
    }
    this.e.DocName = file;
    if (this.TerSave(this.e.DocName, false))
      flag = true;
    this.e.TerArg.InputType = inputType;
    return flag;
  }

  internal bool SetTerBuffer(string hBuffer, string name)
  {
    Cursor cursor = (Cursor) null;
    bool flag = false;
    tc.StrFont font = new tc.StrFont();
    tc.StrPrtFont pfont = new tc.StrPrtFont();
    int x = 0;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (tc.expired)
      return false;
    if ((this.e.TerFlags4 & 1048576 /*0x100000*/) != 0)
    {
      if (!this.LoadHtmlAddOn())
        return false;
      for (int line = 0; line < this.e.TotalLines; ++line)
        this.init.FreeLine(line);
      this.ResetInitVariables();
      this.e.TerArg.hBuffer = (string) null;
      this.e.TerArg.BufferLen = 0;
      return this.HtsReadFromTer(false, "", hBuffer);
    }
    char inputType = this.e.TerArg.InputType;
    if (this.e.TerArg.InputType != 'B')
    {
      this.e.TerArg.InputType = 'B';
      this.e.TerArg.hBuffer = (string) null;
      this.e.TerArg.BufferLen = 0;
      this.e.TerArg.delim = '\r';
    }
    if ((this.e.TerFlags & 512 /*0x0200*/) == 0)
    {
      cursor = this.e.Cursor;
      this.e.Cursor = Cursors.WaitCursor;
    }
    for (int line = 0; line < this.e.TotalLines; ++line)
      this.init.FreeLine(line);
    if (cursor != (Cursor) null)
      this.e.Cursor = cursor;
    if (this.True(this.e.BkPictId))
    {
      this.TransferFontId(false, this.e.BkPictId, ref font, ref pfont);
      x = this.e.BkPictId;
      this.e.BkPictId = 0;
    }
    this.ResetInitVariables();
    this.e.TerArg.hBuffer = (string) null;
    this.e.TerArg.hBuffer = hBuffer;
    this.e.TerArg.BufferLen = hBuffer.Length;
    if (this.TerRead(this.e.DocName))
    {
      if ((this.e.TerFlags3 & 1024 /*0x0400*/) != 0 || this.e.ProtectForm)
        this.SelectFirstFormField();
      if (name != null && this.e.Parent != null)
        this.e.Parent.Text = name;
      if (this.e.TerArg.ShowHorBar || this.e.TerArg.ShowVerBar)
        this.SetScrollBars();
      flag = true;
    }
    this.e.TerArg.InputType = inputType;
    if (this.True(x))
    {
      if (this.e.TotalFonts >= this.e.MaxFonts)
        this.ExpandFontTable(this.e.MaxFonts + this.e.MaxFonts / 3 + 1);
      this.e.TerFont[this.e.TotalFonts].CharWidth = (int[]) null;
      this.e.TerFont[this.e.TotalFonts].hidden = (tc.ClsHdnFont) null;
      this.e.PrtFont[this.e.TotalFonts].CharWidth = (int[]) null;
      this.e.PrtFont[this.e.TotalFonts].hidden = (tc.ClsHdnFont) null;
      this.TransferFontId(true, this.e.TotalFonts, ref font, ref pfont);
      ++this.e.TotalFonts;
      this.e.TerSetBkPictId(this.e.TotalFonts - 1, this.e.BkPictFlag, true);
      return flag;
    }
    this.PaintTer();
    return flag;
  }

  internal bool TerAppendText(string text, int FontId, int ParaId, bool repaint)
  {
    return this.TerAppendTextEx(text, FontId, ParaId, -1, -1, repaint);
  }

  internal bool TerAppendText2(
    string str,
    int FontId,
    int ParaId,
    int CellId,
    int ParaFID,
    bool repaint)
  {
    int[] pPfmt = (int[]) null;
    int[] numArray = new int[2];
    int num1 = 0;
    int num2 = 0;
    int num3 = -1;
    int x = 0;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    char[] charArray = str.ToCharArray();
    int num4 = !this.e.TerArg.WordWrap ? this.e.LineWidth - 2 : this.e.LineWidth / 3;
    int length1 = charArray.Length;
    for (int index = 0; index < length1 && charArray[index] != char.MinValue; ++index)
    {
      if (charArray[index] == '\r')
        ++num1;
      if ((int) charArray[index] == (int) this.e.ParaChar || (int) charArray[index] == (int) this.e.CellChar || charArray[index] == '\u000F')
        ++num2;
      if ((int) charArray[index] == (int) this.e.CellChar)
        x |= 16 /*0x10*/;
      if (charArray[index] == '\u0012')
        x |= 32 /*0x20*/;
      if (num1 == 1 && num3 < 0)
        num3 = index;
    }
    if (length1 != 0)
    {
      bool flag = num3 >= 0 && num3 < length1 - 2;
      if (CellId > 0 || ParaFID > 0 || (length1 < this.e.LineWidth || this.e.TerArg.WordWrap) && num1 + num2 <= 1 && (num1 != 1 || length1 < 2 || charArray[length1 - 2] == '\r') && (num2 != 1 || length1 < 1 || (int) charArray[length1 - 1] == (int) this.e.ParaChar || charArray[length1 - 1] == '\u000F' || (int) charArray[length1 - 1] == (int) this.e.CellChar))
      {
        int num5 = 0;
        int num6;
        for (int length2 = charArray.Length; num5 < length2; num5 += num6)
        {
          num6 = length2 - num5;
          if (num6 > num4)
            num6 = num4;
          if (charArray[num5 + num6 - 1] == '\r' && num6 == num4)
            --num6;
          if (flag)
          {
            for (int index = 0; index < num6 - 2; ++index)
            {
              if (charArray[num5 + index] == '\r' && charArray[num5 + index + 1] == '\n')
              {
                num6 = index + 2;
                break;
              }
            }
          }
          if (this.CheckLineLimit(this.e.TotalLines + 1))
          {
            int totalLines = this.e.TotalLines;
            ++this.e.TotalLines;
            this.InitLine(totalLines);
            this.LineAlloc(totalLines, 0, num6);
            char[] txt = this.e.text[totalLines].txt;
            for (int index = 0; index < num6; ++index)
              txt[index] = charArray[num5 + index];
            if (num6 >= 2 && txt[num6 - 2] == '\r')
            {
              if (this.e.TerArg.WordWrap)
              {
                txt[num6 - 2] = this.e.ParaChar;
                this.LineAlloc(totalLines, num6, num6 - 1);
              }
              else
                this.LineAlloc(totalLines, num6, num6 - 2);
            }
            this.e.text[totalLines].fmt = (ushort[]) null;
            if (FontId >= 0 && FontId < this.e.TotalFonts && this.e.TerFont[FontId].InUse)
              this.e.text[totalLines].UniFmt = (ushort) FontId;
            else if (this.e.InputFontId >= 0)
              this.e.text[totalLines].UniFmt = (ushort) this.e.InputFontId;
            else
              this.e.text[totalLines].UniFmt = (ushort) 0;
            if (ParaId >= 0 && ParaId < this.e.TotalPfmts)
              this.e.text[totalLines].pfmt = ParaId;
            if (CellId >= 0 && CellId < this.e.TotalCells)
              this.e.text[totalLines].cid = CellId;
            if (ParaFID >= 0 && ParaFID < this.e.TotalParaFrames)
              this.e.text[totalLines].fid = ParaFID;
            if (this.True(x) && this.AllocTabw(totalLines))
              this.e.text[totalLines].tabw.type = x;
            if (num2 > 0 && num5 + num6 == length2)
              this.e.text[totalLines].flags = 1;
          }
          else
            break;
        }
      }
      else
      {
        int inputFontId = this.e.InputFontId;
        if (FontId >= 0 && FontId < this.e.TotalFonts && this.e.TerFont[FontId].InUse)
          this.e.InputFontId = FontId;
        if (ParaId >= 0 && ParaId < this.e.TotalPfmts)
        {
          numArray[0] = ParaId;
          numArray[1] = -1;
          pPfmt = numArray;
        }
        int curLine1 = this.e.CurLine;
        int curRow = this.e.CurRow;
        int curCol = this.e.CurCol;
        this.e.CurLine = this.e.TotalLines - 1;
        this.e.CurCol = this.e.text[this.e.CurLine].len;
        int curLine2 = this.e.CurLine;
        int cid = this.e.text[curLine2].cid;
        if (CellId >= 0)
          this.e.text[this.e.CurLine].cid = CellId;
        int fid = this.e.text[curLine2].fid;
        if (ParaFID >= 0)
          this.e.text[this.e.CurLine].fid = ParaFID;
        this.InsertBuffer(charArray, (ushort[]) null, pPfmt, false);
        if (CellId >= 0)
          this.e.text[curLine2].cid = cid;
        if (ParaFID >= 0)
          this.e.text[curLine2].fid = fid;
        this.e.CurLine = curLine1;
        this.e.CurRow = curRow;
        this.e.CurCol = curCol;
        this.e.InputFontId = inputFontId;
      }
    }
    if (repaint)
    {
      this.e.WinHeight = this.e.TerWinHeight / this.e.TerFont[0].height;
      this.PaintTer();
    }
    return true;
  }

  internal bool TerAppendTextEx(
    string text,
    int FontId,
    int ParaId,
    int CellId,
    int ParaFID,
    bool repaint)
  {
    return this.TerAppendText2(text, FontId, ParaId, CellId, ParaFID, repaint);
  }

  internal bool TerDocName(bool get, ref string name)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (get)
      name = this.e.DocName;
    else
      this.e.DocName = name;
    return true;
  }

  internal int TerGetLine(int LineNo, out string text, out int[] font)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    text = "";
    font = new int[1];
    if (LineNo < 0 || LineNo >= this.e.TotalLines)
      return -1;
    char[] txt = this.e.text[LineNo].txt;
    if (txt != null)
      text = new string(txt, 0, this.e.text[LineNo].len);
    ushort[] numArray = this.OpenCfmt(LineNo);
    font = new int[this.e.text[LineNo].len];
    for (int index = 0; index < this.e.text[LineNo].len; ++index)
      font[index] = (int) numArray[index];
    this.CloseCfmt(LineNo);
    return this.e.text[LineNo].len;
  }

  internal bool TerGetLineInfo(
    int LineNo,
    out int ParaId,
    out int CellId,
    out int ParaFID,
    out int x,
    out int y,
    out int height,
    out int lflags,
    out int InfoFlags)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (LineNo == -1)
      LineNo = this.e.CurLine;
    int num1;
    height = num1 = 0;
    int num2;
    y = num2 = num1;
    int num3;
    x = num3 = num2;
    int num4;
    ParaFID = num4 = num3;
    int num5;
    CellId = num5 = num4;
    ParaId = num5;
    int num6;
    InfoFlags = num6 = 0;
    lflags = num6;
    if (LineNo < 0 || LineNo >= this.e.TotalLines)
      return false;
    ParaId = this.e.text[LineNo].pfmt;
    CellId = this.e.text[LineNo].cid;
    ParaFID = this.e.text[LineNo].fid;
    x = this.e.text[LineNo].x;
    y = this.e.text[LineNo].y;
    if (this.e.TerArg.PageMode)
    {
      if (this.e.BorderShowing)
        y -= this.e.TopBorderHeight;
      else if (!this.e.ViewPageHdrFtr)
      {
        int num7 = (int) ((double) this.e.UnitResY * (double) this.e.TerSect[this.GetSection(LineNo)].TopMargin);
        y += num7;
      }
      else
      {
        int section = this.GetSection(LineNo);
        y += this.e.TerSect1[section].HiddenY;
      }
    }
    height = this.e.text[LineNo].height;
    lflags = this.e.text[LineNo].flags;
    InfoFlags = !this.True(this.e.text[LineNo].tabw) ? 0 : this.e.text[LineNo].tabw.type;
    return true;
  }

  internal int TerGetLineParam(int LineNo, int type)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (LineNo < this.e.TotalLines)
    {
      if (LineNo < 0)
        LineNo = this.e.CurLine;
      switch (type)
      {
        case 1:
          return this.e.text[LineNo].flags;
        case 2:
          return this.e.text[LineNo].flags2;
        case 3:
          return this.e.text[LineNo].len;
        case 4:
          return this.False(this.e.text[LineNo].tabw) || this.e.text[LineNo].tabw.ListTextWidth == 0 ? 9999 : this.e.text[LineNo].tabw.ListNbr;
        case 5:
          return this.False(this.e.text[LineNo].tabw) || this.e.text[LineNo].tabw.ListTextWidth == 0 ? 9999 : this.e.text[LineNo].tabw.ListFontId;
        case 6:
          return this.e.text[LineNo].cid;
        case 7:
          return this.e.text[LineNo].fid;
        case 8:
        case 9:
        case 10:
          int bltId = this.e.PfmtId[this.e.text[LineNo].pfmt].BltId;
          int ls = this.e.TerBlt[bltId].ls;
          if (bltId == 0)
            return 0;
          switch (type)
          {
            case 8:
              return this.e.ListOr[ls].ListIdx;
            case 9:
              return ls;
            case 10:
              return this.e.TerBlt[bltId].lvl;
          }
          break;
      }
    }
    return 9999;
  }

  internal string TerGetTextSel()
  {
    SelectionBlock selectionBlock = this.e.GetSelectionBlock();
    bool stretchHilight = this.e.StretchHilight;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.e.HilightType != 0)
    {
      if (!this.NormalizeBlock())
      {
        this.e.RestoreSelection(selectionBlock, false);
        this.e.StretchHilight = stretchHilight;
        return (string) null;
      }
      if (this.CopyToClipboard(629, false))
      {
        string textSel = new string(this.e.DlgChars);
        this.e.DlgChars = (char[]) null;
        this.e.RestoreSelection(selectionBlock, false);
        this.e.StretchHilight = stretchHilight;
        return textSel;
      }
    }
    this.e.RestoreSelection(selectionBlock, false);
    this.e.StretchHilight = stretchHilight;
    return (string) null;
  }

  internal bool TerInsertLine(
    string str,
    int FontId,
    int ParaId,
    int CellId,
    int ParaFID,
    bool repaint)
  {
    bool flag = false;
    int x = 0;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int length = str.Length;
    if (this.CheckLineLimit(this.e.TotalLines + 1))
    {
      char[] charArray = str.ToCharArray();
      this.MoveLineArrays(this.e.CurLine, 1, 'B');
      int curLine = this.e.CurLine;
      ++this.e.CurLine;
      this.e.CurCol = 0;
      this.LineAlloc(curLine, 0, length);
      char[] txt = this.e.text[curLine].txt;
      for (int index = 0; index < length; ++index)
        txt[index] = charArray[index];
      if (length >= 2 && txt[length - 2] == '\r')
      {
        if (this.e.TerArg.WordWrap)
        {
          txt[length - 2] = this.e.ParaChar;
          this.LineAlloc(curLine, length, length - 1);
        }
        else
          this.LineAlloc(curLine, length, length - 2);
        flag = true;
      }
      else if (length > 0 && charArray[length - 1] == '\u0015')
        flag = true;
      else if (length > 0 && (int) charArray[length - 1] == (int) this.e.CellChar)
        x |= 16 /*0x10*/;
      else if (length > 0 && charArray[length - 1] == '\u0012')
        x |= 32 /*0x20*/;
      this.e.text[curLine].fmt = (ushort[]) null;
      if (FontId >= 0 && FontId < this.e.TotalFonts && this.e.TerFont[FontId].InUse)
        this.e.text[curLine].UniFmt = (ushort) FontId;
      else if (this.e.InputFontId >= 0)
        this.e.text[curLine].UniFmt = (ushort) this.e.InputFontId;
      else
        this.e.text[curLine].UniFmt = (ushort) 0;
      if (ParaId >= 0 && ParaId < this.e.TotalPfmts)
        this.e.text[curLine].pfmt = ParaId;
      if (CellId >= 0 && CellId < this.e.TotalCells)
        this.e.text[curLine].cid = CellId;
      if (ParaFID >= 0 && ParaFID < this.e.TotalParaFrames)
        this.e.text[curLine].fid = ParaFID;
      if (this.True(x) && this.AllocTabw(curLine))
        this.e.text[curLine].tabw.type = x;
      if (flag || (x & 16 /*0x10*/) != 0)
        this.e.text[curLine].flags = 1;
      if (repaint)
      {
        this.e.WinHeight = this.e.TerWinHeight / this.e.TerFont[0].height;
        this.PaintTer();
      }
    }
    return true;
  }

  internal bool TerInsertText(string text, int FontId, int ParaId, bool repaint)
  {
    int[] pPfmt = (int[]) null;
    int[] numArray = new int[2];
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int inputFontId = this.e.InputFontId;
    if (FontId >= 0 && FontId < this.e.TotalFonts && this.e.TerFont[FontId].InUse)
    {
      if (ParaId >= 0 && ParaId < this.e.TotalPfmts && this.e.PfmtId[ParaId].StyId > 0)
        FontId = this.SetFontStyleId(FontId, this.e.TerFont[FontId].CharStyId, this.e.PfmtId[ParaId].StyId);
      this.e.InputFontId = FontId;
    }
    if (ParaId >= 0 && ParaId < this.e.TotalPfmts)
    {
      numArray[0] = ParaId;
      numArray[1] = -1;
      pPfmt = numArray;
    }
    int curLine = this.e.CurLine;
    this.InsertBuffer(text, (ushort[]) null, pPfmt, false);
    this.e.InputFontId = inputFontId;
    if (repaint)
    {
      this.e.WinHeight = this.e.TerWinHeight / this.e.TerFont[0].height;
      this.PaintTer();
    }
    else if (this.e.TerArg.WordWrap)
      this.WordWrap(curLine, this.e.CurLine - curLine + 1);
    return true;
  }

  internal bool TerInternetGet(string url, string OutFile)
  {
    WebClient webClient = new WebClient();
    bool flag = true;
    try
    {
      webClient.DownloadFile(url, OutFile);
    }
    catch (Exception ex)
    {
      flag = false;
    }
    webClient.Dispose();
    return flag;
  }

  internal new bool TerNew(string file)
  {
    char inputType = this.e.TerArg.InputType;
    if (this.e.TerArg.InputType != 'F')
    {
      this.e.TerArg.InputType = 'F';
      this.e.TerArg.hBuffer = (string) null;
      this.e.TerArg.BufferLen = 0;
    }
    if (tc.InIE && this.e.TerArg.modified == 99999)
      this.e.TerArg.modified = 0;
    DialogResult dialogResult;
    if (this.True(this.e.TerArg.modified) && (DialogResult.Cancel == (dialogResult = this.ShowMessage(this.e.MsgString[(int) sbyte.MaxValue], "", MessageBoxButtons.YesNoCancel)) || dialogResult == DialogResult.Yes && !(this.e.DocName.Length != 0 ? this.TerSave(this.e.DocName, true) : this.TerSaveAs(this.e.DocName))))
      return false;
    this.ReadTerFile(file);
    this.e.TerShrinkFontTable();
    this.e.TerArg.InputType = inputType;
    this.e.DocName = file;
    if ((this.e.TerFlags5 & 2) != 0 && this.e.Parent != null)
      this.e.Parent.Text = file == "" ? "Unknown" : file;
    this.e.Invalidate();
    if (tc.InIE)
      this.e.TerArg.modified = 99999;
    return true;
  }

  internal new bool TerOpen()
  {
    string file = "";
    int FilterIndex = this.e.UserFileType != 2 ? 1 : 2;
    string filter = "Text Format(*.TXT)|*.TXT|Rich Text Format(*.RTF)|*.RTF";
    if (!this.GetFileName(true, ref file, FilterIndex, filter, "rtf"))
      return false;
    return file == this.e.DocName || this.TerNew(file);
  }

  internal new bool TerRead(string InputFile)
  {
    bool flag1 = false;
    bool flag2 = false;
    OurStreamReader ourStreamReader1 = (OurStreamReader) null;
    int index1 = 0;
    char[] chArray1 = (char[]) null;
    char[] chArray2 = new char[1001];
    int num1 = 0;
    Cursor x = (Cursor) null;
    bool flag3 = true;
    int length = this.e.CfmtSign.Length;
    if (this.e.TerArg.InputType == 'B')
    {
      if (this.e.TerArg.hBuffer == null || this.e.TerArg.hBuffer != null && this.RtfRead(1, (string) null, this.e.TerArg.hBuffer, this.e.TerArg.BufferLen))
        return true;
      chArray1 = this.e.TerArg.hBuffer.ToCharArray();
      if (this.e.TerArg.BufferLen > chArray1.Length)
        this.e.TerArg.BufferLen = chArray1.Length;
      if (this.e.TerArg.delim == '\r')
        this.e.CrLfUsed = true;
      else
        this.e.CrLfUsed = false;
    }
    else
    {
      InputFile = InputFile.Trim();
      if (InputFile.Length == 0)
        return true;
      if (!System.IO.File.Exists(InputFile))
        return DialogResult.No != this.ShowMessage(this.e.MsgString[13], InputFile, MessageBoxButtons.YesNo);
      OurStreamReader ourStreamReader2;
      try
      {
        ourStreamReader2 = new OurStreamReader(InputFile);
      }
      catch (Exception ex)
      {
        return this.PrintError(28, nameof (TerRead));
      }
      ourStreamReader2.Close();
      if (this.RtfRead(0, InputFile, (string) null, 0))
        return true;
      ourStreamReader1 = new OurStreamReader(InputFile);
    }
    if ((this.e.TerFlags & 512 /*0x0200*/) == 0)
      x = Cursors.WaitCursor;
    for (int line = 0; line < this.e.TotalLines; ++line)
      this.init.FreeLine(line);
    this.e.TotalLines = 0;
    if (this.e.DefLang != 1033)
      num1 = this.SetCurLangFont2(0, this.e.DefInpLang);
    while (true)
    {
      char[] txt;
      do
      {
        int index2;
        do
        {
          int num2 = 0;
          if (this.e.TerArg.InputType == 'F')
          {
            switch (ourStreamReader1.ReadLine(chArray2, 1000))
            {
              case -1:
                this.PrintError(29, nameof (TerRead));
                goto label_73;
              case 0:
                goto label_73;
            }
          }
          else if (index1 < this.e.TerArg.BufferLen)
          {
            int index3;
            for (index3 = num2; index1 < this.e.TerArg.BufferLen && (int) chArray1[index1] != (int) this.e.TerArg.delim && index3 < 998; ++index1)
            {
              chArray2[index3] = chArray1[index1];
              ++index3;
            }
            chArray2[index3] = char.MinValue;
            if (index1 < this.e.TerArg.BufferLen && (int) chArray1[index1] == (int) this.e.TerArg.delim)
            {
              chArray2[index3] = '\n';
              ++index3;
              chArray2[index3] = char.MinValue;
            }
            if (flag3)
            {
              if (this.e.TerArg.delim == '\r' && index1 < this.e.TerArg.BufferLen && (int) chArray1[index1] == (int) this.e.TerArg.delim && index1 + 1 < this.e.TerArg.BufferLen && chArray1[index1 + 1] != '\n')
                this.e.CrLfUsed = false;
              flag3 = false;
            }
            if (this.e.CrLfUsed && index1 < this.e.TerArg.BufferLen && (int) chArray1[index1] == (int) this.e.TerArg.delim && index1 + 1 < this.e.TerArg.BufferLen && chArray1[index1 + 1] == '\n')
              ++index1;
            if (index3 > 0 && chArray2[index3 - 1] == '\n')
              ++index1;
          }
          else
            goto label_73;
          index2 = this.lstrlen(chArray2);
          bool flag4 = false;
          if (index2 > 0 && chArray2[index2 - 1] == '\n')
          {
            if (this.e.TerArg.WordWrap)
              flag4 = true;
            --index2;
          }
          chArray2[index2] = char.MinValue;
          if (index2 == length && this.e.CfmtSign == new string(chArray2, 0, index2))
          {
            flag1 = true;
            goto label_73;
          }
          if (flag4)
          {
            chArray2[index2] = this.e.ParaChar;
            ++index2;
            chArray2[index2] = char.MinValue;
          }
        }
        while (flag2);
        if (index2 > 1000)
        {
          this.PrintError(86, nameof (TerRead));
          index2 = 999;
        }
        if (!this.CheckLineLimit(this.e.TotalLines + 1))
        {
          this.e.HoldMessages = true;
          this.PrintError(86, nameof (TerRead));
          this.e.HoldMessages = false;
          flag2 = true;
        }
        else
        {
          ++this.e.TotalLines;
          this.e.CurLine = this.e.TotalLines - 1;
          this.InitLine(this.e.CurLine);
          if (index2 == 1)
          {
            if (chArray2[0] == '\u0012' || (int) chArray2[0] == (int) this.e.CellChar)
            {
              this.AllocTabw(this.e.CurLine);
              if (this.True(this.e.text[this.e.CurLine].tabw))
              {
                if (chArray2[0] == '\u0012')
                  this.e.text[this.e.CurLine].tabw.type |= 32 /*0x20*/;
                if ((int) chArray2[0] == (int) this.e.CellChar)
                  this.e.text[this.e.CurLine].tabw.type |= 16 /*0x10*/;
              }
            }
            this.SetHdrFtrLineFlags(this.e.CurLine, chArray2[0]);
          }
          this.LineAlloc(this.e.CurLine, 0, index2);
          txt = this.e.text[this.e.CurLine].txt;
          if (index2 > 0)
            this.FarMove(chArray2, txt, index2);
        }
      }
      while (this.e.DefLang == 1033 || this.e.text[this.e.CurLine].fmt != null || this.e.FileFormat != 0);
      ushort[] numArray = this.OpenCfmt(this.e.CurLine);
      for (int col = 0; col < this.e.text[this.e.CurLine].len; ++col)
      {
        if (!this.IsEnglishChar(txt, col, this.e.text[this.e.CurLine].len))
          numArray[col] = (ushort) num1;
      }
      this.CloseCfmt(this.e.CurLine);
    }
label_73:
    if (this.e.TotalLines == 0)
    {
      this.e.TotalLines = 1;
      this.InitLine(0);
    }
    if (this.e.TerArg.InputType == 'F')
      ourStreamReader1.Close();
    int num3 = flag1 ? 1 : 0;
    if (this.e.TotalLines == 1 && this.e.text[0].len == 0)
      this.e.FileFormat = 2;
    this.e.CurLine = this.e.CurRow = this.e.CurCol = 0;
    this.e.PageModifyCount = -1;
    if (this.e.TerArg.WordWrap)
    {
      bool flag5 = false;
      this.e.InRtfRead = true;
      this.e.TerOpFlags2 |= 16 /*0x10*/;
      if (this.e.TerArg.PageMode)
        this.Repaginate(false, false, 0, true);
      else
        this.WordWrap(0, this.e.TotalLines);
      if (this.e.TerArg.PageMode)
      {
        for (int index4 = 1; index4 < this.e.TotalParaFrames; ++index4)
        {
          if (this.e.ParaFrame[index4].InUse)
          {
            flag5 = true;
            break;
          }
        }
      }
      this.ReposPageHdrFtr(false);
      if (flag5 && !this.e.ViewPageHdrFtr && this.e.TerArg.PageMode && (this.e.TerFlags2 & 262144 /*0x040000*/) == 0)
      {
        bool paintEnabled = this.e.PaintEnabled;
        this.e.PaintEnabled = false;
        this.ToggleViewHdrFtr();
        this.e.PaintEnabled = paintEnabled;
      }
      if (flag5)
        this.e.TerRepaginate(true);
      if (this.e.ViewPageHdrFtr && this.e.TerArg.PageMode)
      {
        bool flag6 = false;
        for (int index5 = 0; index5 < this.e.TotalLines; ++index5)
        {
          if ((this.e.PfmtId[this.e.text[index5].pfmt].flags & 4096 /*0x1000*/) != 0 && (this.e.text[index5].flags & 655360 /*0x0A0000*/) == 0)
          {
            if (!flag6)
              this.e.CurLine = index5;
            flag6 = true;
            if (index5 > 0 && (this.e.text[index5 - 1].flags & 131072 /*0x020000*/) != 0)
            {
              this.e.CurLine = index5;
              break;
            }
          }
          if ((this.e.PfmtId[this.e.text[index5].pfmt].flags & 12288 /*0x3000*/) == 0)
            break;
        }
      }
      if (flag5)
      {
        this.e.TerRepaginate(true);
        this.e.RepageBeginLine = 0;
      }
      this.e.InRtfRead = false;
      this.e.TerOpFlags2 &= -17;
    }
    this.UpdateToolBar(true);
    this.e.TerArg.modified = 0;
    this.e.RepageBeginLine = 0;
    this.e.PageModifyCount = this.e.TerArg.modified - 1;
    if ((this.e.TerFlags5 & 2) != 0)
      this.e.Parent.Text = InputFile;
    if (this.True(x))
      this.e.Cursor = x;
    return true;
  }

  internal new bool TerSave(string OutFile, bool ToFile)
  {
    StreamWriter streamWriter = (StreamWriter) null;
    int num1 = 0;
    int num2 = 0;
    string str1 = "";
    List<char> charList = (List<char>) null;
    Cursor cursor = (Cursor) null;
    char ch = char.MinValue;
    if (ToFile && this.e.TerArg.InputType != 'F')
    {
      ch = this.e.TerArg.InputType;
      this.e.TerArg.InputType = 'F';
      this.e.TerArg.hBuffer = (string) null;
      this.e.TerArg.BufferLen = 0;
    }
    this.RecreateSections();
    int num3 = this.e.TerArg.SaveFormat != 3 ? this.e.TerArg.SaveFormat : this.e.FileFormat;
    int num4 = num3 == 0 || num3 == 1 || num3 == 5 || num3 == 6 ? 1 : (num3 == 7 ? 1 : 0);
    if (this.e.TerArg.InputType == 'B')
    {
      this.e.TerArg.hBuffer = "";
      if (num3 == 2)
        return this.RtfWrite(1, (string) null, out this.e.TerArg.hBuffer);
      tc.arg_list terArg = this.e.TerArg;
      this.e.TerArg.BufferLen = 0;
      if (this.e.TerArg.delim != '\r')
        this.e.CrLfUsed = false;
      if (num3 == 5)
      {
        this.e.TerArg.BufferLen = 1;
        for (int index = 0; index < this.e.TotalLines; ++index)
          this.e.TerArg.BufferLen += this.e.text[index].len + 1;
      }
      else
      {
        for (int index = 0; index < this.e.TotalLines; ++index)
          this.e.TerArg.BufferLen += this.e.text[index].len + 1;
        if (this.e.CrLfUsed)
          this.e.TerArg.BufferLen += this.e.TotalLines;
      }
      charList = new List<char>(this.e.TerArg.BufferLen + 1);
    }
    else
    {
      if (OutFile == null || OutFile == "")
      {
        string filter = "ASCII Text Format(*.TXT)|*.TXT|Text with Line Breaks|*.TXT|Rich Text Format(*.RTF)|*.RTF|" + "Unicode Text Format(*.TXT)|*.TXT|" + "UTF7 Text Format(*.TXT)|*.TXT|" + "UTF8 Text Format(*.TXT)|*.TXT|";
        if (this.HtmlAddOnFound())
          filter += "HTML Format(*.HTM)|*.HTM|";
        if (this.False(this.GetFileName(false, ref OutFile, 1, filter, "RTF")))
          return false;
        this.e.DocName = OutFile;
        num3 = this.e.TerArg.SaveFormat != 3 ? this.e.TerArg.SaveFormat : this.e.FileFormat;
      }
      if (this.e.TerArg.SaveFormat == 4)
        return this.LoadHtmlAddOn() && this.HtsSaveFromTer(true, OutFile, out tc.SkipStr);
      if (System.IO.File.Exists(OutFile))
      {
        if ((System.IO.File.GetAttributes(OutFile) & FileAttributes.ReadOnly) != (FileAttributes) 0)
          return this.PrintError(163, nameof (TerSave));
        if ((this.e.TerFlags2 & 2048 /*0x0800*/) != 0)
        {
          System.IO.File.Delete(OutFile);
        }
        else
        {
          string str2 = OutFile;
          int num5 = str2.Length - 1;
          while (num5 >= 0 && num5 >= str2.Length - 4 && str2[num5] != '.' && str2[num5] != '\\')
            --num5;
          if (num5 >= 0 && str2[num5] == '.')
            str2 = str2.Substring(0, num5);
          str1 = str2 + ".BU";
          if (System.IO.File.Exists(str1))
            System.IO.File.Delete(str1);
          System.IO.File.Move(OutFile, str1);
        }
      }
      if (num3 == 2)
      {
        int num6 = this.RtfWrite(0, OutFile, out tc.SkipStr) ? 1 : 0;
        if (ch == char.MinValue)
          return num6 != 0;
        this.e.TerArg.InputType = ch;
        return num6 != 0;
      }
      num2 = 0;
      try
      {
        Encoding encoding = Encoding.ASCII;
        if (num3 == 5)
          encoding = Encoding.Unicode;
        if (num3 == 6)
          encoding = Encoding.UTF7;
        if (num3 == 7)
          encoding = Encoding.UTF8;
        streamWriter = new StreamWriter(OutFile, false, encoding);
      }
      catch (Exception ex)
      {
        return this.PrintError(28, nameof (TerSave));
      }
    }
    if ((this.e.TerFlags & 512 /*0x0200*/) == 0 && this.e.TerArg.InputType == 'F')
    {
      cursor = this.e.Cursor;
      this.e.Cursor = Cursors.WaitCursor;
    }
    if (num3 == 5 || num3 == 6 || num3 == 7)
      num3 = 0;
    if (num3 == 1)
      this.e.TerRewrap();
    for (int index1 = 0; index1 < this.e.TotalLines; ++index1)
    {
      char[] txt = this.e.text[index1].txt;
      if (this.e.TerArg.InputType == 'B')
      {
        char[] chArray = new char[this.e.text[index1].len];
        this.FarMove(txt, 0, chArray, 0, this.e.text[index1].len);
        charList.AddRange((IEnumerable<char>) chArray);
        if (this.e.text[index1].tag != null)
        {
          for (int index2 = 0; index2 < this.e.text[index1].len; ++index2)
          {
            if (this.e.text[index1].tag[index2] != (ushort) 0 && (this.e.CharTag[(int) this.e.text[index1].tag[index2]].type == 78 || this.e.CharTag[(int) this.e.text[index1].tag[index2]].type == 79 || this.e.CharTag[(int) this.e.text[index1].tag[index2]].type == 80 /*0x50*/))
            {
              string auxText = this.e.CharTag[(int) this.e.text[index1].tag[index2]].AuxText;
              if (auxText != null && auxText.Length > 0)
              {
                if (auxText.Length == 1)
                {
                  charList[num1 + index2] = auxText[0];
                }
                else
                {
                  charList.RemoveAt(num1 + index2);
                  charList.InsertRange(num1 + index2, (IEnumerable<char>) auxText);
                  num1 += auxText.Length - 1;
                }
              }
              else if (num1 + index2 < charList.Count)
              {
                charList.RemoveAt(num1 + index2);
                --num1;
              }
            }
          }
        }
        num1 += this.e.text[index1].len;
        if (num1 > charList.Count)
          num1 = charList.Count - 1;
        if (num3 == 1 && this.e.TerArg.WordWrap && this.e.text[index1].len > 0)
        {
          char chr = charList[num1 - 1];
          if ((int) chr == (int) this.e.ParaChar || (int) chr == (int) this.e.CellChar || chr == '\u000F' || this.IsBreakChar(chr) && chr != '\f')
            --num1;
        }
        if (num3 != 1 && this.e.TerArg.WordWrap)
        {
          if (this.e.text[index1].len > 0 && num1 > 0)
          {
            char chr = charList[num1 - 1];
            if ((int) chr == (int) this.e.ParaChar || chr == '\u000F' || (int) chr == (int) this.e.CellChar || this.IsBreakChar(chr) && chr != '\f')
            {
              charList[num1 - 1] = this.e.TerArg.delim;
              if (this.e.CrLfUsed)
              {
                charList.Insert(num1, '\n');
                ++num1;
              }
            }
          }
        }
        else
        {
          charList.Insert(num1, this.e.TerArg.delim);
          ++num1;
          if (this.e.CrLfUsed)
          {
            charList.Insert(num1, '\n');
            ++num1;
          }
        }
      }
      else
      {
        char[] chArray = new char[this.e.text[index1].len + 2];
        this.FarMove(txt, 0, chArray, 0, this.e.text[index1].len);
        int count = this.e.text[index1].len;
        for (int index3 = 0; index3 < count; ++index3)
        {
          if (chArray[index3] == char.MinValue)
            chArray[index3] = '\u0001';
        }
        if (num3 == 1 && this.e.TerArg.WordWrap && count > 0)
        {
          char chr = chArray[count - 1];
          if ((int) chr == (int) this.e.ParaChar || (int) chr == (int) this.e.CellChar || chr == '\u000F' || this.IsBreakChar(chr) && chr != '\f')
            --count;
        }
        if (this.e.TerArg.WordWrap && num3 != 1)
        {
          if (count > 0)
          {
            char chr = chArray[count - 1];
            if ((int) chr == (int) this.e.ParaChar || chr == '\u000F' || (int) chr == (int) this.e.CellChar || this.IsBreakChar(chr) && chr != '\f')
            {
              chArray[count - 1] = '\r';
              chArray[count] = '\n';
              ++count;
              num2 += count;
            }
          }
        }
        else
        {
          chArray[count] = '\r';
          int index4 = count + 1;
          chArray[index4] = '\n';
          count = index4 + 1;
          num2 += count;
        }
        try
        {
          streamWriter.Write(chArray, 0, count);
        }
        catch (Exception ex)
        {
          this.PrintError(31 /*0x1F*/, nameof (TerSave));
          streamWriter.Close();
          if (str1 != "" && System.IO.File.Exists(str1))
            System.IO.File.Move(str1, OutFile);
          if (cursor != (Cursor) null)
            this.e.Cursor = cursor;
          return false;
        }
      }
    }
    if (this.e.TerArg.InputType == 'F')
      streamWriter.Close();
    if (this.e.TotalLines == 0)
    {
      this.InitLine(0);
      this.e.TotalLines = 1;
    }
    this.e.TerArg.modified = this.e.PageModifyCount = 0;
    this.e.Notified = false;
    if (this.e.TerArg.InputType == 'B')
    {
      this.e.TerArg.BufferLen = num1;
      if (this.e.TerArg.BufferLen == 0)
      {
        this.e.TerArg.hBuffer = new string(this.e.TerArg.delim, 1);
        this.e.TerArg.BufferLen = 1;
        if (this.e.CrLfUsed)
        {
          this.e.TerArg.hBuffer += new string('\n', 1);
          ++this.e.TerArg.BufferLen;
        }
      }
      else
        this.e.TerArg.hBuffer = new string(charList.ToArray(), 0, num1);
    }
    else
    {
      if ((this.e.TerFlags5 & 2) != 0)
        this.e.Parent.Text = OutFile;
      if (cursor != (Cursor) null)
        this.e.Cursor = cursor;
    }
    return true;
  }

  internal new bool TerSaveAs(string OutFile)
  {
    string filter = "ASCII Text Format(*.TXT)|*.TXT|Text with Line Breaks|*.TXT|Rich Text Format(*.RTF)|*.RTF|" + "Unicode Text Format(*.TXT)|*.TXT|" + "UTF7 Text Format(*.TXT)|*.TXT|" + "UTF8 Text Format(*.TXT)|*.TXT|";
    if (this.HtmlAddOnFound())
      filter += "HTML Format(*.HTM)|*.HTM|";
    if (this.False(this.GetFileName(false, ref OutFile, 1, filter, "RTF")))
      return false;
    this.e.DocName = OutFile;
    if (this.e.TerArg.SaveFormat != 4)
      return this.TerSave(OutFile, true);
    return this.LoadHtmlAddOn() && this.HtsSaveFromTer(true, OutFile, out tc.SkipStr);
  }

  internal bool TerSetDefDir(string DefDir, int type)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    this.e.UserDir = DefDir;
    this.e.UserFileType = type;
    return true;
  }

  internal bool TerSetLine(int LineNo, string text, int[] font)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (LineNo == this.e.TotalLines)
    {
      if (!this.CheckLineLimit(this.e.TotalLines + 1))
        return this.PrintError(88, "SetTerLine");
      ++this.e.TotalLines;
      this.InitLine(LineNo);
      ++this.e.TerArg.modified;
    }
    int NewSize = text.Length;
    if (LineNo >= 0 && LineNo < this.e.TotalLines)
    {
      if (NewSize < 0)
        NewSize = 0;
      int len = this.e.text[LineNo].len;
      this.LineAlloc(LineNo, this.e.text[LineNo].len, NewSize);
      if (NewSize > 0)
      {
        char[] txt = this.e.text[LineNo].txt;
        for (int index = 0; index < this.e.text[LineNo].len; ++index)
          txt[index] = text[index];
        ushort[] numArray = this.OpenCfmt(LineNo);
        if (NewSize > len)
        {
          for (int index = len; index < NewSize; ++index)
          {
            numArray[index] = len <= 0 ? (ushort) 0 : numArray[len - 1];
            if ((this.e.TerFont[(int) numArray[index]].style & 128 /*0x80*/) != 0)
              numArray[index] = (ushort) 0;
          }
        }
        if (font != null)
        {
          for (int index = 0; index < this.e.text[LineNo].len; ++index)
          {
            if (font[index] <= this.e.TotalFonts && this.e.TerFont[font[index]].InUse)
              numArray[index] = (ushort) font[index];
          }
        }
        this.CloseCfmt(LineNo);
        ++this.e.TerArg.modified;
      }
    }
    return true;
  }

  internal bool TerSetLinkPictDir(string PictDir)
  {
    this.e.LinkPictDir = PictDir;
    return true;
  }

  internal bool TerSetOutputFormat(int format)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (format != 3 && format != 0 && format != 1 && format != 2 && format != 4 && format != 5 || format == 4 && !this.HtmlAddOnFound())
      return false;
    this.e.TerArg.SaveFormat = format;
    ++this.e.TerArg.modified;
    this.e.Notified = true;
    return true;
  }

  internal bool TerSetWebFolder(string folder)
  {
    this.e.WebFolder = folder;
    return true;
  }
}
