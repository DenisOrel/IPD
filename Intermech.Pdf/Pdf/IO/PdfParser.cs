// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.IO.PdfParser
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;


namespace Syncfusion.Pdf.IO
{
    internal class PdfParser
    {
      private bool m_bEncrypt;
      private bool m_colorSpace;
      private PdfCrossTable m_crossTable;
      private CrossTable m_cTable;
      private Queue<int> m_integerQueue = new Queue<int>();
      private PdfLexer m_lexer;
      private TokenType m_next;
      private PdfReader m_reader;

      public PdfParser(CrossTable cTable, PdfReader reader, PdfCrossTable crossTable)
      {
        if (reader == null)
          throw new ArgumentNullException(nameof (reader));
        if (cTable == null)
          throw new ArgumentNullException(nameof (cTable));
        if (crossTable == null)
          throw new ArgumentNullException(nameof (crossTable));
        this.m_reader = reader;
        this.m_cTable = cTable;
        this.m_crossTable = crossTable;
        this.m_lexer = new PdfLexer((TextReader) reader);
      }

      private void Advance() => this.m_next = this.m_lexer.GetNextToken();

      private IPdfPrimitive Array()
      {
        this.Match(this.m_next, TokenType.ArrayStart);
        this.Advance();
        PdfArray pdfArray = new PdfArray();
        IPdfPrimitive element;
        while ((element = this.Simple()) != null)
        {
          pdfArray.Add(element);
          this.m_colorSpace = pdfArray[0].ToString() == "/Indexed";
        }
        this.Match(this.m_next, TokenType.ArrayEnd);
        this.Advance();
        pdfArray.FreezeChanges((object) this);
        return (IPdfPrimitive) pdfArray;
      }

      private bool CheckForPreamble(string text)
      {
        string str = PdfString.ByteToString(Encoding.BigEndianUnicode.GetPreamble());
        return text.Length > 1 && text.Substring(0, 2).Equals(str);
      }

      private bool CheckStreamLength(int lexPosition, int value)
      {
        string str = (string) null;
        bool flag = true;
        long position = this.m_reader.Position;
        this.m_reader.Position = (long) (lexPosition + value);
        char[] buffer = new char[20];
        this.m_reader.ReadBlock(buffer, 0, 20);
        for (int index = 0; index < buffer.Length; ++index)
          str += buffer[index].ToString();
        if (!str.StartsWith("\nendstream") && !str.StartsWith("\r\nendstream") && !str.StartsWith("\rendstream") && !str.StartsWith("endstream"))
          flag = false;
        this.m_reader.Position = position;
        return flag;
      }

      private IPdfPrimitive Dictionary()
      {
        this.Match(this.m_next, TokenType.DictionaryStart);
        this.Advance();
        PdfDictionary dic = new PdfDictionary();
        PdfParser.Pair pair;
        while ((pair = this.ReadPair()) != (object) PdfParser.Pair.Empty)
          dic[pair.Name] = pair.Value;
        if (this.m_next != TokenType.DictionaryEnd)
          this.m_next = TokenType.DictionaryEnd;
        this.Match(this.m_next, TokenType.DictionaryEnd);
        if (!this.m_lexer.Skip)
        {
          this.Advance();
        }
        else
        {
          this.m_next = TokenType.ObjectEnd;
          this.m_lexer.Skip = false;
        }
        IPdfPrimitive pdfPrimitive = this.m_next != TokenType.StreamStart ? (IPdfPrimitive) dic : this.ReadStream(dic);
        (pdfPrimitive as IPdfChangable).FreezeChanges((object) this);
        return pdfPrimitive;
      }

      private void Error(PdfParser.ErrorType error, string additional)
      {
        string message;
        switch (error)
        {
          case PdfParser.ErrorType.Unexpected:
            message = "Unexpected token ";
            break;
          case PdfParser.ErrorType.BadlyFormedReal:
            message = "Badly formed real number ";
            break;
          case PdfParser.ErrorType.BadlyFormedInteger:
            message = "Badly formed integer number ";
            break;
          case PdfParser.ErrorType.BadlyFormedDictionary:
            message = "Badly formed dictionary ";
            break;
          case PdfParser.ErrorType.UnknownStreamLength:
            message = "Unknown stream length";
            break;
          default:
            message = "Internal error.";
            break;
        }
        if (additional != null)
          message = $"{message}{additional} before {(object) this.m_lexer.Position}";
        throw new PdfException(message);
      }

      internal char GetObjectFlag()
      {
        this.Match(this.m_next, TokenType.ObjectType);
        int objectFlag = (int) this.m_lexer.Text[0];
        this.Advance();
        return (char) objectFlag;
      }

      private IPdfPrimitive HexString()
      {
        this.Match(this.m_next, TokenType.HexStringStart);
        this.Advance();
        StringBuilder stringBuilder = new StringBuilder(100);
        bool flag = true;
        while (this.m_next != TokenType.HexStringEnd)
        {
          string str = this.m_lexer.Text;
          if (this.m_next == TokenType.HexStringWeird)
            flag = false;
          else if (this.m_next == TokenType.HexStringWeirdEscape)
          {
            flag = false;
            str = str.Substring(1);
          }
          stringBuilder.Append(str);
          this.Advance();
        }
        this.Match(this.m_next, TokenType.HexStringEnd);
        this.Advance();
        return (IPdfPrimitive) new PdfString(stringBuilder.ToString(), !flag);
      }

      private bool IsSubsection()
      {
        if (this.m_next == TokenType.Trailer)
          return false;
        if (this.m_next != TokenType.Number)
          throw new PdfDocumentException("Invalid/Unknown/Unsupported format");
        return true;
      }

      private void Match(TokenType token, TokenType match)
      {
        if (token == match)
          return;
        this.Error(PdfParser.ErrorType.Unexpected, token.ToString());
      }

      private IPdfPrimitive Number()
      {
        PdfNumber pdfNumber;
        if (this.m_integerQueue.Count > 0)
        {
          pdfNumber = new PdfNumber(this.m_integerQueue.Dequeue());
        }
        else
        {
          this.Match(this.m_next, TokenType.Number);
          pdfNumber = this.ParseInteger();
        }
        IPdfPrimitive pdfPrimitive1 = (IPdfPrimitive) pdfNumber;
        if (this.m_next == TokenType.Number)
        {
          PdfNumber integer = this.ParseInteger();
          if (this.m_next == TokenType.Reference)
          {
            IPdfPrimitive pdfPrimitive2 = (IPdfPrimitive) new PdfReferenceHolder(new PdfReference((long) pdfNumber.IntValue, integer.IntValue), this.m_crossTable);
            this.Advance();
            return pdfPrimitive2;
          }
          this.m_integerQueue.Enqueue(integer.IntValue);
        }
        return pdfPrimitive1;
      }

      public IPdfPrimitive Parse()
      {
        this.Match(this.m_next, TokenType.Number);
        this.Simple();
        this.Simple();
        this.Match(this.m_next, TokenType.ObjectStart);
        this.Advance();
        IPdfPrimitive pdfPrimitive = this.Simple();
        if (this.m_next != TokenType.ObjectEnd)
          this.m_next = TokenType.ObjectEnd;
        this.Match(this.m_next, TokenType.ObjectEnd);
        if (!this.m_lexer.Skip)
        {
          this.Advance();
          return pdfPrimitive;
        }
        this.m_lexer.Skip = false;
        return pdfPrimitive;
      }

      public IPdfPrimitive Parse(long offset)
      {
        this.SetOffset(offset);
        this.Advance();
        return this.Parse();
      }

      private PdfNumber ParseInteger()
      {
        double result;
        int num = double.TryParse(this.m_lexer.Text, NumberStyles.Integer, (IFormatProvider) CultureInfo.InvariantCulture, out result) ? 1 : 0;
        PdfNumber integer = (PdfNumber) null;
        if (num != 0)
          integer = new PdfNumber((long) result);
        else
          this.Error(PdfParser.ErrorType.BadlyFormedInteger, this.m_lexer.Text);
        this.Advance();
        return integer;
      }

      private void ParseOldXRef(
        CrossTable cTable,
        System.Collections.Generic.Dictionary<long, CrossTable.ObjectInformation> objects)
      {
        this.Advance();
        while (this.IsSubsection())
          cTable.ParseSubsection(this, objects);
      }

      public IPdfPrimitive ParseXRefTable(
        System.Collections.Generic.Dictionary<long, CrossTable.ObjectInformation> objects,
        CrossTable cTable)
      {
        this.Advance();
        if (this.m_next == TokenType.XRef)
        {
          this.ParseOldXRef(cTable, objects);
          IPdfPrimitive xrefTable = this.Trailer();
          PdfDictionary pdfDictionary = xrefTable as PdfDictionary;
          if (pdfDictionary.ContainsKey("Size"))
          {
            int intValue = (pdfDictionary["Size"] as PdfNumber).IntValue;
            int numberOfSubsection1 = (int) cTable.m_initialNumberOfSubsection;
            int numberOfSubsection2 = (int) cTable.m_totalNumberOfSubsection;
            if (intValue >= numberOfSubsection1 + numberOfSubsection2 || numberOfSubsection1 <= 0 || intValue != numberOfSubsection2)
              return xrefTable;
            int num = numberOfSubsection1 + numberOfSubsection2 - intValue;
            System.Collections.Generic.Dictionary<long, CrossTable.ObjectInformation> dictionary = new System.Collections.Generic.Dictionary<long, CrossTable.ObjectInformation>();
            foreach (KeyValuePair<long, CrossTable.ObjectInformation> keyValuePair in objects)
              dictionary.Add(keyValuePair.Key - (long) num, keyValuePair.Value);
            objects = dictionary;
            cTable.m_objects = dictionary;
          }
          return xrefTable;
        }
        IPdfPrimitive stream = this.Parse();
        cTable.ParseNewTable(stream as PdfStream, objects);
        return stream;
      }

      private string ProcessEscapes(string text)
      {
        text = text.Replace("\r", "");
        StringBuilder stringBuilder = new StringBuilder(text.Length);
        bool flag = false;
        int i = 0;
        for (int length = text.Length; i < length; ++i)
        {
          char ch = text[i];
          if (!flag)
          {
            switch (ch)
            {
              case char.MinValue:
                if (this.Encrypted)
                {
                  stringBuilder.Append(ch);
                  continue;
                }
                continue;
              case '\\':
                flag = true;
                continue;
              default:
                stringBuilder.Append(ch);
                continue;
            }
          }
          else
          {
            if (ch <= 'b')
            {
              if (ch <= ')')
              {
                if (ch != '(' && ch != ')')
                  goto label_23;
              }
              else if (ch != '\\')
              {
                if (ch == 'b')
                {
                  stringBuilder.Append('\b');
                  goto label_27;
                }
                goto label_23;
              }
              stringBuilder.Append(ch);
              goto label_27;
            }
            if (ch <= 'n')
            {
              if (ch != 'f')
              {
                if (ch == 'n')
                {
                  stringBuilder.Append('\n');
                  goto label_27;
                }
              }
              else
              {
                stringBuilder.Append('\f');
                goto label_27;
              }
            }
            else if (ch != 'r')
            {
              if (ch == 't')
              {
                stringBuilder.Append('\t');
                goto label_27;
              }
            }
            else
            {
              stringBuilder.Append('\r');
              goto label_27;
            }
    label_23:
            if (ch <= '7' && ch >= '0')
            {
              ch = this.ProcessOctal(text, ref i);
              --i;
            }
            if (ch < 'Ā')
              stringBuilder.Append(ch);
    label_27:
            flag = false;
          }
        }
        return stringBuilder.ToString();
      }

      private char ProcessOctal(string text, ref int i)
      {
        int length = text.Length;
        int num = 0;
        string empty = string.Empty;
        for (; i < length && num < 3; ++num)
        {
          char ch = text[i];
          switch (ch)
          {
            case '0':
            case '1':
            case '2':
            case '3':
            case '4':
            case '5':
            case '6':
            case '7':
              empty += ch.ToString();
              break;
          }
          ++i;
        }
        return (char) Convert.ToInt32(empty, 8);
      }

      private string ProcessUnicodeEscapes(string text)
      {
        StringBuilder stringBuilder = new StringBuilder(text.Length / 2);
        bool flag = true;
        char ch1 = char.MinValue;
        foreach (char ch2 in text)
        {
          if (flag)
          {
            if (ch2 == ' ')
            {
              stringBuilder.Append(ch2);
              flag = !flag;
            }
            else
              ch1 = (char) ((uint) ch2 << 8);
          }
          else if (ch2 != '\r' && ch2 != '\\')
          {
            if ((int) ch1 + (int) ch2 <= 257)
            {
              ch1 += ch2;
              stringBuilder.Append(ch1);
            }
            else if (stringBuilder.Length > 0)
            {
              ch1 = (char) ((uint) char.MinValue + (uint) ch2);
              stringBuilder.Append(ch1);
            }
          }
          else
            flag = !flag;
          flag = !flag;
        }
        return this.ProcessEscapes(stringBuilder.ToString());
      }

      private void ProcessUnicodeWithPreamble(ref string text)
      {
        byte[] numArray = PdfString.StringToByte(text.Substring(2));
        int index1 = 0;
        string str = (string) null;
        bool flag = false;
        for (int index2 = 0; index2 < numArray.Length - 1; ++index2)
        {
          if (numArray[index2] == (byte) 92 && (numArray[index2 + 1] == (byte) 40 || numArray[index2 + 1] == (byte) 41 || numArray[index2 + 1] == (byte) 13 || numArray[index2 + 1] == (byte) 62 || numArray[index2 + 1] == (byte) 92) || numArray[index2] == (byte) 13)
          {
            for (int index3 = index2; index3 < numArray.Length - 1; ++index3)
              numArray[index3] = numArray[index3 + 1];
            byte[] dst = new byte[numArray.Length - 1];
            Buffer.BlockCopy((System.Array) numArray, 0, (System.Array) dst, 0, numArray.Length - 1);
            numArray = dst;
            --index2;
          }
          else if (numArray[index2] == (byte) 92 && numArray[index2 + 1] == (byte) 114)
          {
            MemoryStream memoryStream = new MemoryStream();
            for (int index4 = 0; index4 < index2; ++index4)
              memoryStream.WriteByte(numArray[index4]);
            memoryStream.WriteByte((byte) 13);
            for (int index5 = index2 + 2; index5 < numArray.Length; ++index5)
              memoryStream.WriteByte(numArray[index5]);
            numArray = PdfStream.StreamToBytes((Stream) memoryStream);
            memoryStream.Dispose();
          }
          else if (numArray[index2] == (byte) 92 && numArray[index2 + 1] == (byte) 110)
          {
            flag = true;
            int count = index2 - 1 - index1;
            str = $"{str}{Encoding.BigEndianUnicode.GetString(numArray, index1, count)}\r\n";
            index1 = index2 + 2;
            index2 += 2;
          }
        }
        int count1 = numArray.Length - index1;
        if (flag)
        {
          text = str;
          text += Encoding.BigEndianUnicode.GetString(numArray, index1, count1);
        }
        else
          text = Encoding.BigEndianUnicode.GetString(numArray, index1, count1);
      }

      private IPdfPrimitive ReadBoolean()
      {
        this.Match(this.m_next, TokenType.Boolean);
        PdfBoolean pdfBoolean = new PdfBoolean(this.m_lexer.Text == "true");
        this.Advance();
        return (IPdfPrimitive) pdfBoolean;
      }

      private IPdfPrimitive ReadName()
      {
        this.Match(this.m_next, TokenType.Name);
        PdfName pdfName = new PdfName(this.m_lexer.Text.Substring(1));
        this.Advance();
        return (IPdfPrimitive) pdfName;
      }

      private PdfParser.Pair ReadPair()
      {
        IPdfPrimitive pdfPrimitive;
        try
        {
          pdfPrimitive = this.Simple();
        }
        catch
        {
          pdfPrimitive = (IPdfPrimitive) null;
        }
        if (pdfPrimitive == null)
          return PdfParser.Pair.Empty;
        PdfName name = pdfPrimitive as PdfName;
        if (name == (PdfName) null)
          this.Error(PdfParser.ErrorType.BadlyFormedDictionary, "next should be a name.");
        return new PdfParser.Pair(name, this.Simple());
      }

      private IPdfPrimitive ReadStream(PdfDictionary dic)
      {
        this.Match(this.m_next, TokenType.StreamStart);
        this.m_lexer.SkipToken();
        this.m_lexer.SkipNewLine();
        IPdfPrimitive pdfPrimitive = dic["Length"];
        PdfNumber pdfNumber = pdfPrimitive as PdfNumber;
        PdfReferenceHolder pdfReferenceHolder = pdfPrimitive as PdfReferenceHolder;
        if (pdfNumber == null && pdfReferenceHolder == (PdfReferenceHolder) null)
        {
          PdfLexer lexer = this.m_lexer;
          long position = this.m_reader.Position;
          this.m_lexer = new PdfLexer((TextReader) this.m_reader);
          long num = this.m_reader.SearchBack("stream");
          long count = this.m_reader.SearchForward("endstream") - num;
          this.m_reader.Position = position;
          this.m_lexer = lexer;
          byte[] data = this.m_lexer.Read((int) count);
          PdfStream pdfStream = new PdfStream(dic, data);
          this.Advance();
          if (this.m_next != TokenType.StreamEnd)
            this.m_next = TokenType.StreamEnd;
          this.Match(this.m_next, TokenType.StreamEnd);
          this.Advance();
          if (this.m_next == TokenType.ObjectEnd)
            return (IPdfPrimitive) pdfStream;
          this.m_next = TokenType.ObjectEnd;
          return (IPdfPrimitive) pdfStream;
        }
        if (pdfReferenceHolder != (PdfReferenceHolder) null)
        {
          PdfLexer lexer = this.m_lexer;
          long position = this.m_reader.Position;
          this.m_lexer = new PdfLexer((TextReader) this.m_reader);
          pdfNumber = this.m_cTable.GetObject((IPdfPrimitive) pdfReferenceHolder.Reference) as PdfNumber;
          this.m_reader.Position = position;
          this.m_lexer = lexer;
        }
        int intValue = pdfNumber.IntValue;
        PdfStream pdfStream1;
        if ((this.CheckStreamLength(this.m_lexer.Position, intValue) ? 1 : 0) != 0)
        {
          byte[] data = this.m_lexer.Read(intValue);
          pdfStream1 = new PdfStream(dic, data);
        }
        else
        {
          long position1 = (long) this.m_lexer.Position;
          long position2 = this.m_reader.Position;
          this.m_reader.Position = position1;
          long count = this.m_reader.SearchForward("endstream") - position1;
          this.m_reader.Position = position2;
          byte[] data = this.m_lexer.Read((int) count);
          pdfStream1 = new PdfStream(dic, data);
        }
        this.Advance();
        if (this.m_next != TokenType.StreamEnd)
          this.m_next = TokenType.StreamEnd;
        this.Match(this.m_next, TokenType.StreamEnd);
        this.Advance();
        if (this.m_next != TokenType.ObjectEnd)
          this.m_next = TokenType.ObjectEnd;
        return (IPdfPrimitive) pdfStream1;
      }

      private IPdfPrimitive ReadString()
      {
        this.Match(this.m_next, TokenType.String);
        string text = this.m_lexer.StringText.ToString();
        bool flag = false;
        if (!this.m_colorSpace)
        {
          if (this.CheckForPreamble(text))
          {
            this.ProcessUnicodeWithPreamble(ref text);
            flag = true;
          }
          else
          {
            text = this.ProcessEscapes(text);
            if (this.CheckForPreamble(text))
            {
              this.ProcessUnicodeWithPreamble(ref text);
              flag = true;
            }
          }
        }
        else
          text = "ColorFound" + text;
        PdfString pdfString = new PdfString(text);
        if (!flag)
          pdfString.Encode = PdfString.ForceEncoding.ASCII;
        this.Advance();
        return (IPdfPrimitive) pdfString;
      }

      private IPdfPrimitive ReadUnicodeString()
      {
        char[] charArray = this.m_lexer.Text.ToCharArray();
        string text = new string(charArray, 1, charArray.Length - 2);
        string str = PdfString.ByteToString(Encoding.BigEndianUnicode.GetPreamble());
        if (charArray.Length > 1)
        {
          if (text.Substring(0, 2).Equals(str))
            this.ProcessUnicodeWithPreamble(ref text);
          else
            text = this.ProcessUnicodeEscapes(text);
        }
        else
          text = this.ProcessUnicodeEscapes(text);
        PdfString pdfString = new PdfString(text);
        if (!this.m_lexer.Skip)
        {
          this.Advance();
          return (IPdfPrimitive) pdfString;
        }
        this.m_next = TokenType.DictionaryEnd;
        return (IPdfPrimitive) pdfString;
      }

      private IPdfPrimitive Real()
      {
        this.Match(this.m_next, TokenType.Real);
        double result;
        int num = double.TryParse(this.m_lexer.Text, NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result) ? 1 : 0;
        PdfNumber pdfNumber = (PdfNumber) null;
        if (num != 0)
          pdfNumber = new PdfNumber((float) result);
        else
          this.Error(PdfParser.ErrorType.BadlyFormedReal, this.m_lexer.Text);
        this.Advance();
        return (IPdfPrimitive) pdfNumber;
      }

      public void RebuildXrefTable(
        System.Collections.Generic.Dictionary<long, CrossTable.ObjectInformation> newObjects,
        CrossTable crosstable)
      {
        PdfReader pdfReader = new PdfReader(this.m_reader.Stream);
        pdfReader.Position = 0L;
        newObjects.Clear();
        while (this.m_reader.Position < pdfReader.Stream.Length - 1L)
        {
          string str = "";
          while (str == "")
            str = pdfReader.ReadLine();
          char[] charArray = str.ToCharArray();
          if (charArray[0] >= '0' && charArray[0] <= '9')
          {
            string[] strArray = str.Split(' ');
            long result1;
            long result2;
            if (strArray.Length > 1 && long.TryParse(strArray[0], out result1) && long.TryParse(strArray[1], out result2) && result2 == 0L && strArray[2].Equals("obj"))
            {
              CrossTable.ObjectInformation objectInformation = new CrossTable.ObjectInformation(CrossTable.ObjectType.Normal, this.m_reader.Position - (long) charArray.Length - 1L, (CrossTable.ArchiveInformation) null, crosstable);
              newObjects.Add(result1, objectInformation);
            }
          }
        }
      }

      public void SetOffset(long offset)
      {
        this.m_reader.Position = offset;
        if (this.m_integerQueue.Count > 0)
          this.m_integerQueue.Clear();
        this.m_lexer.Reset();
      }

      internal IPdfPrimitive Simple()
      {
        if (this.m_integerQueue.Count != 0)
          return this.Number();
        switch (this.m_next)
        {
          case TokenType.DictionaryStart:
            return this.Dictionary();
          case TokenType.HexStringStart:
            return this.HexString();
          case TokenType.String:
            return this.ReadString();
          case TokenType.UnicodeString:
            return this.ReadUnicodeString();
          case TokenType.Number:
            return this.Number();
          case TokenType.Real:
            return this.Real();
          case TokenType.Name:
            return this.ReadName();
          case TokenType.ArrayStart:
            return this.Array();
          case TokenType.Boolean:
            return this.ReadBoolean();
          case TokenType.Null:
            PdfNull pdfNull = new PdfNull();
            this.Advance();
            return (IPdfPrimitive) pdfNull;
          default:
            return (IPdfPrimitive) null;
        }
      }

      internal void StartFrom(long offset)
      {
        this.SetOffset(offset);
        this.Advance();
      }

      public long StartXRef()
      {
        this.Advance();
        this.Match(this.m_next, TokenType.StartXRef);
        this.Advance();
        return (long) (this.Number() as PdfNumber).IntValue;
      }

      public IPdfPrimitive Trailer()
      {
        this.Match(this.m_next, TokenType.Trailer);
        this.Advance();
        return this.Dictionary();
      }

      public IPdfPrimitive Trailer(long offset)
      {
        this.SetOffset(offset);
        return this.Trailer();
      }

      internal bool Encrypted
      {
        get => this.m_bEncrypt;
        set => this.m_bEncrypt = value;
      }

      private enum ErrorType
      {
        None,
        Unexpected,
        BadlyFormedReal,
        BadlyFormedInteger,
        BadlyFormedHexString,
        BadlyFormedDictionary,
        UnknownStreamLength,
      }

      private struct Pair(PdfName name, IPdfPrimitive value)
      {
        public static readonly PdfParser.Pair Empty = new PdfParser.Pair((PdfName) null, (IPdfPrimitive) null);
        public PdfName Name = name;
        public IPdfPrimitive Value = value;

        public static bool operator ==(PdfParser.Pair pair, object obj)
        {
          return !object.Equals(obj, (object) null) && obj is PdfParser.Pair pair1 && pair1.Name == pair.Name && pair.Value == pair1.Value;
        }

        public static bool operator !=(PdfParser.Pair pair, object obj)
        {
          return !object.Equals((object) pair, obj);
        }

        public override bool Equals(object obj) => this == obj;

        public override int GetHashCode() => base.GetHashCode();
      }
    }
}
