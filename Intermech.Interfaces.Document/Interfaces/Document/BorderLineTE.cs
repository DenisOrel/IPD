// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.BorderLineTE
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Класс описывающий линию границы элемента для виртуального TableElement</summary>
[TypeConverter(typeof (BorderLineConverterTE))]
public class BorderLineTE : ICloneable
{
  private const float DefaultSerifWidth = 1.5f;
  private Color? colorte;
  private BorderStyles? stylete;
  private float? widthte;
  private float? serifWidthte = new float?(1.5f);

  public BorderLineTE(Color? color, BorderStyles? style, float? width, float? serifWidth)
  {
    this.stylete = style;
    this.widthte = width;
    this.colorte = color;
    this.serifWidthte = new float?((float) ((double) serifWidth ?? 1.5));
  }

  public BorderLineTE(Color color, BorderStyles style, float width, float serifWidth)
  {
    this.stylete = new BorderStyles?(style);
    this.widthte = new float?(width);
    this.colorte = new Color?(color);
    this.serifWidthte = new float?(serifWidth);
  }

  public BorderLineTE()
  {
  }

  public BorderLineTE(BorderLine bl)
  {
    this.stylete = new BorderStyles?(bl.Style);
    this.widthte = new float?(bl.Width);
    this.colorte = new Color?(bl.Color);
    this.serifWidthte = new float?(bl.SerifWidth);
  }

  /// <summary>Создать полную копию линии</summary>
  /// <returns>Возвращает полную копию линии</returns>
  public BorderLineTE Clone()
  {
    return new BorderLineTE(this.colorte, this.stylete, this.widthte, this.serifWidthte);
  }

  object ICloneable.Clone() => (object) this.Clone();

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
    BorderLineTE borderLineTe = (BorderLineTE) obj;
    Color? colorte1 = this.colorte;
    Color? colorte2 = borderLineTe.colorte;
    if ((colorte1.HasValue == colorte2.HasValue ? (colorte1.HasValue ? (colorte1.GetValueOrDefault() == colorte2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
    {
      BorderStyles? stylete1 = this.stylete;
      BorderStyles? stylete2 = borderLineTe.stylete;
      if (stylete1.GetValueOrDefault() == stylete2.GetValueOrDefault() & stylete1.HasValue == stylete2.HasValue)
      {
        float? nullable = this.widthte;
        float? widthte = borderLineTe.widthte;
        if ((double) nullable.GetValueOrDefault() == (double) widthte.GetValueOrDefault() & nullable.HasValue == widthte.HasValue)
        {
          float? serifWidthte = this.serifWidthte;
          nullable = borderLineTe.serifWidthte;
          return (double) serifWidthte.GetValueOrDefault() == (double) nullable.GetValueOrDefault() & serifWidthte.HasValue == nullable.HasValue;
        }
      }
    }
    return false;
  }

  /// <summary>Получить хэш код объекта</summary>
  /// <returns>Хэш код объекта</returns>
  public override int GetHashCode()
  {
    int hashCode1 = this.colorte.GetHashCode();
    int hashCode2 = this.stylete.GetHashCode();
    int hashCode3 = this.widthte.GetHashCode();
    int hashCode4 = this.serifWidthte.GetHashCode();
    int num = hashCode2 << 13 | hashCode2 >> 19;
    return hashCode1 ^ num ^ (hashCode3 << 26 | hashCode3 >> 6) ^ (hashCode4 << 5 | hashCode4 >> 27);
  }

  /// <summary>Цвет</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_1")]
  [CustomDescription("Attribute.Interfaces.Document_2")]
  [CustomCategory("Attribute.Interfaces.Document_3")]
  public virtual Color? ColorTE
  {
    [DebuggerStepThrough] get => this.colorte;
    set
    {
      Color? colorte = this.colorte;
      Color? nullable = value;
      if ((colorte.HasValue == nullable.HasValue ? (colorte.HasValue ? (colorte.GetValueOrDefault() != nullable.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
        return;
      this.colorte = value;
    }
  }

  /// <summary>Стиль</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_4")]
  [CustomDescription("Attribute.Interfaces.Document_5")]
  [CustomCategory("Attribute.Interfaces.Document_6")]
  public virtual BorderStyles? StyleTE
  {
    [DebuggerStepThrough] get => this.stylete;
    set
    {
      BorderStyles? stylete = this.stylete;
      BorderStyles? nullable = value;
      if (stylete.GetValueOrDefault() == nullable.GetValueOrDefault() & stylete.HasValue == nullable.HasValue)
        return;
      this.stylete = value;
    }
  }

  /// <summary>Толщина линии в миллиметрах. 0 означает толщину по умолчанию</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_7")]
  [CustomDescription("Attribute.Interfaces.Document_8")]
  [CustomCategory("Attribute.Interfaces.Document_9")]
  [TypeConverter(typeof (FloatConverter))]
  public virtual float? WidthTE
  {
    [DebuggerStepThrough] get => this.widthte;
    set
    {
      float? widthte = this.widthte;
      float? nullable = value;
      if ((double) widthte.GetValueOrDefault() == (double) nullable.GetValueOrDefault() & widthte.HasValue == nullable.HasValue)
        return;
      this.widthte = value;
    }
  }

  /// <summary>
  /// Длина штриха в миллиметрах при стиле линии "Один штрих в начале"
  /// </summary>
  [CustomDisplayName("Attribute.Interfaces.Document_611")]
  [CustomDescription("Attribute.Interfaces.Document_612")]
  [CustomCategory("Attribute.Interfaces.Document_9")]
  [TypeConverter(typeof (FloatConverter))]
  public virtual float? SerifWidthTE
  {
    [DebuggerStepThrough] get => new float?((float) ((double) this.serifWidthte ?? 1.5));
    set
    {
      float? serifWidthte = this.serifWidthte;
      float? nullable = value;
      if ((double) serifWidthte.GetValueOrDefault() == (double) nullable.GetValueOrDefault() & serifWidthte.HasValue == nullable.HasValue)
        return;
      this.serifWidthte = new float?((float) ((double) value ?? 1.5));
    }
  }
}
