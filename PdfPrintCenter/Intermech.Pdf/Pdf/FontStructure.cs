// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.FontStructure
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Exporting;
using Syncfusion.Pdf.Primitives;
using Syncfusion.Pdf.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;

#nullable disable
namespace Syncfusion.Pdf;

internal class FontStructure
{
  internal float DefaultGlyphWidth;
  internal Dictionary<int, string> differenceEncoding;
  internal Dictionary<int, string> differenceTable;
  public byte[] fontfilebytess;
  public MemoryStream fontStream;
  public PdfName fontType;
  public GraphicsPath Graphic;
  internal bool Is1C;
  internal bool IsContainFontfile2;
  internal bool isEmbedded;
  private bool isGetFontCalled;
  internal bool IsMappingDone;
  public bool IsSystemFontExist;
  public bool IsTextExtraction;
  internal bool IsType1Font;
  private int kkh;
  internal CffGlyphs m_cffGlyphs;
  private Dictionary<double, string> m_characterMapTable;
  private Dictionary<int, int> m_cidToGidReverseMapTable;
  private Dictionary<double, string> m_cidToGidTable;
  private bool m_containsCmap;
  private Font m_currentFont;
  private Dictionary<string, string> m_differencesDictionary;
  private PdfDictionary m_fontDictionary;
  private string m_fontEncoding;
  private FontFile2 m_fontfile2;
  private bool m_fontFileContainsCmap;
  private Dictionary<int, int> m_fontGlyphWidth;
  private Dictionary<int, int> m_fontGlyphWidthMapping;
  private string m_fontName;
  private float m_fontSize;
  private FontStyle m_fontStyle;
  private bool m_isCID;
  private bool m_isSameFont;
  private Dictionary<int, string> m_macEncodeTable;
  private Dictionary<int, int> m_octDecMapTable;
  private const string m_replacementCharacter = "�";
  private Dictionary<string, double> m_reverseMapTable;
  internal Dictionary<string, byte[]> m_type1FontGlyphs;
  private float m_type1GlyphHeight;
  private PrivateFontCollection pfc;
  internal Dictionary<string, int> ReverseDictMapping;
  private static Dictionary<double, string> tempMapTable = new Dictionary<double, string>();
  internal Dictionary<long, CffGlyphs> type1FontReference;
  internal static Dictionary<int, string> unicodeCharMapTable;

  public FontStructure()
  {
    this.m_containsCmap = true;
    this.pfc = new PrivateFontCollection();
    this.m_type1FontGlyphs = new Dictionary<string, byte[]>();
    this.differenceTable = new Dictionary<int, string>();
    this.differenceEncoding = new Dictionary<int, string>();
    this.type1FontReference = new Dictionary<long, CffGlyphs>();
    this.ReverseDictMapping = new Dictionary<string, int>();
    this.kkh = 1;
    this.m_cffGlyphs = new CffGlyphs();
    this.fontStream = new MemoryStream();
  }

  public FontStructure(IPdfPrimitive fontDictionary)
  {
    this.m_containsCmap = true;
    this.pfc = new PrivateFontCollection();
    this.m_type1FontGlyphs = new Dictionary<string, byte[]>();
    this.differenceTable = new Dictionary<int, string>();
    this.differenceEncoding = new Dictionary<int, string>();
    this.type1FontReference = new Dictionary<long, CffGlyphs>();
    this.ReverseDictMapping = new Dictionary<string, int>();
    this.kkh = 1;
    this.m_cffGlyphs = new CffGlyphs();
    this.fontStream = new MemoryStream();
    this.m_fontDictionary = fontDictionary as PdfDictionary;
    this.fontType = this.m_fontDictionary.Items[new PdfName("Subtype")] as PdfName;
  }

  private int CalculateCheckSum(byte[] bytes)
  {
    if (bytes == null)
      throw new ArgumentNullException(nameof (bytes));
    int num1 = 0;
    int num2 = 0;
    int num3 = 0;
    int num4 = 0;
    int num5 = 0;
    int num6 = 0;
    for (int index1 = bytes.Length / 4; num6 < index1; ++num6)
    {
      int num7 = num5;
      byte[] numArray1 = bytes;
      int index2 = num1;
      int num8 = index2 + 1;
      int num9 = (int) numArray1[index2] & (int) byte.MaxValue;
      num5 = num7 + num9;
      int num10 = num4;
      byte[] numArray2 = bytes;
      int index3 = num8;
      int num11 = index3 + 1;
      int num12 = (int) numArray2[index3] & (int) byte.MaxValue;
      num4 = num10 + num12;
      int num13 = num3;
      byte[] numArray3 = bytes;
      int index4 = num11;
      int num14 = index4 + 1;
      int num15 = (int) numArray3[index4] & (int) byte.MaxValue;
      num3 = num13 + num15;
      int num16 = num2;
      byte[] numArray4 = bytes;
      int index5 = num14;
      num1 = index5 + 1;
      int num17 = (int) numArray4[index5] & (int) byte.MaxValue;
      num2 = num16 + num17;
    }
    return num2 + (num3 << 8) + (num4 << 16 /*0x10*/) + (num5 << 24);
  }

  private string CheckContainInvalidChar(string charvalue)
  {
    foreach (char ch in charvalue.ToCharArray())
    {
      if (ch == ' ')
        charvalue = " ";
    }
    return charvalue;
  }

  internal static string CheckFontName(string fontName)
  {
    string str1 = fontName;
    if (str1.Contains("#20"))
      str1 = str1.Replace("#20", " ");
    string[] sourceArray = new string[1]{ "" };
    int length = 0;
    for (int startIndex = 0; startIndex < str1.Length; ++startIndex)
    {
      string str2 = str1.Substring(startIndex, 1);
      if ("ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".Contains(str2) && startIndex > 0 && !"ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".Contains(str1[startIndex - 1].ToString()))
      {
        ++length;
        string[] destinationArray = new string[length + 1];
        Array.Copy((Array) sourceArray, 0, (Array) destinationArray, 0, length);
        sourceArray = destinationArray;
      }
      string[] strArray;
      IntPtr index;
      (strArray = sourceArray)[(int) (index = (IntPtr) length)] = strArray[(int) index] + str2;
    }
    fontName = string.Empty;
    foreach (string str3 in sourceArray)
      fontName = $"{fontName}{str3} ";
    if (fontName.Contains("Times"))
      fontName = "Times New Roman";
    if (fontName == "Bookshelf Symbol Seven")
      fontName = "Bookshelf Symbol 7";
    if (fontName.Contains("Regular"))
      fontName = fontName.Replace("Regular", "");
    else if (fontName.Contains("Bold"))
      fontName = fontName.Replace("Bold", "");
    else if (fontName.Contains("Italic"))
      fontName = fontName.Replace("Italic", "");
    fontName = fontName.Trim();
    return fontName;
  }

  internal FontStyle CheckFontStyle(string fontName)
  {
    if (!fontName.Contains("Regular"))
    {
      if (fontName.Contains("Bold"))
        return FontStyle.Bold;
      if (fontName.Contains("Italic"))
        return FontStyle.Italic;
    }
    return FontStyle.Regular;
  }

  public string Decode(string textToDecode, bool isSameFont)
  {
    string str1 = string.Empty;
    string str2 = textToDecode;
    this.m_isSameFont = isSameFont;
    switch (str2[0])
    {
      case '(':
        if (str2.Contains("\\\n"))
        {
          StringBuilder stringBuilder = new StringBuilder(str2);
          stringBuilder.Replace("\\\n", "");
          str2 = stringBuilder.ToString();
        }
        str1 = this.GetLiteralString(str2.Substring(1, str2.Length - 2));
        if (this.m_fontDictionary.ContainsKey("Encoding") && (object) (this.m_fontDictionary["Encoding"] as PdfName) != null && (this.m_fontDictionary["Encoding"] as PdfName).Value == "Identity-H")
        {
          string str3 = this.SkipEscapeSequence(str1);
          List<byte> byteList = new List<byte>();
          foreach (char ch in str3)
            byteList.Add((byte) ch);
          str1 = Encoding.BigEndianUnicode.GetString(byteList.ToArray());
          if (str1.Contains("\\"))
          {
            str1 = str1.Replace("\\", "\\\\");
            break;
          }
          break;
        }
        break;
      case '<':
        str1 = this.GetHexaDecimalString(str2.Substring(1, str2.Length - 2));
        break;
      case '[':
        if (str2.Contains("\\\n"))
        {
          StringBuilder stringBuilder = new StringBuilder(str2);
          stringBuilder.Replace("\\\n", "");
          str2 = stringBuilder.ToString();
        }
        int num1;
        for (string str4 = str2.Substring(1, str2.Length - 2); str4.Length > 0; str4 = str4.Substring(num1 + 1, str4.Length - num1 - 1))
        {
          bool flag = false;
          int num2 = str4.IndexOf('(');
          num1 = str4.IndexOf(')');
          int num3 = str4.IndexOf('<');
          int num4 = str4.IndexOf('>');
          if (num3 < num2 && num3 > -1)
          {
            num2 = num3;
            num1 = num4;
            flag = true;
          }
          if (num2 < 0)
          {
            num2 = str4.IndexOf('<');
            num1 = str4.IndexOf('>');
            if (num2 >= 0)
              flag = true;
            else
              break;
          }
          else if (num1 > 0)
          {
            while (str4[num1 - 1] == '\\' && str4.IndexOf(')', num1 + 1) >= 0)
              num1 = str4.IndexOf(')', num1 + 1);
          }
          string str5 = str4.Substring(num2 + 1, num1 - num2 - 1);
          str1 = !flag ? str1 + this.GetLiteralString(str5) : str1 + this.GetHexaDecimalString(str5);
        }
        break;
    }
    if (str1.Contains("\0") && !this.CharacterMapTable.ContainsKey(0.0))
      str1 = str1.Replace("\0", "");
    if (!this.IsTextExtraction)
      str1 = this.SkipEscapeSequence(str1);
    if (this.FontEncoding != "Identity-H" && this.fontType.Value != "TrueType" || this.FontEncoding == "Identity-H" && this.IsType1Font || this.FontEncoding == "Identity-H" && !this.isEmbedded)
    {
      this.IsMappingDone = true;
      str1 = this.CharacterMapTable == null || this.CharacterMapTable.Count <= 0 ? (this.DifferencesDictionary == null || this.DifferencesDictionary.Count <= 0 ? this.SkipEscapeSequence(str1) : this.MapDifferences(str1)) : this.MapCharactersFromTable(str1);
    }
    if (this.m_cidToGidTable != null)
      str1 = this.MapCidToGid(str1);
    if (this.FontName == "ZapfDingbats")
      str1 = this.MapZapf(str1);
    return str1;
  }

  private MemoryStream DecodeASCII85Stream(MemoryStream encodedStream)
  {
    byte[] buffer = new ASCII85().decode(encodedStream.GetBuffer());
    MemoryStream memoryStream = new MemoryStream(buffer, 0, buffer.Length, true, true);
    memoryStream.Position = 0L;
    return memoryStream;
  }

  private MemoryStream DecodeFlateStream(MemoryStream encodedStream)
  {
    encodedStream.Position = 0L;
    encodedStream.ReadByte();
    encodedStream.ReadByte();
    DeflateStream deflateStream = new DeflateStream((Stream) encodedStream, CompressionMode.Decompress, true);
    byte[] buffer = new byte[4096 /*0x1000*/];
    MemoryStream memoryStream = new MemoryStream();
    while (true)
    {
      int count = deflateStream.Read(buffer, 0, 4096 /*0x1000*/);
      if (count > 0)
        memoryStream.Write(buffer, 0, count);
      else
        break;
    }
    return memoryStream;
  }

  public string DecodeTextExtraction(string textToDecode, bool isSameFont)
  {
    string str1 = string.Empty;
    string str2 = textToDecode;
    this.m_isSameFont = isSameFont;
    switch (str2[0])
    {
      case '(':
        if (str2.Contains("\\\n"))
        {
          StringBuilder stringBuilder = new StringBuilder(str2);
          stringBuilder.Replace("\\\n", "");
          str2 = stringBuilder.ToString();
        }
        str1 = this.GetLiteralString(str2.Substring(1, str2.Length - 2));
        if (this.m_fontDictionary.ContainsKey("Encoding") && (object) (this.m_fontDictionary["Encoding"] as PdfName) != null && (this.m_fontDictionary["Encoding"] as PdfName).Value == "Identity-H")
        {
          string str3 = this.SkipEscapeSequence(str1);
          List<byte> byteList = new List<byte>();
          foreach (char ch in str3)
            byteList.Add((byte) ch);
          str1 = Encoding.BigEndianUnicode.GetString(byteList.ToArray());
          break;
        }
        break;
      case '<':
        str1 = this.GetHexaDecimalString(str2.Substring(1, str2.Length - 2));
        break;
      case '[':
        if (str2.Contains("\\\n"))
        {
          StringBuilder stringBuilder = new StringBuilder(str2);
          stringBuilder.Replace("\\\n", "");
          str2 = stringBuilder.ToString();
        }
        int num1;
        for (string str4 = str2.Substring(1, str2.Length - 2); str4.Length > 0; str4 = str4.Substring(num1 + 1, str4.Length - num1 - 1))
        {
          bool flag = false;
          int num2 = str4.IndexOf('(');
          num1 = str4.IndexOf(')');
          int num3 = str4.IndexOf('<');
          int num4 = str4.IndexOf('>');
          if (num3 < num2 && num3 > -1)
          {
            num2 = num3;
            num1 = num4;
            flag = true;
          }
          if (num2 < 0)
          {
            num2 = str4.IndexOf('<');
            num1 = str4.IndexOf('>');
            if (num2 >= 0)
              flag = true;
            else
              break;
          }
          else if (num1 > 0)
          {
            while (str4[num1 - 1] == '\\' && str4.IndexOf(')', num1 + 1) >= 0)
              num1 = str4.IndexOf(')', num1 + 1);
          }
          string str5 = str4.Substring(num2 + 1, num1 - num2 - 1);
          str1 = !flag ? str1 + this.GetLiteralString(str5) : str1 + this.GetHexaDecimalString(str5);
        }
        break;
    }
    if (this.FontEncoding != "Identity-H" || this.FontEncoding == "Identity-H" && this.CurrentFont == null || this.FontEncoding == "Identity-H" && this.m_containsCmap)
    {
      this.IsMappingDone = true;
      str1 = this.CharacterMapTable == null || this.CharacterMapTable.Count <= 0 ? (this.DifferencesDictionary == null || this.DifferencesDictionary.Count <= 0 ? this.SkipEscapeSequence(str1) : this.MapDifferences(str1)) : this.MapCharactersFromTable(str1);
    }
    if (this.m_cidToGidTable != null)
      str1 = this.MapCidToGid(str1);
    if (this.FontName == "ZapfDingbats")
      str1 = this.MapZapf(str1);
    return str1;
  }

  public List<string> DecodeTextTJ(string textToDecode, bool isSameFont)
  {
    string text = string.Empty;
    string str1 = textToDecode;
    this.m_isSameFont = isSameFont;
    List<string> stringList = new List<string>();
    switch (str1[0])
    {
      case '(':
        if (str1.Contains("\\\n"))
        {
          StringBuilder stringBuilder = new StringBuilder(str1);
          stringBuilder.Replace("\\\n", "");
          str1 = stringBuilder.ToString();
        }
        text = this.GetLiteralString(str1.Substring(1, str1.Length - 2));
        if (this.m_fontDictionary.ContainsKey("Encoding") && (object) (this.m_fontDictionary["Encoding"] as PdfName) != null && (this.m_fontDictionary["Encoding"] as PdfName).Value == "Identity-H")
        {
          string str2 = this.SkipEscapeSequence(text);
          List<byte> byteList = new List<byte>();
          foreach (char ch in str2)
            byteList.Add((byte) ch);
          text = Encoding.BigEndianUnicode.GetString(byteList.ToArray());
          break;
        }
        break;
      case '<':
        text = this.GetHexaDecimalString(str1.Substring(1, str1.Length - 2));
        break;
      case '[':
        if (str1.Contains("\\\n"))
        {
          StringBuilder stringBuilder = new StringBuilder(str1);
          stringBuilder.Replace("\\\n", "");
          str1 = stringBuilder.ToString();
        }
        int num1;
        for (string str3 = str1.Substring(1, str1.Length - 2); str3.Length > 0; str3 = str3.Substring(num1 + 1, str3.Length - num1 - 1))
        {
          bool flag = false;
          int length = str3.IndexOf('(');
          num1 = str3.IndexOf(')');
          int num2 = str3.IndexOf('<');
          int num3 = str3.IndexOf('>');
          if (num2 < length && num2 > -1)
          {
            length = num2;
            num1 = num3;
            flag = true;
          }
          if (length < 0)
          {
            length = str3.IndexOf('<');
            num1 = str3.IndexOf('>');
            if (length >= 0)
              flag = true;
            else
              break;
          }
          if (num1 < 0 && str3.Length > 0)
          {
            string str4 = str3;
            stringList.Add(str4);
            break;
          }
          if (num1 > 0)
          {
            while (str3[num1 - 1] == '\\' && (num1 - 1 <= 0 || str3[num1 - 2] != '\\') && str3.IndexOf(')', num1 + 1) >= 0)
              num1 = str3.IndexOf(')', num1 + 1);
          }
          if (length != 0)
          {
            string str5 = str3.Substring(0, length);
            stringList.Add(str5);
          }
          string str6 = str3.Substring(length + 1, num1 - length - 1);
          string str7;
          if (flag)
          {
            str7 = this.GetHexaDecimalString(str6);
            text += str7;
          }
          else
          {
            str7 = this.GetLiteralString(str6);
            text += str7;
          }
          if (str7.Contains("(") || str7.Contains(")"))
          {
            char[] chArray = new char[1]{ '\\' };
            string[] strArray = str7.Split(chArray);
            str7 = "" + strArray[0];
            for (int index = 1; index < strArray.Length; ++index)
            {
              string str8 = strArray[index];
              if (str8.Length != 0)
                str7 = str8[0] == '(' || str8[0] == ')' ? str7 + strArray[index] : $"{str7}\\{str8[index].ToString()}";
            }
          }
          if (str7.Contains("\0") && !this.CharacterMapTable.ContainsKey(0.0))
            str7 = str7.Replace("\0", "");
          if (!this.IsTextExtraction)
            str7 = this.SkipEscapeSequence(str7);
          if (this.FontEncoding != "Identity-H" && this.fontType.Value != "TrueType" || this.FontEncoding == "Identity-H" && this.IsType1Font || this.FontEncoding == "Identity-H" && !this.isEmbedded)
          {
            this.IsMappingDone = true;
            str7 = this.CharacterMapTable == null || this.CharacterMapTable.Count <= 0 ? (this.DifferencesDictionary == null || this.DifferencesDictionary.Count <= 0 ? this.SkipEscapeSequence(str7) : this.MapDifferences(str7)) : this.MapCharactersFromTable(str7);
          }
          if (this.m_cidToGidTable != null)
            str7 = this.MapCidToGid(str7);
          if (str7.Length > 0)
          {
            if (str7[0] >= '\u0E00' && str7[0] <= '\u0E7F' && stringList.Count > 0)
            {
              string str9 = stringList[0];
              string str10 = str9.Remove(str9.Length - 1) + str7;
              stringList[0] = str10 + "s";
            }
            else if ((str7[0] == ' ' || str7[0] == '/') && str7.Length > 1)
            {
              if (str7[1] >= '\u0E00' && str7[1] <= '\u0E7F' && stringList.Count > 0)
              {
                string str11 = stringList[0];
                string str12 = str11.Remove(str11.Length - 1) + str7;
                stringList[0] = str12 + "s";
              }
              else
              {
                string str13 = str7 + "s";
                stringList.Add(str13);
              }
            }
            else
            {
              string str14 = str7 + "s";
              stringList.Add(str14);
            }
          }
          else
          {
            string str15 = str7 + "s";
            stringList.Add(str15);
          }
        }
        break;
    }
    this.SkipEscapeSequence(text);
    return stringList;
  }

  private string EscapeSymbols(string text)
  {
    while (text.Contains("\n"))
      text = text.Replace("\n", "");
    return text;
  }

  ~FontStructure() => FontStructure.tempMapTable = new Dictionary<double, string>();

  public static bool GetBit(int n, byte bit) => (n & 1 << (int) bit) != 0;

  private Dictionary<double, string> GetCharacterMapTable()
  {
    Dictionary<double, string> characterMapTable = new Dictionary<double, string>();
    if (this.m_fontDictionary.ContainsKey("ToUnicode"))
    {
      IPdfPrimitive font = this.m_fontDictionary["ToUnicode"];
      PdfStream pdfStream = (object) (font as PdfReferenceHolder) == null ? font as PdfStream : (font as PdfReferenceHolder).Object as PdfStream;
      if (pdfStream != null)
      {
        pdfStream.Decompress();
        string str1 = Encoding.UTF8.GetString(pdfStream.Data, 0, pdfStream.Data.Length);
        bool flag1 = false;
        bool flag2 = false;
        int num1 = str1.IndexOf("begincmap");
        int num2 = str1.IndexOf("endcmap");
        int startIndex1 = num1;
        int startIndex2 = num1;
        int num3 = num2;
        if (startIndex1 == -1)
          return characterMapTable;
        string str2;
        char ch1;
        do
        {
          if (!flag1)
          {
            startIndex2 = str1.IndexOf("beginbfchar", startIndex1);
            if (startIndex2 < 0)
            {
              flag2 = false;
              startIndex2 = num1;
              startIndex1 = num1;
              num3 = num2;
            }
            else
            {
              num3 = str1.IndexOf("endbfchar", startIndex2);
              startIndex1 = num3;
              flag2 = true;
            }
          }
          if (!flag2)
          {
            int num4 = str1.IndexOf("beginbfrange", startIndex1);
            if (num4 < 0)
            {
              flag1 = false;
            }
            else
            {
              int num5 = str1.IndexOf("endbfrange", startIndex1 + 5);
              startIndex2 = num4;
              num3 = num5;
              startIndex1 = num3;
              flag1 = true;
            }
          }
          if (flag2 | flag1)
          {
            str2 = str1.Substring(startIndex2, num3 - startIndex2);
            List<string> stringList = new List<string>();
            if (flag2)
            {
              char[] chArray = new char[2]{ '\n', '\r' };
              foreach (string hexCode1 in str2.Split(chArray))
              {
                List<string> hexCode2 = FontStructure.GetHexCode(hexCode1);
                if (hexCode2.Count > 1)
                {
                  if (hexCode2[1].Length > 4)
                  {
                    string str3 = hexCode2[1].Replace(" ", "");
                    string charvalue = "";
                    int num6 = str3.Length / 4;
                    for (int index = 0; index < num6; ++index)
                    {
                      char ch2 = (char) long.Parse(str3.Substring(0, 4), NumberStyles.HexNumber);
                      str3 = str3.Substring(4);
                      charvalue += ch2.ToString();
                    }
                    string str4 = this.CheckContainInvalidChar(charvalue);
                    if (!characterMapTable.ContainsKey((double) long.Parse(hexCode2[0], NumberStyles.HexNumber)))
                      characterMapTable.Add((double) long.Parse(hexCode2[0], NumberStyles.HexNumber), str4.ToString());
                  }
                  else if (!characterMapTable.ContainsKey((double) long.Parse(hexCode2[0], NumberStyles.HexNumber)))
                  {
                    Dictionary<double, string> dictionary = characterMapTable;
                    double key = (double) long.Parse(hexCode2[0], NumberStyles.HexNumber);
                    ch1 = (char) long.Parse(hexCode2[1], NumberStyles.HexNumber);
                    string str5 = ch1.ToString();
                    dictionary.Add(key, str5);
                  }
                }
              }
              continue;
            }
            continue;
          }
          goto label_61;
label_4:;
        }
        while (!flag1);
        char[] chArray1 = new char[2]{ '\n', '\r' };
        string[] strArray = str2.Split(chArray1);
        for (int index1 = 0; index1 < strArray.Length; ++index1)
        {
          if (strArray[index1].Contains("["))
          {
            int startIndex3 = strArray[index1].IndexOf("[");
            int num7 = strArray[index1].IndexOf("]");
            string hexCode3;
            if (num7 == -1)
            {
              string str6 = strArray[index1].Substring(startIndex3, strArray[index1].Length - startIndex3);
              for (++index1; !strArray[index1].Contains("]"); ++index1)
                str6 += strArray[index1];
              hexCode3 = str6 + strArray[index1].Substring(0, strArray[index1].IndexOf("]"));
            }
            else
              hexCode3 = strArray[index1].Substring(startIndex3, num7 - startIndex3);
            List<string> stringList = new List<string>();
            List<string> hexCode4 = FontStructure.GetHexCode(hexCode3);
            string hexCode5 = " ";
            List<string> hexCode6;
            if (num7 == -1)
            {
              for (int index2 = 1; index2 <= index1; ++index2)
                hexCode5 += strArray[index2];
              hexCode6 = FontStructure.GetHexCode(hexCode5);
            }
            else
              hexCode6 = FontStructure.GetHexCode(strArray[index1]);
            if (hexCode6.Count > 1)
            {
              double num8 = (double) long.Parse(hexCode6[0], NumberStyles.HexNumber);
              double num9 = (double) long.Parse(hexCode6[1], NumberStyles.HexNumber);
              int index3 = 0;
              double key = num8;
              double num10 = 0.0;
              while (key <= num9)
              {
                char ch3 = (char) (double) long.Parse(((int) (double) Convert.ToInt64(hexCode4[index3], 16 /*0x10*/)).ToString("x"), NumberStyles.HexNumber);
                if (!characterMapTable.ContainsKey(key))
                  characterMapTable.Add(key, ch3.ToString());
                ++key;
                ++num10;
                ++index3;
              }
            }
          }
          else
          {
            List<string> hexCode = FontStructure.GetHexCode(strArray[index1]);
            if (hexCode.Count == 3)
            {
              double num11 = (double) long.Parse(hexCode[0], NumberStyles.HexNumber);
              double num12 = (double) long.Parse(hexCode[1], NumberStyles.HexNumber);
              double int64 = (double) Convert.ToInt64(hexCode[2], 16 /*0x10*/);
              double key = num11;
              double num13 = 0.0;
              while (key <= num12)
              {
                char ch4 = (char) (double) long.Parse(((int) (int64 + num13)).ToString("x"), NumberStyles.HexNumber);
                if (!characterMapTable.ContainsKey(key))
                  characterMapTable.Add(key, ch4.ToString());
                ++key;
                ++num13;
              }
            }
            else if (hexCode.Count > 1)
            {
              int num14 = hexCode.Count / 2;
              for (int index4 = 0; index4 < num14; ++index4)
              {
                if (!characterMapTable.ContainsKey((double) long.Parse(hexCode[index4], NumberStyles.HexNumber)))
                {
                  Dictionary<double, string> dictionary = characterMapTable;
                  double key = (double) long.Parse(hexCode[index4], NumberStyles.HexNumber);
                  ch1 = (char) long.Parse(hexCode[num14 + index4], NumberStyles.HexNumber);
                  string str7 = ch1.ToString();
                  dictionary.Add(key, str7);
                }
              }
            }
          }
        }
        goto label_4;
      }
    }
label_61:
    if (this.m_isSameFont)
    {
      foreach (KeyValuePair<double, string> keyValuePair in characterMapTable)
      {
        if (!FontStructure.tempMapTable.ContainsKey(keyValuePair.Key))
        {
          FontStructure.tempMapTable.Add(keyValuePair.Key, keyValuePair.Value);
        }
        else
        {
          FontStructure.tempMapTable.Remove(keyValuePair.Key);
          FontStructure.tempMapTable.Add(keyValuePair.Key, keyValuePair.Value);
        }
      }
    }
    return characterMapTable;
  }

  internal static string GetCharCode(string decodedCharacter)
  {
    char ch = decodedCharacter.ToCharArray()[0];
    switch (decodedCharacter)
    {
      case " ":
        return "space";
      case "!":
        return "exclam";
      case "\"":
        return "quotedbl";
      case "#":
        return "numbersign";
      case "$":
        return "dollar";
      case "%":
        return "percent";
      case "&":
        return "ampersand";
      case "'":
        return "quotesingle";
      case "(":
        return "parenleft";
      case ")":
        return "parenright";
      case "*":
        return "asterisk";
      case "+":
        return "plus";
      case ",":
        return "comma";
      case "-":
        return "hyphen";
      case ".":
        return "period";
      case "...":
        return "ellipsis";
      case "/":
        return "slash";
      case "0":
        return "zero";
      case "1":
        return "one";
      case "1/2":
        return "onehalf";
      case "1/4":
        return "onequarter";
      case "2":
        return "two";
      case "3":
        return "three";
      case "4":
        return "four";
      case "5":
        return "five";
      case "6":
        return "six";
      case "7":
        return "seven";
      case "8":
        return "eight";
      case "9":
        return "nine";
      case ":":
        return "colon";
      case ";":
        return "semicolon";
      case "<":
        return "less";
      case "=":
        return "equal";
      case ">":
        return "greater";
      case "?":
        return "question";
      case "@":
        return "at";
      case "[":
        return "bracketleft";
      case "\\":
        return "backslash";
      case "]":
        return "bracketright";
      case "^":
        return "asciicircum";
      case "_":
        return "underscore";
      case "`":
        return "grave";
      case "oe":
        return "oe";
      case "{":
        return "braceleft";
      case "|":
        return "bar";
      case "}":
        return "braceright";
      case "~":
        return "asciitilde";
      case "¡":
        return "exclamdown";
      case "¢":
        return "cent";
      case "¤":
        return "currency1";
      case "§":
        return "section";
      case "¨":
        return "dieresis";
      case "©":
        return "copyright";
      case "ª":
        return "ordfeminine";
      case "«":
        return "guillemotleft4";
      case "¬":
        return "logicalnot";
      case "®":
        return "registered";
      case "¯":
        return "macron";
      case "°":
        return "degree";
      case "±":
        return "plusminus";
      case "´":
        return "acute";
      case "¶":
        return "paragraph";
      case "·":
        return "periodcentered";
      case "¸":
        return "cedilla";
      case "\u00B9":
        return "onesuperior";
      case "º":
        return "ordmasculine";
      case "»":
        return "guillemotright4";
      case "¿":
        return "questiondown";
      case "Ä":
        return "Adieresis";
      case "×":
        return "multiply";
      case "Ü":
        return "Udieresis";
      case "ß":
        return "germandbls";
      case "à":
        return "agrave";
      case "ã":
        return "atilde";
      case "ä":
        return "adieresis";
      case "å":
        return "aring";
      case "ç":
        return "ccedilla";
      case "è":
        return "egrave";
      case "é":
        return "eacute";
      case "ê":
        return "ecircumflex";
      case "ë":
        return "edieresis";
      case "ì":
        return "igrave";
      case "í":
        return "iacute";
      case "î":
        return "icircumflex";
      case "ï":
        return "idieresis";
      case "ð":
        return "eth";
      case "ñ":
        return "ntilde";
      case "ò":
        return "ograve";
      case "ó":
        return "oacute";
      case "ô":
        return "ocircumflex";
      case "õ":
        return "otilde";
      case "ö":
        return "odieresis";
      case "÷":
        return "divide";
      case "ø":
        return "oslash";
      case "ü":
        return "udieresis";
      case "č":
        return "ccaron";
      case "ı":
        return "dotlessi";
      case "ł":
        return "lslash";
      case "Š":
        return "Scaron";
      case "š":
        return "scaron";
      case "ž":
        return "zcaron";
      case "ƒ":
        return "florin";
      case "ˆ":
        return "circumflex";
      case "ˇ":
        return "caron";
      case "˘":
        return "breve";
      case "˙":
        return "dotaccent";
      case "˚":
        return "ring";
      case "˛":
        return "ogonek";
      case "˝":
        return "hungarumlaut";
      case "μ":
        return "mu";
      case "–":
        return "endash";
      case "—":
        return "emdash";
      case "‘":
        return "quoteleft";
      case "’":
        return "quoteright";
      case "‚":
        return "quotesinglbase";
      case "“":
        return "quotedblleft";
      case "”":
        return "quotedblright";
      case "„":
        return "quotedblbase";
      case "†":
        return "dagger";
      case "‡":
        return "daggerdbl";
      case "•":
        return "bullet";
      case "‰":
        return "perthousand";
      case "‹":
        return "guilsinglleft";
      case "›":
        return "guilsinglright";
      case "⁄":
        return "fraction";
      case "−":
        return "minus";
      default:
        if (ch == '\u0092')
          decodedCharacter = "quoteright";
        return decodedCharacter;
    }
  }

  private Dictionary<double, string> GetCidToGidTable(byte[] cidTOGidmap)
  {
    Dictionary<double, string> cidToGidTable = new Dictionary<double, string>();
    byte[] bytes = new byte[2];
    int key = 0;
    int num;
    for (int index = 0; index < cidTOGidmap.Length; index = num + 1)
    {
      bytes[0] = cidTOGidmap[index];
      bytes[1] = cidTOGidmap[num = index + 1];
      string str = Encoding.ASCII.GetString(bytes).Replace("\0", "");
      cidToGidTable.Add((double) key, str);
      ++key;
    }
    return cidToGidTable;
  }

  private Dictionary<string, string> GetDifferencesDictionary()
  {
    Dictionary<string, string> differencesDictionary = new Dictionary<string, string>();
    PdfDictionary pdfDictionary = (PdfDictionary) null;
    if (this.m_fontDictionary.ContainsKey("Encoding"))
    {
      if ((object) (this.m_fontDictionary["Encoding"] as PdfReferenceHolder) != null)
        pdfDictionary = (this.m_fontDictionary["Encoding"] as PdfReferenceHolder).Object as PdfDictionary;
      else if (this.m_fontDictionary["Encoding"] is PdfDictionary)
        pdfDictionary = this.m_fontDictionary["Encoding"] as PdfDictionary;
      if (pdfDictionary == null || !pdfDictionary.ContainsKey("Differences"))
        return differencesDictionary;
      int num = 0;
      PdfArray pdfArray = pdfDictionary["Differences"] as PdfArray;
      for (int index = 0; index < pdfArray.Count; ++index)
      {
        string empty = string.Empty;
        if (pdfArray[index] is PdfNumber)
          num = int.Parse((pdfArray[index] as PdfNumber).FloatValue.ToString());
        else if ((object) (pdfArray[index] as PdfName) != null)
        {
          string decodedCharacter1 = (pdfArray[index] as PdfName).Value;
          if (this.fontType.Value == "Type1" && decodedCharacter1 == ".notdef")
          {
            string decodedCharacter2 = " ";
            differencesDictionary.Add(num.ToString(), FontStructure.GetLatinCharacter(decodedCharacter2));
            ++num;
          }
          else
          {
            string specialCharacter = FontStructure.GetSpecialCharacter(FontStructure.GetLatinCharacter(decodedCharacter1));
            differencesDictionary.Add(num.ToString(), FontStructure.GetLatinCharacter(specialCharacter));
            ++num;
          }
        }
      }
    }
    return differencesDictionary;
  }

  private bool GetFlag(byte bit)
  {
    --bit;
    return FontStructure.GetBit(this.Flags.IntValue, bit);
  }

  private PdfNumber GetFlagValue()
  {
    if (this.FontEncoding != "Identity-H")
    {
      if (this.m_fontDictionary.Items.ContainsKey(new PdfName("FontDescriptor")))
      {
        PdfReferenceHolder pdfReferenceHolder = this.m_fontDictionary.Items[new PdfName("FontDescriptor")] as PdfReferenceHolder;
        if (pdfReferenceHolder != (PdfReferenceHolder) null && pdfReferenceHolder.Object is PdfDictionary && pdfReferenceHolder.Object is PdfDictionary pdfDictionary && pdfDictionary.Items.ContainsKey(new PdfName("Flags")))
          return pdfDictionary.Items[new PdfName("Flags")] as PdfNumber;
      }
    }
    else if (this.m_fontDictionary.Items.ContainsKey(new PdfName("DescendantFonts")))
    {
      if (this.m_fontDictionary.Items[new PdfName("DescendantFonts")] is PdfArray)
      {
        if (this.m_fontDictionary.Items[new PdfName("DescendantFonts")] is PdfArray pdfArray1 && (object) (pdfArray1[0] as PdfReferenceHolder) != null)
        {
          PdfReferenceHolder pdfReferenceHolder = pdfArray1[0] as PdfReferenceHolder;
          if (pdfReferenceHolder != (PdfReferenceHolder) null && pdfReferenceHolder.Object is PdfDictionary && pdfReferenceHolder.Object is PdfDictionary pdfDictionary && pdfDictionary.Items.ContainsKey(new PdfName("FontDescriptor")))
          {
            if (pdfDictionary.Items[new PdfName("FontDescriptor")] is PdfDictionary)
            {
              PdfDictionary pdfDictionary1 = pdfDictionary.Items[new PdfName("FontDescriptor")] as PdfDictionary;
            }
            if ((object) (pdfDictionary.Items[new PdfName("FontDescriptor")] as PdfReferenceHolder) != null && (pdfDictionary.Items[new PdfName("FontDescriptor")] as PdfReferenceHolder).Object is PdfDictionary pdfDictionary2 && pdfDictionary2.Items.ContainsKey(new PdfName("Flags")))
              return pdfDictionary2.Items[new PdfName("Flags")] as PdfNumber;
          }
        }
      }
      else
      {
        PdfReferenceHolder pdfReferenceHolder = this.m_fontDictionary.Items[new PdfName("DescendantFonts")] as PdfReferenceHolder;
        if (pdfReferenceHolder != (PdfReferenceHolder) null)
        {
          PdfArray pdfArray2 = pdfReferenceHolder.Object as PdfArray;
          if ((object) (pdfArray2[0] as PdfReferenceHolder) != null)
          {
            PdfName pdfName = ((pdfArray2[0] as PdfReferenceHolder).Object as PdfDictionary)["Subtype"] as PdfName;
            if (!(pdfName.Value == "CIDFontType2"))
            {
              int num = pdfName.Value == "CIDFontType0" ? 1 : 0;
            }
          }
        }
      }
    }
    return (PdfNumber) null;
  }

  public Font GetFont(float size)
  {
    MemoryStream memoryStream = new MemoryStream();
    this.isGetFontCalled = true;
    string fontName = this.FontName;
    FontStyle style = this.FontStyle != FontStyle.Regular ? this.FontStyle : this.CheckFontStyle(this.FontName);
    if (this.IsSystemFontExist)
      return new Font(fontName, size, style);
    Font font = new Font(FontStructure.CheckFontName(fontName), size, style);
    if (this.FontEncoding == "Identity-H")
    {
      try
      {
        PdfDictionary fontDictionary = this.m_fontDictionary;
        if (fontDictionary.ContainsKey("DescendantFonts"))
        {
          PdfArray pdfArray = (PdfArray) null;
          if (fontDictionary["DescendantFonts"] is PdfArray)
            pdfArray = fontDictionary["DescendantFonts"] as PdfArray;
          if ((object) (fontDictionary["DescendantFonts"] as PdfReferenceHolder) != null)
            pdfArray = (fontDictionary["DescendantFonts"] as PdfReferenceHolder).Object as PdfArray;
          if (!(pdfArray[0] is PdfDictionary pdfDictionary))
            pdfDictionary = (pdfArray[0] as PdfReferenceHolder).Object as PdfDictionary;
          if (pdfDictionary.ContainsKey("CIDToGIDMap") && (object) (pdfDictionary["CIDToGIDMap"] as PdfReferenceHolder) != null)
          {
            PdfStream pdfStream = (pdfDictionary["CIDToGIDMap"] as PdfReferenceHolder).Object as PdfStream;
            PdfDictionary streamDictionary = (pdfDictionary["CIDToGIDMap"] as PdfReferenceHolder).Object as PdfDictionary;
            MemoryStream encodedStream = pdfStream.InternalStream;
            if (streamDictionary.ContainsKey("Filter"))
            {
              string[] fontFilter = this.GetFontFilter(streamDictionary);
              if (fontFilter != null)
              {
                for (int index = 0; index < fontFilter.Length; ++index)
                {
                  switch (fontFilter[index])
                  {
                    case "A85":
                    case "ASCII85Decode":
                      encodedStream = this.DecodeASCII85Stream(encodedStream);
                      break;
                    case "FlateDecode":
                      encodedStream = this.DecodeFlateStream(encodedStream);
                      break;
                  }
                }
              }
            }
            encodedStream.Position = 0L;
            this.m_cidToGidTable = this.GetCidToGidTable(encodedStream.GetBuffer());
          }
          fontDictionary = (pdfDictionary["FontDescriptor"] as PdfReferenceHolder).Object as PdfDictionary;
        }
        else if (fontDictionary.ContainsKey("FontDescriptor"))
          fontDictionary = (fontDictionary["FontDescriptor"] as PdfReferenceHolder).Object as PdfDictionary;
        if (fontDictionary.ContainsKey("FontFile"))
        {
          this.IsType1Font = true;
          this.isEmbedded = true;
          long objNum = (fontDictionary["FontFile"] as PdfReferenceHolder).Reference.ObjNum;
          if (!this.type1FontReference.ContainsKey(objNum))
          {
            PdfDictionary streamDictionary = (fontDictionary["FontFile"] as PdfReferenceHolder).Object as PdfDictionary;
            MemoryStream encodedStream = (streamDictionary as PdfStream).InternalStream;
            string[] fontFilter = this.GetFontFilter(streamDictionary);
            if (fontFilter != null)
            {
              for (int index = 0; index < fontFilter.Length; ++index)
              {
                switch (fontFilter[index])
                {
                  case "A85":
                  case "ASCII85Decode":
                    encodedStream = this.DecodeASCII85Stream(encodedStream);
                    break;
                  case "FlateDecode":
                    encodedStream = this.DecodeFlateStream(encodedStream);
                    break;
                }
              }
            }
            encodedStream.Capacity = (int) encodedStream.Length;
            this.m_cffGlyphs = new FontFile().ParseType1FontFile(encodedStream.ToArray());
            this.type1FontReference.Add(objNum, this.m_cffGlyphs);
          }
          else
            this.m_cffGlyphs = this.type1FontReference[objNum];
        }
        else
        {
          if (fontDictionary.ContainsKey("FontFile2"))
          {
            this.IsContainFontfile2 = true;
            this.isEmbedded = true;
            PdfDictionary streamDictionary = (fontDictionary["FontFile2"] as PdfReferenceHolder).Object as PdfDictionary;
            MemoryStream encodedStream = (streamDictionary as PdfStream).InternalStream;
            string[] fontFilter = this.GetFontFilter(streamDictionary);
            if (fontFilter != null)
            {
              for (int index1 = 0; index1 < fontFilter.Length; ++index1)
              {
                switch (fontFilter[index1])
                {
                  case "A85":
                  case "ASCII85Decode":
                    encodedStream = this.DecodeASCII85Stream(encodedStream);
                    break;
                  case "FlateDecode":
                    encodedStream = this.DecodeFlateStream(encodedStream);
                    break;
                  case "RunLengthDecode":
                    encodedStream.Position = 0L;
                    byte[] array = encodedStream.ToArray();
                    int index2 = 0;
                    byte[] numArray1 = new byte[0];
                    byte[] numArray2 = new byte[0];
                    while (index2 < array.Length - 1)
                    {
                      byte[] sourceArray1 = numArray1;
                      int num1 = (int) array[index2];
                      if (num1 >= 0 && num1 <= (int) sbyte.MaxValue)
                      {
                        int sourceIndex = index2 + 1;
                        byte[] numArray3 = new byte[num1 + 1];
                        Array.Copy((Array) array, sourceIndex, (Array) numArray3, 0, numArray3.Length);
                        numArray1 = new byte[sourceArray1.Length + numArray3.Length];
                        Array.Copy((Array) sourceArray1, 0, (Array) numArray1, 0, sourceArray1.Length);
                        Array.Copy((Array) numArray3, 0, (Array) numArray1, sourceArray1.Length, numArray3.Length);
                        index2 = sourceIndex + (num1 + 1);
                      }
                      else if (num1 >= 129 && num1 <= (int) byte.MaxValue)
                      {
                        int index3 = index2 + 1;
                        byte num2 = array[index3];
                        int length = 257 - num1;
                        byte[] sourceArray2 = new byte[length];
                        for (int index4 = 0; index4 < length; ++index4)
                          sourceArray2[index4] = num2;
                        numArray1 = new byte[sourceArray1.Length + sourceArray2.Length];
                        Array.Copy((Array) sourceArray1, 0, (Array) numArray1, 0, sourceArray1.Length);
                        Array.Copy((Array) sourceArray2, 0, (Array) numArray1, sourceArray1.Length, sourceArray2.Length);
                        index2 = index3 + 1;
                      }
                      else if (num1 == 128 /*0x80*/)
                        break;
                    }
                    encodedStream = new MemoryStream(numArray1);
                    encodedStream.Position = 0L;
                    break;
                }
              }
            }
            encodedStream.Capacity = (int) encodedStream.Length;
            this.m_fontfile2 = new FontFile2(encodedStream.ToArray());
            List<TableEntry> tableEntryList = new List<TableEntry>();
            FontDecode fontDecode = new FontDecode();
            this.GetGlyphWidths();
            return font;
          }
          if (fontDictionary.ContainsKey("FontFile3"))
          {
            this.IsType1Font = true;
            this.Is1C = true;
            this.isEmbedded = true;
            long objNum = (fontDictionary["FontFile3"] as PdfReferenceHolder).Reference.ObjNum;
            if (!this.type1FontReference.ContainsKey(objNum))
            {
              PdfDictionary streamDictionary = (fontDictionary["FontFile3"] as PdfReferenceHolder).Object as PdfDictionary;
              MemoryStream encodedStream = (streamDictionary as PdfStream).InternalStream;
              string[] fontFilter = this.GetFontFilter(streamDictionary);
              if (fontFilter != null)
              {
                for (int index = 0; index < fontFilter.Length; ++index)
                {
                  switch (fontFilter[index])
                  {
                    case "A85":
                    case "ASCII85Decode":
                      encodedStream = this.DecodeASCII85Stream(encodedStream);
                      break;
                    case "FlateDecode":
                      encodedStream = this.DecodeFlateStream(encodedStream);
                      break;
                  }
                }
              }
              encodedStream.Capacity = (int) encodedStream.Length;
              byte[] array = encodedStream.ToArray();
              FontFile3 fontFile3 = new FontFile3();
              this.m_cffGlyphs = fontFile3.readType1CFontFile(array);
              this.type1FontReference.Add(objNum, this.m_cffGlyphs);
              this.IsCID = fontFile3.isCID;
            }
            else
              this.m_cffGlyphs = this.type1FontReference[objNum];
          }
        }
      }
      catch (Exception ex)
      {
        return (Font) null;
      }
    }
    else if (this.FontEncoding == "WinAnsiEncoding" || this.FontEncoding == "" || this.FontEncoding == "BuiltIn" || this.FontEncoding == "MacRomanEncoding")
    {
      try
      {
        PdfDictionary fontDictionary = this.m_fontDictionary;
        if (fontDictionary.ContainsKey("DescendantFonts"))
        {
          PdfArray pdfArray = (PdfArray) null;
          if (fontDictionary["DescendantFonts"] is PdfArray)
            pdfArray = fontDictionary["DescendantFonts"] as PdfArray;
          if ((object) (fontDictionary["DescendantFonts"] as PdfReferenceHolder) != null)
            pdfArray = (fontDictionary["DescendantFonts"] as PdfReferenceHolder).Object as PdfArray;
          fontDictionary = (((pdfArray[0] as PdfReferenceHolder).Object as PdfDictionary)["FontDescriptor"] as PdfReferenceHolder).Object as PdfDictionary;
        }
        else if (fontDictionary.ContainsKey("FontDescriptor"))
          fontDictionary = (fontDictionary["FontDescriptor"] as PdfReferenceHolder).Object as PdfDictionary;
        if (fontDictionary.ContainsKey("FontFile"))
        {
          this.isEmbedded = true;
          this.IsType1Font = true;
          long objNum = (fontDictionary["FontFile"] as PdfReferenceHolder).Reference.ObjNum;
          if (!this.type1FontReference.ContainsKey(objNum))
          {
            PdfDictionary streamDictionary = (fontDictionary["FontFile"] as PdfReferenceHolder).Object as PdfDictionary;
            MemoryStream encodedStream = (streamDictionary as PdfStream).InternalStream;
            string[] fontFilter = this.GetFontFilter(streamDictionary);
            if (fontFilter != null)
            {
              for (int index = 0; index < fontFilter.Length; ++index)
              {
                switch (fontFilter[index])
                {
                  case "A85":
                  case "ASCII85Decode":
                    encodedStream = this.DecodeASCII85Stream(encodedStream);
                    break;
                  case "FlateDecode":
                    encodedStream = this.DecodeFlateStream(encodedStream);
                    break;
                }
              }
            }
            encodedStream.Capacity = (int) encodedStream.Length;
            this.m_cffGlyphs = new FontFile().ParseType1FontFile(encodedStream.ToArray());
            this.type1FontReference.Add(objNum, this.m_cffGlyphs);
          }
          else
            this.m_cffGlyphs = this.type1FontReference[objNum];
        }
        else
        {
          if (fontDictionary.ContainsKey("FontFile2"))
          {
            this.IsContainFontfile2 = true;
            this.isEmbedded = true;
            PdfDictionary streamDictionary = (fontDictionary["FontFile2"] as PdfReferenceHolder).Object as PdfDictionary;
            MemoryStream encodedStream = (streamDictionary as PdfStream).InternalStream;
            string[] fontFilter = this.GetFontFilter(streamDictionary);
            if (fontFilter != null)
            {
              for (int index5 = 0; index5 < fontFilter.Length; ++index5)
              {
                switch (fontFilter[index5])
                {
                  case "A85":
                  case "ASCII85Decode":
                    encodedStream = this.DecodeASCII85Stream(encodedStream);
                    break;
                  case "FlateDecode":
                    encodedStream = this.DecodeFlateStream(encodedStream);
                    break;
                  case "RunLengthDecode":
                    encodedStream.Position = 0L;
                    byte[] array = encodedStream.ToArray();
                    int index6 = 0;
                    byte[] numArray4 = new byte[0];
                    byte[] numArray5 = new byte[0];
                    while (index6 < array.Length - 1)
                    {
                      byte[] sourceArray3 = numArray4;
                      int num3 = (int) array[index6];
                      if (num3 >= 0 && num3 <= (int) sbyte.MaxValue)
                      {
                        int sourceIndex = index6 + 1;
                        byte[] numArray6 = new byte[num3 + 1];
                        Array.Copy((Array) array, sourceIndex, (Array) numArray6, 0, numArray6.Length);
                        numArray4 = new byte[sourceArray3.Length + numArray6.Length];
                        Array.Copy((Array) sourceArray3, 0, (Array) numArray4, 0, sourceArray3.Length);
                        Array.Copy((Array) numArray6, 0, (Array) numArray4, sourceArray3.Length, numArray6.Length);
                        index6 = sourceIndex + (num3 + 1);
                      }
                      else if (num3 >= 129 && num3 <= (int) byte.MaxValue)
                      {
                        int index7 = index6 + 1;
                        byte num4 = array[index7];
                        int length = 257 - num3;
                        byte[] sourceArray4 = new byte[length];
                        for (int index8 = 0; index8 < length; ++index8)
                          sourceArray4[index8] = num4;
                        numArray4 = new byte[sourceArray3.Length + sourceArray4.Length];
                        Array.Copy((Array) sourceArray3, 0, (Array) numArray4, 0, sourceArray3.Length);
                        Array.Copy((Array) sourceArray4, 0, (Array) numArray4, sourceArray3.Length, sourceArray4.Length);
                        index6 = index7 + 1;
                      }
                      else if (num3 == 128 /*0x80*/)
                        break;
                    }
                    encodedStream = new MemoryStream(numArray4);
                    encodedStream.Position = 0L;
                    break;
                }
              }
            }
            encodedStream.Capacity = (int) encodedStream.Length;
            this.m_fontfile2 = new FontFile2(encodedStream.ToArray());
            List<TableEntry> tableEntryList = new List<TableEntry>();
            FontDecode fontDecode = new FontDecode();
            this.GetGlyphWidths();
            return font;
          }
          if (fontDictionary.ContainsKey("FontFile3"))
          {
            this.IsType1Font = true;
            this.isEmbedded = true;
            this.Is1C = true;
            long objNum = (fontDictionary["FontFile3"] as PdfReferenceHolder).Reference.ObjNum;
            if (!this.type1FontReference.ContainsKey(objNum))
            {
              PdfDictionary streamDictionary = (fontDictionary["FontFile3"] as PdfReferenceHolder).Object as PdfDictionary;
              MemoryStream encodedStream = (streamDictionary as PdfStream).InternalStream;
              string[] fontFilter = this.GetFontFilter(streamDictionary);
              if (fontFilter != null)
              {
                for (int index = 0; index < fontFilter.Length; ++index)
                {
                  switch (fontFilter[index])
                  {
                    case "A85":
                    case "ASCII85Decode":
                      encodedStream = this.DecodeASCII85Stream(encodedStream);
                      break;
                    case "FlateDecode":
                      encodedStream = this.DecodeFlateStream(encodedStream);
                      break;
                  }
                }
              }
              encodedStream.Capacity = (int) encodedStream.Length;
              byte[] array = encodedStream.ToArray();
              FontFile3 fontFile3 = new FontFile3();
              this.m_cffGlyphs = fontFile3.readType1CFontFile(array);
              this.type1FontReference.Add(objNum, this.m_cffGlyphs);
              this.IsCID = fontFile3.isCID;
            }
            else
              this.m_cffGlyphs = this.type1FontReference[objNum];
          }
          else
          {
            if (fontDictionary.ContainsKey("FontFamily"))
              return new Font((fontDictionary["FontFamily"] as PdfString).Value, this.FontSize, this.FontStyle);
            this.IsSystemFontExist = true;
          }
        }
      }
      catch (Exception ex)
      {
        return (Font) null;
      }
    }
    else
    {
      if (!(this.FontEncoding == "Encoding"))
        return font;
      try
      {
        PdfDictionary fontDictionary = this.m_fontDictionary;
        if (fontDictionary.ContainsKey("Encoding") && (object) (fontDictionary["Encoding"] as PdfReferenceHolder) != null)
        {
          PdfDictionary pdfDictionary = (fontDictionary["Encoding"] as PdfReferenceHolder).Object as PdfDictionary;
          if (pdfDictionary.ContainsKey("Differences"))
          {
            PdfArray pdfArray = pdfDictionary["Differences"] as PdfArray;
            int key = 0;
            for (int index = 0; index < pdfArray.Count; ++index)
            {
              IPdfPrimitive pdfPrimitive = pdfArray[index];
              if (pdfPrimitive is PdfNumber)
              {
                key = (pdfPrimitive as PdfNumber).IntValue;
              }
              else
              {
                string str = (pdfPrimitive as PdfName).Value;
                if (!this.differenceTable.ContainsKey(key))
                  this.differenceTable.Add(key, str);
                ++key;
              }
            }
          }
        }
        if (fontDictionary.ContainsKey("DescendantFonts"))
        {
          PdfArray pdfArray = (PdfArray) null;
          if (fontDictionary["DescendantFonts"] is PdfArray)
            pdfArray = fontDictionary["DescendantFonts"] as PdfArray;
          if ((object) (fontDictionary["DescendantFonts"] as PdfReferenceHolder) != null)
            pdfArray = (fontDictionary["DescendantFonts"] as PdfReferenceHolder).Object as PdfArray;
          fontDictionary = (((pdfArray[0] as PdfReferenceHolder).Object as PdfDictionary)["FontDescriptor"] as PdfReferenceHolder).Object as PdfDictionary;
        }
        else if (fontDictionary.ContainsKey("FontDescriptor"))
          fontDictionary = (fontDictionary["FontDescriptor"] as PdfReferenceHolder).Object as PdfDictionary;
        if (fontDictionary.ContainsKey("FontFile"))
        {
          this.IsType1Font = true;
          this.isEmbedded = true;
          long objNum = (fontDictionary["FontFile"] as PdfReferenceHolder).Reference.ObjNum;
          if (!this.type1FontReference.ContainsKey(objNum))
          {
            PdfDictionary streamDictionary = (fontDictionary["FontFile"] as PdfReferenceHolder).Object as PdfDictionary;
            MemoryStream encodedStream = (streamDictionary as PdfStream).InternalStream;
            string[] fontFilter = this.GetFontFilter(streamDictionary);
            if (fontFilter != null)
            {
              for (int index = 0; index < fontFilter.Length; ++index)
              {
                switch (fontFilter[index])
                {
                  case "A85":
                  case "ASCII85Decode":
                    encodedStream = this.DecodeASCII85Stream(encodedStream);
                    break;
                  case "FlateDecode":
                    encodedStream = this.DecodeFlateStream(encodedStream);
                    break;
                }
              }
            }
            encodedStream.Capacity = (int) encodedStream.Length;
            this.m_cffGlyphs = new FontFile().ParseType1FontFile(encodedStream.ToArray());
            this.type1FontReference.Add(objNum, this.m_cffGlyphs);
          }
          else
            this.m_cffGlyphs = this.type1FontReference[objNum];
        }
        else
        {
          if (fontDictionary.ContainsKey("FontFile2"))
          {
            this.IsContainFontfile2 = true;
            this.isEmbedded = true;
            PdfDictionary streamDictionary = (fontDictionary["FontFile2"] as PdfReferenceHolder).Object as PdfDictionary;
            MemoryStream encodedStream = (streamDictionary as PdfStream).InternalStream;
            string[] fontFilter = this.GetFontFilter(streamDictionary);
            if (fontFilter != null)
            {
              for (int index = 0; index < fontFilter.Length; ++index)
              {
                switch (fontFilter[index])
                {
                  case "A85":
                  case "ASCII85Decode":
                    encodedStream = this.DecodeASCII85Stream(encodedStream);
                    break;
                  case "FlateDecode":
                    encodedStream = this.DecodeFlateStream(encodedStream);
                    break;
                }
              }
            }
            encodedStream.Capacity = (int) encodedStream.Length;
            this.m_fontfile2 = new FontFile2(encodedStream.GetBuffer());
            List<TableEntry> tableEntryList = new List<TableEntry>();
            FontDecode fontDecode = new FontDecode();
            this.GetGlyphWidths();
            return font;
          }
          if (fontDictionary.ContainsKey("FontFile3"))
          {
            this.IsType1Font = true;
            this.Is1C = true;
            this.isEmbedded = true;
            long objNum = (fontDictionary["FontFile3"] as PdfReferenceHolder).Reference.ObjNum;
            if (!this.type1FontReference.ContainsKey(objNum))
            {
              PdfDictionary streamDictionary = (fontDictionary["FontFile3"] as PdfReferenceHolder).Object as PdfDictionary;
              MemoryStream encodedStream = (streamDictionary as PdfStream).InternalStream;
              string[] fontFilter = this.GetFontFilter(streamDictionary);
              if (fontFilter != null)
              {
                for (int index = 0; index < fontFilter.Length; ++index)
                {
                  switch (fontFilter[index])
                  {
                    case "A85":
                    case "ASCII85Decode":
                      encodedStream = this.DecodeASCII85Stream(encodedStream);
                      break;
                    case "FlateDecode":
                      encodedStream = this.DecodeFlateStream(encodedStream);
                      break;
                  }
                }
              }
              encodedStream.Capacity = (int) encodedStream.Length;
              byte[] array = encodedStream.ToArray();
              FontFile3 fontFile3 = new FontFile3();
              this.m_cffGlyphs = fontFile3.readType1CFontFile(array);
              this.type1FontReference.Add(objNum, this.m_cffGlyphs);
              this.IsCID = fontFile3.isCID;
            }
            else
              this.m_cffGlyphs = this.type1FontReference[objNum];
          }
        }
      }
      catch (Exception ex)
      {
        return (Font) null;
      }
    }
    return (Font) null;
  }

  private string GetFontEncoding()
  {
    PdfName pdfName = new PdfName();
    string empty = string.Empty;
    if (!this.m_fontDictionary.ContainsKey("Encoding"))
      return empty;
    PdfName font = this.m_fontDictionary["Encoding"] as PdfName;
    if (!(font == (PdfName) null))
      return font.Value;
    Type type = this.m_fontDictionary["Encoding"].GetType();
    PdfDictionary pdfDictionary = new PdfDictionary();
    if (type.Name == "PdfDictionary")
      pdfDictionary = this.m_fontDictionary["Encoding"] as PdfDictionary;
    else if (type.Name == "PdfReferenceHolder")
      pdfDictionary = (this.m_fontDictionary["Encoding"] as PdfReferenceHolder).Object as PdfDictionary;
    if (pdfDictionary != null && pdfDictionary.ContainsKey("Type"))
      empty = (pdfDictionary["Type"] as PdfName).Value;
    return empty;
  }

  private string[] GetFontFilter(PdfDictionary streamDictionary)
  {
    string[] fontFilter1 = (string[]) null;
    if (streamDictionary != null && streamDictionary.ContainsKey("Filter"))
    {
      if ((object) (streamDictionary["Filter"] as PdfName) != null)
        return new string[1]
        {
          (streamDictionary["Filter"] as PdfName).Value
        };
      if (streamDictionary["Filter"] is PdfArray)
      {
        PdfArray stream = streamDictionary["Filter"] as PdfArray;
        string[] fontFilter2 = new string[stream.Count];
        for (int index = 0; index < stream.Count; ++index)
          fontFilter2[index] = (stream[index] as PdfName).Value;
        return fontFilter2;
      }
      if ((object) (streamDictionary["Filter"] as PdfReferenceHolder) == null)
        return fontFilter1;
      PdfArray pdfArray = (streamDictionary["Filter"] as PdfReferenceHolder).Object as PdfArray;
      fontFilter1 = new string[pdfArray.Count];
      for (int index = 0; index < pdfArray.Count; ++index)
        fontFilter1[index] = (pdfArray[index] as PdfName).Value;
    }
    return fontFilter1;
  }

  private string GetFontName()
  {
    string fontName = string.Empty;
    this.IsSystemFontExist = false;
    List<string> stringList = new List<string>();
    if (this.m_fontDictionary.ContainsKey("BaseFont"))
    {
      PdfName font = this.m_fontDictionary["BaseFont"] as PdfName;
      if (font == (PdfName) null)
        font = (this.m_fontDictionary["BaseFont"] as PdfReferenceHolder).Object as PdfName;
      string str = font.Value;
      if (!this.IsTextExtraction)
      {
        if (str.Contains("#20") && !str.Contains("+"))
        {
          int length = str.LastIndexOf("#20");
          str = str.Substring(0, length) + "+";
        }
        str.Contains("+");
      }
      if (this.IsSystemFontExist)
        return fontName;
      if (font.Value.Contains("+"))
        fontName = font.Value.Split('+')[1];
      else
        fontName = font.Value;
      if (fontName.Contains("-"))
        fontName = fontName.Split('-')[0];
      else if (fontName.Contains(","))
        fontName = fontName.Split(',')[0];
      if (fontName.Contains("MT"))
        fontName = fontName.Replace("MT", "");
    }
    return fontName;
  }

  private FontStyle GetFontStyle()
  {
    FontStyle fontStyle = FontStyle.Regular;
    if (!this.m_fontDictionary.ContainsKey("BaseFont"))
      return fontStyle;
    PdfName font = this.m_fontDictionary["BaseFont"] as PdfName;
    if (font == (PdfName) null)
      font = (this.m_fontDictionary["BaseFont"] as PdfReferenceHolder).Object as PdfName;
    if (font.Value.Contains("-") || font.Value.Contains(","))
    {
      string empty = string.Empty;
      if (font.Value.Contains("-"))
        empty = font.Value.Split('-')[1];
      else if (font.Value.Contains(","))
        empty = font.Value.Split(',')[1];
      string str = empty.Replace("MT", "");
      switch (str)
      {
        case null:
          return fontStyle;
        case "Italic":
        case "Oblique":
          return FontStyle.Italic;
        default:
          if (!(str != "Bold") || !(str != "BoldMT"))
            return FontStyle.Bold;
          return str != "BoldItalic" && str != "BoldOblique" ? fontStyle : FontStyle.Bold | FontStyle.Italic;
      }
    }
    else
    {
      if (font.Value.Contains("Bold"))
        fontStyle = FontStyle.Bold;
      if (font.Value.Contains("BoldItalic") || font.Value.Contains("BoldOblique"))
        fontStyle = FontStyle.Bold | FontStyle.Italic;
      return !font.Value.Contains("Italic") && !font.Value.Contains("Oblique") ? fontStyle : FontStyle.Italic;
    }
  }

  public GraphicsPath GetGlyf(char val)
  {
    if (this.GlyfFontFile2 != null && this.IsNonSymbol || this.FontEncoding != null && (this.FontEncoding == "MacRomanEncoding" || this.FontEncoding == "WinAnsiEncoding"))
    {
      CmapTables cmaptable1 = this.GlyfFontFile2.Cmap.GetCmaptable((ushort) 3, (ushort) 1);
      if (cmaptable1 != null)
      {
        this.Graphic = this.GlyfFontFile2.GetGlyfPathMicrosoftwithencoding(cmaptable1, val);
        return this.Graphic;
      }
      CmapTables cmaptable2 = this.GlyfFontFile2.Cmap.GetCmaptable((ushort) 1, (ushort) 0);
      if (cmaptable2 != null)
        return this.Graphic = this.GlyfFontFile2.GetGlyphsPathMacWithEncoding(cmaptable2, val);
    }
    else if (this.GlyfFontFile2 != null && this.Issymbol || this.FontEncoding == null)
    {
      CmapTables cmaptable3 = this.GlyfFontFile2.Cmap.GetCmaptable((ushort) 3, (ushort) 0);
      if (cmaptable3 != null)
      {
        this.Graphic = this.GlyfFontFile2.GetGlyfPathWindowsWithoutEncoding(cmaptable3, val);
        return this.Graphic;
      }
      CmapTables cmaptable4 = this.GlyfFontFile2.Cmap.GetCmaptable((ushort) 1, (ushort) 0);
      if (cmaptable4 != null)
      {
        this.Graphic = this.GlyfFontFile2.GetGlyphsPathMacWithoutencoding(cmaptable4, val);
        return this.Graphic;
      }
    }
    return (GraphicsPath) null;
  }

  private void GetGlyphWidths()
  {
    if (!(this.FontEncoding == "Identity-H"))
      return;
    fontDictionary = this.m_fontDictionary;
    if (fontDictionary.ContainsKey("DescendantFonts"))
    {
      PdfArray pdfArray = (PdfArray) null;
      if (fontDictionary["DescendantFonts"] is PdfArray)
        pdfArray = fontDictionary["DescendantFonts"] as PdfArray;
      if ((object) (fontDictionary["DescendantFonts"] as PdfReferenceHolder) != null)
        pdfArray = (fontDictionary["DescendantFonts"] as PdfReferenceHolder).Object as PdfArray;
      if (!(pdfArray[0] is PdfDictionary fontDictionary))
        fontDictionary = (pdfArray[0] as PdfReferenceHolder).Object as PdfDictionary;
    }
    this.m_fontGlyphWidth = new Dictionary<int, int>();
    PdfArray pdfArray1 = (PdfArray) null;
    int key1 = 0;
    if (fontDictionary["W"] is PdfArray)
      pdfArray1 = fontDictionary["W"] as PdfArray;
    if ((object) (fontDictionary["W"] as PdfReferenceHolder) != null)
      pdfArray1 = (fontDictionary["W"] as PdfReferenceHolder).Object as PdfArray;
    if (fontDictionary.ContainsKey("DW"))
      this.DefaultGlyphWidth = (fontDictionary["DW"] as PdfNumber).FloatValue;
    try
    {
      int index1;
      if (pdfArray1 == null)
      {
        this.m_fontGlyphWidth = (Dictionary<int, int>) null;
      }
      else
      {
        for (int index2 = 0; index2 < pdfArray1.Count; index2 = index1 + 1)
        {
          if (pdfArray1[index2] is PdfNumber)
            key1 = (pdfArray1[index2] as PdfNumber).IntValue;
          index1 = index2 + 1;
          if (pdfArray1[index1] is PdfArray)
          {
            PdfArray pdfArray2 = pdfArray1[index1] as PdfArray;
            for (int index3 = 0; index3 < pdfArray2.Count; ++index3)
            {
              if (this.m_cidToGidTable != null)
              {
                this.m_fontGlyphWidth = (Dictionary<int, int>) null;
                return;
              }
              if (!this.m_containsCmap)
                this.m_fontGlyphWidth.Add(key1, (pdfArray2[index3] as PdfNumber).IntValue);
              else if (this.CharacterMapTable.ContainsKey((double) key1))
              {
                int key2 = (int) this.CharacterMapTable[(double) key1].ToCharArray()[0];
                if (!this.m_fontGlyphWidth.ContainsKey(key2))
                  this.m_fontGlyphWidth.Add(key2, (pdfArray2[index3] as PdfNumber).IntValue);
              }
              else if (!this.m_fontGlyphWidth.ContainsKey(key1))
                this.m_fontGlyphWidth.Add(key1, (pdfArray2[index3] as PdfNumber).IntValue);
              ++key1;
            }
          }
          else if (pdfArray1[index1] is PdfNumber)
          {
            int intValue = (pdfArray1[index1] as PdfNumber).IntValue;
            ++index1;
            for (; key1 <= intValue; ++key1)
            {
              if (!this.m_fontGlyphWidth.ContainsKey(key1))
                this.m_fontGlyphWidth.Add(key1, (pdfArray1[index1] as PdfNumber).IntValue);
            }
          }
        }
      }
    }
    catch
    {
      this.m_fontGlyphWidth = (Dictionary<int, int>) null;
    }
  }

  private void GetGlyphWidthsNonIdH()
  {
    if (!(this.fontType.Value != "Type3"))
      return;
    int num = 0;
    PdfDictionary fontDictionary = this.m_fontDictionary;
    if (fontDictionary.ContainsKey("DW"))
      this.DefaultGlyphWidth = (fontDictionary["DW"] as PdfNumber).FloatValue;
    if (fontDictionary.ContainsKey("FirstChar"))
      num = (fontDictionary["FirstChar"] as PdfNumber).IntValue;
    if (fontDictionary.ContainsKey("LastChar"))
    {
      int intValue = (fontDictionary["LastChar"] as PdfNumber).IntValue;
    }
    this.m_fontGlyphWidth = new Dictionary<int, int>();
    PdfArray pdfArray1 = (PdfArray) null;
    if (fontDictionary["Widths"] is PdfArray)
      pdfArray1 = fontDictionary["Widths"] as PdfArray;
    if ((object) (fontDictionary["Widths"] as PdfReferenceHolder) != null)
      pdfArray1 = (fontDictionary["Widths"] as PdfReferenceHolder).Object as PdfArray;
    if (fontDictionary.Items.ContainsKey(new PdfName("DescendantFonts")))
    {
      PdfArray pdfArray2 = fontDictionary["DescendantFonts"] as PdfArray;
      if ((object) (pdfArray2[0] as PdfReferenceHolder) != null)
      {
        PdfDictionary pdfDictionary = (pdfArray2[0] as PdfReferenceHolder).Object as PdfDictionary;
        if (pdfDictionary.ContainsKey("W"))
          pdfArray1 = pdfDictionary["W"] as PdfArray;
      }
    }
    if (pdfArray1 == null)
      return;
    try
    {
      for (int index1 = 0; index1 < pdfArray1.Count; ++index1)
      {
        int key1 = num + index1;
        if (this.CharacterMapTable.Count > 0 || this.DifferencesDictionary.Count > 0)
        {
          if (this.CharacterMapTable.ContainsKey((double) key1))
          {
            string str = this.CharacterMapTable[(double) key1];
            int key2 = key1;
            if (!this.m_fontGlyphWidth.ContainsKey(key2))
              this.m_fontGlyphWidth.Add(key2, (pdfArray1[index1] as PdfNumber).IntValue);
          }
          else if (this.DifferencesDictionary.ContainsKey(key1.ToString()))
          {
            string differences = this.DifferencesDictionary[key1.ToString()];
            int key3 = key1;
            if (!this.m_fontGlyphWidth.ContainsKey(key3))
              this.m_fontGlyphWidth.Add(key3, (pdfArray1[index1] as PdfNumber).IntValue);
          }
          else if (!this.m_fontGlyphWidth.ContainsKey(key1))
            this.m_fontGlyphWidth.Add(key1, (pdfArray1[index1] as PdfNumber).IntValue);
        }
        else if (pdfArray1[index1] is PdfArray)
        {
          PdfArray pdfArray3 = pdfArray1[index1] as PdfArray;
          for (int index2 = index1; index2 < pdfArray3.Count; ++index2)
          {
            int key4 = num + index2;
            if (this.CharacterMapTable.Count > 0 || this.DifferencesDictionary.Count > 0)
            {
              if (this.CharacterMapTable.ContainsKey((double) key4))
              {
                int key5 = (int) this.CharacterMapTable[(double) key4].ToCharArray()[0];
                if (!this.m_fontGlyphWidth.ContainsKey(key5))
                  this.m_fontGlyphWidth.Add(key5, (pdfArray3[index2] as PdfNumber).IntValue);
              }
              else if (this.DifferencesDictionary.ContainsKey(key4.ToString()))
              {
                string differences = this.DifferencesDictionary[key4.ToString()];
                int key6 = key4;
                if (!this.m_fontGlyphWidth.ContainsKey(key6))
                  this.m_fontGlyphWidth.Add(key6, (pdfArray3[index2] as PdfNumber).IntValue);
              }
              else if (!this.m_fontGlyphWidth.ContainsKey(key4))
                this.m_fontGlyphWidth.Add(key4, (pdfArray3[index2] as PdfNumber).IntValue);
            }
            else
              this.m_fontGlyphWidth.Add(key4, (pdfArray3[index2] as PdfNumber).IntValue);
          }
        }
        else
          this.m_fontGlyphWidth.Add(key1, (pdfArray1[index1] as PdfNumber).IntValue);
      }
    }
    catch
    {
      this.m_fontGlyphWidth = (Dictionary<int, int>) null;
    }
  }

  private void GetGlyphWidthsType1()
  {
    int num = 0;
    PdfDictionary fontDictionary = this.m_fontDictionary;
    if (fontDictionary.ContainsKey("DW"))
      this.DefaultGlyphWidth = (fontDictionary["DW"] as PdfNumber).FloatValue;
    if (fontDictionary.ContainsKey("FirstChar"))
      num = (fontDictionary["FirstChar"] as PdfNumber).IntValue;
    if (fontDictionary.ContainsKey("LastChar"))
    {
      int intValue = (fontDictionary["LastChar"] as PdfNumber).IntValue;
    }
    this.m_fontGlyphWidth = new Dictionary<int, int>();
    this.m_fontGlyphWidthMapping = new Dictionary<int, int>();
    PdfArray pdfArray = (PdfArray) null;
    if (fontDictionary["Widths"] is PdfArray)
      pdfArray = fontDictionary["Widths"] as PdfArray;
    if ((object) (fontDictionary["Widths"] as PdfReferenceHolder) != null)
      pdfArray = (fontDictionary["Widths"] as PdfReferenceHolder).Object as PdfArray;
    if (pdfArray == null)
      return;
    try
    {
      for (int index = 0; index < pdfArray.Count; ++index)
      {
        int key1 = num + index;
        if (this.CharacterMapTable.ContainsKey((double) key1))
        {
          int key2 = (int) this.CharacterMapTable[(double) key1].ToCharArray()[0];
          if (!this.m_fontGlyphWidthMapping.ContainsKey(key2))
            this.m_fontGlyphWidthMapping.Add(key2, (pdfArray[index] as PdfNumber).IntValue);
        }
        else if (this.DifferencesDictionary.ContainsKey(key1.ToString()))
        {
          string differences = this.DifferencesDictionary[key1.ToString()];
          int key3 = key1;
          if (differences.Length == 1)
            key3 = (int) differences.ToCharArray()[0];
          if (!this.m_fontGlyphWidthMapping.ContainsKey(key3))
            this.m_fontGlyphWidthMapping.Add(key3, (pdfArray[index] as PdfNumber).IntValue);
        }
        else if (!this.m_fontGlyphWidthMapping.ContainsKey(key1))
          this.m_fontGlyphWidthMapping.Add(key1, (pdfArray[index] as PdfNumber).IntValue);
        this.m_fontGlyphWidth.Add(key1, (pdfArray[index] as PdfNumber).IntValue);
      }
    }
    catch
    {
      this.m_fontGlyphWidth = (Dictionary<int, int>) null;
    }
  }

  private string GetHexaDecimalString(string hexEncodedText)
  {
    string hexaDecimalString = string.Empty;
    if (!string.IsNullOrEmpty(hexEncodedText))
    {
      PdfName pdfName = this.m_fontDictionary.Items[new PdfName("Subtype")] as PdfName;
      int num = 2;
      if (pdfName.Value != "Type1" && pdfName.Value != "TrueType" && pdfName.Value != "Type3")
        num = 4;
      hexEncodedText = this.EscapeSymbols(hexEncodedText);
      string s = hexEncodedText;
      string str1 = hexaDecimalString;
      string str2 = (string) null;
      while (hexEncodedText.Length > 0)
      {
        if (hexEncodedText.Length % 4 != 0)
          num = 2;
        string str3 = hexEncodedText.Substring(0, num);
        if (this.m_fontDictionary.ContainsKey("DescendantFonts") && !this.m_fontDictionary.ContainsKey("ToUnicode") && this.m_fontDictionary["DescendantFonts"] is PdfArray font && (object) (font[0] as PdfReferenceHolder) != null)
        {
          PdfDictionary pdfDictionary1 = (font[0] as PdfReferenceHolder).Object as PdfDictionary;
          if ((pdfDictionary1["FontDescriptor"] as PdfReferenceHolder).Object is PdfDictionary pdfDictionary2 && pdfDictionary1.ContainsKey("Subtype") && !pdfDictionary2.ContainsKey("FontFile2") && (pdfDictionary1["Subtype"] as PdfName).Value == "CIDFontType2")
            str3 = this.MapHebrewCharacters(str3);
        }
        hexaDecimalString += ((char) long.Parse(str3, NumberStyles.HexNumber)).ToString();
        hexEncodedText = hexEncodedText.Substring(num, hexEncodedText.Length - num);
        str2 = hexaDecimalString.ToString();
      }
      if (!str2.Contains("\u0093") && !str2.Contains("\u0094") && !str2.Contains("\u0092") || s.Length >= num)
        return hexaDecimalString;
      string str4 = str1;
      byte[] bytes = BitConverter.GetBytes(int.Parse(s, NumberStyles.HexNumber));
      hexEncodedText = Encoding.GetEncoding(1251).GetString(bytes);
      hexEncodedText = hexEncodedText.Remove(1);
      hexaDecimalString = str4 + hexEncodedText;
    }
    return hexaDecimalString;
  }

  internal static List<string> GetHexCode(string hexCode)
  {
    List<string> hexCode1 = new List<string>();
    string str1 = hexCode;
    int num1 = 0;
    int num2 = 0;
    while (num1 >= 0)
    {
      num1 = str1.IndexOf('<');
      int num3 = str1.IndexOf('>');
      if (num1 >= 0 && num3 >= 0)
      {
        string str2 = str1.Substring(num1 + 1, num3 - 1 - num1);
        hexCode1.Add(str2);
        str1 = str1.Substring(num3 + 1, str1.Length - 1 - num3);
      }
      ++num2;
    }
    return hexCode1;
  }

  internal static string GetLatinCharacter(string decodedCharacter)
  {
    switch (decodedCharacter)
    {
      case "Adieresis":
        return "Ä";
      case "Oacute":
        return "Ó";
      case "Scaron":
        return "Š";
      case "Udieresis":
        return "Ü";
      case "acircumflex":
        return "â";
      case "adieresis":
        return "ä";
      case "agrave":
        return "à";
      case "ampersand":
        return "&";
      case "aring":
        return "å";
      case "asciicircum":
        return "^";
      case "asciitilde":
        return "~";
      case "asterisk":
        return "*";
      case "at":
        return "@";
      case "atilde":
        return "ã";
      case "backslash":
        return "\\";
      case "bar":
        return "|";
      case "braceleft":
        return "{";
      case "braceright":
        return "}";
      case "bracketleft":
        return "[";
      case "bracketright":
        return "]";
      case "breve":
        return "˘";
      case "brokenbar":
        return "|";
      case "bullet":
        return "•";
      case "bullet3":
        return "•";
      case "caron":
        return "ˇ";
      case "ccaron":
        return "č";
      case "ccedilla":
        return "ç";
      case "cedilla":
        return "¸";
      case "cent":
        return "¢";
      case "circumflex":
        return "ˆ";
      case "colon":
        return ":";
      case "comma":
        return ",";
      case "copyright":
        return "©";
      case "currency1":
        return "¤";
      case "dagger":
        return "†";
      case "daggerdbl":
        return "‡";
      case "degree":
        return "°";
      case "dieresis":
        return "¨";
      case "divide":
        return "÷";
      case "dollar":
        return "$";
      case "dotaccent":
        return "˙";
      case "dotlessi":
        return "ı";
      case "eacute":
        return "é";
      case "ecircumflex":
        return "ê";
      case "edieresis":
        return "ë";
      case "egrave":
        return "è";
      case "eight":
        return "8";
      case "ellipsis":
        return "...";
      case "emdash":
        return "—";
      case "endash":
        return "–";
      case "equal":
        return "=";
      case "eth":
        return "ð";
      case "exclam":
        return "!";
      case "exclamdown":
        return "¡";
      case "five":
        return "5";
      case "florin":
        return "ƒ";
      case "four":
        return "4";
      case "fraction":
        return "⁄";
      case "germandbls":
        return "ß";
      case "grave":
        return "`";
      case "greater":
        return ">";
      case "guillemotleft4":
        return "«";
      case "guillemotright4":
        return "»";
      case "guilsinglleft":
        return "‹";
      case "guilsinglright":
        return "›";
      case "hungarumlaut":
        return "˝";
      case "hyphen":
        return "-";
      case "hyphen5":
        return "-";
      case "iacute":
        return "í";
      case "icircumflex":
        return "î";
      case "idieresis":
        return "ï";
      case "igrave":
        return "ì";
      case "less":
        return "<";
      case "logicalnot":
        return "¬";
      case "lslash":
        return "ł";
      case "macron":
        return "¯";
      case "middot":
        return "˙";
      case "minus":
        return "−";
      case "mu":
        return "μ";
      case "multiply":
        return "×";
      case "nine":
        return "9";
      case "ntilde":
        return "ñ";
      case "numbersign":
        return "#";
      case "oacute":
        return "ó";
      case "ocircumflex":
        return "ô";
      case "odieresis":
        return "ö";
      case "oe":
        return "oe";
      case "ogonek":
        return "˛";
      case "ograve":
        return "ò";
      case "one":
        return "1";
      case "onehalf":
        return "1/2";
      case "onequarter":
        return "1/4";
      case "onesuperior":
        return "\u00B9";
      case "ordfeminine":
        return "ª";
      case "ordmasculine":
        return "º";
      case "oslash":
        return "ø";
      case "otilde":
        return "õ";
      case "paragraph":
        return "¶";
      case "parenleft":
        return "(";
      case "parenright":
        return ")";
      case "percent":
        return "%";
      case "period":
        return ".";
      case "periodcentered":
        return "·";
      case "perthousand":
        return "‰";
      case "plus":
        return "+";
      case "plusminus":
        return "±";
      case "question":
        return "?";
      case "questiondown":
        return "¿";
      case "quotedbl":
        return "\"";
      case "quotedblbase":
        return "„";
      case "quotedblleft":
        return "“";
      case "quotedblright":
        return "”";
      case "quoteleft":
        return "‘";
      case "quoteright":
        return "’";
      case "quotesinglbase":
        return "‚";
      case "quotesingle":
        return "'";
      case "registered":
        return "®";
      case "ring":
        return "˚";
      case "scaron":
        return "š";
      case "section":
        return "§";
      case "semicolon":
        return ";";
      case "seven":
        return "7";
      case "six":
        return "6";
      case "slash":
        return "/";
      case "space":
        return " ";
      case "space6":
        return " ";
      case "sterling":
        return "£";
      case "three":
        return "3";
      case "two":
        return "2";
      case "udieresis":
        return "ü";
      case "underscore":
        return "_";
      case "zcaron":
        return "ž";
      case "zero":
        return "0";
      default:
        return decodedCharacter;
    }
  }

  private string GetLiteralString(string encodedText)
  {
    string str1 = encodedText;
    int startIndex = -1;
    int num1 = 3;
    while (str1.Contains("\\") || str1.Contains("\0"))
    {
      string empty = string.Empty;
      if (str1.IndexOf('\\', startIndex + 1) >= 0)
      {
        startIndex = str1.IndexOf('\\', startIndex + 1);
      }
      else
      {
        startIndex = str1.IndexOf(char.MinValue, startIndex + 1);
        if (startIndex >= 0)
          num1 = 2;
        else
          break;
      }
      for (int index = startIndex + 1; index <= startIndex + num1; ++index)
      {
        if (index < str1.Length)
        {
          int result = 0;
          if (int.TryParse(str1[index].ToString(), out result))
          {
            if (result <= 8)
              empty += str1[index].ToString();
          }
          else
          {
            empty = string.Empty;
            break;
          }
        }
        else
          empty = string.Empty;
      }
      if (empty != string.Empty)
      {
        int uint64 = (int) Convert.ToUInt64(empty, 8);
        char ch = (char) uint64;
        string str2;
        if (this.CharacterMapTable != null && this.CharacterMapTable.Count > 0)
          str2 = ch.ToString();
        else if (this.DifferencesDictionary != null && this.DifferencesDictionary.Count > 0)
          str2 = ch.ToString();
        else if (this.FontEncoding != "MacRomanEncoding")
        {
          str2 = Encoding.GetEncoding(1252).GetString(new byte[1]
          {
            Convert.ToByte(uint64)
          });
          char[] charArray = str2.ToCharArray();
          int key = 0;
          foreach (int num2 in charArray)
            key = num2;
          if (!this.OctDecMapTable.ContainsKey(key))
            this.OctDecMapTable.Add(key, uint64);
        }
        else if (this.MacEncodeTable.ContainsKey(uint64))
          str2 = this.MacEncodeTable[uint64];
        else
          str2 = Encoding.GetEncoding(1252).GetString(new byte[1]
          {
            Convert.ToByte(uint64)
          });
        str1 = str1.Remove(startIndex, num1 + 1).Insert(startIndex, str2);
      }
    }
    if (str1.Contains("\\") && this.m_fontEncoding != "Identity-H" && str1.Length > 1)
    {
      int num3 = str1.IndexOf("\\");
      switch (str1[num3 + 1])
      {
        case '(':
        case ')':
          Regex.Unescape(str1);
          break;
        default:
          return str1;
      }
    }
    return str1;
  }

  private void GetMacEncodeTable()
  {
    this.m_macEncodeTable = new Dictionary<int, string>();
    this.m_macEncodeTable.Add((int) sbyte.MaxValue, " ");
    this.m_macEncodeTable.Add(128 /*0x80*/, "Ä");
    this.m_macEncodeTable.Add(129, "Å");
    this.m_macEncodeTable.Add(130, "Ç");
    this.m_macEncodeTable.Add(131, "É");
    this.m_macEncodeTable.Add(132, "Ñ");
    this.m_macEncodeTable.Add(133, "Ö");
    this.m_macEncodeTable.Add(134, "Ü");
    this.m_macEncodeTable.Add(135, "á");
    this.m_macEncodeTable.Add(136, "à");
    this.m_macEncodeTable.Add(137, "â");
    this.m_macEncodeTable.Add(138, "ä");
    this.m_macEncodeTable.Add(139, "ã");
    this.m_macEncodeTable.Add(140, "å");
    this.m_macEncodeTable.Add(141, "ç");
    this.m_macEncodeTable.Add(142, "é");
    this.m_macEncodeTable.Add(143, "è");
    this.m_macEncodeTable.Add(144 /*0x90*/, "ê");
    this.m_macEncodeTable.Add(145, "ë");
    this.m_macEncodeTable.Add(146, "í");
    this.m_macEncodeTable.Add(147, "ì");
    this.m_macEncodeTable.Add(148, "î");
    this.m_macEncodeTable.Add(149, "ï");
    this.m_macEncodeTable.Add(150, "ñ");
    this.m_macEncodeTable.Add(151, "ó");
    this.m_macEncodeTable.Add(152, "ò");
    this.m_macEncodeTable.Add(153, "ô");
    this.m_macEncodeTable.Add(154, "ö");
    this.m_macEncodeTable.Add(155, "õ");
    this.m_macEncodeTable.Add(156, "ú");
    this.m_macEncodeTable.Add(157, "ù");
    this.m_macEncodeTable.Add(158, "û");
    this.m_macEncodeTable.Add(159, "ü");
    this.m_macEncodeTable.Add(160 /*0xA0*/, "†");
    this.m_macEncodeTable.Add(161, "°");
    this.m_macEncodeTable.Add(162, "¢");
    this.m_macEncodeTable.Add(163, "£");
    this.m_macEncodeTable.Add(164, "§");
    this.m_macEncodeTable.Add(165, "•");
    this.m_macEncodeTable.Add(166, "¶");
    this.m_macEncodeTable.Add(167, "ß");
    this.m_macEncodeTable.Add(168, "®");
    this.m_macEncodeTable.Add(169, "©");
    this.m_macEncodeTable.Add(170, "™");
    this.m_macEncodeTable.Add(171, "´");
    this.m_macEncodeTable.Add(172, "¨");
    this.m_macEncodeTable.Add(173, "≠");
    this.m_macEncodeTable.Add(174, "Æ");
    this.m_macEncodeTable.Add(175, "Ø");
    this.m_macEncodeTable.Add(176 /*0xB0*/, "∞");
    this.m_macEncodeTable.Add(177, "±");
    this.m_macEncodeTable.Add(178, "≤");
    this.m_macEncodeTable.Add(179, "≥");
    this.m_macEncodeTable.Add(180, "¥");
    this.m_macEncodeTable.Add(181, "µ");
    this.m_macEncodeTable.Add(182, "∂");
    this.m_macEncodeTable.Add(183, "∑");
    this.m_macEncodeTable.Add(184, "∏");
    this.m_macEncodeTable.Add(185, "π");
    this.m_macEncodeTable.Add(186, "∫");
    this.m_macEncodeTable.Add(187, "ª");
    this.m_macEncodeTable.Add(188, "º");
    this.m_macEncodeTable.Add(189, "Ω");
    this.m_macEncodeTable.Add(190, "æ");
    this.m_macEncodeTable.Add(191, "ø");
    this.m_macEncodeTable.Add(192 /*0xC0*/, "¿");
    this.m_macEncodeTable.Add(193, "¡");
    this.m_macEncodeTable.Add(194, "¬");
    this.m_macEncodeTable.Add(195, "√");
    this.m_macEncodeTable.Add(196, "ƒ");
    this.m_macEncodeTable.Add(197, "≈");
    this.m_macEncodeTable.Add(198, "∆");
    this.m_macEncodeTable.Add(199, "«");
    this.m_macEncodeTable.Add(200, "»");
    this.m_macEncodeTable.Add(201, "…");
    this.m_macEncodeTable.Add(202, " ");
    this.m_macEncodeTable.Add(203, "À");
    this.m_macEncodeTable.Add(204, "Ã");
    this.m_macEncodeTable.Add(205, "Õ");
    this.m_macEncodeTable.Add(206, "Œ");
    this.m_macEncodeTable.Add(207, "œ");
    this.m_macEncodeTable.Add(208 /*0xD0*/, "–");
    this.m_macEncodeTable.Add(209, "—");
    this.m_macEncodeTable.Add(210, "“");
    this.m_macEncodeTable.Add(211, "”");
    this.m_macEncodeTable.Add(212, "‘");
    this.m_macEncodeTable.Add(213, "’");
    this.m_macEncodeTable.Add(214, "÷");
    this.m_macEncodeTable.Add(215, "◊");
    this.m_macEncodeTable.Add(216, "ÿ");
    this.m_macEncodeTable.Add(217, "Ÿ");
    this.m_macEncodeTable.Add(218, "⁄");
    this.m_macEncodeTable.Add(219, "€");
    this.m_macEncodeTable.Add(220, "‹");
    this.m_macEncodeTable.Add(221, "›");
    this.m_macEncodeTable.Add(222, "ﬁ");
    this.m_macEncodeTable.Add(223, "ﬂ");
    this.m_macEncodeTable.Add(224 /*0xE0*/, "‡");
    this.m_macEncodeTable.Add(225, "·");
    this.m_macEncodeTable.Add(226, ",");
    this.m_macEncodeTable.Add(227, "„");
    this.m_macEncodeTable.Add(228, "‰");
    this.m_macEncodeTable.Add(229, "Â");
    this.m_macEncodeTable.Add(230, "Ê");
    this.m_macEncodeTable.Add(231, "Á");
    this.m_macEncodeTable.Add(232, "Ë");
    this.m_macEncodeTable.Add(233, "È");
    this.m_macEncodeTable.Add(234, "Í");
    this.m_macEncodeTable.Add(235, "Î");
    this.m_macEncodeTable.Add(236, "Ï");
    this.m_macEncodeTable.Add(237, "Ì");
    this.m_macEncodeTable.Add(238, "Ó");
    this.m_macEncodeTable.Add(239, "Ô");
    this.m_macEncodeTable.Add(240 /*0xF0*/, "\uF8FF");
    this.m_macEncodeTable.Add(241, "Ò");
    this.m_macEncodeTable.Add(242, "Ú");
    this.m_macEncodeTable.Add(243, "Û");
    this.m_macEncodeTable.Add(244, "Ù");
    this.m_macEncodeTable.Add(245, "ı");
    this.m_macEncodeTable.Add(246, "ˆ");
    this.m_macEncodeTable.Add(247, "˜");
    this.m_macEncodeTable.Add(248, "¯");
    this.m_macEncodeTable.Add(249, "˘");
    this.m_macEncodeTable.Add(250, "˙");
    this.m_macEncodeTable.Add(251, "˚");
    this.m_macEncodeTable.Add(252, "¸");
    this.m_macEncodeTable.Add(253, "˝");
    this.m_macEncodeTable.Add(254, "˛");
    this.m_macEncodeTable.Add((int) byte.MaxValue, "ˇ");
  }

  private Dictionary<string, double> GetReverseMapTable()
  {
    this.m_reverseMapTable = new Dictionary<string, double>();
    foreach (KeyValuePair<double, string> keyValuePair in this.CharacterMapTable)
    {
      if (!this.m_reverseMapTable.ContainsKey(keyValuePair.Value))
        this.m_reverseMapTable.Add(keyValuePair.Value, keyValuePair.Key);
    }
    return this.m_reverseMapTable;
  }

  internal static string GetSpecialCharacter(string decodedCharacter)
  {
    switch (decodedCharacter)
    {
      case ".notdef":
        return "▯";
      case "aacute":
        return "á";
      case "airplane":
        return "✈";
      case "ampersandbld":
      case "ampersanditaldm":
      case "ampersandsandm":
      case "ampersandsans":
        return "&";
      case "ampersandit":
        return "&";
      case "ampersanditlc":
        return "&";
      case "aquarius":
        return "♒";
      case "aries":
        return "♈";
      case "barb4right":
        return "➔";
      case "bdash1":
        return "▭";
      case "bdash2":
        return "▫";
      case "bdown":
        return "⇩";
      case "bell":
        return "✁";
      case "bleft":
        return "⇦";
      case "bleftright":
        return "⬄";
      case "bne":
        return "⬁";
      case "bnw":
        return "⬀";
      case "book":
        return "✁";
      case "box2":
        return "◻";
      case "box3":
        return "□";
      case "boxcheck":
        return "☑";
      case "boxcheckbld":
        return "☑";
      case "boxshadowdwn":
        return "❑";
      case "boxshadowup":
        return "❒";
      case "boxx":
        return "☒";
      case "boxxbld":
        return "☒";
      case "boxxmarkbld":
        return "☒";
      case "bright":
        return "⇨";
      case "bse":
        return "⬂";
      case "bsw":
        return "⬃";
      case "bup":
        return "⇧";
      case "bupdown":
        return "⇳";
      case "cancer":
        return "♋";
      case "capricorn":
        return "♑";
      case "check":
        return "✓";
      case "checkbld":
        return "✓";
      case "circle2":
        return "·";
      case "circle4":
        return "•";
      case "circle6":
        return "●";
      case "circleright":
        return "➲";
      case "circleshadowdwn":
        return "❍";
      case "circlestar":
        return "★";
      case "circlex":
        return "=⌔";
      case "circlexbld":
        return "⌔";
      case "clear":
        return "⌧";
      case "command":
        return "⌘";
      case "crescentstar":
        return "☪";
      case "crossmaltese":
        return "✠";
      case "crossshadow":
        return "✞";
      case "crosstar2":
        return "✦";
      case "cuspopen":
        return "⟡";
      case "cuspopen1":
        return "⌑";
      case "deleteleft":
        return "⌫";
      case "deleteright":
        return "⌦";
      case "dodecastar3":
        return "✹";
      case "droplet":
        return "Ὂ7";
      case "eacute":
        return "é";
      case "eightsans":
        return "\u2467";
      case "envelopeback":
        return "✁";
      case "escape":
        return "⍓";
      case "fivesans":
        return "\u2464";
      case "flag":
        return "⚐";
      case "foursans":
        return "\u2463";
      case "foursansinv":
        return "\u2779";
      case "frownface":
        return "☹";
      case "gemini":
        return "♊";
      case "handptdown":
        return "☟";
      case "handptdwn1":
        return "☟";
      case "handptleft":
        return "☜";
      case "handptlft1":
        return "☜";
      case "handptlftsld1":
        return "☚";
      case "handptright":
        return "☞";
      case "handptrt1":
        return "☞";
      case "handptrtsld1":
        return "☛";
      case "handptup":
        return "☝";
      case "handptup1":
        return "☝";
      case "handv":
        return "✌";
      case "handwrite":
        return "✍";
      case "head2right":
        return "➢";
      case "hexstar2":
        return "✶";
      case "hourglass":
        return "⌛";
      case "iacute":
        return "í";
      case "interrobang":
      case "interrobangdm":
      case "interrobangsans":
      case "interrobngsandm":
        return "‽";
      case "keyboard":
        return "⌨";
      case "leo":
        return "♌";
      case "libra":
        return "♎";
      case "lozenge4":
        return "⬧";
      case "lozenge6":
        return "⧫";
      case "ninesans":
        return "\u2468";
      case "oacute":
        return "ó";
      case "octastar2":
        return "✴";
      case "octastar4":
        return "✵";
      case "om":
        return "ॐ";
      case "onesans":
        return "\u2460";
      case "onesansinv":
        return "\u2776";
      case "pencil":
        return "✏";
      case "pennant":
        return "Ὢ9";
      case "pentastar2":
        return "★";
      case "pisces":
        return "♓";
      case "prohibit":
      case "prohibitbld":
        return "⦸";
      case "quotedbllftbld":
        return "❝";
      case "quotedblrtbld":
        return "❞";
      case "readingglasses":
        return "✁";
      case "registercircle":
        return "⌖";
      case "rhombus4":
        return "⬥";
      case "rhombus6":
        return "◆";
      case "ring2":
        return "○";
      case "ringbutton2":
        return "◉";
      case "rosette":
        return "❀";
      case "rosettesolid":
        return "✿";
      case "saggitarius":
        return "♐";
      case "scissors":
        return "✂";
      case "scissorscutting":
        return "✁";
      case "scissorsoutline":
        return "✄";
      case "scorpio":
        return "♏";
      case "sevensans":
        return "\u2466";
      case "sixsans":
        return "\u2465";
      case "skullcrossbones":
        return "☠";
      case "smileface":
        return "☺";
      case "snowflake":
        return "❄";
      case "space":
        return " ";
      case "square2":
        return "▪";
      case "square4":
        return "▪";
      case "square6":
        return "■";
      case "starofdavid":
        return "✡";
      case "starshadow":
        return "✰";
      case "sunshine":
        return "☼";
      case "tapereel":
        return "✇";
      case "target":
        return "◎";
      case "taurus":
        return "♉";
      case "telephone":
        return "☏";
      case "telephonesolid":
        return "✁";
      case "telhandset":
        return "ὍE";
      case "telhandsetcirc":
        return "✁";
      case "tensans":
        return "\u2469";
      case "threesans":
        return "\u2462";
      case "threesansinv":
        return "\u2778";
      case "twosans":
        return "\u2461";
      case "twosansinv":
        return "\u2777";
      case "uacute":
        return "ú";
      case "virgo":
        return "♍";
      case "wheel":
        return "☸";
      case "xmark":
        return "✗";
      case "xmarkbld":
        return "✗";
      case "xrhombus":
        return "❖";
      case "yinyang":
        return "☯";
      case "zerosans":
        return "\u24EA";
      case "zerosansinv":
        return "\u24FF";
      default:
        return decodedCharacter;
    }
  }

  internal Dictionary<int, string> GetUnicodeCharMapTable()
  {
    FontStructure.unicodeCharMapTable = new Dictionary<int, string>();
    StreamReader standardEncoding = Resources.standard_encoding;
    while (true)
    {
      string str1;
      int uint32;
      do
      {
        string str2;
        do
        {
          string str3 = standardEncoding.ReadLine();
          if (str3 == null)
            return FontStructure.unicodeCharMapTable;
          string[] strArray = str3.Split(' ');
          str1 = strArray[0];
          str2 = strArray[1];
        }
        while (!char.IsDigit(str2[0]));
        uint32 = (int) Convert.ToUInt32(str2, 8);
      }
      while (FontStructure.unicodeCharMapTable.ContainsKey(uint32));
      FontStructure.unicodeCharMapTable.Add(uint32, str1);
    }
  }

  internal bool IsCIDFontType()
  {
    bool flag = false;
    if (!this.m_fontDictionary.Items.ContainsKey(new PdfName("DescendantFonts")))
      return flag;
    PdfReferenceHolder pdfReferenceHolder = this.m_fontDictionary.Items[new PdfName("DescendantFonts")] as PdfReferenceHolder;
    if (pdfReferenceHolder == (PdfReferenceHolder) null)
      return flag;
    PdfArray pdfArray = pdfReferenceHolder.Object as PdfArray;
    if ((object) (pdfArray[0] as PdfReferenceHolder) == null)
      return flag;
    PdfName pdfName = ((pdfArray[0] as PdfReferenceHolder).Object as PdfDictionary)["Subtype"] as PdfName;
    return pdfName.Value == "CIDFontType2" || pdfName.Value == "CIDFontType0" || flag;
  }

  internal string MapCharactersFromTable(string decodedText)
  {
    string empty = string.Empty;
    bool flag = false;
    foreach (char key in decodedText)
    {
      if (this.CharacterMapTable.ContainsKey((double) key) && !flag)
      {
        string str = this.CharacterMapTable[(double) key];
        if (str.Contains("�"))
        {
          int startIndex = str.IndexOf("�");
          str = str.Remove(startIndex, 1);
        }
        empty += str;
        flag = false;
      }
      else if (FontStructure.tempMapTable.ContainsKey((double) key) && !flag)
      {
        string str = FontStructure.tempMapTable[(double) key];
        if (str.Contains("�"))
        {
          int startIndex = str.IndexOf("�");
          str = str.Remove(startIndex, 1);
        }
        empty += str;
        flag = false;
      }
      else if (flag)
      {
        switch (key.ToString())
        {
          case "'":
            if (this.CharacterMapTable.ContainsKey(39.0))
            {
              empty += this.CharacterMapTable[39.0];
              break;
            }
            break;
          case "a":
            if (this.CharacterMapTable.ContainsKey(7.0))
            {
              empty += this.CharacterMapTable[7.0];
              break;
            }
            break;
          case "b":
            if (this.CharacterMapTable.ContainsKey(8.0))
            {
              empty += this.CharacterMapTable[8.0];
              break;
            }
            break;
          case "f":
            if (this.CharacterMapTable.ContainsKey(12.0))
            {
              empty += this.CharacterMapTable[12.0];
              break;
            }
            break;
          case "n":
            if (this.CharacterMapTable.ContainsKey(10.0))
            {
              empty += this.CharacterMapTable[10.0];
              break;
            }
            break;
          case "r":
            if (this.CharacterMapTable.ContainsKey(13.0))
            {
              empty += this.CharacterMapTable[13.0];
              break;
            }
            break;
          case "t":
            if (this.CharacterMapTable.ContainsKey(9.0))
            {
              empty += this.CharacterMapTable[9.0];
              break;
            }
            break;
          case "v":
            if (this.CharacterMapTable.ContainsKey(11.0))
            {
              empty += this.CharacterMapTable[11.0];
              break;
            }
            break;
          default:
            if (this.CharacterMapTable.ContainsKey((double) key))
            {
              empty += this.CharacterMapTable[(double) key];
              break;
            }
            break;
        }
        flag = false;
      }
      else if (key == '\\')
        flag = true;
      else
        empty += key.ToString();
    }
    return empty;
  }

  internal string MapCidToGid(string decodedText)
  {
    string empty = string.Empty;
    bool flag = false;
    foreach (char key in decodedText)
    {
      if (this.m_cidToGidTable.ContainsKey((double) key) && !flag)
      {
        string str = this.m_cidToGidTable[(double) key];
        if (str.Contains("�"))
        {
          int startIndex = str.IndexOf("�");
          str = str.Remove(startIndex, 1);
        }
        if (str.Length > 0 && !this.CidToGidReverseMapTable.ContainsKey((int) str[0]))
          this.CidToGidReverseMapTable.Add((int) str[0], (int) key);
        empty += str;
        flag = false;
      }
      else if (FontStructure.tempMapTable.ContainsKey((double) key) && !flag)
      {
        string str = FontStructure.tempMapTable[(double) key];
        if (str.Contains("�"))
        {
          int startIndex = str.IndexOf("�");
          str = str.Remove(startIndex, 1);
        }
        empty += str;
        flag = false;
      }
      else if (flag)
      {
        switch (key.ToString())
        {
          case "'":
            if (this.m_cidToGidTable.ContainsKey(39.0))
            {
              empty += this.CharacterMapTable[39.0];
              break;
            }
            break;
          case "a":
            if (this.m_cidToGidTable.ContainsKey(7.0))
            {
              empty += this.CharacterMapTable[7.0];
              break;
            }
            break;
          case "b":
            if (this.m_cidToGidTable.ContainsKey(8.0))
            {
              empty += this.CharacterMapTable[8.0];
              break;
            }
            break;
          case "f":
            if (this.m_cidToGidTable.ContainsKey(12.0))
            {
              empty += this.CharacterMapTable[12.0];
              break;
            }
            break;
          case "n":
            if (this.m_cidToGidTable.ContainsKey(10.0))
            {
              empty += this.CharacterMapTable[10.0];
              break;
            }
            break;
          case "r":
            if (this.m_cidToGidTable.ContainsKey(13.0))
            {
              empty += this.CharacterMapTable[13.0];
              break;
            }
            break;
          case "t":
            if (this.m_cidToGidTable.ContainsKey(9.0))
            {
              empty += this.CharacterMapTable[9.0];
              break;
            }
            break;
          case "v":
            if (this.m_cidToGidTable.ContainsKey(11.0))
            {
              empty += this.CharacterMapTable[11.0];
              break;
            }
            break;
          default:
            if (this.m_cidToGidTable.ContainsKey((double) key))
            {
              empty += this.CharacterMapTable[(double) key];
              break;
            }
            break;
        }
        flag = false;
      }
      else if (key == '\\')
        flag = true;
    }
    return empty;
  }

  private string MapDifferenceOfWingDings(string decodedText)
  {
    if (decodedText.Length > 1 && decodedText.Contains("c") && decodedText.IndexOf("c") == 0)
    {
      decodedText = decodedText.Remove(0, 1);
      int result = 0;
      int.TryParse(decodedText, out result);
      decodedText = ((char) result).ToString();
    }
    return decodedText;
  }

  internal string MapDifferences(string encodedText)
  {
    string str1 = string.Empty;
    bool flag = false;
    try
    {
      encodedText = Regex.Unescape(encodedText);
    }
    catch (ArgumentException ex)
    {
      encodedText = !string.IsNullOrEmpty(encodedText) ? Regex.Unescape(Regex.Escape(encodedText)) : throw ex;
    }
    foreach (char ch in encodedText)
    {
      Dictionary<string, string> differencesDictionary1 = this.DifferencesDictionary;
      int num1 = (int) ch;
      string key1 = num1.ToString();
      if (differencesDictionary1.ContainsKey(key1))
      {
        string str2 = str1;
        Dictionary<string, string> differencesDictionary2 = this.DifferencesDictionary;
        num1 = (int) ch;
        string key2 = num1.ToString();
        string str3 = differencesDictionary2[key2];
        str1 = str2 + str3;
        if (!this.ReverseDictMapping.ContainsKey(this.DifferencesDictionary[((int) ch).ToString()]))
          this.ReverseDictMapping.Add(this.DifferencesDictionary[((int) ch).ToString()], (int) ch);
        if (this.FontName == "Wingdings")
          str1 = this.MapDifferenceOfWingDings(str1);
        string specialCharacter = PdfTextExtractor.GetSpecialCharacter(str1);
        if (str1 != specialCharacter)
          str1 = str1.Replace(str1, specialCharacter);
        flag = false;
      }
      else if (flag)
      {
        switch (ch)
        {
          case 'n':
            if (this.DifferencesDictionary.ContainsKey(10.ToString()))
            {
              int num2 = 10;
              str1 += this.DifferencesDictionary[num2.ToString()];
              break;
            }
            break;
          case 'r':
            if (this.DifferencesDictionary.ContainsKey(13.ToString()))
            {
              int num3 = 13;
              str1 += this.DifferencesDictionary[num3.ToString()];
              break;
            }
            break;
        }
        flag = false;
      }
      else if (ch == '\\')
        flag = true;
      else
        str1 += ch.ToString();
    }
    return str1;
  }

  internal string MapHebrewCharacters(string hexChar)
  {
    if (hexChar.Substring(0, 2) == "02")
    {
      hexChar = (int.Parse(hexChar, NumberStyles.HexNumber) + 816).ToString("X");
      return hexChar;
    }
    if (hexChar.Substring(0, 2) == "00")
    {
      if (hexChar.Substring(2, 1) == "0" || hexChar.Substring(2, 1) == "1")
      {
        hexChar = (int.Parse(hexChar, NumberStyles.HexNumber) + 29).ToString("X");
        return hexChar;
      }
      hexChar = (int.Parse(hexChar, NumberStyles.HexNumber) + 1335).ToString("X");
    }
    return hexChar;
  }

  private string MapZapf(string encodedText)
  {
    string str = (string) null;
    foreach (int num in encodedText)
    {
      switch (num.ToString("X"))
      {
        case "20":
          str += " ";
          break;
        case "21":
          str += "✁";
          break;
        case "33":
        case "34":
          str += "✔";
          break;
        case "35":
        case "36":
          str += "✖";
          break;
        case "38":
          str += "✗";
          break;
        case "48":
          str += "★";
          break;
        case "57":
          str += "✷";
          break;
        case "64":
          str += "❄";
          break;
        case "65":
          str += "❅";
          break;
        case "6C":
          str += "●";
          break;
        case "6E":
          str += "■";
          break;
        case "6F":
          str += "❏";
          break;
        case "72":
          str += "❒";
          break;
        default:
          str += "✈";
          break;
      }
    }
    return str;
  }

  private string SkipEscapeSequence(string text)
  {
    if (text.Contains("\\"))
    {
      int num = text.IndexOf('\\');
      if (num + 1 == text.Length)
        return text;
      switch (text.Substring(num + 1, 1))
      {
        case "'":
          text = text.Replace("\\'", "'");
          return text;
        case "a":
          text = text.Replace("\\a", "\a");
          return text;
        case "b":
          text = text.Replace("\\b", "\b");
          return text;
        case "f":
          text = text.Replace("\\f", "\f");
          return text;
        case "n":
          text = text.Replace("\\n", "\n");
          return text;
        case "r":
          text = text.Replace("\\r", "\r");
          return text;
        case "t":
          text = text.Replace("\\t", "\t");
          return text;
        case "v":
          text = text.Replace("\\v", "\v");
          return text;
        default:
          try
          {
            text = Regex.Unescape(text);
            break;
          }
          catch (ArgumentException ex)
          {
            text = !string.IsNullOrEmpty(text) ? Regex.Unescape(Regex.Escape(text)) : throw ex;
            return text;
          }
      }
    }
    return text;
  }

  internal Dictionary<double, string> CharacterMapTable
  {
    get
    {
      if (this.m_characterMapTable == null)
        this.m_characterMapTable = this.GetCharacterMapTable();
      return this.m_characterMapTable;
    }
    set => this.m_characterMapTable = value;
  }

  internal Dictionary<double, string> CidToGidMap => this.m_cidToGidTable;

  internal Dictionary<int, int> CidToGidReverseMapTable
  {
    get
    {
      if (this.m_cidToGidReverseMapTable == null)
        this.m_cidToGidReverseMapTable = new Dictionary<int, int>();
      return this.m_cidToGidReverseMapTable;
    }
    set => this.m_cidToGidReverseMapTable = value;
  }

  internal bool ContainsCmap => this.m_fontFileContainsCmap;

  public Font CurrentFont
  {
    get
    {
      if (this.m_currentFont == null && !this.isGetFontCalled && (double) this.m_fontSize != 0.0)
        this.m_currentFont = this.GetFont(this.m_fontSize);
      return this.m_currentFont;
    }
  }

  internal Dictionary<string, string> DifferencesDictionary
  {
    get
    {
      if (this.m_differencesDictionary == null)
        this.m_differencesDictionary = this.GetDifferencesDictionary();
      return this.m_differencesDictionary;
    }
    set => this.m_differencesDictionary = value;
  }

  public PdfNumber Flags => this.GetFlagValue();

  public string FontEncoding
  {
    get
    {
      if (this.m_fontEncoding == null)
        this.m_fontEncoding = this.GetFontEncoding();
      return this.m_fontEncoding;
    }
  }

  internal Dictionary<int, int> FontGlyphWidths
  {
    get
    {
      if (this.FontEncoding == "Identity-H")
        this.GetGlyphWidths();
      else
        this.GetGlyphWidthsNonIdH();
      return this.m_fontGlyphWidth;
    }
    set => this.m_fontGlyphWidth = value;
  }

  public string FontName
  {
    get
    {
      if (this.m_fontName == null)
        this.m_fontName = this.GetFontName();
      return this.m_fontName;
    }
  }

  public float FontSize
  {
    get => this.m_fontSize;
    set
    {
      this.m_fontSize = value;
      this.m_currentFont = this.GetFont(this.m_fontSize);
    }
  }

  public FontStyle FontStyle
  {
    get
    {
      if (this.m_fontStyle == FontStyle.Regular)
        this.m_fontStyle = this.GetFontStyle();
      return this.m_fontStyle;
    }
  }

  internal FontFile2 GlyfFontFile2
  {
    get => this.m_fontfile2;
    set => this.m_fontfile2 = value;
  }

  internal bool IsCID
  {
    get
    {
      this.m_isCID = this.IsCIDFontType();
      return this.m_isCID;
    }
    set => this.m_isCID = value;
  }

  public bool IsNonSymbol => this.GetFlag((byte) 6);

  public bool IsSameFont
  {
    get => this.m_isSameFont;
    set => this.m_isSameFont = value;
  }

  public bool Issymbol => this.GetFlag((byte) 3);

  internal Dictionary<int, string> MacEncodeTable
  {
    get
    {
      if (this.m_macEncodeTable == null)
        this.GetMacEncodeTable();
      return this.m_macEncodeTable;
    }
    set => this.m_macEncodeTable = value;
  }

  internal Dictionary<int, int> OctDecMapTable
  {
    get
    {
      if (this.m_octDecMapTable == null)
        this.m_octDecMapTable = new Dictionary<int, int>();
      return this.m_octDecMapTable;
    }
    set => this.m_octDecMapTable = value;
  }

  internal Dictionary<string, double> ReverseMapTable
  {
    get
    {
      if (this.m_reverseMapTable == null)
        this.m_reverseMapTable = this.GetReverseMapTable();
      return this.m_reverseMapTable;
    }
    set => this.m_reverseMapTable = value;
  }

  internal float Type1GlyphHeight => this.m_type1GlyphHeight;

  internal Dictionary<int, string> UnicodeCharMapTable
  {
    get
    {
      if (FontStructure.unicodeCharMapTable == null)
        FontStructure.unicodeCharMapTable = this.GetUnicodeCharMapTable();
      return FontStructure.unicodeCharMapTable;
    }
    set => FontStructure.unicodeCharMapTable = value;
  }
}
