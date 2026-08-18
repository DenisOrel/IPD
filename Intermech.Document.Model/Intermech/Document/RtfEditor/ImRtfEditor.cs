// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.ImRtfEditor
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Document.UI;
using Intermech.Interfaces.Document;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

public class ImRtfEditor : Control, IEnumerable<char>, IEnumerable
{
  public bool IsBackGroundEditor;
  private bool disposed;
  internal int AllTextAngle;
  internal int AllTextAngle2;
  internal bool RotatedFrame;
  internal bool ShortRtf;
  /// <summary>Только для внутреннего пользования! Текстбокс содержит только простой текст</summary>
  internal bool IsPlaneText;
  internal bool WordWrapSuspended;
  internal Size? clientSizeBuffer;
  internal bool FullRenderMode = true;
  internal Matrix TransformMatrix;
  internal Matrix TransformMatrix2;
  internal bool BlockQuestionMarkAfterUnicode;
  internal Point MouseDownPoint = Point.Empty;
  /// <summary>Количество заблокированных для редактирования первых символов</summary>
  internal int ProtectedFirstCharCount;
  /// <summary>Количество заблокированных для редактирования первых символов с учётом замен формул</summary>
  internal int ProtectedFirstRealCharCount;
  /// <summary>Количество заблокированных для редактирования последних символов</summary>
  internal int ProtectedEndCharCount;
  /// <summary>Количество заблокированных для редактирования последних символов с учётом замен формул</summary>
  internal int ProtectedEndRealCharCount;
  internal int DistributedTextStartPos = -1;
  /// <summary>В тексте были произведены замены спецсимволов и формул</summary>
  internal bool HasTextReplaces;
  internal int OldTextLines;
  internal int OldTextHeight;
  internal int OldTextWidth;
  internal bool AbortPrint;
  internal string[] AutoCompPhrase;
  internal int AutoCompPos;
  internal string[] AutoCompWord;
  internal string[] BalloonText;
  internal CBar bar;
  internal bool BatchMode;
  internal int BeginLine;
  internal Bitmap BkPictBM;
  internal int BkPictFlag;
  internal Graphics BkPictGr;
  internal int BkPictId;
  internal CBlk blk;
  internal COp.RECT BlnRect;
  internal int BorderMarg;
  internal bool BorderShowing;
  internal int BorderSpill;
  internal int BotBorderHeight;
  internal int BoxLeft;
  internal int BoxPage;
  internal int BoxRight;
  internal char[] BreakChars;
  internal Bitmap BufBM;
  internal int BufferLength;
  internal Graphics BufGr;
  internal bool bullet;
  internal int BulletX;
  internal bool CaretEnabled;
  internal bool CaretEngaged;
  internal int CaretHeight;
  internal bool CaretHidden;
  internal int CaretPage;
  internal int CaretPos;
  internal bool CaretPositioned;
  internal bool CaretVert;
  internal tc.StrCell[] cell;
  internal tc.StrCellAux[] CellAux;
  internal char CellChar;
  internal int CfEnhMetafile;
  internal string CfmtSign;
  internal bool CharBoxLeft;
  internal bool CharBoxRight;
  public tc.StrCharTag[] CharTag;
  internal bool CharWidthWrapped;
  internal bool ClipEmbTable;
  internal tc.ClsClipInfo ClipInfo;
  internal COp.RECT ClipRect;
  internal RectangleF _grVisibleClipBounds;
  internal int ClipTblLevel;
  internal bool CloseDC;
  internal int CommandId;
  internal bool CommandSkipped;
  private System.ComponentModel.Container components;
  internal bool ContainsParaFrames;
  internal bool ContinuousScroll;
  internal bool CrLfUsed;
  internal bool CrNewLine;
  internal bool CrSplitLine;
  internal CCtl ctl;
  internal Color CurBackColor;
  internal Region CurClipRgn;
  public int CurCol;
  internal Control CurCtl;
  internal int CurCtlId;
  internal int CurDragObj;
  internal int CurFmtId;
  internal Color CurForeColor;
  internal int CurFrame;
  internal int CurHotSpot;
  internal InputLanguage CurInpLang;
  internal int CurInputField;
  public int CurLine;
  internal int CurLineY;
  internal int CurMapId;
  internal int CurMapPict;
  internal int CurMapRect;
  internal int CurObject;
  internal int CurPage;
  internal int CurPageHeight;
  internal int CurPageWidth;
  internal int CurParaFrame;
  internal int CurPfmt;
  internal PrinterSettings CurPrt;
  internal PageSettings CurPrtPage;
  internal int CurRow;
  internal int CurScrSeg;
  internal int CursDirection;
  internal int CursHorzPos;
  internal int CurSID;
  internal int CursorCell;
  internal int CurTextHeight;
  internal char CurUndoType;
  internal int CurWrapCell;
  internal int CurWrapParaFID;
  internal int CurWrapPfmt;
  internal bool[] CustomMsg;
  internal int DblClickEndCol;
  internal int DblClickEndRow;
  internal bool DblClickHilight;
  internal int DefCellMargin;
  internal InputLanguage DefInpLang;
  internal int DefLang;
  internal int DefTabType;
  internal int DefTabWidth;
  internal int DevResX;
  internal int DevResY;
  internal COp.RECT DirtyRect;
  internal int DispTextHt;
  internal bool DlgBool1;
  internal bool DlgBool2;
  internal byte[] DlgBytes;
  internal bool DlgCancel;
  internal char[] DlgChars;
  internal Color DlgColor1;
  internal Color DlgColor2;
  internal Color DlgColor3;
  internal Color DlgColor4;
  internal int DlgInt1;
  internal int DlgInt2;
  internal int DlgInt3;
  internal int DlgInt4;
  internal int DlgInt5;
  internal int DlgInt6;
  internal int DlgOffFlags;
  internal int DlgOnFlags;
  internal bool DlgPreviewDone;
  internal int DlgResult;
  internal string DlgText;
  internal string DlgText1;
  internal string DlgText2;
  internal string DlgTypeface;
  internal int DlgUint;
  internal int DlgUint1;
  internal ushort DocBegin;
  internal ushort DocCode;
  internal string DocExt;
  internal bool DocHasHeadings;
  internal bool DocHasToc;
  internal int DocHeight;
  internal string DocName;
  internal int DocTextFlow;
  internal bool DoDefWndProc;
  internal bool DoExtraPass;
  internal bool DoHyph;
  internal bool DoPostProcessing;
  internal CDrag drag;
  internal bool DraggingText;
  internal tc.StrDragObj[] DragObj;
  internal CDraw draw;
  internal bool DrawBknd;
  internal CDash dsh;
  internal CEdit edit;
  internal int EditCol;
  internal bool EditEndnoteText;
  internal bool EditFootnoteText;
  internal bool EditingParaStyle;
  internal int EditLine;
  internal bool EditPageHdrFtr;
  internal int EditWordIndex;
  internal int EditWordLen;
  internal bool EnableTransparent;
  internal bool EndnoteAtSect;
  internal int EndnoteNumFmt;
  internal bool EnterHit;
  internal int EvalTimerCount;
  internal bool ExpandLineFmt;
  internal string ExpMessage;
  internal bool ExtPrintPreview;
  internal int ExtraSpacePrtX;
  internal int ExtraSpaceScrX;
  internal int FileFormat;
  internal int FirstFramePage;
  internal int FirstFreeCellId;
  internal int FirstPage2Frame;
  internal int FirstPageHeight;
  internal bool FirstPalette;
  internal int FirstTocPos;
  internal CFld fld;
  internal char FmtSign;
  internal char FmtSignOld;
  internal CFnt fnt;
  internal int FontInUseSize;
  internal bool FontsReleased;
  internal int FootnoteNumFmt;
  internal string FootnoteRest;
  internal int FootnoteRestFont;
  internal tc.StrFrame[] frame;
  internal Color FrameBkColor;
  internal int FrameCellId;
  internal bool FrameClicked;
  internal int FrameDistFromMargin;
  internal int FrameHeight;
  internal bool FrameRectHilighted;
  internal bool FrameRefreshEnabled;
  internal int FrameRefreshLineCount;
  internal int FrameRowId;
  internal int FrameShading;
  internal bool FramesSorted;
  internal bool FrameTabsHilighted;
  internal CFrm frm;
  internal int FtrFirstPageLine;
  internal int FtrLastPageLine;
  internal bool FullCellHilight;
  internal int GlbPrtDCId;
  internal bool GoPrintPreview;
  internal bool HasNestedTables;
  internal bool HasOptionalHyph;
  internal bool HasOverlayingFrames;
  internal bool HasVarWidthFont;
  internal IntPtr hBkPictBM;
  internal IntPtr hBkPictDC;
  internal Bitmap hBlnBM;
  internal IntPtr hBufBM;
  internal IntPtr hBufDC;
  internal int HdrFirstPageLine;
  internal int HdrLastPageLine;
  internal int HiddenX;
  internal int HiddenY;
  internal bool HilightAtCurPos;
  internal int HilightBegCol;
  internal int HilightBegRow;
  internal int HilightEndCol;
  internal int HilightEndRow;
  internal bool HilightTimerOn;
  public int HilightType;
  internal bool HilightWithColCursor;
  internal tc.StrHlSeg[] HlSeg;
  internal IntPtr hMemDC;
  internal IntPtr hOldBufBM;
  internal bool HoldMessages;
  internal int HorScrollPos;
  internal int HorThumbSize;
  internal Menu hPopup;
  internal IntPtr hPrevBkPictBM;
  internal IntPtr hRulerFont;
  internal IntPtr hRulerFontBold;
  internal IntPtr hStatusFont;
  internal IntPtr hTerCurFont;
  internal IntPtr hTerWnd;
  internal bool HtmlMode;
  internal object htn;
  internal int HyphLevel;
  internal bool IgnoreMouseMove;
  internal tc.StrImageMap[] ImageMap;
  internal int ImeByteLen;
  internal int ImeCharLen;
  internal bool ImeEnabled;
  internal int ImeStartPos;
  internal int ImgDenX;
  internal int ImgDenY;
  internal bool InAccelerator;
  internal bool InAutoComp;
  internal bool InDialogBox;
  internal bool InDragDrop;
  internal bool InDrawEval;
  internal bool InFootnote;
  internal CInit init;
  internal tc.arg_list InitArg;
  internal Bitmap InitBkBM;
  internal bool InitPropMsgShown;
  internal bool InlineIme;
  internal bool InOleDrag;
  internal bool InPainting;
  internal bool InPreprocess;
  internal bool InPrinting;
  internal bool InPrintPreview;
  internal int InputFontId;
  internal bool InRtfRead;
  internal bool InsertMode;
  internal bool InUndo;
  internal CIo io;
  internal bool IsPortrait;
  internal bool JoinLines;
  internal int KnownSect;
  internal int KnownSectBegLine;
  internal int KnownSectEndLine;
  internal int LandscapeHX;
  internal int LandscapeHY;
  internal int LastBufferedLine;
  internal int LastFramePage;
  internal bool LastPageCreated;
  internal int LastWrappedLine;
  internal int LeftBorderWidth;
  internal ushort[] LineCfmt;
  internal tc.ClsLinePtr[] LinePtrCache;
  internal int LinePtrCount;
  internal char[] LineText;
  internal int LineWidth;
  internal CLink link;
  internal Color LinkColor;
  internal bool LinkCursShowing;
  internal bool LinkDblClick;
  internal string LinkPictDir;
  internal int LinkStyle;
  internal tc.StrList[] list;
  internal tc.StrListOr[] ListOr;
  internal bool MatchIds;
  internal int MaxBlts;
  internal int MaxBufferLength;
  internal int MaxCells;
  internal int MaxCharTags;
  internal int MaxColBlock;
  internal int MaxDragObjs;
  internal int MaxFonts;
  internal int MaxFrames;
  internal int MaxHlSegs;
  internal int MaxLines;
  internal int MaxLinesPerWin;
  internal int MaxListOr;
  internal int MaxLists;
  internal int MaxOpGrs;
  internal int MaxPages;
  internal int MaxParaFrames;
  internal int MaxPendPicts;
  internal int MaxPfmts;
  internal int MaxReviewers;
  internal int MaxRtfColors;
  internal int MaxScrCtls;
  internal int MaxSects;
  internal int MaxSID;
  internal int MaxSpeedKeys;
  internal int MaxTableRows;
  internal int MaxUndoLimit;
  internal int MaxUndos;
  internal bool mbcs;
  internal Control MdiChild;
  internal Control MdiClient;
  internal Graphics MemGr;
  internal int MessageDisplayed;
  internal int MessageId;
  internal bool MetaPalFound;
  internal int MinThumbHt;
  internal CMisc misc;
  internal CMnu mnu;
  internal bool ModifyProtectColor;
  internal int MouseCol;
  internal int MouseLine;
  internal bool MouseOnTextLine;
  internal char MouseOverShoot;
  internal int MouseOverShootDist;
  internal int MousePictFrame;
  internal int MouseStopCol;
  internal bool MouseStopDone;
  internal int MouseStopLine;
  internal bool MouseStopMsgOn;
  internal bool MouseStopTimerOn;
  internal int MouseStopX;
  internal int MouseStopY;
  internal int MouseX;
  internal int MouseY;
  internal bool MovingControl;
  internal COp.RECT MPRect;
  internal CMsg msg;
  internal string[] MsgString;
  internal bool MultipleToc;
  internal int NativeClipFormat;
  internal bool NetControl;
  internal int NewFrameHeight;
  internal bool NewFrameVPage;
  internal int NewFrameWidth;
  internal int NewFrameX;
  internal int NewFrameY;
  internal int NextFontAux1Id;
  internal int NextFontId;
  internal int NextParaAux1Id;
  internal int NextSpace;
  internal int NextTab;
  internal int NextX;
  internal bool NoTabIndent;
  internal bool Notified;
  internal int ObjectLinkClipFormat;
  internal char OldCellChar;
  internal char OldParaChar;
  internal COle ole;
  internal bool OnWmPaint;
  internal tc.StrOpGr[] OpGr;
  public string Name = "";
  internal bool ignoreRtfFrameSize;
  internal ContextMenu OrgContextMenu;
  internal Bitmap OrigBufBM;
  internal int OrigScrResX;
  internal int OrigScrResY;
  internal int OverhangFont;
  internal int OwnerLinkClipFormat;
  internal CPage page;
  internal Color PageBkColor;
  internal Color PageBorderColor;
  internal int PageBorderWidth;
  internal COp.RECT PageBoxRect;
  internal bool PageBoxShowing;
  internal bool PageBreakShowing;
  internal bool PageHasControls;
  internal float PageHeight;
  internal int PageHeightAdj;
  internal tc.StrPage[] PageInfo;
  internal int PageModifyCount;
  internal bool PagesShowing;
  internal float PageWidth;
  internal int PageX;
  internal int PagingMargin;
  internal int PaintBegLine;
  internal bool PaintEnabled;
  internal int PaintEndLine;
  internal int PaintFlag;
  internal int PaintFrameFlags;
  internal int PaintId;
  internal CPar par;
  internal Color ParaBackColor;
  internal Color ParaBoxColor;
  internal int ParaBoxSpace;
  internal char ParaChar;
  internal tc.StrParaFrame[] ParaFrame;
  internal bool ParaFrameSpace;
  internal int ParaShading;
  internal int ParaSpcAft;
  internal int ParaSpcBef;
  internal tc.StrTlb pBlnTlb;
  internal tc.StrPendPict[] PendPict;
  internal int[] pfmt;
  internal tc.StrPfmt[] PfmtId;
  internal byte[] pFontInUse;
  internal string PictDir;
  internal bool PictureClicked;
  internal bool PictureHilighted;
  internal ushort[] pLineCharWidth;
  internal byte[] pOleData;
  internal int PortraitHX;
  internal int PortraitHY;
  internal CPos pos;
  internal bool PosPageHdrFtr;
  internal PaperKind PprKind;
  internal int PrevCursCol;
  internal int PrevCursLine;
  internal int PrevCursLineY;
  internal int PrevCursPage;
  internal int PreviewHeight;
  internal int PreviewWidth;
  internal int PreviewZoom;
  internal tc.StrRtfParaFrameInfo PrevRtfParaFrameInfo;
  internal tc.StrStyleId PrevStyleId;
  internal int PrevTotalPages;
  internal bool PrinterAvailable;
  internal bool PrivatePrtDC;
  internal bool ProtectForm;
  internal bool ProtectionLock;
  internal CPrt prt;
  internal int PrtCurPage;
  internal Graphics PrtDataGr;
  internal bool PrtDiffRes;
  internal string[] pRtfInfo;
  internal object PrtFontLock = new object();
  internal tc.StrPrtFont[] PrtFont;
  internal Color PrtFrameBkColor;
  internal Graphics PrtGr;
  internal int PrtLeftMarg;
  internal Color PrtParaBkColor;
  internal int PrtResX;
  internal int PrtResY;
  internal Color PrtTextBkColor;
  internal int PrtTopMarg;
  internal int PrtVpX;
  internal int PrtVpY;
  internal int PrtVwExtCX;
  internal int PrtVwExtCY;
  internal int PrtVwOrgX;
  internal int PrtVwOrgY;
  internal int PrtWinExtCX;
  internal int PrtWinExtCY;
  internal int PrtWinOrgX;
  internal int PrtWinOrgY;
  internal tc.StrLineSeg[] pScrSeg;
  internal PrintPreviewDialog PvDlg;
  internal int PvExtHeight;
  internal int PvExtWidth;
  internal float PvHeight;
  internal ToolbarControl PvTlb;
  internal Size PvViewport = Size.Empty;
  internal int PvVpHeight;
  internal int PvVpWidth;
  internal float PvWidth;
  internal int PvX;
  internal int PvY;
  internal bool ReclaimResources;
  internal bool RedrawBorder;
  internal int RepageBeginLine;
  internal bool RepagePending;
  internal bool RepageTimerOn;
  internal bool repaginating;
  internal int RepaintNo;
  internal bool RepaintRuler;
  internal bool ReplaceBlock;
  internal string ReplaceString;
  internal bool ReplaceVerify;
  internal string ReplaceWith;
  internal byte ReqCharSet;
  internal int ReqLang;
  internal int ReqTwipsSize;
  internal string ReqTypeFace;
  internal tc.StrReviewer[] reviewer;
  internal int[] RowHeight;
  internal int[] RowX;
  internal int[] RowY;
  internal char[] RtfClipData;
  internal int RtfClipFormat;
  internal int RtfCurCellId;
  internal int RtfCurLevel;
  internal int RtfCurRowId;
  internal bool RtfInEquation;
  internal string[] RtfInfo;
  internal int RtfInHdrFtr;
  internal int RtfInitCellId;
  internal int RtfInitLevel;
  internal int RtfInput;
  internal bool RtfInTable;
  internal int RtfLastCellX;
  internal int RtfParaFID;
  internal tc.StrRtfParaFrameInfo RtfParaFrameInfo;
  internal int RtfPrevLevel;
  internal CRtfr rtfr;
  internal CRtfw rtfw;
  internal bool RulerClicked;
  internal Font RulerFont;
  internal Font RulerFontBold;
  internal int RulerFontHeight;
  internal int RulerLineX;
  internal bool RulerPending;
  internal COp.RECT RulerRect;
  internal int RulerSection;
  internal int SavePrtFontHeight;
  internal bool SavePrtIsTrueType;
  internal byte SavePrtPitchFamily;
  internal int SavePvFirstFramePage;
  internal bool SavePvFittedView;
  internal int SavePvLastFramePage;
  internal int SavePvOrgX;
  internal int SavePvOrgY;
  internal bool SavePvPageBorder;
  internal bool SavePvPageMode;
  internal bool SavePvPrintView;
  internal bool SavePvRuler;
  internal int SavePvScrResX;
  internal int SavePvScrResY;
  internal bool SavePvShowHorBar;
  internal bool SavePvStatus;
  internal bool SavePvToolbar;
  internal int SavePvToolBarHeight;
  internal bool SavePvViewPageHdrFtr;
  internal bool SavePvWordWrap;
  internal tc.StrScrCtl[] ScrCtl;
  internal int ScrFrameAngle;
  internal Bitmap ScrollBM;
  internal int ScrResX;
  internal int ScrResY;
  internal bool ScrRtl;
  internal int ScrYOffset;
  internal char SearchDirection;
  internal int SearchFlags;
  internal string SearchString;
  internal CSec sec;
  internal int SectLine;
  internal bool SectModified;
  internal bool SendActionMsg;
  internal bool ShadeBegin;
  internal bool ShadeEnd;
  internal bool ShowFieldNames;
  internal bool ShowHiddenText;
  internal bool ShowHyperlinkCursor;
  internal bool ShowPageBorder;
  internal bool ShowPageBreak;
  internal bool ShowParaMark;
  internal bool ShowProtectCaret;
  internal bool ShowPvToolbar;
  internal bool ShowTableGridLines;
  internal bool SkipCommand;
  internal bool SnapToGrid;
  internal int SpaceAft;
  internal int SpaceBef;
  internal Keys[] SpeedKey;
  internal int[] SpeedKeyCmd;
  internal bool[] SpeedKeyEnabled;
  internal bool SpellCheckerPopped;
  internal int SpellCol;
  internal int SpellLine;
  internal bool SpellPending;
  internal CSpl spl;
  internal int SSClipInfo;
  internal object st;
  internal int StatusBarHeight;
  internal Color StatusBkColor;
  internal Color StatusColor;
  internal int StatusColPos;
  internal Font StatusFont;
  internal int StatusFontHeight;
  internal int StatusLinePos;
  internal int StatusLineWidth;
  internal int StatusRowPos;
  internal string StDictDir;
  internal bool StretchHilight;
  internal tc.StrStyleId[] StyleId;
  internal bool TabAlign;
  internal byte TabFlags;
  internal tc.StrTableAux[] TableAux;
  internal int TableCols;
  internal tc.StrTableRow[] TableRow;
  internal int TableRows;
  internal int TabPrevAdj;
  internal int TabPrevCol;
  internal int TabPrevLine;
  internal int TabWidth;
  internal bool TagsWrapped;
  internal CTbl tbl;
  internal bool TblSelCursShowing;
  internal ushort[] TempCfmt;
  internal string TempString;
  internal string TempString1;
  internal tc.arg_list TerArg;
  internal tc.StrBlt[] TerBlt;
  internal string TerClassName;
  internal Font TerCurFont;
  internal int TerFlags;
  internal int TerFlags2;
  internal int TerFlags3;
  internal int TerFlags4;
  internal int TerFlags5;
  internal int TerFlags6;
  internal object TerFontLock = new object();
  internal tc.StrFont[] TerFont;
  internal Graphics TerGr;
  internal string TerHelpFile;
  internal bool TerHelpWanted;
  internal string TerLastDebugMsg;
  internal int TerLastMsg;
  internal int TerOpFlags;
  internal int TerOpFlags2;
  internal COp.RECT TerRect;
  internal Font TerRegFont;
  internal tc.StrSect[] TerSect;
  internal tc.StrSect1[] TerSect1;
  internal tc.StrTab[] TerTab;
  internal COp.TEXTMETRIC TerTextMet;
  internal ToolbarControl TerTlb;
  internal int TerWinHeight;
  internal int TerWinOrgX;
  internal int TerWinOrgY;
  internal COp.RECT TerWinRect;
  internal int TerWinWidth;
  internal tc.ClsLinePtr[] text;
  internal int TextBorder;
  internal Color TextBorderColor;
  internal Color TextDefBkColor;
  internal Color TextDefColor;
  internal bool TextDragged;
  internal tc.StrTlbCustId[] TlbCustId;
  internal tc.StrTlb[][] TlbId;
  internal int TlbIdClicked;
  internal int[][] TlbItem;
  internal int[] TlbItemCount;
  internal bool[][] TlbItemHide;
  internal bool TocShowPageNo;
  internal int TocTabAlign;
  internal int TocTabLeader;
  internal Brush ToolbarBrush;
  internal int ToolBarCfmt;
  internal bool ToolBarFillStyles;
  internal string ToolbarFlagText;
  internal int ToolBarHeight;
  internal int ToolBarPfmt;
  internal int ToolBarSID;
  internal int TopBorderHeight;
  internal int TotalAutoComps;
  internal int TotalBlts;
  internal int TotalCells;
  internal int TotalCharTags;
  internal int TotalDragObjs;
  internal int TotalFonts;
  internal int TotalFrames;
  internal int TotalHlSegs;
  internal int TotalImageMaps;
  private int totalLines;
  internal int TotalListOr;
  internal int TotalLists;
  internal int TotalOpGrs;
  internal int TotalPages;
  internal int TotalParaFrames;
  internal int TotalPendPicts;
  internal int TotalPfmts;
  internal int TotalPointSizes;
  internal int TotalPreviewPages;
  internal int TotalReviewers;
  internal int TotalScrCtls;
  internal int TotalScrSeg;
  internal int TotalSects;
  internal int TotalSID;
  internal int TotalSpeedKeys;
  internal int TotalTableRows;
  internal int TotalTabs;
  internal int TotalTlbCustIds;
  internal int TotalTypes;
  internal bool TrackChanges;
  internal int TrackRev;
  internal tc.ClsDateTime TrackTime;
  internal bool transparent;
  internal CTrk trk;
  internal int TsFrameY;
  internal CUndo ud;
  internal tc.StrUndo[] undo;
  internal int UndoCount;
  internal int UndoInt1;
  internal int UndoInt2;
  internal tc.StrParaFrame UndoParaFrame;
  internal int UndoRef;
  internal int UndoSkipRef;
  internal int UndoTblSize;
  internal int UnitResX;
  internal int UnitResY;
  internal string UserDir;
  internal int UserFileType;
  internal bool UseTextMap;
  internal bool UseWin;
  internal bool UsingZoomFonts;
  internal bool VbxMessageProcessed;
  internal int VerScrollPos;
  internal int VerThumbSize;
  internal bool VerySmallMovement;
  internal int ViewKind;
  internal bool ViewPageHdrFtr;
  internal Cursor WaitCursor;
  internal bool WaitForOle;
  internal string WebFolder;
  internal Cursor WheelCur;
  internal bool WheelShowing;
  internal bool WheelTimerDownOn;
  internal bool WheelTimerUpOn;
  internal int WheelX;
  internal int WheelY;
  internal bool WindowBeingCreated;
  internal bool WindowDestroyed;
  internal int WinHeight;
  internal int WinWidth;
  internal int WinYOffset;
  internal int WinYOffsetLine;
  internal ImageAttributes WmImageAttr;
  internal int WmParaFID;
  internal bool WmWashed;
  internal char[] wrap;
  internal int WrapAddLines;
  internal int WrapBufferSize;
  internal ushort[] WrapCfmt;
  internal ushort[] WrapCharWidth;
  internal byte[] WrapCharWidthClass;
  internal int[] WrapCharWidthDX;
  internal int[] WrapCharWidthCP;
  internal int[] WrapCharWidthOrder;
  internal ushort[] WrapCharWidthText;
  internal ushort[] WrapCtid;
  internal int WrapFlag;
  internal bool WrapHasUniChar;
  internal int WrapMaxLines;
  internal int WrapParaFont;
  internal bool WrapPending;
  internal int WrapSect;
  internal bool WrapSpellChecked;
  internal int WrapTextFlow;
  internal int WrapWidthChars;
  internal int WrapWidthTwips;
  internal CWrap wrp;
  internal int ZoomPercent;

  internal event ImRtfEditor.EventAction Action;

  internal event ImRtfEditor.EventClosing Closing;

  internal event ImRtfEditor.EventHypertext Hypertext;

  internal event ImRtfEditor.EventAction IEAction;

  internal event ImRtfEditor.EventClosing IEClosing;

  internal event ImRtfEditor.EventHypertext IEHypertext;

  internal event ImRtfEditor.EventMergeData IEMergeData;

  internal event ImRtfEditor.EventModified IEModified;

  internal event ImRtfEditor.EventPageCount IEPageCount;

  internal event ImRtfEditor.EventPageSizeChanging IEPageSizeChanging;

  internal event ImRtfEditor.EventPostPaint IEPostPaint;

  internal event ImRtfEditor.EventPreprocess IEPreprocess;

  internal event ImRtfEditor.EventSpellWordReplaced IESpellWordReplaced;

  internal event ImRtfEditor.EventUpdateStatusbar IEUpdateStatusbar;

  internal event ImRtfEditor.EventUpdateToolbar IEUpdateToolbar;

  internal event ImRtfEditor.EventMergeData MergeData;

  internal event ImRtfEditor.EventModified Modified;

  internal event ImRtfEditor.EventPageCount PageCount;

  internal event ImRtfEditor.EventPageSizeChanging PageSizeChanging;

  internal event ImRtfEditor.EventPostPaint PostPaint;

  internal event ImRtfEditor.EventPreprocess Preprocess;

  internal event ImRtfEditor.EventSpellWordReplaced SpellWordReplaced;

  internal event ImRtfEditor.EventUpdateStatusbar UpdateStatusbar;

  internal event ImRtfEditor.EventUpdateToolbar UpdateToolbar;

  public ImRtfEditor()
  {
    this.components = (System.ComponentModel.Container) null;
    this.msg = (CMsg) null;
    this.draw = (CDraw) null;
    this.init = (CInit) null;
    this.fnt = (CFnt) null;
    this.misc = (CMisc) null;
    this.edit = (CEdit) null;
    this.prt = (CPrt) null;
    this.blk = (CBlk) null;
    this.page = (CPage) null;
    this.sec = (CSec) null;
    this.frm = (CFrm) null;
    this.link = (CLink) null;
    this.trk = (CTrk) null;
    this.dsh = (CDash) null;
    this.fld = (CFld) null;
    this.ud = (CUndo) null;
    this.tbl = (CTbl) null;
    this.pos = (CPos) null;
    this.wrp = (CWrap) null;
    this.drag = (CDrag) null;
    this.par = (CPar) null;
    this.spl = (CSpl) null;
    this.ole = (COle) null;
    this.bar = (CBar) null;
    this.ctl = (CCtl) null;
    this.rtfr = (CRtfr) null;
    this.rtfw = (CRtfw) null;
    this.io = (CIo) null;
    this.mnu = (CMnu) null;
    this.LinePtrCache = new tc.ClsLinePtr[151];
    this.SpellCheckerPopped = false;
    this.HasOptionalHyph = true;
    this.InitPropMsgShown = false;
    this.EnterHit = false;
    this.UseWin = true;
    this.InAutoComp = false;
    this.TotalAutoComps = 0;
    this.AutoCompWord = new string[101];
    this.AutoCompPhrase = new string[101];
    this.TotalPendPicts = 0;
    this.MaxPendPicts = 10;
    this.LinkPictDir = "";
    this.TotalHlSegs = 0;
    this.MaxHlSegs = 10;
    this.BalloonText = new string[34];
    this.TlbItem = new int[3][];
    this.TlbItemHide = new bool[3][];
    this.TotalScrCtls = 0;
    this.MaxScrCtls = 10;
    this.TlbItemCount = new int[2];
    this.TotalTlbCustIds = 0;
    this.BreakChars = new char[20];
    this.SpeedKeyEnabled = new bool[300];
    this.SpeedKey = new Keys[300];
    this.SpeedKeyCmd = new int[300];
    this.MaxSpeedKeys = 300;
    this.ProtectForm = false;
    this.RtfInfo = new string[11];
    this.pRtfInfo = new string[11];
    this.StDictDir = "";
    this.MsgString = new string[250];
    this.CustomMsg = new bool[250];
    this.TerLastMsg = 0;
    this.TerLastDebugMsg = "";
    this.PictDir = "";
    this.OrgContextMenu = (ContextMenu) null;
    this.WebFolder = "";
    this.msg = new CMsg(this);
    this.draw = new CDraw(this);
    this.init = new CInit(this);
    this.fnt = new CFnt(this);
    this.misc = new CMisc(this);
    this.edit = new CEdit(this);
    this.prt = new CPrt(this);
    this.blk = new CBlk(this);
    this.page = new CPage(this);
    this.sec = new CSec(this);
    this.frm = new CFrm(this);
    this.link = new CLink(this);
    this.trk = new CTrk(this);
    this.dsh = new CDash(this);
    this.fld = new CFld(this);
    this.ud = new CUndo(this);
    this.tbl = new CTbl(this);
    this.pos = new CPos(this);
    this.wrp = new CWrap(this);
    this.drag = new CDrag(this);
    this.par = new CPar(this);
    this.spl = new CSpl(this);
    this.ole = new COle(this);
    this.bar = new CBar(this);
    this.ctl = new CCtl(this);
    this.rtfr = new CRtfr(this);
    this.rtfw = new CRtfw(this);
    this.io = new CIo(this);
    this.mnu = new CMnu(this);
    this.draw.SetRef();
    this.init.SetRef();
    this.fnt.SetRef();
    this.misc.SetRef();
    this.edit.SetRef();
    this.prt.SetRef();
    this.blk.SetRef();
    this.page.SetRef();
    this.sec.SetRef();
    this.frm.SetRef();
    this.link.SetRef();
    this.trk.SetRef();
    this.dsh.SetRef();
    this.fld.SetRef();
    this.ud.SetRef();
    this.tbl.SetRef();
    this.pos.SetRef();
    this.wrp.SetRef();
    this.drag.SetRef();
    this.par.SetRef();
    this.spl.SetRef();
    this.ole.SetRef();
    this.bar.SetRef();
    this.ctl.SetRef();
    this.rtfr.SetRef();
    this.rtfw.SetRef();
    this.io.SetRef();
    this.mnu.SetRef();
    this.init.InitConstruct();
    this.TerArg.open = false;
    ++tc.TerOpenCount;
    this.InitializeComponent();
  }

  /// <summary>ADDED Сбрасывание настроек для повторной печати</summary>
  public void Reset()
  {
    this.ExtPrintPreview = false;
    this.PvExtWidth = 0;
    this.PvExtHeight = 0;
    this.PvVpWidth = 0;
    this.PvVpHeight = 0;
    this.PvX = 0;
    this.PvY = 0;
    this.TerWinOrgX = 0;
    this.TerWinOrgY = 0;
  }

  internal MainMenu BuildMenu() => this.mnu.BuildMenu();

  internal bool ClearAllTabs(bool repaint) => this.par.ClearAllTabs(repaint);

  internal bool ClearTab(int pos, bool repaint) => this.par.ClearTab(pos, repaint);

  internal bool CloseTer(bool ForcedClose) => this.init.CloseTer(ForcedClose);

  internal bool DeselectTerText(bool repaint) => this.draw.DeselectTerText(repaint);

  private void Dispose2(bool disposing)
  {
    if (this.disposed)
      return;
    this.disposed = true;
    this.TerClassName = this.misc.GetClassName(this.hTerWnd);
    if (this.hTerWnd != IntPtr.Zero)
    {
      --tc.TerOpenCount;
      this.CloseTer(true);
    }
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
    if (!tc.DoClassCleanup)
      return;
    this.TerClassCleanup();
  }

  protected override void Dispose(bool disposing) => this.Dispose2(disposing);

  internal void FireAction(object Sender, int ActionType, int ActionId)
  {
    ImRtfEditor.EventAction action = this.Action;
    if (action != null)
      action(Sender, ActionType, ActionId);
    ImRtfEditor.EventAction ieAction = this.IEAction;
    if (ieAction == null)
      return;
    ieAction(Sender, ActionType, ActionId);
  }

  internal void FireClosing(object Sender)
  {
    ImRtfEditor.EventClosing closing = this.Closing;
    if (closing != null)
      closing(Sender);
    ImRtfEditor.EventClosing ieClosing = this.IEClosing;
    if (ieClosing == null)
      return;
    ieClosing(Sender);
  }

  internal bool FireMergeData(object Sender, string name, out string data)
  {
    data = "";
    ImRtfEditor.EventMergeData mergeData = this.MergeData;
    if (mergeData != null)
      return mergeData(Sender, name, out data);
    ImRtfEditor.EventMergeData ieMergeData = this.IEMergeData;
    return ieMergeData != null && ieMergeData(Sender, name, out data);
  }

  public void FireModified(object Sender)
  {
    ImRtfEditor.EventModified modified = this.Modified;
    if (modified != null)
      modified(Sender);
    ImRtfEditor.EventModified ieModified = this.IEModified;
    if (ieModified == null)
      return;
    ieModified(Sender);
  }

  internal void FirePageCount(object Sender)
  {
    ImRtfEditor.EventPageCount pageCount = this.PageCount;
    if (pageCount != null)
      pageCount(Sender);
    ImRtfEditor.EventPageCount iePageCount = this.IEPageCount;
    if (iePageCount == null)
      return;
    iePageCount(Sender);
  }

  internal void FirePageSizeChanging(object Sender, ref int NewPageSize)
  {
    ImRtfEditor.EventPageSizeChanging pageSizeChanging1 = this.PageSizeChanging;
    if (pageSizeChanging1 != null)
      pageSizeChanging1(Sender, ref NewPageSize);
    ImRtfEditor.EventPageSizeChanging pageSizeChanging2 = this.IEPageSizeChanging;
    if (pageSizeChanging2 == null)
      return;
    pageSizeChanging2(Sender, ref NewPageSize);
  }

  internal void FirePostPaint(object Sender, Graphics gr)
  {
    ImRtfEditor.EventPostPaint postPaint = this.PostPaint;
    if (postPaint != null)
      postPaint(Sender, gr);
    ImRtfEditor.EventPostPaint iePostPaint = this.IEPostPaint;
    if (iePostPaint == null)
      return;
    iePostPaint(Sender, gr);
  }

  internal void FirePreprocess(object Sender, int ActionType, int ActionId)
  {
    ImRtfEditor.EventPreprocess preprocess = this.Preprocess;
    if (preprocess != null)
      preprocess(Sender, ActionType, ActionId);
    ImRtfEditor.EventPreprocess iePreprocess = this.IEPreprocess;
    if (iePreprocess == null)
      return;
    iePreprocess(Sender, ActionType, ActionId);
  }

  internal void FireSpellWordReplaced(object Sender, int CharPos, string PrevWord, string NewWord)
  {
    ImRtfEditor.EventSpellWordReplaced spellWordReplaced1 = this.SpellWordReplaced;
    if (spellWordReplaced1 != null)
      spellWordReplaced1(Sender, CharPos, PrevWord, NewWord);
    ImRtfEditor.EventSpellWordReplaced spellWordReplaced2 = this.IESpellWordReplaced;
    if (spellWordReplaced2 == null)
      return;
    spellWordReplaced2(Sender, CharPos, PrevWord, NewWord);
  }

  internal void FireUpdateStatusbar(object Sender)
  {
    ImRtfEditor.EventUpdateStatusbar updateStatusbar = this.UpdateStatusbar;
    if (updateStatusbar != null)
      updateStatusbar(Sender);
    ImRtfEditor.EventUpdateStatusbar ieUpdateStatusbar = this.IEUpdateStatusbar;
    if (ieUpdateStatusbar == null)
      return;
    ieUpdateStatusbar(Sender);
  }

  internal void FireUpdateToolbar(object Sender)
  {
    ImRtfEditor.EventUpdateToolbar updateToolbar = this.UpdateToolbar;
    if (updateToolbar != null)
      updateToolbar(Sender);
    ImRtfEditor.EventUpdateToolbar ieUpdateToolbar = this.IEUpdateToolbar;
    if (ieUpdateToolbar == null)
      return;
    ieUpdateToolbar(Sender);
  }

  internal bool GetFontInfo(int FontId, out string TypeFace, out int PointSize, out int style)
  {
    return this.fnt.GetFontInfo(FontId, out TypeFace, out PointSize, out style);
  }

  internal bool GetFontInfo2(int FontId, out string TypeFace, out int TwipsSize, out int style)
  {
    return this.fnt.GetFontInfo2(FontId, out TypeFace, out TwipsSize, out style);
  }

  internal string GetTerBuffer() => this.io.GetTerBuffer();

  public bool GetTerCursorPos(out int CursLine, ref int CursCol)
  {
    return this.pos.GetTerCursorPos(out CursLine, ref CursCol);
  }

  internal bool GetTerFields(out tc.StrTerField fld) => this.misc.GetTerFields(out fld);

  internal bool GetTerFieldsAlt(out tc.StrTerField fld, int LineNo)
  {
    return this.misc.GetTerFieldsAlt(out fld, LineNo);
  }

  internal bool InDesignMode() => this.DesignMode;

  private void InitializeComponent()
  {
    this.Name = nameof (ImRtfEditor);
    this.Size = new Size(328, 288);
  }

  internal bool InsertRtfBuf(string buf, int line, int col, bool repaint)
  {
    return this.rtfr.InsertRtfBuf(buf, buf.Length, line, col, repaint);
  }

  public bool InsertTerText(string text, bool repaint) => this.blk.InsertTerText(text, repaint);

  public bool InsertTerText(string text, bool repaint, bool reWrap)
  {
    return this.blk.InsertTerText(text, repaint, reWrap);
  }

  public bool InsertTerText(string text, string font, bool repaint)
  {
    bool flag = true;
    if (this.HilightType == 1 || this.HilightType == 2)
      flag = this.TerDeleteBlock(false);
    if (!flag)
      return false;
    this.SetTerFont(font, false);
    return this.blk.InsertTerText(text, repaint);
  }

  internal void InvertLine(int x1, int y1, int x2, int y2)
  {
    bool style = this.GetStyle(ControlStyles.UserPaint);
    if (this.InDialogBox)
      return;
    Point p1 = new Point(x1, y1);
    Point p2 = new Point(x2, y2);
    Point point1 = this.misc.OurPointToClient(p1);
    Point point2 = this.misc.OurPointToClient(p2);
    int num = this.TerArg.ruler ? this.RulerRect.top : this.TerWinRect.top;
    if (point1.Y < num && point2.Y < num || point1.Y > this.TerWinRect.bottom && point2.Y > this.TerWinRect.bottom || point1.X < this.TerWinRect.left && point2.X < this.TerWinRect.left || point1.X > this.TerWinRect.right && point2.X > this.TerWinRect.right)
      return;
    if (point1.Y < num)
      point1.Y = num;
    if (point2.Y < num)
      point2.Y = num;
    if (point1.Y > this.TerWinRect.bottom)
      point1.Y = this.TerWinRect.bottom;
    if (point2.Y > this.TerWinRect.bottom)
      point2.Y = this.TerWinRect.bottom;
    if (point1.X < this.TerWinRect.left)
      point1.X = this.TerWinRect.left;
    if (point2.X < this.TerWinRect.left)
      point2.X = this.TerWinRect.left;
    if (point1.X > this.TerWinRect.right)
      point1.X = this.TerWinRect.right;
    if (point2.X > this.TerWinRect.right)
      point2.X = this.TerWinRect.right;
    if (!style)
      this.SetStyle(ControlStyles.UserPaint, true);
    Graphics terGr = this.TerGr;
    Color color = ImRtfEditor.InvertColor(this.PageBkColor);
    if (terGr != null)
    {
      terGr.DrawLine(new Pen(color), point1, point2);
    }
    else
    {
      point1 = this.PointToScreen(point1);
      point2 = this.PointToScreen(point2);
      ControlPaint.DrawReversibleLine(point1, point2, Color.White);
    }
    if (style)
      return;
    this.SetStyle(ControlStyles.UserPaint, false);
  }

  public static Color InvertColor(Color c)
  {
    return Color.FromArgb((int) c.A, (int) byte.MaxValue - (int) c.R, (int) byte.MaxValue - (int) c.G, (int) byte.MaxValue - (int) c.B);
  }

  internal void InvertRectangle(COp.RECT rect)
  {
    bool style = this.GetStyle(ControlStyles.UserPaint);
    if (this.InDialogBox)
      return;
    if (!style)
      this.SetStyle(ControlStyles.UserPaint, true);
    Point p = new Point(rect.left, rect.top);
    Point point = new Point(rect.right, rect.bottom);
    Point client = this.misc.OurPointToClient(p);
    point = this.misc.OurPointToClient(point);
    Rectangle rectangle = this.ctl.ToRectangle(this.TerWinRect);
    if (!rectangle.Contains(client) || !rectangle.Contains(point))
      return;
    Graphics terGr = this.TerGr;
    Color color = ImRtfEditor.InvertColor(this.PageBkColor);
    if (terGr != null)
    {
      using (SolidBrush solidBrush = new SolidBrush(color))
        terGr.FillRectangle((Brush) solidBrush, new Rectangle(client.X, client.Y, point.X - client.X, point.Y - client.Y));
    }
    else
    {
      Point screen = this.PointToScreen(client);
      point = this.PointToScreen(point);
      ControlPaint.FillReversibleRectangle(new Rectangle(screen.X, screen.Y, point.X - screen.X, point.Y - screen.Y), Color.White);
    }
    if (style)
      return;
    this.SetStyle(ControlStyles.UserPaint, false);
  }

  protected override bool IsInputChar(char chr) => true;

  protected override void OnCreateControl()
  {
    if ((int) this.hTerWnd != 0 && this.hTerWnd != this.Handle)
      this.init.ReInitWinProp(this.Handle);
    else
      this.init.InitTerInst();
    base.OnCreateControl();
  }

  internal event PreprocessEventHandler PreprocessClick;

  protected override void OnClick(EventArgs e)
  {
    PreprocessEventHandler preprocessClick = this.PreprocessClick;
    if (preprocessClick != null)
    {
      CancelEventArgs cancelEventArgs = new CancelEventArgs();
      preprocessClick((object) this, e, cancelEventArgs);
      if (cancelEventArgs.Cancel)
        return;
    }
    base.OnClick(e);
  }

  internal event PreprocessEventHandler PreprocessDoubleClick;

  protected override void OnDoubleClick(EventArgs ev)
  {
    PreprocessEventHandler preprocessDoubleClick = this.PreprocessDoubleClick;
    if (preprocessDoubleClick != null)
    {
      CancelEventArgs cancelEventArgs = new CancelEventArgs();
      preprocessDoubleClick((object) this, ev, cancelEventArgs);
      if (cancelEventArgs.Cancel)
        return;
    }
    this.ctl.OnDoubleClick(ev);
    base.OnDoubleClick(ev);
  }

  protected override void OnDragDrop(DragEventArgs ev)
  {
    if (!this.TerArg.ReadOnly && (this.TerFlags5 & 1) == 0)
      this.ctl.OnDragDrop(ev);
    base.OnDragDrop(ev);
  }

  protected override void OnDragEnter(DragEventArgs ev)
  {
    this.ctl.OnDragEnter(ev);
    base.OnDragEnter(ev);
  }

  protected override void OnDragLeave(EventArgs ev)
  {
    this.ctl.OnDragLeave(ev);
    base.OnDragLeave(ev);
  }

  protected override void OnDragOver(DragEventArgs ev)
  {
    this.ctl.OnDragOver(ev);
    base.OnDragOver(ev);
  }

  protected override void OnGotFocus(EventArgs ev)
  {
    this.ctl.OnGotFocus(ev);
    base.OnGotFocus(ev);
  }

  internal event PreprocessKeyEventHandler PreprocessKeyDown;

  protected override void OnKeyDown(KeyEventArgs e)
  {
    PreprocessKeyEventHandler preprocessKeyDown = this.PreprocessKeyDown;
    if (preprocessKeyDown != null)
    {
      CancelEventArgs cancelEventArgs = new CancelEventArgs();
      preprocessKeyDown((object) this, e, cancelEventArgs);
      if (cancelEventArgs.Cancel)
        return;
    }
    base.OnKeyDown(e);
  }

  internal event PreprocessKeyEventHandler PreprocessKeyUp;

  protected override void OnKeyUp(KeyEventArgs e)
  {
    PreprocessKeyEventHandler preprocessKeyUp = this.PreprocessKeyUp;
    if (preprocessKeyUp != null)
    {
      CancelEventArgs cancelEventArgs = new CancelEventArgs();
      preprocessKeyUp((object) this, e, cancelEventArgs);
      if (cancelEventArgs.Cancel)
        return;
    }
    base.OnKeyUp(e);
  }

  public event PreprocessKeyPressEventHandler PreprocessKeyPress;

  protected override void OnKeyPress(KeyPressEventArgs ev)
  {
    PreprocessKeyPressEventHandler preprocessKeyPress = this.PreprocessKeyPress;
    if (preprocessKeyPress != null)
    {
      CancelEventArgs cancelEventArgs = new CancelEventArgs();
      preprocessKeyPress((object) this, ev, cancelEventArgs);
      if (cancelEventArgs.Cancel)
        return;
    }
    this.ctl.OnKeyPress(ev);
    base.OnKeyPress(ev);
  }

  protected override void OnLostFocus(EventArgs ev)
  {
    this.ctl.OnLostFocus(ev);
    base.OnLostFocus(ev);
  }

  internal event PreprocessEventHandler PreprocessEnter;

  protected override void OnEnter(EventArgs e)
  {
    PreprocessEventHandler preprocessEnter = this.PreprocessEnter;
    if (preprocessEnter != null)
    {
      CancelEventArgs cancelEventArgs = new CancelEventArgs();
      preprocessEnter((object) this, e, cancelEventArgs);
      if (cancelEventArgs.Cancel)
        return;
    }
    base.OnEnter(e);
  }

  internal event PreprocessEventHandler PreprocessLeave;

  protected override void OnLeave(EventArgs e)
  {
    PreprocessEventHandler preprocessLeave = this.PreprocessLeave;
    if (preprocessLeave != null)
    {
      CancelEventArgs cancelEventArgs = new CancelEventArgs();
      preprocessLeave((object) this, e, cancelEventArgs);
      if (cancelEventArgs.Cancel)
        return;
    }
    base.OnLeave(e);
  }

  internal event PreprocessEventHandler PreprocessMouseEnter;

  protected override void OnMouseEnter(EventArgs e)
  {
    PreprocessEventHandler preprocessMouseEnter = this.PreprocessMouseEnter;
    if (preprocessMouseEnter != null)
    {
      CancelEventArgs cancelEventArgs = new CancelEventArgs();
      preprocessMouseEnter((object) this, e, cancelEventArgs);
      if (cancelEventArgs.Cancel)
        return;
    }
    base.OnMouseEnter(e);
  }

  internal event PreprocessEventHandler PreprocessMouseLeave;

  protected override void OnMouseLeave(EventArgs e)
  {
    PreprocessEventHandler preprocessMouseLeave = this.PreprocessMouseLeave;
    if (preprocessMouseLeave != null)
    {
      CancelEventArgs cancelEventArgs = new CancelEventArgs();
      preprocessMouseLeave((object) this, e, cancelEventArgs);
      if (cancelEventArgs.Cancel)
        return;
    }
    base.OnMouseLeave(e);
  }

  internal event PreprocessMouseEventHandler PreprocessMouseDown;

  internal void FireMouseDown(MouseEventArgs ev) => this.OnMouseDown(ev);

  protected override void OnMouseDown(MouseEventArgs ev)
  {
    PreprocessMouseEventHandler preprocessMouseDown = this.PreprocessMouseDown;
    if (preprocessMouseDown != null)
    {
      CancelEventArgs cancelEventArgs = new CancelEventArgs();
      preprocessMouseDown((object) this, ev, cancelEventArgs);
      if (cancelEventArgs.Cancel)
        return;
    }
    this.ctl.OnMouseDown(ev);
    base.OnMouseDown(ev);
  }

  internal event PreprocessMouseEventHandler PreprocessMouseMove;

  protected override void OnMouseMove(MouseEventArgs ev)
  {
    try
    {
      PreprocessMouseEventHandler preprocessMouseMove = this.PreprocessMouseMove;
      if (preprocessMouseMove != null)
      {
        bool ignoreMouseMove = this.IgnoreMouseMove;
        int num1 = this.Focused ? 1 : 0;
        CancelEventArgs cancelEventArgs = new CancelEventArgs();
        preprocessMouseMove((object) this, ev, cancelEventArgs);
        if (cancelEventArgs.Cancel)
          return;
        int y = ev.Y;
        int x = ev.X;
        int num2 = ignoreMouseMove ? 1 : 0;
        int num3 = this.IgnoreMouseMove ? 1 : 0;
      }
      this.ctl.OnMouseMove(ev);
      base.OnMouseMove(ev);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  internal event PreprocessMouseEventHandler PreprocessMouseUp;

  internal void FireMouseUp(MouseEventArgs ev) => this.OnMouseUp(ev);

  protected override void OnMouseUp(MouseEventArgs ev)
  {
    CancelEventArgs cancelEventArgs = new CancelEventArgs();
    PreprocessMouseEventHandler preprocessMouseUp = this.PreprocessMouseUp;
    if (preprocessMouseUp != null)
      preprocessMouseUp((object) this, ev, cancelEventArgs);
    this.ctl.OnMouseUp(ev);
    base.OnMouseUp(ev);
  }

  internal event PreprocessMouseEventHandler PreprocessMouseWheel;

  protected override void OnMouseWheel(MouseEventArgs ev)
  {
    CancelEventArgs cancelEventArgs = new CancelEventArgs();
    PreprocessMouseEventHandler preprocessMouseWheel = this.PreprocessMouseWheel;
    if (preprocessMouseWheel != null)
      preprocessMouseWheel((object) this, ev, cancelEventArgs);
    if (cancelEventArgs.Cancel)
      return;
    this.ctl.OnMouseWheel(ev);
    base.OnMouseWheel(ev);
  }

  protected override void OnPaint(PaintEventArgs ev)
  {
    try
    {
      if (this.hTerWnd != IntPtr.Zero)
      {
        this.draw.RepaintTer(ev);
        if (!this.CaretEnabled && this.pos.UseCaret())
          this.pos.InitCaret();
      }
      base.OnPaint(ev);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  protected override void OnPaintBackground(PaintEventArgs ev)
  {
    if (this.MovingControl)
      return;
    this.RedrawBorder = true;
  }

  protected override void OnResize(EventArgs ev)
  {
    this.RedrawBorder = true;
    try
    {
      if (this.TerArg.open)
      {
        if (this.PaintEnabled)
          this.draw.RepaintTer((PaintEventArgs) null);
      }
    }
    catch (Exception ex)
    {
    }
    base.OnResize(ev);
  }

  internal bool OurPrintf(params object[] msg) => this.misc.OurPrintf(msg);

  internal bool ParaHangingIndent(bool indent, bool repaint)
  {
    return this.par.ParaHangingIndent(indent, repaint);
  }

  internal bool ParaIndentTwips(int DeltaLeft, int DeltaRight, int DeltaFirst, bool repaint)
  {
    return this.par.ParaIndentTwips(DeltaLeft, DeltaRight, DeltaFirst, repaint);
  }

  internal bool ParaLeftIndent(bool indent, bool repaint)
  {
    return this.par.ParaLeftIndent(indent, repaint);
  }

  internal bool ParaNormal(bool repaint) => this.par.ParaNormal(repaint);

  internal bool ParaRightIndent(bool indent, bool repaint)
  {
    return this.par.ParaRightIndent(indent, repaint);
  }

  internal event EventHandler CursorPosChanged;

  internal virtual void OnCursorPosChanged()
  {
    EventHandler cursorPosChanged = this.CursorPosChanged;
    if (cursorPosChanged == null)
      return;
    cursorPosChanged((object) this, new EventArgs());
  }

  protected override bool ProcessCmdKey(ref Message msg, Keys keys)
  {
    if (!base.ProcessCmdKey(ref msg, keys))
    {
      if (keys == (Keys.Space | Keys.Shift | Keys.Control))
      {
        this.edit.TerAscii('\u000E');
        return true;
      }
      bool flag = this.TabStop && (keys & Keys.Tab) == Keys.Tab && (keys & Keys.Return) != Keys.Return && (keys & Keys.Control) != Keys.Control;
      if (!this.Focused)
      {
        if ((keys & Keys.Tab) != Keys.Tab)
          return base.ProcessCmdKey(ref msg, keys);
        return this.fld.TabOnControl((keys & Keys.Shift) == Keys.Shift) || !flag;
      }
      if (flag)
        return false;
      int index = 0;
      while (index < this.TotalSpeedKeys && (this.SpeedKey[index] != keys || !this.SpeedKeyEnabled[index]))
        ++index;
      if (index == this.TotalSpeedKeys)
        return base.ProcessCmdKey(ref msg, keys);
      this.mnu.ProcessCommand(this.SpeedKeyCmd[index]);
    }
    return true;
  }

  internal bool ReadTerFile(string file) => this.io.ReadTerFile(file);

  internal bool SaveTerFile(string file) => this.io.SaveTerFile(file);

  public bool SelectTerText(int FirstLine, int FirstCol, int LastLine, int LastCol, bool repaint)
  {
    return this.draw.SelectTerText(FirstLine, FirstCol, LastLine, LastCol, repaint);
  }

  internal bool SendLinkMessageToParent(ref tc.StrHyperlink link)
  {
    ImRtfEditor.EventHypertext hypertext = this.Hypertext;
    if (hypertext != null)
      hypertext((object) this, ref link);
    ImRtfEditor.EventHypertext ieHypertext = this.IEHypertext;
    if (ieHypertext != null)
      ieHypertext((object) this, ref link);
    return link.used;
  }

  internal bool SendMergeMessage(string name, out string data)
  {
    data = "";
    return this.FireMergeData((object) this, name, out data);
  }

  internal int SendMessageToParent(int msg, int wParam, int lParam, bool DisableTer)
  {
    int parent = 0;
    if (!this.NetControl)
      return parent;
    if (msg == 2731)
      this.FireAction((object) this, wParam, lParam);
    if (msg == 2732)
      this.FirePreprocess((object) this, wParam, lParam);
    if (msg == 2725)
      this.FireModified((object) this);
    if (msg == 2730)
      this.FireUpdateStatusbar((object) this);
    if (msg == 2729)
      this.FireUpdateToolbar((object) this);
    if (msg == 2724)
      this.FireClosing((object) this);
    return parent;
  }

  internal bool SetTab(int type, int pos, bool repaint) => this.par.SetTab(type, pos, repaint);

  internal bool SetTerBkColor(Color color, bool repaint) => this.fnt.SetTerBkColor(color, repaint);

  internal bool SetTerBuffer(string buffer, string name) => this.io.SetTerBuffer(buffer, name);

  public bool SetTerCharStyle(int FmtType, bool OnOff, bool repaint)
  {
    return this.fnt.SetTerCharStyle(FmtType, OnOff, repaint);
  }

  internal bool SetTerColor(Color color, bool repaint) => this.fnt.SetTerColor(color, repaint);

  internal bool SetTerCursorPos(int NewLine, int NewCol, bool repaint)
  {
    return this.pos.SetTerCursorPos(NewLine, NewCol, repaint);
  }

  internal bool SetTerDefaultFont(
    string TypeFace,
    int PointSize,
    int style,
    Color TextColor,
    bool repaint)
  {
    return this.fnt.SetTerDefaultFont(TypeFace, PointSize, style, TextColor, repaint);
  }

  internal bool SetTerFields(tc.StrTerField fld) => this.misc.SetTerFields(fld);

  internal bool SetTerFont(string TypeFace, bool repaint) => this.fnt.SetTerFont(TypeFace, repaint);

  internal bool SetTerParaFmt(int FmtType, bool OnOff, bool repaint)
  {
    return this.par.SetTerParaFmt(FmtType, OnOff, repaint);
  }

  internal bool SetTerPointSize(int PointSize, bool repaint)
  {
    return this.fnt.SetTerPointSize(PointSize, repaint);
  }

  internal static bool StcPrintf(params object[] msg) => CMisc.StcPrintf(msg);

  /// <summary>Преобразовать абсолютную (сквозную) позицию в строке текста в строку и столбец текста</summary>
  /// <param name="abs">Абсолютная позиция в строке текста</param>
  /// <param name="row">Строка</param>
  /// <param name="col">Столбец</param>
  /// <param name="internalPos">Преобразовать в позицию внутреннего представления строки с учётом замен.
  /// Иначе в позицию в оригинальной строке до замен формул и спецсимволов</param>
  /// <param name="scanAllChars">Принудительно проверять весь текст,
  /// для поиска переносов строки, которые находятся не в конце строки</param>
  public void TerAbsToRowCol(
    int abs,
    out int row,
    out int col,
    bool internalPos = true,
    bool scanAllChars = false)
  {
    this.pos.TerAbsToRowCol(abs, out row, out col, internalPos, scanAllChars);
  }

  internal int TerAcceptChanges(bool all, bool msg, bool repaint)
  {
    return this.trk.TerAcceptChanges(all, msg, repaint);
  }

  internal bool TerAddAutoCompWord(string ACWord, string ACPhrase)
  {
    return this.misc.TerAddAutoCompWord(ACWord, ACPhrase);
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
    return this.link.TerAddImageMapRect(MapId, name, LinkInfo, target, left, top, right, bottom);
  }

  internal void TerAddMenuItem(MenuItem parent, string text, int CmdId)
  {
    this.mnu.TerAddMenuItem(parent, text, CmdId);
  }

  internal bool TerAddToolbarIcon(int line, int id, int CmdId, string BmpFile, string pBalloon)
  {
    return this.bar.TerAddToolbarIcon(line, id, CmdId, BmpFile, pBalloon);
  }

  internal bool TerAddToolbarItem(int line, int id, string pBalloon, Image image, int CmdId)
  {
    return this.bar.TerAddToolbarItem(line, id, pBalloon, image, CmdId);
  }

  internal bool TerAdjustHtmlTable() => this.tbl.TerAdjustHtmlTable();

  internal int TerAnd(int val1, int val2) => this.misc.TerAnd(val1, val2);

  internal bool TerAppendText(string text, int FontId, int ParaId, bool repaint)
  {
    return this.io.TerAppendText(text, FontId, ParaId, repaint);
  }

  internal bool TerAppendText2(
    string str,
    int FontId,
    int ParaId,
    int CellId,
    int ParaFID,
    bool repaint)
  {
    return this.io.TerAppendText2(str, FontId, ParaId, CellId, ParaFID, repaint);
  }

  internal bool TerAppendTextEx(
    string text,
    int FontId,
    int ParaId,
    int CellId,
    int ParaFID,
    bool repaint)
  {
    return this.io.TerAppendTextEx(text, FontId, ParaId, CellId, ParaFID, repaint);
  }

  internal bool TerApplyHyperlink(string LinkCode, bool repaint)
  {
    return this.link.TerApplyHyperlink(LinkCode, repaint);
  }

  internal bool TerBulletToText(bool all, bool repaint) => this.par.TerBulletToText(all, repaint);

  internal bool TerCancelEditStyle() => this.par.TerCancelEditStyle();

  internal bool TerCellBorder(
    int select,
    int TopWidth,
    int BotWidth,
    int LeftWidth,
    int RightWidth,
    bool repaint)
  {
    return this.tbl.TerCellBorder(select, TopWidth, BotWidth, LeftWidth, RightWidth, repaint);
  }

  internal bool TerCellBorder2(
    int select,
    int TopWidth,
    int BotWidth,
    int LeftWidth,
    int RightWidth,
    bool outline,
    bool repaint)
  {
    return this.tbl.TerCellBorder2(select, TopWidth, BotWidth, LeftWidth, RightWidth, outline, repaint);
  }

  internal bool TerCellBorderColor(
    int select,
    Color TopColor,
    Color BotColor,
    Color LeftColor,
    Color RightColor,
    bool repaint)
  {
    return this.tbl.TerCellBorderColor(select, TopColor, BotColor, LeftColor, RightColor, repaint);
  }

  internal bool TerCellColor(int select, Color color, bool repaint)
  {
    return this.tbl.TerCellColor(select, color, repaint);
  }

  internal bool TerCellRotateText(int select, int direction, bool repaint)
  {
    return this.tbl.TerCellRotateText(select, direction, repaint);
  }

  internal bool TerCellShading(int select, int shading, bool repaint)
  {
    return this.tbl.TerCellShading(select, shading, repaint);
  }

  internal bool TerCellVertAlign(int select, int align, bool repaint)
  {
    return this.tbl.TerCellVertAlign(select, align, repaint);
  }

  internal bool TerCellWidth(int select, int width, int margin, bool repaint)
  {
    return this.tbl.TerCellWidth(select, width, margin, repaint);
  }

  internal bool TerChangeField(string name, string data, bool repaint)
  {
    return this.fld.TerChangeField(name, data, repaint);
  }

  internal void TerClassCleanup() => this.misc.TerClassCleanup();

  internal bool TerClearAutoCompList() => this.misc.TerClearAutoCompList();

  internal bool TerClosePrinter() => this.init.TerClosePrinter();

  internal bool TerColBreak(bool repaint) => this.sec.TerColBreak(repaint);

  public bool TerCommand(int CmdId) => this.mnu.TerCommand(CmdId);

  internal bool TerCommand2(int CmdId, bool send) => this.mnu.TerCommand2(CmdId, send);

  internal bool TerCopyHeadersFooters(
    int SrcSect,
    int DestSect,
    bool CopyHdr,
    bool CopyFtr,
    bool CopyFirstHdr,
    bool CopyFirstFtr,
    bool repaint)
  {
    return this.sec.TerCopyHeadersFooters(SrcSect, DestSect, CopyHdr, CopyFtr, CopyFirstHdr, CopyFirstFtr, repaint);
  }

  internal int TerCreateBulletId(bool IsBullet, int start, int level, int type)
  {
    return this.par.TerCreateBulletId(IsBullet, start, level, type);
  }

  internal int TerCreateBulletId2(
    bool IsBullet,
    int start,
    int level,
    int type,
    string TextBef,
    string TextAft)
  {
    return this.par.TerCreateBulletId2(IsBullet, start, level, type, TextBef, TextAft);
  }

  internal int TerCreateBulletId3(
    bool IsBullet,
    int start,
    int level,
    int type,
    string TextBef,
    string TextAft,
    int flags)
  {
    return this.par.TerCreateBulletId3(IsBullet, start, level, type, TextBef, TextAft, flags);
  }

  internal int TerCreateCellId(
    bool NewRow,
    int PrevCell,
    int RowAlign,
    int RowPos,
    int RowMinHeight,
    int CellWidth,
    int shading,
    int LeftWidth,
    int RightWidth,
    int TopWidth,
    int BotWidth,
    int RowSpan,
    int ColSpan,
    int CellFlags)
  {
    return this.tbl.TerCreateCellId(NewRow, PrevCell, RowAlign, RowPos, RowMinHeight, CellWidth, shading, LeftWidth, RightWidth, TopWidth, BotWidth, RowSpan, ColSpan, CellFlags);
  }

  internal bool TerCreateControl()
  {
    if (this.IsHandleCreated)
    {
      if (this.TotalLines == 0 && !this.InRtfRead)
        this.TerRewrap();
      return true;
    }
    if (this.UseWin)
    {
      this.CreateControl();
      return this.IsHandleCreated;
    }
    if (this.hTerWnd == IntPtr.Zero)
    {
      this.init.InitTerInst();
      this.wrp.WordWrap(0, this.TotalLines);
    }
    return true;
  }

  internal bool TerCreateFirstHdrFtr(bool HdrFtr) => this.sec.TerCreateFirstHdrFtr(HdrFtr);

  internal int TerCreateFont(
    int ReuseId,
    bool shared,
    string NewTypeFace,
    int NewPointSize,
    int NewStyle,
    Color NewTextColor,
    Color NewTextBkColor,
    int NewFieldId,
    int NewAuxId)
  {
    return this.fnt.TerCreateFont(ReuseId, shared, NewTypeFace, NewPointSize, NewStyle, NewTextColor, NewTextBkColor, NewFieldId, NewAuxId);
  }

  internal int TerCreateFont2(
    int ReuseId,
    bool shared,
    string NewTypeFace,
    int NewPointSize,
    int NewStyle,
    Color NewTextColor,
    Color NewTextBkColor,
    int NewFieldId,
    int NewAuxId,
    int NewCharStyId,
    int NewParaStyId,
    int NewExpand)
  {
    return this.fnt.TerCreateFont2(ReuseId, shared, NewTypeFace, NewPointSize, NewStyle, NewTextColor, NewTextBkColor, NewFieldId, NewAuxId, NewCharStyId, NewParaStyId, NewExpand);
  }

  internal int TerCreateFont3(
    int ReuseId,
    bool shared,
    string NewTypeFace,
    int NewPointSize,
    int NewStyle,
    Color NewTextColor,
    Color NewTextBkColor,
    int NewFieldId,
    int NewAuxId,
    int NewCharStyId,
    int NewParaStyId,
    int NewExpand,
    int NewCharSet)
  {
    return this.fnt.TerCreateFont3(ReuseId, shared, NewTypeFace, NewPointSize, NewStyle, NewTextColor, NewTextBkColor, NewFieldId, NewAuxId, NewCharStyId, NewParaStyId, NewExpand, NewCharSet);
  }

  internal int TerCreateImageMap(string name) => this.link.TerCreateImageMap(name);

  internal int TerCreateListBulletId(int CurListOr, int level)
  {
    return this.par.TerCreateListBulletId(CurListOr, level);
  }

  internal int TerCreateListBulletId2(int CurListOr, int level, int BltType)
  {
    return this.par.TerCreateListBulletId2(CurListOr, level, BltType);
  }

  internal int TerCreateParaFrameId(int x, int y, int width, int height)
  {
    return this.frm.TerCreateParaFrameId(x, y, width, height);
  }

  internal int TerCreateParaId(
    int ReuseId,
    bool shared,
    int LeftIndentTwips,
    int RightIndentTwips,
    int FirstIndentTwips,
    int TabId,
    int StyId,
    int AuxId,
    int shading,
    int pflags,
    int SpaceBefore,
    int SpaceAfter,
    int SpaceBetween,
    int flags)
  {
    return this.par.TerCreateParaId(ReuseId, shared, LeftIndentTwips, RightIndentTwips, FirstIndentTwips, TabId, StyId, AuxId, shading, pflags, SpaceBefore, SpaceAfter, SpaceBetween, flags);
  }

  internal int TerCreateParaIdEx(
    int ReuseId,
    bool shared,
    int LeftIndentTwips,
    int RightIndentTwips,
    int FirstIndentTwips,
    int TabId,
    int StyId,
    int AuxId,
    int shading,
    int pflags,
    int SpaceBefore,
    int SpaceAfter,
    int SpaceBetween,
    int flags,
    int BltId,
    Color BkColor)
  {
    return this.par.TerCreateParaIdEx(ReuseId, shared, LeftIndentTwips, RightIndentTwips, FirstIndentTwips, TabId, StyId, AuxId, shading, pflags, SpaceBefore, SpaceAfter, SpaceBetween, flags, BltId, BkColor);
  }

  internal int TerCreateTabId(tc.StrTab pTabRec) => this.par.TerCreateTabId(pTabRec);

  internal bool TerCreateTable(int row, int col, bool refresh)
  {
    return this.tbl.TerCreateTable(row, col, refresh);
  }

  public bool TerDeleteBlock(bool repaint, bool forceDel = false)
  {
    return this.blk.TerDeleteBlock(repaint, forceDel);
  }

  internal bool TerDeleteBookmark(string name) => this.fnt.TerDeleteBookmark(name);

  internal bool TerDeleteCells(int select, bool repaint)
  {
    return this.tbl.TerDeleteCells(select, repaint);
  }

  internal bool TerDeleteCellText(int select, bool repaint)
  {
    return this.tbl.TerDeleteCellText(select, repaint);
  }

  internal bool TerDeleteField(bool repaint) => this.fld.TerDeleteField(repaint);

  internal bool TerDeleteFirstHdrFtr(bool HdrFtr, bool msg)
  {
    return this.sec.TerDeleteFirstHdrFtr(HdrFtr, msg);
  }

  internal bool TerDeleteHdrFtr(char HdrFtr, bool msg) => this.sec.TerDeleteHdrFtr(HdrFtr, msg);

  internal bool TerDeleteHypertext(int LineNo, int ColNo, bool repaint)
  {
    return this.link.TerDeleteHypertext(LineNo, ColNo, repaint);
  }

  internal bool TerDeleteObject(int idx) => this.blk.TerDeleteObject(idx);

  internal bool TerDeleteStyle(int CurStyle, string name)
  {
    return this.par.TerDeleteStyle(CurStyle, name);
  }

  internal int TerDeleteTag(int line, int col, int type, string name)
  {
    return this.fnt.TerDeleteTag(line, col, type, name);
  }

  internal bool TerDifTableRows(int row1, int row2) => this.tbl.TerDifTableRows(row1, row2);

  internal bool TerDocName(bool get, ref string name) => this.io.TerDocName(get, ref name);

  internal int TerEditList(
    bool NewList,
    int ListId,
    bool PropDialog,
    string name,
    bool nested,
    int flags)
  {
    return this.par.TerEditList(NewList, ListId, PropDialog, name, nested, flags);
  }

  internal bool TerEditListLevel(
    bool IsList,
    int id,
    int level,
    int StartAt,
    int NumType,
    int CharAft,
    string text,
    int FontId,
    int flags)
  {
    return this.par.TerEditListLevel(IsList, id, level, StartAt, NumType, CharAft, text, FontId, flags);
  }

  internal int TerEditListOr(
    bool NewListOr,
    int ListOrId,
    bool PropDialog,
    int ListId,
    bool OverrideLevels,
    int flags)
  {
    return this.par.TerEditListOr(NewListOr, ListOrId, PropDialog, ListId, OverrideLevels, flags);
  }

  internal int EditStyle(bool start, string name, bool CreateNew, int type, bool repaint)
  {
    return this.par.EditStyle(start, name, CreateNew, type, repaint);
  }

  internal bool TerEditTooltip(int id, string pBalloon) => this.bar.TerEditTooltip(id, pBalloon);

  internal bool TerEnableDashes(int lang, int level, bool enable)
  {
    return this.dsh.TerEnableDashes(lang, level, enable);
  }

  internal bool TerEnableRefresh(bool enable) => this.draw.TerEnableRefresh(enable);

  internal bool TerEnableSpeedKey(int cmd, bool enable) => this.misc.TerEnableSpeedKey(cmd, enable);

  internal bool TerEnableTracking(
    bool enable,
    string UName,
    bool UseDefaultClrStyle,
    int InsStyle,
    Color InsColor,
    int DelStyle,
    Color DelColor)
  {
    return this.trk.TerEnableTracking(enable, UName, UseDefaultClrStyle, InsStyle, InsColor, DelStyle, DelColor);
  }

  internal bool TerEngageCaret(bool AtCursorLoc) => this.pos.TerEngageCaret(AtCursorLoc);

  internal bool TerEquateHtmlTable(int CellId, int TotalTableCols)
  {
    return this.tbl.TerEquateHtmlTable(CellId, TotalTableCols);
  }

  internal bool TerFieldToText(bool all, bool repaint) => this.fld.TerFieldToText(all, repaint);

  internal char[] TerFileToMem(string file, out int pSize)
  {
    return this.blk.TerFileToMem(file, out pSize);
  }

  internal byte[] TerFileToMemBytes(string file, out int pSize)
  {
    return this.blk.TerFileToMemBytes(file, out pSize);
  }

  internal bool TerFindHlinkField(
    string CodeString1,
    string CodeString2,
    ref int pLine,
    ref int pCol)
  {
    return this.link.TerFindHlinkField(CodeString1, CodeString2, ref pLine, ref pCol);
  }

  internal bool TerFindNextChange(bool forward, bool repaint)
  {
    return this.trk.TerFindNextChange(forward, repaint);
  }

  internal bool TerFlushUndo() => this.ud.TerFlushUndo();

  internal int TerGetBkPictId() => this.blk.TerGetBkPictId();

  internal int TerGetBookmark(int index, out string name)
  {
    return this.fnt.TerGetBookmark(index, out name);
  }

  internal Graphics TerGetBufferGr() => this.draw.TerGetBufferGr();

  internal int TerGetBulletInfo(
    int IdType,
    int id,
    out bool IsBullet,
    out int start,
    out int level,
    out int symbol,
    out int flags)
  {
    return this.par.TerGetBulletInfo(IdType, id, out IsBullet, out start, out level, out symbol, out flags);
  }

  internal int TerGetBulletInfo2(
    int IdType,
    int id,
    out bool IsBullet,
    out int start,
    out int level,
    out int symbol,
    out int ListOr,
    out int flags)
  {
    return this.par.TerGetBulletInfo2(IdType, id, out IsBullet, out start, out level, out symbol, out ListOr, out flags);
  }

  internal int TerGetBulletInfo3(
    int IdType,
    int id,
    out bool IsBullet,
    out int start,
    out int level,
    out int symbol,
    out int ListOr,
    out int flags,
    out string ListText)
  {
    return this.par.TerGetBulletInfo3(IdType, id, out IsBullet, out start, out level, out symbol, out ListOr, out flags, out ListText);
  }

  internal int TerGetCaretPos() => this.pos.TerGetCaretPos();

  internal bool TerGetCellBorderColor(
    int CellId,
    out Color pLeft,
    out Color pRight,
    out Color pTop,
    out Color pBot)
  {
    return this.tbl.TerGetCellBorderColor(CellId, out pLeft, out pRight, out pTop, out pBot);
  }

  internal bool TerGetCellBorderWidth(
    int CellId,
    out int pLeft,
    out int pRight,
    out int pTop,
    out int pBot)
  {
    return this.tbl.TerGetCellBorderWidth(CellId, out pLeft, out pRight, out pTop, out pBot);
  }

  internal bool TerGetCellInfo(
    int CellId,
    out int row,
    out int PrevCell,
    out int NextCell,
    out int width,
    out int border,
    out int shading,
    out int RowSpan,
    out int ColSpan,
    out int CellFlags)
  {
    return this.tbl.TerGetCellInfo(CellId, out row, out PrevCell, out NextCell, out width, out border, out shading, out RowSpan, out ColSpan, out CellFlags);
  }

  internal bool TerGetCellInfo2(int CellId, out Color BackColor, out int margin)
  {
    return this.tbl.TerGetCellInfo2(CellId, out BackColor, out margin);
  }

  internal bool TerGetCellParam(int type, int CellId, out int val)
  {
    return this.tbl.TerGetCellParam(type, CellId, out val);
  }

  internal bool TerGetCheckboxInfo(int pict, out bool IsChecked)
  {
    return this.fld.TerGetCheckboxInfo(pict, out IsChecked);
  }

  internal int TerGetControlId(int pict) => this.fld.TerGetControlId(pict);

  internal int TerGetControlPos(Control ctl) => this.fld.TerGetControlPos(ctl);

  internal int TerGetCurFont(int line, int col) => this.fnt.TerGetCurFont(line, col);

  internal string TerGetDir(string file, out string dir) => this.misc.TerGetDir(file, out dir);

  internal int TerGetDispPageNo(int pg) => this.page.TerGetDispPageNo(pg);

  internal bool TerGetDrawObjectInfo(
    int FrameId,
    out int width,
    out int height,
    out int LineWdth,
    out Color LineColor,
    out Color BackColor,
    out int flags)
  {
    return this.frm.TerGetDrawObjectInfo(FrameId, out width, out height, out LineWdth, out LineColor, out BackColor, out flags);
  }

  internal int TerGetEffectiveFont() => this.fnt.TerGetEffectiveFont();

  internal string TerGetExt(string file, out string ext) => this.misc.TerGetExt(file, out ext);

  internal int TerGetField(int LineNo, int ColNo, int type, out string text)
  {
    return this.fld.TerGetField(LineNo, ColNo, type, out text);
  }

  internal int TerGetFieldFont(int font, int FieldId, string FieldCode)
  {
    return this.fnt.TerGetFieldFont(font, FieldId, FieldCode);
  }

  internal int TerGetFontAux1Id(int CurFont) => this.fnt.TerGetFontAux1Id(CurFont);

  internal bool TerGetFontFieldCode(int font, out string FieldCode)
  {
    return this.fnt.TerGetFontFieldCode(font, out FieldCode);
  }

  internal int TerGetFontFieldId(int font) => this.fnt.TerGetFontFieldId(font);

  internal int TerGetFontLang(int font) => this.fnt.TerGetFontLang(font);

  internal int TerGetFontParam(int FontId, int type) => this.fnt.TerGetFontParam(FontId, type);

  internal bool TerGetFontParam(int FontId, int type, out Color color)
  {
    return this.fnt.TerGetFontParam(FontId, type, out color);
  }

  internal int TerGetFontSpace(int font) => this.fnt.TerGetFontSpace(font);

  internal int TerGetFontStyleId(int FontId) => this.fnt.TerGetFontStyleId(FontId);

  internal int TerGetFrameParam(int FrameId, int type) => this.frm.TerGetFrameParam(FrameId, type);

  internal bool TerGetFrameSize(
    int ParaFID,
    out int pX,
    out int pY,
    out int pWidth,
    out int pHeight)
  {
    return this.frm.TerGetFrameSize(ParaFID, out pX, out pY, out pWidth, out pHeight);
  }

  internal int TerGetHdrFtrPos(int line) => this.sec.TerGetHdrFtrPos(line);

  internal bool TerGetHypertext(out string text, out string code)
  {
    return this.link.TerGetHypertext(out text, out code);
  }

  internal bool TerGetHypertext2(
    int LineNo,
    int ColNo,
    out string text,
    out string code,
    bool select)
  {
    return this.link.TerGetHypertext2(LineNo, ColNo, out text, out code, select);
  }

  internal bool TerGetHypertextEx(out string text, out string code, bool select)
  {
    return this.link.TerGetHypertextEx(out text, out code, select);
  }

  internal bool TerGetImageMapInfo(int MapId, out string name, out int pCount)
  {
    return this.link.TerGetImageMapInfo(MapId, out name, out pCount);
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
    return this.link.TerGetImageMapRectInfo(MapId, MapRectId, out name, out LinkInfo, out target, out pLeft, out pTop, out pRight, out pBottom);
  }

  internal bool TerGetInputFieldInfo(int pict, out string name, out int type, out bool border)
  {
    return this.fld.TerGetInputFieldInfo(pict, out name, out type, out border);
  }

  internal int TerGetLastMessage(out string message, out string DebugMsg)
  {
    return this.misc.TerGetLastMessage(out message, out DebugMsg);
  }

  internal int TerGetLevelCell(int level, int LineNo) => this.tbl.TerGetLevelCell(level, LineNo);

  internal int TerGetLine(int LineNo, out string text, out int[] font)
  {
    return this.io.TerGetLine(LineNo, out text, out font);
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
    return this.io.TerGetLineInfo(LineNo, out ParaId, out CellId, out ParaFID, out x, out y, out height, out lflags, out InfoFlags);
  }

  internal int TerGetLineParam(int LineNo, int type) => this.io.TerGetLineParam(LineNo, type);

  public int TerGetLineWidth(int LineNo) => this.pos.TerGetLineWidth(LineNo);

  internal int TerGetListId(string name) => this.par.TerGetListId(name);

  internal bool TerGetListInfo(int ListId, out string name, out int pLevelCount, out int pFlags)
  {
    return this.par.TerGetListInfo(ListId, out name, out pLevelCount, out pFlags);
  }

  internal bool TerGetListInfo(
    int ListId,
    out string name,
    out int pLevelCount,
    out int pFlags,
    out int pRtfId,
    out int pTmplId)
  {
    return this.par.TerGetListInfo(ListId, out name, out pLevelCount, out pFlags, out pRtfId, out pTmplId);
  }

  internal bool TerGetListLevelInfo(
    bool IsList,
    int id,
    int level,
    out int pStartAt,
    out int pNumType,
    out int pCharAft,
    out string text,
    out int pFontId,
    out int pFlags)
  {
    return this.par.TerGetListLevelInfo(IsList, id, level, out pStartAt, out pNumType, out pCharAft, out text, out pFontId, out pFlags);
  }

  internal int TerGetListLine(int line) => this.par.TerGetListLine(line);

  internal bool TerGetListOrInfo(
    int ListOrId,
    out int pListId,
    out int pLevelCount,
    out int pFlags)
  {
    return this.par.TerGetListOrInfo(ListOrId, out pListId, out pLevelCount, out pFlags);
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
    return this.sec.TerGetMarginEx(sect, out pLeft, out pRight, out pTop, out pBottom, out pHeaderY, out pFooterY);
  }

  internal int TerGetNextControlPos(int pos) => this.fld.TerGetNextControlPos(pos);

  internal bool TerGetOrigPictSize(int pict, out int width, out int height)
  {
    return this.blk.TerGetOrigPictSize(pict, out width, out height);
  }

  internal bool TerGetPageBorderDim(out int pWidth, out int pHeight)
  {
    return this.frm.TerGetPageBorderDim(out pWidth, out pHeight);
  }

  internal bool TerGetPageCount(out int pTotalPages, out int pCurPage)
  {
    return this.page.TerGetPageCount(out pTotalPages, out pCurPage);
  }

  internal int TerGetPageFirstLine(int pg) => this.page.TerGetPageFirstLine(pg);

  internal int TerGetPageNumFmt(int sect) => this.sec.TerGetPageNumFmt(sect);

  internal bool TerGetPageOffset(
    int PageNo,
    int rel,
    out int PageX,
    out int PageY,
    out int PageHeight,
    out int PageWidth)
  {
    return this.page.TerGetPageOffset(PageNo, rel, out PageX, out PageY, out PageHeight, out PageWidth);
  }

  internal bool TerGetPageOrient(int pg) => this.page.TerGetPageOrient(pg);

  internal bool TerGetPageOrient2(
    int pg,
    out int pWidth,
    out int pHeight,
    out int pHiddenX,
    out int pHiddenY)
  {
    return this.page.TerGetPageOrient2(pg, out pWidth, out pHeight, out pHiddenX, out pHiddenY);
  }

  internal bool TerGetPageOrientEx(int pg, out int pWidth, out int pHeight)
  {
    return this.page.TerGetPageOrientEx(pg, out pWidth, out pHeight);
  }

  internal bool TerGetPagePos(out int pPage, out int pOff)
  {
    return this.page.TerGetPagePos(out pPage, out pOff);
  }

  internal int TerGetPageSect(int pg) => this.page.TerGetPageSect(pg);

  internal int TerGetParaAux1Id(int CurPara) => this.par.TerGetParaAux1Id(CurPara);

  internal bool TerGetParaInfo(
    int LineNo,
    out int LeftIndent,
    out int RightIndent,
    out int FirstIndent,
    out int TabId,
    out int StyId,
    out int AuxId,
    out int shading,
    out int pflags,
    out int SpaceBefore,
    out int SpaceAfter,
    out int SpaceBetween,
    out int flags)
  {
    return this.par.TerGetParaInfo(LineNo, out LeftIndent, out RightIndent, out FirstIndent, out TabId, out StyId, out AuxId, out shading, out pflags, out SpaceBefore, out SpaceAfter, out SpaceBetween, out flags);
  }

  internal bool TerGetParaInfo2(
    int LineNo,
    out int LeftIndent,
    out int RightIndent,
    out int FirstIndent,
    out int TabId,
    out int StyId,
    out int AuxId,
    out int shading,
    out int pflags,
    out int SpaceBefore,
    out int SpaceAfter,
    out int SpaceBetween,
    out int flags,
    out int Aux1Id,
    out Color BkColor)
  {
    return this.par.TerGetParaInfo2(LineNo, out LeftIndent, out RightIndent, out FirstIndent, out TabId, out StyId, out AuxId, out shading, out pflags, out SpaceBefore, out SpaceAfter, out SpaceBetween, out flags, out Aux1Id, out BkColor);
  }

  internal bool TerGetParaInfo3(
    int LineNo,
    bool IsStyleItem,
    out int LeftIndent,
    out int RightIndent,
    out int FirstIndent,
    out int TabId,
    out int StyId,
    out int AuxId,
    out int shading,
    out int pflags,
    out int SpaceBefore,
    out int SpaceAfter,
    out int SpaceBetween,
    out int flags,
    out int Aux1Id,
    out Color BkColor)
  {
    return this.par.TerGetParaInfo3(LineNo, IsStyleItem, out LeftIndent, out RightIndent, out FirstIndent, out TabId, out StyId, out AuxId, out shading, out pflags, out SpaceBefore, out SpaceAfter, out SpaceBetween, out flags, out Aux1Id, out BkColor);
  }

  internal bool TerGetParaInfo4(
    int LineNo,
    bool IsStyleItem,
    out int LeftIndent,
    out int RightIndent,
    out int FirstIndent,
    out int TabId,
    out int StyId,
    out int AuxId,
    out int shading,
    out int pflags,
    out int SpaceBefore,
    out int SpaceAfter,
    out int SpaceBetween,
    out int flags,
    out int Aux1Id,
    out Color BkColor,
    out int LineSpacing)
  {
    return this.par.TerGetParaInfo4(LineNo, IsStyleItem, out LeftIndent, out RightIndent, out FirstIndent, out TabId, out StyId, out AuxId, out shading, out pflags, out SpaceBefore, out SpaceAfter, out SpaceBetween, out flags, out Aux1Id, out BkColor, out LineSpacing);
  }

  internal int TerGetParam(int type) => this.misc.TerGetParam(type);

  internal bool TerGetParam(int type, out Color color) => this.misc.TerGetParam(type, out color);

  internal int TerGetParaParam(int LineNo, bool IsStyleItem, int type)
  {
    return this.par.TerGetParaParam(LineNo, IsStyleItem, type);
  }

  internal bool TerGetParaParam(int LineNo, bool IsStyleItem, int type, out Color color)
  {
    return this.par.TerGetParaParam(LineNo, IsStyleItem, type, out color);
  }

  internal int TerGetPictCropping(int pict, int type) => this.blk.TerGetPictCropping(pict, type);

  internal int TerGetPictFrame(int pict) => this.blk.TerGetPictFrame(pict);

  internal bool TerGetPictInfo(
    int pict,
    out int style,
    out Rectangle OutRect,
    out int align,
    out int AuxId)
  {
    return this.blk.TerGetPictInfo(pict, out style, out OutRect, out align, out AuxId);
  }

  internal int TerGetPictMapId(int PictId) => this.link.TerGetPictMapId(PictId);

  internal int TerGetPictOffset(int pict) => this.blk.TerGetPictOffset(pict);

  internal int TerGetPrevControlPos(int pos) => this.fld.TerGetPrevControlPos(pos);

  internal bool TerGetPrinterHiddenXY(Graphics gr, out int x, out int y)
  {
    return this.prt.TerGetPrinterHiddenXY(gr, out x, out y);
  }

  internal bool TerGetReadOnly() => this.misc.TerGetReadOnly();

  internal int TerGetRowCellCount(bool GetRowCount) => this.tbl.TerGetRowCellCount(GetRowCount);

  internal bool TerGetRowInfo(
    int RowId,
    out int height,
    out int MinHeight,
    out int FixWidth,
    out int PrevRow,
    out int NextRow,
    out int indent,
    out int flags,
    out int border,
    out int CurWidth)
  {
    return this.tbl.TerGetRowInfo(RowId, out height, out MinHeight, out FixWidth, out PrevRow, out NextRow, out indent, out flags, out border, out CurWidth);
  }

  internal int TerGetRtfDocInfo(int InfoType, out string str)
  {
    return this.misc.TerGetRtfDocInfo(InfoType, out str);
  }

  internal string TerGetRtfSel() => this.rtfw.TerGetRtfSel();

  internal int TerGetSectAlign(int sect) => this.sec.TerGetSectAlign(sect);

  internal bool TerGetSectBins(
    int sect,
    out PaperSourceKind FirstPageBin,
    out PaperSourceKind NextPageBin)
  {
    return this.sec.TerGetSectBins(sect, out FirstPageBin, out NextPageBin);
  }

  internal bool TerGetSectBorder(
    int sect,
    out int BorderType,
    out int width,
    out int space,
    out Color color)
  {
    return this.sec.TerGetSectBorder(sect, out BorderType, out width, out space, out color);
  }

  internal bool TerGetSectInfo(
    out int NumCols,
    out int ColSpace,
    out bool StartPage,
    out int FirstPageNo)
  {
    return this.sec.TerGetSectInfo(out NumCols, out ColSpace, out StartPage, out FirstPageNo);
  }

  internal int TerGetSectParam(int sect, int type) => this.sec.TerGetSectParam(sect, type);

  internal bool TerGetSelection(out int BegLine, out int BegCol, out int EndLine, out int EndCol)
  {
    return this.draw.TerGetSelection(out BegLine, out BegCol, out EndLine, out EndCol);
  }

  internal int TerGetSeqSect(int sect) => this.sec.TerGetSeqSect(sect);

  internal object TerGetSpellTimeObject() => this.spl.TerGetSpellTimeObject();

  internal int TerGetStyleId(string name) => this.par.TerGetStyleId(name);

  internal int TerGetStyleInfo(int id, out string name, out int pType)
  {
    return this.par.TerGetStyleInfo(id, out name, out pType);
  }

  internal int TerGetStyleParam(int id, int type) => this.par.TerGetStyleParam(id, type);

  internal int TerGetTableId(int row) => this.tbl.TerGetTableId(row);

  internal int TerGetTableLevel(int LineNo) => this.tbl.TerGetTableLevel(LineNo);

  internal bool TerGetTablePos(out int pTableNo, out int pRowNo, out int pColNo)
  {
    return this.tbl.TerGetTablePos(out pTableNo, out pRowNo, out pColNo);
  }

  internal bool TerGetTablePos2(out int pTableNo, out int pRowNo, out int pColNo, int ParentCell)
  {
    return this.tbl.TerGetTablePos2(out pTableNo, out pRowNo, out pColNo, ParentCell);
  }

  internal int TerGetTabStop(int line, int TabNo, out int pPos, out int pType, out int pFlag)
  {
    return this.par.TerGetTabStop(line, TabNo, out pPos, out pType, out pFlag);
  }

  internal int TerGetTabStop2(
    int type,
    int line,
    int TabNo,
    out int pPos,
    out int pType,
    out int pFlag)
  {
    return this.par.TerGetTabStop2(type, line, TabNo, out pPos, out pType, out pFlag);
  }

  internal int TerGetTag(
    int line,
    int col,
    out string name,
    out string AuxText,
    out int AuxInt,
    out int flags)
  {
    return this.fnt.TerGetTag(line, col, out name, out AuxText, out AuxInt, out flags);
  }

  internal int TerGetTagEx(
    int line,
    int col,
    int TagType,
    out string name,
    out string AuxText,
    out int AuxInt,
    out int flags)
  {
    return this.fnt.TerGetTagEx(line, col, TagType, out name, out AuxText, out AuxInt, out flags);
  }

  internal int TerGetTagEx(
    int line,
    int col,
    int TagType,
    out string name,
    out string AuxText,
    out int AuxInt,
    out object obj,
    out int flags)
  {
    return this.fnt.TerGetTagEx(line, col, TagType, out name, out AuxText, out AuxInt, out obj, out flags);
  }

  internal bool TerGetTextColor(int FontId, out Color TextColor, out Color TextBackColor)
  {
    return this.fnt.TerGetTextColor(FontId, out TextColor, out TextBackColor, out Color _);
  }

  /// <summary>CHANGED Добавил ULineColor</summary>
  /// <param name="FontId"></param>
  /// <param name="TextColor"></param>
  /// <param name="TextBackColor"></param>
  /// <param name="ulineColor"></param>
  /// <returns></returns>
  internal bool TerGetTextColor(
    int FontId,
    out Color TextColor,
    out Color TextBackColor,
    out Color ulineColor)
  {
    return this.fnt.TerGetTextColor(FontId, out TextColor, out TextBackColor, out ulineColor);
  }

  internal bool TerGetTextFieldInfo(
    int pict,
    out string data,
    out int MaxChars,
    out int width,
    out string typeface,
    out int TwipsSize,
    out int style)
  {
    return this.fld.TerGetTextFieldInfo(pict, out data, out MaxChars, out width, out typeface, out TwipsSize, out style);
  }

  internal int TerGetTextHeight() => this.page.TerGetTextHeight();

  public string TerGetTextSel() => this.io.TerGetTextSel();

  internal int TerGetVisibleCol(int line, int col) => this.pos.TerGetVisibleCol(line, col);

  internal int TerGetWordCount(int flags) => this.blk.TerGetWordCount(flags);

  internal int TerHdrFtrExists(int SectId) => this.sec.TerHdrFtrExists(SectId);

  internal bool TerHideToolbarIcon(int id, bool hide) => this.bar.TerHideToolbarIcon(id, hide);

  internal bool TerHtmlCellWidthFlag(int select, int flag, bool repaint)
  {
    return this.tbl.TerHtmlCellWidthFlag(select, flag, repaint);
  }

  internal bool TerIgnoreCommand() => this.mnu.TerIgnoreCommand();

  internal int TerImageMapNameToId(string name) => this.link.TerImageMapNameToId(name);

  internal bool TerInitSpellTime(object st) => this.spl.TerInitSpellTime(st);

  internal int TerInsertBookmark(int line, int col, string name)
  {
    return this.fnt.TerInsertBookmark(line, col, name);
  }

  internal int TerInsertCheckBoxField(
    string name,
    int TwipsSize,
    bool IsChecked,
    bool insert,
    bool repaint)
  {
    return this.fld.TerInsertCheckBoxField(name, TwipsSize, IsChecked, insert, repaint);
  }

  internal int TerInsertControl(Control ctl, string ClassName, int align, int id, bool insert)
  {
    return this.fld.TerInsertControl(ctl, ClassName, align, id, insert);
  }

  internal bool TerInsertDateTime(string pDateFmt, bool repaint)
  {
    return this.fld.TerInsertDateTime(pDateFmt, repaint);
  }

  internal int TerInsertDrawObject(int type, int x, int y, int width, int height)
  {
    return this.frm.TerInsertDrawObject(type, x, y, width, height);
  }

  internal bool TerInsertField(string name, string data, bool repaint)
  {
    return this.fld.TerInsertField(name, data, repaint);
  }

  internal bool TerInsertFootnote(string FnMarker, string FnText, int style, bool repaint)
  {
    return this.sec.TerInsertFootnote(FnMarker, FnText, style, repaint);
  }

  internal bool TerInsertFootnote2(
    string FnMarker,
    string FnText,
    int style,
    bool IsFootnote,
    bool repaint)
  {
    return this.sec.TerInsertFootnote2(FnMarker, FnText, style, IsFootnote, repaint);
  }

  public int TerInsertHyperlink(string LinkText, string LinkCode, int PictId, bool repaint)
  {
    return this.link.TerInsertHyperlink(LinkText, LinkCode, PictId, repaint);
  }

  internal bool TerInsertLine(
    string text,
    int FontId,
    int ParaId,
    int CellId,
    int ParaFID,
    bool repaint)
  {
    return this.io.TerInsertLine(text, FontId, ParaId, CellId, ParaFID, repaint);
  }

  internal int TerInsertLineObject(int x1, int y1, int x2, int y2)
  {
    return this.frm.TerInsertLineObject(x1, y1, x2, y2);
  }

  internal bool TerInsertObjectId(int pict, bool repaint)
  {
    return this.blk.TerInsertObjectId(pict, repaint);
  }

  internal bool TerInsertPageRef(
    string bookmark,
    bool IsHyperlink,
    bool IsAlphabetic,
    bool repaint)
  {
    return this.page.TerInsertPageRef(bookmark, IsHyperlink, IsAlphabetic, repaint);
  }

  internal int TerInsertParaFrame(
    int x,
    int y,
    int width,
    int height,
    bool boxed,
    bool rotatedFrame = false)
  {
    return this.frm.TerInsertParaFrame(x, y, width, height, boxed, rotatedFrame);
  }

  internal int TerInsertPictureFile(string FileName, bool embed, int align, bool insert)
  {
    return this.blk.TerInsertPictureFile(FileName, embed, align, insert);
  }

  internal int TerInsertPictureFileXY(
    string FileName,
    bool embed,
    int align,
    bool insert,
    int x,
    int y)
  {
    return this.blk.TerInsertPictureFileXY(FileName, embed, align, insert, x, y);
  }

  internal bool TerInsertRtfFile(string FileName, int line, int col, bool repaint)
  {
    return this.rtfr.TerInsertRtfFile(FileName, line, col, repaint);
  }

  internal bool TerInsertTableCol(bool insert, bool AllRows, bool repaint)
  {
    return this.tbl.TerInsertTableCol(insert, AllRows, repaint);
  }

  internal bool TerInsertTableRow(bool insert, bool repaint)
  {
    return this.tbl.TerInsertTableRow(insert, repaint);
  }

  public bool TerInsertText(string text, int FontId, int ParaId, bool repaint)
  {
    return this.io.TerInsertText(text, FontId, ParaId, repaint);
  }

  internal int TerInsertTextInputField(
    string name,
    string data,
    int MaxLen,
    bool border,
    string pTypeface,
    int TwipsSize,
    int TextStyle,
    Color TextColor,
    bool insert,
    bool repaint)
  {
    return this.fld.TerInsertTextInputField(name, data, MaxLen, border, pTypeface, TwipsSize, TextStyle, TextColor, insert, repaint);
  }

  internal bool TerInsertToc(bool repaint) => this.sec.TerInsertToc(repaint);

  internal bool TerInternetGet(string url, string OutFile) => this.io.TerInternetGet(url, OutFile);

  internal bool TerIsModified() => this.misc.TerIsModified();

  internal bool TerIsPrinting() => this.prt.TerIsPrinting();

  internal bool TerIsTableSelected() => this.tbl.TerIsTableSelected();

  internal bool TerLineInfoFlags(int LineNo, int flags)
  {
    return this.misc.TerLineInfoFlags(LineNo, flags);
  }

  internal bool TerLineSelected(int LineNo) => this.edit.TerLineSelected(LineNo);

  internal bool TerLocateAuxIdChar(
    int AuxId,
    bool present,
    ref int StartLine,
    ref int StartCol,
    bool forward)
  {
    return this.fnt.TerLocateAuxIdChar(AuxId, present, ref StartLine, ref StartCol, forward);
  }

  internal bool TerLocateChangedChar(
    int InsRev,
    int DelRev,
    bool present,
    ref int StartLine,
    ref int StartCol,
    bool forward)
  {
    return this.trk.TerLocateChangedChar(InsRev, DelRev, present, ref StartLine, ref StartCol, forward);
  }

  internal bool TerLocateField(int loc, string name, bool exact, bool repaint)
  {
    return this.fld.TerLocateField(loc, name, exact, repaint);
  }

  internal bool TerLocateFieldChar(
    int FieldId,
    string FieldCode,
    bool present,
    ref int StartLine,
    ref int StartCol,
    bool forward)
  {
    return this.fld.TerLocateFieldChar(FieldId, FieldCode, present, ref StartLine, ref StartCol, forward);
  }

  internal bool TerLocateFontFlags(
    int flags,
    bool present,
    ref int StartLine,
    ref int StartCol,
    bool forward)
  {
    return this.fnt.TerLocateFontFlags(flags, present, ref StartLine, ref StartCol, forward);
  }

  internal bool TerLocateFontId(int FontId, ref int pLineNo, ref int pCol)
  {
    return this.fnt.TerLocateFontId(FontId, ref pLineNo, ref pCol);
  }

  internal int TerLocateInputField(int loc, bool repaint)
  {
    return this.fld.TerLocateInputField(loc, repaint);
  }

  internal bool TerLocateStyle(int style, ref int StartLine, ref int StartCol, out int StringLen)
  {
    return this.fnt.TerLocateStyle(style, ref StartLine, ref StartCol, out StringLen);
  }

  internal bool TerLocateStyleChar(
    int style,
    bool present,
    ref int StartLine,
    ref int StartCol,
    bool forward)
  {
    return this.fnt.TerLocateStyleChar(style, present, ref StartLine, ref StartCol, forward);
  }

  internal bool TerMemToFileBytes(string file, byte[] data)
  {
    return this.blk.TerMemToFileBytes(file, data);
  }

  internal int TerMenuEnable(int MenuId) => this.mnu.TerMenuEnable(MenuId);

  internal bool TerMenuEnable2(int MenuId) => this.mnu.TerMenuEnable2(MenuId);

  internal int TerMenuSelect(int MenuId) => this.mnu.TerMenuSelect(MenuId);

  internal bool TerMenuSelect2(int MenuId) => this.mnu.TerMenuSelect2(MenuId);

  internal bool TerMergeFields(string names, string data, bool repaint)
  {
    return this.fld.TerMergeFields(names, data, repaint);
  }

  internal static bool TerMergePrint(ref tc.StrPrint pr) => CPrt.TerMergePrint(ref pr);

  internal bool TerMoveParaFrame(
    int ParaFID,
    int FrameX,
    int FrameY,
    int FrmWidth,
    int FrmHeight)
  {
    return this.frm.TerMoveParaFrame(ParaFID, FrameX, FrameY, FrmWidth, FrmHeight);
  }

  internal bool TerMoveParaFrame2(
    int ParaFID,
    int FrameX,
    int FrameY,
    int FrmWidth,
    int FrmHeight,
    int page)
  {
    return this.frm.TerMoveParaFrame2(ParaFID, FrameX, FrameY, FrmWidth, FrmHeight, page);
  }

  internal bool TerMovePictFrame(int pict, int FrameX, int FrameY)
  {
    return this.frm.TerMovePictFrame(pict, FrameX, FrameY);
  }

  internal bool TerMovePictFrame2(int pict, int DeltaX, int DeltaY)
  {
    return this.frm.TerMovePictFrame2(pict, DeltaX, DeltaY);
  }

  internal bool TerNormalizeBlock() => this.blk.TerNormalizeBlock();

  internal int TerOr(int val1, int val2) => this.misc.TerOr(val1, val2);

  internal static bool TerOverridePageSize(int width, int height)
  {
    return CPrt.TerOverridePageSize(width, height);
  }

  internal Bitmap TerPageBitmap(int page) => this.prt.TerPageBitmap(page);

  internal bool TerPageBreak(bool repaint) => this.page.TerPageBreak(repaint);

  internal int TerPageFromLine(int LineNo) => this.page.TerPageFromLine(LineNo);

  internal Metafile TerPageMetafile(int page) => this.prt.TerPageMetafile(page);

  internal Metafile TerPageMetafile(int page, Stream output)
  {
    return this.prt.TerPageMetafile(page, output);
  }

  internal int TerPastePicture(string format, Image image, int ParaFID, int align, bool insert)
  {
    return this.blk.TerPastePicture(format, image, ParaFID, align, insert);
  }

  internal bool TerPictAltInfo(int pict, bool get, ref string info)
  {
    return this.blk.TerPictAltInfo(pict, get, ref info);
  }

  internal bool TerPictLinkName(int pict, bool get, ref string name)
  {
    return this.blk.TerPictLinkName(pict, get, ref name);
  }

  internal bool TerPixToTextPos(int rel, int x, int y, out int line, ref int col)
  {
    return this.pos.TerPixToTextPos(rel, x, y, out line, ref col);
  }

  internal bool TerPosAfterTable(bool OuterMost, bool repaint)
  {
    return this.tbl.TerPosAfterTable(OuterMost, repaint);
  }

  internal bool TerPosBodyText(int sect, int PosType, bool repaint)
  {
    return this.pos.TerPosBodyText(sect, PosType, repaint);
  }

  internal bool TerPosBookmark(string name, bool repaint) => this.fnt.TerPosBookmark(name, repaint);

  internal bool TerPosFrame(int FrameNo, int pos, bool repaint)
  {
    return this.frm.TerPosFrame(FrameNo, pos, repaint);
  }

  internal bool TerPosHdrFtr(int sect, bool header, int pos, bool repaint)
  {
    return this.sec.TerPosHdrFtr(sect, header, pos, repaint);
  }

  internal bool TerPosHdrFtrEx(int sect, char HdrFtr, int pos, bool repaint)
  {
    return this.sec.TerPosHdrFtrEx(sect, HdrFtr, pos, repaint);
  }

  internal bool TerPosLineAtTop(int line, bool top) => this.pos.TerPosLineAtTop(line, top);

  internal bool TerPosPage(int NewPage) => this.page.TerPosPage(NewPage);

  internal bool TerPosTable(int TableNo, int RowNo, int ColNo, int pos, bool repaint)
  {
    return this.tbl.TerPosTable(TableNo, RowNo, ColNo, pos, repaint);
  }

  internal bool TerPosTable2(
    int TableNo,
    int RowNo,
    int ColNo,
    int pos,
    int ParentCell,
    bool repaint)
  {
    return this.tbl.TerPosTable2(TableNo, RowNo, ColNo, pos, ParentCell, repaint);
  }

  internal bool TerPosTable3(int TableId, int RowNo, int ColNo, int pos, bool repaint)
  {
    return this.tbl.TerPosTable3(TableId, RowNo, ColNo, pos, repaint);
  }

  internal bool TerPosTable4(
    int TableId,
    int TableNo,
    int RowNo,
    int ColNo,
    int pos,
    int ParentCell,
    bool repaint)
  {
    return this.tbl.TerPosTable4(TableId, TableNo, RowNo, ColNo, pos, ParentCell, repaint);
  }

  internal bool TerPosTag(int TagId, string name, int scope, bool repaint)
  {
    return this.fnt.TerPosTag(TagId, name, scope, repaint);
  }

  internal bool TerPosTagEx(int TagType, int TagId, string name, int scope, bool repaint)
  {
    return this.fnt.TerPosTagEx(TagType, TagId, name, scope, repaint);
  }

  internal bool TerPrint(bool dialog) => this.prt.TerPrint(dialog);

  internal bool TerPrint2(bool dialog, int FirstPage, int LastPage, int copies, bool colate)
  {
    return this.prt.TerPrint2(dialog, FirstPage, LastPage, copies, colate);
  }

  internal bool TerPrintEx(bool dialog, int FirstPage, int LastPage)
  {
    return this.prt.TerPrintEx(dialog, FirstPage, LastPage);
  }

  internal bool TerPrintPreview(Graphics gr, Rectangle rect, int page, bool scale)
  {
    return this.prt.TerPrintPreview(gr, rect, page, scale);
  }

  internal PrintPreviewDialog TerPrintPreviewDlg() => this.prt.TerPrintPreviewDlg();

  internal bool TerQueryExit() => this.misc.TerQueryExit();

  internal bool TerQuickRepaint() => this.draw.TerQuickRepaint();

  internal bool TerRecreateToolbar(bool show) => this.bar.TerRecreateToolbar(show);

  public bool TerRepaginate(bool repaint) => this.page.TerRepaginate(repaint);

  internal bool TerRepaint(bool ClearBackground) => this.draw.TerRepaint(ClearBackground);

  internal bool TerResetLastMessage() => this.misc.TerResetLastMessage();

  internal bool TerRewrap() => this.wrp.TerRewrap();

  internal bool TerRotateFrameText(bool dialog, int LineNo, int direction, bool repaint)
  {
    return this.frm.TerRotateFrameText(dialog, LineNo, direction, repaint);
  }

  /// <summary>Преобразовать строку столбец в абсолютные координаты в строке</summary>
  /// <param name="row">Строка</param>
  /// <param name="col">Столбец</param>
  /// <param name="internalPos">Преобразовать в позицию внутреннего представления строки с учётом замен.
  /// Иначе в позицию в оригинальной строке до замен формул и спецсимволов</param>
  /// <param name="scanAllChars">Принудительно проверять весь текст,
  /// для поиска переносов строки, которые находятся не в конце строки</param>
  /// <returns></returns>
  public int TerRowColToAbs(int row, int col, bool internalPos = true, bool scanAllChars = false)
  {
    return this.pos.TerRowColToAbs(row, col, internalPos, scanAllChars);
  }

  internal bool TerRowHeight(int MinHeight, bool AllRows, bool refresh)
  {
    return this.tbl.TerRowHeight(MinHeight, AllRows, refresh);
  }

  internal bool TerRowPosition(int JustFlag, bool AllRows, bool refresh)
  {
    return this.tbl.TerRowPosition(JustFlag, AllRows, refresh);
  }

  internal bool TerRowPositionEx(int JustFlag, int indent, bool AllRows, bool refresh)
  {
    return this.tbl.TerRowPositionEx(JustFlag, indent, AllRows, refresh);
  }

  internal bool TerSavePict(int pict, string FileName, ImageFormat fmt)
  {
    return this.blk.TerSavePict(pict, FileName, fmt);
  }

  internal int TerScrLineHeight(int line) => this.pos.TerScrLineHeight(line);

  public bool TerScrToTwipsX(int scrX, out int twipsX) => this.pos.TerScrToTwipsX(scrX, out twipsX);

  internal bool TerScrToTwipsY(int scrY, out int twipsY)
  {
    return this.pos.TerScrToTwipsY(scrY, out twipsY);
  }

  internal bool TerSearchReplace(
    ref string search,
    string replace,
    int flags,
    int StartPos,
    ref int EndPos,
    out int BufSize)
  {
    return this.misc.TerSearchReplace(ref search, replace, flags, StartPos, ref EndPos, out BufSize);
  }

  public int TerSearchReplace2(
    string search,
    string replace,
    int flags,
    int StartPos,
    int EndPos)
  {
    return this.misc.TerSearchReplace2(search, replace, flags, StartPos, EndPos);
  }

  internal bool TerSectBreak(bool repaint) => this.sec.TerSectBreak(repaint);

  internal bool TerSelectCellText(bool repaint) => this.tbl.TerSelectCellText(repaint);

  internal bool TerSelectCharStyle(int CurStyle, bool repaint)
  {
    return this.par.TerSelectCharStyle(CurStyle, repaint);
  }

  internal bool TerSelectCol(bool repaint) => this.tbl.TerSelectCol(repaint);

  internal bool TerSelectParaStyle(int CurStyle, bool repaint)
  {
    return this.par.TerSelectParaStyle(CurStyle, repaint);
  }

  internal bool TerSelectPrint() => this.prt.TerSelectPrint();

  internal bool TerSelectPrint2(int copies) => this.prt.TerSelectPrint2(copies);

  internal bool TerSelectRow(bool repaint) => this.tbl.TerSelectRow(repaint);

  internal bool TerSelectTable(int level, bool repaint) => this.tbl.TerSelectTable(level, repaint);

  internal bool TerSetBkColor(Color BkColor) => this.fnt.TerSetBkColor(BkColor);

  internal bool TerSetBkPictId(int id, int flag, bool repaint)
  {
    return this.blk.TerSetBkPictId(id, flag, repaint);
  }

  internal bool TerSetBorderColor(Color BkColor) => this.draw.TerSetBorderColor(BkColor);

  internal bool TerSetBullet(bool set, bool repaint) => this.par.TerSetBullet(set, repaint);

  internal bool TerSetBullet2(
    bool set,
    bool IsBullet,
    int start,
    int level,
    int type,
    string TextBef,
    string TextAft,
    bool repaint)
  {
    return this.par.TerSetBullet2(set, IsBullet, start, level, type, TextBef, TextAft, repaint);
  }

  internal bool TerSetBullet3(
    bool set,
    bool IsBullet,
    int start,
    int level,
    int type,
    string TextBef,
    string TextAft,
    int BltFlags,
    bool repaint)
  {
    return this.par.TerSetBullet3(set, IsBullet, start, level, type, TextBef, TextAft, BltFlags, repaint);
  }

  internal bool TerSetBulletEx(
    bool set,
    bool IsBullet,
    int start,
    int level,
    int type,
    bool repaint)
  {
    return this.par.TerSetBulletEx(set, IsBullet, start, level, type, repaint);
  }

  internal bool TerSetBulletId(int BltId, int ParaId) => this.par.TerSetBulletId(BltId, ParaId);

  internal bool TerSetCaretPos(int NewPos) => this.pos.TerSetCaretPos(NewPos);

  internal bool TerSetCellBorderColor(int CellId, Color top, Color bot, Color left, Color right)
  {
    return this.tbl.TerSetCellBorderColor(CellId, top, bot, left, right);
  }

  internal bool TerSetCellInfo(int CellId, Color BackColor, int margin)
  {
    return this.tbl.TerSetCellInfo(CellId, BackColor, margin);
  }

  internal bool TerSetCellInfo2(int CellId, Color BackColor, int margin, int ParentCell)
  {
    return this.tbl.TerSetCellInfo2(CellId, BackColor, margin, ParentCell);
  }

  internal bool TerSetCharAuxId(int AuxId, bool repaint)
  {
    return this.fnt.TerSetCharAuxId(AuxId, repaint);
  }

  internal bool TerSetCharLang(int lang, bool repaint) => this.fnt.TerSetCharLang(lang, repaint);

  internal bool TerSetCharOffset(int offset, bool repaint)
  {
    return this.fnt.TerSetCharOffset(offset, repaint);
  }

  internal bool TerSetCharSet(byte CharSet) => this.fnt.TerSetCharSet(CharSet);

  internal bool TerSetCharSpace(bool dialog, int expand, bool repaint)
  {
    return this.fnt.TerSetCharSpace(dialog, expand, repaint);
  }

  internal bool TerSetCheckboxInfo(int pict, bool IsChecked)
  {
    return this.fld.TerSetCheckboxInfo(pict, IsChecked);
  }

  internal bool TerSetCtlColor(Color BkColor, bool repaint)
  {
    return this.draw.TerSetCtlColor(BkColor, repaint);
  }

  internal bool TerSetCustomMessage(int id, string message)
  {
    return this.misc.TerSetCustomMessage(id, message);
  }

  internal bool TerSetDefBkColor(Color BackColor, bool repaint)
  {
    return this.draw.TerSetDefBkColor(BackColor, repaint);
  }

  internal bool TerSetDefDir(string DefDir, int type) => this.io.TerSetDefDir(DefDir, type);

  internal bool TerSetDefLang(int lang) => this.fnt.TerSetDefLang(lang);

  internal static bool TerSetDefPrinter(PrinterSettings PrtSettings, PageSettings PgSettings)
  {
    return CPrt.TerSetDefPrinter(PrtSettings, PgSettings);
  }

  internal bool TerSetDefTabType(int DefTab) => this.misc.TerSetDefTabType(DefTab);

  internal int TerSetDefTabWidth(int width, bool repaint)
  {
    return this.par.TerSetDefTabWidth(width, repaint);
  }

  internal bool TerSetDefTextColor(Color ForeColor, bool repaint)
  {
    return this.fnt.TerSetDefTextColor(ForeColor, repaint);
  }

  internal bool TerSetDocTextFlow(bool dialog, int flow, bool repaint)
  {
    return this.misc.TerSetDocTextFlow(dialog, flow, repaint);
  }

  internal bool TerSetEffectiveFont(int NewFont) => this.fnt.TerSetEffectiveFont(NewFont);

  internal int TerSetFlags(bool set, int flags) => this.misc.TerSetFlags(set, flags);

  internal int TerSetFlags2(bool set, int flags) => this.misc.TerSetFlags2(set, flags);

  internal int TerSetFlags3(bool set, int flags) => this.misc.TerSetFlags3(set, flags);

  internal int TerSetFlags4(bool set, int flags) => this.misc.TerSetFlags4(set, flags);

  internal int TerSetFlags5(bool set, int flags) => this.misc.TerSetFlags5(set, flags);

  internal int TerSetFlags6(bool set, int flags) => this.misc.TerSetFlags6(set, flags);

  internal bool TerSetFocus() => this.misc.TerSetFocus();

  internal bool TerSetFontField(int font, int FieldId, string FieldCode)
  {
    return this.fnt.TerSetFontField(font, FieldId, FieldCode);
  }

  internal bool TerSetFontId(int FontId) => this.fnt.TerSetFontId(FontId);

  internal bool TerSetFontSpace(int font, int expand, bool repaint)
  {
    return this.fnt.TerSetFontSpace(font, expand, repaint);
  }

  internal bool TerSetFontStyleId(int FontId, int CharStyId, int ParaStyId)
  {
    return this.fnt.TerSetFontStyleId(FontId, CharStyId, ParaStyId);
  }

  internal bool TerSetFrameMarginDist(int dist) => this.frm.TerSetFrameMarginDist(dist);

  internal bool TerSetFrameTextDist(int ParaFID, int dist)
  {
    return this.frm.TerSetFrameTextDist(ParaFID, dist);
  }

  internal bool TerSetFrameYBase(int FrameId, int YBase)
  {
    return this.frm.TerSetFrameYBase(FrameId, YBase);
  }

  internal bool TerSetHdrRow(int CurCell, bool set, bool refresh)
  {
    return this.tbl.TerSetHdrRow(CurCell, set, refresh);
  }

  internal bool TerSetHtmlTblWidth(int CellId, int width)
  {
    return this.tbl.TerSetHtmlTblWidth(CellId, width);
  }

  internal bool TerSetHtnAssembly(Assembly assembly) => this.misc.TerSetHtnAssembly(assembly);

  internal void TerSetHtnLicenseKey(string key) => this.misc.TerSetHtnLicenseKey(key);

  internal bool TerSetHtnObject(object htn) => this.misc.TerSetHtnObject(htn);

  internal bool TerSetInitTypeface(string typeface) => this.fnt.TerSetInitTypeface(typeface);

  internal bool TerSetInputFieldInfo(int pict, string name, bool border)
  {
    return this.fld.TerSetInputFieldInfo(pict, name, border);
  }

  internal static bool TerSetLicenseKey(string key) => CInit.TerSetLicenseKey(key);

  internal bool TerSetLine(int LineNo, string text, int[] font)
  {
    return this.io.TerSetLine(LineNo, text, font);
  }

  internal bool TerSetLinkDblClick(bool DblClick) => this.link.TerSetLinkDblClick(DblClick);

  internal bool TerSetLinkPictDir(string PictDir) => this.io.TerSetLinkPictDir(PictDir);

  internal bool TerSetListBullet(
    bool set,
    int NumType,
    int level,
    int start,
    string TextBef,
    string TextAft,
    bool repaint)
  {
    return this.par.TerSetListBullet(set, NumType, level, start, TextBef, TextAft, repaint);
  }

  internal bool TerSetListBullet2(
    bool set,
    int NumType,
    int level,
    int start,
    string TextBef,
    string TextAft,
    string ListText,
    bool repaint)
  {
    return this.par.TerSetListBullet2(set, NumType, level, start, TextBef, TextAft, ListText, repaint);
  }

  internal bool TerSetListLevel(int level, int increment, bool repaint)
  {
    return this.par.TerSetListLevel(level, increment, repaint);
  }

  internal bool TerSetMapRectInfo(
    int MapId,
    int RectId,
    string name,
    string LinkInfo,
    string target)
  {
    return this.link.TerSetMapRectInfo(MapId, RectId, name, LinkInfo, target);
  }

  internal bool TerSetMargin(int left, int right, int top, int bottom, bool refresh)
  {
    return this.sec.TerSetMargin(left, right, top, bottom, refresh);
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
    return this.sec.TerSetMarginEx(sect, left, right, top, bottom, HeaderY, FooterY, refresh);
  }

  internal bool TerSetMaxUndo(int NewMaxUndoLimit) => this.ud.TerSetMaxUndo(NewMaxUndoLimit);

  internal static bool TerSetMergeDelim(string delim) => CFld.TerSetMergeDelim(delim);

  public bool TerSetModify(bool modified) => this.misc.TerSetModify(modified);

  internal bool TerSetNewFrameDim(int x, int y, int width, int height, bool PageTop)
  {
    return this.frm.TerSetNewFrameDim(x, y, width, height, PageTop);
  }

  internal bool TerSetNextFontAux1Id(int Aux1Id) => this.fnt.TerSetNextFontAux1Id(Aux1Id);

  internal bool TerSetNextParaAux1Id(int Aux1Id) => this.par.TerSetNextParaAux1Id(Aux1Id);

  internal bool TerSetObjectAttrib(
    int FrameId,
    int LineType,
    int LineThickness,
    Color LineColor,
    bool FillSolid,
    Color FillColor)
  {
    return this.frm.TerSetObjectAttrib(FrameId, LineType, LineThickness, LineColor, FillSolid, FillColor);
  }

  internal bool TerSetObjectAttribEx(
    int FrameId,
    int LineType,
    int LineThickness,
    Color LineColor,
    bool FillSolid,
    Color FillColor,
    int ZOrder)
  {
    return this.frm.TerSetObjectAttribEx(FrameId, LineType, LineThickness, LineColor, FillSolid, FillColor, ZOrder);
  }

  internal bool TerSetObjectWrapStyle(int FrameId, int WrapStyle)
  {
    return this.frm.TerSetObjectWrapStyle(FrameId, WrapStyle);
  }

  internal bool TerSetOutputFormat(int format) => this.io.TerSetOutputFormat(format);

  internal bool TerSetPageBkColor(Color BkColor) => this.page.TerSetPageBkColor(BkColor);

  internal bool TerSetPageBorderWidth(int width, bool repaint)
  {
    return this.page.TerSetPageBorderWidth(width, repaint);
  }

  internal bool TerSetPageNumFmt(int sect, int fmt) => this.sec.TerSetPageNumFmt(sect, fmt);

  internal bool TerSetPagePos(int NewPage, int PageY) => this.page.TerSetPagePos(NewPage, PageY);

  internal bool TerSetPaper(PaperSize size, bool IsProtrait, bool repaint)
  {
    return this.sec.TerSetPaper(size, IsProtrait, repaint);
  }

  internal bool TerSetParaAuxId(int FirstLine, int LastLine, int AuxId)
  {
    return this.par.TerSetParaAuxId(FirstLine, LastLine, AuxId);
  }

  internal bool TerSetParaBkColor(bool dialog, Color color, bool repaint)
  {
    return this.par.TerSetParaBkColor(dialog, color, repaint);
  }

  internal bool TerSetParaBorderColor(Color color, bool repaint)
  {
    return this.par.TerSetParaBorderColor(color, repaint);
  }

  internal bool TerSetParaId(int FirstLine, int LastLine, int ParaId)
  {
    return this.par.TerSetParaId(FirstLine, LastLine, ParaId);
  }

  internal bool TerSetParaIndent(int left, int right, int first, bool repaint)
  {
    return this.par.TerSetParaIndent(left, right, first, repaint);
  }

  internal bool TerSetParaList(bool dialog, int ParaId, int CurListOr, int level, bool repaint)
  {
    return this.par.TerSetParaList(dialog, ParaId, CurListOr, level, repaint);
  }

  internal bool TerSetParaShading(int shading, bool repaint)
  {
    return this.par.TerSetParaShading(shading, repaint);
  }

  internal bool TerSetParaSpacing(int SpaceBefore, int SpaceAfter, int SpaceBetween, bool repaint)
  {
    return this.par.TerSetParaSpacing(SpaceBefore, SpaceAfter, SpaceBetween, repaint);
  }

  internal bool TerSetParaSpacing2(
    int SpaceBefore,
    int SpaceAfter,
    int SpaceBetween,
    int LineSpacing,
    bool repaint)
  {
    return this.par.TerSetParaSpacing2(SpaceBefore, SpaceAfter, SpaceBetween, LineSpacing, repaint);
  }

  internal bool TerSetParaTextFlow(bool dialog, int flow, bool repaint)
  {
    return this.par.TerSetParaTextFlow(dialog, flow, repaint);
  }

  internal bool TerSetPflags(int FmtType, bool OnOff, bool repaint)
  {
    return this.par.TerSetPflags(FmtType, OnOff, repaint);
  }

  internal bool TerSetPictCropping(
    int pict,
    int CropLeft,
    int CropTop,
    int CropRight,
    int CropBot,
    bool repaint)
  {
    return this.blk.TerSetPictCropping(pict, CropLeft, CropTop, CropRight, CropBot, repaint);
  }

  internal int TerSetPictFrame(int pict, int FrameType, bool repaint)
  {
    return this.blk.TerSetPictFrame(pict, FrameType, repaint);
  }

  internal int TerSetPictFrame2(int pict, int FrameType, int x, int y, bool repaint)
  {
    return this.blk.TerSetPictFrame2(pict, FrameType, x, y, repaint);
  }

  internal bool TerSetPictInfo(int pict, int style, int align, int AuxId)
  {
    return this.blk.TerSetPictInfo(pict, style, align, AuxId);
  }

  internal bool TerSetPictMapId(int PictId, int MapId) => this.link.TerSetPictMapId(PictId, MapId);

  internal bool TerSetPictOffset(int pict, int offset, bool repaint)
  {
    return this.blk.TerSetPictOffset(pict, offset, repaint);
  }

  internal bool TerSetPictPctWidth(int pict, int width)
  {
    return this.misc.TerSetPictPctWidth(pict, width);
  }

  internal bool TerSetPictSize(int pict, int width, int height)
  {
    return this.blk.TerSetPictSize(pict, width, height);
  }

  internal bool TerSetPreview(int pages, int zoom, bool ShowToolbar)
  {
    return this.prt.TerSetPreview(pages, zoom, ShowToolbar);
  }

  internal bool TerSetPrinter(PrinterSettings pt, PageSettings pg, bool ModDoc)
  {
    return this.prt.TerSetPrinter(pt, pg, ModDoc);
  }

  internal bool TerSetPrintPreview(bool begin) => this.prt.TerSetPrintPreview(begin);

  internal bool TerSetReadOnly(bool ReadOnly) => this.misc.TerSetReadOnly(ReadOnly);

  internal bool TerSetRowKeep(int CurCell, bool set, bool refresh)
  {
    return this.tbl.TerSetRowKeep(CurCell, set, refresh);
  }

  internal bool TerSetRowTextFlow(bool dialog, bool AllRows, int flow, bool refresh)
  {
    return this.tbl.TerSetRowTextFlow(dialog, AllRows, flow, refresh);
  }

  internal bool TerSetRtfDocInfo(int InfoType, string str)
  {
    return this.misc.TerSetRtfDocInfo(InfoType, str);
  }

  internal bool TerSetSearchString(string SearchFor, bool CaseSensitive)
  {
    return this.misc.TerSetSearchString(SearchFor, CaseSensitive);
  }

  internal bool TerSetSect(int NumCols, int ColSpace, bool StartPage)
  {
    return this.sec.TerSetSect(NumCols, ColSpace, StartPage);
  }

  internal bool TerSetSect2(
    int NumCols,
    int ColSpace,
    bool StartPage,
    int FirstPageNo,
    PaperSourceKind FirstPageBin,
    PaperSourceKind NextPageBin)
  {
    return this.sec.TerSetSect2(NumCols, ColSpace, StartPage, FirstPageNo, FirstPageBin, NextPageBin);
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
    return this.sec.TerSetSect3(NumCols, ColSpace, StartPage, FirstPageNo, SetBins, FirstPageBin, NextPageBin, sect, size);
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
    return this.sec.TerSetSect3(NumCols, ColSpace, StartPage, FirstPageNo, SetBins, FirstPageBin, NextPageBin, sect, size, IsPortrait);
  }

  internal bool TerSetSectAlign(int sect, int align, bool repaint)
  {
    return this.sec.TerSetSectAlign(sect, align, repaint);
  }

  internal bool TerSetSectBorder(
    int sect,
    int BorderType,
    int width,
    int space,
    Color color,
    bool repaint)
  {
    return this.sec.TerSetSectBorder(sect, BorderType, width, space, color, repaint);
  }

  internal bool TerSetSectEx(int NumCols, int ColSpace, bool StartPage, int FirstPageNo)
  {
    return this.sec.TerSetSectEx(NumCols, ColSpace, StartPage, FirstPageNo);
  }

  internal bool TerSetSectLineNbr(int sect, bool set, bool repaint)
  {
    return this.sec.TerSetSectLineNbr(sect, set, repaint);
  }

  internal bool TerSetSectLineNbr2(int sect, bool set, int step, bool repaint)
  {
    return this.sec.TerSetSectLineNbr2(sect, set, step, repaint);
  }

  internal bool TerSetSectOrient(bool IsPortrait, bool repaint)
  {
    return this.sec.TerSetSectOrient(IsPortrait, repaint);
  }

  internal bool TerSetSectPageSize(
    int sect,
    PaperKind size,
    int ParamWidth,
    int ParamHeight,
    bool repaint)
  {
    return this.sec.TerSetSectPageSize(sect, size, ParamWidth, ParamHeight, repaint);
  }

  internal bool TerSetSectTextFlow(int sect, int flow, bool repaint)
  {
    return this.sec.TerSetSectTextFlow(sect, flow, repaint);
  }

  internal bool TerSetSectVertAlign(int sect, int valign, bool repaint)
  {
    return this.sec.TerSetSectVertAlign(sect, valign, repaint);
  }

  internal bool TerSetStatusColor(Color TextColor, Color BkColor)
  {
    return this.draw.TerSetStatusColor(TextColor, BkColor);
  }

  internal static bool TerSetStLicenseKey(string key) => CSpl.TerSetStLicenseKey(key);

  internal bool TerSetStyleParam(int id, int type, int IntParam, string TextParam, bool repaint)
  {
    return this.par.TerSetStyleParam(id, type, IntParam, TextParam, repaint);
  }

  internal bool TerSetTab(int type, int pos, byte flags, bool repaint)
  {
    return this.par.TerSetTab(type, pos, flags, repaint);
  }

  internal bool TerSetTableColWidth(int width, bool repaint)
  {
    return this.tbl.TerSetTableColWidth(width, repaint);
  }

  internal bool TerSetTableId(int row, int id) => this.tbl.TerSetTableId(row, id);

  internal int TerSetTag(int line, int col, string name, string AuxText, int AuxInt, int flags)
  {
    return this.fnt.TerSetTag(line, col, name, AuxText, AuxInt, flags);
  }

  internal int TerSetTag(
    int line,
    int col,
    string name,
    string AuxText,
    int AuxInt,
    object obj,
    int flags)
  {
    return this.fnt.TerSetTag(line, col, name, AuxText, AuxInt, obj, flags);
  }

  internal bool TerSetTextCase(bool CaseType, bool repaint)
  {
    return this.fnt.TerSetTextCase(CaseType, repaint);
  }

  internal bool TerSetTextFieldInfo(
    int pict,
    string data,
    int MaxChars,
    int width,
    string typeface,
    int TwipsSize,
    int style)
  {
    return this.fld.TerSetTextFieldInfo(pict, data, MaxChars, width, typeface, TwipsSize, style);
  }

  internal bool TerSetUlineColor(bool dialog, Color color, bool repaint)
  {
    return this.fnt.TerSetUlineColor(dialog, color, repaint);
  }

  internal int TerSetUndoRef(int NewRef) => this.ud.TerSetUndoRef(NewRef);

  internal bool TerSetWatermarkPict(int id, bool wash, bool repaint)
  {
    return this.blk.TerSetWatermarkPict(id, wash, repaint);
  }

  internal bool TerSetWaveUnderline(int LineNo, int StartCol, int EndCol, bool set, bool repaint)
  {
    return this.fnt.TerSetWaveUnderline(LineNo, StartCol, EndCol, set, repaint);
  }

  internal bool TerSetWebFolder(string folder) => this.io.TerSetWebFolder(folder);

  internal bool TerSetWinBorder(int border, bool caption)
  {
    return this.misc.TerSetWinBorder(border, caption);
  }

  internal bool TerSetWrapWidth(int WidthChars, int WidthTwips, bool repaint)
  {
    return this.wrp.TerSetWrapWidth(WidthChars, WidthTwips, repaint);
  }

  internal int TerSetZoom(int percent) => this.misc.TerSetZoom(percent);

  internal bool TerShrinkFontTable() => this.fnt.TerShrinkFontTable();

  internal bool TerSpellCheck(bool StopAfterFirst, bool msg)
  {
    return this.spl.TerSpellCheck(StopAfterFirst, msg);
  }

  internal bool TerSpellCheck2(bool StopAfterFirst, bool msg, out bool Cancelled)
  {
    return this.spl.TerSpellCheck2(StopAfterFirst, msg, out Cancelled);
  }

  internal int TerTab2Spaces(int line, int col) => this.misc.TerTab2Spaces(line, col);

  internal bool TerTableOutlineBorder(int CurCell, int width, Color color, bool repaint)
  {
    return this.tbl.TerTableOutlineBorder(CurCell, width, color, repaint);
  }

  public bool TerTextPosToPix(int rel, int line, int col, out int pX, out int pY)
  {
    return this.pos.TerTextPosToPix(rel, line, col, out pX, out pY);
  }

  internal bool TerUpdateHyperlinkCode(string NewLinkCode)
  {
    return this.link.TerUpdateHyperlinkCode(NewLinkCode);
  }

  internal bool TerUpdateHyperlinkText(string NewLinkText, bool repaint)
  {
    return this.link.TerUpdateHyperlinkText(NewLinkText, repaint);
  }

  internal bool TerUpdateToolbar() => this.bar.TerUpdateToolbar();

  internal Control TerXlateControl(int pict) => this.fld.TerXlateControl(pict);

  internal int TerXlateControl(Control ctl) => this.fld.TerXlateControl(ctl);

  internal int TerXlateControlId(int id) => this.fld.TerXlateControlId(id);

  internal static uint ToUint32(int x) => (uint) x;

  internal void Validate()
  {
  }

  internal bool IsLong(IntPtr val) => (long) val >= 2415919103L /*0x8FFFFFFF*/;

  protected override void WndProc(ref Message msg)
  {
    if (!this.IsLong(msg.WParam))
    {
      if (!this.IsLong(msg.LParam))
      {
        int wparam;
        int lparam;
        int msg1;
        try
        {
          wparam = (int) msg.WParam;
          lparam = (int) msg.LParam;
          msg1 = msg.Msg;
        }
        catch (Exception ex)
        {
          base.WndProc(ref msg);
          return;
        }
        if (msg1 == 1)
        {
          base.WndProc(ref msg);
          return;
        }
        if (this.WindowBeingCreated || this.HoldMessages || this.WaitForOle || !this.TerArg.open)
        {
          base.WndProc(ref msg);
          return;
        }
        this.MessageId = msg1;
        int CmdId = msg1 != 2737 ? 0 : (int) COp.LOWORD(wparam);
        this.CommandId = CmdId;
        if (this.InPrintPreview)
        {
          bool flag = false;
          if (msg1 == 258 || msg1 == 563)
            flag = true;
          if (msg1 >= 512 /*0x0200*/ && msg1 <= 522)
            flag = true;
          if (flag)
          {
            base.WndProc(ref msg);
            return;
          }
          if (msg1 == 2737 && CmdId != 600 && CmdId != 601 && CmdId != 717)
            return;
        }
        if (CmdId != 0 && this.CurLine >= 0 && this.CurLine < this.TotalLines && (this.frm.LineTextAngle(this.CurLine) != 0 || (this.text[this.CurLine].flags2 & 32 /*0x20*/) != 0))
          CmdId = this.CommandId = this.misc.XlateCommandId(CmdId);
        if (this.misc.SendPreprocessMessage(msg1, wparam, lparam))
          return;
        tc.arg_list terArg1 = this.TerArg;
        tc.arg_list terArg2 = this.TerArg;
        this.CaretPositioned = false;
        if ((this.TerFlags4 & 1024 /*0x0400*/) != 0 && (msg1 == 2737 || msg1 == 258))
          this.DocHeight = this.blk.GetDocHeight();
        this.DoPostProcessing = true;
        this.DoDefWndProc = false;
        this.SendActionMsg = true;
        switch (msg1)
        {
          case 275:
            this.ctl.OnTimer(wparam, lparam);
            return;
          case 276:
            this.ctl.OnHScroll(wparam);
            break;
          case 277:
            this.ctl.OnVScroll(wparam);
            break;
          case 2737:
            this.mnu.ProcessCommand(CmdId);
            break;
          default:
            base.WndProc(ref msg);
            break;
        }
        if (this.DoDefWndProc)
        {
          this.DefWndProc(ref msg);
          return;
        }
        if (!this.TerArg.open || !this.DoPostProcessing || this.InPainting || msg1 != 258 && msg1 != 15 && msg1 != 2736 && msg1 != 1034 && msg1 != 277 && msg1 != 276 && (msg1 < 512 /*0x0200*/ || msg1 > 522) && msg1 != 563 && msg1 != 522)
          return;
        this.misc.TerPostProcessing(msg1, wparam, lparam);
        this.misc.SendActionMessage(msg1, wparam, lparam);
        return;
      }
    }
    base.WndProc(ref msg);
  }

  public bool CheckPlaneText()
  {
    if (this.TotalLines > 0 && this.text[0].len > 0)
    {
      int FontId = this.text[0].fmt != null ? (int) this.text[0].fmt[0] : (int) this.text[0].UniFmt;
      List<int> intList = new List<int>();
      int pfmt = this.text[0].pfmt;
      if ((this.PfmtId[pfmt].flags & 8) != 0)
        return false;
      for (int index1 = 0; index1 < this.TotalLines; ++index1)
      {
        if (this.text[index1].pfmt != pfmt || (this.PfmtId[this.text[index1].pfmt].flags & 8) != 0)
          return false;
        if (this.text[index1].fmt != null)
        {
          for (int index2 = 0; index2 < this.text[index1].len; ++index2)
          {
            if ((int) this.text[index1].fmt[index2] != FontId)
            {
              if (!ImDocumentEditorConfig.Instance.SpellCheck)
                return false;
              int index3 = (int) this.text[index1].fmt[index2];
              if (!intList.Contains(index3))
              {
                if (!this.fnt.FontsIsEqual(this.TerFont[FontId], this.TerFont[index3], true))
                  return false;
                intList.Add(index3);
              }
            }
          }
        }
        else if (FontId != (int) this.text[index1].UniFmt)
          return false;
        if (this.text[index1].tag != null)
        {
          for (int index4 = 0; index4 < this.text[index1].len; ++index4)
          {
            if (this.CharTag[(int) this.text[index1].tag[index4]].InUse && (this.CharTag[(int) this.text[index1].tag[index4]].type == 78 || this.CharTag[(int) this.text[index1].tag[index4]].type == 80 /*0x50*/ || this.CharTag[(int) this.text[index1].tag[index4]].type == 81))
              return false;
          }
        }
      }
      Color TextBackColor;
      this.TerGetTextColor(FontId, out Color _, out TextBackColor, out Color _);
      if (TextBackColor != Color.Transparent && TextBackColor != Color.White)
        return false;
    }
    return true;
  }

  /// <summary>Заменить шрифт по умолчанию. Пользоваться только в режиме CellRenderMode!</summary>
  /// <param name="fontID">Шрифт по умолчанию</param>
  internal void ReplaceDefaultFont(int fontID)
  {
    tc.StrFont strFont = this.TerFont[0];
    this.TerFont[0] = this.TerFont[fontID];
    this.TerFont[fontID] = strFont;
    tc.StrPrtFont strPrtFont = this.PrtFont[0];
    this.PrtFont[0] = this.PrtFont[fontID];
    this.PrtFont[fontID] = strPrtFont;
    Font font = this.TerFont[0].font;
    this.TerCurFont = font;
    this.TerRegFont = font;
    this.hTerCurFont = this.TerFont[0].hFont;
    this.TerArg.FontTypeFace = this.TerFont[0].TypeFace;
    this.TerArg.PointSize = this.TerFont[0].TwipsSize / 20;
    this.StyleId[0].TypeFace = this.TerFont[0].TypeFace;
    this.StyleId[0].TwipsSize = this.TerFont[0].TwipsSize;
    this.StyleId[0].style = this.TerFont[0].style;
    this.StyleId[0].TextColor = this.TerFont[0].TextColor;
    COp.TEXTMETRIC tm;
    if (!this.FullRenderMode && this.TerFont[0].TextMetric.HasValue)
    {
      tm = this.TerFont[0].TextMetric.Value;
    }
    else
    {
      this.fnt.GetTextMetrics(this.TerGr, this.TerRegFont, out tm);
      if (!this.FullRenderMode)
        this.TerFont[0].TextMetric = new COp.TEXTMETRIC?(tm);
    }
    this.TerTextMet = tm;
    this.draw.GetWinDimension();
    if (this.FullRenderMode)
      this.draw.DeleteTextMap(true);
    this.ToolBarCfmt = -1;
  }

  protected override void SetBoundsCore(
    int x,
    int y,
    int width,
    int height,
    BoundsSpecified specified)
  {
    if (this.UseWin)
    {
      base.SetBoundsCore(x, y, width, height, specified);
      this.clientSizeBuffer = new Size?();
    }
    else
      this.clientSizeBuffer = new Size?(new Size(width, height));
  }

  /// <summary>Приостановить разбивку по строкам</summary>
  /// <returns>Старое значение WordWrap</returns>
  internal bool SuspendWordWrap()
  {
    if (this.WordWrapSuspended)
      return true;
    this.WordWrapSuspended = true;
    return false;
  }

  /// <summary>Включить разбиение</summary>
  internal void ResumeWordWrap() => this.WordWrapSuspended = false;

  /// <summary>Установить параметры стиля в соответсвии со шрифтом</summary>
  /// <param name="styleID">Идентификатор стиля</param>
  /// <param name="fontID">Идентификатор шрифта</param>
  internal void SetStyleParamsFromFont(int styleID, int fontID)
  {
    this.par.SetStyleParamsFromFont(styleID, fontID);
  }

  /// <summary>
  /// Пропорционально изменить размер шрифта редактора или во всех шрифтах
  /// </summary>
  /// <param name="styleID">id шрифта или -1 для всех</param>
  /// <param name="scaleFactor">коэффициент</param>
  internal void ScaleEditorFontSize(int fontID, float scaleFactor)
  {
    this.par.ScaleEditorFontSize(fontID, scaleFactor);
  }

  /// <summary>Выделить весь текст</summary>
  /// <param name="repaint">Перерисовать</param>
  internal void SelectAll(bool repaint)
  {
    this.SelectTerText(0, 0, this.TotalLines - 1, this.text[this.TotalLines - 1].len - 1, repaint);
  }

  /// <summary>Удалить весь текст</summary>
  /// <param name="repaint">Перерисовать</param>
  /// <returns>true, если успешно</returns>
  internal bool TerDeleteAll(bool repaint) => this.blk.TerDeleteAll(repaint);

  internal bool HasImages()
  {
    for (int index = 0; index < this.TerFont.Length; ++index)
    {
      tc.StrFont strFont = this.TerFont[index];
      if (strFont.InUse && strFont.image != null)
        return true;
    }
    return false;
  }

  internal bool AllParagraphsSelected()
  {
    int StartLine;
    int EndLine;
    this.par.GetParaRange2(out StartLine, out EndLine);
    return EndLine - StartLine + 1 == this.TotalLines;
  }

  internal bool IsAllTextSelected()
  {
    int hilightBegCol = this.HilightBegCol;
    int hilightBegRow = this.HilightBegRow;
    int hilightEndCol = this.HilightEndCol;
    int hilightEndRow = this.HilightEndRow;
    int hilightType = this.HilightType;
    int curCol = this.CurCol;
    int curLine = this.CurLine;
    this.blk.NormalizeBlock(ref hilightBegRow, ref hilightBegCol, ref hilightEndRow, ref hilightEndCol, ref hilightType, ref curLine, ref curCol, false);
    return this.blk.AllSelected(hilightBegRow, hilightBegCol, hilightEndRow, hilightEndCol, hilightType);
  }

  internal SelectionBlock GetSelectionBlock()
  {
    int col1 = this.HilightBegCol;
    int col2 = this.HilightEndCol;
    if (this.HilightType == 1)
    {
      col1 = 0;
      col2 = this.text[this.HilightEndRow].len;
    }
    int abs1 = this.TerRowColToAbs(this.HilightBegRow, col1);
    int abs2 = this.TerRowColToAbs(this.HilightEndRow, col2);
    int abs3 = this.pos.RowColToAbs(this.CurLine, this.CurCol, true, false);
    int endPos = abs2;
    int curPos = abs3;
    int hilightType = this.HilightType;
    int num = this.PictureClicked ? 1 : 0;
    return new SelectionBlock(abs1, endPos, curPos, hilightType, num != 0, true);
  }

  internal void RestoreSelection(SelectionBlock block, bool repaint)
  {
    if (block != null)
    {
      if (block.CurPos >= 0)
      {
        int row;
        int col;
        this.pos.AbsToRowCol(block.CurPos, out row, out col, block.InternalEditorPos, false);
        this.pos.SetTerCursorPos(row, col, false);
      }
      this.HilightType = block.HilightType;
      this.PictureClicked = block.PictureClicked;
      this.pos.AbsToRowCol(block.StartPos, out this.HilightBegRow, out this.HilightBegCol, block.InternalEditorPos, false);
      this.pos.AbsToRowCol(block.EndPos, out this.HilightEndRow, out this.HilightEndCol, block.InternalEditorPos, false);
      if (!repaint)
        return;
      this.draw.PaintTer();
    }
    else
      this.DeselectTerText(repaint);
  }

  /// <summary>Выделить строки</summary>
  /// <param name="FirstLine">Первая строка</param>
  /// <param name="LastLine">Последняя строка</param>
  /// <param name="repaint">Перерисовать</param>
  /// <returns>true, если без ошибок</returns>
  internal bool SelectTerTextLines(int FirstLine, int LastLine, bool repaint)
  {
    return this.draw.SelectTerTextLines(FirstLine, LastLine, repaint);
  }

  /// <summary>Указанная позиция находится в защищенной зоне</summary>
  /// <param name="absPosition">Позиция</param>
  /// <param name="inclusive">Включая позицию перед блоком</param>
  /// <returns></returns>
  internal bool IsProtectedZone(int absPosition, bool inclusive)
  {
    return this.IsProtectedZone(absPosition, inclusive, out bool _);
  }

  /// <summary>Указанная позиция находится в защищенной зоне</summary>
  /// <param name="absPosition">Позиция</param>
  /// <param name="inclusive">Включая позицию перед блоком</param>
  /// <returns></returns>
  internal bool IsProtectedZone(int absPosition, bool inclusive, out bool zoneInBegin)
  {
    zoneInBegin = false;
    bool flag = false;
    if (this.ProtectedFirstRealCharCount > 0 && absPosition <= this.ProtectedFirstRealCharCount - 1)
    {
      flag = true;
      zoneInBegin = true;
    }
    else if (this.ProtectedEndRealCharCount > 0)
    {
      int num = this.GetTotalChars(false) - 2;
      flag = !inclusive ? absPosition > num - this.ProtectedEndRealCharCount : absPosition >= num - this.ProtectedEndRealCharCount;
    }
    return flag;
  }

  /// <summary>Текущая позиция в защищённой от редактирования зоне</summary>
  public bool CurPositionInProtectedZone
  {
    get => this.IsProtectedZone(this.pos.TerRowColToAbs(this.CurLine, this.CurCol), false);
  }

  /// <summary>Назначить тэги на все символы заданного диапазона</summary>
  /// <param name="startLine">Первая строка. Если значение меньше 0, то с текущей позиции</param>
  /// <param name="startCol">Первый столбец</param>
  /// <param name="endLine">Последняя строка</param>
  /// <param name="endCol">Последний столбец</param>
  /// <param name="type">Тип тэга</param>
  /// <param name="name">Имя тэга</param>
  /// <param name="AuxText">Дополнительный текст тэга</param>
  /// <param name="AuxInt">Дополнительное целое число тэга</param>
  /// <returns>Возвращает идентификатор тэга</returns>
  public int SetTextTags(
    int startLine,
    int startCol,
    int endLine,
    int endCol,
    int type,
    string name,
    string AuxText,
    int AuxInt)
  {
    int index1 = 0;
    if (startLine < 0)
    {
      startLine = this.CurLine;
      startLine = this.CurCol;
    }
    if (startLine > endLine || startLine == endLine && startCol > endCol || startLine < 0 || startLine >= this.TotalLines || endLine >= this.TotalLines || startCol < 0 || startCol > this.text[startLine].len || endCol < 0 || endCol > this.text[endLine].len)
      return 0;
    int num1 = startCol;
    for (int line = startLine; line <= endLine; ++line)
    {
      ushort[] numArray = this.fnt.OpenCtid(line);
      int num2;
      if (line == endLine)
      {
        num2 = endCol;
        if (num2 >= this.text[line].len)
          num2 = this.text[line].len - 1;
      }
      else
        num2 = this.text[line].len - 1;
      for (int index2 = num1; index2 <= num2; ++index2)
      {
        if (-1 == (index1 = this.fnt.GetTagSlot()))
          return 0;
        if (numArray[index2] != (ushort) 0)
        {
          int next = (int) numArray[index2];
          int num3 = next;
          while (this.CharTag[next].type != type || !(this.CharTag[next].name == name))
          {
            if (this.CharTag[next].next == 0)
            {
              if (next != index1)
              {
                this.CharTag[next].next = index1;
                goto label_25;
              }
              goto label_25;
            }
            if (this.CharTag[next].next == next)
            {
              this.CharTag[next].next = 0;
            }
            else
            {
              if (this.CharTag[next].next == num3)
              {
                this.CharTag[next].next = index1;
                goto label_25;
              }
              next = this.CharTag[next].next;
            }
          }
          index1 = next;
          this.fnt.FreeTag(index1);
        }
        else
          numArray[index2] = (ushort) index1;
label_25:
        this.CharTag[index1].InUse = true;
        this.CharTag[index1].type = type;
        this.CharTag[index1].name = name;
        this.CharTag[index1].AuxText = AuxText;
        this.CharTag[index1].AuxInt = AuxInt;
        if (this.CheckEndlessLoopTags(index1))
          this.CharTag[index1].next = 0;
      }
      num1 = 0;
      this.fnt.CloseCtid(line);
    }
    ++this.TerArg.modified;
    return index1;
  }

  /// <summary>Назначить тэги на все символы заданного диапазона</summary>
  /// <param name="startLine">Первая строка. Если значение меньше 0, то с текущей позиции</param>
  /// <param name="startCol">Первый столбец</param>
  /// <param name="count">Количество символов (абзац считается за 2!). Если значение -1, то до конца текста</param>
  /// <param name="type">Тип тэга</param>
  /// <param name="name">Имя тэга</param>
  /// <param name="AuxText">Дополнительный текст тэга</param>
  /// <param name="AuxInt">Дополнительное целое число тэга</param>
  /// <param name="expandReplacedChar">Проверять тэг TERTAG_REPLACEDCHAR и учитывать замену</param>
  /// <param name="realCharTagCount">Количество символов с учётом тэга TERTAG_REPLACEDCHAR и замены</param>
  /// <returns>Возвращает идентификатор тэга</returns>
  internal int SetTextTags(
    int startLine,
    int startCol,
    int count,
    int type,
    string name,
    string AuxText,
    int AuxInt,
    bool expandReplacedChar,
    out int realCharTagCount)
  {
    realCharTagCount = 0;
    if (count == 0)
      return 0;
    int index1 = 0;
    if (startLine < 0)
    {
      startLine = this.CurLine;
      startLine = this.CurCol;
    }
    if (startLine < 0 || startLine >= this.TotalLines || startCol < 0 || startCol > this.text[startLine].len)
      return 0;
    int num1 = startCol;
    int num2 = 0;
    bool flag1 = count < 0;
    if (flag1)
      count = int.MaxValue;
    bool flag2 = (this.TerFlags4 & 1) != 0;
    for (int index2 = startLine; num2 < count && index2 < this.TotalLines; ++index2)
    {
      ushort[] numArray = this.fnt.OpenCtid(index2);
      int index3;
      for (index3 = num1; num2 < count && index3 < this.text[index2].len; ++index3)
      {
        ++num2;
        ++realCharTagCount;
        if (-1 == (index1 = this.fnt.GetTagSlot()))
          return 0;
        if (numArray[index3] != (ushort) 0)
        {
          int next = (int) numArray[index3];
          int num3 = next;
          while (true)
          {
            if (this.CharTag[next].type == type && this.CharTag[next].name == name)
            {
              index1 = next;
              this.fnt.FreeTag(index1);
              if (!expandReplacedChar)
                goto label_31;
            }
            if (expandReplacedChar && (this.CharTag[next].type == 78 || this.CharTag[next].type == 79 || this.CharTag[next].type == 80 /*0x50*/))
            {
              if (this.CharTag[next].AuxText != null && this.CharTag[next].AuxText != "")
                num2 += this.CharTag[next].AuxText.Length - 1;
              else
                --num2;
              if (flag2 && index3 == this.text[index2].len - 1 && ((this.text[index2].flags & 1966209) != 0 || this.text[index2].len == 1 && this.pos.LineInfo(index2, 32 /*0x20*/)))
                --num2;
            }
            if (this.CharTag[next].next != 0)
            {
              if (this.CharTag[next].next == next)
                this.CharTag[next].next = 0;
              else if (this.CharTag[next].next != num3)
                next = this.CharTag[next].next;
              else
                goto label_28;
            }
            else
              break;
          }
          if (next != index1)
          {
            this.CharTag[next].next = index1;
            goto label_31;
          }
          goto label_31;
label_28:
          this.CharTag[next].next = index1;
        }
        else
          numArray[index3] = (ushort) index1;
label_31:
        this.CharTag[index1].InUse = true;
        this.CharTag[index1].type = type;
        this.CharTag[index1].name = name;
        this.CharTag[index1].AuxText = AuxText;
        this.CharTag[index1].AuxInt = AuxInt;
        if (this.CheckEndlessLoopTags(index1))
          this.CharTag[index1].next = 0;
      }
      if (index3 == this.text[index2].len)
      {
        if ((this.text[index2].flags2 & 512 /*0x0200*/) != 0 && this.text[index2].txt[index3] == '\u0006')
        {
          --num2;
          --realCharTagCount;
        }
        if (flag1 || num2 < count && index2 + 1 < this.TotalLines)
        {
          if (flag2 && ((this.text[index2].flags & 1966209) != 0 || this.text[index2].len == 1 && this.pos.LineInfo(index2, 32 /*0x20*/)))
          {
            ++num2;
            ++realCharTagCount;
          }
          if (!this.TerArg.WordWrap)
          {
            num2 += 2;
            realCharTagCount += 2;
          }
        }
      }
      num1 = 0;
      this.fnt.CloseCtid(index2);
    }
    ++this.TerArg.modified;
    return index1;
  }

  /// <summary>Есть ли в тексте заданые тэг</summary>
  /// <param name="type">Тип тэга</param>
  /// <param name="name">Имя тэга</param>
  /// <returns></returns>
  internal bool HasTextTags(int type, string name)
  {
    if (type == -1 || name == null || name == "")
      return false;
    for (int index1 = 0; index1 <= this.TotalLines; ++index1)
    {
      ushort[] tag = this.text[index1].tag;
      if (tag != null)
      {
        for (int index2 = 0; index2 < this.text[index1].len && index2 < tag.Length; ++index2)
        {
          if (tag[index2] != (ushort) 0)
          {
            int index3 = 0;
            while (this.CharTag[index3].type != type || !(this.CharTag[index3].name == name))
            {
              index3 = this.CharTag[index3].next;
              if (index3 == 0)
                goto label_11;
            }
            return true;
          }
          continue;
label_11:;
        }
      }
    }
    return false;
  }

  /// <summary>Удалить тэги со всех символов заданного диапазона</summary>
  /// <param name="startLine">Первая строка. Если значение меньше 0, то с текущей позиции</param>
  /// <param name="startCol">Первый столбец</param>
  /// <param name="endLine">Последняя строка</param>
  /// <param name="endCol">Последний столбец</param>
  /// <param name="type">Тип тэга</param>
  internal void DeleteTextTags(
    int startLine,
    int startCol,
    int endLine,
    int endCol,
    int type,
    string name)
  {
    if (startLine < 0)
    {
      startLine = this.CurLine;
      startLine = this.CurCol;
    }
    if (startLine > endLine || startLine == endLine && startCol > endCol || startLine < 0 || startLine >= this.TotalLines || endLine >= this.TotalLines || startCol < 0 || startCol > this.text[startLine].len || endCol < 0 || endCol > this.text[endLine].len)
      return;
    int num1 = startCol;
    for (int index1 = startLine; index1 <= endLine; ++index1)
    {
      ushort[] tag = this.text[index1].tag;
      if (tag != null)
      {
        int num2;
        if (index1 == endLine)
        {
          num2 = endCol;
          if (num2 >= this.text[index1].len)
            num2 = this.text[index1].len - 1;
        }
        else
          num2 = this.text[index1].len - 1;
        for (int index2 = num1; index2 <= num2 && index2 < tag.Length; ++index2)
        {
          if (tag[index2] != (ushort) 0)
          {
            int TagId = (int) tag[index2];
            int index3 = 0;
            do
            {
              if ((type == -1 || this.CharTag[TagId].type == type) && (name == null || this.CharTag[TagId].name == name))
              {
                int next = this.CharTag[TagId].next;
                if (index3 == 0)
                  tag[index2] = (ushort) next;
                else
                  this.CharTag[index3].next = next;
                this.fnt.FreeTag(TagId);
                TagId = next;
              }
              else
              {
                index3 = TagId;
                TagId = this.CharTag[TagId].next;
              }
            }
            while (TagId != 0);
          }
        }
        num1 = 0;
      }
    }
    ++this.TerArg.modified;
  }

  /// <summary>Удалить тэги со всех символов заданного диапазона</summary>
  /// <param name="startLine">Первая строка. Если значение меньше 0, то с текущей позиции</param>
  /// <param name="startCol">Первый столбец</param>
  /// <param name="count">Количество символов (абзац считается за 2!). Если значение -1, то до конца текста</param>
  /// <param name="type">Тип тэга</param>
  /// <param name="expandReplacedChar">Проверять тэг TERTAG_REPLACEDCHAR и учитывать замену</param>
  internal void DeleteTextTags(
    int startLine,
    int startCol,
    int count,
    int type,
    string name,
    bool expandReplacedChar)
  {
    if (count == 0)
      return;
    if (startLine < 0)
    {
      startLine = this.CurLine;
      startLine = this.CurCol;
    }
    if (startLine < 0 || startLine >= this.TotalLines || startCol < 0 || startCol > this.text[startLine].len)
      return;
    int num1 = startCol;
    int num2 = 0;
    if (count < 0)
      count = int.MaxValue;
    for (int index1 = startLine; num2 < count && index1 < this.TotalLines; ++index1)
    {
      ushort[] tag = this.text[index1].tag;
      if (tag != null)
      {
        for (int index2 = num1; num2 < count && index2 < this.text[index1].len && index2 < tag.Length; ++index2)
        {
          ++num2;
          if (tag[index2] != (ushort) 0)
          {
            int TagId = (int) tag[index2];
            int index3 = 0;
            do
            {
              if ((type == -1 || this.CharTag[TagId].type == type) && (name == null || this.CharTag[TagId].name == name))
              {
                int next = this.CharTag[TagId].next;
                if (index3 == 0)
                  tag[index2] = (ushort) next;
                else
                  this.CharTag[index3].next = next;
                this.fnt.FreeTag(TagId);
                TagId = next;
              }
              else
              {
                if (expandReplacedChar && (this.CharTag[TagId].type == 78 || this.CharTag[TagId].type == 79 || this.CharTag[TagId].type == 80 /*0x50*/))
                {
                  if (this.CharTag[TagId].AuxText != null)
                    num2 += this.CharTag[TagId].AuxText.Length - 1;
                  else
                    --num2;
                }
                index3 = TagId;
                TagId = this.CharTag[TagId].next;
              }
            }
            while (TagId != 0);
          }
        }
        num1 = 0;
      }
    }
    ++this.TerArg.modified;
  }

  /// <summary>Проверить, назначен ли тэг данного типа на позицию текста</summary>
  /// <param name="line">Строка</param>
  /// <param name="col">Столбец</param>
  /// <param name="type">Тип тэга</param>
  /// <returns></returns>
  internal bool CheckTextTag(int line, int col, int type)
  {
    if (line < 0 || line >= this.TotalLines || col < 0 || col >= this.text[line].len || this.text[line].tag == null)
      return false;
    for (int next = (int) this.text[line].tag[col]; next != 0; next = this.CharTag[next].next)
    {
      if (this.CharTag[next].type == type)
        return true;
    }
    return false;
  }

  /// <summary>Проверить, назначен ли тэг данного типа на позицию текста</summary>
  /// <param name="line">Строка</param>
  /// <param name="col">Столбец</param>
  /// <param name="types">Типы тэгов</param>
  /// <returns></returns>
  internal bool CheckTextTag(int line, int col, IList<int> types)
  {
    if (line < 0 || line >= this.TotalLines || col < 0 || col >= this.text[line].len || this.text[line].tag == null)
      return false;
    for (int next = (int) this.text[line].tag[col]; next != 0; next = this.CharTag[next].next)
    {
      if (types.Contains(this.CharTag[next].type))
        return true;
    }
    return false;
  }

  /// <summary>Получить тэг данного типа для данной позиции текста</summary>
  /// <param name="line">Строка, если -1, то берётся текущая позиция редактора</param>
  /// <param name="col">Столбец</param>
  /// <param name="type">Тип тэга</param>
  /// <returns>Возвращает id тэга, если он есть, или -1 если тэга заданного типа нет</returns>
  public int GetTextTag(int line, int col, int type)
  {
    if (line < 0)
    {
      line = this.CurLine;
      col = this.CurCol;
    }
    if (line < 0 || line >= this.TotalLines || col < 0 || col >= this.text[line].len || this.text[line].tag == null)
      return -1;
    for (int next = (int) this.text[line].tag[col]; next != 0; next = this.CharTag[next].next)
    {
      if (this.CharTag[next].type == type)
        return next;
    }
    return -1;
  }

  /// <summary>Получить тэг из данного списка типов для данной позиции текста</summary>
  /// <param name="line">Строка, если -1, то берётся текущая позиция редактора</param>
  /// <param name="col">Столбец</param>
  /// <param name="types">Типы тэгов</param>
  /// <returns>Возвращает id тэга, если он есть, или -1 если тэга заданного типа нет</returns>
  public int GetTextTag(int line, int col, IList<int> types)
  {
    if (line < 0)
    {
      line = this.CurLine;
      col = this.CurCol;
    }
    if (line < 0 || line >= this.TotalLines || col < 0 || col >= this.text[line].len || this.text[line].tag == null)
      return -1;
    for (int next = (int) this.text[line].tag[col]; next != 0; next = this.CharTag[next].next)
    {
      if (types.Contains(this.CharTag[next].type))
        return next;
    }
    return -1;
  }

  public string ExtractStringFromTag(IList<int> tags, int line = -1, int col = -1)
  {
    int textTag = this.GetTextTag(line, col, tags);
    return textTag != -1 ? this.CharTag[textTag].AuxText : (string) null;
  }

  public string ExtractStringFromTag(int tag, int line = -1, int col = -1)
  {
    int textTag = this.GetTextTag(line, col, tag);
    return textTag != -1 ? this.CharTag[textTag].AuxText : (string) null;
  }

  /// <summary>Вспомогательный метод для отлова зацикленных ссылок в тагах</summary>
  /// <param name="tag"></param>
  /// <returns></returns>
  internal bool CheckEndlessLoopTags(int tag) => false;

  /// <summary>Получить общее количество символов в документе</summary>
  /// <returns></returns>
  internal int GetTotalChars(bool internalPos = true)
  {
    if (this.TotalLines == 0)
      return 0;
    int num1 = 0;
    int totalChars = 0;
    bool flag = (this.TerFlags4 & 1) != 0;
    if (!this.TerArg.WordWrap)
      num1 = 2;
    for (int LineNo = 0; LineNo < this.TotalLines; ++LineNo)
    {
      if (this.text[LineNo] != null)
      {
        int len = this.text[LineNo].len;
        if ((this.text[LineNo].flags2 & 512 /*0x0200*/) != 0 && len > 0 && this.text[LineNo].txt[len - 1] == '\u0006')
          --len;
        int num2 = 0;
        if (!internalPos && this.text[LineNo].tag != null && this.text[LineNo].tag.Length != 0)
        {
          for (int index1 = 0; index1 < this.text[LineNo].len; ++index1)
          {
            int index2 = (int) this.text[LineNo].tag[index1];
            if (index2 != 0 && (this.CharTag[index2].type == 78 || this.CharTag[index2].type == 79 || this.CharTag[index2].type == 80 /*0x50*/))
            {
              string auxText = this.CharTag[index2].AuxText;
              if (!string.IsNullOrEmpty(auxText))
              {
                num2 += auxText.Length - 1;
              }
              else
              {
                --num2;
                if (flag && this.text[LineNo].txt[index1] == '\u0015')
                  --num2;
              }
            }
          }
        }
        int num3 = len + num2;
        totalChars = totalChars + num3 + num1;
        if (flag && ((this.text[LineNo].flags & 1966209) != 0 || this.text[LineNo].len == 1 && this.pos.LineInfo(LineNo, 32 /*0x20*/)))
          ++totalChars;
      }
    }
    return totalChars;
  }

  /// <summary>Удалить лишний перевод строки в конце текста</summary>
  /// <param name="txt">Простой текст (не RTF!)</param>
  /// <returns>Подчищенный текст</returns>
  internal static string DeleteLastEndLine(string txt)
  {
    if (string.IsNullOrEmpty(txt))
      return txt;
    if (txt[txt.Length - 1] == '\r' || txt[txt.Length - 1] == '\n')
      txt = txt.Length <= 1 || txt[txt.Length - 2] != '\r' && txt[txt.Length - 2] != '\n' ? txt.Remove(txt.Length - 1, 1) : txt.Remove(txt.Length - 2, 2);
    else if (txt[txt.Length - 1] == '\u0015')
      txt = txt.Remove(txt.Length - 1, 1);
    return txt;
  }

  /// <summary>Удалить лишний перевод строки в конце текста</summary>
  /// <param name="">Текст</param>
  /// <returns>Подчищенный текст</returns>
  internal static string DeleteLastEndLineInRTF(string rtf)
  {
    if (string.IsNullOrEmpty(rtf))
      return rtf;
    string str = "\\par}";
    int startIndex = rtf.LastIndexOf(str);
    if (startIndex == rtf.Length - str.Length - 1)
      rtf = rtf.Remove(startIndex, str.Length - 1);
    return rtf;
  }

  public string GetShortRtf()
  {
    bool shortRtf1 = this.ShortRtf;
    this.ShortRtf = true;
    bool rtfOutput = this.RTFOutput;
    this.RTFOutput = true;
    string shortRtf2 = ImRtfEditor.DeleteLastEndLine(this.io.GetTerBuffer());
    this.ShortRtf = shortRtf1;
    this.RTFOutput = rtfOutput;
    return shortRtf2;
  }

  /// <summary>Текст редактора с форматированием в формате RTF</summary>
  public string RtfText
  {
    [DebuggerStepThrough] get
    {
      bool rtfOutput = this.RTFOutput;
      if (!rtfOutput)
        this.RTFOutput = true;
      string terBuffer = this.io.GetTerBuffer();
      if (rtfOutput)
        return terBuffer;
      this.RTFOutput = rtfOutput;
      return terBuffer;
    }
    set
    {
      bool rtfOutput = this.RTFOutput;
      if (!rtfOutput)
        this.RTFOutput = true;
      this.SetTerBuffer(value, (string) null);
      if (rtfOutput)
        return;
      this.RTFOutput = rtfOutput;
    }
  }

  /// <summary>Текст редактора без форматирования</summary>
  public string PlaneText
  {
    get
    {
      bool rtfOutput = this.RTFOutput;
      if (rtfOutput)
        this.RTFOutput = false;
      string planeText = ImRtfEditor.DeleteLastEndLine(this.io.GetTerBuffer());
      if (!rtfOutput)
        return planeText;
      this.RTFOutput = rtfOutput;
      return planeText;
    }
  }

  internal void PaintOnGraphics(Graphics gr, Rectangle clipRect)
  {
    Graphics terGr = this.TerGr;
    this.ClipRect = this.draw.ToRect(clipRect);
    this._grVisibleClipBounds = gr.VisibleClipBounds;
    this.OnWmPaint = true;
    this.draw.ApplyNewGr(gr);
    this.TerGr = gr;
    this.RepaintRuler = true;
    this.TerOpFlags |= 128 /*0x80*/;
    if (this.FullRenderMode && !this.InPrintPreview)
      this.CurClipRgn = new Region(clipRect);
    this.draw.PaintTer();
    this.TerOpFlags &= -2177;
    this.draw.ApplyNewGr(terGr);
    this.TerGr = terGr;
  }

  /// <summary>Получить последную строку страницы</summary>
  /// <param name="page">Номер страницы</param>
  /// <returns>Номер последней строки страницы</returns>
  internal int GetPageLastLine(int page)
  {
    int num = this.TerGetParam(5);
    return page < num - 1 ? this.TerGetPageFirstLine(page + 1) - 1 : this.TerGetParam(1) - 1;
  }

  /// <summary>Получить ширину (twips) текста на странице</summary>
  /// <param name="page">Номер страницы. Если -1, то для всего текста</param>
  /// <returns></returns>
  internal int GetTextWidth(int page)
  {
    if (page >= this.TotalPages)
      return 0;
    int textWidth = 0;
    int num1 = 0;
    int num2 = this.TotalLines;
    if (page != -1)
    {
      num1 = this.PageInfo[page].FirstLine;
      num2 = this.PageInfo[page].LastLine + 1;
    }
    for (int lin = num1; lin < num2; ++lin)
    {
      int lineWidth = this.pos.GetLineWidth(lin, page == -1, true, true);
      if (page != -1)
      {
        if (textWidth < lineWidth)
          textWidth = lineWidth;
      }
      else
        textWidth += lineWidth;
    }
    return textWidth;
  }

  internal int GetTextHeight()
  {
    int textHeight = 0;
    for (int lin = 0; lin < this.TotalLines; ++lin)
      textHeight += this.fld.OrigScrToTwipsY(this.pos.GetLineHeight(lin, out int _, out int _));
    return textHeight;
  }

  internal void MergePrint2(ref tc.StrPrint param) => this.prt.TerMergePrint2(ref param);

  internal void MergePrint3(Graphics gr, Rectangle rect, string rtf)
  {
  }

  internal void ImPrintOnePage(Graphics gr, Rectangle rect)
  {
    GraphicsState gstate = gr.Save();
    gr.PageUnit = GraphicsUnit.Pixel;
    if (this.AllTextAngle2 != 0)
      rect = CDraw.RotateRectangle(rect, this.AllTextAngle2);
    this.PvX = Convert.ToInt32((float) ((double) rect.Left * (double) gr.DpiX / 1440.0));
    this.PvY = Convert.ToInt32((float) ((double) rect.Top * (double) gr.DpiY / 1440.0));
    this.PvExtWidth = rect.Width;
    this.PvExtHeight = rect.Height;
    this.PvVpWidth = Convert.ToInt32((float) ((double) rect.Width * (double) gr.DpiX / 1440.0));
    float num1 = this.PvVpWidth != 0 ? (float) this.PvVpWidth : (float) ((double) rect.Width * (double) gr.DpiX / 1440.0);
    this.PvVpHeight = Convert.ToInt32((float) ((double) rect.Height * (double) gr.DpiY / 1440.0));
    double num2 = this.PvVpHeight != 0 ? (double) this.PvVpHeight : (double) rect.Height * (double) gr.DpiY / 1440.0;
    this.draw.ResetTransform(gr);
    float sx = num1 / (float) this.PvExtWidth;
    double pvExtHeight = (double) this.PvExtHeight;
    float sy = (float) (num2 / pvExtHeight);
    gr.ScaleTransform(sx, sy, System.Drawing.Drawing2D.MatrixOrder.Append);
    float dx = (float) (this.PvX - this.prt.MulDiv(this.TerWinOrgX, this.PvVpWidth, this.PvExtWidth));
    float dy = (float) (this.PvY - this.prt.MulDiv(this.TerWinOrgY, this.PvVpHeight, this.PvExtHeight));
    gr.TranslateTransform(dx, dy, System.Drawing.Drawing2D.MatrixOrder.Append);
    Graphics prtGr = this.PrtGr;
    this.PrtGr = gr;
    this.prt.PrintViewMode(gr, -1, 0);
    if (prtGr != null)
      this.PrtGr = prtGr;
    gr.Restore(gstate);
  }

  internal void ImPrintPreview(Graphics gr, Rectangle rect)
  {
    if ((!this.ExtPrintPreview || this.TerIsPrinting()) && !this.prt.PrePreview(gr))
      return;
    if (this.ExtPrintPreview)
    {
      this.prt.PostPreview();
      this.prt.PrePreview(gr);
    }
    this.ImPrintOnePage(gr, rect);
    if (this.ExtPrintPreview)
      return;
    this.prt.PostPreview();
  }

  /// <summary>Событие возникает при сохранение в Undo информации</summary>
  public event EventHandler UndoSaved;

  /// <summary>Обработка события UndoSaved</summary>
  internal void OnUndoSaved(EventArgs e)
  {
    EventHandler undoSaved = this.UndoSaved;
    if (undoSaved == null)
      return;
    undoSaved((object) this, e);
  }

  public IEnumerator<char> GetEnumerator()
  {
    return (IEnumerator<char>) new ImRtfEditorTextEnumerator(this);
  }

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

  public override Color BackColor
  {
    get => this.TextDefBkColor;
    set
    {
      this.TextDefBkColor = this.TextBorderColor = value;
      if (!this.Created)
        return;
      ++this.TerArg.modified;
      this.SendMessageToParent(2725, (int) this.hTerWnd, 0, false);
      this.TerRepaint(true);
    }
  }

  [Category("Editing")]
  [Description("Display white border around the text area")]
  internal bool BorderMargin
  {
    get => this.DesignMode ? this.InitArg.BorderMargin : this.TerArg.BorderMargin;
    set
    {
      this.InitArg.BorderMargin = value;
      if (this.TerArg.BorderMargin == value || !this.Created)
        return;
      this.TerArg.BorderMargin = value;
      this.Invalidate(true);
    }
  }

  [Description("Execute a command")]
  [Category("Runtime Properties")]
  internal int Command
  {
    get => 0;
    set
    {
      if (this.DesignMode || !this.Created && (int) this.hTerWnd == 0)
        return;
      this.TerCommand(value);
    }
  }

  [Description("Document data")]
  [Category("Runtime Properties")]
  internal string Data
  {
    get => !this.DesignMode && (this.Created || (int) this.hTerWnd != 0) ? this.GetTerBuffer() : "";
    set
    {
      if (this.DesignMode)
        return;
      if (value == null)
        value = "";
      if (this.Created || (int) this.hTerWnd != 0)
      {
        this.SetTerBuffer(value, (string) null);
      }
      else
      {
        this.InitArg.hBuffer = value;
        this.InitArg.BufferLen = this.InitArg.hBuffer.Length;
      }
    }
  }

  [Description("SpellTime Dictionary Directory")]
  [Category("Editing")]
  internal string DictPath
  {
    get => this.StDictDir;
    set => this.StDictDir = value;
  }

  [Description("Cleanup controls created with editor class")]
  [Category("Editing")]
  internal bool DoClassCleanup
  {
    get => tc.DoClassCleanup;
    set => tc.DoClassCleanup = value;
  }

  [Category("Editing")]
  [Description("Display in the browser mode")]
  internal bool FittedView
  {
    get => this.DesignMode ? this.InitArg.FittedView : this.TerArg.FittedView;
    set
    {
      if (this.Created && !this.InitPropMsgShown && this.InitArg.FittedView != value && !this.DesignMode)
      {
        this.misc.PrintError(202, nameof (FittedView));
        this.InitPropMsgShown = true;
      }
      this.InitArg.FittedView = value;
    }
  }

  [Category("Editing")]
  [Description("Display horizontal scroll bar")]
  internal bool HorzScrollBar
  {
    get => this.DesignMode ? this.InitArg.ShowHorBar : this.TerArg.ShowHorBar;
    set
    {
      if (this.Created && !this.InitPropMsgShown && this.InitArg.ShowHorBar != value && !this.DesignMode)
      {
        this.misc.PrintError(202, nameof (HorzScrollBar));
        this.InitPropMsgShown = true;
      }
      this.InitArg.ShowHorBar = value;
    }
  }

  [Category("Editing")]
  [Description("HTML Add-on product license key")]
  internal string HtmlAddOnKey
  {
    get => tc.HtnLicenseKey;
    set => tc.HtnLicenseKey = value;
  }

  [Description("Control to be hosted in a server app")]
  [Category("Editing")]
  internal bool InServer
  {
    get => tc.InServer;
    set => tc.InServer = value;
  }

  [Category("Editing")]
  [Description("Control to be hosted inside IE")]
  internal bool InWebPage
  {
    get => tc.InIE;
    set => tc.InIE = value;
  }

  [Category("Editing")]
  [Description("Display wysiwyg page layout")]
  internal bool PageMode
  {
    get => this.DesignMode ? this.InitArg.PageMode : this.TerArg.PageMode;
    set
    {
      if (this.Created && !this.InitPropMsgShown && this.InitArg.PageMode != value && !this.DesignMode)
      {
        this.misc.PrintError(202, nameof (PageMode));
        this.InitPropMsgShown = true;
      }
      this.InitArg.PageMode = value;
    }
  }

  [Description("Display page breaks")]
  [Category("Editing")]
  internal bool PrintViewMode
  {
    get => this.DesignMode ? this.InitArg.PrintView : this.TerArg.PrintView;
    set
    {
      if (this.Created && !this.InitPropMsgShown && this.InitArg.PrintView != value && !this.DesignMode)
      {
        this.misc.PrintError(202, "PrintView");
        this.InitPropMsgShown = true;
      }
      this.InitArg.PrintView = value;
    }
  }

  [Description("Read-Only mode")]
  [Category("Editing")]
  public bool ReadOnlyMode
  {
    get => this.DesignMode ? this.InitArg.ReadOnly : this.TerArg.ReadOnly;
    set
    {
      this.InitArg.ReadOnly = value;
      if (value == this.TerArg.ReadOnly || !this.Created)
        return;
      this.TerSetReadOnly(value);
    }
  }

  [Description("Save the file in the RTF format")]
  [Category("Editing")]
  internal bool RTFOutput
  {
    get => this.DesignMode ? this.InitArg.SaveFormat == 2 : this.TerArg.SaveFormat == 2;
    set => this.InitArg.SaveFormat = this.TerArg.SaveFormat = value ? 2 : 0;
  }

  [Description("Show ruler")]
  [Category("Editing")]
  internal bool ShowRuler
  {
    get => this.DesignMode ? this.InitArg.ruler : this.TerArg.ruler;
    set
    {
      this.InitArg.ruler = value;
      if (value == this.TerArg.ruler || !this.Created)
        return;
      this.TerCommand(680);
    }
  }

  [Description("Show status bar")]
  [Category("Editing")]
  internal bool ShowStatusBar
  {
    get => this.DesignMode ? this.InitArg.ShowStatus : this.TerArg.ShowStatus;
    set
    {
      this.InitArg.ShowStatus = value;
      if (value == this.TerArg.ShowStatus || !this.Created)
        return;
      this.TerCommand(682);
    }
  }

  [Category("Editing")]
  [Description("Show toolbar")]
  internal bool ShowToolBar
  {
    get => this.DesignMode ? this.InitArg.ToolBar : this.TerArg.ToolBar;
    set
    {
      this.InitArg.ToolBar = value;
      if (value == this.TerArg.ToolBar || !this.Created)
        return;
      this.TerCommand(681);
    }
  }

  [Description("SpellTime product license key")]
  [Category("Editing")]
  internal string SpellTimeKey
  {
    get => tc.StnKey;
    set => tc.StnKey = value;
  }

  [Category("Editing")]
  [Description("Editor license key")]
  internal string TernKey
  {
    get => tc.UserKey;
    set
    {
      tc.UserKey = value;
      if (this.DesignMode || !this.Created)
        return;
      ImRtfEditor.TerSetLicenseKey(value);
    }
  }

  [Category("UseWindow")]
  [Description("Create window to display text")]
  internal bool UseWindow
  {
    get => this.UseWin;
    set => this.UseWin = value;
  }

  [Description("Display vertical scroll bar")]
  [Category("Editing")]
  internal bool VertScrollBar
  {
    get => this.DesignMode ? this.InitArg.ShowVerBar : this.TerArg.ShowVerBar;
    set
    {
      if (this.Created && !this.InitPropMsgShown && this.InitArg.ShowVerBar != value && !this.DesignMode)
      {
        this.misc.PrintError(202, nameof (VertScrollBar));
        this.InitPropMsgShown = true;
      }
      this.InitArg.ShowVerBar = value;
    }
  }

  [Description("Wrap text lines")]
  [Category("Editing")]
  internal bool WordWrap
  {
    get => this.DesignMode ? this.InitArg.WordWrap : this.TerArg.WordWrap;
    set
    {
      if (this.Created && !this.InitPropMsgShown && this.InitArg.WordWrap != value && !this.DesignMode)
      {
        this.misc.PrintError(202, nameof (WordWrap));
        this.InitPropMsgShown = true;
      }
      this.InitArg.WordWrap = value;
    }
  }

  public int TotalLines
  {
    [DebuggerStepThrough] get => this.totalLines;
    set
    {
      if (this.totalLines == value)
        return;
      this.totalLines = value;
      if (this.totalLines <= 1)
        return;
      this.totalLines = value;
    }
  }

  public int VertAlignment { get; internal set; }

  public int TextOrientation { get; internal set; }

  internal delegate void EventAction(object Sender, int ActionType, int ActionId);

  internal delegate void EventClosing(object Sender);

  internal delegate void EventHypertext(object Sender, ref tc.StrHyperlink link);

  internal delegate bool EventMergeData(object Sender, string name, out string data);

  internal delegate void EventModified(object Sender);

  internal delegate void EventPageCount(object Sender);

  internal delegate void EventPageSizeChanging(object Sender, ref int NewPageSize);

  internal delegate void EventPostPaint(object Sender, Graphics gr);

  internal delegate void EventPreprocess(object Sender, int ActionType, int ActionId);

  internal delegate void EventSpellWordReplaced(
    object Sender,
    int CharPos,
    string PrevWord,
    string NewWord);

  internal delegate void EventUpdateStatusbar(object Sender);

  internal delegate void EventUpdateToolbar(object Sender);
}
