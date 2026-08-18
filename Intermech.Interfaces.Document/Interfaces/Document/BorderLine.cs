// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.BorderLine
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Класс описывающий линию границы элемента</summary>
[TypeConverter(typeof (BorderLineConverter))]
[Serializable]
public class BorderLine : IWriteReadXml, ICloneable
{
  private Color color = Color.Black;
  private BorderStyles style = BorderStyles.SolidLine;
  private float width;
  private float serifWidth = 1.5f;

  /// <summary>Создать линию с параметрами по умолчанию</summary>
  public BorderLine()
  {
    this.style = BorderStyles.SolidLine;
    this.width = 0.0f;
    this.color = Color.Black;
  }

  /// <summary>Создать линию с заданными параметрами</summary>
  /// <param name="color">Цвет</param>
  /// <param name="style">Стиль</param>
  /// <param name="width">Толщина. Если толщина 0, то используется толщина линии по умолчанию</param>
  public BorderLine(Color color, BorderStyles style, float width)
  {
    this.style = style;
    this.width = width;
    this.color = color;
  }

  /// <summary>Создать линию с заданными параметрами</summary>
  /// <param name="color">Цвет</param>
  /// <param name="style">Стиль</param>
  /// <param name="width">Толщина. Если толщина 0, то используется толщина линии по умолчанию</param>
  /// <param name="serifWidth">Длинв штриха в миллиметрах, для стиля "один штрих в начале"</param>
  public BorderLine(Color color, BorderStyles style, float width, float serifWidth)
  {
    this.style = style;
    this.width = width;
    this.color = color;
    this.serifWidth = serifWidth;
  }

  /// <summary>Создать линию с заданными параметрами</summary>
  /// <param name="width">Толщина. Если толщина 0, то используется толщина линии по умолчанию</param>
  public BorderLine(float width)
  {
    this.style = BorderStyles.SolidLine;
    this.width = width;
    this.color = Color.Black;
  }

  /// <summary>Создать линию заданного стиля</summary>
  /// <param name="style">Стиль</param>
  public BorderLine(BorderStyles style)
  {
    this.style = style;
    this.width = 0.0f;
    this.color = Color.Black;
  }

  /// <summary>Создать линию заданного стиля</summary>
  /// <param name="style">Стиль</param>
  /// <param name="width">Толщина. Если толщина 0, то используется толщина линии по умолчанию</param>
  public BorderLine(BorderStyles style, float width)
  {
    this.style = style;
    this.width = width;
    this.color = Color.Black;
  }

  /// <summary>Создать полную копию линии</summary>
  /// <returns>Возвращает полную копию линии</returns>
  public BorderLine Clone() => new BorderLine(this.color, this.style, this.width, this.serifWidth);

  /// <summary>Создать полную копию линии</summary>
  /// <returns>Возвращает полную копию линии</returns>
  object ICloneable.Clone() => (object) this.Clone();

  public override string ToString()
  {
    TypeConverter converter = TypeDescriptor.GetConverter(this.Style.GetType());
    return $"{$"{this.Color.ToString()},{converter.ConvertToString((object) this.Style)}"},{this.Width.ToString()}";
  }

  /// <summary>Цвет</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_10")]
  [CustomDescription("Attribute.Interfaces.Document_11")]
  [CustomCategory("Attribute.Interfaces.Document_12")]
  public virtual Color Color
  {
    [DebuggerStepThrough] get => this.color;
    set
    {
      if (!(this.color != value))
        return;
      this.color = value;
    }
  }

  /// <summary>Стиль</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_13")]
  [CustomDescription("Attribute.Interfaces.Document_14")]
  [CustomCategory("Attribute.Interfaces.Document_15")]
  public virtual BorderStyles Style
  {
    [DebuggerStepThrough] get => this.style;
    set
    {
      if (this.style == value)
        return;
      this.style = value;
    }
  }

  /// <summary>Толщина линии в миллиметрах. 0 означает толщину по умолчанию</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_16")]
  [CustomDescription("Attribute.Interfaces.Document_17")]
  [CustomCategory("Attribute.Interfaces.Document_18")]
  [TypeConverter(typeof (FloatConverter))]
  public virtual float Width
  {
    [DebuggerStepThrough] get => this.width;
    set
    {
      if ((double) this.width == (double) value)
        return;
      this.width = value;
    }
  }

  /// <summary>
  /// Длина штриха в миллиметрах при стиле линии "Один штрих в начале"
  /// </summary>
  [CustomDisplayName("Attribute.Interfaces.Document_611")]
  [CustomDescription("Attribute.Interfaces.Document_612")]
  [CustomCategory("Attribute.Interfaces.Document_18")]
  [TypeConverter(typeof (FloatConverter))]
  public virtual float SerifWidth
  {
    [DebuggerStepThrough] get => this.serifWidth;
    set
    {
      if ((double) this.serifWidth == (double) value)
        return;
      this.serifWidth = value;
    }
  }

  /// <summary>Ненулевая толщина линии. Значение 0, заменяется на значение по умолчанию</summary>
  internal float WidthNot0
  {
    [DebuggerStepThrough] get
    {
      return (double) this.width != 0.0 ? this.width : PageElementNode.DefaultLineWidth;
    }
  }

  /// <summary>Преобразовать стиль границы в стиль линии</summary>
  /// <param name="style">Стиль границы</param>
  /// <returns>Стиль линии</returns>
  public static DashStyle ConvertToDashStyle(BorderStyles style)
  {
    switch (style)
    {
      case BorderStyles.SolidLine:
        return DashStyle.Solid;
      case BorderStyles.Dash:
        return DashStyle.Dash;
      case BorderStyles.DashDot:
        return DashStyle.DashDot;
      case BorderStyles.DashDotDot:
        return DashStyle.DashDotDot;
      case BorderStyles.Dot:
        return DashStyle.Dot;
      case BorderStyles.Serif:
        return DashStyle.Solid;
      default:
        return DashStyle.Solid;
    }
  }

  /// <summary>Стиль штрихов линии</summary>
  protected DashStyle PenDashStyle
  {
    [DebuggerStepThrough] get => BorderLine.ConvertToDashStyle(this.style);
  }

  /// <summary>Получить объект Pen соответствующий стилю линии, для отрисовки в миллиметрах</summary>
  /// <returns>Возвращает объект Pen соответствующий стилю линии</returns>
  public Pen GetPen()
  {
    if (this.style == BorderStyles.None)
      return (Pen) null;
    float width = this.width;
    if ((double) width == 0.0)
      width = PageElementNode.DefaultLineWidth;
    return new Pen(this.color, width)
    {
      DashStyle = this.PenDashStyle,
      EndCap = LineCap.Square,
      StartCap = LineCap.Square
    };
  }

  /// <summary>Записать поля в XML</summary>
  /// <param name="elementName">Имя элемента XML, под которым нужно сохранить данные</param>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public virtual void WriteToXml(string elementName, XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    xw.WriteStartElement(elementName);
    xw.WriteAttributeString("color", DocumentTreeNode.ColorConverter.ConvertToInvariantString((object) this.color));
    xw.WriteAttributeString("style", this.style.ToString());
    xw.WriteAttributeString("width", this.width.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    xw.WriteAttributeString("serifwidth", this.serifWidth.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    xw.WriteEndElement();
  }

  /// <summary>Прочитать поле из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле было прочитано</returns>
  public virtual bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    switch (readArgs.Reader.LocalName)
    {
      case "style":
        this.style = (BorderStyles) Enum.Parse(typeof (BorderStyles), readArgs.Reader.Value);
        return true;
      case "width":
        this.width = float.Parse(DocumentTreeNode.ReplaceDS(readArgs.Reader.Value), (IFormatProvider) CultureInfo.InvariantCulture);
        return true;
      case "serifwidth":
        this.serifWidth = float.Parse(DocumentTreeNode.ReplaceDS(readArgs.Reader.Value), (IFormatProvider) CultureInfo.InvariantCulture);
        return true;
      case "color":
        this.color = readArgs.Version >= 11 ? (Color) DocumentTreeNode.ColorConverter.ConvertFromInvariantString(readArgs.Reader.Value) : Color.FromName(readArgs.Reader.Value);
        return true;
      default:
        return false;
    }
  }

  /// <summary>Загрузить из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public virtual void ReadFromXml(XmlReadArgs readArgs)
  {
    WriteReadXmlHelper.ReadFromXml((IWriteReadXml) this, readArgs);
  }

  /// <summary>Проверяет равенство объектов</summary>
  /// <param name="obj">Объект с которым сравнивать</param>
  /// <returns>true, если объекты эквивалентны</returns>
  public override bool Equals(object obj)
  {
    if (obj == null)
      return false;
    if (this == obj)
      return true;
    if (!(this.GetType() == obj.GetType()))
      return base.Equals(obj);
    BorderLine borderLine = (BorderLine) obj;
    return this.color == borderLine.color && this.style == borderLine.style && (double) this.width == (double) borderLine.width && (double) this.serifWidth == (double) borderLine.serifWidth;
  }

  /// <summary>Получить хэш код объекта</summary>
  /// <returns>Хэш код объекта</returns>
  public override int GetHashCode()
  {
    int hashCode1 = this.color.GetHashCode();
    int hashCode2 = this.style.GetHashCode();
    int hashCode3 = this.width.GetHashCode();
    int hashCode4 = this.serifWidth.GetHashCode();
    int num = hashCode2 << 13 | hashCode2 >> 19;
    return hashCode1 ^ num ^ (hashCode3 << 26 | hashCode3 >> 6) ^ (hashCode4 << 5 | hashCode4 >> 27);
  }
}
