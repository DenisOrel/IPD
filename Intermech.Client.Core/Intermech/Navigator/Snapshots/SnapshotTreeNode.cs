
// Type: Intermech.Navigator.Snapshots.SnapshotTreeNode
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
using System.Collections.Generic;
using System.Runtime.CompilerServices;


namespace Intermech.Navigator.Snapshots;

/// <summary>Корневой нод структуры снимка объекта, отображает имя снимка, содержит в себе объект, взятый за основу снимка
/// Сделано для отображения структуры итерации в дереве навигатора на основе ноды итерации, отображаемой в гриде
/// и написанной некогда Бобко.
/// 
///   Содержимым ноды может быть разным (например сохранённый состав, либо сравнение сохранённого состава с
/// актуальным, в будущем будут другие варианты) в зависимости от значения SnapshotDescriptor.Content,
/// которое должно прийти в сервисах (контексте). Значение SnapshotDescriptor.Content контекста инициализируется
/// например в конструкторе дескриптора итерации (можно и в сервисах дерева, и любым подобным образом)
///   Можно было конечно написать потомок ноды для каждого подобного случая, но архитектура навигатора слегка
/// overengenered, и в каждом подобном случае требовало бы создания потомков не одного, а целой кучи классов, что
/// неоправданно</summary>
public class SnapshotTreeNode : SnapshotsNode, IContextAware, ISnapshotContext
{
  /// <summary>Содержимое итерации</summary>
  private INodePart _part;
  /// <summary>Контейнер сервисов</summary>
  [NotNull]
  protected AdvancedServiceContainer _Context;

  /// <summary>Конструктор</summary>
  /// <param name="context">Контекст</param>
  /// <param name="snapshot">Интерфейс итерации</param>
  public SnapshotTreeNode([NotNull] IServiceProvider context, [NotNull] ISnapshot snapshot)
    : base(snapshot.ID, snapshot.RootObjectVersionID)
  {
    this.Options = NodeOptions.CanContainsComposition;
    this.Snapshot = snapshot;
    this._Context = new AdvancedServiceContainer(context);
  }

  /// <summary>Содержимое итерации
  /// В зависимости от значение SnapshotDescriptor.Content, которая должна содержаться в контексте (сервисах), содержимое может быть
  /// разным - сохранённый в итерации состав, сравнение сохранённого состава с актуальным и т.д.</summary>
  [NotNull]
  public INodePart Part
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._part ?? (this._part = this.SnapshotContent == SnapshotDescriptor.Content.SavedStructure ? (INodePart) new SavedInSnapshotPart((IServiceProvider) this._Context) : (INodePart) new CompareObjectWithSavedInSnapshotPart((IServiceProvider) this._Context));
    }
  }

  /// <summary>Создает и возвращает часть, которая отвечает за дочерние элементы-папки</summary>
  /// <returns>Ссылка на интерфейс части</returns>
  [NotNull]
  protected override List<PartSlot> CreateFolderSlots() => this.SlotsFromSinglePart(this.Part);

  /// <summary>Контейнер сервисов</summary>
  [NotNull]
  public new virtual IServiceProvider Services
  {
    get => (IServiceProvider) this._Context;
    set => this._Context.AdvancedProvider = value;
  }

  /// <summary>Интерфейс итерации</summary>
  public ISnapshot Snapshot { get; set; }

  /// <summary>Идентификатор итерации</summary>
  public long SnapshotID => this.Snapshot.ID;

  /// <summary>Что отображается в содержимом итерации</summary>
  public SnapshotDescriptor.Content SnapshotContent
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      SnapshotDescriptor.Content service;
      return !this.Services.TryGetService<SnapshotDescriptor.Content>(out service) ? SnapshotDescriptor.Content.SavedStructure : service;
    }
  }
}
