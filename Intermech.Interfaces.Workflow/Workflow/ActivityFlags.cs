// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.ActivityFlags
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;

#nullable disable
namespace Intermech.Workflow;

[Flags]
public enum ActivityFlags
{
  /// <summary>Запрет удаления действия исполнителями из почты</summary>
  DenyDeletionFromMail = 1,
  /// <summary>Запретить получателю прикреплять вложения</summary>
  DenyAttach = 2,
  /// <summary>Запретить получателю откреплять вложения</summary>
  DenyDetach = 4,
  /// <summary>
  /// Откреплять успешно зарегистрированные объекты. Используется в "Регистрации"
  /// </summary>
  DetachRegisteredObjects = 8,
  /// <summary>
  /// Фильтровать вложенные объекты в зависимости от условий. Используется в "Множественно-условном переходе"
  /// </summary>
  FilterObjects = 16, // 0x00000010
  /// <summary>Требовать заполнение ответа при отправке назад</summary>
  RequireAnswerText = 32, // 0x00000020
  /// <summary>
  /// Сроки начинают отсчитываться тогда, когда каждому из потенциальных исполнителей рассылаются предложения взять действие в работу
  /// </summary>
  StartTermsWithWorkOffers = 64, // 0x00000040
  /// <summary>Флаг что мы уже проверили подписи</summary>
  SignsChecked = 128, // 0x00000080
  StartTermsAcceptWorkOffer = 256, // 0x00000100
  /// <summary>
  /// Не контролировать запрет прикреплений/откреплений для административной сессии
  /// </summary>
  AllowAdminAttach = 512, // 0x00000200
  /// <summary>
  /// Не контролировать запрет прикреплений/откреплений для системной сессии
  /// </summary>
  AllowSystemAttach = 1024, // 0x00000400
  [RealtimeFlag] InheritVars = 32768, // 0x00008000
  /// <summary>
  /// Позволяет в сценариях определить, в каком направлении выполняется действие. Если флаг установлен, производится возврат назад
  /// </summary>
  [RealtimeFlag] Rollback = 65536, // 0x00010000
  /// <summary>Установлен в случае выполнения сценария на сервере</summary>
  [RealtimeFlag] ServerScript = 131072, // 0x00020000
  /// <summary>
  /// Установлен в случае выполнения сценария до выполнения действия
  /// </summary>
  [RealtimeFlag] BeforeExec = 262144, // 0x00040000
  /// <summary>
  /// Установлен в случае выполнения сценария после выполнения действия
  /// </summary>
  [RealtimeFlag] AfterExec = 524288, // 0x00080000
  /// <summary>Установлен, если в данный момент действие отзывается</summary>
  Recalling = 1048576, // 0x00100000
  /// <summary>
  /// Указывает, что в данный момент действие импортируется из файла экспорта
  /// </summary>
  [RealtimeFlag] Importing = 2097152, // 0x00200000
}
