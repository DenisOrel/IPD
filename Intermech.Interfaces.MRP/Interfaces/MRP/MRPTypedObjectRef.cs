// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPTypedObjectRef
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Класс, ссылающийся на описание типизированного объекта
/// </summary>
public class MRPTypedObjectRef : 
  MRPObjectRef,
  IMRPTypedObjectRef,
  IMRPObjectRef,
  IMRPGuidItem,
  IMRPUpdateableItemRef,
  IMRPContext,
  IMRPTypedItem
{
  /// <summary>Идентификатор типа объекта</summary>
  protected int typeID = -1;

  /// <summary>Создать описание объекта</summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="services">Контейнер сервисов (контекст)</param>
  public MRPTypedObjectRef(IServiceProvider services, long objectID)
    : base(services, objectID)
  {
  }

  /// <summary>Создать описание объекта</summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="guid">Guid версии объекта</param>
  /// <param name="services">Контейнер сервисов (контекст)</param>
  public MRPTypedObjectRef(IServiceProvider services, long objectID, Guid guid)
    : base(services, objectID, guid)
  {
  }

  /// <summary>Создать описание объекта</summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="guid">Guid версии объекта</param>
  /// <param name="typeID">Идентификатор типа объекта</param>
  /// <param name="services">Контейнер сервисов (контекст)</param>
  public MRPTypedObjectRef(IServiceProvider services, long objectID, Guid guid, int typeID)
    : base(services, objectID, guid)
  {
    this.typeID = typeID;
  }

  /// <summary>Идентификатор типа объекта</summary>
  public int TypeID
  {
    [DebuggerStepThrough] get => this.typeID;
  }
}
