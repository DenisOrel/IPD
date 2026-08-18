
// Type: Intermech.Client.Core.Show.Net.ShowNew.Shape.TextData
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;


namespace Intermech.Client.Core.Show.Net.ShowNew.Shape;

internal sealed class TextData
{
  private string _familyNameFont = string.Empty;
  private FontStyle _fontStyle;
  private Encoding _encodingText = Encoding.GetEncoding(1252);
  private double _rotateFont;
  private double _sizeFont;
  private double _widthFont;
  private static List<string> FamilyNames = new List<string>();

  static TextData()
  {
    foreach (FontFamily family in FontFamily.Families)
      TextData.FamilyNames.Add(family.Name);
  }

  internal string FamilyNameFont => this._familyNameFont;

  public FontStyle FontStyle => this._fontStyle;

  internal Encoding EncodingText => this._encodingText;

  internal double RotateFont => this._rotateFont;

  internal double SizeFont => this._sizeFont;

  internal double WidthFont => this._widthFont;

  internal void ReadNameFont(ConvertStream buf)
  {
    this._familyNameFont = buf.ReadStringAnsii(buf.ReadBytes((int) buf.ReadByte()));
    string str1 = TextData.FamilyNames.Find((Predicate<string>) (x => string.Compare(x, this._familyNameFont, true) == 0));
    if (str1 != null)
    {
      this._familyNameFont = str1;
    }
    else
    {
      string str2 = "Arial";
      (ServicesManager.GetService(typeof (IOutputView)) as IOutputView).WriteString("Show", $" Шрифт с именем '{this._familyNameFont}' не найден. замена на шрифт '{str2}'");
      this._familyNameFont = str2;
    }
  }

  internal void ReadRotateFontD(ConvertStream buf) => this._rotateFont = buf.ReadDouble();

  internal void ReadRotateFontShort(ConvertStream buf)
  {
    this._rotateFont = (double) buf.ReadInt16() / 10.0;
  }

  internal void ReadSizeFontD(ConvertStream buf) => this._sizeFont = buf.ReadDouble();

  internal void ReadSizeFontShort(ConvertStream buf, double scale)
  {
    this._sizeFont = (double) buf.ReadInt16() * scale;
  }

  internal void ReadWidthFontD(ConvertStream buf) => this._widthFont = buf.ReadDouble();

  internal void ReadWidthFontShort(ConvertStream buf, double scale)
  {
    this._widthFont = (double) buf.ReadUInt16() * scale;
  }

  internal void ReadFontStyle(ConvertStream buf) => this._fontStyle = (FontStyle) buf.ReadByte();

  internal void ReadCharSetFont(byte gdiCharSet)
  {
    switch (gdiCharSet)
    {
      case 0:
        this._encodingText = Encoding.GetEncoding(1252);
        break;
      case 163:
        this._encodingText = Encoding.GetEncoding(1252);
        break;
      case 204:
        this._encodingText = Encoding.GetEncoding(1251);
        break;
      default:
        this._encodingText = Encoding.GetEncoding(1251);
        break;
    }
  }

  internal void ReadColorFont(ConvertStream buf)
  {
    int num = (int) buf.ReadByte();
  }

  internal void ReadColorFont32(ConvertStream buf) => buf.ReadInt32();
}
