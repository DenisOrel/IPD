
// Type: Intermech.SystemGUIDs
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech
{
    public class SystemGUIDs
    {
      /// <summary>тип объектов Уведомляющая выборка</summary>
      public const string NotifySamplesTypeGuid = "cadd96c2-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут Изменил карточку объекта</summary>
      public const string attributeLastEditorGuid = "cadd9b77-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта "Объекты производственной ведомости"</summary>
      public const string objtypeProductionObjects = "cadd9a56-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта "Объекты производственной ведомости"</summary>
      public static readonly Guid objtypeProductionObjectsGuid = new Guid("cadd9a56-306c-11d8-b4e9-00304f19f545");
      /// <summary>Атрибут Условие проверки прав доступа</summary>
      public const string attributeAccessConditionGuid = "cadd9a26-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Процесс"</summary>
      public const string attributeProcessGuidStr = "cad002ce-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Процесс"</summary>
      public static readonly Guid attributeProcessGuid = new Guid("cad002ce-306c-11d8-b4e9-00304f19f545");
      /// <summary>Атрибут "Родительское действие"</summary>
      public static readonly Guid AttrParentActivityGuid = new Guid("cad002cf-306c-11d8-b4e9-00304f19f545");
      /// <summary>Атрибут "Дата и время автосохранения итерации"</summary>
      public static readonly string attributeLastAutoSnapshotDate = "cadd96b9-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Атрибут "Максимальный суммарный размер файлов в шкафу, ГБ"
      /// </summary>
      public static readonly Guid attributeMaxStorageSize = new Guid("cadd98dc-306c-11d8-b4e9-00304f19f545");
      /// <summary>Атрибут "Файловый шкаф для хранения итераций"</summary>
      public static readonly Guid attributeSnapshotStorage = new Guid("cadd96ba-306c-11d8-b4e9-00304f19f545");
      /// <summary>Атрибут "Архив"</summary>
      public static readonly Guid attributeArchive = new Guid("cad0011f-306c-11d8-b4e9-00304f19f545");
      /// <summary>Атрибут "Административная роль"</summary>
      public static readonly Guid attributeIsAdminRole = new Guid("cadd96b6-306c-11d8-b4e9-00304f19f545");
      /// <summary>Группа пользователей "МЕНЕДЖЕРЫ_ПРОЕКТА"</summary>
      public const string groupProjectManagers = "cadd9b91-306c-11d8-b4e9-00304f19f545";
      /// <summary>Группа пользователей "УЧАСТНИКИ_ПРОЕКТА"</summary>
      public const string groupProjectMembers = "cadd9b93-306c-11d8-b4e9-00304f19f545";
      /// <summary>Группа пользователей "СОЗДАТЕЛЬ_СВЯЗИ"</summary>
      public const string groupRelationCreator = "cadd96b3-306c-11d8-b4e9-00304f19f545";
      /// <summary>Группа пользователей "СОЗДАТЕЛЬ_ОБЪЕКТА"</summary>
      public const string groupObjectCreator = "cadd96b1-306c-11d8-b4e9-00304f19f545";
      /// <summary>Роль "Внутренняя служба IPS"</summary>
      public const string roleInternalService = "cadd96ad-306c-11d8-b4e9-00304f19f545";
      /// <summary>Юзер "Служба автообновления настроек IPS"</summary>
      public const string userInternalSettingsUpdater = "cadd96af-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Ссылка на объект-прототип"</summary>
      public const string attributeObjectPrototype = "cadd9668-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Неполный ссылочный объект"</summary>
      public const string objTypeIncompleteObject = "cadd960d-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Настройка подписей"</summary>
      public const string attributeSignsSetup = "cad00148-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип связи Вложения</summary>
      public const string relationTypeAttachmentsStr = "cad01329-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип связи Вложения</summary>
      public static readonly Guid relationTypeAttachments = new Guid("cad01329-306c-11d8-b4e9-00304f19f545");
      /// <summary>Guid типа объектов Листы рассылки</summary>
      public static readonly Guid objtypeDeliveryList = new Guid("cadd9365-306c-11d8-b4e9-00304f19f545");
      /// <summary>Guid атрибута Идентификатор документа (ОТД)</summary>
      public static readonly Guid attributeOriginalObject = new Guid("cadd935a-306c-11d8-b4e9-00304f19f545");
      /// <summary>Guid атрибута Абоненты</summary>
      public static readonly Guid attributeSubscribers = new Guid("cadd9351-306c-11d8-b4e9-00304f19f545");
      /// <summary>Атрибут "Идентификатор предыдущей версии объекта"</summary>
      public const string attributePrevObjectID = "cadd9597-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Сохранённый идентификатор версии в составе"</summary>
      public const string attributeCompositionVerBackup = "cadd955d-306c-11d8-b4e9-00304f19f545";
      /// <summary>Шаг ЖЦ Внешний пользователь</summary>
      public const string stepExternal_User = "cadd9502-306c-11d8-b4e9-00304f19f545";
      /// <summary>Шаг ЖЦ Уволен (для пользователя)</summary>
      public const string stepFired_User = "cadd9504-306c-11d8-b4e9-00304f19f545";
      /// <summary>Шаг ЖЦ Создан (для пользователя)</summary>
      public const string stepIPS_User = "cadd9503-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Роль"</summary>
      public const string attributeRole = "cadd94e6-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Должность"</summary>
      public const string attributeRank = "cad00142-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Исполняющий обязанности"</summary>
      public const string attributeIOUser = "cadd91f5-306c-11d8-b4e9-00304f19f545";
      /// <summary>тип объектов "Настройки исполнения обязанностей"</summary>
      public const string objtypeIOSettings = "cadd94e2-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Конечная дата"</summary>
      public const string attributeIOEndDate = "cadd94e3-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Начальная дата"</summary>
      public const string attributeIOBeginDate = "cadd94e4-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Исполняет обязанности пользователей"</summary>
      public const string attributeIO = "cad015c9-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Идентификатор активной итерации"</summary>
      public const string attributeActiveSnapshotID = "cadd94ce-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Внутренний регистрационный номер"</summary>
      public const string attributeInternalRegNumber = "cadd9430-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Дополнительные наименования"</summary>
      public const string attributeDopNames = "cadd93b1-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Графические замечания к документам"</summary>
      public const string attributeRedlining = "cad0036f-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Статус замечаний"</summary>
      public const string attributeRemarksState = "cadd9abe-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Пользователь заблокирован"</summary>
      public const string attributeLockedUser = "cadd99fb-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Узел информационной сети"</summary>
      public const string attributeF_SITE_ID = "cad01501-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Блокирование настройки видимости закладок"</summary>
      public const string attributeRoleBlockViews = "cadd93ab-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Блокирование настройки контекстных меню"</summary>
      public const string attributeRoleBlockMenus = "cadd93a9-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Блокирование настройки отображения составов"</summary>
      public const string attributeRoleBlockCompositions = "cadd93aa-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Блокирование панелей инструментов составов"</summary>
      public const string attributeRoleBlockToolbars = "cad014b5-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Уровень безопасности"</summary>
      public const string attributeSecurityLevel = "cad00816-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Шаблон состава объекта"</summary>
      public const string attributeObjectTemplate = "cad00815-306c-11d8-b4e9-00304f19f545";
      /// <summary>тип объектов "Библиотечные изображения"</summary>
      public const string objtypeLibraryImage = "cad00140-306c-11d8-b4e9-00304f19f545";
      /// <summary>тип объектов "Внешние криптопровайдеры"</summary>
      public const string objtypeExternalCryptoproviders = "cad00153-306c-11d8-b4e9-00304f19f545";
      /// <summary>тип объектов "Проекты"</summary>
      public const string objtypeProjects = "cad00812-306c-11d8-b4e9-00304f19f545";
      /// <summary>тип объектов "Форма редактирования информации"</summary>
      public const string objtypeForms = "cad0011b-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// тип объектов "Форма редактирования атрибутов объектов и связей"
      /// </summary>
      public const string objtypeFormDataEditingType = "cad0011c-306c-11d8-b4e9-00304f19f545";
      /// <summary>тип объектов "Шаблоны проектов"</summary>
      public const string objtypeProjectTemplates = "cad00813-306c-11d8-b4e9-00304f19f545";
      /// <summary>тип объектов "Шаблоны объектов"</summary>
      public const string objtypeObjectTemplates = "cad00822-306c-11d8-b4e9-00304f19f545";
      /// <summary>тип объектов "Комплекты рабочих чертежей зданий"</summary>
      public const string objtypeBuildingsDocumentSet = "cad0088e-306c-11d8-b4e9-00304f19f545";
      /// <summary>тип объектов "Комплекты рабочих чертежей площадок"</summary>
      public const string objtypeSiteDocumentSet = "cad0088f-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Принадлежность проекту"</summary>
      public const string attributeF_PROJECT_ID = "cad00811-306c-11d8-b4e9-00304f19f545";
      /// <summary>Глобальный идентификатор объекта (IDBObject.GUID)</summary>
      public const string objectGUID = "cad00800-306c-11d8-b4e9-00304f19f545";
      /// <summary>Состояние версии объекта (ObjectKind)</summary>
      public const string attributeF_OBJECT_VER_TYPE = "cadd937c-306c-11d8-b4e9-00304f19f545";
      /// <summary>Загружаемый модуль "Редактор документов и шаблонов"</summary>
      public const string objectDocumentEditorGuid = "cad00735-306c-11d8-b4e9-00304f19f545";
      /// <summary>Загружаемый модуль "Навигатор"</summary>
      public const string objectNavigatorGuid = "cad00720-306c-11d8-b4e9-00304f19f545";
      /// <summary>Загружаемый модуль "Ядро службы инструментов"</summary>
      public const string objectToolsClient = "cad014ad-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Загружаемый модуль "Интегратор с внешними программами"
      /// </summary>
      public const string objectExtProgramsGuid = "cad0073a-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Загружаемый модуль "Среда разработчика сценариев (Script pad)"
      /// </summary>
      public const string objectScriptingClientGuid = "cadd9a3f-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип связи "Простая связь с сортировкой"</summary>
      public const string reltypeSortedGuid = "cad00151-306c-11d8-b4e9-00304f19f545";
      /// <summary>Единица измерения "Штуки"</summary>
      public const string objectShtuki = "cad002e8-306c-11d8-b4e9-00304f19f545";
      public static Guid objectShtukiGuid = new Guid("cad002e8-306c-11d8-b4e9-00304f19f545");
      /// <summary>Единица измерения "Килограммы"</summary>
      public const string objectKilograms = "cad002eb-306c-11d8-b4e9-00304f19f545";
      public static Guid objectKilogramsGuid = new Guid("cad002eb-306c-11d8-b4e9-00304f19f545");
      /// <summary>Физическая величина "Количество"</summary>
      public const string objectQuantity = "cad002e7-306c-11d8-b4e9-00304f19f545";
      public static Guid objectQuantityGuid = new Guid("cad002e7-306c-11d8-b4e9-00304f19f545");
      /// <summary>Физическая величина "Длина"</summary>
      public const string objectLength = "cad002e2-306c-11d8-b4e9-00304f19f545";
      public static Guid objectLengthGuid = new Guid("cad002e2-306c-11d8-b4e9-00304f19f545");
      /// <summary>Физическая величина "Объем"</summary>
      public const string objectVolume = "cad002ef-306c-11d8-b4e9-00304f19f545";
      public static Guid objectVolumeGuid = new Guid("cad002ef-306c-11d8-b4e9-00304f19f545");
      /// <summary>Физическая величина "Масса"</summary>
      public const string objectMass = "cad002e9-306c-11d8-b4e9-00304f19f545";
      public static Guid objectMassGuid = new Guid("cad002e9-306c-11d8-b4e9-00304f19f545");
      /// <summary>Физическая величина "Площадь"</summary>
      public const string objectSquare = "cad002f4-306c-11d8-b4e9-00304f19f545";
      public static Guid objectSquareGuid = new Guid("cad002f4-306c-11d8-b4e9-00304f19f545");
      /// <summary>
      /// Набор физических величин, которые используются в системе в контексте "Количество" по умолчанию (может расширяться настройкой IPS)
      /// </summary>
      public static Guid[] objectQuantityPhysListGuids = new Guid[5]
      {
        SystemGUIDs.objectLengthGuid,
        SystemGUIDs.objectQuantityGuid,
        SystemGUIDs.objectMassGuid,
        SystemGUIDs.objectVolumeGuid,
        SystemGUIDs.objectSquareGuid
      };
      /// <summary>атрибут "Идентификатор файла"</summary>
      public const string attributeF_FILE_ID = "cad001f2-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Имя файла"</summary>
      public const string attributeF_FILENAME = "cad001f3-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Размер файла"</summary>
      public const string attributeF_FILESIZE = "cad001f4-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Дата обновления файла"</summary>
      public const string attributeF_FILEDATE = "cad001f5-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Упакованный размер файла"</summary>
      public const string attributeF_ZIPSIZE = "cad001f6-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Идентификатор объекта/связи"</summary>
      public const string attributeF_OBJECTLINK_ID = "cad001f7-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Метод упаковки файла"</summary>
      public const string attributeF_ARC_METHOD = "cad001f8-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// тип объектов "Персональные прототипы для файловых объектов"
      /// </summary>
      public const string objtypeFilePrivatePrototype = "cad00347-306c-11d8-b4e9-00304f19f545";
      /// <summary>тип объектов "Общие прототипы для файловых объектов"</summary>
      public const string objtypeFileCommonPrototype = "cad00346-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Ссылка на объект"</summary>
      public const string attributeObjectLink = "cad001be-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Объект уведомлений"</summary>
      public const string attributeNotifyObject = "cad0062c-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Даты постановки на уведомление"</summary>
      public const string attributeNotifyDates = "cad0062a-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Свойства уведомлений"</summary>
      public const string attributeNotifyOptions = "cad0062b-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Перв.прим."</summary>
      public const string attributeFirstApplicability = "cad00285-306c-11d8-b4e9-00304f19f545";
      /// <summary>автрибу "Назначение выборки"</summary>
      public const string attributeSampleFunction = "cad00345-306c-11d8-b4e9-00304f19f545";
      /// <summary>тип объектов "Уведомления об изменениях"</summary>
      public const string objtypeNoticesOnChanges = "cad00627-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Список получателей уведомления"</summary>
      public const string attributeAddresseeNotice = "cad00628-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Комментарий к уведомлению"</summary>
      public const string attributeNotifyComment = "cadd9940-306c-11d8-b4e9-00304f19f545";
      /// <summary>обязательный атрибута F_PRJ_GUID</summary>
      public const string attributeF_PRJ_GUID = "cad00344-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// атрибут-индекс "Нормализованный идентификатор объекта"
      /// </summary>
      public const string attributeProductNameNorm = "cad0011a-306c-11d8-b4e9-00304f19f545";
      /// <summary>тип объектов "Прототипы для файловых объектов"</summary>
      public const string objtypeFilePrototype = "cad00342-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Тип объекта "Разделы конструкторских ведомостей" (используется в AVS)
      /// </summary>
      public const string objtypeVedomostiSection = "cad002a7-306c-11d8-b4e9-00304f19f545";
      /// <summary>предметная область "Администрирование системы"</summary>
      public const string subjectAdmin = "cad002d8-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Табличные отчеты"</summary>
      public const string objtypeTableReports = "cad00288-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Не показывать форму при создании объекта"</summary>
      public const string attributeDontShowFormOnCreateObject = "cadd9212-306c-11d8-b4e9-00304f19f545";
      /// <summary>Security Identifier</summary>
      public const string attributeSID = "cadd93c1-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Данные"</summary>
      public const string AttributeData = "cad001b2-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Глобальный идентификатор атрибута "Конкретизация версий объектов в ручных выборках"
      /// </summary>
      public const string attributeConcretizationVersionInManualSelection = "cadd99b3-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Глобальный идентификатор атрибута "Искать среди объектов глобальных и локальных типов"
      /// </summary>
      public const string attributeFindInLocalTypes = "cadd9971-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта "Экземпляры и партии изделий"</summary>
      public const string objtypeInstancesAndParties = "cad00583-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта "Экземпляры деталей"</summary>
      public const string objtypePartsInstances = "cad0058d-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта "Партии деталей"</summary>
      public const string objtypePartiesOfDetails = "cad0058e-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта "Экземпляры комплексов"</summary>
      public const string objtypeComplexInstances = "cad01473-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта "Партии комплексов"</summary>
      public const string objtypePartiesOfComplexes = "cad01472-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта "Экземпляры комплектов"</summary>
      public const string objtypeComplectInstances = "cad01475-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта "Партии комплектов"</summary>
      public const string objtypePartiesOfComplects = "cad01474-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта "Экземпляры прочих изделий"</summary>
      public const string objtypeOtherPartInstances = "cad01471-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта "Партии прочих изделий"</summary>
      public const string objtypePartiesOfOtherParts = "cad01470-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта "Экземпляры сборочных единиц"</summary>
      public const string objtypeAssemblyUnitsInstances = "cad0058b-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта "Партии сборочных единиц"</summary>
      public const string objtypePartiesOfAssemblyUnits = "cad0058c-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта "Экземпляры стандартных изделий"</summary>
      public const string objtypeStandardPartInstances = "cad0063c-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта "Партии стандартных изделий"</summary>
      public const string objtypePartiesOfStandardParts = "cad0063d-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Группа атрибутов "Атрибуты для производства (MRP-система)" (MRP)
      /// </summary>
      public const string groupMRPAttributes = "cadd92eb-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Идентификатор версии изделия" (MRP)</summary>
      public const string attributeArticleVersionID = "cadd92f0-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Создана на основе связи" (MRP)</summary>
      public const string attributeCreatedOnRelation = "cadd92ec-306c-11d8-b4e9-00304f19f545";
      /// <summary>Предметная область "Производство" (MRP)</summary>
      public const string subjectAreaProduction = "cadd92ea-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Производственные заказы" (MRP)</summary>
      public const string objtypeProductionOrders = "cadd92e9-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта "Партии комплектаций" (MRP)</summary>
      public const string objtypeEquipmentParty = "cadd92ee-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта "Экземпляры комплектаций" (MRP)</summary>
      public const string objtypeExemplarParty = "cadd92ef-306c-11d8-b4e9-00304f19f545";
      /// <summary>Объект "Подготовка производства (MRP)" (MRP)</summary>
      public const string objectMRPPlugin = "cadd92f4-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Атрибут "Признак изготовления" (1 - Собственное, 2 - Покупное, 3 - По кооперации, 4 - Не изготавливать)
      /// </summary>
      public const string attributeManufacturingSign = "cad0038f-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Атрибут "Учёт изделий в производстве", целочисленный тип, одно значение из списка разрешённых:
      /// 0 - Партиями, 1 - Экземплярами
      /// </summary>
      public const string attributeProductionAccountingOfParts = "cad0058a-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта "Производственная ведомость"</summary>
      public const string objtypeProductionLists = "cadd9a5c-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта "Производственные копии"</summary>
      public const string objtypeProductionCopy = "cadd9a5d-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Глобальный контекст редактирования"</summary>
      public const string attributeGlobalEditingContext = "cadd9373-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Номер группы изменений"</summary>
      public const string attributeF_MODIFICATION_ID = "cad014d2-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Импортированное извещение"</summary>
      public const string attributeImportedEco = "cadd91f4-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта "Контексты редактирования"</summary>
      public const string objtypeEditingContexts = "cad0146b-306c-11d8-b4e9-00304f19f545";
      /// <summary>группа атрибутов "Все атрибуты"</summary>
      public const string groupAllAttrs = "cad00341-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// атрибут "Номер группы изменений" (обязательный системный атрибут)
      /// </summary>
      public const string attributeChangesGroupNum = "cad014d2-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Номер взаимосвязанного контекста"</summary>
      public const string attributeLinkedContextNumber = "cad014ff-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// атрибут "Правило подбора версий" (для группирующих объектов, контекстов редактирования)
      /// </summary>
      public const string attributeVersionRule = "cad00696-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// атрибут "Основание для изменений" (для группирующих объектов)
      /// </summary>
      public const string attributeChangesReason = "cad00697-306c-11d8-b4e9-00304f19f545";
      /// <summary>группа атрибутов "Атрибуты группирующих объектов"</summary>
      public const string groupGroupObjectAttrs = "cad00694-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// тип объекта "Варианты изменений" (базовый группирующий объект)
      /// </summary>
      public const string objtypeChangesVariant = "cad00698-306c-11d8-b4e9-00304f19f545";
      /// <summary>тип связи "Изменяемые объекты"</summary>
      public const string reltypeChangingObjects = "cad00699-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Guid системного объекта "Нет категории" (конфигуратор составов IPS)
      /// </summary>
      public const string objectNoCategoryGuid = "cad0159f-306c-11d8-b4e9-00304f19f545";
      /// <summary>Guid типа объекта "Опция" (конфигуратор составов IPS)</summary>
      public const string objtypeOption = "cad015b0-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Guid типа объекта "Категории опций" (конфигуратор составов IPS)
      /// </summary>
      public const string objtypeOptionsGroup = "cad015af-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Guid типа объекта "Объекты конфигуратора составов" (конфигуратор составов IPS)
      /// </summary>
      public const string objtypeConfiguratorObjects = "cad00592-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Guid типа объекта "Комплектации" (конфигуратор составов IPS)
      /// </summary>
      public const string objtypeComplements = "cad015b1-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Комплекты документов"</summary>
      public const string objtypeDocComplects = "cad00199-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Копии документов"</summary>
      public const string objtypeDocCopies = "cadd9364-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Объекты ImProject"</summary>
      public const string objtypeImProject = "cad00e90-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Объекты маршрутизатора составов"</summary>
      public const string objtypeDocumentRouter = "cad002aa-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Опубликованные объекты"</summary>
      public const string objtypePortalObjects = "cad01489-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Строительные объекты"</summary>
      public const string objtypeBuildings = "cad00880-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Уведомления"</summary>
      public const string objtypeNotifications = "cad00629-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Экземпляры и партии материалов базовые"</summary>
      public const string objtypeMaterialInstancies = "cadd950b-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Электронные подписи"</summary>
      public const string objtypeSignatures = "cad00137-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Guid типа объекта "Заказы" (конфигуратор составов IPS)
      /// </summary>
      public const string objtypeOrders = "cad00580-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Guid атрибута "Видимые значения опции" (конфигуратор составов IPS)
      /// </summary>
      public const string attributeVisibleOptionValues = "cad015a1-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Guid атрибута "Ссылка на опции" (конфигуратор составов IPS)
      /// </summary>
      public const string attributeOptionsLink = "cad015a9-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Guid атрибута "Условия применения объекта" (конфигуратор составов IPS)
      /// </summary>
      public const string attributeObjectApplicabilityCond = "cad015ac-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Guid атрибута "Условия несовместимости опций" (конфигуратор составов IPS)
      /// </summary>
      public const string attributeOptionsIncompatibility = "cad015ab-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Guid атрибута "Значения опции" (конфигуратор составов IPS)
      /// </summary>
      public const string attributeOptionValues = "cad015a2-306c-11d8-b4e9-00304f19f545";
      /// <summary>Guid атрибута "Код опции" (конфигуратор составов IPS)</summary>
      public const string attributeOptionCode = "cad015a5-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Guid атрибута "Тип данных опции" (конфигуратор составов IPS)
      /// </summary>
      public const string attributeOptionDataType = "cad015aa-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Guid атрибута "Название опции" (конфигуратор составов IPS)
      /// </summary>
      public const string attributeOptionCaption = "cad015a8-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Guid атрибута "Название категории опций" (конфигуратор составов IPS)
      /// </summary>
      public const string attributeOptionsGroupCaption = "cad015a7-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Guid атрибута "Контекст конфигуратора составов" (конфигуратор составов IPS)
      /// </summary>
      public const string attributeConfiguratorContext = "cad015a6-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Guid атрибута "Ссылка на категорию опции" (конфигуратор составов IPS)
      /// </summary>
      public const string attributeCategoryLink = "cad015a4-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Guid атрибута "Изображение конфигуратора составов" (конфигуратор составов IPS)
      /// </summary>
      public const string attributeConfiguratorImage = "cad015a3-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Guid атрибута "Флажки опции" (конфигуратор составов IPS)
      /// </summary>
      public const string attributeOptionFlags = "cad015ad-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Базовая версия объекта"</summary>
      public const string attributeF_BASE_VERSION = "cad014d3-306c-11d8-b4e9-00304f19f545";
      /// <summary>тип объекта "Правило подбора версий"</summary>
      public const string objtypeVersionRule = "cad001b3-306c-11d8-b4e9-00304f19f545";
      /// <summary>тип объекта "Общее правило подбора версий"</summary>
      public const string objtypeVersionRuleCommon = "cad001b4-306c-11d8-b4e9-00304f19f545";
      /// <summary>тип объекта "Персональное правило подбора версий"</summary>
      public const string objtypeVersionRuleUser = "cad001b5-306c-11d8-b4e9-00304f19f545";
      /// <summary>тип объекта "Системное правило подбора версий"</summary>
      public const string objtypeVersionRuleSystem = "cad00278-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Системное виртуальное правило "Последние версии объектов"
      /// </summary>
      public const string filtrationLatestVersions = "cad001df-306c-11d8-b4e9-00304f19f545";
      /// <summary>Системный объект "Последние версии объектов"</summary>
      public const string filtrationLatestVersionsObject = "cad0069c-306c-11d8-b4e9-00304f19f545";
      /// <summary>Системное виртуальное правило "Все версии объектов"</summary>
      public const string filtrationAllVersions = "cad001e0-306c-11d8-b4e9-00304f19f545";
      /// <summary>Системный объект "Все версии объектов"</summary>
      public const string filtrationAllVersionsObject = "cad001e3-306c-11d8-b4e9-00304f19f545";
      /// <summary>Системное виртуальное правило "Подбор базовых версий"</summary>
      public const string filtrationBaseVersions = "cad00601-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Системный объект "Все версии объектов с учётом конкретизации"
      /// </summary>
      public const string filtrationAllConcreteVersionsObject = "cad005ac-306c-11d8-b4e9-00304f19f5455";
      /// <summary>
      /// Системное виртуальное правило "Последовательное проведение изменений"
      /// </summary>
      public const string filtrationSequentialModifications = "cad00602-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// настройки фильтрации состава "Настройки пользователя по умолчанию"
      /// </summary>
      public const string filtrationUserDefaults = "cad001e2-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// настройки фильтрации состава "Правило подбора версий по умолчанию"
      /// (используется, если требуется правило для редактирования по умолчанию)
      /// </summary>
      public const string filtrationDefaultVersionRule = "cad005aa-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Если указать данное значение в качестве ключа в поле Tags в параметрах запроса,
      /// то в качестве значения можно задать ключ настроек фильтрации составов (OwnerID),
      /// который перекроет ключ, заданный коллекции связей или объектов в параметрах
      /// запроса или в соответствующем свойстве
      /// </summary>
      public const string filtrationOverrideOwnerID = "{7196FEC5-A048-4118-AF15-73BEEAA63A87}";
      /// <summary>
      /// Если указать данное значение в качестве ключа в поле Tags в параметрах запроса,
      /// то в качестве значения можно задать информацию о контексте редактирования в виде
      /// экземпляра типа CurrentEditingContext, который перекроет любые настройки контекстов,
      /// за исключением настроек, переданных в контексте потока
      /// </summary>
      public const string filtrationOverrideEditingContext = "{76094280-391F-44AC-8B7B-9B6DEA501110}";
      /// <summary>атрибут "Идентификатор версии в составе"</summary>
      public const string attributeCompositionVersionID = "cad001c2-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Идентификатор версии в составе"</summary>
      public const string attributeVersionInRelation = "cad001c2-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Режим конкретизации версии в составе"</summary>
      public const string attributeRevisionInstantiationMode = "cadd9609-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Атрибуты ядра системы\Правила"</summary>
      public const string attributeKernelVersionRule = "cad001d2-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// атрибут "Запрет использования правила при редактировании объектов"
      /// </summary>
      public const string attributeKernelUsingProhibited = "cad00820-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Тип объектов "Фильтрация составов" (подбор составов по сериям/датам изделий)
      /// </summary>
      public const string objtypeCompositionsFiltration = "cadd940a-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Тип объектов "Головные изделия" (подбор составов по сериям/датам изделий)
      /// </summary>
      public const string objtypeMasterArticle = "cadd940b-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Атрибут "Применяемость в сериях и датах" (подбор составов по сериям/датам изделий).
      /// В атрибуте хранится закодированная информация о применяемости объекта в изделиях и сериях
      /// </summary>
      public const string attributeSeriesApplicabilities = "cadd940c-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// атрибут "Атрибуты ядра системы\Сортировка и отображение составов"
      /// </summary>
      public const string attributeKernelCompositionSorting = "cad00691-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Настройки видов Навигатора"</summary>
      public const string attributeNavigatorViewSettings = "cad01487-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Атрибуты ядра системы\Конфигурации ролей"</summary>
      public const string attributeKernelRolesConfig = "cad00692-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// экземпляр объекта "Настройки роли по умолчанию" (тип объекта "Конфигурации ролей")
      /// </summary>
      public const string objectRolesDefaultConfig = "cad00693-306c-11d8-b4e9-00304f19f545";
      /// <summary>виртуальный атрибут "Результаты фильтрации состава"</summary>
      public const string virtualAttributeFiltrationResults = "cad001f0-306c-11d8-b4e9-00304f19f545";
      /// <summary>плагин "Подбор версий"</summary>
      public const string pluginVersionsSelection = "cad005f2-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// запрет плагину "Подбор версий" добавлять статусы в столбец "Статусы элемента"
      /// </summary>
      public const string pluginVersionsSelectionDisable = "cad005f7-306c-11d8-b4e9-00304f19f545";
      /// <summary>Плагин производственных ведомостей</summary>
      public const string pluginMRPStatus = "cad8491c-5d67-476f-b87a-f2c6dcd807a2";
      /// <summary>Виртуальный атрибут "Статусы элемента"</summary>
      public const string virtualAttributeElementStatuses = "cad005f1-306c-11d8-b4e9-00304f19f545";
      /// <summary>Виртуальный атрибут "Актуальная дата"</summary>
      public const string virtualAttributeActualDate = "cad0080f-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Виртуальный атрибут "Идентификатор родительской версии объекта"
      /// </summary>
      public const string virtualAttributeParentObjectID = "cadd9717-306c-11d8-b4e9-00304f19f545";
      /// <summary>Виртуальный атрибут "Количество версий объекта"</summary>
      public const string virtualAttributeVersionsCount = "cadd98e9-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Виртуальный атрибут "Количество ссылок на версию объекта"
      /// </summary>
      public const string virtualAttributeReferencesCount = "cadd98ed-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Виртуальный атрибут "Количество входимостей в версии объектов"
      /// </summary>
      public const string virtualAttributeRelationsCount = "cadd98ee-306c-11d8-b4e9-00304f19f545";
      /// <summary>Виртуальный атрибут "Дата изменения шага ЖЦ"</summary>
      public const string virtualAttributeLCStepDate = "cadd9972-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Номер группы заменителей"</summary>
      public const string attributeSubstitutesGroupNo = "cad001c0-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Номер заменителя в группе"</summary>
      public const string attributeSubstituteInGroup = "cad001c1-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Имя группы заменителей"</summary>
      public const string attributeSubstituteGroupName = "cad00817-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Имя заменителя"</summary>
      public const string attributeSubstituteName = "cad00818-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Расшифровка допустимых замен"</summary>
      public const string attributeSubstitutesText = "cad00274-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Конструкторский основной вариант"</summary>
      public const string attributeDesignerActualVariant = "cad00654-306c-11d8-b4e9-00304f19f545";
      /// <summary>статусы "Уровень продвижения"</summary>
      public const string statusesLevels = "{7074E0E4-B3AB-4B3E-AD56-050CD256AF10}";
      /// <summary>запрет на отображение статусов "Уровень продвижения"</summary>
      public const string statusesLevelsDisable = "{76FCDEFA-59AF-4468-8BA6-AEF9ACB20795}";
      /// <summary>Тип связи "Технологический состав"</summary>
      public const string reltypeTechComposition = "cad0019f-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта "Маршрут обработки"</summary>
      public const string objtypeRouteProcessing = "cad0016f-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Глобальный идентификатор типа объектов "Контейнер службы параметров"
      /// </summary>
      public const string objTypeParamsStorage = "cadd940d-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Описание"</summary>
      public const string attributeDescription = "cad0001c-306c-11d8-b4e9-00304f19f545";
      /// <summary>Глобальный идентификатор атрибута "Руководитель"</summary>
      public const string attributeDirector = "cadd9233-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Глобальный идентификатор типа объектов "Подразделения"
      /// </summary>
      public const string objtypeDepartment = "cadd9232-306c-11d8-b4e9-00304f19f545";
      /// <summary>Глобальный идентификатор типа объектов "Организации"</summary>
      public const string objtypeOrganization = "cadd9231-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Поз.обозначение"</summary>
      public const string attributePosDesignation = "cad01478-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Ключ Imbase"</summary>
      public const string attributeImbaseKey = "cad00162-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Код Imbase"</summary>
      public const string attributeImbaseCode = "cad0020f-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Ссылка на объект IMBASE"</summary>
      public const string attributeImbaseLink = "cad00209-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Каталог Imbase"</summary>
      public const string objtypeImbaseCatalog = "cad00221-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Марка"</summary>
      public const string objtypeMark = "cad00171-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Материал"</summary>
      public const string objtypeMaterials = "cad00172-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Материал"</summary>
      public const string attrMaterial = "cad0038c-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Материал замена 1"</summary>
      public const string attrMaterialSub1 = "cadd94c2-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Материал замена 2"</summary>
      public const string attrMaterialSub2 = "cadd94c3-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Материал импортирован автоматически"</summary>
      public const string attributeAutoImportMaterial = "cad00797-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Атрибуты ядра системы\Настройки"</summary>
      public const string attributeKernelSettings = "cad001f1-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "предметные области" (необязательный!)</summary>
      public const string attributeAreas = "cad001af-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Дата модификации содержимого объекта"</summary>
      public const string attributeContentModifyDate = "cad0013a-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут F_ATTRIBUTE_ID</summary>
      public const string attributeF_ATTRIBUTE_ID = "cad001ab-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут F_KEY</summary>
      public const string attributeF_KEY = "cad001aa-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Коэффициент приведения к базовой единице"</summary>
      public const string attributeKoefficient = "cad00025-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "По умолчанию"</summary>
      public const string attributeDefault = "cad001a7-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Ключ ImBase"</summary>
      public const string attributeF_IMBASE_KEY = "cad00162-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Должности"</summary>
      public const string objtypeRanks = "cad00147-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Статус записи в журнале истории значений"</summary>
      public const string attributeF_STATUS = "cad0015c-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Дата присвоения значения"</summary>
      public const string attributeF_SET_DATE = "cad0015d-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Целое число"</summary>
      public const string attributeF_INTEGER_VALUE = "cad0015e-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Строка"</summary>
      public const string attributeF_STRING_VALUE = "cad0015f-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Вещественное число"</summary>
      public const string attributeF_DOUBLE_VALUE = "cad00160-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Дата"</summary>
      public const string attributeF_DATE_VALUE = "cad00161-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Ключ папки классификатора"</summary>
      public const string attributeFOLDER_KEY = "cad0014d-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Классификация только в последнюю папку"</summary>
      public const string attributeLastFolderClassificationOnly = "cad0156e-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Дата создания объекта"</summary>
      public const string attributeF_OBJ_CREATE = "cad0013c-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут F_GUID</summary>
      public const string attributeF_GUID = "cad00130-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "предметная область"</summary>
      public const string attributeF_AREA_ID = "cad0012f-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Загрузочный файл"</summary>
      public const string attributeMainPluginFile = "cad00127-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Версия сборки"</summary>
      public const string attributePluginVersion = "cad00126-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Загружаемый модуль"</summary>
      public const string objtypePlugin = "cad0005b-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Файловый шкаф" - ссылка на файловый шкаф</summary>
      public const string attributeStorage = "cad0005c-306c-11d8-b4e9-00304f19f545";
      /// <summary>Объект "Система" - юзер, под которым работает система</summary>
      public const string objectSystem = "cad0000d-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// TimedAccessService - служба назначения временных прав доступа
      /// </summary>
      public const string pluginTimedAccess = "cad0005a-306c-11d8-b4e9-00304f19f545";
      /// <summary>Автоматическое создание итераций</summary>
      public const string pluginAutoSnapshots = "cadd96bb-306c-11d8-b4e9-00304f19f545";
      /// <summary>taskRepairData - задача проверки целостности данных</summary>
      public const string taskRepairData = "cadd93c5-306c-11d8-b4e9-00304f19f545";
      /// <summary>taskDeleteTrash - задача удаления устаревших данных</summary>
      public const string taskDeleteTrash = "cadd93c6-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// taskRebuildViews - задача перегенерации представлений данных
      /// </summary>
      public const string taskRebuildViews = "cadd95b3-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// taskIndexer - задача индексации атрибутов (в общем поисковом индексе)
      /// </summary>
      public const string taskIndexer = "cadd93c7-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// taskComputeRelevancy - Пересчёт релевантности данных в общем поисковом индексе
      /// </summary>
      public const string taskComputeRelevancy = "cadd93c8-306c-11d8-b4e9-00304f19f545";
      /// <summary>Очистка файлового кэша сервера приложений</summary>
      public const string taskClearBlobsChache = "17759ebe-1488-4401-a473-3a0792c60c31";
      /// <summary>Перемещение двоичных данных между файловыми шкафами</summary>
      public const string taskRemoveBlobs = "cadd960a-306c-11d8-b4e9-00304f19f545";
      /// <summary>Сбор статистических данных</summary>
      public const string taskStatistics = "f6d37318-b238-4576-a3ee-7f9daad7373f";
      /// <summary>Диагностика системы</summary>
      public const string taskSystemDiagnostics = "b0982a7e-95ad-4f19-9827-bbb84978a2e9";
      /// <summary>
      /// taskSyncronizeDirectory - задача синхронизации со службой каталогов
      /// </summary>
      public const string taskSyncronizeDirectory = "cadd93f2-306c-11d8-b4e9-00304f19f545";
      /// <summary>Роль "Администраторы"</summary>
      public const string objectAdminRole = "cad00006-306c-11d8-b4e9-00304f19f545";
      /// <summary>Группа "ВЛАДЕЛЕЦ_ОБЪЕКТА"</summary>
      public const string objectOWNER_GROUP = "cad00059-306c-11d8-b4e9-00304f19f545";
      /// <summary>Уровень продвижения "Персональный объект"</summary>
      public const string levelPersonal = "cad00049-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Уровень продвижения "Создание и модификация", обозначающий созданные объекты
      /// </summary>
      public const string levelCreated = "cad00013-306c-11d8-b4e9-00304f19f545";
      /// <summary>Уровень продвижения "Согласование и утверждение"</summary>
      public const string levelSigning = "cad003be-306c-11d8-b4e9-00304f19f545";
      /// <summary>Уровень продвижения "Импортировано"</summary>
      public const string levelImported = "cad0069a-306c-11d8-b4e9-00304f19f545";
      /// <summary>Уровень продвижения "Аннулировано"</summary>
      public const string levelAnnulment = "cad00012-306c-11d8-b4e9-00304f19f545";
      /// <summary>Уровень продвижения "Производство и эксплуатация"</summary>
      public const string levelManufacturing = "cad00011-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Уровень продвижения "Удалено", обозначающий удаленные объекты
      /// </summary>
      public const string levelDeleted = "cad0000e-306c-11d8-b4e9-00304f19f545";
      /// <summary>Уровень продвижения "Хранение"</summary>
      public const string levelKeeping = "cad009de-306c-11d8-b4e9-00304f19f545";
      /// <summary>Файловый шкаф по умолчанию</summary>
      public const string objectDOCUMS = "cad0000c-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Файл"</summary>
      public const string attributeFile = "cad0004b-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Глобальный идентификатор для атрибута "Форма ввода информации".
      /// </summary>
      public const string attributeFormData = "cad0011d-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Гриф документа"</summary>
      public const string attributeDocStamp = "cadd9ac2-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      ///  Атрибут "Файл рабочей копии". Хранит файлы рабочих копий документов, перекачанных из Search
      /// </summary>
      public const string attributeWorkFile = "cadd98bc-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      ///  Атрибут "Файл документа, Хранит файлы, если в файле хранится сканированный файл"
      /// </summary>
      public const string attributeDocumentFile = "cadd9620-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Сканированный документ"</summary>
      public const string attributeScanDocument = "cadd9644-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Конфигурационные файлы"</summary>
      public const string attributeConfigFile = "cad014d4-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Рабочий стол"</summary>
      public const string objtypeWorkspace = "cad0004a-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Конфигурации пользователей"</summary>
      public const string objtypeConfigData = "cad00045-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Конфигурации ролей"</summary>
      public const string objtypeRoleConfigData = "cad00690-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Прочие изделия"</summary>
      public const string objtypeOtherProducts = "cad0038d-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Физическая величина"</summary>
      public const string objtypePhysicalValue = "cad00048-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Заголовок объекта"</summary>
      public const string attributeCAPTION = "cad00047-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Контекст состава"</summary>
      public const string attributeCompositionContext = "cad00651-306c-11d8-b4e9-00304f19f545";
      /// <summary>Объект "Группа ВСЕ_ПОЛЬЗОВАТЕЛИ"</summary>
      public const string objectAllUsersGroup = "cad00017-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Файловые шкафы"</summary>
      public const string objtypeStorage = "cad00014-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Схемы поиска объектов"</summary>
      public const string objtypeSearchSchemes = "cad00129-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Общие схемы поиска объектов"</summary>
      public const string objtypeOwnSearchSchemes = "cad0012a-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Персональные схемы поиска объектов"</summary>
      public const string objtypePersonalSearchSchemes = "cad0012b-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Направление поиска"</summary>
      public const string attributeSearchDirection = "cad00131-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип связи "Простая связь между объектами"</summary>
      public const string reltypeSimple = "cad00022-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип связи "Документация на изделие"</summary>
      public const string reltypeDocumentation = "cad00154-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип связи "Состав документации"</summary>
      public const string reltypeDocsComposition = "cad0057c-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип связи "Состав изделий" (ранее "Проектная связь")</summary>
      public const string reltypeSP = "cad00023-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип связи "Комплект, поставляемый отдельно"</summary>
      public const string reltypeAddPackage = "cadd99d9-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип связи "Состав строительных объектов"</summary>
      public const string reltypeBuildingComposition = "cad008d6-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип связи "Изменяется по извещению"</summary>
      public const string reltypeECO = "cad0036b-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Извещение об изменении"</summary>
      public const string objtypeECO_II = "cad00349-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Срок окончания изменения"</summary>
      public const string attributeECO_DateDue = "cadd9562-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Метод сжатия данных"</summary>
      public const string attributeArcMethod = "cad00026-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Размер буфера данных"</summary>
      public const string attributeBufferSize = "cad00027-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Имя таблицы файлового шкафа"</summary>
      public const string attributeStorageTableName = "cad00028-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Тип файлового шкафа"</summary>
      public const string attributeStorageType = "cad00000-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Строка подключения в базе данных"</summary>
      public const string attributeConnectString = "cad00015-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Системная база данных"</summary>
      public const string attributeSystemConnectString = "cadd98bf-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Расчетные формулы классификатора"</summary>
      public const string attributeCalculateFormula = "cad001d7-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Aтрибут "Типы объектов для поиска при вычислении значений атрибутов"
      /// </summary>
      public const string attributeObjTypesForCalcFormula = "cad014c6-306c-11d8-b4e9-00304f19f545";
      /// <summary>Aтрибут "Типы классифицируемых объектов"</summary>
      public const string attributeEnabledClassifyTypes = "cadd9c3f-306c-11d8-b4e9-00304f19f545";
      /// <summary>Aтрибут "Классификация создаваемых объектов"</summary>
      public const string attributeClassifiedObjects = "cad001d9-306c-11d8-b4e9-00304f19f545";
      /// <summary>Объект Системный администратор</summary>
      public const string objectSYSDBA = "cad00016-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Техпроцесс базовый"</summary>
      public const string objtypeTechProcessBase = "cad00185-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов Пользователи</summary>
      public const string objtypeUsers = "cad00002-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов Группы пользователей</summary>
      public const string objtypeGroups = "cad00003-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов Сборочная Единица</summary>
      public const string objtypeAssemblyUnit = "cad00132-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов Комплект</summary>
      public const string objtypePackage = "cad0025f-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов Комплекс</summary>
      public const string objtypeComplex = "cad0025e-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов Спецификация</summary>
      public const string objtypeSpecification = "cad00133-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов Раздел спецификации</summary>
      public const string objtypeSpecificationSection = "cad00254-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов Документ</summary>
      public const string objtypeDocument = "cad00070-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов Конструкторский документ</summary>
      public const string objtypeConstructorDocument = "cad0057f-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта "Ведомости"</summary>
      public const string objtypeRolls = "cad00196-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов Документ Интермех</summary>
      public const string objtypeImDocument = "cad00136-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов Шаблон Документа</summary>
      public const string objtypeImDocumentTemplate = "cad00134-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов Шаблоны конструкторских документов</summary>
      public const string objtypeConstructorDocumentsTemplate = "cad00269-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов Извещения</summary>
      public const string objtypeECO = "cad00348-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов Бумажные документы</summary>
      public const string objtypePaperDocument = "cad0090f-306c-11d8-b4e9-00304f19f545";
      /// <summary>Стандартный обработчик объектов DBObject</summary>
      public const string DBObjectClass = "cad0001e-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Глобальные идентификаторы типов объектов"</summary>
      public const string attributeObjectTypeGuids = "cad00149-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Глобальные идентификаторы типов связей"</summary>
      public const string attributeRelationTypeGuids = "cad0014a-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Глобальные идентификаторы типов атрибутов"</summary>
      public const string attributeAttributeTypeGuids = "cadd9c03-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Глобальный идентификатор типа связи"</summary>
      public const string attributeRelationTypeGuid = "cad001a9-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Условия фильтрации объектов"</summary>
      public const string attributeFilterSelection = "cad00621-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Раздел спецификации"</summary>
      public const string attributeSpecificationSection = "cad00266-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Сортировка"</summary>
      public const string attributeSortIndex = "cad00202-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Допустимые разделы"</summary>
      public const string attributeAllowableSections = "cad0026a-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут с именем пользователя для входа в систему</summary>
      public const string attributeLoginName = "cad00018-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут Пароль</summary>
      public const string attributePassword = "cad00019-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут Внешний пользователь</summary>
      public const string attributeExternalUser = "cad002df-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут Код ОКП</summary>
      public const string attributeCodeOKP = "cad0038a-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Атрибут с именем пользователя для отображения "Выводимое имя"
      /// </summary>
      public const string attributeUserName = "cad0001d-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Атрибут "Обязательность вычисления атрибутов включаемых объектов"
      /// </summary>
      public const string attributeObligatoryCalculated = "cad001d8-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта "Единица измерения"</summary>
      public const string objecttypeMeasure = "cad0000b-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта "Поле внешней базы данных"</summary>
      public const string objecttypeExternalField = "cad0000a-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта "Роль"</summary>
      public const string objtypeRoles = "cad00007-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут Наименование</summary>
      public const string attributeName = "cad00020-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут Краткое наименование</summary>
      public const string attributeShortName = "cad00005-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут Обозначение</summary>
      public const string attributeDesignation = "cad0001f-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут Формат</summary>
      public const string attributeFormat = "cad00255-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут Количество</summary>
      public const string attributeCount = "cad00267-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут Количество на регулировку</summary>
      public const string attributeCountForAdjustment = "cad007a6-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут Нормализованное наименование</summary>
      public const string attributeNormName = "cad00798-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут Схема сортировки</summary>
      public const string attributeSortScheme = "cad0026c-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут Позиция</summary>
      public const string attributePosition = "cad00270-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут Сортировка AVS</summary>
      public const string attributeSortAVS = "cad00272-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут Схема пропуска позиций</summary>
      public const string attributeSkipLinesScheme = "cad00273-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут Схема нумерации позиций</summary>
      public const string attributeNumberingScheme = "cad0026e-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут Настройки граф документа</summary>
      public const string attributeOutputMappingScheme = "cadd9aa0-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут Настройка автозамены в заголовке группы</summary>
      public const string attributeDynamicHeaderKeywordReplacementScheme = "cadd9ac0-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Шаг жизненного цикла для проверки подписей"</summary>
      public const string attributeLCStepForSigns = "cad0014c-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Атрибут "Уровень продвижения объекта для проверки подписей"
      /// </summary>
      public const string attributeLCLevelForSigns = "cad0015b-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Атрибут "Шаг жизненного цикла и тип объекта для проверки подписей"
      /// </summary>
      public const string attributeLCStepObjectTypeForSigns = "cad00922-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут Активный файловый шкаф</summary>
      public const string attributeActiveStorage = "cad00032-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут Примечание</summary>
      public const string attributeNote = "cad00021-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта Изделия</summary>
      public const string objtypeProduct = "cad00268-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут Проверил</summary>
      public const string attributeCheckedBy = "cad00282-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта Детали</summary>
      public const string objtypePart = "cad00250-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта Детали БЧ</summary>
      public const string objtypePartWithoutDrawing = "cad00861-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта Библиотека формул и спецсимволов</summary>
      public const string objtypeFormulaLib = "cad00251-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта Материалы</summary>
      public const string objtypeMaterial = "cad00170-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта Стандартные изделия</summary>
      public const string objtypeStandardProduct = "cad00252-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта Классификаторы</summary>
      public const string objtypeClassifier = "cad00157-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта Общий классификатор</summary>
      public const string objtypeClassifierCommon = "cad0014e-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта Персональный классификатор</summary>
      public const string objtypeClassifierPerson = "cad0014f-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта Папка классификатора</summary>
      public const string objtypeClassifierFolder = "cad00150-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта Выборки и классификаторы</summary>
      public const string objtypeSelectionsAndClassifiers = "cad00119-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта Выборки</summary>
      public const string objtypeSelections = "cad00156-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта Общая выборка</summary>
      public const string objtypeSelectionCommon = "cad00122-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта Персональная выборка</summary>
      public const string objtypeSelectionPerson = "cad00123-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут Ручная выборка</summary>
      public const string attributeHandsSelection = "cad00155-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// GUID типа объектов "Задачи синхронизации с IPS WebPortal"
      /// </summary>
      public const string objtypeTasks = "cad0149e-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Ini файл настроек"</summary>
      public const string attributeOldAVSSettingsIniFiles = "cad002a1-306c-11d8-b4e9-00304f19f545";
      /// <summary>Объект "Настройки старых спецификаций"</summary>
      public const string objectOldAVSSettingsSpecifications = "cad002a2-306c-11d8-b4e9-00304f19f545";
      /// <summary>атрибут "Типы файлов настроек старой спецификации"</summary>
      public const string attributeOldAVSSettingsFileTypes = "cad002a3-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// атрибут "Тип файла настроек старых спецификаций по-умолчанию"
      /// </summary>
      public const string attributeOldAVSSettingsDefaultIniFile = "cad002a4-306c-11d8-b4e9-00304f19f545";
      /// <summary>Объект "Настройки старых ведомостей"</summary>
      public const string objectOldAVSSettingsVedomosti = "cad002a6-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Атрибут "Отображать эскизы страниц" для классификаторов
      /// </summary>
      public const string attributeShowPageThumbs = "cadd99b5-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут Принадлежность выборки</summary>
      public const string attributeSelectionType = "cad00158-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут Принадлежность классификатора</summary>
      public const string attributeClassifierType = "cad00e8f-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип связи "Находится на рабочем столе"</summary>
      public const string relTypeWorkspace = "cad0005e-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта "Папка рабочего стола"</summary>
      public const string objtypeWorkspaceFolder = "cad0005d-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Глобальный идентификатор типа атрибута"</summary>
      public const string attributeAttributeTypeGuid = "cad001d0-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Идентифицирующий атрибут"</summary>
      public const string attributeAttributeIdentifier = "cad014ab-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Отображаемые колонки"</summary>
      public const string attributeColumnScheme = "cad00620-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Роли для схемы поиска"</summary>
      public const string attributeSearchSchemeRoles = "cad00d18-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Глобальный идентификатор типа объекта"</summary>
      public const string attributeObjectTypeGuid = "cad001a0-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Стартовый тип объектов в Навигаторе"</summary>
      public const string navigatorStartObjectTypeGuid = "cadd9c3a-306c-11d8-b4e9-00304f19f545";
      /// <summary>Объект "Общий шаблон спецификаций"</summary>
      public const string objectCommonSpefificationTemplate = "cad0026f-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Бесчертежная деталь"</summary>
      public const string attributePartWithoutDrawing = "cad00624-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Назначение конструкторского документа"</summary>
      public const string attributeDocumentPurpose = "cad00625-306c-11d8-b4e9-00304f19f545";
      /// <summary>Объект "Раздел спецификации 'Документация'"</summary>
      public const string objectSectionDocumentation = "cad00256-306c-11d8-b4e9-00304f19f545";
      /// <summary>Объект "Раздел спецификации 'Комплексы'"</summary>
      public const string objectSectionComplex = "cad00257-306c-11d8-b4e9-00304f19f545";
      /// <summary>Объект "Раздел спецификации 'Сборочные единицы'"</summary>
      public const string objectSectionAssemblyUnits = "cad00258-306c-11d8-b4e9-00304f19f545";
      /// <summary>Объект "Раздел спецификации 'Детали'"</summary>
      public const string objectSectionComponents = "cad00259-306c-11d8-b4e9-00304f19f545";
      /// <summary>Объект "Раздел спецификации 'Стандартные изделия'"</summary>
      public const string objectSectionStandartArticles = "cad0025a-306c-11d8-b4e9-00304f19f545";
      /// <summary>Объект "Раздел спецификации 'Прочие изделия'"</summary>
      public const string objectSectionOtherArticles = "cad0025b-306c-11d8-b4e9-00304f19f545";
      /// <summary>Объект "Раздел спецификации 'Материалы'"</summary>
      public const string objectMaterialsSectionMaterials = "cad0025c-306c-11d8-b4e9-00304f19f545";
      /// <summary>Объект "Раздел спецификации 'Комплекты'"</summary>
      public const string objectSectionComplects = "cad0025d-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Объект "Раздел спецификации 'Комплектовочные единицы'"
      /// </summary>
      public const string objectSectionComplectUnits = "cad00271-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта "Контейнер атрибутов"</summary>
      public const string objtypeContainer = "cad0013b-306c-11d8-b4e9-00304f19f545";
      /// <summary>Чертежи деталей</summary>
      public const string objectPartsDrawings = "cad00261-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Идентификатор группового изделия"</summary>
      public const string attributeGroupInstance = "cad001f9-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Код исполнения"</summary>
      public const string attributeVersionCode = "cad001fa-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Идентификатор изделия"</summary>
      public const string attributeArticleID = "cad00622-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Идентификатор документа"</summary>
      public const string attributeDocumentID = "cad00623-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Масса"</summary>
      public const string attributeWeight = "cad00275-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Удельная масса"</summary>
      public const string attributeUnitWeight = "cad00276-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Размеры"</summary>
      public const string attributeSize = "cad00277-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Зона"</summary>
      public const string attributeZone = "cad0027a-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Разработал"</summary>
      public const string attributeAuthor = "cad00280-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Подразделение"</summary>
      public const string attributeSubdivision = "cad00281-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Литера"</summary>
      public const string attributeLitera = "cad0038b-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Номер раздела спецификации"</summary>
      public const string attributeSectionNum = "cad00279-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Номер части спецификации"</summary>
      public const string attributePartNum = "cad00286-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Часть спецификации"</summary>
      public const string attributePartName = "cad0027e-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Раздел СП"</summary>
      public const string attributeInsertToSection = "cad00210-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Кодовая позиция"</summary>
      public const string attributeCodePosition = "cad0027c-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Нормоконтролёр"</summary>
      public const string attributeNormoControlledBy = "cad00283-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Утвердил"</summary>
      public const string attributeConfirmBy = "cad00284-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Атрибут хранения доп настроек типов объектов, наследованных от типа Документ
      /// </summary>
      public const string docObjTypeSettings = "cad00626-306c-11d8-b4e9-00304f19f545";
      /// <summary>Объект "Стандартный календарь"</summary>
      public const string objectStandardCalendar = "cad01582-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Атрибут для хранения уникального идентификатора входимости для CAD системы
      /// </summary>
      public const string attributeCADInteranceIdentify = "cad0027b-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта "Технологическая сборочная единица"</summary>
      public const string objecttypeProcessComposition = "cad00650-306c-11d8-b4e9-00304f19f545";
      /// <summary>Выборка "Мои объекты"</summary>
      public const string selectionMyObjects = "cad0079c-306c-11d8-b4e9-00304f19f545";
      /// <summary>Выборка "Объекты за последний день"</summary>
      public const string selectionLastDayObjects = "cad00799-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта "Календари"</summary>
      public const string objecttypeCalendars = "cad00d87-306c-11d8-b4e9-00304f19f545";
      /// <summary>Выборка "Объекты за последние семь дней"</summary>
      public const string selectionLast7DaysObjects = "cad0079a-306c-11d8-b4e9-00304f19f545";
      /// <summary>Выборка "Объекты за последние тридцать дней"</summary>
      public const string selectionLast30DaysObjects = "cad0079b-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта "Архив"</summary>
      public const string objtypeArchives = "cad0011e-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Сценарии"</summary>
      public const string objtypeScripts = "cad0036a-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Сценарии планировщика задач"</summary>
      public const string objtypeScheduledScripts = "cadd94cd-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Сценарии автоподбора"</summary>
      public const string objtypeAutoSelectionScript = "cadd98d5-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Тип объектов "Сценарии для кнопок форм редактирования"
      /// </summary>
      public const string objtypeScriptsForButtons = "cadd9962-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Текст сценария" (объекта Сценарий)</summary>
      public const string attributeScriptText = "cad00366-306c-11d8-b4e9-00304f19f545";
      public const string objtypeOrganizerTask = "cad015bc-306c-11d8-b4e9-00304f19f545";
      public const string reltypeOrganizerTask = "cadd938e-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Привязанные выборки"</summary>
      public const string attributeAttachedSelection = "cadd920c-306c-11d8-b4e9-00304f19f545";
      public const string objtypeTableReportCommon = "cad00289-306c-11d8-b4e9-00304f19f545";
      public const string objtypeTableReportPersonal = "cad0028a-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Организационные единицы"</summary>
      public const string objtypeOrganizationUnits = "cadd9235-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Электронная почта"</summary>
      public const string attributeEmailAddress = "cad002de-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Домашний адрес"</summary>
      public const string attributeHomeAddress = "cad002dc-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Телефон"</summary>
      public const string attributePhone = "cad002da-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Почтовый адрес"</summary>
      public const string attributePostalAddress = "cad015dd-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Домашний телефон"</summary>
      public const string attributeHomePhone = "cad002dd-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Мобильный телефон"</summary>
      public const string attributeMobilePhone = "cad015df-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Комната"</summary>
      public const string attributeOffice = "cad002db-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта "Схема поиска визуализатора"</summary>
      public const string objtypeVisScheme = "cadd9aa6-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объекта "Стиль визуализатора"</summary>
      public const string objtypeVisStyle = "cadd9aa7-306c-11d8-b4e9-00304f19f545";
      public const string attributeF_CREATOR_ID = "cadd96b7-306c-11d8-b4e9-00304f19f545";
      public const string attributeF_REL_CREATOR = "cadd96b8-306c-11d8-b4e9-00304f19f545";
      public const string attributeF_ACCESS = "cadd959f-306c-11d8-b4e9-00304f19f545";
      public const string attributeF_OBJECT_ID = "cad00029-306c-11d8-b4e9-00304f19f545";
      public const string attributeF_ID = "cad0002a-306c-11d8-b4e9-00304f19f545";
      public const string attributeF_LC_STEP = "cad0002b-306c-11d8-b4e9-00304f19f545";
      public const string attributeF_VERSION_ID = "cad0002c-306c-11d8-b4e9-00304f19f545";
      public const string attributeF_CHKOUT_BY = "cad0002d-306c-11d8-b4e9-00304f19f545";
      public const string attributeF_OBJECT_TYPE = "cad0002e-306c-11d8-b4e9-00304f19f545";
      public const string attributeF_OWNER_ID = "cad0002f-306c-11d8-b4e9-00304f19f545";
      public const string attributeF_LEVEL_ID = "cad00030-306c-11d8-b4e9-00304f19f545";
      public const string attributeF_MODIFY_DATE = "cad00031-306c-11d8-b4e9-00304f19f545";
      public const string attributeF_PRJLINK_ID = "cad00033-306c-11d8-b4e9-00304f19f545";
      public const string attributeF_PROJ_ID = "cad00034-306c-11d8-b4e9-00304f19f545";
      public const string attributeF_PART_ID = "cad00035-306c-11d8-b4e9-00304f19f545";
      public const string attributeF_RELATION_TYPE = "cad00036-306c-11d8-b4e9-00304f19f545";
      public const string attributeF_CREATE_DATE = "cad00037-306c-11d8-b4e9-00304f19f545";
      public const string attributeF_DELETE_DATE = "cad00038-306c-11d8-b4e9-00304f19f545";
      public const string attributeF_EVENT_ID = "cad00039-306c-11d8-b4e9-00304f19f545";
      public const string attributeF_CATEGORY_TYPE = "cad0003a-306c-11d8-b4e9-00304f19f545";
      public const string attributeF_CATEGORY_ID = "cad0003b-306c-11d8-b4e9-00304f19f545";
      public const string attributeF_RELATION_ID = "cad0003c-306c-11d8-b4e9-00304f19f545";
      public const string attributeF_OBJECT_NAME = "cad0003d-306c-11d8-b4e9-00304f19f545";
      public const string attributeF_USER_ID = "cad0003e-306c-11d8-b4e9-00304f19f545";
      public const string attributeF_COMPUTER_NAME = "cad0003f-306c-11d8-b4e9-00304f19f545";
      public const string attributeF_NOTE = "cad00040-306c-11d8-b4e9-00304f19f545";
      public const string attributeF_EVENT_TYPE = "cad00041-306c-11d8-b4e9-00304f19f545";
      public const string attributeF_BEGIN_DATE = "cad00042-306c-11d8-b4e9-00304f19f545";
      public const string attributeF_END_DATE = "cad00043-306c-11d8-b4e9-00304f19f545";
      public const string attributeF_AUDIT_TYPE = "cad00044-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Список Guid обязательных (системных, id меньше 0) атрибутов, являющихся по своей сути атрибутами типа Ссылка на объект
      /// </summary>
      public static readonly List<Guid> ObligatoryAttributesAsObjectLinks = new List<Guid>((IEnumerable<Guid>) new Guid[6]
      {
        new Guid("cad0002f-306c-11d8-b4e9-00304f19f545"),
        new Guid("cad0003e-306c-11d8-b4e9-00304f19f545"),
        new Guid("cad0002d-306c-11d8-b4e9-00304f19f545"),
        new Guid("cad0003e-306c-11d8-b4e9-00304f19f545"),
        new Guid("cadd96b7-306c-11d8-b4e9-00304f19f545"),
        new Guid("cadd96b8-306c-11d8-b4e9-00304f19f545")
      });
      /// <summary>Тип объекта "Технологические объекты"</summary>
      public const string objtypeTechObjects = "cad00163-306c-11d8-b4e9-00304f19f545";
      /// <summary>  Атрибут "Номер изменения"  </summary>
      public const string attributeChangeNo = "cad00770-306c-11d8-b4e9-00304f19f545";
      /// <summary>  Атрибут "Причина выпуска извещения"  </summary>
      public const string attributeReasonCode = "cad0077d-306c-11d8-b4e9-00304f19f545";
      /// <summary> Атрибут "Срок действия" </summary>
      public const string attributeEndDate = "cad0079e-306c-11d8-b4e9-00304f19f545";
      /// <summary> Атрибут "Дата выпуска" </summary>
      public const string attributeDateOfRelease = "cad0079f-306c-11d8-b4e9-00304f19f545";
      /// <summary> Атрибут "Срок изменения" </summary>
      public const string attributeTermOfChange = "cad007a0-306c-11d8-b4e9-00304f19f545";
      /// <summary> Атрибут "Номера измененных листов"  </summary>
      public const string attribute_LRI_NList1 = "cad00771-306c-11d8-b4e9-00304f19f545";
      /// <summary> Атрибут "Номера замененных листов" </summary>
      public const string attribute_LRI_NList2 = "cad00772-306c-11d8-b4e9-00304f19f545";
      /// <summary> Атрибут "Номера новых листов" </summary>
      public const string attribute_LRI_NList3 = "cad00773-306c-11d8-b4e9-00304f19f545";
      /// <summary> Атрибут "Номера аннулированных листов" </summary>
      public const string attribute_LRI_NList4 = "cad00774-306c-11d8-b4e9-00304f19f545";
      /// <summary> Атрибут "Всего листов в документе" </summary>
      public const string attribute_LRI_NList5 = "cad00775-306c-11d8-b4e9-00304f19f545";
      /// <summary> Атрибут "Входящий № сопроводительного докум. и дата" </summary>
      public const string attribute_LRI_SoprovDoc = "cad00776-306c-11d8-b4e9-00304f19f545";
      /// <summary> Атрибут "№ документа" </summary>
      public const string attribute_LRI_DocNo = "cad00777-306c-11d8-b4e9-00304f19f545";
      /// <summary> Атрибут "Дата внесения изменения" </summary>
      public const string attribute_LRI_Date = "cad00778-306c-11d8-b4e9-00304f19f545";
      /// <summary> Атрибут "Фамилия лица, ответственного за правильность внесения изменения" </summary>
      public const string attribute_LRI_Podpis = "cad00779-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Веса типов документов" (используется в AVS)</summary>
      public const string attributeObjectTypesWeights = "cad00292-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Атрибут "Наименования ini-файлов старого AVS" (используется в AVS)
      /// </summary>
      public const string attributeOldAvsIniFileNames = "cad002a8-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Атрибут "Видимость объекта" (используется для хранения настроек видимости объектов)
      /// </summary>
      public const string attributeObjectVisibility = "cad0062f-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Атрибут "Настройки графы Примечание" (используется в AVS)
      /// </summary>
      public const string attributeNoteFieldSettings = "cad00294-306c-11d8-b4e9-00304f19f545";
      /// <summary> Атрибут "Путь хранения рабочей копии" (перекачивается для документов) </summary>
      public const string attributeDocWorkPath = "cad007a1-306c-11d8-b4e9-00304f19f545";
      /// <summary> Атрибут "Место хранения бумажного документа" (перекачивается для бумажных документов) </summary>
      public const string attributePaperDocPath = "cad007a2-306c-11d8-b4e9-00304f19f545";
      /// <summary> Выборка "Продукция" </summary>
      public const string objectProductSelection = "cad00796-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Ссылка на изображение".</summary>
      public const string attributeImagesFromLibrary = "cad014b6-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Условие"</summary>
      /// <remarks>Используется эспертной системой</remarks>
      public const string attributeCondition = "cad00064-306c-11d8-b4e9-00304f19f545";
      /// <summary>Аттрибут "Превью" (блоб с картинкой)</summary>
      public static readonly Guid attributePreview = new Guid("cadd970d-306c-11d8-b4e9-00304f19f545");
      public const string attributeStartStr = "cad002cb-306c-11d8-b4e9-00304f19f545";
      public static readonly Guid attributeStart = new Guid("cad002cb-306c-11d8-b4e9-00304f19f545");
      public const string attributeFinishStr = "cad002cc-306c-11d8-b4e9-00304f19f545";
      public static readonly Guid attributeFinish = new Guid("cad002cc-306c-11d8-b4e9-00304f19f545");
      public const string attributeRecipientStr = "cad002ca-306c-11d8-b4e9-00304f19f545";
      public static readonly Guid attributeRecipient = new Guid("cad002ca-306c-11d8-b4e9-00304f19f545");
      public const string attributeDueDateStr = "cad0132d-306c-11d8-b4e9-00304f19f545";
      public static readonly Guid attributeDueDate = new Guid("cad0132d-306c-11d8-b4e9-00304f19f545");
      /// <summary>Атрибут "Календарь"</summary>
      public const string attributeCalendarStr = "cad00ea5-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Календарь"</summary>
      public static readonly Guid attributeCalendar = new Guid("cad00ea5-306c-11d8-b4e9-00304f19f545");
      /// <summary>Дата начала работы пользователя</summary>
      public const string UserHireDateGuidStr = "cadd9bf1-306c-11d8-b4e9-00304f19f545";
      /// <summary>Дата начала работы пользователя</summary>
      public static readonly Guid UserHireDateGuid = new Guid("cadd9bf1-306c-11d8-b4e9-00304f19f545");
      /// <summary>Персональный календарь</summary>
      public const string UserCalendarGuidStr = "cadd9b9f-306c-11d8-b4e9-00304f19f545";
      /// <summary>Персональный календарь</summary>
      public static readonly Guid UserCalendarGuid = new Guid("cadd9b9f-306c-11d8-b4e9-00304f19f545");
      /// <summary>Дата увольнения пользователя</summary>
      public const string UserFireDateGuidStr = "cadd9bf2-306c-11d8-b4e9-00304f19f545";
      /// <summary>Дата увольнения пользователя</summary>
      public static readonly Guid UserFireDateGuid = new Guid("cadd9bf2-306c-11d8-b4e9-00304f19f545");
      /// <summary>Тип объектов "Задача"</summary>
      public const string objtypeTaskActivity = "cad002b5-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Утверждение"</summary>
      public const string objtypeApproveActivity = "cad002b4-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Почтовое сообщение"</summary>
      public const string objtypeMail = "cad002bd-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут для хранения категории подузла органайзера.</summary>
      public const string attributeOrganizerChildNodeCategory = "cad015d1-306c-11d8-b4e9-00304f19f545";
      /// <summary>Текст задачи органайзера.</summary>
      public const string attributeOrganizerTaskText = "cad015d2-306c-11d8-b4e9-00304f19f545";
      /// <summary>Повторение задачи органайзера.</summary>
      public const string attributeOrganizerTaskRepetition = "cad015d3-306c-11d8-b4e9-00304f19f545";
      /// <summary>Дата напоминания о задаче органайзера.</summary>
      public const string attributeOrganizerTaskDateReminder = "cad015d4-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Атрибут документа, хранящий ссылку на объект, на который он сгенерирован
      /// </summary>
      public const string attributeOwnerLink = "cad001a6-306c-11d8-b4e9-00304f19f545";
      /// <summary>Напоминание о задаче органайзера.</summary>
      public const string attributeOrganizerTaskReminder = "cad015d5-306c-11d8-b4e9-00304f19f545";
      /// <summary>Категория задачи органайзера.</summary>
      public const string attributeOrganizerTaskCategory = "cad015d6-306c-11d8-b4e9-00304f19f545";
      /// <summary>Приоритет задачи органайзера.</summary>
      public const string attributeOrganizerTaskRelevance = "cad015d7-306c-11d8-b4e9-00304f19f545";
      /// <summary>Состояние задачи органайзера.</summary>
      public const string attributeOrganizerTaskState = "cad015d8-306c-11d8-b4e9-00304f19f545";
      /// <summary>Атрибут "Контрольная сумма"</summary>
      public const string attributeCheckSum = "cad014af-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Страны"</summary>
      public const string objtypeCountries = "cadd9239-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Регионы"</summary>
      public const string objtypeRegions = "cadd9238-306c-11d8-b4e9-00304f19f545";
      /// <summary>Тип объектов "Города"</summary>
      public const string objtypeCities = "cadd9237-306c-11d8-b4e9-00304f19f545";
      /// <summary>Группа пользователей  Новые пользователи</summary>
      public const string ldapNewUsersGroup = "cadd93ee-306c-11d8-b4e9-00304f19f545";
      /// <summary>Группа пользователей  Удаленные пользователи</summary>
      public const string ldapDeletedUsersGroup = "cadd93f0-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Группа пользователей Синхронизация со службой каталогов
      /// </summary>
      public const string ldapSyncUsersGroup = "cadd93ec-306c-11d8-b4e9-00304f19f545";

      /// <summary>Проверяет, системный ли это GUID</summary>
      public static bool IsSystemGUID(string aGUID)
      {
        if (aGUID == null)
          return false;
        if (aGUID.IndexOf("cad", StringComparison.OrdinalIgnoreCase) == 0)
          return aGUID.IndexOf("-306c-11d8-b4e9-00304f19f545", StringComparison.OrdinalIgnoreCase) == 8 || aGUID.IndexOf("306c11d8b4e900304f19f545", StringComparison.OrdinalIgnoreCase) == 8;
        string lower = aGUID.ToLower();
        return lower == "424e4095-d402-44f1-b3c8-379ac6e60e8c" || lower == "cae0d224-f228-401f-bff4-8395e19c05a8";
      }

      /// <summary>Проверяет, системный ли это GUID</summary>
      public static bool IsSystemGUID(Guid aGUID) => SystemGUIDs.IsSystemGUID(aGUID.ToString("D"));

      /// <summary>
      /// Проверяет является ли данный GUID импортированным в эталонную БД
      /// </summary>
      public static bool IsImportedGUID(string aGUID)
      {
        return aGUID != null && aGUID.IndexOf("cae0", StringComparison.OrdinalIgnoreCase) == 0;
      }

      /// <summary>
      /// Проверяет является ли данный GUID пользовательским типом или объектом
      /// </summary>
      public static bool IsUsersGUID(string aGUID)
      {
        return aGUID != null && aGUID.IndexOf("caf0", StringComparison.OrdinalIgnoreCase) == 0;
      }
    }
}
