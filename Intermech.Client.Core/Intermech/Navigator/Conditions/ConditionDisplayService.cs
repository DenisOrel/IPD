
// Type: Intermech.Navigator.Conditions.ConditionDisplayService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Interfaces.SelectionService;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Navigator.Conditions;

internal sealed class ConditionDisplayService : IConditionDisplayService
{
  /// <summary>
  /// Допустимая длина стоки при расшифровке списка значений
  /// </summary>
  private readonly int _maxListValuesCaptionLenght = 50;
  /// <summary>
  /// Строка, ограничивающая значение (например, двойная кавычка: "Val" ) по-умолчанию
  /// </summary>
  private readonly string _listValuesCaptionDelimiter = "\"";
  /// <summary>
  /// Строка, разделяющая значения (например, запятая и пробел: "Val_1", "Val_2" ) по-умолчанию
  /// </summary>
  private readonly string _listValuesCaptionSeparator = ", ";
  private Dictionary<object, IValueToStringConverter> _selectionParameterTypeConverters;

  public ConditionDisplayService() => this.CreateSelectionParameterTypeConverters();

  public string ConvertConditionValueToString(
    IConditionDataProvider dataProvider,
    RelationalOperators relationalOperator,
    object attributeID,
    SelectionParameterTypes selectionParameterType,
    object conditionValue,
    Dictionary<object, string> possibleValues,
    object typeID)
  {
    if (conditionValue == null)
      return string.Empty;
    string str = string.Empty;
    if (conditionValue is InputObjectAttribute)
      str = this.ConvertValue((object) typeof (InputObjectAttribute), dataProvider, conditionValue, typeID);
    else if (conditionValue is IList)
      str = $"[{this.DecodeValueList(dataProvider, relationalOperator, attributeID, (IList) conditionValue, selectionParameterType, possibleValues, typeID)}]";
    else if (possibleValues != null && possibleValues.Count > 0 && possibleValues.ContainsKey(conditionValue))
      str = possibleValues[conditionValue];
    else if (Array.Exists<RelationalOperators>(SelectionParameter.StringOperators, (Predicate<RelationalOperators>) (x => x.Equals((object) relationalOperator))))
    {
      str = Convert.ToString(conditionValue);
    }
    else
    {
      bool flag = false;
      int attributeId = dataProvider.GetAttributeID(attributeID);
      if (attributeId != 0)
      {
        IAttributePropertyDescriber describer = ServicesManager.GetService<IAttributePropertyDescriberService>().GetDescriber(attributeId);
        if (describer != null)
        {
          object propDescriptorValue = describer.GetPropDescriptorValue((IElementInfo) null, attributeId, conditionValue);
          if (propDescriptorValue != null)
          {
            str = propDescriptorValue.ToString();
            flag = true;
          }
        }
      }
      if (!flag)
        str = this.ConvertValue((object) selectionParameterType, dataProvider, conditionValue, typeID);
    }
    return str;
  }

  private string ConvertValue(
    object converterID,
    IConditionDataProvider dataProvider,
    object conditionValue,
    object typeID)
  {
    IValueToStringConverter toStringConverter;
    return this._selectionParameterTypeConverters.TryGetValue(converterID, out toStringConverter) ? toStringConverter.ConvertValue(dataProvider, conditionValue, typeID) : Convert.ToString(conditionValue);
  }

  /// <summary>
  /// Преобразование списка значений к строке вида "Val_1", "Val_2", ...
  /// </summary>
  private string DecodeValueList(
    IConditionDataProvider dataProvider,
    RelationalOperators relationalOperator,
    object attributeID,
    IList values,
    SelectionParameterTypes selectionParameterType,
    Dictionary<object, string> possibleValues,
    object typeID)
  {
    return this.DecodeValueList(dataProvider, relationalOperator, attributeID, values, selectionParameterType, possibleValues, typeID, this._listValuesCaptionDelimiter, this._listValuesCaptionSeparator);
  }

  /// <summary>Преобразование списка значений к строке</summary>
  private string DecodeValueList(
    IConditionDataProvider dataProvider,
    RelationalOperators relationalOperator,
    object attributeID,
    IList values,
    SelectionParameterTypes selectionParameterType,
    Dictionary<object, string> possibleValues,
    object typeID,
    string delimiter,
    string separator)
  {
    string str1 = "";
    string str2 = delimiter;
    string str3 = str2 + separator + str2;
    for (int index = 0; index < values.Count; ++index)
    {
      string str4 = str1 + (index == 0 ? str2 : str3);
      string str5 = this.ConvertConditionValueToString(dataProvider, relationalOperator, attributeID, selectionParameterType, values[index], possibleValues, typeID);
      if (!str5.Equals(string.Empty) && str5.Length > this._maxListValuesCaptionLenght - str4.Length)
        str5 = str5.Substring(0, this._maxListValuesCaptionLenght - str4.Length) + "...";
      str1 = str4 + str5;
      if (index == values.Count - 1)
        str1 += str2;
      else if (str1.Length >= this._maxListValuesCaptionLenght - str3.Length)
      {
        str1 = $"{str1}{str2}, ...";
        break;
      }
    }
    return str1;
  }

  private void CreateSelectionParameterTypeConverters()
  {
    this._selectionParameterTypeConverters = new Dictionary<object, IValueToStringConverter>();
    this.RegisterValueToStringConverter((IValueToStringConverter) new DateTimeValueToStringConverter());
    this.RegisterValueToStringConverter((IValueToStringConverter) new StringValueToStringConverter());
    this.RegisterValueToStringConverter((IValueToStringConverter) new FloatValueToStringConverter());
    this.RegisterValueToStringConverter((IValueToStringConverter) new NumberValueToStringConverter());
    this.RegisterValueToStringConverter((IValueToStringConverter) new BoolValueToStringConverter());
    this.RegisterValueToStringConverter((IValueToStringConverter) new UserValueToStringConverter(SelectionParameterTypes.sptUser));
    this.RegisterValueToStringConverter((IValueToStringConverter) new CheckOutByValueToStringConverter());
    this.RegisterValueToStringConverter((IValueToStringConverter) new ObjectValueToStringConverter());
    this.RegisterValueToStringConverter((IValueToStringConverter) new ObjectTypeValueToStringConverter());
    this.RegisterValueToStringConverter((IValueToStringConverter) new SubjectAreaValueToStringConverter());
    this.RegisterValueToStringConverter((IValueToStringConverter) new LCLevelValueToStringConverter());
    this.RegisterValueToStringConverter((IValueToStringConverter) new LCStepValueToStringConverter());
    this.RegisterValueToStringConverter((IValueToStringConverter) new RelationTypeValueToStringConverter());
    this.RegisterValueToStringConverter((IValueToStringConverter) new GuidValueToStringConverter());
    this.RegisterValueToStringConverter((IValueToStringConverter) new MeasuredValueToStringConverter());
    this.RegisterValueToStringConverter((IValueToStringConverter) new SiteIDValueToStringConverter());
    this.RegisterValueToStringConverter((IValueToStringConverter) new InputObjectAttributeToStringConverter());
    this.RegisterValueToStringConverter((IValueToStringConverter) new HandlerValueToStringConverter());
    this.RegisterValueToStringConverter((IValueToStringConverter) new RelationOpValueValueToStringConverter());
  }

  public void RegisterValueToStringConverter(IValueToStringConverter converter)
  {
    if (this._selectionParameterTypeConverters.ContainsKey(converter.ConverterID))
      throw new Exception($"Для {converter.ConverterID} уже зарегистрирован конвертер!");
    this._selectionParameterTypeConverters.Add(converter.ConverterID, converter);
  }

  public string ConvertInputObjectAttributeToString(
    IConditionDataProvider dataProvider,
    InputObjectAttribute objValue)
  {
    return this.ConvertValue((object) typeof (InputObjectAttribute), dataProvider, (object) objValue, (object) null);
  }
}
