// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPRelationAttributesRef
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using Intermech.Kernel.Search;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Класс, ссылающийся на допустимую коллекцию атрибутов у типа связи
/// </summary>
public class MRPRelationAttributesRef : 
  MRPContext,
  IMRPAttributableTypeRef,
  IMRPContext,
  IMRPTypedItem
{
  /// <summary>Идентификатор типа связи</summary>
  protected int typeID = -1;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="service">Контейнер сервисов</param>
  /// <param name="relTypeID">Идентификатор типа связи</param>
  public MRPRelationAttributesRef(IServiceProvider service, int relTypeID)
    : base(service)
  {
    this.typeID = relTypeID != -1 ? relTypeID : throw new ArgumentException();
  }

  /// <summary>Коллекция допустимых типов атрибутов</summary>
  public IDBAttribute4TypeCollection AttributeTypes4
  {
    get
    {
      return (MRPContextHelper.GetContextSession((IMRPContext) this) ?? throw new ArgumentNullException("session")).GetRelationType(this.TypeID, true).Attributes;
    }
  }

  /// <summary>Тип коллекции, которому принадлежит атрибут</summary>
  public AttributeSourceTypes AttributeSourceType
  {
    [DebuggerStepThrough] get => AttributeSourceTypes.Relation;
  }

  /// <summary>Идентификатор типа атрибута-описателя</summary>
  public int CaptionAttributeID
  {
    [DebuggerStepThrough] get => 0;
  }

  /// <summary>Идентификатор типа связи</summary>
  public int TypeID
  {
    [DebuggerStepThrough] get => this.typeID;
  }
}
