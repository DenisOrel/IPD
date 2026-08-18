
// Type: Intermech.ActionType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech
{
    /// <summary>Тип действия</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_200")]
    [Category("Access")]
    public enum ActionType
    {
      /// <summary>Не определено</summary>
      [CustomDescription("Attribute.AT.Unknown")] Unknown = -1, // 0xFFFFFFFF
      /// <summary>Создание</summary>
      [CustomDescription("Attribute.Interfaces_201")] Create = 0,
      /// <summary>
      /// Создание элемента коллекции или типа. Это действие применимо к любым коллекциям, а также к метаданным, которые умеют создавать подчиненные элементы своего типа.
      /// Пример для метаданного 'тип объекта': Create - это создание нового типа объекта, а CreateChildItem - это создание объекта данного типа.
      /// </summary>
      [CustomDescription("Attribute.Interfaces_202")] CreateChildItem = 1,
      /// <summary>Редактирование</summary>
      [CustomDescription("Attribute.Interfaces_203")] Edit = 2,
      /// <summary>Изменение свойств</summary>
      [CustomDescription("Attribute.Interfaces_204")] EditProperties = 3,
      /// <summary>Удаление</summary>
      [CustomDescription("Attribute.Interfaces_205")] Delete = 4,
      /// <summary>Перемещение</summary>
      [CustomDescription("Attribute.Interfaces_206")] Remove = 5,
      /// <summary>Чтение</summary>
      [CustomDescription("Attribute.Interfaces_207")] Read = 6,
      /// <summary>Запись</summary>
      [CustomDescription("Attribute.Interfaces_208")] Write = 7,
      /// <summary>Просмотр</summary>
      [CustomDescription("Attribute.Interfaces_209")] View = 8,
      /// <summary>Открытие</summary>
      [CustomDescription("Attribute.Interfaces_210")] Open = 9,
      /// <summary>Выполнение</summary>
      [CustomDescription("Attribute.Interfaces_211")] Execute = 10, // 0x0000000A
      /// <summary>Создание связи</summary>
      [CustomDescription("Attribute.Interfaces_212")] AddLink = 11, // 0x0000000B
      /// <summary>Изменение связи</summary>
      [CustomDescription("Attribute.Interfaces_213")] EditLink = 12, // 0x0000000C
      /// <summary>Удаление связи</summary>
      [CustomDescription("Attribute.Interfaces_214")] DeleteLink = 13, // 0x0000000D
      /// <summary>Получение списка</summary>
      [CustomDescription("Attribute.Interfaces_215")] List = 14, // 0x0000000E
      /// <summary>Вычисление</summary>
      [CustomDescription("Attribute.Interfaces_216")] Compute = 15, // 0x0000000F
      /// <summary>Печать</summary>
      [CustomDescription("Attribute.Interfaces_217")] Print = 16, // 0x00000010
      /// <summary>Копирование</summary>
      [CustomDescription("Attribute.Interfaces_218")] Copy = 17, // 0x00000011
      /// <summary>Получение прав доступа</summary>
      [CustomDescription("Attribute.Interfaces_219")] GetAccess = 18, // 0x00000012
      /// <summary>Изменение прав доступа</summary>
      [CustomDescription("Attribute.Interfaces_220")] SetAccess = 19, // 0x00000013
      /// <summary>Вход в систему</summary>
      [CustomDescription("Attribute.Interfaces_221")] Login = 20, // 0x00000014
      /// <summary>Получение связей</summary>
      [CustomDescription("Attribute.Interfaces_222")] GetLinks = 21, // 0x00000015
      /// <summary>Экспорт</summary>
      [CustomDescription("Attribute.Interfaces_223")] Export = 22, // 0x00000016
      /// <summary>Импорт</summary>
      [CustomDescription("Attribute.Interfaces_224")] Import = 23, // 0x00000017
      /// <summary>Отправка</summary>
      [CustomDescription("Attribute.Interfaces_225")] Send = 24, // 0x00000018
      /// <summary>Изменение шага ЖЦ</summary>
      [CustomDescription("Attribute.Interfaces_226")] NextLCStep = 25, // 0x00000019
      /// <summary>Уничтожение</summary>
      [CustomDescription("Attribute.Interfaces_227")] Purge = 26, // 0x0000001A
      /// <summary>Очистка</summary>
      [CustomDescription("Attribute.Interfaces_228")] Clear = 27, // 0x0000001B
      /// <summary>Отмена</summary>
      [CustomDescription("Attribute.Interfaces_229")] Cancel = 28, // 0x0000001C
      /// <summary>Взятие на изменение</summary>
      [CustomDescription("Attribute.Interfaces_230")] CheckOut = 29, // 0x0000001D
      /// <summary>Завершение изменений</summary>
      [CustomDescription("Attribute.Interfaces_231")] CheckIn = 30, // 0x0000001E
      /// <summary>Изменение владельца</summary>
      [CustomDescription("Attribute.Interfaces_232")] TakeOwnership = 31, // 0x0000001F
      /// <summary>Сохранение изменений</summary>
      [CustomDescription("Attribute.Interfaces_233")] Save = 32, // 0x00000020
      /// <summary>Загрузка данных</summary>
      [CustomDescription("Attribute.Interfaces_234")] Load = 33, // 0x00000021
      /// <summary>Действие не определено</summary>
      [CustomDescription("Attribute.Interfaces_235")] Any = 34, // 0x00000022
      /// <summary>Копирование документа на диск</summary>
      [CustomDescription("Attribute.SaveToDisk")] SaveToDisk = 35, // 0x00000023
      /// <summary>Регистрация документа в ОТД</summary>
      [CustomDescription("Attribute.Interfaces_562")] DocRegistry = 36, // 0x00000024
      /// <summary>Включение в состав</summary>
      [CustomDescription("Attribute.Interfaces_440")] IncludeInComposition = 43, // 0x0000002B
      /// <summary>Исключение из состава</summary>
      [CustomDescription("Attribute.Interfaces_441")] ExcludeFromComposition = 44, // 0x0000002C
      /// <summary>Изменение базовой версии</summary>
      [CustomDescription("ChangeBaseVersion")] ChangeBaseVersion = 45, // 0x0000002D
      /// <summary>Восстановление</summary>
      [CustomDescription("Restore")] Restore = 46, // 0x0000002E
      /// <summary>Просмотр истории значений атрибутов</summary>
      [CustomDescription("ShowHistory")] ShowHistory = 47, // 0x0000002F
      /// <summary>Изменение уровня доступа объекта</summary>
      [CustomDescription("ChangeAccessLevel")] ChangeAccessLevel = 48, // 0x00000030
      /// <summary>
      /// Связывание извещения с другим контекстом редактирования
      /// </summary>
      [CustomDescription("LinkECOToContext")] LinkECO_ToContext = 49, // 0x00000031
      /// <summary>
      /// Отвязывание извещения от другого контекста редактирования
      /// </summary>
      [CustomDescription("UnlinkECOFromContext")] UnlinkECO_FromContext = 50, // 0x00000032
      [CustomDescription("ShowNonApplicabilityImbaseRecords")] ShowNonApplicabilityImbaseRecords = 51, // 0x00000033
      [CustomDescription("ShowNonVisibleColumnImbaseRecords")] ShowNonVisibleColumnImbaseRecords = 52, // 0x00000034
      [CustomDescription("ShowNonVisibleRowImbaseRecords")] ShowNonVisibleRowImbaseRecords = 53, // 0x00000035
      [CustomDescription("CreateFolderOrRecordInCatalog")] CreateFolderOrRecordInCatalog = 54, // 0x00000036
      [CustomDescription("CreateTableLinkInCatalog")] CreateTableLinkInCatalog = 55, // 0x00000037
      [CustomDescription("Use")] Use = 56, // 0x00000038
      [CustomDescription("ShowNonUseImbaseRecords")] ShowNonUseImbaseRecords = 57, // 0x00000039
      [CustomDescription("EditTableStructureAndProperties")] EditTableStructureAndProperties = 58, // 0x0000003A
      [CustomDescription("EditTableData")] EditTableData = 59, // 0x0000003B
      [CustomDescription("ManageCatalogIndexes")] ManageCatalogIndexes = 60, // 0x0000003C
      [CustomDescription("AddTableRows")] AddNewRows = 61, // 0x0000003D
      /// <summary>Инициировать процессы по этому шаблону</summary>
      [CustomDescription("Attribute.Interfaces_236")] wfLaunchProcess = 1001, // 0x000003E9
      /// <summary>Редактировать порожденный процесс</summary>
      [CustomDescription("Attribute.Interfaces_237")] wfEditProcess = 1002, // 0x000003EA
      /// <summary>Администрировать порожденный процесс</summary>
      [CustomDescription("Attribute.Interfaces_238")] wfAdminProcess = 1003, // 0x000003EB
      /// <summary>Синхронизация со службой каталогов</summary>
      [CustomDescription("ActionType_DirectorySyncronization")] DirectorySyncronization = 1004, // 0x000003EC
      /// <summary>Опубликован на портале</summary>
      [CustomDescription("Attribute.Interfaces_556")] PublishedOnPortal = 1010, // 0x000003F2
      /// <summary>Импортирован с портала</summary>
      [CustomDescription("Attribute.Interfaces_557")] ImportedFromPortal = 1011, // 0x000003F3
      /// <summary>Работа с аутентичными файлами</summary>
      [CustomDescription("EditAuthenticalFiles")] EditAuthenticalFiles = 1012, // 0x000003F4
      /// <summary>Объединение данных</summary>
      [CustomDescription("CombineData")] CombineData = 1013, // 0x000003F5
      /// <summary>Прерывание процесса</summary>
      [CustomDescription("wfAbortProcess")] wfAbortProcess = 1014, // 0x000003F6
      [CustomDescription("ChangeECOFutureStep")] ChangeECOFutureStep = 1015, // 0x000003F7
      /// <summary>Просмотр карточки объекта</summary>
      [CustomDescription("ViewCard")] ViewCard = 1016, // 0x000003F8
      /// <summary>Выполнение административных процедур в системе</summary>
      [CustomDescription("AdminProcedure")] AdminProcedure = 1017, // 0x000003F9
      /// <summary>Настройка задач в планировщике</summary>
      [CustomDescription("AdminTaskManager")] AdminTaskManager = 1018, // 0x000003FA
    }
}
