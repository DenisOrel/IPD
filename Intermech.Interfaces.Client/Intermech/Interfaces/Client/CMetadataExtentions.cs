// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CMetadataExtentions
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Прокси-класс для работы с расширениями метаданных</summary>
/// <summary>Создает объект.</summary>
/// <param name="session">Клиентская сессия</param>
/// <param name="id">Идентификатор элемента метаданных</param>
internal abstract class CMetadataExtentions(ClientSession session, int id) : 
  CacheObject(session, id),
  IDBMetadataExtensions
{
  /// <summary>Индекс поля F_VALUE</summary>
  private int _idxFldValue = -2;
  /// <summary>Ид. атрибута для работы с расширенными метаданными</summary>
  protected int _AttributeTypeID = -1;
  /// <summary>
  /// Ид. типа объектов для работы с расширенными метаданными
  /// </summary>
  protected int _ObjectTypeID = -1;
  /// <summary>Ид. типа связей для работы с расширенными метаданными</summary>
  protected int _RelationTypeID = -1;

  /// <summary>Получение серверного объекта</summary>
  /// <returns></returns>
  private IDBMetadataExtensions GetServerExtentions()
  {
    this._clientSession.Guard.ValidateCall();
    return this.GetServerObject() is IDBMetadataExtensions serverObject ? serverObject : throw new NotSupportedException("IDBMetadataExtensions not supported!");
  }

  /// <summary>
  /// Ссылка на таблицу с расширениями метаданных IMS_MD_EXTENSIONS
  /// </summary>
  public DataTable ExtensionsTable
  {
    get
    {
      this._clientSession.Guard.ValidateCall();
      return this._clientSession.ClientCache.GetTable("IMS_MD_EXTENSIONS");
    }
  }

  /// <summary>
  /// Записывает в расширенные матаданные именованный набор значений строкового типа
  /// </summary>
  /// <param name="valueName"></param>
  /// <param name="categoryType"></param>
  /// <param name="valuesList"></param>
  public void SetMDValues(string valueName, int categoryType, string[] valuesList)
  {
    this._clientSession.Guard.ValidateCall();
    this.GetServerExtentions().SetMDValues(valueName, categoryType, valuesList);
    this.ReloadClientCache();
  }

  /// <summary>
  /// Записывает в расширенные матаданные именованный набор значений типа int
  /// </summary>
  /// <param name="valueName"></param>
  /// <param name="categoryType"></param>
  /// <param name="valuesList"></param>
  public void SetMDValues(string valueName, int categoryType, int[] valuesList)
  {
    this._clientSession.Guard.ValidateCall();
    this.GetServerExtentions().SetMDValues(valueName, categoryType, valuesList);
    this.ReloadClientCache();
  }

  /// <summary>
  /// Записывает в расширенные матаданные именованный набор значений типа long
  /// </summary>
  /// <param name="valueName"></param>
  /// <param name="categoryType"></param>
  /// <param name="valuesList"></param>
  public void SetMDValues(string valueName, int categoryType, long[] valuesList)
  {
    this._clientSession.Guard.ValidateCall();
    this.GetServerExtentions().SetMDValues(valueName, categoryType, valuesList);
    this.ReloadClientCache();
  }

  /// <summary>
  /// Записывает в расширенные матаданные именованный набор значений типа Guid
  /// </summary>
  /// <param name="valueName"></param>
  /// <param name="categoryType"></param>
  /// <param name="valuesList"></param>
  public void SetMDValues(string valueName, int categoryType, Guid[] valuesList)
  {
    this._clientSession.Guard.ValidateCall();
    this.GetServerExtentions().SetMDValues(valueName, categoryType, valuesList);
    this.ReloadClientCache();
  }

  /// <summary>
  /// Записывает в расширенные матаданные именованный набор значений
  /// </summary>
  /// <param name="valueName">Имя значений</param>
  /// <param name="valuesList">Список значений - будет записан в указанном порядке</param>
  public void SetMDValues(string valueName, string[] valuesList)
  {
    this._clientSession.Guard.ValidateCall();
    this.GetServerExtentions().SetMDValues(valueName, valuesList);
    this.ReloadClientCache();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="valueName"></param>
  /// <param name="categoryType"></param>
  /// <param name="value"></param>
  public void SetMDValue(string valueName, int categoryType, string value)
  {
    this._clientSession.Guard.ValidateCall();
    this.GetServerExtentions().SetMDValue(valueName, categoryType, value);
    this.ReloadClientCache();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="valueName"></param>
  /// <param name="value"></param>
  public void SetMDValue(string valueName, string value)
  {
    this._clientSession.Guard.ValidateCall();
    this.GetServerExtentions().SetMDValue(valueName, value);
    this.ReloadClientCache();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="valueName"></param>
  /// <returns></returns>
  public string GetMDValue(string valueName)
  {
    string[] mdValues = this.GetMDValues(valueName);
    return mdValues.Length == 0 ? string.Empty : mdValues[0];
  }

  /// <summary>
  /// Возвращает строковый список значений для параметра valueName
  /// </summary>
  public string[] GetMDValues(string valueName)
  {
    this._clientSession.Guard.ValidateCall();
    DataTable extensionsTable = this.ExtensionsTable;
    lock (extensionsTable)
    {
      DataRow[] source = extensionsTable.Select($"F_ATTRIBUTE_ID = {this._AttributeTypeID} AND F_OBJECT_TYPE = {this._ObjectTypeID} AND F_RELATION_TYPE = {this._RelationTypeID} AND F_PARAM_NAME = {DataSetProcessor.QString(valueName)}", "F_INLIST_ID ASC");
      if (this._idxFldValue == -2)
        this._idxFldValue = extensionsTable.Columns.IndexOf("F_VALUE");
      System.Func<DataRow, string> selector = (System.Func<DataRow, string>) (item => Convert.ToString(item[this._idxFldValue]));
      return ((IEnumerable<DataRow>) source).Select<DataRow, string>(selector).ToArray<string>();
    }
  }

  /// <summary>
  /// Возвращает целочисленный список значений для параметра valueName
  /// </summary>
  public int[] GetMDValuesInt(string valueName)
  {
    this._clientSession.Guard.ValidateCall();
    return ((IEnumerable<string>) this.GetMDValues(valueName)).Select<string, int>((System.Func<string, int>) (item => Convert.ToInt32(item))).ToArray<int>();
  }

  /// <summary>
  /// Возвращает список значений для параметра valueName в виде гуидов
  /// </summary>
  public Guid[] GetMDValuesGuid(string valueName)
  {
    this._clientSession.Guard.ValidateCall();
    return ((IEnumerable<string>) this.GetMDValues(valueName)).Select<string, Guid>((System.Func<string, Guid>) (item => new Guid(item))).ToArray<Guid>();
  }

  /// <summary>
  /// Возвращает список значений для параметра valueName в виде Int64
  /// </summary>
  public long[] GetMDValuesInt64(string valueName)
  {
    this._clientSession.Guard.ValidateCall();
    return ((IEnumerable<string>) this.GetMDValues(valueName)).Select<string, long>((System.Func<string, long>) (item => Convert.ToInt64(item))).ToArray<long>();
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void ReloadClientCache()
  {
    base.ReloadClientCache();
    this._idxFldValue = -2;
  }
}
