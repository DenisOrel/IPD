// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.CInit
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class CInit : COp
{
  internal CInit(ImRtfEditor ImRtfEditor)
    : base(ImRtfEditor)
  {
  }

  internal new bool AllocArrayMem()
  {
    this.e.MaxPages = 5;
    if (this.e.TerArg.PrintView || this.e.TerArg.PageMode)
      this.e.MaxFrames = 50;
    else
      this.e.MaxFrames = 1;
    this.e.text = new tc.ClsLinePtr[this.e.MaxLines];
    for (int index = 0; index < this.e.MaxLines; ++index)
      this.e.text[index] = (tc.ClsLinePtr) null;
    this.e.TerFont = new tc.StrFont[this.e.MaxFonts + 1];
    this.e.PrtFont = new tc.StrPrtFont[this.e.MaxFonts + 1];
    this.e.PageInfo = new tc.StrPage[this.e.MaxPages + 1];
    this.e.TerBlt = new tc.StrBlt[this.e.MaxBlts + 1];
    this.e.list = new tc.StrList[this.e.MaxLists + 1];
    this.e.ListOr = new tc.StrListOr[this.e.MaxListOr + 1];
    this.e.TerSect = new tc.StrSect[this.e.MaxSects + 1];
    this.e.TerSect1 = new tc.StrSect1[this.e.MaxSects + 1];
    this.e.TableAux = new tc.StrTableAux[this.e.MaxTableRows + 1];
    this.e.CellAux = new tc.StrCellAux[this.e.MaxCells + 1];
    this.e.CharTag = new tc.StrCharTag[this.e.MaxCharTags + 1];
    this.e.RowX = new int[this.e.MaxLinesPerWin + 1];
    this.e.RowY = new int[this.e.MaxLinesPerWin + 1];
    this.e.RowHeight = new int[this.e.MaxLinesPerWin + 1];
    this.e.undo = new tc.StrUndo[this.e.MaxUndos + 1];
    this.e.HlSeg = new tc.StrHlSeg[this.e.MaxHlSegs + 1];
    this.e.ScrCtl = new tc.StrScrCtl[this.e.MaxScrCtls + 1];
    this.e.PendPict = new tc.StrPendPict[this.e.MaxPendPicts + 1];
    this.e.reviewer = new tc.StrReviewer[this.e.MaxReviewers + 1];
    if (tc.GlbPrtDC == null)
      tc.GlbPrtDC = new tc.StrGlbPrtDC[100];
    this.e.TerTab = new tc.StrTab[600];
    this.e.PfmtId = new tc.StrPfmt[this.e.MaxPfmts + 1];
    this.e.StyleId = new tc.StrStyleId[this.e.MaxSID + 1];
    this.e.frame = new tc.StrFrame[this.e.MaxFrames + 1];
    this.e.ParaFrame = new tc.StrParaFrame[this.e.MaxParaFrames + 1];
    this.e.DragObj = new tc.StrDragObj[this.e.MaxDragObjs + 1];
    this.e.TableRow = new tc.StrTableRow[this.e.MaxTableRows + 1];
    this.e.cell = new tc.StrCell[this.e.MaxCells + 1];
    this.e.ImageMap = new tc.StrImageMap[50];
    lock (tc.GlbFontLock)
    {
      if (tc.GlbFont == null)
      {
        tc.GlbFont = new tc.StrGlbFont[500];
        tc.TotalGlbFonts = 0;
      }
    }
    for (int index = 0; index < this.e.MaxFonts; ++index)
    {
      int[] numArray;
      this.e.PrtFont[index].CharWidth = numArray = (int[]) null;
      this.e.TerFont[index].CharWidth = numArray;
    }
    for (int index = 0; index < this.e.MaxLinesPerWin; ++index)
    {
      int num1;
      this.e.RowHeight[index] = num1 = 0;
      int num2;
      this.e.RowY[index] = num2 = num1;
      this.e.RowX[index] = num2;
    }
    this.e.reviewer[0] = new tc.StrReviewer();
    this.e.reviewer[0].name = "";
    this.e.TotalReviewers = 1;
    return true;
  }

  internal new bool AllocLinePtr(int line)
  {
    if (this.e.LinePtrCount > 0)
    {
      --this.e.LinePtrCount;
      this.e.text[line] = this.e.LinePtrCache[this.e.LinePtrCount];
    }
    else
      this.e.text[line] = new tc.ClsLinePtr();
    return true;
  }

  internal new bool AllocWrapBuf(int size)
  {
    if (size == 0)
    {
      int num1 = this.GetDeviceCaps(this.e.TerGr, 10) / (this.e.TerTextMet.tmHeight + this.e.TerTextMet.tmExternalLeading);
      int num2 = this.GetDeviceCaps(this.e.TerGr, 8) / this.e.TerTextMet.tmAveCharWidth;
      this.e.WrapMaxLines = num1 + 5;
      this.e.WrapBufferSize = (num2 + 2) * (num1 + 2);
      if (this.e.WrapBufferSize < 0)
        this.e.WrapBufferSize = 20000;
    }
    else
    {
      if (this.True(this.e.wrap))
      {
        this.e.wrap = (char[]) null;
        this.e.WrapCfmt = (ushort[]) null;
        this.e.WrapCtid = (ushort[]) null;
        this.e.WrapCharWidth = (ushort[]) null;
        this.e.WrapCharWidthOrder = (int[]) null;
        this.e.WrapCharWidthDX = (int[]) null;
        this.e.WrapCharWidthText = (ushort[]) null;
        this.e.WrapCharWidthClass = (byte[]) null;
      }
      this.e.WrapBufferSize = size;
    }
    this.e.wrap = new char[this.e.WrapBufferSize];
    this.e.WrapCfmt = new ushort[this.e.WrapBufferSize];
    this.e.WrapCtid = new ushort[this.e.WrapBufferSize];
    return true;
  }

  internal static bool CheckEval(bool KeyProvided, string key)
  {
    if (!tc.eval)
      return true;
    tc.eval = false;
    tc.expired = false;
    tc.disable = false;
    return true;
  }

  internal new tc.ClsLinePtr CloneLine(int line) => this.e.text[line].Copy();

  internal new tc.ClsLinePtr[] CloneLinePtr()
  {
    tc.ClsLinePtr[] clsLinePtrArray = new tc.ClsLinePtr[this.e.MaxLines + 1];
    for (int line = 0; line < this.e.TotalLines; ++line)
    {
      clsLinePtrArray[line] = this.CloneLine(line);
      if (clsLinePtrArray[line] == null)
        return (tc.ClsLinePtr[]) null;
    }
    return clsLinePtrArray;
  }

  internal bool CloseTer(bool ForcedClose)
  {
    if (this.e.UseWin && !this.e.IsHandleCreated)
      return false;
    if (this.e.ImeEnabled)
      this.DisableIme(false);
    if (this.e.TerArg.InputType == 'F')
    {
      if (ForcedClose)
      {
        if (this.True(this.e.TerArg.modified) && !this.e.TerArg.ReadOnly && !this.TerSave(this.e.DocName, false))
          return false;
      }
      else if (this.True(this.e.TerArg.modified) && !this.e.TerArg.ReadOnly)
      {
        DialogResult dialogResult;
        if (DialogResult.Yes == (dialogResult = this.ShowMessage(this.e.MsgString[(int) sbyte.MaxValue], "", MessageBoxButtons.YesNoCancel)))
        {
          if (!this.TerSave(this.e.DocName, false))
            return false;
        }
        else
        {
          if (dialogResult == DialogResult.Cancel)
            return false;
          this.e.SendMessageToParent(2728, (int) this.e.hTerWnd, 0, false);
        }
      }
    }
    this.e.SendMessageToParent(2724, (int) this.e.hTerWnd, 0, false);
    for (int line = 0; line < this.e.TotalLines; ++line)
      this.init.FreeLine(line);
    this.e.TerArg.InitLine = this.e.CurLine + 1;
    this.e.HoldMessages = true;
    this.e.TerArg.open = false;
    this.e.TerArg.hBuffer = (string) null;
    if (this.e.TerTlb != null)
      this.DestroyToolBar();
    this.e.TerArg.ToolBar = false;
    this.FreeToolbar();
    for (int index = 0; index < this.e.TotalTlbCustIds; ++index)
    {
      if (this.e.TlbCustId[index].InUse)
      {
        this.e.TlbCustId[index].image = (Image) null;
        this.e.TlbCustId[index].pBalloon = (string) null;
      }
    }
    this.e.TotalTlbCustIds = 0;
    if (this.True(this.e.wrap))
    {
      this.e.wrap = (char[]) null;
      this.e.WrapCfmt = (ushort[]) null;
      this.e.WrapCtid = (ushort[]) null;
      this.e.WrapCharWidth = (ushort[]) null;
      this.e.WrapCharWidthOrder = (int[]) null;
      this.e.WrapCharWidthDX = (int[]) null;
      this.e.WrapCharWidthText = (ushort[]) null;
      this.e.WrapCharWidthClass = (byte[]) null;
    }
    if (this.e.UndoCount > 0)
      this.ReleaseUndo();
    this.e.undo = (tc.StrUndo[]) null;
    this.FreeRtfInfo();
    this.DeleteTextMap(false);
    this.FreeImageMapTable();
    this.e.TerFlags = this.ResetTerFlag(134217728 /*0x08000000*/);
    for (int idx = 0; idx < this.e.TotalFonts; ++idx)
    {
      if (this.e.TerFont[idx].InUse)
        this.DeleteTerObject(idx);
    }
    if (tc.TotalGlbFonts == 0)
    {
      if (this.True(this.e.RulerFont))
        this.e.RulerFont.Dispose();
      if (this.e.hRulerFont != IntPtr.Zero)
        this.DeleteObject(this.e.hRulerFont);
      if (this.True(this.e.RulerFontBold))
        this.e.RulerFontBold.Dispose();
      if (this.e.hRulerFontBold != IntPtr.Zero)
        this.DeleteObject(this.e.hRulerFontBold);
      if (this.True(this.e.StatusFont))
        this.e.StatusFont.Dispose();
      if (this.e.hStatusFont != IntPtr.Zero)
        this.DeleteObject(this.e.hStatusFont);
    }
    if (this.e.hPopup != null)
      this.e.hPopup.Dispose();
    this.ExitOle();
    if (this.e.RepageTimerOn)
      this.KillTimer(this.e.hTerWnd, 9182);
    if (this.e.MouseStopTimerOn)
      this.KillTimer(this.e.hTerWnd, 9190);
    if (tc.eval)
      this.KillTimer(this.e.hTerWnd, 9184);
    this.KillHilightTimer();
    if (this.True(this.e.ScrollBM))
      this.e.ScrollBM.Dispose();
    if (this.True(this.e.BkPictBM))
      this.DisposeBkPictBM();
    if (this.True(this.e.InitBkBM))
      this.e.InitBkBM.Dispose();
    if (this.True(this.e.ToolbarBrush))
      this.e.ToolbarBrush.Dispose();
    if (this.True(this.e.PrtGr) && this.e.PrtGr != this.e.TerGr)
    {
      if (this.e.PrivatePrtDC)
        this.e.PrtGr.Dispose();
      else
        --tc.GlbPrtDC[this.e.GlbPrtDCId].UseCount;
    }
    this.ResetBufBM();
    if (this.e.TerGr != null)
      this.e.TerGr.Dispose();
    if (this.e.hMemDC != IntPtr.Zero)
      COp.Win32.DeleteDC(this.e.hMemDC);
    if (this.e.MemGr != null)
      this.e.MemGr.Dispose();
    this.e.MemGr = (Graphics) null;
    if (!this.e.WindowDestroyed)
      this.e.WindowDestroyed = true;
    --tc.TerOpenCount;
    this.e.TerShrinkFontTable();
    this.e.LinePtrCache = (tc.ClsLinePtr[]) null;
    this.e.frame = (tc.StrFrame[]) null;
    this.e.ParaFrame = (tc.StrParaFrame[]) null;
    this.e.TerBlt = (tc.StrBlt[]) null;
    this.e.TerFont = (tc.StrFont[]) null;
    this.e.PrtFont = (tc.StrPrtFont[]) null;
    this.e.TerTab = (tc.StrTab[]) null;
    this.e.TerSect = (tc.StrSect[]) null;
    this.e.TerSect1 = (tc.StrSect1[]) null;
    this.e.PageInfo = (tc.StrPage[]) null;
    this.e.PfmtId = (tc.StrPfmt[]) null;
    this.e.StyleId = (tc.StrStyleId[]) null;
    this.e.DragObj = (tc.StrDragObj[]) null;
    this.e.TableRow = (tc.StrTableRow[]) null;
    this.e.TableAux = (tc.StrTableAux[]) null;
    this.e.cell = (tc.StrCell[]) null;
    this.e.CellAux = (tc.StrCellAux[]) null;
    for (int TagId = 0; TagId < this.e.TotalCharTags; ++TagId)
      this.FreeTag(TagId);
    this.e.CharTag = (tc.StrCharTag[]) null;
    for (int idx = 0; idx < this.e.TotalReviewers; ++idx)
      this.FreeReviewer(idx);
    this.e.reviewer = (tc.StrReviewer[]) null;
    this.FreeListTable();
    this.e.list = (tc.StrList[]) null;
    this.e.ListOr = (tc.StrListOr[]) null;
    this.e.RowX = (int[]) null;
    this.e.RowY = (int[]) null;
    this.e.RowHeight = (int[]) null;
    this.e.hTerWnd = IntPtr.Zero;
    return true;
  }

  internal new bool EnableVScrollBar(bool enable)
  {
    if (this.e.TerArg.ShowVerBar != enable)
    {
      this.e.TerArg.ShowVerBar = enable;
      if (enable)
      {
        this.ShowScrollBar(this.e.hTerWnd, 1, true);
        this.SetScrollRange(this.e.hTerWnd, 1, 0, 1000, false);
        this.draw.GetWinDimension();
      }
      else
      {
        this.ShowScrollBar(this.e.hTerWnd, 1, false);
        this.PostMessage(this.e.hTerWnd, 2736, 0, 0);
      }
    }
    return true;
  }

  internal new bool FreeClonedLinePtr(tc.ClsLinePtr[] ptr, int LineCount)
  {
    for (int index = 0; index < LineCount; ++index)
      ptr[index] = (tc.ClsLinePtr) null;
    ptr = (tc.ClsLinePtr[]) null;
    return true;
  }

  internal new bool FreeLine(int line)
  {
    if (this.e.text[line] != null)
    {
      if (this.e.text[line].tabw != null && (this.e.text[line].tabw.type & 2) != 0)
      {
        this.e.PosPageHdrFtr = true;
        this.e.SectModified = true;
      }
      this.FreeLinePtr(line);
    }
    return true;
  }

  internal new bool FreeLinePtr(int line)
  {
    if (this.e.LinePtrCount < 150)
    {
      this.e.LinePtrCache[this.e.LinePtrCount] = this.e.text[line];
      ++this.e.LinePtrCount;
    }
    this.e.text[line] = (tc.ClsLinePtr) null;
    return true;
  }

  internal new bool FreeRtfInfo()
  {
    for (int index = 0; index < 11; ++index)
      this.e.pRtfInfo[index] = (string) null;
    return true;
  }

  internal new bool InitConstruct()
  {
    if (tc.TerOpenCount == 0)
      this.TerCommonInit();
    this.e.InitArg = new tc.arg_list();
    this.e.InitArg.WordWrap = true;
    this.e.InitArg.PageMode = true;
    this.e.InitArg.PrintView = true;
    this.e.InitArg.FittedView = false;
    this.e.InitArg.ShowHorBar = false;
    this.e.InitArg.ShowVerBar = false;
    this.e.InitArg.ruler = false;
    this.e.InitArg.ToolBar = false;
    this.e.InitArg.BorderMargin = true;
    this.e.InitArg.ReadOnly = false;
    this.e.InitArg.SaveFormat = 2;
    this.e.InitArg.delim = '\r';
    this.e.UseWin = true;
    this.e.InitArg.FontTypeFace = "Arial";
    this.e.InitArg.PointSize = 12;
    this.e.InitArg.file = "";
    this.e.InitArg.hBuffer = "";
    this.e.InitArg.BufferLen = 0;
    this.e.InitArg.InputType = 'B';
    this.e.TerFlags = tc.TerInitFlags;
    this.e.TerFlags2 = tc.TerInitFlags2;
    this.e.TerFlags3 = tc.TerInitFlags3;
    this.e.TerFlags4 = tc.TerInitFlags4;
    this.e.TerFlags5 = tc.TerInitFlags5;
    this.e.TerFlags6 = tc.TerInitFlags6;
    this.e.TextDefBkColor = tc.CLR_WHITE;
    this.e.TextBorderColor = tc.CLR_WHITE;
    this.e.hTerWnd = IntPtr.Zero;
    this.InitToolbarVars();
    return true;
  }

  internal new bool InitLine(int line)
  {
    this.AllocLinePtr(line);
    this.e.text[line].txt = (char[]) null;
    this.e.text[line].fmt = (ushort[]) null;
    this.e.text[line].UniFmt = (ushort) 0;
    this.e.text[line].cwidth = (ushort[]) null;
    this.e.text[line].tag = (ushort[]) null;
    this.e.text[line].len = 0;
    this.e.text[line].height = this.e.PrtResY * 12 / 72;
    this.e.text[line].ScrHt = this.e.ScrResY * 12 / 72;
    this.e.text[line].flags = 0;
    this.e.text[line].flags2 = 0;
    this.e.text[line].tabw = (tc.ClsTabw) null;
    this.e.text[line].y = 0;
    this.e.text[line].x = 0;
    int pfmt = line <= 0 ? 0 : this.e.text[line - 1].pfmt;
    if (pfmt < this.e.TotalPfmts)
      this.e.text[line].pfmt = pfmt;
    else
      this.e.text[line].pfmt = 0;
    int cid = line <= 0 ? 0 : this.e.text[line - 1].cid;
    if (cid < this.e.TotalCells)
      this.e.text[line].cid = cid;
    else
      this.e.text[line].cid = 0;
    int fid = line <= 0 ? 0 : this.e.text[line - 1].fid;
    if (fid < this.e.TotalParaFrames)
      this.e.text[line].fid = fid;
    else
      this.e.text[line].fid = 0;
    if (line > 0 && this.e.text[line].fid == this.e.text[line - 1].fid)
    {
      this.e.text[line].y = this.e.text[line - 1].y + this.e.text[line - 1].height;
      this.e.text[line].x = this.e.text[line - 1].x;
    }
    this.e.text[line].JustAdjX = 0;
    if (line > 0)
      this.e.text[line].page = this.e.text[line - 1].page;
    else
      this.e.text[line].page = 0;
    return true;
  }

  internal new bool InitTerInst()
  {
    ++tc.TerOpenCount;
    if (this.e.UseWin)
    {
      this.e.hTerWnd = this.e.Handle;
    }
    else
    {
      this.e.hTerWnd = COp.Win32.GetDesktopWindow();
      this.e.InitArg.ShowHorBar = false;
      this.e.InitArg.ShowVerBar = false;
      this.e.InitArg.ruler = false;
      this.e.InitArg.ToolBar = false;
      this.e.InitArg.BorderMargin = false;
      this.e.InitArg.ShowStatus = false;
    }
    this.e.NetControl = true;
    this.e.TerArg = this.e.InitArg;
    this.e.InitArg.hBuffer = "";
    this.e.InitArg.BufferLen = this.e.InitArg.hBuffer.Length;
    if (this.e.TerArg.FontTypeFace.Length == 0)
      this.InitTypeface();
    if (this.True(tc.TerStyles))
      this.OverrideStyles();
    if (!this.e.TerArg.WordWrap)
      this.e.TerArg.PrintView = this.e.TerArg.PageMode = this.e.TerArg.FittedView = false;
    if (this.e.TerArg.FittedView)
      this.e.TerArg.PageMode = true;
    if (this.e.TerArg.PageMode)
      this.e.TerArg.PrintView = true;
    if (this.e.TerArg.PageMode && !this.e.TerArg.FittedView)
      this.e.PagesShowing = true;
    else
      this.e.PagesShowing = false;
    if (!this.InitVariables() || !this.AllocArrayMem())
      return false;
    this.InitLine(0);
    this.e.text[0].pfmt = 0;
    this.e.PageInfo[0] = new tc.StrPage();
    this.e.PageInfo[0].FirstLine = 0;
    this.e.PageInfo[0].DispNbr = 1;
    this.e.PageInfo[0].ScrHt = 100;
    this.e.PageInfo[0].flags = 2;
    this.e.TotalSects = 1;
    this.InitSect(0);
    this.e.TotalTabs = 1;
    this.e.TerTab[0] = new tc.StrTab();
    this.e.TerTab[0].SetSize(0);
    this.e.TotalPfmts = 1;
    this.e.PfmtId[0] = new tc.StrPfmt();
    this.e.PfmtId[0].flags = 1024 /*0x0400*/;
    this.e.PfmtId[0].BkColor = tc.CLR_WHITE;
    this.e.PfmtId[0].BorderColor = tc.CLR_AUTO;
    this.e.TotalBlts = 1;
    this.e.TerBlt[0] = new tc.StrBlt().init();
    this.e.TerBlt[0].IsBullet = true;
    this.e.TerBlt[0].BulletChar = '·';
    this.e.TerBlt[0].font = 1;
    this.e.TotalLists = 1;
    this.e.list[0] = new tc.StrList();
    this.e.list[0].InUse = true;
    this.e.TotalListOr = 1;
    this.e.ListOr[0] = new tc.StrListOr();
    this.e.ListOr[0].InUse = true;
    this.e.TotalFrames = 1;
    this.InitFrame(0);
    this.e.TableRow[0] = new tc.StrTableRow();
    this.e.TableAux[0] = new tc.StrTableAux();
    this.e.TableRow[0].InUse = true;
    this.e.TotalTableRows = 1;
    this.e.cell[0] = new tc.StrCell().init();
    this.e.CellAux[0] = new tc.StrCellAux();
    this.e.cell[0].InUse = true;
    this.e.TotalCells = 1;
    this.e.TotalParaFrames = 1;
    this.e.ParaFrame[0] = new tc.StrParaFrame();
    this.e.ParaFrame[0].InUse = true;
    this.e.TotalSID = 2;
    this.e.StyleId[0] = new tc.StrStyleId();
    this.e.StyleId[0].InUse = true;
    this.e.StyleId[0].type = 2;
    this.e.StyleId[0].TextBkColor = tc.CLR_WHITE;
    this.e.StyleId[0].ParaBkColor = tc.CLR_WHITE;
    this.e.StyleId[0].TwipsSize = this.e.TerArg.PointSize * 20;
    this.e.StyleId[0].TypeFace = this.e.TerArg.FontTypeFace;
    this.e.StyleId[0].name = "Normal";
    this.e.StyleId[0].OutlineLevel = -1;
    this.e.StyleId[1] = new tc.StrStyleId();
    this.e.StyleId[1].InUse = true;
    this.e.StyleId[1].type = 1;
    this.e.StyleId[1].TextBkColor = tc.CLR_WHITE;
    this.e.StyleId[1].ParaBkColor = tc.CLR_WHITE;
    this.e.StyleId[1].TypeFace = this.e.TerArg.FontTypeFace;
    this.e.StyleId[1].name = "Default Paragraph Font";
    this.e.StyleId[1].OutlineLevel = -1;
    this.e.CharTag[0] = new tc.StrCharTag();
    this.e.TotalCharTags = 1;
    this.e.CharTag[0].InUse = true;
    this.e.WindowBeingCreated = true;
    this.e.Validate();
    if (!tc.StSearched)
      this.SearchSpellTime();
    if (tc.hSpell != (Assembly) null)
      this.e.TerInitSpellTime((object) null);
    if (!this.InitPrinter() || !this.InitWinProp())
      return false;
    if (this.e.TerArg.WordWrap)
      this.AllocWrapBuf(0);
    this.e.WindowBeingCreated = false;
    this.e.DocName = this.e.TerArg.file;
    if (this.e.TerArg.BufferLen > 0 && !this.TerRead(this.e.DocName))
      return false;
    this.e.TerRewrap();
    this.e.TerArg.hBuffer = "";
    this.e.TerArg.BufferLen = 0;
    this.e.TerArg.open = true;
    this.e.TerArg.modified = 0;
    return true;
  }

  internal new bool InitTypeface()
  {
    this.e.TerArg.FontTypeFace = "Arial";
    return true;
  }

  internal new bool InitVariables()
  {
    this.e.InPainting = false;
    this.e.TotalOpGrs = 0;
    this.e.CloseDC = true;
    this.e.CurRow = 0;
    this.e.CurCol = 0;
    this.e.CurLine = 0;
    this.e.CurLineY = 0;
    this.e.BeginLine = 0;
    this.e.TotalLines = 1;
    this.e.MaxLines = 200;
    this.e.TerWinOrgX = 0;
    this.e.TerWinOrgY = 0;
    this.e.CaretHeight = 0;
    this.e.PrevCursLine = 0;
    this.e.PrevCursCol = 0;
    this.e.PrevCursPage = 0;
    this.e.PrevCursLineY = 0;
    this.e.CursHorzPos = -1;
    this.e.CursDirection = 0;
    this.e.PaintFlag = 4;
    this.e.WinYOffset = 0;
    this.e.WinYOffsetLine = -1;
    this.e.ScrollBM = (Bitmap) null;
    this.e.BkPictBM = (Bitmap) null;
    this.e.BufBM = (Bitmap) null;
    this.e.OrigBufBM = (Bitmap) null;
    this.e.InitBkBM = (Bitmap) null;
    this.e.BufGr = (Graphics) null;
    this.e.RepaintNo = 0;
    this.e.BorderSpill = 0;
    this.e.transparent = false;
    this.e.EnableTransparent = false;
    this.e.HtmlMode = false;
    this.e.TerOpFlags = 0;
    this.e.LinePtrCount = 0;
    this.e.CurCtlId = -1;
    this.e.ScrYOffset = 0;
    this.e.StatusBarHeight = 0;
    this.e.DispTextHt = 0;
    this.e.GlbPrtDCId = 0;
    this.e.MdiClient = (Control) null;
    this.e.MdiChild = (Control) null;
    this.e.MaxRtfColors = 200;
    this.e.MaxSects = 100;
    this.e.ExpandLineFmt = false;
    this.e.DraggingText = false;
    this.e.TextDragged = false;
    this.e.InOleDrag = false;
    this.e.InDragDrop = false;
    this.e.HilightAtCurPos = false;
    this.e.RedrawBorder = false;
    this.e.PrivatePrtDC = true;
    this.e.DrawBknd = true;
    this.e.VerThumbSize = 0;
    this.e.HorThumbSize = 0;
    this.e.MinThumbHt = 0;
    this.e.pLineCharWidth = (ushort[]) null;
    this.e.DefCellMargin = 60;
    this.e.ToolbarFlagText = "";
    this.e.EvalTimerCount = 0;
    this.e.InDrawEval = false;
    this.e.ViewKind = 0;
    this.e.FootnoteNumFmt = 0;
    this.e.EndnoteNumFmt = 4;
    this.e.KnownSect = -1;
    this.e.ScrFrameAngle = 0;
    this.e.FrameDistFromMargin = 1440;
    this.e.DocTextFlow = 0;
    this.e.ScrRtl = false;
    this.e.MousePictFrame = -1;
    this.e.CurCtl = (Control) null;
    this.e.CurCtlId = 0;
    this.e.DocName = "";
    this.e.PrtDataGr = (Graphics) null;
    this.e.TotalScrCtls = 0;
    this.e.PaintId = 0;
    this.e.HasOptionalHyph = false;
    this.e.DocName = "";
    this.e.st = (object) null;
    this.e.PageHeightAdj = 0;
    this.e.WmParaFID = 0;
    this.e.InPreprocess = false;
    this.e.SectLine = -1;
    this.e.PageBkColor = tc.CLR_WHITE;
    this.e.MouseStopTimerOn = false;
    this.e.MouseStopMsgOn = false;
    this.e.MouseStopDone = false;
    if (this.e.WmImageAttr != null)
      this.e.WmImageAttr.Dispose();
    this.e.WmImageAttr = (ImageAttributes) null;
    this.e.TrackChanges = false;
    this.e.TrackRev = 0;
    this.e.TrackTime = (tc.ClsDateTime) null;
    this.e.MaxReviewers = 3;
    this.e.TerTextMet = new COp.TEXTMETRIC();
    this.e.CurMapPict = 0;
    this.e.CurMapId = 0;
    this.e.CurMapRect = 0;
    this.e.TerRegFont = (Font) null;
    this.e.TerCurFont = (Font) null;
    this.e.RulerFont = (Font) null;
    this.e.hRulerFont = IntPtr.Zero;
    this.e.RulerFontBold = (Font) null;
    this.e.hRulerFontBold = IntPtr.Zero;
    this.e.StatusFont = (Font) null;
    this.e.hStatusFont = IntPtr.Zero;
    this.e.DocHasToc = false;
    this.e.DocHasHeadings = false;
    this.e.MultipleToc = false;
    this.e.TocShowPageNo = true;
    this.e.TocTabAlign = 1;
    this.e.TocTabLeader = 1;
    this.e.SpellPending = false;
    this.e.SpellCheckerPopped = false;
    this.e.EditCol = -1;
    this.e.hPopup = (Menu) null;
    this.e.UserDir = "";
    this.e.UserFileType = 2;
    this.e.ImeStartPos = 0;
    this.e.ImeEnabled = false;
    this.e.InlineIme = true;
    this.e.DefLang = 1033;
    if (this.GetACP() == 950 || this.GetACP() == 936 || this.GetACP() == 949 || this.GetACP() == 1361)
      this.e.InlineIme = false;
    this.e.DefLang = this.GetUserDefaultLangID();
    try
    {
      this.e.DefInpLang = InputLanguage.FromCulture(CultureInfo.CurrentCulture);
    }
    catch (Exception ex)
    {
      this.e.DefInpLang = InputLanguage.DefaultInputLanguage;
      LogManager.AddLine(ex);
    }
    this.e.ReqLang = this.e.DefLang;
    this.e.LeftBorderWidth = 0;
    this.e.TopBorderHeight = 0;
    this.e.TerArg.modified = 0;
    this.e.HilightType = 0;
    this.e.StretchHilight = false;
    this.e.HilightWithColCursor = false;
    this.e.IgnoreMouseMove = true;
    this.e.MouseOnTextLine = true;
    this.e.Notified = false;
    this.e.MaxUndoLimit = 100;
    this.e.MaxUndos = 40;
    this.e.UndoCount = 0;
    this.e.UndoTblSize = 0;
    this.e.UndoRef = 0;
    this.e.UndoSkipRef = -1;
    this.e.InUndo = false;
    this.e.InAccelerator = false;
    this.e.SectModified = false;
    this.e.DocHeight = 0;
    this.e.WheelShowing = false;
    this.e.WheelTimerUpOn = false;
    this.e.WheelTimerDownOn = false;
    this.e.WheelCur = (Cursor) null;
    this.e.CurClipRgn = (Region) null;
    this.e.TabPrevLine = -1;
    this.e.TabPrevCol = -1;
    this.e.TabPrevAdj = 0;
    this.e.TotalPages = 1;
    this.e.PrevTotalPages = 1;
    this.e.MaxPages = 0;
    this.e.CurPage = 0;
    this.e.PageModifyCount = -1;
    this.e.RepageBeginLine = 0;
    this.e.RepagePending = false;
    this.e.FirstFramePage = this.e.LastFramePage = 0;
    this.e.repaginating = false;
    this.e.NewFrameX = -1;
    this.e.NewFrameY = -1;
    this.e.NewFrameVPage = false;
    this.e.NewFrameWidth = 2160;
    this.e.NewFrameHeight = 1440;
    this.e.PageInfo = (tc.StrPage[]) null;
    this.e.StyleId = (tc.StrStyleId[]) null;
    this.e.frame = (tc.StrFrame[]) null;
    this.e.TerFont = (tc.StrFont[]) null;
    this.e.PrtFont = (tc.StrPrtFont[]) null;
    this.e.TerTab = (tc.StrTab[]) null;
    this.e.TerBlt = (tc.StrBlt[]) null;
    this.e.TerSect = (tc.StrSect[]) null;
    this.e.TerSect1 = (tc.StrSect1[]) null;
    this.e.TotalFonts = 0;
    this.e.TotalTabs = 0;
    this.e.NextFontId = -1;
    this.e.NextFontAux1Id = 0;
    this.e.NextParaAux1Id = 0;
    this.e.DefTabWidth = 720;
    this.e.DefTabType = 0;
    this.e.TotalSects = 0;
    this.e.FontsReleased = false;
    this.e.InRtfRead = false;
    this.e.MessageDisplayed = 0;
    this.e.BkPictId = 0;
    this.e.PfmtId = (tc.StrPfmt[]) null;
    this.e.MaxPfmts = 200;
    this.e.CurPfmt = 0;
    this.e.InputFontId = -1;
    this.e.WindowBeingCreated = false;
    this.e.WindowDestroyed = false;
    this.e.ReclaimResources = false;
    this.e.PageHasControls = false;
    this.e.ContinuousScroll = false;
    this.e.CrLfUsed = true;
    this.e.ProtectionLock = true;
    this.e.ShowParaMark = false;
    this.e.ShowPageBorder = false;
    this.e.CaretEnabled = false;
    this.e.CaretHidden = true;
    this.e.CaretEngaged = true;
    this.e.CaretPos = 0;
    this.e.ShowProtectCaret = true;
    this.e.ModifyProtectColor = true;
    this.e.LinkStyle = 1;
    this.e.LinkColor = Color.Blue;
    this.e.LinkDblClick = false;
    this.e.BatchMode = (this.e.TerArg.style & 4096 /*0x1000*/) != 0;
    this.e.RepageTimerOn = false;
    this.e.HilightTimerOn = false;
    this.e.ClipTblLevel = 1;
    this.e.ClipEmbTable = true;
    this.e.NoTabIndent = false;
    this.e.WrapBufferSize = 0;
    this.e.WrapCtid = (ushort[]) null;
    this.e.WrapCharWidth = (ushort[]) null;
    this.e.WrapCharWidthOrder = (int[]) null;
    this.e.WrapCharWidthDX = (int[]) null;
    this.e.WrapCharWidthText = (ushort[]) null;
    this.e.WrapCharWidthClass = (byte[]) null;
    this.e.WrapFlag = 3;
    this.e.WrapAddLines = 1;
    this.e.MaxFonts = 25;
    this.e.MaxCharTags = 5;
    this.e.WrapWidthChars = 0;
    this.e.WrapWidthTwips = 0;
    this.e.MaxLinesPerWin = 2000;
    this.e.MaxBlts = 50;
    this.e.MaxLists = 10;
    this.e.MaxListOr = 10;
    this.e.FileFormat = 2;
    this.e.CurSID = -1;
    this.e.TotalSID = 0;
    this.e.MaxSID = 5;
    this.e.EditingParaStyle = false;
    this.e.ScrResX = this.e.ScrResY = this.e.PrtResX = this.e.PrtResY = 0;
    this.e.ZoomPercent = 100;
    this.e.PageBorderWidth = 360;
    this.e.ExtraSpacePrtX = this.e.ExtraSpaceScrX = 0;
    this.e.ToolBarHeight = 0;
    this.e.TerTlb = (ToolbarControl) null;
    this.e.PvTlb = (ToolbarControl) null;
    this.e.ToolBarCfmt = -1;
    this.e.ToolBarPfmt = -1;
    this.e.ToolBarSID = -1;
    this.e.FrameBkColor = tc.CLR_WHITE;
    this.e.ParaBackColor = tc.CLR_WHITE;
    this.e.ParaBoxSpace = 20;
    this.e.TextDefColor = tc.CLR_BLACK;
    if (this.e.ToolbarBrush != null)
      this.e.ToolbarBrush.Dispose();
    this.e.ToolbarBrush = (Brush) null;
    this.e.StatusColor = this.ToColor(8, 8, 8);
    this.e.StatusBkColor = this.ToColor(192 /*0xC0*/, 192 /*0xC0*/, 192 /*0xC0*/);
    if (!this.IsSameColor(this.e.StatusBkColor, SystemColors.Control))
    {
      this.e.StatusColor = SystemColors.ControlText;
      this.e.StatusBkColor = SystemColors.Control;
      this.e.ToolbarBrush = (Brush) new SolidBrush(this.e.StatusBkColor);
    }
    this.e.PageBorderColor = this.ToColor(143, 143, 143);
    this.e.CurForeColor = this.ToColor(0, 0, 0);
    this.e.CurBackColor = this.ToColor(0, 0, 0);
    this.e.FrameShading = 0;
    this.e.ParaShading = 0;
    this.e.PaintFrameFlags = 0;
    this.e.ParaFrameSpace = false;
    this.e.PrtTextBkColor = tc.CLR_WHITE;
    this.e.PrtParaBkColor = tc.CLR_WHITE;
    this.e.PrtFrameBkColor = tc.CLR_WHITE;
    this.e.UseTextMap = true;
    this.e.TextBorder = 0;
    this.e.BoxLeft = 0;
    this.e.BoxRight = 0;
    this.e.PagingMargin = 1;
    this.e.LineWidth = 998;
    this.e.TabWidth = 4;
    this.e.CrNewLine = true;
    this.e.CrSplitLine = true;
    this.e.TabAlign = true;
    this.e.JoinLines = true;
    this.e.HoldMessages = false;
    this.e.FirstPalette = true;
    this.e.BorderShowing = false;
    this.e.DocCode = (ushort) 55555;
    this.e.DocBegin = (ushort) 256 /*0x0100*/;
    this.e.ParaChar = '\u0015';
    this.e.OldParaChar = '¶';
    this.e.DocExt = ".DOC";
    this.e.PrtGr = (Graphics) null;
    this.e.PageWidth = 8.5f;
    this.e.PageHeight = 11f;
    this.e.InsertMode = true;
    this.e.MaxColBlock = 200;
    this.e.SearchString = "";
    this.e.ReplaceString = "";
    this.e.ReplaceWith = "";
    this.e.SearchFlags = 16 /*0x10*/;
    this.e.TerHelpWanted = false;
    this.e.TerHelpFile = "ter_hlp.hlp";
    this.e.CfmtSign = "~`!@#$%^&*()-+|=-TeCfMt";
    this.e.FmtSign = '¿';
    this.e.FmtSignOld = '\u00BE';
    this.e.InPrinting = false;
    this.e.PrinterAvailable = false;
    this.e.PrtDiffRes = false;
    this.e.PprKind = PaperKind.Letter;
    this.e.IsPortrait = true;
    this.e.InPrintPreview = false;
    this.e.ExtPrintPreview = false;
    this.e.GoPrintPreview = false;
    this.e.UsingZoomFonts = false;
    this.e.HasVarWidthFont = false;
    this.e.TotalPreviewPages = 1;
    this.e.PreviewZoom = 0;
    this.e.ShowPvToolbar = true;
    this.e.RulerSection = 0;
    this.e.RulerPending = true;
    this.e.PaintEnabled = true;
    this.e.FrameRefreshEnabled = true;
    this.e.ShowHiddenText = false;
    this.e.ShowFieldNames = false;
    this.e.EditFootnoteText = false;
    this.e.EditEndnoteText = false;
    this.e.EndnoteAtSect = true;
    this.e.InFootnote = false;
    this.e.ShowHyperlinkCursor = false;
    this.e.MatchIds = true;
    this.e.InDialogBox = false;
    this.e.SnapToGrid = true;
    this.e.TblSelCursShowing = false;
    this.e.PvDlg = (PrintPreviewDialog) null;
    this.e.WaitForOle = false;
    this.e.CurDragObj = -1;
    this.e.TotalDragObjs = 0;
    this.e.MaxDragObjs = 50;
    this.e.PictureHilighted = false;
    this.e.FrameTabsHilighted = false;
    this.e.FrameRectHilighted = false;
    this.e.PictureClicked = false;
    this.e.FrameClicked = false;
    this.e.FirstFreeCellId = 0;
    this.e.CursorCell = 0;
    this.e.TotalTableRows = 0;
    this.e.TotalCells = 0;
    this.e.MaxTableRows = 100;
    this.e.MaxCells = 200;
    this.e.MaxParaFrames = 50;
    this.e.CellChar = '\u0013';
    this.e.OldCellChar = '¤';
    this.e.FrameCellId = 0;
    this.e.FrameRowId = 0;
    this.e.ShowTableGridLines = true;
    this.e.ViewPageHdrFtr = false;
    this.e.EditPageHdrFtr = false;
    this.e.PosPageHdrFtr = false;
    this.SetCurLang(InputLanguage.CurrentInputLanguage);
    this.e.TotalImageMaps = 1;
    int index1 = 1 - 1;
    this.e.BreakChars[index1] = '\f';
    int index2 = index1 + 1;
    this.e.BreakChars[index2] = '\u0014';
    int index3 = index2 + 1;
    this.e.BreakChars[index3] = '\u0016';
    int index4 = index3 + 1;
    this.e.BreakChars[index4] = '\u0012';
    int index5 = index4 + 1;
    this.e.BreakChars[index5] = '\u0011';
    int index6 = index5 + 1;
    this.e.BreakChars[index6] = '\u0010';
    int index7 = index6 + 1;
    this.e.BreakChars[index7] = '\u0019';
    int index8 = index7 + 1;
    this.e.BreakChars[index8] = '\u001A';
    this.e.BreakChars[index8 + 1] = char.MinValue;
    this.e.RtfInfo[0] = "title";
    this.e.RtfInfo[1] = "subject";
    this.e.RtfInfo[2] = "author";
    this.e.RtfInfo[3] = "manager";
    this.e.RtfInfo[4] = "company";
    this.e.RtfInfo[5] = "operator";
    this.e.RtfInfo[6] = "category";
    this.e.RtfInfo[7] = "keywords";
    this.e.RtfInfo[8] = "comment";
    this.e.RtfInfo[9] = "doccomm";
    this.e.RtfInfo[10] = "hlinkbase";
    for (int index9 = 0; index9 < 11; ++index9)
      this.e.pRtfInfo[index9] = (string) null;
    this.e.TotalSpeedKeys = this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.SetSpeedKey(this.e.TotalSpeedKeys = 0, 600, Keys.Prior), 600, Keys.Prior | Keys.Shift), 601, Keys.Next), 601, Keys.Next | Keys.Shift), 602, Keys.Up), 602, Keys.Up | Keys.Shift), 603, Keys.Down), 603, Keys.Down | Keys.Shift), 604, Keys.Left), 604, Keys.Left | Keys.Shift), 605, Keys.Right), 605, Keys.Right | Keys.Shift), 610, Keys.Home), 610, Keys.Home | Keys.Shift), 611, Keys.End), 611, Keys.End | Keys.Shift), 715, Keys.Up | Keys.Control), 715, Keys.Up | Keys.Shift | Keys.Control), 716, Keys.Down | Keys.Control), 716, Keys.Down | Keys.Shift | Keys.Control), 612, Keys.Prior | Keys.Control), 612, Keys.Prior | Keys.Shift | Keys.Control), 612, Keys.Home | Keys.Control), 612, Keys.Home | Keys.Shift | Keys.Control), 613, Keys.Next | Keys.Control), 613, Keys.Next | Keys.Shift | Keys.Control), 613, Keys.End | Keys.Control), 613, Keys.End | Keys.Shift | Keys.Control), 620, Keys.Right | Keys.Control), 620, Keys.Right | Keys.Shift | Keys.Control), 621, Keys.Left | Keys.Control), 621, Keys.Left | Keys.Shift | Keys.Control), 745, Keys.Back | Keys.Control), 606, Keys.Delete), 607, Keys.Back), 609, Keys.Tab | Keys.Shift), 614, Keys.Tab | Keys.Control), 608, Keys.Tab), 608, Keys.T | Keys.Control), 615, Keys.F9), 616, Keys.F5 | Keys.Control), 617, Keys.F9 | Keys.Shift), 619, Keys.J | Keys.Alt), 622, Keys.F8), 625, Keys.A | Keys.Control), 628, Keys.X | Keys.Control), 628, Keys.Delete | Keys.Shift), 629, Keys.C | Keys.Control), 629, Keys.Insert | Keys.Control), 630, Keys.V | Keys.Control), 630, Keys.Insert | Keys.Shift), 632, Keys.F8 | Keys.Alt), 623, Keys.C | Keys.Alt), 624, Keys.M | Keys.Alt), 633, Keys.F5), 634, Keys.F | Keys.Control), 635, Keys.F | Keys.Shift | Keys.Control), 636, Keys.F6), 637, Keys.F1), 741, Keys.F7), 638, Keys.F8 | Keys.Shift), 638, Keys.Back | Keys.Alt), 638, Keys.Z | Keys.Control), 747, Keys.Y | Keys.Control), 639, Keys.Insert), 640, Keys.F3), 641, Keys.F3 | Keys.Shift), 642, Keys.F3 | Keys.Control), 643, Keys.F4), 645, Keys.F4 | Keys.Shift), 646, Keys.F10), 647, Keys.D0 | Keys.Alt), 688, Keys.D | Keys.Control), 687, Keys.H | Keys.Control), 742, Keys.H | Keys.Alt), 689, Keys.D3 | Keys.Alt), 652, Keys.D4 | Keys.Alt), 653, Keys.D5 | Keys.Alt), 651, Keys.D6 | Keys.Alt), 654, Keys.D7 | Keys.Alt), 655, Keys.F10 | Keys.Alt), 657, Keys.D8 | Keys.Alt), 658, Keys.D9 | Keys.Alt), 659, Keys.L | Keys.Alt), 660, Keys.R | Keys.Alt), 662, Keys.T | Keys.Alt), 729, Keys.B | Keys.Alt), 748, Keys.N | Keys.Alt), 730, Keys.S | Keys.Alt), 731, Keys.D1 | Keys.Alt), 732, Keys.D2 | Keys.Alt), 670, Keys.Return | Keys.Control), 763, Keys.Return), 763, Keys.Return | Keys.Shift), 804, Keys.N | Keys.Control), 805, Keys.P | Keys.Control);
    return true;
  }

  internal new bool InitWinProp()
  {
    if ((this.e.TerGr = Graphics.FromHwnd(this.e.hTerWnd)) == null)
      return this.PrintError(104, "");
    this.e.OrigScrResX = this.e.ScrResX = this.e.PrtResX = this.GetDeviceCaps(this.e.TerGr, 88);
    this.e.OrigScrResY = this.e.ScrResY = this.e.PrtResY = this.GetDeviceCaps(this.e.TerGr, 90);
    this.e.PrtResX = this.e.PrtResY = this.e.UnitResX = this.e.UnitResY = 1440;
    this.e.ExtraSpaceScrX = this.TwipsToScrX(40);
    this.e.ExtraSpacePrtX = this.TwipsToUnitX(40);
    this.e.StatusLineWidth = this.TwipsToScrY(15);
    if (this.e.StatusLineWidth < 1)
      this.e.StatusLineWidth = 1;
    if (this.e.TerArg.PrintView && !this.OpenCurPrinter(false))
      return false;
    this.e.hMemDC = COp.Win32.CreateCompatibleDC(IntPtr.Zero);
    this.e.hBufDC = COp.Win32.CreateCompatibleDC(IntPtr.Zero);
    for (int index = 0; index < this.e.MaxFonts; ++index)
      this.e.TerFont[index].InUse = false;
    this.e.TotalFonts = 1;
    this.InitTerObject(0);
    this.e.TerFont[0].InUse = true;
    this.e.TerFont[0].TypeFace = this.e.TerArg.FontTypeFace;
    this.e.TerFont[0].TwipsSize = 160 /*0xA0*/;
    this.e.TerFont[0].style = 0;
    if (!this.CreateOneFont(this.e.TerGr, 0, true))
      return this.PrintError(42, nameof (InitWinProp));
    this.e.RulerFont = this.e.TerFont[0].font;
    this.e.hRulerFont = this.e.TerFont[0].hFont;
    this.e.RulerFontHeight = this.e.TerFont[0].height;
    if (this.e.TerArg.PageMode && this.e.TerFont[0].GlbFontId == -1)
    {
      this.e.PrtFont[0].font.Dispose();
      COp.Win32.DeleteObject(this.e.PrtFont[0].hFont);
    }
    this.InitTerObject(0);
    this.e.TerFont[0].InUse = true;
    this.e.TerFont[0].TypeFace = this.e.TerArg.FontTypeFace;
    this.e.TerFont[0].TwipsSize = 160 /*0xA0*/;
    this.e.TerFont[0].style = 2;
    if (!this.CreateOneFont(this.e.TerGr, 0, true))
      return this.PrintError(42, nameof (InitWinProp));
    this.e.RulerFontBold = this.e.TerFont[0].font;
    this.e.hRulerFontBold = this.e.TerFont[0].hFont;
    if (this.e.TerArg.PageMode && this.e.TerFont[0].GlbFontId == -1)
    {
      this.e.PrtFont[0].font.Dispose();
      COp.Win32.DeleteObject(this.e.PrtFont[0].hFont);
    }
    this.InitTerObject(0);
    this.e.TerFont[0].InUse = true;
    this.e.TerFont[0].TypeFace = this.e.TerArg.FontTypeFace;
    this.e.TerFont[0].TwipsSize = 180;
    this.e.TerFont[0].style = 0;
    if (!this.CreateOneFont(this.e.TerGr, 0, true))
      return this.PrintError(42, "InitWinProp(a)");
    this.e.StatusFont = this.e.TerFont[0].font;
    this.e.hStatusFont = this.e.TerFont[0].hFont;
    this.e.StatusFontHeight = this.e.TerFont[0].height;
    if (this.e.TerArg.PageMode && this.e.TerFont[0].GlbFontId == -1)
    {
      this.e.PrtFont[0].font.Dispose();
      COp.Win32.DeleteObject(this.e.PrtFont[0].hFont);
    }
    this.InitTerObject(0);
    this.e.TerFont[0].InUse = true;
    this.e.TerFont[0].TypeFace = this.e.TerArg.FontTypeFace;
    this.e.TerFont[0].TwipsSize = this.e.TerArg.PointSize * 20;
    this.e.TerFont[0].style = 0;
    this.SetCurLang(InputLanguage.CurrentInputLanguage);
    this.e.TerFont[0].CharSet = this.e.ReqCharSet;
    this.e.TerFont[0].lang = this.e.ReqLang;
    if (!this.CreateOneFont(this.e.TerGr, 0, true))
      return this.PrintError(42, "InitWinProp(c)");
    this.e.TerRegFont = this.e.TerCurFont = this.e.TerFont[0].font;
    COp.TEXTMETRIC tm;
    if (!this.e.FullRenderMode && this.e.TerFont[0].TextMetric.HasValue)
    {
      tm = this.e.TerFont[0].TextMetric.Value;
    }
    else
    {
      if (!this.GetTextMetrics(this.e.TerGr, this.e.TerRegFont, out tm))
        return this.PrintError(75, nameof (InitWinProp));
      if (!this.e.FullRenderMode)
        this.e.TerFont[0].TextMetric = new COp.TEXTMETRIC?(tm);
    }
    this.e.TerTextMet = tm;
    this.e.WaitCursor = Cursors.WaitCursor;
    if (this.False(tc.ToolbarBM))
      tc.ToolbarBM = this.LoadBitmap("Intermech.Document.Model.RtfEditor.ter_tlb", "ToolbarBM");
    if (this.False(tc.Table1Cur))
      tc.Table1Cur = Cursors.VSplit;
    if (this.False(tc.Table2Cur))
      tc.Table2Cur = this.LoadCursor("Intermech.Document.Model.RtfEditor.ter_tbl2", "Table2");
    if (this.False(tc.Table3Cur))
      tc.Table3Cur = Cursors.HSplit;
    if (this.False(tc.Tab1Cur))
      tc.Tab1Cur = this.LoadCursor("Intermech.Document.Model.RtfEditor.ter_tab1", "Tab1");
    if (this.False(tc.HyperlinkCur))
      tc.HyperlinkCur = Cursors.Hand;
    if (this.False(tc.PlusCur))
      tc.PlusCur = this.LoadCursor("Intermech.Document.Model.RtfEditor.ter_plus", "Plus");
    if (this.False(tc.DragInCur))
      tc.DragInCur = this.LoadCursor("Intermech.Document.Model.RtfEditor.ter_din", "DragIn");
    if (this.False(tc.DragInCopyCur))
      tc.DragInCopyCur = this.LoadCursor("Intermech.Document.Model.RtfEditor.ter_dinc", "DragInCopy");
    if (this.False(tc.DragOutCur))
      tc.DragOutCur = this.LoadCursor("Intermech.Document.Model.RtfEditor.ter_dout", "DragOut");
    if (this.False(tc.WheelFullCur))
      tc.WheelFullCur = Cursors.NoMoveVert;
    if (this.False(tc.WheelUpCur))
      tc.WheelUpCur = Cursors.PanNorth;
    if (this.False(tc.WheelDownCur))
      tc.WheelDownCur = Cursors.PanSouth;
    if (this.False(tc.HBeamCur))
      tc.HBeamCur = this.LoadCursor("Intermech.Document.Model.RtfEditor.ter_horz", "HBeam");
    if ((this.e.MemGr = Graphics.FromImage((Image) new Bitmap(10, 10))) == null)
      return this.PrintError(103, "");
    if (tc.PagePen == null)
    {
      if ((tc.PagePen = new Pen(Color.Black)) == null)
        return this.PrintError(66, "");
      tc.PagePen.DashStyle = DashStyle.Dot;
    }
    if (tc.FocusPen == null)
    {
      if ((tc.FocusPen = new Pen(Color.Black)) == null)
        return this.PrintError(66, "");
      tc.FocusPen.DashStyle = DashStyle.Dot;
    }
    this.InitToolbar();
    if (this.e.TerArg.ToolBar)
      this.CreateToolBar();
    this.draw.GetWinDimension();
    if (this.e.TerArg.ShowHorBar)
    {
      this.ShowScrollBar(this.e.hTerWnd, 0, true);
      this.SetScrollRange(this.e.hTerWnd, 0, 0, 1000, false);
      this.e.HorScrollPos = 0;
      this.draw.GetWinDimension();
    }
    if (this.e.TerArg.ShowVerBar)
    {
      this.e.TerArg.ShowVerBar = false;
      this.EnableVScrollBar(true);
    }
    this.e.RtfClipFormat = 18;
    this.e.SSClipInfo = 18;
    this.e.NativeClipFormat = 20;
    this.e.OwnerLinkClipFormat = 21;
    this.e.ObjectLinkClipFormat = 22;
    this.e.CfEnhMetafile = 14;
    if (this.e.Parent != null)
    {
      this.e.MdiChild = (Control) this.e;
      for (this.e.MdiClient = this.e.Parent; this.True(this.e.MdiChild) && this.True(this.e.MdiClient) && (this.e.MdiClient.FindForm() != this.e.MdiClient || !((Form) this.e.MdiClient).IsMdiContainer); this.e.MdiClient = this.e.MdiChild.Parent)
        this.e.MdiChild = this.e.MdiClient;
    }
    if (this.e.TerArg.ShowStatus)
      this.DisplayStatus();
    if ((this.e.TerFlags2 & 2) != 0)
      this.SaveBackground();
    this.e.Invalidate();
    CInit.CheckEval(false, tc.UserKey);
    if (tc.eval)
      this.SetTimer(this.e.hTerWnd, 9184, 120000);
    return true;
  }

  internal new bool OverrideStyles()
  {
    this.e.TerArg.WordWrap = (tc.TerStyles & 1) != 0;
    this.e.TerArg.PrintView = (tc.TerStyles & 2) != 0;
    this.e.TerArg.ShowHorBar = (tc.TerStyles & 4) != 0;
    this.e.TerArg.ShowVerBar = (tc.TerStyles & 8) != 0;
    this.e.TerArg.ShowStatus = (tc.TerStyles & 16 /*0x10*/) != 0;
    this.e.TerArg.ruler = (tc.TerStyles & 32 /*0x20*/) != 0;
    this.e.TerArg.BorderMargin = (tc.TerStyles & 64 /*0x40*/) != 0;
    this.e.TerArg.ReadOnly = (tc.TerStyles & 512 /*0x0200*/) != 0;
    this.e.TerArg.PageMode = (tc.TerStyles & 1024 /*0x0400*/) != 0;
    this.e.TerArg.ToolBar = (tc.TerStyles & 2048 /*0x0800*/) != 0;
    this.e.TerArg.FittedView = (tc.TerStyles & 8192 /*0x2000*/) != 0;
    if ((tc.TerStyles & 128 /*0x80*/) != 0)
      this.e.TerArg.SaveFormat = 2;
    return true;
  }

  internal bool ReInitWinProp(IntPtr hNewWnd)
  {
    this.e.hTerWnd = hNewWnd;
    if ((this.e.TerGr = Graphics.FromHwnd(this.e.hTerWnd)) == null)
      return this.PrintError(104, "");
    if (this.e.TerArg.ToolBar)
      this.CreateToolBar();
    this.draw.GetWinDimension();
    return true;
  }

  internal new bool ResetInitVariables()
  {
    this.e.IgnoreMouseMove = true;
    this.e.MouseLine = 0;
    this.e.MouseCol = 0;
    this.e.TotalLines = 1;
    this.e.TotalPfmts = 1;
    this.e.TotalTableRows = 1;
    this.e.TotalCells = 1;
    this.e.TotalParaFrames = 1;
    this.e.TotalTabs = 1;
    this.e.TotalPages = 1;
    this.e.TotalSID = 2;
    this.e.TotalBlts = 1;
    this.e.CurLine = this.e.CurRow = this.e.BeginLine = 0;
    this.e.CurCol = 0;
    this.e.CaretEngaged = true;
    this.e.ViewPageHdrFtr = this.e.EditPageHdrFtr = this.e.PosPageHdrFtr = false;
    this.e.HilightType = 0;
    this.e.DraggingText = false;
    this.e.InOleDrag = false;
    this.e.TerArg.modified = this.e.PageModifyCount = 0;
    this.e.IsPortrait = true;
    this.e.Notified = false;
    this.e.ToolBarCfmt = -1;
    this.e.ToolBarPfmt = -1;
    this.e.ToolBarSID = -1;
    this.e.DocHasToc = false;
    this.e.DocHasHeadings = false;
    this.e.MultipleToc = false;
    this.e.ViewKind = 0;
    this.e.FootnoteNumFmt = 0;
    this.e.DocTextFlow = 0;
    this.e.CurCtl = (Control) null;
    this.e.CurCtlId = 0;
    this.e.TotalHlSegs = 0;
    this.e.TotalScrCtls = 0;
    this.e.HasOptionalHyph = false;
    this.e.DocName = "";
    this.e.WmParaFID = 0;
    this.e.WmImageAttr = (ImageAttributes) null;
    this.e.InPreprocess = false;
    this.e.InAutoComp = false;
    this.e.PageBkColor = tc.CLR_WHITE;
    this.e.TabPrevLine = -1;
    this.e.TabPrevCol = -1;
    this.e.TabPrevAdj = 0;
    this.e.CurMapPict = 0;
    this.e.CurMapId = 0;
    this.e.CurMapRect = 0;
    this.e.TotalSects = 1;
    this.InitSect(0);
    this.InitLine(0);
    if (this.e.UndoCount > 0)
      this.ReleaseUndo();
    for (int idx = 1; idx < this.e.TotalFonts; ++idx)
    {
      if (this.e.TerFont[idx].InUse)
        this.DeleteTerObject(idx);
    }
    this.e.TotalFonts = 1;
    this.e.InputFontId = -1;
    this.e.TotalFrames = 1;
    this.InitFrame(0);
    for (int TagId = 1; TagId < this.e.TotalCharTags; ++TagId)
      this.FreeTag(TagId);
    this.e.TotalCharTags = 1;
    this.FreeListTable();
    this.e.TotalLists = 1;
    this.e.TotalListOr = 1;
    this.FreeRtfInfo();
    this.FreeImageMapTable();
    for (int idx = 1; idx < this.e.TotalReviewers; ++idx)
      this.FreeReviewer(idx);
    this.e.TotalReviewers = 1;
    this.e.TrackChanges = false;
    return true;
  }

  internal new bool SaveBackground()
  {
    this.e.InitBkBM = new Bitmap(this.e.TerWinWidth, this.e.TerWinHeight);
    Graphics DestGr = Graphics.FromImage((Image) this.e.InitBkBM);
    this.BitBlt(DestGr, 0, 0, this.e.TerWinWidth, this.e.TerWinHeight, this.e.TerGr, this.e.TerWinRect.left, this.e.TerWinRect.top, 13369376);
    DestGr.Dispose();
    return true;
  }

  internal new int SetSpeedKey(int idx, int cmd, Keys keys)
  {
    if (idx + 1 >= this.e.MaxSpeedKeys)
    {
      this.e.MaxSpeedKeys += 10;
      this.e.SpeedKey = this.ReAlloc(this.e.SpeedKey, this.e.MaxSpeedKeys);
      this.e.SpeedKeyCmd = this.ReAlloc(this.e.SpeedKeyCmd, this.e.MaxSpeedKeys);
      this.e.SpeedKeyEnabled = this.ReAlloc(this.e.SpeedKeyEnabled, this.e.MaxSpeedKeys);
    }
    this.e.SpeedKey[idx] = keys;
    this.e.SpeedKeyCmd[idx] = cmd;
    this.e.SpeedKeyEnabled[idx] = true;
    ++idx;
    return idx;
  }

  internal bool TerClosePrinter() => true;

  internal bool TerCommonInit()
  {
    tc.InitMbcs = false;
    tc.PagePen = (Pen) null;
    tc.FocusPen = (Pen) null;
    tc.TotalWinPtrs = 0;
    tc.GlbPrtDC = (tc.StrGlbPrtDC[]) null;
    tc.GlbPrtDCCount = 0;
    tc.IsWinNT2000 = Environment.OSVersion.Platform == PlatformID.Win32NT;
    tc.OSCanRotate = false;
    DateTimeFormatInfo currentInfo = DateTimeFormatInfo.CurrentInfo;
    string shortDatePattern = currentInfo.ShortDatePattern;
    string longDatePattern = currentInfo.LongDatePattern;
    char ch1 = currentInfo.DateSeparator[0];
    char ch2 = currentInfo.TimeSeparator[0];
    int index1 = 1 - 1;
    tc.DateFieldText[index1] = shortDatePattern;
    int index2 = index1 + 1;
    tc.DateFieldText[index2] = longDatePattern;
    int index3 = index2 + 1;
    tc.DateFieldText[index3] = shortDatePattern != "M/d/yyyy" ? "M/d/yyyy" : "d/M/yyyy";
    int index4 = index3 + 1;
    tc.DateFieldText[index4] = "yyyy-MM-dd";
    int index5 = index4 + 1;
    tc.DateFieldText[index5] = "d-MMM-yy";
    int index6 = index5 + 1;
    tc.DateFieldText[index6] = shortDatePattern == "M/d/yyyy" ? "M.d.yy" : "d.M.yy";
    int index7 = index6 + 1;
    tc.DateFieldText[index7] = "MMM. d, yy";
    int index8 = index7 + 1;
    tc.DateFieldText[index8] = "d MMMM, yyyy";
    int index9 = index8 + 1;
    tc.DateFieldText[index9] = "MMMM, yy";
    int index10 = index9 + 1;
    tc.DateFieldText[index10] = "MMM-yy";
    int index11 = index10 + 1;
    tc.DateFieldText[index11] = shortDatePattern == "M/d/yyyy" ? "M/d/yy h:mm am/pm" : "d/M/yy h:mm am/pm";
    int index12 = index11 + 1;
    tc.DateFieldText[index12] = shortDatePattern == "M/d/yyyy" ? "M/d/yy h:mm:ss am/pm" : "d/M/yy h:mm:ss am/pm";
    int index13 = index12 + 1;
    tc.DateFieldText[index13] = "h:mm am/pm";
    int index14 = index13 + 1;
    tc.DateFieldText[index14] = "h:mm:ss am/pm";
    int index15 = index14 + 1;
    tc.DateFieldText[index15] = "HH:mm";
    int index16 = index15 + 1;
    tc.DateFieldText[index16] = "HH:mm:ss";
    tc.TotalDateFmts = index16 + 1;
    if (ch1 != '/' || ch2 != ':')
    {
      for (int index17 = 0; index17 < tc.TotalDateFmts; ++index17)
      {
        char[] charArray = tc.DateFieldText[index17].ToCharArray();
        int length = tc.DateFieldText[index17].Length;
        for (int index18 = 0; index18 < length; ++index18)
        {
          if (ch1 != '/' && charArray[index18] == '/' && charArray[index18 + 1] != 'p')
            charArray[index18] = ch1;
          if (ch2 != ':' && charArray[index18] == ':')
            charArray[index18] = ch2;
        }
        tc.DateFieldText[index17] = new string(charArray, 0, length);
      }
    }
    tc.WeekDays[0] = "Sunday";
    tc.WeekDays[1] = "Monday";
    tc.WeekDays[2] = "Tuesday";
    tc.WeekDays[3] = "Wednesday";
    tc.WeekDays[4] = "Thursday";
    tc.WeekDays[5] = "Friday";
    tc.WeekDays[6] = "Saturday";
    tc.MonthNames[0] = "January";
    tc.MonthNames[1] = "February";
    tc.MonthNames[2] = "March";
    tc.MonthNames[3] = "April";
    tc.MonthNames[4] = "May";
    tc.MonthNames[5] = "June";
    tc.MonthNames[6] = "July";
    tc.MonthNames[7] = "August";
    tc.MonthNames[8] = "September";
    tc.MonthNames[9] = "October";
    tc.MonthNames[10] = "November";
    tc.MonthNames[11] = "December";
    int index19 = 0;
    tc.DefPaperSize[index19] = 1;
    tc.DefPaperKind[index19] = PaperKind.Letter;
    tc.DefPaperName[index19] = "Letter";
    tc.DefPaperWidth[index19] = 8.5f;
    tc.DefPaperHeight[index19] = 11f;
    int index20 = 1;
    tc.DefPaperSize[index20] = 5;
    tc.DefPaperKind[index20] = PaperKind.Legal;
    tc.DefPaperName[index20] = "Legal";
    tc.DefPaperWidth[index20] = 8.5f;
    tc.DefPaperHeight[index20] = 14f;
    int index21 = 2;
    tc.DefPaperSize[index21] = 4;
    tc.DefPaperKind[index21] = PaperKind.Ledger;
    tc.DefPaperName[index21] = "Ledger";
    tc.DefPaperWidth[index21] = 17f;
    tc.DefPaperHeight[index21] = 11f;
    int index22 = 3;
    tc.DefPaperSize[index22] = 3;
    tc.DefPaperKind[index22] = PaperKind.Tabloid;
    tc.DefPaperName[index22] = "Tabloid";
    tc.DefPaperWidth[index22] = 11f;
    tc.DefPaperHeight[index22] = 17f;
    int index23 = 4;
    tc.DefPaperSize[index23] = 6;
    tc.DefPaperKind[index23] = PaperKind.Statement;
    tc.DefPaperName[index23] = "Statement";
    tc.DefPaperWidth[index23] = 5.5f;
    tc.DefPaperHeight[index23] = 8.5f;
    int index24 = 5;
    tc.DefPaperSize[index24] = 7;
    tc.DefPaperKind[index24] = PaperKind.Executive;
    tc.DefPaperName[index24] = "Executive";
    tc.DefPaperWidth[index24] = 7.25f;
    tc.DefPaperHeight[index24] = 10.5f;
    int index25 = 6;
    tc.DefPaperSize[index25] = 8;
    tc.DefPaperKind[index25] = PaperKind.A3;
    tc.DefPaperName[index25] = "A3";
    tc.DefPaperWidth[index25] = this.MmToInches(297);
    tc.DefPaperHeight[index25] = this.MmToInches(420);
    int index26 = 7;
    tc.DefPaperSize[index26] = 9;
    tc.DefPaperKind[index26] = PaperKind.A4;
    tc.DefPaperName[index26] = "A4";
    tc.DefPaperWidth[index26] = this.MmToInches(210);
    tc.DefPaperHeight[index26] = this.MmToInches(297);
    int index27 = 8;
    tc.DefPaperSize[index27] = 11;
    tc.DefPaperKind[index27] = PaperKind.A5;
    tc.DefPaperName[index27] = "A5";
    tc.DefPaperWidth[index27] = this.MmToInches(148);
    tc.DefPaperHeight[index27] = this.MmToInches(210);
    int index28 = 9;
    tc.DefPaperSize[index28] = 12;
    tc.DefPaperKind[index28] = PaperKind.B4;
    tc.DefPaperName[index28] = "B4";
    tc.DefPaperWidth[index28] = this.MmToInches(250);
    tc.DefPaperHeight[index28] = this.MmToInches(354);
    int index29 = 10;
    tc.DefPaperSize[index29] = 13;
    tc.DefPaperKind[index29] = PaperKind.B5;
    tc.DefPaperName[index29] = "B5";
    tc.DefPaperWidth[index29] = this.MmToInches(182);
    tc.DefPaperHeight[index29] = this.MmToInches(257);
    tc.DefPaperCount = index29 + 1;
    tc.StSearched = false;
    tc.hSpell = (Assembly) null;
    tc.pStParseLine = (MethodInfo) null;
    tc.pStResetUserDict = (MethodInfo) null;
    tc.pStClearHist = (MethodInfo) null;
    tc.pSpellWord = (MethodInfo) null;
    tc.hHts = IntPtr.Zero;
    tc.SspSearched = false;
    tc.WininetSearched = false;
    return true;
  }

  internal static bool TerSetLicenseKey(string key)
  {
    CInit.CheckEval(true, key);
    if (tc.eval && key != "ttttt-ttttt-ttttt" && key != "uuuuu-uuuuu-uuuuu")
    {
      int num = (int) MessageBox.Show("Invalid Product License Key!");
    }
    return !tc.eval;
  }
}
