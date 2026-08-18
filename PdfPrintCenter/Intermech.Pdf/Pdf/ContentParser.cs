// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.ContentParser
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace Syncfusion.Pdf;

internal class ContentParser
{
  private ContentLexer m_lexer;
  private List<string> m_operands = new List<string>();
  private StringBuilder m_operatorParams;
  private PdfRecordCollection m_recordCollection;
  private static string[] operators = new string[79]
  {
    "b",
    "B",
    "bx",
    "Bx",
    "BDC",
    "BI",
    "BMC",
    "BT",
    "BX",
    "c",
    "cm",
    "CS",
    "cs",
    "d",
    "d0",
    "d1",
    "Do",
    "DP",
    "EI",
    "EMC",
    "ET",
    "EX",
    "f",
    "F",
    "fx",
    "G",
    "g",
    "gs",
    "h",
    "i",
    "ID",
    "j",
    "J",
    "K",
    "k",
    "l",
    "m",
    "M",
    "MP",
    "n",
    "q",
    "Q",
    "re",
    "RG",
    "rg",
    "ri",
    "s",
    "S",
    "SC",
    "sc",
    "SCN",
    "scn",
    "sh",
    "f*",
    "Tx",
    "Tc",
    "Td",
    "TD",
    "Tf",
    "Tj",
    "TJ",
    "TL",
    "Tm",
    "Tr",
    "Ts",
    "Tw",
    "Tz",
    "v",
    "w",
    "W",
    "W*",
    "Wx",
    "y",
    "T*",
    "b*",
    "B*",
    "'",
    "\"",
    "true"
  };

  public ContentParser(byte[] contentStream)
  {
    this.m_lexer = new ContentLexer(contentStream);
    this.m_operatorParams = this.m_lexer.OperatorParams;
    this.m_recordCollection = new PdfRecordCollection();
  }

  private void ConsumeValue()
  {
    char ch1 = ' ';
    char ch2 = ' ';
    char ch3 = ' ';
    int count = 0;
    char nextChar;
    do
    {
      nextChar = this.m_lexer.GetNextChar();
      if (nextChar != 'E')
      {
        this.m_operands.Add(nextChar.ToString());
      }
      else
      {
        ch1 = this.m_lexer.GetNextChar();
        if (ch1 != 'I')
        {
          this.m_operands.Add(nextChar.ToString());
          this.m_operands.Add(ch1.ToString());
        }
        else
        {
          ch2 = this.m_lexer.GetNextChar();
          ch3 = this.m_lexer.GetNextChar(true);
          while (ch3 == ' ' || ch3 == '\r' || ch3 == '\n')
          {
            ch3 = this.m_lexer.GetNextChar();
            ++count;
          }
          this.m_lexer.ResetContentPointer(count);
        }
      }
    }
    while (ch2 != ' ' && ch2 != '\n' || ch3 != 'Q');
    this.m_operatorParams.Length = 0;
    this.m_operatorParams.Append(nextChar);
    this.m_operatorParams.Append(ch1);
    this.CreateRecord();
    this.m_operands.Clear();
    ch1 = this.m_lexer.GetNextChar();
  }

  private void CreateRecord()
  {
    string name = this.m_operatorParams.ToString();
    Array.IndexOf<string>(ContentParser.operators, name);
    this.m_recordCollection.Add(new PdfRecord(name, this.m_operands.ToArray()));
  }

  private TokenType GetNextToken() => this.m_lexer.GetNextToken();

  private void ParseObject(TokenType stop)
  {
    TokenType nextToken;
    while ((nextToken = this.GetNextToken()) != TokenType.Eof && nextToken != stop)
    {
      switch (nextToken)
      {
        case TokenType.Integer:
          this.m_operands.Add(this.m_operatorParams.ToString());
          continue;
        case TokenType.Real:
          this.m_operands.Add(this.m_operatorParams.ToString());
          continue;
        case TokenType.String:
        case TokenType.HexString:
        case TokenType.UnicodeString:
        case TokenType.UnicodeHexString:
          this.m_operands.Add(this.m_operatorParams.ToString());
          continue;
        case TokenType.Name:
          this.m_operands.Add(this.m_operatorParams.ToString());
          continue;
        case TokenType.Operator:
          if (this.m_operatorParams.ToString() == "ID")
          {
            this.CreateRecord();
            this.m_operands.Clear();
            this.ConsumeValue();
            continue;
          }
          this.CreateRecord();
          this.m_operands.Clear();
          continue;
        case TokenType.EndArray:
          throw new InvalidOperationException("Error while parsing content");
        default:
          continue;
      }
    }
  }

  public PdfRecordCollection ReadContent()
  {
    this.ParseObject(TokenType.Eof);
    return this.m_recordCollection;
  }
}
