// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.IO.TokenType
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.IO;

internal enum TokenType
{
  Unknown,
  DictionaryStart,
  DictionaryEnd,
  StreamStart,
  StreamEnd,
  HexStringStart,
  HexStringEnd,
  String,
  UnicodeString,
  Number,
  Real,
  Name,
  ArrayStart,
  ArrayEnd,
  Reference,
  ObjectStart,
  ObjectEnd,
  Boolean,
  HexDigit,
  Eof,
  Trailer,
  StartXRef,
  XRef,
  Null,
  ObjectType,
  HexStringWeird,
  HexStringWeirdEscape,
  WhiteSpace,
}
