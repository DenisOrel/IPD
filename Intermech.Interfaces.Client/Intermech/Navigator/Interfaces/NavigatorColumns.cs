// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.NavigatorColumns
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Класс для хранения настроек вида (колонки "Навигатора", порядок сортировки, группы, т.п.)
/// </summary>
[Serializable]
public class NavigatorColumns : 
  IAssignable,
  ICloneable,
  IComparable<NavigatorColumns>,
  IStreamSerializable
{
  /// <summary>Категория</summary>
  private int _category;
  private Guid _categoryGuid;
  /// <summary>Тип</summary>
  private int _type;
  /// <summary>Дополнение к названию схемы</summary>
  private string _suffix;
  /// <summary>Коллекция колонок "Навигатора"</summary>
  private NodeColumnCollection _columns;
  /// <summary>
  /// Порядковые номера колонок, участвующих в группировании
  /// </summary>
  private List<int> _groups;
  /// <summary>Является ли данная настройка вида унаследованной</summary>
  [NonSerialized]
  private bool _inherited;
  private object _cookie;

  /// <summary>Категория</summary>
  public int Category
  {
    [DebuggerStepThrough] get => this._category;
    set => this._category = value;
  }

  public Guid CategoryGuid
  {
    get => this._categoryGuid;
    set => this._categoryGuid = value;
  }

  /// <summary>Тип</summary>
  public int Type
  {
    [DebuggerStepThrough] get => this._type;
    set => this._type = value;
  }

  /// <summary>Дополнение к названию схемы</summary>
  public string Suffix
  {
    [DebuggerStepThrough] get => this._suffix;
    set => this._suffix = value;
  }

  /// <summary>Коллекция колонок "Навигатора"</summary>
  public NodeColumnCollection Columns
  {
    [DebuggerStepThrough] get => this._columns;
    set => this._columns = value;
  }

  /// <summary>
  /// Порядковые номера колонок, участвующих в группировании
  /// </summary>
  public List<int> Groups
  {
    [DebuggerStepThrough] get => this._groups;
    set => this._groups = value;
  }

  /// <summary>Являются ли настройки пустыми</summary>
  public bool Empty => this._category == 0 && this._type == 0 && this._columns == null;

  /// <summary>Является ли данная настройка вида унаследованной</summary>
  public bool Inherited
  {
    [DebuggerStepThrough] get => this._inherited;
    set => this._inherited = value;
  }

  public object Cookie
  {
    get => this._cookie;
    set => this._cookie = value;
  }

  /// <summary>Создать пустой экземпляр класса</summary>
  public NavigatorColumns()
  {
  }

  /// <summary>
  /// Создать экземпляр класса для указанных категории и типа
  /// </summary>
  /// <param name="category">Категория</param>
  /// <param name="type">Тип</param>
  /// <param name="suffix">Дополнительное имя</param>
  public NavigatorColumns(int category, int type, string suffix)
  {
    this._category = category;
    this._type = type;
    this._suffix = suffix;
  }

  /// <summary>Создать экземпляр класса, заполнить его информацией</summary>
  /// <param name="source">Объект-источник</param>
  public NavigatorColumns(object source) => this.Assign(source);

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true - объекты равны</returns>
  public override bool Equals(object obj) => this.CompareTo(obj as NavigatorColumns) == 0;

  /// <summary>Получить 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  public override int GetHashCode()
  {
    return this._category.GetHashCode() << 24 ^ this._type.GetHashCode() << 8 ^ this._suffix.GetHashCode();
  }

  /// <summary>Очистить поля класса</summary>
  public void Clear()
  {
    this._columns = (NodeColumnCollection) null;
    this._groups = (List<int>) null;
    this._category = 0;
    this._type = 0;
    this._suffix = string.Empty;
    this._inherited = false;
    this._categoryGuid = Guid.Empty;
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public void Assign(object source)
  {
    if (this == source)
      return;
    this.Clear();
    switch (source)
    {
      case NodeColumnCollection collection:
        this._columns = new NodeColumnCollection((IEnumerable<NodeColumn>) collection);
        break;
      case NavigatorColumns navigatorColumns:
        if (navigatorColumns._columns != null)
        {
          this._columns = new NodeColumnCollection(navigatorColumns._columns.Count);
          for (int index = 0; index < navigatorColumns._columns.Count; ++index)
            this._columns.Add(navigatorColumns._columns[index].Clone() as NodeColumn);
        }
        if (navigatorColumns._groups != null)
          this._groups = new List<int>((IEnumerable<int>) navigatorColumns._groups);
        this._category = navigatorColumns._category;
        this._type = navigatorColumns._type;
        this._suffix = navigatorColumns._suffix;
        this._inherited = navigatorColumns._inherited;
        this._cookie = navigatorColumns._cookie;
        this._categoryGuid = navigatorColumns._categoryGuid;
        break;
    }
  }

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public object Clone() => (object) new NavigatorColumns((object) this);

  /// <summary>Выполнить сравнение с указанным объектом</summary>
  /// <param name="other">Объект для сравнения</param>
  /// <returns>-1, 0, 1</returns>
  public int CompareTo(NavigatorColumns other)
  {
    if (other == null)
      return 1;
    int num1 = this._category.CompareTo(other._category);
    if (num1 != 0)
      return num1;
    int num2 = this._type.CompareTo(other._type);
    if (num2 != 0)
      return num2;
    int num3 = this._suffix.CompareTo(other._suffix);
    if (num3 != 0)
      return num3;
    if (this._columns == null || other._columns == null || this._groups == null || other._groups == null || this._columns.Count != other._columns.Count || this._groups.Count != other._groups.Count)
      return 1;
    bool flag = true;
    for (int index = 0; index < this._columns.Count; ++index)
    {
      flag &= this._columns[index].FullEquals((object) other._columns[index]);
      if (!flag)
        return 1;
    }
    for (int index = 0; index < this._groups.Count; ++index)
    {
      flag &= this._groups[index] == other._groups[index];
      if (!flag)
        return 1;
    }
    return 0;
  }

  /// <summary>Сериализовать класс в поток</summary>
  /// <param name="packMode">Степень сжатия</param>
  /// <returns>Сериализованный класс</returns>
  public MemoryStream SerializeToStream(ZLibCompressLevels packMode)
  {
    try
    {
      this.CategoryGuid = ((IGuidMapper) ServicesManager.GetService(typeof (IGuidMapper)))[this.Category];
    }
    catch
    {
      this.CategoryGuid = Guid.Empty;
    }
    MemoryStream memoryStream = new MemoryStream();
    new BinaryFormatter().Serialize((Stream) memoryStream, (object) this);
    MemoryStream outStream = new MemoryStream();
    ZLibStreamHelper.PackStream((Stream) memoryStream, packMode, (Stream) outStream);
    memoryStream.Close();
    outStream.Seek(0L, SeekOrigin.Begin);
    return outStream;
  }

  /// <summary>Десериализовать класс из указанного потока</summary>
  /// <param name="stream">Поток</param>
  /// <returns>true - десериализация выполнена успешно</returns>
  public bool DeserializeFromStream(Stream stream)
  {
    this.Clear();
    if (stream == null)
      return false;
    NavigatorColumns source;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      ZLibStreamHelper.UnpackStream(stream, (Stream) memoryStream);
      try
      {
        source = new BinaryFormatter().Deserialize((Stream) memoryStream) as NavigatorColumns;
      }
      catch
      {
        source = (NavigatorColumns) null;
      }
    }
    this.Assign((object) source);
    try
    {
      IGuidMapper service = (IGuidMapper) ServicesManager.GetService(typeof (IGuidMapper));
      if (this.CategoryGuid != Guid.Empty)
        this.Category = service[this.CategoryGuid];
    }
    catch
    {
    }
    return source != null;
  }
}
