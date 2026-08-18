// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.tc
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

public class tc
{
  internal const int BOLD = 2;
  internal const int ULINE = 1;
  internal const int ULINED = 256 /*0x0100*/;
  internal const int ITALIC = 4;
  internal const int STRIKE = 8;
  internal const int DOUBLE_STRIKE = 524288 /*0x080000*/;
  internal const int SUBSCR = 32 /*0x20*/;
  internal const int SUPSCR = 16 /*0x10*/;
  internal const int HIDDEN = 64 /*0x40*/;
  internal const int PROTECT = 512 /*0x0200*/;
  internal const int CAPS = 65536 /*0x010000*/;
  internal const int SCAPS = 131072 /*0x020000*/;
  internal const int FNOTE = 1024 /*0x0400*/;
  internal const int FNOTEALL = 39936;
  internal const int FNOTENONE = -39937;
  internal const int FNOTEREST = 4096 /*0x1000*/;
  internal const int FNOTETEXT = 2048 /*0x0800*/;
  internal const int ENOTETEXT = 32768 /*0x8000*/;
  internal const int ACTION_CHAR = 4;
  internal const int ACTION_COMMAND = 1;
  internal const int ACTION_HSCROLL = 3;
  internal const int ACTION_KILLFOCUS = 13;
  internal const int ACTION_LBUTTONDBLCLK = 9;
  internal const int ACTION_LBUTTONDOWN = 5;
  internal const int ACTION_LBUTTONUP = 7;
  internal const int ACTION_MOUSEMOVE = 14;
  internal const int ACTION_RBUTTONDBLCLK = 10;
  internal const int ACTION_RBUTTONDOWN = 6;
  internal const int ACTION_RBUTTONUP = 8;
  internal const int ACTION_SETFOCUS = 12;
  internal const int ACTION_SIZE = 11;
  internal const int ACTION_STYLE = 15;
  internal const int ACTION_VSCROLL = 2;
  internal const int ALIGN_BOT = 0;
  internal const int ALIGN_MIDDLE = 1;
  internal const int ALIGN_TOP = 2;
  internal const int ANIM_CONTINUOUS = -2;
  internal const byte ANSI_CHARSET = 0;
  internal const int APPLY_AFT_CUR_LINE = 3;
  internal const int APPLY_BEF_CUR_LINE = 2;
  internal const int APPLY_IGNORE = 0;
  internal const int APPLY_MOD_CUR_LINE = 1;
  internal const int AUTO_SPACE = 270;
  internal const int AUTO_SPACE_AFTER = 262144 /*0x040000*/;
  internal const int AUTO_SPACE_BEFORE = 131072 /*0x020000*/;
  internal CBar bar;
  internal const int BASE_MARG = 1;
  internal const int BASE_PAGE = 0;
  internal const int BASE_PARA = 2;
  internal const byte BFLAG_BOX_FIRST = 1;
  internal const byte BFLAG_BOX_LAST = 2;
  internal const int BFONT_DEF = 0;
  internal const int BFONT_SYMBOL = 1;
  internal const int BFONT_WINGDINGS = 2;
  internal const int BKPICT_STRETCH = 1;
  internal const int BKPICT_TILE = 2;
  internal CBlk blk;
  internal const int BLT_4_DIAMONDS = 4;
  internal const int BLT_ARROW = 5;
  internal const int BLT_CHECK = 6;
  internal const int BLT_DIAMOND = 1;
  internal const int BLT_HOLLOW_SQUARE = 3;
  internal const int BLT_ROUND = 0;
  internal const int BLT_SQUARE = 2;
  internal const int BLTFLAG_HIDDEN = 1;
  internal const int BM_CHECKED = 1;
  internal const int BM_NONE = 2;
  internal const int BM_UNCHECKED = 0;
  internal const int BOLD_WEIGHT = 700;
  internal const int BORDER_ALL = 15;
  internal const int BORDER_BOT = 2;
  internal const int BORDER_CELL_BOT = 5;
  internal const int BORDER_CELL_LEFT = 6;
  internal const int BORDER_CELL_RIGHT = 7;
  internal const int BORDER_CELL_TOP = 4;
  internal const int BORDER_CHAR = 13;
  internal const int BORDER_INDEX_BOT = 1;
  internal const int BORDER_INDEX_LEFT = 2;
  internal const int BORDER_INDEX_RIGHT = 3;
  internal const int BORDER_INDEX_TOP = 0;
  internal const int BORDER_LEFT = 4;
  internal const int BORDER_MARGIN = 75;
  internal const int BORDER_PARA_ALL = 12;
  internal const int BORDER_PARA_BETWEEN = 18;
  internal const int BORDER_PARA_BOT = 1;
  internal const int BORDER_PARA_LEFT = 2;
  internal const int BORDER_PARA_RIGHT = 3;
  internal const int BORDER_PARA_TOP = 0;
  internal const int BORDER_RIGHT = 8;
  internal const int BORDER_ROW_BOT = 9;
  internal const int BORDER_ROW_LEFT = 10;
  internal const int BORDER_ROW_RIGHT = 11;
  internal const int BORDER_ROW_TOP = 8;
  internal const int BORDER_SECT_BOT = 15;
  internal const int BORDER_SECT_LEFT = 16 /*0x10*/;
  internal const int BORDER_SECT_RIGHT = 17;
  internal const int BORDER_SECT_TOP = 14;
  internal const int BORDER_TOP = 1;
  internal const int BOX_BETWEEN = 65536 /*0x010000*/;
  internal const int BOX_BORDER_WIDTH = 40;
  internal const int BOX_BOT = 32 /*0x20*/;
  internal const int BOX_DOUBLE = 256 /*0x0100*/;
  internal const int BOX_LEFT = 64 /*0x40*/;
  internal const int BOX_RIGHT = 128 /*0x80*/;
  internal const int BOX_THICK = 512 /*0x0200*/;
  internal const int BOX_TOP = 16 /*0x10*/;
  internal const int BRDRTYPE_DBL = 1;
  internal const int BRDRTYPE_LAST = 8;
  internal const int BRDRTYPE_NONE = 8;
  internal const int BRDRTYPE_SHADOW = 3;
  internal const int BRDRTYPE_SINGLE = 0;
  internal const int BRDRTYPE_THICK_THIN = 4;
  internal const int BRDRTYPE_THICK_THIN_THICK = 5;
  internal const int BRDRTYPE_THIN_THICK = 6;
  internal const int BRDRTYPE_THIN_THICK_THIN = 7;
  internal const int BRDRTYPE_TRIPLE = 2;
  internal const int BULLET = 8;
  internal const char BULLET_CHAR = '·';
  internal const int CAUX_CELLX_WAITING = 4;
  internal const int CAUX_CLOSED = 8;
  internal const int CAUX_HIDDEN = 16 /*0x10*/;
  internal const int CAUX_PAGINATION_DONE = 1;
  internal const int CAUX_SET_TOP_SPACE = 2;
  internal const char CELL_CHAR = '\u0013';
  internal const int CELL_MARGIN = 60;
  internal const int CENTER = 1;
  internal const int CF_BITMAP = 2;
  internal const int CF_DIB = 8;
  internal const int CF_ENHMETAFILE = 14;
  internal const int CF_MAX = 17;
  internal const int CF_METAFILEPICT = 3;
  internal const int CF_TEXT = 1;
  internal const int CF_UNICODETEXT = 13;
  internal const int CFLAG_3D = 131072 /*0x020000*/;
  internal const int CFLAG_AUTO_WIDTH = 8;
  internal const int CFLAG_COL_SPANNED = 64 /*0x40*/;
  internal const int CFLAG_FIX_WIDTH = 256 /*0x0100*/;
  internal const int CFLAG_FIX_WIDTH_PCT = 512 /*0x0200*/;
  internal const int CFLAG_FORCE_BKND_CLR = 16384 /*0x4000*/;
  internal const int CFLAG_HTML = 32 /*0x20*/;
  internal const int CFLAG_HTML_HDR = 128 /*0x80*/;
  internal const int CFLAG_MARGIN_OVERRIDE = 32768 /*0x8000*/;
  internal const int CFLAG_MERGED = 4;
  internal const int CFLAG_PREEXIST = 1024 /*0x0400*/;
  internal const int CFLAG_ROW_SPANNED = 16 /*0x10*/;
  internal const int CFLAG_RTF_SEL = 2048 /*0x0800*/;
  internal const int CFLAG_SEL1 = 1;
  internal const int CFLAG_SEL2 = 2;
  internal const int CFLAG_VALIGN_BASE = 65536 /*0x010000*/;
  internal const int CFLAG_VALIGN_BOT = 8192 /*0x2000*/;
  internal const int CFLAG_VALIGN_CTR = 4096 /*0x1000*/;
  internal const int CHAR_BOX = 8192 /*0x2000*/;
  internal const int CHAR_DB = 3;
  internal const int CHAR_HI = 2;
  internal const int CHAR_LO = 1;
  internal const int CHAR_NORMAL = 0;
  internal const int CHAR_WIDTH_CACHE_SIZE = 131072 /*0x020000*/;
  internal const int CHARID_CLEAR_ALL = 3;
  internal const int CHARID_CLEAR_LEFT = 1;
  internal const int CHARID_CLEAR_RIGHT = 2;
  internal static Color CLR_AUTO;
  internal static Color CLR_BLACK;
  internal static Color CLR_WHITE;
  internal static Color CLR_WHITE_FORCE;
  internal const char COL_CHAR = '\u0016';
  internal static int[] CosVal;
  internal const int CP_FIX_WIDTH = 0;
  internal const int CP_LEVEL = 2;
  internal const int CP_PARENT_CELL = 1;
  internal const int CP_TEXT_ROTATION = 3;
  internal const int CROP_BOT = 4;
  internal const int CROP_LEFT = 1;
  internal const int CROP_RIGHT = 2;
  internal const int CROP_TOP = 3;
  internal CCtl ctl;
  internal const int CURS_BACKWARD = 2;
  internal const int CURS_DOWN = 4;
  internal const int CURS_FORWARD = 1;
  internal const int CURS_RESET = 0;
  internal const int CURS_UP = 3;
  internal const int CWIDTH_RTL = 32768 /*0x8000*/;
  internal const int CWIDTH_USED = 16384 /*0x4000*/;
  internal const int CWIDTH_WIDTH = 16383 /*0x3FFF*/;
  internal static string[] DateFieldText;
  internal static bool DebugMode;
  internal const int DEF_LINE_WIDTH = 15;
  internal const int DEF_TAB_WIDTH = 720;
  internal const int DEFAULT_CFMT = 0;
  internal const byte DEFAULT_CHARSET = 1;
  internal const int DEFAULT_PFMT = 0;
  internal static int DefPaperCount;
  internal static float[] DefPaperHeight;
  internal static PaperKind[] DefPaperKind;
  internal static string[] DefPaperName;
  internal static int[] DefPaperSize;
  internal static float[] DefPaperWidth;
  internal static PrinterSettings DefPrt;
  internal static PageSettings DefPrtPage;
  internal const bool DIB_TO_METAFILE = true;
  internal static bool disable;
  internal const char DISP_CELL_CHAR = '¤';
  internal const char DISP_HIDDEN_CHAR = '\u0095';
  internal const char DISP_HYPH_CHAR = '¬';
  internal const char DISP_LINE_CHAR = '«';
  internal const char DISP_LINE_CHAR_ALT = '¯';
  internal const char DISP_PARA_CHAR = '¶';
  internal const char DISP_PARA_CHAR_ALT = '¿';
  internal const int DOB_LINE = 3;
  internal const int DOB_LINE_DOTTED = 2;
  internal const int DOB_LINE_NONE = 0;
  internal const int DOB_LINE_SOLID = 1;
  internal const int DOB_RECT = 2;
  internal const int DOB_TEXT_BOX = 1;
  internal static bool DoClassCleanup;
  internal const int DOL_BDIAG = 3;
  internal const int DOL_DIAG = 2;
  internal const int DOL_HORZ = 0;
  internal const int DOL_VERT = 1;
  internal const int DOUBLE_SPACE = 4;
  internal CDrag drag;
  internal const int DRAG_HOT_BL = 6;
  internal const int DRAG_HOT_BOT = 3;
  internal const int DRAG_HOT_BR = 7;
  internal const int DRAG_HOT_CORNER_END = 7;
  internal const int DRAG_HOT_CORNER_START = 4;
  internal const int DRAG_HOT_LEFT = 0;
  internal const int DRAG_HOT_RIGHT = 1;
  internal const int DRAG_HOT_TL = 4;
  internal const int DRAG_HOT_TOP = 2;
  internal const int DRAG_HOT_TR = 5;
  internal const int DRAG_TAB_WIDTH = 100;
  internal const int DRAG_TYPE_CELL = 9;
  internal const int DRAG_TYPE_FRAME_MOVE = 3;
  internal const int DRAG_TYPE_FRAME_SIZE = 2;
  internal const int DRAG_TYPE_PICT_SIZE = 1;
  internal const int DRAG_TYPE_ROW = 11;
  internal const int DRAG_TYPE_ROW_INDENT = 10;
  internal const int DRAG_TYPE_RULER_BEGIN = 5;
  internal const int DRAG_TYPE_RULER_END = 8;
  internal const int DRAG_TYPE_RULER_FIRST = 7;
  internal const int DRAG_TYPE_RULER_LEFT = 5;
  internal const int DRAG_TYPE_RULER_RIGHT = 6;
  internal const int DRAG_TYPE_RULER_TAB = 8;
  internal const int DRAG_TYPE_SCR_FIRST = 1;
  internal const int DRAG_TYPE_SCR_LAST = 3;
  internal static Cursor DragInCopyCur;
  internal static Cursor DragInCur;
  internal static Cursor DragOutCur;
  internal CDraw draw;
  internal CDash dsh;
  internal ImRtfEditor e;
  internal const int EACH_INDENT = 720;
  internal CEdit edit;
  internal const int ENGLISH = 1033;
  internal const char ESCAPE = '\u001B';
  internal static bool eval;
  internal static int EvalDaysRemaining;
  internal static int EvalDaysUsed;
  internal static bool expired;
  internal static bool extended;
  internal const int FFLAG_AUTO_SPELL = 1024 /*0x0400*/;
  internal const int FFLAG_CHECKED = 8192 /*0x2000*/;
  internal const int FFLAG_CTL_DRAWN = 8;
  internal const int FFLAG_CTL_VISIBLE = 4;
  internal const int FFLAG_DRAW_PENDING = 16 /*0x10*/;
  internal const int FFLAG_EQ_OBJECT = 2048 /*0x0800*/;
  internal const int FFLAG_HIDDEN_INFO = 128 /*0x80*/;
  internal const int FFLAG_ITALIC_OH = 1;
  internal const int FFLAG_MASK = 1536 /*0x0600*/;
  internal const int FFLAG_OCX_SRV_ERROR = 2;
  internal const int FFLAG_PICT_DRAWN = 64 /*0x40*/;
  internal const int FFLAG_RESIZED = 4096 /*0x1000*/;
  internal const int FFLAG_SCAPS = 512 /*0x0200*/;
  internal const int FFLAG_SEL = 32 /*0x20*/;
  internal const int FFLAG_USER = 256 /*0x0100*/;
  internal const char FFTR_CHAR = '\u001A';
  internal const char FHDR_CHAR = '\u0019';
  internal const int FIELD_AUTONUMLGL = 12;
  internal const int FIELD_CHECKBOX = 3;
  internal const int FIELD_DATA = 7;
  internal const int FIELD_DATE = 8;
  internal const int FIELD_HLINK = 14;
  internal const int FIELD_LISTBOX = 4;
  internal const int FIELD_LISTNUM = 11;
  internal const int FIELD_NAME = 6;
  internal const int FIELD_NAME_STYLE = 256 /*0x0100*/;
  internal const int FIELD_PAGE_COUNT = 5;
  internal const int FIELD_PAGE_NUMBER = 1;
  internal const int FIELD_PAGE_REF = 16 /*0x10*/;
  internal const int FIELD_PRINTDATE = 10;
  internal const int FIELD_SECT_PAGE_COUNT = 17;
  internal const int FIELD_TC = 13;
  internal const int FIELD_TEXTBOX = 2;
  internal const int FIELD_TOC = 9;
  internal const int FIELD_XE = 15;
  internal const int FILE_OPEN_IN_IE = 99999;
  internal const int FITTED_VIEW_DISP_PAGES = 2;
  internal CFld fld;
  internal const int FLOW_DEF = 0;
  internal const int FLOW_LTR = 1;
  internal const int FLOW_RTL = 2;
  internal const int FMFLAG_ACTIVE = 1;
  internal const int FMFLAG_SSFLDW_FOUND = 2;
  internal const int FNOTE_MARGIN = 50;
  internal CFnt fnt;
  internal static Pen FocusPen;
  internal const int FONTINFO_AUX_ID = 6;
  internal const int FONTINFO_CHARSET = 2;
  internal const int FONTINFO_FLAGS = 7;
  internal const int FONTINFO_FRAME_ID = 9;
  internal const int FONTINFO_FRAME_TYPE = 8;
  internal const int FONTINFO_IS_CTL = 12;
  internal const int FONTINFO_IS_PICT = 11;
  internal const int FONTINFO_OFFSET = 10;
  internal const int FONTINFO_PICT_HEIGHT = 4;
  internal const int FONTINFO_PICT_WIDTH = 3;
  internal const int FONTINFO_UCBASE = 1;
  internal const int FONTINFO_ULINE_COLOR = 5;
  internal const int FP_ERROR = 9999;
  internal const int FP_FILL_PATTERN = 4;
  internal const int FP_TEXT_DIST = 5;
  internal const int FP_TEXT_ROTATION = 1;
  internal const int FP_WRAP_STYLE = 2;
  internal const int FP_YBASE = 3;
  internal tc.StrFrame[] frame;
  internal const int FRAME_BORDER = 480;
  internal const int FRAME_BORDER_BOT = 256 /*0x0100*/;
  internal const int FRAME_BORDER_LEFT = 32 /*0x20*/;
  internal const int FRAME_BORDER_RIGHT = 64 /*0x40*/;
  internal const int FRAME_BORDER_TOP = 128 /*0x80*/;
  internal const int FRAME_BORDER_WIDTH = 15;
  internal const int FRAME_DISABLED = 4096 /*0x1000*/;
  internal const int FRAME_DIST_FROM_MARGIN = 1440;
  internal const int FRAME_DIST_FROM_TEXT = 180;
  internal const int FRAME_DIVIDER_LINE = 2;
  internal const int FRAME_FIRST_ROW_FRAME = 524288 /*0x080000*/;
  internal const int FRAME_FNOTE = 8192 /*0x2000*/;
  internal const int FRAME_FORCE_BKND_CLR = 1024 /*0x0400*/;
  internal const int FRAME_GRAY = 16384 /*0x4000*/;
  internal const int FRAME_LAST_COL = 512 /*0x0200*/;
  internal const int FRAME_LAST_ROW_FRAME = 1048576 /*0x100000*/;
  internal const int FRAME_NON_TEXT_DO = 8;
  internal const int FRAME_ON_SCREEN = 4;
  internal const int FRAME_PAGE_BOX = 65536 /*0x010000*/;
  internal const int FRAME_PICT = 2048 /*0x0800*/;
  internal const int FRAME_RIGHTMOST = 1;
  internal const int FRAME_RTL = 131072 /*0x020000*/;
  internal const int FRAME_RTL_CHECKED = 262144 /*0x040000*/;
  internal const int FRAME_TOP_SPAN = 32768 /*0x8000*/;
  internal const int FRAME_TRANSPARENT = 16 /*0x10*/;
  internal const int FRAME_WATERMARK = 2097152 /*0x200000*/;
  internal const int FRAME1_PARA_FRAME_BOX = 2;
  internal const int FRAME1_PART_EMPTY_CELL = 1;
  internal CFrm frm;
  internal const char FTR_CHAR = '\u0010';
  internal const int FTYPE_DIB = 1;
  internal const int FTYPE_ENHMETAFILE = 5;
  internal const int FTYPE_GIF = 4;
  internal const int FTYPE_JPEG = 3;
  internal const int FTYPE_METAFILE = 2;
  internal const int FTYPE_NONE = 0;
  internal const int FTYPE_OTHER = 7;
  internal const int FTYPE_PNG = 6;
  internal const int GFLAG_ANIM_INFO = 2048 /*0x0800*/;
  internal const int GFLAG_ANIM_SEQ = 1024 /*0x0400*/;
  internal const int GFLAG_BACKGROUND = 65536 /*0x010000*/;
  internal const int GFLAG_CAPS = 4096 /*0x1000*/;
  internal const int GFLAG_CELL_MARGIN_OVERRIDE = 1048576 /*0x100000*/;
  internal const int GFLAG_CHAR_STYLE = 8;
  internal const int GFLAG_DO_TEXT_READ = 4;
  internal const int GFLAG_DPLINEHOLLOW = 512 /*0x0200*/;
  internal const int GFLAG_DRAW_OBJECT = 1;
  internal const int GFLAG_GOT_HEIGHT = 4194304 /*0x400000*/;
  internal const int GFLAG_GOT_WIDTH = 2097152 /*0x200000*/;
  internal const int GFLAG_IN_DPGROUP = 67108864 /*0x04000000*/;
  internal const int GFLAG_IN_FIELD = 8192 /*0x2000*/;
  internal const int GFLAG_IN_FRAME = 128 /*0x80*/;
  internal const int GFLAG_IN_LEVELNUM = 268435456 /*0x10000000*/;
  internal const int GFLAG_IN_LEVELTEXT = 134217728 /*0x08000000*/;
  internal const int GFLAG_IN_MERGE_FIELD = 131072 /*0x020000*/;
  internal const int GFLAG_IN_NESTTABLEPROPS = 524288 /*0x080000*/;
  internal const int GFLAG_IN_SHP = 33554432 /*0x02000000*/;
  internal const int GFLAG_IN_SHPGRP = 536870912 /*0x20000000*/;
  internal const int GFLAG_IN_SHPRSLT = 256 /*0x0100*/;
  internal const int GFLAG_IN_STYLE_ITEM = 32 /*0x20*/;
  internal const int GFLAG_IN_STYLESHEET = 16 /*0x10*/;
  internal const int GFLAG_IN_TOC_FIELD = 262144 /*0x040000*/;
  internal const int GFLAG_INPUT_FIELD = 32768 /*0x8000*/;
  internal const int GFLAG_INSERT_BEF_PARA = 16384 /*0x4000*/;
  internal const int GFLAG_NEW_FRAME = 64 /*0x40*/;
  internal const int GFLAG_NO_UNICODE = 16777216 /*0x01000000*/;
  internal const int GFLAG_USE_SHP_INFO = 8388608 /*0x800000*/;
  internal const int GFLAG2_DEF_FONT_XLATED = 2;
  internal const int GFLAG2_PICT_SHAPE = 1;
  internal static object GlbFontLock = new object();
  internal static tc.StrGlbFont[] GlbFont;
  internal static tc.StrGlbPrtDC[] GlbPrtDC;
  internal static int GlbPrtDCCount;
  internal static Cursor HBeamCur;
  internal const char HDR_CHAR = '\u0011';
  internal static Assembly hHtn;
  internal static IntPtr hHts;
  internal const char HIDDEN_CHAR = '\u001C';
  internal const int HILIGHT_CHAR = 2;
  internal const int HILIGHT_LINE = 1;
  internal const int HILIGHT_OFF = 0;
  internal const int HLINK = 16384 /*0x4000*/;
  internal const char HPARA_CHAR = '\u0005';
  internal static Assembly hSpell;
  internal const int HTML_MISC_FLAG_MASK = 3584 /*0x0E00*/;
  internal static string HtnLicenseKey;
  internal static bool HtnSearched;
  internal static System.Type HtnType;
  internal const int HYPER_CODE = 64 /*0x40*/;
  internal static Cursor HyperlinkCur;
  /// <summary>
  /// Символ мягкого переноса
  /// Используется управляющий код ASCII 06 - "ACKNOWLEDGE" вышедший из употребления.
  /// </summary>
  internal const char HYPH_CHAR = '\u0006';
  internal const int ID_ACCEPT_ALL_CHANGES = 807;
  internal const int ID_ACCEPT_CHANGE = 806;
  internal const int ID_AUTO_SPELL = 776;
  internal const int ID_BACK_SPACE = 607;
  internal const int ID_BACK_TAB = 609;
  internal const int ID_BK_COLOR = 711;
  internal const int ID_BKND_PICT = 737;
  internal const int ID_BLOCK_COPY = 623;
  internal const int ID_BLOCK_MOVE = 624;
  internal const int ID_BOLD_ON = 648;
  internal const int ID_BORDER_MARGIN = 800;
  internal const int ID_BOX_ON = 725;
  internal const int ID_BULLET = 729;
  internal const int ID_CAPS_ON = 774;
  internal const int ID_CENTER = 657;
  internal const int ID_CHAR_NORMAL = 647;
  internal const int ID_CHAR_SPACE = 760;
  internal const int ID_CHAR_STYLE = 731;
  internal const int ID_COL_BREAK = 675;
  internal const int ID_COLOR = 654;
  public const int ID_COPY = 629;
  internal const int ID_CREATE_FIRST_FTR = 755;
  internal const int ID_CREATE_FIRST_HDR = 754;
  internal const int ID_CREATE_LIST = 779;
  internal const int ID_CREATE_LIST_OR = 781;
  internal const int ID_CTRL_DOWN = 716;
  internal const int ID_CTRL_TAB = 614;
  internal const int ID_CTRL_UP = 715;
  public const int ID_CUT = 628;
  public const int ID_DEL = 606;
  internal const int ID_DEL_LINE = 617;
  internal const int ID_DEL_PREV_WORD = 745;
  internal const int ID_DELETE_FIRST_FTR = 757;
  internal const int ID_DELETE_FIRST_HDR = 756;
  internal const int ID_DOC_RTL = 793;
  internal const int ID_DOUBLE_SPACE = 661;
  internal const int ID_DOWN = 603;
  internal const int ID_EDIT_DOB = 736;
  internal const int ID_EDIT_ENOTE = 778;
  internal const int ID_EDIT_FNOTE = 722;
  internal const int ID_EDIT_HDR_FTR = 677;
  internal const int ID_EDIT_INPUT_FIELD = 769;
  internal const int ID_EDIT_LIST = 780;
  internal const int ID_EDIT_LIST_LEVEL = 783;
  internal const int ID_EDIT_LIST_OR = 782;
  internal const int ID_EDIT_OLE = 727;
  internal const int ID_EDIT_PICT = 685;
  internal const int ID_EDIT_STYLE = 730;
  internal const int ID_EMBED_PICT = 632;
  internal const int ID_FILE_BEGIN = 612;
  internal const int ID_FILE_END = 613;
  internal const int ID_FIRST_COMMAND = 600;
  internal const int ID_FIRST_USER_CMD = 1000;
  internal const int ID_FITTED_VIEW = 739;
  internal const int ID_FONTS = 655;
  internal const int ID_FRAME_ROTATE_TEXT = 789;
  internal const int ID_FRAME_YBASE = 735;
  internal const int ID_HANGING_INDENT = 662;
  internal const int ID_HIDDEN_ON = 687;
  internal const int ID_HILIGHT_LINE = 622;
  internal const int ID_HLINK_ON = 742;
  internal const int ID_INLINE_IME = 761;
  internal const int ID_INS_AFT = 615;
  internal const int ID_INS_BEF = 616;
  internal const int ID_INSERT = 639;
  internal const int ID_INSERT_BOOKMARK = 753;
  internal const int ID_INSERT_CHECKBOX = 768 /*0x0300*/;
  internal const int ID_INSERT_DATA_FIELD = 766;
  internal const int ID_INSERT_DATE_TIME = 770;
  internal const int ID_INSERT_DRAW_OBJECT = 726;
  internal const int ID_INSERT_ENOTE = 777;
  internal const int ID_INSERT_FNOTE = 721;
  internal const int ID_INSERT_HLINK = 794;
  internal const int ID_INSERT_HYPH = 759;
  internal const int ID_INSERT_INPUT_FIELD = 767 /*0x02FF*/;
  internal const int ID_INSERT_NBDASH = 746;
  internal const int ID_INSERT_NBSPACE = 740;
  internal const int ID_INSERT_OBJECT = 693;
  internal const int ID_INSERT_PAGE_COUNT = 752;
  internal const int ID_INSERT_PAGE_NUMBER = 719;
  internal const int ID_INSERT_PARA_FRAME = 718;
  internal const int ID_INSERT_TOC = 771;
  internal const int ID_ITALIC_ON = 650;
  internal const int ID_JOIN_LINE = 619;
  internal const int ID_JUMP = 646;
  internal const int ID_JUSTIFY = 663;
  internal const int ID_LAST_USER_CMD = 1100;
  internal const int ID_LEFT = 604;
  internal const int ID_LEFT_INDENT = 659;
  internal const int ID_LEFT_INDENT_DEC = 785;
  internal const int ID_LEFT_JUSTIFY = 772;
  internal const int ID_LINE_BEGIN = 610;
  internal const int ID_LINE_END = 611;
  internal const int ID_LINK_PICT = 738;
  internal const int ID_LIST_LEVEL_DEC = 798;
  internal const int ID_LIST_LEVEL_INC = 797;
  internal const int ID_NEW = 626;
  internal const int ID_NEXT_CHANGE = 804;
  internal const int ID_NEXT_WORD = 620;
  internal const int ID_OPEN = 627;
  internal const int ID_PAGE_BREAK = 670;
  internal const int ID_PAGE_BREAK_BEFORE = 773;
  internal const int ID_PAGE_MODE = 674;
  internal const int ID_PAGE_OPTIONS = 644;
  internal const int ID_PARA_BK_COLOR = 749;
  internal const int ID_PARA_BORDER = 691;
  internal const int ID_PARA_KEEP = 723;
  internal const int ID_PARA_KEEP_NEXT = 724;
  internal const int ID_PARA_LIST = 784;
  internal const int ID_PARA_NBR = 748;
  internal const int ID_PARA_NORMAL = 656;
  internal const int ID_PARA_RTL = 790;
  internal const int ID_PARA_SPACING = 720;
  internal const int ID_PARA_STYLE = 732;
  public const int ID_PASTE = 630;
  internal const int ID_PASTE_SPEC = 631;
  internal const int ID_PASTE_TEXT = 787;
  internal const int ID_PGDN = 601;
  internal const int ID_PGUP = 600;
  internal const int ID_PICT_FROM_FILE = 632;
  internal const int ID_PREV_CHANGE = 805;
  internal const int ID_PREV_WORD = 621;
  internal const int ID_PRINT = 643;
  internal const int ID_PRINT_OPTIONS = 645;
  internal const int ID_PRINT_PREVIEW = 717;
  internal const int ID_PROTECT_FORM = 801;
  internal const int ID_PROTECT_ON = 689;
  internal const int ID_PROTECTION_LOCK = 690;
  internal const int ID_QUIT = 642;
  internal const int ID_REDO = 747;
  internal const int ID_REPAGINATE = 671;
  internal const int ID_REPLACE = 636;
  internal const int ID_RETURN = 763;
  internal const int ID_RIGHT = 605;
  internal const int ID_RIGHT_INDENT = 660;
  internal const int ID_RIGHT_JUSTIFY = 658;
  internal const int ID_RULER = 680;
  internal const int ID_SAVE = 640;
  internal const int ID_SAVEAS = 641;
  internal const int ID_SCAPS_ON = 775;
  internal const int ID_SEARCH = 633;
  internal const int ID_SEARCH_BACK = 635;
  internal const int ID_SEARCH_FOR = 634;
  internal const int ID_SECT_BREAK = 672;
  internal const int ID_SECT_OPTIONS = 673;
  internal const int ID_SECT_RTL = 792;
  internal const int ID_SELECT_ALL = 625;
  internal const int ID_SHOW_FIELD_NAMES = 758;
  internal const int ID_SHOW_HIDDEN = 686;
  internal const int ID_SHOW_HYPERLINK_CURSOR = 710;
  internal const int ID_SHOW_PAGE_BORDER = 744;
  internal const int ID_SHOW_PARA_MARK = 692;
  internal const int ID_SNAP_TO_GRID = 728;
  internal const int ID_SPELL = 741;
  internal const int ID_SPLIT_LINE = 618;
  internal const int ID_STATUS_RIBBON = 682;
  internal const int ID_STRIKE_ON = 651;
  internal const int ID_DOUBLE_STRIKE_ON = 832;
  internal const int ID_SUBSCR_ON = 653;
  internal const int ID_SUPSCR_ON = 652;
  internal const int ID_TAB = 608;
  internal const int ID_TAB_CLEAR = 666;
  internal const int ID_TAB_CLEAR_ALL = 667;
  internal const int ID_TAB_SET = 734;
  internal const int ID_TABLE_CELL_BORDER = 701;
  internal const int ID_TABLE_CELL_BORDER_COLOR = 786;
  internal const int ID_TABLE_CELL_COLOR = 765;
  internal const int ID_TABLE_CELL_SHADE = 702;
  internal const int ID_TABLE_CELL_VALIGN = 750;
  internal const int ID_TABLE_CELL_VTEXT = 803;
  internal const int ID_TABLE_CELL_WIDTH = 788;
  internal const int ID_TABLE_DEL_CELLS = 698;
  internal const int ID_TABLE_HDR_ROW = 762;
  internal const int ID_TABLE_INSERT = 694;
  internal const int ID_TABLE_INSERT_COL = 704;
  internal const int ID_TABLE_INSERT_ROW = 695;
  internal const int ID_TABLE_MERGE_CELLS = 697;
  internal const int ID_TABLE_ROW_HEIGHT = 703;
  internal const int ID_TABLE_ROW_KEEP = 764;
  internal const int ID_TABLE_ROW_POS = 700;
  internal const int ID_TABLE_ROW_RTL = 791;
  internal const int ID_TABLE_SEL_COL = 751;
  internal const int ID_TABLE_SHOW_GRID = 699;
  internal const int ID_TABLE_SPLIT_CELL = 696;
  internal const int ID_TER_HELP = 637;
  internal const int ID_TOOL_BAR = 681;
  internal const int ID_TRACK_CHANGES = 795;
  internal const int ID_ULINE_COLOR = 796;
  internal const int ID_ULINE_ON = 649;
  internal const int ID_ULINED_ON = 688;
  internal const int ID_UNDO = 638;
  internal const int ID_UP = 602;
  internal const int ID_USER1 = 808;
  internal const int ID_USER2 = 809;
  internal const int ID_USER3 = 810;
  internal const int ID_USER4 = 811;
  internal const int ID_USER5 = 812;
  internal const int ID_USER6 = 813;
  internal const int ID_USER7 = 814;
  internal const int ID_USER8 = 815;
  internal const int ID_USER9 = 816;
  internal const int ID_VIEW_HDR_FTR = 676;
  internal const int ID_WATERMARK = 802;
  internal const int ID_WIDOW_ORPHAN = 743;
  internal const int ID_ZOOM = 733;
  internal const int IDC_DELETE = 1;
  internal const int IDC_GOTO = 2;
  internal const int IDC_INSERT = 0;
  internal const int IDC_PAGE_REF = 956;
  internal const int IDC_RANGE_ALL = 773;
  internal const int IDC_RANGE_PAGES = 955;
  internal const int IDC_RANGE_SELECTED = 774;
  internal const int IDC_TABLE_ALL = 942;
  internal const int IDC_TABLE_ALL_ROWS = 891;
  internal const int IDC_TABLE_CUR_ROW = 890;
  internal const int IDC_TABLE_SEL_CELLS = 889;
  internal const int IDC_TABLE_SEL_COLS = 887;
  internal const int IDC_TABLE_SEL_ROWS = 888;
  internal const int IDP_ADD = 2511;
  internal const int IDP_FIRST_POPUP = 2500;
  internal const int IDP_FIRST_SPELL = 2500;
  internal const int IDP_FIRST_WORD = 2500;
  internal const int IDP_IGNORE = 2510;
  internal const int IDP_LAST_POPUP = 2511;
  internal const int IDP_LAST_SPELL = 2511;
  internal const int IDP_LAST_WORD = 2505;
  internal const int IDP_WORD1 = 2500;
  internal const int IDP_WORD2 = 2501;
  internal const int IDP_WORD3 = 2502;
  internal const int IDP_WORD4 = 2503;
  internal const int IDP_WORD5 = 2504;
  internal const int IDP_WORD6 = 2505;
  internal const int INFO_AUTHOR = 2;
  internal const int INFO_CATEGORY = 6;
  internal const int INFO_CELL = 16 /*0x10*/;
  internal const int INFO_COL = 8;
  internal const int INFO_COMMENT = 8;
  internal const int INFO_COMPANY = 4;
  internal const int INFO_DOCCOMM = 9;
  internal const int INFO_DYN_FIELD = 16384 /*0x4000*/;
  internal const int INFO_FRAME = 1024 /*0x0400*/;
  internal const int INFO_FRM_SPC_BEF = 8192 /*0x2000*/;
  internal const int INFO_HLINKBASE = 10;
  internal const int INFO_JUST = 128 /*0x80*/;
  internal const int INFO_KEYWORDS = 7;
  internal const int INFO_MANAGER = 3;
  internal const int INFO_MAX = 11;
  internal const int INFO_OPERATOR = 5;
  internal const int INFO_PAGE = 4;
  internal const int INFO_PAGE_NUMBER = 4096 /*0x1000*/;
  internal const int INFO_ROW = 32 /*0x20*/;
  internal const int INFO_SECT = 2;
  internal const int INFO_SPACE_LINE = 2048 /*0x0800*/;
  internal const int INFO_SUBJECT = 1;
  internal const int INFO_TAB = 1;
  internal const int INFO_TABLE = 64 /*0x40*/;
  internal const int INFO_TITLE = 0;
  internal static bool InIE;
  internal CInit init;
  internal static string InitFontFace;
  internal const int INITIAL_MAX_LINES = 200;
  internal static bool InitMbcs;
  internal static int InitPageHeight;
  internal static int InitPageWidth;
  internal static bool InServer;
  internal CIo io;
  internal static bool IsWinNT2000;
  internal static int[][] JpgFactor = (int[][]) null;
  internal const int JUSTIFY = 2048 /*0x0800*/;
  internal static string ky1;
  internal static string ky2;
  internal static string ky3;
  internal const char LAST_CTRL_CHAR = '\u001C';
  internal const int LEFT = 1024 /*0x0400*/;
  internal const char LEFT_PARAN_CHAR = '“';
  internal const int LFLAG_ASSUMED_TAB = 536870912 /*0x20000000*/;
  internal const int LFLAG_AUTONUMLGL = 1073741824 /*0x40000000*/;
  internal const int LFLAG_BOX_BOT = 1024 /*0x0400*/;
  internal const int LFLAG_BOX_TOP = 512 /*0x0200*/;
  internal const int LFLAG_BREAK = 2;
  internal const int LFLAG_CONTROL = 8;
  internal const int LFLAG_FFTR = 262144 /*0x040000*/;
  internal const int LFLAG_FHDR = 131072 /*0x020000*/;
  internal const int LFLAG_FHDR_FFTR = 393216 /*0x060000*/;
  internal const int LFLAG_FNOTE = 16 /*0x10*/;
  internal const int LFLAG_FNOTETEXT = 65536 /*0x010000*/;
  internal const int LFLAG_FRAME_TOP = 4096 /*0x1000*/;
  internal const int LFLAG_FTR = 1048576 /*0x100000*/;
  internal const int LFLAG_FTRS = 1310720 /*0x140000*/;
  internal const int LFLAG_HDR = 524288 /*0x080000*/;
  internal const int LFLAG_HDR_FTR = 1572864 /*0x180000*/;
  internal const int LFLAG_HDRS = 655360 /*0x0A0000*/;
  internal const int LFLAG_HDRS_FTRS = 1966080 /*0x1E0000*/;
  internal const int LFLAG_HPARA = 67108864 /*0x04000000*/;
  internal const int LFLAG_HTML_RULE = 256 /*0x0100*/;
  internal const int LFLAG_HYPH = 2097152 /*0x200000*/;
  internal const int LFLAG_LINE = 128 /*0x80*/;
  internal const int LFLAG_LIST = 33554432 /*0x02000000*/;
  internal const int LFLAG_LISTNUM = 134217728 /*0x08000000*/;
  internal const int LFLAG_NBDASH = 8192 /*0x2000*/;
  internal const int LFLAG_NBSPACE = 64 /*0x40*/;
  internal const int LFLAG_PARA = 1;
  internal const int LFLAG_PARA_FIRST = 4;
  internal const int LFLAG_PICT = 16384 /*0x4000*/;
  internal const int LFLAG_PICT_SPACE = 32768 /*0x8000*/;
  internal const int LFLAG_SECT = 2048 /*0x0800*/;
  internal const int LFLAG_SELECTED = 268435456 /*0x10000000*/;
  internal const int LFLAG_SHADE_BEGIN = 4194304 /*0x400000*/;
  internal const int LFLAG_SHADE_END = 8388608 /*0x800000*/;
  internal const int LFLAG_SOFT_COL = 32 /*0x20*/;
  internal const int LFLAG_TOC = 16777216 /*0x01000000*/;
  internal const int LFLAG2_BOX_BETWEEN = 16 /*0x10*/;
  internal const int LFLAG2_DEL_REV = 64 /*0x40*/;
  internal const int LFLAG2_ENOTETEXT = 2;
  internal const int LFLAG2_HIDDEN_CHAR = 8;
  internal const int LFLAG2_INS_REV = 128 /*0x80*/;
  internal const int LFLAG2_PARA_RTL = 256 /*0x0100*/;
  internal const int LFLAG2_RTL = 32 /*0x20*/;
  internal const int LFLAG2_SPELL_CHECKED = 1;
  internal const int LFLAG2_TC = 4;
  internal const int LFLAG2_TEMP_HYPH = 512 /*0x0200*/;
  internal const char LINE_CHAR = '\u000F';
  internal CLink link;
  internal const int LIST_BLT = 23;
  internal const int LIST_CARDINAL = 6;
  internal const int LIST_DEC = 0;
  internal const int LIST_DEC_PAD = 22;
  internal const int LIST_LWR_ALPHA = 4;
  internal const int LIST_LWR_ROMAN = 2;
  internal const int LIST_NO_NUM = 255 /*0xFF*/;
  internal const int LIST_ORDINAL = 5;
  internal const int LIST_ORDINAL_TEXT = 7;
  internal const int LIST_UPR_ALPHA = 3;
  internal const int LIST_UPR_ROMAN = 1;
  internal const int LISTAFT_NONE = 2;
  internal const int LISTAFT_SPACE = 1;
  internal const int LISTAFT_TAB = 0;
  internal const int LISTFLAG_RESTART_SEC = 1;
  internal const int LISTLEVEL_LEGAL = 8;
  internal const int LISTLEVEL_NO_RESET = 32 /*0x20*/;
  internal const int LISTLEVEL_OLD = 2;
  internal const int LISTLEVEL_REFORMAT = 16 /*0x10*/;
  internal const int LISTLEVEL_RESTART = 1;
  internal const int LISTLEVEL_RESTARTED = 4;
  internal const int LP_CELL_ID = 6;
  internal const int LP_ERROR = 9999;
  internal const int LP_LINE_FLAGS = 1;
  internal const int LP_LINE_FLAGS2 = 2;
  internal const int LP_LINE_LEN = 3;
  internal const int LP_LIST_FONT = 5;
  internal const int LP_LIST_ID = 8;
  internal const int LP_LIST_LEVEL = 10;
  internal const int LP_LIST_NBR = 4;
  internal const int LP_LIST_OR_ID = 9;
  internal const int LP_PARA_FRAME_ID = 7;
  internal const int MAX_ANIM_GIFS = 100;
  internal const int MAX_AUTO_COMPS = 100;
  internal const int MAX_BLTS = 1500;
  internal const int MAX_BORDERS = 19;
  internal const int MAX_CHAR_TAGS = 2000;
  internal const int MAX_CUST_ICONS = 100;
  internal const int MAX_DATA_LEN = 100;
  internal const int MAX_DP_GROUPS = 10;
  internal const int MAX_DRAG_OBJS = 850;
  internal const int MAX_EXPAND = 4320;
  internal const int MAX_FIELDS = 100;
  internal const int MAX_FONT_HANDLES = 10;
  internal const int MAX_FONTS = 320;
  internal const int MAX_FRAMES = 1500;
  internal const int MAX_GLB_DC = 100;
  internal const int MAX_GLB_FONTS = 500;
  internal const int MAX_IMAGE_MAPS = 50;
  internal const int MAX_INDENT = 8640;
  internal const int MAX_INIT_UNDOS = 40;
  internal const int MAX_JPEG_HUFF_TBLS = 8;
  internal const int MAX_JPEG_IN_BUF = 3;
  internal const int MAX_LINE_PTRS = 150;
  internal const int MAX_LIST_OR = 5000;
  internal const int MAX_LISTS = 1200;
  internal const int MAX_MSGS = 250;
  internal const int MAX_NUM_TEXT_LEN = 13;
  internal const int MAX_PAGES = 4500;
  internal const int MAX_PAL_COLORS = 100;
  internal const int MAX_PAPERS = 11;
  internal const int MAX_PARA_FRAMES = 2000;
  internal const int MAX_PARA_PER_PAGE = 100;
  internal const int MAX_PARA_PER_WIN = 30;
  internal const int MAX_PASSES = 4;
  internal const int MAX_PASTE_FORMATS = 6;
  internal const int MAX_PFMT = 2000;
  internal const int MAX_POINTS = 16 /*0x10*/;
  internal const int MAX_PREVIEW_PAGES = 2;
  internal const int MAX_REVIEWERS = 3;
  internal const int MAX_RGB = 236;
  internal const int MAX_RTF_FONTS = 5000;
  internal const int MAX_RTF_GROUPS = 50;
  internal const int MAX_RTF_LS = 5000;
  internal const int MAX_RTF_SEQ_FONTS = 500;
  internal const int MAX_SECTS = 1100;
  internal const int MAX_SEGS = 100;
  internal const int MAX_SELECT_PRINT_LINES = 1000;
  internal const int MAX_SPACES = 20;
  internal const int MAX_SPEED_KEYS = 300;
  internal const int MAX_SS_IDS = 225;
  internal const int MAX_SS_NAME_WIDTH = 63 /*0x3F*/;
  internal const int MAX_TABS = 600;
  internal const int MAX_TASKS = 50;
  internal const int MAX_TBL_COLS = 300;
  internal const int MAX_TBL_LEVELS = 20;
  internal const int MAX_TLB = 34;
  internal const int MAX_TLB_LINES = 2;
  internal const int MAX_TLB_PER_LINE = 100;
  internal const int MAX_TYPES = 300;
  internal const int MAX_UNDOS = 100;
  internal const int MAX_WIDTH = 1000;
  internal const int MAX_WIN_PTRS = 200;
  internal const int MAX_ZOOMS = 8;
  internal static char MergeDelim;
  internal const int MIN_CELL_WIDTH = 180;
  internal const int MIN_FRAME_MARGIN = 40;
  internal const int MIN_LINES_PER_PAGE = 20;
  internal const float MIN_TEXT_HEIGHT = 0.0005f;
  internal const float MIN_TEXT_WIDTH = 0.0005f;
  internal const int MIN_VTEXT_CELL_HEIGHT = 720;
  internal CMisc misc;
  internal CMnu mnu;
  internal static string[] MonthNames;
  internal const string MSDRAW_CLASS = "MSDraw";
  internal const int MSG_15LINES = 182;
  internal const int MSG_ACCEPT_CHANGES = 225;
  internal const int MSG_ADD = 205;
  internal const int MSG_ATLEAST = 197;
  internal const int MSG_BAD_BMP_FILE = 1;
  internal const int MSG_BAD_FNOTE_MARKER = 2;
  internal const int MSG_BAD_JPG_FILE = 137;
  internal const int MSG_BAD_OBJ_SIZE = 3;
  internal const int MSG_BAD_PNG_DATA = 160 /*0xA0*/;
  internal const int MSG_BAD_PRINTER_DRV = 4;
  internal const int MSG_BAD_RTF = 195;
  internal const int MSG_BAD_VERSION = 130;
  internal const int MSG_BAD_WIN_HANDLE = 129;
  internal const int MSG_BMP_FILES = 176 /*0xB0*/;
  internal const int MSG_BULLET_MISSING = 136;
  internal const int MSG_CANT_SET_PROP = 202;
  internal const int MSG_CELL_DATA_MISSING = 5;
  internal const int MSG_CELL_WITHOUT_ROW = 6;
  internal const int MSG_CHANGES_COUNT = 226;
  internal const int MSG_CHAR_FMT_MISSING = 8;
  internal const int MSG_CHARSTYLE_IN_USE = 7;
  internal const int MSG_CLIPBOARD_BUSY = 9;
  internal const int MSG_CLIPBOARD_NO_DATA = 10;
  internal const int MSG_CLIPBOARD_NO_FMT = 11;
  internal const int MSG_COL_BREAK = 148;
  internal const int MSG_COLUMNS = 173;
  internal const int MSG_COUNT_MISMATCH = 12;
  internal const int MSG_CREATE_NEW_FILE = 13;
  internal const int MSG_DBL_LINE = 183;
  internal const int MSG_DEL_DRAW_OBJ = 152;
  internal const int MSG_DEL_FIRST_FTR = 159;
  internal const int MSG_DEL_FIRST_HDR = 158;
  internal const int MSG_DEL_FRAME = 153;
  internal const int MSG_DEL_FTR = 170;
  internal const int MSG_DEL_HDR = 169;
  internal const int MSG_DEL_TOC = 165;
  internal const int MSG_DELETED_BY = 223;
  internal const int MSG_END_BACKWARD_SEARCH = 14;
  internal const int MSG_END_FORWARD_SEARCH = 15;
  internal const int MSG_ENH_METAFILES = 175;
  internal const int MSG_ERR_ADDING_STRING = 16 /*0x10*/;
  internal const int MSG_ERR_ADDING_VALUE = 17;
  internal const int MSG_ERR_ADV_CREATE = 19;
  internal const int MSG_ERR_ADVISE_CONNECT = 18;
  internal const int MSG_ERR_BLOCK_SIZE = 20;
  internal const int MSG_ERR_BMP_CREATE = 21;
  internal const int MSG_ERR_CTL_CREATE = 22;
  internal const int MSG_ERR_DATA_RETRIEVE = 23;
  internal const int MSG_ERR_DIB_CREATE = 24;
  internal const int MSG_ERR_END_PAGE = 25;
  internal const int MSG_ERR_END_PRINTER = 26;
  internal const int MSG_ERR_ENH_META_HDR = 142;
  internal const int MSG_ERR_FILE_ACCESS = 133;
  internal const int MSG_ERR_FILE_LENGTH = 27;
  internal const int MSG_ERR_FILE_OPEN = 28;
  internal const int MSG_ERR_FILE_READ = 29;
  internal const int MSG_ERR_FILE_SEEK = 30;
  internal const int MSG_ERR_FILE_WRITE = 31 /*0x1F*/;
  internal const int MSG_ERR_FONT_CREATE = 42;
  internal const int MSG_ERR_FONT_SELECT = 43;
  internal const int MSG_ERR_INIT_BMP = 44;
  internal const int MSG_ERR_INIT_OLE = 45;
  internal const int MSG_ERR_INIT_PRINTER = 46;
  internal const int MSG_ERR_INSTANCE = 47;
  internal const int MSG_ERR_JPG_HUFF = 138;
  internal const int MSG_ERR_JPG_MARKER = 139;
  internal const int MSG_ERR_JPG_MCU = 171;
  internal const int MSG_ERR_JPG_NO_HUFF = 140;
  internal const int MSG_ERR_LOAD_ACCEL = 48 /*0x30*/;
  internal const int MSG_ERR_MAIL_MERGE = 49;
  internal const int MSG_ERR_MEM_LOCK = 50;
  internal const int MSG_ERR_META_ACCESS = 51;
  internal const int MSG_ERR_META_BAD_FILE = 52;
  internal const int MSG_ERR_META_COPY = 53;
  internal const int MSG_ERR_META_CREATE = 54;
  internal const int MSG_ERR_META_READ = 55;
  internal const int MSG_ERR_META_RECREATE = 56;
  internal const int MSG_ERR_OBJ_DATA = 57;
  internal const int MSG_ERR_OBJ_SAVE = 58;
  internal const int MSG_ERR_OBJ_SIZE = 59;
  internal const int MSG_ERR_OLE_CLSID = 60;
  internal const int MSG_ERR_OLE_DATA_CONV = 61;
  internal const int MSG_ERR_OLE_LOAD = 62;
  internal const int MSG_ERR_OLE_SAVE = 63 /*0x3F*/;
  internal const int MSG_ERR_OLE_USER = 64 /*0x40*/;
  internal const int MSG_ERR_OLE_VERSION = 65;
  internal const int MSG_ERR_PEN = 66;
  internal const int MSG_ERR_PICT_FILE_FMT = 67;
  internal const int MSG_ERR_PICT_FILE_OPEN = 68;
  internal const int MSG_ERR_PICT_FILE_READ = 69;
  internal const int MSG_ERR_PRESENT_DATA = 70;
  internal const int MSG_ERR_PRINT = 155;
  internal const int MSG_ERR_SITE_CREATE = 71;
  internal const int MSG_ERR_START_PRINTER = 72;
  internal const int MSG_ERR_STORAGE_CREATE = 73;
  internal const int MSG_ERR_STORAGE_RETRIEVE = 74;
  internal const int MSG_ERR_TEXT_METRIC = 75;
  internal const int MSG_ERR_TOO_MANY_FILES = 132;
  internal const int MSG_ERR_VIEW_RETRIEVE = 76;
  internal const int MSG_ERROR = 193;
  internal const int MSG_EXACTLY = 184;
  internal const int MSG_FATAL_ERROR = 191;
  internal const int MSG_FILE_INFO_MISSING = 77;
  internal const int MSG_FILE_OPEN = 192 /*0xC0*/;
  internal const int MSG_FILE_SIGN_MISSING = 78;
  internal const int MSG_FIRST_FTR = 157;
  internal const int MSG_FIRST_HDR = 156;
  internal const int MSG_FONT_TBL_INCOMPL = 79;
  internal const int MSG_GET_ENH_META = 143;
  internal const int MSG_GIF_FILES = 174;
  internal const int MSG_ICO_FILES = 198;
  internal const int MSG_IGNORE_ALL = 204;
  internal const int MSG_INSERTED_BY = 222;
  internal const int MSG_INTERNAL_ERROR = 200;
  internal const int MSG_INVALID_NUMBER = 80 /*0x50*/;
  internal const int MSG_INVALID_PRINT_RECT = 81;
  internal const int MSG_INVALID_SECT_DATA = 82;
  internal const int MSG_INVALID_SELECT = 83;
  internal const int MSG_INVOKE_TEXT_LINK = 221;
  internal const int MSG_IRREVERSIBLE_DEL = 154;
  internal const int MSG_JPG_FILES = 178;
  internal const int MSG_KEEP_HDRFTR_LINE = 84;
  internal const int MSG_LINE_TOO_LONG = 85;
  internal const int MSG_LINE_TRUNCATED = 86;
  internal const int MSG_LINES = 186;
  internal const int MSG_MARGIN = 189;
  internal const int MSG_MARGIN_TOO_LARGE = 87;
  internal const int MSG_MAX_DRAG = 1;
  internal const int MSG_MAX_LINES_EXCEEDED = 88;
  internal const int MSG_MAX_PFMT = 2;
  internal const int MSG_MAX_WIN_LINES = 144 /*0x90*/;
  internal const int MSG_MERGE_DATA_TOO_LONG = 89;
  internal const int MSG_METAFILES = 177;
  internal const int MSG_MISSING_ROW_MARKER = 90;
  internal const int MSG_MULTIPLE = 185;
  internal const int MSG_NO_CHANGED_TEXT = 224 /*0xE0*/;
  internal const int MSG_NO_ERROR = 0;
  internal const int MSG_NO_HELP = 220;
  internal const int MSG_NO_HILIGHT = 93;
  internal const int MSG_NO_LIST_BLOCK = 167;
  internal const int MSG_NO_LIST_OR_BLOCK = 168;
  internal const int MSG_NO_OLE_CLASS = 94;
  internal const int MSG_NO_PRINTER = 164;
  internal const int MSG_NO_RANGE_PRINTING = 95;
  internal const int MSG_NO_RTF_INFO_BLOCK = 166;
  internal const int MSG_NO_SERVER = 96 /*0x60*/;
  internal const int MSG_NO_STRING = 97;
  internal const int MSG_NODEL_HDRFTR_BODY = 91;
  internal const int MSG_NODEL_MIX_HTML = 131;
  internal const int MSG_NOT_TE_FILE = 92;
  internal const int MSG_NULL_FIELD = 98;
  internal const int MSG_OLEERR_NEED_SHARE = 99;
  internal const int MSG_OLEERR_NO_CLASS = 100;
  internal const int MSG_OUT_OF_BLT_TABLE = 135;
  internal const int MSG_OUT_OF_CELL_SLOT = 101;
  internal const int MSG_OUT_OF_CHAR_STACK = 102;
  internal const int MSG_OUT_OF_COMP_DC = 103;
  internal const int MSG_OUT_OF_DC = 104;
  internal const int MSG_OUT_OF_DRAG_OBJ = 105;
  internal const int MSG_OUT_OF_FONT_SLOT = 106;
  internal const int MSG_OUT_OF_GROUP = 107;
  internal const int MSG_OUT_OF_IMAGE_MAP_TABLE = 172;
  internal const int MSG_OUT_OF_MEM = 108;
  internal const int MSG_OUT_OF_PARA_SLOT = 109;
  internal const int MSG_OUT_OF_PARAFID_SLOT = 110;
  internal const int MSG_OUT_OF_RANGE = 111;
  internal const int MSG_OUT_OF_ROW_SLOT = 112 /*0x70*/;
  internal const int MSG_OUT_OF_STYLESHEET = 113;
  internal const int MSG_OUT_OF_TAB_POS = 114;
  internal const int MSG_OUT_OF_TAB_TABLE = 115;
  internal const int MSG_OUT_OF_TASK = 116;
  internal const int MSG_OUT_OF_WIN_SLOTS = 117;
  internal const int MSG_PAGE_BOX = 203;
  internal const int MSG_PAGE_BREAK = 147;
  internal const int MSG_PAGE_FTR = 150;
  internal const int MSG_PAGE_HDR = 149;
  internal const int MSG_PARA_FMT_MISSING = 119;
  internal const int MSG_PARA_TBL_INCOMPL = 120;
  internal const int MSG_PARASTYLE_IN_USE = 118;
  internal const int MSG_PFRAME_DATA_MISSING = 121;
  internal const int MSG_PICTURE_SIZE = 180;
  internal const int MSG_PNG_ABORT = 190;
  internal const int MSG_PNG_FILES = 179;
  internal const int MSG_POINTS = 187;
  internal const int MSG_PP_CLOSE = 219;
  internal const int MSG_PP_EDIT = 215;
  internal const int MSG_PP_FITTED = 217;
  internal const int MSG_PP_ONE_PAGE = 212;
  internal const int MSG_PP_PG = 216;
  internal const int MSG_PP_PRINT = 214;
  internal const int MSG_PP_TWO_PAGES = 213;
  internal const int MSG_PRINT_AREA_SMALL = 122;
  internal const int MSG_PRINTING_CANCELLED = 161;
  internal const int MSG_PROTECTED = 123;
  internal const int MSG_READ_ABORTED = 124;
  internal const int MSG_READ_ONLY = 163;
  internal const int MSG_REDO_EMPTY = 134;
  internal const int MSG_REPLACE_STRING = 125;
  internal const int MSG_ROW_BREAK = 151;
  internal const int MSG_SAVE_INCOMPL = 126;
  internal const int MSG_SAVE_MODS = 127 /*0x7F*/;
  internal const int MSG_SB_COL = 211;
  internal const int MSG_SB_INS = 206;
  internal const int MSG_SB_LINE = 209;
  internal const int MSG_SB_OT = 207;
  internal const int MSG_SB_PAGE = 208 /*0xD0*/;
  internal const int MSG_SB_PRINTING_PAGE = 218;
  internal const int MSG_SB_ROW = 210;
  internal const int MSG_SECT_BREAK = 146;
  internal const int MSG_SECT_TBL_MISSING = 41;
  internal const int MSG_SECTION_FULL = 128 /*0x80*/;
  internal const int MSG_SEL_PROTECTED = 32 /*0x20*/;
  internal const int MSG_SEL_SPAN_HDRFTR = 40;
  internal const int MSG_SINGLE = 181;
  internal const int MSG_SMALL_CELL_MARGIN = 162;
  internal const int MSG_SOFT_PAGE_BREAK = 145;
  internal const int MSG_SPELL_OVER = 33;
  internal const int MSG_SPL_CHECKED = 194;
  internal const int MSG_SPL_INCORRECT = 201;
  internal const int MSG_STYLESHEET_MISSING = 34;
  internal const int MSG_TAB_POS = 188;
  internal const int MSG_TAB_TBL_MISSING = 35;
  internal const int MSG_TAG_TABLE_MISSING = 141;
  internal const int MSG_TBL_DATA_MISSING = 36;
  internal const int MSG_TIF_FILES = 199;
  internal const int MSG_TYPE_STYLE_NAME = 37;
  internal const int MSG_UNDO_EMPTY = 38;
  internal const int MSG_UNEXPECTED_EOF = 39;
  internal const int MSG_UNKNOWN = 196;
  public const char NBDASH_CHAR = '\u0017';
  internal const int NBR_DEC = 0;
  internal const int NBR_DEFAULT = 5;
  internal const int NBR_LWR_ALPHA = 2;
  internal const int NBR_LWR_ROMAN = 4;
  internal const int NBR_UPR_ALPHA = 1;
  internal const int NBR_UPR_ROMAN = 3;
  public const char NBSPACE_CHAR = '\u000E';
  internal const int NORM_WEIGHT = 400;
  internal const int NUM_WIDTH_CHARS = 256 /*0x0100*/;
  internal const int OASPECT_CONTENT = 1;
  internal const int OASPECT_ICON = 2;
  internal const int OASPECT_NONE = 0;
  internal const int OBJ_EMBED = 1;
  internal const int OBJ_EMBED_INSERT = 4;
  internal const int OBJ_EMBED_TEMP = 3;
  internal const int OBJ_LINK = 2;
  internal const int OBJ_NONE = 0;
  internal const int OBJ_OCX = 5;
  internal const int OBJ_OCX_EXT = 6;
  internal const int OCX_BOOL = 1;
  internal const int OCX_DISPATCH = 4;
  internal const int OCX_GET_PROP = 2;
  internal const int OCX_LONG = 2;
  internal const int OCX_METHOD = 0;
  internal const int OCX_NONE = 0;
  internal const int OCX_SET_PROP = 1;
  internal const int OCX_SET_PROP_REF = 3;
  internal const int OCX_STRING = 3;
  internal COle ole;
  internal const int OLENOT_CHANGED = 1;
  internal const int OLENOT_CLOSED = 4;
  internal const int OLENOT_SAVED = 2;
  internal const int OLEPTR_OBJECT = 0;
  internal const int OLEPTR_STORAGE = 1;
  internal COp op;
  internal static bool OSCanRotate;
  internal CPage page;
  internal const int PAGE_BORDER_MARGIN = 360;
  internal const int PAGE_BOX_WIDTH = 1100;
  internal const int PAGE_BOXED = 8;
  internal const char PAGE_CHAR = '\f';
  internal const int PAGE_FIRST_SECT_PAGE = 2;
  internal const int PAGE_FTR = 8192 /*0x2000*/;
  internal const int PAGE_HARD = 1;
  internal const int PAGE_HDR = 4096 /*0x1000*/;
  internal const int PAGE_HDR_FTR = 12288 /*0x3000*/;
  internal const int PAGE_REPAGE = 4;
  internal static Pen PagePen;
  internal const int PAINT_DIRECT = 1;
  internal const int PAINT_LINE = 2;
  internal const int PAINT_MEM = 2;
  internal const int PAINT_MIN = 1;
  internal const int PAINT_PARTIAL_WIN = 5;
  internal const int PAINT_RANGE = 3;
  internal const int PAINT_WIN = 4;
  internal const int PAINT_WIN_RESET = 6;
  internal static Color[] PalColor;
  internal CPar par;
  internal const int PARA_ADDRESS = 2;
  internal const int PARA_BLOCKQUOTE = 1;
  internal const int PARA_BORDER_MARGIN = 50;
  internal const int PARA_BORDER_WIDTH = 30;
  internal const int PARA_BOX = 65776 /*0x0100F0*/;
  internal const int PARA_BOX_ATTRIB = 1008;
  internal const int PARA_BOX_BETWEEN = 65536 /*0x010000*/;
  internal const int PARA_BOX_BOT = 32 /*0x20*/;
  internal const int PARA_BOX_DOUBLE = 256 /*0x0100*/;
  internal const int PARA_BOX_LEFT = 64 /*0x40*/;
  internal const int PARA_BOX_RIGHT = 128 /*0x80*/;
  internal const int PARA_BOX_SPACE = 20;
  internal const int PARA_BOX_THICK = 512 /*0x0200*/;
  internal const int PARA_BOX_TOP = 16 /*0x10*/;
  public const char PARA_CHAR = '\u0015';
  internal const int PARA_FRAME_BEHIND_TEXT = 134217728 /*0x08000000*/;
  internal const int PARA_FRAME_BOXED = 1024 /*0x0400*/;
  internal const int PARA_FRAME_CENTER = 8;
  internal const int PARA_FRAME_CONV = 131072 /*0x020000*/;
  internal const int PARA_FRAME_DOTTED = 2048 /*0x0800*/;
  internal const int PARA_FRAME_FILL_PICT = 8388608 /*0x800000*/;
  internal const int PARA_FRAME_FIXED_HEIGHT = 67108864 /*0x04000000*/;
  internal const int PARA_FRAME_HCHAR = 536870912 /*0x20000000*/;
  internal const int PARA_FRAME_HCOL = 1073741824 /*0x40000000*/;
  internal const int PARA_FRAME_HPAGE = 1;
  internal const int PARA_FRAME_IGNORE_X = 524288 /*0x080000*/;
  internal const int PARA_FRAME_IGNORE_Y = 1048576 /*0x100000*/;
  internal const int PARA_FRAME_IN_CELL = 16777216 /*0x01000000*/;
  internal const int PARA_FRAME_LINE = 256 /*0x0100*/;
  internal const int PARA_FRAME_MIN_WIDTH = 360;
  internal const int PARA_FRAME_NO_LINE = 262144 /*0x040000*/;
  internal const int PARA_FRAME_NO_WRAP = 8192 /*0x2000*/;
  internal const int PARA_FRAME_OBJECT = 896;
  internal const int PARA_FRAME_PAGE_BREAK = 65536 /*0x010000*/;
  internal const int PARA_FRAME_POSITIONED = 32768 /*0x8000*/;
  internal const int PARA_FRAME_RECT = 512 /*0x0200*/;
  internal const int PARA_FRAME_RIGHT_JUST = 4;
  internal const int PARA_FRAME_SELECT = 4096 /*0x1000*/;
  internal const int PARA_FRAME_SET_WIDTH = 16 /*0x10*/;
  internal const int PARA_FRAME_SHPGRP = 2097152 /*0x200000*/;
  internal const int PARA_FRAME_TEXT_BOX = 128 /*0x80*/;
  internal const int PARA_FRAME_USED = 2;
  internal const int PARA_FRAME_VCENTER = 33554432 /*0x02000000*/;
  internal const int PARA_FRAME_VLINE = 268435456 /*0x10000000*/;
  internal const int PARA_FRAME_VMARG = 64 /*0x40*/;
  internal const int PARA_FRAME_VPAGE = 32 /*0x20*/;
  internal const int PARA_FRAME_WATERMARK = 4194304 /*0x400000*/;
  internal const int PARA_FRAME_WRAP_IGNORE = 16384 /*0x4000*/;
  internal const int PARA_HR = 3;
  internal const int PARA_KEEP = 16384 /*0x4000*/;
  internal const int PARA_KEEP_NEXT = 32768 /*0x8000*/;
  internal const int PARA_PRE = 4;
  internal const int PARAINFO_BK_COLOR = 3;
  internal const int PARAINFO_BORDER_COLOR = 2;
  internal const int PARAINFO_ERROR = -999999;
  internal const int PARAINFO_SPC_AFT = 5;
  internal const int PARAINFO_SPC_BEF = 4;
  internal const int PARAINFO_SPC_BET = 6;
  internal const int PARAINFO_TEXT_FLOW = 1;
  internal const int PARAM_DBL = 2;
  internal const int PARAM_IGNORE = -31234;
  internal const int PARAM_INT = 1;
  internal const int PARAM_NONE = 0;
  internal const int PASS_BODY = 3;
  internal const int PASS_FTR = 4;
  internal const int PASS_HDR = 1;
  internal const int PASS_TBL_HDR = 2;
  internal const int PASTE_BM = 4;
  internal const string PASTE_DESC_BM = "Bitmap";
  internal const string PASTE_DESC_DIB = "Device Independent Bitmap";
  internal const string PASTE_DESC_OBJECT = "Object";
  internal const string PASTE_DESC_PICT = "Picture";
  internal const string PASTE_DESC_RTF = "Formatted Text (RTF)";
  internal const string PASTE_DESC_TEXT = "Unformatted Text";
  internal const int PASTE_DIB = 5;
  internal const int PASTE_OBJECT = 0;
  internal const int PASTE_PICT = 3;
  internal const int PASTE_RTF = 1;
  internal const int PASTE_TEXT = 2;
  internal const int PAT_NONE = 0;
  internal const int PAT_OTHER = 2;
  internal const int PAT_SOLID = 1;
  internal const int PDF_RES_X = 1440;
  internal const int PDF_RES_Y = 1440;
  internal const int PFLAG_CELL = 2;
  internal const int PFLAG_ENDNOTE = 128 /*0x80*/;
  internal const int PFLAG_FRAME = 1;
  internal const int PFLAG_NO_WRAP = 16 /*0x10*/;
  internal const int PFLAG_PAGE_BREAK = 64 /*0x40*/;
  internal const int PFLAG_RESERVED1 = 4;
  internal const int PFLAG_RESERVED2 = 4;
  internal const int PFLAG_STYLES = 65520;
  internal const int PFLAG_WIDOW = 32 /*0x20*/;
  internal const int PFRAME_FLOAT = 3;
  internal const int PFRAME_LEFT = 1;
  internal const int PFRAME_NONE = 0;
  internal const int PFRAME_RIGHT = 2;
  internal static MethodInfo pHtsReadFromTer;
  internal static MethodInfo pHtsSaveFromTer;
  internal const int PICT = 128 /*0x80*/;
  internal const int PICT_BITMAP = 4;
  internal const char PICT_CHAR = '\u0018';
  internal const int PICT_CONTROL = 2;
  internal const int PICT_DIBITMAP = 0;
  internal const int PICT_ENHMETAFILE = 9;
  internal const int PICT_FORM = 6;
  internal const int PICT_GIF = 7;
  internal const int PICT_IMAGE = 0;
  internal const int PICT_JPEG = 5;
  internal const int PICT_META7 = 3;
  internal const int PICT_METAFILE = 1;
  internal const int PICT_PNG = 10;
  internal const int PICT_SHAPE = 11;
  internal const int PICT_TIFF = 8;
  internal const int PID_BULLET = 2;
  internal const int PID_LINE = 0;
  internal const int PID_PARA = 1;
  internal const int PID_STYLE = 4;
  internal const int PID_TAB = 3;
  internal static Cursor PlusCur;
  internal const int PNG_CIRC_BUF_HI = 1000;
  internal const int PNG_CIRC_BUF_LO = 500;
  internal const int PNG_MAX_RUN_LEN = 258;
  internal const int PNGCLR_GRAY = 0;
  internal const int PNGCLR_GRAY_ALPHA = 4;
  internal const int PNGCLR_PAL = 3;
  internal const int PNGCLR_TRUE = 2;
  internal const int PNGCLR_TRUE_ALPHA = 6;
  internal const int PNGCMP_DYNAMIC = 2;
  internal const int PNGCMP_ERROR = 3;
  internal const int PNGCMP_FIXED = 1;
  internal const int PNGCMP_NONE = 0;
  internal CPos pos;
  internal const int POS_BEG = 0;
  internal const int POS_END = 1;
  internal const int PPARAM_AUX = 5;
  internal const int PPARAM_AUX1 = 6;
  internal const int PPARAM_BK_COLOR = 14;
  internal const int PPARAM_BLT = 4;
  internal const int PPARAM_BORDER_SPACE = 15;
  internal const int PPARAM_FIRST_INDENT = 2;
  internal const int PPARAM_FLAGS = 17;
  internal const int PPARAM_FLOW = 16 /*0x10*/;
  internal const int PPARAM_LEFT_INDENT = 0;
  internal const int PPARAM_LINE_SPACING = 13;
  internal const int PPARAM_PFLAGS = 9;
  internal const int PPARAM_RIGHT_INDENT = 1;
  internal const int PPARAM_SHADING = 8;
  internal const int PPARAM_SPACE_AFT = 11;
  internal const int PPARAM_SPACE_BEF = 10;
  internal const int PPARAM_SPACE_BET = 12;
  internal const int PPARAM_STY = 7;
  internal const int PPARAM_TAB = 3;
  internal static Color PROTECT_COLOR;
  internal CPrt prt;
  internal const int PSPACE_15 = 1;
  internal const int PSPACE_20 = 2;
  internal const int PSPACE_ATLEAST = 3;
  internal const int PSPACE_EXACTLY = 4;
  internal const int PSPACE_MULTIPLE = 5;
  internal const int PSPACE_SINGLE = 0;
  internal static MethodInfo pSpellWord;
  internal static MethodInfo pStClearHist;
  internal static MethodInfo pStGetAlternateWord;
  internal static MethodInfo pStGetAlternateWordCount;
  internal static MethodInfo pStGetReplacement;
  internal static MethodInfo pStParseLine;
  internal static MethodInfo pStResetUserDict;
  internal static MethodInfo pStSetDictName;
  internal static MethodInfo pStSetLicenseKey;
  internal static MethodInfo pToSpellHist;
  internal static MethodInfo pToUserDict;
  internal const int PV_PAGE_DIST = 1;
  internal const char QUOTE = '"';
  internal const int REDUCTION = 2;
  public const int REL_SCREEN = 0;
  internal const int REL_TEXT_BOX = 2;
  internal const int REL_WINDOW = 1;
  internal const int RFLAG_CELL_MERGED = 1;
  internal const int RFLAG_CURS_IN_TABLE = 4;
  internal const int RFLAG_END_TABLE = 2;
  internal const int RFLAG_HDR_FTR_FOUND = 128 /*0x80*/;
  internal const int RFLAG_HDRFTR_TEXT_FOUND = 32 /*0x20*/;
  internal const int RFLAG_IGNORE_SLASH = 8192 /*0x2000*/;
  internal const int RFLAG_INSERT_BEF_PARA = 32768 /*0x8000*/;
  internal const int RFLAG_NEW_FRAME = 64 /*0x40*/;
  internal const int RFLAG_OUT_OF_TABLE_CELL = 16 /*0x10*/;
  internal const int RFLAG_OUT_OF_TABLE_ROW = 8;
  internal const int RFLAG_PARA_FRAME_FOUND = 4096 /*0x1000*/;
  internal const int RFLAG_READ_SHAPE_PICT = 16384 /*0x4000*/;
  internal const int RFLAG_SECT_FTR_FOUND = 2048 /*0x0800*/;
  internal const int RFLAG_SECT_HDR_FOUND = 1024 /*0x0400*/;
  internal const int RFLAG1_FIRST_TABLE = 64 /*0x40*/;
  internal const int RFLAG1_IGNORE_SPACE = 8192 /*0x2000*/;
  internal const int RFLAG1_NEW_HDR_FTR = 32768 /*0x8000*/;
  internal const int RFLAG1_PARA_ENDED = 32 /*0x20*/;
  internal const int RFLAG1_PASTING_IN_PARA_FRAME = 16384 /*0x4000*/;
  internal const int RFLAG1_PNG_FOUND = 4;
  internal const int RFLAG1_ROW_SPANNED = 4096 /*0x1000*/;
  internal const int RFLAG1_SECT_FFTR_FOUND = 16 /*0x10*/;
  internal const int RFLAG1_SECT_FHDR_FOUND = 8;
  internal const int RFLAG1_SSSUBTABLE_USED = 128 /*0x80*/;
  internal const int RFLAG1_TEXT_FOUND = 2;
  internal const int RFLAG1_TROWD_FOUND = 1;
  internal const int RFLAG2_CHFTN_FOUND = 2;
  internal const int RFLAG2_LEFT_MARG_FOUND = 1;
  internal const int RFLAG2_PICT_FRAME_INSERTED = 8;
  internal const int RFLAG2_UNDO_IN_EMPTY_FILE = 16 /*0x10*/;
  internal const int RIGHT_JUSTIFY = 2;
  internal const int ROFLAG_CELL_CLOSED = 1024 /*0x0400*/;
  internal const int ROFLAG_HIDDEN_TEXT = 256 /*0x0100*/;
  internal const int ROFLAG_IGNORE_SLASH = 2;
  internal const int ROFLAG_IN_ANIM = 64 /*0x40*/;
  internal const int ROFLAG_IN_FIELD_NAME = 512 /*0x0200*/;
  internal const int ROFLAG_IN_FIELD_TC = 4096 /*0x1000*/;
  internal const int ROFLAG_IN_FIELD_XE = 8192 /*0x2000*/;
  internal const int ROFLAG_IN_STYLESHEET = 65536 /*0x010000*/;
  internal const int ROFLAG_IN_SUBSCR = 8;
  internal const int ROFLAG_IN_SUPSCR = 4;
  internal const int ROFLAG_NO_ANIM = 128 /*0x80*/;
  internal const int ROFLAG_NO_CRLF = 2048 /*0x0800*/;
  internal const int ROFLAG_NO_FLUSH = 32 /*0x20*/;
  internal const int ROFLAG_WRITE_FONT = 16384 /*0x4000*/;
  internal const int ROFLAG_WRITE_TROWD = 16 /*0x10*/;
  internal const int ROGFLAG_WRITE_FONT = 1;
  internal const char ROW_CHAR = '\u0012';
  internal const int ROWFLAG_HAS_SUBTABLE = 8;
  internal const int ROWFLAG_HDR = 4;
  internal const int ROWFLAG_KEEP = 8192 /*0x2000*/;
  internal const int ROWFLAG_NEW_TABLE = 32768 /*0x8000*/;
  internal const int ROWFLAG_PREEXIST = 16384 /*0x4000*/;
  internal const int ROWFLAG_RTL = 65536 /*0x010000*/;
  internal const int ROWFLAG_SPLIT = 16 /*0x10*/;
  internal const int RTF_BEGIN = 1;
  internal const int RTF_BUF = 1;
  internal const int RTF_BUF_LEN = 4096 /*0x1000*/;
  internal const int RTF_CB = 2;
  internal const int RTF_CB_APPEND = 5;
  internal const int RTF_CB_BUF = 3;
  internal const int RTF_CONTROL = 3;
  internal const int RTF_END = 2;
  internal const int RTF_END_FILE = 3;
  internal const int RTF_FILE = 0;
  internal const int RTF_FILE_INCOMPLETE = 1;
  internal const int RTF_FRAME_BUF = 4;
  internal const int RTF_LINE_LEN = 80 /*0x50*/;
  internal const int RTF_OBJECT = 8;
  internal const int RTF_PICT = 6;
  internal const int RTF_PROPERTY = 5;
  internal const int RTF_SECT = 7;
  internal const int RTF_SYNTAX_ERROR = 2;
  internal const int RTF_TEXT = 4;
  internal CRtfr rtfr;
  internal CRtfw rtfw;
  internal const int RULER_TOLERANCE = 60;
  internal const int SAVE_DEFAULT = 3;
  internal const int SAVE_HTML = 4;
  internal const int SAVE_RTF = 2;
  internal const int SAVE_TEXT = 0;
  internal const int SAVE_TEXT_LINES = 1;
  internal const int SAVE_UTEXT = 5;
  internal const int SAVE_UTF7TEXT = 6;
  internal const int SAVE_UTF8TEXT = 7;
  internal const int SCOPE_ANY = 3;
  internal const int SCOPE_BACKWARD = 2;
  internal const int SCOPE_BEGIN = 0;
  internal const int SCOPE_FORWARD = 1;
  internal const int SCROLL_RANGE = 1000;
  internal CSec sec;
  internal const int SECT_ALL = -1;
  internal const int SECT_BRDR_FOOTER = 64 /*0x40*/;
  internal const int SECT_BRDR_HEADER = 32 /*0x20*/;
  internal const char SECT_CHAR = '\u0014';
  internal const int SECT_CUR = -2;
  internal const int SECT_EXACT_MARGB = 16 /*0x10*/;
  internal const int SECT_EXACT_MARGT = 8;
  internal const int SECT_HAS_TITLE_PAGE = 4;
  internal const int SECT_LINE = 512 /*0x0200*/;
  internal const int SECT_LINE_DIST = 360;
  internal const int SECT_NEW_PAGE = 1;
  internal const int SECT_RESTART_PAGE_NO = 2;
  internal const int SECT_SNAP_LINE_GRID = 1024 /*0x0400*/;
  internal const int SECT_VALIGN_TOP = 0;
  internal const int SECT_VALIGN_BOT = 256 /*0x0100*/;
  internal const int SECT_VALIGN_CTR = 128 /*0x80*/;
  internal const int SEL_ALL = 4;
  internal const int SEL_CELLS = 1;
  internal const int SEL_COLS = 2;
  internal const int SEL_ROWS = 3;
  internal const int SHPTYPE_ELLIPSE = 3;
  internal const int SHPTYPE_LINE = 20;
  internal const int SHPTYPE_NONE = -1;
  internal const int SHPTYPE_OCX = 201;
  internal const int SHPTYPE_PICT_FRAME = 75;
  internal const int SHPTYPE_RECT = 1;
  internal const int SHPTYPE_TEXT_BOX = 202;
  internal const int SID_CUR = -9998;
  internal const int SID_NORMAL = -9999;
  internal const int SIGN_EOF16 = 188;
  internal const int SIGN_EOF32 = 186;
  internal static bool SkipBool;
  internal static byte SkipByte;
  internal static byte[] SkipBytes;
  internal static char[] SkipChars;
  internal static Color SkipColor;
  internal static int SkipInt;
  internal static int[] SkipIntArray;
  internal static int[] SkipInts;
  internal static object SkipObject;
  internal static COp.RECT SkipRect;
  internal static tc.StrRtfColor SkipRtfColor = new tc.StrRtfColor();
  internal static tc.StrRtfObject SkipRtfObject = new tc.StrRtfObject();
  internal static tc.StrRtfPict SkipRtfPict = new tc.StrRtfPict();
  internal static string SkipStr;
  internal static int SkipUint;
  internal static ushort[] SkipUshortArray;
  internal const int SMALL_FILE = 5000;
  internal const int SMTER_MOUSEWHEELPRESENT = 75;
  internal const int SNAP_DIST = 90;
  internal const int SNAP_DIST_METRIC = 71;
  internal const int SP_FLAGS = 1;
  internal const int SP_LINE_STEP = 2;
  internal const int SPILL_BOT = 8;
  internal const int SPILL_LEFT = 1;
  internal const int SPILL_RIGHT = 2;
  internal const int SPILL_TOP = 4;
  internal CSpl spl;
  internal const int SRCH_BACK = 128 /*0x80*/;
  internal const int SRCH_CASE = 16 /*0x10*/;
  internal const int SRCH_NO_REPLACE_PROT_TEXT = 256 /*0x0100*/;
  internal const int SRCH_REPLACE = 4;
  internal const int SRCH_RETRIEVE = 8;
  internal const int SRCH_SCROLL = 1;
  public const int SRCH_SEARCH = 2;
  internal const int SRCH_SKIP_HIDDEN_TEXT = 64 /*0x40*/;
  internal const int SRCH_WORD = 32 /*0x20*/;
  internal const int SSFLAG_ADD = 1;
  internal const int SSINFO_CHAR_OFFSET = 3;
  internal const int SSINFO_CHAR_SPACE = 2;
  internal const int SSINFO_NAME = 1;
  internal const int SSINFO_NEXT = 4;
  internal static bool SspSearched;
  internal const int SSTYPE_CHAR = 1;
  internal const int SSTYPE_PARA = 2;
  internal static int ST_ADD;
  internal static int ST_ERROR;
  internal static int ST_EXIT;
  internal static int ST_IGNORE;
  internal static int ST_INPUT = 0;
  internal static int ST_INTERACTIVE;
  internal static int ST_MAX_SUG_WORDS;
  internal static int ST_MAX_WORD_LEN;
  internal static int ST_REPLACE;
  internal const int STATUS_ROW = -1;
  internal static string StnKey;
  internal static bool StSearched;
  internal static System.Type StType;
  internal const int SWRAP_AROUND = 2;
  internal const int SWRAP_IGNORE = 3;
  internal const int SWRAP_NO_WRAP = 1;
  internal const int SWRAP_THRU = 5;
  internal const int SWRAP_TIGHT = 4;
  internal const byte SYMBOL_CHARSET = 2;
  internal const char TAB = '\t';
  internal const int TAB_CENTER = 2;
  internal const int TAB_DECIMAL = 3;
  internal const int TAB_DOT = 1;
  internal const int TAB_HYPH = 2;
  internal const int TAB_LEFT = 0;
  internal const int TAB_NONE = 0;
  internal const int TAB_RIGHT = 1;
  internal const int TAB_ULINE = 4;
  internal static Cursor Tab1Cur;
  internal static Cursor Table1Cur;
  internal static Cursor Table2Cur;
  internal static Cursor Table3Cur;
  internal const int TAUX_COPIED = 32 /*0x20*/;
  internal const int TAUX_HAS_NEW_TEXT = 16 /*0x10*/;
  internal const int TAUX_REUSE_CELL_CHAIN = 2;
  internal const int TAUX_RTF_DEL = 4;
  internal const int TAUX_TROWD_DONE = 1;
  internal CTbl tbl;
  internal const int TE_MAX_TAB_STOPS = 20;
  internal const int TER_ACTION = 2731;
  internal const int TER_ANIM_PICT = 2735;
  internal const int TER_BORDER_MARGIN = 64 /*0x40*/;
  internal const int TER_BUF = 2;
  internal const int TER_CENTER = 1;
  internal const int TER_CLOSE = 2724;
  internal const int TER_CUR = 5;
  internal const int TER_FILE = 1;
  internal const int TER_FIRST = 0;
  internal const int TER_FITTED_VIEW = 8192 /*0x2000*/;
  internal const int TER_HSCROLL = 4;
  internal const int TER_HTML_SIZE = 2734;
  internal const int TER_IDLE = 1034;
  internal const int TER_INVISIBLE = 4096 /*0x1000*/;
  internal const int TER_LAST = 1;
  internal const int TER_LEFT = 0;
  internal const int TER_LINK = 2726;
  internal const int TER_MERGE = 2727;
  internal const int TER_MODIFIED = 2725;
  internal const int TER_NEXT = 2;
  internal const int TER_NOT_SAVED = 2728;
  internal const int TER_NUM_STYLES = 14;
  internal const int TER_OUTPUT_RTF = 128 /*0x80*/;
  internal const int TER_PAGE_MODE = 1024 /*0x0400*/;
  internal const int TER_PAGE_SIZE_CHANGING = 2738;
  internal const int TER_PREPROCESS = 2732;
  internal const int TER_PREV = 4;
  internal const int TER_PRINT_VIEW = 2;
  internal const int TER_READ_ONLY = 512 /*0x0200*/;
  internal const int TER_REPAINT = 2736;
  internal const int TER_RIGHT = 2;
  internal const int TER_SHOW_RULER = 32 /*0x20*/;
  internal const int TER_SHOW_STATUS = 16 /*0x10*/;
  internal const int TER_SHOW_TOOLBAR = 2048 /*0x0800*/;
  internal const int TER_UPDATE_STATUSBAR = 2730;
  internal const int TER_UPDATE_TOOLBAR = 2729;
  internal const int TER_USE_PARAMS = 256 /*0x0100*/;
  internal const int TER_VSCROLL = 8;
  internal const int TER_WM_COMMAND = 2737;
  internal const int TER_WORD_WRAP = 1;
  internal const int TER_XLATE_PICT = 2733;
  internal static int TerInitFlags;
  internal static int TerInitFlags2;
  internal static int TerInitFlags3;
  internal static int TerInitFlags4;
  internal static int TerInitFlags5;
  internal static int TerInitFlags6;
  internal static int TerOpenCount;
  internal static int TerStyles;
  internal const int TERTAG_BKM = 1;
  internal const int TERTAG_HEADING = 3;
  internal const int TERTAG_HPARA = 5;
  internal const int TERTAG_HPARA2 = 6;
  internal const int TERTAG_SECT = 4;
  internal const int TERTAG_TOC = 2;
  internal const int TERTAG_USER = 0;
  /// <summary>Защищённый текст</summary>
  public const int TERTAG_PROTECTED = 77;
  /// <summary>Заменённый символ</summary>
  public const int TERTAG_REPLACEDCHAR = 78;
  /// <summary>Автоматически заменённый символ</summary>
  public const int TERTAG_AUTOREPLACEDCHAR = 79;
  /// <summary>Вручную заменённый символ</summary>
  public const int TERTAG_MANUALREPLACEDCHAR = 80 /*0x50*/;
  /// <summary>Стиль формулы</summary>
  public const int TERTAG_FORMULAFORMAT = 81;
  public static int[] ReplacedCharTags = new int[3]
  {
    79,
    80 /*0x50*/,
    78
  };
  internal const int TEXT_BOT_TO_TOP = 2;
  internal const int TEXT_HORZ = 0;
  internal const int TEXT_TOP_TO_BOT = 1;
  internal const int TEXTFLOW_B2T = 2;
  internal const int TEXTFLOW_T2B = 1;
  internal const int TEXTFLOW_T2B_NON_ANSII = 3;
  internal const int TFLAG_ACTIVATE_MDI_CHILD = 4096 /*0x1000*/;
  internal const int TFLAG_APPLY_PRT_ORIENT = 8;
  internal const int TFLAG_APPLY_PRT_PROPS = 8;
  internal const int TFLAG_AUTO_VSCROLL_BAR = 32768 /*0x8000*/;
  internal const int TFLAG_BUF_DISP = 134217728 /*0x08000000*/;
  internal const int TFLAG_COMP_WORD97 = 8192 /*0x2000*/;
  internal const int TFLAG_DISABLE_ACCEL = 32 /*0x20*/;
  internal const int TFLAG_EXCLUDE_HIDDEN_SEL = 16384 /*0x4000*/;
  internal const int TFLAG_IGNORE_PICT_LINK = 16 /*0x10*/;
  internal const int TFLAG_KEEP_FRAME_ASPECT = 262144 /*0x040000*/;
  internal const int TFLAG_KEEP_PICT_ASPECT = 131072 /*0x020000*/;
  internal const int TFLAG_METRIC = 2;
  internal const int TFLAG_NO_AUTO_FULL_CELL_SEL = 1073741824 /*0x40000000*/;
  internal const int TFLAG_NO_CHILD_TOP = 1024 /*0x0400*/;
  internal const int TFLAG_NO_DRAG_TEXT = 2097152 /*0x200000*/;
  internal const int TFLAG_NO_EDIT_OLE = 4194304 /*0x400000*/;
  internal const int TFLAG_NO_EDIT_PICT = 16777216 /*0x01000000*/;
  internal const int TFLAG_NO_HOUR_GLASS = 512 /*0x0200*/;
  internal const int TFLAG_NO_OLE = 268435456 /*0x10000000*/;
  internal const int TFLAG_NO_PALETTE = 65536 /*0x010000*/;
  internal const int TFLAG_NO_PRINTER = 1048576 /*0x100000*/;
  internal const int TFLAG_NO_WRAP = 2048 /*0x0800*/;
  internal const int TFLAG_PICT_IN_FRAME = 524288 /*0x080000*/;
  internal const int TFLAG_RESIZE_BITMAP = 1;
  internal const int TFLAG_RETURN_MSG_ID = 4;
  internal const int TFLAG_ROW_PASTE = 536870912 /*0x20000000*/;
  internal const int TFLAG_SELECT_FULL_HLINK = 67108864 /*0x04000000*/;
  internal const int TFLAG_SHOW_BREAKS = 33554432 /*0x02000000*/;
  internal const int TFLAG_SHOW_CARET = 128 /*0x80*/;
  internal const int TFLAG_SWAP_DECIMAL = 8388608 /*0x800000*/;
  internal const int TFLAG_UNPROTECTED_DEL = 256 /*0x0100*/;
  internal const int TFLAG_USE_NEXT_ID = 64 /*0x40*/;
  internal const int TFLAG2_ALT_LINE_SYM = 8388608 /*0x800000*/;
  internal const int TFLAG2_ALT_PARA_SYM = 4194304 /*0x400000*/;
  internal const int TFLAG2_BKPICT_OVER_PAGE_BORDER = 16 /*0x10*/;
  internal const int TFLAG2_CAN_MERGE_PROT_TEXT = 8;
  internal const int TFLAG2_COMP_WPF = 1;
  internal const int TFLAG2_CURSOR_BEF_HIDDEN = 536870912 /*0x20000000*/;
  internal const int TFLAG2_FULL_REPAINT = 1048576 /*0x100000*/;
  internal const int TFLAG2_HIDE_PAGE_BREAK = 8192 /*0x2000*/;
  internal const int TFLAG2_IGNORE_TIMER = 131072 /*0x020000*/;
  internal const int TFLAG2_INDENT_FRAMES = 67108864 /*0x04000000*/;
  internal const int TFLAG2_INDENT_TABLES = 134217728 /*0x08000000*/;
  internal const int TFLAG2_KEEP_PRINTER_OPEN = 2097152 /*0x200000*/;
  internal const int TFLAG2_NO_ADJUST_CURSOR = 268435456 /*0x10000000*/;
  internal const int TFLAG2_NO_AUTO_HDR_FTR = 262144 /*0x040000*/;
  internal const int TFLAG2_NO_AUTO_REPAGE = 1024 /*0x0400*/;
  internal const int TFLAG2_NO_BKP_FILE = 2048 /*0x0800*/;
  internal const int TFLAG2_NO_CARET = 64 /*0x40*/;
  internal const int TFLAG2_NO_CURSOR_CHANGE = 256 /*0x0100*/;
  internal const int TFLAG2_NO_CURSOR_ON_PROTECT = 1073741824 /*0x40000000*/;
  internal const int TFLAG2_NO_HIDDEN_RTF_TEXT = 32768 /*0x8000*/;
  internal const int TFLAG2_NO_LINE_FITTING = 16777216 /*0x01000000*/;
  internal const int TFLAG2_NO_PRT_CANCEL_DLG = 33554432 /*0x02000000*/;
  internal const int TFLAG2_NO_SHADE_FIELD_TEXT = 65536 /*0x010000*/;
  internal const int TFLAG2_PROTECT_FORMAT = 16384 /*0x4000*/;
  internal const int TFLAG2_RETAIN_BKND = 2;
  internal const int TFLAG2_SELECT_FRAME_PICT = 4096 /*0x1000*/;
  internal const int TFLAG2_SHOW_SECT_PAGE_NO = 128 /*0x80*/;
  internal const int TFLAG2_USE_PAL_FOR_TEXT = 4;
  internal const int TFLAG2_VERT_THUMB_TRACK = 512 /*0x0200*/;
  internal const int TFLAG2_WRITE_FIRST_RTF_COLOR = 524288 /*0x080000*/;
  internal const int TFLAG2_XPARENT_WRITE = 32 /*0x20*/;
  internal const int TFLAG3_CLIP_CELL_OVERFLOW = 33554432 /*0x02000000*/;
  internal const int TFLAG3_CURSOR_IN_CELL = 32 /*0x20*/;
  internal const int TFLAG3_DATA_FIELD_INPUT = 16384 /*0x4000*/;
  internal const int TFLAG3_EMBEDDED_TABLES = 8192 /*0x2000*/;
  internal const int TFLAG3_EXACT_CELL_WIDTH = 64 /*0x40*/;
  internal const int TFLAG3_EXACT_SCREEN_FONT = 67108864 /*0x04000000*/;
  internal const int TFLAG3_GET_BUF_HDR_FTR = 32768 /*0x8000*/;
  internal const int TFLAG3_GRAY_READ_ONLY = 16 /*0x10*/;
  internal const int TFLAG3_HTML_CONT_TABLE = 512 /*0x0200*/;
  internal const int TFLAG3_HTML_READ = 65536 /*0x010000*/;
  internal const int TFLAG3_LARGE_PARA_BORDER = 524288 /*0x080000*/;
  internal const int TFLAG3_LINE_SCROLL = 4194304 /*0x400000*/;
  internal const int TFLAG3_MULTIPLE_RTF_GROUPS = 2048 /*0x0800*/;
  internal const int TFLAG3_NO_EDIT_TABLE_COL = 2;
  internal const int TFLAG3_NO_FULL_CELL_COPY = 256 /*0x0100*/;
  internal const int TFLAG3_NO_MOUSE_SEL = 1073741824 /*0x40000000*/;
  internal const int TFLAG3_NO_RTF_BKND_COLOR = 131072 /*0x020000*/;
  internal const int TFLAG3_NO_SAVE_UNDO = 262144 /*0x040000*/;
  internal const int TFLAG3_NO_SCROLL = 128 /*0x80*/;
  internal const int TFLAG3_NO_TEXT_COLOR_ADJ = 536870912 /*0x20000000*/;
  internal const int TFLAG3_OLD_WORD_FORMAT = 4096 /*0x1000*/;
  internal const int TFLAG3_PASTE_RTF_CODE = 16777216 /*0x01000000*/;
  internal const int TFLAG3_PLAIN_TABLE_BORDER = 8;
  internal const int TFLAG3_PRINT_BKND_PICT = 8388608 /*0x800000*/;
  internal const int TFLAG3_READ_PNG = 268435456 /*0x10000000*/;
  internal const int TFLAG3_SELECT_FIRST_FIELD = 1024 /*0x0400*/;
  internal const int TFLAG3_SHOW_FRAMES = 2097152 /*0x200000*/;
  internal const int TFLAG3_STYLES_ON_TOOLBAR = 1048576 /*0x100000*/;
  internal const int TFLAG3_TABLE_STATUS_LINE = 4;
  internal const int TFLAG3_WRAP_SPACES = 1;
  internal const int TFLAG3_ZERO_CELL_HEIGHT = 134217728 /*0x08000000*/;
  internal const int TFLAG4_ADJ_LEFT_TABLE_COL = 134217728 /*0x08000000*/;
  internal const int TFLAG4_ALWAYS_INVOKE_OLE = 65536 /*0x010000*/;
  internal const int TFLAG4_AUTO_SPELL = 256 /*0x0100*/;
  internal const int TFLAG4_BINARY_RTF_PICT = 512 /*0x0200*/;
  internal const int TFLAG4_COUNT_PCHAR_AS_CRLF = 1;
  internal const int TFLAG4_DISABLE_DATE_UPDATE = 131072 /*0x020000*/;
  internal const int TFLAG4_DONT_FIX_NEG_INDENT = 268435456 /*0x10000000*/;
  internal const int TFLAG4_FULL_DRAG_PROT_TEXT = 4194304 /*0x400000*/;
  internal const int TFLAG4_HIDE_HIDDEN_PARA_MARK = 4;
  internal const int TFLAG4_HTML_INPUT = 1048576 /*0x100000*/;
  internal const int TFLAG4_IME_UNICODE = 2048 /*0x0800*/;
  internal const int TFLAG4_MOD_END_MARK_FONT = 4096 /*0x1000*/;
  internal const int TFLAG4_NO_BUFFERED_SCROLL = 64 /*0x40*/;
  internal const int TFLAG4_NO_DRAG_PROT_TEXT = 2097152 /*0x200000*/;
  internal const int TFLAG4_NO_MERGE_TABLE = 8388608 /*0x800000*/;
  internal const int TFLAG4_NO_OLE_DROP = 16384 /*0x4000*/;
  internal const int TFLAG4_NO_REPAGINATE = 16 /*0x10*/;
  internal const int TFLAG4_NO_RESET_DC = 33554432 /*0x02000000*/;
  internal const int TFLAG4_NO_SHARE_BORDER = 67108864 /*0x04000000*/;
  internal const int TFLAG4_NO_TOC_UPDATE = 16777216 /*0x01000000*/;
  internal const int TFLAG4_ONE_ROW_TOOLBAR = 8192 /*0x2000*/;
  internal const int TFLAG4_PRINT_EMF_AS_BMP = 536870912 /*0x20000000*/;
  internal const int TFLAG4_READONLY_CONTROLS = 8;
  internal const int TFLAG4_REPORT_EASE_MODE = 32768 /*0x8000*/;
  internal const int TFLAG4_SAVE_BMP_AS_PNG = 262144 /*0x040000*/;
  internal const int TFLAG4_SAVE_SHAPE_WITH_DRAW_OBJECT = 524288 /*0x080000*/;
  internal const int TFLAG4_SKIP_PROT_TEXT = 2;
  internal const int TFLAG4_SMOOTH_SCROLL = 32 /*0x20*/;
  internal const int TFLAG4_TEXT_HIDDEN = 128 /*0x80*/;
  internal const int TFLAG4_UNDO_WINDOW_OVERFLOW = 1024 /*0x0400*/;
  internal const int TFLAG5_BEF_AND_AFT_HIDDEN = 2048 /*0x0800*/;
  internal const int TFLAG5_FRAME_TEXT_ONLY = 268435456 /*0x10000000*/;
  internal const int TFLAG5_FULL_REPAGINATE = 16384 /*0x4000*/;
  internal const int TFLAG5_GROUP_UNDO = 4096 /*0x1000*/;
  internal const int TFLAG5_NO_ADJ_FOR_TABLE = 33554432 /*0x02000000*/;
  internal const int TFLAG5_NO_CLEAR_SPL_HIST = 8192 /*0x2000*/;
  internal const int TFLAG5_NO_DRAG_CELL_LINE = 1048576 /*0x100000*/;
  internal const int TFLAG5_NO_DRAG_ROW_LINE = 131072 /*0x020000*/;
  internal const int TFLAG5_NO_EXT_DROP = 1;
  internal const int TFLAG5_NO_NORMALIZE_FIELD = 1024 /*0x0400*/;
  internal const int TFLAG5_NO_NORMALIZE_FNOTE = 256 /*0x0100*/;
  internal const int TFLAG5_NO_SHARE = 134217728 /*0x08000000*/;
  internal const int TFLAG5_NO_SHOW_SPACE_SYM = 32768 /*0x8000*/;
  internal const int TFLAG5_NO_SPELLTIME = 1073741824 /*0x40000000*/;
  internal const int TFLAG5_OLD_HLINK = 65536 /*0x010000*/;
  internal const int TFLAG5_OLD_RULER = 67108864 /*0x04000000*/;
  internal const int TFLAG5_PRINT_PREVIEW_DLG = 2097152 /*0x200000*/;
  internal const int TFLAG5_PROTECT_DATA_FIELD = 536870912 /*0x20000000*/;
  internal const int TFLAG5_RULER_INDENT_FIXED = 4194304 /*0x400000*/;
  internal const int TFLAG5_SET_FORM_TITLE = 2;
  internal const int TFLAG5_SHOW_PAGE_BREAK = 262144 /*0x040000*/;
  internal const int TFLAG5_TOP_ROW_TOOLBAR = 8388608 /*0x800000*/;
  internal const int TFLAG5_VARIABLE_PAGE_SIZE = 524288 /*0x080000*/;
  internal const int TFLAG5_WRITE_DOB = 32 /*0x20*/;
  internal const int TFLAG5_XP_TOOLBAR = 16777216 /*0x01000000*/;
  internal const int TFLAG6_ALLOW_CELL_OVERFLOW = 2048 /*0x0800*/;
  internal const int TFLAG6_CENTER_DLGS_ON_SCR = 1048576 /*0x100000*/;
  internal const int TFLAG6_DONT_PROCESS_BULLET_KEYS = 32 /*0x20*/;
  internal const int TFLAG6_DONT_USE_SPELLTIME = 128 /*0x80*/;
  internal const int TFLAG6_DONT_WRITE_PICT_PATH = 16 /*0x10*/;
  internal const int TFLAG6_INSERT_DROP_PICT_AS_LINK = 4096 /*0x1000*/;
  internal const int TFLAG6_LIST_TO_TEXT_IN_HTML = 32768 /*0x8000*/;
  internal const int TFLAG6_MBCS_HTML_OUTPUT = 33554432 /*0x02000000*/;
  internal const int TFLAG6_NO_LINK_MSG = 131072 /*0x020000*/;
  internal const int TFLAG6_NO_TRACK_MSG = 262144 /*0x040000*/;
  internal const int TFLAG6_OLE_DROP_SOURCE = 524288 /*0x080000*/;
  internal const int TFLAG6_SAVE_TEXT_BK_COLOR = 16384 /*0x4000*/;
  internal const int TFLAG6_SWAP_CR_LINE_BREAK = 67108864 /*0x04000000*/;
  internal const int TFLAG6_TRACK_API = 2097152 /*0x200000*/;
  internal const int TFLAG6_USE_PEN_FOR_BORDER = 1;
  internal const int TFLAG6_WORDPAD_COMPATIBLE_PICT = 64 /*0x40*/;
  internal const int TFLAG6_WRAP_AT_WINDOW_WIDTH = 8192 /*0x2000*/;
  internal const int TFLAG6_WRITE_DEFAULT_COLOR = 4;
  internal const int TIMER_ANIM = 9199;
  internal const int TIMER_EVAL = 9184;
  internal const int TIMER_HILIGHT = 9183;
  internal const int TIMER_MOUSE_STOP = 9190;
  internal const int TIMER_REPAGE = 9182;
  internal const int TIMER_TLB_BALLOON = 9186;
  internal const int TIMER_TLB_BALLOON_OFF = 9187;
  internal const int TIMER_TLB_HILIGHT = 9185;
  internal const int TIMER_WHEEL_DOWN = 9189;
  internal const int TIMER_WHEEL_UP = 9188;
  internal const int TLB_ALIGN_CENTER = 9;
  internal const int TLB_ALIGN_JUSTIFY = 10;
  internal const int TLB_ALIGN_LEFT = 7;
  internal const int TLB_ALIGN_RIGHT = 8;
  internal const int TLB_BOLD = 4;
  internal const int TLB_BULLET = 27;
  internal const int TLB_COPY = 16 /*0x10*/;
  internal const int TLB_CUT = 15;
  internal const int TLB_DATE = 31 /*0x1F*/;
  internal const int TLB_DEC_INDENT = 12;
  internal const int TLB_FIND = 30;
  internal const int TLB_HELP = 23;
  internal const int TLB_ICON_HEIGHT = 26;
  internal const int TLB_INC_INDENT = 11;
  internal const int TLB_ITALIC = 5;
  internal const int TLB_LINE = 1;
  internal const int TLB_MARGIN = 3;
  internal const int TLB_NEW = 19;
  internal const int TLB_NUMBER = 26;
  internal const int TLB_OPEN = 20;
  internal const int TLB_PAGE_COUNT = 33;
  internal const int TLB_PAGE_NUM = 32 /*0x20*/;
  internal const int TLB_PAR = 24;
  internal const int TLB_PASTE = 17;
  internal const int TLB_POINTSIZE = 3;
  internal const int TLB_PREVIEW = 25;
  internal const int TLB_PRINT = 22;
  internal const int TLB_REDO = 29;
  internal const int TLB_SAVE = 21;
  internal const int TLB_SPACER = 18;
  internal const int TLB_SPACER_WIDTH = 3;
  internal const int TLB_STYLE = 13;
  internal const int TLB_TYPEFACE = 2;
  internal const int TLB_ULINE = 6;
  internal const int TLB_UNDO = 28;
  internal const int TLB_ZOOM = 14;
  internal static string TlbClass;
  internal const int TLBFLAG_CHECKED = 4;
  internal const int TLBFLAG_COMBO = 16 /*0x10*/;
  internal const int TLBFLAG_EDITABLE = 32 /*0x20*/;
  internal const int TLBFLAG_ENABLED = 2;
  internal const int TLBFLAG_HILIGHTED = 8;
  internal const int TLBFLAG_MOUSE = 1;
  internal const int TOFLAG_BREAK_CHAR = 1048576 /*0x100000*/;
  internal const int TOFLAG_CLEAR_EOL = 536870912 /*0x20000000*/;
  internal const int TOFLAG_CONTINUOUS_SCROLL = 33554432 /*0x02000000*/;
  internal const int TOFLAG_CURS_ON_TABLE_TOP = 2;
  internal const int TOFLAG_DEBUG = 4096 /*0x1000*/;
  internal const int TOFLAG_DEL_CELL = 1;
  internal const int TOFLAG_DRAWING_FRAME_PICT = 512 /*0x0200*/;
  internal const int TOFLAG_EXT_SPELLER = 268435456 /*0x10000000*/;
  internal const int TOFLAG_EXTERNAL_DC = 16 /*0x10*/;
  internal const int TOFLAG_FNOTE_FRAME = 1024 /*0x0400*/;
  internal const int TOFLAG_FULL_PAINT = 128 /*0x80*/;
  internal const int TOFLAG_HILIGHT_OFF = 8192 /*0x2000*/;
  internal const int TOFLAG_IN_MERGE_PRINT = 8;
  internal const int TOFLAG_INSERT_CHAR = 4;
  internal const int TOFLAG_MERGE_PRINT_RECT = 131072 /*0x020000*/;
  internal const int TOFLAG_NO_ADJUST_POS = 65536 /*0x010000*/;
  internal const int TOFLAG_NO_FIELD_ADJ = 32768 /*0x8000*/;
  internal const int TOFLAG_NO_MORE_TAB_MSG = 67108864 /*0x04000000*/;
  internal const int TOFLAG_NO_RECOVE_ID = 16777216 /*0x01000000*/;
  internal const int TOFLAG_NO_RESET_FONT = 64 /*0x40*/;
  internal const int TOFLAG_NO_TOL = 16384 /*0x4000*/;
  internal const int TOFLAG_PAINT_WAITING = 2048 /*0x0800*/;
  internal const int TOFLAG_REPAINT_MSG = 2097152 /*0x200000*/;
  internal const int TOFLAG_SEL_CELL_CONTENT = 8388608 /*0x800000*/;
  internal const int TOFLAG_SET_FOCUS = 262144 /*0x040000*/;
  internal const int TOFLAG_SET_MOUSE_ON_TEXT_LINE = 4194304 /*0x400000*/;
  internal const int TOFLAG_UPDATE_ENOTE = 1073741824 /*0x40000000*/;
  internal const int TOFLAG_WRAPPING = 256 /*0x0100*/;
  internal const int TOFLAG_WRITING_BKND = 32 /*0x20*/;
  internal const int TOFLAG_X_BEFORE_LINE = 134217728 /*0x08000000*/;
  internal const int TOFLAG_X_BEYOND_LINE = 524288 /*0x080000*/;
  internal const int TOFLAG2_CHECK_DISABLED_FRAMES = 512 /*0x0200*/;
  internal const int TOFLAG2_DEL_CELL_TEXT = 2048 /*0x0800*/;
  internal const int TOFLAG2_DRAW_FNOTE = 8;
  internal const int TOFLAG2_FROM_CB = 64 /*0x40*/;
  internal const int TOFLAG2_FULL_REPAGE = 131072 /*0x020000*/;
  internal const int TOFLAG2_NO_COMPRESS_CFMT = 256 /*0x0100*/;
  internal const int TOFLAG2_NO_NORMALIZE = 4096 /*0x1000*/;
  internal const int TOFLAG2_NO_NORMALIZE_FNOTE = 2;
  internal const int TOFLAG2_NO_SAVE_UNDO = 8192 /*0x2000*/;
  internal const int TOFLAG2_NO_SET_LANG = 1024 /*0x0400*/;
  internal const int TOFLAG2_NO_WRAP = 1;
  internal const int TOFLAG2_PRINTING_TO_EMF = 16384 /*0x4000*/;
  internal const int TOFLAG2_REPOS_PICT_FRAMES = 4;
  internal const int TOFLAG2_SSE_READ = 16 /*0x10*/;
  internal const int TOFLAG2_USE_PEN_FOR_BORDER = 32 /*0x20*/;
  internal const int TOFLAG2_USE_UNDO_FRAME = 65536 /*0x010000*/;
  internal static Bitmap ToolbarBM;
  internal static int TotalBaseColors;
  internal static int TotalDateFmts;
  internal static int TotalGlbFonts;
  internal static int TotalPalColors;
  internal static int TotalWinPtrs;
  internal const int TP_CUR_COL = 17;
  internal const int TP_CUR_LINE = 16 /*0x10*/;
  internal const int TP_CUR_SECT = 18;
  internal const int TP_PAGE_BK_COLOR = 26;
  internal const int TP_SELECTION_END_COL = 23;
  internal const int TP_SELECTION_END_LINE = 22;
  internal const int TP_SELECTION_START_COL = 21;
  internal const int TP_SELECTION_START_LINE = 20;
  internal const int TP_SELECTION_TYPE = 19;
  internal const int TP_TOTAL_BLTS = 9;
  internal const int TP_TOTAL_CELLS = 14;
  internal const int TP_TOTAL_CHAR_TAGS = 7;
  internal const int TP_TOTAL_FONTS = 3;
  internal const int TP_TOTAL_IMAGE_MAPS = 8;
  internal const int TP_TOTAL_LINES = 1;
  internal const int TP_TOTAL_LIST_OR = 11;
  internal const int TP_TOTAL_LISTS = 10;
  internal const int TP_TOTAL_PAGES = 5;
  internal const int TP_TOTAL_PARA_FRAMES = 6;
  internal const int TP_TOTAL_PFMTS = 2;
  internal const int TP_TOTAL_SECTS = 15;
  internal const int TP_TOTAL_STYLES = 4;
  internal const int TP_TOTAL_TABLE_ROWS = 13;
  internal const int TP_TOTAL_TABS = 12;
  internal const int TP_WATERMARK_PICT = 25;
  internal const int TP_WATERMARK_WASH = 24;
  internal const int TRACK_DEL = 2;
  internal const int TRACK_INS = 1;
  internal const int TRACK_NONE = 0;
  internal CTrk trk;
  internal const int TSTYLE_IME = 1;
  internal const int TSTYLE_ULINEF = 2;
  internal CUndo ud;
  internal const char UNDO_CELL_DEL = 'U';
  internal const char UNDO_CELL_INS = 'V';
  internal const char UNDO_DELETE = 'D';
  internal const char UNDO_FONT = 'F';
  internal const char UNDO_FRAME = '1';
  internal const char UNDO_FRAME_LIMITED = '2';
  internal const char UNDO_INSERT = 'I';
  internal const int UNDO_LINE_LIMIT = 5000;
  internal const char UNDO_OBJECT_INSERT = 'O';
  internal const char UNDO_PARA = 'P';
  internal const char UNDO_PICT_SIZE = '3';
  internal const char UNDO_REPLACE = 'R';
  internal const char UNDO_ROW_DEL = 'T';
  internal const char UNDO_ROW_INS = 'S';
  internal const char UNDO_TABLE_ATTRIB = '4';
  internal const int UNIFORM = 65535 /*0xFFFF*/;
  internal const int UNITS_PER_INCH = 1440;
  internal static string UserKey;
  internal const int VKIND_MASTER = 3;
  internal const int VKIND_NONE = 0;
  internal const int VKIND_NORMAL = 4;
  internal const int VKIND_OUTLINE = 2;
  internal const int VKIND_OUTLINE_LAYOUT = 5;
  internal const int VKIND_PAGE_LAYOUT = 1;
  internal const int WC_INCLUDE_HDR_FTR = 2;
  internal const int WC_INCLUDE_HIDDEN = 4;
  internal const int WC_SELECTION = 1;
  internal static string[] WeekDays;
  internal const int WHEEL_RADIUS = 14;
  internal static Cursor WheelDownCur;
  internal static Cursor WheelFullCur;
  internal static Cursor WheelUpCur;
  internal const int WINDOW_EXT = 250;
  internal static bool WininetSearched;
  internal static tc.StrWinPtr[] WinPtr;
  internal const int WM_USER = 1024 /*0x0400*/;
  internal const int WMTER_MOUSEHOVER = 673;
  internal const int WMTER_MOUSEWHEEL = 522;
  internal const int WRAP_MIN = 1;
  internal const int WRAP_OFF = 0;
  internal const int WRAP_PAGE = 4;
  internal const int WRAP_PARA = 2;
  internal const int WRAP_WIN = 3;
  internal const int WRAPSIDE_BOTH = 0;
  internal const int WRAPSIDE_LARGEST = 3;
  internal const int WRAPSIDE_LEFT = 1;
  internal const int WRAPSIDE_RIGHT = 2;
  internal CWrap wrp;
  internal static int[] ZCol;
  internal static int[] ZRow;
  internal const char ZWNJ_CHAR = '\u0004';

  static tc()
  {
    tc.CLR_WHITE = Color.White;
    tc.CLR_WHITE_FORCE = Color.FromArgb((int) byte.MaxValue, 254, 254, 254);
    tc.CLR_BLACK = Color.Black;
    tc.CLR_AUTO = Color.FromArgb((int) byte.MaxValue, 1, 1, 1);
    tc.PROTECT_COLOR = Color.FromArgb(158, 158, 158);
    tc.InIE = false;
    tc.InServer = false;
    tc.DoClassCleanup = false;
    tc.TotalGlbFonts = 0;
    tc.GlbPrtDCCount = 0;
    tc.MergeDelim = '|';
    tc.TerOpenCount = 0;
    tc.DefPrt = (PrinterSettings) null;
    tc.DefPrtPage = (PageSettings) null;
    tc.WinPtr = new tc.StrWinPtr[200];
    tc.StSearched = false;
    tc.HtnSearched = false;
    tc.hHtn = (Assembly) null;
    tc.HtnLicenseKey = "";
    tc.SspSearched = false;
    tc.TerInitFlags = 1207959552 /*0x48000000*/;
    tc.TerInitFlags2 = 32 /*0x20*/;
    tc.TerInitFlags3 = 269492224 /*0x10102000*/;
    tc.TerInitFlags4 = 268435520 /*0x10000040*/;
    tc.TerInitFlags5 = 16777216 /*0x01000000*/;
    tc.TerInitFlags6 = 524288 /*0x080000*/;
    tc.PalColor = new Color[100];
    tc.TotalBaseColors = 0;
    tc.TotalPalColors = 0;
    tc.CosVal = new int[361];
    tc.ZRow = new int[64 /*0x40*/];
    tc.ZCol = new int[64 /*0x40*/];
    tc.TerStyles = 0;
    tc.InitMbcs = false;
    tc.InitPageWidth = 0;
    tc.InitPageHeight = 0;
    tc.InitFontFace = "";
    tc.eval = true;
    tc.expired = false;
    tc.EvalDaysRemaining = 15;
    tc.EvalDaysUsed = 0;
    tc.disable = false;
    tc.ky1 = "X3XP41X";
    tc.ky2 = "XMR5T2X";
    tc.ky3 = "X76CH3X";
    tc.extended = false;
    tc.UserKey = "";
    tc.StnKey = "";
    tc.DateFieldText = new string[30];
    tc.TotalDateFmts = 0;
    tc.WeekDays = new string[7];
    tc.MonthNames = new string[12];
    tc.DebugMode = false;
    tc.TlbClass = "";
    tc.IsWinNT2000 = false;
    tc.OSCanRotate = false;
    tc.DefPaperName = new string[11];
    tc.DefPaperSize = new int[11];
    tc.DefPaperKind = new PaperKind[11];
    tc.DefPaperWidth = new float[11];
    tc.DefPaperHeight = new float[11];
    tc.DefPaperCount = 0;
    tc.SkipInt = 0;
    tc.SkipUint = 0;
    tc.SkipByte = (byte) 0;
    tc.SkipBytes = (byte[]) null;
    tc.SkipChars = (char[]) null;
    tc.SkipInts = (int[]) null;
    tc.SkipBool = false;
    tc.SkipStr = "";
    tc.SkipRect = new COp.RECT();
  }

  internal tc(ImRtfEditor ImRtfEditor)
  {
    this.e = (ImRtfEditor) null;
    this.op = (COp) null;
    this.init = (CInit) null;
    this.draw = (CDraw) null;
    this.fnt = (CFnt) null;
    this.edit = (CEdit) null;
    this.misc = (CMisc) null;
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
    this.e = ImRtfEditor;
  }

  internal char[] AppendArray(char[] src, int idx, int len, char[] dest)
  {
    int length = dest.Length;
    char[] chArray = this.ReAlloc(dest, length + len);
    for (int index = 0; index < len; ++index)
      chArray[length + index] = src[index + idx];
    return chArray;
  }

  internal bool CheckLineLimit(int count)
  {
    if (this.e.TerArg.LineLimit > 0 && count > this.e.TerArg.LineLimit && this.False(this.e.TerOpFlags & 256 /*0x0100*/))
      return false;
    if (count >= this.e.MaxLines)
    {
      int num = this.e.MaxLines + this.e.MaxLines / 4;
      if (num <= count)
        num = count + 1;
      if (this.e.TerArg.LineLimit > 0 && num > this.e.TerArg.LineLimit && this.False(this.e.TerOpFlags & 256 /*0x0100*/))
        num = this.e.TerArg.LineLimit;
      tc.ClsLinePtr[] clsLinePtrArray = new tc.ClsLinePtr[num + 1];
      for (int index = 0; index < this.e.MaxLines; ++index)
        clsLinePtrArray[index] = this.e.text[index];
      for (int maxLines = this.e.MaxLines; maxLines < num; ++maxLines)
        clsLinePtrArray[maxLines] = (tc.ClsLinePtr) null;
      this.e.text = clsLinePtrArray;
      this.e.MaxLines = num;
    }
    return true;
  }

  internal float CmToInches(float x) => x / 2.54f;

  internal int CmToTwips(double x) => (int) (x * 1440.0 * 100.0 / 254.0);

  internal byte[] CopyArray(byte[] ar, int idx)
  {
    int length = ar.Length;
    byte[] numArray = new byte[length - idx];
    for (int index = idx; index < length; ++index)
      numArray[index - idx] = ar[index];
    return numArray;
  }

  internal char[] CopyArray(char[] ar, int idx)
  {
    int length = ar.Length;
    char[] chArray = new char[length - idx];
    for (int index = idx; index < length; ++index)
      chArray[index - idx] = ar[index];
    return chArray;
  }

  internal ushort[] CopyArray(ushort[] ar, int idx)
  {
    int length = ar.Length;
    ushort[] numArray = new ushort[length - idx];
    for (int index = idx; index < length; ++index)
      numArray[index - idx] = ar[index];
    return numArray;
  }

  internal char[] CopyArray(char[] ar, int idx, int len)
  {
    char[] chArray = new char[len];
    for (int index = 0; index < len; ++index)
      chArray[index] = ar[index + idx];
    return chArray;
  }

  internal ushort[] CopyArray(ushort[] ar, int idx, int len)
  {
    ushort[] numArray = new ushort[len];
    for (int index = 0; index < len; ++index)
      numArray[index] = ar[index + idx];
    return numArray;
  }

  internal int DevToTwipsX(int x) => this.MulDiv(x, 1440, this.e.DevResX);

  internal int DevToTwipsY(int x) => this.MulDiv(x, 1440, this.e.DevResY);

  internal int EmuToTwips(int x) => x / 635;

  internal bool False(tc.StrGlbFont[] x) => x == null;

  internal bool False(tc.StrLineSeg[] x) => x == null;

  internal bool False(tc.StrListnum[] x) => x == null;

  internal bool False(tc.ClsAnim x) => x == null;

  internal bool False(tc.ClsForm x) => x == null;

  internal bool False(tc.ClsHdnFont x) => x == null;

  internal bool False(tc.ClsLinePtr x) => x == null;

  internal bool False(tc.ClsTabw x) => x == null;

  internal bool False(ushort[] x) => x == null;

  internal bool False(bool x) => !x;

  internal bool False(Bitmap x) => x == null;

  internal bool False(Brush x) => x == null;

  internal bool False(Font x) => x == null;

  internal bool False(Graphics x) => x == null;

  internal bool False(Pen x) => x == null;

  internal bool False(Region x) => x == null;

  internal bool False(int x) => x == 0;

  internal bool False(IntPtr x) => x == IntPtr.Zero;

  internal bool False(string x) => x == null;

  internal bool False(uint x) => x == 0U;

  internal bool False(Control x) => x == null;

  internal bool False(Cursor x) => x == (Cursor) null;

  internal bool False(InputLanguage x) => x == null;

  internal bool False(byte[] x) => x == null;

  internal bool False(char[] x) => x == null;

  internal bool False(int[] x) => x == null;

  internal void FarMemSet(ushort[] obj, ushort val, int start, int len)
  {
    for (int index = start; index < start + len; ++index)
      obj[index] = val;
  }

  internal void FarMove(char[] src, char[] dest, int count)
  {
    for (int index = 0; index < count; ++index)
      dest[index] = src[index];
  }

  internal void FarMove(int[] src, int[] dest, int count)
  {
    for (int index = 0; index < count; ++index)
      dest[index] = src[index];
  }

  internal void FarMove(char[] src, int SrcIdx, char[] dest, int DestIdx, int count)
  {
    for (int index = 0; index < count; ++index)
      dest[DestIdx + index] = src[SrcIdx + index];
  }

  internal void FarMove(ushort[] src, int SrcIdx, ushort[] dest, int DestIdx, int count)
  {
    for (int index = 0; index < count; ++index)
      dest[DestIdx + index] = src[SrcIdx + index];
  }

  internal void FarMoveOl(tc.ClsLinePtr[] ele, int SrcIdx, int DestIdx, int count)
  {
    if (DestIdx > SrcIdx)
    {
      for (int index = count - 1; index >= 0; --index)
        ele[DestIdx + index] = ele[SrcIdx + index];
    }
    else
    {
      for (int index = 0; index < count; ++index)
        ele[DestIdx + index] = ele[SrcIdx + index];
    }
  }

  internal void FarMoveOl(char[] ele, int SrcIdx, int DestIdx, int count)
  {
    if (DestIdx > SrcIdx)
    {
      for (int index = count - 1; index >= 0; --index)
        ele[DestIdx + index] = ele[SrcIdx + index];
    }
    else
    {
      for (int index = 0; index < count; ++index)
        ele[DestIdx + index] = ele[SrcIdx + index];
    }
  }

  internal void FarMoveOl(ushort[] ele, int SrcIdx, int DestIdx, int count)
  {
    if (DestIdx > SrcIdx)
    {
      for (int index = count - 1; index >= 0; --index)
        ele[DestIdx + index] = ele[SrcIdx + index];
    }
    else
    {
      for (int index = 0; index < count; ++index)
        ele[DestIdx + index] = ele[SrcIdx + index];
    }
  }

  internal void FarMoveOl(char[] src, int SrcIdx, char[] dest, int DestIdx, int count)
  {
    char[] chArray = new char[count];
    for (int index = 0; index < count; ++index)
      chArray[index] = src[SrcIdx + index];
    for (int index = 0; index < count; ++index)
      dest[DestIdx + index] = chArray[index];
  }

  internal void FarMoveOl(ushort[] src, int SrcIdx, ushort[] dest, int DestIdx, int count)
  {
    ushort[] numArray = new ushort[count];
    for (int index = 0; index < count; ++index)
      numArray[index] = src[SrcIdx + index];
    for (int index = 0; index < count; ++index)
      dest[DestIdx + index] = numArray[index];
  }

  internal Rectangle FromRect(COp.RECT OurRect)
  {
    return Rectangle.FromLTRB(OurRect.left, OurRect.top, OurRect.right, OurRect.bottom);
  }

  internal float InchesToCm(float x) => x * 2.54f;

  internal float InchesToMm(float x) => x * 25.4f;

  internal int InchesToPoints(float x) => (int) ((double) x * 72.0);

  internal float InchesToTwips(double x) => (float) (int) (x * 1440.0);

  internal bool IsBreakChar(char chr)
  {
    return (int) chr == (int) this.e.ParaChar || (int) chr == (int) this.e.CellChar || this.lstrchr(this.e.BreakChars, chr);
  }

  internal bool IsFootnoteStyle(int x) => (x & 2048 /*0x0800*/) != 0 && (x & 32768 /*0x8000*/) == 0;

  internal bool IsHdrFtrChar(char x)
  {
    return x == '\u0011' || x == '\u0019' || x == '\u0010' || x == '\u001A';
  }

  internal bool IsLcChar(char x) => x >= 'a' && x <= 'z';

  internal bool IsSectionBorder(int x) => x >= 14 && x <= 17;

  internal bool LineInfo(int LineNo, int flag)
  {
    return LineNo >= 0 && LineNo < this.e.TotalLines && this.e.text[LineNo].tabw != null && (this.e.text[LineNo].tabw.type & flag) != 0;
  }

  internal void lstrcat(char[] str1, char[] str2)
  {
    int num1 = this.lstrlen(str2);
    if (num1 <= 0)
      return;
    int num2 = this.lstrlen(str1);
    int index;
    for (index = 0; index < num1; ++index)
      str1[num2 + index] = str2[index];
    str1[num2 + index] = char.MinValue;
  }

  internal bool lstrchr(char[] InStr, char chr)
  {
    for (int index = 0; InStr[index] != char.MinValue; ++index)
    {
      if ((int) InStr[index] == (int) chr)
        return true;
    }
    return false;
  }

  internal void lstrcpy(char[] str1, char[] str2)
  {
    int num = this.lstrlen(str2);
    if (num > 0)
    {
      int index;
      for (index = 0; index < num; ++index)
        str1[index] = str2[index];
      str1[index] = char.MinValue;
    }
    else
      str1[0] = char.MinValue;
  }

  internal int lstrlen(char[] str)
  {
    if (this.False(str))
      return 0;
    int length = str.Length;
    int index = 0;
    while (index < length && str[index] != char.MinValue)
      ++index;
    return index;
  }

  internal ushort MakeWord(byte x, byte y) => (ushort) ((uint) x << 8 | (uint) y);

  internal float MmToInches(int x) => (float) x / 25.4f;

  internal int MmToUnitX(int x) => this.MulDiv(x, this.e.UnitResX * 10, 254);

  internal int MmToUnitY(int x) => this.MulDiv(x, this.e.UnitResY * 10, 254);

  internal int MulDiv(int x, int y, int z)
  {
    if (x == 0 || y == 0)
      return 0;
    if (z == 0)
      return x;
    long num1 = (long) (x * y);
    long num2 = (long) z;
    long num3 = num1 / num2;
    if (num1 % num2 >= (long) (z >> 1))
      ++num3;
    return (int) num3;
  }

  internal int OrigScrToTwipsX(int x) => this.MulDiv(x, 1440, this.e.OrigScrResX);

  internal int OrigScrToTwipsY(int x) => this.MulDiv(x, 1440, this.e.OrigScrResY);

  internal int OrigScrToUnitsX(int x) => this.MulDiv(x, this.e.UnitResX, this.e.OrigScrResX);

  internal int OrigScrToUnitsY(int x) => this.MulDiv(x, this.e.UnitResY, this.e.OrigScrResY);

  internal float PointsToInches(int x) => (float) x / 72f;

  internal int PointsToScrX(int x) => this.MulDiv(x, this.e.ScrResX, 72);

  internal int PointsToScrY(int x) => this.MulDiv(x, this.e.ScrResY, 72);

  internal int PointsToTwips(float x) => (int) ((double) x * 20.0);

  internal int PointsToUnitX(int x) => this.MulDiv(x, this.e.UnitResX, 72);

  internal int PointsToUnitY(int x) => this.MulDiv(x, this.e.UnitResY, 72);

  internal bool PrintError(int MsgId, string DebugMsg)
  {
    bool flag = false;
    if (this.True(this.e.TerFlags & 4) || this.e.InServer)
      flag = true;
    this.e.TerLastMsg = MsgId;
    if (flag)
    {
      this.e.TerLastDebugMsg = DebugMsg;
    }
    else
    {
      int num = (int) this.misc.ShowMessage(this.e.MsgString[MsgId], DebugMsg, MessageBoxButtons.OK);
    }
    return false;
  }

  internal tc.StrBlt[] ReAlloc(tc.StrBlt[] OldObj, int count)
  {
    tc.StrBlt[] strBltArray = new tc.StrBlt[count];
    for (int index = 0; index < count && index < OldObj.Length; ++index)
      strBltArray[index] = OldObj[index];
    return strBltArray;
  }

  internal tc.StrCell[] ReAlloc(tc.StrCell[] OldObj, int count)
  {
    tc.StrCell[] strCellArray = new tc.StrCell[count];
    for (int index = 0; index < count && index < OldObj.Length; ++index)
      strCellArray[index] = OldObj[index];
    return strCellArray;
  }

  internal tc.StrCellAux[] ReAlloc(tc.StrCellAux[] OldObj, int count)
  {
    tc.StrCellAux[] strCellAuxArray = new tc.StrCellAux[count];
    for (int index = 0; index < count && index < OldObj.Length; ++index)
      strCellAuxArray[index] = OldObj[index];
    return strCellAuxArray;
  }

  internal tc.StrCharTag[] ReAlloc(tc.StrCharTag[] OldObj, int count)
  {
    tc.StrCharTag[] strCharTagArray = new tc.StrCharTag[count];
    for (int index = 0; index < count && index < OldObj.Length; ++index)
      strCharTagArray[index] = OldObj[index];
    return strCharTagArray;
  }

  internal tc.StrDragObj[] ReAlloc(tc.StrDragObj[] OldObj, int count)
  {
    tc.StrDragObj[] strDragObjArray = new tc.StrDragObj[count];
    for (int index = 0; index < count && index < OldObj.Length; ++index)
      strDragObjArray[index] = OldObj[index];
    return strDragObjArray;
  }

  internal tc.StrHlSeg[] ReAlloc(tc.StrHlSeg[] OldObj, int count)
  {
    tc.StrHlSeg[] strHlSegArray = new tc.StrHlSeg[count];
    for (int index = 0; index < count && index < OldObj.Length; ++index)
      strHlSegArray[index] = OldObj[index];
    return strHlSegArray;
  }

  internal tc.StrImageMapRect[] ReAlloc(tc.StrImageMapRect[] OldObj, int count)
  {
    tc.StrImageMapRect[] strImageMapRectArray = new tc.StrImageMapRect[count];
    if (OldObj != null)
    {
      for (int index = 0; index < count && index < OldObj.Length; ++index)
        strImageMapRectArray[index] = OldObj[index];
    }
    return strImageMapRectArray;
  }

  internal tc.StrLineSeg[] ReAlloc(tc.StrLineSeg[] OldObj, int count)
  {
    tc.StrLineSeg[] strLineSegArray = new tc.StrLineSeg[count];
    for (int index = 0; index < count && index < OldObj.Length; ++index)
      strLineSegArray[index] = OldObj[index];
    return strLineSegArray;
  }

  internal tc.StrList[] ReAlloc(tc.StrList[] OldObj, int count)
  {
    tc.StrList[] strListArray = new tc.StrList[count];
    for (int index = 0; index < count && index < OldObj.Length; ++index)
      strListArray[index] = OldObj[index];
    return strListArray;
  }

  internal tc.StrListLevel[] ReAlloc(tc.StrListLevel[] OldObj, int count)
  {
    tc.StrListLevel[] strListLevelArray = new tc.StrListLevel[count];
    for (int index = 0; index < count && index < OldObj.Length; ++index)
      strListLevelArray[index] = OldObj[index];
    return strListLevelArray;
  }

  internal tc.StrListnum[] ReAlloc(tc.StrListnum[] OldObj, int count)
  {
    tc.StrListnum[] strListnumArray = new tc.StrListnum[count];
    for (int index = 0; index < count && index < OldObj.Length; ++index)
      strListnumArray[index] = OldObj[index];
    return strListnumArray;
  }

  internal tc.StrListOr[] ReAlloc(tc.StrListOr[] OldObj, int count)
  {
    tc.StrListOr[] strListOrArray = new tc.StrListOr[count];
    for (int index = 0; index < count && index < OldObj.Length; ++index)
      strListOrArray[index] = OldObj[index];
    return strListOrArray;
  }

  internal tc.StrMergeData[] ReAlloc(tc.StrMergeData[] OldObj, int count)
  {
    tc.StrMergeData[] strMergeDataArray = new tc.StrMergeData[count];
    if (OldObj != null)
    {
      for (int index = 0; index < count && index < OldObj.Length; ++index)
        strMergeDataArray[index] = OldObj[index];
    }
    return strMergeDataArray;
  }

  internal tc.StrPage[] ReAlloc(tc.StrPage[] OldObj, int count)
  {
    tc.StrPage[] strPageArray = new tc.StrPage[count];
    for (int index = 0; index < count && index < OldObj.Length; ++index)
      strPageArray[index] = OldObj[index];
    return strPageArray;
  }

  internal tc.StrParaFrame[] ReAlloc(tc.StrParaFrame[] OldObj, int count)
  {
    tc.StrParaFrame[] strParaFrameArray = new tc.StrParaFrame[count];
    for (int index = 0; index < count && index < OldObj.Length; ++index)
      strParaFrameArray[index] = OldObj[index];
    return strParaFrameArray;
  }

  internal tc.StrPendPict[] ReAlloc(tc.StrPendPict[] OldObj, int count)
  {
    tc.StrPendPict[] strPendPictArray = new tc.StrPendPict[count];
    for (int index = 0; index < count && index < OldObj.Length; ++index)
      strPendPictArray[index] = OldObj[index];
    return strPendPictArray;
  }

  internal tc.StrPfmt[] ReAlloc(tc.StrPfmt[] OldObj, int count)
  {
    tc.StrPfmt[] strPfmtArray = new tc.StrPfmt[count];
    for (int index = 0; index < count && index < OldObj.Length; ++index)
      strPfmtArray[index] = OldObj[index];
    return strPfmtArray;
  }

  internal tc.StrReviewer[] ReAlloc(tc.StrReviewer[] OldObj, int count)
  {
    tc.StrReviewer[] strReviewerArray = new tc.StrReviewer[count];
    if (OldObj != null)
    {
      for (int index = 0; index < count && index < OldObj.Length; ++index)
        strReviewerArray[index] = OldObj[index];
    }
    return strReviewerArray;
  }

  internal tc.StrRtfColor[] ReAlloc(tc.StrRtfColor[] OldObj, int count)
  {
    tc.StrRtfColor[] strRtfColorArray = new tc.StrRtfColor[count];
    if (OldObj != null)
    {
      for (int index = 0; index < count && index < OldObj.Length; ++index)
        strRtfColorArray[index] = OldObj[index];
    }
    return strRtfColorArray;
  }

  internal tc.StrRtfFont[] ReAlloc(tc.StrRtfFont[] OldObj, int count)
  {
    tc.StrRtfFont[] strRtfFontArray = new tc.StrRtfFont[count];
    if (OldObj != null)
    {
      for (int index = 0; index < count && index < OldObj.Length; ++index)
        strRtfFontArray[index] = OldObj[index];
    }
    return strRtfFontArray;
  }

  internal tc.StrScrCtl[] ReAlloc(tc.StrScrCtl[] OldObj, int count)
  {
    tc.StrScrCtl[] strScrCtlArray = new tc.StrScrCtl[count];
    for (int index = 0; index < count && index < OldObj.Length; ++index)
      strScrCtlArray[index] = OldObj[index];
    return strScrCtlArray;
  }

  internal tc.StrSect[] ReAlloc(tc.StrSect[] OldObj, int count)
  {
    tc.StrSect[] strSectArray = new tc.StrSect[count];
    for (int index = 0; index < count && index < OldObj.Length; ++index)
      strSectArray[index] = OldObj[index];
    return strSectArray;
  }

  internal tc.StrSect1[] ReAlloc(tc.StrSect1[] OldObj, int count)
  {
    tc.StrSect1[] strSect1Array = new tc.StrSect1[count];
    for (int index = 0; index < count && index < OldObj.Length; ++index)
      strSect1Array[index] = OldObj[index];
    return strSect1Array;
  }

  internal tc.StrStyleId[] ReAlloc(tc.StrStyleId[] OldObj, int count)
  {
    tc.StrStyleId[] strStyleIdArray = new tc.StrStyleId[count];
    for (int index = 0; index < count && index < OldObj.Length; ++index)
      strStyleIdArray[index] = OldObj[index];
    return strStyleIdArray;
  }

  internal tc.StrTableAux[] ReAlloc(tc.StrTableAux[] OldObj, int count)
  {
    tc.StrTableAux[] strTableAuxArray = new tc.StrTableAux[count];
    for (int index = 0; index < count && index < OldObj.Length; ++index)
      strTableAuxArray[index] = OldObj[index];
    return strTableAuxArray;
  }

  internal tc.StrTableRow[] ReAlloc(tc.StrTableRow[] OldObj, int count)
  {
    tc.StrTableRow[] strTableRowArray = new tc.StrTableRow[count];
    for (int index = 0; index < count && index < OldObj.Length; ++index)
      strTableRowArray[index] = OldObj[index];
    return strTableRowArray;
  }

  internal tc.StrUndo[] ReAlloc(tc.StrUndo[] OldObj, int count)
  {
    tc.StrUndo[] strUndoArray = new tc.StrUndo[count];
    for (int index = 0; index < count && index < OldObj.Length; ++index)
      strUndoArray[index] = OldObj[index];
    return strUndoArray;
  }

  internal bool[] ReAlloc(bool[] OldObj, int count)
  {
    bool[] flagArray = new bool[count];
    for (int index = 0; index < count && index < OldObj.Length; ++index)
      flagArray[index] = OldObj[index];
    return flagArray;
  }

  internal byte[] ReAlloc(byte[] OldObj, int count)
  {
    byte[] numArray = new byte[count];
    for (int index = 0; index < count && index < OldObj.Length; ++index)
      numArray[index] = OldObj[index];
    return numArray;
  }

  internal char[] ReAlloc(char[] OldObj, int count)
  {
    char[] chArray = new char[count];
    for (int index = 0; index < count && index < OldObj.Length; ++index)
      chArray[index] = OldObj[index];
    return chArray;
  }

  internal int[] ReAlloc(int[] OldObj, int count)
  {
    int[] numArray = new int[count];
    for (int index = 0; index < count && index < OldObj.Length; ++index)
      numArray[index] = OldObj[index];
    return numArray;
  }

  internal ushort[] ReAlloc(ushort[] OldObj, int count)
  {
    ushort[] numArray = new ushort[count];
    for (int index = 0; index < count && index < OldObj.Length; ++index)
      numArray[index] = OldObj[index];
    return numArray;
  }

  internal Keys[] ReAlloc(Keys[] OldObj, int count)
  {
    Keys[] keysArray = new Keys[count];
    for (int index = 0; index < count && index < OldObj.Length; ++index)
      keysArray[index] = OldObj[index];
    return keysArray;
  }

  internal static int ResetFlag(int flags, int flag) => flags &= ~flag;

  internal static int ResetLongFlag(ref int flags, int flag) => flags &= ~flag;

  internal int ResetTerFlag(int flag) => this.e.TerFlags &= ~flag;

  internal int ResetTerFlag2(int flag) => this.e.TerFlags2 &= ~flag;

  internal int ResetTerFlag3(int flag) => this.e.TerFlags3 &= ~flag;

  internal int ResetTerFlag4(int flag) => this.e.TerFlags4 &= ~flag;

  internal int ResetTerFlag5(int flag) => this.e.TerFlags5 &= ~flag;

  internal int ResetTerFlag6(int flag) => this.e.TerFlags6 &= ~flag;

  internal int ResetTerOpFlag(int flag)
  {
    this.e.TerOpFlags &= ~flag;
    return this.e.TerOpFlags;
  }

  internal int ResetTerOpFlag2(int flag)
  {
    this.e.TerOpFlags2 &= ~flag;
    return this.e.TerOpFlags2;
  }

  internal static int ResetUintFlag(ref int flags, int flag)
  {
    flags &= ~flag;
    return flags;
  }

  internal int RoundInt(int val, int step)
  {
    bool flag = false;
    if (val < 0)
    {
      val = -val;
      flag = true;
    }
    int num = step * ((val + step / 2) / step);
    return flag ? -num : num;
  }

  internal int ScrToPointsX(int x) => this.MulDiv(x, 72, this.e.ScrResX);

  internal int ScrToPointsY(int x) => this.MulDiv(x, 72, this.e.ScrResY);

  internal int ScrToTwipsX(int x) => this.MulDiv(x, 1440, this.e.ScrResX);

  internal int ScrToTwipsY(int x) => this.MulDiv(x, 1440, this.e.ScrResY);

  internal int ScrToUnitX(int x) => this.MulDiv(x, this.e.UnitResX, this.e.ScrResX);

  internal int ScrToUnitY(int x) => this.MulDiv(x, this.e.UnitResY, this.e.ScrResY);

  internal void SetRect(out COp.RECT OurRect, int x, int y, int right, int bottom)
  {
    OurRect.left = x;
    OurRect.top = y;
    OurRect.right = right;
    OurRect.bottom = bottom;
  }

  internal bool SetRef()
  {
    this.init = this.e.init;
    this.draw = this.e.draw;
    this.fnt = this.e.fnt;
    this.edit = this.e.edit;
    this.misc = this.e.misc;
    this.prt = this.e.prt;
    this.blk = this.e.blk;
    this.page = this.e.page;
    this.sec = this.e.sec;
    this.frm = this.e.frm;
    this.link = this.e.link;
    this.trk = this.e.trk;
    this.dsh = this.e.dsh;
    this.fld = this.e.fld;
    this.ud = this.e.ud;
    this.tbl = this.e.tbl;
    this.pos = this.e.pos;
    this.wrp = this.e.wrp;
    this.drag = this.e.drag;
    this.par = this.e.par;
    this.spl = this.e.spl;
    this.ole = this.e.ole;
    this.bar = this.e.bar;
    this.ctl = this.e.ctl;
    this.rtfr = this.e.rtfr;
    this.rtfw = this.e.rtfw;
    this.io = this.e.io;
    this.mnu = this.e.mnu;
    return true;
  }

  internal bool SwapBools(ref bool pVal1, bool pVal2)
  {
    int num = pVal1 ? 1 : 0;
    pVal1 = pVal2;
    return num != 0;
  }

  internal bool SwapDbls(ref double pVal1, ref double pVal2)
  {
    double num = pVal1;
    pVal1 = pVal2;
    pVal2 = num;
    return true;
  }

  internal bool SwapInts(ref int pVal1, ref int pVal2)
  {
    int num = pVal1;
    pVal1 = pVal2;
    pVal2 = num;
    return true;
  }

  internal int SwapInts(ref int pVal1, int pVal2)
  {
    int num = pVal1;
    pVal1 = pVal2;
    return num;
  }

  internal static int TerSetFlags2(bool set, int flags)
  {
    if (set)
      tc.TerInitFlags2 |= flags;
    else
      tc.TerInitFlags2 = tc.ResetUintFlag(ref tc.TerInitFlags2, flags);
    return tc.TerInitFlags2;
  }

  internal static int TerSetFlags3(bool set, int flags)
  {
    if (set)
      tc.TerInitFlags3 |= flags;
    else
      tc.TerInitFlags3 = tc.ResetUintFlag(ref tc.TerInitFlags3, flags);
    return tc.TerInitFlags3;
  }

  internal static int TerSetFlags4(bool set, int flags)
  {
    if (set)
      tc.TerInitFlags4 |= flags;
    else
      tc.TerInitFlags4 = tc.ResetUintFlag(ref tc.TerInitFlags4, flags);
    return tc.TerInitFlags4;
  }

  internal static int TerSetInitFlags(bool set, int flags)
  {
    if (set)
      tc.TerInitFlags |= flags;
    else
      tc.TerInitFlags = tc.ResetUintFlag(ref tc.TerInitFlags, flags);
    return tc.TerInitFlags;
  }

  internal COp.RECT ToRect(Rectangle rect)
  {
    return new COp.RECT()
    {
      left = rect.X,
      top = rect.Y,
      right = rect.X + rect.Width,
      bottom = rect.Y + rect.Height
    };
  }

  internal Rectangle ToRectangle(COp.RECT OurRect)
  {
    return Rectangle.FromLTRB(OurRect.left, OurRect.top, OurRect.right, OurRect.bottom);
  }

  internal string ToStr(int val) => new string((char) val, 1);

  internal void TraceStack()
  {
    StackTrace stackTrace = new StackTrace();
    int frameCount = stackTrace.FrameCount;
    this.misc.OurPrintf(new object[1]
    {
      (object) "***************************"
    });
    this.misc.OurPrintf(new object[2]
    {
      (object) "Frame Count:",
      (object) (frameCount - 1)
    });
    for (int index = 1; index < frameCount; ++index)
      this.misc.OurPrintf(new object[2]
      {
        (object) "  Method:",
        (object) stackTrace.GetFrame(index).GetMethod().Name
      });
  }

  internal bool True(tc.StrGlbFont[] x) => x != null;

  internal bool True(tc.StrLineSeg[] x) => x != null;

  internal bool True(tc.StrListnum[] x) => x != null;

  internal bool True(tc.ClsAnim x) => x != null;

  internal bool True(tc.ClsForm x) => x != null;

  internal bool True(tc.ClsHdnFont x) => x != null;

  internal bool True(tc.ClsLinePtr x) => x != null;

  internal bool True(tc.ClsTabw x) => x != null;

  internal bool True(bool x) => x;

  internal bool True(Bitmap x) => x != null;

  internal bool True(Brush x) => x != null;

  internal bool True(Font x) => x != null;

  internal bool True(Graphics x) => x != null;

  internal bool True(Pen x) => x != null;

  internal bool True(Region x) => x != null;

  internal bool True(int x) => x != 0;

  internal bool True(IntPtr x) => x != IntPtr.Zero;

  internal bool True(string x) => x != null;

  internal bool True(uint x) => x > 0U;

  internal bool True(Control x) => x != null;

  internal bool True(Cursor x) => x != (Cursor) null;

  internal bool True(byte[] x) => x != null;

  internal bool True(InputLanguage x) => x != null;

  internal bool True(char[] x) => x != null;

  internal bool True(int[] x) => x != null;

  internal bool True(ushort[] x) => x != null;

  internal float TwipsToCm(int x) => (float) ((double) x * 254.0 / 144000.0);

  internal int TwipsToDevX(int x) => this.MulDiv(x, this.e.DevResX, 1440);

  internal int TwipsToDevY(int x) => this.MulDiv(x, this.e.DevResY, 1440);

  internal int TwipsToEmu(int x) => x * 635;

  internal float TwipsToInches(int x) => (float) x / 1440f;

  internal int TwipsToOrigScrX(int x) => this.MulDiv(x, this.e.OrigScrResX, 1440);

  internal int TwipsToOrigScrY(int x) => this.MulDiv(x, this.e.OrigScrResY, 1440);

  internal float TwipsToPoints(int x) => (float) x / 20f;

  internal int TwipsToScrX(int x) => this.MulDiv(x, this.e.ScrResX, 1440);

  internal int TwipsToScrY(int x) => this.MulDiv(x, this.e.ScrResY, 1440);

  internal int TwipsToUnitX(int x) => this.MulDiv(x, this.e.UnitResX, 1440);

  internal int TwipsToUnitY(int x) => this.MulDiv(x, this.e.UnitResY, 1440);

  internal float UnitToInchesX(int x) => (float) x / (float) this.e.PrtResX;

  internal float UnitToInchesY(int x) => (float) x / (float) this.e.PrtResY;

  internal int UnitToPointsX(int x) => this.MulDiv(x, 72, this.e.UnitResX);

  internal int UnitToPointsY(int x) => this.MulDiv(x, 72, this.e.UnitResY);

  internal int UnitToScrX(int x) => this.MulDiv(x, this.e.ScrResX, this.e.UnitResX);

  internal int UnitToScrY(int x) => this.MulDiv(x, this.e.ScrResY, this.e.UnitResY);

  internal int UnitToTwipsX(int x) => this.MulDiv(x, 1440, this.e.UnitResX);

  internal int UnitToTwipsY(int x)
  {
    return this.e.UnitResY == 1440 ? x : this.MulDiv(x, 1440, this.e.UnitResY);
  }

  internal struct arg_list
  {
    internal int x;
    internal int y;
    internal int width;
    internal int height;
    internal int LineLimit;
    internal bool WordWrap;
    internal bool PrintView;
    internal bool PageMode;
    internal bool FittedView;
    internal bool ShowStatus;
    internal bool ShowMenu;
    internal bool ShowHorBar;
    internal bool ShowVerBar;
    internal bool ruler;
    internal bool ToolBar;
    internal bool UserCanClose;
    internal bool BorderMargin;
    internal bool ReadOnly;
    internal int InitLine;
    internal char InputType;
    internal string file;
    internal string hBuffer;
    internal int BufferLen;
    internal char delim;
    internal int SaveFormat;
    internal int style;
    internal string FontTypeFace;
    internal int PointSize;
    internal bool open;
    internal int lastNotifiedModified;
    private int _modified;

    internal int modified
    {
      get => this._modified;
      set
      {
        if (this._modified == value)
          return;
        this._modified = value;
        if (this._modified != 0)
          return;
        this.lastNotifiedModified = 0;
      }
    }
  }

  internal class ClsAnim
  {
    internal int CurAnim;
    internal int delay;
    internal int FirstPict;
    internal int LoopCount;
    internal int NextAnim;
    internal int NextPict;
    internal int OrigLoopCount;
    internal int TimerId;
  }

  internal class ClsBox
  {
    internal string item;
    internal object ObjectValue;
    internal string TextValue;
    internal int value;

    internal ClsBox(string ArgItem, int ArgValue)
    {
      this.item = ArgItem;
      this.value = ArgValue;
    }

    internal ClsBox(string ArgItem, object ArgValue)
    {
      this.item = ArgItem;
      this.ObjectValue = ArgValue;
    }

    internal ClsBox(string ArgItem, string ArgValue)
    {
      this.item = ArgItem;
      this.TextValue = ArgValue;
    }

    public override string ToString() => this.item;
  }

  [Serializable]
  internal class ClsClipInfo
  {
    internal bool EmbTable;
    internal int size;
    internal int TblLevel;
  }

  internal class ClsDateTime
  {
    internal DateTime dt;

    internal tc.ClsDateTime Copy() => (tc.ClsDateTime) this.MemberwiseClone();

    private static bool Equals(tc.ClsDateTime dt1, tc.ClsDateTime dt2)
    {
      if (dt1 == null && dt2 == null)
        return true;
      return dt1 != null && dt2 != null && DateTime.Equals(dt1.dt, dt2.dt);
    }
  }

  internal class ClsForm
  {
    internal bool border;
    internal int CharSet;
    internal int CheckBoxSize;
    internal int CheckBoxType;
    internal string CtlClass;
    internal string family;
    internal int flags;
    internal int FontId;
    internal int FontStyle;
    internal int id;
    internal int InitNum;
    internal string InitText;
    internal bool IsProtected;
    internal int ListCount;
    internal string[] ListItem;
    internal int MaxLen;
    internal string name;
    internal int style;
    internal Color TextBkColor;
    internal Color TextColor = Color.Empty;
    internal int TextType;
    internal int TwipsSize;
    internal string typeface;
  }

  internal class ClsHdnFont
  {
    internal int BaseHeight;
    internal int BaseHeightAdj;
    internal int[] CharWidth;
    internal int ExtLead;
    internal int height;
  }

  internal class ClsLinePtr
  {
    internal int BaseHt;
    internal int cid;
    internal ushort[] cwidth;
    internal int fid;
    internal int flags;
    internal int flags2;
    internal ushort[] fmt;
    internal int frame;
    internal int height;
    internal int JustAdjX;
    internal int len;
    internal int page;
    internal int pfmt;
    internal int ScrHt;
    internal tc.ClsTabw tabw;
    internal ushort[] tag;
    internal char[] txt;
    internal ushort UniFmt;
    internal int x;
    internal int y;

    internal tc.ClsLinePtr Copy()
    {
      tc.ClsLinePtr clsLinePtr = (tc.ClsLinePtr) this.MemberwiseClone();
      int length1;
      if (this.txt != null && (length1 = this.txt.Length) > 0)
      {
        clsLinePtr.txt = new char[length1];
        for (int index = 0; index < length1; ++index)
          clsLinePtr.txt[index] = this.txt[index];
      }
      int length2;
      if (this.fmt != null && (length2 = this.fmt.Length) > 0)
      {
        clsLinePtr.fmt = new ushort[length2];
        for (int index = 0; index < length2; ++index)
          clsLinePtr.fmt[index] = this.fmt[index];
      }
      int length3;
      if (this.tag != null && (length3 = this.tag.Length) > 0)
      {
        clsLinePtr.tag = new ushort[length3];
        for (int index = 0; index < length3; ++index)
          clsLinePtr.tag[index] = this.tag[index];
      }
      int length4;
      if (this.cwidth != null && (length4 = this.cwidth.Length) > 0)
      {
        clsLinePtr.cwidth = new ushort[length4];
        for (int index = 0; index < length4; ++index)
          clsLinePtr.cwidth[index] = this.cwidth[index];
      }
      if (this.tabw != null)
        clsLinePtr.tabw = this.tabw.Copy();
      return clsLinePtr;
    }
  }

  internal class ClsParaFrame
  {
    internal tc.StrParaFrame frm;
  }

  internal class ClsRtf
  {
    internal bool HasIncompleteAsianChar;
    internal bool ApplyPaperSize;
    internal char[] buf;
    internal int BufIndex;
    internal int BufLen;
    internal int CellFlow;
    internal char CurChar;
    internal byte CurTabFlags;
    internal int CurTabType;
    internal int CurTblLevel;
    internal string CurWord;
    internal int DocFlow;
    internal double DoubleParam;
    internal bool EmbedTable;
    internal bool EmptyDoc;
    internal bool EnableTracking;
    internal int EndnoteNo;
    internal bool eof;
    internal string FieldCode;
    internal int FilePos;
    internal int FirstAnimPict;
    internal int FirstLine;
    internal int flags;
    internal int flags1;
    internal int flags2;
    internal int FootnoteNo;
    internal tc.StrRtfGroup[] group;
    internal bool GroupBegin;
    internal bool GroupEnd;
    internal int GroupLevel;
    internal char HdrFtrChar;
    internal FileStream iFile;
    internal bool IgnoreCrLfInControlWord;
    internal bool IgnoreText;
    internal int InitAuxId;
    internal int InitFieldId;
    internal int InitialCell;
    internal int InitialParaFID;
    internal int InitSect;
    internal int InitStyle;
    internal int InitTblCol;
    internal int InsertAftCell;
    internal int InsertBefCell;
    internal int InsLine;
    internal int IntParam;
    internal bool IsControlWord;
    internal int lang;
    internal bool mac;
    internal int OpenCellId;
    internal int OpenLastCellX;
    internal int OpenRowId;
    internal string OutBuf;
    internal bool OutBufHasUnicode;
    internal int OutBufLen;
    internal int PaperHeight;
    internal int PaperWidth;
    internal string param;
    internal bool PastingColumn;
    internal int pict;
    internal int PictFID;
    internal int PictFrameCol;
    internal int PictFrameLine;
    internal int PictHeight;
    internal int PictWidth;
    internal int PrevCellX;
    internal char PrevChar;
    internal int PrevField;
    internal tc.StrSect sect;
    internal int SectFlow;
    internal bool SetWidowOrphan;
    internal int ShpGroup;
    internal bool SomeTextRead;
    internal char[] stack;
    internal int StackLen;
    internal bool SubEntry;
    internal bool SuspendReading;
    internal bool TableInFrame;
    internal bool TableRead;
    internal int TagId;
    internal tc.ClsRtf.StrRtfTableLevel[] TblLevel;
    internal char[] text;
    internal int TextIndex;
    internal int TextLen;
    internal int TotalColors;
    internal int WordLen;
    internal int[] XlateLs;

    internal ClsRtf() => this.TblLevel = new tc.ClsRtf.StrRtfTableLevel[20];

    internal void InitArray()
    {
      this.XlateLs = new int[5000];
      this.text = new char[1001];
      this.stack = new char[1001];
    }

    internal struct StrRtfTableLevel
    {
      internal int CurRowId;
      internal int CurCellId;
      internal int LastCellX;
      internal int OpenRowId;
      internal int OpenCellId;
      internal int OpenLastCellX;
      internal int level;
      internal int InitTblCol;
      internal bool PastingColumn;
    }
  }

  internal class ClsRtfOut
  {
    internal char[] buf;
    internal int BufIndex;
    internal int BufLen;
    internal tc.StrRtfColor[] color;
    internal int DelRevCount;
    internal bool FieldHasPara;
    internal int flags;
    internal tc.StrRtfOutGroup[] group;
    internal int GroupLevel;
    internal int line;
    internal StreamWriter oFile;
    internal int output;
    internal bool ParaFmtOnParaEnd;
    internal int sect;
    internal bool SpacePending;
    internal bool TblHilight;
    internal char[] text;
    internal int TextLen;
    internal int TotalColors;
    internal bool WatermarkWritten;
    internal bool WritingControl;
    internal int[] XlateLs;

    internal string StrText => new string(this.text);
  }

  internal class ClsTabw
  {
    internal byte[] CharFlags;
    internal int CharFlagsLen;
    internal int count;
    internal int FrameCharPos;
    internal int FrameScrWidth;
    internal int FrameSpaceWidth;
    internal int FrameWidth;
    internal int FrameX;
    internal int height;
    internal int JustAdj;
    internal int JustCount;
    internal int JustSpaceCount;
    internal int JustSpaceIgnore;
    internal int ListFontId;
    internal int ListNbr;
    internal int ListnumCount;
    internal string ListText;
    internal int ListTextWidth;
    internal string pAutoNumLgl;
    internal tc.StrListnum[] pListnum;
    internal int section;
    internal int type;
    internal int[] width;

    internal ClsTabw() => this.width = new int[21];

    internal tc.ClsTabw Copy()
    {
      tc.ClsTabw clsTabw = (tc.ClsTabw) this.MemberwiseClone();
      int length1;
      if (this.width != null && (length1 = this.width.Length) > 0)
      {
        clsTabw.width = new int[length1];
        for (int index = 0; index < length1; ++index)
          clsTabw.width[index] = this.width[index];
      }
      int length2;
      if (this.CharFlags != null && (length2 = this.CharFlags.Length) > 0)
      {
        clsTabw.CharFlags = new byte[length2];
        for (int index = 0; index < length2; ++index)
          clsTabw.CharFlags[index] = this.CharFlags[index];
      }
      int length3;
      if (this.pListnum != null && (length3 = this.pListnum.Length) > 0)
      {
        clsTabw.pListnum = new tc.StrListnum[length3];
        for (int index = 0; index < length3; ++index)
          clsTabw.pListnum[index] = this.pListnum[index];
      }
      return clsTabw;
    }
  }

  internal delegate ushort DgtGetNewFontId(
    ushort OldFmt,
    int data1,
    int data2,
    string str1,
    int line,
    int col);

  internal struct StrBlt
  {
    internal bool IsBullet;
    internal int flags;
    internal int start;
    internal int level;
    internal int NumberType;
    internal int font;
    internal char BulletChar;
    internal char BefChar;
    internal char AftChar;
    internal string BefText;
    internal int ls;
    internal int lvl;

    internal tc.StrBlt init()
    {
      this.BefText = "";
      return this;
    }
  }

  internal struct StrCell
  {
    internal bool InUse;
    internal int row;
    internal int NextCell;
    internal int PrevCell;
    internal int x;
    internal int width;
    internal int border;
    internal int[] BorderWidth;
    internal int margin;
    internal int height;
    internal int FirstLine;
    internal int LastLine;
    internal int shading;
    internal int flags;
    internal int RowSpan;
    internal int ColSpan;
    internal Color BackColor;
    internal int FixWidth;
    internal int level;
    internal int ParentCell;
    internal Color[] BorderColor;
    internal int TextAngle;

    internal tc.StrCell init()
    {
      this.BorderWidth = new int[4];
      this.BorderColor = new Color[4];
      return this;
    }

    internal tc.StrCell Copy()
    {
      tc.StrCell strCell = (tc.StrCell) this.MemberwiseClone();
      strCell.init();
      int length1;
      if (this.BorderWidth != null && (length1 = this.BorderWidth.Length) > 0)
      {
        for (int index = 0; index < length1; ++index)
          strCell.BorderWidth[index] = this.BorderWidth[index];
      }
      int length2;
      if (this.BorderColor != null && (length2 = this.BorderColor.Length) > 0)
      {
        for (int index = 0; index < length2; ++index)
          strCell.BorderColor[index] = this.BorderColor[index];
      }
      return strCell;
    }
  }

  internal struct StrCellAux
  {
    internal int PrevColCell;
    internal int NextColCell;
    internal int SpanningCell;
    internal int height;
    internal int flags;
    internal int FirstPage;
    internal int LastPage;
    internal int FirstPageHt;
    internal int LastPageHt;
    internal int BaseHeight;
    internal int SpaceBefore;
    internal int FrameId;
    internal int TempPfmt;

    internal tc.StrCellAux Copy() => (tc.StrCellAux) this.MemberwiseClone();
  }

  public struct StrCharTag
  {
    internal bool InUse;
    internal string name;
    internal int type;
    internal string HtmlInfo;
    public string AuxText;
    internal int AuxInt;
    internal object obj;
    internal int line;
    internal int next;
  }

  internal struct StrControl
  {
    internal string CtlClass;
    internal int style;
    internal int id;
  }

  internal struct StrDlgParam
  {
    internal char[] data;
  }

  internal struct StrDragObj
  {
    internal bool InUse;
    internal bool drawn;
    internal int type;
    internal int id1;
    internal int id2;
    internal int id3;
    internal Point[] ObjPoint;
    internal int ObjPointCount;
    internal COp.RECT ObjRect;
    internal COp.RECT[] HotRect;
    internal int HotRectCount;
    internal bool IsHotPolygon;
    internal bool TextHilighted;
    internal double AspectRatio;
  }

  internal struct StrEnumFonts
  {
    internal bool GetMultiple;
    internal bool EnumTypeFace;
    internal bool result;
    internal byte CharSet;
    internal byte PitchAndFamily;
    internal int TypeCount;
    internal char[][] TypeNames;
    internal int PointCount;
    internal int[] height;
    internal bool TrueType;
  }

  internal struct StrFont
  {
    internal Graphics gr;
    internal Font font;
    internal IntPtr hFont;
    internal int GlbFontId;
    internal string TypeFace;
    internal byte FontFamily;
    internal byte CharSet;
    internal int TwipsSize;
    internal Color TextColor;
    internal Color TextBkColor;
    internal Color UlineColor;
    internal int CharStyId;
    internal int ParaStyId;
    internal int expand;
    internal int lang;
    internal int offset;
    internal int OffsetVal;
    internal string FieldCode;
    internal int TextAngle;
    internal int ObjectType;
    internal int ObjectSize;
    internal ushort OleNotice;
    internal int ObjectAspect;
    internal bool ObjectUpdate;
    internal Control ctl;
    internal int PictType;
    internal Guid ImageType;
    internal IntPtr hMeta;
    internal Image _image;
    internal string PictFile;
    internal tc.ClsForm form;
    internal int bmHeight;
    internal int bmWidth;
    internal int PictHeight;
    internal int PictWidth;
    internal int ColorsUsed;
    internal int PictAlign;
    internal int FrameType;
    internal int ParaFID;
    internal int PictX;
    internal int PictY;
    internal tc.StrRgb[] rgb;
    internal string LinkFile;
    internal string PictAlt;
    internal int TotalRgbs;
    internal int PctWidth;
    internal tc.ClsAnim anim;
    internal int DispFrame;
    internal int MapId;
    internal byte[] PictData;
    internal int CropLeft;
    internal int CropRight;
    internal int CropTop;
    internal int CropBot;
    internal int OrigPictWidth;
    internal int OrigPictHeight;
    internal int InsRev;
    internal tc.ClsDateTime InsTime;
    internal int DelRev;
    internal tc.ClsDateTime DelTime;
    internal int height;
    internal int BaseHeight;
    internal int BaseHeightAdj;
    internal int style;
    internal int TempStyle;
    internal bool InUse;
    internal int[] CharWidth;
    internal int FieldId;
    internal int AuxId;
    internal int Aux1Id;
    internal int flags;
    internal int ExtLead;
    internal int RtfIndex;
    internal tc.ClsHdnFont hidden;
    internal bool VarWidth;
    internal bool rtl;
    internal int CharId;
    internal COp.TEXTMETRIC? TextMetric;

    internal Image image
    {
      get => this._image;
      set => this._image = value;
    }
  }

  internal struct StrFrame
  {
    internal bool empty;
    internal int PageFirstLine;
    internal int PageLastLine;
    internal int ScrFirstLine;
    internal int ScrLastLine;
    internal int x;
    internal int y;
    internal int ScrX;
    internal int ScrY;
    internal int width;
    internal int height;
    internal int TextHeight;
    internal int ScrWidth;
    internal int ScrHeight;
    internal int RowOffset;
    internal int sect;
    internal int border;
    internal int[] BorderWidth;
    internal Color[] BorderColor;
    internal bool LastColumnFrame;
    internal int RowId;
    internal int CellId;
    internal int SpaceTop;
    internal int SpaceBot;
    internal int SpaceLeft;
    internal int SpaceRight;
    internal int shading;
    internal int ParaFrameId;
    internal int flags;
    internal int flags1;
    internal Color BackColor;
    internal int LeftBorderWdth;
    internal int ZOrder;
    internal int DispFrame;
    internal int level;
    internal int BoxFrame;
    internal int ParentFrame;
    internal int OrigX;

    internal tc.StrFrame Copy()
    {
      tc.StrFrame strFrame = (tc.StrFrame) this.MemberwiseClone();
      int length1;
      if (this.BorderWidth != null && (length1 = this.BorderWidth.Length) > 0)
      {
        strFrame.BorderWidth = new int[length1];
        for (int index = 0; index < length1; ++index)
          strFrame.BorderWidth[index] = this.BorderWidth[index];
      }
      int length2;
      if (this.BorderColor != null && (length2 = this.BorderColor.Length) > 0)
      {
        strFrame.BorderColor = new Color[length2];
        for (int index = 0; index < length2; ++index)
          strFrame.BorderColor[index] = this.BorderColor[index];
      }
      return strFrame;
    }
  }

  internal struct StrGifAnim
  {
    internal int[] hDIB;
    internal int AnimLoops;
    internal int AnimPicts;
    internal int[] delay;
    internal int InitDelay;
  }

  internal struct StrGlbFont
  {
    internal int UseCount;
    internal Font font;
    internal IntPtr hFont;
    internal COp.LOGFONT lFont;
    internal IntPtr hDC;
    internal bool IsScrDC;
  }

  internal struct StrGlbPrtDC
  {
    internal int UseCount;
    internal IntPtr hDC;
    internal string name;
    internal string drv;
    internal string port;
  }

  internal struct StrHdrFtr
  {
    internal int FirstLine;
    internal int LastLine;
    internal int height;
    internal int TextHeight;
    internal int LimitFtrLine;
  }

  internal struct StrHlSeg
  {
    internal int x;
    internal int y;
    internal int width;
    internal int height;
  }

  internal struct StrHyperlink
  {
    internal string code;
    internal string text;
    internal bool DoubleClick;
    internal bool RightClick;
    internal bool used;
  }

  internal struct StrImageMap
  {
    internal string name;
    internal int TotalRects;
    internal tc.StrImageMapRect[] pMapRect;
  }

  internal struct StrImageMapRect
  {
    internal string name;
    internal string LinkInfo;
    internal string target;
    internal COp.RECT rect;
  }

  internal struct StrJpg
  {
    internal StreamReader iFile;
    internal byte[] buf;
    internal int BufLen;
    internal int BufIndex;
    internal byte[] text;
    internal int TextLen;
    internal int TextIndex;
    internal byte CurChar;
    internal ushort CurWord;
    internal byte[] stack;
    internal int StackLen;
    internal bool eof;
    internal int FilePos;
    internal bool IsProgressive;
    internal int FirstIdx;
    internal int LastIdx;
    internal int ShiftFactor;
    internal int PrevShiftFactor;
    internal int BitsPerPixel;
    internal int BytesPerPixel;
    internal byte[] pImage;
    internal int ScanLineSize;
    internal int width;
    internal int height;
    internal int ColorTableOffset;
    internal ushort[] red;
    internal ushort[] green;
    internal ushort[] blue;
    internal byte[][] QTable;
    internal int NumComps;
    internal int[] FrameCompId;
    internal int[] HorzSamp;
    internal int[] VertSamp;
    internal int[] QTableId;
    internal int[] HmDCId;
    internal int[] HmACId;
    internal int[] PrevDC;
    internal byte[] pHist;
    internal byte[] pComp;
    internal short[] pBlock;
    internal int EobCount;
    internal int ScanCompCount;
    internal int[] ScanCompId;
    internal int[] ScanBlocksPerMcu;
    internal int ScanMcuPerRow;
    internal int ScanMcuPerCol;
    internal int ScanMaxMcu;
    internal int interleaved;
    internal int HuffTblCount;
    internal tc.StrJpgHuffTbl[] HuffTbl;
    internal int InDword;
    internal int BitIdx;
    internal int CurSym;
    internal int McuPerInterval;
    internal byte MarkerInScan;
    internal int McuPerRow;
    internal int McuPerCol;
    internal int RowsPerMcu;
    internal int ColsPerMcu;
    internal int BytesPerMcu;
    internal int[] BlocksPerRow;
    internal int BlocksPerMcu;
    internal int[] PixelsPerSampX;
    internal int[] PixelsPerSampY;
    internal int CurMcu;
    internal int MaxMcu;
    internal int CurX;
    internal int CurY;
  }

  internal struct StrJpgHuffTbl
  {
    internal bool IsDC;
    internal int id;
    internal byte[] CodeLen;
    internal byte[] pSymbol;
    internal byte[] pSymLen;
    internal ushort[] pCode;
  }

  internal struct StrJpgIfd
  {
    internal ushort tag;
    internal ushort type;
    internal int count;
    internal int data;
  }

  internal struct StrLineSeg
  {
    internal int col;
    internal int count;
    internal int x;
    internal int width;
    internal bool rtl;
    internal tc.StrLineSeg[] pFirstSeg;
    internal int idx;
    internal bool ParaRtl;
    internal int TotalLineSeg;
  }

  internal struct StrList
  {
    internal bool InUse;
    internal int id;
    internal int TmplId;
    internal int FontId;
    internal int LevelCount;
    internal int flags;
    internal string name;
    internal tc.StrListLevel[] level;
  }

  internal struct StrListLevel
  {
    internal int start;
    internal int NumType;
    internal int CharAft;
    internal int LeftIndent;
    internal int RightIndent;
    internal int FirstIndent;
    internal int ParaFlags;
    internal int FontStyles;
    internal int FontId;
    internal char[] text;
    internal int flags;
    internal int MinIndent;
    internal int FontStylesOff;

    internal tc.StrListLevel init()
    {
      this.text = new char[50];
      this.text[0] = char.MinValue;
      return this;
    }

    internal tc.StrListLevel Copy()
    {
      tc.StrListLevel strListLevel = this;
      if (this.text != null)
      {
        int length = 50;
        if (this.text.Length > 50)
          length = this.text.Length;
        strListLevel.text = new char[length];
        for (int index = 0; index < this.text.Length; ++index)
          strListLevel.text[index] = this.text[index];
      }
      return strListLevel;
    }
  }

  internal struct StrListnum
  {
    internal string text;
    internal int ListNbr;
    internal int ls;
    internal int lvl;
    internal bool IsHPARA;
  }

  internal struct StrListOr
  {
    internal bool InUse;
    internal int ListIdx;
    internal int LevelCount;
    internal tc.StrListLevel[] level;
    internal int RtfLs;
    internal int flags;
  }

  internal struct StrMergeData
  {
    internal string pName;
    internal string pData;
  }

  internal struct StrMetaFileHdr
  {
    internal int sign;
    internal ushort hmf;
    internal short left;
    internal short top;
    internal short right;
    internal short bottom;
    internal short UnitsPerInch;
    internal int reserved;
    internal ushort CheckSum;
  }

  internal struct StrOpGr
  {
    internal Graphics gr;
    internal Color TextColor;
    internal Color BkColor;
    internal int BkMode;
    internal IntPtr hFont;
    internal Pen pen;
    internal Brush brush;
    internal IntPtr hDC;
  }

  internal struct StrPage
  {
    internal int FirstLine;
    internal int LastLine;
    internal int FirstRow;
    internal int LastRow;
    internal int DispNbr;
    internal int ScrHt;
    internal int TextHt;
    internal int BodyHt;
    internal int BodyTextHt;
    internal int FnoteHt;
    internal int flags;
    internal int TopSect;
    internal int TblHdrHt;
    internal int TblHdrFirstLine;
    internal int TblHdrLastLine;
    internal int FrameCount;
    internal int HdrSect;
    internal int FtrSect;
  }

  internal struct StrParaFrame
  {
    internal bool InUse;
    internal int x;
    internal int y;
    internal int ParaY;
    internal int width;
    internal int margin;
    internal int height;
    internal int MinHeight;
    internal int DistFromText;
    internal int PageNo;
    internal int flags;
    internal int TextLine;
    internal int LineType;
    internal Color BackColor;
    internal int FillPattern;
    internal int LineWdth;
    internal int PageY;
    internal Color LineColor;
    internal int pict;
    internal int ZOrder;
    internal int CellId;
    internal int TextAngle;
    internal int GroupX;
    internal int GroupY;
    internal int ShapeType;
    internal int FillPict;
    internal int OrgX;
    internal int OrgY;
    internal bool rotatedFrame;

    internal tc.StrParaFrame Copy() => (tc.StrParaFrame) this.MemberwiseClone();
  }

  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  internal struct StrPdfBkm
  {
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    internal byte[] sign;
    internal int level;
    internal int page;
    internal int y;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
    internal string name;

    internal tc.StrPdfBkm init()
    {
      this.sign = new byte[2];
      return this;
    }

    internal byte[] ToByteArray()
    {
      int length = Marshal.SizeOf<tc.StrPdfBkm>(this);
      IntPtr ptr = Marshal.AllocCoTaskMem(length + 1);
      Marshal.StructureToPtr<tc.StrPdfBkm>(this, ptr, true);
      byte[] byteArray = new byte[length];
      for (int ofs = 0; ofs < length; ++ofs)
        byteArray[ofs] = Marshal.ReadByte(ptr, ofs);
      Marshal.FreeCoTaskMem(ptr);
      return byteArray;
    }
  }

  internal struct StrPendPict
  {
    internal int pict;
    internal int x;
    internal int y;
    internal int width;
    internal int height;
    internal int count;
    internal int LineNo;
  }

  internal struct StrPfmt
  {
    internal int LeftIndent;
    internal int RightIndent;
    internal int FirstIndent;
    internal int LeftIndentTwips;
    internal int RightIndentTwips;
    internal int FirstIndentTwips;
    internal int flags;
    internal int TabId;
    internal int BltId;
    internal int StyId;
    internal int shading;
    internal int pflags;
    internal int SpaceBefore;
    internal int SpaceAfter;
    internal int SpaceBetween;
    internal int AuxId;
    internal int Aux1Id;
    internal Color BkColor;
    internal int BorderSpace;
    internal int LineSpacing;
    internal int flow;
    internal Color BorderColor;

    internal tc.StrPfmt Copy() => (tc.StrPfmt) this.MemberwiseClone();
  }

  internal struct StrPng
  {
    internal bool input;
    internal byte[] buf;
    internal int BufLen;
    internal int BufIndex;
    internal byte[] text;
    internal int TextLen;
    internal int TextIndex;
    internal byte CurChar;
    internal ushort CurWord;
    internal byte[] stack;
    internal int StackLen;
    internal bool eof;
    internal int FilePos;
    internal bool extracted;
    internal int ChunkSize;
    internal byte[] pImage;
    internal byte[] pImageScan;
    internal byte pData;
    internal int DataSize;
    internal int DataPos;
    internal int DataBitPos;
    internal byte[] pPrevScan;
    internal byte[] pScan;
    internal int FilterType;
    internal int FilterBufSize;
    internal int CurFilterByte;
    internal int[] RunLenVal;
    internal int[] RunLenBits;
    internal int[] DistVal;
    internal int[] DistBits;
    internal int[][] grid;
    internal int GridX;
    internal int GridY;
    internal int GridPass;
    internal int width;
    internal int height;
    internal int BytesPerColor;
    internal int BytesPerPixel;
    internal int BytesPerPixelAlpha;
    internal int BitDepth;
    internal int ColorComponents;
    internal int PixelsPerByte;
    internal int CurColorByte;
    internal int CurColor;
    internal int ClrTblSize;
    internal int ColorType;
    internal int AspectRatio;
    internal int interlaced;
    internal int InterlacePass;
    internal bool NewScanLine;
    internal int ScanLineSize;
    internal bool ReadAlpha;
    internal bool PalUsed;
    internal bool AlphaPending;
    internal int CurAlphaByte;
    internal bool xparent;
    internal byte[] alpha;
    internal int AlphaCount;
    internal byte[] BkndColor;
    internal byte[] PixColor;
    internal byte[] XparentColor;
    internal int BkndIdx;
    internal Color[] GlobalColor;
    internal Color[] LocalColor;
    internal int CurX;
    internal int CurY;
    internal int pict;
    internal int[] CrcTable;
    internal int crc;
    internal int Checksum;
    internal byte[] CircData;
    internal ushort[] CircNext;
    internal ushort RunLen;
    internal ushort CircBufLen;
    internal ushort FirstRun;
    internal byte[] pInfo;
  }

  internal struct StrPngHuffTbl
  {
    internal int count;
    internal int FirstCode;
    internal int[] pSymbol;
    internal int[] pCode;
    internal int[] pCodeLen;
  }

  internal struct StrPrint
  {
    internal char InputType;
    internal string file;
    internal string buffer;
    internal Graphics gr;
    internal bool IsPrinter;
    internal Rectangle rect;
    internal bool FullPage;
    internal int StartPos;
    internal bool OnePage;
    internal int NextPos;
    internal string MergeFields;
    internal string MergeData;
    internal bool PrintHiddenText;
    internal bool PrintMarginArea;
    internal Form parent;
    internal int NextY;
  }

  internal struct StrPrtFont
  {
    internal Graphics gr;
    internal Font font;
    internal IntPtr hFont;
    internal int GlbFontId;
    internal int height;
    internal int BaseHeight;
    internal int BaseHeightAdj;
    internal int[] CharWidth;
    internal int ExtLead;
    internal int OffsetVal;
    internal tc.ClsHdnFont hidden;
  }

  internal struct StrReviewer
  {
    internal string name;
    internal Color InsColor;
    internal int InsStyle;
    internal Color DelColor;
    internal int DelStyle;
    internal int RtfId;
  }

  internal struct StrRgb
  {
    internal string red;
    internal string green;
    internal string blue;
    internal int count;
  }

  internal struct StrRtfColor
  {
    internal Color color;
  }

  internal struct StrRtfFont
  {
    internal bool InUse;
    internal int FontId;
    internal int CharSet;
    internal string family;
    internal string name;
    internal string name2;

    internal tc.StrRtfFont init()
    {
      this.family = this.name = this.name2 = "";
      return this;
    }
  }

  internal struct StrRtfGroup
  {
    internal tc.ClsRtf rtf;
    internal tc.StrRtfFont[] font;
    internal int MaxRtfFonts;
    internal tc.StrRtfColor[] color;
    internal int[] RtfSID;
    internal int MaxRtfSID;
    internal int RtfGroup;
    internal int FieldGroup;
    internal int DefFont;
    internal string FontFamily;
    internal string TypeFace;
    internal int CharSet;
    internal string FontFamilyDB;
    internal string TypeFaceDB;
    internal int CharSetDB;
    internal string FontFamilyHi;
    internal string TypeFaceHi;
    internal int CharSetHi;
    internal bool rtlch;
    internal int PrevCharSet;
    internal int PointSize2;
    internal Color TextColor;
    internal Color TextBkColor;
    internal Color UlineColor;
    internal int style;
    internal int StyleOff;
    internal int CharStyId;
    internal int FieldId;
    internal int PictId;
    internal int AuxId;
    internal int CharId;
    internal int CharType;
    internal int expand;
    internal int CharScaleX;
    internal int lang;
    internal int offset;
    internal bool caps;
    internal char EndnoteMarker;
    internal int LeftIndent;
    internal int RightIndent;
    internal int FirstIndent;
    internal int flags;
    internal int pflags;
    internal tc.StrTab tab;
    internal tc.StrBlt blt;
    internal int ParShading;
    internal int SpaceBefore;
    internal int SpaceAfter;
    internal int SpaceBetween;
    internal int OutlineLevel;
    internal int LineSpacing;
    internal int ParaStyId;
    internal Color ParaBkColor;
    internal Color ParaBorderColor;
    internal int flow;
    internal int CellFlags;
    internal int NextStyId;
    internal bool InTable;
    internal int BorderType;
    internal int[] BorderWidth;
    internal int[] BorderSpace;
    internal Color[] BorderColor;
    internal int BorderMargin;
    internal int CellShading;
    internal int CellMargin;
    internal int CellColSpan;
    internal Color CellPatFC;
    internal Color CellPatBC;
    internal tc.StrRtfParaFrameInfo ParaFrameInfo;
    internal int FrmFlags;
    internal int TextBoxMargin;
    internal int ParaFID;
    internal int TextAngle;
    internal int gflags;
    internal int gflags2;
    internal string ControlWord;
    internal bool IgnoreText;
    internal int ControlCount;
    internal int AnimLoops;
    internal int AnimDelay;
    internal int LinkPictWidth;
    internal int LinkPictHeight;
    internal tc.StrShape shape;
    internal tc.StrShpGrp ShpGrp;
    internal int level;
    internal int RtfLs;
    internal int ListLvl;
    internal int CharBkPat;
    internal Color CharPatFC;
    internal Color CharPatBC;
    internal int IgnoreCount;
    internal int UcIgnoreCount;
    internal int HPageGroup;
    internal int DpGroupCount;
    internal int[] DpGroupX;
    internal int[] DpGroupY;
    internal int InsRev;
    internal tc.ClsDateTime InsTime;
    internal int DelRev;
    internal tc.ClsDateTime DelTime;
    internal bool revised;
    internal bool deleted;

    internal tc.StrRtfGroup init()
    {
      this.BorderWidth = new int[19];
      this.BorderSpace = new int[19];
      this.BorderColor = new Color[19];
      this.DpGroupX = new int[10];
      this.DpGroupY = new int[10];
      this.tab = new tc.StrTab();
      this.tab.SetSize(0);
      return this;
    }

    internal tc.StrRtfGroup Copy()
    {
      tc.StrRtfGroup strRtfGroup = (tc.StrRtfGroup) this.MemberwiseClone();
      strRtfGroup.init();
      int length1;
      if (this.BorderWidth != null && (length1 = this.BorderWidth.Length) > 0)
      {
        for (int index = 0; index < length1; ++index)
          strRtfGroup.BorderWidth[index] = this.BorderWidth[index];
      }
      int length2;
      if (this.BorderSpace != null && (length2 = this.BorderSpace.Length) > 0)
      {
        for (int index = 0; index < length2; ++index)
          strRtfGroup.BorderSpace[index] = this.BorderSpace[index];
      }
      int length3;
      if (this.BorderColor != null && (length3 = this.BorderColor.Length) > 0)
      {
        for (int index = 0; index < length3; ++index)
          strRtfGroup.BorderColor[index] = this.BorderColor[index];
      }
      int length4;
      if (this.DpGroupX != null && (length4 = this.DpGroupX.Length) > 0)
      {
        for (int index = 0; index < length4; ++index)
          strRtfGroup.DpGroupX[index] = this.DpGroupX[index];
      }
      int length5;
      if (this.DpGroupY != null && (length5 = this.DpGroupY.Length) > 0)
      {
        for (int index = 0; index < length5; ++index)
          strRtfGroup.DpGroupY[index] = this.DpGroupY[index];
      }
      strRtfGroup.tab = this.tab.Copy();
      strRtfGroup.blt = this.blt;
      return strRtfGroup;
    }
  }

  internal struct StrRtfObject
  {
    internal int ObjectType;
    internal byte[] data;
    internal int ObjectSize;
    internal int ObjectAspect;
    internal bool ObjectUpdate;
    internal int pict;
  }

  internal struct StrRtfOutGroup
  {
    internal int FontId;
    internal int style;
    internal int FieldId;
    internal string FieldCode;
    internal int flags;
  }

  internal struct StrRtfParaFrameInfo
  {
    internal int x;
    internal int y;
    internal int width;
    internal int height;
    internal int DistFromText;
    internal int ZOrder;
  }

  internal struct StrRtfPict
  {
    internal int type;
    internal Guid ImageType;
    internal byte[] data;
    internal int DataSize;
    internal Image image;
    internal int OrigHeight;
    internal int OrigWidth;
    internal int height;
    internal int width;
    internal int CropLeft;
    internal int CropRight;
    internal int CropTop;
    internal int CropBot;
    internal int ScaleX;
    internal int ScaleY;
    internal int PictType;
    internal int align;
    internal int PictId;
    internal int BitsPerPixel;
    internal int planes;
    internal int WidthBytes;
    internal int FormId;
    internal tc.ClsForm form;
    internal IntPtr hMeta;
  }

  internal struct StrScrCtl
  {
    internal bool InUse;
    internal int x;
    internal int y;
    internal int width;
    internal int height;
    internal int pict;
    internal Control ctl;
    internal int LineNo;
    internal int PaintId;
  }

  internal struct StrSect
  {
    internal bool InUse;
    internal float LeftMargin;
    internal float RightMargin;
    internal float TopMargin;
    internal float BotMargin;
    internal int columns;
    internal float ColumnSpace;
    internal int FirstLine;
    internal int LastLine;
    internal bool IsPortrait;
    internal int flags;
    internal int LastPage;
    internal int PageNumFmt;
    internal short FirstPageNo;
    internal float HdrMargin;
    internal float FtrMargin;
    internal PaperSourceKind FirstPageBin;
    internal PaperSourceKind bin;
    internal int border;
    internal int[] BorderWidth;
    internal int[] BorderSpace;
    internal int BorderOpts;
    internal Color BorderColor;
    internal int BorderType;
    internal PaperKind PprKind;
    internal float PprWidth;
    internal float PprHeight;
    internal int flow;
    internal int LineSpace;
    internal int LineStep;

    internal tc.StrSect init()
    {
      this.BorderWidth = new int[4];
      this.BorderSpace = new int[4];
      return this;
    }

    internal tc.StrSect Copy()
    {
      tc.StrSect strSect = (tc.StrSect) this.MemberwiseClone();
      strSect.init();
      int length1;
      if (this.BorderWidth != null && (length1 = this.BorderWidth.Length) > 0)
      {
        for (int index = 0; index < length1; ++index)
          strSect.BorderWidth[index] = this.BorderWidth[index];
      }
      int length2;
      if (this.BorderSpace != null && (length2 = this.BorderSpace.Length) > 0)
      {
        for (int index = 0; index < length2; ++index)
          strSect.BorderSpace[index] = this.BorderSpace[index];
      }
      return strSect;
    }
  }

  internal struct StrSect1
  {
    internal int PrevSect;
    internal int NextSect;
    internal tc.StrHdrFtr hdr;
    internal tc.StrHdrFtr ftr;
    internal tc.StrHdrFtr fhdr;
    internal tc.StrHdrFtr fftr;
    internal int HiddenX;
    internal int HiddenY;
    internal float _PgHeight;
    internal float PgWidth;
    internal int LastPageHeight;

    internal float PgHeight
    {
      get => this._PgHeight;
      set => this._PgHeight = value;
    }
  }

  internal struct StrShape
  {
    internal int x;
    internal int y;
    internal int width;
    internal int height;
    internal int FrmFlags;
    internal int WrapType;
    internal int WrapSide;
    internal int type;
    internal int align;
    internal int ZOrder;
    internal Color LineColor;
    internal Color BackColor;
    internal int FillPattern;
    internal int LineWdth;
    internal int DistFromText;
    internal int FillPict;
  }

  internal struct StrShpGrp
  {
    internal int left;
    internal int width;
    internal int top;
    internal int height;
    internal int GroupLeft;
    internal int GroupTop;
    internal int GroupWidth;
    internal int GroupHeight;
  }

  internal struct StrStResult
  {
    internal int code;
    internal string replace;
    internal int TotalAltWords;
    internal string[] AltWord;
  }

  internal struct StrStyleId
  {
    internal bool InUse;
    internal string name;
    internal int type;
    internal byte FontFamily;
    internal int TwipsSize;
    internal int style;
    internal Color TextColor;
    internal Color TextBkColor;
    internal int LeftIndentTwips;
    internal int RightIndentTwips;
    internal int FirstIndentTwips;
    internal int ParaFlags;
    internal int shading;
    internal int SpaceBefore;
    internal int SpaceAfter;
    internal int SpaceBetween;
    internal int TabId;
    internal int OutlineLevel;
    internal int flags;
    internal int LineSpacing;
    internal int next;
    internal int RtfIndex;
    internal int pflags;
    internal int BltId;
    internal Color ParaBkColor;
    internal int expand;
    internal string TypeFace;
    internal Color UlineColor;
    internal int HeadingNo;
    internal Color ParaBorderColor;
    internal int offset;
  }

  internal struct StrTab
  {
    internal int count;
    internal int[] pos;
    internal int[] type;
    internal byte[] flags;

    internal void SetSize(int cnt)
    {
      this.count = cnt;
      this.pos = new int[20];
      this.type = new int[20];
      this.flags = new byte[20];
    }

    internal tc.StrTab Copy()
    {
      tc.StrTab strTab = (tc.StrTab) this.MemberwiseClone();
      strTab.SetSize(this.count);
      int length1;
      if (this.pos != null && (length1 = this.pos.Length) > 0)
      {
        for (int index = 0; index < length1; ++index)
          strTab.pos[index] = this.pos[index];
      }
      int length2;
      if (this.type != null && (length2 = this.type.Length) > 0)
      {
        for (int index = 0; index < length2; ++index)
          strTab.type[index] = this.type[index];
      }
      int length3;
      if (this.flags != null && (length3 = this.flags.Length) > 0)
      {
        for (int index = 0; index < length3; ++index)
          strTab.flags[index] = this.flags[index];
      }
      return strTab;
    }
  }

  internal struct StrTableAux
  {
    internal int BotRowHt;
    internal int TopRowHt;
    internal int FirstPage;
    internal int LastPage;
    internal int flags;
    internal int FrmBegX;
    internal int FrmEndX;

    internal tc.StrTableAux init() => this;

    internal tc.StrTableAux Copy()
    {
      tc.StrTableAux strTableAux = (tc.StrTableAux) this.MemberwiseClone();
      strTableAux.init();
      return strTableAux;
    }
  }

  internal struct StrTableRow
  {
    internal bool InUse;
    internal int FixWidth;
    internal int FirstCell;
    internal int LastCell;
    internal int FrmSpcBef;
    internal int height;
    internal int PrevRow;
    internal int NextRow;
    internal int indent;
    internal int flags;
    internal int CellMargin;
    internal int border;
    internal int[] BorderWidth;
    internal int FirstFrame;
    internal int LastFrame;
    internal int AddedIndent;
    internal int MinHeight;
    internal int CurIndent;
    internal int MinPictHeight;
    internal int PageNo;
    internal int id;
    internal int flow;

    internal tc.StrTableRow init()
    {
      this.BorderWidth = new int[4];
      return this;
    }

    internal tc.StrTableRow Copy()
    {
      tc.StrTableRow strTableRow = (tc.StrTableRow) this.MemberwiseClone();
      strTableRow.init();
      int length;
      if (this.BorderWidth != null && (length = this.BorderWidth.Length) > 0)
      {
        for (int index = 0; index < length; ++index)
          strTableRow.BorderWidth[index] = this.BorderWidth[index];
      }
      return strTableRow;
    }
  }

  internal struct StrTerField
  {
    internal int CurCol;
    internal int WrapFlag;
    internal bool PaintEnabled;
    internal int CurRow;
    internal int BeginLine;
    internal int CurLine;
    internal Color TextBkColor;
    internal Color StatusBkColor;
    internal Color StatusColor;
    internal int HilightType;
    internal int HilightBegCol;
    internal int HilightEndCol;
    internal int HilightBegRow;
    internal int HilightEndRow;
    internal bool StretchHilight;
    internal char[] text;
    internal ushort[] font;
    internal int pfmt;
    internal int LineLen;
    internal int TextApply;
    internal bool ReclaimResources;
    internal bool ModifyProtectColor;
    internal bool LinkDblClick;
    internal bool ShowProtectCaret;
    internal int LinkStyle;
    internal Color LinkColor;
    internal bool SnapToGrid;
    internal bool HtmlMode;
    internal bool ShowTableGridLines;
    internal IntPtr hTerWnd;
    internal Graphics TerGr;
    internal Rectangle TerRect;
    internal Rectangle TerWinRect;
    internal int TotalLines;
    internal int MouseLine;
    internal int MaxColBlock;
    internal int TotalPfmts;
    internal int TotalFonts;
    internal int TotalStyles;
    internal int WinWidth;
    internal int WinHeight;
    internal int TerWinOrgX;
    internal int MouseCol;
    internal bool modified;
    internal bool WordWrap;
    internal int ParaLeftIndent;
    internal int ParaRightIndent;
    internal int ParaFirstIndent;
    internal int ParaFlags;
    internal int ParaTabId;
    internal int ParaCellId;
    internal int ParaShading;
    internal int ParaFrameId;
    internal int ParaSpaceBefore;
    internal int ParaSpaceAfter;
    internal int ParaSpaceBetween;
    internal int ParaStyleId;
    internal int ParaAuxId;
    internal int pflags;
    internal int CurSect;
    internal int LeftMargin;
    internal int RightMargin;
    internal int TopMargin;
    internal int BotMargin;
    internal int columns;
    internal int CurPage;
    internal int TotalPages;
    internal int MouseX;
    internal int MouseY;
    internal bool PrintView;
    internal bool PageMode;
    internal bool FittedView;
    internal bool ShowParaMark;
    internal bool ShowHiddenText;
    internal int CurCtlId;
    internal int ParaFrameFlags;
  }

  internal struct StrTextBuf
  {
    internal char[] pBuf;
    internal char[] lead;
    internal ushort[] pFmt;
    internal int len;
    internal int index;
    internal bool eof;
    internal bool eol;
    internal bool CrFound;
    internal int MaxLineLen;
    internal bool ParaCharFound;
  }

  internal struct StrTlb
  {
    internal int id;
    internal int CmdId;
    internal Control ctl;
    internal string pBalloon;
    internal int flags;
    internal int x;
    internal int y;
    internal int width;
    internal int height;
    internal int IconX;
    internal int IconY;
    internal Image image;

    internal tc.StrTlb Copy() => (tc.StrTlb) this.MemberwiseClone();
  }

  internal struct StrTlbCustId
  {
    internal bool InUse;
    internal int id;
    internal Image image;
    internal int line;
    internal string pBalloon;
    internal int CmdId;
  }

  internal struct StrUndo
  {
    internal char[] txt;
    internal ushort[] fmt;
    internal int[] pfmt;
    internal int beg;
    internal int end;
    internal char type;
    internal int id;
    internal int TblLevel;
    internal bool EmbTable;
    internal int ObjId;
    internal int width;
    internal int height;
    internal tc.ClsParaFrame pFrame;
    internal tc.ClsLinePtr[] LinePtrU;
    internal int TotalLinesU;
    internal int MaxLinesU;
    internal int CursPos;
    internal int RowCount;
    internal int CellCount;
    internal int[] RowId;
    internal int[] CellId;
    internal tc.StrTableRow[] pRow;
    internal tc.StrCell[] pCell;
  }

  internal struct StrWinPtr
  {
    internal bool InUse;
    internal IntPtr hWnd;
    internal ImRtfEditor e;
  }
}
