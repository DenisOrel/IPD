// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.CBlk
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class CBlk : COp
{
  internal bool IsStrikedOut
  {
    get => (this.e.TerFont[this.GetCurCfmt(this.e.CurLine, this.e.CurCol)].style & 8) != 0;
    set
    {
      int curCfmt = this.GetCurCfmt(this.e.CurLine, this.e.CurCol);
      if (value)
        this.e.TerFont[curCfmt].style |= 8;
      else
        this.e.TerFont[curCfmt].style &= -9;
    }
  }

  internal bool IsDoubleStrikedOut
  {
    get
    {
      return (this.e.TerFont[this.GetCurCfmt(this.e.CurLine, this.e.CurCol)].style & 524288 /*0x080000*/) != 0;
    }
    set
    {
      int curCfmt = this.GetCurCfmt(this.e.CurLine, this.e.CurCol);
      if (value)
        this.e.TerFont[curCfmt].style |= 524288 /*0x080000*/;
      else
        this.e.TerFont[curCfmt].style &= -524289;
    }
  }

  internal CBlk(ImRtfEditor ImRtfEditor)
    : base(ImRtfEditor)
  {
  }

  internal new bool AllSelected() => this.AllSelected2(out tc.SkipBool);

  internal new bool AllSelected2(out bool LastCharSelected)
  {
    LastCharSelected = this.e.HilightEndCol >= this.e.text[this.e.HilightEndRow].len;
    return this.e.HilightBegRow == 0 && this.e.HilightEndRow == this.e.TotalLines - 1 && (this.e.HilightType == 1 || this.e.HilightBegCol == 0 && this.e.HilightEndCol >= this.e.text[this.e.HilightEndRow].len - 1) || this.e.HilightType == 2 && this.e.HilightBegRow == 0 && this.e.HilightBegCol == 0 && this.e.TotalLines > 0 && this.e.text[this.e.TotalLines - 1].len == 1 && this.e.HilightEndRow == this.e.TotalLines - 2 && this.e.HilightEndCol >= this.e.text[this.e.HilightEndRow].len;
  }

  internal bool AllSelected(int begRow, int begCol, int endRow, int endCol, int hilightType)
  {
    return begRow == 0 && endRow == this.e.TotalLines - 1 && (hilightType == 1 || begCol == 0 && endCol >= this.e.text[endRow].len - 1) || hilightType == 2 && begRow == 0 && begCol == 0 && this.e.TotalLines > 0 && this.e.text[this.e.TotalLines - 1].len == 1 && endRow == this.e.TotalLines - 2 && endCol >= this.e.text[endRow].len;
  }

  private bool ApplyPrevPictProp(int pict, tc.StrFont pPrevPict)
  {
    if ((pPrevPict.style & 128 /*0x80*/) != 0)
    {
      this.e.TerFont[pict].style = pPrevPict.style;
      this.e.TerFont[pict].FrameType = pPrevPict.FrameType;
      this.e.TerFont[pict].ParaFID = pPrevPict.ParaFID;
      this.e.TerFont[pict].FieldId = pPrevPict.FieldId;
      this.e.TerFont[pict].AuxId = pPrevPict.AuxId;
      this.e.TerFont[pict].Aux1Id = pPrevPict.Aux1Id;
      this.e.TerFont[pict].MapId = pPrevPict.MapId;
      this.e.TerFont[pict].DispFrame = pPrevPict.DispFrame;
    }
    return true;
  }

  internal new bool BlockHasProtectOn(bool msg, bool SkipFieldNames)
  {
    int hilightBegRow = this.e.HilightBegRow;
    int hilightEndRow = this.e.HilightEndRow;
    int col1 = this.e.HilightBegCol;
    int col2 = this.e.HilightEndCol;
    if (this.e.HilightType != 2)
    {
      col1 = 0;
      col2 = this.e.text[hilightEndRow].len;
    }
    return this.BlockHasProtectOn2(hilightBegRow, col1, hilightEndRow, col2, msg, SkipFieldNames);
  }

  internal new bool BlockHasProtectOn2(
    int line1,
    int col1,
    int line2,
    int col2,
    bool msg,
    bool SkipFieldNames)
  {
    if (this.e.IsProtectedZone(this.pos.TerRowColToAbs(line1, col1), true) || this.e.IsProtectedZone(this.pos.TerRowColToAbs(line2, col2), true))
      return true;
    for (int line = line1; line <= line2; ++line)
    {
      if ((this.e.PfmtId[this.e.text[line].pfmt].pflags & 128 /*0x80*/) == 0)
      {
        if (!SkipFieldNames && this.e.text[line].fmt == null && line != line2)
        {
          int uniFmt = (int) this.e.text[line].UniFmt;
          if ((this.e.TerFont[uniFmt].style & 512 /*0x0200*/) != 0 && ((this.e.TerFont[uniFmt].style & 39936) == 0 || col2 >= this.e.text[line2].len || (this.e.TerFont[this.GetCurCfmt(line2, col2)].style & 39936) != 0))
          {
            if (msg)
              this.PrintError(32 /*0x20*/, this.e.MsgString[123]);
            return true;
          }
        }
        else
        {
          int num1 = 0;
          int num2 = this.e.text[line].len;
          if (line == line1 && col1 < this.e.text[line].len)
            num1 = col1;
          if (line == line2 && col2 <= this.e.text[line].len)
            num2 = col2;
          ushort[] numArray = this.OpenCfmt(line);
          for (int index1 = num1; index1 < num2; ++index1)
          {
            int index2 = (int) numArray[index1];
            if (SkipFieldNames && this.e.TerFont[index2].FieldId == 6)
            {
              switch (this.e.text[line].txt[index1])
              {
                case '{':
                case '}':
                  continue;
              }
            }
            if ((this.e.TerFont[index2].style & 512 /*0x0200*/) != 0 || (this.e.TerFlags5 & 536870912 /*0x20000000*/) != 0 && (this.e.TerFont[index2].FieldId == 6 || this.e.TerFont[index2].FieldId == 7))
            {
              this.CloseCfmt(line);
              if ((this.e.TerFont[index2].style & 39936) == 0 || col2 >= this.e.text[line2].len || (this.e.TerFont[this.GetCurCfmt(line2, col2)].style & 39936) != 0)
              {
                if (msg)
                  this.PrintError(32 /*0x20*/, this.e.MsgString[123]);
                return true;
              }
            }
          }
          this.CloseCfmt(line);
        }
      }
    }
    return false;
  }

  internal string BuildPictFileName()
  {
    int hashCode = Thread.CurrentThread.GetHashCode();
    int num = 1;
    string path;
    while (true)
    {
      path = $"{(this.e.InServer ? ".\\" : this.e.PictDir)}{hashCode.ToString()}{num.ToString()}.tmp";
      if (File.Exists(path))
        ++num;
      else
        break;
    }
    return path;
  }

  internal new bool CheckWindowOverflow()
  {
    int docHeight = this.GetDocHeight();
    if (docHeight >= this.e.DocHeight && docHeight > this.e.TerWinHeight && this.e.UndoCount > 0 && this.e.undo[this.e.UndoCount - 1].type != 'D')
    {
      this.MessageBeep(0);
      this.TerPageUp(false);
      if (this.e.UseWin)
        this.PostMessage(this.e.hTerWnd, 2737, 638, 0);
      else
        this.mnu.ProcessCommand(638);
    }
    return true;
  }

  internal new bool CopyFromClipboard(string format, DataObject data)
  {
    bool flag = false;
    string format1 = "";
    if (data == null)
      data = (DataObject) Clipboard.GetDataObject();
    if (data != null)
    {
      if (format == "" && !data.GetDataPresent(DataFormats.Rtf))
      {
        if (data.GetDataPresent(DataFormats.EnhancedMetafile))
          format1 = DataFormats.EnhancedMetafile;
        else if (data.GetDataPresent(DataFormats.Bitmap))
          format1 = DataFormats.Bitmap;
        else if (data.GetDataPresent(DataFormats.MetafilePict))
          format1 = DataFormats.MetafilePict;
        if (format1 != "")
          return this.TerPastePicture(format1, (Image) null, 0, 0, true) > 0;
      }
      if (!this.CanInsert(this.e.CurLine, this.e.CurCol))
      {
        this.MessageBeep(0);
        return true;
      }
      if (this.e.HilightType == 2 && !this.e.InOleDrag)
      {
        this.TerDeleteBlock(true);
        --this.e.UndoRef;
      }
      string str = (string) null;
      if (format == DataFormats.UnicodeText)
        str = (string) data.GetData(format, true);
      else if (format == DataFormats.Rtf)
      {
        str = (string) data.GetData(format, true);
        flag = true;
      }
      else if (data.GetDataPresent(DataFormats.Rtf))
      {
        str = (string) data.GetData(DataFormats.Rtf, true);
        flag = true;
      }
      else if (data.GetDataPresent(DataFormats.UnicodeText, true))
      {
        str = (string) data.GetData(DataFormats.UnicodeText, true);
      }
      else
      {
        foreach (string format2 in data.GetFormats())
        {
          if (format2 == "System.Windows.Forms.TreeNode")
          {
            str = ((TreeNode) data.GetData(format2, true)).Text;
            break;
          }
          try
          {
            str = ((Control) data.GetData(format2, true)).Text;
            break;
          }
          catch
          {
          }
        }
        if (str == null)
        {
          foreach (string format3 in data.GetFormats())
          {
            object data1 = data.GetData(format3, true);
            try
            {
              str = data1.ToString();
              if (str.Length > 0)
                break;
            }
            catch (Exception ex)
            {
            }
          }
        }
      }
      if (str == null)
      {
        this.PrintError(11, "");
        return true;
      }
      this.e.ClipTblLevel = -1;
      this.e.ClipEmbTable = false;
      if (this.True(this.e.text[this.e.CurLine].cid))
        this.e.ClipTblLevel = this.e.cell[this.e.text[this.e.CurLine].cid].level;
      tc.ClsClipInfo clsClipInfo = (tc.ClsClipInfo) null;
      if (data.GetDataPresent("SSClipInfo"))
      {
        try
        {
          clsClipInfo = (tc.ClsClipInfo) data.GetData("SSClipInfo");
        }
        catch (Exception ex)
        {
        }
      }
      if (clsClipInfo != null && clsClipInfo.size == 12)
      {
        this.e.ClipTblLevel = clsClipInfo.TblLevel;
        this.e.ClipEmbTable = clsClipInfo.EmbTable;
      }
      if (this.e.CurCol >= this.e.text[this.e.CurLine].len)
        this.e.CurCol = this.e.text[this.e.CurLine].len;
      if (this.e.CurCol < 0)
        this.e.CurCol = 0;
      if (flag && (this.e.TerFlags3 & 16777216 /*0x01000000*/) == 0)
      {
        this.e.TerOpFlags2 |= 64 /*0x40*/;
        this.e.HilightType = 0;
        this.e.InsertRtfBuf(str, this.e.CurLine, this.e.CurCol, false);
        this.e.TerOpFlags2 &= -65;
      }
      else
      {
        this.e.InputFontId = this.GetEffectiveCfmt();
        this.InsertTerText(str, false);
      }
      this.e.ClipTblLevel = 1;
      this.e.ClipEmbTable = true;
      if (this.e.CurLine >= this.e.TotalLines)
        --this.e.CurLine;
      if (this.e.CurLine - this.e.BeginLine >= this.e.WinHeight || this.e.CurLine - this.e.BeginLine < 0)
      {
        this.e.BeginLine = this.e.CurLine - this.e.WinHeight / 2;
        if (this.e.BeginLine < 0)
          this.e.BeginLine = 0;
      }
      this.e.CurRow = this.e.CurLine - this.e.BeginLine;
      this.e.HilightType = 0;
      this.e.StretchHilight = false;
      this.e.PaintFlag = 4;
      this.PaintTer();
    }
    return true;
  }

  internal bool CopyLineToClipBuf(
    int line,
    int BegCol,
    int EndCol,
    char[] pClpText,
    ref int StartIndex)
  {
    int DestIdx = StartIndex;
    bool flag = false;
    if (!this.LineInfo(line, 32 /*0x20*/))
    {
      if (this.e.text[line].fmt == null && !this.edit.HiddenText((int) this.e.text[line].UniFmt))
        flag = true;
      char[] txt = this.e.text[line].txt;
      int count;
      if (flag)
      {
        count = EndCol - BegCol + 1;
        if (count > 0)
          this.FarMove(txt, BegCol, pClpText, DestIdx, count);
      }
      else
      {
        ushort[] numArray = this.OpenCfmt(line);
        count = 0;
        int num = 0;
        for (int index = BegCol; index <= EndCol && index < this.e.text[line].len; ++index)
        {
          if ((int) numArray[index] == num || !this.edit.HiddenText((int) numArray[index]) && (this.e.TerFont[(int) numArray[index]].style & 128 /*0x80*/) == 0)
          {
            char ch = txt[index];
            if (ch == '\u000E')
              ch = ' ';
            pClpText[DestIdx + count] = ch;
            ++count;
            num = (int) numArray[index];
          }
        }
        this.CloseCfmt(line);
      }
      int index1 = DestIdx + count;
      if (this.e.TerArg.WordWrap)
      {
        int num = index1;
        if (index1 > 0 && pClpText[index1 - 1] == '\u0014')
          --index1;
        if (index1 > 0 && ((int) pClpText[index1 - 1] == (int) this.e.ParaChar || (int) pClpText[index1 - 1] == (int) this.e.CellChar || pClpText[index1 - 1] == '\u000F'))
        {
          --index1;
          if ((int) pClpText[index1] == (int) this.e.CellChar && line + 1 < this.e.TotalLines && !this.LineInfo(line + 1, 32 /*0x20*/))
          {
            pClpText[index1] = '\t';
            ++index1;
          }
        }
        if (index1 < num)
          index1 = this.AddCrLf(index1, pClpText, (ushort[]) null);
      }
      else
        index1 = this.AddCrLf(index1, pClpText, (ushort[]) null);
      pClpText[index1] = char.MinValue;
      StartIndex = index1;
    }
    return true;
  }

  internal new bool CopyToClipboard(int CmdId, bool ToCB)
  {
    int num = 0;
    int StartIndex = 0;
    DataObject data = new DataObject();
    if (this.IsProtected(true, CmdId == 628) || this.e.HilightType == 0 || !this.NormalizeBlock() || !this.NormalizeForFootnote())
      return false;
    if (this.e.HilightType == 1)
    {
      this.e.HilightBegCol = 0;
      this.e.HilightEndCol = this.e.text[this.e.HilightEndRow].len;
      if (this.e.HilightEndCol < 0)
        this.e.HilightEndCol = 0;
    }
    for (int hilightBegRow = this.e.HilightBegRow; hilightBegRow <= this.e.HilightEndRow; ++hilightBegRow)
      num = num + this.e.text[hilightBegRow].len + 2;
    char[] chArray = new char[num + 1];
    if (this.e.HilightBegRow == this.e.HilightEndRow)
    {
      this.CopyLineToClipBuf(this.e.HilightBegRow, this.e.HilightBegCol, this.e.HilightEndCol - 1, chArray, ref StartIndex);
    }
    else
    {
      this.CopyLineToClipBuf(this.e.HilightBegRow, this.e.HilightBegCol, this.e.text[this.e.HilightBegRow].len - 1, chArray, ref StartIndex);
      int LineNo = this.e.HilightBegRow;
      for (int index = this.e.HilightBegRow + 1; index < this.e.HilightEndRow; ++index)
      {
        if (this.LineSelected(index))
        {
          if (this.True(this.e.text[index].cid) && this.True(this.e.text[LineNo].cid))
          {
            int cid = this.e.text[LineNo].cid;
            if (this.e.cell[this.e.text[index].cid].row != this.e.cell[cid].row && !this.LineInfo(LineNo, 32 /*0x20*/))
              StartIndex = this.AddCrLf(StartIndex, chArray, (ushort[]) null);
          }
          this.CopyLineToClipBuf(index, 0, this.e.text[index].len - 1, chArray, ref StartIndex);
          LineNo = index;
        }
      }
      this.CopyLineToClipBuf(this.e.HilightEndRow, 0, this.e.HilightEndCol - 1, chArray, ref StartIndex);
    }
    chArray[StartIndex] = char.MinValue;
    if (ToCB)
    {
      data.SetData(DataFormats.UnicodeText, true, (object) new string(chArray, 0, StartIndex));
      this.RtfWrite(2, "", out tc.SkipStr);
      if (this.e.RtfClipData != null)
        data.SetData(DataFormats.Rtf, true, (object) new string(this.e.RtfClipData));
      if (this.e.ClipInfo != null)
        data.SetData("SSClipInfo", (object) this.e.ClipInfo);
      try
      {
        Clipboard.SetDataObject((object) data, true);
        if (CmdId == 628)
          this.TerDeleteBlock(true);
      }
      catch (Exception ex)
      {
      }
      this.e.PaintFlag = 4;
      this.PaintTer();
    }
    else
      this.e.DlgChars = this.ReAlloc(chArray, StartIndex);
    return true;
  }

  internal new bool DeleteCharBlock(bool SetCurPos, bool repaint)
  {
    bool flag = false;
    bool LastCharSelected = false;
    if (this.e.HilightType == 0)
      return true;
    if (this.e.FullRenderMode && !this.e.IsPlaneText && (this.e.TerOpFlags & 8) == 0)
    {
      if ((this.e.TerFlags & 256 /*0x0100*/) == 0)
      {
        int curCfmt = this.GetCurCfmt(this.e.HilightBegRow, this.e.HilightBegCol);
        int prevCfmt = this.GetPrevCfmt(this.e.HilightEndRow, this.e.HilightEndCol);
        if (this.e.TerFont[curCfmt].FieldId == 9 && this.e.TerFont[prevCfmt].FieldId == 9 && DialogResult.No == this.ShowMessage(this.e.MsgString[165], " ", MessageBoxButtons.YesNo))
          return false;
      }
      if ((this.e.TerOpFlags2 & 4096 /*0x1000*/) == 0 && (!this.NormalizeBlock() || !this.NormalizeForFootnote()))
        return true;
      int level = this.tbl.MinTableLevel(this.e.HilightBegRow, this.e.HilightEndRow);
      if ((this.e.TerFlags3 & 256 /*0x0100*/) != 0 && this.e.text[this.e.HilightBegRow].cid > 0 && this.LevelCell(level, this.e.HilightBegRow) == this.LevelCell(level, this.e.HilightEndRow) && this.LineInfo(this.e.HilightEndRow, 16 /*0x10*/) && this.e.HilightEndCol >= this.e.text[this.e.HilightEndRow].len && this.e.HilightEndCol > 0)
        --this.e.HilightEndCol;
      flag = this.AllSelected2(out LastCharSelected);
      if (this.TableHilighted())
      {
        if ((this.e.TerOpFlags2 & 2048 /*0x0800*/) != 0)
        {
          this.e.TerDeleteCellText(1, false);
          goto label_37;
        }
        this.e.TerDeleteCells(1, false);
        goto label_37;
      }
      if (this.e.HilightBegRow == this.e.HilightEndRow && this.e.HilightEndCol <= this.e.HilightBegCol || this.e.TrackChanges && this.TrackDelBlock(this.e.HilightBegRow, this.e.HilightBegCol, this.e.HilightEndRow, this.e.HilightEndCol, true, repaint))
        return true;
      if ((this.e.TerFlags3 & 262144 /*0x040000*/) != 0)
        this.ud.SaveUndo(this.e.HilightBegRow, this.e.HilightBegCol, this.e.HilightEndRow, this.e.HilightEndCol - 1, 'D');
    }
    if (this.e.HilightBegRow == this.e.HilightEndRow)
    {
      if (this.e.text[this.e.HilightBegRow].len != 0 && this.e.HilightBegCol < this.e.text[this.e.HilightEndRow].len)
      {
        if (this.e.HilightEndCol > this.e.text[this.e.HilightEndRow].len)
          this.e.HilightEndCol = this.e.text[this.e.HilightEndRow].len;
        int count = this.e.HilightEndCol - this.e.HilightBegCol;
        if (this.e.HilightEndCol == this.e.text[this.e.HilightEndRow].len && this.e.HilightEndCol > this.e.HilightBegCol)
          this.e.text[this.e.HilightEndRow].flags &= -4;
        this.edit.MoveLineData(this.e.HilightBegRow, this.e.HilightBegCol, count, 'D');
        if (this.e.text[this.e.HilightBegRow].len == 0 && (this.e.TerArg.WordWrap || (this.e.TerOpFlags & 4) == 0))
        {
          this.MoveLineArrays(this.e.HilightBegRow, 1, 'D');
          if (this.e.CurLine > this.e.HilightBegRow)
            --this.e.CurLine;
        }
        if (this.e.TotalLines == 1 && this.e.text[0].len == 0)
        {
          this.CompressCfmt(0);
          this.e.text[0].fmt = (ushort[]) null;
          this.e.text[0].UniFmt = (ushort) 0;
        }
      }
    }
    else
    {
      int count = this.e.text[this.e.HilightEndRow].len - this.e.HilightEndCol;
      if (count < 0)
        count = 0;
      int NewSize = this.e.HilightBegCol + count;
      this.LineAlloc(this.e.HilightBegRow, this.e.text[this.e.HilightBegRow].len, NewSize);
      if (NewSize > 0 && count > 0)
      {
        this.MoveCharInfo(this.e.HilightEndRow, this.e.HilightEndCol, this.e.HilightBegRow, this.e.HilightBegCol, count);
        this.e.text[this.e.HilightBegRow].pfmt = this.e.text[this.e.HilightEndRow].pfmt;
        this.e.text[this.e.HilightBegRow].cid = this.e.text[this.e.HilightEndRow].cid;
        this.e.text[this.e.HilightBegRow].fid = this.e.text[this.e.HilightEndRow].fid;
        this.e.text[this.e.HilightBegRow].flags = this.e.text[this.e.HilightEndRow].flags;
        this.FreeTabw(this.e.HilightBegRow);
        this.e.text[this.e.HilightBegRow].tabw = this.e.text[this.e.HilightEndRow].tabw;
        this.e.text[this.e.HilightEndRow].tabw = (tc.ClsTabw) null;
      }
      else if (NewSize == 0)
      {
        this.e.text[this.e.HilightBegRow].fmt = (ushort[]) null;
        this.e.text[this.e.HilightBegRow].UniFmt = (ushort) 0;
      }
      this.MoveLineArrays(this.e.HilightBegRow + 1, this.e.HilightEndRow - this.e.HilightBegRow, 'D');
      if (this.e.TotalLines == 0)
      {
        this.e.TotalLines = 1;
        this.InitLine(0);
      }
    }
label_37:
    if (this.e.HilightBegRow >= this.e.TotalLines)
    {
      this.e.HilightBegRow = this.e.TotalLines - 1;
      this.e.HilightBegCol = 0;
    }
    if (this.e.HilightEndRow >= this.e.TotalLines)
    {
      this.e.HilightEndRow = this.e.TotalLines - 1;
      this.e.HilightEndCol = this.e.text[this.e.HilightEndRow].len;
    }
    if (this.e.CurLine >= this.e.TotalLines)
      this.e.CurLine = this.e.TotalLines - 1;
    if (this.e.CurCol >= this.e.text[this.e.CurLine].len)
      this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
    if (this.e.CurCol < 0)
      this.e.CurCol = 0;
    if (flag)
    {
      int pfmt = this.e.text[0].pfmt;
      this.e.ViewPageHdrFtr = false;
      this.e.EditPageHdrFtr = false;
      this.e.InputFontId = -1;
      this.e.CurLine = 0;
      this.e.CurCol = 0;
      this.InitLine(0);
      this.e.text[0].pfmt = LastCharSelected ? 0 : pfmt;
      this.LineAlloc(this.e.CurLine, 0, 1);
      char[] txt = this.e.text[this.e.CurLine].txt;
      ushort[] numArray = this.OpenCfmt(this.e.CurLine);
      int paraChar = (int) this.e.ParaChar;
      txt[0] = (char) paraChar;
      numArray[0] = (ushort) 0;
      this.CloseCfmt(this.e.CurLine);
    }
    if (SetCurPos)
    {
      this.e.CurLine = this.e.HilightBegRow;
      if (this.e.CurLine >= this.e.TotalLines)
        --this.e.CurLine;
      this.e.CurCol = this.e.HilightBegCol;
      if (this.e.CurLine - this.e.BeginLine >= this.e.WinHeight || this.e.CurLine - this.e.BeginLine < 0)
      {
        this.e.BeginLine = this.e.CurLine - this.e.WinHeight / 2;
        if (this.e.BeginLine < 0)
          this.e.BeginLine = 0;
      }
      this.e.CurRow = this.e.CurLine - this.e.BeginLine;
      this.e.CurCol = this.e.HilightBegCol;
    }
    if (this.e.CurLine >= this.e.TotalLines)
    {
      this.e.CurLine = this.e.TotalLines - 1;
      this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
      if (this.e.CurCol < 0)
        this.e.CurCol = 0;
      this.e.BeginLine = this.e.CurLine - this.e.WinHeight / 2;
      if (this.e.BeginLine < 0)
        this.e.BeginLine = 0;
      this.e.CurRow = this.e.CurLine - this.e.BeginLine;
    }
    this.e.HilightType = 0;
    this.e.StretchHilight = false;
    if (repaint)
    {
      this.e.PaintFlag = 4;
      this.PaintTer();
    }
    return true;
  }

  internal new bool DeleteLineBlock(bool disp)
  {
    if (this.e.TotalLines <= 1)
    {
      this.e.HilightType = 0;
      this.e.StretchHilight = false;
      return true;
    }
    if (this.NormalizeBlock())
    {
      this.SaveUndo(this.e.HilightBegRow, 0, this.e.HilightEndRow, this.e.text[this.e.HilightEndRow].len, 'D');
      this.MoveLineArrays(this.e.HilightBegRow, this.e.HilightEndRow - this.e.HilightBegRow + 1, 'D');
      if (this.e.TotalLines == 0)
      {
        this.e.TotalLines = 1;
        this.InitLine(0);
      }
      if (this.e.TotalLines == 1)
        this.e.EditPageHdrFtr = this.e.ViewPageHdrFtr = false;
      if (!disp)
        return true;
      this.e.CurLine = this.e.HilightBegRow;
      if (this.e.CurLine >= this.e.TotalLines)
        --this.e.CurLine;
      if (this.e.CurLine - this.e.BeginLine >= this.e.WinHeight || this.e.CurLine - this.e.BeginLine < 0)
      {
        this.e.BeginLine = this.e.CurLine - this.e.WinHeight / 2;
        if (this.e.BeginLine < 0)
          this.e.BeginLine = 0;
      }
      this.e.CurRow = this.e.CurLine - this.e.BeginLine;
      this.e.HilightType = 0;
      this.e.StretchHilight = false;
      this.e.PaintFlag = 4;
      this.PaintTer();
    }
    return true;
  }

  internal new bool EditPicture()
  {
    int curCfmt = this.GetCurCfmt(this.e.CurLine, this.e.CurCol);
    if ((this.e.TerFont[curCfmt].style & 128 /*0x80*/) == 0)
      return false;
    if (this.CallDialogBox((Form) new terdlg_edit_pict(this.e)))
    {
      this.ReleaseUndo();
      this.e.TerFont[curCfmt].flags |= 4096 /*0x1000*/;
      this.SetPictSize(curCfmt, this.TwipsToScrY(this.e.TerFont[curCfmt].PictHeight), this.TwipsToScrX(this.e.TerFont[curCfmt].PictWidth), true);
      this.XlateSizeForPrt(curCfmt);
      ++this.e.TerArg.modified;
      this.PaintTer();
    }
    return true;
  }

  internal new int GetDocHeight()
  {
    int docHeight = 0;
    for (int lin = 0; lin < this.e.TotalLines; ++lin)
      docHeight += this.ScrLineHeight(lin, true);
    return docHeight;
  }

  internal bool GetTextRange(out int pBegRow, out int pBegCol, out int pEndRow, out int pEndCol)
  {
    int num1;
    pEndCol = num1 = 0;
    int num2;
    pEndRow = num2 = num1;
    int num3;
    pBegCol = num3 = num2;
    pBegRow = num3;
    int index1;
    int num4;
    int index2;
    int num5;
    if (this.e.HilightType == 1)
    {
      this.NormalizeBlock();
      index1 = this.e.HilightBegRow;
      num4 = 0;
      index2 = this.e.HilightEndRow;
      num5 = this.e.text[index2].len;
      if (num5 < 0)
        num5 = 0;
    }
    else if (this.e.HilightType == 2)
    {
      this.NormalizeBlock();
      index1 = this.e.HilightBegRow;
      num4 = this.e.HilightBegCol;
      if (num4 >= this.e.text[index1].len)
        num4 = this.e.text[index1].len - 1;
      if (num4 < 0)
        num4 = 0;
      index2 = this.e.HilightEndRow;
      num5 = this.e.HilightEndCol;
      if (num5 > this.e.text[index2].len)
        num5 = this.e.text[index2].len;
      if (num5 < 0)
        num5 = 0;
      if (index1 == index2 && num4 > num5)
        num4 = num5;
    }
    else
    {
      index1 = 0;
      num4 = 0;
      index2 = this.e.TotalLines - 1;
      if (index2 < 0)
        index2 = 0;
      num5 = this.e.text[index2].len;
      if (num5 < 0)
        num5 = 0;
    }
    pBegRow = index1;
    pEndRow = index2;
    pBegCol = num4;
    pEndCol = num5;
    return true;
  }

  internal new void InsertBuffer(char[] ptr, ushort[] fmt, int[] pPfmt, bool TerFormat)
  {
    int num1 = -1;
    tc.StrTextBuf pInfo = new tc.StrTextBuf();
    int abs = this.RowColToAbs(this.e.CurLine, this.e.CurCol);
    if (this.e.CurLine < this.e.RepageBeginLine)
      this.e.RepageBeginLine = this.e.CurLine;
    if (this.True(pPfmt) && pPfmt[1] == -1)
    {
      num1 = pPfmt[0];
      pPfmt = (int[]) null;
    }
    char[] txt1 = this.e.text[this.e.CurLine].txt;
    int index1 = this.e.text[this.e.CurLine].len - 1;
    while (index1 >= 0 && (int) txt1[index1] != (int) this.e.ParaChar)
      --index1;
    if (index1 >= 0 && this.e.CurCol > index1)
    {
      this.SplitLine(this.e.CurLine, this.e.CurCol, 0);
      ++this.e.CurLine;
      this.e.CurCol = 0;
    }
    pInfo.pBuf = ptr;
    pInfo.pFmt = fmt;
    pInfo.index = 0;
    pInfo.len = 0;
    pInfo.eol = false;
    pInfo.eof = false;
    pInfo.ParaCharFound = false;
    int curLine;
    int num2 = curLine = this.e.CurLine;
    while (!pInfo.eof)
    {
      pInfo.MaxLineLen = !this.e.TerArg.WordWrap ? this.e.LineWidth - this.e.text[this.e.CurLine].len - 1 : 3 * this.e.LineWidth / 4 - this.e.text[this.e.CurLine].len - 1;
      if (pInfo.MaxLineLen <= 0)
        pInfo.MaxLineLen = 10;
      this.NextBufferLine(ref pInfo);
      if (!pInfo.eof || pInfo.len != 0)
      {
        int len1 = this.e.text[this.e.CurLine].len;
        this.LineAlloc(this.e.CurLine, this.e.text[this.e.CurLine].len, this.e.text[this.e.CurLine].len + pInfo.len);
        if (len1 == 1 && (int) this.e.text[this.e.CurLine].txt[0] == (int) this.e.ParaChar)
          this.e.text[this.e.CurLine].txt[this.e.text[this.e.CurLine].len - 1] = this.e.ParaChar;
        else if (this.e.CurCol < len1)
          this.MoveCharInfo(this.e.CurLine, this.e.CurCol, this.e.CurLine, this.e.CurCol + pInfo.len, len1 - this.e.CurCol);
        char[] txt2 = this.e.text[this.e.CurLine].txt;
        ushort[] fmt1;
        ushort[] cmi;
        if (this.e.FullRenderMode || this.e.text[this.e.CurLine].fmt != null || this.e.text[this.e.CurLine].tag != null)
        {
          this.OpenCharInfo(this.e.CurLine, out fmt1, out cmi);
        }
        else
        {
          fmt1 = (ushort[]) null;
          cmi = (ushort[]) null;
        }
        this.FarMove(pInfo.pBuf, pInfo.index, txt2, this.e.CurCol, pInfo.len);
        if (pInfo.pFmt == null)
        {
          if (this.e.FullRenderMode || fmt1 != null)
          {
            ushort CurFont = this.e.InputFontId < 0 ? (this.e.CurCol <= 0 || fmt1 == null ? (ushort) this.GetPrevCfmt(this.e.CurLine, this.e.CurCol) : fmt1[this.e.CurCol - 1]) : (ushort) this.e.InputFontId;
            if ((this.e.TerFont[(int) CurFont].style & 128 /*0x80*/) != 0 && this.e.InputFontId < 0)
              CurFont = (ushort) 0;
            if (this.e.InputFontId == -1 && this.e.TerFont[(int) CurFont].FieldId > 0 && fmt1 != null && (int) CurFont != (int) fmt1[this.e.CurCol])
              CurFont = fmt1[this.e.CurCol];
            if (this.e.FullRenderMode && (this.e.TerOpFlags2 & 1024 /*0x0400*/) == 0)
              CurFont = (ushort) this.SetCurLangFont((int) CurFont);
            if (fmt1 != null)
            {
              for (int index2 = 0; index2 < pInfo.len; ++index2)
                fmt1[this.e.CurCol + index2] = CurFont;
            }
          }
        }
        else
          this.FarMove(pInfo.pFmt, pInfo.index, fmt1, this.e.CurCol, pInfo.len);
        if (this.e.TrackChanges && (this.e.TerFlags6 & 2097152 /*0x200000*/) != 0 && fmt1 != null)
        {
          for (int index3 = 0; index3 < pInfo.len; ++index3)
            fmt1[this.e.CurCol + index3] = this.SetTrackingFont((int) fmt1[this.e.CurCol + index3], 1);
        }
        if (this.e.FullRenderMode || cmi != null)
        {
          if (cmi != null)
          {
            for (int index4 = 0; index4 < pInfo.len; ++index4)
              cmi[this.e.CurCol + index4] = (ushort) 0;
          }
          this.CloseCharInfo(this.e.CurLine);
        }
        int pfmt = this.e.text[this.e.CurLine].pfmt;
        if (this.True(pPfmt))
          this.e.text[this.e.CurLine].pfmt = pPfmt[this.e.CurLine - num2];
        else if (num1 >= 0)
          this.e.text[this.e.CurLine].pfmt = num1;
        if (this.e.text[this.e.CurLine].pfmt >= this.e.TotalPfmts)
          this.e.text[this.e.CurLine].pfmt = 0;
        this.e.CurCol += pInfo.len;
        if (pInfo.eol || pInfo.MaxLineLen == 0 || pInfo.ParaCharFound)
        {
          if (!this.CheckLineLimit(this.e.TotalLines + 1))
          {
            this.PrintError(88, this.e.MsgString[20]);
            break;
          }
          this.MoveLineArrays(this.e.CurLine, 1, 'A');
          if (this.e.text[this.e.CurLine + 1] != null)
          {
            this.e.text[this.e.CurLine + 1].pfmt = pfmt;
            this.e.text[this.e.CurLine + 1].tabw = this.e.text[this.e.CurLine].tabw;
          }
          this.e.text[this.e.CurLine].tabw = (tc.ClsTabw) null;
          ++curLine;
          int num3 = this.e.text[this.e.CurLine].len - this.e.CurCol;
          if (num3 < 0)
            num3 = 0;
          this.LineAlloc(this.e.CurLine + 1, 0, num3);
          if (num3 > 0)
          {
            this.MoveCharInfo(this.e.CurLine, this.e.CurCol, this.e.CurLine + 1, 0, num3);
            int NewSize = this.e.text[this.e.CurLine].len - num3;
            if (NewSize < 0)
              NewSize = 0;
            this.LineAlloc(this.e.CurLine, this.e.text[this.e.CurLine].len, NewSize);
          }
          if (!TerFormat && this.e.TerArg.WordWrap && pInfo.eol)
          {
            int len2 = this.e.text[this.e.CurLine].len;
            char[] txt3 = this.e.text[this.e.CurLine].txt;
            if (len2 == 0 || (int) txt3[len2 - 1] != (int) this.e.ParaChar || (int) txt3[len2 - 1] != (int) this.e.CellChar)
            {
              this.LineAlloc(this.e.CurLine, len2, len2 + 1);
              this.e.text[this.e.CurLine].txt[len2] = this.e.ParaChar;
              this.OpenCharInfo(this.e.CurLine, out fmt1, out cmi);
              fmt1[len2] = this.e.InputFontId < 0 ? (len2 <= 0 ? (ushort) this.GetPrevCfmt(this.e.CurLine, len2) : fmt1[len2 - 1]) : (ushort) this.e.InputFontId;
              if (this.e.InputFontId == -1 && this.e.TerFont[(int) fmt1[len2]].FieldId > 0 && this.GetNextCfmt(this.e.CurLine, len2) != (int) fmt1[len2])
                fmt1[len2] = (ushort) 0;
              if ((this.e.TerFont[(int) fmt1[len2]].style & 128 /*0x80*/) != 0)
                fmt1[len2] = (ushort) 0;
              cmi[len2] = (ushort) 0;
              this.CloseCharInfo(this.e.CurLine);
            }
          }
          if (pInfo.ParaCharFound)
            this.e.text[this.e.CurLine].flags |= 1;
          ++this.e.CurLine;
          if (this.e.CurLine >= this.e.TotalLines)
            this.e.CurLine = this.e.TotalLines - 1;
          this.e.CurCol = 0;
        }
      }
      else
        break;
    }
    int row1;
    int col1;
    this.AbsToRowCol(abs, out row1, out col1);
    int row2;
    int col2;
    this.AbsToRowCol(this.RowColToAbs(this.e.CurLine, this.e.CurCol) - 1, out row2, out col2);
    this.SaveUndo(row1, col1, row2, col2, 'I');
    ++this.e.TerArg.modified;
  }

  internal new void InsertBuffer(string str, ushort[] fmt, int[] pPfmt, bool TerFormat)
  {
    switch (str)
    {
      case null:
        break;
      case "":
        break;
      default:
        char ch = str[str.Length - 1];
        string str1;
        if (this.e.TotalLines == 0 && ch != '\u0015' && ch != '\r' && ch != '\n')
          str1 = str + new string(new char[2]
          {
            '\u0015',
            char.MinValue
          });
        else
          str1 = str + new string(char.MinValue, 1);
        this.InsertBuffer(str1.ToCharArray(), fmt, pPfmt, TerFormat);
        break;
    }
  }

  internal void InitFirstParaChar()
  {
    this.LineAlloc(0, this.e.text[0].len, 1);
    char[] txt = this.e.text[0].txt;
    ushort[] numArray = this.OpenCfmt(0);
    int paraChar = (int) this.e.ParaChar;
    txt[0] = (char) paraChar;
    numArray[0] = (ushort) 0;
    this.CloseCfmt(0);
  }

  internal bool InsertTerText(string text, bool repaint) => this.InsertTerText(text, repaint, true);

  internal bool InsertTerText(string text, bool repaint, bool reWrap)
  {
    if (text == null || text.Length == 0)
      return true;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int curLine = this.e.CurLine;
    char ch = text[text.Length - 1];
    if (this.e.TotalLines == 0 && ch != '\u0015' && ch != '\r' && ch != '\n')
      text += new string(new char[2]
      {
        '\u0015',
        char.MinValue
      });
    else
      text += new string(char.MinValue, 1);
    int abs = this.pos.TerRowColToAbs(this.e.CurLine, this.e.CurCol);
    int num = -1;
    bool flag = false;
    bool zoneInBegin = false;
    if (this.e.IsProtectedZone(abs, false, out zoneInBegin))
    {
      num = this.e.GetTotalChars(false);
      flag = !zoneInBegin;
    }
    this.InsertBuffer(text.ToCharArray(), (ushort[]) null, (int[]) null, false);
    if (num != -1)
    {
      this.e.page.Repaginate(false, false, 0, false);
      if (zoneInBegin)
        this.e.ProtectedFirstRealCharCount += this.e.GetTotalChars(false) - num;
      else if (flag)
        this.e.ProtectedEndRealCharCount += this.e.GetTotalChars(false) - num;
    }
    if (repaint)
    {
      this.e.WinHeight = this.e.TerWinHeight / this.e.TerFont[0].height;
      this.PaintTer();
    }
    else if (reWrap && this.e.TerArg.WordWrap)
      this.WordWrap(curLine, this.e.CurLine - curLine + 1);
    return true;
  }

  internal new bool IsProtected(bool msg, bool del)
  {
    bool flag1 = false;
    if (this.e.HilightType == 0 || !this.NormalizeBlock() || (this.e.TerFlags & 256 /*0x0100*/) != 0)
      return false;
    bool flag2 = this.AllSelected();
    if (del && !flag2 && this.e.HilightBegRow > 0 && this.e.HilightBegCol == 0 && this.e.HilightEndCol == this.e.text[this.e.HilightEndRow].len && (this.e.PfmtId[this.e.text[this.e.HilightBegRow - 1].pfmt].flags & 12288 /*0x3000*/) != 0 && (this.e.PfmtId[this.e.text[this.e.HilightEndRow].pfmt].flags & 12288 /*0x3000*/) == 0 && (this.e.HilightEndRow >= this.e.TotalLines - 1 || this.LineInfo(this.e.HilightEndRow + 1, 2)))
    {
      if (msg)
        this.PrintError(84, this.e.MsgString[123]);
      return true;
    }
    if (this.e.HtmlMode & del && this.e.HilightBegCol > 0 && this.e.PfmtId[this.e.text[this.e.HilightBegRow].pfmt].AuxId != this.e.PfmtId[this.e.text[this.e.HilightEndRow].pfmt].AuxId)
    {
      if (msg)
        this.PrintError(131, this.e.MsgString[123]);
      return true;
    }
    if (!flag2 && (this.e.PfmtId[this.e.text[this.e.HilightBegRow].pfmt].flags & 12288 /*0x3000*/) != 0 != ((this.e.PfmtId[this.e.text[this.e.HilightEndRow].pfmt].flags & 12288 /*0x3000*/) != 0))
    {
      bool flag3 = false;
      if ((this.e.PfmtId[this.e.text[this.e.HilightBegRow].pfmt].flags & 12288 /*0x3000*/) != 0)
      {
        flag3 = true;
      }
      else
      {
        int hilightEndRow1 = this.e.HilightEndRow;
        if (del)
        {
          int hilightEndRow2 = this.e.HilightEndRow;
          while (hilightEndRow2 < this.e.TotalLines && (this.e.PfmtId[this.e.text[hilightEndRow2].pfmt].flags & 12288 /*0x3000*/) == 0)
            ++hilightEndRow2;
          this.e.HilightEndRow = hilightEndRow2 - 1;
          this.e.HilightEndCol = this.e.text[this.e.HilightEndRow].len;
          flag1 = true;
        }
        else
        {
          if (this.e.HilightEndCol == 0 && hilightEndRow1 > this.e.HilightBegRow)
            --hilightEndRow1;
          if ((this.e.PfmtId[this.e.text[hilightEndRow1].pfmt].flags & 12288 /*0x3000*/) != 0)
          {
            if ((this.e.text[hilightEndRow1].flags & 655360 /*0x0A0000*/) != 0 && hilightEndRow1 > this.e.HilightBegRow && (this.e.PfmtId[this.e.text[hilightEndRow1 - 1].pfmt].flags & 12288 /*0x3000*/) == 0)
            {
              this.e.HilightEndRow = hilightEndRow1 - 1;
              this.e.HilightEndCol = this.e.text[this.e.HilightEndRow].len;
            }
            else
              flag3 = true;
          }
        }
      }
      if (flag3)
      {
        if (msg)
          this.PrintError(40, this.e.MsgString[83]);
        return true;
      }
    }
    if (del && !flag2 && !flag1 && this.e.HilightEndCol >= this.e.text[this.e.HilightEndRow].len && this.e.HilightEndRow + 1 < this.e.TotalLines && (this.e.text[this.e.HilightEndRow + 1].flags & 1966080 /*0x1E0000*/) != 0)
    {
      if (msg)
        this.PrintError(123, this.e.MsgString[83]);
      return true;
    }
    return del && this.BlockHasProtectOn(msg, true);
  }

  internal bool NextBufferLine(ref tc.StrTextBuf pInfo)
  {
    pInfo.index += pInfo.len;
    if (pInfo.eol)
      ++pInfo.index;
    if (pInfo.eol && pInfo.CrFound)
      ++pInfo.index;
    pInfo.len = 0;
    bool flag;
    pInfo.eof = flag = false;
    pInfo.eol = flag;
    pInfo.ParaCharFound = false;
    pInfo.CrFound = false;
    int length = pInfo.pBuf.Length;
    while (pInfo.index + pInfo.len < length)
    {
      char ch = pInfo.pBuf[pInfo.index + pInfo.len];
      if (ch == '\u0014')
      {
        if (pInfo.len != 0)
          return true;
        ++pInfo.index;
      }
      else
      {
        if (this.True(pInfo.pFmt))
        {
          ushort index = pInfo.pFmt[pInfo.index + pInfo.len];
          if ((int) index > (int) (ushort) this.e.TotalFonts || this.False(this.e.TerFont[(int) index].InUse))
            pInfo.pFmt[pInfo.index + pInfo.len] = (ushort) 0;
        }
        switch (ch)
        {
          case char.MinValue:
            pInfo.eof = true;
            return true;
          case '\n':
            if (pInfo.CrFound)
              --pInfo.len;
            pInfo.eol = true;
            return true;
          case '\r':
            pInfo.CrFound = true;
            break;
          default:
            pInfo.CrFound = false;
            break;
        }
        if (((int) ch == (int) this.e.ParaChar || (int) ch == (int) this.e.CellChar) && this.e.TerArg.WordWrap)
        {
          if (pInfo.pBuf[pInfo.index + pInfo.len + 1] != '\r')
            pInfo.ParaCharFound = true;
          ++pInfo.len;
          return true;
        }
        if (pInfo.len >= pInfo.MaxLineLen)
        {
          pInfo.MaxLineLen = 0;
          return true;
        }
        ++pInfo.len;
      }
    }
    pInfo.eof = true;
    return true;
  }

  internal bool NormalizeBlock(
    ref int begRow,
    ref int begCol,
    ref int endRow,
    ref int endCol,
    ref int hilightType,
    ref int curLine,
    ref int curCol,
    bool adjustCurPos)
  {
    if (hilightType != 0)
    {
      if (hilightType == 2)
      {
        if (begRow < 0)
          begRow = 0;
        if (endRow < 0)
          endRow = 0;
        if (begRow >= this.e.TotalLines)
          begRow = this.e.TotalLines - 1;
        if (endRow >= this.e.TotalLines)
          endRow = this.e.TotalLines - 1;
        if (begRow == endRow && begCol == endCol)
        {
          hilightType = 0;
          return false;
        }
        if (begRow > endRow)
        {
          int num1 = endRow;
          endRow = begRow;
          begRow = num1;
          int num2 = endCol;
          endCol = begCol;
          begCol = num2;
        }
        if (begRow == endRow && begCol > endCol)
        {
          int num = endCol;
          endCol = begCol;
          begCol = num;
        }
        if (this.e.TerArg.WordWrap && begCol >= this.e.text[begRow].len)
          begCol = this.e.text[begRow].len - 1;
        if (!this.e.TerArg.WordWrap && begCol > this.e.text[begRow].len)
          begCol = this.e.text[begRow].len;
        if (begCol < 0)
          begCol = 0;
        if (endCol > this.e.text[endRow].len)
          endCol = this.e.text[endRow].len;
        if (endCol < 0)
          endCol = 0;
        if (this.AllSelected(begRow, begCol, endRow, endCol, hilightType))
          return true;
        if (!this.e.InUndo && (this.e.TerFlags5 & 33554432 /*0x02000000*/) == 0)
        {
          int pBegRow = begRow;
          int pBegCol = begCol;
          int pEndRow = endRow;
          int pEndCol = endCol;
          int num3 = curLine;
          int num4 = curCol;
          this.tbl.AdjustBlockForTable(ref pBegRow, ref pBegCol, ref pEndRow, ref pEndCol, adjustCurPos);
          curLine = num3;
          curCol = num4;
          begRow = pBegRow;
          begCol = pBegCol;
          endRow = pEndRow;
          endCol = pEndCol;
        }
      }
      else
      {
        if (begRow > endRow)
        {
          int num = endRow;
          endRow = begRow;
          begRow = num;
        }
        if (begCol > endCol)
        {
          int num = endCol;
          endCol = begCol;
          begCol = num;
        }
        if (this.AllSelected(begRow, begCol, endRow, endCol, hilightType))
          return true;
      }
      if (begRow >= this.e.TotalLines)
        begRow = this.e.TotalLines - 1;
      if (endRow >= this.e.TotalLines)
        endRow = this.e.TotalLines - 1;
      if ((this.e.TerFlags & 16384 /*0x4000*/) != 0 && !this.e.ShowHiddenText)
      {
        int StartLine1 = begRow;
        int StartCol1 = begCol;
        bool flag1 = StartLine1 == curLine && StartCol1 == curCol;
        if ((this.e.TerFont[this.GetCurCfmt(StartLine1, StartCol1)].style & 64 /*0x40*/) != 0 && this.e.TerLocateStyleChar(64 /*0x40*/, false, ref StartLine1, ref StartCol1, true) && (StartLine1 < endRow || StartLine1 == endRow && StartCol1 <= endCol - 1))
        {
          begRow = StartLine1;
          begCol = StartCol1;
          if (flag1)
          {
            curLine = StartLine1;
            curCol = StartCol1;
          }
        }
        int StartLine2 = endRow;
        int StartCol2 = endCol - 1;
        bool flag2 = StartLine2 == curLine && StartCol2 + 1 == curCol;
        if (StartCol2 < 0 && StartLine2 > 0)
        {
          --StartLine2;
          StartCol2 = this.e.text[StartLine2].len - 1;
        }
        if (StartCol2 < 0)
          StartCol2 = 0;
        if ((this.e.TerFont[this.GetCurCfmt(StartLine2, StartCol2)].style & 64 /*0x40*/) != 0 && this.e.TerLocateStyleChar(64 /*0x40*/, false, ref StartLine2, ref StartCol2, false) && (StartLine2 > begRow || StartLine2 == begRow && StartCol2 >= begCol))
        {
          endRow = StartLine2;
          endCol = StartCol2 + 1;
          if (flag2)
          {
            curLine = StartLine2;
            curCol = StartCol2 + 1;
          }
        }
        this.e.CursDirection = 1;
      }
      int num5;
      int num6;
      if (this.e.DraggingText && (this.e.TerFlags4 & 4194304 /*0x400000*/) != 0)
      {
        int curCfmt = this.GetCurCfmt(begRow, begCol);
        int prevCfmt = this.GetPrevCfmt(endRow, endCol);
        int fieldId1 = this.e.TerFont[curCfmt].FieldId;
        int fieldId2 = this.e.TerFont[prevCfmt].FieldId;
        if ((this.e.TerFont[curCfmt].style & 512 /*0x0200*/) != 0 && fieldId1 == 0)
        {
          num5 = begRow;
          num6 = begCol;
          if (this.e.TerLocateStyleChar(512 /*0x0200*/, false, ref num5, ref num6, false))
            this.NextTextPos(ref num5, ref num6);
          begRow = num5;
          begCol = num6;
        }
        if ((this.e.TerFont[prevCfmt].style & 512 /*0x0200*/) != 0 && fieldId2 == 0)
        {
          num5 = endRow;
          num6 = endCol;
          this.e.TerLocateStyleChar(512 /*0x0200*/, false, ref num5, ref num6, true);
          endRow = num5;
          endCol = num6;
        }
      }
      if (this.e.DraggingText || (this.e.TerOpFlags & 32768 /*0x8000*/) == 0 && (this.e.TerFlags5 & 1024 /*0x0400*/) == 0)
      {
        bool flag3 = false;
        int curCfmt1 = this.GetCurCfmt(begRow, begCol);
        int prevCfmt1 = this.GetPrevCfmt(endRow, endCol);
        int fieldId3 = this.e.TerFont[curCfmt1].FieldId;
        int fieldId4 = this.e.TerFont[prevCfmt1].FieldId;
        if (fieldId3 == 6 || fieldId3 == 7 || fieldId3 == 9)
          flag3 = true;
        if (fieldId4 == 6 || fieldId4 == 7 || fieldId4 == 9)
          flag3 = true;
        if (flag3 && this.e.TerFont[curCfmt1].FieldId == this.e.TerFont[prevCfmt1].FieldId)
        {
          int fieldId5 = this.e.TerFont[curCfmt1].FieldId;
          int prevCfmt2 = this.GetPrevCfmt(begRow, begCol);
          int curCfmt2 = this.GetCurCfmt(endRow, endCol);
          if (this.e.TerFont[prevCfmt2].FieldId == fieldId5 || this.e.TerFont[curCfmt2].FieldId == fieldId5)
          {
            bool flag4 = false;
            for (int line = begRow; line <= endRow; ++line)
            {
              int num7 = line == begRow ? begCol : 0;
              int num8 = line == endRow ? endCol : this.e.text[line].len;
              for (int col = num7; col < num8; ++col)
              {
                if (this.e.TerFont[this.GetCurCfmt(line, col)].FieldId != fieldId5)
                {
                  flag4 = true;
                  break;
                }
              }
              if (flag4)
                break;
            }
            if (!flag4)
              flag3 = false;
          }
        }
        if (fieldId3 == 9 || fieldId4 == 9 || this.e.DraggingText)
          flag3 = true;
        if (flag3)
        {
          int num9 = endRow;
          int num10 = endCol;
          int num11 = begRow;
          int num12 = begCol;
          if (fieldId3 != 6 && fieldId3 != 7 && fieldId4 == 6 && !this.e.ShowFieldNames)
          {
            num5 = num9;
            num6 = num10 - 1;
            this.FixPos(ref num5, ref num6);
            this.GetFieldScope(num5, num6, 6, out num9, out num10, out tc.SkipInt, out tc.SkipInt);
          }
          else
          {
            int curCfmt3 = this.GetCurCfmt(num11, num12);
            if (this.e.TerFont[curCfmt3].FieldId == 6 || this.e.TerFont[curCfmt3].FieldId == 7)
              this.GetFieldScope(num11, num12, 6, out num11, out num12, out tc.SkipInt, out tc.SkipInt);
            else if (this.e.TerFont[curCfmt3].FieldId == 9)
              this.GetFieldLoc(num11, num12, true, out num11, out num12);
            num5 = num9;
            num6 = num10 - 1;
            this.FixPos(ref num5, ref num6);
            int curCfmt4 = this.GetCurCfmt(num5, num6);
            if (this.e.TerFont[curCfmt4].FieldId == 6 || this.e.TerFont[curCfmt4].FieldId == 7)
            {
              if (!this.GetFieldScope(num5, num6, 7, out tc.SkipInt, out tc.SkipInt, out num9, out num10))
                this.GetFieldScope(num5, num6, 6, out tc.SkipInt, out tc.SkipInt, out num9, out num10);
            }
            else if (this.e.TerFont[curCfmt4].FieldId == 9)
              this.GetFieldLoc(num5, num6, false, out num9, out num10);
          }
          endRow = num9;
          endCol = num10;
          begRow = num11;
          begCol = num12;
        }
      }
    }
    return true;
  }

  internal new bool NormalizeBlock()
  {
    if (this.e.HilightType != 0)
    {
      this.e.StretchHilight = false;
      if (this.e.HilightType == 2)
      {
        if (this.e.HilightBegRow < 0)
          this.e.HilightBegRow = 0;
        if (this.e.HilightEndRow < 0)
          this.e.HilightEndRow = 0;
        if (this.e.HilightBegRow >= this.e.TotalLines)
          this.e.HilightBegRow = this.e.TotalLines - 1;
        if (this.e.HilightEndRow >= this.e.TotalLines)
          this.e.HilightEndRow = this.e.TotalLines - 1;
        if (this.e.HilightBegRow == this.e.HilightEndRow && this.e.HilightBegCol == this.e.HilightEndCol)
        {
          this.e.HilightType = 0;
          return false;
        }
        if (this.e.HilightBegRow > this.e.HilightEndRow)
        {
          int hilightEndRow = this.e.HilightEndRow;
          this.e.HilightEndRow = this.e.HilightBegRow;
          this.e.HilightBegRow = hilightEndRow;
          int hilightEndCol = this.e.HilightEndCol;
          this.e.HilightEndCol = this.e.HilightBegCol;
          this.e.HilightBegCol = hilightEndCol;
        }
        if (this.e.HilightBegRow == this.e.HilightEndRow && this.e.HilightBegCol > this.e.HilightEndCol)
        {
          int hilightEndCol = this.e.HilightEndCol;
          this.e.HilightEndCol = this.e.HilightBegCol;
          this.e.HilightBegCol = hilightEndCol;
        }
        if (this.e.TerArg.WordWrap && this.e.HilightBegCol >= this.e.text[this.e.HilightBegRow].len)
          this.e.HilightBegCol = this.e.text[this.e.HilightBegRow].len - 1;
        if (!this.e.TerArg.WordWrap && this.e.HilightBegCol > this.e.text[this.e.HilightBegRow].len)
          this.e.HilightBegCol = this.e.text[this.e.HilightBegRow].len;
        if (this.e.HilightBegCol < 0)
          this.e.HilightBegCol = 0;
        if (this.e.HilightEndCol > this.e.text[this.e.HilightEndRow].len)
          this.e.HilightEndCol = this.e.text[this.e.HilightEndRow].len;
        if (this.e.HilightEndCol < 0)
          this.e.HilightEndCol = 0;
        if (this.AllSelected())
          return true;
        if (!this.e.InUndo && (this.e.TerFlags5 & 33554432 /*0x02000000*/) == 0)
          this.AdjustBlockForTable(true);
      }
      else
      {
        if (this.e.HilightBegRow > this.e.HilightEndRow)
        {
          int hilightEndRow = this.e.HilightEndRow;
          this.e.HilightEndRow = this.e.HilightBegRow;
          this.e.HilightBegRow = hilightEndRow;
        }
        if (this.e.HilightBegCol > this.e.HilightEndCol)
        {
          int hilightEndCol = this.e.HilightEndCol;
          this.e.HilightEndCol = this.e.HilightBegCol;
          this.e.HilightBegCol = hilightEndCol;
        }
        if (this.AllSelected())
          return true;
      }
      if (this.e.HilightBegRow >= this.e.TotalLines)
        this.e.HilightBegRow = this.e.TotalLines - 1;
      if (this.e.HilightEndRow >= this.e.TotalLines)
        this.e.HilightEndRow = this.e.TotalLines - 1;
      if ((this.e.TerFlags & 16384 /*0x4000*/) != 0 && !this.e.ShowHiddenText)
      {
        int hilightBegRow = this.e.HilightBegRow;
        int hilightBegCol = this.e.HilightBegCol;
        bool flag1 = hilightBegRow == this.e.CurLine && hilightBegCol == this.e.CurCol;
        if ((this.e.TerFont[this.GetCurCfmt(hilightBegRow, hilightBegCol)].style & 64 /*0x40*/) != 0 && this.e.TerLocateStyleChar(64 /*0x40*/, false, ref hilightBegRow, ref hilightBegCol, true) && (hilightBegRow < this.e.HilightEndRow || hilightBegRow == this.e.HilightEndRow && hilightBegCol <= this.e.HilightEndCol - 1))
        {
          this.e.HilightBegRow = hilightBegRow;
          this.e.HilightBegCol = hilightBegCol;
          if (flag1)
          {
            this.e.CurLine = hilightBegRow;
            this.e.CurCol = hilightBegCol;
          }
        }
        int hilightEndRow = this.e.HilightEndRow;
        int StartCol = this.e.HilightEndCol - 1;
        bool flag2 = hilightEndRow == this.e.CurLine && StartCol + 1 == this.e.CurCol;
        if (StartCol < 0 && hilightEndRow > 0)
        {
          --hilightEndRow;
          StartCol = this.e.text[hilightEndRow].len - 1;
        }
        if (StartCol < 0)
          StartCol = 0;
        if ((this.e.TerFont[this.GetCurCfmt(hilightEndRow, StartCol)].style & 64 /*0x40*/) != 0 && this.e.TerLocateStyleChar(64 /*0x40*/, false, ref hilightEndRow, ref StartCol, false) && (hilightEndRow > this.e.HilightBegRow || hilightEndRow == this.e.HilightBegRow && StartCol >= this.e.HilightBegCol))
        {
          this.e.HilightEndRow = hilightEndRow;
          this.e.HilightEndCol = StartCol + 1;
          if (flag2)
          {
            this.e.CurLine = hilightEndRow;
            this.e.CurCol = StartCol + 1;
          }
        }
        this.e.CursDirection = 1;
      }
      int num1;
      int num2;
      if (this.e.DraggingText && (this.e.TerFlags4 & 4194304 /*0x400000*/) != 0)
      {
        int curCfmt = this.GetCurCfmt(this.e.HilightBegRow, this.e.HilightBegCol);
        int prevCfmt = this.GetPrevCfmt(this.e.HilightEndRow, this.e.HilightEndCol);
        int fieldId1 = this.e.TerFont[curCfmt].FieldId;
        int fieldId2 = this.e.TerFont[prevCfmt].FieldId;
        if ((this.e.TerFont[curCfmt].style & 512 /*0x0200*/) != 0 && fieldId1 == 0)
        {
          num1 = this.e.HilightBegRow;
          num2 = this.e.HilightBegCol;
          if (this.e.TerLocateStyleChar(512 /*0x0200*/, false, ref num1, ref num2, false))
            this.NextTextPos(ref num1, ref num2);
          this.e.HilightBegRow = num1;
          this.e.HilightBegCol = num2;
        }
        if ((this.e.TerFont[prevCfmt].style & 512 /*0x0200*/) != 0 && fieldId2 == 0)
        {
          num1 = this.e.HilightEndRow;
          num2 = this.e.HilightEndCol;
          this.e.TerLocateStyleChar(512 /*0x0200*/, false, ref num1, ref num2, true);
          this.e.HilightEndRow = num1;
          this.e.HilightEndCol = num2;
        }
      }
      if (this.e.DraggingText || (this.e.TerOpFlags & 32768 /*0x8000*/) == 0 && (this.e.TerFlags5 & 1024 /*0x0400*/) == 0)
      {
        bool flag3 = false;
        int curCfmt1 = this.GetCurCfmt(this.e.HilightBegRow, this.e.HilightBegCol);
        int prevCfmt1 = this.GetPrevCfmt(this.e.HilightEndRow, this.e.HilightEndCol);
        int fieldId3 = this.e.TerFont[curCfmt1].FieldId;
        int fieldId4 = this.e.TerFont[prevCfmt1].FieldId;
        if (fieldId3 == 6 || fieldId3 == 7 || fieldId3 == 9)
          flag3 = true;
        if (fieldId4 == 6 || fieldId4 == 7 || fieldId4 == 9)
          flag3 = true;
        if (flag3 && this.e.TerFont[curCfmt1].FieldId == this.e.TerFont[prevCfmt1].FieldId)
        {
          int fieldId5 = this.e.TerFont[curCfmt1].FieldId;
          int prevCfmt2 = this.GetPrevCfmt(this.e.HilightBegRow, this.e.HilightBegCol);
          int curCfmt2 = this.GetCurCfmt(this.e.HilightEndRow, this.e.HilightEndCol);
          if (this.e.TerFont[prevCfmt2].FieldId == fieldId5 || this.e.TerFont[curCfmt2].FieldId == fieldId5)
          {
            bool flag4 = false;
            for (int hilightBegRow = this.e.HilightBegRow; hilightBegRow <= this.e.HilightEndRow; ++hilightBegRow)
            {
              int hilightBegCol = hilightBegRow == this.e.HilightBegRow ? this.e.HilightBegCol : 0;
              int num3 = hilightBegRow == this.e.HilightEndRow ? this.e.HilightEndCol : this.e.text[hilightBegRow].len;
              for (int col = hilightBegCol; col < num3; ++col)
              {
                if (this.e.TerFont[this.GetCurCfmt(hilightBegRow, col)].FieldId != fieldId5)
                {
                  flag4 = true;
                  break;
                }
              }
              if (flag4)
                break;
            }
            if (!flag4)
              flag3 = false;
          }
        }
        if (fieldId3 == 9 || fieldId4 == 9 || this.e.DraggingText)
          flag3 = true;
        if (flag3)
        {
          int hilightEndRow = this.e.HilightEndRow;
          int hilightEndCol = this.e.HilightEndCol;
          int hilightBegRow = this.e.HilightBegRow;
          int hilightBegCol = this.e.HilightBegCol;
          if (fieldId3 != 6 && fieldId3 != 7 && fieldId4 == 6 && !this.e.ShowFieldNames)
          {
            num1 = hilightEndRow;
            num2 = hilightEndCol - 1;
            this.FixPos(ref num1, ref num2);
            this.GetFieldScope(num1, num2, 6, out hilightEndRow, out hilightEndCol, out tc.SkipInt, out tc.SkipInt);
          }
          else
          {
            int curCfmt3 = this.GetCurCfmt(hilightBegRow, hilightBegCol);
            if (this.e.TerFont[curCfmt3].FieldId == 6 || this.e.TerFont[curCfmt3].FieldId == 7)
              this.GetFieldScope(hilightBegRow, hilightBegCol, 6, out hilightBegRow, out hilightBegCol, out tc.SkipInt, out tc.SkipInt);
            else if (this.e.TerFont[curCfmt3].FieldId == 9)
              this.GetFieldLoc(hilightBegRow, hilightBegCol, true, out hilightBegRow, out hilightBegCol);
            num1 = hilightEndRow;
            num2 = hilightEndCol - 1;
            this.FixPos(ref num1, ref num2);
            int curCfmt4 = this.GetCurCfmt(num1, num2);
            if (this.e.TerFont[curCfmt4].FieldId == 6 || this.e.TerFont[curCfmt4].FieldId == 7)
            {
              if (!this.GetFieldScope(num1, num2, 7, out tc.SkipInt, out tc.SkipInt, out hilightEndRow, out hilightEndCol))
                this.GetFieldScope(num1, num2, 6, out tc.SkipInt, out tc.SkipInt, out hilightEndRow, out hilightEndCol);
            }
            else if (this.e.TerFont[curCfmt4].FieldId == 9)
              this.GetFieldLoc(num1, num2, false, out hilightEndRow, out hilightEndCol);
          }
          this.e.HilightEndRow = hilightEndRow;
          this.e.HilightEndCol = hilightEndCol;
          this.e.HilightBegRow = hilightBegRow;
          this.e.HilightBegCol = hilightBegCol;
        }
      }
    }
    return true;
  }

  internal new bool NormalizeForFootnote()
  {
    if (this.e.HilightType != 2)
      return false;
    if ((this.e.TerOpFlags2 & 2) == 0 && (this.e.TerFlags5 & 256 /*0x0100*/) == 0)
    {
      int StartLine = this.e.HilightBegRow;
      int StartCol1 = this.e.HilightBegCol;
      int curCfmt1 = this.GetCurCfmt(StartLine, StartCol1);
      if ((this.e.TerFont[curCfmt1].style & 6144) != 0)
      {
        if (!this.e.TerLocateStyleChar(1024 /*0x0400*/, true, ref StartLine, ref StartCol1, false))
          return false;
        curCfmt1 = this.GetCurCfmt(StartLine, StartCol1);
      }
      bool flag1 = StartLine == 0 && StartCol1 == 0;
      if ((this.e.TerFont[curCfmt1].style & 1024 /*0x0400*/) != 0 && !flag1)
      {
        if (!this.e.TerLocateStyleChar(1024 /*0x0400*/, false, ref StartLine, ref StartCol1, false))
          return false;
        ++StartCol1;
        if (StartCol1 + 1 > this.e.text[StartLine].len)
        {
          ++StartLine;
          StartCol1 = 0;
        }
      }
      this.e.HilightBegRow = StartLine;
      this.e.HilightBegCol = StartCol1;
      StartLine = this.e.HilightEndRow;
      int StartCol2 = this.e.HilightEndCol - 1;
      if (StartCol2 < 0)
      {
        --StartLine;
        if (StartLine < 0)
          StartLine = 0;
        StartCol2 = this.e.text[StartLine].len - 1;
        if (StartCol2 < 0)
          StartCol2 = 0;
      }
      bool flag2 = false;
      int curCfmt2 = this.GetCurCfmt(StartLine, StartCol2);
      if ((this.e.TerFont[curCfmt2].style & 1024 /*0x0400*/) != 0)
      {
        if (!this.e.TerLocateStyleChar(1024 /*0x0400*/, false, ref StartLine, ref StartCol2, true))
          return false;
        curCfmt2 = this.GetCurCfmt(StartLine, StartCol2);
        flag2 = true;
      }
      if ((this.e.TerFont[curCfmt2].style & 2048 /*0x0800*/) != 0)
      {
        if (!this.e.TerLocateStyleChar(2048 /*0x0800*/, false, ref StartLine, ref StartCol2, true))
          return false;
        curCfmt2 = this.GetCurCfmt(StartLine, StartCol2);
        flag2 = true;
      }
      if ((this.e.TerFont[curCfmt2].style & 4096 /*0x1000*/) != 0)
      {
        if (!this.e.TerLocateStyleChar(4096 /*0x1000*/, false, ref StartLine, ref StartCol2, true))
          return false;
        flag2 = true;
      }
      if (flag2)
      {
        this.e.HilightEndRow = StartLine;
        this.e.HilightEndCol = StartCol2;
        this.e.TerOpFlags |= 1073741824 /*0x40000000*/;
      }
    }
    return true;
  }

  internal int PictFromImage(Image image, int align, bool embed, string PictFile)
  {
    return this.PictFromImage(image, align, embed, PictFile, Size.Empty, 0);
  }

  internal int PictFromImage(
    Image image,
    int align,
    bool embed,
    string PictFile,
    Size imageSize,
    int offset)
  {
    int openSlot;
    if ((openSlot = this.FindOpenSlot()) == -1)
      return 0;
    this.e.TerFont[openSlot].InUse = true;
    this.e.TerFont[openSlot].image = image;
    this.e.TerFont[openSlot].PictType = 0;
    this.e.TerFont[openSlot].ImageType = image.RawFormat.Guid;
    this.e.TerFont[openSlot].ObjectType = 0;
    if (this.e.TerFont[openSlot].ImageType == ImageFormat.Wmf.Guid || this.e.TerFont[openSlot].ImageType == ImageFormat.Emf.Guid)
    {
      bool flag1 = false;
      bool flag2 = false;
      if (string.IsNullOrEmpty(PictFile) && image.Clone() is Metafile metafile)
        this.e.TerFont[openSlot].hMeta = metafile.GetHenhmetafile();
      if (!string.IsNullOrEmpty(PictFile))
      {
        BinaryReader binaryReader = new BinaryReader((Stream) File.OpenRead(PictFile));
        if (binaryReader.ReadUInt32() == 2596720087U)
        {
          int num1 = (int) binaryReader.ReadUInt16();
          int num2 = (int) binaryReader.ReadInt16();
          int num3 = (int) binaryReader.ReadInt16();
          int num4 = (int) binaryReader.ReadInt16();
          int num5 = (int) binaryReader.ReadInt16();
          int num6 = (int) binaryReader.ReadInt16();
          int num7 = num4 - num2;
          int num8 = num5 - num2;
          if (num6 > 0)
          {
            this.e.TerFont[openSlot].PictWidth = (int) ((double) num7 * 1440.0 / (double) num6);
            this.e.TerFont[openSlot].PictHeight = (int) ((double) num8 * 1440.0 / (double) num6);
            flag1 = true;
          }
          flag2 = true;
        }
        binaryReader.Close();
      }
      if (!flag1)
      {
        if (imageSize != Size.Empty)
        {
          this.e.TerFont[openSlot].PictWidth = imageSize.Width;
          this.e.TerFont[openSlot].PictHeight = imageSize.Height;
        }
        else
        {
          this.e.TerFont[openSlot].PictWidth = (int) ((double) image.Width * 1440.0 / (double) image.HorizontalResolution);
          this.e.TerFont[openSlot].PictHeight = (int) ((double) image.Height * 1440.0 / (double) image.VerticalResolution);
        }
      }
      if (this.e.TerFont[openSlot].ImageType == ImageFormat.Wmf.Guid & flag2)
      {
        FileStream fileStream = new FileStream(PictFile, FileMode.Open, FileAccess.Read);
        int num = 22;
        int length = (int) fileStream.Length - num;
        if (length > 0)
        {
          fileStream.Position = (long) num;
          byte[] numArray = new byte[length];
          fileStream.Read(numArray, 0, length);
          this.e.TerFont[openSlot].hMeta = COp.Win32.SetMetaFileBitsEx(length, numArray);
        }
        fileStream.Close();
      }
    }
    else
    {
      this.e.TerFont[openSlot].PictHeight = this.MulDiv(image.Height, 1440, this.e.OrigScrResY);
      this.e.TerFont[openSlot].PictWidth = this.MulDiv(image.Width, 1440, this.e.OrigScrResY);
    }
    this.e.TerFont[openSlot].style = 128 /*0x80*/;
    this.e.TerFont[openSlot].PictAlign = align;
    this.e.TerFont[openSlot].offset = offset;
    this.SetPictSize(openSlot, this.TwipsToScrY(this.e.TerFont[openSlot].PictHeight), this.TwipsToScrX(this.e.TerFont[openSlot].PictWidth), true);
    this.e.TerFont[openSlot].bmHeight = this.e.TerFont[openSlot].PictHeight;
    this.e.TerFont[openSlot].bmWidth = this.e.TerFont[openSlot].PictWidth;
    this.e.TerFont[openSlot].OrigPictHeight = this.e.TerFont[openSlot].PictHeight;
    this.e.TerFont[openSlot].OrigPictWidth = this.e.TerFont[openSlot].PictWidth;
    this.XlateSizeForPrt(openSlot);
    return openSlot;
  }

  internal new bool SetAnimTimer(int pict)
  {
    if (!this.e.InDialogBox && !this.e.InPrinting && !this.False(this.e.TerFont[pict].anim))
    {
      int index1 = this.e.TerFont[pict].anim.CurAnim;
      if (index1 == 0)
        index1 = pict;
      if (this.True(this.e.TerFont[pict].anim.TimerId))
        this.KillTimer(this.e.hTerWnd, this.e.TerFont[pict].anim.TimerId);
      this.e.TerFont[pict].anim.TimerId = 0;
      if (this.True(this.e.TerFont[index1].anim) && this.e.TerFont[pict].anim.LoopCount != -1)
      {
        int index2 = this.e.TerFont[index1].anim.NextPict;
        if ((this.e.TerOpFlags & 128 /*0x80*/) != 0)
          index2 = index1;
        if (index2 == 0)
        {
          if (this.e.TerFont[pict].anim.LoopCount == -2)
          {
            index2 = pict;
          }
          else
          {
            --this.e.TerFont[pict].anim.LoopCount;
            if (this.e.TerFont[pict].anim.LoopCount >= 0)
              index2 = pict;
          }
        }
        if (index2 > 0 && this.True(this.e.TerFont[index2].anim))
        {
          int id = 9199 + pict;
          this.e.TerFont[pict].anim.NextAnim = index2;
          this.SetTimer(this.e.hTerWnd, id, this.e.TerFont[index2].anim.delay * 10);
          this.e.TerFont[pict].anim.TimerId = id;
        }
      }
    }
    return true;
  }

  internal new bool SetPictSize(int pict, int height, int width, bool icon)
  {
    if (this.edit.HiddenText(pict))
    {
      int num;
      this.e.TerFont[pict].BaseHeight = num = 0;
      this.e.TerFont[pict].height = num;
      for (int index = 0; index < 256 /*0x0100*/; ++index)
        this.e.TerFont[pict].CharWidth[index] = 0;
      return true;
    }
    if (icon && this.e.TerFont[pict].FrameType != 0 && this.e.TerArg.PageMode && (this.e.ParaFrame[this.e.TerFont[pict].ParaFID].flags & 4194304 /*0x400000*/) == 0)
    {
      height = this.e.TerFont[0].height * 3 / 4;
      width = this.e.ShowParaMark ? this.fnt.LwrCharWidth(0, true, 'W') : 1;
    }
    if (height == 0)
      height = 1;
    if (width == 0)
      width = 1;
    if (this.e.TerFont[pict].TextAngle > 0)
      this.SwapInts(ref height, ref width);
    int num1;
    this.e.TerFont[pict].BaseHeight = num1 = height;
    this.e.TerFont[pict].height = num1;
    if (this.e.TerFont[pict].FieldId == 2)
    {
      tc.ClsForm form = this.e.TerFont[pict].form;
      if (this.True(form) && form.FontId >= 0)
      {
        this.e.TerFont[pict].BaseHeight = this.e.TerFont[form.FontId].BaseHeight;
        this.e.TerFont[pict].BaseHeight += (this.e.TerFont[pict].height - this.e.TerFont[form.FontId].height) / 2;
      }
      else
        this.e.TerFont[pict].BaseHeight = this.e.TerFont[pict].height * 2 / 3;
    }
    else if (this.e.TerFont[pict].FieldId == 3)
      this.e.TerFont[pict].BaseHeight = this.e.TerFont[pict].height * 7 / 8;
    else if (this.e.TerFont[pict].offset > 0)
    {
      int num2 = this.e.InPrinting ? this.TwipsToUnitY(this.e.TerFont[pict].offset) : this.TwipsToScrY(this.e.TerFont[pict].offset);
      this.e.TerFont[pict].BaseHeight = height - num2;
      if (this.e.TerFont[pict].BaseHeight < 0)
        this.e.TerFont[pict].BaseHeight = 0;
    }
    else
    {
      if (this.e.TerFont[pict].PictAlign == 1)
        this.e.TerFont[pict].BaseHeight = (height + this.e.TerFont[0].BaseHeight) / 2;
      if (this.e.TerFont[pict].PictAlign == 2)
        this.e.TerFont[pict].BaseHeight = this.e.TerFont[0].height;
      if (this.e.TerFont[pict].PictAlign != 0 && this.e.TerFont[pict].BaseHeight > height)
      {
        this.e.TerFont[pict].BaseHeight = this.e.TerFont[0].BaseHeight;
        if (this.e.TerFont[pict].BaseHeight > height)
          this.e.TerFont[pict].BaseHeight = height;
      }
    }
    this.e.TerFont[pict].CharWidth[0] = 0;
    for (int index = 1; index < 256 /*0x0100*/; ++index)
      this.e.TerFont[pict].CharWidth[index] = width;
    return true;
  }

  internal bool TerDeleteBlock(bool repaint) => this.TerDeleteBlock(repaint, false);

  internal bool TerDeleteBlock(bool repaint, bool forceDel)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (forceDel || !this.IsProtected(true, true))
    {
      ++this.e.TerArg.modified;
      if (this.e.HilightType == 1)
        return this.DeleteLineBlock(repaint);
      if (this.e.HilightType == 2)
        return this.DeleteCharBlock(true, repaint);
    }
    return false;
  }

  /// <summary>Удалить весь текст</summary>
  /// <param name="repaint">Перерисовать</param>
  /// <returns>true, если успешно</returns>
  internal bool TerDeleteAll(bool repaint)
  {
    if (this.e.TotalLines == 1 && this.e.text[0] != null && this.e.text[0].len == 0)
      return true;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    for (int line = 0; line < this.e.TotalLines; ++line)
      this.init.FreeLine(line);
    int num = 1;
    while (num < this.e.TotalCharTags)
      this.FreeTag(num++);
    this.AllocWrapBuf(0);
    if (this.e.TerFont != null)
    {
      for (int index = 0; index < this.e.TerFont.Length; ++index)
      {
        tc.StrFont strFont = this.e.TerFont[index];
        if (strFont.image != null)
          strFont.image.Dispose();
        if (strFont.hMeta != IntPtr.Zero)
          CRtfw.DeleteEnhMetaFile(strFont.hMeta);
        this.e.TerFont[index].image = (Image) null;
      }
    }
    this.e.TotalLines = 1;
    this.e.ViewPageHdrFtr = false;
    this.e.EditPageHdrFtr = false;
    this.e.CurLine = 0;
    this.e.CurCol = 0;
    this.init.InitLine(0);
    this.e.text[0].pfmt = 0;
    this.e.HilightType = 0;
    this.e.HilightBegRow = 0;
    this.e.HilightBegCol = 0;
    this.e.HilightEndRow = 0;
    this.e.HilightEndCol = 0;
    this.e.ProtectedFirstCharCount = 0;
    this.e.ProtectedFirstRealCharCount = 0;
    this.e.ProtectedEndCharCount = 0;
    this.e.ProtectedEndRealCharCount = 0;
    this.e.InputFontId = -1;
    ++this.e.TerArg.modified;
    return true;
  }

  internal bool TerDeleteObject(int idx)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (idx < 0 || idx >= this.e.TotalFonts || this.False(this.e.TerFont[idx].InUse))
      return false;
    this.DeleteTerObject(idx);
    return true;
  }

  internal char[] TerFileToMem(string file, out int pSize)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    pSize = 0;
    if (file.Length == 0)
      return (char[]) null;
    if (!File.Exists(file))
      return (char[]) null;
    StreamReader streamReader;
    if ((OurStreamReader) (streamReader = (StreamReader) new OurStreamReader(file)) == null)
      return (char[]) null;
    string end = streamReader.ReadToEnd();
    streamReader.Close();
    if (end == null)
      return (char[]) null;
    char[] charArray = end.ToCharArray();
    pSize = charArray.Length;
    return charArray;
  }

  internal byte[] TerFileToMemBytes(string file, out int pSize)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    pSize = 0;
    if (file.Length == 0)
      return (byte[]) null;
    if (!File.Exists(file))
      return (byte[]) null;
    byte[] buffer;
    try
    {
      FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
      pSize = (int) fileStream.Length;
      if (pSize == 0)
        return (byte[]) null;
      buffer = new byte[pSize];
      fileStream.Read(buffer, 0, pSize);
      fileStream.Close();
    }
    catch (IOException ex)
    {
      return (byte[]) null;
    }
    return buffer;
  }

  internal int TerGetBkPictId()
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    return this.e.BkPictId;
  }

  internal bool TerGetOrigPictSize(int pict, out int width, out int height)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int num;
    height = num = 0;
    width = num;
    if (pict >= 0 && pict <= this.e.TotalFonts && this.e.TerFont[pict].InUse)
    {
      width = this.e.TerFont[pict].OrigPictWidth;
      height = this.e.TerFont[pict].OrigPictHeight;
    }
    return true;
  }

  internal int TerGetPictCropping(int pict, int type)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (pict >= 0 && pict <= this.e.TotalFonts && !this.False(this.e.TerFont[pict].InUse))
    {
      switch (type)
      {
        case 1:
          return this.e.TerFont[pict].CropLeft;
        case 2:
          return this.e.TerFont[pict].CropRight;
        case 3:
          return this.e.TerFont[pict].CropTop;
        case 4:
          return this.e.TerFont[pict].CropBot;
      }
    }
    return -1;
  }

  internal int TerGetPictFrame(int pict)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    return pict < 0 || pict > this.e.TotalFonts || this.False(this.e.TerFont[pict].InUse) || (this.e.TerFont[pict].style & 128 /*0x80*/) == 0 ? 0 : this.e.TerFont[pict].FrameType;
  }

  internal bool TerGetPictInfo(
    int pict,
    out int style,
    out Rectangle OutRect,
    out int align,
    out int AuxId)
  {
    COp.RECT OurRect = new COp.RECT();
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    style = 0;
    int num1;
    AuxId = num1 = 0;
    align = num1;
    OutRect = new Rectangle();
    if (pict < 0 || pict > this.e.TotalFonts || this.False(this.e.TerFont[pict].InUse))
      return false;
    style = this.e.TerFont[pict].style;
    align = this.e.TerFont[pict].PictAlign;
    AuxId = this.e.TerFont[pict].AuxId;
    OurRect.left = this.e.TerFont[pict].PictX;
    OurRect.top = this.e.TerFont[pict].PictY;
    OurRect.left += this.e.TerWinRect.left;
    OurRect.top += this.e.TerWinRect.top;
    int scrX;
    int num2;
    if (this.e.TerFont[pict].FrameType == 0)
    {
      scrX = this.e.TerFont[pict].CharWidth[24];
      num2 = this.e.TerFont[pict].height;
    }
    else
    {
      scrX = this.TwipsToScrX(this.e.TerFont[pict].PictWidth);
      num2 = this.TwipsToScrX(this.e.TerFont[pict].PictHeight);
    }
    OurRect.right = OurRect.left + scrX;
    OurRect.bottom = OurRect.top + num2;
    OutRect = this.ToRectangle(OurRect);
    return true;
  }

  internal int TerGetPictOffset(int pict)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (pict < 0)
      pict = this.GetCurCfmt(this.e.CurLine, this.e.CurCol);
    return pict < 0 || pict > this.e.TotalFonts || this.False(this.e.TerFont[pict].InUse) ? -1 : this.e.TerFont[pict].offset;
  }

  internal int TerGetWordCount(int flags)
  {
    int num1 = 0;
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = true;
    int wordCount = 0;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if ((flags & 1) != 0 && this.e.HilightType != 0 && !this.NormalizeBlock())
      return -1;
    if ((flags & 1) == 0)
    {
      num1 = this.e.HilightType;
      this.e.HilightType = 0;
    }
    int pBegRow;
    int pBegCol;
    int pEndRow;
    int pEndCol;
    this.GetTextRange(out pBegRow, out pBegCol, out pEndRow, out pEndCol);
    if ((flags & 1) == 0)
      this.e.HilightType = num1;
    for (int line = pBegRow; line <= pEndRow; ++line)
    {
      if ((flags & 2) != 0 || (this.e.PfmtId[this.e.text[line].pfmt].flags & 12288 /*0x3000*/) == 0)
      {
        int num2 = line != pBegRow ? 0 : pBegCol;
        int num3 = line != pEndRow ? this.e.text[line].len : pEndCol;
        char[] txt = this.e.text[line].txt;
        ushort[] numArray = this.OpenCfmt(line);
        for (int index = num2; index < num3; ++index)
        {
          if ((flags & 4) != 0 || !this.edit.HiddenText((int) numArray[index]))
          {
            char chr = txt[index];
            int num4;
            switch (chr)
            {
              case '\t':
              case ' ':
                num4 = 1;
                break;
              default:
                if (!this.IsBreakChar(chr))
                {
                  num4 = (this.e.TerFont[(int) numArray[index]].style & 128 /*0x80*/) != 0 ? 1 : 0;
                  break;
                }
                goto case '\t';
            }
            flag2 = num4 != 0;
            if (flag2 && !flag1 && !flag3)
              ++wordCount;
            flag1 = flag2;
            flag3 = false;
          }
        }
        this.CloseCfmt(line);
      }
    }
    if (!flag2)
      ++wordCount;
    return wordCount;
  }

  internal bool TerInsertObjectId(int pict, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (pict < 0 || pict >= this.e.TotalFonts || !this.e.TerFont[pict].InUse || (this.e.TerFont[pict].style & 128 /*0x80*/) == 0)
      return false;
    int fid1 = this.e.text[this.e.CurLine].fid;
    if (this.e.TerArg.PageMode && (this.e.TerFlags & 524288 /*0x080000*/) != 0 && (fid1 == 0 || (this.e.ParaFrame[fid1].flags & 768 /*0x0300*/) != 0))
      this.e.TerInsertParaFrame(-1, -1, -1, -1, true);
    int fid2 = this.e.text[this.e.CurLine].fid;
    if (this.True(fid2) && (this.e.ParaFrame[fid2].flags & 768 /*0x0300*/) != 0)
    {
      this.InsertMarkerLine(this.e.CurLine, this.e.ParaChar, 0, 0, 0, 0);
      this.e.text[this.e.CurLine].fid = 0;
      this.e.CurCol = 0;
    }
    if (this.e.CurCol >= this.e.text[this.e.CurLine].len)
      this.e.CurCol = this.e.text[this.e.CurLine].len;
    if (this.e.CurCol < 0)
      this.e.CurCol = 0;
    this.MoveLineData(this.e.CurLine, this.e.CurCol, 1, 'B');
    this.e.text[this.e.CurLine].txt[this.e.CurCol] = '\u0018';
    this.OpenCfmt(this.e.CurLine)[this.e.CurCol] = (ushort) pict;
    this.CloseCfmt(this.e.CurLine);
    this.SaveUndo(this.e.CurLine, this.e.CurCol, this.e.CurLine, this.e.CurCol, 'O');
    this.FitPictureInFrame(this.e.CurLine, (this.e.TerFlags & 1) != 0);
    ++this.e.TerArg.modified;
    if (repaint)
      this.PaintTer();
    return true;
  }

  internal int TerInsertPictureFile(string FileName, bool embed, int align, bool insert)
  {
    string file = "";
    string PictFile = (string) null;
    tc.StrFont pPrevPict = new tc.StrFont();
    if (!this.e.TerCreateControl())
      return 0;
    if (FileName != null && FileName.Length == 0)
      FileName = (string) null;
    Image image;
    while (true)
    {
      if (this.True(FileName))
      {
        file = this.ResolveLinkFileName(FileName);
      }
      else
      {
        string filter = "" + this.e.MsgString[176 /*0xB0*/] + this.e.MsgString[177] + this.e.MsgString[178] + this.e.MsgString[179] + this.e.MsgString[174] + this.e.MsgString[175] + this.e.MsgString[198] + this.e.MsgString[199];
        if (!this.GetFileName(true, ref file, 1, filter, "BMP"))
          break;
      }
      try
      {
        image = Image.FromStream((Stream) new MemoryStream(this.e.TerFileToMemBytes(file, out tc.SkipInt)));
        goto label_13;
      }
      catch (Exception ex)
      {
        if (this.True(FileName))
          return 0;
        this.PrintError(69, nameof (TerInsertPictureFile));
      }
    }
    return -1;
label_13:
    if (this.e.NextFontId >= 0)
      pPrevPict = this.e.TerFont[this.e.NextFontId];
    bool flag = this.e.NextFontId >= 0;
    this.GetCurCfmt(this.e.CurLine, this.e.CurCol);
    if (!this.CanInsert(this.e.CurLine, this.e.CurCol) & insert && !flag)
    {
      this.MessageBeep(0);
      return 0;
    }
    int pict = this.PictFromImage(image, align, embed, PictFile);
    if (pict == 0)
      return 0;
    this.e.TerFont[pict].PictFile = (string) null;
    if (embed)
      this.e.TerFont[pict].PictData = this.FileToByteArray(file, out tc.SkipInt);
    if (insert && !flag)
      this.TerInsertObjectId(pict, false);
    else if (flag)
    {
      this.ApplyPrevPictProp(pict, pPrevPict);
      this.RequestPagination(true);
      this.FitPictureInFrame(this.e.CurLine, false);
    }
    if (embed)
      this.e.TerFont[pict].LinkFile = (string) null;
    else
      this.e.TerFont[pict].LinkFile = file;
    ++this.e.TerArg.modified;
    this.e.PaintFlag = 4;
    if (insert | flag)
      this.PaintTer();
    return pict;
  }

  internal int TerInsertPictureFileXY(
    string FileName,
    bool embed,
    int align,
    bool insert,
    int x,
    int y)
  {
    if (!this.e.TerCreateControl())
      return 0;
    int terFlags = this.e.TerFlags;
    this.e.TerFlags |= 524288 /*0x080000*/;
    this.TerMousePos((y << 16 /*0x10*/) + x, false);
    this.e.CurLine = this.e.MouseLine;
    this.e.CurCol = this.e.MouseCol;
    if (this.e.TerArg.PageMode && (this.e.TerFlags & 524288 /*0x080000*/) != 0 && this.e.text[this.e.CurLine].fid == 0)
    {
      this.e.NewFrameX = this.ScrToTwipsX(x - this.e.TerWinRect.left + this.e.TerWinOrgX);
      if (this.e.BorderShowing)
        this.e.NewFrameX -= this.UnitToTwipsX(this.GetBorderLeftSpace(this.e.CurPage));
      int x1 = y - this.e.TerWinRect.top + this.e.TerWinOrgY;
      if (this.e.NewFrameVPage)
      {
        this.e.NewFrameY = this.FrameToPageY(this.ScrToTwipsY(x1));
      }
      else
      {
        int paraFrameLine = this.GetParaFrameLine(this.e.CurLine);
        while (paraFrameLine < this.e.TotalLines && this.e.text[paraFrameLine].fid != 0)
          ++paraFrameLine;
        if (paraFrameLine == this.e.TotalLines)
          --paraFrameLine;
        int units = this.LineToUnits(paraFrameLine);
        this.e.NewFrameY = x1 - units;
        this.e.NewFrameY = this.ScrToTwipsY(this.e.NewFrameY);
      }
    }
    int num = this.TerInsertPictureFile(FileName, embed, align, insert);
    this.e.TerFlags = terFlags;
    return num;
  }

  internal bool TerMemToFileBytes(string file, byte[] data)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    file = file.Trim();
    if (file.Length == 0)
      return false;
    if (File.Exists(file))
      File.Delete(file);
    try
    {
      FileStream fileStream = new FileStream(file, FileMode.CreateNew, FileAccess.Write);
      fileStream.Write(data, 0, data.Length);
      fileStream.Close();
    }
    catch (Exception ex)
    {
      return false;
    }
    return true;
  }

  internal bool TerNormalizeBlock()
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    return this.NormalizeBlock();
  }

  internal int TerPastePicture(
    string format,
    Image image,
    int ParaFID,
    int align,
    bool insert,
    bool force = false)
  {
    return this.TerPastePicture(format, image, ParaFID, align, insert, true, force);
  }

  internal int TerPastePicture(
    string format,
    Image image,
    int ParaFID,
    int align,
    bool insert,
    bool update,
    bool force = false)
  {
    return this.TerPastePicture(format, image, ParaFID, align, insert, Size.Empty, 0, true, force);
  }

  internal int TerPastePicture(
    string format,
    Image image,
    int ParaFID,
    int align,
    bool insert,
    Size imageSize,
    int offset,
    bool update,
    bool force = false)
  {
    bool isStrikedOut = this.e.blk.IsStrikedOut;
    bool doubleStrikedOut = this.e.blk.IsDoubleStrikedOut;
    IDataObject dataObject = (IDataObject) null;
    tc.StrFont pPrevPict = new tc.StrFont();
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.e.HilightType == 2 & insert && this.TerDeleteBlock(update))
      --this.e.UndoRef;
    int line = this.e.CurLine;
    int col = this.e.CurCol;
    if (ParaFID > 0 && this.e.text[line].fid != ParaFID)
    {
      int index = 0;
      while (index < this.e.TotalLines && this.e.text[index].fid != ParaFID)
        ++index;
      if (index < this.e.TotalLines && this.e.text[index].len > 0)
      {
        line = index;
        col = 0;
      }
    }
    if (this.e.NextFontId >= 0)
      pPrevPict = this.e.TerFont[this.e.NextFontId];
    bool flag = this.e.NextFontId >= 0;
    this.GetCurCfmt(line, col);
    if (!(this.CanInsert(this.e.CurLine, this.e.CurCol) | force) & insert && !flag)
    {
      this.MessageBeep(0);
      return 0;
    }
    if (format == null)
      format = "";
    if (format == "" && image == null && dataObject != null)
    {
      if (dataObject.GetDataPresent(DataFormats.EnhancedMetafile))
        format = DataFormats.EnhancedMetafile;
      else if (dataObject.GetDataPresent(DataFormats.Bitmap))
        format = DataFormats.Bitmap;
      else if (dataObject.GetDataPresent(DataFormats.MetafilePict))
        format = DataFormats.MetafilePict;
    }
    if (format == "" && image == null)
      return 0;
    if (image == null && (DataObject) (dataObject = Clipboard.GetDataObject()) == null)
    {
      this.PrintError(9, "");
      return 0;
    }
    this.e.ImgDenX = this.e.ScrResX;
    this.e.ImgDenY = this.e.ScrResY;
    int num = -1;
    if (image == null)
    {
      if (format != DataFormats.MetafilePict && format != DataFormats.EnhancedMetafile && format != DataFormats.Bitmap)
        return num;
      try
      {
        image = (Image) dataObject.GetData(format, true);
      }
      catch (Exception ex)
      {
      }
      try
      {
        if (image == null)
          image = (Image) dataObject.GetData(DataFormats.EnhancedMetafile, true);
      }
      catch (Exception ex)
      {
      }
      try
      {
        if (image == null)
          image = (Image) dataObject.GetData(DataFormats.MetafilePict, true);
      }
      catch (Exception ex)
      {
      }
      try
      {
        if (image == null)
          image = (Image) dataObject.GetData(DataFormats.Bitmap, true);
      }
      catch (Exception ex)
      {
      }
      if (image == null)
      {
        foreach (string format1 in dataObject.GetFormats())
        {
          object data = dataObject.GetData(format1, true);
          if (data != null)
          {
            if (data.GetType() == new MemoryStream().GetType())
            {
              try
              {
                image = Image.FromStream((Stream) data, true);
              }
              catch (Exception ex)
              {
              }
            }
          }
          if (image != null)
            break;
        }
        if (image == null)
          return num;
      }
    }
    int pict = this.PictFromImage(image, align, true, (string) null, imageSize, offset);
    if (pict <= 0)
    {
      this.PrintError(11, nameof (TerPastePicture));
      return 0;
    }
    if (flag)
    {
      this.ApplyPrevPictProp(pict, pPrevPict);
      if (update)
      {
        this.RequestPagination(true);
        this.PaintTer();
      }
    }
    else if (insert)
    {
      this.e.CurLine = line;
      this.e.CurCol = col;
      this.TerInsertObjectId(pict, update);
      this.FitPictureInFrame(this.e.CurLine, false);
    }
    if (isStrikedOut)
      this.e.TerFont[pict].style |= 8;
    if (doubleStrikedOut)
      this.e.TerFont[pict].style |= 524288 /*0x080000*/;
    return pict;
  }

  internal new bool TerPasteSpecial()
  {
    if (this.CallDialogBox((Form) new terdlg_paste_spec(this.e)))
      this.CopyFromClipboard(this.e.DlgText, (DataObject) null);
    return true;
  }

  internal bool TerPictAltInfo(int pict, bool get, ref string info)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (pict < 0 || pict >= this.e.TotalFonts || !this.e.TerFont[pict].InUse || (this.e.TerFont[pict].style & 128 /*0x80*/) == 0)
      return false;
    if (get)
      info = this.e.TerFont[pict].PictAlt;
    else
      this.e.TerFont[pict].PictAlt = info;
    return true;
  }

  internal bool TerPictLinkName(int pict, bool get, ref string name)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (pict < 0 || pict >= this.e.TotalFonts || !this.e.TerFont[pict].InUse || (this.e.TerFont[pict].style & 128 /*0x80*/) == 0)
      return false;
    if (get)
    {
      name = !this.True(this.e.TerFont[pict].LinkFile) ? "" : this.e.TerFont[pict].LinkFile;
    }
    else
    {
      this.e.TerFont[pict].LinkFile = (string) null;
      if (this.True(name) && name.Length > 0)
        this.e.TerFont[pict].LinkFile = name;
    }
    return true;
  }

  internal bool TerSavePict(int pict, string FileName, ImageFormat fmt)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (pict < 0 || pict >= this.e.TotalFonts || !this.e.TerFont[pict].InUse || (this.e.TerFont[pict].style & 128 /*0x80*/) == 0 || this.IsControl(pict) || this.e.TerFont[pict].image == null)
      return false;
    if (tc.InServer && this.e.WebFolder.Length > 0 && FileName.IndexOf("\\") < 0 && FileName.IndexOf(":") < 0)
      FileName = $"{this.e.WebFolder}\\{FileName}";
    if (fmt == ImageFormat.Jpeg && this.e.TerFont[pict].image.RawFormat.Guid == ImageFormat.Jpeg.Guid && this.e.TerFont[pict].PictData != null)
      return this.TerMemToFileBytes(FileName, this.e.TerFont[pict].PictData);
    this.e.TerFont[pict].image.Save(FileName, fmt);
    return true;
  }

  internal bool TerSetBkPictId(int id, int flag, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (id < 0)
    {
      id = this.TerInsertPictureFile((string) null, true, 0, false);
      if (id < 0)
        return false;
    }
    if (id < 0 || id >= this.e.TotalFonts || this.False(this.e.TerFont[id].InUse) || (this.e.TerFont[id].style & 128 /*0x80*/) == 0 && id != 0)
      return false;
    if (this.True(this.e.BkPictId))
      this.DeleteTerObject(this.e.BkPictId);
    if (this.True(this.e.BkPictBM))
      this.DisposeBkPictBM();
    this.e.BkPictId = id;
    this.e.BkPictFlag = flag;
    if (this.True(this.e.BkPictId) && this.e.BkPictFlag == 1)
      this.SetPictSize(this.e.BkPictId, this.e.TerWinHeight, this.e.TerWinWidth, true);
    if (repaint)
      this.e.TerRepaint(true);
    return true;
  }

  internal bool TerSetPictCropping(
    int pict,
    int CropLeft,
    int CropTop,
    int CropRight,
    int CropBot,
    bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (pict >= 0 && pict <= this.e.TotalFonts && !this.False(this.e.TerFont[pict].InUse))
    {
      int num1 = CropLeft + CropRight - this.e.TerFont[pict].CropLeft - this.e.TerFont[pict].CropRight;
      int num2 = CropTop + CropBot - this.e.TerFont[pict].CropTop - this.e.TerFont[pict].CropBot;
      this.e.TerFont[pict].CropLeft = CropLeft;
      this.e.TerFont[pict].CropRight = CropRight;
      this.e.TerFont[pict].CropTop = CropTop;
      this.e.TerFont[pict].CropBot = CropBot;
      int x1 = this.e.TerFont[pict].PictWidth -= num1;
      int x2 = this.e.TerFont[pict].PictHeight -= num2;
      this.e.TerFont[pict].PctWidth = 0;
      this.SetPictSize(pict, this.TwipsToScrY(x2), this.TwipsToScrX(x1), true);
      this.e.TerFont[pict].flags |= 4096 /*0x1000*/;
      this.XlateSizeForPrt(pict);
      ++this.e.TerArg.modified;
      if (repaint)
        this.PaintTer();
    }
    return true;
  }

  internal int TerSetPictFrame(int pict, int FrameType, bool repaint)
  {
    return this.TerSetPictFrame2(pict, FrameType, 2000, 2000, repaint);
  }

  internal int TerSetPictFrame2(int pict, int FrameType, int x, int y, bool repaint)
  {
    int pCol = 0;
    int pLineNo = 0;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (pict < 0 || pict > this.e.TotalFonts || this.False(this.e.TerFont[pict].InUse) || (this.e.TerFont[pict].style & 128 /*0x80*/) == 0 || FrameType != 0 && FrameType != 1 && FrameType != 2 && FrameType != 3)
      return -1;
    int paraFid;
    if (FrameType == 0)
    {
      if (this.e.TerFont[pict].FrameType == 0)
        return 0;
      paraFid = this.e.TerFont[pict].ParaFID;
      if (this.True(paraFid) && paraFid < this.e.TotalParaFrames && this.e.ParaFrame[paraFid].InUse)
        this.e.ParaFrame[paraFid].InUse = false;
      this.e.TerFont[pict].ParaFID = 0;
    }
    else
    {
      if (this.e.TerFont[pict].FrameType == 0)
      {
        int paraFrameSlot;
        if ((paraFrameSlot = this.GetParaFrameSlot()) < 0)
          return -1;
        this.e.ParaFrame[paraFrameSlot] = new tc.StrParaFrame();
        this.e.ParaFrame[paraFrameSlot].InUse = true;
        this.e.ParaFrame[paraFrameSlot].DistFromText = 180;
        this.e.ParaFrame[paraFrameSlot].margin = 0;
        this.e.ParaFrame[paraFrameSlot].PageNo = this.e.CurPage;
        this.e.TerFont[pict].ParaFID = paraFrameSlot;
      }
      this.e.TerFont[pict].FrameType = FrameType;
      paraFid = this.e.TerFont[pict].ParaFID;
      this.e.ParaFrame[paraFid].pict = pict;
      this.e.ParaFrame[paraFid].ShapeType = 75;
      this.e.ParaFrame[paraFid].width = this.e.TerFont[pict].PictWidth + 2 * this.e.ParaFrame[paraFid].margin;
      this.e.ParaFrame[paraFid].height = this.e.TerFont[pict].PictHeight + 2 * this.e.ParaFrame[paraFid].margin;
      this.e.ParaFrame[paraFid].MinHeight = this.e.ParaFrame[paraFid].height;
      if (FrameType == 3)
      {
        this.e.ParaFrame[paraFid].flags |= 32 /*0x20*/;
        int num1;
        this.e.ParaFrame[paraFid].y = num1 = y;
        this.e.ParaFrame[paraFid].ParaY = num1;
        this.e.ParaFrame[paraFid].x = x;
        this.e.ParaFrame[paraFid].OrgX = x;
        int index1 = 0;
        if (this.e.TerLocateFontId(pict, ref pLineNo, ref pCol))
          index1 = this.GetSection(pLineNo);
        this.e.ParaFrame[paraFid].x -= (int) this.InchesToTwips((double) this.e.TerSect[index1].LeftMargin);
        this.e.ParaFrame[paraFid].OrgX -= (int) this.InchesToTwips((double) this.e.TerSect[index1].LeftMargin);
        pLineNo = this.UnitsToLine(0, this.TwipsToScrY(this.PageToFrameY(y)));
        int index2 = pLineNo;
        while (index2 >= 0 && (this.e.text[index2].fid > 0 || this.e.text[index2].cid > 0 || (this.e.text[index2].flags & 1966080 /*0x1E0000*/) != 0 || this.e.text[index2].page >= this.e.CurPage && (this.e.text[index2].flags & 4) == 0))
          --index2;
        if (index2 < 0 || this.e.text[index2].page < this.e.CurPage)
        {
          if (index2 < 0)
            index2 = 0;
          while (index2 < this.e.TotalLines && (this.e.text[index2].fid > 0 || this.e.text[index2].cid > 0 || (this.e.text[index2].flags & 1966080 /*0x1E0000*/) != 0))
            ++index2;
          if (index2 == this.e.TotalLines)
          {
            int num2 = this.e.TotalLines - 1;
          }
        }
        this.AnchorPictFrame(pict, pLineNo, 0);
      }
      else
        tc.ResetUintFlag(ref this.e.ParaFrame[paraFid].flags, 96 /*0x60*/);
    }
    this.SetPictSize(pict, this.TwipsToScrY(this.e.TerFont[pict].PictHeight), this.TwipsToScrX(this.e.TerFont[pict].PictWidth), true);
    this.XlateSizeForPrt(pict);
    if (repaint)
      this.Repaginate(false, true, 0, true);
    return paraFid;
  }

  internal bool TerSetPictInfo(int pict, int style, int align, int AuxId)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (pict >= 0 && pict <= this.e.TotalFonts && !this.False(this.e.TerFont[pict].InUse))
    {
      this.e.TerFont[pict].style = style;
      this.e.TerFont[pict].AuxId = AuxId;
      if (align != this.e.TerFont[pict].PictAlign)
      {
        this.e.TerFont[pict].PictAlign = align;
        this.SetPictSize(pict, this.e.TerFont[pict].height, this.e.TerFont[pict].CharWidth[24], true);
      }
    }
    return true;
  }

  internal bool TerSetPictOffset(int pict, int offset, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (pict < 0)
      pict = this.GetCurCfmt(this.e.CurLine, this.e.CurCol);
    if (pict < 0 || pict > this.e.TotalFonts || this.False(this.e.TerFont[pict].InUse) || (this.e.TerFont[pict].style & 128 /*0x80*/) == 0)
      return false;
    this.e.TerFont[pict].offset = offset;
    this.e.TerFont[pict].PictAlign = 0;
    this.SetPictSize(pict, this.TwipsToScrY(this.e.TerFont[pict].PictHeight), this.TwipsToScrX(this.e.TerFont[pict].PictWidth), true);
    this.XlateSizeForPrt(pict);
    ++this.e.TerArg.modified;
    if (repaint)
      this.PaintTer();
    return true;
  }

  internal bool TerSetPictSize(int pict, int width, int height)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (pict < 0 || pict > this.e.TotalFonts || !this.e.TerFont[pict].InUse || (this.e.TerFont[pict].style & 128 /*0x80*/) == 0)
      return false;
    int pictWidth = this.e.TerFont[pict].PictWidth;
    int pictHeight1 = this.e.TerFont[pict].PictHeight;
    if (width == 0 && height != -1)
      width = height < 0 ? height : height * pictWidth / pictHeight1;
    if (width != -1)
    {
      this.e.TerFont[pict].PictWidth = width >= 0 ? this.ScrToTwipsX(width) : -width;
      this.e.TerFont[pict].PctWidth = 0;
    }
    if (height == 0 && width != -1)
      height = width < 0 ? width : width * pictHeight1 / pictWidth;
    if (height != -1)
      this.e.TerFont[pict].PictHeight = height >= 0 ? this.ScrToTwipsY(height) : -height;
    if (height != -1 || width != -1)
    {
      this.SetPictSize(pict, this.TwipsToScrY(this.e.TerFont[pict].PictHeight), this.TwipsToScrX(this.e.TerFont[pict].PictWidth), true);
      this.e.TerFont[pict].flags |= 4096 /*0x1000*/;
      this.XlateSizeForPrt(pict);
      if (this.e.TerFont[pict].FrameType != 0)
      {
        int paraFid = this.e.TerFont[pict].ParaFID;
        this.e.ParaFrame[paraFid].width = this.e.TerFont[pict].PictWidth;
        int pictHeight2;
        this.e.ParaFrame[paraFid].height = pictHeight2 = this.e.TerFont[pict].PictHeight;
        this.e.ParaFrame[paraFid].MinHeight = pictHeight2;
        if (!this.e.InRtfRead)
          this.e.TerRepaginate(true);
      }
    }
    return true;
  }

  internal bool TerSetWatermarkPict(int id, bool wash, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (id < 0)
    {
      id = this.TerInsertPictureFile((string) null, true, 0, false);
      if (id < 0)
        return false;
    }
    if (id < 0 || id >= this.e.TotalFonts || !this.e.TerFont[id].InUse || (this.e.TerFont[id].style & 128 /*0x80*/) == 0 && id != 0)
      return false;
    this.e.WmWashed = wash;
    if (this.e.WmParaFID > 0)
    {
      int index;
      for (index = 0; index < this.e.TotalFrames; ++index)
      {
        if (this.e.frame[index].ParaFrameId == this.e.WmParaFID)
        {
          this.e.frame[index].ParaFrameId = 0;
          tc.ResetUintFlag(ref this.e.frame[index].flags, 2097152 /*0x200000*/);
          break;
        }
      }
      if (index < this.e.TotalFrames)
      {
        int pict = this.e.ParaFrame[index].pict;
        if (pict > 0)
          this.DeleteTerObject(pict);
        this.e.ParaFrame[index].InUse = false;
      }
      this.e.WmParaFID = 0;
      if (this.e.WmImageAttr != null)
      {
        this.e.WmImageAttr.Dispose();
        this.e.WmImageAttr = (ImageAttributes) null;
      }
    }
    if (id > 0)
    {
      this.e.WmParaFID = this.TerSetPictFrame2(id, 3, 0, 0, false);
      if (this.e.WmParaFID <= 0)
        return false;
      this.e.ParaFrame[this.e.WmParaFID].flags |= 4210688 /*0x404000*/;
      if (wash)
        this.ApplyPictureBrightnessContrast(id, 22938, 19661);
      this.SetPictSize(id, this.TwipsToScrY(this.e.TerFont[id].PictHeight), this.TwipsToScrX(this.e.TerFont[id].PictWidth), true);
      this.XlateSizeForPrt(id);
      this.PosWatermarkFrame(this.e.CurPage);
    }
    ++this.e.TerArg.modified;
    this.RefreshFrames(true);
    if (repaint)
      this.Repaginate(false, true, 0, true);
    return true;
  }

  internal new bool XlateSizeForPrt(int FontIdx)
  {
    if (this.e.TerArg.PrintView)
    {
      this.e.PrtFont[FontIdx].height = this.MulDiv(this.e.TerFont[FontIdx].height, this.e.UnitResY, this.e.ScrResY);
      this.e.PrtFont[FontIdx].BaseHeight = this.MulDiv(this.e.TerFont[FontIdx].BaseHeight, this.e.UnitResY, this.e.ScrResY);
      for (int index = 0; index < 256 /*0x0100*/; ++index)
        this.e.PrtFont[FontIdx].CharWidth[index] = this.e.TerFont[FontIdx].CharWidth[index] * this.e.UnitResX / this.e.ScrResX;
      this.e.PrtFont[FontIdx].ExtLead = this.MulDiv(this.e.TerFont[FontIdx].ExtLead, this.e.UnitResY, this.e.ScrResY);
    }
    return true;
  }
}
