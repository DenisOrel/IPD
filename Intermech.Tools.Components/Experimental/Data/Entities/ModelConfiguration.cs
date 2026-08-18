// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.ModelConfiguration
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

#nullable disable
namespace Experimental.Data.Entities;

/// <summary>
/// Базоый класс для конфигураций доменных моделей. Реализация является thread safe.
/// </summary>
public class ModelConfiguration
{
  private bool isInitialized;
  private IDictionary<Type, EntityTypeDescriptor> descriptors;

  /// <summary>Создает объект.</summary>
  public ModelConfiguration()
  {
    this.descriptors = (IDictionary<Type, EntityTypeDescriptor>) new Dictionary<Type, EntityTypeDescriptor>();
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
    foreach (KeyValuePair<Type, EntityTypeDescriptor> descriptor in (IEnumerable<KeyValuePair<Type, EntityTypeDescriptor>>) this.Descriptors)
    {
      EntityTypeDescriptor entityTypeDescriptor = descriptor.Value;
      if (!entityTypeDescriptor.IsInitialized)
        throw new InvalidOperationException($"Дескриптор '{entityTypeDescriptor}' не был инициализирован.");
    }
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
    if (this.Descriptors.Count == 0)
      return;
    this.Descriptors.Clear();
  }

  /// <summary>
  /// Вызывается только в случае успешной инициализации объекта и используется кэшей, ускорителей и др.
  /// Метод не должен бросать исключений.
  /// </summary>
  protected virtual void DoPostInitialize()
  {
    this.descriptors = this.descriptors.IsReadOnly ? this.descriptors : (IDictionary<Type, EntityTypeDescriptor>) new ReadOnlyDictionary<Type, EntityTypeDescriptor>(this.descriptors);
  }

  /// <summary>
  /// Добавляет дескриптор для типа доменных объектов. Метод может быть вызван только в процессе инициализации текущего объекта.
  /// </summary>
  /// <param name="descriptor">Дескриптор типа доменных объектов</param>
  /// <exception cref="T:ArgumentNullException">Параметр <paramref name="descriptor" /> не должен быть равен null</exception>
  /// <exception cref="T:ArgumentNullException">Инициализация текущего объекта уже была выполнена</exception>
  public void AddDescriptor(EntityTypeDescriptor descriptor)
  {
    if (descriptor == null)
      throw new ArgumentNullException(nameof (descriptor));
    this.RequireNotInitialized();
    this.Descriptors.Add(descriptor.EntityType, descriptor);
  }

  protected IEntityTypeDescriptor GetDescriptorInternal(Type entityType)
  {
    if (entityType == (Type) null)
      throw new ArgumentNullException(nameof (entityType));
    this.RequireInitialized();
    EntityTypeDescriptor descriptorInternal;
    if (this.Descriptors.TryGetValue(entityType, out descriptorInternal))
      return (IEntityTypeDescriptor) descriptorInternal;
    throw new InvalidOperationException($"Не удалось найти дескриптор для типа доменных объектов '{entityType}'.");
  }

  protected IDictionary<Type, EntityTypeDescriptor> Descriptors
  {
    [DebuggerStepThrough] get => this.descriptors;
  }
}
