// Decompiled with JetBrains decompiler
// Type: XmlReaderAPI.RDBMS.Indexer
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
using System.Data.Common;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;
using XmlReaderAPI.Data;
using XmlReaderAPI.MetaData;

#nullable disable
namespace XmlReaderAPI.RDBMS;

/// <summary>
/// Класс, позволяющий сформировать базу данных индекса на основании
/// одного или нескольких потоков, содержащих XML
/// </summary>
public sealed class Indexer : IDisposable, IIndexer
{
  /// <summary>Метаданные</summary>
  private ImMetaData _metaData;
  /// <summary>Контейнер сервисов</summary>
  internal readonly AdvancedServiceContainer services = new AdvancedServiceContainer();
  /// <summary>База данных SQLite</summary>
  internal SQLiteConnection Connection;
  /// <summary>
  /// Коллекция таблиц базы данных, а также имена их колонок (все названия - в верхнем регистре)
  /// </summary>
  private IDictionary<string, IList<string>> _tables = (IDictionary<string, IList<string>>) new Dictionary<string, IList<string>>();

  /// <summary>Метаданные</summary>
  public IImMetaData MetaData
  {
    [DebuggerStepThrough] get => (IImMetaData) this._metaData;
    set
    {
      this._metaData = value as ImMetaData;
      if (this._metaData == null)
        return;
      this._metaData.services.AdvancedProvider = this.Services;
    }
  }

  /// <summary>Контейнер сервисов</summary>
  public IServiceProvider Services
  {
    [DebuggerStepThrough] get => (IServiceProvider) this.services;
  }

  /// <summary>База данных SQLite</summary>
  public object SQLConnection
  {
    [DebuggerStepThrough] get => (object) this.Connection;
    internal set => this.Connection = value as SQLiteConnection;
  }

  /// <summary>
  /// Коллекция таблиц базы данных, а также имена их колонок (все названия - в верхнем регистре)
  /// </summary>
  public IDictionary<string, IList<string>> Tables
  {
    [DebuggerStepThrough] get => this._tables;
    private set => this._tables = value;
  }

  /// <summary>Количество объектов</summary>
  public long Objects { get; private set; }

  /// <summary>Количество связей</summary>
  public long Relations { get; private set; }

  /// <summary>Количество типов атрибутов</summary>
  public int AttributeTypes { get; private set; }

  /// <summary>Количество типов объектов</summary>
  public int ObjectTypes { get; private set; }

  /// <summary>Количество типов связей</summary>
  public int RelationTypes { get; private set; }

  /// <summary>Создать обработчик-индексатор</summary>
  /// <param name="connection">База данных SQLite</param>
  public Indexer(SQLiteConnection connection)
  {
    if (connection == null)
      throw new ArgumentNullException();
    if (connection.State == ConnectionState.Closed)
      connection.Open();
    this.Connection = connection.State == ConnectionState.Open ? connection : throw new ArgumentNullException();
    SQLiteDataReader sqLiteDataReader1 = new SQLiteCommand("SELECT name FROM 'sqlite_master' WHERE type='table' ORDER BY name;", this.Connection).ExecuteReader();
    try
    {
      foreach (DbDataRecord dbDataRecord in (DbDataReader) sqLiteDataReader1)
        this.Tables[dbDataRecord["name"].ToString().ToUpperInvariant().Trim()] = (IList<string>) new List<string>(21);
    }
    finally
    {
      sqLiteDataReader1.Close();
    }
    foreach (KeyValuePair<string, IList<string>> table1 in (IEnumerable<KeyValuePair<string, IList<string>>>) this.Tables)
    {
      SQLiteDataReader sqLiteDataReader2 = new SQLiteCommand($"PRAGMA table_info({table1.Key});", this.Connection).ExecuteReader();
      try
      {
        IList<string> table2 = this.Tables[table1.Key];
        foreach (DbDataRecord dbDataRecord in (DbDataReader) sqLiteDataReader2)
          table2.Add(dbDataRecord["name"].ToString().ToUpperInvariant().Trim());
      }
      finally
      {
        sqLiteDataReader2.Close();
      }
    }
  }

  /// <summary>Освободить ресурсы</summary>
  public void Dispose()
  {
    this.MetaData = (IImMetaData) null;
    this.Tables = (IDictionary<string, IList<string>>) null;
    try
    {
      if (this.Connection != null)
      {
        SQLiteConnection.ClearPool(this.Connection);
        this.Connection.Close();
      }
    }
    catch
    {
    }
    this.Connection = (SQLiteConnection) null;
    this.services.Dispose();
  }

  /// <summary>Событие "Состояние индексатора"</summary>
  public event IndexProgressEventHandler OnIndexProgress;

  /// <summary>событие "Состояние индексатора"</summary>
  /// <param name="e">Аргументы события</param>
  private void FireOnIndexProgress(IndexerEventArgs e)
  {
    IndexProgressEventHandler onIndexProgress = this.OnIndexProgress;
    if (onIndexProgress == null)
      return;
    onIndexProgress((object) this, e);
  }

  /// <summary>
  /// Обработать исключение, возникшее при выполнении команды SQL
  /// </summary>
  /// <param name="e">Исключение</param>
  /// <param name="cmd">Команда SQL, вызвавшая исключение</param>
  /// <returns>Новое исключение, если требуется сформировать новый текст</returns>
  private Exception LogException(Exception e, SQLiteCommand cmd)
  {
    if (e == null || cmd == null)
      return e;
    using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(4096 /*0x1000*/))
    {
      StringBuilder stringBuilder = objectPoolScope.Object;
      stringBuilder.AppendLine("Ошибка выполнения команды SQL в индексе:");
      stringBuilder.AppendLine($"'{cmd.CommandText}'");
      if (cmd.Parameters != null && cmd.Parameters.Count > 0)
      {
        stringBuilder.AppendLine("Параметры:");
        for (int index = 0; index < cmd.Parameters.Count; ++index)
        {
          SQLiteParameter parameter = cmd.Parameters[index];
          string str = parameter.Value != null ? (parameter.Value != DBNull.Value ? Convert.ToString(parameter.Value) : "[NULL]") : "null";
          stringBuilder.AppendLine($" {parameter.ParameterName} = '{str}'");
        }
      }
      stringBuilder.AppendLine("===============");
      return new Exception(stringBuilder.ToString(), e);
    }
  }

  /// <summary>
  /// Сформировать индексы в указанной базе данных из коллекции потоков
  /// </summary>
  /// <param name="kernel">Микроядро</param>
  /// <param name="xmlStreams">Коллекция потоков, содержащих XML</param>
  public void ProcessStreams(IKernel kernel, params Stream[] xmlStreams)
  {
    if (this.Connection == null || this.Connection.State != ConnectionState.Open)
      throw new ArgumentNullException();
    if (kernel == null || xmlStreams == null || xmlStreams.Length == 0)
      return;
    SQLiteCommand currentCommand = (SQLiteCommand) null;
    for (int index = 0; index < xmlStreams.Length; ++index)
    {
      XmlReader xml = XmlReader.Create(xmlStreams[index]);
      SQLiteTransaction transaction = this.Connection.BeginTransaction();
      List<SQLiteCommand> sqLiteCommandList = new List<SQLiteCommand>(1024 /*0x0400*/);
      List<SQLiteCommand> advCommands = new List<SQLiteCommand>(1024 /*0x0400*/);
      ImObject imObject = new ImObject();
      ImRelation imRelation = new ImRelation();
      ImAttributeType imAttributeType = new ImAttributeType();
      ImObjectType imObjectType = new ImObjectType();
      ImRelationType imRelationType = new ImRelationType();
      try
      {
        while (true)
        {
          do
          {
            string name;
            do
            {
              if (sqLiteCommandList.Count >= 1000 || advCommands.Count > 0)
              {
                try
                {
                  if (sqLiteCommandList.Count == 0)
                  {
                    sqLiteCommandList.AddRange((IEnumerable<SQLiteCommand>) advCommands);
                    advCommands.Clear();
                  }
                  while (sqLiteCommandList.Count > 0)
                  {
                    sqLiteCommandList.ForEach((Action<SQLiteCommand>) (item =>
                    {
                      currentCommand = item;
                      try
                      {
                        item.ExecuteNonQuery();
                      }
                      catch
                      {
                        if (advCommands.Count == 0)
                          return;
                        throw;
                      }
                    }));
                    sqLiteCommandList.Clear();
                    sqLiteCommandList.AddRange((IEnumerable<SQLiteCommand>) advCommands);
                    advCommands.Clear();
                  }
                }
                catch (Exception ex)
                {
                  throw this.LogException(ex, currentCommand);
                }
                finally
                {
                  currentCommand = (SQLiteCommand) null;
                  sqLiteCommandList.Clear();
                  transaction.Commit();
                  transaction = this.Connection.BeginTransaction();
                }
              }
              if (xml.Read())
              {
                switch (xml.NodeType)
                {
                  case XmlNodeType.Element:
                  case XmlNodeType.Attribute:
                    name = xml.Name;
                    if (name.Trim().ToUpperInvariant() == "OBJECT")
                    {
                      ++this.Objects;
                      imObject.Clear();
                      if (imObject.Load(xml, kernel))
                      {
                        advCommands.AddRange((IEnumerable<SQLiteCommand>) imObject.GetAsSQL(this.Connection, transaction, this.Tables));
                        continue;
                      }
                      continue;
                    }
                    if (name.Trim().ToUpperInvariant() == "RELATION")
                    {
                      ++this.Relations;
                      imRelation.Clear();
                      if (imRelation.Load(xml, kernel))
                      {
                        advCommands.AddRange((IEnumerable<SQLiteCommand>) imRelation.GetAsSQL(this.Connection, transaction, this.Tables));
                        continue;
                      }
                      continue;
                    }
                    if (name.Trim().ToUpperInvariant() == "ATTRIBUTE_TYPE")
                    {
                      ++this.AttributeTypes;
                      imAttributeType.Clear();
                      if (imAttributeType.Load(xml, kernel))
                      {
                        sqLiteCommandList.AddRange((IEnumerable<SQLiteCommand>) imAttributeType.GetAsSQL(this.Connection, transaction, this.Tables));
                        continue;
                      }
                      continue;
                    }
                    if (name.Trim().ToUpperInvariant() == "OBJECT_TYPE")
                    {
                      ++this.ObjectTypes;
                      imObjectType.Clear();
                      if (imObjectType.Load(xml, kernel))
                      {
                        sqLiteCommandList.AddRange((IEnumerable<SQLiteCommand>) imObjectType.GetAsSQL(this.Connection, transaction, this.Tables));
                        continue;
                      }
                      continue;
                    }
                    continue;
                  default:
                    continue;
                }
              }
              else
                goto label_40;
            }
            while (!(name.Trim().ToUpperInvariant() == "RELATION_TYPE"));
            ++this.RelationTypes;
            imRelationType.Clear();
          }
          while (!imRelationType.Load(xml, kernel));
          sqLiteCommandList.AddRange((IEnumerable<SQLiteCommand>) imRelationType.GetAsSQL(this.Connection, transaction, this.Tables));
        }
      }
      finally
      {
        if (sqLiteCommandList.Count > 0 || advCommands.Count > 0)
        {
          try
          {
            if (sqLiteCommandList.Count == 0)
            {
              sqLiteCommandList.AddRange((IEnumerable<SQLiteCommand>) advCommands);
              advCommands.Clear();
            }
            while (sqLiteCommandList.Count > 0)
            {
              sqLiteCommandList.ForEach((Action<SQLiteCommand>) (item =>
              {
                currentCommand = item;
                try
                {
                  item.ExecuteNonQuery();
                }
                catch
                {
                  if (advCommands.Count == 0)
                    return;
                  throw;
                }
              }));
              sqLiteCommandList.Clear();
              sqLiteCommandList.AddRange((IEnumerable<SQLiteCommand>) advCommands);
              advCommands.Clear();
            }
          }
          catch (Exception ex)
          {
            throw this.LogException(ex, currentCommand);
          }
          finally
          {
            currentCommand = (SQLiteCommand) null;
            sqLiteCommandList.Clear();
            transaction.Commit();
          }
        }
      }
label_40:;
    }
  }
}
