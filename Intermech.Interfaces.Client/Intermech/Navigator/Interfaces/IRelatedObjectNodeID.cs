// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.IRelatedObjectNodeID
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>Интерфейс идентификатора ноды объекта (не обязательно реально существующего в БД) в составе другого объекта</summary>
public interface IRelatedObjectNodeID : IObjectNodeID, INodeID
{
  /// <summary>Идентификатор связи</summary>
  long PrjLinkID { get; }

  /// <summary>Идентификатор типа связи</summary>
  int RelationTypeID { get; }

  /// <summary>Идентификатор версии родительского объекта (для связи)</summary>
  long ProjID { get; }

  /// <summary>Guid связи</summary>
  Guid RelGuid { get; }
}
