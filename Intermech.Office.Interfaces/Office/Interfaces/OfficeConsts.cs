// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Interfaces.OfficeConsts
// Assembly: Intermech.Office.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9492297C-4143-4944-80A1-CEF9501FC1B8
// Assembly location: D:\IPS\Client\Intermech.Office.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Office.Interfaces.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Office.Interfaces;

public class OfficeConsts
{
  /// <summary>Сдвиг в днях для плановой даты, относительно текущей</summary>
  public static int PlannedDataShift = 3;
  /// <summary>Глобальный идентификатор типа объектов "Канцелярские документы"</summary>
  public static readonly Guid ObjtypeOfficeDocumentsGuid = new Guid("cadd9259-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор типа объектов "Поручения"</summary>
  public static readonly Guid ObjtypeResolutionsGuid = new Guid("cadd927f-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор типа объектов "Конфиденциальные поручения"</summary>
  public static readonly Guid ObjtypeConfidentialResolutionsGuid = new Guid("cadd9381-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор типа объектов "Реквизиты документа"</summary>
  public static readonly Guid ObjtypeDocDetailsGuid = new Guid("cadd927d-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор типа объектов "Электронные письма"</summary>
  public static readonly Guid ObjtypeEmailMessagesGuid = new Guid("cadd932d-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор типа связей "Состав канцелярского документа"</summary>
  public static readonly Guid ReltypeOfficeCompositionGuid = new Guid("cadd927c-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор типа связей "Ответ на"</summary>
  public static readonly Guid ReltypeAnswerGuid = new Guid("cadd927b-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор группы атрибутов "Атрибуты входящих канцелярских документов"</summary>
  public static readonly Guid AttrGroupIncomingOfficeParamsGuid = new Guid("cadd9247-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор группы атрибутов "Атрибуты внутренних канцелярских документов"</summary>
  public static readonly Guid AttrGroupInternalOfficeParamsGuid = new Guid("cadd9249-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор группы атрибутов "Атрибуты исходящих канцелярских документов"</summary>
  public static readonly Guid AttrGroupOutgoingOfficeParamsGuid = new Guid("cadd9248-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор группы атрибутов "Атрибуты поручений"</summary>
  public static readonly Guid AttrGroupResolutionParamsGuid = new Guid("cadd971a-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Вид канцелярского документа"</summary>
  public static readonly Guid AttrOfficeDocumentTypeGuid = new Guid("cadd928b-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Дата регистрации"</summary>
  public static readonly Guid AttrRegistrationDateGuid = new Guid("cadd924b-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Регистрационный номер"</summary>
  public static readonly Guid AttrRegNumberGuid = new Guid("cadd924f-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "В ответ на письмо"</summary>
  public static readonly Guid AttrInReplyToGuid = new Guid("cadd932e-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Счетчик регистрационных номеров"</summary>
  public static readonly Guid AttrCountersGuid = new Guid("cadd92b2-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Идентификатор письма"</summary>
  public static readonly Guid AttrMessageIdentityGuid = new Guid("cadd92d5-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Собственная канцелярия"</summary>
  public static readonly Guid AttrSelfOfficeGuid = new Guid("cadd9413-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Ссылка на подразделение"</summary>
  public static readonly Guid AttrUnitLinkGuid = new Guid("cadd9420-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Настройки подразделения"</summary>
  public static readonly Guid AttrUnitSettingsGuid = new Guid("cadd9421-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Автор"</summary>
  public static readonly Guid AttrAuthorGuid = new Guid("cadd928c-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Контролер"</summary>
  public static readonly Guid AttrControllerGuid = new Guid("cadd928f-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Плановая дата исполнения"</summary>
  public static readonly Guid AttrPlannedDateGuid = new Guid("cadd924e-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Контрольное поручение"</summary>
  public static readonly Guid AttrIsControlResolutionGuid = new Guid("cadd928e-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Идентификатор поручения"</summary>
  public static readonly Guid AttrResolutionIdentityGuid = new Guid("cadd92dd-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Исполнители поручения"</summary>
  public static readonly Guid AttrExecutorsGuid = new Guid("cadd9294-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Авторы отчетов"</summary>
  public static readonly Guid AttrReportAuthorsGuid = new Guid("cadd9296-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Отчеты исполнения"</summary>
  public static readonly Guid AttrReportsGuid = new Guid("cadd9298-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Даты отчетов исполнения"</summary>
  public static readonly Guid AttrReportDatesGuid = new Guid("cadd929a-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Адресаты"</summary>
  public static readonly Guid AttrAddresseesGuid = new Guid("cadd924a-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Получатель документа"</summary>
  public static readonly Guid AttrDocRecipientGuid = new Guid("cadd9287-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Получатели документа"</summary>
  public static readonly Guid AttrDocRecipientsGuid = new Guid("cadd9288-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Входящий регистрационный номер"</summary>
  public static readonly Guid AttrInputRegNumGuid = new Guid("cadd9258-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Входящие регистрационные номера"</summary>
  public static readonly Guid AttrInputRegNumsGuid = new Guid("cadd9289-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Дата регистрации в организации-адресате"</summary>
  public static readonly Guid AttrAddresseeRegDateGuid = new Guid("cadd9254-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Даты регистрации в организациях-адресатах"</summary>
  public static readonly Guid AttrAddresseeRegDatesGuid = new Guid("cadd9285-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Фактическая дата исполнения"</summary>
  public static readonly Guid AttrActualDateGuid = new Guid("cadd9253-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Требует ответа"</summary>
  public static readonly Guid AttrResponseRequiresGuid = new Guid("cadd9252-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Срок ответа"</summary>
  public static readonly Guid AttrResponseDateGuid = new Guid("cadd9251-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор типа связей "Адресант"</summary>
  public static readonly Guid AttrAddresserGuid = new Guid("cadd9283-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "E-mail отправителя"</summary>
  public static readonly Guid AttrEmailSenderGuid = new Guid("cadd92d6-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Кто подписал"</summary>
  public static readonly Guid AttrSignatoryGuid = new Guid("cadd924d-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Тема сообщения"</summary>
  public static readonly Guid AttrSubjectGuid = new Guid("cad002d6-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Сообщение"</summary>
  public static readonly Guid AttrMessageGuid = new Guid("cad002d2-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Внутренний регистрационный номер"</summary>
  public static readonly Guid AttrPrivateRegNumberGuid = new Guid("cadd9430-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Зарегистрирован во внутренней канцелярии"</summary>
  public static readonly Guid AttrIsPrivateRegisterGuid = new Guid("cadd969f-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Прочитано"</summary>
  public static readonly Guid AttrReadGuid = new Guid("cadd943b-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Исполнение поручения"</summary>
  public static readonly Guid AttrResolutionExecuteTypeGuid = new Guid("cadd9466-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Порядок исполнения"</summary>
  public static readonly Guid AttrExecutionOrderGuid = new Guid("cadd9467-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Текст конфиденциального поручения"</summary>
  public static readonly Guid AttrPrivacyTextGuid = new Guid("cadd937d-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Отвечающий исполнитель"</summary>
  public static readonly Guid AttrResponseUserGuid = new Guid("cadd9292-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Исходящий регистрационный номер"</summary>
  public static readonly Guid AttrOutgoingRegNumberGuid = new Guid("cadd924c-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Идентификатор в системе СМДО"</summary>
  public static readonly Guid AttrSMDO_IdentityGuid = new Guid("cadd9621-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Номер документа поручения"</summary>
  public static readonly Guid AttrResolutionDocumentRegNumGuid = new Guid("cadd94c5-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Режимы обнуления счетчиков"</summary>
  public static readonly Guid AttrCounterResetModesGuid = new Guid("cadd92b5-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Листов"</summary>
  public static readonly Guid AttrPagesCountGuid = new Guid("cad003a7-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Текст поручения"</summary>
  public static readonly Guid AttrResolutionTextGuid = new Guid("cadd9291-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Дата контроля"</summary>
  public static readonly Guid AttrControlDateGuid = new Guid("cadd9290-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Создавать отдельное поручение для каждого исполнителя"</summary>
  public static readonly Guid AttrTempCreateMultipleResolutionsGuid = new Guid("cadd971b-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Отложенное поручение"</summary>
  public static readonly Guid AttrTempDelayedRunGuid = new Guid("cadd971d-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор формы "Форма поручения"</summary>
  public static readonly Guid FormResolutionGuid = new Guid("cadd92fa-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор формы "Внутренний документ"</summary>
  public static readonly Guid FormInternalDocumentGuid = new Guid("cadd9300-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор формы "Входящий документ"</summary>
  public static readonly Guid FormIngoingDocumentGuid = new Guid("cadd92fe-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор формы "Исходящий документ"</summary>
  public static readonly Guid FormOutgoingDocumentGuid = new Guid("cadd92fc-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор формы "Карточка канцелярского документа"</summary>
  public static readonly Guid FormOfficeDocGuid = new Guid("cadd9375-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор контейнера со счетчиками</summary>
  public static readonly Guid ObjectCounterGuid = new Guid("cadd92b4-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор шаблона процесса для автоматической отправки почты</summary>
  public static readonly Guid ObjectAutoSendTemplateGuid = new Guid("cadd933a-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор группы АДРЕСАТЫ</summary>
  public static readonly Guid ObjectAddresseeGroupGuid = new Guid("cadd94dc-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор роли "Администратор"</summary>
  public static readonly Guid ObjectAdminRoleGuid = new Guid("cad00006-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор схемы ЖЦ поручений</summary>
  public static Guid LcSchemeOfResolutionGuid = new Guid("cadd93a4-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор шага схемы ЖЦ поручений "Создание объекта"</summary>
  public static Guid LсResolutionStepCreationGuid = new Guid("cadd93a2-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор шага схемы ЖЦ поручений "Поручено"</summary>
  public static Guid LсResolutionStepChargedGuid = new Guid("cadd93a1-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор шага схемы ЖЦ поручений "Контроль выполнения"</summary>
  public static Guid LсResolutionStepControlGuid = new Guid("cadd9719-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор шага схемы ЖЦ поручений "Выполнено"</summary>
  public static Guid LcResolutionStepCompletedGuid = new Guid("cadd9718-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор шага схемы ЖЦ поручений "Удаление объекта"</summary>
  public static Guid LсResolutionStepDeletedGuid = new Guid("cadd93a3-306c-11d8-b4e9-00304f19f545");
  /// <summary>Имя модуля для конфигурации</summary>
  public const string ModuleName = "Intermech.Office";
  /// <summary>Имя секции с общими настройками конфигурации</summary>
  public const string GeneralSectionID = "General";
  /// <summary>Имя параметра в конфигурации с идентификатором шаблона для автоматической отправки документов</summary>
  public const string AutoSendTemplateParamName = "AutoSendTemplateID";
  /// <summary>Имя параметра в конфигурации с идентификатором шаблона процессов для контрольных параллельных поручений без документов")]</summary>
  public const string ConsistentControlResolutionTemplateParamName = "ConsistentCtrlResolTemplID";
  /// <summary>Имя параметра в конфигурации с идентификатором шаблона процессов для неконтрольных параллельных поручений без документов")]</summary>
  public const string ConsistentNonControlResolutionTemplateParamName = "ConsistentNCtrlResolTemplID";
  /// <summary>Имя параметра в конфигурации с идентификатором шаблона процессов для контрольных последовательных поручений без документов")]</summary>
  public const string ParallelControlResolutionTemplateParamName = "ParallelCtrlResolTemplID";
  /// <summary>
  /// Имя параметра в конфигурации с идентификатором атрибута, в который записывается тема письма при регистрации его в документ
  /// </summary>
  public static string CaptionAttributeForEmailMessagesParamName = "CaptionAttributeForEmailMessages";
  /// <summary>Имя параметра в конфигурации с идентификатором шаблона процессов для неконтрольных последовательных поручений без
  /// документов")]</summary>
  public const string ParallelNonControlResolutionTemplateParamName = "ParallelNCtrlResolTemplID";
  /// <summary>Имя параметра в конфигурации с идентификатором шаблона</summary>
  public const string SendAddresseeTemplateParamName = "SendAddresseeTemplateID";
  /// <summary>Имя параметра в конфигурации с почтовым адресом отправителя для автоматической отправки документов</summary>
  public const string AutoSendEmailParamName = "AutoSendEmail";
  /// <summary>Имя параметра в конфигурации с флагом использования внутренней канцелярии</summary>
  public const string PrivateOfficeParamName = "PrivateOffice";
  /// <summary>Имя параметра в конфигурации с флагом фильтрации поручений</summary>
  public const string FilterResolutionsParamName = "FilterResolutions";
  /// <summary>Имя параметра в конфигурации с идентификатором пользователя-отправителя для автоматической отправки документов</summary>
  public const string AutoSendUserParamName = "AutoSendUserID";
  /// <summary>Имя параметра в конфигурации со строкой, в которой сохранены идентификаторы пользователей, групп пользователей и ролей, которые могут видеть поручения всех пользователей</summary>
  public const string SupervisorObjVerIDs = "SupervisorObjVerIDs";
  /// <summary>Имя параметра в конфигурации с флагом отображать ли узел "Входящие (подразделение)" в дереве навигатора</summary>
  public const string IncomingPrivateFolderEnableParamName = "IncomingPrivateFolderEnable";

  /// <summary>Идентификатор типа объектов "Канцелярские документы"</summary>
  public static int ObjtypeOfficeDocumentsID { get; private set; }

  /// <summary>Идентификатор типа объектов "Поручения"</summary>
  public static int ObjtypeResolutionsID { get; private set; }

  /// <summary>Идентификатор типа объектов "Конфиденциальные поручения"</summary>
  public static int ObjtypeConfidentialResolutionsID { get; private set; }

  /// <summary>Идентификатор типа объектов "Реквизиты документа"</summary>
  public static int ObjtypeDocDetailsID { get; private set; }

  /// <summary>Идентификатор типа объектов "Электронные письма"</summary>
  public static int ObjtypeEmailMessagesID { get; private set; }

  /// <summary>Идентификатор типа объектов "Организационные единицы"</summary>
  public static int ObjtypeOrganizationUnitsID { get; private set; }

  /// <summary>Идентификатор типа объектов "Группы пользователей"</summary>
  public static int ObjtypeGroupsID { get; private set; }

  /// <summary>Идентификатор типа объектов "Документы"</summary>
  public static int ObjtypeDocumentsID { get; private set; }

  /// <summary>Идентификатор типа объектов "Пользователи"</summary>
  public static int ObjtypeUsersID { get; private set; }

  /// <summary>Идентификатор типа объектов "Организации"</summary>
  public static int ObjtypeOrganizationID { get; private set; }

  /// <summary>Идентификатор типа объектов "Контейнер атрибутов"</summary>
  public static int ObjtypeContainerID { get; private set; }

  /// <summary>Идентификатор типа объектов "Подразделения"</summary>
  public static int ObjtypeDepartmentID { get; private set; }

  /// <summary>Идентификатор типа объектов "Роли"</summary>
  public static int ObjtypeRoleID { get; private set; }

  /// <summary>Идентификатор типа связей "Состав канцелярского документа"</summary>
  public static int ReltypeOfficeCompositionID { get; private set; }

  /// <summary>Идентификатор типа связей "Ответ на"</summary>
  public static int ReltypeAnswerID { get; private set; }

  /// <summary>Идентификатор типа связей "Простая вертикальная связь"</summary>
  public static int ReltypeSimpleID { get; private set; }

  /// <summary>Идентификатор группы атрибутов "Атрибуты входящих канцелярских документов"</summary>
  public static int AttrGroupIncomingOfficeParamsID { get; private set; }

  /// <summary>Идентификатор группы атрибутов "Атрибуты внутренних канцелярских документов"</summary>
  public static int AttrGroupInternalOfficeParamsID { get; private set; }

  /// <summary>Идентификатор группы атрибутов "Атрибуты исходящих канцелярских документов"</summary>
  public static int AttrGroupOutgoingOfficeParamsID { get; private set; }

  /// <summary>Идентификатор группы атрибутов "Атрибуты поручений"</summary>
  public static int AttrGroupResolutionParamsID { get; private set; }

  /// <summary>Идентификатор атрибута "Вид канцелярского документа"</summary>
  public static int AttrOfficeDocumentTypeID { get; private set; }

  /// <summary>Идентификатор атрибута "Дата регистрации"</summary>
  public static int AttrRegistrationDateID { get; private set; }

  /// <summary>Идентификатор атрибута "Регистрационный номер"</summary>
  public static int AttrRegNumberID { get; private set; }

  /// <summary>Идентификатор атрибута "В ответ на письмо"</summary>
  public static int AttrInReplyToID { get; private set; }

  /// <summary>Идентификатор атрибута "Счетчик регистрационных номеров"</summary>
  public static int AttrCountersID { get; private set; }

  /// <summary>Идентификатор атрибута "Идентификатор письма"</summary>
  public static int AttrMessageIdentityID { get; private set; }

  /// <summary>Идентификатор атрибута "Собственная канцелярия"</summary>
  public static int AttrSelfOfficeID { get; private set; }

  /// <summary>Идентификатор атрибута "Ссылка на подразделение"</summary>
  public static int AttrUnitLinkID { get; private set; }

  /// <summary>Идентификатор атрибута "Настройки подразделения"</summary>
  public static int AttrUnitSettingsID { get; private set; }

  /// <summary>Идентификатор атрибута "Автор"</summary>
  public static int AttrAuthorID { get; private set; }

  /// <summary>Идентификатор атрибута "Контролер"</summary>
  public static int AttrControllerID { get; private set; }

  /// <summary>Идентификатор атрибута "Плановая дата исполнения"</summary>
  public static int AttrPlannedDateID { get; private set; }

  /// <summary>Идентификатор атрибута "Контрольное поручение"</summary>
  public static int AttrIsControlResolutionID { get; private set; }

  /// <summary>Идентификатор атрибута "Идентификатор поручения"</summary>
  public static int AttrResolutionIdentityID { get; private set; }

  /// <summary>Идентификатор атрибута "Исполнители поручения"</summary>
  public static int AttrExecutorsID { get; private set; }

  /// <summary>Идентификатор атрибута "Авторы отчетов"</summary>
  public static int AttrReportAuthorsID { get; private set; }

  /// <summary>Идентификатор атрибута "Отчеты исполнения"</summary>
  public static int AttrReportsID { get; private set; }

  /// <summary>Идентификатор атрибута "Даты отчетов исполнения"</summary>
  public static int AttrReportDatesID { get; private set; }

  /// <summary>Идентификатор атрибута "Адресаты"</summary>
  public static int AttrAddresseesID { get; private set; }

  [Obsolete]
  public static Guid AttrAdressatsGuid => OfficeConsts.AttrAddresseesGuid;

  [Obsolete]
  public static int AttrAdressatsID => OfficeConsts.AttrAddresseesID;

  /// <summary>Идентификатор атрибута "Получатель документа"</summary>
  public static int AttrDocRecipientID { get; private set; }

  /// <summary>Идентификатор атрибута "Получатели документа"</summary>
  public static int AttrDocRecipientsID { get; private set; }

  /// <summary>Идентификатор атрибута "Входящий регистрационный номер"</summary>
  public static int AttrInputRegNumID { get; private set; }

  /// <summary>Идентификатор атрибута "Входящие регистрационные номера"</summary>
  public static int AttrInputRegNumsID { get; private set; }

  /// <summary>Идентификатор атрибута "Дата регистрации в организации-адресате"</summary>
  public static int AttrAddresseeRegDateID { get; private set; }

  [Obsolete]
  public static Guid AttrAdressatRegDateGuid => OfficeConsts.AttrAddresseeRegDateGuid;

  [Obsolete]
  public static int AttrAdressatRegDateID => OfficeConsts.AttrAddresseeRegDateID;

  /// <summary>Идентификатор атрибута "Даты регистрации в организациях-адресатах"</summary>
  public static int AttrAddresseeRegDatesID { get; private set; }

  [Obsolete]
  public static Guid AttrAdressatRegDatesGuid => OfficeConsts.AttrAddresseeRegDatesGuid;

  [Obsolete]
  public static int AttrAdressatRegDatesID => OfficeConsts.AttrAddresseeRegDatesID;

  /// <summary>Идентификатор атрибута "Фактическая дата исполнения"</summary>
  public static int AttrActualDateID { get; private set; }

  /// <summary>Идентификатор атрибута "Требует ответа"</summary>
  public static int AttrResponseRequiresID { get; private set; }

  /// <summary>Идентификатор атрибута "Срок ответа"</summary>
  public static int AttrResponseDateID { get; private set; }

  /// <summary>Идентификатор типа связей "Адресант"</summary>
  public static int AttrAddresserID { get; private set; }

  /// <summary>Идентификатор атрибута "E-mail отправителя"</summary>
  public static int AttrEmailSenderID { get; private set; }

  /// <summary>Идентификатор атрибута "Кто подписал"</summary>
  public static int AttrSignatoryID { get; private set; }

  /// <summary>Идентификатор атрибута "Тема сообщения"</summary>
  public static int AttrSubjectID { get; private set; }

  /// <summary>Идентификатор атрибута "Сообщение"</summary>
  public static int AttrMessageID { get; private set; }

  /// <summary>Идентификатор атрибута "Внутренний регистрационный номер"</summary>
  public static int AttrPrivateRegNumberID { get; private set; }

  /// <summary>Идентификатор атрибута "Зарегистрирован во внутренней канцелярии"</summary>
  public static int AttrIsPrivateRegisterID { get; private set; }

  /// <summary>Идентификатор атрибута "Прочитано"</summary>
  public static int AttrReadID { get; private set; }

  /// <summary>Идентификатор атрибута "Исполнение поручения"</summary>
  public static int AttrResolutionExecuteTypeID { get; private set; }

  /// <summary>Идентификатор атрибута "Порядок исполнения"</summary>
  public static int AttrExecutionOrderID { get; private set; }

  /// <summary>Идентификатор атрибута "Текст конфиденциального поручения"</summary>
  public static int AttrPrivacyTextID { get; private set; }

  /// <summary>Идентификатор атрибута "Отвечающий исполнитель"</summary>
  public static int AttrResponseUserID { get; private set; }

  /// <summary>Идентификатор атрибута "Исходящий регистрационный номер"</summary>
  public static int AttrOutgoingRegNumberID { get; private set; }

  /// <summary>Идентификатор атрибута "Идентификатор в системе СМДО"</summary>
  public static int AttrSMDO_IdentityID { get; private set; }

  /// <summary>Идентификатор атрибута "Номер документа поручения"</summary>
  public static int AttrResolutionDocumentRegNumID { get; private set; }

  /// <summary>Идентификатор атрибута "Режимы обнуления счетчиков"</summary>
  public static int AttrCounterResetModesID { get; private set; }

  /// <summary>Идентификатор атрибута "Листов"</summary>
  public static int AttrPagesCountID { get; private set; }

  /// <summary>Идентификатор атрибута "Текст поручения"</summary>
  public static int AttrResolutionTextID { get; private set; }

  /// <summary>Идентификатор атрибута "Дата контроля"</summary>
  public static int AttrControlDateID { get; private set; }

  /// <summary>Идентификатор атрибута "Создавать отдельное поручение для каждого исполнителя"</summary>
  public static int AttrTempCreateMultipleResolutionsID { get; private set; }

  /// <summary>Идентификатор атрибута "Отложенное поручение"</summary>
  public static int AttrTempDelayedRunID { get; private set; }

  /// <summary>Идентификатор атрибута "Электронная почта"</summary>
  public static int AttrEmailAddressID { get; private set; }

  /// <summary>Идентификатор атрибута с именем пользователя для отображения "Выводимое имя"</summary>
  public static int AttrUserNameID { get; private set; }

  /// <summary>Идентификатор атрибута "Наименование"</summary>
  public static int AttrNameID { get; private set; }

  /// <summary>Идентификатор атрибута "Файл"</summary>
  public static int AttrFileID { get; private set; }

  /// <summary>Идентификатор атрибута "Классификация создаваемых объектов"</summary>
  public static int AttrClassifiedObjectsID { get; private set; }

  /// <summary>Идентификатор атрибута "Обозначение"</summary>
  public static int AttrDesignationID { get; private set; }

  /// <summary>Идентификатор атрибута "Руководитель"</summary>
  public static int AttrDirectorID { get; private set; }

  /// <summary>Идентификатор формы "Форма поручения"</summary>
  public static long FormResolutionID { get; private set; }

  /// <summary>Идентификатор формы "Внутренний документ"</summary>
  public static long FormInternalDocumentID { get; private set; }

  [Obsolete]
  public static Guid FormInternalDocumnentGuid => OfficeConsts.FormInternalDocumentGuid;

  [Obsolete]
  public static long FormInternalDocumnentID => OfficeConsts.FormInternalDocumentID;

  /// <summary>Идентификатор формы "Входящий документ"</summary>
  public static long FormIngoingDocumentID { get; private set; }

  /// <summary>Идентификатор формы "Исходящий документ"</summary>
  public static long FormOutgoingDocumentID { get; private set; }

  [Obsolete]
  public static Guid FormOutgoinDocumentGuid => OfficeConsts.FormOutgoingDocumentGuid;

  [Obsolete]
  public static long FormOutgoinDocumentID => OfficeConsts.FormOutgoingDocumentID;

  /// <summary>Идентификатор формы "Карточка канцелярского документа"</summary>
  public static long FormOfficeDocID { get; private set; }

  /// <summary>Идентификатор контейнера со счетчиками</summary>
  public static long ObjectCounterID { get; private set; }

  /// <summary>Идентификатор шаблона процесса для автоматической отправки почты</summary>
  public static long ObjectAutoSendTemplateID { get; private set; }

  /// <summary>Идентификатор группы АДРЕСАТЫ</summary>
  public static long ObjectAddresseeGroupID { get; private set; }

  [Obsolete]
  public static Guid ObjectAddressatGroupGuid => OfficeConsts.ObjectAddresseeGroupGuid;

  [Obsolete]
  public static long ObjectAddressatGroupID => OfficeConsts.ObjectAddresseeGroupID;

  /// <summary>Идентификатор пользователя СИСТЕМА - юзер, под которым работает система</summary>
  public static long ObjectSystemUserID { get; private set; }

  /// <summary>Идентификатор группы пользователей "СОЗДАТЕЛЬ_ОБЪЕКТА"</summary>
  public static long ObjectCreatorUserGroupID { get; private set; }

  /// <summary>Идентификатор группы пользователей "ВСЕ_ПОЛЬЗОВАТЕЛИ"</summary>
  public static long ObjectAllUsersUserGroupID { get; private set; }

  [Obsolete]
  public static long ObjectSYSTEM_userID => OfficeConsts.ObjectSystemUserID;

  [Obsolete]
  public static long ObjectCREATOR_userGroupID => OfficeConsts.ObjectCreatorUserGroupID;

  [Obsolete]
  public static long ObjectALL_USERS_userGroupID => OfficeConsts.ObjectAllUsersUserGroupID;

  /// <summary>Идентификатор роли "Администратор"</summary>
  public static long ObjectAdminRoleID { get; private set; }

  /// <summary>Идентификатор уровня продвижения "Персональный объект"</summary>
  public static int LevelPersonalID { get; private set; }

  /// <summary>Идентификатор уровня продвижения "Создание и модификация"</summary>
  public static int LevelCreatedID { get; private set; }

  /// <summary>Идентификатор уровня продвижения "Согласование и утверждение"</summary>
  public static int LevelSigningID { get; private set; }

  /// <summary>Идентификатор уровня продвижения "Импортировано"</summary>
  public static int LevelImportedID { get; private set; }

  /// <summary>Идентификатор уровня продвижения "Аннулировано"</summary>
  public static int LevelAnnulmentID { get; private set; }

  /// <summary>Идентификатор уровня продвижения "Производство и эксплуатация"</summary>
  public static int LevelManufacturingID { get; private set; }

  /// <summary>Идентификатор уровня продвижения "Удалено"</summary>
  public static int LevelDeletedID { get; private set; }

  /// <summary>Идентификатор уровня продвижения "Хранение"</summary>
  public static int LevelKeepingID { get; private set; }

  /// <summary>Идентификатор схемы ЖЦ поручений</summary>
  public static int LcSchemeOfResolutionID { get; private set; }

  [Obsolete]
  public static Guid LCschemeOfResolutionGuid => OfficeConsts.LcSchemeOfResolutionGuid;

  [Obsolete]
  public static int LCschemeOfResolutionID => OfficeConsts.LcSchemeOfResolutionID;

  /// <summary>Идентификатор шага схемы ЖЦ поручений "Создание объекта"</summary>
  public static int LсResolutionStepCreationID { get; private set; }

  /// <summary>Идентификатор шага схемы ЖЦ поручений "Поручено"</summary>
  public static int LсResolutionStepChargedID { get; private set; }

  /// <summary>Идентификатор шага схемы ЖЦ поручений "Контроль выполнения"</summary>
  public static int LсResolutionStepControlID { get; private set; }

  /// <summary>Идентификатор шага схемы ЖЦ поручений "Выполнено"</summary>
  public static int LcResolutionStepCompletedID { get; private set; }

  [Obsolete]
  public static Guid LсResolutionStepComplitedGuid => OfficeConsts.LcResolutionStepCompletedGuid;

  [Obsolete]
  public static int LсResolutionStepComplitedID => OfficeConsts.LcResolutionStepCompletedID;

  /// <summary>Идентификатор шага схемы ЖЦ поручений "Удаление объекта"</summary>
  public static int LсResolutionStepDeletedID { get; private set; }

  public static void Init([NotNull] IUserSession session)
  {
    if (OfficeConsts.ObjtypeOfficeDocumentsID != 0)
      return;
    OfficeConsts.ObjtypeOfficeDocumentsID = MetaDataHelper.GetObjectTypeID(OfficeConsts.ObjtypeOfficeDocumentsGuid);
    OfficeConsts.ObjtypeResolutionsID = MetaDataHelper.GetObjectTypeID(OfficeConsts.ObjtypeResolutionsGuid);
    OfficeConsts.ObjtypeConfidentialResolutionsID = MetaDataHelper.GetObjectTypeID(OfficeConsts.ObjtypeConfidentialResolutionsGuid);
    OfficeConsts.ObjtypeDocDetailsID = MetaDataHelper.GetObjectTypeID(OfficeConsts.ObjtypeDocDetailsGuid);
    OfficeConsts.ObjtypeEmailMessagesID = MetaDataHelper.GetObjectTypeID(OfficeConsts.ObjtypeEmailMessagesGuid);
    OfficeConsts.ObjtypeOrganizationUnitsID = MetaDataHelper.GetObjectTypeID("cadd9235-306c-11d8-b4e9-00304f19f545");
    OfficeConsts.ObjtypeGroupsID = MetaDataHelper.GetObjectTypeID("cad00003-306c-11d8-b4e9-00304f19f545");
    OfficeConsts.ObjtypeDocumentsID = MetaDataHelper.GetObjectTypeID("cad00070-306c-11d8-b4e9-00304f19f545");
    OfficeConsts.ObjtypeUsersID = MetaDataHelper.GetObjectTypeID("cad00002-306c-11d8-b4e9-00304f19f545");
    OfficeConsts.ObjtypeOrganizationID = MetaDataHelper.GetObjectTypeID("cadd9231-306c-11d8-b4e9-00304f19f545");
    OfficeConsts.ObjtypeContainerID = MetaDataHelper.GetObjectTypeID("cad0013b-306c-11d8-b4e9-00304f19f545");
    OfficeConsts.ObjtypeDepartmentID = MetaDataHelper.GetObjectTypeID("cadd9232-306c-11d8-b4e9-00304f19f545");
    OfficeConsts.ObjtypeRoleID = MetaDataHelper.GetObjectTypeID("cad00007-306c-11d8-b4e9-00304f19f545");
    OfficeConsts.ReltypeOfficeCompositionID = MetaDataHelper.GetRelationTypeID(OfficeConsts.ReltypeOfficeCompositionGuid);
    OfficeConsts.ReltypeAnswerID = MetaDataHelper.GetRelationTypeID(OfficeConsts.ReltypeAnswerGuid);
    OfficeConsts.ReltypeSimpleID = MetaDataHelper.GetRelationTypeID("cad00022-306c-11d8-b4e9-00304f19f545");
    OfficeConsts.AttrGroupIncomingOfficeParamsID = MetaDataHelper.GetAttributeGroupID(OfficeConsts.AttrGroupIncomingOfficeParamsGuid);
    OfficeConsts.AttrGroupInternalOfficeParamsID = MetaDataHelper.GetAttributeGroupID(OfficeConsts.AttrGroupInternalOfficeParamsGuid);
    OfficeConsts.AttrGroupOutgoingOfficeParamsID = MetaDataHelper.GetAttributeGroupID(OfficeConsts.AttrGroupOutgoingOfficeParamsGuid);
    OfficeConsts.AttrGroupResolutionParamsID = MetaDataHelper.GetAttributeGroupID(OfficeConsts.AttrGroupResolutionParamsGuid);
    OfficeConsts.AttrOfficeDocumentTypeID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrOfficeDocumentTypeGuid);
    OfficeConsts.AttrRegistrationDateID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrRegistrationDateGuid);
    OfficeConsts.AttrRegNumberID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrRegNumberGuid);
    OfficeConsts.AttrInReplyToID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrInReplyToGuid);
    OfficeConsts.AttrCountersID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrCountersGuid);
    OfficeConsts.AttrMessageIdentityID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrMessageIdentityGuid);
    OfficeConsts.AttrSelfOfficeID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrSelfOfficeGuid);
    OfficeConsts.AttrUnitLinkID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrUnitLinkGuid);
    OfficeConsts.AttrUnitSettingsID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrUnitSettingsGuid);
    OfficeConsts.AttrAuthorID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrAuthorGuid);
    OfficeConsts.AttrControllerID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrControllerGuid);
    OfficeConsts.AttrPlannedDateID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrPlannedDateGuid);
    OfficeConsts.AttrIsControlResolutionID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrIsControlResolutionGuid);
    OfficeConsts.AttrResolutionIdentityID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrResolutionIdentityGuid);
    OfficeConsts.AttrExecutorsID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrExecutorsGuid);
    OfficeConsts.AttrReportAuthorsID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrReportAuthorsGuid);
    OfficeConsts.AttrReportsID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrReportsGuid);
    OfficeConsts.AttrReportDatesID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrReportDatesGuid);
    OfficeConsts.AttrAddresseesID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrAddresseesGuid);
    OfficeConsts.AttrDocRecipientID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrDocRecipientGuid);
    OfficeConsts.AttrDocRecipientsID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrDocRecipientsGuid);
    OfficeConsts.AttrInputRegNumID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrInputRegNumGuid);
    OfficeConsts.AttrInputRegNumsID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrInputRegNumsGuid);
    OfficeConsts.AttrAddresseeRegDateID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrAddresseeRegDateGuid);
    OfficeConsts.AttrAddresseeRegDatesID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrAddresseeRegDatesGuid);
    OfficeConsts.AttrActualDateID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrActualDateGuid);
    OfficeConsts.AttrResponseRequiresID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrResponseRequiresGuid);
    OfficeConsts.AttrResponseDateID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrResponseDateGuid);
    OfficeConsts.AttrAddresserID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrAddresserGuid);
    OfficeConsts.AttrEmailSenderID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrEmailSenderGuid);
    OfficeConsts.AttrSignatoryID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrSignatoryGuid);
    OfficeConsts.AttrSubjectID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrSubjectGuid);
    OfficeConsts.AttrMessageID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrMessageGuid);
    OfficeConsts.AttrPrivateRegNumberID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrPrivateRegNumberGuid);
    OfficeConsts.AttrIsPrivateRegisterID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrIsPrivateRegisterGuid);
    OfficeConsts.AttrReadID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrReadGuid);
    OfficeConsts.AttrResolutionExecuteTypeID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrResolutionExecuteTypeGuid);
    OfficeConsts.AttrExecutionOrderID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrExecutionOrderGuid);
    OfficeConsts.AttrPrivacyTextID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrPrivacyTextGuid);
    OfficeConsts.AttrResponseUserID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrResponseUserGuid);
    OfficeConsts.AttrOutgoingRegNumberID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrOutgoingRegNumberGuid);
    OfficeConsts.AttrSMDO_IdentityID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrSMDO_IdentityGuid);
    OfficeConsts.AttrResolutionDocumentRegNumID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrResolutionDocumentRegNumGuid);
    OfficeConsts.AttrCounterResetModesID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrCounterResetModesGuid);
    OfficeConsts.AttrPagesCountID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrPagesCountGuid);
    OfficeConsts.AttrResolutionTextID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrResolutionTextGuid);
    OfficeConsts.AttrControlDateID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrControlDateGuid);
    OfficeConsts.AttrTempCreateMultipleResolutionsID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrTempCreateMultipleResolutionsGuid);
    OfficeConsts.AttrTempDelayedRunID = MetaDataHelper.GetAttributeTypeID(OfficeConsts.AttrTempDelayedRunGuid);
    OfficeConsts.AttrEmailAddressID = MetaDataHelper.GetAttributeTypeID("cad002de-306c-11d8-b4e9-00304f19f545");
    OfficeConsts.AttrUserNameID = MetaDataHelper.GetAttributeTypeID("cad0001d-306c-11d8-b4e9-00304f19f545");
    OfficeConsts.AttrNameID = MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545");
    OfficeConsts.AttrFileID = MetaDataHelper.GetAttributeTypeID("cad0004b-306c-11d8-b4e9-00304f19f545");
    OfficeConsts.AttrClassifiedObjectsID = MetaDataHelper.GetAttributeTypeID("cad001d9-306c-11d8-b4e9-00304f19f545");
    OfficeConsts.AttrDesignationID = MetaDataHelper.GetAttributeTypeID("cad0001f-306c-11d8-b4e9-00304f19f545");
    OfficeConsts.AttrDirectorID = MetaDataHelper.GetAttributeTypeID("cadd9233-306c-11d8-b4e9-00304f19f545");
    OfficeConsts.FormResolutionID = session.GetObjectInfo(OfficeConsts.FormResolutionGuid).ObjectID;
    OfficeConsts.FormInternalDocumentID = session.GetObjectInfo(OfficeConsts.FormInternalDocumentGuid).ObjectID;
    OfficeConsts.FormIngoingDocumentID = session.GetObjectInfo(OfficeConsts.FormIngoingDocumentGuid).ObjectID;
    OfficeConsts.FormOutgoingDocumentID = session.GetObjectInfo(OfficeConsts.FormOutgoingDocumentGuid).ObjectID;
    OfficeConsts.FormOfficeDocID = session.GetObjectInfo(OfficeConsts.FormOfficeDocGuid).ObjectID;
    OfficeConsts.ObjectCounterID = session.GetObjectInfo(OfficeConsts.ObjectCounterGuid).ObjectID;
    OfficeConsts.ObjectAutoSendTemplateID = session.GetObjectInfo(OfficeConsts.ObjectAutoSendTemplateGuid).ObjectID;
    OfficeConsts.ObjectAddresseeGroupID = session.GetObjectInfo(OfficeConsts.ObjectAddresseeGroupGuid).ObjectID;
    OfficeConsts.ObjectSystemUserID = session.IdentHelper.SystemID;
    OfficeConsts.ObjectCreatorUserGroupID = session.IdentHelper.OwnerGroupID;
    OfficeConsts.ObjectAllUsersUserGroupID = session.IdentHelper.AllUsersGroupID;
    OfficeConsts.ObjectAdminRoleID = session.IdentHelper.AdminRoleID;
    OfficeConsts.LevelPersonalID = MetaDataHelper.GetLCLevelID("cad00049-306c-11d8-b4e9-00304f19f545");
    OfficeConsts.LevelCreatedID = MetaDataHelper.GetLCLevelID("cad00013-306c-11d8-b4e9-00304f19f545");
    OfficeConsts.LevelSigningID = MetaDataHelper.GetLCLevelID("cad003be-306c-11d8-b4e9-00304f19f545");
    OfficeConsts.LevelImportedID = MetaDataHelper.GetLCLevelID("cad0069a-306c-11d8-b4e9-00304f19f545");
    OfficeConsts.LevelAnnulmentID = MetaDataHelper.GetLCLevelID("cad00012-306c-11d8-b4e9-00304f19f545");
    OfficeConsts.LevelManufacturingID = MetaDataHelper.GetLCLevelID("cad00011-306c-11d8-b4e9-00304f19f545");
    OfficeConsts.LevelDeletedID = MetaDataHelper.GetLCLevelID("cad0000e-306c-11d8-b4e9-00304f19f545");
    OfficeConsts.LevelKeepingID = MetaDataHelper.GetLCLevelID("cad009de-306c-11d8-b4e9-00304f19f545");
    OfficeConsts.LcSchemeOfResolutionID = MetaDataHelper.GetLCSchemaID(OfficeConsts.LcSchemeOfResolutionGuid);
    OfficeConsts.LсResolutionStepCreationID = MetaDataHelper.GetLCStepID(OfficeConsts.LсResolutionStepCreationGuid);
    OfficeConsts.LсResolutionStepChargedID = MetaDataHelper.GetLCStepID(OfficeConsts.LсResolutionStepChargedGuid);
    OfficeConsts.LсResolutionStepControlID = MetaDataHelper.GetLCStepID(OfficeConsts.LсResolutionStepControlGuid);
    OfficeConsts.LcResolutionStepCompletedID = MetaDataHelper.GetLCStepID(OfficeConsts.LcResolutionStepCompletedGuid);
    OfficeConsts.LсResolutionStepDeletedID = MetaDataHelper.GetLCStepID(OfficeConsts.LсResolutionStepDeletedGuid);
  }
}
