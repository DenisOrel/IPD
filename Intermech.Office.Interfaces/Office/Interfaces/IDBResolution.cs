// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Interfaces.IDBResolution
// Assembly: Intermech.Office.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9492297C-4143-4944-80A1-CEF9501FC1B8
// Assembly location: D:\IPS\Client\Intermech.Office.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Office.Interfaces.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Office.Interfaces;

/// <summary>Интерфейс поручения</summary>
public interface IDBResolution : IDBObject, IDBAttributable, IDBSessionable, IPluginsData
{
  /// <summary>Наименование поручения</summary>
  [NotNull]
  string Name { get; set; }

  /// <summary>Идентификатор автора поручения (редактируется вручную, не тоже самое, что создатель поручения)</summary>
  long AuthorID { get; set; }

  /// <summary>Является ли поручение контрольным</summary>
  bool IsControlResolution { get; set; }

  /// <summary>Идентификатор контролёра</summary>
  long ControllerID { get; set; }

  /// <summary>Идентификаторы исполнителей</summary>
  [NotNull]
  long[] ExecutorIDs { get; set; }

  /// <summary>Идентификатор версии канцелярского документа, по которому выпущено поручение</summary>
  long OfficeDocumentObjVerID { get; }

  /// <summary>Является ли поручение конфиденциальным</summary>
  bool IsPrivate { get; }

  /// <summary>Записи об отчётах о прогрессе исполнения поручения</summary>
  [NotNull]
  ResolutionProgressReportRecord[] ProgressReportRecords { get; }

  /// <summary>Дата регистрации</summary>
  DateTime RegistrationDate { get; set; }

  /// <summary>Атрибут "Исполнение поручения" значение которого определяет как будет исполняться поручение - последовательно или параллельно</summary>
  ResolutionExecution ResolutionExecutionType { get; }

  /// <summary>Идентификатор отвечающего исполнителя</summary>
  long ResponseUserID { get; set; }

  /// <summary>Плановая дата исполнения</summary>
  DateTime PlannedDate { get; set; }

  /// <summary>Текст поручения</summary>
  [NotNull]
  string ResolutionText { get; set; }

  /// <summary>Фактическая дата исполнения</summary>
  DateTime ActualDate { get; set; }

  /// <summary>Дата контроля</summary>
  DateTime ControlDate { get; }

  /// <summary>Информация о канцелярской входимости поручения</summary>
  [CanBeNull]
  ResolutionContextInfo ContextInfo { get; }

  /// <summary>Проверка, приходится ли пользователь кем-то для поручения</summary>
  /// <param name="userID">Идентификатор пользователя</param>
  /// <param name="resolutionUserRoles">Список ролей, принадлежность которым проверяется</param>
  /// <returns>true если пользователь выступает в любой из переданных ролей, иначе false</returns>
  bool IsUserAnyOfRoles(long userID, ResolutionUserRoles resolutionUserRoles);

  /// <summary>Проверка, приходится ли текущий пользователь кем-то для поручения</summary>
  /// <param name="resolutionUserRoles">Список ролей, принадлежность которым проверяется</param>
  /// <returns>true если текущий пользователь выступает в любой из переданных ролей, иначе false</returns>
  bool IsUserAnyOfRoles(ResolutionUserRoles resolutionUserRoles);

  /// <summary>Рассылка поручения по соотв. ему маршруту workflow</summary>
  /// <remarks>Вызов имеет смысл только для незапущенных ещё поручений, находящихся на этапе ЖЦ "Создание", например которые были созданы с атрибутом "Отложенное поручение"=True</remarks>
  /// <exception cref="T:System.Exception">Thrown when an exception error condition occurs.</exception>
  void Run();
}
