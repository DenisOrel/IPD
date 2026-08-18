// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.CDrag
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class CDrag : COp
{
  internal CDrag(ImRtfEditor ImRtfEditor)
    : base(ImRtfEditor)
  {
  }

  internal new bool CreateCellDragObj(int type, int id, int BorderX, int BorderY1, int BorderY2)
  {
    int dragObjectSlot;
    if ((dragObjectSlot = this.GetDragObjectSlot()) < 0)
      return false;
    this.e.DragObj[dragObjectSlot].InUse = true;
    this.e.DragObj[dragObjectSlot].type = type;
    this.e.DragObj[dragObjectSlot].id1 = id;
    this.e.DragObj[dragObjectSlot].id2 = -1;
    this.e.DragObj[dragObjectSlot].id3 = -1;
    this.e.DragObj[dragObjectSlot].HotRectCount = 1;
    this.e.DragObj[dragObjectSlot].HotRect[0].left = BorderX - this.TwipsToScrX(60);
    this.e.DragObj[dragObjectSlot].HotRect[0].top = BorderY1;
    this.e.DragObj[dragObjectSlot].HotRect[0].right = BorderX + this.TwipsToScrX(60);
    this.e.DragObj[dragObjectSlot].HotRect[0].bottom = BorderY2;
    this.e.DragObj[dragObjectSlot].ObjPointCount = 2;
    this.e.DragObj[dragObjectSlot].ObjPoint[0].X = BorderX;
    this.e.DragObj[dragObjectSlot].ObjPoint[0].Y = BorderY1;
    this.e.DragObj[dragObjectSlot].ObjPoint[1].X = BorderX;
    this.e.DragObj[dragObjectSlot].ObjPoint[1].Y = BorderY2;
    return true;
  }

  internal new bool CreateRowDragObj(int type, int id, int BorderX1, int BorderX2, int BorderY)
  {
    int dragObjectSlot;
    if ((dragObjectSlot = this.GetDragObjectSlot()) < 0)
      return false;
    this.e.DragObj[dragObjectSlot].InUse = true;
    this.e.DragObj[dragObjectSlot].type = type;
    this.e.DragObj[dragObjectSlot].id1 = id;
    this.e.DragObj[dragObjectSlot].id2 = -1;
    this.e.DragObj[dragObjectSlot].id3 = -1;
    this.e.DragObj[dragObjectSlot].HotRectCount = 1;
    this.e.DragObj[dragObjectSlot].HotRect[0].left = BorderX1;
    this.e.DragObj[dragObjectSlot].HotRect[0].top = BorderY - this.TwipsToScrY(60);
    this.e.DragObj[dragObjectSlot].HotRect[0].right = BorderX2;
    this.e.DragObj[dragObjectSlot].HotRect[0].bottom = BorderY + this.TwipsToScrY(60);
    this.e.DragObj[dragObjectSlot].ObjPointCount = 2;
    this.e.DragObj[dragObjectSlot].ObjPoint[0].X = BorderX1;
    this.e.DragObj[dragObjectSlot].ObjPoint[0].Y = BorderY;
    this.e.DragObj[dragObjectSlot].ObjPoint[1].X = BorderX2;
    this.e.DragObj[dragObjectSlot].ObjPoint[1].Y = BorderY;
    return true;
  }

  internal new bool DeleteDragObjects(int FirstType, int LastType)
  {
    for (int index = 0; index <= this.e.TotalDragObjs; ++index)
    {
      if (this.e.DragObj[index].type >= FirstType && this.e.DragObj[index].type <= LastType)
        this.e.DragObj[index].InUse = false;
    }
    return true;
  }

  internal new bool DragApply(int DeltaX, int DeltaY, int LastX, int LastY)
  {
    int num1 = this.ScrToTwipsX(DeltaX);
    int twipsY1 = this.ScrToTwipsY(DeltaY);
    if (this.e.DragObj[this.e.CurDragObj].type == 5)
    {
      if (this.e.DragObj[this.e.CurDragObj].drawn)
        this.DrawDragRulerIndent();
      if ((this.e.text[this.e.CurLine].flags2 & 32 /*0x20*/) != 0)
        num1 = -num1;
      if (this.e.HilightType != 0 && (this.e.TerFlags5 & 4194304 /*0x400000*/) != 0)
      {
        int id1 = this.e.DragObj[this.e.CurDragObj].id1;
        int left = this.e.PfmtId[id1].LeftIndentTwips + num1;
        if (left < 0)
          left = 0;
        this.e.TerSetParaIndent(left, -1, this.e.PfmtId[id1].FirstIndentTwips - num1, false);
      }
      else
        this.e.ParaIndentTwips(num1, 0, -num1, false);
    }
    else if (this.e.DragObj[this.e.CurDragObj].type == 6)
    {
      if (this.e.DragObj[this.e.CurDragObj].drawn)
        this.DrawDragRulerIndent();
      if ((this.e.text[this.e.CurLine].flags2 & 32 /*0x20*/) != 0)
        num1 = -num1;
      if (this.e.HilightType != 0 && (this.e.TerFlags5 & 4194304 /*0x400000*/) != 0)
      {
        int right = this.e.PfmtId[this.e.DragObj[this.e.CurDragObj].id1].RightIndentTwips - num1;
        if (right < 0)
          right = 0;
        this.e.TerSetParaIndent(-1, right, -1, false);
      }
      else
        this.e.ParaIndentTwips(0, -num1, 0, false);
    }
    else if (this.e.DragObj[this.e.CurDragObj].type == 7)
    {
      if (this.e.DragObj[this.e.CurDragObj].drawn)
        this.DrawDragRulerIndent();
      if ((this.e.text[this.e.CurLine].flags2 & 32 /*0x20*/) != 0)
        num1 = -num1;
      if (this.e.HilightType != 0 && (this.e.TerFlags5 & 4194304 /*0x400000*/) != 0)
      {
        int id1 = this.e.DragObj[this.e.CurDragObj].id1;
        int first = this.e.PfmtId[id1].FirstIndentTwips + num1;
        if (first < 0)
          first = 0;
        this.e.TerSetParaIndent(this.e.PfmtId[id1].LeftIndentTwips, -1, first, false);
      }
      else
        this.e.ParaIndentTwips(0, 0, num1, false);
    }
    else if (this.e.DragObj[this.e.CurDragObj].type == 8)
    {
      if (this.e.DragObj[this.e.CurDragObj].drawn)
        this.DrawDragRulerTab(LastY);
      int id1 = this.e.DragObj[this.e.CurDragObj].id1;
      int index = !this.e.EditingParaStyle ? this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].TabId : this.e.StyleId[this.e.CurSID].TabId;
      if ((this.e.text[this.e.CurLine].flags2 & 32 /*0x20*/) != 0)
        num1 = -num1;
      if (id1 < this.e.TerTab[index].count)
      {
        byte flag = this.e.TerTab[index].flags[id1];
        int type = this.e.TerTab[index].type[id1];
        int num2 = this.e.TerTab[index].pos[id1] + num1;
        if (this.e.SnapToGrid)
          num2 = (this.e.TerFlags & 2) == 0 ? this.RoundInt(num2, 90) : this.RoundInt(num2, 71);
        this.e.ClearTab(this.e.TerTab[index].pos[id1], false);
        if (num2 > 0 && LastY >= this.e.RulerRect.top && LastY < this.e.RulerRect.bottom)
          this.e.TerSetTab(type, num2, flag, false);
      }
    }
    else if (this.e.DragObj[this.e.CurDragObj].type == 11)
    {
      if (this.e.DragObj[this.e.CurDragObj].drawn)
        this.DrawDragRowLine();
      int id1 = this.e.DragObj[this.e.CurDragObj].id1;
      this.SaveUndo(-1, id1, -1, id1, '4');
      int row = this.e.cell[id1].row;
      if (this.e.TableRow[row].MinHeight >= 0)
        this.e.TableRow[row].MinHeight = this.UnitToTwipsY(this.e.TableRow[row].height) + twipsY1;
      else
        this.e.TableRow[row].MinHeight -= twipsY1;
      this.DeleteTextMap(true);
      this.RequestPagination(true);
    }
    else if (this.e.DragObj[this.e.CurDragObj].type != 9)
    {
      if (this.e.DragObj[this.e.CurDragObj].type == 10)
      {
        int num3 = 180;
        if (this.e.DragObj[this.e.CurDragObj].drawn)
          this.DrawDragCellLine();
        int id1 = this.e.DragObj[this.e.CurDragObj].id1;
        int index1 = this.e.DragObj[this.e.CurDragObj].id2;
        int index2 = this.e.DragObj[this.e.CurDragObj].id3;
        if (index1 == -1)
          index1 = id1;
        if (index2 == -1)
          index2 = id1;
        this.SaveUndo(-1, this.e.TableRow[index1].FirstCell, -1, this.e.TableRow[index2].FirstCell, '4');
        for (int index3 = index1; index3 > 0; index3 = this.e.TableRow[index3].NextRow)
        {
          int num4 = num1;
          if (this.e.SnapToGrid)
          {
            int val = this.e.TableRow[index3].indent + num4;
            num4 = ((this.e.TerFlags & 2) == 0 ? this.RoundInt(val, 90) : this.RoundInt(val, 71)) - this.e.TableRow[index3].indent;
            if ((this.e.TableRow[index3].flags & 65536 /*0x010000*/) != 0)
              num4 = -num4;
          }
          if (this.e.TableRow[index3].indent + num4 >= 0)
          {
            int num5 = 0;
            int index4 = this.e.TableRow[index3].FirstCell;
            int num6 = 0;
            while (index4 > 0)
            {
              num5 += this.e.cell[index4].width;
              index4 = this.e.cell[index4].NextCell;
              ++num6;
            }
            if (num4 >= num5 - num3 * num6)
            {
              num4 = num5 - num3 * num6;
              if (num4 < 0)
                num4 = 0;
            }
            this.e.TableRow[index3].indent += num4;
            int indent = this.e.TableRow[index3].indent;
            for (int index5 = this.e.TableRow[index3].FirstCell; index5 > 0; index5 = this.e.cell[index5].NextCell)
            {
              this.e.cell[index5].x = indent;
              this.e.cell[index5].width -= this.e.cell[index5].width * num4 / num5;
              if (this.e.cell[index5].width < num3)
                this.e.cell[index5].width = num3;
              if (this.e.HtmlMode)
              {
                this.e.cell[index5].FixWidth = this.e.cell[index5].width;
                tc.ResetUintFlag(ref this.e.cell[index5].flags, 512 /*0x0200*/);
                this.e.cell[index5].flags |= 256 /*0x0100*/;
              }
              indent += this.e.cell[index5].width;
            }
          }
        }
        this.DeleteTextMap(true);
        this.RequestPagination(true);
      }
      else if (this.e.DragObj[this.e.CurDragObj].type == 2)
      {
        int id1 = this.e.DragObj[this.e.CurDragObj].id1;
        if (this.e.FrameRectHilighted)
          this.DrawDragFrameRect();
        this.FitPictureInFrame(this.e.CurLine, true);
        this.e.TerOpFlags2 |= 65536 /*0x010000*/;
        if ((this.e.ParaFrame[id1].flags & 256 /*0x0100*/) != 0)
          this.DragApplyLineSize(DeltaX, DeltaY);
        else if (this.e.CurHotSpot == 0 || this.e.CurHotSpot == 2)
        {
          int twipsX = this.ScrToTwipsX(this.e.DragObj[this.e.CurDragObj].ObjRect.left);
          int top = this.e.DragObj[this.e.CurDragObj].ObjRect.top;
          if (top > this.e.FirstPageHeight)
            top -= this.e.FirstPageHeight;
          int twipsY2 = this.ScrToTwipsY(top);
          if (this.e.BorderShowing)
          {
            twipsY2 -= this.UnitToTwipsY(this.e.TopBorderHeight);
            twipsX -= this.UnitToTwipsX(this.GetBorderLeftSpace(this.e.CurPage));
          }
          this.e.TerMoveParaFrame(this.e.DragObj[this.e.CurDragObj].id1, twipsX, twipsY2, -1, -1);
        }
        else
        {
          this.SaveUndo(id1, 0, 0, 0, '2');
          this.Repaginate(false, true, 0, true);
        }
        this.e.TerOpFlags2 = tc.ResetFlag(this.e.TerOpFlags2, 65536 /*0x010000*/);
      }
      else if (this.e.DragObj[this.e.CurDragObj].type == 1)
      {
        int id1 = this.e.DragObj[this.e.CurDragObj].id1;
        int paraFid = this.e.TerFont[id1].ParaFID;
        this.e.TerOpFlags2 |= 65536 /*0x010000*/;
        if (paraFid > 0)
          this.SaveUndo(paraFid, 0, 0, 0, '2');
        else
          this.SaveUndo(id1, 0, 0, 0, '3');
        this.e.TerOpFlags2 = tc.ResetFlag(this.e.TerOpFlags2, 65536 /*0x010000*/);
        this.e.TerFont[id1].flags |= 4096 /*0x1000*/;
        if (this.e.CurLine < this.e.RepageBeginLine)
          this.e.RepageBeginLine = this.e.CurLine;
        if (this.e.TerFont[id1].FrameType != 0 && paraFid > 0)
          this.Repaginate(false, true, 0, true);
      }
      else if (this.e.DragObj[this.e.CurDragObj].type == 3)
      {
        if (this.e.FrameRectHilighted)
          this.DrawDragFrameRect();
        int id1 = this.e.DragObj[this.e.CurDragObj].id1;
        if (this.e.ParaFrame[id1].pict > 0)
        {
          this.e.TerMovePictFrame2(this.e.ParaFrame[id1].pict, num1, twipsY1);
        }
        else
        {
          int twipsX;
          int x;
          if ((this.e.ParaFrame[id1].flags & 256 /*0x0100*/) != 0)
          {
            twipsX = this.ScrToTwipsX(this.e.DragObj[this.e.CurDragObj].ObjPoint[0].X);
            x = this.e.ParaFrame[id1].LineType != 2 ? this.e.DragObj[this.e.CurDragObj].ObjPoint[0].Y : this.e.DragObj[this.e.CurDragObj].ObjPoint[1].Y;
          }
          else
          {
            twipsX = this.ScrToTwipsX(this.e.DragObj[this.e.CurDragObj].ObjRect.left);
            x = this.e.DragObj[this.e.CurDragObj].ObjRect.top;
          }
          int firstFramePage = this.e.FirstFramePage;
          if (x > this.e.FirstPageHeight)
          {
            x -= this.e.FirstPageHeight;
            ++firstFramePage;
          }
          int twipsY3 = this.ScrToTwipsY(x);
          if (this.e.BorderShowing)
          {
            twipsY3 -= this.UnitToTwipsY(this.e.TopBorderHeight);
            twipsX -= this.UnitToTwipsX(this.GetBorderLeftSpace(this.e.CurPage));
          }
          this.e.TerMoveParaFrame2(this.e.DragObj[this.e.CurDragObj].id1, twipsX, twipsY3, -1, -1, firstFramePage);
        }
      }
    }
    else
    {
      bool flag = (this.e.TerFlags4 & 134217728 /*0x08000000*/) != 0;
      if (this.e.DragObj[this.e.CurDragObj].drawn)
        this.DrawDragCellLine();
      int id1 = this.e.DragObj[this.e.CurDragObj].id1;
      int BegCol = this.e.DragObj[this.e.CurDragObj].id2;
      int EndCol = this.e.DragObj[this.e.CurDragObj].id3;
      if (BegCol == -1)
        BegCol = id1;
      if (EndCol == -1)
        EndCol = id1;
      this.SaveUndo(-1, BegCol, -1, EndCol, '4');
      int row1 = this.e.cell[BegCol].row;
      int row2 = this.e.cell[EndCol].row;
      int cellRightX = this.GetCellRightX(id1);
      int index = row1;
      do
      {
        int CurCell;
        for (CurCell = this.e.TableRow[index].FirstCell; Math.Abs(this.GetCellRightX(CurCell) - cellRightX) >= 60; CurCell = this.e.cell[CurCell].NextCell)
        {
          if (this.e.cell[CurCell].NextCell == -1)
            goto label_131;
        }
        int num7 = num1;
        if ((this.e.TableRow[index].flags & 65536 /*0x010000*/) != 0)
          num7 = -num7;
        int nextCell = this.e.cell[CurCell].NextCell;
        if (this.e.cell[CurCell].width + num7 >= 180 && (nextCell <= 0 | flag || this.e.cell[nextCell].width - num7 >= 180))
        {
          if (this.e.SnapToGrid)
          {
            int val = this.e.cell[CurCell].x + this.e.cell[CurCell].width + num7;
            num7 = ((this.e.TerFlags & 2) == 0 ? this.RoundInt(val, 90) : this.RoundInt(val, 71)) - this.e.cell[CurCell].x - this.e.cell[CurCell].width;
          }
          this.e.cell[CurCell].width += num7;
          if (this.e.HtmlMode)
          {
            this.e.cell[CurCell].FixWidth = this.e.cell[CurCell].width;
            tc.ResetUintFlag(ref this.e.cell[CurCell].flags, 512 /*0x0200*/);
            this.e.cell[CurCell].flags |= 256 /*0x0100*/;
          }
          if (nextCell > 0 && !flag)
          {
            this.e.cell[nextCell].width -= num7;
            this.e.cell[nextCell].x += num7;
            if (this.e.HtmlMode)
            {
              this.e.cell[nextCell].FixWidth = this.e.cell[nextCell].width;
              tc.ResetUintFlag(ref this.e.cell[nextCell].flags, 512 /*0x0200*/);
              this.e.cell[nextCell].flags |= 256 /*0x0100*/;
            }
          }
        }
label_131:
        if (index != row2)
          index = this.e.TableRow[index].NextRow;
        else
          break;
      }
      while (index != -1);
      this.DeleteTextMap(true);
      this.RequestPagination(true);
    }
    return true;
  }

  internal new bool DragApplyLineSize(int DeltaX, int DeltaY)
  {
    int id1 = this.e.DragObj[this.e.CurDragObj].id1;
    int x = this.e.ParaFrame[id1].x;
    int y = this.e.ParaFrame[id1].y;
    int paraY = this.e.ParaFrame[id1].ParaY;
    if (this.e.ParaFrame[id1].LineType == 2)
      this.e.ParaFrame[id1].y = this.ScrToTwipsY(this.e.DragObj[this.e.CurDragObj].ObjPoint[3].Y);
    else
      this.e.ParaFrame[id1].y = this.ScrToTwipsY(this.e.DragObj[this.e.CurDragObj].ObjPoint[2].Y);
    int x1_1;
    int y1_1;
    int x2;
    int y2;
    this.LineRectToPoints(id1, out x1_1, out y1_1, out x2, out y2);
    if (this.e.CurHotSpot == 0)
    {
      x1_1 += this.ScrToTwipsX(DeltaX);
      y1_1 += this.ScrToTwipsY(DeltaY);
      if (Math.Abs(x1_1 - x2) < 60)
        x1_1 = x2;
      if (Math.Abs(y1_1 - y2) < 60)
        y1_1 = y2;
    }
    else if (this.e.CurHotSpot == 1)
    {
      x2 += this.ScrToTwipsX(DeltaX);
      y2 += this.ScrToTwipsY(DeltaY);
      if (Math.Abs(x1_1 - x2) < 60)
        x2 = x1_1;
      if (Math.Abs(y1_1 - y2) < 60)
        y2 = y1_1;
    }
    this.LinePointsToRect(id1, x1_1, y1_1, x2, y2);
    int x1_2 = this.e.ParaFrame[id1].x;
    int scrY = this.TwipsToScrY(this.e.ParaFrame[id1].y);
    int firstFramePage = this.e.FirstFramePage;
    if (scrY >= this.e.FirstPageHeight)
    {
      scrY -= this.e.FirstPageHeight;
      ++firstFramePage;
    }
    int y1_2 = this.ScrToTwipsY(scrY);
    if (this.e.BorderShowing)
      y1_2 -= this.UnitToTwipsY(this.e.TopBorderHeight);
    this.e.ParaFrame[id1].x = x;
    this.e.ParaFrame[id1].y = y;
    this.e.ParaFrame[id1].ParaY = paraY;
    this.e.TerMoveParaFrame2(id1, x1_2, y1_2, -1, -1, firstFramePage);
    this.LineRectToPoints(id1, out x1_2, out y1_2, out x2, out y2);
    return true;
  }

  internal new bool DragCellSize(int DeltaX)
  {
    if (this.e.DragObj[this.e.CurDragObj].id2 == -1)
    {
      int id1 = this.e.DragObj[this.e.CurDragObj].id1;
      bool flag = this.e.DragObj[this.e.CurDragObj].TextHilighted && (this.e.cell[id1].flags & 3) != 0;
      int CurCell1 = id1;
      int index1 = -1;
      int prevCellInColumnPos;
      for (; (prevCellInColumnPos = this.GetPrevCellInColumnPos(CurCell1, true)) != -1 && (!flag || (this.e.cell[prevCellInColumnPos].flags & 3) != 0); CurCell1 = prevCellInColumnPos)
      {
        int index2 = 0;
        while (index2 < this.e.TotalFrames && this.e.frame[index2].CellId != prevCellInColumnPos)
          ++index2;
        if (index2 < this.e.TotalFrames)
          index1 = index2;
      }
      int CurCell2 = id1;
      int index3 = -1;
      int nextCellInColumnPos;
      for (; (nextCellInColumnPos = this.GetNextCellInColumnPos(CurCell2)) != -1 && (!flag || (this.e.cell[nextCellInColumnPos].flags & 3) != 0); CurCell2 = nextCellInColumnPos)
      {
        int index4 = 0;
        while (index4 < this.e.TotalFrames && this.e.frame[index4].CellId != nextCellInColumnPos)
          ++index4;
        if (index4 < this.e.TotalFrames)
          index3 = index4;
      }
      if (index1 >= 0)
        this.e.DragObj[this.e.CurDragObj].ObjPoint[0].Y = this.e.frame[index1].ScrY;
      if (index3 >= 0)
        this.e.DragObj[this.e.CurDragObj].ObjPoint[1].Y = this.e.frame[index3].ScrY + this.e.frame[index3].ScrHeight;
      this.e.DragObj[this.e.CurDragObj].id2 = CurCell1;
      this.e.DragObj[this.e.CurDragObj].id3 = CurCell2;
    }
    if (this.e.DragObj[this.e.CurDragObj].drawn)
      this.DrawDragCellLine();
    this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].left += DeltaX;
    this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].right += DeltaX;
    for (int index = 0; index < this.e.DragObj[this.e.CurDragObj].ObjPointCount; ++index)
      this.e.DragObj[this.e.CurDragObj].ObjPoint[index].X += DeltaX;
    this.DrawDragCellLine();
    ++this.e.TerArg.modified;
    return true;
  }

  internal new bool DragFrameMove(int DeltaX, int DeltaY)
  {
    bool flag = false;
    if (this.e.FrameRectHilighted)
      this.DrawDragFrameRect();
    if ((this.e.ParaFrame[this.e.DragObj[this.e.CurDragObj].id1].flags & 256 /*0x0100*/) != 0)
      flag = true;
    if (flag)
    {
      this.e.DragObj[this.e.CurDragObj].ObjPoint[0].X += DeltaX;
      this.e.DragObj[this.e.CurDragObj].ObjPoint[1].X += DeltaX;
      this.e.DragObj[this.e.CurDragObj].ObjPoint[0].Y += DeltaY;
      this.e.DragObj[this.e.CurDragObj].ObjPoint[1].Y += DeltaY;
    }
    else
    {
      this.e.DragObj[this.e.CurDragObj].ObjRect.left += DeltaX;
      this.e.DragObj[this.e.CurDragObj].ObjRect.right += DeltaX;
      this.e.DragObj[this.e.CurDragObj].ObjRect.top += DeltaY;
      this.e.DragObj[this.e.CurDragObj].ObjRect.bottom += DeltaY;
    }
    for (int index = 0; index < this.e.TotalDragObjs; ++index)
    {
      if (this.e.DragObj[index].InUse && (this.e.DragObj[index].type == 2 || this.e.DragObj[index].type == 1))
      {
        if (flag)
        {
          this.e.DragObj[index].ObjPoint[0] = this.e.DragObj[this.e.CurDragObj].ObjPoint[0];
          this.e.DragObj[index].ObjPoint[1] = this.e.DragObj[this.e.CurDragObj].ObjPoint[1];
          break;
        }
        this.e.DragObj[index].ObjRect = this.e.DragObj[this.e.CurDragObj].ObjRect;
        break;
      }
    }
    for (int index = 0; index < 4; ++index)
    {
      this.e.DragObj[this.e.CurDragObj].HotRect[index].left += DeltaX;
      this.e.DragObj[this.e.CurDragObj].HotRect[index].right += DeltaX;
      this.e.DragObj[this.e.CurDragObj].HotRect[index].top += DeltaY;
      this.e.DragObj[this.e.CurDragObj].HotRect[index].bottom += DeltaY;
    }
    ++this.e.TerArg.modified;
    this.DrawDragFrameRect();
    return true;
  }

  internal new bool DragPictFrameSize(int type, int DeltaX, int DeltaY)
  {
    int num1 = 0;
    int num2 = 0;
    bool flag1 = false;
    bool flag2 = false;
    int index1 = 0;
    int id1 = this.e.DragObj[this.e.CurDragObj].id1;
    if (type == 2 && (this.e.ParaFrame[id1].flags & 256 /*0x0100*/) != 0)
      flag1 = true;
    if (type == 1)
    {
      index1 = this.e.TerFont[id1].ParaFID;
      if (index1 > 0 && (this.e.ParaFrame[index1].flags & 256 /*0x0100*/) != 0)
        flag1 = true;
    }
    if (type == 1 && this.e.PictureHilighted)
      this.DrawDragPictRect();
    if (type == 2)
    {
      if (this.e.FrameTabsHilighted)
        this.DrawDragFrameTabs();
      if (this.e.FrameRectHilighted)
        this.DrawDragFrameRect();
    }
    bool flag3 = ((uint) this.GetKeyState(16 /*0x10*/) & 32768U /*0x8000*/) > 0U;
    if (this.e.CurHotSpot >= 4 && this.e.CurHotSpot <= 7)
    {
      flag2 = true;
    }
    else
    {
      if (type == 1 && (this.e.TerFlags & 131072 /*0x020000*/) != 0 | flag3)
        flag2 = true;
      if (type == 2 && (this.e.TerFlags & 262144 /*0x040000*/) != 0 && !flag1)
        flag2 = true;
    }
    if (flag2)
    {
      int num3 = Math.Abs(this.e.DragObj[this.e.CurDragObj].ObjRect.right - this.e.DragObj[this.e.CurDragObj].ObjRect.left);
      int num4 = Math.Abs(this.e.DragObj[this.e.CurDragObj].ObjRect.bottom - this.e.DragObj[this.e.CurDragObj].ObjRect.top);
      double aspectRatio = this.e.DragObj[this.e.CurDragObj].AspectRatio;
      if (this.e.CurHotSpot == 0 || this.e.CurHotSpot == 1)
        DeltaY = (aspectRatio == 0.0 ? num2 : (int) ((double) (num3 + DeltaX) / aspectRatio)) - num4;
      else
        DeltaX = (aspectRatio == 0.0 ? num1 : (int) ((double) (num4 + DeltaY) * aspectRatio)) - num3;
    }
    int curHotSpot = this.e.CurHotSpot;
    for (int index2 = 0; index2 < 2; ++index2)
    {
      if (index2 == 1)
      {
        if (flag2)
        {
          if (this.e.CurHotSpot == 0)
          {
            this.e.CurHotSpot = 2;
            DeltaY = -DeltaY;
          }
          else if (this.e.CurHotSpot == 2)
          {
            this.e.CurHotSpot = 0;
            DeltaX = -DeltaX;
          }
          else if (this.e.CurHotSpot == 1)
            this.e.CurHotSpot = 3;
          else if (this.e.CurHotSpot == 3)
            this.e.CurHotSpot = 1;
        }
        else
          break;
      }
      if (this.e.CurHotSpot == 0)
      {
        this.e.DragObj[this.e.CurDragObj].ObjRect.left += DeltaX;
        this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].left += DeltaX;
        this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].right += DeltaX;
        if (flag1)
        {
          this.e.DragObj[this.e.CurDragObj].ObjPoint[0].X += DeltaX;
          this.e.DragObj[this.e.CurDragObj].ObjPoint[0].Y += DeltaY;
          this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].top += DeltaY;
          this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].bottom += DeltaY;
        }
      }
      if (this.e.CurHotSpot == 1)
      {
        this.e.DragObj[this.e.CurDragObj].ObjRect.right += DeltaX;
        this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].left += DeltaX;
        this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].right += DeltaX;
        if (flag1)
        {
          this.e.DragObj[this.e.CurDragObj].ObjPoint[1].X += DeltaX;
          this.e.DragObj[this.e.CurDragObj].ObjPoint[1].Y += DeltaY;
          this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].top += DeltaY;
          this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].bottom += DeltaY;
        }
      }
      if (this.e.CurHotSpot == 2)
      {
        this.e.DragObj[this.e.CurDragObj].ObjRect.top += DeltaY;
        this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].top += DeltaY;
        this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].bottom += DeltaY;
      }
      if (this.e.CurHotSpot == 3)
      {
        this.e.DragObj[this.e.CurDragObj].ObjRect.bottom += DeltaY;
        this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].top += DeltaY;
        this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].bottom += DeltaY;
      }
      if (this.e.CurHotSpot == 4)
      {
        this.e.DragObj[this.e.CurDragObj].ObjRect.left -= DeltaX;
        this.e.DragObj[this.e.CurDragObj].ObjRect.top += DeltaY;
        this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].top += DeltaY;
        this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].bottom += DeltaY;
        this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].left -= DeltaX;
        this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].right -= DeltaX;
        break;
      }
      if (this.e.CurHotSpot == 5)
      {
        this.e.DragObj[this.e.CurDragObj].ObjRect.right += DeltaX;
        this.e.DragObj[this.e.CurDragObj].ObjRect.top += DeltaY;
        this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].top += DeltaY;
        this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].bottom += DeltaY;
        this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].left += DeltaX;
        this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].right += DeltaX;
        break;
      }
      if (this.e.CurHotSpot == 6)
      {
        this.e.DragObj[this.e.CurDragObj].ObjRect.left -= DeltaX;
        this.e.DragObj[this.e.CurDragObj].ObjRect.bottom += DeltaY;
        this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].top += DeltaY;
        this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].bottom += DeltaY;
        this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].left -= DeltaX;
        this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].right -= DeltaX;
        break;
      }
      if (this.e.CurHotSpot == 7)
      {
        this.e.DragObj[this.e.CurDragObj].ObjRect.right += DeltaX;
        this.e.DragObj[this.e.CurDragObj].ObjRect.bottom += DeltaY;
        this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].top += DeltaY;
        this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].bottom += DeltaY;
        this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].left += DeltaX;
        this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].right += DeltaX;
        break;
      }
    }
    this.e.CurHotSpot = curHotSpot;
    if (!flag1)
    {
      num1 = this.e.DragObj[this.e.CurDragObj].ObjRect.right - this.e.DragObj[this.e.CurDragObj].ObjRect.left;
      num2 = this.e.DragObj[this.e.CurDragObj].ObjRect.bottom - this.e.DragObj[this.e.CurDragObj].ObjRect.top;
      if (num1 < this.TwipsToScrX(120))
        num1 = this.TwipsToScrX(120);
      if (num2 < this.TwipsToScrY(120))
        num2 = this.TwipsToScrY(120);
    }
    if (type == 1)
    {
      this.e.TerFont[id1].PictWidth = this.ScrToTwipsX(num1);
      this.e.TerFont[id1].PictHeight = this.ScrToTwipsY(num2);
      if (index1 > 0)
      {
        this.e.ParaFrame[index1].width = this.ScrToTwipsX(num1);
        this.e.ParaFrame[index1].height = this.ScrToTwipsY(num2);
      }
      this.SetPictSize(id1, num2, num1, true);
      this.XlateSizeForPrt(id1);
      this.DrawDragPictRect();
    }
    else
    {
      if (!flag1)
      {
        this.e.ParaFrame[id1].width = this.ScrToTwipsX(num1);
        int twipsY;
        this.e.ParaFrame[id1].height = twipsY = this.ScrToTwipsY(num2);
        this.e.ParaFrame[id1].MinHeight = twipsY;
        if (this.e.ParaFrame[id1].TextAngle > 0)
          this.e.ParaFrame[id1].height = this.e.ParaFrame[id1].MinHeight;
      }
      this.DrawDragFrameTabs();
      this.DrawDragFrameRect();
    }
    ++this.e.TerArg.modified;
    return true;
  }

  internal new bool DragRowIndent(int DeltaX)
  {
    if (this.e.DragObj[this.e.CurDragObj].id2 == -1)
    {
      int id1 = this.e.DragObj[this.e.CurDragObj].id1;
      int index1 = id1;
      int index2 = -1;
      int prevRow;
      for (; (prevRow = this.e.TableRow[index1].PrevRow) != -1; index1 = prevRow)
      {
        int index3 = 0;
        while (index3 < this.e.TotalFrames && this.e.frame[index3].RowId != prevRow)
          ++index3;
        if (index3 < this.e.TotalFrames)
          index2 = index3;
      }
      int index4 = id1;
      int index5 = -1;
      int nextRow;
      for (; (nextRow = this.e.TableRow[index4].NextRow) != -1; index4 = nextRow)
      {
        int index6 = 0;
        while (index6 < this.e.TotalFrames && this.e.frame[index6].RowId != nextRow)
          ++index6;
        if (index6 < this.e.TotalFrames)
          index5 = index6;
      }
      if (index2 >= 0)
        this.e.DragObj[this.e.CurDragObj].ObjPoint[0].Y = this.e.frame[index2].ScrY;
      if (index5 >= 0)
        this.e.DragObj[this.e.CurDragObj].ObjPoint[1].Y = this.e.frame[index5].ScrY + this.e.frame[index5].ScrHeight;
      this.e.DragObj[this.e.CurDragObj].id2 = index1;
      this.e.DragObj[this.e.CurDragObj].id3 = index4;
    }
    if (this.e.DragObj[this.e.CurDragObj].drawn)
      this.DrawDragCellLine();
    this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].left += DeltaX;
    this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].right += DeltaX;
    for (int index = 0; index < this.e.DragObj[this.e.CurDragObj].ObjPointCount; ++index)
      this.e.DragObj[this.e.CurDragObj].ObjPoint[index].X += DeltaX;
    this.DrawDragCellLine();
    ++this.e.TerArg.modified;
    return true;
  }

  internal new bool DragRowSize(int DeltaY)
  {
    if (this.e.DragObj[this.e.CurDragObj].id2 == -1)
    {
      int id1 = this.e.DragObj[this.e.CurDragObj].id1;
      int index1 = id1;
      int index2 = -1;
      int index3;
      for (; (index3 = this.e.cell[index1].PrevCell) > 0; index1 = index3)
      {
        if ((this.e.cell[index3].flags & 16 /*0x10*/) != 0)
          index3 = this.e.CellAux[index3].SpanningCell;
        if (index3 > 0)
        {
          int index4 = 0;
          while (index4 < this.e.TotalFrames && this.e.frame[index4].CellId != index3)
            ++index4;
          if (index4 < this.e.TotalFrames)
            index2 = index4;
        }
        else
          break;
      }
      int index5 = id1;
      int index6 = -1;
      int index7;
      for (; (index7 = this.e.cell[index5].NextCell) > 0; index5 = index7)
      {
        if ((this.e.cell[index7].flags & 16 /*0x10*/) != 0)
          index7 = this.e.CellAux[index7].SpanningCell;
        if (index7 > 0)
        {
          int index8 = 0;
          while (index8 < this.e.TotalFrames && this.e.frame[index8].CellId != index7)
            ++index8;
          if (index8 < this.e.TotalFrames)
            index6 = index8;
        }
        else
          break;
      }
      if (index2 >= 0)
        this.e.DragObj[this.e.CurDragObj].ObjPoint[0].X = this.e.frame[index2].ScrX;
      if (index6 >= 0)
        this.e.DragObj[this.e.CurDragObj].ObjPoint[1].X = this.e.frame[index6].ScrX + this.e.frame[index6].ScrWidth;
      this.e.DragObj[this.e.CurDragObj].id2 = index1;
      this.e.DragObj[this.e.CurDragObj].id3 = index5;
    }
    if (this.e.DragObj[this.e.CurDragObj].drawn)
      this.DrawDragRowLine();
    this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].top += DeltaY;
    this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].bottom += DeltaY;
    for (int index = 0; index < this.e.DragObj[this.e.CurDragObj].ObjPointCount; ++index)
      this.e.DragObj[this.e.CurDragObj].ObjPoint[index].Y += DeltaY;
    this.DrawDragRowLine();
    ++this.e.TerArg.modified;
    return true;
  }

  internal new bool DragRulerIndent(int DeltaX)
  {
    if (this.e.DragObj[this.e.CurDragObj].drawn)
      this.DrawDragRulerIndent();
    this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].left += DeltaX;
    this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].right += DeltaX;
    for (int index = 0; index < this.e.DragObj[this.e.CurDragObj].ObjPointCount; ++index)
      this.e.DragObj[this.e.CurDragObj].ObjPoint[index].X += DeltaX;
    ++this.e.TerArg.modified;
    this.e.RulerPending = true;
    this.DrawDragRulerIndent();
    return true;
  }

  internal new bool DragRulerTab(int DeltaX, int LastY)
  {
    if (this.e.DragObj[this.e.CurDragObj].drawn)
      this.DrawDragRulerTab(LastY);
    this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].left += DeltaX;
    this.e.DragObj[this.e.CurDragObj].HotRect[this.e.CurHotSpot].right += DeltaX;
    for (int index = 0; index < this.e.DragObj[this.e.CurDragObj].ObjPointCount; ++index)
      this.e.DragObj[this.e.CurDragObj].ObjPoint[index].X += DeltaX;
    ++this.e.TerArg.modified;
    this.e.RulerPending = true;
    this.DrawDragRulerTab(LastY);
    return true;
  }

  internal new bool DragText(int x, int y)
  {
    if (this.e.HilightType != 0)
    {
      if (this.e.Cursor == tc.DragOutCur || this.e.Cursor == tc.DragInCur)
      {
        y -= 10;
        x -= 10;
      }
      else if (this.e.Cursor == tc.DragInCopyCur)
      {
        y -= 14;
        x -= 10;
      }
      this.TerMousePos((y << 16 /*0x10*/) + x, true);
      if (this.e.MouseOverShoot == ' ')
      {
        this.NormalizeBlock();
        int abs1 = this.RowColToAbs(this.e.HilightBegRow, this.e.HilightBegCol);
        int abs2 = this.RowColToAbs(this.e.HilightEndRow, this.e.HilightEndCol);
        int abs3 = this.RowColToAbs(this.e.MouseLine, this.e.MouseCol);
        if (abs1 > abs2)
          this.SwapInts(ref abs1, ref abs2);
        string rtfSel;
        if ((abs3 < abs1 || abs3 >= abs2) && ((this.e.TerFlags4 & 2097152 /*0x200000*/) == 0 || !this.IsProtected(true, true)) && (rtfSel = this.e.TerGetRtfSel()) != null)
        {
          int length = rtfSel.Length;
          this.e.CurLine = this.e.MouseLine;
          this.e.CurCol = this.e.MouseCol;
          if (!this.CanInsert(this.e.CurLine, this.e.CurCol))
          {
            this.MessageBeep(0);
          }
          else
          {
            bool flag = ((uint) this.GetKeyState(17) & 32768U /*0x8000*/) > 0U;
            if (this.e.HilightType == 2 && !flag)
            {
              int terFlags = this.e.TerFlags;
              if ((this.e.TerFlags4 & 4194304 /*0x400000*/) != 0)
                this.e.TerFlags |= 256 /*0x0100*/;
              if (this.e.TerDeleteBlock(true))
              {
                --this.e.UndoRef;
                if (abs3 >= abs2)
                  abs3 -= abs2 - abs1;
                int row;
                int col;
                this.AbsToRowCol(abs3, out row, out col);
                this.e.CurLine = row;
                this.e.CurCol = col;
              }
              this.e.TerFlags = terFlags;
            }
            this.e.InsertRtfBuf(rtfSel, this.e.CurLine, this.e.CurCol, false);
            if (this.e.CurLine >= this.e.TotalLines)
              this.e.CurLine = this.e.TotalLines - 1;
            if (this.e.CurLine - this.e.BeginLine >= this.e.WinHeight || this.e.CurLine - this.e.BeginLine < 0)
              this.e.BeginLine = this.e.CurLine - this.e.WinHeight / 2;
            if (this.e.BeginLine < 0)
              this.e.BeginLine = 0;
            this.e.CurRow = this.e.CurLine - this.e.BeginLine;
            if (this.e.CurRow < 0)
              this.e.CurRow = 0;
          }
        }
      }
    }
    this.e.HilightType = 0;
    this.e.DraggingText = false;
    this.e.PaintFlag = 4;
    this.PaintTer();
    return true;
  }

  internal new bool DrawDragCellLine()
  {
    this.TerDrawLine(this.e.DragObj[this.e.CurDragObj].ObjPoint[0].X, this.e.DragObj[this.e.CurDragObj].ObjPoint[0].Y, this.e.DragObj[this.e.CurDragObj].ObjPoint[1].X, this.e.DragObj[this.e.CurDragObj].ObjPoint[1].Y, tc.FocusPen, this.e.TerWinRect, true, true);
    if (this.e.TerArg.ruler)
    {
      int y1 = this.e.TerWinOrgY - (this.e.TerWinRect.top - this.e.RulerRect.top);
      int y2 = y1 + (this.e.RulerRect.bottom - this.e.RulerRect.top);
      this.TerDrawLine(this.e.DragObj[this.e.CurDragObj].ObjPoint[0].X, y1, this.e.DragObj[this.e.CurDragObj].ObjPoint[1].X, y2, new Pen(Color.Black), this.e.RulerRect, true, true);
    }
    this.e.DragObj[this.e.CurDragObj].drawn = !this.e.DragObj[this.e.CurDragObj].drawn;
    return true;
  }

  internal new bool DrawDragFrameRect()
  {
    for (int index = 0; index < this.e.TotalDragObjs; ++index)
    {
      if (this.e.DragObj[index].InUse && (this.e.DragObj[index].type == 2 || this.e.DragObj[index].type == 1))
      {
        if ((this.e.ParaFrame[this.e.DragObj[index].type != 2 ? this.e.TerFont[this.e.DragObj[index].id1].ParaFID : this.e.DragObj[index].id1].flags & 256 /*0x0100*/) != 0)
        {
          this.TerDrawLine(this.e.DragObj[index].ObjPoint[0].X, this.e.DragObj[index].ObjPoint[0].Y, this.e.DragObj[index].ObjPoint[1].X, this.e.DragObj[index].ObjPoint[1].Y, tc.FocusPen, tc.SkipRect, false, true);
          break;
        }
        this.TerDrawRect(this.e.DragObj[index].ObjRect, tc.FocusPen, true, true);
        break;
      }
    }
    this.e.FrameRectHilighted = !this.e.FrameRectHilighted;
    return true;
  }

  internal new bool DrawDragFrameTabs()
  {
    this.DrawDragHotSpots(2);
    this.e.FrameTabsHilighted = !this.e.FrameTabsHilighted;
    return true;
  }

  internal new bool DrawDragHotSpots(int type)
  {
    this.TerSetClipRgn();
    for (int index1 = 0; index1 < this.e.TotalDragObjs; ++index1)
    {
      if (this.e.DragObj[index1].InUse && this.e.DragObj[index1].type == type)
      {
        for (int index2 = 0; index2 < this.e.DragObj[index1].HotRectCount; ++index2)
          this.e.InvertRectangle(this.e.DragObj[index1].HotRect[index2]);
      }
    }
    this.TerResetClipRgn();
    return true;
  }

  internal new bool DrawDragPictRect()
  {
    for (int index = 0; index < this.e.TotalDragObjs; ++index)
    {
      if (this.e.DragObj[index].InUse && this.e.DragObj[index].type == 1)
      {
        this.TerDrawRect(this.e.DragObj[index].ObjRect, tc.FocusPen, true, true);
        break;
      }
    }
    this.DrawDragHotSpots(1);
    this.e.PictureHilighted = !this.e.PictureHilighted;
    return true;
  }

  internal new bool DrawDragRowLine()
  {
    this.TerDrawLine(this.e.DragObj[this.e.CurDragObj].ObjPoint[0].X, this.e.DragObj[this.e.CurDragObj].ObjPoint[0].Y, this.e.DragObj[this.e.CurDragObj].ObjPoint[1].X, this.e.DragObj[this.e.CurDragObj].ObjPoint[1].Y, tc.FocusPen, this.e.TerWinRect, true, true);
    this.e.DragObj[this.e.CurDragObj].drawn = !this.e.DragObj[this.e.CurDragObj].drawn;
    return true;
  }

  internal new bool DrawDragRulerIndent()
  {
    Point[] InPt = new Point[6];
    for (int index = 0; index < this.e.DragObj[this.e.CurDragObj].ObjPointCount; ++index)
    {
      InPt[index] = this.e.DragObj[this.e.CurDragObj].ObjPoint[index];
      InPt[index].Y = InPt[index].Y + this.e.TerWinOrgY + this.e.RulerRect.top - this.e.TerWinRect.top;
    }
    this.TerDrawPolygon(InPt, this.e.DragObj[this.e.CurDragObj].ObjPointCount, Pens.Black, (Brush) null, this.e.RulerRect, true, true);
    int index1 = this.e.DragObj[this.e.CurDragObj].ObjPointCount != 5 ? 1 : 0;
    this.TerDrawLine(this.e.DragObj[this.e.CurDragObj].ObjPoint[index1].X, this.e.TerWinOrgY, this.e.DragObj[this.e.CurDragObj].ObjPoint[index1].X, this.e.TerWinOrgY + this.e.TerWinRect.bottom - this.e.TerWinRect.top, tc.FocusPen, tc.SkipRect, false, true);
    this.e.DragObj[this.e.CurDragObj].drawn = !this.e.DragObj[this.e.CurDragObj].drawn;
    return true;
  }

  internal new bool DrawDragRulerTab(int LastY)
  {
    if ((LastY < this.e.RulerRect.top || LastY > this.e.RulerRect.bottom ? 0 : 1) != 0 || this.e.DragObj[this.e.CurDragObj].drawn)
    {
      this.TerDrawLine(this.e.DragObj[this.e.CurDragObj].ObjPoint[0].X, this.e.DragObj[this.e.CurDragObj].ObjPoint[0].Y, this.e.DragObj[this.e.CurDragObj].ObjPoint[1].X, this.e.DragObj[this.e.CurDragObj].ObjPoint[1].Y, tc.FocusPen, this.e.RulerRect, true, true);
      this.TerDrawLine(this.e.DragObj[this.e.CurDragObj].ObjPoint[1].X, this.e.DragObj[this.e.CurDragObj].ObjPoint[1].Y, this.e.DragObj[this.e.CurDragObj].ObjPoint[1].X, this.e.TerWinOrgY + this.e.TerWinRect.bottom - this.e.TerWinRect.top, tc.FocusPen, this.e.TerWinRect, true, true);
      this.e.DragObj[this.e.CurDragObj].drawn = !this.e.DragObj[this.e.CurDragObj].drawn;
    }
    return true;
  }

  internal bool DropFile(string file)
  {
    string upper = new FileInfo(file).Extension.ToUpper();
    if (upper == ".RTF" && this.e.TerInsertRtfFile(file, -1, 0, true))
      return true;
    if (upper == ".TXT" || upper == ".BAT" || upper == ".SYS")
    {
      int pSize;
      return this.e.InsertTerText(new string(this.e.TerFileToMem(file, out pSize), 0, pSize), true);
    }
    return (upper == ".BMP" || upper == ".JPG" || upper == ".PNG" || upper == ".TIF" || upper == ".GIF") && this.e.TerInsertPictureFile(file, (this.e.TerFlags6 & 4096 /*0x1000*/) == 0, 0, true) > 0;
  }

  internal new bool ExternalDrop(
    DataObject data,
    DragDropEffects effects,
    int KeyState,
    int x,
    int y)
  {
    int pVal1 = 0;
    int pVal2 = 0;
    Point client = this.e.PointToClient(new Point(x, y));
    this.TerMousePos((client.Y << 16 /*0x10*/) + client.X, false);
    if (!this.CanInsert(this.e.MouseLine, this.e.MouseCol))
    {
      this.MessageBeep(0);
      this.PaintTer();
    }
    else
    {
      if (this.e.InOleDrag && this.e.HilightType != 0)
      {
        this.NormalizeBlock();
        pVal1 = this.RowColToAbs(this.e.HilightBegRow, this.e.HilightBegCol);
        pVal2 = this.RowColToAbs(this.e.HilightEndRow, this.e.HilightEndCol);
        int abs = this.RowColToAbs(this.e.MouseLine, this.e.MouseCol);
        if (pVal1 > pVal2)
          this.SwapInts(ref pVal1, ref pVal2);
        if (abs >= pVal1 && abs < pVal2)
        {
          this.e.HilightType = 0;
          this.PaintTer();
          goto label_11;
        }
      }
      this.e.CurLine = this.e.MouseLine;
      this.e.CurCol = this.e.MouseCol;
      int abs1 = this.RowColToAbs(this.e.CurLine, this.e.CurCol);
      bool flag = this.e.InOleDrag && this.e.HilightType != 0 && abs1 < pVal1;
      if (data.GetDataPresent(DataFormats.FileDrop))
      {
        this.DropFile((string) ((Array) data.GetData(DataFormats.FileDrop)).GetValue(0));
      }
      else
      {
        int hilightType = this.e.HilightType;
        this.CopyFromClipboard("", data);
        this.e.HilightType = hilightType;
        if (flag)
        {
          int abs2 = this.RowColToAbs(this.e.CurLine, this.e.CurCol);
          pVal1 += abs2 - abs1;
          int abs3 = pVal2 + (abs2 - abs1);
          this.AbsToRowCol(pVal1, 'B');
          this.AbsToRowCol(abs3, 'E');
        }
      }
    }
label_11:
    return true;
  }

  internal new int GetDragObjectSlot()
  {
    int dragObjectSlot;
    for (dragObjectSlot = 0; dragObjectSlot < this.e.TotalDragObjs; ++dragObjectSlot)
    {
      if (!this.e.DragObj[dragObjectSlot].InUse)
        goto label_14;
    }
    if (this.e.TotalDragObjs >= this.e.MaxDragObjs)
    {
      if (this.e.MaxDragObjs < 100)
        this.e.MaxDragObjs += 20;
      else
        this.e.MaxDragObjs += 50;
      this.e.DragObj = this.ReAlloc(this.e.DragObj, this.e.MaxDragObjs + 1);
    }
    if (this.e.TotalDragObjs < this.e.MaxDragObjs)
    {
      dragObjectSlot = this.e.TotalDragObjs;
      ++this.e.TotalDragObjs;
    }
    else
    {
      if ((this.e.MessageDisplayed & 1) == 0)
      {
        this.PrintError(105, (string) null);
        this.e.MessageDisplayed |= 1;
      }
      return -1;
    }
label_14:
    this.e.DragObj[dragObjectSlot] = new tc.StrDragObj();
    this.e.DragObj[dragObjectSlot].ObjPoint = new Point[6];
    this.e.DragObj[dragObjectSlot].HotRect = new COp.RECT[8];
    return dragObjectSlot;
  }

  internal new bool OleDragText()
  {
    int modified = this.e.TerArg.modified;
    if (this.e.HilightType != 0 && this.e.text[this.e.HilightBegRow].cid > 0 && this.e.text[this.e.HilightBegRow].cid == this.e.text[this.e.HilightEndRow].cid)
    {
      this.NormalizeBlock();
      if (this.LineInfo(this.e.HilightEndRow, 16 /*0x10*/) && this.e.HilightEndCol == this.e.text[this.e.HilightEndRow].len && this.e.HilightEndCol > 0)
        --this.e.HilightEndCol;
    }
    OurDataObject data = new OurDataObject(this.e);
    this.e.InOleDrag = true;
    this.e.TextDragged = false;
    DragDropEffects allowedEffects = DragDropEffects.Copy;
    if (!this.e.TerArg.ReadOnly)
      allowedEffects |= DragDropEffects.Move;
    DragDropEffects dragDropEffects = this.e.DoDragDrop((object) data, allowedEffects);
    this.e.InOleDrag = false;
    this.e.TextDragged = (dragDropEffects & DragDropEffects.Move) == DragDropEffects.Move || (dragDropEffects & DragDropEffects.Copy) == DragDropEffects.Copy;
    if ((dragDropEffects & DragDropEffects.Move) == DragDropEffects.Move && this.e.HilightType != 0)
    {
      int terFlags = this.e.TerFlags;
      if ((this.e.TerFlags4 & 4194304 /*0x400000*/) != 0)
        this.e.TerFlags |= 256 /*0x0100*/;
      if (this.e.TerArg.modified > modified)
        --this.e.UndoRef;
      this.e.TerDeleteBlock(true);
      this.e.TerFlags = terFlags;
    }
    this.e.DraggingText = false;
    return true;
  }

  internal new bool ShowFrameDragObjects(int FrameNo, int ParaFID)
  {
    int num1 = 0;
    int num2 = 0;
    int x1 = 0;
    int y1 = 0;
    int x2 = 0;
    int y2 = 0;
    bool flag = false;
    if (!this.e.FrameTabsHilighted)
    {
      int dragObjectSlot;
      if ((dragObjectSlot = this.GetDragObjectSlot()) < 0)
        return false;
      if (ParaFID < 0)
        ParaFID = this.e.frame[FrameNo].ParaFrameId;
      this.e.UndoParaFrame = this.e.ParaFrame[ParaFID].Copy();
      this.e.DragObj[dragObjectSlot].InUse = true;
      this.e.DragObj[dragObjectSlot].type = 2;
      this.e.DragObj[dragObjectSlot].id1 = ParaFID;
      if ((this.e.ParaFrame[ParaFID].flags & 256 /*0x0100*/) != 0)
        flag = true;
      if (flag)
        this.e.DragObj[dragObjectSlot].HotRectCount = 2;
      else
        this.e.DragObj[dragObjectSlot].HotRectCount = 4;
      int width = this.e.frame[FrameNo].width;
      int height = this.e.frame[FrameNo].height;
      if (height > 0)
        this.e.DragObj[dragObjectSlot].AspectRatio = (double) width / (double) height;
      if (flag)
      {
        this.GetLinePoints(FrameNo, out x1, out y1, out x2, out y2);
        this.e.DragObj[dragObjectSlot].ObjPointCount = 2;
        this.e.DragObj[dragObjectSlot].ObjPoint[0] = new Point(x1, y1);
        this.e.DragObj[dragObjectSlot].ObjPoint[1] = new Point(x2, y2);
        this.e.DragObj[dragObjectSlot].ObjPoint[2] = new Point(x1, y1);
        this.e.DragObj[dragObjectSlot].ObjPoint[3] = new Point(x2, y2);
      }
      else
      {
        this.e.DragObj[dragObjectSlot].ObjRect.left = num1 = this.e.frame[FrameNo].x;
        this.e.DragObj[dragObjectSlot].ObjRect.top = num2 = this.e.frame[FrameNo].y;
        this.e.DragObj[dragObjectSlot].ObjRect.right = this.e.DragObj[dragObjectSlot].ObjRect.left + width;
        this.e.DragObj[dragObjectSlot].ObjRect.bottom = this.e.DragObj[dragObjectSlot].ObjRect.top + height;
      }
      int scrX = this.TwipsToScrX(100);
      int scrY = this.TwipsToScrY(100);
      int num3 = scrX * 3 / 5;
      int num4 = scrY * 3 / 5;
      COp.RECT rect;
      if (flag)
      {
        rect.left = x1;
        rect.top = y1 - scrY / 2;
      }
      else
      {
        rect.left = num1 + 1;
        rect.top = num2 + (height - scrY) / 2;
      }
      rect.right = rect.left + num3;
      rect.bottom = rect.top + scrY;
      this.e.DragObj[dragObjectSlot].HotRect[0] = rect;
      if (flag)
      {
        rect.left = x2 - num3;
        rect.top = y2 - scrY / 2;
      }
      else
      {
        rect.left = num1 + width - num3 - 1;
        rect.top = num2 + (height - scrY) / 2;
      }
      rect.right = rect.left + num3;
      rect.bottom = rect.top + scrY;
      this.e.DragObj[dragObjectSlot].HotRect[1] = rect;
      if (!flag)
      {
        rect.left = num1 + (width - scrX) / 2;
        rect.right = rect.left + scrX;
        rect.top = num2 + 1;
        rect.bottom = rect.top + num4;
        this.e.DragObj[dragObjectSlot].HotRect[2] = rect;
        rect.left = num1 + (width - scrX) / 2;
        rect.right = rect.left + scrX;
        rect.top = num2 + height - num4 - 1;
        rect.bottom = num2 + height - 1;
        this.e.DragObj[dragObjectSlot].HotRect[3] = rect;
      }
      if (this.e.ParaFrame[ParaFID].pict > 0)
        this.ShowFrameMoveObjects(FrameNo, this.e.ParaFrame[ParaFID].pict);
      else
        this.ShowFrameMoveObjects(FrameNo, -1);
    }
    return true;
  }

  internal new bool ShowFrameMoveObjects(int FrameNo, int pict)
  {
    int x1 = 0;
    int x2 = 0;
    int y1 = 0;
    int y2 = 0;
    bool flag = false;
    int dragObjectSlot;
    if ((dragObjectSlot = this.GetDragObjectSlot()) < 0)
      return false;
    if (pict > 0)
      FrameNo = this.e.TerFont[pict].DispFrame;
    int index = this.e.frame[FrameNo].ParaFrameId;
    if (pict > 0 && this.e.TerFont[pict].ParaFID > 0)
      index = this.e.TerFont[pict].ParaFID;
    int x = this.e.frame[FrameNo].x;
    int y = this.e.frame[FrameNo].y;
    int width = this.e.frame[FrameNo].width;
    int height = this.e.frame[FrameNo].height;
    int scrX = this.TwipsToScrX(100);
    int scrY = this.TwipsToScrY(100);
    if ((this.e.ParaFrame[index].flags & 256 /*0x0100*/) != 0)
      flag = true;
    this.e.DragObj[dragObjectSlot].InUse = true;
    this.e.DragObj[dragObjectSlot].type = 3;
    this.e.DragObj[dragObjectSlot].id1 = this.e.frame[FrameNo].ParaFrameId;
    this.e.DragObj[dragObjectSlot].HotRectCount = 4;
    if (flag)
    {
      this.GetLinePoints(FrameNo, out x1, out y1, out x2, out y2);
      this.e.DragObj[dragObjectSlot].ObjPointCount = 2;
      this.e.DragObj[dragObjectSlot].ObjPoint[0] = new Point(x1, y1);
      this.e.DragObj[dragObjectSlot].ObjPoint[1] = new Point(x2, y2);
    }
    else
    {
      this.e.DragObj[dragObjectSlot].ObjRect.left = x;
      this.e.DragObj[dragObjectSlot].ObjRect.top = y;
      this.e.DragObj[dragObjectSlot].ObjRect.right = this.e.DragObj[dragObjectSlot].ObjRect.left + width;
      this.e.DragObj[dragObjectSlot].ObjRect.bottom = this.e.DragObj[dragObjectSlot].ObjRect.top + height;
    }
    if (flag)
    {
      this.e.DragObj[dragObjectSlot].IsHotPolygon = true;
      this.e.DragObj[dragObjectSlot].HotRect[0].left = x1 + scrX;
      this.e.DragObj[dragObjectSlot].HotRect[0].top = y1 - scrX;
      this.e.DragObj[dragObjectSlot].HotRect[1].left = x1 + scrX;
      this.e.DragObj[dragObjectSlot].HotRect[1].top = y1 + scrX;
      this.e.DragObj[dragObjectSlot].HotRect[2].left = x2 - scrX;
      this.e.DragObj[dragObjectSlot].HotRect[2].top = y2 + scrX;
      this.e.DragObj[dragObjectSlot].HotRect[3].left = x2 - scrX;
      this.e.DragObj[dragObjectSlot].HotRect[3].top = y2 - scrX;
    }
    else if (pict > 0 && (this.e.ParaFrame[index].ShapeType == 0 || this.e.ParaFrame[index].ShapeType == 75))
    {
      this.e.DragObj[dragObjectSlot].HotRect[0] = this.e.DragObj[dragObjectSlot].ObjRect;
      this.e.DragObj[dragObjectSlot].HotRect[1] = this.e.DragObj[dragObjectSlot].ObjRect;
      this.e.DragObj[dragObjectSlot].HotRect[2] = this.e.DragObj[dragObjectSlot].ObjRect;
      this.e.DragObj[dragObjectSlot].HotRect[3] = this.e.DragObj[dragObjectSlot].ObjRect;
    }
    else
    {
      COp.RECT rect;
      rect.left = x - scrX;
      rect.right = x;
      rect.top = y;
      rect.bottom = rect.top + height;
      this.e.DragObj[dragObjectSlot].HotRect[0] = rect;
      rect.left = x + width;
      rect.right = rect.left + scrX;
      rect.top = y;
      rect.bottom = rect.top + height;
      this.e.DragObj[dragObjectSlot].HotRect[1] = rect;
      rect.left = x - scrX;
      rect.right = x + width + scrX;
      rect.top = y - scrY;
      rect.bottom = y;
      this.e.DragObj[dragObjectSlot].HotRect[2] = rect;
      rect.left = x - scrX;
      rect.right = x + width + scrX;
      rect.top = y + height;
      rect.bottom = rect.top + scrY;
      this.e.DragObj[dragObjectSlot].HotRect[3] = rect;
    }
    this.e.FrameRectHilighted = false;
    this.DrawDragFrameTabs();
    return true;
  }

  internal new bool ShowPictureDragObjects(int pict)
  {
    if (this.e.PictureHilighted)
    {
      if (this.e.CurDragObj > 0 && this.e.DragObj[this.e.CurDragObj].InUse && this.e.DragObj[this.e.CurDragObj].type == 1 && this.e.DragObj[this.e.CurDragObj].id1 == pict)
        return true;
      this.DrawDragPictRect();
      this.DeleteDragObjects(1, 3);
    }
    int dragObjectSlot;
    if ((dragObjectSlot = this.GetDragObjectSlot()) < 0)
      return false;
    this.e.DragObj[dragObjectSlot].InUse = true;
    this.e.DragObj[dragObjectSlot].type = 1;
    this.e.DragObj[dragObjectSlot].id1 = pict;
    this.e.DragObj[dragObjectSlot].HotRectCount = 8;
    this.e.UndoInt1 = this.e.TerFont[pict].PictWidth;
    this.e.UndoInt2 = this.e.TerFont[pict].PictHeight;
    ref tc.StrFont local = ref this.e.TerFont[pict];
    int scrX1 = this.TwipsToScrX(this.e.TerFont[pict].PictWidth);
    int scrY1 = this.TwipsToScrY(this.e.TerFont[pict].PictHeight);
    if (scrY1 > 0)
      this.e.DragObj[dragObjectSlot].AspectRatio = (double) scrX1 / (double) scrY1;
    int num1 = this.e.TerFont[pict].PictY + this.e.TerWinOrgY;
    this.e.DragObj[dragObjectSlot].ObjRect.top = num1;
    this.e.DragObj[dragObjectSlot].ObjRect.bottom = this.e.DragObj[dragObjectSlot].ObjRect.top + scrY1;
    int num2;
    this.e.DragObj[dragObjectSlot].ObjRect.left = num2 = this.e.TerFont[pict].PictX + this.e.TerWinOrgX;
    this.e.DragObj[dragObjectSlot].ObjRect.right = this.e.DragObj[dragObjectSlot].ObjRect.left + scrX1;
    int scrX2 = this.TwipsToScrX(100);
    int scrY2 = this.TwipsToScrY(100);
    COp.RECT rect;
    rect.left = num2;
    rect.right = rect.left + scrX2;
    rect.top = num1 + (scrY1 - scrY2) / 2;
    rect.bottom = rect.top + scrY2;
    this.e.DragObj[dragObjectSlot].HotRect[0] = rect;
    rect.left = num2 + scrX1 - scrX2;
    rect.right = rect.left + scrX2;
    rect.top = num1 + (scrY1 - scrY2) / 2;
    rect.bottom = rect.top + scrY2;
    this.e.DragObj[dragObjectSlot].HotRect[1] = rect;
    rect.left = num2 + (scrX1 - scrX2) / 2;
    rect.right = rect.left + scrX2;
    rect.top = num1;
    rect.bottom = rect.top + scrY2;
    this.e.DragObj[dragObjectSlot].HotRect[2] = rect;
    rect.left = num2 + (scrX1 - scrX2) / 2;
    rect.right = rect.left + scrX2;
    rect.top = num1 + scrY1 - scrY2;
    rect.bottom = rect.top + scrY2;
    this.e.DragObj[dragObjectSlot].HotRect[3] = rect;
    rect.left = num2;
    rect.right = rect.left + scrX2;
    rect.top = num1;
    rect.bottom = rect.top + scrY2;
    this.e.DragObj[dragObjectSlot].HotRect[4] = rect;
    rect.left = num2 + scrX1 - scrX2;
    rect.right = rect.left + scrX2;
    rect.top = num1;
    rect.bottom = rect.top + scrY2;
    this.e.DragObj[dragObjectSlot].HotRect[5] = rect;
    rect.left = num2;
    rect.right = rect.left + scrX2;
    rect.top = num1 + scrY1 - scrY2;
    rect.bottom = rect.top + scrY2;
    this.e.DragObj[dragObjectSlot].HotRect[6] = rect;
    rect.left = num2 + scrX1 - scrX2;
    rect.right = rect.left + scrX2;
    rect.top = num1 + scrY1 - scrY2;
    rect.bottom = rect.top + scrY2;
    this.e.DragObj[dragObjectSlot].HotRect[7] = rect;
    this.DrawDragPictRect();
    if (this.e.TerFont[pict].ParaFID > 0)
      this.e.UndoParaFrame = this.e.ParaFrame[this.e.TerFont[pict].ParaFID].Copy();
    if (this.e.TerFont[pict].FrameType == 3)
      this.ShowFrameMoveObjects(-1, pict);
    return true;
  }

  internal new bool TerDragObject(int lParam)
  {
    COp.MSG msg = new COp.MSG();
    int DeltaX1 = 0;
    int DeltaY1 = 0;
    int num = 0;
    if (this.e.CurDragObj < 0 || !this.e.DragObj[this.e.CurDragObj].InUse)
      return false;
    this.e.Capture = true;
    int LastX;
    int LastY;
    while (true)
    {
      LastX = (int) (short) COp.LOWORD(lParam);
      LastY = (int) (short) COp.HIWORD(lParam);
      if (this.e.DragObj[this.e.CurDragObj].type == 3)
        num = this.e.ScrResX / 10;
      if (LastX < num)
        LastX = num;
      if (LastX > this.e.TerRect.right - num)
        LastX = this.e.TerRect.right - num;
      if (LastY < 0)
        LastY = 0;
      if (LastY > this.e.TerRect.bottom)
        LastY = this.e.TerRect.bottom;
      int DeltaX2 = LastX - this.e.MouseX;
      int DeltaY2 = LastY - this.e.MouseY;
      DeltaX1 += DeltaX2;
      DeltaY1 += DeltaY2;
      if (this.e.DragObj[this.e.CurDragObj].type == 1)
        this.DragPictFrameSize(1, DeltaX2, DeltaY2);
      else if (this.e.DragObj[this.e.CurDragObj].type == 9)
        this.DragCellSize(DeltaX2);
      else if (this.e.DragObj[this.e.CurDragObj].type == 11)
        this.DragRowSize(DeltaY2);
      else if (this.e.DragObj[this.e.CurDragObj].type == 2)
        this.DragPictFrameSize(2, DeltaX2, DeltaY2);
      else if (this.e.DragObj[this.e.CurDragObj].type == 3)
        this.DragFrameMove(DeltaX2, DeltaY2);
      else if (this.e.DragObj[this.e.CurDragObj].type == 10)
        this.DragRowIndent(DeltaX2);
      else if (this.e.DragObj[this.e.CurDragObj].type == 5)
        this.DragRulerIndent(DeltaX2);
      else if (this.e.DragObj[this.e.CurDragObj].type == 6)
        this.DragRulerIndent(DeltaX2);
      else if (this.e.DragObj[this.e.CurDragObj].type == 7)
        this.DragRulerIndent(DeltaX2);
      else if (this.e.DragObj[this.e.CurDragObj].type == 8)
        this.DragRulerTab(DeltaX2, LastY);
      this.e.MouseX += DeltaX2;
      this.e.MouseY += DeltaY2;
      while (!this.PeekMessage(out msg, IntPtr.Zero, 512 /*0x0200*/, 512 /*0x0200*/, 2))
      {
        if (this.PeekMessage(out msg, IntPtr.Zero, 514, 514, 2) || this.PeekMessage(out msg, IntPtr.Zero, 517, 517, 2) || this.PeekMessage(out msg, IntPtr.Zero, 520, 520, 2))
          goto label_39;
      }
      if (this.PeekMessage(out msg, IntPtr.Zero, 512 /*0x0200*/, 512 /*0x0200*/, 3))
      {
        try
        {
          lParam = msg.lParam.ToInt32();
        }
        catch (Exception ex)
        {
        }
      }
      else
        break;
    }
label_39:
    this.DragApply(DeltaX1, DeltaY1, LastX, LastY);
    return true;
  }

  internal bool TerDrawLine(
    int x1,
    int y1,
    int x2,
    int y2,
    Pen pen,
    COp.RECT ClpRect,
    bool clip,
    bool DoDrag)
  {
    if (tc.DebugMode)
      this.misc.dm(nameof (TerDrawLine));
    if (clip)
    {
      int num = this.e.TerWinOrgY - this.e.TerWinRect.top;
      this.e.draw.SetTerGraphicsClip(new Rectangle(ClpRect.left, ClpRect.top + num, ClpRect.right - ClpRect.left, ClpRect.bottom - ClpRect.top));
    }
    if (DoDrag)
      this.e.InvertLine(x1, y1, x2, y2);
    else
      this.e.TerGr.DrawLine(pen, x1, y1, x2, y2);
    if (clip)
      this.TerSetClipRgn();
    return true;
  }

  internal bool TerDrawPolygon(
    Point[] InPt,
    int PointCount,
    Pen pen,
    Brush hBrush,
    COp.RECT ClpRect,
    bool clip,
    bool DoDrag)
  {
    if (tc.DebugMode)
      this.misc.dm(nameof (TerDrawPolygon));
    if (clip)
    {
      int num = this.e.TerWinOrgY - this.e.TerWinRect.top;
      this.e.draw.SetTerGraphicsClip(new Rectangle(ClpRect.left, ClpRect.top + num, ClpRect.right - ClpRect.left, ClpRect.bottom - ClpRect.top));
    }
    if (DoDrag)
    {
      int index;
      for (index = 0; index + 1 < PointCount; ++index)
        this.TerDrawLine(InPt[index].X, InPt[index].Y, InPt[index + 1].X, InPt[index + 1].Y, pen, ClpRect, false, true);
      this.TerDrawLine(InPt[index].X, InPt[index].Y, InPt[0].X, InPt[0].Y, pen, ClpRect, false, true);
    }
    else
    {
      Point[] points = new Point[PointCount];
      for (int index = 0; index < PointCount; ++index)
        points[index] = InPt[index];
      this.e.TerGr.DrawPolygon(tc.FocusPen, points);
    }
    if (clip)
      this.TerSetClipRgn();
    return true;
  }

  internal new bool TerDrawRect(COp.RECT rect, Pen pen, bool clip, bool DoDrag)
  {
    Point[] points = new Point[5];
    if (tc.DebugMode)
      this.misc.dm(nameof (TerDrawRect));
    if (clip)
      this.TerSetClipRgn();
    if (DoDrag)
    {
      this.TerDrawLine(rect.left, rect.top, rect.right, rect.top, pen, rect, false, true);
      this.TerDrawLine(rect.right, rect.top, rect.right, rect.bottom, pen, rect, false, true);
      this.TerDrawLine(rect.right, rect.bottom, rect.left, rect.bottom, pen, rect, false, true);
      this.TerDrawLine(rect.left, rect.bottom, rect.left, rect.top, pen, rect, false, true);
    }
    else
    {
      points[0].X = rect.left;
      points[0].Y = rect.top;
      points[1].X = rect.right;
      points[1].Y = rect.top;
      points[2].X = rect.right;
      points[2].Y = rect.bottom;
      points[3].X = rect.left;
      points[3].Y = rect.bottom;
      points[4].X = rect.left;
      points[4].Y = rect.top;
      this.e.TerGr.DrawPolygon(pen, points);
    }
    if (clip)
      this.TerSetClipRgn();
    return true;
  }
}
