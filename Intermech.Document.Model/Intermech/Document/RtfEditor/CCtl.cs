// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.CCtl
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class CCtl : COp
{
  internal CCtl(ImRtfEditor ImRtfEditor)
    : base(ImRtfEditor)
  {
    this.e = ImRtfEditor;
  }

  internal void OnDoubleClick(EventArgs ev)
  {
    if (this.e.MouseOverShoot != ' ' || !this.e.MouseOnTextLine)
      return;
    this.TerDoubleClick();
  }

  internal void OnDragDrop(DragEventArgs ev)
  {
    this.ExternalDrop((DataObject) ev.Data, ev.AllowedEffect, ev.KeyState, ev.X, ev.Y);
    this.e.InDragDrop = false;
  }

  internal void OnDragEnter(DragEventArgs ev) => this.e.InDragDrop = true;

  internal void OnDragLeave(EventArgs ev) => this.e.InDragDrop = false;

  internal void OnDragOver(DragEventArgs ev)
  {
    Point client = this.e.PointToClient(new Point(ev.X, ev.Y));
    int lParam = (client.Y << 16 /*0x10*/) + client.X;
    this.e.InDragDrop = true;
    this.SetDragCaret(lParam);
    if (client.Y > this.e.TerWinRect.bottom - 20)
      this.TerWinDown();
    else if (client.Y > this.e.TerWinRect.bottom - 10)
      this.TerPageDn(false);
    else if (client.Y < this.e.TerWinRect.top + 20)
      this.TerWinUp();
    else if (client.Y < this.e.TerWinRect.top + 10)
      this.TerPageUp(false);
    if ((ev.KeyState & 4) == 4)
    {
      if ((ev.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)
        ev.Effect = DragDropEffects.Move;
    }
    else if ((ev.KeyState & 8) == 8)
    {
      if ((ev.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
        ev.Effect = DragDropEffects.Copy;
    }
    else
      ev.Effect = (ev.AllowedEffect & DragDropEffects.Move) != DragDropEffects.Move ? ((ev.AllowedEffect & DragDropEffects.Link) != DragDropEffects.Link ? ((ev.AllowedEffect & DragDropEffects.Copy) != DragDropEffects.Copy ? DragDropEffects.None : DragDropEffects.Copy) : DragDropEffects.Link) : DragDropEffects.Move;
    if (!this.e.TerArg.ReadOnly)
      return;
    ev.Effect = DragDropEffects.None;
  }

  internal void OnGotFocus(EventArgs ev)
  {
    if (this.e.HoldMessages)
      return;
    if (this.e.FontsReleased)
      this.RecreateFonts(this.e.TerGr);
    this.InitCaret();
    this.e.TerOpFlags |= 64 /*0x40*/;
    this.e.IgnoreMouseMove = true;
    if (this.e.PictureClicked && !this.e.PictureHilighted)
      this.DrawDragPictRect();
    if (!this.e.FrameClicked || this.e.FrameTabsHilighted)
      return;
    this.DrawDragFrameTabs();
  }

  internal void OnHScroll(int wParam)
  {
    if (this.e.CaretEngaged)
      this.DisengageCaret();
    if (this.e.WheelShowing)
      this.ResetWheel();
    if (this.e.PictureClicked)
    {
      this.e.HilightType = 0;
      this.e.PictureClicked = false;
    }
    switch (COp.LOWORD(wParam))
    {
      case 0:
        this.TerPageLeft(false);
        break;
      case 1:
        this.TerPageRight(false);
        break;
      case 2:
        this.TerPageLeft(true);
        break;
      case 3:
        this.TerPageRight(true);
        break;
      case 4:
        if (!this.e.InPrintPreview)
        {
          this.TerPageHorz('T', (int) COp.HIWORD(wParam));
          break;
        }
        this.PreviewPageHorz((int) COp.HIWORD(wParam));
        break;
    }
  }

  internal void OnKeyPress(KeyPressEventArgs ev)
  {
    if (this.e.TerArg.ReadOnly && !this.EditingInputField(false, false))
      return;
    char keyChar = ev.KeyChar;
    if (this.e.HilightType != 0 && (this.e.StretchHilight || this.e.TblSelCursShowing) && !this.e.IgnoreMouseMove)
    {
      this.MessageBeep(0);
    }
    else
    {
      if (!this.e.CaretEnabled && this.UseCaret())
        this.InitCaret();
      if (!this.e.CaretEngaged && !this.e.InPrintPreview)
        this.EngageCaret(0);
      if (this.e.WheelShowing)
        this.ResetWheel();
      if (keyChar <= '\u001A')
        return;
      this.TerAscii(keyChar);
    }
  }

  internal void OnLostFocus(EventArgs ev)
  {
    if (this.e.HoldMessages)
      return;
    if (this.e.CaretEngaged)
      this.DisengageCaret();
    if (this.e.WheelShowing)
      this.ResetWheel();
    this.TerDestroyCaret();
    if (this.e.PictureHilighted)
      this.DrawDragPictRect();
    if (!this.e.FrameTabsHilighted)
      return;
    this.DrawDragFrameTabs();
  }

  internal void OnMouseDown(MouseEventArgs ev)
  {
    bool flag = !this.e.TerArg.ReadOnly;
    this.e.MouseDownPoint = new Point(ev.X, ev.Y);
    int lParam = (ev.Y << 16 /*0x10*/) + ev.X;
    if (ev.Button == MouseButtons.Left || ev.Button == MouseButtons.Right)
    {
      if (this.e.WheelShowing)
        this.ResetWheel();
      if ((this.e.TerOpFlags & 262144 /*0x040000*/) != 0 || !this.TerLButtonDown(ev.Button, lParam))
        return;
      this.e.CurInputField = 0;
      if (this.e.CurDragObj >= 0 || ev.Button != MouseButtons.Right)
        return;
      if (this.e.SpellCheckerPopped)
      {
        this.e.SpellCheckerPopped = false;
        this.e.ContextMenu = this.e.OrgContextMenu;
      }
      if (this.DoAutoSpellCheck() && this.spl.OnMisspelledWord(lParam))
      {
        this.e.OrgContextMenu = this.e.ContextMenu;
        this.e.ContextMenu = (ContextMenu) null;
        this.e.SpellCheckerPopped = true;
      }
      if (!flag)
        return;
      this.TerMousePos(lParam, true);
      if (this.e.RulerClicked && this.e.CurDragObj < 0)
        this.DoRulerClick(ev.Button, lParam);
      this.e.CurDragObj = -1;
    }
    else
    {
      if (ev.Button != MouseButtons.Middle)
        return;
      if (this.e.WheelShowing)
      {
        this.ResetWheel();
      }
      else
      {
        if (this.e.InPrinting || !SystemInformation.MouseWheelPresent)
          return;
        this.ActivateWheel(lParam);
      }
    }
  }

  internal void OnMouseMove(MouseEventArgs ev)
  {
    if (ev.Button == MouseButtons.Left && this.e.MouseDownPoint.X - ev.X == 0 && this.e.MouseDownPoint.Y - ev.Y == 0)
      return;
    int lParam = (ev.Y << 16 /*0x10*/) + ev.X;
    if (this.e.WheelShowing)
    {
      this.SetWheelTimer(lParam, false);
      if (this.e.Focused)
        this.TerSetCursorShape(lParam, false);
      this.e.DoPostProcessing = false;
    }
    else if (this.e.IgnoreMouseMove || this.e.DraggingText)
    {
      this.TerSetCursorShape(lParam, false);
      if (this.e.DraggingText)
      {
        this.e.Capture = true;
        if (this.ScrollText())
          return;
      }
      if (this.e.IgnoreMouseMove && !this.e.DraggingText && this.e.CurDragObj < 0)
        this.SetMouseStopTimer(lParam);
      this.e.DoPostProcessing = false;
    }
    else
    {
      if (ev.Button != MouseButtons.Left && ev.Button != MouseButtons.Middle && ev.Button != MouseButtons.Right)
        return;
      if (this.e.CurDragObj >= 0)
        this.TerDragObject(lParam);
      else
        this.TerSetHilight(ev.Button, lParam, false);
    }
  }

  internal void OnMouseUp(MouseEventArgs ev)
  {
    int lParam = (ev.Y << 16 /*0x10*/) + ev.X;
    if (ev.Button == MouseButtons.Left)
    {
      if ((this.e.TerOpFlags & 262144 /*0x040000*/) != 0)
      {
        this.e.TerOpFlags &= -262145;
      }
      else
      {
        this.e.StretchHilight = this.e.TblSelCursShowing = this.e.DblClickHilight = false;
        this.KillHilightTimer();
        if (!this.e.StretchHilight && this.e.HilightBegRow == this.e.HilightEndRow && this.e.HilightBegCol == this.e.HilightEndCol && !this.e.DraggingText)
        {
          this.e.HilightType = 0;
          if (this.MoveCursor(this.e.CurLine, this.e.CurCol))
            this.PaintTer();
        }
        if (this.e.HilightType == 2 && !this.e.DraggingText)
          this.AdjustHilight();
        this.e.IgnoreMouseMove = true;
        this.e.Capture = false;
        if (this.e.CurDragObj >= 0)
        {
          this.PaintTer();
          this.e.CurDragObj = -1;
        }
        else if (this.e.DraggingText && !this.e.InOleDrag)
          this.DragText(ev.X, ev.Y);
        else if (this.e.VerySmallMovement && this.e.HilightType != 0 && this.e.HilightBegRow == this.e.HilightEndRow && this.e.HilightBegCol == this.e.HilightEndCol)
        {
          this.e.HilightType = 0;
          this.PaintTer();
        }
        else if (this.e.HilightType != 0 && !this.e.PictureHilighted)
        {
          this.e.InputFontId = -1;
          this.PaintTer();
        }
        this.TerSetCursorShape(lParam, false);
        if ((this.e.TerOpFlags & 64 /*0x40*/) != 0)
          this.e.TerOpFlags &= -65;
        else
          this.e.InputFontId = -1;
      }
    }
    else
    {
      if (ev.Button != MouseButtons.Right)
        return;
      this.DoAutoSpellCheck();
    }
  }

  internal void OnMouseWheel(MouseEventArgs ev)
  {
    int y = ev.Y;
    int x = ev.X;
    if (this.e.CaretEngaged)
      this.DisengageCaret();
    if (this.e.WheelShowing)
      this.ResetWheel();
    if (this.e.PictureClicked)
    {
      this.e.HilightType = 0;
      this.e.PictureClicked = false;
    }
    if (ev.Delta > 0)
      this.TerWinUp();
    else
      this.TerWinDown();
  }

  internal void OnTimer(int wParam, int lParam)
  {
    if (wParam == 9182 && (this.e.TerFlags2 & 131072 /*0x020000*/) == 0)
    {
      COp.MSG msg;
      do
        ;
      while (this.PeekMessage(out msg, this.e.hTerWnd, 275, 275, 3));
      if (!this.PeekMessage(out msg, this.e.hTerWnd, 0, 0, 2) && (this.e.TerFlags2 & 1024 /*0x0400*/) == 0)
        this.Repaginate(true, false, 0, true);
      do
        ;
      while (this.PeekMessage(out msg, this.e.hTerWnd, 275, 275, 1));
    }
    else
    {
      switch (wParam)
      {
        case 9183:
          this.TerSetHilight(MouseButtons.None, 0, true);
          break;
        case 9184:
          if (tc.eval)
          {
            this.DrawEval();
            break;
          }
          this.KillTimer(this.e.hTerWnd, 9184);
          break;
        default:
          if (wParam == 9188 || wParam == 9189)
          {
            if (!this.e.WheelShowing)
              break;
            if (wParam == 9188)
            {
              this.TerWinUp();
              break;
            }
            this.TerWinDown();
            break;
          }
          if (wParam == 9190)
          {
            this.MouseStopAction();
            break;
          }
          if (wParam < 9199 || (this.e.TerFlags2 & 131072 /*0x020000*/) != 0)
            break;
          this.DrawAnimPict(wParam);
          break;
      }
    }
  }

  internal void OnVScroll(int wParam)
  {
    if (this.e.CaretEngaged)
      this.DisengageCaret();
    if (this.e.WheelShowing)
      this.ResetWheel();
    if (this.e.PictureClicked)
    {
      this.e.HilightType = 0;
      this.e.PictureClicked = false;
    }
    switch (COp.LOWORD(wParam))
    {
      case 0:
        this.TerWinUp();
        break;
      case 1:
        this.TerWinDown();
        break;
      case 2:
        this.TerPageUp(false);
        break;
      case 3:
        this.TerPageDn(false);
        break;
      case 4:
        this.VerThumbPos(wParam);
        break;
      case 5:
        if ((this.e.TerFlags2 & 512 /*0x0200*/) == 0)
        {
          if (!this.e.PagesShowing || this.e.InPrintPreview)
            break;
          this.DrawPageBox(this.e.TerGr, (int) COp.HIWORD(wParam));
          break;
        }
        this.e.TerOpFlags |= 33554432 /*0x02000000*/;
        this.VerThumbPos(wParam);
        this.e.TerOpFlags &= -33554433;
        break;
      case 8:
        if (this.e.PageHasControls && this.True(this.e.ScrollBM) || this.e.PageBoxShowing)
          this.PaintTer();
        this.e.ContinuousScroll = false;
        break;
    }
  }

  internal bool TerLButtonDown(MouseButtons button, int lParam)
  {
    bool flag1 = (this.e.TerFlags3 & 1073741824 /*0x40000000*/) == 0;
    bool flag2 = false;
    bool flag3 = !this.e.TerArg.ReadOnly;
    if (!this.e.Focused)
      flag2 = this.e.Focus();
    this.e.InAutoComp = false;
    this.e.TextDragged = false;
    this.TerMousePos(lParam, true);
    if (this.e.MouseOverShoot != ' ' && this.e.MouseLine >= 0 && this.e.MouseLine < this.e.TotalLines && this.e.TerFont[this.GetCurCfmt(this.e.MouseLine, this.e.MouseCol)].FrameType != 0)
    {
      this.e.MouseOverShoot = ' ';
      if (this.e.CaretEngaged)
        this.DisengageCaret();
    }
    if (!flag2 && (this.e.MouseLine != this.e.CurLine || this.e.MouseCol != this.e.CurCol))
      this.e.InputFontId = -1;
    if (this.e.RulerClicked)
    {
      if (flag3 && this.e.CurDragObj < 0)
        this.DoRulerClick(button, lParam);
    }
    else if (this.e.MouseOverShoot == ' ')
    {
      bool pictureClicked = this.e.PictureClicked;
      this.e.PictureClicked = this.e.FrameClicked = false;
      if (flag1)
      {
        if (this.e.TblSelCursShowing)
          this.HilightTableCol(this.e.MouseLine, true, true);
        else if (this.e.HilightType == 2)
        {
          if (((int) this.GetKeyState(16 /*0x10*/) & 32768 /*0x8000*/) != 0)
            this.e.StretchHilight = true;
          else if (this.e.CurDragObj < 0)
          {
            if (button == MouseButtons.Left)
            {
              if (this.CanDragText())
              {
                this.e.DraggingText = true;
                if ((this.e.TerFlags6 & 524288 /*0x080000*/) != 0)
                  this.OleDragText();
              }
              if (!this.e.TextDragged && !this.e.DraggingText && !this.e.InOleDrag)
              {
                this.e.HilightType = 0;
                this.e.DblClickHilight = false;
                if (!pictureClicked)
                  this.PaintTer();
              }
            }
          }
          else
          {
            if (this.e.DragObj[this.e.CurDragObj].type == 9 || this.e.DragObj[this.e.CurDragObj].type == 10)
              this.MarkCells(888);
            this.e.DragObj[this.e.CurDragObj].TextHilighted = this.e.HilightType != 0;
            this.e.HilightType = 0;
          }
        }
        else if (this.e.HilightType == 0 && ((int) this.GetKeyState(16 /*0x10*/) & 32768 /*0x8000*/) != 0)
        {
          this.e.HilightType = 2;
          if (this.e.CaretEngaged)
          {
            this.e.HilightBegRow = this.e.CurLine;
            this.e.HilightBegCol = this.e.CurCol;
          }
          else
          {
            int row;
            int col;
            this.AbsToRowCol(this.e.CaretPos, out row, out col);
            this.e.HilightBegRow = row;
            this.e.HilightBegCol = col;
          }
          this.e.HilightEndRow = this.e.MouseLine;
          this.e.HilightEndCol = this.e.MouseCol;
          this.e.StretchHilight = true;
        }
      }
      if (this.e.MouseLine >= this.e.TotalLines)
        this.TerMousePos(lParam, true);
      if (this.e.CurDragObj < 0)
      {
        this.e.CurCol = this.e.MouseCol;
        this.GetCurCfmt(this.e.MouseLine, this.e.CurCol);
        bool flag4 = this.MoveCursor(this.e.MouseLine, this.e.CurCol);
        if (flag4)
          this.e.CursDirection = (this.e.TerFlags2 & 536870912 /*0x20000000*/) != 0 ? 2 : 1;
        if (this.e.CurLine != this.e.MouseLine || this.True(this.e.ScrollBM) || this.e.HilightType == 2 | flag4)
        {
          if (((this.True(this.e.ScrollBM) ? 1 : (this.e.HilightType == 2 ? 1 : 0)) | (flag4 ? 1 : 0)) != 0)
          {
            this.TerPosLine(this.e.MouseLine + 1);
          }
          else
          {
            this.e.CurLine = this.e.MouseLine;
            this.e.CurRow = this.e.CurLine - this.e.BeginLine;
            this.DisplayStatusInfo();
            this.DrawRuler(false);
            if (this.e.PictureHilighted)
              this.DrawDragPictRect();
            if (this.e.FrameTabsHilighted)
              this.DrawDragFrameTabs();
            this.DeleteDragObjects(1, 3);
          }
        }
        else
          this.DisplayStatusInfo();
        if (this.e.CurSID >= 0)
          this.e.EditStyle(true, (string) null, true, 0, true);
        if (this.e.SpellPending || this.e.PageBreakShowing || this.LineInfo(this.e.CurLine, 4))
          this.PaintTer();
      }
      else
        this.DisplayStatusInfo();
      int frame;
      if (!this.e.RotatedFrame && (frame = this.frm.GetFrame(this.e.CurLine)) >= 0 && this.e.frame[frame].ParaFrameId > 0)
        this.e.FrameClicked = true;
      int curCfmt = this.GetCurCfmt(this.e.CurLine, this.e.CurCol);
      if ((this.e.TerFont[curCfmt].style & 128 /*0x80*/) != 0 && (this.e.TerFont[curCfmt].style & 512 /*0x0200*/) == 0 && flag1 && this.e.HilightType == 0)
      {
        bool flag5 = this.e.TerArg.ReadOnly && (this.e.TerFlags4 & 8) != 0 && this.IsControl(curCfmt);
        if (this.e.FrameClicked && (this.e.TerFlags2 & 4096 /*0x1000*/) != 0)
        {
          int fid1 = this.e.CurLine == 0 ? 0 : this.e.text[this.e.CurLine - 1].fid;
          int fid2 = this.e.CurLine == this.e.TotalLines - 1 ? 0 : this.e.text[this.e.CurLine + 1].fid;
          int fid3 = this.e.text[this.e.CurLine].fid;
          if (fid1 != fid3 && fid2 != this.e.text[this.e.CurLine].fid && this.e.text[this.e.CurLine].len == 2)
            this.e.FrameClicked = false;
        }
        if (!this.e.FrameClicked && !flag5)
        {
          this.e.PictureClicked = true;
          this.e.HilightType = 2;
          this.e.HilightBegRow = this.e.HilightEndRow = this.e.CurLine;
          this.e.HilightBegCol = this.e.CurCol;
          this.e.HilightEndCol = this.e.CurCol + 1;
          this.e.StretchHilight = false;
          this.e.CursDirection = 1;
        }
      }
    }
    if (!this.e.RulerClicked && this.e.MouseOverShoot == ' ' || this.e.CurDragObj >= 0)
    {
      if (button == MouseButtons.Left || button == MouseButtons.Middle)
        this.e.IgnoreMouseMove = false;
      else if (this.False(this.e.TerArg.WordWrap))
        this.e.IgnoreMouseMove = false;
      if (flag2)
        this.e.IgnoreMouseMove = true;
    }
    this.e.CaretEngaged = true;
    this.e.CurCtlId = -1;
    if (this.e.ImeEnabled)
      this.DisableIme(true);
    if ((button == MouseButtons.Left || button == MouseButtons.Right) && !this.e.RulerClicked && this.e.MouseOverShoot == ' ' && this.e.MouseOnTextLine && this.e.LinkCursShowing)
    {
      if (!this.e.LinkDblClick && this.SendLinkMessage(false, button == MouseButtons.Right))
      {
        if (!this.e.IsHandleCreated)
          return false;
        this.e.IgnoreMouseMove = true;
      }
      else
        this.JumpToPageRefBookmark(true);
    }
    if (((int) this.GetKeyState(17) & 32768 /*0x8000*/) != 0)
      this.InvokeTextLink(true, this.e.CurLine, this.e.CurCol);
    return true;
  }
}
