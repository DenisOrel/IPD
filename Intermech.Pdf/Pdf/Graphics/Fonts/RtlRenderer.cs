// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.Fonts.RtlRenderer
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Native;
using Syncfusion.Pdf.Primitives;
using System;
using System.Collections;
using System.Drawing;


namespace Syncfusion.Pdf.Graphics.Fonts
{
    internal class RtlRenderer
    {
      private const char c_closeBracket = ')';
      private const char c_openBracket = '(';
      private static Bitmap s_bmp = new Bitmap(1, 1);

      private RtlRenderer() => throw new NotImplementedException();

      private static string AddChars(PdfTrueTypeFont font, string line)
      {
        if (font == null)
          throw new ArgumentNullException(nameof (font));
        string text1 = line != null ? line : throw new ArgumentNullException(nameof (line));
        TtfReader ttfReader = (font.InternalFont as UnicodeTrueTypeFont).TtfReader;
        font.SetSymbols(text1);
        string text2 = text1;
        return PdfString.ByteToString(PdfString.ToUnicodeArray(ttfReader.ConvertString(text2), false));
      }

      private static string AddChars(PdfTrueTypeFont font, ushort[] glyphs)
      {
        if (font == null)
          throw new ArgumentNullException(nameof (font));
        if (glyphs == null)
          throw new ArgumentNullException(nameof (glyphs));
        font.SetSymbols(glyphs);
        char[] chArray = new char[glyphs.Length];
        for (int index = 0; index < glyphs.Length; ++index)
          chArray[index] = (char) glyphs[index];
        return PdfString.ByteToString(PdfString.ToUnicodeArray(new string(chArray), false));
      }

      private static void AddGlyphs(ArrayList glyphs, ushort[] pwOutGlyphs, int count)
      {
        if (glyphs == null || pwOutGlyphs == null || pwOutGlyphs.Length < count)
          return;
        for (int index = 0; index < count; ++index)
        {
          ushort pwOutGlyph = pwOutGlyphs[index];
          glyphs.Add((object) pwOutGlyph);
        }
      }

      private static bool ContainsRTLSymbol(ushort[] characterCodes)
      {
        if (characterCodes == null)
          throw new ArgumentNullException(nameof (characterCodes));
        int index = 0;
        for (int length = characterCodes.Length; index < length; ++index)
        {
          if (characterCodes[index] == (ushort) 2 || characterCodes[index] == (ushort) 6)
            return true;
        }
        return false;
      }

      private static string CustomLayout(string line, bool rtl)
      {
        if (line == null)
          throw new ArgumentNullException(nameof (line));
        return rtl ? RtlRenderer.CustomRtl(line) : RtlRenderer.CustomLtr(line);
      }

      private static string[] CustomLayout(
        string line,
        PdfTrueTypeFont font,
        bool rtl,
        bool wordSpace)
      {
        if (line == null)
          throw new ArgumentNullException(nameof (line));
        if (font == null)
          throw new ArgumentNullException(nameof (font));
        string line1 = RtlRenderer.CustomLayout(line, rtl);
        if (wordSpace)
        {
          string[] strArray = line1.Split((char[]) null);
          int length = strArray.Length;
          for (int index = 0; index < length; ++index)
            strArray[index] = RtlRenderer.AddChars(font, strArray[index]);
          return strArray;
        }
        return new string[1]
        {
          RtlRenderer.AddChars(font, line1)
        };
      }

      private static string CustomLtr(string text)
      {
        string str = text != null ? text : throw new ArgumentNullException(nameof (text));
        char[] charArray = str.ToCharArray();
        bool flag1 = true;
        bool flag2 = true;
        ushort[] numArray = new ushort[text.Length];
        KernelApi.GetStringTypeExW(2048U /*0x0800*/, StringInfoType.CT_TYPE2, text, text.Length, numArray);
        int indexCursor = 0;
        int indexLength = 0;
        if (RtlRenderer.ContainsRTLSymbol(numArray))
        {
          int index1 = 0;
          for (int length1 = numArray.Length; index1 < length1; ++index1)
          {
            char symbol = str[index1];
            ushort symbolCode = numArray[index1];
            if (RtlRenderer.IsRTLSymbol(symbolCode))
            {
              RtlRenderer.WriteInLTR(charArray, symbol, false, ref indexCursor, ref indexLength);
              flag1 = false;
            }
            else if (RtlRenderer.IsLTRText(symbolCode))
            {
              RtlRenderer.SaveSymbol(charArray, symbol, false, ref indexCursor, ref indexLength);
              flag1 = true;
            }
            else if (RtlRenderer.IsGeneralEuroNumber(symbolCode) || RtlRenderer.IsEuroTerminator(symbolCode) && RtlRenderer.IsNextEuroNumber(numArray, index1))
            {
              if (flag1)
              {
                RtlRenderer.SaveSymbol(charArray, symbol, false, ref indexCursor, ref indexLength);
                flag1 = true;
              }
              else
              {
                RtlRenderer.WriteInLTR(charArray, symbol, false, ref indexCursor, ref indexLength);
                ++indexCursor;
                --indexLength;
                flag1 = false;
              }
            }
            else if (RtlRenderer.IsWhitespace(symbolCode) && RtlRenderer.IsBackEuroNumber(numArray, index1) && !flag1)
            {
              for (int index2 = indexLength + indexCursor; RtlRenderer.IsEuroNumber(numArray[index2 - 1]); --index2)
              {
                ++indexLength;
                --indexCursor;
              }
              RtlRenderer.WriteInLTR(charArray, symbol, false, ref indexCursor, ref indexLength);
              flag1 = false;
            }
            else
            {
              int index3 = index1;
              for (int length2 = numArray.Length; index3 < length2; ++index3)
              {
                if (RtlRenderer.IsRTLText(numArray[index3]) || !flag1 && RtlRenderer.IsEuroNumber(numArray[index3]))
                {
                  flag2 = false;
                  index3 = length2;
                }
                else if (RtlRenderer.IsLTRText(numArray[index3]) || flag1 && RtlRenderer.IsEuroNumber(numArray[index3]))
                {
                  flag2 = true;
                  index3 = length2;
                }
              }
              if (!flag1 && !flag2)
                RtlRenderer.WriteInLTR(charArray, symbol, false, ref indexCursor, ref indexLength);
              else
                RtlRenderer.SaveSymbol(charArray, symbol, false, ref indexCursor, ref indexLength);
            }
          }
        }
        return new string(charArray);
      }

      private static string CustomRtl(string text)
      {
        string str = text != null ? text : throw new ArgumentNullException(nameof (text));
        char[] charArray = str.ToCharArray();
        bool flag1 = true;
        bool flag2 = true;
        ushort[] numArray = new ushort[text.Length];
        KernelApi.GetStringTypeExW(2048U /*0x0800*/, StringInfoType.CT_TYPE2, text, text.Length, numArray);
        int indexCursor = numArray.Length - 1;
        int indexLength = 0;
        if (RtlRenderer.ContainsRTLSymbol(numArray))
        {
          int index1 = 0;
          for (int length1 = numArray.Length; index1 < length1; ++index1)
          {
            char symbol = str[index1];
            ushort symbolCode = numArray[index1];
            if (RtlRenderer.IsLTRText(symbolCode))
            {
              RtlRenderer.WriteInLTR(charArray, symbol, true, ref indexCursor, ref indexLength);
              flag1 = false;
            }
            else if (RtlRenderer.IsRTLSymbol(symbolCode))
            {
              RtlRenderer.SaveSymbol(charArray, symbol, true, ref indexCursor, ref indexLength);
              flag1 = true;
            }
            else if (RtlRenderer.IsGeneralEuroNumber(symbolCode) || ((!RtlRenderer.IsEuroTerminator(symbolCode) ? 0 : (RtlRenderer.IsBackEuroNumber(numArray, index1) ? 1 : 0)) & (flag1 ? 1 : 0)) != 0)
              RtlRenderer.WriteInLTR(charArray, symbol, true, ref indexCursor, ref indexLength);
            else if (RtlRenderer.IsBracket(symbol))
            {
              RtlRenderer.ReverseBrackets(charArray, symbol, ref indexCursor, ref indexLength);
            }
            else
            {
              int index2 = index1;
              for (int length2 = numArray.Length; index2 < length2; ++index2)
              {
                if (RtlRenderer.IsLTRText(numArray[index2]) || !flag1 && RtlRenderer.IsEuroNumber(numArray[index2]))
                {
                  flag2 = false;
                  index2 = length2;
                }
                else if (RtlRenderer.IsRTLSymbol(numArray[index2]) || flag1 && RtlRenderer.IsEuroNumber(numArray[index2]))
                {
                  flag2 = true;
                  index2 = length2;
                }
              }
              if (!flag1 && !flag2)
                RtlRenderer.WriteInLTR(charArray, symbol, true, ref indexCursor, ref indexLength);
              else
                RtlRenderer.SaveSymbol(charArray, symbol, true, ref indexCursor, ref indexLength);
            }
          }
        }
        return new string(charArray);
      }

      private static string[] CustomSplitLayout(
        string line,
        PdfTrueTypeFont font,
        bool rtl,
        bool wordSpace)
      {
        if (line == null)
          throw new ArgumentNullException(nameof (line));
        if (font == null)
          throw new ArgumentNullException(nameof (font));
        return RtlRenderer.CustomLayout(line, rtl).Split((char[]) null);
      }

      private static byte[] GetBidiLevel(RtlApi.SCRIPT_ITEM[] items, int count)
      {
        byte[] bidiLevel = (byte[]) null;
        if (items != null && items.Length >= count)
        {
          bidiLevel = new byte[count];
          for (int index = 0; index < count; ++index)
          {
            int num = RtlApi.Decrypt((int) items[index].a.s.val, 0, 5);
            bidiLevel[index] = (byte) num;
          }
        }
        return bidiLevel;
      }

      internal static bool GetGlyphIndices(
        string line,
        PdfTrueTypeFont font,
        bool rtl,
        out ushort[] glyphs)
      {
        if (line == null)
          throw new ArgumentNullException(nameof (line));
        if (font == null)
          throw new ArgumentNullException(nameof (font));
        glyphs = (ushort[]) null;
        RtlApi.SCRIPT_ITEM[] items = (RtlApi.SCRIPT_ITEM[]) null;
        int count = 0;
        bool glyphIndices = RtlRenderer.StringItemize(line, rtl, out items, out count);
        if (glyphIndices)
        {
          int[] visualToLogical = (int[]) null;
          int[] logicalToVisual = (int[]) null;
          glyphIndices = RtlRenderer.StringLayout(RtlRenderer.GetBidiLevel(items, count), count, out visualToLogical, out logicalToVisual);
          if (glyphIndices)
            glyphIndices = RtlRenderer.StringShape(line, items, font.Font, count, visualToLogical, out glyphs);
        }
        return glyphIndices;
      }

      private static bool IsBackEuroNumber(ushort[] characterCodes, int index)
      {
        if (characterCodes == null)
          throw new ArgumentNullException(nameof (characterCodes));
        if (index - 1 < 0)
          return false;
        bool flag = false;
        if (index < characterCodes.Length - 1 && index >= 0)
          flag = characterCodes[index - 1] == (ushort) 3;
        return flag;
      }

      private static bool IsBracket(char symbol) => symbol == '(' || symbol == ')';

      private static bool IsEnglish(string word)
      {
        char ch = word.Length > 0 ? word[0] : char.MinValue;
        return ch >= char.MinValue && ch < 'ÿ';
      }

      private static bool IsEuroNumber(ushort symbolCode) => symbolCode == (ushort) 3;

      private static bool IsEuroTerminator(ushort symbolCode) => symbolCode == (ushort) 5;

      private static bool IsGeneralEuroNumber(ushort symbolCode)
      {
        return symbolCode == (ushort) 3 || symbolCode == (ushort) 4;
      }

      private static bool IsLTRText(ushort symbolCode) => symbolCode == (ushort) 1;

      private static bool IsNextEuroNumber(ushort[] characterCodes, int index)
      {
        if (characterCodes == null)
          throw new ArgumentNullException(nameof (characterCodes));
        if (index + 1 > characterCodes.Length)
          return false;
        bool flag = false;
        if (index < characterCodes.Length - 1 && index >= 0)
          flag = characterCodes[index + 1] == (ushort) 3;
        return flag;
      }

      private static bool IsRTLSymbol(ushort symbolCode)
      {
        return symbolCode == (ushort) 2 || symbolCode == (ushort) 6;
      }

      private static bool IsRTLText(ushort symbolCode) => symbolCode == (ushort) 2;

      private static bool IsWhitespace(ushort symbolCode) => symbolCode == (ushort) 10;

      private static void KeepOrder(
        string[] words,
        int startIndex,
        int count,
        string[] result,
        int resultIndex)
      {
        int num = 0;
        int index = resultIndex - count + 1;
        while (num < count)
        {
          result[index] = words[num + startIndex];
          ++num;
          ++index;
        }
      }

      public static string[] Layout(string line, PdfTrueTypeFont font, bool rtl, bool wordSpace)
      {
        if (line == null)
          throw new ArgumentNullException(nameof (line));
        if (font == null)
          throw new ArgumentNullException(nameof (font));
        return font.Unicode ? (font.Font != null ? RtlRenderer.SystemLayout(line, font, rtl, wordSpace) : RtlRenderer.CustomLayout(line, font, rtl, wordSpace)) : new string[1]
        {
          line
        };
      }

      private static void ReverseBrackets(
        char[] convertedData,
        char symbol,
        ref int indexCursor,
        ref int indexLength)
      {
        if (convertedData == null)
          throw new ArgumentNullException(nameof (convertedData));
        indexCursor -= indexLength;
        indexLength = 0;
        convertedData[indexCursor] = symbol == '(' ? ')' : '(';
        --indexCursor;
      }

      private static string[] ReverseWords(string[] words)
      {
        int length = words != null ? words.Length : throw new ArgumentNullException(nameof (words));
        string[] result = new string[length];
        string word = (string) null;
        int num = 0;
        int count = 0;
        int index = 0;
        int resultIndex = length - 1;
        while (index < length)
        {
          if (num != 0)
          {
            if (num != 1)
              throw new PdfException("Internal error.");
            ++count;
            ++index;
            if (index < length)
              word = words[index];
            if (index >= length || !RtlRenderer.IsEnglish(word))
            {
              RtlRenderer.KeepOrder(words, index - count, count, result, resultIndex);
              resultIndex -= count;
              num = 0;
            }
          }
          else
          {
            word = words[index];
            if (RtlRenderer.IsEnglish(word))
            {
              count = 0;
              num = 1;
            }
            else
            {
              result[resultIndex] = word;
              ++index;
              --resultIndex;
            }
          }
        }
        return result;
      }

      private static void SaveSymbol(
        char[] convertedData,
        char symbol,
        bool rtl,
        ref int indexCursor,
        ref int indexLength)
      {
        if (convertedData == null)
          throw new ArgumentNullException(nameof (convertedData));
        indexCursor = rtl ? indexCursor - indexLength : indexCursor + indexLength;
        indexLength = 0;
        convertedData[indexCursor] = symbol;
        indexCursor = rtl ? indexCursor - 1 : indexCursor + 1;
      }

      internal static string[] SplitLayout(
        string line,
        PdfTrueTypeFont font,
        bool rtl,
        bool wordSpace)
      {
        if (line == null)
          throw new ArgumentNullException(nameof (line));
        if (font == null)
          throw new ArgumentNullException(nameof (font));
        int num = !font.Unicode ? 0 : (font.Font != null ? 1 : 0);
        string[] strArray = (string[]) null;
        if (num != 0)
          strArray = RtlRenderer.SystemSplitLayout(line, font, rtl, wordSpace);
        return num != 0 && strArray != null ? strArray : RtlRenderer.CustomSplitLayout(line, font, rtl, wordSpace);
      }

      private static bool StringItemize(
        string text,
        bool rtl,
        out RtlApi.SCRIPT_ITEM[] items,
        out int count)
      {
        if (text == null || text.Length == 0)
        {
          items = (RtlApi.SCRIPT_ITEM[]) null;
          count = 0;
          return false;
        }
        int num1 = 10;
        int num2 = 0;
        int val2 = text.Length + 1;
        count = 0;
        uint num3;
        do
        {
          RtlApi.SCRIPT_CONTROL psControl = new RtlApi.SCRIPT_CONTROL();
          RtlApi.SCRIPT_STATE psState = new RtlApi.SCRIPT_STATE();
          int cMaxItems = Math.Max(16 /*0x10*/, val2);
          items = new RtlApi.SCRIPT_ITEM[cMaxItems];
          if (rtl)
            psState.val |= (ushort) 1;
          num3 = IntPtr.Size == 8 ? RtlApi.ScriptItemize(text, text.Length, cMaxItems, ref psControl, ref psState, items[0], ref count) : RtlApi.ScriptItemize(text, text.Length, cMaxItems, ref psControl, ref psState, ref items[0], ref count);
          switch (num3)
          {
            case 0:
            case 2147942414 /*0x8007000E*/:
              if (num3 == 0U || num2 < num1)
              {
                ++num2;
                val2 = cMaxItems * 2;
                continue;
              }
              break;
          }
          items = (RtlApi.SCRIPT_ITEM[]) null;
          count = 0;
          return false;
        }
        while (num3 != 0U);
        return true;
      }

      private static bool StringLayout(
        byte[] bidi,
        int count,
        out int[] visualToLogical,
        out int[] logicalToVisual)
      {
        visualToLogical = (int[]) null;
        logicalToVisual = (int[]) null;
        bool flag = false;
        if (bidi != null && bidi.Length == count && count > 0)
        {
          visualToLogical = new int[count];
          logicalToVisual = new int[count];
          flag = true;
          if (RtlApi.ScriptLayout(count, ref bidi[0], ref visualToLogical[0], ref logicalToVisual[0]) != 0U)
          {
            flag = false;
            visualToLogical = (int[]) null;
            logicalToVisual = (int[]) null;
          }
        }
        return flag;
      }

      private static bool StringShape(
        string text,
        RtlApi.SCRIPT_ITEM[] items,
        Font font,
        int count,
        int[] visualToLogical,
        out ushort[] glyphs)
      {
        if (text == null || text.Length == 0 || items == null || items.Length < count + 1 || font == null || visualToLogical == null || visualToLogical.Length != count)
        {
          glyphs = (ushort[]) null;
          return false;
        }
        IntPtr zero1 = IntPtr.Zero;
        IntPtr hfont;
        try
        {
          hfont = font.ToHfont();
        }
        catch
        {
          font = new Font("Arial", font.Size, font.Style);
          hfont = font.ToHfont();
        }
        System.Drawing.Graphics graphics;
        lock (RtlRenderer.s_bmp)
          graphics = System.Drawing.Graphics.FromImage((Image) RtlRenderer.s_bmp);
        IntPtr hdc = graphics.GetHdc();
        IntPtr hgdiobj = GdiApi.SelectObject(hdc, hfont);
        IntPtr zero2 = IntPtr.Zero;
        int num1 = 10;
        ArrayList glyphs1 = new ArrayList();
        try
        {
          for (int index1 = 0; index1 < count; ++index1)
          {
            int index2 = visualToLogical[index1];
            int iCharPos = items[index2 + 1].iCharPos;
            string pwcChars = text.Substring(items[index2].iCharPos, iCharPos - items[index2].iCharPos);
            int num2 = 0;
            int cMaxGlyphs = 0;
            ushort[] pwOutGlyphs;
            int pcGlyphs;
            uint num3;
            do
            {
              cMaxGlyphs += pwcChars.Length * 3 / 2;
              pwOutGlyphs = new ushort[cMaxGlyphs];
              ushort[] numArray = new ushort[cMaxGlyphs];
              RtlApi.SCRIPT_VISATTR[] scriptVisattrArray = new RtlApi.SCRIPT_VISATTR[cMaxGlyphs];
              pcGlyphs = 0;
              num3 = RtlApi.ScriptShape(hdc, ref zero2, pwcChars, pwcChars.Length, cMaxGlyphs, ref items[index2].a, ref pwOutGlyphs[0], ref numArray[0], ref scriptVisattrArray[0], ref pcGlyphs);
              if (num3 == 2147746304U /*0x80040200*/)
                items[index2].a.val &= (ushort) 64512;
              else if (num3 != 0U && num3 != 2147942414U /*0x8007000E*/ || num3 != 0U && num2 >= num1)
              {
                glyphs = (ushort[]) null;
                return false;
              }
              ++num2;
            }
            while (num3 != 0U);
            if (num3 == 0U)
              RtlRenderer.AddGlyphs(glyphs1, pwOutGlyphs, pcGlyphs);
          }
        }
        finally
        {
          GdiApi.SelectObject(hdc, hgdiobj);
          graphics.ReleaseHdc(hdc);
          graphics.Dispose();
          GdiApi.DeleteObject(hfont);
        }
        glyphs = (ushort[]) glyphs1.ToArray(typeof (ushort));
        return true;
      }

      private static string SystemLayout(string line, PdfTrueTypeFont font, bool rtl)
      {
        if (line == null)
          throw new ArgumentNullException(nameof (line));
        if (font == null)
          throw new ArgumentNullException(nameof (font));
        ushort[] glyphs = (ushort[]) null;
        if ((RtlRenderer.GetGlyphIndices(line, font, rtl, out glyphs) ? 1 : 0) != 0 && glyphs != null && glyphs.Length != 0)
          return RtlRenderer.AddChars(font, glyphs);
        string line1 = RtlRenderer.CustomLayout(line, rtl);
        return RtlRenderer.AddChars(font, line1);
      }

      private static string[] SystemLayout(
        string line,
        PdfTrueTypeFont font,
        bool rtl,
        bool wordSpace)
      {
        if (line == null)
          throw new ArgumentNullException(nameof (line));
        if (font == null)
          throw new ArgumentNullException(nameof (font));
        if (!wordSpace)
          return new string[1]
          {
            RtlRenderer.SystemLayout(line, font, rtl)
          };
        string[] strArray1 = RtlRenderer.SplitLayout(line, font, rtl, wordSpace);
        string[] strArray2 = new string[strArray1.Length];
        int index = 0;
        for (int length = strArray1.Length; index < length; ++index)
          strArray2[index] = RtlRenderer.AddChars(font, strArray1[index]);
        return strArray2;
      }

      private static string[] SystemSplitLayout(
        string line,
        PdfTrueTypeFont font,
        bool rtl,
        bool wordSpace)
      {
        if (line == null)
          throw new ArgumentNullException(nameof (line));
        if (font == null)
          throw new ArgumentNullException(nameof (font));
        string[] strArray = (string[]) null;
        ushort[] glyphs = (ushort[]) null;
        if (!RtlRenderer.GetGlyphIndices(line, font, rtl, out glyphs) || glyphs == null || glyphs.Length == 0)
          return strArray;
        TtfReader ttfReader = (font.InternalFont as UnicodeTrueTypeFont).TtfReader;
        char[] chArray = new char[glyphs.Length];
        for (int index = 0; index < glyphs.Length; ++index)
        {
          int glyphIndex = (int) glyphs[index];
          TtfGlyphInfo glyph = ttfReader.GetGlyph(glyphIndex);
          if (!glyph.Empty)
            chArray[index] = (char) glyph.CharCode;
        }
        return new string(chArray).Split((char[]) null);
      }

      private static void WriteInLTR(
        char[] convertedData,
        char symbol,
        bool rtl,
        ref int indexCursor,
        ref int indexLength)
      {
        if (convertedData == null)
          throw new ArgumentNullException(nameof (convertedData));
        if (rtl)
        {
          for (int index = indexLength; index > 0; --index)
            convertedData[indexCursor - index] = convertedData[indexCursor - index + 1];
          convertedData[indexCursor] = symbol;
          ++indexLength;
        }
        else
        {
          for (int index = indexLength; index > 0; --index)
            convertedData[index + indexCursor] = convertedData[index + indexCursor - 1];
          convertedData[indexCursor] = symbol;
          ++indexLength;
        }
      }
    }
}
