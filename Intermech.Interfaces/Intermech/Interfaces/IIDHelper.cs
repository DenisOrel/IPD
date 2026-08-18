
// Type: Intermech.Interfaces.IIDHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System;


namespace Intermech.Interfaces
{
    public interface IIDHelper
    {
      /// <summary>
      /// ATTRIBUTE_ID атрибута "Дата модификации содержимого объекта"
      /// </summary>
      int ModifyContentDateID { get; }

      /// <summary>
      /// Возвращает true, если данный тип связи typeID сортируемый вручную (имеет атрибут Сортировка)
      /// </summary>
      bool IsSortedRelationType(int typeID);

      /// <summary>ATTRIBUTE_ID атрибута "Сортировка"</summary>
      int SortIndexID { get; }

      /// <summary>OBJECT_TYPE типа "Загружаемый модуль"</summary>
      int PluginTypeID { get; }

      /// <summary>OBJECT_ID роли "Администратор"</summary>
      long AdminRoleID { get; }

      /// <summary>OBJECT_ID роли "Внутренняя служба IPS"</summary>
      long InternalServiceRoleID { get; }

      /// <summary>OBJECT_ID группы "Владелец объекта"</summary>
      long OwnerGroupID { get; }

      /// <summary>OBJECT_ID группы "СОЗДАТЕЛЬ_ОБЪЕКТА"</summary>
      long ObjectCreatorGroupID { get; }

      /// <summary>OBJECT_ID группы "СОЗДАТЕЛЬ_СВЯЗИ"</summary>
      long RelationCreatorGroupID { get; }

      /// <summary>Получает F_OBJECT_ID для группы ВСЕ ПОЛЬЗОВАТЕЛИ</summary>
      long AllUsersGroupID { get; }

      /// <summary>LEVEL_ID удаленных объектов</summary>
      int DeletedID { get; }

      /// <summary>OBJECT_ID системного администратора</summary>
      long SysdbaID { get; }

      /// <summary>OBJECT_ID пользователя "Система"</summary>
      long SystemID { get; }

      /// <summary>LANGUAGE_ID, принятый по умолчанию</summary>
      string DefaultLanguageID { get; }

      /// <summary>OBJECT_TYPE объектов типа Пользователи</summary>
      int UsersTypeID { get; }

      /// <summary>OBJECT_TYPE объектов типа Группы</summary>
      int GroupsTypeID { get; }

      /// <summary>OBJECT_TYPE файловых шкафов</summary>
      int StorageTypeID { get; }

      /// <summary>ATTRIBUTE_ID имени пользователя для входа в систему</summary>
      int LoginNameID { get; }

      /// <summary>ATTRIBUTE_ID пароля</summary>
      int PasswordID { get; }

      /// <summary>ATTRIBUTE_ID атрибута "Внешний пользователь"</summary>
      int ExternalUserID { get; }

      /// <summary>ATTRIBUTE_ID имени пользователя для отображения</summary>
      int UserNameID { get; }

      /// <summary>OBJECT_TYPE объектов типа Роли</summary>
      int RolesTypeID { get; }

      /// <summary>OBJECT_TYPE типа Единица измерения</summary>
      int MeasureTypeID { get; }

      /// <summary>ATTRIBUTE_ID наименования</summary>
      int NameID { get; }

      /// <summary>ATTRIBUTE_ID краткого наименования</summary>
      int ShortNameID { get; }

      /// <summary>ATTRIBUTE_ID обозначения</summary>
      int DesignationID { get; }

      /// <summary>RELATION_TYPE простой вертикальной связи</summary>
      int SimpleRelationTypeID { get; }

      /// <summary>OBJECT_TYPE типа Физическая величина</summary>
      int PhysicValueTypeID { get; }

      /// <summary>OBJECT_TYPE типа Конфигурационные данные</summary>
      int ConfigDataTypeID { get; }

      /// <summary>OBJECT_TYPE "Рабочий стол"</summary>
      int WorkspaceTypeID { get; }

      /// <summary>LEVEL_ID "Персональный объект"</summary>
      int PersonalLevelID { get; }

      /// <summary>ATTRIBUTE_ID "Файл"</summary>
      int FileAttributeID { get; }

      /// <summary>ATTRIBUTE_ID "Конфигурационные файлы"</summary>
      int ConfigFileAttributeID { get; }

      /// <summary>LEVEL_ID "Созданный объект"</summary>
      int CreatedLevelID { get; }

      /// <summary>RELATION_TYPE проектной связи</summary>
      int SPRelationTypeID { get; }

      /// <summary>RELATION_TYPE документации на изделие</summary>
      int DocRelationTypeID { get; }

      /// <summary>ATTRIBUTE_ID атрибута "Ключ папки классификатора"</summary>
      int FolderKeyID { get; }

      /// <summary>OBJECT_TYPE объектов типа Должности</summary>
      int RanksTypeID { get; }

      /// <summary>OBJECT_TYPE объектов типа "Проекты"</summary>
      int ProjectsTypeID { get; }

      /// <summary>
      /// Возвращает ид. атрибута по строковому представлению его глобального идентификатора
      /// </summary>
      int GetAttributeID(string attributeGuid);

      /// <summary>
      /// Возвращает ид. типа объектов по строковому представлению его глобального идентификатора
      /// </summary>
      int GetObjectTypeID(string otGuid);

      /// <summary>
      /// Возвращает ид. типа связей по строковому представлению его глобального идентификатора
      /// </summary>
      int GetRelationTypeID(string rtGuid);

      /// <summary>ID атрибута "Идентификатор версии в составе"</summary>
      int CompositionVersionID { get; }

      /// <summary>
      /// ID атрибута "Сохранённый идентификатор версии в составе"
      /// </summary>
      int CompositionVersionBackup { get; }

      /// <summary>ID атрибута "Настройки"</summary>
      int SettingsAttributeID { get; }

      /// <summary>ID атрибута "Номер группы заменителей"</summary>
      int SubstitutesGroupNoID { get; }

      /// <summary>ID атрибута "Номер заменителя в группе"</summary>
      int SubstituteInGroup { get; }

      /// <summary>
      /// Возвращает идентификатор обязательного атрибута по его глобальному идентификатору.
      /// Если атрибута с таким гуидом не существует, то возвращается ObligatoryObjectAttributes.Zero.
      /// Если атрибут является не обязательным, то возвращает ObligatoryObjectAttributes.None;
      /// </summary>
      ObligatoryObjectAttributes GetObligatoryAttributeID(Guid guid);

      /// <summary>RELATION_TYPE простой связи с сортировкой</summary>
      int SortedRelationTypeID { get; }

      /// <summary>ID типа объекта "Правило подбора версий"</summary>
      int objtypeVersionRule { get; }

      /// <summary>ID типа объекта "Общее правило подбора версий"</summary>
      int objtypeVersionRuleCommon { get; }

      /// <summary>ID типа объекта "Персональное правило подбора версий"</summary>
      int objtypeVersionRuleUser { get; }

      /// <summary>ID типа объекта "Системное правило подбора версий"</summary>
      int objtypeVersionRuleSystem { get; }

      /// <summary>ID атрибута "Уровень безопасности"</summary>
      int SecurityLevelID { get; }

      /// <summary>LEVEL_ID "Аннулирование"</summary>
      int AnnulmentLevelID { get; }

      /// <summary>LEVEL_ID хранение</summary>
      int KeepingLevelID { get; }

      /// <summary>ATTRIBUTE_ID "Литера"</summary>
      int LiteraID { get; }

      /// <summary>ATTRIBUTE_ID "Идентификатор версии в составе"</summary>
      int AttributeVersionInRelation { get; }

      /// <summary>ATTRIBUTE_ID Внутренний регистрационный номер</summary>
      int InternalRegNumber { get; }

      /// <summary>ID атрибута "Идентификатор активной итерации"</summary>
      int ActiveSnapshotID { get; }

      /// <summary>ID атрибута "Графические замечания к документам"</summary>
      int AttributeRedlining { get; }

      /// <summary>ID атрибута "Необходима публикация на портал"</summary>
      int AttributePublicationNecessary { get; }

      /// <summary>ID атрибута "Опции публикации"</summary>
      int AttributeOptionPublication { get; }

      /// <summary>ID типа объекта "Неполный ссылочный объект"</summary>
      int objtypeIncompleteObject { get; }

      /// <summary>Атрибут "Условие проверки прав доступа"</summary>
      int AttributeAccessCondition { get; }

      /// <summary>Атрибут "Изменил карточку объекта"</summary>
      int AttributeLastEditorID { get; }
    }
}
