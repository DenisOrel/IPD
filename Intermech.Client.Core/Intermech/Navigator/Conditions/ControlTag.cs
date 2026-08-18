
// Type: Intermech.Navigator.Conditions.ControlTag
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.SelectionService;


namespace Intermech.Navigator.Conditions;

/// <summary>Доп данные, которые ложим в тэг контролу</summary>
internal sealed class ControlTag
{
  /// <summary>
  /// Флаг того, что в качестве значения контрола выступает значение value1 из условия выборки
  /// </summary>
  public bool IsFirstValue;
  public int AttributeID;
  public object CurrentValue;
  public SelectionParameterTypes ParamType;

  public ControlTag(bool isFirstValue) => this.IsFirstValue = isFirstValue;

  public ControlTag(
    bool isFirstValue,
    int attributeID,
    object currentValue,
    SelectionParameterTypes paramType)
    : this(isFirstValue)
  {
    this.AttributeID = attributeID;
    this.CurrentValue = currentValue;
    this.ParamType = paramType;
  }
}
