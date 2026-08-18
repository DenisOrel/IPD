// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.INodesFactorySupported
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>Поддержка контекстом фабрики нод</summary>
public interface INodesFactorySupported
{
  /// <summary>Получение фабрики нод в указанном контексте для идентификатора ноды</summary>
  INodesFactory GetNodesFactory(IServiceProvider services, INodeID nodeID);
}
