// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.IConditionDisplayService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces.SelectionService;
using Intermech.Kernel.Search;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Navigator.Interfaces;

public interface IConditionDisplayService
{
  string ConvertConditionValueToString(
    IConditionDataProvider dataProvider,
    RelationalOperators relationalOperator,
    object attributeID,
    SelectionParameterTypes selectionParameterTypes,
    object conditionValue,
    Dictionary<object, string> possibleValues,
    object typeID);

  string ConvertInputObjectAttributeToString(
    IConditionDataProvider dataProvider,
    InputObjectAttribute objValue);

  void RegisterValueToStringConverter(IValueToStringConverter converter);
}
