// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPRelationRef
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>Класс, ссылающийся на описание связи</summary>
public class MRPRelationRef : 
  MRPContext,
  IMRPRelationRef,
  IMRPGuidItem,
  IMRPTypedItem,
  IMRPUpdateableItemRef,
  IMRPContext
{
  /// <summary>Идентификатор версии родительского объекта</summary>
  protected long projectID;
  /// <summary>Идентификатор связи</summary>
  protected long prjLinkID;
  /// <summary>Уникальный глобальный идентификатор элемента</summary>
  protected Guid guid;
  /// <summary>32-битный идентификатор типа связи</summary>
  protected int typeID = -1;
  /// <summary>
  /// Является ли связь созданной (новой), либо она существующая (значение по умолчанию)
  /// </summary>
  protected bool isNewRelation;

  /// <summary>Создать описание связи</summary>
  /// <param name="services">Контейнер сервисов (контекст)</param>
  /// <param name="projectID">Идентификатор версии родительского объекта</param>
  /// <param name="prjLinkID">Идентификатор связи</param>
  public MRPRelationRef(IServiceProvider services, long projectID, long prjLinkID)
    : this(services, projectID, prjLinkID, Guid.Empty, -1, false)
  {
  }

  /// <summary>Создать описание связи</summary>
  /// <param name="services">Контейнер сервисов (контекст)</param>
  /// <param name="projectID">Идентификатор версии родительского объекта</param>
  /// <param name="prjLinkID">Идентификатор связи</param>
  /// <param name="typeID">32-битный идентификатор типа связи</param>
  /// <param name="guid">Уникальный глобальный идентификатор элемента</param>
  /// <param name="isNewRelation">Является ли связь созданной (новой), либо она существующая (значение по умолчанию)</param>
  public MRPRelationRef(
    IServiceProvider services,
    long projectID,
    long prjLinkID,
    Guid guid,
    int typeID,
    bool isNewRelation)
    : base(services)
  {
    this.projectID = projectID;
    this.prjLinkID = prjLinkID;
    this.guid = guid;
    this.typeID = typeID;
    this.isNewRelation = isNewRelation;
  }

  /// <summary>
  /// Является ли связь созданной (новой), либо она существующая (значение по умолчанию)
  /// </summary>
  public virtual bool IsNewRelation
  {
    [DebuggerStepThrough] get => this.isNewRelation;
  }

  /// <summary>Идентификатор версии родительского объекта</summary>
  public virtual long ProjectID
  {
    [DebuggerStepThrough] get => this.projectID;
  }

  /// <summary>Идентификатор связи</summary>
  public virtual long PrjLinkID
  {
    [DebuggerStepThrough] get => this.prjLinkID;
  }

  /// <summary>Уникальный глобальный идентификатор элемента</summary>
  public virtual Guid Guid
  {
    [DebuggerStepThrough] get => this.guid;
  }

  /// <summary>32-битный идентификатор типа связи</summary>
  public virtual int TypeID
  {
    [DebuggerStepThrough] get => this.typeID;
  }

  /// <summary>Обновить идентификатор связи на указанное значение</summary>
  /// <param name="newItemID">Новый идентификатор связи</param>
  public virtual void UpdateItemID(long newItemID) => this.prjLinkID = newItemID;
}
