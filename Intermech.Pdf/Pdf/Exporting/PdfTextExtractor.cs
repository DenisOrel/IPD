// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Exporting.PdfTextExtractor
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;


namespace Syncfusion.Pdf.Exporting
{
    internal class PdfTextExtractor
    {
      private static List<string> m_decodedChar;
      private static Dictionary<string, List<string>> m_differenceArray;
      private static int m_numberOfChars = 50;

      private PdfTextExtractor() => throw new NotImplementedException();

      private static bool CheckToken(string[] tokens, char[] recent)
      {
        foreach (string token in tokens)
        {
          if (token.Length > 1)
          {
            if ((int) recent[PdfTextExtractor.m_numberOfChars - 3] == (int) token[0] && (int) recent[PdfTextExtractor.m_numberOfChars - 2] == (int) token[1] && (recent[PdfTextExtractor.m_numberOfChars - 1] == ' ' || recent[PdfTextExtractor.m_numberOfChars - 1] == '\r' || recent[PdfTextExtractor.m_numberOfChars - 1] == '\n') && (recent[PdfTextExtractor.m_numberOfChars - 4] == ' ' || recent[PdfTextExtractor.m_numberOfChars - 4] == '\r' || recent[PdfTextExtractor.m_numberOfChars - 4] == '\n'))
              return true;
          }
          else if ((int) recent[PdfTextExtractor.m_numberOfChars - 3] == (int) token[0] && (recent[PdfTextExtractor.m_numberOfChars - 1] == ' ' || recent[PdfTextExtractor.m_numberOfChars - 1] == '\r' || recent[PdfTextExtractor.m_numberOfChars - 1] == '\n') && (recent[PdfTextExtractor.m_numberOfChars - 4] == ' ' || recent[PdfTextExtractor.m_numberOfChars - 4] == '\r' || recent[PdfTextExtractor.m_numberOfChars - 4] == '\n'))
            return true;
        }
        return false;
      }

      internal static string ExtractTextFrom_Type0Fonts(
        byte[] data,
        List<IPdfPrimitive> fontref,
        List<PdfName> fontname,
        PdfCrossTable crosstable)
      {
        string str1 = string.Empty;
        if (data != null)
        {
          if (data.Length != 0)
          {
            try
            {
              string empty = string.Empty;
              bool flag1 = false;
              bool flag2 = false;
              int num1 = 0;
              bool flag3 = false;
              bool flag4 = false;
              char[] recent = new char[PdfTextExtractor.m_numberOfChars];
              for (int index = 0; index < PdfTextExtractor.m_numberOfChars; ++index)
                recent[index] = ' ';
              for (int sourceIndex = 0; sourceIndex < data.Length; ++sourceIndex)
              {
                char ch = (char) data[sourceIndex];
                if (PdfTextExtractor.CheckToken(new string[1]
                {
                  "Tf"
                }, recent))
                {
                  int num2 = 0;
                  int num3 = 0;
                  for (int index = 0; index < recent.Length; ++index)
                  {
                    if (recent[index] == '/')
                      num2 = index;
                    else if (index > 0 && recent[index] == 'f' && recent[index - 1] == 'T')
                    {
                      num3 = index;
                      break;
                    }
                  }
                  string str2 = new string(recent).Substring(num2 + 1, num3 - 1 - num2);
                  string str3 = str2.Substring(0, str2.IndexOf(' '));
                  if (fontname.Contains((PdfName) str3))
                  {
                    PdfReferenceHolder pointer = fontref[fontname.IndexOf((PdfName) str3)] as PdfReferenceHolder;
                    PdfDictionary pdfDictionary = crosstable.GetObject((IPdfPrimitive) pointer) as PdfDictionary;
                    pdfDictionary["BaseFont"].ToString();
                    if (pdfDictionary["Subtype"].ToString() == "/Type0")
                    {
                      str1 = "Type0";
                      if (pdfDictionary.ContainsKey("Encoding"))
                      {
                        PdfName pdfName = pdfDictionary["Encoding"] as PdfName;
                        if (pdfName != (PdfName) null && (pdfName.Value == "Identity-H" || pdfName.Value == "Identity-V"))
                          flag4 = true;
                      }
                      if (pdfDictionary.ContainsKey("ToUnicode") && pdfDictionary["ToUnicode"] as PdfReferenceHolder != (PdfReferenceHolder) null & flag4)
                        flag4 = false;
                    }
                    else if (pdfDictionary["Subtype"].ToString() == "/TrueType")
                      str1 = "TrueType";
                  }
                }
                if (flag1)
                {
                  if (num1 == 0)
                  {
                    if (PdfTextExtractor.CheckToken(new string[1]
                    {
                      "TD"
                    }, recent))
                    {
                      empty += Environment.NewLine;
                      flag3 = !flag3;
                    }
                    else if (data[sourceIndex - 1] == (byte) 10 && ch == '-')
                    {
                      if (!empty.EndsWith(Environment.NewLine))
                      {
                        empty += Environment.NewLine;
                        flag3 = !flag3;
                      }
                    }
                    else if (PdfTextExtractor.CheckToken(new string[3]
                    {
                      "'",
                      "T*",
                      "\""
                    }, recent))
                      empty += Environment.NewLine;
                    else if (PdfTextExtractor.CheckToken(new string[1]
                    {
                      "Tj"
                    }, recent))
                      empty += string.Empty;
                  }
                  if (num1 == 0)
                  {
                    if (PdfTextExtractor.CheckToken(new string[1]
                    {
                      "ET"
                    }, recent))
                    {
                      flag1 = false;
                      empty += " ";
                      goto label_56;
                    }
                  }
                  if (ch == '(' && num1 == 0 && !flag2)
                    num1 = 1;
                  else if (ch == ')' && num1 == 1 && !flag2)
                    num1 = 0;
                  else if (num1 == 1)
                  {
                    if (ch == '\\' && !flag2)
                    {
                      flag2 = true;
                      char[] destinationArray = new char[4];
                      if (data.Length >= sourceIndex + 4)
                      {
                        Array.Copy((Array) data, sourceIndex, (Array) destinationArray, 0, 4);
                        if (new string(destinationArray) == "\\000")
                        {
                          sourceIndex += 3;
                          continue;
                        }
                      }
                    }
                    else
                    {
                      if (str1 == "TrueType" && (ch >= ' ' && ch <= '~' || ch >= '\u0080' && ch < 'ÿ'))
                        empty += ch.ToString();
                      else if (str1 == "Type0" & flag4)
                        empty += ch.ToString();
                      else if (str1 == "Type0" && (ch >= '\u0003' && ch <= 'a' || ch >= 'c' && ch < 'â'))
                      {
                        ch += '\u001D';
                        empty += ch.ToString();
                      }
                      else if (recent[PdfTextExtractor.m_numberOfChars - 1] == char.MinValue)
                        empty += " ";
                      flag2 = false;
                    }
                  }
                }
    label_56:
                for (int index = 0; index < PdfTextExtractor.m_numberOfChars - 1; ++index)
                  recent[index] = recent[index + 1];
                recent[PdfTextExtractor.m_numberOfChars - 1] = ch;
                if (!flag1)
                {
                  if (PdfTextExtractor.CheckToken(new string[1]
                  {
                    "BT"
                  }, recent))
                    flag1 = true;
                }
              }
              return empty;
            }
            catch
            {
              return " ";
            }
          }
        }
        return " ";
      }

      public static string ExtractTextFromBytes(byte[] data)
      {
        if (data != null)
        {
          if (data.Length != 0)
          {
            try
            {
              string textFromBytes = string.Empty;
              bool flag1 = false;
              string str1 = (string) null;
              string str2 = (string) null;
              bool flag2 = false;
              bool flag3 = false;
              int num1 = 0;
              string str3 = "";
              bool flag4 = false;
              bool flag5 = false;
              bool flag6 = false;
              string str4 = string.Empty;
              string empty1 = string.Empty;
              string str5 = string.Empty;
              string empty2 = string.Empty;
              char[] recent = new char[PdfTextExtractor.m_numberOfChars];
              for (int index = 0; index < PdfTextExtractor.m_numberOfChars; ++index)
                recent[index] = ' ';
              float num2 = 0.0f;
              bool flag7 = true;
              bool flag8 = false;
              for (int index1 = 0; index1 < data.Length; ++index1)
              {
                char index2 = (char) data[index1];
                if (PdfTextExtractor.m_differenceArray.Count > 0)
                {
                  if (PdfTextExtractor.CheckToken(new string[1]
                  {
                    "Tf"
                  }, recent))
                  {
                    int num3 = 0;
                    int num4 = 0;
                    for (int index3 = 0; index3 < recent.Length; ++index3)
                    {
                      if (recent[index3] == '/')
                        num3 = index3;
                      else if (index3 > 0 && recent[index3] == 'f' && recent[index3 - 1] == 'T')
                        num4 = index3;
                    }
                    string str6 = new string(recent).Substring(num3 + 1, num4 - 1 - num3);
                    string key = str6.Substring(0, str6.IndexOf(' '));
                    if (PdfTextExtractor.m_differenceArray.ContainsKey(key))
                    {
                      foreach (KeyValuePair<string, List<string>> difference in PdfTextExtractor.m_differenceArray)
                      {
                        if (difference.Key.Equals(key))
                        {
                          PdfTextExtractor.m_decodedChar = new List<string>();
                          PdfTextExtractor.m_decodedChar = difference.Value;
                          flag6 = true;
                        }
                      }
                    }
                    else
                      flag6 = false;
                  }
                }
                if (flag1)
                {
                  if (index2 == '[' && data[index1 + 1] == (byte) 40 && data[index1 - 1] == (byte) 10)
                    flag8 = true;
                  if (index2 == ']' && data[index1 - 1] == (byte) 41 && data[index1 + 1] == (byte) 32 /*0x20*/ && data[index1 + 2] == (byte) 84 && data[index1 + 2] == (byte) 74)
                    flag8 = false;
                  if (index2 == 'T' && data[index1 + 1] == (byte) 42 && (data[index1 + 2] == (byte) 91 || data[index1 + 2] == (byte) 40))
                    textFromBytes += Environment.NewLine;
                  char ch1;
                  if (data[index1] == (byte) 39 && data[index1 - 1] == (byte) 41)
                  {
                    if (textFromBytes.Length > 0 && textFromBytes[textFromBytes.Length - 1] != '\n')
                      textFromBytes += Environment.NewLine;
                  }
                  else if (data[index1] == (byte) 84 && data[index1 + 1] == (byte) 68 && data[index1 - 1] == (byte) 32 /*0x20*/)
                  {
                    string str7 = string.Empty;
                    string empty3 = string.Empty;
                    for (int index4 = recent.Length - 2; index4 >= 0; --index4)
                    {
                      char ch2 = recent[index4];
                      if (ch2 != ' ')
                      {
                        empty3 += ch2.ToString();
                      }
                      else
                      {
                        for (int index5 = empty3.Length - 1; index5 >= 0; --index5)
                        {
                          string str8 = str7;
                          ch1 = empty3[index5];
                          string str9 = ch1.ToString();
                          str7 = str8 + str9;
                        }
                        break;
                      }
                    }
                    if (str7 != string.Empty)
                    {
                      if (flag7)
                      {
                        num2 = Convert.ToSingle(str7);
                        flag7 = false;
                      }
                      else
                      {
                        float single = Convert.ToSingle(str7);
                        if ((double) num2 - (double) single != 0.0 && (double) single != 0.0)
                          textFromBytes += Environment.NewLine;
                        num2 = single;
                      }
                    }
                  }
                  else if (data[index1] == (byte) 84 && data[index1 + 1] == (byte) 100 && data[index1 - 1] == (byte) 32 /*0x20*/)
                  {
                    string str10 = string.Empty;
                    string empty4 = string.Empty;
                    for (int index6 = recent.Length - 2; index6 >= 0; --index6)
                    {
                      char ch3 = recent[index6];
                      if (ch3 != ' ')
                      {
                        empty4 += ch3.ToString();
                      }
                      else
                      {
                        for (int index7 = empty4.Length - 1; index7 >= 0; --index7)
                        {
                          string str11 = str10;
                          ch1 = empty4[index7];
                          string str12 = ch1.ToString();
                          str10 = str11 + str12;
                        }
                        break;
                      }
                    }
                    if (flag7)
                    {
                      num2 = Convert.ToSingle(str10);
                      flag7 = false;
                    }
                    else
                    {
                      float single = Convert.ToSingle(str10);
                      if ((double) num2 - (double) single != 0.0 && (double) single != 0.0)
                        textFromBytes += Environment.NewLine;
                      num2 = single;
                    }
                  }
                  else if (PdfTextExtractor.CheckToken(new string[3]
                  {
                    "'",
                    "T*",
                    "\""
                  }, recent))
                    textFromBytes += Environment.NewLine;
                  else if (data[index1] == (byte) 84 && data[index1 + 1] == (byte) 109 && data[index1 - 1] == (byte) 32 /*0x20*/ && (data[index1 + 2] == (byte) 10 || data[index1 + 2] == (byte) 13))
                  {
                    string str13 = string.Empty;
                    for (int index8 = index1 - 2; data[index8] != (byte) 32 /*0x20*/; --index8)
                    {
                      ch1 = (char) data[index8];
                      str13 = ch1.ToString() + str13;
                    }
                    string str14 = str13;
                    if (str4 == "")
                      str4 = "0";
                    if (empty2 != string.Empty && ((double) Convert.ToSingle(str14) < (double) Convert.ToSingle(str4) || str4 == "0"))
                      textFromBytes += Environment.NewLine;
                    if (empty2 != str5)
                      textFromBytes += " ";
                    str5 = empty2;
                    str4 = str14;
                  }
                  if (num1 == 0)
                  {
                    if (data[index1] == (byte) 84 && data[index1 + 1] == (byte) 109 && data[index1 + 2] == (byte) 10 && textFromBytes.Length > 0 && textFromBytes[textFromBytes.Length - 1] == ' ')
                      textFromBytes = textFromBytes.Remove(textFromBytes.Length - 1, 1);
                    if (!PdfTextExtractor.CheckToken(new string[2]
                    {
                      "TD",
                      "Td"
                    }, recent))
                    {
                      if (data[index1 - 1] == (byte) 10)
                      {
                        if (!textFromBytes.EndsWith(Environment.NewLine))
                        {
                          if (str1 == null)
                            str1 = " ";
                          string str15 = textFromBytes;
                          if (str1 == " ")
                            str1 = str15;
                          if (str1.Length != str15.Length)
                          {
                            try
                            {
                              string str16 = str15.Substring(str1.Length, str15.Length - str1.Length).Trim('\r').Trim('\n').Trim(' ');
                              if (str16 == str2)
                              {
                                if (str16.Length > 0)
                                  textFromBytes = textFromBytes.Substring(0, textFromBytes.Length - str16.Length - 1);
                                if (textFromBytes.Length > 0 && textFromBytes[textFromBytes.Length - 1] != '\n')
                                  textFromBytes += Environment.NewLine;
                                str15 = textFromBytes;
                              }
                              str2 = str16;
                            }
                            catch (Exception ex)
                            {
                            }
                          }
                          str1 = str15;
                        }
                      }
                      else if (PdfTextExtractor.CheckToken(new string[3]
                      {
                        "'",
                        "T*",
                        "\""
                      }, recent))
                        textFromBytes += Environment.NewLine;
                      else if (PdfTextExtractor.CheckToken(new string[1]
                      {
                        "Tj"
                      }, recent))
                        textFromBytes += string.Empty;
                    }
                  }
                  if (num1 == 0)
                  {
                    if (PdfTextExtractor.CheckToken(new string[1]
                    {
                      "ET"
                    }, recent))
                    {
                      flag1 = false;
                      if (!flag8)
                      {
                        textFromBytes += " ";
                        goto label_167;
                      }
                      goto label_167;
                    }
                  }
                  if (index2 == '<' && num1 == 0 && !flag3)
                  {
                    flag4 = false;
                    bool flag9 = true;
                    if (!char.IsDigit((char) data[index1 + 1]))
                      flag9 = false;
                    for (int index9 = index1; index9 < data.Length - 1; ++index9)
                    {
                      if (data[index9] == (byte) 62 & flag9)
                      {
                        byte[] numArray = new byte[index9 - index1 - 1];
                        int index10 = 0;
                        for (int index11 = index1 + 1; index11 < index9; ++index11)
                        {
                          numArray[index10] = data[index11];
                          ++index10;
                        }
                        UTF8Encoding utF8Encoding = new UTF8Encoding();
                        int index12 = 0;
                        byte[] bytes = new byte[4];
                        for (int index13 = 0; index13 < index9 - index1 - 1; ++index13)
                        {
                          bytes[index12] = numArray[index13];
                          if (index12 == 3)
                          {
                            long num5 = long.Parse(utF8Encoding.GetString(bytes), NumberStyles.HexNumber);
                            if (num5 < 5000L)
                            {
                              char ch4 = (char) num5;
                              textFromBytes += ch4.ToString();
                              index12 = 0;
                            }
                          }
                          else
                            ++index12;
                        }
                        char[] destinationArray = new char[3];
                        Array.Copy((Array) data, index9 + 1, (Array) destinationArray, 0, 3);
                        string str17 = new string(destinationArray);
                        if (str17.IndexOf("Tj") != -1 || str17.IndexOf("TJ") != -1)
                        {
                          flag4 = true;
                          num1 = 1;
                          break;
                        }
                        break;
                      }
                    }
                  }
                  else if (index2 == '(' && num1 == 0 && !flag3)
                  {
                    num1 = 1;
                    for (int index14 = index1; data[index14] != (byte) 41; ++index14)
                    {
                      if ((data[index1 + 1] < (byte) 32 /*0x20*/ || data[index1 + 1] > (byte) 126) && (data[index1 + 1] < (byte) 128 /*0x80*/ || data[index1 + 1] >= byte.MaxValue))
                        flag2 = true;
                    }
                  }
                  else if (index2 == ')' && num1 == 1 && !flag3)
                  {
                    num1 = 0;
                    if (flag2 = true)
                      flag2 = false;
                    if (!flag8)
                      textFromBytes += string.Empty;
                  }
                  else if (index2 == '>' && num1 == 1 && !flag3 & flag4)
                  {
                    num1 = 0;
                    flag4 = false;
                  }
                  else if (num1 == 1)
                  {
                    if (index2 == '\\' && !flag3)
                    {
                      flag3 = true;
                    }
                    else
                    {
                      if (index2 >= ' ' && index2 <= '~' || index2 >= '\u0080' && index2 < 'ÿ')
                      {
                        if (flag4)
                        {
                          if (flag5)
                          {
                            char ch5 = Convert.ToChar(Convert.ToUInt64((str3 + index2.ToString()).ToString(), 16 /*0x10*/));
                            if (ch5 != char.MinValue)
                              textFromBytes += ch5.ToString();
                            flag5 = false;
                            str3 = string.Empty;
                          }
                          else
                          {
                            str3 += index2.ToString();
                            flag5 = true;
                          }
                        }
                        else if (flag6)
                        {
                          if ((int) index2 > PdfTextExtractor.m_decodedChar.Count)
                          {
                            if (index2 == 'n' && data[index1 - 1] == (byte) 92)
                            {
                              int index15 = 10;
                              textFromBytes += PdfTextExtractor.m_decodedChar[index15];
                            }
                            else if (index2 == 'r' && data[index1 - 1] == (byte) 92)
                            {
                              int index16 = 13;
                              textFromBytes += PdfTextExtractor.m_decodedChar[index16];
                            }
                            else
                              textFromBytes += index2.ToString();
                          }
                          else
                            textFromBytes += PdfTextExtractor.m_decodedChar[(int) index2];
                        }
                        else if (flag2)
                        {
                          string str18 = textFromBytes;
                          ch1 = (char) ((uint) index2 + 29U);
                          string str19 = ch1.ToString();
                          textFromBytes = str18 + str19;
                        }
                        else
                          textFromBytes += index2.ToString();
                      }
                      else if (flag6)
                      {
                        if ((int) index2 > PdfTextExtractor.m_decodedChar.Count)
                        {
                          if (index2 == 'n' && data[index1 - 1] == (byte) 92)
                          {
                            int index17 = 10;
                            textFromBytes += PdfTextExtractor.m_decodedChar[index17];
                          }
                          else if (index2 == 'r' && data[index1 - 1] == (byte) 92)
                          {
                            int index18 = 13;
                            textFromBytes += PdfTextExtractor.m_decodedChar[index18];
                          }
                          else
                            textFromBytes += index2.ToString();
                        }
                        else
                          textFromBytes += PdfTextExtractor.m_decodedChar[(int) index2];
                      }
                      else if (flag2)
                      {
                        string str20 = textFromBytes;
                        ch1 = (char) ((uint) index2 + 29U);
                        string str21 = ch1.ToString();
                        textFromBytes = str20 + str21;
                      }
                      else
                        textFromBytes += index2.ToString();
                      flag3 = false;
                    }
                  }
                }
    label_167:
                for (int index19 = 0; index19 < PdfTextExtractor.m_numberOfChars - 1; ++index19)
                  recent[index19] = recent[index19 + 1];
                recent[PdfTextExtractor.m_numberOfChars - 1] = index2;
                if (!flag1)
                {
                  if (PdfTextExtractor.CheckToken(new string[1]
                  {
                    "BT"
                  }, recent))
                    flag1 = true;
                }
              }
              return textFromBytes;
            }
            catch
            {
              return " ";
            }
          }
        }
        return " ";
      }

      internal static string ExtractTextFromBytes(byte[] data, bool type)
      {
        if (data != null)
        {
          if (data.Length != 0)
          {
            try
            {
              string textFromBytes = string.Empty;
              bool flag1 = false;
              string str1 = (string) null;
              string str2 = (string) null;
              bool flag2 = false;
              int num1 = 0;
              char[] recent = new char[PdfTextExtractor.m_numberOfChars];
              for (int index = 0; index < PdfTextExtractor.m_numberOfChars; ++index)
                recent[index] = ' ';
              bool flag3 = false;
              string empty1 = string.Empty;
              float num2 = 0.0f;
              bool flag4 = true;
              for (int index1 = 0; index1 < data.Length; ++index1)
              {
                char ch1 = (char) data[index1];
                if (flag1)
                {
                  if (data[index1] == (byte) 39 && data[index1 - 1] == (byte) 41)
                  {
                    if (textFromBytes.Length > 0 && textFromBytes[textFromBytes.Length - 1] != '\n')
                      textFromBytes += Environment.NewLine;
                  }
                  else if (data[index1] == (byte) 84 && data[index1 + 1] == (byte) 68 && data[index1 - 1] == (byte) 32 /*0x20*/)
                  {
                    string empty2 = string.Empty;
                    string empty3 = string.Empty;
                    for (int index2 = recent.Length - 2; index2 >= 0; --index2)
                    {
                      char ch2 = recent[index2];
                      if (ch2 != ' ')
                      {
                        empty3 += ch2.ToString();
                      }
                      else
                      {
                        for (int index3 = empty3.Length - 1; index3 >= 0; --index3)
                          empty2 += empty3[index3].ToString();
                        break;
                      }
                    }
                    if (flag4)
                    {
                      num2 = Convert.ToSingle(empty2);
                      flag4 = false;
                    }
                    else
                    {
                      float single = Convert.ToSingle(empty2);
                      if ((double) num2 - (double) single != 0.0 && (double) single != 0.0)
                        textFromBytes += Environment.NewLine;
                      num2 = single;
                    }
                  }
                  else if (data[index1] == (byte) 84 && data[index1 + 1] == (byte) 100 && data[index1 - 1] == (byte) 32 /*0x20*/)
                  {
                    string empty4 = string.Empty;
                    string empty5 = string.Empty;
                    for (int index4 = recent.Length - 2; index4 >= 0; --index4)
                    {
                      char ch3 = recent[index4];
                      if (ch3 != ' ')
                      {
                        empty5 += ch3.ToString();
                      }
                      else
                      {
                        for (int index5 = empty5.Length - 1; index5 >= 0; --index5)
                          empty4 += empty5[index5].ToString();
                        break;
                      }
                    }
                    if (flag4)
                    {
                      num2 = Convert.ToSingle(empty4);
                      flag4 = false;
                    }
                    else
                    {
                      float single = Convert.ToSingle(empty4);
                      if ((double) num2 - (double) single != 0.0 && (double) single != 0.0)
                        textFromBytes += Environment.NewLine;
                      num2 = single;
                    }
                  }
                  else if (PdfTextExtractor.CheckToken(new string[3]
                  {
                    "'",
                    "T*",
                    "\""
                  }, recent))
                    textFromBytes += Environment.NewLine;
                  else if (data[index1] == (byte) 84 && data[index1 + 1] == (byte) 109 && data[index1 - 1] == (byte) 32 /*0x20*/ && data[index1 + 2] == (byte) 10)
                    textFromBytes += Environment.NewLine;
                  if (num1 == 0)
                  {
                    if (data[index1] == (byte) 84 && data[index1 + 1] == (byte) 109 && data[index1 + 2] == (byte) 10 && textFromBytes.Length > 0 && textFromBytes[textFromBytes.Length - 1] == ' ')
                      textFromBytes = textFromBytes.Remove(textFromBytes.Length - 1, 1);
                    if (!PdfTextExtractor.CheckToken(new string[2]
                    {
                      "TD",
                      "Td"
                    }, recent))
                    {
                      if (data[index1 - 1] == (byte) 10)
                      {
                        if (!textFromBytes.EndsWith(Environment.NewLine))
                        {
                          if (str1 == null)
                            str1 = " ";
                          string str3 = textFromBytes;
                          if (str1 == " ")
                            str1 = str3;
                          if (str1.Length != str3.Length)
                          {
                            try
                            {
                              string str4 = str3.Substring(str1.Length, str3.Length - str1.Length).Trim('\r').Trim('\n').Trim(' ');
                              if (str4 == str2)
                              {
                                if (str4.Length > 0)
                                  textFromBytes = textFromBytes.Substring(0, textFromBytes.Length - str4.Length - 1);
                                if (textFromBytes.Length > 0 && textFromBytes[textFromBytes.Length - 1] != '\n')
                                  textFromBytes += Environment.NewLine;
                                str3 = textFromBytes;
                              }
                              str2 = str4;
                            }
                            catch (Exception ex)
                            {
                              throw ex;
                            }
                          }
                          str1 = str3;
                        }
                      }
                      else if (PdfTextExtractor.CheckToken(new string[3]
                      {
                        "'",
                        "T*",
                        "\""
                      }, recent))
                        textFromBytes += Environment.NewLine;
                      else if (PdfTextExtractor.CheckToken(new string[1]
                      {
                        "Tj"
                      }, recent))
                        textFromBytes += string.Empty;
                    }
                  }
                  if (num1 == 0)
                  {
                    if (PdfTextExtractor.CheckToken(new string[1]
                    {
                      "ET"
                    }, recent))
                    {
                      flag1 = false;
                      textFromBytes += " ";
                      goto label_80;
                    }
                  }
                  if (ch1 == '<' && num1 == 0 && !flag2)
                    num1 = 1;
                  else if (ch1 == '>' && num1 == 1 && !flag2)
                  {
                    num1 = 0;
                    textFromBytes += " ";
                  }
                  else if (num1 == 1)
                  {
                    if (ch1 == '\\' && !flag2)
                    {
                      flag2 = true;
                    }
                    else
                    {
                      if (ch1 >= ' ' && ch1 <= '~' || ch1 >= '\u0080' && ch1 < 'ÿ')
                      {
                        if (flag3)
                        {
                          string str5 = empty1 + ch1.ToString();
                          textFromBytes += Convert.ToChar(Convert.ToUInt64(str5.ToString(), 16 /*0x10*/)).ToString();
                          flag3 = false;
                          empty1 = string.Empty;
                        }
                        else
                        {
                          empty1 += ch1.ToString();
                          flag3 = true;
                        }
                      }
                      flag2 = false;
                    }
                  }
                }
    label_80:
                for (int index6 = 0; index6 < PdfTextExtractor.m_numberOfChars - 1; ++index6)
                  recent[index6] = recent[index6 + 1];
                recent[PdfTextExtractor.m_numberOfChars - 1] = ch1;
                if (!flag1)
                {
                  if (PdfTextExtractor.CheckToken(new string[1]
                  {
                    "BT"
                  }, recent))
                    flag1 = true;
                }
              }
              return textFromBytes;
            }
            catch
            {
              return " ";
            }
          }
        }
        return " ";
      }

      public static string ExtractTextFromBytes(
        byte[] data,
        PdfPageBase lpage,
        List<PdfName> fontname,
        List<IPdfPrimitive> fontref)
      {
        if (fontname == null)
          return (string) null;
        PdfCrossTable crosstable = new PdfCrossTable();
        if (lpage is PdfLoadedPage)
          crosstable = (lpage as PdfLoadedPage).Document.CrossTable;
        else if (lpage != null)
          crosstable = (lpage as PdfPage).Document.CrossTable;
        PdfTextExtractor.m_differenceArray = new Dictionary<string, List<string>>();
        for (int index1 = 0; index1 < fontname.Count; ++index1)
        {
          if ((object) (fontref[index1] as PdfReferenceHolder) != null)
          {
            PdfReferenceHolder pointer1 = fontref[index1] as PdfReferenceHolder;
            PdfDictionary pdfDictionary1 = crosstable.GetObject((IPdfPrimitive) pointer1) as PdfDictionary;
            List<string> stringList = new List<string>();
            if (pdfDictionary1["Subtype"].ToString() != "/Type3" && pdfDictionary1.ContainsKey("Encoding"))
            {
              PdfReferenceHolder pointer2 = pdfDictionary1["Encoding"] as PdfReferenceHolder;
              if (crosstable.GetObject((IPdfPrimitive) pointer2) is PdfDictionary pdfDictionary2 && pdfDictionary2.ContainsKey("Differences"))
              {
                Dictionary<PdfName, IPdfPrimitive> items = pdfDictionary2.Items;
                foreach (KeyValuePair<PdfName, IPdfPrimitive> keyValuePair in pdfDictionary2.Items)
                {
                  if (keyValuePair.Key.Value.Equals("Differences"))
                  {
                    PdfArray pdfArray = crosstable.GetObject(keyValuePair.Value) as PdfArray;
                    int count = pdfArray.Count;
                    for (int index2 = 0; index2 < count; ++index2)
                    {
                      string latinCharacter;
                      if (pdfArray[index2] is PdfNumber)
                        latinCharacter = (pdfArray[index2] as PdfNumber).IntValue.ToString();
                      else
                        latinCharacter = PdfTextExtractor.GetLatinCharacter(pdfArray[index2].ToString().Trim('/'));
                      stringList.Add(latinCharacter);
                    }
                  }
                }
                PdfTextExtractor.m_differenceArray.Add(fontname[index1].ToString().Trim('/'), stringList);
              }
            }
          }
        }
        for (int index = 0; index < fontname.Count; ++index)
        {
          if ((object) (fontref[index] as PdfReferenceHolder) != null)
          {
            PdfReferenceHolder pointer = fontref[index] as PdfReferenceHolder;
            PdfDictionary pdfDictionary = crosstable.GetObject((IPdfPrimitive) pointer) as PdfDictionary;
            string empty = string.Empty;
            if (pdfDictionary.ContainsKey("BaseFont"))
              empty = pdfDictionary["BaseFont"].ToString();
            else if (pdfDictionary.ContainsKey("Name"))
              empty = pdfDictionary["Name"].ToString();
            if (pdfDictionary["Subtype"].ToString() == "/Type0")
              return pdfDictionary.ContainsKey("ToUnicode") && !(pdfDictionary["ToUnicode"].ToString() == "/Identity-H") ? PdfTextExtractor.ExtractTextFromBytesEmbedFonts(data, lpage, fontname, fontref) : PdfTextExtractor.ExtractTextFrom_Type0Fonts(data, fontref, fontname, crosstable);
            if (!pdfDictionary.ContainsKey("ToUnicode") || empty.Equals("/Times−Roman") || empty.Equals("/Times-Bold") || empty.Equals("/Times-Italic") || empty.Equals("/Times−BoldItalic") || empty.Equals("/Helvetica") || empty.Equals("/Helvetica−Bold") || empty.Equals("/Helvetica−Oblique") || empty.Equals("/Helvetica−BoldOblique") || empty.Equals("/Courier") || empty.Equals("/Courier−Bold") || empty.Equals("/Courier−Oblique") || empty.Equals("/Courier−BoldOblique") || empty.Equals("/Symbol") || empty.Equals("/ZapfDingbats"))
              return PdfTextExtractor.ExtractTextFromBytes(data);
            if (pdfDictionary.ContainsKey("Encoding"))
            {
              if (pdfDictionary["Encoding"] as PdfReferenceHolder != (PdfReferenceHolder) null)
                return PdfTextExtractor.ExtractTextFromBytesEmbedFonts(data, lpage, fontname, fontref);
              return pdfDictionary["Encoding"].ToString() == "/WinAnsiEncoding" ? PdfTextExtractor.ExtractTextFromBytes(data) : PdfTextExtractor.ExtractTextFromBytesTrueTypeFonts(data, fontref, fontname, crosstable);
            }
            if (pdfDictionary.ContainsKey("ToUnicode"))
              return PdfTextExtractor.ExtractTextFromBytesEmbedFonts(data, lpage, fontname, fontref);
          }
        }
        return (string) null;
      }

      internal static string ExtractTextFromBytesEmbedFonts(
        byte[] data,
        PdfPageBase lpage,
        List<PdfName> m_font,
        List<IPdfPrimitive> m_fref)
      {
        if (m_font == null)
          return " ";
        bool flag1 = false;
        List<PdfArray> pdfArrayList = new List<PdfArray>();
        List<List<string>> stringListList = new List<List<string>>();
        List<Dictionary<double, double>> dictionaryList = new List<Dictionary<double, double>>();
        PdfCrossTable pdfCrossTable = (PdfCrossTable) null;
        if (lpage is PdfLoadedPage)
          pdfCrossTable = (lpage as PdfLoadedPage).Document.CrossTable;
        else if (lpage != null)
          pdfCrossTable = (lpage as PdfPage).Document.CrossTable;
        for (int index1 = 0; index1 < m_font.Count; ++index1)
        {
          PdfReferenceHolder pointer1 = m_fref[index1] as PdfReferenceHolder;
          PdfDictionary pdfDictionary = pdfCrossTable.GetObject((IPdfPrimitive) pointer1) as PdfDictionary;
          PdfReferenceHolder pointer2 = pdfDictionary["Encoding"] as PdfReferenceHolder;
          if (pointer2 != (PdfReferenceHolder) null)
          {
            PdfArray pdfArray = (pdfCrossTable.GetObject((IPdfPrimitive) pointer2) as PdfDictionary)["Differences"] as PdfArray;
            pdfArrayList.Add(pdfArray);
          }
          PdfReferenceHolder pointer3 = pdfDictionary["ToUnicode"] as PdfReferenceHolder;
          if (pointer3 != (PdfReferenceHolder) null)
          {
            PdfStream pdfStream = pdfCrossTable.GetObject((IPdfPrimitive) pointer3) as PdfStream;
            pdfStream.Decompress();
            byte[] data1 = pdfStream.Data;
            string str1 = Encoding.UTF8.GetString(data1, 0, data1.Length);
            int num1 = str1.IndexOf("beginbfchar");
            int num2 = str1.IndexOf("endbfchar");
            if (num1 < 0 && num2 < 0)
            {
              num1 = str1.IndexOf("begincmap");
              num2 = str1.IndexOf("endcmap");
            }
            int num3 = str1.IndexOf("beginbfrange");
            int num4 = str1.IndexOf("endbfrange");
            if (num3 < 0 && num4 < 0)
            {
              num3 = str1.IndexOf("begincidrange");
              num4 = str1.IndexOf("endcidrange");
            }
            if (num3 > 0)
            {
              num1 = num3;
              num2 = num4;
            }
            string str2 = str1.Substring(num1 + 11, num2 - num1 - 11);
            List<string> stringList = new List<string>();
            string str3 = str2;
            int num5 = 0;
            Dictionary<double, double> dictionary = new Dictionary<double, double>();
            int num6 = 0;
            while (num5 >= 0)
            {
              num5 = str3.IndexOf('<');
              int num7 = str3.IndexOf('>');
              if (num5 >= 0 && num7 >= 0)
              {
                string str4 = str3.Substring(num5 + 1, num7 - 1 - num5);
                stringList.Add(str4);
                str3 = str3.Substring(num7 + 1, str3.Length - 1 - num7);
              }
              ++num6;
            }
            bool flag2 = false;
            for (int index2 = 0; index2 < stringList.Count; index2 += 3)
            {
              if (index2 + 2 < stringList.Count && stringList[index2] != stringList[index2 + 1] && long.Parse(stringList[index2], NumberStyles.HexNumber) == long.Parse(stringList[index2 + 2], NumberStyles.HexNumber))
              {
                flag2 = true;
                break;
              }
            }
            int index3 = 0;
            while (index3 < stringList.Count)
            {
              if (!flag2)
              {
                if (stringList[index3] != stringList[index3 + 1])
                {
                  dictionary.Add((double) long.Parse(stringList[index3], NumberStyles.HexNumber), (double) long.Parse(stringList[index3 + 1], NumberStyles.HexNumber));
                  index3 += 2;
                }
                else if (index3 + 2 < stringList.Count)
                {
                  dictionary.Add((double) long.Parse(stringList[index3], NumberStyles.HexNumber), (double) long.Parse(stringList[index3 + 2], NumberStyles.HexNumber));
                  index3 += 3;
                }
                else
                {
                  dictionary.Add((double) long.Parse(stringList[index3], NumberStyles.HexNumber), (double) long.Parse(stringList[index3 + 1], NumberStyles.HexNumber));
                  index3 += 2;
                }
              }
              else if (index3 + 2 < stringList.Count)
              {
                dictionary.Add((double) long.Parse(stringList[index3], NumberStyles.HexNumber), (double) long.Parse(stringList[index3 + 2], NumberStyles.HexNumber));
                index3 += 3;
              }
            }
            dictionaryList.Add(dictionary);
            stringListList.Add(stringList);
          }
        }
        if (data == null || data.Length == 0)
          return " ";
        string empty1 = string.Empty;
        try
        {
          bool flag3 = false;
          bool flag4 = false;
          int num8 = 0;
          char[] recent = new char[PdfTextExtractor.m_numberOfChars];
          for (int index = 0; index < PdfTextExtractor.m_numberOfChars; ++index)
            recent[index] = ' ';
          bool flag5 = false;
          string str5 = (string) null;
          string empty2 = string.Empty;
          string empty3 = string.Empty;
          int index4 = 0;
          List<byte> byteList = new List<byte>();
          List<string> stringList1 = new List<string>();
          List<string> stringList2 = new List<string>();
          bool flag6 = false;
          for (int index5 = 0; index5 < data.Length; ++index5)
          {
            char int32_1 = (char) data[index5];
            if (PdfTextExtractor.CheckToken(new string[1]
            {
              "Tf"
            }, recent))
            {
              int num9 = 0;
              int num10 = 0;
              for (int index6 = 0; index6 < recent.Length; ++index6)
              {
                if (recent[index6] == '/')
                  num9 = index6;
                else if (recent[index6] == 'T' && recent[index6 + 1] == 'f')
                  num10 = index6;
              }
              string str6 = new string(recent).Substring(num9 + 1, num10 - 1 - num9);
              str5 = str6.Substring(0, str6.IndexOf(' '));
              stringList1.Add(str5);
            }
            if (PdfTextExtractor.CheckToken(new string[5]
            {
              "'",
              "T*",
              "Tj",
              "Td",
              "\n"
            }, recent))
            {
              if (byteList.Count > 0 && byteList.Contains((byte) 41))
              {
                string str7 = string.Empty;
                byteList.RemoveRange(byteList.LastIndexOf((byte) 41), byteList.Count - byteList.LastIndexOf((byte) 41));
                string str8 = Encoding.BigEndianUnicode.GetString(byteList.ToArray());
                string str9 = Encoding.ASCII.GetString(byteList.ToArray());
                int num11 = 0;
                foreach (char key in str8)
                {
                  if (dictionaryList.Count > index4 && dictionaryList[index4].ContainsKey((double) key))
                  {
                    ++num11;
                    string str10 = ((char) dictionaryList[index4][(double) key]).ToString();
                    str7 += str10;
                  }
                }
                byteList.Clear();
                if (num11 != str8.Length)
                {
                  str7 = "";
                  foreach (char key in str9)
                  {
                    if (dictionaryList.Count > index4 && dictionaryList[index4].ContainsKey((double) key))
                    {
                      string str11 = ((char) dictionaryList[index4][(double) key]).ToString();
                      str7 += str11;
                    }
                  }
                }
                empty1 += str7;
              }
              else
                byteList.Clear();
              num8 = 0;
              flag1 = false;
            }
            if (flag3)
            {
              if (num8 == 0)
              {
                if (PdfTextExtractor.CheckToken(new string[2]
                {
                  "TD",
                  "Td"
                }, recent))
                {
                  if (flag5)
                    empty1 += string.Empty;
                }
                else if (PdfTextExtractor.CheckToken(new string[1]
                {
                  "Tf"
                }, recent))
                {
                  int num12 = 0;
                  int num13 = 0;
                  for (int index7 = 0; index7 < recent.Length; ++index7)
                  {
                    if (recent[index7] == '/')
                      num12 = index7;
                    else if (recent[index7] == 'T' && recent[index7 + 1] == 'f')
                      num13 = index7;
                  }
                  string str12 = new string(recent).Substring(num12 + 1, num13 - 1 - num12);
                  str5 = str12.Substring(0, str12.IndexOf(' '));
                  stringList1.Add(str5);
                }
                else
                {
                  int num14 = (int) data[index5 - 1];
                }
              }
              if (num8 == 0)
              {
                if (PdfTextExtractor.CheckToken(new string[1]
                {
                  "ET"
                }, recent))
                {
                  flag3 = false;
                  empty1 += " ";
                  goto label_210;
                }
              }
              if (int32_1 == '<' && num8 == 0 && !flag4)
              {
                if (!flag5)
                {
                  num8 = 1;
                  for (int index8 = index5; index8 < data.Length - 1; ++index8)
                  {
                    if (data[index8] == (byte) 62)
                    {
                      byte[] numArray = new byte[index8 - index5 - 1];
                      int index9 = 0;
                      for (int index10 = index5 + 1; index10 < index8; ++index10)
                      {
                        numArray[index9] = data[index10];
                        ++index9;
                      }
                      UTF8Encoding utF8Encoding = new UTF8Encoding();
                      int index11 = 0;
                      int length = index8 - index5 - 1;
                      byte[] bytes = new byte[length];
                      if (length > 3)
                      {
                        for (int index12 = 0; index12 < length; ++index12)
                        {
                          bytes[index11] = numArray[index12];
                          string s = utF8Encoding.GetString(bytes);
                          int index13 = 0;
                          if (flag6 && index11 == length - 1)
                          {
                            char[] charArray = s.ToCharArray();
                            for (int index14 = 0; index14 < length / 2; ++index14)
                            {
                              string str13 = (string) null;
                              for (int index15 = 0; index15 < 2; ++index15)
                              {
                                str13 += charArray[index13].ToString();
                                ++index13;
                              }
                              int index16 = stringListList[0].IndexOf(str13) + 1;
                              char ch = (char) long.Parse(stringListList[0][index16], NumberStyles.HexNumber);
                              empty1 += ch.ToString();
                            }
                            index11 = 0;
                          }
                          else if (index11 == 3 && !flag6)
                          {
                            char ch = (char) long.Parse(s, NumberStyles.HexNumber);
                            empty1 += ch.ToString();
                            index11 = 0;
                          }
                          else
                            ++index11;
                        }
                        break;
                      }
                      flag6 = true;
                      for (int index17 = 0; index17 < 2; ++index17)
                      {
                        bytes[index11] = numArray[index17];
                        if (index11 == 1)
                        {
                          char[] charArray = utF8Encoding.GetString(bytes).ToCharArray();
                          string str14 = (string) null + charArray[0].ToString() + charArray[1].ToString();
                          int index18 = stringListList[0].IndexOf(str14) + 1;
                          char ch = (char) long.Parse(stringListList[0][index18], NumberStyles.HexNumber);
                          empty1 += ch.ToString();
                          index11 = 0;
                        }
                        else
                          ++index11;
                      }
                      break;
                    }
                  }
                }
              }
              else
              {
                if (int32_1 == '(' && num8 == 0 && !flag4)
                {
                  num8 = 1;
                  flag5 = true;
                }
                else if (PdfTextExtractor.CheckToken(new string[3]
                {
                  "'",
                  "T*",
                  "\""
                }, recent))
                {
                  empty1 += Environment.NewLine;
                  flag1 = false;
                }
                else if (PdfTextExtractor.CheckToken(new string[1]
                {
                  "T*"
                }, recent))
                {
                  empty1 += Environment.NewLine;
                  flag1 = false;
                }
                else if (PdfTextExtractor.CheckToken(new string[1]
                {
                  "Tj"
                }, recent))
                {
                  num8 = 0;
                  flag1 = false;
                }
                if (int32_1 == ')' && num8 == 1 && !flag4 && byteList.Count <= 0)
                {
                  num8 = 0;
                  flag1 = false;
                }
                if (int32_1 == '>' && num8 == 1 && !flag4)
                {
                  if (!flag5)
                  {
                    num8 = 0;
                    empty2 = string.Empty;
                  }
                }
                else if (num8 == 1)
                {
                  ulong uint64;
                  if (int32_1 != '\\' || flag4)
                  {
                    if (int32_1 >= ' ' && int32_1 <= '~' || int32_1 >= '\u0080' && int32_1 < 'ÿ')
                    {
                      string str15 = "/" + str5;
                      if (stringList2.Count >= 1)
                      {
                        if (stringList2[stringList2.Count - 1].ToString() != str15)
                          stringList2.Add(str15);
                      }
                      else
                        stringList2.Add(str15);
                      for (int index19 = 0; index19 < m_font.Count; ++index19)
                      {
                        string str16 = m_font[index19].ToString();
                        if (str15.Equals(str16))
                        {
                          index4 = index19;
                          break;
                        }
                      }
                      string str17 = ((m_fref[index4] as PdfReferenceHolder).Object as PdfDictionary)["Subtype"].ToString();
                      PdfDictionary pdfDictionary = (m_fref[index4] as PdfReferenceHolder).Object as PdfDictionary;
                      if (pdfDictionary.ContainsKey("ToUnicode") && stringListList.Count > index4)
                      {
                        List<string> stringList3 = stringListList[index4];
                        char[] chArray1 = new char[stringList3.Count];
                        char[] chArray2 = new char[stringList3.Count];
                        if (!flag5)
                        {
                          int index20 = 0;
                          int num15 = 0;
                          for (; index20 < stringList3.Count; index20 += 2)
                          {
                            char[] chArray3 = chArray1;
                            int index21 = num15;
                            uint64 = Convert.ToUInt64(stringList3[index20].ToString(), 16 /*0x10*/);
                            int int32_2 = (int) (ushort) Convert.ToInt32(uint64.ToString());
                            chArray3[index21] = (char) int32_2;
                            ++num15;
                          }
                          int index22 = 1;
                          int num16 = 0;
                          for (; index22 < stringList3.Count; index22 += 2)
                          {
                            char[] chArray4 = chArray2;
                            int index23 = num16;
                            uint64 = Convert.ToUInt64(stringList3[index22].ToString(), 16 /*0x10*/);
                            int int32_3 = (int) (ushort) Convert.ToInt32(uint64.ToString());
                            chArray4[index23] = (char) int32_3;
                            ++num16;
                          }
                        }
                        if (empty2.Length == 0)
                        {
                          if (dictionaryList[index4].ContainsKey((double) int32_1))
                          {
                            string str18 = ((char) dictionaryList[index4][(double) int32_1]).ToString();
                            empty2 += str18;
                          }
                        }
                        else if (empty2.Length == 1 && !flag5)
                        {
                          uint64 = Convert.ToUInt64(empty2 + int32_1.ToString(), 16 /*0x10*/);
                          int32_1 = (char) Convert.ToInt32(uint64.ToString());
                          empty2 = string.Empty;
                          for (int index24 = 0; index24 < chArray1.Length; ++index24)
                          {
                            if ((int) int32_1 == (int) chArray1[index24])
                            {
                              int32_1 = chArray2[index24];
                              if (int32_1 != char.MinValue)
                              {
                                empty1 += int32_1.ToString();
                                break;
                              }
                              break;
                            }
                          }
                        }
                      }
                      if (flag5)
                      {
                        if (flag1)
                        {
                          if (str17 == "/Type0")
                            byteList.Add((byte) int32_1);
                          else if (pdfDictionary.ContainsKey("ToUnicode"))
                          {
                            if (dictionaryList.Count > index4)
                            {
                              if (dictionaryList[index4].ContainsKey((double) int32_1))
                              {
                                string str19 = ((char) dictionaryList[index4][(double) int32_1]).ToString();
                                empty1 += str19;
                              }
                              else
                                empty1 += int32_1.ToString();
                            }
                            else
                              empty1 += int32_1.ToString();
                          }
                          else
                            empty1 += int32_1.ToString();
                        }
                        else
                          flag1 = true;
                      }
                    }
                    else
                    {
                      string str20 = "/" + str5;
                      if (stringList2.Count >= 1)
                      {
                        if (stringList2[stringList2.Count - 1].ToString() != str20)
                          stringList2.Add(str20);
                      }
                      else
                        stringList2.Add(str20);
                      for (int index25 = 0; index25 < m_font.Count; ++index25)
                      {
                        string str21 = m_font[index25].ToString();
                        if (str20.Equals(str21))
                        {
                          index4 = index25;
                          break;
                        }
                      }
                      string str22 = ((m_fref[index4] as PdfReferenceHolder).Object as PdfDictionary)["Subtype"].ToString();
                      if (stringListList.Count > index4)
                      {
                        List<string> stringList4 = stringListList[index4];
                        char[] chArray5 = new char[stringList4.Count];
                        char[] chArray6 = new char[stringList4.Count];
                        int index26 = 0;
                        int num17 = 0;
                        for (; index26 < stringList4.Count; index26 += 2)
                        {
                          char[] chArray7 = chArray5;
                          int index27 = num17;
                          uint64 = Convert.ToUInt64(stringList4[index26].ToString(), 16 /*0x10*/);
                          int int32_4 = (int) (ushort) Convert.ToInt32(uint64.ToString());
                          chArray7[index27] = (char) int32_4;
                          ++num17;
                        }
                        int index28 = 0;
                        int index29 = 0;
                        for (; index28 < stringList4.Count; index28 += 2)
                        {
                          while (stringList4[index28].ToString().Length < 4)
                            ++index28;
                          uint64 = Convert.ToUInt64(stringList4[index28].ToString(), 16 /*0x10*/);
                          char int32_5 = (char) Convert.ToInt32(uint64.ToString());
                          if (index28 > 2 && stringList4[index28 - 1] != stringList4[index28 - 2])
                          {
                            chArray6[index29] = int32_5;
                            chArray6[index29 + 1] = (char) ((uint) int32_5 + 1U);
                            index29 += 2;
                          }
                          else
                          {
                            chArray6[index29] = int32_5;
                            ++index29;
                          }
                        }
                        if (empty2.Length == 0)
                          empty2 += int32_1.ToString();
                        else if (empty2.Length == 1 && !flag5)
                        {
                          uint64 = Convert.ToUInt64(empty2 + int32_1.ToString(), 16 /*0x10*/);
                          int32_1 = (char) Convert.ToInt32(uint64.ToString());
                          empty2 = string.Empty;
                          for (int index30 = 0; index30 < chArray5.Length; ++index30)
                          {
                            if ((int) int32_1 == (int) chArray5[index30])
                            {
                              int32_1 = chArray6[index30];
                              if (int32_1 != char.MinValue)
                              {
                                empty1 += int32_1.ToString();
                                break;
                              }
                              break;
                            }
                          }
                        }
                        if (flag5)
                        {
                          if (str22 == "/Type0")
                            byteList.Add((byte) int32_1);
                          else if ((int) int32_1 < chArray6.Length)
                            empty1 += chArray6[(int) int32_1 - 1].ToString();
                        }
                      }
                    }
                  }
                  flag4 = false;
                }
              }
            }
    label_210:
            for (int index31 = 0; index31 < PdfTextExtractor.m_numberOfChars - 1; ++index31)
              recent[index31] = recent[index31 + 1];
            recent[PdfTextExtractor.m_numberOfChars - 1] = int32_1;
            if (!flag3)
            {
              if (PdfTextExtractor.CheckToken(new string[1]
              {
                "BT"
              }, recent))
                flag3 = true;
            }
          }
          return empty1;
        }
        catch
        {
          return " ";
        }
      }

      internal static string ExtractTextFromBytesTrueTypeFonts(
        byte[] data,
        List<IPdfPrimitive> fontref,
        List<PdfName> fontname,
        PdfCrossTable crosstable)
      {
        string str1 = string.Empty;
        if (data != null)
        {
          if (data.Length != 0)
          {
            try
            {
              string empty = string.Empty;
              bool flag1 = false;
              bool flag2 = false;
              int num1 = 0;
              string str2 = "";
              Encoding.Default.GetString(data);
              char[] recent = new char[PdfTextExtractor.m_numberOfChars];
              for (int index = 0; index < PdfTextExtractor.m_numberOfChars; ++index)
                recent[index] = ' ';
              for (int index1 = 0; index1 < data.Length; ++index1)
              {
                char ch = (char) data[index1];
                if (PdfTextExtractor.CheckToken(new string[1]
                {
                  "Tf"
                }, recent))
                {
                  int num2 = 0;
                  int num3 = 0;
                  for (int index2 = 0; index2 < recent.Length; ++index2)
                  {
                    if (recent[index2] == '/')
                      num2 = index2;
                    else if (recent[index2] == 'f' && recent[index2 - 1] == 'T')
                    {
                      num3 = index2;
                      break;
                    }
                  }
                  string str3 = new string(recent).Substring(num2 + 1, num3 - 1 - num2);
                  string str4 = str3.Substring(0, str3.IndexOf(' '));
                  if (fontname.Contains((PdfName) str4))
                  {
                    PdfReferenceHolder pointer = fontref[fontname.IndexOf((PdfName) str4)] as PdfReferenceHolder;
                    PdfDictionary pdfDictionary = crosstable.GetObject((IPdfPrimitive) pointer) as PdfDictionary;
                    pdfDictionary["BaseFont"].ToString();
                    if (pdfDictionary["Subtype"].ToString() == "/Type0")
                      str1 = "Type0";
                    else if (pdfDictionary["Subtype"].ToString() == "/TrueType")
                      str1 = "TrueType";
                    else if (pdfDictionary["Subtype"].ToString() == "/Type1")
                      str1 = "Type1";
                  }
                }
                if (flag1)
                {
                  if (num1 == 0)
                  {
                    if (PdfTextExtractor.CheckToken(new string[2]
                    {
                      "TD",
                      "Td"
                    }, recent))
                    {
                      string str5 = new string(recent);
                      int startIndex = str5.IndexOf(' ');
                      int num4 = str5.LastIndexOf(' ');
                      string str6 = str5.Substring(startIndex, num4 - startIndex);
                      if (str6 != str2)
                        empty += Environment.NewLine;
                      str2 = str6;
                    }
                    else if (data[index1 - 1] != (byte) 10)
                    {
                      if (PdfTextExtractor.CheckToken(new string[3]
                      {
                        "'",
                        "T*",
                        "\""
                      }, recent))
                        empty += Environment.NewLine;
                      else if (PdfTextExtractor.CheckToken(new string[1]
                      {
                        "Tj"
                      }, recent))
                        empty += string.Empty;
                    }
                  }
                  if (num1 == 0)
                  {
                    if (PdfTextExtractor.CheckToken(new string[1]
                    {
                      "ET"
                    }, recent))
                    {
                      flag1 = false;
                      empty += " ";
                      goto label_50;
                    }
                  }
                  if (ch == '(' && num1 == 0 && !flag2)
                    num1 = 1;
                  else if (ch == ')' && num1 == 1 && !flag2)
                    num1 = 0;
                  else if (num1 == 1)
                  {
                    if (ch == '\\' && !flag2)
                    {
                      flag2 = true;
                    }
                    else
                    {
                      if (str1 == "Type1")
                        empty += ch.ToString();
                      else if (ch >= '\u0003' && ch <= 'a' || ch >= 'c' && ch < 'â')
                      {
                        ch += '\u001D';
                        empty += ch.ToString();
                      }
                      else if (recent[PdfTextExtractor.m_numberOfChars - 1] == char.MinValue)
                        empty += " ";
                      flag2 = false;
                    }
                  }
                }
    label_50:
                for (int index3 = 0; index3 < PdfTextExtractor.m_numberOfChars - 1; ++index3)
                  recent[index3] = recent[index3 + 1];
                recent[PdfTextExtractor.m_numberOfChars - 1] = ch;
                if (!flag1)
                {
                  if (PdfTextExtractor.CheckToken(new string[1]
                  {
                    "BT"
                  }, recent))
                    flag1 = true;
                }
              }
              return empty;
            }
            catch
            {
              return " ";
            }
          }
        }
        return " ";
      }

      internal static string GetLatinCharacter(string decodedCharacter)
      {
        switch (decodedCharacter)
        {
          case "Adieresis":
            return "Ä";
          case "Scaron":
            return "Š";
          case "Udieresis":
            return "Ü";
          case "adieresis":
            return "ä";
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
            return "˙";
          case "edieresis":
            return "ë";
          case "egrave":
            return "è";
          case "eight":
            return "8";
          case "ellipsis":
            return "...";
          case "emdash":
            return "——";
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
          case "fi":
            return "fl";
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
          case "g120":
            return "·";
          case "g383":
          case "g45":
            return "☺";
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
          case "park":
            return "\uE0E0";
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
    }
}
