// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.StructureView.ArchiveStructureNode
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Archives.StructureView;

internal class ArchiveStructureNode : CompositeNode, IContextAware
{
  /// <summary>Идентификатор выбранного архива</summary>
  protected long arcID;
  /// <summary>Контейнер сервисов</summary>
  private IServiceProvider services;

  /// <summary>Создать узел, заполнить данными</summary>
  /// <param name="id">Идентификатор выбранного архива</param>
  public ArchiveStructureNode(long id)
  {
    this.arcID = id;
    this.options = NodeOptions.None;
  }

  protected override List<PartSlot> CreateNonFolderSlots()
  {
    return this.SlotsFromSinglePart((INodePart) new ArchiveStructureNodePart(this.arcID, this.services));
  }

  public IServiceProvider Services
  {
    get => this.services;
    set => this.services = value;
  }
}
