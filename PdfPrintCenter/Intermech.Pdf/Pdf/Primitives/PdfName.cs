// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Primitives.PdfName
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.IO;
using System;
using System.Text;

#nullable disable
namespace Syncfusion.Pdf.Primitives;

internal class PdfName : IPdfPrimitive
{
  public static string Delimiters = "()<>[]{}/%}";
  private int m_index;
  private bool m_isSaving;
  private int m_position;
  private static readonly char[] m_replacements = new char[4]
  {
    ' ',
    '\t',
    '\n',
    '\r'
  };
  private ObjectStatus m_status;
  private string m_value;
  internal const string StringStartMark = "/";

  public PdfName()
  {
    this.m_value = string.Empty;
    this.m_position = -1;
  }

  public PdfName(Enum value)
    : this(value.ToString())
  {
  }

  public PdfName(string value)
  {
    this.m_value = string.Empty;
    this.m_position = -1;
    this.m_value = value != null ? PdfName.NormalizeValue(value) : throw new ArgumentNullException(nameof (value));
  }

  public IPdfPrimitive Clone(PdfCrossTable crossTable)
  {
    return (IPdfPrimitive) new PdfName()
    {
      Value = this.m_value
    };
  }

  public override bool Equals(object obj)
  {
    PdfName pdfName = obj as PdfName;
    return !(pdfName == (PdfName) null) && pdfName.Value == this.Value;
  }

  public static string EscapeString(string str)
  {
    if (str == null)
      throw new ArgumentNullException(nameof (str));
    if (str == string.Empty)
      return str;
    StringBuilder stringBuilder = new StringBuilder();
    int index = 0;
    for (int length = str.Length; index < length; ++index)
    {
      char ch = str[index];
      PdfName.Delimiters.IndexOf(ch);
      switch (ch)
      {
        case '\n':
          stringBuilder.Append("\n");
          break;
        case '\r':
          stringBuilder.Append("\\r");
          break;
        case '(':
        case ')':
        case '\\':
          stringBuilder.Append(ch);
          break;
        default:
          stringBuilder.Append(ch);
          break;
      }
    }
    return stringBuilder.ToString();
  }

  public override int GetHashCode() => this.Value.GetHashCode();

  private static string NormalizeValue(string value)
  {
    string str = value;
    foreach (char replacement in PdfName.m_replacements)
      str = PdfName.NormalizeValue(str, replacement);
    return str;
  }

  private static string NormalizeValue(string value, char symbol)
  {
    return value.Replace(symbol.ToString(), $"#{(int) symbol:X}");
  }

  public static bool operator ==(PdfName name1, PdfName name2)
  {
    if (object.Equals((object) name1, (object) name2))
      return true;
    return !object.Equals((object) name1, (object) null) && !object.Equals((object) name2, (object) null) && name1.Value == name2.Value;
  }

  public static bool operator ==(PdfName name1, object name2)
  {
    object objA1 = (object) name1;
    if (objA1.Equals(name2))
      return true;
    if (object.Equals(objA1, (object) null) || object.Equals(name2, (object) null))
      return false;
    PdfName objA2 = name2 as PdfName;
    return !object.Equals((object) objA2, (object) null) && name1.Value == objA2.Value;
  }

  public static explicit operator PdfName(string str)
  {
    return str != null ? new PdfName(str) : throw new ArgumentNullException(nameof (str));
  }

  public static bool operator !=(PdfName name1, PdfName name2) => !(name1 == name2);

  public static bool operator !=(PdfName name1, object name2) => name1 != name2;

  public void Save(IPdfWriter writer)
  {
    if (writer == null)
      throw new ArgumentNullException(nameof (writer));
    writer.Write(this.ToString());
  }

  public override string ToString() => "/" + PdfName.EscapeString(this.Value);

  public IPdfPrimitive ClonedObject => (IPdfPrimitive) null;

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

  public string Value
  {
    get => this.m_value;
    set
    {
      if (!(value != this.m_value))
        return;
      string str = value;
      if (value != null && value.Length > 0)
        this.m_value = PdfName.NormalizeValue(value.Substring(0, 1) == "/" ? value.Substring(1) : value);
      else
        this.m_value = str;
    }
  }
}
