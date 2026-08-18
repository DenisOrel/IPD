// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.StringTokenizer
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Text;
using System.Text.RegularExpressions;


namespace Syncfusion.Pdf.Graphics
{
    internal class StringTokenizer
    {
      private const RegexOptions c_regexOptions = RegexOptions.IgnoreCase | RegexOptions.Compiled;
      private const string c_whiteSpacePatterm = "^[ \\t]+$";
      private int m_position;
      private string m_text;
      private static Regex s_whiteSpaceRegex = new Regex("^[ \\t]+$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
      public static readonly char[] Spaces = new char[2]
      {
        ' ',
        '\t'
      };
      public const char Tab = '\t';
      public const char WhiteSpace = ' ';

      public StringTokenizer(string text)
      {
        this.m_text = text != null ? text : throw new ArgumentNullException(nameof (text));
      }

      public void Close() => this.m_text = (string) null;

      private static bool Contains(char[] array, char symbol)
      {
        if (array == null)
          throw new ArgumentNullException(nameof (array));
        for (int index = 0; index < array.Length; ++index)
        {
          if ((int) array[index] == (int) symbol)
            return true;
        }
        return false;
      }

      public static int GetCharsCount(string text, char[] symbols)
      {
        if (text == null)
          throw new ArgumentNullException(nameof (text));
        if (symbols == null)
          throw new ArgumentNullException(nameof (symbols));
        int charsCount = 0;
        int index = 0;
        for (int length = text.Length; index < length; ++index)
        {
          char symbol = text[index];
          if (StringTokenizer.Contains(symbols, symbol))
            ++charsCount;
        }
        return charsCount;
      }

      public static int GetCharsCount(string text, char symbol)
      {
        if (text == null)
          throw new ArgumentNullException("wholeText");
        int charsCount = 0;
        int startIndex = 0;
        do
        {
          int num = text.IndexOf(symbol, startIndex);
          if (num == -1)
            return charsCount;
          ++charsCount;
          startIndex = num + 1;
        }
        while (startIndex != text.Length);
        return charsCount;
      }

      internal static int GetWhitespaceCount(string line, bool start)
      {
        if (line == null)
          throw new ArgumentNullException(nameof (line));
        int whitespaceCount = 0;
        if (line.Length > 0)
        {
          for (int index = start ? 0 : line.Length - 1; index >= 0 && index < line.Length; index = start ? index + 1 : index - 1)
          {
            char token = line[index];
            if (!StringTokenizer.IsSpace(token) && !StringTokenizer.IsTab(token))
              return whitespaceCount;
            ++whitespaceCount;
          }
        }
        return whitespaceCount;
      }

      internal static bool IsSpace(char token) => token == ' ';

      internal static bool IsTab(char token) => token == '\t';

      internal static bool IsWhitespace(string token)
      {
        if (token == null)
          return false;
        try
        {
          return StringTokenizer.s_whiteSpaceRegex.Match(token).Success;
        }
        catch
        {
          return false;
        }
      }

      public char Peek()
      {
        char minValue = char.MinValue;
        if (!this.EOF)
          minValue = this.m_text[this.m_position];
        return minValue;
      }

      public string PeekLine()
      {
        int position = this.m_position;
        string str = this.ReadLine();
        this.m_position = position;
        return str;
      }

      public string PeekWord()
      {
        int position = this.m_position;
        string str = this.ReadWord();
        this.m_position = position;
        return str;
      }

      public char Read()
      {
        char minValue = char.MinValue;
        if (!this.EOF)
        {
          minValue = this.m_text[this.m_position];
          ++this.m_position;
        }
        return minValue;
      }

      public string Read(int count)
      {
        int num = 0;
        StringBuilder stringBuilder = new StringBuilder();
        for (; !this.EOF && num < count; ++num)
        {
          char ch = this.Read();
          stringBuilder.Append(ch);
        }
        return stringBuilder.ToString();
      }

      public string ReadLine()
      {
        int position;
        for (position = this.m_position; position < this.Length; ++position)
        {
          char ch = this.m_text[position];
          switch (ch)
          {
            case '\n':
            case '\r':
              string str = this.m_text.Substring(this.m_position, position - this.m_position);
              this.m_position = position + 1;
              if (ch != '\r' || this.m_position >= this.Length || this.m_text[this.m_position] != '\n')
                return str;
              ++this.m_position;
              return str;
            default:
              continue;
          }
        }
        if (position <= this.m_position)
          return (string) null;
        string str1 = this.m_text.Substring(this.m_position, position - this.m_position);
        this.m_position = position;
        return str1;
      }

      public string ReadToEnd()
      {
        string end = this.m_position != 0 ? this.m_text.Substring(this.m_position, this.Length - this.m_position) : this.m_text;
        this.m_position = this.Length;
        return end;
      }

      public string ReadToSymbol(char symbol, bool readSymbol)
      {
        StringBuilder stringBuilder = new StringBuilder();
        while (!this.EOF)
        {
          char ch = this.Peek();
          if ((int) ch == (int) symbol)
          {
            if (readSymbol)
            {
              int num = (int) this.Read();
              stringBuilder.Append(ch);
              break;
            }
            break;
          }
          stringBuilder.Append(ch);
          int num1 = (int) this.Read();
        }
        return stringBuilder.ToString();
      }

      public string ReadWord()
      {
        int position;
        for (position = this.m_position; position < this.Length; ++position)
        {
          char ch = this.m_text[position];
          switch (ch)
          {
            case '\t':
            case ' ':
              if (position == this.m_position)
                ++position;
              string str1 = this.m_text.Substring(this.m_position, position - this.m_position);
              this.m_position = position;
              return str1;
            case '\n':
            case '\r':
              string str2 = this.m_text.Substring(this.m_position, position - this.m_position);
              this.m_position = position + 1;
              if (ch != '\r' || this.m_position >= this.Length || this.m_text[this.m_position] != '\n')
                return str2;
              ++this.m_position;
              return str2;
            default:
              continue;
          }
        }
        if (position <= this.m_position)
          return (string) null;
        string str = this.m_text.Substring(this.m_position, position - this.m_position);
        this.m_position = position;
        return str;
      }

      public bool EOF => this.m_position == this.m_text.Length;

      public int Length => this.m_text.Length;

      public int Position
      {
        get => this.m_position;
        set => this.m_position = value;
      }
    }
}
