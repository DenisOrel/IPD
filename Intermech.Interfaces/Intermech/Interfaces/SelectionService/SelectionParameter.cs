
// Type: Intermech.Interfaces.SelectionService.SelectionParameter
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Interfaces.SelectionService
{
    public sealed class SelectionParameter
    {
      /// <summary>
      /// Преобразование списка значений к строке вида "Val_1", "Val_2", ...
      /// </summary>
      /// <param name="userSession">Интерфейс пользоваткельской сессии</param>
      /// <param name="valueList">Список значений</param>
      /// <param name="selPar">Тип данных параметра условия выборки</param>
      /// <param name="attributeValues">Допустимые значения атрибутов (если атрибут списковый)</param>
      /// <returns>Результирующая строка</returns>
      private static string DecodeValueList(
        IUserSession userSession,
        IList valueList,
        SelectionParameterTypes selPar,
        Dictionary<object, string> possibleValues)
      {
        return SelectionParameter.DecodeValueList(userSession, valueList, selPar, possibleValues, "\"", ", ");
      }

      /// <summary>Преобразование списка значений к строке</summary>
      /// <param name="userSession">Интерфейс пользоваткельской сессии</param>
      /// <param name="ValueList">Список значений</param>
      /// <param name="selPar">Тип данных параметра условия выборки</param>
      /// <param name="possibleValues">Допустимые значения атрибутов (если атрибут списковый)</param>
      /// <param name="delimiter">Строка, ограничивающая значение (например, двойная кавычка: "Val" )</param>
      /// <param name="separator">Строка, разделяющая значения (например, запятая и пробел: "Val_1", "Val_2" )</param>
      /// <returns>Результирующая строка</returns>
      private static string DecodeValueList(
        IUserSession userSession,
        IList valueList,
        SelectionParameterTypes selPar,
        Dictionary<object, string> possibleValues,
        string delimiter,
        string separator)
      {
        string str1 = "";
        int num = 50;
        string str2 = delimiter;
        string str3 = str2 + separator + str2;
        for (int index = 0; index < valueList.Count; ++index)
        {
          string str4 = str1 + (index == 0 ? str2 : str3);
          string str5 = SelectionParameter.ConvertToString(userSession, valueList[index], selPar, possibleValues);
          if (!str5.Equals(string.Empty) && str5.Length > num - str4.Length)
            str5 = str5.Substring(0, num - str4.Length) + "...";
          str1 = str4 + str5;
          if (index == valueList.Count - 1)
            str1 += str2;
          else if (str1.Length >= num - str3.Length)
          {
            str1 = $"{str1}{str2}, ...";
            break;
          }
        }
        return str1;
      }

      public static SelectionParameterTypes GetNodeValueType(int attributeID, FieldTypes ft)
      {
        SelectionParameterTypes nodeValueType = SelectionParameterTypes.sptNone;
        if (ft == FieldTypes.ftSystem)
        {
          switch (attributeID)
          {
            case -81:
            case -8:
              nodeValueType = SelectionParameterTypes.sptUser;
              break;
            case -23:
              nodeValueType = SelectionParameterTypes.sptLinkType;
              break;
            case -14:
              nodeValueType = SelectionParameterTypes.sptObject;
              break;
            case -12:
              nodeValueType = SelectionParameterTypes.sptGlobalID;
              break;
            case -11:
              nodeValueType = SelectionParameterTypes.sptSubjectArea;
              break;
            case -9:
              nodeValueType = SelectionParameterTypes.sptLifecycleLevel;
              break;
            case -7:
              nodeValueType = SelectionParameterTypes.sptObjectType;
              break;
            case -6:
              nodeValueType = SelectionParameterTypes.sptCheckOutBy;
              break;
            case -4:
              nodeValueType = SelectionParameterTypes.sptLifecycleStep;
              break;
            default:
              ft = ObligatoryObjectAttributesHelper.GetDataType((ObligatoryObjectAttributes) attributeID);
              break;
          }
        }
        switch (ft)
        {
          case FieldTypes.ftString:
          case FieldTypes.ftShortBlob:
          case FieldTypes.ftExternalLink:
          case FieldTypes.ftMemo:
          case FieldTypes.ftBlob:
            nodeValueType = SelectionParameterTypes.sptString;
            break;
          case FieldTypes.ftInteger:
          case FieldTypes.ftAutoInc:
            nodeValueType = SelectionParameterTypes.sptNumber;
            break;
          case FieldTypes.ftDouble:
            nodeValueType = SelectionParameterTypes.sptFloat;
            break;
          case FieldTypes.ftDateTime:
            nodeValueType = SelectionParameterTypes.sptDate;
            break;
          case FieldTypes.ftFile:
            nodeValueType = SelectionParameterTypes.sptFile;
            break;
          case FieldTypes.ftObjectLink:
          case FieldTypes.ftObjectLinkByID:
            IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attributeID);
            nodeValueType = attributeType.SizeType == 0L || !MetaDataHelper.GetObjectTypeGuid(Convert.ToInt32(attributeType.SizeType)).Equals(new Guid("cad00002-306c-11d8-b4e9-00304f19f545")) ? SelectionParameterTypes.sptObject : SelectionParameterTypes.sptUser;
            break;
          case FieldTypes.ftBoolean:
            nodeValueType = SelectionParameterTypes.sptBool;
            break;
          case FieldTypes.ftMeasured:
            nodeValueType = SelectionParameterTypes.sptMeasured;
            break;
          case FieldTypes.ftGuid:
            nodeValueType = SelectionParameterTypes.sptGlobalID;
            break;
        }
        return nodeValueType;
      }

      /// <summary>
      /// Получение типа данных параметра условия выборки на основе значения полей IDBAttributeType
      /// </summary>
      /// <param name="idbAttrType">Интерфейс типа атрибута</param>
      /// <returns>Тип данных параметра условия выборки</returns>
      public static SelectionParameterTypes GetNodeValueType(IDBAttributeType idbAttrType)
      {
        return idbAttrType == null ? SelectionParameterTypes.sptNone : SelectionParameter.GetNodeValueType(idbAttrType.AttributeID, idbAttrType.AttributeType);
      }

      /// <summary>В каких выборках используется реляционный оператор</summary>
      /// <param name="RelationalOperator">реляционный оператор который надо проверить</param>
      /// <returns></returns>
      public static UsedInSelection GetUsedInSelection(RelationalOperators RelationalOperator)
      {
        object[] customAttributes = typeof (RelationalOperators).GetField(RelationalOperator.ToString()).GetCustomAttributes(typeof (SelectionInfo), false);
        return customAttributes != null && customAttributes.Length != 0 ? ((SelectionInfo) customAttributes[0]).Type : UsedInSelection.None;
      }

      public static bool IsNoneValueOpr(RelationalOperators RelationalOperator)
      {
        object[] customAttributes = typeof (RelationalOperators).GetField(RelationalOperator.ToString()).GetCustomAttributes(typeof (SelectionInfo), false);
        return customAttributes != null && customAttributes.Length != 0 && (((SelectionInfo) customAttributes[0]).Options & RelationOperatorOptions.NoneValue) == RelationOperatorOptions.NoneValue;
      }

      /// <summary>
      /// Функция для проверки - применяется ли реляционный оператор, переданный в качестве
      /// аргумента, для задания условия по входимости
      /// </summary>
      /// <param name="RelationalOperator">реляционный оператор который надо проверить</param>
      /// <returns>результат проверки</returns>
      public static bool IsInRelationOpr(RelationalOperators RelationalOperator)
      {
        object[] customAttributes = typeof (RelationalOperators).GetField(RelationalOperator.ToString()).GetCustomAttributes(typeof (SelectionInfo), false);
        return customAttributes != null && customAttributes.Length != 0 && (((SelectionInfo) customAttributes[0]).Options & RelationOperatorOptions.InRelation) == RelationOperatorOptions.InRelation;
      }

      public static bool IsLinkRelationOpr(RelationalOperators relationalOperator)
      {
        return relationalOperator == RelationalOperators.Linked || relationalOperator == RelationalOperators.NotLinked;
      }

      public static RelationalOperators[] StringOperators
      {
        get
        {
          return new RelationalOperators[7]
          {
            RelationalOperators.Substring,
            RelationalOperators.StartString,
            RelationalOperators.EndString,
            RelationalOperators.NotSubstring,
            RelationalOperators.NotStartString,
            RelationalOperators.NotEndString,
            RelationalOperators.StringTemplate
          };
        }
      }

      public static RelationalOperators[] GetInRelationOperators()
      {
        List<RelationalOperators> relationalOperatorsList = new List<RelationalOperators>();
        foreach (RelationalOperators RelationalOperator in Enum.GetValues(typeof (RelationalOperators)))
        {
          if (SelectionParameter.IsInRelationOpr(RelationalOperator))
            relationalOperatorsList.Add(RelationalOperator);
        }
        return relationalOperatorsList.ToArray();
      }

      public static RelationalOperators[] InLCHistoryRelationalOperators
      {
        get
        {
          return new RelationalOperators[8]
          {
            RelationalOperators.Equal,
            RelationalOperators.Between,
            RelationalOperators.NotEqual,
            RelationalOperators.LastNDays,
            RelationalOperators.Greater,
            RelationalOperators.GreaterOrEqual,
            RelationalOperators.Less,
            RelationalOperators.LessOrEqual
          };
        }
      }

      /// <summary>
      /// Получение текстового представления значения параметра условия выборки (по типу атрибута и значению)
      /// </summary>
      /// <param name="userSession">Интерфейс пользоваткельской сессии</param>
      /// <param name="objValue">Значение параметра условия выборки</param>
      /// <param name="attrType">Интерфейс типа атрибута</param>
      /// <returns>Текстовое представление значения параметра условия выборки</returns>
      public static string ConvertToString(
        IUserSession userSession,
        object objValue,
        IDBAttributeType attrType)
      {
        Dictionary<object, string> possibleValues1 = new Dictionary<object, string>();
        if (attrType != null && (attrType.MultipleValued == MultiValueModes.SingleValueFromList || attrType.MultipleValued == MultiValueModes.MultiValuesFromList))
        {
          DataTable possibleValues2 = attrType.GetPossibleValues();
          if (possibleValues2 != null)
          {
            foreach (DataRow row in (InternalDataCollectionBase) possibleValues2.Rows)
            {
              object key = row[attrType.ValueFieldName];
              string str = Convert.ToString(row["F_DESCRIPTION"]);
              if (str == "")
                str = Convert.ToString(key);
              possibleValues1.Add(key, str);
            }
          }
        }
        return SelectionParameter.ConvertToString(userSession, objValue, SelectionParameter.GetNodeValueType(attrType), possibleValues1);
      }

      /// <summary>
      /// Получение текстового представления значения параметра условия выборки (по типу данных и значению)
      /// </summary>
      /// <param name="userSession">Интерфейс пользоваткельской сессии</param>
      /// <param name="objValue">Значение параметра условия выборки</param>
      /// <param name="selParType">Тип данных параметра условия выборки</param>
      /// <returns>Текстовое представление значения параметра условия выборки</returns>
      public static string ConvertToString(
        IUserSession userSession,
        object objValue,
        SelectionParameterTypes selParType)
      {
        return SelectionParameter.ConvertToString(userSession, objValue, selParType, (Dictionary<object, string>) null);
      }

      public static string ConvertToStringInputObjectAttribute(
        IUserSession userSession,
        InputObjectAttribute objValue)
      {
        string inputObjectAttribute = string.Empty;
        if (objValue != null)
        {
          if (!objValue.ObjectGUID.Equals(Guid.Empty))
            inputObjectAttribute = userSession.GetObjectType(objValue.ObjectGUID).ObjectTypeName;
          inputObjectAttribute = inputObjectAttribute == string.Empty ? inputObjectAttribute : inputObjectAttribute + ".";
          if (!objValue.AttributeGUID.Equals(Guid.Empty))
            inputObjectAttribute += userSession.GetAttributeType(objValue.AttributeGUID).Name;
        }
        return inputObjectAttribute;
      }

      /// <summary>
      /// Получение текстового представления значения параметра условия выборки (по типу данных и значению)
      /// </summary>
      /// <param name="userSession">Интерфейс пользоваткельской сессии</param>
      /// <param name="objValue">Значение параметра условия выборки</param>
      /// <param name="selParType">Тип данных параметра условия выборки</param>
      /// <param name="possibleValues">Допустимые значения атрибутов (если атрибут списковый)</param>
      /// <returns>Текстовое представление значения параметра условия выборки</returns>
      private static string ConvertToString(
        IUserSession userSession,
        object objValue,
        SelectionParameterTypes selParType,
        Dictionary<object, string> possibleValues)
      {
        string str = "";
        if (objValue != null && userSession != null)
        {
          if (objValue.GetType() == typeof (InputObjectAttribute))
            str = SelectionParameter.ConvertToStringInputObjectAttribute(userSession, (InputObjectAttribute) objValue);
          else if (objValue is ConditionFormula)
            str = ((ConditionFormula) objValue).Formula;
          else if (objValue is IList)
            str = $"[{SelectionParameter.DecodeValueList(userSession, (IList) objValue, selParType, possibleValues)}]";
          else if (possibleValues != null && possibleValues.Count > 0 && possibleValues.ContainsKey(objValue))
          {
            str = possibleValues[objValue];
          }
          else
          {
            try
            {
              switch (selParType)
              {
                case SelectionParameterTypes.sptString:
                  str = Convert.ToString(objValue);
                  break;
                case SelectionParameterTypes.sptNumber:
                  str = Convert.ToString(Convert.ToInt64(objValue));
                  break;
                case SelectionParameterTypes.sptFloat:
                  str = Convert.ToString(Convert.ToDecimal(objValue));
                  break;
                case SelectionParameterTypes.sptBool:
                  str = Convert.ToBoolean(objValue) ? Consts.TrueValue : Consts.FalseValue;
                  break;
                case SelectionParameterTypes.sptDate:
                  str = !objValue.GetType().Equals(typeof (DateTime)) ? (!Convert.ToString(objValue).Equals(Consts.CurrentDateFunction) ? Convert.ToString(Convert.ToInt64(objValue)) : Consts.CurrentDateFunction) : Convert.ToDateTime(objValue).ToString(DateTimeHelper.GenerateDisplayFormat(Convert.ToString(objValue)));
                  break;
                case SelectionParameterTypes.sptObject:
                  if (Convert.ToInt64(objValue) == -1L)
                  {
                    str = LocalizationHolder.rm.GetString("Interfaces_114");
                    break;
                  }
                  IDBObject dbObject1 = userSession.GetObject(Convert.ToInt64(objValue), false);
                  if (dbObject1 != null)
                  {
                    str = dbObject1.Caption;
                    break;
                  }
                  break;
                case SelectionParameterTypes.sptCheckOutBy:
                case SelectionParameterTypes.sptUser:
                  if (Convert.ToString(objValue) == Consts.CurrentUserFunction)
                  {
                    str = Consts.CurrentUserFunction;
                    break;
                  }
                  if (objValue is ConditionGroupIDReplacer)
                  {
                    IDBObject dbObject2 = userSession.GetObject(((ConditionGroupIDReplacer) objValue).GroupID, false);
                    if (dbObject2 != null)
                    {
                      str = dbObject2.Caption;
                      break;
                    }
                    break;
                  }
                  if (Convert.ToInt64(objValue) == 0L)
                  {
                    if (selParType == SelectionParameterTypes.sptCheckOutBy)
                    {
                      str = LocalizationHolder.rm.GetString("Interfaces_113");
                      break;
                    }
                    break;
                  }
                  IDBObject dbObject3 = userSession.GetObject(Convert.ToInt64(objValue), false);
                  if (dbObject3 != null)
                  {
                    str = dbObject3.Caption;
                    break;
                  }
                  break;
                case SelectionParameterTypes.sptObjectType:
                  if (Convert.ToInt32(objValue) == -1)
                  {
                    str = LocalizationHolder.rm.GetString("Interfaces_115");
                    break;
                  }
                  IDBObjectType objectType = userSession.GetObjectType(Convert.ToInt32(objValue), false);
                  if (objectType != null)
                  {
                    str = objectType.ObjectTypeName;
                    break;
                  }
                  break;
                case SelectionParameterTypes.sptLifecycleLevel:
                  IDBLifecycleLevelType lifecycleLevel = userSession.GetLifecycleLevel(Convert.ToInt32(objValue), false);
                  if (lifecycleLevel != null)
                  {
                    str = lifecycleLevel.LevelName;
                    break;
                  }
                  break;
                case SelectionParameterTypes.sptSubjectArea:
                  IDBSubjectAreaType subjectAreaType = userSession.GetSubjectAreaType(Convert.ToChar(objValue), false);
                  if (subjectAreaType != null)
                  {
                    str = subjectAreaType.AreaName;
                    break;
                  }
                  break;
                case SelectionParameterTypes.sptLinkType:
                  IDBRelationType relationType = userSession.GetRelationType(Convert.ToInt32(objValue), false);
                  if (relationType != null)
                  {
                    str = relationType.Description;
                    break;
                  }
                  break;
                case SelectionParameterTypes.sptLifecycleStep:
                  IDBLifecycleStep lifecycleStep = userSession.GetLifecycleStep(Convert.ToInt32(objValue), false);
                  if (lifecycleStep != null)
                  {
                    str = lifecycleStep.LCName;
                    break;
                  }
                  break;
                case SelectionParameterTypes.sptGlobalID:
                  str = ((Guid) objValue).ToString();
                  break;
                case SelectionParameterTypes.sptMeasured:
                  str = ((MeasuredValue) objValue).Caption;
                  break;
                default:
                  str = Convert.ToString(objValue);
                  break;
              }
            }
            catch (FormatException ex)
            {
            }
          }
        }
        return str;
      }

      public static string GetObjectLinkTextValue(IUserSession userSession, object Value)
      {
        return SelectionParameter.ConvertToString(userSession, Value, SelectionParameterTypes.sptObject, (Dictionary<object, string>) null);
      }

      public static string GetObjectTypeLinkTextValue(IUserSession userSession, object Value)
      {
        return SelectionParameter.ConvertToString(userSession, Value, SelectionParameterTypes.sptObjectType, (Dictionary<object, string>) null);
      }

      public static string GetRelationTypeTextValue(IUserSession userSession, object Value)
      {
        return SelectionParameter.ConvertToString(userSession, Value, SelectionParameterTypes.sptLinkType, (Dictionary<object, string>) null);
      }
    }
}
