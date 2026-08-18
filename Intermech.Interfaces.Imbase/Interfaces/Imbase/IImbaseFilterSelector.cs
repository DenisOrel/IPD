// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.IImbaseFilterSelector
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using Intermech.Interfaces.Compositions;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Imbase;

/// <summary>Интерфейс для работы с диалогом фильтров IMBASE.</summary>
public interface IImbaseFilterSelector
{
  /// <summary>Номер выбранной записи ссылки на таблицу IMBASE.</summary>
  long RecordID { get; set; }

  /// <summary>
  /// Выбрать объект IMBASE.
  /// Выбор осуществляется с возможностью задания фильтров IMBASE.
  /// </summary>
  /// <param name="catalogID">Идентификатор каталога/справочника</param>
  /// <param name="objID">Идентификатор объекта</param>
  /// <param name="prevCheckedIDs">Идентификаторы ранее выбранных объектов IMBASE</param>
  /// <param name="_objectVersionProcessed">флаг обработки версии объектов по VersionID (true) или объектов по ID (false)</param>
  ///  // #022021_ftObjectLinkByID здесь нужна поддержка типа ftObjectLinkByID
  ///             <returns>Идентификаторы выбранных объектов IMBASE</returns>
  List<long> CheckImbaseObjects(
    long catalogID,
    long objID,
    List<long> prevCheckedIDs,
    bool _objectVersionProcessed = true);

  /// <summary>
  /// Выбрать объект IMBASE.
  /// Выбор осуществляется с возможностью задания фильтров IMBASE.
  /// </summary>
  /// <param name="catalogIDs">Идентификаторы каталогов/справочников</param>
  /// <param name="objID">Идентификатор объекта</param>
  /// <param name="prevCheckedIDs">Идентификаторы ранее выбранных объектов IMBASE</param>
  /// <param name="_objectVersionProcessed">флаг обработки версии объектов по VersionID (true) или объектов по ID (false)</param>
  ///  // #022021_ftObjectLinkByID здесь нужна поддержка типа ftObjectLinkByID
  ///             <returns>Идентификаторы выбранных объектов IMBASE</returns>
  List<long> CheckImbaseObjects(
    List<long> catalogIDs,
    long objID,
    List<long> prevCheckedIDs,
    bool _objectVersionProcessed = true);

  /// <summary>
  /// Выбрать объект IMBASE.
  /// Выбор осуществляется с возможностью задания фильтров IMBASE.
  /// </summary>
  /// <param name="catalogID">Идентификатор каталога/справочника</param>
  /// <param name="objID">Идентификатор объекта</param>
  /// <param name="prevSelectedID">Идентификатор ранее выбранного объекта IMBASE</param>
  /// <param name="mode">Режим выбора объекта. Создать объект по записи ярлыка/вернуть идентификатор выбранной папки</param>
  /// <param name="_objectVersionProcessed">флаг обработки версии объектов по VersionID (true) или объектов по ID (false)</param>
  ///  // #022021_ftObjectLinkByID здесь нужна поддержка типа ftObjectLinkByID
  ///             <returns>Идентификатор объекта IMBASE</returns>
  long SelectImbaseObject(
    long catalogID,
    long objID,
    long prevSelectedID,
    ImbaseCatalogSelectMode mode = ImbaseCatalogSelectMode.imcmSelectFolder,
    bool _objectVersionProcessed = true);

  /// <summary>
  /// Выбрать объект IMBASE.
  /// Выбор осуществляется с возможностью задания фильтров IMBASE.
  /// </summary>
  /// <param name="catalogID">Идентификатор каталога/справочника</param>
  /// <param name="needObjType">Тип создаваемых объектов</param>
  /// <param name="objID">Идентификатор объекта</param>
  /// <param name="prevSelectedID">Идентификатор ранее выбранного объекта IMBASE</param>
  /// <param name="mode">Режим выбора объекта. Создать объект по записи ярлыка/вернуть идентификатор выбранной папки</param>
  /// <param name="_objectVersionProcessed">флаг обработки версии объектов по VersionID (true) или объектов по ID (false)</param>
  ///  // #022021_ftObjectLinkByID здесь нужна поддержка типа ftObjectLinkByID
  ///             <returns>Идентификатор объекта IMBASE</returns>
  long SelectImbaseObject(
    long catalogID,
    int needObjType,
    long objID,
    long prevSelectedID,
    ImbaseCatalogSelectMode mode,
    bool _objectVersionProcessed = true);

  /// <summary>
  /// Выбрать объект IMBASE.
  /// Выбор осуществляется с возможностью задания фильтров IMBASE.
  /// </summary>
  /// <param name="catalogIDs">Идентификаторы каталогов/справочников</param>
  /// <param name="needObjTypes">Типы создаваемых объектов</param>
  /// <param name="objID">Идентификатор объекта</param>
  /// <param name="prevSelectedID">Идентификатор ранее выбранного объекта IMBASE</param>
  /// <param name="mode">Режим выбора объекта. Создать объект по записи ярлыка/вернуть идентификатор выбранной папки</param>
  /// <param name="dict">Информация об объекте/связи - Список измененных арибутов</param>
  /// <param name="attrID">Идентификатор атрибута (для загрузки настроек в контексте атрибута)</param>
  /// <param name="_objectVersionProcessed">флаг обработки версии объектов по VersionID (true) или объектов по ID (false)</param>
  ///  // #022021_ftObjectLinkByID здесь нужна поддержка типа ftObjectLinkByID
  ///             <returns>Идентификатор объекта IMBASE</returns>
  long SelectImbaseObject(
    List<long> catalogIDs,
    int[] needObjTypes,
    long objID,
    long prevSelectedID,
    ImbaseCatalogSelectMode mode,
    Dictionary<TypedInfoItem, IEnumerable<AttributeValues>> dict = null,
    int attrID = 0,
    bool _objectVersionProcessed = true);

  /// <summary>
  /// Выбрать объекты IMBASE.
  /// Выбор осуществляется с возможностью задания фильтров IMBASE.
  /// </summary>
  /// <param name="catalogIDs">Идентификаторы каталогов/справочников</param>
  /// <param name="needObjTypes">Типы создаваемых объектов</param>
  /// <param name="objID">Идентификатор объекта</param>
  /// <param name="prevSelectedID">Идентификатор ранее выбранного объекта IMBASE</param>
  /// <param name="mode">Режим выбора объекта. Создать объект по записи ярлыка/вернуть идентификатор выбранной папки</param>
  /// <param name="dict">Информация об объекте/связи - Список измененных арибутов</param>
  /// <param name="attrID">Идентификатор атрибута (для загрузки настроек в контексте атрибута)</param>
  /// <param name="_objectVersionProcessed">флаг обработки версии объектов по VersionID (true) или объектов по ID (false)</param>
  ///  // #022021_ftObjectLinkByID здесь нужна поддержка типа ftObjectLinkByID
  ///             <returns>Идентификаторы объектов IMBASE</returns>
  List<long> SelectImbaseObjects(
    List<long> catalogIDs,
    int[] needObjTypes,
    long objID,
    List<long> prevSelectedID,
    ImbaseCatalogSelectMode mode,
    Dictionary<TypedInfoItem, IEnumerable<AttributeValues>> dict = null,
    int attrID = 0,
    bool _objectVersionProcessed = true);
}
