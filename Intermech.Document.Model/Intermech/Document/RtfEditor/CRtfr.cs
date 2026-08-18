// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.CRtfr
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class CRtfr : COp
{
  internal CRtfr(ImRtfEditor ImRtfEditor)
    : base(ImRtfEditor)
  {
    this.e = ImRtfEditor;
  }

  internal new bool ApplyPictureBrightnessContrast(int pict, int bright, int contrast)
  {
    if (this.e.WmImageAttr != null)
      this.e.WmImageAttr.Dispose();
    this.e.WmImageAttr = new ImageAttributes();
    ColorMatrix newColorMatrix = new ColorMatrix();
    float num1 = (float) bright / 32768f;
    float num2;
    newColorMatrix[4, 2] = num2 = num1;
    float num3;
    newColorMatrix[4, 1] = num3 = num2;
    newColorMatrix[4, 0] = num3;
    this.e.WmImageAttr.SetColorMatrix(newColorMatrix);
    return true;
  }

  internal bool ApplyRtfTagId(tc.ClsRtf rtf, int InsLine)
  {
    if (this.e.text[InsLine].len != 0)
    {
      this.OpenCtid(InsLine)[0] = (ushort) rtf.TagId;
      this.CloseCtid(InsLine);
      rtf.TagId = 0;
    }
    return true;
  }

  internal bool BuildRtfAnimSeq(ref tc.StrRtfGroup group, tc.StrRtfPict pic, int pict)
  {
    int firstAnimPict = group.rtf.FirstAnimPict;
    return this.False(this.e.TerFont[firstAnimPict].InUse) || (this.e.TerFont[firstAnimPict].style & 128 /*0x80*/) == 0 || this.BuildRtfPicture(ref group, pic, pict);
  }

  internal bool BuildRtfPictFrame(ref tc.StrRtfGroup group, int pict)
  {
    tc.ClsRtf rtf = group.rtf;
    int paraFrameSlot;
    if ((paraFrameSlot = this.GetParaFrameSlot()) < 0)
      return false;
    this.e.ParaFrame[paraFrameSlot] = new tc.StrParaFrame();
    this.e.ParaFrame[paraFrameSlot].InUse = true;
    this.e.ParaFrame[paraFrameSlot].DistFromText = 180;
    this.e.ParaFrame[paraFrameSlot].margin = 0;
    this.e.TerFont[pict].ParaFID = paraFrameSlot;
    if (group.shape.align == 1024 /*0x0400*/)
      this.e.TerFont[pict].FrameType = 1;
    else if (group.shape.align == 2)
      this.e.TerFont[pict].FrameType = 2;
    else
      this.e.TerFont[pict].FrameType = 3;
    if ((group.shape.FrmFlags & 97) != 0)
      this.e.TerFont[pict].FrameType = 3;
    int paraFid = this.e.TerFont[pict].ParaFID;
    this.e.ParaFrame[paraFid].pict = pict;
    if (this.e.TerFont[pict].PictType == 11)
    {
      this.e.TerFont[pict].PictWidth = group.shape.width;
      this.e.TerFont[pict].PictHeight = group.shape.height;
      this.e.ParaFrame[paraFid].margin = 0;
      this.e.ParaFrame[paraFid].ShapeType = group.shape.type;
    }
    if (this.e.TerFont[pict].FrameType != 0)
      this.e.ParaFrame[paraFid].ShapeType = group.shape.type;
    this.e.ParaFrame[paraFid].width = this.e.TerFont[pict].PictWidth + 2 * this.e.ParaFrame[paraFid].margin;
    this.e.ParaFrame[paraFid].height = this.e.TerFont[pict].PictHeight + 2 * this.e.ParaFrame[paraFid].margin;
    this.e.ParaFrame[paraFid].MinHeight = this.e.ParaFrame[paraFid].height;
    int frmFlags = group.shape.FrmFlags;
    if (group.shape.WrapType == 1)
      frmFlags |= 8192 /*0x2000*/;
    else if (group.shape.WrapType == 3)
      frmFlags |= 16512;
    this.e.ParaFrame[paraFid].ParaY = group.shape.y;
    this.e.ParaFrame[paraFid].OrgY = group.shape.y;
    this.e.ParaFrame[paraFid].GroupY = group.shape.y;
    this.e.ParaFrame[paraFid].x = group.shape.x;
    this.e.ParaFrame[paraFid].OrgX = group.shape.x;
    this.e.ParaFrame[paraFid].GroupX = group.shape.x;
    if ((frmFlags & 1) != 0)
      this.e.ParaFrame[paraFid].x -= (int) this.InchesToTwips((double) rtf.sect.LeftMargin);
    this.e.ParaFrame[paraFid].ZOrder = group.shape.ZOrder;
    this.e.ParaFrame[paraFid].BackColor = tc.CLR_WHITE;
    this.e.ParaFrame[paraFid].flags = frmFlags;
    this.SetPictSize(pict, this.TwipsToScrY(this.e.TerFont[pict].PictHeight), this.TwipsToScrX(this.e.TerFont[pict].PictWidth), true);
    this.XlateSizeForPrt(pict);
    rtf.flags |= 4096 /*0x1000*/;
    if (this.e.TerFont[pict].PictType == 11)
      this.e.RtfParaFID = paraFid;
    rtf.flags2 |= 8;
    rtf.PictFID = paraFid;
    return true;
  }

  internal bool BuildRtfPicture(ref tc.StrRtfGroup group, tc.StrRtfPict pic, int pict)
  {
    tc.ClsRtf rtf = group.rtf;
    this.e.TerFont[pict].InUse = true;
    this.e.TerFont[pict].PictType = pic.type;
    this.e.TerFont[pict].ImageType = pic.ImageType;
    this.e.TerFont[pict].ObjectType = 0;
    this.e.TerFont[pict].style = 128 /*0x80*/ | group.style;
    this.e.TerFont[pict].AuxId = group.AuxId;
    this.e.TerFont[pict].PictAlign = pic.align;
    this.e.TerFont[pict].PictData = pic.data;
    this.e.TerFont[pict].offset = group.offset;
    this.e.TerFont[pict].CropLeft = pic.CropLeft;
    this.e.TerFont[pict].CropRight = pic.CropRight;
    this.e.TerFont[pict].CropTop = pic.CropTop;
    this.e.TerFont[pict].CropBot = pic.CropBot;
    this.e.TerFont[pict].OrigPictWidth = pic.width;
    this.e.TerFont[pict].OrigPictHeight = pic.height;
    this.e.TerFont[pict].image = pic.image;
    this.e.TerFont[pict].hMeta = pic.hMeta;
    if (pic.hMeta == IntPtr.Zero && pic.image != null && pic.image.Clone() is Metafile metafile)
      this.e.TerFont[pict].hMeta = metafile.GetHenhmetafile();
    if (this.e.TerFont[pict].PictAlign == 0 && (group.style & 32 /*0x20*/) != 0)
      this.e.TerFont[pict].PictAlign = 1;
    if (this.True(group.FieldId))
    {
      this.e.TerFont[pict].FieldId = group.FieldId;
      if (this.True(rtf.FieldCode))
        this.e.TerFont[pict].FieldCode = rtf.FieldCode;
    }
    int pictType = this.e.TerFont[pict].PictType;
    if (this.True(pic.ScaleY))
    {
      pic.height = this.MulDiv(pic.height, pic.ScaleY, 100);
      pic.CropTop = this.MulDiv(pic.CropTop, pic.ScaleY, 100);
      pic.CropBot = this.MulDiv(pic.CropBot, pic.ScaleY, 100);
    }
    if (this.True(pic.ScaleX))
    {
      pic.width = this.MulDiv(pic.width, pic.ScaleX, 100);
      pic.CropLeft = this.MulDiv(pic.CropLeft, pic.ScaleX, 100);
      pic.CropRight = this.MulDiv(pic.CropRight, pic.ScaleX, 100);
    }
    if (pic.CropTop != 0)
      pic.height -= pic.CropTop;
    if (pic.CropBot != 0)
      pic.height -= pic.CropBot;
    if (pic.CropLeft != 0)
      pic.width -= pic.CropLeft;
    if (pic.CropRight != 0)
      pic.width -= pic.CropRight;
    this.e.TerFont[pict].PictHeight = pic.height;
    this.e.TerFont[pict].PictWidth = pic.width;
    switch (pictType)
    {
      case 2:
        this.e.TerFont[pict].form = pic.form;
        this.RealizeControl(pict, (Control) null);
        this.XlateSizeForPrt(pict);
        break;
      case 6:
        this.e.TerFont[pict].form = pic.form;
        this.e.TerFont[pict].FieldId = pic.FormId;
        if (this.e.TerFont[pict].FieldId == 2)
        {
          rtf.pict = pict;
          break;
        }
        this.RealizeControl(pict, (Control) null);
        this.XlateSizeForPrt(pict);
        break;
      default:
        this.e.TerFont[pict].bmWidth = pic.OrigWidth;
        this.e.TerFont[pict].bmHeight = pic.OrigHeight;
        this.SetPictSize(pict, this.TwipsToScrY(this.e.TerFont[pict].PictHeight), this.TwipsToScrX(this.e.TerFont[pict].PictWidth), true);
        this.XlateSizeForPrt(pict);
        break;
    }
    return true;
  }

  internal bool CopyRtfRow(tc.ClsRtf rtf, tc.StrRtfGroup[] group, int CurGroup)
  {
    if ((rtf.flags & 24) == 0)
    {
      if (this.e.RtfCurRowId <= 0)
        return this.SetRtfRowDefault(rtf, group, CurGroup);
      int CurRowId = this.e.TableRow[this.e.RtfCurRowId].NextRow;
      if (rtf.PastingColumn && CurRowId <= 0 && rtf.InitTblCol >= 0)
      {
        rtf.SuspendReading = true;
        return true;
      }
      if (CurRowId <= 0 && (CurRowId = this.GetTableRowSlot()) < 0)
      {
        this.PrintError(112 /*0x70*/, nameof (CopyRtfRow));
        rtf.flags |= 8;
        return true;
      }
      int rtfCurRowId = this.e.RtfCurRowId;
      this.e.RtfCurRowId = CurRowId;
      bool flag = rtf.PastingColumn && rtf.InitTblCol >= 0 && (this.e.TableRow[this.e.RtfCurRowId].flags & 16384 /*0x4000*/) != 0;
      if (!flag)
      {
        this.e.TableRow[CurRowId] = this.e.TableRow[rtfCurRowId].Copy();
        this.e.TableAux[CurRowId] = this.e.TableAux[rtfCurRowId];
        this.e.TableRow[rtfCurRowId].NextRow = CurRowId;
        this.e.TableRow[CurRowId].PrevRow = rtfCurRowId;
        this.e.TableRow[CurRowId].NextRow = -1;
        this.e.TableRow[CurRowId].flags = tc.ResetUintFlag(ref this.e.TableRow[CurRowId].flags, 49152 /*0xC000*/);
        tc.ResetUintFlag(ref this.e.TableAux[CurRowId].flags, 16 /*0x10*/);
        this.e.TableAux[CurRowId].flags |= 36;
      }
      int index1 = this.e.TableRow[rtfCurRowId].FirstCell;
      while (index1 > 0 && (this.e.cell[index1].flags & 1024 /*0x0400*/) != 0)
        index1 = this.e.cell[index1].NextCell;
      if (index1 <= 0)
        index1 = this.e.TableRow[rtfCurRowId].FirstCell;
      int CurCell = -1;
      if (flag)
      {
        if (rtf.InitTblCol == 0)
        {
          CurCell = -1;
        }
        else
        {
          int initTblCol = rtf.InitTblCol;
          for (CurCell = this.e.TableRow[CurRowId].FirstCell; initTblCol > 1 && this.e.cell[CurCell].NextCell > 0; CurCell = this.e.cell[CurCell].NextCell)
            initTblCol -= this.e.cell[CurCell].ColSpan;
        }
      }
      else
      {
        int num;
        this.e.TableRow[CurRowId].LastCell = num = -1;
        this.e.TableRow[CurRowId].FirstCell = num;
      }
      this.e.RtfCurCellId = this.e.RtfLastCellX = 0;
      while (index1 > 0 && (this.e.cell[index1].flags & 1024 /*0x0400*/) == 0)
      {
        int cellSlot;
        if ((cellSlot = this.GetCellSlot(false)) <= 0)
        {
          this.PrintError(101, nameof (CopyRtfRow));
          rtf.flags |= 16 /*0x10*/;
          return true;
        }
        this.CopyCell(index1, cellSlot);
        this.e.cell[cellSlot].row = CurRowId;
        int num;
        this.e.cell[cellSlot].ColSpan = num = 1;
        this.e.cell[cellSlot].RowSpan = num;
        tc.ResetUintFlag(ref this.e.CellAux[cellSlot].flags, 8);
        if (this.e.cell[cellSlot].level > 0)
          tc.ResetUintFlag(ref this.e.cell[cellSlot].flags, 4);
        this.InsertCell(cellSlot, CurCell, CurRowId, 'A');
        CurCell = cellSlot;
        if (this.e.RtfCurCellId == 0)
          this.e.RtfCurCellId = cellSlot;
        this.e.RtfLastCellX = cellSlot;
        if ((this.e.cell[index1].flags & 4) != 0)
        {
          int prevCell = this.e.cell[index1].PrevCell;
          int nextCell = this.e.cell[index1].NextCell;
          if (prevCell > 0)
          {
            this.e.cell[prevCell].NextCell = nextCell;
            this.e.cell[prevCell].width += this.e.cell[index1].width;
            ++this.e.cell[prevCell].ColSpan;
            tc.ResetUintFlag(ref this.e.cell[prevCell].border, 8);
            this.e.cell[prevCell].border |= this.e.cell[index1].border & 8;
            this.e.cell[prevCell].BorderWidth[3] = this.e.cell[index1].BorderWidth[3];
            this.e.cell[prevCell].BorderColor[3] = this.e.cell[index1].BorderColor[3];
          }
          if (nextCell > 0)
            this.e.cell[nextCell].PrevCell = prevCell;
          if (this.e.TableRow[rtfCurRowId].LastCell == index1)
            this.e.TableRow[rtfCurRowId].LastCell = prevCell;
          for (int index2 = 0; index2 < this.e.TotalLines; ++index2)
          {
            if (this.e.text[index2].cid == prevCell)
            {
              int len = this.e.text[index2].len;
              if (len > 0 && (int) this.e.text[index2].txt[len - 1] == (int) this.e.CellChar)
              {
                this.TransferTags(index2, len - 1);
                this.LineAlloc(index2, len, len - 1);
              }
            }
            if (this.e.text[index2].cid == index1)
              this.e.text[index2].cid = prevCell;
          }
          this.DelCell(index1);
          index1 = nextCell;
        }
        else
          index1 = this.e.cell[index1].NextCell;
      }
      if (rtf.PastingColumn && rtf.InitTblCol >= 0 && (this.e.TableRow[rtfCurRowId].flags & 16384 /*0x4000*/) != 0)
        rtf.InsertAftCell = this.e.RtfCurCellId <= 0 || this.e.cell[this.e.RtfCurCellId].PrevCell <= 0 ? this.e.TableRow[rtfCurRowId].LastCell : this.e.cell[this.e.RtfCurCellId].PrevCell;
      else
        rtf.InitTblCol = -1;
      rtf.PrevCellX = 0;
    }
    return true;
  }

  internal bool CopyToOutBuf(tc.ClsRtf rtf)
  {
    tc.StrRtfGroup[] group = rtf.group;
    if (group[rtf.GroupLevel].IgnoreCount > 0)
    {
      int startIndex = group[rtf.GroupLevel].IgnoreCount > rtf.WordLen ? rtf.WordLen : group[rtf.GroupLevel].IgnoreCount;
      rtf.CurWord = rtf.CurWord.Substring(startIndex);
      rtf.WordLen = rtf.CurWord.Length;
      group[rtf.GroupLevel].IgnoreCount -= startIndex;
    }
    if (rtf.WordLen > 0)
    {
      if ((rtf.OutBufLen + rtf.WordLen > 80 /*0x50*/ || rtf.OutBufHasUnicode) && !this.SendRtfText(rtf))
        return false;
      rtf.OutBuf += rtf.CurWord;
      rtf.OutBufLen += rtf.WordLen;
    }
    return true;
  }

  internal bool CreateRtfCell(tc.ClsRtf rtf, tc.StrRtfGroup[] group, int CurGroup)
  {
    bool flag1 = false;
    if ((rtf.flags & 24) == 0)
    {
      if (this.e.RtfCurRowId == 0)
      {
        if (rtf.OpenRowId <= 0)
          return this.PrintError(6, (string) null);
        this.e.RtfCurRowId = rtf.OpenRowId;
        this.e.RtfCurCellId = rtf.OpenCellId;
        this.e.RtfLastCellX = rtf.OpenLastCellX;
      }
      int NewCell = -1;
      if ((this.e.TableAux[this.e.RtfCurRowId].flags & 2) != 0)
      {
        if (this.e.RtfLastCellX == 0)
        {
          NewCell = this.e.TableRow[this.e.RtfCurRowId].FirstCell;
        }
        else
        {
          NewCell = this.e.cell[this.e.RtfLastCellX].NextCell;
          if (NewCell > 0 && (this.e.cell[NewCell].flags & 1024 /*0x0400*/) != 0)
            NewCell = -1;
        }
      }
      if (NewCell <= 0)
      {
        if ((NewCell = this.GetCellSlot(false)) <= 0)
        {
          this.PrintError(101, (string) null);
          rtf.flags |= 16 /*0x10*/;
          return true;
        }
        flag1 = true;
        this.e.cell[NewCell].InUse = true;
        this.e.cell[NewCell].row = this.e.RtfCurRowId;
        int num1;
        this.e.cell[NewCell].LastLine = num1 = -1;
        this.e.cell[NewCell].FirstLine = num1;
        this.e.cell[NewCell].level = rtf.CurTblLevel - 1;
        int curCellId;
        this.e.cell[NewCell].ParentCell = curCellId = rtf.TblLevel[rtf.CurTblLevel - 1].CurCellId;
        if (this.e.HtmlMode)
          this.e.cell[NewCell].flags |= 256 /*0x0100*/;
        if (curCellId > 0)
          this.e.TableRow[this.e.cell[curCellId].row].flags |= 8;
        int num2;
        this.e.cell[NewCell].NextCell = num2 = -1;
        this.e.cell[NewCell].PrevCell = num2;
      }
      int cellMargin;
      if ((group[CurGroup].gflags & 1048576 /*0x100000*/) != 0)
      {
        cellMargin = group[CurGroup].CellMargin;
        this.e.cell[NewCell].flags |= 32768 /*0x8000*/;
      }
      else
        cellMargin = this.e.TableRow[this.e.RtfCurRowId].CellMargin;
      int num3 = Math.Max(Math.Max(cellMargin, group[CurGroup].BorderWidth[6]), group[CurGroup].BorderWidth[7]);
      this.e.cell[NewCell].margin = num3;
      this.e.cell[NewCell].border = 0;
      if (this.True(group[CurGroup].BorderWidth[4]))
        this.e.cell[NewCell].border |= 1;
      if (this.True(group[CurGroup].BorderWidth[5]))
        this.e.cell[NewCell].border |= 2;
      if (this.True(group[CurGroup].BorderWidth[6]))
        this.e.cell[NewCell].border |= 4;
      if (this.True(group[CurGroup].BorderWidth[7]))
        this.e.cell[NewCell].border |= 8;
      if ((this.e.cell[NewCell].border & 1) != 0)
        this.e.cell[NewCell].BorderWidth[0] = group[CurGroup].BorderWidth[4];
      if ((this.e.cell[NewCell].border & 2) != 0)
        this.e.cell[NewCell].BorderWidth[1] = group[CurGroup].BorderWidth[5];
      if ((this.e.cell[NewCell].border & 4) != 0)
        this.e.cell[NewCell].BorderWidth[2] = group[CurGroup].BorderWidth[6];
      if ((this.e.cell[NewCell].border & 8) != 0)
        this.e.cell[NewCell].BorderWidth[3] = group[CurGroup].BorderWidth[7];
      this.e.cell[NewCell].BorderColor[0] = group[CurGroup].BorderColor[4];
      this.e.cell[NewCell].BorderColor[1] = group[CurGroup].BorderColor[5];
      this.e.cell[NewCell].BorderColor[2] = group[CurGroup].BorderColor[6];
      this.e.cell[NewCell].BorderColor[3] = group[CurGroup].BorderColor[7];
      for (int index = 0; index < 4; ++index)
      {
        group[CurGroup].BorderWidth[4 + index] = 0;
        group[CurGroup].BorderColor[4 + index] = tc.CLR_AUTO;
      }
      this.e.cell[NewCell].shading = group[CurGroup].CellShading;
      this.e.cell[NewCell].BackColor = group[CurGroup].CellPatBC;
      if (group[CurGroup].CellShading > 0 && group[CurGroup].CellPatFC != Color.Black)
      {
        int cellShading = group[CurGroup].CellShading;
        Color cellPatFc = group[CurGroup].CellPatFC;
        Color cellPatBc = group[CurGroup].CellPatBC;
        this.e.cell[NewCell].BackColor = this.GetShadedColor(cellPatFc, cellPatBc, cellShading);
        this.e.cell[NewCell].shading = 0;
      }
      if (group[CurGroup].CellColSpan > 0)
        this.e.cell[NewCell].ColSpan = group[CurGroup].CellColSpan;
      this.e.cell[NewCell].TextAngle = group[CurGroup].TextAngle;
      group[CurGroup].CellShading = 0;
      group[CurGroup].CellPatBC = tc.CLR_WHITE;
      group[CurGroup].CellPatFC = Color.Black;
      group[CurGroup].CellColSpan = 1;
      group[CurGroup].TextAngle = 0;
      group[CurGroup].CellMargin = 0;
      tc.ResetUintFlag(ref group[CurGroup].gflags, 1048576 /*0x100000*/);
      int flag2 = 94208 /*0x017000*/;
      this.e.cell[NewCell].flags = tc.ResetUintFlag(ref this.e.cell[NewCell].flags, flag2);
      this.e.cell[NewCell].flags |= group[CurGroup].CellFlags & flag2;
      group[CurGroup].CellFlags = tc.ResetUintFlag(ref group[CurGroup].CellFlags, flag2);
      if (this.e.RtfCurCellId == 0)
        this.e.RtfCurCellId = NewCell;
      if (flag1)
        this.InsertCell(NewCell, this.e.RtfLastCellX, this.e.RtfCurRowId, 'A');
      int num4 = this.e.RtfLastCellX <= 0 ? this.e.TableRow[this.e.RtfCurRowId].indent : this.e.cell[this.e.RtfLastCellX].x + this.e.cell[this.e.RtfLastCellX].width;
      this.e.cell[NewCell].x = num4;
      int intParam = rtf.IntParam;
      if ((this.e.TableRow[this.e.RtfCurRowId].flags & 16384 /*0x4000*/) != 0)
        this.e.cell[NewCell].width = intParam - rtf.PrevCellX;
      else
        this.e.cell[NewCell].width = intParam - num4 + this.e.TableRow[this.e.RtfCurRowId].AddedIndent;
      this.e.cell[NewCell].FixWidth = this.e.cell[NewCell].width;
      rtf.PrevCellX = intParam;
      this.e.RtfLastCellX = NewCell;
      if ((rtf.flags & 1) != 0)
      {
        this.e.cell[NewCell].flags |= 4;
        rtf.flags = tc.ResetUintFlag(ref rtf.flags, 1);
      }
      else
        tc.ResetUintFlag(ref this.e.cell[NewCell].flags, 4);
      if ((rtf.flags1 & 4096 /*0x1000*/) != 0)
      {
        this.e.cell[NewCell].flags |= 16 /*0x10*/;
        rtf.flags1 = tc.ResetUintFlag(ref rtf.flags1, 4096 /*0x1000*/);
      }
      else
        tc.ResetUintFlag(ref this.e.cell[NewCell].flags, 16 /*0x10*/);
    }
    return true;
  }

  internal bool DeleteRtfRow(tc.ClsRtf rtf, int row)
  {
    int prevRow = this.e.TableRow[row].PrevRow;
    int index = this.e.TableRow[row].FirstCell;
    if (prevRow > 0)
      this.e.TableRow[prevRow].NextRow = -1;
    this.e.TableRow[row].InUse = false;
    for (; index > 0; index = this.e.cell[index].NextCell)
      this.e.cell[index].InUse = false;
    return true;
  }

  internal bool ExitRtfGroup(tc.ClsRtf rtf, int CurGroup)
  {
    if (rtf.group[CurGroup].RtfGroup == CurGroup)
    {
      tc.StrRtfGroup[] group = rtf.group;
      for (int index = 0; index < this.e.TotalSID; ++index)
      {
        if (this.e.StyleId[index].RtfIndex != -1 && this.e.StyleId[index].next != 0)
        {
          int next = this.e.StyleId[index].next;
          if (next < 0 || next >= group[CurGroup].MaxRtfSID)
            this.e.StyleId[index].next = 0;
          else
            this.e.StyleId[index].next = group[CurGroup].RtfSID[next];
        }
      }
      group[CurGroup].font = (tc.StrRtfFont[]) null;
      group[CurGroup].color = (tc.StrRtfColor[]) null;
      group[CurGroup].RtfSID = (int[]) null;
    }
    return true;
  }

  internal int ExtractRtfPict(tc.ClsRtf rtf)
  {
    int groupLevel = rtf.GroupLevel;
    while (this.GetRtfWord(rtf))
    {
      if (!rtf.GroupBegin)
      {
        if (rtf.GroupEnd)
        {
          if (rtf.GroupLevel < groupLevel)
            return 0;
        }
        else if (rtf.IsControlWord && this.strcmpi(rtf.CurWord, "pict") == 0)
        {
          int x = this.ReadRtfPicture(rtf);
          if (this.True(x))
            return x;
        }
      }
    }
    return 1;
  }

  internal bool FixNegativeIndents(tc.ClsRtf rtf)
  {
    int num1 = 0;
    if ((this.e.TerFlags4 & 268435456 /*0x10000000*/) == 0)
    {
      if (this.e.TerArg.PageMode && !this.e.TerArg.FittedView)
      {
        int index = 0;
        while (index < this.e.TotalTableRows && (!this.e.TableRow[index].InUse || this.e.TableRow[index].AddedIndent >= 108))
          ++index;
        if (index == this.e.TotalTableRows)
          return true;
      }
      for (int index = 0; index < this.e.TotalCells; ++index)
      {
        if (this.e.cell[index].InUse)
        {
          int num2 = 0;
          if (this.e.cell[index].PrevCell > 0)
          {
            if ((this.e.cell[index].border & 4) != 0 && this.e.cell[index].margin < this.e.cell[index].BorderWidth[2])
              num2 = this.e.cell[index].BorderWidth[2] / 2;
            int prevCell = this.e.cell[index].PrevCell;
            if ((this.e.cell[prevCell].border & 8) != 0 && num2 < this.e.cell[prevCell].BorderWidth[3] / 2)
              num2 = this.e.cell[prevCell].BorderWidth[3] / 2;
          }
          else if ((this.e.cell[index].border & 4) != 0)
            num2 = this.e.cell[index].BorderWidth[2];
          if (this.e.cell[index].NextCell > 0)
          {
            if ((this.e.cell[index].border & 8) != 0 && this.e.cell[index].margin < this.e.cell[index].BorderWidth[3])
              num2 = this.e.cell[index].BorderWidth[3] / 2;
            int nextCell = this.e.cell[index].NextCell;
            if ((this.e.cell[nextCell].border & 4) != 0 && num2 < this.e.cell[nextCell].BorderWidth[2] / 2)
              num2 = this.e.cell[nextCell].BorderWidth[2] / 2;
          }
          else if ((this.e.cell[index].border & 8) != 0)
            num2 = this.e.cell[index].BorderWidth[3];
          if (this.e.cell[index].margin < num2)
            this.e.cell[index].margin = num2;
        }
      }
      for (int index = 0; index < this.e.TotalPfmts; ++index)
      {
        if ((this.e.PfmtId[index].pflags & 3) == 0)
        {
          int leftIndentTwips = this.e.PfmtId[index].LeftIndentTwips;
          if (this.e.PfmtId[index].FirstIndentTwips < 0)
            leftIndentTwips += this.e.PfmtId[index].FirstIndentTwips;
          if (leftIndentTwips < num1)
            num1 = leftIndentTwips;
        }
      }
      for (int index = 0; index < this.e.TotalTableRows; ++index)
      {
        if (this.e.TableRow[index].InUse && this.e.TableRow[index].AddedIndent != 0 && this.e.cell[this.e.TableRow[index].FirstCell].level <= 0)
        {
          int num3 = -this.e.TableRow[index].AddedIndent;
          if (num3 < num1)
            num1 = num3;
        }
      }
      if ((rtf.flags2 & 1) == 0)
      {
        for (int index = 0; index < this.e.TotalParaFrames; ++index)
        {
          if ((this.e.ParaFrame[index].flags & 12) == 0 && this.e.ParaFrame[index].x < num1)
            num1 = this.e.ParaFrame[index].x;
        }
      }
      if (num1 >= 0)
        return true;
      int x = -num1;
      float leftMargin = this.e.TerSect[0].LeftMargin;
      for (int index = 1; index < this.e.TotalSects; ++index)
      {
        if ((double) this.e.TerSect[index].LeftMargin < (double) leftMargin)
          leftMargin = this.e.TerSect[index].LeftMargin;
      }
      float num4 = leftMargin - 0.25f;
      if ((double) num4 < 0.0)
        num4 = 0.0f;
      if (x > (int) ((double) num4 * 1440.0))
        x = (int) ((double) num4 * 1440.0);
      for (int index = 0; index < this.e.TotalPfmts; ++index)
      {
        if ((this.e.PfmtId[index].pflags & 3) == 0)
        {
          this.e.PfmtId[index].LeftIndentTwips += x;
          this.e.PfmtId[index].LeftIndent = this.TwipsToScrX(this.e.PfmtId[index].LeftIndentTwips);
        }
      }
      for (int index = 0; index < this.e.TotalSects; ++index)
      {
        this.e.TerSect[index].LeftMargin -= this.TwipsToInches(x);
        if ((double) this.e.TerSect[index].LeftMargin < 0.0)
          this.e.TerSect[index].LeftMargin = 0.0f;
      }
      for (int index = 0; index < this.e.TotalParaFrames; ++index)
      {
        if ((this.e.ParaFrame[index].flags & 8) != 0)
          this.e.ParaFrame[index].x += x / 2;
        else
          this.e.ParaFrame[index].x += x;
      }
      for (int index = 0; index < this.e.TotalTableRows; ++index)
      {
        int firstCell = this.e.TableRow[index].FirstCell;
        if (this.e.TableRow[index].InUse && this.e.cell[firstCell].InUse && this.e.cell[firstCell].level == 0)
        {
          this.e.TableRow[index].indent += x - this.e.TableRow[index].AddedIndent;
          this.e.TableRow[index].AddedIndent = 0;
        }
      }
      if (this.e.RtfInput < 2)
      {
        for (int index1 = 0; index1 < this.e.TotalLines; ++index1)
        {
          if (!this.True(this.e.text[index1].cid) && !this.True(this.e.text[index1].fid))
          {
            int tabId = this.e.PfmtId[this.e.text[index1].pfmt].TabId;
            if (tabId != 0)
            {
              tc.StrTab TabRec = this.e.TerTab[tabId].Copy();
              for (int index2 = 0; index2 < TabRec.count; ++index2)
              {
                int[] pos;
                IntPtr index3;
                (pos = TabRec.pos)[(int) (index3 = (IntPtr) index2)] = pos[(int) index3] + x;
              }
              int num5 = this.NewTabId(tabId, TabRec);
              tc.StrPfmt pNew = this.e.PfmtId[this.e.text[index1].pfmt].Copy() with
              {
                TabId = num5
              };
              this.e.text[index1].pfmt = this.NewParaId2(this.e.text[index1].pfmt, pNew);
            }
          }
        }
      }
    }
    return true;
  }

  internal bool FmtRtfFootnoteNbr(tc.ClsRtf rtf, out string str, int nbr, int NumFmt)
  {
    str = "";
    switch (NumFmt)
    {
      case 1:
        str = this.AlphaFormat(nbr, true);
        break;
      case 2:
        str = this.AlphaFormat(nbr, false);
        break;
      case 3:
        str = this.romanize(nbr, true);
        break;
      case 4:
        str = this.romanize(nbr, false);
        break;
      default:
        str = nbr.ToString();
        break;
    }
    return true;
  }

  internal bool GetRtfChar(tc.ClsRtf rtf)
  {
    if (rtf.StackLen > 0)
    {
      --rtf.StackLen;
      rtf.CurChar = rtf.stack[rtf.StackLen];
      ++rtf.FilePos;
      return true;
    }
    rtf.eof = false;
    if (rtf.iFile != null)
    {
      if (rtf.TextIndex < rtf.TextLen)
      {
        rtf.CurChar = rtf.text[rtf.TextIndex];
        ++rtf.TextIndex;
        ++rtf.FilePos;
        return true;
      }
      byte[] buffer = new byte[1000];
      rtf.TextLen = rtf.iFile.Read(buffer, 0, 1000);
      for (int index = 0; index < rtf.TextLen; ++index)
        rtf.text[index] = (char) buffer[index];
      if (rtf.TextLen == 0)
      {
        rtf.eof = true;
        return false;
      }
      rtf.TextIndex = 0;
      rtf.CurChar = rtf.text[rtf.TextIndex];
      ++rtf.TextIndex;
      ++rtf.FilePos;
      return true;
    }
    if (rtf.BufLen >= 0 && rtf.BufIndex >= rtf.BufLen)
    {
      rtf.eof = true;
      return false;
    }
    rtf.CurChar = rtf.buf[rtf.BufIndex];
    ++rtf.BufIndex;
    ++rtf.FilePos;
    return true;
  }

  internal bool GetRtfDefaultFont(tc.ClsRtf rtf)
  {
    tc.StrRtfGroup rtfGroup1 = this.GetRtfGroup(rtf);
    if ((rtfGroup1.gflags2 & 2) == 0)
    {
      int defFont = rtf.group[rtf.GroupLevel].DefFont;
      int index;
      if (defFont >= 0 && defFont < 500)
      {
        index = defFont;
      }
      else
      {
        index = 500;
        while (index < rtfGroup1.MaxRtfFonts && (!rtfGroup1.font[index].InUse || rtfGroup1.font[index].FontId != defFont))
          ++index;
      }
      for (int rtfGroup2 = rtf.group[rtf.GroupLevel].RtfGroup; rtfGroup2 <= rtf.GroupLevel; ++rtfGroup2)
      {
        rtf.group[rtfGroup2].DefFont = index;
        rtf.group[rtfGroup2].gflags2 |= 2;
      }
    }
    int defFont1 = rtf.group[rtf.GroupLevel].DefFont;
    rtf.group[rtf.GroupLevel].TypeFace = rtfGroup1.font[defFont1].name;
    rtf.group[rtf.GroupLevel].FontFamily = rtfGroup1.font[defFont1].family;
    rtf.group[rtf.GroupLevel].CharSet = rtfGroup1.font[defFont1].CharSet;
    return true;
  }

  internal int GetRtfFontId(tc.ClsRtf rtf, tc.StrRtfGroup group)
  {
    bool flag = false;
    if ((group.style & 4096 /*0x1000*/) != 0 && (group.style & 2048 /*0x0800*/) == 0 && this.e.FootnoteRestFont > 0 && this.e.FootnoteRestFont < this.e.TotalFonts)
    {
      group.style = group.style & 39936 | this.e.TerFont[this.e.FootnoteRestFont].style & -39937;
      group.TextColor = this.e.TerFont[this.e.FootnoteRestFont].TextColor;
      group.TextBkColor = this.e.TerFont[this.e.FootnoteRestFont].TextBkColor;
      group.UlineColor = this.e.TerFont[this.e.FootnoteRestFont].UlineColor;
      group.PointSize2 = this.e.TerFont[this.e.FootnoteRestFont].TwipsSize / 10;
      group.TypeFace = this.e.TerFont[this.e.FootnoteRestFont].TypeFace;
      this.e.FootnoteRestFont = 0;
    }
    int textAngle = this.e.cell[this.e.RtfCurCellId].TextAngle;
    if (this.e.RtfCurCellId != 0)
      ;
    string str2 = group.TypeFace;
    if (str2.Length == 0)
      str2 = this.e.TerArg.FontTypeFace;
    int x1 = group.PointSize2 * 10;
    if (x1 == 0)
      x1 = 240 /*0xF0*/;
    if (group.CharScaleX > 0)
      x1 = this.MulDiv(x1, group.CharScaleX, 100);
    int num1 = group.CharSet;
    if (rtf.OutBufHasUnicode)
      flag = true;
    else if (group.CharType == 2)
      flag = true;
    if (flag && (group.TypeFaceHi == null || group.TypeFaceHi.Length == 0))
      flag = false;
    if (flag)
    {
      str2 = group.TypeFaceHi;
      num1 = group.CharSetHi;
    }
    if (group.rtlch && num1 != 178 && num1 != 177)
    {
      num1 = group.PrevCharSet;
      if (num1 != 178 && num1 != 177)
        num1 = 177;
    }
    group.PrevCharSet = num1;
    int num2 = group.style;
    if (rtf.InitFieldId == 2)
    {
      num2 = rtf.InitStyle;
      group.AuxId = rtf.InitAuxId;
    }
    int x2 = rtf.InitFieldId <= 0 ? group.FieldId : rtf.InitFieldId;
    if (group.lang == this.e.DefLang)
      group.lang = 0;
    Color color = group.CharBkPat == 0 || this.IsSameColor(group.CharPatBC, tc.CLR_WHITE) ? group.TextBkColor : group.CharPatBC;
    if (!group.revised)
    {
      group.InsRev = 0;
      group.InsTime = (tc.ClsDateTime) null;
    }
    if (!group.deleted)
    {
      group.DelRev = 0;
      group.DelTime = (tc.ClsDateTime) null;
    }
    int index1 = 0;
    while (index1 < this.e.TotalFonts && (!this.e.TerFont[index1].InUse || this.e.TerFont[index1].TwipsSize != x1 || this.e.TerFont[index1].style != num2 || !(this.e.TerFont[index1].TextColor == group.TextColor) || !(this.e.TerFont[index1].TextBkColor == color) || !(this.e.TerFont[index1].UlineColor == group.UlineColor) || this.e.TerFont[index1].CharStyId != group.CharStyId || this.e.TerFont[index1].ParaStyId != group.ParaStyId || this.e.TerFont[index1].expand != group.expand || this.e.TerFont[index1].FieldId != x2 || this.e.TerFont[index1].AuxId != group.AuxId || this.e.TerFont[index1].CharId != group.CharId || this.e.TerFont[index1].offset != group.offset || this.e.TerFont[index1].TextAngle != group.TextAngle || this.e.TerFont[index1].TempStyle != 0 || x2 > 0 && !this.IsSameFieldCode(this.e.TerFont[index1].FieldCode, rtf.FieldCode) || (int) this.e.TerFont[index1].CharSet != num1 && num1 != 1 || this.strcmpi(this.e.TerFont[index1].TypeFace, str2) != 0 || this.e.TerFont[index1].InsRev != group.InsRev || !object.Equals((object) this.e.TerFont[index1].InsTime, (object) group.InsTime) || this.e.TerFont[index1].DelRev != group.DelRev || !object.Equals((object) this.e.TerFont[index1].DelTime, (object) group.DelTime)))
      ++index1;
    int rtfFontId = index1;
    if (rtfFontId == this.e.TotalFonts)
    {
      int index2 = 0;
      while (index2 < this.e.TotalFonts && !this.False(this.e.TerFont[index2].InUse))
        ++index2;
      rtfFontId = index2;
      if (rtfFontId == this.e.TotalFonts)
      {
        if (this.e.TotalFonts == 320)
        {
          rtfFontId = 0;
        }
        else
        {
          if (this.e.TotalFonts >= this.e.MaxFonts)
          {
            int NewMaxFonts = this.e.MaxFonts + this.e.MaxFonts / 3 + 1;
            if (NewMaxFonts <= this.e.TotalFonts)
              NewMaxFonts = this.e.TotalFonts + 1;
            this.ExpandFontTable(NewMaxFonts);
          }
          rtfFontId = this.e.TotalFonts;
          ++this.e.TotalFonts;
        }
      }
      if (rtfFontId == 0)
        return rtfFontId;
      this.InitTerObject(rtfFontId);
      this.e.TerFont[rtfFontId].InUse = true;
      this.e.TerFont[rtfFontId].TypeFace = str2;
      this.e.TerFont[rtfFontId].TwipsSize = x1;
      this.e.TerFont[rtfFontId].TextColor = group.TextColor;
      this.e.TerFont[rtfFontId].TextBkColor = color;
      this.e.TerFont[rtfFontId].UlineColor = group.UlineColor;
      this.e.TerFont[rtfFontId].style = num2;
      this.e.TerFont[rtfFontId].FieldId = x2;
      this.e.TerFont[rtfFontId].AuxId = group.AuxId;
      this.e.TerFont[rtfFontId].CharId = group.CharId;
      this.e.TerFont[rtfFontId].lang = group.lang;
      this.e.TerFont[rtfFontId].offset = group.offset;
      this.e.TerFont[rtfFontId].TextAngle = group.TextAngle;
      this.e.TerFont[rtfFontId].CharStyId = group.CharStyId;
      this.e.TerFont[rtfFontId].ParaStyId = group.ParaStyId;
      this.e.TerFont[rtfFontId].expand = group.expand;
      this.e.TerFont[rtfFontId].CharSet = (byte) num1;
      if (this.True(x2) && this.True(rtf.FieldCode))
        this.e.TerFont[rtfFontId].FieldCode = rtf.FieldCode;
      else
        this.e.TerFont[rtfFontId].FieldCode = (string) null;
      string str1 = group.FontFamily;
      if (flag && group.FontFamilyHi != null && group.FontFamilyHi.Length > 0)
        str1 = group.FontFamilyHi;
      byte num3 = this.strcmpi(str1, "Roman") != 0 ? (this.strcmpi(str1, "swiss") != 0 ? (this.strcmpi(str1, "modern") != 0 ? (this.strcmpi(str1, "script") != 0 ? (this.strcmpi(str1, "decor") != 0 ? (byte) 0 : (byte) 80 /*0x50*/) : (byte) 64 /*0x40*/) : (byte) 48 /*0x30*/) : (byte) 32 /*0x20*/) : (byte) 16 /*0x10*/;
      this.e.TerFont[rtfFontId].FontFamily = num3;
      this.e.TerFont[rtfFontId].InsRev = group.InsRev;
      this.e.TerFont[rtfFontId].InsTime = group.InsTime == null ? (tc.ClsDateTime) null : group.InsTime.Copy();
      this.e.TerFont[rtfFontId].DelRev = group.DelRev;
      this.e.TerFont[rtfFontId].DelTime = group.DelTime == null ? (tc.ClsDateTime) null : group.DelTime.Copy();
      if (!this.CreateOneFont(this.e.TerGr, rtfFontId, true))
        rtfFontId = 0;
    }
    return rtfFontId;
  }

  internal tc.StrRtfGroup GetRtfGroup(tc.ClsRtf rtf) => this.GetRtfGroup(rtf, out tc.SkipInt);

  internal tc.StrRtfGroup GetRtfGroup(tc.ClsRtf rtf, out int RtfGroup)
  {
    int groupLevel = rtf.GroupLevel;
    RtfGroup = rtf.group[groupLevel].RtfGroup;
    return rtf.group[RtfGroup];
  }

  internal bool GetRtfHexChar(tc.ClsRtf rtf)
  {
    while (this.GetRtfChar(rtf))
    {
      if (rtf.CurChar != '\n' && rtf.CurChar != '\r' && rtf.CurChar != ' ')
      {
        if (rtf.CurChar >= '0' && rtf.CurChar <= '9')
          rtf.CurChar -= '0';
        else if (rtf.CurChar >= 'A' && rtf.CurChar <= 'F')
        {
          rtf.CurChar = (char) ((int) rtf.CurChar - 65 + 10);
        }
        else
        {
          if (rtf.CurChar < 'a' || rtf.CurChar > 'f')
            return false;
          rtf.CurChar = (char) ((int) rtf.CurChar - 97 + 10);
        }
        byte num = (byte) ((uint) (byte) rtf.CurChar << 4);
        while (this.GetRtfChar(rtf))
        {
          if (rtf.CurChar != '\n' && rtf.CurChar != '\r' && rtf.CurChar != ' ')
          {
            if (rtf.CurChar >= '0' && rtf.CurChar <= '9')
              rtf.CurChar -= '0';
            else if (rtf.CurChar >= 'A' && rtf.CurChar <= 'F')
            {
              rtf.CurChar = (char) ((int) rtf.CurChar - 65 + 10);
            }
            else
            {
              if (rtf.CurChar < 'a' || rtf.CurChar > 'f')
                return false;
              rtf.CurChar = (char) ((int) rtf.CurChar - 97 + 10);
            }
            rtf.CurChar = (char) ((uint) num + (uint) rtf.CurChar);
            return true;
          }
        }
        return false;
      }
    }
    return false;
  }

  internal int GetRtfInsertionLine(tc.ClsRtf rtf)
  {
    tc.StrRtfGroup strRtfGroup = rtf.group[rtf.GroupLevel];
    if (!this.CheckLineLimit(this.e.TotalLines + 1))
    {
      this.PrintError(86, this.e.MsgString[88]);
      return -1;
    }
    int rtfInsertionLine = this.e.CurLine;
    if ((rtf.flags & 32768 /*0x8000*/) != 0 || (strRtfGroup.gflags & 16384 /*0x4000*/) != 0)
    {
      while (rtfInsertionLine > 0 && (this.e.text[rtfInsertionLine - 1].flags & 3) == 0 && this.e.text[rtfInsertionLine - 1].fid <= 0)
        --rtfInsertionLine;
    }
    else if (rtf.InsertBefCell > 0)
    {
      int level = this.e.cell[rtf.InsertAftCell].level;
      int index = 0;
      while (index < this.e.TotalLines && this.LevelCell(level, -this.e.text[index].cid) != rtf.InsertBefCell)
        ++index;
      if (index < this.e.TotalLines)
        rtfInsertionLine = index;
      this.e.CurLine = rtfInsertionLine;
    }
    else if (rtf.InsertAftCell > 0)
    {
      int level = this.e.cell[rtf.InsertAftCell].level;
      int index = 0;
      while (index < this.e.TotalLines && this.LevelCell(level, -this.e.text[index].cid) != rtf.InsertAftCell)
        ++index;
      if (index < this.e.TotalLines)
      {
        while (index < this.e.TotalLines && this.LevelCell(level, -this.e.text[index].cid) == rtf.InsertAftCell)
          ++index;
        if (index < this.e.TotalLines)
          rtfInsertionLine = index;
      }
      this.e.CurLine = rtfInsertionLine;
    }
    int num;
    rtf.InsertAftCell = num = 0;
    rtf.InsertBefCell = num;
    if (this.e.RtfInput >= 2 && this.e.RtfInput != 5 || rtfInsertionLine != this.e.CurLine)
    {
      this.MoveLineArrays(rtfInsertionLine, 1, 'B');
    }
    else
    {
      ++this.e.TotalLines;
      rtfInsertionLine = this.e.CurLine = this.e.TotalLines - 1;
      this.InitLine(rtfInsertionLine);
    }
    rtf.InsLine = rtfInsertionLine;
    return rtfInsertionLine;
  }

  internal bool GetRtfLevelInfo(tc.ClsRtf rtf, int level)
  {
    this.e.RtfCurRowId = rtf.TblLevel[level].CurRowId;
    this.e.RtfCurCellId = rtf.TblLevel[level].CurCellId;
    this.e.RtfLastCellX = rtf.TblLevel[level].LastCellX;
    rtf.OpenRowId = rtf.TblLevel[level].OpenRowId;
    rtf.OpenCellId = rtf.TblLevel[level].OpenCellId;
    rtf.OpenLastCellX = rtf.TblLevel[level].OpenLastCellX;
    rtf.InitTblCol = rtf.TblLevel[level].InitTblCol;
    rtf.PastingColumn = rtf.TblLevel[level].PastingColumn;
    rtf.CurTblLevel = level;
    return true;
  }

  internal bool GetRtfWord(tc.ClsRtf rtf)
  {
    char c = char.MinValue;
    bool flag1 = false;
    bool flag2 = (rtf.flags & 8192 /*0x2000*/) != 0;
    int num1 = 1001;
    char[] OldObj = new char[num1 + 1];
    rtf.IgnoreText = false;
    while (!rtf.eof)
    {
      bool flag3;
      rtf.SubEntry = flag3 = false;
      bool flag4;
      rtf.GroupEnd = flag4 = flag3;
      rtf.GroupBegin = flag4;
      rtf.IsControlWord = false;
      rtf.CurWord = "";
      rtf.WordLen = 0;
      int index1 = 0;
      while (this.GetRtfChar(rtf))
      {
        if (index1 + 10 > num1)
        {
          int num2 = 300;
          OldObj = this.ReAlloc(OldObj, num1 + num2 + 1);
          num1 += 300;
        }
        char ch = c;
        c = rtf.CurChar;
        if (index1 > 0 && OldObj[index1 - 1] != '\\' | flag2)
        {
          switch (c)
          {
            case '\\':
              if (flag2)
                break;
              goto case '{';
            case '{':
            case '}':
              this.PushRtfChar(rtf);
              goto label_21;
          }
        }
        OldObj[index1] = c;
        ++index1;
        switch (index1)
        {
          case 1:
            switch (c)
            {
              case ')':
                if (this.e.RtfInEquation)
                {
                  --index1;
                  break;
                }
                break;
              case '{':
              case '}':
                goto label_21;
            }
            break;
          case 2:
            if (OldObj[index1 - 2] != '\\' || OldObj[index1 - 1] != '\\' || flag2)
              goto default;
            goto label_21;
          default:
            if ((c != ' ' || (rtf.flags1 & 8192 /*0x2000*/) != 0) && (!this.e.RtfInEquation || c != '('))
            {
              if (c == ')' && this.e.RtfInEquation)
              {
                --index1;
                break;
              }
              if (!rtf.mac || index1 <= 1 || c != '\n' || ch == '\r')
                break;
              goto label_21;
            }
            goto label_21;
        }
        if (index1 == 1000)
          break;
      }
label_21:
      if (rtf.eof)
        return false;
      OldObj[index1] = char.MinValue;
      if (index1 == 1 && OldObj[0] == '{')
      {
        if (!this.SendRtfText(rtf))
          return false;
        if (rtf.GroupLevel >= 50)
          return this.PrintError(107, nameof (GetRtfWord));
        rtf.GroupBegin = true;
        ++rtf.GroupLevel;
        if (rtf.GroupLevel > 0)
          rtf.group[rtf.GroupLevel] = rtf.group[rtf.GroupLevel - 1].Copy();
        rtf.group[rtf.GroupLevel].ControlCount = 0;
        tc.ResetUintFlag(ref rtf.group[rtf.GroupLevel].gflags, 64 /*0x40*/);
        if ((rtf.group[rtf.GroupLevel].gflags & 16 /*0x10*/) != 0)
        {
          rtf.group[rtf.GroupLevel].gflags |= 32 /*0x20*/;
          this.InitGroupForStyle(rtf);
        }
        return true;
      }
      if (index1 == 1 && OldObj[0] == '}')
      {
        int groupLevel1 = rtf.GroupLevel;
        if ((rtf.group[groupLevel1].gflags & 4) != 0 && (groupLevel1 == 0 || (rtf.group[groupLevel1 - 1].gflags & 4) == 0) && (rtf.flags1 & 32 /*0x20*/) == 0)
        {
          rtf.OutBuf += new string(this.e.ParaChar, 1);
          ++rtf.OutBufLen;
        }
        if (!this.SendRtfText(rtf))
          return false;
        if ((rtf.group[groupLevel1].gflags & 32768 /*0x8000*/) != 0 && rtf.pict >= 0 && (groupLevel1 == 0 || (rtf.group[groupLevel1 - 1].gflags & 32768 /*0x8000*/) == 0) && rtf.group[groupLevel1].FieldId != 2)
        {
          this.RealizeControl(rtf.pict, (Control) null);
          this.XlateSizeForPrt(rtf.pict);
        }
        int fieldId = rtf.group[rtf.GroupLevel - 1].FieldId;
        if (rtf.group[rtf.GroupLevel].FieldId == 6 && fieldId != 6)
        {
          int style = rtf.group[rtf.GroupLevel].style;
          rtf.group[rtf.GroupLevel].style |= 512 /*0x0200*/;
          rtf.OutBuf = "}";
          rtf.OutBufLen = rtf.OutBuf.Length;
          if (!this.SendRtfText(rtf))
            return false;
          rtf.group[rtf.GroupLevel].style = style;
        }
        int num3 = rtf.group[rtf.GroupLevel - 1].flags & 12288 /*0x3000*/;
        if ((this.e.RtfInHdrFtr & 12288 /*0x3000*/) != 0 && num3 == 0)
        {
          if ((rtf.flags & 32 /*0x20*/) == 0)
          {
            rtf.OutBuf = " " + new string(this.e.ParaChar, 1);
            rtf.OutBufLen = 2;
            if (!this.SendRtfText(rtf))
              return false;
          }
          this.SetRtfParaDefault(rtf, rtf.group);
          rtf.OutBuf = new string(rtf.HdrFtrChar, 1);
          rtf.OutBufLen = 1;
          if (!this.SendRtfText(rtf))
            return false;
          this.e.RtfInHdrFtr = 0;
        }
        if (rtf.GroupLevel > 0 && (rtf.flags1 & 128 /*0x80*/) != 0 && rtf.group[rtf.GroupLevel].level != rtf.group[rtf.GroupLevel - 1].level)
        {
          int groupLevel2 = rtf.GroupLevel;
          this.SetRtfTblLevel(rtf, groupLevel2 - 1, rtf.group[groupLevel2 - 1].level, rtf.group[groupLevel2].level);
        }
        rtf.GroupEnd = true;
        if ((rtf.group[rtf.GroupLevel].gflags & 32 /*0x20*/) != 0 && rtf.GroupLevel > 0 && (rtf.group[rtf.GroupLevel - 1].gflags & 32 /*0x20*/) == 0)
          this.UpdateRtfStylesheet(rtf);
        this.ExitRtfGroup(rtf, rtf.GroupLevel);
        --rtf.GroupLevel;
        if ((rtf.group[rtf.GroupLevel].style & 8192 /*0x2000*/) == 0)
          this.e.RtfInEquation = false;
        if ((rtf.group[rtf.GroupLevel].style & 4096 /*0x1000*/) != 0 && (rtf.group[rtf.GroupLevel + 1].style & 2048 /*0x0800*/) != 0 && this.e.FootnoteRest.Length > 0)
        {
          rtf.OutBuf += this.e.FootnoteRest;
          rtf.OutBufLen = rtf.OutBuf.Length;
          if (!this.SendRtfText(rtf))
            return false;
          this.e.FootnoteRest = "";
          tc.ResetUintFlag(ref rtf.group[rtf.GroupLevel].style, 4096 /*0x1000*/);
        }
        return rtf.GroupLevel >= 0 && (rtf.GroupLevel != 0 || (this.e.TerFlags3 & 2048 /*0x0800*/) != 0 && (this.e.TerOpFlags2 & 64 /*0x40*/) == 0);
      }
      bool flag5 = false;
      int length1 = 0;
      char[] chArray = new char[OldObj.Length + 10];
      for (int index2 = 0; index2 < index1; ++index2)
      {
        char ch = c;
        c = OldObj[index2];
        if (flag5)
        {
          switch (c)
          {
            case '\'':
              if (index2 + 2 < index1)
              {
                char upper1 = char.ToUpper(OldObj[index2 + 1]);
                int num4 = (upper1 < 'A' ? (int) upper1 - 48 /*0x30*/ : 10 + (int) upper1 - 65) << 4;
                chArray[length1] = (char) num4;
                char upper2 = char.ToUpper(OldObj[index2 + 2]);
                int num5 = upper2 < 'A' ? (int) upper2 - 48 /*0x30*/ : 10 + (int) upper2 - 65;
                chArray[length1] += (char) num5;
                index2 += 2;
                if (chArray[length1] == '\n' && (rtf.group[rtf.GroupLevel].gflags & 402653184 /*0x18000000*/) == 0)
                {
                  chArray[length1] = 'l';
                  int index3 = length1 + 1;
                  chArray[index3] = 'i';
                  int index4 = index3 + 1;
                  chArray[index4] = 'n';
                  int index5 = index4 + 1;
                  chArray[index5] = 'e';
                  length1 = index5 + 1;
                  rtf.IsControlWord = true;
                  flag1 = false;
                  goto case '|';
                }
                if (chArray[length1] > '\u001C' || chArray[length1] == '\n' || chArray[length1] == '\r' || chArray[length1] == '\t' || chArray[length1] == '\f' || chArray[length1] == '\u001C' || (rtf.group[rtf.GroupLevel].gflags & 402653184 /*0x18000000*/) != 0)
                {
                  ++length1;
                  goto case '|';
                }
                goto case '|';
              }
              goto case '|';
            case '*':
              rtf.IgnoreText = true;
              goto case '|';
            case '-':
              chArray[length1] = '\u0006';
              ++length1;
              goto case '|';
            case ':':
              rtf.SubEntry = true;
              goto case '|';
            case '\\':
            case '{':
            case '}':
              chArray[length1] = c;
              ++length1;
              goto case '|';
            case '_':
              chArray[length1] = '\u0017';
              ++length1;
              goto case '|';
            case '|':
              flag5 = false;
              continue;
            case '~':
              chArray[length1] = '\u000E';
              ++length1;
              goto case '|';
            default:
              if (rtf.IsControlWord)
              {
                int num6 = (int) this.ShowMessage(new string(OldObj, 0, length1), this.e.MsgString[195], MessageBoxButtons.OK);
                rtf.eof = true;
                return false;
              }
              rtf.IsControlWord = true;
              flag1 = false;
              if (c == '\n' || c == '\r')
              {
                chArray[length1] = 'p';
                int index6 = length1 + 1;
                chArray[index6] = 'a';
                int index7 = index6 + 1;
                chArray[index7] = 'r';
                length1 = index7 + 1;
                goto case '|';
              }
              chArray[length1] = c;
              ++length1;
              goto case '|';
          }
        }
        else if (c == '\\' && !flag2)
        {
          flag5 = true;
        }
        else
        {
          if (rtf.IsControlWord)
          {
            switch (c)
            {
              case '\n':
                if (!rtf.IgnoreCrLfInControlWord)
                {
                  for (int index8 = index1 - 1; index8 > index2; --index8)
                  {
                    rtf.CurChar = OldObj[index8];
                    this.PushRtfChar(rtf);
                  }
                  goto label_116;
                }
                continue;
              case '\r':
                continue;
              default:
                if (c == '-' && (index2 + 1 == index1 || OldObj[index2 + 1] < '0' || OldObj[index2 + 1] > '9') && OldObj[index2 + 1] != ' ')
                {
                  for (int index9 = index1 - 1; index9 > index2; --index9)
                  {
                    rtf.CurChar = OldObj[index9];
                    this.PushRtfChar(rtf);
                  }
                  goto label_116;
                }
                bool flag6 = c == '.' || c == '-' || c >= '0' && c <= '9';
                if (!flag6 && !char.IsLetter(c) && c != ' ')
                {
                  for (int index10 = index1 - 1; index10 >= index2; --index10)
                  {
                    rtf.CurChar = OldObj[index10];
                    this.PushRtfChar(rtf);
                  }
                  goto label_116;
                }
                if (flag1 && !flag6)
                {
                  for (int index11 = index1 - 1; index11 >= index2; --index11)
                  {
                    rtf.CurChar = OldObj[index11];
                    this.PushRtfChar(rtf);
                  }
                  if (c == ' ')
                  {
                    this.GetRtfChar(rtf);
                    goto label_116;
                  }
                  goto label_116;
                }
                if (flag6)
                {
                  flag1 = true;
                  break;
                }
                break;
            }
          }
          if (c != '\n' && c != '\r')
          {
            chArray[length1] = c;
            if (c < ' ' && c != '\t' && c != '\f')
              chArray[length1] = '_';
            ++length1;
          }
          else if (rtf.mac && length1 > 0 && c == '\n' && ch != '\r')
          {
            chArray[length1] = this.e.ParaChar;
            ++length1;
          }
        }
      }
label_116:
      int length2;
      rtf.WordLen = length2 = length1;
      rtf.CurWord = new string(chArray, 0, length2);
      if (rtf.IsControlWord && rtf.WordLen == 1 && (rtf.CurWord[0] == 'r' || rtf.CurWord[0] == 'n'))
      {
        rtf.IsControlWord = false;
        rtf.CurWord = "";
        rtf.WordLen = 0;
      }
      else if (this.False(rtf.IsControlWord))
      {
        if (rtf.WordLen > 0)
          return true;
        continue;
      }
      rtf.CurWord = rtf.CurWord.Trim();
      int length3 = rtf.CurWord.Length;
      int num7;
      for (num7 = 0; num7 < length3; ++num7)
      {
        char ch = rtf.CurWord[num7];
        if (ch == '.' || ch == '-' || ch >= '0' && ch <= '9')
          break;
      }
      if (num7 == 0)
        num7 = 1;
      if (length3 == 0)
      {
        rtf.param = "";
        rtf.CurWord = " ";
        rtf.WordLen = 1;
      }
      else
      {
        rtf.param = rtf.CurWord.Substring(num7);
        rtf.CurWord = rtf.CurWord.Substring(0, num7);
        rtf.WordLen = num7;
      }
      if (rtf.param.Length > 0)
      {
        if (rtf.param.Length == 1 && rtf.param[0] == '-')
        {
          rtf.IntParam = 0;
          rtf.DoubleParam = 0.0;
        }
        else
        {
          rtf.IntParam = this.ToInt(rtf.param);
          rtf.DoubleParam = this.ToDouble(rtf.param);
        }
      }
      else
      {
        rtf.IntParam = 1;
        rtf.DoubleParam = 1.0;
      }
      ++rtf.group[rtf.GroupLevel].ControlCount;
      return true;
    }
    return false;
  }

  internal new bool HideHiddenParaMarkers()
  {
    bool flag1 = true;
    for (int index = 0; index < this.e.TotalLines - 1; ++index)
    {
      int len1 = this.e.text[index].len;
      if (len1 != 0)
      {
        if (flag1 && !this.IsHiddenLine(index))
          flag1 = false;
        char[] txt1 = this.e.text[index].txt;
        if ((int) txt1[len1 - 1] != (int) this.e.ParaChar)
        {
          if (this.IsBreakChar(txt1[len1 - 1]))
            flag1 = true;
        }
        else
        {
          int curCfmt = this.GetCurCfmt(index, len1 - 1);
          if (!flag1 && (this.e.TerFont[curCfmt].style & 64 /*0x40*/) != 0 && this.e.text[index].cid == this.e.text[index + 1].cid && this.e.text[index].fid == this.e.text[index + 1].fid)
          {
            bool flag2 = true;
            if (this.e.text[index + 1].len >= 0)
            {
              char[] txt2 = this.e.text[index + 1].txt;
              if (this.IsHdrFtrChar(txt2[0]))
                flag2 = false;
              if (txt2[this.e.text[index + 1].len - 1] == '\u0014')
                flag2 = false;
            }
            if (flag2)
            {
              this.e.text[index].txt[len1 - 1] = '\u0005';
              this.SetTag(index, len1 - 1, 5, "HPARA", (string) null, this.e.text[index + 1].pfmt);
              int pfmt = this.e.text[index].pfmt;
              this.e.text[index].flags &= -2;
              this.e.text[index + 1].flags &= -5;
              for (++index; index < this.e.TotalLines; ++index)
              {
                int len2 = this.e.text[index].len;
                if (len2 > 0)
                {
                  this.e.text[index].pfmt = pfmt;
                  this.ChangeLineTextStyle(index, this.e.PfmtId[pfmt].StyId);
                  if (this.IsBreakChar(this.e.text[index].txt[len2 - 1]))
                    break;
                }
              }
            }
          }
          flag1 = true;
        }
      }
    }
    return true;
  }

  internal bool ImportRtfData(
    int action,
    ref tc.StrRtfGroup CurGroup,
    object data,
    tc.StrRtfPict pic,
    tc.StrRtfObject obj)
  {
    bool flag1 = false;
    if (action != 1 && action != 2)
      this.SetRtfTableInfo(ref CurGroup);
    switch (action)
    {
      case 1:
        this.e.InRtfRead = true;
        tc.ClsRtf rtf1 = CurGroup.rtf;
        rtf1.FirstLine = this.e.CurLine;
        CurGroup.TypeFace = this.e.TerArg.FontTypeFace;
        CurGroup.FontFamily = "nil";
        CurGroup.PointSize2 = 2 * this.e.TerArg.PointSize;
        CurGroup.TextColor = this.e.TerFont[0].TextColor;
        CurGroup.TextBkColor = tc.CLR_WHITE;
        CurGroup.UlineColor = tc.CLR_AUTO;
        this.e.RtfInitCellId = this.e.RtfCurCellId = this.e.RtfCurRowId = this.e.RtfLastCellX = 0;
        int num1;
        rtf1.OpenLastCellX = num1 = 0;
        int num2;
        rtf1.OpenRowId = num2 = num1;
        rtf1.OpenCellId = num2;
        this.e.RtfInitLevel = 0;
        rtf1.EmbedTable = true;
        if (this.e.RtfInput != 0 && this.e.RtfInput != 1)
        {
          this.e.RtfInHdrFtr = this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].flags & 12288 /*0x3000*/;
          rtf1.flags1 |= 64 /*0x40*/;
          CurGroup.flags |= this.e.RtfInHdrFtr;
          this.e.RtfInitCellId = this.e.RtfCurCellId = this.e.text[this.e.CurLine].cid;
          if (this.e.InUndo && this.e.CurUndoType == 'T')
            this.e.RtfCurCellId = 0;
          if (this.e.RtfCurCellId > 0)
          {
            this.e.RtfCurRowId = this.e.cell[this.e.RtfCurCellId].row;
            CurGroup.InTable = true;
            rtf1.flags |= 4;
            rtf1.InitTblCol = this.GetCellColumn(this.e.RtfCurCellId, true);
            if (!this.e.ClipEmbTable && (this.e.ClipTblLevel <= 0 || this.e.cell[this.e.RtfCurCellId].level == this.e.ClipTblLevel - 1))
            {
              rtf1.EmbedTable = false;
              if (this.e.cell[this.e.RtfCurCellId].PrevCell > 0 || (this.e.TerFlags & 536870912 /*0x20000000*/) == 0)
                rtf1.PastingColumn = true;
            }
            if (this.e.ClipTblLevel < 0)
              this.e.ClipTblLevel = 1;
            if (!this.LineInfo(this.e.CurLine, 32 /*0x20*/))
              rtf1.InsertBefCell = this.e.RtfCurCellId;
            this.e.RtfInitLevel = this.e.cell[this.e.RtfCurCellId].level + 1;
            int rtfInitLevel;
            rtf1.CurTblLevel = rtfInitLevel = this.e.RtfInitLevel;
            CurGroup.level = rtfInitLevel;
            this.SaveRtfLevelInfo(rtf1, rtf1.CurTblLevel);
            if (rtf1.CurTblLevel > 0)
              rtf1.TblLevel[rtf1.CurTblLevel - 1].CurCellId = this.e.cell[this.e.RtfCurCellId].ParentCell;
          }
          this.e.RtfParaFID = this.e.text[this.e.CurLine].fid;
          if (this.e.RtfParaFID > 0)
          {
            this.e.RtfParaFrameInfo.x = this.e.ParaFrame[this.e.RtfParaFID].x;
            this.e.RtfParaFrameInfo.y = this.e.ParaFrame[this.e.RtfParaFID].ParaY;
            this.e.RtfParaFrameInfo.width = this.e.ParaFrame[this.e.RtfParaFID].width;
            this.e.RtfParaFrameInfo.height = this.e.ParaFrame[this.e.RtfParaFID].MinHeight;
            this.e.RtfParaFrameInfo.DistFromText = this.e.ParaFrame[this.e.RtfParaFID].DistFromText;
            this.e.RtfParaFrameInfo.ZOrder = this.e.ParaFrame[this.e.RtfParaFID].ZOrder;
            CurGroup.ParaFrameInfo = this.e.RtfParaFrameInfo;
            CurGroup.FrmFlags |= 2;
            rtf1.flags1 |= 16384 /*0x4000*/;
          }
          rtf1.InitSect = this.GetSection(this.e.CurLine);
          this.e.IsPortrait = this.e.TerSect[rtf1.InitSect].IsPortrait;
          if (!rtf1.EmptyDoc && (this.e.TerFlags3 & 32768 /*0x8000*/) == 0 || this.RtfHdrFtrExists(rtf1.InitSect))
          {
            rtf1.flags |= 3200;
            rtf1.flags1 |= 24;
          }
          int curCfmt = this.GetCurCfmt(this.e.CurLine, this.e.CurCol);
          char curChar = this.GetCurChar(this.e.CurLine, this.e.CurCol);
          if (this.e.TerFont[curCfmt].FieldId == 6 && curChar != '{' || this.e.TerFont[curCfmt].FieldId == 7)
            rtf1.InitFieldId = this.e.TerFont[curCfmt].FieldId;
          if (rtf1.InitFieldId == 0 && this.e.TerFont[curCfmt].FieldId == 2)
          {
            bool flag2 = false;
            if (this.e.ProtectForm)
              flag2 = true;
            else if (this.e.CurLine > 0 || this.e.CurCol > 0)
            {
              this.GetPrevCfmt(this.e.CurLine, this.e.CurCol);
              if (this.e.TerFont[curCfmt].FieldId == 2)
                flag2 = true;
            }
            if (flag2)
            {
              rtf1.InitFieldId = 2;
              if (this.e.TerFont[curCfmt].FieldCode != null)
              {
                rtf1.FieldCode = this.e.TerFont[curCfmt].FieldCode;
                rtf1.InitStyle = this.e.TerFont[curCfmt].style;
                rtf1.InitAuxId = this.e.TerFont[curCfmt].AuxId;
              }
            }
          }
          CurGroup.ParaStyId = this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].StyId;
          if (this.e.TrackChanges)
          {
            CurGroup.revised = true;
            CurGroup.InsRev = this.e.TrackRev;
            CurGroup.InsTime = this.e.TrackTime;
          }
          if (this.e.InUndo && this.e.TotalLines == 1 && this.e.text[0].len == 1 && this.e.text[0].txt[0] == '\u0015' && this.e.text[0].pfmt == 0)
            rtf1.flags2 |= 16 /*0x10*/;
        }
        else
        {
          for (int line = 0; line < this.e.TotalLines; ++line)
            this.init.FreeLine(line);
          this.e.TotalLines = 0;
          this.e.FileFormat = 2;
          if (CurGroup.TextColor == Color.Black)
            CurGroup.TextColor = tc.CLR_AUTO;
          this.e.RtfInHdrFtr = 0;
          rtf1.InitSect = 0;
        }
        this.e.RepageBeginLine = this.e.CurLine;
        break;
      case 2:
        if (tc.DebugMode)
          this.misc.dm("RTF_END");
        tc.ClsRtf rtf2 = (tc.ClsRtf) data;
        for (int row = 0; row < this.e.TotalTableRows; ++row)
        {
          if (this.e.TableRow[row].InUse && (this.e.TableAux[row].flags & 4) != 0)
            this.DeleteRtfRow(rtf2, row);
        }
        if ((rtf2.flags2 & 16 /*0x10*/) != 0 && this.e.CurLine + 1 == this.e.TotalLines && this.e.CurLine > 0 && this.e.text[this.e.CurLine - 1].len > 0)
        {
          this.MoveLineArrays(this.e.CurLine, 1, 'D');
          if (this.e.CurLine >= this.e.TotalLines)
          {
            this.e.CurLine = this.e.TotalLines - 1;
            this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
          }
        }
        if (this.e.TotalLines == 0)
        {
          this.e.TotalLines = 1;
          this.InitLine(0);
        }
        if (!this.e.TerArg.WordWrap && this.e.RtfInput < 2)
          this.MergeRtfLinePieces(0, this.e.TotalLines - 1);
        if ((this.e.TerFlags4 & 4) != 0 && this.e.RtfInput < 2)
          this.HideHiddenParaMarkers();
        if (this.e.RtfInput < 2 || (this.e.TerFlags3 & 32768 /*0x8000*/) != 0)
        {
          this.e.TerSect[rtf2.InitSect] = rtf2.sect.Copy();
          if (!this.e.TerSect[rtf2.InitSect].IsPortrait)
          {
            float pprWidth = this.e.TerSect[rtf2.InitSect].PprWidth;
            this.e.TerSect[rtf2.InitSect].PprWidth = this.e.TerSect[rtf2.InitSect].PprHeight;
            this.e.TerSect[rtf2.InitSect].PprHeight = pprWidth;
          }
        }
        this.FixNegativeIndents(rtf2);
        if (this.e.TotalSects > 1)
          this.RecreateSections();
        this.SetSectPageSize();
        if (this.e.TerArg.PrintView)
          this.RepairTable();
        if (this.e.RtfInput == 0 || this.e.RtfInput == 1)
        {
          this.e.IsPortrait = this.e.TerSect[0].IsPortrait;
          this.e.CurLine = this.e.CurRow = this.e.CurCol = 0;
          this.e.RepageBeginLine = 0;
          if (this.e.TotalParaFrames == 1)
            this.e.WrapAddLines = 50;
          if (this.e.TerArg.PageMode)
          {
            int LastPage = this.e.TotalParaFrames > 500 ? 2 : 30;
            bool flag3 = this.e.BatchMode || this.e.TotalLines < 5000;
            if (this.e.TotalParaFrames > 500)
              flag3 = false;
            if (!flag3)
            {
              for (int index = 0; index < this.e.TotalFonts; ++index)
              {
                if (this.e.TerFont[index].InUse && this.e.TerFont[index].FieldId == 9)
                  flag3 = true;
              }
            }
            if (!flag3)
            {
              for (int index = 0; index < this.e.TotalPfmts; ++index)
              {
                if (this.e.TerBlt[this.e.PfmtId[index].BltId].ls > 0)
                  flag3 = true;
              }
            }
            if ((this.e.TerFlags5 & 16384 /*0x4000*/) != 0)
              flag3 = true;
            if (flag3)
              this.Repaginate(false, false, 0, true);
            else
              this.Repaginate(false, false, LastPage, true);
          }
          else if (this.e.TerArg.WordWrap)
            this.WordWrap(0, this.e.TotalLines);
          if (this.e.TerArg.PageMode)
          {
            for (int index = 1; index < this.e.TotalParaFrames; ++index)
            {
              if (this.e.ParaFrame[index].InUse && (this.e.ParaFrame[index].flags & 16384 /*0x4000*/) == 0)
              {
                flag1 = true;
                break;
              }
            }
          }
        }
        else
          this.e.CurCol = 0;
        if (this.e.TerArg.WordWrap)
          this.ReposPageHdrFtr(false);
        if (((rtf2.flags & 4096 /*0x1000*/) != 0 || (rtf2.flags1 & 32768 /*0x8000*/) != 0) && !this.e.ViewPageHdrFtr && this.e.TerArg.PageMode && (this.e.TerFlags2 & 262144 /*0x040000*/) == 0)
        {
          bool paintEnabled = this.e.PaintEnabled;
          this.e.PaintEnabled = false;
          this.ToggleViewHdrFtr();
          this.e.PaintEnabled = paintEnabled;
        }
        if (this.e.RtfInput == 0 || this.e.RtfInput == 1)
        {
          if (flag1)
            this.e.TerRepaginate(true);
          if (this.e.ViewPageHdrFtr && this.e.TerArg.PageMode)
          {
            bool flag4 = false;
            for (int index = 0; index < this.e.TotalLines; ++index)
            {
              if ((this.e.PfmtId[this.e.text[index].pfmt].flags & 4096 /*0x1000*/) != 0 && (this.e.text[index].flags & 655360 /*0x0A0000*/) == 0)
              {
                if (!flag4)
                  this.e.CurLine = index;
                flag4 = true;
                if (index > 0 && (this.e.text[index - 1].flags & 131072 /*0x020000*/) != 0)
                {
                  this.e.CurLine = index;
                  break;
                }
              }
              if ((this.e.PfmtId[this.e.text[index].pfmt].flags & 12288 /*0x3000*/) == 0)
                break;
            }
          }
          this.e.TerArg.modified = 0;
        }
        if (this.e.RtfInput < 2 && rtf2.EnableTracking)
          this.e.TerEnableTracking(true, "", true, 0, tc.CLR_AUTO, 0, tc.CLR_AUTO);
        this.e.ToolBarFillStyles = true;
        if (this.e.UseWindow)
          this.UpdateToolBar(true);
        this.RequestPagination(true);
        this.e.InRtfRead = false;
        if (tc.DebugMode)
        {
          this.misc.dm("RTF_END - exit");
          break;
        }
        break;
      case 4:
        return this.ImportRtfTextLine(ref CurGroup, (string) data);
      case 6:
        return this.ImportRtfPicture(ref CurGroup, pic);
      case 7:
        tc.ClsRtf clsRtf = (tc.ClsRtf) data;
        int index1 = 0;
        while (index1 < this.e.TotalSects && this.e.TerSect[index1].InUse)
          ++index1;
        if (index1 == this.e.TotalSects)
        {
          if (this.e.TotalSects >= this.e.MaxSects)
          {
            this.ExpandSectArray(-1);
            if (this.e.TotalSects >= this.e.MaxSects)
              return this.PrintError(128 /*0x80*/, (string) null);
          }
          ++this.e.TotalSects;
        }
        this.e.TerSect[index1] = clsRtf.sect.Copy();
        if (!this.e.TerSect[index1].IsPortrait)
        {
          float pprWidth = this.e.TerSect[index1].PprWidth;
          this.e.TerSect[index1].PprWidth = this.e.TerSect[index1].PprHeight;
          this.e.TerSect[index1].PprHeight = pprWidth;
        }
        this.e.WrapSect = index1;
        return this.ImportRtfTextLine(ref clsRtf.group[clsRtf.GroupLevel], clsRtf.OutBuf);
    }
    return true;
  }

  internal bool ImportRtfPicture(ref tc.StrRtfGroup group, tc.StrRtfPict pic)
  {
    tc.ClsRtf rtf = group.rtf;
    int pict;
    if (pic.PictId > 0)
    {
      this.e.CurObject = pict = pic.PictId;
    }
    else
    {
      if ((pict = this.FindOpenSlot()) == -1)
        return false;
      this.e.CurObject = pict;
      if ((group.gflags & 1024 /*0x0400*/) != 0)
        return this.BuildRtfAnimSeq(ref group, pic, pict);
    }
    int rtfInsertionLine;
    if (-1 == (rtfInsertionLine = this.GetRtfInsertionLine(rtf)))
      return false;
    this.SetRtfParaFID(rtfInsertionLine, ref group);
    this.LineAlloc(rtfInsertionLine, 0, 1);
    this.e.text[rtfInsertionLine].txt[0] = '\u0018';
    this.OpenCfmt(rtfInsertionLine)[0] = (ushort) pict;
    this.CloseCfmt(rtfInsertionLine);
    this.e.text[rtfInsertionLine].y = 0;
    this.e.text[rtfInsertionLine].x = 0;
    this.e.text[rtfInsertionLine].cid = this.e.RtfCurCellId;
    this.SetRtfParaId(rtfInsertionLine, ref group);
    if (pic.PictId <= 0)
      this.BuildRtfPicture(ref group, pic, pict);
    this.e.text[rtfInsertionLine].height = this.e.PrtFont[pict].height;
    int num = group.gflags & 2048 /*0x0800*/;
    bool flag = false;
    if ((group.gflags & 256 /*0x0100*/) == 0 && (group.shape.type == 75 || group.shape.type == 201))
      flag = true;
    if (this.e.TerFont[pict].PictType == 11)
      flag = true;
    if (flag)
    {
      this.BuildRtfPictFrame(ref group, pict);
      rtf.PictFrameLine = rtfInsertionLine;
      rtf.PictFrameCol = 0;
    }
    if (this.True(rtf.TagId))
      this.ApplyRtfTagId(rtf, rtfInsertionLine);
    rtf.SomeTextRead = true;
    tc.ResetUintFlag(ref rtf.flags1, 32 /*0x20*/);
    ++this.e.CurLine;
    return true;
  }

  internal bool ImportRtfTextLine(ref tc.StrRtfGroup group, string text)
  {
    bool flag = false;
    tc.ClsRtf rtf = group.rtf;
    int rtfInsertionLine;
    if (-1 == (rtfInsertionLine = this.GetRtfInsertionLine(rtf)))
      return false;
    this.SetRtfParaFID(rtfInsertionLine, ref group);
    if (this.True(group.style & 2048 /*0x0800*/))
      this.SetRtfFootnote(ref group, rtfInsertionLine - 1);
    if (this.True(group.gflags & 4096 /*0x1000*/) && rtf.OutBufLen > 0)
      rtf.OutBuf = rtf.OutBuf.ToUpper();
    int length1 = text.Length;
    if (this.False(rtf.flags2 & 32 /*0x20*/) && !rtf.OutBufHasUnicode)
    {
      int charType = group.CharType;
      int num1 = charType != 3 || group.TypeFaceDB == null || group.TypeFaceDB.Length <= 0 ? (charType != 2 || group.TypeFaceHi == null || group.TypeFaceHi.Length <= 0 ? group.CharSet : group.CharSetHi) : group.CharSetDB;
      if (group.CharType == 3)
      {
        tc.ResetUintFlag(ref group.gflags2, 16 /*0x10*/);
        string UniText;
        if (this.RtfTextToUnicode(rtf, text, out UniText, ref group))
        {
          if (UniText.Length < text.Length)
            group.gflags2 |= 16 /*0x10*/;
          text = UniText;
        }
      }
      else if (num1 >= 2 && length1 > 0)
      {
        char c = text[length1 - 1];
        if (c < ' ')
          --length1;
        if (length1 > 0)
        {
          if (c < ' ')
            text = text.Substring(0, length1);
          string UniText;
          if (this.RtfTextToUnicode(rtf, text, out UniText, ref group))
            text = UniText;
          if (c < ' ')
            text += new string(c, 1);
        }
      }
      else
      {
        int num2 = 0;
        while (num2 < length1 && text[num2] < 'Ā' && (text[num2] < '\u0080' || text[num2] > '¡'))
          ++num2;
        if (num2 < length1)
        {
          string str = text.Substring(0, num2);
          for (; num2 < length1; ++num2)
          {
            if (text[num2] < '\u0080')
              str += new string(text[num2], 1);
            else if (text[num2] > '¡' && text[num2] < 'Ā')
            {
              str += new string(text[num2], 1);
            }
            else
            {
              string text1 = new string(text[num2], 1);
              string UniText;
              if (this.RtfTextToUnicode(rtf, text1, out UniText, ref group))
                text1 = UniText;
              str += text1;
            }
          }
          text = str;
        }
      }
    }
    if (group.IgnoreCount > 0)
    {
      int length2 = text.Length;
      int startIndex = length2 > group.IgnoreCount ? group.IgnoreCount : length2;
      text = startIndex != length2 ? text.Substring(startIndex) : "";
      group.IgnoreCount -= startIndex;
    }
    if (group.caps)
      text = text.ToUpper();
    int length3 = text.Length;
    if (length3 == 1 && text[0] == '\u000F' && group.CharId == 0 && rtfInsertionLine > 0 && this.e.text[rtfInsertionLine - 1].len > 0)
    {
      int len = this.e.text[rtfInsertionLine - 1].len;
      if (this.e.text[rtfInsertionLine - 1].txt[len - 1] == '\u000F' && this.e.TerFont[(int) this.OpenCfmt(rtfInsertionLine - 1)[len - 1]].CharId > 0)
      {
        text = "";
        length3 = text.Length;
      }
    }
    this.e.text[rtfInsertionLine].fmt = (ushort[]) null;
    this.e.text[rtfInsertionLine].UniFmt = (ushort) 0;
    this.LineAlloc(rtfInsertionLine, 0, length3);
    char[] txt = this.e.text[rtfInsertionLine].txt;
    if (length3 > 0)
      this.FarMove(text.ToCharArray(), txt, length3);
    int index1 = this.GetRtfFontId(rtf, group);
    int fieldId = this.e.TerFont[index1].FieldId;
    if (group.PictId > 0 && this.e.TerFont[group.PictId].InUse && this.True(this.e.TerFont[group.PictId].style & 128 /*0x80*/))
      index1 = group.PictId;
    this.e.text[rtfInsertionLine].fmt = (ushort[]) null;
    this.e.text[rtfInsertionLine].UniFmt = (ushort) index1;
    this.e.text[rtfInsertionLine].y = 0;
    this.e.text[rtfInsertionLine].x = 0;
    this.e.text[rtfInsertionLine].height = this.e.TerFont[index1].height;
    if (this.e.RtfCurCellId < 0)
      this.e.RtfCurCellId = 0;
    this.e.text[rtfInsertionLine].cid = this.e.RtfCurCellId;
    this.SetRtfParaId(rtfInsertionLine, ref group);
    if (rtfInsertionLine > 0 && this.e.text[rtfInsertionLine - 1].fid != this.e.text[rtfInsertionLine].fid)
    {
      int fid = this.e.text[rtfInsertionLine].fid;
      if (this.True(this.e.ParaFrame[fid].flags & 128 /*0x80*/) && this.True(this.e.ParaFrame[fid].flags & 131072 /*0x020000*/))
      {
        int len1 = this.e.text[rtfInsertionLine - 1].len;
        if ((int) this.e.text[rtfInsertionLine - 1].txt[len1 - 1] != (int) this.e.ParaChar)
        {
          this.MoveLineData(rtfInsertionLine - 1, len1 - 1, 1, 'A');
          int len2 = this.e.text[rtfInsertionLine - 1].len;
          this.e.text[rtfInsertionLine - 1].txt[len2 - 1] = this.e.ParaChar;
        }
      }
    }
    if (text.Length == 1)
    {
      if (text[0] == '\u0014' && this.AllocTabw(rtfInsertionLine))
      {
        this.e.text[rtfInsertionLine].tabw.type = 2;
        this.e.text[rtfInsertionLine].tabw.section = this.e.WrapSect;
        this.e.text[rtfInsertionLine].tabw.count = 0;
        flag = true;
      }
      if (text[0] == '\u0014' && rtfInsertionLine > 0)
        this.e.text[rtfInsertionLine].pfmt = this.e.text[rtfInsertionLine - 1].pfmt;
      if (text[0] == '\u0014' || text[0] == '\f')
        this.e.text[rtfInsertionLine].fid = 0;
      if (text[0] == '\u0012' && rtfInsertionLine > 0 && this.e.text[rtfInsertionLine].fid != this.e.text[rtfInsertionLine - 1].fid)
      {
        this.e.text[rtfInsertionLine].fid = this.e.RtfParaFID = this.e.text[rtfInsertionLine - 1].fid;
        if (this.e.RtfParaFID > 0)
          this.e.RtfParaFrameInfo = this.e.PrevRtfParaFrameInfo;
      }
      if ((text[0] == '\u0011' || text[0] == '\u0010' || text[0] == '\u0002' || text[0] == '\u0003' || text[0] == '\u0019' || text[0] == '\u001A' || text[0] == '\a' || text[0] == '\b') && rtfInsertionLine - 2 >= 0 && this.e.text[rtfInsertionLine - 2].len == 1 && this.e.text[rtfInsertionLine - 1].len == 1)
      {
        char ch = this.e.text[rtfInsertionLine - 2].txt[0];
        if (ch <= '\a')
        {
          if (ch != '\u0002' && ch != '\u0003' && ch != '\a')
            goto label_71;
        }
        else if (ch <= '\u0011')
        {
          if (ch != '\u0010' && ch != '\u0011')
            goto label_71;
        }
        else if (ch != '\u0019' && ch != '\u001A')
          goto label_71;
        int num = 0;
        goto label_72;
label_71:
        num = ch != '\b' ? 1 : 0;
label_72:
        if (num == 0 && this.e.text[rtfInsertionLine - 1].txt[0] == '\u0015')
        {
          this.MoveLineData(rtfInsertionLine - 1, 0, 1, 'B');
          this.e.text[rtfInsertionLine - 1].txt[0] = ' ';
        }
      }
      this.SetHdrFtrLineFlags(rtfInsertionLine, text[0]);
    }
    int length4 = text.Length;
    tc.ResetUintFlag(ref rtf.flags1, 32 /*0x20*/);
    if (length4 > 0)
    {
      char ch = text[length4 - 1];
      if ((int) ch == (int) this.e.ParaChar)
        rtf.flags1 |= 32 /*0x20*/;
      if (ch == '\u0012' || (int) ch == (int) this.e.CellChar)
      {
        if (this.e.text[rtfInsertionLine].tabw == null)
          this.AllocTabw(rtfInsertionLine);
        if (this.True(this.e.text[rtfInsertionLine].tabw))
        {
          if (ch == '\u0012')
            this.e.text[rtfInsertionLine].tabw.type |= 32 /*0x20*/;
          else if ((int) ch == (int) this.e.CellChar)
            this.e.text[rtfInsertionLine].tabw.type |= 16 /*0x10*/;
        }
      }
      this.SetHdrFtrLineFlags(rtfInsertionLine, ch);
      if ((int) ch == (int) this.e.ParaChar || (int) ch == (int) this.e.CellChar)
        this.e.text[rtfInsertionLine].flags |= 1;
      if (fieldId == 9)
        this.e.text[rtfInsertionLine].flags |= 16777216 /*0x01000000*/;
      if (fieldId == 11)
        this.e.text[rtfInsertionLine].flags |= 134217728 /*0x08000000*/;
      if (fieldId == 12)
        this.e.text[rtfInsertionLine].flags |= 1073741824 /*0x40000000*/;
      if (fieldId == 13)
        this.e.text[rtfInsertionLine].flags2 |= 4;
      if (fieldId == 21)
        this.e.text[rtfInsertionLine].flags2 |= 65536 /*0x010000*/;
      rtf.PrevField = fieldId;
      if (ch == '\f' || ch == '\u0014' || ch == '\u0016' || ch == '\u0012' || this.IsHdrFtrChar(ch))
        this.e.text[rtfInsertionLine].flags |= 2;
      if (ch == '\u0014')
        this.e.text[rtfInsertionLine].flags |= 2048 /*0x0800*/;
      if (this.True(this.e.TerFont[index1].style & 32768 /*0x8000*/))
      {
        this.e.text[rtfInsertionLine].flags2 |= 2;
        this.e.TerOpFlags |= 1073741824 /*0x40000000*/;
      }
      else if (this.True(this.e.TerFont[index1].style & 2048 /*0x0800*/))
        this.e.text[rtfInsertionLine].flags |= 65536 /*0x010000*/;
      if (this.True(this.e.text[rtfInsertionLine].flags & 1))
      {
        for (int index2 = rtfInsertionLine - 1; index2 >= 0 && this.False(this.e.text[index2].flags & 3); --index2)
          this.e.text[index2].pfmt = this.e.text[rtfInsertionLine].pfmt;
      }
      if (rtfInsertionLine > 0 && (int) ch == (int) this.e.CellChar && this.e.text[rtfInsertionLine].cid > 0 && this.e.cell[this.e.text[rtfInsertionLine].cid].PrevCell > 0)
      {
        int cid = this.e.text[rtfInsertionLine].cid;
        for (int index3 = rtfInsertionLine - 1; index3 >= 0 && (!this.True(this.e.text[index3].tabw) || !this.True(this.e.text[index3].tabw.type & 16 /*0x10*/)) && this.e.text[rtfInsertionLine].fid == this.e.text[index3].fid; --index3)
        {
          if (this.e.text[index3].cid == 0)
          {
            this.e.text[index3].cid = cid;
            int pfmt = this.e.text[index3].pfmt;
            if (this.False(this.e.PfmtId[pfmt].pflags & 2))
              this.e.text[index3].pfmt = this.SetParaParam(pfmt, 9, this.e.PfmtId[pfmt].pflags | 2);
          }
        }
      }
      if (rtfInsertionLine > 0 && (int) ch == (int) this.e.CellChar && this.e.text[rtfInsertionLine].cid > 0)
      {
        int cid1 = this.e.text[rtfInsertionLine].cid;
        int level1 = this.e.cell[cid1].level;
        for (int index4 = rtfInsertionLine - 1; index4 >= 0 && this.e.text[index4].cid > 0; --index4)
        {
          int cid2 = this.e.text[index4].cid;
          int level2 = this.e.cell[cid2].level;
          if (cid2 != cid1)
          {
            if (level2 > level1)
            {
              if (level2 == level1 + 1 && level1 >= 0)
                this.e.cell[cid2].ParentCell = cid1;
            }
            else
              break;
          }
        }
      }
      if (rtfInsertionLine > 0 && ch == '\u0012')
      {
        int num = 0;
        this.e.text[rtfInsertionLine].pfmt = this.e.text[rtfInsertionLine - 1].pfmt;
        if (this.e.TotalParaFrames > 1)
        {
          int row1 = this.e.cell[this.e.text[rtfInsertionLine].cid].row;
          int index5 = rtfInsertionLine;
          int cid3;
          while (index5 >= 0 && !this.False(cid3 = this.e.text[index5].cid) && this.e.cell[cid3].row == row1 && (num = this.e.text[index5].fid) <= 0)
            --index5;
          if (num > 0)
          {
            int row2 = this.e.cell[this.e.text[rtfInsertionLine].cid].row;
            int cid4;
            for (int index6 = rtfInsertionLine; index6 >= 0 && !this.False(cid4 = this.e.text[index6].cid) && this.e.cell[cid4].row == row2; --index6)
              this.e.text[index6].fid = num;
          }
        }
      }
      if (rtfInsertionLine > 0 && this.e.text[rtfInsertionLine - 1].fid > 0 && this.e.text[rtfInsertionLine - 1].fid != this.e.text[rtfInsertionLine].fid && this.e.text[rtfInsertionLine - 1].len > 0)
      {
        int len = this.e.text[rtfInsertionLine - 1].len;
        if ((int) this.e.text[rtfInsertionLine - 1].txt[len - 1] != (int) this.e.ParaChar)
        {
          this.MoveLineArrays(rtfInsertionLine, 1, 'B');
          this.e.text[rtfInsertionLine].cid = 0;
          this.e.text[rtfInsertionLine].flags = 1;
          this.LineAlloc(rtfInsertionLine, 0, 1);
          this.e.text[rtfInsertionLine].txt[0] = this.e.ParaChar;
          ++rtfInsertionLine;
          rtf.InsLine = rtfInsertionLine;
          ++this.e.CurLine;
        }
      }
    }
    if (this.True(rtf.TagId))
      this.ApplyRtfTagId(rtf, rtfInsertionLine);
    if (flag)
    {
      string name = "Section" + this.e.WrapSect.ToString();
      this.SetTag(rtfInsertionLine, 0, 4, name, (string) null, this.e.WrapSect);
    }
    rtf.SomeTextRead = true;
    ++this.e.CurLine;
    return true;
  }

  internal bool InitGroupForStyle(tc.ClsRtf rtf)
  {
    int groupLevel = rtf.GroupLevel;
    tc.StrRtfGroup[] group = rtf.group;
    group[groupLevel].ParaStyId = 0;
    group[groupLevel].CharStyId = 1;
    group[groupLevel].TypeFace = "";
    group[groupLevel].PointSize2 = 0;
    group[groupLevel].style = 0;
    group[groupLevel].TextColor = this.e.TerFont[0].TextColor;
    group[groupLevel].TextBkColor = tc.CLR_WHITE;
    group[groupLevel].UlineColor = tc.CLR_AUTO;
    group[groupLevel].LeftIndent = 0;
    group[groupLevel].RightIndent = 0;
    group[groupLevel].FirstIndent = 0;
    group[groupLevel].flags = 0;
    group[groupLevel].ParShading = 0;
    group[groupLevel].SpaceBefore = 0;
    group[groupLevel].SpaceAfter = 0;
    group[groupLevel].SpaceBetween = 0;
    group[groupLevel].LineSpacing = 0;
    group[groupLevel].ParaBkColor = tc.CLR_WHITE;
    group[groupLevel].ParaBorderColor = tc.CLR_AUTO;
    group[groupLevel].tab.count = 0;
    return true;
  }

  internal bool InitGroupFromStyle(tc.ClsRtf rtf, int id, int type, bool initialize)
  {
    tc.StrRtfGroup rtfGroup = this.GetRtfGroup(rtf);
    tc.StrRtfGroup strRtfGroup = rtf.group[rtf.GroupLevel];
    if ((strRtfGroup.style & 39936) == 0 && id < rtfGroup.RtfSID.Length)
    {
      id = rtfGroup.RtfSID[id];
      if (id < 0 || id >= this.e.TotalSID)
        return true;
      if (type == 2)
        strRtfGroup.ParaStyId = id;
      else
        strRtfGroup.CharStyId = id;
      if (initialize)
      {
        if (this.e.StyleId[id].TypeFace.Length > 0)
          strRtfGroup.TypeFace = this.e.StyleId[id].TypeFace;
        if (this.e.StyleId[id].TwipsSize > 0)
          strRtfGroup.PointSize2 = this.e.StyleId[id].TwipsSize / 10;
        strRtfGroup.style = this.e.StyleId[id].style | strRtfGroup.style & 39936;
        strRtfGroup.TextColor = this.e.StyleId[id].TextColor;
        strRtfGroup.TextBkColor = this.e.StyleId[id].TextBkColor;
        strRtfGroup.UlineColor = this.e.StyleId[id].UlineColor;
        strRtfGroup.expand = this.e.StyleId[id].expand;
        strRtfGroup.offset = this.e.StyleId[id].offset;
        if (type == 2)
        {
          strRtfGroup.LeftIndent = this.e.StyleId[id].LeftIndentTwips;
          strRtfGroup.RightIndent = this.e.StyleId[id].RightIndentTwips;
          strRtfGroup.FirstIndent = this.e.StyleId[id].FirstIndentTwips;
          strRtfGroup.flags |= this.e.StyleId[id].ParaFlags;
          strRtfGroup.pflags |= this.e.StyleId[id].pflags;
          strRtfGroup.ParShading = this.e.StyleId[id].shading;
          strRtfGroup.SpaceBefore = this.e.StyleId[id].SpaceBefore;
          strRtfGroup.SpaceAfter = this.e.StyleId[id].SpaceAfter;
          strRtfGroup.SpaceBetween = this.e.StyleId[id].SpaceBetween;
          strRtfGroup.LineSpacing = this.e.StyleId[id].LineSpacing;
          strRtfGroup.ParaBkColor = this.e.StyleId[id].ParaBkColor;
          strRtfGroup.ParaBorderColor = this.e.StyleId[id].ParaBorderColor;
          strRtfGroup.OutlineLevel = this.e.StyleId[id].OutlineLevel;
          int tabId = this.e.StyleId[id].TabId;
          strRtfGroup.tab = this.e.TerTab[tabId].Copy();
        }
      }
      rtf.group[rtf.GroupLevel] = strRtfGroup;
    }
    return true;
  }

  internal bool InitRtfGroup(tc.ClsRtf rtf, int RtfGroup)
  {
    tc.StrRtfGroup[] group = rtf.group;
    group[RtfGroup].RtfGroup = RtfGroup;
    group[RtfGroup].DefFont = 0;
    tc.ResetUintFlag(ref group[RtfGroup].gflags2, 2);
    group[RtfGroup].MaxRtfFonts = 200;
    group[RtfGroup].font = new tc.StrRtfFont[group[RtfGroup].MaxRtfFonts];
    for (int index = 0; index < group[RtfGroup].MaxRtfFonts; ++index)
    {
      tc.StrRtfFont strRtfFont = new tc.StrRtfFont();
      group[RtfGroup].font[index] = strRtfFont.init();
    }
    group[RtfGroup].color = new tc.StrRtfColor[this.e.MaxRtfColors];
    for (int index = 0; index < this.e.MaxRtfColors; ++index)
      group[RtfGroup].color[index].color = Color.Black;
    group[RtfGroup].MaxRtfSID = 200;
    group[RtfGroup].RtfSID = new int[group[RtfGroup].MaxRtfSID];
    for (int index = 0; index < group[RtfGroup].MaxRtfSID; ++index)
      group[RtfGroup].RtfSID[index] = -1;
    for (int index = 0; index < this.e.TotalSID; ++index)
      this.e.StyleId[index].RtfIndex = -1;
    return true;
  }

  internal bool InsertRtfBuf(string buf, int BufLen, int line, int col, bool repaint)
  {
    bool flag1 = false;
    int input = 2;
    bool flag2 = false;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (line < 0)
    {
      if (line == -2)
        input = 5;
      line = this.e.CurLine;
      col = this.e.CurCol;
    }
    else
    {
      if (col < 0)
        this.AbsToRowCol(line, out line, out col);
      if (line >= this.e.TotalLines)
      {
        line = this.e.TotalLines > 0 ? this.e.TotalLines - 1 : 0;
        col = this.e.text[line] == null ? 0 : this.e.text[line].len;
        if (this.e.TerArg.WordWrap)
          --col;
        if (col < 0)
          col = 0;
      }
      if (line < 0)
        line = 0;
      if (this.e.text[line] == null || col > this.e.text[line].len)
        col = this.e.text[line].len;
      if (col < 0)
        col = 0;
    }
    if ((this.e.TerOpFlags2 & 64 /*0x40*/) != 0 && this.e.ClipTblLevel > 0 && this.e.TerFont[this.GetCurCfmt(line, col)].FieldId == 7 && this.e.TerLocateFieldChar(7, (string) null, false, ref line, ref col, false) && this.e.TerLocateFieldChar(6, (string) null, false, ref line, ref col, false))
      this.NextTextPos(ref line, ref col);
    this.e.CurLine = line;
    this.e.CurCol = col;
    if (input == 2 && this.True(this.e.text[this.e.CurLine].cid))
      flag2 = true;
    int abs1 = input != 5 ? this.RowColToAbs(this.e.CurLine, this.e.CurCol) : this.RowColToAbs(this.e.TotalLines - 1, this.e.text[this.e.TotalLines - 1].len - 1);
    if (this.e.CurCol > 0)
    {
      bool paintEnabled = this.e.PaintEnabled;
      this.e.PaintEnabled = false;
      int num = this.TerSplitLine(0, this.e.TabAlign, true) ? 1 : 0;
      this.e.PaintEnabled = paintEnabled;
      flag1 = true;
      if (num == 0)
        return false;
    }
    if ((this.e.TerOpFlags2 & 64 /*0x40*/) == 0 && !this.e.InUndo)
    {
      this.e.ClipEmbTable = (this.e.TerFlags3 & 8192 /*0x2000*/) != 0;
      this.e.ClipTblLevel = 0;
    }
    int curLine = this.e.CurLine;
    int num1 = this.RtfRead(input, (string) null, buf, BufLen) ? 1 : 0;
    if (curLine > 0 & flag1)
      --curLine;
    if (this.e.TerArg.WordWrap)
      this.WordWrap(curLine, this.e.CurLine - curLine + 1);
    if (!this.e.TerArg.WordWrap && this.e.CurLine > curLine)
      this.MergeRtfLinePieces(curLine, this.e.CurLine);
    int row1;
    int col1;
    this.AbsToRowCol(abs1, out row1, out col1);
    int abs2 = this.RowColToAbs(this.e.CurLine, this.e.CurCol);
    if (flag2 && this.e.CurCol == 0 && this.e.CurLine > 0 && this.e.text[this.e.CurLine].cid != this.e.text[this.e.CurLine - 1].cid && this.e.text[this.e.CurLine - 1].cid > 0 && this.LineInfo(this.e.CurLine - 1, 48 /*0x30*/))
      --abs2;
    int row2;
    int col2;
    this.AbsToRowCol(abs2 - 1, out row2, out col2);
    if (num1 != 0)
      this.SaveUndo(row1, col1, row2, col2, 'I');
    if (!repaint)
      return num1 != 0;
    this.PaintTer();
    return num1 != 0;
  }

  internal bool IsRtfPlainFont(tc.ClsRtf rtf, ref tc.StrRtfGroup group)
  {
    return group.TypeFace.Length <= 0 && group.PointSize2 == 0 && (!(group.TextColor != this.e.TerFont[0].TextColor) || !(group.TextColor != tc.CLR_AUTO)) && !(group.TextBkColor != tc.CLR_WHITE) && !(group.UlineColor != tc.CLR_AUTO) && (group.style & -39937) == 0 && group.CharStyId == 1 && group.CharSet == 1 && group.AuxId == 0 && group.lang == rtf.lang && group.offset == 0 && group.CharBkPat == 0 && !(group.CharPatFC != group.TextColor) && !(group.CharPatBC != group.TextBkColor);
  }

  private bool IsSameRtfParaFrame(tc.StrRtfParaFrameInfo frame1, tc.StrRtfParaFrameInfo frame2)
  {
    return frame1.x == frame2.x && frame1.y == frame2.y && frame1.width == frame2.width && frame1.height == frame2.height && frame1.ZOrder == frame2.ZOrder && frame1.DistFromText == frame2.DistFromText;
  }

  internal int MakeRtfHiddenList(tc.ClsRtf rtf)
  {
    int length = 9;
    int num1 = 0;
    int num2 = 0;
    for (int index = 0; index < this.e.TotalLists; ++index)
    {
      if (this.e.list[index].id > num1)
        num1 = this.e.list[index].id;
      if (this.e.list[index].TmplId > num2)
        num2 = this.e.list[index].TmplId;
    }
    int num3 = num1 + 1;
    int num4 = num2 + 1;
    int listSlot;
    if ((listSlot = this.GetListSlot()) < 0)
      return 0;
    this.e.list[listSlot].InUse = true;
    this.e.list[listSlot].id = num3;
    this.e.list[listSlot].TmplId = num4;
    this.e.list[listSlot].FontId = 0;
    this.e.list[listSlot].LevelCount = length;
    this.e.list[listSlot].flags = 0;
    this.e.list[listSlot].name = "hidden " + listSlot.ToString();
    this.e.list[listSlot].level = new tc.StrListLevel[length];
    tc.StrListLevel[] level = this.e.list[listSlot].level;
    for (int index = 0; index < length; ++index)
    {
      level[index].NumType = (int) byte.MaxValue;
      level[index].start = 1;
      level[index].CharAft = 0;
      level[index].text = new char[50];
      level[index].text[0] = char.MinValue;
    }
    int listOrSlot;
    if ((listOrSlot = this.GetListOrSlot()) < 0)
      return 0;
    this.e.ListOr[listOrSlot].InUse = true;
    this.e.ListOr[listOrSlot].ListIdx = listSlot;
    this.e.ListOr[listOrSlot].LevelCount = 0;
    return listOrSlot;
  }

  internal bool MakeRtfObject(
    tc.ClsRtf rtf,
    byte[] data,
    int ObjectType,
    int ObjectSize,
    int ObjectAspect,
    bool ObjectUpdate)
  {
    tc.StrRtfObject strRtfObject;
    strRtfObject.ObjectType = ObjectType;
    strRtfObject.ObjectSize = ObjectSize;
    strRtfObject.ObjectAspect = ObjectAspect;
    strRtfObject.ObjectUpdate = ObjectUpdate;
    strRtfObject.pict = this.e.CurObject;
    strRtfObject.data = data;
    this.ImportRtfData(8, ref rtf.group[rtf.GroupLevel], (object) null, tc.SkipRtfPict, strRtfObject);
    return true;
  }

  internal bool MergeRtfLinePieces(int FirstLine, int LastLine)
  {
    int index1 = 0;
    char[] OldObj1 = new char[1];
    ushort[] OldObj2 = new ushort[1];
    int num1 = 0;
    int index2 = 0;
    if (LastLine >= this.e.TotalLines)
      LastLine = this.e.TotalLines <= 0 ? 0 : this.e.TotalLines - 1;
    for (; index2 <= this.e.CurLine; ++index2)
    {
      int num2 = index2 >= this.e.CurLine ? this.e.CurCol : this.e.text[index2].len;
      num1 += num2;
      if (num2 > 0 && index2 >= FirstLine && (int) this.e.text[index2].txt[num2 - 1] == (int) this.e.ParaChar)
        --num1;
    }
    int NewSize = 0;
    for (int index3 = FirstLine; index3 <= LastLine; ++index3)
    {
      if (NewSize == 0)
        index1 = index3;
      int len = this.e.text[index3].len;
      char[] txt1 = this.e.text[index3].txt;
      char chr = len <= 0 ? char.MinValue : txt1[len - 1];
      bool flag = false;
      if ((int) chr == (int) this.e.ParaChar || this.lstrchr(this.e.BreakChars, chr))
        flag = true;
      if (flag)
      {
        this.LineAlloc(index3, len, len - 1);
        len = this.e.text[index3].len;
      }
      if (NewSize != 0 || len < 1000)
      {
        int num3 = -1;
        if (NewSize > 0 && NewSize + len >= 1000)
          num3 = --index3;
        if (num3 == -1)
        {
          char[] txt2 = this.e.text[index3].txt;
          ushort[] numArray = this.OpenCfmt(index3);
          OldObj1 = this.ReAlloc(OldObj1, NewSize + len);
          OldObj2 = this.ReAlloc(OldObj2, NewSize + len);
          for (int index4 = 0; index4 < len; ++index4)
          {
            OldObj1[NewSize + index4] = txt2[index4];
            OldObj2[NewSize + index4] = numArray[index4];
          }
          this.CloseCfmt(index3);
          NewSize += len;
          if (flag || index3 == LastLine)
            num3 = index3;
        }
        if (num3 != -1)
        {
          if (num3 < index1)
            num3 = index1;
          if (num3 > index1)
          {
            this.LineAlloc(index1, this.e.text[index1].len, NewSize);
            char[] txt3 = this.e.text[index1].txt;
            ushort[] numArray = this.OpenCfmt(index1);
            for (int index5 = 0; index5 < NewSize; ++index5)
            {
              txt3[index5] = OldObj1[index5];
              numArray[index5] = OldObj2[index5];
            }
            this.CloseCfmt(index1);
            int count = num3 - index1;
            this.MoveLineArrays(index1 + 1, count, 'D');
            index3 -= count;
            LastLine -= count;
          }
          NewSize = 0;
        }
      }
    }
    for (int line = FirstLine; line <= LastLine; ++line)
    {
      this.e.text[line].pfmt = 0;
      this.e.TotalPfmts = 1;
      if (this.True(this.e.text[line].tabw))
        this.FreeTabw(line);
    }
    this.e.CurLine = this.e.CurCol = 0;
    while (num1 >= 0)
    {
      if (num1 >= this.e.text[this.e.CurLine].len)
      {
        num1 -= this.e.text[this.e.CurLine].len;
        if (this.e.CurLine >= this.e.TotalLines - 1)
        {
          this.e.CurCol = this.e.text[this.e.TotalLines - 1].len;
          break;
        }
        ++this.e.CurLine;
      }
      else
      {
        this.e.CurCol = num1;
        break;
      }
    }
    if (this.e.CurCol < 0)
      this.e.CurCol = 0;
    return true;
  }

  internal int ProcessRtfControl(tc.ClsRtf rtf)
  {
    tc.StrRtfGroup[] group = rtf.group;
    string lower = rtf.CurWord.ToLower();
    int groupLevel1 = rtf.GroupLevel;
    group[groupLevel1].IgnoreCount = 0;
    if (this.RtfCmp(lower, "par") != 0 && this.RtfCmp(lower, "line") != 0 && this.RtfCmp(lower, "lbr") != 0 && this.RtfCmp(lower, "cell") != 0 && this.RtfCmp(lower, "nestcell") != 0 && this.RtfCmp(lower, "tab") != 0 && this.RtfCmp(lower, "tx") != 0 && this.RtfCmp(lower, "u") != 0 && !this.SendRtfText(rtf))
      return 3;
    bool flag = true;
    if (this.RtfCmp(lower, nameof (rtf)) == 0)
      this.InitRtfGroup(rtf, rtf.GroupLevel);
    else if (this.RtfCmp(lower, "info") == 0)
    {
      if (this.e.RtfInput < 2)
        this.ReadRtfInfo(rtf);
      else
        this.SkipRtfGroup(rtf);
    }
    else if (this.RtfCmp(lower, "revtbl") == 0)
    {
      if (this.e.RtfInput < 2)
        this.ReadRtfReviewers(rtf);
      else
        this.SkipRtfGroup(rtf);
    }
    else if (this.RtfCmp(lower, "revised") == 0)
      group[groupLevel1].revised = this.True(rtf.IntParam);
    else if (this.RtfCmp(lower, "deleted") == 0)
      group[groupLevel1].deleted = this.True(rtf.IntParam);
    else if (this.RtfCmp(lower, "revauth") == 0 || this.RtfCmp(lower, "revauthdel") == 0)
    {
      int intParam = rtf.IntParam;
      int index = 0;
      while (index < this.e.TotalReviewers && this.e.reviewer[index].RtfId != intParam)
        ++index;
      if (index < this.e.TotalReviewers)
      {
        if (this.RtfCmp(lower, "revauth") == 0)
          group[groupLevel1].InsRev = index;
        else
          group[groupLevel1].DelRev = index;
      }
    }
    else if (this.RtfCmp(lower, "revdttm") == 0 || this.RtfCmp(lower, "revdttmdel") == 0)
    {
      uint intParam = (uint) rtf.IntParam;
      int bitVal1 = (int) this.GetBitVal(intParam, 5, 6);
      int bitVal2 = (int) this.GetBitVal(intParam, 10, 5);
      int bitVal3 = (int) this.GetBitVal(intParam, 15, 5);
      int bitVal4 = (int) this.GetBitVal(intParam, 19, 4);
      int year = (int) this.GetBitVal(intParam, 28, 9) + 1900;
      tc.ClsDateTime clsDateTime = new tc.ClsDateTime();
      clsDateTime.dt = new DateTime(year, bitVal4, bitVal3, bitVal2, bitVal1, 0);
      if (this.RtfCmp(lower, "revdttm") == 0)
        group[groupLevel1].InsTime = clsDateTime;
      else
        group[groupLevel1].DelTime = clsDateTime;
    }
    else if (this.RtfCmp(lower, "revisions") == 0)
    {
      if (this.e.RtfInput < 2)
        rtf.EnableTracking = true;
    }
    else if (this.RtfCmp(lower, "deflang") == 0)
    {
      rtf.lang = rtf.IntParam;
      if (this.e.RtfInput < 2)
        this.e.DefLang = rtf.lang;
    }
    else if (this.RtfCmp(lower, "lang") == 0)
      group[groupLevel1].lang = rtf.IntParam;
    else if (this.RtfCmp(lower, "mac") == 0)
      rtf.mac = true;
    else if (this.RtfCmp(lower, "viewkind") == 0)
    {
      if (this.e.RtfInput < 2)
        this.e.ViewKind = rtf.IntParam;
    }
    else if (this.RtfCmp(lower, "background") == 0)
    {
      if (this.e.RtfInput < 2)
        group[groupLevel1].gflags |= 65536 /*0x010000*/;
      else
        this.SkipRtfGroup(rtf);
    }
    else if (this.RtfCmp(lower, "formprot") == 0)
    {
      if (this.e.RtfInput < 2)
      {
        this.e.ProtectForm = true;
        this.e.TerArg.ReadOnly = true;
      }
    }
    else if (this.RtfCmp(lower, "deftab") == 0)
    {
      if (this.e.RtfInput < 2 && rtf.IntParam > 0)
        this.e.DefTabWidth = rtf.IntParam;
    }
    else if (this.RtfCmp(lower, "margl") == 0)
    {
      if (this.e.RtfInput < 2)
      {
        if (rtf.IntParam < 0)
          rtf.IntParam = 720;
        this.e.TerSect[0].LeftMargin = rtf.sect.LeftMargin = this.TwipsToInches(rtf.IntParam);
        rtf.flags2 |= 1;
      }
    }
    else if (this.RtfCmp(lower, "margr") == 0)
    {
      if (this.e.RtfInput < 2)
      {
        if (rtf.IntParam < 0)
          rtf.IntParam = 720;
        this.e.TerSect[0].RightMargin = rtf.sect.RightMargin = this.TwipsToInches(rtf.IntParam);
      }
    }
    else if (this.RtfCmp(lower, "margt") == 0)
    {
      if (this.e.RtfInput < 2)
      {
        if (rtf.IntParam < 0)
        {
          this.e.TerSect[0].flags |= 8;
          rtf.sect.flags |= 8;
          rtf.IntParam = -rtf.IntParam;
        }
        else
        {
          tc.ResetUintFlag(ref this.e.TerSect[0].flags, 8);
          tc.ResetUintFlag(ref rtf.sect.flags, 8);
        }
        this.e.TerSect[0].TopMargin = rtf.sect.TopMargin = this.TwipsToInches(rtf.IntParam);
      }
    }
    else if (this.RtfCmp(lower, "margb") == 0)
    {
      if (this.e.RtfInput < 2)
      {
        if (rtf.IntParam < 0)
        {
          this.e.TerSect[0].flags |= 16 /*0x10*/;
          rtf.sect.flags |= 16 /*0x10*/;
          rtf.IntParam = -rtf.IntParam;
        }
        else
        {
          tc.ResetUintFlag(ref this.e.TerSect[0].flags, 16 /*0x10*/);
          tc.ResetUintFlag(ref rtf.sect.flags, 16 /*0x10*/);
        }
        this.e.TerSect[0].BotMargin = rtf.sect.BotMargin = this.TwipsToInches(rtf.IntParam);
      }
    }
    else if (this.RtfCmp(lower, "paperw") == 0 || this.RtfCmp(lower, "paperh") == 0)
    {
      if (this.RtfCmp(lower, "paperw") == 0)
        rtf.PaperWidth = rtf.IntParam;
      if (this.RtfCmp(lower, "paperh") == 0)
        rtf.PaperHeight = rtf.IntParam;
      this.SetRtfDocPaperSize(rtf);
      if (this.RtfCmp(lower, "paperw") == 0)
        rtf.sect.PprWidth = this.TwipsToInches(rtf.IntParam);
      if (this.RtfCmp(lower, "paperh") == 0)
        rtf.sect.PprHeight = this.TwipsToInches(rtf.IntParam);
      this.SetRtfSectPaperSize(rtf);
    }
    else if (this.RtfCmp(lower, "psz") == 0)
    {
      int intParam = rtf.IntParam;
      int index = 0;
      while (index < tc.DefPaperCount && tc.DefPaperSize[index] != intParam)
        ++index;
      if (index < tc.DefPaperCount)
      {
        rtf.PaperWidth = (int) ((double) tc.DefPaperWidth[index] * 1440.0);
        rtf.PaperHeight = (int) ((double) tc.DefPaperHeight[index] * 1440.0);
        this.e.PprKind = tc.DefPaperKind[index];
        rtf.sect.PprWidth = tc.DefPaperWidth[index];
        rtf.sect.PprHeight = tc.DefPaperHeight[index];
        rtf.sect.PprKind = tc.DefPaperKind[index];
      }
    }
    else if (this.RtfCmp(lower, "landscape") == 0)
      this.e.IsPortrait = rtf.sect.IsPortrait = false;
    else if (this.RtfCmp(lower, "ftnnar") == 0)
    {
      if (this.e.RtfInput < 2)
        this.e.FootnoteNumFmt = 0;
    }
    else if (this.RtfCmp(lower, "ftnnalc") == 0)
    {
      if (this.e.RtfInput < 2)
        this.e.FootnoteNumFmt = 2;
    }
    else if (this.RtfCmp(lower, "ftnnauc") == 0)
    {
      if (this.e.RtfInput < 2)
        this.e.FootnoteNumFmt = 1;
    }
    else if (this.RtfCmp(lower, "ftnnrlc") == 0)
    {
      if (this.e.RtfInput < 2)
        this.e.FootnoteNumFmt = 4;
    }
    else if (this.RtfCmp(lower, "ftnnruc") == 0)
    {
      if (this.e.RtfInput < 2)
        this.e.FootnoteNumFmt = 3;
    }
    else if (this.RtfCmp(lower, "aftnnar") == 0)
    {
      if (this.e.RtfInput < 2)
        this.e.EndnoteNumFmt = 0;
    }
    else if (this.RtfCmp(lower, "aftnnalc") == 0)
    {
      if (this.e.RtfInput < 2)
        this.e.EndnoteNumFmt = 2;
    }
    else if (this.RtfCmp(lower, "aftnnauc") == 0)
    {
      if (this.e.RtfInput < 2)
        this.e.EndnoteNumFmt = 1;
    }
    else if (this.RtfCmp(lower, "aftnnrlc") == 0)
    {
      if (this.e.RtfInput < 2)
        this.e.EndnoteNumFmt = 4;
    }
    else if (this.RtfCmp(lower, "aftnnruc") == 0)
    {
      if (this.e.RtfInput < 2)
        this.e.EndnoteNumFmt = 3;
    }
    else if (this.RtfCmp(lower, "aenddoc") == 0)
    {
      if (this.e.RtfInput < 2)
        this.e.EndnoteAtSect = false;
    }
    else if (this.RtfCmp(lower, "notabind") == 0)
    {
      if (this.e.RtfInput < 2)
        this.e.NoTabIndent = true;
    }
    else if (this.RtfCmp(lower, "rtldoc") == 0)
    {
      if (this.e.RtfInput < 2)
        this.e.DocTextFlow = 2;
      rtf.DocFlow = 2;
    }
    else if (this.RtfCmp(lower, "ltrdoc") == 0)
    {
      if (this.e.RtfInput < 2)
        this.e.DocTextFlow = 1;
      rtf.DocFlow = 1;
    }
    else if (this.RtfCmp(lower, "sectd") == 0)
    {
      rtf.sect = this.e.TerSect[rtf.InitSect].Copy();
      group[groupLevel1].BorderType = 0;
      for (int index = 14; index <= 17; ++index)
      {
        int num;
        group[groupLevel1].BorderSpace[index] = num = 0;
        group[groupLevel1].BorderWidth[index] = num;
        group[groupLevel1].BorderColor[index] = tc.CLR_AUTO;
      }
      if (rtf.PaperWidth > 0 && rtf.PaperHeight > 0)
      {
        this.e.IsPortrait = true;
        rtf.sect.PprWidth = this.TwipsToInches(rtf.PaperWidth);
        rtf.sect.PprHeight = this.TwipsToInches(rtf.PaperHeight);
        rtf.sect.IsPortrait = this.e.IsPortrait;
        this.SetRtfSectPaperSize(rtf);
      }
      else if (!rtf.sect.IsPortrait)
      {
        float pprWidth = rtf.sect.PprWidth;
        rtf.sect.PprWidth = rtf.sect.PprHeight;
        rtf.sect.PprHeight = pprWidth;
      }
      rtf.SectFlow = 0;
    }
    else if (this.RtfCmp(lower, "linemod") == 0)
    {
      rtf.sect.flags |= 512 /*0x0200*/;
      rtf.sect.LineStep = rtf.IntParam;
    }
    else if (this.RtfCmp(lower, "marglsxn") == 0)
    {
      if (rtf.IntParam < 0)
        rtf.IntParam = 720;
      rtf.sect.LeftMargin = this.TwipsToInches(rtf.IntParam);
      rtf.flags2 |= 1;
    }
    else if (this.RtfCmp(lower, "margrsxn") == 0)
    {
      if (rtf.IntParam < 0)
        rtf.IntParam = 720;
      rtf.sect.RightMargin = this.TwipsToInches(rtf.IntParam);
    }
    else if (this.RtfCmp(lower, "margtsxn") == 0)
    {
      if (rtf.IntParam < 0)
      {
        rtf.IntParam = -rtf.IntParam;
        rtf.sect.flags |= 8;
      }
      else
        tc.ResetUintFlag(ref rtf.sect.flags, 8);
      rtf.sect.TopMargin = this.TwipsToInches(rtf.IntParam);
    }
    else if (this.RtfCmp(lower, "margbsxn") == 0)
    {
      if (rtf.IntParam < 0)
      {
        rtf.IntParam = -rtf.IntParam;
        rtf.sect.flags |= 16 /*0x10*/;
      }
      else
        tc.ResetUintFlag(ref rtf.sect.flags, 16 /*0x10*/);
      rtf.sect.BotMargin = this.TwipsToInches(rtf.IntParam);
    }
    else if (this.RtfCmp(lower, "pgbrdrt") == 0)
      group[groupLevel1].BorderType = 14;
    else if (this.RtfCmp(lower, "pgbrdrb") == 0)
      group[groupLevel1].BorderType = 15;
    else if (this.RtfCmp(lower, "pgbrdrl") == 0)
      group[groupLevel1].BorderType = 16 /*0x10*/;
    else if (this.RtfCmp(lower, "pgbrdrr") == 0)
      group[groupLevel1].BorderType = 17;
    else if (this.RtfCmp(lower, "pgbrdrhead") == 0)
      rtf.sect.flags |= 32 /*0x20*/;
    else if (this.RtfCmp(lower, "pgbrdrfoot") == 0)
      rtf.sect.flags |= 64 /*0x40*/;
    else if (this.RtfCmp(lower, "pgbrdropt") == 0)
      rtf.sect.BorderOpts = rtf.IntParam;
    else if (this.RtfCmp(lower, "headery") == 0)
    {
      if (rtf.IntParam < 0)
        rtf.IntParam = 720;
      rtf.sect.HdrMargin = this.TwipsToInches(rtf.IntParam);
    }
    else if (this.RtfCmp(lower, "footery") == 0)
    {
      if (rtf.IntParam < 0)
        rtf.IntParam = 720;
      rtf.sect.FtrMargin = this.TwipsToInches(rtf.IntParam);
    }
    else if (this.RtfCmp(lower, "sbknone") == 0)
      rtf.sect.flags = tc.ResetUintFlag(ref rtf.sect.flags, 1);
    else if (this.RtfCmp(lower, "sbkpage") == 0)
      rtf.sect.flags |= 1;
    else if (this.RtfCmp(lower, "pgnstarts") == 0)
      rtf.sect.FirstPageNo = (short) rtf.IntParam;
    else if (this.RtfCmp(lower, "pgnrestart") == 0)
      rtf.sect.flags |= 2;
    else if (this.RtfCmp(lower, "titlepg") == 0)
    {
      if (this.e.RtfInput < 2)
        rtf.sect.flags |= 4;
    }
    else if (this.RtfCmp(lower, "pgncont") == 0)
    {
      rtf.sect.FirstPageNo = (short) 0;
      rtf.sect.flags = tc.ResetUintFlag(ref rtf.sect.flags, 2);
    }
    else if (this.RtfCmp(lower, "vertal") == 0)
    {
      if ((group[groupLevel1].gflags & 128 /*0x80*/) == 0)
      {
        tc.ResetUintFlag(ref rtf.sect.flags, 384);
        rtf.sect.flags |= 256 /*0x0100*/;
      }
    }
    else if (this.RtfCmp(lower, "vertalc") == 0)
    {
      if ((group[groupLevel1].gflags & 128 /*0x80*/) == 0)
      {
        tc.ResetUintFlag(ref rtf.sect.flags, 384);
        rtf.sect.flags |= 128 /*0x80*/;
      }
    }
    else if (this.RtfCmp(lower, "pgndec") == 0)
      rtf.sect.PageNumFmt = 0;
    else if (this.RtfCmp(lower, "pgnucrm") == 0)
      rtf.sect.PageNumFmt = 3;
    else if (this.RtfCmp(lower, "pgnlcrm") == 0)
      rtf.sect.PageNumFmt = 4;
    else if (this.RtfCmp(lower, "pgnucltr") == 0)
      rtf.sect.PageNumFmt = 1;
    else if (this.RtfCmp(lower, "pgnlcltr") == 0)
      rtf.sect.PageNumFmt = 2;
    else if (this.RtfCmp(lower, "cols") == 0)
      rtf.sect.columns = rtf.IntParam;
    else if (this.RtfCmp(lower, "colsx") == 0)
      rtf.sect.ColumnSpace = this.TwipsToInches(rtf.IntParam);
    else if (this.RtfCmp(lower, "pgwsxn") == 0 || this.RtfCmp(lower, "pghsxn") == 0)
    {
      if (this.RtfCmp(lower, "pgwsxn") == 0)
        rtf.sect.PprWidth = this.TwipsToInches(rtf.IntParam);
      if (this.RtfCmp(lower, "pghsxn") == 0)
        rtf.sect.PprHeight = this.TwipsToInches(rtf.IntParam);
      this.SetRtfSectPaperSize(rtf);
    }
    else if (this.RtfCmp(lower, "lndscpsxn") == 0)
      rtf.sect.IsPortrait = false;
    else if (this.RtfCmp(lower, "binfsxn") == 0)
      rtf.sect.FirstPageBin = (PaperSourceKind) rtf.IntParam;
    else if (this.RtfCmp(lower, "binsxn") == 0)
      rtf.sect.bin = (PaperSourceKind) rtf.IntParam;
    else if (this.RtfCmp(lower, "rtlsect") == 0)
    {
      rtf.sect.flow = 2;
      rtf.SectFlow = 2;
    }
    else if (this.RtfCmp(lower, "ltrsect") == 0)
    {
      rtf.sect.flow = 1;
      rtf.SectFlow = 1;
    }
    else if (this.RtfCmp(lower, "sectlinegrid") == 0)
      rtf.sect.LineSpace = rtf.IntParam;
    else if (this.RtfCmp(lower, "sectspecifyl") == 0)
      rtf.sect.flags |= 1024 /*0x0400*/;
    else if (this.RtfCmp(lower, "sect") == 0)
    {
      if (this.False(group[groupLevel1].InTable) && this.False(this.e.RtfInHdrFtr))
      {
        if (rtf.OutBufLen > 0 && !this.SendRtfText(rtf))
          return 3;
        rtf.OutBuf = new string('\u0014', 1);
        rtf.OutBufLen = 1;
        if (!this.SendRtfText(rtf))
          return 3;
        this.SetRtfParaDefault(rtf, group);
        rtf.flags = tc.ResetUintFlag(ref rtf.flags, 3072 /*0x0C00*/);
        rtf.flags1 = tc.ResetUintFlag(ref rtf.flags1, 24);
      }
    }
    else if (this.RtfCmp(lower, "header") == 0 || this.RtfCmp(lower, "headerl") == 0 || this.RtfCmp(lower, "headerr") == 0)
    {
      if ((rtf.flags & 1024 /*0x0400*/) != 0 || (rtf.flags1 & 16384 /*0x4000*/) != 0)
      {
        this.SkipRtfGroup(rtf);
      }
      else
      {
        if (rtf.OutBufLen > 0 && !this.SendRtfText(rtf))
          return 3;
        groupLevel1 = rtf.GroupLevel;
        group[groupLevel1].flags |= 4096 /*0x1000*/;
        rtf.HdrFtrChar = '\u0011';
        rtf.OutBuf = new string('\u0011', 1);
        rtf.OutBufLen = 1;
        if (!this.SendRtfText(rtf))
          return 3;
        this.e.RtfInHdrFtr = 4096 /*0x1000*/;
        rtf.flags = tc.ResetUintFlag(ref rtf.flags, 32 /*0x20*/);
        rtf.flags |= 1152;
        rtf.flags1 |= 32768 /*0x8000*/;
      }
    }
    else if (this.RtfCmp(lower, "headerf") == 0)
    {
      if ((rtf.flags1 & 8) != 0 || (rtf.flags1 & 16384 /*0x4000*/) != 0)
      {
        this.SkipRtfGroup(rtf);
      }
      else
      {
        if (rtf.OutBufLen > 0 && !this.SendRtfText(rtf))
          return 3;
        groupLevel1 = rtf.GroupLevel;
        group[groupLevel1].flags |= 4096 /*0x1000*/;
        rtf.HdrFtrChar = '\u0019';
        rtf.OutBuf = new string('\u0019', 1);
        rtf.OutBufLen = 1;
        if (!this.SendRtfText(rtf))
          return 3;
        this.e.RtfInHdrFtr = 4096 /*0x1000*/;
        rtf.flags = tc.ResetUintFlag(ref rtf.flags, 32 /*0x20*/);
        rtf.flags |= 128 /*0x80*/;
        rtf.flags1 |= 32776;
      }
    }
    else if (this.RtfCmp(lower, "footer") == 0 || this.RtfCmp(lower, "footerl") == 0 || this.RtfCmp(lower, "footerr") == 0)
    {
      if ((rtf.flags & 2048 /*0x0800*/) != 0 || (rtf.flags1 & 16384 /*0x4000*/) != 0)
      {
        this.SkipRtfGroup(rtf);
      }
      else
      {
        if (rtf.OutBufLen > 0 && !this.SendRtfText(rtf))
          return 3;
        groupLevel1 = rtf.GroupLevel;
        group[groupLevel1].flags |= 8192 /*0x2000*/;
        rtf.HdrFtrChar = '\u0010';
        rtf.OutBuf = new string('\u0010', 1);
        rtf.OutBufLen = 1;
        if (!this.SendRtfText(rtf))
          return 3;
        this.e.RtfInHdrFtr = 8192 /*0x2000*/;
        rtf.flags = tc.ResetUintFlag(ref rtf.flags, 32 /*0x20*/);
        rtf.flags |= 2176;
        rtf.flags1 |= 32768 /*0x8000*/;
      }
    }
    else if (this.RtfCmp(lower, "footerf") == 0)
    {
      if ((rtf.flags1 & 16 /*0x10*/) != 0 || (rtf.flags1 & 16384 /*0x4000*/) != 0)
      {
        this.SkipRtfGroup(rtf);
      }
      else
      {
        if (rtf.OutBufLen > 0 && !this.SendRtfText(rtf))
          return 3;
        groupLevel1 = rtf.GroupLevel;
        group[groupLevel1].flags |= 8192 /*0x2000*/;
        rtf.HdrFtrChar = '\u001A';
        rtf.OutBuf = new string('\u001A', 1);
        rtf.OutBufLen = 1;
        if (!this.SendRtfText(rtf))
          return 3;
        this.e.RtfInHdrFtr = 8192 /*0x2000*/;
        rtf.flags = tc.ResetUintFlag(ref rtf.flags, 32 /*0x20*/);
        rtf.flags |= 128 /*0x80*/;
        rtf.flags1 |= 32784;
      }
    }
    else if (this.RtfCmp(lower, "uc") == 0)
      group[groupLevel1].UcIgnoreCount = rtf.IntParam;
    else if (this.RtfCmp(lower, "u") == 0)
    {
      char c = (char) rtf.IntParam;
      if (!rtf.OutBufHasUnicode)
        this.SendRtfText(rtf);
      if (c == ' ')
        c = ' ';
      if (c == '‑')
        c = '\u0017';
      rtf.OutBuf += new string(c, 1);
      ++rtf.OutBufLen;
      rtf.OutBufHasUnicode = true;
      group[groupLevel1].IgnoreCount = group[groupLevel1].UcIgnoreCount;
    }
    else if (this.RtfCmp(lower, "fonttbl") == 0)
    {
      switch (this.ReadRtfFontTable(rtf))
      {
        case 1:
          return 1;
        case 2:
          return 2;
      }
    }
    else if (this.RtfCmp(lower, "colortbl") == 0)
    {
      switch (this.ReadRtfColorTable(rtf))
      {
        case 1:
          return 1;
        case 2:
          return 2;
      }
    }
    else if (this.RtfCmp(lower, "deff") == 0)
    {
      int intParam = rtf.IntParam;
      group[groupLevel1].DefFont = intParam;
    }
    else if (this.RtfCmp(lower, "stylesheet") == 0)
      group[groupLevel1].gflags |= 16 /*0x10*/;
    else if (this.RtfCmp(lower, "s") == 0)
    {
      if (rtf.IntParam < 0)
        rtf.IntParam = 0;
      if ((group[groupLevel1].gflags & 16 /*0x10*/) != 0)
        group[groupLevel1].ParaStyId = rtf.IntParam;
      else
        this.InitGroupFromStyle(rtf, rtf.IntParam, 2, false);
    }
    else if (this.RtfCmp(lower, "cs") == 0)
    {
      if ((group[groupLevel1].gflags & 16 /*0x10*/) != 0)
      {
        group[groupLevel1].CharStyId = rtf.IntParam;
        group[groupLevel1].gflags |= 8;
      }
      else
        this.InitGroupFromStyle(rtf, rtf.IntParam, 1, false);
    }
    else if (this.RtfCmp(lower, "snext") == 0)
    {
      if ((group[groupLevel1].gflags & 16 /*0x10*/) != 0)
        group[groupLevel1].NextStyId = rtf.IntParam;
    }
    else if (this.RtfCmp(lower, "object") == 0)
    {
      switch (this.ReadRtfObject(rtf))
      {
        case 1:
          return 1;
        case 2:
          return 2;
      }
    }
    else if (this.RtfCmp(lower, "shp") == 0)
    {
      rtf.ShpGroup = groupLevel1;
      group[groupLevel1].gflags |= 33554432 /*0x02000000*/;
    }
    else if (this.RtfCmp(lower, "shpgrp") == 0)
    {
      rtf.ShpGroup = groupLevel1;
      group[groupLevel1].gflags |= 536870912 /*0x20000000*/;
    }
    else if (this.RtfCmp(lower, "shpinst") == 0)
    {
      if ((group[groupLevel1].gflags & 33554432 /*0x02000000*/) != 0)
        this.ReadRtfShape(rtf);
      else if ((group[groupLevel1].gflags & 536870912 /*0x20000000*/) != 0)
        this.ReadRtfShpGrp(rtf);
    }
    else if (this.RtfCmp(lower, "shprslt") == 0)
    {
      if ((group[groupLevel1].gflags & 128 /*0x80*/) != 0 && (group[groupLevel1].gflags & 33554432 /*0x02000000*/) == 0)
      {
        if ((rtf.flags & 16384 /*0x4000*/) != 0)
          this.ExtractRtfPict(rtf);
      }
      else if ((group[groupLevel1].gflags & 536870912 /*0x20000000*/) != 0)
        this.SkipRtfGroup(rtf);
      else if ((rtf.flags & 16384 /*0x4000*/) == 0)
      {
        this.SkipRtfGroup(rtf);
      }
      else
      {
        if (!group[groupLevel1].InTable && this.e.RtfCurRowId > 0)
          this.e.RtfCurRowId = this.e.RtfCurCellId = this.e.RtfLastCellX = 0;
        if (this.e.RtfCurRowId > 0)
          this.SkipRtfGroup(rtf);
        else
          group[groupLevel1].gflags |= 256 /*0x0100*/;
      }
    }
    else if (this.RtfCmp(lower, "shppict") == 0)
    {
      if ((group[groupLevel1].gflags & 33554432 /*0x02000000*/) != 0)
      {
        if ((group[groupLevel1].gflags & 128 /*0x80*/) == 0)
          this.ReadRtfShape(rtf);
      }
      else
        rtf.flags |= 16384 /*0x4000*/;
    }
    else if (this.RtfCmp(lower, "nonshppict") == 0)
    {
      if ((rtf.flags & 16384 /*0x4000*/) == 0)
        this.SkipRtfGroup(rtf);
    }
    else if (this.RtfCmp(lower, "sn") == 0)
    {
      string str;
      this.ReadRtfShapeParam(rtf, out str);
      if (this.strcmpi(str, "fillBlip") == 0)
      {
        if (groupLevel1 > 0)
          group[groupLevel1 - 1].shape.FrmFlags |= 8388608 /*0x800000*/;
        group[groupLevel1].shape.FrmFlags |= 8388608 /*0x800000*/;
      }
    }
    else if (this.RtfCmp(lower, "sv") == 0)
    {
      if ((rtf.flags & 16384 /*0x4000*/) != 0)
      {
        this.ReadRtfShapeProp(rtf, "pict");
        if ((group[groupLevel1].shape.FrmFlags & 8388608 /*0x800000*/) != 0 && group[groupLevel1].shape.FillPict > 0 && this.e.RtfParaFID > 0)
        {
          this.e.ParaFrame[this.e.RtfParaFID].ShapeType = 0;
          tc.ResetUintFlag(ref this.e.ParaFrame[this.e.RtfParaFID].flags, 1024 /*0x0400*/);
          this.e.ParaFrame[this.e.RtfParaFID].flags |= 8388608 /*0x800000*/;
          this.e.ParaFrame[this.e.RtfParaFID].FillPict = group[groupLevel1].shape.FillPict;
        }
      }
      else
        this.SkipRtfGroup(rtf);
    }
    else if (this.RtfCmp(lower, "ssanimseq") == 0)
      group[groupLevel1].gflags |= 1024 /*0x0400*/;
    else if (this.RtfCmp(lower, "ssanimloops") == 0)
    {
      group[groupLevel1].AnimLoops = rtf.IntParam;
      group[groupLevel1].gflags |= 2048 /*0x0800*/;
    }
    else if (this.RtfCmp(lower, "ssanimdelay") == 0)
    {
      group[groupLevel1].AnimDelay = rtf.IntParam;
      group[groupLevel1].gflags |= 2048 /*0x0800*/;
    }
    else if (this.RtfCmp(lower, "pict") == 0 || this.RtfCmp(lower, "sscontrol") == 0)
    {
      switch (this.ReadRtfPicture(rtf))
      {
        case 1:
          return 1;
        case 2:
          return 2;
      }
    }
    else if (this.RtfCmp(lower, "sslinkpictw") == 0)
      group[groupLevel1].LinkPictWidth = rtf.IntParam;
    else if (this.RtfCmp(lower, "sslinkpicth") == 0)
      group[groupLevel1].LinkPictHeight = rtf.IntParam;
    else if (this.RtfCmp(lower, "subpictid") == 0)
    {
      if (rtf.OutBufLen > 0 && !this.SendRtfText(rtf))
        return 3;
      groupLevel1 = rtf.GroupLevel;
      group[groupLevel1].PictId = rtf.IntParam;
      rtf.OutBuf = new string('-', 1);
      rtf.OutBufLen = 1;
      if (!this.SendRtfText(rtf))
        return 3;
      group[groupLevel1].PictId = 0;
    }
    else if (this.RtfCmp(lower, "tc") == 0)
    {
      group[groupLevel1].FieldId = 13;
      rtf.FieldCode = (string) null;
      tc.ResetUintFlag(ref group[groupLevel1].style, 64 /*0x40*/);
    }
    else if (this.RtfCmp(lower, "tcl") == 0)
    {
      if (group[groupLevel1].FieldId == 13)
      {
        rtf.FieldCode = "\\tcl" + rtf.IntParam.ToString();
        for (int index = groupLevel1; index >= 0 && group[index].FieldId == 13; --index)
          tc.ResetUintFlag(ref group[index].style, 64 /*0x40*/);
      }
    }
    else if (this.RtfCmp(lower, "xe") == 0)
    {
      if (rtf.PrevField == 15)
      {
        rtf.OutBuf += " ";
        ++rtf.OutBufLen;
        if (!this.SendRtfText(rtf))
          return 3;
      }
      group[groupLevel1].FieldId = 15;
    }
    else if (this.RtfCmp(lower, "plain") == 0)
      this.SetRtfFontDefault(rtf, group);
    else if (this.RtfCmp(lower, "loch") == 0)
      group[groupLevel1].CharType = 1;
    else if (this.RtfCmp(lower, "hich") == 0)
      group[groupLevel1].CharType = 2;
    else if (this.RtfCmp(lower, "dbch") == 0)
      group[groupLevel1].CharType = 3;
    else if (this.RtfCmp(lower, "rtlch") == 0)
      group[groupLevel1].rtlch = true;
    else if (this.RtfCmp(lower, "ltrch") == 0)
      group[groupLevel1].rtlch = false;
    else if (this.RtfCmp(lower, "f") == 0 || this.RtfCmp(lower, "af") == 0)
    {
      int intParam = rtf.IntParam;
      tc.StrRtfGroup rtfGroup = this.GetRtfGroup(rtf);
      groupLevel1 = rtf.GroupLevel;
      int index;
      if (intParam >= 0 && intParam < 500)
      {
        index = intParam;
      }
      else
      {
        index = 500;
        while (index < rtfGroup.MaxRtfFonts && (!rtfGroup.font[index].InUse || rtfGroup.font[index].FontId != intParam))
          ++index;
      }
      if (index < rtfGroup.MaxRtfFonts)
      {
        if (group[groupLevel1].CharType == 3)
        {
          group[groupLevel1].TypeFaceDB = rtfGroup.font[index].name;
          group[groupLevel1].FontFamilyDB = rtfGroup.font[index].family;
          group[groupLevel1].CharSetDB = rtfGroup.font[index].CharSet;
        }
        else if (group[groupLevel1].CharType == 1)
        {
          group[groupLevel1].TypeFace = rtfGroup.font[index].name;
          group[groupLevel1].FontFamily = rtfGroup.font[index].family;
          group[groupLevel1].CharSet = rtfGroup.font[index].CharSet;
        }
        else if (group[groupLevel1].CharType == 2)
        {
          group[groupLevel1].TypeFaceHi = rtfGroup.font[index].name;
          group[groupLevel1].FontFamilyHi = rtfGroup.font[index].family;
          group[groupLevel1].CharSetHi = rtfGroup.font[index].CharSet;
        }
        else if (this.RtfCmp(lower, "f") == 0)
        {
          group[groupLevel1].TypeFace = rtfGroup.font[index].name;
          group[groupLevel1].FontFamily = rtfGroup.font[index].family;
          group[groupLevel1].CharSet = rtfGroup.font[index].CharSet;
          group[groupLevel1].TypeFaceHi = rtfGroup.font[index].name;
          group[groupLevel1].FontFamilyHi = rtfGroup.font[index].family;
          group[groupLevel1].CharSetHi = rtfGroup.font[index].CharSet;
          group[groupLevel1].TypeFaceDB = rtfGroup.font[index].name;
          group[groupLevel1].FontFamilyDB = rtfGroup.font[index].family;
          group[groupLevel1].CharSetDB = rtfGroup.font[index].CharSet;
        }
      }
    }
    else if (this.RtfCmp(lower, "fs") == 0)
    {
      groupLevel1 = rtf.GroupLevel;
      group[groupLevel1].PointSize2 = rtf.IntParam;
    }
    else if (this.RtfCmp(lower, "sscharaux") == 0)
    {
      groupLevel1 = rtf.GroupLevel;
      if (!this.e.HtmlMode && group[groupLevel1].FieldId != 2)
        group[groupLevel1].AuxId = rtf.IntParam;
    }
    else if (this.RtfCmp(lower, "cf") == 0)
    {
      tc.StrRtfColor[] color = this.GetRtfGroup(rtf).color;
      groupLevel1 = rtf.GroupLevel;
      if (rtf.param.Length == 0)
        rtf.IntParam = 0;
      if (rtf.IntParam >= 0 && rtf.IntParam < this.e.MaxRtfColors)
        group[groupLevel1].TextColor = color[rtf.IntParam].color;
    }
    else if (this.RtfCmp(lower, "ulc") == 0)
    {
      tc.StrRtfColor[] color = this.GetRtfGroup(rtf).color;
      groupLevel1 = rtf.GroupLevel;
      if (rtf.param.Length == 0)
        rtf.IntParam = 0;
      if (rtf.IntParam >= 0 && rtf.IntParam < this.e.MaxRtfColors)
        group[groupLevel1].UlineColor = color[rtf.IntParam].color;
    }
    else if (this.RtfCmp(lower, "chbghoriz") == 0 || this.RtfCmp(lower, "chbgvert") == 0 || this.RtfCmp(lower, "chbgfdiag") == 0 || this.RtfCmp(lower, "chbgbdiag") == 0 || this.RtfCmp(lower, "chbgcross") == 0 || this.RtfCmp(lower, "chbgdcross") == 0 || this.RtfCmp(lower, "chbgdkhoriz") == 0 || this.RtfCmp(lower, "chbgdkvert") == 0 || this.RtfCmp(lower, "chbgdkfdiag") == 0 || this.RtfCmp(lower, "chbgdkbdiag") == 0 || this.RtfCmp(lower, "chbgdkcross") == 0 || this.RtfCmp(lower, "chbgdkdcross") == 0)
      group[groupLevel1].CharBkPat = 2;
    else if (this.RtfCmp(lower, "chcfpat") == 0 || this.RtfCmp(lower, "chcbpat") == 0)
    {
      tc.StrRtfColor[] color = this.GetRtfGroup(rtf).color;
      groupLevel1 = rtf.GroupLevel;
      if (rtf.param.Length == 0)
        rtf.IntParam = 0;
      if (rtf.IntParam >= 0 && rtf.IntParam < this.e.MaxRtfColors)
      {
        if (this.RtfCmp(lower, "chcfpat") == 0)
          group[groupLevel1].CharPatFC = color[rtf.IntParam].color;
        else
          group[groupLevel1].CharPatBC = color[rtf.IntParam].color;
      }
    }
    else if (this.RtfCmp(lower, "cb") == 0 || this.RtfCmp(lower, "highlight") == 0)
    {
      tc.StrRtfColor[] color = this.GetRtfGroup(rtf).color;
      groupLevel1 = rtf.GroupLevel;
      if (rtf.param.Length == 0)
        rtf.IntParam = 0;
      if (rtf.IntParam == 0)
        group[groupLevel1].TextBkColor = this.e.TextDefBkColor;
      else if (rtf.IntParam >= 0 && rtf.IntParam < this.e.MaxRtfColors)
        group[groupLevel1].TextBkColor = color[rtf.IntParam].color;
    }
    else if (this.RtfCmp(lower, "b") == 0)
    {
      groupLevel1 = rtf.GroupLevel;
      if (this.True(this.True(rtf.IntParam)))
      {
        group[groupLevel1].style |= 2;
      }
      else
      {
        group[groupLevel1].style &= -3;
        group[groupLevel1].StyleOff |= 2;
      }
    }
    else if (this.RtfCmp(lower, "sshlink") == 0)
    {
      groupLevel1 = rtf.GroupLevel;
      if (this.True(this.True(rtf.IntParam)))
        group[groupLevel1].style |= 16384 /*0x4000*/;
      else
        group[groupLevel1].style &= -16385;
    }
    else if (this.RtfCmp(lower, "i") == 0)
    {
      groupLevel1 = rtf.GroupLevel;
      if (this.True(rtf.IntParam))
      {
        group[groupLevel1].style |= 4;
      }
      else
      {
        group[groupLevel1].style &= -5;
        group[groupLevel1].StyleOff |= 4;
      }
    }
    else if (this.RtfCmp(lower, "v") == 0)
    {
      groupLevel1 = rtf.GroupLevel;
      if (this.True(rtf.IntParam) && group[groupLevel1].FieldId == 13)
      {
        for (int index = groupLevel1; index >= 0 && group[index].FieldId == 13; --index)
          group[index].style |= 64 /*0x40*/;
      }
      else if (this.True(rtf.IntParam))
        group[groupLevel1].style |= 64 /*0x40*/;
      else
        group[groupLevel1].style &= -65;
    }
    else if (this.RtfCmp(lower, "dn") == 0)
    {
      groupLevel1 = rtf.GroupLevel;
      if (rtf.param == "0")
      {
        group[groupLevel1].offset = 0;
      }
      else
      {
        if (rtf.IntParam == 0)
          rtf.IntParam = 6;
        group[groupLevel1].offset = -this.PointsToTwips((float) rtf.IntParam) / 2;
      }
    }
    else if (this.RtfCmp(lower, "sub") == 0)
    {
      groupLevel1 = rtf.GroupLevel;
      group[groupLevel1].style |= 32 /*0x20*/;
    }
    else if (this.RtfCmp(lower, "nosupersub") == 0)
      group[groupLevel1].style = tc.ResetUintFlag(ref group[groupLevel1].style, 48 /*0x30*/);
    else if (this.RtfCmp(lower, "strike") == 0)
    {
      groupLevel1 = rtf.GroupLevel;
      if (this.True(rtf.IntParam))
      {
        group[groupLevel1].style |= 8;
      }
      else
      {
        group[groupLevel1].style &= -9;
        group[groupLevel1].StyleOff |= 8;
      }
    }
    else if (this.RtfCmp(lower, "striked") == 0)
    {
      groupLevel1 = rtf.GroupLevel;
      if (this.True(rtf.IntParam))
      {
        group[groupLevel1].style |= 524288 /*0x080000*/;
      }
      else
      {
        group[groupLevel1].style &= -524289;
        group[groupLevel1].StyleOff |= 524288 /*0x080000*/;
      }
    }
    else if (this.RtfCmp(lower, "protect") == 0)
    {
      groupLevel1 = rtf.GroupLevel;
      if (this.True(rtf.IntParam))
      {
        group[groupLevel1].style |= 512 /*0x0200*/;
      }
      else
      {
        group[groupLevel1].style &= -513;
        group[groupLevel1].StyleOff |= 512 /*0x0200*/;
      }
    }
    else if (this.RtfCmp(lower, "ul") == 0 || this.RtfCmp(lower, "uld") == 0 || this.RtfCmp(lower, "ulw") == 0)
    {
      groupLevel1 = rtf.GroupLevel;
      if (this.True(rtf.IntParam))
      {
        group[groupLevel1].style |= 1;
      }
      else
      {
        group[groupLevel1].style &= -2;
        group[groupLevel1].StyleOff |= 1;
      }
    }
    else if (this.RtfCmp(lower, "uldb") == 0)
    {
      groupLevel1 = rtf.GroupLevel;
      if (this.True(rtf.IntParam))
      {
        group[groupLevel1].style |= 256 /*0x0100*/;
      }
      else
      {
        group[groupLevel1].style &= -257;
        group[groupLevel1].StyleOff |= 256 /*0x0100*/;
      }
    }
    else if (this.RtfCmp(lower, "ulnone") == 0)
    {
      groupLevel1 = rtf.GroupLevel;
      group[groupLevel1].style &= -258;
      group[groupLevel1].StyleOff |= 257;
    }
    else if (this.RtfCmp(lower, "expnd") == 0)
      group[groupLevel1].expand = rtf.IntParam * 20 / 4;
    else if (this.RtfCmp(lower, "expndtw") == 0)
      group[groupLevel1].expand = rtf.IntParam;
    else if (this.RtfCmp(lower, "charscalex") == 0)
      group[groupLevel1].CharScaleX = rtf.IntParam;
    else if (this.RtfCmp(lower, "up") == 0)
    {
      groupLevel1 = rtf.GroupLevel;
      if (rtf.param == "0")
      {
        group[groupLevel1].offset = 0;
      }
      else
      {
        if (rtf.IntParam == 0)
          rtf.IntParam = 6;
        group[groupLevel1].offset = this.PointsToTwips((float) rtf.IntParam) / 2;
      }
    }
    else if (this.RtfCmp(lower, "super") == 0)
    {
      groupLevel1 = rtf.GroupLevel;
      group[groupLevel1].style |= 16 /*0x10*/;
    }
    else if (this.RtfCmp(lower, "scaps") == 0 || this.RtfCmp(lower, "caps") == 0)
    {
      groupLevel1 = rtf.GroupLevel;
      int num = this.RtfCmp(lower, "scaps") == 0 ? 131072 /*0x020000*/ : 65536 /*0x010000*/;
      if (this.True(rtf.IntParam))
      {
        group[groupLevel1].style |= num;
      }
      else
      {
        group[groupLevel1].style &= ~num;
        group[groupLevel1].StyleOff |= num;
      }
    }
    else if (this.RtfCmp(lower, "tab") == 0)
    {
      rtf.OutBuf += new string('\t', 1);
      ++rtf.OutBufLen;
    }
    else if (this.RtfCmp(lower, "chpgn") == 0)
    {
      if (rtf.OutBufLen > 0 && !this.SendRtfText(rtf))
        return 1;
      rtf.OutBuf = "1";
      rtf.OutBufLen = 1;
      rtf.group[groupLevel1].FieldId = 1;
      if (!this.SendRtfText(rtf))
        return 1;
      rtf.group[groupLevel1].FieldId = 0;
    }
    else if (this.RtfCmp(lower, "page") == 0)
    {
      if (this.False(group[groupLevel1].InTable) && this.False(this.e.RtfInHdrFtr))
      {
        rtf.OutBuf += new string('\f', 1);
        ++rtf.OutBufLen;
        if (!this.SendRtfText(rtf))
          return 3;
      }
    }
    else if (this.RtfCmp(lower, "column") == 0)
    {
      if (this.False(group[groupLevel1].InTable))
      {
        rtf.OutBuf += new string('\u0016', 1);
        ++rtf.OutBufLen;
      }
    }
    else if (this.RtfCmp(lower, "zwnj") == 0)
    {
      rtf.OutBuf += new string('\u0004', 1);
      ++rtf.OutBufLen;
      if (!this.SendRtfText(rtf))
        return 3;
    }
    else if (this.RtfCmp(lower, "par") == 0 || this.RtfCmp(lower, "line") == 0 || this.RtfCmp(lower, "lbr") == 0)
    {
      if (this.RtfCmp(lower, "par") == 0)
      {
        rtf.OutBuf += new string(this.e.ParaChar, 1);
      }
      else
      {
        rtf.OutBuf += new string('\u000F', 1);
        if (this.RtfCmp(lower, "lbr") == 0)
          group[groupLevel1].CharId = rtf.IntParam;
      }
      ++rtf.OutBufLen;
      if (!this.SendRtfText(rtf))
        return 3;
      group[groupLevel1].CharId = 0;
    }
    else if (this.RtfCmp(lower, "pard") == 0)
      this.SetRtfParaDefault(rtf, group);
    else if (this.RtfCmp(lower, "ql") == 0)
    {
      groupLevel1 = rtf.GroupLevel;
      group[groupLevel1].flags &= -2052;
      group[groupLevel1].flags |= 1024 /*0x0400*/;
    }
    else if (this.RtfCmp(lower, "qr") == 0)
    {
      groupLevel1 = rtf.GroupLevel;
      group[groupLevel1].flags &= -3074;
      group[groupLevel1].flags |= 2;
    }
    else if (this.RtfCmp(lower, "qc") == 0)
    {
      groupLevel1 = rtf.GroupLevel;
      group[groupLevel1].flags &= -3075;
      group[groupLevel1].flags |= 1;
    }
    else if (this.RtfCmp(lower, "rtlpar") == 0)
      group[groupLevel1].flow = 2;
    else if (this.RtfCmp(lower, "ltrpar") == 0)
      group[groupLevel1].flow = 1;
    else if (this.RtfCmp(lower, "qj") == 0 || this.RtfCmp(lower, "qk") == 0)
    {
      groupLevel1 = rtf.GroupLevel;
      group[groupLevel1].flags &= -1028;
      group[groupLevel1].flags |= 2048 /*0x0800*/;
    }
    else if (this.RtfCmp(lower, "fi") == 0)
    {
      groupLevel1 = rtf.GroupLevel;
      group[groupLevel1].FirstIndent = rtf.IntParam;
    }
    else if (this.RtfCmp(lower, "li") == 0)
    {
      groupLevel1 = rtf.GroupLevel;
      group[groupLevel1].LeftIndent = rtf.IntParam;
    }
    else if (this.RtfCmp(lower, "ri") == 0)
    {
      groupLevel1 = rtf.GroupLevel;
      group[groupLevel1].RightIndent = rtf.IntParam;
      if (group[groupLevel1].RightIndent > 32000)
        group[groupLevel1].RightIndent = 0;
    }
    else if (this.RtfCmp(lower, "sb") == 0)
    {
      groupLevel1 = rtf.GroupLevel;
      group[groupLevel1].SpaceBefore = rtf.IntParam;
    }
    else if (this.RtfCmp(lower, "sbauto") == 0)
    {
      groupLevel1 = rtf.GroupLevel;
      if (rtf.IntParam == 1)
        group[groupLevel1].flags |= 131072 /*0x020000*/;
    }
    else if (this.RtfCmp(lower, "sa") == 0)
    {
      groupLevel1 = rtf.GroupLevel;
      group[groupLevel1].SpaceAfter = rtf.IntParam;
    }
    else if (this.RtfCmp(lower, "saauto") == 0)
    {
      groupLevel1 = rtf.GroupLevel;
      if (rtf.IntParam == 1)
        group[groupLevel1].flags |= 262144 /*0x040000*/;
    }
    else if (this.RtfCmp(lower, "sl") == 0)
    {
      group[groupLevel1].flags = tc.ResetUintFlag(ref group[groupLevel1].flags, 4);
      group[groupLevel1].SpaceBetween = rtf.IntParam;
      if (group[groupLevel1].SpaceBetween == 1)
        group[groupLevel1].SpaceBetween = 0;
    }
    else if (this.RtfCmp(lower, "slmult") == 0)
    {
      groupLevel1 = rtf.GroupLevel;
      if (rtf.IntParam == 1)
      {
        if (group[groupLevel1].SpaceBetween == 480)
        {
          group[groupLevel1].flags |= 4;
          group[groupLevel1].LineSpacing = 0;
        }
        else if (group[groupLevel1].SpaceBetween > 0)
        {
          group[groupLevel1].LineSpacing = this.MulDiv(group[groupLevel1].SpaceBetween - 240 /*0xF0*/, 100, 240 /*0xF0*/);
          tc.ResetUintFlag(ref group[groupLevel1].flags, 4);
        }
        group[groupLevel1].SpaceBetween = 0;
      }
    }
    else if (this.RtfCmp(lower, "keep") == 0)
    {
      groupLevel1 = rtf.GroupLevel;
      group[groupLevel1].flags |= 16384 /*0x4000*/;
    }
    else if (this.RtfCmp(lower, "keepn") == 0)
    {
      groupLevel1 = rtf.GroupLevel;
      group[groupLevel1].flags |= 32768 /*0x8000*/;
    }
    else if (this.RtfCmp(lower, "widowctrl") == 0)
    {
      rtf.SetWidowOrphan = true;
      group[groupLevel1].pflags |= 32 /*0x20*/;
    }
    else if (this.RtfCmp(lower, "widctlpar") == 0)
      group[groupLevel1].pflags |= 32 /*0x20*/;
    else if (this.RtfCmp(lower, "ssparnw") == 0)
      group[groupLevel1].pflags |= 16 /*0x10*/;
    else if (this.RtfCmp(lower, "nowidctlpar") == 0)
      group[groupLevel1].pflags = tc.ResetUintFlag(ref group[groupLevel1].pflags, 32 /*0x20*/);
    else if (this.RtfCmp(lower, "pagebb") == 0)
      group[groupLevel1].pflags |= 64 /*0x40*/;
    else if (this.RtfCmp(lower, "outlinelevel") == 0)
      group[groupLevel1].OutlineLevel = rtf.IntParam;
    else if (this.RtfCmp(lower, "pn") == 0)
    {
      switch (this.ReadRtfBullet(rtf))
      {
        case 1:
          return 1;
        case 2:
          return 2;
      }
    }
    else if (this.RtfCmp(lower, "listtable") == 0)
    {
      switch (this.ReadRtfList(rtf))
      {
        case 1:
          return 1;
        case 2:
          return 2;
      }
    }
    else if (this.RtfCmp(lower, "listoverridetable") == 0)
    {
      switch (this.ReadRtfListOr(rtf))
      {
        case 1:
          return 1;
        case 2:
          return 2;
      }
    }
    else if (this.RtfCmp(lower, "ls") == 0)
      group[groupLevel1].RtfLs = rtf.IntParam;
    else if (this.RtfCmp(lower, "ilvl") == 0)
      group[groupLevel1].ListLvl = rtf.IntParam;
    else if (this.RtfCmp(lower, "listtext") == 0)
      this.SkipRtfGroup(rtf);
    else if (this.RtfCmp(lower, "chbrdr") == 0)
    {
      if (this.True(rtf.IntParam))
      {
        group[groupLevel1].BorderType = 13;
        group[groupLevel1].style |= 8192 /*0x2000*/;
      }
      else
      {
        group[groupLevel1].style &= -8193;
        group[groupLevel1].StyleOff |= 8192 /*0x2000*/;
      }
    }
    else if (this.RtfCmp(lower, "box") == 0)
      group[groupLevel1].BorderType = 12;
    else if (this.RtfCmp(lower, "brdrt") == 0)
      group[groupLevel1].BorderType = 0;
    else if (this.RtfCmp(lower, "brdrb") == 0)
      group[groupLevel1].BorderType = 1;
    else if (this.RtfCmp(lower, "brdrl") == 0)
      group[groupLevel1].BorderType = 2;
    else if (this.RtfCmp(lower, "brdrr") == 0)
      group[groupLevel1].BorderType = 3;
    else if (this.RtfCmp(lower, "brdrbtw") == 0)
      group[groupLevel1].BorderType = 18;
    else if (this.RtfCmp(lower, "brdrs") == 0)
    {
      int borderType = group[groupLevel1].BorderType;
      this.SetRtfParaBorders(ref group[groupLevel1], borderType);
      if (this.IsSectionBorder(borderType))
      {
        this.SetRtfSectBorders(ref group[groupLevel1], borderType);
        rtf.sect.BorderType = 0;
      }
    }
    else if (this.RtfCmp(lower, "brdrnone") == 0)
    {
      int borderType = group[groupLevel1].BorderType;
      group[groupLevel1].BorderWidth[borderType] = 0;
      if (this.IsSectionBorder(borderType))
      {
        this.SetRtfSectBorders(ref group[groupLevel1], borderType);
        rtf.sect.BorderType = 8;
      }
    }
    else if (this.RtfCmp(lower, "brdrsh") == 0)
    {
      int borderType = group[groupLevel1].BorderType;
      group[groupLevel1].BorderWidth[borderType] = 0;
      if (this.IsSectionBorder(borderType))
      {
        this.SetRtfSectBorders(ref group[groupLevel1], borderType);
        rtf.sect.BorderType = 3;
      }
    }
    else if (this.RtfCmp(lower, "brdrsh") == 0)
    {
      int borderType = group[groupLevel1].BorderType;
      group[groupLevel1].BorderWidth[borderType] = 0;
      if (this.IsSectionBorder(borderType))
      {
        this.SetRtfSectBorders(ref group[groupLevel1], borderType);
        rtf.sect.BorderType = 3;
      }
    }
    else if (this.RtfCmp(lower, "brdrtnthsg") == 0 || this.RtfCmp(lower, "brdrtnthmg") == 0 || this.RtfCmp(lower, "brdrtnthlg") == 0)
    {
      int borderType = group[groupLevel1].BorderType;
      group[groupLevel1].BorderWidth[borderType] = 0;
      if (this.IsSectionBorder(borderType))
      {
        this.SetRtfSectBorders(ref group[groupLevel1], borderType);
        rtf.sect.BorderType = 6;
      }
    }
    else if (this.RtfCmp(lower, "brdrtnthtnsg") == 0 || this.RtfCmp(lower, "brdrtnthtnmg") == 0 || this.RtfCmp(lower, "brdrtnthtnlg") == 0)
    {
      int borderType = group[groupLevel1].BorderType;
      group[groupLevel1].BorderWidth[borderType] = 0;
      if (this.IsSectionBorder(borderType))
      {
        this.SetRtfSectBorders(ref group[groupLevel1], borderType);
        rtf.sect.BorderType = 7;
      }
    }
    else if (this.RtfCmp(lower, "brdrthtnsg") == 0 || this.RtfCmp(lower, "brdrthtnmg") == 0 || this.RtfCmp(lower, "brdrthtnlg") == 0)
    {
      int borderType = group[groupLevel1].BorderType;
      group[groupLevel1].BorderWidth[borderType] = 0;
      if (this.IsSectionBorder(borderType))
      {
        this.SetRtfSectBorders(ref group[groupLevel1], borderType);
        rtf.sect.BorderType = 4;
      }
    }
    else if (this.RtfCmp(lower, "brdrthtnthg") == 0 || this.RtfCmp(lower, "brdrthtnthmg") == 0 || this.RtfCmp(lower, "brdrthtnthlg") == 0)
    {
      int borderType = group[groupLevel1].BorderType;
      group[groupLevel1].BorderWidth[borderType] = 0;
      if (this.IsSectionBorder(borderType))
      {
        this.SetRtfSectBorders(ref group[groupLevel1], borderType);
        rtf.sect.BorderType = 5;
      }
    }
    else if (this.RtfCmp(lower, "brdrth") == 0)
    {
      int borderType = group[groupLevel1].BorderType;
      this.SetRtfParaBorders(ref group[groupLevel1], borderType);
      group[groupLevel1].flags |= 512 /*0x0200*/;
      group[groupLevel1].BorderWidth[borderType] = 50;
      if (this.IsSectionBorder(borderType))
        this.SetRtfSectBorders(ref group[groupLevel1], borderType);
    }
    else if (this.RtfCmp(lower, "brdrw") == 0)
    {
      int borderType = group[groupLevel1].BorderType;
      this.SetRtfParaBorders(ref group[groupLevel1], borderType);
      if (rtf.IntParam > 20)
        group[groupLevel1].flags |= 512 /*0x0200*/;
      group[groupLevel1].BorderWidth[borderType] = rtf.IntParam;
      if (this.IsSectionBorder(borderType))
        this.SetRtfSectBorders(ref group[groupLevel1], borderType);
    }
    else if (this.RtfCmp(lower, "brdrdb") == 0)
    {
      int borderType = group[groupLevel1].BorderType;
      this.SetRtfParaBorders(ref group[groupLevel1], borderType);
      group[groupLevel1].flags |= 256 /*0x0100*/;
      group[groupLevel1].BorderWidth[borderType] = 50;
      if (this.IsSectionBorder(borderType))
      {
        this.SetRtfSectBorders(ref group[groupLevel1], borderType);
        rtf.sect.BorderType = 1;
      }
    }
    else if (this.RtfCmp(lower, "brdrtriple") == 0)
    {
      int borderType = group[groupLevel1].BorderType;
      this.SetRtfParaBorders(ref group[groupLevel1], borderType);
      group[groupLevel1].flags |= 256 /*0x0100*/;
      group[groupLevel1].BorderWidth[borderType] = 50;
      if (this.IsSectionBorder(borderType))
      {
        this.SetRtfSectBorders(ref group[groupLevel1], borderType);
        rtf.sect.BorderType = 2;
      }
    }
    else if (this.RtfCmp(lower, "brdrcf") == 0)
    {
      tc.StrRtfColor[] color = this.GetRtfGroup(rtf).color;
      if (rtf.param.Length == 0)
        rtf.IntParam = 0;
      int borderType = group[groupLevel1].BorderType;
      if (rtf.IntParam >= 0 && rtf.IntParam < this.e.MaxRtfColors)
        group[groupLevel1].BorderColor[borderType] = color[rtf.IntParam].color;
      this.SetRtfParaBorders(ref group[groupLevel1], borderType);
      if ((group[groupLevel1].flags & 65776 /*0x0100F0*/) != 0)
        group[groupLevel1].ParaBorderColor = color[rtf.IntParam].color;
      if (rtf.param.Length == 0)
        rtf.IntParam = 0;
      if (rtf.IntParam >= 0 && rtf.IntParam < this.e.MaxRtfColors)
        group[groupLevel1].BorderColor[borderType] = color[rtf.IntParam].color;
      if (this.IsSectionBorder(borderType))
      {
        this.SetRtfSectBorders(ref group[groupLevel1], borderType);
        rtf.sect.BorderColor = color[rtf.IntParam].color;
      }
    }
    else if (this.RtfCmp(lower, "brsp") == 0)
    {
      groupLevel1 = rtf.GroupLevel;
      int borderType = group[groupLevel1].BorderType;
      this.SetRtfParaBorders(ref group[groupLevel1], borderType);
      group[groupLevel1].BorderSpace[borderType] = rtf.IntParam;
      group[groupLevel1].BorderMargin = rtf.IntParam;
      if (this.IsSectionBorder(borderType))
        this.SetRtfSectBorders(ref group[groupLevel1], borderType);
    }
    else if (this.RtfCmp(lower, "shading") == 0)
    {
      groupLevel1 = rtf.GroupLevel;
      group[groupLevel1].ParShading = rtf.IntParam;
    }
    else if (this.RtfCmp(lower, "cbpat") == 0)
    {
      tc.StrRtfColor[] color = this.GetRtfGroup(rtf).color;
      group[groupLevel1].ParaBkColor = color[rtf.IntParam].color;
    }
    else if (this.RtfCmp(lower, "cfpat") == 0)
    {
      tc.StrRtfColor[] color = this.GetRtfGroup(rtf).color;
      group[groupLevel1].ParaBkColor = color[rtf.IntParam].color;
      group[groupLevel1].ParShading = 0;
    }
    else
      flag = false;
    int groupLevel2;
    if (!flag)
    {
      if (this.RtfCmp(lower, "phpg") == 0)
      {
        if (!group[groupLevel1].InTable && this.e.RtfCurRowId > 0 && !rtf.TableInFrame)
          this.e.RtfCurRowId = this.e.RtfCurCellId = this.e.RtfLastCellX = 0;
        int groupLevel3 = rtf.GroupLevel;
        group[groupLevel3].FrmFlags |= 3;
        group[groupLevel3].HPageGroup = groupLevel3;
        group[groupLevel3].gflags |= 192 /*0xC0*/;
      }
      else if (this.RtfCmp(lower, "pvpg") == 0)
      {
        if (!group[groupLevel1].InTable && this.e.RtfCurRowId > 0 && !rtf.TableInFrame)
          this.e.RtfCurRowId = this.e.RtfCurCellId = this.e.RtfLastCellX = 0;
        group[groupLevel1].FrmFlags |= 34;
        group[groupLevel1].gflags |= 192 /*0xC0*/;
      }
      else if (this.RtfCmp(lower, "pvmrg") == 0)
      {
        if (!group[groupLevel1].InTable && this.e.RtfCurRowId > 0 && !rtf.TableInFrame)
          this.e.RtfCurRowId = this.e.RtfCurCellId = this.e.RtfLastCellX = 0;
        group[groupLevel1].FrmFlags |= 66;
        group[groupLevel1].gflags |= 192 /*0xC0*/;
      }
      else if (this.RtfCmp(lower, "posxr") == 0)
      {
        if (!group[groupLevel1].InTable && this.e.RtfCurRowId > 0 && !rtf.TableInFrame)
          this.e.RtfCurRowId = this.e.RtfCurCellId = this.e.RtfLastCellX = 0;
        group[groupLevel1].FrmFlags |= 6;
        group[groupLevel1].gflags |= 192 /*0xC0*/;
      }
      else if (this.RtfCmp(lower, "posxc") == 0)
      {
        if (!group[groupLevel1].InTable && this.e.RtfCurRowId > 0 && !rtf.TableInFrame)
          this.e.RtfCurRowId = this.e.RtfCurCellId = this.e.RtfLastCellX = 0;
        group[groupLevel1].FrmFlags |= 10;
        group[groupLevel1].gflags |= 192 /*0xC0*/;
      }
      else if (this.RtfCmp(lower, "posyc") == 0)
      {
        if (!group[groupLevel1].InTable && this.e.RtfCurRowId > 0 && !rtf.TableInFrame)
          this.e.RtfCurRowId = this.e.RtfCurCellId = this.e.RtfLastCellX = 0;
        group[groupLevel1].FrmFlags |= 33554434 /*0x02000002*/;
        group[groupLevel1].gflags |= 192 /*0xC0*/;
      }
      else if (this.RtfCmp(lower, "posx") == 0 || this.RtfCmp(lower, "posnegx") == 0)
      {
        if (!this.e.ignoreRtfFrameSize)
        {
          if (!group[groupLevel1].InTable && this.e.RtfCurRowId > 0 && !rtf.TableInFrame)
            this.e.RtfCurRowId = this.e.RtfCurCellId = this.e.RtfLastCellX = 0;
          group[groupLevel1].ParaFrameInfo.x = rtf.IntParam;
          group[groupLevel1].FrmFlags |= 2;
          group[groupLevel1].gflags |= 192 /*0xC0*/;
        }
      }
      else if (this.RtfCmp(lower, "posxi") == 0)
      {
        if (!group[groupLevel1].InTable && this.e.RtfCurRowId > 0 && !rtf.TableInFrame)
          this.e.RtfCurRowId = this.e.RtfCurCellId = this.e.RtfLastCellX = 0;
        group[groupLevel1].ParaFrameInfo.x = 0;
        group[groupLevel1].FrmFlags |= 2;
        group[groupLevel1].gflags |= 192 /*0xC0*/;
      }
      else if (this.RtfCmp(lower, "posy") == 0 || this.RtfCmp(lower, "posnegy") == 0)
      {
        if (!this.e.ignoreRtfFrameSize)
        {
          if (!group[groupLevel1].InTable && this.e.RtfCurRowId > 0 && !rtf.TableInFrame)
            this.e.RtfCurRowId = this.e.RtfCurCellId = this.e.RtfLastCellX = 0;
          group[groupLevel1].ParaFrameInfo.y = rtf.IntParam;
          group[groupLevel1].FrmFlags |= 2;
          group[groupLevel1].gflags |= 192 /*0xC0*/;
        }
      }
      else if (this.RtfCmp(lower, "posyt") == 0)
      {
        if (!group[groupLevel1].InTable && this.e.RtfCurRowId > 0 && !rtf.TableInFrame)
          this.e.RtfCurRowId = this.e.RtfCurCellId = this.e.RtfLastCellX = 0;
        group[groupLevel1].ParaFrameInfo.y = 0;
        group[groupLevel1].FrmFlags |= 2;
        if ((group[groupLevel1].FrmFlags & 32 /*0x20*/) == 0)
          group[groupLevel1].FrmFlags |= 64 /*0x40*/;
        group[groupLevel1].gflags |= 192 /*0xC0*/;
      }
      else if (this.RtfCmp(lower, "absw") == 0)
      {
        if (!this.e.ignoreRtfFrameSize)
        {
          if (!group[groupLevel1].InTable && this.e.RtfCurRowId > 0 && !rtf.TableInFrame)
            this.e.RtfCurRowId = this.e.RtfCurCellId = this.e.RtfLastCellX = 0;
          group[groupLevel1].ParaFrameInfo.width = rtf.IntParam;
          group[groupLevel1].FrmFlags |= 2;
          group[groupLevel1].gflags |= 192 /*0xC0*/;
        }
      }
      else if (this.RtfCmp(lower, "absh") == 0)
      {
        if (!this.e.ignoreRtfFrameSize)
        {
          if (!group[groupLevel1].InTable && this.e.RtfCurRowId > 0 && !rtf.TableInFrame)
            this.e.RtfCurRowId = this.e.RtfCurCellId = this.e.RtfLastCellX = 0;
          group[groupLevel1].ParaFrameInfo.height = Math.Abs(rtf.IntParam);
          if (rtf.IntParam < 0)
            group[groupLevel1].FrmFlags |= 67108864 /*0x04000000*/;
          group[groupLevel1].FrmFlags |= 2;
          group[groupLevel1].gflags |= 192 /*0xC0*/;
        }
      }
      else if (this.RtfCmp(lower, "dfrmtxtx") == 0)
      {
        if (!group[groupLevel1].InTable && this.e.RtfCurRowId > 0 && !rtf.TableInFrame)
          this.e.RtfCurRowId = this.e.RtfCurCellId = this.e.RtfLastCellX = 0;
        group[groupLevel1].ParaFrameInfo.DistFromText = Math.Abs(rtf.IntParam);
      }
      else if (this.RtfCmp(lower, "nowrap") == 0)
        group[groupLevel1].FrmFlags |= 8192 /*0x2000*/;
      else if (this.RtfCmp(lower, "frmtxtblr") == 0 || this.RtfCmp(lower, "frmtxtbrl") == 0 || this.RtfCmp(lower, "frmtxtbrlv") == 0)
        group[groupLevel1].TextAngle = 270;
      else if (this.RtfCmp(lower, "frmtxbtlr") == 0)
        group[groupLevel1].TextAngle = 90;
      else if (this.RtfCmp(lower, "do") == 0)
      {
        group[groupLevel1].gflags |= 16577;
        rtf.flags |= 64 /*0x40*/;
        this.e.RtfParaFID = 0;
        group[groupLevel1].TextAngle = 0;
      }
      else if (this.RtfCmp(lower, "dptxbtlr") == 0)
        group[groupLevel1].TextAngle = 90;
      else if (this.RtfCmp(lower, "dptxtbrl") == 0)
        group[groupLevel1].TextAngle = 270;
      else if (this.RtfCmp(lower, "dobxpage") == 0)
      {
        group[groupLevel1].FrmFlags |= 1;
        group[groupLevel1].HPageGroup = groupLevel1;
      }
      else if (this.RtfCmp(lower, "dobypage") == 0)
        group[groupLevel1].FrmFlags |= 32 /*0x20*/;
      else if (this.RtfCmp(lower, "dobymargin") == 0)
        group[groupLevel1].FrmFlags |= 64 /*0x40*/;
      else if (this.RtfCmp(lower, "dodhgt") == 0)
      {
        group[groupLevel1].ParaFrameInfo.ZOrder = rtf.IntParam;
        if (group[groupLevel1].ParaFrameInfo.ZOrder == 0)
          group[groupLevel1].ParaFrameInfo.ZOrder = -1;
      }
      else if (this.RtfCmp(lower, "dptxbxmar") == 0)
        group[groupLevel1].TextBoxMargin = rtf.IntParam;
      else if (this.RtfCmp(lower, "dptxbx") == 0)
        group[groupLevel1].FrmFlags |= 130;
      else if (this.RtfCmp(lower, "dpgroup") == 0)
      {
        group[groupLevel1].gflags |= 67108864 /*0x04000000*/;
        if (group[groupLevel1].DpGroupCount < 10)
          ++group[groupLevel1].DpGroupCount;
      }
      else if (this.RtfCmp(lower, "dpendgroup") == 0)
      {
        this.e.RtfParaFID = 0;
        if (group[groupLevel1].DpGroupCount > 0)
          --group[groupLevel1].DpGroupCount;
        tc.ResetUintFlag(ref group[groupLevel1].gflags, 67108864 /*0x04000000*/);
      }
      else if (this.RtfCmp(lower, "dpline") == 0)
      {
        this.e.RtfParaFID = 0;
        tc.ResetUintFlag(ref group[groupLevel1].gflags, 67108864 /*0x04000000*/);
        group[groupLevel1].FrmFlags |= 258;
        rtf.OutBuf += new string(this.e.ParaChar, 1);
        ++rtf.OutBufLen;
        this.e.RtfParaFID = 0;
        if (!this.SendRtfText(rtf))
          return 3;
        this.e.ParaFrame[this.e.RtfParaFID].x = this.e.ParaFrame[this.e.RtfParaFID].ParaY = -1;
        this.e.ParaFrame[this.e.RtfParaFID].width = this.e.ParaFrame[this.e.RtfParaFID].height = 1;
        this.e.ParaFrame[this.e.RtfParaFID].LineType = 0;
      }
      else if (this.RtfCmp(lower, "dprect") == 0)
      {
        this.e.RtfParaFID = 0;
        tc.ResetUintFlag(ref group[groupLevel1].gflags, 67108864 /*0x04000000*/);
        group[groupLevel1].FrmFlags |= 514;
        rtf.OutBuf += new string(this.e.ParaChar, 1);
        ++rtf.OutBufLen;
        if (!this.SendRtfText(rtf))
          return 3;
      }
      else if (this.RtfCmp(lower, "dptxbxtext") == 0)
      {
        this.e.RtfParaFID = 0;
        group[groupLevel1].gflags |= 4;
        tc.ResetUintFlag(ref group[groupLevel1].gflags, 67108864 /*0x04000000*/);
      }
      else if (this.RtfCmp(lower, "dpptx") == 0 && this.e.RtfParaFID > 0)
      {
        int index = group[groupLevel1].DpGroupCount - 1;
        if (index >= 0 && (group[groupLevel1].gflags & 67108864 /*0x04000000*/) == 0)
          rtf.IntParam += group[groupLevel1].DpGroupX[index];
        if (this.e.ParaFrame[this.e.RtfParaFID].x < 0)
        {
          this.e.ParaFrame[this.e.RtfParaFID].x = rtf.IntParam;
        }
        else
        {
          this.e.ParaFrame[this.e.RtfParaFID].width = rtf.IntParam - this.e.ParaFrame[this.e.RtfParaFID].x;
          group[groupLevel1].gflags |= 2097152 /*0x200000*/;
          if ((group[groupLevel1].gflags & 4194304 /*0x400000*/) != 0)
            this.SetRtfLineOrient(rtf, false, false);
        }
      }
      else if (this.RtfCmp(lower, "dppty") == 0 && this.e.RtfParaFID > 0)
      {
        int index = group[groupLevel1].DpGroupCount - 1;
        if (index >= 0 && (group[groupLevel1].gflags & 67108864 /*0x04000000*/) == 0)
          rtf.IntParam += group[groupLevel1].DpGroupX[index];
        if (this.e.ParaFrame[this.e.RtfParaFID].ParaY < 0)
        {
          this.e.ParaFrame[this.e.RtfParaFID].ParaY = rtf.IntParam;
        }
        else
        {
          this.e.ParaFrame[this.e.RtfParaFID].height = this.e.ParaFrame[this.e.RtfParaFID].MinHeight = rtf.IntParam - this.e.ParaFrame[this.e.RtfParaFID].ParaY;
          group[groupLevel1].gflags |= 4194304 /*0x400000*/;
          if ((group[groupLevel1].gflags & 2097152 /*0x200000*/) != 0)
            this.SetRtfLineOrient(rtf, false, false);
        }
      }
      else if (this.RtfCmp(lower, "dpx") == 0)
      {
        int index1 = group[groupLevel1].DpGroupCount - 1;
        if ((group[groupLevel1].gflags & 67108864 /*0x04000000*/) != 0)
        {
          if (index1 >= 0)
            group[groupLevel1].DpGroupX[index1] = rtf.IntParam;
          if (index1 - 1 >= 0)
          {
            int[] dpGroupX;
            IntPtr index2;
            (dpGroupX = group[groupLevel1].DpGroupX)[(int) (index2 = (IntPtr) index1)] = dpGroupX[(int) index2] + group[groupLevel1].DpGroupX[index1 - 1];
          }
        }
        else if (this.e.RtfParaFID > 0)
        {
          if ((this.e.ParaFrame[this.e.RtfParaFID].flags & 640) != 0)
            this.e.ParaFrame[this.e.RtfParaFID].x = 0;
          if (index1 >= 0)
            this.e.ParaFrame[this.e.RtfParaFID].x = group[groupLevel1].DpGroupX[index1];
          this.e.ParaFrame[this.e.RtfParaFID].x += rtf.IntParam;
          if ((this.e.ParaFrame[this.e.RtfParaFID].flags & 1) != 0)
          {
            this.e.ParaFrame[this.e.RtfParaFID].x -= (int) this.InchesToTwips((double) rtf.sect.LeftMargin);
            this.e.ParaFrame[this.e.RtfParaFID].flags = tc.ResetUintFlag(ref this.e.ParaFrame[this.e.RtfParaFID].flags, 1);
          }
        }
      }
      else if (this.RtfCmp(lower, "dpy") == 0)
      {
        int index3 = group[groupLevel1].DpGroupCount - 1;
        if ((group[groupLevel1].gflags & 67108864 /*0x04000000*/) != 0)
        {
          if (index3 >= 0)
            group[groupLevel1].DpGroupY[index3] = rtf.IntParam;
          if (index3 - 1 >= 0)
          {
            int[] dpGroupY;
            IntPtr index4;
            (dpGroupY = group[groupLevel1].DpGroupY)[(int) (index4 = (IntPtr) index3)] = dpGroupY[(int) index4] + group[groupLevel1].DpGroupY[index3 - 1];
          }
        }
        else if (this.e.RtfParaFID > 0)
        {
          if ((this.e.ParaFrame[this.e.RtfParaFID].flags & 640) != 0)
            this.e.ParaFrame[this.e.RtfParaFID].ParaY = 0;
          if (index3 >= 0)
            this.e.ParaFrame[this.e.RtfParaFID].ParaY = group[groupLevel1].DpGroupY[index3];
          this.e.ParaFrame[this.e.RtfParaFID].ParaY += rtf.IntParam;
        }
      }
      else if (this.RtfCmp(lower, "dpxsize") == 0 && this.e.RtfParaFID > 0)
      {
        groupLevel2 = rtf.GroupLevel;
        this.e.ParaFrame[this.e.RtfParaFID].width = rtf.IntParam;
        tc.ResetUintFlag(ref this.e.ParaFrame[this.e.RtfParaFID].flags, 16 /*0x10*/);
      }
      else if (this.RtfCmp(lower, "dpysize") == 0 && this.e.RtfParaFID > 0)
      {
        groupLevel2 = rtf.GroupLevel;
        this.e.ParaFrame[this.e.RtfParaFID].height = this.e.ParaFrame[this.e.RtfParaFID].MinHeight = rtf.IntParam;
      }
      else if (this.RtfCmp(lower, "dplinehollow") == 0 && this.e.RtfParaFID > 0)
        group[groupLevel1].gflags |= 512 /*0x0200*/;
      else if (this.RtfCmp(lower, "dplinesolid") == 0 && this.e.RtfParaFID > 0)
        this.e.ParaFrame[this.e.RtfParaFID].flags |= 1024 /*0x0400*/;
      else if (this.RtfCmp(lower, "dplinedot") == 0 && this.e.RtfParaFID > 0)
        this.e.ParaFrame[this.e.RtfParaFID].flags |= 3072 /*0x0C00*/;
      else if (this.RtfCmp(lower, "dplinew") == 0 && this.e.RtfParaFID > 0)
      {
        this.e.ParaFrame[this.e.RtfParaFID].LineWdth = rtf.IntParam;
        if (rtf.IntParam > 0 && (group[groupLevel1].gflags & 512 /*0x0200*/) == 0)
          this.e.ParaFrame[this.e.RtfParaFID].flags |= 1024 /*0x0400*/;
      }
      else if (this.RtfCmp(lower, "dplinecor") == 0 && this.e.RtfParaFID > 0)
      {
        Color lineColor = this.e.ParaFrame[this.e.RtfParaFID].LineColor;
        this.e.ParaFrame[this.e.RtfParaFID].LineColor = this.ToColor(rtf.IntParam, (int) lineColor.G, (int) lineColor.B);
      }
      else if (this.RtfCmp(lower, "dplinecog") == 0 && this.e.RtfParaFID > 0)
      {
        Color lineColor = this.e.ParaFrame[this.e.RtfParaFID].LineColor;
        this.e.ParaFrame[this.e.RtfParaFID].LineColor = this.ToColor((int) lineColor.R, rtf.IntParam, (int) lineColor.B);
      }
      else if (this.RtfCmp(lower, "dplinecob") == 0 && this.e.RtfParaFID > 0)
      {
        Color lineColor = this.e.ParaFrame[this.e.RtfParaFID].LineColor;
        this.e.ParaFrame[this.e.RtfParaFID].LineColor = this.ToColor((int) lineColor.R, (int) lineColor.G, rtf.IntParam);
      }
      else if (this.RtfCmp(lower, "dpfillbgcr") == 0 && this.e.RtfParaFID > 0)
      {
        Color backColor = this.e.ParaFrame[this.e.RtfParaFID].BackColor;
        this.e.ParaFrame[this.e.RtfParaFID].BackColor = this.ToColor(rtf.IntParam, (int) backColor.G, (int) backColor.B);
      }
      else if (this.RtfCmp(lower, "dpfillbgcg") == 0 && this.e.RtfParaFID > 0)
      {
        Color backColor = this.e.ParaFrame[this.e.RtfParaFID].BackColor;
        this.e.ParaFrame[this.e.RtfParaFID].BackColor = this.ToColor((int) backColor.R, rtf.IntParam, (int) backColor.B);
      }
      else if (this.RtfCmp(lower, "dpfillbgcb") == 0 && this.e.RtfParaFID > 0)
      {
        Color backColor = this.e.ParaFrame[this.e.RtfParaFID].BackColor;
        this.e.ParaFrame[this.e.RtfParaFID].BackColor = this.ToColor((int) backColor.R, (int) backColor.G, rtf.IntParam);
      }
      else if (this.RtfCmp(lower, "dpfillbggray") == 0 && this.e.RtfParaFID > 0)
      {
        int num = (int) byte.MaxValue - rtf.IntParam * (int) byte.MaxValue / 200;
        this.e.ParaFrame[this.e.RtfParaFID].BackColor = this.ToColor(num, num, num);
      }
      else if (this.RtfCmp(lower, "dpfillpat") == 0 && this.e.RtfParaFID > 0)
        this.e.ParaFrame[this.e.RtfParaFID].FillPattern = rtf.IntParam;
      else if (this.RtfCmp(lower, "tqr") == 0)
        rtf.CurTabType = 1;
      else if (this.RtfCmp(lower, "tqdec") == 0)
        rtf.CurTabType = 3;
      else if (this.RtfCmp(lower, "tqc") == 0)
        rtf.CurTabType = 2;
      else if (this.RtfCmp(lower, "tldot") == 0)
        rtf.CurTabFlags = (byte) 1;
      else if (this.RtfCmp(lower, "tlhyph") == 0)
        rtf.CurTabFlags = (byte) 2;
      else if (this.RtfCmp(lower, "tlul") == 0)
        rtf.CurTabFlags = (byte) 4;
      else if (this.RtfCmp(lower, "tx") == 0)
      {
        int groupLevel4 = rtf.GroupLevel;
        int count = group[groupLevel4].tab.count;
        if (count < 20)
        {
          int index = 0;
          while (index < count && (group[groupLevel4].tab.pos[index] != rtf.IntParam || group[groupLevel4].tab.type[index] != rtf.CurTabType))
            ++index;
          if (index == count)
          {
            group[groupLevel4].tab.pos[count] = rtf.IntParam;
            group[groupLevel4].tab.type[count] = rtf.CurTabType;
            group[groupLevel4].tab.flags[count] = rtf.CurTabFlags;
            ++group[groupLevel4].tab.count;
          }
        }
        rtf.CurTabType = 0;
        rtf.CurTabFlags = (byte) 0;
      }
      else if (this.RtfCmp(lower, "sssubtable") == 0)
      {
        int level = group[groupLevel1].level;
        rtf.flags1 |= 128 /*0x80*/;
        this.SetRtfTblLevel(rtf, groupLevel1, level + 1, level);
      }
      else if (this.RtfCmp(lower, "itap") == 0)
      {
        int num = rtf.IntParam;
        if ((group[groupLevel1].style & 39936) == 0)
        {
          if (num < 0)
            num = 0;
          group[groupLevel1].level = this.e.RtfInitLevel + num;
          if (!rtf.EmbedTable && this.e.RtfCurCellId != this.e.RtfInitCellId)
            --group[groupLevel1].level;
        }
      }
      else if (this.RtfCmp(lower, "nonesttables") == 0)
        this.SkipRtfGroup(rtf);
      else if (this.RtfCmp(lower, "nesttableprops") == 0)
        group[groupLevel1].gflags |= 524288 /*0x080000*/;
      else if (this.RtfCmp(lower, "trowd") == 0)
      {
        if ((group[groupLevel1].FrmFlags & 2) != 0)
          rtf.TableInFrame = true;
        if (this.e.RtfInput >= 2 && this.e.RtfInput != 5 && (rtf.flags1 & 2) == 0 && this.e.RtfCurCellId == 0 && this.e.CurLine > 0 && this.e.text[this.e.CurLine - 1].len > 0 && this.e.text[this.e.CurLine - 1].cid == 0 && (this.e.text[this.e.CurLine - 1].flags & 131) == 0)
        {
          rtf.OutBuf += new string(this.e.ParaChar, 1);
          ++rtf.OutBufLen;
          if (!this.SendRtfText(rtf))
            return 3;
        }
        if (rtf.PastingColumn && rtf.SomeTextRead && this.False(rtf.TableRead))
        {
          rtf.SuspendReading = true;
          this.MessageBeep(0);
          return 0;
        }
        if ((group[groupLevel1].gflags & 524288 /*0x080000*/) == 0 && (rtf.flags1 & 128 /*0x80*/) == 0)
        {
          group[groupLevel1].level = this.e.RtfInitLevel + 1;
          if (!rtf.EmbedTable)
            --group[groupLevel1].level;
          if (rtf.CurTblLevel != group[groupLevel1].level)
            this.SetRtfTblLevel(rtf, groupLevel1, group[groupLevel1].level, rtf.CurTblLevel);
        }
        if (!this.SetRtfRowDefault(rtf, group, groupLevel1))
          return 2;
      }
      else if ((this.RtfCmp(lower, "row") == 0 || this.RtfCmp(lower, "nestrow") == 0) && (rtf.flags & 2) == 0)
      {
        group[groupLevel1].InTable = true;
        if (!rtf.PastingColumn || (this.e.TableRow[this.e.RtfCurRowId].flags & 16384 /*0x4000*/) == 0)
        {
          rtf.OutBuf += new string('\u0012', 1);
          ++rtf.OutBufLen;
          if (!this.SendRtfText(rtf))
            return 3;
        }
        int rtfCurRowId = this.e.RtfCurRowId;
        this.e.TableRow[rtfCurRowId].border = 0;
        if (this.True(group[groupLevel1].BorderWidth[8]))
          this.e.TableRow[rtfCurRowId].border |= 1;
        if (this.True(group[groupLevel1].BorderWidth[9]))
          this.e.TableRow[rtfCurRowId].border |= 2;
        if (this.True(group[groupLevel1].BorderWidth[10]))
          this.e.TableRow[rtfCurRowId].border |= 4;
        if (this.True(group[groupLevel1].BorderWidth[11]))
          this.e.TableRow[rtfCurRowId].border |= 8;
        if ((this.e.TableRow[rtfCurRowId].border & 1) != 0)
          this.e.TableRow[rtfCurRowId].BorderWidth[0] = group[groupLevel1].BorderWidth[8];
        if ((this.e.TableRow[rtfCurRowId].border & 2) != 0)
          this.e.TableRow[rtfCurRowId].BorderWidth[1] = group[groupLevel1].BorderWidth[9];
        if ((this.e.TableRow[rtfCurRowId].border & 4) != 0)
          this.e.TableRow[rtfCurRowId].BorderWidth[2] = group[groupLevel1].BorderWidth[10];
        if ((this.e.TableRow[rtfCurRowId].border & 8) != 0)
          this.e.TableRow[rtfCurRowId].BorderWidth[3] = group[groupLevel1].BorderWidth[11];
        if (!this.CopyRtfRow(rtf, group, groupLevel1))
          return 2;
        tc.ResetUintFlag(ref this.e.TableAux[this.e.RtfCurRowId].flags, 3);
      }
      else if (this.RtfCmp(lower, "intbl") == 0)
      {
        if (this.True(rtf.IntParam))
        {
          group[groupLevel1].InTable = true;
          if ((rtf.flags1 & 128 /*0x80*/) == 0)
          {
            group[groupLevel1].level = this.e.RtfInitLevel + 1;
            if (!rtf.EmbedTable)
              --group[groupLevel1].level;
          }
          if (!rtf.EmbedTable)
            rtf.InitialCell = 0;
        }
        else
          group[groupLevel1].InTable = false;
      }
      else if (this.RtfCmp(lower, "sstrid") == 0)
      {
        if (this.e.RtfCurRowId > 0 && (this.e.TableRow[this.e.RtfCurRowId].flags & 16384 /*0x4000*/) == 0)
        {
          int num = rtf.IntParam;
          for (int index = 1; index < this.e.TotalTableRows; ++index)
          {
            if ((this.e.TableRow[index].flags & 16384 /*0x4000*/) != 0 && this.e.TableRow[index].id == num)
            {
              num = 0;
              break;
            }
          }
          this.e.TableRow[this.e.RtfCurRowId].id = num;
        }
      }
      else if (this.RtfCmp(lower, "trhdr") == 0)
      {
        if (this.e.RtfCurRowId > 0 && (this.e.TableRow[this.e.RtfCurRowId].flags & 16384 /*0x4000*/) == 0)
          this.e.TableRow[this.e.RtfCurRowId].flags |= 4;
      }
      else if (this.RtfCmp(lower, "trkeep") == 0)
      {
        if (this.e.RtfCurRowId > 0 && (this.e.TableRow[this.e.RtfCurRowId].flags & 16384 /*0x4000*/) == 0)
          this.e.TableRow[this.e.RtfCurRowId].flags |= 8192 /*0x2000*/;
      }
      else if (this.RtfCmp(lower, "trgaph") == 0)
      {
        if (this.e.RtfCurRowId > 0 && (this.e.TableRow[this.e.RtfCurRowId].flags & 16384 /*0x4000*/) == 0)
          this.e.TableRow[this.e.RtfCurRowId].CellMargin = rtf.IntParam;
      }
      else if (this.RtfCmp(lower, "trqr") == 0)
      {
        if (this.e.RtfCurRowId > 0 && (this.e.TableRow[this.e.RtfCurRowId].flags & 16384 /*0x4000*/) == 0)
          this.e.TableRow[this.e.RtfCurRowId].flags |= 2;
      }
      else if (this.RtfCmp(lower, "trqc") == 0)
      {
        if (this.e.RtfCurRowId > 0 && (this.e.TableRow[this.e.RtfCurRowId].flags & 16384 /*0x4000*/) == 0)
          this.e.TableRow[this.e.RtfCurRowId].flags |= 1;
      }
      else if (this.RtfCmp(lower, "trleft") == 0)
      {
        if (this.e.RtfCurRowId > 0 && (this.e.TableRow[this.e.RtfCurRowId].flags & 16384 /*0x4000*/) == 0)
        {
          int num = rtf.IntParam;
          if (num < 0 && (this.e.TerFlags4 & 268435456 /*0x10000000*/) == 0)
          {
            this.e.TableRow[this.e.RtfCurRowId].AddedIndent = -num;
            num = 0;
            if (this.e.RtfInput >= 2)
              this.e.TableRow[this.e.RtfCurRowId].AddedIndent = 0;
          }
          this.e.TableRow[this.e.RtfCurRowId].indent = num;
        }
        rtf.PrevCellX = rtf.IntParam;
      }
      else if (this.RtfCmp(lower, "trrh") == 0)
      {
        if (this.e.RtfCurRowId > 0 && (this.e.TableRow[this.e.RtfCurRowId].flags & 16384 /*0x4000*/) == 0)
          this.e.TableRow[this.e.RtfCurRowId].MinHeight = rtf.IntParam;
      }
      else if (this.RtfCmp(lower, "trbrdrt") == 0)
      {
        group[groupLevel1].BorderType = 8;
        group[groupLevel1].BorderWidth[8] = this.ScrToTwipsY(1);
      }
      else if (this.RtfCmp(lower, "trbrdrb") == 0)
      {
        group[groupLevel1].BorderType = 9;
        group[groupLevel1].BorderWidth[9] = this.ScrToTwipsY(1);
      }
      else if (this.RtfCmp(lower, "trbrdrl") == 0)
      {
        group[groupLevel1].BorderType = 10;
        group[groupLevel1].BorderWidth[10] = this.ScrToTwipsX(1);
      }
      else if (this.RtfCmp(lower, "trbrdrr") == 0)
      {
        group[groupLevel1].BorderType = 11;
        group[groupLevel1].BorderWidth[11] = this.ScrToTwipsX(1);
      }
      else if (this.RtfCmp(lower, "rtlrow") == 0)
      {
        rtf.CellFlow = 2;
        if (this.e.RtfCurRowId > 0 && (this.e.TableRow[this.e.RtfCurRowId].flags & 16384 /*0x4000*/) == 0)
          this.e.TableRow[this.e.RtfCurRowId].flow = 2;
      }
      else if (this.RtfCmp(lower, "ltrrow") == 0)
      {
        rtf.CellFlow = 1;
        if (this.e.RtfCurRowId > 0 && (this.e.TableRow[this.e.RtfCurRowId].flags & 16384 /*0x4000*/) == 0)
          this.e.TableRow[this.e.RtfCurRowId].flow = 1;
      }
      else if (this.RtfCmp(lower, "ssdelcell") == 0 || this.RtfCmp(lower, "sswholetablerows") == 0)
      {
        rtf.InitTblCol = -1;
        rtf.PastingColumn = false;
        if (this.RtfCmp(lower, "ssdelcell") == 0)
          rtf.EmbedTable = false;
        int num;
        rtf.InsertAftCell = num = 0;
        rtf.InsertBefCell = num;
      }
      else if (this.RtfCmp(lower, "sstblend") == 0)
      {
        if ((rtf.flags & 4) != 0)
          rtf.flags |= 2;
      }
      else if (this.RtfCmp(lower, "cellx") == 0)
      {
        if (!this.CreateRtfCell(rtf, group, groupLevel1))
          return 2;
      }
      else if (this.RtfCmp(lower, "clmrg") == 0)
        rtf.flags |= 1;
      else if (this.RtfCmp(lower, "clvmrg") == 0)
        rtf.flags1 |= 4096 /*0x1000*/;
      else if ((this.RtfCmp(lower, "cell") == 0 || this.RtfCmp(lower, "nestcell") == 0) && (rtf.flags & 2) == 0)
      {
        if (this.e.RtfCurCellId > 0 && (this.e.CellAux[this.e.RtfCurCellId].flags & 8) != 0)
          this.SetRtfTableInfo(ref group[groupLevel1]);
        int num = this.e.RtfCurCellId;
        if (group[groupLevel1].InTable && this.e.RtfCurRowId <= 0 && rtf.OpenRowId > 0)
        {
          this.e.RtfCurRowId = rtf.OpenRowId;
          this.e.RtfCurCellId = num = rtf.OpenCellId;
          this.e.RtfLastCellX = rtf.OpenLastCellX;
        }
        rtf.OutBuf += new string(this.e.CellChar, 1);
        ++rtf.OutBufLen;
        if (!this.SendRtfText(rtf))
          return 3;
        if (this.e.RtfCurCellId > 0)
          this.e.CellAux[this.e.RtfCurCellId].flags |= 8;
      }
      else if (this.RtfCmp(lower, "clpadt") == 0)
      {
        group[groupLevel1].CellMargin = rtf.IntParam;
        group[groupLevel1].gflags |= 1048576 /*0x100000*/;
      }
      else if (this.RtfCmp(lower, "clvertalt") == 0)
        group[groupLevel1].CellFlags = tc.ResetUintFlag(ref group[groupLevel1].CellFlags, 12288 /*0x3000*/);
      else if (this.RtfCmp(lower, "clvertalc") == 0)
        group[groupLevel1].CellFlags |= 4096 /*0x1000*/;
      else if (this.RtfCmp(lower, "clvertalb") == 0)
        group[groupLevel1].CellFlags |= 8192 /*0x2000*/;
      else if (this.RtfCmp(lower, "ssclvertalbs") == 0)
        group[groupLevel1].CellFlags |= 65536 /*0x010000*/;
      else if (this.RtfCmp(lower, "clbrdrt") == 0)
      {
        group[groupLevel1].BorderType = 4;
        group[groupLevel1].BorderWidth[4] = 0;
      }
      else if (this.RtfCmp(lower, "clbrdrb") == 0)
      {
        group[groupLevel1].BorderType = 5;
        group[groupLevel1].BorderWidth[5] = 0;
      }
      else if (this.RtfCmp(lower, "clbrdrl") == 0)
      {
        group[groupLevel1].BorderType = 6;
        group[groupLevel1].BorderWidth[6] = 0;
      }
      else if (this.RtfCmp(lower, "clbrdrr") == 0)
      {
        group[groupLevel1].BorderType = 7;
        group[groupLevel1].BorderWidth[7] = 0;
      }
      else if (this.RtfCmp(lower, "clshdng") == 0)
        group[groupLevel1].CellShading = rtf.IntParam / 100;
      else if (this.RtfCmp(lower, "clcfpat") == 0 || this.RtfCmp(lower, "clcbpat") == 0)
      {
        if (rtf.IntParam >= 0 && rtf.IntParam < this.e.MaxRtfColors)
        {
          tc.StrRtfColor[] color = this.GetRtfGroup(rtf).color;
          if (this.RtfCmp(lower, "clcfpat") == 0)
          {
            group[groupLevel1].CellPatFC = color[rtf.IntParam].color;
          }
          else
          {
            group[groupLevel1].CellPatBC = color[rtf.IntParam].color;
            if (this.IsSameColor(group[groupLevel1].CellPatBC, tc.CLR_AUTO))
              group[groupLevel1].CellPatBC = tc.CLR_WHITE;
          }
          group[groupLevel1].CellFlags |= 16384 /*0x4000*/;
        }
      }
      else if (this.RtfCmp(lower, "sscolspan") == 0)
        group[groupLevel1].CellColSpan = rtf.IntParam;
      else if (this.RtfCmp(lower, "cltxbtlr") == 0 || this.RtfCmp(lower, "cltxbtrl") == 0)
        group[groupLevel1].TextAngle = 90;
      else if (this.RtfCmp(lower, "cltxtblr") == 0 || this.RtfCmp(lower, "cltxtbrl") == 0)
        group[groupLevel1].TextAngle = 270;
      else if (this.RtfCmp(lower, "brdrdb") == 0)
      {
        int borderType = group[groupLevel1].BorderType;
        group[groupLevel1].BorderWidth[borderType] = 2;
      }
      else if (this.RtfCmp(lower, "brdrw") == 0)
      {
        int borderType = group[groupLevel1].BorderType;
        group[groupLevel1].BorderWidth[borderType] = rtf.IntParam;
      }
      else if (this.RtfCmp(lower, "bkmkstart") == 0)
      {
        switch (this.ReadRtfBookmark(rtf))
        {
          case 1:
            return 1;
          case 2:
            return 2;
        }
      }
      else if (this.RtfCmp(lower, "sstag") == 0)
      {
        switch (this.ReadRtfTag(rtf))
        {
          case 1:
            return 1;
          case 2:
            return 2;
        }
      }
      else if (lower == "replchartag" || lower == "areplchartag" || lower == "mreplchartag" || lower == "chrfmttag")
      {
        switch (this.ReadRtfCustomCharTag(rtf, lower))
        {
          case 1:
            return 1;
          case 2:
            return 2;
        }
      }
      else if (this.RtfCmp(lower, "field") == 0)
      {
        switch (this.ReadRtfField(rtf))
        {
          case 1:
            return 1;
          case 2:
            return 2;
        }
      }
      else if (this.RtfCmp(lower, "fldrslt") == 0)
      {
        if ((group[groupLevel1].gflags & 131072 /*0x020000*/) != 0)
          group[groupLevel1].FieldId = 7;
        else
          this.SkipRtfGroup(rtf);
      }
      else if (this.RtfCmp(lower, "formfield") == 0)
      {
        switch (this.ReadRtfFormField(rtf))
        {
          case 1:
            return 1;
          case 2:
            return 2;
        }
      }
      else if (this.RtfCmp(lower, "footnote") == 0)
      {
        group[groupLevel1].style |= 2048 /*0x0800*/;
        tc.ResetUintFlag(ref group[groupLevel1].style, 4096 /*0x1000*/);
      }
      else if (this.RtfCmp(lower, "ftnalt") == 0)
      {
        group[groupLevel1].style |= 32768 /*0x8000*/;
        if ((rtf.flags2 & 2) != 0)
        {
          --rtf.FootnoteNo;
          ++rtf.EndnoteNo;
          string str;
          this.FmtRtfFootnoteNbr(rtf, out str, rtf.EndnoteNo, this.e.EndnoteNumFmt);
          group[groupLevel1].EndnoteMarker = str[0];
          if (str.Length > 1)
            this.e.FootnoteRest = str.Substring(1);
          else
            this.e.FootnoteRest = "";
          if (rtf.InsLine >= 0 && rtf.InsLine < this.e.TotalLines && this.e.text[rtf.InsLine].len == 1)
            this.e.text[rtf.InsLine].txt[0] = str[0];
          tc.ResetUintFlag(ref rtf.flags2, 2);
        }
      }
      else if (this.RtfCmp(lower, "chftn") == 0)
      {
        if ((group[groupLevel1].style & 2048 /*0x0800*/) == 0)
        {
          ++rtf.FootnoteNo;
          string str;
          this.FmtRtfFootnoteNbr(rtf, out str, rtf.FootnoteNo, this.e.FootnoteNumFmt);
          rtf.OutBuf += str;
          rtf.OutBufLen = rtf.OutBuf.Length;
          this.e.FootnoteRest = "";
          if (!this.SendRtfText(rtf))
            return 3;
          if (this.e.FootnoteRest.Length > 0)
            group[groupLevel1].style |= 4096 /*0x1000*/;
          rtf.flags2 |= 2;
        }
        else
        {
          string str;
          if ((group[groupLevel1].style & 32768 /*0x8000*/) != 0)
            this.FmtRtfFootnoteNbr(rtf, out str, rtf.EndnoteNo, this.e.EndnoteNumFmt);
          else
            this.FmtRtfFootnoteNbr(rtf, out str, rtf.FootnoteNo, this.e.FootnoteNumFmt);
          rtf.OutBuf += str;
          rtf.OutBufLen = rtf.OutBuf.Length;
        }
      }
      else if (this.RtfCmp(lower, "bullet") == 0)
      {
        rtf.OutBuf += new string('\u0095', 1);
        ++rtf.OutBufLen;
      }
      else if (this.RtfCmp(lower, "emdash") == 0)
      {
        rtf.OutBuf += new string('\u0097', 1);
        ++rtf.OutBufLen;
      }
      else if (this.RtfCmp(lower, "endash") == 0)
      {
        rtf.OutBuf += new string('\u0096', 1);
        ++rtf.OutBufLen;
      }
      else if (this.RtfCmp(lower, "lquote") == 0)
      {
        rtf.OutBuf += new string('\u0091', 1);
        ++rtf.OutBufLen;
      }
      else if (this.RtfCmp(lower, "rquote") == 0)
      {
        rtf.OutBuf += new string('\u0092', 1);
        ++rtf.OutBufLen;
      }
      else if (this.RtfCmp(lower, "ldblquote") == 0)
      {
        rtf.OutBuf += new string('\u0093', 1);
        ++rtf.OutBufLen;
      }
      else if (this.RtfCmp(lower, "rdblquote") == 0)
      {
        rtf.OutBuf += new string('\u0094', 1);
        ++rtf.OutBufLen;
      }
      else if (group[rtf.GroupLevel].ControlCount == 1 && rtf.IgnoreText)
      {
        if (this.RtfCmp(lower, "caps") != 0 && this.RtfCmp(lower, "deleted") != 0 && this.RtfCmp(lower, "expnd") != 0 && this.RtfCmp(lower, "expndtw") != 0 && this.RtfCmp(lower, "kerning") != 0 && this.RtfCmp(lower, "outl") != 0 && this.RtfCmp(lower, "revised") != 0 && this.RtfCmp(lower, "revauth") != 0 && this.RtfCmp(lower, "revdttm") != 0 && this.RtfCmp(lower, "scaps") != 0 && this.RtfCmp(lower, "shad") != 0 && this.RtfCmp(lower, "rtlch") != 0 && this.RtfCmp(lower, "ltrch") != 0 && this.RtfCmp(lower, "cchs") != 0 && this.RtfCmp(lower, "lang") != 0 && this.RtfCmp(lower, "widctlpar") != 0 && this.RtfCmp(lower, "background") != 0)
        {
          switch (this.SkipRtfGroup(rtf))
          {
            case 1:
              return 1;
            case 2:
              return 2;
          }
        }
      }
      else if (group[rtf.GroupLevel].ControlCount == 1 && (this.RtfCmp(lower, "pntext") == 0 || this.RtfCmp(lower, "pntextb") == 0))
      {
        switch (this.SkipRtfGroup(rtf))
        {
          case 1:
            return 1;
          case 2:
            return 2;
        }
      }
    }
    return 0;
  }

  internal bool PushRtfChar(tc.ClsRtf rtf)
  {
    if (rtf.StackLen >= 1000)
      return this.PrintError(102, nameof (PushRtfChar));
    rtf.stack[rtf.StackLen] = rtf.CurChar;
    ++rtf.StackLen;
    --rtf.FilePos;
    return true;
  }

  internal int ReadRtfBookmark(tc.ClsRtf rtf)
  {
    int groupLevel = rtf.GroupLevel;
    string str1 = "";
    while (this.GetRtfWord(rtf))
    {
      if (!rtf.GroupBegin)
      {
        if (rtf.GroupEnd)
        {
          if (rtf.GroupLevel < groupLevel)
          {
            string str2 = str1.Trim();
            int tagSlot;
            if (str2.IndexOf("_Toc") != 0 && str2.Length > 0 && -1 != (tagSlot = this.GetTagSlot()))
            {
              this.e.CharTag[tagSlot].InUse = true;
              this.e.CharTag[tagSlot].type = 1;
              this.e.CharTag[tagSlot].name = str2;
              if (this.True(rtf.TagId))
              {
                int tag = rtf.TagId;
                while (this.e.CharTag[tag].next > 0)
                  tag = this.e.CharTag[tag].next;
                this.e.CharTag[tag].next = tagSlot;
                if (this.e.CheckEndlessLoopTags(tag))
                  this.e.CharTag[tag].next = 0;
              }
              else
                rtf.TagId = tagSlot;
            }
            return 0;
          }
        }
        else if (!rtf.IsControlWord && str1.Length + rtf.CurWord.Length < 1000)
          str1 += rtf.CurWord;
      }
    }
    return 1;
  }

  internal int ReadRtfBullet(tc.ClsRtf rtf)
  {
    bool flag1 = false;
    bool flag2 = false;
    int groupLevel1 = rtf.GroupLevel;
    tc.StrRtfGroup strRtfGroup = rtf.group[groupLevel1 - 1];
    tc.StrRtfGroup[] group = rtf.group;
    tc.StrBlt strBlt = new tc.StrBlt();
    strRtfGroup.blt = strBlt.init();
    strRtfGroup.blt.NumberType = 5;
    while (this.GetRtfWord(rtf))
    {
      if (!rtf.GroupBegin)
      {
        if (rtf.GroupEnd)
        {
          if (rtf.GroupLevel < groupLevel1)
          {
            rtf.group[groupLevel1 - 1] = strRtfGroup;
            return 0;
          }
        }
        else if (rtf.IsControlWord)
        {
          strRtfGroup.flags |= 8;
          flag1 = flag2 = false;
          if (this.strcmpi(rtf.CurWord, "pnlvlblt") == 0 || this.strcmpi(rtf.CurWord, "pnlvl") == 0 && rtf.IntParam == 11)
            strRtfGroup.blt.IsBullet = true;
          else if (this.strcmpi(rtf.CurWord, "pnlvl") == 0)
            strRtfGroup.blt.level = rtf.IntParam;
          else if (this.strcmpi(rtf.CurWord, "pnlvlcont") == 0)
            strRtfGroup.blt.flags |= 1;
          else if (this.strcmpi(rtf.CurWord, "pnstart") == 0)
          {
            strRtfGroup.blt.start = rtf.IntParam;
          }
          else
          {
            this.strcmpi(rtf.CurWord, "pnindent");
            if (this.strcmpi(rtf.CurWord, "pnf") == 0)
            {
              tc.StrRtfGroup rtfGroup = this.GetRtfGroup(rtf);
              int intParam = rtf.IntParam;
              int index;
              if (intParam >= 0 && intParam < 500)
              {
                index = intParam;
              }
              else
              {
                index = 500;
                while (index < rtfGroup.MaxRtfFonts && (!rtfGroup.font[index].InUse || rtfGroup.font[index].FontId != intParam))
                  ++index;
              }
              if (strRtfGroup.blt.IsBullet)
                strRtfGroup.blt.font = this.strcmpi(rtfGroup.font[index].name, "symbol") != 0 ? (this.strcmpi(rtfGroup.font[index].name, "wingdings") != 0 ? 0 : 2) : 1;
              int groupLevel2 = rtf.GroupLevel;
              group[groupLevel2].TypeFace = rtfGroup.font[index].name;
              group[groupLevel2].FontFamily = rtfGroup.font[index].family;
              group[groupLevel2].CharSet = rtfGroup.font[index].CharSet;
            }
            else if (this.strcmpi(rtf.CurWord, "pnfs") == 0)
            {
              int groupLevel3 = rtf.GroupLevel;
              group[groupLevel3].PointSize2 = rtf.IntParam;
            }
            else if (this.strcmpi(rtf.CurWord, "pnb") == 0)
            {
              int groupLevel4 = rtf.GroupLevel;
              if (this.True(rtf.IntParam))
                group[groupLevel4].style |= 2;
              else
                group[groupLevel4].style &= -3;
            }
            else if (this.strcmpi(rtf.CurWord, "pni") == 0)
            {
              int groupLevel5 = rtf.GroupLevel;
              if (this.True(rtf.IntParam))
                group[groupLevel5].style |= 4;
              else
                group[groupLevel5].style &= -5;
            }
            else if (this.strcmpi(rtf.CurWord, "pndec") == 0 && !strRtfGroup.blt.IsBullet)
              strRtfGroup.blt.NumberType = 0;
            else if (this.strcmpi(rtf.CurWord, "pnucltr") == 0 && !strRtfGroup.blt.IsBullet)
              strRtfGroup.blt.NumberType = 1;
            else if (this.strcmpi(rtf.CurWord, "pnlcltr") == 0 && !strRtfGroup.blt.IsBullet)
              strRtfGroup.blt.NumberType = 2;
            else if (this.strcmpi(rtf.CurWord, "pnucrm") == 0 && !strRtfGroup.blt.IsBullet)
              strRtfGroup.blt.NumberType = 3;
            else if (this.strcmpi(rtf.CurWord, "pnlcrm") == 0 && !strRtfGroup.blt.IsBullet)
              strRtfGroup.blt.NumberType = 4;
            else if (this.strcmpi(rtf.CurWord, "pntxtb") == 0)
              flag1 = true;
            else if (this.strcmpi(rtf.CurWord, "pntxta") == 0)
              flag2 = true;
          }
        }
        else if (flag1)
        {
          if (strRtfGroup.blt.IsBullet)
          {
            strRtfGroup.blt.BulletChar = rtf.CurWord[0];
          }
          else
          {
            strRtfGroup.blt.BefText = rtf.CurWord;
            if (strRtfGroup.blt.BefText.Length > 13)
              strRtfGroup.blt.BefText = strRtfGroup.blt.BefText.Substring(0, 13);
          }
        }
        else if (flag2 && !strRtfGroup.blt.IsBullet)
          strRtfGroup.blt.AftChar = rtf.CurWord[0];
      }
    }
    return 1;
  }

  internal int ReadRtfColorTable(tc.ClsRtf rtf)
  {
    int index1 = 0;
    tc.StrRtfGroup[] group = rtf.group;
    int RtfGroup;
    this.GetRtfGroup(rtf, out RtfGroup);
    tc.StrRtfColor[] OldObj = group[RtfGroup].color;
    for (int index2 = 0; index2 < this.e.MaxRtfColors; ++index2)
    {
      OldObj[index2].color = this.e.TerFont[0].TextColor;
      if (OldObj[index2].color == Color.Black)
        OldObj[index2].color = tc.CLR_AUTO;
    }
    int groupLevel = rtf.GroupLevel;
    rtf.IgnoreCrLfInControlWord = true;
    while (this.GetRtfWord(rtf))
    {
      if (!rtf.GroupBegin)
      {
        if (rtf.GroupEnd)
        {
          if (rtf.GroupLevel < groupLevel)
          {
            rtf.IgnoreCrLfInControlWord = false;
            return 0;
          }
        }
        else if (rtf.IsControlWord)
        {
          int red = (int) OldObj[index1].color.R;
          int green = (int) OldObj[index1].color.G;
          int blue = (int) OldObj[index1].color.B;
          if (this.strcmpi(rtf.CurWord, "red") == 0)
            red = (int) (byte) rtf.IntParam;
          else if (this.strcmpi(rtf.CurWord, "green") == 0)
            green = (int) (byte) rtf.IntParam;
          else if (this.strcmpi(rtf.CurWord, "blue") == 0)
            blue = (int) (byte) rtf.IntParam;
          OldObj[index1].color = this.ToColor(red, green, blue);
        }
        else if (this.strcmpi(rtf.CurWord, ";") == 0)
        {
          ++index1;
          if (index1 >= this.e.MaxRtfColors)
          {
            int num = 50;
            OldObj = group[RtfGroup].color = this.ReAlloc(OldObj, this.e.MaxRtfColors + num);
            this.e.MaxRtfColors += num;
          }
        }
      }
    }
    return 1;
  }

  internal int ReadRtfField(tc.ClsRtf rtf)
  {
    string str1 = "";
    string txt = "";
    char ch = '"';
    char minValue = char.MinValue;
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    bool flag4 = false;
    bool flag5 = false;
    bool flag6 = false;
    bool flag7 = false;
    bool flag8 = false;
    bool flag9 = false;
    int num1 = 0;
    Color color1 = Color.Black;
    Color color2 = Color.White;
    int groupLevel1 = rtf.GroupLevel;
    bool flag10 = (rtf.group[groupLevel1].gflags & 8192 /*0x2000*/) != 0;
    rtf.group[groupLevel1].gflags |= 8192 /*0x2000*/;
    rtf.group[groupLevel1].FieldGroup = groupLevel1;
    if (this.True(rtf.FieldCode) && !flag10 && rtf.InitFieldId != 2)
      rtf.FieldCode = (string) null;
    while (this.GetRtfWord(rtf))
    {
      if (!rtf.GroupBegin)
      {
        if (rtf.GroupEnd)
        {
          if (rtf.GroupLevel >= groupLevel1)
            continue;
        }
        else if (this.strcmpi(rtf.CurWord, "fldinst") != 0 || !rtf.IsControlWord)
        {
          if (this.strcmpi(rtf.CurWord, "fldrslt") == 0 && rtf.IsControlWord)
          {
            if (!flag1)
              flag6 = false;
            if (flag6)
            {
              int groupLevel2 = rtf.GroupLevel;
              rtf.group[groupLevel2].gflags |= 32768 /*0x8000*/;
            }
            else if ((flag1 || !flag7) && !flag10)
            {
              this.SkipRtfGroup(rtf);
              continue;
            }
          }
          else
            continue;
        }
        else
        {
          if (rtf.InitFieldId > 0)
          {
            this.SkipRtfGroup(rtf);
            flag7 = true;
            continue;
          }
          int groupLevel3 = rtf.GroupLevel;
          while (!rtf.GroupEnd || rtf.GroupLevel >= groupLevel3)
          {
            if (!this.GetRtfWord(rtf))
              return 1;
            if (!rtf.GroupBegin)
            {
              if (rtf.GroupEnd)
              {
                if (rtf.GroupLevel < groupLevel3)
                  break;
              }
              else if (!flag5)
              {
                string curWord = rtf.CurWord;
                if (rtf.IsControlWord)
                {
                  if (flag9 && curWord == "u")
                  {
                    char intParam = (char) rtf.IntParam;
                    rtf.FieldCode += new string(intParam, 1);
                    rtf.group[rtf.GroupLevel].IgnoreCount = rtf.group[rtf.GroupLevel].UcIgnoreCount;
                  }
                  else
                  {
                    int num2 = this.ProcessRtfControl(rtf);
                    if (num2 != 0)
                      return num2;
                    if (this.strcmpi(curWord, "formfield") == 0)
                      flag1 = true;
                  }
                }
                else
                {
                  string str2 = curWord.Trim();
                  if (str2.Length != 0)
                  {
                    if (flag10 && this.strcmpi(str2, "INCLUDEPICTURE") != 0)
                      flag5 = true;
                    else if (flag9)
                    {
                      if (rtf.group[rtf.GroupLevel].IgnoreCount > 0)
                        --rtf.group[rtf.GroupLevel].IgnoreCount;
                      else
                        rtf.FieldCode += str2;
                    }
                    else
                    {
                      flag7 = true;
                      if (this.strcmpi(str2, "SYMBOL") == 0 && !flag3)
                      {
                        if (!this.GetRtfWord(rtf))
                          return 1;
                        string str3 = rtf.CurWord.Trim();
                        if (!rtf.IsControlWord)
                        {
                          if (str3 == "183" && rtf.group[rtf.GroupLevel].CharSet < 128 /*0x80*/)
                          {
                            rtf.OutBuf += new string('\u0095', 1);
                            ++rtf.OutBufLen;
                            flag1 = true;
                          }
                          else
                          {
                            flag2 = true;
                            flag3 = flag4 = false;
                            str1 = txt = "";
                            int length = str3.Length;
                            int index = 0;
                            while (index < length && str3[index] >= '0' && str3[index] <= '9')
                              ++index;
                            if (index == length)
                            {
                              int groupLevel4 = rtf.GroupLevel;
                              if (rtf.OutBufLen > 0 && !this.SendRtfText(rtf))
                                return 1;
                              this.e.TempString = str3;
                              minValue = (char) this.ToInt(this.e.TempString);
                              rtf.group[groupLevel4].TypeFace = "Symbol";
                              rtf.group[groupLevel4].FontFamily = "";
                              rtf.group[groupLevel4].CharType = 0;
                              num1 = rtf.group[groupLevel4].style;
                              color1 = rtf.group[groupLevel4].TextColor;
                              color2 = rtf.group[groupLevel4].TextBkColor;
                              flag1 = true;
                            }
                          }
                        }
                      }
                      else if (flag2)
                      {
                        if (str2 == "\\")
                          flag3 = flag4 = false;
                        if (flag3)
                          str1 += rtf.CurWord;
                        if (flag4)
                          txt += rtf.CurWord;
                        if (this.strcmpi(str2, "f") == 0)
                          flag3 = true;
                        if (this.strcmpi(str2, "s") == 0)
                          flag4 = true;
                      }
                      else if (this.strcmpi(str2, "s") == 0 & flag2)
                      {
                        flag3 = false;
                        flag4 = true;
                      }
                      else if (this.strcmpi(str2, "PAGE") == 0 && !this.e.HtmlMode)
                      {
                        int groupLevel5 = rtf.GroupLevel;
                        if (rtf.OutBufLen > 0 && !this.SendRtfText(rtf))
                          return 1;
                        rtf.OutBuf = "1";
                        rtf.OutBufLen = 1;
                        rtf.group[groupLevel5].FieldId = 1;
                        if (!this.SendRtfText(rtf))
                          return 1;
                        flag1 = true;
                      }
                      else if ((this.strcmpi(str2, "NUMPAGES") == 0 || this.strcmpi(str2, "SECTIONPAGES") == 0) && !this.e.HtmlMode)
                      {
                        int groupLevel6 = rtf.GroupLevel;
                        if (rtf.OutBufLen > 0 && !this.SendRtfText(rtf))
                          return 1;
                        rtf.OutBuf = "1";
                        rtf.OutBufLen = 1;
                        rtf.group[groupLevel6].FieldId = this.strcmpi(str2, "NUMPAGES") == 0 ? 5 : 17;
                        if (!this.SendRtfText(rtf))
                          return 1;
                        flag1 = true;
                      }
                      else if ((this.strcmpi(str2, "TIME") == 0 || this.strcmpi(str2, "DATE") == 0 || this.strcmpi(str2, "PRINTDATE") == 0) && !this.e.HtmlMode)
                      {
                        string str4 = "";
                        int groupLevel7 = rtf.GroupLevel;
                        int num3 = this.strcmpi(str2, "PRINTDATE") == 0 ? 10 : 8;
                        while (this.GetRtfWord(rtf))
                        {
                          if (!rtf.GroupBegin)
                          {
                            if (rtf.GroupEnd)
                            {
                              if (rtf.GroupLevel < groupLevel7)
                                break;
                            }
                            else if (!rtf.IsControlWord)
                              str4 += rtf.CurWord;
                          }
                        }
                        string DateString = str4.Trim();
                        int length = DateString.Length;
                        int index1 = 0;
                        while (index1 < length && DateString[index1] != '"')
                          ++index1;
                        string pDateFmt;
                        if (index1 < length)
                        {
                          int startIndex = index1 + 1;
                          int index2 = startIndex;
                          while (index2 < length && DateString[index2] != '"')
                            ++index2;
                          pDateFmt = DateString.Substring(startIndex, index2 - startIndex);
                        }
                        else
                          pDateFmt = "";
                        rtf.FieldCode = pDateFmt;
                        if (num3 == 8)
                        {
                          this.GetDateString(pDateFmt, out DateString, -1);
                          if (rtf.OutBufLen > 0 && !this.SendRtfText(rtf))
                            return 1;
                          rtf.OutBuf = DateString;
                          rtf.OutBufLen = rtf.OutBuf.Length;
                          ++rtf.GroupLevel;
                          rtf.group[rtf.GroupLevel].FieldId = num3;
                          if (!this.SendRtfText(rtf))
                            return 1;
                          --rtf.GroupLevel;
                          flag1 = true;
                        }
                        else
                        {
                          for (int index3 = groupLevel1; index3 <= rtf.GroupLevel; ++index3)
                            rtf.group[index3].FieldId = num3;
                        }
                        if (rtf.GroupLevel < groupLevel3)
                          break;
                      }
                      else if (this.strcmpi(str2, "TOC") == 0 && !this.e.HtmlMode || this.strcmpi(str2, "LISTNUM") == 0 || this.strcmpi(str2, "AUTONUMLGL") == 0 || this.strcmpi(str2, "PAGEREF") == 0 || this.strcmpi(str2, "HYPERLINK") == 0)
                      {
                        bool flag11 = this.strcmpi(str2, "TOC") == 0;
                        bool flag12 = this.strcmpi(str2, "LISTNUM") == 0;
                        bool flag13 = this.strcmpi(str2, "AUTONUMLGL") == 0;
                        bool flag14 = this.strcmpi(str2, "HYPERLINK") == 0;
                        bool flag15 = this.strcmpi(str2, "PAGEREF") == 0;
                        int style = rtf.group[rtf.GroupLevel].style;
                        string str5;
                        this.ReadRtfGroupText(rtf, out str5);
                        rtf.FieldCode = str5;
                        flag9 = true;
                        for (int index = groupLevel1; index <= rtf.GroupLevel; ++index)
                        {
                          if (flag11)
                            rtf.group[index].FieldId = 9;
                          if (flag12)
                            rtf.group[index].FieldId = 11;
                          if (flag13)
                            rtf.group[index].FieldId = 12;
                          if (flag14)
                            rtf.group[index].FieldId = 14;
                          if (flag15)
                            rtf.group[index].FieldId = 16 /*0x10*/;
                          rtf.group[index].style = style;
                        }
                        if (rtf.GroupLevel < groupLevel3)
                          break;
                      }
                      else if (this.strcmpi(str2, "EQ") == 0)
                      {
                        this.e.RtfInEquation = true;
                        flag1 = true;
                      }
                      else if (this.strcmpi(str2, "INCLUDEPICTURE") == 0)
                      {
                        this.ReadRtfGroupText(rtf, out str2);
                        int num4 = str2.IndexOf('"') >= 0 ? 1 : 0;
                        this.StripSlashes(this.ExtractQuotedText(str2), out str2);
                        int length = str2.IndexOf(" ");
                        if (num4 == 0 && length >= 0)
                          str2 = str2.Substring(0, length);
                        if (this.ReadRtfLinkedPicture(rtf, str2) > 0)
                          flag1 = true;
                      }
                      else if (this.strcmpi(str2, "SET") == 0 && !flag8)
                      {
                        flag5 = true;
                        flag1 = true;
                      }
                      else if (this.strcmpi(str2, "ASK") == 0 && !flag8)
                      {
                        flag5 = true;
                        flag1 = true;
                      }
                      else if (this.strcmpi(str2, "include") == 0 && !flag8)
                        flag5 = true;
                      else if (this.strcmpi(str2, "shape") == 0 && !flag8)
                      {
                        flag5 = true;
                      }
                      else
                      {
                        if (this.e.RtfInEquation && this.strcmpi(str2, "X(") == 0)
                        {
                          int groupLevel8 = rtf.GroupLevel;
                          if (rtf.OutBufLen > 0 && !this.SendRtfText(rtf))
                            return 1;
                          rtf.group[groupLevel8].style |= 8192 /*0x2000*/;
                          break;
                        }
                        if (this.strcmpi(str2, "FORMTEXT") == 0)
                          flag6 = true;
                        else if (this.strcmpi(str2, "FORMCHECKBOX") != 0 && this.strcmpi(str2, "FORMDROPDOWN") != 0)
                        {
                          if (this.strcmpi(str2, "MERGEFIELD") == 0)
                            flag8 = true;
                          else if (!(this.strcmpi(str2, "MERGEFIELD") == 0 | flag1) && !this.e.HtmlMode)
                          {
                            string str6 = "";
                            int ignoreCount = rtf.group[rtf.GroupLevel].IgnoreCount;
                            if (ignoreCount > 0)
                            {
                              str6 = rtf.OutBuf;
                              rtf.OutBuf = "";
                              rtf.OutBufLen = 0;
                              rtf.group[rtf.GroupLevel].IgnoreCount = 0;
                            }
                            if (rtf.OutBufLen > 0 && !this.SendRtfText(rtf))
                              return 1;
                            for (int index = groupLevel3; index <= rtf.GroupLevel; ++index)
                              rtf.group[index].FieldId = 6;
                            int style = rtf.group[rtf.GroupLevel].style;
                            rtf.group[rtf.GroupLevel].style |= 512 /*0x0200*/;
                            rtf.OutBuf = "{";
                            rtf.OutBufLen = rtf.OutBuf.Length;
                            if (!this.SendRtfText(rtf))
                              return 1;
                            rtf.group[rtf.GroupLevel].style = style;
                            for (int index = groupLevel1; index <= rtf.GroupLevel; ++index)
                              rtf.group[index].gflags |= 131072 /*0x020000*/;
                            if (ignoreCount > 0)
                            {
                              rtf.OutBuf = str6;
                              rtf.OutBufLen = str6.Length;
                              rtf.group[rtf.GroupLevel].IgnoreCount = ignoreCount;
                            }
                            this.CopyToOutBuf(rtf);
                            return 0;
                          }
                        }
                      }
                    }
                  }
                }
              }
            }
          }
          if (flag2)
          {
            int groupLevel9 = rtf.GroupLevel;
            str1 = str1.Trim();
            txt = txt.Trim();
            if (str1.Length > 0)
            {
              tc.StrRtfGroup rtfGroup = this.GetRtfGroup(rtf);
              if ((int) str1[0] == (int) ch)
                str1 = str1.Substring(1, str1.Length - 2).Trim();
              int length = str1.Length;
              if (length > 0 && (int) str1[length - 1] == (int) ch)
                str1 = str1.Substring(0, length - 1);
              int index;
              for (index = 0; index < rtfGroup.MaxRtfFonts; ++index)
              {
                if (rtfGroup.font[index].InUse)
                {
                  if (this.strcmpi(rtfGroup.font[index].name, str1) != 0)
                  {
                    if (this.strcmpi(rtfGroup.font[index].name2, str1) == 0)
                    {
                      str1 = rtfGroup.font[index].name;
                      break;
                    }
                  }
                  else
                    break;
                }
              }
              rtf.group[groupLevel9].TypeFace = str1;
              if (index < rtfGroup.MaxRtfFonts)
                rtf.group[groupLevel9].CharSet = rtfGroup.font[index].CharSet;
              if (this.strcmpi(str1, "Wingdings") == 0)
                rtf.group[groupLevel9].CharSet = 2;
              if (this.strcmpi(str1, "Monotype Sorts") == 0)
                rtf.group[groupLevel9].CharSet = 2;
              if (rtf.group[groupLevel9].CharSet == 2)
                rtf.group[groupLevel9].CharType = 0;
            }
            if (txt.Length > 0)
            {
              int num5 = this.ToInt(txt);
              if (num5 > 0)
                rtf.group[groupLevel9].PointSize2 = num5 * 2;
            }
            rtf.group[groupLevel9].style = num1;
            rtf.group[groupLevel9].TextColor = color1;
            rtf.group[groupLevel9].TextBkColor = color2;
            rtf.OutBuf = new string(minValue, 1);
            rtf.OutBufLen = 1;
            if (!this.SendRtfText(rtf))
              return 1;
          }
          if (!this.e.RtfInEquation)
            continue;
        }
        return 0;
      }
    }
    return 1;
  }

  internal int ReadRtfFontTable(tc.ClsRtf rtf)
  {
    tc.StrRtfGroup[] group = rtf.group;
    int groupLevel1 = rtf.GroupLevel;
    int RtfGroup;
    this.GetRtfGroup(rtf, out RtfGroup);
    tc.StrRtfFont[] font = group[RtfGroup].font;
    while (this.GetRtfWord(rtf))
    {
      if (!rtf.GroupBegin)
      {
        if (rtf.GroupEnd)
        {
          if (rtf.GroupLevel < groupLevel1)
          {
            this.GetRtfDefaultFont(rtf);
            if (this.e.RtfInput < 2 && (group[rtf.GroupLevel].TypeFace != this.e.TerFont[0].TypeFace || group[rtf.GroupLevel].PointSize2 != this.e.TerFont[0].TwipsSize / 10 || group[rtf.GroupLevel].style != this.e.TerFont[0].style))
              this.e.SetTerDefaultFont(group[rtf.GroupLevel].TypeFace, group[rtf.GroupLevel].PointSize2 / 2, group[rtf.GroupLevel].style, this.e.TerFont[0].TextColor, false);
            return 0;
          }
          continue;
        }
        if (rtf.GroupLevel != groupLevel1 || rtf.IsControlWord)
        {
          if (this.strcmpi(rtf.CurWord, "f") != 0)
          {
            if (!rtf.IsControlWord)
              return 2;
            int num = this.ProcessRtfControl(rtf);
            if (num != 0)
              return num;
            continue;
          }
          int intParam1 = rtf.IntParam;
          int index;
          if (intParam1 >= 0 && intParam1 < 500)
            index = intParam1;
          else if (group[RtfGroup].MaxRtfFonts < 500)
          {
            index = 500;
          }
          else
          {
            index = 500;
            while (index < group[RtfGroup].MaxRtfFonts && !this.False(font[index].InUse))
              ++index;
          }
          if (index < 0 || index >= 5000)
            return 2;
          if (index >= group[RtfGroup].MaxRtfFonts)
          {
            int count = index + 50;
            if (count >= 5000)
              count = 4999;
            if (count < index + 1)
              count = index + 1;
            group[RtfGroup].font = this.ReAlloc(group[RtfGroup].font, count);
            for (int maxRtfFonts = group[RtfGroup].MaxRtfFonts; maxRtfFonts < count; ++maxRtfFonts)
            {
              tc.StrRtfFont strRtfFont = new tc.StrRtfFont();
              group[RtfGroup].font[maxRtfFonts] = strRtfFont.init();
            }
            group[RtfGroup].MaxRtfFonts = count;
            font = group[RtfGroup].font;
          }
          font[index].InUse = true;
          font[index].FontId = intParam1;
          font[index].CharSet = 1;
          font[index].family = "";
          font[index].name = "";
          bool flag1 = false;
          if (this.GetRtfWord(rtf))
          {
            int intParam2;
            if (rtf.IsControlWord)
            {
              if (this.strcmpi(rtf.CurWord, "fcharset") == 0)
              {
                font[index].CharSet = intParam2 = rtf.IntParam;
              }
              else
              {
                bool flag2 = true;
                if (rtf.WordLen <= 1)
                  return 2;
                if (rtf.CurWord[0] != 'f' && rtf.CurWord[0] != 'F')
                  flag2 = false;
                if (rtf.CurWord.Length > 29)
                  rtf.CurWord = rtf.CurWord.Substring(0, 29);
                font[index].family = rtf.CurWord;
                if (flag2)
                  font[index].family = font[index].family.Substring(1);
                else if (font[index].family.Length == 0)
                  font[index].family = rtf.CurWord;
                font[index].family = font[index].family.Trim();
              }
            }
            else
              flag1 = true;
            int groupLevel2 = rtf.GroupLevel;
            bool flag3 = true;
            while (flag1 || this.GetRtfWord(rtf))
            {
              flag1 = false;
              if (rtf.GroupEnd && rtf.GroupLevel < groupLevel2)
              {
                font[index].name = font[index].name.Trim();
                goto label_1;
              }
              if (rtf.WordLen != 0)
              {
                if (rtf.IsControlWord)
                {
                  if (this.strcmpi(rtf.CurWord, "falt") == 0)
                  {
                    bool exists = false;
                    string lower = font[index].name.ToLower();
                    int charSet = (int) this.GetCharSet(this.e.TerGr, font[index].name, ref exists);
                    if (exists || lower == "arial" || lower == "courier" || lower == "courier new" || lower == "times new roman")
                    {
                      if (rtf.GroupLevel > groupLevel2)
                        this.SkipRtfGroup(rtf);
                    }
                    else
                    {
                      font[index].name2 = font[index].name;
                      font[index].name = "";
                      flag3 = true;
                    }
                  }
                  else if (this.strcmpi(rtf.CurWord, "fcharset") == 0)
                    font[index].CharSet = intParam2 = rtf.IntParam;
                  else if (rtf.GroupLevel > groupLevel2)
                    this.SkipRtfGroup(rtf);
                }
                else
                {
                  if (flag3)
                    font[index].name += rtf.CurWord;
                  int length1 = rtf.CurWord.Length;
                  if (length1 > 0 && rtf.CurWord[length1 - 1] == ';')
                  {
                    int length2 = font[index].name.Length;
                    if (flag3 && length2 > 0)
                    {
                      bool exists = false;
                      font[index].name = font[index].name.Substring(0, length2 - 1);
                      font[index].name = font[index].name.Trim();
                      int charSet = (int) this.GetCharSet(this.e.TerGr, font[index].name, ref exists);
                      if (!exists)
                      {
                        string str1 = "Western,Greek,Cyr,CE,(Hebrew),(Arabic),(Baltic)";
                        int num = font[index].name.Length - 1;
                        while (num >= 0 && font[index].name[num] != ' ')
                          --num;
                        if (num > 0)
                        {
                          string str2 = font[index].name.Substring(num + 1);
                          if (str1.IndexOf(str2) >= 0)
                          {
                            font[index].name = font[index].name.Substring(0, num);
                            font[index].name = font[index].name.Trim();
                          }
                        }
                      }
                      if (font[index].name.ToUpper().IndexOf("WINGDINGS") == 0)
                        font[index].CharSet = 2;
                      if (font[index].name.Length == 0)
                      {
                        font[index].name = font[0].name;
                        font[index].family = font[0].family;
                        font[index].CharSet = font[0].CharSet;
                      }
                    }
                    if (rtf.GroupLevel != groupLevel2)
                      flag3 = false;
                    else
                      goto label_1;
                  }
                }
              }
            }
          }
          return 1;
        }
        continue;
      }
      continue;
label_1:;
    }
    return 1;
  }

  internal int ReadRtfFormField(tc.ClsRtf rtf)
  {
    int num1 = 0;
    bool flag = false;
    tc.StrRtfGroup[] group = rtf.group;
    if (rtf.group[rtf.GroupLevel].TypeFace.Length == 0)
      this.GetRtfDefaultFont(rtf);
    int groupLevel = rtf.GroupLevel;
    tc.StrRtfPict pic = new tc.StrRtfPict();
    pic.type = 6;
    pic.FormId = -1;
    pic.width = this.e.TerFont[0].TwipsSize / 10;
    pic.height = this.e.TerFont[0].TwipsSize / 10;
    tc.ClsForm clsForm = new tc.ClsForm();
    clsForm.FontId = -1;
    clsForm.TextBkColor = this.e.StatusBkColor;
    clsForm.name = "";
    while (this.GetRtfWord(rtf))
    {
      if (flag && rtf.GroupLevel < num1)
        flag = false;
      if (!rtf.GroupBegin)
      {
        if (rtf.GroupEnd)
        {
          if (rtf.GroupLevel < groupLevel)
          {
            if (pic.FormId >= 0)
            {
              pic.form = clsForm;
              if (pic.FormId == 2)
              {
                int fieldGroup = group[rtf.GroupLevel].FieldGroup;
                group[fieldGroup].FieldId = 2;
                rtf.FieldCode = $"{clsForm.MaxLen.ToString()}|{clsForm.name}";
                int num2 = 70000;
                for (int index = 0; index < this.e.TotalFonts; ++index)
                {
                  if (this.e.TerFont[index].FieldId == 2 && this.e.TerFont[index].AuxId >= num2)
                    num2 = this.e.TerFont[index].AuxId + 1;
                }
                group[fieldGroup].AuxId = num2;
              }
              else
                this.ImportRtfData(6, ref rtf.group[rtf.GroupLevel], (object) null, pic, tc.SkipRtfObject);
            }
            return 0;
          }
        }
        else if (rtf.IsControlWord)
        {
          if (this.strcmpi(rtf.CurWord, "fftype") == 0)
          {
            if (rtf.IntParam == 0)
              pic.FormId = 2;
            else if (rtf.IntParam == 1)
            {
              pic.FormId = 3;
              clsForm.CtlClass = "CheckBox";
            }
            else if (rtf.IntParam == 2)
            {
              pic.FormId = 4;
              clsForm.CtlClass = "ComboBox";
            }
          }
          else if (this.strcmpi(rtf.CurWord, "ssfldw") == 0)
          {
            pic.width = rtf.IntParam;
            clsForm.flags |= 2;
          }
          else if (this.strcmpi(rtf.CurWord, "ffhps") == 0)
          {
            int num3;
            pic.width = num3 = rtf.IntParam * 10;
            pic.height = num3;
            pic.OrigWidth = pic.width;
            pic.OrigHeight = pic.height;
          }
          else if (this.strcmpi(rtf.CurWord, "ffmaxlen") == 0)
            clsForm.MaxLen = rtf.IntParam;
          else if (this.strcmpi(rtf.CurWord, "ssffborder") != 0)
          {
            if (this.strcmpi(rtf.CurWord, "ffres") == 0 || this.strcmpi(rtf.CurWord, "ffdefres") == 0)
            {
              clsForm.InitNum = rtf.IntParam;
              if (pic.FormId == 3 && clsForm.InitNum > 1)
                clsForm.InitNum = 0;
            }
            else if (this.strcmpi(rtf.CurWord, "ffname") == 0)
            {
              flag = true;
              num1 = rtf.GroupLevel;
            }
          }
        }
        else if (flag)
          clsForm.name += rtf.CurWord;
      }
    }
    return 1;
  }

  internal bool ReadRtfGroupText(tc.ClsRtf rtf, out string str)
  {
    int groupLevel = rtf.GroupLevel;
    str = "";
    while (this.GetRtfWord(rtf))
    {
      if (!rtf.GroupBegin)
      {
        if (rtf.GroupEnd)
        {
          if (rtf.GroupLevel < groupLevel)
            break;
        }
        else if (!rtf.IsControlWord)
          str += rtf.CurWord;
      }
    }
    str = str.Trim();
    return true;
  }

  internal int ReadRtfInfo(tc.ClsRtf rtf)
  {
    int groupLevel1 = rtf.GroupLevel;
    while (this.GetRtfWord(rtf))
    {
      if (!rtf.GroupBegin)
      {
        if (rtf.GroupEnd)
        {
          if (rtf.GroupLevel < groupLevel1)
            return 0;
          continue;
        }
        if (rtf.IsControlWord)
        {
          int index1 = 0;
          while (index1 < 11 && this.strcmpi(this.e.RtfInfo[index1], rtf.CurWord) != 0)
            ++index1;
          if (index1 != 11)
          {
            int index2 = index1;
            int groupLevel2 = rtf.GroupLevel;
            this.e.pRtfInfo[index2] = (string) null;
            int num1 = 0;
            while (this.GetRtfWord(rtf))
            {
              if (!rtf.GroupBegin)
              {
                if (rtf.GroupEnd)
                {
                  if (rtf.GroupLevel < groupLevel2)
                    goto label_1;
                }
                else if (this.False(rtf.IsControlWord))
                {
                  int num2 = num1 + rtf.CurWord.Length;
                  string[] pRtfInfo;
                  IntPtr index3;
                  (pRtfInfo = this.e.pRtfInfo)[(int) (index3 = (IntPtr) index2)] = pRtfInfo[(int) index3] + rtf.CurWord;
                  num1 = num2;
                }
              }
            }
            break;
          }
          continue;
        }
        continue;
      }
      continue;
label_1:;
    }
    return 1;
  }

  internal int ReadRtfLinkedPicture(tc.ClsRtf rtf, string PictFile)
  {
    tc.StrRtfGroup[] group = rtf.group;
    int groupLevel = rtf.GroupLevel;
    PictFile = this.ResolveLinkFileName(PictFile);
    if (!File.Exists(PictFile))
      return -1;
    int pict = this.e.TerInsertPictureFile(PictFile, false, 0, false);
    if (pict <= 0)
      return -1;
    this.e.TerFont[pict].style |= group[groupLevel].style;
    if (this.True(group[groupLevel].FieldId))
    {
      this.e.TerFont[pict].FieldId = group[groupLevel].FieldId;
      if (this.True(rtf.FieldCode))
        this.e.TerFont[pict].FieldCode = rtf.FieldCode;
    }
    this.ImportRtfData(6, ref rtf.group[rtf.GroupLevel], (object) null, new tc.StrRtfPict()
    {
      PictId = pict
    }, tc.SkipRtfObject);
    if (group[groupLevel].LinkPictWidth > 0 || group[groupLevel].LinkPictHeight > 0)
      this.e.TerSetPictSize(pict, this.TwipsToScrX(group[groupLevel].LinkPictWidth), this.TwipsToScrY(group[groupLevel].LinkPictHeight));
    return pict;
  }

  internal int ReadRtfList(tc.ClsRtf rtf)
  {
    int ListId = -1;
    int index1 = 0;
    int length1 = 0;
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    int num1 = 0;
    int num2 = 0;
    int index2 = 0;
    int num3 = 0;
    tc.StrList strList = new tc.StrList();
    tc.StrListLevel strListLevel1 = new tc.StrListLevel().init();
    int groupLevel = rtf.GroupLevel;
    rtf.group[groupLevel].gflags |= 16777216 /*0x01000000*/;
    while (this.GetRtfWord(rtf))
    {
      if (!rtf.GroupBegin)
      {
        if (rtf.GroupEnd)
        {
          if (rtf.GroupLevel < num1)
            flag1 = false;
          if (flag3 && rtf.GroupLevel < num2)
          {
            if (ListId >= 0)
              this.e.list[ListId] = strList;
            flag3 = false;
            ListId = -1;
          }
          if (flag2 && rtf.GroupLevel < num3)
          {
            flag2 = false;
            strListLevel1.FontId = !this.IsRtfPlainFont(rtf, ref rtf.group[rtf.GroupLevel + 1]) ? this.GetRtfFontId(rtf, rtf.group[rtf.GroupLevel + 1]) : 0;
            strListLevel1.FontStylesOff = rtf.group[rtf.GroupLevel + 1].StyleOff;
            strList.level[index1] = strListLevel1;
          }
          if (rtf.GroupLevel < groupLevel)
            return 0;
        }
        else if (rtf.IsControlWord)
        {
          if (this.strcmpi(rtf.CurWord, "list") == 0)
          {
            strList = new tc.StrList();
            flag3 = true;
            num2 = rtf.GroupLevel;
            strList.InUse = true;
            strList.LevelCount = length1 = 9;
            strList.level = new tc.StrListLevel[length1];
            for (int index3 = 0; index3 < length1; ++index3)
            {
              tc.StrListLevel strListLevel2 = new tc.StrListLevel();
              strList.level[index3] = strListLevel2.init();
            }
            index1 = -1;
          }
          else if (this.strcmpi(rtf.CurWord, "listtemplateid") == 0)
            strList.TmplId = rtf.IntParam;
          else if (this.strcmpi(rtf.CurWord, "listrestarthdn") == 0)
          {
            if (this.True(rtf.IntParam))
              strList.flags |= 1;
          }
          else if (this.strcmpi(rtf.CurWord, "listsimple") == 0 || this.strcmpi(rtf.CurWord, "listhybrid") == 0)
          {
            length1 = this.strcmpi(rtf.CurWord, "listhybrid") != 0 ? (!(rtf.param == "0") ? 1 : 9) : 9;
            if (strList.LevelCount != length1)
            {
              strList.level = new tc.StrListLevel[length1];
              for (int index4 = 0; index4 < length1; ++index4)
              {
                tc.StrListLevel strListLevel3 = new tc.StrListLevel();
                strList.level[index4] = strListLevel3.init();
              }
            }
            strList.LevelCount = length1;
          }
          else if (this.strcmpi(rtf.CurWord, "listname") == 0)
          {
            string str;
            this.ReadRtfGroupText(rtf, out str);
            int length2 = str.Length;
            if (length2 > 0 && str[length2 - 1] == ';')
              str = str.Substring(0, length2 - 1);
            strList.name = str;
          }
          else if (this.strcmpi(rtf.CurWord, "listid") == 0)
          {
            strList.id = rtf.IntParam;
            int index5 = 1;
            while (index5 < this.e.TotalLists && (!this.e.list[index5].InUse || this.e.list[index5].id != rtf.IntParam))
              ++index5;
            if (index5 < this.e.TotalLists)
            {
              ListId = index5;
              this.FreeList(ListId);
            }
            else if ((ListId = this.GetListSlot()) < 0)
              return 2;
          }
          else if (this.strcmpi(rtf.CurWord, "listlevel") == 0)
          {
            ++index1;
            if (index1 >= 0 && index1 < length1)
            {
              strListLevel1 = strList.level[index1] with
              {
                start = 1
              };
              flag1 = false;
              flag2 = true;
              num3 = rtf.GroupLevel;
              this.SetRtfFontDefault(rtf, rtf.group);
              if (this.e.TotalSID > 0 && this.e.StyleId[0].TwipsSize != 0)
                rtf.group[rtf.GroupLevel].PointSize2 = this.e.StyleId[0].TwipsSize / 10;
            }
          }
          else if (this.strcmpi(rtf.CurWord, "levelstartat") == 0)
          {
            if (index1 >= 0 && index1 < length1)
              strListLevel1.start = rtf.IntParam;
          }
          else if (this.strcmpi(rtf.CurWord, "levelnfc") == 0)
          {
            if (index1 >= 0 && index1 < length1)
              strListLevel1.NumType = rtf.IntParam;
          }
          else if (this.strcmpi(rtf.CurWord, "levelfollow") == 0)
          {
            if (index1 >= 0 && index1 < length1)
              strListLevel1.CharAft = rtf.IntParam;
          }
          else if (this.strcmpi(rtf.CurWord, "levelindent") == 0)
          {
            if (index1 >= 0 && index1 < length1)
              strListLevel1.MinIndent = rtf.IntParam;
          }
          else if (this.strcmpi(rtf.CurWord, "levelold") == 0)
          {
            if (index1 >= 0 && index1 < length1 && rtf.IntParam > 0)
              strListLevel1.flags |= 2;
          }
          else if (this.strcmpi(rtf.CurWord, "levellegal") == 0)
          {
            if (index1 >= 0 && index1 < length1 && rtf.IntParam > 0)
              strListLevel1.flags |= 8;
          }
          else if (this.strcmpi(rtf.CurWord, "levelnorestart") == 0)
          {
            if (index1 >= 0 && index1 < length1)
              strListLevel1.flags |= 32 /*0x20*/;
          }
          else if (this.strcmpi(rtf.CurWord, "leveltext") == 0)
          {
            rtf.group[rtf.GroupLevel].gflags |= 134217728 /*0x08000000*/;
            if (index1 >= 0 && index1 < length1)
            {
              flag1 = true;
              num1 = rtf.GroupLevel;
              index2 = 0;
            }
          }
          else if (this.strcmpi(rtf.CurWord, "levelnumbers") == 0)
          {
            rtf.group[rtf.GroupLevel].gflags |= 268435456 /*0x10000000*/;
          }
          else
          {
            int num4 = this.ProcessRtfControl(rtf);
            if (num4 != 0)
              return num4;
          }
        }
        else if (flag1)
        {
          if (rtf.OutBufLen > 0)
          {
            int index6 = 0;
            while (index6 < rtf.OutBufLen)
            {
              if (index2 < 49)
                strListLevel1.text[index2] = rtf.OutBuf[index6];
              ++index6;
              ++index2;
            }
          }
          int ignoreCount = rtf.group[rtf.GroupLevel].IgnoreCount;
          while (ignoreCount < rtf.WordLen)
          {
            if (index2 < 49)
              strListLevel1.text[index2] = rtf.CurWord[ignoreCount];
            ++ignoreCount;
            ++index2;
          }
          rtf.OutBuf = "";
          rtf.OutBufLen = 0;
          rtf.group[rtf.GroupLevel].IgnoreCount = 0;
          strListLevel1.text[index2] = char.MinValue;
        }
      }
    }
    return 1;
  }

  internal int ReadRtfListOr(tc.ClsRtf rtf)
  {
    int index1 = 0;
    int length = 0;
    int num1 = 0;
    int num2 = 0;
    int num3 = 0;
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    bool flag4 = false;
    int num4 = 0;
    int index2 = 0;
    tc.StrListOr pListOr2 = new tc.StrListOr();
    tc.StrListLevel strListLevel1 = new tc.StrListLevel().init();
    bool flag5 = false;
    int groupLevel = rtf.GroupLevel;
    rtf.group[groupLevel].gflags |= 16777216 /*0x01000000*/;
    for (int index3 = 0; index3 < this.e.TotalListOr; ++index3)
      this.e.ListOr[index3].RtfLs = 0;
    while (this.GetRtfWord(rtf))
    {
      if (!rtf.GroupBegin)
      {
        if (rtf.GroupEnd)
        {
          if (rtf.GroupLevel < num4)
            flag1 = false;
          if (flag3 && rtf.GroupLevel < num3)
          {
            flag3 = false;
            if (index1 >= 0)
              pListOr2.level[index1] = strListLevel1;
          }
          if (flag2 && rtf.GroupLevel < num2)
          {
            flag2 = false;
            int rtfFontId = this.GetRtfFontId(rtf, rtf.group[rtf.GroupLevel + 1]);
            if (rtfFontId > 0)
              strListLevel1.FontId = rtfFontId;
            strListLevel1.FontStylesOff = rtf.group[rtf.GroupLevel + 1].StyleOff;
          }
          if (flag4 && rtf.GroupLevel < num1)
          {
            flag4 = false;
            int ListId;
            if (this.e.RtfInput >= 2)
            {
              for (ListId = 0; ListId < this.e.TotalListOr; ++ListId)
              {
                if (this.e.ListOr[ListId].InUse && this.e.ListOr[ListId].RtfLs <= 0 && this.IsSameListOr(this.e.ListOr[ListId], pListOr2))
                {
                  this.FreeListOr(ListId);
                  break;
                }
              }
            }
            else
              ListId = this.e.TotalListOr;
            if (ListId == this.e.TotalListOr && (ListId = this.GetListOrSlot()) < 0)
              return 2;
            this.e.ListOr[ListId] = pListOr2;
            rtf.XlateLs[pListOr2.RtfLs] = ListId;
          }
          if (rtf.GroupLevel < groupLevel)
            return 0;
        }
        else if (rtf.IsControlWord)
        {
          if (this.strcmpi(rtf.CurWord, "listoverride") == 0)
          {
            pListOr2 = new tc.StrListOr();
            pListOr2.InUse = true;
            pListOr2.LevelCount = length = 0;
            index1 = -1;
            flag2 = false;
            flag4 = true;
            num1 = rtf.GroupLevel;
          }
          else if (this.strcmpi(rtf.CurWord, "listoverridecount") == 0)
          {
            length = rtf.IntParam;
            if (pListOr2.LevelCount != length)
            {
              pListOr2.level = (tc.StrListLevel[]) null;
              if (length > 0)
              {
                pListOr2.level = new tc.StrListLevel[length];
                for (int index4 = 0; index4 < length; ++index4)
                {
                  tc.StrListLevel strListLevel2 = new tc.StrListLevel();
                  pListOr2.level[index4] = strListLevel2.init();
                }
              }
            }
            pListOr2.LevelCount = length;
          }
          else if (this.strcmpi(rtf.CurWord, "listid") == 0)
          {
            int index5 = 1;
            while (index5 < this.e.TotalLists && (!this.e.list[index5].InUse || this.e.list[index5].id != rtf.IntParam))
              ++index5;
            if (index5 == this.e.TotalLists)
              index5 = this.e.TotalLists - 1;
            pListOr2.ListIdx = index5;
          }
          else if (this.strcmpi(rtf.CurWord, "ls") == 0)
            pListOr2.RtfLs = rtf.IntParam;
          else if (this.strcmpi(rtf.CurWord, "lfolevel") == 0)
          {
            ++index1;
            if (index1 >= 0 && index1 < length)
            {
              flag3 = true;
              num3 = rtf.GroupLevel;
              strListLevel1 = pListOr2.level[index1];
              flag1 = false;
              tc.StrList strList = this.e.list[pListOr2.ListIdx];
              if (index1 < strList.LevelCount)
                strListLevel1 = strList.level[index1].Copy();
              this.SetRtfFontDefault(rtf, rtf.group);
              flag5 = false;
            }
          }
          else if (this.strcmpi(rtf.CurWord, "listlevel") == 0)
          {
            if (index1 >= 0 && index1 < length)
            {
              flag2 = true;
              num2 = rtf.GroupLevel;
              if (this.e.TotalSID > 0 && this.e.StyleId[0].TwipsSize != 0)
                rtf.group[rtf.GroupLevel].PointSize2 = this.e.StyleId[0].TwipsSize / 10;
            }
          }
          else if (this.strcmpi(rtf.CurWord, "listoverridestartat") == 0)
          {
            flag5 = true;
            if (index1 >= 0 && index1 < length)
              strListLevel1.flags |= 1;
          }
          else if (this.strcmpi(rtf.CurWord, "levelnorestart") == 0)
          {
            if (index1 >= 0 && index1 < length)
              strListLevel1.flags |= 32 /*0x20*/;
          }
          else if (this.strcmpi(rtf.CurWord, "listoverrideformat") == 0)
          {
            if (index1 >= 0 && index1 < length)
              strListLevel1.flags |= 16 /*0x10*/;
          }
          else if (this.strcmpi(rtf.CurWord, "levelstartat") == 0)
          {
            if (((index1 < 0 ? 0 : (index1 < length ? 1 : 0)) & (flag5 ? 1 : 0)) != 0)
              strListLevel1.start = rtf.IntParam;
          }
          else if (this.strcmpi(rtf.CurWord, "levelnfc") == 0)
          {
            if (index1 >= 0 && index1 < length)
              strListLevel1.NumType = rtf.IntParam;
          }
          else if (this.strcmpi(rtf.CurWord, "levelfollow") == 0)
          {
            if (index1 >= 0 && index1 < length)
              strListLevel1.CharAft = rtf.IntParam;
          }
          else if (this.strcmpi(rtf.CurWord, "levelindent") == 0)
          {
            if (index1 >= 0 && index1 < length)
              strListLevel1.MinIndent = rtf.IntParam;
          }
          else if (this.strcmpi(rtf.CurWord, "levellegal") == 0)
          {
            if (index1 >= 0 && index1 < length && rtf.IntParam > 0)
              strListLevel1.flags |= 8;
          }
          else if (this.strcmpi(rtf.CurWord, "leveltext") == 0)
          {
            rtf.group[rtf.GroupLevel].gflags |= 134217728 /*0x08000000*/;
            if (index1 >= 0 && index1 < length)
            {
              flag1 = true;
              num4 = rtf.GroupLevel;
              index2 = 0;
            }
          }
          else if (this.strcmpi(rtf.CurWord, "levelnumbers") == 0)
          {
            rtf.group[rtf.GroupLevel].gflags |= 268435456 /*0x10000000*/;
          }
          else
          {
            int num5 = this.ProcessRtfControl(rtf);
            if (num5 != 0)
              return num5;
          }
        }
        else if (flag1)
        {
          if (rtf.OutBufLen > 0)
          {
            int index6 = 0;
            while (index6 < rtf.OutBufLen)
            {
              if (index2 < 49)
                strListLevel1.text[index2] = rtf.OutBuf[index6];
              ++index6;
              ++index2;
            }
          }
          int ignoreCount = rtf.group[rtf.GroupLevel].IgnoreCount;
          while (ignoreCount < rtf.WordLen)
          {
            if (index2 < 49)
              strListLevel1.text[index2] = rtf.CurWord[ignoreCount];
            ++ignoreCount;
            ++index2;
          }
          rtf.OutBuf = "";
          rtf.OutBufLen = 0;
          rtf.group[rtf.GroupLevel].IgnoreCount = 0;
          strListLevel1.text[index2] = char.MinValue;
        }
      }
    }
    return 1;
  }

  internal byte[] ReadRtfObjBytes(tc.ClsRtf rtf, out int size)
  {
    int num = 16384 /*0x4000*/;
    int index = size = 0;
    int count = num;
    byte[] OldObj = new byte[count];
    do
    {
      while (this.GetRtfHexChar(rtf))
      {
        if (index >= count)
        {
          count += num;
          OldObj = this.ReAlloc(OldObj, count);
        }
        OldObj[index] = (byte) rtf.CurChar;
        ++index;
      }
      if (rtf.CurChar == '}')
      {
        this.PushRtfChar(rtf);
        size = index;
        return OldObj;
      }
      if (rtf.CurChar == '{')
      {
        this.PushRtfChar(rtf);
        if (!this.GetRtfWord(rtf))
          return (byte[]) null;
      }
      else
        goto label_9;
    }
    while (this.SkipRtfGroup(rtf) == 0);
    return (byte[]) null;
label_9:
    return (byte[]) null;
  }

  internal int ReadRtfObject(tc.ClsRtf rtf)
  {
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    byte[] data = (byte[]) null;
    int ObjectType = 0;
    int ObjectAspect = 0;
    bool ObjectUpdate = false;
    int size = 0;
    int num1 = 0;
    int groupLevel = rtf.GroupLevel;
    this.e.CurObject = -1;
    while (this.GetRtfWord(rtf))
    {
      if (!rtf.GroupBegin)
      {
        if (rtf.GroupEnd)
        {
          if (rtf.GroupLevel < groupLevel)
          {
            if (!flag1 || this.e.CurObject <= 0 || ObjectType != 1 && ObjectType != 2 && ObjectType != 5)
              return 0;
            this.MakeRtfObject(rtf, data, ObjectType, size, ObjectAspect, ObjectUpdate);
            return num1;
          }
        }
        else if (!flag2)
        {
          if (rtf.IsControlWord)
          {
            if (this.strcmpi(rtf.CurWord, "objemb") == 0)
              ObjectType = 1;
            else if (this.strcmpi(rtf.CurWord, "objocx") == 0)
              ObjectType = 5;
            else if (this.strcmpi(rtf.CurWord, "objautlink") == 0)
              ObjectType = 2;
            else if (this.strcmpi(rtf.CurWord, "objupdate") == 0)
              ObjectUpdate = true;
            else if (this.strcmpi(rtf.CurWord, "sscontent") == 0)
              ObjectAspect = 1;
            else if (this.strcmpi(rtf.CurWord, "ssicon") == 0)
              ObjectAspect = 2;
            else if (this.strcmpi(rtf.CurWord, "result") == 0)
              flag3 = true;
            else if (this.strcmpi(rtf.CurWord, "objclass") == 0 || this.strcmpi(rtf.CurWord, "objname") == 0)
            {
              int num2;
              if ((num2 = this.SkipRtfGroup(rtf)) != 0)
                return num2;
            }
            else if (this.strcmpi(rtf.CurWord, "pict") == 0)
            {
              if (flag3)
              {
                int x = this.ReadRtfPicture(rtf);
                if (this.True(x))
                  num1 = x;
              }
              else
                flag2 = true;
            }
            else if (this.strcmpi(rtf.CurWord, "objdata") == 0)
            {
              if (!flag1)
              {
                if ((data = this.ReadRtfObjBytes(rtf, out size)) == null)
                  return 2;
                if (size > 0)
                  flag1 = true;
              }
            }
            else if (flag3)
            {
              int num3 = this.ProcessRtfControl(rtf);
              if (num3 != 0)
                return num3;
            }
          }
          else if (flag3)
          {
            if (rtf.OutBufLen + rtf.WordLen > 80 /*0x50*/ && !this.SendRtfText(rtf))
              return 2;
            rtf.OutBuf += rtf.CurWord;
            rtf.OutBufLen += rtf.WordLen;
          }
        }
      }
    }
    return 1;
  }

  internal int ReadRtfPicture(tc.ClsRtf rtf)
  {
    int num1 = 0;
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    int groupLevel = rtf.GroupLevel;
    tc.StrRtfPict pic = new tc.StrRtfPict();
    pic.type = -1;
    while (this.GetRtfWord(rtf))
    {
      if (!rtf.GroupBegin)
      {
        if (rtf.GroupEnd)
        {
          if (rtf.GroupLevel < groupLevel)
          {
            if (flag1)
            {
              if ((rtf.group[rtf.GroupLevel].shape.FrmFlags & 8388608 /*0x800000*/) != 0)
              {
                int openSlot;
                if ((openSlot = this.FindOpenSlot()) == -1 || !this.BuildRtfPicture(ref rtf.group[rtf.GroupLevel], pic, openSlot))
                  return 0;
                rtf.group[rtf.GroupLevel].shape.FillPict = openSlot;
                if (this.e.RtfParaFID > 0)
                {
                  this.e.TerFont[openSlot].PictWidth = this.e.ParaFrame[this.e.RtfParaFID].width;
                  this.e.TerFont[openSlot].PictHeight = this.e.ParaFrame[this.e.RtfParaFID].height;
                  this.SetPictSize(openSlot, this.e.TerFont[openSlot].PictHeight, this.e.TerFont[openSlot].PictWidth, false);
                }
              }
              else
                this.ImportRtfData(6, ref rtf.group[rtf.GroupLevel], (object) null, pic, tc.SkipRtfObject);
              rtf.flags = tc.ResetUintFlag(ref rtf.flags, 16384 /*0x4000*/);
            }
            return 0;
          }
        }
        else if (!flag2 && !flag1)
        {
          if (rtf.IsControlWord)
          {
            if (this.strcmpi(rtf.CurWord, "wmetafile") == 0)
            {
              pic.type = 1;
              continue;
            }
            if (this.strcmpi(rtf.CurWord, "emfblip") == 0)
            {
              pic.type = 9;
              continue;
            }
            if (this.strcmpi(rtf.CurWord, "dibitmap") == 0)
            {
              pic.type = 0;
              continue;
            }
            if (this.strcmpi(rtf.CurWord, "wbitmap") == 0)
            {
              pic.type = 4;
              continue;
            }
            if (this.strcmpi(rtf.CurWord, "jpegblip") == 0)
            {
              pic.type = 5;
              continue;
            }
            if (this.strcmpi(rtf.CurWord, "pngblip") == 0)
            {
              if ((this.e.TerFlags3 & 268435456 /*0x10000000*/) != 0)
              {
                pic.type = 10;
                continue;
              }
              rtf.flags1 |= 4;
              continue;
            }
            if (this.strcmpi(rtf.CurWord, "ssctl") == 0)
            {
              pic.type = 2;
              continue;
            }
            if (this.strcmpi(rtf.CurWord, "picw") == 0)
            {
              pic.OrigWidth = rtf.IntParam;
              continue;
            }
            if (this.strcmpi(rtf.CurWord, "pich") == 0)
            {
              pic.OrigHeight = rtf.IntParam;
              continue;
            }
            if (this.strcmpi(rtf.CurWord, "picwgoal") == 0)
            {
              pic.width = rtf.IntParam;
              continue;
            }
            if (this.strcmpi(rtf.CurWord, "piccropl") == 0)
            {
              pic.CropLeft = rtf.IntParam;
              continue;
            }
            if (this.strcmpi(rtf.CurWord, "piccropr") == 0)
            {
              pic.CropRight = rtf.IntParam;
              continue;
            }
            if (this.strcmpi(rtf.CurWord, "pichgoal") == 0)
            {
              pic.height = rtf.IntParam;
              continue;
            }
            if (this.strcmpi(rtf.CurWord, "piccropt") == 0)
            {
              pic.CropTop = rtf.IntParam;
              continue;
            }
            if (this.strcmpi(rtf.CurWord, "piccropb") == 0)
            {
              pic.CropBot = rtf.IntParam;
              continue;
            }
            if (this.strcmpi(rtf.CurWord, "picscalex") == 0)
            {
              pic.ScaleX = rtf.IntParam;
              continue;
            }
            if (this.strcmpi(rtf.CurWord, "picscaley") == 0)
            {
              pic.ScaleY = rtf.IntParam;
              continue;
            }
            if (this.strcmpi(rtf.CurWord, "wbmbitspixel") == 0)
            {
              pic.BitsPerPixel = rtf.IntParam;
              continue;
            }
            if (this.strcmpi(rtf.CurWord, "wbmplanes") == 0)
            {
              pic.planes = rtf.IntParam;
              continue;
            }
            if (this.strcmpi(rtf.CurWord, "wbmwidthbytes") == 0)
            {
              pic.WidthBytes = rtf.IntParam;
              continue;
            }
            if (this.strcmpi(rtf.CurWord, "sspicalign") == 0)
            {
              pic.align = rtf.IntParam;
              continue;
            }
            if (this.strcmpi(rtf.CurWord, "bin") == 0)
            {
              num1 = (int) rtf.DoubleParam;
              flag3 = true;
              rtf.WordLen = 0;
            }
            else
            {
              if (rtf.IgnoreText)
              {
                this.SkipRtfGroup(rtf);
                continue;
              }
              continue;
            }
          }
          for (int index = rtf.WordLen - 1; index >= 0; --index)
          {
            rtf.CurChar = rtf.CurWord[index];
            this.PushRtfChar(rtf);
          }
          int num2 = 0;
          int length = 8192 /*0x2000*/;
          int count = 0;
          pic.data = (byte[]) null;
          while ((!flag3 || this.GetRtfChar(rtf)) && (flag3 || this.GetRtfHexChar(rtf)))
          {
            if (count >= num2)
            {
              pic.data = pic.data == null ? new byte[length] : this.ReAlloc(pic.data, num2 + length);
              num2 += length;
            }
            pic.data[count] = (byte) rtf.CurChar;
            ++count;
            if (num1 > 0 && count >= num1)
              break;
          }
          pic.DataSize = count;
          pic.data = this.ReAlloc(pic.data, count);
          if (!flag2)
          {
            this.PushRtfChar(rtf);
            if (pic.type != 2)
            {
              MemoryStream memoryStream = new MemoryStream(pic.data);
              try
              {
                pic.image = pic.type == 1 || pic.type == 9 ? (Image) new Metafile((Stream) memoryStream) : Image.FromStream((Stream) memoryStream);
              }
              catch (Exception ex)
              {
                return 0;
              }
              this.e.ImgDenX = this.e.ScrResX;
              this.e.ImgDenY = this.e.ScrResY;
              Image image = pic.image;
              pic.ImageType = image.RawFormat.Guid;
              if (pic.type == 1)
                pic.ImageType = ImageFormat.Wmf.Guid;
              if (pic.type == 9)
                pic.ImageType = ImageFormat.Emf.Guid;
              if (pic.ImageType == ImageFormat.Wmf.Guid || pic.ImageType == ImageFormat.Emf.Guid)
              {
                if (pic.type == 1)
                  pic.hMeta = COp.Win32.SetMetaFileBitsEx(pic.data.Length, pic.data);
                if (pic.hMeta == IntPtr.Zero)
                {
                  pic.OrigWidth = (int) ((double) image.Width * 1440.0 / (double) image.HorizontalResolution);
                  pic.OrigHeight = (int) ((double) image.Height * 1440.0 / (double) image.VerticalResolution);
                }
              }
              else
              {
                pic.OrigHeight = this.MulDiv(image.Height, 1440, this.e.OrigScrResY);
                pic.OrigWidth = this.MulDiv(image.Width, 1440, this.e.OrigScrResY);
              }
              pic.type = 0;
              if (pic.width == 0)
              {
                pic.width = pic.OrigWidth;
                if (this.e.ImgDenX != this.e.ScrResX)
                  pic.width = this.MulDiv(pic.width, this.e.ScrResX, this.e.ImgDenX);
              }
              if (pic.height == 0)
              {
                pic.height = pic.OrigHeight;
                if (this.e.ImgDenY != this.e.ScrResY)
                  pic.height = this.MulDiv(pic.height, this.e.ScrResY, this.e.ImgDenY);
              }
              if (pic.width == 0 || pic.height == 0)
              {
                tc.StrRtfGroup strRtfGroup = rtf.group[groupLevel];
                if ((strRtfGroup.gflags & 33554432 /*0x02000000*/) != 0)
                {
                  pic.width = strRtfGroup.shape.width;
                  pic.height = strRtfGroup.shape.height;
                }
                if (pic.width == 0 || pic.height == 0)
                  return 0;
              }
            }
            else
            {
              pic.form = new tc.ClsForm();
              pic.form.CtlClass = "";
              for (int index = 0; index < count; ++index)
              {
                char c = (char) pic.data[index];
                if (c != char.MinValue)
                  pic.form.CtlClass += new string(c, 1);
                else
                  break;
              }
              if (this.strcmpi(pic.form.CtlClass, "Edit") == 0)
                pic.form.CtlClass = "TextBox";
            }
            flag1 = true;
          }
        }
      }
    }
    return 1;
  }

  internal bool ReadRtfReviewers(tc.ClsRtf rtf)
  {
    string str = "";
    int groupLevel;
    int num1 = groupLevel = rtf.GroupLevel;
    for (int index = 0; index < this.e.TotalReviewers; ++index)
      this.e.reviewer[index].RtfId = index;
    int num2 = -1;
    while (this.GetRtfWord(rtf))
    {
      if (rtf.GroupBegin)
      {
        groupLevel = rtf.GroupLevel;
        ++num2;
        str = "";
      }
      else
      {
        if (rtf.GroupEnd)
        {
          if (rtf.GroupLevel < num1)
            return true;
          if (rtf.GroupLevel < groupLevel)
          {
            if (num2 >= 0)
            {
              int length = str.Length;
              if (length > 0 && str[length - 1] == ';')
                str = str.Substring(0, length - 1);
              if (string.Compare(str, "unknown", true) == 0)
                str = "";
              int index = 0;
              while (index < this.e.TotalReviewers && string.Compare(this.e.reviewer[index].name, str, true) != 0)
                ++index;
              if (index == this.e.TotalReviewers)
              {
                index = this.trk.GetReviewerSlot();
                this.e.reviewer[index].name = str;
              }
              this.e.reviewer[index].RtfId = num2;
              continue;
            }
            continue;
          }
        }
        if (!rtf.IsControlWord)
          str += rtf.CurWord;
      }
    }
    return false;
  }

  internal int ReadRtfShape(tc.ClsRtf rtf)
  {
    int ObjectType = 0;
    int num1 = 0;
    int num2 = 20;
    byte[] data = (byte[]) null;
    int size = 0;
    int ObjectAspect = 0;
    bool ObjectUpdate = false;
    string str1 = "";
    bool FlipH = false;
    bool FlipV = false;
    bool flag1 = false;
    int rtfParaFid = this.e.RtfParaFID;
    int num3 = 1;
    bool flag2 = true;
    bool flag3 = false;
    int bright = 0;
    int contrast = 65536 /*0x010000*/;
    int num4 = 0;
    int num5 = 0;
    bool flag4 = false;
    bool flag5 = (rtf.flags & 32768 /*0x8000*/) != 0;
    int groupLevel1 = rtf.GroupLevel;
    int groupLevel2;
    int index1 = groupLevel2 = rtf.GroupLevel;
    tc.StrRtfGroup[] group = rtf.group;
    bool flag6 = (group[index1].gflags & 65536 /*0x010000*/) != 0;
    bool inTable = group[index1].InTable;
    if (!flag6)
      rtf.flags |= 16384 /*0x4000*/;
    tc.ResetUintFlag(ref rtf.flags1, 4);
    tc.ResetUintFlag(ref rtf.flags2, 8);
    int shpGroup1 = rtf.ShpGroup;
    for (int shpGroup2 = rtf.ShpGroup; shpGroup2 <= rtf.GroupLevel; ++shpGroup2)
    {
      if ((group[index1].gflags & 536870912 /*0x20000000*/) == 0)
      {
        group[shpGroup2].shape.LineWdth = 0;
        group[shpGroup2].shape.LineColor = Color.Black;
        group[shpGroup2].shape.BackColor = Color.White;
        group[shpGroup2].shape.DistFromText = 180;
      }
    }
    int pict1 = -1;
    while (this.GetRtfWord(rtf))
    {
      if (!rtf.GroupBegin)
      {
        if (rtf.GroupEnd)
        {
          if (rtf.GroupLevel < groupLevel1)
          {
            if (this.True(group[rtf.GroupLevel].gflags & 33554432 /*0x02000000*/))
              group[rtf.GroupLevel].FrmFlags |= group[groupLevel2].shape.FrmFlags;
            if (data != null && pict1 > 0 && size > 0 && ObjectType != 0)
              this.MakeRtfObject(rtf, data, ObjectType, size, ObjectAspect, ObjectUpdate);
            if (flag5)
              rtf.flags |= 32768 /*0x8000*/;
            else
              tc.ResetUintFlag(ref rtf.flags, 32768 /*0x8000*/);
            if ((rtf.flags & 16384 /*0x4000*/) != 0 && (rtf.flags1 & 4) != 0)
              group[shpGroup1].shape = group[groupLevel2].shape;
            if (!flag4)
              this.e.RtfParaFID = rtfParaFid;
            return 0;
          }
        }
        else
        {
          int groupLevel3 = rtf.GroupLevel;
          string name1;
          if (rtf.IsControlWord)
          {
            if (this.strcmpi(rtf.CurWord, "shpleft") == 0)
              group[groupLevel2].shape.x = rtf.IntParam;
            else if (this.strcmpi(rtf.CurWord, "shpright") == 0)
              group[groupLevel2].shape.width = rtf.IntParam - group[groupLevel2].shape.x;
            else if (this.strcmpi(rtf.CurWord, "shptop") == 0)
              group[groupLevel2].shape.y = rtf.IntParam;
            else if (this.strcmpi(rtf.CurWord, "shpbottom") == 0)
              group[groupLevel2].shape.height = rtf.IntParam - group[groupLevel2].shape.y;
            else if (this.strcmpi(rtf.CurWord, "shpbxpage") == 0)
              group[groupLevel2].shape.FrmFlags |= 1;
            else if (this.strcmpi(rtf.CurWord, "shpbxcolumn") == 0)
              group[groupLevel2].shape.FrmFlags |= 1073741824 /*0x40000000*/;
            else if (this.strcmpi(rtf.CurWord, "shpbypage") == 0)
              group[groupLevel2].shape.FrmFlags |= 32 /*0x20*/;
            else if (this.strcmpi(rtf.CurWord, "shpbymargin") == 0)
              group[groupLevel2].shape.FrmFlags |= 64 /*0x40*/;
            else if (this.strcmpi(rtf.CurWord, "shpbxignore") == 0)
            {
              tc.ResetUintFlag(ref group[groupLevel2].shape.FrmFlags, 1);
              group[groupLevel2].shape.FrmFlags |= 1074266112 /*0x40080000*/;
            }
            else if (this.strcmpi(rtf.CurWord, "shpbyignore") == 0)
            {
              tc.ResetUintFlag(ref group[groupLevel2].shape.FrmFlags, 268435552 /*0x10000060*/);
              group[groupLevel2].shape.FrmFlags |= 1048576 /*0x100000*/;
            }
            else if (this.strcmpi(rtf.CurWord, "shpwr") == 0)
            {
              int intParam;
              group[shpGroup1].shape.WrapType = intParam = rtf.IntParam;
              group[groupLevel2].shape.WrapType = intParam;
            }
            else if (this.strcmpi(rtf.CurWord, "shpwrk") == 0)
            {
              int intParam;
              group[shpGroup1].shape.WrapSide = intParam = rtf.IntParam;
              group[groupLevel2].shape.WrapSide = intParam;
            }
            else if (this.strcmpi(rtf.CurWord, "shpz") == 0)
            {
              int intParam;
              group[shpGroup1].shape.ZOrder = intParam = rtf.IntParam;
              group[groupLevel2].shape.ZOrder = intParam;
            }
            else if (this.strcmpi(rtf.CurWord, "ssshpalignleft") == 0)
            {
              int num6;
              group[shpGroup1].shape.align = num6 = 1024 /*0x0400*/;
              group[groupLevel2].shape.align = num6;
            }
            else if (this.strcmpi(rtf.CurWord, "ssshpalignright") == 0)
            {
              int num7;
              group[shpGroup1].shape.align = num7 = 2;
              group[groupLevel2].shape.align = num7;
            }
            else if (this.strcmpi(rtf.CurWord, "shpfblwtxt") == 0)
            {
              int intParam = rtf.IntParam;
              tc.ResetUintFlag(ref group[groupLevel2].shape.FrmFlags, 134217728 /*0x08000000*/);
              if (this.True(intParam))
                group[groupLevel2].shape.FrmFlags |= 134217728 /*0x08000000*/;
            }
            else if (this.strcmpi(rtf.CurWord, "sp") == 0)
              str1 = "";
            else if (this.strcmpi(rtf.CurWord, "sn") == 0)
              this.ReadRtfShapeParam(rtf, out str1);
            else if (this.strcmpi(rtf.CurWord, "sv") == 0)
            {
              if ((this.strcmpi(str1, "shapeType") == 0 || this.strcmpi(str1, "pVerticies") == 0) && !flag6)
              {
                int num8;
                if ((num8 = this.ReadRtfShapeParam(rtf, out name1)) > 0)
                  return num8;
                num1 = 0;
                if (this.strcmpi(str1, "pVerticies") == 0)
                {
                  string[] strArray = name1.Split(';', '(', ')', ',', ' ');
                  if (strArray.Length >= 2)
                  {
                    int num9 = this.ToInt(strArray[0]);
                    switch (this.ToInt(strArray[1]))
                    {
                      case 2:
                        num1 = 20;
                        break;
                      case 4:
                        num1 = 1;
                        break;
                    }
                    if (num1 == 20 && num9 == 8 && strArray.Length >= 6)
                    {
                      int num10 = this.ToInt(strArray[2]);
                      int num11 = this.ToInt(strArray[3]);
                      int num12 = this.ToInt(strArray[4]);
                      int num13 = this.ToInt(strArray[5]);
                      group[groupLevel2].shape.x += num10;
                      group[groupLevel2].shape.y += num11;
                      group[groupLevel2].shape.width = num12 - num10;
                      group[groupLevel2].shape.height = num13 - num11;
                    }
                  }
                }
                else
                  num1 = this.ToInt(name1);
                group[groupLevel2].shape.type = num1;
                if (num1 == 75 || num1 == 201)
                  tc.ResetUintFlag(ref rtf.flags, 32768 /*0x8000*/);
                if (num1 == 20 || num1 == 1 || num1 == 3)
                {
                  int groupLevel4 = rtf.GroupLevel;
                  flag1 = num1 == 20;
                  group[groupLevel2].shape.FrmFlags |= flag1 ? 256 /*0x0100*/ : 512 /*0x0200*/;
                  group[groupLevel2].FrmFlags |= group[groupLevel2].shape.FrmFlags;
                  group[groupLevel2].gflags |= 8388608 /*0x800000*/;
                  group[groupLevel2].HPageGroup = groupLevel2;
                  group[groupLevel2].shape.LineWdth = 15;
                  group[groupLevel2].shape.FrmFlags |= 1024 /*0x0400*/;
                  if ((group[groupLevel2].gflags & 536871040 /*0x20000080*/) != 0 | inTable || num1 == 3 || num1 == 20 || num1 == 1)
                  {
                    tc.StrRtfPict pic = new tc.StrRtfPict();
                    int num14;
                    pic.PictType = num14 = 11;
                    pic.type = num14;
                    flag3 = true;
                    group[groupLevel2].gflags2 |= 1;
                    this.e.RtfParaFID = group[groupLevel2].ParaFID;
                    rtf.GroupLevel = groupLevel2;
                    this.ImportRtfData(6, ref rtf.group[rtf.GroupLevel], (object) null, pic, tc.SkipRtfObject);
                    rtf.GroupLevel = groupLevel4;
                  }
                  else
                  {
                    group[groupLevel2].FrmFlags |= 2;
                    rtf.flags |= 32768 /*0x8000*/;
                    rtf.flags |= 64 /*0x40*/;
                    rtf.OutBuf += new string(this.e.ParaChar, 1);
                    ++rtf.OutBufLen;
                    rtf.GroupLevel = groupLevel2;
                    if (!this.SendRtfText(rtf))
                      return 3;
                    rtf.GroupLevel = groupLevel4;
                  }
                  this.e.ParaFrame[this.e.RtfParaFID].LineColor = group[groupLevel2].shape.LineColor;
                  this.e.ParaFrame[this.e.RtfParaFID].BackColor = group[groupLevel2].shape.BackColor;
                  this.e.ParaFrame[this.e.RtfParaFID].LineWdth = num2;
                  this.e.ParaFrame[this.e.RtfParaFID].flags |= 1024 /*0x0400*/;
                  if (flag1)
                    this.SetRtfLineOrient(rtf, FlipH, FlipV);
                  tc.ResetUintFlag(ref rtf.flags, 49152 /*0xC000*/);
                }
                else if (num1 == 202 && (group[groupLevel2].gflags & 128 /*0x80*/) == 0)
                {
                  bool flag7 = true;
                  if (inTable)
                  {
                    if (this.e.RtfInput >= 2)
                    {
                      flag7 = false;
                    }
                    else
                    {
                      long curLine = (long) this.e.CurLine;
                      while (curLine > 0L && (this.e.text[(int) (IntPtr) (curLine - 1L)].flags & 3) == 0)
                        --curLine;
                      if (curLine > 0L && (this.e.text[(int) (IntPtr) (curLine - 1L)].cid != 0 || this.e.text[(int) (IntPtr) (curLine - 1L)].fid != 0))
                        flag7 = false;
                    }
                  }
                  if (flag7)
                  {
                    group[groupLevel2].FrmFlags |= 130;
                    group[groupLevel2].FrmFlags |= group[groupLevel2].shape.FrmFlags;
                    group[groupLevel2].gflags |= 8388608 /*0x800000*/;
                    group[groupLevel2].HPageGroup = groupLevel2;
                    group[groupLevel2].shape.FrmFlags = 1024 /*0x0400*/;
                    group[groupLevel2].shape.LineWdth = 15;
                    group[groupLevel2].TextBoxMargin = 80 /*0x50*/;
                    rtf.flags |= 32768 /*0x8000*/;
                    rtf.flags |= 64 /*0x40*/;
                  }
                  else
                    num1 = 0;
                }
                else if (num1 != 75 && num1 != 201)
                  num1 = 0;
                flag2 = true;
              }
              else if (this.strcmpi(str1, "relLeft") == 0)
              {
                int num15;
                if ((num15 = this.ReadRtfShapeParam(rtf, out name1)) > 0)
                  return num15;
                int num16 = num4 = this.ToInt(name1);
                int num17 = group[groupLevel2].ShpGrp.left + num16;
                group[groupLevel2].shape.x = num17;
              }
              else if (this.strcmpi(str1, "relRight") == 0)
              {
                int num18;
                if ((num18 = this.ReadRtfShapeParam(rtf, out name1)) > 0)
                  return num18;
                int x = this.ToInt(name1) - num4;
                int groupWidth = group[groupLevel2].ShpGrp.GroupWidth;
                int width = group[groupLevel2].ShpGrp.width;
                group[groupLevel2].shape.x = group[groupLevel2].ShpGrp.left + this.MulDiv(num4 - group[groupLevel2].ShpGrp.GroupLeft, width, groupWidth);
                group[groupLevel2].shape.width = this.MulDiv(x, width, groupWidth);
              }
              else if (this.strcmpi(str1, "relTop") == 0)
              {
                int num19;
                if ((num19 = this.ReadRtfShapeParam(rtf, out name1)) > 0)
                  return num19;
                int num20 = num5 = this.ToInt(name1);
                int num21 = group[groupLevel2].ShpGrp.top + num20;
                group[groupLevel2].shape.y = num21;
              }
              else if (this.strcmpi(str1, "relBottom") == 0)
              {
                int num22;
                if ((num22 = this.ReadRtfShapeParam(rtf, out name1)) > 0)
                  return num22;
                int x = this.ToInt(name1) - num5;
                int groupHeight = group[groupLevel2].ShpGrp.GroupHeight;
                int height = group[groupLevel2].ShpGrp.height;
                group[groupLevel2].shape.y = group[groupLevel2].ShpGrp.top + this.MulDiv(num5 - group[groupLevel2].ShpGrp.GroupTop, height, groupHeight);
                group[groupLevel2].shape.height = this.MulDiv(x, height, groupHeight);
              }
              else if (this.strcmpi(str1, "posh") == 0)
              {
                int index2 = (rtf.flags2 & 8) == 0 || rtf.PictFID <= 0 ? this.e.RtfParaFID : rtf.PictFID;
                int num23;
                if ((num23 = this.ReadRtfShapeParam(rtf, out name1)) > 0)
                  return num23;
                int num24 = this.ToInt(name1);
                if (index2 > 0)
                {
                  if (num24 == 1)
                  {
                    int num25;
                    this.e.ParaFrame[index2].OrgX = num25 = 0;
                    this.e.ParaFrame[index2].x = num25;
                  }
                  else if (num24 == 2)
                    this.e.ParaFrame[index2].flags |= 8;
                  else if (num24 == 3)
                    this.e.ParaFrame[index2].flags |= 4;
                }
                if (num24 == 1)
                  group[groupLevel2].shape.x = 0;
                else if (num24 == 2)
                  group[groupLevel2].shape.FrmFlags |= 8;
                else if (num24 == 3)
                  group[groupLevel2].shape.FrmFlags |= 4;
              }
              else if (this.strcmpi(str1, "posrelv") == 0)
              {
                int num26;
                if ((num26 = this.ReadRtfShapeParam(rtf, out name1)) > 0)
                  return num26;
                int num27 = this.ToInt(name1);
                int index3 = (rtf.flags2 & 8) == 0 || rtf.PictFID <= 0 ? this.e.RtfParaFID : rtf.PictFID;
                if (index3 > 0)
                {
                  tc.ResetUintFlag(ref this.e.ParaFrame[index3].flags, 268435552 /*0x10000060*/);
                  switch (num27)
                  {
                    case 0:
                      this.e.ParaFrame[index3].flags |= 64 /*0x40*/;
                      break;
                    case 1:
                      this.e.ParaFrame[index3].flags |= 32 /*0x20*/;
                      break;
                    case 3:
                      this.e.ParaFrame[index3].flags |= 268435456 /*0x10000000*/;
                      break;
                  }
                }
                tc.ResetUintFlag(ref group[groupLevel2].shape.FrmFlags, 268435552 /*0x10000060*/);
                if (num27 == 0)
                  group[groupLevel2].shape.FrmFlags |= 64 /*0x40*/;
                else if (num27 == 1)
                  group[groupLevel2].shape.FrmFlags |= 32 /*0x20*/;
                else if (num27 == 3)
                  group[groupLevel2].shape.FrmFlags |= 268435456 /*0x10000000*/;
              }
              else if (this.strcmpi(str1, "posrelh") == 0)
              {
                int num28;
                if ((num28 = this.ReadRtfShapeParam(rtf, out name1)) > 0)
                  return num28;
                int num29 = this.ToInt(name1);
                int index4 = (rtf.flags2 & 8) == 0 || rtf.PictFID <= 0 ? this.e.RtfParaFID : rtf.PictFID;
                if (index4 > 0)
                {
                  tc.ResetUintFlag(ref this.e.ParaFrame[index4].flags, 1610612737 /*0x60000001*/);
                  switch (num29)
                  {
                    case 1:
                      this.e.ParaFrame[index4].x = this.e.ParaFrame[index4].OrgX - (int) this.InchesToTwips((double) rtf.sect.LeftMargin);
                      this.e.ParaFrame[index4].flags |= 1;
                      break;
                    case 2:
                      this.e.ParaFrame[index4].flags |= 1073741824 /*0x40000000*/;
                      break;
                    case 3:
                      this.e.ParaFrame[index4].flags |= 536870912 /*0x20000000*/;
                      break;
                  }
                }
                tc.ResetUintFlag(ref group[groupLevel2].shape.FrmFlags, 1610612737 /*0x60000001*/);
                if (num29 == 1)
                  group[groupLevel2].shape.FrmFlags |= 1;
                else if (num29 == 2)
                  group[groupLevel2].shape.FrmFlags |= 1073741824 /*0x40000000*/;
                else if (num29 == 3)
                  group[groupLevel2].shape.FrmFlags |= 536870912 /*0x20000000*/;
              }
              else if (this.strcmpi(str1, "fLayoutInCell") == 0)
              {
                int num30;
                if ((num30 = this.ReadRtfShapeParam(rtf, out name1)) > 0)
                  return num30;
                int num31 = this.ToInt(name1);
                int index5 = (rtf.flags2 & 8) == 0 || rtf.PictFID <= 0 ? this.e.RtfParaFID : rtf.PictFID;
                if (index5 > 0)
                {
                  tc.ResetUintFlag(ref this.e.ParaFrame[index5].flags, 16777216 /*0x01000000*/);
                  if (num31 == 1)
                    this.e.ParaFrame[index5].flags |= 16777216 /*0x01000000*/;
                }
                tc.ResetUintFlag(ref group[groupLevel2].shape.FrmFlags, 16777216 /*0x01000000*/);
                if (num31 == 1)
                  group[groupLevel2].shape.FrmFlags |= 16777216 /*0x01000000*/;
              }
              else if (this.strcmpi(str1, "lineWidth") == 0)
              {
                int index6 = (rtf.flags2 & 8) == 0 || rtf.PictFID <= 0 ? this.e.RtfParaFID : rtf.PictFID;
                if (flag2)
                {
                  int num32;
                  if ((num32 = this.ReadRtfShapeParam(rtf, out name1)) > 0)
                    return num32;
                  num2 = this.EmuToTwips(this.ToInt(name1));
                  if ((num1 == 20 || num1 == 1) && index6 > 0)
                  {
                    this.e.ParaFrame[index6].LineWdth = num2;
                    this.e.ParaFrame[index6].flags |= 1024 /*0x0400*/;
                  }
                  if (num1 == 202 || num1 == 1)
                  {
                    group[groupLevel2].shape.LineWdth = num2;
                    group[groupLevel2].shape.FrmFlags = 1024 /*0x0400*/;
                  }
                }
              }
              else if (this.strcmpi(str1, "fLine") == 0)
              {
                int index7 = (rtf.flags2 & 8) == 0 || rtf.PictFID <= 0 ? this.e.RtfParaFID : rtf.PictFID;
                int num33;
                if ((num33 = this.ReadRtfShapeParam(rtf, out name1)) > 0)
                  return num33;
                flag2 = this.True(this.ToInt(name1));
                switch (num1)
                {
                  case 1:
                  case 75:
                  case 202:
                    if (flag2)
                    {
                      if (group[groupLevel2].shape.LineWdth == 0)
                        group[groupLevel2].shape.LineWdth = 20;
                      group[groupLevel2].shape.FrmFlags = 1024 /*0x0400*/;
                      if (index7 > 0)
                      {
                        this.e.ParaFrame[index7].flags |= 1024 /*0x0400*/;
                        if (this.e.ParaFrame[index7].LineWdth == 0)
                        {
                          this.e.ParaFrame[index7].LineWdth = 20;
                          continue;
                        }
                        continue;
                      }
                      continue;
                    }
                    tc.ResetUintFlag(ref group[groupLevel2].shape.FrmFlags, 1024 /*0x0400*/);
                    if (index7 > 0)
                    {
                      tc.ResetUintFlag(ref this.e.ParaFrame[index7].flags, 1024 /*0x0400*/);
                      continue;
                    }
                    continue;
                  case 20:
                    if (!flag2)
                    {
                      this.e.ParaFrame[index7].flags |= 262144 /*0x040000*/;
                      if (index7 > 0)
                      {
                        tc.ResetUintFlag(ref this.e.ParaFrame[index7].flags, 262144 /*0x040000*/);
                        continue;
                      }
                      continue;
                    }
                    continue;
                  default:
                    continue;
                }
              }
              else if (this.strcmpi(str1, "lineDashing") == 0)
              {
                int index8 = (rtf.flags2 & 8) == 0 || rtf.PictFID <= 0 ? this.e.RtfParaFID : rtf.PictFID;
                int num34;
                if ((num34 = this.ReadRtfShapeParam(rtf, out name1)) > 0)
                  return num34;
                if (this.ToInt(name1) == 6)
                {
                  if ((num1 == 20 || num1 == 1) && index8 > 0)
                    this.e.ParaFrame[index8].flags |= 3072 /*0x0C00*/;
                  if (num1 == 202 || num1 == 1)
                    group[groupLevel2].shape.FrmFlags = 3072 /*0x0C00*/;
                }
              }
              else if (this.strcmpi(str1, "fillBlip") == 0)
              {
                for (int index9 = groupLevel2; index9 <= rtf.GroupLevel; ++index9)
                  group[index9].shape.FrmFlags |= 8388608 /*0x800000*/;
                if (!flag6)
                  rtf.flags |= 16384 /*0x4000*/;
                flag4 = false;
              }
              else if (this.strcmpi(str1, "fillType") == 0)
              {
                int num35;
                if ((num35 = this.ReadRtfShapeParam(rtf, out name1)) > 0)
                  return num35;
                if (this.ToInt(name1) == 3)
                {
                  for (int index10 = groupLevel2; index10 <= rtf.GroupLevel; ++index10)
                    group[index10].shape.FrmFlags |= 8388608 /*0x800000*/;
                  if (!flag6)
                    rtf.flags |= 16384 /*0x4000*/;
                  flag4 = true;
                }
              }
              else if (this.strcmpi(str1, "pibName") == 0)
              {
                rtf.flags |= 8192 /*0x2000*/;
                string str2;
                int num36;
                if ((num36 = this.ReadRtfShapeParam(rtf, out str2)) > 0)
                  return num36;
                tc.ResetUintFlag(ref rtf.flags, 8192 /*0x2000*/);
                this.StripSlashes(str2, out name1);
                this.XlateRtfHex(name1);
                if (pict1 < 0)
                  pict1 = this.ReadRtfLinkedPicture(rtf, name1);
                else
                  this.e.TerPictLinkName(pict1, false, ref name1);
                if (pict1 >= 0)
                  tc.ResetUintFlag(ref rtf.flags, 16384 /*0x4000*/);
              }
              else if (this.strcmpi(str1, "pibFlags") == 0)
              {
                int num37;
                if ((num37 = this.ReadRtfShapeParam(rtf, out name1)) > 0)
                  return num37;
                if (pict1 >= 0 && name1 != "10" && name1 != "14")
                {
                  string name2 = "";
                  this.e.TerPictLinkName(pict1, false, ref name2);
                }
              }
              else if (this.strcmpi(str1, "fillColor") == 0)
              {
                int num38;
                if ((num38 = this.ReadRtfShapeParam(rtf, out name1)) > 0)
                  return num38;
                Color color = this.ToColor(this.ToInt(name1));
                if (flag6 && this.e.RtfInput < 2 && (this.e.TerFlags3 & 131072 /*0x020000*/) == 0)
                {
                  if ((this.e.TerFlags6 & 16384 /*0x4000*/) != 0)
                    this.e.TextDefBkColor = color;
                  else
                    this.e.PageBkColor = color;
                }
                if ((num1 == 20 || num1 == 1) && this.e.RtfParaFID > 0)
                {
                  this.e.ParaFrame[this.e.RtfParaFID].BackColor = color;
                  this.e.ParaFrame[this.e.RtfParaFID].FillPattern = num3;
                }
                if (num1 == 202 || num1 == 1)
                {
                  group[groupLevel2].shape.BackColor = color;
                  group[groupLevel2].shape.FillPattern = num3;
                }
              }
              else if (this.strcmpi(str1, "fFilled") == 0)
              {
                int num39;
                if ((num39 = this.ReadRtfShapeParam(rtf, out name1)) > 0)
                  return num39;
                if (this.ToInt(name1) == 0)
                  num3 = group[groupLevel2].shape.FillPattern = 0;
              }
              else if (this.strcmpi(str1, "lineColor") == 0)
              {
                int num40;
                if ((num40 = this.ReadRtfShapeParam(rtf, out name1)) > 0)
                  return num40;
                Color color = this.ToColor(this.ToInt(name1));
                if ((num1 == 20 || num1 == 1) && this.e.RtfParaFID > 0)
                  this.e.ParaFrame[this.e.RtfParaFID].LineColor = color;
                if (num1 == 202 || num1 == 1)
                  group[groupLevel2].shape.LineColor = color;
              }
              else if (this.strcmpi(str1, "txflTextFlow") == 0)
              {
                int num41 = 0;
                int num42;
                if ((num42 = this.ReadRtfShapeParam(rtf, out name1)) > 0)
                  return num42;
                switch (this.ToInt(name1))
                {
                  case 1:
                  case 3:
                    num41 = 270;
                    break;
                  case 2:
                    num41 = 90;
                    break;
                }
                for (int shpGroup3 = rtf.ShpGroup; shpGroup3 <= rtf.GroupLevel; ++shpGroup3)
                  group[shpGroup3].TextAngle = num41;
              }
              else if (this.strcmpi(str1, "fBehindDocument") == 0)
              {
                int num43;
                if ((num43 = this.ReadRtfShapeParam(rtf, out name1)) > 0)
                  return num43;
                int x = this.ToInt(name1);
                tc.ResetUintFlag(ref group[groupLevel2].shape.FrmFlags, 134217728 /*0x08000000*/);
                if (this.True(x))
                  group[groupLevel2].shape.FrmFlags |= 134217728 /*0x08000000*/;
              }
              else if (this.strcmpi(str1, "dxTextLeft") == 0)
              {
                int num44;
                if ((num44 = this.ReadRtfShapeParam(rtf, out name1)) > 0)
                  return num44;
                int x = this.ToInt(name1);
                group[groupLevel2].TextBoxMargin = this.EmuToTwips(x);
              }
              else if (this.strcmpi(str1, "fFlipH") == 0 || this.strcmpi(str1, "fRelFlipH") == 0)
              {
                int num45;
                if ((num45 = this.ReadRtfShapeParam(rtf, out name1)) > 0)
                  return num45;
                FlipH = this.True(this.ToInt(name1));
                if (flag1 & FlipH)
                  this.SetRtfLineOrient(rtf, FlipH, FlipV);
              }
              else if (this.strcmpi(str1, "fFlipV") == 0 || this.strcmpi(str1, "fRelFlipV") == 0)
              {
                int num46;
                if ((num46 = this.ReadRtfShapeParam(rtf, out name1)) > 0)
                  return num46;
                FlipV = this.True(this.ToInt(name1));
                if (flag1 & FlipV)
                  this.SetRtfLineOrient(rtf, FlipH, FlipV);
              }
              else if (this.strcmpi(str1, "wzName") == 0)
              {
                int num47;
                if ((num47 = this.ReadRtfShapeParam(rtf, out name1)) > 0)
                  return num47;
                if (this.strcmpi(name1, "WordPictureWatermark3") == 0 && (rtf.flags2 & 8) != 0 && rtf.PictFID > 0)
                {
                  if (this.e.RtfInput < 2)
                  {
                    this.e.WmParaFID = rtf.PictFID;
                    this.e.ParaFrame[rtf.PictFID].flags |= 4194304 /*0x400000*/;
                    int pict2 = this.e.ParaFrame[this.e.WmParaFID].pict;
                    if (bright != 0 || contrast != 65536 /*0x010000*/)
                    {
                      this.ApplyPictureBrightnessContrast(pict2, bright, contrast);
                      this.e.WmWashed = true;
                    }
                    else
                    {
                      if (this.e.WmImageAttr != null)
                        this.e.WmImageAttr.Dispose();
                      this.e.WmImageAttr = (ImageAttributes) null;
                    }
                    this.SetPictSize(pict2, this.TwipsToScrY(this.e.TerFont[pict2].PictHeight), this.TwipsToScrX(this.e.TerFont[pict2].PictWidth), true);
                    this.XlateSizeForPrt(pict2);
                  }
                  this.MoveLineData(rtf.PictFrameLine, rtf.PictFrameCol, 1, 'D');
                }
              }
              else if (this.strcmpi(str1, "pictureBrightness") == 0)
              {
                int num48;
                if ((num48 = this.ReadRtfShapeParam(rtf, out name1)) > 0)
                  return num48;
                bright = this.ToInt(name1);
              }
              else if (this.strcmpi(str1, "pictureContrast") == 0)
              {
                int num49;
                if ((num49 = this.ReadRtfShapeParam(rtf, out name1)) > 0)
                  return num49;
                contrast = this.ToInt(name1);
              }
            }
            else if (this.strcmpi(rtf.CurWord, "pict") == 0)
            {
              if (pict1 >= 0 || (rtf.flags & 16384 /*0x4000*/) == 0)
              {
                this.SkipRtfGroup(rtf);
              }
              else
              {
                int x = this.ReadRtfPicture(rtf);
                if (this.True(x))
                  return x;
                if ((group[rtf.GroupLevel].shape.FrmFlags & 8388608 /*0x800000*/) != 0 && group[rtf.GroupLevel].shape.FillPict > 0)
                {
                  for (int index11 = groupLevel2; index11 < rtf.GroupLevel; ++index11)
                    group[index11].shape.FillPict = group[rtf.GroupLevel].shape.FillPict;
                  if ((num1 == 20 || num1 == 1) && this.e.RtfParaFID > 0)
                  {
                    this.e.ParaFrame[this.e.RtfParaFID].flags |= 8388608 /*0x800000*/;
                    this.e.ParaFrame[this.e.RtfParaFID].FillPict = group[groupLevel2].shape.FillPict;
                  }
                }
                if ((rtf.flags & 16384 /*0x4000*/) == 0 && (rtf.flags & 32768 /*0x8000*/) != 0 && !flag6)
                {
                  rtf.OutBuf += new string(this.e.ParaChar, 1);
                  ++rtf.OutBufLen;
                  this.SendRtfText(rtf);
                }
              }
            }
            else if (this.strcmpi(rtf.CurWord, "objemb") == 0)
              ObjectType = 1;
            else if (this.strcmpi(rtf.CurWord, "objocx") == 0)
              ObjectType = 5;
            else if (this.strcmpi(rtf.CurWord, "objautlink") == 0)
              ObjectType = 2;
            else if (this.strcmpi(rtf.CurWord, "objupdate") == 0)
              ObjectUpdate = true;
            else if (this.strcmpi(rtf.CurWord, "sscontent") == 0)
              ObjectAspect = 1;
            else if (this.strcmpi(rtf.CurWord, "ssicon") == 0)
              ObjectAspect = 2;
            else if (this.strcmpi(rtf.CurWord, "objdata") == 0)
              data = this.ReadRtfObjBytes(rtf, out size);
            else if (this.strcmpi(rtf.CurWord, "shp") == 0)
              this.SkipRtfGroup(rtf);
            else if (this.strcmpi(rtf.CurWord, "shpgrp") == 0)
              this.SkipRtfGroup(rtf);
            else if (this.strcmpi(rtf.CurWord, "shptxt") == 0)
            {
              if (this.e.RtfParaFID > 0 && num1 == 20)
              {
                rtf.flags |= 16384 /*0x4000*/;
              }
              else
              {
                if (num1 == 1 && this.e.CurLine > 0)
                {
                  int num50 = this.e.CurLine - 1;
                  if (flag3)
                  {
                    for (int line = this.e.CurLine - 1; line >= 0; --line)
                    {
                      if (this.e.text[line].len == 1)
                      {
                        int idx = (int) this.OpenCfmt(line)[0];
                        this.CloseCfmt(line);
                        if ((this.e.TerFont[idx].style & 128 /*0x80*/) != 0 && this.e.TerFont[idx].ParaFID == this.e.RtfParaFID)
                        {
                          this.MoveLineData(line, 0, 1, 'D');
                          this.DeleteTerObject(idx);
                          this.e.ParaFrame[this.e.RtfParaFID].InUse = false;
                          this.e.RtfParaFID = 0;
                          break;
                        }
                      }
                    }
                  }
                  else
                  {
                    int line = this.e.CurLine - 1;
                    while (line >= 0 && this.e.text[line].fid != this.e.RtfParaFID)
                      --line;
                    if (line >= 0 && this.e.text[line].fid > 0)
                    {
                      this.MoveLineData(line, 0, 1, 'D');
                      this.e.ParaFrame[this.e.RtfParaFID].InUse = false;
                      int num51;
                      this.e.RtfParaFID = num51 = 0;
                      this.e.text[line].fid = num51;
                    }
                  }
                  group[groupLevel2].shape.type = num1 = 202;
                  tc.ResetUintFlag(ref group[groupLevel2].FrmFlags, 512 /*0x0200*/);
                  tc.ResetUintFlag(ref group[groupLevel2].shape.FrmFlags, 512 /*0x0200*/);
                  tc.ResetUintFlag(ref group[groupLevel2].gflags2, 1);
                  group[groupLevel2].FrmFlags |= 130;
                  group[groupLevel2].FrmFlags |= group[groupLevel2].shape.FrmFlags;
                  group[groupLevel2].gflags |= 8388608 /*0x800000*/;
                  group[groupLevel2].HPageGroup = groupLevel2;
                  for (int index12 = groupLevel2 + 1; index12 <= rtf.GroupLevel; ++index12)
                  {
                    group[index12].shape = group[groupLevel2].shape;
                    group[index12].FrmFlags = group[groupLevel2].FrmFlags;
                    group[index12].gflags = group[groupLevel2].gflags;
                    group[index12].gflags2 = group[groupLevel2].gflags2;
                    group[index12].HPageGroup = group[groupLevel2].HPageGroup;
                  }
                  rtf.flags |= 32768 /*0x8000*/;
                  rtf.flags |= 64 /*0x40*/;
                  rtf.flags |= 16384 /*0x4000*/;
                }
                if (num1 == 202)
                {
                  if (group[groupLevel2].shape.width < 40 && group[groupLevel2].shape.height < 40)
                  {
                    this.SkipRtfGroup(rtf);
                  }
                  else
                  {
                    int num52 = this.ReadRtfShapeText(rtf);
                    if (num52 != 0)
                      return num52;
                  }
                  tc.ResetUintFlag(ref rtf.flags, 16384 /*0x4000*/);
                }
              }
            }
          }
        }
      }
    }
    return 1;
  }

  internal int ReadRtfShapeParam(tc.ClsRtf rtf, out string str)
  {
    bool flag = true;
    str = "";
    int num = 0;
    int groupLevel = rtf.GroupLevel;
    while (this.GetRtfWord(rtf))
    {
      if (!rtf.GroupBegin)
      {
        if (rtf.GroupEnd)
        {
          if (rtf.GroupLevel < groupLevel)
            return 0;
        }
        else if (flag)
        {
          int length = rtf.CurWord.Length;
          str += rtf.CurWord;
          num += length;
        }
      }
    }
    return 1;
  }

  internal int ReadRtfShapeProp(tc.ClsRtf rtf, string prop)
  {
    int groupLevel = rtf.GroupLevel;
    while (this.GetRtfWord(rtf))
    {
      if (!rtf.GroupBegin)
      {
        if (rtf.GroupEnd)
        {
          if (rtf.GroupLevel < groupLevel)
            return 0;
        }
        else if (rtf.IsControlWord && this.strcmpi(rtf.CurWord, prop) == 0)
        {
          int x = this.ReadRtfPicture(rtf);
          if (this.True(x))
            return x;
        }
      }
    }
    return 1;
  }

  internal int ReadRtfShapeText(tc.ClsRtf rtf)
  {
    int groupLevel = rtf.GroupLevel;
    rtf.group[groupLevel].gflags |= 8388608 /*0x800000*/;
    while (this.GetRtfWord(rtf))
    {
      if (!rtf.GroupEnd || rtf.GroupLevel >= groupLevel)
      {
        if (rtf.IsControlWord)
        {
          int num = this.ProcessRtfControl(rtf);
          if (num != 0)
            return num;
        }
        else if (rtf.WordLen > 0)
          this.CopyToOutBuf(rtf);
      }
      else
      {
        if (rtf.OutBufLen > 0)
          this.SendRtfText(rtf);
        return 0;
      }
    }
    return 1;
  }

  internal int ReadRtfShpGrp(tc.ClsRtf rtf)
  {
    int num1 = 1;
    string str1 = "";
    int num2 = 0;
    int num3 = 0;
    int groupLevel1 = rtf.GroupLevel;
    int groupLevel2;
    int index = groupLevel2 = rtf.GroupLevel;
    tc.StrRtfGroup[] group = rtf.group;
    group[index].shape.LineWdth = 0;
    group[index].shape.LineColor = Color.Black;
    group[index].shape.BackColor = Color.White;
    group[index].shape.FrmFlags |= 2097152 /*0x200000*/;
    tc.StrShpGrp shpGrp = group[index].ShpGrp;
    while (this.GetRtfWord(rtf))
    {
      if (!rtf.GroupBegin)
      {
        if (rtf.GroupEnd)
        {
          if (rtf.GroupLevel < groupLevel1)
            return 0;
        }
        else
        {
          int groupLevel3 = rtf.GroupLevel;
          string str2;
          if (rtf.IsControlWord)
          {
            if (this.strcmpi(rtf.CurWord, "shpleft") == 0)
              group[groupLevel2].ShpGrp.left = rtf.IntParam;
            else if (this.strcmpi(rtf.CurWord, "shpright") == 0)
              group[groupLevel2].ShpGrp.width = rtf.IntParam - group[groupLevel2].ShpGrp.left;
            else if (this.strcmpi(rtf.CurWord, "shptop") == 0)
              group[groupLevel2].ShpGrp.top = rtf.IntParam;
            else if (this.strcmpi(rtf.CurWord, "shpbottom") == 0)
              group[groupLevel2].ShpGrp.height = rtf.IntParam - group[groupLevel2].ShpGrp.top;
            else if (this.strcmpi(rtf.CurWord, "shpbxpage") == 0)
              group[groupLevel2].shape.FrmFlags |= 1;
            else if (this.strcmpi(rtf.CurWord, "shpbxcolumn") == 0)
              group[groupLevel2].shape.FrmFlags |= 1073741824 /*0x40000000*/;
            else if (this.strcmpi(rtf.CurWord, "shpbypage") == 0)
              group[groupLevel2].shape.FrmFlags |= 32 /*0x20*/;
            else if (this.strcmpi(rtf.CurWord, "shpbymargin") == 0)
              group[groupLevel2].shape.FrmFlags |= 64 /*0x40*/;
            else if (this.strcmpi(rtf.CurWord, "shpbxignore") == 0)
            {
              tc.ResetUintFlag(ref group[groupLevel2].shape.FrmFlags, 1);
              group[groupLevel2].shape.FrmFlags |= 1074266112 /*0x40080000*/;
            }
            else if (this.strcmpi(rtf.CurWord, "shpbyignore") == 0)
            {
              tc.ResetUintFlag(ref group[groupLevel2].shape.FrmFlags, 268435552 /*0x10000060*/);
              group[groupLevel2].shape.FrmFlags |= 1048576 /*0x100000*/;
            }
            else if (this.strcmpi(rtf.CurWord, "shpwr") == 0)
              group[groupLevel2].shape.WrapType = rtf.IntParam;
            else if (this.strcmpi(rtf.CurWord, "shpwrk") == 0)
              group[groupLevel2].shape.WrapSide = rtf.IntParam;
            else if (this.strcmpi(rtf.CurWord, "shpz") == 0)
              group[groupLevel2].shape.ZOrder = rtf.IntParam;
            else if (this.strcmpi(rtf.CurWord, "ssshpalignleft") == 0)
              group[groupLevel2].shape.align = 1024 /*0x0400*/;
            else if (this.strcmpi(rtf.CurWord, "ssshpalignright") == 0)
              group[groupLevel2].shape.align = 2;
            else if (this.strcmpi(rtf.CurWord, "sp") == 0)
              str1 = "";
            else if (this.strcmpi(rtf.CurWord, "sn") == 0)
              this.ReadRtfShapeParam(rtf, out str1);
            else if (this.strcmpi(rtf.CurWord, "sv") == 0)
            {
              if (this.strcmpi(str1, "groupLeft") == 0)
              {
                int num4;
                if ((num4 = this.ReadRtfShapeParam(rtf, out str2)) > 0)
                  return num4;
                group[groupLevel2].ShpGrp.GroupLeft = this.ToInt(str2);
              }
              else if (this.strcmpi(str1, "groupRight") == 0)
              {
                int num5;
                if ((num5 = this.ReadRtfShapeParam(rtf, out str2)) > 0)
                  return num5;
                group[groupLevel2].ShpGrp.GroupWidth = this.ToInt(str2) - group[groupLevel2].ShpGrp.GroupLeft;
              }
              else if (this.strcmpi(str1, "groupTop") == 0)
              {
                int num6;
                if ((num6 = this.ReadRtfShapeParam(rtf, out str2)) > 0)
                  return num6;
                group[groupLevel2].ShpGrp.GroupTop = this.ToInt(str2);
              }
              else if (this.strcmpi(str1, "groupBottom") == 0)
              {
                int num7;
                if ((num7 = this.ReadRtfShapeParam(rtf, out str2)) > 0)
                  return num7;
                group[groupLevel2].ShpGrp.GroupHeight = this.ToInt(str2) - group[groupLevel2].ShpGrp.GroupTop;
              }
              else if (this.strcmpi(str1, "relLeft") == 0)
              {
                int num8;
                if ((num8 = this.ReadRtfShapeParam(rtf, out str2)) > 0)
                  return num8;
                int num9 = num2 = this.ToInt(str2);
                int num10 = shpGrp.left + num9;
                group[groupLevel2].ShpGrp.left = num10;
              }
              else if (this.strcmpi(str1, "relRight") == 0)
              {
                int num11;
                if ((num11 = this.ReadRtfShapeParam(rtf, out str2)) > 0)
                  return num11;
                int x = this.ToInt(str2) - num2;
                int groupWidth = shpGrp.GroupWidth;
                int width = shpGrp.width;
                group[groupLevel2].ShpGrp.left = shpGrp.left + this.MulDiv(num2 - shpGrp.GroupLeft, width, groupWidth);
                group[groupLevel2].ShpGrp.width = this.MulDiv(x, width, groupWidth);
              }
              else if (this.strcmpi(str1, "relTop") == 0)
              {
                int num12;
                if ((num12 = this.ReadRtfShapeParam(rtf, out str2)) > 0)
                  return num12;
                int num13 = num3 = this.ToInt(str2);
                int num14 = shpGrp.top + num13;
                group[groupLevel2].ShpGrp.top = num14;
              }
              else if (this.strcmpi(str1, "relBottom") == 0)
              {
                int num15;
                if ((num15 = this.ReadRtfShapeParam(rtf, out str2)) > 0)
                  return num15;
                int x = this.ToInt(str2) - num3;
                int groupHeight = shpGrp.GroupHeight;
                int height = shpGrp.height;
                group[groupLevel2].ShpGrp.top = shpGrp.top + this.MulDiv(num3 - shpGrp.GroupTop, height, groupHeight);
                group[groupLevel2].ShpGrp.height = this.MulDiv(x, height, groupHeight);
              }
              else if (this.strcmpi(str1, "lineWidth") == 0)
              {
                int num16;
                if ((num16 = this.ReadRtfShapeParam(rtf, out str2)) > 0)
                  return num16;
                int twips = this.EmuToTwips(this.ToInt(str2));
                group[groupLevel2].shape.LineWdth = twips;
                group[groupLevel2].shape.FrmFlags |= 1024 /*0x0400*/;
              }
              else if (this.strcmpi(str1, "fLine") == 0)
              {
                int num17;
                if ((num17 = this.ReadRtfShapeParam(rtf, out str2)) > 0)
                  return num17;
                if (this.True(this.ToInt(str2)))
                  group[groupLevel2].shape.FrmFlags = 1024 /*0x0400*/;
              }
              else if (this.strcmpi(str1, "lineDashing") == 0)
              {
                int num18;
                if ((num18 = this.ReadRtfShapeParam(rtf, out str2)) > 0)
                  return num18;
                if (this.ToInt(str2) == 6)
                  group[groupLevel2].shape.FrmFlags |= 2048 /*0x0800*/;
              }
              else if (this.strcmpi(str1, "fillColor") == 0)
              {
                int num19;
                if ((num19 = this.ReadRtfShapeParam(rtf, out str2)) > 0)
                  return num19;
                Color color = this.ToColor(this.ToInt(str2));
                group[groupLevel2].shape.BackColor = color;
                group[groupLevel2].shape.FillPattern = num1;
              }
              else if (this.strcmpi(str1, "fFilled") == 0)
              {
                int num20;
                if ((num20 = this.ReadRtfShapeParam(rtf, out str2)) > 0)
                  return num20;
                if (this.ToInt(str2) == 0)
                  num1 = group[groupLevel2].shape.FillPattern = 0;
              }
              else if (this.strcmpi(str1, "lineColor") == 0)
              {
                int num21;
                if ((num21 = this.ReadRtfShapeParam(rtf, out str2)) > 0)
                  return num21;
                Color color = this.ToColor(this.ToInt(str2));
                group[groupLevel2].shape.LineColor = color;
              }
              else if (this.strcmpi(str1, "fBehindDocument") == 0)
              {
                int num22;
                if ((num22 = this.ReadRtfShapeParam(rtf, out str2)) > 0)
                  return num22;
                int x = this.ToInt(str2);
                tc.ResetUintFlag(ref group[groupLevel2].shape.FrmFlags, 134217728 /*0x08000000*/);
                if (this.True(x))
                  group[groupLevel2].shape.FrmFlags |= 134217728 /*0x08000000*/;
              }
              else if (this.strcmpi(str1, "posrelv") == 0)
              {
                int num23;
                if ((num23 = this.ReadRtfShapeParam(rtf, out str2)) > 0)
                  return num23;
                int num24 = this.ToInt(str2);
                tc.ResetUintFlag(ref group[groupLevel2].shape.FrmFlags, 268435552 /*0x10000060*/);
                switch (num24)
                {
                  case 0:
                    group[groupLevel2].shape.FrmFlags |= 64 /*0x40*/;
                    continue;
                  case 1:
                    group[groupLevel2].shape.FrmFlags |= 32 /*0x20*/;
                    continue;
                  case 3:
                    group[groupLevel2].shape.FrmFlags |= 268435456 /*0x10000000*/;
                    continue;
                  default:
                    continue;
                }
              }
              else if (this.strcmpi(str1, "posrelh") == 0)
              {
                int num25;
                if ((num25 = this.ReadRtfShapeParam(rtf, out str2)) > 0)
                  return num25;
                int num26 = this.ToInt(str2);
                tc.ResetUintFlag(ref group[groupLevel2].shape.FrmFlags, 1610612737 /*0x60000001*/);
                if (num26 == 1)
                  group[groupLevel2].shape.FrmFlags |= 1;
                if (num26 == 2)
                  group[groupLevel2].shape.FrmFlags |= 1073741824 /*0x40000000*/;
                else if (num26 == 3)
                  group[groupLevel2].shape.FrmFlags |= 536870912 /*0x20000000*/;
              }
            }
            else
            {
              int num27 = this.ProcessRtfControl(rtf);
              if (num27 != 0)
                return num27;
            }
          }
        }
      }
    }
    return 1;
  }

  internal int ReadRtfTag(tc.ClsRtf rtf)
  {
    int num = 0;
    string str1 = "";
    bool flag1 = true;
    bool flag2 = false;
    int groupLevel = rtf.GroupLevel;
    string str2 = "";
    while (this.GetRtfWord(rtf))
    {
      if (!rtf.GroupBegin)
      {
        if (rtf.GroupEnd)
        {
          if (rtf.GroupLevel < groupLevel)
          {
            string str3 = str2.Trim();
            int tagSlot;
            if (str3.Length > 0 && -1 != (tagSlot = this.GetTagSlot()))
            {
              this.e.CharTag[tagSlot].InUse = true;
              this.e.CharTag[tagSlot].type = 0;
              this.e.CharTag[tagSlot].AuxInt = num;
              this.e.CharTag[tagSlot].AuxText = str1;
              this.e.CharTag[tagSlot].name = str3;
              if (this.True(rtf.TagId))
              {
                int tag = rtf.TagId;
                while (this.e.CharTag[tag].next > 0)
                  tag = this.e.CharTag[tag].next;
                this.e.CharTag[tag].next = tagSlot;
                if (this.e.CheckEndlessLoopTags(tag))
                  this.e.CharTag[tag].next = 0;
              }
              else
                rtf.TagId = tagSlot;
            }
            return 0;
          }
        }
        else if (rtf.IsControlWord)
        {
          if (rtf.CurWord == "sstagint")
          {
            num = rtf.IntParam;
            flag1 = false;
            flag2 = true;
          }
        }
        else
        {
          if (flag1 && str2.Length + rtf.CurWord.Length < 1000)
            str2 += rtf.CurWord;
          if (flag2)
            str1 += rtf.CurWord;
        }
      }
    }
    return 1;
  }

  internal int ReadRtfCustomCharTag(tc.ClsRtf rtf, string tagRtfCode)
  {
    int num1 = 0;
    string str1 = "";
    bool flag1 = true;
    bool flag2 = false;
    int groupLevel = rtf.GroupLevel;
    string str2 = "";
    int num2 = 0;
    switch (tagRtfCode)
    {
      case "replchartag":
        num2 = 78;
        break;
      case "areplchartag":
        num2 = 79;
        break;
      case "mreplchartag":
        num2 = 80 /*0x50*/;
        break;
      case "chrfmttag":
        num2 = 81;
        break;
    }
    while (this.GetRtfWord(rtf))
    {
      if (!rtf.GroupBegin)
      {
        if (rtf.GroupEnd)
        {
          if (rtf.GroupLevel < groupLevel)
          {
            string str3 = str2.Trim();
            int tagSlot;
            if (str3.Length > 0 && -1 != (tagSlot = this.GetTagSlot()))
            {
              this.e.CharTag[tagSlot].InUse = true;
              this.e.CharTag[tagSlot].type = num2;
              this.e.CharTag[tagSlot].AuxInt = num1;
              this.e.CharTag[tagSlot].AuxText = str1;
              this.e.CharTag[tagSlot].name = str3;
              if (this.True(rtf.TagId))
              {
                int tag = rtf.TagId;
                while (this.e.CharTag[tag].next > 0)
                  tag = this.e.CharTag[tag].next;
                this.e.CharTag[tag].next = tagSlot;
                if (this.e.CheckEndlessLoopTags(tag))
                  this.e.CharTag[tag].next = 0;
              }
              else
                rtf.TagId = tagSlot;
            }
            return 0;
          }
        }
        else if (rtf.IsControlWord)
        {
          if (rtf.CurWord == "replchartagint" || rtf.CurWord == "areplchartagint" || rtf.CurWord == "mreplchartagint" || rtf.CurWord == "chrfmttagint")
          {
            num1 = rtf.IntParam;
            flag1 = false;
            flag2 = true;
          }
          else if (rtf.CurWord == "u")
          {
            char ch = (char) rtf.IntParam;
            switch (ch)
            {
              case ' ':
                ch = ' ';
                break;
              case '‑':
                ch = '\u0017';
                break;
            }
            if (flag1 && str2.Length + 1 < 1000)
              str2 += ch.ToString();
            if (flag2)
              str1 += ch.ToString();
          }
        }
        else
        {
          if (flag1 && str2.Length + rtf.CurWord.Length < 1000)
            str2 += rtf.CurWord;
          if (flag2)
            str1 += rtf.CurWord;
        }
      }
    }
    return 1;
  }

  internal new string ResolveLinkFileName(string PictFile)
  {
    if ((PictFile.Length <= 1 || PictFile[1] != ':') && (PictFile.Length <= 0 || PictFile[0] != '\\'))
    {
      if (this.e.LinkPictDir.Length > 0)
      {
        PictFile = $"{this.e.LinkPictDir}\\{PictFile}";
        return PictFile;
      }
      string str = "";
      if (this.e.UserDir.Length > 0)
        str = this.e.UserDir;
      else if (this.e.DocName.Length > 0 && this.e.DocName[0] == '\\' || this.e.DocName.Length > 1 && this.e.DocName[1] == ':')
      {
        int length = this.e.DocName.Length;
        string docName = this.e.DocName;
        int num = length - 1;
        while (num > 0 && docName[num] != '\\')
          --num;
        str = docName.Substring(0, num);
      }
      if (str.Length > 0)
      {
        int length = str.Length;
        if (str[length - 1] != '\\')
          str += "\\";
        PictFile = str + PictFile;
      }
    }
    return PictFile;
  }

  internal int RtfCmp(string x, string y) => !x.Equals(y) ? 1 : 0;

  internal bool RtfHdrFtrExists(int sect)
  {
    int firstLine1 = this.e.TerSect1[sect].hdr.FirstLine;
    int lastLine1 = this.e.TerSect1[sect].hdr.LastLine;
    if (firstLine1 >= 0 && firstLine1 < this.e.TotalLines && lastLine1 >= 0 && lastLine1 < this.e.TotalLines && firstLine1 >= this.e.TerSect[sect].FirstLine && (lastLine1 > firstLine1 + 2 || lastLine1 == firstLine1 + 2 && this.e.text[firstLine1 + 1].len > 1))
      return true;
    int firstLine2 = this.e.TerSect1[sect].ftr.FirstLine;
    int lastLine2 = this.e.TerSect1[sect].ftr.LastLine;
    if (firstLine2 >= 0 && firstLine2 < this.e.TotalLines && lastLine2 >= 0 && lastLine2 < this.e.TotalLines && firstLine2 >= this.e.TerSect[sect].FirstLine && (lastLine2 > firstLine2 + 2 || lastLine2 == firstLine2 + 2 && this.e.text[firstLine2 + 1].len > 1))
      return true;
    int firstLine3 = this.e.TerSect1[sect].fhdr.FirstLine;
    int lastLine3 = this.e.TerSect1[sect].fhdr.LastLine;
    if (firstLine3 >= 0 && firstLine3 < this.e.TotalLines && lastLine3 >= 0 && lastLine3 < this.e.TotalLines && firstLine3 >= this.e.TerSect[sect].FirstLine && (lastLine3 > firstLine3 + 2 || lastLine3 == firstLine3 + 2 && this.e.text[firstLine3 + 1].len > 1))
      return true;
    int firstLine4 = this.e.TerSect1[sect].fftr.FirstLine;
    int lastLine4 = this.e.TerSect1[sect].fftr.LastLine;
    return firstLine4 >= 0 && firstLine4 < this.e.TotalLines && lastLine4 >= 0 && lastLine4 < this.e.TotalLines && firstLine4 >= this.e.TerSect[sect].FirstLine && (lastLine4 > firstLine4 + 2 || lastLine4 == firstLine4 + 2 && this.e.text[firstLine4 + 1].len > 1);
  }

  internal new bool RtfRead(int input, string InFile, string StrBuf, int BufLen)
  {
    FileStream fileStream = (FileStream) null;
    Cursor x = (Cursor) null;
    if (tc.DebugMode)
      this.misc.dm(nameof (RtfRead));
    this.e.RtfInput = input;
    this.e.FootnoteRest = "";
    this.e.FootnoteRestFont = 0;
    if (input == 0)
    {
      InFile = InFile.Trim();
      if (InFile.Length == 0)
        return false;
      if (!File.Exists(InFile))
        ;
      try
      {
        fileStream = new FileStream(InFile, FileMode.Open, FileAccess.Read);
      }
      catch (IOException ex)
      {
        return this.PrintError(28, nameof (RtfRead));
      }
    }
    for (int index = 0; index < this.e.TotalTableRows; ++index)
    {
      if (this.e.TableRow[index].InUse)
        this.e.TableRow[index].flags |= 16384 /*0x4000*/;
      tc.ResetUintFlag(ref this.e.TableAux[index].flags, 55);
    }
    for (int index = 0; index < this.e.TotalCells; ++index)
    {
      if (this.e.cell[index].InUse)
      {
        this.e.cell[index].flags |= 1024 /*0x0400*/;
        tc.ResetUintFlag(ref this.e.CellAux[index].flags, 8);
      }
    }
    for (int index = 0; index < this.e.TotalReviewers; ++index)
      this.e.reviewer[index].RtfId = index;
    tc.ClsRtf clsRtf = new tc.ClsRtf();
    clsRtf.InitArray();
    int section = this.GetSection(this.e.CurLine);
    clsRtf.PaperWidth = (int) this.InchesToTwips((double) this.e.TerSect1[section].PgWidth);
    clsRtf.PaperHeight = (int) this.InchesToTwips((double) this.e.TerSect1[section].PgHeight);
    clsRtf.InitTblCol = -1;
    clsRtf.CurTabType = 0;
    clsRtf.lang = this.e.DefLang;
    if (this.e.TotalLines == 1 && this.e.text[0].len <= 1)
      clsRtf.EmptyDoc = true;
    if (input == 0)
    {
      clsRtf.iFile = fileStream;
    }
    else
    {
      clsRtf.buf = StrBuf.ToCharArray();
      clsRtf.BufLen = BufLen;
      clsRtf.iFile = (FileStream) null;
    }
    tc.StrRtfGroup[] strRtfGroupArray = clsRtf.group = new tc.StrRtfGroup[50];
    for (int index = 0; index < 50; ++index)
    {
      strRtfGroupArray[index] = new tc.StrRtfGroup();
      strRtfGroupArray[index].init();
    }
    strRtfGroupArray[0].rtf = clsRtf;
    strRtfGroupArray[0].CharStyId = 1;
    strRtfGroupArray[0].CharSet = 1;
    strRtfGroupArray[0].CellPatBC = tc.CLR_WHITE;
    strRtfGroupArray[0].ParaBkColor = tc.CLR_WHITE;
    strRtfGroupArray[0].CharPatBC = tc.CLR_WHITE;
    strRtfGroupArray[0].shape.WrapType = 1;
    strRtfGroupArray[0].shape.WrapSide = 0;
    strRtfGroupArray[0].shape.type = -1;
    strRtfGroupArray[0].UcIgnoreCount = 1;
    strRtfGroupArray[0].TextBoxMargin = -1;
    strRtfGroupArray[0].OutlineLevel = -1;
    strRtfGroupArray[0].ParaBorderColor = tc.CLR_AUTO;
    for (int index = 0; index < 19; ++index)
      strRtfGroupArray[0].BorderColor[index] = tc.CLR_AUTO;
    strRtfGroupArray[0].blt = this.e.TerBlt[0];
    this.e.RtfCurRowId = this.e.RtfCurCellId = this.e.RtfLastCellX = this.e.RtfParaFID = 0;
    this.e.RtfInEquation = false;
    this.e.RtfParaFrameInfo = new tc.StrRtfParaFrameInfo();
    this.e.FirstFreeCellId = 0;
    bool paintEnabled = this.e.PaintEnabled;
    this.e.PaintEnabled = false;
    bool flag = false;
    if (this.GetRtfWord(clsRtf) && !this.False(clsRtf.GroupBegin) && this.GetRtfWord(clsRtf) && !this.False(clsRtf.IsControlWord))
    {
      clsRtf.CurWord = clsRtf.CurWord.ToUpper();
      if (this.strcmpi("RTF", clsRtf.CurWord) == 0)
      {
        this.InitRtfGroup(clsRtf, clsRtf.GroupLevel);
        if ((this.e.TerFlags & 512 /*0x0200*/) == 0)
        {
          x = this.e.Cursor;
          this.e.Cursor = Cursors.WaitCursor;
        }
        if (this.ImportRtfData(1, ref strRtfGroupArray[clsRtf.GroupLevel], (object) null, tc.SkipRtfPict, tc.SkipRtfObject))
        {
          clsRtf.sect = this.e.TerSect[clsRtf.InitSect].Copy();
          if ((this.e.TerFlags3 & 32768 /*0x8000*/) != 0)
            clsRtf.sect.IsPortrait = true;
          for (int index = 0; index < clsRtf.GroupLevel; ++index)
          {
            strRtfGroupArray[index].flags = strRtfGroupArray[clsRtf.GroupLevel].flags;
            strRtfGroupArray[index].FrmFlags = strRtfGroupArray[clsRtf.GroupLevel].FrmFlags;
            strRtfGroupArray[index].level = strRtfGroupArray[clsRtf.GroupLevel].level;
            strRtfGroupArray[index].ParaFrameInfo = strRtfGroupArray[clsRtf.GroupLevel].ParaFrameInfo;
          }
          clsRtf.InitialCell = this.e.RtfCurCellId;
          clsRtf.InitialParaFID = this.e.RtfParaFID;
          flag = true;
          while (this.GetRtfWord(clsRtf))
          {
            if (clsRtf.IsControlWord)
            {
              switch (this.ProcessRtfControl(clsRtf))
              {
                case 1:
                  this.PrintError(39, $"pos: {clsRtf.FilePos}, control: {clsRtf.IsControlWord}, beg group: {clsRtf.GroupBegin}, end group: {clsRtf.GroupEnd}, word: {clsRtf.CurWord}, word len: {clsRtf.WordLen}, param: {clsRtf.IntParam}");
                  goto label_52;
                case 2:
                  this.PrintError(124, $"pos: {clsRtf.FilePos}, control: {clsRtf.IsControlWord}, beg group: {clsRtf.GroupBegin}, end group: {clsRtf.GroupEnd}, word: {clsRtf.CurWord}, word len: {clsRtf.WordLen}, param: {clsRtf.IntParam}");
                  goto label_52;
                case 3:
                  goto label_52;
                default:
                  if (clsRtf.SuspendReading)
                    goto label_52;
                  continue;
              }
            }
            else if (clsRtf.WordLen > 0 && !this.CopyToOutBuf(clsRtf))
              break;
          }
        }
      }
    }
label_52:
    this.SendRtfText(clsRtf);
    if (clsRtf.OpenRowId > 0)
      this.DeleteRtfRow(clsRtf, clsRtf.OpenRowId);
    if (flag)
      this.ImportRtfData(2, ref strRtfGroupArray[0], (object) clsRtf, tc.SkipRtfPict, tc.SkipRtfObject);
    else
      this.e.InRtfRead = false;
    if (this.True(x))
      this.e.Cursor = x;
    fileStream?.Close();
    if (this.e.RtfInput == 0 && (this.e.TerFlags5 & 2) != 0 && this.e.Parent != null)
      this.e.Parent.Text = InFile;
    this.e.PaintEnabled = paintEnabled;
    return flag;
  }

  internal bool RtfTextToUnicode(
    tc.ClsRtf rtf,
    string text,
    out string UniText,
    ref tc.StrRtfGroup pGroup)
  {
    int pCodePage = 0;
    int num1 = 0;
    rtf.HasIncompleteAsianChar = false;
    int CharSet = pGroup.TypeFaceDB == null || pGroup.TypeFaceDB.Length <= 0 || pGroup.CharType != 3 ? pGroup.CharSet : pGroup.CharSetDB;
    UniText = "";
    if (CharSet == 2)
    {
      UniText = text;
      return false;
    }
    this.IsMbcsCharSet(CharSet, out pCodePage);
    if (CharSet == 77)
      pCodePage = 10000;
    int length1 = text.Length;
    byte[] InChr = new byte[length1];
    for (int index = 0; index < length1; ++index)
    {
      ushort num2 = (ushort) text[index];
      if (num2 > (ushort) byte.MaxValue)
        return false;
      InChr[index] = (byte) num2;
    }
    if (length1 > 0)
      num1 = (int) text[length1 - 1];
    int num3 = this.True(this.MultiByteToWideChar(pCodePage, InChr, out UniText)) ? 1 : 0;
    if (CharSet != 134 && CharSet != 136 && CharSet != 128 /*0x80*/ && CharSet != 129)
      return num3 != 0;
    if (UniText.Length <= 0)
      return num3 != 0;
    int length2 = UniText.Length;
    int num4 = (int) UniText[length2 - 1];
    if (num4 == 0)
    {
      UniText = UniText.Substring(0, length2 - 1);
      rtf.HasIncompleteAsianChar = true;
      return num3 != 0;
    }
    if (UniText[length2 - 1] != '?')
      return num3 != 0;
    if (num4 == num1)
      return num3 != 0;
    UniText = UniText.Substring(0, length2 - 1);
    rtf.HasIncompleteAsianChar = true;
    return num3 != 0;
  }

  internal bool SaveRtfLevelInfo(tc.ClsRtf rtf, int level)
  {
    rtf.TblLevel[level].CurRowId = this.e.RtfCurRowId;
    rtf.TblLevel[level].CurCellId = this.e.RtfCurCellId;
    rtf.TblLevel[level].LastCellX = this.e.RtfLastCellX;
    rtf.TblLevel[level].OpenRowId = rtf.OpenRowId;
    rtf.TblLevel[level].OpenCellId = rtf.OpenCellId;
    rtf.TblLevel[level].OpenLastCellX = rtf.OpenLastCellX;
    rtf.TblLevel[level].InitTblCol = rtf.InitTblCol;
    rtf.TblLevel[level].PastingColumn = rtf.PastingColumn;
    return true;
  }

  internal bool SendRtfText(tc.ClsRtf rtf)
  {
    tc.StrRtfGroup strRtfGroup = rtf.group[rtf.GroupLevel];
    if (rtf.OutBufLen == 0)
      return true;
    if (rtf.GroupLevel == 0)
    {
      rtf.OutBufLen = 0;
      rtf.OutBuf = "";
      return true;
    }
    if (rtf.InitFieldId > 0 && (rtf.group[rtf.GroupLevel].FieldId == 6 || rtf.group[rtf.GroupLevel].FieldId == 2))
    {
      rtf.OutBufLen = 0;
      rtf.OutBuf = "";
      return true;
    }
    if ((rtf.group[rtf.GroupLevel].gflags & 32 /*0x20*/) != 0)
      return true;
    if ((strRtfGroup.gflags & 32768 /*0x8000*/) != 0 && rtf.pict >= 0 && rtf.pict < this.e.TotalFonts && this.e.TerFont[rtf.pict].InUse && (this.e.TerFont[rtf.pict].style & 128 /*0x80*/) != 0)
    {
      this.e.TerFont[rtf.pict].form.InitText += rtf.OutBuf;
      rtf.OutBufLen = 0;
      rtf.OutBuf = "";
      return true;
    }
    if (rtf.group[rtf.GroupLevel].TypeFace.Length == 0)
      this.GetRtfDefaultFont(rtf);
    bool flag = rtf.OutBufLen != 1 || rtf.OutBuf[0] != '\u0014' ? this.ImportRtfData(4, ref rtf.group[rtf.GroupLevel], (object) rtf.OutBuf, tc.SkipRtfPict, tc.SkipRtfObject) : this.ImportRtfData(7, ref rtf.group[rtf.GroupLevel], (object) rtf, tc.SkipRtfPict, tc.SkipRtfObject);
    if (rtf.OutBufLen > 0)
      rtf.PrevChar = rtf.OutBuf[rtf.OutBufLen - 1];
    rtf.OutBuf = "";
    rtf.OutBufLen = 0;
    rtf.OutBufHasUnicode = false;
    rtf.flags |= 32 /*0x20*/;
    rtf.flags1 |= 2;
    return flag;
  }

  internal bool SetRtfDocPaperSize(tc.ClsRtf rtf)
  {
    PaperKind PprKind;
    int num = this.SetRtfPaperSize(rtf, rtf.PaperWidth, rtf.PaperHeight, out PprKind) ? 1 : 0;
    this.e.PprKind = PprKind;
    return num != 0;
  }

  internal bool SetRtfFontDefault(tc.ClsRtf rtf, tc.StrRtfGroup[] group)
  {
    int groupLevel = rtf.GroupLevel;
    group[groupLevel].TypeFace = "";
    group[groupLevel].PointSize2 = 0;
    group[groupLevel].TextColor = this.e.TerFont[0].TextColor;
    if (this.e.RtfInput < 2 && group[groupLevel].TextColor == Color.Black)
      group[groupLevel].TextColor = tc.CLR_AUTO;
    group[groupLevel].TextBkColor = tc.CLR_WHITE;
    group[groupLevel].UlineColor = tc.CLR_AUTO;
    group[groupLevel].style &= 39936;
    group[groupLevel].StyleOff = 0;
    group[groupLevel].CharStyId = 1;
    group[groupLevel].CharSet = 1;
    group[groupLevel].CharType = 0;
    group[groupLevel].TypeFaceDB = "";
    group[groupLevel].FontFamilyDB = "";
    group[groupLevel].CharSetDB = 0;
    group[groupLevel].TypeFaceHi = "";
    group[groupLevel].FontFamilyHi = "";
    group[groupLevel].CharSetHi = 0;
    group[groupLevel].AuxId = 0;
    group[groupLevel].lang = rtf.lang;
    group[groupLevel].offset = 0;
    group[groupLevel].expand = 0;
    group[groupLevel].CharScaleX = 0;
    group[groupLevel].caps = false;
    tc.ResetUintFlag(ref group[groupLevel].gflags, 4096 /*0x1000*/);
    group[groupLevel].CharBkPat = 0;
    group[groupLevel].CharPatFC = group[groupLevel].TextColor;
    group[groupLevel].CharPatBC = group[groupLevel].TextBkColor;
    if (this.e.RtfInput < 2 || !this.e.TrackChanges)
    {
      group[groupLevel].revised = false;
      group[groupLevel].deleted = false;
    }
    return true;
  }

  internal bool SetRtfFootnote(ref tc.StrRtfGroup pGroup, int line)
  {
    if (this.e.text[line].len != 0)
    {
      int len = this.e.text[line].len;
      ushort[] numArray = this.OpenCfmt(line);
      int index1 = (int) numArray[len - 1];
      if ((this.e.TerFont[index1].style & 39936) == 0)
      {
        this.e.InputFontId = index1;
        this.e.HilightType = 0;
        this.e.SetTerCharStyle(1024 /*0x0400*/, true, false);
        for (int index2 = 0; index2 < len; ++index2)
          numArray[index2] = (ushort) this.e.InputFontId;
        this.e.FootnoteRestFont = this.e.InputFontId;
        this.e.InputFontId = -1;
        if (pGroup.EndnoteMarker != char.MinValue && len == 1)
          this.e.text[line].txt[0] = pGroup.EndnoteMarker;
      }
      this.CloseCfmt(line);
      pGroup.EndnoteMarker = char.MinValue;
    }
    return true;
  }

  internal bool SetRtfLineOrient(tc.ClsRtf rtf, bool FlipH, bool FlipV)
  {
    if (Math.Abs(this.e.ParaFrame[this.e.RtfParaFID].width) <= 1)
      this.e.ParaFrame[this.e.RtfParaFID].width = 0;
    if (Math.Abs(this.e.ParaFrame[this.e.RtfParaFID].height) <= 1)
      this.e.ParaFrame[this.e.RtfParaFID].height = 0;
    int x = this.e.ParaFrame[this.e.RtfParaFID].x;
    int x2 = x + this.e.ParaFrame[this.e.RtfParaFID].width;
    int paraY = this.e.ParaFrame[this.e.RtfParaFID].ParaY;
    int y2 = paraY + this.e.ParaFrame[this.e.RtfParaFID].height;
    if (y2 < paraY)
    {
      this.e.ParaFrame[this.e.RtfParaFID].ParaY = y2;
      this.e.ParaFrame[this.e.RtfParaFID].height = paraY - y2;
    }
    this.LinePointsToRect(this.e.RtfParaFID, x, paraY, x2, y2);
    if ((this.e.ParaFrame[this.e.RtfParaFID].flags & 256 /*0x0100*/) != 0 && FlipH | FlipV)
    {
      int num = this.e.ParaFrame[this.e.RtfParaFID].LineType;
      if (FlipH)
      {
        switch (num)
        {
          case 2:
            num = 3;
            break;
          case 3:
            num = 2;
            break;
        }
      }
      if (FlipV)
      {
        switch (num)
        {
          case 2:
            num = 3;
            break;
          case 3:
            num = 2;
            break;
        }
      }
      this.e.ParaFrame[this.e.RtfParaFID].LineType = num;
    }
    return true;
  }

  internal bool SetRtfPaperSize(tc.ClsRtf rtf, int width, int height, out PaperKind PprKind)
  {
    PprKind = PaperKind.Custom;
    if (Math.Abs(width - 12240) < 10 && Math.Abs(height - 15840) < 10)
      PprKind = PaperKind.Letter;
    else if (Math.Abs(width - 15840) < 10 && Math.Abs(height - 12240) < 10)
      PprKind = PaperKind.Letter;
    else if (Math.Abs(width - 12240) < 10 && Math.Abs(height - 20160) < 10)
      PprKind = PaperKind.Legal;
    else if (Math.Abs(width - 20160) < 10 && Math.Abs(height - 12240) < 10)
      PprKind = PaperKind.Legal;
    else if (Math.Abs(width - 11909) < 10 && Math.Abs(height - 16834) < 10)
      PprKind = PaperKind.A4;
    else if (Math.Abs(width - 16834) < 10 && Math.Abs(height - 11909) < 10)
      PprKind = PaperKind.A4;
    else if (Math.Abs(width - 24480) < 10 && Math.Abs(height - 15840) < 10)
      PprKind = PaperKind.Tabloid;
    else if (Math.Abs(width - 15840) < 10 && Math.Abs(height - 24480) < 10)
      PprKind = PaperKind.Tabloid;
    else if (Math.Abs(width - 5940) < 10 && Math.Abs(height - 13680) < 10)
      PprKind = PaperKind.Number10Envelope;
    else if (Math.Abs(width - 13680) < 10 && Math.Abs(height - 5940) < 10)
      PprKind = PaperKind.Number10Envelope;
    else if (width > 0 && height > 0)
    {
      PprKind = PaperKind.Custom;
      rtf.ApplyPaperSize = true;
    }
    return true;
  }

  internal bool SetRtfParaBorders(ref tc.StrRtfGroup group, int BorderType)
  {
    if (BorderType == 12)
      group.flags |= 240 /*0xF0*/;
    if (BorderType == 2)
      group.flags |= 64 /*0x40*/;
    if (BorderType == 3)
      group.flags |= 128 /*0x80*/;
    if (BorderType == 0)
      group.flags |= 16 /*0x10*/;
    if (BorderType == 1)
      group.flags |= 32 /*0x20*/;
    if (BorderType == 18)
      group.flags |= 65536 /*0x010000*/;
    return true;
  }

  internal bool SetRtfParaDefault(tc.ClsRtf rtf, tc.StrRtfGroup[] group)
  {
    int groupLevel = rtf.GroupLevel;
    bool flag = (group[groupLevel].style & 39936) != 0;
    if (rtf.InitialCell == 0 && !flag)
      group[groupLevel].InTable = false;
    if (!group[groupLevel].InTable)
      rtf.CellFlow = 0;
    group[groupLevel].LeftIndent = 0;
    group[groupLevel].RightIndent = 0;
    group[groupLevel].FirstIndent = 0;
    group[groupLevel].flags = this.e.RtfInHdrFtr;
    group[groupLevel].ParaBorderColor = tc.CLR_AUTO;
    group[groupLevel].flow = 0;
    group[groupLevel].tab.count = 0;
    group[groupLevel].BorderMargin = 0;
    group[groupLevel].ParShading = 0;
    group[groupLevel].CellShading = 0;
    group[groupLevel].CellPatBC = tc.CLR_WHITE;
    group[groupLevel].CellPatFC = Color.Black;
    group[groupLevel].CellColSpan = 1;
    group[groupLevel].SpaceBefore = 0;
    group[groupLevel].SpaceAfter = 0;
    group[groupLevel].SpaceBetween = 0;
    group[groupLevel].LineSpacing = 0;
    group[groupLevel].ParaBkColor = tc.CLR_WHITE;
    group[groupLevel].RtfLs = 0;
    group[groupLevel].ListLvl = 0;
    if (!flag)
      group[groupLevel].ParaStyId = 0;
    if ((rtf.flags1 & 128 /*0x80*/) == 0 && !flag)
      group[groupLevel].level = this.e.RtfInitLevel;
    int num = 65520;
    this.True(group[groupLevel].pflags &= ~num);
    if (rtf.SetWidowOrphan)
      group[groupLevel].pflags |= 32 /*0x20*/;
    if (rtf.InitialParaFID == 0 && (group[groupLevel].gflags & 33554436 /*0x02000004*/) == 0 && !flag)
    {
      group[groupLevel].ParaFrameInfo = new tc.StrRtfParaFrameInfo();
      group[groupLevel].FrmFlags = 0;
      group[groupLevel].TextBoxMargin = 0;
      group[groupLevel].ParaFID = 0;
      group[groupLevel].TextAngle = 0;
      tc.ResetUintFlag(ref group[groupLevel].gflags, 192 /*0xC0*/);
    }
    for (int index = 0; index < 19; ++index)
    {
      group[groupLevel].BorderWidth[index] = 0;
      group[groupLevel].BorderColor[index] = tc.CLR_AUTO;
    }
    return true;
  }

  internal bool SetRtfParaFID(int line, ref tc.StrRtfGroup group)
  {
    int num1 = 0;
    bool flag1 = false;
    tc.ClsRtf rtf = group.rtf;
    if ((group.gflags2 & 1) == 0)
    {
      if ((group.gflags & 256 /*0x0100*/) != 0 || (group.gflags & 8388608 /*0x800000*/) != 0)
      {
        if (group.shape.WrapType == 1)
          group.FrmFlags |= 8192 /*0x2000*/;
        else if (group.shape.WrapType == 3)
          group.FrmFlags |= 16384 /*0x4000*/;
        else
          tc.ResetUintFlag(ref group.FrmFlags, 24576 /*0x6000*/);
        if ((group.shape.WrapType == 3 || group.shape.WrapType == 5) && (group.FrmFlags & 896) == 0)
        {
          group.gflags |= 1;
          group.FrmFlags |= 128 /*0x80*/;
          group.FrmFlags |= 131072 /*0x020000*/;
          if (group.shape.WrapType == 3 && group.shape.ZOrder == 0)
          {
            int num2;
            group.shape.ZOrder = num2 = -1;
            group.ParaFrameInfo.ZOrder = num2;
          }
        }
        group.FrmFlags |= 2;
        if ((group.gflags & 8388608 /*0x800000*/) != 0)
        {
          group.ParaFrameInfo = new tc.StrRtfParaFrameInfo();
          group.ParaFrameInfo.x = group.shape.x;
          int hpageGroup = group.HPageGroup;
          rtf.group[hpageGroup].ParaFrameInfo.x = group.shape.x;
          group.ParaFrameInfo.y = group.shape.y;
          group.ParaFrameInfo.width = group.shape.width;
          group.ParaFrameInfo.height = group.shape.height;
          group.ParaFrameInfo.ZOrder = group.shape.ZOrder;
          group.ParaFrameInfo.DistFromText = group.shape.DistFromText;
        }
      }
      else if ((group.FrmFlags & 896) != 0)
        group.FrmFlags |= 16384 /*0x4000*/;
      if ((group.FrmFlags & 2) != 0 && this.e.RtfParaFID == 0)
        rtf.flags |= 4096 /*0x1000*/;
      tc.StrRtfParaFrameInfo paraFrameInfo = group.ParaFrameInfo;
      if ((group.FrmFlags & 2) != 0 && (group.gflags & 1) == 0)
      {
        int twips = (int) this.InchesToTwips((double) ((rtf.sect.IsPortrait ? rtf.sect.PprWidth : rtf.sect.PprHeight) - rtf.sect.LeftMargin - rtf.sect.RightMargin));
        if ((group.FrmFlags & 1) != 0)
        {
          int groupLevel = rtf.GroupLevel;
          int hpageGroup = rtf.group[groupLevel].HPageGroup;
          num1 = rtf.group[hpageGroup].ParaFrameInfo.x;
          flag1 = true;
          paraFrameInfo.x = rtf.group[hpageGroup].ParaFrameInfo.x - (int) this.InchesToTwips((double) rtf.sect.LeftMargin);
        }
        if (paraFrameInfo.width == 0 && (group.FrmFlags & 256 /*0x0100*/) == 0)
        {
          paraFrameInfo.width = (group.FrmFlags & 12) == 0 ? twips + (int) this.InchesToTwips((double) rtf.sect.RightMargin) - paraFrameInfo.x : twips;
          if (paraFrameInfo.width < 360)
            paraFrameInfo.width = 360;
          group.FrmFlags |= 16 /*0x10*/;
        }
        if ((group.FrmFlags & 12) != 0)
        {
          paraFrameInfo.x = twips - paraFrameInfo.width;
          if (paraFrameInfo.width <= 0)
            paraFrameInfo.x = twips;
          if ((group.FrmFlags & 8) != 0)
            paraFrameInfo.x /= 2;
        }
      }
      if ((!this.IsSameRtfParaFrame(paraFrameInfo, this.e.RtfParaFrameInfo) || (rtf.flags & 64 /*0x40*/) != 0) && (rtf.InitialParaFID == 0 || line == rtf.FirstLine))
      {
        this.e.RtfParaFID = 0;
        rtf.InitialParaFID = 0;
        this.e.PrevRtfParaFrameInfo = this.e.RtfParaFrameInfo;
        this.e.RtfParaFrameInfo = paraFrameInfo;
        rtf.flags = tc.ResetUintFlag(ref rtf.flags, 64 /*0x40*/);
      }
      if ((group.FrmFlags & 2) == 0)
        this.e.RtfParaFID = 0;
      if (this.False(this.e.RtfParaFID) && (group.FrmFlags & 2) != 0 && (this.e.RtfParaFID = this.GetParaFrameSlot()) > 0)
      {
        this.e.ParaFrame[this.e.RtfParaFID].InUse = true;
        this.e.ParaFrame[this.e.RtfParaFID].x = this.e.RtfParaFrameInfo.x;
        this.e.ParaFrame[this.e.RtfParaFID].OrgX = flag1 ? num1 : this.e.RtfParaFrameInfo.x;
        this.e.ParaFrame[this.e.RtfParaFID].GroupX = this.e.RtfParaFrameInfo.x;
        this.e.ParaFrame[this.e.RtfParaFID].ParaY = this.e.RtfParaFrameInfo.y;
        this.e.ParaFrame[this.e.RtfParaFID].GroupY = this.e.RtfParaFrameInfo.y;
        this.e.ParaFrame[this.e.RtfParaFID].OrgY = this.e.RtfParaFrameInfo.y;
        this.e.ParaFrame[this.e.RtfParaFID].width = this.e.RtfParaFrameInfo.width;
        this.e.ParaFrame[this.e.RtfParaFID].height = this.e.RtfParaFrameInfo.height;
        this.e.ParaFrame[this.e.RtfParaFID].MinHeight = this.e.RtfParaFrameInfo.height;
        this.e.ParaFrame[this.e.RtfParaFID].ZOrder = this.e.RtfParaFrameInfo.ZOrder;
        this.e.ParaFrame[this.e.RtfParaFID].DistFromText = this.e.RtfParaFrameInfo.DistFromText;
        this.e.ParaFrame[this.e.RtfParaFID].BackColor = Color.White;
        this.e.ParaFrame[this.e.RtfParaFID].flags = group.FrmFlags;
        this.e.ParaFrame[this.e.RtfParaFID].TextLine = line;
        this.e.ParaFrame[this.e.RtfParaFID].LineColor = Color.Black;
        this.e.ParaFrame[this.e.RtfParaFID].TextAngle = group.TextAngle;
        if ((group.gflags & 8388609 /*0x800001*/) != 0)
        {
          if ((this.e.ParaFrame[this.e.RtfParaFID].flags & 128 /*0x80*/) != 0)
            this.e.ParaFrame[this.e.RtfParaFID].margin = group.TextBoxMargin == -1 ? 0 : group.TextBoxMargin;
          else
            this.e.ParaFrame[this.e.RtfParaFID].margin = 0;
        }
        else
          this.e.ParaFrame[this.e.RtfParaFID].margin = group.BorderMargin;
        if ((group.gflags & 8388608 /*0x800000*/) != 0)
        {
          this.e.ParaFrame[this.e.RtfParaFID].flags |= group.shape.FrmFlags;
          this.e.ParaFrame[this.e.RtfParaFID].LineWdth = group.shape.LineWdth;
          this.e.ParaFrame[this.e.RtfParaFID].LineColor = group.shape.LineColor;
          this.e.ParaFrame[this.e.RtfParaFID].BackColor = group.shape.BackColor;
          this.e.ParaFrame[this.e.RtfParaFID].FillPattern = group.shape.FillPattern;
        }
      }
    }
    group.ParaFID = this.e.RtfParaFID;
    if (this.e.RtfCurCellId > 0)
    {
      bool flag2 = false;
      if (this.e.CurLine > 0 && this.e.text[this.e.CurLine - 1].cid > 0 && this.e.text[this.e.CurLine - 1].fid == 0)
        flag2 = true;
      if (flag2 && this.e.ParaFrame[this.e.RtfParaFID].pict == 0)
        this.e.RtfParaFID = group.ParaFID = 0;
    }
    return true;
  }

  internal bool SetRtfParaId(int line, ref tc.StrRtfGroup group)
  {
    tc.ClsRtf rtf = group.rtf;
    int num1 = this.NewTabId(0, group.tab);
    if (group.RtfLs > 0)
    {
      group.flags |= 8;
      tc.StrBlt strBlt = new tc.StrBlt();
      group.blt = strBlt.init();
      group.blt.NumberType = 5;
      if (rtf.XlateLs[group.RtfLs] == 0)
        rtf.XlateLs[group.RtfLs] = this.MakeRtfHiddenList(rtf);
    }
    group.blt.ls = rtf.XlateLs[group.RtfLs];
    group.blt.lvl = group.ListLvl;
    int num2 = this.NewBltId(0, group.blt);
    this.e.text[line].tabw = (tc.ClsTabw) null;
    this.e.text[line].fid = this.e.RtfParaFID;
    if (this.e.RtfParaFID == 0)
      rtf.TableInFrame = false;
    int pflags = group.pflags;
    if (this.True(this.e.RtfParaFID))
      pflags |= 1;
    if (this.True(this.e.text[line].cid))
      pflags |= 2;
    int num3 = group.BorderMargin == 0 ? 20 : group.BorderMargin;
    tc.StrPfmt pNew = new tc.StrPfmt();
    pNew.LeftIndentTwips = group.LeftIndent;
    pNew.RightIndentTwips = group.RightIndent;
    pNew.FirstIndentTwips = group.FirstIndent;
    pNew.TabId = num1;
    pNew.BltId = num2;
    pNew.StyId = group.ParaStyId;
    pNew.shading = group.ParShading;
    pNew.pflags = pflags;
    pNew.SpaceBefore = group.SpaceBefore;
    pNew.SpaceAfter = group.SpaceAfter;
    pNew.SpaceBetween = group.SpaceBetween;
    pNew.LineSpacing = group.LineSpacing;
    pNew.BkColor = group.ParaBkColor;
    pNew.BorderColor = group.ParaBorderColor;
    pNew.BorderSpace = num3;
    pNew.flow = group.flow;
    pNew.flags = group.flags;
    if (pNew.flags == 0)
      pNew.flags = this.e.StyleId[0].ParaFlags;
    this.e.text[line].pfmt = this.NewParaId2(0, pNew);
    return true;
  }

  internal bool SetRtfRowDefault(tc.ClsRtf rtf, tc.StrRtfGroup[] group, int CurGroup)
  {
    if ((rtf.flags & 24) == 0)
    {
      if (rtf.InitialCell > 0 && !rtf.PastingColumn && this.e.cell[rtf.InitialCell].level == (this.e.RtfCurCellId > 0 ? this.e.cell[this.e.RtfCurCellId].level : 0))
      {
        this.e.RtfCurCellId = this.e.RtfCurRowId = 0;
        group[CurGroup].InTable = false;
      }
      if (rtf.OpenRowId > 0)
        this.DeleteRtfRow(rtf, rtf.OpenRowId);
      rtf.OpenRowId = 0;
      if ((this.e.TableAux[this.e.RtfCurRowId].flags & 1) != 0)
      {
        int index1 = this.e.TableRow[this.e.RtfCurRowId].FirstCell;
        while (index1 > 0 && (this.e.cell[index1].flags & 1024 /*0x0400*/) != 0)
          index1 = this.e.cell[index1].NextCell;
        if (index1 <= 0 || this.e.cell[index1].PrevCell <= 0)
          this.e.RtfLastCellX = 0;
        else
          this.e.RtfLastCellX = this.e.cell[index1].PrevCell;
        for (int index2 = 0; index2 < 19; ++index2)
        {
          group[CurGroup].BorderWidth[index2] = 0;
          group[CurGroup].BorderColor[index2] = tc.CLR_AUTO;
        }
        group[CurGroup].CellShading = 0;
        group[CurGroup].CellPatBC = tc.CLR_WHITE;
        group[CurGroup].CellPatFC = Color.Black;
        group[CurGroup].CellColSpan = 1;
        group[CurGroup].TextAngle = 0;
        this.e.TableAux[this.e.RtfCurRowId].flags |= 2;
        return true;
      }
      int index3 = this.e.RtfCurRowId;
      if (index3 > 0)
      {
        if ((this.e.TableAux[index3].flags & 16 /*0x10*/) != 0)
        {
          this.e.TableAux[index3].flags |= 2;
        }
        else
        {
          for (int CurCell = this.e.TableRow[index3].FirstCell; CurCell > 0; CurCell = this.e.cell[CurCell].NextCell)
          {
            if ((this.e.cell[CurCell].flags & 1024 /*0x0400*/) == 0)
              this.RemoveCell(CurCell);
          }
        }
        if (this.e.TableRow[index3].FirstCell <= 0)
        {
          int prevRow = this.e.TableRow[index3].PrevRow;
          int nextRow = this.e.TableRow[index3].NextRow;
          this.e.TableRow[index3] = new tc.StrTableRow();
          this.e.TableRow[index3].init();
          this.e.TableAux[index3] = new tc.StrTableAux();
          this.e.TableRow[index3].InUse = true;
          this.e.TableRow[index3].PrevRow = prevRow;
          this.e.TableRow[index3].NextRow = nextRow;
          this.e.TableRow[index3].flags = tc.ResetUintFlag(ref this.e.TableRow[index3].flags, 16384 /*0x4000*/);
          this.e.TableRow[index3].CellMargin = 0;
        }
      }
      else if ((index3 = this.GetTableRowSlot()) > 0)
      {
        this.e.TableRow[index3].InUse = true;
        int rtfCurRowId = this.e.RtfCurRowId;
        this.e.RtfCurRowId = index3;
        if (rtfCurRowId > 0)
        {
          this.e.TableRow[rtfCurRowId].NextRow = index3;
          this.e.TableRow[index3].PrevRow = rtfCurRowId;
        }
        else
          this.e.TableRow[index3].PrevRow = -1;
        this.e.TableRow[index3].FirstCell = -1;
        this.e.TableRow[index3].NextRow = -1;
        this.e.TableRow[index3].CellMargin = 0;
        if ((rtf.flags1 & 64 /*0x40*/) != 0)
        {
          this.e.TableRow[index3].flags |= 32768 /*0x8000*/;
          tc.ResetUintFlag(ref rtf.flags1, 64 /*0x40*/);
        }
      }
      int num = !rtf.PastingColumn || rtf.InitTblCol < 0 ? 0 : ((this.e.TableRow[index3].flags & 16384 /*0x4000*/) != 0 ? 1 : 0);
      if ((this.e.TableAux[index3].flags & 16 /*0x10*/) == 0)
        this.e.RtfCurCellId = 0;
      this.e.RtfLastCellX = 0;
      if (num != 0)
      {
        int initTblCol = rtf.InitTblCol;
        if (initTblCol > 0)
        {
          for (this.e.RtfLastCellX = this.e.TableRow[index3].FirstCell; initTblCol > 1 && this.e.cell[this.e.RtfLastCellX].NextCell > 0; this.e.RtfLastCellX = this.e.cell[this.e.RtfLastCellX].NextCell)
            initTblCol -= this.e.cell[this.e.RtfLastCellX].ColSpan;
        }
      }
      for (int index4 = 0; index4 < 19; ++index4)
      {
        group[CurGroup].BorderWidth[index4] = 0;
        group[CurGroup].BorderColor[index4] = tc.CLR_AUTO;
      }
      group[CurGroup].CellShading = 0;
      group[CurGroup].CellPatBC = tc.CLR_WHITE;
      group[CurGroup].CellPatFC = Color.Black;
      group[CurGroup].CellColSpan = 1;
      group[CurGroup].TextAngle = 0;
      this.e.TableAux[this.e.RtfCurRowId].flags |= 1;
      rtf.TableRead = true;
    }
    return true;
  }

  internal bool SetRtfSectBorders(ref tc.StrRtfGroup group, int BorderType)
  {
    int index = -1;
    tc.ClsRtf rtf = group.rtf;
    switch (BorderType)
    {
      case 14:
        rtf.sect.border |= 1;
        index = 0;
        break;
      case 15:
        rtf.sect.border |= 2;
        index = 1;
        break;
      case 16 /*0x10*/:
        rtf.sect.border |= 4;
        index = 2;
        break;
      case 17:
        rtf.sect.border |= 8;
        index = 3;
        break;
    }
    if (index >= 0)
    {
      rtf.sect.BorderWidth[index] = group.BorderWidth[BorderType];
      rtf.sect.BorderSpace[index] = group.BorderSpace[BorderType];
    }
    return true;
  }

  internal bool SetRtfSectPaperSize(tc.ClsRtf rtf)
  {
    return this.SetRtfPaperSize(rtf, (int) this.InchesToTwips((double) rtf.sect.PprWidth), (int) this.InchesToTwips((double) rtf.sect.PprHeight), out rtf.sect.PprKind);
  }

  internal bool SetRtfTableInfo(ref tc.StrRtfGroup CurGroup)
  {
    tc.ClsRtf rtf = CurGroup.rtf;
    if (rtf.CurTblLevel != CurGroup.level && (rtf.flags1 & 128 /*0x80*/) == 0)
      this.SetRtfTblLevel(rtf, rtf.GroupLevel, CurGroup.level, rtf.CurTblLevel);
    if (CurGroup.InTable && this.e.RtfCurRowId <= 0 && rtf.OpenRowId > 0)
    {
      this.e.RtfCurRowId = rtf.OpenRowId;
      this.e.RtfCurCellId = rtf.OpenCellId;
      this.e.RtfLastCellX = rtf.OpenLastCellX;
    }
    if (!CurGroup.InTable)
    {
      if (this.e.RtfCurRowId > 0)
      {
        rtf.OpenRowId = this.e.RtfCurRowId;
        rtf.OpenCellId = this.e.RtfCurCellId;
        rtf.OpenLastCellX = this.e.RtfLastCellX;
      }
      this.e.RtfCurRowId = 0;
      this.e.RtfCurCellId = 0;
      this.e.RtfLastCellX = 0;
    }
    int num = !this.True(rtf.OutBuf) || rtf.OutBuf.Length <= 0 ? 0 : (int) rtf.OutBuf[0];
    if (CurGroup.InTable && this.e.RtfCurCellId > 0 && (this.e.CellAux[this.e.RtfCurCellId].flags & 8) != 0 && num != 18)
    {
      if (this.e.cell[this.e.RtfCurCellId].NextCell <= 0 && !this.CreateRtfCell(rtf, rtf.group, rtf.GroupLevel))
        return false;
      this.e.RtfCurCellId = this.e.cell[this.e.RtfCurCellId].NextCell;
    }
    if (rtf.CurTblLevel == 1 && CurGroup.InTable && this.e.RtfCurCellId == 0 && num != 18)
    {
      if (this.e.RtfCurRowId == 0)
        this.SetRtfRowDefault(rtf, rtf.group, rtf.GroupLevel);
      if (!this.CreateRtfCell(rtf, rtf.group, rtf.GroupLevel))
        return false;
    }
    if (this.e.RtfCurRowId > 0)
    {
      tc.ResetUintFlag(ref this.e.TableAux[this.e.RtfCurRowId].flags, 4);
      this.e.TableAux[this.e.RtfCurRowId].flags |= 16 /*0x10*/;
    }
    if (this.False(rtf.TableRead))
      rtf.InsertBefCell = 0;
    if (rtf.InsertBefCell > 0 && this.e.RtfCurCellId > 0 && this.e.cell[rtf.InsertBefCell].level < this.e.cell[this.e.RtfCurCellId].level)
      rtf.InsertBefCell = 0;
    return true;
  }

  internal bool SetRtfTblLevel(tc.ClsRtf rtf, int CurGroup, int NewLevel, int PrevLevel)
  {
    tc.StrRtfGroup[] group = rtf.group;
    if (NewLevel != PrevLevel)
    {
      if (NewLevel < PrevLevel)
      {
        if (rtf.OpenRowId > 0)
          this.e.TableAux[rtf.OpenRowId].flags |= 4;
        if (this.e.RtfCurRowId > 0)
          this.e.TableAux[this.e.RtfCurRowId].flags |= 4;
        this.SaveRtfLevelInfo(rtf, PrevLevel);
        this.GetRtfLevelInfo(rtf, NewLevel);
        int num;
        rtf.CurTblLevel = num = NewLevel;
        group[CurGroup].level = num;
        return true;
      }
      this.SaveRtfLevelInfo(rtf, rtf.CurTblLevel);
      int rtfInitLevel = this.e.RtfInitLevel;
      if (rtf.EmbedTable)
        ++rtfInitLevel;
      if (PrevLevel < rtfInitLevel)
        PrevLevel = rtfInitLevel;
      this.GetRtfLevelInfo(rtf, PrevLevel);
      for (int index = PrevLevel + 1; index <= NewLevel; ++index)
      {
        if (group[CurGroup].InTable && this.e.RtfCurRowId <= 0 && rtf.OpenRowId > 0)
        {
          this.e.RtfCurRowId = rtf.OpenRowId;
          this.e.RtfCurCellId = rtf.OpenCellId;
          this.e.RtfLastCellX = rtf.OpenLastCellX;
        }
        if (this.e.RtfCurRowId <= 0 || (this.e.TableAux[this.e.RtfCurRowId].flags & 33) == 0)
        {
          rtf.CurTblLevel = index - 1;
          this.SetRtfRowDefault(rtf, group, CurGroup);
          this.CreateRtfCell(rtf, group, CurGroup);
        }
        if (this.e.RtfCurCellId > 0 && (this.e.CellAux[this.e.RtfCurCellId].flags & 8) != 0)
        {
          if (this.e.cell[this.e.RtfCurCellId].NextCell <= 0 && !this.CreateRtfCell(rtf, rtf.group, rtf.GroupLevel))
            return false;
          this.e.RtfCurCellId = this.e.cell[this.e.RtfCurCellId].NextCell;
        }
        this.SaveRtfLevelInfo(rtf, index - 1);
        int num1;
        rtf.CurTblLevel = num1 = index;
        group[CurGroup].level = num1;
        this.e.RtfCurRowId = this.e.RtfCurCellId = this.e.RtfLastCellX = 0;
        int num2;
        rtf.OpenLastCellX = num2 = 0;
        int num3;
        rtf.OpenCellId = num3 = num2;
        rtf.OpenRowId = num3;
        rtf.InitTblCol = -1;
        rtf.PastingColumn = false;
        if (!this.SetRtfRowDefault(rtf, group, CurGroup) || !this.CreateRtfCell(rtf, group, CurGroup))
          return false;
        rtf.group[CurGroup].InTable = true;
      }
    }
    return true;
  }

  internal int SkipRtfGroup(tc.ClsRtf rtf)
  {
    int groupLevel = rtf.GroupLevel;
    while (this.GetRtfWord(rtf))
    {
      if (rtf.GroupEnd && rtf.GroupLevel < groupLevel)
        return 0;
    }
    return 1;
  }

  internal bool TerInsertRtfFile(string FileName, int line, int col, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    char[] mem;
    return (mem = this.e.TerFileToMem(FileName, out int _)) != null && this.e.InsertRtfBuf(new string(mem), line, col, repaint);
  }

  internal bool UpdateRtfStylesheet(tc.ClsRtf rtf)
  {
    int num = 2;
    int RtfGroup;
    tc.StrRtfGroup rtfGroup = this.GetRtfGroup(rtf, out RtfGroup);
    string str = rtf.OutBuf.Trim();
    int length = str.Length;
    if (length > 0 && str[length - 1] == ';')
      --length;
    string str1 = str.Substring(0, length);
    rtf.OutBufLen = 0;
    rtf.OutBuf = "";
    tc.StrRtfGroup strRtfGroup = rtf.group[rtf.GroupLevel];
    if ((strRtfGroup.gflags & 8) != 0)
      num = 1;
    int index1 = num != 2 ? strRtfGroup.CharStyId : strRtfGroup.ParaStyId;
    if (index1 >= rtfGroup.MaxRtfSID)
    {
      if ((rtfGroup.RtfSID = this.ReAlloc(rtfGroup.RtfSID, index1 + 1)) == null)
      {
        this.PrintError(108, "UpdateRtfStyleSheet");
        return false;
      }
      for (int maxRtfSid = rtfGroup.MaxRtfSID; maxRtfSid <= index1; ++maxRtfSid)
        rtfGroup.RtfSID[maxRtfSid] = -1;
      rtfGroup.MaxRtfSID = index1 + 1;
      rtf.group[RtfGroup] = rtfGroup;
    }
    int index2 = 0;
    while (index2 < this.e.TotalSID && (!this.e.StyleId[index2].InUse || this.e.StyleId[index2].type != num || this.strcmpi(str1, this.e.StyleId[index2].name) != 0))
      ++index2;
    if (index2 < this.e.TotalSID)
    {
      rtfGroup.RtfSID[index1] = index2;
      if (index2 > 1 || this.e.RtfInput >= 2)
        return true;
    }
    else
    {
      index2 = this.GetStyleIdSlot();
      if (index2 < 0)
        return false;
      this.e.StyleId[index2] = this.NewStyleId();
    }
    this.e.StyleId[index2].InUse = true;
    this.e.StyleId[index2].type = num;
    this.e.StyleId[index2].RtfIndex = index1;
    rtfGroup.RtfSID[index1] = index2;
    this.e.StyleId[index2].name = str1;
    this.e.StyleId[index2].next = strRtfGroup.NextStyId;
    this.e.StyleId[index2].TypeFace = strRtfGroup.TypeFace;
    this.e.StyleId[index2].FontFamily = (byte) 0;
    this.e.StyleId[index2].TwipsSize = strRtfGroup.PointSize2 * 10;
    this.e.StyleId[index2].style = strRtfGroup.style;
    this.e.StyleId[index2].TextColor = strRtfGroup.TextColor;
    this.e.StyleId[index2].TextBkColor = strRtfGroup.TextBkColor;
    this.e.StyleId[index2].UlineColor = strRtfGroup.UlineColor;
    this.e.StyleId[index2].expand = strRtfGroup.expand;
    this.e.StyleId[index2].offset = strRtfGroup.offset;
    this.e.StyleId[index2].LeftIndentTwips = strRtfGroup.LeftIndent;
    this.e.StyleId[index2].RightIndentTwips = strRtfGroup.RightIndent;
    this.e.StyleId[index2].FirstIndentTwips = strRtfGroup.FirstIndent;
    this.e.StyleId[index2].ParaFlags = strRtfGroup.flags;
    this.e.StyleId[index2].pflags = strRtfGroup.pflags & 65520;
    this.e.StyleId[index2].shading = strRtfGroup.ParShading;
    this.e.StyleId[index2].SpaceBefore = strRtfGroup.SpaceBefore;
    this.e.StyleId[index2].SpaceAfter = strRtfGroup.SpaceAfter;
    this.e.StyleId[index2].SpaceBetween = strRtfGroup.SpaceBetween;
    this.e.StyleId[index2].LineSpacing = strRtfGroup.LineSpacing;
    this.e.StyleId[index2].ParaBkColor = strRtfGroup.ParaBkColor;
    this.e.StyleId[index2].ParaBorderColor = strRtfGroup.ParaBorderColor;
    this.e.StyleId[index2].OutlineLevel = strRtfGroup.OutlineLevel;
    if (num == 1)
      this.e.StyleId[index2].flags = 1;
    if (strRtfGroup.RtfLs > 0)
    {
      strRtfGroup.flags |= 8;
      tc.StrBlt strBlt = new tc.StrBlt();
      strRtfGroup.blt = strBlt.init();
      strRtfGroup.blt.NumberType = 5;
    }
    strRtfGroup.blt.ls = rtf.XlateLs[strRtfGroup.RtfLs];
    strRtfGroup.blt.lvl = strRtfGroup.ListLvl;
    if ((strRtfGroup.flags & 8) != 0)
      this.e.StyleId[index2].BltId = this.NewBltId(0, strRtfGroup.blt);
    this.e.StyleId[index2].TabId = this.NewTabId(0, strRtfGroup.tab);
    rtf.group[rtf.GroupLevel] = strRtfGroup;
    return true;
  }

  internal string XlateRtfHex(string InText)
  {
    char ch1 = char.MinValue;
    char ch2 = '\\';
    char[] charArray = InText.ToCharArray();
    int length1 = InText.Length;
    int length2 = 0;
    for (int index = 0; index < length1; ++index)
    {
      char ch3 = charArray[index];
      if ((int) ch1 == (int) ch2)
      {
        if (ch3 == '\'' && index + 2 < length1)
        {
          char upper1 = char.ToUpper(charArray[index + 1]);
          int num1 = (upper1 < 'A' ? (int) upper1 - 48 /*0x30*/ : 10 + (int) upper1 - 65) << 4;
          charArray[length2] = (char) num1;
          char upper2 = char.ToUpper(charArray[index + 2]);
          int num2 = upper2 < 'A' ? (int) upper2 - 48 /*0x30*/ : 10 + (int) upper2 - 65;
          charArray[length2] += (char) num2;
          index += 2;
          ++length2;
        }
        else
        {
          charArray[length2] = ch2;
          ++length2;
          if ((int) ch3 != (int) ch2)
          {
            charArray[length2] = ch3;
            ++length2;
          }
        }
      }
      else if ((int) ch3 != (int) ch2)
      {
        charArray[length2] = ch3;
        ++length2;
      }
      ch1 = ch3;
    }
    if ((int) ch1 == (int) ch2)
    {
      charArray[length2] = ch2;
      ++length2;
    }
    return new string(charArray, 0, length2);
  }
}
