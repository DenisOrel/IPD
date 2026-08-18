
// Type: Intermech.Navigator.Conditions.ControlsHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.SelectionService;
using Intermech.Kernel.Search;
using Intermech.Navigator.SelectionView;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.Conditions;

internal static class ControlsHelper
{
  /// <summary>
  /// Определение количества параметров, обрабатываемых реляционным оператором, по его типу
  /// </summary>
  /// <param name="relationalOperator">Оператор отношений</param>
  /// <returns>Режим количаства операторов</returns>
  public static RelationOperatorValueMode GetRelationOperatorValueMode(
    RelationalOperators relationalOperator)
  {
    switch (relationalOperator)
    {
      case RelationalOperators.None:
      case RelationalOperators.Empty:
      case RelationalOperators.NotEmpty:
      case RelationalOperators.EntersIn:
      case RelationalOperators.ConsistFrom:
      case RelationalOperators.NotEntersInType:
      case RelationalOperators.EntersInType:
      case RelationalOperators.ConsistFromType:
      case RelationalOperators.NotConsistFromType:
      case RelationalOperators.NotExistsOrEmpty:
        return RelationOperatorValueMode.rovmNone;
      case RelationalOperators.Equal:
      case RelationalOperators.NotEqual:
      case RelationalOperators.Greater:
      case RelationalOperators.GreaterOrEqual:
      case RelationalOperators.Less:
      case RelationalOperators.LessOrEqual:
      case RelationalOperators.Substring:
      case RelationalOperators.StartString:
      case RelationalOperators.EndString:
      case RelationalOperators.NotSubstring:
      case RelationalOperators.NotStartString:
      case RelationalOperators.NotEndString:
      case RelationalOperators.LastNDays:
      case RelationalOperators.StringTemplate:
      case RelationalOperators.NextNDays:
      case RelationalOperators.Linked:
      case RelationalOperators.NotLinked:
        return RelationOperatorValueMode.rovmOne;
      case RelationalOperators.Between:
      case RelationalOperators.NotBetween:
        return RelationOperatorValueMode.rovmTwo;
      case RelationalOperators.In:
      case RelationalOperators.NotIn:
        return RelationOperatorValueMode.rovmMulti;
      default:
        return RelationOperatorValueMode.rovmNone;
    }
  }

  /// <summary>
  /// установка флажков определяющих видимость элементов управления
  /// </summary>
  /// <param name="rovm"></param>
  /// <returns></returns>
  public static ShowValueMode GetValueMode(
    RelationOperatorValueMode rovm,
    SelectionParameterTypes paramType,
    RelationalOperators currentOperator,
    bool possibleValuesPresent)
  {
    ShowValueMode valueMode = ShowValueMode.svmNone;
    if (possibleValuesPresent)
    {
      switch (rovm)
      {
        case RelationOperatorValueMode.rovmOne:
        case RelationOperatorValueMode.rovmTwo:
          valueMode |= ShowValueMode.svmList;
          break;
        case RelationOperatorValueMode.rovmMulti:
          valueMode |= ShowValueMode.svmListMulti;
          break;
      }
    }
    else
    {
      switch (rovm)
      {
        case RelationOperatorValueMode.rovmNone:
          goto label_14;
        case RelationOperatorValueMode.rovmMulti:
          valueMode |= ShowValueMode.svmMulti;
          break;
      }
      switch (paramType)
      {
        case SelectionParameterTypes.sptString:
        case SelectionParameterTypes.sptFile:
        case SelectionParameterTypes.sptBlob:
          valueMode |= ShowValueMode.svmString;
          break;
        case SelectionParameterTypes.sptNumber:
        case SelectionParameterTypes.sptFloat:
          valueMode |= ShowValueMode.svmNumber;
          break;
        case SelectionParameterTypes.sptBool:
          valueMode |= ShowValueMode.svmBool;
          break;
        case SelectionParameterTypes.sptDate:
          if (currentOperator != RelationalOperators.LastNDays && currentOperator != RelationalOperators.NextNDays)
          {
            valueMode |= ShowValueMode.svmDate;
            break;
          }
          valueMode |= ShowValueMode.svmNumber;
          break;
        case SelectionParameterTypes.sptSiteID:
        case SelectionParameterTypes.sptObject:
        case SelectionParameterTypes.sptCheckOutBy:
        case SelectionParameterTypes.sptUser:
        case SelectionParameterTypes.sptObjectType:
        case SelectionParameterTypes.sptLifecycleLevel:
        case SelectionParameterTypes.sptSubjectArea:
        case SelectionParameterTypes.sptLinkType:
        case SelectionParameterTypes.sptLifecycleStep:
        case SelectionParameterTypes.sptGlobalID:
        case SelectionParameterTypes.sptMeasured:
        case SelectionParameterTypes.sptFormula:
          valueMode |= ShowValueMode.svmObj;
          break;
      }
    }
label_14:
    return valueMode;
  }

  public static int[] GetObjectTypeFilterForInnerForm(object value)
  {
    if (value is List<object>)
      return ((List<object>) value).ConvertAll<int>((Converter<object, int>) (item => (int) item)).ToArray();
    return new int[1]{ (int) value };
  }
}
