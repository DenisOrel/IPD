// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.NodesInsertPosition
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Куда добавлять новые узлы</summary>
public enum NodesInsertPosition
{
  /// <summary>Узлы добавляются в начало списка</summary>
  Start,
  /// <summary>Узлы добавляются перед указанным узлом</summary>
  Before,
  /// <summary>Узлы добавляются после указанного узла</summary>
  After,
  /// <summary>Узлы добавляются в конец списка</summary>
  End,
}
