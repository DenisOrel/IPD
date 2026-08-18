
// Type: Intermech.Navigator.Classifiers.ClassifierAttribute
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;


namespace Intermech.Navigator.Classifiers;

/// <summary>Расчетная формула</summary>
public class ClassifierAttribute
{
  /// <summary>Информация по атрибуту</summary>
  public CalcFormulaAttribute AttributeValue;
  /// <summary>Формула</summary>
  public MyElement Formula;
  /// <summary>Контроль величины расчитываемого значения в формуле</summary>
  public bool SizeControl;
  /// <summary>Использовать пропущенные номера</summary>
  public bool UseMissed;
  /// <summary>Действие</summary>
  public ClassifierAttributesAction Action;

  public ClassifierAttribute(
    CalcFormulaAttribute attributeValue,
    MyElement formula,
    bool sizeControl)
    : this(attributeValue, formula, sizeControl, false)
  {
  }

  public ClassifierAttribute(
    CalcFormulaAttribute attributeValue,
    MyElement formula,
    bool sizeControl,
    bool useMissed)
  {
    this.AttributeValue = attributeValue;
    this.Formula = formula;
    this.SizeControl = sizeControl;
    this.UseMissed = useMissed;
  }

  public ClassifierAttribute(
    CalcFormulaAttribute AttributeValue,
    MyElement Formula,
    bool sizeControl,
    bool useMissed,
    ClassifierAttributesAction action)
    : this(AttributeValue, Formula, sizeControl, useMissed)
  {
    this.Action = action;
  }
}
