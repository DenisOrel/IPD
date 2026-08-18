// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.DefaultParagraphFormatConverter
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Collections;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Конвертер типа ParagraphFormat</summary>
public class DefaultParagraphFormatConverter : LocalizedExpandableObjectConverter
{
  /// <summary>Возвращает значение, показывающее, требуется ли при изменении значения
  /// этого объекта вызывать CreateInstance, чтобы создать новое значение, используя
  /// заданную контекстную информацию</summary>
  /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную информацию
  /// о формате</param>
  /// <returns>true, если при изменении значения этого объекта требуется вызывать CreateInstance,
  /// чтобы создать новое значение, false, если нет</returns>
  public override bool GetCreateInstanceSupported(ITypeDescriptorContext context) => true;

  /// <summary>Создает экземпляр типа, с которым связан этот TypeConverter,
  /// используя заданную контекстную информацию и переданный набор значений свойств
  /// для этого объекта</summary>
  /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную
  /// информацию о формате</param>
  /// <param name="propertyValues">IDictionary новых значений свойства</param>
  /// <returns>Object, представляющий данный IDictionary или пустая ссылка,
  /// если объект не может быть создан</returns>
  public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
  {
    ParagraphFormat instance = !(context.PropertyDescriptor.GetValue(context.Instance) is ParagraphFormat paragraphFormat) ? new ParagraphFormat() : paragraphFormat.Clone();
    instance.HorzAlignment = (HorzAlignment?) propertyValues[(object) "HorzAlignment"];
    instance.VertAlignment = (VertAlignment?) propertyValues[(object) "VertAlignment"];
    instance.LineSpacingMethod = (LineSpacingMethod?) propertyValues[(object) "LineSpacingMethod"];
    instance.SpaceBetweenLines = (float?) propertyValues[(object) "SpaceBetweenLines"];
    LineSpacingMethod? nullable = instance.LineSpacingMethod;
    if (nullable.HasValue && paragraphFormat != null)
    {
      LineSpacingMethod? lineSpacingMethod1 = paragraphFormat.LineSpacingMethod;
      nullable = lineSpacingMethod1;
      LineSpacingMethod? lineSpacingMethod2 = instance.LineSpacingMethod;
      if (!(nullable.GetValueOrDefault() == lineSpacingMethod2.GetValueOrDefault() & nullable.HasValue == lineSpacingMethod2.HasValue))
      {
        lineSpacingMethod2 = instance.LineSpacingMethod;
        if (lineSpacingMethod2.HasValue)
        {
          switch (lineSpacingMethod2.GetValueOrDefault())
          {
            case LineSpacingMethod.AtLeast:
              nullable = lineSpacingMethod1;
              LineSpacingMethod lineSpacingMethod3 = LineSpacingMethod.AtLeastMM;
              if (!(nullable.GetValueOrDefault() == lineSpacingMethod3 & nullable.HasValue))
              {
                nullable = lineSpacingMethod1;
                LineSpacingMethod lineSpacingMethod4 = LineSpacingMethod.ExactMM;
                if (!(nullable.GetValueOrDefault() == lineSpacingMethod4 & nullable.HasValue))
                {
                  if (!instance.SpaceBetweenLines.HasValue)
                  {
                    instance.SpaceBetweenLines = new float?(12f);
                    goto label_27;
                  }
                  goto label_27;
                }
              }
              instance.SpaceBetweenLines = new float?((float) UnitsConverter.MmToPoints(paragraphFormat.SpaceBetweenLines.Value));
              goto label_27;
            case LineSpacingMethod.AtLeastMM:
              nullable = lineSpacingMethod1;
              LineSpacingMethod lineSpacingMethod5 = LineSpacingMethod.AtLeast;
              if (!(nullable.GetValueOrDefault() == lineSpacingMethod5 & nullable.HasValue))
              {
                nullable = lineSpacingMethod1;
                LineSpacingMethod lineSpacingMethod6 = LineSpacingMethod.Exact;
                if (!(nullable.GetValueOrDefault() == lineSpacingMethod6 & nullable.HasValue))
                {
                  if (!instance.SpaceBetweenLines.HasValue)
                  {
                    instance.SpaceBetweenLines = new float?(4f);
                    goto label_27;
                  }
                  goto label_27;
                }
              }
              instance.SpaceBetweenLines = new float?((float) Math.Round((double) UnitsConverter.PointToMm(paragraphFormat.SpaceBetweenLines.Value), 2));
              goto label_27;
            case LineSpacingMethod.Exact:
              nullable = lineSpacingMethod1;
              LineSpacingMethod lineSpacingMethod7 = LineSpacingMethod.AtLeastMM;
              if (!(nullable.GetValueOrDefault() == lineSpacingMethod7 & nullable.HasValue))
              {
                nullable = lineSpacingMethod1;
                LineSpacingMethod lineSpacingMethod8 = LineSpacingMethod.ExactMM;
                if (!(nullable.GetValueOrDefault() == lineSpacingMethod8 & nullable.HasValue))
                {
                  if (!instance.SpaceBetweenLines.HasValue)
                  {
                    instance.SpaceBetweenLines = new float?(12f);
                    goto label_27;
                  }
                  goto label_27;
                }
              }
              instance.SpaceBetweenLines = new float?((float) UnitsConverter.MmToPoints(paragraphFormat.SpaceBetweenLines.Value));
              goto label_27;
            case LineSpacingMethod.ExactMM:
              nullable = lineSpacingMethod1;
              LineSpacingMethod lineSpacingMethod9 = LineSpacingMethod.AtLeast;
              if (!(nullable.GetValueOrDefault() == lineSpacingMethod9 & nullable.HasValue))
              {
                nullable = lineSpacingMethod1;
                LineSpacingMethod lineSpacingMethod10 = LineSpacingMethod.Exact;
                if (!(nullable.GetValueOrDefault() == lineSpacingMethod10 & nullable.HasValue))
                {
                  if (!instance.SpaceBetweenLines.HasValue)
                  {
                    instance.SpaceBetweenLines = new float?(4f);
                    goto label_27;
                  }
                  goto label_27;
                }
              }
              instance.SpaceBetweenLines = new float?((float) Math.Round((double) UnitsConverter.PointToMm(paragraphFormat.SpaceBetweenLines.Value), 2));
              goto label_27;
            case LineSpacingMethod.Ratio:
              if (!instance.SpaceBetweenLines.HasValue)
              {
                instance.SpaceBetweenLines = new float?(3f);
                goto label_27;
              }
              goto label_27;
          }
        }
        instance.AssignSpaceBetweenLines(new float?());
      }
    }
label_27:
    return (object) instance;
  }

  public override PropertyDescriptorCollection GetProperties(
    ITypeDescriptorContext context,
    object value,
    Attribute[] attributes)
  {
    PropertyDescriptorCollection properties = base.GetProperties(context, value, attributes);
    if (properties != null)
    {
      PropertyDescriptor propertyDescriptor1;
      if ((propertyDescriptor1 = properties.Find("TextLevel", false)) != null)
        properties.Remove(propertyDescriptor1);
      PropertyDescriptor propertyDescriptor2;
      if ((propertyDescriptor2 = properties.Find("IdentLeft", false)) != null)
        properties.Remove(propertyDescriptor2);
      PropertyDescriptor propertyDescriptor3;
      if ((propertyDescriptor3 = properties.Find("IdentRight", false)) != null)
        properties.Remove(propertyDescriptor3);
      PropertyDescriptor propertyDescriptor4;
      if ((propertyDescriptor4 = properties.Find("IdentFirstLine", false)) != null)
        properties.Remove(propertyDescriptor4);
      PropertyDescriptor propertyDescriptor5;
      if ((propertyDescriptor5 = properties.Find("IntervalBefore", false)) != null)
        properties.Remove(propertyDescriptor5);
      PropertyDescriptor propertyDescriptor6;
      if ((propertyDescriptor6 = properties.Find("IntervalAfter", false)) != null)
        properties.Remove(propertyDescriptor6);
      PropertyDescriptor propertyDescriptor7;
      if ((propertyDescriptor7 = properties.Find("DisableFloatLines", false)) != null)
        properties.Remove(propertyDescriptor7);
      PropertyDescriptor propertyDescriptor8;
      if ((propertyDescriptor8 = properties.Find("KeepTogether", false)) != null)
        properties.Remove(propertyDescriptor8);
      PropertyDescriptor propertyDescriptor9;
      if ((propertyDescriptor9 = properties.Find("KeepWithNext", false)) != null)
        properties.Remove(propertyDescriptor9);
      PropertyDescriptor propertyDescriptor10;
      if ((propertyDescriptor10 = properties.Find("FromNewPage", false)) != null)
        properties.Remove(propertyDescriptor10);
      PropertyDescriptor propertyDescriptor11;
      if ((propertyDescriptor11 = properties.Find("DisableWordWrap", false)) != null)
        properties.Remove(propertyDescriptor11);
      if (context.PropertyDescriptor != null && context.PropertyDescriptor.IsReadOnly)
        CustomPropertyDescriptor.SetReadOnlyProperties(properties);
    }
    return properties;
  }
}
