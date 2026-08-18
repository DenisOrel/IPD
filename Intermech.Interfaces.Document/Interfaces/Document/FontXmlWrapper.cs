// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.FontXmlWrapper
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Drawing;
using System.Globalization;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Вспомогательный класс для XML сериализации класса Font</summary>
[Serializable]
public class FontXmlWrapper
{
  /// <summary>Имя шрифта</summary>
  public string Name;
  /// <summary>Размер шрифта</summary>
  public float Size;
  /// <summary>Стиль</summary>
  public FontStyle Style;
  /// <summary>Единицы измерения</summary>
  public GraphicsUnit Unit;
  /// <summary>Кодовая страница</summary>
  public byte GdiCharSet;
  /// <summary>Вертикальный текст</summary>
  public bool GdiVerticalFont;

  /// <summary>Конструктор</summary>
  /// <param name="font">Шрифт</param>
  public FontXmlWrapper(Font font)
  {
    if (font == null)
      return;
    this.Name = font.Name;
    this.Size = font.Size;
    this.Style = font.Style;
    this.Unit = font.Unit;
    this.GdiCharSet = font.GdiCharSet;
    this.GdiVerticalFont = font.GdiVerticalFont;
  }

  /// <summary>Преобразовать в класс Font</summary>
  /// <returns>Соответствующий экземляр Font</returns>
  public Font ToFont()
  {
    return this.Name == null ? (Font) null : new Font(this.Name, this.Size, this.Style, this.Unit, this.GdiCharSet, this.GdiVerticalFont);
  }

  /// <summary>Сохранить в XML</summary>
  /// <param name="xw">XmlWriter</param>
  public void WriteToXml(XmlWriter xw)
  {
    xw.WriteAttributeString("Name", this.Name);
    xw.WriteAttributeString("Size", this.Size.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    xw.WriteAttributeString("Style", this.Style.ToString());
    xw.WriteAttributeString("Unit", this.Unit.ToString());
    xw.WriteAttributeString("GdiCharSet", this.GdiCharSet.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    xw.WriteAttributeString("GdiVerticalFont", this.GdiVerticalFont.ToString((IFormatProvider) CultureInfo.InvariantCulture));
  }

  /// <summary>Загрузить из файла XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Загруженный экземпляр Font</returns>
  public static Font ReadFromXml(XmlReadArgs readArgs)
  {
    FontXmlWrapper fontXmlWrapper = new FontXmlWrapper(new Font("Tahoma", 8.25f));
    if (readArgs.Reader.HasAttributes)
    {
      int i = 0;
      for (int attributeCount = readArgs.Reader.AttributeCount; i < attributeCount; ++i)
      {
        readArgs.Reader.MoveToAttribute(i);
        if ("Name" == readArgs.Reader.LocalName)
          fontXmlWrapper.Name = readArgs.Reader.Value;
        else if ("Size" == readArgs.Reader.LocalName)
          fontXmlWrapper.Size = float.Parse(DocumentTreeNode.ReplaceDS(readArgs.Reader.Value), (IFormatProvider) CultureInfo.InvariantCulture);
        else if ("Style" == readArgs.Reader.LocalName)
          fontXmlWrapper.Style = (FontStyle) Enum.Parse(typeof (FontStyle), readArgs.Reader.Value);
        else if ("Unit" == readArgs.Reader.LocalName)
          fontXmlWrapper.Unit = (GraphicsUnit) Enum.Parse(typeof (GraphicsUnit), readArgs.Reader.Value);
        else if ("GdiCharSet" == readArgs.Reader.LocalName)
          fontXmlWrapper.GdiCharSet = byte.Parse(readArgs.Reader.Value);
        else if ("GdiVerticalFont" == readArgs.Reader.LocalName)
          fontXmlWrapper.GdiVerticalFont = bool.Parse(readArgs.Reader.Value);
      }
      readArgs.Reader.MoveToElement();
    }
    return fontXmlWrapper.ToFont();
  }
}
