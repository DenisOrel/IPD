
// Type: Intermech.Navigator.SelectionView.EditorHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.SelectionService;
using Intermech.Kernel.Search;


namespace Intermech.Navigator.SelectionView;

internal static class EditorHelper
{
  public static ColumnContents GetColumnContents(SelectionParameterTypes type)
  {
    switch (type)
    {
      case SelectionParameterTypes.sptObject:
      case SelectionParameterTypes.sptCheckOutBy:
      case SelectionParameterTypes.sptUser:
      case SelectionParameterTypes.sptLinkType:
      case SelectionParameterTypes.sptLifecycleStep:
        return ColumnContents.ID;
      case SelectionParameterTypes.sptMeasured:
        return ColumnContents.Value;
      case SelectionParameterTypes.sptFile:
      case SelectionParameterTypes.sptBlob:
        return ColumnContents.String;
      default:
        return ColumnContents.Text;
    }
  }

  /// <summary>
  /// Локальная функция для дополнительного контроля допустимых реляционных
  /// операторов системных атрибутов
  /// </summary>
  /// <param name="aAttributeID">указатель на атрибут</param>
  /// <param name="aOperator">реляционнай оператор, который надо проверить на "разрешенность"</param>
  /// <returns>Tсли оператор разрешен, то true, иначе false</returns>
  public static bool IsEnabledOperator(int aAttributeID, RelationalOperators aOperator)
  {
    switch (aAttributeID)
    {
      case -50:
        return true;
      case -24:
      case -22:
      case -21:
      case -20:
      case -10:
      case -5:
      case -3:
      case -2:
        return aOperator == RelationalOperators.Equal || aOperator == RelationalOperators.NotEqual || aOperator == RelationalOperators.Greater || aOperator == RelationalOperators.GreaterOrEqual || aOperator == RelationalOperators.Less || aOperator == RelationalOperators.LessOrEqual;
      case -23:
      case -14:
      case -12:
      case -11:
      case -9:
      case -8:
      case -7:
      case -6:
      case -4:
        return aOperator == RelationalOperators.Equal || aOperator == RelationalOperators.NotEqual;
      default:
        return true;
    }
  }

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

  public static ShowValueMode GetShowValueMode(
    bool attributeValueFromList,
    SelectionParameterTypes objRefTypes,
    RelationOperatorValueMode rovm,
    RelationalOperators value)
  {
    ShowValueMode showValueMode = ShowValueMode.svmNone;
    if (attributeValueFromList)
    {
      switch (rovm)
      {
        case RelationOperatorValueMode.rovmOne:
        case RelationOperatorValueMode.rovmTwo:
          showValueMode |= ShowValueMode.svmList;
          break;
        case RelationOperatorValueMode.rovmMulti:
          showValueMode |= ShowValueMode.svmListMulti;
          break;
      }
    }
    else if (rovm >= RelationOperatorValueMode.rovmOne)
    {
      if (rovm == RelationOperatorValueMode.rovmMulti)
        showValueMode |= ShowValueMode.svmMulti;
      switch (objRefTypes)
      {
        case SelectionParameterTypes.sptNone:
          break;
        case SelectionParameterTypes.sptString:
          showValueMode |= ShowValueMode.svmString;
          break;
        case SelectionParameterTypes.sptNumber:
        case SelectionParameterTypes.sptFloat:
          showValueMode |= ShowValueMode.svmNumber;
          break;
        case SelectionParameterTypes.sptBool:
          showValueMode |= ShowValueMode.svmBool;
          break;
        case SelectionParameterTypes.sptDate:
          if (value != RelationalOperators.LastNDays && value != RelationalOperators.NextNDays)
          {
            showValueMode |= ShowValueMode.svmDate;
            break;
          }
          showValueMode |= ShowValueMode.svmNumber;
          break;
        default:
          if ((uint) (objRefTypes - 17) > 1U)
          {
            if (objRefTypes == SelectionParameterTypes.sptFormula)
            {
              showValueMode |= ShowValueMode.svmFormula;
              break;
            }
            if (objRefTypes >= SelectionParameterTypes.sptObject)
            {
              showValueMode |= ShowValueMode.svmObj;
              break;
            }
            break;
          }
          goto case SelectionParameterTypes.sptString;
      }
    }
    return showValueMode;
  }
}
