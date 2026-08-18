// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.EntityTypeDescriptor
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Experimental.Data.Entities;

/// <summary>
/// Базовый класс для дескрипторов доменных объектов. Реализация является thread safe.
/// </summary>
public class EntityTypeDescriptor : IEntityTypeDescriptor
{
  private Type entityType;
  private bool isInitialized;

  /// <summary>Создает объект</summary>
  /// <param name="entityType">Тип доменных объектов</param>
  /// <exception cref="T:ArgumentNullException">Параметр <paramref name="entityType" /> не должен быть равен null</exception>
  public EntityTypeDescriptor(Type entityType)
  {
    this.entityType = !(entityType == (Type) null) ? entityType : throw new ArgumentNullException(nameof (entityType));
  }

  /// <summary>
  /// Возвращает признак, что инициализация объекта была выполнена.
  /// </summary>
  public bool IsInitialized
  {
    [DebuggerStepThrough] get => this.isInitialized;
  }

  /// <summary>
  /// Проверяет, что инициализация объекта еще была выполнена.
  /// </summary>
  /// <exception cref="T:ArgumentNullException">Инициализация объекта еще не была выполнена</exception>
  protected void RequireNotInitialized()
  {
    if (this.IsInitialized)
      throw new InvalidOperationException($"Инициализация объекта '{this.GetType()}' уже была выполнена.");
  }

  /// <summary>Проверяет, что инициализация объекта была выполнена.</summary>
  /// <exception cref="T:ArgumentNullException">Инициализация объекта еще не была выполнена</exception>
  protected void RequireInitialized()
  {
    if (!this.IsInitialized)
      throw new InvalidOperationException($"Инициализация объекта '{this.GetType()}' еще не была выполнена.");
  }

  /// <summary>Выполняет инициализацию объекта.</summary>
  /// <exception cref="T:ArgumentNullException">Инициализация объекта уже была выполнена</exception>
  public void Initialize()
  {
    this.RequireNotInitialized();
    this.DoValidateBeforeInitialize();
    try
    {
      this.DoInitialize();
      this.DoValidateAfterInitialize();
    }
    catch
    {
      this.DoCleanupAfterInitializationError();
      throw;
    }
    this.isInitialized = true;
    this.DoPostInitialize();
  }

  /// <summary>
  /// Проверяет корректность свойств объекта перед инициализацией.
  /// </summary>
  /// <exception cref="T:InvalidOperationException">Одно из свойств объекта имеет некорректное значение</exception>
  protected virtual void DoValidateBeforeInitialize()
  {
  }

  /// <summary>Выполняет инициализацию объекта.</summary>
  protected virtual void DoInitialize()
  {
  }

  /// <summary>
  /// Проверяет корректность свойств объекта после инициализации.
  /// </summary>
  /// <exception cref="T:InvalidOperationException">Одно из свойств объекта имеет некорректное значение</exception>
  protected virtual void DoValidateAfterInitialize()
  {
  }

  /// <summary>
  /// Выполняет очистку текущего объекта в случае необработанного исключения в процессе инициализации текущего объекта.
  /// </summary>
  protected virtual void DoCleanupAfterInitializationError()
  {
  }

  /// <summary>
  /// Вызывается только в случае успешной инициализации объекта и используется кэшей, ускорителей и др.
  /// Метод не должен бросать исключений.
  /// </summary>
  protected virtual void DoPostInitialize()
  {
  }

  public Type EntityType
  {
    [DebuggerStepThrough] get => this.entityType;
  }

  protected void CheckEntityType(object entity)
  {
    if (!this.EntityType.IsAssignableFrom(entity.GetType()))
      throw new InvalidOperationException($"Дескриптор не поддерживает доменные объекты типа '{entity.GetType()}'.");
  }
}
