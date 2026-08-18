// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.ParagraphFormatConverter
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.Localization;
using System;
using System.Collections;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Конвертер типа ParagraphFormat</summary>
public class ParagraphFormatConverter : LocalizedExpandableObjectConverter
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
    if (context == null)
      throw new ArgumentNullException(nameof (context));
    if (propertyValues == null)
      throw new ArgumentNullException(nameof (propertyValues));
    ParagraphFormat instance = !(context.PropertyDescriptor?.GetValue(context.Instance) is ParagraphFormat paragraphFormat) ? new ParagraphFormat() : paragraphFormat.Clone();
    instance.HorzAlignment = (HorzAlignment?) propertyValues[(object) "HorzAlignment"];
    instance.VertAlignment = (VertAlignment?) propertyValues[(object) "VertAlignment"];
    instance.TextLevel = (int?) propertyValues[(object) "TextLevel"];
    instance.IdentLeft = (float?) propertyValues[(object) "IdentLeft"];
    instance.IdentRight = (float?) propertyValues[(object) "IdentRight"];
    instance.IdentFirstLine = (float?) propertyValues[(object) "IdentFirstLine"];
    instance.IntervalBefore = (float?) propertyValues[(object) "IntervalBefore"];
    instance.IntervalAfter = (float?) propertyValues[(object) "IntervalAfter"];
    instance.LineSpacingMethod = (LineSpacingMethod?) propertyValues[(object) "LineSpacingMethod"];
    instance.SpaceBetweenLines = (float?) propertyValues[(object) "SpaceBetweenLines"];
    instance.DisableFloatLines = (bool?) propertyValues[(object) "DisableFloatLines"];
    instance.KeepTogether = (bool?) propertyValues[(object) "KeepTogether"];
    instance.KeepWithNext = (bool?) propertyValues[(object) "KeepWithNext"];
    instance.FromNewPage = (bool?) propertyValues[(object) "FromNewPage"];
    instance.DisableWordWrap = (bool?) propertyValues[(object) "DisableWordWrap"];
    if (propertyValues.Count != 15)
      LogManager.AddLine(LocalizationHolder.rm.GetString("Interfaces.Document_78"));
    if (instance.LineSpacingMethod.HasValue && paragraphFormat != null)
    {
      LineSpacingMethod? lineSpacingMethod1 = paragraphFormat.LineSpacingMethod;
      LineSpacingMethod? nullable1 = lineSpacingMethod1;
      LineSpacingMethod? lineSpacingMethod2 = instance.LineSpacingMethod;
      if (!(nullable1.GetValueOrDefault() == lineSpacingMethod2.GetValueOrDefault() & nullable1.HasValue == lineSpacingMethod2.HasValue))
      {
        lineSpacingMethod2 = instance.LineSpacingMethod;
        if (lineSpacingMethod2.HasValue)
        {
          switch (lineSpacingMethod2.GetValueOrDefault())
          {
            case LineSpacingMethod.AtLeast:
              LineSpacingMethod? nullable2 = lineSpacingMethod1;
              LineSpacingMethod lineSpacingMethod3 = LineSpacingMethod.AtLeastMM;
              if (!(nullable2.GetValueOrDefault() == lineSpacingMethod3 & nullable2.HasValue))
              {
                nullable2 = lineSpacingMethod1;
                LineSpacingMethod lineSpacingMethod4 = LineSpacingMethod.ExactMM;
                if (!(nullable2.GetValueOrDefault() == lineSpacingMethod4 & nullable2.HasValue))
                {
                  if (!instance.SpaceBetweenLines.HasValue)
                  {
                    instance.SpaceBetweenLines = new float?(12f);
                    goto label_33;
                  }
                  goto label_33;
                }
              }
              instance.SpaceBetweenLines = new float?((float) UnitsConverter.MmToPoints(paragraphFormat.SpaceBetweenLines.Value));
              goto label_33;
            case LineSpacingMethod.AtLeastMM:
              LineSpacingMethod? nullable3 = lineSpacingMethod1;
              LineSpacingMethod lineSpacingMethod5 = LineSpacingMethod.AtLeast;
              if (!(nullable3.GetValueOrDefault() == lineSpacingMethod5 & nullable3.HasValue))
              {
                nullable3 = lineSpacingMethod1;
                LineSpacingMethod lineSpacingMethod6 = LineSpacingMethod.Exact;
                if (!(nullable3.GetValueOrDefault() == lineSpacingMethod6 & nullable3.HasValue))
                {
                  if (!instance.SpaceBetweenLines.HasValue)
                  {
                    instance.SpaceBetweenLines = new float?(4f);
                    goto label_33;
                  }
                  goto label_33;
                }
              }
              instance.SpaceBetweenLines = new float?((float) Math.Round((double) UnitsConverter.PointToMm(paragraphFormat.SpaceBetweenLines.Value), 2));
              goto label_33;
            case LineSpacingMethod.Exact:
              LineSpacingMethod? nullable4 = lineSpacingMethod1;
              LineSpacingMethod lineSpacingMethod7 = LineSpacingMethod.AtLeastMM;
              if (!(nullable4.GetValueOrDefault() == lineSpacingMethod7 & nullable4.HasValue))
              {
                nullable4 = lineSpacingMethod1;
                LineSpacingMethod lineSpacingMethod8 = LineSpacingMethod.ExactMM;
                if (!(nullable4.GetValueOrDefault() == lineSpacingMethod8 & nullable4.HasValue))
                {
                  if (!instance.SpaceBetweenLines.HasValue)
                  {
                    instance.SpaceBetweenLines = new float?(12f);
                    goto label_33;
                  }
                  goto label_33;
                }
              }
              instance.SpaceBetweenLines = new float?((float) UnitsConverter.MmToPoints(paragraphFormat.SpaceBetweenLines.Value));
              goto label_33;
            case LineSpacingMethod.ExactMM:
              LineSpacingMethod? nullable5 = lineSpacingMethod1;
              LineSpacingMethod lineSpacingMethod9 = LineSpacingMethod.AtLeast;
              if (!(nullable5.GetValueOrDefault() == lineSpacingMethod9 & nullable5.HasValue))
              {
                nullable5 = lineSpacingMethod1;
                LineSpacingMethod lineSpacingMethod10 = LineSpacingMethod.Exact;
                if (!(nullable5.GetValueOrDefault() == lineSpacingMethod10 & nullable5.HasValue))
                {
                  if (!instance.SpaceBetweenLines.HasValue)
                  {
                    instance.SpaceBetweenLines = new float?(4f);
                    goto label_33;
                  }
                  goto label_33;
                }
              }
              instance.SpaceBetweenLines = new float?((float) Math.Round((double) UnitsConverter.PointToMm(paragraphFormat.SpaceBetweenLines.Value), 2));
              goto label_33;
            case LineSpacingMethod.Ratio:
              if (!instance.SpaceBetweenLines.HasValue)
              {
                instance.SpaceBetweenLines = new float?(3f);
                goto label_33;
              }
              goto label_33;
          }
        }
        instance.AssignSpaceBetweenLines(new float?());
      }
    }
label_33:
    return (object) instance;
  }
}
