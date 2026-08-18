// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.ParagraphFormat
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
using System.Drawing.Text;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary> Настройки стиля параграфа </summary>
[TypeConverter(typeof (ParagraphFormatConverter))]
[Serializable]
public class ParagraphFormat : ICloneable, IWriteReadXml, IEquatable<ParagraphFormat>
{
  private Intermech.Interfaces.Document.HorzAlignment? _horzAlignment = new Intermech.Interfaces.Document.HorzAlignment?(Intermech.Interfaces.Document.HorzAlignment.Left);
  private Intermech.Interfaces.Document.VertAlignment? _vertAlignment = new Intermech.Interfaces.Document.VertAlignment?(Intermech.Interfaces.Document.VertAlignment.Top);
  private int? _textLevel;
  private float? _identLeft = new float?(0.0f);
  private float? _identRight = new float?(0.0f);
  private float? _identFirstLine = new float?(0.0f);
  private float? _intervalBefore = new float?(0.0f);
  private float? _intervalAfter = new float?(0.0f);
  private Intermech.Interfaces.Document.LineSpacingMethod? _lineSpacingMethod = new Intermech.Interfaces.Document.LineSpacingMethod?(Intermech.Interfaces.Document.LineSpacingMethod.Ratio_1);
  private float? _spaceBetweenLines;
  private bool? _disableFloatLines = new bool?(true);
  private bool? _keepTogether = new bool?(false);
  private bool? _keepWithNext = new bool?(false);
  private bool? _fromNewPage = new bool?(false);
  private bool? _disableWordWrap = new bool?(false);

  /// <summary>Конструктор</summary>
  public ParagraphFormat()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="isAllfieldsNull">всем полям присвоен null </param>
  public ParagraphFormat(bool isAllАFieldsNull)
  {
    if (!isAllАFieldsNull)
      return;
    this._horzAlignment = new Intermech.Interfaces.Document.HorzAlignment?();
    this._vertAlignment = new Intermech.Interfaces.Document.VertAlignment?();
    this._textLevel = new int?();
    this._identLeft = new float?();
    this._identRight = new float?();
    this._identFirstLine = new float?();
    this._intervalBefore = new float?();
    this._intervalAfter = new float?();
    this._lineSpacingMethod = new Intermech.Interfaces.Document.LineSpacingMethod?();
    this._spaceBetweenLines = new float?();
    this._disableFloatLines = new bool?();
    this._keepTogether = new bool?();
    this._keepWithNext = new bool?();
    this._fromNewPage = new bool?();
    this._disableWordWrap = new bool?();
  }

  /// <summary>Конструктор</summary>
  /// <param name="planeTextFormat">Устаревший PlaneTextFormat</param>
  public ParagraphFormat(PlaneTextFormat planeTextFormat)
  {
    this._horzAlignment = new Intermech.Interfaces.Document.HorzAlignment?(planeTextFormat.HorizontalAlignment);
    this._vertAlignment = new Intermech.Interfaces.Document.VertAlignment?(planeTextFormat.VerticalAlignment);
  }

  /// <summary> Выравнивание текста по горизонтали </summary>
  [CustomDisplayName("Attribute.Interfaces.Document_201")]
  [CustomDescription("Attribute.Interfaces.Document_202")]
  [CustomCategory("Attribute.Interfaces.Document_203")]
  [TypeConverter(typeof (EnumCustomConverter))]
  public Intermech.Interfaces.Document.HorzAlignment? HorzAlignment
  {
    [DebuggerStepThrough] get => this._horzAlignment;
    set
    {
      if (!value.HasValue)
        return;
      this._horzAlignment = value;
    }
  }

  /// <summary> Выравнивание текста по вертикали</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_204")]
  [CustomDescription("Attribute.Interfaces.Document_205")]
  [CustomCategory("Attribute.Interfaces.Document_206")]
  [TypeConverter(typeof (EnumCustomConverter))]
  public Intermech.Interfaces.Document.VertAlignment? VertAlignment
  {
    [DebuggerStepThrough] get => this._vertAlignment;
    set
    {
      if (!value.HasValue)
        return;
      this._vertAlignment = value;
    }
  }

  /// <summary> Уровень текста </summary>
  [CustomDisplayName("Attribute.Interfaces.Document_207")]
  [CustomDescription("Attribute.Interfaces.Document_208")]
  [CustomCategory("Attribute.Interfaces.Document_209")]
  [Browsable(false)]
  public int? TextLevel
  {
    [DebuggerStepThrough] get => this._textLevel;
    set
    {
      if (!value.HasValue)
        return;
      this._textLevel = value;
    }
  }

  /// <summary> Отступ слева (в сантиметрах) </summary>
  [CustomDisplayName("Attribute.Interfaces.Document_210")]
  [CustomDescription("Attribute.Interfaces.Document_211")]
  [CustomCategory("Attribute.Interfaces.Document_212")]
  [TypeConverter(typeof (SMConverter))]
  public float? IdentLeft
  {
    [DebuggerStepThrough] get => this._identLeft;
    set
    {
      if (!value.HasValue)
        return;
      this._identLeft = value;
    }
  }

  /// <summary> Отступ справа (в сантиметрах) </summary>
  [CustomDisplayName("Attribute.Interfaces.Document_213")]
  [CustomDescription("Attribute.Interfaces.Document_214")]
  [CustomCategory("Attribute.Interfaces.Document_215")]
  [TypeConverter(typeof (SMConverter))]
  public float? IdentRight
  {
    [DebuggerStepThrough] get => this._identRight;
    set
    {
      if (!value.HasValue)
        return;
      this._identRight = value;
    }
  }

  /// <summary> Отступ первой строки (в сантиметрах) </summary>
  [CustomDisplayName("Attribute.Interfaces.Document_216")]
  [CustomDescription("Attribute.Interfaces.Document_217")]
  [CustomCategory("Attribute.Interfaces.Document_218")]
  [TypeConverter(typeof (SMConverter))]
  public float? IdentFirstLine
  {
    [DebuggerStepThrough] get => this._identFirstLine;
    set
    {
      if (!value.HasValue)
        return;
      this._identFirstLine = value;
    }
  }

  /// <summary> Интервал перед абзацем (в поинтах) </summary>
  [CustomDisplayName("Attribute.Interfaces.Document_219")]
  [CustomDescription("Attribute.Interfaces.Document_220")]
  [CustomCategory("Attribute.Interfaces.Document_221")]
  [TypeConverter(typeof (PTConverter))]
  public float? IntervalBefore
  {
    [DebuggerStepThrough] get => this._intervalBefore;
    set
    {
      if (!value.HasValue)
        return;
      this._intervalBefore = value;
    }
  }

  /// <summary> Интервал после абзаца (в поинтах) </summary>
  [CustomDisplayName("Attribute.Interfaces.Document_222")]
  [CustomDescription("Attribute.Interfaces.Document_223")]
  [CustomCategory("Attribute.Interfaces.Document_224")]
  [TypeConverter(typeof (PTConverter))]
  public float? IntervalAfter
  {
    [DebuggerStepThrough] get => this._intervalAfter;
    set
    {
      if (!value.HasValue)
        return;
      this._intervalAfter = value;
    }
  }

  /// <summary> Способ задания междустрочного пространства </summary>
  [CustomDisplayName("Attribute.Interfaces.Document_225")]
  [CustomDescription("Attribute.Interfaces.Document_226")]
  [CustomCategory("Attribute.Interfaces.Document_227")]
  [TypeConverter(typeof (EnumCustomConverter))]
  [RefreshProperties(RefreshProperties.All)]
  public Intermech.Interfaces.Document.LineSpacingMethod? LineSpacingMethod
  {
    [DebuggerStepThrough] get => this._lineSpacingMethod;
    set
    {
      if (!value.HasValue)
        return;
      this._lineSpacingMethod = value;
    }
  }

  /// <summary> Если _lineSpacingMethod ==
  /// LineSpacingMethod.InPercents, то здесь храниться междустрочное расстояние в процентах от нормального растояния,
  /// Exact - в поинтах
  /// ExactMM - в мм
  /// Ratio - множитель
  /// </summary>
  [CustomDisplayName("Attribute.Interfaces.Document_228")]
  [CustomDescription("Attribute.Interfaces.Document_229")]
  [CustomCategory("Attribute.Interfaces.Document_230")]
  [TypeConverter(typeof (LineSpacingConverter))]
  public float? SpaceBetweenLines
  {
    [DebuggerStepThrough] get => this._spaceBetweenLines;
    set
    {
      if (!value.HasValue)
        return;
      Intermech.Interfaces.Document.LineSpacingMethod? lineSpacingMethod = this.LineSpacingMethod;
      if (lineSpacingMethod.HasValue)
      {
        switch (lineSpacingMethod.GetValueOrDefault())
        {
          case Intermech.Interfaces.Document.LineSpacingMethod.Ratio_1:
          case Intermech.Interfaces.Document.LineSpacingMethod.Ratio_1_5:
          case Intermech.Interfaces.Document.LineSpacingMethod.Ratio_2:
            return;
        }
      }
      this._spaceBetweenLines = value;
    }
  }

  public void AssignSpaceBetweenLines(float? value) => this._spaceBetweenLines = value;

  /// <summary> Запрет висячих строк </summary>
  [CustomDisplayName("Attribute.Interfaces.Document_231")]
  [CustomDescription("Attribute.Interfaces.Document_232")]
  [CustomCategory("Attribute.Interfaces.Document_233")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool? DisableFloatLines
  {
    [DebuggerStepThrough] get => this._disableFloatLines;
    set
    {
      if (!value.HasValue)
        return;
      this._disableFloatLines = value;
    }
  }

  /// <summary> Не разрывать абзац </summary>
  [CustomDisplayName("Attribute.Interfaces.Document_234")]
  [CustomDescription("Attribute.Interfaces.Document_235")]
  [CustomCategory("Attribute.Interfaces.Document_236")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool? KeepTogether
  {
    [DebuggerStepThrough] get => this._keepTogether;
    set
    {
      if (!value.HasValue)
        return;
      this._keepTogether = value;
    }
  }

  /// <summary> Не отрывать абзац от следующего </summary>
  [CustomDisplayName("Attribute.Interfaces.Document_237")]
  [CustomDescription("Attribute.Interfaces.Document_238")]
  [CustomCategory("Attribute.Interfaces.Document_239")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool? KeepWithNext
  {
    [DebuggerStepThrough] get => this._keepWithNext;
    set
    {
      if (!value.HasValue)
        return;
      this._keepWithNext = value;
    }
  }

  /// <summary> Начать с новой страницы </summary>
  [CustomDisplayName("Attribute.Interfaces.Document_240")]
  [CustomDescription("Attribute.Interfaces.Document_241")]
  [CustomCategory("Attribute.Interfaces.Document_242")]
  [Browsable(false)]
  public bool? FromNewPage
  {
    [DebuggerStepThrough] get => this._fromNewPage;
    set
    {
      if (!value.HasValue)
        return;
      this._fromNewPage = value;
    }
  }

  /// <summary> Отключить автоматический перенос строк </summary>
  [CustomDisplayName("Attribute.Interfaces.Document_243")]
  [CustomDescription("Attribute.Interfaces.Document_244")]
  [CustomCategory("Attribute.Interfaces.Document_245")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool? DisableWordWrap
  {
    [DebuggerStepThrough] get => this._disableWordWrap;
    set
    {
      if (!value.HasValue)
        return;
      this._disableWordWrap = value;
    }
  }

  /// <summary> Если поля текущего параграфа не совпадают с полями входного то установить их в null </summary>
  /// <returns> Есть или нет совпадающие поля </returns>
  public bool GetFields(ParagraphFormat var)
  {
    bool flag = true;
    Intermech.Interfaces.Document.HorzAlignment? horzAlignment1 = this._horzAlignment;
    Intermech.Interfaces.Document.HorzAlignment? horzAlignment2 = var.HorzAlignment;
    if (!(horzAlignment1.GetValueOrDefault() == horzAlignment2.GetValueOrDefault() & horzAlignment1.HasValue == horzAlignment2.HasValue))
    {
      this._horzAlignment = new Intermech.Interfaces.Document.HorzAlignment?();
      flag = false;
    }
    else
      flag = true;
    Intermech.Interfaces.Document.VertAlignment? vertAlignment1 = this._vertAlignment;
    Intermech.Interfaces.Document.VertAlignment? vertAlignment2 = var.VertAlignment;
    if (!(vertAlignment1.GetValueOrDefault() == vertAlignment2.GetValueOrDefault() & vertAlignment1.HasValue == vertAlignment2.HasValue))
    {
      this._vertAlignment = new Intermech.Interfaces.Document.VertAlignment?();
      flag = false;
    }
    else
      flag = true;
    bool? disableFloatLines = this._disableFloatLines;
    bool? nullable1 = var.DisableFloatLines;
    if (!(disableFloatLines.GetValueOrDefault() == nullable1.GetValueOrDefault() & disableFloatLines.HasValue == nullable1.HasValue))
    {
      this._disableFloatLines = new bool?();
      flag = false;
    }
    else
      flag = true;
    float? spaceBetweenLines = this._spaceBetweenLines;
    float? nullable2 = var.SpaceBetweenLines;
    if (!((double) spaceBetweenLines.GetValueOrDefault() == (double) nullable2.GetValueOrDefault() & spaceBetweenLines.HasValue == nullable2.HasValue))
    {
      this._spaceBetweenLines = new float?();
      flag = false;
    }
    else
      flag = true;
    nullable1 = this._keepWithNext;
    bool? nullable3 = var.KeepWithNext;
    if (!(nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue))
    {
      this._keepWithNext = new bool?();
      flag = false;
    }
    else
      flag = true;
    nullable3 = this._keepTogether;
    nullable1 = var.KeepTogether;
    if (!(nullable3.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable3.HasValue == nullable1.HasValue))
    {
      this._keepTogether = new bool?();
      flag = false;
    }
    else
      flag = true;
    nullable1 = this._disableWordWrap;
    nullable3 = var.DisableWordWrap;
    if (!(nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue))
    {
      this._disableWordWrap = new bool?();
      flag = false;
    }
    else
      flag = true;
    nullable2 = this._identFirstLine;
    float? nullable4 = var.IdentFirstLine;
    if (!((double) nullable2.GetValueOrDefault() == (double) nullable4.GetValueOrDefault() & nullable2.HasValue == nullable4.HasValue))
    {
      this._identFirstLine = new float?();
      flag = false;
    }
    else
      flag = true;
    nullable4 = this._identLeft;
    nullable2 = var.IdentLeft;
    if (!((double) nullable4.GetValueOrDefault() == (double) nullable2.GetValueOrDefault() & nullable4.HasValue == nullable2.HasValue))
    {
      this._identLeft = new float?();
      flag = false;
    }
    else
      flag = true;
    nullable2 = this._identRight;
    nullable4 = var.IdentRight;
    if (!((double) nullable2.GetValueOrDefault() == (double) nullable4.GetValueOrDefault() & nullable2.HasValue == nullable4.HasValue))
    {
      this._identRight = new float?();
      flag = false;
    }
    else
      flag = true;
    nullable4 = this._intervalBefore;
    nullable2 = var.IntervalBefore;
    if (!((double) nullable4.GetValueOrDefault() == (double) nullable2.GetValueOrDefault() & nullable4.HasValue == nullable2.HasValue))
    {
      this._intervalBefore = new float?();
      flag = false;
    }
    else
      flag = true;
    nullable2 = this._intervalAfter;
    nullable4 = var.IntervalAfter;
    if (!((double) nullable2.GetValueOrDefault() == (double) nullable4.GetValueOrDefault() & nullable2.HasValue == nullable4.HasValue))
    {
      this._intervalAfter = new float?();
      flag = false;
    }
    else
      flag = true;
    nullable3 = this._fromNewPage;
    nullable1 = var.FromNewPage;
    if (!(nullable3.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable3.HasValue == nullable1.HasValue))
    {
      this._fromNewPage = new bool?();
      flag = false;
    }
    else
      flag = true;
    Intermech.Interfaces.Document.LineSpacingMethod? lineSpacingMethod1 = this._lineSpacingMethod;
    Intermech.Interfaces.Document.LineSpacingMethod? lineSpacingMethod2 = var.LineSpacingMethod;
    if (!(lineSpacingMethod1.GetValueOrDefault() == lineSpacingMethod2.GetValueOrDefault() & lineSpacingMethod1.HasValue == lineSpacingMethod2.HasValue))
    {
      this._lineSpacingMethod = new Intermech.Interfaces.Document.LineSpacingMethod?();
      flag = false;
    }
    else
      flag = true;
    int? textLevel1 = this._textLevel;
    int? textLevel2 = var.TextLevel;
    bool fields;
    if (!(textLevel1.GetValueOrDefault() == textLevel2.GetValueOrDefault() & textLevel1.HasValue == textLevel2.HasValue))
    {
      this._textLevel = new int?();
      fields = false;
    }
    else
      fields = true;
    return fields;
  }

  /// <summary> Создание копии </summary>
  /// <returns> Копия объекта </returns>
  public ParagraphFormat Clone()
  {
    ParagraphFormat paragraphFormat = new ParagraphFormat();
    paragraphFormat.CopyParamsFrom(this);
    return paragraphFormat;
  }

  /// <summary> Копирование параметров из некоторого другого объекта </summary>
  /// <param name="paragraphFormat"> Пакет настроек форматирования абзаца </param>
  public void CopyParamsFrom(ParagraphFormat paragraphFormat)
  {
    this._horzAlignment = paragraphFormat != null ? paragraphFormat.HorzAlignment : throw new ArgumentNullException(nameof (paragraphFormat));
    this._vertAlignment = paragraphFormat.VertAlignment;
    this._textLevel = paragraphFormat.TextLevel;
    this._identLeft = paragraphFormat.IdentLeft;
    this._identRight = paragraphFormat.IdentRight;
    this._identFirstLine = paragraphFormat.IdentFirstLine;
    this._intervalBefore = paragraphFormat.IntervalBefore;
    this._intervalAfter = paragraphFormat.IntervalAfter;
    this._lineSpacingMethod = paragraphFormat.LineSpacingMethod;
    this._spaceBetweenLines = paragraphFormat.SpaceBetweenLines;
    this._disableFloatLines = paragraphFormat.DisableFloatLines;
    this._keepTogether = paragraphFormat.KeepTogether;
    this._keepWithNext = paragraphFormat.KeepWithNext;
    this._fromNewPage = paragraphFormat.FromNewPage;
    this._disableWordWrap = paragraphFormat.DisableWordWrap;
  }

  /// <summary>Преобразовать HorzAlignment в StringAlignment</summary>
  /// <param name="align">Значение типа HorzAlignment</param>
  /// <returns>Значение типа StringAlignment</returns>
  public static StringAlignment HorzAlignToStringAlign(Intermech.Interfaces.Document.HorzAlignment align)
  {
    switch (align)
    {
      case Intermech.Interfaces.Document.HorzAlignment.Left:
        return StringAlignment.Near;
      case Intermech.Interfaces.Document.HorzAlignment.Center:
        return StringAlignment.Center;
      case Intermech.Interfaces.Document.HorzAlignment.Right:
        return StringAlignment.Far;
      default:
        return StringAlignment.Near;
    }
  }

  /// <summary>Преобразовать VertAlignment в StringAlignment</summary>
  /// <param name="align">Значение типа VertAlignment</param>
  /// <returns>Значение типа StringAlignment</returns>
  public static StringAlignment VertAlignToStringAlign(Intermech.Interfaces.Document.VertAlignment align)
  {
    switch (align)
    {
      case Intermech.Interfaces.Document.VertAlignment.Top:
        return StringAlignment.Near;
      case Intermech.Interfaces.Document.VertAlignment.Center:
        return StringAlignment.Center;
      case Intermech.Interfaces.Document.VertAlignment.Bottom:
        return StringAlignment.Far;
      default:
        return StringAlignment.Near;
    }
  }

  /// <summary>Преобразовать в StringFormat</summary>
  /// <returns>StringFormat</returns>
  public StringFormat GetStringFormat()
  {
    return new StringFormat()
    {
      Alignment = !this.HorzAlignment.HasValue ? StringAlignment.Near : ParagraphFormat.HorzAlignToStringAlign(this.HorzAlignment.Value),
      LineAlignment = !this.VertAlignment.HasValue ? StringAlignment.Near : ParagraphFormat.VertAlignToStringAlign(this.VertAlignment.Value),
      Trimming = StringTrimming.Character,
      HotkeyPrefix = HotkeyPrefix.None
    };
  }

  public override bool Equals(object obj) => this.Equals(obj as ParagraphFormat);

  public bool Equals(ParagraphFormat other)
  {
    return other != null && EqualityComparer<Intermech.Interfaces.Document.HorzAlignment?>.Default.Equals(this._horzAlignment, other._horzAlignment) && EqualityComparer<Intermech.Interfaces.Document.VertAlignment?>.Default.Equals(this._vertAlignment, other._vertAlignment) && EqualityComparer<int?>.Default.Equals(this._textLevel, other._textLevel) && EqualityComparer<float?>.Default.Equals(this._identLeft, other._identLeft) && EqualityComparer<float?>.Default.Equals(this._identRight, other._identRight) && EqualityComparer<float?>.Default.Equals(this._identFirstLine, other._identFirstLine) && EqualityComparer<float?>.Default.Equals(this._intervalBefore, other._intervalBefore) && EqualityComparer<float?>.Default.Equals(this._intervalAfter, other._intervalAfter) && EqualityComparer<Intermech.Interfaces.Document.LineSpacingMethod?>.Default.Equals(this._lineSpacingMethod, other._lineSpacingMethod) && EqualityComparer<float?>.Default.Equals(this._spaceBetweenLines, other._spaceBetweenLines) && EqualityComparer<bool?>.Default.Equals(this._disableFloatLines, other._disableFloatLines) && EqualityComparer<bool?>.Default.Equals(this._keepTogether, other._keepTogether) && EqualityComparer<bool?>.Default.Equals(this._keepWithNext, other._keepWithNext) && EqualityComparer<bool?>.Default.Equals(this._fromNewPage, other._fromNewPage) && EqualityComparer<bool?>.Default.Equals(this._disableWordWrap, other._disableWordWrap);
  }

  public override int GetHashCode()
  {
    return ((((((((((((((-1492518633 * -1521134295 + EqualityComparer<Intermech.Interfaces.Document.HorzAlignment?>.Default.GetHashCode(this._horzAlignment)) * -1521134295 + EqualityComparer<Intermech.Interfaces.Document.VertAlignment?>.Default.GetHashCode(this._vertAlignment)) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this._textLevel)) * -1521134295 + EqualityComparer<float?>.Default.GetHashCode(this._identLeft)) * -1521134295 + EqualityComparer<float?>.Default.GetHashCode(this._identRight)) * -1521134295 + EqualityComparer<float?>.Default.GetHashCode(this._identFirstLine)) * -1521134295 + EqualityComparer<float?>.Default.GetHashCode(this._intervalBefore)) * -1521134295 + EqualityComparer<float?>.Default.GetHashCode(this._intervalAfter)) * -1521134295 + EqualityComparer<Intermech.Interfaces.Document.LineSpacingMethod?>.Default.GetHashCode(this._lineSpacingMethod)) * -1521134295 + EqualityComparer<float?>.Default.GetHashCode(this._spaceBetweenLines)) * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(this._disableFloatLines)) * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(this._keepTogether)) * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(this._keepWithNext)) * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(this._fromNewPage)) * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(this._disableWordWrap);
  }

  /// <summary> Создание копии </summary>
  /// <returns> Копия объекта </returns>
  object ICloneable.Clone() => (object) this.Clone();

  /// <summary>Записать поля в XML</summary>
  /// <param name="elementName">Имя элемента XML, под которым нужно сохранить данные</param>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public void WriteToXml(string elementName, XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    xw.WriteStartElement(elementName);
    int num1;
    if (this.HorzAlignment.HasValue)
    {
      XmlWriter xmlWriter = xw;
      num1 = (int) this.HorzAlignment.Value;
      string str = num1.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      xmlWriter.WriteAttributeString("hAlign", str);
    }
    if (this.VertAlignment.HasValue)
    {
      XmlWriter xmlWriter = xw;
      num1 = (int) this.VertAlignment.Value;
      string str = num1.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      xmlWriter.WriteAttributeString("vAlign", str);
    }
    if (this.TextLevel.HasValue)
    {
      XmlWriter xmlWriter = xw;
      num1 = this.TextLevel.Value;
      string str = num1.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      xmlWriter.WriteAttributeString("level", str);
    }
    float? nullable1;
    float num2;
    if (this.IdentLeft.HasValue)
    {
      XmlWriter xmlWriter = xw;
      nullable1 = this.IdentLeft;
      num2 = nullable1.Value;
      string str = num2.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      xmlWriter.WriteAttributeString("identL", str);
    }
    nullable1 = this.IdentRight;
    if (nullable1.HasValue)
    {
      XmlWriter xmlWriter = xw;
      nullable1 = this.IdentRight;
      num2 = nullable1.Value;
      string str = num2.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      xmlWriter.WriteAttributeString("identR", str);
    }
    nullable1 = this.IdentFirstLine;
    if (nullable1.HasValue)
    {
      XmlWriter xmlWriter = xw;
      nullable1 = this.IdentFirstLine;
      num2 = nullable1.Value;
      string str = num2.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      xmlWriter.WriteAttributeString("identF", str);
    }
    nullable1 = this.IntervalBefore;
    if (nullable1.HasValue)
    {
      XmlWriter xmlWriter = xw;
      nullable1 = this.IntervalBefore;
      num2 = nullable1.Value;
      string str = num2.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      xmlWriter.WriteAttributeString("before", str);
    }
    nullable1 = this.IntervalAfter;
    if (nullable1.HasValue)
    {
      XmlWriter xmlWriter = xw;
      nullable1 = this.IntervalAfter;
      num2 = nullable1.Value;
      string str = num2.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      xmlWriter.WriteAttributeString("after", str);
    }
    if (this.LineSpacingMethod.HasValue)
    {
      XmlWriter xmlWriter = xw;
      num1 = (int) this.LineSpacingMethod.Value;
      string str = num1.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      xmlWriter.WriteAttributeString("lsMethod", str);
    }
    nullable1 = this.SpaceBetweenLines;
    if (nullable1.HasValue)
    {
      XmlWriter xmlWriter = xw;
      nullable1 = this.SpaceBetweenLines;
      num2 = nullable1.Value;
      string str = num2.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      xmlWriter.WriteAttributeString("btwLines", str);
    }
    bool? nullable2;
    bool flag;
    if (this.DisableFloatLines.HasValue)
    {
      XmlWriter xmlWriter = xw;
      nullable2 = this.DisableFloatLines;
      flag = nullable2.Value;
      string str = flag.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      xmlWriter.WriteAttributeString("noFloatLines", str);
    }
    nullable2 = this.KeepTogether;
    if (nullable2.HasValue)
    {
      XmlWriter xmlWriter = xw;
      nullable2 = this.KeepTogether;
      flag = nullable2.Value;
      string str = flag.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      xmlWriter.WriteAttributeString("noBreak", str);
    }
    nullable2 = this.KeepWithNext;
    if (nullable2.HasValue)
    {
      XmlWriter xmlWriter = xw;
      nullable2 = this.KeepWithNext;
      flag = nullable2.Value;
      string str = flag.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      xmlWriter.WriteAttributeString("keepWithNext", str);
    }
    nullable2 = this.FromNewPage;
    if (nullable2.HasValue)
    {
      XmlWriter xmlWriter = xw;
      nullable2 = this.FromNewPage;
      flag = nullable2.Value;
      string str = flag.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      xmlWriter.WriteAttributeString("newPage", str);
    }
    nullable2 = this.DisableWordWrap;
    if (nullable2.HasValue)
    {
      XmlWriter xmlWriter = xw;
      nullable2 = this.DisableWordWrap;
      flag = nullable2.Value;
      string str = flag.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      xmlWriter.WriteAttributeString("noWrap", str);
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
      if (!flag && (readArgs.Version >= 19 && localName2 == "hAlign" || readArgs.Version < 19 && localName2 == "HorzAlignment"))
      {
        this._horzAlignment = new Intermech.Interfaces.Document.HorzAlignment?((Intermech.Interfaces.Document.HorzAlignment) int.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture));
        if (num2 < attributeCount)
        {
          readArgs.Reader.MoveToAttribute(num2++);
          localName2 = readArgs.Reader.LocalName;
        }
        else
          flag = true;
      }
      if (!flag && (readArgs.Version >= 19 && localName2 == "vAlign" || readArgs.Version < 19 && localName2 == "VertAlignment"))
      {
        this._vertAlignment = new Intermech.Interfaces.Document.VertAlignment?((Intermech.Interfaces.Document.VertAlignment) int.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture));
        if (num2 < attributeCount)
        {
          readArgs.Reader.MoveToAttribute(num2++);
          localName2 = readArgs.Reader.LocalName;
        }
        else
          flag = true;
      }
      if (!flag && (readArgs.Version >= 19 && localName2 == "level" || readArgs.Version < 19 && localName2 == "TextLevel"))
      {
        this._textLevel = new int?(int.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture));
        if (num2 < attributeCount)
        {
          readArgs.Reader.MoveToAttribute(num2++);
          localName2 = readArgs.Reader.LocalName;
        }
        else
          flag = true;
      }
      if (!flag && (readArgs.Version >= 19 && localName2 == "identL" || readArgs.Version < 19 && localName2 == "IdentLeft"))
      {
        this._identLeft = new float?(float.Parse(DocumentTreeNode.ReplaceDS(readArgs.Reader.Value), (IFormatProvider) CultureInfo.InvariantCulture));
        if (num2 < attributeCount)
        {
          readArgs.Reader.MoveToAttribute(num2++);
          localName2 = readArgs.Reader.LocalName;
        }
        else
          flag = true;
      }
      if (!flag && (readArgs.Version >= 19 && localName2 == "identR" || readArgs.Version < 19 && localName2 == "IdentRight"))
      {
        this._identRight = new float?(float.Parse(DocumentTreeNode.ReplaceDS(readArgs.Reader.Value), (IFormatProvider) CultureInfo.InvariantCulture));
        if (num2 < attributeCount)
        {
          readArgs.Reader.MoveToAttribute(num2++);
          localName2 = readArgs.Reader.LocalName;
        }
        else
          flag = true;
      }
      if (!flag && (readArgs.Version >= 19 && localName2 == "identF" || readArgs.Version < 19 && localName2 == "IdentFirstLine"))
      {
        this._identFirstLine = new float?(float.Parse(DocumentTreeNode.ReplaceDS(readArgs.Reader.Value), (IFormatProvider) CultureInfo.InvariantCulture));
        if (num2 < attributeCount)
        {
          readArgs.Reader.MoveToAttribute(num2++);
          localName2 = readArgs.Reader.LocalName;
        }
        else
          flag = true;
      }
      if (!flag && (readArgs.Version >= 19 && localName2 == "before" || readArgs.Version < 19 && localName2 == "IntervalBefore"))
      {
        this._intervalBefore = new float?(float.Parse(DocumentTreeNode.ReplaceDS(readArgs.Reader.Value), (IFormatProvider) CultureInfo.InvariantCulture));
        if (num2 < attributeCount)
        {
          readArgs.Reader.MoveToAttribute(num2++);
          localName2 = readArgs.Reader.LocalName;
        }
        else
          flag = true;
      }
      if (!flag && (readArgs.Version >= 19 && localName2 == "after" || readArgs.Version < 19 && localName2 == "IntervalAfter"))
      {
        this._intervalAfter = new float?(float.Parse(DocumentTreeNode.ReplaceDS(readArgs.Reader.Value), (IFormatProvider) CultureInfo.InvariantCulture));
        if (num2 < attributeCount)
        {
          readArgs.Reader.MoveToAttribute(num2++);
          localName2 = readArgs.Reader.LocalName;
        }
        else
          flag = true;
      }
      if (!flag && (readArgs.Version >= 19 && localName2 == "lsMethod" || readArgs.Version < 19 && localName2 == "LineSpacingMethod"))
      {
        this._lineSpacingMethod = new Intermech.Interfaces.Document.LineSpacingMethod?((Intermech.Interfaces.Document.LineSpacingMethod) int.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture));
        if (num2 < attributeCount)
        {
          readArgs.Reader.MoveToAttribute(num2++);
          localName2 = readArgs.Reader.LocalName;
        }
        else
          flag = true;
      }
      if (!flag && (readArgs.Version >= 19 && localName2 == "btwLines" || readArgs.Version < 19 && localName2 == "SpaceBetweenLines"))
      {
        this._spaceBetweenLines = new float?(float.Parse(DocumentTreeNode.ReplaceDS(readArgs.Reader.Value), (IFormatProvider) CultureInfo.InvariantCulture));
        if (num2 < attributeCount)
        {
          readArgs.Reader.MoveToAttribute(num2++);
          localName2 = readArgs.Reader.LocalName;
        }
        else
          flag = true;
      }
      if (!flag && (readArgs.Version >= 19 && localName2 == "noFloatLines" || readArgs.Version < 19 && localName2 == "DisableFloatLines"))
      {
        this._disableFloatLines = new bool?(bool.Parse(readArgs.Reader.Value));
        if (num2 < attributeCount)
        {
          readArgs.Reader.MoveToAttribute(num2++);
          localName2 = readArgs.Reader.LocalName;
        }
        else
          flag = true;
      }
      if (!flag && (readArgs.Version >= 19 && localName2 == "noBreak" || readArgs.Version < 19 && localName2 == "KeepTogether"))
      {
        this._keepTogether = new bool?(bool.Parse(readArgs.Reader.Value));
        if (num2 < attributeCount)
        {
          readArgs.Reader.MoveToAttribute(num2++);
          localName2 = readArgs.Reader.LocalName;
        }
        else
          flag = true;
      }
      if (!flag && (readArgs.Version >= 19 && localName2 == "keepWithNext" || readArgs.Version < 19 && localName2 == "KeepWithNext"))
      {
        this._keepWithNext = new bool?(bool.Parse(readArgs.Reader.Value));
        if (num2 < attributeCount)
        {
          readArgs.Reader.MoveToAttribute(num2++);
          localName2 = readArgs.Reader.LocalName;
        }
        else
          flag = true;
      }
      if (!flag && (readArgs.Version >= 19 && localName2 == "newPage" || readArgs.Version < 19 && localName2 == "FromNewPage"))
      {
        this._fromNewPage = new bool?(bool.Parse(readArgs.Reader.Value));
        if (num2 < attributeCount)
        {
          readArgs.Reader.MoveToAttribute(num2++);
          localName2 = readArgs.Reader.LocalName;
        }
        else
          flag = true;
      }
      if (!flag && (readArgs.Version >= 19 && localName2 == "noWrap" || readArgs.Version < 19 && localName2 == "DisableWordWrap"))
      {
        this._disableWordWrap = new bool?(bool.Parse(readArgs.Reader.Value));
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
    LogManager.AddLine("ParagraphFormat.ReadFromXml End Element not found.");
  }

  /// <summary>Прочитать одно поле из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле прочитано</returns>
  public bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    switch (readArgs.Reader.LocalName)
    {
      case "DisableFloatLines":
      case "noFloatLines":
        this.DisableFloatLines = new bool?(bool.Parse(readArgs.Reader.Value));
        return true;
      case "DisableWordWrap":
      case "noWrap":
        this.DisableWordWrap = new bool?(bool.Parse(readArgs.Reader.Value));
        return true;
      case "FromNewPage":
      case "newPage":
        this.FromNewPage = new bool?(bool.Parse(readArgs.Reader.Value));
        return true;
      case "HorzAlignment":
      case "hAlign":
        this.HorzAlignment = new Intermech.Interfaces.Document.HorzAlignment?((Intermech.Interfaces.Document.HorzAlignment) int.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture));
        return true;
      case "IdentFirstLine":
      case "identF":
        this.IdentFirstLine = new float?(float.Parse(DocumentTreeNode.ReplaceDS(readArgs.Reader.Value), (IFormatProvider) CultureInfo.InvariantCulture));
        return true;
      case "IdentLeft":
      case "identL":
        this.IdentLeft = new float?(float.Parse(DocumentTreeNode.ReplaceDS(readArgs.Reader.Value), (IFormatProvider) CultureInfo.InvariantCulture));
        return true;
      case "IdentRight":
      case "identR":
        this.IdentRight = new float?(float.Parse(DocumentTreeNode.ReplaceDS(readArgs.Reader.Value), (IFormatProvider) CultureInfo.InvariantCulture));
        return true;
      case "IntervalAfter":
      case "after":
        this.IntervalAfter = new float?(float.Parse(DocumentTreeNode.ReplaceDS(readArgs.Reader.Value), (IFormatProvider) CultureInfo.InvariantCulture));
        return true;
      case "IntervalBefore":
      case "before":
        this.IntervalBefore = new float?(float.Parse(DocumentTreeNode.ReplaceDS(readArgs.Reader.Value), (IFormatProvider) CultureInfo.InvariantCulture));
        return true;
      case "KeepTogether":
      case "noBreak":
        this.KeepTogether = new bool?(bool.Parse(readArgs.Reader.Value));
        return true;
      case "KeepWithNext":
      case "keepWithNext":
        this.KeepWithNext = new bool?(bool.Parse(readArgs.Reader.Value));
        return true;
      case "LineSpacingMethod":
      case "lsMethod":
        this.LineSpacingMethod = new Intermech.Interfaces.Document.LineSpacingMethod?((Intermech.Interfaces.Document.LineSpacingMethod) int.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture));
        return true;
      case "SpaceBetweenLines":
      case "btwLines":
        this.SpaceBetweenLines = new float?(float.Parse(DocumentTreeNode.ReplaceDS(readArgs.Reader.Value), (IFormatProvider) CultureInfo.InvariantCulture));
        return true;
      case "TextLevel":
      case "level":
        this.TextLevel = new int?(int.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture));
        return true;
      case "VertAlignment":
      case "vAlign":
        this.VertAlignment = new Intermech.Interfaces.Document.VertAlignment?((Intermech.Interfaces.Document.VertAlignment) int.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture));
        return true;
      default:
        return false;
    }
  }
}
