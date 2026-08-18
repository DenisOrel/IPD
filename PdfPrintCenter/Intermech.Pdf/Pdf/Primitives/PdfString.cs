// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Primitives.PdfString
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

#nullable disable
namespace Syncfusion.Pdf.Primitives;

internal class PdfString : IPdfPrimitive, IPdfDecryptable
{
  private const string HexFormatPattern = "{0:X2}";
  public const string HexStringMark = "<>";
  internal bool IsFormField;
  private bool m_bConverted;
  private bool m_bDecrypted;
  private PdfString.ForceEncoding m_bForceEncoding;
  private bool m_bHex;
  private PdfString m_clonedObject;
  private PdfCrossTable m_crossTable;
  private byte[] m_data;
  private int m_index;
  private bool m_isPacked;
  private bool m_isParentDecrypted;
  private bool m_isSaving;
  private int m_position;
  private ObjectStatus m_status;
  private string m_value;
  public const string StringMark = "()";

  public PdfString() => this.m_position = -1;

  public PdfString(string value)
  {
    this.m_position = -1;
    if (value == null)
      throw new ArgumentNullException(nameof (value));
    if (value.Length > 0 && value[0] == '\uFEFF')
    {
      this.m_value = value.Substring(1);
    }
    else
    {
      this.m_value = value;
      this.m_data = PdfString.GetAsciiBytes(value);
    }
  }

  public PdfString(byte[] value)
  {
    this.m_position = -1;
    this.m_data = value != null ? value : throw new ArgumentNullException(nameof (value));
    this.m_value = this.m_data[0] != (byte) 254 || this.m_data[1] != byte.MaxValue ? PdfString.ByteToString(this.m_data) : Encoding.BigEndianUnicode.GetString(this.m_data, 2, this.m_data.Length - 2);
    this.m_bHex = true;
  }

  public PdfString(string value, bool encrypted)
  {
    this.m_position = -1;
    if (value == null)
      throw new ArgumentNullException(nameof (value));
    if (!encrypted && value != string.Empty)
    {
      this.m_data = this.HexToBytes(value);
      this.m_value = this.m_data.Length == 0 ? value : (this.m_data[0] != (byte) 254 || this.m_data[1] != byte.MaxValue ? PdfString.ByteToString(this.m_data) : Encoding.BigEndianUnicode.GetString(this.m_data, 2, this.m_data.Length - 2));
    }
    else
      this.m_value = value;
    this.m_bHex = true;
  }

  internal static int ByteCompare(PdfString str1, PdfString str2)
  {
    byte[] bytes1 = str1.Bytes;
    byte[] bytes2 = str2.Bytes;
    int num1 = Math.Min(bytes1.Length, bytes2.Length);
    int num2 = 0;
    for (int index = 0; index < num1; ++index)
    {
      num2 = (int) bytes1[index] - (int) bytes2[index];
      if (num2 != 0)
        break;
    }
    if (num2 == 0)
      num2 = bytes1.Length - bytes2.Length;
    return num2;
  }

  internal static string BytesToHex(byte[] bytes)
  {
    if (bytes == null)
      return string.Empty;
    StringBuilder stringBuilder = new StringBuilder();
    int index = 0;
    for (int length = bytes.Length; index < length; ++index)
      stringBuilder.AppendFormat("{0:X2}", (object) bytes[index]);
    return stringBuilder.ToString();
  }

  public static string ByteToString(byte[] data)
  {
    return data != null ? PdfString.ByteToString(data, data.Length) : throw new ArgumentNullException("stream");
  }

  internal static string ByteToString(byte[] data, int length)
  {
    if (data == null)
      throw new ArgumentNullException("stream");
    if (length > data.Length)
      throw new ArgumentOutOfRangeException(nameof (length), "The length can't be more then the array lenght.");
    char[] chArray = new char[length];
    int index1 = 0;
    for (int index2 = length; index1 < index2; ++index1)
      chArray[index1] = (char) data[index1];
    return new string(chArray);
  }

  public IPdfPrimitive Clone(PdfCrossTable crossTable)
  {
    if (this.m_clonedObject != null && this.m_clonedObject.CrossTable == crossTable)
      return (IPdfPrimitive) this.m_clonedObject;
    this.m_clonedObject = (PdfString) null;
    PdfString pdfString = new PdfString(this.m_value);
    pdfString.Encode = this.m_bForceEncoding;
    pdfString.Converted = this.m_bConverted;
    pdfString.m_bHex = this.m_bHex;
    pdfString.m_crossTable = crossTable;
    this.m_clonedObject = pdfString;
    return (IPdfPrimitive) pdfString;
  }

  public void Decrypt(PdfEncryptor encryptor, long currObjNumber)
  {
    if (encryptor == null || this.m_bDecrypted || this.IsParentDecrypted)
      return;
    this.m_bDecrypted = true;
    this.Value = PdfString.ByteToString(this.Bytes);
    this.Value = PdfString.ByteToString(encryptor.EncryptData(currObjNumber, this.Bytes, false));
    string str1 = PdfString.ByteToString(Encoding.Unicode.GetPreamble());
    string str2 = PdfString.ByteToString(Encoding.BigEndianUnicode.GetPreamble());
    if (this.Value.Length <= 1)
      return;
    if (this.Value.Substring(0, 2).Equals(str1))
    {
      this.ProcessUnicodeWithPreamble(ref this.m_value, Encoding.Unicode);
    }
    else
    {
      if (!this.Value.Substring(0, 2).Equals(str2))
        return;
      this.ProcessUnicodeWithPreamble(ref this.m_value, Encoding.BigEndianUnicode);
    }
  }

  private byte[] EncryptIfNeeded(byte[] data, PdfDocumentBase document)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    PdfSecurity security = document == null ? (PdfSecurity) null : document.Security;
    if (security == null || !security.Enabled)
      return data;
    long objNum = document.CurrentSavingObj.ObjNum;
    data = security.Encryptor.EncryptData(objNum, data, true);
    return PdfString.EscapeSymbols(data);
  }

  internal static byte[] EscapeSymbols(byte[] data)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    MemoryStream memoryStream = new MemoryStream();
    int index = 0;
    for (int length = data.Length; index < length; ++index)
    {
      byte num = data[index];
      switch (num)
      {
        case 13:
          memoryStream.WriteByte((byte) 92);
          memoryStream.WriteByte((byte) 114);
          break;
        case 40:
        case 41:
          memoryStream.WriteByte((byte) 92);
          memoryStream.WriteByte(num);
          break;
        case 62:
        case 92:
          memoryStream.WriteByte((byte) 92);
          memoryStream.WriteByte(num);
          break;
        default:
          memoryStream.WriteByte(num);
          break;
      }
    }
    byte[] bytes = PdfStream.StreamToBytes((Stream) memoryStream);
    memoryStream.Dispose();
    return bytes;
  }

  internal static byte[] EscapeSymbols(byte[] data, bool isFormField)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    MemoryStream memoryStream = new MemoryStream();
    int index = 0;
    for (int length = data.Length; index < length; ++index)
    {
      byte num = data[index];
      switch (num)
      {
        case 13:
          continue;
        case 40:
        case 41:
          memoryStream.WriteByte((byte) 92);
          memoryStream.WriteByte(num);
          continue;
        case 62:
        case 92:
          memoryStream.WriteByte((byte) 92);
          memoryStream.WriteByte(num);
          continue;
        default:
          memoryStream.WriteByte(num);
          continue;
      }
    }
    byte[] bytes = PdfStream.StreamToBytes((Stream) memoryStream);
    memoryStream.Dispose();
    return bytes;
  }

  public static string FromDate(DateTime dateTime) => dateTime.ToString("D:yyyyMMddHHmmss");

  private static byte[] GetAsciiBytes(string value)
  {
    byte[] asciiBytes = value != null ? new byte[value.Length] : throw new ArgumentNullException(nameof (value));
    int index = 0;
    for (int length = value.Length; index < length; ++index)
      asciiBytes[index] = (byte) value[index];
    return asciiBytes;
  }

  private byte[] GetBytes()
  {
    bool unicode = !this.Converted && PdfString.IsUnicode(this.m_value);
    if (this.Encode == PdfString.ForceEncoding.ASCII)
      unicode = false;
    else if (this.Encode == PdfString.ForceEncoding.Unicode)
      unicode = true;
    return this.GetBytes(unicode);
  }

  private byte[] GetBytes(bool unicode)
  {
    return !unicode ? PdfString.GetAsciiBytes(this.m_value) : PdfString.ToUnicodeArray(this.m_value, !this.Converted);
  }

  private byte[] HexDigitsToNumbers(List<byte> hexNumbers)
  {
    byte num = 0;
    bool flag = true;
    List<byte> byteList = new List<byte>(hexNumbers.Count / 2 + 1);
    foreach (byte hexNumber in hexNumbers)
    {
      if (flag)
      {
        num = (byte) ((uint) hexNumber << 4);
        flag = false;
      }
      else
      {
        num += hexNumber;
        byteList.Add(num);
        flag = true;
      }
    }
    if (!flag)
      byteList.Add(num);
    return byteList.ToArray();
  }

  public byte[] HexToBytes(string value)
  {
    List<byte> hexNumbers = new List<byte>(value.Length);
    foreach (char c in value)
    {
      if (char.IsLetterOrDigit(c))
      {
        byte hex = this.ParseHex(c);
        hexNumbers.Add(hex);
      }
    }
    return this.HexDigitsToNumbers(hexNumbers);
  }

  public static bool IsUnicode(string value)
  {
    if (value == null)
      throw new ArgumentNullException(nameof (value));
    return Encoding.UTF8.GetByteCount(value) != value.Length;
  }

  public static explicit operator PdfString(string str)
  {
    return str != null ? new PdfString(str) : throw new ArgumentNullException(nameof (str));
  }

  private byte ParseHex(char c)
  {
    byte hex = 0;
    if (c >= '0' && c <= '9')
      return (byte) ((uint) c - 48U /*0x30*/);
    if (c >= 'A' && c <= 'F')
      return (byte) ((int) c - 65 + 10);
    if (c >= 'a' && c <= 'f')
      hex = (byte) ((int) c - 97 + 10);
    return hex;
  }

  internal byte[] PdfEncode(PdfDocumentBase document)
  {
    byte[] numArray1 = (byte[]) null;
    byte[] data;
    if (!this.Hex)
    {
      data = this.GetBytes();
      PdfSecurity security = document == null ? (PdfSecurity) null : document.Security;
      if (security == null || !security.Enabled)
      {
        StringBuilder stringBuilder = new StringBuilder();
        if (data.Length > 10)
        {
          for (int index = 0; index < 10; ++index)
            stringBuilder.Append(Convert.ToChar(data[index]));
        }
        if (stringBuilder.ToString() == "ColorFound")
        {
          byte[] numArray2 = new byte[data.Length - 10];
          for (int index = 0; index < data.Length - 10; ++index)
            numArray2[index] = data[index + 10];
          numArray1 = new byte[data.Length - 10];
          data = numArray2;
        }
        else
          data = this.IsFormField ? PdfString.EscapeSymbols(data, this.IsFormField) : PdfString.EscapeSymbols(data);
      }
    }
    else
      data = this.m_data != null ? PdfString.GetAsciiBytes(PdfString.BytesToHex(this.m_data)) : PdfString.GetAsciiBytes(this.Value);
    MemoryStream memoryStream = new MemoryStream(data.Length + 2);
    string str = this.Hex ? "<>" : "()";
    bool flag = false;
    byte[] numArray3 = this.EncryptIfNeeded(data, document);
    for (int index = 0; index < numArray3.Length; ++index)
      flag = numArray3[index] >= (byte) 48 /*0x30*/ && numArray3[index] <= (byte) 57 || numArray3[index] >= (byte) 65 && numArray3[index] <= (byte) 70 || numArray3[index] >= (byte) 97 && numArray3[index] <= (byte) 102;
    if (this.Hex && !flag)
      numArray3 = PdfString.GetAsciiBytes(PdfString.BytesToHex(numArray3));
    memoryStream.WriteByte((byte) str[0]);
    memoryStream.Write(numArray3, 0, numArray3.Length);
    memoryStream.WriteByte((byte) str[1]);
    byte[] array = memoryStream.ToArray();
    memoryStream.Dispose();
    return array;
  }

  private void ProcessUnicodeWithPreamble(ref string text, Encoding encoding)
  {
    byte[] numArray = PdfString.StringToByte(text.Substring(2));
    for (int index1 = 0; index1 < numArray.Length - 1; ++index1)
    {
      if (numArray[index1] == (byte) 92 && (numArray[index1 + 1] == (byte) 40 || numArray[index1 + 1] == (byte) 41 || numArray[index1 + 1] == (byte) 13 || numArray[index1 + 1] == (byte) 62 || numArray[index1 + 1] == (byte) 92))
      {
        for (int index2 = index1; index2 < numArray.Length - 1; ++index2)
          numArray[index2] = numArray[index2 + 1];
        byte[] dst = new byte[numArray.Length - 1];
        Buffer.BlockCopy((Array) numArray, 0, (Array) dst, 0, numArray.Length - 1);
        numArray = dst;
        --index1;
      }
    }
    text = encoding.GetString(numArray, 0, numArray.Length);
  }

  public void Save(IPdfWriter writer)
  {
    if (writer == null)
      throw new ArgumentNullException(nameof (writer));
    writer.Write(this.PdfEncode(writer.Document));
  }

  internal static byte[] StringToByte(string data) => PdfString.GetAsciiBytes(data);

  internal void ToHex()
  {
    if (this.Hex)
      return;
    this.Value = PdfString.BytesToHex(this.GetBytes());
    this.m_bHex = true;
  }

  public static byte[] ToUnicodeArray(string value, bool bAddPrefix)
  {
    int length = value != null ? Encoding.BigEndianUnicode.GetByteCount(value) : throw new ArgumentNullException(nameof (value));
    byte[] numArray = (byte[]) null;
    int byteIndex = 0;
    if (bAddPrefix)
    {
      numArray = Encoding.BigEndianUnicode.GetPreamble();
      byteIndex = numArray.Length;
      length += byteIndex;
    }
    byte[] bytes = new byte[length];
    if (bAddPrefix)
      numArray.CopyTo((Array) bytes, 0);
    Encoding.BigEndianUnicode.GetBytes(value, 0, value.Length, bytes, byteIndex);
    return bytes;
  }

  internal byte[] Bytes
  {
    get
    {
      if (this.m_data == null)
        this.m_data = this.GetBytes();
      return this.m_data;
    }
  }

  public IPdfPrimitive ClonedObject => (IPdfPrimitive) this.m_clonedObject;

  internal bool Converted
  {
    get => this.m_bConverted;
    set
    {
      if (this.m_bHex)
        return;
      this.m_bConverted = value;
    }
  }

  internal PdfCrossTable CrossTable => this.m_crossTable;

  public bool Decrypted => this.m_bDecrypted;

  internal PdfString.ForceEncoding Encode
  {
    get => this.m_bForceEncoding;
    set => this.m_bForceEncoding = value;
  }

  internal bool Hex => this.m_bHex;

  internal bool IsPacked
  {
    get => this.m_isPacked;
    set => this.m_isPacked = value;
  }

  internal bool IsParentDecrypted
  {
    get => this.m_isParentDecrypted;
    set => this.m_isParentDecrypted = value;
  }

  public bool IsSaving
  {
    get => this.m_isSaving;
    set => this.m_isSaving = value;
  }

  public int ObjectCollectionIndex
  {
    get => this.m_index;
    set => this.m_index = value;
  }

  public int Position
  {
    get => this.m_position;
    set => this.m_position = value;
  }

  public ObjectStatus Status
  {
    get => this.m_status;
    set => this.m_status = value;
  }

  bool IPdfDecryptable.WasEncrypted
  {
    get => throw new Exception("The method or operation is not implemented.");
  }

  public string Value
  {
    get => this.m_value;
    set
    {
      if (!(value != this.m_value))
        return;
      this.m_value = value;
      this.m_data = (byte[]) null;
      this.Encode = PdfString.ForceEncoding.None;
    }
  }

  internal enum ForceEncoding
  {
    None,
    ASCII,
    Unicode,
  }

  private enum SourceType
  {
    StringValue,
    ByteBuffer,
  }
}
