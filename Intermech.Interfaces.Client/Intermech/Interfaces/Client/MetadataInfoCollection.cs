// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.MetadataInfoCollection
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Базовый класс для наследования получателей инфы о коллекции метаданных определенной категории в режиме чтения
/// </summary>
internal abstract class MetadataInfoCollection : IDBCollection
{
  /// <summary>Клиентский кэш таблиц метаданных</summary>
  protected MetadataInfoParentContext ServiceContext { get; private set; }

  /// <summary>
  /// Ид. родительского метаданного (например, ид. типа, для которого собирается коллекция атрибутов)
  /// </summary>
  public object ParentID { get; set; }

  /// <summary>Нужно ли фильтровать коллекцию по видимости</summary>
  public bool Filtering { get; private set; }

  public MetadataInfoCollection(
    MetadataInfoParentContext serviceContext,
    object parentID,
    bool filtering)
  {
    this.ServiceContext = serviceContext != null ? serviceContext : throw new ArgumentNullException(nameof (serviceContext));
    this.ParentID = parentID;
    this.Filtering = filtering;
  }

  /// <summary>Имя таблицы в базе</summary>
  protected abstract string DBTableName { get; }

  /// <summary>Имя поля для фильтрации таблицы по родителю</summary>
  protected abstract string DBKeyField { get; }

  /// <summary>
  /// Возвращает SQL-условие, при необходимости отсеивающее только объекты, входящие в состав parentID
  /// </summary>
  protected virtual string GetParentSQL()
  {
    return this.ParentID != null && this.ParentID.ToString() != string.Empty ? $"{this.DBKeyField} = {this.ParentID}" : string.Empty;
  }

  /// <summary>
  /// Возвращает таблицу с объектами входящими в состав parentID и отсортированными по orderBy
  /// </summary>
  public virtual DataTable Select(string orderBy, params object[] addInfo)
  {
    DataTable table = this.ServiceContext.ClientCache.GetTable(this.DBTableName);
    DataTable dataTable = table.Clone();
    this.FillCaptions(dataTable);
    DataRow[] fromRows = table.Select(this.GetParentSQL(), orderBy);
    DataSetProcessor.AssignRows(dataTable, (IEnumerable<DataRow>) fromRows);
    if (this.Filtering)
      dataTable = this.ServiceContext.ClientCache.GetFilteredTable(this.DBTableName, this.DBKeyField, dataTable);
    return dataTable;
  }

  public long Count => (long) this.GetCount();

  protected virtual int GetCount()
  {
    return this.ServiceContext.ClientCache.GetTable(this.DBTableName).Select(this.GetParentSQL()).Length;
  }

  /// <summary>
  /// Порожденные классы заполняют заголовки таблицы, возвращаемой селектом
  /// </summary>
  protected virtual void FillCaptions(DataTable datatable)
  {
    foreach (DataColumn column in (InternalDataCollectionBase) datatable.Columns)
      column.Caption = this.GetCaption(column.ColumnName);
  }

  private string GetCaption(string id)
  {
    return (DataSetProcessor.ColumnCaptions[(object) id] ?? (object) id).ToString();
  }

  public int[] GetVisibleList() => throw new OperationNotApplicableException();
}
