// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.CPrt
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class CPrt : COp
{
  private bool PrtCollate;
  private int PrtCopies;
  private int PrtCurCopy;
  private int PrtFirstPage;
  private int PrtLastPage;
  private Button PvClose;
  private Label PvPageNo;
  private Button PvPages;
  private Button PvPrint;
  private TlbComboBox PvZoom;
  private int[] zoom;

  internal CPrt(ImRtfEditor ImRtfEditor)
    : base(ImRtfEditor)
  {
    this.zoom = new int[8]
    {
      0,
      200,
      150,
      100,
      75,
      50,
      25,
      10
    };
    this.e = ImRtfEditor;
  }

  internal bool ApplyPaperBins(PaperSourceKind FirstPageBin, PaperSourceKind NextPageBin)
  {
    for (int index = 0; index < this.e.TotalSects; ++index)
    {
      this.e.TerSect[index].FirstPageBin = FirstPageBin;
      this.e.TerSect[index].bin = NextPageBin;
    }
    ++this.e.TerArg.modified;
    return true;
  }

  internal new bool ApplyPaperOrient(bool IsPortrait)
  {
    for (int index = 0; index < this.e.TotalSects; ++index)
      this.e.TerSect[index].IsPortrait = IsPortrait;
    ++this.e.TerArg.modified;
    return true;
  }

  internal new bool ApplyPaperSize(PaperSize size)
  {
    this.e.PprKind = size.Kind;
    for (int index = 0; index < this.e.TotalSects; ++index)
    {
      this.e.TerSect[index].PprKind = size.Kind;
      this.e.TerSect[index].PprWidth = (float) size.Width / 100f;
      this.e.TerSect[index].PprHeight = (float) size.Height / 100f;
    }
    ++this.e.TerArg.modified;
    return true;
  }

  internal void CreatePvToolbar()
  {
    this.e.PvTlb = new ToolbarControl();
    this.e.PvTlb.Parent = (Control) this.e;
    this.e.PvTlb.Height = this.e.ToolBarHeight = 32 /*0x20*/;
    this.e.PvTlb.Paint += new PaintEventHandler(this.PvTlbPaint);
    this.PvZoom = new TlbComboBox();
    this.PvZoom.Parent = (Control) this.e.PvTlb;
    this.PvZoom.DropDownStyle = ComboBoxStyle.DropDown;
    this.PvZoom.Sorted = false;
    this.PvZoom.Location = new Point(20, 3);
    this.PvZoom.Size = new Size(70, 26);
    this.PvZoom.Font = this.e.RulerFont;
    this.PvZoom.ItemHeight = 14;
    this.PvZoom.SelectedIndexChanged += new EventHandler(this.PvZoomSelectedIndexChanged);
    this.PvZoom.EnterPressed += new TlbComboBox.DgtEnterPressed(this.PvZoomEnterPressed);
    for (int index = 0; index < this.zoom.Length; ++index)
      this.PvZoom.Items.Add(this.zoom[index] > 0 ? (object) this.zoom[index].ToString() : (object) this.e.MsgString[217]);
    int previewZoom = this.e.PreviewZoom;
    if (previewZoom <= 0)
      this.PvZoom.Text = this.e.MsgString[217];
    else
      this.PvZoom.Text = previewZoom.ToString();
    int num = this.GetDeviceCaps(this.e.TerGr, 88) > 96 /*0x60*/ ? 1 : 0;
    int height = 27;
    int y = 4;
    this.PvPages = new Button();
    this.PvPages.Parent = (Control) this.e.PvTlb;
    this.PvPages.Location = new Point(106, y);
    this.PvPages.Size = new Size(78, height);
    this.PvPages.Text = this.e.TotalPreviewPages == 1 ? this.e.MsgString[213] : this.e.MsgString[212];
    if (num != 0)
      this.PvPages.Font = new Font(this.PvPages.Font.FontFamily, this.PvPages.Font.Size - 1f);
    this.PvPages.BackColor = this.e.StatusBkColor;
    this.PvPages.Click += new EventHandler(this.PvTlbPvPagesClick);
    this.PvPrint = new Button();
    this.PvPrint.Parent = (Control) this.e.PvTlb;
    this.PvPrint.Location = new Point(200, y);
    this.PvPrint.Size = new Size(70, height);
    this.PvPrint.Text = this.e.MsgString[214];
    if (num != 0)
      this.PvPrint.Font = new Font(this.PvPrint.Font.FontFamily, this.PvPrint.Font.Size - 1f);
    this.PvPrint.BackColor = this.e.StatusBkColor;
    this.PvPrint.Click += new EventHandler(this.PvTlbPvPrintClick);
    this.PvClose = new Button();
    this.PvClose.Parent = (Control) this.e.PvTlb;
    this.PvClose.Location = new Point(290, y);
    this.PvClose.Size = new Size(70, height);
    this.PvClose.Text = this.e.TerArg.ReadOnly ? this.e.MsgString[219] : this.e.MsgString[215];
    if (num != 0)
      this.PvClose.Font = new Font(this.PvClose.Font.FontFamily, this.PvClose.Font.Size - 1f);
    this.PvClose.BackColor = this.e.StatusBkColor;
    this.PvClose.Click += new EventHandler(this.PvTlbPvCloseClick);
    this.PvPageNo = new Label();
    this.PvPageNo.Parent = (Control) this.e.PvTlb;
    this.PvPageNo.Location = new Point(380, y);
    this.PvPageNo.Size = new Size(70, height);
    this.PvPageNo.TextAlign = ContentAlignment.MiddleLeft;
    this.PvPageNo.BackColor = this.e.StatusBkColor;
    this.PvPageNo.Text = this.e.MsgString[216] + "1";
    if (num != 0)
      this.PvPageNo.Font = new Font(this.PvPageNo.Font.FontFamily, this.PvPageNo.Font.Size - 1f);
    this.PvZoom.locked = false;
    this.e.PvTlb.Visible = true;
  }

  internal new bool CurrentPrinter()
  {
    if (tc.DefPrt != null)
    {
      this.e.CurPrt = (PrinterSettings) tc.DefPrt.Clone();
      if (tc.DefPrtPage != null)
        this.e.CurPrtPage = (PageSettings) tc.DefPrtPage.Clone();
      else
        this.e.CurPrtPage = new PageSettings(this.e.CurPrt);
      this.e.PrinterAvailable = true;
      return true;
    }
    int num = 0;
    try
    {
      IEnumerator enumerator = PrinterSettings.InstalledPrinters.GetEnumerator();
      try
      {
        if (enumerator.MoveNext())
        {
          if (((string) enumerator.Current).Length > 0)
            ++num;
        }
      }
      finally
      {
        if (enumerator is IDisposable disposable)
          disposable.Dispose();
      }
    }
    catch (Exception ex)
    {
    }
    if (num == 0)
    {
      this.e.CurPrt = (PrinterSettings) null;
      this.e.CurPrtPage = (PageSettings) null;
      this.e.PrinterAvailable = false;
      return false;
    }
    this.e.CurPrt = new PrinterSettings();
    this.e.CurPrtPage = new PageSettings(this.e.CurPrt);
    this.e.PrinterAvailable = true;
    return true;
  }

  internal new bool InitPrinter()
  {
    if ((this.e.TerFlags & 1048576 /*0x100000*/) != 0)
    {
      this.e.PrinterAvailable = false;
      return true;
    }
    if (this.CurrentPrinter())
    {
      int num = this.e.PrinterAvailable ? 1 : 0;
    }
    return true;
  }

  internal new bool OpenCurPrinter(bool PrivatePrt) => true;

  private int OurLPtoDP(int x, int WinCX, int VwCX, int WinX, int VwX)
  {
    return (x - WinX) * VwCX / WinCX + VwX;
  }

  internal new bool PaintPrintPreview()
  {
    float num1 = 0.0f;
    float num2 = 0.0f;
    COp.RECT rect1 = new COp.RECT();
    if (this.e.TotalPreviewPages != 0)
    {
      if (this.e.TerArg.PrintView)
        this.e.CurPage = this.GetCurPage(this.e.CurLine);
      int curPage;
      int num3 = curPage = this.e.CurPage;
      Graphics terGr = this.e.TerGr;
      if (this.e.TotalPreviewPages > 2)
        this.e.TotalPreviewPages = 2;
      int pageSect = this.e.TerGetPageSect(num3);
      float pgWidth;
      float num4 = pgWidth = this.e.TerSect1[pageSect].PgWidth;
      float pgHeight;
      float num5 = pgHeight = this.e.TerSect1[pageSect].PgHeight;
      if (this.e.TotalPreviewPages > 1)
      {
        if (num3 + 1 < this.e.TotalPages)
          pageSect = this.e.TerGetPageSect(num3 + 1);
        num1 = this.e.TerSect1[pageSect].PgWidth;
        num2 = this.e.TerSect1[pageSect].PgHeight;
        num4 += 1f + num1;
        if ((double) num2 > (double) pgHeight)
          num5 = num2;
      }
      for (int index = 0; index < this.e.TotalScrCtls; ++index)
      {
        if (this.e.ScrCtl[index].InUse && this.e.ScrCtl[index].ctl != null && this.e.ScrCtl[index].ctl.Visible)
        {
          this.e.ScrCtl[index].ctl.Visible = false;
          this.e.ScrCtl[index].InUse = false;
          this.e.Validate();
        }
      }
      this.SetTerWindowOrg();
      this.TerResetClipRgn();
      Brush brush1 = (Brush) new SolidBrush(this.e.TextDefBkColor);
      terGr.FillRectangle(brush1, new Rectangle(this.e.TerWinOrgX - this.e.TerWinRect.left, this.e.TerWinOrgY - this.e.TerWinRect.top, this.e.TerRect.right - this.e.TerRect.left, this.e.TerRect.bottom - this.e.TerRect.top));
      brush1.Dispose();
      this.e.PvWidth = num4;
      this.e.PvHeight = num5;
      int num6;
      int num7;
      int num8;
      int y1;
      if (this.e.PreviewZoom <= 0)
      {
        num6 = (int) ((double) this.e.TerWinHeight * (double) num4 / (double) num5);
        if (this.e.TerWinWidth > num6)
        {
          num7 = this.e.TerWinHeight;
          num8 = (this.e.TerWinWidth - num6) / 2;
          y1 = 0;
        }
        else
        {
          num7 = (int) ((double) this.e.TerWinWidth * (double) num5 / (double) num4);
          num6 = this.e.TerWinWidth;
          num8 = 0;
          y1 = (this.e.TerWinHeight - num7) / 2;
          if (y1 < 0)
            y1 = 0;
        }
        this.e.TerWinOrgX = this.e.TerWinOrgY = 0;
      }
      else
      {
        num6 = (int) ((double) this.e.ScrResX * (double) num4 * (double) this.e.PreviewZoom / 100.0);
        num7 = (int) ((double) this.e.ScrResY * (double) num5 * (double) this.e.PreviewZoom / 100.0);
        num8 = y1 = 0;
        if (this.e.TerWinWidth > num6)
        {
          num8 = (this.e.TerWinWidth - num6) / 2;
          this.e.TerWinOrgX = 0;
        }
        if (this.e.TerWinHeight > num7)
        {
          y1 = (this.e.TerWinHeight - num7) / 2;
          this.e.TerWinOrgY = 0;
        }
      }
      this.e.PreviewHeight = num7;
      this.e.PreviewWidth = num6;
      int width = (int) (1.0 * (double) num6 / (double) num4);
      int num9 = 0;
      while (num9 < this.e.TotalPreviewPages)
      {
        float num10;
        float num11;
        if (num9 == 0)
        {
          num10 = pgWidth;
          num11 = pgHeight;
        }
        else
        {
          num10 = num1;
          num11 = num2;
        }
        int x1 = (int) ((double) num10 * (double) num6 / (double) num4);
        int x2 = (int) ((double) num11 * (double) num7 / (double) num5);
        rect1.left = num8;
        rect1.right = rect1.left + x1 - 1;
        rect1.top = y1;
        rect1.bottom = rect1.top + x2 - 1;
        this.TerSetClipRgn();
        COp.RECT rect2 = rect1;
        rect2.left += this.e.TerWinRect.left;
        rect2.right += this.e.TerWinRect.left;
        rect2.top += this.e.TerWinRect.top;
        rect2.bottom += this.e.TerWinRect.top;
        terGr.FillRectangle(Brushes.White, new Rectangle(rect1.left, rect1.top, rect1.right - rect1.left, rect1.bottom - rect1.top));
        this.PreviewOnePage(terGr, rect2, num3, true);
        GraphicsState gstate = terGr.Save();
        int num12 = this.MulDiv(x2, this.e.TerWinOrgY, (int) ((double) num11 * (double) this.e.UnitResY));
        if (this.e.TerWinHeight > x2)
          num12 = 0;
        int num13 = this.MulDiv(x1, this.e.TerWinOrgX, (int) ((double) num10 * (double) this.e.UnitResX));
        if (this.e.TerWinWidth > this.e.PreviewWidth)
          num13 = 0;
        this.e.PvX = num13;
        this.e.PvY = num12;
        if (num9 > 0)
        {
          COp.RECT rect3 = rect1 with { right = rect1.left };
          rect3.left = rect3.right - width;
          Brush brush2 = (Brush) new SolidBrush(this.e.TextDefBkColor);
          terGr.FillRectangle(brush2, new Rectangle(rect3.left, y1, width, this.e.PreviewHeight - 1));
          brush2.Dispose();
        }
        terGr.DrawRectangle(Pens.Black, new Rectangle(rect1.left, rect1.top, rect1.right - rect1.left - 1, rect1.bottom - rect1.top - 1));
        terGr.DrawLine(Pens.Black, rect1.right, rect1.top + 1, rect1.right, rect1.bottom);
        terGr.DrawLine(Pens.Black, rect1.left + 1, rect1.bottom, rect1.right, rect1.bottom);
        terGr.Restore(gstate);
        this.TerResetClipRgn();
        num8 += x1 + width;
        ++num9;
        ++num3;
      }
      if (this.e.TerArg.PrintView)
        this.e.CurPage = curPage;
      if (this.e.PvTlb != null)
      {
        string str;
        if (this.e.TotalPreviewPages == 1 || this.e.CurPage + 1 == this.e.TotalPages)
        {
          str = this.e.MsgString[216] + (this.e.CurPage + 1).ToString();
        }
        else
        {
          int num14 = this.e.CurPage + 1;
          int num15 = this.e.CurPage + 2;
          str = $"{this.e.MsgString[216]}{num14.ToString()}/{num15.ToString()}";
        }
        this.PvPageNo.Text = str;
      }
      int y2 = (int) ((double) num5 * (double) this.e.UnitResY);
      int num16 = this.MulDiv(this.e.TerWinHeight, y2, this.e.PreviewHeight);
      if (this.e.TerArg.ShowVerBar)
      {
        int num17 = 1000 - this.e.VerThumbSize;
        int num18 = y2 * this.e.TotalPages - num16;
        int num19;
        if (num18 > 0)
        {
          int num20 = y2 * this.e.CurPage + this.e.TerWinOrgY;
          num19 = num20 * num17 / num18;
          if (num20 >= num18 && num20 > 0)
            num19 = num17;
          if (num19 < 0)
            num19 = 0;
        }
        else
          num19 = 0;
        if (num19 != this.e.VerScrollPos)
        {
          this.e.VerScrollPos = num19;
          this.SetScrollPos(this.e.hTerWnd, 1, this.e.VerScrollPos, true);
        }
      }
      int y3 = (int) ((double) this.e.UnitResX * (double) pgWidth);
      if (this.e.TotalPreviewPages > 1)
        y3 += (int) ((double) this.e.UnitResX * ((double) num1 + 1.0));
      int num21 = this.MulDiv(this.e.TerWinWidth, y3, this.e.PreviewWidth);
      if (this.e.TerArg.ShowHorBar)
      {
        int num22 = 1000;
        int num23 = y3 - num21;
        COp.SCROLLINFO lpsi = new COp.SCROLLINFO();
        int num24 = this.e.TerWinWidth * 1000 / this.e.PreviewWidth;
        if (num24 < this.e.MinThumbHt)
          num24 = this.e.MinThumbHt;
        if (num24 > 1000)
          num24 = 1000;
        int num25 = num22 - num24;
        int num26;
        if (num23 > 0)
        {
          int terWinOrgX = this.e.TerWinOrgX;
          num26 = terWinOrgX * num25 / num23;
          if (terWinOrgX >= num23 && terWinOrgX > 0)
            num26 = num25;
          if (num26 < 0)
            num26 = 0;
        }
        else
          num26 = 0;
        lpsi.cbSize = 28;
        lpsi.fMask = 12;
        if (num24 > 0)
          lpsi.fMask |= 2;
        lpsi.nPage = num24 + 1;
        lpsi.nPos = num26;
        this.SetScrollInfo(this.e.hTerWnd, 0, ref lpsi, true);
        this.e.HorThumbSize = num24;
        this.e.HorScrollPos = num26;
      }
    }
    return true;
  }

  internal bool PostPreview()
  {
    Cursor cursor = (Cursor) null;
    if ((this.e.TerFlags & 512 /*0x0200*/) == 0)
    {
      cursor = this.e.Cursor;
      this.e.Cursor = Cursors.WaitCursor;
    }
    this.e.HoldMessages = true;
    bool paintEnabled = this.e.PaintEnabled;
    this.e.PaintEnabled = false;
    this.e.TerWinOrgX = this.e.TerWinOrgY = 0;
    this.e.TerArg.FittedView = this.e.SavePvFittedView;
    this.e.ShowPageBorder = this.e.SavePvPageBorder;
    if (!this.e.SavePvViewPageHdrFtr && this.e.ViewPageHdrFtr && this.e.SavePvPageMode)
      this.ToggleViewHdrFtr();
    if (!this.e.SavePvPageMode)
      this.TogglePageMode();
    if (!this.e.SavePvPrintView)
      this.e.TerArg.PrintView = this.e.TerArg.PageMode = false;
    if (!this.e.SavePvWordWrap)
      this.TurnOffWordWrap();
    this.e.UsingZoomFonts = false;
    this.e.InPrinting = false;
    this.e.HoldMessages = false;
    this.e.InPrintPreview = false;
    this.e.TextBorder = this.e.ParaShading = 0;
    this.e.TerWinOrgX = this.e.SavePvOrgX;
    this.e.TerWinOrgY = this.e.SavePvOrgY;
    this.draw.ResetTransform(this.e.TerGr);
    this.e.TerGr.TranslateTransform((float) (-this.e.TerWinOrgX + this.e.TerWinRect.left), (float) (-this.e.TerWinOrgY + this.e.TerWinRect.top));
    if (this.True(this.e.BufGr))
    {
      this.draw.ResetTransform(this.e.BufGr);
      this.e.BufGr.TranslateTransform((float) -this.e.TerWinOrgX, (float) -this.e.TerWinOrgY);
    }
    this.e.PaintEnabled = paintEnabled;
    this.e.PrtGr = (Graphics) null;
    this.e.WrapFlag = 4;
    if (this.e.HtmlMode)
      this.e.TerAdjustHtmlTable();
    if (cursor != (Cursor) null)
      this.e.Cursor = cursor;
    return true;
  }

  internal bool PrePreview(Graphics gr)
  {
    bool flag = false;
    Cursor cursor = (Cursor) null;
    if ((this.e.TerFlags & 512 /*0x0200*/) == 0)
    {
      cursor = this.e.Cursor;
      this.e.Cursor = Cursors.WaitCursor;
    }
    this.e.InPrinting = true;
    this.e.HoldMessages = true;
    this.e.InPrintPreview = true;
    bool paintEnabled = this.e.PaintEnabled;
    this.e.PaintEnabled = false;
    this.e.SavePvWordWrap = this.e.TerArg.WordWrap;
    if (!this.e.TerArg.WordWrap)
      this.TurnOnWordWrap();
    if (this.e.EditFootnoteText)
      this.ToggleFootnoteEdit(true);
    if (this.e.EditEndnoteText)
      this.ToggleFootnoteEdit(false);
    this.e.SavePvPrintView = this.e.TerArg.PrintView;
    this.e.SavePvPageMode = this.e.TerArg.PageMode;
    this.e.SavePvPageBorder = this.e.ShowPageBorder;
    this.e.SavePvFittedView = this.e.TerArg.FittedView;
    this.e.SavePvStatus = this.e.TerArg.ShowStatus;
    this.e.SavePvRuler = this.e.TerArg.ruler;
    this.e.SavePvToolbar = this.e.TerArg.ToolBar;
    this.e.SavePvToolBarHeight = this.e.ToolBarHeight;
    this.e.SavePvShowHorBar = this.e.TerArg.ShowHorBar;
    this.e.SavePvViewPageHdrFtr = this.e.ViewPageHdrFtr;
    this.e.SavePvOrgX = this.e.TerWinOrgX;
    this.e.SavePvOrgY = this.e.TerWinOrgY;
    this.e.SavePvFirstFramePage = this.e.FirstFramePage;
    this.e.SavePvLastFramePage = this.e.LastFramePage;
    this.RecreateSections();
    if (this.OpenCurPrinter(false))
    {
      if (this.e.TerArg.WordWrap && !this.e.TerArg.PrintView)
        this.e.TerArg.PrintView = true;
      if (this.e.TerArg.PrintView)
      {
        this.e.ShowPageBorder = false;
        if (!this.e.TerArg.PageMode)
          this.e.TerArg.PageMode = true;
        this.e.TerArg.FittedView = false;
        if (this.e.SavePvPageMode && this.e.EditPageHdrFtr)
          this.ToggleEditHdrFtr();
        if (this.e.SavePvPrintView && !this.e.ViewPageHdrFtr)
        {
          for (int index = 0; index < this.e.TotalSects; ++index)
          {
            if (this.e.TerSect1[index].hdr.FirstLine >= 0 || this.e.TerSect1[index].ftr.FirstLine >= 0 || this.e.TerSect1[index].fhdr.FirstLine >= 0 || this.e.TerSect1[index].fftr.FirstLine >= 0)
            {
              this.ToggleViewHdrFtr();
              break;
            }
          }
        }
      }
      this.e.PrtGr = gr;
      if (this.e.TerArg.PrintView && this.e.HtmlMode)
        this.e.TerAdjustHtmlTable();
      if (!this.e.TerArg.WordWrap)
        this.e.TotalPages = this.e.CurPage = 0;
      this.e.TerWinOrgX = this.e.TerWinOrgY = 0;
      this.e.PrtTextBkColor = this.e.PrtParaBkColor = this.e.PrtFrameBkColor = tc.CLR_WHITE;
      this.e.PrtGr = gr;
      flag = true;
    }
    this.e.HoldMessages = false;
    this.e.PaintEnabled = paintEnabled;
    if (cursor != (Cursor) null)
      this.e.Cursor = cursor;
    return flag;
  }

  internal new bool PreviewDown(bool page)
  {
    int y = (int) ((double) this.e.PvHeight * (double) this.e.UnitResY);
    int num = this.MulDiv(this.e.TerWinHeight, y, this.e.PreviewHeight);
    if (this.e.TerWinOrgY + num >= y)
    {
      if (this.e.CurPage + 1 >= this.e.TotalPages)
        return true;
      ++this.e.CurPage;
      this.e.CurLine = this.e.PageInfo[this.e.CurPage].FirstLine;
      this.e.CurCol = 0;
      this.e.TerWinOrgY = 0;
    }
    else
    {
      if (page)
        this.e.TerWinOrgY += num;
      else
        this.e.TerWinOrgY += num / 6;
      if (this.e.TerWinOrgY + num > y)
        this.e.TerWinOrgY = y - num;
    }
    this.PaintPrintPreview();
    return true;
  }

  internal new bool PreviewLeft(bool page)
  {
    int num = this.MulDiv(this.e.TerWinWidth, (int) ((double) this.e.UnitResX * (double) this.e.PvWidth), this.e.PreviewWidth);
    if (this.e.TerWinOrgX > 0)
    {
      if (page)
        this.e.TerWinOrgX -= num;
      else
        this.e.TerWinOrgX -= num / 6;
      if (this.e.TerWinOrgX < 0)
        this.e.TerWinOrgX = 0;
      this.PaintPrintPreview();
    }
    return true;
  }

  internal bool PreviewOnePage(Graphics gr, COp.RECT rect, int page, bool scale)
  {
    GraphicsState gstate = gr.Save();
    if (scale)
    {
      int pageSect = this.e.TerGetPageSect(page);
      this.e.PvX = rect.left;
      this.e.PvY = rect.top;
      this.e.PvExtWidth = (int) ((double) this.e.TerSect1[pageSect].PgWidth * (double) this.e.UnitResX);
      this.e.PvExtHeight = (int) ((double) this.e.TerSect1[pageSect].PgHeight * (double) this.e.UnitResY);
      this.e.PvVpWidth = rect.right - rect.left;
      this.e.PvVpHeight = rect.bottom - rect.top;
      this.draw.ResetTransform(gr);
      gr.ScaleTransform((float) this.e.PvVpWidth / (float) this.e.PvExtWidth, (float) this.e.PvVpHeight / (float) this.e.PvExtHeight, System.Drawing.Drawing2D.MatrixOrder.Append);
      gr.TranslateTransform((float) (this.e.PvX - this.MulDiv(this.e.TerWinOrgX, this.e.PvVpWidth, this.e.PvExtWidth)), (float) (this.e.PvY - this.MulDiv(this.e.TerWinOrgY, this.e.PvVpHeight, this.e.PvExtHeight)), System.Drawing.Drawing2D.MatrixOrder.Append);
    }
    Graphics prtGr = this.e.PrtGr;
    this.e.PrtGr = gr;
    this.PrintViewMode(this.e.PrtGr, -1, page);
    if (prtGr != null)
      this.e.PrtGr = prtGr;
    gr.Restore(gstate);
    return true;
  }

  internal new bool PreviewPageHorz(int pos)
  {
    int num1 = 1000 - this.e.HorThumbSize;
    int y = (int) ((double) this.e.UnitResX * (double) this.e.PvWidth);
    int num2 = this.MulDiv(this.e.TerWinWidth, y, this.e.PreviewWidth);
    if (pos < 2)
      this.e.TerWinOrgX = 0;
    else if (pos >= num1 - 2)
      this.e.TerWinOrgX = y - num2;
    else if (num1 > 0)
      this.e.TerWinOrgX = (y - num2) * pos / num1;
    this.PaintPrintPreview();
    return true;
  }

  internal new bool PreviewPageVert(int pos)
  {
    int num1 = 1000 - this.e.VerThumbSize;
    int curPage = this.e.CurPage;
    int y = (int) ((double) this.e.PvHeight * (double) this.e.UnitResY);
    int num2 = this.MulDiv(this.e.TerWinHeight, y, this.e.PreviewHeight);
    int num3 = y * this.e.TotalPages;
    int num4;
    int num5;
    if (pos >= num1 - 2)
    {
      num4 = this.e.TotalPages - 1;
      num5 = y - num2;
    }
    else
    {
      int num6 = num3 * pos / num1;
      int num7 = 0;
      for (num4 = 0; num4 < this.e.TotalPages - 1; ++num4)
      {
        int num8 = y;
        if (num7 + num8 <= num6)
          num7 += num8;
        else
          break;
      }
      num5 = num6 - num7;
    }
    if (num5 > y - num2)
      num5 = y - num2;
    if (num5 < 0)
      num5 = 0;
    this.e.CurPage = num4;
    if (this.e.CurPage != curPage)
    {
      this.e.CurLine = this.e.PageInfo[this.e.CurPage].FirstLine;
      this.e.CurCol = 0;
    }
    this.e.TerWinOrgY = num5;
    this.PaintPrintPreview();
    return true;
  }

  internal new bool PreviewRight(bool page)
  {
    int y = (int) ((double) this.e.UnitResX * (double) this.e.PvWidth);
    int num = this.MulDiv(this.e.TerWinWidth, y, this.e.PreviewWidth);
    if (this.e.TerWinOrgX + num < y)
    {
      if (page)
        this.e.TerWinOrgX += num;
      else
        this.e.TerWinOrgX += num / 6;
      if (this.e.TerWinOrgX + num > y)
        this.e.TerWinOrgX = y - num;
      this.PaintPrintPreview();
    }
    return true;
  }

  internal new bool PreviewUp(bool page)
  {
    int y = (int) ((double) this.e.PvHeight * (double) this.e.UnitResY);
    int num = this.MulDiv(this.e.TerWinHeight, y, this.e.PreviewHeight);
    if (this.e.TerWinOrgY == 0)
    {
      if (this.e.CurPage <= 0)
        return true;
      --this.e.CurPage;
      this.e.CurLine = this.e.PageInfo[this.e.CurPage].FirstLine;
      this.e.CurCol = 0;
      this.e.TerWinOrgY = y - num;
      if (this.e.TerWinOrgY < 0)
        this.e.TerWinOrgY = 0;
    }
    else
    {
      if (page)
        this.e.TerWinOrgY -= num;
      else
        this.e.TerWinOrgY -= num / 6;
      if (this.e.TerWinOrgY < 0)
        this.e.TerWinOrgY = 0;
    }
    this.PaintPrintPreview();
    return true;
  }

  internal bool PrintBkndPicture(Graphics gr, int PageNo)
  {
    int width = this.e.PrtFont[this.e.BkPictId].CharWidth[24];
    int height = this.e.PrtFont[this.e.BkPictId].height;
    if (this.e.BkPictId > 0)
    {
      int pWidth;
      int pHeight;
      int pHiddenX;
      int pHiddenY;
      this.e.TerGetPageOrient2(PageNo, out pWidth, out pHeight, out pHiddenX, out pHiddenY);
      int unitX = this.TwipsToUnitX(pWidth - 2 * pHiddenX);
      int unitY = this.TwipsToUnitY(pHeight - 2 * pHiddenY);
      int BeginY1;
      int BeginX1 = BeginY1 = 0;
      if (this.e.BkPictFlag == 1)
        this.PrintPicture(gr, this.e.BkPictId, BeginX1, BeginY1, unitX, unitY, 1);
      else if (this.e.BkPictFlag == 2)
      {
        int num1 = BeginX1 + unitX;
        int num2 = BeginY1 + unitY;
        for (int BeginY2 = BeginY1; BeginY2 < num2; BeginY2 += height)
        {
          int BeginX2 = BeginX1;
          while (BeginX2 < num1)
          {
            this.PrintPicture(gr, this.e.BkPictId, BeginX2, BeginY2, width, height, 1);
            BeginX2 += width;
            if (this.e.InPrintPreview && BeginX2 + width > num1)
              break;
          }
        }
      }
      else
        this.PrintPicture(gr, this.e.BkPictId, BeginX1, BeginY1, width, height, 1);
    }
    return true;
  }

  private bool PrintControl(Graphics gr, int pict, int x, int y, int width, int height)
  {
    int height1 = this.e.PrtFont[0].height;
    bool margin = true;
    bool flag = false;
    tc.ClsForm form = this.e.TerFont[pict].form;
    Control ctl = this.e.TerFont[pict].ctl;
    if (form != null && this.e.TerFont[pict].ctl != null)
    {
      if (this.IsFormField(pict, 2) && !form.border)
        margin = false;
      Pen pen;
      if (form.CtlClass == "TextBox")
      {
        pen = Pens.Black;
      }
      else
      {
        pen = new Pen(Color.Black, (float) this.TwipsToUnitX(15));
        flag = true;
      }
      int num1 = 0;
      int fontId = form.FontId;
      if (fontId < 0 || fontId >= this.e.TotalFonts)
        num1 = 0;
      Font font = this.e.PrtFont[0].font;
      if (form.CtlClass == "RadioButton")
      {
        int num2 = width * 9 / 10;
        int num3 = height * 9 / 10;
        int x1 = x + num2;
        int y1 = y + num3;
        int width1 = width - 2 * num2;
        int height2 = height - 2 * num3;
        gr.DrawEllipse(pen, x1, y1, width1, height2);
        if (((RadioButton) ctl).Checked)
          gr.FillEllipse(Brushes.Black, x1 + width1 / 3, y1 + height2 / 3, width1 / 3, height2 / 3);
      }
      else if (form.CtlClass == "Button")
      {
        gr.DrawRectangle(pen, x, y, width, height);
        string text = ctl.Text;
        this.PrintControlString(gr, font, text, x, y, width, height, HorizontalAlignment.Center, true, -1);
      }
      else if (margin)
        gr.DrawRectangle(pen, x, y, width, height);
      if (form.CtlClass == "CheckBox")
      {
        if (((CheckBox) ctl).Checked)
        {
          gr.DrawLine(pen, x, y, x + width, y + height);
          gr.DrawLine(pen, x, y + height, x + width, y);
        }
      }
      else if (form.CtlClass == "TextBox")
      {
        TextBox textBox = (TextBox) ctl;
        if (textBox.Multiline)
        {
          int y2 = y;
          int index = 0;
          string[] lines = textBox.Lines;
          for (int length = lines.Length; index < length && y2 + height1 < y + height; y2 += height1)
          {
            this.PrintControlString(gr, font, lines[index], x, y2, width, height1, textBox.TextAlign, margin, this.ScrToTwipsY(textBox.Height));
            ++index;
          }
        }
        else
        {
          string str = textBox.Text;
          if (textBox.PasswordChar != char.MinValue)
            str = new string('*', str.Length);
          this.PrintControlString(gr, font, str, x, y, width, height, textBox.TextAlign, margin, -1);
        }
      }
      else if (form.CtlClass == "ComboBox")
      {
        string text = ctl.Text;
        this.PrintControlString(gr, font, text, x, y, width, height, HorizontalAlignment.Left, true, -1);
      }
      else if (form.CtlClass == "ListBox")
      {
        ListBox listBox = (ListBox) ctl;
        int y3 = y;
        int count = listBox.Items.Count;
        int topIndex = listBox.TopIndex;
        for (int unitsY = this.OrigScrToUnitsY(listBox.GetItemHeight(0)); topIndex < count && y3 + unitsY < y + height; y3 += unitsY)
        {
          string str = (string) listBox.Items[topIndex];
          this.PrintControlString(gr, font, str, x, y3, width, unitsY, HorizontalAlignment.Left, true, -1);
          ++topIndex;
        }
      }
      if (flag)
        pen.Dispose();
    }
    return true;
  }

  private bool PrintControlString(
    Graphics gr,
    Font font,
    string str,
    int x,
    int y,
    int width,
    int FontHeight,
    HorizontalAlignment HorzAlign,
    bool margin,
    int CtlHeight)
  {
    COp.SIZE size;
    this.GetTextExtentPoint(gr, font, str, str.Length, out size);
    switch (HorzAlign)
    {
      case HorizontalAlignment.Right:
        int num1 = this.e.PrtFont[0].CharWidth[32 /*0x20*/];
        break;
      case HorizontalAlignment.Center:
        int num2 = (width - size.cx) / 2;
        break;
      default:
        if (margin)
        {
          int num3 = this.e.PrtFont[0].CharWidth[32 /*0x20*/];
          break;
        }
        break;
    }
    int num4 = (FontHeight - size.cy) / 2;
    Rectangle layoutRectangle = new Rectangle(x, y, width, CtlHeight > 0 ? CtlHeight : FontHeight);
    gr.DrawString(str, font, Brushes.Black, (RectangleF) layoutRectangle);
    return true;
  }

  internal bool PrintFrameBorder(
    Graphics gr,
    int FrameNo,
    int BorderIndex,
    int BeginX,
    int BeginY)
  {
    Point[] InPt = new Point[2];
    int PageNo = FrameNo >= this.e.FirstPage2Frame ? this.e.FirstFramePage + 1 : this.e.FirstFramePage;
    bool flag = true;
    COp.RECT rect;
    rect.left = BeginX;
    rect.right = rect.left + this.e.frame[FrameNo].width;
    rect.top = BeginY;
    rect.bottom = BeginY + this.e.frame[FrameNo].height;
    if (this.e.frame[FrameNo].CellId > 0 && (this.e.frame[FrameNo].flags & 131072 /*0x020000*/) != 0 && (BorderIndex == 2 || BorderIndex == 3))
      this.SwapInts(ref rect.left, ref rect.right);
    int x = this.e.frame[FrameNo].BorderWidth[BorderIndex];
    int num1;
    if (BorderIndex == 0 || BorderIndex == 1)
    {
      num1 = this.TwipsToUnitY(x);
      if (this.e.InPrintPreview && num1 > 0)
      {
        int cellId;
        if ((cellId = this.e.frame[FrameNo].CellId) > 0)
        {
          int num2;
          InPt[1].X = num2 = BeginX;
          InPt[0].X = num2;
          InPt[0].Y = BeginY;
          if (BorderIndex == 1)
            InPt[0].Y += this.e.frame[FrameNo].height - num1;
          InPt[1].Y = InPt[0].Y + num1;
          this.LPtoDP(this.e.PrtGr, InPt);
          if (InPt[1].Y <= InPt[0].Y)
          {
            if (BorderIndex == 0)
              InPt[1].Y = InPt[0].Y + 1;
            else if (this.GetSameColumnCell(cellId, true) <= 0 || this.IsPageLastRow(-cellId, PageNo))
              InPt[1].Y = InPt[0].Y + 1;
            if (InPt[1].Y > InPt[0].Y)
            {
              this.DPtoLP(this.e.PrtGr, InPt);
              num1 = (InPt[1].Y - InPt[0].Y) * 5 / 4;
            }
          }
        }
        else
        {
          int num3;
          InPt[0].Y = num3 = 0;
          int num4;
          InPt[1].X = num4 = num3;
          InPt[0].X = num4;
          InPt[1].Y = 1;
          this.DPtoLP(this.e.PrtGr, InPt);
          int num5 = Math.Abs((InPt[1].Y - InPt[0].Y) * 5 / 4);
          if (num1 < num5)
            num1 = num5;
        }
      }
    }
    else
    {
      num1 = this.TwipsToUnitX(x);
      if (this.e.InPrintPreview && num1 > 0)
      {
        int cellId;
        if ((cellId = this.e.frame[FrameNo].CellId) > 0)
        {
          int num6;
          InPt[1].Y = num6 = BeginY;
          InPt[0].Y = num6;
          InPt[0].X = BorderIndex != 2 ? rect.right - num1 : rect.left;
          InPt[1].X = InPt[0].X + num1;
          this.LPtoDP(this.e.PrtGr, InPt);
          if (InPt[1].X <= InPt[0].X)
          {
            if (BorderIndex == 2)
              InPt[1].X = InPt[0].X + 1;
            else if (this.e.cell[cellId].NextCell <= 0)
              InPt[1].X = InPt[0].X + 1;
            if (InPt[1].X > InPt[0].X)
            {
              this.DPtoLP(this.e.PrtGr, InPt);
              num1 = (InPt[1].X - InPt[0].X) * 5 / 4;
            }
          }
        }
        else
        {
          int num7;
          InPt[0].X = num7 = 0;
          int num8;
          InPt[1].Y = num8 = num7;
          InPt[0].Y = num8;
          InPt[1].X = 1;
          this.DPtoLP(this.e.PrtGr, InPt);
          int num9 = Math.Abs((InPt[1].X - InPt[0].X) * 5 / 4);
          if (num1 < num9)
            num1 = num9;
        }
      }
    }
    if (num1 > 0)
    {
      int num10 = num1;
      switch (BorderIndex)
      {
        case 0:
          rect.bottom = rect.top + num10;
          break;
        case 1:
          rect.top = rect.bottom - num10;
          break;
        case 2:
          rect.right = rect.left + num10 + 1;
          break;
        default:
          rect.left = rect.right - num10;
          ++rect.right;
          break;
      }
      Color lineColor = this.e.frame[FrameNo].BorderColor[BorderIndex];
      int paraFrameId = this.e.frame[FrameNo].ParaFrameId;
      if (paraFrameId > 0 && (this.e.ParaFrame[paraFrameId].flags & 131200 /*0x020080*/) != 0)
        lineColor = this.e.ParaFrame[paraFrameId].LineColor;
      if (flag || (this.e.TerOpFlags2 & 32 /*0x20*/) != 0)
      {
        Pen pen = new Pen(lineColor);
        pen.Width = (float) num1;
        if (BorderIndex == 0 || BorderIndex == 1)
          this.e.PrtGr.DrawLine(pen, rect.left, rect.top + num1 / 2, rect.right, rect.top + num1 / 2);
        else
          this.e.PrtGr.DrawLine(pen, rect.left + num1 / 2, rect.top, rect.left + num1 / 2, rect.bottom);
        pen.Dispose();
      }
      else
        this.OurFillRectangle(this.e.PrtGr, lineColor, rect);
    }
    return true;
  }

  internal bool PrintFrameBorderShading(Graphics gr, int FrameNo, int BeginX, int BeginY)
  {
    Color color1 = tc.CLR_WHITE;
    bool flag1 = false;
    int cellId = this.e.frame[FrameNo].CellId;
    int num1 = this.e.ScrFrameAngle;
    Color color2 = this.e.frame[FrameNo].BackColor;
    COp.RECT rect;
    rect.left = BeginX;
    rect.right = rect.left + this.e.frame[FrameNo].width;
    rect.top = BeginY;
    rect.bottom = rect.top + this.e.frame[FrameNo].height;
    if (this.e.frame[FrameNo].CellId > 0)
      num1 = 0;
    if (num1 > 0)
      this.FrameRotateRect(ref rect, this.e.CurFrame);
    bool flag2 = cellId > 0 && color2 != this.PageColor();
    int paraFrameId = this.e.frame[FrameNo].ParaFrameId;
    if ((this.e.ParaFrame[paraFrameId].flags & 131200 /*0x020080*/) != 0 && !flag2)
    {
      if (this.e.ParaFrame[paraFrameId].FillPattern > 0)
      {
        color1 = this.e.ParaFrame[paraFrameId].BackColor;
        flag1 = true;
      }
    }
    else
    {
      if (this.e.HtmlMode && cellId > 0)
        color2 = tc.CLR_WHITE;
      this.e.FrameShading = this.e.frame[FrameNo].shading;
      this.e.PaintFrameFlags = this.e.frame[FrameNo].flags;
      if (color2 != this.PageColor() || (this.e.PaintFrameFlags & 1024 /*0x0400*/) != 0)
      {
        int frameShading = this.e.FrameShading > 0 ? this.e.FrameShading : 0;
        int red = (int) color2.R * (100 - frameShading) / 100;
        int num2 = (int) color2.G * (100 - frameShading) / 100;
        int num3 = (int) color2.B * (100 - frameShading) / 100;
        int green = num2;
        int blue = num3;
        color1 = Color.FromArgb(red, green, blue);
      }
      else
      {
        int num4 = this.e.FrameShading * (int) byte.MaxValue / 100;
        if (num4 > (int) byte.MaxValue)
          num4 = (int) byte.MaxValue;
        int num5 = ~num4 & (int) byte.MaxValue;
        color1 = Color.FromArgb(num5, num5, num5);
      }
      flag1 = !this.IsSameColor(color1, tc.CLR_WHITE);
    }
    if (flag1)
    {
      this.OurFillRectangle(this.e.PrtGr, color1, rect);
      this.e.PrtFrameBkColor = color1;
    }
    else
      this.e.PrtFrameBkColor = tc.CLR_WHITE;
    if (this.e.HtmlMode && this.e.frame[FrameNo].CellId > 0 && (this.e.TerFlags3 & 8) == 0)
    {
      int x1 = BeginX;
      int y1 = BeginY;
      int x2 = BeginX + this.e.frame[FrameNo].width;
      int y2 = BeginY + this.e.frame[FrameNo].height;
      this.e.CurFrame = FrameNo;
      if (this.True(this.e.frame[FrameNo].border))
        this.DrawHtmlCellBorder(this.e.PrtGr, this.e.frame[FrameNo].CellId, x1, y1, x2, y2, this.e.frame[FrameNo].BorderWidth, false);
    }
    else
    {
      if ((this.e.frame[FrameNo].border & 1) != 0)
        this.PrintFrameBorder(this.e.PrtGr, FrameNo, 0, BeginX, BeginY);
      if ((this.e.frame[FrameNo].border & 2) != 0)
        this.PrintFrameBorder(this.e.PrtGr, FrameNo, 1, BeginX, BeginY);
      if ((this.e.frame[FrameNo].border & 4) != 0)
        this.PrintFrameBorder(this.e.PrtGr, FrameNo, 2, BeginX, BeginY);
      if ((this.e.frame[FrameNo].border & 8) != 0)
        this.PrintFrameBorder(this.e.PrtGr, FrameNo, 3, BeginX, BeginY);
    }
    return true;
  }

  internal new bool PrintOneLine(
    int LineNo,
    Graphics gr,
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
    char ch = char.MinValue;
    int num1 = 0;
    bool flag1 = false;
    int num2 = 0;
    int num3 = -1;
    int num4 = 0;
    bool flag2 = (this.e.TerOpFlags2 & 8) != 0;
    COp.RECT pRect;
    if (len != 0)
    {
      ushort index1;
      ushort CurFont = index1 = (ushort) 0;
      int num5;
      int idx = num5 = 0;
      int num6 = num5;
      int num7 = num5;
      if (CurTabw != null && (CurTabw.type & 128 /*0x80*/) != 0)
        flag1 = true;
      if (!flag2)
      {
        this.e.CurScrSeg = 0;
        this.e.ScrRtl = (this.e.text[LineNo].flags2 & 32 /*0x20*/) != 0;
        int pTotalLineSeg = this.e.TotalScrSeg;
        if (this.e.ScrRtl)
          this.e.pScrSeg = this.GetLineSeg(LineNo, pWidth, out pTotalLineSeg);
        this.e.TotalScrSeg = pTotalLineSeg;
        if (this.e.pScrSeg == null)
          this.e.ScrRtl = false;
      }
      int num8;
      bool flag3;
      if (this.e.ScrRtl)
      {
        if (this.LineInfo(LineNo, 1024 /*0x0400*/) && this.e.text[LineNo].tabw.FrameCharPos == 0 && (this.e.TerOpFlags & 8) == 0)
          RowBeginX -= this.e.text[LineNo].tabw.FrameSpaceWidth;
        if (RowBeginX > 0)
        {
          for (int index2 = 0; index2 < this.e.TotalScrSeg; ++index2)
            this.e.pScrSeg[index2].x += RowBeginX;
        }
        this.e.CurScrSeg = this.GetCharSeg(LineNo, 0, this.e.TotalScrSeg, this.e.pScrSeg);
        num8 = this.e.pScrSeg[this.e.CurScrSeg].col + this.e.pScrSeg[this.e.CurScrSeg].count;
        flag3 = this.e.pScrSeg[this.e.CurScrSeg].rtl;
      }
      else
      {
        this.e.CurScrSeg = 0;
        flag3 = false;
        num8 = 9999;
      }
      for (int index3 = 0; index3 <= len; ++index3)
      {
        if (index3 < len)
          index1 = fmt[index3];
        if (index3 == len || (int) index1 != (int) CurFont || ch == '\t' && this.e.TerFont[(int) CurFont].height != 0 || ch == '\u0006' || ch == '\u001C' || ch == '\u0004' || index3 >= num8 || this.e.ScrRtl & flag3 && (index3 > 0 && ptr[index3 - 1] == ' ' || ptr[index3] == ' ') || flag1 && ptr[index3] == ' ' && this.JustifySpace((int) CurFont) || index3 > 0 && CurTabw != null && (CurTabw.type & 1024 /*0x0400*/) != 0 && CurTabw.FrameCharPos == index3)
        {
          int len1 = num7;
          if (ch == '\t')
            --len1;
          if (index3 == len && ch == '\u0006')
            ch = '-';
          if (ch == '\u0006' || ch == '\u001C' || ch == '\u0004')
            --len1;
          if (len1 > 0)
          {
            this.e.PrtTextBkColor = this.e.TerFont[(int) CurFont].TextBkColor;
            if (this.e.TerFont[(int) CurFont].TextBkColor != tc.CLR_WHITE)
            {
              pRect.left = BeginX;
              pRect.right = pRect.left + num6;
              pRect.top = BeginY + SpcBef;
              pRect.bottom = pRect.top + height;
              if (this.e.ScrRtl)
                this.RtlRect(ref pRect, this.e.CurFrame, this.e.pScrSeg[this.e.CurScrSeg]);
              this.OurFillRectangle(this.e.PrtGr, this.e.TerFont[(int) CurFont].TextBkColor, pRect);
            }
            if ((this.e.TerFont[(int) CurFont].style & 128 /*0x80*/) == 0 || this.e.TerFont[(int) CurFont].ParaFID <= 0)
              this.PrintOneSegment(this.e.PrtGr, (int) CurFont, BeginX + num2, BeginY + SpcBef, this.CopyArray(ptr, idx), len1, BaseHeight, ExtLead, 0, this.CopyArray(pWidth, idx));
            if (num4 > 0)
            {
              int num9 = this.e.UnitResY / this.e.ScrResY;
              this.DrawShadowLine(this.e.PrtGr, num3 + num9, BeginY + num9, num4 - num9, BeginY + num9, Pens.Black, (Pen) null);
              this.DrawShadowLine(this.e.PrtGr, num3 + num9, BeginY + height - 2 * num9, num4 - num9, BeginY + height - 2 * num9, Pens.Black, (Pen) null);
              this.DrawShadowLine(this.e.PrtGr, num3 + num9, BeginY + num9, num3 + num9, BeginY + height - 2 * num9, Pens.Black, (Pen) null);
              this.DrawShadowLine(this.e.PrtGr, num4 - num9, BeginY + num9, num4 - num9, BeginY + height - 2 * num9, Pens.Black, (Pen) null);
              num3 = -1;
              num4 = 0;
            }
          }
          idx += num7;
          BeginX += num6;
          if (index3 > 0 && CurTabw != null && (CurTabw.type & 1024 /*0x0400*/) != 0 && CurTabw.FrameCharPos == index3)
            BeginX += this.ScrToUnitX(CurTabw.FrameScrWidth);
          if (ch == '\t')
          {
            int num10 = (int) pWidth[index3 - 1];
            if (this.e.TerFont[(int) CurFont].TextBkColor != tc.CLR_WHITE)
            {
              pRect.left = BeginX;
              pRect.right = pRect.left + num10;
              pRect.top = BeginY + SpcBef;
              pRect.bottom = pRect.top + height;
              this.OurFillRectangle(this.e.PrtGr, this.e.TerFont[(int) CurFont].TextBkColor, pRect);
            }
            int tabId = this.e.PfmtId[CurPara].TabId;
            int CurPos = BeginX;
            if (this.e.TerArg.PageMode)
              CurPos -= LeftMargin + this.e.frame[FrameNo].x + this.e.frame[FrameNo].SpaceLeft;
            byte pFlags;
            this.GetTabPos(this.e.text[LineNo].pfmt, this.e.TerTab[tabId], CurPos, out tc.SkipInt, out tc.SkipInt, out pFlags, false);
            this.e.TabFlags = pFlags;
            int num11 = BeginY + SpcBef + BaseHeight + ExtLead;
            if (this.e.TabFlags == (byte) 1)
              this.DrawDottedLine(this.e.PrtGr, BeginX, num11, BeginX + num10, num11, Color.Black);
            else if (this.e.TabFlags == (byte) 2)
            {
              Pen SolidPen = new Pen(Color.Black);
              SolidPen.DashStyle = DashStyle.Dot;
              this.DrawShadowLine(this.e.PrtGr, BeginX, num11 - 2, BeginX + num10, num11 - 2, SolidPen, (Pen) null);
              SolidPen.Dispose();
            }
            else if (this.e.TabFlags == (byte) 4 || (this.e.TerFont[(int) CurFont].style & 257) != 0)
            {
              Pen SolidPen = (this.e.TerFont[(int) CurFont].style & 0) != 0 || !this.IsSameColor(this.e.TerFont[(int) CurFont].UlineColor, tc.CLR_AUTO) ? new Pen(this.e.TerFont[(int) CurFont].UlineColor) : new Pen(Color.Black);
              int num12 = this.PointsToUnitY(1) / 2;
              if (num12 < 1)
                num12 = 1;
              SolidPen.Width = (float) num12;
              int num13 = num11 + 2 * num12;
              if ((this.e.TerFont[(int) CurFont].style & 256 /*0x0100*/) != 0)
                num13 -= num12;
              this.DrawShadowLine(this.e.PrtGr, BeginX, num13, BeginX + num10, num13, SolidPen, (Pen) null);
              if ((this.e.TerFont[(int) CurFont].style & 256 /*0x0100*/) != 0)
              {
                int num14 = num13 + 2 * num12;
                this.DrawShadowLine(this.e.PrtGr, BeginX, num14, BeginX + num10, num14, SolidPen, (Pen) null);
              }
              SolidPen.Dispose();
            }
            this.e.TabFlags = (byte) 0;
            BeginX += num10;
            ++num1;
          }
          num7 = num6 = 0;
          num2 = 0;
          this.e.CharBoxLeft = this.e.CharBoxRight = false;
          CurFont = index1;
          if (this.e.ScrRtl && this.e.pScrSeg != null && index3 >= num8 && this.e.CurScrSeg + 1 < this.e.TotalScrSeg)
          {
            ++this.e.CurScrSeg;
            num8 = this.e.pScrSeg[this.e.CurScrSeg].col + this.e.pScrSeg[this.e.CurScrSeg].count;
            flag3 = this.e.pScrSeg[this.e.CurScrSeg].rtl;
          }
        }
        if (index3 != len)
        {
          ch = ptr[idx + num7];
          if (ch != '\t')
          {
            bool flag4 = false;
            bool flag5 = false;
            int num15 = 0;
            if (CurTabw != null && idx + num7 < CurTabw.CharFlagsLen && ((int) CurTabw.CharFlags[idx + num7] & 1) != 0)
              flag4 = true;
            if (CurTabw != null && idx + num7 < CurTabw.CharFlagsLen && ((int) CurTabw.CharFlags[idx + num7] & 2) != 0 || index3 == len - 1 && num3 != -1)
              flag5 = true;
            if (flag4)
            {
              num3 = BeginX + num6;
              if (num7 == 0)
                num2 = this.e.ExtraSpacePrtX;
            }
            if ((this.e.TerFont[(int) index1].style & 128 /*0x80*/) == 0 || this.e.TerFont[(int) index1].ParaFID <= 0)
              num15 += (int) pWidth[index3];
            num6 += num15;
            if (flag4)
            {
              ushort[] numArray;
              IntPtr index4;
              (numArray = pWidth)[(int) (index4 = (IntPtr) index3)] = (ushort) ((uint) numArray[(int) index4] - (uint) (ushort) this.e.ExtraSpacePrtX);
            }
            if (flag5)
            {
              ushort[] numArray;
              IntPtr index5;
              (numArray = pWidth)[(int) (index5 = (IntPtr) index3)] = (ushort) ((uint) numArray[(int) index5] - (uint) (ushort) this.e.ExtraSpacePrtX);
              num4 = BeginX + num6;
            }
          }
          ++num7;
        }
        else
          break;
      }
    }
    if ((this.e.TextBorder & 65776 /*0x0100F0*/) != 0)
    {
      pRect.left = this.e.BoxLeft;
      pRect.right = this.e.BoxRight;
      if (this.True(LineNo) && this.e.text[LineNo].fid == 0)
      {
        int x = 20;
        if ((this.e.PfmtId[this.e.text[LineNo].pfmt].flags & 512 /*0x0200*/) != 0)
          x += 20;
        if ((this.e.PfmtId[this.e.text[LineNo].pfmt].flags & 256 /*0x0100*/) != 0)
          x += 40;
        pRect.left -= this.PointsToUnitX(x);
        pRect.right += this.PointsToUnitX(x);
      }
      if (this.True(LineNo) && this.e.text[LineNo].cid > 0)
        pRect.right -= this.PointsToUnitX(1);
      pRect.top = BeginY + ExtLead;
      pRect.bottom = BeginY + height + SpcBef + SpcAft;
      this.e.ParaBoxColor = this.e.PfmtId[this.e.text[LineNo].pfmt].BorderColor;
      this.DrawParaBorder(this.e.PrtGr, pRect, true);
      this.e.TextBorder = this.e.ParaShading = 0;
    }
    if (HasBullet)
      this.DrawBullet(this.e.PrtGr, LineNo, this.e.text[LineNo].pfmt, BulletX, BeginY + SpcBef + ExtLead, BaseHeight, true);
    if (this.e.SectLine >= 0)
      this.DrawSectLineNbr(gr, LineNo - this.e.frame[FrameNo].PageFirstLine, SectLineX, BeginY + SpcBef + ExtLead, BaseHeight);
    if ((this.e.text[LineNo].flags2 & 192 /*0xC0*/) != 0)
      this.DrawTrackingLine(gr, SectLineX, BeginY, height);
    this.e.CharBoxLeft = this.e.CharBoxRight = false;
    if ((this.e.TerOpFlags2 & 8) == 0)
    {
      this.e.pScrSeg = (tc.StrLineSeg[]) null;
      this.e.ScrRtl = false;
    }
    return true;
  }

  private void PrintOnePage(object sender, PrintPageEventArgs ev)
  {
    Graphics graphics = ev.Graphics;
    this.DisplayStatusInfo();
    this.e.PrtGr = graphics;
    graphics.PageUnit = GraphicsUnit.Pixel;
    this.GetPrinterHiddenArea(graphics);
    this.e.PrtResX = (int) graphics.DpiX;
    this.e.PrtResY = (int) graphics.DpiY;
    if ((this.e.TerOpFlags & 131072 /*0x020000*/) != 0)
    {
      this.e.PrtVpX = this.MulDiv(this.e.MPRect.left, this.e.PrtResX, 1440) - this.e.HiddenX;
      this.e.PrtVpY = this.MulDiv(this.e.MPRect.top, this.e.PrtResY, 1440) - this.e.HiddenY;
    }
    else
    {
      this.e.PrtVpX = -this.e.HiddenX;
      this.e.PrtVpY = -this.e.HiddenY;
    }
    this.draw.ResetTransform(graphics);
    graphics.ScaleTransform((float) this.e.PrtResX / 1440f, (float) this.e.PrtResY / 1440f, System.Drawing.Drawing2D.MatrixOrder.Append);
    graphics.TranslateTransform((float) this.e.PrtVpX, (float) this.e.PrtVpY, System.Drawing.Drawing2D.MatrixOrder.Append);
    this.PrintViewMode(this.e.PrtGr, -1, this.e.PrtCurPage);
    this.DeleteOpGr(graphics);
    this.e.PrtGr = (Graphics) null;
    this.e.PrtResX = this.e.PrtResY = 1440;
    ev.HasMorePages = this.SetNextPage();
  }

  internal bool PrintOneSegment(
    Graphics gr,
    int CurFont,
    int BeginX,
    int BeginY,
    char[] ptr,
    int len,
    int BaseHeight,
    int ExtLead,
    int JustAdj,
    ushort[] pWidth)
  {
    int curFmtId = this.e.CurFmtId;
    Color curForeColor = this.e.CurForeColor;
    Color curBackColor = this.e.CurBackColor;
    if (len > 0)
    {
      BeginY = BeginY + BaseHeight - this.e.PrtFont[CurFont].BaseHeight + ExtLead;
      if ((this.e.TerFont[CurFont].style & 128 /*0x80*/) != 0)
      {
        int width = this.e.PrtFont[CurFont].CharWidth[24];
        int height = this.e.PrtFont[CurFont].height;
        this.PrintPicture(this.e.PrtGr, CurFont, BeginX, BeginY, width, height, len);
      }
      else if (!this.edit.HiddenText(CurFont))
      {
        IntPtr hFont = this.OurSetFont(this.e.PrtGr, this.e.PrtFont[CurFont].hFont);
        this.SetBkMode(this.e.PrtGr, 1);
        Color color1 = this.e.TerFont[CurFont].TextColor;
        if (this.True(this.e.TerFont[CurFont].InsRev))
          color1 = this.e.reviewer[this.e.TerFont[CurFont].InsRev].InsColor;
        if (this.True(this.e.TerFont[CurFont].DelRev))
          color1 = this.e.reviewer[this.e.TerFont[CurFont].DelRev].DelColor;
        if (color1 == tc.CLR_AUTO || this.e.HtmlMode && color1 == Color.White)
        {
          Color color2 = this.e.PrtTextBkColor;
          if (color2 == tc.CLR_WHITE)
            color2 = this.e.PrtParaBkColor;
          if (color2 == tc.CLR_WHITE)
            color2 = this.e.PrtFrameBkColor;
          color1 = this.ReverseColor(color2);
        }
        if (this.True(this.e.ParaShading))
        {
          int num1 = this.MulDiv(this.e.ParaShading, (int) byte.MaxValue, 10000);
          if (num1 > (int) byte.MaxValue)
            num1 = (int) byte.MaxValue;
          int num2 = num1 << 16 /*0x10*/ | num1 << 8 | num1;
          if (color1 == this.e.TerFont[0].TextColor)
            color1 = this.ToColor(this.ToColorRef(color1) ^ num2);
        }
        this.e.CurForeColor = color1;
        this.SetTextColor(this.e.PrtGr, color1);
        char[] txt = new char[len + 1];
        int index1;
        for (index1 = 0; index1 < len; ++index1)
        {
          txt[index1] = ptr[index1];
          if (ptr[index1] == '\u000E')
            txt[index1] = ' ';
          if (ptr[index1] == '\u0017')
            txt[index1] = '-';
          if (ptr[index1] == '\u0006')
            txt[index1] = '-';
        }
        txt[index1] = char.MinValue;
        int count = len;
        if ((this.e.TerFont[CurFont].style & 196608 /*0x030000*/) != 0)
        {
          for (int index2 = 0; index2 < count; ++index2)
          {
            if (char.IsLower(txt[index2]))
              txt[index2] = char.ToUpper(txt[index2]);
          }
        }
        int[] pDX = new int[count + 1];
        for (int index3 = 0; index3 < count; ++index3)
          pDX[index3] = (int) pWidth[index3];
        int index4 = 0;
        int num3 = 0;
        for (; index4 < count; ++index4)
          num3 += pDX[index4];
        COp.RECT rect;
        rect.left = BeginX;
        rect.right = rect.left + num3;
        rect.top = BeginY + this.e.PrtFont[CurFont].BaseHeightAdj;
        rect.bottom = rect.top + this.e.PrtFont[CurFont].height;
        this.e.CurFmtId = CurFont;
        int y = BeginY + this.e.PrtFont[CurFont].BaseHeightAdj;
        this.OurExtTextOut(this.e.PrtGr, BeginX, y, 0, rect, txt, count, pDX);
        if (this.e.blk.IsDoubleStrikedOut && txt != null && txt.Length != 0)
        {
          using (Pen SolidPen = new Pen(this.GetTextColor(gr)))
          {
            SolidPen.DashStyle = DashStyle.Solid;
            this.DrawShadowLine(gr, rect.left, y + (rect.bottom - rect.top) / 2 - 2, rect.right, y + (rect.bottom - rect.top) / 2 - 2, SolidPen, (Pen) null);
            this.DrawShadowLine(gr, rect.left, y + (rect.bottom - rect.top) / 2 + 2, rect.right, y + (rect.bottom - rect.top) / 2 + 2, SolidPen, (Pen) null);
          }
        }
        this.OurSetFont(this.e.PrtGr, hFont);
        if ((this.e.TerFont[CurFont].style & 257) != 0 && (this.e.TerFont[CurFont].style & 48 /*0x30*/) == 0)
        {
          int width = this.PointsToUnitY(1) / 2;
          if (width < 1)
            width = 1;
          Pen SolidPen = !this.IsSameColor(this.e.TerFont[CurFont].UlineColor, tc.CLR_AUTO) ? new Pen(this.e.TerFont[CurFont].UlineColor, (float) width) : new Pen(this.e.TerFont[CurFont].TextColor, (float) width);
          int num4 = BeginY + this.e.PrtFont[CurFont].BaseHeight + width;
          if ((this.e.TerFont[CurFont].style & 1) != 0)
            num4 += width;
          int x2 = BeginX + num3;
          this.DrawShadowLine(this.e.PrtGr, BeginX - JustAdj, num4, x2, num4, SolidPen, (Pen) null);
          if ((this.e.TerFont[CurFont].style & 256 /*0x0100*/) != 0)
          {
            int num5 = num4 + 2 * width;
            this.DrawShadowLine(this.e.PrtGr, BeginX - JustAdj, num5, x2, num5, SolidPen, (Pen) null);
          }
          SolidPen.Dispose();
        }
      }
    }
    this.e.CurFmtId = curFmtId;
    this.e.CurForeColor = curForeColor;
    this.e.CurBackColor = curBackColor;
    return true;
  }

  internal bool PrintPageBknd(Graphics gr, int PageNo)
  {
    int index = 0;
    while (index < this.e.TotalFrames && (this.e.frame[index].flags & 65536 /*0x010000*/) == 0)
      ++index;
    int x;
    int y;
    int num1;
    int num2;
    if (index < this.e.TotalFrames)
    {
      this.GetPrinterHiddenArea(gr);
      x = this.e.frame[index].x - this.e.HiddenX;
      y = this.e.frame[index].y - this.e.HiddenY;
      num1 = this.e.frame[index].width;
      num2 = this.e.frame[index].height;
    }
    else
    {
      int pWidth;
      int pHeight;
      int pHiddenX;
      int pHiddenY;
      this.e.TerGetPageOrient2(PageNo, out pWidth, out pHeight, out pHiddenX, out pHiddenY);
      num1 = pWidth - 2 * pHiddenX;
      num2 = pHeight - 2 * pHiddenY;
      x = pHiddenX;
      y = pHiddenY;
    }
    COp.RECT OurRect;
    this.SetRect(out OurRect, x, y, x + num1, y + num2);
    this.OurFillRectangle(gr, this.e.PageBkColor, OurRect);
    return true;
  }

  internal bool PrintParaShading(
    Graphics gr,
    int line,
    int FrameNo,
    int ParaBeginX,
    int ParaLastX,
    int BeginY,
    int height)
  {
    COp.RECT rect;
    rect.left = ParaBeginX;
    rect.right = ParaLastX;
    rect.top = BeginY;
    rect.bottom = rect.top + height;
    rect.top += this.e.ParaSpcBef;
    rect.bottom -= this.e.ParaSpcAft;
    this.e.PrtParaBkColor = tc.CLR_WHITE;
    if (this.e.ParaBackColor != tc.CLR_WHITE)
      this.e.PrtParaBkColor = this.e.ParaBackColor;
    if (this.True(this.e.ParaShading) && this.e.ParaBackColor == tc.CLR_WHITE)
    {
      int num1 = this.e.ParaShading * (int) byte.MaxValue / 10000;
      if (num1 > (int) byte.MaxValue)
        num1 = (int) byte.MaxValue;
      int num2 = num1 << 16 /*0x10*/ | num1 << 8 | num1;
      this.e.PrtParaBkColor = this.ToColor(this.ToColorRef(this.e.PrtParaBkColor) ^ num2);
    }
    else if (this.e.ParaShading != 10000 && this.e.ParaShading != 0)
    {
      byte num = (byte) ((int) byte.MaxValue * this.e.ParaShading / 10000);
      Color prtParaBkColor = this.e.PrtParaBkColor;
      int r = (int) prtParaBkColor.R;
      int g = (int) prtParaBkColor.G;
      int b = (int) prtParaBkColor.B;
      this.e.PrtParaBkColor = Color.FromArgb(r < (int) num ? 0 : r - (int) num, g < (int) num ? 0 : g - (int) num, b < (int) num ? 0 : b - (int) num);
    }
    if (this.e.PrtParaBkColor != tc.CLR_WHITE)
      this.OurFillRectangle(this.e.PrtGr, this.e.PrtParaBkColor, rect);
    return true;
  }

  internal bool PrintPicture(
    Graphics gr,
    int CurFont,
    int BeginX,
    int BeginY,
    int width,
    int height,
    int count)
  {
    GraphicsState gstate = (GraphicsState) null;
    int DestX = BeginX;
    int DestY = BeginY;
    int num1 = width;
    int num2 = height;
    bool flag = this.True(this.e.TerFont[CurFont].CropLeft) || this.True(this.e.TerFont[CurFont].CropRight) || this.True(this.e.TerFont[CurFont].CropTop) || this.True(this.e.TerFont[CurFont].CropBot);
    if (flag && this.e.TerFont[CurFont].OrigPictWidth != 0)
    {
      float num3 = (float) this.e.TerFont[CurFont].PictWidth / (float) (this.e.TerFont[CurFont].OrigPictWidth - this.e.TerFont[CurFont].CropLeft - this.e.TerFont[CurFont].CropRight);
      int x = (int) ((double) this.e.TerFont[CurFont].CropLeft * (double) num3);
      DestX -= this.TwipsToUnitY(x);
      num1 = this.TwipsToUnitX((int) ((double) this.e.TerFont[CurFont].OrigPictWidth * (double) num3));
    }
    if (flag && this.e.TerFont[CurFont].OrigPictHeight != 0)
    {
      float num4 = (float) this.e.TerFont[CurFont].PictHeight / (float) (this.e.TerFont[CurFont].OrigPictHeight - this.e.TerFont[CurFont].CropTop - this.e.TerFont[CurFont].CropBot);
      int x = (int) ((double) this.e.TerFont[CurFont].CropTop * (double) num4);
      DestY -= this.TwipsToUnitY(x);
      num2 = this.TwipsToUnitY((int) ((double) this.e.TerFont[CurFont].OrigPictHeight * (double) num4));
    }
    if (this.e.ScrFrameAngle > 0 && tc.OSCanRotate)
      this.FrameNoRotateDC(this.e.PrtGr);
    if (this.e.TerFont[CurFont].PictType == 0)
    {
      this.FixImageDest(ref DestX, ref DestY, num1, num2);
      if (flag)
      {
        gstate = gr.Save();
        Region region = new Region(new Rectangle(BeginX, BeginY, width, height));
        gr.Clip = region;
      }
      Image image1 = this.e.TerFont[CurFont].image;
      if (this.e.TerFont[CurFont].ImageType == ImageFormat.Wmf.Guid && this.e.TerFont[CurFont].hMeta != IntPtr.Zero)
      {
        IntPtr opDc = this.GetOpDC(gr);
        int StateId = COp.Win32.SaveDC(opDc);
        COp.Win32.SetMapMode(opDc, 8);
        COp.Win32.SetWindowExtEx(opDc, this.MulDiv(this.e.TerFont[CurFont].bmWidth, this.e.OrigScrResX, 1440), this.MulDiv(this.e.TerFont[CurFont].bmHeight, this.e.OrigScrResY, 1440), IntPtr.Zero);
        COp.Win32.SetViewportExtEx(opDc, num1 * this.e.PrtVwExtCX / this.e.PrtWinExtCX, num2 * this.e.PrtVwExtCY / this.e.PrtWinExtCY, IntPtr.Zero);
        COp.Win32.SetViewportOrgEx(opDc, this.OurLPtoDP(DestX, this.e.PrtWinExtCX, this.e.PrtVwExtCX, this.e.PrtWinOrgX, this.e.PrtVwOrgX), this.OurLPtoDP(DestY, this.e.PrtWinExtCY, this.e.PrtVwExtCY, this.e.PrtWinOrgY, this.e.PrtVwOrgY), IntPtr.Zero);
        COp.Win32.SetWindowOrgEx(opDc, 0, 0, IntPtr.Zero);
        COp.Win32.PlayMetaFile(opDc, this.e.TerFont[CurFont].hMeta);
        COp.Win32.RestoreDC(opDc, StateId);
        this.ReleaseOpDC(gr);
      }
      else if (this.e.TerFont[CurFont].ParaFID == this.e.WmParaFID && this.e.WmImageAttr != null)
      {
        Image image2 = this.e.TerFont[CurFont].image;
        Graphics graphics = gr;
        Image image3 = image1;
        Rectangle destRect = new Rectangle(DestX, DestY, num1, num2);
        Size size = image2.Size;
        int width1 = size.Width;
        size = image2.Size;
        int height1 = size.Height;
        ImageAttributes wmImageAttr = this.e.WmImageAttr;
        graphics.DrawImage(image3, destRect, 0, 0, width1, height1, GraphicsUnit.Pixel, wmImageAttr);
      }
      else if (this.e.TerFont[CurFont].ImageType == ImageFormat.Emf.Guid && (this.e.TerFlags4 & 536870912 /*0x20000000*/) != 0)
      {
        int y = 300;
        int width2 = this.MulDiv(num1, y, this.e.UnitResX);
        int height2 = this.MulDiv(num2, y, this.e.UnitResY);
        Bitmap bitmap = new Bitmap(width2, height2, gr);
        Graphics graphics = Graphics.FromImage((Image) bitmap);
        graphics.DrawImage(image1, new Rectangle(0, 0, width2, height2));
        gr.DrawImage((Image) bitmap, new Rectangle(DestX, DestY, num1, num2));
        graphics.Dispose();
        bitmap.Dispose();
      }
      else
        gr.DrawImage(image1, new Rectangle(DestX, DestY, num1, num2));
      if (flag)
        gr.Restore(gstate);
    }
    else if (this.IsControl(CurFont))
      this.PrintControl(this.e.PrtGr, CurFont, BeginX, BeginY, width, height);
    if (this.e.ScrFrameAngle > 0 && tc.OSCanRotate)
      this.FrameRotateDC(this.e.PrtGr, this.e.CurFrame);
    return true;
  }

  internal int PrintViewMode(Graphics gr, int FromLine, int ToLine)
  {
    int num1 = 0;
    int num2 = 0;
    ushort index1 = 0;
    bool flag1 = false;
    int pBulletWidth = 0;
    int num3 = 9999;
    int index2;
    int index3;
    if (FromLine < 0)
    {
      index3 = index2 = ToLine;
      if (index3 >= this.e.TotalPages)
        return 0;
      flag1 = true;
    }
    else
    {
      index3 = 0;
      while (index3 < this.e.TotalPages && this.e.PageInfo[index3].FirstLine <= FromLine)
        ++index3;
      if (index3 > 0)
        --index3;
      index2 = 0;
      while (index2 < this.e.TotalPages && this.e.PageInfo[index2].FirstLine <= ToLine)
        ++index2;
      if (index2 > 0)
        --index2;
      if (index2 < index3)
        index2 = index3;
    }
    int num4 = this.e.FramesSorted ? 1 : 2;
    for (int index4 = index3; index4 <= index2; ++index4)
    {
      this.e.CurPage = index4;
      this.CreateFrames(true, this.e.CurPage, this.e.CurPage);
      bool flag2 = true;
      int num5 = this.e.FramesSorted ? 1 : 2;
      num1 = 0;
      if (this.e.PageBkColor != tc.CLR_WHITE)
        this.PrintPageBknd(gr, index4);
      if ((this.e.TerFlags3 & 8388608 /*0x800000*/) != 0)
        this.PrintBkndPicture(gr, index4);
      for (int index5 = 0; index5 < num5; ++index5)
      {
        for (int index6 = 0; index6 < this.e.TotalFrames; ++index6)
        {
          if ((this.e.frame[index6].flags & 2097152 /*0x200000*/) == 0 | flag2)
          {
            this.e.PrtTextBkColor = this.e.PrtParaBkColor = this.e.PrtFrameBkColor = tc.CLR_WHITE;
            int FrameNo = !this.e.FramesSorted ? index6 : this.e.frame[index6].DispFrame;
            int paraFrameId = this.e.frame[FrameNo].ParaFrameId;
            if ((this.e.FramesSorted || (index5 != 0 || paraFrameId <= 0) && (index5 != 1 || paraFrameId != 0)) && (this.e.frame[FrameNo].CellId <= 0 || (this.e.cell[this.e.frame[FrameNo].CellId].flags & 16 /*0x10*/) == 0 || (this.e.frame[FrameNo].flags & 32768 /*0x8000*/) != 0))
            {
              int sect = this.e.frame[FrameNo].sect;
              int LeftMargin = (int) ((double) this.e.TerSect[sect].LeftMargin * (double) this.e.UnitResX);
              if (flag2)
              {
                num2 = !this.e.ViewPageHdrFtr ? (int) ((double) this.e.TerSect[sect].TopMargin * (double) this.e.UnitResY) : 0;
                if (this.e.WmParaFID > 0)
                {
                  for (int index7 = 0; index7 < this.e.TotalFrames; ++index7)
                  {
                    if ((this.e.frame[index7].flags & 2097152 /*0x200000*/) != 0)
                    {
                      int pict = this.e.ParaFrame[this.e.frame[index7].ParaFrameId].pict;
                      if (pict > 0 && (this.e.TerFont[pict].style & 128 /*0x80*/) != 0)
                      {
                        int width = this.e.PrtFont[pict].CharWidth[24];
                        int height = this.e.PrtFont[pict].height;
                        this.PrintPicture(gr, pict, this.e.frame[index7].x + LeftMargin, this.e.frame[index7].y + num2, width, height, 1);
                        break;
                      }
                    }
                  }
                }
              }
              flag2 = false;
              if (this.False(this.e.frame[FrameNo].empty) && (this.e.frame[FrameNo].flags & 2056) != 0)
              {
                int pageFirstLine = this.e.frame[FrameNo].PageFirstLine;
                if (((pageFirstLine >= FromLine && pageFirstLine <= ToLine || FromLine < 0 || (this.e.PfmtId[this.e.text[pageFirstLine].pfmt].flags & 12288 /*0x3000*/) != 0 ? 1 : (!this.True(this.e.text[pageFirstLine].cid) ? 0 : ((this.e.TableRow[this.e.cell[this.e.text[pageFirstLine].cid].row].flags & 4) != 0 ? 1 : 0))) | (flag1 ? 1 : 0)) != 0)
                {
                  this.e.frame[FrameNo].x += LeftMargin;
                  this.e.frame[FrameNo].y += num2;
                  if ((this.e.ParaFrame[paraFrameId].flags & 8388608 /*0x800000*/) != 0 && this.e.ParaFrame[paraFrameId].FillPict > 0)
                    this.PrintPicture(gr, this.e.ParaFrame[paraFrameId].FillPict, this.e.frame[FrameNo].x, this.e.frame[FrameNo].y, this.e.frame[FrameNo].width, this.e.frame[FrameNo].height, 1);
                  else if ((this.e.ParaFrame[paraFrameId].flags & 256 /*0x0100*/) != 0)
                    this.DrawLineObject(gr, FrameNo);
                  else if ((this.e.ParaFrame[paraFrameId].flags & 512 /*0x0200*/) != 0)
                    this.DrawRectObject(gr, FrameNo);
                  else if (this.e.ParaFrame[paraFrameId].pict > 0)
                  {
                    this.PrintPicture(gr, this.e.ParaFrame[paraFrameId].pict, this.e.frame[FrameNo].x, this.e.frame[FrameNo].y, this.e.frame[FrameNo].width, this.e.frame[FrameNo].height, 1);
                    this.DrawPictRect(gr, FrameNo);
                  }
                }
              }
              else
              {
                this.e.PrtLeftMarg = LeftMargin;
                this.e.PrtTopMarg = num2;
                if ((this.e.frame[FrameNo].flags & 8192 /*0x2000*/) != 0)
                {
                  this.e.CurFrame = FrameNo;
                  this.e.frame[FrameNo].y += num2;
                  this.DrawFootnote(this.e.PrtGr, FrameNo, false);
                }
                else if ((this.e.frame[FrameNo].flags & 65536 /*0x010000*/) != 0)
                {
                  this.DrawPageBorderBox(this.e.PrtGr, FrameNo);
                }
                else
                {
                  int BeginX = this.e.frame[FrameNo].x + LeftMargin;
                  int num6;
                  int BeginY = num6 = this.e.frame[FrameNo].y + num2;
                  this.e.CurFrame = FrameNo;
                  if (!this.e.frame[this.e.CurFrame].empty)
                    this.e.ScrFrameAngle = this.LineTextAngle(-FrameNo);
                  if (this.e.ScrFrameAngle > 0 && tc.OSCanRotate)
                    this.FrameRotateDC(gr, this.e.CurFrame);
                  this.PrintFrameBorderShading(gr, FrameNo, BeginX, BeginY);
                  if (!this.e.frame[FrameNo].empty)
                  {
                    int num7 = BeginY + this.e.frame[FrameNo].SpaceTop;
                    int width = this.e.frame[FrameNo].width;
                    if (this.e.TerSect[sect].columns > 1 && (this.e.frame[FrameNo].flags & 512 /*0x0200*/) == 0 && this.e.frame[FrameNo].CellId == 0 && this.e.frame[FrameNo].ParaFrameId == 0 && (this.e.PfmtId[this.e.text[this.e.frame[FrameNo].PageFirstLine].pfmt].flags & 12288 /*0x3000*/) == 0)
                      width -= (int) ((double) this.e.TerSect[sect].ColumnSpace * (double) this.e.UnitResX);
                    this.e.SectLine = this.GetSectDisplayLine(FrameNo);
                    for (int pageFirstLine = this.e.frame[FrameNo].PageFirstLine; pageFirstLine <= this.e.frame[FrameNo].PageLastLine; ++pageFirstLine)
                    {
                      if (this.TableLevel(pageFirstLine) == this.e.frame[FrameNo].level)
                      {
                        if ((this.e.TerFlags3 & 33554432 /*0x02000000*/) == 0 || this.e.frame[FrameNo].CellId <= 0 || num7 + this.e.text[pageFirstLine].height <= num6 + this.e.frame[FrameNo].height)
                        {
                          num7 += this.GetObjSpcBef(pageFirstLine, false);
                          if ((!this.True(this.e.text[pageFirstLine].tabw) || (this.e.text[pageFirstLine].tabw.type & 12) == 0) && this.e.text[pageFirstLine].len != 0 && this.e.text[pageFirstLine].height != 0)
                          {
                            bool pHasBullet = false;
                            int pfmt = this.e.text[pageFirstLine].pfmt;
                            int flags = this.e.PfmtId[pfmt].flags;
                            int unitX = this.TwipsToUnitX(this.e.PfmtId[pfmt].RightIndentTwips);
                            int num8;
                            int num9 = num8 = this.e.frame[FrameNo].SpaceLeft + this.TwipsToUnitX(this.e.PfmtId[pfmt].LeftIndentTwips);
                            if (pageFirstLine == 0 || (this.e.text[pageFirstLine].flags & 4) != 0)
                              num9 += this.GetFirstIndent(pageFirstLine, out pBulletWidth, out pHasBullet, false);
                            if (this.e.PfmtId[pfmt].FirstIndentTwips < 0)
                              num8 += this.TwipsToUnitX(this.e.PfmtId[pfmt].FirstIndentTwips);
                            int num10 = num8 + BeginX;
                            this.e.ParaShading = this.e.PfmtId[pfmt].shading;
                            this.e.ParaBackColor = this.e.PfmtId[pfmt].BkColor;
                            this.e.ParaBoxSpace = this.e.PfmtId[pfmt].BorderSpace;
                            this.e.TextBorder = 0;
                            this.e.BoxLeft = this.e.BoxRight = 0;
                            if ((flags & 65776 /*0x0100F0*/) != 0 && this.e.text[pageFirstLine].fid == 0)
                            {
                              if ((flags & 16 /*0x10*/) != 0 && (pageFirstLine == this.e.frame[FrameNo].PageFirstLine || !this.HasSameParaBorder(pageFirstLine - 1, pageFirstLine)))
                                this.e.TextBorder |= 16 /*0x10*/;
                              if ((flags & 32 /*0x20*/) != 0 && (pageFirstLine >= this.e.frame[FrameNo].PageLastLine || !this.HasSameParaBorder(pageFirstLine + 1, pageFirstLine)))
                                this.e.TextBorder |= 32 /*0x20*/;
                              if ((flags & 64 /*0x40*/) != 0)
                                this.e.TextBorder |= 64 /*0x40*/;
                              if ((flags & 128 /*0x80*/) != 0)
                                this.e.TextBorder |= 128 /*0x80*/;
                              if ((flags & 65536 /*0x010000*/) != 0)
                                this.e.TextBorder |= 65536 /*0x010000*/;
                              if ((flags & 256 /*0x0100*/) != 0)
                                this.e.TextBorder |= 256 /*0x0100*/;
                              if ((flags & 512 /*0x0200*/) != 0)
                                this.e.TextBorder |= 512 /*0x0200*/;
                            }
                            int num11 = 50;
                            if ((this.e.text[pageFirstLine].flags & 256 /*0x0100*/) != 0)
                              num11 = -25;
                            if (this.True(paraFrameId) && (this.e.ParaFrame[paraFrameId].flags & 896) == 0)
                              num11 = 0;
                            if (this.True(this.e.text[pageFirstLine].cid))
                              num11 = 0;
                            this.e.BoxLeft = BeginX + this.TwipsToUnitX(this.e.PfmtId[pfmt].LeftIndentTwips - num11);
                            if (this.e.PfmtId[pfmt].FirstIndentTwips < 0)
                              this.e.BoxLeft += this.TwipsToUnitX(this.e.PfmtId[pfmt].FirstIndentTwips);
                            this.e.BoxRight = BeginX + width - this.TwipsToUnitX(this.e.PfmtId[pfmt].RightIndentTwips - num11);
                            if (this.True(this.e.TextBorder) && this.True(this.e.text[pageFirstLine].tabw) && this.e.text[pageFirstLine].tabw.FrameX > 0 && Math.Abs(this.e.text[pageFirstLine].tabw.FrameX + this.e.text[pageFirstLine].tabw.FrameWidth - this.e.BoxRight) < this.e.UnitResX)
                              this.e.BoxRight = this.e.text[pageFirstLine].tabw.FrameX;
                            int len = this.e.text[pageFirstLine].len;
                            char[] txt = this.e.text[pageFirstLine].txt;
                            ushort[] numArray = this.OpenCfmt(pageFirstLine);
                            tc.ClsTabw CurTabw = !this.True(this.e.text[pageFirstLine].tabw) ? new tc.ClsTabw() : this.e.text[pageFirstLine].tabw.Copy();
                            bool flag3 = false;
                            int num12 = num9;
                            int num13;
                            int ExtLead = num13 = 0;
                            int BaseHeight = num13;
                            int num14 = num13;
                            if (len > 0 && (this.e.text[pageFirstLine].flags & 2048 /*0x0800*/) != 0)
                              --len;
                            int num15 = len;
                            if (len > 0 && ((int) txt[len - 1] == (int) this.e.ParaChar || (int) txt[len - 1] == (int) this.e.CellChar || txt[len - 1] == '\u0012' || txt[len - 1] == '\u0016' || txt[len - 1] == '\u0014' || txt[len - 1] == '\u000F'))
                            {
                              --num15;
                              flag3 = true;
                            }
                            while (num15 > 0 && txt[num15 - 1] == ' ')
                              --num15;
                            if (num15 > 0)
                              len = num15;
                            bool flag4;
                            bool flag5 = flag4 = false;
                            int index8 = 0;
                            int num16 = len;
                            int x1;
                            int x2 = x1 = num3;
                            ushort[] lineCharWidth = this.GetLineCharWidth(pageFirstLine);
                            for (int index9 = 0; index9 <= len; ++index9)
                            {
                              if (index9 < len)
                                index1 = numArray[index9];
                              if (index9 < num16)
                              {
                                if (!flag4 && (txt[index9] == ' ' || txt[index9] == '\t') && this.e.TerFont[(int) index1].height > 0)
                                  flag4 = true;
                                if (txt[index9] != ' ' || index9 == num16 - 1 && !flag5)
                                {
                                  int num17 = this.e.PrtFont[(int) index1].height - this.e.PrtFont[(int) index1].BaseHeight - this.e.PrtFont[(int) index1].ExtLead;
                                  if (this.e.PrtFont[(int) index1].OffsetVal > 0)
                                    num17 -= this.e.PrtFont[(int) index1].OffsetVal;
                                  if (num17 > num14)
                                    num14 = num17;
                                  if (this.e.PrtFont[(int) index1].BaseHeight > BaseHeight)
                                    BaseHeight = this.e.PrtFont[(int) index1].BaseHeight;
                                  if (this.e.PrtFont[(int) index1].ExtLead > ExtLead)
                                    ExtLead = this.e.PrtFont[(int) index1].ExtLead;
                                  int offsetVal = this.e.PrtFont[(int) index1].OffsetVal;
                                  if (offsetVal == 0)
                                    x2 = x1 = 0;
                                  else if (offsetVal > 0 && offsetVal < x1)
                                    x1 = offsetVal;
                                  else if (offsetVal < 0 && -offsetVal < x2)
                                    x2 = -offsetVal;
                                  flag5 = true;
                                }
                              }
                              if (index9 < num15)
                                num12 += (int) lineCharWidth[index9];
                            }
                            if (x1 == num3)
                              x1 = 0;
                            if (x2 == num3)
                              x2 = 0;
                            if (this.True(x1) && this.True(x2))
                              x1 = x2 = 0;
                            int num18 = BaseHeight + num14 + ExtLead - x1 - x2;
                            if (this.True(x2))
                              BaseHeight -= x2;
                            if (num18 == 0 && flag4 | flag3)
                            {
                              num18 = this.e.PrtFont[index8].height;
                              BaseHeight = this.e.PrtFont[index8].BaseHeight;
                              ExtLead = this.e.PrtFont[index8].ExtLead;
                            }
                            char[] ptr = new char[len + 10];
                            ushort[] fmt = new ushort[len + 10];
                            int index10;
                            for (index10 = 0; index10 < len; ++index10)
                            {
                              ptr[index10] = txt[index10];
                              fmt[index10] = numArray[index10];
                            }
                            ptr[index10] = char.MinValue;
                            if (len > 0 && ((int) ptr[len - 1] == (int) this.e.ParaChar || (int) ptr[len - 1] == (int) this.e.CellChar || ptr[len - 1] == '\u000F' || ptr[len - 1] == '\u0016' || ptr[len - 1] == '\u0012'))
                            {
                              --len;
                              ptr[len] = char.MinValue;
                            }
                            int num19 = 0;
                            if (len > 0 || num12 > 0)
                            {
                              int num20 = 0;
                              int num21 = 0;
                              int num22 = width;
                              if (this.e.frame[FrameNo].CellId > 0 && this.e.cell[this.e.frame[FrameNo].CellId].TextAngle != 0)
                                num22 = this.e.frame[FrameNo].height;
                              if ((this.e.text[pageFirstLine].flags & 536870912 /*0x20000000*/) != 0 && this.True(this.e.text[pageFirstLine].tabw) && this.e.text[pageFirstLine].tabw.count > 0)
                                num20 = this.e.text[pageFirstLine].tabw.width[0];
                              else if ((flags & 1) != 0)
                                num20 = (num22 - unitX - this.e.frame[FrameNo].SpaceRight - num12) / 2;
                              else if ((flags & 2) != 0)
                                num20 = num22 - unitX - this.e.frame[FrameNo].SpaceRight - num12;
                              if ((CurTabw.type & 1024 /*0x0400*/) != 0 && CurTabw.FrameCharPos == 0)
                                num21 = this.ScrToUnitX(CurTabw.FrameScrWidth);
                              num19 = num20 > num21 ? num20 : num21;
                            }
                            int SpcBef;
                            int SpcAft;
                            int pParaSpcBef;
                            int pParaSpcAft;
                            this.GetLineSpacing2(pageFirstLine, num18, out SpcBef, out SpcAft, out pParaSpcBef, out pParaSpcAft, false);
                            this.e.SpaceBef = SpcBef;
                            this.e.SpaceAft = SpcAft;
                            this.e.ParaSpcBef = pParaSpcBef;
                            this.e.ParaSpcAft = pParaSpcAft;
                            if (pageFirstLine >= FromLine && pageFirstLine <= ToLine || FromLine < 0 || (this.e.PfmtId[this.e.text[pageFirstLine].pfmt].flags & 12288 /*0x3000*/) != 0 || this.True(this.e.text[pageFirstLine].cid) && (this.e.TableRow[this.e.cell[this.e.text[pageFirstLine].cid].row].flags & 4) != 0)
                            {
                              if (this.True(this.e.ParaShading) || !this.IsSameColor(this.e.ParaBackColor, this.e.PageBkColor))
                                this.PrintParaShading(gr, pageFirstLine, FrameNo, this.e.BoxLeft, this.e.BoxRight, num7 + ExtLead, num18 + SpcBef + SpcAft - ((this.e.TextBorder & 32 /*0x20*/) != 0 ? this.PointsToUnitY(1) : 0));
                              else
                                this.e.PrtParaBkColor = tc.CLR_WHITE;
                              int RowBeginX = num9 + num19;
                              int BulletX = BeginX + num9 + num19 - pBulletWidth;
                              int SectLineX = BeginX + this.e.frame[FrameNo].SpaceLeft;
                              this.PrintOneLine(pageFirstLine, gr, BeginX + RowBeginX, num7, ptr, fmt, len, num18, BaseHeight, CurTabw, pfmt, SpcBef, SpcAft, ExtLead, FrameNo, LeftMargin, RowBeginX, lineCharWidth, pHasBullet, BulletX, SectLineX);
                              this.SetPdfBkm(gr, pageFirstLine, index4, num7);
                            }
                            num7 += this.e.text[pageFirstLine].height;
                            if (num7 > num1)
                              num1 = num7;
                            this.CloseCfmt(pageFirstLine);
                          }
                        }
                        else
                          break;
                      }
                    }
                    if (this.e.ScrFrameAngle > 0 && tc.OSCanRotate)
                      this.FrameNoRotateDC(gr);
                    this.e.ScrFrameAngle = 0;
                    this.e.SectLine = -1;
                  }
                }
              }
            }
          }
        }
      }
    }
    if (this.e.ScrFrameAngle > 0 && tc.OSCanRotate)
      this.FrameNoRotateDC(gr);
    this.e.ScrFrameAngle = 0;
    return num1;
  }

  private void PvDlgClosed(object sender, EventArgs ev)
  {
    if (this.e.InPrintPreview && !this.e.DlgPreviewDone)
    {
      this.e.PrtCurPage = 0;
      this.TerSetPrintPreview(false);
      this.e.DlgPreviewDone = true;
    }
    this.e.PvDlg = (PrintPreviewDialog) null;
  }

  private void PvDlgLoad(object sender, EventArgs ev) => this.misc.CenterDlgBox((Form) sender);

  private void PvDlgPrintOnePage(object sender, PrintPageEventArgs ev)
  {
    Graphics graphics = ev.Graphics;
    Rectangle pageBounds = ev.PageBounds;
    int prtCurPage = this.e.PrtCurPage;
    int z = 100;
    if (this.e.PrtCurPage == 0)
    {
      this.TerSetPrintPreview(true);
      this.PrtLastPage = this.e.TotalPages - 1;
    }
    int pageSect = this.e.TerGetPageSect(prtCurPage);
    this.e.PvX = pageBounds.X;
    this.e.PvY = pageBounds.Y;
    this.e.PvExtWidth = (int) ((double) this.e.TerSect1[pageSect].PgWidth * (double) this.e.UnitResX);
    this.e.PvExtHeight = (int) ((double) this.e.TerSect1[pageSect].PgHeight * (double) this.e.UnitResY);
    this.e.PvVpWidth = pageBounds.Width;
    this.e.PvVpHeight = pageBounds.Height;
    GraphicsState gstate = graphics.Save();
    this.draw.ResetTransform(graphics);
    graphics.ScaleTransform((float) this.e.PvVpWidth / (float) this.e.PvExtWidth, (float) this.e.PvVpHeight / (float) this.e.PvExtHeight, System.Drawing.Drawing2D.MatrixOrder.Append);
    graphics.TranslateTransform((float) this.e.PvX, (float) this.e.PvY, System.Drawing.Drawing2D.MatrixOrder.Append);
    this.e.PvVpWidth = this.MulDiv(this.e.PvVpWidth, (int) graphics.DpiX, z);
    this.e.PvVpHeight = this.MulDiv(this.e.PvVpHeight, (int) graphics.DpiY, z);
    this.TerPrintPreview(graphics, pageBounds, prtCurPage, false);
    graphics.Restore(gstate);
    ev.HasMorePages = this.SetNextPage();
    if (ev.HasMorePages)
      return;
    this.e.PrtCurPage = 0;
    this.TerSetPrintPreview(false);
    this.e.DlgPreviewDone = true;
  }

  internal void PvTlbPaint(object Sender, PaintEventArgs ev)
  {
    Graphics graphics = ev.Graphics;
    Rectangle clientRectangle = this.e.PvTlb.ClientRectangle;
    Brush brush = (Brush) new SolidBrush(this.e.StatusBkColor);
    graphics.FillRectangle(brush, clientRectangle);
    brush.Dispose();
    graphics.DrawLine(Pens.Black, 0, 0, clientRectangle.Width, 0);
    graphics.DrawLine(Pens.White, 0, 1, clientRectangle.Width, 1);
    graphics.DrawLine(Pens.Black, 0, clientRectangle.Height - 1, clientRectangle.Width, clientRectangle.Height - 1);
  }

  internal void PvTlbPvCloseClick(object Sender, EventArgs ev)
  {
    this.PostMessage(this.e.hTerWnd, 2737, 717, 0);
  }

  internal void PvTlbPvPagesClick(object Sender, EventArgs ev)
  {
    if (this.e.TotalPreviewPages == 1)
      this.e.TotalPreviewPages = 2;
    else
      this.e.TotalPreviewPages = 1;
    this.PvPages.Text = this.e.TotalPreviewPages == 1 ? "Two Page" : "One Page";
    this.PaintPrintPreview();
  }

  internal void PvTlbPvPrintClick(object Sender, EventArgs ev)
  {
    this.e.GoPrintPreview = true;
    this.PostMessage(this.e.hTerWnd, 2737, 717, 0);
    this.PostMessage(this.e.hTerWnd, 2737, 643, 0);
  }

  internal void PvZoomEnterPressed(Control Sender)
  {
    this.e.PreviewZoom = this.ToInt(Sender.Text);
    this.PaintPrintPreview();
  }

  internal void PvZoomSelectedIndexChanged(object Sender, EventArgs ev)
  {
    TlbComboBox tlbComboBox = (TlbComboBox) Sender;
    if (tlbComboBox.locked)
      return;
    this.e.PreviewZoom = this.zoom[tlbComboBox.SelectedIndex];
    this.PaintPrintPreview();
  }

  private void QueryPageSettings(object sender, QueryPageSettingsEventArgs ev)
  {
    int prtCurPage = this.e.PrtCurPage;
    PageSettings pageSettings = ev.PageSettings;
    bool flag = false;
    int pageSect = this.e.TerGetPageSect(prtCurPage);
    pageSettings.Landscape = !this.e.TerSect[pageSect].IsPortrait;
    PaperSourceKind paperSourceKind = prtCurPage == 0 || this.e.PageInfo[prtCurPage - 1].TopSect != this.e.PageInfo[prtCurPage].TopSect ? this.e.TerSect[pageSect].FirstPageBin : this.e.TerSect[pageSect].bin;
    foreach (PaperSource paperSource in this.e.CurPrt.PaperSources)
    {
      if (paperSource.Kind == paperSourceKind)
      {
        pageSettings.PaperSource = paperSource;
        break;
      }
    }
    int width = (int) ((double) this.e.TerSect[pageSect].PprWidth * 100.0);
    int height = (int) ((double) this.e.TerSect[pageSect].PprWidth * 100.0);
    foreach (PaperSize paperSiz in this.e.CurPrt.PaperSizes)
    {
      if (paperSiz.Kind == this.e.TerSect[pageSect].PprKind)
      {
        pageSettings.PaperSize = paperSiz;
        flag = true;
        break;
      }
    }
    if (flag && (!flag || pageSettings.PaperSize.Kind != PaperKind.Custom))
      return;
    pageSettings.PaperSize = new PaperSize("Custom", width, height);
  }

  internal bool SetNextPage()
  {
    if (this.PrtCollate)
    {
      ++this.e.PrtCurPage;
      if (this.e.PrtCurPage > this.PrtLastPage)
      {
        ++this.PrtCurCopy;
        this.e.PrtCurPage = this.PrtFirstPage;
      }
      if (this.PrtCurCopy >= this.PrtCopies)
        return false;
    }
    else
    {
      ++this.PrtCurCopy;
      if (this.PrtCurCopy >= this.PrtCopies)
      {
        ++this.e.PrtCurPage;
        this.PrtCurCopy = 0;
      }
      if (this.e.PrtCurPage > this.PrtLastPage)
        return false;
    }
    return true;
  }

  internal bool SetPdfBkm(Graphics gr, int line, int page, int y)
  {
    int num = 0;
    tc.StrPdfBkm strPdfBkm = new tc.StrPdfBkm();
    if ((this.e.TerOpFlags2 & 16384 /*0x4000*/) != 0)
    {
      int styId = this.e.PfmtId[this.e.text[line].pfmt].StyId;
      if (styId == 0)
        return true;
      bool flag = true;
      int length1 = "heading ".Length;
      string strA = this.e.StyleId[styId].name;
      if (strA.Length > length1)
        strA = strA.Substring(0, length1);
      if (string.Compare(strA, "heading ", true) != 0)
        flag = false;
      if (flag)
      {
        int length2 = "heading ".Length;
        if (this.e.StyleId[styId].name.Length > length2 + 1)
        {
          switch (this.e.StyleId[styId].name[length2 + 1])
          {
            case ' ':
            case ',':
              break;
            default:
              flag = false;
              break;
          }
        }
        if (flag)
          num = (int) this.e.StyleId[styId].name[length2] - 49;
      }
      if (!flag && this.e.StyleId[styId].OutlineLevel >= 0)
      {
        flag = true;
        num = this.e.StyleId[styId].OutlineLevel;
      }
      if (!flag)
        return true;
      strPdfBkm.init();
      strPdfBkm.sign[0] = (byte) 84;
      strPdfBkm.sign[1] = (byte) 69;
      strPdfBkm.level = num;
      strPdfBkm.page = page;
      strPdfBkm.y = y / 20;
      char[] txt = this.e.text[line].txt;
      ushort[] numArray = this.OpenCfmt(line);
      int index1 = this.e.text[line].len;
      if (index1 > 0 && txt[index1] < ' ')
        --index1;
      if (index1 > 99)
        index1 = 99;
      int index2 = index1 - 1;
      while (index2 >= 0 && txt[index2] < ' ')
        --index2;
      if (index1 > 99)
        index1 = 99;
      char[] chArray = new char[index1 + 1];
      int length3 = 0;
      for (int index3 = 0; index3 < index1; ++index3)
      {
        if ((length3 != 0 || txt[index3] >= ' ' && txt[index3] != ' ') && !this.edit.HiddenText((int) numArray[index3]))
        {
          chArray[length3] = txt[index3];
          ++length3;
        }
      }
      chArray[length3] = char.MinValue;
      strPdfBkm.name = new string(chArray, 0, length3);
      this.CloseCfmt(line);
      if (length3 > 0)
        gr.AddMetafileComment(strPdfBkm.ToByteArray());
    }
    return true;
  }

  internal new bool SetSectPageSize()
  {
    for (int index = 0; index < this.e.TotalSects; ++index)
    {
      if (this.e.TerSect[index].IsPortrait || (this.e.TerOpFlags & 16 /*0x10*/) != 0 && (this.e.TerFlags4 & 32768 /*0x8000*/) == 0)
      {
        this.e.TerSect1[index].PgWidth = this.e.TerSect[index].PprWidth;
        this.e.TerSect1[index].PgHeight = this.e.TerSect[index].PprHeight;
        this.e.TerSect1[index].HiddenX = this.e.PortraitHX;
        this.e.TerSect1[index].HiddenY = this.e.PortraitHY;
      }
      else
      {
        this.e.TerSect1[index].PgWidth = this.e.TerSect[index].PprHeight;
        this.e.TerSect1[index].PgHeight = this.e.TerSect[index].PprWidth;
        this.e.TerSect1[index].HiddenX = this.e.LandscapeHX;
        this.e.TerSect1[index].HiddenY = this.e.LandscapeHY;
      }
      if ((this.e.TerOpFlags & 8) == 0)
      {
        if ((double) this.e.TerSect[index].LeftMargin + (double) this.e.TerSect[index].RightMargin > (double) this.e.TerSect1[index].PgWidth - 0.00050000002374872565)
        {
          this.e.TerSect[index].RightMargin = this.e.TerSect1[index].PgWidth - 0.0005f - this.e.TerSect[index].LeftMargin;
          if ((double) this.e.TerSect[index].RightMargin < 0.0)
          {
            this.e.TerSect[index].LeftMargin += this.e.TerSect[index].RightMargin;
            this.e.TerSect[index].RightMargin = 0.0f;
          }
        }
        if ((double) this.e.TerSect[index].TopMargin + (double) this.e.TerSect[index].BotMargin > (double) this.e.TerSect1[index].PgHeight - 0.00050000002374872565)
        {
          this.e.TerSect[index].BotMargin = this.e.TerSect1[index].PgHeight - 0.0005f - this.e.TerSect[index].TopMargin;
          if ((double) this.e.TerSect[index].BotMargin < 0.0)
          {
            this.e.TerSect[index].TopMargin += this.e.TerSect[index].BotMargin;
            this.e.TerSect[index].BotMargin = 0.0f;
          }
        }
      }
    }
    return true;
  }

  internal bool TerGetPrinterHiddenXY(Graphics gr, out int x, out int y)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    this.GetPrinterHiddenArea(gr);
    x = this.e.HiddenX;
    y = this.e.HiddenY;
    return true;
  }

  internal bool TerIsPrinting()
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    return this.e.InPrinting;
  }

  internal static bool TerMergePrint(ref tc.StrPrint param)
  {
    if (param.InputType == 'F' && !File.Exists(param.file))
      return false;
    ImRtfEditor imRtfEditor = new ImRtfEditor();
    imRtfEditor.TerSetFlags5(true, 1073741824 /*0x40000000*/);
    imRtfEditor.TerSetFlags(true, 1048576 /*0x100000*/);
    imRtfEditor.WordWrap = true;
    imRtfEditor.PageMode = true;
    imRtfEditor.PrintViewMode = true;
    imRtfEditor.InServer = true;
    imRtfEditor.TerCreateControl();
    bool flag = imRtfEditor.prt.TerMergePrint2(ref param);
    imRtfEditor.Dispose();
    return flag;
  }

  internal bool TerMergePrint2(ref tc.StrPrint param)
  {
    Graphics graphics = (Graphics) null;
    GraphicsState gstate = (GraphicsState) null;
    bool flag1 = false;
    Cursor cursor = (Cursor) null;
    COp.RECT rect1 = new COp.RECT();
    bool flag2 = false;
    bool flag3 = false;
    Graphics gr = (Graphics) null;
    if (tc.expired || !this.DrawEval())
      return false;
    if (param.gr != null)
      param.OnePage = true;
    if (param.gr == null)
    {
      if (!this.e.PrinterAvailable)
        return false;
      if (param.FullPage)
        param.PrintMarginArea = true;
    }
    if (param.InputType == 'F')
    {
      this.e.ReadTerFile(param.file);
    }
    else
    {
      this.e.Data = param.buffer;
      this.e.DocName = param.file;
    }
    this.e.ShowHiddenText = param.PrintHiddenText;
    if ((this.e.TerFlags & 512 /*0x0200*/) == 0)
    {
      cursor = this.e.Cursor;
      this.e.Cursor = Cursors.WaitCursor;
    }
    this.e.InPrinting = true;
    this.e.HoldMessages = true;
    this.e.TerOpFlags |= 8;
    COp.RECT rect2 = this.ToRect(param.rect);
    int num1 = rect2.left < 0 || rect2.right < 0 || rect2.top < 0 ? 1 : (rect2.bottom < 0 ? 1 : 0);
    COp.RECT rect3 = rect2;
    if (num1 != 0)
    {
      rect3.left = -rect3.left;
      rect3.right = -rect3.right;
      rect3.top = -rect3.top;
      rect3.bottom = -rect3.bottom;
    }
    else
    {
      rect3.left = (int) ((double) this.MmToInches(rect3.left) * 1440.0);
      rect3.right = (int) ((double) this.MmToInches(rect3.right) * 1440.0);
      rect3.top = (int) ((double) this.MmToInches(rect3.top) * 1440.0);
      rect3.bottom = (int) ((double) this.MmToInches(rect3.bottom) * 1440.0);
    }
    this.e.PageWidth = this.TwipsToInches(rect3.right - rect3.left);
    this.e.PageHeight = this.TwipsToInches(rect3.bottom - rect3.top);
    this.e.MPRect = rect3;
    if (param.gr == null && !param.FullPage)
      this.e.TerOpFlags |= 131072 /*0x020000*/;
    else if (param.gr != null)
    {
      graphics = this.e.PrtGr;
      this.e.PrtGr = gr = param.gr;
      gstate = gr.Save();
      gr.PageUnit = GraphicsUnit.Pixel;
      this.e.PrtResX = (int) gr.DpiX;
      this.e.PrtResY = (int) gr.DpiY;
      if (param.IsPrinter)
        this.GetPrinterHiddenArea(gr);
      else
        this.e.HiddenX = this.e.HiddenY = 0;
      this.e.PrtVpX = this.MulDiv(rect3.left, this.e.PrtResX, 1440) - this.e.HiddenX;
      this.e.PrtVpY = this.MulDiv(rect3.top, this.e.PrtResY, 1440) - this.e.HiddenY;
      this.draw.ResetTransform(gr);
      gr.ScaleTransform((float) this.e.PrtResX / 1440f, (float) this.e.PrtResY / 1440f, System.Drawing.Drawing2D.MatrixOrder.Append);
      gr.TranslateTransform((float) this.e.PrtVpX, (float) this.e.PrtVpY, System.Drawing.Drawing2D.MatrixOrder.Append);
      this.e.TerOpFlags |= 131072 /*0x020000*/;
    }
    if (!param.PrintMarginArea)
    {
      int StartLine = -1;
      int num2 = -1;
      for (int index = 0; index <= this.e.TotalLines; ++index)
      {
        bool flag4 = index < this.e.TotalLines && (this.e.PfmtId[this.e.text[index].pfmt].flags & 12288 /*0x3000*/) != 0;
        if (StartLine == -1 & flag4)
          StartLine = num2 = index;
        if (!flag4 && StartLine != -1)
        {
          this.MoveLineArrays(StartLine, num2 - StartLine, 'D');
          StartLine = num2 = -1;
        }
      }
    }
    if (param.PrintMarginArea && !this.e.ViewPageHdrFtr)
    {
      int index = 0;
      while (index < this.e.TotalLines && (this.e.PfmtId[this.e.text[index].pfmt].flags & 12288 /*0x3000*/) == 0)
        ++index;
      if (index < this.e.TotalLines)
        this.ToggleViewHdrFtr();
    }
    if (this.e.TotalParaFrames > 1 && param.PrintMarginArea)
    {
      for (int index = 1; index < this.e.TotalParaFrames; ++index)
      {
        if (this.e.ParaFrame[index].InUse && (this.e.ParaFrame[index].flags & 32 /*0x20*/) != 0)
        {
          int num3 = (int) ((double) this.e.TerSect[0].TopMargin * 1440.0);
          this.e.ParaFrame[index].y -= num3;
          this.e.ParaFrame[index].ParaY -= num3;
          tc.ResetUintFlag(ref this.e.ParaFrame[index].flags, 32 /*0x20*/);
          this.e.ParaFrame[index].flags |= 64 /*0x40*/;
        }
      }
    }
    if (param.gr != null || !param.FullPage)
    {
      for (int index = 0; index < this.e.TotalSects; ++index)
      {
        float pprWidth = this.e.TerSect[index].PprWidth;
        float pprHeight = this.e.TerSect[index].PprHeight;
        float pageWidth;
        this.e.TerSect[index].PprWidth = pageWidth = this.e.PageWidth;
        this.e.TerSect1[index].PgWidth = pageWidth;
        float pageHeight;
        this.e.TerSect[index].PprHeight = pageHeight = this.e.PageHeight;
        this.e.TerSect1[index].PgHeight = pageHeight;
        this.e.TerSect[index].IsPortrait = true;
        if (param.PrintMarginArea)
        {
          this.e.TerSect[index].LeftMargin = this.e.TerSect[index].LeftMargin * this.e.PageWidth / pprWidth;
          this.e.TerSect[index].RightMargin = this.e.TerSect[index].RightMargin * this.e.PageWidth / pprWidth;
          this.e.TerSect[index].TopMargin = this.e.TerSect[index].TopMargin * this.e.PageHeight / pprHeight;
          this.e.TerSect[index].BotMargin = this.e.TerSect[index].BotMargin * this.e.PageHeight / pprHeight;
          this.e.TerSect[index].HdrMargin = this.e.TerSect[index].HdrMargin * this.e.PageHeight / pprHeight;
          this.e.TerSect[index].FtrMargin = this.e.TerSect[index].FtrMargin * this.e.PageHeight / pprHeight;
        }
        else
        {
          this.e.TerSect[index].LeftMargin = 0.0f;
          this.e.TerSect[index].RightMargin = 0.0f;
          this.e.TerSect[index].TopMargin = 0.0f;
          this.e.TerSect[index].BotMargin = 0.0f;
          this.e.TerSect[index].HdrMargin = 0.0f;
          this.e.TerSect[index].FtrMargin = 0.0f;
        }
      }
    }
    bool flag5 = param.StartPos > 0;
    if (flag5)
    {
      this.AbsToRowCol(param.StartPos, 'E');
      for (int index = 0; index < this.e.HilightEndRow; ++index)
      {
        int bltId = this.e.PfmtId[this.e.text[index].pfmt].BltId;
        if (bltId > 0 && (!this.e.TerBlt[bltId].IsBullet || this.e.TerBlt[bltId].ls > 0))
        {
          if (param.StartPos > 0)
          {
            int CursCol = -1;
            this.e.SetTerCursorPos(this.e.HilightEndRow, this.e.HilightEndCol, true);
            this.e.TerPageBreak(true);
            this.e.GetTerCursorPos(out param.StartPos, ref CursCol);
            flag3 = true;
          }
          flag5 = false;
          break;
        }
      }
    }
    if (flag5)
    {
      this.e.HilightBegRow = 0;
      this.e.HilightBegCol = 0;
      this.AbsToRowCol(param.StartPos, 'E');
      this.e.HilightType = 2;
      this.DeleteCharBlock(false, false);
      flag2 = true;
    }
    this.RecreateSections();
    if (this.e.HtmlMode)
      this.e.TerAdjustHtmlTable();
    this.Repaginate(false, false, 0, true);
    if (!this.e.TerMergeFields(param.MergeFields, param.MergeData, false))
    {
      this.PrintError(49, (string) null);
    }
    else
    {
      this.RecreateSections();
      this.Repaginate(false, false, 0, true);
      this.RecreateSections();
      if (this.e.TotalParaFrames > 1)
        this.Repaginate(false, false, 0, true);
      int row;
      int col;
      int ToLine;
      if (param.StartPos < 0)
      {
        int index = -param.StartPos;
        if (index >= this.e.TotalPages)
          index = this.e.TotalPages - 1;
        param.StartPos = this.RowColToAbs(this.e.PageInfo[index].FirstLine, 0);
        this.AbsToRowCol(param.StartPos, out row, out col);
        ToLine = this.e.text[row].page;
      }
      else if (flag2)
      {
        ToLine = 0;
        row = 0;
        col = 0;
      }
      else
      {
        this.AbsToRowCol(param.StartPos, out row, out col);
        ToLine = this.e.text[row].page;
      }
      int index1;
      if (param.OnePage)
      {
        this.e.CurPage = ToLine;
        this.CreateFrames(true, this.e.CurPage, this.e.CurPage);
        index1 = this.e.PageInfo[this.e.CurPage].LastLine;
      }
      else
        index1 = this.e.TotalLines - 1;
      int num4 = this.e.text[index1].len - 1;
      int num5;
      int num6;
      if (param.OnePage)
      {
        num6 = num5 = ToLine;
      }
      else
      {
        num6 = this.e.text[row].page;
        num5 = this.e.text[index1].page;
      }
      if (gr == null)
      {
        PrintDocument printDocument = new PrintDocument();
        printDocument.DocumentName = this.e.DocName;
        printDocument.PrinterSettings = this.e.CurPrt;
        printDocument.DefaultPageSettings = this.e.CurPrtPage;
        printDocument.PrintController = (PrintController) new StandardPrintController();
        printDocument.QueryPageSettings += new QueryPageSettingsEventHandler(this.QueryPageSettings);
        printDocument.PrintPage += new PrintPageEventHandler(this.PrintOnePage);
        this.e.CurPrt.Copies = (short) 1;
        this.e.CurPrt.Collate = false;
        this.PrtFirstPage = this.e.PrtCurPage = num6;
        this.PrtLastPage = num5;
        this.PrtCopies = 1;
        this.PrtCurCopy = 0;
        this.PrtCollate = false;
        printDocument.Print();
        param.NextY = rect2.bottom;
      }
      else
        param.NextY = rect2.top + this.PrintViewMode(this.e.PrtGr, -1, ToLine);
      if (index1 + 1 >= this.e.TotalLines || this.e.CurPage >= this.e.TotalPages - 1 || !param.OnePage)
      {
        param.NextPos = 0;
      }
      else
      {
        param.NextPos = this.RowColToAbs(this.e.PageInfo[ToLine + 1].FirstLine, 0);
        if (flag2)
          param.NextPos += param.StartPos;
        else if (flag3)
          --param.NextPos;
      }
      flag1 = true;
    }
    if (param.gr != null && gstate != null)
      gr.Restore(gstate);
    this.e.PrtGr = graphics;
    if (cursor != (Cursor) null)
      this.e.Cursor = cursor;
    return flag1;
  }

  internal static bool TerOverridePageSize(int width, int height)
  {
    tc.InitPageWidth = width;
    tc.InitPageHeight = height;
    return true;
  }

  internal Bitmap TerPageBitmap(int page)
  {
    int y = 96 /*0x60*/;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int pWidth;
    int pHeight;
    this.e.TerGetPageOrientEx(page, out pWidth, out pHeight);
    Rectangle rectangle = new Rectangle();
    int num;
    rectangle.Y = num = 0;
    rectangle.X = num;
    rectangle.Width = this.MulDiv(pWidth, y, 1440);
    rectangle.Height = this.MulDiv(pHeight, y, 1440);
    IntPtr dc = COp.Win32.GetDC(IntPtr.Zero);
    Metafile metafile = new Metafile(dc, rectangle, MetafileFrameUnit.Pixel, EmfType.EmfOnly);
    Graphics gr = Graphics.FromImage((Image) metafile);
    gr.FillRectangle(Brushes.White, rectangle);
    this.TerPrintPreview(gr, rectangle, page, true);
    gr.Dispose();
    COp.Win32.ReleaseDC(IntPtr.Zero, dc);
    Bitmap bitmap = new Bitmap(rectangle.Width, rectangle.Height);
    Graphics graphics = Graphics.FromImage((Image) bitmap);
    graphics.DrawImage((Image) metafile, new Rectangle(0, 0, rectangle.Width, rectangle.Height));
    graphics.Dispose();
    metafile.Dispose();
    return bitmap;
  }

  internal Metafile TerPageMetafile(int page)
  {
    int y = 300;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int pWidth;
    int pHeight;
    this.e.TerGetPageOrientEx(page, out pWidth, out pHeight);
    Rectangle rectangle = new Rectangle();
    int num;
    rectangle.Y = num = 0;
    rectangle.X = num;
    rectangle.Width = this.MulDiv(pWidth, y, 1440);
    rectangle.Height = this.MulDiv(pHeight, y, 1440);
    IntPtr dc = COp.Win32.GetDC(IntPtr.Zero);
    Metafile metafile = new Metafile(dc, rectangle, MetafileFrameUnit.Document, EmfType.EmfOnly);
    Graphics gr = Graphics.FromImage((Image) metafile);
    this.e.TerOpFlags2 |= 16384 /*0x4000*/;
    this.TerPrintPreview(gr, rectangle, page, true);
    this.e.TerOpFlags2 = tc.ResetFlag(this.e.TerOpFlags2, 16384 /*0x4000*/);
    this.DeleteOpGr(gr);
    gr.Dispose();
    COp.Win32.ReleaseDC(IntPtr.Zero, dc);
    return metafile;
  }

  internal Metafile TerPageMetafile(int page, Stream output)
  {
    int y = 300;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int pWidth;
    int pHeight;
    this.e.TerGetPageOrientEx(page, out pWidth, out pHeight);
    Rectangle rectangle = new Rectangle();
    int num;
    rectangle.Y = num = 0;
    rectangle.X = num;
    rectangle.Width = this.MulDiv(pWidth, y, 1440);
    rectangle.Height = this.MulDiv(pHeight, y, 1440);
    IntPtr dc = COp.Win32.GetDC(IntPtr.Zero);
    Metafile metafile = new Metafile(output, dc, rectangle, MetafileFrameUnit.Document, EmfType.EmfOnly);
    Graphics gr = Graphics.FromImage((Image) metafile);
    this.e.TerOpFlags2 |= 16384 /*0x4000*/;
    this.TerPrintPreview(gr, rectangle, page, true);
    this.e.TerOpFlags2 = tc.ResetFlag(this.e.TerOpFlags2, 16384 /*0x4000*/);
    gr.Dispose();
    COp.Win32.ReleaseDC(IntPtr.Zero, dc);
    return metafile;
  }

  internal new bool TerPageOptions()
  {
    if (!this.CallDialogBox((Form) new terdlg_page(this.e)))
      return false;
    if (this.e.TerArg.PrintView)
      this.PaintTer();
    return true;
  }

  internal bool TerPrint(bool dialog) => this.TerPrintEx(dialog, -1, -1);

  internal bool TerPrint2(bool dialog, int FirstPage, int LastPage, int copies, bool collate)
  {
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    bool flag4 = false;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (tc.expired)
      return false;
    if (!this.e.PrinterAvailable)
    {
      this.e.TerLastMsg = 164;
      return false;
    }
    bool flag5 = false;
    PrintDocument printDocument = new PrintDocument();
    printDocument.DocumentName = this.e.DocName;
    printDocument.PrintController = (PrintController) new StandardPrintController();
    printDocument.PrinterSettings = this.e.CurPrt;
    printDocument.DefaultPageSettings = this.e.CurPrtPage;
    printDocument.QueryPageSettings += new QueryPageSettingsEventHandler(this.QueryPageSettings);
    printDocument.PrintPage += new PrintPageEventHandler(this.PrintOnePage);
    int num;
    if (dialog)
    {
      PrintDialog printDialog = new PrintDialog();
      printDialog.Document = printDocument;
      printDialog.AllowSomePages = true;
      printDialog.AllowSelection = true;
      this.e.CurPrt.FromPage = 1;
      this.e.CurPrt.ToPage = this.e.TotalPages;
      if (this.e.TerArg.PrintView)
      {
        this.e.CurPrt.MinimumPage = 1;
        this.e.CurPrt.MaximumPage = this.e.TotalPages;
      }
      if (printDialog.ShowDialog() != DialogResult.OK)
      {
        this.e.SendActionMsg = false;
        return false;
      }
      this.e.TerRepaint(true);
      num = 773;
      if (this.e.CurPrt.PrintRange == PrintRange.Selection)
        num = 774;
      else if (this.e.CurPrt.PrintRange == PrintRange.SomePages)
      {
        num = 955;
        FirstPage = this.e.CurPrt.FromPage - 1;
        LastPage = this.e.CurPrt.ToPage - 1;
      }
    }
    else
    {
      if (FirstPage >= 0 && FirstPage > LastPage)
        return false;
      if (FirstPage >= 0 || FirstPage == -2)
      {
        this.e.CurPrt.PrintRange = PrintRange.SomePages;
        num = 955;
        if (FirstPage == -2)
          flag5 = true;
      }
      else if (this.e.HilightType == 0)
      {
        num = 773;
        this.e.CurPrt.PrintRange = PrintRange.AllPages;
      }
      else
      {
        num = 774;
        this.e.CurPrt.PrintRange = PrintRange.Selection;
      }
      this.e.CurPrt.Copies = (short) copies;
      copies = 1;
      this.e.CurPrt.Collate = collate;
    }
    if (num == 774 && this.e.TerArg.WordWrap && this.e.EditPageHdrFtr)
      return this.PrintError(95, (string) null);
    if (num == 774 && this.e.HilightType != 0)
    {
      int index = 0;
      while (index < this.e.TotalPages && this.e.HilightBegRow != this.e.PageInfo[index].FirstLine)
        ++index;
      if (index == this.e.TotalPages && this.e.HilightEndRow < this.e.HilightBegRow + 1000)
      {
        printDocument.Dispose();
        return this.TerSelectPrint2(copies);
      }
    }
    if (this.e.HilightType == 0 || this.NormalizeBlock())
    {
      bool wordWrap = this.e.TerArg.WordWrap;
      if (!this.e.TerArg.WordWrap)
        this.TurnOnWordWrap();
      if (this.e.EditFootnoteText)
        this.ToggleFootnoteEdit(true);
      if (this.e.EditEndnoteText)
        this.ToggleFootnoteEdit(false);
      this.RecreateSections();
      bool paintEnabled = this.e.PaintEnabled;
      this.e.PaintEnabled = false;
      this.e.InPrinting = true;
      this.e.HoldMessages = true;
      this.e.PrtTextBkColor = this.e.PrtParaBkColor = this.e.PrtFrameBkColor = tc.CLR_WHITE;
      this.e.PrtCurPage = FirstPage;
      this.DisplayStatus();
      bool editPageHdrFtr = this.e.EditPageHdrFtr;
      this.e.SavePvFirstFramePage = this.e.FirstFramePage;
      this.e.SavePvLastFramePage = this.e.LastFramePage;
      bool showPageBorder = this.e.ShowPageBorder;
      this.e.ShowPageBorder = false;
      if (this.e.TerArg.WordWrap && !this.e.TerArg.PrintView)
      {
        this.e.TerArg.PrintView = true;
        flag1 = true;
      }
      if (this.e.TerArg.PrintView)
      {
        if (!this.e.TerArg.PageMode)
        {
          this.e.TerArg.PageMode = true;
          flag2 = true;
        }
        flag3 = this.e.TerArg.FittedView;
        this.e.TerArg.FittedView = false;
        if (!flag2 && this.e.EditPageHdrFtr)
          this.ToggleEditHdrFtr();
        if (!flag1 && !this.e.ViewPageHdrFtr)
        {
          for (int index = 0; index < this.e.TotalSects; ++index)
          {
            if (this.e.TerSect1[index].hdr.FirstLine >= 0 || this.e.TerSect1[index].ftr.FirstLine >= 0 || this.e.TerSect1[index].fhdr.FirstLine >= 0 || this.e.TerSect1[index].fftr.FirstLine >= 0)
            {
              flag4 = true;
              this.ToggleViewHdrFtr();
              break;
            }
          }
        }
      }
      if (this.e.TerArg.PrintView)
      {
        if (this.e.HtmlMode)
          this.e.TerAdjustHtmlTable();
        this.Repaginate(false, false, 0, true);
        if (this.e.TotalParaFrames > 1)
          this.Repaginate(false, false, 0, true);
        this.RecreateSections();
      }
      switch (num)
      {
        case 773:
          FirstPage = 0;
          LastPage = this.e.TotalPages - 1;
          break;
        case 774:
          if (this.e.HilightType == 0)
          {
            FirstPage = 0;
            LastPage = this.e.TotalPages - 1;
            break;
          }
          FirstPage = this.e.text[this.e.HilightBegRow].page;
          LastPage = this.e.text[this.e.HilightEndRow].page;
          break;
      }
      if (flag5)
        FirstPage = LastPage = this.e.CurPage;
      if (FirstPage >= this.e.TotalPages)
        FirstPage = this.e.TotalPages - 1;
      if (LastPage >= this.e.TotalPages)
        LastPage = this.e.TotalPages - 1;
      if (FirstPage > LastPage)
        FirstPage = LastPage;
      this.PrtFirstPage = this.e.PrtCurPage = FirstPage;
      this.PrtLastPage = LastPage;
      this.PrtCopies = copies;
      this.PrtCurCopy = 0;
      this.PrtCollate = collate;
      this.DisplayStatus();
      printDocument.Print();
      this.e.TerArg.FittedView = flag3;
      this.e.ShowPageBorder = showPageBorder;
      if (flag4)
        this.ToggleViewHdrFtr();
      this.e.EditPageHdrFtr = editPageHdrFtr;
      if (flag2)
        this.TogglePageMode();
      if (flag1)
        this.e.TerArg.PrintView = this.e.TerArg.PageMode = false;
      this.e.InPrinting = false;
      this.e.HoldMessages = false;
      this.e.PaintEnabled = paintEnabled;
      this.e.TextBorder = this.e.ParaShading = 0;
      if (this.e.HtmlMode)
        this.e.TerAdjustHtmlTable();
      if (this.e.TerArg.FittedView)
        this.e.TerRepaginate(false);
      if (this.e.SavePvFirstFramePage != this.e.FirstFramePage && this.e.TerArg.PageMode)
        this.CreateFrames(false, this.e.SavePvFirstFramePage, this.e.SavePvLastFramePage);
      if (!wordWrap)
        this.TurnOffWordWrap();
      this.PaintTer();
      if (this.e.GoPrintPreview)
      {
        this.e.GoPrintPreview = false;
        this.PostMessage(this.e.hTerWnd, 2737, 717, 0);
      }
      printDocument.Dispose();
    }
    return true;
  }

  internal bool TerPrintEx(bool dialog, int FirstPage, int LastPage)
  {
    return this.TerPrint2(dialog, FirstPage, LastPage, 1, true);
  }

  internal new bool TerPrintOptions()
  {
    PaperSize paperSize = (PaperSize) null;
    if (this.e.CurPrt == null || this.e.CurPrtPage == null)
      return false;
    int section = this.GetSection(this.e.CurLine);
    this.e.IsPortrait = this.e.TerSect[section].IsPortrait;
    this.e.CurPrtPage.Landscape = !this.e.IsPortrait;
    this.e.PprKind = this.e.TerSect[section].PprKind;
    foreach (PaperSize paperSiz in this.e.CurPrt.PaperSizes)
    {
      if (paperSiz.Kind == this.e.PprKind)
      {
        paperSize = paperSiz;
        break;
      }
    }
    if (paperSize == null)
      paperSize = new PaperSize("Custom", (int) ((double) this.e.TerSect[section].PprWidth * 100.0), (int) ((double) this.e.TerSect[section].PprHeight * 100.0));
    this.e.CurPrtPage.PaperSize = paperSize;
    foreach (PaperSource paperSource in this.e.CurPrt.PaperSources)
    {
      if (paperSource.Kind == this.e.TerSect[section].FirstPageBin)
      {
        this.e.CurPrtPage.PaperSource = paperSource;
        break;
      }
    }
    PageSetupDialog pageSetupDialog = new PageSetupDialog()
    {
      AllowMargins = false,
      PageSettings = this.e.CurPrtPage
    };
    pageSetupDialog.PageSettings.Margins.Top = (int) ((double) this.e.TerSect[section].TopMargin * 100.0);
    pageSetupDialog.PageSettings.Margins.Bottom = (int) ((double) this.e.TerSect[section].BotMargin * 100.0);
    pageSetupDialog.PageSettings.Margins.Left = (int) ((double) this.e.TerSect[section].LeftMargin * 100.0);
    pageSetupDialog.PageSettings.Margins.Right = (int) ((double) this.e.TerSect[section].RightMargin * 100.0);
    pageSetupDialog.PrinterSettings = this.e.CurPrt;
    int num = (int) pageSetupDialog.ShowDialog();
    this.e.TerSetPrinter(pageSetupDialog.PrinterSettings, pageSetupDialog.PageSettings, true);
    pageSetupDialog.Dispose();
    return true;
  }

  internal bool TerPrintPreview(Graphics gr, Rectangle rect, int page, bool scale)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int num = 1;
    if (!this.e.ExtPrintPreview || !this.e.InPrinting)
    {
      if (!this.PrePreview(gr))
      {
        this.e.InPrintPreview = false;
        return false;
      }
      this.e.HoldMessages = true;
    }
    if (this.e.ExtPrintPreview)
    {
      this.PostPreview();
      if (!this.e.TerArg.WordWrap)
        num = this.e.TotalPages;
      this.PrePreview(gr);
      if (!this.e.TerArg.WordWrap)
        this.e.TotalPages = num;
      this.e.HoldMessages = true;
    }
    if (this.e.TerArg.PrintView && page < 0)
      page = this.GetCurPage(this.e.CurLine);
    this.PreviewOnePage(gr, this.ToRect(rect), page, scale);
    if (!this.e.ExtPrintPreview)
    {
      this.PostPreview();
      this.PaintTer();
    }
    return true;
  }

  internal PrintPreviewDialog TerPrintPreviewDlg()
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.e.PvDlg != null)
    {
      this.e.PvDlg.Dispose();
      this.e.PvDlg = (PrintPreviewDialog) null;
      return this.e.PvDlg;
    }
    this.e.PvDlg = new PrintPreviewDialog();
    this.e.PvDlg.Load += new EventHandler(this.PvDlgLoad);
    this.e.PvDlg.Closed += new EventHandler(this.PvDlgClosed);
    this.e.PvDlg.TopMost = true;
    PrintDocument printDocument = new PrintDocument();
    printDocument.DocumentName = this.e.DocName;
    printDocument.PrinterSettings = this.e.CurPrt;
    printDocument.DefaultPageSettings = this.e.CurPrtPage;
    printDocument.QueryPageSettings += new QueryPageSettingsEventHandler(this.QueryPageSettings);
    printDocument.PrintPage += new PrintPageEventHandler(this.PvDlgPrintOnePage);
    this.e.PvDlg.Document = printDocument;
    this.e.PrtCurPage = 0;
    this.PrtFirstPage = 0;
    this.PrtLastPage = this.e.TotalPages - 1;
    this.misc.CenterDlgBox((Form) this.e.PvDlg);
    this.e.PvDlg.Left = 0;
    this.e.PvDlg.Top = 0;
    this.e.DlgPreviewDone = false;
    this.e.PvDlg.Show();
    return this.e.PvDlg;
  }

  internal new bool TerPrintPreviewMode(bool toolbar)
  {
    if ((this.e.TerFlags5 & 2097152 /*0x200000*/) != 0)
      return this.TerPrintPreviewDlg() != null;
    if (this.e.InPrintPreview)
    {
      this.e.ScrResX = this.e.SavePvScrResX;
      this.e.ScrResY = this.e.SavePvScrResY;
      this.PostPreview();
      this.e.InPrintPreview = false;
      if (this.e.PvTlb != null)
      {
        this.e.Focus();
        this.e.PvTlb.Dispose();
        this.e.PvTlb = (ToolbarControl) null;
      }
      this.e.TerArg.ShowStatus = this.e.SavePvStatus;
      this.e.TerArg.ruler = this.e.SavePvRuler;
      this.e.TerArg.ToolBar = this.e.SavePvToolbar;
      this.e.ToolBarHeight = this.e.SavePvToolBarHeight;
      if (!this.e.SavePvShowHorBar && this.e.TerArg.ShowHorBar)
      {
        this.e.TerArg.ShowHorBar = this.e.SavePvShowHorBar;
        this.ShowScrollBar(this.e.hTerWnd, 0, false);
      }
      if (this.e.TerArg.ToolBar)
        this.e.TerTlb.Visible = true;
      this.e.RulerPending = true;
      this.e.RepaintRuler = true;
      if (this.e.TerArg.WordWrap && this.e.CurCol >= this.e.text[this.e.CurLine].len)
        this.e.CurCol = this.e.text[this.e.CurLine].len - 1;
      if (this.e.CurCol < 0)
        this.e.CurCol = 0;
      this.e.PrevCursPage = this.e.CurPage;
      this.e.PrevCursLine = this.e.CurLine;
      this.e.PrevCursCol = this.e.CurCol;
      this.e.CursDirection = 1;
      if (this.e.TerArg.FittedView || this.e.TotalParaFrames > 1)
        this.e.TerRepaginate(false);
      if (this.e.SavePvFirstFramePage != this.e.FirstFramePage && this.e.TerArg.PageMode)
        this.CreateFrames(false, this.e.SavePvFirstFramePage, this.e.SavePvLastFramePage);
      this.SetTerWindowOrg();
      this.InitCaret();
      this.e.PaintFlag = 4;
      this.RequestPagination(true);
      this.e.Invalidate();
      return true;
    }
    if (!this.PrePreview(this.e.TerGr))
    {
      this.e.InPrintPreview = false;
      return false;
    }
    this.e.SavePvScrResX = this.e.ScrResX;
    this.e.SavePvScrResY = this.e.ScrResY;
    this.e.TerArg.ShowStatus = this.e.TerArg.ruler = this.e.TerArg.ToolBar = false;
    this.TerDestroyCaret();
    if (this.e.SavePvToolbar)
      this.e.TerTlb.Visible = false;
    this.e.PvTlb = (ToolbarControl) null;
    this.e.ToolBarHeight = 0;
    if (toolbar)
      this.CreatePvToolbar();
    if (!this.e.TerArg.ShowHorBar)
    {
      this.e.TerArg.ShowHorBar = true;
      this.ShowScrollBar(this.e.hTerWnd, 0, true);
      this.SetScrollRange(this.e.hTerWnd, 0, 0, 1000, false);
    }
    COp.RECT terRect = this.e.TerRect;
    terRect.top += this.e.ToolBarHeight;
    this.e.Invalidate(this.ToRectangle(terRect));
    return true;
  }

  internal bool TerSelectPrint() => this.TerSelectPrint2(1);

  internal bool TerSelectPrint2(int copies)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.PrinterAvailable || this.e.HilightType == 0 || !this.NormalizeBlock())
      return false;
    return CPrt.TerMergePrint(ref new tc.StrPrint()
    {
      InputType = 'B',
      buffer = this.e.TerGetRtfSel(),
      gr = (Graphics) null,
      OnePage = false,
      FullPage = true
    });
  }

  internal static bool TerSetDefPrinter(PrinterSettings PrtSettings, PageSettings PgSettings)
  {
    tc.DefPrt = PrtSettings;
    tc.DefPrtPage = PgSettings;
    return true;
  }

  internal bool TerSetPreview(int pages, int zoom, bool ShowToolbar)
  {
    if (tc.DebugMode)
      this.misc.dm(nameof (TerSetPreview));
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (pages < 1)
      pages = 1;
    if (pages > 2)
      pages = 2;
    if (zoom < 0)
      zoom = 0;
    if (zoom > 200)
      zoom = 200;
    this.e.TotalPreviewPages = pages;
    this.e.PreviewZoom = zoom;
    this.e.ShowPvToolbar = ShowToolbar;
    if (this.e.InPrintPreview)
      this.e.TerRepaint(true);
    return true;
  }

  internal bool TerSetPrinter(PrinterSettings pt, PageSettings pg, bool ModDoc)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (ModDoc)
    {
      int modified = this.e.TerArg.modified;
      int section = this.GetSection(this.e.CurLine);
      bool flag = false;
      if (pg.PaperSize.Kind != this.e.TerSect[section].PprKind)
      {
        this.ApplyPaperSize(pg.PaperSize);
        flag = true;
      }
      else if (pg.PaperSize.Kind == PaperKind.Custom)
      {
        int width = pg.PaperSize.Width;
        int height = pg.PaperSize.Height;
        int num = (int) ((double) this.e.TerSect[section].PprWidth * 100.0);
        if (width != num || height != (int) this.e.TerSect[section].PprHeight)
        {
          this.ApplyPaperSize(pg.PaperSize);
          flag = true;
        }
      }
      if (pg.Landscape != !this.e.TerSect[section].IsPortrait)
      {
        this.ApplyPaperOrient(!pg.Landscape);
        flag = true;
      }
      if (pg.PaperSource.Kind != this.e.TerSect[section].FirstPageBin)
        this.ApplyPaperBins(pg.PaperSource.Kind, pg.PaperSource.Kind);
      if (flag)
      {
        this.SetSectPageSize();
        this.e.TerRepaginate(true);
      }
    }
    this.e.CurPrt = pt;
    this.e.CurPrtPage = pg;
    return true;
  }

  internal bool TerSetPrintPreview(bool begin)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (begin && this.e.ExtPrintPreview || !begin && !this.e.ExtPrintPreview)
      return false;
    if (begin)
      this.e.SavePvFirstFramePage = this.e.FirstFramePage;
    if (!begin && this.e.InPrinting)
    {
      this.PostPreview();
      if (this.e.TerArg.FittedView)
        this.e.TerRepaginate(false);
      if (this.e.SavePvFirstFramePage != this.e.FirstFramePage && this.e.TerArg.PageMode)
        this.CreateFrames(false, this.e.SavePvFirstFramePage, this.e.SavePvLastFramePage);
      this.SetTerWindowOrg();
      this.InitCaret();
      this.e.PaintFlag = 4;
      this.RequestPagination(true);
      this.PaintTer();
    }
    this.e.ExtPrintPreview = begin;
    return true;
  }

  internal bool TurnOffWordWrap()
  {
    this.e.TerFlags |= 2048 /*0x0800*/;
    this.e.TerRewrap();
    for (int LineNo = 0; LineNo < this.e.TotalLines; ++LineNo)
    {
      int len = this.e.text[LineNo].len;
      if (len > 0 && (int) this.e.text[LineNo].txt[len - 1] == (int) this.e.ParaChar)
      {
        this.LineAlloc(LineNo, len, len - 1);
        this.e.text[LineNo].flags = tc.ResetFlag(this.e.text[LineNo].flags, 5);
      }
    }
    this.e.TerArg.WordWrap = false;
    this.e.TerArg.PageMode = this.e.TerArg.PrintView = this.e.TerArg.FittedView = false;
    this.e.TerFlags = tc.ResetFlag(this.e.TerFlags, 2048 /*0x0800*/);
    return true;
  }

  internal bool TurnOnWordWrap()
  {
    for (int LineNo = 0; LineNo < this.e.TotalLines; ++LineNo)
    {
      int len = this.e.text[LineNo].len;
      this.LineAlloc(LineNo, len, len + 1);
      this.e.text[LineNo].txt[len] = this.e.ParaChar;
      this.e.text[LineNo].flags |= 5;
    }
    this.e.TerArg.WordWrap = true;
    this.e.TerArg.PageMode = this.e.TerArg.PrintView = this.e.TerArg.FittedView = false;
    this.e.TerFlags = tc.ResetFlag(this.e.TerFlags, 2048 /*0x0800*/);
    this.AllocWrapBuf(0);
    this.e.TerRewrap();
    return true;
  }
}
