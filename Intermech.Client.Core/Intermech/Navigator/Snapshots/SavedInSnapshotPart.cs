
// Type: Intermech.Navigator.Snapshots.SavedInSnapshotPart
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Client.Snapshots;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Navigator.VirtualNodes;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace Intermech.Navigator.Snapshots;

/// <summary>Содержимое итерации, позволяющая отобразить в её составе сохранённый состав в итерации состав объекта</summary>
public class SavedInSnapshotPart : 
  DescriptorsPart,
  INodePart,
  INodeItems,
  IContextAware,
  ISnapshotContext
{
  /// <summary>Контейнер сервисов</summary>
  [NotNull]
  protected readonly AdvancedServiceContainer _Services = new AdvancedServiceContainer();
  /// <summary>Интерфейс итерации</summary>
  private ISnapshot _snapshot;

  /// <summary>Конструктор</summary>
  /// <param name="ownerServices">Контекст</param>
  /// <param name="descriptor">Дескриптор содержимого</param>
  protected SavedInSnapshotPart([NotNull] IServiceProvider ownerServices, [NotNull] IDescriptor descriptor)
    : base((DescriptorCollection) new AdvDescriptorCollection(descriptor))
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="ownerServices">Контекст</param>
  public SavedInSnapshotPart([NotNull] IServiceProvider ownerServices)
    : this(ownerServices, (IDescriptor) ObjectInSnapshotDescriptor.Create(ownerServices))
  {
  }

  /// <summary>Контейнер сервисов</summary>
  [NotNull]
  public IServiceProvider Services
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (IServiceProvider) this._Services;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._Services.AdvancedProvider = value;
    }
  }

  /// <summary>Интерфейс итерации</summary>
  public ISnapshot Snapshot
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.Services.EnsureInitialized<ISnapshot>(ref this._snapshot);
    }
  }

  /// <summary>Идентификатор итерации</summary>
  public long SnapshotID => this.Snapshot.ID;

  /// <summary>Получить интерфейс объекта-запроса к источнику данных, используемого для чтения содержимого элементов из пространства
  /// навигации</summary>
  /// <returns>Интерфейс объекта-запроса к источнику данных или null</returns>
  INodeQuery INodePart.GetQuery()
  {
    return (INodeQuery) new SavedInSnapshotQuery(this._descriptors, this._sortedQueries);
  }
}
