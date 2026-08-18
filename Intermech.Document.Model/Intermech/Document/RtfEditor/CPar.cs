// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.CPar
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class CPar : COp
{
  internal CPar(ImRtfEditor ImRtfEditor)
    : base(ImRtfEditor)
  {
  }

  internal bool ApplyCharStyles(int CurStyle, bool force = true)
  {
    for (int NewFont = 0; NewFont < this.e.TotalFonts; ++NewFont)
    {
      if (this.e.TerFont[NewFont].InUse && (this.e.TerFont[NewFont].style & 128 /*0x80*/) == 0 && this.e.TerFont[NewFont].CharStyId == CurStyle)
      {
        tc.StrFont font = this.e.TerFont[NewFont];
        int paraStyId = font.ParaStyId;
        if (this.strcmpi(font.TypeFace, this.e.StyleId[paraStyId].TypeFace) == 0)
          font.TypeFace = this.e.TerArg.FontTypeFace;
        if (font.TwipsSize == this.e.StyleId[paraStyId].TwipsSize)
          font.TwipsSize = this.e.TerArg.PointSize * 20;
        if (force)
          font.style = tc.ResetUintFlag(ref font.style, this.e.StyleId[paraStyId].style);
        if (font.TextColor == this.e.StyleId[paraStyId].TextColor)
          font.TextColor = this.ToColor(0);
        if (font.TextBkColor == this.e.StyleId[paraStyId].TextBkColor)
          font.TextBkColor = this.NewColor(tc.CLR_WHITE);
        if (font.UlineColor == this.e.StyleId[paraStyId].UlineColor)
          font.UlineColor = this.ToColor(0);
        if (font.expand == this.e.StyleId[paraStyId].expand)
          font.expand = 0;
        if (font.offset == this.e.StyleId[paraStyId].offset)
          font.offset = 0;
        this.SetCharStyleId(ref font, this.e.PrevStyleId, this.e.StyleId[CurStyle], force);
        this.SetCharStyleId(ref font, this.e.StyleId[paraStyId], this.e.StyleId[paraStyId], false);
        if (!this.fnt.FontsIsEqual(this.e.TerFont[NewFont], font))
        {
          this.e.TerFont[NewFont] = font;
          this.fnt.CreateOneFont(this.e.TerGr, NewFont, true);
        }
      }
    }
    return true;
  }

  internal new bool ApplyLineTextStyle(int LineNo, int CurParaStyId, int PrevParaStyId)
  {
    int OldFont = 0;
    int OldFmt = 0;
    int len = this.e.text[LineNo].len;
    if (len != 0)
    {
      ushort[] numArray = this.OpenCfmt(LineNo);
      for (int index = 0; index < len; ++index)
      {
        int num = OldFont;
        OldFont = (int) numArray[index];
        if ((this.e.TerFont[OldFont].style & 128 /*0x80*/) == 0)
        {
          if (OldFont == num && index > 0)
          {
            numArray[index] = (ushort) OldFmt;
          }
          else
          {
            tc.StrFont strFont = this.e.TerFont[OldFont];
            if (this.strcmpi(strFont.TypeFace, this.e.StyleId[PrevParaStyId].TypeFace) == 0)
              strFont.TypeFace = this.e.TerArg.FontTypeFace;
            if (strFont.TwipsSize == this.e.StyleId[PrevParaStyId].TwipsSize)
              strFont.TwipsSize = this.e.TerArg.PointSize * 20;
            tc.ResetUintFlag(ref strFont.style, this.e.StyleId[PrevParaStyId].style);
            if (strFont.TextColor == this.e.StyleId[PrevParaStyId].TextColor)
              strFont.TextColor = this.ToColor(0);
            if (strFont.TextBkColor == this.e.StyleId[PrevParaStyId].TextBkColor)
              strFont.TextBkColor = tc.CLR_WHITE;
            if (strFont.UlineColor == this.e.StyleId[PrevParaStyId].UlineColor)
              strFont.UlineColor = this.ToColor(0);
            if (strFont.expand == this.e.StyleId[PrevParaStyId].expand)
              strFont.expand = 0;
            if (strFont.offset == this.e.StyleId[PrevParaStyId].offset)
              strFont.offset = 0;
            if (this.e.StyleId[CurParaStyId].TypeFace.Length > 0 && strFont.CharSet != (byte) 2)
              strFont.TypeFace = this.e.StyleId[CurParaStyId].TypeFace;
            if (this.e.StyleId[CurParaStyId].TwipsSize != this.e.TerArg.PointSize * 20 && this.e.StyleId[CurParaStyId].TwipsSize > 0)
              strFont.TwipsSize = this.e.StyleId[CurParaStyId].TwipsSize;
            strFont.style |= this.e.StyleId[CurParaStyId].style;
            if (this.e.StyleId[CurParaStyId].TextColor != tc.CLR_AUTO)
              strFont.TextColor = this.e.StyleId[CurParaStyId].TextColor;
            if (this.e.StyleId[CurParaStyId].TextBkColor != tc.CLR_WHITE)
              strFont.TextBkColor = this.e.StyleId[CurParaStyId].TextBkColor;
            if (this.e.StyleId[CurParaStyId].UlineColor != tc.CLR_AUTO)
              strFont.UlineColor = this.e.StyleId[CurParaStyId].UlineColor;
            if (this.True(this.e.StyleId[CurParaStyId].expand))
              strFont.expand = this.e.StyleId[CurParaStyId].expand;
            if (this.True(this.e.StyleId[CurParaStyId].offset))
              strFont.offset = this.e.StyleId[CurParaStyId].offset;
            OldFmt = this.GetNewFont(this.e.TerGr, OldFont, strFont.TypeFace, strFont.TwipsSize, strFont.style, strFont.TextColor, strFont.TextBkColor, strFont.UlineColor, this.e.TerFont[OldFont].FieldId, this.e.TerFont[OldFont].AuxId, this.e.TerFont[OldFont].Aux1Id, this.e.TerFont[OldFont].CharStyId, CurParaStyId, strFont.expand, this.e.TerFont[OldFont].TempStyle, this.e.TerFont[OldFont].lang, this.e.TerFont[OldFont].FieldCode, strFont.offset, this.e.TerFont[OldFont].CharSet, this.e.TerFont[OldFont].flags, this.e.TerFont[OldFont].TextAngle);
            if (OldFmt < 0)
              OldFmt = OldFont;
            if (this.e.TerFont[OldFmt].CharStyId != 1)
              OldFmt = this.ForceCharStyle(OldFmt);
            numArray[index] = (ushort) OldFmt;
          }
        }
      }
    }
    return true;
  }

  internal new ushort ApplyParaStyleOnFont(int CurFont, int ParaStyId)
  {
    if ((this.e.TerFont[CurFont].style & 128 /*0x80*/) != 0)
    {
      this.e.TerFont[CurFont].ParaStyId = ParaStyId;
      return (ushort) CurFont;
    }
    if ((this.e.TerFont[CurFont].style & 64 /*0x40*/) == 0)
    {
      tc.StrFont font = this.e.TerFont[CurFont];
      if (this.e.InRtfRead || font.CharStyId != 1)
        font.ParaStyId = ParaStyId;
      else
        this.SetCharStyleId(ref font, this.e.StyleId[font.ParaStyId], this.e.StyleId[ParaStyId], true);
      int newFont;
      if ((newFont = this.GetNewFont(this.e.TerGr, CurFont, font.TypeFace, font.TwipsSize, font.style, font.TextColor, font.TextBkColor, font.UlineColor, font.FieldId, font.AuxId, font.Aux1Id, font.CharStyId, ParaStyId, font.expand, font.TempStyle, font.lang, font.FieldCode, font.offset, font.CharSet, font.flags, font.TextAngle)) >= 0)
        return (ushort) newFont;
    }
    return (ushort) CurFont;
  }

  /// <summary>Применить стиль параграфа</summary>
  /// <param name="CurStyle"></param>
  /// <param name="applyCharStyle">Применить стиль символов</param>
  /// <returns></returns>
  internal bool ApplyParaStyles(int CurStyle, bool applyCharStyle)
  {
    tc.StrStyleId strStyleId1 = new tc.StrStyleId();
    if (applyCharStyle)
    {
      if (this.e.StyleId[CurStyle].TypeFace.Length == 0)
        this.e.StyleId[CurStyle].TypeFace = this.e.PrevStyleId.TypeFace;
      if (this.e.StyleId[CurStyle].TwipsSize == 0)
        this.e.StyleId[CurStyle].TwipsSize = this.e.PrevStyleId.TwipsSize;
      for (int NewFont = 1; NewFont < this.e.TotalFonts; ++NewFont)
      {
        if (this.e.TerFont[NewFont].InUse && (this.e.TerFont[NewFont].style & 128 /*0x80*/) == 0 && this.e.TerFont[NewFont].ParaStyId == CurStyle)
        {
          this.SetCharStyleId(ref this.e.TerFont[NewFont], this.e.PrevStyleId, this.e.StyleId[CurStyle], false);
          tc.StrStyleId strStyleId2 = this.e.StyleId[this.e.TerFont[NewFont].CharStyId];
          if (!this.e.FullRenderMode && strStyleId2.TypeFace == this.e.PrevStyleId.TypeFace || this.e.FullRenderMode && this.misc.strcmpi(strStyleId2.TypeFace, this.e.PrevStyleId.TypeFace) == 0 && strStyleId2.TypeFace.Length > 0)
            this.e.TerFont[NewFont].TypeFace = strStyleId2.TypeFace;
          if (strStyleId2.TwipsSize == this.e.PrevStyleId.TwipsSize && strStyleId2.TwipsSize > 0)
            this.e.TerFont[NewFont].TwipsSize = strStyleId2.TwipsSize;
          if (strStyleId2.expand == this.e.PrevStyleId.expand)
            this.e.TerFont[NewFont].expand = strStyleId2.expand;
          if (strStyleId2.offset == this.e.PrevStyleId.offset)
            this.e.TerFont[NewFont].offset = strStyleId2.offset;
          if (strStyleId2.TextColor == this.e.PrevStyleId.TextColor && this.e.PrevStyleId.TextColor != this.ToColor(0))
            this.e.TerFont[NewFont].TextColor = this.NewColor(strStyleId2.TextColor);
          if (strStyleId2.TextBkColor == this.e.PrevStyleId.TextBkColor && this.e.PrevStyleId.TextBkColor != tc.CLR_WHITE)
            this.e.TerFont[NewFont].TextBkColor = this.NewColor(strStyleId2.TextBkColor);
          if (strStyleId2.UlineColor == this.e.PrevStyleId.UlineColor && this.e.PrevStyleId.UlineColor != this.ToColor(0))
            this.e.TerFont[NewFont].UlineColor = this.NewColor(strStyleId2.UlineColor);
          this.CreateOneFont(this.e.TerGr, NewFont, true);
        }
      }
      if (CurStyle == 0)
      {
        if (this.e.StyleId[CurStyle].TypeFace.Length > 0)
        {
          this.e.TerFont[0].TypeFace = this.e.StyleId[CurStyle].TypeFace;
          this.e.TerArg.FontTypeFace = this.e.StyleId[CurStyle].TypeFace;
        }
        if (this.e.StyleId[CurStyle].TwipsSize > 0)
        {
          this.e.TerFont[0].TwipsSize = this.e.StyleId[CurStyle].TwipsSize;
          this.e.TerArg.PointSize = this.e.StyleId[CurStyle].TwipsSize / 20;
        }
        this.e.TerFont[0].style = this.e.StyleId[CurStyle].style;
        this.e.TerFont[0].TextColor = this.e.StyleId[CurStyle].TextColor;
        this.e.TerFont[0].TextBkColor = this.e.StyleId[CurStyle].TextBkColor;
        this.e.TerFont[0].UlineColor = this.e.StyleId[CurStyle].UlineColor;
        this.e.TerFont[0].expand = this.e.StyleId[CurStyle].expand;
        this.e.TerFont[0].offset = this.e.StyleId[CurStyle].offset;
        this.CreateOneFont(this.e.TerGr, 0, true);
      }
    }
    for (int index = 1; index < this.e.TotalPfmts; ++index)
    {
      if (this.e.PfmtId[index].StyId == CurStyle)
        this.SetParaStyleId(ref this.e.PfmtId[index], this.e.PrevStyleId, this.e.StyleId[CurStyle], false);
    }
    if (CurStyle == 0)
    {
      this.e.PfmtId[0].LeftIndentTwips = this.e.StyleId[CurStyle].LeftIndentTwips;
      this.e.PfmtId[0].RightIndentTwips = this.e.StyleId[CurStyle].RightIndentTwips;
      this.e.PfmtId[0].FirstIndentTwips = this.e.StyleId[CurStyle].FirstIndentTwips;
      this.e.PfmtId[0].LeftIndent = this.MulDiv(this.e.PfmtId[0].LeftIndentTwips, this.e.ScrResX, 1440);
      this.e.PfmtId[0].RightIndent = this.MulDiv(this.e.PfmtId[0].RightIndentTwips, this.e.ScrResX, 1440);
      this.e.PfmtId[0].FirstIndent = this.MulDiv(this.e.PfmtId[0].FirstIndentTwips, this.e.ScrResX, 1440);
      this.e.PfmtId[0].flags = this.e.StyleId[CurStyle].ParaFlags & -129;
      this.e.PfmtId[0].pflags = this.e.StyleId[CurStyle].pflags;
      this.e.PfmtId[0].shading = this.e.StyleId[CurStyle].shading;
      this.e.PfmtId[0].SpaceBefore = this.e.StyleId[CurStyle].SpaceBefore;
      this.e.PfmtId[0].SpaceAfter = this.e.StyleId[CurStyle].SpaceAfter;
      this.e.PfmtId[0].SpaceBetween = this.e.StyleId[CurStyle].SpaceBetween;
      this.e.PfmtId[0].LineSpacing = this.e.StyleId[CurStyle].LineSpacing;
      this.e.PfmtId[0].BkColor = this.e.StyleId[CurStyle].ParaBkColor;
      this.e.PfmtId[0].BorderColor = this.e.StyleId[CurStyle].ParaBorderColor;
      this.e.PfmtId[0].TabId = this.e.StyleId[CurStyle].TabId;
    }
    return true;
  }

  internal new bool ApplyParaStyles(int CurStyle) => this.ApplyParaStyles(CurStyle, true);

  internal new bool ChangeLineTextStyle(int LineNo, int CurParaStyId)
  {
    int OldFont = 0;
    int num1 = 0;
    int len = this.e.text[LineNo].len;
    if (len != 0)
    {
      ushort[] numArray = this.OpenCfmt(LineNo);
      for (int index = 0; index < len; ++index)
      {
        int num2 = OldFont;
        OldFont = (int) numArray[index];
        if ((this.e.TerFont[OldFont].style & 128 /*0x80*/) == 0)
        {
          if (OldFont == num2 && index > 0)
          {
            numArray[index] = (ushort) num1;
          }
          else
          {
            num1 = this.GetNewFont(this.e.TerGr, OldFont, this.e.TerFont[OldFont].TypeFace, this.e.TerFont[OldFont].TwipsSize, this.e.TerFont[OldFont].style, this.e.TerFont[OldFont].TextColor, this.e.TerFont[OldFont].TextBkColor, this.e.TerFont[OldFont].UlineColor, this.e.TerFont[OldFont].FieldId, this.e.TerFont[OldFont].AuxId, this.e.TerFont[OldFont].Aux1Id, this.e.TerFont[OldFont].CharStyId, CurParaStyId, this.e.TerFont[OldFont].expand, this.e.TerFont[OldFont].TempStyle, this.e.TerFont[OldFont].lang, this.e.TerFont[OldFont].FieldCode, this.e.TerFont[OldFont].offset, this.e.TerFont[OldFont].CharSet, this.e.TerFont[OldFont].flags, this.e.TerFont[OldFont].TextAngle);
            if (num1 < 0)
              num1 = OldFont;
            numArray[index] = (ushort) num1;
          }
        }
      }
    }
    return true;
  }

  internal bool CheckLinePLevel(int l, int level, ref tc.StrListLevel pLevel, ref int ListOrId)
  {
    tc.StrListLevel strListLevel = pLevel.Copy();
    int num;
    int bltId;
    if (this.e.EditingParaStyle)
    {
      num = this.e.StyleId[this.e.CurSID].ParaFlags;
      bltId = this.e.StyleId[this.e.CurSID].BltId;
    }
    else
    {
      num = this.e.PfmtId[this.e.text[l].pfmt].flags;
      bltId = this.e.PfmtId[this.e.text[l].pfmt].BltId;
    }
    if ((num & 8) == 0 || this.e.TerBlt[bltId].ls == 0)
      return false;
    if (!this.GetListLevelPtr(this.e.TerBlt[bltId].ls, this.e.TerBlt[bltId].lvl, out pLevel))
    {
      pLevel = strListLevel.Copy();
      return false;
    }
    if (level != this.e.TerBlt[bltId].lvl)
    {
      pLevel = strListLevel.Copy();
      return false;
    }
    ListOrId = this.e.TerBlt[bltId].ls;
    return true;
  }

  internal bool ClearAllTabs(bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.WordWrap)
      return false;
    if (this.e.EditingParaStyle)
    {
      this.e.StyleId[this.e.CurSID].TabId = 0;
      this.DrawRuler(false);
      return true;
    }
    int StartLine;
    int EndLine;
    this.GetParaRange(out StartLine, out EndLine);
    if (!this.e.EditingParaStyle)
      this.SaveUndo(StartLine, 0, EndLine, 0, 'P');
    for (int LineNo = StartLine; LineNo <= EndLine; ++LineNo)
    {
      if (this.LineSelected(LineNo))
        this.e.text[LineNo].pfmt = this.NewParaId(this.e.text[LineNo].pfmt, this.e.PfmtId[this.e.text[LineNo].pfmt].LeftIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].RightIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].FirstIndentTwips, 0, this.e.PfmtId[this.e.text[LineNo].pfmt].BltId, this.e.PfmtId[this.e.text[LineNo].pfmt].AuxId, this.e.PfmtId[this.e.text[LineNo].pfmt].Aux1Id, this.e.PfmtId[this.e.text[LineNo].pfmt].StyId, this.e.PfmtId[this.e.text[LineNo].pfmt].shading, this.e.PfmtId[this.e.text[LineNo].pfmt].pflags, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceBefore, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceAfter, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceBetween, this.e.PfmtId[this.e.text[LineNo].pfmt].LineSpacing, this.e.PfmtId[this.e.text[LineNo].pfmt].BkColor, this.e.PfmtId[this.e.text[LineNo].pfmt].BorderSpace, this.e.PfmtId[this.e.text[LineNo].pfmt].flow, this.e.PfmtId[this.e.text[LineNo].pfmt].flags);
    }
    ++this.e.TerArg.modified;
    if (repaint)
    {
      this.PaintTer();
      if (this.e.RulerPending)
        this.DrawRuler(false);
    }
    else
      this.WordWrap(StartLine, EndLine + 10 - StartLine);
    return true;
  }

  internal bool ClearTab(int pos, bool repaint)
  {
    tc.StrTab strTab = new tc.StrTab();
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.WordWrap)
      return false;
    if (pos < 0)
      pos = 0;
    int StartLine;
    int EndLine;
    this.GetParaRange(out StartLine, out EndLine);
    if (!this.e.EditingParaStyle)
      this.SaveUndo(StartLine, 0, EndLine, 0, 'P');
    for (int LineNo = StartLine; LineNo <= EndLine; ++LineNo)
    {
      if (this.LineSelected(LineNo))
      {
        int tabId;
        if (this.e.EditingParaStyle)
        {
          tabId = this.e.StyleId[this.e.CurSID].TabId;
          if (this.e.TerTab[tabId].count == 0)
            break;
        }
        else
        {
          tabId = this.e.PfmtId[this.e.text[LineNo].pfmt].TabId;
          if (this.e.TerTab[tabId].count == 0)
            continue;
        }
        tc.StrTab TabRec = this.e.TerTab[tabId];
        bool flag = false;
        while (TabRec.count > 0)
        {
          int index1 = 0;
          while (index1 < TabRec.count && Math.Abs(TabRec.pos[index1] - pos) >= 45)
            ++index1;
          if (index1 != TabRec.count)
          {
            for (int index2 = index1 + 1; index2 < TabRec.count; ++index2)
            {
              TabRec.pos[index2 - 1] = TabRec.pos[index2];
              TabRec.type[index2 - 1] = TabRec.type[index2];
              TabRec.flags[index2 - 1] = TabRec.flags[index2];
            }
            --TabRec.count;
            flag = true;
          }
          else
            break;
        }
        if (flag)
        {
          int TabId = this.NewTabId(tabId, TabRec);
          if (this.e.EditingParaStyle)
          {
            this.e.StyleId[this.e.CurSID].TabId = TabId;
            this.DrawRuler(false);
            return true;
          }
          this.e.text[LineNo].pfmt = this.NewParaId(this.e.text[LineNo].pfmt, this.e.PfmtId[this.e.text[LineNo].pfmt].LeftIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].RightIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].FirstIndentTwips, TabId, this.e.PfmtId[this.e.text[LineNo].pfmt].BltId, this.e.PfmtId[this.e.text[LineNo].pfmt].AuxId, this.e.PfmtId[this.e.text[LineNo].pfmt].Aux1Id, this.e.PfmtId[this.e.text[LineNo].pfmt].StyId, this.e.PfmtId[this.e.text[LineNo].pfmt].shading, this.e.PfmtId[this.e.text[LineNo].pfmt].pflags, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceBefore, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceAfter, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceBetween, this.e.PfmtId[this.e.text[LineNo].pfmt].LineSpacing, this.e.PfmtId[this.e.text[LineNo].pfmt].BkColor, this.e.PfmtId[this.e.text[LineNo].pfmt].BorderSpace, this.e.PfmtId[this.e.text[LineNo].pfmt].flow, this.e.PfmtId[this.e.text[LineNo].pfmt].flags);
        }
      }
    }
    ++this.e.TerArg.modified;
    if (repaint)
    {
      this.PaintTer();
      if (this.e.RulerPending)
        this.DrawRuler(false);
    }
    else
      this.WordWrap(StartLine, EndLine + 10 - StartLine);
    return true;
  }

  internal new bool ClearTabDlg()
  {
    int index = !this.e.EditingParaStyle ? this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].TabId : this.e.StyleId[this.e.CurSID].TabId;
    this.e.DlgInt1 = index;
    if (this.CallDialogBox((Form) new terdlg_clear_tab(this.e)))
    {
      int dlgInt2 = this.e.DlgInt2;
      if (dlgInt2 >= 0)
        this.ClearTab(this.e.TerTab[index].pos[dlgInt2], true);
    }
    return true;
  }

  internal new bool CodeListText(string text, char[] code)
  {
    char[] charArray = text.ToCharArray();
    int length = charArray.Length;
    int index1 = 1;
    int index2 = 0;
    while (index2 < length)
    {
      if (charArray[index2] == '~' && index2 + 2 < length && charArray[index2 + 2] == '~')
      {
        int num = (int) charArray[index2 + 1] - 49;
        if (num < 0)
          num = 0;
        if (num > 8)
          num = 8;
        code[index1] = (char) num;
        index2 += 2;
      }
      else
        code[index1] = charArray[index2];
      ++index2;
      ++index1;
    }
    code[index1] = char.MinValue;
    code[0] = (char) (index1 - 1);
    return true;
  }

  internal new bool CopyCharStyle(int src, int dest)
  {
    this.e.StyleId[dest].TypeFace = this.e.StyleId[src].TypeFace;
    this.e.StyleId[dest].TwipsSize = this.e.StyleId[src].TwipsSize;
    this.e.StyleId[dest].style = this.e.StyleId[src].style;
    this.e.StyleId[dest].TextColor = this.e.StyleId[src].TextColor;
    this.e.StyleId[dest].TextBkColor = this.e.StyleId[src].TextBkColor;
    this.e.StyleId[dest].UlineColor = this.e.StyleId[src].UlineColor;
    this.e.StyleId[dest].expand = this.e.StyleId[src].expand;
    this.e.StyleId[dest].offset = this.e.StyleId[src].offset;
    return true;
  }

  internal new bool CrackAutoNumLgl(string NbrText, out string prefix, out int pNbr)
  {
    prefix = "";
    pNbr = 0;
    string str1 = NbrText;
    int num = str1.Length - 1;
    if (num >= 0)
    {
      if (str1[num] == '.')
      {
        str1 = str1.Substring(0, num);
        --num;
        if (num < 0)
          return true;
      }
      while (num >= 0 && str1[num] != '.')
        --num;
      string txt = str1.Substring(num + 1);
      string str2 = str1.Substring(0, num + 1);
      prefix = str2;
      pNbr = this.ToInt(txt);
    }
    return true;
  }

  internal new int CreateDefList(string name)
  {
    int length = 9;
    int num1 = 0;
    int num2 = 0;
    for (int index = 0; index < this.e.TotalLists; ++index)
    {
      if (this.e.list[index].id > num1)
        num1 = this.e.list[index].id;
      if (this.e.list[index].TmplId > num2)
        num2 = this.e.list[index].TmplId;
    }
    int num3 = num1 + 1;
    int num4 = num2 + 1;
    int listSlot;
    if ((listSlot = this.GetListSlot()) < 0)
      return 0;
    this.e.list[listSlot].InUse = true;
    this.e.list[listSlot].id = num3;
    this.e.list[listSlot].TmplId = num4;
    this.e.list[listSlot].FontId = 0;
    this.e.list[listSlot].LevelCount = length;
    this.e.list[listSlot].flags = 0;
    this.e.list[listSlot].name = name;
    this.e.list[listSlot].level = new tc.StrListLevel[length];
    tc.StrListLevel[] level = this.e.list[listSlot].level;
    for (int index = 0; index < length; ++index)
    {
      level[index].start = 1;
      level[index].CharAft = 2;
      level[index].text = new char[50];
    }
    if (this.strcmpi(name, "NumberDefault") == 0)
    {
      int num5;
      level[6].NumType = num5 = 0;
      int num6;
      level[3].NumType = num6 = num5;
      level[0].NumType = num6;
      int num7;
      level[7].NumType = num7 = 4;
      int num8;
      level[4].NumType = num8 = num7;
      level[1].NumType = num8;
      int num9;
      level[8].NumType = num9 = 2;
      int num10;
      level[5].NumType = num10 = num9;
      level[2].NumType = num10;
      char[] text1 = level[0].text;
      text1[0] = '\u0002';
      text1[1] = char.MinValue;
      text1[2] = ')';
      char[] text2 = level[1].text;
      text2[0] = '\u0002';
      text2[1] = '\u0001';
      text2[2] = ')';
      char[] text3 = level[2].text;
      text3[0] = '\u0002';
      text3[1] = '\u0002';
      text3[2] = ')';
      char[] text4 = level[3].text;
      text4[0] = '\u0003';
      text4[1] = '(';
      text4[2] = '\u0003';
      text4[3] = ')';
      char[] text5 = level[4].text;
      text5[0] = '\u0003';
      text5[1] = '(';
      text5[2] = '\u0004';
      text5[3] = ')';
      char[] text6 = level[5].text;
      text6[0] = '\u0003';
      text6[1] = '(';
      text6[2] = '\u0005';
      text6[3] = ')';
      char[] text7 = level[6].text;
      text7[0] = '\u0002';
      text7[1] = char.MinValue;
      text7[2] = '.';
      char[] text8 = level[7].text;
      text8[0] = '\u0002';
      text8[1] = '\u0001';
      text8[2] = '.';
      char[] text9 = level[8].text;
      text9[0] = '\u0002';
      text9[1] = '\u0002';
      text9[2] = '.';
    }
    else if (this.strcmpi(name, "OutlineDefault") == 0)
    {
      level[0].NumType = 1;
      level[1].NumType = 3;
      int num11;
      level[4].NumType = num11 = 0;
      level[2].NumType = num11;
      int num12;
      level[7].NumType = num12 = 4;
      int num13;
      level[5].NumType = num13 = num12;
      level[3].NumType = num13;
      int num14;
      level[8].NumType = num14 = 2;
      level[6].NumType = num14;
      char[] text10 = level[0].text;
      text10[0] = '\u0002';
      text10[1] = char.MinValue;
      text10[2] = '.';
      char[] text11 = level[1].text;
      text11[0] = '\u0002';
      text11[1] = '\u0001';
      text11[2] = '.';
      char[] text12 = level[2].text;
      text12[0] = '\u0002';
      text12[1] = '\u0002';
      text12[2] = '.';
      char[] text13 = level[3].text;
      text13[0] = '\u0002';
      text13[1] = '\u0003';
      text13[2] = ')';
      char[] text14 = level[4].text;
      text14[0] = '\u0003';
      text14[1] = '(';
      text14[2] = '\u0004';
      text14[3] = ')';
      char[] text15 = level[5].text;
      text15[0] = '\u0003';
      text15[1] = '(';
      text15[2] = '\u0005';
      text15[3] = ')';
      char[] text16 = level[6].text;
      text16[0] = '\u0003';
      text16[1] = '(';
      text16[2] = '\u0006';
      text16[3] = ')';
      char[] text17 = level[7].text;
      text17[0] = '\u0003';
      text17[1] = '(';
      text17[2] = '\a';
      text17[3] = ')';
      char[] text18 = level[8].text;
      text18[0] = '\u0003';
      text18[1] = '(';
      text18[2] = '\b';
      text18[3] = ')';
    }
    else
    {
      for (int index = 0; index < 9; ++index)
        level[index].NumType = 0;
      for (int index1 = 0; index1 < 9; ++index1)
      {
        char[] text = level[index1].text;
        int num15 = index1 + 1;
        text[0] = (char) (num15 * 2);
        int index2 = 1;
        int num16 = 0;
        while (num16 < num15)
        {
          text[index2] = (char) num16;
          int index3 = index2 + 1;
          text[index3] = '.';
          ++num16;
          index2 = index3 + 1;
        }
      }
    }
    return listSlot;
  }

  internal new int CreateDefListOr(int ListId, int StartAt, bool OverrideStartAt, int level)
  {
    int listOrSlot;
    if ((listOrSlot = this.GetListOrSlot()) < 0)
      return 0;
    this.e.ListOr[listOrSlot].InUse = true;
    this.e.ListOr[listOrSlot].ListIdx = ListId;
    int length = level != 0 || OverrideStartAt ? 9 : 0;
    this.e.ListOr[listOrSlot].LevelCount = length;
    if (length != 0)
    {
      this.e.ListOr[listOrSlot].level = new tc.StrListLevel[length];
      for (int index1 = 0; index1 < length; ++index1)
      {
        int index2 = index1 >= this.e.list[ListId].LevelCount ? this.e.list[ListId].LevelCount - 1 : index1;
        this.e.ListOr[listOrSlot].level[index1] = this.e.list[ListId].level[index2];
        if (OverrideStartAt && level == index1)
        {
          this.e.ListOr[listOrSlot].level[index1].flags |= 1;
          this.e.ListOr[listOrSlot].level[index1].start = StartAt;
        }
      }
    }
    return listOrSlot;
  }

  internal new bool DecodeListText(char[] code, out string StrText)
  {
    char[] chArray = new char[100];
    int length = 0;
    int num = (int) code[0];
    int index1 = 1;
    while (index1 <= num)
    {
      if (code[index1] <= '\b')
      {
        chArray[length] = '~';
        int index2 = length + 1;
        chArray[index2] = (char) ((uint) code[index1] + 49U);
        length = index2 + 1;
        chArray[length] = '~';
      }
      else
        chArray[length] = code[index1];
      ++index1;
      ++length;
    }
    chArray[length] = char.MinValue;
    StrText = new string(chArray, 0, length);
    return true;
  }

  internal int DlgEditListFont(int FontId)
  {
    FontDialog fontDialog = new FontDialog();
    if (FontId < 0 || FontId >= this.e.TotalFonts || !this.e.TerFont[FontId].InUse || (this.e.TerFont[FontId].style & 128 /*0x80*/) != 0)
      FontId = 0;
    int style = this.e.TerFont[FontId].style;
    fontDialog.Font = this.e.TerFont[FontId].font;
    fontDialog.Color = this.e.TerFont[FontId].TextColor;
    fontDialog.FontMustExist = true;
    fontDialog.ShowColor = true;
    if (fontDialog.ShowDialog() != DialogResult.Cancel)
    {
      Font font = fontDialog.Font;
      int NewTwipsSize = (int) ((double) font.SizeInPoints * 20.0);
      int NewStyle = 0;
      if (font.Bold)
        NewStyle |= 2;
      if (font.Italic)
        NewStyle |= 4;
      if (font.Underline)
        NewStyle |= 1;
      if (font.Strikeout)
        NewStyle |= 8;
      FontId = this.GetNewFont(this.e.TerGr, FontId, font.FontFamily.Name, NewTwipsSize, NewStyle, fontDialog.Color, this.e.TerFont[FontId].TextBkColor, this.e.TerFont[FontId].UlineColor, this.e.TerFont[FontId].FieldId, this.e.TerFont[FontId].AuxId, this.e.TerFont[FontId].Aux1Id, this.e.TerFont[FontId].CharStyId, this.e.TerFont[FontId].ParaStyId, this.e.TerFont[FontId].expand, this.e.TerFont[FontId].TempStyle, this.e.TerFont[FontId].lang, this.e.TerFont[FontId].FieldCode, this.e.TerFont[FontId].offset, font.GdiCharSet, this.e.TerFont[FontId].flags, this.e.TerFont[FontId].TextAngle);
    }
    return FontId;
  }

  internal new int DlgListCharAft(ComboBox box, int val, bool set)
  {
    string[] strArray = new string[20];
    int[] numArray = new int[20];
    int index1 = 0;
    numArray[index1] = 0;
    strArray[index1] = "Tab";
    int index2 = index1 + 1;
    numArray[index2] = 1;
    strArray[index2] = "Space";
    int index3 = index2 + 1;
    numArray[index3] = 2;
    strArray[index3] = "None";
    int num1 = index3 + 1;
    if (set)
    {
      box.Items.Clear();
      for (int index4 = 0; index4 < num1; ++index4)
      {
        int num2 = box.Items.Add((object) new tc.ClsBox(strArray[index4], numArray[index4]));
        if (numArray[index4] == val)
          box.SelectedIndex = num2;
      }
      return val;
    }
    if (box.SelectedItem == null)
      return 0;
    val = ((tc.ClsBox) box.SelectedItem).value;
    return val;
  }

  internal new int DlgListNumType(ComboBox box, int val, bool set)
  {
    string[] strArray = new string[20];
    int[] numArray = new int[20];
    int index1 = 0;
    numArray[index1] = 0;
    strArray[index1] = "Decimal";
    int index2 = index1 + 1;
    numArray[index2] = 1;
    strArray[index2] = "Uppercase Roman letters";
    int index3 = index2 + 1;
    numArray[index3] = 2;
    strArray[index3] = "Lowercase Roman letters";
    int index4 = index3 + 1;
    numArray[index4] = 3;
    strArray[index4] = "Uppercase alphabets";
    int index5 = index4 + 1;
    numArray[index5] = 4;
    strArray[index5] = "Lowercase alphabets";
    int index6 = index5 + 1;
    numArray[index6] = 6;
    strArray[index6] = "Cardinal numbering";
    int index7 = index6 + 1;
    numArray[index7] = 7;
    strArray[index7] = "Ordinal text numbering";
    int index8 = index7 + 1;
    numArray[index8] = 22;
    strArray[index8] = "Decimal padded";
    int index9 = index8 + 1;
    numArray[index9] = 23;
    strArray[index9] = "Bullet";
    int index10 = index9 + 1;
    numArray[index10] = (int) byte.MaxValue;
    strArray[index10] = "Hidden";
    int num1 = index10 + 1;
    if (set)
    {
      box.Items.Clear();
      for (int index11 = 0; index11 < num1; ++index11)
      {
        int num2 = box.Items.Add((object) new tc.ClsBox(strArray[index11], numArray[index11]));
        if (numArray[index11] == val)
          box.SelectedIndex = num2;
      }
      return val;
    }
    if (box.SelectedItem == null)
      return 0;
    val = ((tc.ClsBox) box.SelectedItem).value;
    return val;
  }

  internal bool FillListBox(object box, bool SelectCurrent, bool IsListBox, int CurList)
  {
    int num = 0;
    ListBox listBox = (ListBox) null;
    ComboBox comboBox = (ComboBox) null;
    if (IsListBox)
      listBox = (ListBox) box;
    else
      comboBox = (ComboBox) box;
    if (SelectCurrent && CurList < 0)
    {
      int bltId = this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].BltId;
      CurList = 1;
      if (bltId > 0 && this.e.TerBlt[bltId].ls > 0)
        CurList = this.e.ListOr[this.e.TerBlt[bltId].ls].ListIdx;
    }
    if (IsListBox)
      listBox.Items.Clear();
    else
      comboBox.Items.Clear();
    int index;
    for (index = 1; index < this.e.TotalLists; ++index)
    {
      if (this.e.list[index].InUse)
      {
        string ArgItem = !this.True(this.e.list[index].name) || this.e.list[index].name.Length <= 0 ? "Unnamed List #" + index.ToString() : this.e.list[index].name;
        if (IsListBox)
          listBox.Items.Add((object) new tc.ClsBox(ArgItem, index));
        else
          comboBox.Items.Add((object) new tc.ClsBox(ArgItem, index));
        ++num;
      }
    }
    if (SelectCurrent)
    {
      for (index = 0; index < num; ++index)
      {
        tc.ClsBox clsBox = !IsListBox ? (tc.ClsBox) comboBox.Items[index] : (tc.ClsBox) listBox.Items[index];
        if (clsBox != null && CurList == clsBox.value)
        {
          if (IsListBox)
          {
            listBox.SelectedIndex = index;
            break;
          }
          comboBox.SelectedIndex = index;
          break;
        }
      }
    }
    else if (IsListBox)
      listBox.SelectedIndex = 0;
    else
      comboBox.SelectedIndex = 0;
    return true;
  }

  internal int FillListOrBox(
    object box,
    bool SelectCurrent,
    bool IsListBox,
    int CurListOr,
    bool MustHaveLevels)
  {
    int num = 0;
    ListBox listBox = (ListBox) null;
    ComboBox comboBox = (ComboBox) null;
    if (IsListBox)
      listBox = (ListBox) box;
    else
      comboBox = (ComboBox) box;
    if (SelectCurrent && CurListOr < 0)
    {
      int bltId = this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].BltId;
      CurListOr = 1;
      if (bltId > 0 && this.e.TerBlt[bltId].ls > 0)
        CurListOr = this.e.TerBlt[bltId].ls;
    }
    if (IsListBox)
      listBox.Items.Clear();
    else
      comboBox.Items.Clear();
    for (int ArgValue = 1; ArgValue < this.e.TotalListOr; ++ArgValue)
    {
      if (this.e.ListOr[ArgValue].InUse && (!MustHaveLevels || this.e.ListOr[ArgValue].LevelCount != 0))
      {
        int listIdx = this.e.ListOr[ArgValue].ListIdx;
        string ArgItem = !this.True(this.e.list[listIdx].name) || this.e.list[listIdx].name.Length <= 0 ? "List Override #" + $"{ArgValue:d3}" : $"List: {this.e.list[listIdx].name}, Override# {$"{ArgValue:d3}"}";
        if (IsListBox)
          listBox.Items.Add((object) new tc.ClsBox(ArgItem, ArgValue));
        else
          comboBox.Items.Add((object) new tc.ClsBox(ArgItem, ArgValue));
        ++num;
      }
    }
    if (SelectCurrent && CurListOr != 0)
    {
      for (int index = 0; index < num; ++index)
      {
        tc.ClsBox clsBox = !IsListBox ? (tc.ClsBox) comboBox.Items[index] : (tc.ClsBox) listBox.Items[index];
        if (clsBox != null && CurListOr == clsBox.value)
        {
          if (IsListBox)
          {
            listBox.SelectedIndex = index;
            break;
          }
          comboBox.SelectedIndex = index;
          break;
        }
      }
      return CurListOr;
    }
    if (IsListBox)
    {
      listBox.SelectedIndex = 0;
      return CurListOr;
    }
    comboBox.SelectedIndex = 0;
    return CurListOr;
  }

  internal new bool FillStyleBox(object box, int type, bool SelectCurrent, bool IsListBox)
  {
    int num1 = 0;
    ListBox listBox = (ListBox) null;
    ComboBox comboBox = (ComboBox) null;
    if (IsListBox)
      listBox = (ListBox) box;
    else
      comboBox = (ComboBox) box;
    if (SelectCurrent)
    {
      switch (type)
      {
        case 1:
          num1 = this.e.TerFont[this.GetEffectiveCfmt()].CharStyId;
          break;
        case 2:
          num1 = this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].StyId;
          break;
        default:
          num1 = this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].StyId;
          if (num1 == 0)
          {
            int effectiveCfmt = this.GetEffectiveCfmt();
            if (this.e.TerFont[effectiveCfmt].CharStyId != 1)
            {
              num1 = this.e.TerFont[effectiveCfmt].CharStyId;
              break;
            }
            break;
          }
          break;
      }
    }
    if (IsListBox)
      listBox.Items.Clear();
    else
      comboBox.Items.Clear();
    int num2 = 0;
    for (int ArgValue = 0; ArgValue < this.e.TotalSID; ++ArgValue)
    {
      if (this.e.StyleId[ArgValue].InUse && (this.e.StyleId[ArgValue].type == type || type == 0))
      {
        if (IsListBox)
          listBox.Items.Add((object) new tc.ClsBox(this.e.StyleId[ArgValue].name, ArgValue));
        else
          comboBox.Items.Add((object) new tc.ClsBox(this.e.StyleId[ArgValue].name, ArgValue));
        ++num2;
      }
    }
    if (SelectCurrent)
    {
      for (int index = 0; index < num2; ++index)
      {
        int num3 = !IsListBox ? ((tc.ClsBox) comboBox.Items[index]).value : ((tc.ClsBox) listBox.Items[index]).value;
        if (num1 == num3)
        {
          if (IsListBox)
          {
            listBox.SelectedIndex = index;
            break;
          }
          comboBox.SelectedIndex = index;
          break;
        }
      }
    }
    else if (IsListBox)
      listBox.SelectedIndex = 0;
    else
      comboBox.SelectedIndex = 0;
    return true;
  }

  internal int ForceCharStyle(int OldFmt)
  {
    tc.StrFont font = this.e.TerFont[OldFmt];
    int index = 1;
    int charStyId = this.e.TerFont[OldFmt].CharStyId;
    this.SetCharStyleId(ref font, this.e.StyleId[index], this.e.StyleId[charStyId], true);
    int newFont;
    return (newFont = this.GetNewFont(this.e.TerGr, OldFmt, font.TypeFace, font.TwipsSize, font.style, font.TextColor, font.TextBkColor, font.UlineColor, font.FieldId, font.AuxId, font.Aux1Id, charStyId, font.ParaStyId, font.expand, font.TempStyle, font.lang, font.FieldCode, font.offset, font.CharSet, font.flags, font.TextAngle)) >= 0 ? newFont : OldFmt;
  }

  internal new bool FreeList(int ListId)
  {
    this.e.list[ListId] = new tc.StrList();
    return true;
  }

  internal new bool FreeListOr(int ListId)
  {
    this.e.ListOr[ListId] = new tc.StrListOr();
    return true;
  }

  internal new bool FreeListTable()
  {
    for (int ListId = 1; ListId < this.e.TotalLists; ++ListId)
      this.FreeList(ListId);
    this.e.TotalLists = 1;
    for (int ListId = 1; ListId < this.e.TotalListOr; ++ListId)
      this.FreeListOr(ListId);
    this.e.TotalListOr = 1;
    return true;
  }

  internal bool GetDlgListLevelPtr(
    bool ListItem,
    ComboBox box,
    ComboBox ListOrBox,
    ComboBox LevelBox,
    out tc.StrListLevel pLevel,
    out int id,
    out int level)
  {
    int num;
    level = num = 0;
    id = num;
    tc.StrListLevel strListLevel = new tc.StrListLevel();
    pLevel = strListLevel.init();
    int levelCount;
    tc.StrListLevel[] level1;
    if (ListItem)
    {
      int selectedIndex = box.SelectedIndex;
      if (box.SelectedItem == null)
        return false;
      int index = ((tc.ClsBox) box.SelectedItem).value;
      levelCount = this.e.list[index].LevelCount;
      level1 = this.e.list[index].level;
      id = index;
    }
    else
    {
      int selectedIndex = ListOrBox.SelectedIndex;
      if (ListOrBox.SelectedItem == null)
        return false;
      int index = ((tc.ClsBox) ListOrBox.SelectedItem).value;
      levelCount = this.e.ListOr[index].LevelCount;
      level1 = this.e.ListOr[index].level;
      id = index;
    }
    if (level1 == null || LevelBox.SelectedItem == null)
      return false;
    int index1 = ((tc.ClsBox) LevelBox.SelectedItem).value;
    if (index1 >= levelCount)
      return false;
    level = index1;
    pLevel = level1[index1];
    return true;
  }

  internal new bool GetFieldList(int FieldFont, int LineNo, out int pListOrId, out int pLevel)
  {
    int num1 = 0;
    int x1 = 0;
    ref tc.StrFont local = ref this.e.TerFont[FieldFont];
    int num2;
    pLevel = num2 = 0;
    pListOrId = num2;
    bool flag = true;
    for (int index = LineNo; index >= 0; --index)
    {
      x1 = this.e.PfmtId[this.e.text[index].pfmt].BltId;
      if (!this.True(x1))
      {
        if ((this.e.text[index].flags & 4) != 0)
          flag = false;
      }
      else
        break;
    }
    if (x1 == 0)
    {
      flag = false;
      for (int index = LineNo; index < this.e.TotalLines; ++index)
      {
        x1 = this.e.PfmtId[this.e.text[index].pfmt].BltId;
        if (this.True(x1))
          break;
      }
    }
    string x2 = this.e.TerFont[FieldFont].FieldCode;
    if (this.True(x2))
      x2 = x2.Trim();
    int level = 0;
    for (int index = LineNo - 1; index >= 0; --index)
    {
      if ((this.e.text[index].flags & 134217728 /*0x08000000*/) != 0 && this.True(this.e.text[index].tabw) && this.e.text[index].tabw.ListnumCount > 0 && this.True(this.e.text[index].tabw.pListnum))
      {
        level = this.e.text[index].tabw.pListnum[this.e.text[index].tabw.ListnumCount - 1].lvl;
        break;
      }
      if (this.True(this.e.PfmtId[this.e.text[index].pfmt].BltId))
      {
        level = this.e.TerBlt[x1].lvl;
        break;
      }
    }
    int num3;
    if ((this.False(x2) || x2.Length == 0) && x1 > 0)
    {
      num3 = this.e.TerBlt[x1].ls;
      if (flag)
      {
        level = this.e.TerBlt[this.e.PfmtId[this.e.text[LineNo].pfmt].BltId].lvl + 1;
        if (level > 8)
          level = 8;
      }
    }
    else
    {
      string ParamList = (!this.True(x2) ? "" : x2).ToUpper().Trim();
      int length = ParamList.Length;
      int num4 = 0;
      while (num4 < length && ParamList[num4] != ' ' && ParamList[num4] != '\\')
        ++num4;
      string str = ParamList.Substring(0, num4);
      str.Trim();
      if (str.Length > 0 && this.True(x1) && this.True(this.e.TerBlt[x1].ls))
      {
        int listIdx = this.e.ListOr[this.e.TerBlt[x1].ls].ListIdx;
        if (listIdx >= 0 && listIdx < this.e.TotalLists && this.True(this.e.list[listIdx].name) && this.strcmpi(this.e.list[listIdx].name, str) == 0)
        {
          num1 = this.e.TerBlt[x1].ls;
          level = this.e.TerBlt[x1].lvl + 1;
        }
      }
      if (str.Length == 0)
      {
        num1 = this.e.TerBlt[x1].ls;
        level = this.e.TerBlt[x1].lvl;
      }
      bool pFound;
      int fieldSwitchLong = this.GetFieldSwitchLong(ParamList, "\\S", 1, out pFound);
      level = this.GetFieldSwitchLong(ParamList, "\\L", level + 1, out tc.SkipBool) - 1;
      if (str.Length == 0)
      {
        if (x1 > 0)
        {
          num3 = this.e.TerBlt[x1].ls;
          goto label_61;
        }
        str = "NumberDefault";
        if (ParamList.Length == 0 && this.True(this.e.text[LineNo].tabw))
        {
          if (this.False(this.e.text[LineNo].tabw.pListnum) || this.e.text[LineNo].tabw.ListnumCount == 0)
          {
            level = 0;
          }
          else
          {
            int index = this.e.text[LineNo].tabw.ListnumCount - 1;
            level = this.e.text[LineNo].tabw.pListnum[index].lvl + 1;
          }
        }
      }
      if (level < 0)
        level = 0;
      if (level > 8)
        level = 8;
      int index1 = 0;
      while (index1 < this.e.TotalLists && (!this.e.list[index1].InUse || !this.True(this.e.list[index1].name) || this.strcmpi(this.e.list[index1].name, str) != 0))
        ++index1;
      int ListId = index1 >= this.e.TotalLists ? this.CreateDefList(str) : index1;
      int index2;
      for (index2 = 0; index2 < this.e.TotalListOr; ++index2)
      {
        if (this.e.ListOr[index2].ListIdx == ListId)
        {
          if (this.e.ListOr[index2].LevelCount > 0)
          {
            int index3 = 0;
            while (index3 < this.e.ListOr[index2].LevelCount && (this.e.ListOr[index2].level[index3].flags & 16 /*0x10*/) == 0)
              ++index3;
            if (index3 < this.e.ListOr[index2].LevelCount)
              continue;
          }
          if (pFound)
          {
            if (level < this.e.ListOr[index2].LevelCount && this.e.ListOr[index2].level[level].start == fieldSwitchLong && (this.e.ListOr[index2].level[level].flags & 1) != 0)
              break;
          }
          else if (level < this.e.ListOr[index2].LevelCount)
          {
            if ((this.e.ListOr[index2].level[level].flags & 1) != 0)
              break;
          }
          else if (level < this.e.list[ListId].LevelCount)
            break;
        }
      }
      num3 = index2 >= this.e.TotalListOr ? this.CreateDefListOr(ListId, fieldSwitchLong, pFound, level) : index2;
    }
label_61:
    pListOrId = num3;
    pLevel = level;
    return true;
  }

  internal new int GetFieldSwitchLong(
    string ParamList,
    string param,
    int DefValue,
    out bool pFound)
  {
    int fieldSwitchLong = DefValue;
    pFound = false;
    int num;
    if ((num = ParamList.IndexOf(param)) == -1)
      return fieldSwitchLong;
    string str = ParamList.Substring(num + param.Length);
    int index = 0;
    while (index < str.Length && str[index] == ' ')
      ++index;
    if (index == str.Length)
      return fieldSwitchLong;
    int startIndex = index;
    while (index < str.Length && str[index] != ' ')
      ++index;
    string txt = str.Substring(startIndex, index - startIndex);
    pFound = true;
    return this.ToInt(txt);
  }

  internal new int GetFirstIndent(
    int LineNo,
    out int pBulletWidth,
    out bool pHasBullet,
    bool screen)
  {
    bool flag1 = false;
    int pfmt = this.e.text[LineNo].pfmt;
    int x = 0;
    pHasBullet = false;
    pBulletWidth = 0;
    int firstIndent = !screen ? this.TwipsToUnitX(this.e.PfmtId[pfmt].FirstIndentTwips) : this.TwipsToScrX(this.e.PfmtId[pfmt].FirstIndentTwips);
    if ((this.e.text[LineNo].flags & 33554432 /*0x02000000*/) != 0)
    {
      x = this.True(this.e.text[LineNo].tabw) ? this.e.text[LineNo].tabw.ListTextWidth : 0;
      if (((!this.e.TerArg.PageMode ? 0 : (!this.e.TerArg.FittedView ? 1 : 0)) & (screen ? 1 : 0)) != 0)
        x = this.UnitToScrX(x);
      firstIndent += x;
      flag1 = true;
    }
    else if ((this.e.PfmtId[pfmt].flags & 8) != 0 && this.e.PfmtId[pfmt].FirstIndent < 0)
    {
      int bltId = this.e.PfmtId[pfmt].BltId;
      bool flag2 = false;
      if ((this.e.TerBlt[bltId].flags & 1) != 0)
        flag2 = true;
      if (!flag2)
      {
        x = -firstIndent;
        firstIndent = 0;
        flag1 = true;
      }
    }
    pHasBullet = flag1;
    pBulletWidth = x;
    return firstIndent;
  }

  internal new int GetHeadingNo(int CurStyle)
  {
    string str = "heading ";
    int length = str.Length;
    return this.e.StyleId[CurStyle].name.Length < length || this.e.StyleId[CurStyle].name.Substring(0, length) != str || this.e.StyleId[CurStyle].name.Length > length + 1 ? 0 : (int) this.e.StyleId[CurStyle].name[length] - 48 /*0x30*/;
  }

  internal new bool GetListLevelPtr(int ListOrId, int level, out tc.StrListLevel pLevel)
  {
    tc.StrListLevel strListLevel = new tc.StrListLevel();
    pLevel = strListLevel.init();
    if (ListOrId == 0 || ListOrId >= this.e.TotalListOr)
      return false;
    tc.StrListOr strListOr = this.e.ListOr[ListOrId];
    int listIdx = strListOr.ListIdx;
    if (listIdx <= 0 || listIdx >= this.e.TotalLists)
      return false;
    tc.StrList strList = this.e.list[listIdx];
    if (strListOr.LevelCount == 0 || level >= strListOr.LevelCount)
    {
      if (level >= strList.LevelCount)
      {
        if (strList.LevelCount <= 0)
          return false;
        level = strList.LevelCount - 1;
      }
      pLevel = strList.level[level];
    }
    else
    {
      if (level >= strListOr.LevelCount)
        return false;
      pLevel = strListOr.level[level];
    }
    return true;
  }

  internal new int GetListOrSlot()
  {
    for (int listOrSlot = 1; listOrSlot < this.e.TotalListOr; ++listOrSlot)
    {
      if (!this.e.ListOr[listOrSlot].InUse)
      {
        this.e.ListOr[listOrSlot] = new tc.StrListOr();
        return listOrSlot;
      }
    }
    if (this.e.TotalListOr >= this.e.MaxListOr)
    {
      int num = this.e.MaxListOr + this.e.MaxListOr / 2;
      this.e.ListOr = this.ReAlloc(this.e.ListOr, num + 1);
      this.e.MaxListOr = num;
    }
    this.e.ListOr[this.e.TotalListOr] = new tc.StrListOr();
    ++this.e.TotalListOr;
    return this.e.TotalListOr - 1;
  }

  internal new int GetListSlot()
  {
    for (int listSlot = 1; listSlot < this.e.TotalLists; ++listSlot)
    {
      if (!this.e.list[listSlot].InUse)
      {
        this.e.list[listSlot] = new tc.StrList();
        return listSlot;
      }
    }
    if (this.e.TotalLists >= this.e.MaxLists)
    {
      int num = this.e.MaxLists + this.e.MaxLists / 2;
      this.e.list = this.ReAlloc(this.e.list, num + 1);
      this.e.MaxLists = num;
    }
    this.e.list[this.e.TotalLists] = new tc.StrList();
    ++this.e.TotalLists;
    return this.e.TotalLists - 1;
  }

  internal new bool GetListText(
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
    int num1 = 0;
    char[] chArray1 = new char[50];
    char[] chArray2 = new char[50];
    tc.StrListLevel pLevel1 = new tc.StrListLevel().init();
    tc.StrListLevel pLevel2 = new tc.StrListLevel().init();
    bool flag = false;
    ListText = "";
    int num2;
    pFontId = num2 = 0;
    int num3;
    pListNbr = num3 = num2;
    pListTextWidth = num3;
    chArray2[0] = char.MinValue;
    int OldFont = 0;
    int num4 = 0;
    if (!this.LineInfo(LineNo, 32 /*0x20*/))
    {
      int ls;
      int lvl;
      if (FieldNbr >= 0)
      {
        if (!this.False(this.e.text[LineNo].tabw) && FieldNbr < this.e.text[LineNo].tabw.ListnumCount)
        {
          tc.StrListnum[] pListnum = this.e.text[LineNo].tabw.pListnum;
          ls = pListnum[FieldNbr].ls;
          lvl = pListnum[FieldNbr].lvl;
        }
        else
          goto label_42;
      }
      else if ((this.e.PfmtId[ParaId].flags & 8) != 0)
      {
        int bltId = this.e.PfmtId[ParaId].BltId;
        ls = this.e.TerBlt[bltId].ls;
        lvl = this.e.TerBlt[bltId].lvl;
      }
      else
        goto label_42;
      if (this.GetListLevelPtr(ls, lvl, out pLevel1))
      {
        int num5 = UseLogUnits ? this.TwipsToUnitX(pLevel1.MinIndent) : this.TwipsToScrX(pLevel1.MinIndent);
        char[] text = pLevel1.text;
        int num6 = (int) text[0];
        if (num6 < 50)
        {
          int length = 0;
          for (int index = 1; index <= num6; ++index)
          {
            if (text[index] > '\t')
            {
              chArray2[length] = text[index];
              ++length;
            }
            else
            {
              int level = (int) text[index];
              int numberForLevel = this.GetNumberForLevel(LineNo, ls, level, lvl, FieldNbr);
              if (lvl == level)
                num1 = numberForLevel;
              this.GetListLevelPtr(ls, level, out pLevel2);
              int num7 = pLevel2.NumType;
              if (level < lvl && (pLevel1.flags & 8) != 0 && num7 != (int) byte.MaxValue)
                num7 = 0;
              string str = num7 != (int) byte.MaxValue ? (num7 != 3 ? (num7 != 4 ? (num7 != 1 ? (num7 != 2 ? (num7 != 6 ? (num7 != 7 ? (num7 != 22 ? $"{numberForLevel}" : $"{numberForLevel,2}") : this.OrdinalString(numberForLevel, true, (this.e.TerFont[pLevel2.FontId].style & 196608 /*0x030000*/) != 0)) : this.OrdinalString(numberForLevel, false, (this.e.TerFont[pLevel2.FontId].style & 196608 /*0x030000*/) != 0)) : this.romanize(numberForLevel, false)) : this.romanize(numberForLevel, true)) : this.AlphaFormat(numberForLevel, false)) : this.AlphaFormat(numberForLevel, true)) : "";
              flag = num7 == 1 || num7 == 2;
              chArray2[length] = char.MinValue;
              this.lstrcat(chArray2, str.ToCharArray());
              length = this.lstrlen(chArray2);
            }
          }
          chArray2[length] = char.MinValue;
          ListText = new string(chArray2, 0, length);
          ListText.TrimEnd((char[]) null);
          if (pLevel1.CharAft == 1)
            ListText += " ";
          OldFont = pLevel1.FontId;
          string typeFace = this.e.TerFont[CurParaFont].TypeFace;
          int twipsSize = this.e.TerFont[CurParaFont].TwipsSize;
          int style = this.e.TerFont[CurParaFont].style;
          Color textColor = this.e.TerFont[CurParaFont].TextColor;
          Color textBkColor = this.e.TerFont[CurParaFont].TextBkColor;
          int num8 = 0;
          if (this.strcmpi(this.e.TerFont[OldFont].TypeFace, this.e.TerFont[0].TypeFace) != 0)
          {
            typeFace = this.e.TerFont[OldFont].TypeFace;
            ++num8;
          }
          if (this.e.TerFont[OldFont].TwipsSize != this.e.TerFont[0].TwipsSize)
          {
            twipsSize = this.e.TerFont[OldFont].TwipsSize;
            ++num8;
          }
          if (this.True(this.e.TerFont[OldFont].style) || this.True(pLevel1.FontStylesOff))
          {
            style = this.e.TerFont[OldFont].style;
            ++num8;
          }
          if (this.e.TerFont[OldFont].TextColor != this.e.TerFont[0].TextColor && this.e.TerFont[OldFont].TextColor != tc.CLR_AUTO)
          {
            textColor = this.e.TerFont[OldFont].TextColor;
            ++num8;
          }
          if (this.e.TerFont[OldFont].TextBkColor != this.e.TerFont[0].TextBkColor && this.e.TerFont[OldFont].TextBkColor != this.e.TextDefBkColor)
          {
            textBkColor = this.e.TerFont[OldFont].TextBkColor;
            ++num8;
          }
          if (num8 == 0)
            OldFont = CurParaFont;
          else if (num8 < 5)
          {
            int fid = this.e.text[LineNo].fid;
            OldFont = this.GetNewFont(UseLogUnits ? this.e.PrtFont[0].gr : this.e.TerGr, OldFont, typeFace, twipsSize, style, textColor, textBkColor, tc.CLR_AUTO, 0, 0, 0, 1, 0, 0, 0, 0, (string) null, 0, (byte) 1, 0, this.e.AllTextAngle > 0 ? this.e.AllTextAngle : this.e.ParaFrame[fid].TextAngle);
          }
          COp.SIZE size;
          this.GetTextExtentPoint(this.e.TerGr, UseLogUnits ? this.e.PrtFont[OldFont].font : this.e.TerFont[OldFont].font, ListText, ListText.Length, out size);
          num4 = size.cx;
          if (pLevel1.CharAft == 0)
          {
            if (flag)
              num4 = 0;
            int x = this.e.PfmtId[ParaId].LeftIndentTwips + this.e.PfmtId[ParaId].FirstIndentTwips;
            int CurPos = (!UseLogUnits ? this.TwipsToScrX(x) : this.TwipsToUnitX(x)) + num4;
            int tabId = this.e.PfmtId[ParaId].TabId;
            int pTabPos;
            this.GetTabPos(ParaId, this.e.TerTab[tabId], CurPos, out pTabPos, out int _, out tc.SkipByte, !UseLogUnits);
            int num9 = pTabPos - CurPos;
            if (num9 < 0)
              num9 = 0;
            num4 += num9;
          }
          if (num4 < num5)
            num4 = num5;
        }
      }
    }
label_42:
    pListTextWidth = num4;
    pFontId = OldFont;
    pListNbr = num1;
    return true;
  }

  internal new ushort GetNewCharStyle(
    ushort OldFmt,
    int data1,
    int data2,
    string str1,
    int line,
    int col)
  {
    int OldFont = (int) OldFmt;
    int NewCharStyId = data1;
    int charStyId = this.e.TerFont[OldFont].CharStyId;
    int styId = this.e.PfmtId[this.e.text[line].pfmt].StyId;
    tc.StrFont font = this.e.TerFont[OldFont];
    if ((this.e.TerFont[OldFont].style & 128 /*0x80*/) == 0)
    {
      if (this.strcmpi(font.TypeFace, this.e.StyleId[styId].TypeFace) == 0)
        font.TypeFace = this.e.TerArg.FontTypeFace;
      if (font.TwipsSize == this.e.StyleId[styId].TwipsSize)
        font.TwipsSize = this.e.TerArg.PointSize * 20;
      font.style = tc.ResetUintFlag(ref font.style, this.e.StyleId[styId].style);
      if (font.TextColor == this.e.StyleId[styId].TextColor)
        font.TextColor = this.ToColor(0);
      if (font.TextBkColor == this.e.StyleId[styId].TextBkColor)
        font.TextBkColor = tc.CLR_WHITE;
      if (font.UlineColor == this.e.StyleId[styId].UlineColor)
        font.UlineColor = this.ToColor(0);
      if (font.expand == this.e.StyleId[styId].expand)
        font.expand = 0;
      if (font.offset == this.e.StyleId[styId].offset)
        font.offset = 0;
      this.SetCharStyleId(ref font, this.e.StyleId[charStyId], this.e.StyleId[NewCharStyId], true);
      this.SetCharStyleId(ref font, this.e.StyleId[font.ParaStyId], this.e.StyleId[styId], false);
      int newFont;
      if ((newFont = this.GetNewFont(this.e.TerGr, OldFont, font.TypeFace, font.TwipsSize, font.style, font.TextColor, font.TextBkColor, font.UlineColor, this.e.TerFont[OldFont].FieldId, this.e.TerFont[OldFont].AuxId, this.e.TerFont[OldFont].Aux1Id, NewCharStyId, this.e.TerFont[OldFont].ParaStyId, font.expand, this.e.TerFont[OldFont].TempStyle, this.e.TerFont[OldFont].lang, this.e.TerFont[OldFont].FieldCode, font.offset, this.e.TerFont[OldFont].CharSet, this.e.TerFont[OldFont].flags, this.e.TerFont[OldFont].TextAngle)) >= 0)
        return (ushort) newFont;
    }
    return OldFmt;
  }

  internal string GetNewListName(string prefix)
  {
    int num1 = 1;
    string newListName;
    while (true)
    {
      int num2 = DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
      newListName = $"{prefix}{num2.ToString()}_{num1.ToString()}";
      int index = 0;
      while (index < this.e.TotalLists && !(this.e.list[index].name == newListName))
        ++index;
      if (index != this.e.TotalLists)
        ++num1;
      else
        break;
    }
    return newListName;
  }

  internal new int GetNumberForLevel(
    int LineNo,
    int InitListOrId,
    int level,
    int LineLevel,
    int InitFieldNbr)
  {
    int num1 = -1;
    int num2 = 1;
    int listIdx1 = this.e.ListOr[InitListOrId].ListIdx;
    if (level < this.e.list[listIdx1].LevelCount)
      num2 = this.e.list[listIdx1].level[level].start;
    int numberForLevel;
    int num3 = numberForLevel = num2 - 1;
    tc.StrListLevel pLevel;
    this.GetListLevelPtr(InitListOrId, level, out pLevel);
    bool flag1 = (pLevel.flags & 32 /*0x20*/) == 0;
    if (InitFieldNbr < 0)
    {
      for (int LineNo1 = LineNo - 1; LineNo1 >= 0 && !this.LineInfo(LineNo1, 46) && (this.e.text[LineNo1].flags & 134217728 /*0x08000000*/) == 0; --LineNo1)
      {
        if ((this.e.text[LineNo1].flags & 4) != 0)
        {
          if ((this.e.text[LineNo1].flags & 33554432 /*0x02000000*/) != 0)
          {
            int bltId = this.e.PfmtId[this.e.text[LineNo1].pfmt].BltId;
            int ls = this.e.TerBlt[bltId].ls;
            if (this.e.ListOr[ls].ListIdx == listIdx1)
            {
              int lvl = this.e.TerBlt[bltId].lvl;
              if (ls == InitListOrId && lvl >= level)
              {
                if (lvl <= level)
                {
                  if (!this.False(this.e.text[LineNo1].tabw))
                    return level < LineLevel ? this.e.text[LineNo1].tabw.ListNbr : this.e.text[LineNo1].tabw.ListNbr + 1;
                  break;
                }
              }
              else
                break;
            }
            else
              break;
          }
          else
            break;
        }
      }
    }
    for (int index1 = 0; index1 < this.e.TotalListOr; ++index1)
    {
      for (int index2 = 0; index2 < this.e.ListOr[index1].LevelCount; ++index2)
        tc.ResetLongFlag(ref this.e.ListOr[index1].level[index2].flags, 4);
    }
    bool flag2 = false;
    for (int LineNo2 = 0; LineNo2 <= LineNo; ++LineNo2)
    {
      if (!this.LineInfo(LineNo2, 44))
      {
        int fid = this.e.text[LineNo2].fid;
        if (!this.True(fid) || (this.e.ParaFrame[fid].flags & 768 /*0x0300*/) == 0)
        {
          if ((this.e.list[listIdx1].flags & 1) != 0 && this.LineInfo(LineNo2, 2))
            flag2 = true;
          int num4 = 0;
          if (LineNo2 == LineNo)
            num4 = InitFieldNbr + 1;
          else if (this.True(this.e.text[LineNo2].tabw))
            num4 = this.e.text[LineNo2].tabw.ListnumCount;
          if (num4 < 0)
            num4 = 0;
          for (int index3 = -1; index3 < num4; ++index3)
          {
            int index4;
            int ls;
            int lvl;
            if (index3 == -1)
            {
              index4 = this.e.PfmtId[this.e.text[LineNo2].pfmt].BltId;
              ls = this.e.TerBlt[index4].ls;
              lvl = this.e.TerBlt[index4].lvl;
              if ((this.e.text[LineNo2].flags & 4) == 0)
                continue;
            }
            else if (!this.False(this.e.text[LineNo2].tabw) && index3 < this.e.text[LineNo2].tabw.ListnumCount)
            {
              tc.StrListnum[] pListnum = this.e.text[LineNo2].tabw.pListnum;
              ls = pListnum[index3].ls;
              lvl = pListnum[index3].lvl;
              index4 = -1;
            }
            else
              continue;
            int listIdx2 = this.e.ListOr[ls].ListIdx;
            if (ls != 0)
            {
              bool flag3 = flag2;
              flag2 = false;
              if (listIdx2 == listIdx1 && (index4 < 0 || (this.e.TerBlt[index4].flags & 1) == 0) && (index4 < 0 || !this.e.TerBlt[index4].IsBullet))
              {
                if (flag3 && lvl == level)
                  numberForLevel = num2 - 1;
                if (lvl > level)
                {
                  if (numberForLevel == num2 - 1)
                    ++numberForLevel;
                }
                else
                {
                  if (ls != num1 && level < this.e.ListOr[ls].LevelCount && (this.e.ListOr[ls].level[level].flags & 1) != 0 && (this.e.ListOr[ls].level[level].flags & 4) == 0)
                  {
                    num2 = this.e.ListOr[ls].level[level].start;
                    numberForLevel = num2 - 1;
                    this.e.ListOr[ls].level[level].flags |= 4;
                    num1 = ls;
                  }
                  if (lvl < level)
                  {
                    if (this.e.ListOr[InitListOrId].LevelCount == 0)
                      numberForLevel = num3;
                    else if (flag1)
                      numberForLevel = num2 - 1;
                  }
                  else
                    ++numberForLevel;
                }
              }
            }
          }
        }
      }
    }
    return numberForLevel;
  }

  /// <summary>Возвращает диапазон выделенных параграфов в строках.
  /// В отличии от GetParaRange не сбрасывает выделение нормализацией</summary>
  /// <param name="StartLine"></param>
  /// <param name="EndLine"></param>
  /// <returns></returns>
  internal bool GetParaRange2(out int StartLine, out int EndLine)
  {
    EndLine = 0;
    StartLine = 0;
    int hilightBegCol = this.e.HilightBegCol;
    int hilightBegRow = this.e.HilightBegRow;
    int hilightEndCol = this.e.HilightEndCol;
    int hilightEndRow = this.e.HilightEndRow;
    int hilightType = this.e.HilightType;
    int curCol = this.e.CurCol;
    int curLine = this.e.CurLine;
    if (!this.blk.NormalizeBlock(ref hilightBegRow, ref hilightBegCol, ref hilightEndRow, ref hilightEndCol, ref hilightType, ref curLine, ref curCol, false))
      hilightType = 0;
    int num;
    int index1;
    if (hilightType != 0)
    {
      num = hilightBegRow;
      index1 = hilightEndRow;
      if (hilightEndCol == 0 && index1 > num && this.e.text[index1].cid == 0)
        --index1;
    }
    else
      num = index1 = curLine;
    for (int index2 = num - 1; index2 >= 0; --index2)
    {
      int len = this.e.text[index2].len;
      if (len > 0)
      {
        char[] txt = this.e.text[index2].txt;
        if ((int) txt[len - 1] == (int) this.e.ParaChar || (int) txt[len - 1] == (int) this.e.CellChar || txt[len - 1] == '\u0012' || this.IsHdrFtrChar(txt[len - 1]) || txt[len - 1] == '\u0014' || txt[len - 1] == '\f' || txt[len - 1] == '\u0016')
        {
          num = index2 + 1;
          break;
        }
      }
      if (index2 == 0)
        num = 0;
    }
    for (int index3 = index1; index3 < this.e.TotalLines; ++index3)
    {
      int len = this.e.text[index3].len;
      if (len > 0)
      {
        char[] txt = this.e.text[index3].txt;
        if ((int) txt[len - 1] == (int) this.e.ParaChar || (int) txt[len - 1] == (int) this.e.CellChar || txt[len - 1] == '\u0012' || this.IsHdrFtrChar(txt[len - 1]) || txt[len - 1] == '\u0014' || txt[len - 1] == '\f' || txt[len - 1] == '\u0016')
        {
          index1 = index3;
          break;
        }
      }
      if (index3 + 1 == this.e.TotalLines)
        index1 = index3;
    }
    StartLine = num;
    EndLine = index1;
    return true;
  }

  internal new bool GetParaRange(out int StartLine, out int EndLine)
  {
    int num1;
    EndLine = num1 = 0;
    StartLine = num1;
    if (!this.NormalizeBlock())
      this.e.HilightType = 0;
    int num2;
    int index1;
    if (this.e.HilightType != 0)
    {
      num2 = this.e.HilightBegRow;
      index1 = this.e.HilightEndRow;
      if (this.e.HilightEndCol == 0 && index1 > num2 && this.e.text[index1].cid == 0)
        --index1;
    }
    else
      num2 = index1 = this.e.CurLine;
    for (int index2 = num2 - 1; index2 >= 0; --index2)
    {
      int len = this.e.text[index2].len;
      if (len > 0)
      {
        char[] txt = this.e.text[index2].txt;
        if ((int) txt[len - 1] == (int) this.e.ParaChar || (int) txt[len - 1] == (int) this.e.CellChar || txt[len - 1] == '\u0012' || this.IsHdrFtrChar(txt[len - 1]) || txt[len - 1] == '\u0014' || txt[len - 1] == '\f' || txt[len - 1] == '\u0016')
        {
          num2 = index2 + 1;
          break;
        }
      }
      if (index2 == 0)
        num2 = 0;
    }
    for (int index3 = index1; index3 < this.e.TotalLines; ++index3)
    {
      int len = this.e.text[index3].len;
      if (len > 0)
      {
        char[] txt = this.e.text[index3].txt;
        if ((int) txt[len - 1] == (int) this.e.ParaChar || (int) txt[len - 1] == (int) this.e.CellChar || txt[len - 1] == '\u0012' || this.IsHdrFtrChar(txt[len - 1]) || txt[len - 1] == '\u0014' || txt[len - 1] == '\f' || txt[len - 1] == '\u0016')
        {
          index1 = index3;
          break;
        }
      }
      if (index3 + 1 == this.e.TotalLines)
        index1 = index3;
    }
    StartLine = num2;
    EndLine = index1;
    return true;
  }

  internal new int GetStyleIdSlot()
  {
    for (int styleIdSlot = 2; styleIdSlot < this.e.TotalSID; ++styleIdSlot)
    {
      if (!this.e.StyleId[styleIdSlot].InUse)
      {
        this.e.StyleId[styleIdSlot] = this.NewStyleId();
        return styleIdSlot;
      }
    }
    if (this.e.TotalSID >= this.e.MaxSID)
    {
      int count = this.e.MaxSID + this.e.MaxSID / 2;
      this.e.StyleId = this.ReAlloc(this.e.StyleId, count);
      this.e.MaxSID = count;
    }
    if (this.e.TotalSID < this.e.MaxSID)
    {
      ++this.e.TotalSID;
      this.e.StyleId[this.e.TotalSID - 1] = this.NewStyleId();
      return this.e.TotalSID - 1;
    }
    this.PrintError(113, (string) null);
    return -1;
  }

  internal new bool HasSameParaBorder(int line1, int line2)
  {
    int index1 = this.e.text[line1].pfmt;
    int index2 = this.e.text[line2].pfmt;
    if (this.e.HtmlMode)
      return false;
    if (this.True(this.e.text[line1].tabw) && (this.e.text[line1].tabw.type & 44) != 0)
      index1 = 0;
    if ((this.e.text[line1].flags & 1966080 /*0x1E0000*/) != 0)
      index1 = 0;
    if (this.True(this.e.text[line2].tabw) && (this.e.text[line2].tabw.type & 44) != 0)
      index2 = 0;
    if ((this.e.text[line2].flags & 1966080 /*0x1E0000*/) != 0)
      index2 = 0;
    int flags1 = this.e.PfmtId[index1].flags;
    int flags2 = this.e.PfmtId[index2].flags;
    if ((flags1 & 1008) != 0 != ((flags2 & 1008) != 0) || (flags1 & 12288 /*0x3000*/) != 0 != ((flags2 & 12288 /*0x3000*/) != 0) || this.e.PfmtId[index1].BorderSpace != this.e.PfmtId[index2].BorderSpace || !this.IsSameColor(this.e.PfmtId[index1].BorderColor, this.e.PfmtId[index2].BorderColor))
      return false;
    int leftIndent1 = this.e.PfmtId[index1].LeftIndent;
    if (this.e.PfmtId[index1].FirstIndent < 0)
      leftIndent1 += this.e.PfmtId[index1].FirstIndent;
    int leftIndent2 = this.e.PfmtId[index2].LeftIndent;
    if (this.e.PfmtId[index2].FirstIndent < 0)
      leftIndent2 += this.e.PfmtId[index2].FirstIndent;
    return leftIndent1 == leftIndent2 && this.e.PfmtId[index1].RightIndent == this.e.PfmtId[index2].RightIndent && this.e.text[line1].cid == this.e.text[line2].cid;
  }

  internal new bool HasSameParaShading(int line1, int line2)
  {
    int index1 = this.e.text[line1].pfmt;
    int index2 = this.e.text[line2].pfmt;
    if (this.e.HtmlMode)
      return false;
    if (this.True(this.e.text[line1].tabw) && (this.e.text[line1].tabw.type & 4) != 0)
      index1 = 0;
    if ((this.e.text[line1].flags & 1966080 /*0x1E0000*/) != 0)
      index1 = 0;
    if (this.True(this.e.text[line2].tabw) && (this.e.text[line2].tabw.type & 4) != 0)
      index2 = 0;
    if ((this.e.text[line2].flags & 1966080 /*0x1E0000*/) != 0)
      index2 = 0;
    if (this.e.PfmtId[index1].shading != this.e.PfmtId[index2].shading || this.e.PfmtId[index1].BkColor != this.e.PfmtId[index2].BkColor || (this.e.PfmtId[index1].flags & 12288 /*0x3000*/) != 0 != ((this.e.PfmtId[index2].flags & 12288 /*0x3000*/) != 0))
      return false;
    int leftIndent1 = this.e.PfmtId[index1].LeftIndent;
    if (this.e.PfmtId[index1].FirstIndent < 0)
      leftIndent1 += this.e.PfmtId[index1].FirstIndent;
    int leftIndent2 = this.e.PfmtId[index2].LeftIndent;
    if (this.e.PfmtId[index2].FirstIndent < 0)
      leftIndent2 += this.e.PfmtId[index2].FirstIndent;
    return leftIndent1 == leftIndent2 && this.e.PfmtId[index1].RightIndent == this.e.PfmtId[index2].RightIndent && this.e.text[line1].cid == this.e.text[line2].cid;
  }

  internal new bool IsLineRtl(int LineNo)
  {
    int flow1 = this.e.PfmtId[this.e.text[LineNo].pfmt].flow;
    if (flow1 != 0)
      return flow1 == 2;
    int cid = this.e.text[LineNo].cid;
    int row = this.e.cell[cid].row;
    int flow2 = cid <= 0 ? 0 : this.e.TableRow[row].flow;
    if (flow2 != 0)
      return flow2 == 2;
    int flow3 = this.e.TerSect[this.GetSection(LineNo)].flow;
    return flow3 != 0 ? flow3 == 2 : this.e.DocTextFlow == 2;
  }

  internal new bool IsListLine(int LineNo)
  {
    if (LineNo < 0 || LineNo >= this.e.TotalLines || this.LineInfo(LineNo, 44))
      return false;
    int pfmt = this.e.text[LineNo].pfmt;
    if ((this.e.PfmtId[pfmt].flags & 8) == 0)
      return false;
    int bltId = this.e.PfmtId[pfmt].BltId;
    return bltId != 0 && !this.e.TerBlt[bltId].IsBullet && this.e.TerBlt[bltId].ls != 0 && this.IsFirstParaLine(LineNo);
  }

  internal new bool IsParaRtl(int ParaFlow, int CellFlow, int SectFlow, int DocFlow)
  {
    if (ParaFlow != 0)
      return ParaFlow == 2;
    if (CellFlow != 0)
      return CellFlow == 2;
    return SectFlow == 0 ? DocFlow == 2 : SectFlow == 2;
  }

  internal new bool IsSameListLevel(tc.StrListLevel pLevel1, tc.StrListLevel pLevel2)
  {
    int num1 = 57;
    if (pLevel1.start != pLevel2.start || pLevel1.NumType != pLevel2.NumType || pLevel1.CharAft != pLevel2.CharAft || pLevel1.LeftIndent != pLevel2.LeftIndent || pLevel1.RightIndent != pLevel2.RightIndent || pLevel1.FirstIndent != pLevel2.FirstIndent || pLevel1.ParaFlags != pLevel2.ParaFlags || pLevel1.FontStyles != pLevel2.FontStyles || (pLevel1.flags & num1) != 0 != ((pLevel2.flags & num1) != 0) || pLevel1.MinIndent != pLevel2.MinIndent || pLevel1.FontStylesOff != pLevel2.FontStylesOff || (int) pLevel1.text[0] != (int) pLevel2.text[0])
      return false;
    int num2 = (int) pLevel1.text[0];
    for (int index = 1; index <= num2; ++index)
    {
      if ((int) pLevel1.text[index] != (int) pLevel2.text[index])
        return false;
    }
    return true;
  }

  internal new bool IsSameListOr(tc.StrListOr pListOr1, tc.StrListOr pListOr2)
  {
    if (pListOr1.InUse && !pListOr2.InUse || !pListOr1.InUse && pListOr2.InUse || pListOr1.ListIdx != pListOr2.ListIdx || pListOr1.LevelCount != pListOr2.LevelCount)
      return false;
    for (int index = 0; index < pListOr1.LevelCount; ++index)
    {
      if (!this.IsSameListLevel(pListOr1.level[index], pListOr2.level[index]))
        return false;
    }
    return true;
  }

  internal new bool MakeAutoNumLgl(
    out string StrOut,
    string prefix,
    int nbr,
    int FromHdng,
    int ToHdng)
  {
    StrOut = prefix;
    int length1 = StrOut.Length;
    int length2;
    if (length1 > 0 && StrOut[length1 - 1] != '.')
    {
      StrOut += ".";
      length2 = StrOut.Length;
    }
    if (FromHdng + 1 < ToHdng)
    {
      for (int index = FromHdng + 1; index < ToHdng; ++index)
        StrOut += "0.";
      length2 = StrOut.Length;
    }
    StrOut += nbr.ToString();
    return true;
  }

  internal new int NewBltId(int old, tc.StrBlt BltRec)
  {
    for (int index = 1; index < this.e.TotalBlts; ++index)
    {
      if (this.e.TerBlt[index].flags == BltRec.flags && this.e.TerBlt[index].start == BltRec.start && this.e.TerBlt[index].level == BltRec.level && this.e.TerBlt[index].NumberType == BltRec.NumberType && (int) this.e.TerBlt[index].BulletChar == (int) BltRec.BulletChar && (int) this.e.TerBlt[index].AftChar == (int) BltRec.AftChar && this.e.TerBlt[index].BefText == BltRec.BefText && this.e.TerBlt[index].ls == BltRec.ls && this.e.TerBlt[index].lvl == BltRec.lvl)
        return index;
    }
    int num = -1;
    if (this.e.TotalBlts >= this.e.MaxBlts)
    {
      bool[] flagArray1 = new bool[this.e.TotalBlts + 1];
      bool[] flagArray2 = new bool[this.e.TotalPfmts];
      for (int index = 0; index < this.e.TotalPfmts; ++index)
        flagArray2[index] = false;
      for (int index = 0; index < this.e.TotalLines; ++index)
        flagArray2[this.e.text[index].pfmt] = true;
      for (int index = 1; index < this.e.TotalPfmts; ++index)
      {
        if (!flagArray2[index])
          this.e.PfmtId[index].BltId = 0;
      }
      for (int index = 1; index < this.e.TotalBlts; ++index)
        flagArray1[index] = false;
      for (int index = 0; index < this.e.TotalPfmts; ++index)
        flagArray1[this.e.PfmtId[index].BltId] = true;
      for (int index = 1; index < this.e.TotalBlts; ++index)
      {
        if (!flagArray1[index])
        {
          if (num < 0)
          {
            num = index;
            break;
          }
          break;
        }
      }
    }
    if (num == -1 && this.e.TotalBlts >= this.e.MaxBlts)
    {
      int count = this.e.MaxBlts + this.e.MaxBlts / 2;
      this.e.TerBlt = this.ReAlloc(this.e.TerBlt, count);
      this.e.MaxBlts = count;
    }
    if (this.e.TotalBlts < this.e.MaxBlts || num >= 0)
    {
      int index;
      if (this.e.TotalBlts < this.e.MaxBlts)
      {
        index = this.e.TotalBlts;
        ++this.e.TotalBlts;
      }
      else
        index = num;
      this.e.TerBlt[index] = BltRec;
      return index;
    }
    this.PrintError(135, (string) null);
    return old;
  }

  internal new int NewParaId(
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
    bool flag = false;
    if (old >= 0 && old < this.e.TotalPfmts)
      flag = true;
    tc.StrPfmt pNew = (!flag ? new tc.StrPfmt() : this.e.PfmtId[old].Copy()) with
    {
      LeftIndentTwips = LeftIndentTwips,
      RightIndentTwips = RightIndentTwips,
      FirstIndentTwips = FirstIndentTwips,
      TabId = TabId,
      BltId = BltId,
      AuxId = AuxId,
      Aux1Id = Aux1Id,
      StyId = StyId,
      shading = shading,
      pflags = pflags,
      SpaceBefore = SpaceBefore,
      SpaceAfter = SpaceAfter,
      SpaceBetween = SpaceBetween,
      LineSpacing = LineSpacing,
      BkColor = BkColor,
      BorderSpace = BorderSpace,
      flow = flow,
      flags = flags
    };
    pNew.LeftIndent = this.TwipsToScrX(pNew.LeftIndentTwips);
    pNew.RightIndent = this.TwipsToScrX(pNew.RightIndentTwips);
    pNew.FirstIndent = this.TwipsToScrX(pNew.FirstIndentTwips);
    pNew.BorderColor = flag ? this.e.PfmtId[old].BorderColor : tc.CLR_AUTO;
    return this.NewParaId2(old, pNew);
  }

  internal new int NewParaId2(int old, tc.StrPfmt pNew)
  {
    if ((pNew.flags & 8) == 0)
      pNew.BltId = 0;
    if (this.e.MatchIds)
    {
      if (this.e.PfmtId[old].LeftIndentTwips == pNew.LeftIndentTwips && this.e.PfmtId[old].RightIndentTwips == pNew.RightIndentTwips && this.e.PfmtId[old].FirstIndentTwips == pNew.FirstIndentTwips && this.e.PfmtId[old].TabId == pNew.TabId && this.e.PfmtId[old].BltId == pNew.BltId && this.e.PfmtId[old].AuxId == pNew.AuxId && this.e.PfmtId[old].Aux1Id == pNew.Aux1Id && this.e.PfmtId[old].StyId == pNew.StyId && this.e.PfmtId[old].shading == pNew.shading && this.e.PfmtId[old].pflags == pNew.pflags && this.e.PfmtId[old].SpaceBefore == pNew.SpaceBefore && this.e.PfmtId[old].SpaceAfter == pNew.SpaceAfter && this.e.PfmtId[old].SpaceBetween == pNew.SpaceBetween && this.e.PfmtId[old].LineSpacing == pNew.LineSpacing && this.e.PfmtId[old].BkColor == pNew.BkColor && this.e.PfmtId[old].BorderColor == pNew.BorderColor && this.e.PfmtId[old].BorderSpace == pNew.BorderSpace && this.e.PfmtId[old].flow == pNew.flow && this.e.PfmtId[old].flags == pNew.flags)
        return old;
      for (int index = 0; index < this.e.TotalPfmts; ++index)
      {
        if (this.e.PfmtId[index].LeftIndentTwips == pNew.LeftIndentTwips && this.e.PfmtId[index].RightIndentTwips == pNew.RightIndentTwips && this.e.PfmtId[index].FirstIndentTwips == pNew.FirstIndentTwips && this.e.PfmtId[index].TabId == pNew.TabId && this.e.PfmtId[index].BltId == pNew.BltId && this.e.PfmtId[index].AuxId == pNew.AuxId && this.e.PfmtId[index].Aux1Id == pNew.Aux1Id && this.e.PfmtId[index].StyId == pNew.StyId && this.e.PfmtId[index].shading == pNew.shading && this.e.PfmtId[index].pflags == pNew.pflags && this.e.PfmtId[index].SpaceBefore == pNew.SpaceBefore && this.e.PfmtId[index].SpaceAfter == pNew.SpaceAfter && this.e.PfmtId[index].SpaceBetween == pNew.SpaceBetween && this.e.PfmtId[index].LineSpacing == pNew.LineSpacing && this.e.PfmtId[index].BkColor == pNew.BkColor && this.e.PfmtId[index].BorderColor == pNew.BorderColor && this.e.PfmtId[index].BorderSpace == pNew.BorderSpace && this.e.PfmtId[index].flow == pNew.flow && this.e.PfmtId[index].flags == pNew.flags)
          return index;
      }
    }
    else
      this.e.MatchIds = true;
    int num1 = -1;
    if (this.e.TotalPfmts >= this.e.MaxPfmts && this.e.MaxPfmts >= 1000 && this.e.ReclaimResources)
    {
      bool[] flagArray = new bool[this.e.MaxPfmts];
      for (int index = 0; index < this.e.MaxPfmts; ++index)
        flagArray[index] = false;
      for (int index = 0; index < this.e.TotalLines; ++index)
        flagArray[this.e.text[index].pfmt] = true;
      for (int index1 = 0; index1 < this.e.UndoTblSize; ++index1)
      {
        if (this.e.undo[index1].type == 'P')
        {
          int[] pfmt = this.e.undo[index1].pfmt;
          if (this.True(pfmt))
          {
            int num2 = pfmt[0];
            for (int index2 = 0; index2 < num2; ++index2)
              flagArray[pfmt[index2 + 1]] = true;
          }
        }
      }
      int index3 = 1;
      while (index3 < this.e.MaxPfmts && flagArray[index3])
        ++index3;
      if (index3 < this.e.MaxPfmts)
        num1 = index3;
    }
    if (this.e.TotalPfmts >= this.e.MaxPfmts && num1 == -1)
    {
      int num3 = this.e.MaxPfmts + this.e.MaxPfmts / 2;
      this.e.PfmtId = this.ReAlloc(this.e.PfmtId, num3 + 1);
      this.e.MaxPfmts = num3;
    }
    if (this.e.TotalPfmts < this.e.MaxPfmts || num1 >= 0)
    {
      int index;
      if (this.e.TotalPfmts < this.e.MaxPfmts)
      {
        index = this.e.TotalPfmts;
        ++this.e.TotalPfmts;
      }
      else
        index = num1;
      this.e.PfmtId[index].LeftIndentTwips = pNew.LeftIndentTwips;
      this.e.PfmtId[index].RightIndentTwips = pNew.RightIndentTwips;
      this.e.PfmtId[index].FirstIndentTwips = pNew.FirstIndentTwips;
      this.e.PfmtId[index].TabId = pNew.TabId;
      this.e.PfmtId[index].BltId = pNew.BltId;
      this.e.PfmtId[index].AuxId = pNew.AuxId;
      this.e.PfmtId[index].Aux1Id = pNew.Aux1Id;
      this.e.PfmtId[index].StyId = pNew.StyId;
      this.e.PfmtId[index].shading = pNew.shading;
      this.e.PfmtId[index].pflags = pNew.pflags;
      this.e.PfmtId[index].SpaceBefore = pNew.SpaceBefore;
      this.e.PfmtId[index].SpaceAfter = pNew.SpaceAfter;
      this.e.PfmtId[index].SpaceBetween = pNew.SpaceBetween;
      this.e.PfmtId[index].LineSpacing = pNew.LineSpacing;
      this.e.PfmtId[index].BkColor = pNew.BkColor;
      this.e.PfmtId[index].BorderColor = pNew.BorderColor;
      this.e.PfmtId[index].BorderSpace = pNew.BorderSpace;
      this.e.PfmtId[index].flow = pNew.flow;
      this.e.PfmtId[index].flags = pNew.flags;
      this.e.PfmtId[index].LeftIndent = this.TwipsToScrX(this.e.PfmtId[index].LeftIndentTwips);
      this.e.PfmtId[index].RightIndent = this.TwipsToScrX(this.e.PfmtId[index].RightIndentTwips);
      this.e.PfmtId[index].FirstIndent = this.TwipsToScrX(this.e.PfmtId[index].FirstIndentTwips);
      return index;
    }
    if ((this.e.MessageDisplayed & 2) == 0)
      this.PrintError(109, (string) null);
    this.e.MessageDisplayed |= 2;
    return old;
  }

  internal new tc.StrStyleId NewStyleId()
  {
    return new tc.StrStyleId()
    {
      TypeFace = "",
      OutlineLevel = -1
    };
  }

  internal new int NewTabId(int old, tc.StrTab TabRec)
  {
    bool[] flagArray1 = new bool[600];
    if (TabRec.count == 0)
      return 0;
    for (int index1 = 0; index1 < this.e.TotalTabs; ++index1)
    {
      if (this.e.TerTab[index1].count == TabRec.count)
      {
        int index2 = 0;
        while (index2 < TabRec.count && this.e.TerTab[index1].pos[index2] == TabRec.pos[index2] && this.e.TerTab[index1].type[index2] == TabRec.type[index2] && (int) this.e.TerTab[index1].flags[index2] == (int) TabRec.flags[index2])
          ++index2;
        if (index2 == TabRec.count)
          return index1;
      }
    }
    int num = -1;
    if (this.e.TotalTabs >= 600)
    {
      for (int index = 0; index < this.e.TotalTabs; ++index)
      {
        if (this.e.TerTab[index].count == 0)
        {
          num = index;
          break;
        }
      }
      if (num == -1)
      {
        bool[] flagArray2 = new bool[this.e.TotalPfmts];
        for (int index = 0; index < this.e.TotalPfmts; ++index)
          flagArray2[index] = false;
        for (int index = 0; index < this.e.TotalLines; ++index)
          flagArray2[this.e.text[index].pfmt] = true;
        for (int index = 1; index < this.e.TotalPfmts; ++index)
        {
          if (!flagArray2[index])
            this.e.PfmtId[index].TabId = 0;
        }
        for (int index = 1; index < 600; ++index)
          flagArray1[index] = false;
        for (int index = 0; index < this.e.TotalPfmts; ++index)
          flagArray1[this.e.PfmtId[index].TabId] = true;
        for (int index = 1; index < 600; ++index)
        {
          if (!flagArray1[index])
          {
            if (num < 0)
              num = index;
            this.e.TerTab[index].count = 0;
            break;
          }
        }
      }
    }
    if (this.e.TotalTabs < 600 || num >= 0)
    {
      int index;
      if (this.e.TotalTabs < 600)
      {
        index = this.e.TotalTabs;
        ++this.e.TotalTabs;
      }
      else
        index = num;
      this.e.TerTab[index] = TabRec.Copy();
      return index;
    }
    if ((this.e.TerOpFlags & 67108864 /*0x04000000*/) == 0)
    {
      this.PrintError(115, (string) null);
      this.e.TerOpFlags |= 67108864 /*0x04000000*/;
    }
    return old;
  }

  internal bool ParaHangingIndent(bool indent, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.WordWrap)
      return false;
    int StartLine;
    int EndLine;
    this.GetParaRange(out StartLine, out EndLine);
    if (!this.e.EditingParaStyle)
      this.SaveUndo(StartLine, 0, EndLine, 0, 'P');
    for (int LineNo = StartLine; LineNo <= EndLine; ++LineNo)
    {
      if (this.LineSelected(LineNo))
      {
        int LeftIndentTwips;
        int firstIndentTwips;
        int tabId;
        if (this.e.EditingParaStyle)
        {
          LeftIndentTwips = this.e.StyleId[this.e.CurSID].LeftIndentTwips;
          firstIndentTwips = this.e.StyleId[this.e.CurSID].FirstIndentTwips;
          tabId = this.e.StyleId[this.e.CurSID].TabId;
        }
        else
        {
          LeftIndentTwips = this.e.PfmtId[this.e.text[LineNo].pfmt].LeftIndentTwips;
          firstIndentTwips = this.e.PfmtId[this.e.text[LineNo].pfmt].FirstIndentTwips;
          tabId = this.e.PfmtId[this.e.text[LineNo].pfmt].TabId;
        }
        int num1 = LeftIndentTwips;
        int count = this.e.TerTab[tabId].count;
        if (indent)
        {
          int index;
          for (index = 0; index < count; ++index)
          {
            if (this.e.TerTab[tabId].pos[index] > LeftIndentTwips)
            {
              LeftIndentTwips = this.e.TerTab[tabId].pos[index];
              break;
            }
          }
          if (index == count)
            LeftIndentTwips = (LeftIndentTwips / this.e.DefTabWidth + 1) * this.e.DefTabWidth;
        }
        else
        {
          int index;
          for (index = count - 1; index >= 0; --index)
          {
            if (this.e.TerTab[tabId].pos[index] < LeftIndentTwips)
            {
              LeftIndentTwips = this.e.TerTab[tabId].pos[index];
              break;
            }
          }
          if (index < 0)
          {
            int num2 = LeftIndentTwips / this.e.DefTabWidth;
            if (LeftIndentTwips == num2 * this.e.DefTabWidth)
              --num2;
            LeftIndentTwips = num2 * this.e.DefTabWidth;
          }
        }
        if (LeftIndentTwips > 8640)
          LeftIndentTwips = 8640;
        if (LeftIndentTwips < 0)
          LeftIndentTwips = 0;
        int FirstIndentTwips = firstIndentTwips - (LeftIndentTwips - num1);
        if (this.e.EditingParaStyle)
        {
          this.e.StyleId[this.e.CurSID].LeftIndentTwips = LeftIndentTwips;
          this.e.StyleId[this.e.CurSID].FirstIndentTwips = FirstIndentTwips;
          this.DrawRuler(false);
          return true;
        }
        this.e.text[LineNo].pfmt = this.NewParaId(this.e.text[LineNo].pfmt, LeftIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].RightIndentTwips, FirstIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].TabId, this.e.PfmtId[this.e.text[LineNo].pfmt].BltId, this.e.PfmtId[this.e.text[LineNo].pfmt].AuxId, this.e.PfmtId[this.e.text[LineNo].pfmt].Aux1Id, this.e.PfmtId[this.e.text[LineNo].pfmt].StyId, this.e.PfmtId[this.e.text[LineNo].pfmt].shading, this.e.PfmtId[this.e.text[LineNo].pfmt].pflags, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceBefore, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceAfter, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceBetween, this.e.PfmtId[this.e.text[LineNo].pfmt].LineSpacing, this.e.PfmtId[this.e.text[LineNo].pfmt].BkColor, this.e.PfmtId[this.e.text[LineNo].pfmt].BorderSpace, this.e.PfmtId[this.e.text[LineNo].pfmt].flow, this.e.PfmtId[this.e.text[LineNo].pfmt].flags);
      }
    }
    ++this.e.TerArg.modified;
    if (!this.e.EditingParaStyle && StartLine < this.e.RepageBeginLine)
      this.e.RepageBeginLine = StartLine;
    if (repaint)
      this.PaintTer();
    else
      this.WordWrap(StartLine, EndLine + 10 - StartLine);
    return true;
  }

  internal bool ParaIndentTwips(int DeltaLeft, int DeltaRight, int DeltaFirst, bool repaint)
  {
    int num1 = 0;
    int num2 = 0;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.WordWrap)
      return false;
    int StartLine;
    int EndLine;
    this.GetParaRange(out StartLine, out EndLine);
    if (!this.e.EditingParaStyle)
      this.SaveUndo(StartLine, 0, EndLine, 0, 'P');
    int num3;
    int num4;
    int num5;
    if (this.e.EditingParaStyle)
    {
      num3 = this.e.StyleId[this.e.CurSID].LeftIndentTwips + DeltaLeft;
      num4 = this.e.StyleId[this.e.CurSID].RightIndentTwips + DeltaRight;
      num5 = this.e.StyleId[this.e.CurSID].FirstIndentTwips + DeltaFirst;
    }
    else
    {
      num3 = this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].LeftIndentTwips + DeltaLeft;
      num4 = this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].RightIndentTwips + DeltaRight;
      num5 = this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].FirstIndentTwips + DeltaFirst;
    }
    for (int LineNo = StartLine; LineNo <= EndLine; ++LineNo)
    {
      if (this.LineSelected(LineNo))
      {
        if (!this.e.EditingParaStyle)
        {
          num3 = this.e.PfmtId[this.e.text[LineNo].pfmt].LeftIndentTwips + DeltaLeft;
          num4 = this.e.PfmtId[this.e.text[LineNo].pfmt].RightIndentTwips + DeltaRight;
          num5 = this.e.PfmtId[this.e.text[LineNo].pfmt].FirstIndentTwips + DeltaFirst;
        }
        if (num3 > 8640)
          num3 = 8640;
        if (num3 < 0)
        {
          num5 += num3;
          num3 = 0;
        }
        if (num4 > 8640)
          num4 = 8640;
        if (num4 < 0)
          num4 = 0;
        if (num5 > 8640)
          num5 = 8640;
        if (num3 + num5 < 0)
          num5 = -num3;
        if (this.e.SnapToGrid)
        {
          int step = (this.e.TerFlags & 2) == 0 ? 90 : 71;
          num3 = this.RoundInt(num3, step);
          num4 = this.RoundInt(num4, step);
          num5 = this.RoundInt(num5, step);
        }
        if (this.e.EditingParaStyle)
        {
          this.e.StyleId[this.e.CurSID].LeftIndentTwips = num3;
          this.e.StyleId[this.e.CurSID].RightIndentTwips = num4;
          this.e.StyleId[this.e.CurSID].FirstIndentTwips = num5;
          this.DrawRuler(false);
          return true;
        }
        if ((this.e.TerFlags2 & 67108864 /*0x04000000*/) != 0 && DeltaLeft != 0 && this.e.text[LineNo].fid > 0)
        {
          if (this.e.text[LineNo].fid != num1)
          {
            int fid = this.e.text[LineNo].fid;
            int x = this.e.ParaFrame[fid].x;
            this.e.ParaFrame[fid].x += DeltaLeft;
            if (x >= 0 && this.e.ParaFrame[fid].x < 0)
              this.e.ParaFrame[fid].x = 0;
            num1 = fid;
          }
        }
        else if ((this.e.TerFlags2 & 134217728 /*0x08000000*/) != 0 && DeltaLeft != 0 && this.e.text[LineNo].cid > 0)
        {
          int row = this.e.cell[this.e.text[LineNo].cid].row;
          if (row != num2)
          {
            int indent1 = this.e.TableRow[row].indent;
            this.e.TableRow[row].indent += DeltaLeft;
            if (indent1 >= 0 && this.e.TableRow[row].indent < 0)
              this.e.TableRow[row].indent = 0;
            int index = this.e.TableRow[row].FirstCell;
            int indent2 = this.e.TableRow[row].indent;
            for (; index > 0; index = this.e.cell[index].NextCell)
            {
              this.e.cell[index].x = indent2;
              indent2 += this.e.cell[index].width;
            }
            num2 = row;
          }
        }
        else
          this.e.text[LineNo].pfmt = this.NewParaId(this.e.text[LineNo].pfmt, num3, num4, num5, this.e.PfmtId[this.e.text[LineNo].pfmt].TabId, this.e.PfmtId[this.e.text[LineNo].pfmt].BltId, this.e.PfmtId[this.e.text[LineNo].pfmt].AuxId, this.e.PfmtId[this.e.text[LineNo].pfmt].Aux1Id, this.e.PfmtId[this.e.text[LineNo].pfmt].StyId, this.e.PfmtId[this.e.text[LineNo].pfmt].shading, this.e.PfmtId[this.e.text[LineNo].pfmt].pflags, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceBefore, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceAfter, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceBetween, this.e.PfmtId[this.e.text[LineNo].pfmt].LineSpacing, this.e.PfmtId[this.e.text[LineNo].pfmt].BkColor, this.e.PfmtId[this.e.text[LineNo].pfmt].BorderSpace, this.e.PfmtId[this.e.text[LineNo].pfmt].flow, this.e.PfmtId[this.e.text[LineNo].pfmt].flags);
      }
    }
    ++this.e.TerArg.modified;
    if (!this.e.EditingParaStyle && StartLine < this.e.RepageBeginLine)
      this.e.RepageBeginLine = StartLine;
    if (repaint)
      this.PaintTer();
    else
      this.WordWrap(StartLine, EndLine + 10 - StartLine);
    return true;
  }

  internal bool ParaLeftIndent(bool indent, bool repaint)
  {
    int num1 = 0;
    int num2 = 0;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.WordWrap)
      return false;
    int StartLine;
    int EndLine;
    this.GetParaRange(out StartLine, out EndLine);
    if (!this.e.EditingParaStyle)
      this.SaveUndo(StartLine, 0, EndLine, 0, 'P');
    for (int LineNo = StartLine; LineNo <= EndLine; ++LineNo)
    {
      if (this.LineSelected(LineNo))
      {
        int LeftIndentTwips;
        int firstIndentTwips;
        int index1;
        if (this.e.EditingParaStyle)
        {
          LeftIndentTwips = this.e.StyleId[this.e.CurSID].LeftIndentTwips;
          firstIndentTwips = this.e.StyleId[this.e.CurSID].FirstIndentTwips;
          index1 = this.e.StyleId[this.e.CurSID].TabId;
        }
        else
        {
          LeftIndentTwips = this.e.PfmtId[this.e.text[LineNo].pfmt].LeftIndentTwips;
          firstIndentTwips = this.e.PfmtId[this.e.text[LineNo].pfmt].FirstIndentTwips;
          index1 = this.e.PfmtId[this.e.text[LineNo].pfmt].TabId;
          if ((this.e.TerFlags2 & 67108864 /*0x04000000*/) != 0 && this.True(this.e.text[LineNo].fid))
          {
            LeftIndentTwips = this.e.ParaFrame[this.e.text[LineNo].fid].x;
            index1 = 0;
          }
          if ((this.e.TerFlags2 & 134217728 /*0x08000000*/) != 0 && this.True(this.e.text[LineNo].cid))
          {
            LeftIndentTwips = this.e.TableRow[this.e.cell[this.e.text[LineNo].cid].row].indent;
            index1 = 0;
          }
        }
        int count = this.e.TerTab[index1].count;
        if (indent)
        {
          int index2;
          for (index2 = 0; index2 < count; ++index2)
          {
            if (this.e.TerTab[index1].pos[index2] > LeftIndentTwips)
            {
              LeftIndentTwips = this.e.TerTab[index1].pos[index2];
              break;
            }
          }
          if (index2 == count)
            LeftIndentTwips = (LeftIndentTwips / this.e.DefTabWidth + 1) * this.e.DefTabWidth;
        }
        else
        {
          bool flag = false;
          if (count > 0 && LeftIndentTwips > this.e.TerTab[index1].pos[count - 1])
            flag = true;
          if (flag)
          {
            int num3 = LeftIndentTwips / this.e.DefTabWidth;
            if (LeftIndentTwips == num3 * this.e.DefTabWidth)
              --num3;
            LeftIndentTwips = num3 * this.e.DefTabWidth;
            if (count > 0 && LeftIndentTwips < this.e.TerTab[index1].pos[count - 1])
              LeftIndentTwips = this.e.TerTab[index1].pos[count - 1];
          }
          else
          {
            int index3;
            for (index3 = count - 1; index3 >= 0; --index3)
            {
              if (this.e.TerTab[index1].pos[index3] < LeftIndentTwips)
              {
                LeftIndentTwips = this.e.TerTab[index1].pos[index3];
                break;
              }
            }
            if (index3 < 0)
            {
              int num4 = LeftIndentTwips / this.e.DefTabWidth;
              LeftIndentTwips = num4 <= 0 ? 0 : (num4 - 1) * this.e.DefTabWidth;
              if (LeftIndentTwips < 0)
                LeftIndentTwips = 0;
            }
          }
        }
        if ((this.e.TerFlags2 & 67108864 /*0x04000000*/) != 0 && !this.e.EditingParaStyle && this.e.text[LineNo].fid > 0)
        {
          if (this.e.text[LineNo].fid != num1)
          {
            int fid = this.e.text[LineNo].fid;
            int x = this.e.ParaFrame[fid].x;
            this.e.ParaFrame[fid].x = LeftIndentTwips;
            if (x >= 0 && this.e.ParaFrame[fid].x < 0)
              this.e.ParaFrame[fid].x = 0;
            num1 = fid;
          }
        }
        else if ((this.e.TerFlags2 & 134217728 /*0x08000000*/) != 0 && !this.e.EditingParaStyle && this.e.text[LineNo].cid > 0)
        {
          int row = this.e.cell[this.e.text[LineNo].cid].row;
          if (row != num2)
          {
            int indent1 = this.e.TableRow[row].indent;
            this.e.TableRow[row].indent = LeftIndentTwips;
            if (indent1 >= 0 && this.e.TableRow[row].indent < 0)
              this.e.TableRow[row].indent = 0;
            int index4 = this.e.TableRow[row].FirstCell;
            int indent2 = this.e.TableRow[row].indent;
            for (; index4 > 0; index4 = this.e.cell[index4].NextCell)
            {
              this.e.cell[index4].x = indent2;
              indent2 += this.e.cell[index4].width;
            }
            num2 = row;
          }
        }
        else
        {
          if (LeftIndentTwips > 8640)
            LeftIndentTwips = 8640;
          if (LeftIndentTwips < 0)
            LeftIndentTwips = 0;
          if (LeftIndentTwips + firstIndentTwips < 0)
            LeftIndentTwips = -firstIndentTwips;
          if (this.e.EditingParaStyle)
          {
            this.e.StyleId[this.e.CurSID].LeftIndentTwips = LeftIndentTwips;
            this.DrawRuler(false);
            return true;
          }
          this.e.text[LineNo].pfmt = this.NewParaId(this.e.text[LineNo].pfmt, LeftIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].RightIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].FirstIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].TabId, this.e.PfmtId[this.e.text[LineNo].pfmt].BltId, this.e.PfmtId[this.e.text[LineNo].pfmt].AuxId, this.e.PfmtId[this.e.text[LineNo].pfmt].Aux1Id, this.e.PfmtId[this.e.text[LineNo].pfmt].StyId, this.e.PfmtId[this.e.text[LineNo].pfmt].shading, this.e.PfmtId[this.e.text[LineNo].pfmt].pflags, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceBefore, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceAfter, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceBetween, this.e.PfmtId[this.e.text[LineNo].pfmt].LineSpacing, this.e.PfmtId[this.e.text[LineNo].pfmt].BkColor, this.e.PfmtId[this.e.text[LineNo].pfmt].BorderSpace, this.e.PfmtId[this.e.text[LineNo].pfmt].flow, this.e.PfmtId[this.e.text[LineNo].pfmt].flags);
        }
      }
    }
    ++this.e.TerArg.modified;
    if (!this.e.EditingParaStyle && StartLine < this.e.RepageBeginLine)
      this.e.RepageBeginLine = StartLine;
    if (repaint)
      this.PaintTer();
    else
      this.WordWrap(StartLine, EndLine + 10 - StartLine);
    return true;
  }

  internal bool ParaNormal(bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.WordWrap)
      return false;
    if (this.e.EditingParaStyle)
    {
      this.e.StyleId[this.e.CurSID].LeftIndentTwips = 0;
      this.e.StyleId[this.e.CurSID].RightIndentTwips = 0;
      this.e.StyleId[this.e.CurSID].FirstIndentTwips = 0;
      this.e.StyleId[this.e.CurSID].ParaFlags = 0;
      this.e.StyleId[this.e.CurSID].pflags = 0;
      this.e.StyleId[this.e.CurSID].shading = 0;
      this.e.StyleId[this.e.CurSID].SpaceBefore = 0;
      this.e.StyleId[this.e.CurSID].SpaceAfter = 0;
      this.e.StyleId[this.e.CurSID].SpaceBetween = 0;
      this.e.StyleId[this.e.CurSID].LineSpacing = 0;
      this.e.StyleId[this.e.CurSID].TabId = 0;
      this.e.StyleId[this.e.CurSID].ParaBkColor = tc.CLR_WHITE;
      this.e.StyleId[this.e.CurSID].ParaBorderColor = tc.CLR_AUTO;
      this.DrawRuler(false);
      return true;
    }
    int StartLine;
    int EndLine;
    this.GetParaRange(out StartLine, out EndLine);
    this.SaveUndo(StartLine, 0, EndLine, 0, 'P');
    for (int LineNo = StartLine; LineNo <= EndLine; ++LineNo)
    {
      if (this.LineSelected(LineNo))
      {
        int flags = this.e.PfmtId[this.e.text[LineNo].pfmt].flags & -51216 | 1024 /*0x0400*/;
        this.e.text[LineNo].pfmt = this.NewParaId(this.e.text[LineNo].pfmt, 0, 0, 0, 0, 0, this.e.PfmtId[this.e.text[LineNo].pfmt].AuxId, this.e.PfmtId[this.e.text[LineNo].pfmt].Aux1Id, 0, 0, 0, 0, 0, 0, 0, tc.CLR_WHITE, 20, 0, flags);
      }
    }
    ++this.e.TerArg.modified;
    if (!this.e.EditingParaStyle && StartLine < this.e.RepageBeginLine)
      this.e.RepageBeginLine = StartLine;
    if (repaint)
      this.PaintTer();
    else
      this.WordWrap(StartLine, EndLine + 10 - StartLine);
    return true;
  }

  internal bool ParaRightIndent(bool indent, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.WordWrap)
      return false;
    int StartLine;
    int EndLine;
    this.GetParaRange(out StartLine, out EndLine);
    if (!this.e.EditingParaStyle)
      this.SaveUndo(StartLine, 0, EndLine, 0, 'P');
    for (int LineNo = StartLine; LineNo <= EndLine; ++LineNo)
    {
      if (this.LineSelected(LineNo))
      {
        int num = !this.e.EditingParaStyle ? this.e.PfmtId[this.e.text[LineNo].pfmt].RightIndentTwips : this.e.StyleId[this.e.CurSID].RightIndentTwips;
        int RightIndentTwips = !indent ? (num - 720 < 0 ? 0 : num - 720) : (num + 720 >= 8640 ? 8640 : num + 720);
        if (this.e.EditingParaStyle)
        {
          this.e.StyleId[this.e.CurSID].RightIndentTwips = RightIndentTwips;
          this.DrawRuler(false);
          return true;
        }
        this.e.text[LineNo].pfmt = this.NewParaId(this.e.text[LineNo].pfmt, this.e.PfmtId[this.e.text[LineNo].pfmt].LeftIndentTwips, RightIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].FirstIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].TabId, this.e.PfmtId[this.e.text[LineNo].pfmt].BltId, this.e.PfmtId[this.e.text[LineNo].pfmt].AuxId, this.e.PfmtId[this.e.text[LineNo].pfmt].Aux1Id, this.e.PfmtId[this.e.text[LineNo].pfmt].StyId, this.e.PfmtId[this.e.text[LineNo].pfmt].shading, this.e.PfmtId[this.e.text[LineNo].pfmt].pflags, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceBefore, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceAfter, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceBetween, this.e.PfmtId[this.e.text[LineNo].pfmt].LineSpacing, this.e.PfmtId[this.e.text[LineNo].pfmt].BkColor, this.e.PfmtId[this.e.text[LineNo].pfmt].BorderSpace, this.e.PfmtId[this.e.text[LineNo].pfmt].flow, this.e.PfmtId[this.e.text[LineNo].pfmt].flags);
      }
    }
    ++this.e.TerArg.modified;
    if (!this.e.EditingParaStyle && StartLine < this.e.RepageBeginLine)
      this.e.RepageBeginLine = StartLine;
    if (repaint)
      this.PaintTer();
    else
      this.WordWrap(StartLine, EndLine + 10 - StartLine);
    return true;
  }

  internal new bool SetAutoNumLgl(int LineNo, bool UseLogUnits)
  {
    string StrOut = "";
    int ToHdng = 0;
    int styId1 = this.e.PfmtId[this.e.text[LineNo].pfmt].StyId;
    if (this.True(styId1))
      ToHdng = this.GetHeadingNo(styId1);
    for (int index = LineNo - 1; index >= 0; --index)
    {
      if ((this.e.text[index].flags & 1073741824 /*0x40000000*/) != 0 && !this.False(this.e.text[index].tabw) && !this.False(this.e.text[index].tabw.pAutoNumLgl))
      {
        int FromHdng = 0;
        int styId2 = this.e.PfmtId[this.e.text[index].pfmt].StyId;
        if (this.True(styId2))
          FromHdng = this.GetHeadingNo(styId2);
        if (ToHdng <= 0 || FromHdng != 0 && FromHdng <= ToHdng)
        {
          if (FromHdng == ToHdng)
          {
            string prefix;
            int pNbr;
            this.CrackAutoNumLgl(this.e.text[index].tabw.pAutoNumLgl, out prefix, out pNbr);
            this.MakeAutoNumLgl(out StrOut, prefix, pNbr + 1, FromHdng, ToHdng);
            break;
          }
          this.MakeAutoNumLgl(out StrOut, this.e.text[index].tabw.pAutoNumLgl, 1, FromHdng, ToHdng);
          break;
        }
      }
    }
    if (StrOut.Length == 0)
      this.MakeAutoNumLgl(out StrOut, "", 1, 0, ToHdng);
    this.e.text[LineNo].tabw.pAutoNumLgl = StrOut;
    return true;
  }

  internal new bool SetCharStyleId(
    ref tc.StrFont font,
    tc.StrStyleId PrevStyle,
    tc.StrStyleId NewStyle,
    bool force)
  {
    if ((force || this.strcmpi(PrevStyle.TypeFace, font.TypeFace) == 0 || PrevStyle.TypeFace.Length == 0 && this.strcmpi(this.e.TerArg.FontTypeFace, font.TypeFace) == 0 || this.strcmpi(font.TypeFace, this.e.TerArg.FontTypeFace) == 0) && NewStyle.TypeFace != null && NewStyle.TypeFace.Length > 0)
      font.TypeFace = NewStyle.TypeFace;
    if ((force || PrevStyle.TwipsSize == font.TwipsSize || PrevStyle.TwipsSize == 0 && this.e.TerArg.PointSize * 20 == font.TwipsSize || font.TwipsSize == this.e.TerArg.PointSize * 20) && NewStyle.TwipsSize > 0)
      font.TwipsSize = NewStyle.TwipsSize;
    if (force)
    {
      font.style = tc.ResetUintFlag(ref font.style, PrevStyle.style);
      font.style = tc.ResetUintFlag(ref font.style, 196927);
      font.style |= NewStyle.style;
    }
    else
    {
      int num1 = 196927;
      int num2 = ~num1;
      int num3 = font.style & num1;
      int num4 = PrevStyle.style & num1;
      int num5 = NewStyle.style & num1;
      int num6 = ~(num3 ^ num4) & num1;
      int num7 = num3 & ~num6 | num5 & num6 | font.style & num2;
      font.style = num7;
    }
    if (force)
    {
      font.TextColor = this.NewColor(NewStyle.TextColor);
      font.TextBkColor = this.NewColor(NewStyle.TextBkColor);
      font.UlineColor = this.NewColor(NewStyle.UlineColor);
      if (NewStyle.expand != 0)
        font.expand = NewStyle.expand;
      if (NewStyle.offset != 0)
        font.offset = NewStyle.offset;
    }
    else
    {
      if (this.IsSameColor(PrevStyle.TextColor, font.TextColor) || this.IsSameColor(PrevStyle.TextColor, 0) && this.IsSameColor(font.TextColor, 0) || this.IsSameColor(font.TextColor, 0) || this.IsSameColor(font.TextColor, tc.CLR_AUTO))
        font.TextColor = this.NewColor(NewStyle.TextColor);
      if (PrevStyle.TextBkColor == font.TextBkColor || PrevStyle.TextBkColor == tc.CLR_WHITE && font.TextBkColor == tc.CLR_WHITE || font.TextBkColor == tc.CLR_WHITE)
        font.TextBkColor = this.NewColor(NewStyle.TextBkColor);
      if (this.IsSameColor(PrevStyle.UlineColor, font.UlineColor) || this.IsSameColor(PrevStyle.UlineColor, 0) && this.IsSameColor(font.UlineColor, 0) || this.IsSameColor(font.UlineColor, 0) || this.IsSameColor(font.UlineColor, tc.CLR_AUTO))
        font.UlineColor = this.NewColor(NewStyle.UlineColor);
      if (PrevStyle.expand == font.expand || font.expand == 0)
        font.expand = NewStyle.expand;
      if (PrevStyle.offset == font.offset || font.offset == 0)
        font.offset = NewStyle.offset;
    }
    return true;
  }

  internal new bool SetDlgListLevel(
    bool ListItem,
    ComboBox box,
    ComboBox ListOrBox,
    ComboBox LevelBox)
  {
    bool flag = false;
    int levelCount;
    if (ListItem)
    {
      int selectedIndex = box.SelectedIndex;
      if (box.SelectedItem == null)
        return false;
      levelCount = this.e.list[((tc.ClsBox) box.SelectedItem).value].LevelCount;
      flag = true;
    }
    else
    {
      int selectedIndex = ListOrBox.SelectedIndex;
      if (ListOrBox.SelectedItem == null)
        return false;
      levelCount = this.e.ListOr[((tc.ClsBox) ListOrBox.SelectedItem).value].LevelCount;
    }
    box.Enabled = flag;
    ListOrBox.Enabled = !flag;
    if (LevelBox.Items.Count != levelCount)
    {
      LevelBox.Items.Clear();
      for (int ArgValue = 0; ArgValue < levelCount; ++ArgValue)
      {
        string ArgItem = (ArgValue + 1).ToString();
        LevelBox.Items.Add((object) new tc.ClsBox(ArgItem, ArgValue));
      }
      LevelBox.SelectedIndex = 0;
    }
    return true;
  }

  internal new bool SetDlgListLevelProp(
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
    tc.StrListLevel pLevel;
    if (!this.GetDlgListLevelPtr(ListItem, box, ListOrBox, LevelBox, out pLevel, out tc.SkipInt, out tc.SkipInt))
      return false;
    Restart.Checked = (pLevel.flags & 1) != 0;
    Legal.Checked = (pLevel.flags & 8) != 0;
    Reformat.Checked = (pLevel.flags & 16 /*0x10*/) != 0;
    NoReset.Checked = (pLevel.flags & 32 /*0x20*/) != 0;
    StartAt.Text = pLevel.start.ToString();
    this.DlgListNumType(NumTypeBox, pLevel.NumType, true);
    this.DlgListCharAft(CharAftBox, pLevel.CharAft, true);
    string StrText;
    this.DecodeListText(pLevel.text, out StrText);
    NbrText.Text = StrText;
    this.e.DlgInt1 = pLevel.FontId;
    return true;
  }

  internal new bool SetDlgListParaLevel(ListBox box, ComboBox LevelBox, int CurLevel)
  {
    int selectedIndex = box.SelectedIndex;
    if (box.SelectedItem == null)
      return false;
    int index1 = ((tc.ClsBox) box.SelectedItem).value;
    int levelCount = this.e.ListOr[index1].LevelCount;
    if (levelCount == 0)
      levelCount = this.e.list[this.e.ListOr[index1].ListIdx].LevelCount;
    if (LevelBox.Items.Count != levelCount)
    {
      LevelBox.Items.Clear();
      for (int ArgValue = 0; ArgValue < levelCount; ++ArgValue)
      {
        string ArgItem = (ArgValue + 1).ToString();
        LevelBox.Items.Add((object) new tc.ClsBox(ArgItem, ArgValue));
      }
      for (int index2 = 0; index2 < levelCount; ++index2)
      {
        if (CurLevel == ((tc.ClsBox) LevelBox.Items[index2]).value)
        {
          LevelBox.SelectedIndex = index2;
          break;
        }
      }
    }
    return true;
  }

  internal new bool SetListnum(int LineNo, bool UseLogUnits)
  {
    int index = 0;
    ushort[] numArray = this.OpenCfmt(LineNo);
    char[] txt = this.e.text[LineNo].txt;
    int FieldNbr = -1;
    int FieldFont = 0;
    int len = this.e.text[LineNo].len;
    for (int col = 0; col < len; ++col)
    {
      char ch = txt[col];
      int num = FieldFont;
      FieldFont = (int) numArray[col];
      if (col == 0 || num != FieldFont || ch == '\u0005')
      {
        if (ch == '\u0005')
        {
          int AuxInt;
          if (this.GetTag(LineNo, col, 5, out tc.SkipStr, out tc.SkipStr, out AuxInt, out tc.SkipObject) == 0 || AuxInt < 0 || AuxInt >= this.e.TotalPfmts || (index = this.e.PfmtId[AuxInt].BltId) == 0 || this.e.TerBlt[index].ls == 0)
            continue;
        }
        else if (this.e.TerFont[FieldFont].FieldId != 11)
          continue;
        ++FieldNbr;
        if (this.False(this.e.text[LineNo].tabw))
          this.AllocTabw(LineNo);
        if (this.e.text[LineNo].tabw.ListnumCount == 0)
          this.e.text[LineNo].tabw.pListnum = new tc.StrListnum[FieldNbr + 1];
        else
          this.e.text[LineNo].tabw.pListnum = this.ReAlloc(this.e.text[LineNo].tabw.pListnum, FieldNbr + 1);
        this.e.text[LineNo].tabw.ListnumCount = FieldNbr;
        int pListOrId;
        int pLevel;
        if (ch == '\u0005')
        {
          pListOrId = this.e.TerBlt[index].ls;
          pLevel = this.e.TerBlt[index].lvl;
        }
        else
          this.GetFieldList(FieldFont, LineNo, out pListOrId, out pLevel);
        this.e.text[LineNo].tabw.ListnumCount = FieldNbr + 1;
        tc.StrListnum[] pListnum1 = this.e.text[LineNo].tabw.pListnum;
        pListnum1[FieldNbr].ls = pListOrId;
        pListnum1[FieldNbr].lvl = pLevel;
        pListnum1[FieldNbr].IsHPARA = ch == '\u0005';
        string ListText;
        int pListNbr;
        this.GetListText(this.e.text[LineNo].pfmt, LineNo, out ListText, out tc.SkipInt, out pListNbr, out tc.SkipInt, 0, FieldNbr, UseLogUnits);
        tc.StrListnum[] pListnum2 = this.e.text[LineNo].tabw.pListnum;
        pListnum2[FieldNbr].text = ListText;
        pListnum2[FieldNbr].ListNbr = pListNbr;
      }
    }
    return true;
  }

  internal new bool SetNextStyle()
  {
    if (this.e.text[this.e.CurLine].len == 1)
    {
      int styId = this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].StyId;
      if (this.e.StyleId[styId].next == styId)
        return true;
      this.e.TerSelectParaStyle(this.e.StyleId[styId].next, true);
    }
    return true;
  }

  internal new bool SetParaBorder(int LineNo)
  {
    int flags = this.e.PfmtId[this.e.text[LineNo].pfmt].flags;
    if ((this.e.text[LineNo].flags & 512 /*0x0200*/) != 0)
      this.e.TextBorder |= 16 /*0x10*/;
    if ((this.e.text[LineNo].flags & 1024 /*0x0400*/) != 0)
      this.e.TextBorder |= 32 /*0x20*/;
    if ((this.e.text[LineNo].flags2 & 16 /*0x10*/) != 0)
      this.e.TextBorder |= 65536 /*0x010000*/;
    if ((flags & 64 /*0x40*/) != 0)
      this.e.TextBorder |= 64 /*0x40*/;
    if ((flags & 128 /*0x80*/) != 0)
      this.e.TextBorder |= 128 /*0x80*/;
    if ((flags & 256 /*0x0100*/) != 0)
      this.e.TextBorder |= 256 /*0x0100*/;
    if ((flags & 512 /*0x0200*/) != 0)
      this.e.TextBorder |= 512 /*0x0200*/;
    if (this.True(this.e.text[LineNo].tabw) && (this.e.text[LineNo].tabw.type & 4) != 0)
      this.e.TextBorder = 0;
    if (this.IsHtmlRule(this.e.text[LineNo].pfmt))
      this.e.TextBorder = tc.ResetFlag(this.e.TextBorder, 32 /*0x20*/);
    return true;
  }

  internal new int SetParaParam(int CurPara, int type, int val)
  {
    return this.NewParaId(CurPara, type != 0 ? this.e.PfmtId[CurPara].LeftIndentTwips : val, type != 1 ? this.e.PfmtId[CurPara].RightIndentTwips : val, type != 2 ? this.e.PfmtId[CurPara].FirstIndentTwips : val, type != 3 ? this.e.PfmtId[CurPara].TabId : val, type != 4 ? this.e.PfmtId[CurPara].BltId : val, type != 5 ? this.e.PfmtId[CurPara].AuxId : val, type != 6 ? this.e.PfmtId[CurPara].Aux1Id : val, type != 7 ? this.e.PfmtId[CurPara].StyId : val, type != 8 ? this.e.PfmtId[CurPara].shading : val, type != 9 ? this.e.PfmtId[CurPara].pflags : val, type != 10 ? this.e.PfmtId[CurPara].SpaceBefore : val, type != 11 ? this.e.PfmtId[CurPara].SpaceAfter : val, type != 12 ? this.e.PfmtId[CurPara].SpaceBetween : val, type != 13 ? this.e.PfmtId[CurPara].LineSpacing : val, type != 14 ? this.e.PfmtId[CurPara].BkColor : this.ToColor(val), type != 15 ? this.e.PfmtId[CurPara].BorderSpace : val, type != 16 /*0x10*/ ? this.e.PfmtId[CurPara].flow : val, type != 17 ? this.e.PfmtId[CurPara].flags : val);
  }

  internal bool SetParaSpaceDlg(terdlg_para_space dlg, int idx, int SpaceBetween, int LineSpacing)
  {
    dlg.box.SelectedIndex = idx;
    switch (idx)
    {
      case 0:
      case 1:
      case 2:
        dlg.ParaSpace.Enabled = false;
        dlg.ParaSpace.Text = "";
        dlg.ParaSpaceLbl.Text = "";
        break;
      case 3:
      case 4:
        if (SpaceBetween < 0)
          SpaceBetween = -SpaceBetween;
        if (SpaceBetween == 0)
          SpaceBetween = 12;
        dlg.ParaSpace.Text = SpaceBetween.ToString();
        dlg.ParaSpace.Enabled = true;
        dlg.ParaSpaceLbl.Text = this.e.MsgString[187];
        break;
      case 5:
        double num = (double) ((LineSpacing + 100) / 100);
        dlg.ParaSpace.Text = $"{num:f2}";
        dlg.ParaSpace.Enabled = true;
        dlg.ParaSpaceLbl.Text = this.e.MsgString[186];
        break;
    }
    return true;
  }

  internal new bool SetParaStyleId(
    ref tc.StrPfmt para,
    tc.StrStyleId PrevStyle,
    tc.StrStyleId NewStyle,
    bool force)
  {
    if (force || PrevStyle.LeftIndentTwips == para.LeftIndentTwips || PrevStyle.LeftIndentTwips == 0 && this.e.PfmtId[0].LeftIndentTwips == para.LeftIndentTwips)
      para.LeftIndentTwips = NewStyle.LeftIndentTwips;
    para.LeftIndent = this.MulDiv(para.LeftIndentTwips, this.e.ScrResX, 1440);
    if (force || PrevStyle.RightIndentTwips == para.RightIndentTwips || PrevStyle.RightIndentTwips == 0 && this.e.PfmtId[0].RightIndentTwips == para.RightIndentTwips)
      para.RightIndentTwips = NewStyle.RightIndentTwips;
    para.RightIndent = this.MulDiv(para.RightIndentTwips, this.e.ScrResX, 1440);
    if (force || PrevStyle.FirstIndentTwips == para.FirstIndentTwips || PrevStyle.FirstIndentTwips == 0 && this.e.PfmtId[0].FirstIndentTwips == para.FirstIndentTwips)
      para.FirstIndentTwips = NewStyle.FirstIndentTwips;
    para.FirstIndent = this.MulDiv(para.FirstIndentTwips, this.e.ScrResX, 1440);
    if (force)
    {
      para.flags &= -53248;
      para.flags |= NewStyle.ParaFlags;
      para.pflags &= -49;
      para.pflags |= NewStyle.pflags;
    }
    else
    {
      int num1 = 3075;
      int flags = para.flags;
      int paraFlags = PrevStyle.ParaFlags;
      int num2 = ~num1;
      int num3 = 2051;
      if ((flags & num3) == 0)
        flags |= 1024 /*0x0400*/;
      if ((paraFlags & num3) == 0)
        paraFlags |= 1024 /*0x0400*/;
      if ((flags & num1) == (paraFlags & num1))
      {
        para.flags &= ~num1;
        para.flags |= NewStyle.ParaFlags & num1;
      }
      if ((flags & 4) == (paraFlags & 4))
      {
        para.flags &= -5;
        para.flags |= NewStyle.ParaFlags & 4;
      }
      int num4 = num2 & -5;
      if ((flags & 8) == (paraFlags & 8))
      {
        para.flags &= -9;
        para.flags |= NewStyle.ParaFlags & 8;
      }
      para.flags &= ~(num4 & -9 & -12289);
      para.flags |= NewStyle.ParaFlags & num4;
      int num5 = ~(para.pflags ^ PrevStyle.pflags);
      para.pflags = NewStyle.pflags & num5 | para.pflags & ~num5;
    }
    if (force || PrevStyle.shading == para.shading || PrevStyle.shading == 0 && this.e.PfmtId[0].shading == para.shading)
      para.shading = NewStyle.shading;
    if (force || PrevStyle.ParaBkColor == para.BkColor || PrevStyle.ParaBkColor == tc.CLR_WHITE && this.e.PfmtId[0].BkColor == para.BkColor)
      para.BkColor = NewStyle.ParaBkColor;
    if (force || PrevStyle.ParaBorderColor == para.BorderColor || PrevStyle.ParaBorderColor == tc.CLR_AUTO && this.e.PfmtId[0].BorderColor == para.BorderColor)
      para.BorderColor = NewStyle.ParaBorderColor;
    if (force || PrevStyle.SpaceBefore == para.SpaceBefore || PrevStyle.SpaceBefore == 0 && this.e.PfmtId[0].SpaceBefore == para.SpaceBefore)
      para.SpaceBefore = NewStyle.SpaceBefore;
    if (force || PrevStyle.SpaceAfter == para.SpaceAfter || PrevStyle.SpaceAfter == 0 && this.e.PfmtId[0].SpaceAfter == para.SpaceAfter)
      para.SpaceAfter = NewStyle.SpaceAfter;
    if (force || PrevStyle.SpaceBetween == para.SpaceBetween || PrevStyle.SpaceBetween == 0 && this.e.PfmtId[0].SpaceBetween == para.SpaceBetween)
      para.SpaceBetween = NewStyle.SpaceBetween;
    if (force || PrevStyle.LineSpacing == para.LineSpacing || PrevStyle.LineSpacing == 0 && this.e.PfmtId[0].LineSpacing == para.LineSpacing)
      para.LineSpacing = NewStyle.LineSpacing;
    if (force || PrevStyle.TabId == para.TabId || PrevStyle.TabId == 0 && this.e.PfmtId[0].TabId == para.TabId)
      para.TabId = NewStyle.TabId;
    if (force || PrevStyle.BltId == para.BltId || PrevStyle.BltId == 0 && this.e.PfmtId[0].BltId == para.BltId)
      para.BltId = NewStyle.BltId;
    return true;
  }

  internal new int SetParaTextFlow(int ParaId, int flow)
  {
    return this.NewParaId(ParaId, this.e.PfmtId[ParaId].LeftIndentTwips, this.e.PfmtId[ParaId].RightIndentTwips, this.e.PfmtId[ParaId].FirstIndentTwips, this.e.PfmtId[ParaId].TabId, this.e.PfmtId[ParaId].BltId, this.e.PfmtId[ParaId].AuxId, this.e.PfmtId[ParaId].Aux1Id, this.e.PfmtId[ParaId].StyId, this.e.PfmtId[ParaId].shading, this.e.PfmtId[ParaId].pflags, this.e.PfmtId[ParaId].SpaceBefore, this.e.PfmtId[ParaId].SpaceAfter, this.e.PfmtId[ParaId].SpaceBetween, this.e.PfmtId[ParaId].LineSpacing, this.e.PfmtId[ParaId].BkColor, this.e.PfmtId[ParaId].BorderSpace, flow, this.e.PfmtId[ParaId].flags);
  }

  internal bool SetTab(int type, int pos, bool repaint)
  {
    return this.TerSetTab(type, pos, (byte) 0, repaint);
  }

  internal bool SetTerParaFmt(int FmtType, bool OnOff, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.WordWrap)
      return false;
    int StartLine;
    int EndLine;
    this.GetParaRange(out StartLine, out EndLine);
    if (!this.e.EditingParaStyle)
      this.SaveUndo(StartLine, 0, EndLine, 0, 'P');
    for (int LineNo = StartLine; LineNo <= EndLine; ++LineNo)
    {
      if (this.LineSelected(LineNo))
      {
        int LeftIndentTwips;
        int RightIndentTwips;
        int firstIndentTwips;
        int SpaceBefore;
        int SpaceAfter;
        int SpaceBetween;
        int LineSpacing;
        int num1;
        int num2;
        if (this.e.EditingParaStyle)
        {
          LeftIndentTwips = this.e.StyleId[this.e.CurSID].LeftIndentTwips;
          RightIndentTwips = this.e.StyleId[this.e.CurSID].RightIndentTwips;
          firstIndentTwips = this.e.StyleId[this.e.CurSID].FirstIndentTwips;
          SpaceBefore = this.e.StyleId[this.e.CurSID].SpaceBefore;
          SpaceAfter = this.e.StyleId[this.e.CurSID].SpaceAfter;
          SpaceBetween = this.e.StyleId[this.e.CurSID].SpaceBetween;
          LineSpacing = this.e.StyleId[this.e.CurSID].LineSpacing;
          num1 = 0;
          num2 = this.e.StyleId[this.e.CurSID].ParaFlags;
        }
        else
        {
          LeftIndentTwips = this.e.PfmtId[this.e.text[LineNo].pfmt].LeftIndentTwips;
          RightIndentTwips = this.e.PfmtId[this.e.text[LineNo].pfmt].RightIndentTwips;
          firstIndentTwips = this.e.PfmtId[this.e.text[LineNo].pfmt].FirstIndentTwips;
          SpaceBefore = this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceBefore;
          SpaceAfter = this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceAfter;
          SpaceBetween = this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceBetween;
          LineSpacing = this.e.PfmtId[this.e.text[LineNo].pfmt].LineSpacing;
          num1 = this.e.text[LineNo].fid;
          num2 = this.e.PfmtId[this.e.text[LineNo].pfmt].flags;
        }
        int flags = !OnOff ? num2 & ~FmtType : num2 | FmtType;
        if ((FmtType & 1024 /*0x0400*/) != 0)
          flags &= -2052;
        if ((FmtType & 1) != 0)
          flags &= -3075;
        if ((FmtType & 2) != 0)
          flags &= -3074;
        if ((FmtType & 2048 /*0x0800*/) != 0)
          flags &= -1028;
        if ((FmtType & 4096 /*0x1000*/) != 0)
          flags &= -8193;
        if ((FmtType & 8192 /*0x2000*/) != 0)
          flags &= -4097;
        if (this.e.CurSID < 0 && this.True(this.e.text[LineNo].tabw) && (this.e.text[LineNo].tabw.type & 78) != 0)
          flags &= -12289;
        if ((FmtType & 64 /*0x40*/) != 0)
        {
          int num3 = LeftIndentTwips;
          if (firstIndentTwips < 0)
            num3 += firstIndentTwips;
          if (OnOff && num3 < 60)
            LeftIndentTwips += 60 - num3;
          else if (!OnOff && LeftIndentTwips == 60)
            LeftIndentTwips -= 60;
          if (LeftIndentTwips < 0)
            LeftIndentTwips = 0;
        }
        if ((FmtType & 128 /*0x80*/) != 0)
        {
          if (OnOff && RightIndentTwips < 60)
            RightIndentTwips = 60;
          else if (!OnOff && RightIndentTwips == 60)
            RightIndentTwips -= 60;
          if (RightIndentTwips < 0)
            RightIndentTwips = 0;
        }
        if ((flags & 4) != 0)
          SpaceBetween = LineSpacing = 0;
        if ((FmtType & 16 /*0x10*/) != 0 && num1 == 0)
        {
          if (OnOff && SpaceBefore < 20)
            SpaceBefore = 20;
          else if (!OnOff)
            SpaceBefore -= 20;
          if (SpaceBefore < 0)
            SpaceBefore = 0;
        }
        if ((FmtType & 32 /*0x20*/) != 0 && num1 == 0)
        {
          if (OnOff && SpaceAfter < 20)
            SpaceAfter = 20;
          else if (!OnOff && SpaceAfter >= 20)
            SpaceAfter -= 20;
          if (SpaceAfter < 0)
            SpaceAfter = 0;
        }
        if (this.e.EditingParaStyle)
        {
          this.e.StyleId[this.e.CurSID].LeftIndentTwips = LeftIndentTwips;
          this.e.StyleId[this.e.CurSID].RightIndentTwips = RightIndentTwips;
          this.e.StyleId[this.e.CurSID].FirstIndentTwips = firstIndentTwips;
          this.e.StyleId[this.e.CurSID].SpaceBefore = SpaceBefore;
          this.e.StyleId[this.e.CurSID].SpaceAfter = SpaceAfter;
          this.e.StyleId[this.e.CurSID].SpaceBetween = SpaceBetween;
          this.e.StyleId[this.e.CurSID].LineSpacing = LineSpacing;
          this.e.StyleId[this.e.CurSID].ParaFlags = flags;
          return true;
        }
        this.e.text[LineNo].pfmt = this.NewParaId(this.e.text[LineNo].pfmt, LeftIndentTwips, RightIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].FirstIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].TabId, this.e.PfmtId[this.e.text[LineNo].pfmt].BltId, this.e.PfmtId[this.e.text[LineNo].pfmt].AuxId, this.e.PfmtId[this.e.text[LineNo].pfmt].Aux1Id, this.e.PfmtId[this.e.text[LineNo].pfmt].StyId, this.e.PfmtId[this.e.text[LineNo].pfmt].shading, this.e.PfmtId[this.e.text[LineNo].pfmt].pflags, SpaceBefore, SpaceAfter, SpaceBetween, LineSpacing, this.e.PfmtId[this.e.text[LineNo].pfmt].BkColor, this.e.PfmtId[this.e.text[LineNo].pfmt].BorderSpace, this.e.PfmtId[this.e.text[LineNo].pfmt].flow, flags);
      }
    }
    ++this.e.TerArg.modified;
    if (this.e.TerArg.PrintView && (FmtType & 12288 /*0x3000*/) != 0 && this.e.TotalLines < 5000)
      this.Repaginate(false, false, 0, true);
    else
      this.e.RepageBeginLine = StartLine;
    if (repaint)
    {
      if (!OnOff && (FmtType & 66544) != 0)
        this.e.PaintFlag = 6;
      this.PaintTer();
    }
    else
      this.WordWrap(StartLine, EndLine + 10 - StartLine);
    return true;
  }

  internal bool TerBulletToText(bool all, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.WordWrap || !all && this.e.HilightType == 0)
      return false;
    int num1 = 0;
    int num2 = this.e.TotalLines - 1;
    if (!all)
    {
      this.NormalizeBlock();
      num1 = this.e.HilightBegRow;
      num2 = this.e.HilightEndRow;
    }
    for (int index1 = num2; index1 >= num1; --index1)
    {
      if ((this.e.text[index1].flags & 4) != 0)
      {
        int pfmt = this.e.text[index1].pfmt;
        if (this.e.PfmtId[pfmt].BltId != 0 || (this.e.PfmtId[pfmt].flags & 8) != 0)
        {
          this.DrawBullet(this.e.TerGr, index1, pfmt, 0, 0, 0, false);
          string dlgText1 = this.e.DlgText1;
          int dlgInt1 = this.e.DlgInt1;
          if (dlgText1.Length != 0)
          {
            int bltId = this.e.PfmtId[pfmt].BltId;
            int ls = this.e.TerBlt[bltId].ls;
            if (ls == 0)
            {
              dlgText1 += "\t";
            }
            else
            {
              tc.StrListLevel pLevel;
              if (this.GetListLevelPtr(ls, this.e.TerBlt[bltId].lvl, out pLevel) && pLevel.CharAft == 0)
                dlgText1 += "\t";
            }
            this.e.SetTerCursorPos(index1, 0, false);
            this.e.TerInsertText(dlgText1, dlgInt1, pfmt, false);
            int flags = tc.ResetUintFlag(ref this.e.PfmtId[pfmt].flags, 8);
            int num3 = this.NewParaId(pfmt, this.e.PfmtId[pfmt].LeftIndentTwips, this.e.PfmtId[pfmt].RightIndentTwips, this.e.PfmtId[pfmt].FirstIndentTwips, this.e.PfmtId[pfmt].TabId, 0, this.e.PfmtId[pfmt].AuxId, this.e.PfmtId[pfmt].Aux1Id, this.e.PfmtId[pfmt].StyId, this.e.PfmtId[pfmt].shading, this.e.PfmtId[pfmt].pflags, this.e.PfmtId[pfmt].SpaceBefore, this.e.PfmtId[pfmt].SpaceAfter, this.e.PfmtId[pfmt].SpaceBetween, this.e.PfmtId[pfmt].LineSpacing, this.e.PfmtId[pfmt].BkColor, this.e.PfmtId[pfmt].BorderSpace, this.e.PfmtId[pfmt].flow, flags);
            for (int index2 = index1; index2 < this.e.TotalLines; ++index2)
            {
              if (this.e.text[index2].pfmt == pfmt)
                this.e.text[index2].pfmt = num3;
              if ((this.e.text[index2].flags & 1) != 0)
                break;
            }
          }
        }
      }
    }
    if (repaint)
      this.PaintTer();
    return true;
  }

  internal bool TerCancelEditStyle()
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (this.e.CurSID == -1)
      return false;
    this.e.StyleId[this.e.CurSID] = this.e.PrevStyleId;
    this.e.CurSID = -1;
    this.e.EditingParaStyle = false;
    this.PaintTer();
    return true;
  }

  internal int TerCreateBulletId(bool IsBullet, int start, int level, int type)
  {
    return this.TerCreateBulletId2(IsBullet, start, level, type, (string) null, (string) null);
  }

  internal int TerCreateBulletId2(
    bool IsBullet,
    int start,
    int level,
    int type,
    string TextBef,
    string TextAft)
  {
    return this.TerCreateBulletId3(IsBullet, start, level, type, TextBef, TextAft, 0);
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
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.WordWrap)
      return 0;
    if (TextBef == null)
      TextBef = "";
    if (TextAft == null)
      TextAft = "";
    tc.StrBlt BltRec = this.e.TerBlt[0];
    if (IsBullet)
    {
      BltRec.IsBullet = true;
      BltRec.level = 11;
      switch (type)
      {
        case 1:
          BltRec.BulletChar = '¨';
          BltRec.font = 1;
          break;
        case 2:
          BltRec.BulletChar = '§';
          BltRec.font = 2;
          break;
        case 3:
          BltRec.BulletChar = 'q';
          BltRec.font = 2;
          break;
        case 4:
          BltRec.BulletChar = 'v';
          BltRec.font = 2;
          break;
        case 5:
          BltRec.BulletChar = 'Ø';
          BltRec.font = 2;
          break;
        case 6:
          BltRec.BulletChar = 'ü';
          BltRec.font = 2;
          break;
        default:
          BltRec.BulletChar = '·';
          BltRec.font = 1;
          break;
      }
    }
    else
    {
      BltRec.IsBullet = false;
      BltRec.start = start;
      BltRec.level = level;
      BltRec.NumberType = type;
      BltRec.BefText = TextBef;
      BltRec.AftChar = TextAft.Length <= 0 ? char.MinValue : TextAft[0];
    }
    BltRec.flags = flags;
    return this.NewBltId(0, BltRec);
  }

  internal int TerCreateListBulletId(int CurListOr, int level)
  {
    return this.TerCreateListBulletId2(CurListOr, level, -1);
  }

  internal int TerCreateListBulletId2(int CurListOr, int level, int BltType)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.WordWrap || CurListOr < 0 || CurListOr >= this.e.TotalListOr)
      return 0;
    int levelCount = this.e.ListOr[CurListOr].LevelCount;
    if (levelCount == 0)
      levelCount = this.e.list[this.e.ListOr[CurListOr].ListIdx].LevelCount;
    if (level < 0 || level >= levelCount)
      return 0;
    tc.StrBlt BltRec = this.e.TerBlt[0] with
    {
      IsBullet = false,
      ls = CurListOr,
      lvl = level
    };
    switch (BltType)
    {
      case -1:
        return this.NewBltId(0, BltRec);
      case 1:
        BltRec.BulletChar = '¨';
        BltRec.font = 1;
        goto case -1;
      case 2:
        BltRec.BulletChar = '§';
        BltRec.font = 2;
        goto case -1;
      case 3:
        BltRec.BulletChar = 'q';
        BltRec.font = 2;
        goto case -1;
      case 4:
        BltRec.BulletChar = 'v';
        BltRec.font = 2;
        goto case -1;
      case 5:
        BltRec.BulletChar = 'Ø';
        BltRec.font = 2;
        goto case -1;
      case 6:
        BltRec.BulletChar = 'ü';
        BltRec.font = 2;
        goto case -1;
      default:
        BltRec.BulletChar = '·';
        BltRec.font = 1;
        goto case -1;
    }
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
    return this.TerCreateParaIdEx(ReuseId, shared, LeftIndentTwips, RightIndentTwips, FirstIndentTwips, TabId, StyId, AuxId, shading, pflags, SpaceBefore, SpaceAfter, SpaceBetween, flags, 0, tc.CLR_WHITE);
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
    int old = 0;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (ReuseId >= this.e.MaxPfmts)
      return -1;
    if (ReuseId >= 0)
      shared = false;
    if (ReuseId >= this.e.TotalPfmts)
    {
      for (int totalPfmts = this.e.TotalPfmts; totalPfmts <= ReuseId; ++totalPfmts)
        this.e.PfmtId[totalPfmts] = new tc.StrPfmt();
      this.e.TotalPfmts = ReuseId + 1;
    }
    if (!shared)
      this.e.MatchIds = false;
    if (ReuseId >= 0)
      old = ReuseId;
    int paraIdEx = this.NewParaId(old, LeftIndentTwips, RightIndentTwips, FirstIndentTwips, TabId, BltId, AuxId, this.e.NextParaAux1Id, StyId, shading, pflags, SpaceBefore, SpaceAfter, SpaceBetween, 0, BkColor, 20, 0, flags);
    this.e.NextParaAux1Id = 0;
    if (ReuseId >= 0 && paraIdEx != ReuseId)
    {
      this.e.PfmtId[ReuseId] = this.e.PfmtId[paraIdEx];
      paraIdEx = ReuseId;
    }
    return paraIdEx;
  }

  internal int TerCreateTabId(tc.StrTab pTabRec)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    return !this.e.TerArg.WordWrap ? -1 : this.NewTabId(0, pTabRec);
  }

  internal bool TerDeleteStyle(int CurStyle, string name)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (CurStyle < 0)
    {
      if (name == null || name.Length == 0)
        return false;
      int index = 2;
      while (index < this.e.TotalSID && this.strcmpi(name, this.e.StyleId[index].name) != 0)
        ++index;
      if (index >= this.e.TotalSID)
        return false;
      CurStyle = index;
    }
    if (CurStyle < 2)
      return false;
    if (this.e.StyleId[CurStyle].type == 2)
    {
      for (int index = 0; index < this.e.TotalPfmts; ++index)
      {
        if (this.e.PfmtId[index].StyId == CurStyle)
          this.e.PfmtId[index].StyId = 0;
      }
    }
    for (int index = 0; index < this.e.TotalFonts; ++index)
    {
      if (this.e.TerFont[index].ParaStyId == CurStyle)
        this.e.TerFont[index].ParaStyId = 0;
      if (this.e.TerFont[index].CharStyId == CurStyle)
        this.e.TerFont[index].CharStyId = 1;
    }
    this.e.StyleId[CurStyle].InUse = false;
    ++this.e.TerArg.modified;
    return true;
  }

  internal int TerEditList(
    bool NewList,
    int ListId,
    bool PropDialog,
    string name,
    bool nested,
    int flags)
  {
    new tc.StrListLevel().init();
    new tc.StrListLevel().init();
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    tc.StrListLevel strListLevel1;
    if (NewList)
    {
      if ((ListId = this.GetListSlot()) < 0)
        return -1;
      int num1 = 1;
      DateTime now = DateTime.Now;
      int num2 = now.Second * 1000;
      now = DateTime.Now;
      int millisecond = now.Millisecond;
      int num3 = num2 + millisecond;
      for (int index = 0; index < this.e.TotalLists; ++index)
      {
        if (this.e.list[index].InUse && this.e.list[index].TmplId >= num1)
          num1 = this.e.list[index].TmplId + 1;
        if (this.e.list[index].InUse && this.e.list[index].id >= num3)
          num3 = this.e.list[index].id + 1;
      }
      this.e.list[ListId].InUse = true;
      this.e.list[ListId].id = num3;
      this.e.list[ListId].TmplId = num1;
      this.e.list[ListId].FontId = 0;
      int length = nested ? 9 : 1;
      this.e.list[ListId].LevelCount = length;
      this.e.list[ListId].flags = flags;
      this.e.list[ListId].name = name;
      this.e.list[ListId].level = new tc.StrListLevel[length];
      for (int index = 0; index < length; ++index)
      {
        tc.StrListLevel strListLevel2 = new tc.StrListLevel();
        this.e.list[ListId].level[index] = strListLevel2.init();
      }
      for (int index = 0; index < length; ++index)
      {
        strListLevel1 = new tc.StrListLevel().init() with
        {
          start = 1,
          CharAft = 0,
          NumType = 0,
          text = new char[3]{ '\u0002', (char) index, '.' }
        };
        this.e.list[ListId].level[index] = strListLevel1.Copy();
      }
    }
    else if (ListId < 0)
    {
      if (this.e.TotalLists <= 1 || !this.CallDialogBox((Form) new terdlg_list_select(this.e)))
        return -1;
      ListId = this.e.DlgInt1;
    }
    if (ListId < 1 || ListId >= this.e.TotalLists || !this.e.list[ListId].InUse)
      return -1;
    if (PropDialog)
    {
      this.e.DlgText1 = this.e.list[ListId].name;
      this.e.DlgInt1 = this.e.list[ListId].LevelCount > 1 ? 1 : 0;
      this.e.DlgUint = this.e.list[ListId].flags;
      if (!this.CallDialogBox((Form) new terdlg_list_prop(this.e)))
        return -1;
      name = this.e.DlgText1;
      nested = this.True(this.e.DlgInt1);
      flags = this.e.DlgUint;
    }
    this.e.list[ListId].name = name;
    this.e.list[ListId].flags = flags;
    int count = nested ? 9 : 1;
    if (count != this.e.list[ListId].LevelCount)
    {
      this.e.list[ListId].level = this.ReAlloc(this.e.list[ListId].level, count);
      if (count > this.e.list[ListId].LevelCount)
      {
        int levelCount = this.e.list[ListId].LevelCount;
        strListLevel1 = this.e.list[ListId].level[0];
        for (int index1 = levelCount; index1 < count; ++index1)
        {
          tc.StrListLevel strListLevel3 = strListLevel1.Copy();
          int num = (int) strListLevel1.text[0] + 1;
          for (int index2 = 1; index2 < num; ++index2)
          {
            if (strListLevel3.text[index2] == char.MinValue)
              strListLevel3.text[index2] = (char) index1;
          }
          this.e.list[ListId].level[index1] = strListLevel3;
        }
      }
      this.e.list[ListId].LevelCount = count;
    }
    ++this.e.TerArg.modified;
    return ListId;
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
    new tc.StrListLevel().init();
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (id < 0)
    {
      if (!this.CallDialogBox((Form) new terdlg_list_level(this.e)))
        return true;
    }
    else
    {
      if (id <= 0 || level < 0 || FontId < 0 || this.False(this.e.TerFont[FontId].InUse) || (this.e.TerFont[FontId].style & 128 /*0x80*/) != 0)
        return false;
      tc.StrListLevel strListLevel;
      if (IsList)
      {
        if (id >= this.e.TotalLists || !this.e.list[id].InUse || level >= this.e.list[id].LevelCount)
          return false;
        strListLevel = this.e.list[id].level[level].Copy();
      }
      else
      {
        if (id >= this.e.TotalListOr || !this.e.ListOr[id].InUse || level >= this.e.ListOr[id].LevelCount)
          return false;
        strListLevel = this.e.ListOr[id].level[level].Copy();
      }
      strListLevel.start = StartAt;
      strListLevel.NumType = NumType;
      strListLevel.CharAft = CharAft;
      strListLevel.FontId = FontId;
      strListLevel.flags = flags;
      this.CodeListText(text, strListLevel.text);
      if (IsList)
        this.e.list[id].level[level] = strListLevel;
      else
        this.e.ListOr[id].level[level] = strListLevel;
    }
    ++this.e.TerArg.modified;
    this.RequestPagination(true);
    return true;
  }

  internal int TerEditListOr(
    bool NewListOr,
    int ListOrId,
    bool PropDialog,
    int ListId,
    bool OverrideLevels,
    int flags)
  {
    new tc.StrListLevel().init();
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int index1 = 1;
    while (index1 < this.e.TotalLists && !this.e.list[index1].InUse)
      ++index1;
    if (index1 == this.e.TotalLists)
      return -1;
    if (NewListOr)
    {
      if ((ListOrId = this.GetListOrSlot()) < 0)
        return -1;
      this.e.ListOr[ListOrId].InUse = true;
      this.e.ListOr[ListOrId].ListIdx = ListId;
      int levelCount = OverrideLevels ? this.e.list[ListId].LevelCount : 0;
      this.e.ListOr[ListOrId].LevelCount = levelCount;
      this.e.ListOr[ListOrId].flags = flags;
      if (levelCount > 0)
      {
        this.e.ListOr[ListOrId].level = new tc.StrListLevel[levelCount];
        for (int index2 = 0; index2 < levelCount; ++index2)
        {
          tc.StrListLevel strListLevel = this.e.list[ListId].level[index2].Copy();
          strListLevel.flags |= 17;
          this.e.ListOr[ListOrId].level[index2] = strListLevel;
        }
      }
    }
    else if (ListOrId < 0)
    {
      if (!this.CallDialogBox((Form) new terdlg_listor_select(this.e)))
        return -1;
      ListOrId = this.e.DlgInt1;
    }
    if (ListOrId < 1 || ListOrId >= this.e.TotalListOr || !this.e.ListOr[ListOrId].InUse)
      return -1;
    if (PropDialog)
    {
      this.e.DlgInt1 = this.e.ListOr[ListOrId].ListIdx;
      this.e.DlgInt2 = this.e.ListOr[ListOrId].LevelCount > 0 ? 1 : 0;
      if (!this.CallDialogBox((Form) new terdlg_listor_prop(this.e)))
        return -1;
      ListId = this.e.DlgInt1;
      OverrideLevels = this.True(this.e.DlgInt2);
    }
    this.e.ListOr[ListOrId].flags = flags;
    int levelCount1 = OverrideLevels ? this.e.list[ListId].LevelCount : 0;
    if (levelCount1 == this.e.ListOr[ListOrId].LevelCount)
    {
      if (ListId != this.e.ListOr[ListOrId].ListIdx && levelCount1 > 0)
      {
        for (int index3 = 0; index3 < levelCount1; ++index3)
          this.e.ListOr[ListOrId].level[index3] = this.e.list[ListId].level[index3].Copy();
      }
    }
    else
    {
      if (this.e.ListOr[ListOrId].LevelCount > 0)
        this.e.ListOr[ListOrId].level = (tc.StrListLevel[]) null;
      this.e.ListOr[ListOrId].LevelCount = levelCount1;
      this.e.ListOr[ListOrId].level = new tc.StrListLevel[levelCount1];
      for (int index4 = 0; index4 < levelCount1; ++index4)
        this.e.ListOr[ListOrId].level[index4] = this.e.list[ListId].level[index4].Copy();
    }
    this.e.ListOr[ListOrId].ListIdx = ListId;
    ++this.e.TerArg.modified;
    return ListOrId;
  }

  internal int EditStyle(bool start, string name, bool CreateNew, int type, bool repaint)
  {
    return this.EditStyle(start, -1, name, CreateNew, type, false, repaint);
  }

  /// <summary>Редактировать стиль</summary>
  /// <param name="start"></param>
  /// <param name="name"></param>
  /// <param name="CreateNew"></param>
  /// <param name="type"></param>
  /// <param name="paragraphStyleOnly">Редактировать только стиль параграфа</param>
  /// <param name="repaint"></param>
  /// <returns></returns>
  internal int EditStyle(
    bool start,
    int styleID,
    string name,
    bool CreateNew,
    int type,
    bool paraOnly,
    bool repaint,
    bool forceApplyCharStyle = true)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (styleID == -1 && (name == null || name == ""))
      start = this.e.CurSID < 0;
    if (start && this.e.CurSID >= 0 || !start && this.e.CurSID < 0)
      return -1;
    if (!start)
    {
      int curSid = this.e.CurSID;
      if (this.e.StyleId[this.e.CurSID].type == 2)
        this.ApplyParaStyles(this.e.CurSID, !paraOnly);
      else
        this.ApplyCharStyles(this.e.CurSID, forceApplyCharStyle);
      this.e.CurSID = -1;
      this.e.EditingParaStyle = false;
      ++this.e.TerArg.modified;
      this.e.ToolBarFillStyles = true;
      if (repaint)
      {
        this.PaintTer();
        return curSid;
      }
      if (!this.e.WordWrapSuspended)
        this.wrp.WordWrap(0, this.e.TotalLines);
      return curSid;
    }
    if (styleID == -1 && (name == null || name == ""))
    {
      if (!this.e.FullRenderMode || !this.CallDialogBox((Form) new terdlg_edit_style(this.e)))
        return -1;
      name = this.e.TempString;
      CreateNew = this.e.DlgInt1 != 0;
      type = this.e.DlgInt2;
    }
    if (CreateNew)
    {
      for (int index = 0; index < this.e.TotalSID; ++index)
      {
        if (this.e.StyleId[index].InUse && (!this.e.FullRenderMode && this.e.StyleId[index].name == name || this.e.FullRenderMode && string.Compare(this.e.StyleId[index].name, name, true) == 0))
        {
          CreateNew = false;
          break;
        }
      }
    }
    if (CreateNew)
    {
      this.e.CurSID = this.GetStyleIdSlot();
      if (this.e.CurSID < 0)
        return -1;
      if (type != 1 && type != 2)
        type = 2;
      this.e.StyleId[this.e.CurSID].name = name;
      this.e.StyleId[this.e.CurSID].InUse = true;
      this.e.StyleId[this.e.CurSID].type = type;
      this.e.StyleId[this.e.CurSID].OutlineLevel = -1;
      string strB = "heading ";
      if (type == 2 && name.Length > strB.Length)
      {
        int length = strB.Length;
        int num = -1;
        string strA = name.Substring(0, length);
        if (!this.e.FullRenderMode && strA == strB || this.e.FullRenderMode && string.Compare(strA, strB, true) == 0)
          num = (int) name[length] - 49;
        if (num < 0 || num > 8)
          num = -1;
        this.e.StyleId[this.e.CurSID].OutlineLevel = num;
      }
      int effectiveCfmt = this.fnt.GetEffectiveCfmt();
      if (type == 2 || !this.e.FullRenderMode && this.e.TerFont[effectiveCfmt].TypeFace != this.e.TerArg.FontTypeFace || this.e.FullRenderMode && string.Compare(this.e.TerFont[effectiveCfmt].TypeFace, this.e.TerArg.FontTypeFace, true) != 0)
      {
        this.e.StyleId[this.e.CurSID].TypeFace = this.e.TerFont[effectiveCfmt].TypeFace;
        this.e.StyleId[this.e.CurSID].FontFamily = this.e.TerFont[effectiveCfmt].FontFamily;
      }
      else
        this.e.StyleId[this.e.CurSID].FontFamily = this.e.TerFont[0].FontFamily;
      if (type == 2 || this.e.TerFont[effectiveCfmt].TwipsSize / 20 != this.e.TerArg.PointSize)
        this.e.StyleId[this.e.CurSID].TwipsSize = this.e.TerFont[effectiveCfmt].TwipsSize;
      this.e.StyleId[this.e.CurSID].style = (this.e.TerFont[effectiveCfmt].style &= -48257);
      this.e.StyleId[this.e.CurSID].TextColor = this.e.TerFont[effectiveCfmt].TextColor;
      this.e.StyleId[this.e.CurSID].TextBkColor = this.e.TerFont[effectiveCfmt].TextBkColor;
      this.e.StyleId[this.e.CurSID].UlineColor = this.e.TerFont[effectiveCfmt].UlineColor;
      this.e.StyleId[this.e.CurSID].expand = this.e.TerFont[effectiveCfmt].expand;
      this.e.StyleId[this.e.CurSID].offset = this.e.TerFont[effectiveCfmt].offset;
      if (type == 2)
      {
        int pfmt = this.e.text[this.e.CurLine].pfmt;
        this.e.StyleId[this.e.CurSID].LeftIndentTwips = this.e.PfmtId[pfmt].LeftIndentTwips;
        this.e.StyleId[this.e.CurSID].RightIndentTwips = this.e.PfmtId[pfmt].RightIndentTwips;
        this.e.StyleId[this.e.CurSID].FirstIndentTwips = this.e.PfmtId[pfmt].FirstIndentTwips;
        this.e.StyleId[this.e.CurSID].ParaFlags = (this.e.PfmtId[pfmt].flags &= -12289);
        this.e.StyleId[this.e.CurSID].pflags = this.e.PfmtId[pfmt].pflags & 65520;
        this.e.StyleId[this.e.CurSID].shading = this.e.PfmtId[pfmt].shading;
        this.e.StyleId[this.e.CurSID].SpaceBefore = this.e.PfmtId[pfmt].SpaceBefore;
        this.e.StyleId[this.e.CurSID].SpaceAfter = this.e.PfmtId[pfmt].SpaceAfter;
        this.e.StyleId[this.e.CurSID].SpaceBetween = this.e.PfmtId[pfmt].SpaceBetween;
        this.e.StyleId[this.e.CurSID].LineSpacing = this.e.PfmtId[pfmt].LineSpacing;
        this.e.StyleId[this.e.CurSID].ParaBkColor = this.e.PfmtId[pfmt].BkColor;
        this.e.StyleId[this.e.CurSID].ParaBorderColor = this.e.PfmtId[pfmt].BorderColor;
        this.e.StyleId[this.e.CurSID].TabId = this.e.PfmtId[pfmt].TabId;
      }
      this.e.StyleId[this.e.CurSID].flags |= 1;
    }
    else if (styleID == -1)
    {
      for (int index = 0; index < this.e.TotalSID; ++index)
      {
        if (this.e.StyleId[index].InUse && (!this.e.FullRenderMode && this.e.StyleId[index].name == name || this.strcmpi(this.e.StyleId[index].name, name) == 0))
        {
          this.e.CurSID = index;
          break;
        }
      }
    }
    else
      this.e.CurSID = styleID;
    this.e.PrevStyleId = this.e.StyleId[this.e.CurSID];
    if (this.e.StyleId[this.e.CurSID].type == 2)
      this.e.EditingParaStyle = true;
    this.e.HilightType = 0;
    if (repaint || this.e.FullRenderMode)
      this.PaintTer();
    return this.e.CurSID;
  }

  /// <summary>Установить параметры стиля в соответсвии со шрифтом</summary>
  /// <param name="styleID">Идентификатор стиля</param>
  /// <param name="fontID">Идентификатор шрифта</param>
  internal void SetStyleParamsFromFont(int styleID, int fontID)
  {
    this.e.StyleId[styleID].TypeFace = this.e.TerFont[fontID].TypeFace;
    this.e.StyleId[styleID].FontFamily = this.e.TerFont[fontID].FontFamily;
    this.e.StyleId[styleID].TwipsSize = this.e.TerFont[fontID].TwipsSize;
    this.e.StyleId[styleID].style = tc.ResetUintFlag(ref this.e.TerFont[fontID].style, 48256);
    this.e.StyleId[styleID].TextColor = this.e.TerFont[fontID].TextColor;
    this.e.StyleId[styleID].TextBkColor = this.e.TerFont[fontID].TextBkColor;
    this.e.StyleId[styleID].UlineColor = this.e.TerFont[fontID].UlineColor;
    this.e.StyleId[styleID].expand = this.e.TerFont[fontID].expand;
    this.e.StyleId[styleID].offset = this.e.TerFont[fontID].offset;
  }

  /// <summary>
  /// Прпопорционально изменить размер шрифта выбранного шрифта редактора или во всех шрифтах
  /// </summary>
  /// <param name="fontID">id шрифта или -1 для всех</param>
  /// <param name="scaleFactor">коэффициент</param>
  internal void ScaleEditorFontSize(int fontID, float scaleFactor)
  {
    if (fontID >= 0)
    {
      this.e.TerFont[fontID].TwipsSize = (int) Math.Floor((double) this.e.TerFont[fontID].TwipsSize * (double) scaleFactor);
      int charStyId = this.e.TerFont[fontID].CharStyId;
      int paraStyId = this.e.TerFont[fontID].ParaStyId;
      for (int CurStyle = 0; CurStyle < this.e.StyleId.Length; ++CurStyle)
      {
        if (CurStyle == charStyId || CurStyle == paraStyId)
        {
          this.e.StyleId[CurStyle].TwipsSize = (int) Math.Floor((double) this.e.StyleId[CurStyle].TwipsSize * (double) scaleFactor);
          this.ApplyParaStyles(CurStyle, true);
        }
      }
    }
    else
    {
      for (int index = 0; index < this.e.TerFont.Length; ++index)
        this.e.TerFont[index].TwipsSize = (int) Math.Floor((double) this.e.TerFont[index].TwipsSize * (double) scaleFactor);
      for (int CurStyle = 0; CurStyle < this.e.StyleId.Length; ++CurStyle)
      {
        this.e.StyleId[CurStyle].TwipsSize = (int) Math.Floor((double) this.e.StyleId[CurStyle].TwipsSize * (double) scaleFactor);
        if (this.e.StyleId[CurStyle].type == 2 || this.e.StyleId[CurStyle].type == 1)
          this.ApplyParaStyles(CurStyle, true);
      }
    }
  }

  internal int TerGetBulletInfo(
    int IdType,
    int id,
    out bool IsBullet,
    out int start,
    out int level,
    out int symbol,
    out int flags)
  {
    return this.TerGetBulletInfo2(IdType, id, out IsBullet, out start, out level, out symbol, out tc.SkipInt, out flags);
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
    return this.TerGetBulletInfo3(IdType, id, out IsBullet, out start, out level, out symbol, out ListOr, out flags, out tc.SkipStr);
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
    tc.StrListLevel pLevel = new tc.StrListLevel();
    bool flag = false;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    IsBullet = false;
    int num1;
    flags = num1 = 0;
    int num2;
    ListOr = num2 = num1;
    int num3;
    symbol = num3 = num2;
    int num4;
    level = num4 = num3;
    start = num4;
    ListText = "";
    if (IdType == 0)
    {
      if (id < -1 || id >= this.e.TotalLines)
        return -1;
      if (id == -1)
        id = this.e.CurLine;
      IdType = 1;
      id = this.e.text[id].pfmt;
    }
    if (IdType == 1)
    {
      if (id < 0 || id >= this.e.TotalPfmts)
        return -1;
      IdType = 2;
      int index = id;
      id = this.e.PfmtId[index].BltId;
      if (id == 0 && (this.e.PfmtId[index].flags & 8) == 0)
        return 0;
    }
    else if (IdType == 4)
    {
      if (id < 0 || id >= this.e.TotalSID)
        return -1;
      id = this.e.StyleId[id].BltId;
      if (id == 0 && (this.e.StyleId[id].ParaFlags & 8) == 0)
        return 0;
    }
    int bulletInfo3 = id;
    if (bulletInfo3 < 0 || bulletInfo3 >= this.e.TotalBlts)
      return -1;
    IsBullet = this.e.TerBlt[bulletInfo3].IsBullet;
    ListOr = this.e.TerBlt[bulletInfo3].ls;
    if (this.e.TerBlt[bulletInfo3].ls > 0)
      flag = this.GetListLevelPtr(this.e.TerBlt[bulletInfo3].ls, this.e.TerBlt[bulletInfo3].lvl, out pLevel);
    if (flag & IsBullet)
      IsBullet = pLevel.NumType == 23;
    level = !flag ? this.e.TerBlt[bulletInfo3].level : this.e.TerBlt[bulletInfo3].lvl;
    start = !flag ? this.e.TerBlt[bulletInfo3].start : pLevel.start;
    flags = this.e.TerBlt[bulletInfo3].flags;
    if (this.e.TerBlt[bulletInfo3].IsBullet)
    {
      char bulletChar = this.e.TerBlt[bulletInfo3].BulletChar;
      symbol = 0;
      if (this.e.TerBlt[bulletInfo3].font == 1)
      {
        if (bulletChar == '·')
          symbol = 0;
        if (bulletChar == '¨')
          symbol = 1;
        return bulletInfo3;
      }
      if (this.e.TerBlt[bulletInfo3].font == 2)
      {
        if (bulletChar == '§')
          symbol = 2;
        if (bulletChar == 'q')
          symbol = 3;
        if (bulletChar == 'v')
          symbol = 4;
        if (bulletChar == 'Ø')
          symbol = 5;
        if (bulletChar == 'ü')
          symbol = 6;
      }
      return bulletInfo3;
    }
    if (flag)
    {
      int numType = pLevel.NumType;
      switch (numType)
      {
        case 0:
          symbol = 0;
          break;
        case 1:
          symbol = 3;
          break;
        case 2:
          symbol = 4;
          break;
        case 3:
          symbol = 1;
          break;
        case 4:
          symbol = 2;
          break;
        default:
          symbol = 5;
          break;
      }
      IsBullet = numType == 23;
      this.DecodeListText(pLevel.text, out ListText);
      return bulletInfo3;
    }
    symbol = this.e.TerBlt[bulletInfo3].NumberType;
    return bulletInfo3;
  }

  internal int TerGetListId(string name)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    for (int listId = 0; listId < this.e.TotalLists; ++listId)
    {
      if (this.e.list[listId].InUse && string.Compare(this.e.list[listId].name, name, true) == 0)
        return listId;
    }
    return -1;
  }

  internal bool TerGetListInfo(int ListId, out string name, out int pLevelCount, out int pFlags)
  {
    return this.TerGetListInfo(ListId, out name, out pLevelCount, out pFlags, out tc.SkipInt, out tc.SkipInt);
  }

  internal bool TerGetListInfo(
    int ListId,
    out string name,
    out int pLevelCount,
    out int pFlags,
    out int pRtfId,
    out int pTmplId)
  {
    tc.StrList strList1 = new tc.StrList();
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    name = "";
    int num1;
    pTmplId = num1 = 0;
    int num2;
    pRtfId = num2 = num1;
    pLevelCount = num2;
    pFlags = 0;
    if (ListId < 0)
    {
      int bltId = this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].BltId;
      if (bltId == 0 || this.e.TerBlt[bltId].ls == 0)
        return false;
      ListId = this.e.ListOr[this.e.TerBlt[bltId].ls].ListIdx;
    }
    if (ListId < 0 || ListId >= this.e.TotalLists)
      return false;
    tc.StrList strList2 = this.e.list[ListId];
    name = strList2.name;
    pLevelCount = strList2.LevelCount;
    pFlags = strList2.flags;
    pRtfId = strList2.id;
    pTmplId = strList2.TmplId;
    return true;
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
    new tc.StrListLevel().init();
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int num1;
    pFontId = num1 = 0;
    int num2;
    pNumType = num2 = num1;
    pStartAt = num2;
    pCharAft = 0;
    text = "";
    pFlags = 0;
    if (id < 0)
    {
      int bltId = this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].BltId;
      if (bltId == 0 || this.e.TerBlt[bltId].ls == 0)
        return false;
      int ls = this.e.TerBlt[bltId].ls;
      id = !IsList ? ls : this.e.ListOr[ls].ListIdx;
      level = this.e.TerBlt[bltId].lvl;
    }
    tc.StrListLevel strListLevel;
    if (IsList)
    {
      if (id >= this.e.TotalLists || !this.e.list[id].InUse || level >= this.e.list[id].LevelCount)
        return false;
      strListLevel = this.e.list[id].level[level];
    }
    else
    {
      if (id >= this.e.TotalListOr || !this.e.ListOr[id].InUse || level >= this.e.ListOr[id].LevelCount)
        return false;
      strListLevel = this.e.ListOr[id].level[level];
    }
    pStartAt = strListLevel.start;
    pNumType = strListLevel.NumType;
    pCharAft = strListLevel.CharAft;
    pFontId = strListLevel.FontId;
    pFlags = strListLevel.flags;
    this.DecodeListText(strListLevel.text, out text);
    string typeFace = this.e.TerFont[strListLevel.FontId].TypeFace;
    if (strListLevel.NumType == 23)
    {
      if (text.Length == 0)
        text = new string(char.MinValue, 1);
      else if (text.Length == 1)
      {
        char ch = text[0];
        if (ch == '¨' && string.Compare(typeFace, "Symbol", true) == 0)
          text = new string('\u0001', 1);
        else if (ch == '§' && string.Compare(typeFace, "Wingdings", true) == 0)
          text = new string('\u0002', 1);
        else if (ch == 'q' && string.Compare(typeFace, "Wingdings", true) == 0)
          text = new string('\u0003', 1);
        else if (ch == 'v' && string.Compare(typeFace, "Wingdings", true) == 0)
          text = new string('\u0004', 1);
        else if (ch == 'Ø' && string.Compare(typeFace, "Wingdings", true) == 0)
          text = new string('\u0005', 1);
        else if (ch == 'ü' && string.Compare(typeFace, "Wingdings", true) == 0)
          text = new string('\u0006', 1);
        else if (ch == '·' && string.Compare(typeFace, "Symbol", true) == 0)
          text = new string(char.MinValue, 1);
      }
    }
    return true;
  }

  internal int TerGetListLine(int line)
  {
    int num1 = -1;
    int num2 = 0;
    int num3 = 0;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (line < 0)
      line = this.e.CurLine;
    if ((this.e.PfmtId[this.e.text[line].pfmt].flags & 8) == 0)
    {
      for (int index = line - 1; index >= 0; --index)
      {
        if ((this.e.PfmtId[this.e.text[index].pfmt].flags & 8) != 0)
        {
          int bltId;
          int ls;
          if ((bltId = this.e.PfmtId[this.e.text[index].pfmt].BltId) != 0 && (ls = this.e.TerBlt[bltId].ls) != 0)
          {
            num2 = this.e.ListOr[ls].ListIdx;
            num1 = index;
            num3 = this.e.PfmtId[this.e.text[index].pfmt].LeftIndentTwips;
            break;
          }
          break;
        }
      }
      if (num2 == 0 || num3 != this.e.PfmtId[this.e.text[line].pfmt].LeftIndentTwips)
        return -1;
      for (int index = line + 1; index < this.e.TotalLines; ++index)
      {
        if ((this.e.PfmtId[this.e.text[index].pfmt].flags & 8) != 0)
        {
          int bltId;
          int ls;
          return (bltId = this.e.PfmtId[this.e.text[index].pfmt].BltId) == 0 || (ls = this.e.TerBlt[bltId].ls) == 0 || num2 != this.e.ListOr[ls].ListIdx ? -1 : num1;
        }
      }
    }
    return -1;
  }

  internal bool TerGetListOrInfo(
    int ListOrId,
    out int pListId,
    out int pLevelCount,
    out int pFlags)
  {
    tc.StrListOr strListOr1 = new tc.StrListOr();
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int num1;
    pFlags = num1 = 0;
    int num2;
    pLevelCount = num2 = num1;
    pListId = num2;
    if (ListOrId < 0)
    {
      int bltId = this.e.PfmtId[this.e.text[this.e.CurLine].pfmt].BltId;
      if (bltId == 0 || this.e.TerBlt[bltId].ls == 0)
        return false;
      ListOrId = this.e.TerBlt[bltId].ls;
    }
    if (ListOrId < 0 || ListOrId >= this.e.TotalListOr)
      return false;
    tc.StrListOr strListOr2 = this.e.ListOr[ListOrId];
    pListId = strListOr2.ListIdx;
    pLevelCount = strListOr2.LevelCount;
    pFlags = strListOr2.flags;
    return true;
  }

  internal int TerGetParaAux1Id(int CurPara)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    return CurPara < 0 || CurPara >= this.e.TotalPfmts ? 0 : this.e.PfmtId[CurPara].Aux1Id;
  }

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
    return this.TerGetParaInfo2(LineNo, out LeftIndent, out RightIndent, out FirstIndent, out TabId, out StyId, out AuxId, out shading, out pflags, out SpaceBefore, out SpaceAfter, out SpaceBetween, out flags, out tc.SkipUint, out tc.SkipColor);
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
    return this.TerGetParaInfo3(LineNo, false, out LeftIndent, out RightIndent, out FirstIndent, out TabId, out StyId, out AuxId, out shading, out pflags, out SpaceBefore, out SpaceAfter, out SpaceBetween, out flags, out Aux1Id, out BkColor);
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
    return this.TerGetParaInfo4(LineNo, IsStyleItem, out LeftIndent, out RightIndent, out FirstIndent, out TabId, out StyId, out AuxId, out shading, out pflags, out SpaceBefore, out SpaceAfter, out SpaceBetween, out flags, out Aux1Id, out BkColor, out tc.SkipInt);
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
    int index1 = 0;
    int index2 = -1;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int num1;
    LineSpacing = num1 = 0;
    int num2;
    SpaceBetween = num2 = num1;
    int num3;
    SpaceAfter = num3 = num2;
    int num4;
    SpaceBefore = num4 = num3;
    int num5;
    shading = num5 = num4;
    int num6;
    AuxId = num6 = num5;
    int num7;
    StyId = num7 = num6;
    int num8;
    TabId = num8 = num7;
    int num9;
    FirstIndent = num9 = num8;
    int num10;
    RightIndent = num10 = num9;
    LeftIndent = num10;
    int num11;
    Aux1Id = num11 = 0;
    int num12;
    flags = num12 = num11;
    pflags = num12;
    BkColor = tc.CLR_WHITE;
    if (IsStyleItem)
    {
      index2 = LineNo != -9998 ? LineNo : this.e.CurSID;
      if (index2 < 0 || index2 >= this.e.TotalSID)
        return false;
    }
    else
    {
      if (LineNo < 0)
      {
        index1 = -LineNo;
      }
      else
      {
        if (LineNo >= this.e.TotalLines)
          return false;
        index1 = this.e.text[LineNo].pfmt;
      }
      if (index1 < 0 || index1 >= this.e.TotalPfmts)
        return false;
    }
    if (index2 < 0)
    {
      LeftIndent = this.e.PfmtId[index1].LeftIndentTwips;
      RightIndent = this.e.PfmtId[index1].RightIndentTwips;
      FirstIndent = this.e.PfmtId[index1].FirstIndentTwips;
      TabId = this.e.PfmtId[index1].TabId;
      StyId = this.e.PfmtId[index1].StyId;
      AuxId = this.e.PfmtId[index1].AuxId;
      shading = this.e.PfmtId[index1].shading;
      pflags = this.e.PfmtId[index1].pflags;
      SpaceBefore = this.e.PfmtId[index1].SpaceBefore;
      SpaceAfter = this.e.PfmtId[index1].SpaceAfter;
      SpaceBetween = this.e.PfmtId[index1].SpaceBetween;
      LineSpacing = this.e.PfmtId[index1].LineSpacing;
      flags = this.e.PfmtId[index1].flags;
      Aux1Id = this.e.PfmtId[index1].Aux1Id;
      BkColor = this.e.PfmtId[index1].BkColor;
    }
    else
    {
      LeftIndent = this.e.StyleId[index2].LeftIndentTwips;
      RightIndent = this.e.StyleId[index2].RightIndentTwips;
      FirstIndent = this.e.StyleId[index2].FirstIndentTwips;
      TabId = this.e.StyleId[index2].TabId;
      StyId = index2;
      AuxId = 0;
      shading = this.e.StyleId[index2].shading;
      pflags = this.e.StyleId[index2].pflags;
      SpaceBefore = this.e.StyleId[index2].SpaceBefore;
      SpaceAfter = this.e.StyleId[index2].SpaceAfter;
      SpaceBetween = this.e.StyleId[index2].SpaceBetween;
      LineSpacing = this.e.StyleId[index2].LineSpacing;
      flags = this.e.StyleId[index2].ParaFlags;
      Aux1Id = 0;
      BkColor = this.e.StyleId[index2].ParaBkColor;
    }
    return true;
  }

  internal int TerGetParaParam(int LineNo, bool IsStyleItem, int type)
  {
    int index1 = 0;
    int index2 = -1;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (IsStyleItem)
    {
      index2 = LineNo != -9998 ? LineNo : this.e.CurSID;
      if (index2 < 0 || index2 >= this.e.TotalSID)
        return -999999;
    }
    else
    {
      if (LineNo < 0)
      {
        index1 = -LineNo;
      }
      else
      {
        if (LineNo >= this.e.TotalLines)
          return -999999;
        index1 = this.e.text[LineNo].pfmt;
      }
      if (index1 < 0 || index1 >= this.e.TotalPfmts)
        return -999999;
    }
    switch (type)
    {
      case 1:
        return index2 >= 0 ? 0 : this.e.PfmtId[index1].flow;
      case 4:
        return index2 >= 0 ? this.e.StyleId[index2].SpaceBefore : this.e.PfmtId[index1].SpaceBefore;
      case 5:
        return index2 >= 0 ? this.e.StyleId[index2].SpaceAfter : this.e.PfmtId[index1].SpaceAfter;
      case 6:
        return index2 >= 0 ? this.e.StyleId[index2].SpaceBetween : this.e.PfmtId[index1].SpaceBetween;
      default:
        return -999999;
    }
  }

  internal bool TerGetParaParam(int LineNo, bool IsStyleItem, int type, out Color color)
  {
    int index1 = 0;
    int index2 = -1;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    color = tc.CLR_AUTO;
    if (IsStyleItem)
    {
      index2 = LineNo != -9998 ? LineNo : this.e.CurSID;
      if (index2 < 0 || index2 >= this.e.TotalSID)
        return false;
    }
    else
    {
      if (LineNo < 0)
      {
        index1 = -LineNo;
      }
      else
      {
        if (LineNo >= this.e.TotalLines)
          return false;
        index1 = this.e.text[LineNo].pfmt;
      }
      if (index1 < 0 || index1 >= this.e.TotalPfmts)
        return false;
    }
    switch (type)
    {
      case 2:
        color = index2 < 0 ? this.e.PfmtId[index1].BorderColor : this.e.StyleId[index2].ParaBorderColor;
        return true;
      case 3:
        color = index2 < 0 ? this.e.PfmtId[index1].BkColor : this.e.StyleId[index2].ParaBkColor;
        return true;
      default:
        return false;
    }
  }

  internal int TerGetStyleId(string name)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (name == null || name.Length == 0)
      return -1;
    int index = 0;
    while (index < this.e.TotalSID && this.strcmpi(name, this.e.StyleId[index].name) != 0)
      ++index;
    return index >= this.e.TotalSID ? -1 : index;
  }

  internal int TerGetStyleInfo(int id, out string name, out int pType)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    name = "";
    pType = 0;
    if (id < 0 || id >= this.e.TotalSID || this.False(this.e.StyleId[id].InUse))
      return -1;
    name = this.e.StyleId[id].name;
    pType = this.e.StyleId[id].type;
    return this.e.TotalSID;
  }

  internal int TerGetStyleParam(int id, int type)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (id >= 0 && id < this.e.TotalSID && this.e.StyleId[id].InUse)
    {
      switch (type)
      {
        case 2:
          return this.e.StyleId[id].expand;
        case 3:
          return this.e.StyleId[id].offset;
        case 4:
          return this.e.StyleId[id].next;
      }
    }
    return -1;
  }

  internal int TerGetTabStop(int line, int TabNo, out int pPos, out int pType, out int pFlag)
  {
    return this.e.TerGetTabStop2(0, line, TabNo, out pPos, out pType, out pFlag);
  }

  internal int TerGetTabStop2(
    int type,
    int line,
    int TabNo,
    out int pPos,
    out int pType,
    out int pFlag)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    int num1;
    pFlag = num1 = 0;
    int num2;
    pType = num2 = num1;
    pPos = num2;
    if (!this.e.TerArg.WordWrap)
      return -1;
    int index;
    if (type == 0 || line < 0)
    {
      if (line < 0)
        line = this.e.CurLine;
      if (line >= this.e.TotalLines)
        return -1;
      index = this.e.PfmtId[this.e.text[line].pfmt].TabId;
    }
    else if (type == 1)
    {
      if (line > this.e.TotalPfmts)
        return -1;
      index = this.e.PfmtId[line].TabId;
    }
    else
    {
      if (line > this.e.TotalTabs)
        return -1;
      index = line;
    }
    if (TabNo >= 0)
    {
      if (TabNo >= this.e.TerTab[index].count)
        return -1;
      pPos = this.e.TerTab[index].pos[TabNo];
      pType = this.e.TerTab[index].type[TabNo];
      pFlag = (int) this.e.TerTab[index].flags[TabNo];
    }
    return this.e.TerTab[index].count;
  }

  internal new bool TerParaBorder()
  {
    if (this.e.HilightType != 0)
      this.NormalizeBlock();
    if (!this.CallDialogBox((Form) new terdlg_para_box(this.e)))
      return false;
    if (this.e.TempString != "")
    {
      int num = this.ToInt(this.e.TempString);
      if (num < 0)
        num = 0;
      if (num > 100)
        num = 100;
      this.TerSetParaShading(num * 100, !this.True(this.e.DlgOnFlags) && !this.True(this.e.DlgOffFlags));
    }
    if (this.False(this.e.DlgOffFlags) && this.True(this.e.DlgOnFlags))
      this.SetTerParaFmt(this.e.DlgOnFlags, true, false);
    else if (this.True(this.e.DlgOffFlags) && this.False(this.e.DlgOnFlags))
      this.SetTerParaFmt(this.e.DlgOffFlags, false, false);
    else if (this.True(this.e.DlgOffFlags) && this.True(this.e.DlgOnFlags))
    {
      this.SetTerParaFmt(this.e.DlgOnFlags, true, false);
      this.SetTerParaFmt(this.e.DlgOffFlags, false, true);
    }
    this.e.TerSetParaBorderColor(this.e.DlgColor1, true);
    return true;
  }

  internal new bool TerParaSpacing()
  {
    if (this.e.HilightType != 0)
      this.NormalizeBlock();
    if (!this.CallDialogBox((Form) new terdlg_para_space(this.e)))
      return false;
    this.TerSetParaSpacing2(this.PointsToTwips((float) this.e.DlgInt1), this.PointsToTwips((float) this.e.DlgInt2), this.PointsToTwips((float) this.e.DlgInt3), this.e.DlgInt4, true);
    return true;
  }

  internal bool TerSelectCharStyle(int CurStyle, bool repaint)
  {
    tc.DgtGetNewFontId GetNewFontId = new tc.DgtGetNewFontId(this.GetNewCharStyle);
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (CurStyle < 0)
    {
      this.e.DlgInt1 = 1;
      if (!this.CallDialogBox((Form) new terdlg_select_style(this.e)))
        return false;
      CurStyle = this.e.DlgResult;
    }
    return CurStyle < this.e.TotalSID && this.e.StyleId[CurStyle].type == 1 && this.CharFmt(GetNewFontId, CurStyle, 0, (string) null, repaint);
  }

  internal bool TerSelectParaStyle(int CurStyle, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.WordWrap)
      return false;
    if (CurStyle < 0)
    {
      this.e.DlgInt1 = 2;
      if (!this.CallDialogBox((Form) new terdlg_select_style(this.e)))
        return false;
      CurStyle = this.e.DlgResult;
    }
    if (CurStyle >= this.e.TotalSID || this.e.StyleId[CurStyle].type != 2)
      return false;
    int StartLine;
    int EndLine;
    this.GetParaRange(out StartLine, out EndLine);
    if ((this.e.TerFlags2 & 16384 /*0x4000*/) == 0 || !this.BlockHasProtectOn2(StartLine, 0, EndLine, this.e.text[EndLine].len, true, false))
    {
      if (!this.e.EditingParaStyle)
        this.SaveUndo(StartLine, 0, EndLine, 0, 'P');
      for (int LineNo = StartLine; LineNo <= EndLine; ++LineNo)
      {
        if (this.LineSelected(LineNo))
        {
          int styId = this.e.PfmtId[this.e.text[LineNo].pfmt].StyId;
          tc.StrPfmt para = this.e.PfmtId[this.e.text[LineNo].pfmt];
          this.SetParaStyleId(ref para, this.e.StyleId[para.StyId], this.e.StyleId[CurStyle], true);
          this.e.text[LineNo].pfmt = this.NewParaId(this.e.text[LineNo].pfmt, para.LeftIndentTwips, para.RightIndentTwips, para.FirstIndentTwips, para.TabId, para.BltId, para.AuxId, para.Aux1Id, CurStyle, para.shading, para.pflags, para.SpaceBefore, para.SpaceAfter, para.SpaceBetween, para.LineSpacing, para.BkColor, para.BorderSpace, para.flow, para.flags);
          this.ApplyLineTextStyle(LineNo, CurStyle, styId);
        }
      }
      ++this.e.TerArg.modified;
      this.e.RepageBeginLine = StartLine;
      this.e.InputFontId = -1;
      if (repaint)
        this.PaintTer();
      else
        this.WordWrap(StartLine, EndLine + 10 - StartLine);
    }
    return true;
  }

  internal bool TerSetBullet(bool set, bool repaint)
  {
    return this.TerSetBulletEx(set, true, 1, 0, 0, repaint);
  }

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
    return this.TerSetBullet3(set, IsBullet, start, level, type, TextBef, TextAft, 0, repaint);
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
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.WordWrap)
      return false;
    if (TextBef == null)
      TextBef = "";
    if (TextAft == null)
      TextAft = "";
    if (level < 0)
      level = 0;
    if (level > 10)
      level = 10;
    int StartLine;
    int EndLine;
    this.GetParaRange(out StartLine, out EndLine);
    if (!this.e.EditingParaStyle)
      this.SaveUndo(StartLine, 0, EndLine, 0, 'P');
    for (int LineNo = StartLine; LineNo <= EndLine; ++LineNo)
    {
      if (this.LineSelected(LineNo))
      {
        int num;
        int LeftIndentTwips;
        int FirstIndentTwips;
        int bltId;
        if (this.e.EditingParaStyle)
        {
          num = this.e.StyleId[this.e.CurSID].ParaFlags;
          LeftIndentTwips = this.e.StyleId[this.e.CurSID].LeftIndentTwips;
          FirstIndentTwips = this.e.StyleId[this.e.CurSID].FirstIndentTwips;
          bltId = this.e.StyleId[this.e.CurSID].BltId;
        }
        else
        {
          num = this.e.PfmtId[this.e.text[LineNo].pfmt].flags;
          LeftIndentTwips = this.e.PfmtId[this.e.text[LineNo].pfmt].LeftIndentTwips;
          FirstIndentTwips = this.e.PfmtId[this.e.text[LineNo].pfmt].FirstIndentTwips;
          bltId = this.e.PfmtId[this.e.text[LineNo].pfmt].BltId;
        }
        int flags;
        int BltId;
        if (set)
        {
          flags = num | 8;
          BltId = this.TerCreateBulletId3(IsBullet, start, level, type, TextBef, TextAft, BltFlags);
        }
        else
        {
          flags = num & -9;
          BltId = 0;
        }
        if (set && (this.e.PfmtId[this.e.text[LineNo].pfmt].flags & 8) == 0)
        {
          LeftIndentTwips += 360;
          FirstIndentTwips -= 360;
        }
        else if (!set && (this.e.PfmtId[this.e.text[LineNo].pfmt].flags & 8) != 0)
        {
          LeftIndentTwips -= 360;
          FirstIndentTwips += 360;
        }
        if (LeftIndentTwips < 0)
          LeftIndentTwips = 0;
        if (LeftIndentTwips + FirstIndentTwips < 0)
          FirstIndentTwips = -LeftIndentTwips;
        if (this.e.EditingParaStyle)
        {
          this.e.StyleId[this.e.CurSID].ParaFlags = flags;
          this.e.StyleId[this.e.CurSID].LeftIndentTwips = LeftIndentTwips;
          this.e.StyleId[this.e.CurSID].FirstIndentTwips = FirstIndentTwips;
          this.e.StyleId[this.e.CurSID].BltId = BltId;
          this.DrawRuler(false);
          return true;
        }
        this.e.text[LineNo].pfmt = this.NewParaId(this.e.text[LineNo].pfmt, LeftIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].RightIndentTwips, FirstIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].TabId, BltId, this.e.PfmtId[this.e.text[LineNo].pfmt].AuxId, this.e.PfmtId[this.e.text[LineNo].pfmt].Aux1Id, this.e.PfmtId[this.e.text[LineNo].pfmt].StyId, this.e.PfmtId[this.e.text[LineNo].pfmt].shading, this.e.PfmtId[this.e.text[LineNo].pfmt].pflags, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceBefore, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceAfter, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceBetween, this.e.PfmtId[this.e.text[LineNo].pfmt].LineSpacing, this.e.PfmtId[this.e.text[LineNo].pfmt].BkColor, this.e.PfmtId[this.e.text[LineNo].pfmt].BorderSpace, this.e.PfmtId[this.e.text[LineNo].pfmt].flow, flags);
      }
    }
    ++this.e.TerArg.modified;
    if (repaint)
    {
      this.PaintTer();
      if (this.e.RulerPending)
        this.DrawRuler(false);
    }
    else
      this.WordWrap(StartLine, EndLine + 10 - StartLine);
    return true;
  }

  internal bool TerSetBulletEx(
    bool set,
    bool IsBullet,
    int start,
    int level,
    int type,
    bool repaint)
  {
    return this.TerSetBullet2(set, IsBullet, start, level, type, (string) null, (string) null, repaint);
  }

  internal bool TerSetBulletId(int BltId, int ParaId)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (ParaId < 0 || ParaId >= this.e.TotalPfmts || BltId < 0 || BltId >= this.e.TotalBlts)
      return false;
    this.e.PfmtId[ParaId].BltId = BltId;
    return true;
  }

  internal int TerSetDefTabWidth(int width, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (width < 60)
      width = 60;
    int defTabWidth = this.e.DefTabWidth;
    this.e.DefTabWidth = width;
    if (!repaint)
      return defTabWidth;
    this.PaintTer();
    return defTabWidth;
  }

  internal bool TerSetListBullet(
    bool set,
    int NumType,
    int level,
    int start,
    string TextBef,
    string TextAft,
    bool repaint)
  {
    return this.TerSetListBullet2(set, NumType, level, start, TextBef, TextAft, "", repaint);
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
    int num1 = 0;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.WordWrap)
      return false;
    if (TextBef == null)
      TextBef = "";
    if (TextAft == null)
      TextAft = "";
    if (ListText == null)
      ListText = "";
    if (level > 10)
      level = 10;
    int StartLine;
    int EndLine;
    this.GetParaRange(out StartLine, out EndLine);
    if (!this.e.EditingParaStyle)
      this.SaveUndo(StartLine, 0, EndLine, 0, 'P');
    for (int index1 = StartLine; index1 <= EndLine; ++index1)
    {
      if (this.LineSelected(index1))
      {
        int flags1;
        int leftIndentTwips;
        int firstIndentTwips;
        int BltId;
        if (this.e.EditingParaStyle)
        {
          flags1 = this.e.StyleId[this.e.CurSID].ParaFlags;
          leftIndentTwips = this.e.StyleId[this.e.CurSID].LeftIndentTwips;
          firstIndentTwips = this.e.StyleId[this.e.CurSID].FirstIndentTwips;
          BltId = this.e.StyleId[this.e.CurSID].BltId;
        }
        else
        {
          flags1 = this.e.PfmtId[this.e.text[index1].pfmt].flags;
          leftIndentTwips = this.e.PfmtId[this.e.text[index1].pfmt].LeftIndentTwips;
          firstIndentTwips = this.e.PfmtId[this.e.text[index1].pfmt].FirstIndentTwips;
          BltId = this.e.PfmtId[this.e.text[index1].pfmt].BltId;
        }
        if (level < 0 && (flags1 & 8) != 0)
          level = this.e.TerBlt[BltId].lvl;
        if (level < 0)
          level = 0;
        int flags2;
        if (set)
        {
          flags2 = flags1 | 8;
          if (num1 == 0)
          {
            int CurListOr = 0;
            int ListId = 0;
            bool flag1 = false;
            if (this.e.TerBlt[BltId].ls > 0)
            {
              CurListOr = this.e.TerBlt[BltId].ls;
              ListId = this.e.ListOr[CurListOr].ListIdx;
            }
            if (this.e.TerBlt[BltId].ls == 0 || !this.e.ListOr[CurListOr].InUse || this.e.list[ListId].LevelCount != 9)
            {
              ListId = this.TerEditList(true, 0, false, this.GetNewListName("List"), true, 0);
              CurListOr = this.TerEditListOr(true, 0, false, ListId, false, 0);
              flag1 = true;
            }
            tc.StrListLevel[] strListLevelArray = (tc.StrListLevel[]) null;
            if (this.e.ListOr[CurListOr].LevelCount > level)
              strListLevelArray = this.e.ListOr[CurListOr].level;
            else if (this.e.list[ListId].LevelCount > level)
              strListLevelArray = this.e.list[ListId].level;
            if (strListLevelArray != null && strListLevelArray[level].NumType != NumType)
              strListLevelArray = (tc.StrListLevel[]) null;
            if (strListLevelArray == null)
            {
              for (int index2 = 0; index2 < this.e.TotalListOr; ++index2)
              {
                if (this.e.ListOr[index2].ListIdx == ListId && this.e.ListOr[index2].LevelCount > level && this.e.ListOr[index2].level[level].NumType == NumType)
                {
                  CurListOr = index2;
                  flag1 = true;
                  strListLevelArray = this.e.ListOr[CurListOr].level;
                  break;
                }
              }
            }
            if (strListLevelArray == null)
            {
              CurListOr = this.e.TerEditListOr(true, 0, false, ListId, true, 0);
              flag1 = true;
              if (this.e.ListOr[CurListOr].LevelCount > level)
              {
                strListLevelArray = this.e.ListOr[CurListOr].level;
                if (NumType == 23)
                {
                  strListLevelArray[level].NumType = NumType;
                  strListLevelArray[level].flags |= 17;
                }
              }
            }
            if (strListLevelArray != null)
            {
              int index3;
              if (this.e.InputFontId >= 0)
              {
                index3 = this.e.InputFontId;
              }
              else
              {
                index3 = this.GetCurCfmt(index1, 0);
                if ((this.e.TerFont[index3].style & 128 /*0x80*/) != 0)
                  index3 = 0;
              }
              string text;
              if (ListText == "")
              {
                if (NumType == 23)
                {
                  text = new string('\uF0B7', 1);
                  index3 = this.GetNewFont(this.e.TerGr, index3, "Symbol", this.e.TerFont[index3].TwipsSize, 0, this.e.TerFont[index3].TextColor, this.e.TerFont[index3].TextBkColor, this.e.TerFont[index3].UlineColor, 0, 0, 0, 1, 0, 0, 0, 0, (string) null, 0, (byte) 2, 0, this.e.TerFont[index3].TextAngle);
                }
                else
                  text = $"{TextBef}~{(level + 1).ToString()}~{TextAft}";
              }
              else
              {
                text = ListText;
                if (NumType == 23 && ListText.Length == 1)
                {
                  char ch = ListText[0];
                  string NewTypeFace = "";
                  int NewCharSet = 0;
                  if (ch == '\uF0B7')
                  {
                    NewTypeFace = "Symbol";
                    NewCharSet = 2;
                  }
                  else if (ch == '\uF0A7' || ch == '\uF0D8' || ch == '\uF0FC' || ch == '\uF076' || ch >= '\uF000' && ch <= '\uF0FF')
                  {
                    NewTypeFace = "Wingdings";
                    NewCharSet = 2;
                  }
                  if (NewTypeFace != "")
                    index3 = this.GetNewFont(this.e.TerGr, index3, NewTypeFace, this.e.TerFont[index3].TwipsSize, 0, this.e.TerFont[index3].TextColor, this.e.TerFont[index3].TextBkColor, this.e.TerFont[index3].UlineColor, 0, 0, 0, 1, 0, 0, 0, 0, (string) null, 0, (byte) (ushort) NewCharSet, 0, this.e.TerFont[index3].TextAngle);
                }
              }
              bool flag2 = this.e.ListOr[CurListOr].LevelCount > level;
              int flags3 = flag2 ? this.e.ListOr[CurListOr].level[level].flags : this.e.list[ListId].level[level].flags;
              this.TerEditListLevel(!flag2, flag2 ? CurListOr : ListId, level, start, NumType, 0, text, index3, flags3);
              if (flag1 || this.e.TerBlt[BltId].lvl != level)
                BltId = this.TerCreateListBulletId(CurListOr, level);
              num1 = BltId;
            }
            else
              continue;
          }
          else
            BltId = num1;
        }
        else
        {
          flags2 = tc.ResetUintFlag(ref flags1, 8);
          BltId = 0;
        }
        int LeftIndentTwips;
        int FirstIndentTwips;
        if (set)
        {
          int num2 = this.e.DefTabWidth / 2;
          if (num2 == 0)
            num2 = 270;
          while (num2 < 270)
            num2 *= 2;
          LeftIndentTwips = leftIndentTwips + num2 * (level + 1);
          FirstIndentTwips = -num2;
          if (ListText != null)
          {
            int num3 = ListText.Length / 6 * num2;
            if (num3 > 1440)
              num3 = 1440;
            LeftIndentTwips += num3;
            FirstIndentTwips -= num3;
          }
        }
        else
        {
          LeftIndentTwips = leftIndentTwips + firstIndentTwips;
          if (LeftIndentTwips < 0)
            LeftIndentTwips = 0;
          FirstIndentTwips = 0;
        }
        if (this.e.EditingParaStyle)
        {
          this.e.StyleId[this.e.CurSID].ParaFlags = flags2;
          this.e.StyleId[this.e.CurSID].LeftIndentTwips = LeftIndentTwips;
          this.e.StyleId[this.e.CurSID].FirstIndentTwips = FirstIndentTwips;
          this.e.StyleId[this.e.CurSID].BltId = BltId;
          this.DrawRuler(false);
          return true;
        }
        this.e.text[index1].pfmt = this.NewParaId(this.e.text[index1].pfmt, LeftIndentTwips, this.e.PfmtId[this.e.text[index1].pfmt].RightIndentTwips, FirstIndentTwips, this.e.PfmtId[this.e.text[index1].pfmt].TabId, BltId, this.e.PfmtId[this.e.text[index1].pfmt].AuxId, this.e.PfmtId[this.e.text[index1].pfmt].Aux1Id, this.e.PfmtId[this.e.text[index1].pfmt].StyId, this.e.PfmtId[this.e.text[index1].pfmt].shading, this.e.PfmtId[this.e.text[index1].pfmt].pflags, this.e.PfmtId[this.e.text[index1].pfmt].SpaceBefore, this.e.PfmtId[this.e.text[index1].pfmt].SpaceAfter, this.e.PfmtId[this.e.text[index1].pfmt].SpaceBetween, this.e.PfmtId[this.e.text[index1].pfmt].LineSpacing, this.e.PfmtId[this.e.text[index1].pfmt].BkColor, this.e.PfmtId[this.e.text[index1].pfmt].BorderSpace, this.e.PfmtId[this.e.text[index1].pfmt].flow, flags2);
      }
    }
    ++this.e.TerArg.modified;
    if (repaint)
    {
      this.e.TerRepaginate(true);
      if (this.e.RulerPending)
        this.DrawRuler(false);
    }
    return true;
  }

  internal bool TerSetListLevel(int level, int increment, bool repaint)
  {
    int num1 = 0;
    int num2 = 0;
    int num3 = 0;
    string StrText = (string) null;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.WordWrap)
      return false;
    int StartLine;
    int EndLine;
    this.GetParaRange(out StartLine, out EndLine);
    if (!this.e.EditingParaStyle)
      this.SaveUndo(StartLine, 0, EndLine, 0, 'P');
    for (int LineNo = StartLine; LineNo <= EndLine; ++LineNo)
    {
      if (this.LineSelected(LineNo))
      {
        int flags;
        int LeftIndentTwips;
        int FirstIndentTwips;
        int bltId1;
        if (this.e.EditingParaStyle)
        {
          flags = this.e.StyleId[this.e.CurSID].ParaFlags;
          LeftIndentTwips = this.e.StyleId[this.e.CurSID].LeftIndentTwips;
          FirstIndentTwips = this.e.StyleId[this.e.CurSID].FirstIndentTwips;
          bltId1 = this.e.StyleId[this.e.CurSID].BltId;
        }
        else
        {
          flags = this.e.PfmtId[this.e.text[LineNo].pfmt].flags;
          LeftIndentTwips = this.e.PfmtId[this.e.text[LineNo].pfmt].LeftIndentTwips;
          FirstIndentTwips = this.e.PfmtId[this.e.text[LineNo].pfmt].FirstIndentTwips;
          bltId1 = this.e.PfmtId[this.e.text[LineNo].pfmt].BltId;
        }
        if ((flags & 8) != 0 && this.e.TerBlt[bltId1].ls != 0)
        {
          int BltId;
          if (num3 == 0)
          {
            tc.StrListLevel pLevel1 = new tc.StrListLevel();
            tc.StrListLevel pLevel2 = new tc.StrListLevel();
            bool flag1 = false;
            this.GetListLevelPtr(this.e.TerBlt[bltId1].ls, this.e.TerBlt[bltId1].lvl, out pLevel1);
            if (level == -1)
              level = this.e.TerBlt[bltId1].lvl + increment;
            if (level < 0)
              level = 0;
            if (level > 8)
              level = 8;
            this.GetListLevelPtr(this.e.TerBlt[bltId1].ls, level, out pLevel1);
            if (LineNo > 0)
              this.CheckLinePLevel(LineNo - 1, level, ref pLevel1, ref tc.SkipInt);
            if (LineNo + 1 < this.e.TotalLines)
              this.CheckLinePLevel(LineNo + 1, level, ref pLevel1, ref tc.SkipInt);
            int ListOrId = this.e.TerBlt[bltId1].ls;
            int listIdx = this.e.ListOr[ListOrId].ListIdx;
            if (this.e.ListOr[ListOrId].LevelCount > level)
            {
              pLevel2 = this.e.ListOr[ListOrId].level[level].Copy();
              flag1 = true;
            }
            else if (this.e.list[listIdx].LevelCount > level)
            {
              pLevel2 = this.e.list[listIdx].level[level].Copy();
              flag1 = true;
            }
            if (LineNo > 0 && this.CheckLinePLevel(LineNo - 1, level, ref pLevel2, ref ListOrId))
              flag1 = true;
            if (LineNo + 1 < this.e.TotalLines && this.CheckLinePLevel(LineNo + 1, level, ref pLevel2, ref ListOrId))
              flag1 = true;
            if (flag1 && pLevel2.NumType != pLevel1.NumType)
              flag1 = false;
            if (!flag1)
            {
              for (int index = 0; index < this.e.TotalListOr; ++index)
              {
                if (this.e.ListOr[index].ListIdx == listIdx && this.e.ListOr[index].LevelCount > level && this.e.ListOr[index].level[level].NumType == pLevel1.NumType)
                {
                  ListOrId = index;
                  pLevel2 = this.e.ListOr[ListOrId].level[level].Copy();
                  flag1 = true;
                  break;
                }
              }
            }
            if (!flag1)
            {
              ListOrId = this.e.TerEditListOr(true, 0, false, listIdx, true, 0);
              if (this.e.ListOr[ListOrId].LevelCount > level)
              {
                pLevel2 = this.e.ListOr[ListOrId].level[level].Copy();
                if (pLevel1.NumType == 23)
                {
                  pLevel2 = pLevel1.Copy();
                  pLevel2.flags |= 17;
                }
                flag1 = true;
              }
            }
            if (flag1)
            {
              this.DecodeListText(pLevel2.text, out StrText);
              BltId = this.TerCreateListBulletId(ListOrId, level);
              bool flag2 = false;
              for (int index = LineNo - 1; index >= 0; --index)
              {
                int bltId2 = this.e.PfmtId[this.e.text[index].pfmt].BltId;
                if (this.e.TerBlt[bltId2].ls != 0)
                {
                  if (this.e.TerBlt[bltId2].lvl == level)
                  {
                    LeftIndentTwips = this.e.PfmtId[this.e.text[index].pfmt].LeftIndentTwips;
                    FirstIndentTwips = this.e.PfmtId[this.e.text[index].pfmt].FirstIndentTwips;
                    flag2 = true;
                    break;
                  }
                }
                else
                  break;
              }
              if (!flag2)
              {
                int num4 = this.e.DefTabWidth / 2;
                if (num4 == 0)
                  num4 = 270;
                while (num4 < 270)
                  num4 *= 2;
                LeftIndentTwips += num4 * increment;
                if (LeftIndentTwips < 0)
                  LeftIndentTwips = 0;
              }
              num3 = BltId;
              num1 = LeftIndentTwips;
              num2 = FirstIndentTwips;
            }
            else
              continue;
          }
          else
          {
            BltId = num3;
            FirstIndentTwips = num2;
            LeftIndentTwips = num1;
          }
          if (this.e.EditingParaStyle)
          {
            this.e.StyleId[this.e.CurSID].ParaFlags = flags;
            this.e.StyleId[this.e.CurSID].LeftIndentTwips = LeftIndentTwips;
            this.e.StyleId[this.e.CurSID].FirstIndentTwips = FirstIndentTwips;
            this.e.StyleId[this.e.CurSID].BltId = BltId;
            this.DrawRuler(false);
            return true;
          }
          this.e.text[LineNo].pfmt = this.NewParaId(this.e.text[LineNo].pfmt, LeftIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].RightIndentTwips, FirstIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].TabId, BltId, this.e.PfmtId[this.e.text[LineNo].pfmt].AuxId, this.e.PfmtId[this.e.text[LineNo].pfmt].Aux1Id, this.e.PfmtId[this.e.text[LineNo].pfmt].StyId, this.e.PfmtId[this.e.text[LineNo].pfmt].shading, this.e.PfmtId[this.e.text[LineNo].pfmt].pflags, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceBefore, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceAfter, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceBetween, this.e.PfmtId[this.e.text[LineNo].pfmt].LineSpacing, this.e.PfmtId[this.e.text[LineNo].pfmt].BkColor, this.e.PfmtId[this.e.text[LineNo].pfmt].BorderSpace, this.e.PfmtId[this.e.text[LineNo].pfmt].flow, flags);
        }
      }
    }
    ++this.e.TerArg.modified;
    if (repaint)
    {
      this.RequestPagination(true);
      this.PaintTer();
      if (this.e.RulerPending)
        this.DrawRuler(false);
    }
    return true;
  }

  internal bool TerSetNextParaAux1Id(int Aux1Id)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    this.e.NextParaAux1Id = Aux1Id;
    return true;
  }

  internal bool TerSetParaAuxId(int FirstLine, int LastLine, int AuxId)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.WordWrap)
      return false;
    if (FirstLine < 0)
      this.GetParaRange(out FirstLine, out LastLine);
    if (!this.e.EditingParaStyle)
      this.SaveUndo(FirstLine, 0, LastLine, 0, 'P');
    for (int LineNo = FirstLine; LineNo <= LastLine; ++LineNo)
    {
      if (this.LineSelected(LineNo))
        this.e.text[LineNo].pfmt = this.NewParaId(this.e.text[LineNo].pfmt, this.e.PfmtId[this.e.text[LineNo].pfmt].LeftIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].RightIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].FirstIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].TabId, this.e.PfmtId[this.e.text[LineNo].pfmt].BltId, AuxId, this.e.PfmtId[this.e.text[LineNo].pfmt].Aux1Id, this.e.PfmtId[this.e.text[LineNo].pfmt].StyId, this.e.PfmtId[this.e.text[LineNo].pfmt].shading, this.e.PfmtId[this.e.text[LineNo].pfmt].pflags, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceBefore, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceAfter, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceBetween, this.e.PfmtId[this.e.text[LineNo].pfmt].LineSpacing, this.e.PfmtId[this.e.text[LineNo].pfmt].BkColor, this.e.PfmtId[this.e.text[LineNo].pfmt].BorderSpace, this.e.PfmtId[this.e.text[LineNo].pfmt].flow, this.e.PfmtId[this.e.text[LineNo].pfmt].flags);
    }
    ++this.e.TerArg.modified;
    return true;
  }

  internal bool TerSetParaBkColor(bool dialog, Color color, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.WordWrap)
      return false;
    int StartLine;
    int EndLine;
    this.GetParaRange(out StartLine, out EndLine);
    if (!this.e.EditingParaStyle)
      this.SaveUndo(StartLine, 0, EndLine, 0, 'P');
    if (dialog)
    {
      color = !this.e.EditingParaStyle ? this.e.PfmtId[this.e.text[StartLine].pfmt].BkColor : this.e.StyleId[this.e.CurSID].ParaBkColor;
      color = this.DlgEditColor((Control) this.e, color, false);
      if (this.e.DlgCancel)
        return false;
    }
    ++this.e.TerArg.modified;
    for (int LineNo = StartLine; LineNo <= EndLine; ++LineNo)
    {
      if (this.LineSelected(LineNo))
      {
        if (this.e.EditingParaStyle)
        {
          this.e.StyleId[this.e.CurSID].ParaBkColor = color;
          return true;
        }
        this.e.text[LineNo].pfmt = this.NewParaId(this.e.text[LineNo].pfmt, this.e.PfmtId[this.e.text[LineNo].pfmt].LeftIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].RightIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].FirstIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].TabId, this.e.PfmtId[this.e.text[LineNo].pfmt].BltId, this.e.PfmtId[this.e.text[LineNo].pfmt].AuxId, this.e.PfmtId[this.e.text[LineNo].pfmt].Aux1Id, this.e.PfmtId[this.e.text[LineNo].pfmt].StyId, this.e.PfmtId[this.e.text[LineNo].pfmt].shading, this.e.PfmtId[this.e.text[LineNo].pfmt].pflags, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceBefore, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceAfter, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceBetween, this.e.PfmtId[this.e.text[LineNo].pfmt].LineSpacing, color, this.e.PfmtId[this.e.text[LineNo].pfmt].BorderSpace, this.e.PfmtId[this.e.text[LineNo].pfmt].flow, this.e.PfmtId[this.e.text[LineNo].pfmt].flags);
      }
    }
    if (repaint)
      this.PaintTer();
    return true;
  }

  internal bool TerSetParaBorderColor(Color color, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.WordWrap)
      return false;
    int StartLine;
    int EndLine;
    this.GetParaRange(out StartLine, out EndLine);
    if (!this.e.EditingParaStyle)
      this.SaveUndo(StartLine, 0, EndLine, 0, 'P');
    ++this.e.TerArg.modified;
    for (int LineNo = StartLine; LineNo <= EndLine; ++LineNo)
    {
      if (this.LineSelected(LineNo))
      {
        if (this.e.EditingParaStyle)
        {
          this.e.StyleId[this.e.CurSID].ParaBorderColor = color;
          return true;
        }
        tc.StrPfmt pNew = this.e.PfmtId[this.e.text[LineNo].pfmt].Copy() with
        {
          BorderColor = color
        };
        this.e.text[LineNo].pfmt = this.NewParaId2(this.e.text[LineNo].pfmt, pNew);
      }
    }
    if (repaint)
      this.PaintTer();
    return true;
  }

  internal bool TerSetParaId(int FirstLine, int LastLine, int ParaId)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.WordWrap || ParaId < 0 || ParaId >= this.e.TotalPfmts)
      return false;
    if (FirstLine < 0)
      this.GetParaRange(out FirstLine, out LastLine);
    if (!this.e.EditingParaStyle)
      this.SaveUndo(FirstLine, 0, LastLine, 0, 'P');
    for (int index = FirstLine; index <= LastLine; ++index)
      this.e.text[index].pfmt = ParaId;
    ++this.e.TerArg.modified;
    return true;
  }

  internal bool TerSetParaIndent(int left, int right, int first, bool repaint)
  {
    int num1 = 0;
    int num2 = 0;
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.WordWrap)
      return false;
    int StartLine;
    int EndLine;
    this.GetParaRange(out StartLine, out EndLine);
    if (!this.e.EditingParaStyle)
      this.SaveUndo(StartLine, 0, EndLine, 0, 'P');
    for (int LineNo = StartLine; LineNo <= EndLine; ++LineNo)
    {
      if (this.LineSelected(LineNo))
      {
        int val1 = left;
        int val2 = right;
        int val3 = first;
        if (this.e.SnapToGrid)
        {
          int step = (this.e.TerFlags & 2) == 0 ? 90 : 71;
          val1 = this.RoundInt(val1, step);
          val2 = this.RoundInt(val2, step);
          val3 = this.RoundInt(val3, step);
        }
        if (this.e.EditingParaStyle)
        {
          if (left != -1)
            this.e.StyleId[this.e.CurSID].LeftIndentTwips = val1;
          if (right != -1)
            this.e.StyleId[this.e.CurSID].RightIndentTwips = val2;
          if (first != -1)
            this.e.StyleId[this.e.CurSID].FirstIndentTwips = val3;
          this.DrawRuler(false);
          return true;
        }
        if ((this.e.TerFlags2 & 67108864 /*0x04000000*/) != 0 && left != 0 && left != -1 && this.e.text[LineNo].fid > 0)
        {
          if (this.e.text[LineNo].fid != num1)
          {
            int fid = this.e.text[LineNo].fid;
            int x = this.e.ParaFrame[fid].x;
            this.e.ParaFrame[fid].x = left;
            if (x >= 0 && this.e.ParaFrame[fid].x < 0)
              this.e.ParaFrame[fid].x = 0;
            num1 = fid;
          }
        }
        else if ((this.e.TerFlags2 & 134217728 /*0x08000000*/) != 0 && left != 0 && left != -1 && this.e.text[LineNo].cid > 0)
        {
          int row = this.e.cell[this.e.text[LineNo].cid].row;
          if (row != num2)
          {
            int indent1 = this.e.TableRow[row].indent;
            this.e.TableRow[row].indent = left;
            if (indent1 >= 0 && this.e.TableRow[row].indent < 0)
              this.e.TableRow[row].indent = 0;
            int index = this.e.TableRow[row].FirstCell;
            int indent2 = this.e.TableRow[row].indent;
            for (; index > 0; index = this.e.cell[index].NextCell)
            {
              this.e.cell[index].x = indent2;
              indent2 += this.e.cell[index].width;
            }
            num2 = row;
          }
        }
        else
          this.e.text[LineNo].pfmt = this.NewParaId(this.e.text[LineNo].pfmt, left == -1 ? this.e.PfmtId[this.e.text[LineNo].pfmt].LeftIndentTwips : val1, right == -1 ? this.e.PfmtId[this.e.text[LineNo].pfmt].RightIndentTwips : val2, first == -1 ? this.e.PfmtId[this.e.text[LineNo].pfmt].FirstIndentTwips : val3, this.e.PfmtId[this.e.text[LineNo].pfmt].TabId, this.e.PfmtId[this.e.text[LineNo].pfmt].BltId, this.e.PfmtId[this.e.text[LineNo].pfmt].AuxId, this.e.PfmtId[this.e.text[LineNo].pfmt].Aux1Id, this.e.PfmtId[this.e.text[LineNo].pfmt].StyId, this.e.PfmtId[this.e.text[LineNo].pfmt].shading, this.e.PfmtId[this.e.text[LineNo].pfmt].pflags, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceBefore, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceAfter, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceBetween, this.e.PfmtId[this.e.text[LineNo].pfmt].LineSpacing, this.e.PfmtId[this.e.text[LineNo].pfmt].BkColor, this.e.PfmtId[this.e.text[LineNo].pfmt].BorderSpace, this.e.PfmtId[this.e.text[LineNo].pfmt].flow, this.e.PfmtId[this.e.text[LineNo].pfmt].flags);
      }
    }
    ++this.e.TerArg.modified;
    if (!this.e.EditingParaStyle && StartLine < this.e.RepageBeginLine)
      this.e.RepageBeginLine = StartLine;
    if (repaint)
      this.PaintTer();
    else
      this.WordWrap(StartLine, EndLine + 10 - StartLine);
    return true;
  }

  internal bool TerSetParaList(bool dialog, int ParaId, int CurListOr, int level, bool repaint)
  {
    int StartLine = 0;
    int EndLine = 0;
    bool flag = ParaId >= 0;
    new tc.StrBlt().init();
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.WordWrap)
      return false;
    int bltId;
    if (flag)
    {
      if (ParaId >= this.e.TotalPfmts)
        return false;
      bltId = this.e.PfmtId[ParaId].BltId;
    }
    else
    {
      this.GetParaRange(out StartLine, out EndLine);
      if (this.e.EditingParaStyle)
      {
        bltId = this.e.StyleId[this.e.CurSID].BltId;
      }
      else
      {
        ParaId = this.e.text[StartLine].pfmt;
        bltId = this.e.PfmtId[ParaId].BltId;
      }
    }
    if (dialog)
    {
      this.e.DlgInt1 = this.e.DlgInt2 = 0;
      if (bltId > 0)
      {
        this.e.DlgInt1 = this.e.TerBlt[bltId].ls;
        this.e.DlgInt2 = this.e.TerBlt[bltId].lvl;
      }
      if (!this.CallDialogBox((Form) new terdlg_list_para(this.e)))
        return true;
      CurListOr = this.e.DlgInt1;
      level = this.e.DlgInt2;
    }
    if (CurListOr < 0 || CurListOr >= this.e.TotalListOr)
      return false;
    if (CurListOr > 0)
    {
      int levelCount = this.e.ListOr[CurListOr].LevelCount;
      if (levelCount == 0)
        levelCount = this.e.list[this.e.ListOr[CurListOr].ListIdx].LevelCount;
      if (level < 0 || level >= levelCount)
        return false;
    }
    ++this.e.TerArg.modified;
    int BltId;
    if (CurListOr == 0)
      BltId = 0;
    else
      BltId = this.NewBltId(0, this.e.TerBlt[0] with
      {
        IsBullet = false,
        ls = CurListOr,
        lvl = level
      });
    if (flag)
    {
      this.e.PfmtId[ParaId].BltId = BltId;
      tc.ResetUintFlag(ref this.e.PfmtId[ParaId].flags, 8);
      if (this.True(CurListOr))
        this.e.PfmtId[ParaId].flags |= 8;
      if (!repaint)
        return true;
    }
    else
    {
      if (!this.e.EditingParaStyle)
        this.SaveUndo(StartLine, 0, EndLine, 0, 'P');
      for (int LineNo = StartLine; LineNo <= EndLine; ++LineNo)
      {
        if (this.LineSelected(LineNo))
        {
          int flags1 = !this.e.EditingParaStyle ? this.e.PfmtId[this.e.text[LineNo].pfmt].flags : this.e.StyleId[this.e.CurSID].ParaFlags;
          int flags2 = CurListOr <= 0 ? tc.ResetUintFlag(ref flags1, 8) : flags1 | 8;
          if (this.e.EditingParaStyle)
          {
            this.e.StyleId[this.e.CurSID].ParaFlags = flags2;
            this.e.StyleId[this.e.CurSID].BltId = BltId;
            this.DrawRuler(false);
            return true;
          }
          this.e.text[LineNo].pfmt = this.NewParaId(this.e.text[LineNo].pfmt, this.e.PfmtId[this.e.text[LineNo].pfmt].LeftIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].RightIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].FirstIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].TabId, BltId, this.e.PfmtId[this.e.text[LineNo].pfmt].AuxId, this.e.PfmtId[this.e.text[LineNo].pfmt].Aux1Id, this.e.PfmtId[this.e.text[LineNo].pfmt].StyId, this.e.PfmtId[this.e.text[LineNo].pfmt].shading, this.e.PfmtId[this.e.text[LineNo].pfmt].pflags, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceBefore, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceAfter, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceBetween, this.e.PfmtId[this.e.text[LineNo].pfmt].LineSpacing, this.e.PfmtId[this.e.text[LineNo].pfmt].BkColor, this.e.PfmtId[this.e.text[LineNo].pfmt].BorderSpace, this.e.PfmtId[this.e.text[LineNo].pfmt].flow, flags2);
        }
      }
    }
    if (repaint)
    {
      this.RequestPagination(true);
      this.PaintTer();
    }
    return true;
  }

  internal bool TerSetParaShading(int shading, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.WordWrap)
      return false;
    if (shading < 0)
      shading = 0;
    if (shading > 10000)
      shading = 10000;
    if (this.e.EditingParaStyle)
    {
      this.e.StyleId[this.e.CurSID].shading = shading;
      return true;
    }
    int StartLine;
    int EndLine;
    this.GetParaRange(out StartLine, out EndLine);
    if (!this.e.EditingParaStyle)
      this.SaveUndo(StartLine, 0, EndLine, 0, 'P');
    for (int LineNo = StartLine; LineNo <= EndLine; ++LineNo)
    {
      if (this.LineSelected(LineNo))
        this.e.text[LineNo].pfmt = this.NewParaId(this.e.text[LineNo].pfmt, this.e.PfmtId[this.e.text[LineNo].pfmt].LeftIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].RightIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].FirstIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].TabId, this.e.PfmtId[this.e.text[LineNo].pfmt].BltId, this.e.PfmtId[this.e.text[LineNo].pfmt].AuxId, this.e.PfmtId[this.e.text[LineNo].pfmt].Aux1Id, this.e.PfmtId[this.e.text[LineNo].pfmt].StyId, shading, this.e.PfmtId[this.e.text[LineNo].pfmt].pflags, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceBefore, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceAfter, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceBetween, this.e.PfmtId[this.e.text[LineNo].pfmt].LineSpacing, this.e.PfmtId[this.e.text[LineNo].pfmt].BkColor, this.e.PfmtId[this.e.text[LineNo].pfmt].BorderSpace, this.e.PfmtId[this.e.text[LineNo].pfmt].flow, this.e.PfmtId[this.e.text[LineNo].pfmt].flags);
    }
    ++this.e.TerArg.modified;
    if (repaint)
    {
      this.PaintTer();
      if (this.e.RulerPending)
        this.DrawRuler(false);
    }
    else
      this.WordWrap(StartLine, EndLine + 10 - StartLine);
    return true;
  }

  internal bool TerSetParaSpacing(int SpaceBefore, int SpaceAfter, int SpaceBetween, bool repaint)
  {
    return this.TerSetParaSpacing2(SpaceBefore, SpaceAfter, SpaceBetween, 0, repaint);
  }

  internal bool TerSetParaSpacing2(
    int NewSpaceBefore,
    int NewSpaceAfter,
    int NewSpaceBetween,
    int NewLineSpacing,
    bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.WordWrap)
      return false;
    if (NewSpaceBefore > 5000)
      NewSpaceBefore = 5000;
    if (NewSpaceAfter > 5000)
      NewSpaceAfter = 5000;
    if (NewSpaceBetween < -2000 && NewSpaceBetween != -9999)
      NewSpaceBetween = -2000;
    if (NewSpaceBetween > 2000)
      NewSpaceBetween = 2000;
    int StartLine;
    int EndLine;
    this.GetParaRange(out StartLine, out EndLine);
    if (!this.e.EditingParaStyle)
      this.SaveUndo(StartLine, 0, EndLine, 0, 'P');
    for (int LineNo = StartLine; LineNo <= EndLine; ++LineNo)
    {
      if (this.LineSelected(LineNo))
      {
        int flags;
        int SpaceBefore;
        int SpaceAfter;
        int num1;
        int num2;
        if (this.e.EditingParaStyle)
        {
          flags = this.e.StyleId[this.e.CurSID].ParaFlags;
          SpaceBefore = NewSpaceBefore >= 0 ? NewSpaceBefore : this.e.StyleId[this.e.CurSID].SpaceBefore;
          SpaceAfter = NewSpaceAfter >= 0 ? NewSpaceAfter : this.e.StyleId[this.e.CurSID].SpaceAfter;
          num1 = NewSpaceBetween != -9999 ? NewSpaceBetween : this.e.StyleId[this.e.CurSID].SpaceBetween;
          num2 = NewLineSpacing >= 0 ? NewLineSpacing : this.e.StyleId[this.e.CurSID].LineSpacing;
        }
        else
        {
          flags = this.e.PfmtId[this.e.text[LineNo].pfmt].flags;
          SpaceBefore = NewSpaceBefore >= 0 ? NewSpaceBefore : this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceBefore;
          SpaceAfter = NewSpaceAfter >= 0 ? NewSpaceAfter : this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceAfter;
          num1 = NewSpaceBetween != -9999 ? NewSpaceBetween : this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceBetween;
          num2 = NewLineSpacing >= 0 ? NewLineSpacing : this.e.PfmtId[this.e.text[LineNo].pfmt].LineSpacing;
        }
        if (num1 != 0)
          num2 = 0;
        if (num2 != 0)
          num1 = 0;
        if (this.True(num1) || this.True(num2))
          flags = tc.ResetUintFlag(ref flags, 4);
        if (this.e.EditingParaStyle)
        {
          this.e.StyleId[this.e.CurSID].SpaceBefore = SpaceBefore;
          this.e.StyleId[this.e.CurSID].SpaceAfter = SpaceAfter;
          this.e.StyleId[this.e.CurSID].SpaceBetween = num1;
          this.e.StyleId[this.e.CurSID].LineSpacing = num2;
          this.e.StyleId[this.e.CurSID].ParaFlags = flags;
          return true;
        }
        this.e.text[LineNo].pfmt = this.NewParaId(this.e.text[LineNo].pfmt, this.e.PfmtId[this.e.text[LineNo].pfmt].LeftIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].RightIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].FirstIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].TabId, this.e.PfmtId[this.e.text[LineNo].pfmt].BltId, this.e.PfmtId[this.e.text[LineNo].pfmt].AuxId, this.e.PfmtId[this.e.text[LineNo].pfmt].Aux1Id, this.e.PfmtId[this.e.text[LineNo].pfmt].StyId, this.e.PfmtId[this.e.text[LineNo].pfmt].shading, this.e.PfmtId[this.e.text[LineNo].pfmt].pflags, SpaceBefore, SpaceAfter, num1, num2, this.e.PfmtId[this.e.text[LineNo].pfmt].BkColor, this.e.PfmtId[this.e.text[LineNo].pfmt].BorderSpace, this.e.PfmtId[this.e.text[LineNo].pfmt].flow, flags);
      }
    }
    ++this.e.TerArg.modified;
    if (repaint)
    {
      this.PaintTer();
      if (this.e.RulerPending)
        this.DrawRuler(false);
    }
    else
      this.WordWrap(StartLine, EndLine + 10 - StartLine);
    return true;
  }

  internal bool TerSetParaTextFlow(bool dialog, int flow, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.WordWrap || this.e.EditingParaStyle)
      return false;
    int StartLine;
    int EndLine;
    this.GetParaRange(out StartLine, out EndLine);
    if (dialog)
    {
      this.e.DlgInt1 = this.e.PfmtId[this.e.text[StartLine].pfmt].flow;
      this.e.DlgText1 = "Paragraph Text Flow";
      if (!this.CallDialogBox((Form) new terdlg_para_text_flow(this.e)))
        return true;
      flow = this.e.DlgInt1;
    }
    if (flow == 0 || flow == 2 || flow == 1)
    {
      if (!this.e.EditingParaStyle)
        this.SaveUndo(StartLine, 0, EndLine, 0, 'P');
      for (int LineNo = StartLine; LineNo <= EndLine; ++LineNo)
      {
        if (this.LineSelected(LineNo))
          this.e.text[LineNo].pfmt = this.NewParaId(this.e.text[LineNo].pfmt, this.e.PfmtId[this.e.text[LineNo].pfmt].LeftIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].RightIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].FirstIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].TabId, this.e.PfmtId[this.e.text[LineNo].pfmt].BltId, this.e.PfmtId[this.e.text[LineNo].pfmt].AuxId, this.e.PfmtId[this.e.text[LineNo].pfmt].Aux1Id, this.e.PfmtId[this.e.text[LineNo].pfmt].StyId, this.e.PfmtId[this.e.text[LineNo].pfmt].shading, this.e.PfmtId[this.e.text[LineNo].pfmt].pflags, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceBefore, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceAfter, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceBetween, this.e.PfmtId[this.e.text[LineNo].pfmt].LineSpacing, this.e.PfmtId[this.e.text[LineNo].pfmt].BkColor, this.e.PfmtId[this.e.text[LineNo].pfmt].BorderSpace, flow, this.e.PfmtId[this.e.text[LineNo].pfmt].flags);
      }
      if (repaint)
      {
        this.RequestPagination(true);
        this.PaintTer();
      }
    }
    return true;
  }

  internal bool TerSetPflags(int FmtType, bool OnOff, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.WordWrap)
      return false;
    FmtType &= 65520;
    if (FmtType == 0)
      return false;
    int StartLine;
    int EndLine;
    this.GetParaRange(out StartLine, out EndLine);
    if (!this.e.EditingParaStyle)
      this.SaveUndo(StartLine, 0, EndLine, 0, 'P');
    for (int LineNo = StartLine; LineNo <= EndLine; ++LineNo)
    {
      if (this.LineSelected(LineNo))
      {
        int num = !this.e.EditingParaStyle ? this.e.PfmtId[this.e.text[LineNo].pfmt].pflags : this.e.StyleId[this.e.CurSID].pflags;
        int pflags = !OnOff ? num & ~FmtType : num | FmtType;
        if (this.e.EditingParaStyle)
        {
          this.e.StyleId[this.e.CurSID].pflags = pflags;
          return true;
        }
        this.e.text[LineNo].pfmt = this.NewParaId(this.e.text[LineNo].pfmt, this.e.PfmtId[this.e.text[LineNo].pfmt].LeftIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].RightIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].FirstIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].TabId, this.e.PfmtId[this.e.text[LineNo].pfmt].BltId, this.e.PfmtId[this.e.text[LineNo].pfmt].AuxId, this.e.PfmtId[this.e.text[LineNo].pfmt].Aux1Id, this.e.PfmtId[this.e.text[LineNo].pfmt].StyId, this.e.PfmtId[this.e.text[LineNo].pfmt].shading, pflags, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceBefore, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceAfter, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceBetween, this.e.PfmtId[this.e.text[LineNo].pfmt].LineSpacing, this.e.PfmtId[this.e.text[LineNo].pfmt].BkColor, this.e.PfmtId[this.e.text[LineNo].pfmt].BorderSpace, this.e.PfmtId[this.e.text[LineNo].pfmt].flow, this.e.PfmtId[this.e.text[LineNo].pfmt].flags);
      }
    }
    ++this.e.TerArg.modified;
    if (this.e.TerArg.PrintView && (FmtType & 32 /*0x20*/) != 0 && this.e.TotalLines < 5000)
      this.Repaginate(false, false, 0, true);
    else
      this.e.RepageBeginLine = StartLine;
    if (repaint)
      this.PaintTer();
    else
      this.WordWrap(StartLine, EndLine + 10 - StartLine);
    return true;
  }

  internal bool TerSetStyleParam(int id, int type, int IntParam, string TextParam, bool repaint)
  {
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (id < 0 || id >= this.e.TotalSID || !this.e.StyleId[id].InUse)
      return false;
    switch (type)
    {
      case 1:
        this.e.StyleId[id].name = TextParam;
        break;
      case 4:
        int index = IntParam;
        if (index < 0 || index >= this.e.TotalSID || !this.e.StyleId[index].InUse)
          return false;
        this.e.StyleId[id].next = index;
        break;
      default:
        return false;
    }
    ++this.e.TerArg.modified;
    if (repaint)
      this.UpdateToolBar(true);
    return true;
  }

  internal bool TerSetTab(int type, int pos, byte flags, bool repaint)
  {
    int num = 30;
    tc.StrTab strTab = new tc.StrTab();
    if (!this.e.IsHandleCreated)
      this.e.TerCreateControl();
    if (!this.e.TerArg.WordWrap)
      return false;
    if (pos < 0)
    {
      if (!this.CallDialogBox((Form) new terdlg_set_tab(this.e)))
        return false;
      pos = this.e.DlgInt1;
      type = this.e.DlgInt2;
      flags = (byte) this.e.DlgInt3;
    }
    if (pos < 0)
      pos = 0;
    if (type != 0 && type != 1 && type != 2 && type != 3)
      type = 0;
    int StartLine;
    int EndLine;
    this.GetParaRange(out StartLine, out EndLine);
    if (!this.e.EditingParaStyle)
      this.SaveUndo(StartLine, 0, EndLine, 0, 'P');
    for (int LineNo = StartLine; LineNo <= EndLine; ++LineNo)
    {
      if (this.LineSelected(LineNo))
      {
        int old = !this.e.EditingParaStyle ? this.e.PfmtId[this.e.text[LineNo].pfmt].TabId : this.e.StyleId[this.e.CurSID].TabId;
        tc.StrTab TabRec = this.e.TerTab[old];
        if (TabRec.count == 20)
          return this.PrintError(114, "Set Tab");
        int index1 = 0;
        while (index1 < TabRec.count && Math.Abs(TabRec.pos[index1] - pos) > num && TabRec.pos[index1] <= pos)
          ++index1;
        int index2;
        if (index1 == TabRec.count)
        {
          index2 = TabRec.count;
          ++TabRec.count;
        }
        else
        {
          index2 = index1;
          if (Math.Abs(TabRec.pos[index2] - pos) > num)
          {
            for (int index3 = TabRec.count - 1; index3 >= index2; --index3)
            {
              TabRec.pos[index3 + 1] = TabRec.pos[index3];
              TabRec.type[index3 + 1] = TabRec.type[index3];
              TabRec.flags[index3 + 1] = TabRec.flags[index3];
            }
            ++TabRec.count;
          }
        }
        TabRec.pos[index2] = pos;
        TabRec.type[index2] = type;
        TabRec.flags[index2] = flags;
        int TabId = this.NewTabId(old, TabRec);
        if (this.e.EditingParaStyle)
        {
          this.e.StyleId[this.e.CurSID].TabId = TabId;
          this.DrawRuler(false);
          return true;
        }
        this.e.text[LineNo].pfmt = this.NewParaId(this.e.text[LineNo].pfmt, this.e.PfmtId[this.e.text[LineNo].pfmt].LeftIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].RightIndentTwips, this.e.PfmtId[this.e.text[LineNo].pfmt].FirstIndentTwips, TabId, this.e.PfmtId[this.e.text[LineNo].pfmt].BltId, this.e.PfmtId[this.e.text[LineNo].pfmt].AuxId, this.e.PfmtId[this.e.text[LineNo].pfmt].Aux1Id, this.e.PfmtId[this.e.text[LineNo].pfmt].StyId, this.e.PfmtId[this.e.text[LineNo].pfmt].shading, this.e.PfmtId[this.e.text[LineNo].pfmt].pflags, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceBefore, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceAfter, this.e.PfmtId[this.e.text[LineNo].pfmt].SpaceBetween, this.e.PfmtId[this.e.text[LineNo].pfmt].LineSpacing, this.e.PfmtId[this.e.text[LineNo].pfmt].BkColor, this.e.PfmtId[this.e.text[LineNo].pfmt].BorderSpace, this.e.PfmtId[this.e.text[LineNo].pfmt].flow, this.e.PfmtId[this.e.text[LineNo].pfmt].flags);
      }
    }
    ++this.e.TerArg.modified;
    if (!this.e.EditingParaStyle && StartLine < this.e.RepageBeginLine)
      this.e.RepageBeginLine = StartLine;
    if (repaint)
    {
      this.PaintTer();
      if (this.e.RulerPending)
        this.DrawRuler(false);
    }
    else
      this.WordWrap(StartLine, EndLine + 10 - StartLine);
    return true;
  }
}
