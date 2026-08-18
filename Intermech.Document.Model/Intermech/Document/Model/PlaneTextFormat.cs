// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.PlaneTextFormat
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>Формат текста</summary>
[TypeConverter(typeof (PlaneTextFormatConverter))]
[Serializable]
public class PlaneTextFormat : IWriteReadXml, ICloneable
{
  private HorzAlignment horizontalAlignment;
  private VertAlignment verticalAlignment;
  private StringFormatFlags formatFlags;
  private StringTrimming trimming = StringTrimming.Character;
  private HotkeyPrefix hotkeyPrefix;

  /// <summary>выравнивание текста</summary>
  [CustomDisplayName("Attribute.Document.Model_65")]
  [CustomDescription("Attribute.Document.Model_66")]
  [CustomCategory("Attribute.Document.Model_67")]
  public HorzAlignment HorizontalAlignment
  {
    [DebuggerStepThrough] get => this.horizontalAlignment;
    set => this.horizontalAlignment = value;
  }

  /// <summary>Преобразовать HorzAlignment в StringAlignment</summary>
  /// <param name="align">Значение типа HorzAlignment</param>
  /// <returns>Значение типа StringAlignment</returns>
  public static StringAlignment HorzAlignToStringAlign(HorzAlignment align)
  {
    switch (align)
    {
      case HorzAlignment.Left:
        return StringAlignment.Near;
      case HorzAlignment.Center:
        return StringAlignment.Center;
      case HorzAlignment.Right:
        return StringAlignment.Far;
      default:
        return StringAlignment.Near;
    }
  }

  /// <summary>Преобразовать StringAlignment в HorzAlignment</summary>
  /// <param name="align">Значение типа StringAlignment</param>
  /// <returns>Значение типа HorzAlignment</returns>
  public static HorzAlignment StringAlignToHorzAlign(StringAlignment align)
  {
    switch (align)
    {
      case StringAlignment.Near:
        return HorzAlignment.Left;
      case StringAlignment.Center:
        return HorzAlignment.Center;
      case StringAlignment.Far:
        return HorzAlignment.Right;
      default:
        return HorzAlignment.Left;
    }
  }

  /// <summary>Выравнивание текста по вертикали</summary>
  [CustomDisplayName("Attribute.Document.Model_68")]
  [CustomDescription("Attribute.Document.Model_69")]
  [CustomCategory("Attribute.Document.Model_70")]
  public VertAlignment VerticalAlignment
  {
    [DebuggerStepThrough] get => this.verticalAlignment;
    set => this.verticalAlignment = value;
  }

  /// <summary>Преобразовать VertAlignment в StringAlignment</summary>
  /// <param name="align">Значение типа VertAlignment</param>
  /// <returns>Значение типа StringAlignment</returns>
  public static StringAlignment VertAlignToStringAlign(VertAlignment align)
  {
    switch (align)
    {
      case VertAlignment.Top:
        return StringAlignment.Near;
      case VertAlignment.Center:
        return StringAlignment.Center;
      case VertAlignment.Bottom:
        return StringAlignment.Far;
      default:
        return StringAlignment.Near;
    }
  }

  /// <summary>Преобразовать StringAlignment в VertAlignment</summary>
  /// <param name="align">Значение типа StringAlignment</param>
  /// <returns>Значение типа VertAlignment</returns>
  public static VertAlignment StringAlignToVertAlign(StringAlignment align)
  {
    switch (align)
    {
      case StringAlignment.Near:
        return VertAlignment.Top;
      case StringAlignment.Center:
        return VertAlignment.Center;
      case StringAlignment.Far:
        return VertAlignment.Bottom;
      default:
        return VertAlignment.Top;
    }
  }

  /// <summary>Флаги форматирования</summary>
  [Browsable(false)]
  public StringFormatFlags FormatFlags
  {
    [DebuggerStepThrough] get => this.formatFlags;
    set => this.formatFlags = value;
  }

  /// <summary>Обрезать текст</summary>
  [Browsable(false)]
  public StringTrimming Trimming
  {
    [DebuggerStepThrough] get => this.trimming;
    set => this.trimming = value;
  }

  /// <summary>Учитывать префикс горячих клавиш</summary>
  [Browsable(false)]
  public HotkeyPrefix HotkeyPrefix
  {
    [DebuggerStepThrough] get => this.hotkeyPrefix;
    set => this.hotkeyPrefix = value;
  }

  /// <summary>Конструктор</summary>
  /// <param name="format">Образец типа StringFormat</param>
  public PlaneTextFormat(StringFormat format)
  {
    this.HorizontalAlignment = PlaneTextFormat.StringAlignToHorzAlign(format.Alignment);
    this.VerticalAlignment = PlaneTextFormat.StringAlignToVertAlign(format.LineAlignment);
    this.FormatFlags = format.FormatFlags;
    this.Trimming = format.Trimming;
    this.HotkeyPrefix = format.HotkeyPrefix;
  }

  /// <summary>Конструктор</summary>
  public PlaneTextFormat()
  {
  }

  /// <summary>Преобразовать в StringFormat</summary>
  /// <returns>StringFormat</returns>
  public StringFormat ToStringFormat()
  {
    return new StringFormat()
    {
      Alignment = PlaneTextFormat.HorzAlignToStringAlign(this.HorizontalAlignment),
      LineAlignment = PlaneTextFormat.VertAlignToStringAlign(this.VerticalAlignment),
      FormatFlags = this.FormatFlags,
      Trimming = this.Trimming,
      HotkeyPrefix = this.HotkeyPrefix
    };
  }

  /// <summary>Клонировать</summary>
  /// <returns>Клон</returns>
  public virtual PlaneTextFormat Clone() => new PlaneTextFormat(this.ToStringFormat());

  /// <summary>Записать поля в XML</summary>
  /// <param name="elementName">Имя элемента XML, под которым нужно сохранить данные</param>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public void WriteToXml(string elementName, XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    xw.WriteStartElement(elementName);
    xw.WriteAttributeString("HorizontalAlignment", this.HorizontalAlignment.ToString());
    xw.WriteAttributeString("VerticalAlignment", this.VerticalAlignment.ToString());
    xw.WriteAttributeString("FormatFlags", this.FormatFlags.ToString());
    xw.WriteAttributeString("Trimming", this.Trimming.ToString());
    xw.WriteAttributeString("HotkeyPrefix", this.HotkeyPrefix.ToString());
    xw.WriteEndElement();
  }

  /// <summary>Загрузить поле из текущего узла XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле загружено</returns>
  public bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    switch (readArgs.Reader.LocalName)
    {
      case "HorizontalAlignment":
        string str1 = readArgs.Reader.Value;
        switch (str1)
        {
          case "Near":
            str1 = "Left";
            break;
          case "Far":
            str1 = "Right";
            break;
        }
        this.HorizontalAlignment = (HorzAlignment) Enum.Parse(typeof (HorzAlignment), str1);
        return true;
      case "VerticalAlignment":
        string str2 = readArgs.Reader.Value;
        switch (str2)
        {
          case "Near":
            str2 = "Top";
            break;
          case "Far":
            str2 = "Bottom";
            break;
        }
        this.VerticalAlignment = (VertAlignment) Enum.Parse(typeof (VertAlignment), str2);
        return true;
      case "FormatFlags":
        this.FormatFlags = (StringFormatFlags) Enum.Parse(typeof (StringFormatFlags), readArgs.Reader.Value);
        return true;
      case "Trimming":
        this.Trimming = (StringTrimming) Enum.Parse(typeof (StringTrimming), readArgs.Reader.Value);
        return true;
      case "HotkeyPrefix":
        this.HotkeyPrefix = (HotkeyPrefix) Enum.Parse(typeof (HotkeyPrefix), readArgs.Reader.Value);
        return true;
      default:
        return false;
    }
  }

  /// <summary>Загрузить узел из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public void ReadFromXml(XmlReadArgs readArgs)
  {
    WriteReadXmlHelper.ReadFromXml((IWriteReadXml) this, readArgs);
  }

  object ICloneable.Clone() => (object) this.Clone();
}
