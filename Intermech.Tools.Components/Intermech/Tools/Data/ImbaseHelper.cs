// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Data.ImbaseHelper
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using System;

#nullable disable
namespace Intermech.Tools.Data;

/// <summary>
/// Предоставляет удобные методы для поиска изделий в IMBASE.
/// </summary>
public static class ImbaseHelper
{
  /// <summary>
  /// Проверяет, является ли указанная строка ключем IMBASE.
  /// </summary>
  /// <param name="text">Проверяемая строка</param>
  /// <returns>true, если это ключ IMBASE, false - иначе</returns>
  /// <exception cref="T:System.ArgumentNullException">Проверяемая строка не указана</exception>
  public static bool IsImbaseKey(string text)
  {
    if (text == null)
      throw new ArgumentNullException(nameof (text));
    return text.Length == 38 && text.StartsWith("IG", StringComparison.CurrentCultureIgnoreCase) && GuidHelper.IsGuid(text.Substring(2));
  }

  /// <summary>
  /// Находит в каталогах IMBASE запись с указанными значениями идентифицирующих атрибутов и возвращает результат поиска.
  /// </summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="identityAttributeId">Идетифицирующий атрибут</param>
  /// <param name="identityValue">Значение для поиска</param>
  /// <param name="tableId">Идентификатор ссылки на таблицу</param>
  /// <param name="recordId">Номер записи</param>
  public static bool FindRecordByIndex(
    IUserSession session,
    StringKey identityAttributeKey,
    int identityAttributeId,
    string identityValue,
    out long tableId,
    out long recordId)
  {
    IImbaseIndexingService service = ServiceUtils.GetService<IImbaseIndexingService>((object) session, true);
    try
    {
      return service.FindByIndex(session.SessionGUID, identityAttributeId, identityValue, out tableId, out recordId);
    }
    catch (IndexNotFoundException ex)
    {
      throw new IndexNotFoundException($"Невозможно найти объект IMBASE по значению атрибута '{identityAttributeKey}'='{identityValue}', так как не настроена индексация каталогов IMBASE по этому атрибуту. Обратитесь к администратору IPS.", (Exception) ex);
    }
  }

  /// <summary>Создает объект IMBASE по указанной записи IMBASE.</summary>
  /// <param name="tableId">Идентификатор таблицы IMBASE</param>
  /// <param name="recordId">Идентификатор записи в таблице IMBASE</param>
  /// <returns>Описатель созданного объекта - (идентификатор версии, идентификатор типа объекта, ключ Imbase)</returns>
  public static Tuple<long, int, string> CreateImbaseObject(long tableId, long recordId)
  {
    return ImbaseHelper.ImbaseObjectInfo(ImbaseHelper.CreateImbaseObjectInternal(tableId, recordId));
  }

  private static long CreateImbaseObjectInternal(long tableId, long recordId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return ServiceUtils.GetService<IImbaseServer>((object) sessionKeeper.Session, true).CreateObject(sessionKeeper.Session.SessionGUID, -1L, tableId, recordId, true, -1);
  }

  /// <summary>
  /// Находит в каталогах IMBASE запись с указанными значениями идентифицирующих атрибутов и возвращает соответствующее этой записи изделие.
  /// Если изделие найдено, но еще не существует, то метод создает его. Если изделие не найдено, то метод возвращает null.
  /// </summary>
  /// <param name="imbaseKey">Ключ Imbase. Может быть пусто или null</param>
  /// <param name="designation">Обозначение изделия. Может быть пусто или null</param>
  /// <param name="okpCode">Код ОКП изделия. Может быть пусто или null</param>
  /// <param name="name">Наименование изделия. Может быть пусто или null</param>
  /// <returns>Описатель найденного изделия - (идентификатор версии, идентификатор типа изделия, ключ Imbase). Может быть null, если изделие не найдено</returns>
  /// <exception cref="T:Intermech.Interfaces.Imbase.IndexNotFoundException">Не настроены индексы по каталогам IMBASE</exception>
  public static Tuple<long, int, string> FindOrCreateImbaseObject(
    string imbaseKey,
    string designation,
    string okpCode,
    string name)
  {
    ValueBag attributes = new ValueBag();
    if (!string.IsNullOrEmpty(imbaseKey))
      attributes.Add((StringKey) IDCache.Default.ImbaseKey.Text, (object) imbaseKey);
    if (!string.IsNullOrEmpty(designation))
      attributes.Add((StringKey) IDCache.Default.Designation.Text, (object) designation);
    if (!string.IsNullOrEmpty(okpCode))
      attributes.Add((StringKey) IDCache.Default.OKPCode.Text, (object) okpCode);
    if (!string.IsNullOrEmpty(name))
      attributes.Add((StringKey) IDCache.Default.Name.Text, (object) name);
    attributes.AcceptChanges();
    return ImbaseHelper.FindOrCreateImbaseObject(attributes);
  }

  /// <summary>
  /// Находит в каталогах IMBASE запись по идентифицирующим атрибутам и возвращает соответствующее этой записи изделие. Используются атрибуты: ключ Imbase,
  /// обозначение, код ОКП, наименование. Если изделие найдено, но еще не существует, то метод создает его. Если изделие не найдено, то метод возвращает null.
  /// </summary>
  /// <param name="designation">Обозначение изделия. Может быть пусто или null</param>
  /// <param name="okpCode">Код ОКП изделия. Может быть пусто или null</param>
  /// <param name="name">Наименование изделия. Может быть пусто или null</param>
  /// <returns>Описатель найденного изделия - (идентификатор версии, идентификатор типа изделия, ключ Imbase). Может быть null, если изделие не найдено</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на контейнер значений атрибутов не может быть null</exception>
  /// <exception cref="T:Intermech.Interfaces.Imbase.IndexNotFoundException">Не настроены индексы по каталогам IMBASE</exception>
  public static Tuple<long, int, string> FindOrCreateImbaseObject(ValueBag attributes)
  {
    long objectId1 = attributes != null ? ImbaseHelper.RecreateImbaseObjectByKey(attributes) : throw new ArgumentNullException(nameof (attributes));
    if (objectId1 != 0L)
      return ImbaseHelper.ImbaseObjectInfo(objectId1);
    long objectId2 = ImbaseHelper.RecreateImbaseObjectByIdentity((StringKey) IDCache.Default.Designation.Text, IDCache.Default.Designation.Id, attributes);
    if (objectId2 != 0L)
      return ImbaseHelper.ImbaseObjectInfo(objectId2);
    long objectId3 = ImbaseHelper.RecreateImbaseObjectByIdentity((StringKey) IDCache.Default.OKPCode.Text, IDCache.Default.OKPCode.Id, attributes);
    if (objectId3 != 0L)
      return ImbaseHelper.ImbaseObjectInfo(objectId3);
    long objectId4 = ImbaseHelper.RecreateImbaseObjectByIdentity((StringKey) IDCache.Default.Name.Text, IDCache.Default.Name.Id, attributes);
    return objectId4 != 0L ? ImbaseHelper.ImbaseObjectInfo(objectId4) : (Tuple<long, int, string>) null;
  }

  private static long RecreateImbaseObjectByKey(ValueBag attributes)
  {
    string imbaseKey = attributes.Read<string>((StringKey) IDCache.Default.ImbaseKey.Text, string.Empty);
    if (!string.IsNullOrEmpty(imbaseKey))
    {
      try
      {
        long objectIdByImbaseKey = ServiceUtils.GetService<IImbaseSelector>((object) ServicesManager.ServiceContainer, true).GetObjectIdByImbaseKey(imbaseKey, true);
        if (objectIdByImbaseKey != -1L)
          return objectIdByImbaseKey;
      }
      catch
      {
      }
    }
    return 0;
  }

  private static long RecreateImbaseObjectByIdentity(
    StringKey identityAttributeKey,
    int identityAttributeId,
    ValueBag attributes)
  {
    string identityValue = attributes.Read<string>(identityAttributeKey, string.Empty);
    return !string.IsNullOrEmpty(identityValue) ? ImbaseHelper.RecreateImbaseObjectByIdentity(identityAttributeKey, identityAttributeId, identityValue) : 0L;
  }

  private static long RecreateImbaseObjectByIdentity(
    StringKey identityAttributeKey,
    int identityAttributeId,
    string identityValue)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ServiceUtils.GetService<IImbaseIndexingService>((object) sessionKeeper.Session, true);
      long tableId;
      long recordId;
      if (ImbaseHelper.FindRecordByIndex(sessionKeeper.Session, identityAttributeKey, identityAttributeId, identityValue, out tableId, out recordId))
        return ImbaseHelper.CreateImbaseObjectInternal(tableId, recordId);
    }
    return 0;
  }

  private static Tuple<long, int, string> ImbaseObjectInfo(long objectId)
  {
    int objectType;
    string str;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectId, true);
      objectType = dbObject.ObjectType;
      str = $"IG{dbObject.GUID:D}";
    }
    return Tuple.Create<long, int, string>(objectId, objectType, str);
  }
}
