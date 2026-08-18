// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.CharFormat
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.ComponentModel;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary> Дескриптор шрифта </summary>
[TypeConverter(typeof (CharFormatConverter))]
[Serializable]
public class CharFormat : ICloneable, IWriteReadXml, IEquatable<CharFormat>
{
  private string fontFamily = "Arial";
  private float? fontSize = new float?(10f);
  private CharStyle charStyle;
  private Color? underlineColor;
  private CharStyle undefinedCharStyles;
  private int? zoom = new int?(100);
  private float? interval = new float?(100f);
  private float? displacement = new float?(100f);
  private byte gdiCharSet = 1;
  private Color? textColor = new Color?(Color.Black);
  private Color? textBkColor = new Color?(Color.White);

  /// <summary> Название шрифта </summary>
  [CustomDisplayName("Attribute.Interfaces.Document_26")]
  [CustomDescription("Attribute.Interfaces.Document_27")]
  [CustomCategory("Attribute.Interfaces.Document_28")]
  [TypeConverter(typeof (FontConverter.FontNameConverter))]
  public virtual string FontFamily
  {
    [DebuggerStepThrough] get
    {
      return this.fontFamily != null && this.fontFamily == "" ? TextData.DefaultCharFormat.FontFamily : this.fontFamily;
    }
    set => this.fontFamily = value;
  }

  /// <summary>Кодовая страница</summary>
  [Browsable(false)]
  public byte GdiCharSet
  {
    [DebuggerStepThrough] get => this.gdiCharSet;
    set => this.gdiCharSet = value;
  }

  /// <summary>Стиль символа</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_29")]
  [CustomDescription("Attribute.Interfaces.Document_30")]
  [CustomCategory("Attribute.Interfaces.Document_31")]
  [TypeConverter(typeof (EnumCustomConverter))]
  public BoldItalicStyle? BoldItalic
  {
    [DebuggerStepThrough] get
    {
      return (this.UndefinedCharStyles & (CharStyle.Bold | CharStyle.Italic)) != CharStyle.Regular ? new BoldItalicStyle?() : new BoldItalicStyle?((BoldItalicStyle) (this.charStyle & (CharStyle.Bold | CharStyle.Italic)));
    }
    set
    {
      if (value.HasValue)
      {
        this.charStyle = this.charStyle & ~(CharStyle.Bold | CharStyle.Italic) | (CharStyle) value.Value;
        this.UndefinedCharStyles &= ~(CharStyle.Bold | CharStyle.Italic);
      }
      else
      {
        this.charStyle &= ~(CharStyle.Bold | CharStyle.Italic);
        this.UndefinedCharStyles |= CharStyle.Bold | CharStyle.Italic;
      }
    }
  }

  /// <summary>Размер шрифта, в точках</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_32")]
  [CustomDescription("Attribute.Interfaces.Document_33")]
  [CustomCategory("Attribute.Interfaces.Document_34")]
  [TypeConverter(typeof (FontSizeConverter))]
  public float? FontSize
  {
    [DebuggerStepThrough] get => this.fontSize;
    set => this.fontSize = value;
  }

  /// <summary> Размер шрифта, в мм</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_35")]
  [CustomDescription("Attribute.Interfaces.Document_36")]
  [CustomCategory("Attribute.Interfaces.Document_37")]
  [TypeConverter(typeof (FontSizeMmConverter))]
  public float? FontSizeMm
  {
    [DebuggerStepThrough] get
    {
      return !this.fontSize.HasValue ? new float?() : new float?((float) Math.Round((double) UnitsConverter.PointToMm(this.fontSize.Value), 1));
    }
    set
    {
      float? fontSizeMm = this.FontSizeMm;
      float? nullable = value;
      if ((double) fontSizeMm.GetValueOrDefault() == (double) nullable.GetValueOrDefault() & fontSizeMm.HasValue == nullable.HasValue)
        return;
      if (!value.HasValue)
        this.fontSize = new float?();
      else
        this.fontSize = new float?(0.25f * (float) (int) Math.Round((double) UnitsConverter.MmToPointsF(value.Value) / 0.25));
    }
  }

  /// <summary> Цвет текста </summary>
  [Browsable(false)]
  public Color? TextColor
  {
    [DebuggerStepThrough] get => this.textColor;
    set => this.textColor = value;
  }

  /// <summary> Цвет текста </summary>
  [CustomDisplayName("Attribute.Interfaces.Document_38")]
  [CustomDescription("Attribute.Interfaces.Document_39")]
  [CustomCategory("Attribute.Interfaces.Document_40")]
  public Color? TextColorForUser
  {
    [DebuggerStepThrough] get
    {
      if (!this.textColor.HasValue)
        return new Color?();
      return !this.textColor.Value.IsEmpty ? new Color?(this.textColor.Value) : new Color?(Color.Black);
    }
    set => this.textColor = value;
  }

  /// <summary>Цвет фона текста</summary>
  [Browsable(false)]
  public Color? TextBkColor
  {
    [DebuggerStepThrough] get => this.textBkColor;
    set => this.textBkColor = value;
  }

  /// <summary> Цвет текста </summary>
  [CustomDisplayName("Attribute.Interfaces.Document_526")]
  [CustomDescription("Attribute.Interfaces.Document_526")]
  [CustomCategory("Attribute.Interfaces.Document_40")]
  public Color? TextBkColorForUser
  {
    [DebuggerStepThrough] get
    {
      if (!this.textBkColor.HasValue)
        return new Color?();
      return !this.textBkColor.Value.IsEmpty ? new Color?(this.textBkColor.Value) : new Color?(Color.White);
    }
    set => this.textBkColor = value;
  }

  /// <summary> Стиль подчёркивания </summary>
  [CustomDisplayName("Attribute.Interfaces.Document_41")]
  [CustomDescription("Attribute.Interfaces.Document_42")]
  [CustomCategory("Attribute.Interfaces.Document_43")]
  [TypeConverter(typeof (EnumCustomConverter))]
  public UnderlineStyle? Underline
  {
    [DebuggerStepThrough] get
    {
      return (this.UndefinedCharStyles & (CharStyle.Underline | CharStyle.DoubleUnderline)) != CharStyle.Regular ? new UnderlineStyle?() : new UnderlineStyle?((UnderlineStyle) (this.charStyle & (CharStyle.Underline | CharStyle.DoubleUnderline)));
    }
    set
    {
      if (value.HasValue)
      {
        this.charStyle = this.charStyle & ~(CharStyle.Underline | CharStyle.DoubleUnderline) | (CharStyle) value.Value;
        this.UndefinedCharStyles &= ~(CharStyle.Underline | CharStyle.DoubleUnderline);
      }
      else
      {
        this.charStyle &= ~(CharStyle.Underline | CharStyle.DoubleUnderline);
        this.UndefinedCharStyles |= CharStyle.Underline | CharStyle.DoubleUnderline;
      }
    }
  }

  /// <summary> Стиль зачеркивания </summary>
  [CustomDisplayName("Attribute.Interfaces.Document_44")]
  [CustomDescription("Attribute.Interfaces.Document_45")]
  [CustomCategory("Attribute.Interfaces.Document_46")]
  [TypeConverter(typeof (EnumCustomConverter))]
  public StrikeoutLineStyle? Strike
  {
    [DebuggerStepThrough] get
    {
      return (this.UndefinedCharStyles & (CharStyle.Strikethrough | CharStyle.DoubleStrikethrough)) != CharStyle.Regular ? new StrikeoutLineStyle?() : new StrikeoutLineStyle?((StrikeoutLineStyle) (this.charStyle & (CharStyle.Strikethrough | CharStyle.DoubleStrikethrough)));
    }
    set
    {
      if (value.HasValue)
      {
        this.charStyle = this.charStyle & ~(CharStyle.Strikethrough | CharStyle.DoubleStrikethrough) | (CharStyle) value.Value;
        this.UndefinedCharStyles &= ~(CharStyle.Strikethrough | CharStyle.DoubleStrikethrough);
      }
      else
      {
        this.charStyle &= ~(CharStyle.Strikethrough | CharStyle.DoubleStrikethrough);
        this.UndefinedCharStyles |= CharStyle.Strikethrough | CharStyle.DoubleStrikethrough;
      }
    }
  }

  /// <summary> Все Прописные </summary>
  [Browsable(false)]
  public bool? AllCaps
  {
    [DebuggerStepThrough] get
    {
      return (this.UndefinedCharStyles & CharStyle.AllCaps) != CharStyle.Regular ? new bool?() : new bool?((this.charStyle & CharStyle.AllCaps) != 0);
    }
    set
    {
      if (value.HasValue)
      {
        if (value.Value)
        {
          this.charStyle |= CharStyle.AllCaps;
          this.AllSmallCaps = new bool?(false);
        }
        else
          this.charStyle &= ~CharStyle.AllCaps;
        this.UndefinedCharStyles &= ~CharStyle.AllCaps;
      }
      else
        this.UndefinedCharStyles |= CharStyle.AllCaps;
    }
  }

  /// <summary> Малые прописные </summary>
  [Browsable(false)]
  public bool? AllSmallCaps
  {
    [DebuggerStepThrough] get
    {
      return (this.UndefinedCharStyles & CharStyle.AllSmallCaps) != CharStyle.Regular ? new bool?() : new bool?((this.charStyle & CharStyle.AllSmallCaps) != 0);
    }
    set
    {
      if (value.HasValue)
      {
        if (value.Value)
        {
          this.charStyle |= CharStyle.AllSmallCaps;
          this.AllCaps = new bool?(false);
        }
        else
          this.charStyle &= ~CharStyle.AllSmallCaps;
        this.UndefinedCharStyles &= ~CharStyle.AllSmallCaps;
      }
      else
        this.UndefinedCharStyles |= CharStyle.AllSmallCaps;
    }
  }

  /// <summary>Подстрочный </summary>
  [Browsable(false)]
  public bool? Subscript
  {
    [DebuggerStepThrough] get
    {
      return (this.UndefinedCharStyles & CharStyle.Subscript) != CharStyle.Regular ? new bool?() : new bool?((this.charStyle & CharStyle.Subscript) != 0);
    }
    set
    {
      if (value.HasValue)
      {
        if (value.Value)
        {
          this.charStyle |= CharStyle.Subscript;
          this.Superscript = new bool?(false);
        }
        else
          this.charStyle &= ~CharStyle.Subscript;
        this.UndefinedCharStyles &= ~CharStyle.Subscript;
      }
      else
        this.UndefinedCharStyles |= CharStyle.Subscript;
    }
  }

  /// <summary>Надстрочный </summary>
  [Browsable(false)]
  public bool? Superscript
  {
    [DebuggerStepThrough] get
    {
      return (this.UndefinedCharStyles & CharStyle.Superscript) != CharStyle.Regular ? new bool?() : new bool?((this.charStyle & CharStyle.Superscript) != 0);
    }
    set
    {
      if (value.HasValue)
      {
        if (value.Value)
        {
          this.charStyle |= CharStyle.Superscript;
          this.Subscript = new bool?(false);
        }
        else
          this.charStyle &= ~CharStyle.Superscript;
        this.UndefinedCharStyles &= ~CharStyle.Superscript;
      }
      else
        this.UndefinedCharStyles |= CharStyle.Superscript;
    }
  }

  /// <summary>Скрытый </summary>
  [Browsable(false)]
  public bool? HiddenText
  {
    [DebuggerStepThrough] get
    {
      return (this.UndefinedCharStyles & CharStyle.HiddenText) != CharStyle.Regular ? new bool?() : new bool?((this.charStyle & CharStyle.HiddenText) != 0);
    }
    set
    {
      if (value.HasValue)
      {
        if (value.Value)
          this.charStyle |= CharStyle.HiddenText;
        else
          this.charStyle &= ~CharStyle.HiddenText;
        this.UndefinedCharStyles &= ~CharStyle.HiddenText;
      }
      else
        this.UndefinedCharStyles |= CharStyle.HiddenText;
    }
  }

  /// <summary> Цвет подчёркивания. Если равен null, то цвет подчёркивания = цвету текста </summary>
  [CustomDisplayName("Attribute.Interfaces.Document_47")]
  [CustomDescription("Attribute.Interfaces.Document_48")]
  [CustomCategory("Attribute.Interfaces.Document_49")]
  public Color? UnderlineColor
  {
    [DebuggerStepThrough] get => this.underlineColor;
    set => this.underlineColor = value;
  }

  /// <summary> Модификаторы </summary>
  [Browsable(false)]
  public CharStyle CharStyle
  {
    [DebuggerStepThrough] get => this.charStyle;
    set => this.charStyle = value;
  }

  /// <summary>
  /// Модификаторы у которых неопределены значения.
  /// Например, в том случае, если выбран и зачёркнутый и не зачёркнутый текст, то в данной структуре будет установлен флаг BOLD
  /// </summary>
  [Browsable(false)]
  public CharStyle UndefinedCharStyles
  {
    [DebuggerStepThrough] get => this.undefinedCharStyles;
    set => this.undefinedCharStyles = value;
  }

  /// <summary> Масштаб </summary>
  [CustomDisplayName("Attribute.Interfaces.Document_50")]
  [CustomDescription("Attribute.Interfaces.Document_51")]
  [CustomCategory("Attribute.Interfaces.Document_52")]
  [Browsable(false)]
  public int? Zoom
  {
    [DebuggerStepThrough] get => this.zoom;
    set => this.zoom = value;
  }

  /// <summary> Интервал </summary>
  [CustomDisplayName("Attribute.Interfaces.Document_53")]
  [CustomDescription("Attribute.Interfaces.Document_54")]
  [CustomCategory("Attribute.Interfaces.Document_55")]
  [Browsable(false)]
  [TypeConverter(typeof (FloatConverter))]
  public float? Interval
  {
    [DebuggerStepThrough] get => this.interval;
    set => this.interval = value;
  }

  /// <summary> Смещение </summary>
  [CustomDisplayName("Attribute.Interfaces.Document_56")]
  [CustomDescription("Attribute.Interfaces.Document_57")]
  [CustomCategory("Attribute.Interfaces.Document_58")]
  [Browsable(false)]
  [TypeConverter(typeof (FloatConverter))]
  public float? Displacement
  {
    [DebuggerStepThrough] get => this.displacement;
    set => this.displacement = value;
  }

  /// <summary> Создание копии </summary>
  /// <returns> Копия объекта </returns>
  object ICloneable.Clone() => (object) this.Clone();

  /// <summary>Конструктор</summary>
  public CharFormat()
  {
  }

  /// <summary>Конструктор</summary>
  public CharFormat(bool isAllАFieldsNull)
  {
    if (!isAllАFieldsNull)
      return;
    this.fontFamily = (string) null;
    this.fontSize = new float?();
    this.textColor = new Color?();
    this.textBkColor = new Color?();
    this.underlineColor = new Color?();
    this.BoldItalic = new BoldItalicStyle?();
    this.TextColorForUser = new Color?();
    this.TextBkColorForUser = new Color?();
    this.Underline = new UnderlineStyle?();
    this.UnderlineColor = new Color?();
    this.Strike = new StrikeoutLineStyle?();
    this.AllCaps = new bool?();
    this.AllSmallCaps = new bool?();
    this.HiddenText = new bool?();
    this.Subscript = new bool?();
    this.Superscript = new bool?();
  }

  /// <summary>Конструктор</summary>
  /// <param name="font">Шрифт на основе которого создать</param>
  public CharFormat(Font font)
  {
    this.fontFamily = font.Name;
    this.fontSize = new float?(font.SizeInPoints);
    this.charStyle = CharFormat.FontStyleToCharStyle(font.Style);
    this.gdiCharSet = font.GdiCharSet;
  }

  /// <summary>Конструктор</summary>
  /// <param name="fontFamily">Имя шрифта</param>
  /// <param name="fontSize">Размер шрифта</param>
  /// <param name="charStyle">Стиль шрифта</param>
  public CharFormat(string fontFamily, float fontSize, CharStyle charStyle)
  {
    this.fontFamily = fontFamily;
    this.fontSize = new float?(fontSize);
    this.charStyle = charStyle;
  }

  /// <summary>Преобразовать FontStyle в CharStyle</summary>
  /// <param name="fontStyle">FontStyle</param>
  /// <returns>CharStyle</returns>
  public static CharStyle FontStyleToCharStyle(FontStyle fontStyle)
  {
    CharStyle charStyle = CharStyle.Regular;
    if ((fontStyle & FontStyle.Bold) != FontStyle.Regular)
      charStyle |= CharStyle.Bold;
    if ((fontStyle & FontStyle.Italic) != FontStyle.Regular)
      charStyle |= CharStyle.Italic;
    if ((fontStyle & FontStyle.Strikeout) != FontStyle.Regular)
      charStyle |= CharStyle.Strikethrough;
    if ((fontStyle & FontStyle.Underline) != FontStyle.Regular)
      charStyle |= CharStyle.Underline;
    return charStyle;
  }

  /// <summary>Преобразовать FontStyle в CharStyle</summary>
  /// <param name="charStyle">CharStyle</param>
  /// <returns>FontStyle</returns>
  public static FontStyle CharStyleToFontStyle(CharStyle charStyle)
  {
    FontStyle fontStyle = FontStyle.Regular;
    if ((charStyle & CharStyle.Bold) != CharStyle.Regular)
      fontStyle |= FontStyle.Bold;
    if ((charStyle & CharStyle.Italic) != CharStyle.Regular)
      fontStyle |= FontStyle.Italic;
    if ((charStyle & CharStyle.Strikethrough) != CharStyle.Regular)
      fontStyle |= FontStyle.Strikeout;
    if ((charStyle & CharStyle.Underline) != CharStyle.Regular)
      fontStyle |= FontStyle.Underline;
    return fontStyle;
  }

  /// <summary> Создание копии </summary>
  /// <returns> Копия объекта </returns>
  public CharFormat Clone()
  {
    CharFormat charFormat = new CharFormat();
    charFormat.CopyParamsFrom(this);
    return charFormat;
  }

  /// <summary> Копирование параметров из некоторого другого объекта </summary>
  /// <param name="CharFormat"> Дексриптор фонта, параметры которго должны быть скопированны в данный </param>
  public void CopyParamsFrom(CharFormat CharFormat)
  {
    this.fontFamily = CharFormat != null ? CharFormat.fontFamily : throw new ArgumentNullException(nameof (CharFormat));
    this.fontSize = CharFormat.fontSize;
    this.charStyle = CharFormat.charStyle;
    this.textColor = CharFormat.textColor;
    this.textBkColor = CharFormat.textBkColor;
    this.underlineColor = CharFormat.underlineColor;
    this.zoom = CharFormat.zoom;
    this.interval = CharFormat.interval;
    this.displacement = CharFormat.displacement;
    this.gdiCharSet = CharFormat.gdiCharSet;
    this.undefinedCharStyles = CharFormat.undefinedCharStyles;
  }

  /// <summary>Получить соответствующий экземпляр класса Font.
  /// Учитываются поля fontFamily, fontSize, charStyle, gdiCharSet</summary>
  /// <returns>Соответствующий экземпляр класса Font</returns>
  public Font GetFont()
  {
    float emSize = 10f;
    if (this.fontSize.HasValue)
      emSize = this.fontSize.Value;
    Font font;
    try
    {
      font = new Font(this.FontFamily, emSize, CharFormat.CharStyleToFontStyle(this.charStyle), GraphicsUnit.Point, this.gdiCharSet);
    }
    catch (Exception ex)
    {
      try
      {
        font = new Font(this.FontFamily, emSize, FontStyle.Regular, GraphicsUnit.Point, this.gdiCharSet);
        this.charStyle = CharStyle.Regular;
        int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Interfaces.Document_2"), (object) ex.Message), LocalizationHolder.rm.GetString("Interfaces.Document_157"));
      }
      catch
      {
        try
        {
          font = new Font(TextData.DefaultCharFormat.FontFamily, emSize, CharFormat.CharStyleToFontStyle(this.charStyle), GraphicsUnit.Point, this.gdiCharSet);
          this.fontFamily = TextData.DefaultCharFormat.FontFamily;
          int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Interfaces.Document_2"), (object) ex.Message), LocalizationHolder.rm.GetString("Interfaces.Document_157"));
        }
        catch
        {
          font = new Font("Arial", emSize, CharFormat.CharStyleToFontStyle(this.charStyle), GraphicsUnit.Point, this.gdiCharSet);
          this.fontFamily = "Arial";
          int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Interfaces.Document_2"), (object) ex.Message), LocalizationHolder.rm.GetString("Interfaces.Document_157"));
        }
      }
    }
    return font;
  }

  public override bool Equals(object obj) => this.Equals(obj as CharFormat);

  public bool Equals(CharFormat other)
  {
    return other != null && this.fontFamily == other.fontFamily && EqualityComparer<float?>.Default.Equals(this.fontSize, other.fontSize) && this.charStyle == other.charStyle && EqualityComparer<Color?>.Default.Equals(this.underlineColor, other.underlineColor) && this.undefinedCharStyles == other.undefinedCharStyles && EqualityComparer<int?>.Default.Equals(this.zoom, other.zoom) && EqualityComparer<float?>.Default.Equals(this.interval, other.interval) && EqualityComparer<float?>.Default.Equals(this.displacement, other.displacement) && (int) this.gdiCharSet == (int) other.gdiCharSet && EqualityComparer<Color?>.Default.Equals(this.textColor, other.textColor) && EqualityComparer<Color?>.Default.Equals(this.textBkColor, other.textBkColor);
  }

  public override int GetHashCode()
  {
    return ((((((((((-1406924450 * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.fontFamily)) * -1521134295 + EqualityComparer<float?>.Default.GetHashCode(this.fontSize)) * -1521134295 + this.charStyle.GetHashCode()) * -1521134295 + EqualityComparer<Color?>.Default.GetHashCode(this.underlineColor)) * -1521134295 + this.undefinedCharStyles.GetHashCode()) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.zoom)) * -1521134295 + EqualityComparer<float?>.Default.GetHashCode(this.interval)) * -1521134295 + EqualityComparer<float?>.Default.GetHashCode(this.displacement)) * -1521134295 + this.gdiCharSet.GetHashCode()) * -1521134295 + EqualityComparer<Color?>.Default.GetHashCode(this.textColor)) * -1521134295 + EqualityComparer<Color?>.Default.GetHashCode(this.textBkColor);
  }

  /// <summary>Записать поля в XML</summary>
  /// <param name="elementName">Имя элемента XML, под которым нужно сохранить данные</param>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public void WriteToXml(string elementName, XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    xw.WriteStartElement(elementName);
    xw.WriteAttributeString("name", this.fontFamily);
    if (this.fontSize.HasValue)
      xw.WriteAttributeString("size", this.fontSize.Value.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    if (this.charStyle != CharStyle.Regular)
      xw.WriteAttributeString("style", ((int) this.charStyle).ToString((IFormatProvider) CultureInfo.InvariantCulture));
    Color empty;
    if (this.textColor.HasValue)
    {
      empty = this.textColor.Value;
      if (!empty.IsEmpty)
        xw.WriteAttributeString("color", DocumentTreeNode.ColorConverter.ConvertToInvariantString((object) this.textColor));
    }
    if (this.textBkColor.HasValue)
    {
      empty = this.textBkColor.Value;
      if (!empty.IsEmpty)
        xw.WriteAttributeString("bkColor", DocumentTreeNode.ColorConverter.ConvertToInvariantString((object) this.textBkColor));
    }
    if (this.underlineColor.HasValue)
    {
      Color? underlineColor = this.underlineColor;
      empty = Color.Empty;
      if ((underlineColor.HasValue ? (underlineColor.HasValue ? (underlineColor.GetValueOrDefault() != empty ? 1 : 0) : 0) : 1) != 0)
        xw.WriteAttributeString("underlineColor", DocumentTreeNode.ColorConverter.ConvertToInvariantString((object) this.underlineColor));
    }
    xw.WriteEndElement();
  }

  /// <summary>Загрузить из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public void ReadFromXml(XmlReadArgs readArgs)
  {
    if (readArgs == null)
      throw new ArgumentNullException(nameof (readArgs));
    string localName1 = readArgs.Reader.LocalName;
    if (readArgs.Reader.HasAttributes)
    {
      int attributeCount = readArgs.Reader.AttributeCount;
      int num1 = 0;
      bool flag = num1 == attributeCount;
      XmlReader reader = readArgs.Reader;
      int i1 = num1;
      int num2 = i1 + 1;
      reader.MoveToAttribute(i1);
      string localName2 = readArgs.Reader.LocalName;
      if (!flag && (readArgs.Version >= 20 && localName2 == "name" || readArgs.Version < 20 && localName2 == "fontName"))
      {
        this.fontFamily = readArgs.Reader.Value;
        if (num2 < attributeCount)
        {
          readArgs.Reader.MoveToAttribute(num2++);
          localName2 = readArgs.Reader.LocalName;
        }
        else
          flag = true;
      }
      if (!flag && (readArgs.Version >= 20 && localName2 == "size" || readArgs.Version < 20 && localName2 == "fontSize"))
      {
        this.fontSize = readArgs.Version >= 17 ? new float?(float.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture)) : new float?(float.Parse(DocumentTreeNode.ReplaceDS(readArgs.Reader.Value), (IFormatProvider) CultureInfo.InvariantCulture));
        if (num2 < attributeCount)
        {
          readArgs.Reader.MoveToAttribute(num2++);
          localName2 = readArgs.Reader.LocalName;
        }
        else
          flag = true;
      }
      if (!flag && (readArgs.Version >= 20 && localName2 == "style" || readArgs.Version < 20 && localName2 == "charStyle"))
      {
        this.charStyle = (CharStyle) int.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture);
        if (num2 < attributeCount)
        {
          readArgs.Reader.MoveToAttribute(num2++);
          localName2 = readArgs.Reader.LocalName;
        }
        else
          flag = true;
      }
      if (!flag && localName2 == "color")
      {
        this.textColor = (Color?) DocumentTreeNode.ColorConverter.ConvertFromInvariantString(readArgs.Reader.Value);
        if (num2 < attributeCount)
        {
          readArgs.Reader.MoveToAttribute(num2++);
          localName2 = readArgs.Reader.LocalName;
        }
        else
          flag = true;
      }
      if (!flag && localName2 == "bkColor")
      {
        this.textBkColor = (Color?) DocumentTreeNode.ColorConverter.ConvertFromInvariantString(readArgs.Reader.Value);
        if (num2 < attributeCount)
        {
          readArgs.Reader.MoveToAttribute(num2++);
          localName2 = readArgs.Reader.LocalName;
        }
        else
          flag = true;
      }
      if (!flag && localName2 == "underlineColor")
      {
        this.underlineColor = (Color?) DocumentTreeNode.ColorConverter.ConvertFromInvariantString(readArgs.Reader.Value);
        if (num2 < attributeCount)
          readArgs.Reader.MoveToAttribute(num2++);
      }
      for (int i2 = num2; i2 < attributeCount; ++i2)
      {
        readArgs.Reader.MoveToAttribute(i2);
        if (!this.ReadFieldFromXml(readArgs))
          readArgs.Reader.ReadOuterXml();
      }
      readArgs.Reader.MoveToElement();
    }
    bool flag1 = readArgs.Reader.IsEmptyElement;
    while (!flag1 && (readArgs.SkipRead || readArgs.Reader.Read()))
    {
      readArgs.SkipRead = false;
      switch (readArgs.Reader.NodeType)
      {
        case XmlNodeType.Element:
          if (!this.ReadFieldFromXml(readArgs))
          {
            readArgs.Reader.ReadOuterXml();
            readArgs.SkipRead = true;
            continue;
          }
          continue;
        case XmlNodeType.EndElement:
          if (localName1 == readArgs.Reader.LocalName)
          {
            flag1 = true;
            continue;
          }
          continue;
        default:
          continue;
      }
    }
    if (flag1)
      return;
    LogManager.AddLine("CharFormat.ReadFromXml End Element not found.");
  }

  /// <summary>Прочитать одно поле из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле прочитано</returns>
  public bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    switch (readArgs.Reader.LocalName)
    {
      case "bkColor":
        this.textBkColor = new Color?((Color) DocumentTreeNode.ColorConverter.ConvertFromInvariantString(readArgs.Reader.Value));
        return true;
      case "charStyle":
      case "style":
        this.charStyle = (CharStyle) int.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture);
        return true;
      case "color":
        this.textColor = new Color?((Color) DocumentTreeNode.ColorConverter.ConvertFromInvariantString(readArgs.Reader.Value));
        return true;
      case "fontName":
      case "name":
        this.fontFamily = readArgs.Reader.Value;
        return true;
      case "fontSize":
      case "size":
        this.fontSize = new float?(float.Parse(DocumentTreeNode.ReplaceDS(readArgs.Reader.Value), (IFormatProvider) CultureInfo.InvariantCulture));
        return true;
      case "underlineColor":
        this.underlineColor = new Color?((Color) DocumentTreeNode.ColorConverter.ConvertFromInvariantString(readArgs.Reader.Value));
        return true;
      default:
        return false;
    }
  }
}
