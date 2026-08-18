
// Type: Intermech.Interfaces.MetadataUpdates.UpdateScriptHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Collections.Generic;


namespace Intermech.Interfaces.MetadataUpdates
{
    /// <summary>
    /// Константы и методы для системы автообновления метаданных и объектов
    /// </summary>
    public static class UpdateScriptHelper
    {
      /// <summary>Название корневого нода</summary>
      public const string XMLRootNode = "Objects";
      /// <summary>Название нода - версии плагина</summary>
      public const string XMLVersionAttribute = "PluginVersion";
      /// <summary>Название нода - объекта/метаданного</summary>
      public const string XMLObjectNode = "Object";
      /// <summary>Название нода - свойство объекта/метаданного</summary>
      public const string XMLPropertyNode = "Property";
      /// <summary>Название нода - расширение</summary>
      public const string XMLExtensionNode = "Extension";
      /// <summary>Атрибут нода - GUID объекта/метаданного</summary>
      public const string XMLGuidAttribute = "Guid";
      /// <summary>Атрибут нода - Доп.данные</summary>
      public const string XMLTagAttribute = "Tag";
      /// <summary>Атрибут нода - ID категории</summary>
      public const string XMLCategoryIDAttribute = "CategoryID";
      /// <summary>Атрибут нода - Флаг обязательности</summary>
      public const string XMLObligatoryAttribute = "Obligatory";
      /// <summary>Атрибут нода - идентификатор свойства/атрибута</summary>
      public const string XMLIdAttribute = "Id";
      /// <summary>Атрибут нода</summary>
      public const string XMLParamNameAttribute = "ParamName";
      /// <summary>Атрибут нода</summary>
      public const string XMLInListIDAttribute = "InListID";
      /// <summary>Атрибут нода</summary>
      public const string XMLCategoryTypeAttribute = "CategoryType";
      /// <summary>Название нода - значение атрибута</summary>
      public const string XMLPropValueAttribute = "PropValue";
      /// <summary>Атрибут нода - значение</summary>
      public const string XMLValueAttribute = "Value";
      /// <summary>Атрибут нода - строковое значение</summary>
      public const string XMLStringValueAttribute = "StringValue";
      /// <summary>Атрибут нода - вещественное значение</summary>
      public const string XMLDoubleValueAttribute = "DoubleValue";
      /// <summary>Атрибут нода - целочисленное значение</summary>
      public const string XMLIntegerValueAttribute = "IntegerValue";
      /// <summary>Атрибут нода - значение времени/даты</summary>
      public const string XMLDateValueAttribute = "DateValue";
      /// <summary>Атрибут нода - дополнительное значение</summary>
      public const string XMLTagValueAttribute = "TagValue";
      /// <summary>Атрибут нода - идентификатор прав</summary>
      public const string XMLRightIDValueAttribute = "RightID";
      /// <summary>Атрибут нода - тип прав</summary>
      public const string XMLRightTypeValueAttribute = "RightType";
      /// <summary>Атрибут нода - пользователь</summary>
      public const string XMLUserIDValueAttribute = "UserID";
      /// <summary>Атрибут нода - владелец</summary>
      public const string XMLOwnerIDValueAttribute = "OwnerID";
      /// <summary>Атрибут нода - дата и время начала</summary>
      public const string XMLBeginDateValueAttribute = "BeginDate";
      /// <summary>Атрибут нода - дата и время окончания</summary>
      public const string XMLEndDateValueAttribute = "EndDate";
      /// <summary>Название нода "Допустимые значения"</summary>
      public const string F_POSSIBLE_VALUES = "F_POSSIBLE_VALUES";
      /// <summary>Название нода "Данные схемы ЖЦ"</summary>
      public const string F_SCHEMA_DATA = "F_SCHEMA_DATA";
      /// <summary>Название нода "Только чтение"</summary>
      public const string F_READ_ONLY = "F_READ_ONLY";
      /// <summary>GUID уровня продвижения</summary>
      public const string F_LEVEL_GUID = "F_LEVEL_GUID";
      /// <summary>Классификация создаваемых объектов</summary>
      public const string F_CLASSIFY_TYPE = "F_CLASSIFY_TYPE";
      /// <summary>Безопастность</summary>
      public const string F_ACCESS = "F_ACCESS";
      /// <summary>Расширенные метаданные</summary>
      public const string F_EXTENSIONS = "F_EXTENSIONS";
      /// <summary>Безопасность</summary>
      public static readonly string AccessNodeText = LocalizationHolder.rm.GetString("Interfaces_795");
      /// <summary>Значение "Текущая дата"</summary>
      public const string NOW_DATE = "NOW";
      /// <summary>Значение "Текущий пользователь"</summary>
      public const string CURRENT_USER = "CURRENT";
      /// <summary>Шаблон имени файла с иконкой</summary>
      public const string IconName = "icon{0}{1}.dat";
      /// <summary>Шаблон имени файла с DataSet схемы ЖЦ</summary>
      public const string SchemaFileName = "schema{0}.dat";
      /// <summary>Шаблон имени файла с графическими данными схемы ЖЦ</summary>
      public const string DrawDataFileName = "draw{0}.dat";
      /// <summary>Шаблон имени файла с блобом/мемо</summary>
      public const string BlobFileName = "blob{0}{1}_{2}.dat";
      /// <summary>
      /// Префикс в названии нода, в котором содержиться информация по атрибуту
      /// </summary>
      private const string _attributePrefix = "@A";

      /// <summary>
      /// Опции атрибутов, поддерживаемые в скриптах автообновления метаданных.
      /// </summary>
      public static AttributeOptions[] AllowedAttributeOptions { get; } = UpdateScriptHelper.GetAllowedAttributeOptionsSlow();

      private static AttributeOptions[] GetAllowedAttributeOptionsSlow()
      {
        List<AttributeOptions> attributeOptionsList = new List<AttributeOptions>();
        foreach (AttributeOptions enumValue in typeof (AttributeOptions).GetEnumValues())
        {
          bool flag = false;
          object[] customAttributes = typeof (AttributeOptions).GetField(enumValue.ToString()).GetCustomAttributes(typeof (AutoUpdateParameters), false);
          if (customAttributes.Length != 0)
            flag = ((AutoUpdateParameters) customAttributes[0]).UsedInScripts;
          if (flag)
            attributeOptionsList.Add(enumValue);
        }
        return attributeOptionsList.ToArray();
      }

      /// <summary>Определяет, содержит ли нод информацию по атрибуту</summary>
      /// <param name="name"></param>
      /// <returns></returns>
      public static bool IsAttributeNode(string name)
      {
        return name != null ? name.StartsWith("@A") : throw new ArgumentNullException(nameof (name));
      }

      public static string RemoveAttributeNodeNamePrefix(string name)
      {
        if (name == null)
          throw new ArgumentNullException(nameof (name));
        return !name.StartsWith("@A") ? name : name.Remove(0, "@A".Length);
      }

      /// <summary>Получает Guid атрибута из названия нода</summary>
      /// <param name="name">Название нода</param>
      /// <returns>Guid атрибута или Guid.Empty</returns>
      public static Guid GetAttributeGuidFromNode(string name)
      {
        name = UpdateScriptHelper.RemoveAttributeNodeNamePrefix(name);
        return !GuidHelper.IsGuid(name) ? Guid.Empty : new Guid(name);
      }

      public static string GetAttributeNodeNameFromGuid(Guid attributeGuid, bool includePrefix)
      {
        string nodeNameFromGuid = attributeGuid.ToString("D");
        if (includePrefix)
          nodeNameFromGuid = "@A" + nodeNameFromGuid;
        return nodeNameFromGuid;
      }
    }
}
