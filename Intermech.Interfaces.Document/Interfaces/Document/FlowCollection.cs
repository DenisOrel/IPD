// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.FlowCollection
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Коллекция потоков</summary>
[Serializable]
public class FlowCollection : IList, ICollection, IEnumerable, IWriteReadXml
{
  private List<IFlowElement> list = new List<IFlowElement>();

  /// <summary>Только для чтения</summary>
  public virtual bool IsReadOnly
  {
    [DebuggerStepThrough] get => ((IList) this.list).IsReadOnly;
  }

  /// <summary>индексатор коллекции</summary>
  public virtual IFlowElement this[int index]
  {
    [DebuggerStepThrough] get => this.list[index];
    set
    {
      this.list[index] = value != null ? value : throw new ArgumentNullException(nameof (value));
    }
  }

  object IList.this[int index]
  {
    [DebuggerStepThrough] get => (object) this[index];
    [DebuggerStepThrough] set => this[index] = (IFlowElement) value;
  }

  /// <summary>Удалить по индексу</summary>
  /// <param name="index"></param>
  [DebuggerStepThrough]
  public virtual void RemoveAt(int index) => this.list.RemoveAt(index);

  /// <summary>Вставить</summary>
  /// <param name="index">Индекс</param>
  /// <param name="value">Значение</param>
  [DebuggerStepThrough]
  public virtual void Insert(int index, IFlowElement value)
  {
    if (value == null)
      throw new ArgumentNullException(nameof (value));
    this.list.Insert(index, value);
  }

  [DebuggerStepThrough]
  void IList.Insert(int index, object value) => this.Insert(index, (IFlowElement) value);

  /// <summary>Удалить</summary>
  /// <param name="value">Элемент</param>
  [DebuggerStepThrough]
  public virtual void Remove(IFlowElement value) => this.list.Remove(value);

  [DebuggerStepThrough]
  void IList.Remove(object value) => this.Remove((IFlowElement) value);

  /// <summary>содержится в коллекции</summary>
  /// <param name="value">Элемент</param>
  /// <returns>true, если содержится в коллекции</returns>
  [DebuggerStepThrough]
  public virtual bool Contains(IFlowElement value) => this.list.Contains(value);

  [DebuggerStepThrough]
  bool IList.Contains(object value) => this.Contains((IFlowElement) value);

  /// <summary>Очистить</summary>
  [DebuggerStepThrough]
  public virtual void Clear() => this.list.Clear();

  /// <summary>индекс элемента</summary>
  /// <param name="value">Элемента</param>
  /// <returns>Индекс элемента. Если элемент не найден, то -1</returns>
  [DebuggerStepThrough]
  public virtual int IndexOf(IFlowElement value) => this.list.IndexOf(value);

  [DebuggerStepThrough]
  int IList.IndexOf(object value) => this.IndexOf((IFlowElement) value);

  /// <summary>Добавить</summary>
  /// <param name="value">Элемент</param>
  /// <returns>Индекс элемента</returns>
  [DebuggerStepThrough]
  public int Add(IFlowElement value)
  {
    this.list.Add(value);
    return this.list.Count - 1;
  }

  [DebuggerStepThrough]
  int IList.Add(object value) => this.Add((IFlowElement) value);

  /// <summary>Коллекция фиксированного размера</summary>
  public virtual bool IsFixedSize
  {
    [DebuggerStepThrough] get => ((IList) this.list).IsFixedSize;
  }

  /// <summary>Является ли доступ к интерфейсу ICollection синхронизированным (потокобезопасным)</summary>
  public virtual bool IsSynchronized
  {
    [DebuggerStepThrough] get => ((ICollection) this.list).IsSynchronized;
  }

  /// <summary>количество элементов</summary>
  public virtual int Count
  {
    [DebuggerStepThrough] get => this.list.Count;
  }

  /// <summary>Скопировать элементы в массив</summary>
  /// <param name="array">Массив приемник</param>
  /// <param name="index">Индекс с которого начинать копирование</param>
  [DebuggerStepThrough]
  public virtual void CopyTo(Array array, int index)
  {
    ((ICollection) this.list).CopyTo(array, index);
  }

  /// <summary>Объект, который можно использовать для синхронизации доступа</summary>
  public object SyncRoot
  {
    [DebuggerStepThrough] get => ((ICollection) this.list).SyncRoot;
  }

  /// <summary>Получить Enumerator</summary>
  /// <returns>Enumerator</returns>
  [DebuggerStepThrough]
  public IEnumerator GetEnumerator() => (IEnumerator) this.list.GetEnumerator();

  /// <summary>Записать поля в XML</summary>
  /// <param name="elementName">Имя элемента XML, под которым нужно сохранить данные</param>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public virtual void WriteToXml(string elementName, XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    xw.WriteStartElement(elementName);
    for (int index = 0; index < this.list.Count; ++index)
      xw.WriteElementString("item", objectRefId.GetId((object) this.list[index], out bool _).ToString((IFormatProvider) CultureInfo.InvariantCulture));
    xw.WriteEndElement();
  }

  /// <summary>Прочитать одно поле из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле прочитано</returns>
  public virtual bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    if (!(readArgs.Reader.LocalName == "item"))
      return false;
    if (!readArgs.Reader.HasValue && !readArgs.Reader.IsEmptyElement)
      readArgs.Reader.Read();
    if (readArgs.Reader.HasValue)
      DocumentTreeNode.AddObjectReference((object) this, readArgs.ObjectReferences, "item", readArgs.Reader.Value);
    return true;
  }

  /// <summary>Загрузить узел из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public virtual void ReadFromXml(XmlReadArgs readArgs)
  {
    this.Clear();
    WriteReadXmlHelper.ReadFromXml((IWriteReadXml) this, readArgs);
  }
}
