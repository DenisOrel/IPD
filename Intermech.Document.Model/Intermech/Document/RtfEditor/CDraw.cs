// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.CDraw
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class CDraw : COp
{
  private int displayTextFontStyle;

  internal CDraw(ImRtfEditor ImRtfEditor)
    : base(ImRtfEditor)
  {
  }

  /// <summary>Общий метод отрисовки документа</summary>
  internal new bool PaintTer()
  {
    if (this.e.TotalLines == 0)
      return true;
    if (tc.DebugMode)
      this.misc.dm(nameof (PaintTer));
    Graphics gr = this.e.TerGr;
    if (this.e.BufGr != null)
    {
      gr = this.e.BufGr;
      this.GetOpDC(this.e.BufGr);
    }
    if (!this.e.PaintEnabled || (this.e.TerFlags4 & 16 /*0x10*/) != 0)
      return true;
    this.e.InPainting = true;
    if (this.e.InPrintPreview)
    {
      this.PaintPrintPreview();
      this.e.InPainting = false;
      return true;
    }
    if (this.e.HilightBegRow >= this.e.TotalLines || this.e.HilightEndRow >= this.e.TotalLines)
      this.e.HilightType = 0;
    if (this.e.FontsReleased)
      this.RecreateFonts(this.e.TerGr);
    if (this.e.InitBkBM != null)
    {
      int id = this.e.TerPastePicture("", (Image) this.e.InitBkBM, 0, 0, false);
      this.e.InitBkBM = (Bitmap) null;
      if (id != 0)
        this.e.TerSetBkPictId(id, 1, false);
      this.DeleteTextMap(true);
    }
    if ((this.e.PaintFlag & 1) != 0)
    {
      if (this.e.PageBreakShowing && !this.LineInfo(this.e.CurLine, 4))
        this.e.PaintFlag = 4;
      if (!this.e.PageBreakShowing && this.LineInfo(this.e.CurLine, 4))
        this.e.PaintFlag = 4;
    }
    if (this.e.PaintFlag == 6)
      this.DeleteTextMap(true);
    else if (this.e.BkPictId != 0 && this.e.PaintFlag == 4 && this.e.UseTextMap)
      this.DeleteTextMap(false);
    if (this.e.PaintFlag == 6)
      this.e.PaintFlag = 4;
    if (this.e.PosPageHdrFtr)
    {
      this.e.PaintEnabled = false;
      this.ReposPageHdrFtr(false);
      this.e.PaintEnabled = true;
    }
    if (this.e.FullRenderMode)
      this.pos.AdjustHiddenPos();
    while (true)
    {
      int paintFlag = this.e.PaintFlag;
      int WrapLines = this.e.WinHeight + 10;
      if (this.e.TerArg.PageMode)
      {
        this.e.CurFrame = this.frm.GetFrame(this.e.CurLine);
        if (this.e.CurFrame >= 0)
        {
          WrapLines = this.e.WinHeight + 20;
          if (!this.e.FullRenderMode)
          {
            this.e.BeginLine = 0;
          }
          else
          {
            this.e.BeginLine = this.pos.UnitsToLine2(this.e.frame[this.e.CurFrame].x, this.e.TerWinOrgY, -1);
            if (this.e.CurLine < this.e.BeginLine)
            {
              this.e.BeginLine = this.e.PageInfo[this.e.CurPage].FirstLine;
              if (this.e.CurLine < this.e.BeginLine)
                this.e.BeginLine = this.e.CurLine;
            }
            if ((this.e.frame[this.e.CurFrame].flags & 4) != 0 && this.e.frame[this.e.CurFrame].ScrFirstLine < this.e.BeginLine && this.e.frame[this.e.CurFrame].ScrFirstLine >= 0)
              this.e.BeginLine = this.e.frame[this.e.CurFrame].ScrFirstLine;
            if ((this.e.PfmtId[this.e.text[this.e.BeginLine].pfmt].flags & 12288 /*0x3000*/) != 0 != ((this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].flags & 12288 /*0x3000*/) != 0))
            {
              this.e.BeginLine = this.e.CurLine;
              if ((this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].flags & 12288 /*0x3000*/) == 0 && this.e.BeginLine >= this.e.PageInfo[this.e.CurPage].FirstLine && this.e.BeginLine <= this.e.PageInfo[this.e.CurPage].FirstLine)
                this.e.BeginLine = this.e.PageInfo[this.e.CurPage].FirstLine;
            }
          }
          if (this.e.WrapFlag == 4)
          {
            this.e.BeginLine = this.e.PageInfo[this.e.CurPage].FirstLine;
            WrapLines = this.e.CurPage != this.e.TotalPages - 1 ? this.e.PageInfo[this.e.CurPage + 1].FirstLine - this.e.BeginLine : this.e.TotalLines - this.e.BeginLine;
          }
        }
      }
      if (this.e.WrapFlag != 0 && this.e.TerArg.WordWrap && (this.e.TerOpFlags2 & 1) == 0)
      {
        if (this.e.CommandId > 0 || !this.PeekMessage(out COp.MSG _, this.e.hTerWnd, 258, 258, 2))
          this.WordWrap(this.e.BeginLine, WrapLines);
        else
          this.e.WrapPending = true;
      }
      if (this.e.ScrollBM != null)
      {
        this.e.PaintFlag = 4;
        this.e.ScrollBM.Dispose();
        this.e.ScrollBM = (Bitmap) null;
      }
      if (this.e.TotalLines > 0)
      {
        if (this.e.CurLine >= this.e.TotalLines)
          this.e.CurLine = this.e.TotalLines - 1;
        if (this.e.TerArg.WordWrap && this.e.CurCol >= this.e.text[this.e.CurLine].len)
          this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
      }
      else
        this.e.CurLine = 0;
      if (this.e.CurCol < 0)
        this.e.CurCol = 0;
      if (this.e.FullRenderMode)
      {
        this.pos.HorScrollCheck();
        int curPage = this.e.CurPage;
        this.e.CurPage = this.page.GetCurPage(this.e.CurLine);
        if (this.e.CurPage != curPage)
        {
          this.e.WrapFlag = 3;
          this.e.PaintFlag = 4;
        }
        if (this.e.CurCol < 0)
          this.e.CurCol = 0;
        if (!this.e.TerArg.PageMode)
        {
          if (this.e.CurRow >= this.e.WinHeight && !this.DocFitsInWindow())
          {
            this.e.BeginLine = this.e.CurLine - (this.e.WinHeight - 1);
            if (this.e.BeginLine < 0)
              this.e.BeginLine = 0;
            this.e.PaintFlag = 4;
          }
          if (this.e.BeginLine < 0)
            this.e.BeginLine = 0;
          this.e.CurRow = this.e.CurLine - this.e.BeginLine;
          if (this.e.CurRow < 0)
          {
            this.e.BeginLine = this.e.CurLine;
            this.e.CurRow = 0;
            this.e.PaintFlag = 4;
          }
        }
      }
      else
        this.e.CurPage = 0;
      if (this.e.BeginLine != this.e.WinYOffsetLine)
        this.e.WinYOffsetLine = -1;
      if (this.e.WinYOffsetLine == -1)
        this.e.WinYOffset = 0;
      if (this.e.HilightType != 0 && this.e.StretchHilight)
      {
        if (!this.e.TblSelCursShowing || !this.InSameTable(this.e.text[this.e.HilightBegRow].cid, this.e.text[this.e.HilightEndRow].cid))
          this.SyncHilight();
        if (this.e.PaintFlag != 4)
          this.e.PaintFlag = 4;
      }
      if (this.e.HilightType != 0 && !this.e.StretchHilight && this.e.HilightBegRow == this.e.HilightEndRow && this.e.HilightBegCol == this.e.HilightEndCol)
        this.e.HilightType = 0;
      if (!this.e.TerArg.PageMode)
      {
        this.e.CurFrame = 0;
        this.e.frame[this.e.CurFrame].ScrFirstLine = this.e.BeginLine;
        this.e.frame[this.e.CurFrame].PageFirstLine = 0;
        this.e.frame[this.e.CurFrame].PageLastLine = this.e.TotalLines - 1;
        this.e.frame[this.e.CurFrame].level = 0;
      }
      if (this.e.PictureHilighted)
      {
        this.e.PaintFlag = 4;
        this.DrawDragPictRect();
      }
      if (this.e.FrameTabsHilighted)
      {
        this.e.PaintFlag = 4;
        this.DrawDragFrameTabs();
      }
      if (this.e.FullRenderMode)
        this.DeleteDragObjects(1, 3);
      if (this.e.PaintFlag < 4 && this.e.SpellPending && !this.WordBeingEdited())
        this.e.PaintFlag = 4;
      if (this.e.PaintFlag >= 4)
        this.e.SpellPending = false;
      if (this.e.PaintFlag >= 4 && !this.e.IsPlaneText)
      {
        for (int index = 0; index < this.e.TotalFonts; ++index)
        {
          if (this.e.TerFont[index].InUse && (this.e.TerFont[index].style & 128 /*0x80*/) != 0)
            this.e.TerFont[index].flags &= -89;
        }
      }
      ++this.e.RepaintNo;
      this.e.DirtyRect.left = -1;
      if (this.e.PaintFlag >= 4)
        this.e.PageBreakShowing = false;
      this.e.DrawBknd = true;
      if (this.e.BkPictId == 0 && this.e.PaintFlag == 4 && gr == this.e.BufGr)
      {
        COp.RECT OurRect;
        this.SetRect(out OurRect, this.e.TerWinOrgX, this.e.TerWinOrgY, this.e.TerWinOrgX + this.e.TerWinWidth, this.e.TerWinOrgY + this.e.TerWinHeight);
        Color color = this.SetBkColor(gr, this.PageColor());
        this.OurExtTextOut(gr, 0, 0, 2, OurRect, (char[]) null, 0, (int[]) null);
        this.SetBkColor(gr, color);
        this.e.DrawBknd = false;
      }
      else if (this.e.FullRenderMode)
        this.ClearScrForXparentWrite(gr);
      this.e.TotalHlSegs = 0;
      this.e.TotalPendPicts = 0;
      this.e.CloseDC = false;
      if (this.e.TerArg.PageMode)
      {
        if (this.e.PaintFlag == 4 || this.e.PaintFlag == 5)
          this.PaintFrames(gr, -1);
        else if (this.e.PaintFlag == 2 || this.e.PaintFlag == 3)
        {
          bool flag = !this.e.FullRenderMode || (this.e.TerFlags2 & 1048576 /*0x100000*/) != 0;
          if (!flag)
          {
            int rowHeight = this.GetRowHeight(this.e.CurLine);
            int SpcBef;
            int SpcAft;
            this.GetLineSpacing(this.e.CurLine, rowHeight, out SpcBef, out SpcAft, true);
            int num = rowHeight + (SpcBef + SpcAft);
            if (this.ScrLineHeight(this.e.CurLine, false) != num)
              flag = true;
          }
          if (flag)
          {
            this.e.PaintFlag = 4;
            this.e.WrapFlag = 3;
            if (this.e.FullRenderMode)
              this.ClearScrForXparentWrite(gr);
            this.PaintFrames(gr, -1);
          }
          else
            this.PaintFrames(gr, this.e.CurLine);
        }
      }
      else
      {
        if (this.e.CaretEnabled && !this.e.CaretHidden)
          this.HideCaret(this.e.hTerWnd);
        if (this.e.FullRenderMode)
          this.TerSetClipRgn();
        if (this.e.PaintFlag == 2)
        {
          if (this.GetLineHeight(this.e.CurLine, out int _, out tc.SkipInt) != this.GetRowHeight(this.e.CurLine))
            this.e.PaintFlag = 5;
          else
            this.PaintRows(gr, this.e.CurRow, this.e.CurRow);
        }
        if (this.e.PaintFlag == 5)
          this.PaintRows(gr, this.e.CurRow - 1, 9999);
        else if (this.e.PaintFlag == 4)
          this.PaintRows(gr, 0, 9999);
      }
      this.e.CloseDC = true;
      this.ReleaseOpDC(this.e.TerGr, false);
      if (this.e.BufGr != null)
        this.ReleaseOpDC(this.e.BufGr, false);
      if (this.e.FullRenderMode)
        this.HideCtls();
      this.e.DrawBknd = true;
      if (this.e.TotalHlSegs > 0)
        this.DrawHlSegs(this.e.BufGr);
      if (this.e.PaintFlag >= 4)
      {
        bool flag = false;
        for (int ctl = 0; ctl < this.e.TotalFonts; ++ctl)
        {
          if (this.e.TerFont[ctl].InUse && (this.e.TerFont[ctl].style & 128 /*0x80*/) != 0 && (this.e.TerFont[ctl].flags & 24) == 0 && (this.e.TerFont[ctl].flags & 4) != 0 && this.HideControl(ctl))
          {
            flag = true;
            break;
          }
        }
        if (flag)
        {
          this.e.Validate();
          if (this.e.FullRenderMode)
            this.TerSetClipRgn();
        }
      }
      if (this.e.WheelShowing)
        this.DrawWheel(gr);
      if (this.e.BufGr != null && this.e.UseWin)
      {
        if (!tc.disable || this.e.InDesignMode())
        {
          this.BlastToScreen();
          this.CopyControls();
        }
        else if (tc.disable)
        {
          string s = "EVALUATION EXPIRED!  TO PURCHASE, PLEASE VISIT:  WWW.SUBSYSTEMS.COM";
          this.e.TerGr.FillRectangle(Brushes.White, new Rectangle(this.e.TerWinOrgX, this.e.TerWinOrgY, this.e.TerWinWidth, this.e.TerWinHeight));
          this.e.TerGr.DrawString(s, this.e.TerFont[0].font, Brushes.Black, new PointF((float) this.e.TerWinOrgX, (float) (this.e.TerWinOrgY + this.e.TerWinHeight / 2)));
        }
      }
      if (this.e.FullRenderMode)
      {
        this.DisplayStatusInfo();
        this.e.TerGr.ResetClip();
      }
      if (this.e.FullRenderMode)
      {
        if (this.e.UseWin && this.e.CaretEnabled && !this.e.CaretHidden)
          this.ShowCaret(this.e.hTerWnd);
        if (this.e.RulerPending || this.e.RulerSection != this.GetSection(this.e.CurLine) || this.e.RulerLineX != this.e.text[this.e.CurLine].x || paintFlag == 4)
          this.DrawRuler(true);
        if (this.e.TerArg.ShowHorBar || this.e.TerArg.ShowVerBar)
          this.SetScrollBars();
        this.OurSetCaretPos();
      }
      if (this.e.BorderSpill != 0)
        this.RedrawNonText(this.e.BorderSpill);
      this.e.PaintFlag = 4;
      this.e.WrapFlag = 3;
      this.e.UseTextMap = true;
      this.e.PrevCursLine = this.e.CurLine;
      this.e.PrevCursCol = this.e.CurCol;
      this.e.PrevCursLineY = this.e.CurLineY + this.e.TerWinOrgY;
      this.e.PrevCursPage = this.e.CurPage;
      this.e.CursDirection = 0;
      if (this.e.CurClipRgn != null)
      {
        this.e.CurClipRgn.Dispose();
        this.e.CurClipRgn = (Region) null;
      }
      this.e.BorderSpill = 0;
      if ((this.e.TerFlags5 & 4096 /*0x1000*/) == 0)
        ++this.e.UndoRef;
      this.e.FirstPalette = true;
      this.e.PageBoxShowing = false;
      this.e.OnWmPaint = false;
      ++this.e.PaintId;
      if (this.e.HilightType != 2 || !this.e.StretchHilight)
        this.e.HilightAtCurPos = false;
      if (!this.e.TerArg.PageMode && this.e.CurLine > this.e.frame[0].ScrLastLine && this.e.BeginLine < this.e.CurLine)
        ++this.e.BeginLine;
      else
        break;
    }
    if ((this.e.TerFlags & 32768 /*0x8000*/) != 0)
    {
      bool enable = false;
      bool showVerBar = this.e.TerArg.ShowVerBar;
      if (!this.e.TerArg.WordWrap || !this.e.TerArg.PageMode)
      {
        enable = this.e.BeginLine != 0 || this.e.frame[0].ScrLastLine + 1 != this.e.TotalLines;
        this.EnableVScrollBar(enable);
      }
      if (this.e.TerArg.PageMode)
      {
        enable = this.e.TerWinOrgY > 0 || this.e.TotalPages > 1 || this.SumPageScrHeight(0, this.e.TotalPages) > this.e.TerWinHeight;
        this.EnableVScrollBar(enable);
      }
      else if (this.e.TerArg.FittedView)
      {
        enable = this.e.TerWinOrgY > 0 || this.e.TotalPages > 1 || this.e.CurTextHeight > this.e.TerWinHeight;
        this.EnableVScrollBar(enable);
      }
      if (enable && !showVerBar && this.e.TerArg.ShowVerBar)
        this.SetScrollBars();
    }
    this.e.InPainting = false;
    this.e.FirePostPaint((object) this.e, this.e.TerGr);
    return true;
  }

  /// <summary>Быстро обновить изображение</summary>
  internal bool TerQuickRepaint()
  {
    if (tc.DebugMode)
      this.misc.dm(nameof (TerQuickRepaint));
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    this.PaintTer();
    return true;
  }

  /// <summary>Перерисовать</summary>
  internal bool TerRepaint(bool ClearBackground)
  {
    if (!this.e.PaintEnabled)
      return true;
    if (tc.DebugMode)
      this.misc.dm(nameof (TerRepaint));
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    this.e.TerOpFlags |= 128 /*0x80*/;
    bool onWmPaint = this.e.OnWmPaint;
    if (this.e.FullRenderMode)
    {
      if (this.e.BkPictBM != null)
        this.DisposeBkPictBM();
      if (this.e.FontsReleased)
        this.RecreateFonts(this.e.TerGr);
      if (this.e.OnWmPaint)
      {
        int num1 = -this.e.TerWinOrgX + this.e.TerWinRect.left;
        int num2 = -this.e.TerWinOrgY + this.e.TerWinRect.top;
        this.e.ClipRect.left += num1;
        this.e.ClipRect.right += num1;
        this.e.ClipRect.top += num2;
        this.e.ClipRect.bottom += num2;
      }
    }
    int terWinWidth = this.e.TerWinWidth;
    int terWinHeight = this.e.TerWinHeight;
    if (this.e.FullRenderMode)
    {
      this.GetWinDimension();
      if (ClearBackground || this.e.RedrawBorder)
        this.TerDrawBorder();
      if (this.e.HtmlMode && this.e.TerWinWidth != terWinWidth)
      {
        if (this.e.TotalCells > 1)
          this.e.TerAdjustHtmlTable();
        this.AdjustHtmlRulerWidth();
        if (this.e.TotalFonts > 1)
          this.AdjustHtmlPictWidth();
        if (this.e.TotalCells > 1)
          this.RequestPagination(true);
      }
    }
    if (this.e.FullRenderMode && this.e.CurRow >= this.e.WinHeight)
    {
      this.e.CurRow = this.e.WinHeight - 1;
      this.e.BeginLine = this.e.CurLine - this.e.CurRow;
    }
    if (this.e.OnWmPaint)
    {
      if (this.e.FullRenderMode)
      {
        if (this.e.ClipRect.left < this.e.TerWinRect.left)
          this.e.ClipRect.left = this.e.TerWinRect.left;
        if (this.e.ClipRect.right > this.e.TerWinRect.right)
          this.e.ClipRect.right = this.e.TerWinRect.right;
        if (this.e.ClipRect.left > this.e.ClipRect.right)
          this.e.ClipRect.left = this.e.ClipRect.right;
        if (this.e.ClipRect.top < this.e.TerWinRect.top)
          this.e.ClipRect.top = this.e.TerWinRect.top;
        if (this.e.ClipRect.bottom > this.e.TerWinRect.bottom)
          this.e.ClipRect.bottom = this.e.TerWinRect.bottom;
        if (this.e.ClipRect.top > this.e.ClipRect.bottom)
          this.e.ClipRect.top = this.e.ClipRect.bottom;
      }
      if (!this.e.InPrintPreview)
        this.e.CurClipRgn = new Region(this.FromRect(this.e.ClipRect));
    }
    if (this.e.FullRenderMode)
      this.DeleteTextMap(true);
    int repageBeginLine = this.e.RepageBeginLine;
    if (ClearBackground)
    {
      this.e.RepaintRuler = true;
      this.e.RulerPending = true;
    }
    this.PaintTer();
    if (this.e.FullRenderMode)
    {
      if (this.e.HtmlMode && this.e.TotalCells > 1)
        this.e.RepageBeginLine = repageBeginLine;
      if (this.e.TerArg.ShowStatus)
        this.DisplayStatus();
      if (((ClearBackground ? 1 : (this.e.TerWinWidth != terWinWidth ? 1 : 0)) | (onWmPaint ? 1 : 0)) != 0)
      {
        if (this.e.TerTlb != null)
          this.e.TerTlb.Invalidate();
        if (this.e.PvTlb != null)
          this.e.PvTlb.Invalidate();
      }
      if (this.e.HtmlMode && (this.e.TerWinWidth != terWinWidth || this.e.TerWinHeight != terWinHeight))
        this.e.SendMessageToParent(2734, (int) this.e.hTerWnd, 0, false);
    }
    this.e.TerOpFlags &= -2177;
    return true;
  }

  /// <summary>Перерисовать</summary>
  internal new bool RepaintTer(PaintEventArgs ev)
  {
    if (tc.DebugMode)
      this.misc.dm(nameof (RepaintTer));
    if (this.e.InPainting)
      return true;
    if (ev != null)
      this.e.ClipRect = this.ToRect(ev.ClipRectangle);
    this.e.OnWmPaint = true;
    Graphics gr = Graphics.FromHwnd(this.e.hTerWnd);
    if (this.e.TerGr != null)
    {
      this.ApplyNewGr(gr);
      this.e.TerGr.Dispose();
    }
    this.e.TerGr = gr;
    this.e.RepaintRuler = true;
    return this.TerRepaint(false);
  }

  internal void SetTerGraphicsClip(Rectangle rectangle)
  {
    this.e.TerGr.Clip = new Region(rectangle);
    this.e._grVisibleClipBounds = (RectangleF) rectangle;
  }

  /// <summary>Установить положение контрола редактора</summary>
  internal new void SetTerWindowOrg()
  {
    if (this.e.PictureHilighted)
    {
      this.e.PaintFlag = 4;
      this.DrawDragPictRect();
    }
    if (this.e.FrameTabsHilighted)
    {
      this.e.PaintFlag = 4;
      this.DrawDragFrameTabs();
    }
    this.DeleteDragObjects(1, 3);
    this.TerDestroyCaret();
    if (this.e.UseWin)
    {
      this.ResetTransform(this.e.TerGr);
      this.e.TerGr.TranslateTransform((float) (-this.e.TerWinOrgX + this.e.TerWinRect.left), (float) (-this.e.TerWinOrgY + this.e.TerWinRect.top));
    }
    if (this.e.BufGr != null)
    {
      this.ResetTransform(this.e.BufGr);
      this.e.BufGr.TranslateTransform((float) -this.e.TerWinOrgX, (float) -this.e.TerWinOrgY);
    }
    this.InitCaret();
    this.DeleteTextMap(false);
    this.e.RulerPending = true;
  }

  /// <summary>Установить положение по Y контрола редактора</summary>
  internal new bool SetYOrigin(int line)
  {
    bool flag = false;
    int frame = this.frm.GetFrame(line);
    if (frame < 0)
      return false;
    int num1 = this.e.frame[frame].y + this.e.frame[frame].SpaceTop;
    for (int pageFirstLine = this.e.frame[frame].PageFirstLine; pageFirstLine < line; ++pageFirstLine)
    {
      if (this.TableLevel(pageFirstLine) == this.e.frame[frame].level)
        num1 += this.ScrLineHeight(pageFirstLine, true);
    }
    if (this.e.CurLineY > this.e.TerWinHeight - this.ScrLineHeight(line, true))
      flag = true;
    if (num1 > this.e.TerWinOrgY + this.e.TerWinHeight - this.ScrLineHeight(line, true))
      flag = true;
    if (flag)
      this.e.CurLineY = this.e.TerWinHeight - this.ScrLineHeight(line, true);
    if (this.e.CurLineY < 0)
      this.e.CurLineY = 0;
    if (num1 < this.e.CurLineY)
      this.e.CurLineY = num1;
    int num2 = num1 - this.e.CurLineY;
    if (this.e.TerWinOrgY != num2)
    {
      this.e.TerWinOrgY = num2;
      this.SetTerWindowOrg();
    }
    return true;
  }

  /// <summary>Обновить размеры окна</summary>
  internal new bool GetWinDimension()
  {
    int terWinWidth = this.e.TerWinWidth;
    if (tc.DebugMode)
      this.misc.dm(nameof (GetWinDimension));
    Size size = !this.e.clientSizeBuffer.HasValue ? this.e.ClientSize : this.e.clientSizeBuffer.Value;
    this.e.TerRect = new COp.RECT(size);
    int num1 = this.e.TerTextMet.tmHeight + this.e.TerTextMet.tmExternalLeading;
    if (num1 <= 0)
      num1 = 1;
    int num2 = this.e.TerTextMet.tmAveCharWidth;
    if (num2 <= 0)
      num2 = 1;
    int num3;
    if (this.e.TerArg.ruler)
    {
      this.e.RulerRect.top = this.e.TerRect.top + this.e.ToolBarHeight;
      this.e.RulerRect.bottom = this.e.RulerRect.top + 2 * this.e.RulerFontHeight;
      num3 = this.e.RulerRect.bottom - this.e.RulerRect.top;
    }
    else
      num3 = 0;
    if (this.e.TerTextMet.tmAveCharWidth > 0)
    {
      if (!this.e.FullRenderMode)
        this.e.WinWidth = size.Width / num2 - 1;
      else
        this.e.WinWidth = (this.e.TerRect.right - this.e.TerRect.left) / num2 - 1;
    }
    if (!this.e.FullRenderMode)
      this.e.WinHeight = size.Height / num1;
    else
      this.e.WinHeight = (this.e.TerRect.bottom - this.e.TerRect.top - num3) / num1;
    if (this.e.WinHeight == 0)
      this.e.WinHeight = 1;
    if (this.e.WinWidth <= 0)
      this.e.WinWidth = 1;
    if (this.e.TerArg.BorderMargin && this.e.ScrResX > 0)
    {
      this.e.BorderMarg = 75;
      this.e.TerWinRect.left = this.e.TerRect.left + this.TwipsToOrigScrX(75);
      this.e.TerWinRect.right = this.e.TerRect.right - this.TwipsToOrigScrX(75);
      this.e.TerWinRect.top = this.e.TerRect.top + num3 + this.e.ToolBarHeight + this.TwipsToOrigScrY(75);
      this.e.TerWinRect.bottom = this.e.TerRect.bottom - this.TwipsToOrigScrY(75);
    }
    else
    {
      this.e.TerWinRect = this.e.TerRect;
      if (this.e.FullRenderMode)
        this.e.TerWinRect.top += num3 + this.e.ToolBarHeight;
      this.e.BorderMarg = 0;
    }
    if (this.e.FullRenderMode)
    {
      this.e.StatusBarHeight = this.e.TerArg.ShowStatus ? this.e.StatusFontHeight + 6 * this.e.StatusLineWidth : 0;
      this.e.TerWinRect.bottom -= this.e.StatusBarHeight;
      this.e.RulerRect.left = this.e.TerRect.left;
      this.e.RulerRect.right = this.e.TerRect.right;
    }
    this.e.TerWinWidth = this.e.TerWinRect.right - this.e.TerWinRect.left;
    this.e.TerWinHeight = this.e.TerWinRect.bottom - this.e.TerWinRect.top;
    if (this.e.FullRenderMode)
    {
      int x = SystemInformation.VerticalScrollBarThumbHeight;
      int z = this.e.TerRect.bottom - this.e.TerRect.top - 2 * SystemInformation.VerticalScrollBarArrowHeight;
      if (x > z / 2)
        x = z / 2;
      this.e.MinThumbHt = this.MulDiv(x, 1000, z);
    }
    else
      this.e.MinThumbHt = 0;
    if (this.e.FullRenderMode)
    {
      if (this.e.TerTlb != null)
      {
        this.e.TerTlb.Location = new Point(this.e.TerRect.left, 0);
        this.e.TerTlb.Size = new Size(this.e.TerRect.right - this.e.TerRect.left, this.e.ToolBarHeight);
      }
      if (this.e.PvTlb != null)
      {
        this.e.PvTlb.Location = new Point(this.e.TerRect.left, 0);
        this.e.PvTlb.Size = new Size(this.e.TerRect.right - this.e.TerRect.left, this.e.ToolBarHeight);
      }
    }
    this.e.frame[0].ScrWidth = 9999;
    this.e.frame[0].width = 9999;
    this.e.frame[0].ScrHeight = this.e.TerWinHeight;
    this.e.frame[0].height = this.e.TerWinHeight;
    if (this.e.FullRenderMode)
    {
      this.ResetBufBM();
      if (this.e.UseWin)
      {
        this.ResetTransform(this.e.TerGr);
        this.e.TerGr.TranslateTransform((float) (-this.e.TerWinOrgX + this.e.TerWinRect.left), (float) (-this.e.TerWinOrgY + this.e.TerWinRect.top));
      }
      if (this.e.BufGr != null)
      {
        this.ResetTransform(this.e.BufGr);
        this.e.BufGr.TranslateTransform((float) -this.e.TerWinOrgX, (float) -this.e.TerWinOrgY);
      }
    }
    if (this.e.BkPictId != 0 && this.e.BkPictFlag == 1)
      this.SetPictSize(this.e.BkPictId, this.e.TerWinHeight, this.e.TerWinWidth, true);
    if (this.e.TerArg.FittedView && this.e.TerWinWidth != terWinWidth && terWinWidth > 0)
      this.RequestPagination(true);
    return true;
  }

  /// <summary>Создать матрицу для поворота текста. Угол может принимать только значения: 0, 90, -90, 270, 180</summary>
  /// <param name="rect">Прямоугольник в котором находится текст. Переворачивается вместе с текстом</param>
  /// <param name="angle">Угол поворота. Может принимать только значения: 90, -90, 270, 180</param>
  /// <returns>Матрица пересчета</returns>
  internal static Matrix GetMatrixForRotateTextInBox(ref RectangleF rect, int angle)
  {
    Matrix forRotateTextInBox = new Matrix();
    RectangleF rectangleF = rect;
    switch (angle)
    {
      case -90:
      case 270:
        rectangleF = new RectangleF(rect.X, rect.Y, rect.Height, rect.Width);
        forRotateTextInBox.Translate(-rectangleF.X, -rectangleF.Y, System.Drawing.Drawing2D.MatrixOrder.Append);
        forRotateTextInBox.Rotate(-90f, System.Drawing.Drawing2D.MatrixOrder.Append);
        forRotateTextInBox.Translate(rectangleF.X, rectangleF.Y + rectangleF.Width, System.Drawing.Drawing2D.MatrixOrder.Append);
        break;
      case 90:
        rectangleF = new RectangleF(rect.X, rect.Y, rect.Height, rect.Width);
        forRotateTextInBox.Translate(-rectangleF.X, -rectangleF.Y, System.Drawing.Drawing2D.MatrixOrder.Append);
        forRotateTextInBox.Rotate(90f, System.Drawing.Drawing2D.MatrixOrder.Append);
        forRotateTextInBox.Translate(rectangleF.X + rectangleF.Height, rectangleF.Y, System.Drawing.Drawing2D.MatrixOrder.Append);
        break;
      case 180:
        forRotateTextInBox.Translate(-rectangleF.X, -rectangleF.Y, System.Drawing.Drawing2D.MatrixOrder.Append);
        forRotateTextInBox.Rotate(180f, System.Drawing.Drawing2D.MatrixOrder.Append);
        forRotateTextInBox.Translate(rectangleF.Right, rectangleF.Bottom, System.Drawing.Drawing2D.MatrixOrder.Append);
        break;
    }
    rect = rectangleF;
    return forRotateTextInBox;
  }

  /// <summary>Создать матрицу для поворота текста. Угол может принимать только значения: 0, 90, -90, 270, 180</summary>
  /// <param name="rect">Прямоугольник в котором находится текст. Переворачивается вместе с текстом</param>
  /// <param name="angle">Угол поворота. Может принимать только значения: 90, -90, 270, 180</param>
  /// <returns>Матрица пересчета</returns>
  internal static Matrix GetMatrixForRotateTextInBox(ref Rectangle rect, int angle)
  {
    Matrix forRotateTextInBox = new Matrix();
    Rectangle rectangle = rect;
    switch (angle)
    {
      case -90:
      case 270:
        rectangle = new Rectangle(rect.X, rect.Y, rect.Height, rect.Width);
        forRotateTextInBox.Translate((float) -rectangle.X, (float) -rectangle.Y, System.Drawing.Drawing2D.MatrixOrder.Append);
        forRotateTextInBox.Rotate(-90f, System.Drawing.Drawing2D.MatrixOrder.Append);
        forRotateTextInBox.Translate((float) rectangle.X, (float) (rectangle.Y + rectangle.Width), System.Drawing.Drawing2D.MatrixOrder.Append);
        break;
      case 90:
        rectangle = new Rectangle(rect.X, rect.Y, rect.Height, rect.Width);
        forRotateTextInBox.Translate((float) -rectangle.X, (float) -rectangle.Y, System.Drawing.Drawing2D.MatrixOrder.Append);
        forRotateTextInBox.Rotate(90f, System.Drawing.Drawing2D.MatrixOrder.Append);
        forRotateTextInBox.Translate((float) (rectangle.X + rectangle.Height), (float) rectangle.Y, System.Drawing.Drawing2D.MatrixOrder.Append);
        break;
      case 180:
        forRotateTextInBox.Translate((float) -rectangle.X, (float) -rectangle.Y, System.Drawing.Drawing2D.MatrixOrder.Append);
        forRotateTextInBox.Rotate(180f, System.Drawing.Drawing2D.MatrixOrder.Append);
        forRotateTextInBox.Translate((float) rectangle.Right, (float) rectangle.Bottom, System.Drawing.Drawing2D.MatrixOrder.Append);
        break;
    }
    rect = rectangle;
    return forRotateTextInBox;
  }

  /// <summary>Повернуть прямоугольник с текстом</summary>
  /// <param name="rect"></param>
  /// <param name="angle"></param>
  /// <returns></returns>
  internal static Rectangle RotateRectangle(Rectangle rect, int angle)
  {
    switch (angle)
    {
      case -90:
      case 270:
        return new Rectangle(rect.X, rect.Y, rect.Height, rect.Width);
      case 90:
        return new Rectangle(rect.X, rect.Y, rect.Height, rect.Width);
      default:
        return rect;
    }
  }

  /// <summary>Сбросить матрицу трансформации</summary>
  internal void ResetTransform(Graphics gr)
  {
    if (this.e.AllTextAngle2 != 0)
    {
      if (this.e.TransformMatrix != null && this.e.TransformMatrix2 != null)
        this.e.TransformMatrix2.Multiply(this.e.TransformMatrix, System.Drawing.Drawing2D.MatrixOrder.Append);
      if (gr == null || this.e.TransformMatrix2 == null)
        return;
      gr.Transform = this.e.TransformMatrix2;
    }
    else if (this.e.TransformMatrix != null)
    {
      if (gr != null)
        gr.Transform = this.e.TransformMatrix;
      this.e.TransformMatrix2 = this.e.TransformMatrix;
    }
    else
      gr?.ResetTransform();
  }

  /// <summary>Нарисовать фреймы</summary>
  internal new void PaintFrames(Graphics gr, int PaintLine)
  {
    if (this.e.TotalLines == 0)
      return;
    int idx = 0;
    bool flag1 = false;
    bool flag2 = false;
    int paintFlag = this.e.PaintFlag;
    if (tc.DebugMode)
      this.misc.dm(nameof (PaintFrames));
    if (this.e.PaintFlag != 2 && this.e.PaintFlag != 3)
    {
      if (this.e.FullRenderMode)
      {
        this.DeleteDragObjects(9, 9);
        this.DeleteDragObjects(11, 11);
        this.DeleteDragObjects(10, 10);
      }
      if (this.e.WrapFlag != 1)
        this.frm.RefreshFrames(this.e.CommandId != 600 && this.e.CommandId != 601 && this.e.CommandId != 602 && this.e.CommandId != 603 && this.e.MessageId != 276 && this.e.MessageId != 277 || this.e.MessageId == 258);
    }
    if (this.e.OnWmPaint)
    {
      int num = this.PageTextWidth();
      if (this.e.TerWinOrgX + this.e.TerWinWidth > num)
      {
        this.e.TerWinOrgX = num - this.e.TerWinWidth;
        if (this.e.TerWinOrgX < 0)
          this.e.TerWinOrgX = 0;
        if (this.e.UseWin)
        {
          this.ReleaseOpDC(this.e.TerGr, true);
          this.ResetTransform(this.e.TerGr);
          this.e.TerGr.TranslateTransform((float) (-this.e.TerWinOrgX + this.e.TerWinRect.left), (float) (-this.e.TerWinOrgY + this.e.TerWinRect.top));
        }
        if (this.e.BufGr != null)
        {
          this.ReleaseOpDC(this.e.BufGr, true);
          this.ResetTransform(this.e.BufGr);
          this.e.BufGr.TranslateTransform((float) -this.e.TerWinOrgX, (float) -this.e.TerWinOrgY);
        }
        if (this.e.CurClipRgn != null)
          this.e.CurClipRgn.Dispose();
        this.e.CurClipRgn = (Region) null;
      }
    }
    int curLine = this.e.CurLine;
    if (curLine > 0 && (this.e.text[curLine].flags & 2048 /*0x0800*/) != 0 && this.e.CurPage > this.e.FirstFramePage && curLine == this.e.PageInfo[this.e.CurPage].FirstLine)
      --curLine;
    this.e.CurFrame = this.frm.GetFrame(curLine);
    if (this.e.CurFrame < 0)
    {
      while (curLine <= this.e.PageInfo[this.e.CurPage].LastLine && (this.e.CurFrame = this.frm.GetFrame(curLine)) < 0)
        ++curLine;
      if (curLine > this.e.PageInfo[this.e.CurPage].LastLine)
        return;
      this.e.CurLine = curLine;
    }
    int fid = this.e.text[this.e.CurLine].fid;
    bool flag3 = fid > 0 && (this.e.ParaFrame[fid].flags & 8) != 0;
    if (!flag3 && (this.e.text[this.e.CurLine].flags & 16384 /*0x4000*/) != 0 && this.e.TerFont[this.fnt.GetCurCfmt(this.e.CurLine, this.e.CurCol)].ParaFID > 0)
      flag3 = true;
    int num1 = this.e.frame[this.e.CurFrame].y + this.e.frame[this.e.CurFrame].SpaceTop;
    int num2 = this.e.frame[this.e.CurFrame].y + this.e.frame[this.e.CurFrame].height - this.e.frame[this.e.CurFrame].SpaceTop;
    int num3 = -1;
    int pageFirstLine1 = this.e.frame[this.e.CurFrame].PageFirstLine;
    int num4 = -1;
    for (; pageFirstLine1 <= this.e.CurLine; ++pageFirstLine1)
    {
      if (this.tbl.TableLevel(pageFirstLine1) == this.e.frame[this.e.CurFrame].level)
      {
        if (pageFirstLine1 > this.e.frame[this.e.CurFrame].PageFirstLine)
        {
          int num5 = pageFirstLine1 != 0 || num4 == -1 ? this.pos.ScrLineHeight(pageFirstLine1, true) : num4;
          if (pageFirstLine1 == 0)
            num4 = num5;
          if (num1 + num5 / 4 >= num2 && num3 < 0)
            num3 = pageFirstLine1 - 1;
        }
        if (pageFirstLine1 < this.e.CurLine)
        {
          int num6 = pageFirstLine1 != 0 || num4 == -1 ? this.pos.ScrLineHeight(pageFirstLine1, true) : num4;
          if (pageFirstLine1 == 0)
            num4 = num6;
          else
            num6 = 0;
          num1 += num6;
        }
        else
          break;
      }
    }
    int num7 = this.e.CurLine != 0 || num4 == -1 ? this.pos.ScrLineHeight(this.e.CurLine, true) : num4;
    if (this.e.CurLine == 0)
      num4 = num7;
    if (num1 > this.e.TerWinOrgY + this.e.TerWinHeight - num7)
      flag1 = true;
    if (num1 < this.e.TerWinOrgY)
      flag2 = true;
    int num8 = pageFirstLine1 != 0 || num4 == -1 ? this.pos.ScrLineHeight(pageFirstLine1, true) : num4;
    if (pageFirstLine1 == 0)
      num4 = num8;
    int num9 = num1 + num8;
    if (num3 >= 0 && (this.e.TerFlags3 & 32 /*0x20*/) != 0 && this.e.CurLine > num3)
    {
      this.e.CurLine = num3;
      this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
      if (this.e.CurCol < 0)
        this.e.CurCol = 0;
    }
    else if (this.e.FullRenderMode && (num9 <= this.e.TerWinOrgY || num9 > this.e.TerWinOrgY + this.e.TerWinHeight) && this.e.CaretEngaged && !flag3 && this.e.ScrollBM == null && !this.e.InDragDrop)
      this.SetYOrigin(this.e.CurLine);
    else
      this.e.CurLineY = num9 - this.e.TerWinOrgY;
    if (this.e.FullRenderMode && (this.e.TerFlags3 & 128 /*0x80*/) != 0)
    {
      bool flag4 = false;
      if (this.e.TerWinOrgY != 0)
      {
        this.e.TerWinOrgY = 0;
        flag4 = true;
      }
      if (this.e.TerWinOrgX != 0 && !this.e.TerArg.ShowHorBar)
      {
        this.e.TerWinOrgX = 0;
        flag4 = true;
      }
      if (flag4)
        this.SetTerWindowOrg();
    }
    this.e.CurFrame = this.frm.GetFrame(this.e.CurLine);
    if (this.e.CurFrame < 0)
      return;
    if ((this.e.TerFlags3 & 128 /*0x80*/) == 0 && this.e.WinYOffsetLine < 0 & flag2 && this.e.CaretEngaged && !flag3 && this.e.ScrollBM == null && !this.e.InDragDrop && this.e.frame[this.e.CurFrame].y + this.e.frame[this.e.CurFrame].SpaceTop < this.e.TerWinOrgY && this.e.frame[this.e.CurFrame].y + this.e.frame[this.e.CurFrame].height > this.e.TerWinOrgY)
    {
      int num10 = this.e.frame[this.e.CurFrame].y + this.e.frame[this.e.CurFrame].SpaceTop;
      for (int pageFirstLine2 = this.e.frame[this.e.CurFrame].PageFirstLine; pageFirstLine2 < this.e.CurLine; ++pageFirstLine2)
      {
        if (this.TableLevel(pageFirstLine2) == this.e.frame[this.e.CurFrame].level)
        {
          int num11 = pageFirstLine2 != 0 || num4 == -1 ? this.pos.ScrLineHeight(pageFirstLine2, true) : num4;
          if (pageFirstLine2 == 0)
            num4 = num11;
          if ((flag1 || num10 + num11 / 2 <= this.e.TerWinOrgY) && num10 <= this.e.TerWinOrgY)
            num10 += num11;
          else
            break;
        }
      }
      if (num10 != this.e.TerWinOrgY)
      {
        this.e.CurLineY -= num10 - this.e.TerWinOrgY;
        this.e.TerWinOrgY = num10;
        this.SetTerWindowOrg();
        if (this.e.BkPictId > 0)
          this.DeleteTextMap(true);
      }
    }
    if (this.e.FullRenderMode && this.e.PaintFlag == 4 && this.e.PaintFlag != paintFlag)
      this.ClearScrForXparentWrite(gr);
    int curFrame = this.e.CurFrame;
    if (this.e.CaretEnabled && !this.e.CaretHidden)
      this.HideCaret(this.e.hTerWnd);
    if (this.e.FullRenderMode)
      this.TerSetClipRgn();
    this.e.BeginLine = -1;
    this.e.WinHeight = 0;
    if (this.e.WmParaFID > 0 && this.e.PaintFlag >= 4)
    {
      for (int index = 0; index < this.e.TotalFrames; ++index)
      {
        if ((this.e.frame[index].flags & 2097152 /*0x200000*/) != 0 && this.e.frame[index].ScrX < this.e.TerWinOrgX + this.e.TerWinWidth && this.e.frame[index].ScrX + this.e.frame[index].ScrWidth > this.e.TerWinOrgX && this.e.frame[index].y < this.e.TerWinOrgY + this.e.TerWinHeight && this.e.frame[index].ScrY + this.e.frame[index].ScrHeight > this.e.TerWinOrgY)
        {
          int pict = this.e.ParaFrame[this.e.frame[index].ParaFrameId].pict;
          if (pict > 0)
            this.PictOut(gr, this.e.frame[index].ScrX, this.e.frame[index].ScrY, this.e.frame[index].width, this.e.frame[index].height, pict, 1, 0, true);
        }
      }
    }
    int num12 = !this.e.FullRenderMode || this.e.FramesSorted ? 1 : 5;
    for (int index1 = 0; index1 < num12; ++index1)
    {
      for (int index2 = 0; index2 < this.e.TotalFrames; ++index2)
      {
        int dispFrame = this.e.frame[index2].DispFrame;
        if ((this.e.FullRenderMode || !this.e.frame[dispFrame].empty) && (this.e.frame[dispFrame].flags & 2097152 /*0x200000*/) == 0)
        {
          int paraFrameId = this.e.frame[dispFrame].ParaFrameId;
          if (!this.e.FramesSorted)
          {
            if ((index1 != 0 || paraFrameId == 0) && (index1 <= 0 || paraFrameId != 0 || (this.e.frame[dispFrame].flags & 66016) != 0) && (index1 != 1 || this.e.frame[dispFrame].empty || this.e.CurLine < this.e.frame[dispFrame].PageFirstLine || this.e.CurLine > this.e.frame[dispFrame].PageLastLine) && (index1 <= 1 || !this.e.frame[dispFrame].empty || this.e.frame[dispFrame].BoxFrame != dispFrame || paraFrameId <= 0) && (index1 != 2 || this.e.frame[dispFrame].empty || this.e.CurLine >= this.e.frame[dispFrame].PageFirstLine && this.e.CurLine <= this.e.frame[dispFrame].PageLastLine))
            {
              bool flag5 = (this.e.ParaFrame[paraFrameId].flags & 768 /*0x0300*/) != 0;
              if (flag5 && (this.e.ParaFrame[paraFrameId].flags & 512 /*0x0200*/) != 0 && this.e.ParaFrame[paraFrameId].FillPattern > 0)
                flag5 = false;
              if (index1 != 3 & flag5 || index1 == 3 && !flag5 || index1 != 4 && (this.e.frame[dispFrame].flags & 66016) != 0 || index1 == 4 && (this.e.frame[dispFrame].flags & 66016) == 0)
                continue;
            }
            else
              continue;
          }
          this.e.CurFrame = dispFrame;
          this.e.EnableTransparent = (this.e.frame[this.e.CurFrame].flags & 16 /*0x10*/) != 0 || (this.e.TerFlags2 & 32 /*0x20*/) != 0;
          if ((this.e.TerOpFlags & 4) != 0 && !this.e.repaginating && this.e.frame[dispFrame].ParaFrameId == 0 && this.e.frame[dispFrame].PageFirstLine < this.e.frame[dispFrame].PageLastLine)
          {
            int pageFirstLine3 = this.e.frame[dispFrame].PageFirstLine;
            if (pageFirstLine3 >= 0 && pageFirstLine3 < this.e.TotalLines && this.e.text[pageFirstLine3].fid > 0)
              ++this.e.frame[dispFrame].PageFirstLine;
          }
          this.e.frame[dispFrame].ScrX = this.e.frame[dispFrame].x;
          this.e.frame[dispFrame].ScrY = this.e.frame[dispFrame].y + this.e.frame[dispFrame].SpaceTop;
          this.e.frame[dispFrame].ScrWidth = this.e.frame[dispFrame].width;
          this.e.frame[dispFrame].ScrHeight = this.e.frame[dispFrame].height - this.e.frame[dispFrame].SpaceTop;
          this.e.frame[dispFrame].ScrFirstLine = -1;
          if (this.e.PaintFlag != 2 && this.e.PaintFlag != 3)
            this.e.frame[dispFrame].ScrLastLine = -1;
          this.e.frame[dispFrame].flags &= 4;
          int border = this.e.frame[dispFrame].border;
          bool flag6 = false;
          if (this.e.AllTextAngle > 0)
            flag6 = true;
          else if (this.e.frame[dispFrame].ParaFrameId > 0)
            flag6 = this.e.ParaFrame[this.e.frame[dispFrame].ParaFrameId].TextAngle > 0;
          else if (this.e.frame[dispFrame].CellId > 0)
            flag6 = this.e.cell[this.e.frame[dispFrame].CellId].TextAngle > 0;
          if ((this.e.frame[dispFrame].ScrX < this.e.TerWinOrgX + this.e.TerWinWidth || flag6) && (this.e.frame[dispFrame].ScrX + this.e.frame[dispFrame].ScrWidth > this.e.TerWinOrgX || flag6) && this.e.frame[dispFrame].y < this.e.TerWinOrgY + this.e.TerWinHeight && (this.e.frame[dispFrame].ScrY + this.e.frame[dispFrame].ScrHeight > this.e.TerWinOrgY || flag6) && (this.e.frame[dispFrame].CellId <= 0 || (this.e.cell[this.e.frame[dispFrame].CellId].flags & 16 /*0x10*/) == 0 || (this.e.frame[dispFrame].flags & 32768 /*0x8000*/) != 0))
          {
            this.e.frame[dispFrame].flags |= 4;
            if (!this.e.frame[dispFrame].empty && (this.e.frame[dispFrame].flags & 8) != 0)
            {
              int pageFirstLine4;
              this.e.frame[dispFrame].ScrLastLine = pageFirstLine4 = this.e.frame[dispFrame].PageFirstLine;
              this.e.frame[dispFrame].ScrFirstLine = pageFirstLine4;
              if (this.e.PaintFlag != 2 && this.e.PaintFlag != 3)
              {
                int pageFirstLine5;
                this.e.frame[dispFrame].ScrLastLine = pageFirstLine5 = this.e.frame[dispFrame].PageFirstLine;
                this.e.frame[dispFrame].ScrFirstLine = pageFirstLine5;
                this.e.frame[dispFrame].RowOffset = idx;
                this.e.RowX[idx] = 0;
                this.e.RowY[idx] = this.e.frame[dispFrame].ScrY;
                this.e.RowHeight[idx] = 0;
                idx += 2;
                if (idx >= this.e.MaxLinesPerWin)
                  this.ExpandRowArrays(idx);
              }
              if (this.e.PaintFlag != 2)
              {
                if ((this.e.ParaFrame[paraFrameId].flags & 256 /*0x0100*/) != 0)
                  this.DrawLineObject(gr, dispFrame);
                else if ((this.e.ParaFrame[paraFrameId].flags & 512 /*0x0200*/) != 0)
                  this.DrawRectObject(gr, dispFrame);
              }
            }
            else if (!this.e.frame[dispFrame].empty && (this.e.frame[dispFrame].flags & 2048 /*0x0800*/) != 0)
              this.DrawPictFrame(gr, dispFrame);
            else if ((this.e.frame[dispFrame].flags & 8192 /*0x2000*/) != 0)
            {
              this.DrawFootnote(gr, dispFrame, true);
            }
            else
            {
              this.e.FrameShading = this.e.frame[dispFrame].shading;
              this.e.FrameBkColor = this.e.frame[dispFrame].BackColor;
              this.e.PaintFrameFlags = this.e.frame[dispFrame].flags;
              if ((this.e.ParaFrame[paraFrameId].flags & 131200 /*0x020080*/) != 0 && (this.IsSameColor(this.e.FrameBkColor, tc.CLR_WHITE) || this.e.frame[dispFrame].CellId == 0))
              {
                if (this.e.ParaFrame[paraFrameId].FillPattern > 0)
                  this.e.FrameBkColor = this.e.ParaFrame[paraFrameId].BackColor;
                this.e.FrameShading = -1;
              }
              int num13;
              int num14 = num13 = 0;
              if ((this.e.frame[dispFrame].border & 1) != 0)
                num14 = this.TwipsToScrY(this.e.frame[dispFrame].BorderWidth[0]);
              if ((this.e.frame[dispFrame].border & 4) != 0)
                num13 = this.TwipsToScrY(this.e.frame[dispFrame].BorderWidth[2]);
              if (this.e.FullRenderMode && this.e.PaintFlag >= 4 && this.e.frame[dispFrame].SpaceTop - num14 > 0 && this.e.frame[dispFrame].ScrY > this.e.TerWinOrgY && this.e.frame[dispFrame].y < this.e.TerWinOrgY + this.e.TerWinHeight && this.LineTextAngle(-dispFrame) == 0)
              {
                COp.RECT OurRect;
                this.SetRect(out OurRect, this.e.frame[dispFrame].ScrX + num13, this.e.frame[dispFrame].y + num14, this.e.frame[dispFrame].ScrX + this.e.frame[dispFrame].ScrWidth - num13 + 1, this.e.frame[dispFrame].ScrY);
                this.SetColor(gr, 'T', 0);
                if (this.e.BkPictId != 0)
                  this.e.transparent = true;
                this.TerTextOut(gr, OurRect.left, OurRect.top, OurRect, (char[]) null, 0, (ushort[]) null, 0);
              }
              this.e.frame[dispFrame].ScrFirstLine = this.e.frame[dispFrame].PageFirstLine;
              if (this.e.PaintFlag != 2 && this.e.PaintFlag != 3)
                this.e.frame[dispFrame].ScrLastLine = this.e.frame[dispFrame].PageLastLine;
              int scrX = this.e.frame[dispFrame].ScrX;
              int num15 = this.e.frame[dispFrame].ScrX + this.e.frame[dispFrame].ScrWidth;
              int y = this.e.frame[dispFrame].y;
              int num16 = y + this.e.frame[dispFrame].height;
              if (this.e.frame[dispFrame].empty && this.e.frame[dispFrame].CellId > 0 && this.e.cell[this.e.frame[dispFrame].CellId].NextCell == -1 && (this.e.frame[dispFrame].flags & 1048576 /*0x100000*/) != 0)
              {
                if ((this.e.frame[dispFrame].flags & 131072 /*0x020000*/) != 0)
                  ++scrX;
                else
                  --num15;
              }
              if (((this.e.frame[dispFrame].empty ? 1 : (this.e.frame[dispFrame].ScrY >= this.e.TerWinOrgY ? 1 : 0)) | (flag6 ? 1 : 0)) == 0)
              {
                int scrY = this.e.frame[dispFrame].ScrY;
                int scrFirstLine = this.e.frame[dispFrame].ScrFirstLine;
                while (true)
                {
                  int num17;
                  if (this.TableLevel(scrFirstLine) != this.e.frame[dispFrame].level)
                  {
                    num17 = 0;
                  }
                  else
                  {
                    num17 = scrFirstLine != 0 || num4 == -1 ? this.pos.ScrLineHeight(scrFirstLine, true) : num4;
                    if (scrFirstLine == 0)
                      num4 = num17;
                  }
                  if (scrY + num17 <= this.e.TerWinOrgY && scrFirstLine != this.e.frame[dispFrame].ScrLastLine)
                  {
                    scrY += num17;
                    ++scrFirstLine;
                  }
                  else
                    break;
                }
                this.e.frame[dispFrame].ScrHeight = this.e.frame[dispFrame].height - (scrY - this.e.frame[dispFrame].ScrY);
                this.e.frame[dispFrame].ScrY = scrY;
                this.e.frame[dispFrame].ScrFirstLine = scrFirstLine;
                if (this.e.BeginLine < 0)
                  this.e.BeginLine = scrFirstLine;
                this.e.frame[dispFrame].border &= -2;
              }
              if (this.e.frame[dispFrame].ScrY + this.e.frame[dispFrame].ScrHeight > this.e.TerWinOrgY + this.e.TerWinHeight && !flag6)
              {
                this.e.frame[dispFrame].ScrHeight = this.e.TerWinOrgY + this.e.TerWinHeight - this.e.frame[dispFrame].ScrY;
                this.e.frame[dispFrame].border &= -3;
              }
              if (dispFrame < this.e.TotalFrames - 1 && (this.e.frame[dispFrame].flags & 1) != 0 && !flag6)
                this.e.frame[dispFrame].ScrWidth = this.e.TerWinOrgX + this.e.TerWinWidth - this.e.frame[dispFrame].ScrX;
              if (dispFrame == this.e.TotalFrames - 1 && !flag6)
              {
                this.e.frame[dispFrame].ScrWidth = this.e.TerWinOrgX + this.e.TerWinWidth - this.e.frame[dispFrame].ScrX;
                this.e.frame[dispFrame].ScrHeight = this.e.TerWinOrgY + this.e.TerWinHeight - this.e.frame[dispFrame].ScrY;
              }
              this.e.SectLine = this.GetSectDisplayLine(this.e.CurFrame);
              if (this.e.PaintFlag == 2)
              {
                if (!this.e.frame[dispFrame].empty && this.TableLevel(PaintLine) == this.e.frame[dispFrame].level && PaintLine >= this.e.frame[dispFrame].ScrFirstLine && PaintLine <= this.e.frame[dispFrame].ScrLastLine)
                {
                  this.PaintRows(gr, PaintLine - this.e.frame[dispFrame].ScrFirstLine, PaintLine - this.e.frame[dispFrame].ScrFirstLine);
                  break;
                }
              }
              else if (this.e.PaintFlag == 3)
              {
                for (PaintLine = this.e.PaintBegLine; PaintLine <= this.e.PaintEndLine; ++PaintLine)
                {
                  if (!this.e.frame[dispFrame].empty && this.TableLevel(PaintLine) == this.e.frame[dispFrame].level && PaintLine >= this.e.frame[dispFrame].ScrFirstLine && PaintLine <= this.e.frame[dispFrame].ScrLastLine)
                    this.PaintRows(gr, PaintLine - this.e.frame[dispFrame].ScrFirstLine, PaintLine - this.e.frame[dispFrame].ScrFirstLine);
                }
                if (!this.e.frame[dispFrame].empty && this.e.PaintEndLine >= this.e.frame[dispFrame].ScrFirstLine && this.e.PaintEndLine <= this.e.frame[dispFrame].ScrLastLine)
                  break;
              }
              else if (this.e.PaintFlag != 2 && this.e.PaintFlag != 3)
              {
                this.e.frame[this.e.CurFrame].RowOffset = idx;
                if ((this.e.frame[dispFrame].flags & 65536 /*0x010000*/) == 0)
                  this.PaintRows(gr, 0, 9999);
                if (this.e.frame[this.e.CurFrame].CellId > 0 && (this.e.HilightType == 0 || !this.e.StretchHilight) && (this.e.frame[this.e.CurFrame].flags & 528384 /*0x081000*/) == 0 && (this.e.TerFlags3 & 2) == 0)
                {
                  int rowId = this.e.frame[this.e.CurFrame].RowId;
                  bool flag7 = (this.e.frame[this.e.CurFrame].flags & 131072 /*0x020000*/) != 0;
                  if ((this.e.TerFlags5 & 1048576 /*0x100000*/) == 0)
                  {
                    if (this.e.TableRow[rowId].FirstCell == this.e.frame[this.e.CurFrame].CellId)
                      this.CreateCellDragObj(10, this.e.frame[dispFrame].RowId, flag7 ? num15 : scrX, y, num16);
                    this.CreateCellDragObj(9, this.e.frame[dispFrame].CellId, flag7 ? scrX : num15, y, num16);
                  }
                  if ((this.e.TerFlags5 & 131072 /*0x020000*/) == 0)
                    this.CreateRowDragObj(11, this.e.frame[dispFrame].CellId, flag7 ? num15 : scrX, flag7 ? scrX : num15, num16);
                }
                if (!this.e.frame[dispFrame].empty)
                {
                  int num18 = this.e.frame[dispFrame].ScrLastLine - this.e.frame[dispFrame].ScrFirstLine + 1;
                  idx = idx + num18 + 1;
                  if (idx >= this.e.MaxLinesPerWin)
                    this.ExpandRowArrays(idx);
                }
                if ((this.e.frame[dispFrame].flags & 480) != 0)
                  this.DrawPageBorder(gr, dispFrame);
              }
              if ((this.e.frame[dispFrame].flags & 65536 /*0x010000*/) != 0)
                this.DrawPageBorderBox(gr, dispFrame);
              this.e.frame[dispFrame].border = border;
              if (!this.e.frame[dispFrame].empty)
                this.e.WinHeight += this.e.frame[dispFrame].ScrLastLine - this.e.frame[dispFrame].ScrFirstLine + 1;
            }
          }
        }
      }
    }
    this.e.CurFrame = curFrame;
    if (this.e.BeginLine < 0)
      this.e.BeginLine = this.e.CurLine;
    this.e.EnableTransparent = false;
  }

  /// <summary>Нарисовать строки</summary>
  /// <param name="gr"></param>
  /// <param name="FirstRow"></param>
  /// <param name="LastRow"></param>
  internal new void PaintRows(Graphics gr, int FirstRow, int LastRow)
  {
    int pBegCol = 0;
    int pEndCol = 0;
    int pBegRow = 0;
    int pEndRow = 0;
    COp.RECT rect = new COp.RECT();
    int num1 = -1;
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    bool flag4 = false;
    bool flag5 = false;
    if (tc.DebugMode)
      this.misc.dm(nameof (PaintRows));
    int winHeight = this.e.WinHeight;
    int hilightType = this.e.HilightType;
    if (this.e.HilightType == 2 && this.e.PictureClicked && this.e.HilightBegRow == this.e.HilightEndRow && this.e.HilightBegCol == this.e.HilightEndCol - 1)
      this.e.HilightType = 0;
    if (FirstRow < 0)
      FirstRow = 0;
    int scrX1 = this.e.frame[this.e.CurFrame].ScrX;
    int scrY;
    this.e.TsFrameY = scrY = this.e.frame[this.e.CurFrame].ScrY;
    int pVal1 = scrX1 + this.e.frame[this.e.CurFrame].ScrWidth;
    int pVal2_1 = scrY + this.e.frame[this.e.CurFrame].ScrHeight;
    int pageLastLine = this.e.frame[this.e.CurFrame].PageLastLine;
    if (pageLastLine < this.e.TotalLines && this.LineInfo(pageLastLine, 32 /*0x20*/) && pageLastLine > this.e.frame[this.e.CurFrame].PageFirstLine)
      --pageLastLine;
    this.e.FrameCellId = this.e.frame[this.e.CurFrame].CellId;
    this.e.FrameRowId = this.e.frame[this.e.CurFrame].RowId;
    this.e.FrameHeight = this.e.frame[this.e.CurFrame].height;
    this.e.ScrFrameAngle = 0;
    int level1 = this.e.frame[this.e.CurFrame].level;
    int num2 = scrX1;
    this.e.EnableTransparent = (this.e.frame[this.e.CurFrame].flags & 16 /*0x10*/) != 0;
    if (this.e.BkPictId != 0)
      this.e.EnableTransparent = true;
    if ((this.e.TerFlags2 & 32 /*0x20*/) != 0)
      this.e.EnableTransparent = true;
    this.e.FullCellHilight = false;
    int rowOffset = this.e.frame[this.e.CurFrame].RowOffset;
    if (this.e.PaintFlag != 2)
    {
      this.e.RowY[rowOffset] = scrY;
      if (!this.e.TerArg.PageMode)
        this.e.RowY[rowOffset] -= this.e.WinYOffset;
    }
    if (this.e.frame[this.e.CurFrame].ScrWidth == 0 || this.e.frame[this.e.CurFrame].ScrHeight == 0)
    {
      bool flag6 = true;
      if (this.e.frame[this.e.CurFrame].PageFirstLine == pageLastLine && pageLastLine >= 0 && pageLastLine < this.e.TotalLines && (this.e.PfmtId[this.e.text[pageLastLine].pfmt].flags & 12288 /*0x3000*/) != 0)
        flag6 = false;
      if (flag6)
        goto label_249;
    }
    if (this.e.HilightType != 0)
    {
      pBegRow = this.e.HilightBegRow;
      pEndRow = this.e.HilightEndRow;
      pBegCol = this.e.HilightBegCol;
      pEndCol = this.e.HilightEndCol;
      this.AdjustBlockForTable(ref pBegRow, ref pBegCol, ref pEndRow, ref pEndCol, false);
      if (this.e.HilightType == 2)
      {
        if (pBegRow > pEndRow)
        {
          int num3 = pEndRow;
          pEndRow = pBegRow;
          pBegRow = num3;
          int num4 = pEndCol;
          pEndCol = pBegCol;
          pBegCol = num4;
        }
        if (pBegRow == pEndRow && pBegCol > pEndCol)
        {
          int num5 = pEndCol;
          pEndCol = pBegCol;
          pBegCol = num5;
        }
      }
      else
      {
        if (pBegRow > pEndRow)
        {
          int num6 = pEndRow;
          pEndRow = pBegRow;
          pBegRow = num6;
        }
        if (pBegCol > pEndCol)
        {
          int num7 = pEndCol;
          pEndCol = pBegCol;
          pBegCol = num7;
        }
      }
      int level2 = this.MinTableLevel(pBegRow, pEndRow);
      int num8 = num1 = this.LevelCell(level2, pBegRow);
      int num9 = this.LevelCell(level2, pEndRow);
      flag2 = this.TableHilighted();
      if (num8 == num9 && num8 > 0)
        flag4 = true;
    }
    if (this.e.frame[this.e.CurFrame].empty)
      flag5 = true;
    if (this.e.PaintFlag >= 4 && this.e.frame[this.e.CurFrame].CellId > 0 && this.LineTextAngle(-this.e.CurFrame) != 0)
      flag5 = true;
    if (flag5)
    {
      Color frameBkColor = this.e.FrameBkColor;
      bool flag7 = false;
      int cellId = this.e.frame[this.e.CurFrame].CellId;
      int firstLine = this.e.cell[cellId].FirstLine;
      if ((this.e.frame[this.e.CurFrame].flags & 480) != 0 && (this.e.BkPictId == 0 || (this.e.TerFlags2 & 16 /*0x10*/) == 0))
        flag7 = true;
      if (cellId > 0 && (this.e.frame[this.e.CurFrame].flags1 & 1) != 0 && this.e.HilightType != 0 && !flag4 && firstLine >= this.e.HilightBegRow && firstLine <= this.e.HilightEndRow && this.LineSelected(firstLine))
        this.e.FullCellHilight = true;
      if (flag7)
        this.e.FrameBkColor = this.e.PageBorderColor;
      else if (this.e.FullRenderMode)
        this.SetColor(gr, 'T', 0);
      if (cellId > 0 && this.e.cell[cellId].TextAngle > 0)
        this.ClearEOL(gr, this.e.frame[this.e.CurFrame].x, this.e.frame[this.e.CurFrame].y, this.e.frame[this.e.CurFrame].x + this.e.frame[this.e.CurFrame].width, this.e.frame[this.e.CurFrame].height, !this.e.EnableTransparent);
      else
        this.ClearEOL(gr, scrX1, this.e.RowY[rowOffset], pVal1, pVal2_1 - this.e.RowY[rowOffset], !this.e.EnableTransparent);
      if (flag7)
        this.e.FrameBkColor = frameBkColor;
      this.e.FullCellHilight = false;
      if (this.e.frame[this.e.CurFrame].empty)
        goto label_249;
    }
    if (this.e.frame[this.e.CurFrame].ParaFrameId > 0)
      this.e.ScrFrameAngle = this.e.ParaFrame[this.e.frame[this.e.CurFrame].ParaFrameId].TextAngle;
    else if (this.e.frame[this.e.CurFrame].CellId > 0)
      this.e.ScrFrameAngle = this.e.cell[this.e.frame[this.e.CurFrame].CellId].TextAngle;
    else if (this.e.AllTextAngle > 0)
      this.e.ScrFrameAngle = this.e.AllTextAngle;
    if (this.e.ScrFrameAngle > 0)
    {
      if (tc.OSCanRotate)
        this.FrameRotateDC(gr, this.e.CurFrame);
      this.SwapInts(ref pVal1, ref pVal2_1);
    }
    for (int index1 = FirstRow; index1 <= LastRow; ++index1)
    {
      int index2 = this.e.frame[this.e.CurFrame].ScrFirstLine + index1;
      if (this.e.TerArg.PageMode && index2 < this.e.TotalLines && this.TableLevel(index2) != level1)
      {
        if (rowOffset + index1 + 1 >= this.e.MaxLinesPerWin)
          this.ExpandRowArrays(rowOffset);
        this.e.RowY[rowOffset + index1 + 1] = this.e.RowY[rowOffset + index1];
        this.e.RowHeight[rowOffset + index1] = 0;
      }
      else
      {
        this.e.TextBorder = 0;
        this.e.ParaBoxColor = tc.CLR_AUTO;
        this.e.BoxRight = 0;
        this.e.BoxLeft = 0;
        int num10;
        this.e.ParaShading = num10 = 0;
        this.e.SpaceBef = this.e.SpaceAft = this.e.ScrYOffset = 0;
        this.e.ParaSpcBef = this.e.ParaSpcAft = 0;
        this.e.CharBoxLeft = this.e.CharBoxRight = this.e.bullet = this.e.ShadeBegin = this.e.ShadeEnd = false;
        this.e.ExpandLineFmt = false;
        this.e.ScrRtl = index2 >= 0 && index2 < this.e.TotalLines && (this.e.text[index2].flags2 & 32 /*0x20*/) != 0;
        this.e.TotalScrSeg = this.e.CurScrSeg = 0;
        bool flag8 = this.e.ScrRtl || this.e.ScrFrameAngle > 0;
        if (index2 < this.e.TotalLines)
          this.e.CurPfmt = this.e.text[index2].pfmt;
        else
          this.e.CurPfmt = 0;
        this.e.TabFlags = (byte) 0;
        char minValue = char.MinValue;
        int pBulletWidth = 0;
        Color color;
        this.e.ParaBackColor = color = tc.CLR_WHITE;
        this.e.ParaBoxSpace = 20;
        if (index2 > pageLastLine || index2 >= this.e.TotalLines)
        {
          if (!this.e.TerArg.PageMode)
            this.e.WinHeight = index1;
          if (this.e.RowY[rowOffset + index1] < pVal2_1 && this.e.ScrFrameAngle == 0)
          {
            int num11 = 0;
            if (pVal2_1 == this.e.TerWinOrgY + this.e.TerWinHeight)
              num11 = 1;
            this.ClearEOL(gr, scrX1, this.e.RowY[rowOffset + index1], pVal1, pVal2_1 - this.e.RowY[rowOffset + index1] + num11, !this.e.EnableTransparent);
            if (!this.e.TerArg.PageMode && this.e.WinHeight < winHeight)
            {
              this.e.WinHeight = winHeight;
              break;
            }
            break;
          }
          break;
        }
        int pfmt = index2 >= this.e.TotalLines ? 0 : this.e.text[index2].pfmt;
        if (!this.e.TerArg.PrintView && this.LineInfo(index2, 16384 /*0x4000*/))
          this.UpdateDynField(index2, 0);
        if (index2 < this.e.TotalLines)
          this.CompressCfmt(index2);
        if (rowOffset + index1 + 1 >= this.e.MaxLinesPerWin)
          this.ExpandRowArrays(rowOffset + index1 + 1);
        int pBaseHeight;
        this.e.RowHeight[rowOffset + index1] = this.GetLineHeight(index2, out pBaseHeight, out tc.SkipInt);
        if (this.e.TerArg.PageMode && this.e.text[index2].height == 0)
          this.e.RowHeight[rowOffset + index1] = 0;
        this.e.DispTextHt = this.e.RowHeight[rowOffset + index1];
        if (this.e.TerArg.WordWrap)
        {
          int num12;
          this.e.RowX[rowOffset + index1] = num12 = this.e.frame[this.e.CurFrame].SpaceLeft + this.e.PfmtId[pfmt].LeftIndent;
          if ((this.e.text[index2].flags & 4) != 0)
          {
            bool pHasBullet;
            this.e.RowX[rowOffset + index1] += this.GetFirstIndent(index2, out pBulletWidth, out pHasBullet, true);
            this.e.bullet = pHasBullet;
          }
          if (this.e.PfmtId[pfmt].FirstIndent < 0)
            num12 += this.e.PfmtId[pfmt].FirstIndent;
          num2 = num12 + scrX1;
          int flags = this.e.PfmtId[pfmt].flags;
          if ((flags & 65776 /*0x0100F0*/) != 0 && this.e.text[index2].fid == 0)
            this.SetParaBorder(index2);
          this.e.BoxLeft = scrX1 + this.e.frame[this.e.CurFrame].SpaceLeft + this.e.PfmtId[pfmt].LeftIndent - this.TwipsToScrX(50);
          if (this.e.PfmtId[pfmt].FirstIndent < 0)
            this.e.BoxLeft += this.e.PfmtId[pfmt].FirstIndent;
          this.e.BoxRight = scrX1 + this.e.frame[this.e.CurFrame].SpaceLeft + this.TerWrapWidth(index2, -1) - this.e.PfmtId[pfmt].RightIndent + this.TwipsToScrX(50);
          if ((this.e.text[index2].flags & 4194304 /*0x400000*/) != 0)
            this.e.ShadeBegin = true;
          if ((this.e.text[index2].flags & 8388608 /*0x800000*/) != 0)
            this.e.ShadeEnd = true;
          this.e.ParaBoxSpace = this.e.PfmtId[pfmt].BorderSpace;
          this.e.ParaBoxColor = this.e.PfmtId[pfmt].BorderColor;
          if (!this.e.TerArg.PageMode && this.e.text[index2].tabw != null && (this.e.text[index2].tabw.type & 16 /*0x10*/) != 0)
            this.e.TextBorder |= 2;
          num10 = this.e.PfmtId[pfmt].shading;
          color = this.e.PfmtId[pfmt].BkColor;
          flag3 = num10 > 0 || color != tc.CLR_WHITE;
          bool flag9 = false;
          if (this.e.ScrRtl)
          {
            if ((flags & 2) != 0)
              flag9 = true;
          }
          else if ((flags & 2) != 0)
            flag9 = true;
          if ((flags & 1) != 0 | flag9)
          {
            bool flag10 = !this.LineInfo(index2, 1024 /*0x0400*/) && (this.e.text[index2].flags & 536870912 /*0x20000000*/) == 0;
            int num13 = 0;
            if (!flag10 && this.e.text[index2].tabw != null && this.e.text[index2].tabw.FrameCharPos == 0)
            {
              flag10 = true;
              num13 = this.e.text[index2].tabw.FrameScrWidth;
            }
            if (flag10)
            {
              int num14 = this.TerWrapWidth(index2, -1) - (this.e.RowX[rowOffset + index1] - this.e.frame[this.e.CurFrame].SpaceLeft) - this.e.PfmtId[pfmt].RightIndent - this.GetLineWidth(index2, false, true) - num13;
              if (num14 > 0)
              {
                if ((this.e.PfmtId[pfmt].flags & 1) != 0)
                  this.e.RowX[rowOffset + index1] += num14 / 2;
                else
                  this.e.RowX[rowOffset + index1] += num14;
              }
            }
          }
          if (this.e.TerArg.PageMode && (this.e.text[index2].flags & 536870912 /*0x20000000*/) != 0 && this.e.text[index2].tabw != null && this.e.text[index2].tabw.count > 0)
            this.e.RowX[rowOffset + index1] += this.TwipsToScrX(this.e.text[index2].tabw.width[0]);
        }
        else
          this.e.RowX[rowOffset + index1] = 0;
        int objSpcBef = this.GetObjSpcBef(index2, true);
        if (objSpcBef > 0)
        {
          int textBorder = this.e.TextBorder;
          this.e.TextBorder = 0;
          if (this.e.FullRenderMode)
          {
            this.SetColor(gr, 'T', 0);
            this.ClearEOL(gr, scrX1, this.e.RowY[rowOffset + index1], pVal1, objSpcBef, !this.e.EnableTransparent);
          }
          if (this.e.PaintFlag != 2)
            this.e.RowY[rowOffset + index1] += objSpcBef;
          this.e.TextBorder = textBorder;
        }
        int SpcBef;
        int SpcAft;
        int pParaSpcBef;
        int pParaSpcAft;
        this.GetLineSpacing2(index2, this.e.RowHeight[rowOffset + index1], out SpcBef, out SpcAft, out pParaSpcBef, out pParaSpcAft, true);
        this.e.SpaceBef = SpcBef;
        this.e.SpaceAft = SpcAft;
        this.e.ParaSpcBef = pParaSpcBef;
        this.e.ParaSpcAft = pParaSpcAft;
        this.e.RowY[rowOffset + index1 + 1] = this.e.RowY[rowOffset + index1] + this.e.RowHeight[rowOffset + index1];
        this.e.RowY[rowOffset + index1 + 1] += this.e.SpaceBef + this.e.SpaceAft;
        if (this.e.TerArg.PageMode)
        {
          this.e.RowY[rowOffset + index1 + 1] = this.e.RowY[rowOffset + index1] + this.ScrLineHeight(index2, false);
          this.e.ScrYOffset = this.e.RowY[rowOffset + index1 + 1] - this.e.RowHeight[rowOffset + index1] - this.e.SpaceBef - this.e.SpaceAft - this.e.RowY[rowOffset + index1];
          if (this.e.ScrYOffset < -1)
            this.e.ScrYOffset = -1;
          this.e.RowHeight[rowOffset + index1] = this.e.RowY[rowOffset + index1 + 1] - this.e.RowY[rowOffset + index1] - this.e.SpaceBef - this.e.SpaceAft;
        }
        if (this.e.RowY[rowOffset + index1 + 1] > this.e.TerWinOrgY + this.e.TerWinHeight + 1 && index1 != 0 && this.e.ScrFrameAngle == 0)
        {
          if (pVal2_1 - this.e.RowY[rowOffset + index1] > this.e.ScrResY / 4 && !flag1)
          {
            flag1 = true;
          }
          else
          {
            if (!this.e.TerArg.PageMode)
              this.e.WinHeight = index1 + 1;
            if (this.e.RowY[rowOffset + index1] < pVal2_1)
            {
              this.e.TextBorder = 0;
              this.e.SpaceBef = this.e.SpaceAft = 0;
              this.ClearEOL(gr, scrX1, this.e.RowY[rowOffset + index1], pVal1, pVal2_1 - this.e.RowY[rowOffset + index1], !this.e.EnableTransparent);
              break;
            }
            break;
          }
        }
        if ((this.e.TerFlags2 & 32 /*0x20*/) != 0 && this.e.PaintFlag != 4)
          this.ClearEOL(gr, scrX1, this.e.RowY[rowOffset + index1], pVal1, this.e.RowY[rowOffset + index1 + 1] - this.e.RowY[rowOffset + index1], true);
        int num15 = this.e.RowY[rowOffset + index1];
        if (this.e.PaintFlag != 2 && this.e.PaintFlag != 3)
          this.e.frame[this.e.CurFrame].ScrLastLine = index2;
        if (this.e.CurLine == index2)
          this.e.CurLineY = num15 - this.e.TerWinOrgY;
        if (this.e.TerArg.WordWrap && this.e.TerArg.PrintView && !this.e.TerArg.PageMode)
        {
          int index3 = 0;
          while (index3 < this.e.TotalPages && (this.e.PageInfo[index3].FirstLine - 1 != index2 || (this.e.PageInfo[index3].flags & 1) != 0))
            ++index3;
          if (index3 < this.e.TotalPages && (this.e.text[index2].tabw == null || (this.e.text[index2].tabw.type & 6) == 0))
            this.e.TextBorder = 2;
        }
        if (this.e.text[index2].tabw != null && (this.e.text[index2].tabw.type & 16384 /*0x4000*/) != 0)
          this.UpdateDynField(index2, this.e.CurFrame >= this.e.FirstPage2Frame ? this.e.FirstFramePage + 1 : this.e.FirstFramePage);
        if (this.DoAutoSpellCheck() && (this.e.text[index2].flags2 & 1) == 0)
          this.AutoSpellCheck(index2);
        char[] ptr = this.e.text[index2].txt;
        int len = this.e.text[index2].len;
        if (this.e.TerArg.WordWrap && len > 0 && this.e.ShowParaMark && (this.e.text[index2].flags & 2048 /*0x0800*/) != 0)
        {
          --len;
          minValue = ptr[len];
        }
        while (len > 0 && (!this.e.ShowParaMark || (this.e.text[index2].flags & 2048 /*0x0800*/) == 0) && !this.e.ScrRtl && ptr[len - 1] == ' ')
          --len;
        if (!this.e.TerArg.WordWrap && this.e.text[index2].fmt == null)
        {
          int index4 = 0;
          while (index4 < len && ptr[index4] != '\t')
            ++index4;
          if (index4 != len)
            this.ExpandCfmt(index2);
        }
        if (this.e.text[index2].fmt != null)
          this.e.TempCfmt = this.e.text[index2].fmt;
        if (this.e.TerArg.WordWrap && len > 0 && !this.e.ShowParaMark && (this.e.text[index2].flags & 536873089) != 0)
          this.e.ExpandLineFmt = true;
        bool flag11 = (this.e.TerFlags2 & 12582912 /*0xC00000*/) != 0 || this.e.mbcs;
        if (flag11 && this.e.TerArg.WordWrap && len > 0 && this.e.ShowParaMark && (this.e.text[index2].flags & 2177) != 0)
        {
          if (this.e.text[index2].fmt != null)
          {
            this.e.LineCfmt = new ushort[len + 1];
            for (int index5 = 0; index5 < len; ++index5)
              this.e.LineCfmt[index5] = this.e.text[index2].fmt[index5];
            this.e.TempCfmt = this.e.LineCfmt;
          }
          this.e.ExpandLineFmt = true;
        }
        if (this.e.ScrRtl)
          this.e.ExpandLineFmt = true;
        if (this.e.ExpandLineFmt && this.e.text[index2].fmt == null)
        {
          this.e.LineCfmt = new ushort[len + 1];
          for (int index6 = 0; index6 < len; ++index6)
            this.e.LineCfmt[index6] = this.e.text[index2].UniFmt;
          this.e.TempCfmt = this.e.LineCfmt;
        }
        if (this.e.ExpandLineFmt || this.e.text[index2].fmt != null)
        {
          this.e.LineText = new char[len + 1];
          for (int index7 = 0; index7 < len; ++index7)
          {
            if (ptr[index7] == '\u0017')
              this.e.LineText[index7] = '-';
            else if (ptr[index7] == '\u000E')
              this.e.LineText[index7] = ' ';
            else if (ptr[index7] == '\u0006')
            {
              if (this.e.ShowParaMark)
                this.e.LineText[index7] = '¬';
              else if (index7 == len - 1)
                this.e.LineText[index7] = '-';
              else
                this.e.LineText[index7] = ptr[index7];
              this.e.HasOptionalHyph = true;
            }
            else if (ptr[index7] == '\u001C')
            {
              if (this.e.ShowParaMark)
                this.e.LineText[index7] = '\u0095';
              else
                this.e.LineText[index7] = ptr[index7];
            }
            else
              this.e.LineText[index7] = ptr[index7];
          }
          ptr = this.e.LineText;
          if (flag11 && this.e.ShowParaMark && len > 0 && ((int) ptr[len - 1] == (int) this.e.ParaChar || (int) ptr[len - 1] == (int) this.e.CellChar || ptr[len - 1] == '\u000F'))
          {
            int OldFont = (int) this.e.TempCfmt[len - 1];
            string NewTypeFace = "Arial";
            int num16 = this.e.TerFont[OldFont].TwipsSize / 20;
            if ((int) ptr[len - 1] == (int) this.e.ParaChar && (this.e.TerFlags2 & 4194304 /*0x400000*/) != 0)
              NewTypeFace = "Symbol";
            if (ptr[len - 1] == '\u000F' && (this.e.TerFlags2 & 8388608 /*0x800000*/) != 0)
            {
              NewTypeFace = "Symbol";
              if (num16 > 3)
                num16 -= 3;
            }
            this.e.TempCfmt[len - 1] = (ushort) this.GetNewFont(gr, OldFont, NewTypeFace, num16 * 20, 0, this.e.TerFont[OldFont].TextColor, this.e.TerFont[OldFont].TextBkColor, this.e.TerFont[OldFont].UlineColor, 0, 0, 0, 1, 0, 0, 0, 0, (string) null, 0, (byte) 1, 0, this.e.TerFont[OldFont].TextAngle);
          }
        }
        this.e.pLineCharWidth = this.pos.GetLineCharWidth(index2);
        this.e.pScrSeg = (tc.StrLineSeg[]) null;
        int pTotalLineSeg = 0;
        if (this.e.ScrRtl)
          this.e.pScrSeg = this.GetLineSeg(index2, this.e.pLineCharWidth, out pTotalLineSeg);
        this.e.TotalScrSeg = pTotalLineSeg;
        int HiEndCol;
        int HiBegCol = HiEndCol = -1;
        this.e.FullCellHilight = false;
        if (this.e.HilightType != 0 && index2 >= pBegRow && index2 <= pEndRow && this.LineSelected(index2))
        {
          if (this.e.HilightType == 1)
          {
            HiBegCol = 0;
            HiEndCol = 9999;
          }
          else if (this.e.HilightType == 2)
          {
            if (index2 > pBegRow && index2 < pEndRow)
            {
              HiBegCol = 0;
              HiEndCol = len;
            }
            else if (index2 == pBegRow && index2 == pEndRow)
            {
              HiBegCol = pBegCol;
              HiEndCol = pEndCol;
            }
            else if (index2 == pBegRow)
            {
              HiBegCol = pBegCol;
              HiEndCol = len;
            }
            else
            {
              HiBegCol = 0;
              HiEndCol = pEndCol;
            }
            if (this.e.text[index2].cid > 0 & flag2)
              this.e.FullCellHilight = true;
            if (index2 <= pEndRow && this.e.text[index2].cid > 0 && !flag4)
              this.e.FullCellHilight = true;
          }
          if (HiEndCol < HiBegCol)
            HiEndCol = HiBegCol;
        }
        this.e.NextX = scrX1 + this.e.RowX[rowOffset + index1];
        if (this.e.RowX[rowOffset + index1] > 0)
        {
          rect.left = scrX1;
          rect.top = this.e.RowY[rowOffset + index1];
          rect.right = !flag3 ? this.e.NextX : num2 - this.TwipsToScrX(50);
          rect.bottom = this.e.RowY[rowOffset + index1 + 1];
          char area = this.e.HilightType != 1 || HiBegCol < 0 ? 'T' : 'H';
          if (this.e.FullRenderMode)
          {
            this.SetColor(gr, area, 0);
            this.e.CurFmtId = 0;
            this.WriteSpaceRect(gr, rect);
          }
          if (this.e.FullRenderMode & flag3 && this.e.NextX > rect.right)
          {
            rect.left = rect.right;
            rect.right = this.e.NextX;
            this.e.ParaShading = num10;
            this.e.ParaBackColor = color;
            this.SetColor(gr, area, 0);
            this.WriteSpaceRect(gr, rect);
          }
        }
        this.e.TerOpFlags2 |= 256 /*0x0100*/;
        if (this.e.bullet)
          this.DrawBullet(gr, index2, pfmt, this.e.NextX - pBulletWidth, num15 + this.e.SpaceBef, pBaseHeight, true);
        this.e.TerOpFlags2 &= -257;
        this.e.NextTab = 0;
        this.e.NextSpace = 0;
        this.e.ParaShading = num10;
        this.e.ParaBackColor = color;
        if (this.e.SectLine >= 0)
          this.DrawSectLineNbr(gr, index2 - this.e.frame[this.e.CurFrame].PageFirstLine, scrX1 + this.e.frame[this.e.CurFrame].SpaceLeft, num15 + this.e.SpaceBef, pBaseHeight);
        this.DisplayText(gr, 0, num15, this.e.RowHeight[rowOffset + index1], pBaseHeight, ptr, len, index2, minValue == char.MinValue && !flag3, 'T');
        if ((this.e.text[index2].flags2 & 192 /*0xC0*/) != 0)
          this.DrawTrackingLine(gr, scrX1 + this.e.frame[this.e.CurFrame].SpaceLeft, num15, this.e.RowHeight[rowOffset + index1]);
        if (HiBegCol >= 0 && HiEndCol > 0 && !this.e.FullCellHilight)
          this.SaveHlLine(gr, index2, HiBegCol, HiEndCol, num15, this.e.RowHeight[rowOffset + index1] + this.e.SpaceBef + this.e.SpaceAft);
        if ((this.e.NextX < pVal1 | flag8) & flag3)
        {
          if (this.e.FullRenderMode)
          {
            if (this.e.NextX < this.e.BoxRight)
            {
              this.SetColor(gr, 'T', 0);
              this.ClearEOL(gr, this.e.NextX, num15, this.e.BoxRight, this.e.RowHeight[rowOffset + index1] + this.e.SpaceBef + this.e.SpaceAft, false);
              this.e.NextX = this.e.BoxRight;
            }
            this.e.ParaShading = 0;
            this.e.ParaBackColor = tc.CLR_WHITE;
            this.SetColor(gr, 'T', 0);
            this.ClearEOL(gr, this.e.NextX, num15, pVal1, this.e.RowHeight[rowOffset + index1] + this.e.SpaceBef + this.e.SpaceAft, false);
          }
        }
        else if (this.e.NextX < pVal1 | flag8 && minValue > char.MinValue && this.e.FullRenderMode)
        {
          int y = num15 + (this.e.RowHeight[rowOffset + index1] + this.e.SpaceBef + this.e.SpaceAft) / 2;
          this.ClearEOL(gr, this.e.NextX, num15, pVal1, this.e.RowHeight[rowOffset + index1] + this.e.SpaceBef + this.e.SpaceAft, false);
          if (minValue == '\u0014')
            this.WriteBreakLine(gr, this.e.MsgString[146], this.e.NextX, y, this.e.TerArg.PageMode ? pVal1 - this.e.NextX : this.e.TerWinWidth - this.e.NextX, (Pen) null);
          this.e.NextX = pVal1;
        }
      }
    }
    if (this.e.ScrFrameAngle > 0 && tc.OSCanRotate)
      this.FrameNoRotateDC(gr);
    this.e.ScrFrameAngle = 0;
label_249:
    if ((this.e.frame[this.e.CurFrame].border & 15) != 0 && (this.e.frame[this.e.CurFrame].flags & 8) == 0)
    {
      int scrX2 = this.e.frame[this.e.CurFrame].ScrX;
      int y = this.e.frame[this.e.CurFrame].y;
      int pVal2_2 = this.e.frame[this.e.CurFrame].ScrX + this.e.frame[this.e.CurFrame].width;
      int y2 = this.e.frame[this.e.CurFrame].y + this.e.frame[this.e.CurFrame].height;
      if (this.e.HtmlMode && this.e.frame[this.e.CurFrame].CellId > 0 && (this.e.TerFlags3 & 8) == 0 && (this.e.cell[this.e.frame[this.e.CurFrame].CellId].flags & 131072 /*0x020000*/) != 0)
      {
        this.DrawHtmlCellBorder(gr, this.e.frame[this.e.CurFrame].CellId, scrX2, y, pVal2_2, y2, this.e.frame[this.e.CurFrame].BorderWidth, true);
      }
      else
      {
        int num17 = this.e.frame[this.e.CurFrame].CellId <= 0 ? 0 : ((this.e.frame[this.e.CurFrame].flags & 131072 /*0x020000*/) != 0 ? 1 : 0);
        if (num17 != 0)
          this.SwapInts(ref scrX2, ref pVal2_2);
        this.DrawFrameBorder(gr, scrX2, y, pVal2_2, y, this.e.frame[this.e.CurFrame].BorderWidth[0], 1);
        this.DrawFrameBorder(gr, scrX2, y, scrX2, y2, this.e.frame[this.e.CurFrame].BorderWidth[2], 4);
        int num18 = this.e.frame[this.e.CurFrame].CellId > 0 ? y2 - 1 : y2;
        this.DrawFrameBorder(gr, scrX2, num18, pVal2_2, num18, this.e.frame[this.e.CurFrame].BorderWidth[1], 2);
        int num19 = num17 != 0 || this.e.frame[this.e.CurFrame].CellId == 0 ? pVal2_2 : pVal2_2 - 1;
        this.DrawFrameBorder(gr, num19, y, num19, y2, this.e.frame[this.e.CurFrame].BorderWidth[3], 8);
      }
    }
    this.e.TextBorder = 0;
    this.e.ParaBoxColor = tc.CLR_AUTO;
    this.e.FrameShading = 0;
    this.e.FrameHeight = 0;
    this.e.FrameRowId = 0;
    this.e.FrameCellId = 0;
    this.e.BoxRight = 0;
    this.e.BoxLeft = 0;
    this.e.ParaShading = 0;
    this.e.ScrYOffset = 0;
    this.e.TsFrameY = 0;
    this.e.SpaceAft = 0;
    this.e.SpaceBef = 0;
    this.e.ShadeEnd = false;
    this.e.ShadeBegin = false;
    this.e.transparent = false;
    this.e.EnableTransparent = false;
    this.e.FullCellHilight = false;
    this.e.bullet = false;
    this.e.CharBoxRight = false;
    this.e.CharBoxLeft = false;
    this.e.HilightType = hilightType;
    this.e.CurPfmt = 0;
    this.e.TabFlags = (byte) 0;
    this.e.ParaBackColor = tc.CLR_WHITE;
    this.e.FrameBkColor = tc.CLR_WHITE;
    this.e.ParaBoxSpace = 20;
    this.e.PaintFrameFlags = 0;
    this.e.ExpandLineFmt = false;
    this.e.ScrRtl = false;
    this.e.pLineCharWidth = (ushort[]) null;
    this.e.pScrSeg = (tc.StrLineSeg[]) null;
    this.e.CurScrSeg = 0;
    this.e.TotalScrSeg = 0;
    this.e.LineText = (char[]) null;
    this.e.LineCfmt = (ushort[]) null;
    this.e.SectLine = -1;
  }

  /// <summary>Нарисовать текст</summary>
  internal new bool TerTextOut(
    Graphics gr,
    int x,
    int y,
    COp.RECT rect,
    char[] ptr,
    int count,
    ushort[] pWidth,
    int BaseHeight)
  {
    int options = 0;
    int num1 = 4;
    bool flag1 = false;
    int num2 = count;
    int num3 = 0;
    COp.RECT TextRect = rect;
    if (count == 0)
      ptr = new char[0];
    if (!this.e.transparent)
      options = 2;
    if ((this.e.TerFlags2 & 32 /*0x20*/) != 0 && (this.e.transparent || this.e.ScrFrameAngle > 0 || this.e.ScrRtl))
      num1 = 0;
    bool flag2 = ((this.e.TerFlags4 & 256 /*0x0100*/) != 0 || (this.e.TerOpFlags & 268435456 /*0x10000000*/) != 0) && (this.e.TerFont[this.e.CurFmtId].flags & 1024 /*0x0400*/) != 0;
    if (count == 1)
    {
      char x1 = ptr[0];
      switch (x1)
      {
        case '\f':
        case '\u0012':
        case '\u0014':
        case '\u0016':
          if (this.e.TerArg.PageMode)
            this.e.NextX = rect.right = this.e.frame[this.e.CurFrame].x + this.e.frame[this.e.CurFrame].width - 10;
          else
            this.e.NextX = rect.right = rect.left + this.e.TerWinWidth;
          flag1 = true;
          break;
        default:
          if (!this.IsHdrFtrChar(x1))
            break;
          goto case '\f';
      }
    }
    if (rect.right >= this.e.TerWinOrgX || this.e.ScrFrameAngle != 0 || this.e.ScrRtl)
    {
      int style = this.e.TerFont[this.e.CurFmtId].style;
      int tempStyle = this.e.TerFont[this.e.CurFmtId].TempStyle;
      if ((this.e.TerFont[this.e.CurFmtId].flags & 1) != 0 && !this.e.ScrRtl)
        rect.right += this.fnt.LwrCharWidth(this.e.CurFmtId, true, 'W');
      if (!this.edit.HiddenText(this.e.CurFmtId) || count == 0)
      {
        int overhangFont = this.e.OverhangFont;
        if ((this.e.TerFont[this.e.CurFmtId].flags & 1) != 0 && count > 0)
          this.e.OverhangFont = this.e.CurFmtId;
        else
          this.e.OverhangFont = -1;
        this.TextSegmentExists(x, y, rect);
        if ((this.e.TerOpFlags & 32 /*0x20*/) != 0 && this.e.BkPictId != 0)
          options = 0;
        if ((this.e.TerFont[this.e.CurFmtId].flags & 1) != 0 && !this.e.ScrRtl)
        {
          if (overhangFont >= 0)
          {
            rect.left += this.fnt.LwrCharWidth(overhangFont, true, 'W');
            if (rect.left > rect.right)
              rect.left = rect.right;
          }
          else
          {
            int right = rect.right;
            rect.right = rect.left + this.fnt.LwrCharWidth(this.e.CurFmtId, true, ' ');
            this.OurExtTextOut(gr, x, y, 2, rect, (char[]) null, 0, (int[]) null);
            rect.right = right;
          }
        }
      }
      if (flag1)
      {
        this.OurExtTextOut(gr, x, y, options, rect, (char[]) null, 0, (int[]) null);
        y = rect.top + (rect.bottom - rect.top) / 2;
        int width = rect.right - rect.left;
        if ((!this.e.TerArg.ReadOnly || (this.e.TerFlags & 33554432 /*0x02000000*/) != 0) && (!this.e.TerArg.PageMode || this.e.ShowParaMark || (this.e.ShowPageBreak || (this.e.TerFlags5 & 262144 /*0x040000*/) != 0) && ptr[0] == '\f'))
        {
          if (ptr[0] == '\f')
          {
            this.WriteBreakLine(gr, this.e.MsgString[147], rect.left, y, width, Pens.Black);
            this.e.PageBreakShowing = true;
          }
          if (ptr[0] == '\u0016')
            this.WriteBreakLine(gr, this.e.MsgString[148], rect.left, y, width - this.TwipsToScrX(360), Pens.Black);
          if (ptr[0] == '\u0014')
            this.WriteBreakLine(gr, this.e.MsgString[146], rect.left, y, width, Pens.Black);
        }
        if (ptr[0] == '\u0011')
          this.WriteBreakLine(gr, this.e.MsgString[149], rect.left, y, width, (Pen) null);
        if (ptr[0] == '\u0010')
          this.WriteBreakLine(gr, this.e.MsgString[150], rect.left, y, width, (Pen) null);
        if (ptr[0] == '\u0019')
          this.WriteBreakLine(gr, this.e.MsgString[156], rect.left, y, width, (Pen) null);
        if (ptr[0] == '\u001A')
          this.WriteBreakLine(gr, this.e.MsgString[157], rect.left, y, width, (Pen) null);
        if (ptr[0] == '\u0012' && !this.e.TerArg.PageMode)
          this.WriteBreakLine(gr, this.e.MsgString[151], rect.left, y, width - this.TwipsToScrX(360), Pens.Black);
        if ((this.e.TextBorder & 65776 /*0x0100F0*/) != 0)
          this.DrawParaBorder(gr, rect, true);
      }
      else if (count == 0 || !this.edit.HiddenText(this.e.CurFmtId))
      {
        char minValue = char.MinValue;
        char[] chArray = this.CopyArray(ptr, 0);
        if (this.e.TerArg.WordWrap && !this.e.ShowParaMark)
        {
          while (count > 0 && ((int) chArray[count - 1] == (int) this.e.ParaChar || (int) chArray[count - 1] == (int) this.e.CellChar || chArray[count - 1] == '\u000F' || chArray[count - 1] == '\u0014'))
          {
            --count;
            ++num3;
          }
        }
        if ((this.e.TerFont[this.e.CurFmtId].style & 196608 /*0x030000*/) != 0)
        {
          for (int index = 0; index < count; ++index)
          {
            if (char.IsLower(chArray[index]))
              chArray[index] = char.ToUpper(chArray[index]);
          }
        }
        int[] textCharWidth = this.GetTextCharWidth(this.e.CurFmtId, chArray, count, pWidth);
        if (this.e.CharBoxLeft)
          textCharWidth[0] -= this.e.ExtraSpaceScrX;
        if (this.e.CharBoxRight && count > 0)
          textCharWidth[count - 1] -= this.e.ExtraSpaceScrX;
        if (this.e.ShowParaMark && count > 0 && ((int) chArray[count - 1] == (int) this.e.ParaChar || (int) chArray[count - 1] == (int) this.e.CellChar || chArray[count - 1] == '\u000F'))
        {
          minValue = chArray[count - 1];
          if ((int) chArray[count - 1] == (int) this.e.ParaChar)
            chArray[count - 1] = (this.e.TerFlags2 & 4194304 /*0x400000*/) != 0 ? '¿' : '¶';
          if (chArray[count - 1] == '\u000F')
            chArray[count - 1] = (this.e.TerFlags2 & 8388608 /*0x800000*/) != 0 ? '¯' : '«';
          if ((int) chArray[count - 1] == (int) this.e.CellChar)
            chArray[count - 1] = '¤';
        }
        if (this.e.SpaceBef > 0 || this.e.SpaceAft > 0)
        {
          COp.RECT rect1 = rect;
          if (this.e.CharBoxLeft)
            rect1.left += this.e.ExtraSpaceScrX;
          if (this.e.CharBoxRight)
            rect1.right -= this.e.ExtraSpaceScrX;
          COp.RECT rect2 = rect1;
          rect2.top += this.e.SpaceBef;
          rect2.bottom -= this.e.SpaceAft;
          this.OurExtTextOut(gr, x, y + this.e.ScrYOffset, options | num1, rect2, chArray, count, textCharWidth);
          if (this.e.BkPictId == 0)
          {
            int paraShading = this.e.ParaShading;
            Color paraBackColor = this.e.ParaBackColor;
            int bkMode = this.GetBkMode(gr);
            Color curForeColor = this.e.CurForeColor;
            Color curBackColor = this.e.CurBackColor;
            rect2 = rect1;
            rect2.top += this.e.ParaSpcBef;
            rect2.bottom = rect1.top + this.e.SpaceBef;
            this.SetColor(gr, 'T', 0);
            this.SetBkMode(gr, 2);
            this.OurExtTextOut(gr, x, y + this.e.ScrYOffset, options | num1, rect2, (char[]) null, 0, (int[]) null);
            rect2 = rect1 with
            {
              top = rect1.bottom - this.e.SpaceAft,
              bottom = rect1.bottom - this.e.ParaSpcAft
            };
            this.SetColor(gr, 'T', 0);
            this.SetBkMode(gr, 2);
            this.OurExtTextOut(gr, x, y + this.e.ScrYOffset, options | num1, rect2, (char[]) null, 0, (int[]) null);
            if (curForeColor != this.e.CurForeColor)
            {
              this.SetTextColor(gr, curForeColor);
              this.e.CurForeColor = curForeColor;
            }
            if (curBackColor != this.e.CurBackColor)
            {
              this.SetBkColor(gr, curBackColor);
              this.e.CurBackColor = curBackColor;
            }
            this.SetBkMode(gr, bkMode);
          }
        }
        else
        {
          COp.RECT rect3 = rect;
          if (this.e.CharBoxLeft)
            rect3.left += this.e.ExtraSpaceScrX;
          if (this.e.CharBoxRight)
            rect3.right -= this.e.ExtraSpaceScrX;
          this.OurExtTextOut(gr, x, y + this.e.ScrYOffset, options | num1, rect3, chArray, count, textCharWidth);
        }
        if (minValue != char.MinValue)
          chArray[count - 1] = minValue;
        if ((this.e.TextBorder & 1) != 0)
          this.DrawShadowLine(gr, rect.left, y + 1 + this.e.SpaceBef, rect.right, y + 1 + this.e.SpaceBef, tc.PagePen, (Pen) null);
        if ((this.e.TextBorder & 2) != 0)
          this.DrawShadowLine(gr, rect.left, rect.bottom - 1 - this.e.SpaceAft, rect.right, rect.bottom - 1 - this.e.SpaceAft, tc.PagePen, (Pen) null);
        Pen SolidPen1;
        if (count > 0 && ((style & 64 /*0x40*/) != 0 || (style & 2048 /*0x0800*/) != 0 && (this.e.TerOpFlags & 1024 /*0x0400*/) == 0) && (SolidPen1 = new Pen(this.GetTextColor(gr))) != null)
        {
          SolidPen1.DashStyle = DashStyle.Dot;
          ref tc.StrFont local = ref this.e.TerFont[this.e.CurFmtId];
          this.DrawShadowLine(gr, TextRect.left, y + BaseHeight, TextRect.right, y + BaseHeight, SolidPen1, (Pen) null);
          SolidPen1.Dispose();
        }
        bool flag3 = (style & 524288 /*0x080000*/) != 0 || (this.displayTextFontStyle & 524288 /*0x080000*/) != 0;
        bool flag4 = (style & 1) != 0;
        bool flag5 = (style & 256 /*0x0100*/) != 0;
        if ((flag5 | flag4 | flag3 && (style & 48 /*0x30*/) == 0 || (tempStyle & 1) != 0 | flag2) && count > 0)
        {
          DashStyle dashStyle = (tempStyle & 1) == 0 || !(flag4 | flag5) ? DashStyle.Solid : DashStyle.Dot;
          Color color = this.GetTextColor(gr);
          if (this.e.TerFont[this.e.CurFmtId].UlineColor != tc.CLR_AUTO)
            color = this.e.TerFont[this.e.CurFmtId].UlineColor;
          if (flag2 && this.e.CurBackColor != Color.Red)
            color = Color.Red;
          Pen SolidPen2;
          if ((SolidPen2 = new Pen(color)) != null)
          {
            int num4 = (style & 1) != 0 | flag2 ? 1 : 0;
            int num5 = 0;
            SolidPen2.DashStyle = dashStyle;
            if (num2 == 0)
            {
              num5 = TextRect.right - TextRect.left;
            }
            else
            {
              for (int index = 0; index < num2 - num3; ++index)
                num5 += pWidth != null ? (int) pWidth[index] : this.fnt.LwrCharWidth(this.e.CurFmtId, true, chArray[index]);
            }
            int num6 = BaseHeight != 0 ? BaseHeight + TextRect.top + this.e.SpaceBef + this.e.ScrYOffset : y + this.e.TerFont[this.e.CurFmtId].BaseHeight + this.e.ScrYOffset;
            if (flag2)
              this.DrawWigglyLine(gr, TextRect.left, num6 + 1, TextRect.left + num5, num6 + 1, color);
            else if (flag5)
            {
              this.DrawShadowLine(gr, TextRect.left, num6, TextRect.left + num5, num6, SolidPen2, (Pen) null);
              this.DrawShadowLine(gr, TextRect.left, num6 + 2, TextRect.left + num5, num6 + 2, SolidPen2, (Pen) null);
            }
            else if (flag4)
              this.DrawShadowLine(gr, TextRect.left, num6 + 1, TextRect.left + num5, num6 + 1, SolidPen2, (Pen) null);
            if (flag3)
            {
              int num7 = num6 - this.fnt.LwrCharWidth(this.e.CurFmtId, true, chArray[0]) / 2;
              this.DrawShadowLine(gr, TextRect.left, num7 - 1, TextRect.left + num5, num7 - 1, SolidPen2, (Pen) null);
              this.DrawShadowLine(gr, TextRect.left, num7 + 2, TextRect.left + num5, num7 + 2, SolidPen2, (Pen) null);
            }
            SolidPen2.Dispose();
          }
        }
        if ((this.e.TextBorder & 65776 /*0x0100F0*/) != 0)
          this.DrawParaBorder(gr, TextRect, true);
        Pen SolidPen3;
        if ((this.e.TerFont[this.e.CurFmtId].style & 8192 /*0x2000*/) != 0 && count > 0 && (SolidPen3 = new Pen(this.GetTextColor(gr))) != null)
        {
          int left = TextRect.left;
          int num8 = 0;
          int num9 = rect.top + 1;
          int num10 = rect.top + this.e.DispTextHt - 2;
          if (num2 == 0)
          {
            num8 = TextRect.right - TextRect.left;
          }
          else
          {
            for (int index = 0; index < count; ++index)
              num8 += pWidth != null ? (int) pWidth[index] : this.fnt.LwrCharWidth(this.e.CurFmtId, true, chArray[index]);
          }
          int num11 = left + num8;
          if (this.e.CharBoxLeft)
            ++left;
          if (this.e.CharBoxRight)
            --num11;
          if (num10 > rect.bottom - 2)
            num10 = rect.bottom - 2;
          ref tc.StrFont local = ref this.e.TerFont[this.e.CurFmtId];
          this.DrawShadowLine(gr, left, num9, num11, num9, SolidPen3, (Pen) null);
          this.DrawShadowLine(gr, left, num10, num11, num10, SolidPen3, (Pen) null);
          if (this.e.CharBoxLeft)
            this.DrawShadowLine(gr, left, num9, left, num10, SolidPen3, (Pen) null);
          if (this.e.CharBoxRight)
            this.DrawShadowLine(gr, num11, num9, num11, num10, SolidPen3, (Pen) null);
          SolidPen3.Dispose();
        }
      }
    }
    return true;
  }

  /// <summary>Нарисовать текст</summary>
  internal new bool DisplayText(
    Graphics gr,
    int col,
    int rowUnits,
    int height,
    int baseHeight,
    char[] ptr,
    int count,
    int line,
    bool toEOL,
    char area)
  {
    ushort index1 = 0;
    bool flag1 = (this.e.TerOpFlags & 1024 /*0x0400*/) != 0;
    this.e.OverhangFont = -1;
    int num1 = this.e.ScrFrameAngle <= 0 ? (!this.e.ScrRtl ? this.e.frame[this.e.CurFrame].ScrX + this.e.frame[this.e.CurFrame].ScrWidth : this.e.frame[this.e.CurFrame].x + this.e.frame[this.e.CurFrame].width) : this.e.frame[this.e.CurFrame].ScrX + this.e.frame[this.e.CurFrame].height;
    bool flag2 = this.e.ScrFrameAngle > 0;
    int num2;
    bool flag3;
    if (this.e.ScrRtl && this.e.pScrSeg != null)
    {
      this.e.CurScrSeg = this.GetCharSeg(line, col, this.e.TotalScrSeg, this.e.pScrSeg);
      num2 = this.e.pScrSeg[this.e.CurScrSeg].col + this.e.pScrSeg[this.e.CurScrSeg].count;
      flag3 = this.e.pScrSeg[this.e.CurScrSeg].rtl;
    }
    else
    {
      this.e.CurScrSeg = 0;
      flag3 = false;
      num2 = 9999;
    }
    COp.RECT rect;
    rect.top = rowUnits;
    rect.bottom = rect.top + height + this.e.SpaceBef + this.e.SpaceAft;
    int height1 = rect.bottom - rect.top;
    rowUnits += this.e.SpaceBef;
    int len = this.e.text[line].len;
    if (count > 0)
    {
      if (!flag1 && this.e.text[line].fmt == null && !this.e.ExpandLineFmt && this.e.text[line].tag == null)
      {
        ushort uniFmt = this.e.text[line].UniFmt;
        this.displayTextFontStyle = this.e.TerFont[(int) uniFmt].style;
        this.SetFont(gr, uniFmt, area);
        int baseHeightAdj = this.e.TerFont[(int) uniFmt].BaseHeightAdj;
        if (this.e.NextX < num1 | flag2)
        {
          int nextX;
          rect.left = nextX = this.e.NextX;
          for (int index2 = 0; index2 < count; ++index2)
            this.e.NextX += (int) this.e.pLineCharWidth[col + index2];
          if (toEOL && (this.displayTextFontStyle & 4) != 0 && (this.e.TerFont[(int) uniFmt].flags & 1) == 0)
            this.e.NextX += this.fnt.LwrCharWidth((int) uniFmt, true, 'W');
          rect.right = this.e.NextX <= num1 ? this.e.NextX : num1;
          if ((this.displayTextFontStyle & 128 /*0x80*/) != 0)
            this.TerPictOut(gr, nextX, rowUnits, rect, (int) uniFmt, count, line, baseHeight);
          else
            this.TerTextOut(gr, nextX, rowUnits + baseHeightAdj, rect, ptr, count, this.e.pLineCharWidth, baseHeight);
        }
      }
      else
      {
        ushort[] numArray;
        if (this.e.TempCfmt != null && this.e.text[line].fmt != null)
        {
          if (col > 0)
            index1 = this.e.TempCfmt[col - 1];
          numArray = this.CopyArray(this.e.TempCfmt, col);
        }
        else
        {
          index1 = this.e.text[line].UniFmt;
          numArray = new ushort[this.e.text[line].len - col];
        }
        ushort[] ar = this.CopyArray(this.e.pLineCharWidth, col);
        ushort index3 = numArray[0];
        this.displayTextFontStyle = this.e.TerFont[(int) index3].style;
        bool flag4 = (this.e.TerFont[(int) index3].flags & 1) != 0;
        int num3 = (this.e.TerFont[(int) index1].flags & 1) != 0 ? 1 : 0;
        int idx = 0;
        if (!flag1 && this.e.text[line].tabw != null && (this.e.text[line].tabw.type & 1024 /*0x0400*/) != 0 && this.e.text[line].tabw.FrameCharPos == col && col == 0)
          this.WriteFrameSpace(gr, line, rect.top, height1);
        int col1 = 1;
        if (num3 != 0 && !flag4 && col1 < count && ptr[col1] == ' ')
        {
          this.e.NextX += this.fnt.LwrCharWidth((int) index3, true, ' ');
          ++col1;
          ++idx;
          index3 = numArray[col1];
          this.displayTextFontStyle = this.e.TerFont[(int) index3].style;
        }
        bool flag5 = this.e.CheckTextTag(line, col, 77);
        for (; col1 <= count; ++col1)
        {
          bool flag6 = flag5;
          int num4 = flag1 || this.e.text[line].tabw == null || (this.e.text[line].tabw.type & 1024 /*0x0400*/) == 0 ? -1 : this.e.text[line].tabw.FrameCharPos;
          bool flag7 = !flag1 && this.e.text[line].tabw != null && this.e.text[line].tabw.CharFlagsLen > 0;
          char ch = col1 < count ? ptr[col1] : char.MinValue;
          if (idx != count - 1 || this.e.ShowParaMark || (int) ptr[idx] != (int) this.e.ParaChar)
          {
            flag5 = this.e.CheckTextTag(line, col1, 77);
            if (flag5 != flag6 || col1 == count || (int) numArray[col1] != (int) index3 || ch == '\t' || ch == '\u0006' || ch == '\u001C' || ptr[col1] == '\u0004' || ch == ' ' && this.e.ShowParaMark && (this.e.TerFlags5 & 32768 /*0x8000*/) == 0 || !this.e.TerArg.PageMode && ch == '\u0014' || num4 == col + col1 || col1 + col >= num2 || this.e.ScrRtl & flag3 && (ptr[col1 - 1] == ' ' || ch == ' ') || flag7 && col + col1 - 1 > 0 && ((int) this.e.text[line].tabw.CharFlags[col + col1 - 1] & 2) != 0)
            {
              int style = this.e.TerFont[(int) index3].style;
              try
              {
                if (flag6)
                  this.e.TerFont[(int) index3].style |= 512 /*0x0200*/;
                this.SetFont(gr, index3, area);
              }
              finally
              {
                this.e.TerFont[(int) index3].style = style;
              }
              int y1 = rowUnits + baseHeight - this.e.TerFont[(int) index3].BaseHeight + this.e.TerFont[(int) index3].BaseHeightAdj;
              if (ptr[idx] == '\t' && !this.edit.HiddenText((int) numArray[idx]))
              {
                this.WriteTab(gr, line, y1, rect.top, height1, baseHeight + this.e.SpaceBef, (int) ar[idx]);
                ++idx;
              }
              else if (this.e.ShowParaMark && ptr[idx] == ' ' && !this.edit.HiddenText((int) numArray[idx]) && (this.e.TerFlags5 & 32768 /*0x8000*/) == 0)
              {
                this.WriteSpace(gr, line, y1, rect.top, height1, baseHeight + this.e.SpaceBef, (int) ar[idx]);
                ++idx;
              }
              else if ((ptr[idx] == '\u0006' || ptr[idx] == '\u001C') && !this.e.ShowParaMark)
                ++idx;
              else if (ptr[idx] == '\u0004')
                ++idx;
              this.e.ShowPageBreak = idx < count && ptr[idx] == '\f' && this.e.CurLine == line;
              if (this.e.NextX < num1 | flag2 && col1 > idx)
              {
                int nextX;
                rect.left = nextX = this.e.NextX;
                for (int index4 = 0; index4 < col1 - idx; ++index4)
                  this.e.NextX += (int) ar[idx + index4];
                if (!flag1 && this.e.text[line].tabw != null && col + idx < this.e.text[line].tabw.CharFlagsLen && ((int) this.e.text[line].tabw.CharFlags[col + idx] & 1) != 0)
                {
                  nextX += this.e.ExtraSpaceScrX;
                  this.e.CharBoxLeft = true;
                }
                if (!flag1 && this.e.text[line].tabw != null && col + col1 - 1 < this.e.text[line].tabw.CharFlagsLen && ((int) this.e.text[line].tabw.CharFlags[col + col1 - 1] & 2) != 0)
                  this.e.CharBoxRight = true;
                if (toEOL && col1 == count && (this.displayTextFontStyle & 4) != 0 && !flag4)
                  this.e.NextX += this.fnt.LwrCharWidth((int) index3, true, 'W');
                rect.right = this.e.NextX <= num1 | flag2 ? this.e.NextX : num1;
                int y2 = rowUnits + baseHeight - this.e.TerFont[(int) index3].BaseHeight + this.e.TerFont[(int) index3].BaseHeightAdj;
                if ((this.displayTextFontStyle & 128 /*0x80*/) != 0)
                {
                  int scrYoffset = this.e.ScrYOffset;
                  ref tc.StrFont local1 = ref this.e.TerFont[(int) index3];
                  ref tc.StrFont local2 = ref this.e.TerFont[(int) index3];
                  this.TerPictOut(gr, nextX, y2 + this.e.ScrYOffset, rect, (int) index3, col1 - idx, line, baseHeight);
                }
                else
                {
                  if (this.e.TextOrientation == 90 || this.e.TextOrientation == 270 || this.e.TextOrientation == -90)
                  {
                    int num5 = 0;
                    if (this.e.VertAlignment != 0)
                    {
                      num5 = (int) this.e._grVisibleClipBounds.Width - this.e.CurTextHeight;
                      if (num5 < 0)
                        num5 = 0;
                      if (this.e.VertAlignment == 128 /*0x80*/)
                        num5 /= 2;
                    }
                    if (this.e.CaretPos == 0)
                      y2 += num5;
                  }
                  this.TerTextOut(gr, nextX, y2, rect, this.CopyArray(ptr, idx), col1 - idx, this.CopyArray(ar, idx), baseHeight);
                }
                this.e.CharBoxRight = false;
                this.e.CharBoxLeft = false;
                if (flag4 && col1 < count && ptr[col1] == ' ' && (this.e.TerFont[(int) numArray[col1]].flags & 1) == 0)
                {
                  this.e.NextX += this.fnt.LwrCharWidth((int) numArray[col1], true, ' ');
                  ++col1;
                }
              }
              if (num4 == col + col1)
                this.WriteFrameSpace(gr, line, rect.top, height1);
              if (col1 != count)
              {
                idx = col1;
                ushort index5 = index3;
                if (idx < count)
                  index3 = numArray[col1];
                this.displayTextFontStyle = this.e.TerFont[(int) index3].style;
                flag4 = (this.e.TerFont[(int) index3].flags & 1) != 0;
                ref tc.StrFont local = ref this.e.TerFont[(int) index5];
                if (this.e.ScrRtl && this.e.pScrSeg != null && col1 + col >= num2)
                {
                  ++this.e.CurScrSeg;
                  num2 = this.e.pScrSeg[this.e.CurScrSeg].col + this.e.pScrSeg[this.e.CurScrSeg].count;
                  flag3 = this.e.pScrSeg[this.e.CurScrSeg].rtl;
                }
              }
              else
                break;
            }
          }
          else
            break;
        }
      }
    }
    int nextX1 = this.e.NextX;
    if (toEOL && this.e.NextX <= num1 && this.e.ScrFrameAngle == 0)
    {
      int num6 = !this.e.ScrRtl || this.e.pScrSeg == null ? 0 : (this.e.CurScrSeg < this.e.TotalScrSeg ? 1 : 0);
      bool flag8 = false;
      if (this.e.OverhangFont >= 0)
        this.e.NextX += this.fnt.LwrCharWidth(this.e.OverhangFont, true, 'W');
      rect.left = this.e.NextX;
      rect.right = num1;
      if (rect.right == this.e.TerWinOrgX + this.e.TerWinWidth)
        ++rect.right;
      if (num6 != 0)
      {
        flag8 = this.e.pScrSeg[this.e.CurScrSeg].rtl;
        this.e.pScrSeg[this.e.CurScrSeg].rtl = true;
      }
      if (this.e.FullRenderMode && rect.right > rect.left && (this.e.CurBackColor != this.PageColor() || this.e.TextBorder != 0 || this.e.BkPictId > 0 || this.e.CurFrame > 0))
      {
        this.SetFont(gr, (ushort) 0, area);
        this.WriteSpaceRect(gr, rect);
      }
      if (num6 != 0)
        this.e.pScrSeg[this.e.CurScrSeg].rtl = flag8;
      this.e.NextX = num1;
    }
    return true;
  }

  /// <summary>Нарисовать текст</summary>
  internal new int OurExtTextOut(
    Graphics gr,
    int x,
    int y,
    int options,
    COp.RECT rect,
    char[] txt,
    int count,
    int[] pDX)
  {
    bool flag = this.e.ScrRtl && this.e.pScrSeg != null && this.e.CurScrSeg < this.e.TotalScrSeg;
    COp.RECT clip = rect;
    if (flag)
    {
      x = this.RtlX(x, clip.right - clip.left, this.e.CurFrame, this.e.pScrSeg[this.e.CurScrSeg]);
      this.RtlRect(ref clip, this.e.CurFrame, this.e.pScrSeg[this.e.CurScrSeg]);
    }
    if (tc.OSCanRotate || this.e.ScrFrameAngle == 0)
    {
      if ((flag ? (this.e.pScrSeg[this.e.CurScrSeg].rtl ? 1 : 0) : 0) != 0)
      {
        int num1 = 0;
        int[] numArray = new int[count + 1];
        if (count > 0)
        {
          for (int index = 0; index < count; ++index)
          {
            numArray[index] = pDX[count - index - 1];
            num1 += numArray[index];
          }
          int x1 = 2;
          COp.SIZE size;
          this.GetTextExtentPoint(gr, this.e.TerFont[this.e.CurFmtId].hFont, txt.ToString(), count, out size);
          if (this.e.InPrinting)
            x1 = this.ScrToUnitX(x1);
          if (size.cx > num1 + x1)
          {
            int num2 = (size.cx - num1) / count;
          }
          else if (size.cx < num1 - x1)
          {
            int num3 = (num1 - size.cx) / count;
          }
        }
        return this.ExtTextOut(gr, x, y, options | 128 /*0x80*/, clip, txt, count, (int[]) null);
      }
      if (this.e.TerFont[this.e.CurFmtId].rtl)
        options |= 128 /*0x80*/;
      return this.ExtTextOut(gr, x, y, options, clip, txt, count, pDX);
    }
    int x2 = this.FrameRotateX(x, y, this.e.CurFrame);
    int y1 = this.FrameRotateY(x, y, this.e.CurFrame);
    this.FrameRotateRect(ref clip, this.e.CurFrame);
    if (this.e.TerFont[this.e.CurFmtId].rtl)
      options |= 128 /*0x80*/;
    return this.ExtTextOut(gr, x2, y1, options, clip, txt, count, pDX);
  }

  /// <summary>Установить текущий цвет для отрисовки</summary>
  internal new bool SetColor(Graphics gr, char area, int font)
  {
    if (this.e.IsPlaneText)
      return true;
    bool flag1 = true;
    bool flag2 = false;
    if (this.e.CurFrame < 0)
      this.e.CurFrame = 0;
    if (this.e.CurFrame > 0)
    {
      int paraFrameId = this.e.frame[this.e.CurFrame].ParaFrameId;
      if (paraFrameId > 0 && paraFrameId < this.e.TotalParaFrames && (this.e.ParaFrame[paraFrameId].flags & 131200 /*0x020080*/) != 0)
        flag2 = this.e.ParaFrame[paraFrameId].FillPattern != 0;
    }
    Color clrWhite = tc.CLR_WHITE;
    int num1 = this.e.PageBkColor == tc.CLR_WHITE ? 1 : 0;
    Color color1 = num1 != 0 || this.IsSameColor(this.e.PageBkColor, tc.CLR_WHITE) ? this.e.TextDefBkColor : this.e.PageBkColor;
    Color color2 = this.e.TerFont[font].TextBkColor;
    if (color2 == tc.CLR_WHITE || this.IsSameColor(color2, tc.CLR_WHITE))
      color2 = this.e.ParaBackColor == tc.CLR_WHITE || this.IsSameColor(color2, tc.CLR_WHITE) ? color1 : this.e.ParaBackColor;
    if (area == 'P')
    {
      color2 = !(this.e.ParaBackColor != tc.CLR_WHITE) ? color1 : this.e.ParaBackColor;
      area = 'T';
    }
    Color color3 = this.e.FrameBkColor;
    if (color3 == tc.CLR_WHITE)
      color3 = color1;
    if ((this.e.FrameShading != 0 || color3 != color1 || (this.e.PaintFrameFlags & 1024 /*0x0400*/) != 0) && color2 == color1 && (area == 'T' || area == 'H') && this.e.ParaShading == 0 && this.e.ParaBackColor == tc.CLR_WHITE)
    {
      if (this.e.FrameShading == -1)
        color2 = color3;
      else if (color3 != color1 || (this.e.PaintFrameFlags & 1024 /*0x0400*/) != 0)
      {
        int frameShading = this.e.FrameShading > 0 ? this.e.FrameShading : 0;
        color2 = this.ToColor((int) (byte) ((int) color3.R * (100 - frameShading) / 100), (int) (byte) ((int) color3.G * (100 - frameShading) / 100), (int) (byte) ((int) color3.B * (100 - frameShading) / 100));
      }
      else
      {
        int num2 = this.e.FrameShading * (int) byte.MaxValue / 100;
        if (num2 > (int) byte.MaxValue)
          num2 = (int) byte.MaxValue;
        int num3 = ~num2 & (int) byte.MaxValue;
        color2 = this.ToColor(num3, num3, num3);
        if (this.e.FrameShading < 80 /*0x50*/)
          flag1 = false;
      }
    }
    if (this.e.FullCellHilight)
      area = 'H';
    Color color4 = this.e.TerFont[font].TextColor;
    if (this.e.TerFont[font].InsRev != 0)
      color4 = this.e.reviewer[this.e.TerFont[font].InsRev].InsColor;
    if (this.e.TerFont[font].DelRev != 0)
      color4 = this.e.reviewer[this.e.TerFont[font].DelRev].DelColor;
    if (num1 == 0)
      flag1 = false;
    if (((!(color4 == tc.CLR_AUTO) ? 0 : ((this.e.TerFlags3 & 536870912 /*0x20000000*/) == 0 ? 1 : 0)) & (flag1 ? 1 : 0)) != 0)
      color4 = this.ReverseColor(color2);
    if (color4 == tc.CLR_BLACK)
      color4 = this.e.TextDefColor;
    switch (area)
    {
      case 'H':
        color4 = (this.e.TerFont[font].style & 512 /*0x0200*/) == 0 || !this.e.ModifyProtectColor ? this.ReverseColor(color4) : this.ReverseColor(this.XOrColor(color4, tc.PROTECT_COLOR));
        color2 = this.ReverseColor(color2);
        break;
      case 'S':
        color4 = this.e.StatusColor;
        color2 = this.e.StatusBkColor;
        break;
      case 'T':
        if (this.e.TerArg.ReadOnly && (this.e.TerFlags3 & 16 /*0x10*/) != 0)
        {
          color4 = !(color4 == Color.Black) ? this.XOrColor(color4, tc.PROTECT_COLOR) : this.ToColor(128 /*0x80*/, 128 /*0x80*/, 128 /*0x80*/);
          break;
        }
        if ((this.e.TerFont[font].style & 512 /*0x0200*/) != 0 && this.e.ModifyProtectColor || this.e.TerArg.ReadOnly && (this.e.TerFlags3 & 16 /*0x10*/) != 0 || (this.e.TerOpFlags & 1024 /*0x0400*/) != 0 || (this.e.frame[this.e.CurFrame].flags & 16384 /*0x4000*/) != 0 && this.e.ModifyProtectColor || this.e.ViewPageHdrFtr && !this.e.EditPageHdrFtr && (this.e.PfmtId[this.e.CurPfmt].flags & 12288 /*0x3000*/) != 0 || this.e.CurSID >= 0)
        {
          color4 = this.XOrColor(color4, tc.PROTECT_COLOR);
          break;
        }
        break;
    }
    if (this.e.ParaShading != 0 && this.e.ParaBackColor == tc.CLR_WHITE)
    {
      int num4 = this.e.ParaShading * (int) byte.MaxValue / 10000;
      if (num4 > (int) byte.MaxValue)
        num4 = (int) byte.MaxValue;
      if (num4 >= 104 && num4 <= 152)
        num4 = 104;
      Color color5 = this.ToColor(num4, num4, num4);
      if ((!this.e.HtmlMode || (this.e.TerFont[font].style & this.e.LinkStyle) == 0) && color4 == this.e.TerFont[0].TextColor && this.e.ParaShading >= 80 /*0x50*/)
        color4 = this.XOrColor(color4, color5);
      color2 = !(color2 == color1) ? this.XOrColor(color2, color5) : this.XOrColor(Color.White, color5);
    }
    else if (this.e.ParaShading != 0)
    {
      byte num5 = (byte) ((int) byte.MaxValue * this.e.ParaShading / 10000);
      byte r = color2.R;
      byte g = color2.G;
      byte b = color2.B;
      color2 = this.ToColor((int) r < (int) num5 ? 0 : (int) (byte) ((uint) r - (uint) num5), (int) g < (int) num5 ? 0 : (int) (byte) ((uint) g - (uint) num5), (int) b < (int) num5 ? 0 : (int) (byte) ((uint) b - (uint) num5));
    }
    if ((this.e.ParaShading != 0 || this.e.ParaBackColor != tc.CLR_WHITE) && (color4 == tc.CLR_AUTO ? tc.CLR_BLACK : color4) == color2)
      color4 = this.ReverseColor(color2);
    if (this.e.TerFont[font].FieldId > 0 && this.IsDynField(this.e.TerFont[font].FieldId) || this.e.TerFont[font].FieldId == 6 || this.e.TerFont[font].FieldId == 7 && (this.e.TerFlags2 & 65536 /*0x010000*/) == 0 || this.e.TerFont[font].FieldId == 2 && (this.e.TerFlags2 & 65536 /*0x010000*/) == 0)
    {
      color4 = this.XOrColor(color4, this.ToColor(63 /*0x3F*/, 63 /*0x3F*/, 63 /*0x3F*/));
      color2 = this.XOrColor(color2, this.ToColor(63 /*0x3F*/, 63 /*0x3F*/, 63 /*0x3F*/));
    }
    this.SetTextColor(gr, color4);
    this.SetBkColor(gr, color2);
    this.e.CurForeColor = color4;
    this.e.CurBackColor = color2;
    this.e.transparent = this.e.EnableTransparent;
    if (color2 != color1)
      this.e.transparent = false;
    if (flag2)
      this.e.transparent = false;
    this.SetBkMode(gr, this.e.transparent ? 1 : 2);
    return true;
  }

  /// <summary>Установить текущий фонт для отрисовки</summary>
  internal new bool SetFont(Graphics gr, ushort fmt, char area)
  {
    if (area != 'N')
      this.SetColor(gr, area, (int) fmt);
    this.e.CurFmtId = (int) fmt;
    if ((this.e.TerFont[(int) fmt].style & 128 /*0x80*/) == 0)
    {
      if (this.e.InPrinting)
      {
        this.e.TerCurFont = this.e.PrtFont[(int) fmt].font;
        this.e.hTerCurFont = this.e.PrtFont[(int) fmt].hFont;
      }
      else
      {
        this.e.TerCurFont = this.e.TerFont[(int) fmt].font;
        this.e.hTerCurFont = this.e.TerFont[(int) fmt].hFont;
      }
      this.OurSetFont(gr, this.e.hTerCurFont);
    }
    return true;
  }

  internal new bool ActivateWheel(int lParam)
  {
    if (this.e.CaretEngaged)
      this.DisengageCaret();
    if (this.e.PictureClicked)
    {
      this.e.HilightType = 0;
      this.e.PictureClicked = false;
    }
    this.e.WheelShowing = true;
    this.e.WheelX = (int) (short) COp.LOWORD(lParam);
    this.e.WheelY = (int) (short) COp.HIWORD(lParam);
    this.e.WheelCur = tc.WheelFullCur;
    this.TerSetCursorShape(lParam, false);
    this.PaintTer();
    return true;
  }

  internal bool AddHlSeg(Graphics gr, int DestX, int DestY, int DestWidth, int DestHeight)
  {
    int totalHlSegs = this.e.TotalHlSegs;
    ++this.e.TotalHlSegs;
    if (this.e.TotalHlSegs > this.e.MaxHlSegs)
    {
      this.e.MaxHlSegs += 10;
      this.e.HlSeg = this.ReAlloc(this.e.HlSeg, this.e.MaxHlSegs);
    }
    if (this.e.ScrRtl && this.e.pScrSeg != null && this.e.CurScrSeg < this.e.TotalScrSeg)
    {
      int pVal1 = this.RtlX(DestX, 0, this.e.CurFrame, this.e.pScrSeg[this.e.CurScrSeg]);
      int pVal2 = this.RtlX(DestX + DestWidth, 0, this.e.CurFrame, this.e.pScrSeg[this.e.CurScrSeg]);
      if (pVal2 < pVal1)
        this.SwapInts(ref pVal1, ref pVal2);
      DestX = pVal1;
      DestWidth = pVal2 - pVal1;
    }
    if (this.e.ScrFrameAngle == 0)
    {
      this.e.HlSeg[totalHlSegs].x = DestX;
      this.e.HlSeg[totalHlSegs].y = DestY;
      this.e.HlSeg[totalHlSegs].width = DestWidth;
      this.e.HlSeg[totalHlSegs].height = DestHeight;
      return true;
    }
    COp.RECT OurRect;
    this.SetRect(out OurRect, DestX, DestY, DestX + DestWidth, DestY + DestHeight);
    this.FrameRotateRect(ref OurRect, this.e.CurFrame);
    int num1 = OurRect.left < OurRect.right ? OurRect.left : OurRect.right;
    int num2 = OurRect.top < OurRect.bottom ? OurRect.top : OurRect.bottom;
    this.e.HlSeg[totalHlSegs].x = num1;
    this.e.HlSeg[totalHlSegs].y = num2;
    this.e.HlSeg[totalHlSegs].width = DestHeight;
    this.e.HlSeg[totalHlSegs].height = DestWidth;
    return true;
  }

  internal bool AddPendPict(int CurX, int CurY, int pict, int count, int LineNo)
  {
    if (this.e.TotalPendPicts >= this.e.MaxPendPicts)
    {
      this.e.MaxPendPicts += 10;
      this.e.PendPict = this.ReAlloc(this.e.PendPict, this.e.MaxPendPicts);
    }
    int totalPendPicts = this.e.TotalPendPicts;
    int num = this.e.TerFont[pict].CharWidth[24];
    int height = this.e.TerFont[pict].height;
    if (this.e.ScrFrameAngle != 0)
    {
      COp.RECT OurRect = new COp.RECT();
      this.SetRect(out OurRect, CurX, CurY, CurX + num, CurY + height);
      this.FrameRotateRect(ref OurRect, this.e.CurFrame);
      CurX = OurRect.left < OurRect.right ? OurRect.left : OurRect.right;
      CurY = OurRect.top < OurRect.bottom ? OurRect.top : OurRect.bottom;
    }
    this.e.PendPict[totalPendPicts].x = CurX;
    this.e.PendPict[totalPendPicts].y = CurY;
    this.e.PendPict[totalPendPicts].width = num;
    this.e.PendPict[totalPendPicts].height = height;
    this.e.PendPict[totalPendPicts].pict = pict;
    this.e.PendPict[totalPendPicts].count = count;
    this.e.PendPict[totalPendPicts].LineNo = LineNo;
    ++this.e.TotalPendPicts;
    return true;
  }

  internal new bool AdjustHilight()
  {
    if (this.e.HilightType == 2 && !this.e.PictureClicked)
    {
      if (this.AdjustBlockForTable(true))
        this.PaintTer();
      if (this.e.HilightBegRow >= this.e.TotalLines || (this.e.PfmtId[this.e.text[this.e.HilightBegRow].pfmt].flags & 12288 /*0x3000*/) == 0 || this.AllSelected())
        return true;
      int index = this.e.HilightBegRow + 1;
      while (index < this.e.TotalLines && index <= this.e.HilightEndRow && (this.e.text[index].flags & 1966080 /*0x1E0000*/) == 0)
        ++index;
      if (index - 1 < this.e.HilightEndRow)
      {
        this.e.HilightEndRow = index - 1;
        this.e.HilightEndCol = this.e.text[this.e.HilightEndRow].len;
        this.PaintTer();
      }
    }
    return true;
  }

  internal bool ApplyNewGr(Graphics gr)
  {
    for (int index = 0; index < this.e.TotalFonts; ++index)
    {
      if (this.e.TerFont[index].InUse && this.e.TerFont[index].gr == this.e.TerGr)
      {
        this.e.TerFont[index].gr = gr;
        if (this.e.TerArg.PrintView)
          this.e.PrtFont[index].gr = gr;
      }
    }
    for (int index = 0; index < this.e.TotalOpGrs; ++index)
    {
      if (this.e.OpGr[index].gr == this.e.TerGr)
      {
        if (IntPtr.Zero != this.e.OpGr[index].hDC)
        {
          this.e.OpGr[index].gr.ReleaseHdc(this.e.OpGr[index].hDC);
          this.e.OpGr[index].hDC = IntPtr.Zero;
        }
        this.e.OpGr[index].gr = (Graphics) null;
      }
    }
    this.e._grVisibleClipBounds = gr.VisibleClipBounds;
    return true;
  }

  internal new bool BkPictOut(Graphics gr, int x, int y)
  {
    int width = this.e.TerFont[this.e.BkPictId].CharWidth[24];
    int height = this.e.TerFont[this.e.BkPictId].height;
    if (tc.DebugMode)
      this.misc.dm(nameof (BkPictOut));
    if (this.e.BkPictFlag == 1)
      this.PictOut(gr, x, y, width, height, this.e.BkPictId, 1, -1, false);
    else if (this.e.BkPictFlag == 2)
    {
      int num1 = x + this.e.TerWinWidth;
      int num2 = y + this.e.TerWinHeight;
      bool flag = (this.e.TerFlags & 65536 /*0x010000*/) != 0;
      this.TerSetClipRgn();
      for (int CurY = y; CurY < num2; CurY += height)
      {
        int CurX = x;
        while (CurX < num1)
        {
          this.PictOut(gr, CurX, CurY, width, height, this.e.BkPictId, 1, -1, false);
          CurX += width;
          this.e.TerFlags |= 65536 /*0x010000*/;
        }
      }
      if (!flag)
        this.e.TerSetFlags(false, 65536 /*0x010000*/);
      this.TerResetClipRgn();
    }
    else
    {
      this.PictOut(gr, x, y, width, height, this.e.BkPictId, 1, -1, false);
      if (gr == this.e.TerGr || gr == this.e.BufGr)
      {
        COp.RECT clip;
        if (width < this.e.TerWinWidth)
        {
          clip.left = x + width;
          clip.right = x + this.e.TerWinWidth;
          clip.top = y;
          clip.bottom = y + height;
          this.SetBkColor(gr, this.PageColor());
          this.ExtTextOut(gr, 0, 0, 2, clip, (char[]) null, 0, (int[]) null);
        }
        if (height < this.e.TerWinHeight)
        {
          clip.left = x;
          clip.right = x + this.e.TerWinWidth;
          clip.top = y + height;
          clip.bottom = y + this.e.TerWinHeight;
          this.SetBkColor(gr, this.PageColor());
          this.ExtTextOut(gr, 0, 0, 2, clip, (char[]) null, 0, (int[]) null);
        }
      }
    }
    return true;
  }

  internal void BlastToScreen()
  {
    IntPtr opDc = this.GetOpDC(this.e.TerGr);
    if (this.e.PaintFlag == 2 || this.e.PaintFlag == 3)
    {
      Rectangle rectangle = this.ToRectangle(this.e.DirtyRect);
      COp.Win32.BitBlt(opDc, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, this.e.hBufDC, rectangle.X, rectangle.Y, 13369376);
    }
    else if (this.e.PaintFlag != 1)
      COp.Win32.BitBlt(opDc, this.e.TerWinOrgX, this.e.TerWinOrgY, this.e.TerWinWidth, this.e.TerWinHeight, this.e.hBufDC, this.e.TerWinOrgX, this.e.TerWinOrgY, 13369376);
    this.ReleaseOpDC(this.e.TerGr);
  }

  internal new bool CalcBorderSpill(int x1, int y1, int x2, int y2)
  {
    if (x1 <= this.e.TerWinRect.left && x2 >= 0)
      this.e.BorderSpill |= 1;
    if (x1 <= this.e.TerRect.right && x2 >= this.e.TerWinRect.right)
      this.e.BorderSpill |= 2;
    if (y1 <= this.e.TerWinOrgY && y2 >= this.e.TerWinOrgY - this.e.TerWinRect.top)
      this.e.BorderSpill |= 4;
    if (y2 >= this.e.TerWinOrgY + this.e.TerWinHeight && y1 < this.e.TerWinOrgY + this.e.TerRect.bottom - this.e.TerWinRect.top)
      this.e.BorderSpill |= 8;
    if (this.e.BorderSpill != 0)
      this.e.BorderSpill |= 3;
    return true;
  }

  internal new bool ClearEOL(Graphics gr, int x, int y, int LastX, int height, bool ClearBknd)
  {
    if (!this.e.FullRenderMode)
      return true;
    int num = this.e.TerWinOrgY + this.e.TerWinHeight;
    if (tc.DebugMode)
      this.misc.dm(nameof (ClearEOL));
    if (x != LastX && height != 0)
    {
      COp.RECT rect;
      rect.left = x;
      if (rect.left < this.e.TerWinOrgX && y < num)
        rect.left = this.e.TerWinOrgX;
      rect.top = y;
      rect.right = LastX;
      if (rect.right == this.e.TerWinOrgX + this.e.TerWinWidth)
        ++rect.right;
      if (rect.right > this.e.TerWinOrgX + this.e.TerWinWidth + 1 && y < num)
        rect.right = this.e.TerWinOrgX + this.e.TerWinWidth + 1;
      rect.bottom = y + height;
      int curFmtId = this.e.CurFmtId;
      this.e.CurFmtId = 0;
      this.SetColor(gr, 'T', (int) (ushort) this.e.CurFmtId);
      if (ClearBknd)
        this.e.TerOpFlags |= 32 /*0x20*/;
      this.e.TerOpFlags |= 536870912 /*0x20000000*/;
      if (y > this.e.TerWinOrgY + this.e.TerWinHeight)
        this.TerTextOut(gr, x, y, rect, (char[]) null, 0, (ushort[]) null, 0);
      else
        this.WriteSpaceRect(gr, rect);
      this.e.TerOpFlags &= -536870913 /*0xDFFFFFFF*/;
      if (ClearBknd)
        this.e.TerOpFlags &= -33;
      this.e.CurFmtId = curFmtId;
    }
    return true;
  }

  internal new bool ClearScrForXparentWrite(Graphics gr)
  {
    if ((this.e.TerFlags2 & 32 /*0x20*/) != 0 && (this.e.PaintFlag == 4 || this.e.PaintFlag == 6))
      this.e.CurFrame = 0;
    return true;
  }

  internal bool CopyControls()
  {
    for (int index = 0; index < this.e.TotalScrCtls; ++index)
    {
      if (this.e.ScrCtl[index].InUse && this.e.ScrCtl[index].ctl.Visible)
      {
        COp.RECT terRect = this.e.TerRect;
        if (this.e.TerTlb != null)
          terRect.top = this.e.ToolBarHeight;
        COp.Win32.ValidateRect(this.e.hTerWnd, ref terRect);
        this.TerSetClipRgn();
        this.CopyCtlImage(this.e.ScrCtl[index].pict);
      }
    }
    return true;
  }

  internal new bool CopyCtlImage(int pict)
  {
    int num1 = 0;
    int num2 = this.e.TotalFonts - 1;
    if (this.e.BufGr != null)
    {
      if (pict >= 0)
        num1 = num2 = pict;
      for (int index = num1; index <= num2; ++index)
      {
        if (this.e.TerFont[index].InUse && (this.e.TerFont[index].style & 128 /*0x80*/) != 0 && (this.e.TerFont[index].PictType == 2 || this.e.TerFont[index].PictType == 6) && this.e.TerFont[index].ctl != null && this.e.TerFont[index].ctl.Visible)
        {
          int num3 = this.e.TerFont[index].PictX + this.e.TerWinOrgX;
          int num4 = this.e.TerFont[index].PictY + this.e.TerWinOrgY;
          int DestWidth = this.e.TerFont[index].CharWidth[24];
          int height = this.e.TerFont[index].height;
          if (!this.e.UseWin)
          {
            IntPtr opDc = this.GetOpDC(this.e.TerGr);
            COp.Win32.BitBlt(this.e.hBufDC, num3, num4, DestWidth, height, opDc, num3, num4, 13369376);
            this.ReleaseOpDC(this.e.TerGr);
          }
        }
      }
    }
    return true;
  }

  internal new bool DeleteTextMap(bool RedrawBackground)
  {
    if (tc.DebugMode)
      this.misc.dm(nameof (DeleteTextMap));
    if (RedrawBackground && this.e.BkPictId > 0)
    {
      if (this.e.BkPictBM != null)
      {
        if (this.e.BufGr == null)
        {
          this.e.TerGr.DrawImage((Image) this.e.BkPictBM, new Rectangle(this.e.TerWinOrgX, this.e.TerWinOrgY, this.e.TerWinWidth, this.e.TerWinHeight), new Rectangle(0, 0, this.e.TerWinWidth, this.e.TerWinHeight), GraphicsUnit.Pixel);
          if (this.e.CaretEnabled && !this.e.CaretHidden)
            this.ShowCaret(this.e.hTerWnd);
        }
      }
      else if (this.e.TerWinWidth > 0 && this.e.TerWinHeight > 0)
      {
        this.PaintBkPict(this.e.BufGr != null ? this.e.BufGr : this.e.TerGr);
        if ((this.e.BkPictBM = new Bitmap(this.e.TerWinWidth, this.e.TerWinHeight, this.e.TerGr)) != null)
        {
          Graphics graphics = Graphics.FromImage((Image) this.e.BkPictBM);
          if (this.e.BufBM != null)
            graphics.DrawImage((Image) this.e.BufBM, new Rectangle(0, 0, this.e.TerWinWidth, this.e.TerWinHeight), new Rectangle(this.e.TerWinOrgX, this.e.TerWinOrgY, this.e.TerWinWidth, this.e.TerWinHeight), GraphicsUnit.Pixel);
          this.e.BkPictGr = graphics;
          this.e.hBkPictDC = this.e.BkPictGr.GetHdc();
          this.e.hBkPictBM = this.e.BkPictBM.GetHbitmap();
          this.e.hPrevBkPictBM = this.SelectObject(this.e.hBkPictDC, this.e.hBkPictBM);
          if (this.e.CaretEnabled && !this.e.CaretHidden)
            this.ShowCaret(this.e.hTerWnd);
        }
      }
    }
    this.e.PaintFlag = 4;
    return true;
  }

  internal bool DeselectTerText(bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    this.e.HilightType = 0;
    if (repaint)
      this.PaintTer();
    return true;
  }

  internal new bool DisplayStatus()
  {
    bool inPrinting = this.e.InPrinting;
    if (!this.e.InPrintPreview)
    {
      if (!this.e.TerArg.ShowStatus)
        return this.e.SendMessageToParent(2730, (int) this.e.hTerWnd, 0, false) != 0;
      this.e.InPrinting = false;
      this.TerResetClipRgn();
      if (this.e.FontsReleased)
        this.RecreateFonts(this.e.TerGr);
      Pen hSolidPen = new Pen(this.ToColor(8355711 /*0x7F7F7F*/), (float) this.e.StatusLineWidth);
      Pen pen = new Pen(this.ToColor(15724527 /*0xEFEFEF*/), (float) this.e.StatusLineWidth);
      this.e.TerCurFont = this.e.StatusFont;
      Color color1 = this.SetTextColor(this.e.TerGr, this.e.StatusColor);
      Color color2 = this.SetBkColor(this.e.TerGr, this.e.StatusBkColor);
      int units;
      int num1 = units = this.LineToUnits(-1);
      int x1_1 = this.e.TerWinOrgX - (this.e.TerWinRect.left - this.e.TerRect.left);
      int x2_1 = x1_1 + this.e.TerRect.right - this.e.TerRect.left;
      COp.RECT clip;
      clip.left = x1_1;
      clip.right = x2_1;
      clip.top = num1;
      clip.bottom = clip.top + this.e.StatusFontHeight + 6 * this.e.StatusLineWidth;
      this.ExtTextOut(this.e.TerGr, 0, 0, 2, clip, (char[]) null, 0, (int[]) null);
      int num2 = num1 + this.e.StatusFontHeight + 6 * this.e.StatusLineWidth - 1;
      this.DrawShadowLine(this.e.TerGr, x1_1, num2, x2_1, num2, pen, (Pen) null);
      int y = num1 + 3 * this.e.StatusLineWidth;
      string str1 = !this.e.InsertMode ? this.e.MsgString[207] : this.e.MsgString[206];
      int num3 = this.e.TerWinOrgX + 2;
      this.OurSetFont(this.e.TerGr, this.e.hStatusFont);
      this.TextOut(this.e.TerGr, num3 + this.e.StatusLineWidth, y, str1, 3);
      int num4 = this.TerTextExtentX(this.e.TerGr, this.e.MsgString[206], this.e.MsgString[206].Length);
      int num5 = this.TerTextExtentX(this.e.TerGr, this.e.MsgString[207], this.e.MsgString[207].Length);
      if (num5 > num4)
        num4 = num5;
      int num6 = 2 * this.e.StatusLineWidth;
      int y1 = units + num6;
      int y2 = y1 + this.e.StatusFontHeight + this.e.StatusLineWidth;
      int x1_2 = num3;
      int x2_2 = num3 + num4 + 2 * this.e.StatusLineWidth;
      this.DrawShadowBox(this.e.TerGr, x1_2, y1, x2_2, y2, hSolidPen, pen);
      string str2;
      if (inPrinting)
        str2 = this.e.MsgString[218] + "0000";
      else if (this.e.TerArg.FittedView)
        str2 = $"{this.e.MsgString[209]}00000 {this.e.MsgString[211]}0000";
      else
        str2 = $"{this.e.MsgString[210]}0000 {this.e.MsgString[209]}00000 {this.e.MsgString[211]}0000";
      int x1 = Math.Max((this.e.TerWinRect.right - this.e.TerWinRect.left - this.TerTextExtentX(this.e.TerGr, str2, str2.Length)) / 2, x2_2 + 10) + this.e.TerWinOrgX;
      int x1_3 = x1 - 4;
      int x2;
      if (inPrinting)
      {
        string str3 = this.e.MsgString[218];
        this.TextOut(this.e.TerGr, x1, y, str3, str3.Length);
        int x3 = x1 + this.TerTextExtentX(this.e.TerGr, str3, str3.Length);
        this.e.StatusRowPos = x3 - this.e.TerWinOrgX;
        this.e.TempString = (this.e.PrtCurPage + 1).ToString();
        if (this.e.PrtCurPage < 0)
          this.e.TempString = "";
        for (int length = this.e.TempString.Length; length < 4; ++length)
          this.e.TempString += " ";
        this.TextOut(this.e.TerGr, x3, y, this.e.TempString, this.e.TempString.Length);
        x2 = x3 + this.TerTextExtentX(this.e.TerGr, "0000 ", 5);
      }
      else
      {
        if (!this.e.TerArg.FittedView)
        {
          string str4 = !this.e.TerArg.WordWrap || !this.e.TerArg.PrintView ? this.e.MsgString[210] : this.e.MsgString[208 /*0xD0*/];
          this.TextOut(this.e.TerGr, x1, y, str4, str4.Length);
          int x4 = x1 + this.TerTextExtentX(this.e.TerGr, str4, str4.Length);
          this.e.StatusRowPos = x4 - this.e.TerWinOrgX;
          if (this.e.TerArg.WordWrap && this.e.TerArg.PrintView)
          {
            if ((this.e.TerFlags2 & 128 /*0x80*/) != 0)
              this.e.TempString = this.e.PageInfo[this.e.CurPage].DispNbr.ToString();
            else
              this.e.TempString = (this.e.CurPage + 1).ToString();
          }
          else
            this.e.TempString = (this.e.CurRow + 1).ToString();
          for (int length = this.e.TempString.Length; length < 4; ++length)
            this.e.TempString += " ";
          this.TextOut(this.e.TerGr, x4, y, this.e.TempString, this.e.TempString.Length);
          x1 = x4 + this.TerTextExtentX(this.e.TerGr, "0000 ", 5);
        }
        string str5 = this.e.MsgString[209];
        this.TextOut(this.e.TerGr, x1, y, str5, str5.Length);
        int x5 = x1 + this.TerTextExtentX(this.e.TerGr, str5, str5.Length);
        this.e.StatusLinePos = x5 - this.e.TerWinOrgX;
        this.e.TempString = this.GetStatusLineNo().ToString();
        for (int length = this.e.TempString.Length; length < 5; ++length)
          this.e.TempString += " ";
        this.TextOut(this.e.TerGr, x5, y, this.e.TempString, this.e.TempString.Length);
        int x6 = x5 + this.TerTextExtentX(this.e.TerGr, "00000 ", 6);
        string str6 = this.e.MsgString[211];
        this.TextOut(this.e.TerGr, x6, y, str6, str6.Length);
        x2 = x6 + this.TerTextExtentX(this.e.TerGr, str6, str6.Length);
        this.e.StatusColPos = x2 - this.e.TerWinOrgX;
        this.e.TempString = (this.e.CurCol + 1).ToString();
        for (int length = this.e.TempString.Length; length < 4; ++length)
          this.e.TempString += " ";
        this.TextOut(this.e.TerGr, x2, y, this.e.TempString, this.e.TempString.Length);
      }
      int x2_3 = x2 + this.TerTextExtentX(this.e.TerGr, "00000", 5) + 1;
      this.DrawShadowBox(this.e.TerGr, x1_3, y1, x2_3, y2, hSolidPen, pen);
      this.SetTextColor(this.e.TerGr, color1);
      this.SetBkColor(this.e.TerGr, color2);
      this.TerResetClipRgn();
      this.e.InPrinting = inPrinting;
      hSolidPen.Dispose();
      pen.Dispose();
    }
    return true;
  }

  internal new bool DisplayStatusInfo()
  {
    int[] dx = new int[5];
    bool inPrinting = this.e.InPrinting;
    if (!this.e.TerArg.ShowStatus)
      return this.e.SendMessageToParent(2730, (int) this.e.hTerWnd, 0, false) != 0;
    this.e.InPrinting = false;
    this.e.draw.SetTerGraphicsClip(new Rectangle(this.e.TerWinRect.left, this.e.TerWinRect.top, this.e.TerWinRect.right - 1 - this.e.TerWinRect.left, this.e.TerRect.bottom - this.e.TerWinRect.top));
    this.e.TerCurFont = this.e.StatusFont;
    Color color1 = this.SetTextColor(this.e.TerGr, this.e.StatusColor);
    Color color2 = this.SetBkColor(this.e.TerGr, this.e.StatusBkColor);
    int mode = this.SetBkMode(this.e.TerGr, 2);
    this.e.TempString = "0123456789";
    COp.SIZE size;
    this.GetTextExtentPoint(this.e.TerGr, this.e.TerCurFont, this.e.TempString, this.e.TempString.Length, out size);
    for (int index = 0; index < 5; ++index)
      dx[index] = size.cx / 10;
    int y = this.LineToUnits(-1) + 3 * this.e.StatusLineWidth;
    COp.RECT clip;
    clip.top = y;
    clip.bottom = y + size.cy;
    if (inPrinting)
    {
      this.e.TempString = (this.e.PrtCurPage + 1).ToString();
      for (int length = this.e.TempString.Length; length < 4; ++length)
        this.e.TempString += " ";
      clip.left = this.e.TerWinOrgX + this.e.StatusRowPos;
      clip.right = clip.left + 4 * dx[0];
      this.ExtTextOut(this.e.TerGr, this.e.TerWinOrgX + this.e.StatusRowPos, y, 2, clip, this.e.TempString.ToCharArray(), 4, dx);
    }
    else
    {
      if (!this.e.TerArg.FittedView)
      {
        if (this.e.TerArg.WordWrap && this.e.TerArg.PrintView)
        {
          if ((this.e.TerFlags2 & 128 /*0x80*/) != 0)
            this.e.TempString = this.e.PageInfo[this.e.CurPage].DispNbr.ToString();
          else
            this.e.TempString = (this.e.CurPage + 1).ToString();
        }
        else
          this.e.TempString = (this.e.CurRow + 1).ToString();
        for (int length = this.e.TempString.Length; length < 4; ++length)
          this.e.TempString += " ";
        clip.left = this.e.TerWinOrgX + this.e.StatusRowPos;
        clip.right = clip.left + 4 * dx[0];
        this.ExtTextOut(this.e.TerGr, this.e.TerWinOrgX + this.e.StatusRowPos, y, 2, clip, this.e.TempString.ToCharArray(), 4, dx);
      }
      this.e.TempString = this.GetStatusLineNo().ToString();
      for (int length = this.e.TempString.Length; length < 5; ++length)
        this.e.TempString += " ";
      clip.left = this.e.TerWinOrgX + this.e.StatusLinePos;
      clip.right = clip.left + 5 * dx[0];
      this.ExtTextOut(this.e.TerGr, this.e.TerWinOrgX + this.e.StatusLinePos, y, 2, clip, this.e.TempString.ToCharArray(), 5, dx);
      this.e.TempString = (this.e.CurCol + 1).ToString();
      for (int length = this.e.TempString.Length; length < 4; ++length)
        this.e.TempString += " ";
      clip.left = this.e.TerWinOrgX + this.e.StatusColPos;
      clip.right = clip.left + 4 * dx[0];
      this.ExtTextOut(this.e.TerGr, this.e.TerWinOrgX + this.e.StatusColPos, y, 2, clip, this.e.TempString.ToCharArray(), 4, dx);
    }
    this.SetTextColor(this.e.TerGr, color1);
    this.SetBkColor(this.e.TerGr, color2);
    this.SetBkMode(this.e.TerGr, mode);
    this.TerResetClipRgn();
    this.e.InPrinting = inPrinting;
    return true;
  }

  internal new bool DrawAnimPict(int TimerId)
  {
    int pict = TimerId - 9199;
    if (!this.e.InDialogBox && !this.e.InPrinting)
    {
      this.KillTimer(this.e.hTerWnd, TimerId);
      if (pict < 0 || pict >= this.e.TotalFonts || !this.e.TerFont[pict].InUse || (this.e.TerFont[pict].style & 128 /*0x80*/) == 0 || this.e.TerFont[pict].anim == null)
        return false;
      this.e.TerFont[pict].anim.TimerId = 0;
      if ((this.e.TerFont[pict].flags & 64 /*0x40*/) == 0)
        return true;
      int nextAnim = this.e.TerFont[pict].anim.NextAnim;
      if (!this.e.TerFont[nextAnim].InUse || (this.e.TerFont[nextAnim].style & 128 /*0x80*/) == 0)
        return false;
      this.TerSetClipRgn();
      this.e.TerFont[nextAnim].CharWidth[24] = this.e.TerFont[pict].CharWidth[24];
      this.e.TerFont[nextAnim].height = this.e.TerFont[pict].height;
      if (this.e.TerFont[pict].FrameType != 0)
        this.SetPictSize(nextAnim, this.TwipsToScrY(this.e.TerFont[nextAnim].PictHeight), this.TwipsToScrX(this.e.TerFont[nextAnim].PictWidth), false);
      this.PictOut(this.e.TerGr, this.e.TerFont[pict].PictX + this.e.TerWinOrgX, this.e.TerFont[pict].PictY + this.e.TerWinOrgY, -1, -1, nextAnim, 1, -1, true);
      if (this.e.TerFont[pict].FrameType != 0)
        this.SetPictSize(nextAnim, 0, 0, true);
      this.TerResetClipRgn();
      this.e.TerFont[pict].anim.CurAnim = nextAnim;
      this.SetAnimTimer(pict);
    }
    return true;
  }

  internal new bool DrawBkPictRect(COp.RECT rect, Graphics gr)
  {
    if (this.e.CaretEnabled && !this.e.CaretHidden)
      this.HideCaret(this.e.hTerWnd);
    if (this.e.BkPictBM != null)
    {
      if (this.e.ScrRtl && this.e.pScrSeg != null && this.e.CurScrSeg < this.e.TotalScrSeg)
        this.RtlRect(ref rect, this.e.CurFrame, this.e.pScrSeg[this.e.CurScrSeg]);
      this.BitBlt(gr, rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top, this.e.hBkPictDC, rect.left - this.e.TerWinOrgX, rect.top - this.e.TerWinOrgY, 13369376);
    }
    if (this.e.CaretEnabled && !this.e.CaretHidden)
      this.ShowCaret(this.e.hTerWnd);
    return true;
  }

  internal new bool DrawBullet(
    Graphics gr,
    int LineNo,
    int CurPara,
    int x,
    int y,
    int BaseHeight,
    bool DoDraw)
  {
    int num1 = 0;
    int mode = 2;
    int styId = this.e.PfmtId[CurPara].StyId;
    string str1 = "HEADING";
    bool flag1 = false;
    int index1 = -1;
    if (tc.DebugMode)
      this.misc.dm(nameof (DrawBullet));
    if (!this.IsHiddenLine(LineNo) && !this.LineInfo(LineNo, 32 /*0x20*/))
    {
      string name = this.e.StyleId[styId].name;
      name.ToUpper();
      bool flag2 = name.IndexOf(str1) == 0;
      int bltId1 = this.e.PfmtId[CurPara].BltId;
      bool flag3;
      int options = !(flag3 = this.IsDefLangRtl()) ? 0 : 128 /*0x80*/;
      int line = LineNo;
      while (line < this.e.TotalLines - 1 && (this.e.text[line].flags & 1) == 0)
        ++line;
      int index2 = this.e.text[line].len != 0 ? (index1 = this.GetCurCfmt(line, this.e.text[line].len - 1)) : 0;
      if (this.e.TerBlt[bltId1].ls > 0)
      {
        if ((this.e.text[LineNo].flags & 33554432 /*0x02000000*/) == 0)
          return true;
        tc.StrListLevel pLevel;
        if (this.GetListLevelPtr(this.e.TerBlt[bltId1].ls, this.e.TerBlt[bltId1].lvl, out pLevel))
        {
          flag1 = pLevel.NumType == 1 || pLevel.NumType == 2;
          if (flag1 && pLevel.CharAft != 0)
            flag1 = false;
          if (flag1 && pLevel.text != null && pLevel.text.Length != 0)
          {
            for (int index3 = 0; index3 < (int) pLevel.text[0]; ++index3)
            {
              int num2 = (int) pLevel.text[index3 + 1];
              if (num2 < 9 && num2 != this.e.TerBlt[bltId1].lvl)
              {
                flag1 = false;
                break;
              }
            }
          }
        }
      }
      if (this.e.TerBlt[bltId1].IsBullet)
      {
        tc.StrListLevel pLevel;
        if (this.GetListLevelPtr(this.e.TerBlt[bltId1].ls, this.e.TerBlt[bltId1].lvl, out pLevel))
        {
          index2 = pLevel.FontId;
          if (this.IsSameColor(this.e.TerFont[index2].TextColor, tc.CLR_AUTO) && index1 >= 0 && !this.IsSameColor(this.e.TerFont[index1].TextColor, tc.CLR_AUTO))
            index2 = (int) this.GetNewColor((ushort) index2, this.ToColorRef(this.e.TerFont[index1].TextColor), 0, "", LineNo, 0);
        }
        else
        {
          string NewTypeFace = this.e.TerBlt[bltId1].font != 1 ? (this.e.TerBlt[bltId1].font != 2 ? this.e.TerFont[0].TypeFace : "Wingdings") : "Symbol";
          int NewTwipsSize = this.e.TerFont[index2].TwipsSize;
          if (this.e.TerBlt[bltId1].BulletChar == '¨')
            NewTwipsSize = !this.e.InPrinting ? NewTwipsSize * 3 / 4 : NewTwipsSize * 4 / 5;
          index2 = this.GetNewFont(gr, index2, NewTypeFace, NewTwipsSize, 0, this.e.TerFont[index2].TextColor, tc.CLR_WHITE, this.e.TerFont[index2].UlineColor, 0, 0, 0, 1, this.e.PfmtId[CurPara].StyId, 0, 0, 0, (string) null, 0, (byte) 1, 0, this.e.TerFont[index2].TextAngle);
        }
      }
      else if ((this.e.text[LineNo].flags & 33554432 /*0x02000000*/) != 0)
      {
        if (this.e.text[LineNo].tabw != null)
          index2 = this.e.text[LineNo].tabw.ListFontId;
      }
      else
      {
        if (this.e.HtmlMode && this.e.TerFont[index2].style != 0 || this.e.TerFont[index2].FieldId > 0)
          index2 = this.GetNewFont(gr, index2, this.e.TerFont[index2].TypeFace, this.e.TerFont[index2].TwipsSize, 0, this.e.TerFont[index2].TextColor, tc.CLR_WHITE, this.e.TerFont[index2].UlineColor, 0, 0, 0, 1, this.e.PfmtId[CurPara].StyId, 0, 0, 0, (string) null, 0, (byte) 1, 0, this.e.TerFont[index2].TextAngle);
        if (flag3 && !this.e.TerFont[index2].rtl)
          index2 = this.SetCurLangFont2(index2, this.e.DefInpLang);
      }
      this.SetFont(gr, (ushort) index2, 'T');
      if (this.e.TerFont[index2].BaseHeight > 0)
        y = !this.e.InPrinting ? y + BaseHeight - this.e.TerFont[index2].BaseHeight + this.e.TerFont[index2].BaseHeightAdj : y + BaseHeight - this.e.PrtFont[index2].BaseHeight + this.e.PrtFont[index2].BaseHeightAdj;
      string str2;
      if (this.e.TerBlt[bltId1].IsBullet)
        str2 = this.e.TerBlt[bltId1].BulletChar.ToString();
      else if ((this.e.text[LineNo].flags & 33554432 /*0x02000000*/) != 0)
      {
        str2 = "";
        if (this.e.text[LineNo].tabw != null && this.e.text[LineNo].tabw.ListText != null)
          str2 = this.e.text[LineNo].tabw.ListText;
      }
      else
      {
        int num3 = 0;
        int index4 = LineNo;
        int LineNo1;
        for (LineNo1 = LineNo - 1; LineNo1 >= 0; --LineNo1)
        {
          int bltId2 = this.e.PfmtId[this.e.text[LineNo1].pfmt].BltId;
          if (!this.LineInfo(LineNo1, 32 /*0x20*/))
          {
            if (this.e.HtmlMode)
            {
              int level = this.e.TerBlt[bltId2].level;
              if ((this.e.PfmtId[this.e.text[LineNo1 + 1].pfmt].SpaceBefore <= 0 || (this.e.text[LineNo1 + 1].flags & 4) == 0) && level >= this.e.TerBlt[bltId1].level && (this.e.TerBlt[bltId2].IsBullet || this.e.TerBlt[bltId2].start == this.e.TerBlt[bltId1].start))
              {
                if (bltId2 != 0 && this.e.TerBlt[bltId2].level <= this.e.TerBlt[bltId1].level)
                {
                  if (!this.LineInfo(LineNo1, 16 /*0x10*/) && !this.e.TerBlt[bltId1].IsBullet)
                  {
                    if ((this.e.text[LineNo1].flags & 4) != 0)
                      ++num3;
                  }
                  else
                    break;
                }
              }
              else
                break;
            }
            else if (bltId2 != 0)
            {
              if ((this.e.TerBlt[bltId2].flags & 1) == 0 && !this.e.TerBlt[bltId2].IsBullet && this.e.TerBlt[bltId2].start == this.e.TerBlt[bltId1].start && (this.e.text[LineNo1].flags & 2) == 0)
              {
                if (this.e.TerBlt[bltId2].level >= this.e.TerBlt[bltId1].level)
                {
                  if (this.e.TerBlt[bltId2].level == this.e.TerBlt[bltId1].level)
                  {
                    if ((int) this.e.TerBlt[bltId2].AftChar == (int) this.e.TerBlt[bltId1].AftChar && this.strcmpi(this.e.TerBlt[bltId2].BefText, this.e.TerBlt[bltId1].BefText) == 0 && this.e.TerBlt[bltId2].NumberType == this.e.TerBlt[bltId1].NumberType)
                    {
                      if ((this.e.text[LineNo1].flags & 4) != 0)
                        ++num3;
                      index4 = LineNo1;
                    }
                    else
                      break;
                  }
                }
                else
                  break;
              }
            }
            else
              break;
          }
        }
        int num4 = LineNo1 + 1;
        int bltId3 = this.e.PfmtId[this.e.text[index4].pfmt].BltId;
        int num5 = num3 + this.e.TerBlt[bltId3].start;
        str2 = "";
        if (this.e.TerBlt[bltId1].NumberType == 1)
          str2 = this.AlphaFormat(num5, true);
        else if (this.e.TerBlt[bltId1].NumberType == 2)
          str2 = this.AlphaFormat(num5, false);
        else if (this.e.TerBlt[bltId1].NumberType == 3)
          str2 = this.romanize(num5, true);
        else if (this.e.TerBlt[bltId1].NumberType == 4)
          str2 = this.romanize(num5, false);
        else if (this.e.TerBlt[bltId1].NumberType == 0)
          str2 = num5.ToString();
        else if (this.e.TerBlt[bltId1].NumberType == 5 && !flag2)
          str2 = $"{(ValueType) (char) (65 + num5 - 1)}";
        flag1 = this.e.TerBlt[bltId1].NumberType == 3 || this.e.TerBlt[bltId1].NumberType == 4;
        if (!this.e.HtmlMode)
        {
          if (this.e.TerBlt[bltId3].BefText.Length > 0)
          {
            string str3 = str2;
            str2 = this.e.TerBlt[bltId3].BefText + str3;
          }
          if (this.e.TerBlt[bltId3].AftChar != char.MinValue)
            str2 += this.e.TerBlt[bltId3].AftChar.ToString();
        }
        if (this.e.HtmlMode || this.e.TerBlt[bltId3].BefText.Length == 0 && this.e.TerBlt[bltId3].AftChar == char.MinValue)
          str2 += ".";
      }
      if (this.LineInfo(LineNo, 12) && this.e.text[LineNo].len == 1)
        str2 = "";
      int length = str2.Length;
      for (int index5 = 0; index5 < length; ++index5)
        num1 += this.fnt.LwrCharWidth(index2, true, str2[index5]);
      if ((this.e.TerBlt[bltId1].flags & 1) == 0)
      {
        if (!DoDraw)
        {
          this.e.DlgText1 = str2;
          this.e.DlgInt1 = index2;
          return true;
        }
        y += this.e.ScrYOffset;
        if (this.e.InPrinting)
          mode = this.SetBkMode(gr, 1);
        if (flag1)
        {
          COp.SIZE size;
          this.GetTextExtentPoint(gr, this.e.TerCurFont, str2, str2.Length, out size);
          int num6 = !this.e.InPrinting ? this.ScrToTwipsX(size.cx) : this.UnitToTwipsX(size.cx);
          if (this.e.PfmtId[CurPara].LeftIndentTwips + this.e.PfmtId[CurPara].FirstIndentTwips < num6)
          {
            int x1 = this.e.PfmtId[CurPara].LeftIndentTwips + this.e.PfmtId[CurPara].FirstIndentTwips;
            size.cx = !this.e.InPrinting ? this.TwipsToScrX(x1) : this.TwipsToUnitX(x1);
          }
          x -= size.cx;
          if (x < 0)
            x = 0;
        }
        else if ((this.e.text[LineNo].flags & 33554432 /*0x02000000*/) == 0 && num1 > Math.Abs(this.e.PfmtId[CurPara].FirstIndent))
        {
          x -= num1 - Math.Abs(this.e.PfmtId[CurPara].FirstIndent);
          if (x < 0)
            x = 0;
        }
        if (this.e.ScrRtl && this.e.pScrSeg != null)
        {
          COp.SIZE size;
          this.GetTextExtentPoint(gr, this.e.TerCurFont, str2, str2.Length, out size);
          x = this.RtlX(x, size.cx, this.e.CurFrame, new tc.StrLineSeg());
        }
        int x2 = this.FrameRotateX(x, y, this.e.CurFrame);
        int y1 = this.FrameRotateY(x, y, this.e.CurFrame);
        this.ExtTextOut(gr, x2, y1, options, str2.ToCharArray(), length, (int[]) null);
        if (this.e.InPrinting)
          this.SetBkMode(gr, mode);
        if ((this.e.TerFont[index2].style & 524545) != 0)
        {
          bool flag4 = (this.e.TerFont[index2].style & 256 /*0x0100*/) != 0;
          int num7 = (this.e.TerFont[index2].style & 1) != 0 ? 1 : 0;
          bool flag5 = (this.e.TerFont[index2].style & 524288 /*0x080000*/) != 0;
          COp.SIZE size;
          this.GetTextExtentPoint(gr, this.e.TerCurFont, str2, str2.Length, out size);
          y += this.e.TerFont[index2].BaseHeight - this.e.TerFont[index2].BaseHeightAdj;
          int num8 = y - this.e.TerFont[index2].BaseHeight / 2 - (this.e.InPrinting ? this.TwipsToUnitY(15) : this.TwipsToScrY(15));
          Pen SolidPen = new Pen(this.e.TerFont[index2].TextColor);
          int num9 = flag4 ? 1 : 0;
          if ((num7 | num9) != 0)
            this.DrawShadowLine(gr, x, y, x + size.cx, y, SolidPen, (Pen) null);
          if (flag4)
          {
            if (this.e.InPrinting)
              y += this.TwipsToUnitY(30);
            else
              y += this.TwipsToScrY(30);
            this.DrawShadowLine(gr, x, y, x + size.cx, y, SolidPen, (Pen) null);
          }
          if (flag5)
          {
            this.DrawShadowLine(gr, x, num8, x + size.cx, num8, SolidPen, (Pen) null);
            int num10 = num8 + (this.e.InPrinting ? this.TwipsToUnitY(30) : this.TwipsToScrY(30));
            this.DrawShadowLine(gr, x, num10, x + size.cx, num10, SolidPen, (Pen) null);
          }
          SolidPen.Dispose();
        }
      }
    }
    return true;
  }

  internal new bool DrawDarkDottedLine(Graphics gr, int x1, int y1, int x2, int y2)
  {
    if (y1 == y2)
    {
      for (int x = x1; x <= x2; x += 2)
        this.SetPixel(gr, x, y1, Color.Black);
    }
    else
    {
      for (int y = y1; y <= y2; y += 2)
        this.SetPixel(gr, x1, y, Color.Black);
    }
    return true;
  }

  internal new bool DrawDottedLine(Graphics gr, int x1, int y1, int x2, int y2, Color color)
  {
    int num1 = 3;
    int num2 = 40;
    bool flag = true;
    Pen black = Pens.Black;
    if (y1 != y2)
      flag = false;
    if (this.e.InPrinting)
    {
      this.ReleaseOpDC(gr, true);
      if (flag)
      {
        int num3 = this.e.UnitResX / num2;
        if (num3 < 2)
          num3 = 2;
        for (int x1_1 = x1; x1_1 <= x2; x1_1 += num3)
          gr.DrawLine(black, x1_1, y1, x1_1 + num3 / 2, y1);
      }
      else
      {
        int num4 = this.e.UnitResY / num2;
        if (num4 < 2)
          num4 = 2;
        for (int y1_1 = y1; y1_1 <= y2; y1_1 += num4)
          gr.DrawLine(black, x1, y1_1, x1, y1_1 + num4 / 2);
      }
    }
    else if (flag)
    {
      if (this.e.ScrRtl)
      {
        x1 = this.RtlX(x1, 0, this.e.CurFrame, new tc.StrLineSeg());
        x2 = this.RtlX(x2, 0, this.e.CurFrame, new tc.StrLineSeg());
      }
      if (x1 > x2)
        this.SwapInts(ref x1, ref x2);
      for (int x = x1; x <= x2; x += num1)
        this.SetPixel(gr, x, y1, color);
    }
    else
    {
      if (y1 > y2)
        this.SwapInts(ref y1, ref y2);
      for (int y = y1; y <= y2; y += num1)
        this.SetPixel(gr, x1, y, color);
    }
    return true;
  }

  internal new bool DrawEval()
  {
    if (!tc.eval || this.e.InDrawEval || this.e.InDesignMode())
      return true;
    ++this.e.EvalTimerCount;
    this.e.InDrawEval = true;
    IntPtr hTerWnd = this.e.hTerWnd;
    int num1 = this.e.HtmlMode ? 1 : 0;
    string str = "";
    if (!tc.expired)
      str += "License key not set! TE Edit Control (v13) is now running in the demo mode.\n\n";
    int num2 = (int) this.ShowMessage(str + "Please visit our web site: www.subsystems.com to purchase the product.\n" + "\n" + "If you have already purchased a license for this product, please set the license \n" + "using the TerSetLicenseKey method.  Your license key is available in a \n" + "distribution file called key.txt." + "\n" + "\n" + "\n" + "Thank you.", tc.EvalDaysUsed <= 0 ? "Evaluation Message" : (!tc.expired ? "EVALUATION DAY(S) REMAINING: " + tc.EvalDaysRemaining.ToString() : "***** EVALUATION EXPIRED *******"), MessageBoxButtons.OK);
    this.e.InDrawEval = false;
    return !tc.expired;
  }

  internal new bool DrawFootnote(Graphics gr, int FrameNo, bool screen)
  {
    if (tc.DebugMode)
      this.misc.dm(nameof (DrawFootnote));
    this.e.CurFrame = FrameNo;
    this.e.frame[FrameNo].SpaceRight = this.e.frame[FrameNo].SpaceLeft;
    int num1 = this.e.frame[FrameNo].x + this.e.frame[FrameNo].SpaceLeft;
    int num2 = this.e.frame[FrameNo].width - this.e.frame[FrameNo].SpaceLeft - this.e.frame[FrameNo].SpaceRight;
    if (!screen)
      num1 += this.e.PrtLeftMarg;
    int y1 = this.e.frame[FrameNo].y;
    int num3;
    if (screen)
    {
      COp.RECT rect;
      rect.left = num1;
      rect.right = !this.e.BorderShowing ? this.e.TerWinOrgX + this.e.TerWinWidth : rect.left + num2;
      rect.top = this.e.frame[FrameNo].y;
      rect.bottom = rect.top + this.e.frame[FrameNo].height;
      Color curBackColor = this.e.CurBackColor;
      this.e.CurBackColor = this.PageColor();
      this.SetBkColor(gr, this.e.CurBackColor);
      this.TerTextOut(gr, rect.left, rect.top, rect, (char[]) null, 0, (ushort[]) null, 0);
      this.e.CurBackColor = curBackColor;
      this.SetBkColor(gr, this.e.CurBackColor);
      num3 = this.TwipsToScrY(50);
    }
    else
      num3 = this.TwipsToUnitY(50);
    int y2 = y1 + num3;
    int page = this.e.text[this.e.frame[FrameNo].PageFirstLine].page;
    for (int pageFirstLine = this.e.frame[FrameNo].PageFirstLine; pageFirstLine <= this.e.frame[FrameNo].PageLastLine && pageFirstLine < this.e.TotalLines; ++pageFirstLine)
    {
      if (this.e.text[pageFirstLine].page == page && (this.e.text[pageFirstLine].flags & 65536 /*0x010000*/) != 0)
        y2 += this.ExtractFootnote(gr, num1, y2, pageFirstLine, this.GetSection(pageFirstLine), screen, true);
    }
    Pen SolidPen;
    if ((SolidPen = new Pen(this.ToColor(12632256 /*0xC0C0C0*/))) != null)
    {
      int num4 = this.e.frame[this.e.CurFrame].y + num3 / 2;
      if (this.IsDefLangRtl())
        this.DrawShadowLine(gr, num1 + num2, num4, num1 + num2 * 2 / 3, num4, SolidPen, (Pen) null);
      else
        this.DrawShadowLine(gr, num1, num4, num1 + num2 / 3, num4, SolidPen, (Pen) null);
      SolidPen.Dispose();
      int num5 = this.e.frame[this.e.CurFrame].y + this.e.frame[this.e.CurFrame].height - num3 / 2;
      if (screen && !this.e.BorderShowing)
        this.DrawDottedLine(gr, num1, num5, this.e.frame[this.e.CurFrame].x + this.e.frame[this.e.CurFrame].width, num5, Color.Black);
    }
    return true;
  }

  internal new bool DrawFrameBorder(
    Graphics gr,
    int x1,
    int y1,
    int x2,
    int y2,
    int thickness,
    int border)
  {
    Color color = Color.Black;
    int cellId = this.e.frame[this.e.CurFrame].CellId;
    int PageNo = this.e.CurFrame >= this.e.FirstPage2Frame ? this.e.FirstFramePage + 1 : this.e.FirstFramePage;
    bool scrRtl = this.e.ScrRtl;
    if (cellId > 0 && (this.e.CellAux[cellId].flags & 16 /*0x10*/) != 0)
    {
      bool flag = true;
      for (int index = this.e.TableRow[this.e.cell[cellId].row].FirstCell; index > 0; index = this.e.cell[index].NextCell)
      {
        if ((this.e.CellAux[index].flags & 16 /*0x10*/) == 0)
          flag = false;
      }
      if (flag)
        return true;
    }
    if (thickness > 0)
    {
      int num1 = thickness;
      if (cellId == 0)
      {
        thickness = y1 != y2 ? this.TwipsToScrX(thickness) : this.TwipsToScrY(thickness);
        if (thickness == 0 && num1 > 0)
          thickness = 1;
      }
      else
      {
        bool flag1;
        switch (border)
        {
          case 1:
          case 4:
            bool flag2 = true;
            if (border == 1 | flag2)
            {
              thickness = y1 != y2 ? this.TwipsToScrX(thickness) : this.TwipsToScrY(thickness);
              if (thickness <= 0)
              {
                thickness = 1;
                goto label_29;
              }
              goto label_29;
            }
            int num2 = this.TwipsToScrX(thickness);
            if (num2 <= 0)
              num2 = 1;
            thickness = this.TwipsToScrX(thickness) - num2;
            goto label_29;
          case 8:
            flag1 = this.e.cell[cellId].NextCell <= 0;
            if (this.e.ScrRtl)
            {
              flag1 = true;
              if (this.e.cell[cellId].NextCell <= 0)
              {
                thickness /= 2;
                break;
              }
              break;
            }
            break;
          default:
            flag1 = this.GetBorderCell(cellId, PageNo, true) <= 0;
            break;
        }
        if (flag1)
        {
          thickness = y1 != y2 ? this.TwipsToScrX(thickness) : this.TwipsToScrY(thickness);
          if (thickness <= 0)
            thickness = 1;
        }
        else
        {
          int num3 = y1 != y2 ? this.TwipsToScrX(thickness) : this.TwipsToScrY(thickness);
          if (num3 <= 0)
            num3 = 1;
          thickness = y1 != y2 ? this.TwipsToScrX(2 * thickness) - num3 : this.TwipsToScrY(2 * thickness) - num3;
        }
      }
label_29:
      if (thickness <= 0)
        return true;
    }
    if (thickness == 0)
    {
      if ((this.e.frame[this.e.CurFrame].flags & 131072 /*0x020000*/) != 0)
      {
        switch (border)
        {
          case 4:
            border = 8;
            break;
          case 8:
            border = 4;
            break;
        }
      }
      if ((this.e.frame[this.e.CurFrame].border & border) == 0 || cellId > 0 && (this.e.cell[cellId].border & border) != 0)
        return true;
      if (cellId > 0 && border == 1)
      {
        int row = this.e.cell[cellId].row;
        int prevRow = this.e.TableRow[row].PrevRow;
        bool flag3 = false;
        if (prevRow >= 0)
        {
          if (this.e.TableAux[prevRow].LastPage != this.e.TableAux[row].FirstPage)
            flag3 = true;
          if (!flag3 && this.e.TableAux[row].LastPage == PageNo && this.e.TableAux[row].LastPage != this.e.TableAux[row].FirstPage)
            flag3 = true;
          if (!flag3)
          {
            int scrX1 = this.UnitToScrX(this.e.TableAux[prevRow].FrmBegX);
            int scrX2 = this.UnitToScrX(this.e.TableAux[prevRow].FrmEndX);
            if (x1 >= scrX1 && x2 <= scrX2)
              return true;
            bool flag4 = x1 < scrX1 && x2 > scrX2;
            if (!flag4 && x1 < scrX1 && x2 <= scrX1)
              flag4 = true;
            if (!flag4 && x2 > scrX2 && x1 >= scrX2)
              flag4 = true;
            if (!flag4)
            {
              if (x1 < scrX1)
                x2 = scrX1;
              if (x2 > scrX2)
                x1 = scrX2;
            }
          }
        }
      }
      if ((this.e.frame[this.e.CurFrame].flags & 131072 /*0x020000*/) != 0 && border == 8 && x1 == -1 && x2 == -1)
        x1 = x2 = 0;
      return this.e.FullCellHilight ? this.DrawDottedLine(gr, x1, y1, x2, y2, Color.White) : this.DrawDottedLine(gr, x1, y1, x2, y2, Color.Black);
    }
    if (border == 1)
      color = this.e.frame[this.e.CurFrame].BorderColor[0];
    if (border == 2)
      color = this.e.frame[this.e.CurFrame].BorderColor[1];
    if (border == 4)
      color = this.e.frame[this.e.CurFrame].BorderColor[2];
    if (border == 8)
      color = this.e.frame[this.e.CurFrame].BorderColor[3];
    int paraFrameId = this.e.frame[this.e.CurFrame].ParaFrameId;
    if ((this.e.ParaFrame[paraFrameId].flags & 131200 /*0x020080*/) != 0)
      color = this.e.ParaFrame[paraFrameId].LineColor;
    if (paraFrameId != 0 && (this.e.ParaFrame[paraFrameId].flags & 2048 /*0x0800*/) != 0)
      return this.DrawDottedLine(gr, x1, y1, x2, y2, color);
    this.e.ScrRtl = false;
    if (thickness == 1)
    {
      if (this.e.FullCellHilight)
      {
        this.DrawShadowLine(gr, x1, y1, x2, y2, Pens.White, (Pen) null);
      }
      else
      {
        Pen SolidPen = !(color == Color.Black) ? new Pen(color, 1f) : Pens.Black;
        this.DrawShadowLine(gr, x1, y1, x2, y2, SolidPen, (Pen) null);
        if (color != Color.Black)
          SolidPen.Dispose();
      }
    }
    else if (thickness > 1)
    {
      COp.RECT rect;
      switch (border)
      {
        case 1:
        case 2:
          rect.top = border != 1 ? y1 - thickness + 1 : y1;
          rect.bottom = rect.top + thickness;
          rect.left = x1;
          rect.right = x2;
          goto label_84;
        case 4:
          rect.left = x1;
          break;
        default:
          rect.left = x1 - thickness + 1;
          break;
      }
      rect.right = rect.left + thickness;
      rect.top = y1;
      rect.bottom = y2;
label_84:
      this.ReleaseOpDC(gr, true);
      if (this.e.FullCellHilight)
        this.OurFillRectangle(gr, tc.CLR_WHITE, rect);
      else
        this.OurFillRectangle(gr, color, rect);
    }
    this.e.ScrRtl = scrRtl;
    return true;
  }

  internal new bool DrawHtmlCellBorder(
    Graphics gr,
    int CellId,
    int x1,
    int y1,
    int x2,
    int y2,
    int[] BorderWidth,
    bool screen)
  {
    Pen SolidPen1 = Pens.Black;
    Pen SolidPen2 = Pens.White;
    Color color = Color.White;
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = true;
    bool flag4 = false;
    bool flag5 = false;
    bool flag6 = false;
    bool flag7 = false;
    int PageNo = this.e.CurFrame >= this.e.FirstPage2Frame ? this.e.FirstFramePage + 1 : this.e.FirstFramePage;
    if ((this.e.cell[CellId].flags & 16 /*0x10*/) == 0)
    {
      int width;
      if (screen)
      {
        width = 1;
      }
      else
      {
        width = this.TwipsToUnitX(15);
        SolidPen1 = new Pen(Color.Black);
        SolidPen2 = new Pen(this.ToColor(4144959 /*0x3F3F3F*/), (float) width);
        flag1 = flag2 = true;
      }
      int num1 = 2 * width;
      int num2 = 3 * width;
      bool flag8 = this.IsPartRow(true, this.e.cell[CellId].row, PageNo);
      bool flag9 = this.IsPartRow(false, this.e.cell[CellId].row, PageNo);
      if (this.e.TerArg.ReadOnly)
      {
        int pageFirstLine = this.e.frame[this.e.CurFrame].PageFirstLine;
        if (this.e.text[pageFirstLine].tabw != null && (this.e.text[pageFirstLine].tabw.type & 16 /*0x10*/) != 0 && this.e.text[pageFirstLine].len == 1 && !flag8)
          flag3 = false;
      }
      int num3;
      int num4;
      int num5;
      int num6;
      if (screen)
      {
        num3 = this.TwipsToScrX(BorderWidth[2]);
        num4 = this.TwipsToScrX(BorderWidth[3]);
        num5 = this.TwipsToScrY(BorderWidth[0]);
        num6 = this.TwipsToScrY(BorderWidth[1]);
      }
      else
      {
        num3 = this.TwipsToUnitX(BorderWidth[2]);
        num4 = this.TwipsToUnitX(BorderWidth[3]);
        num5 = this.TwipsToUnitY(BorderWidth[0]);
        num6 = this.TwipsToUnitY(BorderWidth[1]);
      }
      int row = this.e.cell[CellId].row;
      if (this.IsFirstTableRow(row))
        flag4 = true;
      for (int rowSpan = this.e.cell[CellId].RowSpan; rowSpan > 1; --rowSpan)
      {
        if (row > 0)
          row = this.e.TableRow[row].NextRow;
      }
      if (row <= 0 || this.IsLastTableRow(row))
        flag5 = true;
      if (this.e.cell[CellId].PrevCell <= 0)
        flag6 = true;
      if (this.e.cell[CellId].NextCell <= 0)
        flag7 = true;
      if (screen)
        color = this.SetBkColor(gr, this.PageColor());
      COp.RECT OurRect;
      this.SetRect(out OurRect, x1, y1, x2, y1 + num5);
      if (!flag8)
        this.ExtTextOut(gr, 0, 0, 2, OurRect, (char[]) null, 0, (int[]) null);
      this.SetRect(out OurRect, x1, y2 - num6, x2, y2);
      if (!flag9)
        this.ExtTextOut(gr, 0, 0, 2, OurRect, (char[]) null, 0, (int[]) null);
      this.SetRect(out OurRect, x1, y1, x1 + num3, y2);
      this.ExtTextOut(gr, 0, 0, 2, OurRect, (char[]) null, 0, (int[]) null);
      this.SetRect(out OurRect, x2 - num4, y1, x2, y2);
      this.ExtTextOut(gr, 0, 0, 2, OurRect, (char[]) null, 0, (int[]) null);
      if (screen)
        this.SetBkColor(gr, color);
      bool flag10;
      if (this.e.cell[CellId].BorderColor[0] != tc.CLR_AUTO)
      {
        if (flag1)
          SolidPen2.Dispose();
        if (flag2)
          SolidPen1.Dispose();
        bool flag11;
        flag10 = flag11 = false;
        SolidPen2 = new Pen(this.e.cell[CellId].BorderColor[0]);
        SolidPen1 = new Pen(this.e.cell[CellId].BorderColor[0]);
        flag1 = flag2 = true;
      }
      else if (screen && this.PageColor() == Color.White)
      {
        if (flag1)
          SolidPen2.Dispose();
        if (flag2)
          SolidPen1.Dispose();
        flag10 = flag2 = false;
        SolidPen2 = new Pen(this.ToColor(11513775 /*0xAFAFAF*/));
        flag1 = true;
      }
      if (flag4)
      {
        this.DrawShadowLine(gr, x1, y1, x2 - width, y1, SolidPen2, (Pen) null);
        y1 += num2;
      }
      if (flag5)
      {
        this.DrawShadowLine(gr, x1, y2 - width, x2 - width, y2 - width, SolidPen1, (Pen) null);
        y2 -= width;
      }
      if (flag6)
      {
        this.DrawShadowLine(gr, x1, y1 - num1, x1, y2, SolidPen2, (Pen) null);
        x1 += num2;
      }
      if (flag7)
      {
        this.DrawShadowLine(gr, x2 - width, y1 - num1, x2 - width, y2, SolidPen1, (Pen) null);
        x2 -= width;
      }
      if (flag3)
      {
        if (!flag8)
          this.DrawShadowLine(gr, x1, y1, x2 - num2, y1, SolidPen1, (Pen) null);
        this.DrawShadowLine(gr, x1, y1, x1, y2 - num2, SolidPen1, (Pen) null);
        if (!flag9)
          this.DrawShadowLine(gr, x1 + width, y2 - num2, x2 - num2, y2 - num2, SolidPen2, (Pen) null);
        this.DrawShadowLine(gr, x2 - num2, y1 + width, x2 - num2, y2 - num2, SolidPen2, (Pen) null);
      }
      if (flag1)
        SolidPen2.Dispose();
      if (flag2)
        SolidPen1.Dispose();
      if (!screen)
      {
        SolidPen1.Dispose();
        SolidPen2.Dispose();
      }
    }
    return true;
  }

  internal new bool DrawLightDottedLine(Graphics gr, int x1, int y1, int x2, int y2)
  {
    if (y1 == y2)
    {
      for (int x = x1; x <= x2; x += 4)
        this.SetPixel(gr, x, y1, Color.Black);
    }
    else
    {
      for (int y = y1; y <= y2; y += 4)
        this.SetPixel(gr, x1, y, Color.Black);
    }
    return true;
  }

  internal new bool DrawLineObject(Graphics gr, int FrameNo)
  {
    int paraFrameId = this.e.frame[FrameNo].ParaFrameId;
    DashStyle dashStyle = DashStyle.Solid;
    if (!this.e.frame[FrameNo].empty && (this.e.ParaFrame[paraFrameId].flags & 256 /*0x0100*/) != 0 && (this.e.ParaFrame[paraFrameId].flags & 262144 /*0x040000*/) == 0)
    {
      int pict = this.e.ParaFrame[paraFrameId].pict;
      if (pict > 0 && pict < this.e.TotalFonts && this.e.TerFont[pict].PictType == 11)
      {
        this.e.TerFont[pict].PictX = this.e.frame[FrameNo].x - this.e.TerWinOrgX;
        this.e.TerFont[pict].PictY = this.e.frame[FrameNo].y - this.e.TerWinOrgY;
        this.e.TerFont[pict].DispFrame = FrameNo;
      }
      int x1;
      int y1;
      int x2;
      int y2;
      this.GetLinePoints(FrameNo, out x1, out y1, out x2, out y2);
      int width = !this.e.InPrinting ? this.TwipsToScrX(this.e.ParaFrame[paraFrameId].LineWdth) : this.TwipsToUnitX(this.e.ParaFrame[paraFrameId].LineWdth);
      if ((this.e.ParaFrame[paraFrameId].flags & 2048 /*0x0800*/) != 0)
      {
        dashStyle = DashStyle.Dot;
        width = 1;
      }
      Pen SolidPen;
      if ((SolidPen = new Pen(this.e.ParaFrame[paraFrameId].LineColor, (float) width)) == null)
        return true;
      SolidPen.DashStyle = dashStyle;
      this.DrawShadowLine(gr, x1, y1, x2, y2, SolidPen, (Pen) null);
      SolidPen.Dispose();
    }
    return true;
  }

  internal bool DrawMouseStopMsg(Graphics gr, string text)
  {
    Pen hSolidPen = Pens.White;
    Pen black = Pens.Black;
    bool flag1 = false;
    bool flag2 = false;
    int num1 = 5;
    int index1 = 0;
    string[] strArray = new string[10];
    if (!this.e.InPrintPreview)
    {
      int length = text.Length;
      int startIndex = 0;
      for (int index2 = 0; index2 <= length; ++index2)
      {
        if (index2 == length || text[index2] == '\n')
        {
          strArray[index1] = text.Substring(startIndex, index2 - startIndex);
          ++index1;
          startIndex = index2 + 1;
          if (index1 >= 10)
            break;
        }
      }
      int num2 = this.e.RulerFontHeight * index1 + this.e.RulerFontHeight / 2;
      if (num2 < this.e.TerWinHeight)
      {
        int num3 = 0;
        for (int index3 = 0; index3 < index1; ++index3)
        {
          COp.SIZE size;
          this.GetTextExtentPoint(gr, this.e.RulerFontBold, strArray[index3], strArray[index3].Length, out size);
          if (size.cx > num3)
            num3 = size.cx;
        }
        int num4 = num3 + num1 * 2;
        if (num4 < this.e.TerWinWidth)
        {
          COp.RECT OurRect;
          OurRect.top = this.e.TerWinOrgY + (this.e.MouseStopY - this.e.TerWinRect.top - num2 - 10);
          if (OurRect.top <= this.e.TerWinOrgY)
          {
            OurRect.top = this.e.TerWinOrgY + 1;
            flag2 = true;
          }
          OurRect.bottom = OurRect.top + num2;
          if (OurRect.bottom >= this.e.TerWinOrgY + this.e.TerWinHeight)
          {
            OurRect.bottom = this.e.TerWinOrgY + this.e.TerWinHeight - 1;
            OurRect.top = OurRect.bottom - num2;
            flag2 = true;
          }
          OurRect.left = this.e.TerWinOrgX + (this.e.MouseStopX - this.e.TerWinRect.left - num4 / 2);
          if (flag2)
          {
            if (this.e.MouseStopX - this.e.TerWinRect.left < this.e.TerWinWidth / 2)
              OurRect.left += num4 / 2 + 2 * num1;
            else
              OurRect.left -= num4 / 2 + 2 * num1;
          }
          if (OurRect.left < this.e.TerWinOrgX)
            OurRect.left = this.e.TerWinOrgX;
          OurRect.right = OurRect.left + num4;
          if (OurRect.right >= this.e.TerWinOrgX + this.e.TerWinWidth)
          {
            OurRect.right = this.e.TerWinOrgX + this.e.TerWinWidth - 1;
            OurRect.left = OurRect.right - num4;
          }
          if (this.PageColor() == Color.White)
          {
            hSolidPen = new Pen(this.ToColor(175, 175, 175));
            flag1 = true;
          }
          Brush brush = (Brush) new SolidBrush(this.ToColor((int) byte.MaxValue, (int) byte.MaxValue, 225));
          gr.FillRectangle(brush, this.ToRectangle(OurRect));
          brush.Dispose();
          this.DrawShadowBox(gr, OurRect.left, OurRect.top, OurRect.right, OurRect.bottom, hSolidPen, black);
          int y = OurRect.top + this.e.RulerFontHeight / 4;
          for (int index4 = 0; index4 < index1; ++index4)
          {
            Font font = index4 % 2 != 0 ? this.e.RulerFont : this.e.RulerFontBold;
            gr.DrawString(strArray[index4], font, Brushes.Black, new PointF((float) (OurRect.left + num1), (float) y));
            y += this.e.RulerFontHeight;
          }
        }
      }
      if (flag1)
        hSolidPen.Dispose();
      this.e.MouseStopMsgOn = true;
    }
    return true;
  }

  internal new int DrawOneFootnote(
    Graphics gr,
    int x,
    int y,
    int line,
    int BufLen,
    int WrapWidth,
    bool screen,
    bool DrawIt)
  {
    int num1 = 0;
    int idx = 0;
    int num2 = 0;
    int x1 = 0;
    bool flag = false;
    ushort[] numArray = new ushort[1001];
    this.e.TerOpFlags2 |= 8;
    int index1;
    for (; idx < BufLen; idx = index1 + 1)
    {
      index1 = idx;
      int num3 = -1;
      int num4 = 0;
      if (num2 > 0)
        num4 = x1;
      int num5;
      while (true)
      {
        int x2 = (int) this.e.WrapCharWidth[index1];
        num5 = screen ? this.ScrToUnitX(x2) : x2;
        numArray[index1 - idx] = (ushort) x2;
        if (num2 == 0 && !flag)
          x1 += x2;
        if (this.e.wrap[index1] == ' ')
          flag = true;
        if ((int) this.e.wrap[index1] != (int) this.e.ParaChar && this.e.wrap[index1] != '\u000F' && num4 + num5 < WrapWidth && index1 - idx < 1000 && index1 + 1 < BufLen)
        {
          if (this.e.wrap[index1] == ' ')
            num3 = index1;
          num4 += num5;
          ++index1;
        }
        else
          break;
      }
      ++num2;
      if (num3 >= 0 && num4 + num5 > WrapWidth)
        index1 = num3;
      int num6 = index1 - idx + 1;
      int pBaseHeight;
      int pExtLead;
      int textHeight = this.GetTextHeight(this.CopyArray(this.e.wrap, idx, num6), this.CopyArray(this.e.WrapCfmt, idx, num6), num6, false, out pBaseHeight, out pExtLead);
      if (screen)
        num1 += this.UnitToScrY(textHeight);
      else
        num1 += textHeight;
      if (DrawIt)
      {
        this.e.TerOpFlags |= 1024 /*0x0400*/;
        this.e.TempCfmt = this.CopyArray(this.e.WrapCfmt, idx);
        this.e.NextX = x;
        if ((this.e.text[line].flags2 & 32 /*0x20*/) != 0)
        {
          this.e.ScrRtl = true;
          int pTotalLineSeg;
          this.e.pScrSeg = this.GetLineSeg2(-1, numArray, out pTotalLineSeg, num6, this.e.frame[this.e.CurFrame].SpaceLeft, this.CopyArray(this.e.wrap, idx, num6), this.CopyArray(this.e.WrapCfmt, idx, num6));
          this.e.TotalScrSeg = pTotalLineSeg;
          this.e.CurScrSeg = 0;
        }
        if (screen)
        {
          this.e.pLineCharWidth = new ushort[num6 + 1];
          if (this.e.pLineCharWidth != null)
          {
            for (int index2 = 0; index2 < num6; ++index2)
              this.e.pLineCharWidth[index2] = numArray[index2];
          }
          this.GetTextHeight(this.CopyArray(this.e.wrap, idx, num6), this.CopyArray(this.e.WrapCfmt, idx, num6), num6, true, out pBaseHeight, out pExtLead);
          if (num2 > 1)
            this.e.NextX += this.UnitToScrX(x1);
          this.DisplayText(gr, 0, y, this.UnitToScrY(textHeight), pBaseHeight, this.CopyArray(this.e.wrap, idx, num6), num6, line, false, 'T');
          y += this.UnitToScrY(textHeight);
          this.e.pLineCharWidth = (ushort[]) null;
        }
        else
        {
          if (num2 > 1)
            this.e.NextX += x1;
          this.PrintOneLine(line, gr, this.e.NextX, y, this.CopyArray(this.e.wrap, idx, num6), this.CopyArray(this.e.WrapCfmt, idx, num6), num6, textHeight, pBaseHeight, (tc.ClsTabw) null, this.e.text[line].pfmt, 0, 0, pExtLead, this.e.CurFrame, this.e.PrtLeftMarg, this.e.PageX, numArray, false, 0, 0);
          y += textHeight;
        }
        if ((this.e.text[line].flags2 & 32 /*0x20*/) != 0)
        {
          this.e.ScrRtl = false;
          this.e.pScrSeg = (tc.StrLineSeg[]) null;
          this.e.TotalScrSeg = this.e.CurScrSeg = 0;
        }
        this.e.TerOpFlags &= -1025;
      }
    }
    this.e.TerOpFlags2 &= -9;
    return num1;
  }

  internal bool DrawPageBorder(Graphics gr, int FrameNo)
  {
    Pen black = Pens.Black;
    Pen ShadowPen = (Pen) null;
    int num = this.e.frame[FrameNo].flags & 480;
    if ((num & 320) != 0)
      ShadowPen = new Pen(Color.Black, 3f);
    int x = this.e.frame[FrameNo].x;
    int y = this.e.frame[FrameNo].y;
    int width = this.e.frame[FrameNo].width;
    int height = this.e.frame[FrameNo].height;
    int scrX = this.UnitToScrX(this.e.frame[FrameNo].LeftBorderWdth);
    if ((num & 128 /*0x80*/) != 0)
      this.DrawShadowLine(gr, x + scrX, y + height - 1, x + width - scrX, y + height - 1, black, (Pen) null);
    if ((num & 256 /*0x0100*/) != 0)
      this.DrawShadowLine(gr, x + scrX, y, x + width - scrX, y, black, ShadowPen);
    if ((num & 32 /*0x20*/) != 0)
      this.DrawShadowLine(gr, x + width - 1, y, x + width - 1, y + height, black, (Pen) null);
    if ((num & 64 /*0x40*/) != 0)
      this.DrawShadowLine(gr, x, y, x, y + height, black, ShadowPen);
    ShadowPen?.Dispose();
    return true;
  }

  internal new bool DrawPageBorderBox(Graphics gr, int FrameNo)
  {
    int num1 = 30;
    bool[] flagArray = new bool[5];
    int[] numArray = new int[5];
    int x1 = this.e.frame[FrameNo].x;
    int y = this.e.frame[FrameNo].y;
    int width = this.e.frame[FrameNo].width;
    int height = this.e.frame[FrameNo].height;
    int sect = this.e.frame[FrameNo].sect;
    int border = this.e.TerSect[sect].border;
    int num2 = 1;
    int num3 = 2;
    bool flag1;
    flagArray[2] = flag1 = false;
    bool flag2;
    flagArray[1] = flag2 = flag1;
    flagArray[0] = flag2;
    bool flag3 = false;
    if (this.e.TerSect[sect].BorderType == 1)
    {
      num2 = 2;
      num3 = 4;
    }
    else if (this.e.TerSect[sect].BorderType == 2)
    {
      num2 = 3;
      num3 = 6;
    }
    else if (this.e.TerSect[sect].BorderType == 3)
      flag3 = true;
    else if (this.e.TerSect[sect].BorderType == 4)
    {
      num2 = 2;
      flagArray[1] = true;
      num3 = 3;
    }
    else if (this.e.TerSect[sect].BorderType == 6)
    {
      num2 = 2;
      flagArray[0] = true;
      num3 = 3;
    }
    else if (this.e.TerSect[sect].BorderType == 5)
    {
      num2 = 3;
      flagArray[1] = true;
      num3 = 5;
    }
    else if (this.e.TerSect[sect].BorderType == 7)
    {
      num2 = 3;
      bool flag4;
      flagArray[2] = flag4 = true;
      flagArray[0] = flag4;
      num3 = 4;
    }
    float num4;
    while (true)
    {
      num4 = (float) (this.e.TerSect[sect].BorderWidth[0] - num1 * (num2 - 1)) / (float) num3;
      if ((double) num4 <= (double) num1 && num1 > 15 && num2 != 1)
        num1 /= 2;
      else
        break;
    }
    for (int index = 0; index < num2; ++index)
      numArray[index] = (int) ((double) num4 * (flagArray[index] ? 1.0 : 2.0));
    if (this.e.PageBkColor != tc.CLR_WHITE && !this.e.InPrinting)
    {
      int x2 = this.e.TerSect[sect].BorderSpace[0];
      int num5 = this.e.InPrinting ? this.TwipsToUnitY(x2) : this.TwipsToScrY(x2);
      if (this.e.InPrintPreview)
        num5 -= this.ScrToUnitY(2);
      int x3 = this.e.TerSect[sect].BorderSpace[1];
      int num6 = this.e.InPrinting ? this.TwipsToUnitY(x3) : this.TwipsToScrY(x3);
      int x4 = this.e.TerSect[sect].BorderSpace[2];
      int num7 = this.e.InPrinting ? this.TwipsToUnitX(x4) : this.TwipsToScrX(x4);
      if (this.e.InPrintPreview)
        num7 -= this.ScrToUnitY(2);
      int x5 = this.e.TerSect[sect].BorderSpace[3];
      int num8 = this.e.InPrinting ? this.TwipsToUnitX(x5) : this.TwipsToScrX(x5);
      COp.RECT OurRect;
      this.SetRect(out OurRect, x1 - num7, y - num5, x1 + width + num8, y);
      this.OurFillRectangle(gr, tc.CLR_WHITE, OurRect);
      this.SetRect(out OurRect, x1 - num7, y + height, x1 + width + num8, y + height + num6);
      this.OurFillRectangle(gr, tc.CLR_WHITE, OurRect);
      this.SetRect(out OurRect, x1 - num7, y - num5, x1, y + height + num6);
      this.OurFillRectangle(gr, tc.CLR_WHITE, OurRect);
      this.SetRect(out OurRect, x1 + width, y - num5, x1 + width + num8, y + height + num6);
      this.OurFillRectangle(gr, tc.CLR_WHITE, OurRect);
    }
    Pen ShadowPen = (Pen) null;
    for (int index = 0; index < num2; ++index)
    {
      int pDeviceThick;
      Pen pen = this.OurCreatePen(numArray[index], this.e.TerSect[sect].BorderColor, out pDeviceThick);
      if (flag3)
        ShadowPen = this.OurCreatePen(this.e.TerSect[sect].BorderWidth[0] * (flagArray[index] ? 2 : 1), this.ToColor(this.ToColorRef(this.e.TerSect[sect].BorderColor) | 8421504 /*0x808080*/), out pDeviceThick);
      if ((border & 1) != 0)
        this.DrawShadowLine(gr, x1, y, x1 + width, y, pen, ShadowPen);
      if ((border & 2) != 0)
        this.DrawShadowLine(gr, x1, y + height, x1 + width, y + height, pen, ShadowPen);
      if ((border & 4) != 0)
        this.DrawShadowLine(gr, x1, y, x1, y + height, pen, ShadowPen);
      if ((border & 8) != 0)
        this.DrawShadowLine(gr, x1 + width, y, x1 + width, y + height, pen, ShadowPen);
      if (index + 1 < num2)
      {
        int x6 = num1 + (numArray[index] + numArray[index + 1]) / 2;
        int num9 = this.e.InPrinting ? this.TwipsToUnitX(x6) : this.TwipsToScrX(x6);
        x1 += num9;
        y += num9;
        width -= 2 * num9;
        height -= 2 * num9;
      }
      pen.Dispose();
      ShadowPen?.Dispose();
      ShadowPen = (Pen) null;
    }
    return true;
  }

  internal new bool DrawPageBox(Graphics gr, int pos)
  {
    Pen hSolidPen = Pens.White;
    Pen black = Pens.Black;
    bool flag = false;
    int z = 1000 - this.e.VerThumbSize;
    if (!this.e.PagesShowing || this.e.TotalPages < 1 || !this.e.PageBoxShowing && pos == this.e.VerScrollPos)
      return false;
    int num1 = this.MulDiv(this.SumPageScrHeight(0, this.e.TotalPages) - this.e.TerWinHeight, pos, z);
    int num2 = 0;
    int PageNo;
    for (PageNo = 0; PageNo < this.e.TotalPages - 1; ++PageNo)
    {
      num2 += this.GetScrPageHt(PageNo);
      if (num2 > num1)
        break;
    }
    int num3 = this.e.RulerFontHeight * 3 / 2;
    int origScrX = this.TwipsToOrigScrX(1100);
    if (!this.e.PageBoxShowing)
    {
      this.e.PageBoxRect.top = this.e.TerWinOrgY + pos * this.e.TerWinHeight / 1000;
      if (this.e.PageBoxRect.top <= this.e.TerWinOrgY)
        this.e.PageBoxRect.top = this.e.TerWinOrgY + 1;
      if (this.e.TerWinHeight <= num3)
        return false;
      this.e.PageBoxRect.bottom = this.e.PageBoxRect.top + num3;
      if (this.e.PageBoxRect.bottom >= this.e.TerWinOrgY + this.e.TerWinHeight)
      {
        this.e.PageBoxRect.bottom = this.e.TerWinOrgY + this.e.TerWinHeight - 1;
        this.e.PageBoxRect.top = this.e.PageBoxRect.bottom - num3;
      }
      this.e.PageBoxRect.right = this.e.TerWinOrgX + this.e.TerWinWidth - origScrX / 8;
      this.e.PageBoxRect.left = this.e.PageBoxRect.right - origScrX;
      if (this.e.PageBoxRect.left < this.e.TerWinOrgX)
        return false;
      this.e.BoxPage = -1;
      this.e.PageBoxShowing = true;
    }
    if (PageNo != this.e.BoxPage)
    {
      if (this.PageColor() == Color.White)
      {
        hSolidPen = new Pen(this.ToColor(175, 175, 175));
        flag = true;
      }
      Brush brush = (Brush) new SolidBrush(this.ToColor((int) byte.MaxValue, (int) byte.MaxValue, 225));
      gr.FillRectangle(brush, this.ToRectangle(this.e.PageBoxRect));
      brush.Dispose();
      this.DrawShadowBox(gr, this.e.PageBoxRect.left, this.e.PageBoxRect.top, this.e.PageBoxRect.right, this.e.PageBoxRect.bottom, hSolidPen, black);
      string s = (this.e.TerFlags2 & 128 /*0x80*/) == 0 ? this.e.MsgString[203] + (PageNo + 1).ToString() : this.e.MsgString[203] + this.e.PageInfo[PageNo].DispNbr.ToString();
      gr.DrawString(s, this.e.RulerFont, Brushes.Black, new PointF((float) (this.e.PageBoxRect.left + origScrX / 8), (float) (this.e.PageBoxRect.top + this.e.RulerFontHeight / 4)));
      this.e.BoxPage = PageNo;
    }
    if (flag)
      hSolidPen.Dispose();
    return true;
  }

  internal new bool DrawParaBorder(Graphics gr, COp.RECT TextRect, bool outer)
  {
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    bool flag4 = false;
    COp.RECT rect = TextRect;
    rect.top -= this.e.SpaceBef;
    rect.bottom += this.e.SpaceAft;
    int num1 = !this.e.InPrinting ? this.TwipsToScrY(this.e.ParaBoxSpace) : this.TwipsToUnitY(this.e.ParaBoxSpace);
    rect.top += this.e.ParaSpcBef;
    rect.bottom -= this.e.ParaSpcAft;
    int num2 = !this.e.InPrinting ? this.PointsToScrX(1) : this.PointsToUnitX(1);
    if (num2 < 1)
      num2 = 1;
    int num3 = (this.e.TextBorder & 512 /*0x0200*/) == 0 ? num2 : 2 * num2;
    int num4 = 0;
    int num5;
    if ((this.e.TextBorder & 256 /*0x0100*/) != 0)
    {
      if (outer)
      {
        num5 = 2 * num3 + num2;
        num4 = -(num2 + num3);
      }
      else
        num5 = num3;
    }
    else
      num5 = num3;
    int num6;
    if ((this.e.TerFlags3 & 524288 /*0x080000*/) != 0)
    {
      num6 = num4 + 1;
    }
    else
    {
      int num7 = num4;
      num4 += this.e.SpaceBef;
      num6 = num7 + this.e.SpaceAft;
    }
    int num8 = this.e.BoxLeft - (num5 - num3) - num1;
    int num9 = this.e.BoxRight + (num5 - num3) + num1;
    if ((this.e.TextBorder & 16 /*0x10*/) != 0)
      flag3 = true;
    if ((this.e.TextBorder & 65568 /*0x010020*/) != 0)
      flag4 = true;
    if ((this.e.TextBorder & 64 /*0x40*/) != 0 && num8 >= rect.left && num8 < rect.right)
      flag1 = true;
    if ((this.e.TextBorder & 128 /*0x80*/) != 0 && num9 >= rect.left && num9 < rect.right)
      flag2 = true;
    if (flag3 | flag4 | flag1 | flag2)
    {
      Color color = this.IsSameColor(this.e.ParaBoxColor, tc.CLR_AUTO) ? Color.Black : this.e.ParaBoxColor;
      double width = (double) num3;
      Pen SolidPen;
      if ((SolidPen = new Pen(color, (float) width)) == null)
        return true;
      if (flag3)
      {
        int x1 = num8 >= rect.left ? num8 : rect.left;
        int x2 = num9 <= rect.right ? num9 : rect.right;
        int y2;
        int y1 = y2 = rect.top + num4;
        this.DrawShadowLine(gr, x1, y1, x2, y2, SolidPen, (Pen) null);
        if (this.e.HtmlMode && !flag4)
          this.DrawShadowLine(gr, x1, y1, x2, y2, Pens.White, (Pen) null);
      }
      if (flag4)
      {
        int x1 = num8 >= rect.left ? num8 : rect.left;
        int x2 = num9 <= rect.right ? num9 : rect.right;
        int y2;
        int y1 = y2 = rect.bottom - num6;
        if ((this.e.TextBorder & 65536 /*0x010000*/) != 0)
          y1 = (y2 -= 2 * num2);
        if (x2 > x1)
          this.DrawShadowLine(gr, x1, y1, x2, y2, SolidPen, (Pen) null);
      }
      if (flag1)
      {
        int x2;
        int x1 = x2 = num8;
        int top = rect.top;
        if (flag3)
          top += num4;
        int bottom = rect.bottom;
        if (flag4)
          bottom -= num6;
        this.DrawShadowLine(gr, x1, top, x2, bottom, SolidPen, (Pen) null);
      }
      if (flag2)
      {
        int x2;
        int x1 = x2 = num9;
        int top = rect.top;
        if (flag3)
          top += num4;
        int bottom = rect.bottom;
        if (flag4)
          bottom -= num6;
        this.DrawShadowLine(gr, x1, top, x2, bottom, SolidPen, (Pen) null);
      }
      SolidPen.Dispose();
      if ((this.e.TextBorder & 256 /*0x0100*/) != 0 & outer)
        this.DrawParaBorder(gr, TextRect, false);
    }
    return true;
  }

  internal new bool DrawPictFrame(Graphics gr, int FrameNo)
  {
    if (tc.DebugMode)
      this.misc.dm(nameof (DrawPictFrame));
    int x = this.e.frame[FrameNo].x;
    int y = this.e.frame[FrameNo].y;
    int width = this.e.frame[FrameNo].width;
    int height = this.e.frame[FrameNo].height;
    COp.RECT OurRect;
    this.SetRect(out OurRect, x, y - this.e.frame[FrameNo].SpaceTop, x + width, y + height);
    this.SetColor(gr, 'T', 0);
    this.TerTextOut(gr, OurRect.left, OurRect.top, OurRect, (char[]) null, 0, (ushort[]) null, 0);
    int pict = this.e.ParaFrame[this.e.frame[FrameNo].ParaFrameId].pict;
    if (this.e.TerFont[pict].InUse && (this.e.TerFont[pict].style & 128 /*0x80*/) != 0)
    {
      OurRect.top = y;
      OurRect.left = x + this.e.frame[FrameNo].SpaceLeft;
      OurRect.right = x + width - this.e.frame[FrameNo].SpaceRight;
      OurRect.bottom = y + height - this.e.frame[FrameNo].SpaceBot;
      this.SetPictSize(pict, this.TwipsToScrY(this.e.TerFont[pict].PictHeight), this.TwipsToScrX(this.e.TerFont[pict].PictWidth), false);
      this.e.TerOpFlags |= 512 /*0x0200*/;
      this.TerPictOut(gr, OurRect.left, OurRect.top, OurRect, pict, 1, this.e.frame[FrameNo].PageFirstLine, 0);
      this.e.TerOpFlags &= -513;
      this.DrawPictRect(gr, FrameNo);
      this.SetPictSize(pict, 0, 0, true);
      this.e.TerFont[pict].DispFrame = FrameNo;
    }
    return true;
  }

  internal new bool DrawPictRect(Graphics gr, int FrameNo)
  {
    int paraFrameId = this.e.frame[FrameNo].ParaFrameId;
    if ((this.e.ParaFrame[paraFrameId].flags & 1024 /*0x0400*/) == 0)
      return false;
    int x = this.e.frame[FrameNo].x;
    int y = this.e.frame[FrameNo].y;
    int width = this.e.frame[FrameNo].width;
    int height = this.e.frame[FrameNo].height;
    COp.RECT rect;
    rect.top = y;
    rect.left = x + this.e.frame[FrameNo].SpaceLeft;
    rect.right = x + width - this.e.frame[FrameNo].SpaceRight;
    rect.bottom = y + height - this.e.frame[FrameNo].SpaceBot;
    Pen pen = this.OurCreatePen(this.e.ParaFrame[paraFrameId].LineWdth, this.e.ParaFrame[paraFrameId].LineColor, out int _);
    this.DrawShadowLine(gr, rect.left, rect.top, rect.right, rect.top, pen, (Pen) null);
    this.DrawShadowLine(gr, rect.left, rect.bottom, rect.right, rect.bottom, pen, (Pen) null);
    this.DrawShadowLine(gr, rect.left, rect.top, rect.left, rect.bottom, pen, (Pen) null);
    this.DrawShadowLine(gr, rect.right, rect.top, rect.right, rect.bottom, pen, (Pen) null);
    pen.Dispose();
    return true;
  }

  internal new bool DrawRectObject(Graphics gr, int FrameNo)
  {
    int paraFrameId = this.e.frame[FrameNo].ParaFrameId;
    if (!this.e.frame[FrameNo].empty && (this.e.ParaFrame[paraFrameId].flags & 512 /*0x0200*/) != 0)
    {
      int pict = this.e.ParaFrame[paraFrameId].pict;
      if (pict > 0 && pict < this.e.TotalFonts && this.e.TerFont[pict].PictType == 11)
      {
        this.e.TerFont[pict].PictX = this.e.frame[FrameNo].x - this.e.TerWinOrgX;
        this.e.TerFont[pict].PictY = this.e.frame[FrameNo].y - this.e.TerWinOrgY;
        this.e.TerFont[pict].DispFrame = FrameNo;
      }
      COp.RECT rect;
      rect.left = this.e.frame[FrameNo].x;
      rect.right = rect.left + this.e.frame[FrameNo].width;
      if (rect.right == rect.left)
        ++rect.right;
      rect.top = this.e.frame[FrameNo].y;
      rect.bottom = rect.top + this.e.frame[FrameNo].height;
      if (rect.bottom == rect.top)
        ++rect.bottom;
      this.e.CurFmtId = 0;
      Color curForeColor = this.e.CurForeColor;
      Color curBackColor = this.e.CurBackColor;
      this.e.CurForeColor = tc.CLR_WHITE;
      this.e.CurBackColor = this.e.ParaFrame[paraFrameId].BackColor;
      if (!this.e.InPrinting && !this.e.InPrintPreview)
        this.TextSegmentExists(rect.left, rect.top, rect);
      this.e.CurForeColor = curForeColor;
      this.e.CurBackColor = curBackColor;
      Color BrushColor = this.e.ParaFrame[paraFrameId].BackColor;
      if (this.e.ParaFrame[paraFrameId].FillPattern >= 2 && this.e.ParaFrame[paraFrameId].FillPattern <= 13)
      {
        int num = new int[12]
        {
          5,
          10,
          20,
          25,
          30,
          40,
          50,
          60,
          70,
          75,
          80 /*0x50*/,
          90
        }[this.e.ParaFrame[paraFrameId].FillPattern - 2];
        BrushColor = this.ToColor((int) BrushColor.R * (100 - num) / 100, (int) BrushColor.G * (100 - num) / 100, (int) BrushColor.B * (100 - num) / 100);
      }
      if (this.e.ParaFrame[paraFrameId].ShapeType == 3)
      {
        bool fill = this.e.ParaFrame[paraFrameId].FillPattern > 0 && BrushColor != this.PageColor();
        int width = !this.e.InPrinting ? this.TwipsToScrX(this.e.ParaFrame[paraFrameId].LineWdth) : this.TwipsToUnitX(this.e.ParaFrame[paraFrameId].LineWdth);
        if (this.e.InPrintPreview && width < this.ScrToUnitX(1))
          width = this.ScrToUnitX(1);
        DashStyle dashStyle = DashStyle.Solid;
        if ((this.e.ParaFrame[paraFrameId].flags & 2048 /*0x0800*/) != 0)
        {
          dashStyle = DashStyle.Dot;
          width = 1;
        }
        this.OurEllipse(gr, fill, BrushColor, new Pen(this.e.ParaFrame[paraFrameId].LineColor, (float) width)
        {
          DashStyle = dashStyle
        }, rect);
      }
      else
      {
        if (this.e.ParaFrame[paraFrameId].FillPattern > 0 && BrushColor != this.PageColor())
          this.OurFillRectangle(gr, BrushColor, rect);
        if ((this.e.ParaFrame[paraFrameId].flags & 8388608 /*0x800000*/) != 0 && this.e.ParaFrame[paraFrameId].FillPict > 0)
          this.PictOut(gr, rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top, this.e.ParaFrame[paraFrameId].FillPict, 1, -1, true);
        if (this.e.ParaFrame[paraFrameId].LineWdth > 0 && (this.e.ParaFrame[paraFrameId].flags & 1024 /*0x0400*/) != 0)
        {
          int width1 = !this.e.InPrinting ? this.TwipsToScrX(this.e.ParaFrame[paraFrameId].LineWdth) : this.TwipsToUnitX(this.e.ParaFrame[paraFrameId].LineWdth);
          if (this.e.InPrintPreview && width1 < this.ScrToUnitX(1))
            width1 = this.ScrToUnitX(1);
          if (width1 > 0)
          {
            DashStyle dashStyle = DashStyle.Solid;
            if ((this.e.ParaFrame[paraFrameId].flags & 2048 /*0x0800*/) != 0)
            {
              dashStyle = DashStyle.Dot;
              width1 = 1;
            }
            Pen SolidPen = new Pen(this.e.ParaFrame[paraFrameId].LineColor, (float) width1);
            SolidPen.DashStyle = dashStyle;
            this.DrawShadowLine(gr, rect.left, rect.top, rect.left, rect.bottom, SolidPen, (Pen) null);
            this.DrawShadowLine(gr, rect.right, rect.top, rect.right, rect.bottom, SolidPen, (Pen) null);
            SolidPen.Dispose();
          }
          int width2 = !this.e.InPrinting ? this.TwipsToScrY(this.e.ParaFrame[paraFrameId].LineWdth) : this.TwipsToUnitY(this.e.ParaFrame[paraFrameId].LineWdth);
          if (this.e.InPrintPreview && width2 < this.ScrToUnitY(1))
            width2 = this.ScrToUnitY(1);
          if (width2 > 0)
          {
            DashStyle dashStyle = DashStyle.Solid;
            if ((this.e.ParaFrame[paraFrameId].flags & 2048 /*0x0800*/) != 0)
            {
              dashStyle = DashStyle.Dot;
              width2 = 1;
            }
            Pen SolidPen = new Pen(this.e.ParaFrame[paraFrameId].LineColor, (float) width2);
            SolidPen.DashStyle = dashStyle;
            this.DrawShadowLine(gr, rect.left, rect.top, rect.right, rect.top, SolidPen, (Pen) null);
            this.DrawShadowLine(gr, rect.left, rect.bottom, rect.right, rect.bottom, SolidPen, (Pen) null);
            SolidPen.Dispose();
          }
        }
      }
    }
    return true;
  }

  internal new void DrawRuler(bool CanPostpone)
  {
    if ((this.e.TerFlags5 & 67108864 /*0x04000000*/) != 0)
    {
      this.OldDrawRuler(CanPostpone);
    }
    else
    {
      if (!this.e.TerArg.ruler)
        return;
      if (!this.e.CaretEngaged && !this.e.RepaintRuler && this.e.MessageId != 276 && this.e.HilightType == 0)
      {
        this.e.RulerPending = true;
      }
      else
      {
        COp.MSG msg;
        if (CanPostpone && this.e.RulerLineX == this.e.text[this.e.CurLine].x && (this.PeekMessage(out msg, this.e.hTerWnd, 258, 258, 2) || this.PeekMessage(out msg, this.e.hTerWnd, 256 /*0x0100*/, 256 /*0x0100*/, 2)))
        {
          this.e.RulerPending = true;
        }
        else
        {
          this.e.RulerPending = this.e.RepaintRuler = false;
          this.e.RulerSection = this.GetSection(this.e.CurLine);
          this.e.RulerLineX = this.e.text[this.e.CurLine].x;
          if (this.e.HilightType != 0)
            this.e.CurPfmt = this.e.text[this.e.HilightBegRow].pfmt;
          else
            this.e.CurPfmt = this.e.text[this.e.CurLine].pfmt;
          this.DeleteDragObjects(5, 8);
          int scrResX = this.e.ScrResX;
          int section = this.GetSection(this.e.CurLine);
          int num1 = !this.e.BorderShowing ? (int) ((double) scrResX * ((double) this.e.TerSect1[section].PgWidth - (double) this.e.TerSect[section].LeftMargin - (double) this.e.TerSect[section].RightMargin)) : (int) ((double) scrResX * (double) this.e.TerSect1[section].PgWidth) + 2 * this.UnitToScrX(this.e.LeftBorderWidth);
          if (num1 < this.e.TerWinOrgX + this.e.TerWinWidth)
            num1 = this.e.TerWinOrgX + this.e.TerWinWidth;
          int FrameNo = this.frm.GetFrame(this.e.CurLine);
          if (FrameNo < 0)
            FrameNo = 0;
          this.e.CurFrame = FrameNo;
          int num2 = this.e.frame[FrameNo].x + this.e.frame[FrameNo].SpaceLeft;
          this.TerResetClipRgn();
          COp.RECT rect1 = new COp.RECT();
          rect1.left = this.e.TerWinOrgX + (this.e.RulerRect.left - this.e.TerWinRect.left);
          rect1.right = this.e.TerWinOrgX + this.e.TerWinWidth + (this.e.RulerRect.right - this.e.TerWinRect.right);
          int width = rect1.right - rect1.left;
          rect1.top = 0;
          rect1.bottom = this.e.RulerRect.bottom - this.e.RulerRect.top;
          int num3 = this.e.TerWinOrgY + this.e.RulerRect.top - this.e.TerWinRect.top;
          Bitmap bitmap = new Bitmap(rect1.right - rect1.left + 1, rect1.bottom - rect1.top + 1, this.e.TerGr);
          Graphics gr = Graphics.FromImage((Image) bitmap);
          this.ResetTransform(gr);
          gr.TranslateTransform((float) -rect1.left, 0.0f);
          this.SetTextColor(gr, Color.Black);
          this.SetBkMode(gr, 1);
          Pen black = Pens.Black;
          Pen white = Pens.White;
          this.OurSetFont(gr, this.e.hRulerFont);
          Brush brush1 = !this.IsSameColor(this.e.TextDefBkColor, tc.CLR_WHITE) ? (Brush) new SolidBrush(this.e.TextDefBkColor) : Brushes.White;
          gr.FillRectangle(brush1, new Rectangle(rect1.left, 0, rect1.right + 2 - rect1.left, rect1.bottom + 1 - rect1.top));
          if (!this.IsSameColor(this.e.TextDefBkColor, tc.CLR_WHITE))
            brush1.Dispose();
          Brush brush2 = this.e.ToolbarBrush != null ? this.e.ToolbarBrush : Brushes.LightGray;
          int num4 = this.e.RulerFontHeight / 2;
          COp.RECT rect2 = rect1;
          rect2.bottom = rect2.top + num4;
          gr.FillRectangle(brush2, new Rectangle(rect2.left, rect2.top, rect2.right + 2 - rect2.left, rect2.bottom + 1 - rect2.top));
          rect2 = rect1;
          rect2.top = rect2.bottom - num4;
          gr.FillRectangle(brush2, new Rectangle(rect2.left, rect2.top, rect2.right + 2 - rect2.left, rect2.bottom + 1 - rect2.top));
          Brush gray = Brushes.Gray;
          rect2 = rect1;
          rect2.top += num4;
          rect2.bottom -= num4;
          rect2.right = num2;
          gr.FillRectangle(gray, new Rectangle(rect2.left, rect2.top, rect2.right + 2 - rect2.left, rect2.bottom + 1 - rect2.top));
          rect2.right = rect1.right;
          rect2.left = num2 + this.TerWrapWidth(this.e.CurLine, -1);
          gr.FillRectangle(gray, new Rectangle(rect2.left, rect2.top, rect2.right + 2 - rect2.left, rect2.bottom + 1 - rect2.top));
          this.DrawShadowLine(gr, rect1.left, rect1.top, rect1.right, rect1.top, black, (Pen) null);
          this.e.ScrRtl = (this.e.text[this.e.CurLine].flags2 & 256 /*0x0100*/) != 0;
          this.e.pScrSeg = (tc.StrLineSeg[]) null;
          int num5 = rect1.top + this.e.RulerFontHeight;
          float num6 = (float) num2 - 5f;
          float num7 = (float) scrResX;
          if ((this.e.TerFlags & 2) != 0)
            num7 = num7 * 10f / this.InchesToMm(1f);
          int y = rect1.top + num4;
          int num8 = 0;
          while ((double) num6 <= (double) num1)
          {
            int num9 = this.RoundInt((int) ((double) num6 * 10.0), 10) / 10;
            string s = num8.ToString();
            int x = num9;
            if (this.e.ScrRtl)
            {
              tc.StrLineSeg pSeg = new tc.StrLineSeg();
              x = this.RtlX(x - 5, 0, FrameNo, pSeg);
            }
            if ((double) num6 > (double) num2)
              gr.DrawString(s, this.e.RulerFont, Brushes.Black, (PointF) new Point(x, y));
            num6 += num7;
            ++num8;
          }
          float num10 = (float) scrResX / 2f;
          if ((this.e.TerFlags & 2) != 0)
            num10 = num10 * 10f / this.InchesToMm(1f);
          float num11 = (float) num2 + num10;
          int num12 = num5 - this.e.RulerFontHeight / 3;
          int num13 = 0;
          for (; (double) num11 < (double) num1; num11 += num10)
          {
            ++num13;
            if (num13 % 2 != 0)
            {
              int num14 = this.RoundInt((int) ((double) num11 * 10.0), 10) / 10;
              this.DrawShadowLine(gr, num14, num12 + 2, num14, num12 + 6, black, white);
            }
          }
          float num15 = (float) scrResX / 8f;
          if ((this.e.TerFlags & 2) != 0)
            num15 = num15 * 20f / this.InchesToMm(1f);
          float num16 = (float) num2 + num15;
          int num17 = num5 - this.e.RulerFontHeight / 3;
          int num18 = 0;
          for (; (double) num16 < (double) num1; num16 += num15)
          {
            ++num18;
            if (num18 % 4 != 0)
            {
              int num19 = this.RoundInt((int) ((double) num16 * 10.0), 10) / 10;
              this.DrawShadowLine(gr, num19, num17 + 3, num19, num17 + 5, black, white);
            }
          }
          tc.StrTab strTab = this.e.TerTab[!this.e.EditingParaStyle ? this.e.PfmtId[this.e.CurPfmt].TabId : this.e.StyleId[this.e.CurSID].TabId];
          for (int index = 0; index < strTab.count; ++index)
          {
            int num20 = num4 - 2;
            int num21 = this.TwipsToScrX(strTab.pos[index]) + num2;
            int num22 = rect1.bottom - num4;
            this.DrawShadowLine(gr, num21, num5 + 1, num21, num22, black, black);
            int dragObjectSlot;
            if ((this.e.CurSID < 0 || this.e.EditingParaStyle) && (dragObjectSlot = this.GetDragObjectSlot()) >= 0)
            {
              int num23 = this.e.ScrRtl ? this.RtlX(num21, 0, FrameNo, new tc.StrLineSeg()) : num21;
              this.e.DragObj[dragObjectSlot].InUse = true;
              this.e.DragObj[dragObjectSlot].type = 8;
              this.e.DragObj[dragObjectSlot].id1 = index;
              this.e.DragObj[dragObjectSlot].HotRectCount = 1;
              this.e.DragObj[dragObjectSlot].HotRect[0].left = num23 - this.TwipsToScrX(60);
              this.e.DragObj[dragObjectSlot].HotRect[0].top = num3 + num5;
              this.e.DragObj[dragObjectSlot].HotRect[0].right = num23 + this.TwipsToScrX(60);
              this.e.DragObj[dragObjectSlot].HotRect[0].bottom = num3 + num5 + this.e.RulerFontHeight + 1;
              this.e.DragObj[dragObjectSlot].ObjPointCount = 2;
              this.e.DragObj[dragObjectSlot].ObjPoint[0].X = num23;
              this.e.DragObj[dragObjectSlot].ObjPoint[0].Y = num3 + num5;
              this.e.DragObj[dragObjectSlot].ObjPoint[1].X = num23;
              this.e.DragObj[dragObjectSlot].ObjPoint[1].Y = num3 + num5 + this.e.RulerFontHeight;
            }
            int x1 = num21 + 1;
            if (strTab.type[index] == 0)
              this.DrawShadowLine(gr, x1 - 1, num22, x1 + num20 + 1, num22, black, black);
            else if (strTab.type[index] == 1)
              this.DrawShadowLine(gr, x1, num22, x1 - num20 - 1, num22, black, black);
            else
              this.DrawShadowLine(gr, x1 - num20, num22, x1 + num20, num22, black, black);
            if (strTab.type[index] == 3)
            {
              int num24 = (num5 + num22) / 2 + 1;
              this.DrawShadowLine(gr, x1 + num20 / 2, num24, x1 + num20 / 2 + 2, num24, black, black);
            }
          }
          int x2 = (((strTab.count != 0 ? strTab.pos[strTab.count - 1] : 0) + 60) / this.e.DefTabWidth + 1) * this.e.DefTabWidth;
          do
          {
            int num25 = this.TwipsToScrX(x2) + num2;
            if (num25 < num1)
            {
              int num26 = rect1.bottom - num4;
              this.DrawShadowLine(gr, num25, num26 + 2, num25, num26 + 5, white, (Pen) null);
              x2 += this.e.DefTabWidth;
            }
            else
              break;
          }
          while (x2 >= 0);
          if (!this.e.TerArg.BorderMargin && (!this.e.ShowPageBorder || this.e.TerArg.FittedView))
          {
            Point[] points = new Point[3];
            points[0].X = !this.e.EditingParaStyle ? this.e.PfmtId[this.e.CurPfmt].LeftIndent : this.TwipsToScrX(this.e.StyleId[this.e.CurSID].LeftIndentTwips);
            points[0].X += num2;
            points[0].Y = num5;
            points[1].X = points[0].X;
            points[1].Y = num5 + this.e.RulerFontHeight;
            points[2].X = points[0].X + this.e.RulerFontHeight / 2;
            points[2].Y = num5 + this.e.RulerFontHeight / 2;
            if (this.e.ScrRtl)
            {
              for (int index = 0; index < 3; ++index)
              {
                tc.StrLineSeg pSeg = new tc.StrLineSeg();
                points[index].X = this.RtlX(points[index].X, 0, FrameNo, pSeg);
              }
            }
            gr.FillPolygon(Brushes.LightGray, points);
            gr.DrawPolygon(Pens.Black, points);
            int dragObjectSlot1;
            if ((this.e.CurSID < 0 || this.e.EditingParaStyle) && (dragObjectSlot1 = this.GetDragObjectSlot()) >= 0)
            {
              this.e.DragObj[dragObjectSlot1].InUse = true;
              this.e.DragObj[dragObjectSlot1].type = 5;
              this.e.DragObj[dragObjectSlot1].id1 = this.e.CurPfmt;
              this.e.DragObj[dragObjectSlot1].HotRectCount = 1;
              this.e.DragObj[dragObjectSlot1].HotRect[0].left = points[0].X;
              this.e.DragObj[dragObjectSlot1].HotRect[0].top = num3 + points[0].Y;
              this.e.DragObj[dragObjectSlot1].HotRect[0].right = points[2].X;
              if (this.e.ScrRtl)
                this.SwapInts(ref this.e.DragObj[dragObjectSlot1].HotRect[0].left, ref this.e.DragObj[dragObjectSlot1].HotRect[0].right);
              this.e.DragObj[dragObjectSlot1].HotRect[0].bottom = num3 + points[1].Y;
              this.e.DragObj[dragObjectSlot1].ObjPointCount = 3;
              for (int index = 0; index < 3; ++index)
                this.e.DragObj[dragObjectSlot1].ObjPoint[index] = points[index];
            }
            points[0].X = !this.e.EditingParaStyle ? this.e.PfmtId[this.e.CurPfmt].LeftIndent + this.e.PfmtId[this.e.CurPfmt].FirstIndent : this.TwipsToScrX(this.e.StyleId[this.e.CurSID].LeftIndentTwips + this.e.StyleId[this.e.CurSID].FirstIndentTwips);
            points[0].X += num2;
            points[0].Y = num5 - this.e.RulerFontHeight;
            points[1].X = points[0].X;
            points[1].Y = num5;
            points[2].X = points[0].X + this.e.RulerFontHeight / 2;
            points[2].Y = num5 - this.e.RulerFontHeight / 2;
            if (this.e.ScrRtl)
            {
              for (int index = 0; index < 3; ++index)
              {
                tc.StrLineSeg pSeg = new tc.StrLineSeg();
                points[index].X = this.RtlX(points[index].X, 0, FrameNo, pSeg);
              }
            }
            gr.FillPolygon(Brushes.LightGray, points);
            gr.DrawPolygon(Pens.Black, points);
            int dragObjectSlot2;
            if ((this.e.CurSID < 0 || this.e.EditingParaStyle) && (dragObjectSlot2 = this.GetDragObjectSlot()) >= 0)
            {
              this.e.DragObj[dragObjectSlot2].InUse = true;
              this.e.DragObj[dragObjectSlot2].type = 7;
              this.e.DragObj[dragObjectSlot2].id1 = this.e.CurPfmt;
              this.e.DragObj[dragObjectSlot2].HotRectCount = 1;
              this.e.DragObj[dragObjectSlot2].HotRect[0].left = points[0].X;
              this.e.DragObj[dragObjectSlot2].HotRect[0].top = num3 + points[0].Y;
              this.e.DragObj[dragObjectSlot2].HotRect[0].right = points[2].X;
              if (this.e.ScrRtl)
                this.SwapInts(ref this.e.DragObj[dragObjectSlot2].HotRect[0].left, ref this.e.DragObj[dragObjectSlot2].HotRect[0].right);
              this.e.DragObj[dragObjectSlot2].HotRect[0].bottom = num3 + points[1].Y;
              this.e.DragObj[dragObjectSlot2].ObjPointCount = 3;
              for (int index = 0; index < 3; ++index)
                this.e.DragObj[dragObjectSlot2].ObjPoint[index] = points[index];
            }
            points[0].X = !this.e.EditingParaStyle ? this.TerWrapWidth(this.e.CurLine, -1) - this.e.PfmtId[this.e.CurPfmt].RightIndent : this.TerWrapWidth(this.e.CurLine, -1) - this.TwipsToScrX(this.e.StyleId[this.e.CurSID].RightIndentTwips);
            points[0].X += num2;
            points[0].Y = num5;
            points[1].X = points[0].X;
            points[1].Y = num5 + this.e.RulerFontHeight;
            points[2].X = points[0].X - this.e.RulerFontHeight / 2;
            points[2].Y = num5 + this.e.RulerFontHeight / 2;
            if (this.e.ScrRtl)
            {
              for (int index = 0; index < 3; ++index)
              {
                tc.StrLineSeg pSeg = new tc.StrLineSeg();
                points[index].X = this.RtlX(points[index].X, 0, FrameNo, pSeg);
              }
            }
            gr.FillPolygon(Brushes.LightGray, points);
            gr.DrawPolygon(Pens.Black, points);
            int dragObjectSlot3;
            if ((this.e.CurSID < 0 || this.e.EditingParaStyle) && (dragObjectSlot3 = this.GetDragObjectSlot()) >= 0)
            {
              this.e.DragObj[dragObjectSlot3].InUse = true;
              this.e.DragObj[dragObjectSlot3].type = 6;
              this.e.DragObj[dragObjectSlot3].id1 = this.e.CurPfmt;
              this.e.DragObj[dragObjectSlot3].HotRectCount = 1;
              this.e.DragObj[dragObjectSlot3].HotRect[0].left = points[2].X;
              this.e.DragObj[dragObjectSlot3].HotRect[0].top = num3 + points[0].Y;
              this.e.DragObj[dragObjectSlot3].HotRect[0].right = points[0].X;
              if (this.e.ScrRtl)
                this.SwapInts(ref this.e.DragObj[dragObjectSlot3].HotRect[0].left, ref this.e.DragObj[dragObjectSlot3].HotRect[0].right);
              this.e.DragObj[dragObjectSlot3].HotRect[0].bottom = num3 + points[1].Y;
              this.e.DragObj[dragObjectSlot3].ObjPointCount = 3;
              for (int index = 0; index < 3; ++index)
                this.e.DragObj[dragObjectSlot3].ObjPoint[index] = points[index];
            }
          }
          else
          {
            Point[] points = new Point[5];
            int num27 = num4 / 2 + 1;
            points[0].X = !this.e.EditingParaStyle ? this.e.PfmtId[this.e.CurPfmt].LeftIndent : this.TwipsToScrX(this.e.StyleId[this.e.CurSID].LeftIndentTwips);
            points[0].X += num2;
            points[0].Y = num5;
            points[1].X = points[0].X - num27;
            points[1].Y = points[0].Y + num27;
            points[2].X = points[1].X;
            points[2].Y = rect1.bottom - num4;
            points[3].X = points[0].X + num27;
            points[3].Y = points[2].Y;
            points[4].X = points[3].X;
            points[4].Y = points[1].Y;
            if (this.e.ScrRtl)
            {
              for (int index = 0; index < 5; ++index)
              {
                tc.StrLineSeg pSeg = new tc.StrLineSeg();
                points[index].X = this.RtlX(points[index].X, 0, FrameNo, pSeg);
              }
            }
            gr.FillPolygon(Brushes.LightGray, points);
            gr.DrawPolygon(Pens.Black, points);
            int dragObjectSlot4;
            if ((this.e.CurSID < 0 || this.e.EditingParaStyle) && (dragObjectSlot4 = this.GetDragObjectSlot()) >= 0)
            {
              this.e.DragObj[dragObjectSlot4].InUse = true;
              this.e.DragObj[dragObjectSlot4].type = 5;
              this.e.DragObj[dragObjectSlot4].id1 = this.e.CurPfmt;
              this.e.DragObj[dragObjectSlot4].HotRectCount = 1;
              this.e.DragObj[dragObjectSlot4].HotRect[0].left = points[0].X - num27;
              this.e.DragObj[dragObjectSlot4].HotRect[0].top = num3 + points[0].Y;
              this.e.DragObj[dragObjectSlot4].HotRect[0].right = points[0].X + num27;
              this.e.DragObj[dragObjectSlot4].HotRect[0].bottom = num3 + points[2].Y;
              this.e.DragObj[dragObjectSlot4].ObjPointCount = 5;
              for (int index = 0; index < 5; ++index)
                this.e.DragObj[dragObjectSlot4].ObjPoint[index] = points[index];
            }
            points[0].X = !this.e.EditingParaStyle ? this.e.PfmtId[this.e.CurPfmt].LeftIndent + this.e.PfmtId[this.e.CurPfmt].FirstIndent : this.TwipsToScrX(this.e.StyleId[this.e.CurSID].LeftIndentTwips + this.e.StyleId[this.e.CurSID].FirstIndentTwips);
            points[0].X += num2;
            points[0].Y = num5;
            points[1].X = points[0].X - num27;
            points[1].Y = points[0].Y - num27;
            points[2].X = points[1].X;
            points[2].Y = rect1.top + num4;
            points[3].X = points[0].X + num27;
            points[3].Y = points[2].Y;
            points[4].X = points[3].X;
            points[4].Y = points[1].Y;
            if (this.e.ScrRtl)
            {
              for (int index = 0; index < 5; ++index)
              {
                tc.StrLineSeg pSeg = new tc.StrLineSeg();
                points[index].X = this.RtlX(points[index].X, 0, FrameNo, pSeg);
              }
            }
            gr.FillPolygon(Brushes.LightGray, points);
            gr.DrawPolygon(Pens.Black, points);
            int dragObjectSlot5;
            if ((this.e.CurSID < 0 || this.e.EditingParaStyle) && (dragObjectSlot5 = this.GetDragObjectSlot()) >= 0)
            {
              this.e.DragObj[dragObjectSlot5].InUse = true;
              this.e.DragObj[dragObjectSlot5].type = 7;
              this.e.DragObj[dragObjectSlot5].id1 = this.e.CurPfmt;
              this.e.DragObj[dragObjectSlot5].HotRectCount = 1;
              this.e.DragObj[dragObjectSlot5].HotRect[0].left = points[0].X - num27;
              this.e.DragObj[dragObjectSlot5].HotRect[0].top = num3 + points[2].Y;
              this.e.DragObj[dragObjectSlot5].HotRect[0].right = points[0].X + num27;
              this.e.DragObj[dragObjectSlot5].HotRect[0].bottom = num3 + points[0].Y;
              this.e.DragObj[dragObjectSlot5].ObjPointCount = 5;
              for (int index = 0; index < 5; ++index)
                this.e.DragObj[dragObjectSlot5].ObjPoint[index] = points[index];
            }
            points[0].X = !this.e.EditingParaStyle ? this.TerWrapWidth(this.e.CurLine, -1) - this.e.PfmtId[this.e.CurPfmt].RightIndent : this.TerWrapWidth(this.e.CurLine, -1) - this.TwipsToScrX(this.e.StyleId[this.e.CurSID].RightIndentTwips);
            points[0].X += num2;
            points[0].Y = num5;
            points[1].X = points[0].X - num27;
            points[1].Y = points[0].Y + num27;
            points[2].X = points[1].X;
            points[2].Y = rect1.bottom - num4;
            points[3].X = points[0].X + num27;
            points[3].Y = points[2].Y;
            points[4].X = points[3].X;
            points[4].Y = points[1].Y;
            if (this.e.ScrRtl)
            {
              for (int index = 0; index < 5; ++index)
              {
                tc.StrLineSeg pSeg = new tc.StrLineSeg();
                points[index].X = this.RtlX(points[index].X, 0, FrameNo, pSeg);
              }
            }
            gr.FillPolygon(Brushes.LightGray, points);
            gr.DrawPolygon(Pens.Black, points);
            int dragObjectSlot6;
            if ((this.e.CurSID < 0 || this.e.EditingParaStyle) && (dragObjectSlot6 = this.GetDragObjectSlot()) >= 0)
            {
              this.e.DragObj[dragObjectSlot6].InUse = true;
              this.e.DragObj[dragObjectSlot6].type = 6;
              this.e.DragObj[dragObjectSlot6].id1 = this.e.CurPfmt;
              this.e.DragObj[dragObjectSlot6].HotRectCount = 1;
              this.e.DragObj[dragObjectSlot6].HotRect[0].left = points[0].X - num27;
              this.e.DragObj[dragObjectSlot6].HotRect[0].top = num3 + points[0].Y;
              this.e.DragObj[dragObjectSlot6].HotRect[0].right = points[0].X + num27;
              this.e.DragObj[dragObjectSlot6].HotRect[0].bottom = num3 + points[2].Y;
              this.e.DragObj[dragObjectSlot6].ObjPointCount = 5;
              for (int index = 0; index < 5; ++index)
                this.e.DragObj[dragObjectSlot6].ObjPoint[index] = points[index];
            }
          }
          this.SetBkMode(gr, 2);
          this.e.TerGr.DrawImage((Image) bitmap, new Rectangle(rect1.left, this.e.TerWinOrgY + rect1.top - (this.e.TerWinRect.top - this.e.RulerRect.top), width, rect1.bottom - rect1.top), new Rectangle(0, rect1.top, width, rect1.bottom - rect1.top), GraphicsUnit.Pixel);
          gr.Dispose();
          bitmap.Dispose();
          this.TerResetClipRgn();
          this.e.ScrRtl = false;
        }
      }
    }
  }

  internal void DrawScrPicts(Graphics gr)
  {
    for (int index = 0; index < this.e.TotalPendPicts; ++index)
    {
      int pict = this.e.PendPict[index].pict;
      int num = this.e.PendPict[index].width * this.e.PendPict[index].count;
      Bitmap bitmap = new Bitmap(num, this.e.PendPict[index].height, this.e.TerGr);
      Graphics gr1 = Graphics.FromImage((Image) bitmap);
      int CurY;
      int CurX = CurY = 0;
      if (this.IsControl(pict))
      {
        CurX = this.e.PendPict[index].x;
        CurY = this.e.PendPict[index].y;
      }
      if (this.e.TerFont[pict].ImageType == ImageFormat.Wmf.Guid || this.e.TerFont[pict].ImageType == ImageFormat.Emf.Guid)
      {
        using (SolidBrush solidBrush = new SolidBrush(this.PageColor()))
          gr1.FillRectangle((Brush) solidBrush, new Rectangle(0, 0, num, this.e.PendPict[index].height));
      }
      this.PictOut(gr1, CurX, CurY, this.e.PendPict[index].width, this.e.PendPict[index].height, pict, this.e.PendPict[index].count, this.e.PendPict[index].LineNo, true);
      if (!this.IsControl(pict))
      {
        IntPtr hgdiObj = COp.Win32.SelectObject(this.e.hMemDC, bitmap.GetHbitmap());
        COp.Win32.BitBlt(this.e.hBufDC, this.e.PendPict[index].x, this.e.PendPict[index].y, num, this.e.PendPict[index].height, this.e.hMemDC, 0, 0, 13369376);
        COp.Win32.SelectObject(this.e.hMemDC, hgdiObj);
        COp.Win32.DeleteObject(hgdiObj);
        gr1.Dispose();
        bitmap.Dispose();
      }
    }
  }

  internal new bool DrawSectLineNbr(Graphics gr, int row, int x, int y, int BaseHeight)
  {
    int index1 = 0;
    int num1 = 0;
    int mode = 0;
    if (tc.DebugMode)
      this.misc.dm(nameof (DrawSectLineNbr));
    int sect = this.e.frame[this.e.CurFrame].sect;
    int num2 = this.e.SectLine + row + 1;
    int lineStep = this.e.TerSect[sect].LineStep;
    if (num2 % lineStep <= 0)
    {
      string str = num2.ToString();
      int options = !this.IsDefLangRtl() ? 0 : 128 /*0x80*/;
      this.SetFont(gr, (ushort) index1, 'T');
      int length = str.Length;
      for (int index2 = 0; index2 < length; ++index2)
        num1 += this.fnt.LwrCharWidth(index1, !this.e.InPrinting, str[index2]);
      x -= this.e.InPrinting ? this.TwipsToUnitX(360) : this.TwipsToScrX(360);
      x -= num1;
      y = !this.e.InPrinting ? y + BaseHeight - this.e.TerFont[index1].BaseHeight + this.e.TerFont[index1].BaseHeightAdj : y + BaseHeight - this.e.PrtFont[index1].BaseHeight + this.e.PrtFont[index1].BaseHeightAdj;
      y += this.e.ScrYOffset;
      if (this.e.InPrinting)
        mode = this.SetBkMode(gr, 1);
      if (this.e.ScrRtl && this.e.pScrSeg != null)
      {
        COp.SIZE size;
        this.GetTextExtentPoint(gr, this.e.TerCurFont, str, str.Length, out size);
        x = this.RtlX(x, size.cx, this.e.CurFrame, new tc.StrLineSeg());
      }
      this.ExtTextOut(gr, x, y, options, str.ToCharArray(), length, (int[]) null);
      if (this.e.InPrinting)
        this.SetBkMode(gr, mode);
    }
    return true;
  }

  internal new bool DrawShadowBox(
    Graphics gr,
    int x1,
    int y1,
    int x2,
    int y2,
    Pen hSolidPen,
    Pen hShadowPen)
  {
    this.DrawShadowLine(gr, x1, y1, x2, y1, hSolidPen, (Pen) null);
    this.DrawShadowLine(gr, x1, y2, x2, y2, hShadowPen, (Pen) null);
    this.DrawShadowLine(gr, x1, y1, x1, y2, hSolidPen, (Pen) null);
    this.DrawShadowLine(gr, x2, y1, x2, y2, hShadowPen, (Pen) null);
    return true;
  }

  internal new bool DrawShadowLine(
    Graphics gr,
    int x1,
    int y1,
    int x2,
    int y2,
    Pen SolidPen,
    Pen ShadowPen)
  {
    if (this.OpDCIsOpen(gr))
      return this.DrawShadowLine32(gr, x1, y1, x2, y2, SolidPen, ShadowPen);
    this.OurDrawLine(gr, SolidPen, x1, y1, x2, y2);
    if (ShadowPen != null)
    {
      if (SolidPen == ShadowPen)
      {
        if (x1 == x2)
          x1 = x2 = x1 + 1;
        if (y1 == y2)
          y1 = y2 = y1 + 1;
      }
      else
      {
        ++x1;
        ++x2;
        ++y1;
        ++y2;
      }
      this.OurDrawLine(gr, ShadowPen, x1, y1, x2, y2);
    }
    return true;
  }

  internal bool DrawShadowLine32(
    Graphics gr,
    int x1,
    int y1,
    int x2,
    int y2,
    Pen SolidPen,
    Pen ShadowPen)
  {
    IntPtr handle1 = IntPtr.Zero;
    IntPtr opDc = this.GetOpDC(gr);
    IntPtr pen = this.CreatePen(SolidPen);
    IntPtr handle2 = this.SelectObject(opDc, pen);
    this.OurMoveToEx(opDc, x1, y1, IntPtr.Zero);
    this.OurLineTo(opDc, x2, y2);
    if (ShadowPen != null)
    {
      handle1 = this.CreatePen(ShadowPen);
      this.SelectObject(opDc, handle1);
      ++x1;
      ++x2;
      ++y1;
      ++y2;
      this.OurMoveToEx(opDc, x1, y1, IntPtr.Zero);
      this.OurLineTo(opDc, x2, y2);
    }
    this.SelectObject(opDc, handle2);
    this.DeleteObject(pen);
    if (IntPtr.Zero != handle1)
      this.DeleteObject(handle1);
    return true;
  }

  internal new bool DrawTrackingLine(Graphics gr, int x, int y, int height)
  {
    int x1 = 45;
    if (tc.DebugMode)
      this.misc.dm(nameof (DrawTrackingLine));
    if (!this.e.InPrinting && !this.e.BorderShowing)
      x1 = 0;
    x -= this.e.InPrinting ? this.TwipsToUnitX(x1) : this.TwipsToScrX(x1);
    this.DrawShadowLine(gr, x, y, x, y + height, Pens.Black, (Pen) null);
    return true;
  }

  internal new bool DrawWheel(Graphics gr)
  {
    int num1 = 14;
    Point[] points = new Point[3];
    int num2 = this.e.WheelX - this.e.TerWinRect.left + this.e.TerWinOrgX;
    int num3 = this.e.WheelY - this.e.TerWinRect.top + this.e.TerWinOrgY;
    Color color1 = this.ToColor(8355711 /*0x7F7F7F*/);
    if (color1 == this.PageColor())
      color1 = Color.White;
    Color color2 = this.PageColor();
    Pen pen = new Pen(color1);
    using (SolidBrush solidBrush = new SolidBrush(color2))
      gr.FillEllipse((Brush) solidBrush, num2 - num1, num3 - num1, 2 * num1, 2 * num1);
    gr.DrawEllipse(pen, num2 - num1, num3 - num1, 2 * num1, 2 * num1);
    pen.Dispose();
    gr.FillEllipse((Brush) new SolidBrush(color1), num2 - 2, num3 - 2, 5, 5);
    points[0].X = num2;
    points[0].Y = num3 - (num1 - 2);
    points[1].X = num2 - (num1 - 6);
    points[1].Y = num3 - 4;
    points[2].X = num2 + (num1 - 6);
    points[2].Y = num3 - 4;
    using (SolidBrush solidBrush = new SolidBrush(color1))
      gr.FillPolygon((Brush) solidBrush, points);
    points[0].X = num2;
    points[0].Y = num3 + (num1 - 3);
    points[1].X = num2 - (num1 - 7);
    points[1].Y = num3 + 4;
    points[2].X = num2 + (num1 - 7);
    points[2].Y = num3 + 4;
    using (SolidBrush solidBrush = new SolidBrush(color1))
      gr.FillPolygon((Brush) solidBrush, points);
    return true;
  }

  internal new bool DrawWigglyLine(Graphics gr, int x1, int y1, int x2, int y2, Color color)
  {
    int num1 = 0;
    bool flag = true;
    if (this.e.ScrFrameAngle != 0 || this.e.ScrRtl)
    {
      if (this.e.ScrRtl)
      {
        x1 = this.RtlX(x1, 0, this.e.CurFrame, this.e.pScrSeg != null ? this.e.pScrSeg[this.e.CurScrSeg] : new tc.StrLineSeg());
        x2 = this.RtlX(x2, 0, this.e.CurFrame, this.e.pScrSeg != null ? this.e.pScrSeg[this.e.CurScrSeg] : new tc.StrLineSeg());
      }
      if (this.e.ScrFrameAngle != 0)
      {
        int num2 = this.FrameRotateX(x1, y1, this.e.CurFrame);
        int num3 = this.FrameRotateY(x1, y1, this.e.CurFrame);
        x1 = num2;
        y1 = num3;
        int num4 = this.FrameRotateX(x2, y2, this.e.CurFrame);
        int num5 = this.FrameRotateY(x2, y2, this.e.CurFrame);
        x2 = num4;
        y2 = num5;
      }
    }
    if (y1 != y2)
      flag = false;
    if (flag)
    {
      if (x1 > x2)
        this.SwapInts(ref x1, ref x2);
      int num6 = 0;
      int x = x1;
      while (x <= x2)
      {
        this.SetPixel(gr, x, y1 + num6, color);
        num6 = num1 % 4;
        switch (num6)
        {
          case 2:
            num6 = 0;
            break;
          case 3:
            num6 = -1;
            break;
        }
        ++x;
        ++num1;
      }
    }
    else
    {
      if (y1 > y2)
        this.SwapInts(ref y1, ref y2);
      int num7 = 0;
      for (int y = y1; y <= y2; ++y)
      {
        this.SetPixel(gr, x1 + num7, y, color);
        num7 = num1 % 4;
        switch (num7)
        {
          case 2:
            num7 = 0;
            break;
          case 3:
            num7 = -1;
            break;
        }
      }
    }
    return true;
  }

  internal new bool FixImageDest(ref int DestX, ref int DestY, int DestWidth, int DestHeight)
  {
    if (this.e.ScrRtl && this.e.pScrSeg != null && this.e.CurScrSeg < this.e.TotalScrSeg)
    {
      int pVal1 = this.RtlX(DestX, 0, this.e.CurFrame, this.e.pScrSeg[this.e.CurScrSeg]);
      int pVal2 = this.RtlX(DestX + DestWidth, 0, this.e.CurFrame, this.e.pScrSeg[this.e.CurScrSeg]);
      if (pVal2 < pVal1)
        this.SwapInts(ref pVal1, ref pVal2);
      DestX = pVal1;
    }
    if (this.e.ScrFrameAngle != 0)
    {
      COp.RECT rect;
      rect.left = DestX;
      rect.right = DestX + DestWidth;
      rect.top = DestY;
      rect.bottom = DestY + DestHeight;
      this.FrameRotateRect(ref rect, this.e.CurFrame);
      DestX = rect.left < rect.right ? rect.left : rect.right;
      DestY = rect.top < rect.bottom ? rect.top : rect.bottom;
    }
    return true;
  }

  internal new bool FrameToDraw(int FrameNo, int PaintLine)
  {
    if (tc.DebugMode)
      this.misc.dm(nameof (FrameToDraw));
    if (this.e.PaintFlag == 2)
    {
      if (!this.e.frame[FrameNo].empty && this.TableLevel(PaintLine) == this.e.frame[FrameNo].level && PaintLine >= this.e.frame[FrameNo].ScrFirstLine && PaintLine <= this.e.frame[FrameNo].ScrLastLine)
        return true;
    }
    else
    {
      if (this.e.PaintFlag != 3)
        return true;
      for (PaintLine = this.e.PaintBegLine; PaintLine <= this.e.PaintEndLine; ++PaintLine)
      {
        if (!this.e.frame[FrameNo].empty && this.TableLevel(PaintLine) == this.e.frame[FrameNo].level && PaintLine >= this.e.frame[FrameNo].ScrFirstLine && PaintLine <= this.e.frame[FrameNo].ScrLastLine)
          return true;
      }
    }
    return false;
  }

  internal new int GetSectDisplayLine(int FrameNo)
  {
    int sectDisplayLine = -1;
    if (this.e.frame[FrameNo].empty || (this.e.frame[FrameNo].flags & 4096 /*0x1000*/) != 0 || this.e.frame[FrameNo].ParaFrameId > 0 || this.e.frame[FrameNo].CellId > 0)
      return -1;
    int pageFirstLine = this.e.frame[FrameNo].PageFirstLine;
    if (this.e.text[pageFirstLine] != null && (this.e.PfmtId[this.e.text[pageFirstLine].pfmt].flags & 12288 /*0x3000*/) != 0)
      return -1;
    int sect = this.e.frame[FrameNo].sect;
    if ((this.e.TerSect[sect].flags & 512 /*0x0200*/) == 0)
      return -1;
    int page = this.e.text[pageFirstLine].page;
    int firstLine = this.e.PageInfo[page].FirstLine;
    int section = this.GetSection(firstLine);
    for (int lin = firstLine; lin <= this.e.PageInfo[page].LastLine; ++lin)
    {
      if (this.e.text[lin].page == page && this.e.text[lin].cid <= 0 && this.e.text[lin].fid <= 0 && (this.e.PfmtId[this.e.text[lin].pfmt].flags & 12288 /*0x3000*/) == 0)
      {
        if (lin <= pageFirstLine)
        {
          if (section != sect && (this.e.TerSect[this.GetSection(lin)].flags & 512 /*0x0200*/) == 0)
            sectDisplayLine = -1;
          else
            ++sectDisplayLine;
        }
        else
          break;
      }
    }
    return sectDisplayLine;
  }

  internal new int GetStatusLineNo()
  {
    if (!this.e.TerArg.WordWrap || !this.e.TerArg.PrintView || this.e.TerArg.FittedView)
    {
      int curLine = this.e.CurLine;
      if (this.e.TerArg.FittedView && (this.e.TerFlags3 & 4) != 0)
        curLine -= this.GetStatusLineNoAdj(0, this.e.CurLine);
      return curLine + 1;
    }
    int statusLineNo = this.e.CurLine + 1 - this.e.PageInfo[this.e.CurPage].FirstLine;
    if (this.e.TerArg.PageMode)
    {
      int num1 = this.e.CurPage >= this.e.TotalPages - 1 ? this.e.TotalLines - this.e.PageInfo[this.e.CurPage].FirstLine : this.e.PageInfo[this.e.CurPage + 1].FirstLine - this.e.PageInfo[this.e.CurPage].FirstLine;
      int num2;
      int index1 = num2 = this.e.PageInfo[this.e.CurPage].FirstLine;
      if ((this.e.PfmtId[this.e.text[index1].pfmt].flags & 12288 /*0x3000*/) != 0)
      {
        int index2;
        for (index2 = index1; index2 < this.e.TotalLines && (this.e.PfmtId[this.e.text[index2].pfmt].flags & 12288 /*0x3000*/) != 0 && num1 > 1; ++index2)
          --num1;
        num2 = index2;
      }
      if (this.e.ViewPageHdrFtr)
      {
        tc.StrHdrFtr hdr;
        if (this.PageHdrSect(this.e.CurPage, out hdr) < 0)
        {
          int num3;
          hdr.LastLine = num3 = -1;
          hdr.FirstLine = num3;
        }
        int num4 = hdr.LastLine - hdr.FirstLine + 1 - 2;
        if (num4 == 1 && hdr.FirstLine >= 0 && hdr.FirstLine + 1 < this.e.TotalLines && this.e.text[hdr.FirstLine + 1].len == 1 && !this.e.EditPageHdrFtr)
          num4 = 0;
        if ((this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].flags & 4096 /*0x1000*/) != 0)
        {
          statusLineNo = 0;
          for (int curLine = this.e.CurLine; curLine >= 0 && (this.e.text[curLine].flags & 1966080 /*0x1E0000*/) == 0; --curLine)
            ++statusLineNo;
        }
        else if ((this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].flags & 8192 /*0x2000*/) != 0)
        {
          statusLineNo = num4 + num1;
          for (int curLine = this.e.CurLine; curLine >= 0 && (this.e.text[curLine].flags & 1966080 /*0x1E0000*/) == 0; --curLine)
            ++statusLineNo;
        }
        else
          statusLineNo = this.e.CurLine - num2 + num4 + 1;
      }
      else
      {
        statusLineNo = this.e.CurLine - num2 + 1;
        if ((this.e.TerFlags3 & 4) != 0)
          statusLineNo -= this.GetStatusLineNoAdj(this.e.PageInfo[this.e.CurPage].FirstLine, this.e.PageInfo[this.e.CurPage].LastLine);
      }
    }
    int firstLine = this.e.PageInfo[this.e.CurPage].FirstLine;
    bool flag = (this.e.PfmtId[this.e.text[firstLine].pfmt].flags & 12288 /*0x3000*/) != 0;
    for (; firstLine < this.e.CurLine; ++firstLine)
    {
      if (!flag || (this.e.PfmtId[this.e.text[firstLine].pfmt].flags & 12288 /*0x3000*/) == 0)
      {
        flag = false;
        if ((this.e.PfmtId[this.e.text[firstLine].pfmt].flags & 12288 /*0x3000*/) != 0)
          --statusLineNo;
      }
    }
    if (statusLineNo < 1)
      statusLineNo = 1;
    return statusLineNo;
  }

  internal new int GetStatusLineNoAdj(int FirstLine, int LastLine)
  {
    int statusLineNoAdj = 0;
    int num1 = 0;
    int num2 = 0;
    int num3 = 0;
    if (this.e.CurLine < FirstLine)
      return 0;
    for (int LineNo = FirstLine; LineNo <= LastLine; ++LineNo)
    {
      if (LineNo == this.e.CurLine)
      {
        if (this.e.text[this.e.CurLine].cid != 0)
        {
          statusLineNoAdj += num2 - num1;
          break;
        }
        break;
      }
      if (this.e.text[LineNo].cid != 0)
      {
        ++num2;
        ++num1;
      }
      if (this.LineInfo(LineNo, 16 /*0x10*/))
      {
        if (num1 > num3)
          num3 = num1;
        num1 = 0;
      }
      else if (this.LineInfo(LineNo, 32 /*0x20*/))
      {
        statusLineNoAdj += num2 - num3;
        int num4;
        num1 = num4 = 0;
        num2 = num4;
        num3 = num4;
      }
    }
    return statusLineNoAdj;
  }

  internal bool HideCtls()
  {
    bool flag1 = false;
    for (int index1 = 0; index1 < this.e.TotalScrCtls; ++index1)
    {
      if (this.e.ScrCtl[index1].InUse && this.e.ScrCtl[index1].PaintId != this.e.PaintId)
      {
        int lineNo = this.e.ScrCtl[index1].LineNo;
        int pict = this.e.ScrCtl[index1].pict;
        bool flag2 = lineNo < 0 || lineNo >= this.e.TotalLines || !this.IsLineVisible(lineNo);
        if (!flag2)
        {
          ushort[] numArray = this.OpenCfmt(lineNo);
          int len = this.e.text[lineNo].len;
          int index2 = 0;
          while (index2 < len && (int) numArray[index2] != pict)
            ++index2;
          if (index2 == len)
            flag2 = true;
          this.CloseCfmt(lineNo);
        }
        if (flag2)
        {
          if (this.e.ScrCtl[index1].ctl.Visible)
            flag1 = true;
          this.e.ScrCtl[index1].ctl.Visible = false;
          this.e.ScrCtl[index1].InUse = false;
          COp.RECT terRect = this.e.TerRect;
          if (this.e.TerTlb != null)
            terRect.top = this.e.ToolBarHeight;
          COp.Win32.ValidateRect(this.e.hTerWnd, ref terRect);
          this.TerSetClipRgn();
        }
      }
    }
    if (flag1)
      this.TerDrawBorder();
    return true;
  }

  internal new bool KillHilightTimer()
  {
    if (this.e.HilightTimerOn)
    {
      this.KillTimer(this.e.hTerWnd, 9183);
      this.e.HilightTimerOn = false;
    }
    return true;
  }

  internal new bool MouseStopAction()
  {
    string pMsg = "";
    this.KillTimer(this.e.hTerWnd, 9190);
    this.e.MouseStopTimerOn = false;
    if (this.e.MouseStopMsgOn)
      this.PaintTer();
    if (this.e.MouseStopLine >= 0)
    {
      if (this.TrackingComment(this.e.MouseStopLine, this.e.MouseStopCol, out pMsg))
        this.DrawMouseStopMsg(this.e.TerGr, pMsg);
      else if (this.InvokeTextLink(false, this.e.MouseStopLine, this.e.MouseStopCol))
        this.DrawMouseStopMsg(this.e.TerGr, this.e.MsgString[221]);
    }
    this.e.MouseStopDone = true;
    return true;
  }

  internal void OldBlastToScreen()
  {
    if (this.e.PaintFlag == 2 || this.e.PaintFlag == 3)
    {
      Rectangle rectangle = this.ToRectangle(this.e.DirtyRect);
      rectangle.X -= this.e.TerWinOrgX;
      rectangle.Y -= this.e.TerWinOrgY;
      if (this.e.BufBM == null)
        return;
      this.e.TerGr.DrawImage((Image) this.e.BufBM, this.ToRectangle(this.e.DirtyRect), rectangle, GraphicsUnit.Pixel);
    }
    else
    {
      if (this.e.PaintFlag == 1 || this.e.BufBM == null)
        return;
      this.e.TerGr.DrawImage((Image) this.e.BufBM, new Rectangle(this.e.TerWinOrgX, this.e.TerWinOrgY, this.e.TerWinWidth, this.e.TerWinHeight), new Rectangle(0, 0, this.e.TerWinWidth, this.e.TerWinHeight), GraphicsUnit.Pixel);
    }
  }

  internal void OldDrawRuler(bool CanPostpone)
  {
    Point[] points = new Point[3];
    if (!this.e.TerArg.ruler)
      return;
    if (!this.e.CaretEngaged && !this.e.RepaintRuler && this.e.MessageId != 276 && this.e.HilightType == 0)
    {
      this.e.RulerPending = true;
    }
    else
    {
      COp.MSG msg;
      if (CanPostpone && this.e.RulerLineX == this.e.text[this.e.CurLine].x && (this.PeekMessage(out msg, this.e.hTerWnd, 258, 258, 2) || this.PeekMessage(out msg, this.e.hTerWnd, 256 /*0x0100*/, 256 /*0x0100*/, 2)))
      {
        this.e.RulerPending = true;
      }
      else
      {
        this.e.RulerPending = this.e.RepaintRuler = false;
        this.e.RulerSection = this.GetSection(this.e.CurLine);
        this.e.RulerLineX = this.e.text[this.e.CurLine].x;
        if (this.e.HilightType != 0)
          this.e.CurPfmt = this.e.text[this.e.HilightBegRow].pfmt;
        else
          this.e.CurPfmt = this.e.text[this.e.CurLine].pfmt;
        this.DeleteDragObjects(5, 8);
        int scrResX = this.e.ScrResX;
        int section = this.GetSection(this.e.CurLine);
        int num1 = !this.e.BorderShowing ? (int) ((double) scrResX * ((double) this.e.TerSect1[section].PgWidth - (double) this.e.TerSect[section].LeftMargin - (double) this.e.TerSect[section].RightMargin)) : (int) ((double) scrResX * (double) this.e.TerSect1[section].PgWidth) + 2 * this.UnitToScrX(this.e.LeftBorderWidth);
        if (num1 < this.e.TerWinOrgX + this.e.TerWinWidth)
          num1 = this.e.TerWinOrgX + this.e.TerWinWidth;
        int FrameNo = this.frm.GetFrame(this.e.CurLine);
        if (FrameNo < 0)
          FrameNo = 0;
        this.e.CurFrame = FrameNo;
        int num2 = this.e.frame[FrameNo].x + this.e.frame[FrameNo].SpaceLeft;
        this.TerResetClipRgn();
        COp.RECT rect = new COp.RECT();
        rect.left = this.e.TerWinOrgX + (this.e.RulerRect.left - this.e.TerWinRect.left);
        rect.right = this.e.TerWinOrgX + this.e.TerWinWidth + (this.e.RulerRect.right - this.e.TerWinRect.right);
        int width = rect.right - rect.left;
        rect.top = 0;
        rect.bottom = this.e.RulerRect.bottom - this.e.RulerRect.top;
        int num3 = this.e.TerWinOrgY + this.e.RulerRect.top - this.e.TerWinRect.top;
        Bitmap bitmap = new Bitmap(rect.right - rect.left + 1, rect.bottom - rect.top + 1, this.e.TerGr);
        Graphics gr = Graphics.FromImage((Image) bitmap);
        this.ResetTransform(gr);
        gr.TranslateTransform((float) -rect.left, 0.0f);
        this.SetTextColor(gr, Color.Black);
        this.SetBkMode(gr, 1);
        Pen black = Pens.Black;
        Pen white = Pens.White;
        this.OurSetFont(gr, this.e.hRulerFont);
        Brush brush = (Brush) new SolidBrush(this.e.StatusBkColor);
        gr.FillRectangle(brush, new Rectangle(rect.left, 0, rect.right + 2 - rect.left, rect.bottom + 1 - rect.top));
        brush.Dispose();
        this.DrawShadowLine(gr, rect.left, rect.top, rect.right, rect.top, black, white);
        int num4 = rect.top + this.e.RulerFontHeight;
        this.DrawShadowLine(gr, rect.left, num4, rect.right, num4, black, white);
        this.e.ScrRtl = (this.e.text[this.e.CurLine].flags2 & 32 /*0x20*/) != 0;
        this.e.pScrSeg = (tc.StrLineSeg[]) null;
        float num5 = (float) num2;
        float num6 = (float) scrResX;
        if ((this.e.TerFlags & 2) != 0)
          num6 = num6 * 10f / this.InchesToMm(1f);
        int top = rect.top;
        int num7 = 0;
        while ((double) num5 <= (double) num1)
        {
          int num8 = this.RoundInt((int) ((double) num5 * 10.0), 10) / 10;
          this.DrawShadowLine(gr, num8, num4, num8, top, black, white);
          string s = num7.ToString();
          int x = num8 + 2;
          if (this.e.ScrRtl)
          {
            tc.StrLineSeg pSeg = new tc.StrLineSeg();
            x = this.RtlX(x - 5, 0, FrameNo, pSeg);
          }
          if ((double) num5 > (double) num2)
            gr.DrawString(s, this.e.RulerFont, Brushes.Black, (PointF) new Point(x, top));
          num5 += num6;
          ++num7;
        }
        float num9 = (float) scrResX / 2f;
        if ((this.e.TerFlags & 2) != 0)
          num9 = num9 * 10f / this.InchesToMm(1f);
        float num10 = (float) num2 + num9;
        int y2_1 = num4 - this.e.RulerFontHeight / 2;
        for (; (double) num10 < (double) num1; num10 += num9)
        {
          int num11 = this.RoundInt((int) ((double) num10 * 10.0), 10) / 10;
          this.DrawShadowLine(gr, num11, num4, num11, y2_1, black, white);
        }
        float num12 = (float) scrResX / 8f;
        if ((this.e.TerFlags & 2) != 0)
          num12 = num12 * 20f / this.InchesToMm(1f);
        float num13 = (float) num2 + num12;
        int y2_2 = num4 - this.e.RulerFontHeight / 3;
        for (; (double) num13 < (double) num1; num13 += num12)
        {
          int num14 = this.RoundInt((int) ((double) num13 * 10.0), 10) / 10;
          this.DrawShadowLine(gr, num14, num4, num14, y2_2, black, white);
        }
        tc.StrTab strTab = this.e.TerTab[!this.e.EditingParaStyle ? this.e.PfmtId[this.e.CurPfmt].TabId : this.e.StyleId[this.e.CurSID].TabId];
        for (int index = 0; index < strTab.count; ++index)
        {
          int num15 = this.TwipsToScrX(strTab.pos[index]) + num2;
          int y2_3 = strTab.type[index] != 2 ? (strTab.type[index] != 3 ? num4 + this.e.RulerFontHeight * 2 / 3 : num4 + this.e.RulerFontHeight * 2 / 3 - 1) : num4 + this.e.RulerFontHeight * 3 / 4;
          this.DrawShadowLine(gr, num15, num4, num15, y2_3, black, (Pen) null);
          int dragObjectSlot;
          if ((this.e.CurSID < 0 || this.e.EditingParaStyle) && (dragObjectSlot = this.GetDragObjectSlot()) >= 0)
          {
            int num16 = this.e.ScrRtl ? this.RtlX(num15, 0, FrameNo, new tc.StrLineSeg()) : num15;
            this.e.DragObj[dragObjectSlot].InUse = true;
            this.e.DragObj[dragObjectSlot].type = 8;
            this.e.DragObj[dragObjectSlot].id1 = index;
            this.e.DragObj[dragObjectSlot].HotRectCount = 1;
            this.e.DragObj[dragObjectSlot].HotRect[0].left = num16 - this.TwipsToScrX(60);
            this.e.DragObj[dragObjectSlot].HotRect[0].top = num3 + num4;
            this.e.DragObj[dragObjectSlot].HotRect[0].right = num16 + this.TwipsToScrX(60);
            this.e.DragObj[dragObjectSlot].HotRect[0].bottom = num3 + num4 + this.e.RulerFontHeight + 1;
            this.e.DragObj[dragObjectSlot].ObjPointCount = 2;
            this.e.DragObj[dragObjectSlot].ObjPoint[0].X = num16;
            this.e.DragObj[dragObjectSlot].ObjPoint[0].Y = num3 + num4;
            this.e.DragObj[dragObjectSlot].ObjPoint[1].X = num16;
            this.e.DragObj[dragObjectSlot].ObjPoint[1].Y = num3 + num4 + this.e.RulerFontHeight;
          }
          int y2_4 = num4 + this.e.RulerFontHeight / 4;
          this.DrawShadowLine(gr, num15, num4, num15 - this.e.RulerFontHeight / 4, y2_4, black, (Pen) null);
          this.DrawShadowLine(gr, num15, num4, num15 + this.e.RulerFontHeight / 4, y2_4, black, (Pen) null);
          int num17 = num4 + this.e.RulerFontHeight * 2 / 3;
          if (strTab.type[index] == 0)
            this.DrawShadowLine(gr, num15, num17, num15 + this.e.RulerFontHeight / 3, num17, black, (Pen) null);
          else if (strTab.type[index] == 1)
            this.DrawShadowLine(gr, num15, num17, num15 - this.e.RulerFontHeight / 3, num17, black, (Pen) null);
          else if (strTab.type[index] == 3)
            this.DrawShadowLine(gr, num15, num17, num15, num17 + 2, black, (Pen) null);
        }
        int x1 = (((strTab.count != 0 ? strTab.pos[strTab.count - 1] : 0) + 60) / this.e.DefTabWidth + 1) * this.e.DefTabWidth;
        do
        {
          int num18 = this.TwipsToScrX(x1) + num2;
          if (num18 < num1)
          {
            if ((this.e.TerFlags & 2) != 0)
            {
              int y2_5 = num4 + this.e.RulerFontHeight;
              this.DrawShadowLine(gr, num18, num4 + this.e.RulerFontHeight / 2, num18, y2_5, black, white);
            }
            else
            {
              int num19 = num4 + this.e.RulerFontHeight / 2;
              this.DrawShadowLine(gr, num18, num4, num18, num19, black, (Pen) null);
              this.DrawShadowLine(gr, num18 - this.e.RulerFontHeight / 4, num19, num18 + this.e.RulerFontHeight / 4, num19, black, (Pen) null);
            }
            x1 += this.e.DefTabWidth;
          }
          else
            break;
        }
        while (x1 >= 0);
        points[0].X = !this.e.EditingParaStyle ? this.e.PfmtId[this.e.CurPfmt].LeftIndent : this.TwipsToScrX(this.e.StyleId[this.e.CurSID].LeftIndentTwips);
        points[0].X += num2;
        points[0].Y = num4;
        points[1].X = points[0].X;
        points[1].Y = num4 + this.e.RulerFontHeight;
        points[2].X = points[0].X + this.e.RulerFontHeight / 2;
        points[2].Y = num4 + this.e.RulerFontHeight / 2;
        if (this.e.ScrRtl)
        {
          for (int index = 0; index < 3; ++index)
          {
            tc.StrLineSeg pSeg = new tc.StrLineSeg();
            points[index].X = this.RtlX(points[index].X, 0, FrameNo, pSeg);
          }
        }
        gr.FillPolygon(Brushes.Black, points);
        int dragObjectSlot1;
        if ((this.e.CurSID < 0 || this.e.EditingParaStyle) && (dragObjectSlot1 = this.GetDragObjectSlot()) >= 0)
        {
          this.e.DragObj[dragObjectSlot1].InUse = true;
          this.e.DragObj[dragObjectSlot1].type = 5;
          this.e.DragObj[dragObjectSlot1].id1 = this.e.CurPfmt;
          this.e.DragObj[dragObjectSlot1].HotRectCount = 1;
          this.e.DragObj[dragObjectSlot1].HotRect[0].left = points[0].X;
          this.e.DragObj[dragObjectSlot1].HotRect[0].top = num3 + points[0].Y;
          this.e.DragObj[dragObjectSlot1].HotRect[0].right = points[2].X;
          if (this.e.ScrRtl)
            this.SwapInts(ref this.e.DragObj[dragObjectSlot1].HotRect[0].left, ref this.e.DragObj[dragObjectSlot1].HotRect[0].right);
          this.e.DragObj[dragObjectSlot1].HotRect[0].bottom = num3 + points[1].Y;
          this.e.DragObj[dragObjectSlot1].ObjPointCount = 3;
          for (int index = 0; index < 3; ++index)
            this.e.DragObj[dragObjectSlot1].ObjPoint[index] = points[index];
        }
        points[0].X = !this.e.EditingParaStyle ? this.e.PfmtId[this.e.CurPfmt].LeftIndent + this.e.PfmtId[this.e.CurPfmt].FirstIndent : this.TwipsToScrX(this.e.StyleId[this.e.CurSID].LeftIndentTwips + this.e.StyleId[this.e.CurSID].FirstIndentTwips);
        points[0].X += num2;
        points[0].Y = num4 - this.e.RulerFontHeight;
        points[1].X = points[0].X;
        points[1].Y = num4;
        points[2].X = points[0].X + this.e.RulerFontHeight / 2;
        points[2].Y = num4 - this.e.RulerFontHeight / 2;
        if (this.e.ScrRtl)
        {
          for (int index = 0; index < 3; ++index)
          {
            tc.StrLineSeg pSeg = new tc.StrLineSeg();
            points[index].X = this.RtlX(points[index].X, 0, FrameNo, pSeg);
          }
        }
        gr.FillPolygon(Brushes.Black, points);
        int dragObjectSlot2;
        if ((this.e.CurSID < 0 || this.e.EditingParaStyle) && (dragObjectSlot2 = this.GetDragObjectSlot()) >= 0)
        {
          this.e.DragObj[dragObjectSlot2].InUse = true;
          this.e.DragObj[dragObjectSlot2].type = 7;
          this.e.DragObj[dragObjectSlot2].id1 = this.e.CurPfmt;
          this.e.DragObj[dragObjectSlot2].HotRectCount = 1;
          this.e.DragObj[dragObjectSlot2].HotRect[0].left = points[0].X;
          this.e.DragObj[dragObjectSlot2].HotRect[0].top = num3 + points[0].Y;
          this.e.DragObj[dragObjectSlot2].HotRect[0].right = points[2].X;
          if (this.e.ScrRtl)
            this.SwapInts(ref this.e.DragObj[dragObjectSlot2].HotRect[0].left, ref this.e.DragObj[dragObjectSlot2].HotRect[0].right);
          this.e.DragObj[dragObjectSlot2].HotRect[0].bottom = num3 + points[1].Y;
          this.e.DragObj[dragObjectSlot2].ObjPointCount = 3;
          for (int index = 0; index < 3; ++index)
            this.e.DragObj[dragObjectSlot2].ObjPoint[index] = points[index];
        }
        points[0].X = !this.e.EditingParaStyle ? this.TerWrapWidth(this.e.CurLine, -1) - this.e.PfmtId[this.e.CurPfmt].RightIndent : this.TerWrapWidth(this.e.CurLine, -1) - this.TwipsToScrX(this.e.StyleId[this.e.CurSID].RightIndentTwips);
        points[0].X += num2;
        points[0].Y = num4;
        points[1].X = points[0].X;
        points[1].Y = num4 + this.e.RulerFontHeight;
        points[2].X = points[0].X - this.e.RulerFontHeight / 2;
        points[2].Y = num4 + this.e.RulerFontHeight / 2;
        if (this.e.ScrRtl)
        {
          for (int index = 0; index < 3; ++index)
          {
            tc.StrLineSeg pSeg = new tc.StrLineSeg();
            points[index].X = this.RtlX(points[index].X, 0, FrameNo, pSeg);
          }
        }
        gr.FillPolygon(Brushes.Black, points);
        int dragObjectSlot3;
        if ((this.e.CurSID < 0 || this.e.EditingParaStyle) && (dragObjectSlot3 = this.GetDragObjectSlot()) >= 0)
        {
          this.e.DragObj[dragObjectSlot3].InUse = true;
          this.e.DragObj[dragObjectSlot3].type = 6;
          this.e.DragObj[dragObjectSlot3].id1 = this.e.CurPfmt;
          this.e.DragObj[dragObjectSlot3].HotRectCount = 1;
          this.e.DragObj[dragObjectSlot3].HotRect[0].left = points[2].X;
          this.e.DragObj[dragObjectSlot3].HotRect[0].top = num3 + points[0].Y;
          this.e.DragObj[dragObjectSlot3].HotRect[0].right = points[0].X;
          if (this.e.ScrRtl)
            this.SwapInts(ref this.e.DragObj[dragObjectSlot3].HotRect[0].left, ref this.e.DragObj[dragObjectSlot3].HotRect[0].right);
          this.e.DragObj[dragObjectSlot3].HotRect[0].bottom = num3 + points[1].Y;
          this.e.DragObj[dragObjectSlot3].ObjPointCount = 3;
          for (int index = 0; index < 3; ++index)
            this.e.DragObj[dragObjectSlot3].ObjPoint[index] = points[index];
        }
        this.SetBkMode(gr, 2);
        this.e.TerGr.DrawImage((Image) bitmap, new Rectangle(rect.left, this.e.TerWinOrgY + rect.top - (this.e.TerWinRect.top - this.e.RulerRect.top), width, rect.bottom - rect.top), new Rectangle(0, rect.top, width, rect.bottom - rect.top), GraphicsUnit.Pixel);
        gr.Dispose();
        bitmap.Dispose();
        this.TerResetClipRgn();
        this.e.ScrRtl = false;
      }
    }
  }

  internal bool OldResetBufBM()
  {
    if (tc.DebugMode)
      this.misc.dm("ResetBufBM");
    if (this.e.BufGr != null)
    {
      for (int index = 0; index < this.e.TotalOpGrs; ++index)
      {
        if (this.e.OpGr[index].gr == this.e.BufGr)
        {
          if (IntPtr.Zero != this.e.OpGr[index].hDC)
          {
            this.e.OpGr[index].gr.ReleaseHdc(this.e.OpGr[index].hDC);
            this.e.OpGr[index].hDC = IntPtr.Zero;
          }
          this.e.OpGr[index].gr = (Graphics) null;
        }
      }
      this.e.BufGr.Dispose();
      if (this.e.BufBM != null)
        this.e.BufBM.Dispose();
      this.e.BufGr = (Graphics) null;
      this.e.BufBM = this.e.OrigBufBM = (Bitmap) null;
    }
    if ((this.e.TerFlags & 134217728 /*0x08000000*/) != 0)
    {
      if ((this.e.BufGr = Graphics.FromImage((Image) (this.e.BufBM = new Bitmap(this.e.TerRect.right - this.e.TerRect.left + 1, this.e.TerRect.bottom - this.e.TerRect.top + 1, this.e.TerGr)))) == null)
        return false;
      Color color = this.SetBkColor(this.e.BufGr, this.PageColor());
      this.OurExtTextOut(this.e.BufGr, 0, 0, 2, this.e.TerRect, (char[]) null, 0, (int[]) null);
      this.SetBkColor(this.e.BufGr, color);
    }
    return true;
  }

  internal bool OurBitBlt(
    Graphics gr,
    int DestX,
    int DestY,
    int DestWidth,
    int DestHeight,
    Graphics SrcGr,
    Bitmap SrcBM,
    int SrcX,
    int SrcY,
    int rop)
  {
    if (this.e.ScrRtl && this.e.pScrSeg != null && this.e.CurScrSeg < this.e.TotalScrSeg)
    {
      int pVal1 = this.RtlX(DestX, 0, this.e.CurFrame, this.e.pScrSeg[this.e.CurScrSeg]);
      int pVal2 = this.RtlX(DestX + DestWidth, 0, this.e.CurFrame, this.e.pScrSeg[this.e.CurScrSeg]);
      if (pVal2 < pVal1)
        this.SwapInts(ref pVal1, ref pVal2);
      DestX = pVal1;
      DestWidth = pVal2 - pVal1;
    }
    if (this.e.ScrFrameAngle == 0)
      return this.BitBlt(gr, DestX, DestY, DestWidth, DestHeight, SrcGr, SrcBM, SrcX, SrcY, rop);
    COp.RECT OurRect;
    this.SetRect(out OurRect, DestX, DestY, DestX + DestWidth, DestY + DestHeight);
    this.FrameRotateRect(ref OurRect, this.e.CurFrame);
    int DestX1 = OurRect.left < OurRect.right ? OurRect.left : OurRect.right;
    int DestY1 = OurRect.top < OurRect.bottom ? OurRect.top : OurRect.bottom;
    return this.BitBlt(gr, DestX1, DestY1, DestHeight, DestWidth, SrcGr, SrcBM, SrcX, SrcY, rop);
  }

  internal new Pen OurCreatePen(int TwipsThick, Color color, out int pDeviceThick)
  {
    int width = !this.e.InPrinting ? this.TwipsToScrX(TwipsThick) : this.TwipsToUnitX(TwipsThick);
    if (width < 1)
      width = 1;
    pDeviceThick = width;
    if (color == Color.Empty)
      color = Color.Black;
    return new Pen(color, (float) width);
  }

  internal new void OurDrawImage(
    Graphics gr,
    Image image,
    int DestX,
    int DestY,
    int DestWidth,
    int DestHeight)
  {
    if (this.e.ScrRtl && this.e.pScrSeg != null && this.e.CurScrSeg < this.e.TotalScrSeg)
    {
      int pVal1 = this.RtlX(DestX, 0, this.e.CurFrame, this.e.pScrSeg[this.e.CurScrSeg]);
      int pVal2 = this.RtlX(DestX + DestWidth, 0, this.e.CurFrame, this.e.pScrSeg[this.e.CurScrSeg]);
      if (pVal2 < pVal1)
        this.SwapInts(ref pVal1, ref pVal2);
      DestX = pVal1;
      DestWidth = pVal2 - pVal1;
    }
    if (this.e.ScrFrameAngle == 0)
    {
      gr.DrawImage(image, new Rectangle(DestX, DestY, DestWidth, DestHeight));
    }
    else
    {
      COp.RECT rect;
      rect.left = DestX;
      rect.right = DestX + DestWidth;
      rect.top = DestY;
      rect.bottom = DestY + DestHeight;
      this.FrameRotateRect(ref rect, this.e.CurFrame);
      int x = rect.left < rect.right ? rect.left : rect.right;
      int y = rect.top < rect.bottom ? rect.top : rect.bottom;
      gr.DrawImage(image, new Rectangle(x, y, DestWidth, DestHeight));
    }
  }

  internal void OurDrawLine(Graphics gr, Pen pen, int x1, int y1, int x2, int y2)
  {
    if ((tc.OSCanRotate || this.e.ScrFrameAngle == 0) && !this.e.ScrRtl)
    {
      gr.DrawLine(pen, x1, y1, x2, y2);
    }
    else
    {
      if (this.e.ScrRtl)
      {
        x1 = this.RtlX(x1, 0, this.e.CurFrame, this.e.pScrSeg != null ? this.e.pScrSeg[this.e.CurScrSeg] : new tc.StrLineSeg());
        x2 = this.RtlX(x2, 0, this.e.CurFrame, this.e.pScrSeg != null ? this.e.pScrSeg[this.e.CurScrSeg] : new tc.StrLineSeg());
      }
      int x1_1;
      int y1_1;
      int x2_1;
      int y2_1;
      if (tc.OSCanRotate)
      {
        x1_1 = x1;
        y1_1 = y1;
        x2_1 = x2;
        y2_1 = y2;
      }
      else
      {
        x1_1 = this.FrameRotateX(x1, y1, this.e.CurFrame);
        y1_1 = this.FrameRotateY(x1, y1, this.e.CurFrame);
        x2_1 = this.FrameRotateX(x2, y2, this.e.CurFrame);
        y2_1 = this.FrameRotateY(x2, y2, this.e.CurFrame);
      }
      gr.DrawLine(pen, x1_1, y1_1, x2_1, y2_1);
    }
  }

  internal void OurEllipse(Graphics gr, bool fill, Color BrushColor, Pen pen, COp.RECT rect)
  {
    if (this.OpDCIsOpen(gr))
    {
      this.Ellipse(gr, fill, BrushColor, pen, rect);
    }
    else
    {
      if (fill)
      {
        Brush brush = (Brush) new SolidBrush(BrushColor);
        gr.FillEllipse(brush, this.ToRectangle(rect));
        brush.Dispose();
      }
      if (pen == null)
        return;
      gr.DrawEllipse(pen, this.ToRectangle(rect));
    }
  }

  internal new void OurFillPolygon(Graphics gr, Brush brush, Color BrushColor, Point[] pt)
  {
    if (this.OpDCIsOpen(gr))
      this.Polygon(gr, BrushColor, pt);
    else
      gr.FillPolygon(brush, pt);
  }

  internal new void OurFillRectangle(Graphics gr, Color BrushColor, COp.RECT rect)
  {
    if (this.OpDCIsOpen(gr))
    {
      this.FillRect(gr, BrushColor, ref rect);
    }
    else
    {
      Brush brush = (Brush) new SolidBrush(BrushColor);
      gr.FillRectangle(brush, this.FromRect(rect));
      brush.Dispose();
    }
  }

  internal bool OurLineTo(IntPtr hDC, int x, int y)
  {
    if ((tc.OSCanRotate || this.e.ScrFrameAngle == 0) && !this.e.ScrRtl)
      return this.LineTo(hDC, x, y);
    if (this.e.ScrRtl)
      x = this.RtlX(x, 0, this.e.CurFrame, this.e.pScrSeg != null ? this.e.pScrSeg[this.e.CurScrSeg] : new tc.StrLineSeg());
    int x1;
    int y1;
    if (tc.OSCanRotate)
    {
      x1 = x;
      y1 = y;
    }
    else
    {
      x1 = this.FrameRotateX(x, y, this.e.CurFrame);
      y1 = this.FrameRotateY(x, y, this.e.CurFrame);
    }
    return this.LineTo(hDC, x1, y1);
  }

  internal bool OurMoveToEx(IntPtr hDC, int x, int y, IntPtr pPt)
  {
    if ((tc.OSCanRotate || this.e.ScrFrameAngle == 0) && !this.e.ScrRtl)
      return this.MoveToEx(hDC, x, y, pPt);
    if (this.e.ScrRtl)
      x = this.RtlX(x, 0, this.e.CurFrame, this.e.pScrSeg != null ? this.e.pScrSeg[this.e.CurScrSeg] : new tc.StrLineSeg());
    int x1;
    int y1;
    if (tc.OSCanRotate)
    {
      x1 = x;
      y1 = y;
    }
    else
    {
      x1 = this.FrameRotateX(x, y, this.e.CurFrame);
      y1 = this.FrameRotateY(x, y, this.e.CurFrame);
    }
    return this.MoveToEx(hDC, x1, y1, pPt);
  }

  internal new bool PaintBkPict(Graphics gr)
  {
    COp.RECT clip = new COp.RECT();
    Graphics graphics = (Graphics) null;
    Graphics gr1 = gr;
    Bitmap bitmap = (Bitmap) null;
    if (tc.DebugMode)
      this.misc.dm(nameof (PaintBkPict));
    if (this.e.BkPictId != 0)
    {
      if (this.e.TerFont[this.e.BkPictId].ImageType == ImageFormat.Wmf.Guid)
      {
        if (gr == this.e.TerGr || gr == this.e.BufGr)
        {
          if ((bitmap = new Bitmap(this.e.TerWinWidth, this.e.TerWinHeight, this.e.TerGr)) == null || (graphics = Graphics.FromImage((Image) bitmap)) == null)
            return false;
          gr1 = graphics;
        }
        int num;
        clip.top = num = 0;
        clip.left = num;
        clip.right = clip.left + this.e.TerWinWidth;
        clip.bottom = clip.top + this.e.TerWinHeight;
        this.SetBkColor(gr1, this.PageColor());
        this.ExtTextOut(gr1, 0, 0, 2, clip, (char[]) null, 0, (int[]) null);
      }
      if (gr1 == this.e.TerGr || gr1 == this.e.BufGr)
      {
        if (this.e.CaretEnabled && !this.e.CaretHidden)
          this.HideCaret(this.e.hTerWnd);
        this.BkPictOut(gr1, this.e.TerWinOrgX, this.e.TerWinOrgY);
        if (this.e.CaretEnabled && !this.e.CaretHidden)
          this.ShowCaret(this.e.hTerWnd);
      }
      else
      {
        COp.RECT terWinRect = this.e.TerWinRect;
        int terWinOrgX = this.e.TerWinOrgX;
        int terWinOrgY = this.e.TerWinOrgY;
        this.e.TerWinRect = clip;
        this.e.TerWinOrgX = this.e.TerWinOrgY = 0;
        this.BkPictOut(gr1, 0, 0);
        this.e.TerWinRect = terWinRect;
        this.e.TerWinOrgX = terWinOrgX;
        this.e.TerWinOrgY = terWinOrgY;
        if (graphics != null && bitmap != null && (gr == this.e.TerGr || gr == this.e.BufGr))
        {
          if (this.e.CaretEnabled && !this.e.CaretHidden)
            this.HideCaret(this.e.hTerWnd);
          gr.DrawImage((Image) bitmap, new Rectangle(this.e.TerWinOrgX, this.e.TerWinOrgY, this.e.TerWinWidth, this.e.TerWinHeight), new Rectangle(0, 0, this.e.TerWinWidth, this.e.TerWinHeight), GraphicsUnit.Pixel);
          if (this.e.CaretEnabled && !this.e.CaretHidden)
            this.ShowCaret(this.e.hTerWnd);
        }
      }
      graphics?.Dispose();
    }
    return true;
  }

  internal bool PictOut(
    Graphics gr,
    int CurX,
    int CurY,
    int width,
    int height,
    int pict,
    int count,
    int LineNo,
    bool ToBufDC)
  {
    int SrcX = 0;
    int SrcY = 0;
    int DestX = CurX;
    int DestY = CurY;
    count = 1;
    if (width < 0)
      width = this.e.TerFont[pict].CharWidth[24];
    if (height < 0)
      height = this.e.TerFont[pict].height;
    int num1;
    int DestWidth = num1 = width;
    int num2;
    int DestHeight = num2 = height;
    if (this.e.TerFont[pict].ObjectType == 5 && (this.e.TerFont[pict].flags & 2) == 0 && this.e.ScrollBM == null)
    {
      if ((this.e.TerFont[pict].flags & 4) != 0)
      {
        this.e.MovingControl = true;
        this.e.MovingControl = false;
        this.e.Validate();
        this.TerSetClipRgn();
      }
      if ((this.e.TerFont[pict].flags & 4) != 0)
      {
        this.e.TerFont[pict].flags |= 8;
        return true;
      }
    }
    int num3 = this.e.TerFont[pict].CropLeft != 0 || this.e.TerFont[pict].CropRight != 0 || this.e.TerFont[pict].CropTop != 0 ? 1 : (this.e.TerFont[pict].CropBot != 0 ? 1 : 0);
    if (num3 != 0 && this.e.TerFont[pict].PictWidth != 0)
    {
      float num4 = (float) this.e.TerFont[pict].PictWidth / (float) (this.e.TerFont[pict].OrigPictWidth - this.e.TerFont[pict].CropLeft - this.e.TerFont[pict].CropRight);
      SrcX = this.TwipsToScrX((int) ((double) this.e.TerFont[pict].CropLeft * (double) num4));
      DestX -= SrcX;
      num1 = this.TwipsToScrX((int) ((double) this.e.TerFont[pict].OrigPictWidth * (double) num4));
    }
    if (num3 != 0 && this.e.TerFont[pict].PictHeight != 0)
    {
      float num5 = (float) this.e.TerFont[pict].PictHeight / (float) (this.e.TerFont[pict].OrigPictHeight - this.e.TerFont[pict].CropTop - this.e.TerFont[pict].CropBot);
      SrcY = this.TwipsToScrY((int) ((double) this.e.TerFont[pict].CropTop * (double) num5));
      DestY -= SrcY;
      num2 = this.TwipsToScrY((int) ((double) this.e.TerFont[pict].OrigPictHeight * (double) num5));
    }
    if (this.e.ScrFrameAngle > 0 && tc.OSCanRotate)
      this.FrameNoRotateDC(gr);
    if (this.e.TerFont[pict].PictType == 0)
    {
      int width1 = num1 * count;
      Bitmap bitmap = (Bitmap) null;
      IntPtr num6 = IntPtr.Zero;
      Graphics graphics;
      int y;
      int x;
      if (ToBufDC)
      {
        bitmap = new Bitmap(num1, num2);
        graphics = Graphics.FromImage((Image) bitmap);
        graphics.CompositingQuality = CompositingQuality.HighQuality;
        graphics.SmoothingMode = SmoothingMode.HighQuality;
        graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
        x = y = 0;
        if (this.e.TerFont[pict].ImageType == ImageFormat.Wmf.Guid)
          num6 = this.e.TerFont[pict].hMeta;
      }
      else
      {
        graphics = gr;
        x = CurX;
        y = CurY;
      }
      if (this.e.TerFont[pict].ImageType == ImageFormat.Wmf.Guid || this.e.TerFont[pict].ImageType == ImageFormat.Emf.Guid)
      {
        using (SolidBrush solidBrush = new SolidBrush(this.PageColor()))
          graphics.FillRectangle((Brush) solidBrush, new Rectangle(x, y, width1, num2));
      }
      IntPtr hMeta1 = this.e.TerFont[pict].hMeta;
      if (((!(this.e.TerFont[pict].ImageType == ImageFormat.Wmf.Guid) && !(this.e.TerFont[pict].ImageType == ImageFormat.Emf.Guid) || !(this.e.TerFont[pict].image is Metafile) ? 0 : (hMeta1 != IntPtr.Zero ? 1 : 0)) & (ToBufDC ? 1 : 0)) != 0)
      {
        IntPtr num7 = hMeta1;
        IntPtr hgdiObj = COp.Win32.SelectObject(this.e.hMemDC, num7);
        this.FixImageDest(ref DestX, ref DestY, num1, num2);
        if (this.e.hBufDC != IntPtr.Zero)
        {
          COp.RECT rect = new COp.RECT(DestX + SrcX, DestY + SrcY, DestWidth + DestX + SrcX, DestHeight + DestY + SrcY);
          COp.Win32.PlayEnhMetaFile(this.e.hBufDC, num7, ref rect);
        }
        else
        {
          IntPtr opDc = this.GetOpDC(this.FindOpGr(gr));
          COp.RECT rect = new COp.RECT(DestX, DestY, num1 + DestX, num2 + DestY);
          IntPtr hMeta2 = num7;
          ref COp.RECT local = ref rect;
          COp.Win32.PlayEnhMetaFile(opDc, hMeta2, ref local);
          this.ReleaseOpDC(gr);
        }
        COp.Win32.SelectObject(this.e.hMemDC, hgdiObj);
        COp.Win32.DeleteObject(num7);
        graphics.Dispose();
        bitmap.Dispose();
      }
      else if (hMeta1 != IntPtr.Zero & ToBufDC)
      {
        IntPtr hbitmap = bitmap.GetHbitmap();
        IntPtr hgdiObj = COp.Win32.SelectObject(this.e.hMemDC, hbitmap);
        int StateId = COp.Win32.SaveDC(this.e.hMemDC);
        COp.Win32.SetMapMode(this.e.hMemDC, 8);
        COp.Win32.SetWindowExtEx(this.e.hMemDC, this.e.TerFont[pict].bmWidth, this.e.TerFont[pict].bmHeight, IntPtr.Zero);
        COp.Win32.SetViewportExtEx(this.e.hMemDC, num1, num2, IntPtr.Zero);
        COp.Win32.SetViewportOrgEx(this.e.hMemDC, 0, 0, IntPtr.Zero);
        COp.Win32.SetWindowOrgEx(this.e.hMemDC, 0, 0, IntPtr.Zero);
        COp.Win32.PlayMetaFile(this.e.hMemDC, hMeta1);
        COp.Win32.RestoreDC(this.e.hMemDC, StateId);
        this.FixImageDest(ref DestX, ref DestY, num1, num2);
        if (this.e.hBufDC != IntPtr.Zero)
        {
          COp.Win32.BitBlt(this.e.hBufDC, DestX, DestY, num1, num2, this.e.hMemDC, 0, 0, 13369376);
        }
        else
        {
          COp.Win32.BitBlt(this.GetOpDC(this.FindOpGr(gr)), DestX, DestY, num1, num2, this.e.hMemDC, 0, 0, 13369376);
          this.ReleaseOpDC(gr);
        }
        COp.Win32.SelectObject(this.e.hMemDC, hgdiObj);
        COp.Win32.DeleteObject(hbitmap);
        graphics.Dispose();
        bitmap.Dispose();
      }
      else
      {
        if (this.e.TerFont[pict].ParaFID == this.e.WmParaFID && this.e.WmImageAttr != null)
        {
          Image image = this.e.TerFont[pict].image;
          graphics.DrawImage(image, new Rectangle(x, y, num1, num2), 0, 0, image.Size.Width, image.Size.Height, GraphicsUnit.Pixel, this.e.WmImageAttr);
        }
        else
        {
          if (this.e.TerFont[pict].ImageType == ImageFormat.Gif.Guid || this.e.TerFont[pict].ImageType == ImageFormat.Png.Guid)
          {
            Color color = this.PageColor();
            int index = 0;
            if (LineNo >= 0 && LineNo < this.e.TotalLines)
              index = this.e.text[LineNo].pfmt;
            if (!this.IsSameColor(this.e.PfmtId[index].BkColor, tc.CLR_WHITE))
              color = this.e.PfmtId[index].BkColor;
            using (SolidBrush solidBrush = new SolidBrush(color))
              graphics.FillRectangle((Brush) solidBrush, new Rectangle(x, y, num1, num2));
          }
          graphics.DrawImage(this.e.TerFont[pict].image, new Rectangle(x, y, num1, num2));
        }
        if (ToBufDC)
        {
          IntPtr hbitmap = bitmap.GetHbitmap();
          IntPtr hgdiObj = COp.Win32.SelectObject(this.e.hMemDC, hbitmap);
          this.FixImageDest(ref DestX, ref DestY, num1, num2);
          if (this.e.hBufDC != IntPtr.Zero)
          {
            COp.Win32.BitBlt(this.e.hBufDC, DestX + SrcX, DestY + SrcY, DestWidth, DestHeight, this.e.hMemDC, SrcX, SrcY, 13369376);
          }
          else
          {
            COp.Win32.BitBlt(this.GetOpDC(this.FindOpGr(gr)), DestX, DestY, num1, num2, this.e.hMemDC, 0, 0, 13369376);
            this.ReleaseOpDC(gr);
          }
          COp.Win32.SelectObject(this.e.hMemDC, hgdiObj);
          COp.Win32.DeleteObject(hbitmap);
          graphics.Dispose();
          bitmap.Dispose();
        }
      }
    }
    else if (this.e.TerFont[pict].ctl != null)
    {
      Control ctl = this.e.TerFont[pict].ctl;
      Point client = this.OurPointToClient(new Point(DestX, DestY));
      COp.RECT terWinRect1 = this.e.TerWinRect;
      this.TwipsToOrigScrY(this.e.BorderMarg);
      COp.RECT terWinRect2 = this.e.TerWinRect;
      this.TwipsToOrigScrY(this.e.BorderMarg);
      this.CalcBorderSpill(DestX, DestY, DestX + num1, DestY + num2);
      if (true)
      {
        bool flag = false;
        Point location = ctl.Location;
        if (location.X == client.X)
        {
          location = ctl.Location;
          if (location.Y == client.Y)
            goto label_59;
        }
        ctl.Location = client;
        flag = true;
label_59:
        if (num1 != ctl.Width)
        {
          ctl.Width = num1;
          flag = true;
        }
        if (num2 != ctl.Height)
        {
          ctl.Height = num2;
          flag = true;
        }
        if (!ctl.Visible)
        {
          ctl.Visible = true;
          flag = true;
        }
        if (flag)
          ctl.Refresh();
        int index = 0;
        while (index < this.e.TotalScrCtls && (!this.e.ScrCtl[index].InUse || this.e.ScrCtl[index].pict != pict || this.e.ScrCtl[index].ctl != this.e.TerFont[pict].ctl))
          ++index;
        if (index == this.e.TotalScrCtls)
        {
          index = 0;
          while (index < this.e.TotalScrCtls && this.e.ScrCtl[index].InUse)
            ++index;
        }
        if (index == this.e.TotalScrCtls)
        {
          if (this.e.TotalScrCtls >= this.e.MaxScrCtls)
          {
            this.e.MaxScrCtls += 10;
            this.e.ScrCtl = this.ReAlloc(this.e.ScrCtl, this.e.MaxScrCtls);
          }
          index = this.e.TotalScrCtls;
          ++this.e.TotalScrCtls;
        }
        this.e.ScrCtl[index] = new tc.StrScrCtl();
        this.e.ScrCtl[index].InUse = true;
        this.e.ScrCtl[index].x = client.X;
        this.e.ScrCtl[index].y = client.Y;
        this.e.ScrCtl[index].width = num1;
        this.e.ScrCtl[index].height = num2;
        this.e.ScrCtl[index].pict = pict;
        this.e.ScrCtl[index].ctl = ctl;
        this.e.ScrCtl[index].LineNo = LineNo;
        this.e.ScrCtl[index].PaintId = this.e.PaintId;
        COp.RECT terRect = this.e.TerRect;
        if (this.e.TerTlb != null)
          terRect.top = this.e.ToolBarHeight;
        COp.Win32.ValidateRect(this.e.hTerWnd, ref terRect);
        this.TerSetClipRgn();
      }
      else
      {
        int index = 0;
        while (index < this.e.TotalScrCtls && (!this.e.ScrCtl[index].InUse || this.e.ScrCtl[index].pict != pict || this.e.ScrCtl[index].ctl != this.e.TerFont[pict].ctl))
          ++index;
        if (index < this.e.TotalScrCtls)
        {
          ctl.Visible = false;
          this.e.ScrCtl[index].InUse = false;
          COp.RECT terRect = this.e.TerRect;
          if (this.e.TerTlb != null)
            terRect.top = this.e.ToolBarHeight;
          COp.Win32.ValidateRect(this.e.hTerWnd, ref terRect);
          this.TerSetClipRgn();
          this.e.Validate();
        }
      }
    }
    this.e.TerFont[pict].flags = tc.ResetUintFlag(ref this.e.TerFont[pict].flags, 16 /*0x10*/);
    if (this.e.ScrFrameAngle > 0 && tc.OSCanRotate)
      this.FrameRotateDC(gr, this.e.CurFrame);
    return true;
  }

  internal bool RedrawNonText(int spill)
  {
    if (this.e.UseWin)
    {
      int windowLong;
      int flags = windowLong = COp.Win32.GetWindowLong(this.e.hTerWnd, -16);
      tc.ResetUintFlag(ref flags, 33554432 /*0x02000000*/);
      COp.Win32.SetWindowLong(this.e.hTerWnd, -16, flags);
      if ((spill & 4) != 0 && this.e.TerArg.ruler)
      {
        this.e.RepaintRuler = true;
        this.DrawRuler(false);
      }
      if ((spill & 8) != 0 && this.e.TerArg.ShowStatus)
        this.DisplayStatus();
      if (this.e.TerArg.BorderMargin)
      {
        Color color = this.SetBkColor(this.e.TerGr, this.e.TextDefBkColor);
        COp.RECT clip;
        if ((spill & 1) != 0)
        {
          clip.left = this.e.TerWinOrgX - this.TwipsToScrX(75);
          clip.right = this.e.TerWinOrgX;
          clip.top = this.e.TerWinOrgY - this.e.TerWinRect.top;
          clip.bottom = clip.top + this.e.TerRect.bottom;
          this.ExtTextOut(this.e.TerGr, clip.left, clip.top, 2, clip, (char[]) null, 0, (int[]) null);
        }
        if ((spill & 2) != 0)
        {
          clip.left = this.e.TerWinOrgX + this.e.TerWinWidth;
          clip.right = clip.left + this.TwipsToScrX(75) + 1;
          clip.top = this.e.TerWinOrgY - this.e.TerWinRect.top;
          clip.bottom = clip.top + this.e.TerRect.bottom;
          this.ExtTextOut(this.e.TerGr, clip.left, clip.top, 2, clip, (char[]) null, 0, (int[]) null);
        }
        if ((spill & 4) != 0)
        {
          clip.left = this.e.TerWinOrgX - this.TwipsToScrX(75);
          clip.right = clip.left + this.e.TerRect.right;
          clip.top = this.e.TerWinOrgY - this.TwipsToScrY(75);
          clip.bottom = this.e.TerWinOrgY;
          this.ExtTextOut(this.e.TerGr, clip.left, clip.top, 2, clip, (char[]) null, 0, (int[]) null);
        }
        if ((spill & 8) != 0)
        {
          clip.left = this.e.TerWinOrgX - this.TwipsToScrX(75);
          clip.right = clip.left + this.e.TerRect.right;
          clip.top = this.e.TerWinOrgY + this.e.TerWinHeight;
          clip.bottom = clip.top + this.TwipsToScrY(75);
          this.ExtTextOut(this.e.TerGr, clip.left, clip.top, 2, clip, (char[]) null, 0, (int[]) null);
        }
        this.SetBkColor(this.e.TerGr, color);
      }
      if (windowLong != flags)
        COp.Win32.SetWindowLong(this.e.hTerWnd, -16, windowLong);
    }
    return true;
  }

  internal new bool ResetBufBM()
  {
    if (tc.DebugMode)
      this.misc.dm(nameof (ResetBufBM));
    if (this.e.BufGr != null)
    {
      for (int index = 0; index < this.e.TotalOpGrs; ++index)
      {
        if (this.e.OpGr[index].gr == this.e.BufGr)
        {
          if (IntPtr.Zero != this.e.OpGr[index].hDC)
            this.e.OpGr[index].hDC = IntPtr.Zero;
          this.e.OpGr[index].gr = (Graphics) null;
        }
      }
      this.e.BufGr.Dispose();
      if (this.e.BufBM != null)
        this.e.BufBM.Dispose();
      this.e.BufGr = (Graphics) null;
      this.e.OrigBufBM = (Bitmap) null;
      this.e.BufBM = (Bitmap) null;
    }
    if (this.e.hBufBM != IntPtr.Zero)
    {
      COp.Win32.SelectObject(this.e.hBufDC, this.e.hOldBufBM);
      COp.Win32.DeleteObject(this.e.hBufBM);
      this.e.hBufBM = IntPtr.Zero;
    }
    if (this.e.hBufDC != IntPtr.Zero && (this.e.TerFlags & 134217728 /*0x08000000*/) == 0)
    {
      COp.Win32.DeleteDC(this.e.hBufDC);
      this.e.hBufDC = IntPtr.Zero;
    }
    if ((this.e.TerFlags & 134217728 /*0x08000000*/) != 0)
    {
      if ((this.e.BufGr = Graphics.FromImage((Image) (this.e.BufBM = new Bitmap(this.e.TerRect.right - this.e.TerRect.left + 1, this.e.TerRect.bottom - this.e.TerRect.top + 1, this.e.TerGr)))) == null)
        return false;
      if (this.e.BufBM != null)
        this.e.hBufBM = this.e.BufBM.GetHbitmap();
      this.e.hOldBufBM = COp.Win32.SelectObject(this.e.hBufDC, this.e.hBufBM);
      Color color = this.SetBkColor(this.e.BufGr, this.PageColor());
      this.OurExtTextOut(this.e.BufGr, 0, 0, 2, this.e.TerRect, (char[]) null, 0, (int[]) null);
      this.SetBkColor(this.e.BufGr, color);
    }
    return true;
  }

  internal new bool ResetWheel()
  {
    if (this.e.WheelShowing)
    {
      this.SetWheelTimer(0, true);
      this.e.WheelShowing = false;
      if ((Cursor) null != this.e.WheelCur)
      {
        this.e.WheelCur = (Cursor) null;
        this.TerSetCursorShape(0, false);
      }
      this.PaintTer();
    }
    return true;
  }

  internal bool SaveHlLine(
    Graphics gr,
    int LineNo,
    int HiBegCol,
    int HiEndCol,
    int y,
    int height)
  {
    int curScrSeg = this.e.CurScrSeg;
    int num = !this.e.ScrRtl || this.e.pScrSeg == null ? 1 : this.e.TotalScrSeg;
    for (int index = 0; index < num; ++index)
    {
      int col1;
      int col2;
      if (this.e.ScrRtl && this.e.pScrSeg != null && num > 1)
      {
        if (HiBegCol < this.e.pScrSeg[index].col + this.e.pScrSeg[index].count && HiEndCol > this.e.pScrSeg[index].col)
        {
          col1 = HiBegCol > this.e.pScrSeg[index].col ? HiBegCol : this.e.pScrSeg[index].col;
          col2 = (HiEndCol < this.e.pScrSeg[index].col + this.e.pScrSeg[index].count ? HiEndCol : this.e.pScrSeg[index].col + this.e.pScrSeg[index].count) - 1;
        }
        else
          continue;
      }
      else
      {
        col1 = HiBegCol;
        col2 = HiEndCol - 1;
      }
      int units1 = this.ColToUnits(col1, LineNo, 0);
      int units2 = this.ColToUnits(col2, LineNo, 2);
      if (this.e.ScrRtl && this.e.pScrSeg != null)
        this.e.CurScrSeg = index;
      this.AddHlSeg(gr, units1, y, units2 - units1, height);
    }
    this.e.CurScrSeg = curScrSeg;
    return true;
  }

  internal bool SelectTerText(
    int FirstLine,
    int FirstCol,
    int LastLine,
    int LastCol,
    bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (FirstCol < 0)
      this.AbsToRowCol(FirstLine, out FirstLine, out FirstCol);
    if (LastCol < 0)
    {
      this.AbsToRowCol(LastLine, out LastLine, out LastCol);
      if ((FirstLine != LastLine || FirstCol != LastCol) && this.e.TerArg.WordWrap && LastCol == 0 && LastLine > 0 && this.LineEndsInBreak(LastLine - 1))
      {
        --LastLine;
        LastCol = this.e.text[LastLine].len;
      }
    }
    this.e.HilightType = 2;
    this.e.HilightBegRow = FirstLine;
    this.e.HilightBegCol = FirstCol;
    this.e.HilightEndRow = LastLine;
    this.e.HilightEndCol = LastCol;
    this.e.StretchHilight = false;
    if (repaint)
    {
      int curCfmt = this.GetCurCfmt(this.e.HilightBegRow, this.e.HilightBegCol);
      this.e.PictureClicked = false;
      if ((this.e.TerFont[curCfmt].style & 128 /*0x80*/) != 0)
      {
        int abs = this.RowColToAbs(this.e.HilightBegRow, this.e.HilightBegCol);
        this.e.PictureClicked = this.RowColToAbs(this.e.HilightEndRow, this.e.HilightEndCol) == abs + 1;
      }
      if (this.e.PictureClicked && (this.e.TerFlags & 16777216 /*0x01000000*/) == 0)
      {
        this.e.PictureHilighted = false;
        this.ShowPictureDragObjects(curCfmt);
      }
      else
        this.PaintTer();
    }
    return true;
  }

  /// <summary>Выделить строки</summary>
  /// <param name="FirstLine">Первая строка</param>
  /// <param name="LastLine">Последняя строка</param>
  /// <param name="repaint">Перерисовать</param>
  /// <returns>true, если без ошибок</returns>
  internal bool SelectTerTextLines(int FirstLine, int LastLine, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    this.e.HilightType = 1;
    if (FirstLine > LastLine)
    {
      int num = LastLine;
      LastLine = FirstLine;
      FirstLine = num;
    }
    if (FirstLine < 0)
      FirstLine = 0;
    this.e.HilightBegRow = FirstLine;
    this.e.HilightBegCol = 0;
    if (LastLine > this.e.TotalLines - 1)
      LastLine = this.e.TotalLines - 1;
    this.e.HilightEndRow = LastLine;
    this.e.HilightEndCol = this.e.text[LastLine].len - 1;
    this.e.StretchHilight = false;
    if (repaint)
    {
      int curCfmt = this.GetCurCfmt(this.e.HilightBegRow, this.e.HilightBegCol);
      this.e.PictureClicked = false;
      if ((this.e.TerFont[curCfmt].style & 128 /*0x80*/) != 0)
      {
        int abs = this.RowColToAbs(this.e.HilightBegRow, this.e.HilightBegCol);
        this.e.PictureClicked = this.RowColToAbs(this.e.HilightEndRow, this.e.HilightEndCol) == abs + 1;
      }
      if (this.e.PictureClicked && (this.e.TerFlags & 16777216 /*0x01000000*/) == 0)
      {
        this.e.PictureHilighted = false;
        this.ShowPictureDragObjects(curCfmt);
      }
      else
        this.PaintTer();
    }
    return true;
  }

  internal new bool SetMouseStopTimer(int lParam)
  {
    int num1 = this.e.MouseX = (int) (short) COp.LOWORD(lParam);
    int num2 = this.e.MouseY = (int) (short) COp.HIWORD(lParam);
    if (!this.e.MouseStopTimerOn || num1 != this.e.MouseStopX || num2 != this.e.MouseStopY)
    {
      this.e.MouseStopX = num1;
      this.e.MouseStopY = num2;
      this.TerMousePos(lParam, true);
      if (this.e.MouseStopTimerOn && this.e.MouseLine == this.e.MouseStopLine && this.e.MouseCol == this.e.MouseStopCol || this.e.MouseStopDone && this.e.MouseLine == this.e.MouseStopLine && this.e.MouseCol == this.e.MouseStopCol)
        return true;
      this.e.MouseStopLine = this.e.MouseLine;
      if (!this.e.MouseOnTextLine)
        this.e.MouseStopLine = -1;
      this.e.MouseStopCol = this.e.MouseCol;
      if (this.e.MouseStopTimerOn)
        return true;
      this.SetTimer(this.e.hTerWnd, 9190, 500);
      this.e.MouseStopTimerOn = true;
      this.e.MouseStopDone = false;
    }
    return true;
  }

  internal new bool SetWheelTimer(int lParam, bool reset)
  {
    int num1 = 100;
    if (this.e.WheelTimerUpOn)
    {
      this.KillTimer(this.e.hTerWnd, 9188);
      this.e.WheelTimerUpOn = false;
    }
    if (this.e.WheelTimerDownOn)
    {
      this.KillTimer(this.e.hTerWnd, 9189);
      this.e.WheelTimerDownOn = false;
    }
    if (!reset)
    {
      this.e.WheelCur = tc.WheelFullCur;
      int num2 = (int) (short) COp.HIWORD(lParam);
      if (num2 <= this.e.WheelY - 14)
      {
        this.e.WheelCur = tc.WheelUpCur;
        this.e.WheelTimerUpOn = this.SetTimer(this.e.hTerWnd, 9188, num1 / ((this.e.WheelY - num2) / 14)) != 0;
      }
      else if (num2 >= this.e.WheelY + 14)
      {
        this.e.WheelCur = tc.WheelDownCur;
        this.e.WheelTimerDownOn = this.SetTimer(this.e.hTerWnd, 9189, num1 / ((num2 - this.e.WheelY) / 14)) != 0;
      }
    }
    return true;
  }

  internal new bool SyncHilight()
  {
    this.e.HilightEndRow = this.e.CurLine;
    this.e.HilightEndCol = this.e.CurCol;
    if ((this.e.HilightEndRow > this.e.HilightBegRow || this.e.HilightEndRow == this.e.HilightBegRow && this.e.HilightEndCol > this.e.HilightBegCol) && this.e.TerArg.WordWrap && !this.e.HilightAtCurPos && this.e.HilightEndCol == 0 && this.e.HilightEndRow > 0 && this.LineEndsInBreak(this.e.HilightEndRow - 1))
    {
      --this.e.HilightEndRow;
      this.e.HilightEndCol = this.e.text[this.e.HilightEndRow].len;
      if (this.LineInfo(this.e.HilightEndRow, 32 /*0x20*/) && this.e.HilightEndRow > 0)
      {
        --this.e.HilightEndRow;
        this.e.HilightEndCol = this.e.text[this.e.HilightEndRow].len;
      }
    }
    return true;
  }

  internal new bool TerDrawBorder()
  {
    if (this.e.TerArg.BorderMargin || this.e.InPrintPreview)
    {
      GraphicsState gstate = this.e.TerGr.Save();
      this.ResetTransform(this.e.TerGr);
      this.SetBkMode(this.e.TerGr, 2);
      this.SetBkColor(this.e.TerGr, this.e.TextBorderColor);
      this.TerResetClipRgn();
      COp.RECT terRect = this.e.TerRect;
      if (this.e.InPrintPreview)
      {
        this.HideControl(-1);
        terRect.top += this.e.ToolBarHeight;
        this.ExtTextOut(this.e.TerGr, 0, 0, 2, terRect, (char[]) null, 0, (int[]) null);
      }
      else
      {
        if (this.e.StatusBarHeight != 0 || this.e.ToolBarHeight != 0 || this.e.TerArg.ruler)
        {
          terRect.top += this.e.ToolBarHeight;
          if (this.e.TerArg.ruler)
            terRect.top = this.e.RulerRect.bottom;
          terRect.bottom -= this.e.StatusBarHeight;
        }
        if (terRect.top < this.e.TerWinRect.top && terRect.left < terRect.right)
          this.ExtTextOut(this.e.TerGr, 0, 0, 2, terRect with
          {
            bottom = this.e.TerWinRect.top
          }, (char[]) null, 0, (int[]) null);
        if (terRect.bottom > this.e.TerWinRect.bottom && terRect.left < terRect.right)
          this.ExtTextOut(this.e.TerGr, 0, 0, 2, terRect with
          {
            top = this.e.TerWinRect.bottom
          }, (char[]) null, 0, (int[]) null);
        if (terRect.left < this.e.TerWinRect.left && terRect.top < terRect.bottom)
          this.ExtTextOut(this.e.TerGr, 0, 0, 2, terRect with
          {
            right = this.e.TerWinRect.left
          }, (char[]) null, 0, (int[]) null);
        if (terRect.right > this.e.TerWinRect.right && terRect.top < terRect.bottom)
          this.ExtTextOut(this.e.TerGr, 0, 0, 2, terRect with
          {
            left = this.e.TerWinRect.right
          }, (char[]) null, 0, (int[]) null);
      }
      this.e.TerGr.Restore(gstate);
      this.e.RedrawBorder = false;
    }
    return true;
  }

  internal bool TerEnableRefresh(bool enable)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    this.e.PaintEnabled = enable;
    return true;
  }

  internal Graphics TerGetBufferGr()
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    return this.e.BufGr;
  }

  internal bool TerGetSelection(
    out int pBegLine,
    out int pBegCol,
    out int pEndLine,
    out int pEndCol)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int num1;
    pEndCol = num1 = 0;
    int num2;
    pEndLine = num2 = num1;
    int num3;
    pBegCol = num3 = num2;
    pBegLine = num3;
    if (this.e.HilightType == 0)
      return false;
    pBegLine = this.e.HilightBegRow;
    pBegCol = this.e.HilightBegCol;
    pEndLine = this.e.HilightEndRow;
    pEndCol = this.e.HilightEndCol;
    if (this.e.HilightType == 1)
    {
      pBegCol = 0;
      pEndCol = this.e.text[this.e.HilightEndRow].len;
    }
    return true;
  }

  internal bool TerPictOut(
    Graphics gr,
    int CurX,
    int CurY,
    COp.RECT rect,
    int pict,
    int count,
    int LineNo,
    int baseHeight = 0)
  {
    bool flag1 = true;
    bool flag2 = false;
    if (!this.edit.HiddenText(pict))
    {
      if (this.e.TerFont[pict].FrameType == 0 || (this.e.TerOpFlags & 512 /*0x0200*/) != 0)
      {
        this.e.TerFont[pict].PictX = CurX;
        if (this.e.ScrRtl)
          this.e.TerFont[pict].PictX = this.RtlX(this.e.TerFont[pict].PictX, this.e.TerFont[pict].CharWidth[24], this.e.CurFrame, this.e.pScrSeg[this.e.CurScrSeg]);
        this.e.TerFont[pict].PictX -= this.e.TerWinOrgX;
        this.e.TerFont[pict].PictY = CurY - this.e.TerWinOrgY;
      }
      if (rect.right < this.e.TerWinOrgX && !this.e.ScrRtl)
        return true;
      bool flag3 = this.e.TerArg.PageMode && this.e.TerFont[pict].FrameType != 0 && (this.e.TerOpFlags & 512 /*0x0200*/) == 0 && !this.e.ShowParaMark;
      this.TextSegmentExists(CurX, CurY, rect);
      if (this.e.BkPictId == 0)
      {
        if ((this.e.TerFont[pict].PictType == 0 || this.e.TerFont[pict].PictType == 2 || this.e.TerFont[pict].PictType == 6) && !flag3)
          flag1 = false;
        if (this.e.TerFont[pict].ObjectType == 5 && (this.e.TerFont[pict].flags & 2) == 0 && this.e.ScrollBM == null)
          flag1 = false;
        if (!flag1)
        {
          COp.RECT rect1 = rect;
          if (CurY > rect.top)
          {
            rect1.bottom = CurY;
            this.OurExtTextOut(gr, CurX, CurY, 6, rect1, (char[]) null, 0, (int[]) null);
          }
          if (CurY + this.e.TerFont[pict].height < rect.bottom)
          {
            COp.RECT rect2 = rect with
            {
              top = CurY + this.e.TerFont[pict].height
            };
            this.OurExtTextOut(gr, CurX, CurY, 6, rect2, (char[]) null, 0, (int[]) null);
          }
        }
        else if (flag3 || this.e.TerFont[pict].ImageType == ImageFormat.Emf.Guid || this.e.TerFont[pict].ImageType == ImageFormat.Wmf.Guid)
          this.OurExtTextOut(gr, CurX, CurY, 6, rect, (char[]) null, 0, (int[]) null);
      }
      if (flag3)
        return true;
      if (this.e.BufGr != null && gr != this.e.TerGr && this.e.ScrollBM == null && !flag3 && this.e.TerFont[pict].ObjectType == 5 && (this.e.TerFont[pict].flags & 2) == 0)
        flag2 = true;
      if (flag2)
      {
        this.e.TerFont[pict].flags |= 16 /*0x10*/;
      }
      else
      {
        int pict1 = pict;
        if (this.e.TerFont[pict].anim != null && this.e.TerFont[pict].anim.CurAnim != 0)
        {
          pict1 = this.e.TerFont[pict].anim.CurAnim;
          if (this.e.TerFont[pict1].InUse && (this.e.TerFont[pict1].style & 128 /*0x80*/) != 0)
          {
            this.e.TerFont[pict1].CharWidth[24] = this.e.TerFont[pict].CharWidth[24];
            this.e.TerFont[pict1].height = this.e.TerFont[pict].height;
          }
          else
            pict1 = pict;
        }
        int width = this.e.TerFont[pict1].CharWidth[24];
        int height = this.e.TerFont[pict1].height;
        if (this.e.ScrFrameAngle != 0)
        {
          COp.RECT OurRect = new COp.RECT();
          this.SetRect(out OurRect, CurX, CurY, CurX + width, CurY + height);
          this.FrameRotateRect(ref OurRect, this.e.CurFrame);
          CurX = OurRect.left < OurRect.right ? OurRect.left : OurRect.right;
          CurY = OurRect.top < OurRect.bottom ? OurRect.top : OurRect.bottom;
        }
        this.PictOut(gr, CurX, CurY, width, height, pict1, count, LineNo, true);
        int num1 = (this.e.TerFont[pict].style & 8) != 0 ? 1 : ((this.displayTextFontStyle & 8) != 0 ? 1 : 0);
        bool flag4 = (this.e.TerFont[pict].style & 524288 /*0x080000*/) != 0 || (this.displayTextFontStyle & 524288 /*0x080000*/) != 0;
        int num2 = flag4 ? 1 : 0;
        if ((num1 | num2) != 0)
        {
          Pen SolidPen = new Pen(this.e.TerFont[pict].TextColor);
          int num3 = this.e.TerTextMet.tmHeight - this.e.TerTextMet.tmDescent;
          COp.OUTLINETEXTMETRIC ofm;
          if (this.GetFontMetrics(gr, this.e.TerCurFont, out ofm))
            num3 = (int) ofm.otmsStrikeoutSize;
          if (this.e.ScrFrameAngle == 0)
          {
            int num4 = CurY + height - num3 - (this.e.TerFont[pict].height - this.e.TerFont[pict].BaseHeight);
            this.DrawShadowLine(gr, CurX, num4, CurX + width, num4, SolidPen, (Pen) null);
            if (flag4)
              this.DrawShadowLine(gr, CurX, num4 - 2, CurX + width, num4 - 2, SolidPen, (Pen) null);
          }
          else if (this.e.ScrFrameAngle == 90)
          {
            this.DrawShadowLine(gr, CurX + width - num3, CurY, CurX + width - num3, CurY + height, SolidPen, (Pen) null);
            if (flag4)
              this.DrawShadowLine(gr, CurX + width - num3 - 2, CurY, CurX + width - num3 - 2, CurY + height, SolidPen, (Pen) null);
          }
        }
      }
      this.e.TerFont[pict].flags |= 64 /*0x40*/;
      if ((this.e.TextBorder & 65776 /*0x0100F0*/) != 0)
        this.DrawParaBorder(gr, rect, true);
      if (this.e.TerFont[pict].anim != null)
        this.SetAnimTimer(pict);
    }
    return true;
  }

  internal new bool TerResetClipRgn()
  {
    this.e.TerGr.ResetClip();
    return true;
  }

  internal bool TerSetBorderColor(Color BkColor)
  {
    this.e.TextBorderColor = BkColor;
    this.e.Invalidate();
    return true;
  }

  internal new bool TerSetCharHilight()
  {
    this.KillHilightTimer();
    if (this.e.PictureClicked)
      this.e.HilightType = 0;
    if (((int) this.GetKeyState(16 /*0x10*/) & 32768 /*0x8000*/) != 0)
    {
      if (this.e.HilightType != 2)
      {
        this.e.HilightType = 2;
        this.e.HilightBegRow = this.e.HilightEndRow = this.e.CurLine;
        this.e.HilightBegCol = this.e.HilightEndCol = this.e.CurCol;
      }
      this.e.StretchHilight = true;
    }
    else if (this.e.HilightType == 2)
    {
      this.e.HilightType = 0;
      this.PaintTer();
    }
    this.e.InputFontId = -1;
    this.e.PictureClicked = this.e.FrameClicked = false;
    this.e.HilightWithColCursor = false;
    return true;
  }

  internal new bool TerSetClipRgn()
  {
    Rectangle rectangle = new Rectangle(this.e.TerWinOrgX, this.e.TerWinOrgY, this.e.TerWinRect.right - this.e.TerWinRect.left, this.e.TerWinRect.bottom - this.e.TerWinRect.top);
    if (!this.OpDCIsOpen(this.e.TerGr))
      this.SetTerGraphicsClip(rectangle);
    return true;
  }

  internal bool TerSetCtlColor(Color BkColor, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    this.e.TextDefBkColor = this.e.TextBorderColor = BkColor;
    if (repaint)
      this.e.TerRepaint(true);
    else
      this.e.Invalidate();
    return true;
  }

  internal bool TerSetDefBkColor(Color BackColor, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (BackColor != this.e.TextDefBkColor)
    {
      this.e.TextDefBkColor = this.e.TextBorderColor = BackColor;
      if (repaint)
        this.TerRepaint(true);
    }
    return true;
  }

  internal new bool TerSetHilight(MouseButtons button, int lParam, bool scrolling)
  {
    if (this.e.TotalLines == 0)
      return true;
    int num1 = 0;
    bool flag1 = false;
    bool flag2 = false;
    int num2 = (this.e.TerFlags3 & 1073741824 /*0x40000000*/) == 0 ? 1 : 0;
    bool flag3 = this.e.HilightType == 0;
    if (num2 != 0)
    {
      int mouseLine = this.e.MouseLine;
      int mouseCol = this.e.MouseCol;
      bool flag4 = (this.e.TerOpFlags & 524288 /*0x080000*/) != 0;
      this.KillHilightTimer();
      if (scrolling)
      {
        flag1 = true;
      }
      else
      {
        int num3 = Math.Abs((int) (short) COp.LOWORD(lParam) - this.e.MouseX);
        int num4 = Math.Abs((int) (short) COp.HIWORD(lParam) - this.e.MouseY);
        int num5 = this.e.TerTextMet.tmAveCharWidth / 4;
        flag2 = num3 > num5 || num4 > (this.e.TerTextMet.tmHeight + this.e.TerTextMet.tmExternalLeading) / 8;
        if (this.e.VerySmallMovement & flag2)
          this.e.VerySmallMovement = false;
        this.TerMousePos(lParam, true);
        if (this.e.TblSelCursShowing && this.e.MouseOverShoot == ' ' && this.e.HilightType == 2 && this.InSameTable(this.e.text[this.e.HilightBegRow].cid, this.e.text[this.e.MouseLine].cid))
          return this.HilightTableCol(this.e.MouseLine, false, true);
      }
      if (flag2)
        this.e.FrameClicked = false;
      if ((button == MouseButtons.Left || button == MouseButtons.Middle || button == MouseButtons.Right | scrolling) && this.e.MouseOverShoot != ' ')
      {
        this.e.PaintEnabled = false;
        this.ScrollText();
        this.e.PaintEnabled = true;
        this.e.MouseLine = this.e.CurLine;
        if ((this.e.text[this.e.CurLine].flags2 & 32 /*0x20*/) == 0)
          this.e.MouseCol = this.e.CurCol;
        flag1 = true;
      }
      if (this.e.HilightType != 0 && !this.e.StretchHilight && (button == MouseButtons.Left || button == MouseButtons.Middle || button == MouseButtons.Right))
      {
        this.e.HilightType = 0;
        this.e.WrapFlag = 0;
        int num6 = this.e.PictureClicked ? 1 : 0;
        this.PaintTer();
      }
      if (this.e.HilightType == 0)
      {
        switch (button)
        {
          case MouseButtons.Left:
            this.e.HilightType = 2;
            break;
          case MouseButtons.Right:
            this.e.HilightType = 1;
            break;
        }
        if (this.e.HilightType != 0)
        {
          this.e.HilightBegRow = mouseLine;
          this.e.HilightBegCol = mouseCol;
          this.e.HilightEndRow = this.e.HilightEndCol = -1;
          this.e.StretchHilight = true;
          this.e.HilightWithColCursor = false;
          this.e.Capture = true;
        }
        this.e.VerySmallMovement = true;
      }
      int num7 = this.e.MouseCol;
      if (!flag1 && this.e.MouseLine < this.e.TotalLines - 1 && this.e.MouseCol < this.e.text[this.e.MouseLine].len && (this.e.HilightEndRow > this.e.HilightBegRow || this.e.HilightEndRow == this.e.HilightBegRow && this.e.HilightEndCol >= this.e.HilightBegCol))
      {
        ++num7;
        if (this.e.TerArg.WordWrap && num7 >= this.e.text[this.e.MouseLine].len)
          --num7;
      }
      if (!flag1 && num7 == this.e.text[this.e.MouseLine].len - 1 && (this.e.TerOpFlags & 524288 /*0x080000*/) != 0 && (!flag3 || !flag4))
        ++num7;
      if (this.e.DblClickHilight && this.e.MouseLine != this.e.DblClickEndRow)
        this.e.DblClickHilight = false;
      if (this.e.DblClickHilight && num7 >= this.e.HilightBegCol && num7 < this.e.DblClickEndCol)
        num7 = this.e.DblClickEndCol;
      if (this.e.StretchHilight && (this.e.HilightEndRow != this.e.MouseLine || this.e.HilightEndCol != num7))
      {
        if (this.e.HilightEndRow >= 0 && this.e.HilightEndRow < this.e.TotalLines)
          num1 = this.e.text[this.e.HilightEndRow].cid;
        this.e.HilightEndRow = this.e.MouseLine;
        this.e.HilightEndCol = num7;
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
        if (flag1)
        {
          this.e.CurLine = this.e.MouseLine;
          this.e.CurCol = num7;
          this.e.CurRow = this.e.CurLine - this.e.BeginLine;
          int num8 = this.e.PictureClicked ? 1 : 0;
          this.PaintTer();
        }
        else
        {
          int num9 = this.e.CurLine;
          int num10 = this.e.MouseLine;
          if (this.e.CurLine > this.e.MouseLine)
          {
            num9 = this.e.MouseLine;
            num10 = this.e.CurLine;
          }
          bool stretchHilight = this.e.StretchHilight;
          this.e.StretchHilight = false;
          bool frameRefreshEnabled = this.e.FrameRefreshEnabled;
          this.e.FrameRefreshEnabled = false;
          if (this.e.TerArg.PageMode)
          {
            this.e.CurLine = this.e.MouseLine;
            if (this.e.CurCol >= this.e.text[this.e.MouseLine].len && this.e.text[this.e.MouseLine].len > 0)
              this.e.CurCol = this.e.text[this.e.MouseLine].len - 1;
            this.e.PaintBegLine = num9 - 1;
            this.e.PaintEndLine = num10 + 1;
            this.e.PaintFlag = 3;
            if ((this.e.TerFlags2 & 32 /*0x20*/) != 0)
              this.e.PaintFlag = 4;
            if (num9 == num10)
              this.e.PaintFlag = 2;
            if (this.e.text[this.e.HilightBegRow].cid != this.e.text[this.e.HilightEndRow].cid)
              this.e.PaintFlag = 4;
            else if (this.e.text[this.e.HilightEndRow].cid != num1)
              this.e.PaintFlag = 4;
            else if ((this.e.text[this.e.HilightBegRow].flags & 16384 /*0x4000*/) != 0)
              this.e.PaintFlag = 4;
            else if (this.e.HasOverlayingFrames)
              this.e.PaintFlag = 4;
            else if (this.LineInfo(this.e.HilightBegRow, 1024 /*0x0400*/))
              this.e.PaintFlag = 4;
            else if (this.LineTextAngle(this.e.HilightBegRow) > 0)
              this.e.PaintFlag = 4;
            this.e.WrapFlag = 0;
            int hilightType = this.e.HilightType;
            int num11 = this.e.PictureClicked ? 1 : 0;
            this.TerRepaint(false);
          }
          else
          {
            this.e.CurLine = this.e.MouseLine;
            this.e.CurRow = this.e.CurLine - this.e.BeginLine;
            this.e.PaintFlag = 4;
            this.e.WrapFlag = 0;
            int hilightType = this.e.HilightType;
            int num12 = this.e.PictureClicked ? 1 : 0;
            this.PaintTer();
          }
          this.e.StretchHilight = stretchHilight;
          this.e.FrameRefreshEnabled = frameRefreshEnabled;
          this.e.CurLine = this.e.MouseLine;
          this.e.CurCol = num7;
          if (this.e.CurLine + 1 == this.e.TotalLines && this.e.CurCol >= this.e.text[this.e.CurLine].len && this.e.CurCol > 0)
            --this.e.CurCol;
          this.e.CurRow = this.e.CurLine - this.e.BeginLine;
          this.OurSetCaretPos();
        }
      }
      if (flag1 && !this.PeekMessage(out COp.MSG _, IntPtr.Zero, 512 /*0x0200*/, 522, 2) && !this.e.HilightTimerOn)
        this.e.HilightTimerOn = this.SetTimer(this.e.hTerWnd, 9183, 40) != 0;
    }
    return true;
  }

  internal bool TerSetStatusColor(Color TextColor, Color BkColor)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    this.e.StatusColor = TextColor;
    this.e.StatusBkColor = BkColor;
    if (this.e.ToolbarBrush != null)
      this.e.ToolbarBrush.Dispose();
    this.e.ToolbarBrush = (Brush) new SolidBrush(this.e.StatusBkColor);
    if (this.e.TerTlb != null)
      this.e.TerTlb.Invalidate();
    if (this.e.PvTlb != null)
      this.e.PvTlb.Invalidate();
    this.e.RulerPending = true;
    this.e.Invalidate();
    return true;
  }

  internal new bool TextSegmentExists(int x, int y, COp.RECT rect)
  {
    if (this.e.ScrollBM == null)
    {
      if ((this.e.TerFlags2 & 32 /*0x20*/) != 0 && (this.e.TerOpFlags & 32 /*0x20*/) != 0 && this.e.transparent)
      {
        if (this.e.BkPictBM != null)
        {
          this.DrawBkPictRect(rect, this.e.BufGr);
        }
        else
        {
          this.PageColor();
          Color color = this.SetBkColor(this.e.BufGr, this.PageColor());
          this.ExtTextOut(this.e.BufGr, 0, 0, 2, rect, (char[]) null, 0, (int[]) null);
          this.SetBkColor(this.e.BufGr, color);
        }
      }
      else if (this.e.BkPictBM != null)
        this.DrawBkPictRect(rect, this.e.BufGr);
      if (this.e.DirtyRect.left == -1)
      {
        this.e.DirtyRect = rect;
      }
      else
      {
        if (rect.left < this.e.DirtyRect.left)
          this.e.DirtyRect.left = rect.left;
        if (rect.top < this.e.DirtyRect.top)
          this.e.DirtyRect.top = rect.top;
        if (rect.right > this.e.DirtyRect.right)
          this.e.DirtyRect.right = rect.right;
        if (rect.bottom > this.e.DirtyRect.bottom)
          this.e.DirtyRect.bottom = rect.bottom;
      }
    }
    return false;
  }

  internal new bool VerThumbPos(int wParam)
  {
    if (this.e.InPrintPreview)
      this.PreviewPageVert((int) COp.HIWORD(wParam));
    else if ((int) (ushort) this.e.VerScrollPos != (int) COp.HIWORD(wParam))
    {
      if (this.e.TerArg.PageMode)
      {
        this.PgmPageVert((int) COp.HIWORD(wParam));
      }
      else
      {
        int z = 1000 - this.e.VerThumbSize;
        this.TerPosLine(1 + this.MulDiv(this.LastScrollBeginLine(), (int) COp.HIWORD(wParam), z));
        this.e.BeginLine = this.e.CurLine;
        this.e.CurRow = 0;
        this.PaintTer();
      }
    }
    return true;
  }

  internal new bool WriteBreakLine(
    Graphics gr,
    string BreakString,
    int x,
    int y,
    int width,
    Pen pen)
  {
    bool scrRtl = this.e.ScrRtl;
    if (this.e.ZoomPercent <= 50)
      this.e.TerCurFont = this.e.TerFont[0].font;
    else
      this.e.TerCurFont = this.e.RulerFont;
    this.OurSetFont(gr, this.e.hRulerFont);
    COp.SIZE size;
    this.GetTextExtentPoint(gr, this.e.RulerFont, BreakString, BreakString.Length, out size);
    if (size.cx >= width * 3 / 4)
      size.cx = 0;
    if (this.e.ScrRtl)
      x = this.RtlX(x, width, this.e.CurFrame, new tc.StrLineSeg());
    int num = (width - size.cx) / 2;
    if (num < 0)
      num = 0;
    Color color = this.SetTextColor(gr, Color.Black);
    if (size.cx > 0)
      this.TextOut(gr, x + num, y - size.cy / 2, BreakString, BreakString.Length);
    this.SetTextColor(gr, color);
    this.e.ScrRtl = false;
    if (pen != null)
    {
      this.DrawShadowLine(gr, x, y, x + num, y, pen, (Pen) null);
      this.DrawShadowLine(gr, x + num + size.cx, y, x + width, y, pen, (Pen) null);
    }
    else
    {
      this.DrawDottedLine(gr, x, y, x + num, y, Color.Black);
      this.DrawDottedLine(gr, x + num + size.cx, y, x + width, y, Color.Black);
    }
    this.e.ScrRtl = scrRtl;
    return true;
  }

  internal new bool WriteFrameSpace(Graphics gr, int line, int y, int height)
  {
    bool flag = (this.e.text[line].flags2 & 32 /*0x20*/) != 0;
    if (tc.DebugMode)
      this.misc.dm(nameof (WriteFrameSpace));
    int num = this.UnitToScrX(this.e.text[line].tabw.FrameX + this.e.text[line].x) - this.e.NextX;
    if (num < 0)
      num = 0;
    int scrX = this.UnitToScrX(this.e.text[line].tabw.FrameWidth);
    this.e.text[line].tabw.FrameScrWidth = num + scrX;
    COp.RECT rect;
    if (num > 0)
    {
      rect.left = this.e.NextX;
      this.e.NextX = rect.right = rect.left + num;
      rect.top = y;
      rect.bottom = y + height;
      this.SetColor(this.e.TerGr, 'T', 0);
      if (!flag)
        this.TerTextOut(gr, this.e.NextX, y, rect, (char[]) null, 0, (ushort[]) null, 0);
    }
    if (scrX > 0)
    {
      rect.left = this.e.NextX;
      this.e.NextX = rect.right = rect.left + scrX;
      rect.top = y;
      rect.bottom = y + height;
      this.SetColor(this.e.TerGr, 'T', 0);
      this.e.ParaFrameSpace = true;
      if (!flag)
        this.TerTextOut(gr, this.e.NextX, y, rect, (char[]) null, 0, (ushort[]) null, 0);
      this.e.ParaFrameSpace = false;
    }
    return true;
  }

  internal bool WriteSpace(
    Graphics gr,
    int line,
    int y,
    int RectY,
    int height,
    int BaseHeight,
    int width)
  {
    COp.RECT rect;
    rect.left = this.e.NextX;
    this.e.NextX = rect.right = rect.left + width;
    rect.top = RectY;
    rect.bottom = RectY + height;
    this.TerTextOut(gr, this.e.NextX, y, rect, (char[]) null, 0, (ushort[]) null, 0);
    int num = this.e.TerFont[this.e.CurFmtId].BaseHeight / 4;
    if (num >= width)
      num = width - 1;
    if (num < 1)
      num = 1;
    rect.left = rect.left + width / 2 - num / 2;
    rect.right = rect.left + num;
    rect.top = rect.top + BaseHeight - this.e.TerFont[this.e.CurFmtId].BaseHeight / 2;
    rect.bottom = rect.top + num;
    if (num <= 4)
      this.OurFillRectangle(gr, this.e.CurForeColor, rect);
    else
      this.OurEllipse(gr, true, this.e.CurForeColor, (Pen) null, rect);
    return true;
  }

  internal new bool WriteSpaceRect(Graphics gr, COp.RECT rect)
  {
    if (!this.e.TerArg.PageMode || !this.e.ContainsParaFrames || this.e.frame[this.e.CurFrame].ParaFrameId > 0)
    {
      this.TerTextOut(gr, rect.left, rect.top, rect, (char[]) null, 0, (ushort[]) null, 0);
      return true;
    }
    int FrameX;
    int FrameWidth;
    this.GetFrameSpace(-1, rect, out FrameX, out FrameWidth, out tc.SkipInt);
    if (FrameX == 0 && FrameWidth == 0)
    {
      this.TerTextOut(gr, rect.left, rect.top, rect, (char[]) null, 0, (ushort[]) null, 0);
      return true;
    }
    int num = rect.left + this.UnitToScrX(FrameX);
    FrameWidth = this.UnitToScrX(FrameWidth);
    COp.RECT rect1;
    rect1.top = rect.top;
    rect1.bottom = rect.bottom;
    if (num > rect.left)
    {
      rect1.left = rect.left;
      rect1.right = num;
      if (rect1.right > rect.right)
        rect1.right = rect.right;
      this.TerTextOut(gr, rect1.left, rect1.top, rect1, (char[]) null, 0, (ushort[]) null, 0);
    }
    if (FrameWidth > 0 && num < rect.right)
    {
      rect1.left = num;
      rect1.right = num + FrameWidth;
      if (rect1.right > rect.right)
        rect1.right = rect.right;
      this.e.ParaFrameSpace = true;
      if (this.e.BkPictId == 0)
        this.TerTextOut(gr, rect1.left, rect1.top, rect1, (char[]) null, 0, (ushort[]) null, 0);
      this.e.ParaFrameSpace = false;
    }
    if (num + FrameWidth < rect.right)
    {
      rect1.left = num + FrameWidth;
      rect1.right = rect.right;
      this.TerTextOut(gr, rect1.left, rect1.top, rect1, (char[]) null, 0, (ushort[]) null, 0);
    }
    return true;
  }

  internal new bool WriteTab(
    Graphics gr,
    int line,
    int y,
    int RectY,
    int height,
    int BaseHeight,
    int width)
  {
    int tabId = this.e.PfmtId[this.e.text[line].pfmt].TabId;
    int nextX = this.e.NextX;
    if (this.e.TerArg.PageMode)
      nextX -= this.e.frame[this.e.CurFrame].x + this.e.frame[this.e.CurFrame].SpaceLeft;
    byte pFlags;
    this.GetTabPos(this.e.text[line].pfmt, this.e.TerTab[tabId], nextX, out tc.SkipInt, out tc.SkipInt, out pFlags, true);
    this.e.TabFlags = pFlags;
    ++this.e.NextTab;
    COp.RECT rect;
    rect.left = this.e.NextX;
    this.e.NextX = rect.right = rect.left + width;
    rect.top = RectY;
    rect.bottom = RectY + height;
    this.TerTextOut(gr, this.e.NextX, y, rect, (char[]) null, 0, (ushort[]) null, 0);
    if (this.e.ShowParaMark)
    {
      int num1 = 10;
      int num2 = 4;
      Point[] pt = new Point[3];
      int x1 = rect.left + (rect.right - rect.left) / 2 - num1 / 2;
      if (x1 < rect.left)
      {
        x1 = rect.left;
        num1 = rect.right - rect.left;
      }
      int x2 = x1 + num1;
      if (x2 > rect.right)
        x2 = rect.right;
      if (x2 - x1 > num1 / 2)
      {
        int num3 = rect.top + BaseHeight - 5;
        if (num3 <= rect.top)
          num3 = rect.top + 1;
        this.DrawShadowLine(gr, x1, num3, x2, num3, Pens.Black, (Pen) null);
        pt[0].X = x2;
        pt[0].Y = num3;
        pt[1].X = x2 - num2;
        if (pt[1].X < rect.left)
          pt[1].X = rect.left;
        pt[1].Y = num3 - num2 / 2;
        pt[2].X = x2 - num2;
        if (pt[2].X < rect.left)
          pt[2].X = rect.left;
        pt[2].Y = num3 + num2 / 2;
        if (this.e.ScrRtl)
        {
          for (int index = 0; index < 3; ++index)
          {
            tc.StrLineSeg pSeg = new tc.StrLineSeg();
            pt[index].X = this.RtlX(pt[index].X, 0, this.e.CurFrame, pSeg);
          }
        }
        this.OurFillPolygon(gr, Brushes.Black, Color.Black, pt);
      }
    }
    if (this.e.TabFlags == (byte) 1)
    {
      int num = RectY + BaseHeight;
      if (num <= RectY)
        num = RectY + 1;
      this.DrawDottedLine(gr, rect.left, num, rect.right, num, this.e.CurForeColor);
    }
    else if (this.e.TabFlags == (byte) 2)
    {
      Pen SolidPen = new Pen(this.e.CurForeColor);
      int num = RectY + BaseHeight - 2;
      if (num <= RectY)
        num = RectY + 1;
      SolidPen.DashStyle = DashStyle.Dot;
      this.DrawShadowLine(gr, rect.left, num, rect.right, num, SolidPen, (Pen) null);
      SolidPen.Dispose();
    }
    else if (this.e.TabFlags == (byte) 4 || (this.e.TerFont[this.e.CurFmtId].style & 524545) != 0)
    {
      int num4 = (this.e.TerFont[this.e.CurFmtId].style & 1) != 0 ? 1 : 0;
      bool flag1 = (this.e.TerFont[this.e.CurFmtId].style & 256 /*0x0100*/) != 0;
      bool flag2 = (this.e.TerFont[this.e.CurFmtId].style & 524288 /*0x080000*/) != 0;
      Pen SolidPen = new Pen(this.e.CurForeColor, (this.e.TerFont[this.e.CurFmtId].style & 2) != 0 ? 1f : 0.0f);
      int num5 = RectY + BaseHeight;
      if (num5 <= RectY)
        num5 = RectY + 1;
      int num6 = num5 - BaseHeight / 2 - 1;
      int num7 = flag1 ? 1 : 0;
      if ((num4 | num7) != 0)
      {
        this.DrawShadowLine(gr, rect.left, num5, rect.right, num5, SolidPen, (Pen) null);
        if (flag1)
        {
          int num8 = num5 + 2;
          this.DrawShadowLine(gr, rect.left, num8, rect.right, num8, SolidPen, (Pen) null);
        }
      }
      if (flag2)
      {
        this.DrawShadowLine(gr, rect.left, num6, rect.right, num6, SolidPen, (Pen) null);
        int num9 = num6 + 2;
        this.DrawShadowLine(gr, rect.left, num9, rect.right, num9, SolidPen, (Pen) null);
      }
      SolidPen.Dispose();
    }
    if ((this.e.TerFont[this.e.CurFmtId].style & 1) != 0 && !this.edit.HiddenText(this.e.CurFmtId))
      this.DrawShadowLine(gr, rect.left, y + this.e.TerFont[this.e.CurFmtId].BaseHeight + 1, rect.right, y + this.e.TerFont[this.e.CurFmtId].BaseHeight + 1, Pens.Black, (Pen) null);
    this.e.TabFlags = (byte) 0;
    return true;
  }
}
