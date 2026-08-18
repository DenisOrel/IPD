
// Type: Intermech.AttributesTypeHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;


namespace Intermech
{
    public class AttributesTypeHelper
    {
      /// <summary>
      /// 
      /// </summary>
      private static readonly IDictionary<FieldTypes, FieldTypesAttributeValues> _fieldTypesAttributeValuesCache = (IDictionary<FieldTypes, FieldTypesAttributeValues>) new ConcurrentDictionary<FieldTypes, FieldTypesAttributeValues>();

      /// <summary>
      /// 
      /// </summary>
      /// <param name="fieldTypes"></param>
      private static FieldTypesAttributeValues GetFieldTypesAttributeValues(FieldTypes fieldTypes)
      {
        FieldTypesAttributeValues typesAttributeValues;
        if (!AttributesTypeHelper._fieldTypesAttributeValuesCache.TryGetValue(fieldTypes, out typesAttributeValues))
        {
          typesAttributeValues = new FieldTypesAttributeValues();
          AttributesTypeHelper._fieldTypesAttributeValuesCache[fieldTypes] = typesAttributeValues;
        }
        return typesAttributeValues;
      }

      public static string GetCaption(FieldTypes ft)
      {
        FieldTypesAttributeValues typesAttributeValues = AttributesTypeHelper.GetFieldTypesAttributeValues(ft);
        return typesAttributeValues.Description ?? (typesAttributeValues.Description = EnumTypeHelper.GetCaption((Enum) ft));
      }

      public static string GetCaption(FieldTypes[] ft)
      {
        return string.Join(", ", ((IEnumerable<FieldTypes>) ft).Select<FieldTypes, string>((Func<FieldTypes, string>) (item => AttributesTypeHelper.GetCaption(item))));
      }

      public static FieldTypes GetFieldType(string s)
      {
        return (FieldTypes) EnumTypeHelper.GetEnumValue(typeof (FieldTypes), s, (object) FieldTypes.ftUnknown);
      }

      /// <summary>Вернуть тип данных, хранимых в AttributeValues</summary>
      /// <param name="ft"></param>
      /// <returns></returns>
      public static Type GetTypeOfAttributeValue(FieldTypes ft)
      {
        FieldTypesAttributeValues typesAttributeValues = AttributesTypeHelper.GetFieldTypesAttributeValues(ft);
        if (typesAttributeValues.Type != (Type) null)
          return typesAttributeValues.Type;
        object[] customAttributes = ft.GetType().GetField(ft.ToString()).GetCustomAttributes(typeof (TypeOfAttributeValueAttribute), false);
        typesAttributeValues.Type = customAttributes.Length != 0 ? ((TypeOfAttributeValueAttribute) customAttributes[0]).TypeOfAttributeValue : typeof (object);
        return typesAttributeValues.Type;
      }

      /// <summary>
      /// Вернуть тип данных .NET, который соответствует типу данных
      /// </summary>
      /// <param name="fieldTypes">Тип атрибута</param>
      /// <returns>Тип данных .NET, который соответствует типу данных указанного атрибута в СУБД</returns>
      public static Type GetRDBMSTypeOfAttributeValue(FieldTypes fieldTypes)
      {
        FieldTypesAttributeValues typesAttributeValues = AttributesTypeHelper.GetFieldTypesAttributeValues(fieldTypes);
        if (typesAttributeValues.RDbMsType != (Type) null)
          return typesAttributeValues.RDbMsType;
        object[] customAttributes = fieldTypes.GetType().GetField(fieldTypes.ToString()).GetCustomAttributes(typeof (RDBMSTypeOfAttributeValueAttribute), false);
        typesAttributeValues.RDbMsType = customAttributes.Length != 0 ? ((RDBMSTypeOfAttributeValueAttribute) customAttributes[0]).TypeOfAttributeValue : typeof (object);
        return typesAttributeValues.RDbMsType;
      }

      /// <summary>
      /// Вернуть тип данных .NET, который соответствует типу данных указанного атрибута в СУБД
      /// </summary>
      /// <param name="attributeID">Идентификатор атрибута</param>
      /// <returns>Тип данных .NET, который соответствует типу данных указанного атрибута в СУБД</returns>
      public static Type GetRDBMSTypeOfAttributeValue(int attributeID)
      {
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attributeID);
        return attributeType == null ? typeof (object) : AttributesTypeHelper.GetRDBMSTypeOfAttributeValue(attributeType.RealFieldType);
      }

      /// <summary>
      /// Ф-ция возвращает true, если тип данных ft является комплексным, т.е. хранит значение в нескольких полях БД
      /// </summary>
      public static bool IsComplexAttributeType(FieldTypes ft)
      {
        return ft == FieldTypes.ftBlob || ft == FieldTypes.ftExternalLink || ft == FieldTypes.ftFile || ft == FieldTypes.ftMeasured || ft == FieldTypes.ftMemo || ft == FieldTypes.ftObjectLink || ft == FieldTypes.ftObjectLinkByID || ft == FieldTypes.ftShortBlob;
      }

      /// <summary>Функция сравнения допустимых сзачений</summary>
      /// <param name="val1">Значение 1</param>
      /// <param name="val2">Значение 2</param>
      /// <param name="valueFieldName">Имя поля, которое используется для хранения значения допустимых значений атрибута</param>
      /// <returns>Результат сравнения</returns>
      public static bool EqualValues(object val1, object val2, string valueFieldName)
      {
        object obj1 = CompareValuesHelper.NormalizedValue(val1);
        object obj2 = CompareValuesHelper.NormalizedValue(val2);
        if (obj1 == null && obj2 != null || obj2 == null && obj1 != null)
          return false;
        if (obj1 == null && obj2 == null)
          return true;
        switch (valueFieldName)
        {
          case "F_INTEGER_VALUE":
            return Convert.ToInt64(val1) == Convert.ToInt64(val2);
          case "F_DOUBLE_VALUE":
            return Convert.ToDouble(val1) == Convert.ToDouble(val2);
          case "F_DATE_VALUE":
            return Convert.ToDateTime(val1) == Convert.ToDateTime(val2);
          case "F_STRING_VALUE":
            return Convert.ToString(val1) == Convert.ToString(val2);
          default:
            return false;
        }
      }
    }
}
