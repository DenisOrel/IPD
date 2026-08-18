// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.CFct
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class CFct : tc
{
  internal CFct(ImRtfEditor ImRtfEditor)
    : base(ImRtfEditor)
  {
  }

  internal void AbsToRowCol(int abs, char dest) => this.pos.AbsToRowCol(abs, dest);

  /// <summary>Преобразовать абсолютную (сквозную) позицию в строке текста в строку и столбец текста</summary>
  /// <param name="abs">Абсолютная позиция в строке текста</param>
  /// <param name="row">Строка</param>
  /// <param name="col">Столбец</param>
  /// <param name="internalPos">Преобразовать в позицию внутреннего представления строки с учётом замен.
  /// Иначе в позицию в оригинальной строке до замен формул и спецсимволов</param>
  /// <param name="scanAllChars">Принудительно проверять весь текст,
  /// для поиска переносов строки, которые находятся не в конце строки</param>
  internal void AbsToRowCol(
    int abs,
    out int row,
    out int col,
    bool internalPos = true,
    bool scanAllChars = false)
  {
    this.pos.AbsToRowCol(abs, out row, out col, internalPos, scanAllChars);
  }

  internal bool ActivateWheel(int lParam) => this.draw.ActivateWheel(lParam);

  internal void AddChar(ref string str, char chr) => this.misc.AddChar(ref str, chr);

  internal int AddCrLf(int index, char[] pUndo, ushort[] pUndoCfmt)
  {
    return this.misc.AddCrLf(index, pUndo, pUndoCfmt);
  }

  internal bool AddExt(string name, string ext) => this.io.AddExt(name, ext);

  internal void AddSlashes(string InStr, out string OutStr, int count)
  {
    this.misc.AddSlashes(InStr, out OutStr, count);
  }

  internal bool AdjustBlockForTable(bool AdjustCurPos)
  {
    return this.tbl.AdjustBlockForTable(AdjustCurPos);
  }

  internal bool AdjustBlockForTable(
    ref int pBegRow,
    ref int pBegCol,
    ref int pEndRow,
    ref int pEndCol,
    bool AdjustCurPos)
  {
    return this.tbl.AdjustBlockForTable(ref pBegRow, ref pBegCol, ref pEndRow, ref pEndCol, AdjustCurPos);
  }

  internal bool AdjustFontHeight(int NewFont, int ResY, bool apply)
  {
    return this.fnt.AdjustFontHeight(NewFont, ResY, apply);
  }

  internal void AdjustHiddenPos() => this.pos.AdjustHiddenPos();

  internal bool AdjustHilight() => this.draw.AdjustHilight();

  internal bool AdjustHtmlPictWidth() => this.misc.AdjustHtmlPictWidth();

  internal bool AdjustHtmlRulerWidth() => this.misc.AdjustHtmlRulerWidth();

  internal int AdjustPageNbr(int PageNo, int LineNo) => this.page.AdjustPageNbr(PageNo, LineNo);

  internal bool AdjustSections(int AfterLine, int count)
  {
    return this.sec.AdjustSections(AfterLine, count);
  }

  internal bool AdjustTableRowWidth(int row) => this.tbl.AdjustTableRowWidth(row);

  internal bool AllocArrayMem() => this.init.AllocArrayMem();

  internal bool AllocLinePtr(int line) => this.init.AllocLinePtr(line);

  internal bool AllocTabw(int line) => this.misc.AllocTabw(line);

  internal bool AllocTabwCharFlags(int line) => this.misc.AllocTabwCharFlags(line);

  internal bool AllocWrapBuf(int size) => this.init.AllocWrapBuf(size);

  internal bool AllSelected() => this.blk.AllSelected();

  internal bool AllSelected2(out bool LastCharSelected)
  {
    return this.blk.AllSelected2(out LastCharSelected);
  }

  internal string AlphaFormat(int nbr, bool upper) => this.misc.AlphaFormat(nbr, upper);

  internal bool AnchorParaFound(int line, int CurFID) => this.frm.AnchorParaFound(line, CurFID);

  internal bool AnchorPictFrame(int pict, int LineNo, int col)
  {
    return this.frm.AnchorPictFrame(pict, LineNo, col);
  }

  internal bool ApplyCharStyles(int CurStyle) => this.par.ApplyCharStyles(CurStyle, true);

  internal bool ApplyLineTextStyle(int LineNo, int CurParaStyId, int PrevParaStyId)
  {
    return this.par.ApplyLineTextStyle(LineNo, CurParaStyId, PrevParaStyId);
  }

  internal bool ApplyPaperOrient(bool IsPortrait) => this.prt.ApplyPaperOrient(IsPortrait);

  internal bool ApplyPaperSize(PaperSize size) => this.prt.ApplyPaperSize(size);

  internal ushort ApplyParaStyleOnFont(int CurFont, int ParaStyId)
  {
    return this.par.ApplyParaStyleOnFont(CurFont, ParaStyId);
  }

  internal bool ApplyParaStyles(int CurStyle) => this.par.ApplyParaStyles(CurStyle);

  internal bool ApplyPictureBrightnessContrast(int pict, int bright, int contrast)
  {
    return this.rtfr.ApplyPictureBrightnessContrast(pict, bright, contrast);
  }

  internal bool ApplyZoomPercent(int NewZoom) => this.misc.ApplyZoomPercent(NewZoom);

  internal bool AutoSpellCheck(int LineNo) => this.spl.AutoSpellCheck(LineNo);

  internal bool BkPictOut(Graphics gr, int x, int y) => this.draw.BkPictOut(gr, x, y);

  internal bool BlockHasProtectOn(bool msg, bool SkipFieldNames)
  {
    return this.blk.BlockHasProtectOn(msg, SkipFieldNames);
  }

  internal bool BlockHasProtectOn2(
    int line1,
    int col1,
    int line2,
    int col2,
    bool msg,
    bool SkipFieldNames)
  {
    return this.blk.BlockHasProtectOn2(line1, col1, line2, col2, msg, SkipFieldNames);
  }

  internal bool CalcBorderSpill(int x1, int y1, int x2, int y2)
  {
    return this.draw.CalcBorderSpill(x1, y1, x2, y2);
  }

  internal bool CalcFrameSpace(
    int line,
    COp.RECT rect,
    out int FrameX,
    out int FrameWidth,
    out int FrameHt,
    int sect,
    bool GetRowSpace,
    bool GetLineSpace,
    bool GetRowIndent,
    int PageNo)
  {
    return this.frm.CalcFrameSpace(line, rect, out FrameX, out FrameWidth, out FrameHt, sect, GetRowSpace, GetLineSpace, GetRowIndent, PageNo);
  }

  internal int CalcFrmIndentBefRow(int line, int sect) => this.frm.CalcFrmIndentBefRow(line, sect);

  internal int CalcFrmSpcBef(int line, int sect, bool set, int PageNo)
  {
    return this.frm.CalcFrmSpcBef(line, sect, set, PageNo);
  }

  internal int CalcFrmSpcBefRow(int line, int sect) => this.frm.CalcFrmSpcBefRow(line, sect);

  internal bool CallDialogBox(Form dlg) => this.misc.CallDialogBox(dlg);

  internal bool CanDragText() => this.edit.CanDragText();

  internal bool CanInsert(int line, int col) => this.edit.CanInsert(line, col);

  internal bool CanInsertBreakChar(int line, int col) => this.edit.CanInsertBreakChar(line, col);

  internal bool CanInsertInInputField(int pict, int line, int col)
  {
    return this.edit.CanInsertInInputField(pict, line, col);
  }

  internal bool CanInsertObject(int line, int col) => this.edit.CanInsertObject(line, col);

  internal bool CanInsertTable(int line, int col) => this.tbl.CanInsertTable(line, col);

  internal bool CanInsertTextObject(int line, int col) => this.edit.CanInsertTextObject(line, col);

  internal bool ChangeLineTextStyle(int LineNo, int CurParaStyId)
  {
    return this.par.ChangeLineTextStyle(LineNo, CurParaStyId);
  }

  internal bool CharFmt(
    tc.DgtGetNewFontId GetNewFontId,
    int data1,
    int data2,
    string str1,
    bool repaint)
  {
    return this.fnt.CharFmt(GetNewFontId, data1, data2, str1, repaint);
  }

  internal bool CharFmtChr(
    tc.DgtGetNewFontId GetNewFontId,
    int data1,
    int data2,
    string str1,
    bool repaint)
  {
    return this.fnt.CharFmtChr(GetNewFontId, data1, data2, str1, repaint);
  }

  internal bool CharFmtLine(
    tc.DgtGetNewFontId GetNewFontId,
    int data1,
    int data2,
    string str1,
    bool repaint)
  {
    return this.fnt.CharFmtLine(GetNewFontId, data1, data2, str1, repaint);
  }

  internal bool CharMessagePending() => this.misc.CharMessagePending();

  internal bool CharWidthAlloc(int line, int OldSize, int NewSize)
  {
    return this.fnt.CharWidthAlloc(line, OldSize, NewSize);
  }

  internal bool CheckDlgValue(
    Form form,
    char type,
    TextBox tb,
    double BeginRange,
    double EndRange)
  {
    return this.misc.CheckDlgValue(form, type, tb, BeginRange, EndRange);
  }

  internal bool CheckImageMapHit(int pict) => this.link.CheckImageMapHit(pict);

  internal bool CheckWindowOverflow() => this.blk.CheckWindowOverflow();

  internal bool ClearEOL(Graphics gr, int x, int y, int LastX, int height, bool ClearBknd)
  {
    return this.draw.ClearEOL(gr, x, y, LastX, height, ClearBknd);
  }

  internal bool ClearScrForXparentWrite(Graphics gr) => this.draw.ClearScrForXparentWrite(gr);

  internal bool ClearTabDlg() => this.par.ClearTabDlg();

  internal tc.ClsLinePtr CloneLine(int line) => this.init.CloneLine(line);

  internal tc.ClsLinePtr[] CloneLinePtr() => this.init.CloneLinePtr();

  internal void CloseCfmt(int line) => this.fnt.CloseCfmt(line);

  internal void CloseCharInfo(int line) => this.fnt.CloseCharInfo(line);

  internal void CloseCtid(int line) => this.fnt.CloseCtid(line);

  internal bool CodeListText(string InText, char[] code) => this.par.CodeListText(InText, code);

  internal int ColToUnits(int col, int LineNo, int CursPos)
  {
    return this.pos.ColToUnits(col, LineNo, CursPos);
  }

  internal void CompressCfmt(int line) => this.fnt.CompressCfmt(line);

  internal void CompressCtid(int line) => this.fnt.CompressCtid(line);

  internal bool ConvertibleToTrueType(string RastFont, ref string TtFont)
  {
    return this.fnt.ConvertibleToTrueType(RastFont, ref TtFont);
  }

  internal bool CopyCell(int src, int dest) => this.tbl.CopyCell(src, dest);

  internal bool CopyCharStyle(int src, int dest) => this.par.CopyCharStyle(src, dest);

  internal bool CopyCtlImage(int pict) => this.draw.CopyCtlImage(pict);

  internal bool CopyFromClipboard(string format, DataObject data)
  {
    return this.blk.CopyFromClipboard(format, data);
  }

  internal void CopyLineData(int SrcLine, int DestLine)
  {
    this.edit.CopyLineData(SrcLine, DestLine);
  }

  internal bool CopyTabw(int FromLine, int ToLine) => this.misc.CopyTabw(FromLine, ToLine);

  internal bool CopyToClipboard(int CmdId, bool ToCB) => this.blk.CopyToClipboard(CmdId, ToCB);

  internal bool CopyWrapLineData(int LineBegin, int LineEnd, char CurChar, int lflags2)
  {
    return this.wrp.CopyWrapLineData(LineBegin, LineEnd, CurChar, lflags2);
  }

  internal bool CrackAutoNumLgl(string NbrText, out string prefix, out int pNbr)
  {
    return this.par.CrackAutoNumLgl(NbrText, out prefix, out pNbr);
  }

  internal bool CreateCellDragObj(int type, int id, int BorderX, int BorderY1, int BorderY2)
  {
    return this.drag.CreateCellDragObj(type, id, BorderX, BorderY1, BorderY2);
  }

  internal int CreateDefList(string name) => this.par.CreateDefList(name);

  internal int CreateDefListOr(int ListId, int StartAt, bool OverrideStartAt, int level)
  {
    return this.par.CreateDefListOr(ListId, StartAt, OverrideStartAt, level);
  }

  internal bool CreateEndnote() => this.page.CreateEndnote();

  internal void CreateFrames(bool printer, int PageNo, int LastPage)
  {
    this.frm.CreateFrames(printer, PageNo, LastPage);
  }

  internal int CreateGlbFont(COp.LOGFONT lFont, Graphics gr) => this.fnt.CreateGlbFont(lFont, gr);

  internal bool CreateOneFont(Graphics gr, int NewFont, bool ScreenFont)
  {
    return this.fnt.CreateOneFont(gr, NewFont, ScreenFont);
  }

  internal bool CreatePageHdrFtr(char type, int sect) => this.sec.CreatePageHdrFtr(type, sect);

  internal bool CreateRowDragObj(int type, int id, int BorderX1, int BorderX2, int BorderY)
  {
    return this.drag.CreateRowDragObj(type, id, BorderX1, BorderX2, BorderY);
  }

  internal bool CreateToc() => this.sec.CreateToc();

  internal bool CreateToolBar() => this.bar.CreateToolBar();

  internal bool CtidAlloc(int line, int OldSize, int NewSize)
  {
    return this.fnt.CtidAlloc(line, OldSize, NewSize);
  }

  internal bool CurrentPrinter() => this.prt.CurrentPrinter();

  internal bool CursorOnFirstWord() => this.pos.CursorOnFirstWord();

  internal bool DecodeListText(char[] code, out string StrText)
  {
    return this.par.DecodeListText(code, out StrText);
  }

  internal bool DelCell(int CurCell) => this.tbl.DelCell(CurCell);

  internal bool DeleteCharBlock(bool SetCurPos, bool repaint)
  {
    return this.blk.DeleteCharBlock(SetCurPos, repaint);
  }

  internal bool DeleteDragObjects(int FirstType, int LastType)
  {
    return this.drag.DeleteDragObjects(FirstType, LastType);
  }

  internal bool DeleteFrame() => this.frm.DeleteFrame();

  internal bool DeleteLineBlock(bool disp) => this.blk.DeleteLineBlock(disp);

  internal int DeleteTag(int line, int col, int type, string name)
  {
    return this.fnt.DeleteTag(line, col, type, name);
  }

  internal bool DeleteTerObject(int idx) => this.fnt.DeleteTerObject(idx);

  internal bool DeleteTextMap(bool RedrawBackground) => this.draw.DeleteTextMap(RedrawBackground);

  internal bool DestroyToolBar() => this.bar.DestroyToolBar();

  internal bool DisableIme(bool repaint) => this.misc.DisableIme(repaint);

  internal bool DisengageCaret() => this.pos.DisengageCaret();

  internal bool DisplacePointers(int StartLine, int count)
  {
    return this.wrp.DisplacePointers(StartLine, count);
  }

  internal bool DisplayStatus() => this.draw.DisplayStatus();

  internal bool DisplayStatusInfo() => this.draw.DisplayStatusInfo();

  internal bool DisplayText(
    Graphics gr,
    int col,
    int RowUnits,
    int height,
    int BaseHeight,
    char[] ptr,
    int count,
    int line,
    bool ToEOL,
    char area)
  {
    return this.draw.DisplayText(gr, col, RowUnits, height, BaseHeight, ptr, count, line, ToEOL, area);
  }

  internal bool DispMsg(int msg) => this.misc.DispMsg(msg);

  internal bool DisposeBkPictBM() => this.misc.DisposeBkPictBM();

  internal Color DlgEditColor(Control form, Color InColor, bool FullOpen)
  {
    return this.misc.DlgEditColor(form, InColor, FullOpen);
  }

  internal bool DlgEditFont() => this.misc.DlgEditFont();

  internal int DlgListCharAft(ComboBox box, int val, bool set)
  {
    return this.par.DlgListCharAft(box, val, set);
  }

  internal int DlgListNumType(ComboBox box, int val, bool set)
  {
    return this.par.DlgListNumType(box, val, set);
  }

  internal bool dm(string DebugMsg) => this.misc.dm(DebugMsg);

  internal bool DoAutoSpellCheck() => this.spl.DoAutoSpellCheck();

  internal bool DocFitsInWindow() => this.pos.DocFitsInWindow();

  internal bool DoPopupSelection(int CmdId) => this.misc.DoPopupSelection(CmdId);

  internal bool DoRulerClick(MouseButtons button, int lParam)
  {
    return this.misc.DoRulerClick(button, lParam);
  }

  internal bool DragApply(int DeltaX, int DeltaY, int LastX, int LastY)
  {
    return this.drag.DragApply(DeltaX, DeltaY, LastX, LastY);
  }

  internal bool DragApplyLineSize(int DeltaX, int DeltaY)
  {
    return this.drag.DragApplyLineSize(DeltaX, DeltaY);
  }

  internal bool DragCellSize(int DeltaX) => this.drag.DragCellSize(DeltaX);

  internal bool DragFrameMove(int DeltaX, int DeltaY) => this.drag.DragFrameMove(DeltaX, DeltaY);

  internal bool DragPictFrameSize(int type, int DeltaX, int DeltaY)
  {
    return this.drag.DragPictFrameSize(type, DeltaX, DeltaY);
  }

  internal bool DragRowIndent(int DeltaX) => this.drag.DragRowIndent(DeltaX);

  internal bool DragRowSize(int DeltaY) => this.drag.DragRowSize(DeltaY);

  internal bool DragRulerIndent(int DeltaX) => this.drag.DragRulerIndent(DeltaX);

  internal bool DragRulerTab(int DeltaX, int LastY) => this.drag.DragRulerTab(DeltaX, LastY);

  internal bool DragText(int x, int y) => this.drag.DragText(x, y);

  internal bool DrawAnimPict(int TimerId) => this.draw.DrawAnimPict(TimerId);

  internal bool DrawBkPictRect(COp.RECT rect, Graphics gr) => this.draw.DrawBkPictRect(rect, gr);

  internal bool DrawBullet(
    Graphics hDrawGr,
    int LineNo,
    int CurPara,
    int x,
    int y,
    int BaseHeight,
    bool DoDraw)
  {
    return this.draw.DrawBullet(hDrawGr, LineNo, CurPara, x, y, BaseHeight, DoDraw);
  }

  internal bool DrawDarkDottedLine(Graphics gr, int x1, int y1, int x2, int y2)
  {
    return this.draw.DrawDarkDottedLine(gr, x1, y1, x2, y2);
  }

  internal bool DrawDottedLine(Graphics gr, int x1, int y1, int x2, int y2, Color color)
  {
    return this.draw.DrawDottedLine(gr, x1, y1, x2, y2, color);
  }

  internal bool DrawDragCellLine() => this.drag.DrawDragCellLine();

  internal bool DrawDragFrameRect() => this.drag.DrawDragFrameRect();

  internal bool DrawDragFrameTabs() => this.drag.DrawDragFrameTabs();

  internal bool DrawDragHotSpots(int type) => this.drag.DrawDragHotSpots(type);

  internal bool DrawDragPictRect() => this.drag.DrawDragPictRect();

  internal bool DrawDragRowLine() => this.drag.DrawDragRowLine();

  internal bool DrawDragRulerIndent() => this.drag.DrawDragRulerIndent();

  internal bool DrawDragRulerTab(int LastY) => this.drag.DrawDragRulerTab(LastY);

  internal bool DrawEval() => this.draw.DrawEval();

  internal bool DrawFootnote(Graphics gr, int FrameNo, bool screen)
  {
    return this.draw.DrawFootnote(gr, FrameNo, screen);
  }

  internal bool DrawFrameBorder(
    Graphics gr,
    int x1,
    int y1,
    int x2,
    int y2,
    int thickness,
    int border)
  {
    return this.draw.DrawFrameBorder(gr, x1, y1, x2, y2, thickness, border);
  }

  internal bool DrawHtmlCellBorder(
    Graphics gr,
    int CellId,
    int x1,
    int y1,
    int x2,
    int y2,
    int[] BorderWidth,
    bool screen)
  {
    return this.draw.DrawHtmlCellBorder(gr, CellId, x1, y1, x2, y2, BorderWidth, screen);
  }

  internal bool DrawLightDottedLine(Graphics gr, int x1, int y1, int x2, int y2)
  {
    return this.draw.DrawLightDottedLine(gr, x1, y1, x2, y2);
  }

  internal bool DrawLineObject(Graphics gr, int FrameNo) => this.draw.DrawLineObject(gr, FrameNo);

  internal int DrawOneFootnote(
    Graphics gr,
    int x,
    int y,
    int line,
    int BufLen,
    int WrapWidth,
    bool screen,
    bool DrawIt)
  {
    return this.draw.DrawOneFootnote(gr, x, y, line, BufLen, WrapWidth, screen, DrawIt);
  }

  internal bool DrawPageBorderBox(Graphics gr, int FrameNo)
  {
    return this.draw.DrawPageBorderBox(gr, FrameNo);
  }

  internal bool DrawPageBox(Graphics gr, int pos) => this.draw.DrawPageBox(gr, pos);

  internal bool DrawParaBorder(Graphics gr, COp.RECT TextRect, bool outer)
  {
    return this.draw.DrawParaBorder(gr, TextRect, outer);
  }

  internal bool DrawPictFrame(Graphics gr, int FrameNo) => this.draw.DrawPictFrame(gr, FrameNo);

  internal bool DrawPictRect(Graphics gr, int FrameNo) => this.draw.DrawPictRect(gr, FrameNo);

  internal bool DrawRectObject(Graphics gr, int FrameNo) => this.draw.DrawRectObject(gr, FrameNo);

  internal void DrawRuler(bool CanPostpone) => this.draw.DrawRuler(CanPostpone);

  internal bool DrawSectLineNbr(Graphics gr, int row, int x, int y, int BaseHeight)
  {
    return this.draw.DrawSectLineNbr(gr, row, x, y, BaseHeight);
  }

  internal bool DrawShadowBox(
    Graphics gr,
    int x1,
    int y1,
    int x2,
    int y2,
    Pen hSolidPen,
    Pen hShadowPen)
  {
    return this.draw.DrawShadowBox(gr, x1, y1, x2, y2, hSolidPen, hShadowPen);
  }

  internal bool DrawShadowLine(
    Graphics gr,
    int x1,
    int y1,
    int x2,
    int y2,
    Pen SolidPen,
    Pen ShadowPen)
  {
    return this.draw.DrawShadowLine(gr, x1, y1, x2, y2, SolidPen, ShadowPen);
  }

  internal bool DrawTrackingLine(Graphics gr, int x, int y, int height)
  {
    return this.draw.DrawTrackingLine(gr, x, y, height);
  }

  internal bool DrawWheel(Graphics gr) => this.draw.DrawWheel(gr);

  internal bool DrawWigglyLine(Graphics gr, int x1, int y1, int x2, int y2, Color color)
  {
    return this.draw.DrawWigglyLine(gr, x1, y1, x2, y2, color);
  }

  internal bool EditingInputField(bool del, bool bksp) => this.fld.EditingInputField(del, bksp);

  internal bool EditInputField() => this.fld.EditInputField();

  internal bool EditPicture() => this.blk.EditPicture();

  internal bool EnableToolbarIcons(bool enable) => this.bar.EnableToolbarIcons(enable);

  internal bool EnableVScrollBar(bool enable) => this.init.EnableVScrollBar(enable);

  internal bool EngageCaret(int cmd) => this.pos.EngageCaret(cmd);

  internal bool ExitOle() => this.ole.ExitOle();

  internal void ExpandCfmt(int line) => this.fnt.ExpandCfmt(line);

  internal void ExpandCtid(int line) => this.fnt.ExpandCtid(line);

  internal bool ExpandFontTable(int NewMaxFonts) => this.fnt.ExpandFontTable(NewMaxFonts);

  internal bool ExpandRowArrays(int idx) => this.misc.ExpandRowArrays(idx);

  internal bool ExpandSectArray(int NewMax) => this.sec.ExpandSectArray(NewMax);

  internal bool ExternalDrop(
    DataObject data,
    DragDropEffects effects,
    int KeyState,
    int x,
    int y)
  {
    return this.drag.ExternalDrop(data, effects, KeyState, x, y);
  }

  internal int ExtractFootnote(
    Graphics gr,
    int x,
    int y,
    int line,
    int sect,
    bool screen,
    bool DoDraw)
  {
    return this.page.ExtractFootnote(gr, x, y, line, sect, screen, DoDraw);
  }

  internal string ExtractQuotedText(string str) => this.misc.ExtractQuotedText(str);

  internal bool FieldFound(int LineNo, int ColNo, string name, bool exact)
  {
    return this.fld.FieldFound(LineNo, ColNo, name, exact);
  }

  internal byte[] FileToByteArray(string FileName, out int size)
  {
    return this.misc.FileToByteArray(FileName, out size);
  }

  internal int FillFontBox(ComboBox cb) => this.misc.FillFontBox(cb);

  internal bool FillPointBox(ComboBox cb) => this.misc.FillPointBox(cb);

  internal bool FillStyleBox(object box, int type, bool SelectCurrent, bool IsListBox)
  {
    return this.par.FillStyleBox(box, type, SelectCurrent, IsListBox);
  }

  internal int FindOpenSlot() => this.fnt.FindOpenSlot();

  internal bool FitPictureInFrame(int line, bool ResizePicture)
  {
    return this.frm.FitPictureInFrame(line, ResizePicture);
  }

  internal bool FixImageDest(ref int DestX, ref int DestY, int DestWidth, int DestHeight)
  {
    return this.draw.FixImageDest(ref DestX, ref DestY, DestWidth, DestHeight);
  }

  internal bool FixPos() => this.edit.FixPos();

  internal bool FixPos(ref int pLine, ref int pCol) => this.edit.FixPos(ref pLine, ref pCol);

  internal bool FmtAlloc(int line, int OldSize, int NewSize)
  {
    return this.fnt.FmtAlloc(line, OldSize, NewSize);
  }

  internal bool FrameNoRotateDC(Graphics gr) => this.pos.FrameNoRotateDC(gr);

  internal bool FrameRotateDC(Graphics gr, int FrameNo) => this.pos.FrameRotateDC(gr, FrameNo);

  internal bool FrameRotateRect(ref COp.RECT rect, int FrameNo)
  {
    return this.pos.FrameRotateRect(ref rect, FrameNo);
  }

  internal int FrameRotateX(int x, int y, int FrameNo) => this.pos.FrameRotateX(x, y, FrameNo);

  internal int FrameRotateY(int x, int y, int FrameNo) => this.pos.FrameRotateY(x, y, FrameNo);

  internal bool FrameToDraw(int FrameNo, int PaintLine)
  {
    return this.draw.FrameToDraw(FrameNo, PaintLine);
  }

  internal int FrameToMargX(int x) => this.frm.FrameToMargX(x);

  internal int FrameToPageY(int y) => this.frm.FrameToPageY(y);

  internal bool FreeClonedLinePtr(tc.ClsLinePtr[] ptr, int LineCount)
  {
    return this.init.FreeClonedLinePtr(ptr, LineCount);
  }

  internal bool FreeFontResources(bool always) => this.fnt.FreeFontResources(always);

  internal bool FreeHtmlAddOn() => this.misc.FreeHtmlAddOn();

  internal bool FreeImageMapTable() => this.link.FreeImageMapTable();

  internal bool FreeLine(int line) => this.init.FreeLine(line);

  internal bool FreeLinePtr(int line) => this.init.FreeLinePtr(line);

  internal bool FreeList(int ListId) => this.par.FreeList(ListId);

  internal bool FreeListOr(int ListId) => this.par.FreeListOr(ListId);

  internal bool FreeListTable() => this.par.FreeListTable();

  internal bool FreeReviewer(int idx) => this.trk.FreeReviewer(idx);

  internal bool FreeRtfInfo() => this.init.FreeRtfInfo();

  internal bool FreeTabw(int line) => this.misc.FreeTabw(line);

  internal bool FreeTabwMembers(int line) => this.misc.FreeTabwMembers(line);

  internal bool FreeTag(int TagId) => this.fnt.FreeTag(TagId);

  internal bool FreeToolbar() => this.bar.FreeToolbar();

  internal int GetAnchorY(int AnchorLine) => this.frm.GetAnchorY(AnchorLine);

  internal uint GetBitVal(uint val, int off, int bits) => this.misc.GetBitVal(val, off, bits);

  internal int GetBorderCell(int CellId, int PageNo, bool next)
  {
    return this.frm.GetBorderCell(CellId, PageNo, next);
  }

  internal int GetBorderLeftSpace(int PageNo) => this.frm.GetBorderLeftSpace(PageNo);

  internal bool GetCaretXY(int CaretLine, int CaretCol, int y, out int pX, out int pY)
  {
    return this.pos.GetCaretXY(CaretLine, CaretCol, y, out pX, out pY);
  }

  internal int GetCellColumn(int CurCell, bool UseColSpan)
  {
    return this.tbl.GetCellColumn(CurCell, UseColSpan);
  }

  internal int GetCellFrameBotWidth(int CellId, ref int borders, int PageNo, out Color pColor)
  {
    return this.frm.GetCellFrameBotWidth(CellId, ref borders, PageNo, out pColor);
  }

  internal int GetCellFrameLeftWidth(int CellId, ref int borders, out Color pColor)
  {
    return this.frm.GetCellFrameLeftWidth(CellId, ref borders, out pColor);
  }

  internal int GetCellFrameRightWidth(int CellId, ref int borders, out Color pColor)
  {
    return this.frm.GetCellFrameRightWidth(CellId, ref borders, out pColor);
  }

  internal int GetCellFrameTopWidth(int CellId, ref int borders, int PageNo, out Color pColor)
  {
    return this.frm.GetCellFrameTopWidth(CellId, ref borders, PageNo, out pColor);
  }

  internal bool GetCellMinMaxWidth(int cl, out int MinWidth, out int MaxWidth, int TblWidth)
  {
    return this.tbl.GetCellMinMaxWidth(cl, out MinWidth, out MaxWidth, TblWidth);
  }

  internal int GetCellRightX(int CurCell) => this.tbl.GetCellRightX(CurCell);

  internal int GetCellSlot(bool recover) => this.tbl.GetCellSlot(recover);

  internal int GetCharSeg(int LineNo, int col, int SegCount, tc.StrLineSeg[] pSegIn)
  {
    return this.pos.GetCharSeg(LineNo, col, SegCount, pSegIn);
  }

  internal byte GetCharSet(Graphics gr, string typeface, ref bool exists)
  {
    return this.fnt.GetCharSet(gr, typeface, ref exists);
  }

  internal int GetCharWidth(int line, int col) => this.fnt.GetCharWidth(line, col);

  internal int GetColumnCell(int row, int col, bool UseColSpan)
  {
    return this.tbl.GetColumnCell(row, col, UseColSpan);
  }

  internal int GetCtid(int line, int col) => this.fnt.GetCtid(line, col);

  internal int GetCurCfmt(int line, int col) => this.fnt.GetCurCfmt(line, col);

  internal char GetCurChar(int line, int col) => this.edit.GetCurChar(line, col);

  internal int GetCurPage(int LineNo) => this.page.GetCurPage(LineNo);

  internal bool GetCursDirection() => this.edit.GetCursDirection();

  internal bool GetDateString(string pDateFmt, out string DateString, int FieldFont)
  {
    return this.misc.GetDateString(pDateFmt, out DateString, FieldFont);
  }

  internal bool GetDlgListLevelPtr(
    bool ListItem,
    ComboBox box,
    ComboBox ListOrBox,
    ComboBox LevelBox,
    out tc.StrListLevel pLevel)
  {
    return this.par.GetDlgListLevelPtr(ListItem, box, ListOrBox, LevelBox, out pLevel);
  }

  internal int GetDocHeight() => this.blk.GetDocHeight();

  internal int GetDragObjectSlot() => this.drag.GetDragObjectSlot();

  internal int GetEffectiveCfmt() => this.fnt.GetEffectiveCfmt();

  internal bool GetFieldList(int FieldFont, int LineNo, out int pListOrId, out int pLevel)
  {
    return this.par.GetFieldList(FieldFont, LineNo, out pListOrId, out pLevel);
  }

  internal bool GetFieldLoc(int LineNo, int ColNo, bool begin, out int pLine, out int pCol)
  {
    return this.fld.GetFieldLoc(LineNo, ColNo, begin, out pLine, out pCol);
  }

  internal bool GetFieldScope(int LineNo, int ColNo, int type)
  {
    return this.fld.GetFieldScope(LineNo, ColNo, type);
  }

  internal bool GetFieldScope(
    int LineNo,
    int ColNo,
    int type,
    out int pBegLine,
    out int pBegCol,
    out int pEndLine,
    out int pEndCol)
  {
    return this.fld.GetFieldScope(LineNo, ColNo, type, out pBegLine, out pBegCol, out pEndLine, out pEndCol);
  }

  internal int GetFieldSwitchLong(string ParamList, string param, int DefValue, out bool pFound)
  {
    return this.par.GetFieldSwitchLong(ParamList, param, DefValue, out pFound);
  }

  internal bool GetFileName(
    bool open,
    ref string file,
    int FilterIndex,
    string filter,
    string ext)
  {
    return this.io.GetFileName(open, ref file, FilterIndex, filter, ext);
  }

  internal int GetFirstIndent(int LineNo, out int pBulletWidth, out bool pHasBullet, bool screen)
  {
    return this.par.GetFirstIndent(LineNo, out pBulletWidth, out pHasBullet, screen);
  }

  internal int GetFlatX(int x, int y, int line) => this.pos.GetFlatX(x, y, line);

  internal int GetFrame(int lin) => this.frm.GetFrame(lin);

  internal bool GetFrameSpace(
    int line,
    COp.RECT rect,
    out int FrameX,
    out int FrameWidth,
    out int FrameHt)
  {
    return this.frm.GetFrameSpace(line, rect, out FrameX, out FrameWidth, out FrameHt);
  }

  internal int GetFrameTextAngle(int FrameNo) => this.pos.GetFrameTextAngle(FrameNo);

  internal int GetFrmSpcBef(int line, bool InScrUnits) => this.frm.GetFrmSpcBef(line, InScrUnits);

  internal int GetHdrFtrFlag(int LineNo) => this.sec.GetHdrFtrFlag(LineNo);

  internal bool GetHdrFtrRange(char delim, int StartLine, out int pFirstLine, out int pCount)
  {
    return this.sec.GetHdrFtrRange(delim, StartLine, out pFirstLine, out pCount);
  }

  internal int GetHeadingNo(int CurStyle) => this.par.GetHeadingNo(CurStyle);

  internal bool GetHotSpotHit(Point pt, out int obj, out int HotSpot)
  {
    return this.pos.GetHotSpotHit(pt, out obj, out HotSpot);
  }

  internal bool GetHypertextEnd(ref int pLineNo, ref int pColNo)
  {
    return this.link.GetHypertextEnd(ref pLineNo, ref pColNo);
  }

  internal bool GetHypertextStart(ref int pLineNo, ref int pColNo)
  {
    return this.link.GetHypertextStart(ref pLineNo, ref pColNo);
  }

  internal int GetHyphPrefixLen(
    char[] CurWord,
    ushort[] pFmt,
    int idx,
    int MaxLen,
    int LimitCount,
    ushort[] WordWidth,
    int AvailWidth,
    out int PrefixWidth,
    bool UsePrtUnits)
  {
    return this.dsh.GetHyphPrefixLen(CurWord, pFmt, idx, MaxLen, LimitCount, WordWidth, AvailWidth, out PrefixWidth, UsePrtUnits);
  }

  internal int GetLastSpannedCellHeight(int CurCell, out int pScrHeight, int PageNo)
  {
    return this.tbl.GetLastSpannedCellHeight(CurCell, out pScrHeight, PageNo);
  }

  internal ushort[] GetLineCharWidth(int LineNo) => this.pos.GetLineCharWidth(LineNo, false);

  internal void GetLineData(
    int SrcLine,
    int SrcCol,
    int count,
    char[] ptr,
    ushort[] fmt,
    ushort[] ct)
  {
    this.edit.GetLineData(SrcLine, SrcCol, count, ptr, fmt, ct);
  }

  internal int GetLineHeight(int lin, out int pBaseHeight, out int pExtLead)
  {
    return this.pos.GetLineHeight(lin, out pBaseHeight, out pExtLead);
  }

  internal bool GetLinePoints(int FrameNo, out int x1, out int y1, out int x2, out int y2)
  {
    return this.frm.GetLinePoints(FrameNo, out x1, out y1, out x2, out y2);
  }

  internal tc.StrLineSeg[] GetLineSeg(int LineNo, ushort[] pWidthParam, out int pTotalLineSeg)
  {
    return this.pos.GetLineSeg(LineNo, pWidthParam, out pTotalLineSeg);
  }

  internal tc.StrLineSeg[] GetLineSeg2(
    int LineNo,
    ushort[] pWidthParam,
    out int pTotalLineSeg,
    int len,
    int CurX,
    char[] ptr,
    ushort[] fmt)
  {
    return this.pos.GetLineSeg2(LineNo, pWidthParam, out pTotalLineSeg, len, CurX, ptr, fmt);
  }

  internal bool GetLineSpacing(
    int lin,
    int TextHeight,
    out int SpcBef,
    out int SpcAft,
    bool screen)
  {
    return this.pos.GetLineSpacing(lin, TextHeight, out SpcBef, out SpcAft, screen);
  }

  internal bool GetLineSpacing2(
    int lin,
    int TextHeight,
    out int SpcBef,
    out int SpcAft,
    out int pParaSpcBef,
    out int pParaSpcAft,
    bool screen)
  {
    return this.pos.GetLineSpacing2(lin, TextHeight, out SpcBef, out SpcAft, out pParaSpcBef, out pParaSpcAft, screen);
  }

  internal bool GetLineSpacingAlt(
    int lin,
    int TextHeight,
    out int SpcBef,
    out int SpcAft,
    out int pParaSpcBef,
    out int pParaSpcAft,
    bool screen)
  {
    return this.pos.GetLineSpacingAlt(lin, TextHeight, out SpcBef, out SpcAft, out pParaSpcBef, out pParaSpcAft, screen);
  }

  internal int GetLineWidth(int lin, bool IncludeDelim, bool screen)
  {
    return this.pos.GetLineWidth(lin, IncludeDelim, screen, false);
  }

  internal bool GetListLevelPtr(int ListOrId, int level, out tc.StrListLevel pLevel)
  {
    return this.par.GetListLevelPtr(ListOrId, level, out pLevel);
  }

  internal int GetListOrSlot() => this.par.GetListOrSlot();

  internal int GetListSlot() => this.par.GetListSlot();

  internal bool GetListText(
    int ParaId,
    int LineNo,
    out string ListText,
    out int pListTextWidth,
    out int pListNbr,
    out int pFontId,
    int CurParaFont,
    int FieldNbr,
    bool UseLogUnits)
  {
    return this.par.GetListText(ParaId, LineNo, out ListText, out pListTextWidth, out pListNbr, out pFontId, CurParaFont, FieldNbr, UseLogUnits);
  }

  internal ushort GetNewCharStyle(
    ushort OldFmt,
    int data1,
    int data2,
    string str,
    int line,
    int col)
  {
    return this.par.GetNewCharStyle(OldFmt, data1, data2, str, line, col);
  }

  internal ushort GetNewColor(
    ushort OldFmt,
    int data1,
    int data2,
    string str,
    int line,
    int col)
  {
    return this.fnt.GetNewColor(OldFmt, data1, data2, str, line, col);
  }

  internal ushort GetNewFieldId(ushort OldFmt, int FieldId, string FieldCode, int line, int col)
  {
    return this.fld.GetNewFieldId(OldFmt, FieldId, FieldCode, line, col);
  }

  internal int GetNewFont(
    Graphics gr,
    int OldFont,
    string NewTypeFace,
    int NewTwipsSize,
    int NewStyle,
    Color NewTextColor,
    Color NewTextBkColor,
    Color NewUlineColor,
    int NewFieldId,
    int NewAuxId,
    int NewAux1Id,
    int NewCharStyId,
    int NewParaStyId,
    int NewExpand,
    int NewTempStyle,
    int NewLang,
    string NewFieldCode,
    int NewOffset,
    byte NewCharSet,
    int NewFlags,
    int NewTextAngle)
  {
    return this.fnt.GetNewFont(gr, OldFont, NewTypeFace, NewTwipsSize, NewStyle, NewTextColor, NewTextBkColor, NewUlineColor, NewFieldId, NewAuxId, NewAux1Id, NewCharStyId, NewParaStyId, NewExpand, NewTempStyle, NewLang, NewFieldCode, NewOffset, NewCharSet, NewFlags, NewTextAngle);
  }

  internal int GetNewFont2(Graphics gr, int OldFont, tc.StrFont font)
  {
    return this.fnt.GetNewFont2(gr, OldFont, font);
  }

  internal ushort GetNewPointSize(ushort OldFmt, int data1, int data2, int line, int col)
  {
    return this.fnt.GetNewPointSize(OldFmt, data1, data2, line, col);
  }

  internal ushort GetNewStyle(
    ushort OldFmt,
    int data1,
    int data2,
    string str1,
    int line,
    int col)
  {
    return this.fnt.GetNewStyle(OldFmt, data1, data2, str1, line, col);
  }

  internal ushort GetNewTempStyle(ushort OldFmt, int data1, int data2, int line, int col)
  {
    return this.fnt.GetNewTempStyle(OldFmt, data1, data2, line, col);
  }

  internal int GetNextCellInColumn(int CurCell, bool exact)
  {
    return this.tbl.GetNextCellInColumn(CurCell, exact);
  }

  internal int GetNextCellInColumnPos(int CurCell) => this.tbl.GetNextCellInColumnPos(CurCell);

  internal int GetNextCfmt(int line, int col) => this.fnt.GetNextCfmt(line, col);

  internal int GetNumberForLevel(
    int LineNo,
    int InitListOrId,
    int level,
    int LineLevel,
    int InitFieldNbr)
  {
    return this.par.GetNumberForLevel(LineNo, InitListOrId, level, LineLevel, InitFieldNbr);
  }

  internal int GetObjSpcBef(int line, bool InScrUnits) => this.frm.GetObjSpcBef(line, InScrUnits);

  internal int GetParaFrameLine(int StartLine) => this.frm.GetParaFrameLine(StartLine);

  internal int GetParaFrameSlot() => this.frm.GetParaFrameSlot();

  internal bool GetParaRange(out int StartLine, out int EndLine)
  {
    return this.par.GetParaRange(out StartLine, out EndLine);
  }

  internal int GetPrevCellInColumn(int CurCell, bool exact, bool UseIndex)
  {
    return this.tbl.GetPrevCellInColumn(CurCell, exact, UseIndex);
  }

  internal int GetPrevCellInColumnPos(int CurCell, bool exact)
  {
    return this.tbl.GetPrevCellInColumnPos(CurCell, exact);
  }

  internal int GetPrevCfmt(int line, int col) => this.fnt.GetPrevCfmt(line, col);

  internal int GetRemainingCellSpans(int CurCell) => this.tbl.GetRemainingCellSpans(CurCell);

  internal int GetRowCell(int row, int col) => this.tbl.GetRowCell(row, col);

  internal int GetRowHeight(int lin) => this.pos.GetRowHeight(lin);

  internal int GetRowWidth(int row) => this.tbl.GetRowWidth(row);

  internal int GetRowX(int lin) => this.pos.GetRowX(lin);

  internal int GetRowY(int lin) => this.pos.GetRowY(lin);

  internal int GetSameColumnCell(int CellId, bool next) => this.tbl.GetSameColumnCell(CellId, next);

  internal int GetScrPageHt(int PageNo) => this.page.GetScrPageHt(PageNo);

  internal bool GetSectColWidthSpace(
    int TopSect,
    int CurSect,
    out int ColumnWidth,
    out int ColumnSpace,
    out int TextX,
    out int YBefHdr)
  {
    return this.sec.GetSectColWidthSpace(TopSect, CurSect, out ColumnWidth, out ColumnSpace, out TextX, out YBefHdr);
  }

  internal int GetSectDisplayLine(int FrameNo) => this.draw.GetSectDisplayLine(FrameNo);

  internal int GetSection(int lin) => this.sec.GetSection(lin);

  internal Color GetShadedColor(Color FcColor, Color BcColor, int shade)
  {
    return this.misc.GetShadedColor(FcColor, BcColor, shade);
  }

  internal int GetSpaceAdj(int line, int SpaceNo) => this.pos.GetSpaceAdj(line, SpaceNo);

  internal int GetSpannedRowHeight(int CurCell, out int pScrHeight, int PageNo)
  {
    return this.tbl.GetSpannedRowHeight(CurCell, out pScrHeight, PageNo);
  }

  internal int GetStatusLineNo() => this.draw.GetStatusLineNo();

  internal int GetStatusLineNoAdj(int FirstLine, int LastLine)
  {
    return this.draw.GetStatusLineNoAdj(FirstLine, LastLine);
  }

  internal string GetStringField(string InStr, int nbr, char delim)
  {
    return this.misc.GetStringField(InStr, nbr, delim);
  }

  internal int GetStyleIdSlot() => this.par.GetStyleIdSlot();

  internal bool GetTableMinMaxWidths(
    int FirstRow,
    out int pTblMinWidth,
    out int pTblMaxWidth,
    out int pTblWidth,
    out bool pExactWidth,
    out int[] pColMinWidth,
    out int[] pColMaxWidth,
    int EmbTblWidth)
  {
    return this.tbl.GetTableMinMaxWidths(FirstRow, out pTblMinWidth, out pTblMaxWidth, out pTblWidth, out pExactWidth, out pColMinWidth, out pColMaxWidth, EmbTblWidth);
  }

  internal int GetTableRowSlot() => this.tbl.GetTableRowSlot();

  internal bool GetTabPos(
    int ParaId,
    tc.StrTab tab,
    int CurPos,
    out int pTabPos,
    out int pTabType,
    out byte pFlags,
    bool screen)
  {
    return this.pos.GetTabPos(ParaId, tab, CurPos, out pTabPos, out pTabType, out pFlags, screen);
  }

  internal int GetTabWidth(int line, int TabNo, int CurX)
  {
    return this.pos.GetTabWidth(line, TabNo, CurX);
  }

  internal int GetTag(
    int line,
    int col,
    int type,
    out string name,
    out string AuxText,
    out int AuxInt)
  {
    return this.fnt.GetTag(line, col, type, out name, out AuxText, out AuxInt);
  }

  internal int GetTag(
    int line,
    int col,
    int type,
    out string name,
    out string AuxText,
    out int AuxInt,
    out object obj)
  {
    return this.fnt.GetTag(line, col, type, out name, out AuxText, out AuxInt, out obj);
  }

  internal int GetTagSlot() => this.fnt.GetTagSlot();

  internal int GetTblSpcBef(int LineNo, bool screen) => this.tbl.GetTblSpcBef(LineNo, screen);

  internal int[] GetTextCharWidth(int FontId, char[] ptr, int len, ushort[] pWidth)
  {
    return this.pos.GetTextCharWidth(FontId, ptr, len, pWidth);
  }

  internal int GetTextFont(int CurFont) => this.fnt.GetTextFont(CurFont);

  internal int GetTextHeight(
    char[] ptr,
    ushort[] fmt,
    int len,
    bool screen,
    out int pBaseHeight,
    out int pExtLead)
  {
    return this.pos.GetTextHeight(ptr, fmt, len, screen, out pBaseHeight, out pExtLead);
  }

  internal int GetTypeField(System.Type type, string name) => this.misc.GetTypeField(type, name);

  internal bool GetWinDimension() => this.draw.GetWinDimension();

  internal bool GetWrapCharWidth() => this.wrp.GetWrapCharWidth();

  internal bool HasSameParaBorder(int line1, int line2) => this.par.HasSameParaBorder(line1, line2);

  internal bool HasSameParaShading(int line1, int line2)
  {
    return this.par.HasSameParaShading(line1, line2);
  }

  internal bool HdrFtrExists(tc.StrHdrFtr hdr) => this.sec.HdrFtrExists(hdr);

  internal bool HiddenText(int CurFont) => this.edit.HiddenText(CurFont);

  internal bool HideControl(int ctl) => this.fld.HideControl(ctl);

  internal bool HideHiddenParaMarkers() => this.rtfr.HideHiddenParaMarkers();

  internal bool HilightTableCol(int LineNo, bool IsNew, bool repaint)
  {
    return this.tbl.HilightTableCol(LineNo, IsNew, repaint);
  }

  internal void HorScrollCheck() => this.pos.HorScrollCheck();

  internal bool HScrollAllowed() => this.pos.HScrollAllowed();

  internal bool HtmlAddOnFound() => this.misc.HtmlAddOnFound();

  internal int HtmlListLevel(int ParaId) => this.misc.HtmlListLevel(ParaId);

  internal bool HtsReadFromTer(bool FromFile, string FileName, string buf)
  {
    return this.misc.HtsReadFromTer(FromFile, FileName, buf);
  }

  internal bool HtsSaveFromTer(bool ToFile, string FileName, out string buf)
  {
    return this.misc.HtsSaveFromTer(ToFile, FileName, out buf);
  }

  internal bool InitCaret() => this.pos.InitCaret();

  internal bool InitCell(int CurCell) => this.tbl.InitCell(CurCell);

  internal bool InitConstruct() => this.init.InitConstruct();

  internal bool InitFrame(int FrameNo) => this.frm.InitFrame(FrameNo);

  internal bool InitLine(int line) => this.init.InitLine(line);

  internal bool InitPrinter() => this.prt.InitPrinter();

  internal bool InitSect(int sect) => this.sec.InitSect(sect);

  internal bool InitTerInst() => this.init.InitTerInst();

  internal bool InitTerObject(int idx) => this.fnt.InitTerObject(idx);

  internal bool InitToolbar() => this.bar.InitToolbar();

  internal bool InitToolbarVars() => this.bar.InitToolbarVars();

  internal bool InitTypeface() => this.init.InitTypeface();

  internal bool InitVariables() => this.init.InitVariables();

  internal bool InitWinProp() => this.init.InitWinProp();

  internal bool InOuterLevels(int level, int LineNo) => this.tbl.InOuterLevels(level, LineNo);

  internal bool InRotatedFrame(int x, int y, int FrameNo) => this.pos.InRotatedFrame(x, y, FrameNo);

  internal bool InSameTable(int cell1, int cell2) => this.tbl.InSameTable(cell1, cell2);

  internal bool InsertBookmark() => this.fnt.InsertBookmark();

  internal void InsertBuffer(char[] ptr, ushort[] fmt, int[] pPfmt, bool TerFormat)
  {
    this.blk.InsertBuffer(ptr, fmt, pPfmt, TerFormat);
  }

  internal void InsertBuffer(string str, ushort[] fmt, int[] pPfmt, bool TerFormat)
  {
    this.blk.InsertBuffer(str, fmt, pPfmt, TerFormat);
  }

  internal bool InsertCell(int NewCell, int CurCell, int CurRowId, char type)
  {
    return this.tbl.InsertCell(NewCell, CurCell, CurRowId, type);
  }

  internal bool InsertDynField(int FieldId, string FieldCode)
  {
    return this.fld.InsertDynField(FieldId, FieldCode);
  }

  internal bool InsertHyperlink() => this.link.InsertHyperlink();

  internal bool InsertMarkerLine(
    int LineNo,
    char BreakChar,
    int CurFont,
    int CurParaId,
    int TabwType,
    int CurCellId)
  {
    return this.edit.InsertMarkerLine(LineNo, BreakChar, CurFont, CurParaId, TabwType, CurCellId);
  }

  internal bool InvokeTextLink(bool invoke, int line, int col)
  {
    return this.link.InvokeTextLink(invoke, line, col);
  }

  internal bool IsAnchorName(string code) => this.link.IsAnchorName(code);

  internal bool IsBaselineAlignedCellLine(int line) => this.tbl.IsBaselineAlignedCellLine(line);

  internal bool IsCaretVisible(int CaretLine, int CaretCol)
  {
    return this.pos.IsCaretVisible(CaretLine, CaretCol);
  }

  internal bool IsControl(int pict) => this.fld.IsControl(pict);

  internal bool IsDefLangRtl() => this.misc.IsDefLangRtl();

  internal bool IsDynField(int FieldId) => this.fld.IsDynField(FieldId);

  internal bool IsEnglishChar(char[] ptr, int col, int len)
  {
    return this.fnt.IsEnglishChar(ptr, col, len);
  }

  internal bool IsFirstParaLine(int LineNo) => this.wrp.IsFirstParaLine(LineNo);

  internal bool IsFirstTableRow(int row) => this.tbl.IsFirstTableRow(row);

  internal bool IsFormField(int pict, int type) => this.fld.IsFormField(pict, type);

  internal bool IsFramePict(int pict) => this.frm.IsFramePict(pict);

  internal bool IsHiddenLine(int LineNo) => this.edit.IsHiddenLine(LineNo);

  internal bool IsHtmlList(int ParaId) => this.misc.IsHtmlList(ParaId);

  internal bool IsHtmlRule(int ParaId) => this.misc.IsHtmlRule(ParaId);

  internal bool IsHypertext(int CurCfmt) => this.link.IsHypertext(CurCfmt);

  internal bool IsHypertext2(int CurCfmt, bool IncludeAnchorName)
  {
    return this.link.IsHypertext2(CurCfmt, IncludeAnchorName);
  }

  internal bool IsHypertext3(int CurCfmt, bool IncludeAnchorName, bool IncludePageRef)
  {
    return this.link.IsHypertext3(CurCfmt, IncludeAnchorName, IncludePageRef);
  }

  internal bool IsLastSpannedCell(int cl) => this.tbl.IsLastSpannedCell(cl);

  internal bool IsLastTableRow(int row) => this.tbl.IsLastTableRow(row);

  internal bool IsLineRtl(int LineNo) => this.par.IsLineRtl(LineNo);

  internal bool IsLineVisible(int lin) => this.pos.IsLineVisible(lin);

  internal bool IsListLine(int LineNo) => this.par.IsListLine(LineNo);

  internal bool IsLoneHypertextChar(int LineNo, int ColNo)
  {
    return this.link.IsLoneHypertextChar(LineNo, ColNo);
  }

  internal bool IsMbcsCharSet(int CharSet, out int pCodePage)
  {
    return this.fnt.IsMbcsCharSet(CharSet, out pCodePage);
  }

  internal bool IsPageLastRow(int row, int PageNo) => this.page.IsPageLastRow(row, PageNo);

  internal bool IsParaRtl(int ParaFlow, int CellFlow, int SectFlow, int DocFlow)
  {
    return this.par.IsParaRtl(ParaFlow, CellFlow, SectFlow, DocFlow);
  }

  internal bool IsPartRow(bool top, int row, int PageNo) => this.tbl.IsPartRow(top, row, PageNo);

  internal bool IsProtected(bool msg, bool del) => this.blk.IsProtected(msg, del);

  internal bool IsProtectedChar(int line, int col) => this.edit.IsProtectedChar(line, col);

  internal bool IsSameField(int font1, int font2) => this.fld.IsSameField(font1, font2);

  internal bool IsSameFieldCode(string code1, string code2)
  {
    return this.fld.IsSameFieldCode(code1, code2);
  }

  internal bool IsSameListLevel(tc.StrListLevel pLevel1, tc.StrListLevel pLevel2)
  {
    return this.par.IsSameListLevel(pLevel1, pLevel2);
  }

  internal bool IsSameListOr(tc.StrListOr pListOr1, tc.StrListOr pListOr2)
  {
    return this.par.IsSameListOr(pListOr1, pListOr2);
  }

  internal bool IsSpannedRow(int row) => this.tbl.IsSpannedRow(row);

  internal bool IsSpanningRow(int row) => this.tbl.IsSpanningRow(row);

  internal bool IsTextPosVisible(int line, int col) => this.pos.IsTextPosVisible(line, col);

  internal bool IsTrackChangeFont(int CurCfmt) => this.trk.IsTrackChangeFont(CurCfmt);

  internal bool IsValidBookmark(string name, bool exists) => this.fnt.IsValidBookmark(name, exists);

  internal bool IsValidInputFont(ref int pPrevFont, int CurFont, int LineNo, int col)
  {
    return this.fnt.IsValidInputFont(ref pPrevFont, CurFont, LineNo, col);
  }

  internal bool JumpToPageRefBookmark(bool repaint) => this.fnt.JumpToPageRefBookmark(repaint);

  internal bool JustifySpace(int CurFont) => this.pos.JustifySpace(CurFont);

  internal bool KillHilightTimer() => this.draw.KillHilightTimer();

  internal int LastScrollBeginLine() => this.edit.LastScrollBeginLine();

  internal int LevelCell(int level, int LineNo) => this.tbl.LevelCell(level, LineNo);

  internal int LevelRow(int level, int row) => this.tbl.LevelRow(level, row);

  internal void LineAlloc(int LineNo, int OldSize, int NewSize)
  {
    this.edit.LineAlloc(LineNo, OldSize, NewSize);
  }

  internal bool LineEndsInBreak(int LineNo) => this.misc.LineEndsInBreak(LineNo);

  internal bool LinePointsToRect(int ParaFID, int x1, int y1, int x2, int y2)
  {
    return this.frm.LinePointsToRect(ParaFID, x1, y1, x2, y2);
  }

  internal bool LineRectToPoints(int ParaFID, out int x1, out int y1, out int x2, out int y2)
  {
    return this.frm.LineRectToPoints(ParaFID, out x1, out y1, out x2, out y2);
  }

  internal bool LineSelected(int LineNo) => this.edit.LineSelected(LineNo);

  internal int LineTextAngle(int LineNo) => this.frm.LineTextAngle(LineNo);

  internal int LineToUnits(int line) => this.pos.LineToUnits(line);

  internal bool LoadHtmlAddOn() => this.misc.LoadHtmlAddOn();

  internal bool LogPrintf(params object[] msg) => this.misc.LogPrintf(msg);

  internal int LwrCharWidth(int font, bool screen, char chr)
  {
    return this.fnt.LwrCharWidth(font, screen, chr);
  }

  internal bool MakeAutoNumLgl(
    out string StrOut,
    string prefix,
    int nbr,
    int FromHdng,
    int ToHdng)
  {
    return this.par.MakeAutoNumLgl(out StrOut, prefix, nbr, FromHdng, ToHdng);
  }

  internal bool MarkCells(int select) => this.tbl.MarkCells(select);

  internal bool MessagePending() => this.misc.MessagePending();

  internal int MinTableLevel(int FromLine, int ToLine) => this.tbl.MinTableLevel(FromLine, ToLine);

  internal bool MouseStopAction() => this.draw.MouseStopAction();

  internal void MoveCharInfo(int SrcLine, int SrcCol, int DestLine, int DestCol, int count)
  {
    this.edit.MoveCharInfo(SrcLine, SrcCol, DestLine, DestCol, count);
  }

  internal bool MoveCursor(int LineNo, int col) => this.edit.MoveCursor(LineNo, col);

  internal void MoveLineArrays(int StartLine, int count, char InsertDel)
  {
    this.edit.MoveLineArrays(StartLine, count, InsertDel);
  }

  internal void MoveLineData(int line, int StartPos, int count, char InsertDel)
  {
    this.edit.MoveLineData(line, StartPos, count, InsertDel);
  }

  internal int NewBltId(int old, tc.StrBlt BltRec) => this.par.NewBltId(old, BltRec);

  internal int NewParaId(
    int old,
    int LeftIndentTwips,
    int RightIndentTwips,
    int FirstIndentTwips,
    int TabId,
    int BltId,
    int AuxId,
    int Aux1Id,
    int StyId,
    int shading,
    int pflags,
    int SpaceBefore,
    int SpaceAfter,
    int SpaceBetween,
    int LineSpacing,
    Color BkColor,
    int BorderSpace,
    int flow,
    int flags)
  {
    return this.par.NewParaId(old, LeftIndentTwips, RightIndentTwips, FirstIndentTwips, TabId, BltId, AuxId, Aux1Id, StyId, shading, pflags, SpaceBefore, SpaceAfter, SpaceBetween, LineSpacing, BkColor, BorderSpace, flow, flags);
  }

  internal int NewParaId2(int old, tc.StrPfmt pNew) => this.par.NewParaId2(old, pNew);

  internal tc.StrStyleId NewStyleId() => this.par.NewStyleId();

  internal int NewTabId(int old, tc.StrTab TabRec) => this.par.NewTabId(old, TabRec);

  internal bool NextTextPos() => this.edit.NextTextPos();

  internal bool NextTextPos(ref int pLineNo, ref int pCol)
  {
    return this.edit.NextTextPos(ref pLineNo, ref pCol);
  }

  internal bool NormalizeBlock() => this.blk.NormalizeBlock();

  internal bool NormalizeForFootnote() => this.blk.NormalizeForFootnote();

  internal bool OleDragText() => this.drag.OleDragText();

  internal bool OlePostProcessing() => this.ole.OlePostProcessing();

  internal ushort[] OpenCfmt(int line) => this.fnt.OpenCfmt(line);

  internal bool OpenCharInfo(int line, out ushort[] fmt, out ushort[] cmi)
  {
    return this.fnt.OpenCharInfo(line, out fmt, out cmi);
  }

  internal ushort[] OpenCtid(int line) => this.fnt.OpenCtid(line);

  internal bool OpenCurPrinter(bool PrivatePrt) => this.prt.OpenCurPrinter(PrivatePrt);

  internal string OrdinalString(int val, bool AddSuffix, bool upper)
  {
    return this.misc.OrdinalString(val, AddSuffix, upper);
  }

  internal bool OurBitBlt(
    Graphics gr,
    int DestX,
    int DestY,
    int DestWidth,
    int DestHeight,
    Graphics SrcGr,
    int SrcX,
    int SrcY,
    int rop)
  {
    return this.draw.OurBitBlt(gr, DestX, DestY, DestWidth, DestHeight, SrcGr, SrcX, SrcY, rop);
  }

  internal Pen OurCreatePen(int TwipsThick, Color color, out int pDeviceThick)
  {
    return this.draw.OurCreatePen(TwipsThick, color, out pDeviceThick);
  }

  internal void OurDrawImage(
    Graphics gr,
    Image image,
    int DestX,
    int DestY,
    int DestWidth,
    int DestHeight)
  {
    this.draw.OurDrawImage(gr, image, DestX, DestY, DestWidth, DestHeight);
  }

  internal int OurExtTextOut(
    Graphics gr,
    int x,
    int y,
    int options,
    COp.RECT rect,
    char[] txt,
    int count,
    int[] pDX)
  {
    return this.draw.OurExtTextOut(gr, x, y, options, rect, txt, count, pDX);
  }

  internal void OurFillPolygon(Graphics gr, Brush brush, Color color, Point[] pt)
  {
    this.draw.OurFillPolygon(gr, brush, color, pt);
  }

  internal void OurFillRectangle(Graphics gr, Color BrushColor, COp.RECT rect)
  {
    this.draw.OurFillRectangle(gr, BrushColor, rect);
  }

  internal Point OurPointToClient(Point p) => this.misc.OurPointToClient(p);

  internal bool OurPrintf(params object[] msg) => this.misc.OurPrintf(msg);

  internal bool OurSetCaretPos() => this.pos.OurSetCaretPos();

  internal bool OverrideStyles() => this.init.OverrideStyles();

  internal Color PageColor() => this.page.PageColor();

  internal int PageFromLine(int LineNo, int PrevPage) => this.page.PageFromLine(LineNo, PrevPage);

  internal int PageFtrHeight(int PageNo, bool IncludeOverflow)
  {
    return this.page.PageFtrHeight(PageNo, IncludeOverflow);
  }

  internal int PageFtrSect(int PageNo, out tc.StrHdrFtr ftr)
  {
    return this.page.PageFtrSect(PageNo, out ftr);
  }

  internal int PageFtrTextHeight(int PageNo) => this.page.PageFtrTextHeight(PageNo);

  internal int PageHdrHeight(int PageNo, bool IncludeOverflow)
  {
    return this.page.PageHdrHeight(PageNo, IncludeOverflow);
  }

  internal int PageHdrHeight2(int PageNo, bool IncludeOverflow, bool inherit)
  {
    return this.page.PageHdrHeight2(PageNo, IncludeOverflow, inherit);
  }

  internal int PageHdrSect(int PageNo, out tc.StrHdrFtr hdr)
  {
    return this.page.PageHdrSect(PageNo, out hdr);
  }

  internal int PageTextWidth() => this.page.PageTextWidth();

  internal int PageToFrameY(int y) => this.frm.PageToFrameY(y);

  internal bool PaintBkPict(Graphics gr) => this.draw.PaintBkPict(gr);

  internal void PaintFrames(Graphics gr, int PaintLine) => this.draw.PaintFrames(gr, PaintLine);

  internal bool PaintPrintPreview() => this.prt.PaintPrintPreview();

  internal void PaintRows(Graphics gr, int FirstRow, int LastRow)
  {
    this.draw.PaintRows(gr, FirstRow, LastRow);
  }

  internal bool PaintTer() => this.draw.PaintTer();

  internal int ParaIdForFrame(int CurPara, int HdrFtr) => this.frm.ParaIdForFrame(CurPara, HdrFtr);

  internal int ParamIdToSID(int id) => this.misc.ParamIdToSID(id);

  internal bool PgmDown() => this.page.PgmDown();

  internal bool PgmLeft() => this.page.PgmLeft();

  internal bool PgmPageDn() => this.page.PgmPageDn();

  internal bool PgmPageHorz(int pos) => this.page.PgmPageHorz(pos);

  internal bool PgmPageLeft(bool pg) => this.page.PgmPageLeft(pg);

  internal bool PgmPageRight(bool pg) => this.page.PgmPageRight(pg);

  internal bool PgmPageUp() => this.page.PgmPageUp();

  internal bool PgmPageVert(int pos) => this.page.PgmPageVert(pos);

  internal bool PgmRight(bool HilightBegins) => this.page.PgmRight(HilightBegins);

  internal bool PgmUp() => this.page.PgmUp();

  internal bool PgmWinDown() => this.page.PgmWinDown();

  internal bool PgmWinLeft() => this.page.PgmWinLeft();

  internal bool PgmWinRight() => this.page.PgmWinRight();

  internal bool PgmWinScroll(int ScrollX, int ScrollY) => this.page.PgmWinScroll(ScrollX, ScrollY);

  internal bool PgmWinUp() => this.page.PgmWinUp();

  internal bool PosAfterHiddenText() => this.pos.PosAfterHiddenText();

  internal bool PosTag(int TagId, string name, int type, int scope, bool repaint)
  {
    return this.fnt.PosTag(TagId, name, type, scope, repaint);
  }

  internal int PosToCol(int x, int y, int line) => this.pos.PosToCol(x, y, line);

  internal bool PosWatermarkFrame(int pg) => this.page.PosWatermarkFrame(pg);

  internal bool PrepForObject() => this.misc.PrepForObject();

  internal bool PreviewDown(bool page) => this.prt.PreviewDown(page);

  internal bool PreviewLeft(bool page) => this.prt.PreviewLeft(page);

  internal bool PreviewPageHorz(int pos) => this.prt.PreviewPageHorz(pos);

  internal bool PreviewPageVert(int pos) => this.prt.PreviewPageVert(pos);

  internal bool PreviewRight(bool page) => this.prt.PreviewRight(page);

  internal bool PreviewUp(bool page) => this.prt.PreviewUp(page);

  internal bool PrevTextPos() => this.edit.PrevTextPos();

  internal bool PrevTextPos(ref int pLineNo, ref int pCol)
  {
    return this.edit.PrevTextPos(ref pLineNo, ref pCol);
  }

  internal bool PrintOneLine(
    int LineNo,
    Graphics PrtGr,
    int BeginX,
    int BeginY,
    char[] ptr,
    ushort[] fmt,
    int len,
    int height,
    int BaseHeight,
    tc.ClsTabw CurTabw,
    int CurPara,
    int SpcBef,
    int SpcAft,
    int ExtLead,
    int FrameNo,
    int LeftMargin,
    int RowBeginX,
    ushort[] pWidth,
    bool HasBullet,
    int BulletX,
    int SectLineX)
  {
    return this.prt.PrintOneLine(LineNo, PrtGr, BeginX, BeginY, ptr, fmt, len, height, BaseHeight, CurTabw, CurPara, SpcBef, SpcAft, ExtLead, FrameNo, LeftMargin, RowBeginX, pWidth, HasBullet, BulletX, SectLineX);
  }

  internal bool ProcessCommand(int cmd) => this.mnu.ProcessCommand(cmd);

  internal bool RealizeControl(int pict, Control ctl) => this.fld.RealizeControl(pict, ctl);

  internal bool RecoverCellSlots() => this.tbl.RecoverCellSlots();

  internal bool RecoverTableRowSlots() => this.tbl.RecoverTableRowSlots();

  internal bool RecreateFonts(Graphics gr) => this.fnt.RecreateFonts(gr);

  internal bool RecreateSections() => this.sec.RecreateSections();

  internal bool RedrawNonText() => this.draw.RedrawNonText();

  internal bool RefreshFrames(bool ForceRefresh) => this.frm.RefreshFrames(ForceRefresh);

  internal bool ReleaseRedo() => this.ud.ReleaseRedo();

  internal bool ReleaseUndo() => this.ud.ReleaseUndo();

  internal bool RemoveCell(int CurCell) => this.tbl.RemoveCell(CurCell);

  internal bool Repaginate(bool yield, bool selective, int LastPage, bool repaint)
  {
    return this.page.Repaginate(yield, selective, LastPage, repaint);
  }

  internal bool RepaintTer(PaintEventArgs ev) => this.draw.RepaintTer(ev);

  internal bool RepairHdrFtrDelims(int FirstLine, int LastLine)
  {
    return this.sec.RepairHdrFtrDelims(FirstLine, LastLine);
  }

  internal int RepairOneTable(int FirstLine, int level)
  {
    return this.tbl.RepairOneTable(FirstLine, level);
  }

  internal bool RepairTable() => this.tbl.RepairTable();

  internal bool ReplaceTextInPlace(ref int pLine, ref int pCol, int len, string txt)
  {
    return this.edit.ReplaceTextInPlace(ref pLine, ref pCol, len, txt);
  }

  internal bool ReplaceTextString(string replace, int StartPos, int EndPos)
  {
    return this.misc.ReplaceTextString(replace, StartPos, EndPos);
  }

  internal bool ReposPageHdrFtr(bool repaint) => this.sec.ReposPageHdrFtr(repaint);

  internal bool ReposPictFrames() => this.frm.ReposPictFrames();

  internal bool RequestPagination(bool full) => this.page.RequestPagination(full);

  internal bool ResetBufBM() => this.draw.ResetBufBM();

  internal bool ResetHdrFtr(ref tc.StrHdrFtr hdr) => this.sec.ResetHdrFtr(ref hdr);

  internal bool ResetInitVariables() => this.init.ResetInitVariables();

  internal bool ResetWheel() => this.draw.ResetWheel();

  internal string ResolveLinkFileName(string PictFile) => this.rtfr.ResolveLinkFileName(PictFile);

  internal bool RestoreWrapHilight(
    int HilightBeg,
    int HilightEnd,
    bool BegHilightAtLineEnd,
    bool EndHilightAtLineEnd,
    bool SelectAll)
  {
    return this.wrp.RestoreWrapHilight(HilightBeg, HilightEnd, BegHilightAtLineEnd, EndHilightAtLineEnd, SelectAll);
  }

  internal string romanize(int val, bool upper) => this.misc.romanize(val, upper);

  /// <summary>Преобразовать строку столбец в абсолютные координаты в строке</summary>
  /// <param name="row">Строка</param>
  /// <param name="col">Столбец</param>
  /// <param name="internalPos">Преобразовать в позицию внутреннего представления строки с учётом замен.
  /// Иначе в позицию в оригинальной строке до замен формул и спецсимволов</param>
  /// <returns></returns>
  internal int RowColToAbs(int row, int col, bool internalPos = true)
  {
    return this.pos.RowColToAbs(row, col, internalPos, false);
  }

  internal bool RtfRead(int input, string InFile, string InBuf, int BufLen)
  {
    return this.rtfr.RtfRead(input, InFile, InBuf, BufLen);
  }

  internal bool RtfWrite(int output, string OutFile, out string OutData)
  {
    return this.rtfw.RtfWrite(output, OutFile, out OutData);
  }

  internal bool RtlRect(ref COp.RECT pRect, int FrameNo, tc.StrLineSeg pSeg)
  {
    return this.pos.RtlRect(ref pRect, FrameNo, pSeg);
  }

  internal int RtlX(int x, int TextRectWidth, int FrameNo, tc.StrLineSeg pSeg)
  {
    return this.pos.RtlX(x, TextRectWidth, FrameNo, pSeg);
  }

  internal bool SaveBackground() => this.init.SaveBackground();

  internal void SaveUndo(int BegLine, int BegCol, int EndLine, int EndCol, char type)
  {
    this.ud.SaveUndo(BegLine, BegCol, EndLine, EndCol, type);
  }

  internal bool SaveWrapHilight(
    out int pHilightBeg,
    out int pHilightEnd,
    out bool pBegHilightAtLineEnd,
    out bool pEndHilightAtLineEnd,
    out bool pSelectAll)
  {
    return this.wrp.SaveWrapHilight(out pHilightBeg, out pHilightEnd, out pBegHilightAtLineEnd, out pEndHilightAtLineEnd, out pSelectAll);
  }

  internal int ScrLineHeight(int lin, bool AddSpcBef) => this.pos.ScrLineHeight(lin, AddSpcBef);

  internal bool ScrollText() => this.edit.ScrollText();

  internal bool ScrollUndo() => this.ud.ScrollUndo();

  internal bool SearchDisplay(
    string text,
    char opt,
    int StartLine,
    int StartCol,
    int EndLine,
    int EndCol)
  {
    return this.misc.SearchDisplay(text, opt, StartLine, StartCol, EndLine, EndCol);
  }

  internal bool SearchSpellTime() => this.spl.SearchSpellTime();

  internal bool SelectFirstFormField() => this.fld.SelectFirstFormField();

  internal bool SendActionMessage(int message, int wParam, int lParam)
  {
    return this.misc.SendActionMessage(message, wParam, lParam);
  }

  internal bool SendLinkMessage(bool DoubleClick, bool RightClick)
  {
    return this.link.SendLinkMessage(DoubleClick, RightClick);
  }

  internal bool SendPreprocessMessage(int message, int wParam, int lParam)
  {
    return this.misc.SendPreprocessMessage(message, wParam, lParam);
  }

  internal bool SetAnimTimer(int pict) => this.blk.SetAnimTimer(pict);

  internal bool SetAutoNumLgl(int LineNo, bool UseLogUnits)
  {
    return this.par.SetAutoNumLgl(LineNo, UseLogUnits);
  }

  internal bool SetCellLines() => this.tbl.SetCellLines();

  internal bool SetCharStyleId(
    ref tc.StrFont font,
    tc.StrStyleId PrevStyle,
    tc.StrStyleId NewStyle,
    bool force)
  {
    return this.par.SetCharStyleId(ref font, PrevStyle, NewStyle, force);
  }

  internal void SetCharWidth(int line, int col, int width)
  {
    this.fnt.SetCharWidth(line, col, width);
  }

  internal bool SetColor(Graphics gr, char area, int font) => this.draw.SetColor(gr, area, font);

  internal void SetCtid(int line, int col, int tag) => this.fnt.SetCtid(line, col, tag);

  internal bool SetCurLang(InputLanguage lng) => this.fnt.SetCurLang(lng);

  internal int SetCurLangFont(int CurFont) => this.fnt.SetCurLangFont(CurFont);

  internal int SetCurLangFont2(int CurFont, InputLanguage lng)
  {
    return this.fnt.SetCurLangFont2(CurFont, lng);
  }

  internal bool SetDlgListLevel(
    bool ListItem,
    ComboBox box,
    ComboBox ListOrBox,
    ComboBox LevelBox)
  {
    return this.par.SetDlgListLevel(ListItem, box, ListOrBox, LevelBox);
  }

  internal bool SetDlgListLevelProp(
    bool ListItem,
    ComboBox box,
    ComboBox ListOrBox,
    ComboBox LevelBox,
    CheckBox Restart,
    CheckBox Legal,
    CheckBox Reformat,
    CheckBox NoReset,
    TextBox StartAt,
    ComboBox NumTypeBox,
    ComboBox CharAftBox,
    TextBox NbrText)
  {
    return this.par.SetDlgListLevelProp(ListItem, box, ListOrBox, LevelBox, Restart, Legal, Reformat, NoReset, StartAt, NumTypeBox, CharAftBox, NbrText);
  }

  internal bool SetDlgListParaLevel(ListBox box, ComboBox LevelBox, int CurLevel)
  {
    return this.par.SetDlgListParaLevel(box, LevelBox, CurLevel);
  }

  internal bool SetDragCaret(int lParam) => this.pos.SetDragCaret(lParam);

  internal bool SetFnoteFontInfo(bool set) => this.fnt.SetFnoteFontInfo(set);

  internal bool SetFont(Graphics gr, ushort fmt, char area) => this.draw.SetFont(gr, fmt, area);

  internal int SetFontFieldId(int CurFont, int FieldId, string FieldCode)
  {
    return this.fld.SetFontFieldId(CurFont, FieldId, FieldCode);
  }

  internal int SetFontFlags(int CurFont, int flags, bool set)
  {
    return this.fnt.SetFontFlags(CurFont, flags, set);
  }

  internal int SetFontStyle(int CurFont, int style, bool set)
  {
    return this.fnt.SetFontStyle(CurFont, style, set);
  }

  internal int SetFontStyleId(int CurFont, int CharStyId, int ParaStyId)
  {
    return this.fnt.SetFontStyleId(CurFont, CharStyId, ParaStyId);
  }

  internal int SetFontTextAngle(int CurFont, int TextAngle)
  {
    return this.fnt.SetFontTextAngle(CurFont, TextAngle);
  }

  internal bool SetHdrFtrLineFlags(int LineNo, char HdrFtrChar)
  {
    return this.sec.SetHdrFtrLineFlags(LineNo, HdrFtrChar);
  }

  internal void SetLineData(
    int DestLine,
    int DestCol,
    int count,
    char[] ptr,
    ushort[] fmt,
    ushort[] ct)
  {
    this.edit.SetLineData(DestLine, DestCol, count, ptr, fmt, ct);
  }

  internal bool SetLineText(string str, int line, int col) => this.edit.SetLineText(str, line, col);

  internal bool SetListnum(int LineNo, bool UseLogUnits)
  {
    return this.par.SetListnum(LineNo, UseLogUnits);
  }

  internal bool SetMouseStopTimer(int lParam) => this.draw.SetMouseStopTimer(lParam);

  internal bool SetNextStyle() => this.par.SetNextStyle();

  internal bool SetPageFromY(int y) => this.page.SetPageFromY(y);

  internal bool SetParaBorder(int LineNo) => this.par.SetParaBorder(LineNo);

  internal int SetParaParam(int CurPara, int type, int val)
  {
    return this.par.SetParaParam(CurPara, type, val);
  }

  internal bool SetParaStyleId(
    ref tc.StrPfmt para,
    tc.StrStyleId PrevStyle,
    tc.StrStyleId NewStyle,
    bool force)
  {
    return this.par.SetParaStyleId(ref para, PrevStyle, NewStyle, force);
  }

  internal int SetParaTextFlow(int ParaId, int flow) => this.par.SetParaTextFlow(ParaId, flow);

  internal bool SetPictSize(int pict, int height, int width, bool icon)
  {
    return this.blk.SetPictSize(pict, height, width, icon);
  }

  internal int SetRowIndent(int LineNo, int row, int sect, int ColumnWidth)
  {
    return this.tbl.SetRowIndent(LineNo, row, sect, ColumnWidth);
  }

  internal int SetScapFont(int CurFont, bool set) => this.fnt.SetScapFont(CurFont, set);

  internal bool SetScrollBars() => this.pos.SetScrollBars();

  internal bool SetSectPageSize() => this.sec.SetSectPageSize();

  internal int SetSpeedKey(int idx, int cmd, Keys keys) => this.init.SetSpeedKey(idx, cmd, keys);

  internal bool SetSubtableCellWidths(int cl, int width)
  {
    return this.tbl.SetSubtableCellWidths(cl, width);
  }

  internal bool SetTableCellWidths(
    int FirstRow,
    int TblMinWidth,
    int TblMaxWidth,
    int TblWidth,
    bool ExactWidth,
    int[] ColMinWidth,
    int[] ColMaxWidth)
  {
    return this.tbl.SetTableCellWidths(FirstRow, TblMinWidth, TblMaxWidth, TblWidth, ExactWidth, ColMinWidth, ColMaxWidth);
  }

  internal int SetTag(int line, int col, int type, string name, string AuxText, int AuxInt)
  {
    return this.fnt.SetTag(line, col, type, name, AuxText, AuxInt);
  }

  internal void SetTerWindowOrg() => this.draw.SetTerWindowOrg();

  internal bool SetTextInputFieldWnd(int pict, int CurCtlWidth)
  {
    return this.fld.SetTextInputFieldWnd(pict, CurCtlWidth);
  }

  internal ushort SetTrackingFont(int CurFont, int type) => this.trk.SetTrackingFont(CurFont, type);

  internal bool SetWheelTimer(int lParam, bool reset) => this.draw.SetWheelTimer(lParam, reset);

  internal bool SetYOrigin(int line) => this.draw.SetYOrigin(line);

  internal bool ShowFrameDragObjects(int FrameNo, int ParaFID)
  {
    return this.drag.ShowFrameDragObjects(FrameNo, ParaFID);
  }

  internal bool ShowFrameMoveObjects(int FrameNo, int pict)
  {
    return this.drag.ShowFrameMoveObjects(FrameNo, pict);
  }

  internal DialogResult ShowMessage(string msg1, string msg2, MessageBoxButtons buttons)
  {
    return this.misc.ShowMessage(msg1, msg2, buttons);
  }

  internal bool ShowPictureDragObjects(int pict) => this.drag.ShowPictureDragObjects(pict);

  internal bool SpellCheckCurWordPart1(int lParam) => this.spl.SpellCheckCurWordPart1(lParam);

  internal bool SpellCheckCurWordPart2(int CmdId) => this.spl.SpellCheckCurWordPart2(CmdId);

  internal bool SplitLine(int line, int col, int extra) => this.edit.SplitLine(line, col, extra);

  internal int strcmpi(string str1, string str2) => this.misc.strcmpi(str1, str2);

  internal void StripSlashes(string InStr, out string OutStr)
  {
    this.misc.StripSlashes(InStr, out OutStr);
  }

  internal void StrPrepend(char[] str, char[] pre) => this.misc.StrPrepend(str, pre);

  internal void StrQuote(ref string str) => this.misc.StrQuote(ref str);

  internal int SumPageScrHeight(int StartPage, int count)
  {
    return this.page.SumPageScrHeight(StartPage, count);
  }

  internal bool SyncHilight() => this.draw.SyncHilight();

  internal bool TableHilighted() => this.tbl.TableHilighted();

  internal int TableLevel(int LineNo) => this.tbl.TableLevel(LineNo);

  internal bool TabOnControl(bool ShiftPressed) => this.fld.TabOnControl(ShiftPressed);

  internal bool TblHilightLeft() => this.tbl.TblHilightLeft();

  internal bool TblHilightRight(bool HilightBegins) => this.tbl.TblHilightRight(HilightBegins);

  internal bool TerAscii(char AscCode) => this.edit.TerAscii(AscCode);

  internal bool TerBackSpace() => this.edit.TerBackSpace();

  internal bool TerBackTab() => this.edit.TerBackTab();

  internal bool TerBackTabCell() => this.tbl.TerBackTabCell();

  internal bool TerBeginFile() => this.edit.TerBeginFile();

  internal bool TerBeginLine() => this.edit.TerBeginLine();

  internal bool TerColors(bool foreground) => this.fnt.TerColors(foreground);

  internal bool TerCtrlDown() => this.edit.TerCtrlDown();

  internal bool TerCtrlUp() => this.edit.TerCtrlUp();

  internal bool TerDel() => this.edit.TerDel();

  internal bool TerDeleteLine() => this.edit.TerDeleteLine();

  internal bool TerDelPrevWord() => this.edit.TerDelPrevWord();

  internal bool TerDestroyCaret() => this.pos.TerDestroyCaret();

  internal bool TerDoubleClick() => this.misc.TerDoubleClick();

  internal bool TerDown() => this.edit.TerDown();

  internal bool TerDragObject(int lParam) => this.drag.TerDragObject(lParam);

  internal bool TerDrawBorder() => this.draw.TerDrawBorder();

  internal bool TerDrawLine(
    int x1,
    int y1,
    int x2,
    int y2,
    Pen hPen,
    COp.RECT ClpRect,
    bool DoDrag)
  {
    return this.drag.TerDrawLine(x1, y1, x2, y2, hPen, ClpRect, DoDrag);
  }

  internal bool TerDrawPolygon(
    Point pt,
    int PointCount,
    Pen hPen,
    Brush hBrush,
    COp.RECT ClpRect,
    bool clip,
    bool DoDrag)
  {
    return this.drag.TerDrawPolygon(pt, PointCount, hPen, hBrush, ClpRect, clip, DoDrag);
  }

  internal bool TerDrawRect(COp.RECT rect, Pen hPen, bool clip, bool DoDrag)
  {
    return this.drag.TerDrawRect(rect, hPen, clip, DoDrag);
  }

  internal bool TerEditOle(bool edit) => this.ole.TerEditOle(edit);

  internal bool TerEndFile() => this.edit.TerEndFile();

  internal bool TerEndLine() => this.edit.TerEndLine();

  internal bool TerFonts() => this.fnt.TerFonts();

  internal bool TerGetCharWidth(
    Graphics gr,
    int NewFont,
    bool ScreenFont,
    int overhang,
    byte PitchAndFamily)
  {
    return this.fnt.TerGetCharWidth(gr, NewFont, ScreenFont, overhang, PitchAndFamily);
  }

  internal int TerGetCharWidthAlt(Graphics gr, int NewFont, char chr, int overhang)
  {
    return this.fnt.TerGetCharWidthAlt(gr, NewFont, chr, overhang);
  }

  internal int TerGetFieldFont(int font, int FieldId, string FieldCode)
  {
    return this.fld.TerGetFieldFont(font, FieldId, FieldCode);
  }

  internal bool TerInsert() => this.misc.TerInsert();

  internal bool TerInsertTab() => this.edit.TerInsertTab();

  internal bool TerJoinLine() => this.edit.TerJoinLine();

  internal bool TerJump() => this.pos.TerJump();

  internal bool TerLeft() => this.edit.TerLeft();

  internal bool TerMergeCells() => this.tbl.TerMergeCells();

  internal bool TerMousePos(int lParam, bool SetPage) => this.pos.TerMousePos(lParam, SetPage);

  internal bool TerNew(string file) => this.io.TerNew(file);

  internal bool TerNextWord() => this.edit.TerNextWord();

  internal bool TerOpen() => this.io.TerOpen();

  internal bool TerPageDn(bool keyboard) => this.edit.TerPageDn(keyboard);

  internal bool TerPageHorz(char type, int pos) => this.edit.TerPageHorz(type, pos);

  internal bool TerPageLeft(bool page) => this.edit.TerPageLeft(page);

  internal bool TerPageOptions() => this.prt.TerPageOptions();

  internal bool TerPageRight(bool page) => this.edit.TerPageRight(page);

  internal bool TerPageUp(bool keyboard) => this.edit.TerPageUp(keyboard);

  internal bool TerParaBorder() => this.par.TerParaBorder();

  internal bool TerParaSpacing() => this.par.TerParaSpacing();

  internal bool TerPasteSpecial() => this.blk.TerPasteSpecial();

  internal bool TerPictOut(
    Graphics gr,
    int CurX,
    int CurY,
    COp.RECT rect,
    int pict,
    int count,
    int LineNo)
  {
    return this.draw.TerPictOut(gr, CurX, CurY, rect, pict, count, LineNo, 0);
  }

  internal bool TerPosLine(int GotoLine) => this.pos.TerPosLine(GotoLine);

  internal bool TerPostProcessing(int message, int wParam, int lParam)
  {
    return this.misc.TerPostProcessing(message, wParam, lParam);
  }

  internal bool TerPrevWord(bool pos) => this.edit.TerPrevWord(pos);

  internal bool TerPrintOptions() => this.prt.TerPrintOptions();

  internal bool TerPrintPreviewMode(bool toolbar) => this.prt.TerPrintPreviewMode(toolbar);

  internal bool TerRead(string InputFile) => this.io.TerRead(InputFile);

  internal bool TerReplaceString() => this.misc.TerReplaceString();

  internal bool TerResetClipRgn() => this.draw.TerResetClipRgn();

  internal bool TerReturn() => this.edit.TerReturn();

  internal bool TerRight() => this.edit.TerRight();

  internal bool TerSave(string OutFile, bool ToFile) => this.io.TerSave(OutFile, ToFile);

  internal bool TerSaveAs(string OutFile) => this.io.TerSaveAs(OutFile);

  internal bool TerSearchBackward() => this.misc.TerSearchBackward();

  internal bool TerSearchForward() => this.misc.TerSearchForward();

  internal bool TerSearchString() => this.misc.TerSearchString();

  internal bool TerSetCharHilight() => this.draw.TerSetCharHilight();

  internal bool TerSetClipRgn() => this.draw.TerSetClipRgn();

  internal bool TerSetCursorShape(int lParam, bool normal)
  {
    return this.pos.TerSetCursorShape(lParam, normal);
  }

  internal bool TerSetHilight(MouseButtons button, int lParam, bool scrolling)
  {
    return this.draw.TerSetHilight(button, lParam, scrolling);
  }

  internal bool TerSplitCell() => this.tbl.TerSplitCell();

  internal bool TerSplitLine(int StartCol, bool AlignTab, bool repaint)
  {
    return this.edit.TerSplitLine(StartCol, AlignTab, repaint);
  }

  internal bool TerTabCell() => this.tbl.TerTabCell();

  internal int TerTextExtentX(Graphics gr, string str, int len)
  {
    return this.misc.TerTextExtentX(gr, str, len);
  }

  internal bool TerTextOut(
    Graphics gr,
    int x,
    int y,
    COp.RECT rect,
    char[] ptr,
    int count,
    ushort[] pWidth,
    int BaseHeight)
  {
    return this.draw.TerTextOut(gr, x, y, rect, ptr, count, pWidth, BaseHeight);
  }

  internal bool TerToggleTableGrid() => this.tbl.TerToggleTableGrid();

  internal bool TerUndo(bool DoUndo) => this.ud.TerUndo(DoUndo);

  internal bool TerUp() => this.edit.TerUp();

  internal bool TerWinDown() => this.edit.TerWinDown();

  internal bool TerWinLeft() => this.edit.TerWinLeft();

  internal bool TerWinRight() => this.edit.TerWinRight();

  internal bool TerWinUp() => this.edit.TerWinUp();

  internal int TerWrapWidth(int lin, int sect) => this.wrp.TerWrapWidth(lin, sect);

  internal int TerWrapWidth2(int lin, int sect, bool screen)
  {
    return this.wrp.TerWrapWidth2(lin, sect, screen);
  }

  internal bool TextSegmentExists(int x, int y, COp.RECT rect)
  {
    return this.draw.TextSegmentExists(x, y, rect);
  }

  internal double ToDouble(string txt) => this.misc.ToDouble(txt);

  internal double ToDouble(TextBox item) => this.misc.ToDouble(item);

  internal double ToDouble(string txt, out bool error) => this.misc.ToDouble(txt, out error);

  internal double ToDouble(TextBox item, bool DoCmToInches)
  {
    return this.misc.ToDouble(item, DoCmToInches);
  }

  internal bool ToggleEditHdrFtr() => this.sec.ToggleEditHdrFtr();

  internal bool ToggleFieldNames() => this.fld.ToggleFieldNames();

  internal bool ToggleFittedView() => this.page.ToggleFittedView();

  internal bool ToggleFootnoteEdit(bool footnote) => this.sec.ToggleFootnoteEdit(footnote);

  internal bool ToggleHiddenText() => this.misc.ToggleHiddenText();

  internal bool TogglePageBorder() => this.page.TogglePageBorder();

  internal bool TogglePageMode() => this.page.TogglePageMode();

  internal bool ToggleParaMark() => this.misc.ToggleParaMark();

  internal bool ToggleRuler() => this.misc.ToggleRuler();

  internal bool ToggleStatusRibbon() => this.misc.ToggleStatusRibbon();

  internal bool ToggleToolBar() => this.bar.ToggleToolBar();

  internal bool ToggleViewHdrFtr() => this.sec.ToggleViewHdrFtr();

  internal int ToInt(string txt) => this.misc.ToInt(txt);

  internal int ToInt(TextBox item) => this.misc.ToInt(item);

  internal int ToInt(string txt, out bool error) => this.misc.ToInt(txt, out error);

  internal bool ToInt(Form form, TextBox item, int IntVar) => this.misc.ToInt(form, item, IntVar);

  internal bool ToInt(Form form, TextBox item, out int result)
  {
    return this.misc.ToInt(form, item, out result);
  }

  internal bool TrackDel(int line, int col, bool forward) => this.trk.TrackDel(line, col, forward);

  internal bool TrackDelBlock(
    int BegLine,
    int BegCol,
    int EndLine,
    int EndCol,
    bool ResetHilight,
    bool repaint)
  {
    return this.trk.TrackDelBlock(BegLine, BegCol, EndLine, EndCol, ResetHilight, repaint);
  }

  internal bool TrackingComment(int line, int col, out string pMsg)
  {
    return this.trk.TrackingComment(line, col, out pMsg);
  }

  internal bool TransferFontId(
    bool IntoTerFont,
    int FontId,
    ref tc.StrFont font,
    ref tc.StrPrtFont pfont)
  {
    return this.fnt.TransferFontId(IntoTerFont, FontId, ref font, ref pfont);
  }

  internal bool TransferTags(int line, int col) => this.fnt.TransferTags(line, col);

  internal int UniformRowBorderCell(int CellId, bool next)
  {
    return this.tbl.UniformRowBorderCell(CellId, next);
  }

  internal int UnitsToCol(int HorUnits, int line) => this.pos.UnitsToCol(HorUnits, line);

  internal int UnitsToLine(int x, int y) => this.pos.UnitsToLine(x, y);

  internal int UnitsToLine2(int x, int y, int frm) => this.pos.UnitsToLine2(x, y, frm);

  internal bool UpdateDynField(int line, int PageNo) => this.fld.UpdateDynField(line, PageNo);

  internal bool UpdateTagTable() => this.fnt.UpdateTagTable();

  internal bool UpdateToc() => this.sec.UpdateToc();

  internal bool UpdateToolBar(bool always) => this.bar.UpdateToolBar(always);

  internal bool UseCaret() => this.pos.UseCaret();

  internal bool VerThumbPos(int wParam) => this.draw.VerThumbPos(wParam);

  internal bool WordBeingEdited() => this.spl.WordBeingEdited();

  internal bool WordWrap(int StartLine, int WrapLines) => this.wrp.WordWrap(StartLine, WrapLines);

  internal bool WrapMakeBuffer(int StartLine, int WrapLines)
  {
    return this.wrp.WrapMakeBuffer(StartLine, WrapLines);
  }

  internal bool WrapParseBuffer(int StartLine) => this.wrp.WrapParseBuffer(StartLine);

  internal bool WriteBreakLine(
    Graphics gr,
    string BreakString,
    int x,
    int y,
    int width,
    Pen hPen)
  {
    return this.draw.WriteBreakLine(gr, BreakString, x, y, width, hPen);
  }

  internal bool WriteFrameSpace(Graphics gr, int line, int y, int height)
  {
    return this.draw.WriteFrameSpace(gr, line, y, height);
  }

  internal bool WriteSpaceRect(Graphics gr, COp.RECT rect) => this.draw.WriteSpaceRect(gr, rect);

  internal bool WriteTab(
    Graphics gr,
    int line,
    int y,
    int RectY,
    int height,
    int BaseHeight,
    int width)
  {
    return this.draw.WriteTab(gr, line, y, RectY, height, BaseHeight, width);
  }

  internal int XlateCommandId(int CmdId) => this.misc.XlateCommandId(CmdId);

  internal bool XlateSizeForPrt(int FontIdx) => this.blk.XlateSizeForPrt(FontIdx);
}
