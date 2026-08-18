// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.CFld
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class CFld : COp
{
  internal CFld(ImRtfEditor ImRtfEditor)
    : base(ImRtfEditor)
  {
    this.e = ImRtfEditor;
  }

  internal void CtlGotFocus(object Sender, EventArgs ev)
  {
    int pict = 0;
    while (pict < this.e.TotalFonts && (!this.IsControl(pict) || this.e.TerFont[pict].ctl != (Control) Sender))
      ++pict;
    if (pict == this.e.TotalFonts)
      return;
    int index;
    this.e.CurInputField = index = pict;
    tc.ClsForm form = this.e.TerFont[index].form;
    Control ctl = this.e.TerFont[index].ctl;
    if (!(form.CtlClass == "CheckBox"))
      return;
    ctl.ForeColor = Color.DarkBlue;
    ((ButtonBase) ctl).FlatStyle = FlatStyle.Flat;
  }

  internal void CtlLostFocus(object Sender, EventArgs ev)
  {
    int pict = 0;
    while (pict < this.e.TotalFonts && (!this.IsControl(pict) || this.e.TerFont[pict].ctl != (Control) Sender))
      ++pict;
    if (pict == this.e.TotalFonts)
      return;
    tc.ClsForm form = this.e.TerFont[pict].form;
    Control ctl = this.e.TerFont[pict].ctl;
    if (form.CtlClass == "TextBox")
    {
      if (ctl == null)
        return;
      ((TextBoxBase) ctl).SelectionStart = 0;
      ((TextBoxBase) ctl).SelectionLength = 0;
    }
    else
    {
      if (!(form.CtlClass == "CheckBox") || ctl == null)
        return;
      ctl.ForeColor = Color.Black;
      ((ButtonBase) ctl).FlatStyle = FlatStyle.Standard;
    }
  }

  internal void CtlModified(object Sender, EventArgs ev) => ++this.e.TerArg.modified;

  internal new bool EditingInputField(bool del, bool bksp)
  {
    if (!this.e.TerArg.ReadOnly || !this.e.ProtectForm)
      return false;
    if (this.e.HilightType == 0)
    {
      if (del | bksp)
      {
        int curLine = this.e.CurLine;
        int curCol = this.e.CurCol;
        if (bksp)
          this.PrevTextPos(ref curLine, ref curCol);
        int pLineNo1 = curLine;
        int pCol1 = curCol;
        this.PrevTextPos(ref pLineNo1, ref pCol1);
        int num = this.True(this.e.TerFont[this.GetCurCfmt(pLineNo1, pCol1)].FieldId == 2) ? 1 : 0;
        int pLineNo2 = curLine;
        int pCol2 = curCol;
        this.NextTextPos(ref pLineNo2, ref pCol2);
        bool flag = this.True(this.e.TerFont[this.GetCurCfmt(pLineNo2, pCol2)].FieldId == 2);
        if (num == 0 && !flag)
          return false;
      }
      int curCfmt1 = this.GetCurCfmt(this.e.CurLine, this.e.CurCol);
      if (del && this.False(this.e.TerFont[curCfmt1].FieldId == 2))
        return false;
      if (!bksp && this.True(this.e.TerFont[curCfmt1].FieldId == 2))
        return true;
      int curLine1 = this.e.CurLine;
      int curCol1 = this.e.CurCol;
      this.PrevTextPos(ref curLine1, ref curCol1);
      int curCfmt2 = this.GetCurCfmt(curLine1, curCol1);
      return (!bksp || !this.False(this.e.TerFont[curCfmt2].FieldId == 2)) && this.e.TerArg.ReadOnly && this.True(this.e.TerFont[curCfmt2].FieldId == 2);
    }
    int hilightBegRow = this.e.HilightBegRow;
    int hilightBegCol = this.e.HilightBegCol;
    int hilightEndRow = this.e.HilightEndRow;
    int hilightEndCol = this.e.HilightEndCol;
    bool stretchHilight = this.e.StretchHilight;
    this.NormalizeBlock();
    this.e.HilightBegRow = this.SwapInts(ref hilightBegRow, this.e.HilightBegRow);
    this.e.HilightBegCol = this.SwapInts(ref hilightBegCol, this.e.HilightBegCol);
    this.e.HilightEndRow = this.SwapInts(ref hilightEndRow, this.e.HilightEndRow);
    this.e.HilightEndCol = this.SwapInts(ref hilightEndCol, this.e.HilightEndCol);
    this.e.StretchHilight = this.SwapBools(ref stretchHilight, this.e.StretchHilight);
    if (this.False(this.e.TerFont[this.GetCurCfmt(hilightBegRow, hilightBegCol)].FieldId == 2))
      return false;
    int pLineNo3 = hilightEndRow;
    int pCol3 = hilightEndCol;
    this.PrevTextPos(ref pLineNo3, ref pCol3);
    if (this.False(this.e.TerFont[this.GetCurCfmt(pLineNo3, pCol3)].FieldId == 2))
      return false;
    if (del | bksp && this.False(this.e.TerFont[this.GetCurCfmt(hilightEndRow, hilightEndCol)].FieldId == 2))
    {
      if (hilightBegRow == 0 && hilightBegCol == 0)
        return false;
      int pLineNo4 = hilightBegRow;
      int pCol4 = hilightBegCol;
      this.PrevTextPos(ref pLineNo4, ref pCol4);
      if (this.False(this.e.TerFont[this.GetCurCfmt(pLineNo4, pCol4)].FieldId == 2))
        return false;
    }
    return true;
  }

  internal new bool EditInputField()
  {
    int index = this.e.CurInputField;
    if (this.IsFormField(index, 0))
    {
      tc.ClsForm form = this.e.TerFont[index].form;
      this.e.DlgText1 = form.name;
      this.e.DlgTypeface = form.typeface;
      this.e.DlgInt1 = form.MaxLen;
      this.e.DlgBool1 = form.border;
      this.e.DlgInt3 = form.TwipsSize;
      this.e.DlgInt4 = form.FontStyle;
      this.e.DlgInt5 = this.e.TerFont[index].PictWidth;
      this.e.DlgInt6 = this.e.TerFont[index].FieldId;
      this.e.DlgColor1 = form.TextColor;
    }
    else
    {
      int curCfmt = this.GetCurCfmt(this.e.CurLine, this.e.CurCol);
      index = this.e.TerFont[curCfmt].AuxId;
      if (!this.FindTextInputField(index))
        return false;
      this.e.DlgText1 = this.GetStringField(this.e.TerFont[curCfmt].FieldCode, 1, '|');
      this.e.DlgTypeface = this.e.TerFont[curCfmt].TypeFace;
      this.e.DlgInt1 = this.ToInt(this.GetStringField(this.e.TerFont[curCfmt].FieldCode, 0, '|'));
      this.e.DlgBool1 = (this.e.TerFont[curCfmt].style & 8192 /*0x2000*/) != 0;
      this.e.DlgInt3 = this.e.TerFont[curCfmt].TwipsSize;
      this.e.DlgInt4 = this.e.TerFont[curCfmt].style;
      this.e.DlgInt4 = tc.ResetFlag(this.e.DlgInt4, 8192 /*0x2000*/);
      this.e.DlgInt5 = this.e.TerFont[curCfmt].PictWidth;
      this.e.DlgInt6 = this.e.TerFont[curCfmt].FieldId;
      this.e.DlgColor1 = this.e.TerFont[curCfmt].TextColor;
    }
    if (!this.CallDialogBox((Form) new terdlg_edit_input_field(this.e)))
      return false;
    string dlgTypeface = this.e.DlgTypeface;
    this.TerSetInputFieldInfo(index, this.e.DlgText1, this.e.DlgBool1);
    this.TerSetTextFieldInfo(index, (string) null, this.e.DlgInt1, this.e.DlgInt5, dlgTypeface, this.e.DlgInt3, this.e.DlgInt4);
    return true;
  }

  internal new bool FieldFound(int LineNo, int ColNo, string name, bool exact)
  {
    bool flag = false;
    string text;
    int field = this.TerGetField(LineNo, ColNo, 6, out text);
    if (exact)
    {
      if (this.False(name))
        return false;
      if (name == text)
        flag = true;
      return flag;
    }
    if (name == null)
      return true;
    int num = name.Length;
    if (num > field)
      num = field;
    int index = 0;
    while (index < num && (int) text[index] == (int) name[index])
      ++index;
    if (index == num)
      flag = true;
    return flag;
  }

  internal bool FindTextInputField(int id)
  {
    int StartLine = 0;
    int StartCol = 0;
    while (this.e.TerLocateFieldChar(2, (string) null, true, ref StartLine, ref StartCol, true))
    {
      int curCfmt = this.GetCurCfmt(StartLine, StartCol);
      if (this.e.TerFont[curCfmt].AuxId == id)
      {
        this.e.CurLine = StartLine;
        this.e.CurCol = StartCol;
        return true;
      }
      if (!this.e.TerLocateFieldChar(2, this.e.TerFont[curCfmt].FieldCode, false, ref StartLine, ref StartCol, true))
        return false;
    }
    return false;
  }

  internal new bool GetFieldLoc(int LineNo, int ColNo, bool begin, out int pLine, out int pCol)
  {
    int col = 0;
    bool flag1 = true;
    int curCfmt1 = this.GetCurCfmt(LineNo, ColNo);
    int fieldId = this.e.TerFont[curCfmt1].FieldId;
    string fieldCode = this.e.TerFont[curCfmt1].FieldCode;
    int line;
    if (begin)
    {
      for (line = LineNo; line >= 0; --line)
      {
        for (col = line == LineNo ? ColNo : this.e.text[line].len - 1; col >= 0; --col)
        {
          int curCfmt2 = this.GetCurCfmt(line, col);
          bool flag2 = fieldId != this.e.TerFont[curCfmt2].FieldId;
          if (!flag2 && fieldCode != this.e.TerFont[curCfmt2].FieldCode)
            flag2 = !this.IsSameFieldCode(fieldCode, this.e.TerFont[curCfmt2].FieldCode);
          if (flag2)
          {
            ++col;
            if (col >= this.e.text[line].len)
            {
              ++line;
              col = 0;
              break;
            }
            break;
          }
          if (fieldId == 6 && fieldId == this.e.TerFont[curCfmt2].FieldId && (this.e.TerFont[curCfmt2].style & 512 /*0x0200*/) != 0 && this.GetCurChar(line, col) == '{')
            break;
        }
        if (col >= 0)
          break;
      }
      if (line < 0)
      {
        line = 0;
        col = 0;
      }
    }
    else
    {
      for (line = LineNo; line < this.e.TotalLines; ++line)
      {
        for (col = line == LineNo ? ColNo : 0; col < this.e.text[line].len; ++col)
        {
          int curCfmt3 = this.GetCurCfmt(line, col);
          bool flag3 = fieldId != this.e.TerFont[curCfmt3].FieldId;
          if (!flag3 && fieldCode != this.e.TerFont[curCfmt3].FieldCode)
            flag3 = !this.IsSameFieldCode(fieldCode, this.e.TerFont[curCfmt3].FieldCode);
          if (!flag3 && (fieldId != 6 || fieldId != this.e.TerFont[curCfmt3].FieldId || (this.e.TerFont[curCfmt3].style & 512 /*0x0200*/) == 0 || this.GetCurChar(line, col) != '{' || flag1))
            flag1 = false;
          else
            break;
        }
        if (col < this.e.text[line].len)
          break;
      }
      if (line == this.e.TotalLines)
      {
        line = this.e.TotalLines - 1;
        col = this.e.text[line].len;
      }
    }
    pLine = line;
    pCol = col;
    return true;
  }

  internal new bool GetFieldScope(int LineNo, int ColNo, int type)
  {
    int pBegLine;
    int pBegCol;
    int pEndLine;
    int pEndCol;
    int num = this.GetFieldScope(LineNo, ColNo, type, out pBegLine, out pBegCol, out pEndLine, out pEndCol) ? 1 : 0;
    this.e.HilightBegRow = pBegLine;
    this.e.HilightBegCol = pBegCol;
    this.e.HilightEndRow = pEndLine;
    this.e.HilightEndCol = pEndCol;
    return num != 0;
  }

  internal new bool GetFieldScope(
    int LineNo,
    int ColNo,
    int type,
    out int pBegLine,
    out int pBegCol,
    out int pEndLine,
    out int pEndCol)
  {
    int num1;
    pEndCol = num1 = 0;
    int num2;
    pEndLine = num2 = num1;
    int num3;
    pBegCol = num3 = num2;
    pBegLine = num3;
    int fieldId = this.e.TerFont[this.GetCurCfmt(LineNo, ColNo)].FieldId;
    switch (fieldId)
    {
      case 6:
      case 7:
        if (type == 6 && fieldId == 7)
        {
          this.GetFieldLoc(LineNo, ColNo, true, out LineNo, out ColNo);
          --ColNo;
          this.FixPos(ref LineNo, ref ColNo);
        }
        else if (type == 7 && fieldId == 6)
          this.GetFieldLoc(LineNo, ColNo, false, out LineNo, out ColNo);
        if (this.e.TerFont[this.GetCurCfmt(LineNo, ColNo)].FieldId != type)
          return false;
        this.GetFieldLoc(LineNo, ColNo, true, out pBegLine, out pBegCol);
        this.GetFieldLoc(LineNo, ColNo, false, out pEndLine, out pEndCol);
        return true;
      default:
        return false;
    }
  }

  internal new ushort GetNewFieldId(
    ushort OldFmt,
    int FieldId,
    string FieldCode,
    int line,
    int col)
  {
    int OldFont = (int) OldFmt;
    int style = this.e.TerFont[OldFont].style;
    int newFont;
    return (newFont = this.GetNewFont(this.e.TerGr, OldFont, this.e.TerFont[OldFont].TypeFace, this.e.TerFont[OldFont].TwipsSize, style, this.e.TerFont[OldFont].TextColor, this.e.TerFont[OldFont].TextBkColor, this.e.TerFont[OldFont].UlineColor, FieldId, this.e.TerFont[OldFont].AuxId, this.e.TerFont[OldFont].Aux1Id, this.e.TerFont[OldFont].CharStyId, this.e.TerFont[OldFont].ParaStyId, this.e.TerFont[OldFont].expand, this.e.TerFont[OldFont].TempStyle, this.e.TerFont[OldFont].lang, FieldCode, this.e.TerFont[OldFont].offset, this.e.TerFont[OldFont].CharSet, this.e.TerFont[OldFont].flags, this.e.TerFont[OldFont].TextAngle)) >= 0 ? (ushort) newFont : OldFmt;
  }

  internal new bool HideControl(int ctl) => true;

  internal new bool InsertDynField(int FieldId, string FieldCode)
  {
    if (this.e.text[this.e.CurLine].len < 0 || this.e.CurCol < this.e.text[this.e.CurLine].len)
    {
      this.e.HilightType = 0;
      int newFieldId = (int) this.GetNewFieldId((ushort) this.GetCurCfmt(this.e.CurLine, this.e.CurCol), FieldId, FieldCode, this.e.CurLine, this.e.CurCol);
      this.e.InputFontId = -1;
      if (newFieldId < 0 || this.e.TerFont[newFieldId].FieldId != FieldId)
        return false;
      int FieldFont = this.SetCurLangFont(newFieldId);
      string DateString;
      if (FieldId == 8)
        this.GetDateString(FieldCode, out DateString, FieldFont);
      else
        DateString = (this.e.CurPage + 1).ToString();
      int length = DateString.Length;
      this.MoveLineData(this.e.CurLine, this.e.CurCol, length, 'B');
      this.SaveUndo(this.e.CurLine, this.e.CurCol, this.e.CurLine, this.e.CurCol + length - 1, 'I');
      char[] txt = this.e.text[this.e.CurLine].txt;
      ushort[] numArray = this.OpenCfmt(this.e.CurLine);
      for (int curCol = this.e.CurCol; curCol < this.e.CurCol + length; ++curCol)
      {
        txt[curCol] = DateString[curCol - this.e.CurCol];
        numArray[curCol] = (ushort) FieldFont;
      }
      this.e.CurCol += length;
      this.CloseCfmt(this.e.CurLine);
      this.PaintTer();
    }
    return true;
  }

  internal new bool IsControl(int pict)
  {
    if (pict < 0 || pict >= this.e.TotalFonts || !this.e.TerFont[pict].InUse || (this.e.TerFont[pict].style & 128 /*0x80*/) == 0)
      return false;
    return this.e.TerFont[pict].PictType == 2 || this.e.TerFont[pict].PictType == 6;
  }

  internal new bool IsDynField(int FieldId)
  {
    return FieldId == 1 || FieldId == 5 || FieldId == 17 || FieldId == 8 || FieldId == 10 || FieldId == 11 || FieldId == 12 || FieldId == 16 /*0x10*/;
  }

  internal new bool IsFormField(int pict, int type)
  {
    if (pict < 0 || pict >= this.e.TotalFonts || !this.e.TerFont[pict].InUse)
      return false;
    int fieldId = this.e.TerFont[pict].FieldId;
    switch (fieldId)
    {
      case 2:
      case 3:
      case 4:
        return (type == 0 || type == fieldId) && (fieldId == 2 || (this.e.TerFont[pict].style & 128 /*0x80*/) != 0 && this.e.TerFont[pict].PictType == 6);
      default:
        return false;
    }
  }

  internal new bool IsSameField(int font1, int font2)
  {
    if (this.e.TerFont[font1].FieldId == 0 || this.e.TerFont[font2].FieldId == 0)
      return false;
    if (font1 == font2)
      return true;
    return this.e.TerFont[font1].FieldId == this.e.TerFont[font2].FieldId && this.IsSameFieldCode(this.e.TerFont[font1].FieldCode, this.e.TerFont[font2].FieldCode);
  }

  internal new bool IsSameFieldCode(string code1, string code2)
  {
    if (code1 == null && code2 == null)
      return true;
    return code1 != null && code2 != null && code1 == code2;
  }

  internal new bool RealizeControl(int pict, Control ctl)
  {
    tc.ClsForm form = this.e.TerFont[pict].form;
    int scrX = this.TwipsToScrX(this.e.TerFont[pict].PictWidth);
    int scrY = this.TwipsToScrY(this.e.TerFont[pict].PictHeight);
    if (ctl == null)
    {
      if (form.CtlClass == null)
        form.CtlClass = "TextBox";
      ctl = this.strcmpi(form.CtlClass, "TextBox") != 0 ? (this.strcmpi(form.CtlClass, "CheckBox") != 0 ? (this.strcmpi(form.CtlClass, "ComboBox") != 0 ? (this.strcmpi(form.CtlClass, "RadioButton") != 0 ? (this.strcmpi(form.CtlClass, "Button") != 0 ? (Control) new TextBox() : (Control) new Button()) : (Control) new RadioButton()) : (Control) new ComboBox()) : (Control) new CheckBox()) : (Control) new TextBox();
    }
    this.e.TerFont[pict].ctl = ctl;
    ctl.Width = scrX;
    ctl.Height = scrY;
    if (this.e.UseWin)
      ctl.Parent = (Control) this.e;
    ctl.Visible = false;
    if (!ctl.Created && this.e.UseWin)
      ctl.CreateControl();
    if (form.CtlClass == "CheckBox")
      ctl.BackColor = this.e.StatusBkColor;
    if (form.CtlClass == "RadioButton")
      ctl.BackColor = this.PageColor();
    this.e.CurCtl = ctl;
    this.e.CurInputField = pict;
    if (this.e.TerFont[pict].PictType == 6 && this.e.TerFont[pict].FieldId == 2)
    {
      ctl.Text = form.InitText;
      this.SetTextInputFieldWnd(pict, this.e.TerFont[pict].PictWidth);
    }
    else if (this.e.TerFont[pict].PictType == 6 && this.e.TerFont[pict].FieldId == 3)
      ((CheckBox) ctl).Checked = this.True(form.InitNum);
    if (form.CtlClass == "CheckBox" || form.CtlClass == "TextBox" || form.CtlClass == "Button" || form.CtlClass == "RadioButton" || form.CtlClass == "ComboBox")
    {
      ctl.GotFocus += new EventHandler(this.CtlGotFocus);
      ctl.LostFocus += new EventHandler(this.CtlLostFocus);
    }
    if (form.CtlClass == "TextBox")
      ((TextBoxBase) ctl).BorderStyle = form.border ? BorderStyle.Fixed3D : BorderStyle.None;
    if (form.CtlClass == "CheckBox")
      ((CheckBox) ctl).CheckedChanged += new EventHandler(this.CtlModified);
    else if (form.CtlClass == "RadioButton")
      ((RadioButton) ctl).CheckedChanged += new EventHandler(this.CtlModified);
    else if (form.CtlClass == "TextBox")
      ((TextBoxBase) ctl).ModifiedChanged += new EventHandler(this.CtlModified);
    this.SetPictSize(pict, this.TwipsToScrY(this.e.TerFont[pict].PictHeight), this.TwipsToScrX(this.e.TerFont[pict].PictWidth), true);
    this.e.TerFont[pict].bmHeight = this.e.TerFont[pict].height;
    this.e.TerFont[pict].bmWidth = this.e.TerFont[pict].CharWidth[24];
    this.e.TerFont[pict].TwipsSize = this.e.TerFont[pict].PictHeight;
    this.XlateSizeForPrt(pict);
    int pictType = this.e.TerFont[pict].PictType;
    return true;
  }

  internal new bool SelectFirstFormField()
  {
    int pict = 0;
    while (pict < this.e.TotalFonts && !this.IsFormField(pict, 0))
      ++pict;
    if (pict != this.e.TotalFonts)
    {
      int abs = 0;
      while ((abs = this.TerGetNextControlPos(abs - 1)) >= 0)
      {
        int row;
        int col;
        this.AbsToRowCol(abs, out row, out col);
        int curFont = this.e.TerGetCurFont(row, col);
        if (this.IsFormField(curFont, 0))
          return this.SelectFormField(curFont);
      }
    }
    return true;
  }

  internal bool SelectFormField(int pict)
  {
    if (this.e.TerFont[pict].FieldId == 2)
    {
      this.FindTextInputField(this.e.TerFont[pict].AuxId);
      this.e.SetTerCursorPos(this.e.CurLine, this.e.CurCol, !this.IsTextPosVisible(this.e.CurLine, this.e.CurCol));
      this.SelectTextInputField(true);
      this.e.CurCtl = (Control) null;
    }
    else
    {
      Control ctl = this.e.TerFont[pict].ctl;
      int controlPos = this.TerGetControlPos(ctl);
      if (controlPos < 0)
        return true;
      this.e.CurCtl = ctl;
      int row;
      int col;
      this.AbsToRowCol(controlPos, out row, out col);
      this.e.SetTerCursorPos(controlPos, -1, !this.IsTextPosVisible(row, col));
      ctl.Focus();
    }
    return true;
  }

  internal bool SelectTextInputField(bool repaint)
  {
    return this.SelectTextInputField2(this.e.CurLine, this.e.CurCol, repaint);
  }

  internal bool SelectTextInputField2(int InitLine, int InitCol, bool repaint)
  {
    int curCfmt = this.GetCurCfmt(InitLine, InitCol);
    if (this.e.TerFont[curCfmt].FieldId != 2)
      return false;
    int num1 = InitLine;
    int num2 = InitCol;
    if (!this.e.TerLocateFieldChar(2, this.e.TerFont[curCfmt].FieldCode, false, ref num1, ref num2, false))
    {
      this.e.HilightBegRow = this.e.HilightBegCol = 0;
    }
    else
    {
      this.NextTextPos(ref num1, ref num2);
      this.e.HilightBegRow = num1;
      this.e.HilightBegCol = num2;
    }
    num1 = InitLine;
    int StartCol = InitCol;
    if (!this.e.TerLocateFieldChar(2, this.e.TerFont[curCfmt].FieldCode, false, ref num1, ref StartCol, true))
    {
      this.e.HilightEndRow = this.e.TotalLines - 1;
      this.e.HilightEndCol = this.e.text[this.e.TotalLines - 1].len;
    }
    else
    {
      this.e.HilightEndRow = num1;
      this.e.HilightEndCol = StartCol;
    }
    this.e.HilightType = 2;
    this.e.StretchHilight = false;
    this.e.Focus();
    if (repaint)
      this.PaintTer();
    return true;
  }

  internal new int SetFontFieldId(int CurFont, int FieldId, string FieldCode)
  {
    return (this.e.TerFont[CurFont].style & 128 /*0x80*/) != 0 ? CurFont : this.GetNewFont(this.e.TerGr, CurFont, this.e.TerFont[CurFont].TypeFace, this.e.TerFont[CurFont].TwipsSize, this.e.TerFont[CurFont].style, this.e.TerFont[CurFont].TextColor, this.e.TerFont[CurFont].TextBkColor, this.e.TerFont[CurFont].UlineColor, FieldId, this.e.TerFont[CurFont].AuxId, this.e.TerFont[CurFont].Aux1Id, this.e.TerFont[CurFont].CharStyId, this.e.TerFont[CurFont].ParaStyId, this.e.TerFont[CurFont].expand, this.e.TerFont[CurFont].TempStyle, this.e.TerFont[CurFont].lang, FieldCode, this.e.TerFont[CurFont].offset, this.e.TerFont[CurFont].CharSet, this.e.TerFont[CurFont].flags, this.e.TerFont[CurFont].TextAngle);
  }

  internal new bool SetTextInputFieldWnd(int pict, int CurCtlWidth)
  {
    tc.ClsForm form = this.e.TerFont[pict].form;
    TextBox ctl = (TextBox) this.e.TerFont[pict].ctl;
    int NewTempStyle = 0;
    int num1 = CurCtlWidth;
    int num2 = this.TwipsToScrX(this.e.TerFont[pict].PictWidth);
    int num3 = this.TwipsToScrX(this.e.TerFont[pict].PictHeight);
    int fontStyle = form.FontStyle;
    if ((fontStyle & 1) != 0)
      NewTempStyle |= 2;
    if (form.FontId < 0)
      form.FontId = this.GetNewFont(this.e.TerGr, 0, form.typeface, form.TwipsSize, fontStyle & 7, form.TextColor, form.TextBkColor, tc.CLR_AUTO, 0, 0, 0, 0, 0, 0, NewTempStyle, 0, (string) null, 0, (byte) form.CharSet, 0, 0);
    if (form.TextBkColor != Color.White)
      ctl.BackColor = form.TextBkColor;
    if (form.FontId >= 0)
    {
      int fontId = form.FontId;
      Font font = this.e.TerFont[fontId].font;
      ctl.Font = this.e.TerFont[form.FontId].font;
      this.e.TerFont[pict].PictAlign = 1;
      if (form.border)
        this.e.TerFont[pict].height = this.e.TerFont[fontId].height * 5 / 4;
      else
        this.e.TerFont[pict].height = this.e.TerFont[fontId].height;
      this.e.TerFont[pict].PictHeight = this.ScrToTwipsY(this.e.TerFont[pict].height);
      int length1 = ctl.Text.Length;
      if (length1 > 0 && (length1 == form.MaxLen || form.MaxLen == 0))
      {
        int num4 = this.e.TerWinWidth * 5 / 6;
        COp.SIZE size;
        this.GetTextExtentPoint(this.e.TerGr, font, ctl.Text, length1, out size);
        size.cx += 10;
        if ((form.flags & 2) != 0)
          num4 = this.TwipsToScrX(this.e.TerFont[pict].PictWidth) + 10;
        int length2 = form.InitText.Length;
        int num5 = 0;
        for (int index = 0; index < length2; ++index)
        {
          if ((int) form.InitText[index] == (int) this.e.ParaChar)
            ++num5;
        }
        if (size.cx > num4 || num5 > 0)
        {
          int num6 = size.cx / num4 + 1 + num5;
          while (true)
          {
            size.cx = num4;
            this.e.TerFont[pict].height *= num6;
            this.e.TerFont[pict].PictHeight = this.ScrToTwipsY(this.e.TerFont[pict].height);
            if (!ctl.Multiline)
            {
              ctl.Multiline = true;
              ctl.Height = num3 = this.e.TerFont[pict].height;
              if ((form.flags & 2) != 0)
                size.cx = this.TwipsToScrX(this.e.TerFont[pict].PictWidth);
              ctl.Width = num1 = size.cx;
              if (num5 == 0)
              {
                ctl.Text = form.InitText;
              }
              else
              {
                int length3 = form.InitText.Length;
                char[] chArray = new char[length2 + 1 + num5];
                int length4 = 0;
                int index = 0;
                while (index < length3 + 1)
                {
                  if ((int) form.InitText[index] == (int) this.e.ParaChar)
                  {
                    chArray[length4] = '\r';
                    ++length4;
                    chArray[length4] = '\n';
                  }
                  else
                    chArray[length4] = form.InitText[index];
                  ++index;
                  ++length4;
                }
                ctl.Text = new string(chArray, 0, length4);
              }
            }
            if (ctl.Multiline)
            {
              int length5 = ctl.Lines.Length;
              if (length5 != num6)
                num6 = length5;
              else
                break;
            }
            else
              break;
          }
        }
        this.e.TerFont[pict].PictWidth = this.ScrToTwipsX(size.cx);
        num2 = size.cx;
      }
      if (this.e.TerFont[pict].height != num3 || num2 != num1)
      {
        int height;
        ctl.Height = height = this.e.TerFont[pict].height;
        int num7;
        ctl.Width = num7 = num2;
      }
      ctl.MaxLength = form.MaxLen;
    }
    return true;
  }

  internal new bool TabOnControl(bool ShiftPressed)
  {
    bool flag = ShiftPressed;
    int curCfmt = this.GetCurCfmt(this.e.CurLine, this.e.CurCol);
    int abs1;
    int num;
    if (this.e.TerFont[curCfmt].FieldId == 2)
    {
      int auxId = this.e.TerFont[curCfmt].AuxId;
      this.SelectTextInputField(false);
      num = !flag ? (abs1 = this.RowColToAbs(this.e.HilightEndRow, this.e.HilightEndCol) - 1) : (abs1 = this.RowColToAbs(this.e.HilightBegRow, this.e.HilightBegCol));
      this.e.DeselectTerText(false);
    }
    else
    {
      int pict = 0;
      while (pict < this.e.TotalFonts && (!this.IsControl(pict) || this.e.TerFont[pict].ctl == null || !this.e.TerFont[pict].ctl.Focused))
        ++pict;
      if (pict == this.e.TotalFonts)
        return false;
      ref tc.StrFont local = ref this.e.TerFont[pict];
      int index = pict;
      num = abs1 = this.TerGetControlPos(this.e.TerFont[index].ctl);
      if (abs1 < 0)
        return false;
    }
    int curFont;
    if (flag)
    {
      int row;
      int col;
      while ((abs1 = this.TerGetPrevControlPos(abs1 - 1)) >= 0)
      {
        this.AbsToRowCol(abs1, out row, out col);
        curFont = this.e.TerGetCurFont(row, col);
        if (this.e.TerFont[curFont].FieldId == 2 || this.IsControl(curFont))
          goto label_21;
      }
      int abs2 = this.RowColToAbs(this.e.TotalLines - 1, this.e.text[this.e.TotalLines - 1].len);
      while ((abs2 = this.TerGetPrevControlPos(abs2 - 1)) >= 0 && abs2 > num)
      {
        this.AbsToRowCol(abs2, out row, out col);
        curFont = this.e.TerGetCurFont(row, col);
        if (this.e.TerFont[curFont].FieldId == 2 || this.IsControl(curFont))
          goto label_21;
      }
    }
    else
    {
      int row;
      int col;
      while ((abs1 = this.TerGetNextControlPos(abs1 + 1)) >= 0)
      {
        this.AbsToRowCol(abs1, out row, out col);
        curFont = this.e.TerGetCurFont(row, col);
        if (this.e.TerFont[curFont].FieldId == 2 || this.IsControl(curFont))
          goto label_21;
      }
      int abs3 = 0;
      while ((abs3 = this.TerGetNextControlPos(abs3 - 1)) >= 0 && abs3 < num)
      {
        this.AbsToRowCol(abs3, out row, out col);
        curFont = this.e.TerGetCurFont(row, col);
        if (this.e.TerFont[curFont].FieldId == 2 || this.IsControl(curFont))
          goto label_21;
      }
    }
    return false;
label_21:
    this.SelectFormField(curFont);
    return true;
  }

  internal bool TerChangeField(string name, string data, bool repaint)
  {
    int loc = 0;
    int num = -1;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    while (this.TerLocateField(loc, name, true, false))
    {
      int curCol = this.e.CurCol;
      int curLine = this.e.CurLine;
      int pLine;
      int pCol;
      if (this.GetFieldScope(this.e.CurLine, this.e.CurCol, 7))
      {
        pLine = this.e.HilightBegRow;
        pCol = this.e.HilightBegCol;
        num = this.GetCurCfmt(pLine, pCol);
        this.e.HilightType = 2;
        this.e.StretchHilight = false;
        this.e.TerOpFlags |= 32768 /*0x8000*/;
        this.e.TerDeleteBlock(false);
        this.e.HilightType = 0;
        this.e.TerOpFlags &= -32769;
      }
      else if (!this.GetFieldLoc(this.e.CurLine, this.e.CurCol, false, out pLine, out pCol))
        return false;
      this.e.CurLine = pLine;
      this.e.CurCol = pCol;
      if (data.Length > 0)
      {
        this.e.HilightType = 0;
        if (num >= 0)
          this.e.InputFontId = num;
        else
          this.e.InputFontId = (int) this.GetNewFieldId((ushort) this.GetNextCfmt(curLine, curCol), 7, (string) null, this.e.CurLine, this.e.CurCol);
        this.e.TerOpFlags2 |= 1024 /*0x0400*/;
        this.InsertBuffer(data, (ushort[]) null, (int[]) null, false);
        this.e.TerOpFlags2 = tc.ResetFlag(this.e.TerOpFlags2, 1024 /*0x0400*/);
        this.e.InputFontId = -1;
      }
      loc = 2;
      --this.e.CurCol;
      this.FixPos();
    }
    ++this.e.CurCol;
    this.FixPos();
    this.ReleaseUndo();
    if (repaint)
      this.PaintTer();
    return loc != 0;
  }

  internal bool TerDeleteField(bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    switch (this.e.TerFont[this.GetCurCfmt(this.e.CurLine, this.e.CurCol)].FieldId)
    {
      case 6:
      case 7:
        if (!this.GetFieldScope(this.e.CurLine, this.e.CurCol, 6))
          return false;
        if (this.e.TerFont[this.GetCurCfmt(this.e.HilightEndRow, this.e.HilightEndCol)].FieldId == 7)
        {
          int pLine;
          int pCol;
          if (!this.GetFieldLoc(this.e.HilightEndRow, this.e.HilightEndCol, false, out pLine, out pCol))
            return false;
          this.e.HilightEndRow = pLine;
          this.e.HilightEndCol = pCol;
        }
        this.e.HilightType = 2;
        this.e.StretchHilight = false;
        int terFlags = this.e.TerFlags;
        this.e.TerFlags |= 256 /*0x0100*/;
        this.e.TerOpFlags |= 32768 /*0x8000*/;
        this.e.TerDeleteBlock(repaint);
        this.e.TerFlags = terFlags;
        this.e.HilightType = 0;
        this.e.TerOpFlags &= -32769;
        this.ReleaseUndo();
        return true;
      default:
        return false;
    }
  }

  internal bool TerFieldToText(bool all, bool repaint)
  {
    ushort num1 = 0;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int num2 = 0;
    int num3 = 0;
    int index1 = this.e.TotalLines - 1;
    int num4 = this.e.text[index1].len - 1;
    if (!all)
    {
      if (this.e.HilightType == 0)
        return false;
      this.NormalizeBlock();
      num2 = this.e.HilightBegRow;
      num3 = this.e.HilightBegCol;
      index1 = this.e.HilightEndRow;
      num4 = this.e.HilightEndCol - 1;
    }
    for (int line = num2; line <= index1; ++line)
    {
      int len = this.e.text[line].len;
      ushort[] numArray = this.OpenCfmt(line);
      ushort OldFmt = ushort.MaxValue;
      int num5 = line == num2 ? num3 : 0;
      int num6 = line == index1 ? num4 : this.e.text[line].len - 1;
      for (int index2 = num5; index2 <= num6; ++index2)
      {
        ushort index3 = numArray[index2];
        int fieldId = this.e.TerFont[(int) index3].FieldId;
        switch (fieldId)
        {
          case 5:
          case 6:
          case 7:
          case 8:
          case 17:
            if (fieldId == 6)
            {
              this.MoveLineData(line, index2, 1, 'D');
              numArray = this.OpenCfmt(line);
              --index2;
              --num6;
              break;
            }
            if ((int) index3 != (int) OldFmt)
            {
              OldFmt = index3;
              if ((this.e.TerFont[(int) OldFmt].style & 128 /*0x80*/) != 0)
                this.e.TerFont[(int) OldFmt].FieldId = 0;
              else
                num1 = this.GetNewFieldId(OldFmt, 0, (string) null, line, index2);
            }
            numArray[index2] = num1;
            break;
        }
      }
      this.CloseCfmt(line);
    }
    if (repaint)
      this.PaintTer();
    return true;
  }

  internal bool TerGetCheckboxInfo(int pict, out bool IsChecked)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    IsChecked = false;
    if (!this.IsFormField(pict, 3))
      return false;
    IsChecked = ((CheckBox) this.e.TerFont[pict].ctl).Checked;
    return true;
  }

  internal int TerGetControlId(int pict)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    return pict < 0 || pict >= this.e.TotalFonts || !this.IsControl(pict) ? -1 : this.e.TerFont[pict].form.id;
  }

  internal int TerGetControlPos(Control ctl)
  {
    int col = 0;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int num;
    if (-1 != (num = this.TerXlateControl(ctl)))
    {
      int index;
      for (index = 0; index < this.e.TotalLines; ++index)
      {
        ushort[] numArray = this.OpenCfmt(index);
        col = 0;
        while (col < this.e.text[index].len && (int) numArray[col] != num)
          ++col;
        this.CloseCfmt(index);
        if (col < this.e.text[index].len)
          break;
      }
      if (index < this.e.TotalLines)
        return this.RowColToAbs(index, col);
    }
    return -1;
  }

  internal int TerGetField(int LineNo, int ColNo, int type, out string text)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    text = "";
    if (type != 6 && type != 7)
      return 0;
    if (LineNo < 0)
    {
      LineNo = this.e.CurLine;
      ColNo = this.e.CurCol;
    }
    int pBegLine;
    int pBegCol;
    int pEndLine;
    int pEndCol;
    if (!this.GetFieldScope(LineNo, ColNo, type, out pBegLine, out pBegCol, out pEndLine, out pEndCol))
      return 0;
    if (type == 6)
    {
      ++pBegCol;
      this.FixPos(ref pBegLine, ref pBegCol);
      --pEndCol;
      this.FixPos(ref pEndLine, ref pEndCol);
      if (pBegLine == pEndLine && pEndCol <= pBegCol)
        return 0;
    }
    for (int index = pBegLine; index <= pEndLine; ++index)
    {
      int startIndex = index == pBegLine ? pBegCol : 0;
      int num = index == pEndLine ? pEndCol : this.e.text[index].len;
      char[] txt = this.e.text[index].txt;
      text += new string(txt, startIndex, num - startIndex);
    }
    return text.Length;
  }

  internal new int TerGetFieldFont(int font, int FieldId, string FieldCode)
  {
    if (font < 0 || font >= this.e.TotalFonts || !this.e.TerFont[font].InUse)
      return -1;
    if (this.e.TerFont[font].FieldId == FieldId && this.IsSameFieldCode(this.e.TerFont[font].FieldCode, FieldCode))
      return font;
    if ((this.e.TerFont[font].style & 128 /*0x80*/) == 0)
      return (int) this.GetNewFieldId((ushort) font, FieldId, FieldCode, 0, 0);
    this.e.TerFont[font].FieldId = FieldId;
    this.e.TerFont[font].FieldCode = FieldCode;
    return font;
  }

  internal bool TerGetInputFieldInfo(int pict, out string name, out int type, out bool border)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    name = "";
    type = 0;
    border = false;
    if (this.IsFormField(pict, 0))
    {
      tc.ClsForm form = this.e.TerFont[pict].form;
      if (form != null)
      {
        name = form.name;
        type = this.e.TerFont[pict].FieldId;
        border = form.border;
        return true;
      }
      if (this.e.TerFont[pict].FieldId == 2)
      {
        type = 2;
        name = this.GetStringField(this.e.TerFont[pict].FieldCode, 1, '|');
        border = (this.e.TerFont[pict].style & 8192 /*0x2000*/) != 0;
        return true;
      }
    }
    else if (this.FindTextInputField(pict))
    {
      int curCfmt = this.GetCurCfmt(this.e.CurLine, this.e.CurCol);
      type = 2;
      name = this.GetStringField(this.e.TerFont[curCfmt].FieldCode, 1, '|');
      border = (this.e.TerFont[curCfmt].style & 8192 /*0x2000*/) != 0;
      return true;
    }
    return false;
  }

  internal int TerGetNextControlPos(int pos)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int row;
    int col1;
    this.AbsToRowCol(pos, out row, out col1);
    for (int index = row; index < this.e.TotalLines; ++index)
    {
      if (this.e.text[index].len != 0)
      {
        int num = index != row ? 0 : col1;
        ushort[] numArray = this.OpenCfmt(index);
        for (int col2 = num; col2 < this.e.text[index].len; ++col2)
        {
          if (this.IsControl((int) numArray[col2]) || this.e.TerFont[(int) numArray[col2]].FieldId == 2)
            return this.RowColToAbs(index, col2);
        }
        this.CloseCfmt(index);
      }
    }
    return -1;
  }

  internal int TerGetPrevControlPos(int pos)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int row;
    int col;
    this.AbsToRowCol(pos, out row, out col);
    for (int index1 = row; index1 >= 0; --index1)
    {
      if (this.e.text[index1].len != 0)
      {
        int num = index1 != row ? this.e.text[index1].len - 1 : col;
        ushort[] numArray = this.OpenCfmt(index1);
        for (int index2 = num; index2 >= 0; --index2)
        {
          if (!this.IsControl((int) numArray[index2]))
          {
            if (this.e.TerFont[(int) numArray[index2]].FieldId == 2)
            {
              this.SelectTextInputField2(index1, index2, false);
              index1 = this.e.HilightBegRow;
              index2 = this.e.HilightBegCol;
              this.e.DeselectTerText(false);
            }
            else
              continue;
          }
          return this.RowColToAbs(index1, index2);
        }
        this.CloseCfmt(index1);
      }
    }
    return -1;
  }

  internal bool TerGetTextFieldInfo(
    int pict,
    out string data,
    out int MaxChars,
    out int reserved,
    out string typeface,
    out int TwipsSize,
    out int style)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    string str;
    typeface = str = "";
    data = str;
    int num1;
    TwipsSize = num1 = 0;
    int num2;
    reserved = num2 = num1;
    MaxChars = num2;
    style = 0;
    if (!this.FindTextInputField(pict))
      return false;
    int curCfmt = this.GetCurCfmt(this.e.CurLine, this.e.CurCol);
    MaxChars = this.ToInt(this.GetStringField(this.e.TerFont[curCfmt].FieldCode, 0, '|'));
    typeface = this.e.TerFont[curCfmt].TypeFace;
    TwipsSize = this.e.TerFont[curCfmt].TwipsSize;
    style = this.e.TerFont[curCfmt].style;
    this.SelectTextInputField(false);
    data = this.e.TerGetTextSel();
    this.e.DeselectTerText(false);
    return true;
  }

  internal int TerInsertCheckBoxField(
    string name,
    int TwipsSize,
    bool IsChecked,
    bool insert,
    bool repaint)
  {
    tc.ClsForm clsForm = new tc.ClsForm();
    int num1 = 0;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.CanInsertTextObject(this.e.CurLine, this.e.CurCol))
      return 0;
    if (this.False(name) || name.Length == 0)
    {
      if (!this.CallDialogBox((Form) new terdlg_checkbox_field(this.e)))
        return 0;
      name = this.e.DlgText1;
      TwipsSize = this.e.DlgInt1;
      IsChecked = this.True(this.e.DlgInt2);
      if (name.Length == 0)
        return 0;
    }
    bool flag = this.e.NextFontId >= 0;
    this.GetCurCfmt(this.e.CurLine, this.e.CurCol);
    int openSlot;
    if ((openSlot = this.FindOpenSlot()) == -1)
      return 0;
    this.e.TerFont[openSlot].form = clsForm;
    clsForm.CtlClass = "CheckBox";
    clsForm.name = name;
    clsForm.style = 0;
    clsForm.CheckBoxSize = TwipsSize;
    clsForm.InitNum = IsChecked ? 1 : 0;
    clsForm.border = true;
    clsForm.id = num1;
    int num2;
    int num3 = num2 = TwipsSize;
    if (this.e.HilightType == 2)
      this.e.TerDeleteBlock(true);
    --this.e.UndoRef;
    this.e.TerFont[openSlot].InUse = true;
    this.e.TerFont[openSlot].PictType = 6;
    this.e.TerFont[openSlot].FieldId = 3;
    this.e.TerFont[openSlot].ObjectType = 0;
    this.e.TerFont[openSlot].PictHeight = num2;
    this.e.TerFont[openSlot].PictWidth = num3;
    this.e.TerFont[openSlot].style = 128 /*0x80*/;
    this.e.TerFont[openSlot].PictAlign = 1;
    this.e.TerFont[openSlot].AuxId = num1;
    if (!this.RealizeControl(openSlot, (Control) new CheckBox()))
    {
      this.InitTerObject(openSlot);
    }
    else
    {
      this.XlateSizeForPrt(openSlot);
      if (insert && !flag)
      {
        if (this.e.CurCol >= this.e.text[this.e.CurLine].len)
          this.e.CurCol = this.e.text[this.e.CurLine].len;
        if (this.e.CurCol < 0)
          this.e.CurCol = 0;
        this.MoveLineData(this.e.CurLine, this.e.CurCol, 1, 'B');
        this.e.text[this.e.CurLine].txt[this.e.CurCol] = '\u0018';
        this.OpenCfmt(this.e.CurLine)[this.e.CurCol] = (ushort) openSlot;
        this.CloseCfmt(this.e.CurLine);
      }
    }
    this.e.PaintFlag = 4;
    if (insert | flag)
      this.PaintTer();
    return openSlot;
  }

  internal int TerInsertControl(Control ctl, string ClassName, int align, int id, bool insert)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    bool flag = this.e.NextFontId >= 0;
    if ((this.e.TerFont[this.GetCurCfmt(this.e.CurLine, this.e.CurCol)].style & 512 /*0x0200*/) != 0 & insert && !flag)
    {
      this.MessageBeep(0);
      return 0;
    }
    int openSlot;
    if ((openSlot = this.FindOpenSlot()) == -1)
      return 0;
    int twipsX = this.OrigScrToTwipsX(ctl.Width);
    int twipsY = this.OrigScrToTwipsY(ctl.Height);
    tc.ClsForm clsForm = new tc.ClsForm();
    clsForm.CtlClass = ClassName;
    clsForm.id = id;
    if (this.e.HilightType == 2)
      this.e.TerDeleteBlock(true);
    --this.e.UndoRef;
    this.e.TerFont[openSlot].InUse = true;
    this.e.TerFont[openSlot].PictType = 2;
    this.e.TerFont[openSlot].ObjectType = 0;
    this.e.TerFont[openSlot].PictHeight = twipsY;
    this.e.TerFont[openSlot].PictWidth = twipsX;
    this.e.TerFont[openSlot].style = 128 /*0x80*/;
    this.e.TerFont[openSlot].PictAlign = align;
    this.e.TerFont[openSlot].AuxId = id;
    this.e.TerFont[openSlot].form = clsForm;
    if (!this.RealizeControl(openSlot, ctl))
    {
      this.InitTerObject(openSlot);
    }
    else
    {
      this.XlateSizeForPrt(openSlot);
      if (insert && !flag)
      {
        if (this.e.CurCol >= this.e.text[this.e.CurLine].len)
          this.e.CurCol = this.e.text[this.e.CurLine].len;
        if (this.e.CurCol < 0)
          this.e.CurCol = 0;
        this.MoveLineData(this.e.CurLine, this.e.CurCol, 1, 'B');
        this.e.text[this.e.CurLine].txt[this.e.CurCol] = '\u0018';
        this.OpenCfmt(this.e.CurLine)[this.e.CurCol] = (ushort) openSlot;
        this.CloseCfmt(this.e.CurLine);
      }
    }
    this.e.PaintFlag = 4;
    if (insert | flag)
      this.PaintTer();
    return openSlot;
  }

  internal bool TerInsertDateTime(string pDateFmt, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.False(pDateFmt) || pDateFmt.Length == 0)
    {
      if (!this.CallDialogBox((Form) new terdlg_date(this.e)))
        return false;
      pDateFmt = this.e.DlgText1;
    }
    return this.InsertDynField(8, pDateFmt);
  }

  internal bool TerInsertField(string name, string data, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.CanInsertTextObject(this.e.CurLine, this.e.CurCol))
      return false;
    if (this.False(name) || name.Length == 0)
    {
      if (!this.CallDialogBox((Form) new terdlg_data_field(this.e)))
        return false;
      name = this.e.DlgText1;
      data = this.e.DlgText2;
      if (name.Length == 0)
        return false;
    }
    int effectiveCfmt = this.GetEffectiveCfmt();
    int curLine1 = this.e.CurLine;
    int curCol1 = this.e.CurCol;
    int terOpFlags2 = this.e.TerOpFlags2;
    this.e.TerOpFlags2 |= 8192 /*0x2000*/;
    this.e.HilightType = 0;
    int num = this.e.InputFontId < 0 ? 0 : ((this.e.TerFont[this.e.InputFontId].style & 512 /*0x0200*/) != 0 ? 1 : 0);
    int OldFmt = (int) this.GetNewFieldId((ushort) this.GetCurCfmt(this.e.CurLine, this.e.CurCol), 6, (string) null, this.e.CurLine, this.e.CurCol);
    if (num == 0)
      OldFmt = (int) this.GetNewStyle((ushort) OldFmt, 512 /*0x0200*/, 0, "", this.e.CurLine, -1);
    int newStyle = (int) this.GetNewStyle((ushort) OldFmt, 512 /*0x0200*/, 1, "", this.e.CurLine, -1);
    this.e.InputFontId = newStyle;
    this.InsertBuffer("{", (ushort[]) null, (int[]) null, false);
    this.e.InputFontId = OldFmt;
    this.InsertBuffer(name, (ushort[]) null, (int[]) null, false);
    this.e.InputFontId = newStyle;
    this.InsertBuffer("}", (ushort[]) null, (int[]) null, false);
    this.e.InputFontId = effectiveCfmt;
    if (this.True(data) && data.Length > 0)
    {
      this.e.HilightType = 0;
      this.e.InputFontId = (int) this.GetNewFieldId((ushort) this.e.InputFontId, 7, (string) null, this.e.CurLine, this.e.CurCol);
      this.InsertBuffer(data, (ushort[]) null, (int[]) null, false);
    }
    this.e.TerOpFlags2 = terOpFlags2;
    int curLine2 = this.e.CurLine;
    int curCol2 = this.e.CurCol;
    this.PrevTextPos(ref curLine2, ref curCol2);
    this.SaveUndo(curLine1, curCol1, curLine2, curCol2, 'I');
    this.e.InputFontId = -1;
    ++this.e.TerArg.modified;
    if (repaint)
      this.PaintTer();
    return true;
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
    bool reserved,
    bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.CanInsertTextObject(this.e.CurLine, this.e.CurCol))
      return 0;
    if (this.False(name) || name.Length == 0)
    {
      if (!this.CallDialogBox((Form) new terdlg_input_field(this.e)))
        return 0;
      name = this.e.DlgText1;
      data = this.e.DlgText2;
      pTypeface = this.e.DlgTypeface;
      MaxLen = this.e.DlgInt1;
      border = this.True(this.e.DlgInt2);
      TwipsSize = this.e.DlgInt3;
      TextStyle = this.e.DlgInt4;
      TextColor = this.e.DlgColor1;
      if (name.Length == 0)
        return 0;
    }
    if (border)
      TextStyle |= 8192 /*0x2000*/;
    int font = this.e.TerCreateFont(-1, false, pTypeface, TwipsSize / 20, TextStyle, TextColor, tc.CLR_WHITE, 2, 0);
    if (font < 0)
      return 0;
    this.e.TerFont[font].FieldCode = $"{MaxLen.ToString()}|{name}";
    int num = 1;
    for (int index = 0; index < this.e.TotalFonts; ++index)
    {
      if (index != font && this.e.TerFont[index].FieldId == 2 && this.e.TerFont[index].AuxId >= num)
        num = this.e.TerFont[index].AuxId + 1;
    }
    this.e.TerFont[font].AuxId = num;
    int undoRef = this.e.UndoRef;
    if (this.e.HilightType == 2)
      this.e.TerDeleteBlock(true);
    this.e.UndoRef = undoRef;
    this.e.TerInsertText(data, font, -1, repaint);
    return font;
  }

  internal bool TerLocateField(int loc, string name, bool exact, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (loc != 0 && loc != 1 && loc != 2 && loc != 4)
      return false;
    int pLine = this.e.CurLine;
    int pCol = this.e.CurCol;
    if (loc == 0)
    {
      pLine = 0;
      pCol = 0;
    }
    if (loc == 1)
    {
      pLine = this.e.TotalLines - 1;
      pCol = this.e.text[pLine].len - 1;
      this.FixPos(ref pLine, ref pCol);
    }
    switch (this.e.TerFont[this.GetCurCfmt(pLine, pCol)].FieldId)
    {
      case 6:
      case 7:
        if (loc != 0 && loc != 1 || !this.FieldFound(pLine, pCol, name, exact))
          goto default;
        break;
      default:
        if (loc == 0)
          loc = 2;
        if (loc == 1)
          loc = 4;
        while ((loc != 2 || pLine != this.e.TotalLines - 1 || pCol != this.e.text[pLine].len) && (loc != 4 || pLine != 0 || pCol != 0))
        {
          int fieldId = this.e.TerFont[this.GetCurCfmt(pLine, pCol)].FieldId;
          if (loc == 2)
          {
            if (fieldId == 6)
            {
              this.GetFieldLoc(pLine, pCol, false, out pLine, out pCol);
              fieldId = this.e.TerFont[this.GetCurCfmt(pLine, pCol)].FieldId;
            }
            if (fieldId == 7)
            {
              this.GetFieldLoc(pLine, pCol, false, out pLine, out pCol);
              fieldId = this.e.TerFont[this.GetCurCfmt(pLine, pCol)].FieldId;
            }
          }
          else
          {
            if (fieldId == 7)
            {
              this.GetFieldLoc(pLine, pCol, true, out pLine, out pCol);
              --pCol;
              this.FixPos(ref pLine, ref pCol);
              fieldId = this.e.TerFont[this.GetCurCfmt(pLine, pCol)].FieldId;
            }
            if (fieldId == 6)
            {
              this.GetFieldLoc(pLine, pCol, true, out pLine, out pCol);
              --pCol;
              this.FixPos(ref pLine, ref pCol);
              fieldId = this.e.TerFont[this.GetCurCfmt(pLine, pCol)].FieldId;
            }
          }
          while (fieldId != 6 && fieldId != 7)
          {
            if (loc == 2)
            {
              if (pLine == this.e.TotalLines - 1 && pCol == this.e.text[pLine].len)
                return false;
              this.GetFieldLoc(pLine, pCol, false, out pLine, out pCol);
              fieldId = this.e.TerFont[this.GetCurCfmt(pLine, pCol)].FieldId;
            }
            else
            {
              if (pLine == 0 && pCol == 0)
                return false;
              this.GetFieldLoc(pLine, pCol, true, out pLine, out pCol);
              --pCol;
              this.FixPos(ref pLine, ref pCol);
              fieldId = this.e.TerFont[this.GetCurCfmt(pLine, pCol)].FieldId;
            }
          }
          if (this.FieldFound(pLine, pCol, name, exact))
            goto label_34;
        }
        return false;
    }
label_34:
    int fieldId1 = this.e.TerFont[this.GetCurCfmt(pLine, pCol)].FieldId;
    if (fieldId1 == 7)
    {
      this.GetFieldLoc(pLine, pCol, true, out pLine, out pCol);
      --pCol;
      this.FixPos(ref pLine, ref pCol);
      fieldId1 = this.e.TerFont[this.GetCurCfmt(pLine, pCol)].FieldId;
    }
    if (fieldId1 == 6)
      this.GetFieldLoc(pLine, pCol, true, out pLine, out pCol);
    this.e.CurLine = pLine;
    this.e.CurCol = pCol;
    this.e.CursDirection = 1;
    if (repaint)
      this.PaintTer();
    return true;
  }

  internal bool TerLocateFieldChar(
    int FieldId,
    string FieldCode,
    bool present,
    ref int StartLine,
    ref int StartCol,
    bool forward)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (FieldId == 0)
      FieldCode = (string) null;
    if (forward)
    {
      for (int line = StartLine; line < this.e.TotalLines; ++line)
      {
        int num = line != StartLine ? 0 : StartCol;
        if (num < this.e.text[line].len && this.e.text[line].len != 0)
        {
          if (this.e.text[line].fmt == null)
          {
            int uniFmt = (int) this.e.text[line].UniFmt;
            int fieldId = this.e.TerFont[uniFmt].FieldId;
            string code2 = this.e.TerFont[uniFmt].FieldCode;
            if (this.False(FieldCode))
              code2 = (string) null;
            if (present && fieldId == FieldId && this.IsSameFieldCode(FieldCode, code2) || !present && (fieldId != FieldId || !this.IsSameFieldCode(FieldCode, code2)))
            {
              StartLine = line;
              StartCol = num;
              return true;
            }
          }
          else
          {
            ushort[] numArray = this.OpenCfmt(line);
            for (int index1 = num; index1 < this.e.text[line].len; ++index1)
            {
              int index2 = (int) numArray[index1];
              int fieldId = this.e.TerFont[index2].FieldId;
              string code2 = this.e.TerFont[index2].FieldCode;
              if (this.False(FieldCode))
                code2 = (string) null;
              if (present && fieldId == FieldId && this.IsSameFieldCode(FieldCode, code2) || !present && (fieldId != FieldId || !this.IsSameFieldCode(FieldCode, code2)))
              {
                StartLine = line;
                StartCol = index1;
                this.CloseCfmt(line);
                return true;
              }
            }
            this.CloseCfmt(line);
          }
        }
      }
    }
    else
    {
      for (int line = StartLine; line >= 0; --line)
      {
        int num = line != StartLine ? this.e.text[line].len - 1 : StartCol;
        if (num >= 0 && this.e.text[line].len != 0)
        {
          if (this.e.text[line].fmt == null)
          {
            int uniFmt = (int) this.e.text[line].UniFmt;
            int fieldId = this.e.TerFont[uniFmt].FieldId;
            string code2 = this.e.TerFont[uniFmt].FieldCode;
            if (this.False(FieldCode))
              code2 = (string) null;
            if (present && fieldId == FieldId && this.IsSameFieldCode(FieldCode, code2) || !present && (fieldId != FieldId || !this.IsSameFieldCode(FieldCode, code2)))
            {
              StartLine = line;
              StartCol = num;
              return true;
            }
          }
          else
          {
            ushort[] numArray = this.OpenCfmt(line);
            for (int index3 = num; index3 >= 0; --index3)
            {
              int index4 = (int) numArray[index3];
              int fieldId = this.e.TerFont[index4].FieldId;
              string code2 = this.e.TerFont[index4].FieldCode;
              if (this.False(FieldCode))
                code2 = (string) null;
              if (present && fieldId == FieldId && this.IsSameFieldCode(FieldCode, code2) || !present && (fieldId != FieldId || !this.IsSameFieldCode(FieldCode, code2)))
              {
                StartLine = line;
                StartCol = index3;
                this.CloseCfmt(line);
                return true;
              }
            }
            this.CloseCfmt(line);
          }
        }
      }
    }
    return false;
  }

  internal int TerLocateInputField(int loc, bool repaint)
  {
    int num = 0;
    bool flag1 = false;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int curCfmt = this.GetCurCfmt(this.e.CurLine, this.e.CurCol);
    if (loc == 5)
      return this.e.TerFont[curCfmt].FieldId == 2 ? this.e.TerFont[curCfmt].AuxId : this.e.CurInputField;
    int pLineNo = this.e.CurLine;
    int pCol = this.e.CurCol;
    if (loc == 0)
    {
      pLineNo = 0;
      pCol = -1;
      loc = 2;
    }
    else if (loc == 1)
    {
      pLineNo = this.e.TotalLines - 1;
      pCol = this.e.text[pLineNo].len - 1;
      loc = 4;
    }
    else if (loc == 2 && this.e.TerFont[curCfmt].FieldId == 2)
      flag1 = true;
    else if (loc == 4 && this.e.TerFont[curCfmt].FieldId == 2)
      flag1 = true;
    if (flag1)
      num = this.e.TerFont[curCfmt].AuxId;
    while ((loc != 2 || this.NextTextPos(ref pLineNo, ref pCol)) && (loc != 4 || this.PrevTextPos(ref pLineNo, ref pCol)))
    {
      int pict = this.GetCurCfmt(pLineNo, pCol);
      if (!flag1 || this.e.TerFont[pict].FieldId != 2 || this.e.TerFont[pict].AuxId != num)
      {
        flag1 = false;
        bool flag2;
        if (this.e.TerFont[pict].FieldId == 2)
        {
          pict = this.e.TerFont[pict].AuxId;
          flag2 = true;
        }
        else if (this.IsFormField(pict, 0))
          flag2 = true;
        else
          continue;
        this.e.SetTerCursorPos(pLineNo, pCol, repaint);
        return !flag2 ? 0 : pict;
      }
    }
    return 0;
  }

  internal bool TerMergeFields(string names, string data, bool repaint)
  {
    int num1 = 100;
    bool flag1 = false;
    bool flag2 = false;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (names != null && data != null)
    {
      int num2 = 0;
      if (names.Length == 0)
        return true;
      tc.StrMergeData[] OldObj = new tc.StrMergeData[num1 + 1];
      int index1 = 0;
      int num3;
      int num4;
      for (num3 = -1; (num4 = names.IndexOf(tc.MergeDelim, num3 + 1)) >= 0; num3 = num4)
      {
        OldObj[index1].pName = names.Substring(num3 + 1, num4 - num3 - 1);
        ++index1;
        if (index1 >= num1)
        {
          num1 += 100;
          OldObj = this.ReAlloc(OldObj, num1 + 1);
        }
      }
      if (num3 < names.Length && index1 < num1)
      {
        OldObj[index1].pName = names.Substring(num3 + 1);
        ++index1;
      }
      int index2 = 0;
      int num5;
      int num6;
      for (num5 = -1; (num6 = data.IndexOf(tc.MergeDelim, num5 + 1)) >= 0; num5 = num6)
      {
        OldObj[index2].pData = data.Substring(num5 + 1, num6 - num5 - 1);
        ++index2;
        if (index2 >= num1)
          break;
      }
      if (num5 < data.Length && index2 < num1)
      {
        OldObj[index2].pData = data.Substring(num5 + 1);
        ++index2;
      }
      if (index1 != index2 || index2 == num1)
      {
        this.PrintError(12, "");
        return flag2;
      }
      int StartLine = 0;
      int num7;
      int StringLen;
      int idx;
      for (int StartCol = 0; this.e.TerLocateStyle(256 /*0x0100*/, ref StartLine, ref StartCol, out StringLen); StartCol = idx + (num2 + num7))
      {
        int num8;
        num7 = num8 = 0;
        char[] txt1 = this.e.text[StartLine].txt;
        int num9 = 0;
        while (num9 < StringLen && ((int) txt1[num9 + StartCol] == (int) this.e.ParaChar || (int) txt1[num9 + StartCol] == (int) this.e.CellChar || this.lstrchr(this.e.BreakChars, txt1[num9 + StartCol])))
          ++num9;
        idx = StartCol + num9;
        StringLen -= num9;
        if (StringLen != 0)
        {
          char[] chArray = this.CopyArray(txt1, idx, StringLen);
          if (idx + StringLen >= this.e.text[StartLine].len && (this.e.text[StartLine].flags & 3) == 0)
          {
            int index3 = StartLine + 1;
            for (bool flag3 = false; index3 < this.e.TotalLines && !flag3; ++index3)
            {
              if (this.e.text[index3].len > 0)
              {
                char[] txt2 = this.e.text[index3].txt;
                ushort[] numArray = this.OpenCfmt(index3);
                int index4;
                for (index4 = 0; index4 < this.e.text[index3].len && (this.e.TerFont[(int) numArray[index4]].style & 256 /*0x0100*/) != 0; ++index4)
                {
                  if ((int) txt2[index4] == (int) this.e.ParaChar || (int) txt2[index4] == (int) this.e.CellChar || this.lstrchr(this.e.BreakChars, txt2[index4]))
                    flag3 = true;
                  if (flag3)
                    break;
                }
                this.CloseCfmt(index3);
                int num10 = index4;
                int len = this.e.text[StartLine].len;
                if (num10 != 0 && num10 + len < 1000)
                {
                  this.LineAlloc(StartLine, len, len + num10);
                  this.MoveCharInfo(index3, 0, StartLine, len, num10);
                  chArray = this.AppendArray(this.e.text[index3].txt, 0, num10, chArray);
                  StringLen = chArray.Length;
                  this.MoveLineData(index3, 0, num10, 'D');
                  if (this.e.text[index3].len > 0)
                    break;
                }
                else
                  break;
              }
            }
          }
          int num11 = StringLen;
          int index5 = StringLen - 1;
          while (index5 >= 0 && ((int) chArray[index5] == (int) this.e.ParaChar || (int) chArray[index5] == (int) this.e.CellChar || this.lstrchr(this.e.BreakChars, chArray[index5])))
            --index5;
          StringLen = index5 + 1;
          int index6 = 0;
          while (index6 < StringLen && chArray[index6] == ' ')
            ++index6;
          int num12 = index6;
          idx += num12;
          if (num12 != StringLen)
          {
            for (int index7 = num12; index7 < StringLen; ++index7)
              chArray[index7 - num12] = chArray[index7];
            StringLen -= num12;
            int index8 = StringLen - 1;
            while (index8 >= 0 && chArray[index8] == ' ')
              --index8;
            num7 = StringLen - index8 - 1;
            StringLen -= num7;
            if (StringLen != num11)
              chArray = this.ReAlloc(chArray, StringLen);
            int length = StringLen;
            int index9;
            for (index9 = 0; index9 < index1; ++index9)
            {
              string strB = new string(chArray, 0, StringLen);
              if (string.Compare(OldObj[index9].pName, strB, true) == 0)
                break;
            }
            char[] charArray;
            if (index9 == index1)
            {
              num2 = length;
              string data1;
              if (this.e.SendMergeMessage(new string(chArray, 0, length), out data1))
              {
                charArray = data1.ToCharArray();
                num2 = charArray.Length;
              }
              else
                continue;
            }
            else
            {
              charArray = OldObj[index9].pData.ToCharArray();
              num2 = charArray.Length;
            }
            if (num2 > 100)
            {
              if (this.e.TerArg.WordWrap)
              {
                flag1 = true;
              }
              else
              {
                this.PrintError(89, "");
                return flag2;
              }
            }
            if (num2 > length)
              this.MoveLineData(StartLine, idx + length - 1, num2 - length, 'A');
            if (num2 < length)
              this.MoveLineData(StartLine, idx + num2, length - num2, 'D');
            char[] txt3 = this.e.text[StartLine].txt;
            for (int index10 = 0; index10 < num2; ++index10)
              txt3[idx + index10] = charArray[index10];
            ushort[] fmt;
            ushort[] cmi;
            this.OpenCharInfo(StartLine, out fmt, out cmi);
            for (int index11 = length; index11 < num2; ++index11)
            {
              fmt[idx + index11] = fmt[idx + length - 1];
              cmi[idx + index11] = (ushort) 0;
            }
            this.CloseCharInfo(StartLine);
            this.e.HilightType = 2;
            this.e.HilightBegRow = this.e.HilightEndRow = StartLine;
            this.e.HilightBegCol = idx - num12;
            this.e.HilightEndCol = idx + num2 + num7;
            this.e.SetTerCharStyle(256 /*0x0100*/, false, false);
            ++this.e.TerArg.modified;
          }
        }
      }
      if (flag1)
        this.e.TerRewrap();
      if (repaint)
        this.PaintTer();
    }
    return true;
  }

  internal bool TerSetCheckboxInfo(int pict, bool IsChecked)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.IsFormField(pict, 3))
      return false;
    ref tc.StrFont local = ref this.e.TerFont[pict];
    bool flag = ((CheckBox) this.e.TerFont[pict].ctl).Checked;
    if (flag && !IsChecked || !flag & IsChecked)
      ((CheckBox) this.e.TerFont[pict].ctl).Checked = IsChecked;
    ++this.e.TerArg.modified;
    return true;
  }

  internal bool TerSetInputFieldInfo(int pict, string name, bool border)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.IsFormField(pict, 0))
    {
      tc.ClsForm form = this.e.TerFont[pict].form;
      if (name != null)
        form.name = name;
      if (this.e.TerFont[pict].FieldId == 2)
      {
        form.border = border;
        if (form.CtlClass == "TextBox")
          ((TextBoxBase) this.e.TerFont[pict].ctl).BorderStyle = form.border ? BorderStyle.Fixed3D : BorderStyle.None;
      }
      ++this.e.TerArg.modified;
      return true;
    }
    if (!this.FindTextInputField(pict))
      return false;
    int curLine = this.e.CurLine;
    int curCol = this.e.CurCol;
    int index = this.GetCurCfmt(this.e.CurLine, this.e.CurCol);
    if (this.e.TerFont[index].FieldId != 2 || this.e.TerFont[index].FieldCode == null || this.e.TerFont[index].AuxId != pict)
      return false;
    string stringField1 = this.GetStringField(this.e.TerFont[index].FieldCode, 0, '|');
    string stringField2 = this.GetStringField(this.e.TerFont[index].FieldCode, 1, '|');
    string str1 = name;
    string str2 = $"{stringField1}|{str1}";
    string stringField3;
    do
    {
      int curCfmt;
      do
      {
        this.e.TerFont[index].FieldCode = str2;
        if (this.e.CurLine != this.e.TotalLines || this.e.CurCol != this.e.text[this.e.CurLine].len - 1)
        {
          this.NextTextPos();
          curCfmt = this.GetCurCfmt(this.e.CurLine, this.e.CurCol);
        }
        else
          goto label_17;
      }
      while (curCfmt == index);
      stringField3 = this.GetStringField(this.e.TerFont[index].FieldCode, 1, '|');
      index = curCfmt;
    }
    while (this.e.TerFont[index].FieldId == 2 && this.e.TerFont[index].AuxId == pict && stringField3 == stringField2);
label_17:
    this.e.CurLine = curLine;
    this.e.CurCol = curCol;
    this.SelectTextInputField(false);
    this.e.SetTerCharStyle(8192 /*0x2000*/, border, true);
    ++this.e.TerArg.modified;
    return true;
  }

  internal static bool TerSetMergeDelim(string delim)
  {
    tc.MergeDelim = delim[0];
    return true;
  }

  internal bool TerSetTextFieldInfo(
    int pict,
    string data,
    int MaxChars,
    int reserved,
    string typeface,
    int TwipsSize,
    int style)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.FindTextInputField(pict))
      return false;
    int curLine = this.e.CurLine;
    int curCol = this.e.CurCol;
    int index = this.GetCurCfmt(this.e.CurLine, this.e.CurCol);
    bool flag = (this.e.TerFont[index].style & 8192 /*0x2000*/) != 0;
    if (this.ToInt(this.GetStringField(this.e.TerFont[index].FieldCode, 0, '|')) != MaxChars)
    {
      string stringField1 = this.GetStringField(this.e.TerFont[index].FieldCode, 1, '|');
      string str = $"{MaxChars.ToString()}|{stringField1}";
      string stringField2;
      do
      {
        int curCfmt;
        do
        {
          this.e.TerFont[index].FieldCode = str;
          if (this.e.CurLine != this.e.TotalLines - 1 || this.e.CurCol != this.e.text[this.e.CurLine].len - 1)
          {
            this.NextTextPos();
            curCfmt = this.GetCurCfmt(this.e.CurLine, this.e.CurCol);
          }
          else
            goto label_9;
        }
        while (curCfmt == index);
        stringField2 = this.GetStringField(this.e.TerFont[index].FieldCode, 1, '|');
        index = curCfmt;
      }
      while (this.e.TerFont[index].FieldId == 2 && this.e.TerFont[index].AuxId == pict && stringField2 == stringField1);
    }
label_9:
    this.e.CurLine = curLine;
    this.e.CurCol = curCol;
    this.SelectTextInputField(false);
    int abs1 = this.RowColToAbs(this.e.HilightBegRow, this.e.HilightBegCol);
    int abs2 = this.RowColToAbs(this.e.HilightEndRow, this.e.HilightEndCol);
    this.e.SetTerFont(typeface, false);
    this.e.SetTerPointSize(-TwipsSize, false);
    this.e.SetTerCharStyle(8511, false, false);
    if (flag)
      style |= 8192 /*0x2000*/;
    this.e.SetTerCharStyle(style, true, false);
    if (data != null)
      this.ReplaceTextString(data, abs1, abs2 - 1);
    this.ReleaseUndo();
    ++this.e.TerArg.modified;
    this.e.DeselectTerText(true);
    return true;
  }

  internal Control TerXlateControl(int pict)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    return !this.IsControl(pict) ? (Control) null : this.e.TerFont[pict].ctl;
  }

  internal int TerXlateControl(Control ctl)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    for (int pict = 0; pict < this.e.TotalFonts; ++pict)
    {
      if (this.IsControl(pict) && this.e.TerFont[pict].ctl == ctl)
        return pict;
    }
    return -1;
  }

  internal int TerXlateControlId(int id)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    for (int pict = 0; pict < this.e.TotalFonts; ++pict)
    {
      if (this.IsControl(pict) && this.e.TerFont[pict].form.id == id)
        return pict;
    }
    return -1;
  }

  internal new bool ToggleFieldNames()
  {
    this.e.ShowFieldNames = !this.e.ShowFieldNames;
    this.RecreateFonts(this.e.TerGr);
    this.e.PageModifyCount = -1;
    this.e.RepageBeginLine = 0;
    this.PaintTer();
    return true;
  }

  internal new bool UpdateDynField(int line, int PageNo)
  {
    int index1 = 0;
    int StartPos1 = 0;
    int num1 = 0;
    string str1 = "";
    int num2 = line;
    int dispNbr1 = this.e.PageInfo[PageNo].DispNbr;
    int pageNumFmt = this.e.TerSect[this.e.PageInfo[PageNo].TopSect].PageNumFmt;
    string str2;
    switch (pageNumFmt)
    {
      case 1:
        str2 = this.AlphaFormat(dispNbr1, true);
        break;
      case 2:
        str2 = this.AlphaFormat(dispNbr1, false);
        break;
      case 3:
        str2 = this.romanize(dispNbr1, true);
        break;
      case 4:
        str2 = this.romanize(dispNbr1, false);
        break;
      default:
        str2 = dispNbr1.ToString();
        break;
    }
    int length1 = str2.Length;
    int totalPages = this.e.TotalPages;
    string str3;
    switch (pageNumFmt)
    {
      case 1:
        str3 = this.AlphaFormat(totalPages, true);
        break;
      case 2:
        str3 = this.AlphaFormat(totalPages, false);
        break;
      case 3:
        str3 = this.romanize(totalPages, true);
        break;
      case 4:
        str3 = this.romanize(totalPages, false);
        break;
      default:
        str3 = totalPages.ToString();
        break;
    }
    int length2 = str3.Length;
    if (this.e.TotalSects > 1)
    {
      int topSect = this.e.PageInfo[PageNo].TopSect;
      int num3 = 1;
      for (int index2 = PageNo - 1; index2 >= 0 && this.e.PageInfo[index2].TopSect == topSect; --index2)
        ++num3;
      for (int index3 = PageNo + 1; index3 < this.e.TotalPages && this.e.PageInfo[index3].TopSect == topSect; ++index3)
        ++num3;
      switch (pageNumFmt)
      {
        case 1:
          str1 = this.AlphaFormat(num3, true);
          break;
        case 2:
          str1 = this.AlphaFormat(num3, false);
          break;
        case 3:
          str1 = this.romanize(num3, true);
          break;
        case 4:
          str1 = this.romanize(num3, false);
          break;
        default:
          str1 = num3.ToString();
          break;
      }
      num1 = str1.Length;
    }
    this.e.TempString = this.e.PrevTotalPages.ToString();
    int length3 = this.e.TempString.Length;
    if (length1 < length3)
    {
      str2 = str2.PadLeft(length3);
      length1 = str2.Length;
    }
    if (length2 < length3)
    {
      str3 = str3.PadLeft(length3);
      length2 = str3.Length;
    }
    if (num1 < length3 && this.e.TotalSects > 1)
    {
      str1 = str1.PadLeft(length3);
      num1 = str1.Length;
    }
    while (true)
    {
      int pCol1;
      int len1;
      int num4;
      string str4;
      do
      {
        int len2 = this.e.text[line].len;
        if (len2 == 0)
          return true;
        if (StartPos1 >= len2)
        {
          if (line + 1 < this.e.TotalLines)
          {
            int curCfmt = this.GetCurCfmt(line + 1, 0);
            if (this.e.TerFont[curCfmt].FieldId != 1 && this.e.TerFont[curCfmt].FieldId != 5 && this.e.TerFont[curCfmt].FieldId != 17)
              return true;
            ushort[] numArray = this.OpenCfmt(line + 1);
            int count = 0;
            for (int index4 = 0; index4 < this.e.text[line + 1].len && (this.e.TerFont[(int) numArray[index4]].FieldId == 1 || this.e.TerFont[(int) numArray[index4]].FieldId == 5 || this.e.TerFont[(int) numArray[index4]].FieldId == 17); ++index4)
              ++count;
            this.CloseCfmt(line + 1);
            int modified = this.e.TerArg.modified;
            if (count > 0)
              this.MoveLineData(line + 1, 0, count, 'D');
            this.e.TerArg.modified = modified;
          }
          return true;
        }
        ushort[] numArray1 = this.OpenCfmt(line);
        int index5;
        for (index5 = StartPos1; index5 < len2; ++index5)
        {
          int fieldId = this.e.TerFont[(int) numArray1[index5]].FieldId;
          if (this.IsDynField(fieldId) && (fieldId != 8 || (this.e.TerFlags4 & 131072 /*0x020000*/) == 0) && (fieldId != 10 || this.e.InPrinting && !this.e.InPrintPreview))
            break;
        }
        if (index5 == len2)
        {
          this.CloseCfmt(line);
          return true;
        }
        int FieldFont = (int) numArray1[index5];
        int fieldId1 = this.e.TerFont[(int) numArray1[index5]].FieldId;
        string fieldCode = this.e.TerFont[(int) numArray1[index5]].FieldCode;
        pCol1 = index5;
        len1 = 1;
        int index6;
        for (index6 = pCol1 + 1; index6 < len2 && this.e.TerFont[(int) numArray1[index6]].FieldId == fieldId1; ++index6)
          ++len1;
        this.CloseCfmt(line);
        switch (fieldId1)
        {
          case 1:
            num4 = length1;
            break;
          case 8:
          case 10:
          case 11:
          case 12:
          case 16 /*0x10*/:
            string DateString = "";
            if (index6 == len2)
            {
              int pLineNo = line + 1;
              int pCol2 = 0;
              while (this.e.TerFont[this.GetCurCfmt(pLineNo, pCol2)].FieldId == fieldId1)
              {
                ++len1;
                if (!this.NextTextPos(ref pLineNo, ref pCol2))
                  break;
              }
            }
            if (this.e.TerFont[this.GetPrevCfmt(line, pCol1)].FieldId == fieldId1)
            {
              StartPos1 = pCol1 + len1;
              if (StartPos1 >= len2)
                return true;
              continue;
            }
            switch (fieldId1)
            {
              case 8:
              case 10:
                this.GetDateString(fieldCode, out DateString, FieldFont);
                break;
              case 11:
                DateString = "List";
                if (this.True(this.e.text[line].tabw) && index1 < this.e.text[line].tabw.ListnumCount)
                {
                  DateString = this.e.text[line].tabw.pListnum[index1].text;
                  ++index1;
                  break;
                }
                break;
              case 12:
                DateString = "1";
                if (this.True(this.e.text[line].tabw) && this.True(this.e.text[line].tabw.pAutoNumLgl))
                {
                  DateString = this.e.text[line].tabw.pAutoNumLgl;
                  if (this.False(fieldCode) || fieldCode.Length == 0 || fieldCode.IndexOf("\\e") < 0)
                  {
                    DateString += ".";
                    break;
                  }
                  break;
                }
                break;
              case 16 /*0x10*/:
                if (this.e.TerArg.PageMode)
                {
                  bool flag = fieldCode.IndexOf("\\* alphabetic") >= 0;
                  int length4 = fieldCode.Length;
                  int index7 = 0;
                  int num5 = 0;
                  while (num5 < length4 && fieldCode[num5] != ' ')
                    ++num5;
                  string str5 = fieldCode.Substring(0, num5);
                  int index8 = 1;
                  while (index8 < this.e.TotalCharTags && (!this.e.CharTag[index8].InUse || this.e.CharTag[index8].type != 1 || !(this.e.CharTag[index8].name == str5)))
                    ++index8;
                  if (index8 < this.e.TotalCharTags)
                  {
                    int line1 = this.e.CharTag[index8].line;
                    if (line1 >= 0 && line1 < this.e.TotalLines)
                      index7 = this.e.text[line1].page;
                  }
                  if (index7 < 0)
                    index7 = 0;
                  if (index7 >= this.e.TotalPages)
                    index7 = this.e.TotalPages - 1;
                  int dispNbr2 = this.e.PageInfo[index7].DispNbr;
                  DateString = !flag ? dispNbr2.ToString() : this.AlphaFormat(dispNbr2, false);
                  break;
                }
                break;
            }
            if (DateString.Length == 0)
            {
              StartPos1 = pCol1 + len1;
            }
            else
            {
              this.ReplaceTextInPlace(ref line, ref pCol1, len1, DateString);
              StartPos1 = pCol1;
            }
            if (line > num2)
              return true;
            continue;
          case 17:
            if (this.e.TotalSects != 1)
            {
              num4 = num1;
              break;
            }
            goto default;
          default:
            num4 = length2;
            break;
        }
        switch (fieldId1)
        {
          case 1:
            str4 = str2;
            break;
          case 17:
            if (this.e.TotalSects != 1)
            {
              str4 = str1;
              break;
            }
            goto default;
          default:
            str4 = str3;
            break;
        }
        if (num4 <= len1)
        {
          char[] txt = this.e.text[line].txt;
          int index9;
          for (index9 = pCol1; index9 < pCol1 + num4; ++index9)
            txt[index9] = str4[index9 - pCol1];
          StartPos1 = index9;
        }
        else
          goto label_104;
      }
      while (num4 >= len1);
      int modified1 = this.e.TerArg.modified;
      this.MoveLineData(line, StartPos1, len1 - num4, 'D');
      this.e.TerArg.modified = modified1;
      continue;
label_104:
      char[] txt1 = this.e.text[line].txt;
      int index10;
      for (index10 = pCol1; index10 < pCol1 + len1; ++index10)
        txt1[index10] = str4[index10 - pCol1];
      int StartPos2 = index10;
      int modified2 = this.e.TerArg.modified;
      this.MoveLineData(line, StartPos2, num4 - len1, 'B');
      this.e.TerArg.modified = modified2;
      char[] txt2 = this.e.text[line].txt;
      ushort[] numArray2 = this.OpenCfmt(line);
      int index11;
      for (index11 = StartPos2; index11 < pCol1 + num4; ++index11)
      {
        txt2[index11] = str4[index11 - pCol1];
        numArray2[index11] = numArray2[index11 - 1];
      }
      this.CloseCfmt(line);
      StartPos1 = index11;
    }
  }
}
