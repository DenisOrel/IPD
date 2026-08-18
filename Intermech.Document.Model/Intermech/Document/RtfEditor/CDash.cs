// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.CDash
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class CDash : COp
{
  internal const uint DASHES_FAILURE = 2147500037 /*0x80004005*/;
  internal const uint DASHES_NOHYPHS = 1;
  internal const uint DASHES_OK = 0;
  internal const uint DASHES_OUTOFMEMORY = 2147942414 /*0x8007000E*/;
  internal const int DASHES_TYPE_ACHANGE = 5;
  internal const int DASHES_TYPE_ADD = 2;
  internal const int DASHES_TYPE_CHANGE = 3;
  internal const int DASHES_TYPE_DELETE = 4;
  internal const int DASHES_TYPE_NORMAL = 1;
  internal const int DASHES_TYPE_NULL = 0;

  internal CDash(ImRtfEditor ImRtfEditor)
    : base(ImRtfEditor)
  {
    this.e = ImRtfEditor;
  }

  [DllImport("Dashes.dll", CharSet = CharSet.Ansi)]
  internal static extern uint DashesSetLanguage(short lang);

  internal new int GetHyphPrefixLen(
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
    int hyphPrefixLen = 0;
    byte num1 = 63 /*0x3F*/;
    uint num2 = 128 /*0x80*/;
    PrefixWidth = 0;
    if (!this.e.DoHyph)
      return 0;
    if (MaxLen > 1000)
      MaxLen = 1000;
    int num3;
    for (num3 = 0; num3 < MaxLen; ++num3)
    {
      char ch = CurWord[idx + num3];
      bool flag = ch >= '!' && ch <= '.' || ch >= ':' && ch <= '@';
      if (((ch == ' ' ? 1 : (ch < ' ' ? 1 : 0)) | (flag ? 1 : 0)) != 0 || ch >= '0' && ch <= '9')
        break;
    }
    int count = num3;
    if (count >= 1000 || LimitCount > count)
      return 0;
    for (int index = 0; index < LimitCount; ++index)
    {
      int num4 = (int) WordWidth[index];
    }
    int index1;
    for (index1 = LimitCount - 1; index1 >= 0; --index1)
    {
      int index2 = (int) pFmt[idx + index1];
      int num5 = !UsePrtUnits ? this.e.TerFont[index2].CharWidth[45] : this.e.PrtFont[index2].CharWidth[45];
      if (AvailWidth < num5)
        AvailWidth += (int) WordWidth[index1];
      else
        break;
    }
    LimitCount = index1 + 1;
    if (LimitCount < 1)
      return 0;
    CDash.StrDashes[] dashes = new CDash.StrDashes[count + 1];
    ushort[] CurWord1 = new ushort[count + 1];
    int index3;
    for (index3 = 0; index3 < count; ++index3)
    {
      CurWord1[index3] = (ushort) CurWord[idx + index3];
      dashes[index3] = new CDash.StrDashes();
    }
    CurWord1[index3] = (ushort) 0;
    if (CDash.HyphenateUnicode(CurWord1, count, dashes) == 0U)
    {
      for (int index4 = LimitCount - 1; index4 >= 0; --index4)
      {
        if (dashes[index4].ht == 1 && !this.True(dashes[index4].iStrength & num2) && (long) (dashes[index4].iStrength & (uint) num1) <= (long) this.e.HyphLevel)
        {
          hyphPrefixLen = index4 + 1;
          break;
        }
      }
    }
    PrefixWidth = 0;
    if (hyphPrefixLen > 0)
    {
      for (int index5 = 0; index5 < hyphPrefixLen; ++index5)
        PrefixWidth += (int) WordWidth[index5];
      int index6 = (int) pFmt[idx + hyphPrefixLen - 1];
      int num6 = !UsePrtUnits ? this.e.TerFont[index6].CharWidth[45] : this.e.PrtFont[index6].CharWidth[45];
      PrefixWidth += num6;
    }
    return hyphPrefixLen;
  }

  [DllImport("Dashes.dll", CharSet = CharSet.Ansi)]
  internal static extern uint HyphenateUnicode(
    ushort[] CurWord,
    int count,
    [In, Out] CDash.StrDashes[] dashes);

  internal bool TerEnableDashes(int lang, int level, bool enable)
  {
    if (!enable)
    {
      this.e.DoHyph = false;
      return true;
    }
    try
    {
      if (CDash.DashesSetLanguage((short) lang) != 0U)
        return false;
      this.e.DoHyph = true;
      this.e.HyphLevel = level;
      return true;
    }
    catch (Exception ex)
    {
      this.e.DoHyph = false;
      return false;
    }
  }

  internal struct StrDashes
  {
    internal int ht;
    internal ushort wchModified;
    internal ushort wchModified2;
    internal uint iStrength;
  }
}
