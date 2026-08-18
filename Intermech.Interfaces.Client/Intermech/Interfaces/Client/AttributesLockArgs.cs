// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.AttributesLockArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Реализует аргументы события, используемого для построения списка блокируемых атрибутов.
/// </summary>
public sealed class AttributesLockArgs : EventArgs
{
  private readonly AttributableElements elementKind;
  private readonly long elementId;
  private readonly int elementType;
  private HashSet<int> lockedAttributes;
  private HashSet<int> unlockedAttributes;
  private Dictionary<object, object> tags;

  /// <summary>Создает объект.</summary>
  /// <param name="elementKind">Указывает, к чему относятся атрибуты - к объекту или связи</param>
  /// <param name="elementId">Идентификатор версии объекта или связи</param>
  /// <param name="elementType">Идентификатор типа объекта или типа связи</param>
  public AttributesLockArgs(AttributableElements elementKind, long elementId, int elementType)
  {
    this.elementKind = elementKind;
    this.elementId = elementId;
    this.elementType = elementType;
    this.lockedAttributes = new HashSet<int>();
    this.unlockedAttributes = new HashSet<int>();
  }

  /// <summary>
  /// Указывает, к чему относятся атрибуты - к объекту или связи.
  /// </summary>
  public AttributableElements ElementKind
  {
    [DebuggerStepThrough] get => this.elementKind;
  }

  /// <summary>Возвращает идентификатор версии объекта или связи.</summary>
  public long ElementId
  {
    [DebuggerStepThrough] get => this.elementId;
  }

  /// <summary>Возвращает идентификатор типа объекта или типа связи.</summary>
  public int ElementType
  {
    [DebuggerStepThrough] get => this.elementType;
  }

  /// <summary>
  /// Возвращает коллекцию идентификаторов атрибутов, заблокированных от изменения.
  /// </summary>
  public HashSet<int> LockedAttributes
  {
    [DebuggerStepThrough] get => this.lockedAttributes;
  }

  /// <summary>
  /// Возвращает коллекцию идентификаторов атрибутов, разблокированных для изменения.
  /// Значение этого свойства имеет приоритет перед <see cref="P:Intermech.Interfaces.Client.AttributesLockArgs.LockedAttributes" /> при построении окончательного блокируемых атрибутов.
  /// </summary>
  public HashSet<int> UnlockedAttributes
  {
    [DebuggerStepThrough] get => this.unlockedAttributes;
  }

  /// <summary>Возвращает коллекцию тегов.</summary>
  public Dictionary<object, object> Tags
  {
    [DebuggerStepThrough] get
    {
      if (this.tags == null)
        this.tags = new Dictionary<object, object>();
      return this.tags;
    }
  }
}
