// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.XmlImportExtAction
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using System;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>Действия, которые может выполнять расширение импорта</summary>
[Flags]
[Serializable]
public enum XmlImportExtAction : long
{
  /// <summary>
  /// Действие вызывается после загрузки конфигурации импорта
  /// </summary>
  ImLoadConfigData = 1,
  /// <summary>Синхронизация метаданных</summary>
  ImSyncMetaData = 2,
  /// <summary>
  /// Произвольная обработка индекса XML после его формирования
  /// (вызывается после ImSyncMetaData)
  /// </summary>
  IndexPostProcess = 16, // 0x0000000000000010
  /// <summary>
  /// Действие вызывается перед началом импорта объектов в базу IPS
  /// </summary>
  ImBeforeImportObjects = 256, // 0x0000000000000100
  /// <summary>
  /// Действие вызывается перед началом импорта объекта в базу IPS
  /// </summary>
  ImBeforeImportObject = 512, // 0x0000000000000200
  /// <summary>
  /// Действие вызывается для обработки правил импорта и создания объектов
  /// </summary>
  ImProcessImportCreateRule = 1024, // 0x0000000000000400
  /// <summary>Действие вызывается при поиске объектов</summary>
  /// <remarks>Вызывается перед поиском по правилам в конфигурации. Если заполняется IPS.IDBObject - поиск по
  /// правилам не производиться</remarks>
  ImProcessSearchObject = 2048, // 0x0000000000000800
  /// <summary>Обработка/преобразование записей типа ObjectRecord</summary>
  ImObjectConvertion = 4096, // 0x0000000000001000
  /// <summary>
  /// Связь объектов индекса XML с объектами базы данных IPS
  /// </summary>
  ImObjectLinkWithIPS = 8192, // 0x0000000000002000
  /// <summary>
  /// Заполнение структуры объектов для импорта данных в базу IPS
  /// </summary>
  ImObjectFillImportData = 16384, // 0x0000000000004000
  /// <summary>
  /// Получение типов объектов из конфигурации импорта и базы данных IPS
  /// </summary>
  ImObjectGetType = 65536, // 0x0000000000010000
  /// <summary>Поиск/создание Guid для записей типа IImObject</summary>
  ImObjectGetGuid = 131072, // 0x0000000000020000
  /// <summary>
  /// Поиск владельцев и редакторов для записей типа IImObject
  /// </summary>
  ImObjectGetOwnerID = 262144, // 0x0000000000040000
  /// <summary>Поиск шага ЖЦ / уровня продвижения</summary>
  ImObjectGetLcStep = 524288, // 0x0000000000080000
  /// <summary>
  /// Обработка коллекции описаний атрибутов IImAttribute для записей типа IImObject
  /// </summary>
  ImObjectImAttributesConvertion = 1048576, // 0x0000000000100000
  /// <summary>
  /// Обработка структуры объекта с заполненными данными (атрибутами) для записей типа IImObject перед импортом в базу
  /// </summary>
  ImObjectBeforeImportData = 2097152, // 0x0000000000200000
  /// <summary>Действие вызывается после импорта объекта в базу IPS</summary>
  ImAfterImportObject = 16777216, // 0x0000000001000000
  /// <summary>Действие вызывается после импорта объектов в базу IPS</summary>
  ImAfterImportObjects = 33554432, // 0x0000000002000000
  /// <summary>
  /// Действие вызывается перед началом импорта связей в базу IPS
  /// </summary>
  ImBeforeImportRelations = 4294967296, // 0x0000000100000000
  /// <summary>
  /// Действие вызывается перед началом импорта связи в базу IPS
  /// </summary>
  ImBeforeImportRelation = 8589934592, // 0x0000000200000000
  /// <summary>Обработка/преобразование записей типа IImRelation</summary>
  ImRelationConvertion = 68719476736, // 0x0000001000000000
  /// <summary>
  /// Получение типов связей из конфигурации импорта и базы данных IPS
  /// </summary>
  ImRelationGetType = 137438953472, // 0x0000002000000000
  /// <summary>
  /// Обработка коллекции описаний атрибутов IImAttribute для записей типа IImRelation
  /// </summary>
  ImRelationImAttributesConvertion = 1099511627776, // 0x0000010000000000
  /// <summary>
  /// Обработка структуры объекта с заполненными данными (атрибутами) для записей типа IImRelation перед импортом в базу
  /// </summary>
  ImRelationBeforeImportData = 4398046511104, // 0x0000040000000000
  /// <summary>Действие вызывается после импорта связи в базу IPS</summary>
  ImAfterImportRelation = 17592186044416, // 0x0000100000000000
  /// <summary>Действие вызывается после импорта связей в базу IPS</summary>
  ImAfterImportRelations = ImAfterImportRelation, // 0x0000100000000000
  /// <summary>Произвольная обработка после завершения задачи</summary>
  TaskPostProcess = 72057594037927936, // 0x0100000000000000
  /// <summary>Задание по поиску существующих объектов</summary>
  ObjectSearchAction = 1152921504606846976, // 0x1000000000000000
  /// <summary>Задание по созданию новых объектов</summary>
  ObjectCreateAction = 2305843009213693952, // 0x2000000000000000
}
