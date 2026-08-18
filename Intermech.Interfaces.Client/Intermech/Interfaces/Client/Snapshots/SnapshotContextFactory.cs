// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.Snapshots.SnapshotContextFactory
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Diagnostics;
using System;

#nullable disable
namespace Intermech.Interfaces.Client.Snapshots;

/// <summary>Фабрика интерфейса контекста итерации</summary>
public abstract class SnapshotContextFactory
{
  /// <summary>Статический метод-фабрика</summary>
  /// <param name="serviceProvider"></param>
  /// <returns>Созданный ISnapshotContext</returns>
  [NotNull]
  public static ISnapshotContext Create([NotNull] IServiceProvider serviceProvider)
  {
    return (ISnapshotContext) new SnapshotContextFactory.SnapshotContext(serviceProvider);
  }

  /// <summary>Реализация ISnapshotContext - интерфейса сущности в контексте итерации</summary>
  protected class SnapshotContext : ISnapshotContext
  {
    public SnapshotContext([NotNull] IServiceProvider serviceProvider)
    {
      this.Snapshot = serviceProvider.GetService<ISnapshot>();
    }

    /// <summary>Интерфейс итерации</summary>
    [NotNull]
    public ISnapshot Snapshot { get; }

    /// <summary>Идентификатор итерации</summary>
    [NotEmpty]
    public long SnapshotID => this.Snapshot.ID;
  }
}
