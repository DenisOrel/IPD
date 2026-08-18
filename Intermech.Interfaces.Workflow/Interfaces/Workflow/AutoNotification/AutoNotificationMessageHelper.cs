// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.AutoNotification.AutoNotificationMessageHelper
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

#nullable disable
namespace Intermech.Interfaces.Workflow.AutoNotification;

/// <summary>Хелпер для работы с текстом сообщений уведомлений</summary>
public class AutoNotificationMessageHelper
{
  /// <summary>Тело уведомления для событий отказа прав доступа.</summary>
  public static string AccessMessageBody = LocalizationHolder.rm.GetString("Interfaces.Workflow_14") + LocalizationHolder.rm.GetString("Interfaces.Workflow_15") + LocalizationHolder.rm.GetString("Interfaces.Workflow_10");
  /// <summary>
  /// Тело уведомления для событий, связанных с объектами (кроме выпуска версии)
  /// </summary>
  public static string ObjectMessageBody = LocalizationHolder.rm.GetString("Interfaces.Workflow_21") + LocalizationHolder.rm.GetString("Interfaces.Workflow_10");
  /// <summary>Тело уведомления для события выпуска версии</summary>
  public static string VersionMessageBody = LocalizationHolder.rm.GetString("Interfaces.Workflow_21") + LocalizationHolder.rm.GetString("Interfaces.Workflow_43") + LocalizationHolder.rm.GetString("Interfaces.Workflow_10");
  /// <summary>Тело уведомления для событий перехода на шаг ЖЦ</summary>
  public static string LCMessageBody = LocalizationHolder.rm.GetString("Interfaces.Workflow_21") + LocalizationHolder.rm.GetString("Interfaces.Workflow_16") + LocalizationHolder.rm.GetString("Interfaces.Workflow_10");
  /// <summary>
  /// Тело уведомления для событий перехода на уровень продвижения
  /// </summary>
  public static string LСLevelMessageBody = LocalizationHolder.rm.GetString("Interfaces.Workflow_21") + LocalizationHolder.rm.GetString("Interfaces.Workflow_17") + LocalizationHolder.rm.GetString("Interfaces.Workflow_10");
  /// <summary>
  /// Тело уведомления для событий, связанных с изменением атрибута объекта
  /// </summary>
  public static string AttrMessageBody = LocalizationHolder.rm.GetString("Interfaces.Workflow_21") + LocalizationHolder.rm.GetString("Interfaces.Workflow_11") + LocalizationHolder.rm.GetString("Interfaces.Workflow_13") + LocalizationHolder.rm.GetString("Interfaces.Workflow_10");
  /// <summary>Тело уведомления для событий, связанных со связями</summary>
  public static string RelationMessageBody = LocalizationHolder.rm.GetString("Interfaces.Workflow_19") + LocalizationHolder.rm.GetString("Interfaces.Workflow_20") + LocalizationHolder.rm.GetString("Interfaces.Workflow_22") + LocalizationHolder.rm.GetString("Interfaces.Workflow_10");
  /// <summary>[Заголовок объекта]</summary>
  public static string AttrCaption = LocalizationHolder.rm.GetString("Interfaces.Workflow_24");
  /// <summary>[Заголовок родительской версии объекта]</summary>
  public static string ParentVersionCaption = LocalizationHolder.rm.GetString("Interfaces.Workflow_44");
  /// <summary>%Пользователь%</summary>
  public static string User = LocalizationHolder.rm.GetString("Interfaces.Workflow_25");
  /// <summary>[Наим. измененного атрибута]</summary>
  public static string ChangedAttrName = LocalizationHolder.rm.GetString("Interfaces.Workflow_37");
  /// <summary>[Наим. атрибута]</summary>
  public static string AttrName = LocalizationHolder.rm.GetString("Interfaces.Workflow_26");
  /// <summary>%Старое значение%</summary>
  public static string OldAttrValue = LocalizationHolder.rm.GetString("Interfaces.Workflow_27");
  /// <summary>%Новое значение%</summary>
  public static string NewAttrValue = LocalizationHolder.rm.GetString("Interfaces.Workflow_28");
  /// <summary>%Текст уведомления об отказе в правах доступа%</summary>
  public static string NotificationTextAccessDenied = LocalizationHolder.rm.GetString("Interfaces.Workflow_14");
  /// <summary>%Тип проверки прав%</summary>
  public static string AccessType = LocalizationHolder.rm.GetString("Interfaces.Workflow_30");
  /// <summary>%Шаг жизненного цикла%</summary>
  public static string LCStep = LocalizationHolder.rm.GetString("Interfaces.Workflow_31");
  /// <summary>%Уровень продвижения%</summary>
  public static string LCLevel = LocalizationHolder.rm.GetString("Interfaces.Workflow_32");
  /// <summary>[Заголовок родительского объекта]</summary>
  public static string ProjAttrCaption = LocalizationHolder.rm.GetString("Interfaces.Workflow_33");
  /// <summary>[Заголовок дочернего объекта]</summary>
  public static string PartAttrCaption = LocalizationHolder.rm.GetString("Interfaces.Workflow_34");
  /// <summary>%Значение%</summary>
  public static string AttrValue = LocalizationHolder.rm.GetString("Interfaces.Workflow_35");
  /// <summary>%Тип связи%</summary>
  public static string RelTypeName = LocalizationHolder.rm.GetString("Interfaces.Workflow_38");
  /// <summary>Строка ссылки на объект</summary>
  public static string ObjectLink = LocalizationHolder.rm.GetString("Interfaces.Workflow_36");
}
