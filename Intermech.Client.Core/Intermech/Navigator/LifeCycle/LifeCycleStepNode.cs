
// Type: Intermech.Navigator.LifeCycle.LifeCycleStepNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Navigator.LifeCycle;

/// <summary>Виртуальный узел "Шаг жизненного цикла"</summary>
public class LifeCycleStepNode : CompositeNode, IContextAware
{
  /// <summary>Заголовок узла</summary>
  protected internal string caption;
  /// <summary>Идентификатор шага жизненного цикла</summary>
  protected internal int id;
  /// <summary>Контейнер сервисов</summary>
  private IServiceProvider services;

  /// <summary>Создать узел</summary>
  public LifeCycleStepNode() => this.options = NodeOptions.None;

  /// <summary>Создать узел, заполнить данными</summary>
  /// <param name="id">Идентификатор шага жизненного цикла</param>
  public LifeCycleStepNode(int id)
  {
    this.id = id;
    this.caption = MetaDataHelper.GetLCStepName(id);
    this.options = NodeOptions.None;
  }

  /// <summary>Контейнер сервисов</summary>
  public IServiceProvider Services
  {
    [DebuggerStepThrough] get => this.services;
    [DebuggerStepThrough] set => this.services = value;
  }

  protected override List<PartSlot> CreateFolderSlots() => (List<PartSlot>) null;

  protected override List<PartSlot> CreateNonFolderSlots() => (List<PartSlot>) null;

  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    return nodeID is LifeCycleStepNodeID lifeCycleStepNodeId && dataFormat == typeof (IDBLCStepID) ? (object) new DBLCStepID(lifeCycleStepNodeId.id, MetaDataHelper.GetLCStepName(lifeCycleStepNodeId.id)) : base.GetData(nodeID, dataFormat);
  }
}
