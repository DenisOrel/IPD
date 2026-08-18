// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.MetadataInfoParentContext
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Вспомогательный объект, связывающий родительский сервис <see cref="T:Intermech.Interfaces.Client.ClientMetadataCacheService" /> и его внутренние объекты.
/// Реализация является thread safe.
/// </summary>
internal sealed class MetadataInfoParentContext
{
  /// <summary>Создает объект</summary>
  /// <param name="parent">родительский сервис</param>
  /// <param name="clientCache">сервис кэша метаданных</param>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="parent" /> содержит null; параметр <paramref name="clientCache" /> содержит null</exception>
  public MetadataInfoParentContext(ClientMetadataCacheService parent, IClientCache clientCache)
  {
    if (parent == null)
      throw new ArgumentNullException(nameof (parent));
    if (clientCache == null)
      throw new ArgumentNullException(nameof (clientCache));
    this.Parent = parent;
    this.ClientCache = clientCache;
  }

  /// <summary>Возвращает родительский сервис</summary>
  public ClientMetadataCacheService Parent { get; }

  /// <summary>Возвращает сервис кэша метаданных</summary>
  public IClientCache ClientCache { get; }
}
