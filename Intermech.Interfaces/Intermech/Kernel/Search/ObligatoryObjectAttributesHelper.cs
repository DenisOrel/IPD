
// Type: Intermech.Kernel.Search.ObligatoryObjectAttributesHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Reflection;


namespace Intermech.Kernel.Search
{
    /// <summary>
    /// Вспомогательный класс для работы с обязательными атрибутами
    /// </summary>
    public class ObligatoryObjectAttributesHelper
    {
      /// <summary>
      /// 
      /// </summary>
      private static Dictionary<ObligatoryObjectAttributes, ObligatoryObjectAttributesHelper.ObligatoryAttrInfo> _cache_ObligatoryAttr2AttrInfo = new Dictionary<ObligatoryObjectAttributes, ObligatoryObjectAttributesHelper.ObligatoryAttrInfo>();

      /// <summary>Получение данных по атрибуту</summary>
      /// <param name="attribute"></param>
      private static ObligatoryObjectAttributesHelper.ObligatoryAttrInfo GetObligatoryAttrInfo(
        ObligatoryObjectAttributes attribute)
      {
        ObligatoryObjectAttributesHelper.ObligatoryAttrInfo obligatoryAttrInfo;
        if (ObligatoryObjectAttributesHelper._cache_ObligatoryAttr2AttrInfo.TryGetValue(attribute, out obligatoryAttrInfo))
          return obligatoryAttrInfo;
        obligatoryAttrInfo = new ObligatoryObjectAttributesHelper.ObligatoryAttrInfo(attribute);
        ObligatoryObjectAttributesHelper._cache_ObligatoryAttr2AttrInfo[attribute] = obligatoryAttrInfo;
        return obligatoryAttrInfo;
      }

      /// <summary>Получить заголовок обязательного атрибута</summary>
      /// <param name="attr">Атрибут</param>
      /// <returns>Заголовок</returns>
      public static string GetCaption(ObligatoryObjectAttributes attr)
      {
        return ObligatoryObjectAttributesHelper.GetObligatoryAttrInfo(attr).Caption;
      }

      /// <summary>
      /// Получить заголовок обязательного атрибута по названию его поля
      /// </summary>
      /// <param name="fieldName">Название поля</param>
      /// <returns>Заголовок или null</returns>
      public static string GetCaption(string fieldName)
      {
        object attr = Enum.Parse(typeof (ObligatoryObjectAttributes), fieldName, true);
        return attr != null ? ObligatoryObjectAttributesHelper.GetCaption((ObligatoryObjectAttributes) attr) : (string) null;
      }

      /// <summary>Получить название поля для обязательного атрибута</summary>
      /// <param name="attr">Атрибут</param>
      /// <returns>Название поля</returns>
      public static string FieldName(ObligatoryObjectAttributes attr)
      {
        return ObligatoryObjectAttributesHelper.GetObligatoryAttrInfo(attr).FieldName;
      }

      /// <summary>Получить обязательный атрибут по его заголовку</summary>
      /// <param name="caption">Заголовок</param>
      /// <returns>Обязательный атрибут</returns>
      public static ObligatoryObjectAttributes GetObligatoryObjectAttribute(string caption)
      {
        return (ObligatoryObjectAttributes) EnumTypeHelper.GetEnumValue(typeof (ObligatoryObjectAttributes), caption);
      }

      /// <summary>
      /// Проверить, является ли указанный заголовок заголовком обязательного атрибута
      /// </summary>
      /// <param name="caption">Заголовок</param>
      /// <returns>true, если указанный заголовок является заголовком обязательного атрибута</returns>
      public static bool IsObligatoryAttribute(string caption)
      {
        return EnumTypeHelper.GetEnumValue(typeof (ObligatoryObjectAttributes), caption) != null;
      }

      /// <summary>Проверить, является ли указанный атрибут обязательным</summary>
      /// <returns>true, если указанный атрибут является обязательным</returns>
      public static bool IsObligatoryAttribute(int attributeID)
      {
        return attributeID < 0 && attributeID != -10000;
      }

      /// <summary>
      /// По идентификатору атрибута определяет является ли атрибут виртуальным вычисляемым
      /// </summary>
      /// <param name="attributeID">Ид. атрибута</param>
      /// <returns>true если атрибут виртуальный вычисляемый</returns>
      public static bool IsVirtualAttribute(int attributeID)
      {
        return attributeID == -85 || attributeID == -86 || attributeID == -87 || attributeID == -84;
      }

      /// <summary>
      /// Проверяет является ли атрибут системным полем типа Guid
      /// </summary>
      /// <param name="attribute">Некий идентификатор атрибута</param>
      /// <returns></returns>
      public static bool IsGuidField(object attribute)
      {
        switch (attribute)
        {
          case int _:
            int int32 = Convert.ToInt32(attribute);
            switch (int32)
            {
              case -18:
              case -12:
                return true;
              default:
                return int32 == -26;
            }
          case Guid guid:
            return guid.Equals(new Guid("cad00130-306c-11d8-b4e9-00304f19f545")) || guid.Equals(new Guid("cad00800-306c-11d8-b4e9-00304f19f545")) || guid.Equals(new Guid("cad00344-306c-11d8-b4e9-00304f19f545"));
          case string _:
            return attribute.ToString() == ObligatoryObjectAttributesHelper.GetCaption(ObligatoryObjectAttributes.F_GUID) || attribute.ToString() == ObligatoryObjectAttributesHelper.GetCaption(ObligatoryObjectAttributes.F_OBJ_GUID) || attribute.ToString() == ObligatoryObjectAttributesHelper.GetCaption(ObligatoryObjectAttributes.F_PRJ_GUID);
          default:
            return false;
        }
      }

      /// <summary>
      /// Определяет к чему относится атрибут attribute (объектам, связям)
      /// //Optimize
      /// Метод надо переписать, для ObligatoryObjectAttributes наряду с атрибутом CustomDescription нуна
      /// завести еще один атрибут, который бы прописан был для каждого со значением AttributeSourceTypes
      /// в объявлении enum ObligatoryObjectAttributes
      /// например: [CustomDescription("Attribute.Interfaces_380"), SourceType(AttributeSourceTypes.Object)]F_CHKOUT_BY = -6,
      /// </summary>
      /// <param name="attribute">Обязательный атрибут</param>
      /// <returns>Источник обязательного атрибута</returns>
      public static AttributeSourceTypes GetAttributeSourceType(ObligatoryObjectAttributes attribute)
      {
        return ObligatoryObjectAttributesHelper.GetObligatoryAttrInfo(attribute).SourceType;
      }

      /// <summary>
      /// Возвращает тип данных, который хранится в обязательном атрибуте attribute
      /// </summary>
      /// <param name="attribute">Обязательный атрибут</param>
      /// <returns>Тип данных, который хранится в обязательном атрибуте</returns>
      public static FieldTypes GetDataType(ObligatoryObjectAttributes attribute)
      {
        switch (attribute)
        {
          case ObligatoryObjectAttributes.F_LCSTEP_DATE:
          case ObligatoryObjectAttributes.F_DATE_VALUE:
          case ObligatoryObjectAttributes.F_SET_DATE:
          case ObligatoryObjectAttributes.F_END_DATE:
          case ObligatoryObjectAttributes.F_BEGIN_DATE:
          case ObligatoryObjectAttributes.F_DELETE_DATE:
          case ObligatoryObjectAttributes.F_CREATE_DATE:
          case ObligatoryObjectAttributes.F_OBJ_CREATE:
          case ObligatoryObjectAttributes.F_MODIFY_DATE:
            return FieldTypes.ftDateTime;
          case ObligatoryObjectAttributes.F_FILENAME:
          case ObligatoryObjectAttributes.F_STRING_VALUE:
          case ObligatoryObjectAttributes.CAPTION:
          case ObligatoryObjectAttributes.F_NOTE:
          case ObligatoryObjectAttributes.F_COMPUTER_NAME:
          case ObligatoryObjectAttributes.F_OBJECT_NAME:
          case ObligatoryObjectAttributes.F_SITE_ID:
          case ObligatoryObjectAttributes.F_AREA_ID:
            return FieldTypes.ftString;
          case ObligatoryObjectAttributes.F_DOUBLE_VALUE:
            return FieldTypes.ftDouble;
          case ObligatoryObjectAttributes.F_PRJ_GUID:
          case ObligatoryObjectAttributes.F_OBJ_GUID:
          case ObligatoryObjectAttributes.F_GUID:
            return FieldTypes.ftGuid;
          default:
            return FieldTypes.ftInteger;
        }
      }

      /// <summary>
      /// Возвращает тип данных, который передаётся ядром от атрибута attribute в момент вычисления значения другого атрибута с типом данных formulaFieldType, в формуле которого задействован данный атрибут.
      /// </summary>
      /// <param name="attribute">Ид. обязательного атрибута</param>
      /// <param name="formulaFieldType">Тип данных атрибута, формулу которого мы рассматриваем в данный момент</param>
      /// <returns>Если возвращает значение FieldTypes.ftUnknown, то этот атрибут не может быть задействован в формуле</returns>
      public static FieldTypes GetInFormulaDataType(
        ObligatoryObjectAttributes attribute,
        FieldTypes formulaFieldType)
      {
        if (formulaFieldType == FieldTypes.ftString && (attribute == ObligatoryObjectAttributes.F_LEVEL_ID || attribute == ObligatoryObjectAttributes.F_LC_STEP || attribute == ObligatoryObjectAttributes.F_OBJECT_TYPE || attribute == ObligatoryObjectAttributes.F_PROJECT_ID))
          return FieldTypes.ftString;
        return ObligatoryObjectAttributesHelper.CanUseInFormula(attribute) ? ObligatoryObjectAttributesHelper.GetDataType(attribute) : FieldTypes.ftUnknown;
      }

      /// <summary>
      /// Возвращает true, если данный атрибут может быть использован в формулах вычисляемых атрибутов IPS
      /// </summary>
      public static bool CanUseInFormula(ObligatoryObjectAttributes attribute)
      {
        return attribute == ObligatoryObjectAttributes.F_OBJECT_ID || attribute == ObligatoryObjectAttributes.F_LEVEL_ID || attribute == ObligatoryObjectAttributes.F_VERSION_ID || attribute == ObligatoryObjectAttributes.F_ID || attribute == ObligatoryObjectAttributes.F_LC_STEP || attribute == ObligatoryObjectAttributes.F_OBJECT_TYPE || attribute == ObligatoryObjectAttributes.F_PROJECT_ID;
      }

      /// <summary>Элемент кеша для ObligatoryObjectAttributes</summary>
      /// <remarks>Для "тяжелых" операций данные кешируем </remarks>
      internal struct ObligatoryAttrInfo
      {
        /// <summary>Источник обязательного атрибута</summary>
        public AttributeSourceTypes SourceType;
        /// <summary>Заголовок</summary>
        public string Caption;
        /// <summary>Название поля</summary>
        public string FieldName;

        /// <summary>Конструктор</summary>
        /// <param name="attribute"></param>
        public ObligatoryAttrInfo(ObligatoryObjectAttributes attribute)
        {
          this.SourceType = AttributeSourceTypes.Other;
          FieldInfo field = typeof (ObligatoryObjectAttributes).GetField(attribute.ToString());
          object[] objArray = !(field == (FieldInfo) null) ? field.GetCustomAttributes(typeof (SourceType), true) : throw new KernelException($"Obligatory attribute ID = {attribute} not found");
          if (objArray.Length == 1)
            this.SourceType = (objArray[0] as SourceType).AttributeSourceType;
          this.Caption = EnumTypeHelper.GetCaption((Enum) attribute);
          this.FieldName = attribute != ObligatoryObjectAttributes.None ? attribute.ToString() : string.Empty;
        }
      }
    }
}
