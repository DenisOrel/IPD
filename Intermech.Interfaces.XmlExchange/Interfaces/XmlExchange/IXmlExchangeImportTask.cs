// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.IXmlExchangeImportTask
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using Intermech.Diagnostics;
using Intermech.Interfaces.XmlExchange.Services.Import;
using System;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>Задача импорта данных из XML</summary>
public interface IXmlExchangeImportTask : IXmlExchangeTask, IDisposable
{
  /// <summary>
  /// Протокол выполненных действий (очищается после чтения)
  /// </summary>
  string Log { get; }

  /// <summary>Есть ли ошибка в процессе работы</summary>
  bool HasError { get; }

  /// <summary>
  /// Исключение, которое возникло в процессе работы задания
  /// </summary>
  Exception Exception { get; }

  /// <summary>
  /// Загрузить на сервер файл во временное хранилище.
  /// Метод можно вызывать однократно для небольших файлов, либо
  /// многократно для больших
  /// </summary>
  /// <param name="fileName">Имя временного файла (серверное)</param>
  /// <param name="buffer">Порция данных</param>
  /// <param name="bufferSize">Размер порции данных</param>
  /// <param name="append">true - файл будет дописан, если существует</param>
  /// <returns>true - действие выполнено успешно</returns>
  bool UploadData(string fileName, byte[] buffer, int bufferSize, bool append);

  /// <summary>Вызов импорта пакета данных</summary>
  /// <returns></returns>
  bool Execute([NotNull] XmlExchangeImportTaskParams importParams);

  /// <summary>
  /// Загрузить на сервер файл XML во временное хранилище.
  /// Метод можно вызывать однократно для небольших файлов, либо
  /// многократно для больших
  /// </summary>
  /// <param name="fileName">Имя временного файла (серверное)</param>
  /// <param name="buff">Порция данных</param>
  /// <param name="size">Размер порции данных</param>
  /// <param name="append">true - файл будет дописан, если существует</param>
  /// <returns>true - действие выполнено успешно</returns>
  [Obsolete("Use UploadData method instead", true)]
  bool UploadXML(string fileName, byte[] buff, int size, bool append);

  /// <summary>Выбрать конфигурацию импорта</summary>
  /// <param name="cfgID">Идентификатор версии объекта с конфигурацией импорта</param>
  /// <returns>true - действие выполнено успешно</returns>
  [Obsolete("Use Execute method instead", true)]
  bool SelectCfg(long cfgID);

  /// <summary>
  /// Обработать ZIP-архив, считать его метаданные, получить список файлов
  /// </summary>
  /// <returns>true - задание успешно выполнено</returns>
  [Obsolete("Use Execute method instead", true)]
  bool ParseZIP();

  /// <summary>Задача поиска существующих объектов</summary>
  /// <returns>true - задание успешно выполнено</returns>
  [Obsolete("Use Execute method instead", true)]
  bool ImportObjects();

  /// <summary>Задача по импорту объектов Imbase</summary>
  /// <returns>true - задание успешно выполнено</returns>
  [Obsolete("Use Execute method instead", true)]
  bool ImportImbaseObjects();

  /// <summary>Импортировать связи</summary>
  /// <returns>true - задание успешно выполнено</returns>
  [Obsolete("Use Execute method instead", true)]
  bool ImportRelations();
}
