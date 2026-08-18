
// Type: Intermech.Interfaces.MyAttributeHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>Класс, вытягивающий из сессии метаданные атрибутов</summary>
    public abstract class MyAttributeHelper
    {
      /// <summary>
      /// Получить ID атрибута по его GUID, заодно проверить его существование в базе
      /// </summary>
      /// <param name="AttrGUID">GUID атрибута</param>
      /// <returns>0, если атрибут не найден</returns>
      public static int GetAttrID(string AttrGUID)
      {
        if (AttrGUID.Length <= 0)
          return 0;
        IMSAttributeType imsAttributeType = (IMSAttributeType) null;
        try
        {
          if (!AttrGUID.StartsWith("["))
            imsAttributeType = MetaDataHelper.GetAttributeType(new Guid(AttrGUID));
        }
        catch
        {
          imsAttributeType = (IMSAttributeType) null;
        }
        if (imsAttributeType != null)
          return imsAttributeType.AttributeID;
        return 0;
      }

      /// <summary>
      /// Получить GUID атрибута по его ID, заодно проверить его существование в базе
      /// </summary>
      /// <param name="AttrID">ID атрибута</param>
      /// <returns>GUID атрибута или "" если атрибут не найден</returns>
      public static string GetAttrGUID(int AttrID)
      {
        if (AttrID == 0)
          return "";
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(AttrID);
        return attributeType != null ? attributeType.AttributeGuid.ToString() : string.Empty;
      }

      /// <summary>
      /// Получить метаданные атрибута по его GUID, заодно проверить его существование в базе
      /// </summary>
      /// <param name="AttrGUID">GUID атрибута</param>
      /// <param name="AttrName">ref Имя атрибута</param>
      /// <param name="AttrID">ref ID атрибута</param>
      /// <param name="AttrType">ref тип значения атрибута</param>
      /// <param name="IsSystemType">ref если true, то тип атрибута описан как ftSystem</param>
      /// <returns>true, если атрибут найден</returns>
      public static bool GetAttrInfo(
        string AttrGUID,
        ref string AttrName,
        ref int AttrID,
        ref FieldTypes AttrType,
        ref bool IsSystemType)
      {
        AttrID = 0;
        AttrName = cvConsts.cvAttribute;
        AttrType = FieldTypes.ftUnknown;
        IsSystemType = false;
        if (AttrGUID.Length <= 0)
          return false;
        bool attrInfo = false;
        IMSAttributeType imsAttributeType = (IMSAttributeType) null;
        try
        {
          if (!AttrGUID.StartsWith("["))
            imsAttributeType = MetaDataHelper.GetAttributeType(new Guid(AttrGUID));
        }
        catch
        {
          imsAttributeType = (IMSAttributeType) null;
        }
        if (imsAttributeType != null)
        {
          AttrID = imsAttributeType.AttributeID;
          AttrName = imsAttributeType.Name;
          AttrType = imsAttributeType.RealFieldType;
          attrInfo = true;
        }
        IsSystemType = AttrType == FieldTypes.ftSystem;
        return attrInfo;
      }

      /// <summary>
      /// Получить метаданные атрибута + допустимые значения по его GUID, заодно проверить его существование в базе
      /// </summary>
      /// <param name="AttrGUID">GUID атрибута</param>
      /// <param name="AttrName">ref Имя атрибута</param>
      /// <param name="AttrID">ref ID атрибута</param>
      /// <param name="AttrType">ref тип значения атрибута</param>
      /// <param name="IsSystemType">ref если true, то тип атрибута описан как ftSystem</param>
      /// <param name="IsAttrList">ref если true, у атрибута есть список допустимих значений</param>
      /// <param name="AttrPossibleValues">ref список допустимих значений атрибута (коллекция элементов MyElement)</param>
      /// <returns>true, если атрибут найден</returns>
      public static bool GetAttrInfo(
        string AttrGUID,
        ref string AttrName,
        ref int AttrID,
        ref FieldTypes AttrType,
        ref bool IsSystemType,
        ref bool IsAttrList,
        ref ArrayList AttrPossibleValues)
      {
        AttrID = 0;
        AttrName = cvConsts.cvAttribute;
        AttrType = FieldTypes.ftUnknown;
        IsSystemType = false;
        IsAttrList = false;
        if (AttrPossibleValues == null)
          AttrPossibleValues = new ArrayList();
        AttrPossibleValues.Clear();
        if (AttrGUID.Length <= 0)
          return false;
        bool attrInfo = false;
        IMSAttributeType imsAttributeType = (IMSAttributeType) null;
        try
        {
          if (!AttrGUID.StartsWith("["))
            imsAttributeType = MetaDataHelper.GetAttributeType(new Guid(AttrGUID));
        }
        catch
        {
          imsAttributeType = (IMSAttributeType) null;
        }
        if (imsAttributeType != null)
        {
          AttrID = imsAttributeType.AttributeID;
          AttrName = imsAttributeType.Name;
          AttrType = imsAttributeType.RealFieldType;
          IsAttrList = imsAttributeType.MultiValueMode == MultiValueModes.SingleValueFromList || imsAttributeType.MultiValueMode == MultiValueModes.MultiValuesFromList;
          if (IsAttrList)
          {
            List<object> possibleValues = imsAttributeType.PossibleValues;
            List<object> valuesDescriptions = imsAttributeType.PossibleValuesDescriptions;
            if (possibleValues != null && valuesDescriptions != null && possibleValues.Count == valuesDescriptions.Count)
            {
              for (int index = 0; index < possibleValues.Count; ++index)
              {
                string caption = valuesDescriptions[index].ToString();
                if (string.IsNullOrEmpty(caption))
                  caption = possibleValues[index].ToString();
                AttrPossibleValues.Add((object) new MyElement(possibleValues[index], caption, (object) null));
              }
            }
          }
          if (MyAttributeHelper.IsValidSystemIDType(AttrID))
          {
            IsAttrList = true;
            if (AttrID == -9)
            {
              List<IMSLifeCycleLevel> lcLevelsList = MetaDataHelper.GetLCLevelsList();
              lcLevelsList.Sort();
              for (int index = 0; index < lcLevelsList.Count; ++index)
              {
                MyElement myElement = new MyElement((object) lcLevelsList[index].LevelID, lcLevelsList[index].Name, (object) null);
                AttrPossibleValues.Add((object) myElement);
              }
            }
          }
          attrInfo = true;
        }
        IsSystemType = AttrType == FieldTypes.ftSystem;
        return attrInfo;
      }

      /// <summary>
      /// Получить метаданные атрибута по его ID, заодно проверить его существование в базе
      /// </summary>
      /// <param name="AttrID">ID атрибута</param>
      /// <param name="AttrName">ref Имя атрибута</param>
      /// <param name="AttrGUID">ref GUID атрибута</param>
      /// <param name="AttrType">ref тип значения атрибута</param>
      /// <param name="IsSystemType">ref если true, то тип атрибута описан как ftSystem</param>
      /// <returns>true, если атрибут найден</returns>
      public static bool GetAttrInfo(
        int AttrID,
        ref string AttrName,
        ref string AttrGUID,
        ref FieldTypes AttrType,
        ref bool IsSystemType)
      {
        AttrGUID = "";
        AttrName = cvConsts.cvAttribute;
        AttrType = FieldTypes.ftUnknown;
        IsSystemType = false;
        if (AttrID == 0)
          return false;
        bool attrInfo = false;
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(AttrID);
        if (attributeType != null)
        {
          AttrName = attributeType.Name;
          AttrType = attributeType.RealFieldType;
          AttrGUID = attributeType.AttributeGuid.ToString();
          attrInfo = true;
        }
        IsSystemType = AttrType == FieldTypes.ftSystem;
        return attrInfo;
      }

      /// <summary>
      /// Получить метаданные атрибута + допустимые значения по его ID, заодно проверить его существование в базе
      /// </summary>
      /// <param name="AttrID">ID атрибута</param>
      /// <param name="AttrName">ref Имя атрибута</param>
      /// <param name="AttrGUID">ref GUID атрибута</param>
      /// <param name="AttrType">ref тип значения атрибута</param>
      /// <param name="IsSystemType">ref если true, то тип атрибута описан как ftSystem</param>
      /// <param name="IsAttrList">ref если true, у атрибута есть список допустимих значений</param>
      /// <param name="AttrPossibleValues">ref список допустимих значений атрибута (коллекция элементов MyElement)</param>
      /// <returns>true, если атрибут найден</returns>
      public static bool GetAttrInfo(
        int AttrID,
        ref string AttrName,
        ref string AttrGUID,
        ref FieldTypes AttrType,
        ref bool IsSystemType,
        ref bool IsAttrList,
        ref ArrayList AttrPossibleValues)
      {
        AttrGUID = "";
        AttrName = cvConsts.cvAttribute;
        AttrType = FieldTypes.ftUnknown;
        IsSystemType = false;
        IsAttrList = false;
        if (AttrPossibleValues == null)
          AttrPossibleValues = new ArrayList();
        AttrPossibleValues.Clear();
        if (AttrID == 0)
          return false;
        bool attrInfo = false;
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(AttrID);
        if (attributeType != null)
        {
          AttrName = attributeType.Name;
          AttrType = attributeType.RealFieldType;
          AttrGUID = attributeType.AttributeGuid.ToString();
          IsAttrList = attributeType.MultiValueMode == MultiValueModes.SingleValueFromList || attributeType.MultiValueMode == MultiValueModes.MultiValuesFromList;
          if (IsAttrList)
          {
            List<object> possibleValues = attributeType.PossibleValues;
            List<object> valuesDescriptions = attributeType.PossibleValuesDescriptions;
            if (possibleValues != null && valuesDescriptions != null && possibleValues.Count == valuesDescriptions.Count)
            {
              for (int index = 0; index < possibleValues.Count; ++index)
              {
                string caption = valuesDescriptions[index].ToString();
                if (string.IsNullOrEmpty(caption))
                  caption = possibleValues[index].ToString();
                AttrPossibleValues.Add((object) new MyElement(possibleValues[index], caption, (object) null));
              }
            }
          }
          if (MyAttributeHelper.IsValidSystemIDType(AttrID))
          {
            IsAttrList = true;
            if (AttrID == -9)
            {
              List<IMSLifeCycleLevel> lcLevelsList = MetaDataHelper.GetLCLevelsList();
              lcLevelsList.Sort();
              for (int index = 0; index < lcLevelsList.Count; ++index)
              {
                MyElement myElement = new MyElement((object) lcLevelsList[index].LevelID, lcLevelsList[index].Name, (object) null);
                AttrPossibleValues.Add((object) myElement);
              }
            }
          }
          attrInfo = true;
        }
        IsSystemType = AttrType == FieldTypes.ftSystem;
        return attrInfo;
      }

      /// <summary>
      /// Выполнить проверку совместимости двух типов аргументов на предмет сравнения.
      /// Можно ли сравнивать значение атрибута первого типа со значением атрибута второго типа
      /// </summary>
      /// <param name="Type_1">Тип первого атрибута</param>
      /// <param name="Type_2">Тип второго атрибута</param>
      /// <returns>true, если типы пригодны для сравнения</returns>
      public static bool IsComparable(FieldTypes Type_1, FieldTypes Type_2)
      {
        if (!MyAttributeHelper.IsValidType(Type_1) || !MyAttributeHelper.IsValidType(Type_2))
          return false;
        switch (Type_1)
        {
          case FieldTypes.ftString:
            return Type_2 == FieldTypes.ftString || Type_2 == FieldTypes.ftMemo || Type_2 == FieldTypes.ftGuid;
          case FieldTypes.ftInteger:
            return Type_2 == FieldTypes.ftInteger || Type_2 == FieldTypes.ftAutoInc;
          case FieldTypes.ftDouble:
            return Type_2 == FieldTypes.ftDouble || Type_2 == FieldTypes.ftInteger || Type_2 == FieldTypes.ftAutoInc;
          case FieldTypes.ftDateTime:
            return Type_2 == FieldTypes.ftDateTime;
          case FieldTypes.ftShortBlob:
            return Type_2 == FieldTypes.ftShortBlob;
          case FieldTypes.ftFile:
            return Type_2 == FieldTypes.ftFile;
          case FieldTypes.ftMemo:
            return Type_2 == FieldTypes.ftMemo || Type_2 == FieldTypes.ftString || Type_2 == FieldTypes.ftGuid;
          case FieldTypes.ftBlob:
            return Type_2 == FieldTypes.ftBlob;
          case FieldTypes.ftBoolean:
            return Type_2 == FieldTypes.ftBoolean;
          case FieldTypes.ftMeasured:
            return Type_2 == FieldTypes.ftMeasured;
          case FieldTypes.ftAutoInc:
            return Type_2 == FieldTypes.ftInteger || Type_2 == FieldTypes.ftAutoInc;
          case FieldTypes.ftGuid:
            return Type_2 == FieldTypes.ftGuid || Type_2 == FieldTypes.ftMemo || Type_2 == FieldTypes.ftString;
          default:
            return false;
        }
      }

      /// <summary>
      /// Вернуть список типов, совместимых с типом атрибута AttrType
      /// </summary>
      /// <param name="AttrType">Проверяемый тип атрибута</param>
      /// <returns>null в случае ошибки, иначе список типов из FieldTypes, совместимых с AttrType</returns>
      public static object[] GetComparableTypes(FieldTypes AttrType)
      {
        if (!MyAttributeHelper.IsValidType(AttrType))
          return (object[]) null;
        switch (AttrType)
        {
          case FieldTypes.ftString:
            return new object[3]
            {
              (object) FieldTypes.ftString,
              (object) FieldTypes.ftMemo,
              (object) FieldTypes.ftGuid
            };
          case FieldTypes.ftInteger:
            return new object[2]
            {
              (object) FieldTypes.ftInteger,
              (object) FieldTypes.ftAutoInc
            };
          case FieldTypes.ftDouble:
            return new object[3]
            {
              (object) FieldTypes.ftDouble,
              (object) FieldTypes.ftInteger,
              (object) FieldTypes.ftAutoInc
            };
          case FieldTypes.ftDateTime:
            return new object[1]
            {
              (object) FieldTypes.ftDateTime
            };
          case FieldTypes.ftShortBlob:
            return new object[1]
            {
              (object) FieldTypes.ftShortBlob
            };
          case FieldTypes.ftFile:
            return new object[1]{ (object) FieldTypes.ftFile };
          case FieldTypes.ftMemo:
            return new object[3]
            {
              (object) FieldTypes.ftMemo,
              (object) FieldTypes.ftString,
              (object) FieldTypes.ftGuid
            };
          case FieldTypes.ftBlob:
            return new object[1]{ (object) FieldTypes.ftBlob };
          case FieldTypes.ftBoolean:
            return new object[1]
            {
              (object) FieldTypes.ftBoolean
            };
          case FieldTypes.ftMeasured:
            return new object[1]
            {
              (object) FieldTypes.ftMeasured
            };
          case FieldTypes.ftAutoInc:
            return new object[2]
            {
              (object) FieldTypes.ftInteger,
              (object) FieldTypes.ftAutoInc
            };
          case FieldTypes.ftGuid:
            return new object[3]
            {
              (object) FieldTypes.ftGuid,
              (object) FieldTypes.ftMemo,
              (object) FieldTypes.ftString
            };
          default:
            return (object[]) null;
        }
      }

      /// <summary>
      /// Проверяет, допустимо ли использовать указанный тип атрибута в значениях критериев подбора
      /// </summary>
      /// <param name="AttrType">Проверяемый тип атрибута</param>
      /// <returns>true - атрибуты такого типа можно применять в значениях критериев подбора</returns>
      public static bool IsValidType(FieldTypes AttrType) => AttrType != 0;

      /// <summary>
      /// Выполнить проверку - является ли атрибут нормальным простым типом
      /// </summary>
      /// <param name="AttrType">Проверяемый тип атрибута</param>
      /// <returns>true - атрибуты такого типа можно применять в значениях критериев подбора</returns>
      public static bool IsSimpleType(FieldTypes AttrType)
      {
        return AttrType == FieldTypes.ftSystem || AttrType == FieldTypes.ftString || AttrType == FieldTypes.ftInteger || AttrType == FieldTypes.ftDouble || AttrType == FieldTypes.ftDateTime || AttrType == FieldTypes.ftBoolean || AttrType == FieldTypes.ftMeasured || AttrType == FieldTypes.ftAutoInc;
      }

      /// <summary>
      /// Проверить, имеет ли отношение тип атрибута к системным атрибутам с ID пользователей
      /// </summary>
      /// <param name="AttrID">ID проверяемого атрибута</param>
      /// <returns>true, если указанный тип атрибута - один из системных атрибутов с ID пользователей</returns>
      public static bool IsUserIDType(int AttrID) => AttrID == -36 || AttrID == -8 || AttrID == -6;

      /// <summary>
      /// Проверить, имеет ли отношение тип атрибута к системным атрибутам с ID пользователей
      /// </summary>
      /// <param name="AttrGuid">Guid проверяемого атрибута</param>
      /// <returns>true, если указанный тип атрибута - один из системных атрибутов с ID пользователей</returns>
      public static bool IsUserGuidType(string AttrGuid)
      {
        return AttrGuid == "cad0002d-306c-11d8-b4e9-00304f19f545" || AttrGuid == "cad0002f-306c-11d8-b4e9-00304f19f545";
      }

      /// <summary>
      /// Проверить, является ли атрибут допустимым системным атрибутом для получения его значений
      /// </summary>
      /// <param name="AttrID">ID проверяемого атрибута</param>
      /// <returns>true, если указанный тип атрибута - один из допустимых системных атрибутов</returns>
      public static bool IsValidSystemIDType(int AttrID)
      {
        switch (AttrID)
        {
          case -36:
            return true;
          case -9:
            return true;
          case -8:
            return true;
          case -6:
            return true;
          default:
            return false;
        }
      }
    }
}
