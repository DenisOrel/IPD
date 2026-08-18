// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.wfConsts
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Workflow;

/// <summary>Класс, содержащий константы маршрутизатора</summary>
public class wfConsts
{
  public const string AutoNotificationSettingsElmntName = "AutoNotificationSettings";
  public const string AccessDeniedNotificationElmntName = "AccessDeniedAutoNotification";
  public const string AttributableNotificationElmntName = "AttributableAutoNotification";
  public const string LCStepNotificationElmntName = "LCStepAutoNotification";
  public const string LCLevelNotificationElmntName = "LCLevelAutoNotification";
  public const string AttrChangingNotificationElmntName = "AttrChangingAutoNotification";
  public const string AutoNotificationIDAttrName = "AutoNotificationID";
  public const string NotifEventTypeAttrName = "ActionType";
  public const string WayOfNotificationAttrName = "WayOfNotification";
  public const string FilterTypesElemName = "FilterTypes";
  public const string ObjectTypeIdsElemName = "ObjectTypeIds";
  public const string AdresseeElemName = "Adressee";
  public const string MessageElemName = "Message";
  public const string ListItemElemName = "ListItem";
  public const string ListItemIdAttrName = "ListItemId";
  public const string ListCountAttrName = "ListCount";
  public const string AdresseeTypeAttrName = "Adressee";
  public const string ObjectSetSourceElemName = "ObjectSetSource";
  public const string ObjectSetObtainMethodAttrName = "ObjectSetObtainMethod";
  public const string VersionRuleAttrName = "VersionRuleID";
  public const string ObjectTypesIdsElemName = "ObjectTypesIds";
  public const string RelationTypesIdsElemName = "RelationTypesIds";
  public const string ScriptIdAttrName = "ScriptIdAttr";
  public const string SearchSchemeIdAttrName = "SearchSchemeId";
  public const string AdresseeSourceElemName = "AdresseeSource";
  public const string AdresseeSourceTypeAttrName = "AdresseeSourceType";
  public const string AttrIdAttrName = "AttributeId";
  public const string EmailAttrName = "Email";
  public const string UserIdsElemName = "UserIds";
  public const string GroupIdsElemName = "GroupIds";
  public const string RoleIdsElemName = "RoleIds";
  public const string AccessTypeAttrName = "AccessType";
  public const string ActuationConditionElemName = "ActuationCondition";
  public const string FormulaElemName = "FormulaForAttr";
  public const string FormulaStringElemName = "FormulaStr";
  public const string SpreadFormulaAttrName = "SpreadFormula";
  public const string UseOldAttrValuesAttrName = "UseOldAttrValues";
  public const string LcSchemeIdAttrName = "LcSchemeId";
  public const string LcStepIdAttrName = "LcStepId";
  public const string LcLevelIdAttrName = "LcLevelId";
  public const string AttrTypesIdsElemName = "AttrTypesIds";
  public const string SpecificAdresseeTypeName = "SpecificAdressee";
  public const string ComputeAdresseeTypeName = "ComputeAdressee";
  /// <summary>Имя лог-файла для записи ошибок</summary>
  public static readonly string AutoNotifLogFile = "AutoNotification.log";
  /// <summary>Guid объекта "Автоматическое уведомление"</summary>
  public static readonly Guid AutoNotificationTypeGuid = new Guid("cadd96e1-306c-11d8-b4e9-00304f19f545");
  /// <summary>ID объекта "Автоматическое уведомление"</summary>
  public static int AutoNotificationTypeID;
  /// <summary>Guid шага ЖЦ "Создание автоуведомления"</summary>
  public static readonly Guid CreateAutoNotificationLCStepGuid = new Guid("cadd996a-306c-11d8-b4e9-00304f19f545");
  /// <summary>ID шага ЖЦ "Создание автоуведомления"</summary>
  public static int CreateAutoNotificationLCStepID;
  /// <summary>Guid атрибута Настройки автоуведомлений</summary>
  public static readonly Guid AttrAutoNotificationSettingsGuid = new Guid("cadd9738-306c-11d8-b4e9-00304f19f545");
  /// <summary>ID атрибута Настройки автоуведомлений</summary>
  public static int AttrAutoNotificationSettingsID;
  /// <summary>Guid атрибута Тип автоуведомления</summary>
  public static readonly Guid AttrAutoNotificationTypeGuid = new Guid("cadd9968-306c-11d8-b4e9-00304f19f545");
  /// <summary>ID атрибута Тип автоуведомления</summary>
  public static int AttrAutoNotificationTypeID;
  /// <summary>Подбор базовых версий</summary>
  public static long FiltrationBaseVersionsID;
  /// <summary>
  /// Разрешена ли передача значений атрибутов в службу автоматических уведомлений
  /// </summary>
  public static bool SendAttrs2DelayedNotificationMode = false;
  /// <summary>Guid атрибута Автор</summary>
  public static readonly Guid AttrAuthorGuid = new Guid("cadd928c-306c-11d8-b4e9-00304f19f545");
  /// <summary>ID атрибута Автор</summary>
  public static int AttrAuthorID;
  /// <summary>Guid атрибута Владелец</summary>
  public static readonly Guid AttrOwnerGuid = new Guid("cad0002f-306c-11d8-b4e9-00304f19f545");
  /// <summary>ID атрибута Владелец</summary>
  public static int AttrOwnerID;
  /// <summary>Guid атрибута Руководитель</summary>
  public static readonly Guid AttrDirectorGuid = new Guid("cadd9233-306c-11d8-b4e9-00304f19f545");
  /// <summary>ID атрибута Руководитель</summary>
  public static int AttrDirectorID;
  /// <summary>Guid типа Организации</summary>
  public static readonly Guid OrganizationTypeGuid = new Guid("cadd9231-306c-11d8-b4e9-00304f19f545");
  /// <summary>ID типа Организации</summary>
  public static int OrganizationTypeID = 0;
  /// <summary>Guid типа Подразделения</summary>
  public static readonly Guid DepartmentTypeGuid = new Guid("cadd9231-306c-11d8-b4e9-00304f19f545");
  /// <summary>ID типа Подразделения</summary>
  public static int DepartmentTypeID = 0;
  /// <summary>Объекты маршрутизатора Guid</summary>
  public static readonly Guid ObjectsGuid = new Guid("cad002aa-306c-11d8-b4e9-00304f19f545");
  /// <summary>Объекты маршрутизатора ID</summary>
  public static int ObjectsTypeID = 0;
  /// <summary>тип Шаблон процесса Guid</summary>
  public static readonly Guid SchemesGuid = new Guid("cad002ac-306c-11d8-b4e9-00304f19f545");
  /// <summary>тип Шаблон процесса ID</summary>
  public static int SchemesTypeID = 0;
  /// <summary>тип Процесс Guid</summary>
  public static readonly Guid ProcessesGuid = new Guid("cad002ad-306c-11d8-b4e9-00304f19f545");
  /// <summary>тип Процесс ID</summary>
  public static int ProcessesTypeID;
  /// <summary>тип Действия Guid</summary>
  public static readonly Guid ActivitiesGuid = new Guid("cad002af-306c-11d8-b4e9-00304f19f545");
  /// <summary>тип Действия ID</summary>
  public static int ActivitiesTypeID;
  /// <summary>тип Действия с исполнителями Guid</summary>
  public static readonly Guid ParticipantActivitiesGuid = new Guid("cad002b1-306c-11d8-b4e9-00304f19f545");
  /// <summary>тип Действия с исполнителями ID</summary>
  public static int ParticipantActivitiesTypeID;
  /// <summary>тип Элементы процесса Guid</summary>
  public static readonly Guid ProcessAtomsGuid = new Guid("cad002ae-306c-11d8-b4e9-00304f19f545");
  /// <summary>тип Элементы процесса ID</summary>
  public static int ProcessAtomsTypeID;
  public static readonly Guid LinksGuid = new Guid("cad002b0-306c-11d8-b4e9-00304f19f545");
  public static int LinksTypeID;
  public static readonly Guid ActivityExecLCStepGuid = new Guid("cad00368-306c-11d8-b4e9-00304f19f545");
  public static int ActivityExecLCStepID;
  public static int LinkExecLCStepID;
  /// <summary>Категории шаблонов процессов Guid</summary>
  public static readonly Guid SchemeCategoriesGuid = new Guid("cad002ab-306c-11d8-b4e9-00304f19f545");
  /// <summary>Категории шаблонов процессов ID</summary>
  public static int SchemeCategoriesID;
  /// <summary>Формы маршрутизатора Guid</summary>
  public static readonly Guid FormsGuid = new Guid("cad002be-306c-11d8-b4e9-00304f19f545");
  /// <summary>Формы маршрутизатора ID</summary>
  public static int FormsTypeID;
  public static readonly Guid StartGuid = new Guid("cad002b3-306c-11d8-b4e9-00304f19f545");
  public static int StartTypeID;
  public static readonly Guid TaskGuid = new Guid("cad002b5-306c-11d8-b4e9-00304f19f545");
  public static int TaskTypeID;
  public static readonly Guid ApproveGuid = new Guid("cad002b4-306c-11d8-b4e9-00304f19f545");
  public static int ApproveTypeID;
  public static readonly Guid StopGuid = new Guid("cad002b6-306c-11d8-b4e9-00304f19f545");
  public static int StopTypeID;
  public static readonly Guid CondGuid = new Guid("cad002b7-306c-11d8-b4e9-00304f19f545");
  public static int CondTypeID;
  public static readonly Guid CaseGuid = new Guid("cad002b8-306c-11d8-b4e9-00304f19f545");
  public static int CaseTypeID;
  public static readonly Guid SubProcessGuid = new Guid("cad002b9-306c-11d8-b4e9-00304f19f545");
  public static int SubProcessTypeID;
  public static readonly Guid AbortGuid = new Guid("cad002ba-306c-11d8-b4e9-00304f19f545");
  public static int AbortTypeID;
  public static readonly Guid TimerGuid = new Guid("cad002bb-306c-11d8-b4e9-00304f19f545");
  public static int TimerTypeID;
  public static readonly Guid RegisterGuid = new Guid("cad0133f-306c-11d8-b4e9-00304f19f545");
  public static int RegisterTypeID;
  public static readonly Guid ScriptGuid = new Guid("cad0132c-306c-11d8-b4e9-00304f19f545");
  public static int ScriptTypeID;
  public static readonly Guid LifeCycleGuid = new Guid("cad002bf-306c-11d8-b4e9-00304f19f545");
  public static int LifeCycleTypeID;
  public static readonly Guid RemoteSubProcessGuid = new Guid("cad01333-306c-11d8-b4e9-00304f19f545");
  public static int RemoteSubProcessTypeID;
  /// <summary>Почтовое предложение</summary>
  public static readonly Guid WorkOfferTypeGuid = new Guid("cad002bc-306c-11d8-b4e9-00304f19f545");
  /// <summary>Почтовое предложение</summary>
  public static int WorkOfferTypeID;
  /// <summary>Почтовое сообщение</summary>
  public static readonly Guid MessageTypeGuid = new Guid("cad002bd-306c-11d8-b4e9-00304f19f545");
  /// <summary>Почтовое сообщение</summary>
  public static int MessageTypeID;
  /// <summary>Список идентификаторов типов относящихся к сообщениям</summary>
  public static readonly List<int> MessageTypeIDs = new List<int>();
  /// <summary>ИД типа группы пользователей</summary>
  public static int GroupTypeID;
  /// <summary>ИД типа пользователи</summary>
  public static int UserTypeID;
  /// <summary>ИД типа должности</summary>
  public static int RanksTypeID;
  /// <summary>ИД типа роли</summary>
  public static int RolesTypeID;
  public static readonly Guid SignGraphGuid = new Guid("cad00141-306c-11d8-b4e9-00304f19f545");
  public static int SignGraphID;
  /// <summary>Атрибут приоритета процесса</summary>
  public static readonly Guid AttrPriorityGuid = new Guid("cad002d1-306c-11d8-b4e9-00304f19f545");
  /// <summary>Атрибут приоритета процесса</summary>
  public static int AttrPriorityID;
  /// <summary>атрибут "Body" (устарел)</summary>
  public static readonly Guid AttrBodyGuid = new Guid("cad002c2-306c-11d8-b4e9-00304f19f545");
  /// <summary>атрибут "Body" (устарел)</summary>
  public static int AttrBodyID;
  /// <summary>атрибут "Графические данные"</summary>
  public static readonly Guid AttrGraphDataGuid = new Guid("cadd94f3-306c-11d8-b4e9-00304f19f545");
  /// <summary>атрибут "Графические данные"</summary>
  public static int AttrGraphDataID;
  /// <summary>атрибут "Название"</summary>
  public static readonly Guid AttrNameGuid = new Guid("cad002c3-306c-11d8-b4e9-00304f19f545");
  /// <summary>атрибут "Название"</summary>
  public static int AttrNameID;
  /// <summary>атрибут "Описание"</summary>
  public static readonly Guid AttrDescriptionGuid = new Guid("cad0001c-306c-11d8-b4e9-00304f19f545");
  /// <summary>атрибут "Описание"</summary>
  public static int AttrDescriptionID;
  /// <summary>атрибут "Исполнители"</summary>
  public static readonly Guid AttrParticipantsGuid = new Guid("cad002c4-306c-11d8-b4e9-00304f19f545");
  /// <summary>атрибут "Исполнители"</summary>
  public static int AttrParticipantsID;
  /// <summary>Атрибут "Коллектор"</summary>
  public static readonly Guid AttrCollectorGuid = new Guid("cad002c7-306c-11d8-b4e9-00304f19f545");
  /// <summary>Атрибут "Коллектор"</summary>
  public static int AttrCollectorID;
  /// <summary>Атрибут "Уведомления"</summary>
  public static readonly Guid AttrNotificationsGuid = new Guid("cad002d5-306c-11d8-b4e9-00304f19f545");
  /// <summary>Атрибут "Уведомления"</summary>
  public static int AttrNotificationsID;
  /// <summary>Атрибут "Файл импорта"</summary>
  public static readonly Guid AttrBriefcaseGuid = new Guid("cadd972a-306c-11d8-b4e9-00304f19f545");
  /// <summary>Атрибут "Файл импорта"</summary>
  public static int AttrBriefcaseID;
  /// <summary>Guid атрибута "Объект уведомлений"</summary>
  public static readonly Guid AttrNotifyObjectGuid = new Guid("cad0062c-306c-11d8-b4e9-00304f19f545");
  /// <summary>ID атрибута "Объект уведомлений"</summary>
  public static int AttrNotifyObjectID;
  /// <summary>Guid объекта "Уведомление об изменении"</summary>
  public static readonly Guid NotifyObjectGuid = new Guid("cad00627-306c-11d8-b4e9-00304f19f545");
  /// <summary>ID объекта "Уведомление об изменении"</summary>
  public static int NotifyObjectTypeID;
  /// <summary>ГУИД атрибута "Атрибуты для уведомления об изменении"</summary>
  public static readonly Guid AttrListAttributesGuid = new Guid("cadd93ca-306c-11d8-b4e9-00304f19f545");
  /// <summary>ID атрибута "Атрибуты для уведомления об изменении"</summary>
  public static int AttrListAttributesID;
  /// <summary>
  /// ГУИД атрибута "Перечень ГУИДов атрибутов для уведомления"
  /// </summary>
  public static Guid AttrGUIDsAttributesGuid = new Guid("cadd9589-306c-11d8-b4e9-00304f19f545");
  /// <summary>
  /// ID атрибута "Перечень ГУИДов атрибутов для уведомления"
  /// </summary>
  public static int AttrGUIDsAttributesID;
  /// <summary>ID атрибута "Список получателей уведомления"</summary>
  public static int AttrAddresseeNoticeID;
  /// <summary>ID атрибута "Даты постановки на уведомление"</summary>
  public static int AttrNotifyDatesID;
  /// <summary>ID атрибута "Свойства уведомлений"</summary>
  public static int AttrNotifyOptionsID;
  /// <summary>Атрибут "Содержит Вложения"</summary>
  public static readonly Guid AttrAttachmentsGuid = new Guid("cad002d7-306c-11d8-b4e9-00304f19f545");
  /// <summary>Атрибут "Содержит Вложения"</summary>
  public static int AttrAttachmentsID;
  /// <summary>Атрибут "Откат"</summary>
  public static readonly Guid AttrRollbackKindGuid = new Guid("cad0034c-306c-11d8-b4e9-00304f19f545");
  /// <summary>Атрибут "Откат"</summary>
  public static int AttrRollbackKindID;
  /// <summary>Атрибут "Шаблон подпроцесса" (для SubProcessActivity)</summary>
  public static readonly Guid AttrSubprocessSchemeGuid = new Guid("cad00352-306c-11d8-b4e9-00304f19f545");
  /// <summary>Атрибут "Шаблон подпроцесса" (для SubProcessActivity)</summary>
  public static int AttrSubprocessSchemeID;
  /// <summary>Атрибут "Подпроцесс" (для SubProcessActivity)</summary>
  public static readonly Guid AttrSubprocessGuid = new Guid("cad0146a-306c-11d8-b4e9-00304f19f545");
  /// <summary>Атрибут "Подпроцесс" (для SubProcessActivity)</summary>
  public static int AttrSubprocessID;
  /// <summary>
  /// Атрибут "Формат имени подпроцесса" (для SubProcessActivity)
  /// </summary>
  public static readonly Guid AttrSubprocFormatGuid = new Guid("cad00353-306c-11d8-b4e9-00304f19f545");
  /// <summary>
  /// Атрибут "Формат имени подпроцесса" (для SubProcessActivity)
  /// </summary>
  public static int AttrSubprocFormatID;
  /// <summary>Ждать завершения</summary>
  public static readonly Guid AttrWaitForCompletionGuid = new Guid("cad00354-306c-11d8-b4e9-00304f19f545");
  /// <summary>Ждать завершения</summary>
  public static int AttrWaitForCompletionID;
  /// <summary>Вид сценария (атрибут связи)</summary>
  public static readonly Guid AttrScriptKindGuid = new Guid("cad00360-306c-11d8-b4e9-00304f19f545");
  /// <summary>Вид сценария (атрибут связи)</summary>
  public static int AttrScriptKindID;
  /// <summary>Текст сценария (атрибут объекта Сценарий)</summary>
  public static readonly Guid AttrScriptTextGuid = new Guid("cad00366-306c-11d8-b4e9-00304f19f545");
  /// <summary>Текст сценария (атрибут объекта Сценарий)</summary>
  public static int AttrScriptTextID;
  /// <summary>Родительский шаблон</summary>
  public static readonly Guid AttrPrototypeGuid = new Guid("cad00362-306c-11d8-b4e9-00304f19f545");
  /// <summary>Родительский шаблон</summary>
  public static int AttrPrototypeID;
  /// <summary>Создание действий по факту выполнения</summary>
  public static readonly Guid AttrCreateActivitiesOnDemandGuid = new Guid("cadd94f4-306c-11d8-b4e9-00304f19f545");
  /// <summary>Создание действий по факту выполнения</summary>
  public static int AttrCreateActivitiesOnDemandID;
  /// <summary>Родительский процесс</summary>
  public static readonly Guid AttrParentProcessGuid = new Guid("cad01332-306c-11d8-b4e9-00304f19f545");
  /// <summary>Родительский процесс</summary>
  public static int AttrParentProcessID;
  /// <summary>Временные права</summary>
  public static readonly Guid AttrTempRightsGuid = new Guid("cadd94c8-306c-11d8-b4e9-00304f19f545");
  /// <summary>Временные права</summary>
  public static int AttrTempRightsID;
  /// <summary>Владелец связи</summary>
  public static readonly Guid AttrRelationOwnerGuid = new Guid("cadd94c9-306c-11d8-b4e9-00304f19f545");
  /// <summary>Владелец связи</summary>
  public static int AttrRelationOwnerID;
  /// <summary>Разрешенные типы вложений</summary>
  public static readonly Guid AttrAllowedAttachTypesGuid = new Guid("cadd98be-306c-11d8-b4e9-00304f19f545");
  /// <summary>Разрешенные типы вложений</summary>
  public static int AttrAllowedAttachTypesID;
  /// <summary>Показывать форму при отправке действия назад</summary>
  public static readonly Guid AttrShowFormWithActivityBackGuid = new Guid("cadd99ab-306c-11d8-b4e9-00304f19f545");
  /// <summary>Показывать форму при отправке действия назад</summary>
  public static int AttrShowFormWithActivityBackID;
  /// <summary>Индивидуальная настройка графов подписей GUID</summary>
  public static readonly Guid AttrGraphForTypeGuid = new Guid("cadd9aa1-306c-11d8-b4e9-00304f19f545");
  /// <summary>Индивидуальная настройка графов подписей ID</summary>
  public static int AttrGraphForTypeID;
  /// Runtime-атрибуты
  ///             <summary>"Начато"</summary>
  public static int AttrStartedID;
  /// <summary>"Выполнено"</summary>
  public static int AttrCompletedID;
  /// <summary>"Статус действия"</summary>
  public static readonly Guid AttrActivityStatusGuid = new Guid("cad002cd-306c-11d8-b4e9-00304f19f545");
  /// <summary>"Статус действия"</summary>
  public static int AttrActivityStatusID;
  /// <summary>"Родительское действие"</summary>
  public static readonly Guid AttrParentActivityGuid = new Guid("cad002cf-306c-11d8-b4e9-00304f19f545");
  /// <summary>"Родительское действие"</summary>
  public static int AttrParentActivityID;
  /// <summary>"Результат выполнения действия"</summary>
  public static readonly Guid AttrActivityResultGuid = new Guid("cad002d0-306c-11d8-b4e9-00304f19f545");
  /// <summary>"Результат выполнения действия"</summary>
  public static int AttrActivityResultID;
  /// <summary>Сообщение на данном шаге</summary>
  public static readonly Guid AttrActivityMessageGuid = new Guid("cad002d2-306c-11d8-b4e9-00304f19f545");
  /// <summary>Сообщение на данном шаге</summary>
  public static int AttrActivityMessageID;
  /// <summary>SenderActivity</summary>
  public static readonly Guid AttrSenderActivityGuid = new Guid("cad002d3-306c-11d8-b4e9-00304f19f545");
  /// <summary>SenderActivity</summary>
  public static int AttrSenderActivityID;
  /// <summary>История выполнения</summary>
  public static readonly Guid AttrExecHistoryGuid = new Guid("cad002d4-306c-11d8-b4e9-00304f19f545");
  /// <summary>История выполнения</summary>
  public static int AttrExecHistoryID;
  /// <summary>Переменные</summary>
  public static readonly Guid AttrVariablesGuid = new Guid("cad0034f-306c-11d8-b4e9-00304f19f545");
  /// <summary>Переменные</summary>
  public static int AttrVariablesID;
  /// <summary>Пользовательская форма</summary>
  public static readonly Guid AttrFormGuid = new Guid("cad00350-306c-11d8-b4e9-00304f19f545");
  /// <summary>Пользовательская форма</summary>
  public static int AttrFormID;
  /// <summary>Требуемые подписи</summary>
  public static readonly Guid AttrRequiredSignsGuid = new Guid("cad0035a-306c-11d8-b4e9-00304f19f545");
  /// <summary>Требуемые подписи</summary>
  public static int AttrRequiredSignsID;
  /// <summary>Статус удаленного процесса</summary>
  public static readonly Guid AttrRemoteProcessStatusGuid = new Guid("cadd94c6-306c-11d8-b4e9-00304f19f545");
  /// <summary>Статус удаленного процесса</summary>
  public static int AttrRemoteProcessStatusID;
  /// <summary>Аттрибут шаблон в режиме отладки</summary>
  public static readonly Guid AttrIsDebugGuid = new Guid("cadd9967-306c-11d8-b4e9-00304f19f545");
  /// <summary>Аттрибут шаблон в режиме отладки</summary>
  public static int AttrIsDebugID;
  /// Системные переменные
  public static readonly Guid SysVarStarterGuid = new Guid("cad00358-306c-11d8-b4e9-00304f19f545");
  public static int SysVarStarterID;
  public static readonly Guid SysVarSenderGuid = new Guid("cad00359-306c-11d8-b4e9-00304f19f545");
  public static int SysVarSenderID;
  public static readonly Guid SysVarDenyDocDeleteGuid = new Guid("cad01330-306c-11d8-b4e9-00304f19f545");
  public static int SysVarDenyDocDeleteID;
  public static readonly Guid SysVarMultiStartGuid = new Guid("cad01331-306c-11d8-b4e9-00304f19f545");
  public static int SysVarMultiStartID;
  public static readonly Guid SysVarTaskPercentGuid = new Guid("cadd9464-306c-11d8-b4e9-00304f19f545");
  public static int SysVarTaskPercentID;
  /// <summary>Администратор шаблона процессов</summary>
  public static readonly Guid SchemeAdministratorGuid = new Guid("cadd9a9d-306c-11d8-b4e9-00304f19f545");
  /// <summary>Администратор шаблона процессов</summary>
  public static int SchemeAdministratorID;
  /// <summary>Guid атрибута Выполнено системой</summary>
  public static readonly Guid AutoExecuteAttributeGuid = new Guid("cadd9c07-306c-11d8-b4e9-00304f19f545");
  /// <summary>ID атрибута Выполнено системой</summary>
  public static int AutoExecuteAttributeID;
  /// Атрибуты связей
  ///              <summary>GUID атрибута "Связь входит в"</summary>
  public static readonly Guid AttrToActivityGuid = new Guid("cad002c5-306c-11d8-b4e9-00304f19f545");
  /// <summary>ID атрибута "Связь входит в"</summary>
  public static int AttrToActivityID;
  /// <summary>GUID атрибута "Связь исходит из"</summary>
  public static readonly Guid AttrFromActivityGuid = new Guid("cad002c6-306c-11d8-b4e9-00304f19f545");
  /// <summary>ID атрибута "Связь исходит из"</summary>
  public static int AttrFromActivityID;
  /// <summary>Атрибут "Действие", содержит ссылку на действие</summary>
  public static readonly Guid AttrActivityGuid = new Guid("cad002c8-306c-11d8-b4e9-00304f19f545");
  /// <summary>Атрибут "Действие", содержит ссылку на действие</summary>
  public static int AttrActivityID;
  /// <summary>Атрибут "Процесс", содержит ссылку на процесс</summary>
  public static readonly Guid AttrProcessGuid = new Guid("cad002ce-306c-11d8-b4e9-00304f19f545");
  /// <summary>Атрибут "Процесс", содержит ссылку на процесс</summary>
  public static int AttrProcessID;
  /// <summary>Атрибут "Тип связей между действиями"</summary>
  public static readonly Guid AttrLinkKindGuid = new Guid("cad0034d-306c-11d8-b4e9-00304f19f545");
  /// <summary>Атрибут "Тип связей между действиями"</summary>
  public static int AttrLinkKindID;
  public static readonly Guid AttrConditionGuid = new Guid("cad00351-306c-11d8-b4e9-00304f19f545");
  public static int AttrConditionID;
  /// <summary>
  /// Гуид атрибута с новыми формулами для условных переходов которые отвязаны от экспертной системы
  /// </summary>
  public static readonly Guid AttrConditionFormulaGuid = new Guid("cadd9b85-306c-11d8-b4e9-00304f19f545");
  /// <summary>
  /// ID атрибута с новыми формулами для условных переходов которые отвязаны от экспертной системы
  /// </summary>
  public static int AttrConditionFormulaID;
  /// <summary>Получатель</summary>
  public static int AttrRecipID;
  public static readonly Guid AttrSenderGuid = new Guid("cad002c9-306c-11d8-b4e9-00304f19f545");
  public static int AttrSenderID;
  /// <summary>Исполняющий обязанности</summary>
  public static int AttrIOUserID;
  public static readonly Guid AttrSubjectGuid = new Guid("cad002d6-306c-11d8-b4e9-00304f19f545");
  public static int AttrSubjectID;
  /// <summary>для Approve: какие типы объектов подписывать</summary>
  public static readonly Guid AttrObjectTypesGuid = new Guid("cad00149-306c-11d8-b4e9-00304f19f545");
  /// <summary>для Approve: какие типы объектов подписывать</summary>
  public static int AttrObjectTypesID;
  /// <summary>Что подписывать</summary>
  public static readonly Guid AttrWhatToSignGuid = new Guid("cad00363-306c-11d8-b4e9-00304f19f545");
  /// <summary>Что подписывать</summary>
  public static int AttrWhatToSignID;
  /// <summary>
  /// для действия "Изменение статуса", содержит настройки куда чего переводить
  /// </summary>
  public static readonly Guid AttrLCConfigAttrGuid = new Guid("cad0035b-306c-11d8-b4e9-00304f19f545");
  /// <summary>
  /// для действия "Изменение статуса", содержит настройки куда чего переводить
  /// </summary>
  public static int AttrLCConfigAttrID;
  public static readonly Guid AttrAddInfoGuid = new Guid("cad0035d-306c-11d8-b4e9-00304f19f545");
  public static int AttrAddInfoID;
  public static readonly Guid AttrObjectListGuid = new Guid("cad00063-306c-11d8-b4e9-00304f19f545");
  public static int AttrObjectListID;
  public static readonly Guid AttrAddIDGuid = new Guid("cad0035e-306c-11d8-b4e9-00304f19f545");
  public static int AttrAddIDID;
  public static readonly Guid AttrDocArchiveGuid = new Guid("cad00361-306c-11d8-b4e9-00304f19f545");
  public static int AttrDocArchiveID;
  public static readonly Guid AttrRevArchiveGuid = new Guid("cad0132a-306c-11d8-b4e9-00304f19f545");
  public static int AttrRevArchiveID;
  public static readonly Guid AttrSenderDeletionGuid = new Guid("cad00355-306c-11d8-b4e9-00304f19f545");
  public static int AttrSenderDeletionID;
  public static readonly Guid AttrRecipDeletionGuid = new Guid("cad00356-306c-11d8-b4e9-00304f19f545");
  public static int AttrRecipDeletionID;
  public static readonly Guid AttrRecipStatusGuid = new Guid("cad0035f-306c-11d8-b4e9-00304f19f545");
  public static int AttrRecipStatusID;
  public static readonly Guid AttrSenderStatusGuid = new Guid("cad00365-306c-11d8-b4e9-00304f19f545");
  public static int AttrSenderStatusID;
  public static long UserID = 0;
  /// <summary>
  /// Идентификатор версии объекта для пользователя "Система"
  /// </summary>
  public static long SystemUserID = 0;
  public static readonly Guid WorkflowVarsGroupGuid = new Guid("cad0034e-306c-11d8-b4e9-00304f19f545");
  public static int WorkflowVarsGroupID;
  public static readonly Guid WorkflowSysVarsGroupGuid = new Guid("cad00357-306c-11d8-b4e9-00304f19f545");
  public static int WorkflowSysVarsGroupID;
  public static int CategorySchemesRoot;
  public static readonly Guid CategorySchemesRootGuid = new Guid("{97F31340-C301-444d-94BD-DF8DADC08519}");
  public static int SimpleLinkTypeID = 0;
  public static readonly int MaxStoredTextLength = Consts.DefaultStringDbFieldLength;
  public static HashSet<ActivityKind> ParticipantActivityKinds = new HashSet<ActivityKind>((IEnumerable<ActivityKind>) new ActivityKind[3]
  {
    ActivityKind.Start,
    ActivityKind.Task,
    ActivityKind.Approve
  });
  public static HashSet<ActivityKind> MayBeParticipantActivityKinds = new HashSet<ActivityKind>((IEnumerable<ActivityKind>) new ActivityKind[2]
  {
    ActivityKind.Script,
    ActivityKind.RemoteSubProcess
  });
  public static HashSet<ActivityKind> RollbackActivityKinds = new HashSet<ActivityKind>((IEnumerable<ActivityKind>) new ActivityKind[7]
  {
    ActivityKind.Task,
    ActivityKind.Approve,
    ActivityKind.Register,
    ActivityKind.SubProcess,
    ActivityKind.LCStep,
    ActivityKind.RemoteSubProcess,
    ActivityKind.Script
  });
  protected static int[] _termActivityTypes = (int[]) null;
  protected static List<int> _mailObjectTypes = (List<int>) null;
  public static string UnknownStr = "<?>";
  public static Guid WorkflowTimerServiceGuid = new Guid("cad0035c-306c-11d8-b4e9-00304f19f545");
  public static Guid WorkflowPortalDelayStarterGuid = new Guid("cadd9c06-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid ScriptsGuid = new Guid("cad0036a-306c-11d8-b4e9-00304f19f545");
  public static int ScriptsTypeID;
  public static readonly Guid ScriptRelationGuid = new Guid("cad00367-306c-11d8-b4e9-00304f19f545");
  public static int ScriptRelationTypeID;
  public static readonly Guid AttrScriptExecSide = new Guid("cad01328-306c-11d8-b4e9-00304f19f545");
  public static int AttrScriptExecSideID;
  public static readonly Guid FileTypeGuid = new Guid("cad00118-306c-11d8-b4e9-00304f19f545");
  public static int FileTypeID;
  public static readonly Guid AttachmentRelationGuid = new Guid("cad01329-306c-11d8-b4e9-00304f19f545");
  public static int AttachmentRelationTypeID;
  public static readonly Guid ArchivesTypeGuid = new Guid("cad0011e-306c-11d8-b4e9-00304f19f545");
  public static int ArchivesTypeID;
  public static readonly Guid AttrArchiveGuid = SystemGUIDs.attributeArchive;
  public static int AttrArchiveID;
  public static readonly Guid AttrVersionLinkGuid = new Guid("cad001c2-306c-11d8-b4e9-00304f19f545");
  public static int AttrVersionLinkID;
  public static readonly Guid VirtualColumnSchemeGuid = new Guid("C99B0B8A-0D45-45f5-9E9A-DCF8911AC925");
  public static int AttrFormNameID;
  public static readonly Guid AttrFormBodyGuid = new Guid("cad0011d-306c-11d8-b4e9-00304f19f545");
  public static int AttrFormBodyID;
  public static int DocsInECORelationTypeID;
  public static readonly Guid AttrTermsGuid = new Guid("cad00364-306c-11d8-b4e9-00304f19f545");
  public static int AttrTermsID;
  public static readonly Guid AttrUserRankGuid = new Guid("cad00142-306c-11d8-b4e9-00304f19f545");
  public static int AttrUserRankID;
  public static int AttrCompletedTermID;
  public static readonly Guid AttrBlockingGuid = new Guid("cad0132e-306c-11d8-b4e9-00304f19f545");
  public static int AttrBlockingID;
  public static List<ActivityStatus> ExecStatuses = new List<ActivityStatus>((IEnumerable<ActivityStatus>) new ActivityStatus[4]
  {
    ActivityStatus.Executed,
    ActivityStatus.CollectorWaiting,
    ActivityStatus.DefineWaiting,
    ActivityStatus.ParticipantWaiting
  });
  public static List<ActivityStatus> CompletedStatuses = new List<ActivityStatus>((IEnumerable<ActivityStatus>) new ActivityStatus[4]
  {
    ActivityStatus.Terminated,
    ActivityStatus.Completed,
    ActivityStatus.AutoCompleted,
    ActivityStatus.Recalled
  });
  public static List<int> ProtectedAttributeTypes = new List<int>();
  public static int MaxNonUserActivitiesCounter = 50;
  public static readonly Guid AttrMailFolderGuid = new Guid("cad0132f-306c-11d8-b4e9-00304f19f545");
  public static int AttrMailFolderID;
  public static int AttrExternalUserID;
  public static int AttrFileID;
  public static readonly Guid ProductionLCLevelGuid = new Guid("cad00011-306c-11d8-b4e9-00304f19f545");
  public static int ProductionLCLevelID = 0;
  public static long ObjectOwnerGroupID;
  public static readonly Guid SignsRelationTypeGuid = new Guid("cad00139-306c-11d8-b4e9-00304f19f545");
  public static int SignsRelationTypeID = 0;
  public static readonly Guid LinkedTaskRelationTypeGuid = new Guid("cadd9465-306c-11d8-b4e9-00304f19f545");
  public static int LinkedTaskRelationTypeID;
  /// <summary>
  /// Глобальный идентификатор типа объектов "Электронные письма"
  /// </summary>
  public static readonly Guid objtypeEmailMessages = new Guid("cadd932d-306c-11d8-b4e9-00304f19f545");
  /// <summary>Идентификатор типа объектов "Электронные письма"</summary>
  public static int objtypeEmailMessagesID = 0;
  /// <summary>
  /// Глобальный идентификатор типа атрибутов "Электронная почта"
  /// </summary>
  public static readonly Guid attributeEmail = new Guid("cad002de-306c-11d8-b4e9-00304f19f545");
  /// <summary>Идентификатор атрибута "Электронная почта"</summary>
  public static int attributeEmailID = 0;
  /// <summary>
  /// Глобальный идентификатор атрибута "Идентификатор письма"
  /// </summary>
  public static readonly Guid attributeMessageIDGuid = new Guid("cadd92d5-306c-11d8-b4e9-00304f19f545");
  /// <summary>Идентификатор атрибута "Идентификатор письма"</summary>
  public static int attributeMessageID = 0;
  /// <summary>
  /// Глобальный идентификатор атрибута "E-mail отправителя"
  /// </summary>
  public static readonly Guid attributeEmailSender = new Guid("cadd92d6-306c-11d8-b4e9-00304f19f545");
  /// <summary>Идентификатор атрибута "E-mail отправителя"</summary>
  public static int attributeEmailSenderID = 0;
  /// <summary>Глобальный идентификатор атрибута "В ответ на письмо"</summary>
  public static readonly Guid attributeInReplyTo = new Guid("cadd932e-306c-11d8-b4e9-00304f19f545");
  /// <summary>Идентификатор атрибута "В ответ на письмо"</summary>
  public static int attributeInReplyToID = 0;
  /// <summary>
  /// Глобальный идентификатор атрибута "Канцелярский документ"
  /// </summary>
  public static readonly Guid attributeOfficeDocument = new Guid("cadd932f-306c-11d8-b4e9-00304f19f545");
  /// <summary>Идентификатор атрибута "Канцелярский документ"</summary>
  public static int attributeOfficeDocumentID = 0;
  /// <summary>
  /// Глобальный идентификатор атрибута "Отправитель письма"
  /// </summary>
  public static readonly Guid attributeSender = new Guid("cadd92d2-306c-11d8-b4e9-00304f19f545");
  /// <summary>Идентификатор атрибута "Отправитель письма"</summary>
  public static int attributeSenderID = 0;
  /// <summary>Глобальный идентификатор атрибута "Дата письма"</summary>
  public static readonly Guid attributeEmailData = new Guid("cadd92d4-306c-11d8-b4e9-00304f19f545");
  /// <summary>Идентификатор атрибута "Дата письма"</summary>
  public static int attributeEmailDataID = 0;
  public static string AllObjectsCaption = LocalizationHolder.rm.GetString("Workflow.Design_AllObjects");
  private static int _incompleteObjectType = -1;
  /// <summary>Локальные сценарии маршрутизатора GUID типа</summary>
  public static readonly Guid WorkflowLocalScriptGuid = new Guid("cadd996d-306c-11d8-b4e9-00304f19f545");
  /// <summary>Локальные сценарии маршрутизатора ID типа</summary>
  public static int WorkflowLocalScript;
  /// <summary>Общие сценарии маршрутизатора GUID типа</summary>
  public static readonly Guid WorkflowCommonScriptGuid = new Guid("cadd996e-306c-11d8-b4e9-00304f19f545");
  /// <summary>Общие сценарии маршрутизатора ID типа</summary>
  public static int WorkflowCommonScript;
  /// <summary>
  /// Guid группы атрибутов "Глобальные переменные маршрутизатора"
  /// </summary>
  public static readonly Guid GlobalVariablesGroupGuid = new Guid("cadd9aba-306c-11d8-b4e9-00304f19f545");
  /// <summary>
  /// ID группы атрибутов "Глобальные переменные маршрутизатора"
  /// </summary>
  public static int GlobalVariablesGroupID;
  public const string ElseLinkName = "ИНАЧЕ";
  public static int CheckOutMode = -1024;
  public static List<int> ECOTypes = new List<int>();
  public static List<int> SystemVariables = (List<int>) null;

  /// <summary>
  /// Тип объектов "Неполный ссылочный объект". Объекты этого типа создаются при импорте из портфеля workflow некоторых атрибутов для создания ссылок на объекты, которых нет в текущей базе
  /// </summary>
  public static int IncompleteObjectType
  {
    get
    {
      if (wfConsts._incompleteObjectType == -1)
        wfConsts._incompleteObjectType = MetaDataHelper.GetObjectTypeID(new Guid("cadd960d-306c-11d8-b4e9-00304f19f545"));
      return wfConsts._incompleteObjectType;
    }
  }

  public static void Init(IUserSession sess)
  {
    wfConsts.UserID = sess.UserID;
    if (wfConsts.ObjectsTypeID != 0)
      return;
    wfConsts.FiltrationBaseVersionsID = sess.GetObjectInfo(new Guid("cad00601-306c-11d8-b4e9-00304f19f545")).ObjectID;
    wfConsts.SchemesTypeID = sess.GetObjectType(wfConsts.SchemesGuid).ObjectType;
    wfConsts.ProcessesTypeID = sess.GetObjectType(wfConsts.ProcessesGuid).ObjectType;
    wfConsts.ActivitiesTypeID = sess.GetObjectType(wfConsts.ActivitiesGuid).ObjectType;
    wfConsts.ParticipantActivitiesTypeID = sess.GetObjectType(wfConsts.ParticipantActivitiesGuid).ObjectType;
    wfConsts.ProcessAtomsTypeID = sess.GetObjectType(wfConsts.ProcessAtomsGuid).ObjectType;
    wfConsts.NotifyObjectTypeID = sess.GetObjectType(wfConsts.NotifyObjectGuid).ObjectType;
    wfConsts.AutoNotificationTypeID = sess.GetObjectType(wfConsts.AutoNotificationTypeGuid).ObjectType;
    wfConsts.WorkflowCommonScript = sess.GetObjectType(wfConsts.WorkflowCommonScriptGuid).ObjectType;
    wfConsts.WorkflowLocalScript = sess.GetObjectType(wfConsts.WorkflowLocalScriptGuid).ObjectType;
    wfConsts.AttrParticipantsID = sess.GetAttributeType(wfConsts.AttrParticipantsGuid).AttributeID;
    wfConsts.GroupTypeID = sess.IdentHelper.GroupsTypeID;
    wfConsts.UserTypeID = sess.IdentHelper.UsersTypeID;
    wfConsts.RanksTypeID = sess.IdentHelper.RanksTypeID;
    wfConsts.RolesTypeID = sess.IdentHelper.RolesTypeID;
    wfConsts.AttrNameID = sess.GetAttributeType(wfConsts.AttrNameGuid).AttributeID;
    IDBAttributeType attributeType1 = sess.GetAttributeType(wfConsts.AttrBodyGuid, false);
    if (attributeType1 != null)
      wfConsts.AttrBodyID = attributeType1.AttributeID;
    wfConsts.AttrGraphDataID = sess.GetAttributeType(wfConsts.AttrGraphDataGuid).AttributeID;
    wfConsts.AttrDescriptionID = sess.GetAttributeType(wfConsts.AttrDescriptionGuid).AttributeID;
    wfConsts.LinksTypeID = sess.GetObjectType(wfConsts.LinksGuid).ObjectType;
    wfConsts.WorkOfferTypeID = sess.GetObjectType(wfConsts.WorkOfferTypeGuid).ObjectType;
    wfConsts.MessageTypeID = sess.GetObjectType(wfConsts.MessageTypeGuid).ObjectType;
    wfConsts.ObjectsTypeID = sess.GetObjectType(wfConsts.ObjectsGuid).ObjectType;
    ActivityInfos.Init(sess);
    wfConsts.AttrCollectorID = sess.GetAttributeType(wfConsts.AttrCollectorGuid).AttributeID;
    wfConsts.AttrProcessID = sess.GetAttributeType(wfConsts.AttrProcessGuid).AttributeID;
    wfConsts.AttrActivityID = sess.GetAttributeType(wfConsts.AttrActivityGuid).AttributeID;
    wfConsts.AttrRecipID = sess.GetAttributeType(SystemGUIDs.attributeRecipient).AttributeID;
    wfConsts.AttrSenderID = sess.GetAttributeType(wfConsts.AttrSenderGuid).AttributeID;
    wfConsts.AttrIOUserID = sess.GetAttributeType(new Guid("cadd91f5-306c-11d8-b4e9-00304f19f545")).AttributeID;
    wfConsts.AttrStartedID = sess.GetAttributeType(SystemGUIDs.attributeStart).AttributeID;
    wfConsts.AttrCompletedID = sess.GetAttributeType(SystemGUIDs.attributeFinish).AttributeID;
    wfConsts.AttrActivityStatusID = sess.GetAttributeType(wfConsts.AttrActivityStatusGuid).AttributeID;
    wfConsts.AttrToActivityID = sess.GetAttributeType(wfConsts.AttrToActivityGuid).AttributeID;
    wfConsts.AttrFromActivityID = sess.GetAttributeType(wfConsts.AttrFromActivityGuid).AttributeID;
    wfConsts.AttrParentActivityID = sess.GetAttributeType(wfConsts.AttrParentActivityGuid).AttributeID;
    wfConsts.AttrActivityResultID = sess.GetAttributeType(wfConsts.AttrActivityResultGuid).AttributeID;
    wfConsts.AttrPriorityID = sess.GetAttributeType(wfConsts.AttrPriorityGuid).AttributeID;
    wfConsts.AttrActivityMessageID = sess.GetAttributeType(wfConsts.AttrActivityMessageGuid).AttributeID;
    wfConsts.AttrSenderActivityID = sess.GetAttributeType(wfConsts.AttrSenderActivityGuid).AttributeID;
    wfConsts.AttrExecHistoryID = sess.GetAttributeType(wfConsts.AttrExecHistoryGuid).AttributeID;
    wfConsts.StartTypeID = sess.GetObjectType(wfConsts.StartGuid).ObjectType;
    wfConsts.TaskTypeID = sess.GetObjectType(wfConsts.TaskGuid).ObjectType;
    wfConsts.ApproveTypeID = sess.GetObjectType(wfConsts.ApproveGuid).ObjectType;
    wfConsts.AttrNotificationsID = sess.GetAttributeType(wfConsts.AttrNotificationsGuid).AttributeID;
    wfConsts.AttrBriefcaseID = sess.GetAttributeType(wfConsts.AttrBriefcaseGuid).AttributeID;
    wfConsts.AttrAddresseeNoticeID = sess.GetAttributeType(new Guid("cad00628-306c-11d8-b4e9-00304f19f545")).AttributeID;
    wfConsts.AttrGUIDsAttributesID = sess.GetAttributeType(wfConsts.AttrGUIDsAttributesGuid).AttributeID;
    wfConsts.AttrNotifyDatesID = sess.GetAttributeType(new Guid("cad0062a-306c-11d8-b4e9-00304f19f545")).AttributeID;
    wfConsts.AttrNotifyOptionsID = sess.GetAttributeType(new Guid("cad0062b-306c-11d8-b4e9-00304f19f545")).AttributeID;
    wfConsts.AttrAutoNotificationSettingsID = sess.GetAttributeType(wfConsts.AttrAutoNotificationSettingsGuid).AttributeID;
    wfConsts.AttrAutoNotificationTypeID = sess.GetAttributeType(wfConsts.AttrAutoNotificationTypeGuid).AttributeID;
    wfConsts.AttrAuthorID = sess.GetAttributeType(wfConsts.AttrAuthorGuid).AttributeID;
    wfConsts.AttrOwnerID = sess.GetAttributeType(wfConsts.AttrOwnerGuid).AttributeID;
    wfConsts.AttrDirectorID = sess.GetAttributeType(wfConsts.AttrDirectorGuid).AttributeID;
    IDBAttributeType attributeType2 = sess.GetAttributeType(wfConsts.AttrListAttributesGuid, false);
    if (attributeType2 != null)
      wfConsts.AttrListAttributesID = attributeType2.AttributeID;
    wfConsts.AttrNotifyObjectID = sess.GetAttributeType(wfConsts.AttrNotifyObjectGuid).AttributeID;
    wfConsts.AttrSubjectID = sess.GetAttributeType(wfConsts.AttrSubjectGuid).AttributeID;
    wfConsts.AttrAttachmentsID = sess.GetAttributeType(wfConsts.AttrAttachmentsGuid).AttributeID;
    wfConsts.SchemeCategoriesID = sess.GetObjectType(wfConsts.SchemeCategoriesGuid).ObjectType;
    wfConsts.AttrRollbackKindID = sess.GetAttributeType(wfConsts.AttrRollbackKindGuid).AttributeID;
    wfConsts.AttrLinkKindID = sess.GetAttributeType(wfConsts.AttrLinkKindGuid).AttributeID;
    wfConsts.WorkflowVarsGroupID = sess.GetAttributesGroup(wfConsts.WorkflowVarsGroupGuid).GroupID;
    wfConsts.WorkflowSysVarsGroupID = sess.GetAttributesGroup(wfConsts.WorkflowSysVarsGroupGuid).GroupID;
    wfConsts.AttrVariablesID = sess.GetAttributeType(wfConsts.AttrVariablesGuid).AttributeID;
    wfConsts.AttrFormID = sess.GetAttributeType(wfConsts.AttrFormGuid).AttributeID;
    wfConsts.AttrConditionID = sess.GetAttributeType(wfConsts.AttrConditionGuid).AttributeID;
    wfConsts.AttrConditionFormulaID = sess.GetAttributeType(wfConsts.AttrConditionFormulaGuid).AttributeID;
    wfConsts.StopTypeID = sess.GetObjectType(wfConsts.StopGuid).ObjectType;
    wfConsts.CondTypeID = sess.GetObjectType(wfConsts.CondGuid).ObjectType;
    wfConsts.CaseTypeID = sess.GetObjectType(wfConsts.CaseGuid).ObjectType;
    wfConsts.SubProcessTypeID = sess.GetObjectType(wfConsts.SubProcessGuid).ObjectType;
    wfConsts.AbortTypeID = sess.GetObjectType(wfConsts.AbortGuid).ObjectType;
    wfConsts.TimerTypeID = sess.GetObjectType(wfConsts.TimerGuid).ObjectType;
    wfConsts.RegisterTypeID = sess.GetObjectType(wfConsts.RegisterGuid).ObjectType;
    wfConsts.ScriptTypeID = sess.GetObjectType(wfConsts.ScriptGuid).ObjectType;
    wfConsts.LifeCycleTypeID = sess.GetObjectType(wfConsts.LifeCycleGuid).ObjectType;
    wfConsts.RemoteSubProcessTypeID = sess.GetObjectType(wfConsts.RemoteSubProcessGuid).ObjectType;
    wfConsts.AttrSubprocessSchemeID = sess.GetAttributeType(wfConsts.AttrSubprocessSchemeGuid).AttributeID;
    wfConsts.AttrSubprocessID = sess.GetAttributeType(wfConsts.AttrSubprocessGuid).AttributeID;
    wfConsts.AttrSubprocFormatID = sess.GetAttributeType(wfConsts.AttrSubprocFormatGuid).AttributeID;
    wfConsts.FormsTypeID = sess.GetObjectType(wfConsts.FormsGuid).ObjectType;
    wfConsts.AttrWaitForCompletionID = sess.GetAttributeType(wfConsts.AttrWaitForCompletionGuid).AttributeID;
    wfConsts.SimpleLinkTypeID = sess.IdentHelper.SimpleRelationTypeID;
    wfConsts.AttrSenderDeletionID = sess.GetAttributeType(wfConsts.AttrSenderDeletionGuid).AttributeID;
    wfConsts.AttrRecipDeletionID = sess.GetAttributeType(wfConsts.AttrRecipDeletionGuid).AttributeID;
    wfConsts.ActivityExecLCStepID = sess.GetLifecycleStep(wfConsts.ActivityExecLCStepGuid).LCStep;
    wfConsts.LinkExecLCStepID = wfConsts.ActivityExecLCStepID;
    wfConsts.CreateAutoNotificationLCStepID = sess.GetLifecycleStep(wfConsts.CreateAutoNotificationLCStepGuid).LCStep;
    wfConsts.SignGraphID = sess.GetAttributeType(wfConsts.SignGraphGuid).AttributeID;
    wfConsts.AttrRequiredSignsID = sess.GetAttributeType(wfConsts.AttrRequiredSignsGuid).AttributeID;
    wfConsts.AttrObjectTypesID = sess.GetAttributeType(wfConsts.AttrObjectTypesGuid).AttributeID;
    wfConsts.AttrLCConfigAttrID = sess.GetAttributeType(wfConsts.AttrLCConfigAttrGuid).AttributeID;
    wfConsts.AttrAddInfoID = sess.GetAttributeType(wfConsts.AttrAddInfoGuid).AttributeID;
    wfConsts.AttrObjectListID = sess.GetAttributeType(wfConsts.AttrObjectListGuid).AttributeID;
    wfConsts.AttrAddIDID = sess.GetAttributeType(wfConsts.AttrAddIDGuid).AttributeID;
    wfConsts.AttrRecipStatusID = sess.GetAttributeType(wfConsts.AttrRecipStatusGuid).AttributeID;
    wfConsts.AttrSenderStatusID = sess.GetAttributeType(wfConsts.AttrSenderStatusGuid).AttributeID;
    wfConsts.ScriptsTypeID = sess.GetObjectType(wfConsts.ScriptsGuid).ObjectType;
    wfConsts.FileTypeID = sess.GetObjectType(wfConsts.FileTypeGuid).ObjectType;
    wfConsts.AttrScriptKindID = sess.GetAttributeType(wfConsts.AttrScriptKindGuid).AttributeID;
    wfConsts.AttrRemoteProcessStatusID = sess.GetAttributeType(wfConsts.AttrRemoteProcessStatusGuid).AttributeID;
    wfConsts.AttrIsDebugID = sess.GetAttributeType(wfConsts.AttrIsDebugGuid).AttributeID;
    wfConsts.ScriptRelationTypeID = sess.GetRelationType(wfConsts.ScriptRelationGuid).RelationType;
    wfConsts.AttrScriptTextID = sess.GetAttributeType(wfConsts.AttrScriptTextGuid).AttributeID;
    wfConsts.AttrScriptExecSideID = sess.GetAttributeType(wfConsts.AttrScriptExecSide).AttributeID;
    wfConsts.AttrDocArchiveID = sess.GetAttributeType(wfConsts.AttrDocArchiveGuid).AttributeID;
    wfConsts.AttrRevArchiveID = sess.GetAttributeType(wfConsts.AttrRevArchiveGuid).AttributeID;
    wfConsts.ArchivesTypeID = sess.GetObjectType(wfConsts.ArchivesTypeGuid).ObjectType;
    wfConsts.DepartmentTypeID = sess.GetObjectType(wfConsts.DepartmentTypeGuid).ObjectType;
    wfConsts.OrganizationTypeID = sess.GetObjectType(wfConsts.OrganizationTypeGuid).ObjectType;
    wfConsts.AttrArchiveID = sess.GetAttributeType(wfConsts.AttrArchiveGuid).AttributeID;
    wfConsts.AttachmentRelationTypeID = sess.GetRelationType(wfConsts.AttachmentRelationGuid).RelationType;
    wfConsts.AttrVersionLinkID = sess.GetAttributeType(wfConsts.AttrVersionLinkGuid).AttributeID;
    wfConsts.AttrPrototypeID = sess.GetAttributeType(wfConsts.AttrPrototypeGuid).AttributeID;
    wfConsts.AttrCreateActivitiesOnDemandID = sess.GetAttributeType(wfConsts.AttrCreateActivitiesOnDemandGuid).AttributeID;
    wfConsts.AttrFormNameID = sess.IdentHelper.NameID;
    wfConsts.AttrFormBodyID = sess.GetAttributeType(wfConsts.AttrFormBodyGuid).AttributeID;
    wfConsts.DocsInECORelationTypeID = sess.GetRelationType(new Guid("cad0036b-306c-11d8-b4e9-00304f19f545")).RelationType;
    wfConsts.FillECOTypes(sess);
    wfConsts.AttrWhatToSignID = sess.GetAttributeType(wfConsts.AttrWhatToSignGuid).AttributeID;
    wfConsts.AttrTermsID = sess.GetAttributeType(wfConsts.AttrTermsGuid).AttributeID;
    wfConsts.AttrUserRankID = sess.GetAttributeType(wfConsts.AttrUserRankGuid).AttributeID;
    wfConsts.AttrCompletedTermID = sess.GetAttributeType(SystemGUIDs.attributeDueDate).AttributeID;
    wfConsts.AttrBlockingID = sess.GetAttributeType(wfConsts.AttrBlockingGuid).AttributeID;
    wfConsts.AttrMailFolderID = sess.GetAttributeType(wfConsts.AttrMailFolderGuid).AttributeID;
    wfConsts.SignsRelationTypeID = sess.GetRelationType(wfConsts.SignsRelationTypeGuid).RelationType;
    wfConsts.AttrParentProcessID = sess.GetAttributeType(wfConsts.AttrParentProcessGuid).AttributeID;
    wfConsts.AttrTempRightsID = sess.GetAttributeType(wfConsts.AttrTempRightsGuid).AttributeID;
    wfConsts.AttrRelationOwnerID = sess.GetAttributeType(wfConsts.AttrRelationOwnerGuid).AttributeID;
    wfConsts.AttrAllowedAttachTypesID = sess.GetAttributeType(wfConsts.AttrAllowedAttachTypesGuid).AttributeID;
    wfConsts.AttrShowFormWithActivityBackID = sess.GetAttributeType(wfConsts.AttrShowFormWithActivityBackGuid).AttributeID;
    wfConsts.AttrGraphForTypeID = sess.GetAttributeType(wfConsts.AttrGraphForTypeGuid).AttributeID;
    wfConsts.AttrExternalUserID = sess.GetAttributeType(new Guid("cad002df-306c-11d8-b4e9-00304f19f545")).AttributeID;
    wfConsts.AttrFileID = sess.GetAttributeType(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545")).AttributeID;
    wfConsts.LinkedTaskRelationTypeID = sess.GetRelationType(wfConsts.LinkedTaskRelationTypeGuid).RelationType;
    wfConsts.SysVarStarterID = sess.GetAttributeType(wfConsts.SysVarStarterGuid).AttributeID;
    wfConsts.SysVarSenderID = sess.GetAttributeType(wfConsts.SysVarSenderGuid).AttributeID;
    wfConsts.SysVarDenyDocDeleteID = sess.GetAttributeType(wfConsts.SysVarDenyDocDeleteGuid).AttributeID;
    wfConsts.SysVarMultiStartID = sess.GetAttributeType(wfConsts.SysVarMultiStartGuid).AttributeID;
    wfConsts.SysVarTaskPercentID = sess.GetAttributeType(wfConsts.SysVarTaskPercentGuid).AttributeID;
    wfConsts.SchemeAdministratorID = sess.GetAttributeType(wfConsts.SchemeAdministratorGuid).AttributeID;
    wfConsts.AutoExecuteAttributeID = sess.GetAttributeType(wfConsts.AutoExecuteAttributeGuid).AttributeID;
    wfConsts.attributeOfficeDocumentID = sess.GetAttributeType(wfConsts.attributeOfficeDocument).AttributeID;
    wfConsts.attributeMessageID = sess.GetAttributeType(wfConsts.attributeMessageIDGuid).AttributeID;
    wfConsts.attributeInReplyToID = sess.GetAttributeType(wfConsts.attributeInReplyTo).AttributeID;
    wfConsts.attributeSenderID = sess.GetAttributeType(wfConsts.attributeSender).AttributeID;
    wfConsts.attributeEmailDataID = sess.GetAttributeType(wfConsts.attributeEmailData).AttributeID;
    wfConsts.attributeEmailSenderID = sess.GetAttributeType(wfConsts.attributeEmailSender).AttributeID;
    wfConsts.attributeEmailID = sess.GetAttributeType(wfConsts.attributeEmail).AttributeID;
    wfConsts.objtypeEmailMessagesID = sess.GetObjectType(wfConsts.objtypeEmailMessages).ObjectType;
    wfConsts.GlobalVariablesGroupID = sess.GetAttributesGroup(wfConsts.GlobalVariablesGroupGuid).GroupID;
    wfConsts.ProtectedAttributeTypes.Add(wfConsts.AttrActivityMessageID);
    wfConsts.ObjectOwnerGroupID = sess.GetObject(new Guid("cad00059-306c-11d8-b4e9-00304f19f545")).ObjectID;
    wfConsts.SystemUserID = sess.GetObject(new Guid("cad0000d-306c-11d8-b4e9-00304f19f545")).ObjectID;
    wfConsts.ProductionLCLevelID = sess.GetLifecycleLevel(wfConsts.ProductionLCLevelGuid).LevelID;
    wfConsts.SystemVariables = new List<int>((IEnumerable<int>) new int[2]
    {
      wfConsts.SysVarStarterID,
      wfConsts.SysVarSenderID
    });
    wfConsts.MessageTypeIDs.Add(wfConsts.WorkOfferTypeID);
    foreach (DataRow row in (InternalDataCollectionBase) sess.GetObjectTypeCollection(wfConsts.WorkOfferTypeID).SelectRecursive("").Rows)
    {
      if (Convert.ToInt32(row["F_VERSIONABLE"]) > 0)
        wfConsts.MessageTypeIDs.Add(Convert.ToInt32(row["F_OBJECT_TYPE"]));
    }
  }

  private static void FillECOTypes(IUserSession sess)
  {
    wfConsts.ECOTypes.Clear();
    IDBObjectType objectType = sess.GetObjectType(new Guid("cad00348-306c-11d8-b4e9-00304f19f545"));
    foreach (DataRow row in (InternalDataCollectionBase) sess.GetObjectTypeCollection(objectType.ObjectType).SelectRecursive("").Rows)
      wfConsts.ECOTypes.Add(Convert.ToInt32(row["F_OBJECT_TYPE"]));
  }

  public static bool IsECO(int typeid) => wfConsts.ECOTypes.IndexOf(typeid) != -1;

  public static bool IsSystemVariable(int typeID) => wfConsts.SystemVariables.IndexOf(typeID) != -1;

  /// <summary>
  /// Проверить является ли действие, действием с исполнителем
  /// </summary>
  /// <param name="kind"></param>
  /// <returns></returns>
  public static bool IsParticipantActivity(ActivityKind kind)
  {
    return wfConsts.ParticipantActivityKinds.Contains(kind);
  }

  /// <summary>Проверить является ли тип сообщением воркфлоу</summary>
  /// <param name="typeID"></param>
  /// <returns></returns>
  public static bool IsWorkflowMessage(int typeID)
  {
    return typeID == wfConsts.WorkOfferTypeID || typeID == wfConsts.MessageTypeID || wfConsts.IsActivity(typeID);
  }

  public static bool IsActivity(int typeID) => ActivityInfos.Types.ContainsKey(typeID);

  public static bool IsMessage(int typeID) => wfConsts.MessageTypeIDs.Contains(typeID);

  public static ActivityKind IntToActivityKind(int kind) => (ActivityKind) kind;

  public static int[] TermActivityTypes
  {
    get
    {
      if (wfConsts._termActivityTypes == null)
        wfConsts._termActivityTypes = new int[2]
        {
          wfConsts.TaskTypeID,
          wfConsts.ApproveTypeID
        };
      return wfConsts._termActivityTypes;
    }
  }

  public static List<int> MailObjectTypes
  {
    get
    {
      if (wfConsts._mailObjectTypes == null)
      {
        List<ActivityKind> activityKindList = new List<ActivityKind>();
        activityKindList.AddRange((IEnumerable<ActivityKind>) wfConsts.ParticipantActivityKinds);
        activityKindList.AddRange((IEnumerable<ActivityKind>) wfConsts.MayBeParticipantActivityKinds);
        wfConsts._mailObjectTypes = new List<int>();
        foreach (ActivityKind kind in activityKindList)
        {
          ActivityInfo byKind = ActivityInfos.FindByKind(kind);
          if (byKind != null && !wfConsts._mailObjectTypes.Contains(byKind.Type))
            wfConsts._mailObjectTypes.Add(byKind.Type);
        }
        wfConsts._mailObjectTypes.Add(wfConsts.WorkOfferTypeID);
        wfConsts._mailObjectTypes.Add(wfConsts.MessageTypeID);
      }
      return wfConsts._mailObjectTypes;
    }
  }
}
