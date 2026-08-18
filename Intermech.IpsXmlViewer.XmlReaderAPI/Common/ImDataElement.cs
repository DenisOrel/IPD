// Decompiled with JetBrains decompiler
// Type: XmlReaderAPI.Common.ImDataElement
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
using XmlReaderAPI.Data;

#nullable disable
namespace XmlReaderAPI.Common;

/// <summary>
/// Абстрактный базовый класс, содержащий список ключей и значения, а также Guid (используется для объектов и связей).
/// Класс умеет загружать своё содержимое из XML, в т.ч. и значения атрибутов
/// </summary>
public abstract class ImDataElement(int capacity = 0) : 
  ImGuidElement(capacity),
  IImDataElement,
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

  /// <summary>Является ли элемент объектом или связью</summary>
  public abstract bool IsObject { get; }

  /// <summary>
  /// Уникальный идентификатор элемента (версия объекта / идентификатор связи)
  /// </summary>
  public abstract string UniqueID { get; internal set; }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="other"></param>
  /// <returns></returns>
  public virtual bool Equals(IImMetaDataElement other)
  {
    return other != null && this.UniqueID == other.UniqueID;
  }

  /// <summary>Загрузить необязательные атрибуты из базы данных</summary>
  public void LoadAttributes(IKernel kernel)
  {
    kernel?.ReadItemAttributes((IImDataElement) this, false);
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
    int num = -1;
    string str = string.Empty;
    if (kernel != null)
      str = this.UniqueID = kernel.GetUniqueID.ToString();
    while (xml.Read() && (xml.Depth != depth || xml.NodeType != XmlNodeType.EndElement))
    {
      if (xml.NodeType == XmlNodeType.Element)
      {
        if (xml.Depth <= depth)
          return true;
        string upperInvariant = xml.Name.Trim().ToUpperInvariant();
        if (upperInvariant == "ATTRIBUTES" || num > 0 && xml.Depth >= num)
        {
          if (num < 0)
            num = xml.Depth;
          ImAttribute attribute = new ImAttribute();
          if (attribute.Load(xml, kernel) && !string.IsNullOrEmpty(attribute.F_ATTRIBUTE_ID))
          {
            attribute.IsObjectAttribute = this.IsObject;
            attribute.OwnerID = this.UniqueID;
            attribute.MultiValuesCount = 1;
            attribute.Normalize();
            attribute.IsObjectAttribute = this.IsObject;
            attribute.OwnerID = this.UniqueID;
            string dictAttrKey = attribute.DictAttrKey;
            ImAttribute imAttribute = (ImAttribute) null;
            object obj;
            if (this._attributes.TryGetValue(dictAttrKey, out obj))
              imAttribute = obj as ImAttribute;
            if (imAttribute == null)
              this._attributes[attribute.DictAttrKey] = (object) attribute;
            else if (imAttribute.CanMergeWith((IImAttribute) attribute))
            {
              imAttribute.MergeWith((IImAttribute) attribute);
              ++imAttribute.MultiValuesCount;
            }
          }
        }
        else
        {
          this.SetAsString(upperInvariant, xml.ReadInnerXml());
          if (string.IsNullOrEmpty(this.UniqueID) && !string.IsNullOrEmpty(str))
            this.UniqueID = str;
        }
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
      SQLiteCommand sqLiteCommand = new SQLiteCommand($"INSERT INTO '{this.SQLTableName}' ({stringBuilder1}) VALUES ({stringBuilder2});", connection, transaction);
      sqLiteCommand.Parameters.AddRange(values);
      asSql.Add(sqLiteCommand);
    }
    finally
    {
      objectPoolScope2.Dispose();
      objectPoolScope1.Dispose();
    }
    foreach (KeyValuePair<string, object> attribute in (IEnumerable<KeyValuePair<string, object>>) this._attributes)
    {
      if (attribute.Value is ImBaseElement imBaseElement)
        asSql.AddRange((IEnumerable<SQLiteCommand>) imBaseElement.GetAsSQL(connection, transaction, tables));
    }
    return (IList<SQLiteCommand>) asSql;
  }
}
