
// Type: Intermech.Tools.Data.Sync.AttributeSyncUnit
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Data;
using System;
using System.Diagnostics;


namespace Intermech.Tools.Data.Sync;

/// <summary>
/// Описывает атрибут, переносимый из одной системы в другую
/// </summary>
[DebuggerDisplay("AttributeSyncUnit: {key} (ThrowSetException: {throwSetException})")]
public sealed class AttributeSyncUnit : IEquatable<AttributeSyncUnit>
{
  /// <summary>Идентификатор атрибута</summary>
  private readonly StringKey key;
  /// <summary>Коллекция флагов, управляющих переносом атрибута.</summary>
  private readonly NamedFlagCollection flags;
  private static readonly StringKey ThrowSetExceptionFlag = new StringKey(nameof (ThrowSetException));
  private static readonly StringKey CaseInsensitiveFlag = new StringKey(nameof (CaseInsensitive));

  /// <summary>Возвращает ключ атрибута.</summary>
  public StringKey Key
  {
    [DebuggerStepThrough] get => this.key;
  }

  /// <summary>
  /// Возвращает или устанавливает признак, что при неудачной записи значения этого атрибута следует сбрасывать исключение.
  /// Реализуется признак с помощью одноименного флага.
  /// </summary>
  public bool ThrowSetException
  {
    [DebuggerStepThrough] get => this.flags[AttributeSyncUnit.ThrowSetExceptionFlag];
    [DebuggerStepThrough] set => this.flags[AttributeSyncUnit.ThrowSetExceptionFlag] = value;
  }

  /// <summary>
  /// Возвращает или задает признак нечувствительности к регистру при сравнении строковых значений.
  /// Реализуется признак с помощью одноименного флага.
  /// </summary>
  public bool CaseInsensitive
  {
    [DebuggerStepThrough] get => this.flags[AttributeSyncUnit.CaseInsensitiveFlag];
    [DebuggerStepThrough] set => this.flags[AttributeSyncUnit.CaseInsensitiveFlag] = value;
  }

  /// <summary>
  /// Возвращает коллекцию флагов, управляющих переносом атрибута.
  /// </summary>
  public NamedFlagCollection Flags => this.flags;

  /// <summary>Создает объект</summary>
  /// <param name="key">Ключ атрибута</param>
  /// <param name="throwSetException">Признак, что при неуспешной записи значения этого атрибута следует сбрасывать исключение</param>
  public AttributeSyncUnit(StringKey key, bool throwSetException)
  {
    this.key = !(key == (StringKey) null) ? key : throw new ArgumentNullException(nameof (key));
    this.flags = new NamedFlagCollection();
    this.ThrowSetException = throwSetException;
  }

  /// <summary>
  /// Проверяет эквивалентность текущего и указанного объектов.
  /// </summary>
  /// <param name="obj">Другой объект, с которым сравнивается текущий объект</param>
  /// <returns>true, если объекты эквивалентны</returns>
  /// <exception cref="T:System.ArgumentNullException">Другой объект не указана</exception>
  public bool Equals(AttributeSyncUnit obj)
  {
    if (obj == null)
      throw new ArgumentNullException(nameof (obj));
    return this.key == obj.key && this.ThrowSetException == obj.ThrowSetException && this.CaseInsensitive == obj.CaseInsensitive;
  }

  /// <summary>
  /// Проверяет эквивалентность текущего и указанного объектов.
  /// </summary>
  /// <param name="obj">Другой объект, с которым сравнивается текущий объект</param>
  /// <returns>true, если объекты эквивалентны</returns>
  public override bool Equals(object obj)
  {
    return !(obj is AttributeSyncUnit attributeSyncUnit) ? base.Equals(obj) : this.Equals(attributeSyncUnit);
  }

  /// <summary>Возвращает хэш-код ключа.</summary>
  /// <returns>Значение хэш-кода</returns>
  public override int GetHashCode() => this.key.GetHashCode();

  /// <summary>Возвращает строковое представление объекта.</summary>
  /// <returns></returns>
  public override string ToString() => this.key.ToString();
}
