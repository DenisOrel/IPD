// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.LineSpacingConverter
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Конвертер типа Float для полей с размерностью в миллиметрах.</summary>
public class LineSpacingConverter : FloatConverter
{
  /// <summary>Возвращает значение, показывающее, может ли этот конвертер преобразовать данный объект в заданный тип, используя заданную контекстную информацию</summary>
  /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную информацию о формате</param>
  /// <param name="destinationType">Type, представляющий тип, в который требуется сделать преобразование</param>
  /// <returns>true, если конвертер может осуществить такое преобразование, false, если нет</returns>
  public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
  {
    return destinationType == typeof (string) || base.CanConvertTo(context, destinationType);
  }

  /// <summary>Преобразует данное значение в заданный тип, используя заданные
  /// контекстную информацию и информацию о культурной среде</summary>
  /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную информацию о формате</param>
  /// <param name="culture">Объект CultureInfo. Если передается значение пустая ссылка,
  /// то предполагается использование информации о культурной среде</param>
  /// <param name="value">Объект Object, который нужно преобразовать</param>
  /// <param name="destinationType">Type, в который требуется преобразовать параметр value</param>
  /// <returns>Объект Object, представляющий преобразованное значение</returns>
  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    if (destinationType == (Type) null)
      throw new ArgumentNullException(nameof (destinationType));
    if (!(destinationType == typeof (string)) || !(value is float num))
      return base.ConvertTo(context, culture, value, destinationType);
    ParagraphFormat instance = context.Instance as ParagraphFormat;
    string str = num.ToString();
    if (instance != null)
    {
      LineSpacingMethod? lineSpacingMethod = instance.LineSpacingMethod;
      if (lineSpacingMethod.HasValue)
      {
        switch (lineSpacingMethod.GetValueOrDefault())
        {
          case LineSpacingMethod.AtLeast:
            str += LocalizationHolder.rm.GetString("Interfaces.Document_42");
            break;
          case LineSpacingMethod.AtLeastMM:
            str += LocalizationHolder.rm.GetString("Interfaces.Document_43");
            break;
          case LineSpacingMethod.Exact:
            str += LocalizationHolder.rm.GetString("Interfaces.Document_44");
            break;
          case LineSpacingMethod.ExactMM:
            str += LocalizationHolder.rm.GetString("Interfaces.Document_45");
            break;
        }
      }
    }
    return (object) str;
  }

  /// <summary>Преобразует данный объект в тип этого конвертера,
  /// используя заданную контекстную информацию и информацию о культурной среде</summary>
  /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную информацию о формате</param>
  /// <param name="culture">Объект CultureInfo, который нужно использовать в качестве текущей культурной среды</param>
  /// <param name="value">Объект Object, который нужно преобразовать</param>
  /// <returns>Объект Object, представляющий преобразованное значение</returns>
  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    if (context.Instance is ParagraphFormat instance)
    {
      string text = value as string;
      double number;
      string textAfterNumber;
      NumberParserAdvanced.ParseNumber(text, true, out number, out string _, out textAfterNumber);
      textAfterNumber = textAfterNumber.Trim();
      if (text != null)
      {
        LineSpacingMethod? lineSpacingMethod1;
        if (textAfterNumber != "")
        {
          lineSpacingMethod1 = instance.LineSpacingMethod;
          LineSpacingMethod lineSpacingMethod2 = LineSpacingMethod.Ratio;
          if (lineSpacingMethod1.GetValueOrDefault() == lineSpacingMethod2 & lineSpacingMethod1.HasValue)
            throw new ArgumentException(LocalizationHolder.rm.GetString("Interfaces.Document_46") + value);
        }
        double num1;
        switch (textAfterNumber)
        {
          case "":
            num1 = 1.0;
            lineSpacingMethod1 = instance.LineSpacingMethod;
            LineSpacingMethod lineSpacingMethod3 = LineSpacingMethod.Ratio_1;
            if (!(lineSpacingMethod1.GetValueOrDefault() == lineSpacingMethod3 & lineSpacingMethod1.HasValue))
            {
              lineSpacingMethod1 = instance.LineSpacingMethod;
              LineSpacingMethod lineSpacingMethod4 = LineSpacingMethod.Ratio_1_5;
              if (!(lineSpacingMethod1.GetValueOrDefault() == lineSpacingMethod4 & lineSpacingMethod1.HasValue))
              {
                lineSpacingMethod1 = instance.LineSpacingMethod;
                LineSpacingMethod lineSpacingMethod5 = LineSpacingMethod.Ratio_2;
                if (!(lineSpacingMethod1.GetValueOrDefault() == lineSpacingMethod5 & lineSpacingMethod1.HasValue))
                  break;
              }
            }
            instance.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.Ratio);
            break;
          case "cm":
            num1 = 10.0;
            lineSpacingMethod1 = instance.LineSpacingMethod;
            LineSpacingMethod lineSpacingMethod6 = LineSpacingMethod.Exact;
            if (lineSpacingMethod1.GetValueOrDefault() == lineSpacingMethod6 & lineSpacingMethod1.HasValue)
              instance.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.ExactMM);
            lineSpacingMethod1 = instance.LineSpacingMethod;
            LineSpacingMethod lineSpacingMethod7 = LineSpacingMethod.AtLeast;
            if (lineSpacingMethod1.GetValueOrDefault() == lineSpacingMethod7 & lineSpacingMethod1.HasValue)
            {
              instance.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.AtLeastMM);
              break;
            }
            break;
          case "m":
            num1 = 1000.0;
            lineSpacingMethod1 = instance.LineSpacingMethod;
            LineSpacingMethod lineSpacingMethod8 = LineSpacingMethod.Exact;
            if (lineSpacingMethod1.GetValueOrDefault() == lineSpacingMethod8 & lineSpacingMethod1.HasValue)
              instance.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.ExactMM);
            lineSpacingMethod1 = instance.LineSpacingMethod;
            LineSpacingMethod lineSpacingMethod9 = LineSpacingMethod.AtLeast;
            if (lineSpacingMethod1.GetValueOrDefault() == lineSpacingMethod9 & lineSpacingMethod1.HasValue)
            {
              instance.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.AtLeastMM);
              break;
            }
            break;
          case "mm":
            num1 = 1.0;
            lineSpacingMethod1 = instance.LineSpacingMethod;
            LineSpacingMethod lineSpacingMethod10 = LineSpacingMethod.Exact;
            if (lineSpacingMethod1.GetValueOrDefault() == lineSpacingMethod10 & lineSpacingMethod1.HasValue)
              instance.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.ExactMM);
            lineSpacingMethod1 = instance.LineSpacingMethod;
            LineSpacingMethod lineSpacingMethod11 = LineSpacingMethod.AtLeast;
            if (lineSpacingMethod1.GetValueOrDefault() == lineSpacingMethod11 & lineSpacingMethod1.HasValue)
            {
              instance.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.AtLeastMM);
              break;
            }
            break;
          case "pt":
            num1 = 1.0;
            lineSpacingMethod1 = instance.LineSpacingMethod;
            LineSpacingMethod lineSpacingMethod12 = LineSpacingMethod.ExactMM;
            if (lineSpacingMethod1.GetValueOrDefault() == lineSpacingMethod12 & lineSpacingMethod1.HasValue)
              instance.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.Exact);
            lineSpacingMethod1 = instance.LineSpacingMethod;
            LineSpacingMethod lineSpacingMethod13 = LineSpacingMethod.AtLeastMM;
            if (lineSpacingMethod1.GetValueOrDefault() == lineSpacingMethod13 & lineSpacingMethod1.HasValue)
            {
              instance.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.AtLeast);
              break;
            }
            break;
          case "м":
            num1 = 1000.0;
            lineSpacingMethod1 = instance.LineSpacingMethod;
            LineSpacingMethod lineSpacingMethod14 = LineSpacingMethod.Exact;
            if (lineSpacingMethod1.GetValueOrDefault() == lineSpacingMethod14 & lineSpacingMethod1.HasValue)
              instance.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.ExactMM);
            lineSpacingMethod1 = instance.LineSpacingMethod;
            LineSpacingMethod lineSpacingMethod15 = LineSpacingMethod.AtLeast;
            if (lineSpacingMethod1.GetValueOrDefault() == lineSpacingMethod15 & lineSpacingMethod1.HasValue)
            {
              instance.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.AtLeastMM);
              break;
            }
            break;
          case "мм":
            num1 = 1.0;
            lineSpacingMethod1 = instance.LineSpacingMethod;
            LineSpacingMethod lineSpacingMethod16 = LineSpacingMethod.Exact;
            if (lineSpacingMethod1.GetValueOrDefault() == lineSpacingMethod16 & lineSpacingMethod1.HasValue)
              instance.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.ExactMM);
            lineSpacingMethod1 = instance.LineSpacingMethod;
            LineSpacingMethod lineSpacingMethod17 = LineSpacingMethod.AtLeast;
            if (lineSpacingMethod1.GetValueOrDefault() == lineSpacingMethod17 & lineSpacingMethod1.HasValue)
            {
              instance.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.AtLeastMM);
              break;
            }
            break;
          case "пт":
            num1 = 1.0;
            lineSpacingMethod1 = instance.LineSpacingMethod;
            LineSpacingMethod lineSpacingMethod18 = LineSpacingMethod.ExactMM;
            if (lineSpacingMethod1.GetValueOrDefault() == lineSpacingMethod18 & lineSpacingMethod1.HasValue)
              instance.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.Exact);
            lineSpacingMethod1 = instance.LineSpacingMethod;
            LineSpacingMethod lineSpacingMethod19 = LineSpacingMethod.AtLeastMM;
            if (lineSpacingMethod1.GetValueOrDefault() == lineSpacingMethod19 & lineSpacingMethod1.HasValue)
            {
              instance.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.AtLeast);
              break;
            }
            break;
          case "см":
            num1 = 10.0;
            lineSpacingMethod1 = instance.LineSpacingMethod;
            LineSpacingMethod lineSpacingMethod20 = LineSpacingMethod.Exact;
            if (lineSpacingMethod1.GetValueOrDefault() == lineSpacingMethod20 & lineSpacingMethod1.HasValue)
              instance.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.ExactMM);
            lineSpacingMethod1 = instance.LineSpacingMethod;
            LineSpacingMethod lineSpacingMethod21 = LineSpacingMethod.AtLeast;
            if (lineSpacingMethod1.GetValueOrDefault() == lineSpacingMethod21 & lineSpacingMethod1.HasValue)
            {
              instance.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.AtLeastMM);
              break;
            }
            break;
          default:
            throw new ArgumentException(LocalizationHolder.rm.GetString("Interfaces.Document_51") + value);
        }
        lineSpacingMethod1 = instance.LineSpacingMethod;
        int num2;
        if (lineSpacingMethod1.HasValue)
        {
          switch (lineSpacingMethod1.GetValueOrDefault())
          {
            case LineSpacingMethod.AtLeast:
            case LineSpacingMethod.Exact:
              num2 = 0;
              goto label_48;
            case LineSpacingMethod.AtLeastMM:
            case LineSpacingMethod.ExactMM:
              num2 = 1;
              goto label_48;
          }
        }
        num2 = 2;
label_48:
        double num3 = number * num1;
        if (num2 == 0)
          num3 = 0.25 * (double) (int) Math.Round(num3 / 0.25);
        return (object) (float) Math.Round(num3, 2);
      }
    }
    return base.ConvertFrom(context, culture, value);
  }
}
