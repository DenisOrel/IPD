// Decompiled with JetBrains decompiler
// Type: XmlReaderAPI.Common.ImMetaDataElement
// Assembly: Intermech.IpsXmlViewer.XmlReaderAPI, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 197F841C-E5B9-4815-BCCD-9737649DED5C
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.XmlReaderAPI.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.XmlReaderAPI.xml

using Intermech.IpsXmlViewer.Interfaces;
using Intermech.Pools;
using Intermech.Text;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;
using System.Xml;

#nullable disable
namespace XmlReaderAPI.Common;

/// <summary>
/// Абстрактный базовый класс, содержащий Guid (используется для метаданных)
/// </summary>
public abstract class ImMetaDataElement : 
  ImGuidElement,
  IImMetaDataElement,
  IImGuidElement,
  IImCompositeAttrElement,
  IImCompositeElement,
  IImBaseElement,
  IAssignable,
  ICloneable,
  IDisplayable,
  IEquatable<IImMetaDataElement>
{
  /// <summary>
  /// Имя таблицы SQL, в которой хранится содержимое элемента
  /// </summary>
  public abstract string SQLTableName { get; }

  /// <summary>Количество атрибутов, которые есть в записи</summary>
  public virtual int SQLAttributes
  {
    get
    {
      int sqlAttributes = 0;
      foreach (KeyValuePair<string, object> attribute in (IEnumerable<KeyValuePair<string, object>>) this._attributes)
      {
        if (attribute.Value is string)
          ++sqlAttributes;
      }
      return sqlAttributes;
    }
  }

  /// <summary>
  /// Уникальный идентификатор элемента (тип атрибута/объекта/связи)
  /// </summary>
  public abstract string UniqueID { get; }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="other"></param>
  /// <returns></returns>
  public virtual bool Equals(IImMetaDataElement other)
  {
    return other != null && this.UniqueID == other.UniqueID;
  }

  /// <summary>Загрузить содержимое объекта из документа XML</summary>
  /// <param name="xml">Документ XML</param>
  /// <param name="kernel">Микроядро</param>
  /// <returns>true - узел считал содержимое коректно</returns>
  public override bool Load(XmlReader xml, IKernel kernel)
  {
    this.Clear();
    if (xml == null || xml.NodeType != XmlNodeType.Element || xml.Name.Trim().ToUpperInvariant() != this.MainAttrName)
      return false;
    int depth = xml.Depth;
    while (xml.Read() && (xml.Depth != depth || xml.NodeType != XmlNodeType.EndElement))
    {
      if (xml.NodeType == XmlNodeType.Element)
      {
        if (xml.Depth <= depth)
          return true;
        this.SetAsString(xml.Name.Trim().ToUpperInvariant(), xml.ReadInnerXml());
      }
    }
    return true;
  }

  /// <summary>
  /// Получить содержимое элемента в виде SQL-последовательностей (SQLite)
  /// </summary>
  /// <param name="connection">Соединение</param>
  /// <param name="transaction">Транзакция</param>
  /// <param name="tables">Список таблиц и их колонок</param>
  /// <returns>Содержимое элемента в виде SQL-последовательностей (SQLite) или null</returns>
  public override IList<SQLiteCommand> GetAsSQL(
    SQLiteConnection connection,
    SQLiteTransaction transaction,
    IDictionary<string, IList<string>> tables)
  {
    List<SQLiteCommand> asSql = new List<SQLiteCommand>(this._attributes.Count);
    List<KeyValuePair<string, object>> keyValuePairList = new List<KeyValuePair<string, object>>(this._attributes.Count);
    IList<string> table = tables[this.SQLTableName.ToUpperInvariant().Trim()];
    foreach (KeyValuePair<string, object> attribute in (IEnumerable<KeyValuePair<string, object>>) this._attributes)
    {
      if (!(attribute.Value is IImAttribute) && table.Contains(attribute.Key.ToUpperInvariant().Trim()))
        keyValuePairList.Add(attribute);
    }
    int count = keyValuePairList.Count;
    if (count == 0)
      return (IList<SQLiteCommand>) asSql;
    ObjectPoolScope<StringBuilder> objectPoolScope1 = TextServices.StringBuilderPool.Allocate(2046);
    ObjectPoolScope<StringBuilder> objectPoolScope2 = TextServices.StringBuilderPool.Allocate(2046);
    try
    {
      StringBuilder stringBuilder1 = objectPoolScope1.Object;
      StringBuilder stringBuilder2 = objectPoolScope2.Object;
      SQLiteParameter[] values = new SQLiteParameter[count];
      int index = 0;
      foreach (KeyValuePair<string, object> keyValuePair in keyValuePairList)
      {
        stringBuilder1.Append("'");
        stringBuilder1.Append(keyValuePair.Key);
        stringBuilder1.Append(index < count - 1 ? "'," : "'");
        stringBuilder2.Append("@");
        stringBuilder2.Append(keyValuePair.Key);
        stringBuilder2.Append(index < count - 1 ? "," : "");
        values[index] = new SQLiteParameter("@" + keyValuePair.Key, keyValuePair.Value);
        ++index;
      }
      SQLiteCommand sqLiteCommand = new SQLiteCommand($"INSERT INTO '{this.SQLTableName}' ({stringBuilder1.ToString()}) VALUES ({stringBuilder2.ToString()});", connection, transaction);
      sqLiteCommand.Parameters.AddRange(values);
      asSql.Add(sqLiteCommand);
    }
    finally
    {
      objectPoolScope1.Dispose();
      objectPoolScope2.Dispose();
    }
    foreach (KeyValuePair<string, object> attribute in (IEnumerable<KeyValuePair<string, object>>) this._attributes)
    {
      if (attribute.Value is ImBaseElement imBaseElement)
        asSql.AddRange((IEnumerable<SQLiteCommand>) imBaseElement.GetAsSQL(connection, transaction, tables));
    }
    return (IList<SQLiteCommand>) asSql;
  }
}
