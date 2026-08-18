// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.CSpl
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Controls.SpellCheck;
using Intermech.Document.UI;
using System;
using System.Reflection;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class CSpl : COp
{
  private string[] AltWord;
  private int WordIndex;
  private int WordLen;

  internal CSpl(ImRtfEditor ImRtfEditor)
    : base(ImRtfEditor)
  {
    this.AltWord = new string[12];
  }

  internal new bool AutoSpellCheck(int LineNo)
  {
    int num1 = 0;
    int num2 = -1;
    int num3 = -1;
    string OutCurWord = "";
    bool flag1 = false;
    int len = this.e.text[LineNo].len;
    if (len != 0)
    {
      ushort[] numArray = this.OpenCfmt(LineNo);
      int num4 = -1;
      for (int index = 0; index < len; ++index)
      {
        int CurFont = (int) numArray[index];
        if ((this.e.TerFont[CurFont].flags & 1024 /*0x0400*/) != 0)
        {
          if (CurFont != num4)
          {
            num1 = this.SetFontFlags(CurFont, 1024 /*0x0400*/, false);
            num4 = CurFont;
          }
          numArray[index] = (ushort) num1;
        }
      }
      string CurLine = new string(this.e.text[LineNo].txt, 0, this.e.text[LineNo].len);
      int CurIndex;
      int WordIndex = CurIndex = 0;
      int line;
      while ((line = this.StParseLine(CurLine, ref OutCurWord, ref WordIndex, ref CurIndex, this.e.text[LineNo].len)) > 0)
      {
        int length1 = OutCurWord.Length;
        int length2;
        for (length2 = 0; length2 < length1; ++length2)
        {
          int CurFont = (int) numArray[WordIndex + length2];
          if (CurFont != 0 && CurFont != num3)
          {
            if (CurFont != num2)
            {
              if (this.e.TerFont[CurFont].FieldId == 6 || this.edit.HiddenText(CurFont) || (this.e.TerFont[CurFont].style & 688) != 0)
              {
                num2 = CurFont;
                break;
              }
              num3 = CurFont;
            }
            else
              break;
          }
        }
        if (length2 > 0)
        {
          OutCurWord = OutCurWord.Substring(0, length2);
          CurIndex -= line - length2;
          int num5 = length2;
          if (this.e.EditLine >= 0)
          {
            int num6 = this.e.EditLine != LineNo || this.e.EditCol < WordIndex ? 0 : (this.e.EditCol <= WordIndex + num5 ? 1 : 0);
            bool flag2 = this.e.CurLine == LineNo && this.e.CurCol >= WordIndex && this.e.CurCol <= WordIndex + num5;
            if (num6 != 0)
            {
              if (flag2)
              {
                flag1 = true;
                this.e.EditWordIndex = WordIndex;
                this.e.EditWordLen = num5;
                continue;
              }
              this.e.EditLine = -1;
            }
          }
          int ResultCode;
          if (!this.SpellWord(OutCurWord, 0, out ResultCode))
          {
            if ((ResultCode & tc.ST_ERROR) == 0)
            {
              int num7 = -1;
              for (int index = 0; index < num5; ++index)
              {
                int CurFont = (int) numArray[WordIndex + index];
                if (CurFont != num7)
                {
                  num1 = this.SetFontFlags(CurFont, 1024 /*0x0400*/, true);
                  num7 = CurFont;
                }
                numArray[WordIndex + index] = (ushort) num1;
              }
            }
            else
              break;
          }
        }
      }
      this.CloseCfmt(LineNo);
      if (flag1)
      {
        this.e.SpellPending = true;
      }
      else
      {
        this.e.text[LineNo].flags2 |= 1;
        if (!this.WordBeingEdited())
          this.e.EditLine = -1;
      }
    }
    return true;
  }

  internal new bool DoAutoSpellCheck()
  {
    return this.e.TerArg.WordWrap && this.False(this.e.TerArg.ReadOnly) && (this.e.TerFlags4 & 256 /*0x0100*/) != 0;
  }

  internal bool GetMisspelledWord(
    int LineNo,
    int col,
    out string word,
    ref int pWordIdx,
    ref int pWordLen)
  {
    word = "";
    if (LineNo < 0 || LineNo >= this.e.TotalLines)
      return false;
    int len = this.e.text[LineNo].len;
    if (col < 0 || col >= len)
      return false;
    ushort[] numArray = this.OpenCfmt(LineNo);
    if ((this.e.TerFont[(int) numArray[col]].flags & 1024 /*0x0400*/) == 0)
      return false;
    int index1 = col - 1;
    while (index1 >= 0 && (this.e.TerFont[(int) numArray[index1]].flags & 1024 /*0x0400*/) != 0)
      --index1;
    int startIndex = index1 + 1;
    int length = 0;
    int index2 = startIndex;
    while (index2 < len && (this.e.TerFont[(int) numArray[index2]].flags & 1024 /*0x0400*/) != 0)
    {
      ++index2;
      ++length;
    }
    this.CloseCfmt(LineNo);
    char[] txt = this.e.text[LineNo].txt;
    word = new string(txt, startIndex, length);
    pWordIdx = startIndex;
    pWordLen = length;
    return true;
  }

  internal bool OnMisspelledWord(int lParam)
  {
    int pWordIdx = 0;
    int pWordLen = 0;
    this.TerMousePos(lParam, true);
    return this.GetMisspelledWord(this.e.MouseLine, this.e.MouseCol, out string _, ref pWordIdx, ref pWordLen);
  }

  internal new bool SearchSpellTime()
  {
    tc.StSearched = true;
    tc.hSpell = (Assembly) null;
    tc.StType = (System.Type) null;
    if ((this.e.TerFlags5 & 1073741824 /*0x40000000*/) != 0)
      return false;
    try
    {
      tc.hSpell = !tc.InIE ? Assembly.LoadFrom("SpellTime.DLL") : AppDomain.CurrentDomain.Load("SpellTime");
    }
    catch (Exception ex)
    {
      tc.hSpell = (Assembly) null;
      return false;
    }
    foreach (System.Type type in tc.hSpell.GetTypes())
    {
      if (type != (System.Type) null && type.Name == "SpellTime")
      {
        tc.StType = type;
        break;
      }
    }
    if (tc.StType == (System.Type) null)
    {
      tc.hSpell = (Assembly) null;
      return false;
    }
    try
    {
      tc.pStParseLine = tc.StType.GetMethod("StParseLine");
      tc.pStResetUserDict = tc.StType.GetMethod("StResetUserDict");
      tc.pStClearHist = tc.StType.GetMethod("StClearHist");
      tc.pSpellWord = tc.StType.GetMethod("SpellWord");
      tc.pStSetDictName = tc.StType.GetMethod("StSetDictName");
      tc.pToSpellHist = tc.StType.GetMethod("ToSpellHist");
      tc.pToUserDict = tc.StType.GetMethod("ToUserDict");
      tc.pStSetLicenseKey = tc.StType.GetMethod("StSetLicenseKey");
      tc.pStGetReplacement = tc.StType.GetMethod("StGetReplacement");
      tc.pStGetAlternateWordCount = tc.StType.GetMethod("StGetAlternateWordCount");
      tc.pStGetAlternateWord = tc.StType.GetMethod("StGetAlternateWord");
      tc.ST_INTERACTIVE = this.GetTypeField(tc.StType, "ST_INTERACTIVE");
      tc.ST_MAX_WORD_LEN = this.GetTypeField(tc.StType, "ST_MAX_WORD_LEN");
      tc.ST_MAX_SUG_WORDS = this.GetTypeField(tc.StType, "ST_MAX_SUG_WORDS");
      tc.ST_INTERACTIVE = this.GetTypeField(tc.StType, "ST_INTERACTIVE");
      tc.ST_ERROR = this.GetTypeField(tc.StType, "ST_ERROR");
      tc.ST_IGNORE = this.GetTypeField(tc.StType, "ST_IGNORE");
      tc.ST_REPLACE = this.GetTypeField(tc.StType, "ST_REPLACE");
      tc.ST_ADD = this.GetTypeField(tc.StType, "ST_ADD");
      tc.ST_INTERACTIVE = this.GetTypeField(tc.StType, "ST_INTERACTIVE");
      tc.ST_EXIT = this.GetTypeField(tc.StType, "ST_EXIT");
    }
    catch (Exception ex)
    {
      tc.StType = (System.Type) null;
      tc.hSpell = (Assembly) null;
    }
    return true;
  }

  internal new bool SpellCheckCurWordPart1(int lParam)
  {
    int pWordIdx = 0;
    int pWordLen = 0;
    if (this.e.SpellCheckerPopped)
    {
      this.e.SpellCheckerPopped = false;
      this.e.ContextMenu = this.e.OrgContextMenu;
    }
    this.TerMousePos(lParam, true);
    this.e.SpellLine = this.e.MouseLine;
    this.e.SpellCol = this.e.MouseCol;
    string word;
    if (!this.GetMisspelledWord(this.e.MouseLine, this.e.MouseCol, out word, ref pWordIdx, ref pWordLen))
      return false;
    if (this.SpellWord(word, 0, out tc.SkipInt))
    {
      for (int index = 0; index < this.e.TotalLines; ++index)
        tc.ResetUintFlag(ref this.e.text[index].flags2, 1);
      this.e.Invalidate();
      return false;
    }
    this.StGetAlternateWordCount();
    this.e.TerTextPosToPix(1, this.e.MouseLine, pWordIdx + pWordLen, out int _, out int _);
    return true;
  }

  internal bool SpellCheckCurWordPart3()
  {
    for (int index = 0; index < this.e.TotalLines; ++index)
      tc.ResetUintFlag(ref this.e.text[index].flags2, 1);
    this.PaintTer();
    return true;
  }

  internal new bool SpellCheckCurWordPart2(int CmdId)
  {
    int pWordIdx = 0;
    int pWordLen = 0;
    int SegLen = 0;
    int LineIndex = 0;
    int undoRef = this.e.UndoRef;
    this.e.ContextMenu = this.e.OrgContextMenu;
    string word;
    if (!this.GetMisspelledWord(this.e.SpellLine, this.e.SpellCol, out word, ref pWordIdx, ref pWordLen))
      return false;
    if (CmdId >= 2500 && CmdId <= 2505)
    {
      this.SplReplaceWord(word, this.e.SpellLine, pWordIdx, ref LineIndex, ref SegLen, this.AltWord[CmdId - 2500], undoRef);
      tc.ResetUintFlag(ref this.e.text[this.e.SpellLine].flags2, 1);
    }
    else if (CmdId == 2510 || CmdId == 2511)
    {
      string lower = word.ToLower();
      if (CmdId == 2510)
      {
        this.ToSpellHist(lower, 'I', "");
      }
      else
      {
        this.ToUserDict(lower);
        this.StResetUserDict((string) null, ref tc.SkipStr);
      }
      for (int index = 0; index < this.e.TotalLines; ++index)
        tc.ResetUintFlag(ref this.e.text[index].flags2, 1);
    }
    this.PaintTer();
    return true;
  }

  internal bool SpellWord(string InputWord, int flags, out int ResultCode)
  {
    ResultCode = 0;
    if (!ImDocumentEditorConfig.Instance.SpellCheck)
      return true;
    try
    {
      return SpellChecker.Instance.SpellWord(InputWord);
    }
    catch
    {
      ResultCode = tc.ST_ERROR;
      return false;
    }
  }

  internal void SplHilightWord()
  {
    this.e.HilightType = 2;
    this.e.HilightBegRow = this.e.HilightEndRow = this.e.CurLine;
    this.e.CurCol = this.e.HilightBegCol = this.WordIndex;
    this.e.HilightEndCol = this.WordIndex + this.WordLen;
    if (this.e.CurLine - this.e.BeginLine > this.e.WinHeight / 2 || this.e.CurLine - this.e.BeginLine < 0)
    {
      this.e.BeginLine = this.e.CurLine - this.e.WinHeight / 4;
      if (this.e.BeginLine < 0)
        this.e.BeginLine = 0;
    }
    this.e.CurRow = this.e.CurLine - this.e.BeginLine;
    this.e.WrapFlag = 0;
    this.e.TerOpFlags |= 65536 /*0x010000*/;
    if (!this.e.CaretEngaged)
      this.e.TerEngageCaret(true);
    this.PaintTer();
    this.e.TerOpFlags &= -65537;
  }

  internal bool SplReplaceWord(
    string CurWord,
    int LineNo,
    int WordIndex,
    ref int LineIndex,
    ref int SegLen,
    string NewWord,
    int OrigUndoRef)
  {
    int length1 = CurWord.Length;
    int length2 = NewWord.Length;
    int num = length2 - length1;
    if (num != 0)
    {
      if (this.e.text[LineNo].len + num > this.e.LineWidth - 1)
        return false;
      LineIndex += num;
      SegLen += num;
    }
    this.e.UndoRef = OrigUndoRef;
    this.SaveUndo(LineNo, WordIndex, LineNo, WordIndex + length1, 'D');
    if (num < 0)
      this.MoveLineData(LineNo, WordIndex, Math.Abs(num), 'D');
    if (num > 0)
      this.MoveLineData(LineNo, WordIndex, Math.Abs(num), 'B');
    char[] txt = this.e.text[LineNo].txt;
    for (int index = 0; index < length2; ++index)
      txt[WordIndex + index] = NewWord[index];
    this.e.UndoRef = OrigUndoRef;
    this.SaveUndo(LineNo, WordIndex, LineNo, WordIndex + length2, 'I');
    this.e.FireSpellWordReplaced((object) this.e, this.RowColToAbs(LineNo, WordIndex), CurWord, NewWord);
    tc.ResetUintFlag(ref this.e.text[LineNo].flags2, 1);
    ++this.e.TerArg.modified;
    return true;
  }

  internal bool StClearHist() => false;

  internal string StGetAlternateWord(int AlternateNumber)
  {
    object[] parameters = new object[1]
    {
      (object) AlternateNumber
    };
    return tc.pStGetAlternateWord != (MethodInfo) null ? (string) tc.pStGetAlternateWord.Invoke(this.e.st, parameters) : "";
  }

  internal int StGetAlternateWordCount()
  {
    return tc.pStGetAlternateWordCount != (MethodInfo) null ? (int) tc.pStGetAlternateWordCount.Invoke(this.e.st, (object[]) null) : 0;
  }

  internal string StGetReplacement()
  {
    return tc.pStGetReplacement != (MethodInfo) null ? (string) tc.pStGetReplacement.Invoke(this.e.st, (object[]) null) : "";
  }

  internal int StParseLine(
    string CurLine,
    ref string OutCurWord,
    ref int WordIndex,
    ref int CurIndex,
    int LineLen)
  {
    return !ImDocumentEditorConfig.Instance.SpellCheck ? 0 : SpellChecker.Instance.StParseLine(CurLine, ref OutCurWord, ref WordIndex, ref CurIndex, LineLen);
  }

  internal bool StResetUserDict(string NewName, ref string OldName)
  {
    if (tc.pStResetUserDict != (MethodInfo) null)
    {
      object[] parameters = new object[2]
      {
        (object) NewName,
        (object) OldName
      };
      int num = (bool) tc.pStResetUserDict.Invoke(this.e.st, parameters) ? 1 : 0;
      OldName = (string) parameters[1];
      return num != 0;
    }
    OldName = NewName;
    return false;
  }

  internal object TerGetSpellTimeObject() => this.e.st;

  internal bool TerInitSpellTime(object st)
  {
    string str = "";
    int num = 0;
    if (tc.hSpell == (Assembly) null || tc.StType == (System.Type) null || (this.e.TerFlags6 & 128 /*0x80*/) != 0)
      return false;
    if (st != null)
    {
      this.e.st = st;
      return true;
    }
    if (this.e.StDictDir != "")
    {
      string stDictDir = this.e.StDictDir;
      switch (stDictDir[stDictDir.Length - 1])
      {
        case ':':
        case '\\':
          str = stDictDir + "dict35.d";
          break;
        default:
          stDictDir += "\\";
          goto case ':';
      }
    }
    System.Type[] types = new System.Type[2]
    {
      typeof (string),
      typeof (int)
    };
    object[] parameters = new object[2]
    {
      (object) str,
      (object) num
    };
    try
    {
      this.e.st = tc.StType.GetConstructor(types).Invoke(parameters);
      if (tc.StnKey != "")
        ImRtfEditor.TerSetStLicenseKey(tc.StnKey);
    }
    catch (Exception ex)
    {
      this.e.st = (object) null;
      tc.StType = (System.Type) null;
      tc.hSpell = (Assembly) null;
    }
    return true;
  }

  internal static bool TerSetStLicenseKey(string key)
  {
    if (!(tc.pStSetLicenseKey != (MethodInfo) null))
      return false;
    object[] parameters = new object[1]{ (object) key };
    return (bool) tc.pStSetLicenseKey.Invoke((object) null, parameters);
  }

  internal bool TerSpellCheck(bool StopAfterFirst, bool msg)
  {
    return this.TerSpellCheck2(StopAfterFirst, msg, out tc.SkipBool);
  }

  internal bool TerSpellCheck2(bool StopAfterFirst, bool msg, out bool Cancelled)
  {
    int num1 = 0;
    int num2 = 0;
    string OutCurWord = "";
    Cursor cursor = (Cursor) null;
    int num3 = -1;
    int num4 = -1;
    Cancelled = false;
    if (this.e.st == null)
      return false;
    int undoRef = this.e.UndoRef;
    int index1;
    int num5;
    int index2;
    int num6;
    if (this.e.HilightType == 1)
    {
      this.NormalizeBlock();
      index1 = this.e.HilightBegRow;
      num5 = 0;
      index2 = this.e.HilightEndRow;
      num6 = this.e.text[index2].len;
      if (num6 < 0)
        num6 = 0;
      this.e.HilightType = 0;
      this.e.StretchHilight = false;
    }
    else if (this.e.HilightType == 2)
    {
      this.NormalizeBlock();
      index1 = this.e.HilightBegRow;
      num5 = this.e.HilightBegCol;
      if (num5 >= this.e.text[index1].len)
        num5 = this.e.text[index1].len - 1;
      if (num5 < 0)
        num5 = 0;
      index2 = this.e.HilightEndRow;
      num6 = this.e.HilightEndCol - 1;
      if (num6 >= this.e.text[index2].len)
        num6 = this.e.text[index2].len - 1;
      if (num6 < 0)
        num6 = 0;
      if (index1 == index2 && num5 > num6)
        num5 = num6;
      this.e.HilightType = 0;
      this.e.StretchHilight = false;
    }
    else
    {
      index1 = 0;
      num5 = 0;
      index2 = this.e.TotalLines - 1;
      if (index2 < 0)
        index2 = 0;
      num6 = this.e.text[index2].len;
      if (num6 < 0)
        num6 = 0;
    }
    if ((this.e.TerFlags & 512 /*0x0200*/) == 0)
    {
      cursor = this.e.Cursor;
      this.e.Cursor = Cursors.WaitCursor;
    }
    int x = this.e.TerFlags2 & 1024 /*0x0400*/;
    this.e.TerFlags2 |= 1024 /*0x0400*/;
    this.e.TerOpFlags2 |= 1;
    if ((this.e.TerFlags5 & 8192 /*0x2000*/) == 0)
      this.StClearHist();
    for (this.e.CurLine = index1; this.e.CurLine <= index2; ++this.e.CurLine)
    {
      if (this.e.text[this.e.CurLine].len != 0)
      {
        string CurLine = new string(this.e.text[this.e.CurLine].txt, 0, this.e.text[this.e.CurLine].len);
        int len;
        for (int index3 = 0; index3 < this.e.text[this.e.CurLine].len; index3 = len)
        {
          len = this.e.text[this.e.CurLine].len;
          while ((this.WordLen = this.StParseLine(CurLine, ref OutCurWord, ref this.WordIndex, ref index3, len)) > 0)
          {
            if ((this.e.CurLine != index1 || this.WordIndex >= num5) && (this.e.CurLine != index2 || this.WordIndex <= num6))
            {
              ushort[] numArray = this.OpenCfmt(this.e.CurLine);
              int length1 = OutCurWord.Length;
              int length2;
              for (length2 = 0; length2 < length1; ++length2)
              {
                int CurFont = (int) numArray[this.WordIndex + length2];
                if (CurFont != 0 && CurFont != num4)
                {
                  if (CurFont != num3)
                  {
                    if (this.e.TerFont[CurFont].FieldId == 6 || this.edit.HiddenText(CurFont) || (this.e.TerFont[CurFont].style & 688) != 0 || this.e.TerFont[CurFont].rtl)
                    {
                      num3 = CurFont;
                      break;
                    }
                    num4 = CurFont;
                  }
                  else
                    break;
                }
              }
              this.CloseCfmt(this.e.CurLine);
              if (length2 > 0)
              {
                OutCurWord = OutCurWord.Substring(0, length2);
                index3 -= this.WordLen - length2;
                int num7 = this.WordLen = length2;
                int ResultCode;
                if (!this.SpellWord(OutCurWord, 0, out ResultCode))
                {
                  if ((ResultCode & tc.ST_ERROR) == 0)
                  {
                    if (StopAfterFirst)
                      return false;
                    if (cursor != (Cursor) null)
                      this.e.Cursor = cursor;
                    this.SplHilightWord();
                    this.SpellWord(OutCurWord, tc.ST_INTERACTIVE, out ResultCode);
                    if ((ResultCode & (tc.ST_IGNORE | tc.ST_REPLACE | tc.ST_ADD | tc.ST_INPUT)) != 0)
                      ++num2;
                    else if ((ResultCode & (tc.ST_EXIT | tc.ST_ERROR)) != 0)
                    {
                      if ((ResultCode & tc.ST_EXIT) != 0)
                      {
                        Cancelled = true;
                        goto label_63;
                      }
                      goto label_63;
                    }
                    string replacement = this.StGetReplacement();
                    if (replacement.Length > 0)
                    {
                      this.SplReplaceWord(OutCurWord, this.e.CurLine, this.WordIndex, ref index3, ref len, replacement, undoRef);
                      len = this.e.text[this.e.CurLine].len;
                      CurLine = new string(this.e.text[this.e.CurLine].txt, 0, this.e.text[this.e.CurLine].len);
                    }
                    if (cursor != (Cursor) null)
                      this.e.Cursor = Cursors.WaitCursor;
                  }
                  else
                    goto label_63;
                }
                ++num1;
              }
            }
          }
          if (len == this.e.text[this.e.CurLine].len)
            break;
        }
      }
    }
    this.e.CurLine = index2;
label_63:
    this.StResetUserDict((string) null, ref tc.SkipStr);
    if (cursor != (Cursor) null)
      this.e.Cursor = cursor;
    this.e.HilightType = 0;
    if (this.False(x))
      this.e.TerFlags2 = tc.ResetFlag(this.e.TerFlags2, 1024 /*0x0400*/);
    this.e.TerOpFlags2 &= -2;
    if (this.e.CurLine - this.e.BeginLine >= this.e.WinHeight - 1 || this.e.CurLine - this.e.BeginLine < 0)
    {
      this.e.BeginLine = this.e.CurLine - this.e.WinHeight / 2;
      if (this.e.BeginLine < 0)
        this.e.BeginLine = 0;
    }
    this.e.CurRow = this.e.CurLine - this.e.BeginLine;
    this.e.CurCol = 0;
    this.e.PaintFlag = 4;
    this.PaintTer();
    string msg1 = $"{this.e.MsgString[194]}{num1.ToString()} {this.e.MsgString[201]}{num2.ToString()}";
    if (msg && !StopAfterFirst)
    {
      int num8 = (int) this.ShowMessage(msg1, this.e.MsgString[33], MessageBoxButtons.OK);
    }
    return num2 == 0;
  }

  internal bool TerSpellCheckCurWord(int x, int y)
  {
    return this.SpellCheckCurWordPart1((y << 16 /*0x10*/) + x);
  }

  private bool ToSpellHist(string CurWord, char flag, string ReplaceWord)
  {
    if (!(tc.pToSpellHist != (MethodInfo) null))
      return false;
    object[] parameters = new object[3]
    {
      (object) CurWord,
      (object) flag,
      (object) ReplaceWord
    };
    return (bool) tc.pToSpellHist.Invoke(this.e.st, parameters);
  }

  internal bool ToUserDict(string CurWord)
  {
    if (!(tc.pToUserDict != (MethodInfo) null))
      return false;
    object[] parameters = new object[1]{ (object) CurWord };
    return (bool) tc.pToUserDict.Invoke(this.e.st, parameters);
  }

  internal new bool WordBeingEdited()
  {
    return this.e.EditLine == this.e.CurLine && ((this.e.EditCol < this.e.EditWordIndex ? 0 : (this.e.EditCol <= this.e.EditWordIndex + this.e.EditWordLen ? 1 : 0)) & (this.e.CurCol < this.e.EditWordIndex ? (false ? 1 : 0) : (this.e.CurCol <= this.e.EditWordIndex + this.e.EditWordLen ? 1 : 0))) != 0;
  }
}
