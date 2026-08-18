// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPObjectAttributesRef
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
/// Класс, ссылающийся на допустимую коллекцию атрибутов у типа объекта
/// </summary>
public class MRPObjectAttributesRef : MRPContext, IMRPAttributableTypeRef, IMRPContext, IMRPTypedItem
{
  /// <summary>Идентификатор типа объекта</summary>
  protected int typeID = -1;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="service">Контейнер сервисов</param>
  /// <param name="objectTypeID">Идентификатор типа объекта</param>
  public MRPObjectAttributesRef(IServiceProvider service, int objectTypeID)
    : base(service)
  {
    this.typeID = objectTypeID != -1 ? objectTypeID : throw new ArgumentException();
  }

  /// <summary>Коллекция допустимых типов атрибутов</summary>
  public IDBAttribute4TypeCollection AttributeTypes4
  {
    get
    {
      return (MRPContextHelper.GetContextSession((IMRPContext) this) ?? throw new ArgumentNullException("session")).GetObjectType(this.TypeID, true).Attributes;
    }
  }

  /// <summary>Тип коллекции, которому принадлежит атрибут</summary>
  public AttributeSourceTypes AttributeSourceType
  {
    [DebuggerStepThrough] get => AttributeSourceTypes.Object;
  }

  /// <summary>Идентификатор типа атрибута-описателя</summary>
  public int CaptionAttributeID
  {
    get
    {
      return (MRPContextHelper.GetContextSession((IMRPContext) this) ?? throw new ArgumentNullException("session")).GetObjectType(this.TypeID, true).CaptionAttribute;
    }
  }

  /// <summary>Идентификатор типа объекта</summary>
  public int TypeID
  {
    [DebuggerStepThrough] get => this.typeID;
  }
}
