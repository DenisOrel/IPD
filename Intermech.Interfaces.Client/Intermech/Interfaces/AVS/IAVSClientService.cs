// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.IAVSClientService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary>Клиентский сервис AVS</summary>
public interface IAVSClientService
{
  /// <summary>Идентификатор активной спецификации</summary>
  long ActiveDocumentId { get; }

  /// <summary>Спецификация нуждается в обновлении</summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="objectGuid">Глобальный идентификатор версии объекта</param>
  /// <param name="reasonList">Возвращает сообщения причинах необходимости обновления</param>
  /// <returns></returns>
  bool SpecificationIsNeedUpdate(
    long objectID,
    int objectType,
    Guid objectGuid,
    out List<string> reasonList);

  /// <summary>Открыть конструкторский документ в редакторе AVS</summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="saveAfterUpdate">Сохранить файл документа после обновления при открытии</param>
  /// <returns></returns>
  object EditAVSDocument(long objectID, int objectType, bool saveAfterUpdate, bool checkIfCanEdit);

  /// <summary>Получить документ для просмотра</summary>
  /// <param name="objectID">Идентификатор версии СП</param>
  /// <param name="objectType">Тип СП</param>
  /// <returns>ImDocument, если требуется обновление документа, иначе null</returns>
  object GetViewDocument(long objectID, int objectType);

  /// <summary>Активное окно является окном со спецификацией</summary>
  bool ActiveWindowIsSpecification { get; }

  /// <summary>Добавить внешнюю команду в AVS.
  /// Новые пункты меню добавляются в меню "Документ"</summary>
  /// <param name="externalAVSCommand">Команда</param>
  void AddExternalAVSCommand(ExternalAVSCommand externalAVSCommand);

  /// <summary>Удалить внешнюю команду из AVS</summary>
  /// <param name="name">Имя команды</param>
  void RemoveExternalAVSCommand(string name);

  /// <summary>Получить внешнюю команду AVS</summary>
  /// <param name="name"></param>
  /// <returns>Возвращает класс с именем и обработчиком команды. Если команды нет, то метод возвращает null</returns>
  ExternalAVSCommand GetExternalAVSCommand(string name);

  /// <summary>Поддерживается ли тип объекта БД как документ AVS</summary>
  /// <param name="dbObjectTypeID">Идентификатор типа объекта БД</param>
  bool IsAVSDocumentSupportedType(int dbObjectTypeID);

  /// <summary>
  /// Событие, возникающего перед CommitCreation для заготовки
  /// </summary>
  event BeforeCommitCreationAVSDocumentEventHandler BeforeCommitCreationAVSDocumentEvent;

  /// <summary>
  /// Метод вызывается по нажатию на кнопку готово перед коммитом для нового объекта (т.е. создаваемый объект еще заготовка)
  /// </summary>
  /// <param name="e">Заготовка</param>
  /// <returns></returns>
  void OnBeforeCommitCreationAVSDocument(BeforeCommitCreationAVSDocumentEventArgs e);
}
