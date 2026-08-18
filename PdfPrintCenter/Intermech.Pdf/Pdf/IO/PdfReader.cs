// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.IO.PdfReader
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.IO;
using System.Text;

#nullable disable
namespace Syncfusion.Pdf.IO;

internal class PdfReader : TextReader
{
  private bool m_bBytePeeked;
  private string m_delimiters = "()<>[]{}/%";
  private int m_peekedByte;
  private Stream m_stream;

  public PdfReader(Stream stream)
  {
    this.m_stream = stream != null ? stream : throw new ArgumentNullException(nameof (stream));
  }

  private int AppendChar(ref string line)
  {
    int num = this.Read();
    if (num != -1)
      line = line.Insert(line.Length, ((char) num).ToString());
    return num;
  }

  public override void Close() => this.Dispose(true);

  protected override void Dispose(bool disposing)
  {
    this.m_stream = (Stream) null;
    base.Dispose(disposing);
  }

  public string GetNextToken()
  {
    string empty = string.Empty;
    this.SkipWS();
    int character = this.Peek();
    int num;
    if (!this.IsDelimiter((char) character))
    {
      for (; character != -1 && !this.IsSeparator((char) character); character = this.Peek())
        num = this.AppendChar(ref empty);
      return empty;
    }
    num = this.AppendChar(ref empty);
    return empty;
  }

  private bool GetPeeked(out int byteValue)
  {
    bool bBytePeeked = this.m_bBytePeeked;
    if (this.m_bBytePeeked)
    {
      this.m_bBytePeeked = false;
      byteValue = this.m_peekedByte;
      return bBytePeeked;
    }
    byteValue = 0;
    return bBytePeeked;
  }

  public bool IsDelimiter(char character)
  {
    foreach (int delimiter in this.m_delimiters)
    {
      if (delimiter == (int) character)
        return true;
    }
    return false;
  }

  public bool IsEol(char character) => character == '\n' || character == '\r';

  public bool IsSeparator(char character)
  {
    return char.IsWhiteSpace(character) || this.IsDelimiter(character);
  }

  public override int Peek()
  {
    int byteValue;
    if (this.m_bBytePeeked)
    {
      this.GetPeeked(out byteValue);
    }
    else
    {
      this.m_peekedByte = this.Read();
      byteValue = this.m_peekedByte;
    }
    if (this.m_peekedByte != -1)
      this.m_bBytePeeked = true;
    return byteValue;
  }

  public override int Read()
  {
    if (!this.m_bBytePeeked)
      return this.m_stream.ReadByte();
    int byteValue;
    this.GetPeeked(out byteValue);
    return byteValue;
  }

  public override int Read(char[] buffer, int index, int count)
  {
    if (count < 0)
      throw new ArgumentException("The value can't be less then zero", nameof (count));
    int index1 = index;
    if (this.m_bBytePeeked && count > 0)
    {
      buffer[index1] = (char) this.m_peekedByte;
      this.m_bBytePeeked = false;
      --count;
      ++index1;
    }
    if (count > 0)
    {
      byte[] buffer1 = new byte[count];
      count = this.m_stream.Read(buffer1, 0, count);
      for (int index2 = 0; index2 < count; ++index2)
      {
        char ch = (char) buffer1[index2];
        buffer[index1 + index2] = ch;
      }
      index1 += count;
    }
    return index1 - index;
  }

  public string ReadBack(int length)
  {
    Encoding utF8 = Encoding.UTF8;
    byte[] buffer = new byte[length];
    if (this.Position < (long) length)
      throw new PdfDocumentException("Invalid/Unknown/Unsupported format");
    this.Position -= (long) length;
    if (this.m_stream.Read(buffer, 0, length) < length)
      throw new PdfDocumentException("Read failure.");
    byte[] bytes = buffer;
    int length1 = buffer.Length;
    return utF8.GetString(bytes, 0, length1);
  }

  public override int ReadBlock(char[] buffer, int index, int count)
  {
    return this.Read(buffer, index, count);
  }

  public override string ReadLine()
  {
    string str = string.Empty;
    int character;
    for (character = this.m_stream.ReadByte(); character != -1 && !this.IsEol((char) character); character = this.m_stream.ReadByte())
      str = str.Insert(str.Length, ((char) character).ToString());
    if (character == 13 && this.m_stream.ReadByte() != 10)
      --this.m_stream.Position;
    return str;
  }

  internal string ReadStream() => new StreamReader(this.m_stream).ReadToEnd();

  public override string ReadToEnd()
  {
    string end = string.Empty;
    for (int index = this.Read(); index != -1; index = this.m_stream.ReadByte())
      end = end.Insert(end.Length, ((char) index).ToString());
    return end;
  }

  public long SearchBack(string token)
  {
    long position = this.Position;
    this.SkipWSBack();
    if (this.Position < (long) token.Length)
      return -1;
    string str = this.ReadBack(token.Length);
    long num = this.Position - (long) token.Length;
    while (str.CompareTo(token) != 0)
    {
      if (num < 0L)
        throw new PdfDocumentException($"Invalid/Unknown/Unsupported format\nUnable to find token '{token}'");
      --this.Position;
      if (this.Position < (long) token.Length)
        return -1;
      str = this.ReadBack(token.Length);
      num = this.Position - (long) token.Length;
    }
    this.Position = num;
    return num;
  }

  public long SearchForward(string token)
  {
    Encoding utF8 = Encoding.UTF8;
    byte[] numArray = new byte[token.Length];
    int num1;
    do
    {
      long position = this.Position;
      num1 = this.Read();
      numArray[0] = (byte) num1;
      if ((int) numArray[0] == (int) token[0])
      {
        long num2 = this.Position - 1L;
        int num3 = this.m_stream.Read(numArray, 1, token.Length - 1);
        this.Position = num2;
        int num4 = token.Length - 1;
        if (num3 < num4)
          return -1;
        if (token.CompareTo(utF8.GetString(numArray, 0, numArray.Length)) == 0)
          return num2;
        ++this.Position;
      }
    }
    while (num1 != -1);
    return -1;
  }

  public long Seek(long offset, SeekOrigin origin) => this.m_stream.Seek(offset, origin);

  public void SkipWS()
  {
    if (this.Position == this.m_stream.Length)
      return;
    int c;
    do
    {
      c = this.Read();
    }
    while (char.IsWhiteSpace((char) c));
    if (c == -1)
      this.Position = this.m_stream.Length;
    else
      --this.Position;
  }

  public void SkipWSBack()
  {
    if (this.Position == 0L)
      throw new PdfDocumentException("Invalid/Unknown/Unsupported format");
    --this.Position;
    while (char.IsWhiteSpace((char) this.Read()))
      this.Position -= 2L;
  }

  public long Position
  {
    get => this.m_stream.Position;
    set => this.m_stream.Position = value;
  }

  public Stream Stream => this.m_stream;
}
