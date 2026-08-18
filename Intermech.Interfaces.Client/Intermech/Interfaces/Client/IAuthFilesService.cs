// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IAuthFilesService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Navigator.Interfaces;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client;

public interface IAuthFilesService
{
  /// <summary>
  /// Событие назначения аутентичных файлов на версию объекта
  /// </summary>
  event AuthFileAssignEventHandler AuthFileAssignEvent;

  /// <summary>
  /// Событие определения необходимости назначения аутентичных файлов на версию объекта
  /// </summary>
  event AuthFileNeedGenerateEventHandler AuthFileNeedGenerate;

  /// <summary>Запустить событие AuthFileAssignEvent</summary>
  /// <param name="eventArgs"></param>
  void FireEventAuthFileAssign(AuthFileAssignEventArgs eventArgs);

  /// <summary>Запустить событие AuthFileNeedGenerate</summary>
  /// <param name="eventArgs"></param>
  void FireAuthFileNeedGenerate(AuthFileNeedGenerateEventArgs eventArgs);

  /// <summary>
  /// Проверить аутентичные файлы и при необходимости перегенерировать
  /// </summary>
  /// <param name="items">Список объектов</param>
  /// <param name="askForNotInternals">Задавать ли вопросы в режиме обновления, надо ли обновлять аутентичные файлы для не внутренних документов IPS. Вопросы не задаются для внутренних документов в любом режиме, а также для не внутренних в режиме создания)</param>
  /// <param name="updateMode">Режим: false: перегенерировать в любом случае (режим создания), или true: проверять необходимость создания файлов (режим обновления)</param>
  /// <returns>true - проверка выполнена; false - результаты проверки не гарантируют актуальности аутентичных файлов (напр, пользователь не подтвердил необходимость перегенерации аутентичных файлов)</returns>
  bool CheckAuthFiles(ISelectedItems items, bool updateMode, bool askForNotInternals = true);

  /// <summary>
  /// Сохранить аутентичные файлы в папку.
  /// Проверка на актуальность не производится.
  /// Для проверки на актуальность вызывать CheckAuthFiles
  /// </summary>
  /// <param name="items">Список объектов</param>
  /// <param name="folderPath">Папка для сохранения, должна существовать</param>
  /// <param name="onAuthFileReplace">При null возможны коллизии, если папка была не пуста</param>
  void SaveAuthFiles(
    ISelectedItems items,
    string folderPath,
    AuthFileSaveNameResolveHandler onAuthFileNameResolve);

  List<string> GetPossibleAuthFileTypes();

  /// <summary>Назначить в версию объекта аутентичные файлы явно</summary>
  /// <param name="objectId">идент. версии объекта</param>
  /// <param name="authFiles">список аутентичных файлов для назначения</param>
  /// <param name="onAuthFileReplace">событие при необходимости запроса на перезапись, при null перезаписывать</param>
  /// <returns></returns>
  bool AssignFileWithFilenames(
    long objectId,
    string[] authFiles,
    AuthFileReplaceEventHandler onAuthFileReplace);
}
