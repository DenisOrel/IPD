// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.IMRPAttributableTypeRef
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using Intermech.Kernel.Search;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Интерфейс, ссылающийся на допустимую коллекцию атрибутов у какого-то элемента
/// </summary>
public interface IMRPAttributableTypeRef : IMRPContext
{
  /// <summary>Коллекция допустимых типов атрибутов</summary>
  IDBAttribute4TypeCollection AttributeTypes4 { get; }

  /// <summary>Тип коллекции, которому принадлежит атрибут</summary>
  AttributeSourceTypes AttributeSourceType { get; }

  /// <summary>Идентификатор типа атрибута-описателя</summary>
  int CaptionAttributeID { get; }
}
