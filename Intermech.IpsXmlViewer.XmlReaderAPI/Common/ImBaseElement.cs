// Decompiled with JetBrains decompiler
// Type: XmlReaderAPI.Common.ImBaseElement
// Assembly: Intermech.IpsXmlViewer.XmlReaderAPI, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 197F841C-E5B9-4815-BCCD-9737649DED5C
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.XmlReaderAPI.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.XmlReaderAPI.xml

using Intermech.IpsXmlViewer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Xml;

#nullable disable
namespace XmlReaderAPI.Common;

/// <summary>Абстрактный базовый класс</summary>
public abstract class ImBaseElement : IImBaseElement, IAssignable, ICloneable, IDisplayable
{
  /// <summary>Строка для отображения на экране</summary>
  public abstract string Text { get; }

  /// <summary>Очистить поля класса</summary>
  public abstract void Clear();

  /// <summary>
  /// Заполнить поля класса информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public abstract void Assign(object source);

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса или null</returns>
  public virtual object Clone()
  {
    if (Activator.CreateInstance(this.GetType()) is ImBaseElement instance)
      instance.Assign((object) this);
    return (object) instance;
  }

  /// <summary>Имя атрибута, в котором хранится содержимое элемента</summary>
  public abstract string MainAttrName { get; }

  /// <summary>Загрузить содержимое объекта из документа XML</summary>
  /// <param name="xml">Документ XML</param>
  /// <param name="kernel">Микроядро</param>
  /// <returns>true - узел считал содержимое коректно</returns>
  public abstract bool Load(XmlReader xml, IKernel kernel);

  /// <summary>
  /// Получить содержимое элемента в виде SQL-последовательностей (SQLite)
  /// </summary>
  /// <param name="connection">Соединение</param>
  /// <param name="transaction">Транзакция</param>
  /// <param name="tables">Список таблиц и их колонок</param>
  /// <returns>Содержимое элемента в виде SQL-последовательностей (SQLite) или null</returns>
  public abstract IList<SQLiteCommand> GetAsSQL(
    SQLiteConnection connection,
    SQLiteTransaction transaction,
    IDictionary<string, IList<string>> tables);
}
