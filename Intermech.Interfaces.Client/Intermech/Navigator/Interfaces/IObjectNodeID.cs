// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.IObjectNodeID
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>Интерфейс идентификатора ноды объекта (не обязательно реально существующего в БД)</summary>
public interface IObjectNodeID : INodeID
{
  /// <summary>Идентификатор версии объекта</summary>
  long ObjectVersionID { get; }

  /// <summary>Идентификатор объекта (НЕ ВЕРСИИ!!!)</summary>
  long ObjectID { get; }

  /// <summary>Тип объекта</summary>
  int ObjTypeId { get; }

  /// <summary>Заголовок объекта</summary>
  string Caption { get; }
}
