// Decompiled with JetBrains decompiler
// Type: XmlReaderAPI.Data.ImAttribute
// Assembly: Intermech.IpsXmlViewer.XmlReaderAPI, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 197F841C-E5B9-4815-BCCD-9737649DED5C
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.XmlReaderAPI.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.XmlReaderAPI.xml

using Intermech.Common;
using Intermech.IpsXmlViewer.Interfaces;
using Intermech.Pools;
using Intermech.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using XmlReaderAPI.Common;
using XmlReaderAPI.MetaData;
using XmlReaderAPI.Properties;

#nullable disable
namespace XmlReaderAPI.Data;

/// <summary>Атрибут объекта/связи</summary>
[Description("Атрибут объекта/связи")]
[DebuggerDisplay("[{F_ATTRIBUTE_ID}] \"{Text}\"")]
[XmlRoot("ATTRIBUTE")]
public sealed class ImAttribute : 
  ImCompositeAttrElement,
  IImAttribute,
  IImCompositeAttrElement,
  IImCompositeElement,
  IImBaseElement,
  IAssignable,
  ICloneable,
  IDisplayable,
  IComparable<ImAttribute>
{
  /// <summary>
  /// 
  /// </summary>
  public const int OptimizedCapacity = 10;

  /// <summary>Атрибут принадлежит объекту</summary>
  public bool IsObjectAttribute
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.GetAsBoolean("IS_OBJECT", true);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this.SetAsBoolean("IS_OBJECT", value);
  }

  /// <summary>
  /// Идентификатор версии объекта/связи - владельца атрибута
  /// </summary>
  public string OwnerID
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.GetAsString("OWNER_ID", (string) null);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this.SetAsString("OWNER_ID", value);
  }

  /// <summary>Создать пустой экземпляр класса</summary>
  public ImAttribute()
    : this(10)
  {
  }

  /// <summary>Создать пустой экземпляр класса</summary>
  public ImAttribute(int capacity)
    : base(capacity)
  {
  }

  /// <summary>
  /// Создать пустой экземпляр класса и заполнить его информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public ImAttribute(object source)
    : this()
  {
    this.Assign(source);
  }

  public override string ToString() => this.Text;

  /// <summary>
  /// Имя таблицы SQL, в которой хранится содержимое элемента
  /// </summary>
  public string SQLTableName
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => "IMS_ATTRIBUTES";
  }

  /// <summary>Имя атрибута, в котором хранится содержимое элемента</summary>
  public override string MainAttrName
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => "ATTRIBUTE";
  }

  /// <summary>Загрузить содержимое объекта из документа XML</summary>
  /// <param name="xml">Документ XML</param>
  /// <param name="kernel">Микроядро</param>
  /// <returns>true - узел считал содержимое корректно</returns>
  public override bool Load(XmlReader xml, IKernel kernel)
  {
    this.Clear();
    if (xml == null || xml.NodeType != XmlNodeType.Element || xml.Name.Trim().ToUpperInvariant() != "ATTRIBUTE")
      return false;
    int depth = xml.Depth;
    try
    {
      while (xml.Read())
      {
        if (xml.Depth == depth && xml.NodeType == XmlNodeType.EndElement)
          return true;
        if (xml.NodeType == XmlNodeType.Element)
        {
          if (xml.Depth <= depth)
            return true;
          this.SetAsString(xml.Name.Trim().ToUpperInvariant(), xml.ReadInnerXml());
        }
      }
    }
    finally
    {
      if (kernel?.Indexer?.MetaData != null)
      {
        IImAttributeType attributeType = kernel.Indexer.MetaData.GetAttributeType(this.GetAsInt32("F_ATTRIBUTE_ID", 0));
        if (attributeType != null)
          this["F_ATTRIBUTE_TYPE"] = (object) attributeType.F_ATTRIBUTE_TYPE;
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
    IDictionary<int, IDictionary<string, object>> dictionary = this.DeNormalize();
    HashSet<string> stringSet = new HashSet<string>(this._attributes.Count);
    IList<string> table = tables[this.SQLTableName.ToUpperInvariant().Trim()];
    foreach (KeyValuePair<string, object> attribute in (IEnumerable<KeyValuePair<string, object>>) this._attributes)
    {
      if (!(attribute.Value is IImAttribute))
      {
        string from = StringsHelper.ExtractFrom(attribute.Key.ToUpperInvariant().Trim(), string.Empty, ".");
        if (table.Contains(from))
          stringSet.Add(from);
      }
    }
    if (stringSet.Count == 0)
      return (IList<SQLiteCommand>) asSql;
    stringSet.Add("IS_OBJECT");
    stringSet.Add("OWNER_ID");
    int count = stringSet.Count;
    string fAttributeId = this.F_ATTRIBUTE_ID;
    List<SQLiteParameter> sqLiteParameterList = new List<SQLiteParameter>(count);
    foreach (KeyValuePair<int, IDictionary<string, object>> keyValuePair1 in (IEnumerable<KeyValuePair<int, IDictionary<string, object>>>) dictionary)
    {
      ObjectPoolScope<StringBuilder> objectPoolScope1 = TextServices.StringBuilderPool.Allocate(2046);
      ObjectPoolScope<StringBuilder> objectPoolScope2 = TextServices.StringBuilderPool.Allocate(2046);
      try
      {
        StringBuilder stringBuilder1 = objectPoolScope1.Object;
        StringBuilder stringBuilder2 = objectPoolScope2.Object;
        sqLiteParameterList.Clear();
        keyValuePair1.Value["F_ATTRIBUTE_ID"] = (object) fAttributeId;
        keyValuePair1.Value["IS_OBJECT"] = (object) this.IsObjectAttribute;
        keyValuePair1.Value["OWNER_ID"] = (object) this.OwnerID;
        keyValuePair1.Value["F_INLIST_ID"] = (object) keyValuePair1.Key.ToString();
        foreach (KeyValuePair<string, object> keyValuePair2 in (IEnumerable<KeyValuePair<string, object>>) keyValuePair1.Value)
        {
          string from = StringsHelper.ExtractFrom(keyValuePair2.Key.ToUpperInvariant().Trim(), string.Empty, ".");
          if (stringSet.Contains(from))
          {
            stringBuilder1.Append(sqLiteParameterList.Count > 0 ? ",'" : "'");
            stringBuilder1.Append(from);
            stringBuilder1.Append("'");
            stringBuilder2.Append(sqLiteParameterList.Count > 0 ? ",@" : "@");
            stringBuilder2.Append(from);
            sqLiteParameterList.Add(new SQLiteParameter("@" + from, keyValuePair2.Value));
          }
        }
        SQLiteCommand sqLiteCommand = new SQLiteCommand($"INSERT INTO '{this.SQLTableName}' ({stringBuilder1}) VALUES ({stringBuilder2});", connection, transaction);
        sqLiteCommand.Parameters.AddRange(sqLiteParameterList.ToArray());
        asSql.Add(sqLiteCommand);
      }
      finally
      {
        objectPoolScope1.Dispose();
        objectPoolScope2.Dispose();
      }
    }
    return (IList<SQLiteCommand>) asSql;
  }

  /// <summary>
  /// В свойстве хранится количество элементов, если атрибут является многозначным
  /// (значение меньше 2 - однозначный атрибут)
  /// </summary>
  public int MultiValuesCount { get; set; }

  /// <summary>Возвращается минимальное значение F_INLIST_ID*</summary>
  public int F_INLIST_ID
  {
    get
    {
      int val1 = -1;
      foreach (KeyValuePair<string, object> attribute in (IEnumerable<KeyValuePair<string, object>>) this._attributes)
      {
        if (attribute.Key.StartsWith(nameof (F_INLIST_ID)))
        {
          int result;
          int.TryParse(Convert.ToString(attribute.Value), out result);
          if (val1 == -1)
            val1 = result;
          if (val1 >= 0 && result >= 0)
            val1 = Math.Min(val1, result);
        }
      }
      return val1 < 0 ? 0 : val1;
    }
  }

  /// <summary>Возвращается значение F_ATTRIBUTE_ID</summary>
  public string F_ATTRIBUTE_ID
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.GetAsString(nameof (F_ATTRIBUTE_ID), string.Empty);
    }
  }

  /// <summary>Возвращается значение F_ATTRIBUTE_TYPE</summary>
  public int F_ATTRIBUTE_TYPE
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.GetAsInt32(nameof (F_ATTRIBUTE_TYPE), 0);
    }
  }

  /// <summary>
  /// Возвращается имя атрибута для хранения в словарике у объекта/связи
  /// </summary>
  public string DictAttrKey
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return ImAttributeType.GetDictAttrKey(this.F_ATTRIBUTE_ID);
    }
  }

  /// <summary>Очистить поля класса</summary>
  public override void Clear()
  {
    base.Clear();
    this.MultiValuesCount = 0;
  }

  /// <summary>
  /// Заполнить поля класса информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public override void Assign(object source)
  {
    if (this == source)
      return;
    base.Assign(source);
    if (!(source is ImAttribute imAttribute))
      return;
    this.MultiValuesCount = imAttribute.MultiValuesCount;
  }

  /// <summary>Текст, отображаемый на экране</summary>
  public override string Text
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.GetAsString("F_VALUE", string.Empty);
    }
  }

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="other">Объект для сравнения</param>
  /// <returns>-1, 0, 1</returns>
  public int CompareTo(ImAttribute other) => 0;

  /// <summary>
  /// Извлечь из словарика атрибутов все значения (многозначные, однозначные) и
  /// сформировать словарик, ключом которого являются значения F_INLIST_ID,
  /// а значениями - значения атрибута
  /// </summary>
  /// <returns>Словарик, ключом которого являются значения F_INLIST_ID,
  /// а значениями - значения атрибута</returns>
  public IDictionary<int, IDictionary<string, object>> DeNormalize()
  {
    IDictionary<int, IDictionary<string, object>> dictionary1 = (IDictionary<int, IDictionary<string, object>>) new Dictionary<int, IDictionary<string, object>>(this._attributes.Count);
    foreach (KeyValuePair<string, object> attribute in (IEnumerable<KeyValuePair<string, object>>) this._attributes)
    {
      string s = StringsHelper.ExtractFrom(attribute.Key, ".", string.Empty);
      if (s == attribute.Key)
        s = "0";
      int result;
      int.TryParse(s, out result);
      IDictionary<string, object> dictionary2;
      if (!dictionary1.TryGetValue(result, out dictionary2))
      {
        dictionary2 = dictionary1[result] = (IDictionary<string, object>) new Dictionary<string, object>(2);
        dictionary2["F_INLIST_ID"] = (object) result;
      }
      string from = StringsHelper.ExtractFrom(attribute.Key, string.Empty, ".");
      dictionary2[from] = attribute.Value;
    }
    return dictionary1;
  }

  /// <summary>
  /// Метод осуществляет переименование имён значений атрибута
  /// в соответствии со значением F_INLIST_ID
  /// </summary>
  public void Normalize()
  {
    List<int> inListIds = this.GetInListIDs();
    if (inListIds.Count > 1)
      return;
    int num = inListIds.Count == 1 ? inListIds[0] : this.F_INLIST_ID;
    if (num == 0)
      return;
    IDictionary<string, object> dictionary = (IDictionary<string, object>) new Dictionary<string, object>(this._attributes.Count);
    foreach (KeyValuePair<string, object> attribute in (IEnumerable<KeyValuePair<string, object>>) this._attributes)
    {
      if (attribute.Key == "F_ATTRIBUTE_ID" || attribute.Key.Contains<char>('.'))
        dictionary[attribute.Key] = attribute.Value;
      else
        dictionary[$"{attribute.Key}.{num}"] = attribute.Value;
    }
    this._attributes = dictionary;
  }

  /// <summary>
  /// Метод изучает значения атрибутов и возвращает список всех F_INLIST_ID.*
  /// </summary>
  /// <returns></returns>
  public List<int> GetInListIDs()
  {
    List<int> inListIds = new List<int>(1);
    foreach (KeyValuePair<string, object> attribute in (IEnumerable<KeyValuePair<string, object>>) this._attributes)
    {
      if (attribute.Key.StartsWith("F_INLIST_ID"))
      {
        string from = StringsHelper.ExtractFrom(attribute.Key, ".", string.Empty);
        if (from.ToLowerInvariant() == "F_INLIST_ID".ToLowerInvariant())
          from = Convert.ToString(attribute.Value);
        int result;
        int.TryParse(from, out result);
        if (!inListIds.Contains(result))
          inListIds.Add(result);
      }
    }
    inListIds.Sort();
    return inListIds;
  }

  /// <summary>
  /// Проверить, можно ли объединиться с указанным атрибутом
  /// (касается многозначных атрибутов)
  /// </summary>
  /// <param name="attribute">Проверяемый атрибут</param>
  /// <returns>true - объединение возможно</returns>
  public bool CanMergeWith(IImAttribute attribute)
  {
    return attribute != null && !(this.GetAsString("F_ATTRIBUTE_ID", "0") != attribute.GetAsString("F_ATTRIBUTE_ID", "0")) && this.F_INLIST_ID != attribute.F_INLIST_ID;
  }

  /// <summary>
  /// Выполнить объединение значений с указанным атрибутом.
  /// ВНИМАНИЕ!!! АТРИБУТЫ ДОЛЖНЫ БЫТЬ НОРМАЛИЗОВАНЫ!!!
  /// </summary>
  /// <param name="attribute">Атрибут, со значениями которого требуется выполнить объединение</param>
  public void MergeWith(IImAttribute attribute)
  {
    if (!this.CanMergeWith(attribute))
      throw new ArgumentException(Resources.exceptionCanNotMergeWithAttribute);
    foreach (KeyValuePair<string, object> attribute1 in (IEnumerable<KeyValuePair<string, object>>) attribute.Attributes)
      this._attributes[attribute1.Key] = attribute1.Value;
    if (this.MultiValuesCount == 0)
      this.MultiValuesCount = 2;
    else
      ++this.MultiValuesCount;
  }
}
