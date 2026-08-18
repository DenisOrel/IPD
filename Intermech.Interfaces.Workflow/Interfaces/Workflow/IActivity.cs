// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.IActivity
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Workflow;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Workflow;

public interface IActivity : 
  IMailObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData,
  IDBSecurityCollection,
  IDBSecurity
{
  /// <summary>Название действия</summary>
  string Name { get; set; }

  /// <summary>Описание действия</summary>
  string Description { get; set; }

  /// <summary>
  /// Процесс/шаблон процесса, которому принадлежит действие
  /// </summary>
  IScheme Process { get; }

  /// <summary>Список переменных</summary>
  IVariables Variables { get; }

  /// <summary>Вложения</summary>
  IAttachments Attachments { get; }

  /// <summary>
  /// Используется для сигнализации, что какие-то свойства изменились на клиенте и их нужно перечитать в серверном объекте.
  /// </summary>
  /// <param name="flag">Что изменилось</param>
  void Changed(ActivityChanged flag);

  /// <summary>
  /// Используется для сигнализации, что какие-то свойства изменились на клиенте и их нужно перечитать в серверном объекте.
  /// </summary>
  /// <param name="flag">Что изменилось</param>
  void Changed(ActivityChanged flag, object tag);

  /// <summary>Статус действия</summary>
  ActivityStatus Status { get; }

  /// <summary>
  /// Возвращает истину, если действие находится в одном из выполняющихся статусов
  /// </summary>
  bool Executed { get; }

  /// <summary>Сообщение на текущем шаге</summary>
  string MessageText { get; set; }

  /// <summary>Тип действия</summary>
  ActivityKind Kind { get; }

  /// <summary>Флаги</summary>
  ActivityFlags Flags { get; set; }

  /// <summary>Вид возврата</summary>
  RollbackKind RollbackKind { get; }

  /// <summary>Приоритет</summary>
  ProcessPriority Priority { get; set; }

  /// <summary>
  /// Исполнитель текущего действия (идентификатор версии объекта пользователя в системе)
  /// </summary>
  long ParticipantID { get; }

  /// <summary>
  /// Идентификатор пользовательской формы, назначенной на действие
  /// </summary>
  long FormID { get; }

  /// <summary>Список глобальных переменных у шаблона</summary>
  IVariables GlobalVariables { get; }

  /// <summary>
  /// Проверка валидности действия (все ли основные свойства заполнены)
  /// </summary>
  /// <returns>Строку с ошибками, если имеются.</returns>
  string Validate(bool checkSubProcessSchemes = true, List<long> checkedSchemesList = null);

  /// <summary>
  /// Проверка валидности действия (все ли основные свойства заполнены)
  /// </summary>
  /// <returns></returns>
  bool IsValid();
}
