// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.CBar
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class CBar : COp
{
  internal CBar(ImRtfEditor ImRtfEditor)
    : base(ImRtfEditor)
  {
  }

  internal bool CheckAndPaintToolbar(Graphics gr, Bitmap bm, COp.RECT rect, bool check)
  {
    int modified = this.e.TerArg.modified;
    bool flag = !check;
    int index1 = this.GetEffectiveCfmt();
    if (index1 < 0)
      index1 = 0;
    int num1 = 0;
    for (int index2 = 0; index2 < 2; ++index2)
    {
      if (flag)
        this.DrawShadowLine(gr, rect.left, num1, rect.right, num1, Pens.Black, Pens.White);
      for (int index3 = 0; index3 < this.e.TlbItemCount[index2]; ++index3)
      {
        tc.StrTlb pTlb = this.e.TlbId[index2][index3];
        int id = pTlb.id;
        switch (id)
        {
          case 1:
            if (flag)
            {
              this.DrawShadowLine(gr, pTlb.x, pTlb.y, pTlb.x, pTlb.y + pTlb.height, Pens.Black, Pens.White);
              break;
            }
            break;
          case 2:
            TlbComboBox ctl1 = (TlbComboBox) pTlb.ctl;
            string str1 = this.e.CurSID < 0 ? this.e.TerFont[index1].TypeFace : this.e.StyleId[this.e.CurSID].TypeFace;
            if (check && str1 != ctl1.Text)
              return true;
            if (flag)
            {
              ctl1.locked = true;
              if (this.e.CurSID >= 0)
                ctl1.Text = this.e.StyleId[this.e.CurSID].TypeFace;
              else
                ctl1.Text = this.e.TerFont[index1].TypeFace;
              ctl1.locked = false;
              break;
            }
            break;
          case 3:
            TlbComboBox ctl2 = (TlbComboBox) pTlb.ctl;
            int num2 = this.e.CurSID < 0 ? this.e.TerFont[index1].TwipsSize : this.e.StyleId[this.e.CurSID].TwipsSize;
            int num3 = num2 / 20;
            string str2 = $"{num3:d}";
            if (num2 > num3 * 20)
              str2 += ".5";
            if (check && str2 != ctl2.Text)
              return true;
            if (flag)
            {
              ctl2.locked = true;
              ctl2.Text = str2;
              ctl2.locked = false;
              break;
            }
            break;
          case 13:
            TlbComboBox ctl3 = (TlbComboBox) pTlb.ctl;
            int index4;
            if (this.True(this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].StyId))
            {
              index4 = this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].StyId;
            }
            else
            {
              int charStyId = this.e.TerFont[index1].CharStyId;
              index4 = charStyId == 1 || !this.e.StyleId[charStyId].InUse ? 0 : charStyId;
            }
            if (ctl3.FindStringExact(this.e.StyleId[index4].name) == -1 || this.e.ToolBarFillStyles)
            {
              ctl3.locked = true;
              this.FillStyleBox((object) ctl3, 0, true, false);
              ctl3.locked = false;
              ctl3.FindStringExact(this.e.StyleId[index4].name);
              this.e.ToolBarFillStyles = false;
            }
            if (check && ctl3.Text != this.e.StyleId[index4].name)
              return true;
            if (flag)
            {
              ctl3.Text = this.e.StyleId[index4].name;
              ctl3.locked = false;
              break;
            }
            break;
          case 14:
            TlbComboBox ctl4 = (TlbComboBox) pTlb.ctl;
            string str3 = this.e.ZoomPercent.ToString() + "%";
            if (check && str3 != ctl4.Text)
              return true;
            if (flag)
            {
              ctl4.locked = true;
              ctl4.Text = str3;
              ctl4.locked = false;
              break;
            }
            break;
        }
        if (pTlb.CmdId > 0)
        {
          int cmdId = pTlb.CmdId;
          int flags1 = pTlb.flags;
          int flags2 = pTlb.flags;
          int num4 = 14;
          tc.ResetUintFlag(ref flags2, 2);
          tc.ResetUintFlag(ref flags2, 4);
          if (this.e.TerMenuEnable(this.XlateCommandId(cmdId)) == 0)
            flags2 |= 2;
          if (id == this.e.TlbIdClicked || this.e.TerMenuSelect(this.XlateCommandId(cmdId)) == 8)
            flags2 |= 4;
          if (check && (flags1 & num4) != (flags2 & num4))
            return true;
          if (flag)
          {
            pTlb.flags = flags2;
            this.PaintToolbarIcon(gr, bm, pTlb);
          }
        }
        this.e.TlbId[index2][index3] = pTlb;
      }
      num1 += 32 /*0x20*/;
    }
    this.e.TerArg.modified = modified;
    return !check;
  }

  internal bool CheckToolbarIcon(Bitmap bm, int x, int y, int width, int height)
  {
    int x1 = x + width - 1;
    int y1 = y + height - 1;
    Color color = this.ToColor(128 /*0x80*/, 128 /*0x80*/, 128 /*0x80*/);
    Color white1 = Color.White;
    Color white2 = Color.White;
    Color pixel = bm.GetPixel(x, y);
    for (int x2 = x; x2 <= x1; ++x2)
    {
      bm.SetPixel(x2, y, color);
      if (x2 > x)
        bm.SetPixel(x2, y1, white1);
    }
    for (int y2 = y; y2 <= y1; ++y2)
    {
      bm.SetPixel(x, y2, color);
      if (y2 > y)
        bm.SetPixel(x1, y2, white1);
    }
    ++x;
    ++y;
    int num1 = x1 - 1;
    int num2 = y1 - 1;
    bool flag = true;
    for (int x3 = x; x3 <= num1; ++x3)
    {
      for (int y3 = flag ? y : y + 1; y3 <= num2; y3 += 2)
      {
        if (bm.GetPixel(x3, y3) == pixel)
          bm.SetPixel(x3, y3, white2);
      }
      flag = !flag;
    }
    return true;
  }

  internal bool ColorToolbarIcon(Bitmap bm, int x, int y, int width, int height)
  {
    int num1 = x + width - 1;
    int num2 = y + height - 1;
    Color pixel = bm.GetPixel(x, y);
    for (int x1 = x; x1 <= num1; ++x1)
    {
      for (int y1 = y; y1 <= num2; ++y1)
      {
        if (bm.GetPixel(x1, y1) == pixel)
          bm.SetPixel(x1, y1, this.e.StatusBkColor);
      }
    }
    return true;
  }

  internal new bool CreateToolBar()
  {
    if (this.e.TerArg.ToolBar)
    {
      this.e.ToolBarHeight = 0;
      int num1 = 32 /*0x20*/;
      int num2 = 3;
      tc.StrTlb strTlb;
      for (int index1 = 0; index1 < 2; ++index1)
      {
        if (this.e.TlbItemCount[index1] > 0)
        {
          num2 += this.e.ToolBarHeight;
          this.e.ToolBarHeight += num1;
        }
        int num3 = 3;
        for (int index2 = 0; index2 < this.e.TlbItemCount[index1]; ++index2)
        {
          strTlb = this.e.TlbId[index1][index2];
          int id = strTlb.id;
          strTlb.x = num3;
          strTlb.y = num2;
          if ((strTlb.flags & 16 /*0x10*/) == 0)
            ++strTlb.y;
          if ((strTlb.flags & 16 /*0x10*/) != 0)
            strTlb.y += 3;
          num3 += strTlb.width;
          this.e.TlbId[index1][index2] = strTlb;
        }
      }
      this.e.TerTlb = new ToolbarControl();
      this.e.TerTlb.Parent = (Control) this.e;
      this.e.TerTlb.Location = new Point(0, 0);
      this.e.TerTlb.Size = new Size(this.e.TerRect.right - this.e.TerRect.left, this.e.ToolBarHeight);
      this.e.TerTlb.Paint += new PaintEventHandler(this.EvToolbarPaint);
      this.e.TerTlb.MouseDown += new MouseEventHandler(this.EvToolbarMouseDown);
      this.e.TerTlb.MouseUp += new MouseEventHandler(this.EvToolbarMouseUp);
      this.e.TerTlb.MouseMove += new MouseEventHandler(this.EvToolbarMouseMove);
      this.e.TerTlb.TlbTimer += new ToolbarControl.DgtTlbTimer(this.TlbTimer);
      for (int index3 = 0; index3 < 2; ++index3)
      {
        for (int index4 = 0; index4 < this.e.TlbItemCount[index3]; ++index4)
        {
          strTlb = this.e.TlbId[index3][index4];
          int id = strTlb.id;
          if ((strTlb.flags & 16 /*0x10*/) != 0)
          {
            TlbComboBox tlbComboBox = new TlbComboBox();
            strTlb.ctl = (Control) tlbComboBox;
            tlbComboBox.Parent = (Control) this.e.TerTlb;
            tlbComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            if ((strTlb.flags & 32 /*0x20*/) != 0)
              tlbComboBox.DropDownStyle = ComboBoxStyle.DropDown;
            if (id == 2 || id == 13)
              tlbComboBox.Sorted = true;
            tlbComboBox.Location = new Point(strTlb.x, strTlb.y);
            tlbComboBox.Size = new Size(strTlb.width, strTlb.height);
            tlbComboBox.Tag = (object) id;
            tlbComboBox.locked = false;
            tlbComboBox.TabStop = false;
            tlbComboBox.Font = this.e.RulerFont;
            tlbComboBox.ItemHeight = 14;
            tlbComboBox.SelectedIndexChanged += new EventHandler(this.EvComboSelectedIndexChanged);
            tlbComboBox.EnterPressed += new TlbComboBox.DgtEnterPressed(this.EvComboEnterPressed);
            switch (id)
            {
              case 2:
                this.FillFontBox((ComboBox) tlbComboBox);
                break;
              case 3:
                this.FillPointBox((ComboBox) tlbComboBox);
                break;
              case 13:
                this.FillStyleBox((object) tlbComboBox, 0, true, false);
                break;
              case 14:
                int num4 = 7;
                int[] numArray = new int[7]
                {
                  200,
                  150,
                  100,
                  75,
                  50,
                  25,
                  10
                };
                tlbComboBox.Items.Clear();
                for (int index5 = 0; index5 < num4; ++index5)
                {
                  this.e.TempString = numArray[index5].ToString() + "%";
                  tlbComboBox.Items.Add((object) this.e.TempString);
                }
                break;
            }
          }
          this.e.TlbId[index3][index4] = strTlb;
        }
      }
      if (this.e.TerArg.ReadOnly)
        this.EnableToolbarIcons(false);
      this.e.ToolBarCfmt = -1;
      this.e.ToolBarPfmt = -1;
      this.UpdateToolBar(true);
    }
    return true;
  }

  internal new bool DestroyToolBar()
  {
    if (this.e.TerTlb != null)
    {
      this.e.TerTlb.Dispose();
      this.e.TerTlb = (ToolbarControl) null;
      this.e.ToolBarHeight = 0;
    }
    return true;
  }

  internal new bool EnableToolbarIcons(bool enable)
  {
    for (int index1 = 0; index1 < 2; ++index1)
    {
      for (int index2 = 0; index2 < this.e.TlbItemCount[index1]; ++index2)
      {
        tc.StrTlb strTlb = this.e.TlbId[index1][index2];
        Control ctl = strTlb.ctl;
        if (ctl != null && (enable || strTlb.id != 14))
          ctl.Enabled = enable;
      }
    }
    return true;
  }

  internal bool EraseBalloon()
  {
    if (!this.False(this.e.hBlnBM))
    {
      IntPtr dc = this.GetDC(IntPtr.Zero);
      Graphics graphics1 = Graphics.FromHdc(dc);
      Graphics graphics2 = Graphics.FromImage((Image) this.e.hBlnBM);
      COp.RECT blnRect1 = this.e.BlnRect;
      COp.RECT blnRect2 = this.e.BlnRect;
      COp.RECT blnRect3 = this.e.BlnRect;
      COp.RECT blnRect4 = this.e.BlnRect;
      graphics1.DrawImage((Image) this.e.hBlnBM, this.ToRectangle(this.e.BlnRect), new Rectangle(0, 0, this.e.hBlnBM.Width, this.e.hBlnBM.Height), GraphicsUnit.Pixel);
      graphics2.Dispose();
      graphics1.Dispose();
      this.ReleaseDC(dc, IntPtr.Zero);
      this.e.hBlnBM.Dispose();
      this.e.hBlnBM = (Bitmap) null;
    }
    return true;
  }

  internal void EvComboEnterPressed(Control Sender)
  {
    string text = "";
    if (this.True(this.e.hBlnBM))
      this.EraseBalloon();
    int id = this.ToolbarCtlToId((object) Sender);
    switch (id)
    {
      case 2:
      case 3:
      case 13:
      case 14:
        ComboBox cb = (ComboBox) Sender;
        if (id != 13)
          text = cb.Text;
        this.SetComboResult(cb, id, text);
        break;
    }
  }

  internal void EvComboSelectedIndexChanged(object Sender, EventArgs ev)
  {
    string text = "";
    if (this.True(this.e.hBlnBM))
      this.EraseBalloon();
    int id = this.ToolbarCtlToId(Sender);
    switch (id)
    {
      case 2:
      case 3:
      case 13:
      case 14:
        TlbComboBox cb = (TlbComboBox) Sender;
        if (cb.locked)
          break;
        if (id != 13)
        {
          int selectedIndex = cb.SelectedIndex;
          if (selectedIndex < 0)
            break;
          text = cb.Items[selectedIndex].ToString();
        }
        this.SetComboResult((ComboBox) cb, id, text);
        break;
    }
  }

  internal void EvToolbarMouseDown(object Sender, MouseEventArgs ev)
  {
    tc.StrTlb pTlb;
    if (ev.Button != MouseButtons.Left || !this.TlbMousePos(ev.X, ev.Y, out pTlb) || (pTlb.flags & 1) == 0 || (pTlb.flags & 2) == 0)
      return;
    int num = pTlb.CmdId;
    this.KillTimer(this.e.TerTlb.Handle, 9186);
    this.KillTimer(this.e.TerTlb.Handle, 9187);
    Graphics DestGr = Graphics.FromHwnd(this.e.TerTlb.Handle);
    this.e.TlbIdClicked = pTlb.id;
    this.PaintToolbar(DestGr);
    DestGr.Dispose();
    if (!this.True(num))
      return;
    if (num == 659 && (this.e.text[this.e.CurLine].flags2 & 256 /*0x0100*/) != 0)
      num = 660;
    this.PostMessage(this.e.hTerWnd, 2737, num, 0);
  }

  internal void EvToolbarMouseMove(object Sender, MouseEventArgs ev)
  {
    this.TlbMouseMove(ev.X, ev.Y);
  }

  internal void EvToolbarMouseUp(object Sender, MouseEventArgs ev)
  {
    if (ev.Button != MouseButtons.Left)
      return;
    this.e.TlbIdClicked = 0;
    this.PostMessage(this.e.hTerWnd, 1034, 0, 0);
  }

  internal void EvToolbarPaint(object Sender, PaintEventArgs ev)
  {
    Graphics graphics = ev.Graphics;
    this.e.TlbIdClicked = 0;
    this.PaintToolbar(graphics);
  }

  internal new bool FreeToolbar()
  {
    if (this.e.TerArg.ToolBar)
      this.ToggleToolBar();
    for (int index1 = 0; index1 < 2; ++index1)
    {
      for (int index2 = 0; index2 < this.e.TlbItemCount[index1]; ++index2)
        this.e.TlbId[index1][index2].pBalloon = (string) null;
      this.e.TlbItemCount[index1] = 0;
    }
    return true;
  }

  internal bool GrayToolbarIcon(Bitmap bm, int x, int y, int width, int height)
  {
    int num1 = x + width - 1;
    int num2 = y + height - 1;
    Color color = this.ToColor(128 /*0x80*/, 128 /*0x80*/, 128 /*0x80*/);
    Color white = Color.White;
    ++x;
    ++y;
    int num3 = num1 - 1;
    int num4 = num2 - 1;
    for (int x1 = x; x1 <= num3; ++x1)
    {
      for (int y1 = y; y1 <= num4; ++y1)
      {
        Color pixel1 = bm.GetPixel(x1, y1);
        if (this.IsSameColor(pixel1, Color.Black) || this.IsSameColor(pixel1, this.ToColor(0, 0, 128 /*0x80*/)))
        {
          bm.SetPixel(x1, y1, color);
          Color pixel2 = bm.GetPixel(x1 + 1, y1 + 1);
          if (pixel1 != pixel2)
            bm.SetPixel(x1 + 1, y1 + 1, white);
        }
      }
    }
    return true;
  }

  internal bool HilightXpIcon(Bitmap bm, int x, int y, int width, int height, bool IsChecked)
  {
    int x1 = x + width - 1;
    int y1 = y + height - 1;
    Color color1 = this.ToColor(128 /*0x80*/, 128 /*0x80*/, 128 /*0x80*/);
    Color color2 = Color.White;
    Color color3 = Color.White;
    int num1 = (this.e.TerFlags5 & 16777216 /*0x01000000*/) != 0 ? 1 : 0;
    Color pixel = bm.GetPixel(x, y);
    if (num1 != 0)
    {
      color1 = color2 = Color.DarkSlateBlue;
      color3 = !IsChecked ? Color.LightBlue : Color.DeepSkyBlue;
    }
    for (int x2 = x; x2 <= x1; ++x2)
    {
      bm.SetPixel(x2, y, color1);
      if (x2 > x)
        bm.SetPixel(x2, y1, color2);
    }
    for (int y2 = y; y2 <= y1; ++y2)
    {
      bm.SetPixel(x, y2, color1);
      if (y2 > y)
        bm.SetPixel(x1, y2, color2);
    }
    ++x;
    ++y;
    int num2 = x1 - 1;
    int num3 = y1 - 1;
    bool flag = true;
    for (int x3 = x; x3 <= num2; ++x3)
    {
      for (int y3 = flag ? y : y + 1; y3 <= num3; y3 += 2)
      {
        if (this.IsSameColor(bm.GetPixel(x3, y3), pixel))
          bm.SetPixel(x3, y3, color3);
      }
      flag = !flag;
    }
    return true;
  }

  internal new bool InitToolbar()
  {
    int num1 = 1;
    int num2 = (this.e.TerFlags4 & 8192 /*0x2000*/) != 0 ? 1 : 0;
    if ((this.e.TerFlags5 & 8388608 /*0x800000*/) != 0)
      num2 = num1 = 0;
    this.e.TlbId = new tc.StrTlb[3][];
    for (int index = 0; index < 2; ++index)
    {
      this.e.TlbId[index] = new tc.StrTlb[101];
      this.e.TlbItemCount[index] = 0;
    }
    for (int line = num2; line <= num1; ++line)
    {
      int num3 = 0;
      int num4 = 0;
      int num5 = 0;
      for (int index = 0; index < 100; ++index)
      {
        if (this.e.TlbItem[line][index] == 1)
        {
          if (index > 0 && this.e.TlbItem[line][index - 1] != 0 && num3 == 0)
          {
            if (index + 1 < 100 && this.e.TlbItem[line][index] == 18)
            {
              ++index;
              continue;
            }
            continue;
          }
          num3 = 0;
        }
        if ((this.e.TlbItem[line][index] != 13 || (this.e.TerFlags3 & 1048576 /*0x100000*/) != 0) && !this.e.TlbItemHide[line][index] && (this.e.TlbItem[line][index] != 18 || index <= 0 || !this.e.TlbItemHide[line][index - 1]) && (this.e.TlbItem[line][index] != 18 || index <= 1 || this.e.TlbItem[line][index - 1] != 18 || !this.e.TlbItemHide[line][index - 2]) && (this.e.TlbItem[line][index] != 18 || num5 != 0 || num4 < 2))
        {
          if (this.e.TlbItem[line][index] > 0 && this.e.TlbItem[line][index] < 34)
            this.TerAddToolbarItem(line, this.e.TlbItem[line][index], (string) null, (Image) null, -1);
          if (this.e.TlbItem[line][index] != 1 && this.e.TlbItem[line][index] != 18)
            ++num3;
          if (this.e.TlbItem[line][index] == 18)
            ++num4;
          else
            ++num5;
        }
      }
    }
    for (int index = 0; index < this.e.TotalTlbCustIds; ++index)
    {
      if (this.e.TlbCustId[index].InUse)
      {
        if (this.e.TlbCustId[index].id > 0)
          this.TerAddToolbarItem(this.e.TlbCustId[index].line, this.e.TlbCustId[index].id, this.e.TlbCustId[index].pBalloon, (Image) null, -1);
        else
          this.TerAddToolbarItem(this.e.TlbCustId[index].line, 0, this.e.TlbCustId[index].pBalloon, this.e.TlbCustId[index].image, this.e.TlbCustId[index].CmdId);
      }
    }
    return true;
  }

  internal new bool InitToolbarVars()
  {
    this.e.BalloonText[1] = "";
    this.e.BalloonText[2] = "Font";
    this.e.BalloonText[3] = "Font Size";
    this.e.BalloonText[4] = "Bold";
    this.e.BalloonText[5] = "Italic";
    this.e.BalloonText[6] = "Underline";
    this.e.BalloonText[7] = "Align Left";
    this.e.BalloonText[8] = "Align Right";
    this.e.BalloonText[9] = "Center";
    this.e.BalloonText[10] = "Justify";
    this.e.BalloonText[11] = "Increase Indent";
    this.e.BalloonText[12] = "Decrease Indent";
    this.e.BalloonText[13] = "Style";
    this.e.BalloonText[14] = "Zoom";
    this.e.BalloonText[15] = "Cut";
    this.e.BalloonText[16 /*0x10*/] = "Copy";
    this.e.BalloonText[17] = "Paste";
    this.e.BalloonText[18] = "";
    this.e.BalloonText[19] = "New";
    this.e.BalloonText[20] = "Open";
    this.e.BalloonText[21] = "Save";
    this.e.BalloonText[22] = "Print";
    this.e.BalloonText[23] = "Help";
    this.e.BalloonText[24] = "Show Markers";
    this.e.BalloonText[25] = "Print Preview";
    this.e.BalloonText[26] = "Numbering";
    this.e.BalloonText[27] = "Bullets";
    this.e.BalloonText[28] = "Undo";
    this.e.BalloonText[29] = "Redo";
    this.e.BalloonText[30] = "Find";
    this.e.BalloonText[31 /*0x1F*/] = "Insert Date/Time";
    this.e.BalloonText[32 /*0x20*/] = "Insert Page Number";
    this.e.BalloonText[33] = "Insert Page Count";
    for (int index1 = 0; index1 < 2; ++index1)
    {
      this.e.TlbItem[index1] = new int[101];
      this.e.TlbItemHide[index1] = new bool[101];
      for (int index2 = 0; index2 < 100; ++index2)
      {
        this.e.TlbItem[index1][index2] = 0;
        this.e.TlbItemHide[index1][index2] = false;
      }
    }
    int index3 = 1 - 1;
    this.e.TlbItem[0][index3] = 18;
    int index4 = index3 + 1;
    this.e.TlbItem[0][index4] = 1;
    int index5 = index4 + 1;
    this.e.TlbItem[0][index5] = 1;
    int index6 = index5 + 1;
    this.e.TlbItem[0][index6] = 19;
    int index7 = index6 + 1;
    this.e.TlbItem[0][index7] = 18;
    int index8 = index7 + 1;
    this.e.TlbItem[0][index8] = 20;
    int index9 = index8 + 1;
    this.e.TlbItem[0][index9] = 18;
    int index10 = index9 + 1;
    this.e.TlbItem[0][index10] = 21;
    int index11 = index10 + 1;
    this.e.TlbItem[0][index11] = 18;
    int index12 = index11 + 1;
    this.e.TlbItem[0][index12] = 22;
    int index13 = index12 + 1;
    this.e.TlbItem[0][index13] = 18;
    int index14 = index13 + 1;
    this.e.TlbItem[0][index14] = 25;
    int index15 = index14 + 1;
    this.e.TlbItem[0][index15] = 18;
    int index16 = index15 + 1;
    this.e.TlbItem[0][index16] = 18;
    int index17 = index16 + 1;
    this.e.TlbItem[0][index17] = 1;
    int index18 = index17 + 1;
    this.e.TlbItem[0][index18] = 18;
    int index19 = index18 + 1;
    this.e.TlbItem[0][index19] = 15;
    int index20 = index19 + 1;
    this.e.TlbItem[0][index20] = 18;
    int index21 = index20 + 1;
    this.e.TlbItem[0][index21] = 16 /*0x10*/;
    int index22 = index21 + 1;
    this.e.TlbItem[0][index22] = 18;
    int index23 = index22 + 1;
    this.e.TlbItem[0][index23] = 17;
    int index24 = index23 + 1;
    this.e.TlbItem[0][index24] = 18;
    int index25 = index24 + 1;
    this.e.TlbItem[0][index25] = 18;
    int index26 = index25 + 1;
    this.e.TlbItem[0][index26] = 1;
    int index27 = index26 + 1;
    this.e.TlbItem[0][index27] = 18;
    int index28 = index27 + 1;
    this.e.TlbItem[0][index28] = 28;
    int index29 = index28 + 1;
    this.e.TlbItem[0][index29] = 18;
    int index30 = index29 + 1;
    this.e.TlbItem[0][index30] = 29;
    int index31 = index30 + 1;
    this.e.TlbItem[0][index31] = 18;
    int index32 = index31 + 1;
    this.e.TlbItem[0][index32] = 18;
    int index33 = index32 + 1;
    this.e.TlbItem[0][index33] = 1;
    int index34 = index33 + 1;
    this.e.TlbItem[0][index34] = 18;
    int index35 = index34 + 1;
    this.e.TlbItem[0][index35] = 30;
    int index36 = index35 + 1;
    this.e.TlbItem[0][index36] = 18;
    int index37 = index36 + 1;
    this.e.TlbItem[0][index37] = 18;
    int index38 = index37 + 1;
    this.e.TlbItem[0][index38] = 1;
    int index39 = index38 + 1;
    this.e.TlbItem[0][index39] = 18;
    int index40 = index39 + 1;
    this.e.TlbItem[0][index40] = 31 /*0x1F*/;
    int index41 = index40 + 1;
    this.e.TlbItem[0][index41] = 18;
    int index42 = index41 + 1;
    this.e.TlbItem[0][index42] = 32 /*0x20*/;
    int index43 = index42 + 1;
    this.e.TlbItem[0][index43] = 18;
    int index44 = index43 + 1;
    this.e.TlbItem[0][index44] = 33;
    int index45 = index44 + 1;
    this.e.TlbItem[0][index45] = 18;
    int index46 = index45 + 1;
    this.e.TlbItem[0][index46] = 24;
    int index47 = index46 + 1;
    this.e.TlbItem[0][index47] = 18;
    int index48 = index47 + 1;
    this.e.TlbItem[0][index48] = 18;
    int index49 = index48 + 1;
    this.e.TlbItem[0][index49] = 1;
    int index50 = index49 + 1;
    this.e.TlbItem[0][index50] = 18;
    int index51 = index50 + 1;
    this.e.TlbItem[0][index51] = 23;
    int index52 = index51 + 1;
    this.e.TlbItem[0][index52] = 18;
    int index53 = index52 + 1;
    this.e.TlbItem[0][index53] = 18;
    int index54 = index53 + 1;
    this.e.TlbItem[0][index54] = 14;
    int index55 = index54 + 1;
    this.e.TlbItem[0][index55] = 18;
    int index56 = index55 + 1;
    this.e.TlbItem[0][index56] = 1;
    this.e.TlbItem[0][index56 + 1] = 18;
    int index57 = 1 - 1;
    this.e.TlbItem[1][index57] = 18;
    int index58 = index57 + 1;
    this.e.TlbItem[1][index58] = 1;
    int index59 = index58 + 1;
    this.e.TlbItem[1][index59] = 1;
    int index60 = index59 + 1;
    this.e.TlbItem[1][index60] = 18;
    int index61 = index60 + 1;
    this.e.TlbItem[1][index61] = 13;
    int index62 = index61 + 1;
    this.e.TlbItem[1][index62] = 18;
    int index63 = index62 + 1;
    this.e.TlbItem[1][index63] = 2;
    int index64 = index63 + 1;
    this.e.TlbItem[1][index64] = 18;
    int index65 = index64 + 1;
    this.e.TlbItem[1][index65] = 3;
    int index66 = index65 + 1;
    this.e.TlbItem[1][index66] = 18;
    int index67 = index66 + 1;
    this.e.TlbItem[1][index67] = 18;
    int index68 = index67 + 1;
    this.e.TlbItem[1][index68] = 4;
    int index69 = index68 + 1;
    this.e.TlbItem[1][index69] = 5;
    int index70 = index69 + 1;
    this.e.TlbItem[1][index70] = 6;
    int index71 = index70 + 1;
    this.e.TlbItem[1][index71] = 18;
    int index72 = index71 + 1;
    this.e.TlbItem[1][index72] = 1;
    int index73 = index72 + 1;
    this.e.TlbItem[1][index73] = 18;
    int index74 = index73 + 1;
    this.e.TlbItem[1][index74] = 7;
    int index75 = index74 + 1;
    this.e.TlbItem[1][index75] = 9;
    int index76 = index75 + 1;
    this.e.TlbItem[1][index76] = 8;
    int index77 = index76 + 1;
    this.e.TlbItem[1][index77] = 10;
    int index78 = index77 + 1;
    this.e.TlbItem[1][index78] = 18;
    int index79 = index78 + 1;
    this.e.TlbItem[1][index79] = 1;
    int index80 = index79 + 1;
    this.e.TlbItem[1][index80] = 18;
    int index81 = index80 + 1;
    this.e.TlbItem[1][index81] = 26;
    int index82 = index81 + 1;
    this.e.TlbItem[1][index82] = 27;
    int index83 = index82 + 1;
    this.e.TlbItem[1][index83] = 18;
    int index84 = index83 + 1;
    this.e.TlbItem[1][index84] = 11;
    this.e.TlbItem[1][index84 + 1] = 12;
    return true;
  }

  internal bool PaintBalloon()
  {
    Pen black = Pens.Black;
    if (this.e.pBlnTlb.id == -1)
      return false;
    if (this.e.hBlnBM != null)
    {
      this.e.hBlnBM.Dispose();
      this.e.hBlnBM = (Bitmap) null;
    }
    for (int index1 = 0; index1 < 2; ++index1)
    {
      for (int index2 = 0; index2 < this.e.TlbItemCount[index1]; ++index2)
      {
        tc.StrTlb strTlb = this.e.TlbId[index1][index2];
        if ((strTlb.flags & 16 /*0x10*/) != 0 && strTlb.ctl != null && strTlb.ctl.Focused)
          return true;
      }
    }
    string pBalloon = this.e.pBlnTlb.pBalloon;
    if (!this.False(pBalloon) && pBalloon.Length != 0)
    {
      IntPtr dc = this.GetDC(IntPtr.Zero);
      Graphics graphics = Graphics.FromHdc(dc);
      Point pt;
      this.GetCursorPos(out pt);
      this.e.BlnRect.left = pt.X;
      this.e.BlnRect.top = pt.Y + 20;
      SizeF sizeF = graphics.MeasureString(pBalloon, this.e.RulerFont);
      this.e.BlnRect.right = (int) ((double) this.e.BlnRect.left + (double) sizeF.Width + 5.0);
      this.e.BlnRect.bottom = (int) ((double) this.e.BlnRect.top + (double) sizeF.Height + 5.0);
      int num1 = this.e.BlnRect.right - this.e.BlnRect.left;
      int num2 = this.e.BlnRect.bottom - this.e.BlnRect.top;
      this.e.hBlnBM = new Bitmap(num1, num2, graphics);
      Graphics DestGr = Graphics.FromImage((Image) this.e.hBlnBM);
      this.BitBlt(DestGr, 0, 0, num1, num2, graphics, this.e.BlnRect.left, this.e.BlnRect.top, 13369376);
      DestGr.Dispose();
      Brush brush = (Brush) new SolidBrush(this.ToColor((int) byte.MaxValue, (int) byte.MaxValue, 225));
      graphics.FillRectangle(brush, this.ToRectangle(this.e.BlnRect));
      brush.Dispose();
      Pen hSolidPen = new Pen(this.ToColor(175, 175, 175));
      this.DrawShadowBox(graphics, this.e.BlnRect.left, this.e.BlnRect.top, this.e.BlnRect.right - 1, this.e.BlnRect.bottom - 1, hSolidPen, black);
      graphics.DrawString(pBalloon, this.e.RulerFont, Brushes.Black, new PointF((float) (this.e.BlnRect.left + 3), (float) (this.e.BlnRect.top + 3)));
      hSolidPen.Dispose();
      graphics.Dispose();
      this.ReleaseDC(dc, IntPtr.Zero);
      this.SetTimer(this.e.TerTlb.Handle, 9187, 10000);
    }
    return true;
  }

  internal bool PaintToolbar(Graphics DestGr)
  {
    int modified = this.e.TerArg.modified;
    if (DestGr == null)
      return false;
    COp.RECT rect;
    int num1;
    rect.top = num1 = 0;
    rect.left = num1;
    rect.right = this.e.TerRect.right - this.e.TerRect.left;
    rect.bottom = this.e.ToolBarHeight;
    int num2 = this.e.TerRect.right;
    for (int index1 = 0; index1 < 2; ++index1)
    {
      for (int index2 = 0; index2 < this.e.TlbItemCount[index1]; ++index2)
      {
        tc.StrTlb strTlb = this.e.TlbId[index1][index2];
        if (strTlb.x + strTlb.width > num2)
          num2 = strTlb.x + strTlb.width;
      }
    }
    Bitmap bm = new Bitmap(num2 + 1, rect.bottom + 1, DestGr);
    Graphics gr = Graphics.FromImage((Image) bm);
    SolidBrush solidBrush = new SolidBrush(this.ToColor(192 /*0xC0*/, 192 /*0xC0*/, 192 /*0xC0*/));
    gr.FillRectangle(this.e.ToolbarBrush == null ? (Brush) solidBrush : this.e.ToolbarBrush, new Rectangle(0, 0, rect.right + 1, rect.bottom + 1));
    solidBrush.Dispose();
    this.GetEffectiveCfmt();
    this.CheckAndPaintToolbar(gr, bm, rect, false);
    DestGr.DrawImage((Image) bm, this.ToRectangle(rect), this.ToRectangle(rect), GraphicsUnit.Pixel);
    gr.Dispose();
    bm.Dispose();
    this.e.TerArg.modified = modified;
    return true;
  }

  internal bool PaintToolbarIcon(Graphics gr, Bitmap bm, tc.StrTlb pTlb)
  {
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    if ((pTlb.flags & 4) != 0 && (pTlb.flags & 8) == 0)
      flag1 = true;
    if (gr == null || bm == null)
    {
      int num = this.e.TerRect.right;
      for (int index1 = 0; index1 < 2; ++index1)
      {
        for (int index2 = 0; index2 < this.e.TlbItemCount[index1]; ++index2)
        {
          tc.StrTlb strTlb = this.e.TlbId[index1][index2];
          if (strTlb.x + strTlb.width > num)
            num = strTlb.x + strTlb.width;
        }
      }
      bm = new Bitmap(num + 1, this.e.ToolBarHeight + 1);
      gr = Graphics.FromImage((Image) bm);
      flag2 = true;
    }
    if (pTlb.image != null)
      gr.DrawImage(pTlb.image, new Rectangle(pTlb.x, pTlb.y, pTlb.width, pTlb.height), new Rectangle(pTlb.IconX, pTlb.IconY, pTlb.width, pTlb.height), GraphicsUnit.Pixel);
    else if ((pTlb.flags & 2) != 0 | flag3)
    {
      gr.DrawImage((Image) tc.ToolbarBM, new Rectangle(pTlb.x, pTlb.y, pTlb.width, pTlb.height), new Rectangle(pTlb.IconX, pTlb.IconY, pTlb.width, pTlb.height), GraphicsUnit.Pixel);
    }
    else
    {
      ImageAttributes imageAttr = new ImageAttributes();
      ColorMatrix newColorMatrix = new ColorMatrix();
      float num1 = 0.2746582f;
      float num2;
      newColorMatrix[4, 2] = num2 = num1;
      float num3;
      newColorMatrix[4, 1] = num3 = num2;
      newColorMatrix[4, 0] = num3;
      imageAttr.SetColorMatrix(newColorMatrix);
      gr.DrawImage((Image) tc.ToolbarBM, new Rectangle(pTlb.x, pTlb.y, pTlb.width, pTlb.height), pTlb.IconX, pTlb.IconY, pTlb.width, pTlb.height, GraphicsUnit.Pixel, imageAttr);
    }
    if (this.e.ToolbarBrush != null)
      this.ColorToolbarIcon(bm, pTlb.x, pTlb.y, pTlb.width, pTlb.height);
    if ((pTlb.flags & 2) == 0 & flag3)
      this.GrayToolbarIcon(bm, pTlb.x, pTlb.y, pTlb.width, pTlb.height);
    if (flag1)
      this.CheckToolbarIcon(bm, pTlb.x, pTlb.y, pTlb.width, pTlb.height);
    if ((pTlb.flags & 8) != 0)
    {
      if (flag3)
      {
        if ((pTlb.flags & 2) != 0)
          this.HilightXpIcon(bm, pTlb.x, pTlb.y, pTlb.width, pTlb.height, (pTlb.flags & 4) != 0);
      }
      else
      {
        Pen gray = Pens.Gray;
        Pen white = Pens.White;
        bool flag4 = (pTlb.flags & 4) != 0;
        this.DrawShadowLine(gr, pTlb.x, pTlb.y, pTlb.x + pTlb.width, pTlb.y, flag4 ? gray : white, (Pen) null);
        this.DrawShadowLine(gr, pTlb.x, pTlb.y, pTlb.x, pTlb.y + pTlb.height, flag4 ? gray : white, (Pen) null);
        bool flag5 = !flag4;
        this.DrawShadowLine(gr, pTlb.x, pTlb.y + pTlb.height - 1, pTlb.x + pTlb.width, pTlb.y + pTlb.height - 1, flag5 ? gray : white, (Pen) null);
        this.DrawShadowLine(gr, pTlb.x + pTlb.width - 1, pTlb.y, pTlb.x + pTlb.width - 1, pTlb.y + pTlb.height, flag5 ? gray : white, (Pen) null);
      }
    }
    if (flag2)
    {
      Graphics graphics = Graphics.FromHwnd(this.e.TerTlb.Handle);
      Rectangle rectangle = new Rectangle(pTlb.x, pTlb.y, pTlb.width, pTlb.height);
      graphics.DrawImage((Image) bm, rectangle, rectangle, GraphicsUnit.Pixel);
      graphics.Dispose();
      gr.Dispose();
      bm.Dispose();
    }
    return true;
  }

  internal void SetComboResult(ComboBox cb, int id, string text)
  {
    this.e.Focus();
    switch (id)
    {
      case 2:
        this.e.SetTerFont(text, true);
        this.e.SendMessageToParent(2731, 1, 655, false);
        break;
      case 3:
        this.e.SetTerPointSize(-(int) (this.ToDouble(text) * 20.0), true);
        this.e.SendMessageToParent(2731, 1, 655, false);
        break;
      case 13:
        int index = ((tc.ClsBox) cb.SelectedItem).value;
        if (index >= 0 && this.e.StyleId[index].InUse && !this.SendPreprocessMessage(-15, index, 0))
        {
          if (this.e.StyleId[index].type == 1)
            this.e.TerSelectCharStyle(index, true);
          else
            this.e.TerSelectParaStyle(index, true);
          this.SendActionMessage(-15, index, 0);
          break;
        }
        break;
      case 14:
        int length = text.Length;
        if (length > 0 && text[length - 1] == '%')
          text = text.Substring(0, length - 1);
        this.e.TerSetZoom(this.ToInt(text));
        this.e.SendMessageToParent(2731, 1, 733, false);
        break;
    }
    this.PostMessage(this.e.hTerWnd, 1034, 0, 0);
  }

  internal bool TerAddToolbarIcon(int line, int id, int CmdId, string BmpFile, string pBalloon)
  {
    tc.StrTlbCustId strTlbCustId = new tc.StrTlbCustId();
    if (this.e.TotalTlbCustIds == 0)
      this.e.TlbCustId = new tc.StrTlbCustId[101];
    if (this.e.TotalTlbCustIds >= 100)
      return false;
    if (line < 0)
      line = 0;
    if (line > 1)
      line = 1;
    strTlbCustId.InUse = true;
    strTlbCustId.line = line;
    if (id > 0)
    {
      if (id >= 34)
        return false;
      strTlbCustId.id = id;
    }
    else
    {
      try
      {
        strTlbCustId.image = Image.FromFile(BmpFile);
      }
      catch (OutOfMemoryException ex)
      {
        return false;
      }
      strTlbCustId.CmdId = CmdId;
      if (pBalloon == null)
        pBalloon = "";
    }
    strTlbCustId.pBalloon = pBalloon;
    this.e.TlbCustId[this.e.TotalTlbCustIds] = strTlbCustId;
    ++this.e.TotalTlbCustIds;
    return true;
  }

  internal bool TerAddToolbarItem(int line, int id, string pBalloon, Image image, int CmdId)
  {
    tc.StrTlb strTlb1 = new tc.StrTlb();
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (line < 0 || line >= 2 || this.e.TlbItemCount[line] >= 100 || id < 0 || id >= 34)
      return false;
    int index1 = this.e.TlbItemCount[line];
    int[] tlbItemCount;
    IntPtr index2;
    (tlbItemCount = this.e.TlbItemCount)[(int) (index2 = (IntPtr) line)] = tlbItemCount[(int) index2] + 1;
    strTlb1.id = id;
    if (this.False(pBalloon))
      pBalloon = this.e.BalloonText[id];
    strTlb1.pBalloon = pBalloon;
    strTlb1.height = 26;
    strTlb1.width = 28;
    switch (id)
    {
      case 0:
        strTlb1.CmdId = CmdId;
        strTlb1.flags |= 1;
        strTlb1.image = image;
        strTlb1.id = 34;
        for (int index3 = 0; index3 < 2; ++index3)
        {
          for (int index4 = 0; index4 < this.e.TlbItemCount[index3]; ++index4)
          {
            tc.StrTlb strTlb2 = this.e.TlbId[index3][index4];
            if (strTlb2.id >= strTlb1.id)
              strTlb1.id = strTlb2.id + 1;
          }
        }
        break;
      case 1:
      case 18:
        strTlb1.width = 3;
        break;
      case 2:
        strTlb1.width = 156;
        strTlb1.height = 208 /*0xD0*/;
        strTlb1.flags |= 16 /*0x10*/;
        strTlb1.flags |= 32 /*0x20*/;
        break;
      case 3:
        strTlb1.width = 78;
        strTlb1.height = 208 /*0xD0*/;
        strTlb1.flags |= 16 /*0x10*/;
        strTlb1.flags |= 32 /*0x20*/;
        break;
      case 4:
        strTlb1.CmdId = 648;
        strTlb1.flags |= 1;
        strTlb1.IconX = 99;
        strTlb1.IconY = 182;
        break;
      case 5:
        strTlb1.CmdId = 650;
        strTlb1.flags |= 1;
        strTlb1.IconX = 128 /*0x80*/;
        strTlb1.IconY = 182;
        break;
      case 6:
        strTlb1.CmdId = 649;
        strTlb1.flags |= 1;
        strTlb1.IconX = 160 /*0xA0*/;
        strTlb1.IconY = 180;
        break;
      case 7:
        strTlb1.CmdId = 772;
        strTlb1.flags |= 1;
        strTlb1.IconX = 29;
        strTlb1.IconY = 124;
        break;
      case 8:
        strTlb1.CmdId = 658;
        strTlb1.flags |= 1;
        strTlb1.IconX = 55;
        strTlb1.IconY = 124;
        break;
      case 9:
        strTlb1.CmdId = 657;
        strTlb1.flags |= 1;
        strTlb1.IconX = 2;
        strTlb1.IconY = 125;
        break;
      case 10:
        strTlb1.CmdId = 663;
        strTlb1.flags |= 1;
        strTlb1.IconX = 1;
        strTlb1.IconY = 152;
        break;
      case 11:
        strTlb1.CmdId = 659;
        strTlb1.flags |= 1;
        strTlb1.IconX = 62;
        strTlb1.IconY = 214;
        break;
      case 12:
        strTlb1.CmdId = 785;
        strTlb1.flags |= 1;
        strTlb1.IconX = 245;
        strTlb1.IconY = 209;
        break;
      case 13:
        strTlb1.width = 130;
        strTlb1.height = 208 /*0xD0*/;
        strTlb1.flags |= 16 /*0x10*/;
        break;
      case 14:
        strTlb1.width = 71;
        strTlb1.height = 208 /*0xD0*/;
        strTlb1.flags |= 16 /*0x10*/;
        strTlb1.flags |= 32 /*0x20*/;
        break;
      case 15:
        strTlb1.CmdId = 628;
        strTlb1.flags |= 1;
        strTlb1.IconX = 157;
        strTlb1.IconY = 148;
        break;
      case 16 /*0x10*/:
        strTlb1.CmdId = 629;
        strTlb1.flags |= 1;
        strTlb1.IconX = 189;
        strTlb1.IconY = 150;
        break;
      case 17:
        strTlb1.CmdId = 630;
        strTlb1.flags |= 1;
        strTlb1.IconX = 219;
        strTlb1.IconY = 150;
        break;
      case 19:
        strTlb1.CmdId = 626;
        strTlb1.flags |= 1;
        strTlb1.IconX = 218;
        strTlb1.IconY = 120;
        break;
      case 20:
        strTlb1.CmdId = 627;
        strTlb1.flags |= 1;
        strTlb1.IconX = 158;
        strTlb1.IconY = 212;
        break;
      case 21:
        strTlb1.CmdId = 640;
        strTlb1.flags |= 1;
        strTlb1.IconX = 189;
        strTlb1.IconY = 121;
        break;
      case 22:
        strTlb1.CmdId = 643;
        strTlb1.flags |= 1;
        strTlb1.IconX = 247;
        strTlb1.IconY = 119;
        break;
      case 23:
        strTlb1.CmdId = 637;
        strTlb1.flags |= 1;
        strTlb1.IconX = 253;
        strTlb1.IconY = 93;
        break;
      case 24:
        strTlb1.CmdId = 692;
        strTlb1.flags |= 1;
        strTlb1.IconX = 252;
        strTlb1.IconY = 148;
        break;
      case 25:
        strTlb1.CmdId = 717;
        strTlb1.flags |= 1;
        strTlb1.IconX = 124;
        strTlb1.IconY = 149;
        break;
      case 26:
        strTlb1.CmdId = 748;
        strTlb1.flags |= 1;
        strTlb1.IconX = 34;
        strTlb1.IconY = 214;
        break;
      case 27:
        strTlb1.CmdId = 729;
        strTlb1.flags |= 1;
        strTlb1.IconX = 5;
        strTlb1.IconY = 214;
        break;
      case 28:
        strTlb1.CmdId = 638;
        strTlb1.flags |= 1;
        strTlb1.IconX = 92;
        strTlb1.IconY = 95;
        break;
      case 29:
        strTlb1.CmdId = 747;
        strTlb1.flags |= 1;
        strTlb1.IconX = 124;
        strTlb1.IconY = 95;
        break;
      case 30:
        strTlb1.CmdId = 633;
        strTlb1.flags |= 1;
        strTlb1.IconX = 192 /*0xC0*/;
        strTlb1.IconY = 92;
        break;
      case 31 /*0x1F*/:
        strTlb1.CmdId = 770;
        strTlb1.flags |= 1;
        strTlb1.IconX = 222;
        strTlb1.IconY = 93;
        break;
      case 32 /*0x20*/:
        strTlb1.CmdId = 719;
        strTlb1.flags |= 1;
        strTlb1.IconX = 167;
        strTlb1.IconY = 63 /*0x3F*/;
        break;
      case 33:
        strTlb1.CmdId = 752;
        strTlb1.flags |= 1;
        strTlb1.IconX = 192 /*0xC0*/;
        strTlb1.IconY = 63 /*0x3F*/;
        break;
    }
    this.e.TlbId[line][index1] = strTlb1;
    return true;
  }

  internal bool TerEditTooltip(int id, string pBalloon)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (id <= 0 || id >= 34)
      return false;
    for (int index1 = 0; index1 < 2; ++index1)
    {
      for (int index2 = 0; index2 < this.e.TlbItemCount[index1]; ++index2)
      {
        tc.StrTlb strTlb = this.e.TlbId[index1][index2];
        if (strTlb.id == id)
        {
          strTlb.pBalloon = pBalloon;
          this.e.TlbId[index1][index2] = strTlb;
        }
      }
    }
    return true;
  }

  internal bool TerHideToolbarIcon(int id, bool hide)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (id > 0 && id < 34)
    {
      for (int index1 = 0; index1 < 2; ++index1)
      {
        for (int index2 = 0; index2 < 100; ++index2)
        {
          if (this.e.TlbItem[index1][index2] == id)
          {
            this.e.TlbItemHide[index1][index2] = hide;
            if (index2 + 1 == 100 && this.e.TlbItem[index1][index2 + 1] == 18)
              this.e.TlbItemHide[index1][index2 + 1] = hide;
            return true;
          }
        }
      }
    }
    return false;
  }

  internal bool TerRecreateToolbar(bool show)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    this.FreeToolbar();
    this.InitToolbar();
    if (show)
      this.ToggleToolBar();
    return true;
  }

  internal bool TerUpdateToolbar()
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    this.e.ToolBarFillStyles = true;
    return this.UpdateToolBar(true);
  }

  internal bool TlbMouseMove(int x, int y)
  {
    bool flag1 = false;
    bool flag2 = false;
    IntPtr handle = this.e.TerTlb.Handle;
    if (x == 0 && y == 0)
    {
      for (int index1 = 0; index1 < 2; ++index1)
      {
        for (int index2 = 0; index2 < this.e.TlbItemCount[index1]; ++index2)
        {
          tc.StrTlb pTlb = this.e.TlbId[index1][index2];
          if (pTlb.id != 18 && pTlb.id != 1 && (pTlb.flags & 8) != 0)
          {
            tc.ResetUintFlag(ref pTlb.flags, 8);
            this.PaintToolbarIcon((Graphics) null, (Bitmap) null, pTlb);
          }
        }
      }
    }
    else
    {
      tc.StrTlb pTlb1;
      this.TlbMousePos(x, y, out pTlb1);
      for (int index3 = 0; index3 < 2; ++index3)
      {
        for (int index4 = 0; index4 < this.e.TlbItemCount[index3]; ++index4)
        {
          tc.StrTlb pTlb2 = this.e.TlbId[index3][index4];
          if (pTlb2.id == 18 || pTlb2.id == 1)
          {
            flag2 = true;
          }
          else
          {
            if (pTlb2.id == pTlb1.id)
            {
              if ((pTlb2.flags & 8) == 0)
              {
                if ((pTlb2.flags & 16 /*0x10*/) == 0)
                {
                  pTlb2.flags |= 8;
                  this.PaintToolbarIcon((Graphics) null, (Bitmap) null, pTlb2);
                }
                if (this.True(this.e.hBlnBM))
                {
                  this.EraseBalloon();
                  this.e.pBlnTlb = pTlb2;
                  this.PaintBalloon();
                }
                else
                {
                  this.e.pBlnTlb = pTlb2;
                  this.SetTimer(handle, 9186, 1200);
                }
              }
              this.SetTimer(handle, 9185, 50);
              if ((pTlb2.flags & 16 /*0x10*/) == 0)
                flag1 = true;
            }
            else if ((pTlb2.flags & 8) != 0)
            {
              tc.ResetUintFlag(ref pTlb2.flags, 8);
              this.PaintToolbarIcon((Graphics) null, (Bitmap) null, pTlb2);
            }
            this.e.TlbId[index3][index4] = pTlb2;
          }
        }
      }
    }
    if (!flag1 && !flag2 || x == 0 && y == 0)
    {
      if (this.True(this.e.hBlnBM))
        this.EraseBalloon();
      this.KillTimer(handle, 9186);
      this.KillTimer(handle, 9187);
      if (x == 0 && y == 0)
        this.KillTimer(handle, 9185);
    }
    return true;
  }

  internal bool TlbMousePos(int x, int y, out tc.StrTlb pTlb)
  {
    pTlb = new tc.StrTlb();
    for (int index1 = 0; index1 < 2; ++index1)
    {
      for (int index2 = 0; index2 < this.e.TlbItemCount[index1]; ++index2)
      {
        pTlb = this.e.TlbId[index1][index2];
        int num = (pTlb.flags & 16 /*0x10*/) != 0 ? 26 : pTlb.height;
        if (x >= pTlb.x && x <= pTlb.x + pTlb.width && y >= pTlb.y && y <= pTlb.y + num)
          return true;
      }
    }
    pTlb.id = -1;
    return false;
  }

  internal void TlbTimer(int TimerId)
  {
    IntPtr handle = this.e.TerTlb.Handle;
    Point pt;
    this.GetCursorPos(out pt);
    COp.RECT rect = this.ToRect(this.e.TerTlb.RectangleToScreen(this.e.TerTlb.ClientRectangle));
    bool flag = pt.X >= rect.left && pt.Y >= rect.top && pt.X < rect.right && pt.Y < rect.bottom;
    switch (TimerId)
    {
      case 9185:
        if (flag)
          break;
        this.TlbMouseMove(0, 0);
        break;
      case 9186:
        this.KillTimer(handle, 9186);
        if (!flag)
          break;
        this.PaintBalloon();
        break;
      case 9187:
        this.KillTimer(handle, 9187);
        if (!this.True(this.e.hBlnBM))
          break;
        this.EraseBalloon();
        break;
    }
  }

  internal new bool ToggleToolBar()
  {
    if (this.e.UseWin)
    {
      this.e.TerArg.ToolBar = !this.e.TerArg.ToolBar;
      if (this.e.TerArg.ToolBar)
        this.CreateToolBar();
      else
        this.DestroyToolBar();
      this.e.Invalidate((Region) null);
    }
    return true;
  }

  internal int ToolbarCtlToId(object Sender)
  {
    int id = -1;
    for (int index1 = 0; index1 < 2; ++index1)
    {
      for (int index2 = 0; index2 < this.e.TlbItemCount[index1]; ++index2)
      {
        if ((this.e.TlbId[index1][index2].flags & 16 /*0x10*/) != 0 && this.e.TlbId[index1][index2].ctl == (ComboBox) Sender)
        {
          id = this.e.TlbId[index1][index2].id;
          break;
        }
      }
    }
    return id;
  }

  internal new bool UpdateToolBar(bool always)
  {
    bool flag = true;
    COp.RECT rect = new COp.RECT();
    if (!this.e.CaretEngaged && !always)
      return false;
    if (this.e.MessageId != 132 && this.e.MessageId != 32 /*0x20*/ && this.e.MessageId != 160 /*0xA0*/)
    {
      int num = this.e.Focused ? 1 : 0;
      if (this.e.TerTlb != null)
      {
        if (!always && !this.CheckAndPaintToolbar((Graphics) null, (Bitmap) null, rect, true))
          flag = false;
        if (flag)
        {
          Graphics DestGr = Graphics.FromHwnd(this.e.TerTlb.Handle);
          this.PaintToolbar(DestGr);
          DestGr.Dispose();
        }
      }
      this.e.SendMessageToParent(2729, (int) this.e.hTerWnd, 0, false);
      if (num != 0)
        this.e.Focus();
    }
    return true;
  }
}
