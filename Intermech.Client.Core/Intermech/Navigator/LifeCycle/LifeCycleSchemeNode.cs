
// Type: Intermech.Navigator.LifeCycle.LifeCycleSchemeNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Navigator.LifeCycle;

/// <summary>Виртуальный узел "Схема жизненного цикла"</summary>
public class LifeCycleSchemeNode : CompositeNode, IContextAware
{
  /// <summary>Заголовок узла</summary>
  protected internal string caption;
  /// <summary>Идентификатор схемы жизненного цикла</summary>
  protected internal int id;
  /// <summary>Контейнер сервисов</summary>
  private IServiceProvider services;

  /// <summary>Создать узел</summary>
  public LifeCycleSchemeNode() => this.options = NodeOptions.None;

  /// <summary>Создать узел, заполнить данными</summary>
  /// <param name="id">Идентификатор схемы жизненного цикла</param>
  public LifeCycleSchemeNode(int id)
  {
    this.id = id;
    this.caption = MetaDataHelper.GetLCSchemaName(id);
    this.options = NodeOptions.None;
  }

  /// <summary>Контейнер сервисов</summary>
  public IServiceProvider Services
  {
    [DebuggerStepThrough] get => this.services;
    [DebuggerStepThrough] set => this.services = value;
  }

  /// <summary>Вернуть список слотов-папок</summary>
  /// <returns>Слоты-папки</returns>
  protected override List<PartSlot> CreateNonFolderSlots()
  {
    List<PartSlot> nonFolderSlots = base.CreateNonFolderSlots() ?? new List<PartSlot>();
    nonFolderSlots.Add(new PartSlot(Intermech.Navigator.Consts.CategoryLifeCycleSchemeNodeGuid, (INodePart) new LifeCycleSchemeStepsPart(this.id, this.services)));
    return nonFolderSlots;
  }
}
