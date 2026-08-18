// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ObjectCreatorCustomServiceEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Аргументы события выбора пользовательского мастера создания объектов определенного типа.
/// </summary>
public sealed class ObjectCreatorCustomServiceEventArgs : EventArgs
{
  private object[] _constructorParams;
  private int objectTypeId;
  private bool handled;
  private Type customServiceType;

  /// <summary>Создает объект.</summary>
  /// <param name="objectTypeId">Идентификатор типа создаваемого объекта</param>
  /// <exception cref="T:ArgumentException">Параметр <paramref name="objectTypeId" /> не задан</exception>
  public ObjectCreatorCustomServiceEventArgs(int objectTypeId)
  {
    this.objectTypeId = objectTypeId != -1 ? objectTypeId : throw new ArgumentException("Не задан идентификатор типа создаваемого объекта.", nameof (objectTypeId));
  }

  /// <summary>Возвращает идентификатор типа создаваемого объекта.</summary>
  public int ObjectTypeId => this.objectTypeId;

  /// <summary>
  /// Возвращает или задает признак, что событие было обработано.
  /// </summary>
  public bool Handled
  {
    get => this.handled;
    set => this.handled = value;
  }

  /// <summary>
  /// Возвращает или задает тип пользовательского мастера создания объектов определенного типа.
  /// </summary>
  public Type CustomServiceType
  {
    get => this.customServiceType;
    set => this.customServiceType = value;
  }

  /// <summary>
  /// Возвращает или задает набор параметров для конструктора собственного создателя объектов.
  /// В случае null (либо при длине массива равной 0) вызывается конструктор без параметров.
  /// </summary>
  public object[] ConstructorParams
  {
    get => this._constructorParams;
    set => this._constructorParams = value;
  }
}
