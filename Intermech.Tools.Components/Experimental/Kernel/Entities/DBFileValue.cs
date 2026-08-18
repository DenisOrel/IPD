// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBFileValue
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using Intermech.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Experimental.Kernel.Entities;

/// <summary>Класс значений для файловых атрибутов IPS.</summary>
public sealed class DBFileValue : IEquatable<DBFileValue>, IPropertyValueWithLoader
{
  private DBFileValueLoader loader;
  private bool isLoaded;
  private byte[] content;
  private string name;
  private static readonly DBFileValue emptyValue = new DBFileValue(string.Empty, new byte[0]);

  /// <summary>Создает объект.</summary>
  /// <param name="name">Имя файла</param>
  /// <param name="content">Содержимое файла в виде массива байт</param>
  /// <exception cref="T:System.ArgumentException">параметр <paramref name="name" /> содержит null; параметр <paramref name="content" /> содержит null</exception>
  public DBFileValue(string name, byte[] content)
  {
    if (name == null)
      throw new ArgumentNullException(nameof (name));
    if (content == null)
      throw new ArgumentNullException(nameof (content));
    this.name = name;
    this.content = content;
    this.isLoaded = true;
  }

  /// <summary>Создает объект.</summary>
  /// <param name="loader">Загрузчик значений</param>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="loader" /> содержит null</exception>
  internal DBFileValue(DBFileValueLoader loader)
  {
    this.loader = loader != null ? loader : throw new ArgumentNullException(nameof (loader));
    this.isLoaded = false;
  }

  /// <summary>Возвращает имя файла.</summary>
  public string Name
  {
    [DebuggerStepThrough] get
    {
      if (!this.isLoaded)
        this.LoadValue();
      return this.name;
    }
  }

  /// <summary>Возвращает содержимое файла.</summary>
  public byte[] Content
  {
    [DebuggerStepThrough] get
    {
      if (!this.isLoaded)
        this.LoadValue();
      return this.content;
    }
  }

  /// <summary>Возвращает признак, что значение уже загружено.</summary>
  public bool IsLoaded
  {
    [DebuggerStepThrough] get => this.isLoaded;
  }

  /// <summary>Загружает значение, если это еще не было сделано.</summary>
  public void Load()
  {
    if (this.isLoaded)
      return;
    this.LoadValue();
  }

  private void RequireLoader()
  {
    if (this.loader == null)
      throw new InvalidOperationException("Загрузчик значений не задан.");
  }

  private void LoadValue()
  {
    this.RequireLoader();
    DBFileValue dbFileValue = this.loader.LoadValue();
    this.name = dbFileValue.Name;
    this.content = dbFileValue.Content;
    this.isLoaded = true;
  }

  /// <summary>Возвращает пустое значение.</summary>
  public static DBFileValue Empty
  {
    [DebuggerStepThrough] get => DBFileValue.emptyValue;
  }

  /// <summary>
  /// Задает доменный объект, которому принадлежит значение свойства.
  /// Это объект используется как источник для чтения из базы данных.
  /// </summary>
  /// <param name="entityTypeDescriptor">Дескриптор доменного объекта</param>
  /// <param name="entity">Доменный объект</param>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="entityTypeDescriptor" /> содержит null; параметр <paramref name="entityTypeDescriptor" /> содержит null</exception>
  void IPropertyValueWithLoader.SetEntity(
    IDBEntityTypeDescriptor entityTypeDescriptor,
    object entity)
  {
    if (entityTypeDescriptor == null)
      throw new ArgumentNullException(nameof (entityTypeDescriptor));
    if (entity == null)
      throw new ArgumentNullException(nameof (entity));
    if (this.isLoaded)
      return;
    this.RequireLoader();
    this.loader.SetEntity(entityTypeDescriptor, entity);
  }

  public bool Equals(DBFileValue other)
  {
    if (other != null)
    {
      if (this.isLoaded && other.isLoaded)
        return PathUtils.IsSamePath(this.name, other.name) && CollectionUtils.ContentEqual<byte>((ICollection<byte>) this.content, (ICollection<byte>) other.content);
      if (this.loader != null && other.loader != null)
        return this.loader.Equals(other.loader);
    }
    return false;
  }

  public override bool Equals(object obj)
  {
    return !(obj is DBFileValue other) ? base.Equals(obj) : this.Equals(other);
  }

  public override int GetHashCode() => !this.isLoaded ? 0 : this.name.GetHashCode();

  public override string ToString()
  {
    return this.isLoaded && this.name == string.Empty && this.content.Length == 0 ? "Empty" : base.ToString();
  }
}
