// Decompiled with JetBrains decompiler
// Type: XmlReaderAPI.Kernel.Kernel
// Assembly: Intermech.IpsXmlViewer.XmlReaderAPI, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 197F841C-E5B9-4815-BCCD-9737649DED5C
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.XmlReaderAPI.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.XmlReaderAPI.xml

using Intermech.IpsXmlViewer.Interfaces;
using Intermech.Pools;
using Intermech.Text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Text;
using XmlReaderAPI.Data;
using XmlReaderAPI.MetaData;
using XmlReaderAPI.Properties;
using XmlReaderAPI.ReaderAPI.Common;

#nullable disable
namespace XmlReaderAPI.Kernel;

/// <summary>Микроядро, позволяющее читать информацию из индекса</summary>
public sealed class Kernel : IDisposable, IKernel
{
  /// <summary>Получить уникальное внутреннее целочисленное значение</summary>
  private long _uniqueId = 9000000000000000000;
  /// <summary>Объект для безопасного доступа</summary>
  private readonly object _lockGetUniqueId = new object();
  /// <summary>Кэш схем для таблиц SqlLite</summary>
  private readonly SchemaCacheHolder _schemaCacheHolder = new SchemaCacheHolder();
  /// <summary>База данных</summary>
  private XmlReaderAPI.RDBMS.Indexer _indexer;
  /// <summary>Контейнер сервисов</summary>
  private readonly AdvancedServiceContainer _services = new AdvancedServiceContainer();

  /// <summary>Получить уникальное внутреннее целочисленное значение</summary>
  public long GetUniqueID
  {
    get
    {
      lock (this._lockGetUniqueId)
      {
        ++this._uniqueId;
        return this._uniqueId;
      }
    }
  }

  /// <summary>База данных</summary>
  public IIndexer Indexer
  {
    [DebuggerStepThrough] get => (IIndexer) this._indexer;
    private set
    {
      this._indexer = value as XmlReaderAPI.RDBMS.Indexer;
      if (this._indexer == null)
        return;
      this._indexer.services.AdvancedProvider = this.Services;
    }
  }

  /// <summary>Метаданные</summary>
  public IImMetaData MetaData => this.Indexer?.MetaData;

  /// <summary>Контейнер сервисов</summary>
  public IServiceProvider Services
  {
    [DebuggerStepThrough] get => (IServiceProvider) this._services;
  }

  /// <summary>Создать микроядро</summary>
  /// <param name="indexer">База данных</param>
  /// <param name="services">Контейнер сервисов</param>
  public Kernel(XmlReaderAPI.RDBMS.Indexer indexer, IServiceProvider services)
  {
    this.Open(indexer, services);
  }

  /// <summary>Инициализировать микроядро</summary>
  /// <param name="indexer">База данных</param>
  /// <param name="aServices">Контейнер сервисов</param>
  public void Open(XmlReaderAPI.RDBMS.Indexer indexer, IServiceProvider aServices)
  {
    this._services.AdvancedProvider = aServices;
    this.Indexer = (IIndexer) (indexer ?? throw new ArgumentNullException(nameof (indexer)));
  }

  /// <summary>Закрыть ресурсы</summary>
  public void Close()
  {
    this._indexer?.Dispose();
    this._indexer = (XmlReaderAPI.RDBMS.Indexer) null;
    this._services.Dispose();
  }

  /// <summary>Освободить ресурсы</summary>
  public void Dispose() => this.Close();

  /// <summary>Выполнить набор команд в рамках одной транзакции</summary>
  /// <param name="commands">Набор команд, который должен быть выполнен в рамках одной транзакции</param>
  /// <param name="throwIfError">true - генерировать исключение при ошибке</param>
  /// <returns>true - все команды выполнены успешно</returns>
  public bool Execute(IList<SQLiteCommand> commands, bool throwIfError)
  {
    if (commands == null || commands.Count == 0)
      return true;
    if (this._indexer.Connection == null || this._indexer.Connection.State != ConnectionState.Open || this._indexer.Tables == null)
    {
      if (throwIfError)
        throw new ArgumentNullException("[connection] / [this.Execute]");
      return false;
    }
    SQLiteTransaction sqLiteTransaction = (SQLiteTransaction) null;
    try
    {
      sqLiteTransaction = this._indexer.Connection.BeginTransaction();
      foreach (SQLiteCommand command in (IEnumerable<SQLiteCommand>) commands)
      {
        command.Transaction = sqLiteTransaction;
        command.ExecuteNonQuery();
      }
    }
    catch
    {
      sqLiteTransaction?.Rollback();
      sqLiteTransaction = (SQLiteTransaction) null;
      if (!throwIfError)
        return false;
      throw;
    }
    finally
    {
      sqLiteTransaction?.Commit();
    }
    return true;
  }

  /// <summary>
  /// Удалить из таблицы строки, содержащие указанное значение заданного поля
  /// </summary>
  /// <param name="tableName">Имя изменяемой таблицы</param>
  /// <param name="keyFieldName">Имя ключевого поля</param>
  /// <param name="keyFieldValue">Значение ключевого поля</param>
  /// <param name="throwIfNotFound">true - генерировать исключение, если были ошибки</param>
  /// <returns>true - строки были удалены</returns>
  public bool DeleteRows(
    string tableName,
    string keyFieldName,
    object keyFieldValue,
    bool throwIfNotFound)
  {
    List<Tuple<string, object>> field2Values = new List<Tuple<string, object>>()
    {
      new Tuple<string, object>(keyFieldName, keyFieldValue)
    };
    return this.DeleteRows(tableName, field2Values, throwIfNotFound);
  }

  /// <summary>
  /// Удалить из таблицы строки, содержащие указанное значение заданного поля
  /// </summary>
  /// <param name="tableName">Имя изменяемой таблицы</param>
  /// <param name="field2Values">Имя - Значение ключевого поля</param>
  /// <param name="throwIfNotFound">true - генерировать исключение, если были ошибки</param>
  /// <returns>true - строки были удалены</returns>
  public bool DeleteRows(
    string tableName,
    List<Tuple<string, object>> field2Values,
    bool throwIfNotFound)
  {
    if (this._indexer.Connection == null || this._indexer.Connection.State != ConnectionState.Open || this._indexer.Tables == null || string.IsNullOrEmpty(tableName) || !this._indexer.Tables.ContainsKey(tableName.ToUpperInvariant()) || field2Values == null || field2Values.Count == 0)
    {
      if (throwIfNotFound)
        throw new ArgumentNullException("[connection] / [this.DeleteRows]");
      return false;
    }
    foreach (Tuple<string, object> field2Value in field2Values)
    {
      if (string.IsNullOrEmpty(field2Value.Item1) || !this._indexer.Tables[tableName.ToUpperInvariant()].Contains(field2Value.Item1.ToUpperInvariant()) || field2Value.Item2 == null)
      {
        if (throwIfNotFound)
          throw new ArgumentException("[connection] / [this.DeleteRows] / [field2ValueList]");
        return false;
      }
    }
    SQLiteTransaction transaction = (SQLiteTransaction) null;
    try
    {
      transaction = this._indexer.Connection.BeginTransaction();
      using (SQLiteCommand sqLiteCommand = new SQLiteCommand("", this._indexer.Connection, transaction))
      {
        sqLiteCommand.CommandText = $"DELETE FROM '{tableName}' WHERE ";
        for (int index = 0; index < field2Values.Count; ++index)
        {
          Tuple<string, object> field2Value = field2Values[index];
          if (index != 0)
            sqLiteCommand.CommandText += " AND ";
          sqLiteCommand.CommandText += string.Format(" {0} = @{0} ", (object) field2Value.Item1);
          sqLiteCommand.Parameters.AddWithValue("@" + field2Value.Item1, field2Value.Item2);
        }
        sqLiteCommand.CommandType = CommandType.Text;
        return sqLiteCommand.ExecuteNonQuery() > 0;
      }
    }
    catch
    {
      transaction?.Rollback();
      transaction = (SQLiteTransaction) null;
      if (!throwIfNotFound)
        return false;
      throw;
    }
    finally
    {
      transaction?.Commit();
    }
  }

  /// <summary>
  /// Получить значение поля у записи со значением указанного ключа
  /// </summary>
  /// <param name="tableName">Имя таблицы</param>
  /// <param name="keyFieldName">Имя ключевого поля для поиска</param>
  /// <param name="keyFieldValue">Значение ключевого поля для поиска</param>
  /// <param name="fieldName">Имя запрашиваемого поля</param>
  /// <param name="throwIfNotFound">true - генерировать исключение, если объект не был найден</param>
  /// <returns>Значение или null</returns>
  public object GetFieldValue(
    string tableName,
    string keyFieldName,
    object keyFieldValue,
    string fieldName,
    bool throwIfNotFound)
  {
    if (this._indexer.Connection != null && this._indexer.Connection.State == ConnectionState.Open && this._indexer.Tables != null && !string.IsNullOrEmpty(tableName) && !string.IsNullOrEmpty(keyFieldName) && !string.IsNullOrEmpty(fieldName) && keyFieldValue != null)
    {
      if (keyFieldValue != DBNull.Value)
      {
        try
        {
          using (SQLiteCommand sqLiteCommand = new SQLiteCommand(this._indexer.Connection))
          {
            sqLiteCommand.CommandText = string.Format("SELECT {0} FROM '{1}' WHERE {2} = @{2}", (object) fieldName, (object) tableName, (object) keyFieldName);
            sqLiteCommand.Parameters.AddWithValue("@" + keyFieldName, keyFieldValue);
            sqLiteCommand.CommandType = CommandType.Text;
            return sqLiteCommand.ExecuteScalar();
          }
        }
        catch
        {
          if (!throwIfNotFound)
            return (object) null;
          throw;
        }
      }
    }
    if (throwIfNotFound)
      throw new ArgumentNullException("[connection] / [this.GetFieldValue]");
    return (object) null;
  }

  /// <summary>
  /// Установить значение поля у записи со значением указанного ключа
  /// </summary>
  /// <param name="tableName">Имя таблицы</param>
  /// <param name="keyFieldName">Имя ключевого поля для поиска</param>
  /// <param name="keyFieldValue">Значение ключевого поля для поиска</param>
  /// <param name="fieldName">Имя устанавливаемого поля</param>
  /// <param name="fieldValue">Значение</param>
  /// <param name="throwIfNotFound">true - генерировать исключение, если объект не был найден</param>
  /// <returns>Значение или null</returns>
  public bool SetFieldValue(
    string tableName,
    string keyFieldName,
    object keyFieldValue,
    string fieldName,
    object fieldValue,
    bool throwIfNotFound)
  {
    if (this._indexer.Connection != null && this._indexer.Connection.State == ConnectionState.Open && this._indexer.Tables != null && !string.IsNullOrEmpty(tableName) && !string.IsNullOrEmpty(keyFieldName) && !string.IsNullOrEmpty(fieldName) && keyFieldValue != null && keyFieldValue != DBNull.Value)
    {
      if (fieldValue != null)
      {
        try
        {
          using (SQLiteCommand sqLiteCommand = new SQLiteCommand(this._indexer.Connection))
          {
            sqLiteCommand.CommandText = string.Format("UPDATE '{0}' SET {1} = @{1} WHERE {2} = @{2}", (object) tableName, (object) fieldName, (object) keyFieldName);
            sqLiteCommand.Parameters.AddWithValue("@" + fieldName, fieldValue);
            sqLiteCommand.Parameters.AddWithValue("@" + keyFieldName, keyFieldValue);
            sqLiteCommand.CommandType = CommandType.Text;
            return sqLiteCommand.ExecuteNonQuery() > 0;
          }
        }
        catch
        {
          if (!throwIfNotFound)
            return false;
          throw;
        }
      }
    }
    if (throwIfNotFound)
      throw new ArgumentNullException("[connection] / [this.SetFieldValue]");
    return false;
  }

  /// <summary>Получить список типов атрибутов</summary>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <returns>Список типов атрибутов или null</returns>
  public List<IImAttributeType> GetAttributeTypes(bool throwIfError)
  {
    if (this._indexer.Connection != null && this._indexer.Connection.State == ConnectionState.Open)
    {
      if (this._indexer.Tables != null)
      {
        try
        {
          List<IImAttributeType> attributeTypes;
          using (SQLiteCommand sqLiteCommand = new SQLiteCommand(this._indexer.Connection))
          {
            attributeTypes = new List<IImAttributeType>();
            sqLiteCommand.CommandType = CommandType.Text;
            sqLiteCommand.CommandText = $"SELECT O.* FROM {"IMS_ATTRIBUTE_TYPES"} O ORDER BY O.{"F_ATTRIBUTE_ID"}";
            using (CachedDataReader cachedDataReader = new CachedDataReader((IDataReader) sqLiteCommand.ExecuteReader(), this._schemaCacheHolder.GetCacheItem("IMS_ATTRIBUTE_TYPES")))
            {
              while (cachedDataReader.Read())
              {
                ImAttributeType imAttributeType = new ImAttributeType();
                IDataRecord dataRecord = (IDataRecord) cachedDataReader;
                for (int i = 0; i < dataRecord.FieldCount; ++i)
                  imAttributeType[dataRecord.GetName(i)] = dataRecord.GetValue(i);
                attributeTypes.Add((IImAttributeType) imAttributeType);
              }
            }
          }
          return attributeTypes;
        }
        catch
        {
          if (!throwIfError)
            return (List<IImAttributeType>) null;
          throw;
        }
      }
    }
    if (throwIfError)
      throw new ArgumentNullException("[connection] / [this.Tables]");
    return (List<IImAttributeType>) null;
  }

  /// <summary>Получить список типов объектов</summary>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <returns>Список типов объектов или null</returns>
  public List<IImObjectType> GetObjectTypes(bool throwIfError)
  {
    if (this._indexer.Connection != null && this._indexer.Connection.State == ConnectionState.Open)
    {
      if (this._indexer.Tables != null)
      {
        try
        {
          List<IImObjectType> objectTypes;
          using (SQLiteCommand sqLiteCommand = new SQLiteCommand(this._indexer.Connection))
          {
            objectTypes = new List<IImObjectType>();
            sqLiteCommand.CommandType = CommandType.Text;
            sqLiteCommand.CommandText = $"SELECT O.* FROM {"IMS_OBJECT_TYPES"} O ORDER BY O.{"F_OBJ_TYPE"}";
            using (CachedDataReader cachedDataReader = new CachedDataReader((IDataReader) sqLiteCommand.ExecuteReader(), this._schemaCacheHolder.GetCacheItem("IMS_OBJECT_TYPES")))
            {
              while (cachedDataReader.Read())
              {
                ImObjectType imObjectType = new ImObjectType();
                IDataRecord dataRecord = (IDataRecord) cachedDataReader;
                for (int i = 0; i < dataRecord.FieldCount; ++i)
                  imObjectType[dataRecord.GetName(i)] = dataRecord.GetValue(i);
                objectTypes.Add((IImObjectType) imObjectType);
              }
            }
          }
          return objectTypes;
        }
        catch
        {
          if (!throwIfError)
            return (List<IImObjectType>) null;
          throw;
        }
      }
    }
    if (throwIfError)
      throw new ArgumentNullException("[connection] / [this.Tables]");
    return (List<IImObjectType>) null;
  }

  /// <summary>Получить список типов связей</summary>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <returns>Список типов связей или null</returns>
  public List<IImRelationType> GetRelationTypes(bool throwIfError)
  {
    if (this._indexer.Connection != null && this._indexer.Connection.State == ConnectionState.Open)
    {
      if (this._indexer.Tables != null)
      {
        try
        {
          List<IImRelationType> relationTypes;
          using (SQLiteCommand sqLiteCommand = new SQLiteCommand(this._indexer.Connection))
          {
            relationTypes = new List<IImRelationType>();
            sqLiteCommand.CommandType = CommandType.Text;
            sqLiteCommand.CommandText = $"SELECT O.* FROM {"IMS_RELATION_TYPES"} O ORDER BY O.{"F_RELATION_TYPE"}";
            using (CachedDataReader cachedDataReader = new CachedDataReader((IDataReader) sqLiteCommand.ExecuteReader(), this._schemaCacheHolder.GetCacheItem("IMS_RELATION_TYPES")))
            {
              while (cachedDataReader.Read())
              {
                ImRelationType imRelationType = new ImRelationType();
                IDataRecord dataRecord = (IDataRecord) cachedDataReader;
                for (int i = 0; i < dataRecord.FieldCount; ++i)
                  imRelationType[dataRecord.GetName(i)] = dataRecord.GetValue(i);
                relationTypes.Add((IImRelationType) imRelationType);
              }
            }
          }
          return relationTypes;
        }
        catch
        {
          if (!throwIfError)
            return (List<IImRelationType>) null;
          throw;
        }
      }
    }
    if (throwIfError)
      throw new ArgumentNullException("[connection] / [this.Tables]");
    return (List<IImRelationType>) null;
  }

  /// <summary>Загрузить атрибуты в указанный объект/связь</summary>
  /// <param name="item">Объект/связь</param>
  /// <param name="throwIfNotFound">true - генерировать исключение, если объект не был найден</param>
  /// <returns>Описание объекта или null</returns>
  public void ReadItemAttributes(IImDataElement item, bool throwIfNotFound)
  {
    if (this._indexer?.Connection != null && this._indexer.Connection.State == ConnectionState.Open && this._indexer.Tables != null && item != null)
    {
      if (!string.IsNullOrEmpty(item.UniqueID))
      {
        try
        {
          using (SQLiteCommand sqLiteCommand = new SQLiteCommand(this._indexer.Connection))
          {
            sqLiteCommand.CommandText = string.Format("SELECT * FROM '{0}' WHERE {1} = @{1} AND {2} = @{2} ORDER BY {3}, {4}, {5}", (object) "IMS_ATTRIBUTES", (object) "OWNER_ID", (object) "IS_OBJECT", (object) "F_ATTRIBUTE_ID", (object) "F_INLIST_ID", (object) "F_VALUE");
            sqLiteCommand.Parameters.AddWithValue("@OWNER_ID", (object) item.UniqueID);
            sqLiteCommand.Parameters.AddWithValue("@IS_OBJECT", (object) (item.IsObject ? 1 : 0));
            sqLiteCommand.CommandType = CommandType.Text;
            using (CachedDataReader cachedDataReader = new CachedDataReader((IDataReader) sqLiteCommand.ExecuteReader(), this._schemaCacheHolder.GetCacheItem("IMS_ATTRIBUTES")))
            {
              int fieldCount = cachedDataReader.FieldCount;
              string[] strArray = new string[fieldCount];
              for (int i = 0; i < cachedDataReader.FieldCount; ++i)
                strArray[i] = cachedDataReader.GetName(i);
              while (cachedDataReader.Read())
              {
                IDataRecord dataRecord = (IDataRecord) cachedDataReader;
                ImAttribute attribute = new ImAttribute()
                {
                  IsObjectAttribute = item.IsObject
                };
                for (int i = 0; i < fieldCount; ++i)
                  attribute[strArray[i]] = dataRecord.GetValue(i);
                attribute.Normalize();
                string dictAttrKey = attribute.DictAttrKey;
                if (item[dictAttrKey] is IImAttribute imAttribute)
                {
                  if (imAttribute != attribute && imAttribute.CanMergeWith((IImAttribute) attribute))
                    imAttribute.MergeWith((IImAttribute) attribute);
                }
                else
                  item[dictAttrKey] = (object) attribute;
              }
              return;
            }
          }
        }
        catch
        {
          if (!throwIfNotFound)
            return;
          throw;
        }
      }
    }
    if (throwIfNotFound)
      throw new ArgumentNullException("[connection] / [this.Tables] / [item]");
  }

  /// <summary>Удаление атрибутов</summary>
  /// <param name="ownerId">Ид. владельца (объекта / связи)</param>
  /// <param name="isObject">Признак объекта</param>
  /// <param name="throwIfNotFound"></param>
  /// <returns></returns>
  public bool DeleteAttributes(long ownerId, bool isObject, bool throwIfNotFound)
  {
    return this.DeleteRows("IMS_ATTRIBUTES", new List<Tuple<string, object>>()
    {
      new Tuple<string, object>("OWNER_ID", (object) ownerId),
      new Tuple<string, object>("IS_OBJECT", (object) isObject)
    }, throwIfNotFound);
  }

  /// <summary>
  /// Получить идентификатор версии объекта из индекса на основании уникального глобального идентификатора его версии
  /// </summary>
  /// <param name="F_OBJECTGUID">Уникальный глобальный идентификатор версии объекта</param>
  /// <param name="throwIfNotFound">true - генерировать исключение, если объект не был найден</param>
  /// <returns>Guid версии объекта или null</returns>
  public long GetObjectID(Guid F_OBJECTGUID, bool throwIfNotFound)
  {
    if (this._indexer.Connection != null && this._indexer.Connection.State == ConnectionState.Open && this._indexer.Tables != null)
    {
      if (!(F_OBJECTGUID == Guid.Empty))
      {
        try
        {
          using (SQLiteCommand sqLiteCommand = new SQLiteCommand(this._indexer.Connection))
          {
            sqLiteCommand.CommandText = string.Format("SELECT {0} FROM '{1}' WHERE {2} = @{2}", (object) "F_OBJECT_ID", (object) "IMS_OBJECTS", (object) nameof (F_OBJECTGUID));
            sqLiteCommand.Parameters.AddWithValue("@F_OBJECTGUID", (object) F_OBJECTGUID.ToString());
            sqLiteCommand.CommandType = CommandType.Text;
            object obj = sqLiteCommand.ExecuteScalar();
            long result;
            if (obj != null && obj != DBNull.Value && long.TryParse(obj.ToString(), out result))
              return result;
            if (throwIfNotFound)
              throw new Exception(string.Format(Resources.exceptionObjectNotFound, (object) F_OBJECTGUID));
            return 0;
          }
        }
        catch
        {
          if (!throwIfNotFound)
            return 0;
          throw;
        }
      }
    }
    if (throwIfNotFound)
      throw new ArgumentNullException("[connection] / [this.Tables] / [F_OBJECTGUID]");
    return 0;
  }

  /// <summary>
  /// Получить Guid версии объекта из индекса на основании идентификатора его версии
  /// </summary>
  /// <param name="F_OBJECT_ID">Идентификатор версии объекта</param>
  /// <param name="throwIfNotFound">true - генерировать исключение, если объект не был найден</param>
  /// <returns>Guid версии объекта или null</returns>
  public Guid GetObjectGuid(long F_OBJECT_ID, bool throwIfNotFound)
  {
    if (this._indexer.Connection != null && this._indexer.Connection.State == ConnectionState.Open && this._indexer.Tables != null && F_OBJECT_ID != 0L)
    {
      if (F_OBJECT_ID != -1L)
      {
        try
        {
          using (SQLiteCommand sqLiteCommand = new SQLiteCommand(this._indexer.Connection))
          {
            sqLiteCommand.CommandText = string.Format("SELECT {0} FROM '{1}' WHERE {2} = @{2}", (object) "F_OBJECTGUID", (object) "IMS_OBJECTS", (object) nameof (F_OBJECT_ID));
            sqLiteCommand.Parameters.AddWithValue("@F_OBJECT_ID", (object) F_OBJECT_ID);
            sqLiteCommand.CommandType = CommandType.Text;
            object obj = sqLiteCommand.ExecuteScalar();
            if (obj != null && obj != DBNull.Value && GuidHelper.IsGuid(obj.ToString()))
              return new Guid(obj.ToString());
            if (throwIfNotFound)
              throw new Exception(string.Format(Resources.exceptionObjectNotFound, (object) F_OBJECT_ID));
            return Guid.Empty;
          }
        }
        catch
        {
          if (!throwIfNotFound)
            return Guid.Empty;
          throw;
        }
      }
    }
    if (throwIfNotFound)
      throw new ArgumentNullException("[connection] / [this.Tables]");
    return Guid.Empty;
  }

  /// <summary>
  /// Получить описание объекта из индекса на основании идентификатора его версии
  /// </summary>
  /// <param name="F_OBJECT_ID">Идентификатор версии объекта</param>
  /// <param name="onlyObligatory">true - читаются только обязательные атрибуты (минус один SQL-запрос)</param>
  /// <param name="throwIfNotFound">true - генерировать исключение, если объект не был найден</param>
  /// <returns>Описание объекта или null</returns>
  public IImObject GetObject(long F_OBJECT_ID, bool onlyObligatory, bool throwIfNotFound)
  {
    if (this._indexer.Connection != null && this._indexer.Connection.State == ConnectionState.Open)
    {
      if (this._indexer.Tables != null)
      {
        try
        {
          ImObject imObject;
          using (SQLiteCommand sqLiteCommand = new SQLiteCommand(this._indexer.Connection))
          {
            sqLiteCommand.CommandText = string.Format("SELECT * FROM '{0}' WHERE {1} = @{1}", (object) "IMS_OBJECTS", (object) nameof (F_OBJECT_ID));
            sqLiteCommand.Parameters.AddWithValue("@F_OBJECT_ID", (object) F_OBJECT_ID);
            sqLiteCommand.CommandType = CommandType.Text;
            using (CachedDataReader cachedDataReader = new CachedDataReader((IDataReader) sqLiteCommand.ExecuteReader(), this._schemaCacheHolder.GetCacheItem("IMS_OBJECTS")))
            {
              imObject = new ImObject();
              if (cachedDataReader.Read())
              {
                IDataRecord dataRecord = (IDataRecord) cachedDataReader;
                for (int i = 0; i < dataRecord.FieldCount; ++i)
                  imObject[dataRecord.GetName(i)] = dataRecord.GetValue(i);
              }
              else
              {
                if (throwIfNotFound)
                  throw new Exception(string.Format(Resources.exceptionObjectNotFound, (object) F_OBJECT_ID));
                return (IImObject) null;
              }
            }
          }
          if (!onlyObligatory)
            this.ReadItemAttributes((IImDataElement) imObject, throwIfNotFound);
          return (IImObject) imObject;
        }
        catch
        {
          if (!throwIfNotFound)
            return (IImObject) null;
          throw;
        }
      }
    }
    if (throwIfNotFound)
      throw new ArgumentNullException("[connection] / [this.Tables]");
    return (IImObject) null;
  }

  /// <summary>
  /// Получить описание объекта из индекса на основании уникального глобального идентификатора его версии
  /// </summary>
  /// <param name="onlyObligatory">true - читаются только обязательные атрибуты (минус один SQL-запрос)</param>
  /// <param name="F_OBJECTGUID">Уникальный глобальный идентификатор версии объекта</param>
  /// <param name="throwIfNotFound">true - генерировать исключение, если объект не был найден</param>
  /// <returns>Описание объекта или null</returns>
  public IImObject GetObject(Guid F_OBJECTGUID, bool onlyObligatory, bool throwIfNotFound)
  {
    if (this._indexer.Connection != null && this._indexer.Connection.State == ConnectionState.Open && this._indexer.Tables != null)
    {
      if (!(F_OBJECTGUID == Guid.Empty))
      {
        try
        {
          ImObject imObject;
          using (SQLiteCommand sqLiteCommand = new SQLiteCommand(this._indexer.Connection))
          {
            sqLiteCommand.CommandText = string.Format("SELECT * FROM '{0}' WHERE {1} = @{1}", (object) "IMS_OBJECTS", (object) nameof (F_OBJECTGUID));
            sqLiteCommand.Parameters.AddWithValue("@F_OBJECTGUID", (object) F_OBJECTGUID.ToString());
            sqLiteCommand.CommandType = CommandType.Text;
            using (CachedDataReader cachedDataReader = new CachedDataReader((IDataReader) sqLiteCommand.ExecuteReader(), this._schemaCacheHolder.GetCacheItem("IMS_OBJECTS")))
            {
              imObject = new ImObject();
              if (cachedDataReader.Read())
              {
                IDataRecord dataRecord = (IDataRecord) cachedDataReader;
                for (int i = 0; i < dataRecord.FieldCount; ++i)
                  imObject[dataRecord.GetName(i)] = dataRecord.GetValue(i);
              }
              else
              {
                if (throwIfNotFound)
                  throw new Exception(string.Format(Resources.exceptionObjectNotFound, (object) F_OBJECTGUID));
                return (IImObject) imObject;
              }
            }
          }
          if (!onlyObligatory)
            this.ReadItemAttributes((IImDataElement) imObject, throwIfNotFound);
          return (IImObject) imObject;
        }
        catch
        {
          if (!throwIfNotFound)
            return (IImObject) null;
          throw;
        }
      }
    }
    if (throwIfNotFound)
      throw new ArgumentNullException("[connection] / [this.Tables] / [F_OBJECTGUID]");
    return (IImObject) null;
  }

  /// <summary>Создать в индексе запись объекта и его атрибутов</summary>
  /// <param name="obj">Описание создаваемой версии объекта</param>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <returns>Описание созданной версии объекта</returns>
  public IImObject CreateObject(IImObject obj, bool throwIfError)
  {
    if (this._indexer.Connection == null || this._indexer.Connection.State != ConnectionState.Open || this._indexer.Tables == null)
    {
      if (throwIfError)
        throw new ArgumentNullException("[connection] / [this.CreateObject]");
      return (IImObject) null;
    }
    ImObject imObject = new ImObject((object) obj);
    if (string.IsNullOrEmpty(imObject.F_OBJECT_ID))
      imObject.F_OBJECT_ID = this.GetUniqueID.ToString();
    List<SQLiteCommand> commands = new List<SQLiteCommand>(1024 /*0x0400*/);
    commands.AddRange((IEnumerable<SQLiteCommand>) imObject.GetAsSQL(this._indexer.Connection, (SQLiteTransaction) null, this._indexer.Tables));
    return !this.Execute((IList<SQLiteCommand>) commands, throwIfError) ? (IImObject) null : this.GetObject(Convert.ToInt64(imObject.F_OBJECT_ID), false, throwIfError);
  }

  /// <summary>Удалить объект с указанным идентификатором из индекса</summary>
  /// <param name="F_OBJECT_ID">Идентификатор удаляемой версии объекта</param>
  /// <param name="throwIfNotFound">true - генерировать исключение, если были ошибки</param>
  /// <returns>true - объект был удалён</returns>
  public bool DeleteObject(long F_OBJECT_ID, bool throwIfNotFound)
  {
    return this.DeleteRows("IMS_OBJECTS", nameof (F_OBJECT_ID), (object) F_OBJECT_ID, throwIfNotFound) && this.DeleteAttributes(F_OBJECT_ID, true, throwIfNotFound);
  }

  /// <summary>Удалить объект с указанным идентификатором из индекса</summary>
  /// <param name="F_OBJECTGUID">Уникальный глобальный идентификатор удаляемой версии объекта</param>
  /// <param name="throwIfNotFound">true - генерировать исключение, если были ошибки</param>
  /// <returns>true - объект был удалён</returns>
  public bool DeleteObject(Guid F_OBJECTGUID, bool throwIfNotFound)
  {
    long objectId = this.GetObjectID(F_OBJECTGUID, throwIfNotFound);
    return this.DeleteRows("IMS_OBJECTS", nameof (F_OBJECTGUID), (object) F_OBJECTGUID, throwIfNotFound) && this.DeleteAttributes(objectId, true, throwIfNotFound);
  }

  private IEnumerable<IImObject> DoGetObjects(
    string sqlCondition,
    string sqlOrder,
    bool throwIfError,
    bool readAttributes = false)
  {
    if (this._indexer.Connection == null || this._indexer.Connection.State != ConnectionState.Open || this._indexer.Tables == null)
    {
      if (throwIfError)
        throw new ArgumentNullException("[connection] / [this.Tables]");
    }
    else
    {
      using (SQLiteCommand command = new SQLiteCommand(this._indexer.Connection))
      {
        command.CommandType = CommandType.Text;
        command.CommandText = $"SELECT O.* FROM IMS_OBJECTS O {(!string.IsNullOrEmpty(sqlCondition) ? " WHERE " + sqlCondition : string.Empty)} {(!string.IsNullOrEmpty(sqlOrder) ? " ORDER BY " + sqlOrder : string.Empty)}";
        using (CachedDataReader reader = new CachedDataReader((IDataReader) command.ExecuteReader(), this._schemaCacheHolder.GetCacheItem("IMS_OBJECTS")))
        {
          string[] readerFields = new string[reader.FieldCount];
          for (int i = 0; i < reader.FieldCount; ++i)
            readerFields[i] = reader.GetName(i);
          while (reader.Read())
          {
            IDataRecord dataRecord = (IDataRecord) reader;
            ImObject imObject = new ImObject();
            for (int i = 0; i < readerFields.Length; ++i)
              imObject[readerFields[i]] = dataRecord.GetValue(i);
            if (readAttributes)
              this.ReadItemAttributes((IImDataElement) imObject, throwIfError);
            yield return (IImObject) imObject;
          }
          readerFields = (string[]) null;
        }
      }
    }
  }

  /// <summary>
  /// Получить список объектов (только с обязательными атрибутами по умолчанию) указанного типа
  /// </summary>
  /// <param name="sqlCondition">Условие на объекты</param>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <param name="readAttributes">Загружать в объекты все атрибуты (выполняется много дополнительных запросов)</param>
  /// <returns>Список объектов (только с обязательными атрибутами) указанного типа или null</returns>
  public IEnumerable<IImObject> GetObjects(
    string sqlCondition,
    string sqlOrder,
    bool throwIfError,
    bool readAttributes = false)
  {
    try
    {
      return this.DoGetObjects(sqlCondition, sqlOrder, throwIfError, readAttributes);
    }
    catch
    {
      if (!throwIfError)
        return (IEnumerable<IImObject>) null;
      throw;
    }
  }

  /// <summary>
  /// Получить список объектов (только с обязательными атрибутами по умолчанию) указанного типа
  /// </summary>
  /// <param name="F_OBJECT_TYPE">Идентификатор типа связи объекта (-1 - все объекты)</param>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <param name="readAttributes">Загружать в объекты все атрибуты (выполняется много дополнительных запросов)</param>
  /// <returns>Список объектов (только с обязательными атрибутами) указанного типа или null</returns>
  public List<IImObject> GetObjects(int F_OBJECT_TYPE, bool throwIfError, bool readAttributes = false)
  {
    if (this._indexer.Connection != null && this._indexer.Connection.State == ConnectionState.Open)
    {
      if (this._indexer.Tables != null)
      {
        try
        {
          List<IImObject> objects;
          using (SQLiteCommand sqLiteCommand = new SQLiteCommand(this._indexer.Connection))
          {
            objects = new List<IImObject>();
            sqLiteCommand.CommandType = CommandType.Text;
            if (F_OBJECT_TYPE == -1)
            {
              sqLiteCommand.CommandText = "SELECT O.* FROM IMS_OBJECTS O";
            }
            else
            {
              sqLiteCommand.CommandText = string.Format("SELECT O.* FROM {0} O WHERE O.{1} = @{1} ORDER BY O.{1}", (object) "IMS_OBJECTS", (object) nameof (F_OBJECT_TYPE));
              sqLiteCommand.Parameters.AddWithValue("@F_OBJECT_TYPE", (object) F_OBJECT_TYPE);
            }
            using (CachedDataReader cachedDataReader = new CachedDataReader((IDataReader) sqLiteCommand.ExecuteReader(), this._schemaCacheHolder.GetCacheItem("IMS_OBJECTS")))
            {
              string[] strArray = new string[cachedDataReader.FieldCount];
              for (int i = 0; i < cachedDataReader.FieldCount; ++i)
                strArray[i] = cachedDataReader.GetName(i);
              while (cachedDataReader.Read())
              {
                IDataRecord dataRecord = (IDataRecord) cachedDataReader;
                ImObject imObject = new ImObject();
                for (int i = 0; i < strArray.Length; ++i)
                  imObject[strArray[i]] = dataRecord.GetValue(i);
                if (readAttributes)
                  this.ReadItemAttributes((IImDataElement) imObject, throwIfError);
                objects.Add((IImObject) imObject);
              }
            }
          }
          return objects;
        }
        catch
        {
          if (!throwIfError)
            return (List<IImObject>) null;
          throw;
        }
      }
    }
    if (throwIfError)
      throw new ArgumentNullException("[connection] / [this.Tables]");
    return (List<IImObject>) null;
  }

  /// <summary>
  /// Получить идентификатор связи из индекса на основании её уникального глобального идентификатора
  /// </summary>
  /// <param name="F_PRJ_GUID">Уникальный глобальный идентификатор связи</param>
  /// <param name="throwIfNotFound">true - генерировать исключение, если связь не была найдена</param>
  /// <returns>Guid версии объекта или null</returns>
  public long GetRelationID(Guid F_PRJ_GUID, bool throwIfNotFound)
  {
    if (this._indexer.Connection != null && this._indexer.Connection.State == ConnectionState.Open && this._indexer.Tables != null)
    {
      if (!(F_PRJ_GUID == Guid.Empty))
      {
        try
        {
          using (SQLiteCommand sqLiteCommand = new SQLiteCommand(this._indexer.Connection))
          {
            sqLiteCommand.CommandText = string.Format("SELECT {0} FROM '{1}' WHERE {2} = @{2}", (object) "F_PRJLINK_ID", (object) "IMS_RELATIONS", (object) nameof (F_PRJ_GUID));
            sqLiteCommand.Parameters.AddWithValue("@F_PRJ_GUID", (object) F_PRJ_GUID.ToString());
            sqLiteCommand.CommandType = CommandType.Text;
            object obj = sqLiteCommand.ExecuteScalar();
            long result;
            if (obj != null && obj != DBNull.Value && long.TryParse(obj.ToString(), out result))
              return result;
            if (throwIfNotFound)
              throw new Exception(string.Format(Resources.exceptionRelationNotFound, (object) F_PRJ_GUID));
            return 0;
          }
        }
        catch
        {
          if (!throwIfNotFound)
            return 0;
          throw;
        }
      }
    }
    if (throwIfNotFound)
      throw new ArgumentNullException("[connection] / [this.Tables] / [F_PRJ_GUID]");
    return 0;
  }

  /// <summary>
  /// Получить Guid связи из индекса на основании её идентификатора
  /// </summary>
  /// <param name="F_PRJLINK_ID">Идентификатор связи</param>
  /// <param name="throwIfNotFound">true - генерировать исключение, если связь не была найдена</param>
  /// <returns>Guid связи или Guid.Empty</returns>
  public Guid GetRelationGuid(long F_PRJLINK_ID, bool throwIfNotFound)
  {
    if (this._indexer.Connection != null && this._indexer.Connection.State == ConnectionState.Open)
    {
      if (this._indexer.Tables != null)
      {
        try
        {
          using (SQLiteCommand sqLiteCommand = new SQLiteCommand(this._indexer.Connection))
          {
            sqLiteCommand.CommandText = string.Format("SELECT {0} FROM '{1}' WHERE {2} = @{2}", (object) "F_PRJ_GUID", (object) "IMS_RELATIONS", (object) nameof (F_PRJLINK_ID));
            sqLiteCommand.Parameters.AddWithValue("@F_PRJLINK_ID", (object) F_PRJLINK_ID);
            sqLiteCommand.CommandType = CommandType.Text;
            object obj = sqLiteCommand.ExecuteScalar();
            if (obj != null && obj != DBNull.Value && GuidHelper.IsGuid(obj.ToString()))
              return new Guid(obj.ToString());
            if (throwIfNotFound)
              throw new Exception(string.Format(Resources.exceptionRelationNotFound, (object) F_PRJLINK_ID));
            return Guid.Empty;
          }
        }
        catch
        {
          if (!throwIfNotFound)
            return Guid.Empty;
          throw;
        }
      }
    }
    if (throwIfNotFound)
      throw new ArgumentNullException("[connection] / [this.Tables]");
    return Guid.Empty;
  }

  /// <summary>
  /// Получить описание связи из индекса на основании её идентификатора
  /// </summary>
  /// <param name="F_PRJLINK_ID">Идентификатор связи</param>
  /// <param name="onlyObligatory">true - читаются только обязательные атрибуты (минус один SQL-запрос)</param>
  /// <param name="throwIfNotFound">true - генерировать исключение, если связь не была найдена</param>
  /// <returns>Описание связи или null</returns>
  public IImRelation GetRelation(long F_PRJLINK_ID, bool onlyObligatory, bool throwIfNotFound)
  {
    if (this._indexer.Connection != null && this._indexer.Connection.State == ConnectionState.Open)
    {
      if (this._indexer.Tables != null)
      {
        try
        {
          ImRelation relation;
          using (SQLiteCommand sqLiteCommand = new SQLiteCommand(this._indexer.Connection))
          {
            sqLiteCommand.CommandText = string.Format("SELECT * FROM '{0}' WHERE {1} = @{1}", (object) "IMS_RELATIONS", (object) nameof (F_PRJLINK_ID));
            sqLiteCommand.Parameters.AddWithValue("@F_PRJLINK_ID", (object) F_PRJLINK_ID);
            sqLiteCommand.CommandType = CommandType.Text;
            using (CachedDataReader cachedDataReader = new CachedDataReader((IDataReader) sqLiteCommand.ExecuteReader(), this._schemaCacheHolder.GetCacheItem("IMS_RELATIONS")))
            {
              relation = new ImRelation();
              if (cachedDataReader.Read())
              {
                IDataRecord dataRecord = (IDataRecord) cachedDataReader;
                for (int i = 0; i < dataRecord.FieldCount; ++i)
                  relation[dataRecord.GetName(i)] = dataRecord.GetValue(i);
              }
              else
              {
                if (throwIfNotFound)
                  throw new Exception(string.Format(Resources.exceptionRelationNotFound, (object) F_PRJLINK_ID));
                return (IImRelation) null;
              }
            }
          }
          if (!onlyObligatory)
            this.ReadItemAttributes((IImDataElement) relation, throwIfNotFound);
          return (IImRelation) relation;
        }
        catch
        {
          if (!throwIfNotFound)
            return (IImRelation) null;
          throw;
        }
      }
    }
    if (throwIfNotFound)
      throw new ArgumentNullException("[connection] / [this.Tables]");
    return (IImRelation) null;
  }

  /// <summary>
  /// Получить описание связи из индекса на основании её глобального уникального идентификатора
  /// </summary>
  /// <param name="F_PRJ_GUID">Уникальный глобальный идентификатор связи</param>
  /// <param name="onlyObligatory">true - читаются только обязательные атрибуты (минус один SQL-запрос)</param>
  /// <param name="throwIfNotFound">true - генерировать исключение, если связь не была найдена</param>
  /// <returns>Описание связи или null</returns>
  public IImRelation GetRelation(Guid F_PRJ_GUID, bool onlyObligatory, bool throwIfNotFound)
  {
    if (this._indexer.Connection != null && this._indexer.Connection.State == ConnectionState.Open && this._indexer.Tables != null)
    {
      if (!(F_PRJ_GUID == Guid.Empty))
      {
        try
        {
          ImRelation relation;
          using (SQLiteCommand sqLiteCommand = new SQLiteCommand(this._indexer.Connection))
          {
            sqLiteCommand.CommandText = string.Format("SELECT * FROM '{0}' WHERE {1} = @{1}", (object) "IMS_RELATIONS", (object) nameof (F_PRJ_GUID));
            sqLiteCommand.Parameters.AddWithValue("@F_PRJ_GUID", (object) F_PRJ_GUID.ToString());
            sqLiteCommand.CommandType = CommandType.Text;
            using (CachedDataReader cachedDataReader = new CachedDataReader((IDataReader) sqLiteCommand.ExecuteReader(), this._schemaCacheHolder.GetCacheItem("IMS_RELATIONS")))
            {
              relation = new ImRelation();
              if (cachedDataReader.Read())
              {
                IDataRecord dataRecord = (IDataRecord) cachedDataReader;
                for (int i = 0; i < dataRecord.FieldCount; ++i)
                  relation[dataRecord.GetName(i)] = dataRecord.GetValue(i);
              }
              else
              {
                if (throwIfNotFound)
                  throw new Exception(string.Format(Resources.exceptionRelationNotFound, (object) F_PRJ_GUID));
                return (IImRelation) null;
              }
            }
          }
          if (!onlyObligatory)
            this.ReadItemAttributes((IImDataElement) relation, throwIfNotFound);
          return (IImRelation) relation;
        }
        catch
        {
          if (!throwIfNotFound)
            return (IImRelation) null;
          throw;
        }
      }
    }
    if (throwIfNotFound)
      throw new ArgumentNullException("[connection] / [this.Tables] / [F_PRJ_GUID]");
    return (IImRelation) null;
  }

  /// <summary>Создать в индексе запись связи и её атрибутов</summary>
  /// <param name="rel">Описание создаваемой связи</param>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <returns>Описание созданной связи</returns>
  public IImRelation CreateRelation(IImRelation rel, bool throwIfError)
  {
    if (this._indexer.Connection == null || this._indexer.Connection.State != ConnectionState.Open || this._indexer.Tables == null)
    {
      if (throwIfError)
        throw new ArgumentNullException("[connection] / [this.CreateRelation]");
      return (IImRelation) null;
    }
    ImRelation imRelation = new ImRelation((object) rel);
    if (string.IsNullOrEmpty(imRelation.GetAsString("F_PRJLINK_ID", string.Empty)))
      imRelation["F_PRJLINK_ID"] = (object) this.GetUniqueID.ToString();
    List<SQLiteCommand> commands = new List<SQLiteCommand>(1024 /*0x0400*/);
    commands.AddRange((IEnumerable<SQLiteCommand>) imRelation.GetAsSQL(this._indexer.Connection, (SQLiteTransaction) null, this._indexer.Tables));
    return !this.Execute((IList<SQLiteCommand>) commands, throwIfError) ? (IImRelation) null : this.GetRelation(Convert.ToInt64(imRelation["F_PRJLINK_ID"]), false, throwIfError);
  }

  /// <summary>Удалить связь с указанным идентификатором из индекса</summary>
  /// <param name="F_PRJLINK_ID">Идентификатор удаляемой связи</param>
  /// <param name="throwIfNotFound">true - генерировать исключение, если были ошибки</param>
  /// <returns>true - связь была удалена</returns>
  public bool DeleteRelation(long F_PRJLINK_ID, bool throwIfNotFound)
  {
    return this.DeleteRows("IMS_RELATIONS", nameof (F_PRJLINK_ID), (object) F_PRJLINK_ID, throwIfNotFound) && this.DeleteAttributes(F_PRJLINK_ID, false, throwIfNotFound);
  }

  /// <summary>
  /// Удалить связь с указанным уникальным глобальным идентификатором из индекса
  /// </summary>
  /// <param name="F_PRJ_GUID">Уникальный глобальный идентификатор удаляемой связи</param>
  /// <param name="throwIfNotFound">true - генерировать исключение, если были ошибки</param>
  /// <returns>true - связь была удалена</returns>
  public bool DeleteRelation(Guid F_PRJ_GUID, bool throwIfNotFound)
  {
    long relationId = this.GetRelationID(F_PRJ_GUID, throwIfNotFound);
    return this.DeleteRows("IMS_RELATIONS", nameof (F_PRJ_GUID), (object) F_PRJ_GUID, throwIfNotFound) && this.DeleteAttributes(relationId, false, throwIfNotFound);
  }

  /// <summary>
  /// Проверить наличие применяемости / состава у указанного объекта (по указанному типу связи либо по любым)
  /// </summary>
  /// <param name="F_OBJ_VALUE">Локальный идентификатор версии дочернего / родительского объекта</param>
  /// <param name="F_OBJ_FIELD">Наименование поля, которому будет вестись поиск объекта</param>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <param name="F_RELATION_TYPE">Проверяемый тип связи или Consts.UnknownIDx32, если требуется проверить любые типы связей</param>
  /// <returns>true - в составе есть как минимум одна связь указанного (или любого) типа</returns>
  internal bool HasComposition(
    long F_OBJ_VALUE,
    string F_OBJ_FIELD,
    bool throwIfError,
    int F_RELATION_TYPE = 0)
  {
    if (this._indexer.Connection != null && this._indexer.Connection.State == ConnectionState.Open)
    {
      if (this._indexer.Tables != null)
      {
        try
        {
          using (SQLiteCommand sqLiteCommand = new SQLiteCommand(this._indexer.Connection))
          {
            sqLiteCommand.CommandType = CommandType.Text;
            if (F_RELATION_TYPE == 0)
            {
              sqLiteCommand.CommandText = string.Format("SELECT MIN({0}) FROM {1} WHERE {2} = @{2}", (object) "F_PRJLINK_ID", (object) "IMS_RELATIONS", (object) F_OBJ_FIELD);
            }
            else
            {
              sqLiteCommand.CommandText = string.Format("SELECT MIN({0}) FROM {1} WHERE {2} = @{2} AND {3} = @{3}", (object) "F_PRJLINK_ID", (object) "IMS_RELATIONS", (object) F_OBJ_FIELD, (object) nameof (F_RELATION_TYPE));
              sqLiteCommand.Parameters.AddWithValue("@F_RELATION_TYPE", (object) F_RELATION_TYPE);
            }
            sqLiteCommand.Parameters.AddWithValue("@" + F_OBJ_FIELD, (object) F_OBJ_VALUE);
            object obj = sqLiteCommand.ExecuteScalar();
            return obj != null && obj != DBNull.Value;
          }
        }
        catch
        {
          if (!throwIfError)
            return false;
          throw;
        }
      }
    }
    if (throwIfError)
      throw new ArgumentNullException("[connection] / [this.Tables]");
    return false;
  }

  /// <summary>
  /// Получить список типов связей из применяемости / состава указанного объекта
  /// </summary>
  /// <param name="F_OBJ_VALUE">Локальный идентификатор версии дочернего / родительского объекта</param>
  /// <param name="F_OBJ_FIELD">Наименование поля, которому будет вестись поиск объекта</param>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <returns>Список типов связей из состава указанного родительского объекта</returns>
  internal List<IImRelationType> GetCompositionRelTypes(
    long F_OBJ_VALUE,
    string F_OBJ_FIELD,
    bool throwIfError)
  {
    List<IImRelationType> compositionRelTypes = new List<IImRelationType>();
    if (this._indexer.Connection != null && this._indexer.Connection.State == ConnectionState.Open)
    {
      if (this._indexer.Tables != null)
      {
        try
        {
          using (SQLiteCommand sqLiteCommand = new SQLiteCommand(this._indexer.Connection))
          {
            sqLiteCommand.CommandType = CommandType.Text;
            sqLiteCommand.CommandText = string.Format("SELECT DISTINCT {0} FROM {1} WHERE {2} = @{2} ORDER BY {0}", (object) "F_RELATION_TYPE", (object) "IMS_RELATIONS", (object) F_OBJ_FIELD);
            sqLiteCommand.Parameters.AddWithValue("@" + F_OBJ_FIELD, (object) F_OBJ_VALUE);
            using (CachedDataReader cachedDataReader = new CachedDataReader((IDataReader) sqLiteCommand.ExecuteReader()))
            {
              while (cachedDataReader.Read())
              {
                object obj = cachedDataReader.GetValue(0);
                try
                {
                  IImRelationType relationType = this._indexer.MetaData.GetRelationType(Convert.ToInt32(obj));
                  if (relationType != null)
                  {
                    if (!compositionRelTypes.Contains(relationType))
                      compositionRelTypes.Add(relationType);
                  }
                }
                catch
                {
                }
              }
            }
          }
          return compositionRelTypes;
        }
        catch
        {
          if (!throwIfError)
            return compositionRelTypes;
          throw;
        }
      }
    }
    if (throwIfError)
      throw new ArgumentNullException("[connection] / [this.Tables]");
    return compositionRelTypes;
  }

  /// <summary>
  /// Получить список объектов (только с обязательными атрибутами по умолчанию) верхнего уровня в составах
  /// </summary>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <param name="readAttributes">Загружать в объекты все атрибуты (выполняется много дополнительных запросов)</param>
  /// <returns>Список объектов (только с обязательными атрибутами) верхнего уровня в составах или null</returns>
  public List<IImObject> GetRootObjects(bool throwIfError, bool readAttributes = false)
  {
    if (this._indexer.Connection != null && this._indexer.Connection.State == ConnectionState.Open)
    {
      if (this._indexer.Tables != null)
      {
        try
        {
          List<IImObject> rootObjects;
          using (SQLiteCommand sqLiteCommand = new SQLiteCommand(this._indexer.Connection))
          {
            rootObjects = new List<IImObject>();
            sqLiteCommand.CommandText = string.Format("SELECT DISTINCT O.* FROM {1} O, {2} R WHERE R.{3} NOT IN (SELECT {4} FROM {2}) AND O.{0} = R.{3}", (object) "F_OBJECT_ID", (object) "IMS_OBJECTS", (object) "IMS_RELATIONS", (object) "F_PROJ_OBJ", (object) "F_PART_OBJ");
            sqLiteCommand.CommandType = CommandType.Text;
            using (CachedDataReader cachedDataReader = new CachedDataReader((IDataReader) sqLiteCommand.ExecuteReader(), this._schemaCacheHolder.GetCacheItem("IMS_OBJECTS")))
            {
              while (cachedDataReader.Read())
              {
                ImObject imObject = new ImObject();
                IDataRecord dataRecord = (IDataRecord) cachedDataReader;
                for (int i = 0; i < dataRecord.FieldCount; ++i)
                  imObject[dataRecord.GetName(i)] = dataRecord.GetValue(i);
                if (readAttributes)
                  this.ReadItemAttributes((IImDataElement) imObject, throwIfError);
                rootObjects.Add((IImObject) imObject);
              }
            }
          }
          return rootObjects;
        }
        catch
        {
          if (!throwIfError)
            return (List<IImObject>) null;
          throw;
        }
      }
    }
    if (throwIfError)
      throw new ArgumentNullException("[connection] / [this.Tables]");
    return (List<IImObject>) null;
  }

  /// <summary>
  /// Получить все связи указанного типа (или все связи из индекса)
  /// </summary>
  /// <param name="F_RELATION_TYPE">Тип связи (-1 - все типы связей)</param>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <param name="readAttributes">Загружать в объекты все атрибуты (выполняется много дополнительных запросов)</param>
  /// <returns></returns>
  private IEnumerable<IImRelation> DoGetRelations(
    string sqlConditions,
    string sqlOrder,
    bool throwIfError,
    bool readAttributes = false)
  {
    if (this._indexer.Connection == null || this._indexer.Connection.State != ConnectionState.Open || this._indexer.Tables == null)
    {
      if (throwIfError)
        throw new ArgumentNullException("[connection] / [this.Tables]");
    }
    else
    {
      using (SQLiteCommand command = new SQLiteCommand(this._indexer.Connection))
      {
        command.CommandType = CommandType.Text;
        if (string.IsNullOrEmpty(sqlConditions))
        {
          string empty = string.Empty;
        }
        else
        {
          string str = $" WHERE {sqlConditions} ";
        }
        command.CommandText = $"SELECT R.* FROM IMS_RELATIONS R {sqlConditions} {(!string.IsNullOrEmpty(sqlOrder) ? " ORDER BY " + sqlOrder : string.Empty)}";
        using (CachedDataReader reader = new CachedDataReader((IDataReader) command.ExecuteReader(), this._schemaCacheHolder.GetCacheItem("IMS_RELATIONS")))
        {
          string[] readerFields = new string[reader.FieldCount];
          for (int i = 0; i < reader.FieldCount; ++i)
            readerFields[i] = reader.GetName(i);
          while (reader.Read())
          {
            ImRelation relation = new ImRelation();
            IDataRecord dataRecord = (IDataRecord) reader;
            for (int i = 0; i < readerFields.Length; ++i)
              relation[readerFields[i]] = dataRecord.GetValue(i);
            if (readAttributes)
              this.ReadItemAttributes((IImDataElement) relation, throwIfError);
            yield return (IImRelation) relation;
          }
          readerFields = (string[]) null;
        }
      }
    }
  }

  /// <summary>
  /// Получить все связи указанного типа (или все связи из индекса)
  /// </summary>
  /// <param name="F_RELATION_TYPE">Тип связи (-1 - все типы связей)</param>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <param name="readAttributes">Загружать в объекты все атрибуты (выполняется много дополнительных запросов)</param>
  /// <returns></returns>
  public IEnumerable<IImRelation> GetRelations(
    string sqlConditions,
    string sqlOrder,
    bool throwIfError,
    bool readAttributes = false)
  {
    try
    {
      return this.DoGetRelations(sqlConditions, sqlOrder, throwIfError, readAttributes);
    }
    catch (Exception ex)
    {
      if (!throwIfError)
        return (IEnumerable<IImRelation>) null;
      throw;
    }
  }

  /// <summary>
  /// Получить все связи указанного типа (или все связи из индекса)
  /// </summary>
  /// <param name="F_RELATION_TYPE">Тип связи (-1 - все типы связей)</param>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <param name="readAttributes">Загружать в объекты все атрибуты (выполняется много дополнительных запросов)</param>
  /// <returns></returns>
  public IEnumerable<IImRelation> GetRelations(
    int F_RELATION_TYPE,
    bool throwIfError,
    bool readAttributes = false)
  {
    if (this._indexer.Connection == null || this._indexer.Connection.State != ConnectionState.Open || this._indexer.Tables == null)
    {
      if (throwIfError)
        throw new ArgumentNullException("[connection] / [this.Tables]");
    }
    else
    {
      using (SQLiteCommand command = new SQLiteCommand(this._indexer.Connection))
      {
        command.CommandType = CommandType.Text;
        if (F_RELATION_TYPE == -1)
        {
          command.CommandText = "SELECT R.* FROM IMS_RELATIONS R ORDER BY R.F_RELATION_TYPE";
        }
        else
        {
          command.CommandText = "SELECT R.* FROM IMS_RELATIONS R WHERE R.F_RELATION_TYPE = @F_RELATION_TYPE";
          command.Parameters.AddWithValue("@F_RELATION_TYPE", (object) F_RELATION_TYPE);
        }
        using (CachedDataReader reader = new CachedDataReader((IDataReader) command.ExecuteReader(), this._schemaCacheHolder.GetCacheItem("IMS_RELATIONS")))
        {
          string[] readerFields = new string[reader.FieldCount];
          for (int i = 0; i < reader.FieldCount; ++i)
            readerFields[i] = reader.GetName(i);
          while (reader.Read())
          {
            ImRelation relation = new ImRelation();
            IDataRecord dataRecord = (IDataRecord) reader;
            for (int i = 0; i < readerFields.Length; ++i)
              relation[readerFields[i]] = dataRecord.GetValue(i);
            if (readAttributes)
              this.ReadItemAttributes((IImDataElement) relation, throwIfError);
            yield return (IImRelation) relation;
          }
          readerFields = (string[]) null;
        }
      }
    }
  }

  /// <summary>
  /// Получить состав (только с обязательными атрибутами связей и объектов) указанного родительского объекта
  /// </summary>
  /// <param name="F_PROJ_ID">Уникальный глобальный идентификатор версии родительского объекта</param>
  /// <param name="F_RELATION_TYPE">Тип связи (-1 - все типы связей)</param>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <param name="readAttributes">Загружать в объекты все атрибуты (выполняется много дополнительных запросов)</param>
  /// <returns>Список объектов (только с обязательными атрибутами) верхнего уровня в составах или null</returns>
  public List<IImRelation> GetComposition(
    Guid F_PROJ_ID,
    int F_RELATION_TYPE,
    bool throwIfError,
    bool readAttributes = false)
  {
    return this.GetComposition(this.GetObjectID(F_PROJ_ID, throwIfError), F_RELATION_TYPE, throwIfError, readAttributes);
  }

  /// <summary>
  /// Получить состав (только с обязательными атрибутами связей и объектов) указанного родительского объекта
  /// </summary>
  /// <param name="F_PROJ_OBJ">Идентификатор версии родительского объекта</param>
  /// <param name="F_RELATION_TYPE">Тип связи (-1 - все типы связей)</param>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <param name="readAttributes">Загружать в объекты все атрибуты (выполняется много дополнительных запросов)</param>
  /// <returns>Список объектов (только с обязательными атрибутами) верхнего уровня в составах или null</returns>
  public List<IImRelation> GetComposition(
    long F_PROJ_OBJ,
    int F_RELATION_TYPE,
    bool throwIfError,
    bool readAttributes = false)
  {
    if (this._indexer.Connection != null && this._indexer.Connection.State == ConnectionState.Open)
    {
      if (this._indexer.Tables != null)
      {
        try
        {
          List<IImRelation> composition;
          using (SQLiteCommand sqLiteCommand = new SQLiteCommand(this._indexer.Connection))
          {
            composition = new List<IImRelation>();
            sqLiteCommand.CommandType = CommandType.Text;
            if (F_RELATION_TYPE == -1)
            {
              sqLiteCommand.CommandText = $"SELECT R.*, O.* FROM {"IMS_RELATIONS"} R, {"IMS_OBJECTS"} O WHERE R.{nameof (F_PROJ_OBJ)} = @F_PROJ_OBJ AND R.{"F_PART_OBJ"} = O.{"F_OBJECT_ID"} ORDER BY R.{nameof (F_RELATION_TYPE)}";
            }
            else
            {
              sqLiteCommand.CommandText = string.Format("SELECT R.*, O.* FROM {0} R, {1} O WHERE R.{2} = @{2} AND R.{5} = @{5} AND R.{3} = O.{4} ORDER BY R.{5}", (object) "IMS_RELATIONS", (object) "IMS_OBJECTS", (object) nameof (F_PROJ_OBJ), (object) "F_PART_OBJ", (object) "F_OBJECT_ID", (object) nameof (F_RELATION_TYPE));
              sqLiteCommand.Parameters.AddWithValue("@F_RELATION_TYPE", (object) F_RELATION_TYPE);
            }
            sqLiteCommand.Parameters.AddWithValue("@F_PROJ_OBJ", (object) F_PROJ_OBJ);
            using (CachedDataReader cachedDataReader = new CachedDataReader((IDataReader) sqLiteCommand.ExecuteReader()))
            {
              while (cachedDataReader.Read())
              {
                ImRelation imRelation = new ImRelation();
                IDataRecord dataRecord = (IDataRecord) cachedDataReader;
                for (int i = 0; i < dataRecord.FieldCount; ++i)
                  imRelation[dataRecord.GetName(i)] = dataRecord.GetValue(i);
                if (readAttributes)
                  this.ReadItemAttributes((IImDataElement) imRelation, throwIfError);
                composition.Add((IImRelation) imRelation);
              }
            }
          }
          return composition;
        }
        catch
        {
          if (!throwIfError)
            return (List<IImRelation>) null;
          throw;
        }
      }
    }
    if (throwIfError)
      throw new ArgumentNullException("[connection] / [this.Tables]");
    return (List<IImRelation>) null;
  }

  /// <summary>
  /// Получить список типов связей из состава указанного родительского объекта
  /// </summary>
  /// <param name="F_PROJ_OBJ">Локальный идентификатор версии родительского объекта</param>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <returns>Список типов связей из состава указанного родительского объекта</returns>
  public List<IImRelationType> GetCompositionRelTypes(long F_PROJ_OBJ, bool throwIfError)
  {
    return this.GetCompositionRelTypes(F_PROJ_OBJ, nameof (F_PROJ_OBJ), throwIfError);
  }

  /// <summary>
  /// Проверить наличие состава у указанного объекта (по указанному типу связи либо по любым)
  /// </summary>
  /// <param name="F_PROJ_OBJ">Локальный идентификатор версии родительского объекта</param>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <param name="F_RELATION_TYPE">Проверяемый тип связи или Consts.UnknownIDx32, если требуется проверить любые типы связей</param>
  /// <returns>true - в составе есть как минимум одна связь указанного (или любого) типа</returns>
  public bool HasComposition(long F_PROJ_OBJ, bool throwIfError, int F_RELATION_TYPE = 0)
  {
    return this.HasComposition(F_PROJ_OBJ, nameof (F_PROJ_OBJ), throwIfError, F_RELATION_TYPE);
  }

  /// <summary>
  /// Получить применяемость (только с обязательными атрибутами связей и объектов) указанного дочернего объекта
  /// </summary>
  /// <param name="F_PART_ID">Уникальный глобальный идентификатор версии родительского объекта</param>
  /// <param name="F_RELATION_TYPE">Тип связи (-1 - все типы связей)</param>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <param name="readAttributes">Загружать в объекты все атрибуты (выполняется много дополнительных запросов)</param>
  /// <returns>Список объектов (только с обязательными атрибутами) верхнего уровня в составах или null</returns>
  public List<IImRelation> GetApplicability(
    Guid F_PART_ID,
    int F_RELATION_TYPE,
    bool throwIfError,
    bool readAttributes = false)
  {
    return this.GetApplicability(this.GetObjectID(F_PART_ID, throwIfError), F_RELATION_TYPE, throwIfError, readAttributes);
  }

  /// <summary>
  /// Получить применяемость (только с обязательными атрибутами связей и объектов) указанного дочернего объекта
  /// </summary>
  /// <param name="F_PART_OBJ">Идентификатор версии родительского объекта</param>
  /// <param name="F_RELATION_TYPE">Тип связи (-1 - все типы связей)</param>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <param name="readAttributes">Загружать в объекты все атрибуты (выполняется много дополнительных запросов)</param>
  /// <returns>Список объектов (только с обязательными атрибутами) верхнего уровня в составах или null</returns>
  public List<IImRelation> GetApplicability(
    long F_PART_OBJ,
    int F_RELATION_TYPE,
    bool throwIfError,
    bool readAttributes = false)
  {
    if (this._indexer.Connection != null && this._indexer.Connection.State == ConnectionState.Open)
    {
      if (this._indexer.Tables != null)
      {
        try
        {
          List<IImRelation> applicability;
          using (SQLiteCommand sqLiteCommand = new SQLiteCommand(this._indexer.Connection))
          {
            applicability = new List<IImRelation>();
            sqLiteCommand.CommandType = CommandType.Text;
            if (F_RELATION_TYPE == -1)
            {
              sqLiteCommand.CommandText = string.Format("SELECT R.*, O.* FROM {0} R, {1} O WHERE R.{2} = @{2} AND R.{3} = O.{4} ORDER BY R.{5}", (object) "IMS_RELATIONS", (object) "IMS_OBJECTS", (object) nameof (F_PART_OBJ), (object) "F_PROJ_OBJ", (object) "F_OBJECT_ID", (object) nameof (F_RELATION_TYPE));
            }
            else
            {
              sqLiteCommand.CommandText = string.Format("SELECT R.*, O.* FROM {0} R, {1} O WHERE R.{2} = @{2} AND R.{5} = @{5} AND R.{3} = O.{4} ORDER BY R.{5}", (object) "IMS_RELATIONS", (object) "IMS_OBJECTS", (object) nameof (F_PART_OBJ), (object) "F_PROJ_OBJ", (object) "F_OBJECT_ID", (object) nameof (F_RELATION_TYPE));
              sqLiteCommand.Parameters.AddWithValue("@F_RELATION_TYPE", (object) F_RELATION_TYPE);
            }
            sqLiteCommand.Parameters.AddWithValue("@F_PART_OBJ", (object) F_PART_OBJ);
            using (CachedDataReader cachedDataReader = new CachedDataReader((IDataReader) sqLiteCommand.ExecuteReader()))
            {
              while (cachedDataReader.Read())
              {
                ImRelation imRelation = new ImRelation();
                IDataRecord dataRecord = (IDataRecord) cachedDataReader;
                for (int i = 0; i < dataRecord.FieldCount; ++i)
                  imRelation[dataRecord.GetName(i)] = dataRecord.GetValue(i);
                if (readAttributes)
                  this.ReadItemAttributes((IImDataElement) imRelation, throwIfError);
                applicability.Add((IImRelation) imRelation);
              }
            }
          }
          return applicability;
        }
        catch
        {
          if (!throwIfError)
            return (List<IImRelation>) null;
          throw;
        }
      }
    }
    if (throwIfError)
      throw new ArgumentNullException("[connection] / [this.Tables]");
    return (List<IImRelation>) null;
  }

  /// <summary>
  /// Получить список типов связей из состава указанного родительского объекта
  /// </summary>
  /// <param name="F_PART_OBJ">Локальный идентификатор версии дочернего объекта</param>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <returns>Список типов связей из состава указанного родительского объекта</returns>
  public List<IImRelationType> GetGetApplicabilityRelTypes(long F_PART_OBJ, bool throwIfError)
  {
    return this.GetCompositionRelTypes(F_PART_OBJ, nameof (F_PART_OBJ), throwIfError);
  }

  /// <summary>
  /// Проверить наличие применяемости у указанного объекта (по указанному типу связи либо по любым)
  /// </summary>
  /// <param name="F_PART_OBJ">Локальный идентификатор версии дочернего объекта</param>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <param name="F_RELATION_TYPE">Проверяемый тип связи или Consts.UnknownIDx32, если требуется проверить любые типы связей</param>
  /// <returns>true - в составе есть как минимум одна связь указанного (или любого) типа</returns>
  public bool HasApplicability(long F_PART_OBJ, bool throwIfError, int F_RELATION_TYPE = 0)
  {
    return this.HasComposition(F_PART_OBJ, nameof (F_PART_OBJ), throwIfError, F_RELATION_TYPE);
  }

  /// <summary>Загрузить метаданные из индекса</summary>
  public void LoadMetaData()
  {
    if (this._indexer == null)
      return;
    this._indexer.MetaData = (IImMetaData) new ImMetaData((object) this);
  }

  /// <summary>
  /// Подготовить список параметров и значений для условия WHERE
  /// </summary>
  /// <param name="whereConditions">Пары "Имя параметра", значение параметра, которые будут добавлены в условие WHERE</param>
  /// <returns>Пары "Имя параметра", значение параметра, которые будут добавлены в условие WHERE</returns>
  internal List<Tuple<string, object>> PrepareWhereParams(params object[] whereConditions)
  {
    List<Tuple<string, object>> tupleList = (List<Tuple<string, object>>) null;
    if (whereConditions != null && whereConditions.Length > 1)
    {
      tupleList = new List<Tuple<string, object>>(whereConditions.Length / 2 + 1);
      int index1 = 0;
      while (index1 < whereConditions.Length)
      {
        object whereCondition1 = whereConditions[index1];
        int index2 = index1 + 1;
        if (index2 < whereConditions.Length)
        {
          object whereCondition2 = whereConditions[index2];
          index1 = index2 + 1;
          if (whereCondition1 != null && whereCondition2 != null)
          {
            string str = Convert.ToString(whereCondition1);
            if (!string.IsNullOrEmpty(str))
              tupleList.Add(new Tuple<string, object>(str, whereCondition2));
          }
        }
        else
          break;
      }
    }
    return tupleList;
  }

  /// <summary>
  /// Подготовить строку условий WHERE для указанных параметров и их значений, объединённых по AND
  /// </summary>
  /// <param name="pars">Пары "Имя параметра", значение параметра, которые будут добавлены в условие WHERE</param>
  /// <param name="operation">Логическая операция</param>
  /// <returns>Строка условий WHERE для указанных параметров и их значений, объединённых по AND</returns>
  internal string PrepareWhereString(List<Tuple<string, object>> pars, string operation = "AND")
  {
    if (pars == null || pars.Count == 0)
      return string.Empty;
    using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(4096 /*0x1000*/))
    {
      StringBuilder stringBuilder = objectPoolScope.Object;
      for (int index = 0; index < pars.Count; ++index)
      {
        stringBuilder.Append("'");
        stringBuilder.Append(pars[index].Item1);
        stringBuilder.Append("' = @");
        stringBuilder.Append(pars[index].Item1);
        stringBuilder.Append(index < pars.Count - 1 ? $" {operation} " : " ");
      }
      return stringBuilder.ToString();
    }
  }

  /// <summary>Выполнить замену значений полей в таблице</summary>
  /// <param name="tableName">Имя таблицы</param>
  /// <param name="fieldSource">Имя исходного поля, по значению которого выполняется поиск</param>
  /// <param name="valueSource">Исходное искомое значение</param>
  /// <param name="fieldDest">Имя изменяемого поля</param>
  /// <param name="valueDest">Значение в изменяемом поле</param>
  /// <param name="whereConditions">Пары "Имя параметра", значение параметра, которые будут добавлены в условие WHERE</param>
  /// <returns>true - действие выполнено успешно</returns>
  public bool UpdateTableFields(
    string tableName,
    string fieldSource,
    object valueSource,
    string fieldDest,
    object valueDest,
    params object[] whereConditions)
  {
    if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(fieldSource) || string.IsNullOrEmpty(fieldDest))
      return false;
    List<Tuple<string, object>> pars = this.PrepareWhereParams(whereConditions);
    string str = this.PrepareWhereString(pars);
    try
    {
      using (SQLiteCommand command = new SQLiteCommand(this._indexer.Connection))
      {
        command.CommandText = $"UPDATE {tableName} SET '{fieldDest}' = @p1 WHERE {fieldSource} = @p2 {(string.IsNullOrEmpty(str) ? (object) string.Empty : (object) "AND")} {str}";
        command.Parameters.AddWithValue("@p1", valueDest);
        command.Parameters.AddWithValue("@p2", valueSource);
        pars?.ForEach((Action<Tuple<string, object>>) (par => command.Parameters.AddWithValue("@" + par.Item1, par.Item2)));
        command.CommandType = CommandType.Text;
        command.ExecuteNonQuery();
        return true;
      }
    }
    catch
    {
      return false;
    }
  }

  /// <summary>
  /// Получить список объектов, на которые есть ссылки (только с обязательными атрибутами объектов)
  /// </summary>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <param name="readAttributes">Загружать в объекты все атрибуты (выполняется много дополнительных запросов)</param>
  /// <returns>Список объектов (только с обязательными атрибутами) или null</returns>
  public List<IImObject> GetReferencedObjects(bool throwIfError, bool readAttributes = false)
  {
    if (this._indexer.Connection != null && this._indexer.Connection.State == ConnectionState.Open)
    {
      if (this._indexer.Tables != null)
      {
        try
        {
          List<IImObject> referencedObjects;
          using (SQLiteCommand sqLiteCommand = new SQLiteCommand(this._indexer.Connection))
          {
            referencedObjects = new List<IImObject>();
            sqLiteCommand.CommandType = CommandType.Text;
            sqLiteCommand.CommandText = string.Format("SELECT O.* FROM {0} O WHERE O.{1} IN (SELECT DISTINCT A.{2} FROM {3} A WHERE A.{4} = @{4} AND A.{2} NOTNULL ORDER BY A.{2}) ORDER BY O.{5}", (object) "IMS_OBJECTS", (object) "F_OBJECT_ID", (object) "F_INTEGER_VALUE", (object) "IMS_ATTRIBUTES", (object) "F_ATTRIBUTE_TYPE", (object) "F_OBJECT_TYPE");
            sqLiteCommand.Parameters.AddWithValue("@F_ATTRIBUTE_TYPE", (object) FieldTypes.ftObjectLink);
            using (CachedDataReader cachedDataReader = new CachedDataReader((IDataReader) sqLiteCommand.ExecuteReader(), this._schemaCacheHolder.GetCacheItem("IMS_OBJECTS")))
            {
              while (cachedDataReader.Read())
              {
                ImObject imObject = new ImObject();
                IDataRecord dataRecord = (IDataRecord) cachedDataReader;
                for (int i = 0; i < dataRecord.FieldCount; ++i)
                  imObject[dataRecord.GetName(i)] = dataRecord.GetValue(i);
                if (readAttributes)
                  this.ReadItemAttributes((IImDataElement) imObject, throwIfError);
                referencedObjects.Add((IImObject) imObject);
              }
            }
          }
          return referencedObjects;
        }
        catch
        {
          if (!throwIfError)
            return (List<IImObject>) null;
          throw;
        }
      }
    }
    if (throwIfError)
      throw new ArgumentNullException("[connection] / [this.Tables]");
    return (List<IImObject>) null;
  }

  /// <summary>
  /// Получить список объектов типа "Пользователи", на которые есть ссылки у других объектов
  /// в полях F_OWNER_ID и F_CHKOUT_BY (только с обязательными атрибутами объектов)
  /// </summary>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <param name="readAttributes">Загружать в объекты все атрибуты (выполняется много дополнительных запросов)</param>
  /// <returns>Список объектов (только с обязательными атрибутами) или null</returns>
  public List<IImObject> GetUserReferences(bool throwIfError, bool readAttributes = false)
  {
    if (this._indexer.Connection != null && this._indexer.Connection.State == ConnectionState.Open)
    {
      if (this._indexer.Tables != null)
      {
        try
        {
          List<IImObject> userReferences;
          using (SQLiteCommand sqLiteCommand = new SQLiteCommand(this._indexer.Connection))
          {
            userReferences = new List<IImObject>();
            sqLiteCommand.CommandType = CommandType.Text;
            sqLiteCommand.CommandText = string.Format("SELECT DISTINCT * FROM {1} WHERE ({0} IN (SELECT DISTINCT {2} FROM {1})) OR ({0} IN (SELECT DISTINCT {3} FROM {1})) ORDER BY {0}", (object) "F_OBJECT_ID", (object) "IMS_OBJECTS", (object) "F_OWNER_ID", (object) "F_CHKOUT_BY");
            sqLiteCommand.Parameters.AddWithValue("@F_ATTRIBUTE_TYPE", (object) FieldTypes.ftObjectLink);
            using (CachedDataReader cachedDataReader = new CachedDataReader((IDataReader) sqLiteCommand.ExecuteReader(), this._schemaCacheHolder.GetCacheItem("IMS_OBJECTS")))
            {
              while (cachedDataReader.Read())
              {
                ImObject imObject = new ImObject();
                IDataRecord dataRecord = (IDataRecord) cachedDataReader;
                for (int i = 0; i < dataRecord.FieldCount; ++i)
                  imObject[dataRecord.GetName(i)] = dataRecord.GetValue(i);
                if (readAttributes)
                  this.ReadItemAttributes((IImDataElement) imObject, throwIfError);
                userReferences.Add((IImObject) imObject);
              }
            }
          }
          return userReferences;
        }
        catch
        {
          if (!throwIfError)
            return (List<IImObject>) null;
          throw;
        }
      }
    }
    if (throwIfError)
      throw new ArgumentNullException("[connection] / [this.Tables]");
    return (List<IImObject>) null;
  }

  /// <summary>Связать объект базы данных индекса с объектом IPS</summary>
  /// <param name="xmlObjectId">Идентификатор версии объекта в базе данных индекса</param>
  /// <param name="ipsObjectId">Идентификатор версии объекта в базе данных IPS</param>
  /// <param name="ipsObjTypeId">Идентификатор типа объекта в базе данных IPS</param>
  public void LinkWithIPSObject(long xmlObjectId, long ipsObjectId, int ipsObjTypeId)
  {
    this.UpdateTableFields("IMS_RELATIONS", "F_PROJ_OBJ", (object) xmlObjectId, "IPS_F_PROJ_OBJ", (object) ipsObjectId);
    this.UpdateTableFields("IMS_RELATIONS", "F_PART_OBJ", (object) xmlObjectId, "IPS_F_PART_OBJ", (object) ipsObjectId);
    this.UpdateTableFields("IMS_OBJECTS", "F_OBJECT_ID", (object) xmlObjectId, "IPS_F_OBJECT_ID", (object) ipsObjectId);
    if (ipsObjTypeId == -1)
      return;
    this.UpdateTableFields("IMS_OBJECTS", "F_OBJECT_ID", (object) xmlObjectId, "IPS_F_OBJ_TYPE", (object) ipsObjTypeId);
  }
}
