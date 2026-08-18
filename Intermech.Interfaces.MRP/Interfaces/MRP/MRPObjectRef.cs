// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPObjectRef
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>Класс, ссылающийся на описание объекта</summary>
public class MRPObjectRef : 
  MRPContext,
  IMRPObjectRef,
  IMRPGuidItem,
  IMRPUpdateableItemRef,
  IMRPContext
{
  /// <summary>Идентификатор версии объекта</summary>
  protected long objectID;
  /// <summary>Уникальный глобальный идентификатор версии объекта</summary>
  protected Guid guid = Guid.Empty;

  /// <summary>Создать описание объекта</summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="services">Контейнер сервисов (контекст)</param>
  public MRPObjectRef(IServiceProvider services, long objectID)
    : this(services, objectID, Guid.Empty)
  {
  }

  /// <summary>Создать описание объекта</summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="guid">Guid версии объекта</param>
  /// <param name="services">Контейнер сервисов (контекст)</param>
  public MRPObjectRef(IServiceProvider services, long objectID, Guid guid)
    : base(services)
  {
    this.objectID = objectID;
    this.guid = guid;
  }

  /// <summary>Идентификатор версии объекта</summary>
  public virtual long ObjectID
  {
    [DebuggerStepThrough] get => this.objectID;
  }

  /// <summary>Уникальный глобальный идентификатор элемента</summary>
  public virtual Guid Guid
  {
    [DebuggerStepThrough] get => this.guid;
  }

  /// <summary>
  /// Обновить целочисленный идентификатор элемента на указанное значение
  /// </summary>
  /// <param name="newItemID">Новый целочисленный идентификатор элемента</param>
  public virtual void UpdateItemID(long newItemID) => this.objectID = newItemID;
}
