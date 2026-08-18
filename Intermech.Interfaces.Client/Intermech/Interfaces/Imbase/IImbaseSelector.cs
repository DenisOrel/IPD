// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.IImbaseSelector
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces.Client;
using Intermech.Interfaces.Client.Imbase;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.Interfaces.Imbase;

/// <summary>Сервис для выбора объектов из IMBASE.</summary>
public interface IImbaseSelector
{
  /// <summary>
  /// Получение конвертера для атрибутов у которых выставлен флаг AttributeOptions.ImbaseFlag_TableRecordRef.
  /// </summary>
  /// <returns>Конвертер атрибута</returns>
  TypeConverter GetConverterForTableRecordRefFlag();

  /// <summary>
  /// Получение редактора для атрибутов, у которых выставлен флаг AttributeOptions.ImbaseFlag_TableRecordRef.
  /// </summary>
  /// <returns>Редактор атрибута</returns>
  UITypeEditor GetEditorForTableRecordRefFlag();

  /// <summary>
  /// Получение описателя атрибутов, у которых выставлен флаг AttributeOptions.ImbaseFlag_TableRecordRef, для применения в службе подписки на редактирование атрибутов в формах и карточках объектов
  /// </summary>
  IAttributePropertyDescriber GetDescriberForTableRecordRefFlag();

  /// <summary>Выбирает один объект из указанного Каталога.</summary>
  /// <param name="caption">Заголовок окна при выборе</param>
  /// <param name="description"></param>
  /// <param name="catalogId">Идентификатор Каталога ( Guid, Идентификатор версии(Int64) или имя (string))</param>
  /// <param name="rawObject">Указывает, создавать ли объект нового типа(false) или вернуть сам выбранный объект(true)</param>
  /// <param name="commitCreation">Указывает создавать ли объект в базе (true) или возвращать заготовку (возвращает отрицательный objectId)</param>
  /// <param name="allowedTypes">Список базовых объектов IMBASE, которые могут быть выбраны</param>
  /// <param name="needType">Тип создаваемого объекта или -1 для типа, определяемого по атрибутам</param>
  /// <param name="contextObjsID">Идентификатор изделия</param>
  /// <param name="selectedItemsAnalyzer">Кастомный анализатор, передается когда работа стандартного анализатора противоречит логике</param>
  /// <returns>Идентификатор выбранного объекта или -1 при отмене выбора</returns>
  long SelectFromCatalog(
    string caption,
    string description,
    object catalogId,
    bool rawObject,
    bool commitCreation,
    int[] allowedTypes,
    int needType,
    long contextObjsID,
    object selectedItemsAnalyzer = null);

  /// <summary>Выбирает один объект из указанного Каталога.</summary>
  /// <param name="caption">Заголовок окна при выборе</param>
  /// <param name="description"></param>
  /// <param name="catalogId">Идентификатор Каталога ( Guid, Идентификатор версии(Int64) или имя (string))</param>
  /// <param name="rawObject">Указывает, создавать ли объект нового типа(false) или вернуть сам выбранный объект(true)</param>
  /// <param name="commitCreation">Указывает создавать ли объект в базе (true) или возвращать заготовку (возвращает отрицательный objectId)</param>
  /// <param name="allowedTypes">Список базовых объектов IMBASE, которые могут быть выбраны</param>
  /// <param name="needType">Тип создаваемого объекта или -1 для типа, определяемого по атрибутам</param>
  /// <returns>Идентификатор выбранного объекта или -1 при отмене выбора</returns>
  long SelectFromCatalog(
    string caption,
    string description,
    object catalogId,
    bool rawObject,
    bool commitCreation,
    int[] allowedTypes,
    int needType);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="caption"></param>
  /// <param name="description"></param>
  /// <param name="catalogObject"></param>
  /// <param name="needType"></param>
  /// <param name="contextObjsID">Идентификатор изделия</param>
  /// <returns></returns>
  long SelectFromCatalog(
    string caption,
    string description,
    object catalogObject,
    int needType,
    long contextObjsID);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="catalogObject"></param>
  /// <param name="selectedID"></param>
  /// <returns></returns>
  long SelectFromCatalog(object catalogObject, long selectedID);

  /// <summary>Выбор из указанного Каталога одного объекта</summary>
  /// <param name="selectorParams"></param>
  /// <returns></returns>
  long SelectFromCatalog(ImbaseSelectorParams selectorParams);

  /// <summary>
  /// Создать объект на основе выбранной записи каталога IMBASE
  /// (используется при заполнении атрибута материал)
  /// </summary>
  /// <param name="catalogObject">выбранный каталог</param>
  /// <param name="selectedID">и запись в каталоге</param>
  /// <returns> id созданного материала</returns>
  long CreateFromCatalog(object catalogObject, long selectedID);

  /// <summary>Выбирает запись IMBASE.</summary>
  /// <param name="caption">Заголовок окна при выборе</param>
  /// <param name="description"></param>
  /// <param name="contextObjsID">Идентификатор объекта</param>
  /// <returns>Идентификатор ссылки а таблицу IMBASE и номер записи или null при отмене выбора</returns>
  /// <remarks>Если передается contextObjsID, то у объекта получаем объект IMBASE, на который он ссылается, и позиционируемся на этом объекте</remarks>
  Tuple<long, long> SelectRecord(string caption, string description, long contextObjsID);

  /// <summary>Выбирает запись IMBASE.</summary>
  /// <param name="imbaseObjID">Идентификатор объекта IMBASE (каталога или папки)</param>
  /// <param name="caption">Заголовок окна при выборе</param>
  /// <param name="description">Описание</param>
  /// <returns>Идентификатор ссылки а таблицу IMBASE и номер записи или null при отмене выбора</returns>
  Tuple<long, long> SelectRecord(long imbaseObjID, string caption, string description);

  /// <summary>
  /// Динамический выбор из IMBASE без накопления результатов выбора. При выборе объекта
  /// он обрабатывается ядром и в делегат передается идентификатор выбранного объекта.
  /// </summary>
  /// <param name="caption">Заголовок окна при выборе</param>
  /// <param name="description"></param>
  /// <param name="catalogId">Идентификатор Каталога ( Guid, Идентификатор версии(Int64) или имя (string))</param>
  /// <param name="rawObject">Указывает, создавать ли объект нового типа(false) или вернуть сам выбранный объект(true)</param>
  /// <param name="commitCreation">Указывает создавать ли объект в базе (true) или возвращать заготовку (возвращает отрицательный objectId)</param>
  /// <param name="needType">Тип создаваемого объекта или -1 для типа, определяемого по атрибутам</param>
  /// <param name="dynamicSelection">Делегат, который принимает события о выборе объекта. Ес</param>
  /// <param name="contextObjsID">Идентификатор изделия</param>
  /// <returns>Возвращает массив идентификаторов выбранных объектов</returns>
  long[] DynamicSelection(
    string caption,
    string description,
    object catalogId,
    bool rawObject,
    bool commitCreation,
    int needType,
    DynamicSelectionEventHandler dynamicSelection,
    long contextObjsID);

  /// <summary>
  /// Динамический выбор из IMBASE без накопления результатов выбора. При выборе объекта
  /// он обрабатывается ядром и в делегат передается идентификатор выбранного объекта.
  /// </summary>
  /// <param name="caption">Заголовок окна при выборе</param>
  /// <param name="description"></param>
  /// <param name="catalogId">Идентификатор Каталога ( Guid, Идентификатор версии(Int64) или имя (string))</param>
  /// <param name="rawObject">Указывает, создавать ли объект нового типа(false) или вернуть сам выбранный объект(true)</param>
  /// <param name="commitCreation">Указывает создавать ли объект в базе (true) или возвращать заготовку (возвращает отрицательный objectId)</param>
  /// <param name="needType">Тип создаваемого объекта или -1 для типа, определяемого по атрибутам</param>
  /// <param name="dynamicSelection">Делегат, который принимает события о выборе объекта. Ес</param>
  /// <returns>Возвращает массив идентификаторов выбранных объектов</returns>
  long[] DynamicSelection(
    string caption,
    string description,
    object catalogId,
    bool rawObject,
    bool commitCreation,
    int needType,
    DynamicSelectionEventHandler dynamicSelection);

  /// <summary>
  /// Идентификатор объекта-контекста выбора ( ID таблицы или ссылки на таблицу). Используется при выборе записи таблицы.
  /// </summary>
  long ContextObjectId { get; set; }

  long RecordId { get; set; }

  /// <summary>
  /// Возвращает список каталогов, которым назначен через атрибут
  /// Imbase.Consts.ObjectTypeAndAttCatalogLinkID :" Привязка к типу объекта, атрибуту"
  /// список пар вида [тип объекта:тип атрибута].
  /// </summary>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="attType">Тип атрибута</param>
  /// <returns>Список идентификаторов объектов типа Каталог IMBASE</returns>
  long[] CatalogsForObjectAtt(int objectType, int attType);

  /// <summary>
  /// Get all object types + attributes links to imbase catalogs
  /// by attribute Imbase.Consts.ObjectTypeAndAttCatalogLinkID (with object's type hierarchy).
  /// </summary>
  /// <param name="objectType">Object type id</param>
  /// <returns></returns>
  List<ImbaseObjectAttrLink> GetImbaseObjectAttrLinks(int objectType);

  /// <summary>
  /// Get all object types + attributes links to imbase catalogs
  /// by attribute Imbase.Consts.ObjectTypeAndAttCatalogLinkID (without object's type hierarchy).
  /// </summary>
  /// <returns></returns>
  List<ImbaseObjectAttrLink> GetImbaseObjectAttrLinks();

  /// <summary>
  /// Позволяет получить идентификатор объекта по старому ключу IMBASE.
  /// </summary>
  /// <param name="oldImbaseKey">Старый ключ IMBASE</param>
  /// <param name="objectType">Тип объектов, среди которых искать</param>
  /// <param name="createIfNotFound">создавать ли новый объект, если не найден</param>
  /// <returns>Идентификатор версии объекта</returns>
  long GetObjectIdByOldImbaseKey(string oldImbaseKey, int objectType, bool createIfNotFound);

  /// <summary>
  /// Позволяет получить идентификатор объекта по ключу IMBASE.
  /// </summary>
  /// <param name="imbaseKey">Ключ IMBASE</param>
  /// <param name="createIfNotFound">создавать ли новый объект, если не найден( в случае старого ключа)</param>
  /// <returns>Идентификатор версии объекта</returns>
  long GetObjectIdByImbaseKey(string imbaseKey, bool createIfNotFound);

  /// <summary>Формирование ключа "Ссылка на запись таблицы IMBASE".</summary>
  /// <param name="strImbaseKey">Ранее сформированный ключ</param>
  /// <param name="useGuid">Использовать GUID как идентификатор ярлыка вместо ID</param>
  /// <returns>Новый ключ. Если строка пустая то запись таблицы не была выбрана</returns>
  string SelectRecord(string strImbaseKey, bool useGuid);

  /// <summary>
  /// Получить виртуальный нод "Каталоги и справочники IMBASE".
  /// </summary>
  /// <param name="catalogIDs">Список идентификаторов каталогов, которые необходимо отобразить</param>
  /// <returns>Виртуальный нод "Каталоги и справочники IMBASE"</returns>
  /// <remarks>Если список идентификаторов каталогов пуст или null, то отображается все дерево IMBASE</remarks>
  IDescriptor GetRootDescriptor(List<long> catalogIDs);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="catalogIDs"></param>
  /// <param name="typeIDs"></param>
  /// <param name="services"></param>
  /// <returns></returns>
  List<long> SelectImbaseObjects(
    List<long> catalogIDs = null,
    List<int> typeIDs = null,
    IServiceProvider services = null);

  /// <summary>
  /// Возвращает дескриптор для указанного атрибута в контексте типа объекта, на основании свойства атрибута "Справочник Imbase".
  /// Если тип объекта не задан, читает свойство "Справочник Imbase" у атрибута.
  /// Если тип атрибута не задан - вернет дескриптор из всех каталогов, из которых можно выбрать
  /// </summary>
  /// <returns></returns>
  IDescriptor GetImbaseDescriptor(int objectTypeId = -1, int attributeId = 0);
}
