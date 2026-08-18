// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.CMisc
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class CMisc : COp
{
  internal CMisc(ImRtfEditor ImRtfEditor)
    : base(ImRtfEditor)
  {
  }

  internal new void AddChar(ref string str, char chr) => str += new string(chr, 1);

  internal new int AddCrLf(int index, char[] pUndo, ushort[] pUndoCfmt)
  {
    pUndo[index] = '\r';
    if (pUndoCfmt != null)
    {
      pUndoCfmt[index] = index <= 0 ? (ushort) 0 : pUndoCfmt[index - 1];
      if ((this.e.TerFont[(int) pUndoCfmt[index]].style & 128 /*0x80*/) != 0)
        pUndoCfmt[index] = (ushort) 0;
    }
    ++index;
    pUndo[index] = '\n';
    if (pUndoCfmt != null)
      pUndoCfmt[index] = pUndoCfmt[index - 1];
    ++index;
    return index;
  }

  internal new void AddSlashes(string InStr, out string OutStr, int count)
  {
    int length = InStr.Length;
    StringBuilder stringBuilder = new StringBuilder(length * count, length * count);
    stringBuilder.Length = 0;
    for (int index1 = 0; index1 < length; ++index1)
    {
      stringBuilder.Append(InStr[index1]);
      if (InStr[index1] == '\\')
      {
        for (int index2 = 0; index2 < count - 1; ++index2)
          stringBuilder.Append(InStr[index1]);
      }
    }
    OutStr = stringBuilder.ToString();
  }

  internal new bool AdjustHtmlPictWidth()
  {
    for (int pict = 0; pict < this.e.TotalFonts; ++pict)
    {
      if (this.e.TerFont[pict].InUse && (this.e.TerFont[pict].style & 128 /*0x80*/) != 0 && this.e.TerFont[pict].PctWidth != 0)
        this.e.TerSetPictPctWidth(pict, this.e.TerFont[pict].PctWidth);
    }
    return true;
  }

  internal new bool AdjustHtmlRulerWidth()
  {
    for (int index = 0; index < this.e.TotalPfmts; ++index)
    {
      if ((this.e.PfmtId[index].AuxId & 3584 /*0x0E00*/) >> 9 == 3)
      {
        int y = (int) (short) COp.LOWORD(this.e.PfmtId[index].Aux1Id);
        if (y != 0)
        {
          int num1 = (int) COp.HIWORD(this.e.PfmtId[index].Aux1Id);
          int num2 = y < 0 ? -y : this.MulDiv(this.e.TerWinWidth, y, 100);
          int x1;
          int x2 = x1 = 0;
          switch (num1)
          {
            case 1:
              x2 = x1 = (this.e.TerWinWidth - num2) / 2;
              break;
            case 2:
              x2 = this.e.TerWinWidth - num2;
              break;
            default:
              x1 = this.e.TerWinWidth - num2;
              break;
          }
          if (x2 < 0)
            x2 = 0;
          if (x1 < 0)
            x1 = 0;
          this.e.PfmtId[index].LeftIndentTwips = this.ScrToTwipsX(x2);
          this.e.PfmtId[index].LeftIndent = x2;
          this.e.PfmtId[index].RightIndentTwips = this.ScrToTwipsX(x1);
          this.e.PfmtId[index].RightIndent = x1;
        }
      }
    }
    return true;
  }

  internal new bool AllocTabw(int line)
  {
    this.e.text[line].tabw = new tc.ClsTabw();
    return true;
  }

  internal new bool AllocTabwCharFlags(int line)
  {
    if (this.False(this.e.text[line].tabw) && !this.AllocTabw(line))
      return false;
    this.e.text[line].tabw.CharFlags = new byte[this.e.text[line].len];
    this.e.text[line].tabw.CharFlagsLen = this.e.text[line].len;
    for (int index = 0; index < this.e.text[line].len; ++index)
      this.e.text[line].tabw.CharFlags[index] = (byte) 0;
    return true;
  }

  internal new string AlphaFormat(int nbr, bool upper)
  {
    int num = (nbr - 1) / 26;
    char ch = (char) ((upper ? 65 : 97) + (int) (ushort) ((nbr - 1) % 26));
    char[] chArray = new char[1000];
    int length;
    for (length = 0; length < num + 1; ++length)
      chArray[length] = ch;
    chArray[length] = char.MinValue;
    return new string(chArray, 0, length);
  }

  internal new bool ApplyZoomPercent(int NewZoom)
  {
    this.e.ScrResX = this.MulDiv(this.e.OrigScrResX, NewZoom, 100);
    this.e.ScrResY = this.MulDiv(this.e.OrigScrResY, NewZoom, 100);
    for (int index = 0; index < this.e.TotalPfmts; ++index)
    {
      this.e.PfmtId[index].LeftIndent = this.TwipsToScrX(this.e.PfmtId[index].LeftIndentTwips);
      this.e.PfmtId[index].RightIndent = this.TwipsToScrX(this.e.PfmtId[index].RightIndentTwips);
      this.e.PfmtId[index].FirstIndent = this.TwipsToScrX(this.e.PfmtId[index].FirstIndentTwips);
    }
    this.e.ZoomPercent = NewZoom;
    return true;
  }

  internal new bool CallDialogBox(Form dlg)
  {
    int num = this.e.CaretEngaged ? 1 : 0;
    this.e.InDialogBox = true;
    bool flag = DialogResult.OK == dlg.ShowDialog();
    dlg.Dispose();
    this.e.InDialogBox = false;
    this.e.TlbIdClicked = 0;
    this.e.Focus();
    this.InitCaret();
    if (num != 0 && !this.e.CaretEngaged)
      this.e.TerEngageCaret(true);
    return flag;
  }

  internal bool CenterDlgBox(Form dlg)
  {
    int num1;
    int num2;
    if ((this.e.TerFlags6 & 1048576 /*0x100000*/) != 0)
    {
      Rectangle workingArea = SystemInformation.WorkingArea;
      num1 = (workingArea.Width - dlg.Width) / 2;
      num2 = (workingArea.Height - dlg.Height) / 2;
    }
    else
    {
      Point screen = this.e.PointToScreen(new Point(0, 0));
      num1 = screen.X + (this.e.Width - dlg.Width) / 2;
      num2 = screen.Y + (this.e.Height - dlg.Height) / 2;
    }
    dlg.Left = num1;
    dlg.Top = num2;
    return true;
  }

  internal new bool CharMessagePending()
  {
    COp.MSG msg;
    return (this.PeekMessage(out msg, this.e.hTerWnd, 258, 258, 2) || this.PeekMessage(out msg, this.e.hTerWnd, 256 /*0x0100*/, 256 /*0x0100*/, 2)) && msg.hwnd == this.e.hTerWnd || this.e.HilightType != 0 && this.e.StretchHilight && this.PeekMessage(out msg, this.e.hTerWnd, 512 /*0x0200*/, 512 /*0x0200*/, 2) && msg.hwnd == this.e.hTerWnd;
  }

  internal new bool CheckDlgValue(
    Form form,
    char type,
    TextBox tb,
    double BeginRange,
    double EndRange)
  {
    bool error = false;
    double num1 = type != 'I' ? this.ToDouble(tb.Text, out error) : (double) this.ToInt(tb.Text, out error);
    if (!error && num1 >= BeginRange && num1 <= EndRange)
      return true;
    int num2 = (int) this.ShowMessage(this.e.MsgString[80 /*0x50*/], this.e.MsgString[111], MessageBoxButtons.OK);
    tb.Focus();
    form.DialogResult = DialogResult.None;
    return false;
  }

  private bool ClassCleanupCallback(IntPtr hWnd, IntPtr lparam)
  {
    Control control = Control.FromHandle(hWnd);
    if (control != null && this.GetClassName(hWnd) == this.e.TerClassName)
      control.Dispose();
    return true;
  }

  internal new bool CopyTabw(int FromLine, int ToLine)
  {
    if (this.True(this.e.text[ToLine].tabw))
      this.FreeTabwMembers(ToLine);
    else
      this.AllocTabw(ToLine);
    this.e.text[ToLine].tabw = this.e.text[FromLine].tabw.Copy();
    return true;
  }

  internal new bool DisableIme(bool repaint) => true;

  internal new bool DispMsg(int msg)
  {
    string str = $"Other: {msg:x}";
    switch (msg)
    {
      case 0:
        str = "WM_null";
        break;
      case 1:
        str = "WM_CREATE";
        break;
      case 2:
        str = "WM_DESTROY";
        break;
      case 3:
        str = "WM_MOVE";
        break;
      case 5:
        str = "WM_SIZE";
        break;
      case 6:
        str = "WM_ACTIVATE";
        break;
      case 7:
        str = "WM_SETFOCUS";
        break;
      case 8:
        str = "WM_KILLFOCUS";
        break;
      case 10:
        str = "WM_ENABLE";
        break;
      case 11:
        str = "WM_SETREDRAW";
        break;
      case 12:
        str = "WM_SETTEXT";
        break;
      case 13:
        str = "WM_GETTEXT";
        break;
      case 14:
        str = "WM_GETTEXTLENGTH";
        break;
      case 15:
        str = "WM_PAINT";
        break;
      case 16 /*0x10*/:
        str = "WM_CLOSE";
        break;
      case 17:
        str = "WM_QUERYENDSESSION";
        break;
      case 18:
        str = "WM_QUIT";
        break;
      case 19:
        str = "WM_QUERYOPEN";
        break;
      case 20:
        str = "WM_ERASEBKGND";
        break;
      case 21:
        str = "WM_SYSCOLORCHANGE";
        break;
      case 22:
        str = "WM_ENDSESSION";
        break;
      case 23:
        str = "WM_SYSTEMERROR";
        break;
      case 24:
        str = "WM_SHOWWINDOW";
        break;
      case 25:
        str = "WM_CTLCOLOR";
        break;
      case 26:
        str = "WM_WININICHANGE";
        break;
      case 27:
        str = "WM_DEVMODECHANGE";
        break;
      case 28:
        str = "WM_ACTIVATEAPP";
        break;
      case 29:
        str = "WM_FONTCHANGE";
        break;
      case 30:
        str = "WM_TIMECHANGE";
        break;
      case 31 /*0x1F*/:
        str = "WM_CANCELMODE";
        break;
      case 32 /*0x20*/:
        str = "WM_SETCURSOR";
        break;
      case 33:
        str = "WM_MOUSEACTIVATE";
        break;
      case 34:
        str = "WM_CHILDACTIVATE";
        break;
      case 35:
        str = "WM_QUEUESYNC";
        break;
      case 36:
        str = "WM_GETMINMAXINFO";
        break;
      case 39:
        str = "WM_ICONERASEBKGND";
        break;
      case 40:
        str = "WM_NEXTDLGCTL";
        break;
      case 42:
        str = "WM_SPOOLERSTATUS";
        break;
      case 43:
        str = "WM_DRAWITEM";
        break;
      case 44:
        str = "WM_MEASUREITEM";
        break;
      case 45:
        str = "WM_DELETEITEM";
        break;
      case 46:
        str = "WM_VKEYTOITEM";
        break;
      case 47:
        str = "WM_CHARTOITEM";
        break;
      case 48 /*0x30*/:
        str = "WM_SETFONT";
        break;
      case 49:
        str = "WM_GETFONT";
        break;
      case 55:
        str = "WM_QUERYDRAGICON";
        break;
      case 57:
        str = "WM_COMPAREITEM";
        break;
      case 65:
        str = "WM_COMPACTING";
        break;
      case 68:
        str = "WM_COMMNOTIFY";
        break;
      case 70:
        str = "WM_WINDOWPOSCHANGING";
        break;
      case 71:
        str = "WM_WINDOWPOSCHANGED";
        break;
      case 72:
        str = "WM_POWER";
        break;
      case 129:
        str = "WM_NCCREATE";
        break;
      case 130:
        str = "WM_NCDESTROY";
        break;
      case 131:
        str = "WM_NCCALCSIZE";
        break;
      case 132:
        str = "WM_NCHITTEST";
        break;
      case 133:
        str = "WM_NCPAINT";
        break;
      case 134:
        str = "WM_NCACTIVATE";
        break;
      case 135:
        str = "WM_GETDLGCODE";
        break;
      case 160 /*0xA0*/:
        str = "WM_NCMOUSEMOVE";
        break;
      case 161:
        str = "WM_NCLBUTTONDOWN";
        break;
      case 162:
        str = "WM_NCLBUTTONUP";
        break;
      case 163:
        str = "WM_NCLBUTTONDBLCLK";
        break;
      case 164:
        str = "WM_NCRBUTTONDOWN";
        break;
      case 165:
        str = "WM_NCRBUTTONUP";
        break;
      case 166:
        str = "WM_NCRBUTTONDBLCLK";
        break;
      case 167:
        str = "WM_NCMBUTTONDOWN";
        break;
      case 168:
        str = "WM_NCMBUTTONUP";
        break;
      case 169:
        str = "WM_NCMBUTTONDBLCLK";
        break;
      case 256 /*0x0100*/:
        str = "WM_KEYDOWN";
        break;
      case 257:
        str = "WM_KEYUP";
        break;
      case 258:
        str = "WM_CHAR";
        break;
      case 259:
        str = "WM_DEADCHAR";
        break;
      case 260:
        str = "WM_SYSKEYDOWN";
        break;
      case 261:
        str = "WM_SYSKEYUP";
        break;
      case 262:
        str = "WM_SYSCHAR";
        break;
      case 263:
        str = "WM_SYSDEADCHAR";
        break;
      case 269:
        str = "WM_IME_STARTCOMPOSITION";
        break;
      case 270:
        str = "WM_IME_ENDCOMPOSITION";
        break;
      case 271:
        str = "WM_IME_COMPOSITION";
        break;
      case 272:
        str = "WM_INITDIALOG";
        break;
      case 273:
        str = "WM_COMMAND";
        break;
      case 274:
        str = "WM_SYSCOMMAND";
        break;
      case 275:
        str = "WM_TIMER";
        break;
      case 276:
        str = "WM_HSCROLL";
        break;
      case 277:
        str = "WM_VSCROLL";
        break;
      case 278:
        str = "WM_INITMENU";
        break;
      case 279:
        str = "WM_INITMENUPOPUP";
        break;
      case 287:
        str = "WM_MENUSELECT";
        break;
      case 288:
        str = "WM_MENUCHAR";
        break;
      case 289:
        str = "WM_ENTERIDLE";
        break;
      case 512 /*0x0200*/:
        str = "WM_MOUSEMOVE";
        break;
      case 513:
        str = "WM_LBUTTONDOWN";
        break;
      case 514:
        str = "WM_LBUTTONUP";
        break;
      case 515:
        str = "WM_LBUTTONDBLCLK";
        break;
      case 516:
        str = "WM_RBUTTONDOWN";
        break;
      case 517:
        str = "WM_RBUTTONUP";
        break;
      case 518:
        str = "WM_RBUTTONDBLCLK";
        break;
      case 519:
        str = "WM_MBUTTONDOWN";
        break;
      case 520:
        str = "WM_MBUTTONUP";
        break;
      case 521:
        str = "WM_MBUTTONDBLCLK";
        break;
      case 528:
        str = "WM_PARENTNOTIFY";
        break;
      case 544:
        str = "WM_MDICREATE";
        break;
      case 545:
        str = "WM_MDIDESTROY";
        break;
      case 546:
        str = "WM_MDIACTIVATE";
        break;
      case 547:
        str = "WM_MDIRESTORE";
        break;
      case 548:
        str = "WM_MDINEXT";
        break;
      case 549:
        str = "WM_MDIMAXIMIZE";
        break;
      case 550:
        str = "WM_MDITILE";
        break;
      case 551:
        str = "WM_MDICASCADE";
        break;
      case 552:
        str = "WM_MDIICONARRANGE";
        break;
      case 553:
        str = "WM_MDIGETACTIVE";
        break;
      case 560:
        str = "WM_MDISETMENU";
        break;
      case 563:
        str = "WM_DROPFILES";
        break;
      case 642:
        str = "WM_IME_NOTIFY";
        break;
      case 644:
        str = "WM_IME_COMPOSITIONFULL";
        break;
      case 646:
        str = "WM_IME_CHAR";
        break;
      case 768 /*0x0300*/:
        str = "WM_CUT";
        break;
      case 769:
        str = "WM_COPY";
        break;
      case 770:
        str = "WM_PASTE";
        break;
      case 771:
        str = "WM_CLEAR";
        break;
      case 772:
        str = "WM_UNDO";
        break;
      case 773:
        str = "WM_RENDERFORMAT";
        break;
      case 774:
        str = "WM_RENDERALLFORMATS";
        break;
      case 775:
        str = "WM_DESTROYCLIPBOARD";
        break;
      case 776:
        str = "WM_DRAWCLIPBOARD";
        break;
      case 777:
        str = "WM_PAINTCLIPBOARD";
        break;
      case 778:
        str = "WM_VSCROLLCLIPBOARD";
        break;
      case 779:
        str = "WM_SIZECLIPBOARD";
        break;
      case 780:
        str = "WM_ASKCBFORMATNAME";
        break;
      case 781:
        str = "WM_CHANGECBCHAIN";
        break;
      case 782:
        str = "WM_HSCROLLCLIPBOARD";
        break;
      case 783:
        str = "WM_QUERYNEWPALETTE";
        break;
      case 784:
        str = "WM_PALETTEISCHANGING";
        break;
      case 785:
        str = "WM_PALETTECHANGED";
        break;
      case 896:
        str = "WM_PENWINFIRST";
        break;
      case 911:
        str = "WM_PENWINLAST";
        break;
      case 912:
        str = "WM_COALESCE_FIRST";
        break;
      case 927:
        str = "WM_COALESCE_LAST";
        break;
      case 1024 /*0x0400*/:
        str = "WM_USER";
        break;
    }
    this.OurPrintf(new object[2]
    {
      (object) str,
      (object) $"hWnd: {this.e.hTerWnd:x}"
    });
    return true;
  }

  internal new bool DisposeBkPictBM()
  {
    if (this.e.hBkPictDC != IntPtr.Zero)
      this.SelectObject(this.e.hBkPictDC, this.e.hPrevBkPictBM);
    this.DeleteObject(this.e.hBkPictBM);
    if (this.e.BkPictGr != null && this.e.BufBM != null)
    {
      this.e.BkPictGr.ReleaseHdc(this.e.hBkPictDC);
      this.e.BkPictGr.Dispose();
    }
    if (this.e.BkPictBM != null)
      this.e.BkPictBM.Dispose();
    this.e.hBkPictDC = IntPtr.Zero;
    this.e.hBkPictBM = IntPtr.Zero;
    this.e.hPrevBkPictBM = IntPtr.Zero;
    this.e.BkPictGr = (Graphics) null;
    this.e.BkPictBM = (Bitmap) null;
    return true;
  }

  internal new Color DlgEditColor(Control parent, Color InColor, bool FullOpen)
  {
    ColorDialog colorDialog = new ColorDialog();
    int[] numArray = new int[16 /*0x10*/];
    this.e.DlgCancel = true;
    float num1 = (float) byte.MaxValue / 16f;
    for (int index = 0; index < 16 /*0x10*/; ++index)
    {
      int num2 = (int) (byte) ((double) (index + 1) * (double) num1);
      numArray[index] = (num2 << 16 /*0x10*/) + (num2 << 8) + num2;
    }
    colorDialog.Color = InColor;
    if (colorDialog.Color == tc.CLR_AUTO)
      colorDialog.Color = tc.CLR_WHITE;
    colorDialog.CustomColors = numArray;
    colorDialog.AllowFullOpen = FullOpen;
    int num3 = DialogResult.OK == colorDialog.ShowDialog() ? 1 : 0;
    parent.Focus();
    if (num3 == 0)
      return InColor;
    this.e.DlgCancel = false;
    return colorDialog.Color;
  }

  internal new bool DlgEditFont()
  {
    FontDialog fontDialog = new FontDialog();
    FontStyle style = FontStyle.Regular;
    if (this.ctl.True(this.e.DlgInt4 & 2))
      style |= FontStyle.Bold;
    if (this.ctl.True(this.e.DlgInt4 & 4))
      style |= FontStyle.Italic;
    if (this.ctl.True(this.e.DlgInt4 & 1))
      style |= FontStyle.Underline;
    if (this.ctl.True(this.e.DlgInt4 & 8))
      style |= FontStyle.Strikeout;
    Font font1 = new Font(this.e.DlgTypeface, (float) this.e.DlgInt3 / 20f, style);
    fontDialog.Font = font1;
    fontDialog.Color = this.e.DlgColor1;
    fontDialog.FontMustExist = true;
    fontDialog.ShowColor = true;
    if (fontDialog.ShowDialog() == DialogResult.Cancel)
      return false;
    Font font2 = fontDialog.Font;
    int num1 = (int) ((double) font2.SizeInPoints * 20.0);
    int num2 = 0;
    if (font2.Bold)
      num2 |= 2;
    if (font2.Italic)
      num2 |= 4;
    if (font2.Underline)
      num2 |= 1;
    if (font2.Strikeout)
      num2 |= 8;
    this.e.DlgInt4 = num2;
    this.e.DlgInt3 = num1;
    this.e.DlgTypeface = font2.Name;
    this.e.DlgColor1 = fontDialog.Color;
    font2.Dispose();
    return true;
  }

  internal new bool dm(string DebugMsg)
  {
    if (tc.DebugMode)
      this.OurPrintf(new object[1]{ (object) DebugMsg });
    return true;
  }

  internal new bool DoPopupSelection(int CmdId)
  {
    return CmdId < 2500 || CmdId > 2511 || this.SpellCheckCurWordPart2(CmdId);
  }

  internal new bool DoRulerClick(MouseButtons button, int lParam)
  {
    if (this.e.TerArg.WordWrap)
    {
      if (this.e.CurSID >= 0 && !this.e.EditingParaStyle)
      {
        this.MessageBeep(0);
        return true;
      }
      int x1 = (int) (short) COp.LOWORD(lParam) - this.e.TerWinRect.left + this.e.TerWinOrgX;
      int frame = this.frm.GetFrame(this.e.CurLine);
      int x2 = (this.e.text[this.e.CurLine].flags2 & 32 /*0x20*/) == 0 ? x1 - (this.e.frame[frame].x + this.e.frame[frame].SpaceLeft) : this.RtlX(x1, 0, this.e.CurFrame, new tc.StrLineSeg());
      if (x2 < 0)
        x2 = 0;
      int num = this.ScrToTwipsX(x2);
      if (this.e.SnapToGrid)
        num = (this.e.TerFlags & 2) == 0 ? this.RoundInt(num, 90) : this.RoundInt(num, 71);
      int type = ((int) this.GetKeyState(16 /*0x10*/) & 32768 /*0x8000*/) == 0 ? (button != MouseButtons.Left ? 1 : this.e.DefTabType) : (button != MouseButtons.Left ? 3 : 2);
      int tabId = this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].TabId;
      int index = 0;
      while (index < this.e.TerTab[tabId].count && Math.Abs(this.e.TerTab[tabId].pos[index] - num) >= 45)
        ++index;
      if (index < this.e.TerTab[tabId].count || num <= 0)
        this.e.ClearTab(this.e.TerTab[tabId].pos[index], true);
      else
        this.e.SetTab(type, num, true);
    }
    return true;
  }

  internal new bool ExpandRowArrays(int idx)
  {
    int maxLinesPerWin = this.e.MaxLinesPerWin;
    this.e.MaxLinesPerWin += 50;
    if (this.e.MaxLinesPerWin <= idx)
      this.e.MaxLinesPerWin = idx + 50;
    this.e.RowX = this.ReAlloc(this.e.RowX, this.e.MaxLinesPerWin);
    this.e.RowY = this.ReAlloc(this.e.RowY, this.e.MaxLinesPerWin);
    this.e.RowHeight = this.ReAlloc(this.e.RowHeight, this.e.MaxLinesPerWin);
    for (int index = maxLinesPerWin; index < this.e.MaxLinesPerWin; ++index)
    {
      int num1;
      this.e.RowHeight[index] = num1 = 0;
      int num2;
      this.e.RowY[index] = num2 = num1;
      this.e.RowX[index] = num2;
    }
    return true;
  }

  internal new string ExtractQuotedText(string str)
  {
    int num;
    if ((num = str.IndexOf('"')) >= 0)
    {
      str = str.Substring(num + 1);
      int length;
      if ((length = str.IndexOf('"')) >= 0)
        str = str.Substring(0, length);
    }
    return str;
  }

  internal new byte[] FileToByteArray(string FileName, out int size)
  {
    byte[] byteArray = (byte[]) null;
    size = 0;
    if (FileName.Length > 0 && (!File.Exists(FileName) || File.GetAttributes(FileName) != FileAttributes.Normal && (File.GetAttributes(FileName) & (FileAttributes.ReadOnly | FileAttributes.Archive)) == (FileAttributes) 0))
      return byteArray;
    FileStream fileStream;
    try
    {
      fileStream = new FileStream(FileName, FileMode.Open, FileAccess.Read);
    }
    catch (Exception ex)
    {
      return byteArray;
    }
    size = (int) fileStream.Length;
    byte[] buffer = new byte[size];
    fileStream.Read(buffer, 0, size);
    fileStream.Close();
    return buffer;
  }

  internal new int FillFontBox(ComboBox cb)
  {
    using (InstalledFontCollection installedFontCollection = new InstalledFontCollection())
    {
      FontFamily[] families = installedFontCollection.Families;
      cb.Items.Clear();
      foreach (FontFamily fontFamily in families)
        cb.Items.Add((object) fontFamily.Name);
      return families.Length;
    }
  }

  internal new bool FillPointBox(ComboBox cb)
  {
    int[] numArray = new int[16 /*0x10*/]
    {
      4,
      6,
      7,
      8,
      9,
      10,
      11,
      12,
      14,
      16 /*0x10*/,
      18,
      20,
      24,
      30,
      36,
      72
    };
    cb.Items.Clear();
    foreach (int num in numArray)
    {
      string str = $"{num,2}";
      cb.Items.Add((object) str);
    }
    return true;
  }

  internal new bool FreeHtmlAddOn()
  {
    tc.hHtn = (Assembly) null;
    tc.HtnType = (System.Type) null;
    tc.StSearched = false;
    return true;
  }

  internal new bool FreeTabw(int line)
  {
    if (this.e.text[line].tabw != null)
    {
      this.e.text[line].tabw.CharFlags = (byte[]) null;
      this.e.text[line].tabw.ListText = (string) null;
      this.e.text[line].tabw.pListnum = (tc.StrListnum[]) null;
      this.e.text[line].tabw = (tc.ClsTabw) null;
    }
    return true;
  }

  internal new bool FreeTabwMembers(int line)
  {
    if (this.e.text[line].tabw != null)
    {
      this.e.text[line].tabw.CharFlags = (byte[]) null;
      this.e.text[line].tabw.ListText = (string) null;
      this.e.text[line].tabw.pListnum = (tc.StrListnum[]) null;
    }
    return true;
  }

  internal new uint GetBitVal(uint val, int off, int bits)
  {
    val <<= 31 /*0x1F*/ - off;
    val >>= 32 /*0x20*/ - bits;
    return val;
  }

  internal new bool GetDateString(string DateFmt, out string DateString, int FieldFont)
  {
    int culture = FieldFont < 0 ? (FieldFont != -2 ? InputLanguage.CurrentInputLanguage.Culture.LCID : 1033) : this.e.TerFont[FieldFont].lang;
    if (culture == 0)
      culture = InputLanguage.CurrentInputLanguage.Culture.LCID;
    int length1 = DateFmt.IndexOf("am/pm");
    int length2 = DateFmt.Length;
    if (length1 >= 0)
      DateFmt = $"{DateFmt.Substring(0, length1)}tt{DateFmt.Substring(length1 + 5, length2 - (length1 + 5))}";
    DateString = " ";
    DateTime now = DateTime.Now;
    CultureInfo cultureInfo = new CultureInfo(culture);
    DateString = now.ToString(DateFmt, (IFormatProvider) cultureInfo.DateTimeFormat);
    return true;
  }

  internal new Color GetShadedColor(Color FcColor, Color BcColor, int shade)
  {
    return this.ToColor(this.GetShadedColorComp((int) FcColor.R, (int) BcColor.R, shade), this.GetShadedColorComp((int) FcColor.G, (int) BcColor.G, shade), this.GetShadedColorComp((int) FcColor.B, (int) BcColor.B, shade));
  }

  internal int GetShadedColorComp(int FcColor, int BcColor, int shade)
  {
    if (FcColor == BcColor)
      return FcColor;
    int x = BcColor - FcColor;
    return BcColor - this.MulDiv(x, shade, 100);
  }

  internal new string GetStringField(string InStr, int nbr, char delim)
  {
    if (InStr == null)
      return "";
    char[] chArray = new char[InStr.Length + 1];
    int length = InStr.Length;
    int num1;
    for (num1 = 0; num1 < length; ++num1)
    {
      if (num1 > 0 && (int) InStr[num1 - 1] == (int) delim)
        --nbr;
      if (nbr == 0)
        break;
    }
    if (num1 == length)
      return "";
    int num2 = num1;
    int index;
    for (index = num2; index < length && (int) InStr[index] != (int) delim; ++index)
      chArray[index - num2] = InStr[index];
    return new string(chArray, 0, index - num2);
  }

  internal bool GetTerFields(out tc.StrTerField fld) => this.GetTerFieldsAlt(out fld, -1);

  internal bool GetTerFieldsAlt(out tc.StrTerField fld, int LineNo)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (LineNo < 0)
      LineNo = this.e.CurLine;
    fld.hTerWnd = this.e.hTerWnd;
    fld.TerGr = this.e.TerGr;
    fld.TerRect = this.ToRectangle(this.e.TerRect);
    fld.TerWinRect = this.ToRectangle(this.e.TerWinRect);
    fld.TotalLines = this.e.TotalLines;
    fld.TotalPfmts = this.e.TotalPfmts;
    fld.TotalFonts = this.e.TotalFonts;
    fld.TotalStyles = this.e.TotalSID;
    fld.WinWidth = this.e.WinWidth;
    fld.WinHeight = this.e.WinHeight;
    fld.TerWinOrgX = this.e.TerWinOrgX;
    fld.MouseCol = this.e.MouseCol;
    fld.MouseLine = this.e.MouseLine;
    fld.MaxColBlock = this.e.MaxColBlock;
    fld.modified = this.True(this.e.TerArg.modified);
    fld.WordWrap = this.e.TerArg.WordWrap;
    int pfmt = this.e.text[LineNo].pfmt;
    fld.ParaLeftIndent = this.e.PfmtId[pfmt].LeftIndentTwips;
    fld.ParaRightIndent = this.e.PfmtId[pfmt].RightIndentTwips;
    fld.ParaFirstIndent = this.e.PfmtId[pfmt].FirstIndentTwips;
    fld.ParaFlags = this.e.PfmtId[pfmt].flags;
    fld.pflags = this.e.PfmtId[pfmt].pflags;
    fld.ParaTabId = this.e.PfmtId[pfmt].TabId;
    fld.ParaCellId = this.e.text[LineNo].cid;
    fld.ParaFrameId = this.e.text[LineNo].fid;
    fld.ParaShading = this.e.PfmtId[pfmt].shading;
    fld.ParaSpaceBefore = this.e.PfmtId[pfmt].SpaceBefore;
    fld.ParaSpaceAfter = this.e.PfmtId[pfmt].SpaceAfter;
    fld.ParaSpaceBetween = this.e.PfmtId[pfmt].SpaceBetween;
    fld.ParaStyleId = this.e.PfmtId[pfmt].StyId;
    fld.ParaAuxId = this.e.PfmtId[pfmt].AuxId;
    int section;
    fld.CurSect = section = this.GetSection(LineNo);
    fld.LeftMargin = (int) this.InchesToTwips((double) this.e.TerSect[section].LeftMargin);
    fld.RightMargin = (int) this.InchesToTwips((double) this.e.TerSect[section].RightMargin);
    fld.TopMargin = (int) this.InchesToTwips((double) this.e.TerSect[section].TopMargin);
    fld.BotMargin = (int) this.InchesToTwips((double) this.e.TerSect[section].BotMargin);
    fld.columns = this.e.TerSect[section].columns;
    fld.CurPage = this.e.CurPage;
    fld.TotalPages = this.e.TotalPages;
    fld.MouseX = this.e.MouseX;
    fld.MouseY = this.e.MouseY;
    fld.PrintView = this.e.TerArg.PrintView;
    fld.PageMode = this.e.TerArg.PageMode;
    fld.FittedView = this.e.TerArg.FittedView;
    fld.ShowParaMark = this.e.ShowParaMark;
    fld.ShowHiddenText = this.e.ShowHiddenText;
    fld.CurCtlId = this.e.CurCtlId;
    fld.ParaFrameFlags = this.e.ParaFrame[fld.ParaFrameId].flags;
    fld.CurRow = this.e.CurRow;
    fld.CurCol = this.e.CurCol;
    fld.CurLine = this.e.CurLine;
    fld.BeginLine = this.e.BeginLine;
    fld.PaintEnabled = this.e.PaintEnabled;
    fld.WrapFlag = this.e.WrapFlag;
    fld.ReclaimResources = this.e.ReclaimResources;
    if (!this.e.TerArg.WordWrap)
      fld.WrapFlag = 0;
    fld.TextBkColor = this.e.TextDefBkColor;
    fld.StatusBkColor = this.e.StatusBkColor;
    fld.StatusColor = this.e.StatusColor;
    fld.LinkColor = this.e.LinkColor;
    fld.SnapToGrid = this.e.SnapToGrid;
    fld.HtmlMode = this.e.HtmlMode;
    fld.ShowTableGridLines = this.e.ShowTableGridLines;
    fld.ModifyProtectColor = this.e.ModifyProtectColor;
    fld.HilightType = this.e.HilightType;
    fld.HilightBegRow = this.e.HilightBegRow;
    fld.HilightEndRow = this.e.HilightEndRow;
    fld.HilightBegCol = this.e.HilightBegCol;
    fld.HilightEndCol = this.e.HilightEndCol;
    fld.StretchHilight = this.e.StretchHilight;
    fld.LinkStyle = this.e.LinkStyle;
    fld.LinkDblClick = this.e.LinkDblClick;
    fld.ShowProtectCaret = this.e.ShowProtectCaret;
    fld.LineLen = this.e.text[LineNo].len;
    char[] txt = this.e.text[LineNo].txt;
    fld.text = new char[fld.LineLen];
    for (int index = 0; index < this.e.text[LineNo].len; ++index)
      fld.text[index] = txt[index];
    ushort[] numArray = this.OpenCfmt(LineNo);
    fld.font = new ushort[fld.LineLen];
    for (int index = 0; index < this.e.text[LineNo].len; ++index)
      fld.font[index] = numArray[index];
    this.CloseCfmt(LineNo);
    fld.pfmt = this.e.text[LineNo].pfmt;
    fld.TextApply = 0;
    return true;
  }

  internal new int GetTypeField(System.Type type, string name)
  {
    return (int) type.GetField(name).GetValue((object) null);
  }

  internal new bool HtmlAddOnFound() => this.LoadHtmlAddOn();

  internal new int HtmlListLevel(int ParaId)
  {
    int num = 7;
    return this.e.PfmtId[ParaId].AuxId & num;
  }

  internal new bool HtsReadFromTer(bool FromFile, string FileName, string buf)
  {
    bool flag = false;
    if (!this.LoadHtmlAddOn())
      return false;
    if (this.e.htn == null)
    {
      if (!this.TerSetHtnObject((object) null))
        return false;
      flag = true;
    }
    object[] parameters = new object[4]
    {
      (object) FromFile,
      (object) FileName,
      (object) buf,
      (object) tc.HtnLicenseKey
    };
    int num = (bool) tc.pHtsReadFromTer.Invoke(this.e.htn, parameters) ? 1 : 0;
    if (!flag)
      return num != 0;
    this.e.htn = (object) null;
    return num != 0;
  }

  internal new bool HtsSaveFromTer(bool ToFile, string FileName, out string buf)
  {
    bool flag = false;
    buf = "";
    if (!this.LoadHtmlAddOn())
      return false;
    if (this.e.htn == null)
    {
      if (!this.TerSetHtnObject((object) null))
        return false;
      flag = true;
    }
    object[] parameters = new object[4]
    {
      (object) ToFile,
      (object) FileName,
      (object) buf,
      (object) tc.HtnLicenseKey
    };
    int num = (bool) tc.pHtsSaveFromTer.Invoke(this.e.htn, parameters) ? 1 : 0;
    buf = (string) parameters[2];
    if (!flag)
      return num != 0;
    this.e.htn = (object) null;
    return num != 0;
  }

  internal new bool IsDefLangRtl()
  {
    for (int index = 0; index < this.e.TotalFonts; ++index)
    {
      if (this.e.TerFont[index].InUse && (this.e.TerFont[index].style & 128 /*0x80*/) == 0)
      {
        if (this.e.TerFont[index].lang == this.e.DefLang)
          return this.e.TerFont[index].rtl;
        if (this.e.TerFont[index].lang == 0 && this.e.TerFont[index].rtl)
          return true;
      }
    }
    return false;
  }

  internal new bool IsHtmlList(int ParaId)
  {
    int num1 = 3;
    int num2 = 7;
    return (this.e.PfmtId[ParaId].AuxId >> num1 & num2) != 0;
  }

  internal new bool IsHtmlRule(int ParaId)
  {
    int num1 = 9;
    int num2 = 3;
    int num3 = 7;
    return (this.e.PfmtId[ParaId].AuxId >> num1 & num3) == num2;
  }

  internal new bool LineEndsInBreak(int LineNo)
  {
    return LineNo >= 0 && LineNo < this.e.TotalLines && this.e.text[LineNo].len != 0 && this.IsBreakChar(this.e.text[LineNo].txt[this.e.text[LineNo].len - 1]);
  }

  internal new bool LoadHtmlAddOn()
  {
    if (!tc.HtnSearched)
      this.SearchHtn();
    return tc.HtnType != (System.Type) null;
  }

  internal new bool LogPrintf(params object[] msg)
  {
    string msg1 = "";
    foreach (object obj in msg)
    {
      if (msg1.Length > 0)
        msg1 += " ";
      msg1 = obj == null ? msg1 + "null object" : msg1 + obj.ToString();
    }
    return CMisc.OurPrintf((object) msg1, true);
  }

  internal new bool MessagePending()
  {
    try
    {
      COp.MSG msg;
      if ((this.PeekMessage(out msg, this.e.hTerWnd, 258, 258, 2) && msg.wParam.ToInt32() != 13 && this.e.CommandId != 763 || this.PeekMessage(out msg, this.e.hTerWnd, 256 /*0x0100*/, 256 /*0x0100*/, 2) || this.PeekMessage(out msg, this.e.hTerWnd, 513, 522, 2)) && msg.hwnd == this.e.hTerWnd)
        return true;
      if (this.e.HilightType != 0)
      {
        if (this.e.StretchHilight)
        {
          if (this.PeekMessage(out msg, this.e.hTerWnd, 512 /*0x0200*/, 512 /*0x0200*/, 2))
          {
            if (msg.hwnd == this.e.hTerWnd)
              return true;
          }
        }
      }
    }
    catch (Exception ex)
    {
      return false;
    }
    return false;
  }

  internal new string OrdinalString(int val, bool AddSuffix, bool upper)
  {
    string str = (val <= 999999 ? (val != 0 ? this.OrdinalStringThousands(val, AddSuffix) : "Zero") : val.ToString()).Trim();
    if (upper)
      str = str.ToUpper();
    return str;
  }

  internal string OrdinalString20(int val, bool AddSuffix)
  {
    string str = "";
    if (AddSuffix)
    {
      switch (val)
      {
        case 1:
          return "First";
        case 2:
          return "Second";
        case 3:
          return "Third";
        case 4:
          return "Fourth";
        case 5:
          return "Fifth";
        case 6:
          return "Sixth";
        case 7:
          return "Seventh";
        case 8:
          return "Eighth";
        case 9:
          return "Ninth";
        case 10:
          return "Tenth";
        case 11:
          return "Eleventh";
        case 12:
          return "Twelfth";
        case 13:
          return "Thirteenth";
        case 14:
          return "Fourteenth";
        case 15:
          return "Fifteenth";
        case 16 /*0x10*/:
          return "Sixteenth";
        case 17:
          return "Seventeenth";
        case 18:
          return "Eighteenth";
        case 19:
          return "Nineteenth";
        case 20:
          return "Twentieth";
        default:
          return str;
      }
    }
    else
    {
      switch (val)
      {
        case 1:
          return "One ";
        case 2:
          return "Two ";
        case 3:
          return "Three ";
        case 4:
          return "Four ";
        case 5:
          return "Five ";
        case 6:
          return "Six ";
        case 7:
          return "Seven ";
        case 8:
          return "Eight ";
        case 9:
          return "Nine ";
        case 10:
          return "Ten ";
        case 11:
          return "Eleven ";
        case 12:
          return "Twelve ";
        case 13:
          return "Thirteen ";
        case 14:
          return "Fourteen ";
        case 15:
          return "Fifteen ";
        case 16 /*0x10*/:
          return "Sixteen ";
        case 17:
          return "Seventeen ";
        case 18:
          return "Eighteen ";
        case 19:
          return "Nineteen ";
        case 20:
          return "Twenty ";
        default:
          return str;
      }
    }
  }

  internal string OrdinalString99(int val, bool AddSuffix)
  {
    string str = "";
    if (val <= 20)
      return this.OrdinalString20(val, AddSuffix);
    if (val > 99)
      return str;
    int num = val / 10;
    int val1 = val % 10;
    switch (num)
    {
      case 2:
        str = "Twenty ";
        break;
      case 3:
        str = "Thirty ";
        break;
      case 4:
        str = "Forty ";
        break;
      case 5:
        str = "Fifty ";
        break;
      case 6:
        str = "Sixty ";
        break;
      case 7:
        str = "Seventy ";
        break;
      case 8:
        str = "Eighty ";
        break;
      case 9:
        str = "Ninety ";
        break;
    }
    return str + this.OrdinalString20(val1, AddSuffix);
  }

  internal string OrdinalString999(int val, bool AddSuffix)
  {
    string str1 = "";
    if (val <= 99)
      return this.OrdinalString99(val, AddSuffix);
    if (val <= 999)
    {
      int val1 = val / 100;
      int val2 = val % 100;
      string str2 = this.OrdinalString20(val1, false);
      string str3 = "Hundred ";
      if (val2 == 0 & AddSuffix)
        str3 = "Hundredth";
      str1 = str2 + str3;
      if (val2 > 0)
        str1 = $"{str1}And {this.OrdinalString99(val2, AddSuffix)}";
    }
    return str1;
  }

  internal string OrdinalStringThousands(int val, bool AddSuffix)
  {
    string str1 = "";
    if (val <= 999)
      return this.OrdinalString999(val, AddSuffix);
    if (val <= 999999)
    {
      int val1 = val / 1000;
      int val2 = val % 1000;
      string str2 = this.OrdinalString999(val1, false);
      string str3 = "Thousand ";
      if (val2 == 0 & AddSuffix)
        str3 = "Thousandth";
      str1 = str2 + str3;
      if (val2 > 0)
        str1 = $"{str1}And {this.OrdinalString999(val2, AddSuffix)}";
    }
    return str1;
  }

  internal new Point OurPointToClient(Point p)
  {
    return new Point(this.e.TerWinRect.left + (p.X - this.e.TerWinOrgX), this.e.TerWinRect.top + (p.Y - this.e.TerWinOrgY));
  }

  internal new bool OurPrintf(params object[] msg)
  {
    string msg1 = "";
    foreach (object obj in msg)
    {
      if (msg1.Length > 0)
        msg1 += " ";
      msg1 = obj == null ? msg1 + "null object" : msg1 + obj.ToString();
    }
    return CMisc.OurPrintf((object) msg1, false);
  }

  private static bool OurPrintf(object msg, bool LogIt)
  {
    string path = "c:\\temp\\ter.log";
    string str1 = msg.ToString();
    if (LogIt)
    {
      StreamWriter streamWriter;
      try
      {
        streamWriter = new StreamWriter(path, true, Encoding.ASCII);
      }
      catch (Exception ex)
      {
        return false;
      }
      string str2 = DateTime.Now.ToString();
      streamWriter.Write(str2);
      streamWriter.Write(" --> ");
      streamWriter.WriteLine(str1);
      streamWriter.Close();
      return true;
    }
    IntPtr window1;
    if (IntPtr.Zero != (window1 = COp.Win32.FindWindow("DBWin", (string) null)))
    {
      IntPtr window2;
      if (IntPtr.Zero != (window2 = COp.Win32.GetWindow(window1, 5)))
      {
        bool flag = false;
        int length = str1.Length;
        int index = 0;
        while (index < length && str1[index] >= ' ')
          ++index;
        if (index < 32 /*0x20*/)
          flag = true;
        if (!flag)
          str1 += "\r\n";
        COp.Win32.SendMessage(window2, 177, (IntPtr) (int) short.MaxValue, (IntPtr) (int) short.MaxValue);
        COp.Win32.SendMessage(window2, 194, IntPtr.Zero, str1);
        COp.Win32.SendMessage(window2, 183, IntPtr.Zero, IntPtr.Zero);
        if (flag)
        {
          string lParam = "\r\n";
          COp.Win32.SendMessage(window2, 177, (IntPtr) (int) short.MaxValue, (IntPtr) (int) short.MaxValue);
          COp.Win32.SendMessage(window2, 194, IntPtr.Zero, lParam);
          COp.Win32.SendMessage(window2, 183, IntPtr.Zero, IntPtr.Zero);
        }
      }
    }
    else
    {
      int num = (int) MessageBox.Show(str1, "", MessageBoxButtons.OK);
    }
    return true;
  }

  internal new int ParamIdToSID(int id)
  {
    int sid;
    switch (id)
    {
      case -9999:
        sid = 0;
        break;
      case -9998:
        sid = this.e.CurSID;
        break;
      default:
        sid = -id;
        break;
    }
    if (sid >= this.e.TotalSID)
      sid = -1;
    return sid;
  }

  internal bool ParseUserString(string src, out string dest)
  {
    int length = src.Length;
    dest = "";
    if (length != 0)
    {
      for (int index = 0; index < length; ++index)
      {
        if (src[index] == '^' && index < length - 1)
        {
          if (src[index + 1] == 'p')
            dest += new string(this.e.ParaChar, 1);
          else if (src[index + 1] == 't')
            dest += new string('\t', 1);
          else if (src[index + 1] == '^')
            dest += new string('^', 1);
          else if (src[index + 1] == '+')
            dest += new string('\u0097', 1);
          else if (src[index + 1] == '-')
            dest += new string('\u0096', 1);
          else if (src[index + 1] == 'm')
            dest += new string('\f', 1);
          else if (src[index + 1] == 'b')
          {
            dest += new string('\u0014', 1);
          }
          else
          {
            dest += new string(src[index], 1);
            continue;
          }
          ++index;
        }
        else if (src[index] == '\r' && index + 1 < length && src[index + 1] == '\n')
        {
          dest += new string(this.e.ParaChar, 1);
          ++index;
        }
        else
          dest += new string(src[index], 1);
      }
    }
    return true;
  }

  internal new bool PrepForObject()
  {
    if (this.e.CurLine > 0 && this.e.text[this.e.CurLine].len > 0 && this.e.text[this.e.CurLine - 1].len > 0)
    {
      int curCfmt1 = this.GetCurCfmt(this.e.CurLine, 0);
      int curCfmt2 = this.GetCurCfmt(this.e.CurLine - 1, this.e.text[this.e.CurLine - 1].len - 1);
      if ((this.e.TerFont[curCfmt1].style & 512 /*0x0200*/) != 0 && (this.e.TerFont[curCfmt2].style & 512 /*0x0200*/) != 0)
      {
        this.MessageBeep(0);
        return false;
      }
    }
    if (this.e.CurCol > 0 && this.SplitLine(this.e.CurLine, this.e.CurCol, 0))
    {
      ++this.e.CurLine;
      this.e.CurCol = 0;
    }
    int len;
    if (this.e.CurLine > 0 && (len = this.e.text[this.e.CurLine - 1].len) > 0 && (this.e.text[this.e.CurLine - 1].flags & 1966080 /*0x1E0000*/) == 0)
    {
      char[] txt1 = this.e.text[this.e.CurLine - 1].txt;
      if ((int) txt1[len - 1] != (int) this.e.ParaChar && (int) txt1[len - 1] != (int) this.e.CellChar && txt1[len - 1] != '\u0012' && txt1[len - 1] != '\u0014' && txt1[len - 1] != '\u0016' && txt1[len - 1] != '\f')
      {
        this.LineAlloc(this.e.CurLine - 1, len, len + 1);
        char[] txt2 = this.e.text[this.e.CurLine - 1].txt;
        ushort[] numArray = this.OpenCfmt(this.e.CurLine - 1);
        txt2[len] = this.e.ParaChar;
        numArray[len] = numArray[len - 1];
        if ((this.e.TerFont[(int) numArray[len]].style & 128 /*0x80*/) != 0)
          numArray[len] = (ushort) 0;
        this.SaveUndo(this.e.CurLine - 1, len, this.e.CurLine - 1, len, 'O');
      }
    }
    return true;
  }

  internal new bool ReplaceTextString(string replace, int StartPos, int EndPos)
  {
    int undoRef = this.e.UndoRef;
    int row1;
    int col1;
    this.AbsToRowCol(StartPos, out row1, out col1);
    StartPos = this.RowColToAbs(row1, col1);
    ++EndPos;
    int row2;
    int col2;
    this.AbsToRowCol(EndPos, out row2, out col2);
    EndPos = this.RowColToAbs(row2, col2);
    int num1 = EndPos - StartPos;
    if (num1 < 0)
      return false;
    char[] charArray = replace.ToCharArray();
    int length = replace.Length;
    int num2 = num1 >= length ? length : num1;
    int row3;
    int col3;
    this.AbsToRowCol(StartPos + num2, out row3, out col3);
    int idx = 0;
    ++this.e.TerArg.modified;
    if (num2 > 0)
    {
      this.SaveUndo(row1, col1, row3, col3, 'D');
      for (int line = row1; line <= row3; ++line)
      {
        if (this.e.text[line].len != 0)
        {
          int num3 = 0;
          int num4 = this.e.text[line].len;
          if (line == row1)
            num3 = col1;
          if (line == row3)
            num4 = col3;
          char[] txt = this.e.text[line].txt;
          this.OpenCfmt(line);
          int index = num3;
          while (index < num4)
          {
            if (idx < length)
              txt[index] = charArray[idx];
            ++index;
            ++idx;
          }
          this.CloseCfmt(line);
        }
      }
      this.e.UndoRef = undoRef;
      this.SaveUndo(row1, col1, row3, col3, 'I');
    }
    if (num1 != length)
    {
      if (num1 > length)
      {
        this.e.HilightType = 2;
        this.e.HilightBegRow = row3;
        this.e.HilightBegCol = col3;
        this.e.HilightEndRow = row2;
        this.e.HilightEndCol = col2;
        this.e.UndoRef = undoRef;
        int terOpFlags2 = this.e.TerOpFlags2;
        this.e.TerOpFlags2 |= 4096 /*0x1000*/;
        this.DeleteCharBlock(false, false);
        this.e.TerOpFlags2 = terOpFlags2;
        this.e.HilightType = 0;
      }
      else
      {
        int curLine = this.e.CurLine;
        int curRow = this.e.CurRow;
        int curCol = this.e.CurCol;
        int beginLine = this.e.BeginLine;
        int inputFontId = this.e.InputFontId;
        this.e.CurLine = row3;
        this.e.CurCol = col3;
        this.e.InputFontId = this.GetPrevCfmt(row3, col3);
        this.e.UndoRef = undoRef;
        this.InsertBuffer(this.CopyArray(charArray, idx), (ushort[]) null, (int[]) null, false);
        this.e.CurLine = curLine;
        this.e.CurRow = curRow;
        this.e.CurCol = curCol;
        this.e.InputFontId = inputFontId;
        this.e.BeginLine = beginLine;
      }
    }
    return true;
  }

  internal new string romanize(int val, bool upper)
  {
    string str = "";
    int num1 = 1;
    while (val > 0)
    {
      int num2 = val / 10;
      int num3 = val - 10 * num2;
      val = num2;
      switch (num1)
      {
        case 1:
          switch (num3)
          {
            case 1:
              str = "I";
              break;
            case 2:
              str = "II";
              break;
            case 3:
              str = "III";
              break;
            case 4:
              str = "IV";
              break;
            case 5:
              str = "V";
              break;
            case 6:
              str = "VI";
              break;
            case 7:
              str = "VII";
              break;
            case 8:
              str = "VIII";
              break;
            case 9:
              str = "IX";
              break;
          }
          break;
        case 2:
          switch (num3)
          {
            case 1:
              str = "X" + str;
              break;
            case 2:
              str = "XX" + str;
              break;
            case 3:
              str = "XXX" + str;
              break;
            case 4:
              str = "XL" + str;
              break;
            case 5:
              str = "L" + str;
              break;
            case 6:
              str = "LX" + str;
              break;
            case 7:
              str = "LXX" + str;
              break;
            case 8:
              str = "LXXX" + str;
              break;
            case 9:
              str = "XC" + str;
              break;
          }
          break;
        case 3:
          switch (num3)
          {
            case 1:
              str = "C" + str;
              break;
            case 2:
              str = "CC" + str;
              break;
            case 3:
              str = "CCC" + str;
              break;
            case 4:
              str = "CD" + str;
              break;
            case 5:
              str = "D" + str;
              break;
            case 6:
              str = "DC" + str;
              break;
            case 7:
              str = "DCC" + str;
              break;
            case 8:
              str = "DCCC" + str;
              break;
            case 9:
              str = "CM" + str;
              break;
          }
          break;
        case 4:
          while (num3-- > 0)
            str = "M" + str;
          break;
        default:
          str = "!" + str;
          break;
      }
      ++num1;
    }
    if (!upper)
      str = str.ToLower();
    return str;
  }

  internal new bool SearchDisplay(
    string text,
    char opt,
    int StartLine,
    int StartCol,
    int EndLine,
    int EndCol)
  {
    bool flag1 = (this.e.SearchFlags & 64 /*0x40*/) != 0;
    bool flag2 = !this.e.ViewPageHdrFtr && (this.e.SearchFlags & 64 /*0x40*/) != 0;
    bool flag3 = false;
    int length;
    if ((length = text.Length) == 0)
      return false;
    if ((this.e.SearchFlags & 16 /*0x10*/) == 0)
      text = text.ToLower();
    string str1 = " .,:<>?;'!@#$%^&*()-+=|\\/~01234567890";
    for (int index = 0; this.e.BreakChars[index] != char.MinValue; ++index)
      str1 += new string(this.e.BreakChars[index], 1);
    string str2 = str1 + new string('\t', 1) + new string(this.e.ParaChar, 1) + new string(this.e.CellChar, 1) + new string('\u000F', 1);
    if (opt == 'E' || opt == 'F' || opt == 'B')
    {
      StartLine = this.e.CurLine;
      StartCol = this.e.CurCol;
    }
    if (opt == 'E')
    {
      StartLine = 0;
      StartCol = 0;
      opt = 'F';
    }
    if (opt == 'F')
    {
      EndLine = this.e.TotalLines - 1;
      EndCol = this.e.text[EndLine].len - 1;
    }
    if (opt == 'B')
    {
      EndLine = 0;
      EndCol = 0;
    }
    if (opt == 'R')
      opt = 'F';
    if (opt == 'S')
      opt = 'B';
    if (StartCol >= this.e.LineWidth)
      StartCol = this.e.LineWidth - 1;
    if (StartCol < 0)
      StartCol = 0;
    if (EndCol >= this.e.LineWidth)
      EndCol = this.e.LineWidth - 1;
    if (EndCol < 0)
      EndCol = 0;
    if (opt == 'F')
    {
      for (int line1 = StartLine; line1 <= EndLine; ++line1)
      {
        if (StartCol >= this.e.text[line1].len)
          StartCol = 0;
        else if (!flag2 || (this.e.PfmtId[this.e.text[line1].pfmt].flags & 12288 /*0x3000*/) == 0)
        {
          int len = this.e.text[line1].len;
          string str3 = new string(this.e.text[line1].txt, 0, len);
          this.e.LineCfmt = new ushort[len + 1];
          this.FarMove(this.OpenCfmt(line1), 0, this.e.LineCfmt, 0, len);
          this.CloseCfmt(line1);
          int num1 = this.e.text[line1].len;
          int line2 = line1 + 1;
          for (int index = this.e.text[line1].len + length; line2 <= EndLine && num1 < index; ++line2)
          {
            if (this.e.text[line2].len > 0)
            {
              int num2 = this.e.text[line2].len;
              if (num1 + num2 > index)
                num2 = index - num1;
              str3 += new string(this.e.text[line2].txt, 0, num2);
              int DestIdx = num1;
              ushort[] src = this.OpenCfmt(line2);
              this.e.LineCfmt = this.ReAlloc(this.e.LineCfmt, this.e.LineCfmt.Length + num2);
              this.FarMove(src, 0, this.e.LineCfmt, DestIdx, num2);
              this.CloseCfmt(line2);
              num1 = str3.Length;
            }
          }
          if (num1 == this.e.text[line1].len + length)
            --num1;
          if ((this.e.SearchFlags & 16 /*0x10*/) == 0)
            str3 = str3.ToLower();
          int num3 = num1 - StartCol;
          int num4 = line1 != EndLine ? num1 - length - StartCol : EndCol - StartCol - length + 1;
          int num5 = str3.IndexOf(text, StartCol) - StartCol;
          if (num5 >= 0 && num5 <= num4)
          {
            if ((this.e.SearchFlags & 32 /*0x20*/) != 0)
            {
              bool flag4 = true;
              if (StartCol + num5 > 0 && str2.IndexOf(str3[StartCol + num5 - 1]) < 0 && this.e.TerFont[(int) this.e.LineCfmt[StartCol + num5 - 1]].FieldId == this.e.TerFont[(int) this.e.LineCfmt[StartCol + num5]].FieldId)
                flag4 = false;
              if (num5 < num4 && str2.IndexOf(str3[StartCol + num5 + length]) < 0 && this.e.TerFont[(int) this.e.LineCfmt[StartCol + num5 + length - 1]].FieldId == this.e.TerFont[(int) this.e.LineCfmt[StartCol + num5 + length]].FieldId)
                flag4 = false;
              if (!flag4)
              {
                if (num5 + length < this.e.text[line1].len)
                {
                  StartCol = StartCol + num5 + length;
                  --line1;
                  continue;
                }
                continue;
              }
            }
            if (flag1)
            {
              int num6 = 0;
              while (num6 < length && !this.edit.HiddenText((int) this.e.LineCfmt[StartCol + num5 + num6]))
                ++num6;
              if (num6 < length)
              {
                while (num6 < length && this.edit.HiddenText((int) this.e.LineCfmt[StartCol + num5 + num6]))
                  ++num6;
                for (StartCol = StartCol + num5 + num6; line1 < this.e.TotalLines && StartCol >= this.e.text[line1].len; ++line1)
                  StartCol -= this.e.text[line1].len;
                --line1;
                continue;
              }
            }
            this.e.CurCol = StartCol + num5;
            this.e.CurLine = line1;
            if ((this.e.SearchFlags & 1) != 0)
            {
              this.e.HilightType = 2;
              this.e.StretchHilight = false;
              this.e.HilightBegRow = this.e.HilightEndRow = this.e.CurLine;
              this.e.HilightBegCol = this.e.CurCol;
              this.e.HilightEndCol = this.e.CurCol + length;
              if (this.e.HilightEndCol > this.e.text[line1].len)
                this.AbsToRowCol(this.RowColToAbs(this.e.CurLine, this.e.CurCol) + length, 'E');
              if ((this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].flags & 12288 /*0x3000*/) != 0 && this.e.ViewPageHdrFtr && !this.e.EditPageHdrFtr && this.e.CaretEngaged)
                this.DisengageCaret();
              this.TerPosLine(this.e.CurLine + 1);
              this.PaintTer();
            }
            return true;
          }
          StartCol = 0;
        }
      }
    }
    if (opt == 'B')
    {
      for (int line3 = StartLine; line3 >= EndLine; --line3)
      {
        if (this.e.text[line3].len != 0 && (!flag2 || (this.e.PfmtId[this.e.text[line3].pfmt].flags & 12288 /*0x3000*/) == 0))
        {
          int len = this.e.text[line3].len;
          string str4 = new string(this.e.text[line3].txt, 0, len);
          this.e.LineCfmt = new ushort[len + 1];
          this.FarMove(this.OpenCfmt(line3), 0, this.e.LineCfmt, 0, len);
          this.CloseCfmt(line3);
          int num7 = this.e.text[line3].len;
          int line4 = line3 + 1;
          for (int index = this.e.text[line3].len + length; line4 <= EndLine && num7 < index; ++line4)
          {
            if (this.e.text[line4].len > 0)
            {
              int num8 = this.e.text[line4].len;
              if (num7 + num8 > index)
                num8 = index - num7;
              str4 += new string(this.e.text[line4].txt, 0, num8);
              int DestIdx = num7;
              ushort[] src = this.OpenCfmt(line4);
              this.e.LineCfmt = this.ReAlloc(this.e.LineCfmt, this.e.LineCfmt.Length + num8);
              this.FarMove(src, 0, this.e.LineCfmt, DestIdx, num8);
              this.CloseCfmt(line4);
              num7 = str4.Length;
            }
          }
          if (num7 == this.e.text[line3].len + length)
            --num7;
          if ((this.e.SearchFlags & 16 /*0x10*/) == 0)
            str4 = str4.ToLower();
          if (line3 != StartLine)
            StartCol = num7;
          if (StartCol > 0)
          {
            int num9 = StartCol;
            int num10 = line3 != EndLine ? 0 : EndCol;
            int index = str4.LastIndexOf(text, num9 - 1);
            if (index >= 0 && index >= num10)
            {
              if ((this.e.SearchFlags & 32 /*0x20*/) != 0)
              {
                bool flag5 = true;
                if (index > num10 && str2.IndexOf(str4[index - 1]) < 0 && this.e.TerFont[(int) this.e.LineCfmt[index - 1]].FieldId == this.e.TerFont[(int) this.e.LineCfmt[index]].FieldId)
                  flag5 = false;
                if (index + length <= StartCol && str2.IndexOf(str4[index + length]) < 0 && this.e.TerFont[(int) this.e.LineCfmt[index + length - 1]].FieldId == this.e.TerFont[(int) this.e.LineCfmt[index + length]].FieldId)
                  flag5 = false;
                if (!flag5)
                  continue;
              }
              this.e.CurCol = index;
              this.e.CurLine = line3;
              if ((this.e.SearchFlags & 1) != 0)
              {
                this.e.HilightType = 2;
                this.e.StretchHilight = false;
                this.e.HilightBegRow = this.e.HilightEndRow = this.e.CurLine;
                this.e.HilightBegCol = this.e.CurCol;
                this.e.HilightEndCol = this.e.CurCol + length;
                if (this.e.HilightEndCol > this.e.text[line3].len)
                  this.AbsToRowCol(this.RowColToAbs(this.e.CurLine, this.e.CurCol) + length, 'E');
                this.TerPosLine(this.e.CurLine + 1);
                this.PaintTer();
              }
              flag3 = true;
              break;
            }
          }
        }
      }
    }
    return flag3;
  }

  internal bool SearchHtn()
  {
    tc.HtnSearched = true;
    tc.HtnType = (System.Type) null;
    if (tc.hHtn == (Assembly) null)
    {
      try
      {
        tc.hHtn = !tc.InIE ? Assembly.LoadFrom("htn.dll") : AppDomain.CurrentDomain.Load("htn");
      }
      catch (Exception ex)
      {
        tc.hHtn = (Assembly) null;
      }
    }
    if (tc.hHtn == (Assembly) null)
    {
      if (tc.InIE)
        return false;
      string directoryName = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
      string assemblyFile = "htn.dll";
      if (directoryName != null && directoryName != "")
        assemblyFile = $"{directoryName}\\{assemblyFile}";
      try
      {
        tc.hHtn = Assembly.LoadFrom(assemblyFile);
      }
      catch (Exception ex)
      {
        tc.hHtn = (Assembly) null;
        return false;
      }
    }
    if (tc.hHtn == (Assembly) null)
      return false;
    foreach (System.Type type in tc.hHtn.GetTypes())
    {
      if (type.Name == "Htn")
      {
        tc.HtnType = type;
        break;
      }
    }
    if (tc.HtnType == (System.Type) null)
    {
      tc.hHtn = (Assembly) null;
      return false;
    }
    try
    {
      tc.pHtsSaveFromTer = tc.HtnType.GetMethod("HtsSaveFromTer");
      tc.pHtsReadFromTer = tc.HtnType.GetMethod("HtsReadFromTer");
    }
    catch (Exception ex)
    {
      tc.HtnType = (System.Type) null;
      tc.hHtn = (Assembly) null;
    }
    return true;
  }

  internal new bool SendActionMessage(int message, int wParam, int lParam)
  {
    if (this.e.SendActionMsg)
    {
      switch (message)
      {
        case -15:
          this.e.SendMessageToParent(2732, 15, wParam, false);
          break;
        case 5:
          this.e.SendMessageToParent(2731, 11, lParam, false);
          break;
        case 7:
          this.e.SendMessageToParent(2731, 12, wParam, false);
          break;
        case 8:
          this.e.SendMessageToParent(2731, 13, wParam, false);
          break;
        case 258:
          this.e.SendMessageToParent(2731, 4, wParam, false);
          break;
        case 273:
          this.e.SendMessageToParent(2731, 1, (int) COp.LOWORD(wParam), false);
          break;
        case 276:
          this.e.SendMessageToParent(2731, 3, (int) COp.LOWORD(wParam), false);
          break;
        case 277:
          this.e.SendMessageToParent(2731, 2, (int) COp.LOWORD(wParam), false);
          break;
        case 512 /*0x0200*/:
          this.e.SendMessageToParent(2731, 14, lParam, false);
          break;
        case 513:
          this.e.SendMessageToParent(2731, 5, lParam, false);
          break;
        case 514:
          this.e.SendMessageToParent(2731, 7, lParam, false);
          break;
        case 515:
          this.e.SendMessageToParent(2731, 9, lParam, false);
          break;
        case 516:
          this.e.SendMessageToParent(2731, 6, lParam, false);
          break;
        case 517:
          this.e.SendMessageToParent(2731, 8, lParam, false);
          break;
        case 518:
          this.e.SendMessageToParent(2731, 10, lParam, false);
          break;
      }
    }
    this.e.SendActionMsg = true;
    return true;
  }

  internal new bool SendPreprocessMessage(int message, int wParam, int lParam)
  {
    if (this.e.InPreprocess)
      return false;
    this.e.InPreprocess = true;
    this.e.SkipCommand = false;
    switch (message)
    {
      case -15:
        this.e.SendMessageToParent(2732, 15, wParam, false);
        break;
      case 5:
        this.e.SendMessageToParent(2732, 11, lParam, false);
        break;
      case 7:
        this.e.SendMessageToParent(2732, 12, wParam, false);
        break;
      case 8:
        this.e.SendMessageToParent(2732, 13, wParam, false);
        break;
      case 258:
        this.e.SendMessageToParent(2732, 4, wParam, false);
        break;
      case 273:
        this.e.SendMessageToParent(2732, 1, (int) COp.LOWORD(wParam), false);
        break;
      case 276:
        this.e.SendMessageToParent(2732, 3, (int) COp.LOWORD(wParam), false);
        break;
      case 277:
        this.e.SendMessageToParent(2732, 2, (int) COp.LOWORD(wParam), false);
        break;
      case 512 /*0x0200*/:
        this.e.SendMessageToParent(2732, 14, lParam, false);
        break;
      case 513:
        this.e.SendMessageToParent(2732, 5, lParam, false);
        break;
      case 514:
        this.e.SendMessageToParent(2732, 7, lParam, false);
        break;
      case 515:
        this.e.SendMessageToParent(2732, 9, lParam, false);
        break;
      case 516:
        this.e.SendMessageToParent(2732, 6, lParam, false);
        break;
      case 517:
        this.e.SendMessageToParent(2732, 8, lParam, false);
        break;
      case 518:
        this.e.SendMessageToParent(2732, 10, lParam, false);
        break;
    }
    this.e.InPreprocess = false;
    return this.e.SkipCommand;
  }

  internal bool SetTerFields(tc.StrTerField fld)
  {
    bool flag = false;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    this.e.PaintFlag = 2;
    this.e.CurRow = fld.CurRow;
    this.e.CurCol = fld.CurCol;
    this.e.CurLine = fld.CurLine;
    this.e.BeginLine = fld.BeginLine;
    if (this.e.CurLine >= this.e.TotalLines)
    {
      this.e.CurLine = this.e.TotalLines - 1;
      this.e.PaintFlag = 4;
    }
    if (this.e.CurLine < 0)
    {
      this.e.CurLine = 0;
      this.e.PaintFlag = 4;
    }
    if (this.e.CurLine - this.e.BeginLine > this.e.WinHeight)
    {
      this.e.BeginLine = this.e.CurLine - this.e.WinHeight;
      this.e.PaintFlag = 4;
    }
    if (this.e.BeginLine < 0)
    {
      this.e.BeginLine = 0;
      this.e.PaintFlag = 4;
    }
    if (this.e.CurRow != this.e.CurLine - this.e.BeginLine)
      this.e.PaintFlag = 4;
    this.e.CurRow = this.e.CurLine - this.e.BeginLine;
    if (this.e.CurCol < 0)
    {
      this.e.CurCol = 0;
      this.e.PaintFlag = 4;
    }
    if (this.e.StatusColor != fld.StatusColor || this.e.TextDefBkColor != fld.TextBkColor || this.e.StatusBkColor != fld.StatusBkColor)
      this.e.PaintFlag = 4;
    if (this.e.StatusColor != fld.StatusColor || this.e.StatusBkColor != fld.StatusBkColor)
    {
      if (this.e.TerArg.ShowStatus)
        flag = true;
      if (this.e.StatusBkColor != fld.StatusBkColor)
      {
        if (this.e.ToolbarBrush != null)
          this.e.ToolbarBrush.Dispose();
        this.e.ToolbarBrush = (Brush) new SolidBrush(fld.StatusBkColor);
        if (this.e.TerTlb != null)
          this.e.TerTlb.Invalidate();
        if (this.e.PvTlb != null)
          this.e.PvTlb.Invalidate();
        this.e.RulerPending = true;
      }
    }
    if (this.e.TextDefBkColor != fld.TextBkColor)
      this.e.TerSetBkColor(fld.TextBkColor);
    this.e.StatusBkColor = fld.StatusBkColor;
    this.e.StatusColor = fld.StatusColor;
    if (flag)
      this.DisplayStatus();
    this.e.LinkColor = fld.LinkColor;
    this.e.SnapToGrid = fld.SnapToGrid;
    this.e.HtmlMode = fld.HtmlMode;
    if (this.e.ShowTableGridLines != fld.ShowTableGridLines)
      this.e.PaintFlag = 4;
    this.e.ShowTableGridLines = fld.ShowTableGridLines;
    this.e.ModifyProtectColor = fld.ModifyProtectColor;
    this.e.HilightType = fld.HilightType;
    this.e.HilightBegRow = fld.HilightBegRow;
    this.e.HilightEndRow = fld.HilightEndRow;
    this.e.HilightBegCol = fld.HilightBegCol;
    this.e.HilightEndCol = fld.HilightEndCol;
    this.e.StretchHilight = fld.StretchHilight;
    this.e.LinkStyle = fld.LinkStyle;
    this.e.LinkDblClick = fld.LinkDblClick;
    this.e.ShowProtectCaret = fld.ShowProtectCaret;
    int index1 = -1;
    if (fld.TextApply == 1)
      index1 = this.e.CurLine;
    else if (fld.TextApply == 2)
    {
      this.MoveLineArrays(this.e.CurLine, 1, 'B');
      index1 = this.e.CurLine;
    }
    else if (fld.TextApply == 3)
    {
      this.MoveLineArrays(this.e.CurLine, 1, 'A');
      index1 = this.e.CurLine + 1;
    }
    if (index1 >= 0)
    {
      if (fld.LineLen < 0)
        fld.LineLen = 0;
      this.LineAlloc(index1, this.e.text[index1].len, fld.LineLen);
      char[] txt = this.e.text[index1].txt;
      for (int index2 = 0; index2 < this.e.text[index1].len; ++index2)
        txt[index2] = fld.text[index2];
      ushort[] numArray = this.OpenCfmt(index1);
      for (int index3 = 0; index3 < this.e.text[index1].len; ++index3)
      {
        numArray[index3] = (ushort) 0;
        if ((int) fld.font[index3] <= this.e.TotalFonts && this.e.TerFont[(int) fld.font[index3]].InUse)
          numArray[index3] = fld.font[index3];
      }
      this.CloseCfmt(index1);
      this.e.text[index1].pfmt = 0;
      if (fld.pfmt >= 0 && fld.pfmt <= this.e.TotalPfmts)
        this.e.text[index1].pfmt = fld.pfmt;
      ++this.e.TerArg.modified;
      this.e.PaintFlag = 4;
    }
    this.e.WrapFlag = fld.WrapFlag;
    this.e.ReclaimResources = fld.ReclaimResources;
    if (!this.e.PaintEnabled && fld.PaintEnabled)
      this.e.PaintFlag = 4;
    this.e.PaintEnabled = fld.PaintEnabled;
    this.PaintTer();
    return true;
  }

  internal new DialogResult ShowMessage(string msg1, string msg2, MessageBoxButtons buttons)
  {
    int num = this.e.Focused ? 1 : 0;
    DialogResult dialogResult = MessageBox.Show(msg1, msg2, buttons);
    if (num != 0)
      this.e.Focus();
    return dialogResult;
  }

  internal static bool StcPrintf(params object[] msg)
  {
    string msg1 = "";
    foreach (object obj in msg)
    {
      if (msg1.Length > 0)
        msg1 += " ";
      msg1 = obj == null ? msg1 + "null object" : msg1 + obj.ToString();
    }
    return CMisc.OurPrintf((object) msg1, false);
  }

  internal new int strcmpi(string str1, string str2) => string.Compare(str1, str2, true);

  internal new void StripSlashes(string InStr, out string OutStr)
  {
    int length1 = InStr.Length;
    int num = 0;
    bool flag = false;
    char[] chArray = new char[length1 + 1];
    int length2;
    for (int index = length2 = 0; index < length1; ++index)
    {
      if (InStr[index] == '\\')
      {
        ++num;
        if (num % 2 != 1)
          continue;
      }
      else
        num = 0;
      if (InStr[index] == '"')
      {
        if (!flag)
          flag = true;
        else
          break;
      }
      else
      {
        chArray[length2] = InStr[index];
        ++length2;
      }
    }
    OutStr = new string(chArray, 0, length2);
  }

  internal new void StrPrepend(char[] str, char[] pre)
  {
    char[] chArray = new char[1000];
    this.lstrcpy(chArray, pre);
    this.lstrcat(chArray, str);
    this.lstrcpy(str, chArray);
  }

  internal new void StrQuote(ref string str) => str = new string('"', 1) + str + new string('"', 1);

  internal bool TerAddAutoCompWord(string ACWord, string ACPhrase)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.e.TotalAutoComps + 1 > 100)
      return false;
    if (ACWord == null)
      ACWord = "";
    if (ACPhrase == null)
      ACPhrase = "";
    if (ACWord.Length == 0)
      return false;
    this.e.AutoCompWord[this.e.TotalAutoComps] = ACWord;
    this.e.AutoCompPhrase[this.e.TotalAutoComps] = ACPhrase;
    ++this.e.TotalAutoComps;
    return true;
  }

  internal int TerAnd(int val1, int val2) => val1 & val2;

  internal void TerClassCleanup()
  {
    COp.Win32.EnumThreadWindows(COp.Win32.GetCurrentThreadId(), new COp.Win32.EnumThreadWindowsCallback(this.ClassCleanupCallback), IntPtr.Zero);
  }

  internal bool TerClearAutoCompList()
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    this.e.TotalAutoComps = 0;
    return true;
  }

  internal new bool TerDoubleClick()
  {
    int index1 = 0;
    if (!this.e.RulerClicked && this.e.text[this.e.CurLine].len != 0 && !this.TerEditOle(false) && (!this.e.LinkDblClick || !this.SendLinkMessage(true, false)) && (this.e.TerFlags3 & 1073741824 /*0x40000000*/) == 0)
    {
      if (this.e.PictureClicked)
      {
        index1 = this.GetCurCfmt(this.e.CurLine, this.e.CurCol);
        if ((this.e.TerFont[index1].style & 128 /*0x80*/) != 0)
          return true;
      }
      int line1 = this.e.CurLine;
      int index2 = this.e.CurCol;
      bool flag1 = false;
      bool flag2 = false;
      while (line1 >= 0)
      {
        if (index2 < 0)
          index2 = 0;
        char[] txt = this.e.text[line1].txt;
        ushort[] numArray = this.OpenCfmt(line1);
        for (; index2 >= 0; --index2)
        {
          int index3 = index1;
          index1 = (int) numArray[index2];
          if ((index2 < this.e.CurCol || line1 < this.e.CurLine) && this.e.TerFont[index1].FieldId != this.e.TerFont[index3].FieldId)
          {
            flag1 = true;
            flag2 = true;
            break;
          }
          bool flag3 = txt[index2] >= '!' && txt[index2] <= '.' || txt[index2] >= ':' && txt[index2] <= '@';
          if (flag1 && (flag3 || txt[index2] == ' ' || txt[index2] == '\t' || (int) txt[index2] == (int) this.e.ParaChar || txt[index2] == '\u000F' || (int) txt[index2] == (int) this.e.CellChar || txt[index2] == '\f' || txt[index2] == '\u0016' || txt[index2] == '\u0014' || txt[index2] == '\u000E'))
          {
            flag2 = true;
            break;
          }
          if (txt[index2] != ' ' && txt[index2] != '\t')
            flag1 = true;
        }
        if (!flag2)
        {
          --line1;
          if (line1 >= 0)
            index2 = this.e.text[line1].len - 1;
        }
        else
          break;
      }
      if (line1 == -1)
        line1 = 0;
      if (flag1)
      {
        this.e.HilightBegCol = index2 + 1;
      }
      else
      {
        int curCol = this.e.CurCol;
        while (curCol < this.e.text[line1].len && (this.e.text[line1].txt[curCol] == ' ' || this.e.text[line1].txt[curCol] == '\t'))
          ++curCol;
        if (curCol == this.e.text[line1].len)
          return true;
        this.e.HilightBegCol = curCol;
      }
      this.e.HilightBegRow = line1;
      bool flag4 = false;
      int index4 = this.e.HilightBegCol;
      int line2 = this.e.HilightBegRow;
      while (line2 < this.e.TotalLines)
      {
        char[] txt = this.e.text[line2].txt;
        ushort[] numArray = this.OpenCfmt(line2);
        for (; index4 < this.e.text[line2].len; ++index4)
        {
          int index5 = index1;
          index1 = (int) numArray[index4];
          if ((index4 > this.e.HilightBegCol || line2 > this.e.HilightBegRow) && this.e.TerFont[index1].FieldId != this.e.TerFont[index5].FieldId)
          {
            flag4 = true;
            break;
          }
          if ((txt[index4] < '!' || txt[index4] > '.' ? (txt[index4] < ':' ? 0 : (txt[index4] <= '@' ? 1 : 0)) : 1) != 0 || txt[index4] == '\t' || (int) txt[index4] == (int) this.e.ParaChar || txt[index4] == '\u000F' || (int) txt[index4] == (int) this.e.CellChar || txt[index4] == '\f' || txt[index4] == '\u0016' || txt[index4] == '\u0014' || txt[index4] == '\u000E')
          {
            flag4 = true;
            break;
          }
          if (!flag4 || txt[index4] == ' ')
          {
            if (txt[index4] == ' ')
              flag4 = true;
          }
          else
            break;
        }
        if (!flag4)
        {
          ++line2;
          if (line2 < this.e.TotalLines)
            index4 = 0;
        }
        else
          break;
      }
      if (line2 >= this.e.TotalLines)
        line2 = this.e.TotalLines - 1;
      if (flag4)
        this.e.HilightEndCol = index4;
      else
        this.e.HilightEndCol = this.e.text[line2].len;
      if (this.e.HilightEndCol >= this.e.text[line2].len && line2 + 1 == this.e.TotalLines && this.e.TerArg.WordWrap)
        --this.e.HilightEndCol;
      this.e.HilightEndRow = line2;
      for (int hilightBegRow = this.e.HilightBegRow; hilightBegRow <= this.e.HilightEndRow; ++hilightBegRow)
        this.CloseCfmt(hilightBegRow);
      if (this.e.HilightBegRow > this.e.HilightEndRow || this.e.HilightEndCol <= this.e.HilightBegCol && this.e.HilightBegRow == this.e.HilightEndRow || this.e.HilightBegCol == -1)
        return true;
      this.e.HilightType = 2;
      this.e.StretchHilight = true;
      this.e.IgnoreMouseMove = false;
      this.e.DblClickHilight = true;
      this.e.DblClickEndRow = this.e.HilightEndRow;
      this.e.DblClickEndCol = this.e.HilightEndCol;
    }
    return true;
  }

  internal bool TerEnableSpeedKey(int cmd, bool enable)
  {
    bool flag = false;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (cmd >= 600 && cmd < 900)
    {
      int index = 0;
      while (index < this.e.TotalSpeedKeys && this.e.SpeedKeyCmd[index] != cmd)
        ++index;
      if (index < this.e.TotalSpeedKeys)
      {
        flag = this.e.SpeedKeyEnabled[index];
        this.e.SpeedKeyEnabled[index] = enable;
      }
    }
    return flag;
  }

  internal string TerGetDir(string file, out string dir)
  {
    dir = "";
    int length = file.Length;
    if (length != 0)
    {
      int index1;
      for (index1 = length - 1; index1 >= 0; --index1)
      {
        switch (file[index1])
        {
          case ':':
          case '\\':
            goto label_5;
          default:
            continue;
        }
      }
label_5:
      if (index1 < 0)
        return dir;
      int num = index1 + 1;
      for (int index2 = 0; index2 < num; ++index2)
      {
        char c = file[index2];
        dir += new string(c, 1);
      }
    }
    return dir;
  }

  internal string TerGetExt(string file, out string ext)
  {
    ext = "";
    int length = file.Length;
    if (length != 0)
    {
      int index;
      for (index = length - 1; index >= 0; --index)
      {
        switch (file[index])
        {
          case '.':
            goto label_6;
          case ':':
          case '\\':
            return ext;
          default:
            continue;
        }
      }
label_6:
      if (index < 0)
        return ext;
      for (; index < length; ++index)
      {
        char c = file[index];
        ext += new string(c, 1);
      }
    }
    return ext;
  }

  internal int TerGetLastMessage(out string message, out string DebugMsg)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    message = this.e.MsgString[this.e.TerLastMsg];
    DebugMsg = this.e.TerLastDebugMsg;
    return this.e.TerLastMsg;
  }

  internal int TerGetParam(int type)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    switch (type)
    {
      case 1:
        return this.e.TotalLines;
      case 2:
        return this.e.TotalPfmts;
      case 3:
        return this.e.TotalFonts;
      case 4:
        return this.e.TotalSID;
      case 5:
        return this.e.TotalPages;
      case 6:
        return this.e.TotalParaFrames;
      case 7:
        return this.e.TotalCharTags;
      case 8:
        return this.e.TotalImageMaps;
      case 9:
        return this.e.TotalBlts;
      case 10:
        return this.e.TotalLists;
      case 11:
        return this.e.TotalListOr;
      case 12:
        return this.e.TotalTabs;
      case 13:
        return this.e.TotalTableRows;
      case 14:
        return this.e.TotalCells;
      case 15:
        return this.e.TotalSects;
      case 16 /*0x10*/:
        return this.e.CurLine;
      case 17:
        return this.e.CurCol;
      case 18:
        return this.GetSection(this.e.CurLine);
      case 19:
        return this.e.HilightType;
      case 20:
        return this.e.HilightBegRow;
      case 21:
        return this.e.HilightBegCol;
      case 22:
        return this.e.HilightEndRow;
      case 23:
        return this.e.HilightEndCol;
      case 24:
        return !this.e.WmWashed ? 0 : 1;
      case 25:
        return this.e.WmParaFID > 0 ? this.e.ParaFrame[this.e.WmParaFID].pict : -1;
      default:
        return -1;
    }
  }

  internal bool TerGetParam(int type, out Color color)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    color = tc.CLR_WHITE;
    if (type != 26)
      return false;
    color = this.e.PageBkColor;
    return true;
  }

  internal bool TerGetReadOnly() => this.e.TerArg.ReadOnly;

  internal int TerGetRtfDocInfo(int InfoType, out string str)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    str = "";
    if (InfoType < 0 || InfoType >= 11 || this.e.pRtfInfo[InfoType] == null)
      return 0;
    str = this.e.pRtfInfo[InfoType];
    return this.e.pRtfInfo[InfoType].Length;
  }

  internal new bool TerInsert()
  {
    if (this.e.InsertMode)
      this.e.InsertMode = false;
    else
      this.e.InsertMode = true;
    if (this.e.TrackChanges)
      this.e.InsertMode = true;
    this.DisplayStatus();
    return true;
  }

  internal bool TerIsModified()
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    return this.True(this.e.TerArg.modified);
  }

  internal bool TerLineInfoFlags(int LineNo, int flags) => this.LineInfo(LineNo, flags);

  internal int TerOr(int val1, int val2) => val1 | val2;

  internal new bool TerPostProcessing(int message, int wParam, int lParam)
  {
    if (!this.e.InPrintPreview && this.e.PaintEnabled && this.e.TotalLines != 0 && (this.e.TotalLines != 1 || this.e.text[0].len != 0))
    {
      if ((this.e.TerFlags4 & 1024 /*0x0400*/) != 0 && (this.e.MessageId == 273 || this.e.MessageId == 258))
        this.CheckWindowOverflow();
      this.OlePostProcessing();
      if (message != 273 || COp.LOWORD(wParam) != (ushort) 602 && COp.LOWORD(wParam) != (ushort) 603 && COp.LOWORD(wParam) != (ushort) 600 && COp.LOWORD(wParam) != (ushort) 601)
        this.e.CursHorzPos = -1;
      if (!this.e.CaretPositioned)
        this.OurSetCaretPos();
      if (this.e.TerArg.lastNotifiedModified < this.e.TerArg.modified && !this.e.TerArg.ReadOnly)
      {
        this.e.TerArg.lastNotifiedModified = this.e.TerArg.modified;
        this.e.Notified = true;
        this.e.SendMessageToParent(2725, (int) this.e.hTerWnd, 0, false);
      }
      if (this.e.PosPageHdrFtr)
        this.ReposPageHdrFtr(true);
      if (this.e.text[this.e.CurLine] != null && this.e.CurPfmt != this.e.text[this.e.CurLine].pfmt || this.e.RulerSection != this.GetSection(this.e.CurLine))
        this.DrawRuler(true);
      if (message != 258 && (!this.CharMessagePending() || this.e.CommandId != 0))
        this.UpdateToolBar(false);
      if (this.e.TerArg.PrintView && (this.e.RepageBeginLine < this.e.TotalLines || this.e.PageModifyCount != this.e.TerArg.modified))
      {
        if (this.e.TotalLines >= 5000 && !this.e.RepageTimerOn && (this.e.TerFlags2 & 131072 /*0x020000*/) == 0)
          this.e.RepageTimerOn = this.True(this.SetTimer(this.e.hTerWnd, 9182, 5000));
        if ((this.e.TotalLines < 5000 || !this.e.RepageTimerOn) && (this.e.TerFlags2 & 1024 /*0x0400*/) == 0 && this.Repaginate(true, false, 0, true))
          return true;
      }
      if (this.e.SectModified && !this.MessagePending() && this.RecreateSections())
      {
        this.PaintTer();
        this.TerPostProcessing(message, wParam, lParam);
        return true;
      }
    }
    return true;
  }

  internal bool TerQueryExit()
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.e.InDialogBox)
      return false;
    if (this.True(this.e.TerArg.modified) && !this.e.TerArg.ReadOnly)
    {
      DialogResult dialogResult;
      if (DialogResult.Yes == (dialogResult = this.ShowMessage(this.e.MsgString[(int) sbyte.MaxValue], "", MessageBoxButtons.YesNoCancel)))
        return this.e.DocName.Length > 0 ? this.TerSave(this.e.DocName, true) : this.TerSaveAs(this.e.DocName);
      if (dialogResult == DialogResult.Cancel)
        return false;
    }
    return true;
  }

  internal new bool TerReplaceString()
  {
    int num1 = 0;
    bool flag = false;
    Cursor cursor = (Cursor) null;
    int undoRef = this.e.UndoRef;
    if (!this.NormalizeBlock() || !this.CallDialogBox((Form) new terdlg_replace(this.e)) || this.e.ReplaceString.Length == 0)
      return true;
    int length1 = this.e.ReplaceString.Length;
    int length2 = this.e.ReplaceWith.Length;
    char opt = !this.e.ReplaceBlock ? 'E' : 'R';
    int StartLine;
    int StartCol;
    int EndLine;
    int EndCol;
    if (opt == 'E')
    {
      StartLine = 0;
      StartCol = 0;
      EndLine = this.e.TotalLines - 1;
      EndCol = this.e.text[EndLine].len - 1;
    }
    else
    {
      StartLine = this.e.HilightBegRow;
      StartCol = this.e.HilightType != 2 ? 0 : this.e.HilightBegCol;
      EndLine = this.e.HilightEndRow;
      EndCol = this.e.HilightType != 2 ? this.e.text[this.e.HilightEndRow].len - 1 : this.e.HilightEndCol - 1;
    }
    if (EndCol < 0)
      EndCol = 0;
    this.e.SearchFlags = 80 /*0x50*/;
    if (this.e.ReplaceVerify)
      this.e.SearchFlags |= 1;
    else if ((this.e.TerFlags & 512 /*0x0200*/) == 0)
    {
      cursor = this.e.Cursor;
      this.e.Cursor = Cursors.WaitCursor;
    }
    while (this.SearchDisplay(this.e.ReplaceString, opt, StartLine, StartCol, EndLine, EndCol))
    {
      this.e.HilightType = 2;
      this.e.HilightBegRow = this.e.HilightEndRow = this.e.CurLine;
      this.e.HilightBegCol = this.e.CurCol;
      this.e.HilightEndCol = this.e.CurCol + length1;
      if (this.e.HilightEndCol > this.e.text[this.e.CurLine].len)
        this.AbsToRowCol(this.RowColToAbs(this.e.CurLine, this.e.CurCol) + length1, 'E');
      int num2 = this.IsProtected(false, true) ? 1 : 0;
      this.e.HilightType = 0;
      if (num2 != 0)
      {
        num1 = this.e.CurCol;
        this.e.CurCol += length1;
        StartLine = this.e.HilightEndRow;
        StartCol = this.e.HilightEndCol;
        if (opt == 'E')
          opt = 'F';
      }
      else
      {
        if (this.e.ReplaceVerify)
        {
          this.e.HilightType = 2;
          this.e.HilightBegRow = this.e.HilightEndRow = this.e.CurLine;
          this.e.HilightBegCol = this.e.CurCol;
          this.e.HilightEndCol = this.e.CurCol + length1;
          if (this.e.HilightEndCol > this.e.text[this.e.CurLine].len)
            this.AbsToRowCol(this.RowColToAbs(this.e.CurLine, this.e.CurCol) + length1, 'E');
          this.TerPosLine(this.e.CurLine + 1);
          this.PaintTer();
          DialogResult dialogResult;
          if (DialogResult.No == (dialogResult = this.ShowMessage(this.e.MsgString[125], "", MessageBoxButtons.YesNoCancel)))
          {
            num1 = this.e.CurCol;
            this.e.CurCol += length1;
            if (this.e.CurCol > this.e.LineWidth)
              this.e.CurCol = this.e.LineWidth;
            StartLine = this.e.CurLine;
            StartCol = this.e.CurCol;
            if (opt == 'E')
            {
              opt = 'F';
              continue;
            }
            continue;
          }
          if (dialogResult == DialogResult.Cancel)
            return true;
        }
        flag = true;
        int num3;
        if (this.e.CurCol + length1 > this.e.text[this.e.CurLine].len && this.e.CurLine < this.e.TotalLines - 1)
        {
          num3 = this.e.text[this.e.CurLine].len - this.e.CurCol;
          int count = length1 - num3;
          while (count > 0 && this.e.CurLine + 1 < this.e.TotalLines)
          {
            if (count >= this.e.text[this.e.CurLine + 1].len)
            {
              count -= this.e.text[this.e.CurLine + 1].len;
              this.e.UndoRef = undoRef;
              if (this.e.text[this.e.CurLine + 1].len > 0)
                this.SaveUndo(this.e.CurLine + 1, 0, this.e.CurLine + 1, this.e.text[this.e.CurLine + 1].len - 1, 'D');
              this.MoveLineArrays(this.e.CurLine + 1, 1, 'D');
            }
            else
            {
              this.e.UndoRef = undoRef;
              this.SaveUndo(this.e.CurLine + 1, 0, this.e.CurLine + 1, count - 1, 'D');
              this.MoveLineData(this.e.CurLine + 1, 0, count, 'D');
              break;
            }
          }
        }
        else
          num3 = length1;
        this.e.UndoRef = undoRef;
        this.SaveUndo(this.e.CurLine, this.e.CurCol, this.e.CurLine, this.e.CurCol + num3 - 1, 'D');
        if (length2 > num3)
          this.MoveLineData(this.e.CurLine, this.e.CurCol + num3 - 1, length2 - num3, 'A');
        if (length2 < num3)
          this.MoveLineData(this.e.CurLine, this.e.CurCol + length2, num3 - length2, 'D');
        this.SetLineText(this.e.ReplaceWith, this.e.CurLine, this.e.CurCol);
        ushort[] fmt;
        ushort[] cmi;
        this.OpenCharInfo(this.e.CurLine, out fmt, out cmi);
        for (int index = num3; index < length2; ++index)
        {
          fmt[this.e.CurCol + index] = fmt[this.e.CurCol + num3 - 1];
          cmi[this.e.CurCol + index] = (ushort) 0;
        }
        this.CloseCharInfo(this.e.CurLine);
        if (this.e.text[this.e.CurLine].len > this.e.LineWidth)
          this.LineAlloc(this.e.CurLine, this.e.text[this.e.CurLine].len, this.e.LineWidth);
        this.e.UndoRef = undoRef;
        this.SaveUndo(this.e.CurLine, this.e.CurCol, this.e.CurLine, this.e.CurCol + length2 - 1, 'I');
        ++this.e.TerArg.modified;
        if (opt == 'E')
          opt = 'F';
        num1 = this.e.CurCol;
        this.e.CurCol += length2;
        if (this.e.CurCol >= this.e.LineWidth)
          this.e.CurCol = this.e.LineWidth;
        StartLine = this.e.CurLine;
        StartCol = this.e.CurCol;
        if (opt == 'R' && StartLine == EndLine && StartCol > EndCol)
        {
          this.PaintTer();
          return true;
        }
      }
    }
    if (!this.e.ReplaceVerify && cursor != (Cursor) null)
      this.e.Cursor = cursor;
    if (this.e.CurLine >= this.e.TotalLines)
      this.e.CurLine = this.e.TotalLines - 1;
    if (!flag)
    {
      this.PrintError(97, "");
      this.PaintTer();
    }
    if (flag)
    {
      this.e.CurCol = num1;
      this.TerPosLine(this.e.CurLine + 1);
      if (length2 > 0)
      {
        this.e.HilightType = 2;
        this.e.HilightBegRow = this.e.HilightEndRow = this.e.CurLine;
        this.e.HilightBegCol = this.e.CurCol;
        this.e.HilightEndCol = this.e.CurCol + length2;
        if (this.e.HilightEndCol > this.e.text[this.e.CurLine].len)
          this.AbsToRowCol(this.RowColToAbs(this.e.CurLine, this.e.CurCol) + length2, 'E');
      }
      this.PaintTer();
    }
    return true;
  }

  internal bool TerResetLastMessage()
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    this.e.TerLastMsg = 0;
    this.e.TerLastDebugMsg = "";
    return true;
  }

  internal new bool TerSearchBackward()
  {
    if (this.e.SearchString.Length == 0)
    {
      this.TerSearchString();
      return true;
    }
    if (!this.SearchDisplay(this.e.SearchString, 'B', 0, 0, 0, 0))
    {
      this.PrintError(97, this.e.MsgString[14]);
      ++this.e.CurCol;
    }
    return true;
  }

  internal new bool TerSearchForward()
  {
    if (this.e.SearchString.Length == 0)
    {
      this.TerSearchString();
      return true;
    }
    ++this.e.CurCol;
    if (!this.SearchDisplay(this.e.SearchString, 'F', 0, 0, 0, 0))
    {
      this.PrintError(97, this.e.MsgString[15]);
      --this.e.CurCol;
    }
    return true;
  }

  internal bool TerSearchReplace(
    ref string search,
    string replace,
    int flags,
    int StartPos,
    ref int EndPos,
    out int BufSize)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    BufSize = 0;
    this.e.SearchFlags = flags;
    if ((this.e.SearchFlags & 2) != 0)
    {
      string dest;
      if (!this.ParseUserString(search, out dest))
        return false;
      this.e.SearchString = dest;
      int row;
      int col;
      this.AbsToRowCol(StartPos, out row, out col);
      int curLine = this.e.CurLine;
      int curCol = this.e.CurCol;
      bool flag = (this.e.SearchFlags & 128 /*0x80*/) == 0 ? this.SearchDisplay(this.e.SearchString, 'R', row, col, this.e.TotalLines - 1, this.e.text[this.e.TotalLines - 1].len) : this.SearchDisplay(this.e.SearchString, 'S', row, col, 0, 0);
      this.e.SearchString = "";
      if (!flag)
        return false;
      EndPos = this.RowColToAbs(this.e.CurLine, this.e.CurCol);
      if ((this.e.SearchFlags & 1) == 0)
      {
        this.e.CurLine = curLine;
        this.e.CurCol = curCol;
      }
      return true;
    }
    if ((this.e.SearchFlags & 4) != 0)
    {
      if ((flags & 256 /*0x0100*/) != 0)
      {
        this.AbsToRowCol(StartPos, 'B');
        this.AbsToRowCol(EndPos + 1, 'E');
        this.e.HilightType = 2;
        this.e.StretchHilight = false;
        if (this.IsProtected(false, true))
          return false;
      }
      string dest;
      if (!this.ParseUserString(replace, out dest) || !this.ReplaceTextString(dest, StartPos, EndPos))
        return false;
      this.PaintTer();
      return true;
    }
    if ((this.e.SearchFlags & 8) != 0)
    {
      int row1;
      int col1;
      this.AbsToRowCol(StartPos, out row1, out col1);
      int row2;
      int col2;
      this.AbsToRowCol(EndPos + 1, out row2, out col2);
      for (int index = row1; index <= row2; ++index)
      {
        if (this.e.text[index].len != 0)
        {
          int startIndex = 0;
          int num = this.e.text[index].len;
          if (index == row1)
            startIndex = col1;
          if (index == row2)
            num = col2;
          char[] txt = this.e.text[index].txt;
          search += new string(txt, startIndex, num - startIndex);
        }
      }
      BufSize = search.Length;
    }
    return true;
  }

  internal int TerSearchReplace2(
    string search,
    string replace,
    int flags,
    int StartPos,
    int EndPos)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    bool flag = this.TerSearchReplace(ref search, replace, flags, StartPos, ref EndPos, out int _);
    if ((flags & 4) != 0)
      return 1;
    return flag ? EndPos : -1;
  }

  internal new bool TerSearchString()
  {
    this.e.SearchFlags |= 65;
    if (this.CallDialogBox((Form) new terdlg_search(this.e)) && this.e.SearchString.Length != 0)
    {
      if (this.e.SearchDirection == 'F')
        this.NextTextPos();
      if (!this.SearchDisplay(this.e.SearchString, this.e.SearchDirection, 0, 0, 0, 0))
      {
        this.PrintError(97, "Find");
        if (this.e.CurLine >= this.e.TotalLines)
          this.e.CurLine = this.e.TotalLines - 1;
      }
    }
    return true;
  }

  internal bool TerSetCustomMessage(int id, string message)
  {
    if (id < 0 || id >= 250)
      return false;
    this.e.MsgString[id] = message;
    this.e.CustomMsg[id] = true;
    return true;
  }

  internal bool TerSetDefTabType(int DefTab)
  {
    if (DefTab != 0 && DefTab != 1 && DefTab != 3 && DefTab != 2)
      return false;
    this.e.DefTabType = DefTab;
    return true;
  }

  internal bool TerSetDocTextFlow(bool dialog, int flow, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.WordWrap)
      return false;
    if (dialog)
    {
      this.e.DlgInt1 = this.e.DocTextFlow;
      this.e.DlgText1 = "Document Text Flow";
      if (!this.CallDialogBox((Form) new terdlg_para_text_flow(this.e)))
        return true;
      flow = this.e.DlgInt1;
    }
    if (flow == 0 || flow == 2 || flow == 1)
    {
      this.e.DocTextFlow = flow;
      if (repaint)
      {
        this.RequestPagination(true);
        this.PaintTer();
      }
    }
    return true;
  }

  internal int TerSetFlags(bool set, int flags)
  {
    if (set)
      this.e.TerFlags |= flags;
    else
      this.e.TerFlags = this.ResetTerFlag(flags);
    return this.e.TerFlags;
  }

  internal int TerSetFlags2(bool set, int flags)
  {
    if (set)
      this.e.TerFlags2 |= flags;
    else
      this.e.TerFlags2 = this.ResetTerFlag2(flags);
    return this.e.TerFlags2;
  }

  internal int TerSetFlags3(bool set, int flags)
  {
    if (set)
    {
      this.e.TerFlags3 |= flags;
      if ((flags & 32 /*0x20*/) != 0)
        this.e.CursorCell = this.e.text[this.e.CurLine].cid;
    }
    else
    {
      this.e.TerFlags3 = this.ResetTerFlag3(flags);
      if ((flags & 32 /*0x20*/) != 0)
        this.e.CursorCell = 0;
    }
    return this.e.TerFlags3;
  }

  internal int TerSetFlags4(bool set, int flags)
  {
    if (set)
      this.e.TerFlags4 |= flags;
    else
      this.e.TerFlags4 = this.ResetTerFlag4(flags);
    return this.e.TerFlags4;
  }

  internal int TerSetFlags5(bool set, int flags)
  {
    if (set)
      this.e.TerFlags5 |= flags;
    else
      this.e.TerFlags5 = this.ResetTerFlag5(flags);
    return this.e.TerFlags5;
  }

  internal int TerSetFlags6(bool set, int flags)
  {
    if (set)
      this.e.TerFlags6 |= flags;
    else
      this.e.TerFlags6 = this.ResetTerFlag5(flags);
    return this.e.TerFlags6;
  }

  internal bool TerSetFocus()
  {
    if (!this.e.UseWin)
      return false;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    IntPtr hTerWnd = this.e.hTerWnd;
    this.e.TerOpFlags |= 262144 /*0x040000*/;
    this.PostMessage(hTerWnd, 513, 0, 0);
    this.PostMessage(hTerWnd, 514, 0, 0);
    return true;
  }

  internal bool TerSetHtnAssembly(Assembly assembly)
  {
    tc.hHtn = assembly;
    return true;
  }

  internal void TerSetHtnLicenseKey(string key) => tc.HtnLicenseKey = key;

  internal bool TerSetHtnObject(object htn)
  {
    if (!tc.HtnSearched)
      this.SearchHtn();
    if (tc.hHtn == (Assembly) null || tc.HtnType == (System.Type) null)
      return false;
    if (htn != null)
    {
      this.e.htn = htn;
      return true;
    }
    if (this.e.htn == null)
    {
      try
      {
        System.Type[] types = new System.Type[2]
        {
          typeof (ImRtfEditor),
          typeof (bool)
        };
        this.e.htn = tc.HtnType.GetConstructor(types).Invoke(new object[2]
        {
          (object) this.e,
          (object) false
        });
      }
      catch (Exception ex)
      {
        this.e.htn = (object) null;
        tc.HtnType = (System.Type) null;
        tc.hHtn = (Assembly) null;
        return false;
      }
    }
    return true;
  }

  internal bool TerSetModify(bool modified)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (modified)
    {
      ++this.e.TerArg.modified;
    }
    else
    {
      this.e.TerArg.modified = 0;
      this.e.Notified = false;
    }
    return true;
  }

  internal bool TerSetPictPctWidth(int pict, int width)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (pict >= 0 && pict <= this.e.TotalFonts && !this.False(this.e.TerFont[pict].InUse))
    {
      if (width > 0)
      {
        if (width > 100)
          width = 100;
        this.e.TerFont[pict].PctWidth = width;
        int num = this.MulDiv(this.e.TerWinWidth, width, 100);
        this.e.TerFont[pict].PictWidth = this.ScrToPointsX(num);
        this.SetPictSize(pict, this.e.TerFont[pict].height, num, true);
      }
      this.XlateSizeForPrt(pict);
    }
    return true;
  }

  internal bool TerSetReadOnly(bool ReadOnly)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    bool flag = this.e.TerArg.ReadOnly;
    this.e.TerArg.ReadOnly = ReadOnly;
    this.InitCaret();
    if (this.e.TerTlb != null)
    {
      this.EnableToolbarIcons(!ReadOnly);
      this.UpdateToolBar(true);
    }
    if ((this.e.TerFlags4 & 8) != 0)
    {
      for (int pict = 0; pict < this.e.TotalFonts; ++pict)
      {
        if (this.IsControl(pict) && this.e.TerFont[pict].ctl != null)
          this.e.TerFont[pict].ctl.Enabled = !ReadOnly;
      }
    }
    return flag;
  }

  internal bool TerSetRtfDocInfo(int InfoType, string str)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (InfoType < 0 || InfoType >= 11)
      return false;
    this.e.pRtfInfo[InfoType] = str;
    ++this.e.TerArg.modified;
    return true;
  }

  internal bool TerSetSearchString(string SearchFor, bool CaseSensitive)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    this.e.SearchString = SearchFor;
    if (CaseSensitive)
      this.e.SearchFlags |= 16 /*0x10*/;
    else
      this.e.SearchFlags = tc.ResetFlag(this.e.SearchFlags, 16 /*0x10*/);
    this.e.SearchFlags |= 1;
    return true;
  }

  internal bool TerSetWinBorder(int border, bool caption)
  {
    int num = caption ? 12582912 /*0xC00000*/ : 0;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int windowLong1 = COp.Win32.GetWindowLong(this.e.hTerWnd, -16);
    int windowLong2 = COp.Win32.GetWindowLong(this.e.hTerWnd, -20);
    tc.ResetUintFlag(ref windowLong1, 12845056 /*0xC40000*/);
    tc.ResetUintFlag(ref windowLong2, 512 /*0x0200*/);
    int NewVal = windowLong1 | num;
    switch (border)
    {
      case 1:
        NewVal |= 8388608 /*0x800000*/;
        break;
      case 2:
        NewVal |= 262144 /*0x040000*/;
        break;
      case 3:
        windowLong2 |= 512 /*0x0200*/;
        break;
    }
    COp.Win32.SetWindowLong(this.e.hTerWnd, -16, NewVal);
    COp.Win32.SetWindowLong(this.e.hTerWnd, -20, windowLong2);
    COp.Win32.SetWindowPos(this.e.hTerWnd, IntPtr.Zero, 0, 0, 0, 0, 51);
    return true;
  }

  internal int TerSetZoom(int percent)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (percent == -2)
      return this.e.ZoomPercent;
    int zoomPercent = this.e.ZoomPercent;
    if (percent < 0)
    {
      if (!this.CallDialogBox((Form) new terdlg_zoom(this.e)))
        return -1;
      percent = this.e.DlgInt1;
    }
    if (this.e.ZoomPercent == percent)
      return this.e.ZoomPercent;
    if (percent < 10)
      percent = 10;
    if (percent > 500)
      percent = 500;
    this.ApplyZoomPercent(percent);
    this.RecreateFonts(this.e.TerGr);
    for (int pict = 0; pict < this.e.TotalFonts; ++pict)
    {
      if (this.IsControl(pict))
      {
        int width = this.e.TerFont[pict].CharWidth[24];
        int height = this.e.TerFont[pict].height;
        this.e.TerFont[pict].ctl.Size = new Size(width, height);
      }
    }
    if (this.e.TerArg.PageMode)
      this.e.TerRepaginate(true);
    this.e.TerRepaint(true);
    return zoomPercent;
  }

  internal int TerTab2Spaces(int line, int col)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (line < 0 || line >= this.e.TotalLines || col < 0 || col >= this.e.text[line].len)
      return 0;
    char[] txt = this.e.text[line].txt;
    if (txt[col] != '\t')
      return 0;
    ushort[] numArray = this.OpenCfmt(line);
    int num1 = 0;
    if (col > 0 && this.e.TabPrevLine == line && this.e.TabPrevCol == col - 1 && txt[col - 1] == '\t' && (int) numArray[col - 1] == (int) numArray[col])
      num1 = this.e.TabPrevAdj;
    int x = (int) this.GetLineCharWidth(line)[col] + num1;
    int index = (int) this.OpenCfmt(line)[col];
    this.CloseCfmt(line);
    int z = this.e.TerFont[index].CharWidth[32 /*0x20*/];
    if (z == 0)
      return 0;
    int num2 = this.MulDiv(x, 1, z);
    this.e.TabPrevLine = line;
    this.e.TabPrevCol = col;
    this.e.TabPrevAdj = x - num2 * z;
    return num2;
  }

  internal new int TerTextExtentX(Graphics gr, string str, int len)
  {
    COp.SIZE size;
    this.GetTextExtentPoint(gr, this.e.TerCurFont, str, len, out size);
    return size.cx;
  }

  internal new double ToDouble(string txt) => this.ToDouble(txt, out tc.SkipBool);

  internal new double ToDouble(TextBox item) => this.ToDouble(item.Text);

  internal new double ToDouble(string txt, out bool error)
  {
    error = false;
    try
    {
      return Convert.ToDouble(txt);
    }
    catch (Exception ex)
    {
      error = true;
      this.e.ExpMessage = ex.Message;
      return 0.0;
    }
  }

  internal new double ToDouble(TextBox item, bool DoCmToInches)
  {
    double inches = this.ToDouble(item.Text);
    if (DoCmToInches)
      inches = (double) this.CmToInches((float) inches);
    return inches;
  }

  internal new bool ToggleHiddenText()
  {
    this.e.ShowHiddenText = !this.e.ShowHiddenText;
    if (!this.e.ShowHiddenText && (this.e.TerFlags4 & 4) != 0)
      this.HideHiddenParaMarkers();
    this.RecreateFonts(this.e.TerGr);
    this.e.PageModifyCount = -1;
    this.e.RepageBeginLine = 0;
    this.PaintTer();
    return true;
  }

  internal new bool ToggleParaMark()
  {
    bool flag = false;
    this.e.ShowParaMark = !this.e.ShowParaMark;
    if (this.e.HasOptionalHyph)
      this.RecreateFonts(this.e.TerGr);
    for (int pict = 0; pict < this.e.TotalFonts; ++pict)
    {
      if (!this.False(this.e.TerFont[pict].InUse) && (this.e.TerFont[pict].style & 128 /*0x80*/) != 0 && this.e.TerFont[pict].FrameType != 0)
      {
        this.SetPictSize(pict, 0, 0, true);
        flag = true;
      }
    }
    this.DeleteTextMap(true);
    if (flag && this.e.TerArg.PageMode)
      this.e.TerRepaginate(true);
    else
      this.PaintTer();
    return true;
  }

  internal new bool ToggleRuler()
  {
    this.e.TerArg.ruler = !this.e.TerArg.ruler;
    this.e.Invalidate();
    return true;
  }

  internal new bool ToggleStatusRibbon()
  {
    this.e.TerArg.ShowStatus = !this.e.TerArg.ShowStatus;
    this.e.Invalidate();
    return true;
  }

  internal new int ToInt(string txt) => this.ToInt(txt, out tc.SkipBool);

  internal new int ToInt(TextBox item) => this.ToInt(item.Text);

  internal new int ToInt(string txt, out bool error)
  {
    error = false;
    try
    {
      return Convert.ToInt32(txt);
    }
    catch (Exception ex)
    {
      error = true;
      this.e.ExpMessage = ex.Message;
      return 0;
    }
  }

  internal new bool ToInt(Form form, TextBox item, out int result)
  {
    result = 0;
    bool error;
    int num1 = this.ToInt(item.Text, out error);
    if (error)
    {
      int num2 = (int) this.ShowMessage(this.e.MsgString[80 /*0x50*/], "", MessageBoxButtons.OK);
      item.Focus();
      form.DialogResult = DialogResult.None;
      return false;
    }
    result = num1;
    return true;
  }

  internal new bool ToInt(Form form, TextBox item, int IntVar)
  {
    int result;
    if (this.ToInt(form, item, out result))
    {
      switch (IntVar)
      {
        case 1:
          this.e.DlgInt1 = result;
          break;
        case 2:
          this.e.DlgInt2 = result;
          break;
        case 3:
          this.e.DlgInt3 = result;
          break;
        case 4:
          this.e.DlgInt4 = result;
          break;
        case 5:
          this.e.DlgInt5 = result;
          break;
        case 6:
          this.e.DlgInt6 = result;
          break;
        default:
          goto label_8;
      }
      return true;
    }
label_8:
    return false;
  }

  internal new int XlateCommandId(int CmdId)
  {
    int num1 = this.LineTextAngle(this.e.CurLine);
    int num2 = CmdId;
    if ((this.e.text[this.e.CurLine].flags2 & 32 /*0x20*/) != 0 && (this.e.text[this.e.CurLine].flags2 & 256 /*0x0100*/) != 0)
    {
      if (CmdId == 604)
        num2 = 605;
      if (CmdId == 605)
        num2 = 604;
      if (CmdId == 659)
        num2 = 660;
      if (CmdId == 660)
        num2 = 659;
      if (CmdId == 772)
        num2 = 658;
      if (CmdId == 658)
        num2 = 772;
    }
    CmdId = num2;
    if (num1 == 0)
      return CmdId;
    if (CmdId == 602)
      num2 = num1 == 90 ? 605 : 604;
    if (CmdId == 603)
      num2 = num1 == 90 ? 604 : 605;
    if (CmdId == 604)
      num2 = num1 == 90 ? 602 : 603;
    if (CmdId == 605)
      num2 = num1 == 90 ? 603 : 602;
    return num2;
  }
}
