// Decompiled with JetBrains decompiler
// Type: Intermech.IpsXmlViewer.Interfaces.IIndexer
// Assembly: Intermech.IpsXmlViewer.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35FFF223-7A37-420F-9D15-CF4A93D8C384
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.xml

using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.IpsXmlViewer.Interfaces;

/// <summary>
/// Интерфейс, позволяющий сформировать базу данных индекса на основании
/// одного или нескольких потоков, содержащих XML
/// </summary>
public interface IIndexer
{
  /// <summary>Контейнер сервисов</summary>
  IServiceProvider Services { get; }

  /// <summary>База данных SQLite</summary>
  object SQLConnection { get; }

  /// <summary>Метаданные</summary>
  IImMetaData MetaData { get; set; }

  /// <summary>
  /// Коллекция таблиц базы данных, а также имена их колонок (все названия - в верхнем регистре)
  /// </summary>
  IDictionary<string, IList<string>> Tables { get; }

  /// <summary>Количество объектов</summary>
  long Objects { get; }

  /// <summary>Количество связей</summary>
  long Relations { get; }

  /// <summary>Количество типов атрибутов</summary>
  int AttributeTypes { get; }

  /// <summary>Количество типов объектов</summary>
  int ObjectTypes { get; }

  /// <summary>Количество типов связей</summary>
  int RelationTypes { get; }

  /// <summary>Событие "Состояние индексатора"</summary>
  event IndexProgressEventHandler OnIndexProgress;

  /// <summary>
  /// Сформировать индексы в указанной базе данных из коллекции потоков
  /// </summary>
  /// <param name="kernel">Микроядро</param>
  /// <param name="xmlStreams">Коллекция потоков, содержащих XML</param>
  void ProcessStreams(IKernel kernel, params Stream[] xmlStreams);
}
