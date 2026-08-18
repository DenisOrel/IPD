// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.Helpers.ImbaseSearchTool
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Imbase.Helpers;

/// <summary>
/// Реализует инструмент поиска записей в IMBASE по значениям атрибутов
/// </summary>
public class ImbaseSearchTool
{
  private IUserSession userSession;
  private IImbaseServer imbaseServerService;
  private IImbaseIndexingService imbaseIndexingService;

  /// <summary>Создает объект.</summary>
  /// <param name="userSession">Пользовательская сессия</param>
  /// <param name="imbaseServerService">Основной сервис IMBASE</param>
  /// <param name="imbaseIndexingService">Сервис индексов IMBASE</param>
  /// <exception cref="T:ArgumentNullException">userSession || imbaseServerService || imbaseIndexingService</exception>
  public ImbaseSearchTool(
    IUserSession userSession,
    IImbaseServer imbaseServerService,
    IImbaseIndexingService imbaseIndexingService)
  {
    if (userSession == null)
      throw new ArgumentNullException(nameof (userSession));
    if (imbaseServerService == null)
      throw new ArgumentNullException(nameof (imbaseServerService));
    if (imbaseIndexingService == null)
      throw new ArgumentNullException(nameof (imbaseIndexingService));
    this.userSession = userSession;
    this.imbaseServerService = imbaseServerService;
    this.imbaseIndexingService = imbaseIndexingService;
  }

  /// <summary>
  /// Находит запись в каталогах IMBASE по типу объекта и значению атрибута записи.
  /// </summary>
  /// <param name="objectTypeId">Идентификатор типа объекта</param>
  /// <param name="allowDerivedObjectTypes">Признак, учитывать только указанный тип объекта или ветку типов</param>
  /// <param name="attributeId">Идентификатор атрибута</param>
  /// <param name="attributeValue">Значение атрибута</param>
  /// <returns>Кортеж из идентификаторов каталога, таблицы и записи; или null, если найти запись не удалось</returns>
  /// <exception cref="T:ArgumentException">objectTypeId || attributeId</exception>
  /// <exception cref="T:ArgumentNullException">attributeValue</exception>
  public Tuple<long, long, long> FindRecord(
    int objectTypeId,
    bool allowDerivedObjectTypes,
    int attributeId,
    string attributeValue)
  {
    if (objectTypeId == -1)
      throw new ArgumentException("Не задан идентификатор типа объекта для искомой записи.", nameof (objectTypeId));
    if (attributeId == 0)
      throw new ArgumentException("Не задан идентификатор атрибута.", nameof (attributeId));
    if (attributeValue == null)
      throw new ArgumentNullException(nameof (attributeValue));
    long[] catalogsForCreateType = this.imbaseServerService.GetCatalogsForCreateType(this.userSession.SessionGUID, (object) objectTypeId, allowDerivedObjectTypes);
    if (catalogsForCreateType == null || catalogsForCreateType.Length == 0)
      return (Tuple<long, long, long>) null;
    List<long> longList = new List<long>(catalogsForCreateType.Length);
    foreach (long catalogID in catalogsForCreateType)
    {
      try
      {
        long tableRefID;
        long recID;
        if (this.imbaseIndexingService.FindByIndex(this.userSession.SessionGUID, catalogID, attributeId, attributeValue, out tableRefID, out recID))
          return Tuple.Create<long, long, long>(catalogID, tableRefID, recID);
      }
      catch (IndexNotFoundException ex)
      {
        longList.Add(catalogID);
      }
    }
    if (longList.Count == 0)
      return (Tuple<long, long, long>) null;
    string name = this.userSession.GetAttributeType(attributeId, true).Name;
    List<string> values = new List<string>(longList.Count);
    foreach (long objectID in longList)
    {
      IDBObject dbObject = this.userSession.GetObject(objectID, true);
      values.Add(dbObject.Caption);
    }
    throw new KernelException($"Не удалось найти требуемый объект IMBASE из-за отсутствия индексов по атрибуту '{name}' для следующих каталогов IMBASE: {string.Join(", ", (IEnumerable<string>) values)}. Обратитесь к администратору IPS для создания отсутствующих индексов.");
  }
}
