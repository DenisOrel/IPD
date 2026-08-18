// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.MetadataInfoObject
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Базовый класс для наследования получателей инфы о метаданных в режиме чтения
/// </summary>
internal abstract class MetadataInfoObject
{
  /// <summary>
  /// Таблица с параметрами, которые могут храниться в данных объектах.
  /// </summary>
  protected HybridTable paramsTable = new HybridTable();
  /// <summary>Ид. атрибута для работы с расширенными метаданными</summary>
  protected int _AttributeTypeID = -1;
  /// <summary>
  /// Ид. типа объектов для работы с расширенными метаданными
  /// </summary>
  protected int _ObjectTypeID = -1;
  /// <summary>Ид. типа связей для работы с расширенными метаданными</summary>
  protected int _RelationTypeID = -1;

  /// <summary>Клиентский кэш таблиц метаданных</summary>
  protected MetadataInfoParentContext ServiceContext { get; private set; }

  /// <summary>Ид. метаданного (AttributeID например)</summary>
  protected int MetadataID { get; private set; }

  public MetadataInfoObject(MetadataInfoParentContext serviceContext, int metadataID)
  {
    this.ServiceContext = serviceContext != null ? serviceContext : throw new ArgumentNullException(nameof (serviceContext));
    this.MetadataID = metadataID;
    this.paramsTable.Create(this.GetSourceDataRowFromCache());
    if (this.paramsTable.RowsCount == 0)
      throw new KernelException(this.MetadataNotFoundMessage);
  }

  /// <summary>Имя таблицы в базе</summary>
  protected abstract string DBTableName { get; }

  /// <summary>
  /// Сообщение об ошибке о том, что метаданное с таким ид. не найдено
  /// </summary>
  protected abstract string MetadataNotFoundMessage { get; }

  /// <summary>Наименование метаданного</summary>
  public abstract string ObjectName { get; }

  /// <summary>
  /// Возвращает строку таблицы из клиентского кэша, описывающая текущий элемент метаданных.
  /// </summary>
  /// <returns>Строка таблицы из клиентского кэша, описывающая текущий элемент метаданных. Может быть null</returns>
  protected virtual DataRow GetSourceDataRowFromCache()
  {
    return this.ServiceContext.ClientCache.GetTable(this.DBTableName).Rows.Find((object) this.MetadataID);
  }

  /// <summary>
  /// Возвращает строковый список значений для параметра valueName
  /// </summary>
  public string[] GetMDValues(string valueName)
  {
    DataTable table = this.ServiceContext.ClientCache.GetTable("IMS_MD_EXTENSIONS");
    lock (table)
    {
      DataRow[] source = table.Select($"F_ATTRIBUTE_ID = {this._AttributeTypeID} AND F_OBJECT_TYPE = {this._ObjectTypeID} AND F_RELATION_TYPE = {this._RelationTypeID} AND F_PARAM_NAME = {DataSetProcessor.QString(valueName)}", "F_INLIST_ID ASC");
      int valueIndex = table.Columns.IndexOf("F_VALUE");
      System.Func<DataRow, string> selector = (System.Func<DataRow, string>) (item => Convert.ToString(item[valueIndex]));
      return ((IEnumerable<DataRow>) source).Select<DataRow, string>(selector).ToArray<string>();
    }
  }

  /// <summary>
  /// Возвращает целочисленный список значений для параметра valueName
  /// </summary>
  public long[] GetMDValuesInt64(string valueName)
  {
    return ((IEnumerable<string>) this.GetMDValues(valueName)).Select<string, long>((System.Func<string, long>) (item => Convert.ToInt64(item))).ToArray<long>();
  }

  public virtual Guid GUID
  {
    [DebuggerStepThrough] get => new Guid(this.paramsTable[0]["F_GUID"].ToString());
  }
}
