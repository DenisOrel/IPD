// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.Email.IEmailDownloadService
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Workflow.Email;

public interface IEmailDownloadService
{
  /// <summary>Запуск задачи приема почты</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="processID">Уникальный идентификатор задачи приема почты</param>
  /// <param name="accauntEmal">Email</param>
  /// <param name="removeMessages">Удалить письма из папки Входящие</param>
  void StartDownload(Guid sessionGuid, Guid processID, string accauntEmal, bool removeMessages);

  /// <summary>Получить свойства задачи приема почты</summary>
  /// <param name="processID">Уникальный идентификатор задачи приема почты</param>
  /// <returns></returns>
  EmailDownloadProperties GetDownloadProperties(Guid processID);

  /// <summary>Завершение задачи приема почты</summary>
  /// <param name="processID">Уникальный идентификатор задачи приема почты</param>
  void CompleteDownload(Guid processID);

  /// <summary>Остановить прием почты</summary>
  /// <param name="processID">Уникальный идентификатор задачи приема почты</param>
  void StopDownload(Guid processID);

  /// <summary>Перечитать настройки приема почты</summary>
  void ReloadSettings();
}
