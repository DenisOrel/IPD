// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.ContentLexer
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Text;

#nullable disable
namespace Syncfusion.Pdf;

internal class ContentLexer
{
  private int m_charPointer;
  private byte[] m_contentStream;
  private char m_currentChar;
  private char m_nextChar;
  private StringBuilder m_operatorParams = new StringBuilder();
  private static string[] m_textShowers = new string[4]
  {
    "Tj",
    "'",
    "TJ",
    "\""
  };
  private TokenType m_tType;

  public ContentLexer(byte[] contentStream) => this.m_contentStream = contentStream;

  private bool CheckForTextOperator()
  {
    char nextChar = this.m_nextChar;
    int num = 0;
    if (Array.IndexOf<string>(ContentLexer.m_textShowers, nextChar.ToString()) < 0)
    {
      if (this.IsWhiteSpace(nextChar))
      {
        nextChar = (char) this.m_contentStream[this.m_charPointer];
        ++num;
      }
      string str = nextChar.ToString() + ((char) this.m_contentStream[this.m_charPointer + num]).ToString();
      if (Array.IndexOf<string>(ContentLexer.m_textShowers, str) < 0)
        return false;
    }
    return true;
  }

  private char ConsumeValue()
  {
    this.m_operatorParams.Append(this.m_currentChar);
    return this.GetNextChar();
  }

  private TokenType GetComment()
  {
    this.ResetToken();
    char ch;
    do
      ;
    while ((ch = this.ConsumeValue()) != '\n' && ch != char.MaxValue);
    return TokenType.Comment;
  }

  private TokenType GetHexadecimalString()
  {
    char ch1 = '<';
    char ch2 = '>';
    int num1 = 0;
    char ch3 = this.ConsumeValue();
    while (true)
    {
      do
      {
        for (; (int) ch3 == (int) ch1; ch3 = this.ConsumeValue())
          ++num1;
        if ((int) ch3 == (int) ch2)
        {
          if (num1 == 0)
          {
            int num2 = (int) this.ConsumeValue();
            return TokenType.HexString;
          }
          ch3 = this.ConsumeValue();
        }
        else
          goto label_8;
      }
      while (!ch3.Equals('>'));
      --num1;
      continue;
label_8:
      ch3 = this.ConsumeValue();
    }
  }

  private string GetLiterals(char ch)
  {
    int num = 0;
    string str1 = "";
    while (true)
    {
      switch (ch)
      {
        case '(':
          ++num;
          str1 += ch.ToString();
          ch = this.GetNextChar();
          continue;
        case '\\':
          string str2 = str1 + ch.ToString();
          ch = this.GetNextChar();
          str1 = str2 + ch.ToString();
          ch = this.GetNextChar();
          continue;
        default:
          if (ch == ')' && num != 0)
          {
            str1 += ch.ToString();
            ch = this.GetNextChar();
            --num;
            continue;
          }
          if (ch != ')' || num != 0)
          {
            str1 += ch.ToString();
            ch = this.GetNextChar();
            continue;
          }
          goto label_7;
      }
    }
label_7:
    return str1 + ch.ToString();
  }

  private TokenType GetLiteralString()
  {
    this.ResetToken();
    char ch1 = this.m_currentChar != '(' ? this.m_currentChar : this.m_currentChar;
    char ch2 = this.ConsumeValue();
    char ch3;
    while (ch1 != '(')
    {
      switch (ch2)
      {
        case '(':
          this.m_operatorParams.Append(this.GetLiterals(this.ConsumeValue()));
          ch2 = this.GetNextChar();
          continue;
        case ']':
          ch3 = this.ConsumeValue();
          goto label_7;
        default:
          ch2 = this.ConsumeValue();
          continue;
      }
    }
    this.m_operatorParams.Append(this.GetLiterals(ch2));
    ch3 = this.GetNextChar();
label_7:
    return TokenType.String;
  }

  private TokenType GetName()
  {
    this.ResetToken();
    char ch;
    do
    {
      ch = this.ConsumeValue();
    }
    while (!this.IsWhiteSpace(ch) && !this.IsDelimiter(ch));
    return TokenType.Name;
  }

  internal char GetNextChar()
  {
    if (this.m_contentStream.Length > this.m_charPointer)
    {
      this.m_currentChar = this.m_nextChar;
      this.m_nextChar = (char) this.m_contentStream[this.m_charPointer++];
      if (this.m_currentChar == '\r')
      {
        if (this.m_nextChar == '\n')
        {
          this.m_currentChar = this.m_nextChar;
          if (this.m_contentStream.Length > this.m_charPointer)
            this.m_nextChar = (char) this.m_contentStream[this.m_charPointer++];
        }
        else
          this.m_currentChar = '\n';
      }
    }
    return this.m_currentChar;
  }

  internal char GetNextChar(bool value) => this.m_nextChar;

  public TokenType GetNextToken()
  {
    this.ResetToken();
    char nextChar = this.MoveToNextChar();
    switch (nextChar)
    {
      case '"':
      case '\'':
        return this.m_tType = this.GetOperator();
      case '%':
        return this.m_tType = this.GetComment();
      case '(':
      case '[':
        return this.m_tType = this.GetLiteralString();
      case '+':
      case '-':
        return this.m_tType = this.GetNumber();
      case '.':
        return this.m_tType = this.GetNumber();
      case '/':
        return this.m_tType = this.GetName();
      case '<':
        return this.m_tType = this.GetHexadecimalString();
      default:
        if (char.IsDigit(nextChar))
          return this.m_tType = this.GetNumber();
        if (char.IsLetter(nextChar))
          return this.m_tType = this.GetOperator();
        return nextChar == char.MaxValue ? (this.m_tType = TokenType.Eof) : TokenType.None;
    }
  }

  private TokenType GetNumber()
  {
    char c = this.m_currentChar;
    switch (c)
    {
      case '+':
      case '-':
        this.m_operatorParams.Append(this.m_currentChar);
        c = this.GetNextChar();
        break;
    }
    while (true)
    {
      if (char.IsDigit(c))
        this.m_operatorParams.Append(this.m_currentChar);
      else if (c == '.')
        this.m_operatorParams.Append(this.m_currentChar);
      else
        break;
      c = this.GetNextChar();
    }
    return TokenType.Integer;
  }

  private TokenType GetOperator()
  {
    this.ResetToken();
    char ch = this.m_currentChar;
    while (this.IsOperator(ch))
      ch = this.ConsumeValue();
    return TokenType.Operator;
  }

  private bool IsDelimiter(char ch)
  {
    switch (ch)
    {
      case '%':
      case '(':
      case ')':
      case '/':
      case '<':
      case '>':
      case '[':
      case ']':
        return true;
      default:
        return false;
    }
  }

  private bool IsOperator(char ch)
  {
    if (!char.IsLetter(ch))
    {
      switch (ch)
      {
        case '"':
        case '\'':
        case '*':
          break;
        default:
          return false;
      }
    }
    return true;
  }

  private bool IsWhiteSpace(char ch)
  {
    switch (ch)
    {
      case char.MinValue:
      case '\t':
      case '\n':
      case '\f':
      case '\r':
      case ' ':
        return true;
      default:
        return false;
    }
  }

  private char MoveToNextChar()
  {
    while (this.m_currentChar != char.MaxValue)
    {
      switch (this.m_currentChar)
      {
        case char.MinValue:
        case '\b':
        case '\t':
        case '\n':
        case '\f':
        case '\r':
        case ' ':
          int nextChar = (int) this.GetNextChar();
          continue;
        default:
          return this.m_currentChar;
      }
    }
    return this.m_currentChar;
  }

  internal void ResetContentPointer(int count) => this.m_charPointer -= count;

  private void ResetToken() => this.m_operatorParams.Length = 0;

  internal StringBuilder OperatorParams => this.m_operatorParams;

  internal TokenType Token => this.m_tType;
}
